// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13393
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13393
{
  private static byte[] sspq = new byte[1052]
  {
    (byte) 14,
    (byte) 7,
    (byte) 237,
    (byte) 8,
    (byte) 186,
    (byte) 75,
    (byte) 72,
    (byte) 58,
    (byte) 139,
    (byte) 27,
    (byte) 153,
    (byte) 81,
    (byte) 221,
    (byte) 133,
    (byte) 60,
    (byte) 244,
    (byte) 115,
    (byte) 191,
    (byte) 75,
    (byte) 18,
    (byte) 181,
    (byte) 217,
    (byte) 76,
    (byte) 177,
    (byte) 158,
    (byte) 236,
    (byte) 147,
    (byte) 209,
    (byte) 162,
    (byte) 7,
    (byte) 251,
    (byte) 39,
    (byte) 240 /*0xF0*/,
    (byte) 203,
    (byte) 217,
    (byte) 66,
    (byte) 118,
    (byte) 167,
    (byte) 204,
    (byte) 182,
    (byte) 114,
    (byte) 32 /*0x20*/,
    (byte) 45,
    (byte) 177,
    (byte) 147,
    (byte) 231,
    (byte) 63 /*0x3F*/,
    (byte) 3,
    (byte) 160 /*0xA0*/,
    (byte) 193,
    (byte) 180,
    (byte) 209,
    (byte) 102,
    (byte) 41,
    (byte) 28,
    (byte) 125,
    (byte) 142,
    (byte) 80 /*0x50*/,
    (byte) 51,
    (byte) 2,
    (byte) 108,
    (byte) 89,
    (byte) 142,
    (byte) 85,
    (byte) 181,
    (byte) 133,
    (byte) 20,
    (byte) 32 /*0x20*/,
    (byte) 132,
    (byte) 162,
    (byte) 241,
    (byte) 187,
    (byte) 27,
    (byte) 251,
    (byte) 254,
    (byte) 191,
    (byte) 172,
    (byte) 238,
    (byte) 234,
    (byte) 10,
    (byte) 36,
    (byte) 3,
    (byte) 24,
    (byte) 194,
    (byte) 240 /*0xF0*/,
    (byte) 59,
    (byte) 156,
    (byte) 224 /*0xE0*/,
    (byte) 224 /*0xE0*/,
    (byte) 177,
    (byte) 240 /*0xF0*/,
    (byte) 230,
    (byte) 86,
    (byte) 153,
    (byte) 218,
    (byte) 210,
    (byte) 177,
    (byte) 10,
    (byte) 100,
    (byte) 129,
    (byte) 33,
    (byte) 239,
    (byte) 8,
    (byte) 165,
    (byte) 243,
    (byte) 82,
    (byte) 182,
    (byte) 40,
    (byte) 254,
    (byte) 221,
    (byte) 223,
    (byte) 246,
    (byte) 133,
    (byte) 49,
    (byte) 182,
    (byte) 131,
    (byte) 225,
    (byte) 80 /*0x50*/,
    (byte) 128 /*0x80*/,
    (byte) 45,
    (byte) 188,
    (byte) 113,
    (byte) 205,
    (byte) 234,
    (byte) 162,
    (byte) 15,
    (byte) 230,
    (byte) 11,
    (byte) 164,
    (byte) 208 /*0xD0*/,
    (byte) 2,
    (byte) 141,
    (byte) 59,
    (byte) 189,
    (byte) 29,
    (byte) 125,
    (byte) 190,
    (byte) 168,
    (byte) 184,
    (byte) 32 /*0x20*/,
    (byte) 206,
    (byte) 230,
    (byte) 173,
    (byte) 245,
    (byte) 108,
    (byte) 137,
    (byte) 43,
    (byte) 183,
    (byte) 5,
    (byte) 224 /*0xE0*/,
    (byte) 55,
    (byte) 38,
    (byte) 48 /*0x30*/,
    (byte) 150,
    (byte) 222,
    (byte) 62,
    (byte) 72,
    (byte) 96 /*0x60*/,
    (byte) 251,
    (byte) 239,
    (byte) 77,
    (byte) 207,
    (byte) 76,
    (byte) 69,
    (byte) 247,
    (byte) 236,
    (byte) 228,
    (byte) 232,
    (byte) 76,
    (byte) 67,
    (byte) 168,
    (byte) 53,
    (byte) 53,
    (byte) 84,
    (byte) 215,
    (byte) 17,
    (byte) 72,
    (byte) 128 /*0x80*/,
    (byte) 12,
    (byte) 187,
    (byte) 82,
    (byte) 224 /*0xE0*/,
    (byte) 88,
    (byte) 69,
    (byte) 103,
    (byte) 100,
    (byte) 145,
    (byte) 131,
    (byte) 14,
    (byte) 243,
    (byte) 51,
    (byte) 248,
    (byte) 58,
    (byte) 215,
    (byte) 28,
    (byte) 171,
    (byte) 228,
    (byte) 181,
    (byte) 92,
    (byte) 214,
    (byte) 32 /*0x20*/,
    (byte) 112 /*0x70*/,
    (byte) 157,
    (byte) 158,
    (byte) 117,
    (byte) 180,
    (byte) 29,
    (byte) 134,
    (byte) 194,
    (byte) 118,
    (byte) 37,
    (byte) 188,
    (byte) 69,
    (byte) 151,
    (byte) 56,
    (byte) 226,
    (byte) 166,
    (byte) 23,
    (byte) 16 /*0x10*/,
    (byte) 12,
    (byte) 250,
    (byte) 51,
    (byte) 88,
    (byte) 35,
    (byte) 141,
    (byte) 12,
    (byte) 172,
    (byte) 62,
    (byte) 86,
    (byte) 182,
    (byte) 65,
    (byte) 159,
    (byte) 54,
    (byte) 105,
    (byte) 147,
    (byte) 168,
    (byte) 35,
    (byte) 136,
    (byte) 98,
    (byte) 173,
    (byte) 204,
    (byte) 211,
    (byte) 22,
    (byte) 160 /*0xA0*/,
    (byte) 105,
    (byte) 174,
    (byte) 10,
    (byte) 189,
    (byte) 121,
    (byte) 139,
    (byte) 177,
    (byte) 181,
    (byte) 26,
    (byte) 107,
    (byte) 58,
    (byte) 73,
    (byte) 141,
    (byte) 246,
    (byte) 182,
    (byte) 78,
    (byte) 52,
    (byte) 64 /*0x40*/,
    (byte) 220,
    (byte) 186,
    (byte) 133,
    (byte) 154,
    (byte) 233,
    (byte) 239,
    (byte) 158,
    (byte) 181,
    (byte) 201,
    (byte) 107,
    (byte) 20,
    (byte) 125,
    (byte) 136,
    (byte) 2,
    (byte) 229,
    (byte) 62,
    (byte) 115,
    (byte) 123,
    (byte) 152,
    (byte) 228,
    (byte) 117,
    (byte) 17,
    (byte) 139,
    (byte) 15,
    (byte) 118,
    (byte) 121,
    (byte) 120,
    (byte) 180,
    (byte) 38,
    (byte) 30,
    (byte) 124,
    (byte) 23,
    (byte) 231,
    (byte) 124,
    (byte) 204,
    (byte) 3,
    (byte) 22,
    (byte) 145,
    (byte) 116,
    (byte) 74,
    (byte) 190,
    (byte) 14,
    (byte) 195,
    (byte) 1,
    (byte) 18,
    (byte) 83,
    (byte) 140,
    (byte) 82,
    (byte) 82,
    (byte) 64 /*0x40*/,
    (byte) 14,
    (byte) 57,
    (byte) 89,
    (byte) 20,
    (byte) 219,
    (byte) 179,
    (byte) 93,
    (byte) 187,
    (byte) 173,
    (byte) 206,
    (byte) 76,
    (byte) 141,
    (byte) 52,
    (byte) 211,
    (byte) 90,
    (byte) 94,
    (byte) 219,
    (byte) 188,
    (byte) 233,
    (byte) 167,
    (byte) 128 /*0x80*/,
    (byte) 116,
    (byte) 113,
    (byte) 122,
    (byte) 253,
    (byte) 232,
    (byte) 86,
    (byte) 97,
    (byte) 177,
    (byte) 81,
    (byte) 179,
    (byte) 224 /*0xE0*/,
    (byte) 30,
    (byte) 212,
    (byte) 225,
    (byte) 51,
    (byte) 214,
    (byte) 180,
    (byte) 65,
    (byte) 116,
    (byte) 114,
    (byte) 154,
    (byte) 161,
    (byte) 130,
    (byte) 193,
    (byte) 155,
    (byte) 46,
    (byte) 0,
    (byte) 24,
    (byte) 128 /*0x80*/,
    (byte) 158,
    (byte) 198,
    (byte) 41,
    (byte) 1,
    (byte) 192 /*0xC0*/,
    (byte) 178,
    (byte) 117,
    (byte) 236,
    (byte) 183,
    (byte) 28,
    (byte) 19,
    (byte) 46,
    (byte) 190,
    (byte) 87,
    (byte) 101,
    (byte) 71,
    (byte) 172,
    (byte) 94,
    (byte) 24,
    (byte) 91,
    (byte) 111,
    (byte) 191,
    (byte) 248,
    (byte) 161,
    (byte) 114,
    (byte) 150,
    (byte) 190,
    (byte) 168,
    (byte) 110,
    (byte) 153,
    (byte) 174,
    (byte) 156,
    (byte) 223,
    (byte) 133,
    (byte) 197,
    (byte) 237,
    (byte) 63 /*0x3F*/,
    (byte) 114,
    (byte) 201,
    (byte) 81,
    (byte) 146,
    (byte) 37,
    (byte) 73,
    (byte) 134,
    (byte) 120,
    (byte) 137,
    (byte) 167,
    (byte) 44,
    (byte) 23,
    (byte) 1,
    (byte) 15,
    (byte) 139,
    (byte) 217,
    (byte) 24,
    (byte) 10,
    (byte) 170,
    (byte) 96 /*0x60*/,
    (byte) 68,
    (byte) 174,
    (byte) 53,
    (byte) 131,
    (byte) 246,
    (byte) 240 /*0xF0*/,
    (byte) 206,
    (byte) 224 /*0xE0*/,
    (byte) 30,
    (byte) 241,
    (byte) 33,
    (byte) 103,
    (byte) 181,
    (byte) 231,
    (byte) 230,
    (byte) 50,
    (byte) 197,
    (byte) 242,
    (byte) 224 /*0xE0*/,
    (byte) 169,
    (byte) 126,
    (byte) 232,
    (byte) 63 /*0x3F*/,
    (byte) 163,
    (byte) 73,
    (byte) 90,
    (byte) 39,
    (byte) 4,
    (byte) 71,
    (byte) 122,
    byte.MaxValue,
    (byte) 82,
    (byte) 12,
    (byte) 88,
    (byte) 75,
    (byte) 186,
    (byte) 208 /*0xD0*/,
    (byte) 73,
    (byte) 190,
    (byte) 94,
    (byte) 143,
    (byte) 205,
    (byte) 202,
    (byte) 64 /*0x40*/,
    (byte) 254,
    (byte) 132,
    (byte) 102,
    (byte) 132,
    (byte) 234,
    (byte) 134,
    (byte) 33,
    (byte) 152,
    (byte) 147,
    (byte) 189,
    (byte) 58,
    (byte) 244,
    (byte) 146,
    (byte) 207,
    (byte) 172,
    (byte) 200,
    (byte) 100,
    (byte) 170,
    (byte) 155,
    (byte) 105,
    (byte) 215,
    (byte) 92,
    (byte) 122,
    byte.MaxValue,
    (byte) 190,
    (byte) 118,
    (byte) 215,
    (byte) 79,
    (byte) 254,
    (byte) 48 /*0x30*/,
    (byte) 57,
    (byte) 182,
    (byte) 90,
    (byte) 139,
    (byte) 99,
    (byte) 213,
    (byte) 84,
    (byte) 131,
    (byte) 143,
    (byte) 114,
    (byte) 11,
    (byte) 223,
    (byte) 250,
    (byte) 232,
    (byte) 193,
    (byte) 165,
    (byte) 179,
    (byte) 36,
    (byte) 248,
    (byte) 5,
    (byte) 57,
    (byte) 102,
    (byte) 57,
    (byte) 251,
    (byte) 115,
    (byte) 198,
    (byte) 140,
    (byte) 136,
    (byte) 117,
    (byte) 201,
    (byte) 66,
    (byte) 246,
    (byte) 162,
    (byte) 21,
    (byte) 123,
    (byte) 155,
    (byte) 42,
    (byte) 13,
    (byte) 192 /*0xC0*/,
    (byte) 108,
    (byte) 195,
    (byte) 43,
    (byte) 59,
    (byte) 148,
    (byte) 14,
    (byte) 245,
    (byte) 220,
    (byte) 137,
    (byte) 151,
    (byte) 191,
    (byte) 103,
    (byte) 123,
    (byte) 247,
    (byte) 83,
    (byte) 239,
    (byte) 141,
    (byte) 47,
    (byte) 132,
    (byte) 203,
    (byte) 170,
    (byte) 37,
    (byte) 7,
    (byte) 110,
    (byte) 209,
    (byte) 134,
    (byte) 38,
    (byte) 10,
    (byte) 33,
    (byte) 133,
    (byte) 207,
    (byte) 190,
    (byte) 90,
    (byte) 69,
    (byte) 10,
    (byte) 176 /*0xB0*/,
    (byte) 204,
    (byte) 198,
    (byte) 89,
    (byte) 210,
    (byte) 54,
    (byte) 226,
    (byte) 10,
    (byte) 0,
    (byte) 27,
    (byte) 136,
    (byte) 44,
    (byte) 118,
    (byte) 249,
    (byte) 48 /*0x30*/,
    (byte) 206,
    (byte) 97,
    (byte) 139,
    (byte) 231,
    byte.MaxValue,
    (byte) 241,
    (byte) 156,
    (byte) 99,
    (byte) 71,
    (byte) 149,
    (byte) 217,
    (byte) 56,
    (byte) 253,
    (byte) 3,
    (byte) 226,
    (byte) 32 /*0x20*/,
    (byte) 171,
    (byte) 249,
    (byte) 213,
    (byte) 96 /*0x60*/,
    (byte) 210,
    (byte) 52,
    (byte) 8,
    (byte) 4,
    (byte) 173,
    (byte) 110,
    (byte) 82,
    (byte) 37,
    (byte) 76,
    (byte) 44,
    (byte) 81,
    (byte) 149,
    (byte) 1,
    (byte) 223,
    (byte) 96 /*0x60*/,
    (byte) 117,
    (byte) 38,
    (byte) 64 /*0x40*/,
    (byte) 45,
    (byte) 194,
    (byte) 135,
    (byte) 83,
    (byte) 197,
    (byte) 30,
    (byte) 19,
    (byte) 90,
    (byte) 25,
    (byte) 10,
    (byte) 235,
    (byte) 61,
    (byte) 89,
    (byte) 81,
    (byte) 133,
    (byte) 221,
    (byte) 125,
    (byte) 164,
    (byte) 7,
    (byte) 153,
    (byte) 177,
    (byte) 61,
    (byte) 143,
    (byte) 39,
    (byte) 135,
    (byte) 68,
    (byte) 21,
    (byte) 178,
    (byte) 195,
    (byte) 237,
    (byte) 192 /*0xC0*/,
    (byte) 94,
    (byte) 145,
    (byte) 150,
    (byte) 184,
    (byte) 224 /*0xE0*/,
    (byte) 157,
    (byte) 50,
    (byte) 246,
    (byte) 62,
    (byte) 210,
    (byte) 94,
    (byte) 176 /*0xB0*/,
    (byte) 70,
    (byte) 204,
    (byte) 27,
    (byte) 73,
    (byte) 228,
    (byte) 185,
    (byte) 169,
    (byte) 45,
    (byte) 61,
    (byte) 213,
    (byte) 120,
    (byte) 217,
    (byte) 236,
    (byte) 105,
    (byte) 147,
    (byte) 231,
    (byte) 196,
    (byte) 242,
    (byte) 180,
    (byte) 41,
    (byte) 132,
    (byte) 206,
    (byte) 77,
    (byte) 35,
    (byte) 75,
    (byte) 26,
    (byte) 16 /*0x10*/,
    (byte) 222,
    (byte) 11,
    (byte) 142,
    (byte) 233,
    (byte) 66,
    (byte) 142,
    (byte) 119,
    (byte) 222,
    (byte) 208 /*0xD0*/,
    (byte) 104,
    (byte) 86,
    (byte) 232,
    (byte) 23,
    (byte) 103,
    (byte) 216,
    (byte) 164,
    (byte) 112 /*0x70*/,
    (byte) 33,
    (byte) 239,
    (byte) 3,
    (byte) 171,
    (byte) 3,
    (byte) 50,
    (byte) 63 /*0x3F*/,
    (byte) 126,
    (byte) 89,
    (byte) 108,
    (byte) 106,
    (byte) 212,
    (byte) 58,
    (byte) 59,
    (byte) 252,
    (byte) 185,
    (byte) 161,
    (byte) 231,
    (byte) 22,
    (byte) 232,
    (byte) 81,
    (byte) 248,
    (byte) 110,
    (byte) 207,
    (byte) 116,
    (byte) 131,
    (byte) 92,
    (byte) 50,
    (byte) 114,
    (byte) 150,
    (byte) 124,
    (byte) 88,
    (byte) 115,
    (byte) 11,
    (byte) 34,
    (byte) 245,
    (byte) 172,
    (byte) 247,
    (byte) 126,
    (byte) 128 /*0x80*/,
    (byte) 100,
    (byte) 36,
    (byte) 36,
    (byte) 238,
    (byte) 204,
    (byte) 125,
    (byte) 112 /*0x70*/,
    (byte) 66,
    (byte) 167,
    (byte) 175,
    (byte) 70,
    (byte) 29,
    (byte) 57,
    (byte) 178,
    (byte) 194,
    (byte) 34,
    (byte) 10,
    (byte) 61,
    (byte) 127 /*0x7F*/,
    (byte) 169,
    (byte) 205,
    (byte) 157,
    (byte) 54,
    (byte) 21,
    (byte) 47,
    (byte) 105,
    (byte) 159,
    (byte) 113,
    (byte) 93,
    (byte) 20,
    (byte) 108,
    (byte) 102,
    (byte) 31 /*0x1F*/,
    (byte) 118,
    (byte) 6,
    (byte) 184,
    (byte) 175,
    (byte) 19,
    (byte) 7,
    (byte) 200,
    (byte) 85,
    (byte) 242,
    (byte) 219,
    (byte) 58,
    (byte) 227,
    (byte) 33,
    (byte) 51,
    (byte) 118,
    (byte) 118,
    (byte) 161,
    (byte) 30,
    (byte) 132,
    (byte) 17,
    (byte) 112 /*0x70*/,
    (byte) 72,
    (byte) 70,
    (byte) 251,
    (byte) 32 /*0x20*/,
    (byte) 9,
    (byte) 223,
    (byte) 222,
    (byte) 246,
    (byte) 102,
    (byte) 29,
    (byte) 129,
    (byte) 99,
    (byte) 14,
    (byte) 26,
    (byte) 118,
    (byte) 57,
    (byte) 186,
    (byte) 150,
    (byte) 28,
    (byte) 10,
    (byte) 186,
    (byte) 108,
    (byte) 228,
    (byte) 151,
    (byte) 252,
    (byte) 90,
    (byte) 230,
    (byte) 75,
    (byte) 208 /*0xD0*/,
    (byte) 44,
    (byte) 251,
    (byte) 139,
    (byte) 245,
    (byte) 187,
    (byte) 54,
    (byte) 114,
    (byte) 142,
    (byte) 233,
    (byte) 26,
    (byte) 37,
    (byte) 62,
    (byte) 237,
    (byte) 182,
    (byte) 120,
    (byte) 229,
    (byte) 240 /*0xF0*/,
    (byte) 204,
    (byte) 159,
    (byte) 193,
    (byte) 168,
    (byte) 35,
    (byte) 26,
    (byte) 55,
    (byte) 158,
    (byte) 171,
    (byte) 117,
    (byte) 134,
    (byte) 201,
    (byte) 137,
    (byte) 66,
    (byte) 113,
    (byte) 242,
    (byte) 44,
    (byte) 227,
    (byte) 24,
    (byte) 182,
    (byte) 24,
    (byte) 233,
    (byte) 236,
    (byte) 186,
    (byte) 218,
    (byte) 223,
    (byte) 66,
    (byte) 249,
    (byte) 18,
    (byte) 118,
    (byte) 120,
    (byte) 180,
    (byte) 225,
    (byte) 210,
    (byte) 6,
    (byte) 61,
    (byte) 126,
    (byte) 2,
    (byte) 88,
    (byte) 138,
    (byte) 184,
    (byte) 130,
    (byte) 67,
    (byte) 141,
    (byte) 16 /*0x10*/,
    (byte) 21,
    (byte) 196,
    (byte) 230,
    (byte) 71,
    (byte) 44,
    (byte) 179,
    (byte) 227,
    (byte) 102,
    (byte) 142,
    (byte) 47,
    (byte) 20,
    (byte) 32 /*0x20*/,
    (byte) 171,
    (byte) 67,
    (byte) 144 /*0x90*/,
    (byte) 96 /*0x60*/,
    (byte) 98,
    (byte) 45,
    (byte) 43,
    (byte) 142,
    (byte) 192 /*0xC0*/,
    (byte) 210,
    (byte) 105,
    (byte) 8,
    (byte) 206,
    (byte) 115,
    (byte) 13,
    (byte) 62,
    (byte) 235,
    (byte) 139,
    (byte) 53,
    (byte) 14,
    (byte) 120,
    (byte) 203,
    (byte) 102,
    (byte) 101,
    (byte) 168,
    (byte) 165,
    (byte) 172,
    (byte) 83,
    (byte) 163,
    (byte) 2,
    (byte) 61,
    (byte) 64 /*0x40*/,
    (byte) 73,
    (byte) 210,
    (byte) 57,
    (byte) 122,
    (byte) 59,
    (byte) 21,
    (byte) 208 /*0xD0*/,
    (byte) 2,
    (byte) 231,
    (byte) 135,
    (byte) 14,
    (byte) 204,
    (byte) 169,
    (byte) 101,
    (byte) 156,
    (byte) 251,
    (byte) 222,
    (byte) 26,
    (byte) 198,
    (byte) 72,
    (byte) 109,
    (byte) 181,
    (byte) 92,
    (byte) 47,
    (byte) 55,
    (byte) 95,
    (byte) 188,
    (byte) 43,
    (byte) 48 /*0x30*/,
    (byte) 45,
    (byte) 102,
    (byte) 192 /*0xC0*/,
    (byte) 217,
    (byte) 35,
    (byte) 145,
    (byte) 138,
    (byte) 230,
    (byte) 173,
    byte.MaxValue,
    (byte) 245,
    (byte) 168,
    (byte) 102,
    (byte) 187,
    (byte) 12,
    (byte) 206,
    (byte) 192 /*0xC0*/,
    (byte) 11,
    (byte) 37,
    (byte) 203,
    (byte) 77,
    (byte) 234,
    (byte) 80 /*0x50*/,
    (byte) 133,
    (byte) 23,
    (byte) 102,
    (byte) 110,
    (byte) 238,
    (byte) 133,
    (byte) 211,
    (byte) 20,
    (byte) 235,
    (byte) 93,
    (byte) 98,
    (byte) 113,
    (byte) 147,
    (byte) 12,
    (byte) 167,
    (byte) 186,
    (byte) 111,
    byte.MaxValue,
    (byte) 48 /*0x30*/,
    (byte) 144 /*0x90*/,
    (byte) 152,
    (byte) 193,
    (byte) 200,
    (byte) 21,
    (byte) 39,
    (byte) 214,
    (byte) 164,
    (byte) 184,
    (byte) 38,
    (byte) 39,
    (byte) 58,
    (byte) 208 /*0xD0*/,
    (byte) 240 /*0xF0*/,
    (byte) 153,
    (byte) 73,
    (byte) 197,
    (byte) 1,
    (byte) 160 /*0xA0*/,
    (byte) 176 /*0xB0*/,
    (byte) 12,
    (byte) 244,
    (byte) 159,
    (byte) 38,
    (byte) 247,
    (byte) 241,
    (byte) 158,
    (byte) 157,
    (byte) 86,
    (byte) 95,
    (byte) 167,
    byte.MaxValue,
    (byte) 82,
    (byte) 47,
    (byte) 41,
    (byte) 164,
    (byte) 22,
    (byte) 30,
    (byte) 130,
    (byte) 203,
    (byte) 3,
    (byte) 160 /*0xA0*/,
    (byte) 223,
    (byte) 156,
    (byte) 124
  };
  private static byte[] sspr = new byte[1052]
  {
    (byte) 46,
    (byte) 183,
    (byte) 169,
    (byte) 91,
    (byte) 212,
    (byte) 226,
    (byte) 225,
    (byte) 64 /*0x40*/,
    (byte) 56,
    (byte) 69,
    (byte) 110,
    (byte) 28,
    (byte) 105,
    (byte) 161,
    (byte) 52,
    (byte) 66,
    (byte) 226,
    (byte) 64 /*0x40*/,
    (byte) 10,
    (byte) 152,
    (byte) 77,
    (byte) 192 /*0xC0*/,
    (byte) 84,
    (byte) 243,
    (byte) 153,
    (byte) 181,
    (byte) 190,
    (byte) 37,
    (byte) 146,
    (byte) 36,
    (byte) 238,
    (byte) 73,
    (byte) 217,
    (byte) 88,
    (byte) 165,
    (byte) 210,
    (byte) 176 /*0xB0*/,
    (byte) 89,
    (byte) 200,
    (byte) 110,
    (byte) 166,
    (byte) 230,
    (byte) 13,
    (byte) 243,
    (byte) 192 /*0xC0*/,
    (byte) 1,
    (byte) 241,
    (byte) 22,
    (byte) 186,
    (byte) 40,
    (byte) 224 /*0xE0*/,
    (byte) 31 /*0x1F*/,
    (byte) 23,
    (byte) 0,
    (byte) 117,
    (byte) 58,
    (byte) 173,
    (byte) 135,
    (byte) 168,
    (byte) 168,
    (byte) 108,
    (byte) 85,
    (byte) 183,
    (byte) 202,
    (byte) 41,
    (byte) 183,
    (byte) 115,
    (byte) 220,
    (byte) 210,
    (byte) 69,
    (byte) 213,
    (byte) 201,
    (byte) 41,
    (byte) 24,
    (byte) 30,
    (byte) 127 /*0x7F*/,
    (byte) 150,
    (byte) 130,
    (byte) 65,
    (byte) 183,
    (byte) 140,
    (byte) 54,
    (byte) 46,
    (byte) 186,
    (byte) 52,
    (byte) 20,
    (byte) 226,
    (byte) 174,
    (byte) 28,
    (byte) 24,
    (byte) 123,
    (byte) 148,
    (byte) 133,
    (byte) 243,
    (byte) 46,
    (byte) 47,
    (byte) 5,
    (byte) 249,
    (byte) 13,
    (byte) 222,
    (byte) 148,
    (byte) 246,
    (byte) 232,
    (byte) 179,
    (byte) 186,
    (byte) 179,
    (byte) 4,
    (byte) 0,
    (byte) 47,
    (byte) 183,
    (byte) 28,
    (byte) 18,
    (byte) 140,
    (byte) 253,
    (byte) 123,
    (byte) 21,
    (byte) 249,
    (byte) 115,
    (byte) 243,
    (byte) 205,
    (byte) 179,
    (byte) 199,
    (byte) 33,
    (byte) 40,
    (byte) 246,
    (byte) 6,
    (byte) 80 /*0x50*/,
    (byte) 188,
    (byte) 106,
    (byte) 92,
    (byte) 71,
    (byte) 123,
    (byte) 74,
    (byte) 7,
    (byte) 15,
    (byte) 22,
    (byte) 155,
    (byte) 64 /*0x40*/,
    (byte) 8,
    (byte) 251,
    (byte) 42,
    (byte) 101,
    (byte) 0,
    (byte) 214,
    (byte) 10,
    (byte) 240 /*0xF0*/,
    (byte) 96 /*0x60*/,
    (byte) 0,
    (byte) 195,
    (byte) 97,
    (byte) 178,
    (byte) 208 /*0xD0*/,
    (byte) 59,
    (byte) 51,
    (byte) 147,
    (byte) 175,
    (byte) 135,
    (byte) 110,
    (byte) 134,
    (byte) 218,
    byte.MaxValue,
    (byte) 169,
    (byte) 92,
    (byte) 193,
    (byte) 91,
    (byte) 91,
    (byte) 90,
    (byte) 217,
    (byte) 125,
    (byte) 3,
    (byte) 201,
    (byte) 200,
    (byte) 130,
    (byte) 138,
    (byte) 12,
    (byte) 83,
    (byte) 123,
    (byte) 69,
    (byte) 208 /*0xD0*/,
    (byte) 109,
    (byte) 158,
    (byte) 4,
    (byte) 99,
    (byte) 178,
    (byte) 205,
    (byte) 204,
    (byte) 124,
    (byte) 223,
    (byte) 237,
    (byte) 76,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 76,
    (byte) 126,
    (byte) 240 /*0xF0*/,
    (byte) 142,
    (byte) 47,
    (byte) 126,
    (byte) 236,
    (byte) 228,
    (byte) 134,
    (byte) 73,
    (byte) 62,
    (byte) 11,
    (byte) 109,
    (byte) 169,
    (byte) 192 /*0xC0*/,
    (byte) 17,
    (byte) 236,
    (byte) 68,
    (byte) 177,
    (byte) 234,
    (byte) 242,
    (byte) 31 /*0x1F*/,
    (byte) 185,
    (byte) 160 /*0xA0*/,
    (byte) 14,
    (byte) 253,
    (byte) 121,
    (byte) 153,
    (byte) 89,
    (byte) 93,
    (byte) 19,
    (byte) 79,
    (byte) 44,
    (byte) 56,
    (byte) 133,
    (byte) 159,
    (byte) 228,
    (byte) 57,
    (byte) 1,
    (byte) 253,
    (byte) 157,
    (byte) 195,
    (byte) 47,
    (byte) 160 /*0xA0*/,
    (byte) 216,
    (byte) 101,
    (byte) 28,
    (byte) 169,
    (byte) 111,
    (byte) 86,
    (byte) 64 /*0x40*/,
    (byte) 163,
    (byte) 56,
    (byte) 160 /*0xA0*/,
    (byte) 31 /*0x1F*/,
    (byte) 139,
    (byte) 140,
    (byte) 191,
    (byte) 103,
    (byte) 228,
    (byte) 27,
    (byte) 190,
    (byte) 78,
    (byte) 89,
    (byte) 105,
    (byte) 26,
    (byte) 228,
    (byte) 246,
    (byte) 95,
    (byte) 20,
    (byte) 112 /*0x70*/,
    (byte) 25,
    (byte) 207,
    (byte) 193,
    (byte) 211,
    (byte) 130,
    (byte) 196,
    (byte) 13,
    (byte) 86,
    (byte) 15,
    (byte) 252,
    (byte) 7,
    (byte) 45,
    (byte) 239,
    (byte) 141,
    (byte) 208 /*0xD0*/,
    (byte) 28,
    (byte) 90,
    (byte) 156,
    (byte) 47,
    (byte) 12,
    (byte) 23,
    (byte) 77,
    (byte) 93,
    (byte) 135,
    (byte) 133,
    (byte) 114,
    (byte) 165,
    (byte) 60,
    (byte) 127 /*0x7F*/,
    (byte) 227,
    (byte) 165,
    (byte) 218,
    (byte) 165,
    (byte) 0,
    (byte) 196,
    (byte) 180,
    (byte) 6,
    (byte) 85,
    (byte) 155,
    (byte) 240 /*0xF0*/,
    (byte) 240 /*0xF0*/,
    (byte) 69,
    (byte) 186,
    (byte) 36,
    (byte) 20,
    (byte) 138,
    (byte) 224 /*0xE0*/,
    (byte) 168,
    (byte) 54,
    (byte) 165,
    (byte) 106,
    (byte) 109,
    (byte) 40,
    (byte) 92,
    (byte) 205,
    (byte) 17,
    (byte) 115,
    (byte) 36,
    (byte) 19,
    (byte) 1,
    (byte) 193,
    (byte) 90,
    (byte) 137,
    (byte) 223,
    (byte) 135,
    (byte) 181,
    (byte) 214,
    (byte) 86,
    (byte) 5,
    (byte) 169,
    (byte) 211,
    (byte) 26,
    (byte) 31 /*0x1F*/,
    (byte) 145,
    (byte) 40,
    (byte) 81,
    (byte) 146,
    (byte) 180,
    (byte) 130,
    (byte) 169,
    (byte) 226,
    (byte) 52,
    (byte) 67,
    (byte) 208 /*0xD0*/,
    (byte) 49,
    (byte) 189,
    (byte) 236,
    (byte) 223,
    (byte) 196,
    (byte) 193,
    (byte) 42,
    (byte) 244,
    (byte) 153,
    (byte) 56,
    (byte) 218,
    (byte) 85,
    (byte) 40,
    (byte) 204,
    (byte) 231,
    (byte) 243,
    (byte) 130,
    (byte) 163,
    (byte) 215,
    (byte) 112 /*0x70*/,
    (byte) 232,
    (byte) 237,
    (byte) 144 /*0x90*/,
    (byte) 44,
    (byte) 115,
    (byte) 13,
    (byte) 44,
    (byte) 123,
    (byte) 162,
    (byte) 10,
    (byte) 117,
    (byte) 73,
    (byte) 55,
    (byte) 118,
    (byte) 97,
    (byte) 52,
    (byte) 60,
    (byte) 251,
    (byte) 203,
    (byte) 19,
    (byte) 16 /*0x10*/,
    (byte) 72,
    (byte) 214,
    (byte) 32 /*0x20*/,
    (byte) 195,
    (byte) 29,
    (byte) 26,
    (byte) 91,
    (byte) 163,
    (byte) 17,
    (byte) 230,
    (byte) 150,
    (byte) 123,
    (byte) 15,
    (byte) 24,
    (byte) 92,
    (byte) 88,
    (byte) 243,
    (byte) 61,
    (byte) 87,
    (byte) 129,
    (byte) 187,
    (byte) 91,
    (byte) 221,
    (byte) 246,
    (byte) 14,
    (byte) 241,
    (byte) 22,
    (byte) 160 /*0xA0*/,
    (byte) 73,
    (byte) 244,
    (byte) 102,
    (byte) 161,
    (byte) 38,
    (byte) 129,
    (byte) 140,
    (byte) 4,
    (byte) 53,
    (byte) 168,
    (byte) 168,
    (byte) 120,
    (byte) 23,
    (byte) 119,
    (byte) 127 /*0x7F*/,
    (byte) 47,
    (byte) 173,
    (byte) 162,
    (byte) 35,
    (byte) 191,
    (byte) 115,
    (byte) 153,
    (byte) 190,
    (byte) 46,
    (byte) 154,
    (byte) 237,
    (byte) 83,
    (byte) 170,
    (byte) 206,
    (byte) 159,
    (byte) 46,
    (byte) 196,
    (byte) 79,
    (byte) 240 /*0xF0*/,
    (byte) 180,
    (byte) 224 /*0xE0*/,
    (byte) 114,
    (byte) 216,
    (byte) 184,
    (byte) 71,
    (byte) 91,
    (byte) 162,
    (byte) 104,
    byte.MaxValue,
    (byte) 146,
    (byte) 210,
    (byte) 219,
    (byte) 133,
    (byte) 33,
    (byte) 96 /*0x60*/,
    (byte) 231,
    (byte) 219,
    (byte) 97,
    (byte) 233,
    (byte) 213,
    (byte) 179,
    (byte) 61,
    (byte) 79,
    (byte) 194,
    (byte) 4,
    (byte) 71,
    (byte) 74,
    (byte) 230,
    (byte) 13,
    (byte) 124,
    (byte) 248,
    (byte) 183,
    (byte) 228,
    (byte) 13,
    (byte) 197,
    (byte) 42,
    (byte) 11,
    (byte) 148,
    (byte) 222,
    (byte) 98,
    (byte) 149,
    (byte) 20,
    (byte) 244,
    (byte) 89,
    (byte) 238,
    (byte) 133,
    (byte) 121,
    (byte) 55,
    (byte) 185,
    (byte) 218,
    (byte) 226,
    (byte) 190,
    (byte) 55,
    (byte) 71,
    (byte) 72,
    (byte) 131,
    (byte) 229,
    (byte) 229,
    (byte) 133,
    (byte) 28,
    (byte) 252,
    (byte) 98,
    (byte) 137,
    (byte) 30,
    (byte) 165,
    (byte) 103,
    (byte) 146,
    (byte) 57,
    (byte) 143,
    (byte) 176 /*0xB0*/,
    (byte) 41,
    (byte) 220,
    (byte) 25,
    (byte) 16 /*0x10*/,
    (byte) 129,
    (byte) 115,
    (byte) 34,
    (byte) 131,
    (byte) 180,
    (byte) 185,
    (byte) 228,
    (byte) 206,
    (byte) 198,
    (byte) 187,
    (byte) 9,
    (byte) 109,
    (byte) 18,
    (byte) 99,
    (byte) 50,
    (byte) 107,
    (byte) 56,
    (byte) 96 /*0x60*/,
    (byte) 138,
    (byte) 30,
    (byte) 194,
    (byte) 82,
    (byte) 33,
    (byte) 100,
    (byte) 240 /*0xF0*/,
    (byte) 158,
    (byte) 193,
    (byte) 229,
    (byte) 40,
    (byte) 148,
    (byte) 22,
    (byte) 157,
    (byte) 234,
    (byte) 10,
    (byte) 43,
    (byte) 7,
    (byte) 253,
    (byte) 189,
    (byte) 67,
    (byte) 17,
    (byte) 97,
    (byte) 137,
    (byte) 141,
    (byte) 137,
    (byte) 113,
    (byte) 144 /*0x90*/,
    (byte) 154,
    (byte) 42,
    (byte) 223,
    (byte) 166,
    (byte) 99,
    (byte) 125,
    (byte) 212,
    (byte) 128 /*0x80*/,
    (byte) 123,
    (byte) 6,
    (byte) 147,
    (byte) 191,
    (byte) 97,
    (byte) 254,
    (byte) 212,
    (byte) 58,
    (byte) 61,
    (byte) 174,
    (byte) 5,
    (byte) 45,
    (byte) 134,
    (byte) 129,
    (byte) 179,
    (byte) 195,
    (byte) 72,
    (byte) 47,
    (byte) 207,
    (byte) 186,
    (byte) 131,
    (byte) 233,
    (byte) 173,
    (byte) 242,
    (byte) 43,
    (byte) 63 /*0x3F*/,
    (byte) 206,
    (byte) 16 /*0x10*/,
    (byte) 204,
    (byte) 17,
    (byte) 37,
    (byte) 207,
    (byte) 191,
    (byte) 140,
    (byte) 171,
    (byte) 91,
    (byte) 191,
    (byte) 21,
    (byte) 228,
    (byte) 72,
    (byte) 110,
    (byte) 168,
    (byte) 125,
    (byte) 153,
    (byte) 241,
    (byte) 152,
    (byte) 63 /*0x3F*/,
    (byte) 122,
    (byte) 22,
    (byte) 153,
    (byte) 148,
    (byte) 41,
    (byte) 16 /*0x10*/,
    (byte) 100,
    (byte) 73,
    (byte) 205,
    (byte) 178,
    (byte) 151,
    (byte) 254,
    (byte) 28,
    (byte) 27,
    (byte) 84,
    (byte) 56,
    (byte) 249,
    (byte) 9,
    (byte) 50,
    (byte) 215,
    (byte) 100,
    (byte) 227,
    (byte) 174,
    (byte) 77,
    (byte) 219,
    (byte) 69,
    (byte) 119,
    (byte) 135,
    (byte) 228,
    (byte) 64 /*0x40*/,
    (byte) 133,
    (byte) 152,
    (byte) 229,
    (byte) 28,
    (byte) 16 /*0x10*/,
    (byte) 161,
    (byte) 245,
    (byte) 104,
    (byte) 101,
    (byte) 154,
    (byte) 226,
    (byte) 157,
    (byte) 63 /*0x3F*/,
    (byte) 46,
    (byte) 48 /*0x30*/,
    (byte) 28,
    (byte) 234,
    (byte) 169,
    (byte) 21,
    (byte) 196,
    (byte) 113,
    (byte) 223,
    (byte) 141,
    (byte) 202,
    (byte) 254,
    (byte) 12,
    (byte) 149,
    (byte) 40,
    (byte) 35,
    (byte) 168,
    (byte) 53,
    (byte) 153,
    (byte) 118,
    (byte) 123,
    (byte) 164,
    (byte) 251,
    (byte) 202,
    (byte) 19,
    (byte) 79,
    (byte) 105,
    (byte) 52,
    (byte) 146,
    (byte) 157,
    (byte) 63 /*0x3F*/,
    (byte) 76,
    (byte) 148,
    (byte) 167,
    (byte) 17,
    (byte) 151,
    (byte) 228,
    (byte) 118,
    (byte) 6,
    (byte) 99,
    (byte) 64 /*0x40*/,
    (byte) 142,
    (byte) 182,
    (byte) 122,
    (byte) 103,
    (byte) 218,
    (byte) 116,
    (byte) 156,
    (byte) 79,
    (byte) 60,
    (byte) 20,
    (byte) 25,
    (byte) 186,
    (byte) 90,
    (byte) 168,
    (byte) 248,
    (byte) 35,
    byte.MaxValue,
    (byte) 170,
    (byte) 236,
    (byte) 160 /*0xA0*/,
    (byte) 37,
    (byte) 152,
    (byte) 194,
    (byte) 52,
    (byte) 249,
    (byte) 232,
    (byte) 53,
    (byte) 122,
    (byte) 66,
    (byte) 178,
    (byte) 153,
    (byte) 166,
    (byte) 230,
    (byte) 198,
    (byte) 33,
    (byte) 149,
    (byte) 61,
    (byte) 10,
    (byte) 106,
    (byte) 74,
    (byte) 157,
    (byte) 40,
    (byte) 46,
    (byte) 95,
    (byte) 110,
    (byte) 145,
    (byte) 254,
    (byte) 246,
    byte.MaxValue,
    (byte) 147,
    (byte) 188,
    (byte) 237,
    (byte) 241,
    (byte) 17,
    (byte) 2,
    (byte) 156,
    (byte) 4,
    (byte) 114,
    (byte) 78,
    (byte) 123,
    (byte) 252,
    (byte) 212,
    (byte) 234,
    (byte) 108,
    (byte) 138,
    (byte) 141,
    (byte) 163,
    (byte) 214,
    (byte) 3,
    (byte) 209,
    (byte) 156,
    (byte) 174,
    (byte) 190,
    (byte) 147,
    (byte) 120,
    (byte) 58,
    (byte) 147,
    (byte) 159,
    (byte) 220,
    (byte) 181,
    (byte) 28,
    (byte) 197,
    (byte) 55,
    (byte) 12,
    (byte) 121,
    (byte) 69,
    (byte) 18,
    (byte) 83,
    (byte) 159,
    (byte) 153,
    (byte) 2,
    (byte) 15,
    (byte) 139,
    (byte) 81,
    (byte) 131,
    (byte) 228,
    (byte) 113,
    (byte) 41,
    (byte) 106,
    (byte) 224 /*0xE0*/,
    (byte) 175,
    (byte) 87,
    (byte) 146,
    (byte) 169,
    (byte) 143,
    (byte) 3,
    (byte) 131,
    (byte) 254,
    (byte) 105,
    (byte) 228,
    (byte) 59,
    (byte) 16 /*0x10*/,
    (byte) 174,
    (byte) 45,
    (byte) 131,
    (byte) 115,
    (byte) 38,
    (byte) 80 /*0x50*/,
    (byte) 129,
    (byte) 216,
    (byte) 76,
    (byte) 115,
    (byte) 140,
    (byte) 125,
    byte.MaxValue,
    (byte) 203,
    (byte) 184,
    (byte) 67,
    (byte) 86,
    (byte) 185,
    (byte) 89,
    (byte) 182,
    (byte) 106,
    (byte) 60,
    (byte) 163,
    (byte) 235,
    (byte) 51,
    (byte) 58,
    (byte) 158,
    (byte) 217,
    (byte) 31 /*0x1F*/,
    (byte) 233,
    (byte) 64 /*0x40*/,
    (byte) 104,
    (byte) 107,
    (byte) 120,
    (byte) 203,
    (byte) 31 /*0x1F*/,
    (byte) 102,
    (byte) 18,
    (byte) 114,
    (byte) 63 /*0x3F*/,
    (byte) 168,
    (byte) 233,
    (byte) 93,
    (byte) 196,
    (byte) 111,
    (byte) 143,
    (byte) 79,
    (byte) 47,
    (byte) 129,
    (byte) 181,
    (byte) 137,
    (byte) 6,
    (byte) 5,
    (byte) 28,
    (byte) 90,
    (byte) 208 /*0xD0*/,
    (byte) 245,
    (byte) 55,
    (byte) 142,
    (byte) 78,
    (byte) 12,
    (byte) 240 /*0xF0*/,
    (byte) 2,
    (byte) 30,
    (byte) 38,
    (byte) 98,
    (byte) 122,
    (byte) 89,
    (byte) 3,
    (byte) 78,
    (byte) 149,
    (byte) 51,
    (byte) 24,
    (byte) 44,
    (byte) 78,
    (byte) 29,
    (byte) 136,
    (byte) 37,
    (byte) 242,
    (byte) 157,
    (byte) 145,
    (byte) 41,
    (byte) 162,
    (byte) 86,
    (byte) 171,
    (byte) 46,
    (byte) 115,
    (byte) 227,
    (byte) 198,
    (byte) 143,
    (byte) 13,
    (byte) 91,
    (byte) 37,
    (byte) 233,
    (byte) 46,
    (byte) 195,
    (byte) 18,
    (byte) 180,
    (byte) 111,
    (byte) 20,
    (byte) 210,
    (byte) 19,
    (byte) 142,
    (byte) 254,
    (byte) 33,
    (byte) 163,
    (byte) 64 /*0x40*/,
    (byte) 27,
    (byte) 20,
    (byte) 129,
    (byte) 95,
    (byte) 1,
    (byte) 246,
    (byte) 56,
    (byte) 206,
    (byte) 221,
    (byte) 123,
    (byte) 69,
    (byte) 183,
    (byte) 212,
    (byte) 30,
    (byte) 2,
    (byte) 227,
    (byte) 223,
    (byte) 81,
    (byte) 119,
    (byte) 180,
    (byte) 96 /*0x60*/,
    (byte) 191,
    (byte) 103,
    (byte) 221,
    (byte) 31 /*0x1F*/,
    (byte) 128 /*0x80*/,
    (byte) 181,
    (byte) 128 /*0x80*/,
    (byte) 195,
    (byte) 111,
    (byte) 6,
    (byte) 183,
    (byte) 12,
    (byte) 102,
    (byte) 129,
    (byte) 235,
    (byte) 108,
    (byte) 192 /*0xC0*/,
    byte.MaxValue,
    (byte) 193,
    (byte) 106,
    (byte) 54,
    (byte) 70,
    (byte) 186,
    (byte) 214,
    (byte) 164,
    (byte) 160 /*0xA0*/,
    (byte) 137,
    (byte) 187,
    (byte) 229,
    (byte) 184,
    (byte) 113,
    (byte) 198,
    (byte) 10,
    (byte) 101,
    (byte) 23,
    (byte) 151,
    (byte) 34,
    (byte) 214,
    (byte) 247,
    (byte) 51,
    (byte) 22,
    (byte) 77,
    (byte) 209,
    (byte) 163,
    (byte) 197,
    (byte) 84,
    (byte) 198,
    (byte) 74,
    (byte) 198,
    (byte) 102,
    (byte) 187,
    (byte) 232,
    (byte) 7,
    (byte) 185,
    (byte) 11,
    (byte) 188,
    (byte) 46,
    (byte) 43,
    (byte) 189,
    (byte) 218,
    (byte) 196,
    (byte) 51,
    (byte) 176 /*0xB0*/,
    (byte) 26,
    (byte) 19,
    (byte) 72,
    (byte) 194,
    (byte) 95,
    (byte) 154,
    (byte) 144 /*0x90*/,
    (byte) 131,
    (byte) 165,
    (byte) 133,
    (byte) 96 /*0x60*/,
    (byte) 183,
    (byte) 46,
    (byte) 8,
    (byte) 116,
    (byte) 185,
    (byte) 209,
    (byte) 250,
    (byte) 235,
    (byte) 237,
    (byte) 220,
    (byte) 114,
    (byte) 54,
    (byte) 23,
    (byte) 33,
    (byte) 215,
    (byte) 241,
    (byte) 217,
    (byte) 253,
    (byte) 85,
    (byte) 100,
    (byte) 36,
    (byte) 248,
    (byte) 114,
    (byte) 245,
    (byte) 226,
    (byte) 109,
    (byte) 249,
    (byte) 239
  };

  internal static string ssp_appserver_13394()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[53];
      byte[] numArray2 = new byte[53];
      numArray2[0] = (byte) 129;
      numArray2[1] = (byte) 125;
      numArray2[45] = (byte) 204;
      numArray2[3] = (byte) 142;
      numArray2[25] = (byte) 161;
      numArray2[5] = (byte) 203;
      numArray2[7] = (byte) 25;
      numArray2[8] = (byte) 28;
      numArray2[43] = (byte) 142;
      numArray2[27] = (byte) 191;
      numArray2[48 /*0x30*/] = (byte) 178;
      numArray2[11] = (byte) 168;
      numArray2[30] = (byte) 178;
      numArray2[13] = (byte) 131;
      numArray2[16 /*0x10*/] = (byte) 210;
      numArray2[50] = (byte) 28;
      numArray2[2] = (byte) 30;
      numArray2[34] = (byte) 38;
      numArray2[15] = (byte) 207;
      numArray2[19] = (byte) 54;
      numArray2[20] = (byte) 13;
      numArray2[21] = (byte) 138;
      numArray2[22] = (byte) 86;
      numArray2[14] = (byte) 247;
      numArray2[31 /*0x1F*/] = (byte) 89;
      numArray2[42] = (byte) 168;
      numArray2[35] = (byte) 208 /*0xD0*/;
      numArray2[17] = (byte) 99;
      numArray2[41] = (byte) 203;
      numArray2[29] = (byte) 29;
      numArray2[9] = (byte) 188;
      numArray2[10] = (byte) 231;
      numArray2[23] = (byte) 76;
      numArray2[18] = (byte) 168;
      numArray2[39] = (byte) 248;
      numArray2[4] = (byte) 104;
      numArray2[36] = (byte) 252;
      numArray2[37] = (byte) 84;
      numArray2[38] = (byte) 253;
      numArray2[6] = (byte) 110;
      numArray2[40] = (byte) 131;
      numArray2[28] = (byte) 20;
      numArray2[24] = (byte) 146;
      numArray2[32 /*0x20*/] = (byte) 163;
      numArray2[44] = (byte) 94;
      numArray2[12] = (byte) 215;
      numArray2[46] = (byte) 66;
      numArray2[47] = (byte) 58;
      numArray2[26] = (byte) 151;
      numArray2[33] = (byte) 5;
      numArray2[49] = (byte) 124;
      numArray2[51] = (byte) 253;
      numArray2[52] = (byte) 148;
      byte[] numArray3 = new byte[53]
      {
        (byte) 20,
        (byte) 252,
        (byte) 177,
        (byte) 228,
        (byte) 4,
        (byte) 61,
        (byte) 51,
        (byte) 135,
        (byte) 169,
        (byte) 244,
        (byte) 243,
        (byte) 241,
        (byte) 22,
        (byte) 198,
        (byte) 229,
        (byte) 207,
        (byte) 199,
        (byte) 194,
        (byte) 172,
        (byte) 56,
        (byte) 70,
        (byte) 53,
        (byte) 71,
        (byte) 215,
        (byte) 161,
        (byte) 176 /*0xB0*/,
        (byte) 20,
        (byte) 224 /*0xE0*/,
        (byte) 107,
        (byte) 41,
        (byte) 60,
        (byte) 7,
        (byte) 222,
        (byte) 12,
        (byte) 74,
        (byte) 4,
        (byte) 53,
        (byte) 208 /*0xD0*/,
        (byte) 150,
        (byte) 99,
        (byte) 89,
        (byte) 146,
        (byte) 104,
        (byte) 208 /*0xD0*/,
        (byte) 70,
        (byte) 171,
        (byte) 87,
        (byte) 17,
        (byte) 9,
        (byte) 239,
        (byte) 19,
        (byte) 123,
        (byte) 253
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[53];
    byte[] numArray5 = new byte[53]
    {
      (byte) 253,
      (byte) 20,
      (byte) 94,
      (byte) 227,
      (byte) 53,
      (byte) 104,
      (byte) 214,
      (byte) 162,
      (byte) 109,
      (byte) 254,
      (byte) 84,
      (byte) 201,
      (byte) 10,
      (byte) 12,
      (byte) 45,
      (byte) 124,
      (byte) 167,
      (byte) 35,
      (byte) 47,
      (byte) 235,
      (byte) 222,
      (byte) 123,
      (byte) 57,
      (byte) 67,
      (byte) 29,
      (byte) 254,
      (byte) 238,
      (byte) 127 /*0x7F*/,
      (byte) 127 /*0x7F*/,
      (byte) 238,
      (byte) 97,
      (byte) 30,
      (byte) 224 /*0xE0*/,
      (byte) 4,
      (byte) 199,
      (byte) 26,
      (byte) 75,
      (byte) 71,
      (byte) 35,
      (byte) 205,
      (byte) 238,
      (byte) 49,
      (byte) 89,
      (byte) 169,
      (byte) 48 /*0x30*/,
      (byte) 7,
      (byte) 189,
      (byte) 99,
      (byte) 176 /*0xB0*/,
      (byte) 16 /*0x10*/,
      (byte) 1,
      (byte) 123,
      (byte) 103
    };
    byte[] numArray6 = new byte[53];
    numArray6[1] = (byte) 180;
    numArray6[51] = (byte) 83;
    numArray6[2] = (byte) 64 /*0x40*/;
    numArray6[22] = (byte) 61;
    numArray6[52] = (byte) 239;
    numArray6[3] = (byte) 178;
    numArray6[6] = (byte) 139;
    numArray6[7] = (byte) 71;
    numArray6[14] = (byte) 130;
    numArray6[44] = (byte) 223;
    numArray6[10] = (byte) 5;
    numArray6[11] = (byte) 136;
    numArray6[12] = (byte) 50;
    numArray6[13] = (byte) 20;
    numArray6[0] = (byte) 169;
    numArray6[15] = (byte) 243;
    numArray6[29] = (byte) 165;
    numArray6[43] = (byte) 56;
    numArray6[18] = (byte) 106;
    numArray6[19] = (byte) 84;
    numArray6[20] = (byte) 22;
    numArray6[50] = (byte) 241;
    numArray6[27] = (byte) 217;
    numArray6[26] = (byte) 54;
    numArray6[24] = (byte) 73;
    numArray6[48 /*0x30*/] = (byte) 185;
    numArray6[4] = (byte) 112 /*0x70*/;
    numArray6[46] = (byte) 82;
    numArray6[16 /*0x10*/] = (byte) 60;
    numArray6[25] = (byte) 216;
    numArray6[31 /*0x1F*/] = (byte) 52;
    numArray6[39] = (byte) 166;
    numArray6[32 /*0x20*/] = (byte) 55;
    numArray6[33] = (byte) 55;
    numArray6[34] = (byte) 193;
    numArray6[9] = (byte) 179;
    numArray6[37] = (byte) 123;
    numArray6[17] = (byte) 137;
    numArray6[38] = (byte) 119;
    numArray6[36] = (byte) 9;
    numArray6[40] = (byte) 142;
    numArray6[8] = (byte) 183;
    numArray6[42] = (byte) 29;
    numArray6[30] = (byte) 164;
    numArray6[41] = (byte) 14;
    numArray6[23] = (byte) 126;
    numArray6[21] = (byte) 98;
    numArray6[47] = (byte) 35;
    numArray6[35] = (byte) 20;
    numArray6[49] = (byte) 32 /*0x20*/;
    numArray6[5] = (byte) 149;
    numArray6[28] = (byte) 197;
    numArray6[45] = (byte) 229;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 53);
    for (int index = 0; index < 53; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13395(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[31 /*0x1F*/] = (byte) 238;
    sourceArray1[1] = (byte) 8;
    sourceArray1[2] = (byte) 174;
    sourceArray1[37] = (byte) 246;
    sourceArray1[0] = (byte) 75;
    sourceArray1[14] = (byte) 0;
    sourceArray1[4] = (byte) 240 /*0xF0*/;
    sourceArray1[19] = (byte) 157;
    sourceArray1[8] = (byte) 183;
    sourceArray1[11] = (byte) 23;
    sourceArray1[44] = (byte) 31 /*0x1F*/;
    sourceArray1[23] = (byte) 248;
    sourceArray1[12] = (byte) 254;
    sourceArray1[26] = (byte) 48 /*0x30*/;
    sourceArray1[46] = (byte) 32 /*0x20*/;
    sourceArray1[15] = (byte) 221;
    sourceArray1[16 /*0x10*/] = (byte) 19;
    sourceArray1[30] = (byte) 177;
    sourceArray1[42] = (byte) 84;
    sourceArray1[43] = (byte) 122;
    sourceArray1[6] = (byte) 178;
    sourceArray1[28] = (byte) 75;
    sourceArray1[3] = (byte) 246;
    sourceArray1[21] = (byte) 131;
    sourceArray1[24] = (byte) 136;
    sourceArray1[35] = (byte) 40;
    sourceArray1[9] = (byte) 127 /*0x7F*/;
    sourceArray1[27] = (byte) 143;
    sourceArray1[10] = byte.MaxValue;
    sourceArray1[17] = (byte) 15;
    sourceArray1[7] = (byte) 145;
    sourceArray1[22] = (byte) 24;
    sourceArray1[32 /*0x20*/] = (byte) 132;
    sourceArray1[33] = (byte) 229;
    sourceArray1[34] = (byte) 244;
    sourceArray1[40] = (byte) 92;
    sourceArray1[36] = (byte) 211;
    sourceArray1[29] = (byte) 106;
    sourceArray1[38] = (byte) 98;
    sourceArray1[39] = (byte) 14;
    sourceArray1[45] = (byte) 160 /*0xA0*/;
    sourceArray1[41] = (byte) 73;
    sourceArray1[25] = (byte) 106;
    sourceArray1[18] = (byte) 102;
    sourceArray1[20] = (byte) 71;
    sourceArray1[5] = (byte) 136;
    sourceArray1[13] = (byte) 210;
    sourceArray1[47] = (byte) 203;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 29,
      (byte) 0,
      (byte) 177,
      (byte) 217,
      (byte) 11,
      (byte) 33,
      (byte) 154,
      (byte) 197,
      (byte) 236,
      (byte) 139,
      (byte) 58,
      (byte) 102,
      (byte) 201,
      (byte) 235,
      (byte) 210,
      (byte) 65,
      (byte) 36,
      (byte) 5,
      (byte) 247,
      (byte) 143,
      (byte) 155,
      (byte) 222,
      (byte) 223,
      (byte) 69,
      (byte) 250,
      (byte) 28,
      (byte) 69,
      (byte) 236,
      (byte) 118,
      (byte) 85,
      (byte) 30,
      (byte) 35,
      (byte) 139,
      (byte) 139,
      (byte) 43,
      (byte) 99,
      (byte) 3,
      (byte) 132,
      (byte) 211,
      (byte) 230,
      (byte) 67,
      (byte) 121,
      (byte) 47,
      (byte) 239,
      (byte) 89,
      (byte) 79,
      (byte) 124,
      (byte) 8
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[50];
    byte[] response2 = new byte[50];
    Array.Copy((Array) sc_13393.sspq, 0, (Array) numArray2, 0, 50);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13393.sspr, 0, (Array) numArray2, 0, 50);
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

  internal static string ssp_appserver_13396()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[46];
      byte[] numArray2 = new byte[46]
      {
        (byte) 66,
        (byte) 187,
        (byte) 154,
        (byte) 87,
        (byte) 52,
        (byte) 186,
        (byte) 59,
        (byte) 105,
        (byte) 206,
        (byte) 184,
        (byte) 119,
        (byte) 151,
        (byte) 131,
        (byte) 32 /*0x20*/,
        (byte) 113,
        (byte) 96 /*0x60*/,
        (byte) 18,
        (byte) 171,
        (byte) 249,
        (byte) 125,
        (byte) 203,
        (byte) 243,
        (byte) 149,
        (byte) 160 /*0xA0*/,
        (byte) 149,
        (byte) 172,
        (byte) 155,
        (byte) 30,
        (byte) 182,
        (byte) 172,
        (byte) 143,
        (byte) 49,
        (byte) 224 /*0xE0*/,
        (byte) 65,
        (byte) 184,
        (byte) 108,
        (byte) 86,
        (byte) 115,
        (byte) 164,
        (byte) 135,
        (byte) 58,
        (byte) 61,
        (byte) 254,
        (byte) 253,
        (byte) 251,
        (byte) 136
      };
      byte[] numArray3 = new byte[46]
      {
        (byte) 144 /*0x90*/,
        (byte) 68,
        (byte) 115,
        (byte) 82,
        (byte) 45,
        (byte) 227,
        (byte) 11,
        (byte) 153,
        (byte) 170,
        (byte) 50,
        (byte) 215,
        (byte) 72,
        (byte) 114,
        (byte) 197,
        (byte) 94,
        (byte) 249,
        (byte) 140,
        (byte) 63 /*0x3F*/,
        (byte) 102,
        (byte) 88,
        (byte) 61,
        (byte) 162,
        (byte) 197,
        (byte) 186,
        (byte) 73,
        (byte) 209,
        (byte) 199,
        (byte) 76,
        (byte) 121,
        (byte) 159,
        (byte) 223,
        (byte) 211,
        (byte) 237,
        (byte) 126,
        (byte) 238,
        (byte) 165,
        (byte) 96 /*0x60*/,
        (byte) 131,
        (byte) 83,
        (byte) 45,
        (byte) 82,
        (byte) 176 /*0xB0*/,
        (byte) 105,
        (byte) 63 /*0x3F*/,
        (byte) 220,
        (byte) 141
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[46];
    byte[] numArray5 = new byte[46]
    {
      (byte) 135,
      (byte) 45,
      (byte) 87,
      (byte) 166,
      (byte) 68,
      (byte) 250,
      (byte) 79,
      (byte) 38,
      (byte) 249,
      (byte) 194,
      (byte) 136,
      (byte) 31 /*0x1F*/,
      (byte) 29,
      (byte) 179,
      (byte) 33,
      (byte) 41,
      (byte) 70,
      (byte) 183,
      (byte) 66,
      (byte) 103,
      (byte) 5,
      (byte) 9,
      (byte) 159,
      (byte) 7,
      (byte) 114,
      (byte) 66,
      (byte) 142,
      (byte) 245,
      (byte) 151,
      (byte) 164,
      (byte) 12,
      (byte) 2,
      (byte) 10,
      (byte) 252,
      (byte) 47,
      (byte) 127 /*0x7F*/,
      (byte) 55,
      (byte) 138,
      (byte) 156,
      (byte) 70,
      (byte) 105,
      (byte) 69,
      (byte) 92,
      (byte) 176 /*0xB0*/,
      (byte) 28,
      (byte) 104
    };
    byte[] numArray6 = new byte[46]
    {
      (byte) 7,
      (byte) 110,
      (byte) 63 /*0x3F*/,
      (byte) 63 /*0x3F*/,
      (byte) 26,
      (byte) 163,
      (byte) 240 /*0xF0*/,
      (byte) 225,
      (byte) 249,
      (byte) 176 /*0xB0*/,
      (byte) 64 /*0x40*/,
      (byte) 218,
      (byte) 21,
      (byte) 218,
      (byte) 197,
      (byte) 38,
      (byte) 84,
      (byte) 37,
      (byte) 24,
      (byte) 40,
      (byte) 9,
      (byte) 23,
      (byte) 5,
      (byte) 179,
      (byte) 98,
      (byte) 158,
      (byte) 165,
      (byte) 160 /*0xA0*/,
      (byte) 130,
      (byte) 164,
      (byte) 222,
      (byte) 183,
      (byte) 136,
      (byte) 228,
      (byte) 71,
      (byte) 33,
      (byte) 238,
      (byte) 80 /*0x50*/,
      (byte) 86,
      (byte) 36,
      (byte) 37,
      (byte) 203,
      (byte) 139,
      (byte) 84,
      (byte) 55,
      (byte) 137
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 46);
    for (int index = 0; index < 46; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[45];
    byte[] response = new byte[45];
    Array.Copy((Array) sc_13393.sspq, 50, (Array) numArray7, 0, 45);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13393.sspr, 50, (Array) numArray7, 0, 45);
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

  internal static string ssp_appserver_13397()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 182,
        (byte) 7,
        (byte) 251,
        (byte) 151,
        (byte) 83,
        (byte) 252,
        (byte) 5,
        (byte) 58,
        (byte) 227,
        (byte) 249,
        (byte) 182,
        (byte) 106,
        (byte) 42,
        (byte) 92,
        (byte) 208 /*0xD0*/,
        (byte) 160 /*0xA0*/,
        (byte) 102,
        (byte) 41,
        (byte) 221,
        (byte) 104,
        (byte) 65,
        (byte) 204,
        (byte) 101
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 176 /*0xB0*/,
        (byte) 73,
        (byte) 55,
        (byte) 252,
        (byte) 136,
        (byte) 236,
        (byte) 103,
        (byte) 227,
        (byte) 19,
        (byte) 3,
        (byte) 138,
        (byte) 111,
        (byte) 216,
        (byte) 136,
        (byte) 24,
        (byte) 157,
        (byte) 233,
        (byte) 27,
        (byte) 223,
        (byte) 143,
        (byte) 226,
        (byte) 197,
        (byte) 107
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30];
      byte[] response = new byte[30];
      Array.Copy((Array) sc_13393.sspq, 95, (Array) numArray4, 0, 30);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 95, (Array) numArray4, 0, 30);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23];
    numArray6[17] = (byte) 106;
    numArray6[1] = (byte) 174;
    numArray6[2] = (byte) 31 /*0x1F*/;
    numArray6[3] = (byte) 188;
    numArray6[4] = (byte) 51;
    numArray6[7] = (byte) 148;
    numArray6[20] = (byte) 110;
    numArray6[6] = (byte) 216;
    numArray6[15] = (byte) 114;
    numArray6[9] = (byte) 73;
    numArray6[13] = (byte) 172;
    numArray6[19] = (byte) 236;
    numArray6[5] = (byte) 137;
    numArray6[10] = (byte) 59;
    numArray6[14] = (byte) 19;
    numArray6[21] = (byte) 244;
    numArray6[16 /*0x10*/] = (byte) 181;
    numArray6[12] = (byte) 43;
    numArray6[18] = (byte) 99;
    numArray6[8] = (byte) 52;
    numArray6[0] = (byte) 128 /*0x80*/;
    numArray6[11] = (byte) 84;
    numArray6[22] = (byte) 183;
    byte[] numArray7 = new byte[23];
    numArray7[1] = (byte) 12;
    numArray7[3] = (byte) 212;
    numArray7[18] = (byte) 132;
    numArray7[7] = (byte) 192 /*0xC0*/;
    numArray7[9] = (byte) 235;
    numArray7[5] = (byte) 142;
    numArray7[6] = (byte) 163;
    numArray7[4] = (byte) 184;
    numArray7[13] = (byte) 190;
    numArray7[14] = (byte) 249;
    numArray7[10] = (byte) 237;
    numArray7[12] = (byte) 159;
    numArray7[20] = (byte) 18;
    numArray7[0] = (byte) 171;
    numArray7[2] = (byte) 23;
    numArray7[15] = (byte) 72;
    numArray7[16 /*0x10*/] = (byte) 207;
    numArray7[17] = (byte) 177;
    numArray7[11] = (byte) 46;
    numArray7[19] = (byte) 8;
    numArray7[8] = (byte) 245;
    numArray7[21] = (byte) 51;
    numArray7[22] = (byte) 101;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[25];
    byte[] response1 = new byte[25];
    Array.Copy((Array) sc_13393.sspq, 125, (Array) numArray8, 0, 25);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13393.sspr, 125, (Array) numArray8, 0, 25);
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

  internal static int ssp_appserver_13398(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[25] = (byte) 229;
    sourceArray1[4] = (byte) 223;
    sourceArray1[2] = (byte) 105;
    sourceArray1[18] = (byte) 176 /*0xB0*/;
    sourceArray1[15] = (byte) 107;
    sourceArray1[26] = (byte) 221;
    sourceArray1[1] = (byte) 141;
    sourceArray1[7] = (byte) 89;
    sourceArray1[31 /*0x1F*/] = (byte) 195;
    sourceArray1[44] = (byte) 254;
    sourceArray1[10] = (byte) 231;
    sourceArray1[23] = (byte) 84;
    sourceArray1[12] = (byte) 159;
    sourceArray1[11] = (byte) 38;
    sourceArray1[16 /*0x10*/] = (byte) 48 /*0x30*/;
    sourceArray1[0] = (byte) 249;
    sourceArray1[43] = (byte) 165;
    sourceArray1[17] = (byte) 223;
    sourceArray1[47] = (byte) 250;
    sourceArray1[29] = (byte) 46;
    sourceArray1[20] = (byte) 96 /*0x60*/;
    sourceArray1[14] = (byte) 6;
    sourceArray1[22] = (byte) 90;
    sourceArray1[42] = (byte) 8;
    sourceArray1[24] = (byte) 193;
    sourceArray1[19] = (byte) 240 /*0xF0*/;
    sourceArray1[3] = (byte) 253;
    sourceArray1[27] = (byte) 22;
    sourceArray1[28] = (byte) 227;
    sourceArray1[13] = (byte) 113;
    sourceArray1[9] = (byte) 40;
    sourceArray1[45] = (byte) 135;
    sourceArray1[32 /*0x20*/] = (byte) 129;
    sourceArray1[33] = (byte) 161;
    sourceArray1[34] = (byte) 148;
    sourceArray1[35] = (byte) 174;
    sourceArray1[36] = (byte) 83;
    sourceArray1[37] = (byte) 32 /*0x20*/;
    sourceArray1[38] = (byte) 157;
    sourceArray1[39] = (byte) 2;
    sourceArray1[8] = (byte) 23;
    sourceArray1[46] = (byte) 86;
    sourceArray1[5] = (byte) 188;
    sourceArray1[40] = (byte) 64 /*0x40*/;
    sourceArray1[30] = (byte) 5;
    sourceArray1[21] = (byte) 21;
    sourceArray1[6] = (byte) 104;
    sourceArray1[41] = (byte) 94;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[35] = (byte) 7;
    sourceArray2[31 /*0x1F*/] = (byte) 222;
    sourceArray2[2] = (byte) 66;
    sourceArray2[3] = (byte) 74;
    sourceArray2[4] = (byte) 21;
    sourceArray2[1] = (byte) 239;
    sourceArray2[6] = (byte) 139;
    sourceArray2[7] = (byte) 192 /*0xC0*/;
    sourceArray2[9] = (byte) 204;
    sourceArray2[10] = (byte) 97;
    sourceArray2[14] = (byte) 150;
    sourceArray2[11] = (byte) 76;
    sourceArray2[32 /*0x20*/] = (byte) 156;
    sourceArray2[17] = (byte) 8;
    sourceArray2[25] = (byte) 202;
    sourceArray2[15] = (byte) 61;
    sourceArray2[16 /*0x10*/] = (byte) 62;
    sourceArray2[44] = (byte) 57;
    sourceArray2[19] = (byte) 24;
    sourceArray2[36] = (byte) 33;
    sourceArray2[43] = (byte) 9;
    sourceArray2[12] = (byte) 185;
    sourceArray2[22] = (byte) 120;
    sourceArray2[23] = (byte) 77;
    sourceArray2[24] = (byte) 200;
    sourceArray2[39] = (byte) 178;
    sourceArray2[47] = (byte) 0;
    sourceArray2[27] = (byte) 202;
    sourceArray2[0] = (byte) 224 /*0xE0*/;
    sourceArray2[29] = (byte) 29;
    sourceArray2[21] = (byte) 27;
    sourceArray2[41] = (byte) 162;
    sourceArray2[13] = (byte) 250;
    sourceArray2[33] = (byte) 191;
    sourceArray2[46] = (byte) 114;
    sourceArray2[34] = (byte) 54;
    sourceArray2[28] = (byte) 232;
    sourceArray2[37] = (byte) 131;
    sourceArray2[38] = (byte) 109;
    sourceArray2[30] = (byte) 131;
    sourceArray2[40] = (byte) 152;
    sourceArray2[8] = (byte) 108;
    sourceArray2[42] = (byte) 135;
    sourceArray2[5] = (byte) 15;
    sourceArray2[18] = (byte) 190;
    sourceArray2[45] = (byte) 86;
    sourceArray2[26] = (byte) 156;
    sourceArray2[20] = (byte) 243;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[48 /*0x30*/];
    byte[] response2 = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_13393.sspq, 150, (Array) numArray2, 0, 48 /*0x30*/);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13393.sspr, 150, (Array) numArray2, 0, 48 /*0x30*/);
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

  internal static string ssp_appserver_13399()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 137,
        (byte) 189,
        (byte) 241,
        (byte) 176 /*0xB0*/,
        (byte) 60,
        (byte) 60,
        (byte) 2,
        (byte) 241,
        (byte) 219,
        (byte) 190,
        (byte) 205,
        (byte) 246,
        (byte) 199,
        (byte) 84,
        (byte) 63 /*0x3F*/,
        (byte) 156,
        (byte) 108,
        (byte) 119,
        (byte) 6,
        (byte) 222,
        (byte) 147,
        (byte) 90,
        (byte) 96 /*0x60*/,
        (byte) 217,
        (byte) 89,
        (byte) 15,
        (byte) 176 /*0xB0*/,
        (byte) 250,
        (byte) 94,
        (byte) 56,
        (byte) 44,
        (byte) 44,
        (byte) 147,
        (byte) 103,
        (byte) 130,
        (byte) 253,
        (byte) 97,
        (byte) 48 /*0x30*/,
        (byte) 28,
        (byte) 9,
        (byte) 101,
        (byte) 114,
        (byte) 181
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 58,
        (byte) 247,
        (byte) 96 /*0x60*/,
        (byte) 160 /*0xA0*/,
        (byte) 76,
        (byte) 69,
        (byte) 142,
        (byte) 122,
        (byte) 115,
        (byte) 12,
        (byte) 164,
        (byte) 140,
        (byte) 254,
        (byte) 138,
        (byte) 120,
        (byte) 152,
        (byte) 212,
        (byte) 172,
        (byte) 140,
        (byte) 149,
        (byte) 166,
        (byte) 81,
        (byte) 185,
        (byte) 212,
        (byte) 223,
        (byte) 227,
        (byte) 200,
        (byte) 227,
        (byte) 210,
        (byte) 213,
        (byte) 193,
        (byte) 187,
        (byte) 33,
        (byte) 40,
        (byte) 181,
        (byte) 166,
        (byte) 141,
        (byte) 55,
        (byte) 45,
        byte.MaxValue,
        (byte) 159,
        (byte) 110,
        (byte) 102
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43];
    numArray5[5] = (byte) 51;
    numArray5[1] = (byte) 164;
    numArray5[2] = (byte) 250;
    numArray5[12] = (byte) 87;
    numArray5[4] = (byte) 175;
    numArray5[32 /*0x20*/] = (byte) 152;
    numArray5[6] = (byte) 221;
    numArray5[15] = (byte) 7;
    numArray5[41] = (byte) 188;
    numArray5[9] = (byte) 46;
    numArray5[10] = (byte) 93;
    numArray5[11] = (byte) 23;
    numArray5[30] = (byte) 164;
    numArray5[17] = (byte) 163;
    numArray5[14] = (byte) 140;
    numArray5[20] = (byte) 98;
    numArray5[27] = (byte) 253;
    numArray5[39] = (byte) 248;
    numArray5[18] = (byte) 146;
    numArray5[19] = (byte) 160 /*0xA0*/;
    numArray5[34] = (byte) 164;
    numArray5[21] = (byte) 96 /*0x60*/;
    numArray5[7] = (byte) 214;
    numArray5[23] = (byte) 66;
    numArray5[24] = (byte) 59;
    numArray5[42] = (byte) 225;
    numArray5[26] = (byte) 90;
    numArray5[35] = (byte) 11;
    numArray5[28] = (byte) 121;
    numArray5[29] = (byte) 32 /*0x20*/;
    numArray5[16 /*0x10*/] = (byte) 146;
    numArray5[31 /*0x1F*/] = (byte) 155;
    numArray5[8] = (byte) 134;
    numArray5[22] = (byte) 209;
    numArray5[0] = (byte) 107;
    numArray5[33] = (byte) 92;
    numArray5[36] = byte.MaxValue;
    numArray5[3] = (byte) 58;
    numArray5[38] = (byte) 136;
    numArray5[25] = (byte) 60;
    numArray5[40] = (byte) 79;
    numArray5[37] = (byte) 35;
    numArray5[13] = (byte) 45;
    byte[] numArray6 = new byte[43]
    {
      (byte) 96 /*0x60*/,
      (byte) 13,
      (byte) 178,
      (byte) 119,
      (byte) 44,
      (byte) 28,
      (byte) 168,
      (byte) 15,
      (byte) 228,
      (byte) 251,
      (byte) 146,
      (byte) 153,
      (byte) 98,
      (byte) 222,
      (byte) 49,
      (byte) 78,
      (byte) 104,
      (byte) 187,
      (byte) 177,
      (byte) 156,
      (byte) 141,
      (byte) 74,
      (byte) 224 /*0xE0*/,
      (byte) 76,
      (byte) 208 /*0xD0*/,
      (byte) 197,
      (byte) 165,
      (byte) 35,
      (byte) 42,
      (byte) 233,
      (byte) 131,
      (byte) 185,
      (byte) 91,
      (byte) 162,
      (byte) 112 /*0x70*/,
      (byte) 127 /*0x7F*/,
      (byte) 98,
      (byte) 10,
      (byte) 210,
      byte.MaxValue,
      (byte) 96 /*0x60*/,
      (byte) 58,
      (byte) 164
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13400()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[14] = (byte) 86;
      numArray2[0] = (byte) 57;
      numArray2[19] = (byte) 244;
      numArray2[20] = (byte) 139;
      numArray2[3] = (byte) 21;
      numArray2[5] = (byte) 111;
      numArray2[6] = (byte) 94;
      numArray2[7] = (byte) 123;
      numArray2[1] = (byte) 71;
      numArray2[8] = (byte) 201;
      numArray2[12] = (byte) 102;
      numArray2[11] = (byte) 222;
      numArray2[18] = (byte) 79;
      numArray2[22] = (byte) 21;
      numArray2[21] = (byte) 85;
      numArray2[15] = (byte) 113;
      numArray2[9] = (byte) 221;
      numArray2[17] = (byte) 152;
      numArray2[10] = (byte) 202;
      numArray2[13] = (byte) 251;
      numArray2[16 /*0x10*/] = (byte) 248;
      numArray2[4] = (byte) 31 /*0x1F*/;
      numArray2[2] = (byte) 36;
      byte[] numArray3 = new byte[23];
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[12] = (byte) 202;
      numArray3[15] = (byte) 191;
      numArray3[1] = (byte) 72;
      numArray3[2] = (byte) 53;
      numArray3[5] = (byte) 28;
      numArray3[13] = (byte) 157;
      numArray3[4] = (byte) 220;
      numArray3[6] = (byte) 39;
      numArray3[9] = (byte) 236;
      numArray3[10] = (byte) 199;
      numArray3[3] = (byte) 48 /*0x30*/;
      numArray3[18] = (byte) 40;
      numArray3[21] = (byte) 228;
      numArray3[14] = (byte) 90;
      numArray3[7] = (byte) 192 /*0xC0*/;
      numArray3[16 /*0x10*/] = (byte) 55;
      numArray3[22] = (byte) 185;
      numArray3[8] = (byte) 104;
      numArray3[17] = (byte) 190;
      numArray3[20] = (byte) 41;
      numArray3[11] = (byte) 9;
      numArray3[19] = (byte) 174;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 30,
      (byte) 109,
      (byte) 174,
      (byte) 136,
      (byte) 162,
      (byte) 80 /*0x50*/,
      (byte) 230,
      (byte) 139,
      (byte) 103,
      (byte) 218,
      (byte) 125,
      (byte) 87,
      (byte) 94,
      (byte) 137,
      (byte) 224 /*0xE0*/,
      (byte) 108,
      (byte) 226,
      (byte) 100,
      (byte) 141,
      (byte) 51,
      byte.MaxValue,
      (byte) 200,
      (byte) 210
    };
    byte[] numArray6 = new byte[23];
    numArray6[15] = (byte) 168;
    numArray6[1] = (byte) 122;
    numArray6[12] = (byte) 192 /*0xC0*/;
    numArray6[3] = (byte) 54;
    numArray6[4] = (byte) 32 /*0x20*/;
    numArray6[20] = (byte) 5;
    numArray6[17] = (byte) 198;
    numArray6[9] = (byte) 234;
    numArray6[8] = (byte) 87;
    numArray6[2] = (byte) 22;
    numArray6[13] = byte.MaxValue;
    numArray6[11] = (byte) 83;
    numArray6[10] = (byte) 191;
    numArray6[6] = (byte) 135;
    numArray6[18] = (byte) 96 /*0x60*/;
    numArray6[14] = (byte) 119;
    numArray6[16 /*0x10*/] = (byte) 232;
    numArray6[7] = (byte) 169;
    numArray6[22] = (byte) 133;
    numArray6[19] = (byte) 95;
    numArray6[0] = (byte) 64 /*0x40*/;
    numArray6[21] = (byte) 101;
    numArray6[5] = (byte) 90;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13401()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 148,
        (byte) 80 /*0x50*/,
        (byte) 8,
        (byte) 244,
        (byte) 243,
        (byte) 85,
        (byte) 155,
        (byte) 131,
        (byte) 218,
        (byte) 192 /*0xC0*/,
        (byte) 16 /*0x10*/,
        (byte) 0,
        (byte) 171,
        (byte) 215,
        (byte) 9,
        (byte) 79
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 45,
        (byte) 79,
        (byte) 137,
        (byte) 187,
        (byte) 18,
        (byte) 205,
        (byte) 250,
        (byte) 226,
        (byte) 17,
        (byte) 34,
        (byte) 240 /*0xF0*/,
        (byte) 203,
        (byte) 71,
        (byte) 223,
        (byte) 35,
        (byte) 214
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
      (byte) 166,
      (byte) 77,
      (byte) 93,
      (byte) 34,
      (byte) 172,
      (byte) 3,
      (byte) 133,
      (byte) 125,
      (byte) 156,
      (byte) 139,
      (byte) 175,
      (byte) 109,
      (byte) 68,
      (byte) 132,
      (byte) 167,
      (byte) 251
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 41,
      (byte) 6,
      (byte) 74,
      (byte) 101,
      (byte) 45,
      (byte) 3,
      (byte) 59,
      (byte) 181,
      (byte) 17,
      (byte) 174,
      (byte) 251,
      (byte) 184,
      (byte) 170,
      (byte) 10,
      (byte) 99,
      (byte) 31 /*0x1F*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13402()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41];
      numArray2[29] = (byte) 220;
      numArray2[20] = (byte) 110;
      numArray2[2] = (byte) 154;
      numArray2[3] = (byte) 169;
      numArray2[21] = (byte) 216;
      numArray2[26] = (byte) 203;
      numArray2[28] = (byte) 4;
      numArray2[31 /*0x1F*/] = (byte) 42;
      numArray2[8] = (byte) 115;
      numArray2[9] = (byte) 43;
      numArray2[34] = (byte) 122;
      numArray2[35] = (byte) 8;
      numArray2[12] = (byte) 187;
      numArray2[13] = (byte) 36;
      numArray2[4] = (byte) 128 /*0x80*/;
      numArray2[15] = (byte) 234;
      numArray2[16 /*0x10*/] = (byte) 54;
      numArray2[25] = (byte) 82;
      numArray2[18] = (byte) 92;
      numArray2[19] = (byte) 241;
      numArray2[17] = (byte) 45;
      numArray2[33] = (byte) 5;
      numArray2[22] = (byte) 254;
      numArray2[5] = (byte) 209;
      numArray2[23] = (byte) 248;
      numArray2[10] = (byte) 79;
      numArray2[32 /*0x20*/] = byte.MaxValue;
      numArray2[27] = (byte) 124;
      numArray2[24] = (byte) 43;
      numArray2[7] = (byte) 206;
      numArray2[30] = (byte) 137;
      numArray2[38] = (byte) 161;
      numArray2[1] = (byte) 158;
      numArray2[0] = (byte) 92;
      numArray2[39] = (byte) 219;
      numArray2[36] = (byte) 179;
      numArray2[11] = (byte) 76;
      numArray2[14] = (byte) 186;
      numArray2[37] = (byte) 179;
      numArray2[40] = (byte) 211;
      numArray2[6] = (byte) 47;
      byte[] numArray3 = new byte[41];
      numArray3[25] = (byte) 170;
      numArray3[37] = (byte) 1;
      numArray3[26] = (byte) 106;
      numArray3[2] = (byte) 217;
      numArray3[4] = (byte) 116;
      numArray3[40] = (byte) 115;
      numArray3[6] = (byte) 158;
      numArray3[7] = (byte) 149;
      numArray3[32 /*0x20*/] = (byte) 46;
      numArray3[8] = (byte) 200;
      numArray3[20] = (byte) 30;
      numArray3[39] = (byte) 110;
      numArray3[12] = (byte) 80 /*0x50*/;
      numArray3[13] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 48 /*0x30*/;
      numArray3[28] = (byte) 219;
      numArray3[16 /*0x10*/] = (byte) 38;
      numArray3[17] = (byte) 66;
      numArray3[18] = (byte) 247;
      numArray3[34] = (byte) 0;
      numArray3[38] = (byte) 44;
      numArray3[21] = (byte) 204;
      numArray3[22] = (byte) 30;
      numArray3[23] = (byte) 43;
      numArray3[10] = (byte) 25;
      numArray3[1] = (byte) 76;
      numArray3[19] = (byte) 253;
      numArray3[36] = (byte) 106;
      numArray3[9] = (byte) 231;
      numArray3[29] = (byte) 55;
      numArray3[24] = (byte) 120;
      numArray3[0] = (byte) 96 /*0x60*/;
      numArray3[15] = (byte) 101;
      numArray3[5] = (byte) 227;
      numArray3[33] = (byte) 193;
      numArray3[35] = (byte) 48 /*0x30*/;
      numArray3[27] = (byte) 151;
      numArray3[3] = (byte) 252;
      numArray3[30] = (byte) 228;
      numArray3[31 /*0x1F*/] = (byte) 109;
      numArray3[11] = (byte) 34;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 77,
      (byte) 23,
      (byte) 170,
      (byte) 254,
      (byte) 203,
      (byte) 91,
      (byte) 189,
      (byte) 165,
      (byte) 180,
      (byte) 165,
      (byte) 53,
      (byte) 156,
      (byte) 195,
      (byte) 213,
      (byte) 112 /*0x70*/,
      (byte) 24,
      (byte) 155,
      (byte) 52,
      (byte) 67,
      (byte) 86,
      (byte) 42,
      (byte) 127 /*0x7F*/,
      (byte) 231,
      (byte) 124,
      (byte) 32 /*0x20*/,
      (byte) 105,
      (byte) 244,
      (byte) 101,
      (byte) 82,
      (byte) 5,
      (byte) 6,
      (byte) 196,
      (byte) 205,
      (byte) 93,
      (byte) 244,
      (byte) 175,
      (byte) 158,
      (byte) 164,
      (byte) 193,
      (byte) 88,
      (byte) 85
    };
    byte[] numArray6 = new byte[41]
    {
      (byte) 165,
      (byte) 68,
      (byte) 60,
      (byte) 43,
      (byte) 231,
      (byte) 246,
      (byte) 36,
      (byte) 185,
      (byte) 113,
      (byte) 210,
      (byte) 107,
      (byte) 223,
      (byte) 229,
      (byte) 33,
      (byte) 63 /*0x3F*/,
      (byte) 119,
      (byte) 150,
      (byte) 101,
      (byte) 4,
      (byte) 242,
      (byte) 95,
      (byte) 52,
      (byte) 126,
      (byte) 158,
      (byte) 80 /*0x50*/,
      (byte) 248,
      (byte) 65,
      (byte) 108,
      (byte) 244,
      (byte) 16 /*0x10*/,
      (byte) 83,
      (byte) 193,
      (byte) 156,
      (byte) 117,
      (byte) 123,
      (byte) 226,
      (byte) 177,
      (byte) 163,
      (byte) 165,
      (byte) 93,
      (byte) 221
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13403()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[4] = (byte) 171;
      numArray2[9] = (byte) 252;
      numArray2[21] = (byte) 207;
      numArray2[3] = (byte) 250;
      numArray2[2] = (byte) 169;
      numArray2[18] = (byte) 174;
      numArray2[6] = (byte) 192 /*0xC0*/;
      numArray2[7] = (byte) 110;
      numArray2[17] = (byte) 82;
      numArray2[15] = (byte) 103;
      numArray2[10] = (byte) 206;
      numArray2[0] = (byte) 99;
      numArray2[12] = (byte) 45;
      numArray2[13] = (byte) 221;
      numArray2[8] = (byte) 243;
      numArray2[5] = (byte) 115;
      numArray2[11] = (byte) 48 /*0x30*/;
      numArray2[16 /*0x10*/] = (byte) 88;
      numArray2[14] = (byte) 97;
      numArray2[19] = (byte) 158;
      numArray2[20] = (byte) 113;
      numArray2[1] = (byte) 191;
      numArray2[22] = (byte) 102;
      byte[] numArray3 = new byte[23]
      {
        (byte) 27,
        (byte) 250,
        (byte) 17,
        (byte) 19,
        (byte) 146,
        (byte) 156,
        (byte) 80 /*0x50*/,
        (byte) 143,
        (byte) 66,
        (byte) 198,
        (byte) 75,
        (byte) 40,
        (byte) 8,
        (byte) 104,
        (byte) 217,
        (byte) 137,
        (byte) 83,
        (byte) 124,
        (byte) 66,
        (byte) 44,
        (byte) 17,
        (byte) 140,
        (byte) 11
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 240 /*0xF0*/,
      (byte) 253,
      (byte) 237,
      (byte) 82,
      (byte) 73,
      (byte) 178,
      (byte) 147,
      (byte) 88,
      (byte) 200,
      (byte) 57,
      (byte) 48 /*0x30*/,
      (byte) 125,
      (byte) 77,
      (byte) 131,
      (byte) 242,
      (byte) 18,
      (byte) 80 /*0x50*/,
      (byte) 49,
      (byte) 212,
      (byte) 32 /*0x20*/,
      (byte) 183,
      (byte) 28,
      (byte) 237
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 175,
      (byte) 25,
      (byte) 147,
      (byte) 79,
      (byte) 35,
      (byte) 83,
      (byte) 100,
      (byte) 102,
      (byte) 204,
      (byte) 44,
      (byte) 75,
      (byte) 54,
      (byte) 214,
      (byte) 191,
      (byte) 8,
      (byte) 201,
      (byte) 228,
      (byte) 26,
      (byte) 86,
      (byte) 108,
      (byte) 244,
      (byte) 12,
      (byte) 203
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13404()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 245,
        (byte) 5,
        (byte) 186,
        (byte) 67,
        (byte) 10,
        (byte) 47,
        (byte) 19,
        (byte) 250,
        (byte) 4,
        (byte) 252,
        (byte) 43,
        (byte) 181,
        (byte) 166,
        (byte) 41,
        (byte) 81,
        (byte) 130
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 243,
        (byte) 169,
        (byte) 140,
        (byte) 209,
        (byte) 242,
        (byte) 237,
        (byte) 137,
        (byte) 52,
        (byte) 233,
        (byte) 83,
        (byte) 78,
        (byte) 159,
        (byte) 202,
        (byte) 1,
        (byte) 194,
        (byte) 105
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
      (byte) 249,
      (byte) 210,
      (byte) 231,
      (byte) 77,
      (byte) 213,
      (byte) 76,
      (byte) 155,
      (byte) 166,
      (byte) 168,
      (byte) 212,
      (byte) 15,
      (byte) 66,
      (byte) 53,
      (byte) 241,
      (byte) 150,
      (byte) 157
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[0] = (byte) 82;
    numArray6[1] = (byte) 168;
    numArray6[10] = (byte) 120;
    numArray6[4] = (byte) 80 /*0x50*/;
    numArray6[3] = (byte) 27;
    numArray6[5] = (byte) 129;
    numArray6[6] = (byte) 199;
    numArray6[14] = (byte) 173;
    numArray6[8] = (byte) 189;
    numArray6[7] = (byte) 134;
    numArray6[2] = (byte) 190;
    numArray6[11] = (byte) 215;
    numArray6[13] = (byte) 43;
    numArray6[9] = (byte) 249;
    numArray6[12] = (byte) 198;
    numArray6[15] = (byte) 193;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13405()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[64 /*0x40*/];
      byte[] numArray2 = new byte[55];
      numArray2[32 /*0x20*/] = (byte) 147;
      numArray2[3] = (byte) 246;
      numArray2[22] = (byte) 55;
      numArray2[37] = byte.MaxValue;
      numArray2[12] = (byte) 15;
      numArray2[5] = (byte) 137;
      numArray2[38] = (byte) 184;
      numArray2[7] = (byte) 37;
      numArray2[53] = (byte) 159;
      numArray2[0] = (byte) 14;
      numArray2[10] = (byte) 246;
      numArray2[11] = (byte) 195;
      numArray2[6] = (byte) 107;
      numArray2[36] = (byte) 41;
      numArray2[29] = (byte) 84;
      numArray2[15] = (byte) 116;
      numArray2[13] = (byte) 140;
      numArray2[17] = (byte) 79;
      numArray2[18] = (byte) 198;
      numArray2[19] = (byte) 191;
      numArray2[20] = (byte) 72;
      numArray2[2] = (byte) 228;
      numArray2[27] = (byte) 66;
      numArray2[23] = (byte) 176 /*0xB0*/;
      numArray2[14] = (byte) 253;
      numArray2[33] = (byte) 77;
      numArray2[26] = (byte) 175;
      numArray2[42] = (byte) 249;
      numArray2[45] = (byte) 184;
      numArray2[4] = (byte) 110;
      numArray2[28] = (byte) 58;
      numArray2[8] = (byte) 244;
      numArray2[21] = (byte) 249;
      numArray2[24] = (byte) 208 /*0xD0*/;
      numArray2[34] = (byte) 143;
      numArray2[35] = (byte) 156;
      numArray2[30] = (byte) 13;
      numArray2[46] = (byte) 31 /*0x1F*/;
      numArray2[41] = (byte) 99;
      numArray2[16 /*0x10*/] = (byte) 59;
      numArray2[40] = (byte) 206;
      numArray2[9] = (byte) 185;
      numArray2[44] = (byte) 46;
      numArray2[25] = (byte) 109;
      numArray2[31 /*0x1F*/] = (byte) 43;
      numArray2[39] = (byte) 196;
      numArray2[1] = (byte) 183;
      numArray2[47] = (byte) 106;
      numArray2[48 /*0x30*/] = (byte) 103;
      numArray2[49] = (byte) 37;
      numArray2[43] = (byte) 93;
      numArray2[50] = (byte) 128 /*0x80*/;
      numArray2[52] = (byte) 66;
      numArray2[51] = (byte) 229;
      numArray2[54] = (byte) 0;
      byte[] numArray3 = new byte[55]
      {
        (byte) 28,
        (byte) 216,
        (byte) 106,
        (byte) 184,
        (byte) 55,
        (byte) 173,
        (byte) 207,
        (byte) 60,
        (byte) 191,
        (byte) 66,
        (byte) 154,
        (byte) 237,
        (byte) 217,
        (byte) 174,
        (byte) 83,
        (byte) 189,
        (byte) 80 /*0x50*/,
        (byte) 181,
        (byte) 94,
        (byte) 252,
        (byte) 199,
        (byte) 251,
        (byte) 239,
        (byte) 225,
        (byte) 254,
        (byte) 199,
        (byte) 218,
        (byte) 159,
        (byte) 123,
        (byte) 165,
        (byte) 32 /*0x20*/,
        (byte) 168,
        (byte) 28,
        (byte) 100,
        (byte) 174,
        (byte) 89,
        (byte) 90,
        (byte) 55,
        (byte) 102,
        (byte) 242,
        (byte) 168,
        (byte) 234,
        (byte) 97,
        (byte) 248,
        (byte) 7,
        (byte) 57,
        (byte) 242,
        (byte) 76,
        (byte) 53,
        (byte) 233,
        (byte) 27,
        (byte) 111,
        (byte) 250,
        (byte) 112 /*0x70*/,
        (byte) 212
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[9]
      {
        (byte) 177,
        (byte) 13,
        (byte) 179,
        (byte) 149,
        (byte) 78,
        (byte) 213,
        (byte) 204,
        (byte) 131,
        (byte) 225
      };
      byte[] numArray5 = new byte[9]
      {
        (byte) 114,
        (byte) 238,
        (byte) 219,
        byte.MaxValue,
        (byte) 216,
        (byte) 198,
        (byte) 176 /*0xB0*/,
        (byte) 225,
        (byte) 39
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[64 /*0x40*/];
    byte[] numArray7 = new byte[55]
    {
      (byte) 70,
      (byte) 85,
      (byte) 97,
      (byte) 223,
      (byte) 24,
      (byte) 74,
      (byte) 108,
      (byte) 165,
      (byte) 40,
      (byte) 29,
      (byte) 145,
      (byte) 240 /*0xF0*/,
      (byte) 214,
      (byte) 212,
      (byte) 59,
      (byte) 142,
      (byte) 217,
      (byte) 169,
      (byte) 68,
      (byte) 49,
      (byte) 101,
      (byte) 117,
      (byte) 67,
      (byte) 205,
      (byte) 4,
      (byte) 84,
      (byte) 86,
      (byte) 162,
      (byte) 141,
      (byte) 46,
      (byte) 131,
      (byte) 74,
      (byte) 232,
      (byte) 235,
      (byte) 191,
      (byte) 132,
      (byte) 227,
      (byte) 111,
      (byte) 38,
      (byte) 100,
      (byte) 174,
      (byte) 111,
      (byte) 125,
      (byte) 148,
      (byte) 123,
      (byte) 194,
      (byte) 190,
      (byte) 122,
      (byte) 154,
      (byte) 61,
      (byte) 169,
      (byte) 55,
      (byte) 248,
      (byte) 67,
      (byte) 29
    };
    byte[] numArray8 = new byte[55];
    numArray8[15] = (byte) 245;
    numArray8[42] = (byte) 122;
    numArray8[9] = (byte) 37;
    numArray8[3] = (byte) 47;
    numArray8[4] = (byte) 49;
    numArray8[29] = (byte) 244;
    numArray8[19] = (byte) 245;
    numArray8[28] = (byte) 159;
    numArray8[23] = (byte) 30;
    numArray8[30] = (byte) 140;
    numArray8[37] = (byte) 182;
    numArray8[11] = (byte) 73;
    numArray8[12] = (byte) 21;
    numArray8[13] = (byte) 151;
    numArray8[36] = (byte) 100;
    numArray8[6] = (byte) 224 /*0xE0*/;
    numArray8[21] = (byte) 88;
    numArray8[52] = (byte) 186;
    numArray8[18] = (byte) 109;
    numArray8[8] = (byte) 19;
    numArray8[41] = (byte) 87;
    numArray8[10] = (byte) 20;
    numArray8[0] = (byte) 47;
    numArray8[53] = (byte) 11;
    numArray8[24] = (byte) 88;
    numArray8[27] = (byte) 108;
    numArray8[32 /*0x20*/] = (byte) 18;
    numArray8[7] = (byte) 115;
    numArray8[39] = (byte) 184;
    numArray8[14] = (byte) 124;
    numArray8[17] = (byte) 219;
    numArray8[31 /*0x1F*/] = (byte) 15;
    numArray8[25] = (byte) 24;
    numArray8[33] = (byte) 230;
    numArray8[34] = (byte) 64 /*0x40*/;
    numArray8[54] = (byte) 224 /*0xE0*/;
    numArray8[35] = byte.MaxValue;
    numArray8[16 /*0x10*/] = (byte) 249;
    numArray8[38] = (byte) 121;
    numArray8[48 /*0x30*/] = (byte) 14;
    numArray8[40] = (byte) 212;
    numArray8[5] = (byte) 66;
    numArray8[20] = (byte) 9;
    numArray8[43] = (byte) 45;
    numArray8[44] = (byte) 4;
    numArray8[45] = (byte) 127 /*0x7F*/;
    numArray8[46] = (byte) 65;
    numArray8[47] = (byte) 248;
    numArray8[2] = (byte) 127 /*0x7F*/;
    numArray8[49] = (byte) 235;
    numArray8[50] = (byte) 133;
    numArray8[51] = (byte) 158;
    numArray8[1] = (byte) 79;
    numArray8[26] = (byte) 75;
    numArray8[22] = (byte) 55;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[9]
    {
      (byte) 66,
      (byte) 153,
      (byte) 46,
      (byte) 23,
      (byte) 193,
      (byte) 201,
      (byte) 118,
      (byte) 77,
      (byte) 246
    };
    byte[] numArray10 = new byte[9]
    {
      (byte) 222,
      (byte) 28,
      (byte) 223,
      (byte) 3,
      (byte) 154,
      (byte) 40,
      (byte) 56,
      (byte) 104,
      (byte) 31 /*0x1F*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 9);
    for (int index = 0; index < 9; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[42];
    byte[] response = new byte[42];
    Array.Copy((Array) sc_13393.sspq, 198, (Array) numArray11, 0, 42);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13393.sspr, 198, (Array) numArray11, 0, 42);
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

  internal static string ssp_appserver_13406()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[65];
      byte[] numArray2 = new byte[55]
      {
        (byte) 18,
        (byte) 108,
        (byte) 161,
        (byte) 156,
        (byte) 12,
        (byte) 49,
        (byte) 181,
        (byte) 189,
        (byte) 129,
        (byte) 118,
        (byte) 13,
        (byte) 50,
        (byte) 80 /*0x50*/,
        (byte) 219,
        (byte) 236,
        (byte) 23,
        (byte) 122,
        (byte) 17,
        (byte) 104,
        (byte) 8,
        (byte) 223,
        (byte) 48 /*0x30*/,
        (byte) 11,
        (byte) 52,
        (byte) 133,
        (byte) 134,
        (byte) 219,
        (byte) 3,
        (byte) 144 /*0x90*/,
        (byte) 191,
        (byte) 29,
        (byte) 180,
        (byte) 99,
        (byte) 230,
        (byte) 232,
        (byte) 199,
        (byte) 182,
        (byte) 135,
        (byte) 128 /*0x80*/,
        (byte) 219,
        (byte) 246,
        (byte) 128 /*0x80*/,
        (byte) 213,
        (byte) 63 /*0x3F*/,
        (byte) 51,
        (byte) 108,
        (byte) 229,
        (byte) 141,
        (byte) 167,
        (byte) 144 /*0x90*/,
        (byte) 169,
        (byte) 119,
        (byte) 250,
        (byte) 165,
        (byte) 70
      };
      byte[] numArray3 = new byte[55];
      numArray3[36] = (byte) 220;
      numArray3[10] = (byte) 36;
      numArray3[2] = (byte) 17;
      numArray3[45] = (byte) 117;
      numArray3[46] = (byte) 160 /*0xA0*/;
      numArray3[47] = (byte) 12;
      numArray3[33] = (byte) 44;
      numArray3[24] = (byte) 67;
      numArray3[8] = (byte) 197;
      numArray3[25] = (byte) 42;
      numArray3[14] = (byte) 155;
      numArray3[9] = (byte) 107;
      numArray3[12] = (byte) 76;
      numArray3[27] = (byte) 75;
      numArray3[3] = (byte) 1;
      numArray3[43] = (byte) 28;
      numArray3[44] = (byte) 147;
      numArray3[21] = (byte) 105;
      numArray3[52] = (byte) 159;
      numArray3[35] = (byte) 38;
      numArray3[20] = (byte) 190;
      numArray3[16 /*0x10*/] = (byte) 26;
      numArray3[22] = (byte) 49;
      numArray3[23] = (byte) 181;
      numArray3[5] = (byte) 153;
      numArray3[28] = (byte) 152;
      numArray3[26] = (byte) 85;
      numArray3[7] = (byte) 19;
      numArray3[17] = (byte) 56;
      numArray3[29] = (byte) 115;
      numArray3[38] = (byte) 114;
      numArray3[31 /*0x1F*/] = (byte) 91;
      numArray3[42] = (byte) 76;
      numArray3[1] = (byte) 98;
      numArray3[34] = (byte) 118;
      numArray3[18] = (byte) 98;
      numArray3[19] = (byte) 43;
      numArray3[37] = (byte) 4;
      numArray3[4] = byte.MaxValue;
      numArray3[15] = (byte) 252;
      numArray3[40] = (byte) 127 /*0x7F*/;
      numArray3[41] = (byte) 16 /*0x10*/;
      numArray3[0] = (byte) 62;
      numArray3[13] = (byte) 26;
      numArray3[39] = (byte) 10;
      numArray3[49] = (byte) 126;
      numArray3[32 /*0x20*/] = (byte) 133;
      numArray3[48 /*0x30*/] = (byte) 234;
      numArray3[6] = (byte) 178;
      numArray3[30] = (byte) 179;
      numArray3[50] = (byte) 108;
      numArray3[51] = (byte) 196;
      numArray3[53] = (byte) 94;
      numArray3[11] = (byte) 136;
      numArray3[54] = (byte) 192 /*0xC0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[10]
      {
        (byte) 107,
        (byte) 253,
        (byte) 229,
        (byte) 7,
        (byte) 85,
        (byte) 43,
        (byte) 171,
        (byte) 178,
        (byte) 159,
        (byte) 17
      };
      byte[] numArray5 = new byte[10];
      numArray5[5] = (byte) 143;
      numArray5[2] = (byte) 119;
      numArray5[8] = (byte) 90;
      numArray5[3] = (byte) 152;
      numArray5[4] = (byte) 156;
      numArray5[6] = (byte) 130;
      numArray5[0] = (byte) 136;
      numArray5[7] = (byte) 26;
      numArray5[1] = (byte) 123;
      numArray5[9] = (byte) 188;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[65];
    byte[] numArray7 = new byte[55];
    numArray7[12] = (byte) 114;
    numArray7[3] = (byte) 209;
    numArray7[46] = (byte) 14;
    numArray7[26] = (byte) 78;
    numArray7[0] = (byte) 36;
    numArray7[40] = (byte) 83;
    numArray7[6] = (byte) 75;
    numArray7[7] = (byte) 41;
    numArray7[8] = (byte) 184;
    numArray7[2] = (byte) 218;
    numArray7[43] = (byte) 109;
    numArray7[11] = (byte) 222;
    numArray7[16 /*0x10*/] = (byte) 40;
    numArray7[1] = (byte) 179;
    numArray7[35] = (byte) 84;
    numArray7[52] = (byte) 178;
    numArray7[48 /*0x30*/] = (byte) 134;
    numArray7[17] = (byte) 103;
    numArray7[18] = (byte) 51;
    numArray7[19] = (byte) 59;
    numArray7[14] = (byte) 127 /*0x7F*/;
    numArray7[21] = (byte) 178;
    numArray7[10] = (byte) 209;
    numArray7[23] = (byte) 194;
    numArray7[24] = (byte) 23;
    numArray7[41] = (byte) 232;
    numArray7[50] = (byte) 225;
    numArray7[53] = (byte) 234;
    numArray7[28] = (byte) 199;
    numArray7[29] = (byte) 221;
    numArray7[30] = (byte) 62;
    numArray7[31 /*0x1F*/] = (byte) 174;
    numArray7[32 /*0x20*/] = (byte) 69;
    numArray7[15] = (byte) 239;
    numArray7[34] = (byte) 192 /*0xC0*/;
    numArray7[47] = (byte) 130;
    numArray7[36] = (byte) 122;
    numArray7[22] = (byte) 173;
    numArray7[27] = (byte) 104;
    numArray7[39] = (byte) 241;
    numArray7[9] = (byte) 127 /*0x7F*/;
    numArray7[20] = (byte) 139;
    numArray7[42] = (byte) 16 /*0x10*/;
    numArray7[38] = (byte) 75;
    numArray7[44] = (byte) 237;
    numArray7[45] = (byte) 56;
    numArray7[37] = (byte) 90;
    numArray7[25] = (byte) 52;
    numArray7[33] = (byte) 130;
    numArray7[49] = (byte) 220;
    numArray7[4] = (byte) 96 /*0x60*/;
    numArray7[51] = (byte) 224 /*0xE0*/;
    numArray7[5] = (byte) 192 /*0xC0*/;
    numArray7[13] = (byte) 20;
    numArray7[54] = (byte) 240 /*0xF0*/;
    byte[] numArray8 = new byte[55]
    {
      (byte) 14,
      (byte) 211,
      (byte) 220,
      (byte) 174,
      (byte) 10,
      (byte) 84,
      (byte) 173,
      (byte) 198,
      (byte) 128 /*0x80*/,
      (byte) 178,
      (byte) 247,
      (byte) 174,
      (byte) 193,
      (byte) 125,
      (byte) 195,
      (byte) 179,
      (byte) 72,
      (byte) 215,
      (byte) 103,
      (byte) 206,
      (byte) 218,
      (byte) 206,
      (byte) 19,
      (byte) 240 /*0xF0*/,
      (byte) 53,
      (byte) 228,
      (byte) 194,
      (byte) 71,
      (byte) 41,
      (byte) 103,
      (byte) 99,
      (byte) 213,
      (byte) 122,
      (byte) 59,
      (byte) 84,
      (byte) 232,
      (byte) 201,
      (byte) 26,
      (byte) 81,
      (byte) 162,
      (byte) 46,
      (byte) 242,
      (byte) 194,
      (byte) 149,
      (byte) 45,
      (byte) 176 /*0xB0*/,
      (byte) 137,
      (byte) 196,
      (byte) 157,
      (byte) 58,
      (byte) 95,
      (byte) 117,
      (byte) 115,
      (byte) 160 /*0xA0*/,
      (byte) 108
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[10]
    {
      (byte) 67,
      (byte) 218,
      (byte) 6,
      (byte) 166,
      (byte) 1,
      (byte) 119,
      (byte) 222,
      (byte) 227,
      (byte) 38,
      (byte) 61
    };
    byte[] numArray10 = new byte[10];
    numArray10[1] = (byte) 27;
    numArray10[5] = (byte) 57;
    numArray10[8] = (byte) 167;
    numArray10[6] = (byte) 203;
    numArray10[4] = (byte) 75;
    numArray10[2] = (byte) 75;
    numArray10[3] = (byte) 97;
    numArray10[7] = (byte) 28;
    numArray10[0] = (byte) 202;
    numArray10[9] = (byte) 59;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 10);
    for (int index = 0; index < 10; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13407()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[58];
      byte[] numArray2 = new byte[55];
      numArray2[32 /*0x20*/] = (byte) 155;
      numArray2[50] = (byte) 19;
      numArray2[53] = (byte) 220;
      numArray2[12] = (byte) 25;
      numArray2[25] = (byte) 199;
      numArray2[19] = (byte) 98;
      numArray2[6] = (byte) 88;
      numArray2[7] = (byte) 69;
      numArray2[0] = (byte) 156;
      numArray2[10] = (byte) 30;
      numArray2[22] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 139;
      numArray2[2] = (byte) 130;
      numArray2[13] = (byte) 217;
      numArray2[14] = (byte) 111;
      numArray2[11] = (byte) 197;
      numArray2[16 /*0x10*/] = (byte) 87;
      numArray2[51] = (byte) 203;
      numArray2[23] = (byte) 84;
      numArray2[45] = (byte) 104;
      numArray2[43] = (byte) 81;
      numArray2[47] = (byte) 233;
      numArray2[20] = (byte) 192 /*0xC0*/;
      numArray2[17] = (byte) 72;
      numArray2[3] = (byte) 233;
      numArray2[9] = (byte) 138;
      numArray2[36] = (byte) 244;
      numArray2[27] = (byte) 138;
      numArray2[8] = (byte) 124;
      numArray2[29] = (byte) 102;
      numArray2[1] = (byte) 134;
      numArray2[31 /*0x1F*/] = (byte) 57;
      numArray2[18] = (byte) 164;
      numArray2[33] = byte.MaxValue;
      numArray2[38] = (byte) 73;
      numArray2[39] = (byte) 155;
      numArray2[48 /*0x30*/] = (byte) 8;
      numArray2[37] = (byte) 244;
      numArray2[34] = (byte) 9;
      numArray2[30] = (byte) 175;
      numArray2[40] = (byte) 229;
      numArray2[41] = (byte) 24;
      numArray2[42] = (byte) 137;
      numArray2[21] = (byte) 41;
      numArray2[4] = (byte) 153;
      numArray2[54] = (byte) 70;
      numArray2[46] = (byte) 227;
      numArray2[44] = (byte) 244;
      numArray2[35] = (byte) 122;
      numArray2[49] = (byte) 15;
      numArray2[28] = (byte) 47;
      numArray2[24] = (byte) 228;
      numArray2[52] = (byte) 101;
      numArray2[15] = (byte) 14;
      numArray2[26] = (byte) 174;
      byte[] numArray3 = new byte[55]
      {
        (byte) 53,
        (byte) 19,
        (byte) 175,
        (byte) 116,
        (byte) 164,
        (byte) 166,
        (byte) 148,
        (byte) 91,
        (byte) 253,
        (byte) 39,
        (byte) 7,
        (byte) 2,
        (byte) 200,
        (byte) 80 /*0x50*/,
        (byte) 174,
        (byte) 31 /*0x1F*/,
        (byte) 86,
        (byte) 144 /*0x90*/,
        (byte) 71,
        (byte) 99,
        (byte) 145,
        (byte) 130,
        (byte) 50,
        (byte) 13,
        (byte) 60,
        (byte) 154,
        (byte) 115,
        (byte) 169,
        (byte) 174,
        (byte) 103,
        (byte) 161,
        (byte) 141,
        (byte) 135,
        (byte) 150,
        (byte) 233,
        (byte) 235,
        (byte) 131,
        (byte) 141,
        (byte) 224 /*0xE0*/,
        (byte) 9,
        (byte) 227,
        (byte) 82,
        (byte) 185,
        (byte) 174,
        (byte) 115,
        (byte) 34,
        (byte) 20,
        (byte) 117,
        (byte) 18,
        (byte) 70,
        (byte) 210,
        (byte) 218,
        (byte) 128 /*0x80*/,
        (byte) 13,
        (byte) 63 /*0x3F*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[3]
      {
        (byte) 0,
        (byte) 138,
        (byte) 0
      };
      numArray4[0] = (byte) 205;
      numArray4[2] = (byte) 238;
      byte[] numArray5 = new byte[3]
      {
        (byte) 199,
        (byte) 68,
        (byte) 116
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[58];
    byte[] numArray7 = new byte[55];
    numArray7[5] = (byte) 102;
    numArray7[16 /*0x10*/] = (byte) 22;
    numArray7[7] = (byte) 45;
    numArray7[3] = (byte) 135;
    numArray7[14] = (byte) 42;
    numArray7[45] = (byte) 210;
    numArray7[30] = (byte) 252;
    numArray7[22] = (byte) 248;
    numArray7[8] = (byte) 184;
    numArray7[24] = (byte) 137;
    numArray7[37] = (byte) 69;
    numArray7[21] = (byte) 153;
    numArray7[2] = (byte) 116;
    numArray7[25] = (byte) 166;
    numArray7[13] = (byte) 109;
    numArray7[15] = (byte) 161;
    numArray7[12] = (byte) 171;
    numArray7[35] = (byte) 162;
    numArray7[18] = (byte) 96 /*0x60*/;
    numArray7[19] = (byte) 24;
    numArray7[20] = (byte) 194;
    numArray7[6] = (byte) 164;
    numArray7[1] = (byte) 82;
    numArray7[9] = (byte) 81;
    numArray7[10] = (byte) 60;
    numArray7[52] = (byte) 9;
    numArray7[26] = (byte) 201;
    numArray7[27] = (byte) 13;
    numArray7[28] = (byte) 174;
    numArray7[42] = (byte) 32 /*0x20*/;
    numArray7[36] = (byte) 59;
    numArray7[31 /*0x1F*/] = (byte) 212;
    numArray7[32 /*0x20*/] = (byte) 102;
    numArray7[33] = (byte) 224 /*0xE0*/;
    numArray7[34] = (byte) 145;
    numArray7[0] = (byte) 130;
    numArray7[49] = (byte) 34;
    numArray7[41] = (byte) 166;
    numArray7[38] = (byte) 17;
    numArray7[39] = (byte) 248;
    numArray7[44] = (byte) 77;
    numArray7[43] = (byte) 124;
    numArray7[40] = (byte) 251;
    numArray7[4] = (byte) 75;
    numArray7[29] = (byte) 127 /*0x7F*/;
    numArray7[11] = (byte) 27;
    numArray7[46] = (byte) 108;
    numArray7[17] = (byte) 118;
    numArray7[48 /*0x30*/] = (byte) 202;
    numArray7[47] = (byte) 224 /*0xE0*/;
    numArray7[50] = (byte) 223;
    numArray7[51] = (byte) 155;
    numArray7[23] = (byte) 100;
    numArray7[53] = (byte) 73;
    numArray7[54] = (byte) 118;
    byte[] numArray8 = new byte[55]
    {
      (byte) 160 /*0xA0*/,
      (byte) 206,
      (byte) 191,
      (byte) 49,
      (byte) 184,
      (byte) 175,
      (byte) 134,
      (byte) 51,
      (byte) 66,
      (byte) 8,
      (byte) 136,
      (byte) 69,
      (byte) 138,
      (byte) 73,
      (byte) 87,
      (byte) 186,
      (byte) 6,
      (byte) 191,
      (byte) 128 /*0x80*/,
      (byte) 19,
      (byte) 5,
      (byte) 199,
      (byte) 128 /*0x80*/,
      (byte) 185,
      (byte) 65,
      (byte) 253,
      (byte) 227,
      (byte) 0,
      (byte) 15,
      (byte) 87,
      (byte) 166,
      (byte) 202,
      (byte) 7,
      (byte) 133,
      (byte) 41,
      (byte) 209,
      (byte) 170,
      (byte) 29,
      (byte) 28,
      (byte) 239,
      (byte) 162,
      (byte) 90,
      (byte) 65,
      (byte) 237,
      (byte) 10,
      (byte) 8,
      (byte) 7,
      (byte) 189,
      (byte) 143,
      (byte) 113,
      (byte) 43,
      (byte) 186,
      (byte) 60,
      (byte) 208 /*0xD0*/,
      (byte) 227
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[3]
    {
      (byte) 249,
      (byte) 72,
      (byte) 106
    };
    byte[] numArray10 = new byte[3]
    {
      (byte) 0,
      (byte) 0,
      (byte) 78
    };
    numArray10[1] = (byte) 68;
    numArray10[0] = (byte) 216;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 3);
    for (int index = 0; index < 3; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13408()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[58];
      byte[] numArray2 = new byte[55];
      numArray2[1] = (byte) 51;
      numArray2[19] = (byte) 41;
      numArray2[24] = (byte) 50;
      numArray2[32 /*0x20*/] = (byte) 72;
      numArray2[31 /*0x1F*/] = (byte) 52;
      numArray2[4] = (byte) 138;
      numArray2[11] = (byte) 49;
      numArray2[7] = (byte) 184;
      numArray2[51] = (byte) 8;
      numArray2[2] = (byte) 62;
      numArray2[10] = (byte) 84;
      numArray2[35] = (byte) 208 /*0xD0*/;
      numArray2[0] = (byte) 96 /*0x60*/;
      numArray2[13] = (byte) 152;
      numArray2[14] = (byte) 228;
      numArray2[15] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 207;
      numArray2[17] = (byte) 144 /*0x90*/;
      numArray2[9] = (byte) 145;
      numArray2[36] = (byte) 3;
      numArray2[50] = (byte) 35;
      numArray2[33] = (byte) 134;
      numArray2[18] = (byte) 42;
      numArray2[3] = (byte) 58;
      numArray2[8] = (byte) 245;
      numArray2[49] = (byte) 81;
      numArray2[46] = (byte) 175;
      numArray2[41] = (byte) 116;
      numArray2[28] = (byte) 18;
      numArray2[29] = (byte) 51;
      numArray2[27] = (byte) 166;
      numArray2[26] = (byte) 2;
      numArray2[43] = (byte) 223;
      numArray2[25] = (byte) 254;
      numArray2[34] = (byte) 65;
      numArray2[21] = (byte) 16 /*0x10*/;
      numArray2[40] = (byte) 147;
      numArray2[23] = (byte) 107;
      numArray2[38] = (byte) 131;
      numArray2[39] = (byte) 63 /*0x3F*/;
      numArray2[30] = (byte) 77;
      numArray2[37] = (byte) 130;
      numArray2[42] = (byte) 100;
      numArray2[20] = (byte) 240 /*0xF0*/;
      numArray2[44] = (byte) 136;
      numArray2[45] = (byte) 200;
      numArray2[5] = (byte) 247;
      numArray2[47] = (byte) 210;
      numArray2[48 /*0x30*/] = (byte) 207;
      numArray2[6] = (byte) 13;
      numArray2[12] = (byte) 152;
      numArray2[22] = (byte) 56;
      numArray2[52] = (byte) 200;
      numArray2[53] = (byte) 115;
      numArray2[54] = (byte) 166;
      byte[] numArray3 = new byte[55]
      {
        (byte) 237,
        (byte) 29,
        (byte) 48 /*0x30*/,
        (byte) 186,
        (byte) 191,
        (byte) 113,
        (byte) 57,
        (byte) 242,
        (byte) 119,
        (byte) 137,
        (byte) 94,
        (byte) 217,
        (byte) 78,
        (byte) 199,
        (byte) 160 /*0xA0*/,
        (byte) 1,
        (byte) 166,
        (byte) 73,
        (byte) 163,
        (byte) 75,
        (byte) 59,
        (byte) 175,
        (byte) 98,
        (byte) 27,
        (byte) 123,
        (byte) 182,
        (byte) 61,
        (byte) 135,
        (byte) 163,
        (byte) 128 /*0x80*/,
        (byte) 238,
        (byte) 82,
        (byte) 19,
        (byte) 199,
        (byte) 208 /*0xD0*/,
        (byte) 119,
        (byte) 189,
        (byte) 123,
        (byte) 95,
        (byte) 209,
        (byte) 169,
        (byte) 82,
        (byte) 162,
        (byte) 117,
        (byte) 75,
        (byte) 244,
        (byte) 212,
        (byte) 136,
        (byte) 211,
        (byte) 53,
        (byte) 75,
        (byte) 8,
        (byte) 254,
        (byte) 153,
        (byte) 254
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[3]
      {
        (byte) 0,
        (byte) 157,
        (byte) 0
      };
      numArray4[0] = (byte) 239;
      numArray4[2] = (byte) 61;
      byte[] numArray5 = new byte[3]
      {
        (byte) 226,
        (byte) 111,
        (byte) 29
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_13393.sspq, 240 /*0xF0*/, (Array) numArray6, 0, 54);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13393.sspr, 240 /*0xF0*/, (Array) numArray6, 0, 54);
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
    byte[] numArray7 = new byte[58];
    byte[] numArray8 = new byte[55];
    numArray8[26] = (byte) 36;
    numArray8[32 /*0x20*/] = (byte) 238;
    numArray8[2] = (byte) 246;
    numArray8[7] = (byte) 27;
    numArray8[4] = (byte) 26;
    numArray8[48 /*0x30*/] = (byte) 149;
    numArray8[6] = (byte) 197;
    numArray8[12] = (byte) 161;
    numArray8[8] = (byte) 31 /*0x1F*/;
    numArray8[31 /*0x1F*/] = (byte) 171;
    numArray8[1] = (byte) 139;
    numArray8[10] = (byte) 193;
    numArray8[37] = (byte) 136;
    numArray8[13] = (byte) 157;
    numArray8[29] = (byte) 234;
    numArray8[3] = (byte) 92;
    numArray8[17] = (byte) 79;
    numArray8[20] = (byte) 182;
    numArray8[18] = (byte) 130;
    numArray8[19] = (byte) 58;
    numArray8[34] = (byte) 116;
    numArray8[22] = (byte) 202;
    numArray8[47] = (byte) 217;
    numArray8[16 /*0x10*/] = (byte) 100;
    numArray8[0] = (byte) 9;
    numArray8[54] = (byte) 96 /*0x60*/;
    numArray8[53] = (byte) 65;
    numArray8[11] = (byte) 70;
    numArray8[9] = (byte) 196;
    numArray8[43] = (byte) 177;
    numArray8[30] = (byte) 143;
    numArray8[45] = (byte) 69;
    numArray8[27] = (byte) 112 /*0x70*/;
    numArray8[33] = (byte) 253;
    numArray8[14] = (byte) 227;
    numArray8[28] = (byte) 118;
    numArray8[36] = (byte) 11;
    numArray8[15] = (byte) 55;
    numArray8[38] = (byte) 35;
    numArray8[39] = (byte) 164;
    numArray8[40] = (byte) 182;
    numArray8[41] = (byte) 124;
    numArray8[42] = (byte) 197;
    numArray8[25] = (byte) 118;
    numArray8[44] = (byte) 80 /*0x50*/;
    numArray8[5] = (byte) 207;
    numArray8[46] = (byte) 109;
    numArray8[52] = (byte) 155;
    numArray8[51] = (byte) 215;
    numArray8[49] = (byte) 64 /*0x40*/;
    numArray8[21] = (byte) 80 /*0x50*/;
    numArray8[24] = (byte) 46;
    numArray8[50] = (byte) 79;
    numArray8[23] = (byte) 202;
    numArray8[35] = (byte) 252;
    byte[] numArray9 = new byte[55]
    {
      (byte) 10,
      (byte) 106,
      (byte) 182,
      (byte) 170,
      (byte) 235,
      (byte) 23,
      (byte) 98,
      (byte) 237,
      (byte) 124,
      (byte) 131,
      (byte) 14,
      (byte) 74,
      (byte) 168,
      (byte) 109,
      (byte) 144 /*0x90*/,
      (byte) 188,
      (byte) 194,
      (byte) 173,
      (byte) 139,
      (byte) 28,
      (byte) 32 /*0x20*/,
      (byte) 204,
      (byte) 226,
      (byte) 115,
      (byte) 41,
      (byte) 131,
      (byte) 108,
      (byte) 143,
      (byte) 51,
      (byte) 8,
      (byte) 24,
      (byte) 0,
      (byte) 168,
      (byte) 123,
      (byte) 160 /*0xA0*/,
      (byte) 148,
      (byte) 160 /*0xA0*/,
      (byte) 212,
      (byte) 206,
      (byte) 204,
      (byte) 163,
      (byte) 235,
      (byte) 42,
      (byte) 181,
      (byte) 76,
      (byte) 43,
      (byte) 134,
      (byte) 82,
      (byte) 108,
      (byte) 109,
      (byte) 250,
      (byte) 43,
      (byte) 63 /*0x3F*/,
      (byte) 230,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[3]
    {
      (byte) 177,
      (byte) 68,
      (byte) 155
    };
    byte[] numArray11 = new byte[3]
    {
      (byte) 200,
      (byte) 127 /*0x7F*/,
      (byte) 101
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 3);
    for (int index = 0; index < 3; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13409()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[146];
      byte[] numArray2 = new byte[55];
      numArray2[5] = (byte) 171;
      numArray2[10] = (byte) 35;
      numArray2[28] = (byte) 22;
      numArray2[12] = (byte) 251;
      numArray2[4] = (byte) 109;
      numArray2[36] = (byte) 67;
      numArray2[6] = (byte) 77;
      numArray2[7] = (byte) 129;
      numArray2[3] = (byte) 6;
      numArray2[13] = (byte) 157;
      numArray2[51] = (byte) 197;
      numArray2[21] = (byte) 12;
      numArray2[2] = (byte) 170;
      numArray2[11] = (byte) 99;
      numArray2[50] = (byte) 93;
      numArray2[39] = (byte) 3;
      numArray2[16 /*0x10*/] = (byte) 62;
      numArray2[17] = (byte) 155;
      numArray2[18] = (byte) 44;
      numArray2[19] = (byte) 34;
      numArray2[15] = (byte) 251;
      numArray2[9] = (byte) 228;
      numArray2[22] = (byte) 81;
      numArray2[23] = (byte) 240 /*0xF0*/;
      numArray2[52] = (byte) 78;
      numArray2[25] = (byte) 141;
      numArray2[26] = (byte) 148;
      numArray2[20] = (byte) 39;
      numArray2[32 /*0x20*/] = (byte) 104;
      numArray2[29] = (byte) 228;
      numArray2[30] = (byte) 153;
      numArray2[35] = (byte) 52;
      numArray2[40] = (byte) 198;
      numArray2[27] = (byte) 45;
      numArray2[53] = (byte) 75;
      numArray2[48 /*0x30*/] = (byte) 113;
      numArray2[24] = (byte) 118;
      numArray2[37] = (byte) 195;
      numArray2[34] = (byte) 254;
      numArray2[47] = (byte) 10;
      numArray2[33] = (byte) 246;
      numArray2[41] = (byte) 242;
      numArray2[42] = byte.MaxValue;
      numArray2[43] = (byte) 217;
      numArray2[44] = (byte) 154;
      numArray2[45] = (byte) 195;
      numArray2[31 /*0x1F*/] = (byte) 231;
      numArray2[1] = (byte) 6;
      numArray2[54] = (byte) 114;
      numArray2[49] = (byte) 72;
      numArray2[46] = (byte) 50;
      numArray2[38] = (byte) 66;
      numArray2[14] = (byte) 63 /*0x3F*/;
      numArray2[0] = (byte) 125;
      numArray2[8] = (byte) 171;
      byte[] numArray3 = new byte[55]
      {
        (byte) 118,
        (byte) 66,
        (byte) 86,
        (byte) 97,
        (byte) 79,
        (byte) 220,
        (byte) 37,
        (byte) 132,
        (byte) 138,
        (byte) 201,
        (byte) 62,
        (byte) 124,
        (byte) 209,
        (byte) 190,
        (byte) 168,
        (byte) 34,
        (byte) 41,
        (byte) 28,
        (byte) 214,
        (byte) 144 /*0x90*/,
        (byte) 216,
        (byte) 29,
        (byte) 195,
        (byte) 97,
        (byte) 127 /*0x7F*/,
        (byte) 2,
        (byte) 181,
        (byte) 89,
        (byte) 47,
        (byte) 108,
        (byte) 41,
        (byte) 246,
        (byte) 98,
        (byte) 175,
        (byte) 183,
        (byte) 153,
        (byte) 64 /*0x40*/,
        (byte) 159,
        (byte) 194,
        (byte) 223,
        (byte) 166,
        (byte) 44,
        (byte) 209,
        (byte) 58,
        (byte) 142,
        (byte) 21,
        (byte) 14,
        (byte) 59,
        (byte) 39,
        (byte) 245,
        (byte) 61,
        (byte) 44,
        (byte) 249,
        (byte) 92,
        (byte) 194
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[15] = (byte) 246;
      numArray4[31 /*0x1F*/] = (byte) 70;
      numArray4[24] = (byte) 30;
      numArray4[3] = (byte) 141;
      numArray4[4] = (byte) 100;
      numArray4[37] = (byte) 94;
      numArray4[6] = (byte) 63 /*0x3F*/;
      numArray4[36] = (byte) 221;
      numArray4[42] = (byte) 186;
      numArray4[9] = (byte) 220;
      numArray4[30] = (byte) 46;
      numArray4[17] = (byte) 182;
      numArray4[12] = (byte) 140;
      numArray4[7] = (byte) 119;
      numArray4[29] = (byte) 115;
      numArray4[44] = (byte) 171;
      numArray4[32 /*0x20*/] = (byte) 41;
      numArray4[54] = (byte) 84;
      numArray4[28] = (byte) 103;
      numArray4[52] = (byte) 143;
      numArray4[20] = (byte) 232;
      numArray4[21] = (byte) 247;
      numArray4[49] = (byte) 117;
      numArray4[23] = (byte) 46;
      numArray4[46] = (byte) 217;
      numArray4[25] = (byte) 85;
      numArray4[26] = (byte) 61;
      numArray4[27] = (byte) 165;
      numArray4[22] = (byte) 233;
      numArray4[10] = (byte) 129;
      numArray4[2] = (byte) 132;
      numArray4[19] = (byte) 211;
      numArray4[47] = (byte) 96 /*0x60*/;
      numArray4[33] = (byte) 65;
      numArray4[34] = (byte) 36;
      numArray4[35] = (byte) 84;
      numArray4[41] = (byte) 23;
      numArray4[50] = (byte) 232;
      numArray4[51] = (byte) 56;
      numArray4[39] = (byte) 234;
      numArray4[40] = (byte) 249;
      numArray4[11] = (byte) 16 /*0x10*/;
      numArray4[1] = (byte) 222;
      numArray4[43] = (byte) 236;
      numArray4[18] = (byte) 173;
      numArray4[45] = (byte) 90;
      numArray4[48 /*0x30*/] = (byte) 153;
      numArray4[16 /*0x10*/] = (byte) 60;
      numArray4[38] = (byte) 65;
      numArray4[0] = (byte) 100;
      numArray4[8] = (byte) 163;
      numArray4[14] = (byte) 72;
      numArray4[5] = (byte) 156;
      numArray4[53] = (byte) 144 /*0x90*/;
      numArray4[13] = (byte) 47;
      byte[] numArray5 = new byte[55]
      {
        (byte) 219,
        (byte) 61,
        (byte) 59,
        (byte) 125,
        (byte) 198,
        (byte) 165,
        (byte) 54,
        (byte) 236,
        (byte) 18,
        (byte) 199,
        (byte) 178,
        (byte) 185,
        (byte) 16 /*0x10*/,
        (byte) 132,
        (byte) 221,
        (byte) 161,
        (byte) 33,
        (byte) 194,
        (byte) 211,
        (byte) 127 /*0x7F*/,
        (byte) 209,
        (byte) 159,
        (byte) 78,
        (byte) 85,
        (byte) 175,
        (byte) 17,
        (byte) 191,
        (byte) 12,
        (byte) 97,
        (byte) 204,
        (byte) 28,
        (byte) 86,
        (byte) 0,
        (byte) 161,
        (byte) 197,
        (byte) 18,
        (byte) 55,
        (byte) 191,
        (byte) 70,
        (byte) 102,
        (byte) 119,
        (byte) 159,
        (byte) 181,
        (byte) 140,
        (byte) 22,
        (byte) 45,
        (byte) 44,
        (byte) 179,
        (byte) 128 /*0x80*/,
        (byte) 126,
        (byte) 92,
        (byte) 93,
        (byte) 159,
        (byte) 151,
        (byte) 151
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[36];
      numArray6[25] = (byte) 101;
      numArray6[8] = (byte) 130;
      numArray6[2] = (byte) 147;
      numArray6[28] = (byte) 50;
      numArray6[29] = (byte) 235;
      numArray6[5] = (byte) 28;
      numArray6[24] = (byte) 134;
      numArray6[6] = (byte) 165;
      numArray6[15] = (byte) 66;
      numArray6[3] = (byte) 131;
      numArray6[17] = (byte) 123;
      numArray6[27] = (byte) 128 /*0x80*/;
      numArray6[14] = (byte) 238;
      numArray6[19] = (byte) 178;
      numArray6[0] = (byte) 211;
      numArray6[12] = (byte) 148;
      numArray6[11] = (byte) 82;
      numArray6[1] = (byte) 170;
      numArray6[18] = (byte) 200;
      numArray6[4] = (byte) 143;
      numArray6[20] = (byte) 189;
      numArray6[21] = (byte) 16 /*0x10*/;
      numArray6[22] = (byte) 80 /*0x50*/;
      numArray6[23] = (byte) 236;
      numArray6[33] = (byte) 103;
      numArray6[13] = (byte) 145;
      numArray6[9] = (byte) 77;
      numArray6[7] = (byte) 183;
      numArray6[10] = (byte) 93;
      numArray6[26] = (byte) 150;
      numArray6[30] = (byte) 227;
      numArray6[16 /*0x10*/] = (byte) 56;
      numArray6[32 /*0x20*/] = (byte) 135;
      numArray6[34] = (byte) 179;
      numArray6[31 /*0x1F*/] = (byte) 106;
      numArray6[35] = (byte) 69;
      byte[] numArray7 = new byte[36];
      numArray7[18] = (byte) 129;
      numArray7[14] = (byte) 179;
      numArray7[2] = (byte) 84;
      numArray7[3] = (byte) 163;
      numArray7[20] = (byte) 46;
      numArray7[26] = (byte) 160 /*0xA0*/;
      numArray7[17] = (byte) 207;
      numArray7[7] = (byte) 233;
      numArray7[8] = (byte) 172;
      numArray7[9] = (byte) 180;
      numArray7[0] = (byte) 253;
      numArray7[6] = (byte) 134;
      numArray7[5] = (byte) 131;
      numArray7[13] = (byte) 239;
      numArray7[15] = (byte) 20;
      numArray7[33] = (byte) 160 /*0xA0*/;
      numArray7[12] = (byte) 122;
      numArray7[31 /*0x1F*/] = (byte) 19;
      numArray7[32 /*0x20*/] = (byte) 4;
      numArray7[30] = (byte) 238;
      numArray7[4] = (byte) 80 /*0x50*/;
      numArray7[21] = (byte) 61;
      numArray7[22] = (byte) 128 /*0x80*/;
      numArray7[23] = (byte) 248;
      numArray7[24] = (byte) 14;
      numArray7[25] = (byte) 28;
      numArray7[10] = (byte) 238;
      numArray7[27] = (byte) 87;
      numArray7[28] = (byte) 8;
      numArray7[29] = (byte) 93;
      numArray7[16 /*0x10*/] = (byte) 112 /*0x70*/;
      numArray7[34] = (byte) 22;
      numArray7[35] = (byte) 0;
      numArray7[19] = (byte) 142;
      numArray7[1] = (byte) 105;
      numArray7[11] = (byte) 251;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[146];
    byte[] numArray9 = new byte[55]
    {
      (byte) 48 /*0x30*/,
      (byte) 236,
      (byte) 122,
      (byte) 213,
      (byte) 185,
      (byte) 193,
      (byte) 210,
      (byte) 62,
      (byte) 191,
      (byte) 67,
      (byte) 192 /*0xC0*/,
      (byte) 1,
      (byte) 236,
      (byte) 188,
      (byte) 172,
      (byte) 38,
      (byte) 5,
      (byte) 118,
      (byte) 13,
      (byte) 50,
      (byte) 201,
      (byte) 109,
      (byte) 124,
      (byte) 74,
      (byte) 112 /*0x70*/,
      (byte) 224 /*0xE0*/,
      (byte) 239,
      (byte) 211,
      (byte) 245,
      (byte) 27,
      (byte) 210,
      (byte) 89,
      (byte) 39,
      (byte) 79,
      (byte) 187,
      (byte) 10,
      (byte) 143,
      (byte) 208 /*0xD0*/,
      (byte) 172,
      (byte) 253,
      (byte) 48 /*0x30*/,
      (byte) 25,
      (byte) 253,
      (byte) 251,
      (byte) 119,
      (byte) 226,
      byte.MaxValue,
      (byte) 183,
      (byte) 49,
      (byte) 60,
      (byte) 136,
      (byte) 102,
      (byte) 162,
      (byte) 210,
      (byte) 216
    };
    byte[] numArray10 = new byte[55];
    numArray10[18] = (byte) 23;
    numArray10[26] = (byte) 130;
    numArray10[9] = (byte) 136;
    numArray10[3] = (byte) 188;
    numArray10[4] = (byte) 25;
    numArray10[5] = (byte) 92;
    numArray10[49] = (byte) 250;
    numArray10[7] = (byte) 200;
    numArray10[19] = (byte) 85;
    numArray10[34] = (byte) 221;
    numArray10[45] = (byte) 16 /*0x10*/;
    numArray10[11] = (byte) 214;
    numArray10[12] = (byte) 157;
    numArray10[13] = (byte) 63 /*0x3F*/;
    numArray10[43] = (byte) 122;
    numArray10[44] = (byte) 139;
    numArray10[16 /*0x10*/] = (byte) 45;
    numArray10[17] = (byte) 120;
    numArray10[52] = (byte) 134;
    numArray10[10] = (byte) 58;
    numArray10[20] = (byte) 119;
    numArray10[21] = (byte) 157;
    numArray10[22] = (byte) 35;
    numArray10[51] = (byte) 135;
    numArray10[15] = (byte) 227;
    numArray10[25] = (byte) 212;
    numArray10[23] = (byte) 127 /*0x7F*/;
    numArray10[1] = (byte) 11;
    numArray10[28] = (byte) 99;
    numArray10[29] = (byte) 33;
    numArray10[41] = (byte) 1;
    numArray10[31 /*0x1F*/] = (byte) 164;
    numArray10[32 /*0x20*/] = (byte) 167;
    numArray10[33] = (byte) 201;
    numArray10[39] = (byte) 175;
    numArray10[35] = (byte) 248;
    numArray10[36] = (byte) 152;
    numArray10[38] = (byte) 9;
    numArray10[48 /*0x30*/] = (byte) 233;
    numArray10[30] = (byte) 197;
    numArray10[40] = (byte) 232;
    numArray10[50] = (byte) 253;
    numArray10[42] = (byte) 15;
    numArray10[46] = (byte) 136;
    numArray10[14] = (byte) 34;
    numArray10[27] = (byte) 128 /*0x80*/;
    numArray10[54] = (byte) 167;
    numArray10[47] = (byte) 116;
    numArray10[2] = (byte) 54;
    numArray10[6] = (byte) 173;
    numArray10[37] = (byte) 254;
    numArray10[53] = (byte) 115;
    numArray10[0] = (byte) 241;
    numArray10[8] = (byte) 157;
    numArray10[24] = (byte) 195;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 92,
      (byte) 216,
      (byte) 248,
      (byte) 153,
      (byte) 93,
      (byte) 144 /*0x90*/,
      (byte) 16 /*0x10*/,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 132,
      (byte) 36,
      (byte) 8,
      (byte) 8,
      (byte) 55,
      (byte) 203,
      (byte) 246,
      (byte) 228,
      (byte) 208 /*0xD0*/,
      (byte) 224 /*0xE0*/,
      (byte) 23,
      (byte) 246,
      (byte) 195,
      (byte) 132,
      (byte) 125,
      (byte) 130,
      (byte) 18,
      (byte) 20,
      (byte) 65,
      (byte) 207,
      (byte) 36,
      (byte) 132,
      (byte) 79,
      (byte) 165,
      (byte) 169,
      (byte) 3,
      (byte) 146,
      (byte) 218,
      (byte) 146,
      (byte) 212,
      (byte) 62,
      (byte) 72,
      (byte) 133,
      (byte) 96 /*0x60*/,
      (byte) 132,
      (byte) 61,
      (byte) 173,
      (byte) 134,
      (byte) 109,
      (byte) 135,
      (byte) 166,
      (byte) 174,
      (byte) 0,
      (byte) 72,
      (byte) 59,
      (byte) 249
    };
    byte[] numArray12 = new byte[55];
    numArray12[46] = (byte) 3;
    numArray12[0] = (byte) 41;
    numArray12[2] = (byte) 204;
    numArray12[3] = (byte) 61;
    numArray12[11] = (byte) 115;
    numArray12[51] = (byte) 200;
    numArray12[53] = (byte) 88;
    numArray12[7] = (byte) 217;
    numArray12[5] = (byte) 58;
    numArray12[37] = (byte) 148;
    numArray12[10] = (byte) 63 /*0x3F*/;
    numArray12[52] = (byte) 150;
    numArray12[12] = (byte) 186;
    numArray12[4] = (byte) 243;
    numArray12[29] = (byte) 96 /*0x60*/;
    numArray12[26] = (byte) 220;
    numArray12[16 /*0x10*/] = (byte) 87;
    numArray12[21] = (byte) 23;
    numArray12[18] = (byte) 248;
    numArray12[43] = (byte) 210;
    numArray12[20] = (byte) 165;
    numArray12[36] = (byte) 248;
    numArray12[22] = (byte) 43;
    numArray12[23] = (byte) 61;
    numArray12[15] = (byte) 184;
    numArray12[47] = byte.MaxValue;
    numArray12[33] = (byte) 78;
    numArray12[30] = (byte) 153;
    numArray12[28] = (byte) 121;
    numArray12[50] = (byte) 237;
    numArray12[31 /*0x1F*/] = (byte) 18;
    numArray12[19] = (byte) 56;
    numArray12[32 /*0x20*/] = (byte) 161;
    numArray12[41] = (byte) 40;
    numArray12[34] = (byte) 197;
    numArray12[35] = (byte) 94;
    numArray12[38] = (byte) 35;
    numArray12[25] = (byte) 175;
    numArray12[27] = (byte) 77;
    numArray12[39] = (byte) 191;
    numArray12[54] = (byte) 41;
    numArray12[49] = (byte) 151;
    numArray12[42] = (byte) 134;
    numArray12[9] = (byte) 198;
    numArray12[44] = (byte) 236;
    numArray12[45] = (byte) 153;
    numArray12[17] = (byte) 22;
    numArray12[40] = (byte) 102;
    numArray12[48 /*0x30*/] = (byte) 11;
    numArray12[24] = (byte) 223;
    numArray12[13] = (byte) 193;
    numArray12[8] = (byte) 52;
    numArray12[14] = (byte) 143;
    numArray12[6] = (byte) 68;
    numArray12[1] = (byte) 110;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[36]
    {
      (byte) 59,
      (byte) 65,
      (byte) 191,
      (byte) 214,
      (byte) 21,
      (byte) 13,
      (byte) 21,
      (byte) 123,
      (byte) 206,
      (byte) 249,
      (byte) 175,
      (byte) 201,
      (byte) 250,
      (byte) 187,
      (byte) 213,
      (byte) 115,
      (byte) 223,
      (byte) 86,
      (byte) 33,
      (byte) 142,
      (byte) 123,
      (byte) 215,
      (byte) 139,
      (byte) 100,
      (byte) 109,
      (byte) 107,
      (byte) 157,
      (byte) 25,
      (byte) 128 /*0x80*/,
      (byte) 96 /*0x60*/,
      (byte) 252,
      (byte) 40,
      (byte) 52,
      (byte) 234,
      (byte) 36,
      (byte) 58
    };
    byte[] numArray14 = new byte[36];
    numArray14[28] = (byte) 166;
    numArray14[1] = (byte) 215;
    numArray14[2] = (byte) 184;
    numArray14[3] = (byte) 11;
    numArray14[4] = (byte) 75;
    numArray14[5] = (byte) 201;
    numArray14[35] = (byte) 65;
    numArray14[7] = (byte) 180;
    numArray14[8] = (byte) 38;
    numArray14[29] = (byte) 155;
    numArray14[9] = (byte) 181;
    numArray14[11] = (byte) 164;
    numArray14[12] = (byte) 109;
    numArray14[31 /*0x1F*/] = (byte) 108;
    numArray14[14] = (byte) 68;
    numArray14[10] = (byte) 85;
    numArray14[16 /*0x10*/] = (byte) 183;
    numArray14[6] = (byte) 34;
    numArray14[18] = (byte) 121;
    numArray14[19] = (byte) 1;
    numArray14[33] = (byte) 45;
    numArray14[20] = (byte) 85;
    numArray14[22] = (byte) 28;
    numArray14[23] = (byte) 28;
    numArray14[24] = (byte) 200;
    numArray14[15] = (byte) 82;
    numArray14[26] = (byte) 246;
    numArray14[25] = (byte) 22;
    numArray14[21] = (byte) 199;
    numArray14[34] = (byte) 200;
    numArray14[30] = (byte) 48 /*0x30*/;
    numArray14[17] = (byte) 253;
    numArray14[27] = (byte) 203;
    numArray14[0] = (byte) 21;
    numArray14[32 /*0x20*/] = (byte) 72;
    numArray14[13] = (byte) 42;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 36);
    for (int index = 0; index < 36; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13410()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 60,
        (byte) 59,
        (byte) 236,
        (byte) 252,
        (byte) 23,
        (byte) 205,
        (byte) 145,
        (byte) 240 /*0xF0*/,
        (byte) 152,
        (byte) 230
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 47,
        (byte) 229,
        (byte) 100,
        (byte) 19,
        (byte) 14,
        (byte) 30,
        (byte) 7,
        (byte) 199,
        (byte) 183,
        (byte) 144 /*0x90*/
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
      (byte) 191,
      (byte) 131,
      (byte) 111,
      (byte) 131,
      (byte) 184,
      (byte) 210,
      (byte) 195,
      (byte) 15,
      (byte) 43,
      (byte) 82
    };
    byte[] numArray6 = new byte[10];
    numArray6[7] = (byte) 142;
    numArray6[1] = (byte) 152;
    numArray6[4] = (byte) 122;
    numArray6[3] = (byte) 43;
    numArray6[6] = (byte) 114;
    numArray6[5] = (byte) 205;
    numArray6[2] = (byte) 234;
    numArray6[0] = (byte) 231;
    numArray6[8] = (byte) 225;
    numArray6[9] = (byte) 53;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13411()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[44];
      byte[] numArray2 = new byte[44]
      {
        (byte) 108,
        (byte) 146,
        (byte) 162,
        (byte) 106,
        (byte) 10,
        (byte) 214,
        (byte) 23,
        (byte) 197,
        (byte) 7,
        (byte) 253,
        (byte) 216,
        (byte) 23,
        (byte) 57,
        (byte) 112 /*0x70*/,
        (byte) 59,
        (byte) 11,
        (byte) 12,
        (byte) 98,
        (byte) 16 /*0x10*/,
        (byte) 232,
        (byte) 250,
        (byte) 33,
        (byte) 211,
        (byte) 148,
        (byte) 166,
        (byte) 8,
        (byte) 195,
        (byte) 195,
        (byte) 56,
        (byte) 181,
        (byte) 171,
        (byte) 151,
        (byte) 9,
        (byte) 0,
        (byte) 23,
        (byte) 38,
        (byte) 48 /*0x30*/,
        (byte) 88,
        (byte) 5,
        (byte) 85,
        (byte) 59,
        (byte) 43,
        (byte) 212,
        (byte) 70
      };
      byte[] numArray3 = new byte[44]
      {
        (byte) 194,
        (byte) 46,
        (byte) 125,
        (byte) 237,
        (byte) 237,
        (byte) 252,
        (byte) 109,
        (byte) 17,
        (byte) 113,
        (byte) 180,
        (byte) 161,
        (byte) 159,
        (byte) 46,
        (byte) 5,
        (byte) 112 /*0x70*/,
        (byte) 7,
        (byte) 229,
        (byte) 94,
        (byte) 78,
        (byte) 136,
        (byte) 169,
        (byte) 224 /*0xE0*/,
        (byte) 199,
        (byte) 159,
        (byte) 142,
        (byte) 136,
        (byte) 226,
        (byte) 111,
        (byte) 194,
        (byte) 32 /*0x20*/,
        (byte) 168,
        (byte) 213,
        (byte) 47,
        (byte) 153,
        (byte) 239,
        (byte) 57,
        (byte) 199,
        (byte) 66,
        (byte) 167,
        (byte) 87,
        (byte) 219,
        (byte) 249,
        (byte) 37,
        (byte) 21
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[39];
      byte[] response = new byte[39];
      Array.Copy((Array) sc_13393.sspq, 294, (Array) numArray4, 0, 39);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 294, (Array) numArray4, 0, 39);
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
      (byte) 119,
      (byte) 140,
      (byte) 204,
      (byte) 104,
      (byte) 33,
      (byte) 148,
      (byte) 3,
      (byte) 67,
      (byte) 231,
      (byte) 158,
      (byte) 130,
      (byte) 243,
      (byte) 32 /*0x20*/,
      (byte) 196,
      (byte) 116,
      (byte) 11,
      (byte) 211,
      (byte) 161,
      (byte) 24,
      (byte) 166,
      (byte) 91,
      (byte) 228,
      (byte) 165,
      (byte) 254,
      (byte) 48 /*0x30*/,
      (byte) 217,
      (byte) 240 /*0xF0*/,
      (byte) 166,
      (byte) 180,
      (byte) 119,
      (byte) 208 /*0xD0*/,
      (byte) 105,
      (byte) 171,
      (byte) 191,
      (byte) 151,
      (byte) 172,
      (byte) 140,
      (byte) 172,
      (byte) 185,
      (byte) 30,
      (byte) 89,
      (byte) 120,
      (byte) 197,
      (byte) 48 /*0x30*/
    };
    byte[] numArray7 = new byte[44];
    numArray7[26] = (byte) 115;
    numArray7[1] = (byte) 195;
    numArray7[23] = (byte) 66;
    numArray7[40] = (byte) 197;
    numArray7[29] = (byte) 72;
    numArray7[5] = (byte) 147;
    numArray7[21] = (byte) 153;
    numArray7[3] = (byte) 11;
    numArray7[8] = (byte) 12;
    numArray7[9] = (byte) 188;
    numArray7[14] = (byte) 81;
    numArray7[11] = (byte) 8;
    numArray7[39] = (byte) 200;
    numArray7[13] = (byte) 71;
    numArray7[42] = (byte) 176 /*0xB0*/;
    numArray7[15] = (byte) 114;
    numArray7[16 /*0x10*/] = (byte) 150;
    numArray7[17] = (byte) 239;
    numArray7[4] = (byte) 114;
    numArray7[19] = (byte) 207;
    numArray7[7] = (byte) 194;
    numArray7[33] = (byte) 174;
    numArray7[22] = (byte) 171;
    numArray7[27] = (byte) 83;
    numArray7[24] = (byte) 109;
    numArray7[25] = (byte) 73;
    numArray7[6] = (byte) 129;
    numArray7[43] = (byte) 95;
    numArray7[18] = (byte) 94;
    numArray7[32 /*0x20*/] = (byte) 6;
    numArray7[0] = (byte) 116;
    numArray7[30] = (byte) 175;
    numArray7[10] = (byte) 4;
    numArray7[38] = (byte) 228;
    numArray7[34] = (byte) 163;
    numArray7[35] = (byte) 148;
    numArray7[36] = (byte) 138;
    numArray7[37] = (byte) 71;
    numArray7[12] = (byte) 6;
    numArray7[28] = (byte) 230;
    numArray7[2] = (byte) 105;
    numArray7[41] = byte.MaxValue;
    numArray7[20] = (byte) 42;
    numArray7[31 /*0x1F*/] = (byte) 131;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 44);
    for (int index = 0; index < 44; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[36];
    byte[] response1 = new byte[36];
    Array.Copy((Array) sc_13393.sspq, 333, (Array) numArray8, 0, 36);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13393.sspr, 333, (Array) numArray8, 0, 36);
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

  internal static string ssp_appserver_13412()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 79,
        (byte) 250,
        (byte) 200,
        (byte) 19,
        (byte) 130,
        (byte) 69,
        (byte) 18,
        (byte) 1,
        (byte) 162,
        (byte) 140,
        (byte) 141,
        (byte) 130,
        (byte) 194,
        (byte) 245,
        (byte) 88,
        (byte) 143,
        (byte) 83,
        (byte) 36,
        (byte) 211,
        (byte) 239,
        (byte) 185,
        (byte) 97,
        (byte) 186
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 224 /*0xE0*/,
        (byte) 18,
        (byte) 84,
        (byte) 138,
        (byte) 23,
        (byte) 213,
        (byte) 0,
        (byte) 253,
        (byte) 0,
        (byte) 147,
        (byte) 243,
        (byte) 84,
        (byte) 190,
        (byte) 242,
        (byte) 11,
        (byte) 205,
        (byte) 223,
        (byte) 210,
        (byte) 1,
        (byte) 94,
        (byte) 172,
        (byte) 10,
        (byte) 187
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_13393.sspq, 369, (Array) numArray4, 0, 38);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 369, (Array) numArray4, 0, 38);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23]
    {
      (byte) 94,
      (byte) 195,
      (byte) 106,
      (byte) 144 /*0x90*/,
      (byte) 17,
      (byte) 227,
      (byte) 245,
      (byte) 11,
      (byte) 119,
      (byte) 127 /*0x7F*/,
      (byte) 249,
      (byte) 104,
      (byte) 25,
      (byte) 226,
      (byte) 179,
      (byte) 114,
      (byte) 9,
      (byte) 126,
      (byte) 29,
      (byte) 110,
      (byte) 119,
      (byte) 79,
      (byte) 56
    };
    byte[] numArray7 = new byte[23];
    numArray7[5] = (byte) 137;
    numArray7[16 /*0x10*/] = (byte) 29;
    numArray7[2] = (byte) 69;
    numArray7[3] = (byte) 136;
    numArray7[4] = (byte) 109;
    numArray7[14] = (byte) 137;
    numArray7[1] = (byte) 72;
    numArray7[13] = (byte) 107;
    numArray7[7] = (byte) 163;
    numArray7[9] = (byte) 2;
    numArray7[22] = (byte) 158;
    numArray7[12] = (byte) 80 /*0x50*/;
    numArray7[0] = (byte) 140;
    numArray7[19] = (byte) 220;
    numArray7[17] = (byte) 187;
    numArray7[15] = (byte) 85;
    numArray7[10] = (byte) 221;
    numArray7[21] = (byte) 157;
    numArray7[18] = (byte) 14;
    numArray7[6] = (byte) 212;
    numArray7[20] = (byte) 230;
    numArray7[8] = (byte) 160 /*0xA0*/;
    numArray7[11] = (byte) 57;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[50];
    byte[] response1 = new byte[50];
    Array.Copy((Array) sc_13393.sspq, 407, (Array) numArray8, 0, 50);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13393.sspr, 407, (Array) numArray8, 0, 50);
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

  internal static string ssp_appserver_13413()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[7] = (byte) 98;
      numArray2[5] = (byte) 133;
      numArray2[11] = (byte) 20;
      numArray2[3] = (byte) 192 /*0xC0*/;
      numArray2[12] = (byte) 37;
      numArray2[2] = (byte) 54;
      numArray2[14] = (byte) 1;
      numArray2[1] = (byte) 193;
      numArray2[8] = (byte) 26;
      numArray2[9] = (byte) 40;
      numArray2[10] = (byte) 89;
      numArray2[6] = (byte) 196;
      numArray2[4] = (byte) 151;
      numArray2[13] = (byte) 41;
      numArray2[0] = (byte) 53;
      numArray2[15] = (byte) 226;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 183,
        (byte) 212,
        (byte) 95,
        (byte) 226,
        (byte) 62,
        (byte) 168,
        (byte) 108,
        (byte) 95,
        (byte) 227,
        (byte) 35,
        (byte) 18,
        (byte) 89,
        (byte) 60,
        (byte) 196,
        (byte) 63 /*0x3F*/,
        (byte) 196
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
      (byte) 231,
      (byte) 71,
      (byte) 39,
      (byte) 167,
      (byte) 29,
      (byte) 133,
      (byte) 247,
      (byte) 13,
      (byte) 219,
      (byte) 238,
      (byte) 102,
      (byte) 217,
      (byte) 40,
      (byte) 11,
      (byte) 118,
      (byte) 238
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 225,
      (byte) 63 /*0x3F*/,
      (byte) 120,
      (byte) 206,
      (byte) 100,
      (byte) 120,
      (byte) 203,
      (byte) 67,
      (byte) 187,
      (byte) 233,
      (byte) 89,
      (byte) 245,
      (byte) 207,
      (byte) 113,
      (byte) 10,
      (byte) 102
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13414()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[166];
      byte[] numArray2 = new byte[55]
      {
        (byte) 108,
        (byte) 248,
        (byte) 157,
        (byte) 28,
        (byte) 166,
        (byte) 182,
        (byte) 105,
        (byte) 5,
        (byte) 126,
        (byte) 252,
        (byte) 247,
        (byte) 152,
        (byte) 193,
        (byte) 127 /*0x7F*/,
        (byte) 25,
        (byte) 154,
        (byte) 52,
        (byte) 198,
        (byte) 116,
        (byte) 180,
        (byte) 163,
        (byte) 188,
        (byte) 237,
        (byte) 171,
        (byte) 61,
        (byte) 23,
        (byte) 165,
        (byte) 80 /*0x50*/,
        (byte) 230,
        (byte) 172,
        (byte) 225,
        (byte) 35,
        (byte) 108,
        (byte) 114,
        (byte) 61,
        (byte) 238,
        (byte) 170,
        (byte) 11,
        (byte) 1,
        (byte) 102,
        (byte) 28,
        (byte) 242,
        (byte) 142,
        (byte) 223,
        (byte) 246,
        (byte) 206,
        (byte) 24,
        (byte) 169,
        (byte) 241,
        (byte) 220,
        (byte) 185,
        (byte) 132,
        (byte) 201,
        (byte) 91,
        (byte) 78
      };
      byte[] numArray3 = new byte[55];
      numArray3[37] = (byte) 243;
      numArray3[1] = (byte) 175;
      numArray3[2] = (byte) 14;
      numArray3[38] = (byte) 64 /*0x40*/;
      numArray3[31 /*0x1F*/] = (byte) 8;
      numArray3[46] = (byte) 195;
      numArray3[35] = (byte) 43;
      numArray3[14] = (byte) 140;
      numArray3[8] = (byte) 165;
      numArray3[9] = (byte) 92;
      numArray3[10] = (byte) 169;
      numArray3[0] = (byte) 86;
      numArray3[12] = (byte) 66;
      numArray3[39] = (byte) 20;
      numArray3[43] = (byte) 190;
      numArray3[29] = (byte) 93;
      numArray3[52] = (byte) 23;
      numArray3[17] = (byte) 16 /*0x10*/;
      numArray3[5] = (byte) 196;
      numArray3[19] = (byte) 221;
      numArray3[6] = (byte) 170;
      numArray3[21] = (byte) 40;
      numArray3[51] = (byte) 222;
      numArray3[23] = (byte) 6;
      numArray3[24] = (byte) 16 /*0x10*/;
      numArray3[25] = (byte) 207;
      numArray3[26] = (byte) 58;
      numArray3[4] = (byte) 132;
      numArray3[28] = (byte) 83;
      numArray3[16 /*0x10*/] = (byte) 34;
      numArray3[36] = (byte) 130;
      numArray3[22] = (byte) 28;
      numArray3[27] = (byte) 34;
      numArray3[33] = (byte) 89;
      numArray3[53] = (byte) 204;
      numArray3[20] = (byte) 78;
      numArray3[3] = (byte) 213;
      numArray3[15] = (byte) 74;
      numArray3[32 /*0x20*/] = (byte) 219;
      numArray3[13] = (byte) 68;
      numArray3[50] = (byte) 251;
      numArray3[18] = (byte) 137;
      numArray3[42] = (byte) 226;
      numArray3[41] = (byte) 164;
      numArray3[44] = (byte) 209;
      numArray3[45] = (byte) 105;
      numArray3[7] = (byte) 189;
      numArray3[47] = (byte) 147;
      numArray3[48 /*0x30*/] = (byte) 213;
      numArray3[49] = (byte) 107;
      numArray3[30] = (byte) 126;
      numArray3[34] = (byte) 142;
      numArray3[40] = (byte) 219;
      numArray3[11] = (byte) 230;
      numArray3[54] = (byte) 78;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 61,
        (byte) 111,
        (byte) 144 /*0x90*/,
        (byte) 20,
        (byte) 186,
        (byte) 21,
        (byte) 223,
        (byte) 190,
        (byte) 122,
        (byte) 27,
        (byte) 43,
        (byte) 179,
        (byte) 70,
        (byte) 54,
        (byte) 251,
        (byte) 82,
        (byte) 183,
        (byte) 149,
        (byte) 222,
        (byte) 114,
        (byte) 73,
        (byte) 145,
        (byte) 204,
        (byte) 53,
        (byte) 147,
        (byte) 152,
        (byte) 29,
        (byte) 97,
        (byte) 157,
        (byte) 156,
        (byte) 77,
        (byte) 134,
        (byte) 11,
        (byte) 231,
        (byte) 123,
        (byte) 189,
        (byte) 226,
        (byte) 65,
        (byte) 72,
        (byte) 189,
        (byte) 220,
        (byte) 5,
        (byte) 167,
        (byte) 135,
        (byte) 158,
        (byte) 180,
        (byte) 127 /*0x7F*/,
        (byte) 181,
        (byte) 148,
        (byte) 108,
        (byte) 4,
        (byte) 46,
        (byte) 190,
        (byte) 16 /*0x10*/,
        (byte) 178
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 248,
        (byte) 95,
        (byte) 92,
        (byte) 155,
        (byte) 50,
        (byte) 184,
        (byte) 87,
        (byte) 254,
        (byte) 60,
        (byte) 106,
        (byte) 83,
        (byte) 75,
        (byte) 166,
        (byte) 193,
        (byte) 137,
        (byte) 104,
        (byte) 219,
        (byte) 72,
        (byte) 143,
        (byte) 139,
        (byte) 168,
        (byte) 123,
        (byte) 59,
        (byte) 158,
        (byte) 43,
        (byte) 250,
        (byte) 40,
        (byte) 84,
        (byte) 218,
        (byte) 44,
        (byte) 130,
        (byte) 84,
        (byte) 25,
        (byte) 64 /*0x40*/,
        (byte) 199,
        (byte) 59,
        (byte) 42,
        (byte) 40,
        (byte) 200,
        (byte) 113,
        (byte) 73,
        (byte) 29,
        (byte) 204,
        (byte) 50,
        (byte) 23,
        (byte) 158,
        (byte) 147,
        (byte) 159,
        (byte) 130,
        (byte) 123,
        (byte) 98,
        (byte) 13,
        (byte) 41,
        (byte) 118,
        (byte) 227
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 91,
        (byte) 128 /*0x80*/,
        (byte) 34,
        (byte) 248,
        (byte) 125,
        (byte) 33,
        (byte) 222,
        (byte) 205,
        (byte) 164,
        (byte) 168,
        (byte) 95,
        (byte) 186,
        (byte) 62,
        (byte) 112 /*0x70*/,
        (byte) 118,
        (byte) 76,
        (byte) 33,
        (byte) 149,
        (byte) 68,
        (byte) 234,
        (byte) 68,
        (byte) 150,
        (byte) 223,
        (byte) 25,
        (byte) 103,
        (byte) 204,
        (byte) 48 /*0x30*/,
        (byte) 152,
        (byte) 30,
        (byte) 200,
        (byte) 251,
        (byte) 52,
        (byte) 208 /*0xD0*/,
        (byte) 26,
        (byte) 135,
        (byte) 192 /*0xC0*/,
        (byte) 38,
        (byte) 197,
        (byte) 94,
        (byte) 228,
        (byte) 201,
        (byte) 185,
        (byte) 249,
        (byte) 12,
        (byte) 31 /*0x1F*/,
        (byte) 250,
        (byte) 86,
        (byte) 251,
        (byte) 142,
        (byte) 225,
        (byte) 156,
        (byte) 122,
        (byte) 110,
        (byte) 193,
        (byte) 94
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 195,
        (byte) 91,
        (byte) 93,
        (byte) 163,
        (byte) 202,
        (byte) 184,
        (byte) 40,
        (byte) 12,
        (byte) 181,
        (byte) 92,
        (byte) 111,
        (byte) 6,
        (byte) 120,
        (byte) 227,
        (byte) 57,
        (byte) 213,
        (byte) 159,
        (byte) 126,
        (byte) 250,
        (byte) 183,
        (byte) 146,
        (byte) 196,
        (byte) 208 /*0xD0*/,
        (byte) 154,
        (byte) 59,
        (byte) 248,
        (byte) 27,
        (byte) 152,
        (byte) 107,
        (byte) 80 /*0x50*/,
        (byte) 88,
        (byte) 60,
        (byte) 16 /*0x10*/,
        (byte) 188,
        (byte) 11,
        (byte) 187,
        (byte) 25,
        (byte) 20,
        (byte) 69,
        (byte) 180,
        (byte) 250,
        (byte) 99,
        (byte) 206,
        (byte) 134,
        (byte) 226,
        (byte) 101,
        (byte) 102,
        (byte) 241,
        (byte) 10,
        (byte) 97,
        (byte) 16 /*0x10*/,
        (byte) 100,
        (byte) 68,
        (byte) 38,
        (byte) 103
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[1]{ (byte) 8 };
      byte[] numArray9 = new byte[1]{ (byte) 17 };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[166];
    byte[] numArray11 = new byte[55]
    {
      (byte) 69,
      (byte) 175,
      (byte) 220,
      (byte) 173,
      (byte) 43,
      (byte) 234,
      (byte) 35,
      (byte) 233,
      (byte) 181,
      (byte) 38,
      (byte) 92,
      (byte) 55,
      (byte) 96 /*0x60*/,
      (byte) 32 /*0x20*/,
      (byte) 139,
      (byte) 157,
      (byte) 210,
      (byte) 249,
      (byte) 108,
      (byte) 117,
      (byte) 53,
      (byte) 9,
      (byte) 60,
      (byte) 44,
      (byte) 128 /*0x80*/,
      (byte) 197,
      (byte) 15,
      (byte) 188,
      (byte) 194,
      (byte) 31 /*0x1F*/,
      (byte) 29,
      (byte) 52,
      (byte) 173,
      (byte) 243,
      (byte) 131,
      (byte) 10,
      (byte) 28,
      (byte) 95,
      (byte) 244,
      (byte) 16 /*0x10*/,
      (byte) 51,
      (byte) 18,
      (byte) 216,
      (byte) 187,
      (byte) 63 /*0x3F*/,
      (byte) 66,
      (byte) 14,
      (byte) 160 /*0xA0*/,
      (byte) 211,
      (byte) 38,
      (byte) 206,
      (byte) 15,
      (byte) 35,
      (byte) 59,
      (byte) 187
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 56,
      (byte) 58,
      (byte) 240 /*0xF0*/,
      (byte) 144 /*0x90*/,
      (byte) 70,
      (byte) 22,
      (byte) 203,
      (byte) 4,
      (byte) 30,
      (byte) 134,
      (byte) 110,
      (byte) 202,
      (byte) 161,
      (byte) 178,
      (byte) 164,
      (byte) 7,
      (byte) 15,
      (byte) 86,
      (byte) 94,
      (byte) 245,
      (byte) 20,
      (byte) 149,
      (byte) 152,
      (byte) 130,
      (byte) 150,
      (byte) 53,
      (byte) 60,
      (byte) 195,
      (byte) 90,
      (byte) 2,
      (byte) 204,
      (byte) 28,
      (byte) 8,
      (byte) 67,
      (byte) 123,
      (byte) 28,
      (byte) 179,
      (byte) 214,
      (byte) 98,
      (byte) 176 /*0xB0*/,
      (byte) 55,
      (byte) 235,
      (byte) 212,
      (byte) 93,
      (byte) 189,
      (byte) 234,
      (byte) 162,
      (byte) 65,
      (byte) 168,
      (byte) 166,
      (byte) 80 /*0x50*/,
      (byte) 61,
      (byte) 159,
      (byte) 127 /*0x7F*/,
      (byte) 40
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 176 /*0xB0*/,
      (byte) 44,
      (byte) 101,
      (byte) 219,
      (byte) 103,
      (byte) 38,
      (byte) 150,
      (byte) 8,
      (byte) 40,
      (byte) 138,
      (byte) 108,
      (byte) 156,
      (byte) 128 /*0x80*/,
      (byte) 128 /*0x80*/,
      (byte) 115,
      (byte) 4,
      (byte) 92,
      (byte) 66,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 128 /*0x80*/,
      (byte) 236,
      (byte) 61,
      (byte) 183,
      (byte) 110,
      (byte) 232,
      (byte) 156,
      (byte) 80 /*0x50*/,
      (byte) 14,
      (byte) 16 /*0x10*/,
      (byte) 113,
      (byte) 56,
      (byte) 71,
      (byte) 151,
      (byte) 108,
      (byte) 50,
      (byte) 143,
      (byte) 52,
      (byte) 204,
      (byte) 236,
      (byte) 66,
      (byte) 179,
      (byte) 23,
      (byte) 184,
      (byte) 162,
      (byte) 3,
      (byte) 82,
      (byte) 165,
      (byte) 90,
      (byte) 10,
      (byte) 199,
      (byte) 248,
      (byte) 152,
      (byte) 11,
      (byte) 241
    };
    byte[] numArray14 = new byte[55];
    numArray14[10] = (byte) 148;
    numArray14[1] = (byte) 227;
    numArray14[29] = (byte) 51;
    numArray14[41] = (byte) 0;
    numArray14[4] = (byte) 29;
    numArray14[5] = (byte) 43;
    numArray14[47] = (byte) 242;
    numArray14[7] = (byte) 71;
    numArray14[8] = (byte) 243;
    numArray14[51] = (byte) 44;
    numArray14[53] = (byte) 218;
    numArray14[3] = (byte) 92;
    numArray14[12] = (byte) 240 /*0xF0*/;
    numArray14[13] = (byte) 41;
    numArray14[14] = (byte) 143;
    numArray14[15] = (byte) 172;
    numArray14[40] = (byte) 120;
    numArray14[17] = (byte) 248;
    numArray14[31 /*0x1F*/] = (byte) 124;
    numArray14[46] = (byte) 72;
    numArray14[19] = (byte) 99;
    numArray14[26] = (byte) 192 /*0xC0*/;
    numArray14[39] = (byte) 150;
    numArray14[23] = (byte) 216;
    numArray14[18] = (byte) 229;
    numArray14[25] = (byte) 126;
    numArray14[22] = (byte) 186;
    numArray14[27] = (byte) 245;
    numArray14[28] = (byte) 102;
    numArray14[42] = (byte) 166;
    numArray14[30] = (byte) 109;
    numArray14[11] = (byte) 230;
    numArray14[37] = (byte) 24;
    numArray14[21] = (byte) 145;
    numArray14[34] = (byte) 158;
    numArray14[16 /*0x10*/] = (byte) 113;
    numArray14[36] = (byte) 172;
    numArray14[24] = (byte) 44;
    numArray14[38] = (byte) 94;
    numArray14[45] = (byte) 97;
    numArray14[35] = (byte) 78;
    numArray14[52] = (byte) 250;
    numArray14[6] = (byte) 76;
    numArray14[43] = (byte) 52;
    numArray14[33] = (byte) 251;
    numArray14[32 /*0x20*/] = (byte) 2;
    numArray14[48 /*0x30*/] = (byte) 117;
    numArray14[2] = (byte) 90;
    numArray14[9] = (byte) 14;
    numArray14[49] = (byte) 202;
    numArray14[50] = (byte) 253;
    numArray14[20] = (byte) 21;
    numArray14[0] = (byte) 110;
    numArray14[44] = byte.MaxValue;
    numArray14[54] = (byte) 146;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[29] = (byte) 61;
    numArray15[37] = (byte) 207;
    numArray15[2] = (byte) 232;
    numArray15[3] = (byte) 146;
    numArray15[4] = (byte) 79;
    numArray15[5] = (byte) 246;
    numArray15[41] = (byte) 0;
    numArray15[21] = (byte) 166;
    numArray15[38] = (byte) 173;
    numArray15[52] = (byte) 14;
    numArray15[25] = (byte) 220;
    numArray15[15] = (byte) 41;
    numArray15[12] = (byte) 252;
    numArray15[13] = (byte) 148;
    numArray15[14] = (byte) 95;
    numArray15[42] = (byte) 218;
    numArray15[16 /*0x10*/] = (byte) 211;
    numArray15[18] = (byte) 20;
    numArray15[9] = (byte) 25;
    numArray15[19] = (byte) 212;
    numArray15[20] = (byte) 229;
    numArray15[34] = (byte) 72;
    numArray15[26] = (byte) 111;
    numArray15[17] = (byte) 241;
    numArray15[27] = (byte) 169;
    numArray15[24] = (byte) 101;
    numArray15[50] = (byte) 142;
    numArray15[30] = (byte) 30;
    numArray15[28] = (byte) 246;
    numArray15[43] = (byte) 148;
    numArray15[48 /*0x30*/] = (byte) 63 /*0x3F*/;
    numArray15[31 /*0x1F*/] = (byte) 130;
    numArray15[32 /*0x20*/] = (byte) 187;
    numArray15[33] = (byte) 218;
    numArray15[7] = (byte) 159;
    numArray15[35] = (byte) 198;
    numArray15[8] = (byte) 147;
    numArray15[36] = (byte) 100;
    numArray15[11] = (byte) 106;
    numArray15[22] = (byte) 188;
    numArray15[40] = (byte) 49;
    numArray15[54] = (byte) 132;
    numArray15[39] = (byte) 194;
    numArray15[23] = (byte) 44;
    numArray15[44] = (byte) 39;
    numArray15[45] = (byte) 38;
    numArray15[46] = (byte) 29;
    numArray15[47] = (byte) 0;
    numArray15[49] = (byte) 89;
    numArray15[53] = (byte) 25;
    numArray15[0] = (byte) 153;
    numArray15[51] = (byte) 43;
    numArray15[10] = (byte) 66;
    numArray15[1] = (byte) 1;
    numArray15[6] = (byte) 206;
    byte[] numArray16 = new byte[55]
    {
      (byte) 178,
      (byte) 71,
      (byte) 207,
      (byte) 74,
      (byte) 95,
      (byte) 175,
      (byte) 214,
      (byte) 236,
      (byte) 10,
      (byte) 0,
      (byte) 128 /*0x80*/,
      (byte) 99,
      (byte) 188,
      (byte) 174,
      (byte) 65,
      (byte) 205,
      (byte) 138,
      (byte) 55,
      (byte) 20,
      (byte) 230,
      (byte) 7,
      (byte) 34,
      (byte) 71,
      (byte) 189,
      (byte) 149,
      (byte) 168,
      (byte) 225,
      (byte) 224 /*0xE0*/,
      (byte) 163,
      (byte) 145,
      (byte) 56,
      (byte) 219,
      (byte) 251,
      (byte) 52,
      (byte) 192 /*0xC0*/,
      (byte) 137,
      (byte) 178,
      (byte) 123,
      (byte) 163,
      (byte) 52,
      (byte) 168,
      (byte) 189,
      (byte) 188,
      (byte) 158,
      (byte) 37,
      (byte) 82,
      (byte) 170,
      (byte) 85,
      (byte) 153,
      (byte) 11,
      (byte) 86,
      (byte) 171,
      (byte) 235,
      (byte) 227,
      (byte) 122
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[1]{ (byte) 182 };
    byte[] numArray18 = new byte[1]{ (byte) 231 };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 1);
    for (int index = 0; index < 1; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_13415(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[22] = (byte) 183;
    sourceArray1[1] = (byte) 76;
    sourceArray1[2] = (byte) 13;
    sourceArray1[27] = (byte) 242;
    sourceArray1[4] = (byte) 246;
    sourceArray1[5] = (byte) 24;
    sourceArray1[10] = (byte) 149;
    sourceArray1[30] = (byte) 34;
    sourceArray1[38] = (byte) 189;
    sourceArray1[7] = (byte) 98;
    sourceArray1[25] = (byte) 45;
    sourceArray1[11] = (byte) 140;
    sourceArray1[12] = (byte) 71;
    sourceArray1[8] = (byte) 10;
    sourceArray1[0] = (byte) 7;
    sourceArray1[43] = (byte) 165;
    sourceArray1[9] = (byte) 23;
    sourceArray1[17] = (byte) 186;
    sourceArray1[40] = (byte) 122;
    sourceArray1[19] = (byte) 7;
    sourceArray1[20] = (byte) 35;
    sourceArray1[47] = (byte) 140;
    sourceArray1[34] = (byte) 145;
    sourceArray1[23] = (byte) 43;
    sourceArray1[24] = (byte) 182;
    sourceArray1[41] = (byte) 40;
    sourceArray1[26] = (byte) 100;
    sourceArray1[13] = (byte) 143;
    sourceArray1[28] = (byte) 179;
    sourceArray1[37] = (byte) 49;
    sourceArray1[6] = (byte) 27;
    sourceArray1[31 /*0x1F*/] = (byte) 82;
    sourceArray1[32 /*0x20*/] = (byte) 76;
    sourceArray1[16 /*0x10*/] = (byte) 194;
    sourceArray1[18] = (byte) 195;
    sourceArray1[35] = (byte) 26;
    sourceArray1[36] = (byte) 105;
    sourceArray1[33] = (byte) 219;
    sourceArray1[3] = (byte) 202;
    sourceArray1[39] = (byte) 141;
    sourceArray1[14] = (byte) 157;
    sourceArray1[21] = (byte) 13;
    sourceArray1[29] = (byte) 133;
    sourceArray1[15] = (byte) 199;
    sourceArray1[44] = (byte) 224 /*0xE0*/;
    sourceArray1[45] = (byte) 19;
    sourceArray1[46] = (byte) 224 /*0xE0*/;
    sourceArray1[42] = (byte) 219;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 103,
      (byte) 162,
      (byte) 16 /*0x10*/,
      (byte) 132,
      (byte) 140,
      (byte) 118,
      (byte) 0,
      (byte) 82,
      (byte) 66,
      (byte) 178,
      (byte) 225,
      (byte) 55,
      (byte) 43,
      (byte) 243,
      (byte) 31 /*0x1F*/,
      (byte) 42,
      (byte) 228,
      (byte) 202,
      (byte) 79,
      (byte) 163,
      (byte) 198,
      (byte) 245,
      (byte) 97,
      (byte) 53,
      (byte) 174,
      (byte) 163,
      (byte) 168,
      (byte) 181,
      (byte) 245,
      (byte) 109,
      (byte) 124,
      (byte) 204,
      (byte) 159,
      (byte) 71,
      (byte) 183,
      (byte) 221,
      (byte) 134,
      (byte) 165,
      (byte) 224 /*0xE0*/,
      (byte) 64 /*0x40*/,
      (byte) 178,
      (byte) 162,
      (byte) 210,
      (byte) 188,
      (byte) 131,
      (byte) 174,
      (byte) 53
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13416()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[241];
      byte[] numArray2 = new byte[55];
      numArray2[51] = (byte) 86;
      numArray2[47] = (byte) 173;
      numArray2[0] = (byte) 211;
      numArray2[29] = (byte) 60;
      numArray2[10] = (byte) 200;
      numArray2[22] = (byte) 22;
      numArray2[13] = (byte) 112 /*0x70*/;
      numArray2[3] = (byte) 149;
      numArray2[8] = (byte) 117;
      numArray2[36] = (byte) 73;
      numArray2[34] = (byte) 208 /*0xD0*/;
      numArray2[40] = (byte) 98;
      numArray2[12] = (byte) 152;
      numArray2[44] = (byte) 206;
      numArray2[14] = (byte) 123;
      numArray2[4] = (byte) 202;
      numArray2[16 /*0x10*/] = (byte) 79;
      numArray2[17] = (byte) 35;
      numArray2[18] = (byte) 243;
      numArray2[38] = (byte) 193;
      numArray2[26] = (byte) 114;
      numArray2[19] = (byte) 48 /*0x30*/;
      numArray2[15] = (byte) 219;
      numArray2[23] = (byte) 102;
      numArray2[11] = (byte) 131;
      numArray2[31 /*0x1F*/] = (byte) 219;
      numArray2[7] = (byte) 193;
      numArray2[27] = (byte) 7;
      numArray2[2] = (byte) 142;
      numArray2[41] = (byte) 154;
      numArray2[24] = (byte) 254;
      numArray2[21] = (byte) 247;
      numArray2[32 /*0x20*/] = (byte) 97;
      numArray2[33] = (byte) 172;
      numArray2[30] = (byte) 128 /*0x80*/;
      numArray2[35] = (byte) 218;
      numArray2[6] = (byte) 97;
      numArray2[37] = (byte) 67;
      numArray2[20] = (byte) 231;
      numArray2[25] = (byte) 214;
      numArray2[49] = (byte) 254;
      numArray2[28] = (byte) 91;
      numArray2[42] = (byte) 211;
      numArray2[43] = (byte) 203;
      numArray2[1] = (byte) 115;
      numArray2[45] = (byte) 1;
      numArray2[5] = (byte) 98;
      numArray2[46] = (byte) 238;
      numArray2[48 /*0x30*/] = (byte) 121;
      numArray2[9] = (byte) 138;
      numArray2[50] = (byte) 1;
      numArray2[39] = (byte) 48 /*0x30*/;
      numArray2[52] = (byte) 99;
      numArray2[53] = (byte) 127 /*0x7F*/;
      numArray2[54] = (byte) 183;
      byte[] numArray3 = new byte[55]
      {
        (byte) 15,
        (byte) 4,
        (byte) 198,
        (byte) 134,
        (byte) 79,
        (byte) 124,
        (byte) 216,
        (byte) 176 /*0xB0*/,
        (byte) 80 /*0x50*/,
        (byte) 54,
        (byte) 132,
        (byte) 211,
        (byte) 217,
        (byte) 66,
        (byte) 175,
        (byte) 242,
        (byte) 180,
        (byte) 106,
        (byte) 120,
        byte.MaxValue,
        (byte) 45,
        (byte) 153,
        (byte) 240 /*0xF0*/,
        (byte) 153,
        (byte) 154,
        (byte) 49,
        (byte) 134,
        (byte) 183,
        (byte) 223,
        (byte) 254,
        (byte) 15,
        (byte) 201,
        (byte) 229,
        (byte) 102,
        (byte) 47,
        (byte) 14,
        (byte) 22,
        (byte) 154,
        (byte) 248,
        (byte) 197,
        (byte) 66,
        (byte) 193,
        (byte) 120,
        (byte) 108,
        (byte) 252,
        (byte) 122,
        (byte) 192 /*0xC0*/,
        (byte) 80 /*0x50*/,
        (byte) 62,
        (byte) 137,
        (byte) 32 /*0x20*/,
        (byte) 186,
        (byte) 195,
        (byte) 15,
        (byte) 216
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 145,
        (byte) 154,
        (byte) 10,
        (byte) 42,
        (byte) 204,
        (byte) 8,
        (byte) 254,
        (byte) 223,
        (byte) 117,
        (byte) 64 /*0x40*/,
        (byte) 246,
        (byte) 170,
        (byte) 36,
        (byte) 112 /*0x70*/,
        (byte) 95,
        (byte) 148,
        (byte) 130,
        (byte) 197,
        (byte) 24,
        (byte) 138,
        (byte) 137,
        (byte) 182,
        (byte) 29,
        (byte) 230,
        (byte) 182,
        (byte) 180,
        (byte) 239,
        (byte) 69,
        (byte) 54,
        (byte) 216,
        (byte) 47,
        (byte) 138,
        (byte) 61,
        (byte) 106,
        (byte) 143,
        (byte) 18,
        (byte) 195,
        (byte) 21,
        (byte) 59,
        (byte) 43,
        (byte) 154,
        (byte) 226,
        (byte) 107,
        (byte) 159,
        (byte) 11,
        (byte) 99,
        (byte) 180,
        (byte) 252,
        (byte) 149,
        (byte) 95,
        (byte) 29,
        (byte) 235,
        (byte) 82,
        (byte) 44,
        (byte) 137
      };
      byte[] numArray5 = new byte[55];
      numArray5[22] = (byte) 111;
      numArray5[31 /*0x1F*/] = (byte) 253;
      numArray5[2] = (byte) 37;
      numArray5[3] = (byte) 172;
      numArray5[20] = (byte) 99;
      numArray5[45] = (byte) 47;
      numArray5[50] = (byte) 174;
      numArray5[7] = (byte) 192 /*0xC0*/;
      numArray5[16 /*0x10*/] = (byte) 49;
      numArray5[9] = (byte) 76;
      numArray5[54] = (byte) 20;
      numArray5[11] = (byte) 223;
      numArray5[42] = (byte) 54;
      numArray5[13] = (byte) 229;
      numArray5[23] = (byte) 249;
      numArray5[14] = (byte) 147;
      numArray5[32 /*0x20*/] = (byte) 249;
      numArray5[47] = (byte) 245;
      numArray5[18] = (byte) 235;
      numArray5[19] = (byte) 147;
      numArray5[34] = (byte) 100;
      numArray5[21] = (byte) 211;
      numArray5[53] = (byte) 192 /*0xC0*/;
      numArray5[0] = (byte) 155;
      numArray5[17] = (byte) 77;
      numArray5[5] = (byte) 87;
      numArray5[1] = (byte) 142;
      numArray5[27] = (byte) 122;
      numArray5[24] = (byte) 123;
      numArray5[29] = (byte) 168;
      numArray5[30] = (byte) 56;
      numArray5[38] = (byte) 54;
      numArray5[25] = (byte) 3;
      numArray5[33] = (byte) 28;
      numArray5[28] = (byte) 198;
      numArray5[4] = (byte) 124;
      numArray5[36] = (byte) 192 /*0xC0*/;
      numArray5[37] = (byte) 70;
      numArray5[6] = (byte) 215;
      numArray5[39] = (byte) 64 /*0x40*/;
      numArray5[40] = (byte) 208 /*0xD0*/;
      numArray5[41] = (byte) 139;
      numArray5[43] = (byte) 89;
      numArray5[15] = (byte) 79;
      numArray5[44] = (byte) 12;
      numArray5[51] = (byte) 90;
      numArray5[46] = (byte) 94;
      numArray5[26] = (byte) 121;
      numArray5[48 /*0x30*/] = byte.MaxValue;
      numArray5[49] = (byte) 151;
      numArray5[8] = (byte) 66;
      numArray5[35] = (byte) 110;
      numArray5[52] = (byte) 106;
      numArray5[10] = (byte) 0;
      numArray5[12] = (byte) 97;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 74,
        (byte) 32 /*0x20*/,
        (byte) 25,
        (byte) 38,
        (byte) 9,
        (byte) 210,
        (byte) 225,
        (byte) 204,
        (byte) 51,
        (byte) 92,
        (byte) 217,
        (byte) 44,
        (byte) 184,
        (byte) 50,
        (byte) 203,
        (byte) 104,
        (byte) 217,
        (byte) 13,
        (byte) 109,
        (byte) 123,
        (byte) 240 /*0xF0*/,
        (byte) 59,
        (byte) 92,
        (byte) 4,
        (byte) 215,
        (byte) 103,
        (byte) 71,
        (byte) 11,
        (byte) 226,
        (byte) 210,
        (byte) 211,
        (byte) 254,
        (byte) 35,
        (byte) 150,
        (byte) 84,
        (byte) 129,
        (byte) 9,
        (byte) 236,
        (byte) 216,
        (byte) 102,
        (byte) 236,
        (byte) 193,
        (byte) 108,
        (byte) 153,
        (byte) 33,
        (byte) 238,
        (byte) 144 /*0x90*/,
        (byte) 92,
        (byte) 228,
        (byte) 248,
        (byte) 140,
        (byte) 47,
        (byte) 116,
        (byte) 21,
        (byte) 14
      };
      byte[] numArray7 = new byte[55];
      numArray7[41] = (byte) 88;
      numArray7[54] = (byte) 106;
      numArray7[37] = (byte) 250;
      numArray7[33] = (byte) 44;
      numArray7[12] = (byte) 40;
      numArray7[7] = (byte) 175;
      numArray7[16 /*0x10*/] = (byte) 59;
      numArray7[2] = (byte) 91;
      numArray7[8] = (byte) 191;
      numArray7[36] = (byte) 198;
      numArray7[19] = (byte) 183;
      numArray7[18] = (byte) 218;
      numArray7[29] = (byte) 211;
      numArray7[34] = (byte) 235;
      numArray7[27] = (byte) 21;
      numArray7[43] = (byte) 91;
      numArray7[40] = (byte) 95;
      numArray7[11] = (byte) 243;
      numArray7[31 /*0x1F*/] = (byte) 252;
      numArray7[5] = (byte) 48 /*0x30*/;
      numArray7[20] = (byte) 87;
      numArray7[21] = (byte) 30;
      numArray7[22] = (byte) 231;
      numArray7[48 /*0x30*/] = (byte) 42;
      numArray7[24] = (byte) 66;
      numArray7[25] = (byte) 166;
      numArray7[17] = (byte) 173;
      numArray7[14] = (byte) 37;
      numArray7[28] = (byte) 27;
      numArray7[49] = (byte) 206;
      numArray7[4] = (byte) 150;
      numArray7[15] = (byte) 229;
      numArray7[32 /*0x20*/] = (byte) 195;
      numArray7[39] = (byte) 187;
      numArray7[0] = (byte) 200;
      numArray7[35] = (byte) 106;
      numArray7[9] = (byte) 20;
      numArray7[23] = (byte) 78;
      numArray7[38] = (byte) 40;
      numArray7[30] = (byte) 228;
      numArray7[26] = (byte) 83;
      numArray7[50] = (byte) 30;
      numArray7[10] = (byte) 84;
      numArray7[42] = (byte) 143;
      numArray7[44] = (byte) 232;
      numArray7[45] = (byte) 239;
      numArray7[46] = (byte) 158;
      numArray7[47] = (byte) 205;
      numArray7[13] = (byte) 101;
      numArray7[3] = (byte) 54;
      numArray7[1] = (byte) 184;
      numArray7[51] = (byte) 74;
      numArray7[52] = (byte) 62;
      numArray7[53] = (byte) 172;
      numArray7[6] = (byte) 66;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 49,
        (byte) 101,
        (byte) 203,
        (byte) 159,
        (byte) 176 /*0xB0*/,
        byte.MaxValue,
        (byte) 26,
        (byte) 229,
        (byte) 81,
        (byte) 250,
        (byte) 152,
        (byte) 153,
        (byte) 139,
        (byte) 106,
        (byte) 78,
        (byte) 254,
        (byte) 43,
        (byte) 120,
        (byte) 86,
        (byte) 195,
        (byte) 178,
        (byte) 155,
        (byte) 2,
        (byte) 103,
        (byte) 109,
        (byte) 139,
        (byte) 80 /*0x50*/,
        (byte) 166,
        (byte) 169,
        (byte) 27,
        (byte) 134,
        (byte) 168,
        (byte) 5,
        (byte) 188,
        (byte) 194,
        (byte) 56,
        (byte) 106,
        (byte) 59,
        (byte) 102,
        (byte) 216,
        (byte) 241,
        (byte) 43,
        (byte) 245,
        (byte) 235,
        (byte) 214,
        (byte) 228,
        (byte) 15,
        (byte) 47,
        (byte) 80 /*0x50*/,
        (byte) 251,
        (byte) 66,
        (byte) 15,
        (byte) 137,
        (byte) 125,
        (byte) 78
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 89,
        (byte) 216,
        (byte) 137,
        (byte) 44,
        (byte) 116,
        (byte) 144 /*0x90*/,
        (byte) 36,
        (byte) 232,
        (byte) 140,
        (byte) 126,
        (byte) 132,
        (byte) 67,
        (byte) 146,
        (byte) 219,
        (byte) 1,
        (byte) 143,
        (byte) 50,
        (byte) 202,
        (byte) 125,
        (byte) 201,
        (byte) 207,
        (byte) 19,
        (byte) 24,
        (byte) 217,
        (byte) 84,
        (byte) 229,
        (byte) 82,
        (byte) 30,
        (byte) 79,
        (byte) 19,
        (byte) 211,
        (byte) 204,
        (byte) 212,
        (byte) 141,
        (byte) 80 /*0x50*/,
        (byte) 35,
        (byte) 116,
        (byte) 213,
        (byte) 15,
        (byte) 254,
        (byte) 0,
        (byte) 19,
        (byte) 196,
        (byte) 189,
        (byte) 183,
        (byte) 248,
        (byte) 129,
        (byte) 73,
        (byte) 182,
        (byte) 103,
        (byte) 241,
        (byte) 138,
        (byte) 230,
        (byte) 145,
        (byte) 152
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[21];
      numArray10[11] = (byte) 167;
      numArray10[1] = (byte) 50;
      numArray10[2] = (byte) 36;
      numArray10[7] = (byte) 116;
      numArray10[14] = (byte) 239;
      numArray10[10] = (byte) 207;
      numArray10[4] = (byte) 115;
      numArray10[0] = (byte) 100;
      numArray10[18] = (byte) 134;
      numArray10[9] = (byte) 147;
      numArray10[12] = (byte) 22;
      numArray10[3] = (byte) 174;
      numArray10[16 /*0x10*/] = (byte) 125;
      numArray10[13] = (byte) 148;
      numArray10[5] = (byte) 59;
      numArray10[15] = (byte) 17;
      numArray10[8] = (byte) 240 /*0xF0*/;
      numArray10[17] = (byte) 209;
      numArray10[6] = (byte) 54;
      numArray10[19] = (byte) 165;
      numArray10[20] = (byte) 23;
      byte[] numArray11 = new byte[21];
      numArray11[5] = (byte) 176 /*0xB0*/;
      numArray11[1] = (byte) 222;
      numArray11[2] = (byte) 59;
      numArray11[8] = (byte) 127 /*0x7F*/;
      numArray11[3] = (byte) 124;
      numArray11[19] = (byte) 115;
      numArray11[10] = (byte) 20;
      numArray11[7] = (byte) 230;
      numArray11[20] = (byte) 87;
      numArray11[9] = (byte) 83;
      numArray11[18] = (byte) 3;
      numArray11[0] = (byte) 155;
      numArray11[12] = (byte) 102;
      numArray11[11] = (byte) 212;
      numArray11[14] = (byte) 184;
      numArray11[15] = (byte) 12;
      numArray11[6] = (byte) 8;
      numArray11[17] = (byte) 69;
      numArray11[16 /*0x10*/] = (byte) 196;
      numArray11[4] = (byte) 110;
      numArray11[13] = (byte) 184;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[241];
    byte[] numArray13 = new byte[55];
    numArray13[9] = (byte) 160 /*0xA0*/;
    numArray13[50] = (byte) 216;
    numArray13[2] = (byte) 141;
    numArray13[40] = (byte) 89;
    numArray13[22] = (byte) 137;
    numArray13[20] = (byte) 167;
    numArray13[38] = (byte) 45;
    numArray13[7] = (byte) 150;
    numArray13[25] = (byte) 153;
    numArray13[14] = (byte) 28;
    numArray13[13] = (byte) 153;
    numArray13[51] = (byte) 130;
    numArray13[18] = (byte) 18;
    numArray13[30] = (byte) 122;
    numArray13[41] = (byte) 49;
    numArray13[5] = (byte) 83;
    numArray13[16 /*0x10*/] = (byte) 133;
    numArray13[19] = (byte) 220;
    numArray13[53] = (byte) 47;
    numArray13[12] = (byte) 60;
    numArray13[47] = (byte) 244;
    numArray13[3] = (byte) 230;
    numArray13[21] = (byte) 60;
    numArray13[0] = (byte) 122;
    numArray13[24] = (byte) 95;
    numArray13[49] = (byte) 68;
    numArray13[23] = (byte) 51;
    numArray13[27] = (byte) 97;
    numArray13[28] = (byte) 100;
    numArray13[29] = (byte) 97;
    numArray13[35] = (byte) 248;
    numArray13[31 /*0x1F*/] = (byte) 20;
    numArray13[32 /*0x20*/] = (byte) 69;
    numArray13[15] = (byte) 71;
    numArray13[34] = (byte) 13;
    numArray13[10] = (byte) 170;
    numArray13[36] = (byte) 217;
    numArray13[37] = (byte) 108;
    numArray13[26] = (byte) 156;
    numArray13[39] = (byte) 184;
    numArray13[11] = (byte) 55;
    numArray13[44] = (byte) 41;
    numArray13[42] = (byte) 9;
    numArray13[43] = (byte) 171;
    numArray13[17] = (byte) 121;
    numArray13[45] = (byte) 114;
    numArray13[33] = (byte) 223;
    numArray13[1] = (byte) 65;
    numArray13[48 /*0x30*/] = (byte) 0;
    numArray13[8] = (byte) 230;
    numArray13[6] = (byte) 82;
    numArray13[46] = (byte) 108;
    numArray13[52] = (byte) 191;
    numArray13[4] = (byte) 208 /*0xD0*/;
    numArray13[54] = (byte) 185;
    byte[] numArray14 = new byte[55]
    {
      (byte) 46,
      (byte) 2,
      (byte) 54,
      (byte) 106,
      (byte) 103,
      (byte) 31 /*0x1F*/,
      (byte) 211,
      (byte) 62,
      (byte) 152,
      (byte) 20,
      (byte) 187,
      (byte) 248,
      (byte) 110,
      (byte) 139,
      (byte) 98,
      (byte) 106,
      (byte) 155,
      (byte) 241,
      (byte) 191,
      (byte) 228,
      (byte) 72,
      (byte) 84,
      (byte) 24,
      (byte) 247,
      (byte) 138,
      (byte) 71,
      (byte) 1,
      (byte) 32 /*0x20*/,
      (byte) 240 /*0xF0*/,
      (byte) 106,
      (byte) 111,
      (byte) 19,
      (byte) 246,
      (byte) 149,
      (byte) 33,
      (byte) 227,
      (byte) 32 /*0x20*/,
      (byte) 136,
      (byte) 226,
      (byte) 160 /*0xA0*/,
      (byte) 118,
      (byte) 109,
      (byte) 145,
      (byte) 35,
      (byte) 38,
      (byte) 67,
      (byte) 188,
      (byte) 242,
      (byte) 72,
      (byte) 192 /*0xC0*/,
      (byte) 253,
      (byte) 58,
      (byte) 106,
      (byte) 56,
      (byte) 33
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 80 /*0x50*/,
      (byte) 154,
      (byte) 10,
      (byte) 15,
      (byte) 143,
      (byte) 18,
      (byte) 251,
      (byte) 46,
      (byte) 64 /*0x40*/,
      (byte) 221,
      (byte) 53,
      (byte) 189,
      (byte) 227,
      (byte) 239,
      (byte) 50,
      (byte) 86,
      (byte) 88,
      (byte) 26,
      (byte) 119,
      (byte) 173,
      (byte) 88,
      (byte) 118,
      (byte) 107,
      (byte) 28,
      (byte) 63 /*0x3F*/,
      (byte) 132,
      (byte) 74,
      (byte) 200,
      (byte) 193,
      (byte) 238,
      (byte) 104,
      (byte) 227,
      (byte) 193,
      (byte) 102,
      (byte) 47,
      (byte) 74,
      (byte) 235,
      (byte) 241,
      (byte) 216,
      (byte) 236,
      (byte) 137,
      (byte) 86,
      (byte) 83,
      (byte) 163,
      (byte) 218,
      (byte) 170,
      (byte) 249,
      (byte) 230,
      (byte) 142,
      (byte) 223,
      (byte) 13,
      (byte) 146,
      (byte) 204,
      (byte) 94,
      (byte) 130
    };
    byte[] numArray16 = new byte[55];
    numArray16[15] = (byte) 247;
    numArray16[14] = (byte) 244;
    numArray16[2] = (byte) 151;
    numArray16[17] = (byte) 83;
    numArray16[31 /*0x1F*/] = (byte) 61;
    numArray16[5] = (byte) 45;
    numArray16[1] = (byte) 213;
    numArray16[7] = (byte) 83;
    numArray16[39] = (byte) 59;
    numArray16[9] = (byte) 157;
    numArray16[10] = (byte) 217;
    numArray16[18] = (byte) 198;
    numArray16[32 /*0x20*/] = (byte) 83;
    numArray16[13] = (byte) 56;
    numArray16[25] = (byte) 221;
    numArray16[38] = (byte) 126;
    numArray16[11] = (byte) 226;
    numArray16[3] = (byte) 197;
    numArray16[21] = (byte) 14;
    numArray16[36] = (byte) 26;
    numArray16[51] = (byte) 86;
    numArray16[44] = (byte) 223;
    numArray16[22] = (byte) 39;
    numArray16[12] = (byte) 81;
    numArray16[24] = (byte) 232;
    numArray16[46] = (byte) 157;
    numArray16[19] = (byte) 243;
    numArray16[27] = (byte) 127 /*0x7F*/;
    numArray16[49] = (byte) 121;
    numArray16[29] = (byte) 244;
    numArray16[30] = (byte) 17;
    numArray16[28] = (byte) 164;
    numArray16[26] = (byte) 105;
    numArray16[6] = (byte) 36;
    numArray16[34] = (byte) 166;
    numArray16[16 /*0x10*/] = (byte) 137;
    numArray16[33] = (byte) 9;
    numArray16[37] = (byte) 116;
    numArray16[8] = (byte) 176 /*0xB0*/;
    numArray16[20] = (byte) 170;
    numArray16[40] = (byte) 125;
    numArray16[41] = (byte) 193;
    numArray16[4] = (byte) 213;
    numArray16[43] = (byte) 3;
    numArray16[23] = (byte) 210;
    numArray16[45] = (byte) 22;
    numArray16[35] = (byte) 206;
    numArray16[48 /*0x30*/] = (byte) 157;
    numArray16[0] = (byte) 182;
    numArray16[47] = (byte) 222;
    numArray16[50] = (byte) 216;
    numArray16[42] = (byte) 128 /*0x80*/;
    numArray16[52] = (byte) 89;
    numArray16[53] = (byte) 228;
    numArray16[54] = (byte) 18;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[15] = (byte) 116;
    numArray17[1] = (byte) 59;
    numArray17[22] = (byte) 230;
    numArray17[3] = (byte) 100;
    numArray17[44] = (byte) 227;
    numArray17[31 /*0x1F*/] = (byte) 99;
    numArray17[9] = (byte) 152;
    numArray17[7] = (byte) 159;
    numArray17[19] = (byte) 32 /*0x20*/;
    numArray17[37] = (byte) 191;
    numArray17[0] = (byte) 172;
    numArray17[11] = (byte) 107;
    numArray17[12] = (byte) 203;
    numArray17[30] = (byte) 40;
    numArray17[46] = (byte) 218;
    numArray17[16 /*0x10*/] = (byte) 102;
    numArray17[17] = (byte) 206;
    numArray17[33] = (byte) 181;
    numArray17[38] = (byte) 92;
    numArray17[45] = (byte) 187;
    numArray17[20] = (byte) 154;
    numArray17[48 /*0x30*/] = (byte) 95;
    numArray17[49] = (byte) 220;
    numArray17[23] = (byte) 14;
    numArray17[24] = (byte) 60;
    numArray17[25] = (byte) 148;
    numArray17[47] = (byte) 5;
    numArray17[27] = (byte) 58;
    numArray17[28] = (byte) 132;
    numArray17[29] = (byte) 213;
    numArray17[35] = (byte) 228;
    numArray17[21] = (byte) 186;
    numArray17[32 /*0x20*/] = (byte) 157;
    numArray17[4] = (byte) 248;
    numArray17[34] = (byte) 80 /*0x50*/;
    numArray17[42] = (byte) 75;
    numArray17[36] = (byte) 77;
    numArray17[2] = (byte) 128 /*0x80*/;
    numArray17[50] = (byte) 219;
    numArray17[39] = (byte) 93;
    numArray17[40] = (byte) 144 /*0x90*/;
    numArray17[41] = (byte) 142;
    numArray17[26] = (byte) 110;
    numArray17[43] = (byte) 21;
    numArray17[10] = (byte) 152;
    numArray17[6] = (byte) 113;
    numArray17[18] = (byte) 24;
    numArray17[5] = (byte) 139;
    numArray17[13] = (byte) 110;
    numArray17[14] = (byte) 70;
    numArray17[8] = (byte) 59;
    numArray17[51] = (byte) 107;
    numArray17[52] = (byte) 104;
    numArray17[53] = (byte) 150;
    numArray17[54] = (byte) 203;
    byte[] numArray18 = new byte[55];
    numArray18[38] = (byte) 211;
    numArray18[1] = (byte) 162;
    numArray18[2] = (byte) 214;
    numArray18[39] = (byte) 228;
    numArray18[26] = (byte) 187;
    numArray18[13] = (byte) 4;
    numArray18[42] = (byte) 11;
    numArray18[22] = (byte) 226;
    numArray18[48 /*0x30*/] = (byte) 105;
    numArray18[9] = (byte) 227;
    numArray18[3] = (byte) 239;
    numArray18[11] = (byte) 22;
    numArray18[15] = (byte) 115;
    numArray18[29] = (byte) 128 /*0x80*/;
    numArray18[35] = (byte) 236;
    numArray18[43] = (byte) 124;
    numArray18[16 /*0x10*/] = (byte) 0;
    numArray18[17] = (byte) 20;
    numArray18[47] = (byte) 69;
    numArray18[19] = (byte) 52;
    numArray18[20] = (byte) 8;
    numArray18[5] = (byte) 133;
    numArray18[8] = (byte) 133;
    numArray18[23] = (byte) 233;
    numArray18[24] = (byte) 91;
    numArray18[25] = (byte) 122;
    numArray18[21] = (byte) 41;
    numArray18[27] = (byte) 179;
    numArray18[28] = (byte) 45;
    numArray18[44] = (byte) 174;
    numArray18[10] = (byte) 2;
    numArray18[18] = (byte) 213;
    numArray18[51] = (byte) 48 /*0x30*/;
    numArray18[54] = (byte) 131;
    numArray18[34] = (byte) 167;
    numArray18[33] = (byte) 188;
    numArray18[36] = (byte) 134;
    numArray18[0] = (byte) 208 /*0xD0*/;
    numArray18[37] = (byte) 65;
    numArray18[6] = (byte) 189;
    numArray18[40] = (byte) 239;
    numArray18[41] = (byte) 14;
    numArray18[14] = (byte) 19;
    numArray18[7] = (byte) 153;
    numArray18[31 /*0x1F*/] = (byte) 62;
    numArray18[45] = (byte) 57;
    numArray18[30] = (byte) 113;
    numArray18[32 /*0x20*/] = (byte) 172;
    numArray18[4] = (byte) 13;
    numArray18[49] = (byte) 229;
    numArray18[50] = (byte) 30;
    numArray18[52] = (byte) 157;
    numArray18[46] = (byte) 114;
    numArray18[53] = (byte) 125;
    numArray18[12] = (byte) 48 /*0x30*/;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 200,
      (byte) 19,
      (byte) 183,
      (byte) 50,
      (byte) 127 /*0x7F*/,
      (byte) 129,
      (byte) 165,
      (byte) 241,
      (byte) 234,
      (byte) 197,
      (byte) 254,
      (byte) 228,
      (byte) 198,
      (byte) 180,
      (byte) 29,
      (byte) 44,
      (byte) 240 /*0xF0*/,
      (byte) 57,
      (byte) 156,
      (byte) 16 /*0x10*/,
      (byte) 57,
      (byte) 7,
      (byte) 107,
      (byte) 90,
      (byte) 90,
      (byte) 176 /*0xB0*/,
      (byte) 136,
      (byte) 161,
      (byte) 65,
      (byte) 29,
      (byte) 49,
      (byte) 9,
      (byte) 226,
      (byte) 177,
      (byte) 205,
      (byte) 222,
      (byte) 149,
      (byte) 87,
      (byte) 125,
      (byte) 124,
      (byte) 212,
      (byte) 0,
      (byte) 138,
      (byte) 99,
      (byte) 155,
      (byte) 67,
      (byte) 100,
      (byte) 181,
      (byte) 7,
      (byte) 84,
      (byte) 64 /*0x40*/,
      (byte) 150,
      (byte) 135,
      (byte) 232,
      (byte) 32 /*0x20*/
    };
    byte[] numArray20 = new byte[55];
    numArray20[40] = (byte) 196;
    numArray20[1] = (byte) 214;
    numArray20[52] = (byte) 243;
    numArray20[46] = (byte) 185;
    numArray20[3] = (byte) 104;
    numArray20[5] = (byte) 232;
    numArray20[6] = (byte) 172;
    numArray20[23] = (byte) 174;
    numArray20[2] = (byte) 78;
    numArray20[38] = (byte) 188;
    numArray20[10] = (byte) 232;
    numArray20[45] = (byte) 87;
    numArray20[12] = (byte) 29;
    numArray20[13] = (byte) 125;
    numArray20[14] = (byte) 129;
    numArray20[8] = (byte) 80 /*0x50*/;
    numArray20[16 /*0x10*/] = (byte) 6;
    numArray20[53] = (byte) 216;
    numArray20[21] = (byte) 170;
    numArray20[0] = (byte) 70;
    numArray20[20] = (byte) 162;
    numArray20[36] = (byte) 219;
    numArray20[34] = (byte) 222;
    numArray20[22] = (byte) 252;
    numArray20[24] = (byte) 185;
    numArray20[25] = (byte) 59;
    numArray20[26] = (byte) 6;
    numArray20[27] = (byte) 214;
    numArray20[28] = (byte) 32 /*0x20*/;
    numArray20[29] = (byte) 65;
    numArray20[30] = (byte) 193;
    numArray20[31 /*0x1F*/] = (byte) 154;
    numArray20[32 /*0x20*/] = (byte) 10;
    numArray20[19] = (byte) 148;
    numArray20[47] = (byte) 112 /*0x70*/;
    numArray20[35] = (byte) 72;
    numArray20[7] = (byte) 9;
    numArray20[37] = (byte) 238;
    numArray20[9] = (byte) 22;
    numArray20[39] = (byte) 65;
    numArray20[15] = (byte) 110;
    numArray20[18] = (byte) 156;
    numArray20[42] = (byte) 195;
    numArray20[43] = (byte) 2;
    numArray20[44] = (byte) 69;
    numArray20[33] = (byte) 77;
    numArray20[49] = (byte) 212;
    numArray20[41] = (byte) 249;
    numArray20[48 /*0x30*/] = (byte) 138;
    numArray20[11] = (byte) 154;
    numArray20[50] = (byte) 41;
    numArray20[51] = (byte) 24;
    numArray20[17] = (byte) 219;
    numArray20[4] = (byte) 119;
    numArray20[54] = (byte) 193;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[21];
    numArray21[14] = (byte) 24;
    numArray21[15] = (byte) 13;
    numArray21[12] = (byte) 9;
    numArray21[0] = (byte) 157;
    numArray21[4] = (byte) 106;
    numArray21[5] = (byte) 195;
    numArray21[13] = (byte) 94;
    numArray21[7] = (byte) 249;
    numArray21[8] = (byte) 115;
    numArray21[3] = (byte) 38;
    numArray21[10] = (byte) 132;
    numArray21[1] = (byte) 192 /*0xC0*/;
    numArray21[18] = (byte) 233;
    numArray21[6] = (byte) 241;
    numArray21[11] = (byte) 218;
    numArray21[2] = (byte) 131;
    numArray21[19] = (byte) 191;
    numArray21[17] = (byte) 108;
    numArray21[16 /*0x10*/] = (byte) 157;
    numArray21[9] = (byte) 7;
    numArray21[20] = (byte) 57;
    byte[] numArray22 = new byte[21]
    {
      (byte) 16 /*0x10*/,
      (byte) 36,
      (byte) 46,
      (byte) 95,
      (byte) 93,
      (byte) 108,
      (byte) 80 /*0x50*/,
      (byte) 114,
      (byte) 39,
      (byte) 35,
      (byte) 26,
      (byte) 223,
      (byte) 252,
      (byte) 206,
      (byte) 173,
      (byte) 2,
      (byte) 134,
      (byte) 49,
      (byte) 139,
      (byte) 157,
      (byte) 197
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 21);
    for (int index = 0; index < 21; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[17];
    byte[] response = new byte[17];
    Array.Copy((Array) sc_13393.sspq, 457, (Array) numArray23, 0, 17);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13393.sspr, 457, (Array) numArray23, 0, 17);
    for (int index = 0; index < numArray23.Length; ++index)
    {
      if ((int) numArray23[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_13417()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[132];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 164;
      numArray2[35] = (byte) 240 /*0xF0*/;
      numArray2[1] = (byte) 218;
      numArray2[14] = (byte) 49;
      numArray2[4] = (byte) 195;
      numArray2[5] = (byte) 89;
      numArray2[6] = (byte) 226;
      numArray2[12] = (byte) 230;
      numArray2[47] = (byte) 78;
      numArray2[15] = (byte) 17;
      numArray2[29] = (byte) 204;
      numArray2[17] = (byte) 210;
      numArray2[22] = (byte) 170;
      numArray2[13] = (byte) 156;
      numArray2[39] = (byte) 54;
      numArray2[11] = (byte) 99;
      numArray2[41] = (byte) 136;
      numArray2[8] = (byte) 7;
      numArray2[54] = (byte) 8;
      numArray2[34] = (byte) 127 /*0x7F*/;
      numArray2[20] = (byte) 80 /*0x50*/;
      numArray2[30] = (byte) 248;
      numArray2[52] = (byte) 24;
      numArray2[19] = (byte) 241;
      numArray2[24] = (byte) 48 /*0x30*/;
      numArray2[3] = (byte) 114;
      numArray2[26] = (byte) 213;
      numArray2[27] = (byte) 164;
      numArray2[7] = (byte) 65;
      numArray2[40] = (byte) 235;
      numArray2[0] = (byte) 186;
      numArray2[31 /*0x1F*/] = (byte) 14;
      numArray2[32 /*0x20*/] = (byte) 154;
      numArray2[37] = (byte) 76;
      numArray2[53] = (byte) 73;
      numArray2[48 /*0x30*/] = (byte) 84;
      numArray2[46] = (byte) 158;
      numArray2[9] = (byte) 92;
      numArray2[38] = (byte) 154;
      numArray2[50] = (byte) 133;
      numArray2[2] = (byte) 18;
      numArray2[10] = (byte) 244;
      numArray2[42] = (byte) 57;
      numArray2[25] = (byte) 21;
      numArray2[44] = (byte) 144 /*0x90*/;
      numArray2[45] = (byte) 2;
      numArray2[21] = (byte) 12;
      numArray2[43] = (byte) 161;
      numArray2[16 /*0x10*/] = (byte) 19;
      numArray2[33] = (byte) 217;
      numArray2[23] = (byte) 67;
      numArray2[51] = (byte) 193;
      numArray2[49] = (byte) 15;
      numArray2[28] = (byte) 58;
      numArray2[36] = (byte) 224 /*0xE0*/;
      byte[] numArray3 = new byte[55];
      numArray3[45] = (byte) 160 /*0xA0*/;
      numArray3[0] = (byte) 128 /*0x80*/;
      numArray3[17] = (byte) 112 /*0x70*/;
      numArray3[26] = (byte) 239;
      numArray3[47] = (byte) 117;
      numArray3[5] = (byte) 89;
      numArray3[20] = (byte) 42;
      numArray3[28] = (byte) 199;
      numArray3[7] = (byte) 75;
      numArray3[8] = (byte) 23;
      numArray3[10] = (byte) 135;
      numArray3[15] = (byte) 12;
      numArray3[44] = (byte) 73;
      numArray3[21] = (byte) 179;
      numArray3[3] = (byte) 178;
      numArray3[34] = (byte) 64 /*0x40*/;
      numArray3[48 /*0x30*/] = (byte) 112 /*0x70*/;
      numArray3[31 /*0x1F*/] = (byte) 185;
      numArray3[19] = (byte) 18;
      numArray3[6] = (byte) 60;
      numArray3[1] = (byte) 61;
      numArray3[51] = (byte) 16 /*0x10*/;
      numArray3[22] = (byte) 77;
      numArray3[39] = (byte) 208 /*0xD0*/;
      numArray3[4] = (byte) 103;
      numArray3[24] = (byte) 146;
      numArray3[41] = (byte) 36;
      numArray3[27] = (byte) 201;
      numArray3[38] = (byte) 72;
      numArray3[2] = (byte) 168;
      numArray3[30] = (byte) 31 /*0x1F*/;
      numArray3[23] = (byte) 44;
      numArray3[32 /*0x20*/] = (byte) 189;
      numArray3[33] = (byte) 119;
      numArray3[14] = (byte) 20;
      numArray3[35] = (byte) 230;
      numArray3[36] = (byte) 41;
      numArray3[52] = (byte) 138;
      numArray3[16 /*0x10*/] = (byte) 168;
      numArray3[25] = (byte) 69;
      numArray3[40] = (byte) 79;
      numArray3[9] = (byte) 161;
      numArray3[42] = (byte) 149;
      numArray3[43] = (byte) 70;
      numArray3[12] = (byte) 60;
      numArray3[18] = (byte) 19;
      numArray3[46] = (byte) 106;
      numArray3[11] = (byte) 208 /*0xD0*/;
      numArray3[13] = (byte) 38;
      numArray3[49] = byte.MaxValue;
      numArray3[50] = (byte) 118;
      numArray3[29] = (byte) 58;
      numArray3[37] = (byte) 76;
      numArray3[53] = (byte) 31 /*0x1F*/;
      numArray3[54] = (byte) 23;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 20,
        (byte) 32 /*0x20*/,
        (byte) 82,
        (byte) 167,
        (byte) 176 /*0xB0*/,
        (byte) 144 /*0x90*/,
        (byte) 106,
        (byte) 126,
        (byte) 164,
        (byte) 105,
        (byte) 170,
        (byte) 82,
        (byte) 232,
        (byte) 45,
        (byte) 200,
        (byte) 150,
        (byte) 6,
        (byte) 152,
        (byte) 160 /*0xA0*/,
        (byte) 200,
        (byte) 185,
        (byte) 77,
        (byte) 51,
        (byte) 141,
        (byte) 109,
        (byte) 171,
        (byte) 142,
        (byte) 136,
        (byte) 111,
        (byte) 93,
        (byte) 35,
        (byte) 149,
        (byte) 241,
        (byte) 61,
        (byte) 222,
        (byte) 212,
        (byte) 98,
        (byte) 14,
        (byte) 15,
        (byte) 20,
        (byte) 161,
        (byte) 146,
        (byte) 21,
        (byte) 87,
        (byte) 172,
        (byte) 149,
        (byte) 169,
        (byte) 143,
        (byte) 56,
        (byte) 167,
        (byte) 231,
        (byte) 81,
        (byte) 89,
        (byte) 61,
        (byte) 162
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 250,
        (byte) 193,
        (byte) 155,
        (byte) 102,
        (byte) 154,
        (byte) 12,
        (byte) 221,
        (byte) 63 /*0x3F*/,
        (byte) 98,
        (byte) 145,
        (byte) 163,
        (byte) 3,
        (byte) 64 /*0x40*/,
        (byte) 249,
        (byte) 7,
        (byte) 242,
        (byte) 251,
        (byte) 116,
        (byte) 214,
        (byte) 19,
        (byte) 246,
        (byte) 152,
        (byte) 76,
        (byte) 231,
        (byte) 18,
        (byte) 37,
        (byte) 209,
        (byte) 185,
        (byte) 35,
        (byte) 231,
        (byte) 232,
        (byte) 52,
        (byte) 223,
        (byte) 50,
        (byte) 31 /*0x1F*/,
        (byte) 213,
        (byte) 119,
        (byte) 83,
        (byte) 218,
        (byte) 75,
        (byte) 249,
        (byte) 234,
        (byte) 43,
        (byte) 42,
        (byte) 15,
        (byte) 248,
        (byte) 55,
        (byte) 208 /*0xD0*/,
        (byte) 134,
        (byte) 209,
        (byte) 39,
        (byte) 69,
        (byte) 55,
        (byte) 86,
        (byte) 178
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[22]
      {
        (byte) 196,
        (byte) 57,
        (byte) 4,
        (byte) 2,
        (byte) 225,
        (byte) 15,
        (byte) 71,
        (byte) 70,
        (byte) 20,
        (byte) 97,
        (byte) 171,
        (byte) 79,
        (byte) 243,
        (byte) 51,
        (byte) 247,
        (byte) 137,
        (byte) 140,
        (byte) 39,
        (byte) 39,
        (byte) 56,
        (byte) 219,
        (byte) 224 /*0xE0*/
      };
      byte[] numArray7 = new byte[22];
      numArray7[4] = (byte) 168;
      numArray7[1] = (byte) 57;
      numArray7[2] = (byte) 123;
      numArray7[21] = (byte) 116;
      numArray7[8] = (byte) 153;
      numArray7[19] = (byte) 227;
      numArray7[6] = (byte) 63 /*0x3F*/;
      numArray7[3] = (byte) 75;
      numArray7[0] = (byte) 62;
      numArray7[9] = (byte) 248;
      numArray7[10] = (byte) 7;
      numArray7[14] = (byte) 167;
      numArray7[12] = (byte) 226;
      numArray7[13] = (byte) 119;
      numArray7[7] = (byte) 33;
      numArray7[11] = (byte) 71;
      numArray7[15] = (byte) 231;
      numArray7[17] = (byte) 120;
      numArray7[18] = (byte) 55;
      numArray7[16 /*0x10*/] = (byte) 237;
      numArray7[5] = (byte) 57;
      numArray7[20] = (byte) 190;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[132];
    byte[] numArray9 = new byte[55]
    {
      (byte) 115,
      (byte) 239,
      (byte) 200,
      (byte) 205,
      (byte) 21,
      (byte) 123,
      (byte) 142,
      (byte) 17,
      (byte) 228,
      (byte) 96 /*0x60*/,
      (byte) 1,
      (byte) 3,
      (byte) 144 /*0x90*/,
      (byte) 70,
      (byte) 208 /*0xD0*/,
      (byte) 115,
      (byte) 217,
      (byte) 173,
      (byte) 16 /*0x10*/,
      (byte) 55,
      (byte) 11,
      (byte) 149,
      (byte) 34,
      (byte) 125,
      (byte) 122,
      (byte) 103,
      (byte) 163,
      (byte) 106,
      (byte) 202,
      (byte) 72,
      (byte) 164,
      (byte) 4,
      (byte) 190,
      (byte) 12,
      (byte) 84,
      (byte) 135,
      (byte) 10,
      (byte) 110,
      (byte) 158,
      (byte) 31 /*0x1F*/,
      (byte) 136,
      (byte) 214,
      (byte) 185,
      (byte) 116,
      (byte) 9,
      (byte) 14,
      (byte) 47,
      (byte) 60,
      (byte) 254,
      (byte) 157,
      (byte) 190,
      (byte) 134,
      (byte) 237,
      (byte) 8,
      (byte) 57
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 0,
      (byte) 80 /*0x50*/,
      (byte) 120,
      (byte) 16 /*0x10*/,
      (byte) 212,
      (byte) 194,
      (byte) 124,
      (byte) 229,
      (byte) 162,
      (byte) 203,
      (byte) 93,
      (byte) 177,
      (byte) 171,
      (byte) 147,
      (byte) 174,
      (byte) 59,
      (byte) 120,
      (byte) 250,
      (byte) 201,
      (byte) 155,
      (byte) 5,
      (byte) 172,
      (byte) 161,
      (byte) 74,
      (byte) 201,
      (byte) 90,
      (byte) 155,
      (byte) 193,
      (byte) 89,
      (byte) 86,
      (byte) 214,
      (byte) 35,
      (byte) 232,
      (byte) 81,
      (byte) 16 /*0x10*/,
      (byte) 9,
      (byte) 179,
      (byte) 12,
      (byte) 176 /*0xB0*/,
      (byte) 10,
      (byte) 93,
      (byte) 55,
      (byte) 72,
      (byte) 39,
      (byte) 199,
      (byte) 151,
      (byte) 178,
      (byte) 187,
      (byte) 50,
      (byte) 148,
      (byte) 60,
      (byte) 187,
      (byte) 217,
      (byte) 150,
      (byte) 25
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 74,
      (byte) 82,
      (byte) 174,
      (byte) 215,
      (byte) 36,
      (byte) 70,
      (byte) 155,
      (byte) 188,
      (byte) 253,
      (byte) 69,
      (byte) 132,
      (byte) 242,
      (byte) 73,
      (byte) 105,
      (byte) 106,
      (byte) 15,
      (byte) 240 /*0xF0*/,
      (byte) 175,
      (byte) 53,
      (byte) 219,
      (byte) 173,
      (byte) 116,
      (byte) 108,
      (byte) 55,
      (byte) 103,
      (byte) 108,
      (byte) 44,
      (byte) 138,
      (byte) 30,
      (byte) 22,
      (byte) 181,
      (byte) 210,
      (byte) 118,
      (byte) 185,
      (byte) 191,
      (byte) 27,
      (byte) 239,
      (byte) 72,
      (byte) 99,
      (byte) 143,
      (byte) 30,
      (byte) 184,
      (byte) 12,
      (byte) 200,
      (byte) 171,
      (byte) 73,
      (byte) 180,
      (byte) 52,
      (byte) 83,
      (byte) 119,
      (byte) 253,
      (byte) 89,
      (byte) 3,
      (byte) 2,
      (byte) 31 /*0x1F*/
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 188,
      (byte) 96 /*0x60*/,
      (byte) 231,
      (byte) 35,
      (byte) 203,
      (byte) 27,
      (byte) 135,
      (byte) 117,
      (byte) 32 /*0x20*/,
      (byte) 159,
      (byte) 178,
      (byte) 60,
      (byte) 171,
      (byte) 3,
      (byte) 225,
      (byte) 116,
      (byte) 194,
      (byte) 223,
      (byte) 69,
      (byte) 46,
      (byte) 226,
      (byte) 181,
      (byte) 191,
      (byte) 152,
      (byte) 219,
      (byte) 10,
      (byte) 59,
      (byte) 134,
      (byte) 148,
      (byte) 129,
      (byte) 128 /*0x80*/,
      (byte) 250,
      (byte) 197,
      (byte) 115,
      (byte) 174,
      (byte) 90,
      (byte) 63 /*0x3F*/,
      (byte) 194,
      (byte) 20,
      (byte) 216,
      (byte) 58,
      (byte) 179,
      (byte) 213,
      (byte) 68,
      (byte) 176 /*0xB0*/,
      (byte) 24,
      (byte) 149,
      (byte) 202,
      (byte) 173,
      (byte) 130,
      (byte) 1,
      (byte) 158,
      (byte) 187,
      (byte) 234,
      (byte) 186
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[22]
    {
      (byte) 118,
      (byte) 112 /*0x70*/,
      (byte) 184,
      (byte) 25,
      (byte) 16 /*0x10*/,
      (byte) 157,
      (byte) 230,
      (byte) 144 /*0x90*/,
      (byte) 177,
      (byte) 124,
      (byte) 143,
      (byte) 170,
      (byte) 79,
      (byte) 31 /*0x1F*/,
      (byte) 168,
      (byte) 75,
      (byte) 166,
      (byte) 189,
      (byte) 245,
      (byte) 103,
      (byte) 163,
      (byte) 139
    };
    byte[] numArray14 = new byte[22]
    {
      (byte) 11,
      (byte) 247,
      (byte) 8,
      (byte) 29,
      (byte) 79,
      (byte) 115,
      (byte) 63 /*0x3F*/,
      (byte) 38,
      (byte) 249,
      (byte) 89,
      (byte) 181,
      (byte) 52,
      (byte) 103,
      (byte) 181,
      (byte) 79,
      (byte) 122,
      (byte) 41,
      (byte) 179,
      (byte) 80 /*0x50*/,
      (byte) 143,
      (byte) 156,
      (byte) 237
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 22);
    for (int index = 0; index < 22; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13418()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[40];
      byte[] numArray2 = new byte[40]
      {
        (byte) 167,
        (byte) 25,
        (byte) 91,
        (byte) 207,
        (byte) 115,
        (byte) 154,
        (byte) 232,
        (byte) 53,
        (byte) 120,
        (byte) 216,
        (byte) 171,
        (byte) 117,
        (byte) 13,
        (byte) 26,
        (byte) 37,
        (byte) 56,
        (byte) 250,
        (byte) 205,
        (byte) 37,
        (byte) 138,
        (byte) 4,
        (byte) 87,
        (byte) 85,
        (byte) 14,
        (byte) 93,
        (byte) 68,
        (byte) 41,
        (byte) 110,
        (byte) 67,
        (byte) 235,
        (byte) 82,
        (byte) 1,
        (byte) 33,
        (byte) 218,
        (byte) 246,
        (byte) 205,
        (byte) 53,
        (byte) 226,
        (byte) 8,
        (byte) 244
      };
      byte[] numArray3 = new byte[40];
      numArray3[9] = (byte) 229;
      numArray3[16 /*0x10*/] = (byte) 209;
      numArray3[27] = (byte) 5;
      numArray3[29] = (byte) 216;
      numArray3[6] = (byte) 62;
      numArray3[5] = (byte) 140;
      numArray3[38] = (byte) 223;
      numArray3[7] = (byte) 225;
      numArray3[8] = (byte) 166;
      numArray3[19] = (byte) 81;
      numArray3[26] = (byte) 48 /*0x30*/;
      numArray3[11] = (byte) 96 /*0x60*/;
      numArray3[12] = (byte) 71;
      numArray3[13] = (byte) 213;
      numArray3[14] = (byte) 146;
      numArray3[15] = (byte) 112 /*0x70*/;
      numArray3[2] = (byte) 87;
      numArray3[33] = (byte) 10;
      numArray3[10] = (byte) 220;
      numArray3[39] = (byte) 220;
      numArray3[18] = (byte) 26;
      numArray3[21] = (byte) 193;
      numArray3[22] = (byte) 101;
      numArray3[23] = (byte) 75;
      numArray3[3] = (byte) 205;
      numArray3[25] = (byte) 105;
      numArray3[17] = (byte) 17;
      numArray3[1] = (byte) 151;
      numArray3[4] = (byte) 110;
      numArray3[24] = (byte) 212;
      numArray3[0] = (byte) 186;
      numArray3[20] = (byte) 20;
      numArray3[32 /*0x20*/] = (byte) 94;
      numArray3[28] = (byte) 65;
      numArray3[30] = (byte) 171;
      numArray3[35] = (byte) 101;
      numArray3[36] = (byte) 232;
      numArray3[37] = (byte) 134;
      numArray3[34] = (byte) 52;
      numArray3[31 /*0x1F*/] = (byte) 183;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[40];
    byte[] numArray5 = new byte[40]
    {
      (byte) 216,
      (byte) 90,
      (byte) 2,
      (byte) 111,
      (byte) 178,
      (byte) 168,
      (byte) 166,
      (byte) 9,
      (byte) 232,
      (byte) 112 /*0x70*/,
      (byte) 217,
      (byte) 131,
      (byte) 70,
      (byte) 39,
      (byte) 25,
      (byte) 73,
      (byte) 246,
      (byte) 221,
      (byte) 124,
      (byte) 183,
      (byte) 233,
      (byte) 103,
      (byte) 120,
      (byte) 139,
      (byte) 97,
      (byte) 165,
      (byte) 71,
      (byte) 159,
      (byte) 62,
      (byte) 221,
      (byte) 69,
      (byte) 154,
      (byte) 34,
      (byte) 77,
      (byte) 25,
      (byte) 51,
      (byte) 130,
      (byte) 58,
      (byte) 161,
      (byte) 139
    };
    byte[] numArray6 = new byte[40]
    {
      (byte) 230,
      (byte) 147,
      (byte) 38,
      (byte) 69,
      (byte) 233,
      (byte) 188,
      (byte) 151,
      (byte) 88,
      (byte) 228,
      (byte) 225,
      (byte) 89,
      (byte) 234,
      (byte) 113,
      (byte) 159,
      (byte) 11,
      (byte) 123,
      (byte) 133,
      (byte) 13,
      (byte) 209,
      (byte) 230,
      (byte) 252,
      (byte) 222,
      (byte) 193,
      (byte) 142,
      (byte) 173,
      (byte) 32 /*0x20*/,
      (byte) 247,
      (byte) 175,
      (byte) 225,
      (byte) 106,
      (byte) 69,
      (byte) 91,
      (byte) 190,
      (byte) 216,
      (byte) 251,
      (byte) 98,
      (byte) 50,
      (byte) 40,
      (byte) 53,
      (byte) 28
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 40);
    for (int index = 0; index < 40; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13419()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[10] = (byte) 67;
      numArray2[3] = (byte) 195;
      numArray2[5] = (byte) 59;
      numArray2[1] = (byte) 172;
      numArray2[4] = (byte) 151;
      numArray2[2] = (byte) 130;
      numArray2[6] = (byte) 120;
      numArray2[7] = (byte) 79;
      numArray2[8] = (byte) 24;
      numArray2[9] = (byte) 151;
      numArray2[0] = (byte) 235;
      numArray2[13] = (byte) 119;
      numArray2[12] = (byte) 225;
      numArray2[11] = (byte) 112 /*0x70*/;
      numArray2[14] = (byte) 14;
      numArray2[15] = (byte) 12;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 164,
        (byte) 211,
        (byte) 99,
        (byte) 167,
        (byte) 138,
        (byte) 188,
        (byte) 147,
        (byte) 74,
        (byte) 118,
        (byte) 167,
        (byte) 1,
        (byte) 136,
        (byte) 13,
        (byte) 218,
        (byte) 192 /*0xC0*/,
        (byte) 198
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[14] = (byte) 148;
    numArray5[1] = (byte) 35;
    numArray5[2] = (byte) 132;
    numArray5[8] = (byte) 139;
    numArray5[4] = (byte) 2;
    numArray5[0] = (byte) 180;
    numArray5[6] = (byte) 245;
    numArray5[11] = (byte) 81;
    numArray5[5] = (byte) 101;
    numArray5[9] = (byte) 10;
    numArray5[7] = (byte) 130;
    numArray5[15] = (byte) 42;
    numArray5[12] = (byte) 10;
    numArray5[13] = (byte) 44;
    numArray5[3] = (byte) 7;
    numArray5[10] = (byte) 168;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 79,
      (byte) 214,
      (byte) 193,
      (byte) 20,
      (byte) 135,
      (byte) 95,
      (byte) 33,
      (byte) 53,
      (byte) 135,
      (byte) 148,
      (byte) 99,
      (byte) 102,
      (byte) 40,
      (byte) 129,
      (byte) 198,
      (byte) 37
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13420()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37];
      numArray2[2] = (byte) 200;
      numArray2[1] = (byte) 0;
      numArray2[32 /*0x20*/] = (byte) 209;
      numArray2[3] = (byte) 165;
      numArray2[21] = (byte) 207;
      numArray2[4] = (byte) 53;
      numArray2[26] = (byte) 91;
      numArray2[7] = (byte) 238;
      numArray2[28] = (byte) 97;
      numArray2[9] = (byte) 148;
      numArray2[25] = (byte) 226;
      numArray2[17] = (byte) 83;
      numArray2[33] = (byte) 170;
      numArray2[20] = (byte) 154;
      numArray2[14] = (byte) 195;
      numArray2[27] = (byte) 45;
      numArray2[16 /*0x10*/] = (byte) 61;
      numArray2[13] = (byte) 199;
      numArray2[18] = (byte) 243;
      numArray2[11] = (byte) 143;
      numArray2[15] = (byte) 21;
      numArray2[0] = (byte) 220;
      numArray2[22] = (byte) 86;
      numArray2[23] = (byte) 141;
      numArray2[6] = (byte) 214;
      numArray2[24] = (byte) 168;
      numArray2[36] = (byte) 188;
      numArray2[10] = (byte) 188;
      numArray2[8] = (byte) 101;
      numArray2[29] = (byte) 132;
      numArray2[30] = (byte) 64 /*0x40*/;
      numArray2[34] = (byte) 77;
      numArray2[12] = (byte) 167;
      numArray2[19] = (byte) 104;
      numArray2[5] = (byte) 188;
      numArray2[35] = (byte) 117;
      numArray2[31 /*0x1F*/] = (byte) 50;
      byte[] numArray3 = new byte[37]
      {
        (byte) 23,
        (byte) 125,
        (byte) 0,
        (byte) 251,
        (byte) 12,
        (byte) 158,
        (byte) 26,
        (byte) 129,
        (byte) 245,
        (byte) 46,
        (byte) 128 /*0x80*/,
        (byte) 165,
        (byte) 184,
        (byte) 35,
        (byte) 197,
        (byte) 153,
        (byte) 201,
        (byte) 184,
        (byte) 252,
        (byte) 61,
        (byte) 99,
        (byte) 147,
        (byte) 52,
        (byte) 203,
        (byte) 193,
        (byte) 228,
        (byte) 69,
        (byte) 253,
        (byte) 142,
        (byte) 63 /*0x3F*/,
        (byte) 115,
        (byte) 168,
        (byte) 222,
        (byte) 191,
        (byte) 227,
        (byte) 25,
        (byte) 65
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[37];
    byte[] numArray5 = new byte[37]
    {
      (byte) 57,
      (byte) 213,
      (byte) 123,
      (byte) 117,
      (byte) 251,
      (byte) 147,
      (byte) 205,
      (byte) 158,
      (byte) 130,
      (byte) 78,
      (byte) 224 /*0xE0*/,
      (byte) 91,
      (byte) 64 /*0x40*/,
      (byte) 203,
      (byte) 130,
      (byte) 108,
      (byte) 244,
      (byte) 141,
      (byte) 204,
      (byte) 13,
      (byte) 109,
      (byte) 200,
      (byte) 47,
      (byte) 111,
      (byte) 111,
      (byte) 230,
      (byte) 75,
      (byte) 59,
      (byte) 18,
      (byte) 178,
      (byte) 223,
      (byte) 254,
      (byte) 78,
      (byte) 140,
      (byte) 112 /*0x70*/,
      (byte) 100,
      (byte) 130
    };
    byte[] numArray6 = new byte[37]
    {
      (byte) 41,
      (byte) 75,
      (byte) 37,
      (byte) 185,
      (byte) 86,
      (byte) 247,
      (byte) 82,
      (byte) 118,
      (byte) 187,
      (byte) 18,
      (byte) 198,
      (byte) 2,
      (byte) 226,
      (byte) 161,
      (byte) 177,
      (byte) 2,
      (byte) 112 /*0x70*/,
      (byte) 243,
      (byte) 154,
      (byte) 207,
      (byte) 171,
      (byte) 214,
      (byte) 172,
      (byte) 16 /*0x10*/,
      (byte) 94,
      (byte) 10,
      (byte) 176 /*0xB0*/,
      (byte) 226,
      (byte) 115,
      (byte) 241,
      (byte) 35,
      (byte) 0,
      (byte) 248,
      (byte) 155,
      (byte) 120,
      (byte) 64 /*0x40*/,
      (byte) 18
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13421()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 66,
        (byte) 209,
        (byte) 58,
        (byte) 221,
        (byte) 160 /*0xA0*/,
        (byte) 57,
        (byte) 130,
        (byte) 214,
        (byte) 103,
        (byte) 23,
        (byte) 118,
        (byte) 38,
        (byte) 242,
        (byte) 18,
        (byte) 112 /*0x70*/,
        (byte) 166
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[9] = (byte) 192 /*0xC0*/;
      numArray3[5] = (byte) 123;
      numArray3[1] = (byte) 128 /*0x80*/;
      numArray3[3] = (byte) 220;
      numArray3[4] = (byte) 236;
      numArray3[11] = (byte) 99;
      numArray3[14] = (byte) 26;
      numArray3[0] = (byte) 210;
      numArray3[7] = (byte) 149;
      numArray3[2] = (byte) 6;
      numArray3[10] = (byte) 44;
      numArray3[12] = (byte) 4;
      numArray3[6] = (byte) 190;
      numArray3[13] = (byte) 1;
      numArray3[8] = (byte) 190;
      numArray3[15] = (byte) 3;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17];
      byte[] response = new byte[17];
      Array.Copy((Array) sc_13393.sspq, 474, (Array) numArray4, 0, 17);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 474, (Array) numArray4, 0, 17);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 204,
      (byte) 46,
      (byte) 182,
      (byte) 0,
      (byte) 203,
      (byte) 251,
      (byte) 147,
      (byte) 134,
      (byte) 39,
      (byte) 185,
      (byte) 126,
      byte.MaxValue,
      (byte) 77,
      (byte) 111,
      (byte) 165,
      (byte) 231
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 194,
      (byte) 177,
      (byte) 16 /*0x10*/,
      (byte) 75,
      (byte) 87,
      (byte) 47,
      (byte) 240 /*0xF0*/,
      (byte) 193,
      (byte) 84,
      (byte) 175,
      (byte) 30,
      (byte) 46,
      (byte) 173,
      (byte) 131,
      (byte) 48 /*0x30*/,
      (byte) 89
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13422()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[49];
      byte[] numArray2 = new byte[49]
      {
        (byte) 105,
        (byte) 127 /*0x7F*/,
        (byte) 44,
        (byte) 225,
        (byte) 53,
        (byte) 5,
        (byte) 10,
        (byte) 177,
        (byte) 57,
        (byte) 91,
        (byte) 95,
        (byte) 114,
        (byte) 64 /*0x40*/,
        (byte) 217,
        (byte) 234,
        (byte) 31 /*0x1F*/,
        (byte) 115,
        (byte) 246,
        (byte) 40,
        (byte) 150,
        (byte) 60,
        (byte) 38,
        (byte) 161,
        (byte) 171,
        (byte) 44,
        (byte) 155,
        (byte) 27,
        (byte) 104,
        (byte) 183,
        (byte) 146,
        (byte) 134,
        (byte) 11,
        (byte) 253,
        (byte) 160 /*0xA0*/,
        (byte) 230,
        (byte) 77,
        (byte) 1,
        (byte) 213,
        (byte) 203,
        (byte) 197,
        (byte) 240 /*0xF0*/,
        (byte) 226,
        (byte) 76,
        (byte) 204,
        (byte) 89,
        (byte) 11,
        (byte) 191,
        (byte) 116,
        (byte) 132
      };
      byte[] numArray3 = new byte[49];
      numArray3[46] = (byte) 139;
      numArray3[12] = (byte) 143;
      numArray3[2] = (byte) 47;
      numArray3[3] = (byte) 74;
      numArray3[35] = (byte) 52;
      numArray3[5] = byte.MaxValue;
      numArray3[11] = (byte) 44;
      numArray3[7] = (byte) 10;
      numArray3[31 /*0x1F*/] = (byte) 146;
      numArray3[34] = (byte) 178;
      numArray3[10] = (byte) 3;
      numArray3[44] = (byte) 132;
      numArray3[21] = (byte) 4;
      numArray3[23] = (byte) 8;
      numArray3[14] = (byte) 56;
      numArray3[15] = (byte) 127 /*0x7F*/;
      numArray3[24] = (byte) 221;
      numArray3[25] = (byte) 211;
      numArray3[18] = (byte) 203;
      numArray3[19] = (byte) 75;
      numArray3[20] = (byte) 173;
      numArray3[13] = (byte) 120;
      numArray3[22] = (byte) 68;
      numArray3[42] = (byte) 159;
      numArray3[6] = (byte) 231;
      numArray3[27] = (byte) 230;
      numArray3[32 /*0x20*/] = (byte) 153;
      numArray3[9] = (byte) 32 /*0x20*/;
      numArray3[28] = (byte) 244;
      numArray3[29] = (byte) 209;
      numArray3[1] = (byte) 138;
      numArray3[36] = (byte) 94;
      numArray3[38] = (byte) 96 /*0x60*/;
      numArray3[33] = (byte) 148;
      numArray3[4] = (byte) 216;
      numArray3[30] = (byte) 102;
      numArray3[8] = (byte) 240 /*0xF0*/;
      numArray3[16 /*0x10*/] = (byte) 246;
      numArray3[26] = (byte) 187;
      numArray3[39] = (byte) 221;
      numArray3[40] = (byte) 184;
      numArray3[41] = (byte) 191;
      numArray3[37] = (byte) 161;
      numArray3[43] = (byte) 82;
      numArray3[48 /*0x30*/] = (byte) 240 /*0xF0*/;
      numArray3[45] = (byte) 240 /*0xF0*/;
      numArray3[0] = (byte) 250;
      numArray3[47] = (byte) 94;
      numArray3[17] = (byte) 17;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[49];
    byte[] numArray5 = new byte[49];
    numArray5[18] = (byte) 209;
    numArray5[1] = (byte) 129;
    numArray5[2] = (byte) 231;
    numArray5[28] = (byte) 88;
    numArray5[4] = (byte) 213;
    numArray5[5] = (byte) 88;
    numArray5[37] = (byte) 202;
    numArray5[0] = (byte) 31 /*0x1F*/;
    numArray5[22] = (byte) 79;
    numArray5[9] = (byte) 104;
    numArray5[10] = (byte) 9;
    numArray5[27] = (byte) 135;
    numArray5[21] = (byte) 249;
    numArray5[14] = (byte) 68;
    numArray5[48 /*0x30*/] = (byte) 151;
    numArray5[36] = (byte) 251;
    numArray5[16 /*0x10*/] = (byte) 200;
    numArray5[17] = (byte) 136;
    numArray5[35] = (byte) 37;
    numArray5[6] = (byte) 39;
    numArray5[20] = (byte) 90;
    numArray5[15] = (byte) 191;
    numArray5[33] = (byte) 137;
    numArray5[23] = (byte) 46;
    numArray5[12] = (byte) 149;
    numArray5[25] = (byte) 126;
    numArray5[26] = (byte) 249;
    numArray5[24] = (byte) 157;
    numArray5[3] = (byte) 103;
    numArray5[8] = (byte) 160 /*0xA0*/;
    numArray5[43] = (byte) 59;
    numArray5[31 /*0x1F*/] = (byte) 221;
    numArray5[32 /*0x20*/] = (byte) 76;
    numArray5[19] = (byte) 209;
    numArray5[34] = (byte) 188;
    numArray5[30] = (byte) 52;
    numArray5[29] = (byte) 113;
    numArray5[11] = (byte) 46;
    numArray5[38] = (byte) 73;
    numArray5[39] = (byte) 170;
    numArray5[40] = (byte) 90;
    numArray5[41] = (byte) 222;
    numArray5[7] = (byte) 60;
    numArray5[13] = (byte) 189;
    numArray5[44] = (byte) 100;
    numArray5[47] = (byte) 115;
    numArray5[46] = (byte) 138;
    numArray5[42] = (byte) 5;
    numArray5[45] = (byte) 81;
    byte[] numArray6 = new byte[49]
    {
      (byte) 173,
      (byte) 1,
      (byte) 244,
      (byte) 54,
      (byte) 42,
      (byte) 77,
      (byte) 38,
      (byte) 90,
      (byte) 120,
      (byte) 88,
      (byte) 10,
      (byte) 242,
      (byte) 117,
      (byte) 188,
      (byte) 1,
      (byte) 60,
      (byte) 77,
      (byte) 237,
      (byte) 242,
      (byte) 133,
      (byte) 54,
      (byte) 253,
      (byte) 162,
      (byte) 188,
      (byte) 190,
      (byte) 84,
      (byte) 28,
      (byte) 248,
      (byte) 249,
      (byte) 205,
      (byte) 235,
      (byte) 187,
      (byte) 84,
      (byte) 197,
      (byte) 154,
      (byte) 40,
      (byte) 92,
      (byte) 197,
      (byte) 94,
      byte.MaxValue,
      (byte) 170,
      (byte) 45,
      (byte) 100,
      (byte) 51,
      (byte) 123,
      (byte) 170,
      (byte) 72,
      (byte) 26,
      (byte) 181
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 49);
    for (int index = 0; index < 49; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13423()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[11] = (byte) 125;
      numArray2[2] = (byte) 203;
      numArray2[7] = (byte) 193;
      numArray2[12] = (byte) 98;
      numArray2[4] = (byte) 221;
      numArray2[5] = byte.MaxValue;
      numArray2[3] = (byte) 33;
      numArray2[6] = (byte) 189;
      numArray2[0] = (byte) 114;
      numArray2[9] = (byte) 29;
      numArray2[10] = (byte) 238;
      numArray2[15] = (byte) 26;
      numArray2[1] = (byte) 120;
      numArray2[13] = (byte) 217;
      numArray2[14] = (byte) 175;
      numArray2[8] = (byte) 50;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 146,
        (byte) 15,
        (byte) 82,
        (byte) 116,
        (byte) 2,
        (byte) 70,
        (byte) 173,
        (byte) 15,
        (byte) 143,
        (byte) 185,
        (byte) 70,
        (byte) 227,
        (byte) 213,
        (byte) 65,
        (byte) 238,
        (byte) 41
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18];
      byte[] response = new byte[18];
      Array.Copy((Array) sc_13393.sspq, 491, (Array) numArray4, 0, 18);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 491, (Array) numArray4, 0, 18);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[1] = (byte) 7;
    numArray6[4] = (byte) 36;
    numArray6[11] = (byte) 92;
    numArray6[6] = (byte) 36;
    numArray6[3] = (byte) 74;
    numArray6[5] = (byte) 144 /*0x90*/;
    numArray6[0] = (byte) 191;
    numArray6[7] = (byte) 2;
    numArray6[15] = (byte) 55;
    numArray6[2] = (byte) 76;
    numArray6[10] = (byte) 15;
    numArray6[13] = (byte) 237;
    numArray6[12] = (byte) 160 /*0xA0*/;
    numArray6[9] = (byte) 22;
    numArray6[14] = (byte) 197;
    numArray6[8] = (byte) 63 /*0x3F*/;
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 202,
      (byte) 174,
      (byte) 150,
      (byte) 231,
      (byte) 97,
      (byte) 118,
      (byte) 168,
      (byte) 39,
      (byte) 68,
      (byte) 1,
      (byte) 154,
      (byte) 9,
      (byte) 49,
      (byte) 145,
      (byte) 181,
      (byte) 80 /*0x50*/
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13424(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 179,
      (byte) 11,
      (byte) 168,
      (byte) 146,
      (byte) 88,
      (byte) 75,
      (byte) 76,
      (byte) 198,
      (byte) 248,
      (byte) 227,
      (byte) 161,
      (byte) 173,
      (byte) 212,
      (byte) 194,
      (byte) 78,
      (byte) 4,
      (byte) 2,
      (byte) 54,
      (byte) 139,
      byte.MaxValue,
      (byte) 246,
      (byte) 31 /*0x1F*/,
      (byte) 171,
      (byte) 211,
      (byte) 188,
      (byte) 82,
      (byte) 158,
      (byte) 245,
      (byte) 234,
      (byte) 140,
      (byte) 106,
      (byte) 55,
      (byte) 184,
      (byte) 11,
      (byte) 105,
      (byte) 160 /*0xA0*/,
      (byte) 59,
      (byte) 45,
      (byte) 22,
      (byte) 231,
      (byte) 186,
      (byte) 143,
      (byte) 51,
      (byte) 149,
      (byte) 27,
      (byte) 187,
      (byte) 218,
      (byte) 222
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 178,
      (byte) 33,
      (byte) 254,
      (byte) 121,
      (byte) 80 /*0x50*/,
      (byte) 121,
      (byte) 179,
      (byte) 5,
      (byte) 27,
      (byte) 153,
      (byte) 200,
      (byte) 59,
      (byte) 186,
      (byte) 119,
      (byte) 88,
      (byte) 31 /*0x1F*/,
      (byte) 99,
      (byte) 48 /*0x30*/,
      (byte) 138,
      (byte) 21,
      (byte) 106,
      (byte) 91,
      (byte) 72,
      byte.MaxValue,
      (byte) 27,
      (byte) 235,
      (byte) 181,
      (byte) 111,
      (byte) 156,
      (byte) 232,
      (byte) 182,
      (byte) 46,
      (byte) 78,
      (byte) 42,
      (byte) 69,
      (byte) 78,
      (byte) 37,
      (byte) 89,
      (byte) 139,
      (byte) 135,
      (byte) 67,
      (byte) 161,
      (byte) 94,
      (byte) 200,
      (byte) 131,
      (byte) 52,
      (byte) 169
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13425()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37];
      numArray2[30] = (byte) 33;
      numArray2[32 /*0x20*/] = (byte) 190;
      numArray2[34] = (byte) 14;
      numArray2[0] = (byte) 41;
      numArray2[27] = (byte) 0;
      numArray2[5] = (byte) 213;
      numArray2[6] = (byte) 213;
      numArray2[17] = (byte) 173;
      numArray2[35] = (byte) 171;
      numArray2[9] = (byte) 87;
      numArray2[31 /*0x1F*/] = (byte) 202;
      numArray2[10] = (byte) 220;
      numArray2[12] = (byte) 121;
      numArray2[13] = (byte) 105;
      numArray2[19] = (byte) 213;
      numArray2[15] = (byte) 201;
      numArray2[7] = (byte) 6;
      numArray2[14] = (byte) 244;
      numArray2[18] = (byte) 201;
      numArray2[33] = (byte) 106;
      numArray2[36] = (byte) 214;
      numArray2[21] = (byte) 164;
      numArray2[29] = (byte) 191;
      numArray2[23] = (byte) 103;
      numArray2[24] = (byte) 235;
      numArray2[25] = (byte) 21;
      numArray2[26] = (byte) 183;
      numArray2[4] = (byte) 96 /*0x60*/;
      numArray2[28] = (byte) 119;
      numArray2[16 /*0x10*/] = (byte) 88;
      numArray2[3] = (byte) 148;
      numArray2[11] = (byte) 108;
      numArray2[2] = (byte) 53;
      numArray2[22] = (byte) 68;
      numArray2[20] = (byte) 32 /*0x20*/;
      numArray2[8] = (byte) 173;
      numArray2[1] = (byte) 13;
      byte[] numArray3 = new byte[37]
      {
        (byte) 139,
        (byte) 232,
        (byte) 220,
        (byte) 21,
        (byte) 49,
        (byte) 52,
        (byte) 123,
        (byte) 6,
        (byte) 206,
        (byte) 175,
        (byte) 33,
        (byte) 152,
        (byte) 149,
        (byte) 134,
        (byte) 188,
        (byte) 88,
        (byte) 138,
        (byte) 244,
        (byte) 187,
        (byte) 241,
        (byte) 148,
        (byte) 79,
        (byte) 18,
        (byte) 61,
        (byte) 245,
        (byte) 168,
        (byte) 12,
        (byte) 167,
        (byte) 100,
        (byte) 252,
        (byte) 217,
        (byte) 145,
        (byte) 50,
        (byte) 171,
        (byte) 217,
        (byte) 104,
        (byte) 244
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_13393.sspq, 509, (Array) numArray4, 0, 12);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 509, (Array) numArray4, 0, 12);
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
    byte[] numArray5 = new byte[37];
    byte[] numArray6 = new byte[37]
    {
      (byte) 227,
      (byte) 8,
      (byte) 45,
      (byte) 245,
      (byte) 71,
      (byte) 130,
      (byte) 129,
      (byte) 76,
      (byte) 45,
      (byte) 161,
      (byte) 26,
      (byte) 14,
      (byte) 86,
      (byte) 126,
      (byte) 30,
      (byte) 2,
      (byte) 184,
      (byte) 243,
      (byte) 121,
      (byte) 205,
      (byte) 27,
      (byte) 231,
      (byte) 25,
      (byte) 131,
      (byte) 82,
      (byte) 25,
      (byte) 155,
      (byte) 40,
      (byte) 180,
      (byte) 58,
      (byte) 158,
      (byte) 230,
      (byte) 130,
      (byte) 211,
      (byte) 101,
      (byte) 234,
      (byte) 85
    };
    byte[] numArray7 = new byte[37]
    {
      (byte) 103,
      (byte) 90,
      (byte) 208 /*0xD0*/,
      (byte) 69,
      (byte) 148,
      (byte) 145,
      (byte) 29,
      (byte) 194,
      (byte) 99,
      (byte) 77,
      (byte) 46,
      (byte) 119,
      (byte) 88,
      (byte) 135,
      (byte) 10,
      (byte) 240 /*0xF0*/,
      (byte) 196,
      (byte) 238,
      (byte) 217,
      (byte) 50,
      (byte) 222,
      (byte) 222,
      (byte) 168,
      (byte) 110,
      (byte) 43,
      (byte) 93,
      (byte) 239,
      (byte) 209,
      (byte) 81,
      (byte) 51,
      (byte) 66,
      (byte) 221,
      (byte) 113,
      (byte) 50,
      (byte) 139,
      (byte) 186,
      (byte) 188
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13426()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[10] = (byte) 127 /*0x7F*/;
      numArray2[11] = (byte) 77;
      numArray2[2] = (byte) 18;
      numArray2[5] = (byte) 174;
      numArray2[6] = (byte) 187;
      numArray2[9] = (byte) 66;
      numArray2[14] = (byte) 251;
      numArray2[7] = (byte) 71;
      numArray2[13] = (byte) 108;
      numArray2[1] = (byte) 179;
      numArray2[4] = (byte) 233;
      numArray2[0] = (byte) 29;
      numArray2[3] = (byte) 10;
      numArray2[12] = (byte) 190;
      numArray2[8] = (byte) 54;
      numArray2[15] = (byte) 238;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 99,
        (byte) 86,
        (byte) 31 /*0x1F*/,
        (byte) 142,
        (byte) 0,
        (byte) 191,
        (byte) 82,
        (byte) 93,
        (byte) 14,
        (byte) 249,
        (byte) 159,
        (byte) 217,
        (byte) 183,
        (byte) 191,
        (byte) 235,
        (byte) 196
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
      (byte) 52,
      (byte) 7,
      (byte) 121,
      (byte) 104,
      (byte) 106,
      (byte) 223,
      (byte) 225,
      (byte) 125,
      (byte) 247,
      (byte) 30,
      (byte) 153,
      (byte) 195,
      (byte) 102,
      (byte) 106,
      (byte) 252,
      (byte) 172
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 213,
      (byte) 98,
      (byte) 43,
      (byte) 108,
      (byte) 225,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 56,
      (byte) 100,
      (byte) 112 /*0x70*/,
      (byte) 235,
      (byte) 175,
      (byte) 196,
      (byte) 170,
      (byte) 165,
      (byte) 201
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13427()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[40];
      byte[] numArray2 = new byte[40]
      {
        (byte) 134,
        (byte) 191,
        (byte) 15,
        (byte) 214,
        (byte) 93,
        (byte) 43,
        (byte) 163,
        (byte) 19,
        (byte) 137,
        (byte) 2,
        (byte) 168,
        (byte) 207,
        (byte) 129,
        (byte) 111,
        (byte) 199,
        (byte) 230,
        (byte) 48 /*0x30*/,
        (byte) 58,
        (byte) 195,
        (byte) 7,
        (byte) 119,
        (byte) 25,
        (byte) 252,
        (byte) 228,
        (byte) 154,
        (byte) 201,
        (byte) 29,
        (byte) 173,
        (byte) 55,
        (byte) 0,
        (byte) 106,
        (byte) 233,
        (byte) 104,
        (byte) 5,
        (byte) 119,
        (byte) 116,
        (byte) 247,
        (byte) 162,
        (byte) 10,
        (byte) 63 /*0x3F*/
      };
      byte[] numArray3 = new byte[40]
      {
        (byte) 65,
        (byte) 129,
        (byte) 186,
        (byte) 200,
        (byte) 48 /*0x30*/,
        (byte) 85,
        (byte) 49,
        (byte) 218,
        (byte) 244,
        (byte) 63 /*0x3F*/,
        (byte) 131,
        (byte) 100,
        (byte) 39,
        (byte) 203,
        (byte) 197,
        (byte) 210,
        (byte) 245,
        (byte) 46,
        (byte) 86,
        (byte) 153,
        (byte) 247,
        (byte) 118,
        (byte) 198,
        (byte) 159,
        (byte) 143,
        (byte) 153,
        (byte) 245,
        (byte) 242,
        (byte) 245,
        (byte) 232,
        (byte) 52,
        (byte) 26,
        (byte) 91,
        (byte) 118,
        (byte) 180,
        (byte) 73,
        (byte) 13,
        (byte) 192 /*0xC0*/,
        (byte) 135,
        (byte) 103
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_13393.sspq, 521, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 521, (Array) numArray4, 0, 48 /*0x30*/);
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
    byte[] numArray5 = new byte[40];
    byte[] numArray6 = new byte[40]
    {
      (byte) 102,
      (byte) 118,
      (byte) 48 /*0x30*/,
      (byte) 70,
      (byte) 240 /*0xF0*/,
      (byte) 106,
      (byte) 93,
      (byte) 182,
      (byte) 98,
      (byte) 199,
      byte.MaxValue,
      (byte) 79,
      (byte) 38,
      (byte) 237,
      (byte) 107,
      (byte) 165,
      (byte) 66,
      (byte) 22,
      (byte) 71,
      (byte) 188,
      (byte) 188,
      (byte) 106,
      (byte) 13,
      (byte) 231,
      (byte) 234,
      (byte) 186,
      (byte) 4,
      (byte) 200,
      (byte) 0,
      (byte) 12,
      (byte) 53,
      (byte) 237,
      (byte) 162,
      (byte) 34,
      (byte) 38,
      (byte) 184,
      (byte) 83,
      (byte) 132,
      (byte) 185,
      (byte) 186
    };
    byte[] numArray7 = new byte[40];
    numArray7[36] = (byte) 136;
    numArray7[23] = (byte) 6;
    numArray7[7] = (byte) 246;
    numArray7[2] = (byte) 189;
    numArray7[4] = (byte) 204;
    numArray7[1] = (byte) 224 /*0xE0*/;
    numArray7[5] = (byte) 26;
    numArray7[19] = (byte) 176 /*0xB0*/;
    numArray7[17] = (byte) 238;
    numArray7[9] = (byte) 209;
    numArray7[10] = (byte) 218;
    numArray7[3] = (byte) 52;
    numArray7[12] = (byte) 61;
    numArray7[13] = (byte) 154;
    numArray7[14] = (byte) 179;
    numArray7[8] = (byte) 181;
    numArray7[16 /*0x10*/] = (byte) 122;
    numArray7[35] = (byte) 23;
    numArray7[18] = (byte) 69;
    numArray7[31 /*0x1F*/] = (byte) 21;
    numArray7[20] = (byte) 22;
    numArray7[21] = (byte) 163;
    numArray7[29] = (byte) 243;
    numArray7[26] = (byte) 175;
    numArray7[24] = (byte) 110;
    numArray7[22] = (byte) 75;
    numArray7[39] = (byte) 6;
    numArray7[27] = (byte) 118;
    numArray7[28] = (byte) 51;
    numArray7[11] = (byte) 89;
    numArray7[6] = (byte) 70;
    numArray7[0] = (byte) 163;
    numArray7[32 /*0x20*/] = (byte) 85;
    numArray7[33] = (byte) 195;
    numArray7[34] = (byte) 2;
    numArray7[38] = (byte) 185;
    numArray7[25] = (byte) 198;
    numArray7[37] = (byte) 182;
    numArray7[15] = (byte) 217;
    numArray7[30] = (byte) 153;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 40);
    for (int index = 0; index < 40; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13428()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 244,
        (byte) 6,
        (byte) 42,
        (byte) 173,
        (byte) 18,
        (byte) 42,
        (byte) 79,
        (byte) 10,
        (byte) 40,
        (byte) 131,
        (byte) 84,
        (byte) 20,
        (byte) 1,
        (byte) 24,
        (byte) 45,
        (byte) 21
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 91,
        (byte) 9,
        (byte) 25,
        (byte) 112 /*0x70*/,
        (byte) 54,
        (byte) 166,
        (byte) 120,
        (byte) 253,
        (byte) 83,
        (byte) 34,
        (byte) 242,
        (byte) 8,
        (byte) 110,
        (byte) 10,
        (byte) 106,
        (byte) 117
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[29];
      byte[] response = new byte[29];
      Array.Copy((Array) sc_13393.sspq, 569, (Array) numArray4, 0, 29);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 569, (Array) numArray4, 0, 29);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 171,
      (byte) 34,
      (byte) 200,
      (byte) 157,
      (byte) 179,
      (byte) 32 /*0x20*/,
      (byte) 155,
      (byte) 51,
      (byte) 243,
      (byte) 203,
      (byte) 8,
      (byte) 41,
      (byte) 52,
      (byte) 130,
      (byte) 252,
      (byte) 12
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 123,
      (byte) 200,
      (byte) 179,
      (byte) 144 /*0x90*/,
      (byte) 218,
      (byte) 76,
      (byte) 127 /*0x7F*/,
      (byte) 49,
      (byte) 72,
      (byte) 137,
      (byte) 103,
      (byte) 191,
      (byte) 104,
      (byte) 114,
      (byte) 16 /*0x10*/,
      (byte) 90
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13429(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 196;
    sourceArray1[6] = (byte) 195;
    sourceArray1[2] = (byte) 5;
    sourceArray1[19] = (byte) 197;
    sourceArray1[45] = (byte) 12;
    sourceArray1[8] = (byte) 254;
    sourceArray1[21] = (byte) 243;
    sourceArray1[0] = (byte) 46;
    sourceArray1[39] = (byte) 161;
    sourceArray1[7] = (byte) 248;
    sourceArray1[10] = (byte) 6;
    sourceArray1[9] = (byte) 228;
    sourceArray1[3] = (byte) 50;
    sourceArray1[43] = (byte) 230;
    sourceArray1[14] = (byte) 10;
    sourceArray1[15] = (byte) 173;
    sourceArray1[25] = (byte) 47;
    sourceArray1[33] = (byte) 126;
    sourceArray1[17] = (byte) 124;
    sourceArray1[12] = (byte) 250;
    sourceArray1[47] = (byte) 165;
    sourceArray1[22] = (byte) 51;
    sourceArray1[1] = (byte) 13;
    sourceArray1[5] = (byte) 127 /*0x7F*/;
    sourceArray1[30] = (byte) 25;
    sourceArray1[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    sourceArray1[34] = (byte) 124;
    sourceArray1[27] = (byte) 153;
    sourceArray1[28] = (byte) 204;
    sourceArray1[4] = (byte) 76;
    sourceArray1[13] = (byte) 221;
    sourceArray1[37] = (byte) 106;
    sourceArray1[32 /*0x20*/] = (byte) 43;
    sourceArray1[41] = (byte) 59;
    sourceArray1[11] = (byte) 29;
    sourceArray1[35] = (byte) 20;
    sourceArray1[36] = (byte) 194;
    sourceArray1[38] = (byte) 103;
    sourceArray1[29] = (byte) 221;
    sourceArray1[18] = (byte) 173;
    sourceArray1[40] = (byte) 216;
    sourceArray1[16 /*0x10*/] = (byte) 92;
    sourceArray1[42] = (byte) 245;
    sourceArray1[24] = (byte) 115;
    sourceArray1[44] = (byte) 1;
    sourceArray1[20] = (byte) 88;
    sourceArray1[46] = (byte) 57;
    sourceArray1[23] = (byte) 232;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 69,
      (byte) 87,
      (byte) 100,
      (byte) 35,
      (byte) 114,
      (byte) 0,
      (byte) 6,
      (byte) 19,
      (byte) 83,
      (byte) 97,
      (byte) 17,
      (byte) 245,
      (byte) 151,
      (byte) 79,
      (byte) 124,
      (byte) 85,
      (byte) 104,
      (byte) 46,
      (byte) 232,
      (byte) 28,
      (byte) 121,
      (byte) 115,
      (byte) 23,
      (byte) 223,
      (byte) 23,
      (byte) 78,
      (byte) 80 /*0x50*/,
      (byte) 151,
      (byte) 231,
      (byte) 113,
      (byte) 34,
      (byte) 86,
      (byte) 29,
      (byte) 209,
      (byte) 100,
      (byte) 88,
      (byte) 228,
      (byte) 215,
      (byte) 172,
      (byte) 82,
      (byte) 46,
      (byte) 65,
      (byte) 73,
      (byte) 0,
      (byte) 111,
      (byte) 161,
      (byte) 144 /*0x90*/,
      (byte) 140
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13430()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 10;
      numArray2[1] = (byte) 188;
      numArray2[4] = (byte) 115;
      numArray2[3] = (byte) 252;
      numArray2[2] = (byte) 69;
      numArray2[6] = (byte) 81;
      numArray2[0] = (byte) 25;
      numArray2[7] = (byte) 120;
      numArray2[9] = (byte) 44;
      numArray2[8] = (byte) 4;
      byte[] numArray3 = new byte[10]
      {
        (byte) 57,
        (byte) 36,
        (byte) 53,
        (byte) 100,
        (byte) 197,
        (byte) 123,
        byte.MaxValue,
        (byte) 230,
        (byte) 170,
        (byte) 3
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
      (byte) 30,
      (byte) 187,
      (byte) 70,
      (byte) 72,
      (byte) 202,
      (byte) 37,
      (byte) 63 /*0x3F*/,
      (byte) 13,
      (byte) 40,
      (byte) 50
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 27,
      (byte) 237,
      (byte) 86,
      (byte) 237,
      (byte) 6,
      (byte) 187,
      (byte) 53,
      (byte) 142,
      (byte) 64 /*0x40*/,
      (byte) 134
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[33];
    byte[] response = new byte[33];
    Array.Copy((Array) sc_13393.sspq, 598, (Array) numArray7, 0, 33);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13393.sspr, 598, (Array) numArray7, 0, 33);
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

  internal static string ssp_appserver_13431()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[53];
      byte[] numArray2 = new byte[53];
      numArray2[50] = (byte) 41;
      numArray2[1] = (byte) 195;
      numArray2[2] = (byte) 106;
      numArray2[3] = (byte) 185;
      numArray2[24] = (byte) 217;
      numArray2[15] = (byte) 147;
      numArray2[6] = (byte) 62;
      numArray2[13] = (byte) 19;
      numArray2[8] = (byte) 13;
      numArray2[52] = (byte) 29;
      numArray2[10] = (byte) 246;
      numArray2[11] = (byte) 165;
      numArray2[12] = (byte) 217;
      numArray2[38] = (byte) 198;
      numArray2[25] = (byte) 126;
      numArray2[16 /*0x10*/] = (byte) 48 /*0x30*/;
      numArray2[51] = (byte) 221;
      numArray2[17] = (byte) 60;
      numArray2[18] = (byte) 237;
      numArray2[47] = (byte) 178;
      numArray2[20] = (byte) 180;
      numArray2[21] = (byte) 106;
      numArray2[36] = (byte) 42;
      numArray2[0] = (byte) 214;
      numArray2[5] = (byte) 132;
      numArray2[48 /*0x30*/] = (byte) 98;
      numArray2[26] = (byte) 111;
      numArray2[27] = (byte) 234;
      numArray2[19] = (byte) 196;
      numArray2[29] = (byte) 83;
      numArray2[42] = (byte) 51;
      numArray2[31 /*0x1F*/] = (byte) 71;
      numArray2[9] = (byte) 215;
      numArray2[22] = (byte) 204;
      numArray2[44] = (byte) 204;
      numArray2[30] = (byte) 84;
      numArray2[35] = (byte) 141;
      numArray2[33] = (byte) 139;
      numArray2[43] = (byte) 86;
      numArray2[39] = (byte) 219;
      numArray2[40] = (byte) 55;
      numArray2[23] = (byte) 18;
      numArray2[14] = (byte) 188;
      numArray2[4] = (byte) 25;
      numArray2[37] = (byte) 71;
      numArray2[45] = (byte) 225;
      numArray2[46] = (byte) 122;
      numArray2[7] = (byte) 25;
      numArray2[28] = (byte) 240 /*0xF0*/;
      numArray2[49] = (byte) 128 /*0x80*/;
      numArray2[34] = (byte) 85;
      numArray2[41] = (byte) 12;
      numArray2[32 /*0x20*/] = (byte) 57;
      byte[] numArray3 = new byte[53]
      {
        (byte) 106,
        (byte) 108,
        (byte) 144 /*0x90*/,
        (byte) 237,
        (byte) 150,
        (byte) 98,
        (byte) 174,
        (byte) 48 /*0x30*/,
        (byte) 5,
        (byte) 174,
        (byte) 116,
        (byte) 25,
        (byte) 31 /*0x1F*/,
        (byte) 23,
        (byte) 226,
        (byte) 22,
        (byte) 229,
        (byte) 73,
        (byte) 134,
        (byte) 199,
        (byte) 157,
        (byte) 172,
        (byte) 124,
        (byte) 186,
        (byte) 127 /*0x7F*/,
        (byte) 181,
        (byte) 222,
        (byte) 183,
        (byte) 140,
        (byte) 246,
        (byte) 208 /*0xD0*/,
        (byte) 193,
        (byte) 50,
        (byte) 61,
        (byte) 192 /*0xC0*/,
        (byte) 1,
        (byte) 187,
        (byte) 76,
        (byte) 141,
        (byte) 182,
        (byte) 211,
        (byte) 97,
        (byte) 32 /*0x20*/,
        (byte) 102,
        (byte) 155,
        (byte) 37,
        (byte) 200,
        (byte) 168,
        (byte) 12,
        (byte) 205,
        (byte) 246,
        (byte) 95,
        (byte) 7
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[53];
    byte[] numArray5 = new byte[53];
    numArray5[2] = (byte) 129;
    numArray5[1] = (byte) 52;
    numArray5[51] = (byte) 189;
    numArray5[25] = (byte) 129;
    numArray5[32 /*0x20*/] = (byte) 29;
    numArray5[5] = (byte) 149;
    numArray5[16 /*0x10*/] = (byte) 150;
    numArray5[12] = (byte) 90;
    numArray5[18] = (byte) 75;
    numArray5[6] = (byte) 88;
    numArray5[41] = (byte) 23;
    numArray5[11] = (byte) 133;
    numArray5[4] = (byte) 232;
    numArray5[23] = (byte) 161;
    numArray5[27] = (byte) 200;
    numArray5[15] = (byte) 152;
    numArray5[46] = (byte) 27;
    numArray5[17] = (byte) 130;
    numArray5[22] = (byte) 190;
    numArray5[7] = (byte) 204;
    numArray5[20] = (byte) 168;
    numArray5[21] = (byte) 196;
    numArray5[14] = (byte) 212;
    numArray5[48 /*0x30*/] = (byte) 32 /*0x20*/;
    numArray5[24] = (byte) 40;
    numArray5[19] = (byte) 75;
    numArray5[26] = (byte) 215;
    numArray5[30] = (byte) 233;
    numArray5[28] = (byte) 83;
    numArray5[29] = (byte) 89;
    numArray5[8] = (byte) 184;
    numArray5[49] = (byte) 155;
    numArray5[31 /*0x1F*/] = (byte) 17;
    numArray5[33] = (byte) 118;
    numArray5[34] = (byte) 27;
    numArray5[35] = (byte) 181;
    numArray5[36] = (byte) 19;
    numArray5[37] = (byte) 193;
    numArray5[38] = (byte) 126;
    numArray5[50] = (byte) 128 /*0x80*/;
    numArray5[3] = (byte) 17;
    numArray5[39] = (byte) 150;
    numArray5[42] = (byte) 42;
    numArray5[43] = (byte) 233;
    numArray5[44] = (byte) 201;
    numArray5[45] = (byte) 152;
    numArray5[40] = (byte) 109;
    numArray5[47] = (byte) 95;
    numArray5[0] = (byte) 100;
    numArray5[10] = (byte) 10;
    numArray5[9] = (byte) 144 /*0x90*/;
    numArray5[13] = (byte) 205;
    numArray5[52] = (byte) 26;
    byte[] numArray6 = new byte[53]
    {
      (byte) 88,
      (byte) 40,
      (byte) 89,
      (byte) 116,
      (byte) 165,
      (byte) 36,
      (byte) 166,
      (byte) 155,
      (byte) 11,
      (byte) 79,
      (byte) 232,
      (byte) 191,
      (byte) 218,
      (byte) 212,
      (byte) 134,
      (byte) 226,
      (byte) 15,
      (byte) 241,
      (byte) 23,
      (byte) 124,
      (byte) 43,
      (byte) 186,
      (byte) 64 /*0x40*/,
      (byte) 199,
      (byte) 129,
      (byte) 157,
      (byte) 8,
      (byte) 204,
      (byte) 175,
      (byte) 43,
      (byte) 23,
      (byte) 185,
      (byte) 34,
      (byte) 77,
      (byte) 249,
      (byte) 83,
      (byte) 27,
      (byte) 67,
      (byte) 158,
      (byte) 247,
      (byte) 61,
      (byte) 240 /*0xF0*/,
      (byte) 137,
      (byte) 231,
      (byte) 30,
      (byte) 161,
      (byte) 186,
      (byte) 122,
      (byte) 44,
      (byte) 112 /*0x70*/,
      (byte) 183,
      (byte) 205,
      (byte) 4
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 53);
    for (int index = 0; index < 53; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13432()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[64 /*0x40*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 5,
        (byte) 161,
        (byte) 9,
        (byte) 88,
        (byte) 0,
        (byte) 70,
        (byte) 232,
        (byte) 191,
        (byte) 133,
        (byte) 122,
        (byte) 1,
        (byte) 12,
        (byte) 97,
        (byte) 53,
        (byte) 223,
        (byte) 81,
        (byte) 252,
        (byte) 236,
        (byte) 220,
        (byte) 2,
        (byte) 241,
        (byte) 205,
        (byte) 145,
        (byte) 6,
        (byte) 96 /*0x60*/,
        (byte) 47,
        (byte) 201,
        (byte) 167,
        (byte) 162,
        (byte) 44,
        (byte) 113,
        (byte) 144 /*0x90*/,
        (byte) 122,
        (byte) 33,
        (byte) 117,
        (byte) 158,
        (byte) 42,
        (byte) 127 /*0x7F*/,
        (byte) 22,
        (byte) 146,
        (byte) 244,
        (byte) 223,
        (byte) 70,
        (byte) 200,
        (byte) 207,
        (byte) 35,
        (byte) 213,
        (byte) 120,
        (byte) 33,
        (byte) 196,
        (byte) 175,
        (byte) 205,
        (byte) 137,
        (byte) 224 /*0xE0*/,
        (byte) 112 /*0x70*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 185,
        (byte) 249,
        (byte) 228,
        (byte) 6,
        (byte) 112 /*0x70*/,
        (byte) 63 /*0x3F*/,
        (byte) 158,
        (byte) 48 /*0x30*/,
        (byte) 159,
        (byte) 79,
        (byte) 75,
        (byte) 78,
        (byte) 105,
        (byte) 234,
        (byte) 165,
        (byte) 54,
        (byte) 96 /*0x60*/,
        (byte) 68,
        (byte) 19,
        (byte) 206,
        (byte) 243,
        (byte) 205,
        (byte) 109,
        (byte) 102,
        (byte) 223,
        (byte) 46,
        (byte) 92,
        (byte) 187,
        (byte) 50,
        (byte) 226,
        (byte) 6,
        (byte) 183,
        (byte) 90,
        (byte) 10,
        (byte) 41,
        (byte) 235,
        (byte) 140,
        (byte) 223,
        (byte) 101,
        (byte) 224 /*0xE0*/,
        (byte) 197,
        (byte) 203,
        (byte) 106,
        (byte) 94,
        (byte) 64 /*0x40*/,
        (byte) 1,
        (byte) 117,
        (byte) 155,
        (byte) 184,
        (byte) 226,
        (byte) 6,
        (byte) 39,
        (byte) 28,
        (byte) 184,
        (byte) 18
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[9]
      {
        (byte) 2,
        (byte) 247,
        (byte) 209,
        (byte) 124,
        (byte) 65,
        (byte) 28,
        (byte) 88,
        (byte) 8,
        (byte) 113
      };
      byte[] numArray5 = new byte[9];
      numArray5[5] = (byte) 71;
      numArray5[1] = (byte) 98;
      numArray5[2] = (byte) 80 /*0x50*/;
      numArray5[7] = (byte) 58;
      numArray5[4] = (byte) 97;
      numArray5[8] = (byte) 149;
      numArray5[6] = (byte) 162;
      numArray5[0] = (byte) 239;
      numArray5[3] = (byte) 253;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[34];
      byte[] response = new byte[34];
      Array.Copy((Array) sc_13393.sspq, 631, (Array) numArray6, 0, 34);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13393.sspr, 631, (Array) numArray6, 0, 34);
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
    byte[] numArray7 = new byte[64 /*0x40*/];
    byte[] numArray8 = new byte[55]
    {
      (byte) 186,
      (byte) 120,
      (byte) 43,
      (byte) 187,
      (byte) 62,
      (byte) 219,
      (byte) 99,
      (byte) 105,
      (byte) 248,
      (byte) 142,
      (byte) 130,
      (byte) 178,
      byte.MaxValue,
      (byte) 245,
      (byte) 145,
      (byte) 53,
      (byte) 91,
      (byte) 78,
      (byte) 154,
      (byte) 8,
      (byte) 216,
      (byte) 172,
      (byte) 195,
      (byte) 152,
      (byte) 165,
      (byte) 63 /*0x3F*/,
      (byte) 75,
      (byte) 174,
      (byte) 200,
      (byte) 199,
      (byte) 83,
      (byte) 141,
      (byte) 72,
      (byte) 91,
      (byte) 205,
      (byte) 204,
      (byte) 233,
      (byte) 247,
      (byte) 132,
      (byte) 18,
      (byte) 93,
      (byte) 244,
      (byte) 152,
      (byte) 7,
      (byte) 54,
      (byte) 45,
      (byte) 158,
      (byte) 61,
      (byte) 100,
      (byte) 9,
      (byte) 118,
      (byte) 15,
      (byte) 22,
      (byte) 61,
      (byte) 244
    };
    byte[] numArray9 = new byte[55];
    numArray9[6] = (byte) 105;
    numArray9[2] = (byte) 130;
    numArray9[23] = (byte) 214;
    numArray9[16 /*0x10*/] = (byte) 98;
    numArray9[36] = (byte) 3;
    numArray9[5] = (byte) 243;
    numArray9[1] = (byte) 214;
    numArray9[12] = (byte) 9;
    numArray9[8] = (byte) 240 /*0xF0*/;
    numArray9[40] = (byte) 175;
    numArray9[10] = (byte) 219;
    numArray9[7] = (byte) 88;
    numArray9[39] = (byte) 155;
    numArray9[18] = (byte) 154;
    numArray9[35] = (byte) 80 /*0x50*/;
    numArray9[25] = (byte) 223;
    numArray9[27] = (byte) 244;
    numArray9[9] = (byte) 99;
    numArray9[26] = (byte) 23;
    numArray9[19] = (byte) 102;
    numArray9[20] = (byte) 251;
    numArray9[21] = (byte) 90;
    numArray9[22] = (byte) 148;
    numArray9[53] = (byte) 177;
    numArray9[24] = (byte) 12;
    numArray9[3] = (byte) 219;
    numArray9[33] = (byte) 65;
    numArray9[51] = (byte) 232;
    numArray9[28] = (byte) 40;
    numArray9[34] = (byte) 159;
    numArray9[48 /*0x30*/] = (byte) 225;
    numArray9[31 /*0x1F*/] = (byte) 201;
    numArray9[29] = (byte) 245;
    numArray9[15] = (byte) 73;
    numArray9[14] = (byte) 116;
    numArray9[44] = (byte) 34;
    numArray9[0] = (byte) 242;
    numArray9[37] = (byte) 225;
    numArray9[30] = (byte) 55;
    numArray9[50] = (byte) 20;
    numArray9[4] = (byte) 201;
    numArray9[41] = (byte) 228;
    numArray9[42] = (byte) 180;
    numArray9[43] = (byte) 252;
    numArray9[52] = (byte) 139;
    numArray9[38] = (byte) 71;
    numArray9[46] = (byte) 131;
    numArray9[47] = (byte) 163;
    numArray9[17] = (byte) 13;
    numArray9[49] = (byte) 44;
    numArray9[11] = (byte) 191;
    numArray9[45] = (byte) 31 /*0x1F*/;
    numArray9[32 /*0x20*/] = (byte) 167;
    numArray9[13] = (byte) 3;
    numArray9[54] = (byte) 20;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[9]
    {
      (byte) 49,
      (byte) 158,
      (byte) 93,
      (byte) 130,
      (byte) 106,
      (byte) 226,
      (byte) 223,
      (byte) 247,
      (byte) 126
    };
    byte[] numArray11 = new byte[9]
    {
      (byte) 191,
      (byte) 107,
      (byte) 50,
      (byte) 87,
      (byte) 14,
      (byte) 244,
      (byte) 211,
      (byte) 166,
      (byte) 43
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 9);
    for (int index = 0; index < 9; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13433()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[51];
      byte[] numArray2 = new byte[51]
      {
        (byte) 244,
        (byte) 131,
        (byte) 68,
        (byte) 6,
        (byte) 2,
        (byte) 143,
        (byte) 171,
        (byte) 239,
        (byte) 32 /*0x20*/,
        (byte) 179,
        (byte) 167,
        (byte) 247,
        (byte) 170,
        (byte) 0,
        (byte) 62,
        (byte) 254,
        (byte) 232,
        (byte) 120,
        (byte) 85,
        (byte) 198,
        (byte) 30,
        (byte) 37,
        (byte) 67,
        (byte) 39,
        (byte) 254,
        (byte) 61,
        (byte) 112 /*0x70*/,
        (byte) 136,
        (byte) 212,
        (byte) 194,
        (byte) 101,
        (byte) 188,
        (byte) 175,
        (byte) 242,
        (byte) 201,
        (byte) 143,
        (byte) 0,
        (byte) 135,
        (byte) 143,
        (byte) 129,
        (byte) 78,
        (byte) 9,
        (byte) 17,
        (byte) 234,
        (byte) 182,
        (byte) 139,
        (byte) 192 /*0xC0*/,
        (byte) 226,
        (byte) 161,
        (byte) 249,
        (byte) 150
      };
      byte[] numArray3 = new byte[51];
      numArray3[42] = (byte) 98;
      numArray3[17] = (byte) 74;
      numArray3[35] = (byte) 67;
      numArray3[12] = (byte) 178;
      numArray3[4] = (byte) 135;
      numArray3[5] = (byte) 100;
      numArray3[6] = (byte) 249;
      numArray3[2] = (byte) 182;
      numArray3[24] = (byte) 94;
      numArray3[47] = (byte) 188;
      numArray3[10] = (byte) 189;
      numArray3[32 /*0x20*/] = (byte) 247;
      numArray3[9] = (byte) 231;
      numArray3[25] = (byte) 85;
      numArray3[3] = (byte) 117;
      numArray3[15] = (byte) 241;
      numArray3[16 /*0x10*/] = (byte) 61;
      numArray3[7] = (byte) 64 /*0x40*/;
      numArray3[29] = (byte) 59;
      numArray3[46] = (byte) 92;
      numArray3[22] = (byte) 72;
      numArray3[20] = (byte) 200;
      numArray3[26] = (byte) 238;
      numArray3[23] = (byte) 0;
      numArray3[43] = (byte) 172;
      numArray3[1] = (byte) 0;
      numArray3[31 /*0x1F*/] = (byte) 78;
      numArray3[27] = (byte) 107;
      numArray3[44] = (byte) 0;
      numArray3[18] = (byte) 197;
      numArray3[30] = (byte) 113;
      numArray3[41] = (byte) 85;
      numArray3[28] = (byte) 25;
      numArray3[13] = (byte) 157;
      numArray3[34] = (byte) 225;
      numArray3[8] = (byte) 225;
      numArray3[36] = (byte) 187;
      numArray3[14] = (byte) 109;
      numArray3[38] = (byte) 95;
      numArray3[39] = (byte) 105;
      numArray3[40] = (byte) 109;
      numArray3[0] = (byte) 177;
      numArray3[11] = (byte) 132;
      numArray3[37] = (byte) 138;
      numArray3[33] = (byte) 143;
      numArray3[45] = (byte) 78;
      numArray3[19] = (byte) 184;
      numArray3[21] = (byte) 147;
      numArray3[48 /*0x30*/] = (byte) 5;
      numArray3[49] = (byte) 107;
      numArray3[50] = (byte) 162;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 51);
      for (int index = 0; index < 51; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[51];
    byte[] numArray5 = new byte[51];
    numArray5[18] = (byte) 59;
    numArray5[26] = (byte) 96 /*0x60*/;
    numArray5[44] = (byte) 210;
    numArray5[30] = (byte) 115;
    numArray5[4] = (byte) 120;
    numArray5[5] = (byte) 187;
    numArray5[11] = (byte) 59;
    numArray5[43] = (byte) 129;
    numArray5[8] = (byte) 147;
    numArray5[35] = (byte) 129;
    numArray5[10] = (byte) 144 /*0x90*/;
    numArray5[20] = (byte) 26;
    numArray5[12] = (byte) 64 /*0x40*/;
    numArray5[13] = (byte) 48 /*0x30*/;
    numArray5[14] = (byte) 206;
    numArray5[29] = (byte) 48 /*0x30*/;
    numArray5[27] = (byte) 188;
    numArray5[39] = (byte) 63 /*0x3F*/;
    numArray5[17] = (byte) 83;
    numArray5[49] = (byte) 182;
    numArray5[42] = (byte) 236;
    numArray5[31 /*0x1F*/] = (byte) 226;
    numArray5[22] = (byte) 30;
    numArray5[23] = (byte) 114;
    numArray5[24] = (byte) 75;
    numArray5[7] = (byte) 172;
    numArray5[3] = (byte) 41;
    numArray5[40] = (byte) 103;
    numArray5[28] = (byte) 164;
    numArray5[1] = (byte) 253;
    numArray5[19] = (byte) 95;
    numArray5[25] = (byte) 39;
    numArray5[32 /*0x20*/] = (byte) 244;
    numArray5[45] = (byte) 240 /*0xF0*/;
    numArray5[34] = (byte) 246;
    numArray5[33] = (byte) 223;
    numArray5[36] = (byte) 193;
    numArray5[37] = (byte) 229;
    numArray5[38] = (byte) 235;
    numArray5[21] = (byte) 223;
    numArray5[16 /*0x10*/] = (byte) 57;
    numArray5[41] = (byte) 54;
    numArray5[2] = (byte) 19;
    numArray5[50] = (byte) 9;
    numArray5[6] = (byte) 231;
    numArray5[9] = (byte) 72;
    numArray5[46] = (byte) 173;
    numArray5[47] = (byte) 131;
    numArray5[48 /*0x30*/] = (byte) 206;
    numArray5[15] = (byte) 21;
    numArray5[0] = (byte) 53;
    byte[] numArray6 = new byte[51]
    {
      (byte) 9,
      (byte) 90,
      (byte) 69,
      (byte) 250,
      (byte) 201,
      (byte) 151,
      (byte) 165,
      (byte) 227,
      (byte) 249,
      (byte) 167,
      (byte) 70,
      (byte) 21,
      (byte) 39,
      (byte) 146,
      (byte) 67,
      (byte) 71,
      (byte) 95,
      (byte) 83,
      (byte) 236,
      (byte) 251,
      (byte) 160 /*0xA0*/,
      (byte) 237,
      (byte) 133,
      (byte) 155,
      (byte) 229,
      (byte) 106,
      (byte) 106,
      (byte) 20,
      (byte) 157,
      (byte) 237,
      (byte) 84,
      (byte) 60,
      (byte) 233,
      (byte) 23,
      (byte) 216,
      (byte) 204,
      (byte) 251,
      (byte) 176 /*0xB0*/,
      (byte) 197,
      (byte) 22,
      (byte) 211,
      (byte) 226,
      (byte) 87,
      (byte) 154,
      (byte) 118,
      (byte) 189,
      (byte) 241,
      (byte) 229,
      (byte) 144 /*0x90*/,
      (byte) 231,
      (byte) 203
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 51);
    for (int index = 0; index < 51; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13434(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 116,
      (byte) 155,
      (byte) 252,
      (byte) 98,
      (byte) 67,
      (byte) 1,
      (byte) 130,
      (byte) 215,
      (byte) 124,
      (byte) 61,
      (byte) 25,
      (byte) 60,
      (byte) 23,
      (byte) 59,
      (byte) 230,
      (byte) 98,
      (byte) 152,
      (byte) 170,
      (byte) 51,
      (byte) 77,
      (byte) 204,
      (byte) 22,
      (byte) 187,
      (byte) 181,
      (byte) 106,
      (byte) 240 /*0xF0*/,
      (byte) 123,
      (byte) 178,
      (byte) 45,
      (byte) 148,
      (byte) 108,
      (byte) 139,
      (byte) 114,
      (byte) 110,
      (byte) 253,
      (byte) 197,
      (byte) 2,
      (byte) 62,
      (byte) 94,
      (byte) 31 /*0x1F*/,
      (byte) 132,
      (byte) 195,
      (byte) 104,
      byte.MaxValue,
      (byte) 21,
      (byte) 76,
      (byte) 148,
      (byte) 232
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 91;
    sourceArray2[6] = (byte) 215;
    sourceArray2[2] = (byte) 143;
    sourceArray2[18] = (byte) 71;
    sourceArray2[42] = (byte) 218;
    sourceArray2[5] = (byte) 134;
    sourceArray2[20] = (byte) 113;
    sourceArray2[12] = (byte) 127 /*0x7F*/;
    sourceArray2[14] = (byte) 22;
    sourceArray2[34] = (byte) 125;
    sourceArray2[10] = (byte) 141;
    sourceArray2[1] = (byte) 217;
    sourceArray2[23] = (byte) 107;
    sourceArray2[13] = (byte) 143;
    sourceArray2[44] = (byte) 150;
    sourceArray2[30] = (byte) 210;
    sourceArray2[16 /*0x10*/] = (byte) 199;
    sourceArray2[8] = (byte) 4;
    sourceArray2[47] = (byte) 198;
    sourceArray2[24] = (byte) 8;
    sourceArray2[11] = (byte) 97;
    sourceArray2[21] = (byte) 243;
    sourceArray2[22] = (byte) 132;
    sourceArray2[31 /*0x1F*/] = (byte) 152;
    sourceArray2[17] = (byte) 121;
    sourceArray2[32 /*0x20*/] = (byte) 151;
    sourceArray2[4] = (byte) 183;
    sourceArray2[27] = (byte) 125;
    sourceArray2[28] = (byte) 143;
    sourceArray2[29] = (byte) 197;
    sourceArray2[3] = (byte) 228;
    sourceArray2[26] = (byte) 84;
    sourceArray2[0] = (byte) 183;
    sourceArray2[33] = (byte) 30;
    sourceArray2[9] = (byte) 133;
    sourceArray2[39] = (byte) 160 /*0xA0*/;
    sourceArray2[36] = (byte) 165;
    sourceArray2[37] = (byte) 83;
    sourceArray2[38] = (byte) 113;
    sourceArray2[41] = (byte) 106;
    sourceArray2[40] = (byte) 202;
    sourceArray2[15] = (byte) 152;
    sourceArray2[19] = (byte) 124;
    sourceArray2[43] = (byte) 36;
    sourceArray2[25] = (byte) 104;
    sourceArray2[45] = (byte) 51;
    sourceArray2[46] = (byte) 227;
    sourceArray2[35] = (byte) 110;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13435(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 113;
    sourceArray1[22] = (byte) 159;
    sourceArray1[45] = (byte) 22;
    sourceArray1[17] = (byte) 4;
    sourceArray1[25] = (byte) 20;
    sourceArray1[42] = (byte) 115;
    sourceArray1[14] = (byte) 242;
    sourceArray1[28] = (byte) 21;
    sourceArray1[8] = (byte) 120;
    sourceArray1[1] = (byte) 169;
    sourceArray1[2] = (byte) 161;
    sourceArray1[0] = (byte) 235;
    sourceArray1[3] = (byte) 137;
    sourceArray1[13] = (byte) 245;
    sourceArray1[4] = (byte) 86;
    sourceArray1[41] = (byte) 109;
    sourceArray1[5] = (byte) 138;
    sourceArray1[37] = (byte) 186;
    sourceArray1[18] = (byte) 59;
    sourceArray1[24] = (byte) 163;
    sourceArray1[11] = (byte) 165;
    sourceArray1[20] = (byte) 191;
    sourceArray1[15] = (byte) 223;
    sourceArray1[23] = (byte) 58;
    sourceArray1[6] = (byte) 27;
    sourceArray1[30] = (byte) 45;
    sourceArray1[26] = (byte) 63 /*0x3F*/;
    sourceArray1[27] = (byte) 122;
    sourceArray1[7] = (byte) 157;
    sourceArray1[21] = (byte) 188;
    sourceArray1[32 /*0x20*/] = (byte) 25;
    sourceArray1[16 /*0x10*/] = (byte) 101;
    sourceArray1[9] = (byte) 176 /*0xB0*/;
    sourceArray1[33] = (byte) 155;
    sourceArray1[31 /*0x1F*/] = (byte) 142;
    sourceArray1[35] = (byte) 233;
    sourceArray1[36] = (byte) 79;
    sourceArray1[19] = (byte) 50;
    sourceArray1[38] = (byte) 141;
    sourceArray1[39] = (byte) 11;
    sourceArray1[40] = (byte) 64 /*0x40*/;
    sourceArray1[43] = (byte) 238;
    sourceArray1[12] = (byte) 218;
    sourceArray1[29] = (byte) 240 /*0xF0*/;
    sourceArray1[44] = (byte) 194;
    sourceArray1[10] = (byte) 35;
    sourceArray1[46] = (byte) 106;
    sourceArray1[47] = (byte) 22;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 207;
    sourceArray2[1] = (byte) 168;
    sourceArray2[41] = (byte) 201;
    sourceArray2[39] = (byte) 7;
    sourceArray2[4] = (byte) 144 /*0x90*/;
    sourceArray2[15] = (byte) 234;
    sourceArray2[20] = (byte) 145;
    sourceArray2[7] = (byte) 12;
    sourceArray2[8] = (byte) 172;
    sourceArray2[26] = (byte) 152;
    sourceArray2[10] = (byte) 141;
    sourceArray2[17] = (byte) 50;
    sourceArray2[40] = (byte) 132;
    sourceArray2[35] = (byte) 90;
    sourceArray2[3] = (byte) 89;
    sourceArray2[29] = (byte) 167;
    sourceArray2[16 /*0x10*/] = (byte) 54;
    sourceArray2[12] = (byte) 238;
    sourceArray2[18] = (byte) 33;
    sourceArray2[21] = (byte) 110;
    sourceArray2[0] = (byte) 182;
    sourceArray2[34] = (byte) 169;
    sourceArray2[6] = (byte) 100;
    sourceArray2[31 /*0x1F*/] = (byte) 71;
    sourceArray2[27] = (byte) 243;
    sourceArray2[25] = (byte) 36;
    sourceArray2[5] = (byte) 193;
    sourceArray2[11] = (byte) 49;
    sourceArray2[28] = (byte) 250;
    sourceArray2[37] = (byte) 221;
    sourceArray2[30] = (byte) 68;
    sourceArray2[38] = (byte) 205;
    sourceArray2[32 /*0x20*/] = (byte) 55;
    sourceArray2[33] = (byte) 41;
    sourceArray2[46] = (byte) 134;
    sourceArray2[22] = (byte) 100;
    sourceArray2[9] = (byte) 216;
    sourceArray2[44] = (byte) 89;
    sourceArray2[24] = (byte) 216;
    sourceArray2[23] = (byte) 149;
    sourceArray2[42] = (byte) 234;
    sourceArray2[36] = (byte) 196;
    sourceArray2[13] = (byte) 198;
    sourceArray2[43] = (byte) 60;
    sourceArray2[2] = (byte) 16 /*0x10*/;
    sourceArray2[45] = (byte) 55;
    sourceArray2[14] = (byte) 130;
    sourceArray2[47] = (byte) 215;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13436(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 157,
      (byte) 211,
      (byte) 159,
      (byte) 158,
      (byte) 156,
      (byte) 51,
      (byte) 195,
      (byte) 197,
      (byte) 80 /*0x50*/,
      (byte) 116,
      (byte) 156,
      (byte) 48 /*0x30*/,
      (byte) 216,
      (byte) 83,
      (byte) 17,
      (byte) 5,
      (byte) 135,
      (byte) 19,
      (byte) 162,
      (byte) 86,
      (byte) 24,
      (byte) 140,
      (byte) 151,
      (byte) 73,
      (byte) 24,
      (byte) 33,
      (byte) 34,
      (byte) 99,
      (byte) 254,
      (byte) 229,
      (byte) 161,
      (byte) 171,
      (byte) 93,
      (byte) 118,
      (byte) 210,
      (byte) 125,
      (byte) 204,
      (byte) 192 /*0xC0*/,
      (byte) 168,
      (byte) 24,
      (byte) 63 /*0x3F*/,
      (byte) 185,
      (byte) 70,
      (byte) 193,
      (byte) 96 /*0x60*/,
      (byte) 3,
      (byte) 214,
      (byte) 132
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 47,
      (byte) 22,
      (byte) 206,
      (byte) 91,
      (byte) 92,
      (byte) 22,
      (byte) 195,
      (byte) 44,
      (byte) 149,
      (byte) 105,
      (byte) 92,
      (byte) 114,
      (byte) 189,
      (byte) 7,
      (byte) 9,
      (byte) 200,
      (byte) 251,
      (byte) 118,
      (byte) 18,
      (byte) 205,
      (byte) 214,
      (byte) 58,
      (byte) 26,
      (byte) 79,
      (byte) 10,
      (byte) 16 /*0x10*/,
      (byte) 90,
      (byte) 168,
      (byte) 199,
      (byte) 237,
      (byte) 209,
      (byte) 98,
      (byte) 60,
      (byte) 70,
      (byte) 253,
      (byte) 202,
      (byte) 108,
      (byte) 15,
      (byte) 236,
      (byte) 79,
      (byte) 222,
      (byte) 231,
      (byte) 79,
      (byte) 116,
      (byte) 57,
      (byte) 149,
      (byte) 245,
      (byte) 162
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13437()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55]
      {
        (byte) 89,
        (byte) 36,
        (byte) 156,
        (byte) 17,
        (byte) 60,
        (byte) 85,
        (byte) 133,
        (byte) 123,
        (byte) 16 /*0x10*/,
        (byte) 95,
        (byte) 198,
        (byte) 107,
        (byte) 210,
        (byte) 241,
        (byte) 211,
        (byte) 97,
        (byte) 129,
        (byte) 147,
        (byte) 57,
        (byte) 199,
        (byte) 107,
        (byte) 137,
        (byte) 23,
        (byte) 94,
        (byte) 70,
        (byte) 213,
        (byte) 233,
        (byte) 21,
        (byte) 75,
        (byte) 158,
        (byte) 18,
        (byte) 156,
        (byte) 15,
        (byte) 46,
        (byte) 82,
        (byte) 57,
        (byte) 218,
        (byte) 193,
        (byte) 65,
        (byte) 118,
        (byte) 58,
        (byte) 44,
        (byte) 7,
        (byte) 223,
        (byte) 242,
        (byte) 218,
        (byte) 238,
        (byte) 111,
        (byte) 48 /*0x30*/,
        (byte) 116,
        (byte) 2,
        (byte) 189,
        (byte) 40,
        (byte) 14,
        (byte) 8
      };
      byte[] numArray3 = new byte[55];
      numArray3[53] = (byte) 147;
      numArray3[8] = (byte) 56;
      numArray3[39] = (byte) 243;
      numArray3[54] = (byte) 145;
      numArray3[4] = (byte) 243;
      numArray3[41] = (byte) 153;
      numArray3[46] = (byte) 107;
      numArray3[7] = (byte) 218;
      numArray3[44] = (byte) 113;
      numArray3[9] = (byte) 210;
      numArray3[31 /*0x1F*/] = (byte) 51;
      numArray3[11] = (byte) 14;
      numArray3[49] = (byte) 2;
      numArray3[52] = (byte) 136;
      numArray3[10] = (byte) 226;
      numArray3[13] = (byte) 146;
      numArray3[30] = (byte) 254;
      numArray3[14] = (byte) 160 /*0xA0*/;
      numArray3[3] = (byte) 125;
      numArray3[35] = (byte) 28;
      numArray3[20] = (byte) 82;
      numArray3[17] = (byte) 198;
      numArray3[18] = (byte) 91;
      numArray3[23] = (byte) 84;
      numArray3[5] = (byte) 98;
      numArray3[1] = (byte) 53;
      numArray3[16 /*0x10*/] = (byte) 146;
      numArray3[27] = (byte) 23;
      numArray3[28] = (byte) 148;
      numArray3[29] = (byte) 186;
      numArray3[21] = (byte) 68;
      numArray3[25] = (byte) 182;
      numArray3[19] = (byte) 184;
      numArray3[33] = (byte) 187;
      numArray3[34] = (byte) 118;
      numArray3[50] = (byte) 131;
      numArray3[36] = (byte) 161;
      numArray3[0] = (byte) 32 /*0x20*/;
      numArray3[37] = (byte) 54;
      numArray3[12] = (byte) 185;
      numArray3[32 /*0x20*/] = (byte) 114;
      numArray3[47] = (byte) 152;
      numArray3[42] = (byte) 121;
      numArray3[43] = (byte) 221;
      numArray3[15] = (byte) 254;
      numArray3[45] = (byte) 217;
      numArray3[2] = (byte) 162;
      numArray3[22] = (byte) 7;
      numArray3[48 /*0x30*/] = (byte) 47;
      numArray3[24] = (byte) 22;
      numArray3[6] = (byte) 24;
      numArray3[51] = (byte) 84;
      numArray3[38] = (byte) 199;
      numArray3[26] = (byte) 8;
      numArray3[40] = (byte) 189;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/]
      {
        (byte) 209,
        (byte) 95,
        (byte) 127 /*0x7F*/,
        (byte) 241,
        (byte) 138,
        (byte) 88,
        (byte) 215,
        (byte) 247,
        (byte) 182,
        (byte) 90,
        (byte) 126,
        (byte) 27,
        (byte) 109,
        (byte) 234,
        (byte) 202,
        (byte) 118
      };
      byte[] numArray5 = new byte[16 /*0x10*/]
      {
        (byte) 98,
        (byte) 11,
        (byte) 177,
        (byte) 76,
        (byte) 166,
        (byte) 131,
        (byte) 197,
        (byte) 170,
        (byte) 97,
        (byte) 204,
        (byte) 193,
        (byte) 150,
        (byte) 45,
        (byte) 244,
        (byte) 41,
        (byte) 164
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[71];
    byte[] numArray7 = new byte[55]
    {
      (byte) 236,
      (byte) 165,
      (byte) 163,
      (byte) 236,
      (byte) 157,
      (byte) 170,
      (byte) 78,
      (byte) 233,
      (byte) 88,
      (byte) 233,
      (byte) 13,
      (byte) 126,
      (byte) 92,
      (byte) 53,
      (byte) 87,
      (byte) 203,
      (byte) 150,
      (byte) 92,
      (byte) 25,
      (byte) 63 /*0x3F*/,
      (byte) 164,
      (byte) 189,
      (byte) 154,
      (byte) 17,
      (byte) 36,
      (byte) 203,
      (byte) 180,
      (byte) 40,
      (byte) 2,
      (byte) 120,
      (byte) 245,
      (byte) 174,
      (byte) 192 /*0xC0*/,
      (byte) 135,
      (byte) 96 /*0x60*/,
      (byte) 47,
      (byte) 46,
      (byte) 115,
      (byte) 225,
      (byte) 71,
      (byte) 59,
      (byte) 110,
      (byte) 126,
      (byte) 14,
      (byte) 168,
      (byte) 219,
      (byte) 33,
      (byte) 230,
      (byte) 22,
      (byte) 31 /*0x1F*/,
      (byte) 51,
      (byte) 26,
      (byte) 157,
      (byte) 129,
      (byte) 157
    };
    byte[] numArray8 = new byte[55];
    numArray8[18] = (byte) 126;
    numArray8[53] = (byte) 1;
    numArray8[2] = (byte) 69;
    numArray8[3] = (byte) 171;
    numArray8[42] = (byte) 146;
    numArray8[50] = (byte) 128 /*0x80*/;
    numArray8[6] = (byte) 6;
    numArray8[11] = (byte) 110;
    numArray8[22] = (byte) 102;
    numArray8[40] = (byte) 167;
    numArray8[15] = (byte) 228;
    numArray8[26] = (byte) 38;
    numArray8[43] = (byte) 217;
    numArray8[12] = (byte) 241;
    numArray8[7] = (byte) 222;
    numArray8[10] = (byte) 220;
    numArray8[14] = (byte) 90;
    numArray8[17] = (byte) 3;
    numArray8[0] = (byte) 119;
    numArray8[19] = (byte) 240 /*0xF0*/;
    numArray8[25] = (byte) 40;
    numArray8[21] = (byte) 49;
    numArray8[1] = (byte) 62;
    numArray8[9] = (byte) 126;
    numArray8[8] = (byte) 97;
    numArray8[24] = (byte) 175;
    numArray8[13] = (byte) 155;
    numArray8[27] = (byte) 68;
    numArray8[28] = (byte) 18;
    numArray8[5] = (byte) 161;
    numArray8[38] = (byte) 200;
    numArray8[31 /*0x1F*/] = (byte) 223;
    numArray8[16 /*0x10*/] = (byte) 212;
    numArray8[33] = (byte) 5;
    numArray8[32 /*0x20*/] = (byte) 32 /*0x20*/;
    numArray8[30] = (byte) 204;
    numArray8[36] = (byte) 161;
    numArray8[51] = (byte) 47;
    numArray8[23] = (byte) 127 /*0x7F*/;
    numArray8[39] = (byte) 28;
    numArray8[20] = (byte) 11;
    numArray8[41] = (byte) 205;
    numArray8[34] = (byte) 175;
    numArray8[37] = (byte) 123;
    numArray8[44] = (byte) 164;
    numArray8[45] = (byte) 206;
    numArray8[46] = (byte) 178;
    numArray8[47] = (byte) 209;
    numArray8[48 /*0x30*/] = (byte) 84;
    numArray8[49] = (byte) 143;
    numArray8[4] = (byte) 101;
    numArray8[35] = (byte) 125;
    numArray8[52] = (byte) 104;
    numArray8[29] = (byte) 84;
    numArray8[54] = (byte) 52;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[16 /*0x10*/]
    {
      (byte) 41,
      (byte) 125,
      (byte) 40,
      (byte) 39,
      (byte) 19,
      (byte) 3,
      (byte) 155,
      (byte) 25,
      (byte) 78,
      (byte) 127 /*0x7F*/,
      (byte) 235,
      (byte) 163,
      (byte) 3,
      (byte) 0,
      (byte) 118,
      (byte) 107
    };
    byte[] numArray10 = new byte[16 /*0x10*/]
    {
      (byte) 14,
      (byte) 180,
      (byte) 29,
      (byte) 217,
      (byte) 116,
      (byte) 74,
      (byte) 150,
      (byte) 7,
      (byte) 220,
      (byte) 197,
      (byte) 159,
      (byte) 170,
      (byte) 4,
      (byte) 56,
      (byte) 76,
      (byte) 23
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13438()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 114,
        (byte) 30,
        (byte) 43,
        (byte) 93,
        (byte) 188,
        (byte) 36,
        (byte) 97,
        (byte) 51,
        (byte) 59,
        (byte) 69
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 142,
        (byte) 2,
        (byte) 143,
        (byte) 102,
        (byte) 211,
        (byte) 86,
        (byte) 159,
        (byte) 74,
        (byte) 179,
        (byte) 90
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
      (byte) 165,
      (byte) 115,
      (byte) 46,
      (byte) 230,
      (byte) 41,
      (byte) 2,
      (byte) 131,
      (byte) 2,
      (byte) 0,
      (byte) 251
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 50,
      (byte) 86,
      (byte) 109,
      (byte) 164,
      (byte) 173,
      (byte) 106,
      (byte) 164,
      (byte) 144 /*0x90*/,
      (byte) 246,
      (byte) 3
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13439(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[41] = (byte) 105;
    sourceArray1[39] = (byte) 239;
    sourceArray1[10] = (byte) 106;
    sourceArray1[1] = (byte) 51;
    sourceArray1[4] = (byte) 186;
    sourceArray1[11] = (byte) 238;
    sourceArray1[33] = (byte) 223;
    sourceArray1[7] = (byte) 10;
    sourceArray1[19] = (byte) 93;
    sourceArray1[47] = (byte) 154;
    sourceArray1[20] = (byte) 140;
    sourceArray1[32 /*0x20*/] = (byte) 58;
    sourceArray1[12] = (byte) 162;
    sourceArray1[30] = (byte) 251;
    sourceArray1[2] = (byte) 73;
    sourceArray1[15] = (byte) 8;
    sourceArray1[13] = (byte) 104;
    sourceArray1[35] = (byte) 8;
    sourceArray1[0] = (byte) 155;
    sourceArray1[34] = (byte) 151;
    sourceArray1[40] = (byte) 153;
    sourceArray1[27] = (byte) 113;
    sourceArray1[22] = (byte) 133;
    sourceArray1[6] = (byte) 12;
    sourceArray1[24] = (byte) 235;
    sourceArray1[25] = (byte) 41;
    sourceArray1[26] = (byte) 157;
    sourceArray1[5] = (byte) 80 /*0x50*/;
    sourceArray1[28] = (byte) 35;
    sourceArray1[29] = (byte) 141;
    sourceArray1[17] = (byte) 105;
    sourceArray1[31 /*0x1F*/] = (byte) 79;
    sourceArray1[3] = (byte) 143;
    sourceArray1[9] = (byte) 203;
    sourceArray1[21] = (byte) 71;
    sourceArray1[18] = (byte) 99;
    sourceArray1[44] = (byte) 191;
    sourceArray1[37] = (byte) 49;
    sourceArray1[38] = (byte) 198;
    sourceArray1[36] = (byte) 251;
    sourceArray1[8] = (byte) 82;
    sourceArray1[23] = (byte) 87;
    sourceArray1[16 /*0x10*/] = (byte) 189;
    sourceArray1[43] = (byte) 26;
    sourceArray1[14] = (byte) 221;
    sourceArray1[45] = (byte) 19;
    sourceArray1[46] = (byte) 225;
    sourceArray1[42] = (byte) 220;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = (byte) 201;
    sourceArray2[36] = (byte) 100;
    sourceArray2[30] = (byte) 71;
    sourceArray2[15] = (byte) 36;
    sourceArray2[0] = (byte) 135;
    sourceArray2[31 /*0x1F*/] = (byte) 64 /*0x40*/;
    sourceArray2[6] = (byte) 226;
    sourceArray2[21] = (byte) 146;
    sourceArray2[45] = (byte) 65;
    sourceArray2[46] = (byte) 198;
    sourceArray2[10] = (byte) 28;
    sourceArray2[34] = (byte) 228;
    sourceArray2[28] = (byte) 180;
    sourceArray2[3] = (byte) 106;
    sourceArray2[13] = (byte) 117;
    sourceArray2[12] = (byte) 181;
    sourceArray2[42] = (byte) 109;
    sourceArray2[41] = (byte) 161;
    sourceArray2[9] = (byte) 80 /*0x50*/;
    sourceArray2[1] = (byte) 241;
    sourceArray2[14] = (byte) 210;
    sourceArray2[27] = (byte) 151;
    sourceArray2[22] = (byte) 109;
    sourceArray2[23] = (byte) 1;
    sourceArray2[24] = (byte) 127 /*0x7F*/;
    sourceArray2[25] = (byte) 19;
    sourceArray2[20] = (byte) 57;
    sourceArray2[19] = (byte) 89;
    sourceArray2[26] = (byte) 32 /*0x20*/;
    sourceArray2[29] = (byte) 83;
    sourceArray2[5] = (byte) 144 /*0x90*/;
    sourceArray2[11] = (byte) 246;
    sourceArray2[2] = (byte) 43;
    sourceArray2[33] = (byte) 4;
    sourceArray2[8] = (byte) 241;
    sourceArray2[32 /*0x20*/] = (byte) 249;
    sourceArray2[7] = (byte) 81;
    sourceArray2[16 /*0x10*/] = (byte) 7;
    sourceArray2[38] = (byte) 237;
    sourceArray2[39] = (byte) 235;
    sourceArray2[40] = (byte) 223;
    sourceArray2[18] = (byte) 8;
    sourceArray2[35] = (byte) 63 /*0x3F*/;
    sourceArray2[43] = (byte) 23;
    sourceArray2[44] = (byte) 67;
    sourceArray2[4] = (byte) 215;
    sourceArray2[17] = (byte) 16 /*0x10*/;
    sourceArray2[37] = (byte) 161;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13440()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 237,
        (byte) 140,
        (byte) 177,
        (byte) 205,
        (byte) 0,
        (byte) 126,
        (byte) 205,
        (byte) 86,
        (byte) 70,
        (byte) 61
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 161,
        (byte) 183,
        (byte) 122,
        (byte) 60,
        (byte) 148,
        (byte) 174,
        (byte) 128 /*0x80*/,
        (byte) 10,
        (byte) 113,
        (byte) 140
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 31 /*0x1F*/;
    numArray5[0] = (byte) 230;
    numArray5[2] = (byte) 4;
    numArray5[3] = (byte) 48 /*0x30*/;
    numArray5[7] = (byte) 80 /*0x50*/;
    numArray5[6] = (byte) 101;
    numArray5[9] = (byte) 118;
    numArray5[1] = (byte) 95;
    numArray5[4] = (byte) 149;
    numArray5[5] = (byte) 31 /*0x1F*/;
    byte[] numArray6 = new byte[10]
    {
      (byte) 195,
      (byte) 147,
      (byte) 149,
      (byte) 25,
      (byte) 110,
      (byte) 60,
      (byte) 180,
      (byte) 189,
      (byte) 40,
      (byte) 40
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13441()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[111];
      byte[] numArray2 = new byte[55]
      {
        (byte) 210,
        (byte) 83,
        (byte) 162,
        (byte) 38,
        (byte) 36,
        (byte) 226,
        (byte) 252,
        (byte) 39,
        (byte) 104,
        (byte) 5,
        (byte) 201,
        (byte) 78,
        (byte) 223,
        (byte) 232,
        (byte) 198,
        (byte) 250,
        (byte) 73,
        (byte) 113,
        (byte) 205,
        (byte) 22,
        (byte) 98,
        (byte) 99,
        (byte) 66,
        (byte) 100,
        (byte) 166,
        (byte) 50,
        (byte) 136,
        (byte) 12,
        (byte) 119,
        (byte) 142,
        (byte) 23,
        (byte) 102,
        (byte) 181,
        (byte) 240 /*0xF0*/,
        (byte) 94,
        (byte) 66,
        (byte) 124,
        (byte) 63 /*0x3F*/,
        (byte) 44,
        (byte) 60,
        (byte) 121,
        (byte) 39,
        (byte) 1,
        (byte) 185,
        (byte) 80 /*0x50*/,
        (byte) 216,
        (byte) 68,
        (byte) 214,
        (byte) 106,
        (byte) 100,
        (byte) 10,
        (byte) 203,
        (byte) 178,
        (byte) 147,
        (byte) 176 /*0xB0*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 29,
        (byte) 31 /*0x1F*/,
        (byte) 10,
        (byte) 79,
        (byte) 132,
        (byte) 59,
        (byte) 49,
        (byte) 219,
        (byte) 24,
        (byte) 254,
        (byte) 80 /*0x50*/,
        (byte) 144 /*0x90*/,
        (byte) 177,
        (byte) 76,
        (byte) 161,
        (byte) 159,
        (byte) 40,
        (byte) 138,
        (byte) 5,
        (byte) 78,
        (byte) 115,
        (byte) 100,
        (byte) 161,
        (byte) 145,
        (byte) 131,
        (byte) 206,
        (byte) 210,
        (byte) 163,
        (byte) 238,
        (byte) 235,
        (byte) 131,
        (byte) 245,
        (byte) 209,
        (byte) 3,
        (byte) 82,
        (byte) 143,
        (byte) 62,
        (byte) 136,
        (byte) 94,
        (byte) 199,
        (byte) 7,
        (byte) 185,
        (byte) 209,
        (byte) 161,
        (byte) 201,
        (byte) 122,
        (byte) 198,
        (byte) 162,
        (byte) 142,
        (byte) 43,
        (byte) 101,
        (byte) 182,
        (byte) 135,
        (byte) 123,
        (byte) 154
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[15] = (byte) 69;
      numArray4[12] = (byte) 132;
      numArray4[46] = (byte) 229;
      numArray4[3] = (byte) 90;
      numArray4[4] = (byte) 240 /*0xF0*/;
      numArray4[43] = (byte) 63 /*0x3F*/;
      numArray4[34] = (byte) 61;
      numArray4[22] = (byte) 205;
      numArray4[14] = (byte) 9;
      numArray4[9] = (byte) 152;
      numArray4[10] = (byte) 239;
      numArray4[11] = (byte) 138;
      numArray4[48 /*0x30*/] = (byte) 0;
      numArray4[13] = (byte) 125;
      numArray4[16 /*0x10*/] = (byte) 145;
      numArray4[29] = (byte) 180;
      numArray4[53] = (byte) 12;
      numArray4[17] = (byte) 157;
      numArray4[24] = (byte) 177;
      numArray4[8] = (byte) 99;
      numArray4[20] = (byte) 136;
      numArray4[21] = (byte) 19;
      numArray4[52] = (byte) 117;
      numArray4[26] = (byte) 18;
      numArray4[54] = (byte) 136;
      numArray4[25] = (byte) 162;
      numArray4[45] = (byte) 76;
      numArray4[2] = (byte) 242;
      numArray4[28] = (byte) 76;
      numArray4[33] = (byte) 49;
      numArray4[27] = (byte) 132;
      numArray4[7] = (byte) 237;
      numArray4[32 /*0x20*/] = (byte) 239;
      numArray4[18] = (byte) 59;
      numArray4[30] = (byte) 34;
      numArray4[35] = (byte) 191;
      numArray4[36] = (byte) 58;
      numArray4[37] = (byte) 225;
      numArray4[1] = (byte) 127 /*0x7F*/;
      numArray4[39] = (byte) 118;
      numArray4[40] = (byte) 39;
      numArray4[41] = (byte) 203;
      numArray4[42] = (byte) 237;
      numArray4[51] = (byte) 245;
      numArray4[44] = (byte) 85;
      numArray4[5] = (byte) 176 /*0xB0*/;
      numArray4[0] = (byte) 62;
      numArray4[23] = (byte) 238;
      numArray4[38] = (byte) 136;
      numArray4[6] = (byte) 218;
      numArray4[50] = (byte) 32 /*0x20*/;
      numArray4[19] = (byte) 98;
      numArray4[47] = (byte) 231;
      numArray4[49] = (byte) 133;
      numArray4[31 /*0x1F*/] = (byte) 64 /*0x40*/;
      byte[] numArray5 = new byte[55]
      {
        (byte) 115,
        (byte) 161,
        (byte) 166,
        (byte) 139,
        (byte) 11,
        (byte) 41,
        (byte) 79,
        (byte) 153,
        (byte) 193,
        (byte) 99,
        (byte) 246,
        (byte) 4,
        (byte) 238,
        (byte) 135,
        (byte) 155,
        (byte) 235,
        (byte) 124,
        (byte) 176 /*0xB0*/,
        (byte) 53,
        (byte) 140,
        (byte) 10,
        (byte) 56,
        (byte) 160 /*0xA0*/,
        (byte) 211,
        (byte) 18,
        (byte) 207,
        (byte) 197,
        (byte) 128 /*0x80*/,
        (byte) 73,
        (byte) 162,
        (byte) 239,
        (byte) 167,
        (byte) 159,
        (byte) 52,
        (byte) 228,
        (byte) 184,
        (byte) 30,
        (byte) 134,
        (byte) 224 /*0xE0*/,
        (byte) 112 /*0x70*/,
        (byte) 94,
        (byte) 16 /*0x10*/,
        (byte) 216,
        (byte) 181,
        (byte) 132,
        (byte) 112 /*0x70*/,
        (byte) 171,
        (byte) 127 /*0x7F*/,
        (byte) 157,
        (byte) 91,
        (byte) 119,
        (byte) 98,
        (byte) 24,
        (byte) 115,
        (byte) 20
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[1]{ (byte) 106 };
      byte[] numArray7 = new byte[1]{ (byte) 10 };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[27];
      byte[] response = new byte[27];
      Array.Copy((Array) sc_13393.sspq, 665, (Array) numArray8, 0, 27);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13393.sspr, 665, (Array) numArray8, 0, 27);
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
    byte[] numArray9 = new byte[111];
    byte[] numArray10 = new byte[55];
    numArray10[30] = (byte) 201;
    numArray10[49] = (byte) 44;
    numArray10[23] = (byte) 143;
    numArray10[3] = (byte) 168;
    numArray10[46] = (byte) 10;
    numArray10[39] = (byte) 106;
    numArray10[6] = (byte) 79;
    numArray10[1] = (byte) 203;
    numArray10[10] = (byte) 248;
    numArray10[9] = (byte) 194;
    numArray10[13] = (byte) 120;
    numArray10[32 /*0x20*/] = (byte) 203;
    numArray10[5] = byte.MaxValue;
    numArray10[29] = (byte) 254;
    numArray10[35] = (byte) 10;
    numArray10[15] = (byte) 247;
    numArray10[16 /*0x10*/] = (byte) 7;
    numArray10[17] = (byte) 195;
    numArray10[18] = (byte) 104;
    numArray10[19] = (byte) 54;
    numArray10[20] = (byte) 30;
    numArray10[21] = (byte) 96 /*0x60*/;
    numArray10[47] = (byte) 85;
    numArray10[7] = (byte) 34;
    numArray10[42] = (byte) 68;
    numArray10[25] = (byte) 237;
    numArray10[40] = (byte) 68;
    numArray10[27] = (byte) 140;
    numArray10[26] = (byte) 3;
    numArray10[2] = (byte) 244;
    numArray10[36] = (byte) 132;
    numArray10[31 /*0x1F*/] = (byte) 168;
    numArray10[8] = (byte) 127 /*0x7F*/;
    numArray10[33] = (byte) 34;
    numArray10[22] = (byte) 120;
    numArray10[52] = (byte) 168;
    numArray10[11] = (byte) 233;
    numArray10[24] = (byte) 104;
    numArray10[38] = (byte) 133;
    numArray10[28] = (byte) 248;
    numArray10[34] = (byte) 84;
    numArray10[41] = (byte) 197;
    numArray10[12] = (byte) 121;
    numArray10[43] = (byte) 48 /*0x30*/;
    numArray10[44] = (byte) 188;
    numArray10[45] = (byte) 64 /*0x40*/;
    numArray10[37] = (byte) 48 /*0x30*/;
    numArray10[53] = (byte) 167;
    numArray10[48 /*0x30*/] = (byte) 170;
    numArray10[14] = (byte) 163;
    numArray10[50] = (byte) 67;
    numArray10[51] = (byte) 87;
    numArray10[0] = (byte) 232;
    numArray10[4] = (byte) 102;
    numArray10[54] = (byte) 236;
    byte[] numArray11 = new byte[55]
    {
      (byte) 165,
      (byte) 126,
      (byte) 135,
      (byte) 110,
      (byte) 60,
      (byte) 11,
      (byte) 8,
      (byte) 37,
      (byte) 150,
      (byte) 245,
      (byte) 53,
      (byte) 139,
      (byte) 250,
      (byte) 57,
      (byte) 84,
      (byte) 61,
      (byte) 181,
      (byte) 19,
      (byte) 14,
      (byte) 19,
      (byte) 245,
      (byte) 36,
      (byte) 173,
      (byte) 40,
      (byte) 174,
      (byte) 119,
      (byte) 124,
      (byte) 18,
      (byte) 64 /*0x40*/,
      (byte) 143,
      (byte) 25,
      (byte) 238,
      (byte) 60,
      (byte) 74,
      (byte) 11,
      (byte) 98,
      (byte) 245,
      (byte) 223,
      (byte) 243,
      (byte) 220,
      (byte) 92,
      (byte) 146,
      (byte) 77,
      (byte) 233,
      (byte) 192 /*0xC0*/,
      (byte) 28,
      (byte) 2,
      (byte) 117,
      (byte) 125,
      (byte) 216,
      (byte) 237,
      (byte) 152,
      (byte) 141,
      (byte) 11,
      (byte) 252
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 145,
      (byte) 24,
      (byte) 149,
      (byte) 77,
      (byte) 224 /*0xE0*/,
      (byte) 93,
      (byte) 198,
      (byte) 134,
      (byte) 78,
      (byte) 250,
      (byte) 33,
      (byte) 88,
      (byte) 225,
      (byte) 83,
      (byte) 229,
      (byte) 50,
      (byte) 142,
      (byte) 113,
      (byte) 189,
      (byte) 87,
      (byte) 15,
      (byte) 102,
      (byte) 6,
      (byte) 47,
      (byte) 125,
      (byte) 209,
      (byte) 19,
      (byte) 77,
      (byte) 89,
      (byte) 86,
      (byte) 110,
      (byte) 161,
      (byte) 94,
      (byte) 171,
      (byte) 141,
      (byte) 216,
      (byte) 118,
      (byte) 33,
      (byte) 160 /*0xA0*/,
      (byte) 62,
      (byte) 253,
      (byte) 177,
      (byte) 30,
      (byte) 39,
      (byte) 145,
      (byte) 216,
      (byte) 47,
      (byte) 101,
      (byte) 135,
      (byte) 230,
      (byte) 27,
      (byte) 68,
      (byte) 214,
      (byte) 22,
      (byte) 252
    };
    byte[] numArray13 = new byte[55];
    numArray13[50] = (byte) 78;
    numArray13[11] = (byte) 171;
    numArray13[30] = (byte) 199;
    numArray13[3] = (byte) 172;
    numArray13[49] = (byte) 176 /*0xB0*/;
    numArray13[5] = (byte) 240 /*0xF0*/;
    numArray13[6] = (byte) 93;
    numArray13[15] = (byte) 154;
    numArray13[22] = (byte) 242;
    numArray13[8] = (byte) 38;
    numArray13[9] = (byte) 185;
    numArray13[45] = (byte) 177;
    numArray13[24] = (byte) 84;
    numArray13[18] = (byte) 135;
    numArray13[37] = (byte) 64 /*0x40*/;
    numArray13[12] = (byte) 130;
    numArray13[16 /*0x10*/] = (byte) 130;
    numArray13[34] = (byte) 140;
    numArray13[13] = (byte) 126;
    numArray13[19] = (byte) 215;
    numArray13[20] = (byte) 190;
    numArray13[21] = (byte) 32 /*0x20*/;
    numArray13[10] = (byte) 158;
    numArray13[14] = (byte) 208 /*0xD0*/;
    numArray13[36] = (byte) 100;
    numArray13[25] = (byte) 106;
    numArray13[26] = (byte) 27;
    numArray13[27] = (byte) 74;
    numArray13[28] = (byte) 161;
    numArray13[29] = (byte) 65;
    numArray13[35] = byte.MaxValue;
    numArray13[31 /*0x1F*/] = (byte) 12;
    numArray13[51] = (byte) 50;
    numArray13[1] = (byte) 198;
    numArray13[40] = (byte) 187;
    numArray13[4] = (byte) 216;
    numArray13[52] = (byte) 247;
    numArray13[17] = (byte) 134;
    numArray13[38] = (byte) 219;
    numArray13[54] = (byte) 89;
    numArray13[23] = (byte) 19;
    numArray13[32 /*0x20*/] = (byte) 247;
    numArray13[42] = (byte) 87;
    numArray13[43] = (byte) 212;
    numArray13[44] = (byte) 253;
    numArray13[33] = (byte) 163;
    numArray13[46] = (byte) 252;
    numArray13[47] = (byte) 120;
    numArray13[48 /*0x30*/] = (byte) 78;
    numArray13[2] = (byte) 24;
    numArray13[39] = (byte) 49;
    numArray13[0] = (byte) 189;
    numArray13[41] = (byte) 135;
    numArray13[53] = (byte) 51;
    numArray13[7] = (byte) 25;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[1]{ (byte) 201 };
    byte[] numArray15 = new byte[1]{ (byte) 65 };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 1);
    for (int index = 0; index < 1; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_13442()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 190,
        (byte) 97,
        (byte) 180,
        (byte) 178,
        (byte) 136,
        (byte) 99,
        (byte) 70,
        (byte) 171,
        (byte) 27,
        (byte) 62
      };
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 114;
      numArray3[1] = (byte) 145;
      numArray3[2] = (byte) 156;
      numArray3[4] = (byte) 107;
      numArray3[9] = (byte) 166;
      numArray3[7] = (byte) 192 /*0xC0*/;
      numArray3[6] = (byte) 20;
      numArray3[3] = (byte) 135;
      numArray3[5] = (byte) 30;
      numArray3[0] = (byte) 26;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 116,
      (byte) 139,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 147,
      (byte) 70,
      (byte) 40,
      (byte) 36,
      (byte) 87,
      (byte) 209
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 171,
      (byte) 153,
      (byte) 120,
      (byte) 0,
      (byte) 0,
      (byte) 200,
      (byte) 0,
      (byte) 0,
      (byte) 159,
      (byte) 0
    };
    numArray6[6] = (byte) 92;
    numArray6[3] = (byte) 217;
    numArray6[7] = (byte) 128 /*0x80*/;
    numArray6[9] = (byte) 23;
    numArray6[4] = (byte) 151;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13443()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 104;
      numArray2[1] = (byte) 240 /*0xF0*/;
      numArray2[2] = (byte) 61;
      numArray2[7] = (byte) 113;
      numArray2[4] = (byte) 195;
      numArray2[9] = (byte) 171;
      numArray2[0] = (byte) 193;
      numArray2[5] = (byte) 142;
      numArray2[8] = (byte) 50;
      numArray2[3] = (byte) 97;
      byte[] numArray3 = new byte[10]
      {
        (byte) 5,
        (byte) 59,
        (byte) 12,
        (byte) 200,
        (byte) 250,
        (byte) 243,
        (byte) 172,
        (byte) 190,
        (byte) 82,
        (byte) 122
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
      (byte) 45,
      (byte) 157,
      (byte) 57,
      (byte) 209,
      (byte) 23,
      (byte) 201,
      (byte) 248,
      (byte) 129,
      (byte) 169,
      (byte) 163
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 1,
      (byte) 211,
      (byte) 28,
      (byte) 55,
      (byte) 219,
      (byte) 66,
      (byte) 22,
      (byte) 56,
      (byte) 57,
      (byte) 58
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13444(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 227;
    sourceArray1[1] = (byte) 201;
    sourceArray1[19] = (byte) 15;
    sourceArray1[3] = (byte) 88;
    sourceArray1[4] = (byte) 53;
    sourceArray1[23] = (byte) 80 /*0x50*/;
    sourceArray1[34] = (byte) 184;
    sourceArray1[7] = (byte) 2;
    sourceArray1[8] = (byte) 246;
    sourceArray1[9] = (byte) 88;
    sourceArray1[10] = (byte) 128 /*0x80*/;
    sourceArray1[43] = (byte) 120;
    sourceArray1[0] = (byte) 97;
    sourceArray1[12] = (byte) 160 /*0xA0*/;
    sourceArray1[14] = (byte) 152;
    sourceArray1[30] = (byte) 38;
    sourceArray1[33] = (byte) 43;
    sourceArray1[2] = (byte) 105;
    sourceArray1[11] = (byte) 25;
    sourceArray1[32 /*0x20*/] = (byte) 91;
    sourceArray1[5] = (byte) 142;
    sourceArray1[21] = (byte) 137;
    sourceArray1[18] = (byte) 230;
    sourceArray1[6] = (byte) 29;
    sourceArray1[24] = (byte) 167;
    sourceArray1[25] = (byte) 57;
    sourceArray1[26] = (byte) 42;
    sourceArray1[27] = (byte) 150;
    sourceArray1[28] = (byte) 132;
    sourceArray1[29] = (byte) 47;
    sourceArray1[35] = (byte) 105;
    sourceArray1[20] = (byte) 195;
    sourceArray1[47] = (byte) 101;
    sourceArray1[17] = (byte) 7;
    sourceArray1[31 /*0x1F*/] = (byte) 15;
    sourceArray1[13] = (byte) 27;
    sourceArray1[16 /*0x10*/] = (byte) 181;
    sourceArray1[37] = (byte) 183;
    sourceArray1[38] = (byte) 132;
    sourceArray1[39] = (byte) 123;
    sourceArray1[36] = (byte) 154;
    sourceArray1[44] = (byte) 150;
    sourceArray1[42] = (byte) 67;
    sourceArray1[22] = (byte) 188;
    sourceArray1[40] = (byte) 15;
    sourceArray1[45] = (byte) 175;
    sourceArray1[46] = (byte) 87;
    sourceArray1[41] = (byte) 155;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 91;
    sourceArray2[1] = (byte) 128 /*0x80*/;
    sourceArray2[30] = (byte) 32 /*0x20*/;
    sourceArray2[25] = (byte) 88;
    sourceArray2[23] = (byte) 7;
    sourceArray2[5] = (byte) 220;
    sourceArray2[41] = (byte) 43;
    sourceArray2[47] = (byte) 33;
    sourceArray2[8] = (byte) 35;
    sourceArray2[9] = (byte) 86;
    sourceArray2[40] = (byte) 220;
    sourceArray2[11] = (byte) 162;
    sourceArray2[39] = (byte) 237;
    sourceArray2[46] = (byte) 7;
    sourceArray2[4] = (byte) 183;
    sourceArray2[37] = (byte) 197;
    sourceArray2[45] = (byte) 202;
    sourceArray2[17] = (byte) 19;
    sourceArray2[18] = (byte) 68;
    sourceArray2[34] = (byte) 43;
    sourceArray2[29] = (byte) 140;
    sourceArray2[21] = (byte) 185;
    sourceArray2[22] = (byte) 41;
    sourceArray2[0] = (byte) 133;
    sourceArray2[24] = (byte) 240 /*0xF0*/;
    sourceArray2[16 /*0x10*/] = (byte) 141;
    sourceArray2[10] = (byte) 97;
    sourceArray2[19] = (byte) 151;
    sourceArray2[28] = (byte) 82;
    sourceArray2[7] = (byte) 134;
    sourceArray2[15] = (byte) 146;
    sourceArray2[31 /*0x1F*/] = (byte) 49;
    sourceArray2[32 /*0x20*/] = (byte) 107;
    sourceArray2[33] = (byte) 71;
    sourceArray2[43] = (byte) 194;
    sourceArray2[35] = (byte) 139;
    sourceArray2[2] = (byte) 75;
    sourceArray2[3] = (byte) 217;
    sourceArray2[20] = (byte) 32 /*0x20*/;
    sourceArray2[27] = (byte) 61;
    sourceArray2[36] = (byte) 148;
    sourceArray2[42] = (byte) 94;
    sourceArray2[14] = (byte) 13;
    sourceArray2[38] = (byte) 185;
    sourceArray2[44] = (byte) 134;
    sourceArray2[12] = (byte) 18;
    sourceArray2[6] = (byte) 83;
    sourceArray2[13] = (byte) 164;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13445(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 134,
      (byte) 97,
      (byte) 79,
      (byte) 134,
      (byte) 8,
      (byte) 150,
      (byte) 149,
      (byte) 127 /*0x7F*/,
      (byte) 15,
      (byte) 175,
      (byte) 158,
      (byte) 57,
      (byte) 252,
      (byte) 129,
      (byte) 206,
      (byte) 199,
      (byte) 204,
      (byte) 202,
      (byte) 178,
      (byte) 25,
      (byte) 219,
      (byte) 94,
      (byte) 108,
      (byte) 40,
      (byte) 130,
      (byte) 26,
      (byte) 242,
      (byte) 0,
      (byte) 52,
      (byte) 63 /*0x3F*/,
      (byte) 163,
      (byte) 171,
      (byte) 137,
      (byte) 219,
      (byte) 240 /*0xF0*/,
      (byte) 170,
      (byte) 122,
      (byte) 118,
      (byte) 139,
      (byte) 111,
      (byte) 106,
      (byte) 68,
      (byte) 147,
      (byte) 157,
      (byte) 102,
      (byte) 4,
      (byte) 6
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 46,
      (byte) 223,
      (byte) 200,
      (byte) 244,
      (byte) 24,
      (byte) 43,
      (byte) 123,
      (byte) 132,
      (byte) 123,
      (byte) 205,
      (byte) 243,
      (byte) 161,
      (byte) 158,
      (byte) 32 /*0x20*/,
      (byte) 96 /*0x60*/,
      (byte) 231,
      (byte) 83,
      (byte) 195,
      (byte) 122,
      (byte) 57,
      (byte) 57,
      (byte) 224 /*0xE0*/,
      (byte) 76,
      (byte) 97,
      (byte) 116,
      (byte) 84,
      (byte) 41,
      (byte) 141,
      (byte) 242,
      (byte) 37,
      (byte) 3,
      (byte) 133,
      (byte) 253,
      (byte) 182,
      (byte) 237,
      (byte) 94,
      (byte) 119,
      (byte) 157,
      (byte) 133,
      (byte) 8,
      (byte) 243,
      (byte) 31 /*0x1F*/,
      (byte) 133,
      byte.MaxValue,
      (byte) 70,
      (byte) 31 /*0x1F*/,
      (byte) 134
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13446(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 154,
      (byte) 86,
      (byte) 157,
      (byte) 40,
      (byte) 245,
      (byte) 39,
      (byte) 3,
      (byte) 237,
      (byte) 164,
      (byte) 148,
      (byte) 74,
      (byte) 208 /*0xD0*/,
      (byte) 12,
      (byte) 242,
      (byte) 176 /*0xB0*/,
      (byte) 33,
      (byte) 144 /*0x90*/,
      (byte) 36,
      (byte) 76,
      (byte) 85,
      (byte) 63 /*0x3F*/,
      (byte) 67,
      (byte) 174,
      (byte) 151,
      (byte) 202,
      (byte) 206,
      (byte) 193,
      (byte) 125,
      (byte) 20,
      (byte) 10,
      (byte) 130,
      (byte) 172,
      (byte) 121,
      (byte) 73,
      (byte) 17,
      (byte) 50,
      (byte) 61,
      (byte) 200,
      (byte) 180,
      (byte) 83,
      (byte) 115,
      (byte) 157,
      (byte) 203,
      (byte) 214,
      (byte) 249,
      (byte) 195,
      (byte) 145,
      (byte) 115
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 139,
      (byte) 7,
      (byte) 64 /*0x40*/,
      (byte) 92,
      (byte) 187,
      (byte) 172,
      (byte) 138,
      (byte) 61,
      (byte) 249,
      (byte) 79,
      (byte) 74,
      (byte) 214,
      (byte) 27,
      (byte) 32 /*0x20*/,
      (byte) 121,
      (byte) 130,
      (byte) 81,
      (byte) 193,
      (byte) 16 /*0x10*/,
      (byte) 245,
      (byte) 86,
      (byte) 236,
      (byte) 145,
      (byte) 96 /*0x60*/,
      (byte) 226,
      (byte) 125,
      (byte) 94,
      (byte) 223,
      (byte) 156,
      (byte) 165,
      (byte) 165,
      (byte) 93,
      (byte) 153,
      (byte) 160 /*0xA0*/,
      (byte) 44,
      (byte) 45,
      (byte) 68,
      (byte) 99,
      (byte) 139,
      (byte) 167,
      (byte) 23,
      (byte) 27,
      (byte) 194,
      (byte) 228,
      (byte) 99,
      (byte) 69,
      (byte) 59,
      (byte) 152
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13447(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 112 /*0x70*/,
      (byte) 25,
      (byte) 86,
      (byte) 168,
      (byte) 6,
      (byte) 93,
      (byte) 38,
      (byte) 101,
      (byte) 133,
      (byte) 217,
      (byte) 153,
      (byte) 18,
      (byte) 158,
      (byte) 87,
      (byte) 194,
      (byte) 225,
      (byte) 125,
      (byte) 227,
      (byte) 200,
      (byte) 111,
      (byte) 248,
      (byte) 226,
      (byte) 161,
      (byte) 120,
      (byte) 216,
      (byte) 158,
      (byte) 7,
      (byte) 61,
      (byte) 78,
      (byte) 41,
      (byte) 46,
      (byte) 179,
      (byte) 40,
      (byte) 113,
      (byte) 8,
      (byte) 48 /*0x30*/,
      (byte) 186,
      (byte) 137,
      (byte) 55,
      (byte) 107,
      (byte) 142,
      (byte) 238,
      (byte) 237,
      (byte) 195,
      (byte) 49,
      (byte) 195,
      (byte) 91,
      (byte) 108
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 216,
      (byte) 226,
      (byte) 122,
      (byte) 96 /*0x60*/,
      (byte) 21,
      (byte) 213,
      (byte) 94,
      (byte) 66,
      (byte) 9,
      (byte) 78,
      (byte) 2,
      (byte) 215,
      (byte) 254,
      (byte) 129,
      (byte) 26,
      (byte) 16 /*0x10*/,
      (byte) 220,
      (byte) 123,
      (byte) 212,
      (byte) 206,
      (byte) 195,
      (byte) 46,
      (byte) 246,
      (byte) 40,
      (byte) 166,
      (byte) 179,
      (byte) 252,
      (byte) 181,
      (byte) 240 /*0xF0*/,
      (byte) 204,
      (byte) 165,
      (byte) 6,
      (byte) 172,
      (byte) 31 /*0x1F*/,
      (byte) 60,
      (byte) 216,
      (byte) 204,
      (byte) 150,
      (byte) 15,
      (byte) 40,
      (byte) 42,
      (byte) 196,
      (byte) 185,
      (byte) 186,
      (byte) 18,
      (byte) 126,
      (byte) 162,
      (byte) 128 /*0x80*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[11];
    byte[] response2 = new byte[11];
    Array.Copy((Array) sc_13393.sspq, 692, (Array) numArray2, 0, 11);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13393.sspr, 692, (Array) numArray2, 0, 11);
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

  internal static int ssp_appserver_13448(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 230,
      (byte) 234,
      (byte) 44,
      (byte) 78,
      (byte) 146,
      (byte) 38,
      (byte) 243,
      (byte) 171,
      (byte) 92,
      (byte) 225,
      (byte) 10,
      (byte) 244,
      (byte) 222,
      (byte) 70,
      (byte) 20,
      (byte) 66,
      (byte) 154,
      (byte) 111,
      (byte) 187,
      (byte) 82,
      (byte) 44,
      (byte) 37,
      (byte) 63 /*0x3F*/,
      (byte) 47,
      (byte) 122,
      (byte) 184,
      (byte) 219,
      (byte) 185,
      (byte) 207,
      (byte) 20,
      (byte) 165,
      (byte) 214,
      (byte) 145,
      (byte) 25,
      (byte) 75,
      (byte) 217,
      (byte) 46,
      (byte) 30,
      (byte) 59,
      (byte) 107,
      (byte) 226,
      (byte) 86,
      (byte) 49,
      (byte) 7,
      (byte) 55,
      (byte) 153,
      (byte) 153,
      (byte) 151
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 20,
      (byte) 62,
      (byte) 245,
      (byte) 14,
      (byte) 211,
      (byte) 141,
      (byte) 169,
      (byte) 245,
      (byte) 107,
      (byte) 133,
      (byte) 49,
      (byte) 148,
      (byte) 157,
      (byte) 248,
      (byte) 90,
      (byte) 199,
      (byte) 43,
      (byte) 145,
      (byte) 123,
      (byte) 182,
      (byte) 88,
      (byte) 82,
      (byte) 149,
      (byte) 136,
      (byte) 201,
      (byte) 190,
      (byte) 90,
      (byte) 235,
      (byte) 148,
      (byte) 219,
      (byte) 229,
      (byte) 227,
      (byte) 209,
      (byte) 110,
      (byte) 125,
      (byte) 193,
      (byte) 124,
      (byte) 161,
      (byte) 82,
      (byte) 72,
      (byte) 48 /*0x30*/,
      (byte) 145,
      (byte) 123,
      (byte) 209,
      (byte) 54,
      (byte) 0,
      (byte) 140,
      (byte) 53
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13449()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50];
      numArray2[30] = (byte) 253;
      numArray2[1] = (byte) 9;
      numArray2[2] = (byte) 125;
      numArray2[48 /*0x30*/] = (byte) 54;
      numArray2[31 /*0x1F*/] = (byte) 170;
      numArray2[8] = (byte) 74;
      numArray2[42] = (byte) 32 /*0x20*/;
      numArray2[26] = (byte) 247;
      numArray2[47] = (byte) 3;
      numArray2[19] = (byte) 124;
      numArray2[34] = (byte) 123;
      numArray2[11] = (byte) 71;
      numArray2[7] = (byte) 218;
      numArray2[45] = (byte) 233;
      numArray2[14] = (byte) 98;
      numArray2[15] = (byte) 196;
      numArray2[49] = (byte) 213;
      numArray2[32 /*0x20*/] = (byte) 89;
      numArray2[21] = (byte) 80 /*0x50*/;
      numArray2[16 /*0x10*/] = (byte) 251;
      numArray2[43] = (byte) 168;
      numArray2[10] = (byte) 220;
      numArray2[22] = (byte) 111;
      numArray2[23] = (byte) 214;
      numArray2[13] = (byte) 51;
      numArray2[36] = (byte) 155;
      numArray2[46] = (byte) 203;
      numArray2[27] = (byte) 225;
      numArray2[3] = (byte) 193;
      numArray2[29] = (byte) 68;
      numArray2[28] = (byte) 30;
      numArray2[12] = (byte) 60;
      numArray2[4] = (byte) 170;
      numArray2[33] = (byte) 44;
      numArray2[20] = (byte) 52;
      numArray2[25] = (byte) 55;
      numArray2[6] = (byte) 211;
      numArray2[37] = (byte) 137;
      numArray2[38] = (byte) 46;
      numArray2[39] = (byte) 50;
      numArray2[40] = (byte) 74;
      numArray2[41] = (byte) 96 /*0x60*/;
      numArray2[0] = (byte) 91;
      numArray2[18] = (byte) 20;
      numArray2[44] = (byte) 122;
      numArray2[35] = (byte) 135;
      numArray2[9] = (byte) 58;
      numArray2[24] = (byte) 243;
      numArray2[17] = (byte) 59;
      numArray2[5] = (byte) 252;
      byte[] numArray3 = new byte[50]
      {
        (byte) 41,
        (byte) 204,
        (byte) 50,
        (byte) 43,
        (byte) 241,
        (byte) 213,
        (byte) 57,
        (byte) 154,
        (byte) 15,
        (byte) 56,
        (byte) 176 /*0xB0*/,
        (byte) 13,
        (byte) 100,
        (byte) 243,
        (byte) 179,
        (byte) 80 /*0x50*/,
        (byte) 77,
        (byte) 25,
        (byte) 70,
        (byte) 188,
        (byte) 190,
        (byte) 63 /*0x3F*/,
        (byte) 27,
        (byte) 17,
        (byte) 38,
        (byte) 113,
        (byte) 191,
        (byte) 247,
        byte.MaxValue,
        (byte) 98,
        (byte) 142,
        (byte) 153,
        (byte) 149,
        (byte) 42,
        (byte) 67,
        (byte) 227,
        (byte) 206,
        (byte) 254,
        (byte) 185,
        (byte) 201,
        (byte) 249,
        (byte) 86,
        (byte) 78,
        (byte) 231,
        (byte) 64 /*0x40*/,
        byte.MaxValue,
        (byte) 244,
        (byte) 10,
        (byte) 175,
        (byte) 158
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50]
    {
      (byte) 91,
      (byte) 171,
      (byte) 42,
      (byte) 114,
      (byte) 47,
      (byte) 11,
      (byte) 114,
      (byte) 219,
      (byte) 211,
      (byte) 74,
      (byte) 196,
      (byte) 7,
      (byte) 16 /*0x10*/,
      (byte) 71,
      (byte) 115,
      (byte) 94,
      (byte) 190,
      (byte) 153,
      (byte) 210,
      (byte) 41,
      (byte) 72,
      (byte) 237,
      (byte) 52,
      (byte) 181,
      (byte) 82,
      (byte) 180,
      (byte) 217,
      (byte) 47,
      (byte) 22,
      (byte) 91,
      (byte) 20,
      (byte) 13,
      (byte) 176 /*0xB0*/,
      (byte) 168,
      (byte) 79,
      (byte) 157,
      (byte) 126,
      (byte) 11,
      (byte) 105,
      (byte) 15,
      (byte) 116,
      (byte) 148,
      (byte) 26,
      (byte) 169,
      (byte) 247,
      (byte) 178,
      (byte) 28,
      (byte) 17,
      (byte) 48 /*0x30*/,
      (byte) 186
    };
    byte[] numArray6 = new byte[50];
    numArray6[6] = (byte) 159;
    numArray6[1] = (byte) 16 /*0x10*/;
    numArray6[47] = (byte) 192 /*0xC0*/;
    numArray6[3] = (byte) 250;
    numArray6[21] = (byte) 81;
    numArray6[38] = (byte) 233;
    numArray6[30] = (byte) 218;
    numArray6[7] = (byte) 81;
    numArray6[36] = (byte) 197;
    numArray6[31 /*0x1F*/] = (byte) 241;
    numArray6[10] = (byte) 1;
    numArray6[11] = (byte) 111;
    numArray6[12] = (byte) 89;
    numArray6[40] = (byte) 192 /*0xC0*/;
    numArray6[28] = (byte) 95;
    numArray6[15] = (byte) 221;
    numArray6[41] = (byte) 242;
    numArray6[17] = (byte) 83;
    numArray6[18] = (byte) 222;
    numArray6[19] = (byte) 53;
    numArray6[20] = (byte) 7;
    numArray6[13] = (byte) 49;
    numArray6[22] = (byte) 237;
    numArray6[5] = (byte) 127 /*0x7F*/;
    numArray6[24] = (byte) 65;
    numArray6[25] = (byte) 41;
    numArray6[23] = (byte) 234;
    numArray6[16 /*0x10*/] = (byte) 254;
    numArray6[2] = (byte) 248;
    numArray6[29] = (byte) 4;
    numArray6[9] = (byte) 194;
    numArray6[26] = (byte) 91;
    numArray6[32 /*0x20*/] = (byte) 170;
    numArray6[33] = (byte) 48 /*0x30*/;
    numArray6[34] = (byte) 39;
    numArray6[0] = (byte) 140;
    numArray6[27] = (byte) 110;
    numArray6[37] = (byte) 40;
    numArray6[35] = (byte) 69;
    numArray6[4] = (byte) 72;
    numArray6[48 /*0x30*/] = (byte) 71;
    numArray6[39] = (byte) 93;
    numArray6[42] = (byte) 140;
    numArray6[14] = (byte) 146;
    numArray6[44] = (byte) 145;
    numArray6[45] = (byte) 67;
    numArray6[46] = (byte) 185;
    numArray6[8] = (byte) 161;
    numArray6[43] = (byte) 71;
    numArray6[49] = (byte) 203;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13450()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 220;
      numArray2[7] = (byte) 235;
      numArray2[9] = (byte) 128 /*0x80*/;
      numArray2[1] = (byte) 250;
      numArray2[6] = (byte) 154;
      numArray2[5] = (byte) 79;
      numArray2[3] = (byte) 101;
      numArray2[0] = (byte) 217;
      numArray2[2] = (byte) 127 /*0x7F*/;
      numArray2[8] = (byte) 182;
      byte[] numArray3 = new byte[10];
      numArray3[9] = (byte) 111;
      numArray3[1] = (byte) 11;
      numArray3[4] = (byte) 67;
      numArray3[3] = (byte) 95;
      numArray3[2] = (byte) 159;
      numArray3[0] = (byte) 81;
      numArray3[5] = (byte) 202;
      numArray3[7] = (byte) 52;
      numArray3[8] = (byte) 29;
      numArray3[6] = (byte) 71;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 163,
      (byte) 129,
      (byte) 179,
      (byte) 230,
      (byte) 128 /*0x80*/,
      (byte) 65,
      (byte) 85,
      (byte) 70,
      (byte) 58,
      (byte) 83
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 1,
      (byte) 96 /*0x60*/,
      (byte) 228,
      (byte) 225,
      (byte) 201,
      (byte) 198,
      (byte) 214,
      (byte) 196,
      (byte) 164,
      (byte) 226
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[44];
    byte[] response = new byte[44];
    Array.Copy((Array) sc_13393.sspq, 703, (Array) numArray7, 0, 44);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13393.sspr, 703, (Array) numArray7, 0, 44);
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

  internal static string ssp_appserver_13451()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[305];
      byte[] numArray2 = new byte[55]
      {
        (byte) 1,
        (byte) 231,
        (byte) 232,
        (byte) 220,
        (byte) 102,
        (byte) 24,
        (byte) 48 /*0x30*/,
        (byte) 16 /*0x10*/,
        (byte) 186,
        (byte) 74,
        (byte) 128 /*0x80*/,
        (byte) 193,
        (byte) 187,
        (byte) 31 /*0x1F*/,
        (byte) 41,
        (byte) 129,
        (byte) 52,
        (byte) 55,
        (byte) 127 /*0x7F*/,
        (byte) 214,
        (byte) 12,
        (byte) 12,
        (byte) 195,
        (byte) 240 /*0xF0*/,
        (byte) 18,
        (byte) 97,
        (byte) 46,
        (byte) 24,
        (byte) 7,
        (byte) 13,
        (byte) 170,
        (byte) 104,
        (byte) 150,
        (byte) 65,
        (byte) 79,
        (byte) 16 /*0x10*/,
        (byte) 133,
        (byte) 242,
        (byte) 13,
        (byte) 106,
        (byte) 43,
        (byte) 34,
        (byte) 184,
        (byte) 75,
        (byte) 252,
        (byte) 150,
        (byte) 183,
        (byte) 236,
        (byte) 249,
        (byte) 115,
        (byte) 119,
        (byte) 192 /*0xC0*/,
        byte.MaxValue,
        (byte) 170,
        (byte) 155
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 132,
        (byte) 198,
        (byte) 13,
        (byte) 158,
        (byte) 248,
        (byte) 160 /*0xA0*/,
        (byte) 236,
        (byte) 160 /*0xA0*/,
        (byte) 181,
        (byte) 200,
        (byte) 87,
        (byte) 58,
        (byte) 143,
        (byte) 180,
        (byte) 72,
        (byte) 250,
        (byte) 63 /*0x3F*/,
        (byte) 138,
        (byte) 174,
        (byte) 160 /*0xA0*/,
        (byte) 118,
        (byte) 213,
        (byte) 122,
        (byte) 32 /*0x20*/,
        (byte) 75,
        (byte) 56,
        (byte) 94,
        (byte) 78,
        (byte) 41,
        (byte) 59,
        (byte) 54,
        (byte) 179,
        (byte) 225,
        (byte) 18,
        (byte) 236,
        (byte) 118,
        (byte) 250,
        (byte) 242,
        (byte) 106,
        (byte) 87,
        (byte) 102,
        (byte) 127 /*0x7F*/,
        (byte) 54,
        (byte) 250,
        (byte) 109,
        (byte) 45,
        (byte) 173,
        (byte) 191,
        (byte) 111,
        (byte) 190,
        (byte) 116,
        (byte) 19,
        (byte) 33,
        (byte) 217,
        (byte) 66
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[17] = (byte) 170;
      numArray4[1] = (byte) 202;
      numArray4[2] = (byte) 5;
      numArray4[3] = (byte) 234;
      numArray4[19] = (byte) 84;
      numArray4[10] = (byte) 122;
      numArray4[39] = (byte) 25;
      numArray4[7] = (byte) 160 /*0xA0*/;
      numArray4[47] = (byte) 119;
      numArray4[9] = (byte) 43;
      numArray4[52] = (byte) 177;
      numArray4[11] = (byte) 207;
      numArray4[37] = (byte) 128 /*0x80*/;
      numArray4[32 /*0x20*/] = (byte) 91;
      numArray4[49] = (byte) 151;
      numArray4[35] = (byte) 40;
      numArray4[16 /*0x10*/] = (byte) 20;
      numArray4[48 /*0x30*/] = (byte) 54;
      numArray4[18] = (byte) 234;
      numArray4[26] = (byte) 148;
      numArray4[20] = (byte) 119;
      numArray4[12] = (byte) 25;
      numArray4[22] = (byte) 123;
      numArray4[23] = (byte) 169;
      numArray4[54] = (byte) 173;
      numArray4[25] = (byte) 31 /*0x1F*/;
      numArray4[21] = (byte) 244;
      numArray4[27] = (byte) 149;
      numArray4[38] = (byte) 20;
      numArray4[50] = (byte) 234;
      numArray4[46] = (byte) 223;
      numArray4[6] = (byte) 172;
      numArray4[4] = (byte) 23;
      numArray4[33] = (byte) 235;
      numArray4[29] = (byte) 104;
      numArray4[53] = (byte) 159;
      numArray4[36] = (byte) 8;
      numArray4[34] = (byte) 141;
      numArray4[42] = (byte) 228;
      numArray4[15] = (byte) 41;
      numArray4[40] = (byte) 11;
      numArray4[41] = (byte) 170;
      numArray4[28] = (byte) 233;
      numArray4[31 /*0x1F*/] = (byte) 165;
      numArray4[44] = (byte) 61;
      numArray4[45] = (byte) 28;
      numArray4[0] = (byte) 248;
      numArray4[51] = byte.MaxValue;
      numArray4[13] = (byte) 251;
      numArray4[24] = (byte) 66;
      numArray4[14] = (byte) 65;
      numArray4[43] = (byte) 107;
      numArray4[8] = (byte) 23;
      numArray4[5] = (byte) 8;
      numArray4[30] = (byte) 236;
      byte[] numArray5 = new byte[55]
      {
        (byte) 55,
        (byte) 182,
        (byte) 119,
        (byte) 51,
        (byte) 78,
        (byte) 242,
        (byte) 126,
        (byte) 144 /*0x90*/,
        (byte) 232,
        (byte) 5,
        (byte) 72,
        (byte) 195,
        (byte) 124,
        (byte) 159,
        (byte) 22,
        (byte) 212,
        (byte) 211,
        (byte) 131,
        (byte) 33,
        (byte) 216,
        (byte) 51,
        (byte) 36,
        (byte) 132,
        (byte) 51,
        (byte) 208 /*0xD0*/,
        (byte) 227,
        (byte) 104,
        (byte) 93,
        (byte) 217,
        (byte) 93,
        (byte) 52,
        (byte) 138,
        (byte) 177,
        (byte) 160 /*0xA0*/,
        (byte) 224 /*0xE0*/,
        (byte) 131,
        (byte) 192 /*0xC0*/,
        (byte) 242,
        (byte) 195,
        (byte) 159,
        (byte) 67,
        (byte) 181,
        (byte) 198,
        (byte) 83,
        (byte) 181,
        (byte) 106,
        (byte) 7,
        (byte) 61,
        (byte) 120,
        (byte) 171,
        (byte) 47,
        (byte) 249,
        (byte) 11,
        (byte) 114,
        (byte) 4
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 239,
        (byte) 130,
        (byte) 184,
        (byte) 87,
        (byte) 183,
        (byte) 100,
        (byte) 49,
        (byte) 82,
        (byte) 224 /*0xE0*/,
        (byte) 184,
        (byte) 238,
        (byte) 228,
        (byte) 63 /*0x3F*/,
        (byte) 115,
        (byte) 187,
        (byte) 122,
        (byte) 42,
        (byte) 84,
        (byte) 216,
        (byte) 219,
        (byte) 228,
        (byte) 2,
        (byte) 124,
        (byte) 140,
        (byte) 19,
        (byte) 67,
        (byte) 219,
        (byte) 39,
        (byte) 132,
        (byte) 19,
        (byte) 100,
        (byte) 94,
        (byte) 149,
        (byte) 155,
        (byte) 189,
        (byte) 85,
        (byte) 99,
        (byte) 78,
        (byte) 61,
        (byte) 143,
        (byte) 3,
        (byte) 217,
        (byte) 137,
        (byte) 240 /*0xF0*/,
        (byte) 196,
        (byte) 36,
        (byte) 220,
        (byte) 4,
        (byte) 154,
        (byte) 238,
        (byte) 182,
        (byte) 2,
        (byte) 7,
        (byte) 24,
        (byte) 89
      };
      byte[] numArray7 = new byte[55];
      numArray7[51] = (byte) 251;
      numArray7[36] = (byte) 251;
      numArray7[21] = (byte) 149;
      numArray7[23] = (byte) 227;
      numArray7[4] = (byte) 231;
      numArray7[5] = (byte) 113;
      numArray7[48 /*0x30*/] = (byte) 170;
      numArray7[7] = (byte) 37;
      numArray7[8] = (byte) 89;
      numArray7[9] = (byte) 128 /*0x80*/;
      numArray7[43] = (byte) 188;
      numArray7[30] = (byte) 191;
      numArray7[17] = (byte) 9;
      numArray7[53] = (byte) 123;
      numArray7[2] = (byte) 245;
      numArray7[15] = (byte) 60;
      numArray7[38] = (byte) 158;
      numArray7[50] = (byte) 58;
      numArray7[49] = (byte) 193;
      numArray7[3] = (byte) 233;
      numArray7[20] = (byte) 98;
      numArray7[16 /*0x10*/] = (byte) 29;
      numArray7[11] = (byte) 108;
      numArray7[1] = (byte) 205;
      numArray7[35] = (byte) 157;
      numArray7[25] = (byte) 24;
      numArray7[52] = (byte) 243;
      numArray7[27] = (byte) 136;
      numArray7[28] = (byte) 253;
      numArray7[24] = (byte) 79;
      numArray7[45] = (byte) 96 /*0x60*/;
      numArray7[31 /*0x1F*/] = (byte) 109;
      numArray7[32 /*0x20*/] = (byte) 235;
      numArray7[33] = (byte) 174;
      numArray7[34] = (byte) 17;
      numArray7[10] = (byte) 4;
      numArray7[14] = (byte) 15;
      numArray7[40] = (byte) 160 /*0xA0*/;
      numArray7[37] = (byte) 147;
      numArray7[29] = (byte) 187;
      numArray7[39] = (byte) 26;
      numArray7[41] = (byte) 131;
      numArray7[42] = (byte) 38;
      numArray7[12] = (byte) 176 /*0xB0*/;
      numArray7[6] = (byte) 241;
      numArray7[0] = (byte) 249;
      numArray7[46] = (byte) 42;
      numArray7[47] = (byte) 46;
      numArray7[22] = (byte) 181;
      numArray7[18] = (byte) 201;
      numArray7[44] = (byte) 165;
      numArray7[54] = (byte) 153;
      numArray7[19] = (byte) 235;
      numArray7[13] = (byte) 121;
      numArray7[26] = (byte) 12;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 99,
        (byte) 83,
        (byte) 13,
        (byte) 177,
        (byte) 124,
        (byte) 56,
        (byte) 112 /*0x70*/,
        (byte) 242,
        (byte) 57,
        (byte) 209,
        (byte) 98,
        (byte) 229,
        (byte) 21,
        (byte) 223,
        (byte) 209,
        (byte) 76,
        (byte) 66,
        (byte) 221,
        (byte) 0,
        (byte) 189,
        (byte) 239,
        (byte) 148,
        (byte) 26,
        (byte) 46,
        (byte) 136,
        (byte) 88,
        (byte) 190,
        (byte) 130,
        (byte) 117,
        (byte) 152,
        (byte) 60,
        (byte) 244,
        (byte) 91,
        (byte) 214,
        (byte) 211,
        (byte) 183,
        (byte) 188,
        (byte) 84,
        (byte) 125,
        (byte) 210,
        (byte) 75,
        (byte) 170,
        (byte) 250,
        (byte) 4,
        (byte) 214,
        (byte) 104,
        (byte) 30,
        (byte) 247,
        (byte) 8,
        (byte) 159,
        (byte) 123,
        (byte) 99,
        (byte) 78,
        (byte) 182,
        (byte) 138
      };
      byte[] numArray9 = new byte[55];
      numArray9[24] = (byte) 35;
      numArray9[45] = (byte) 177;
      numArray9[19] = (byte) 103;
      numArray9[27] = (byte) 121;
      numArray9[38] = (byte) 216;
      numArray9[5] = (byte) 187;
      numArray9[16 /*0x10*/] = (byte) 134;
      numArray9[7] = (byte) 175;
      numArray9[14] = (byte) 132;
      numArray9[9] = (byte) 129;
      numArray9[36] = (byte) 49;
      numArray9[11] = (byte) 41;
      numArray9[4] = (byte) 136;
      numArray9[13] = (byte) 12;
      numArray9[35] = (byte) 167;
      numArray9[23] = (byte) 140;
      numArray9[32 /*0x20*/] = (byte) 35;
      numArray9[28] = (byte) 210;
      numArray9[31 /*0x1F*/] = (byte) 108;
      numArray9[33] = (byte) 155;
      numArray9[20] = (byte) 88;
      numArray9[21] = (byte) 96 /*0x60*/;
      numArray9[22] = (byte) 25;
      numArray9[17] = (byte) 152;
      numArray9[1] = (byte) 169;
      numArray9[25] = (byte) 240 /*0xF0*/;
      numArray9[26] = (byte) 63 /*0x3F*/;
      numArray9[46] = (byte) 133;
      numArray9[3] = (byte) 203;
      numArray9[29] = (byte) 101;
      numArray9[30] = (byte) 240 /*0xF0*/;
      numArray9[15] = (byte) 5;
      numArray9[48 /*0x30*/] = (byte) 129;
      numArray9[54] = (byte) 141;
      numArray9[34] = (byte) 184;
      numArray9[39] = (byte) 25;
      numArray9[41] = (byte) 227;
      numArray9[37] = (byte) 72;
      numArray9[51] = (byte) 230;
      numArray9[43] = (byte) 203;
      numArray9[40] = (byte) 39;
      numArray9[8] = (byte) 4;
      numArray9[42] = (byte) 38;
      numArray9[6] = (byte) 49;
      numArray9[44] = (byte) 140;
      numArray9[2] = (byte) 235;
      numArray9[10] = (byte) 159;
      numArray9[47] = (byte) 182;
      numArray9[0] = (byte) 61;
      numArray9[49] = (byte) 132;
      numArray9[50] = (byte) 76;
      numArray9[12] = (byte) 235;
      numArray9[52] = (byte) 111;
      numArray9[53] = (byte) 141;
      numArray9[18] = (byte) 12;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 134,
        (byte) 8,
        (byte) 233,
        (byte) 179,
        (byte) 115,
        (byte) 166,
        (byte) 132,
        (byte) 186,
        (byte) 199,
        (byte) 228,
        (byte) 244,
        (byte) 7,
        (byte) 156,
        (byte) 178,
        (byte) 193,
        (byte) 6,
        (byte) 213,
        (byte) 20,
        (byte) 22,
        (byte) 63 /*0x3F*/,
        (byte) 166,
        (byte) 239,
        (byte) 65,
        (byte) 91,
        (byte) 218,
        (byte) 140,
        (byte) 198,
        (byte) 141,
        (byte) 214,
        (byte) 133,
        (byte) 97,
        (byte) 5,
        (byte) 206,
        (byte) 90,
        (byte) 86,
        (byte) 36,
        (byte) 111,
        (byte) 165,
        (byte) 234,
        (byte) 135,
        (byte) 212,
        (byte) 181,
        (byte) 160 /*0xA0*/,
        (byte) 250,
        (byte) 234,
        (byte) 228,
        (byte) 81,
        (byte) 93,
        (byte) 204,
        (byte) 252,
        (byte) 75,
        (byte) 47,
        (byte) 47,
        (byte) 246,
        (byte) 125
      };
      byte[] numArray11 = new byte[55]
      {
        (byte) 253,
        (byte) 71,
        (byte) 70,
        (byte) 238,
        (byte) 247,
        (byte) 35,
        (byte) 89,
        (byte) 142,
        (byte) 131,
        (byte) 251,
        (byte) 163,
        (byte) 223,
        (byte) 29,
        (byte) 52,
        (byte) 73,
        (byte) 197,
        (byte) 77,
        (byte) 70,
        (byte) 162,
        (byte) 182,
        (byte) 30,
        (byte) 211,
        (byte) 162,
        (byte) 35,
        (byte) 245,
        (byte) 162,
        (byte) 0,
        (byte) 229,
        (byte) 114,
        (byte) 115,
        (byte) 16 /*0x10*/,
        (byte) 3,
        (byte) 82,
        (byte) 133,
        (byte) 193,
        (byte) 90,
        (byte) 16 /*0x10*/,
        (byte) 235,
        (byte) 234,
        (byte) 52,
        (byte) 100,
        (byte) 204,
        (byte) 158,
        (byte) 59,
        (byte) 99,
        (byte) 5,
        (byte) 161,
        (byte) 250,
        (byte) 230,
        (byte) 203,
        (byte) 85,
        (byte) 87,
        (byte) 219,
        (byte) 184,
        (byte) 61
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[30]
      {
        (byte) 142,
        (byte) 216,
        (byte) 231,
        (byte) 224 /*0xE0*/,
        (byte) 246,
        (byte) 227,
        (byte) 66,
        (byte) 130,
        (byte) 239,
        (byte) 56,
        (byte) 66,
        (byte) 91,
        (byte) 157,
        (byte) 151,
        (byte) 47,
        (byte) 42,
        (byte) 143,
        (byte) 106,
        (byte) 240 /*0xF0*/,
        (byte) 79,
        (byte) 70,
        (byte) 113,
        (byte) 246,
        (byte) 59,
        (byte) 104,
        (byte) 192 /*0xC0*/,
        (byte) 218,
        (byte) 57,
        (byte) 50,
        (byte) 213
      };
      byte[] numArray13 = new byte[30];
      numArray13[19] = (byte) 109;
      numArray13[14] = (byte) 77;
      numArray13[24] = (byte) 144 /*0x90*/;
      numArray13[22] = (byte) 115;
      numArray13[26] = (byte) 100;
      numArray13[3] = (byte) 45;
      numArray13[6] = (byte) 6;
      numArray13[17] = (byte) 180;
      numArray13[8] = (byte) 123;
      numArray13[9] = (byte) 205;
      numArray13[25] = (byte) 174;
      numArray13[11] = (byte) 64 /*0x40*/;
      numArray13[12] = (byte) 160 /*0xA0*/;
      numArray13[23] = (byte) 224 /*0xE0*/;
      numArray13[20] = (byte) 92;
      numArray13[15] = (byte) 180;
      numArray13[16 /*0x10*/] = (byte) 214;
      numArray13[21] = (byte) 155;
      numArray13[18] = (byte) 151;
      numArray13[7] = (byte) 21;
      numArray13[10] = (byte) 148;
      numArray13[5] = (byte) 81;
      numArray13[2] = (byte) 163;
      numArray13[13] = (byte) 40;
      numArray13[0] = (byte) 135;
      numArray13[4] = (byte) 237;
      numArray13[27] = (byte) 136;
      numArray13[1] = (byte) 82;
      numArray13[28] = (byte) 43;
      numArray13[29] = (byte) 70;
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 275] ^= numArray13[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray14 = new byte[305];
    byte[] numArray15 = new byte[55];
    numArray15[14] = (byte) 74;
    numArray15[53] = (byte) 234;
    numArray15[2] = (byte) 101;
    numArray15[3] = (byte) 155;
    numArray15[4] = (byte) 64 /*0x40*/;
    numArray15[31 /*0x1F*/] = (byte) 199;
    numArray15[26] = (byte) 195;
    numArray15[40] = (byte) 184;
    numArray15[48 /*0x30*/] = (byte) 151;
    numArray15[9] = (byte) 140;
    numArray15[10] = (byte) 31 /*0x1F*/;
    numArray15[38] = (byte) 239;
    numArray15[19] = (byte) 30;
    numArray15[13] = (byte) 118;
    numArray15[44] = (byte) 108;
    numArray15[24] = (byte) 120;
    numArray15[42] = (byte) 183;
    numArray15[17] = (byte) 145;
    numArray15[35] = (byte) 74;
    numArray15[6] = (byte) 98;
    numArray15[20] = (byte) 45;
    numArray15[21] = (byte) 52;
    numArray15[37] = (byte) 102;
    numArray15[23] = (byte) 80 /*0x50*/;
    numArray15[11] = (byte) 241;
    numArray15[25] = (byte) 220;
    numArray15[0] = (byte) 26;
    numArray15[27] = (byte) 235;
    numArray15[47] = (byte) 166;
    numArray15[15] = (byte) 44;
    numArray15[30] = (byte) 167;
    numArray15[7] = (byte) 39;
    numArray15[32 /*0x20*/] = (byte) 85;
    numArray15[33] = (byte) 40;
    numArray15[34] = (byte) 224 /*0xE0*/;
    numArray15[16 /*0x10*/] = (byte) 102;
    numArray15[36] = (byte) 168;
    numArray15[43] = (byte) 254;
    numArray15[5] = (byte) 182;
    numArray15[18] = (byte) 117;
    numArray15[28] = (byte) 87;
    numArray15[41] = (byte) 221;
    numArray15[8] = (byte) 233;
    numArray15[52] = (byte) 148;
    numArray15[49] = (byte) 127 /*0x7F*/;
    numArray15[45] = (byte) 86;
    numArray15[46] = (byte) 178;
    numArray15[51] = (byte) 164;
    numArray15[22] = (byte) 76;
    numArray15[39] = (byte) 174;
    numArray15[54] = (byte) 34;
    numArray15[12] = (byte) 206;
    numArray15[50] = (byte) 17;
    numArray15[29] = (byte) 46;
    numArray15[1] = (byte) 96 /*0x60*/;
    byte[] numArray16 = new byte[55]
    {
      (byte) 249,
      (byte) 185,
      (byte) 211,
      (byte) 22,
      (byte) 146,
      (byte) 213,
      (byte) 56,
      (byte) 232,
      (byte) 192 /*0xC0*/,
      (byte) 71,
      (byte) 173,
      (byte) 78,
      (byte) 226,
      (byte) 218,
      (byte) 187,
      (byte) 188,
      (byte) 81,
      (byte) 227,
      (byte) 199,
      (byte) 153,
      (byte) 61,
      (byte) 203,
      (byte) 96 /*0x60*/,
      (byte) 157,
      (byte) 73,
      (byte) 52,
      (byte) 24,
      (byte) 89,
      (byte) 187,
      (byte) 124,
      (byte) 66,
      (byte) 43,
      (byte) 60,
      (byte) 167,
      (byte) 244,
      (byte) 9,
      (byte) 42,
      (byte) 198,
      (byte) 226,
      (byte) 12,
      (byte) 200,
      (byte) 124,
      (byte) 116,
      (byte) 5,
      (byte) 248,
      (byte) 64 /*0x40*/,
      (byte) 216,
      (byte) 202,
      (byte) 11,
      (byte) 115,
      (byte) 222,
      (byte) 169,
      (byte) 125,
      (byte) 16 /*0x10*/,
      (byte) 68
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray14, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 49,
      (byte) 1,
      (byte) 57,
      (byte) 213,
      (byte) 35,
      (byte) 248,
      (byte) 157,
      (byte) 226,
      (byte) 36,
      (byte) 49,
      (byte) 160 /*0xA0*/,
      (byte) 155,
      (byte) 201,
      (byte) 240 /*0xF0*/,
      (byte) 189,
      (byte) 21,
      (byte) 10,
      (byte) 36,
      (byte) 66,
      (byte) 127 /*0x7F*/,
      (byte) 51,
      (byte) 207,
      (byte) 104,
      (byte) 29,
      (byte) 12,
      (byte) 188,
      (byte) 220,
      (byte) 12,
      byte.MaxValue,
      (byte) 100,
      (byte) 229,
      (byte) 50,
      (byte) 145,
      (byte) 111,
      (byte) 98,
      (byte) 65,
      (byte) 82,
      (byte) 230,
      (byte) 28,
      (byte) 20,
      (byte) 94,
      (byte) 55,
      (byte) 240 /*0xF0*/,
      (byte) 136,
      (byte) 107,
      (byte) 117,
      (byte) 205,
      (byte) 11,
      (byte) 82,
      (byte) 237,
      (byte) 196,
      (byte) 96 /*0x60*/,
      (byte) 143,
      (byte) 233,
      (byte) 147
    };
    byte[] numArray18 = new byte[55];
    numArray18[43] = (byte) 57;
    numArray18[1] = (byte) 110;
    numArray18[22] = (byte) 6;
    numArray18[3] = (byte) 64 /*0x40*/;
    numArray18[20] = (byte) 191;
    numArray18[5] = (byte) 34;
    numArray18[10] = (byte) 139;
    numArray18[0] = (byte) 166;
    numArray18[8] = (byte) 231;
    numArray18[51] = (byte) 197;
    numArray18[27] = (byte) 48 /*0x30*/;
    numArray18[11] = (byte) 245;
    numArray18[12] = (byte) 136;
    numArray18[35] = (byte) 170;
    numArray18[14] = (byte) 156;
    numArray18[15] = (byte) 180;
    numArray18[17] = (byte) 43;
    numArray18[47] = (byte) 221;
    numArray18[54] = (byte) 4;
    numArray18[19] = (byte) 165;
    numArray18[26] = (byte) 235;
    numArray18[16 /*0x10*/] = (byte) 68;
    numArray18[31 /*0x1F*/] = (byte) 162;
    numArray18[30] = (byte) 155;
    numArray18[24] = (byte) 143;
    numArray18[23] = (byte) 215;
    numArray18[7] = (byte) 106;
    numArray18[6] = (byte) 209;
    numArray18[28] = (byte) 131;
    numArray18[13] = (byte) 175;
    numArray18[34] = (byte) 136;
    numArray18[52] = (byte) 185;
    numArray18[32 /*0x20*/] = (byte) 171;
    numArray18[33] = (byte) 9;
    numArray18[21] = (byte) 246;
    numArray18[18] = (byte) 155;
    numArray18[36] = (byte) 131;
    numArray18[37] = (byte) 211;
    numArray18[46] = (byte) 232;
    numArray18[39] = (byte) 186;
    numArray18[40] = (byte) 140;
    numArray18[41] = (byte) 157;
    numArray18[25] = (byte) 66;
    numArray18[2] = (byte) 116;
    numArray18[45] = (byte) 172;
    numArray18[9] = (byte) 112 /*0x70*/;
    numArray18[44] = (byte) 247;
    numArray18[4] = (byte) 155;
    numArray18[38] = (byte) 93;
    numArray18[49] = (byte) 132;
    numArray18[50] = (byte) 246;
    numArray18[29] = (byte) 157;
    numArray18[42] = (byte) 131;
    numArray18[53] = (byte) 240 /*0xF0*/;
    numArray18[48 /*0x30*/] = (byte) 248;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray14, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 55] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 106,
      (byte) 50,
      (byte) 119,
      (byte) 75,
      (byte) 215,
      (byte) 207,
      (byte) 31 /*0x1F*/,
      (byte) 199,
      (byte) 203,
      (byte) 98,
      (byte) 126,
      (byte) 141,
      (byte) 213,
      (byte) 141,
      (byte) 7,
      (byte) 81,
      (byte) 54,
      (byte) 83,
      (byte) 166,
      (byte) 174,
      (byte) 163,
      (byte) 180,
      (byte) 44,
      (byte) 124,
      (byte) 83,
      (byte) 228,
      (byte) 232,
      (byte) 0,
      (byte) 43,
      (byte) 64 /*0x40*/,
      (byte) 223,
      (byte) 68,
      (byte) 125,
      (byte) 237,
      (byte) 203,
      (byte) 233,
      (byte) 62,
      (byte) 172,
      (byte) 246,
      (byte) 133,
      (byte) 16 /*0x10*/,
      (byte) 116,
      (byte) 49,
      (byte) 8,
      (byte) 244,
      (byte) 239,
      byte.MaxValue,
      (byte) 156,
      (byte) 152,
      (byte) 7,
      (byte) 43,
      (byte) 116,
      (byte) 240 /*0xF0*/,
      (byte) 212,
      (byte) 21
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 83,
      (byte) 204,
      (byte) 179,
      (byte) 45,
      (byte) 122,
      (byte) 144 /*0x90*/,
      (byte) 164,
      (byte) 156,
      (byte) 219,
      (byte) 125,
      (byte) 237,
      (byte) 120,
      (byte) 210,
      (byte) 117,
      (byte) 72,
      (byte) 122,
      (byte) 177,
      (byte) 17,
      (byte) 236,
      (byte) 38,
      (byte) 218,
      (byte) 18,
      (byte) 249,
      (byte) 88,
      (byte) 3,
      (byte) 2,
      (byte) 198,
      (byte) 173,
      (byte) 127 /*0x7F*/,
      (byte) 45,
      (byte) 129,
      (byte) 40,
      (byte) 189,
      (byte) 172,
      (byte) 82,
      (byte) 77,
      (byte) 206,
      (byte) 228,
      (byte) 52,
      (byte) 35,
      (byte) 216,
      (byte) 240 /*0xF0*/,
      (byte) 184,
      (byte) 252,
      (byte) 31 /*0x1F*/,
      (byte) 1,
      (byte) 227,
      (byte) 194,
      (byte) 12,
      (byte) 63 /*0x3F*/,
      (byte) 185,
      (byte) 3,
      (byte) 200,
      (byte) 75,
      (byte) 150
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray14, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 110] ^= numArray20[index];
    byte[] numArray21 = new byte[55]
    {
      (byte) 251,
      (byte) 247,
      (byte) 242,
      (byte) 230,
      (byte) 30,
      (byte) 156,
      (byte) 139,
      (byte) 131,
      (byte) 58,
      (byte) 16 /*0x10*/,
      (byte) 231,
      (byte) 194,
      (byte) 29,
      (byte) 19,
      (byte) 137,
      (byte) 93,
      (byte) 33,
      (byte) 158,
      (byte) 47,
      (byte) 131,
      (byte) 35,
      (byte) 136,
      (byte) 99,
      (byte) 228,
      (byte) 76,
      (byte) 104,
      (byte) 35,
      (byte) 20,
      (byte) 106,
      (byte) 84,
      (byte) 168,
      (byte) 215,
      (byte) 20,
      (byte) 227,
      (byte) 66,
      (byte) 180,
      (byte) 3,
      (byte) 158,
      (byte) 241,
      (byte) 215,
      (byte) 166,
      (byte) 133,
      (byte) 185,
      (byte) 223,
      (byte) 23,
      (byte) 217,
      (byte) 122,
      (byte) 244,
      (byte) 162,
      (byte) 23,
      (byte) 207,
      (byte) 54,
      (byte) 229,
      (byte) 51,
      (byte) 226
    };
    byte[] numArray22 = new byte[55]
    {
      (byte) 196,
      (byte) 8,
      (byte) 210,
      (byte) 245,
      (byte) 181,
      (byte) 24,
      (byte) 0,
      (byte) 91,
      (byte) 213,
      (byte) 154,
      (byte) 4,
      (byte) 143,
      (byte) 126,
      (byte) 141,
      (byte) 175,
      (byte) 113,
      (byte) 194,
      (byte) 103,
      (byte) 209,
      (byte) 233,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 135,
      (byte) 97,
      (byte) 194,
      (byte) 90,
      (byte) 77,
      (byte) 132,
      (byte) 145,
      (byte) 164,
      (byte) 53,
      (byte) 50,
      (byte) 223,
      (byte) 88,
      (byte) 124,
      (byte) 192 /*0xC0*/,
      (byte) 47,
      (byte) 211,
      (byte) 139,
      (byte) 239,
      (byte) 170,
      (byte) 12,
      (byte) 15,
      (byte) 156,
      (byte) 230,
      (byte) 163,
      (byte) 28,
      (byte) 187,
      (byte) 230,
      (byte) 104,
      (byte) 44,
      (byte) 194,
      (byte) 213,
      (byte) 2,
      (byte) 183
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray14, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 165] ^= numArray22[index];
    byte[] numArray23 = new byte[55];
    numArray23[9] = (byte) 173;
    numArray23[4] = (byte) 118;
    numArray23[29] = (byte) 241;
    numArray23[3] = (byte) 121;
    numArray23[39] = (byte) 109;
    numArray23[42] = (byte) 7;
    numArray23[35] = (byte) 4;
    numArray23[7] = (byte) 103;
    numArray23[38] = (byte) 47;
    numArray23[2] = (byte) 182;
    numArray23[31 /*0x1F*/] = (byte) 1;
    numArray23[11] = (byte) 208 /*0xD0*/;
    numArray23[50] = (byte) 141;
    numArray23[13] = (byte) 90;
    numArray23[14] = (byte) 130;
    numArray23[15] = (byte) 172;
    numArray23[16 /*0x10*/] = (byte) 88;
    numArray23[27] = (byte) 132;
    numArray23[1] = (byte) 154;
    numArray23[41] = (byte) 119;
    numArray23[20] = (byte) 132;
    numArray23[30] = (byte) 225;
    numArray23[25] = (byte) 114;
    numArray23[10] = (byte) 58;
    numArray23[24] = (byte) 46;
    numArray23[26] = (byte) 83;
    numArray23[8] = (byte) 16 /*0x10*/;
    numArray23[37] = (byte) 93;
    numArray23[12] = (byte) 212;
    numArray23[19] = (byte) 206;
    numArray23[17] = (byte) 221;
    numArray23[28] = (byte) 114;
    numArray23[40] = (byte) 211;
    numArray23[33] = (byte) 32 /*0x20*/;
    numArray23[49] = (byte) 137;
    numArray23[36] = (byte) 59;
    numArray23[34] = (byte) 244;
    numArray23[43] = (byte) 43;
    numArray23[48 /*0x30*/] = (byte) 77;
    numArray23[5] = (byte) 24;
    numArray23[18] = (byte) 158;
    numArray23[54] = (byte) 15;
    numArray23[21] = (byte) 204;
    numArray23[6] = (byte) 230;
    numArray23[44] = (byte) 64 /*0x40*/;
    numArray23[45] = (byte) 35;
    numArray23[46] = (byte) 205;
    numArray23[47] = (byte) 188;
    numArray23[32 /*0x20*/] = (byte) 207;
    numArray23[0] = (byte) 14;
    numArray23[53] = (byte) 164;
    numArray23[23] = (byte) 238;
    numArray23[52] = (byte) 67;
    numArray23[22] = (byte) 115;
    numArray23[51] = (byte) 86;
    byte[] numArray24 = new byte[55];
    numArray24[54] = (byte) 18;
    numArray24[4] = (byte) 94;
    numArray24[2] = (byte) 122;
    numArray24[3] = (byte) 113;
    numArray24[12] = (byte) 193;
    numArray24[1] = (byte) 110;
    numArray24[34] = (byte) 39;
    numArray24[45] = (byte) 158;
    numArray24[0] = (byte) 59;
    numArray24[9] = (byte) 36;
    numArray24[13] = (byte) 101;
    numArray24[11] = (byte) 227;
    numArray24[46] = (byte) 174;
    numArray24[28] = (byte) 124;
    numArray24[14] = (byte) 179;
    numArray24[15] = (byte) 228;
    numArray24[16 /*0x10*/] = (byte) 147;
    numArray24[18] = (byte) 42;
    numArray24[53] = (byte) 117;
    numArray24[19] = (byte) 253;
    numArray24[17] = (byte) 24;
    numArray24[21] = (byte) 248;
    numArray24[44] = (byte) 94;
    numArray24[6] = (byte) 35;
    numArray24[5] = (byte) 75;
    numArray24[25] = (byte) 23;
    numArray24[26] = (byte) 253;
    numArray24[41] = (byte) 122;
    numArray24[48 /*0x30*/] = (byte) 15;
    numArray24[29] = (byte) 86;
    numArray24[49] = (byte) 250;
    numArray24[22] = (byte) 51;
    numArray24[32 /*0x20*/] = (byte) 201;
    numArray24[33] = (byte) 222;
    numArray24[8] = (byte) 218;
    numArray24[35] = (byte) 85;
    numArray24[51] = (byte) 197;
    numArray24[37] = (byte) 154;
    numArray24[38] = (byte) 44;
    numArray24[27] = (byte) 171;
    numArray24[40] = (byte) 86;
    numArray24[10] = (byte) 94;
    numArray24[42] = (byte) 235;
    numArray24[36] = (byte) 197;
    numArray24[43] = (byte) 25;
    numArray24[20] = (byte) 144 /*0x90*/;
    numArray24[39] = (byte) 44;
    numArray24[47] = (byte) 35;
    numArray24[24] = (byte) 73;
    numArray24[23] = (byte) 48 /*0x30*/;
    numArray24[50] = (byte) 18;
    numArray24[30] = (byte) 125;
    numArray24[7] = (byte) 225;
    numArray24[52] = (byte) 185;
    numArray24[31 /*0x1F*/] = (byte) 146;
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray14, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 220] ^= numArray24[index];
    byte[] numArray25 = new byte[30]
    {
      (byte) 115,
      (byte) 88,
      (byte) 209,
      (byte) 238,
      (byte) 194,
      (byte) 60,
      (byte) 232,
      (byte) 93,
      (byte) 208 /*0xD0*/,
      (byte) 84,
      (byte) 149,
      (byte) 125,
      (byte) 162,
      (byte) 163,
      (byte) 20,
      (byte) 179,
      (byte) 15,
      (byte) 65,
      (byte) 18,
      (byte) 171,
      (byte) 199,
      (byte) 27,
      (byte) 155,
      (byte) 23,
      (byte) 155,
      (byte) 56,
      (byte) 178,
      (byte) 37,
      (byte) 187,
      (byte) 35
    };
    byte[] numArray26 = new byte[30]
    {
      (byte) 202,
      (byte) 211,
      (byte) 146,
      (byte) 165,
      (byte) 94,
      (byte) 209,
      (byte) 190,
      (byte) 31 /*0x1F*/,
      (byte) 6,
      (byte) 64 /*0x40*/,
      (byte) 13,
      (byte) 2,
      (byte) 231,
      (byte) 96 /*0x60*/,
      (byte) 93,
      (byte) 228,
      (byte) 48 /*0x30*/,
      (byte) 250,
      (byte) 247,
      (byte) 184,
      (byte) 163,
      (byte) 202,
      (byte) 16 /*0x10*/,
      (byte) 156,
      (byte) 150,
      (byte) 184,
      (byte) 234,
      (byte) 174,
      (byte) 168,
      (byte) 112 /*0x70*/
    };
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray14, 275, 30);
    for (int index = 0; index < 30; ++index)
      numArray14[index + 275] ^= numArray26[index];
    return Encoding.UTF8.GetString(numArray14);
  }

  internal static string ssp_appserver_13452()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[47];
      byte[] numArray2 = new byte[47];
      numArray2[20] = (byte) 48 /*0x30*/;
      numArray2[1] = (byte) 122;
      numArray2[2] = (byte) 252;
      numArray2[44] = (byte) 56;
      numArray2[45] = (byte) 116;
      numArray2[5] = (byte) 58;
      numArray2[6] = (byte) 70;
      numArray2[15] = (byte) 116;
      numArray2[8] = (byte) 186;
      numArray2[32 /*0x20*/] = (byte) 233;
      numArray2[35] = (byte) 153;
      numArray2[11] = (byte) 38;
      numArray2[12] = (byte) 141;
      numArray2[25] = (byte) 137;
      numArray2[14] = (byte) 151;
      numArray2[36] = (byte) 182;
      numArray2[21] = (byte) 87;
      numArray2[17] = (byte) 222;
      numArray2[40] = (byte) 101;
      numArray2[42] = (byte) 96 /*0x60*/;
      numArray2[41] = (byte) 5;
      numArray2[4] = (byte) 133;
      numArray2[43] = (byte) 163;
      numArray2[3] = (byte) 139;
      numArray2[24] = (byte) 83;
      numArray2[9] = (byte) 247;
      numArray2[22] = (byte) 188;
      numArray2[27] = (byte) 195;
      numArray2[13] = (byte) 121;
      numArray2[23] = (byte) 185;
      numArray2[30] = (byte) 243;
      numArray2[31 /*0x1F*/] = (byte) 1;
      numArray2[7] = (byte) 193;
      numArray2[33] = (byte) 156;
      numArray2[34] = (byte) 188;
      numArray2[16 /*0x10*/] = (byte) 121;
      numArray2[18] = (byte) 103;
      numArray2[37] = (byte) 131;
      numArray2[38] = (byte) 188;
      numArray2[19] = (byte) 245;
      numArray2[28] = (byte) 162;
      numArray2[10] = (byte) 223;
      numArray2[26] = (byte) 138;
      numArray2[29] = (byte) 173;
      numArray2[39] = (byte) 215;
      numArray2[0] = (byte) 230;
      numArray2[46] = (byte) 171;
      byte[] numArray3 = new byte[47]
      {
        (byte) 47,
        (byte) 163,
        (byte) 20,
        (byte) 238,
        (byte) 135,
        (byte) 127 /*0x7F*/,
        (byte) 12,
        (byte) 123,
        (byte) 188,
        (byte) 12,
        (byte) 226,
        (byte) 72,
        (byte) 160 /*0xA0*/,
        (byte) 213,
        (byte) 182,
        (byte) 134,
        (byte) 209,
        (byte) 39,
        (byte) 189,
        (byte) 43,
        (byte) 5,
        (byte) 225,
        (byte) 122,
        (byte) 99,
        (byte) 222,
        (byte) 60,
        (byte) 133,
        (byte) 80 /*0x50*/,
        (byte) 158,
        (byte) 232,
        (byte) 10,
        (byte) 66,
        (byte) 204,
        (byte) 193,
        (byte) 100,
        (byte) 72,
        byte.MaxValue,
        (byte) 160 /*0xA0*/,
        (byte) 211,
        (byte) 160 /*0xA0*/,
        (byte) 31 /*0x1F*/,
        (byte) 115,
        (byte) 196,
        (byte) 57,
        (byte) 223,
        (byte) 169,
        (byte) 99
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[47];
    byte[] numArray5 = new byte[47]
    {
      (byte) 83,
      (byte) 158,
      (byte) 90,
      (byte) 55,
      (byte) 186,
      (byte) 123,
      (byte) 144 /*0x90*/,
      (byte) 137,
      (byte) 179,
      (byte) 97,
      (byte) 157,
      (byte) 13,
      (byte) 170,
      (byte) 152,
      (byte) 164,
      (byte) 194,
      (byte) 90,
      (byte) 29,
      (byte) 94,
      (byte) 77,
      (byte) 196,
      (byte) 9,
      (byte) 148,
      (byte) 9,
      (byte) 241,
      (byte) 41,
      (byte) 143,
      (byte) 34,
      (byte) 74,
      (byte) 171,
      (byte) 96 /*0x60*/,
      (byte) 103,
      (byte) 174,
      (byte) 68,
      (byte) 163,
      (byte) 122,
      (byte) 38,
      (byte) 166,
      (byte) 35,
      (byte) 214,
      (byte) 165,
      (byte) 209,
      (byte) 80 /*0x50*/,
      (byte) 14,
      (byte) 55,
      (byte) 62,
      (byte) 28
    };
    byte[] numArray6 = new byte[47];
    numArray6[34] = (byte) 10;
    numArray6[1] = (byte) 65;
    numArray6[2] = (byte) 151;
    numArray6[41] = (byte) 186;
    numArray6[4] = (byte) 17;
    numArray6[5] = (byte) 86;
    numArray6[43] = (byte) 153;
    numArray6[7] = (byte) 126;
    numArray6[28] = (byte) 146;
    numArray6[9] = (byte) 243;
    numArray6[10] = (byte) 165;
    numArray6[11] = (byte) 84;
    numArray6[8] = (byte) 163;
    numArray6[42] = (byte) 231;
    numArray6[31 /*0x1F*/] = (byte) 22;
    numArray6[15] = (byte) 245;
    numArray6[21] = (byte) 250;
    numArray6[19] = (byte) 190;
    numArray6[3] = (byte) 164;
    numArray6[27] = (byte) 17;
    numArray6[20] = (byte) 162;
    numArray6[29] = (byte) 222;
    numArray6[22] = (byte) 14;
    numArray6[36] = (byte) 46;
    numArray6[24] = (byte) 89;
    numArray6[16 /*0x10*/] = (byte) 102;
    numArray6[26] = (byte) 182;
    numArray6[13] = (byte) 9;
    numArray6[25] = (byte) 41;
    numArray6[18] = (byte) 80 /*0x50*/;
    numArray6[30] = (byte) 168;
    numArray6[33] = (byte) 113;
    numArray6[32 /*0x20*/] = (byte) 61;
    numArray6[40] = (byte) 210;
    numArray6[37] = (byte) 28;
    numArray6[12] = (byte) 153;
    numArray6[6] = (byte) 109;
    numArray6[17] = (byte) 116;
    numArray6[38] = (byte) 34;
    numArray6[39] = (byte) 224 /*0xE0*/;
    numArray6[45] = (byte) 253;
    numArray6[35] = (byte) 84;
    numArray6[14] = (byte) 233;
    numArray6[0] = (byte) 208 /*0xD0*/;
    numArray6[44] = (byte) 118;
    numArray6[23] = (byte) 160 /*0xA0*/;
    numArray6[46] = (byte) 247;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 47);
    for (int index = 0; index < 47; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13453()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[22] = (byte) 152;
      numArray2[1] = (byte) 1;
      numArray2[2] = (byte) 224 /*0xE0*/;
      numArray2[20] = (byte) 76;
      numArray2[17] = (byte) 241;
      numArray2[4] = (byte) 160 /*0xA0*/;
      numArray2[14] = (byte) 127 /*0x7F*/;
      numArray2[18] = (byte) 129;
      numArray2[8] = (byte) 170;
      numArray2[15] = (byte) 148;
      numArray2[11] = (byte) 45;
      numArray2[9] = (byte) 66;
      numArray2[6] = (byte) 21;
      numArray2[13] = (byte) 132;
      numArray2[10] = (byte) 236;
      numArray2[0] = (byte) 12;
      numArray2[16 /*0x10*/] = (byte) 131;
      numArray2[5] = (byte) 12;
      numArray2[7] = (byte) 60;
      numArray2[19] = (byte) 166;
      numArray2[21] = (byte) 69;
      numArray2[3] = (byte) 181;
      numArray2[12] = (byte) 143;
      byte[] numArray3 = new byte[23]
      {
        (byte) 186,
        (byte) 175,
        (byte) 93,
        (byte) 105,
        (byte) 235,
        (byte) 206,
        (byte) 144 /*0x90*/,
        (byte) 115,
        (byte) 145,
        (byte) 121,
        (byte) 208 /*0xD0*/,
        (byte) 31 /*0x1F*/,
        (byte) 176 /*0xB0*/,
        (byte) 225,
        (byte) 82,
        (byte) 140,
        (byte) 26,
        (byte) 104,
        (byte) 233,
        byte.MaxValue,
        (byte) 84,
        (byte) 122,
        (byte) 204
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 204,
      (byte) 16 /*0x10*/,
      (byte) 249,
      (byte) 181,
      (byte) 234,
      (byte) 252,
      (byte) 199,
      (byte) 105,
      (byte) 155,
      (byte) 114,
      (byte) 4,
      (byte) 81,
      (byte) 78,
      (byte) 233,
      (byte) 68,
      (byte) 165,
      (byte) 123,
      (byte) 203,
      (byte) 29,
      (byte) 11,
      (byte) 176 /*0xB0*/,
      (byte) 194,
      (byte) 43
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 226,
      (byte) 93,
      (byte) 19,
      (byte) 193,
      (byte) 158,
      (byte) 145,
      (byte) 244,
      (byte) 1,
      (byte) 60,
      (byte) 2,
      (byte) 240 /*0xF0*/,
      (byte) 35,
      (byte) 69,
      (byte) 89,
      (byte) 13,
      (byte) 73,
      (byte) 171,
      (byte) 248,
      (byte) 100,
      (byte) 35,
      (byte) 156,
      (byte) 130,
      (byte) 130
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13454()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[3] = (byte) 78;
      numArray2[0] = (byte) 51;
      numArray2[2] = (byte) 22;
      numArray2[1] = (byte) 61;
      numArray2[4] = (byte) 209;
      numArray2[6] = (byte) 113;
      numArray2[5] = (byte) 207;
      numArray2[7] = (byte) 135;
      numArray2[8] = (byte) 85;
      numArray2[9] = (byte) 164;
      byte[] numArray3 = new byte[10]
      {
        (byte) 63 /*0x3F*/,
        (byte) 80 /*0x50*/,
        (byte) 167,
        (byte) 166,
        (byte) 115,
        (byte) 235,
        (byte) 220,
        (byte) 78,
        (byte) 36,
        (byte) 209
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[1] = (byte) 42;
    numArray5[4] = (byte) 164;
    numArray5[2] = (byte) 172;
    numArray5[3] = (byte) 224 /*0xE0*/;
    numArray5[9] = (byte) 200;
    numArray5[5] = (byte) 9;
    numArray5[6] = (byte) 228;
    numArray5[0] = (byte) 178;
    numArray5[8] = (byte) 19;
    numArray5[7] = (byte) 97;
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 6;
    numArray6[1] = (byte) 31 /*0x1F*/;
    numArray6[2] = (byte) 42;
    numArray6[0] = (byte) 41;
    numArray6[8] = (byte) 138;
    numArray6[4] = (byte) 236;
    numArray6[6] = (byte) 239;
    numArray6[7] = (byte) 151;
    numArray6[5] = (byte) 180;
    numArray6[3] = (byte) 167;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[17];
    byte[] response = new byte[17];
    Array.Copy((Array) sc_13393.sspq, 747, (Array) numArray7, 0, 17);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13393.sspr, 747, (Array) numArray7, 0, 17);
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

  internal static string ssp_appserver_13455()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[85];
      byte[] numArray2 = new byte[55];
      numArray2[45] = (byte) 169;
      numArray2[1] = (byte) 222;
      numArray2[48 /*0x30*/] = (byte) 238;
      numArray2[3] = (byte) 241;
      numArray2[44] = (byte) 33;
      numArray2[18] = (byte) 14;
      numArray2[36] = (byte) 209;
      numArray2[53] = (byte) 118;
      numArray2[8] = (byte) 126;
      numArray2[9] = (byte) 159;
      numArray2[17] = byte.MaxValue;
      numArray2[11] = (byte) 254;
      numArray2[6] = (byte) 134;
      numArray2[13] = (byte) 246;
      numArray2[14] = (byte) 46;
      numArray2[31 /*0x1F*/] = (byte) 31 /*0x1F*/;
      numArray2[26] = (byte) 97;
      numArray2[0] = (byte) 50;
      numArray2[41] = (byte) 113;
      numArray2[19] = (byte) 148;
      numArray2[12] = (byte) 218;
      numArray2[21] = (byte) 229;
      numArray2[38] = (byte) 78;
      numArray2[28] = (byte) 16 /*0x10*/;
      numArray2[10] = (byte) 32 /*0x20*/;
      numArray2[25] = (byte) 164;
      numArray2[5] = (byte) 3;
      numArray2[30] = (byte) 131;
      numArray2[24] = (byte) 25;
      numArray2[4] = (byte) 214;
      numArray2[20] = (byte) 17;
      numArray2[7] = (byte) 90;
      numArray2[32 /*0x20*/] = (byte) 9;
      numArray2[42] = (byte) 254;
      numArray2[49] = (byte) 149;
      numArray2[35] = (byte) 135;
      numArray2[29] = (byte) 217;
      numArray2[37] = (byte) 115;
      numArray2[50] = (byte) 146;
      numArray2[39] = (byte) 32 /*0x20*/;
      numArray2[40] = (byte) 103;
      numArray2[27] = (byte) 60;
      numArray2[23] = (byte) 190;
      numArray2[43] = (byte) 209;
      numArray2[34] = (byte) 5;
      numArray2[2] = (byte) 181;
      numArray2[46] = (byte) 212;
      numArray2[33] = (byte) 150;
      numArray2[47] = (byte) 80 /*0x50*/;
      numArray2[54] = (byte) 69;
      numArray2[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray2[51] = (byte) 195;
      numArray2[52] = (byte) 19;
      numArray2[15] = (byte) 164;
      numArray2[22] = (byte) 126;
      byte[] numArray3 = new byte[55];
      numArray3[35] = (byte) 47;
      numArray3[31 /*0x1F*/] = (byte) 42;
      numArray3[2] = (byte) 236;
      numArray3[22] = (byte) 72;
      numArray3[9] = (byte) 245;
      numArray3[8] = (byte) 204;
      numArray3[27] = (byte) 32 /*0x20*/;
      numArray3[4] = (byte) 214;
      numArray3[28] = (byte) 226;
      numArray3[51] = (byte) 162;
      numArray3[12] = (byte) 79;
      numArray3[11] = (byte) 247;
      numArray3[41] = (byte) 162;
      numArray3[37] = (byte) 8;
      numArray3[39] = (byte) 164;
      numArray3[15] = (byte) 106;
      numArray3[16 /*0x10*/] = (byte) 164;
      numArray3[17] = (byte) 116;
      numArray3[14] = (byte) 228;
      numArray3[19] = (byte) 161;
      numArray3[20] = (byte) 215;
      numArray3[0] = (byte) 209;
      numArray3[13] = (byte) 245;
      numArray3[23] = (byte) 112 /*0x70*/;
      numArray3[24] = (byte) 159;
      numArray3[54] = (byte) 183;
      numArray3[5] = (byte) 77;
      numArray3[42] = (byte) 42;
      numArray3[21] = (byte) 116;
      numArray3[45] = (byte) 203;
      numArray3[10] = (byte) 231;
      numArray3[18] = (byte) 160 /*0xA0*/;
      numArray3[1] = (byte) 119;
      numArray3[33] = (byte) 2;
      numArray3[30] = (byte) 93;
      numArray3[25] = (byte) 81;
      numArray3[36] = (byte) 64 /*0x40*/;
      numArray3[7] = (byte) 63 /*0x3F*/;
      numArray3[38] = (byte) 173;
      numArray3[29] = (byte) 51;
      numArray3[40] = (byte) 233;
      numArray3[48 /*0x30*/] = (byte) 98;
      numArray3[34] = (byte) 103;
      numArray3[43] = (byte) 119;
      numArray3[44] = (byte) 204;
      numArray3[26] = (byte) 53;
      numArray3[46] = (byte) 166;
      numArray3[47] = (byte) 16 /*0x10*/;
      numArray3[32 /*0x20*/] = (byte) 235;
      numArray3[49] = (byte) 2;
      numArray3[50] = (byte) 203;
      numArray3[6] = (byte) 226;
      numArray3[52] = (byte) 80 /*0x50*/;
      numArray3[53] = (byte) 157;
      numArray3[3] = (byte) 174;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30];
      numArray4[20] = (byte) 157;
      numArray4[0] = (byte) 220;
      numArray4[2] = (byte) 33;
      numArray4[1] = (byte) 171;
      numArray4[4] = (byte) 247;
      numArray4[25] = (byte) 61;
      numArray4[16 /*0x10*/] = (byte) 129;
      numArray4[10] = (byte) 71;
      numArray4[8] = (byte) 15;
      numArray4[9] = (byte) 24;
      numArray4[28] = (byte) 49;
      numArray4[24] = (byte) 143;
      numArray4[19] = (byte) 146;
      numArray4[13] = (byte) 117;
      numArray4[14] = (byte) 219;
      numArray4[22] = (byte) 104;
      numArray4[15] = (byte) 219;
      numArray4[17] = (byte) 238;
      numArray4[18] = (byte) 32 /*0x20*/;
      numArray4[5] = (byte) 21;
      numArray4[11] = (byte) 234;
      numArray4[7] = (byte) 172;
      numArray4[3] = (byte) 221;
      numArray4[21] = (byte) 81;
      numArray4[6] = byte.MaxValue;
      numArray4[12] = (byte) 12;
      numArray4[26] = (byte) 238;
      numArray4[27] = (byte) 136;
      numArray4[23] = (byte) 164;
      numArray4[29] = (byte) 47;
      byte[] numArray5 = new byte[30]
      {
        (byte) 98,
        (byte) 68,
        (byte) 172,
        (byte) 72,
        (byte) 30,
        (byte) 220,
        (byte) 169,
        (byte) 133,
        (byte) 162,
        (byte) 248,
        (byte) 181,
        (byte) 32 /*0x20*/,
        (byte) 99,
        (byte) 150,
        (byte) 146,
        (byte) 221,
        (byte) 186,
        (byte) 5,
        (byte) 254,
        (byte) 134,
        (byte) 30,
        (byte) 62,
        (byte) 121,
        (byte) 146,
        (byte) 26,
        (byte) 234,
        (byte) 171,
        (byte) 101,
        (byte) 39,
        (byte) 115
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[85];
    byte[] numArray7 = new byte[55]
    {
      (byte) 223,
      (byte) 12,
      (byte) 198,
      (byte) 116,
      (byte) 85,
      (byte) 208 /*0xD0*/,
      (byte) 82,
      (byte) 67,
      (byte) 113,
      (byte) 197,
      (byte) 222,
      (byte) 150,
      (byte) 240 /*0xF0*/,
      (byte) 53,
      (byte) 46,
      (byte) 79,
      (byte) 86,
      (byte) 129,
      (byte) 154,
      (byte) 140,
      (byte) 144 /*0x90*/,
      (byte) 250,
      (byte) 191,
      (byte) 117,
      (byte) 46,
      (byte) 151,
      (byte) 25,
      (byte) 69,
      (byte) 27,
      (byte) 194,
      (byte) 110,
      (byte) 27,
      (byte) 133,
      (byte) 50,
      (byte) 60,
      (byte) 46,
      (byte) 142,
      (byte) 197,
      (byte) 103,
      (byte) 132,
      (byte) 169,
      (byte) 94,
      (byte) 196,
      (byte) 134,
      (byte) 44,
      (byte) 176 /*0xB0*/,
      (byte) 47,
      (byte) 86,
      (byte) 20,
      (byte) 45,
      (byte) 247,
      (byte) 57,
      (byte) 140,
      (byte) 233,
      (byte) 234
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 220,
      (byte) 43,
      (byte) 126,
      (byte) 17,
      (byte) 49,
      (byte) 71,
      (byte) 185,
      (byte) 240 /*0xF0*/,
      (byte) 177,
      (byte) 81,
      (byte) 39,
      (byte) 131,
      (byte) 181,
      (byte) 209,
      (byte) 179,
      (byte) 25,
      (byte) 106,
      (byte) 198,
      (byte) 163,
      (byte) 0,
      (byte) 53,
      (byte) 127 /*0x7F*/,
      (byte) 71,
      (byte) 194,
      (byte) 77,
      (byte) 78,
      (byte) 162,
      (byte) 43,
      (byte) 68,
      (byte) 216,
      (byte) 218,
      (byte) 123,
      (byte) 238,
      (byte) 1,
      (byte) 197,
      (byte) 167,
      (byte) 120,
      (byte) 214,
      (byte) 254,
      (byte) 34,
      (byte) 236,
      (byte) 72,
      (byte) 212,
      (byte) 58,
      (byte) 238,
      (byte) 57,
      (byte) 229,
      (byte) 175,
      (byte) 27,
      (byte) 147,
      (byte) 33,
      (byte) 29,
      (byte) 92,
      (byte) 42,
      (byte) 228
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[30]
    {
      (byte) 80 /*0x50*/,
      (byte) 69,
      (byte) 189,
      (byte) 182,
      (byte) 12,
      (byte) 39,
      (byte) 174,
      (byte) 87,
      (byte) 194,
      (byte) 17,
      (byte) 190,
      (byte) 248,
      (byte) 0,
      (byte) 193,
      (byte) 144 /*0x90*/,
      (byte) 26,
      (byte) 21,
      (byte) 227,
      (byte) 50,
      (byte) 53,
      (byte) 56,
      (byte) 73,
      (byte) 125,
      (byte) 104,
      (byte) 195,
      (byte) 49,
      (byte) 237,
      (byte) 202,
      (byte) 53,
      (byte) 143
    };
    byte[] numArray10 = new byte[30]
    {
      (byte) 218,
      (byte) 51,
      (byte) 149,
      (byte) 192 /*0xC0*/,
      (byte) 180,
      (byte) 70,
      (byte) 12,
      (byte) 142,
      (byte) 54,
      (byte) 116,
      (byte) 248,
      (byte) 30,
      (byte) 126,
      (byte) 54,
      (byte) 58,
      (byte) 23,
      (byte) 200,
      (byte) 113,
      (byte) 53,
      (byte) 41,
      (byte) 138,
      (byte) 26,
      (byte) 99,
      (byte) 231,
      (byte) 140,
      (byte) 70,
      (byte) 196,
      (byte) 77,
      (byte) 254,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 30);
    for (int index = 0; index < 30; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_13393.sspq, 764, (Array) numArray11, 0, 49);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13393.sspr, 764, (Array) numArray11, 0, 49);
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

  internal static int ssp_appserver_13456(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 149,
      (byte) 143,
      (byte) 167,
      (byte) 206,
      (byte) 53,
      (byte) 183,
      (byte) 84,
      (byte) 219,
      (byte) 194,
      (byte) 252,
      (byte) 49,
      (byte) 13,
      (byte) 54,
      (byte) 40,
      (byte) 250,
      (byte) 187,
      (byte) 245,
      (byte) 231,
      (byte) 85,
      (byte) 174,
      (byte) 46,
      (byte) 192 /*0xC0*/,
      (byte) 188,
      (byte) 143,
      (byte) 9,
      (byte) 39,
      (byte) 233,
      (byte) 94,
      (byte) 140,
      (byte) 189,
      (byte) 185,
      (byte) 128 /*0x80*/,
      (byte) 11,
      (byte) 26,
      (byte) 225,
      (byte) 230,
      (byte) 250,
      (byte) 79,
      (byte) 78,
      (byte) 89,
      (byte) 27,
      (byte) 108,
      (byte) 132,
      byte.MaxValue,
      (byte) 239,
      (byte) 251,
      (byte) 169,
      (byte) 181
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 181,
      (byte) 135,
      (byte) 234,
      (byte) 15,
      (byte) 247,
      (byte) 207,
      (byte) 190,
      (byte) 105,
      (byte) 219,
      (byte) 113,
      (byte) 225,
      (byte) 156,
      (byte) 100,
      (byte) 195,
      (byte) 138,
      (byte) 54,
      (byte) 0,
      (byte) 106,
      (byte) 4,
      (byte) 226,
      (byte) 57,
      (byte) 216,
      (byte) 83,
      (byte) 105,
      (byte) 104,
      (byte) 52,
      (byte) 34,
      (byte) 130,
      (byte) 241,
      (byte) 40,
      (byte) 159,
      (byte) 181,
      (byte) 63 /*0x3F*/,
      (byte) 134,
      (byte) 172,
      (byte) 109,
      (byte) 57,
      (byte) 36,
      (byte) 3,
      (byte) 220,
      (byte) 31 /*0x1F*/,
      (byte) 120,
      (byte) 49,
      (byte) 138,
      (byte) 8,
      (byte) 221,
      (byte) 202,
      (byte) 227
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13457()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[7] = (byte) 14;
      numArray2[21] = (byte) 251;
      numArray2[27] = (byte) 14;
      numArray2[19] = (byte) 166;
      numArray2[6] = (byte) 196;
      numArray2[5] = (byte) 24;
      numArray2[28] = (byte) 125;
      numArray2[1] = (byte) 103;
      numArray2[11] = (byte) 4;
      numArray2[20] = (byte) 153;
      numArray2[8] = (byte) 206;
      numArray2[12] = (byte) 66;
      numArray2[17] = (byte) 19;
      numArray2[2] = (byte) 234;
      numArray2[13] = (byte) 177;
      numArray2[15] = (byte) 28;
      numArray2[16 /*0x10*/] = (byte) 71;
      numArray2[26] = (byte) 159;
      numArray2[18] = (byte) 192 /*0xC0*/;
      numArray2[22] = (byte) 146;
      numArray2[0] = (byte) 14;
      numArray2[10] = (byte) 78;
      numArray2[32 /*0x20*/] = (byte) 150;
      numArray2[31 /*0x1F*/] = (byte) 225;
      numArray2[3] = (byte) 212;
      numArray2[25] = (byte) 188;
      numArray2[9] = (byte) 246;
      numArray2[4] = (byte) 211;
      numArray2[14] = (byte) 2;
      numArray2[24] = (byte) 60;
      numArray2[30] = (byte) 97;
      numArray2[29] = (byte) 51;
      numArray2[23] = (byte) 193;
      numArray2[33] = (byte) 31 /*0x1F*/;
      numArray2[34] = (byte) 113;
      numArray2[35] = (byte) 163;
      byte[] numArray3 = new byte[36]
      {
        (byte) 166,
        (byte) 31 /*0x1F*/,
        (byte) 226,
        (byte) 209,
        (byte) 237,
        (byte) 140,
        (byte) 214,
        (byte) 247,
        (byte) 236,
        (byte) 249,
        (byte) 30,
        (byte) 106,
        (byte) 98,
        (byte) 90,
        (byte) 18,
        (byte) 55,
        (byte) 130,
        (byte) 7,
        (byte) 223,
        (byte) 76,
        (byte) 120,
        (byte) 66,
        (byte) 246,
        (byte) 202,
        (byte) 121,
        (byte) 202,
        (byte) 115,
        (byte) 143,
        (byte) 55,
        (byte) 130,
        (byte) 197,
        (byte) 70,
        (byte) 94,
        (byte) 140,
        (byte) 21,
        (byte) 9
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[36];
    byte[] numArray5 = new byte[36]
    {
      (byte) 89,
      (byte) 112 /*0x70*/,
      (byte) 109,
      (byte) 163,
      (byte) 188,
      byte.MaxValue,
      (byte) 98,
      (byte) 20,
      (byte) 240 /*0xF0*/,
      (byte) 196,
      (byte) 121,
      (byte) 246,
      (byte) 21,
      (byte) 32 /*0x20*/,
      (byte) 94,
      (byte) 206,
      (byte) 6,
      (byte) 115,
      (byte) 252,
      (byte) 105,
      (byte) 212,
      (byte) 174,
      (byte) 240 /*0xF0*/,
      (byte) 145,
      (byte) 176 /*0xB0*/,
      (byte) 18,
      (byte) 153,
      (byte) 253,
      (byte) 154,
      (byte) 160 /*0xA0*/,
      (byte) 51,
      (byte) 232,
      (byte) 12,
      (byte) 102,
      (byte) 165,
      (byte) 162
    };
    byte[] numArray6 = new byte[36];
    numArray6[31 /*0x1F*/] = (byte) 126;
    numArray6[9] = (byte) 85;
    numArray6[2] = (byte) 89;
    numArray6[24] = (byte) 234;
    numArray6[4] = (byte) 230;
    numArray6[22] = (byte) 30;
    numArray6[6] = (byte) 121;
    numArray6[7] = (byte) 234;
    numArray6[28] = (byte) 202;
    numArray6[18] = (byte) 191;
    numArray6[10] = (byte) 228;
    numArray6[23] = (byte) 151;
    numArray6[35] = (byte) 152;
    numArray6[13] = (byte) 12;
    numArray6[14] = (byte) 40;
    numArray6[0] = (byte) 45;
    numArray6[30] = (byte) 133;
    numArray6[17] = (byte) 161;
    numArray6[11] = (byte) 109;
    numArray6[27] = (byte) 176 /*0xB0*/;
    numArray6[20] = (byte) 119;
    numArray6[15] = (byte) 176 /*0xB0*/;
    numArray6[1] = (byte) 198;
    numArray6[16 /*0x10*/] = (byte) 131;
    numArray6[19] = (byte) 159;
    numArray6[25] = (byte) 59;
    numArray6[26] = (byte) 105;
    numArray6[3] = (byte) 240 /*0xF0*/;
    numArray6[8] = (byte) 211;
    numArray6[29] = (byte) 40;
    numArray6[21] = (byte) 110;
    numArray6[5] = (byte) 2;
    numArray6[12] = (byte) 17;
    numArray6[33] = (byte) 116;
    numArray6[34] = (byte) 175;
    numArray6[32 /*0x20*/] = (byte) 152;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[12];
    byte[] response = new byte[12];
    Array.Copy((Array) sc_13393.sspq, 813, (Array) numArray7, 0, 12);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13393.sspr, 813, (Array) numArray7, 0, 12);
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

  internal static string ssp_appserver_13458()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[327];
      byte[] numArray2 = new byte[55]
      {
        (byte) 250,
        (byte) 34,
        (byte) 140,
        (byte) 147,
        (byte) 50,
        (byte) 177,
        (byte) 177,
        (byte) 40,
        (byte) 80 /*0x50*/,
        (byte) 68,
        (byte) 137,
        (byte) 140,
        (byte) 168,
        (byte) 136,
        (byte) 36,
        (byte) 42,
        (byte) 101,
        (byte) 48 /*0x30*/,
        (byte) 30,
        (byte) 32 /*0x20*/,
        (byte) 18,
        (byte) 156,
        (byte) 100,
        (byte) 100,
        (byte) 185,
        (byte) 238,
        (byte) 207,
        (byte) 71,
        (byte) 111,
        (byte) 149,
        (byte) 37,
        (byte) 173,
        (byte) 82,
        (byte) 143,
        (byte) 127 /*0x7F*/,
        (byte) 153,
        (byte) 217,
        (byte) 237,
        (byte) 100,
        (byte) 50,
        (byte) 162,
        (byte) 10,
        (byte) 190,
        (byte) 214,
        (byte) 237,
        (byte) 172,
        (byte) 18,
        (byte) 13,
        (byte) 247,
        (byte) 224 /*0xE0*/,
        (byte) 85,
        (byte) 59,
        (byte) 197,
        (byte) 119,
        (byte) 13
      };
      byte[] numArray3 = new byte[55];
      numArray3[1] = (byte) 155;
      numArray3[33] = (byte) 237;
      numArray3[2] = (byte) 37;
      numArray3[43] = (byte) 203;
      numArray3[6] = (byte) 125;
      numArray3[27] = (byte) 114;
      numArray3[14] = (byte) 67;
      numArray3[7] = (byte) 217;
      numArray3[15] = (byte) 133;
      numArray3[5] = (byte) 86;
      numArray3[20] = (byte) 188;
      numArray3[17] = (byte) 192 /*0xC0*/;
      numArray3[46] = (byte) 65;
      numArray3[13] = (byte) 124;
      numArray3[28] = (byte) 112 /*0x70*/;
      numArray3[11] = (byte) 112 /*0x70*/;
      numArray3[50] = (byte) 60;
      numArray3[0] = (byte) 8;
      numArray3[18] = (byte) 160 /*0xA0*/;
      numArray3[19] = (byte) 121;
      numArray3[36] = (byte) 97;
      numArray3[51] = (byte) 76;
      numArray3[22] = (byte) 92;
      numArray3[23] = (byte) 10;
      numArray3[24] = (byte) 111;
      numArray3[35] = (byte) 160 /*0xA0*/;
      numArray3[26] = (byte) 229;
      numArray3[52] = (byte) 166;
      numArray3[53] = (byte) 191;
      numArray3[25] = (byte) 127 /*0x7F*/;
      numArray3[30] = (byte) 8;
      numArray3[31 /*0x1F*/] = (byte) 70;
      numArray3[32 /*0x20*/] = (byte) 241;
      numArray3[21] = (byte) 2;
      numArray3[34] = (byte) 76;
      numArray3[12] = (byte) 232;
      numArray3[16 /*0x10*/] = (byte) 243;
      numArray3[37] = (byte) 123;
      numArray3[47] = (byte) 235;
      numArray3[39] = (byte) 194;
      numArray3[40] = (byte) 10;
      numArray3[41] = (byte) 234;
      numArray3[42] = (byte) 117;
      numArray3[44] = (byte) 48 /*0x30*/;
      numArray3[4] = (byte) 159;
      numArray3[45] = (byte) 223;
      numArray3[3] = (byte) 245;
      numArray3[10] = (byte) 132;
      numArray3[48 /*0x30*/] = (byte) 0;
      numArray3[54] = (byte) 176 /*0xB0*/;
      numArray3[29] = (byte) 251;
      numArray3[8] = (byte) 151;
      numArray3[38] = (byte) 66;
      numArray3[9] = (byte) 135;
      numArray3[49] = (byte) 192 /*0xC0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 176 /*0xB0*/,
        (byte) 206,
        (byte) 103,
        (byte) 32 /*0x20*/,
        (byte) 25,
        (byte) 42,
        (byte) 123,
        (byte) 53,
        (byte) 244,
        (byte) 95,
        (byte) 151,
        (byte) 232,
        (byte) 176 /*0xB0*/,
        (byte) 158,
        (byte) 101,
        (byte) 171,
        (byte) 155,
        (byte) 69,
        (byte) 182,
        (byte) 218,
        (byte) 216,
        (byte) 101,
        (byte) 153,
        (byte) 225,
        (byte) 189,
        (byte) 228,
        (byte) 254,
        (byte) 98,
        (byte) 121,
        (byte) 13,
        (byte) 225,
        (byte) 123,
        (byte) 160 /*0xA0*/,
        (byte) 172,
        (byte) 2,
        (byte) 68,
        (byte) 184,
        (byte) 47,
        (byte) 31 /*0x1F*/,
        (byte) 39,
        (byte) 135,
        (byte) 128 /*0x80*/,
        (byte) 28,
        (byte) 19,
        (byte) 98,
        (byte) 28,
        (byte) 187,
        (byte) 242,
        (byte) 120,
        (byte) 196,
        (byte) 62,
        (byte) 179,
        (byte) 79,
        (byte) 165,
        (byte) 86
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 160 /*0xA0*/,
        (byte) 138,
        (byte) 249,
        (byte) 250,
        (byte) 87,
        (byte) 43,
        (byte) 136,
        (byte) 246,
        (byte) 216,
        (byte) 163,
        (byte) 169,
        (byte) 237,
        (byte) 124,
        (byte) 218,
        (byte) 114,
        (byte) 162,
        (byte) 34,
        (byte) 137,
        (byte) 150,
        (byte) 85,
        (byte) 87,
        (byte) 189,
        (byte) 246,
        (byte) 80 /*0x50*/,
        (byte) 113,
        (byte) 138,
        (byte) 54,
        (byte) 108,
        (byte) 223,
        (byte) 169,
        (byte) 48 /*0x30*/,
        (byte) 177,
        (byte) 25,
        (byte) 63 /*0x3F*/,
        (byte) 11,
        (byte) 120,
        (byte) 248,
        (byte) 210,
        (byte) 130,
        (byte) 99,
        (byte) 205,
        (byte) 81,
        (byte) 45,
        (byte) 138,
        (byte) 105,
        (byte) 172,
        (byte) 13,
        (byte) 47,
        (byte) 180,
        (byte) 124,
        (byte) 30,
        (byte) 91,
        (byte) 120,
        (byte) 12,
        (byte) 253
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 134,
        (byte) 99,
        (byte) 52,
        (byte) 44,
        (byte) 24,
        (byte) 188,
        (byte) 231,
        (byte) 125,
        (byte) 27,
        (byte) 71,
        (byte) 4,
        (byte) 156,
        (byte) 32 /*0x20*/,
        (byte) 242,
        (byte) 107,
        (byte) 38,
        (byte) 142,
        (byte) 82,
        (byte) 87,
        (byte) 73,
        (byte) 134,
        (byte) 127 /*0x7F*/,
        (byte) 161,
        (byte) 40,
        (byte) 11,
        (byte) 234,
        (byte) 180,
        (byte) 206,
        (byte) 46,
        (byte) 44,
        (byte) 251,
        (byte) 85,
        (byte) 183,
        (byte) 16 /*0x10*/,
        (byte) 50,
        (byte) 203,
        (byte) 234,
        (byte) 250,
        (byte) 111,
        (byte) 196,
        (byte) 53,
        (byte) 149,
        (byte) 70,
        (byte) 212,
        (byte) 183,
        (byte) 86,
        (byte) 88,
        (byte) 210,
        (byte) 210,
        (byte) 141,
        (byte) 192 /*0xC0*/,
        (byte) 82,
        byte.MaxValue,
        (byte) 1,
        (byte) 173
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 211,
        (byte) 20,
        (byte) 241,
        (byte) 106,
        (byte) 133,
        (byte) 118,
        (byte) 46,
        (byte) 157,
        (byte) 96 /*0x60*/,
        (byte) 179,
        (byte) 65,
        (byte) 127 /*0x7F*/,
        (byte) 78,
        (byte) 77,
        (byte) 145,
        (byte) 171,
        (byte) 215,
        (byte) 184,
        (byte) 0,
        (byte) 182,
        (byte) 152,
        (byte) 176 /*0xB0*/,
        (byte) 251,
        (byte) 204,
        (byte) 245,
        (byte) 138,
        (byte) 220,
        (byte) 76,
        (byte) 213,
        (byte) 247,
        (byte) 113,
        (byte) 186,
        (byte) 183,
        (byte) 66,
        (byte) 237,
        (byte) 181,
        (byte) 204,
        (byte) 12,
        (byte) 76,
        (byte) 209,
        (byte) 106,
        (byte) 56,
        (byte) 183,
        (byte) 150,
        (byte) 185,
        (byte) 209,
        (byte) 200,
        (byte) 237,
        (byte) 47,
        (byte) 23,
        (byte) 251,
        (byte) 80 /*0x50*/,
        (byte) 154,
        (byte) 113,
        (byte) 33
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 11,
        (byte) 32 /*0x20*/,
        (byte) 45,
        (byte) 7,
        (byte) 24,
        (byte) 79,
        (byte) 239,
        (byte) 76,
        (byte) 174,
        (byte) 228,
        (byte) 15,
        (byte) 191,
        (byte) 159,
        (byte) 59,
        (byte) 148,
        (byte) 227,
        (byte) 147,
        (byte) 19,
        (byte) 240 /*0xF0*/,
        (byte) 56,
        (byte) 204,
        (byte) 113,
        (byte) 181,
        (byte) 146,
        (byte) 226,
        (byte) 252,
        (byte) 160 /*0xA0*/,
        (byte) 107,
        (byte) 168,
        (byte) 86,
        (byte) 182,
        (byte) 98,
        (byte) 44,
        (byte) 9,
        (byte) 222,
        (byte) 217,
        (byte) 65,
        (byte) 189,
        (byte) 28,
        (byte) 70,
        (byte) 87,
        (byte) 136,
        (byte) 8,
        (byte) 113,
        (byte) 72,
        (byte) 18,
        (byte) 50,
        (byte) 82,
        (byte) 44,
        (byte) 111,
        (byte) 108,
        (byte) 237,
        (byte) 188,
        (byte) 205,
        (byte) 245
      };
      byte[] numArray9 = new byte[55];
      numArray9[49] = (byte) 73;
      numArray9[40] = (byte) 215;
      numArray9[34] = (byte) 227;
      numArray9[37] = (byte) 241;
      numArray9[4] = (byte) 181;
      numArray9[5] = (byte) 126;
      numArray9[6] = (byte) 254;
      numArray9[3] = (byte) 247;
      numArray9[8] = (byte) 175;
      numArray9[9] = (byte) 251;
      numArray9[10] = (byte) 78;
      numArray9[11] = (byte) 62;
      numArray9[35] = (byte) 180;
      numArray9[2] = (byte) 98;
      numArray9[23] = (byte) 62;
      numArray9[15] = (byte) 206;
      numArray9[16 /*0x10*/] = (byte) 142;
      numArray9[17] = (byte) 156;
      numArray9[18] = (byte) 111;
      numArray9[47] = (byte) 222;
      numArray9[20] = (byte) 192 /*0xC0*/;
      numArray9[21] = (byte) 56;
      numArray9[22] = (byte) 142;
      numArray9[13] = (byte) 237;
      numArray9[27] = (byte) 174;
      numArray9[28] = (byte) 214;
      numArray9[46] = (byte) 132;
      numArray9[45] = (byte) 165;
      numArray9[33] = (byte) 33;
      numArray9[29] = (byte) 36;
      numArray9[30] = (byte) 201;
      numArray9[19] = (byte) 24;
      numArray9[36] = (byte) 192 /*0xC0*/;
      numArray9[12] = (byte) 243;
      numArray9[48 /*0x30*/] = (byte) 173;
      numArray9[39] = (byte) 210;
      numArray9[24] = (byte) 61;
      numArray9[1] = (byte) 253;
      numArray9[38] = (byte) 66;
      numArray9[0] = (byte) 23;
      numArray9[51] = (byte) 130;
      numArray9[41] = (byte) 12;
      numArray9[54] = (byte) 50;
      numArray9[25] = (byte) 188;
      numArray9[44] = (byte) 24;
      numArray9[53] = (byte) 234;
      numArray9[26] = (byte) 75;
      numArray9[43] = (byte) 109;
      numArray9[42] = (byte) 73;
      numArray9[14] = (byte) 155;
      numArray9[50] = (byte) 42;
      numArray9[31 /*0x1F*/] = (byte) 88;
      numArray9[52] = (byte) 246;
      numArray9[7] = (byte) 190;
      numArray9[32 /*0x20*/] = (byte) 79;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 81,
        (byte) 84,
        (byte) 82,
        (byte) 96 /*0x60*/,
        (byte) 68,
        (byte) 106,
        (byte) 109,
        (byte) 99,
        (byte) 7,
        (byte) 53,
        (byte) 129,
        (byte) 23,
        (byte) 75,
        (byte) 250,
        (byte) 62,
        (byte) 246,
        (byte) 19,
        (byte) 13,
        (byte) 96 /*0x60*/,
        (byte) 184,
        byte.MaxValue,
        (byte) 174,
        (byte) 81,
        (byte) 45,
        (byte) 89,
        (byte) 205,
        (byte) 107,
        (byte) 84,
        (byte) 211,
        (byte) 147,
        (byte) 114,
        (byte) 99,
        (byte) 19,
        (byte) 72,
        (byte) 85,
        (byte) 67,
        (byte) 174,
        (byte) 54,
        (byte) 143,
        (byte) 121,
        (byte) 66,
        (byte) 224 /*0xE0*/,
        (byte) 148,
        (byte) 73,
        (byte) 26,
        (byte) 221,
        (byte) 38,
        (byte) 210,
        (byte) 87,
        byte.MaxValue,
        (byte) 254,
        (byte) 222,
        (byte) 80 /*0x50*/,
        (byte) 121,
        (byte) 39
      };
      byte[] numArray11 = new byte[55];
      numArray11[33] = (byte) 155;
      numArray11[32 /*0x20*/] = (byte) 29;
      numArray11[10] = (byte) 113;
      numArray11[12] = (byte) 93;
      numArray11[4] = (byte) 102;
      numArray11[5] = (byte) 14;
      numArray11[24] = (byte) 209;
      numArray11[43] = (byte) 134;
      numArray11[48 /*0x30*/] = (byte) 98;
      numArray11[53] = (byte) 81;
      numArray11[7] = (byte) 56;
      numArray11[46] = (byte) 21;
      numArray11[31 /*0x1F*/] = (byte) 42;
      numArray11[13] = (byte) 2;
      numArray11[14] = (byte) 166;
      numArray11[15] = (byte) 81;
      numArray11[16 /*0x10*/] = (byte) 173;
      numArray11[17] = (byte) 151;
      numArray11[27] = (byte) 174;
      numArray11[37] = (byte) 186;
      numArray11[21] = (byte) 68;
      numArray11[19] = (byte) 148;
      numArray11[22] = (byte) 52;
      numArray11[11] = (byte) 28;
      numArray11[34] = (byte) 190;
      numArray11[0] = (byte) 225;
      numArray11[40] = (byte) 141;
      numArray11[18] = (byte) 179;
      numArray11[28] = (byte) 54;
      numArray11[29] = (byte) 167;
      numArray11[30] = (byte) 132;
      numArray11[39] = (byte) 55;
      numArray11[51] = (byte) 37;
      numArray11[3] = (byte) 204;
      numArray11[2] = (byte) 85;
      numArray11[35] = (byte) 190;
      numArray11[36] = (byte) 51;
      numArray11[6] = (byte) 113;
      numArray11[38] = (byte) 186;
      numArray11[26] = (byte) 173;
      numArray11[1] = (byte) 42;
      numArray11[41] = (byte) 183;
      numArray11[42] = (byte) 218;
      numArray11[23] = (byte) 17;
      numArray11[44] = (byte) 131;
      numArray11[45] = (byte) 120;
      numArray11[20] = (byte) 31 /*0x1F*/;
      numArray11[47] = (byte) 147;
      numArray11[9] = (byte) 78;
      numArray11[49] = (byte) 94;
      numArray11[50] = (byte) 39;
      numArray11[25] = (byte) 218;
      numArray11[52] = (byte) 186;
      numArray11[8] = (byte) 78;
      numArray11[54] = (byte) 188;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[52];
      numArray12[43] = (byte) 111;
      numArray12[27] = (byte) 116;
      numArray12[2] = (byte) 207;
      numArray12[12] = (byte) 187;
      numArray12[14] = (byte) 174;
      numArray12[35] = (byte) 163;
      numArray12[6] = (byte) 222;
      numArray12[20] = (byte) 128 /*0x80*/;
      numArray12[16 /*0x10*/] = (byte) 109;
      numArray12[33] = (byte) 66;
      numArray12[40] = (byte) 66;
      numArray12[9] = (byte) 105;
      numArray12[18] = (byte) 206;
      numArray12[3] = (byte) 102;
      numArray12[38] = (byte) 13;
      numArray12[15] = (byte) 102;
      numArray12[1] = (byte) 139;
      numArray12[17] = (byte) 75;
      numArray12[37] = (byte) 19;
      numArray12[19] = (byte) 248;
      numArray12[41] = (byte) 87;
      numArray12[21] = (byte) 23;
      numArray12[22] = (byte) 162;
      numArray12[23] = (byte) 132;
      numArray12[50] = (byte) 251;
      numArray12[25] = (byte) 159;
      numArray12[11] = (byte) 243;
      numArray12[49] = (byte) 249;
      numArray12[39] = (byte) 148;
      numArray12[29] = (byte) 170;
      numArray12[30] = (byte) 150;
      numArray12[10] = (byte) 226;
      numArray12[32 /*0x20*/] = (byte) 40;
      numArray12[24] = (byte) 107;
      numArray12[34] = (byte) 120;
      numArray12[5] = (byte) 188;
      numArray12[36] = (byte) 72;
      numArray12[28] = (byte) 52;
      numArray12[7] = (byte) 47;
      numArray12[31 /*0x1F*/] = (byte) 23;
      numArray12[26] = (byte) 27;
      numArray12[13] = (byte) 189;
      numArray12[42] = (byte) 240 /*0xF0*/;
      numArray12[48 /*0x30*/] = (byte) 114;
      numArray12[44] = (byte) 64 /*0x40*/;
      numArray12[0] = (byte) 17;
      numArray12[46] = (byte) 161;
      numArray12[47] = (byte) 251;
      numArray12[4] = (byte) 28;
      numArray12[45] = (byte) 58;
      numArray12[8] = (byte) 11;
      numArray12[51] = (byte) 184;
      byte[] numArray13 = new byte[52];
      numArray13[47] = (byte) 245;
      numArray13[1] = (byte) 5;
      numArray13[11] = (byte) 174;
      numArray13[39] = (byte) 1;
      numArray13[31 /*0x1F*/] = (byte) 233;
      numArray13[5] = (byte) 133;
      numArray13[41] = (byte) 22;
      numArray13[28] = (byte) 31 /*0x1F*/;
      numArray13[51] = (byte) 91;
      numArray13[9] = (byte) 187;
      numArray13[32 /*0x20*/] = (byte) 87;
      numArray13[7] = (byte) 120;
      numArray13[12] = (byte) 243;
      numArray13[13] = (byte) 161;
      numArray13[22] = (byte) 36;
      numArray13[37] = (byte) 253;
      numArray13[50] = (byte) 248;
      numArray13[17] = (byte) 142;
      numArray13[25] = (byte) 13;
      numArray13[36] = (byte) 63 /*0x3F*/;
      numArray13[6] = (byte) 168;
      numArray13[18] = (byte) 166;
      numArray13[10] = byte.MaxValue;
      numArray13[23] = (byte) 107;
      numArray13[24] = (byte) 117;
      numArray13[43] = (byte) 240 /*0xF0*/;
      numArray13[26] = (byte) 0;
      numArray13[27] = (byte) 32 /*0x20*/;
      numArray13[30] = (byte) 161;
      numArray13[19] = (byte) 14;
      numArray13[16 /*0x10*/] = (byte) 121;
      numArray13[20] = (byte) 219;
      numArray13[3] = (byte) 202;
      numArray13[33] = (byte) 197;
      numArray13[34] = (byte) 244;
      numArray13[35] = (byte) 144 /*0x90*/;
      numArray13[0] = (byte) 174;
      numArray13[21] = (byte) 215;
      numArray13[38] = (byte) 95;
      numArray13[40] = (byte) 125;
      numArray13[15] = (byte) 185;
      numArray13[2] = (byte) 216;
      numArray13[42] = (byte) 212;
      numArray13[4] = (byte) 235;
      numArray13[44] = (byte) 210;
      numArray13[29] = (byte) 168;
      numArray13[46] = (byte) 200;
      numArray13[14] = (byte) 114;
      numArray13[48 /*0x30*/] = (byte) 103;
      numArray13[49] = (byte) 142;
      numArray13[45] = (byte) 117;
      numArray13[8] = (byte) 60;
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index + 275] ^= numArray13[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray14 = new byte[327];
    byte[] numArray15 = new byte[55];
    numArray15[2] = (byte) 203;
    numArray15[1] = (byte) 97;
    numArray15[22] = (byte) 238;
    numArray15[3] = (byte) 186;
    numArray15[4] = (byte) 254;
    numArray15[38] = (byte) 190;
    numArray15[46] = (byte) 167;
    numArray15[7] = (byte) 31 /*0x1F*/;
    numArray15[54] = (byte) 198;
    numArray15[11] = (byte) 22;
    numArray15[17] = (byte) 117;
    numArray15[36] = (byte) 153;
    numArray15[45] = (byte) 91;
    numArray15[13] = (byte) 97;
    numArray15[14] = (byte) 163;
    numArray15[50] = (byte) 118;
    numArray15[16 /*0x10*/] = (byte) 17;
    numArray15[18] = (byte) 61;
    numArray15[8] = (byte) 55;
    numArray15[19] = (byte) 27;
    numArray15[29] = (byte) 70;
    numArray15[52] = (byte) 56;
    numArray15[15] = (byte) 143;
    numArray15[41] = (byte) 227;
    numArray15[6] = (byte) 59;
    numArray15[25] = (byte) 105;
    numArray15[26] = (byte) 47;
    numArray15[0] = (byte) 188;
    numArray15[5] = (byte) 134;
    numArray15[9] = (byte) 6;
    numArray15[30] = (byte) 131;
    numArray15[12] = (byte) 203;
    numArray15[35] = (byte) 171;
    numArray15[21] = (byte) 39;
    numArray15[51] = (byte) 200;
    numArray15[10] = (byte) 168;
    numArray15[28] = (byte) 239;
    numArray15[37] = (byte) 33;
    numArray15[34] = (byte) 161;
    numArray15[39] = (byte) 237;
    numArray15[40] = (byte) 111;
    numArray15[27] = (byte) 185;
    numArray15[42] = (byte) 191;
    numArray15[32 /*0x20*/] = (byte) 122;
    numArray15[24] = (byte) 212;
    numArray15[33] = (byte) 134;
    numArray15[47] = (byte) 58;
    numArray15[20] = (byte) 78;
    numArray15[48 /*0x30*/] = (byte) 58;
    numArray15[49] = (byte) 24;
    numArray15[43] = (byte) 74;
    numArray15[23] = (byte) 156;
    numArray15[44] = (byte) 105;
    numArray15[53] = (byte) 211;
    numArray15[31 /*0x1F*/] = (byte) 95;
    byte[] numArray16 = new byte[55];
    numArray16[12] = (byte) 158;
    numArray16[13] = (byte) 205;
    numArray16[46] = (byte) 168;
    numArray16[3] = (byte) 68;
    numArray16[4] = (byte) 168;
    numArray16[39] = (byte) 46;
    numArray16[0] = (byte) 213;
    numArray16[7] = (byte) 5;
    numArray16[38] = (byte) 66;
    numArray16[9] = (byte) 96 /*0x60*/;
    numArray16[10] = (byte) 215;
    numArray16[18] = (byte) 127 /*0x7F*/;
    numArray16[27] = (byte) 134;
    numArray16[37] = (byte) 164;
    numArray16[23] = (byte) 244;
    numArray16[22] = (byte) 190;
    numArray16[16 /*0x10*/] = (byte) 93;
    numArray16[15] = (byte) 123;
    numArray16[24] = (byte) 196;
    numArray16[2] = (byte) 178;
    numArray16[20] = (byte) 91;
    numArray16[21] = (byte) 32 /*0x20*/;
    numArray16[26] = (byte) 34;
    numArray16[5] = (byte) 16 /*0x10*/;
    numArray16[11] = (byte) 251;
    numArray16[33] = (byte) 124;
    numArray16[6] = (byte) 216;
    numArray16[42] = (byte) 113;
    numArray16[49] = (byte) 56;
    numArray16[29] = (byte) 17;
    numArray16[54] = (byte) 249;
    numArray16[32 /*0x20*/] = (byte) 152;
    numArray16[8] = (byte) 153;
    numArray16[31 /*0x1F*/] = (byte) 101;
    numArray16[34] = (byte) 196;
    numArray16[35] = (byte) 229;
    numArray16[36] = (byte) 251;
    numArray16[43] = (byte) 251;
    numArray16[1] = (byte) 249;
    numArray16[19] = (byte) 85;
    numArray16[40] = byte.MaxValue;
    numArray16[28] = (byte) 222;
    numArray16[30] = (byte) 165;
    numArray16[25] = (byte) 165;
    numArray16[44] = (byte) 155;
    numArray16[14] = (byte) 115;
    numArray16[45] = (byte) 74;
    numArray16[47] = (byte) 244;
    numArray16[48 /*0x30*/] = (byte) 91;
    numArray16[51] = (byte) 131;
    numArray16[50] = (byte) 59;
    numArray16[17] = (byte) 163;
    numArray16[52] = (byte) 183;
    numArray16[53] = (byte) 23;
    numArray16[41] = (byte) 142;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray14, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 231,
      (byte) 179,
      (byte) 113,
      (byte) 232,
      (byte) 198,
      (byte) 166,
      (byte) 217,
      (byte) 31 /*0x1F*/,
      (byte) 227,
      (byte) 116,
      (byte) 33,
      (byte) 209,
      (byte) 87,
      (byte) 26,
      (byte) 230,
      (byte) 54,
      (byte) 133,
      (byte) 214,
      (byte) 95,
      (byte) 238,
      (byte) 219,
      (byte) 20,
      (byte) 239,
      (byte) 133,
      (byte) 177,
      (byte) 186,
      (byte) 26,
      (byte) 117,
      (byte) 36,
      (byte) 15,
      (byte) 25,
      (byte) 193,
      (byte) 79,
      (byte) 116,
      (byte) 136,
      (byte) 25,
      (byte) 229,
      (byte) 75,
      (byte) 34,
      (byte) 178,
      (byte) 210,
      (byte) 224 /*0xE0*/,
      (byte) 27,
      (byte) 85,
      (byte) 185,
      (byte) 246,
      (byte) 43,
      (byte) 126,
      (byte) 236,
      (byte) 77,
      (byte) 169,
      (byte) 144 /*0x90*/,
      (byte) 66,
      (byte) 163,
      (byte) 24
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 222,
      (byte) 160 /*0xA0*/,
      (byte) 22,
      (byte) 53,
      (byte) 241,
      (byte) 218,
      (byte) 21,
      (byte) 134,
      (byte) 124,
      (byte) 100,
      (byte) 223,
      (byte) 180,
      (byte) 220,
      (byte) 228,
      (byte) 233,
      (byte) 105,
      (byte) 243,
      (byte) 4,
      (byte) 140,
      (byte) 213,
      (byte) 51,
      (byte) 39,
      (byte) 134,
      (byte) 149,
      (byte) 166,
      (byte) 57,
      (byte) 7,
      (byte) 249,
      (byte) 247,
      (byte) 219,
      (byte) 104,
      (byte) 240 /*0xF0*/,
      (byte) 197,
      (byte) 233,
      (byte) 31 /*0x1F*/,
      (byte) 210,
      (byte) 171,
      (byte) 58,
      (byte) 118,
      (byte) 85,
      (byte) 30,
      (byte) 126,
      (byte) 14,
      (byte) 174,
      (byte) 69,
      (byte) 188,
      (byte) 34,
      (byte) 106,
      (byte) 56,
      (byte) 182,
      (byte) 83,
      (byte) 142,
      (byte) 32 /*0x20*/,
      (byte) 122,
      (byte) 68
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray14, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 55] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 236,
      (byte) 55,
      (byte) 12,
      (byte) 140,
      (byte) 99,
      (byte) 251,
      (byte) 211,
      (byte) 91,
      (byte) 96 /*0x60*/,
      (byte) 130,
      (byte) 226,
      (byte) 146,
      (byte) 204,
      (byte) 81,
      (byte) 57,
      (byte) 179,
      (byte) 173,
      (byte) 27,
      (byte) 251,
      (byte) 249,
      (byte) 153,
      (byte) 203,
      (byte) 186,
      (byte) 142,
      (byte) 155,
      (byte) 137,
      (byte) 214,
      (byte) 102,
      (byte) 136,
      (byte) 126,
      (byte) 172,
      (byte) 91,
      (byte) 181,
      (byte) 85,
      (byte) 249,
      (byte) 19,
      (byte) 22,
      (byte) 37,
      (byte) 110,
      (byte) 228,
      (byte) 71,
      (byte) 250,
      (byte) 89,
      (byte) 115,
      (byte) 72,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 252,
      (byte) 111,
      (byte) 221,
      (byte) 149,
      (byte) 135,
      (byte) 28,
      (byte) 217,
      (byte) 42
    };
    byte[] numArray20 = new byte[55];
    numArray20[32 /*0x20*/] = (byte) 144 /*0x90*/;
    numArray20[1] = (byte) 7;
    numArray20[54] = (byte) 108;
    numArray20[3] = (byte) 160 /*0xA0*/;
    numArray20[4] = (byte) 115;
    numArray20[18] = (byte) 201;
    numArray20[6] = (byte) 49;
    numArray20[7] = (byte) 190;
    numArray20[8] = (byte) 122;
    numArray20[9] = (byte) 34;
    numArray20[15] = (byte) 95;
    numArray20[11] = (byte) 92;
    numArray20[24] = (byte) 116;
    numArray20[13] = (byte) 116;
    numArray20[20] = (byte) 49;
    numArray20[30] = (byte) 46;
    numArray20[16 /*0x10*/] = (byte) 135;
    numArray20[17] = (byte) 62;
    numArray20[41] = (byte) 43;
    numArray20[19] = (byte) 156;
    numArray20[14] = (byte) 111;
    numArray20[21] = (byte) 223;
    numArray20[46] = (byte) 181;
    numArray20[23] = (byte) 17;
    numArray20[37] = (byte) 91;
    numArray20[25] = (byte) 180;
    numArray20[26] = (byte) 44;
    numArray20[27] = (byte) 191;
    numArray20[28] = (byte) 137;
    numArray20[29] = (byte) 97;
    numArray20[22] = (byte) 5;
    numArray20[2] = (byte) 97;
    numArray20[50] = (byte) 23;
    numArray20[33] = (byte) 254;
    numArray20[34] = (byte) 75;
    numArray20[35] = (byte) 16 /*0x10*/;
    numArray20[36] = (byte) 68;
    numArray20[49] = (byte) 175;
    numArray20[53] = (byte) 44;
    numArray20[42] = (byte) 162;
    numArray20[40] = (byte) 216;
    numArray20[0] = (byte) 23;
    numArray20[52] = (byte) 38;
    numArray20[43] = (byte) 137;
    numArray20[44] = (byte) 155;
    numArray20[10] = (byte) 48 /*0x30*/;
    numArray20[31 /*0x1F*/] = (byte) 115;
    numArray20[12] = (byte) 248;
    numArray20[5] = (byte) 211;
    numArray20[39] = (byte) 78;
    numArray20[45] = (byte) 27;
    numArray20[48 /*0x30*/] = (byte) 56;
    numArray20[51] = (byte) 190;
    numArray20[38] = (byte) 199;
    numArray20[47] = (byte) 196;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray14, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 110] ^= numArray20[index];
    byte[] numArray21 = new byte[55]
    {
      (byte) 55,
      (byte) 65,
      (byte) 112 /*0x70*/,
      (byte) 74,
      (byte) 117,
      (byte) 40,
      (byte) 247,
      (byte) 177,
      (byte) 72,
      (byte) 1,
      (byte) 76,
      (byte) 127 /*0x7F*/,
      (byte) 142,
      (byte) 4,
      (byte) 82,
      (byte) 24,
      (byte) 86,
      (byte) 215,
      (byte) 119,
      (byte) 249,
      (byte) 169,
      (byte) 132,
      (byte) 159,
      (byte) 193,
      (byte) 54,
      (byte) 173,
      (byte) 106,
      (byte) 220,
      (byte) 73,
      (byte) 56,
      (byte) 180,
      (byte) 159,
      (byte) 42,
      (byte) 97,
      (byte) 96 /*0x60*/,
      (byte) 243,
      (byte) 35,
      (byte) 117,
      (byte) 100,
      byte.MaxValue,
      (byte) 128 /*0x80*/,
      (byte) 235,
      (byte) 121,
      (byte) 14,
      (byte) 92,
      (byte) 36,
      (byte) 186,
      (byte) 60,
      (byte) 186,
      (byte) 194,
      (byte) 175,
      (byte) 80 /*0x50*/,
      (byte) 20,
      (byte) 180,
      (byte) 140
    };
    byte[] numArray22 = new byte[55]
    {
      (byte) 229,
      (byte) 99,
      (byte) 2,
      (byte) 185,
      (byte) 115,
      (byte) 199,
      (byte) 18,
      (byte) 0,
      (byte) 66,
      (byte) 22,
      (byte) 162,
      (byte) 47,
      (byte) 110,
      (byte) 141,
      (byte) 231,
      (byte) 21,
      (byte) 160 /*0xA0*/,
      (byte) 172,
      (byte) 220,
      (byte) 225,
      (byte) 57,
      (byte) 44,
      (byte) 112 /*0x70*/,
      (byte) 94,
      (byte) 97,
      (byte) 25,
      (byte) 181,
      (byte) 52,
      (byte) 16 /*0x10*/,
      (byte) 115,
      (byte) 44,
      (byte) 222,
      (byte) 144 /*0x90*/,
      (byte) 13,
      (byte) 1,
      (byte) 84,
      (byte) 27,
      (byte) 15,
      (byte) 215,
      (byte) 111,
      (byte) 95,
      (byte) 56,
      (byte) 157,
      (byte) 3,
      (byte) 3,
      (byte) 139,
      (byte) 135,
      (byte) 132,
      (byte) 159,
      (byte) 55,
      (byte) 124,
      (byte) 160 /*0xA0*/,
      (byte) 137,
      (byte) 233,
      (byte) 206
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray14, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 165] ^= numArray22[index];
    byte[] numArray23 = new byte[55];
    numArray23[6] = (byte) 15;
    numArray23[0] = (byte) 175;
    numArray23[42] = (byte) 175;
    numArray23[3] = (byte) 189;
    numArray23[1] = (byte) 20;
    numArray23[5] = (byte) 103;
    numArray23[12] = (byte) 94;
    numArray23[7] = (byte) 77;
    numArray23[33] = (byte) 81;
    numArray23[22] = (byte) 18;
    numArray23[40] = (byte) 105;
    numArray23[11] = (byte) 163;
    numArray23[49] = (byte) 222;
    numArray23[53] = (byte) 167;
    numArray23[51] = (byte) 59;
    numArray23[47] = (byte) 205;
    numArray23[15] = (byte) 114;
    numArray23[25] = (byte) 33;
    numArray23[2] = (byte) 200;
    numArray23[9] = (byte) 84;
    numArray23[10] = (byte) 45;
    numArray23[21] = (byte) 100;
    numArray23[14] = (byte) 225;
    numArray23[32 /*0x20*/] = (byte) 173;
    numArray23[24] = (byte) 46;
    numArray23[35] = (byte) 97;
    numArray23[17] = (byte) 121;
    numArray23[27] = (byte) 137;
    numArray23[16 /*0x10*/] = (byte) 119;
    numArray23[19] = (byte) 11;
    numArray23[30] = (byte) 117;
    numArray23[50] = (byte) 156;
    numArray23[4] = (byte) 27;
    numArray23[28] = (byte) 99;
    numArray23[34] = (byte) 115;
    numArray23[48 /*0x30*/] = (byte) 206;
    numArray23[41] = (byte) 193;
    numArray23[26] = (byte) 135;
    numArray23[38] = (byte) 101;
    numArray23[39] = (byte) 132;
    numArray23[44] = (byte) 141;
    numArray23[18] = (byte) 189;
    numArray23[29] = (byte) 139;
    numArray23[43] = (byte) 207;
    numArray23[20] = (byte) 144 /*0x90*/;
    numArray23[45] = (byte) 152;
    numArray23[46] = (byte) 91;
    numArray23[8] = (byte) 172;
    numArray23[13] = (byte) 245;
    numArray23[54] = (byte) 17;
    numArray23[36] = (byte) 156;
    numArray23[31 /*0x1F*/] = (byte) 213;
    numArray23[52] = (byte) 206;
    numArray23[23] = (byte) 243;
    numArray23[37] = (byte) 65;
    byte[] numArray24 = new byte[55]
    {
      (byte) 152,
      (byte) 8,
      (byte) 88,
      (byte) 233,
      (byte) 63 /*0x3F*/,
      (byte) 61,
      (byte) 171,
      (byte) 127 /*0x7F*/,
      (byte) 133,
      (byte) 216,
      (byte) 21,
      (byte) 41,
      (byte) 132,
      (byte) 194,
      (byte) 53,
      (byte) 159,
      (byte) 101,
      (byte) 134,
      (byte) 34,
      (byte) 87,
      (byte) 125,
      (byte) 225,
      (byte) 89,
      (byte) 144 /*0x90*/,
      (byte) 217,
      (byte) 23,
      (byte) 191,
      (byte) 134,
      (byte) 3,
      (byte) 67,
      (byte) 101,
      (byte) 69,
      (byte) 235,
      (byte) 19,
      (byte) 168,
      (byte) 239,
      (byte) 198,
      (byte) 71,
      (byte) 51,
      (byte) 228,
      (byte) 139,
      (byte) 60,
      (byte) 237,
      (byte) 154,
      (byte) 52,
      (byte) 34,
      (byte) 6,
      (byte) 68,
      (byte) 220,
      (byte) 198,
      (byte) 131,
      (byte) 150,
      (byte) 186,
      (byte) 179,
      (byte) 29
    };
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray14, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 220] ^= numArray24[index];
    byte[] numArray25 = new byte[52]
    {
      (byte) 111,
      (byte) 249,
      (byte) 51,
      (byte) 37,
      (byte) 203,
      (byte) 100,
      (byte) 50,
      (byte) 165,
      (byte) 80 /*0x50*/,
      (byte) 240 /*0xF0*/,
      (byte) 205,
      (byte) 88,
      (byte) 32 /*0x20*/,
      (byte) 13,
      (byte) 132,
      (byte) 166,
      (byte) 149,
      (byte) 190,
      (byte) 247,
      (byte) 39,
      (byte) 82,
      (byte) 48 /*0x30*/,
      (byte) 243,
      (byte) 119,
      (byte) 110,
      (byte) 109,
      (byte) 123,
      (byte) 136,
      (byte) 17,
      (byte) 115,
      (byte) 0,
      (byte) 253,
      (byte) 147,
      (byte) 159,
      (byte) 88,
      (byte) 184,
      (byte) 57,
      (byte) 126,
      (byte) 24,
      (byte) 115,
      (byte) 91,
      (byte) 75,
      (byte) 228,
      (byte) 177,
      (byte) 128 /*0x80*/,
      (byte) 242,
      (byte) 45,
      (byte) 251,
      (byte) 226,
      (byte) 129,
      (byte) 254,
      (byte) 70
    };
    byte[] numArray26 = new byte[52];
    numArray26[4] = (byte) 78;
    numArray26[19] = (byte) 241;
    numArray26[43] = (byte) 60;
    numArray26[34] = (byte) 243;
    numArray26[7] = (byte) 154;
    numArray26[5] = (byte) 162;
    numArray26[51] = (byte) 166;
    numArray26[47] = (byte) 136;
    numArray26[8] = (byte) 63 /*0x3F*/;
    numArray26[6] = (byte) 25;
    numArray26[31 /*0x1F*/] = (byte) 20;
    numArray26[20] = (byte) 241;
    numArray26[30] = (byte) 51;
    numArray26[13] = (byte) 220;
    numArray26[14] = (byte) 155;
    numArray26[2] = (byte) 236;
    numArray26[16 /*0x10*/] = (byte) 118;
    numArray26[17] = (byte) 42;
    numArray26[18] = (byte) 238;
    numArray26[38] = (byte) 175;
    numArray26[1] = (byte) 142;
    numArray26[22] = (byte) 156;
    numArray26[49] = (byte) 77;
    numArray26[25] = (byte) 68;
    numArray26[24] = (byte) 75;
    numArray26[26] = (byte) 226;
    numArray26[15] = (byte) 19;
    numArray26[27] = (byte) 0;
    numArray26[28] = (byte) 189;
    numArray26[29] = (byte) 249;
    numArray26[3] = (byte) 241;
    numArray26[37] = (byte) 116;
    numArray26[42] = (byte) 70;
    numArray26[33] = (byte) 191;
    numArray26[21] = (byte) 233;
    numArray26[35] = (byte) 75;
    numArray26[36] = (byte) 83;
    numArray26[39] = (byte) 31 /*0x1F*/;
    numArray26[0] = (byte) 176 /*0xB0*/;
    numArray26[23] = (byte) 30;
    numArray26[40] = (byte) 234;
    numArray26[41] = (byte) 197;
    numArray26[10] = (byte) 224 /*0xE0*/;
    numArray26[32 /*0x20*/] = (byte) 248;
    numArray26[44] = (byte) 203;
    numArray26[45] = (byte) 234;
    numArray26[46] = (byte) 144 /*0x90*/;
    numArray26[9] = (byte) 240 /*0xF0*/;
    numArray26[48 /*0x30*/] = (byte) 52;
    numArray26[11] = (byte) 136;
    numArray26[50] = (byte) 161;
    numArray26[12] = (byte) 122;
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray14, 275, 52);
    for (int index = 0; index < 52; ++index)
      numArray14[index + 275] ^= numArray26[index];
    return Encoding.UTF8.GetString(numArray14);
  }

  internal static string ssp_appserver_13459()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[327];
      byte[] numArray2 = new byte[55]
      {
        (byte) 161,
        (byte) 102,
        (byte) 243,
        (byte) 196,
        (byte) 196,
        (byte) 59,
        (byte) 29,
        (byte) 104,
        (byte) 3,
        (byte) 3,
        (byte) 104,
        (byte) 55,
        (byte) 30,
        (byte) 159,
        (byte) 78,
        (byte) 155,
        (byte) 25,
        (byte) 240 /*0xF0*/,
        (byte) 80 /*0x50*/,
        (byte) 236,
        (byte) 248,
        (byte) 27,
        (byte) 79,
        (byte) 68,
        (byte) 128 /*0x80*/,
        (byte) 71,
        (byte) 78,
        (byte) 148,
        (byte) 57,
        (byte) 144 /*0x90*/,
        (byte) 124,
        (byte) 141,
        (byte) 25,
        (byte) 234,
        (byte) 132,
        (byte) 204,
        (byte) 38,
        (byte) 237,
        (byte) 184,
        (byte) 140,
        (byte) 156,
        (byte) 34,
        (byte) 173,
        (byte) 180,
        (byte) 158,
        (byte) 138,
        (byte) 145,
        (byte) 142,
        (byte) 93,
        (byte) 171,
        (byte) 252,
        (byte) 35,
        (byte) 150,
        (byte) 179,
        (byte) 207
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 39,
        (byte) 148,
        (byte) 118,
        (byte) 170,
        (byte) 14,
        (byte) 185,
        (byte) 238,
        (byte) 81,
        (byte) 193,
        (byte) 82,
        (byte) 245,
        (byte) 44,
        (byte) 166,
        (byte) 64 /*0x40*/,
        (byte) 194,
        (byte) 162,
        (byte) 122,
        (byte) 115,
        (byte) 156,
        (byte) 98,
        (byte) 104,
        (byte) 189,
        (byte) 146,
        (byte) 223,
        (byte) 157,
        (byte) 116,
        (byte) 249,
        (byte) 247,
        (byte) 149,
        (byte) 148,
        (byte) 162,
        (byte) 156,
        (byte) 6,
        (byte) 1,
        (byte) 71,
        (byte) 32 /*0x20*/,
        (byte) 60,
        (byte) 63 /*0x3F*/,
        (byte) 181,
        (byte) 173,
        (byte) 124,
        (byte) 34,
        (byte) 216,
        (byte) 81,
        (byte) 217,
        (byte) 143,
        (byte) 52,
        (byte) 109,
        (byte) 238,
        (byte) 152,
        (byte) 44,
        (byte) 249,
        (byte) 19,
        (byte) 225,
        (byte) 68
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[24] = (byte) 191;
      numArray4[42] = (byte) 195;
      numArray4[25] = (byte) 55;
      numArray4[52] = (byte) 249;
      numArray4[4] = (byte) 137;
      numArray4[18] = (byte) 213;
      numArray4[6] = (byte) 114;
      numArray4[31 /*0x1F*/] = (byte) 27;
      numArray4[2] = (byte) 125;
      numArray4[9] = (byte) 30;
      numArray4[1] = (byte) 67;
      numArray4[11] = (byte) 27;
      numArray4[35] = (byte) 169;
      numArray4[29] = (byte) 247;
      numArray4[5] = (byte) 47;
      numArray4[15] = (byte) 55;
      numArray4[16 /*0x10*/] = (byte) 196;
      numArray4[13] = (byte) 117;
      numArray4[45] = (byte) 115;
      numArray4[19] = (byte) 109;
      numArray4[0] = (byte) 175;
      numArray4[30] = (byte) 101;
      numArray4[22] = (byte) 246;
      numArray4[48 /*0x30*/] = (byte) 207;
      numArray4[3] = (byte) 236;
      numArray4[43] = (byte) 107;
      numArray4[26] = (byte) 142;
      numArray4[20] = (byte) 155;
      numArray4[28] = (byte) 89;
      numArray4[7] = (byte) 134;
      numArray4[47] = (byte) 53;
      numArray4[8] = (byte) 219;
      numArray4[32 /*0x20*/] = (byte) 254;
      numArray4[33] = (byte) 182;
      numArray4[34] = (byte) 118;
      numArray4[10] = (byte) 52;
      numArray4[36] = (byte) 59;
      numArray4[37] = (byte) 159;
      numArray4[41] = (byte) 46;
      numArray4[14] = (byte) 44;
      numArray4[38] = (byte) 18;
      numArray4[21] = (byte) 165;
      numArray4[40] = (byte) 59;
      numArray4[17] = (byte) 80 /*0x50*/;
      numArray4[27] = (byte) 148;
      numArray4[12] = (byte) 130;
      numArray4[46] = (byte) 218;
      numArray4[44] = (byte) 92;
      numArray4[39] = (byte) 243;
      numArray4[49] = (byte) 235;
      numArray4[50] = (byte) 210;
      numArray4[51] = (byte) 98;
      numArray4[23] = (byte) 182;
      numArray4[53] = (byte) 24;
      numArray4[54] = (byte) 123;
      byte[] numArray5 = new byte[55];
      numArray5[32 /*0x20*/] = (byte) 13;
      numArray5[1] = (byte) 91;
      numArray5[5] = (byte) 68;
      numArray5[3] = (byte) 1;
      numArray5[4] = (byte) 243;
      numArray5[10] = (byte) 217;
      numArray5[44] = (byte) 237;
      numArray5[40] = (byte) 209;
      numArray5[34] = (byte) 136;
      numArray5[0] = (byte) 143;
      numArray5[9] = (byte) 82;
      numArray5[27] = (byte) 250;
      numArray5[41] = (byte) 89;
      numArray5[13] = (byte) 32 /*0x20*/;
      numArray5[14] = (byte) 93;
      numArray5[45] = (byte) 229;
      numArray5[11] = (byte) 131;
      numArray5[20] = (byte) 36;
      numArray5[2] = (byte) 87;
      numArray5[19] = (byte) 189;
      numArray5[7] = (byte) 33;
      numArray5[21] = (byte) 106;
      numArray5[22] = (byte) 145;
      numArray5[23] = (byte) 209;
      numArray5[24] = (byte) 246;
      numArray5[25] = (byte) 106;
      numArray5[8] = (byte) 9;
      numArray5[12] = (byte) 108;
      numArray5[28] = (byte) 205;
      numArray5[53] = (byte) 235;
      numArray5[6] = (byte) 8;
      numArray5[31 /*0x1F*/] = (byte) 102;
      numArray5[49] = (byte) 182;
      numArray5[33] = (byte) 203;
      numArray5[15] = (byte) 120;
      numArray5[46] = (byte) 214;
      numArray5[36] = (byte) 53;
      numArray5[37] = (byte) 105;
      numArray5[38] = (byte) 209;
      numArray5[29] = (byte) 222;
      numArray5[30] = (byte) 216;
      numArray5[26] = (byte) 92;
      numArray5[17] = (byte) 107;
      numArray5[42] = (byte) 16 /*0x10*/;
      numArray5[16 /*0x10*/] = (byte) 46;
      numArray5[43] = (byte) 38;
      numArray5[50] = (byte) 243;
      numArray5[47] = (byte) 90;
      numArray5[48 /*0x30*/] = (byte) 93;
      numArray5[18] = (byte) 160 /*0xA0*/;
      numArray5[39] = (byte) 6;
      numArray5[51] = (byte) 172;
      numArray5[52] = (byte) 204;
      numArray5[35] = (byte) 147;
      numArray5[54] = (byte) 207;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 88,
        (byte) 233,
        (byte) 168,
        (byte) 38,
        (byte) 162,
        (byte) 61,
        (byte) 70,
        (byte) 215,
        (byte) 236,
        (byte) 56,
        (byte) 101,
        (byte) 76,
        (byte) 142,
        (byte) 131,
        (byte) 33,
        (byte) 179,
        (byte) 132,
        (byte) 148,
        (byte) 38,
        (byte) 225,
        (byte) 217,
        (byte) 200,
        (byte) 216,
        (byte) 207,
        (byte) 125,
        (byte) 83,
        (byte) 25,
        (byte) 126,
        (byte) 54,
        (byte) 130,
        (byte) 238,
        (byte) 135,
        (byte) 32 /*0x20*/,
        (byte) 173,
        (byte) 172,
        (byte) 167,
        (byte) 33,
        (byte) 228,
        (byte) 159,
        (byte) 6,
        (byte) 218,
        (byte) 37,
        (byte) 96 /*0x60*/,
        (byte) 229,
        (byte) 24,
        (byte) 88,
        (byte) 150,
        (byte) 226,
        (byte) 216,
        (byte) 243,
        (byte) 3,
        (byte) 124,
        (byte) 125,
        (byte) 67,
        (byte) 18
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 126,
        (byte) 67,
        (byte) 13,
        (byte) 243,
        (byte) 188,
        (byte) 56,
        (byte) 37,
        (byte) 100,
        (byte) 198,
        (byte) 91,
        (byte) 109,
        (byte) 97,
        (byte) 4,
        (byte) 34,
        (byte) 117,
        (byte) 87,
        (byte) 58,
        (byte) 50,
        (byte) 168,
        (byte) 29,
        (byte) 60,
        (byte) 9,
        (byte) 250,
        (byte) 140,
        (byte) 220,
        (byte) 254,
        (byte) 80 /*0x50*/,
        (byte) 112 /*0x70*/,
        (byte) 44,
        (byte) 159,
        (byte) 227,
        (byte) 191,
        (byte) 24,
        (byte) 110,
        (byte) 253,
        (byte) 118,
        (byte) 220,
        (byte) 203,
        (byte) 103,
        (byte) 16 /*0x10*/,
        (byte) 226,
        (byte) 46,
        (byte) 118,
        (byte) 240 /*0xF0*/,
        (byte) 34,
        (byte) 69,
        (byte) 151,
        (byte) 65,
        (byte) 15,
        (byte) 55,
        (byte) 167,
        (byte) 33,
        (byte) 187,
        (byte) 36,
        (byte) 162
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 217,
        (byte) 169,
        (byte) 78,
        (byte) 36,
        (byte) 199,
        (byte) 161,
        (byte) 106,
        (byte) 73,
        (byte) 221,
        (byte) 44,
        (byte) 224 /*0xE0*/,
        (byte) 214,
        (byte) 121,
        (byte) 146,
        (byte) 160 /*0xA0*/,
        (byte) 244,
        (byte) 31 /*0x1F*/,
        (byte) 6,
        (byte) 179,
        (byte) 104,
        (byte) 243,
        (byte) 183,
        (byte) 36,
        (byte) 189,
        (byte) 55,
        (byte) 166,
        (byte) 67,
        (byte) 127 /*0x7F*/,
        (byte) 114,
        (byte) 9,
        (byte) 221,
        (byte) 155,
        (byte) 28,
        (byte) 150,
        (byte) 72,
        (byte) 58,
        (byte) 81,
        (byte) 226,
        (byte) 18,
        (byte) 132,
        (byte) 245,
        (byte) 155,
        (byte) 58,
        (byte) 43,
        (byte) 181,
        (byte) 12,
        (byte) 94,
        (byte) 96 /*0x60*/,
        (byte) 98,
        (byte) 135,
        (byte) 94,
        (byte) 60,
        (byte) 94,
        (byte) 39,
        (byte) 30
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 165,
        (byte) 116,
        (byte) 44,
        (byte) 168,
        (byte) 28,
        (byte) 211,
        (byte) 12,
        (byte) 232,
        (byte) 161,
        (byte) 234,
        (byte) 242,
        (byte) 208 /*0xD0*/,
        (byte) 208 /*0xD0*/,
        (byte) 4,
        (byte) 92,
        (byte) 152,
        (byte) 198,
        (byte) 188,
        (byte) 167,
        (byte) 197,
        (byte) 130,
        (byte) 67,
        (byte) 85,
        (byte) 238,
        (byte) 218,
        (byte) 198,
        (byte) 161,
        (byte) 178,
        (byte) 154,
        (byte) 77,
        (byte) 210,
        (byte) 74,
        (byte) 206,
        (byte) 16 /*0x10*/,
        (byte) 63 /*0x3F*/,
        (byte) 228,
        (byte) 160 /*0xA0*/,
        (byte) 228,
        (byte) 158,
        (byte) 233,
        (byte) 91,
        (byte) 32 /*0x20*/,
        (byte) 215,
        (byte) 221,
        (byte) 136,
        (byte) 79,
        (byte) 94,
        (byte) 193,
        (byte) 176 /*0xB0*/,
        (byte) 67,
        (byte) 66,
        (byte) 167,
        (byte) 76,
        (byte) 126,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 145,
        (byte) 236,
        (byte) 33,
        (byte) 94,
        (byte) 235,
        (byte) 215,
        (byte) 211,
        (byte) 79,
        (byte) 69,
        (byte) 185,
        (byte) 227,
        (byte) 74,
        (byte) 102,
        (byte) 163,
        (byte) 61,
        (byte) 243,
        (byte) 47,
        (byte) 114,
        (byte) 87,
        (byte) 124,
        (byte) 249,
        (byte) 7,
        (byte) 43,
        (byte) 71,
        (byte) 226,
        (byte) 28,
        (byte) 234,
        (byte) 171,
        (byte) 160 /*0xA0*/,
        (byte) 60,
        (byte) 245,
        (byte) 120,
        (byte) 38,
        (byte) 182,
        (byte) 191,
        (byte) 245,
        (byte) 241,
        (byte) 38,
        (byte) 10,
        (byte) 196,
        (byte) 103,
        (byte) 220,
        (byte) 111,
        (byte) 82,
        (byte) 122,
        (byte) 22,
        (byte) 252,
        (byte) 228,
        (byte) 32 /*0x20*/,
        (byte) 72,
        (byte) 47,
        (byte) 180,
        (byte) 198,
        (byte) 164,
        (byte) 139
      };
      byte[] numArray11 = new byte[55];
      numArray11[22] = (byte) 27;
      numArray11[18] = (byte) 123;
      numArray11[53] = (byte) 154;
      numArray11[2] = (byte) 133;
      numArray11[4] = (byte) 117;
      numArray11[3] = (byte) 197;
      numArray11[6] = (byte) 234;
      numArray11[7] = (byte) 18;
      numArray11[8] = (byte) 64 /*0x40*/;
      numArray11[15] = (byte) 23;
      numArray11[10] = (byte) 216;
      numArray11[11] = (byte) 136;
      numArray11[12] = (byte) 149;
      numArray11[13] = (byte) 100;
      numArray11[47] = (byte) 8;
      numArray11[5] = (byte) 184;
      numArray11[24] = (byte) 40;
      numArray11[41] = (byte) 212;
      numArray11[20] = (byte) 163;
      numArray11[45] = (byte) 167;
      numArray11[0] = (byte) 150;
      numArray11[44] = (byte) 231;
      numArray11[30] = (byte) 197;
      numArray11[23] = (byte) 0;
      numArray11[37] = (byte) 114;
      numArray11[25] = (byte) 86;
      numArray11[26] = (byte) 147;
      numArray11[43] = (byte) 137;
      numArray11[28] = (byte) 5;
      numArray11[29] = (byte) 27;
      numArray11[9] = (byte) 87;
      numArray11[31 /*0x1F*/] = (byte) 99;
      numArray11[36] = (byte) 199;
      numArray11[33] = (byte) 9;
      numArray11[14] = (byte) 119;
      numArray11[16 /*0x10*/] = (byte) 58;
      numArray11[51] = (byte) 183;
      numArray11[40] = (byte) 216;
      numArray11[38] = (byte) 239;
      numArray11[39] = (byte) 178;
      numArray11[21] = (byte) 185;
      numArray11[32 /*0x20*/] = (byte) 224 /*0xE0*/;
      numArray11[42] = (byte) 50;
      numArray11[46] = (byte) 220;
      numArray11[48 /*0x30*/] = (byte) 67;
      numArray11[35] = (byte) 63 /*0x3F*/;
      numArray11[19] = (byte) 195;
      numArray11[54] = (byte) 39;
      numArray11[17] = (byte) 173;
      numArray11[49] = (byte) 58;
      numArray11[50] = (byte) 77;
      numArray11[34] = (byte) 222;
      numArray11[52] = (byte) 208 /*0xD0*/;
      numArray11[1] = (byte) 44;
      numArray11[27] = (byte) 86;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[52];
      numArray12[45] = (byte) 82;
      numArray12[29] = (byte) 126;
      numArray12[2] = (byte) 174;
      numArray12[3] = (byte) 229;
      numArray12[17] = (byte) 214;
      numArray12[13] = (byte) 235;
      numArray12[6] = (byte) 251;
      numArray12[28] = (byte) 244;
      numArray12[8] = (byte) 186;
      numArray12[9] = (byte) 161;
      numArray12[42] = (byte) 85;
      numArray12[11] = (byte) 174;
      numArray12[36] = (byte) 141;
      numArray12[47] = (byte) 90;
      numArray12[14] = (byte) 94;
      numArray12[41] = (byte) 9;
      numArray12[19] = (byte) 48 /*0x30*/;
      numArray12[26] = (byte) 18;
      numArray12[16 /*0x10*/] = (byte) 187;
      numArray12[10] = (byte) 133;
      numArray12[20] = (byte) 75;
      numArray12[27] = (byte) 230;
      numArray12[22] = (byte) 218;
      numArray12[1] = (byte) 175;
      numArray12[23] = (byte) 211;
      numArray12[25] = (byte) 178;
      numArray12[4] = (byte) 156;
      numArray12[12] = (byte) 20;
      numArray12[0] = (byte) 12;
      numArray12[15] = (byte) 44;
      numArray12[30] = (byte) 243;
      numArray12[31 /*0x1F*/] = (byte) 187;
      numArray12[44] = (byte) 199;
      numArray12[40] = (byte) 107;
      numArray12[34] = (byte) 10;
      numArray12[35] = (byte) 250;
      numArray12[33] = (byte) 75;
      numArray12[39] = (byte) 216;
      numArray12[24] = (byte) 5;
      numArray12[38] = (byte) 163;
      numArray12[48 /*0x30*/] = (byte) 199;
      numArray12[37] = (byte) 242;
      numArray12[32 /*0x20*/] = (byte) 67;
      numArray12[5] = (byte) 86;
      numArray12[18] = (byte) 238;
      numArray12[7] = (byte) 132;
      numArray12[46] = (byte) 196;
      numArray12[49] = (byte) 32 /*0x20*/;
      numArray12[21] = (byte) 189;
      numArray12[43] = (byte) 80 /*0x50*/;
      numArray12[50] = (byte) 120;
      numArray12[51] = (byte) 184;
      byte[] numArray13 = new byte[52]
      {
        (byte) 117,
        (byte) 144 /*0x90*/,
        byte.MaxValue,
        (byte) 198,
        (byte) 211,
        (byte) 18,
        (byte) 245,
        (byte) 4,
        (byte) 214,
        (byte) 105,
        (byte) 33,
        (byte) 109,
        (byte) 113,
        (byte) 41,
        (byte) 159,
        (byte) 184,
        (byte) 164,
        (byte) 126,
        (byte) 224 /*0xE0*/,
        (byte) 177,
        (byte) 185,
        (byte) 129,
        (byte) 217,
        (byte) 76,
        (byte) 144 /*0x90*/,
        byte.MaxValue,
        (byte) 202,
        (byte) 86,
        (byte) 101,
        (byte) 96 /*0x60*/,
        (byte) 33,
        (byte) 253,
        (byte) 72,
        (byte) 246,
        (byte) 21,
        (byte) 56,
        (byte) 44,
        (byte) 136,
        (byte) 8,
        (byte) 214,
        (byte) 192 /*0xC0*/,
        (byte) 191,
        (byte) 160 /*0xA0*/,
        (byte) 88,
        (byte) 98,
        (byte) 3,
        (byte) 72,
        (byte) 1,
        (byte) 42,
        (byte) 231,
        (byte) 48 /*0x30*/,
        byte.MaxValue
      };
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index + 275] ^= numArray13[index];
      byte[] numArray14 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_13393.sspq, 825, (Array) numArray14, 0, 20);
      key.Query(true, 335, numArray14, response);
      Array.Copy((Array) sc_13393.sspr, 825, (Array) numArray14, 0, 20);
      for (int index = 0; index < numArray14.Length; ++index)
      {
        if ((int) numArray14[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray15 = new byte[327];
    byte[] numArray16 = new byte[55]
    {
      (byte) 129,
      (byte) 110,
      (byte) 201,
      (byte) 79,
      (byte) 231,
      (byte) 13,
      (byte) 204,
      (byte) 242,
      (byte) 62,
      (byte) 187,
      (byte) 82,
      (byte) 86,
      (byte) 181,
      (byte) 207,
      (byte) 133,
      (byte) 83,
      (byte) 212,
      (byte) 218,
      (byte) 225,
      (byte) 229,
      (byte) 190,
      (byte) 8,
      (byte) 97,
      (byte) 189,
      (byte) 227,
      (byte) 47,
      (byte) 111,
      (byte) 252,
      (byte) 225,
      (byte) 84,
      (byte) 37,
      (byte) 147,
      (byte) 248,
      (byte) 13,
      (byte) 234,
      (byte) 141,
      (byte) 145,
      (byte) 151,
      (byte) 99,
      (byte) 194,
      (byte) 96 /*0x60*/,
      (byte) 9,
      (byte) 77,
      (byte) 57,
      (byte) 105,
      (byte) 223,
      (byte) 189,
      (byte) 190,
      (byte) 100,
      (byte) 198,
      (byte) 81,
      (byte) 99,
      (byte) 12,
      (byte) 209,
      (byte) 186
    };
    byte[] numArray17 = new byte[55];
    numArray17[48 /*0x30*/] = (byte) 65;
    numArray17[8] = (byte) 36;
    numArray17[1] = (byte) 134;
    numArray17[3] = (byte) 215;
    numArray17[30] = (byte) 232;
    numArray17[5] = (byte) 137;
    numArray17[4] = byte.MaxValue;
    numArray17[32 /*0x20*/] = (byte) 226;
    numArray17[20] = (byte) 169;
    numArray17[9] = (byte) 217;
    numArray17[35] = (byte) 54;
    numArray17[11] = (byte) 77;
    numArray17[12] = (byte) 103;
    numArray17[13] = (byte) 47;
    numArray17[14] = (byte) 136;
    numArray17[50] = (byte) 243;
    numArray17[16 /*0x10*/] = (byte) 236;
    numArray17[27] = (byte) 55;
    numArray17[18] = (byte) 105;
    numArray17[10] = (byte) 17;
    numArray17[54] = (byte) 114;
    numArray17[21] = (byte) 125;
    numArray17[22] = (byte) 24;
    numArray17[7] = (byte) 134;
    numArray17[49] = (byte) 83;
    numArray17[23] = (byte) 188;
    numArray17[26] = (byte) 222;
    numArray17[39] = (byte) 24;
    numArray17[28] = (byte) 153;
    numArray17[29] = (byte) 39;
    numArray17[33] = (byte) 18;
    numArray17[24] = (byte) 238;
    numArray17[45] = (byte) 41;
    numArray17[40] = (byte) 29;
    numArray17[34] = (byte) 141;
    numArray17[44] = (byte) 113;
    numArray17[0] = (byte) 80 /*0x50*/;
    numArray17[6] = (byte) 99;
    numArray17[51] = (byte) 113;
    numArray17[36] = (byte) 75;
    numArray17[17] = (byte) 120;
    numArray17[15] = (byte) 208 /*0xD0*/;
    numArray17[19] = (byte) 153;
    numArray17[25] = (byte) 177;
    numArray17[37] = (byte) 103;
    numArray17[41] = (byte) 41;
    numArray17[46] = (byte) 55;
    numArray17[47] = (byte) 195;
    numArray17[42] = (byte) 177;
    numArray17[31 /*0x1F*/] = (byte) 222;
    numArray17[43] = (byte) 11;
    numArray17[2] = (byte) 172;
    numArray17[52] = (byte) 155;
    numArray17[53] = (byte) 33;
    numArray17[38] = (byte) 119;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray15, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index] ^= numArray17[index];
    byte[] numArray18 = new byte[55]
    {
      (byte) 183,
      (byte) 104,
      (byte) 70,
      (byte) 238,
      (byte) 55,
      (byte) 246,
      (byte) 231,
      (byte) 211,
      (byte) 56,
      (byte) 54,
      (byte) 85,
      (byte) 115,
      (byte) 118,
      (byte) 61,
      (byte) 16 /*0x10*/,
      (byte) 203,
      (byte) 251,
      (byte) 244,
      (byte) 189,
      (byte) 87,
      (byte) 131,
      (byte) 170,
      (byte) 130,
      (byte) 92,
      (byte) 214,
      (byte) 42,
      (byte) 22,
      (byte) 238,
      (byte) 214,
      (byte) 153,
      (byte) 217,
      (byte) 77,
      (byte) 180,
      (byte) 155,
      (byte) 206,
      (byte) 195,
      (byte) 101,
      (byte) 53,
      (byte) 88,
      (byte) 1,
      (byte) 124,
      (byte) 208 /*0xD0*/,
      (byte) 113,
      (byte) 74,
      (byte) 192 /*0xC0*/,
      (byte) 101,
      (byte) 247,
      (byte) 198,
      (byte) 3,
      (byte) 32 /*0x20*/,
      (byte) 241,
      (byte) 89,
      (byte) 72,
      (byte) 101,
      (byte) 97
    };
    byte[] numArray19 = new byte[55]
    {
      (byte) 11,
      (byte) 227,
      (byte) 31 /*0x1F*/,
      (byte) 6,
      (byte) 107,
      (byte) 192 /*0xC0*/,
      (byte) 59,
      (byte) 140,
      (byte) 87,
      (byte) 216,
      (byte) 174,
      (byte) 27,
      (byte) 209,
      (byte) 205,
      (byte) 237,
      (byte) 180,
      (byte) 9,
      (byte) 93,
      (byte) 174,
      (byte) 14,
      (byte) 207,
      (byte) 156,
      (byte) 190,
      (byte) 105,
      (byte) 59,
      (byte) 20,
      (byte) 193,
      (byte) 199,
      (byte) 53,
      (byte) 95,
      (byte) 158,
      (byte) 63 /*0x3F*/,
      (byte) 153,
      (byte) 128 /*0x80*/,
      (byte) 107,
      (byte) 241,
      (byte) 81,
      (byte) 22,
      (byte) 119,
      (byte) 245,
      (byte) 19,
      (byte) 227,
      (byte) 50,
      byte.MaxValue,
      (byte) 96 /*0x60*/,
      (byte) 58,
      (byte) 241,
      (byte) 145,
      (byte) 134,
      (byte) 221,
      (byte) 198,
      (byte) 213,
      (byte) 59,
      (byte) 18,
      (byte) 25
    };
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray15, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 55] ^= numArray19[index];
    byte[] numArray20 = new byte[55];
    numArray20[16 /*0x10*/] = (byte) 187;
    numArray20[6] = (byte) 159;
    numArray20[2] = (byte) 13;
    numArray20[8] = (byte) 7;
    numArray20[4] = (byte) 248;
    numArray20[5] = (byte) 253;
    numArray20[7] = (byte) 93;
    numArray20[21] = (byte) 156;
    numArray20[15] = (byte) 155;
    numArray20[39] = (byte) 211;
    numArray20[3] = (byte) 223;
    numArray20[17] = (byte) 219;
    numArray20[34] = (byte) 123;
    numArray20[33] = (byte) 221;
    numArray20[14] = (byte) 198;
    numArray20[24] = (byte) 80 /*0x50*/;
    numArray20[48 /*0x30*/] = (byte) 43;
    numArray20[0] = (byte) 233;
    numArray20[1] = (byte) 23;
    numArray20[9] = (byte) 190;
    numArray20[20] = (byte) 55;
    numArray20[11] = (byte) 110;
    numArray20[22] = (byte) 113;
    numArray20[23] = (byte) 51;
    numArray20[12] = (byte) 76;
    numArray20[25] = (byte) 234;
    numArray20[26] = (byte) 181;
    numArray20[18] = (byte) 178;
    numArray20[47] = (byte) 186;
    numArray20[29] = (byte) 5;
    numArray20[28] = (byte) 128 /*0x80*/;
    numArray20[19] = (byte) 56;
    numArray20[32 /*0x20*/] = (byte) 193;
    numArray20[53] = (byte) 212;
    numArray20[13] = (byte) 102;
    numArray20[30] = (byte) 79;
    numArray20[36] = (byte) 75;
    numArray20[37] = (byte) 34;
    numArray20[38] = (byte) 131;
    numArray20[10] = (byte) 218;
    numArray20[40] = (byte) 210;
    numArray20[44] = (byte) 28;
    numArray20[42] = (byte) 184;
    numArray20[27] = (byte) 167;
    numArray20[31 /*0x1F*/] = (byte) 57;
    numArray20[43] = (byte) 167;
    numArray20[46] = (byte) 182;
    numArray20[45] = (byte) 87;
    numArray20[41] = (byte) 237;
    numArray20[49] = (byte) 189;
    numArray20[50] = (byte) 140;
    numArray20[51] = (byte) 138;
    numArray20[52] = (byte) 175;
    numArray20[35] = (byte) 29;
    numArray20[54] = (byte) 174;
    byte[] numArray21 = new byte[55]
    {
      (byte) 15,
      (byte) 64 /*0x40*/,
      (byte) 12,
      (byte) 197,
      (byte) 52,
      (byte) 196,
      (byte) 252,
      (byte) 47,
      (byte) 0,
      (byte) 39,
      (byte) 188,
      byte.MaxValue,
      (byte) 127 /*0x7F*/,
      (byte) 114,
      (byte) 26,
      (byte) 2,
      (byte) 95,
      (byte) 132,
      (byte) 213,
      (byte) 20,
      (byte) 254,
      (byte) 214,
      (byte) 180,
      (byte) 238,
      (byte) 7,
      (byte) 50,
      (byte) 232,
      (byte) 152,
      (byte) 47,
      (byte) 97,
      (byte) 101,
      byte.MaxValue,
      (byte) 23,
      (byte) 124,
      (byte) 6,
      (byte) 148,
      (byte) 189,
      (byte) 53,
      (byte) 231,
      (byte) 241,
      (byte) 77,
      (byte) 223,
      (byte) 3,
      (byte) 127 /*0x7F*/,
      (byte) 20,
      (byte) 118,
      (byte) 246,
      (byte) 33,
      (byte) 224 /*0xE0*/,
      (byte) 171,
      (byte) 126,
      (byte) 235,
      (byte) 110,
      (byte) 244,
      (byte) 97
    };
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray15, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 110] ^= numArray21[index];
    byte[] numArray22 = new byte[55]
    {
      (byte) 194,
      (byte) 150,
      (byte) 218,
      (byte) 76,
      (byte) 123,
      (byte) 25,
      (byte) 137,
      (byte) 169,
      (byte) 22,
      (byte) 193,
      (byte) 64 /*0x40*/,
      (byte) 189,
      (byte) 49,
      (byte) 221,
      (byte) 4,
      (byte) 226,
      (byte) 82,
      (byte) 139,
      (byte) 55,
      (byte) 46,
      (byte) 145,
      (byte) 156,
      (byte) 78,
      (byte) 235,
      (byte) 44,
      (byte) 250,
      (byte) 85,
      (byte) 132,
      (byte) 146,
      (byte) 185,
      (byte) 126,
      (byte) 201,
      (byte) 195,
      (byte) 154,
      (byte) 175,
      (byte) 253,
      (byte) 154,
      (byte) 64 /*0x40*/,
      (byte) 95,
      (byte) 248,
      (byte) 43,
      (byte) 142,
      (byte) 100,
      (byte) 232,
      (byte) 7,
      (byte) 17,
      (byte) 80 /*0x50*/,
      (byte) 144 /*0x90*/,
      (byte) 239,
      (byte) 80 /*0x50*/,
      (byte) 126,
      (byte) 226,
      (byte) 45,
      (byte) 59,
      (byte) 74
    };
    byte[] numArray23 = new byte[55]
    {
      (byte) 147,
      (byte) 238,
      (byte) 120,
      (byte) 101,
      (byte) 66,
      (byte) 60,
      (byte) 139,
      (byte) 10,
      (byte) 224 /*0xE0*/,
      (byte) 73,
      (byte) 201,
      (byte) 45,
      (byte) 23,
      (byte) 17,
      (byte) 13,
      (byte) 210,
      (byte) 177,
      (byte) 82,
      (byte) 173,
      (byte) 152,
      (byte) 120,
      (byte) 137,
      (byte) 72,
      (byte) 55,
      (byte) 141,
      (byte) 151,
      (byte) 23,
      (byte) 205,
      (byte) 182,
      (byte) 150,
      (byte) 180,
      (byte) 239,
      (byte) 189,
      (byte) 62,
      (byte) 227,
      (byte) 87,
      (byte) 72,
      (byte) 21,
      (byte) 46,
      (byte) 55,
      (byte) 198,
      (byte) 194,
      (byte) 66,
      (byte) 113,
      (byte) 167,
      (byte) 236,
      (byte) 9,
      (byte) 52,
      (byte) 62,
      (byte) 208 /*0xD0*/,
      (byte) 99,
      (byte) 112 /*0x70*/,
      (byte) 186,
      (byte) 99,
      (byte) 13
    };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray15, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 165] ^= numArray23[index];
    byte[] numArray24 = new byte[55]
    {
      (byte) 239,
      (byte) 31 /*0x1F*/,
      (byte) 129,
      (byte) 196,
      (byte) 5,
      (byte) 22,
      (byte) 93,
      (byte) 66,
      (byte) 118,
      (byte) 249,
      (byte) 150,
      (byte) 46,
      (byte) 7,
      (byte) 71,
      (byte) 18,
      (byte) 44,
      (byte) 63 /*0x3F*/,
      (byte) 2,
      (byte) 3,
      (byte) 56,
      (byte) 102,
      (byte) 228,
      (byte) 27,
      (byte) 169,
      (byte) 196,
      (byte) 148,
      (byte) 66,
      (byte) 59,
      (byte) 155,
      (byte) 155,
      (byte) 136,
      (byte) 80 /*0x50*/,
      (byte) 136,
      (byte) 182,
      (byte) 171,
      (byte) 32 /*0x20*/,
      (byte) 222,
      (byte) 52,
      (byte) 37,
      (byte) 119,
      (byte) 6,
      (byte) 165,
      (byte) 145,
      (byte) 23,
      (byte) 185,
      (byte) 97,
      (byte) 232,
      (byte) 8,
      (byte) 107,
      (byte) 182,
      (byte) 238,
      (byte) 56,
      (byte) 71,
      (byte) 238,
      (byte) 224 /*0xE0*/
    };
    byte[] numArray25 = new byte[55]
    {
      (byte) 103,
      (byte) 151,
      (byte) 134,
      (byte) 51,
      (byte) 62,
      (byte) 222,
      (byte) 189,
      (byte) 225,
      (byte) 96 /*0x60*/,
      (byte) 246,
      (byte) 70,
      (byte) 55,
      (byte) 58,
      (byte) 84,
      (byte) 151,
      (byte) 223,
      (byte) 135,
      (byte) 64 /*0x40*/,
      (byte) 137,
      (byte) 167,
      (byte) 219,
      (byte) 100,
      (byte) 12,
      (byte) 2,
      (byte) 115,
      (byte) 70,
      (byte) 241,
      (byte) 104,
      (byte) 52,
      (byte) 52,
      (byte) 31 /*0x1F*/,
      (byte) 100,
      (byte) 152,
      (byte) 177,
      (byte) 224 /*0xE0*/,
      (byte) 129,
      (byte) 233,
      (byte) 117,
      (byte) 119,
      (byte) 107,
      (byte) 60,
      (byte) 68,
      (byte) 132,
      (byte) 2,
      (byte) 72,
      (byte) 31 /*0x1F*/,
      (byte) 126,
      (byte) 62,
      (byte) 13,
      (byte) 190,
      (byte) 170,
      (byte) 120,
      (byte) 216,
      (byte) 44,
      (byte) 254
    };
    key.Query(true, 335, numArray24, numArray24);
    Array.Copy((Array) numArray24, 0, (Array) numArray15, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 220] ^= numArray25[index];
    byte[] numArray26 = new byte[52]
    {
      (byte) 215,
      byte.MaxValue,
      (byte) 113,
      (byte) 211,
      (byte) 33,
      (byte) 167,
      (byte) 218,
      (byte) 112 /*0x70*/,
      (byte) 70,
      (byte) 165,
      (byte) 80 /*0x50*/,
      (byte) 156,
      (byte) 242,
      (byte) 78,
      (byte) 10,
      (byte) 220,
      (byte) 139,
      (byte) 49,
      (byte) 192 /*0xC0*/,
      (byte) 82,
      (byte) 4,
      (byte) 240 /*0xF0*/,
      (byte) 99,
      (byte) 172,
      (byte) 58,
      (byte) 208 /*0xD0*/,
      (byte) 228,
      (byte) 173,
      (byte) 80 /*0x50*/,
      (byte) 8,
      (byte) 154,
      (byte) 213,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 6,
      (byte) 53,
      (byte) 180,
      (byte) 164,
      (byte) 229,
      (byte) 254,
      (byte) 182,
      (byte) 166,
      (byte) 115,
      (byte) 187,
      (byte) 151,
      (byte) 107,
      (byte) 121,
      (byte) 104,
      (byte) 227,
      (byte) 91,
      (byte) 188,
      (byte) 189
    };
    byte[] numArray27 = new byte[52]
    {
      (byte) 42,
      (byte) 192 /*0xC0*/,
      (byte) 183,
      (byte) 197,
      (byte) 169,
      (byte) 185,
      (byte) 233,
      (byte) 144 /*0x90*/,
      (byte) 110,
      (byte) 211,
      (byte) 168,
      (byte) 77,
      (byte) 208 /*0xD0*/,
      (byte) 101,
      (byte) 248,
      (byte) 250,
      (byte) 62,
      (byte) 49,
      (byte) 218,
      (byte) 22,
      (byte) 143,
      (byte) 143,
      (byte) 122,
      (byte) 206,
      (byte) 112 /*0x70*/,
      (byte) 127 /*0x7F*/,
      (byte) 133,
      (byte) 13,
      (byte) 166,
      (byte) 179,
      (byte) 210,
      (byte) 233,
      (byte) 169,
      (byte) 85,
      (byte) 57,
      (byte) 225,
      (byte) 168,
      (byte) 203,
      (byte) 224 /*0xE0*/,
      (byte) 91,
      (byte) 47,
      (byte) 5,
      (byte) 240 /*0xF0*/,
      (byte) 129,
      (byte) 244,
      (byte) 111,
      (byte) 112 /*0x70*/,
      (byte) 230,
      (byte) 90,
      (byte) 64 /*0x40*/,
      (byte) 171,
      (byte) 227
    };
    key.Query(true, 335, numArray26, numArray26);
    Array.Copy((Array) numArray26, 0, (Array) numArray15, 275, 52);
    for (int index = 0; index < 52; ++index)
      numArray15[index + 275] ^= numArray27[index];
    return Encoding.UTF8.GetString(numArray15);
  }

  internal static string ssp_appserver_13460()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[287];
      byte[] numArray2 = new byte[55]
      {
        (byte) 155,
        (byte) 196,
        (byte) 153,
        (byte) 81,
        (byte) 194,
        (byte) 45,
        (byte) 32 /*0x20*/,
        (byte) 61,
        (byte) 10,
        (byte) 123,
        (byte) 252,
        (byte) 214,
        (byte) 71,
        (byte) 213,
        (byte) 40,
        (byte) 231,
        (byte) 75,
        (byte) 194,
        (byte) 156,
        (byte) 93,
        (byte) 124,
        (byte) 167,
        (byte) 20,
        (byte) 207,
        (byte) 86,
        (byte) 124,
        (byte) 201,
        (byte) 245,
        (byte) 76,
        (byte) 221,
        (byte) 181,
        (byte) 232,
        (byte) 27,
        (byte) 31 /*0x1F*/,
        (byte) 153,
        (byte) 239,
        (byte) 10,
        (byte) 147,
        (byte) 60,
        (byte) 136,
        (byte) 134,
        (byte) 53,
        (byte) 176 /*0xB0*/,
        (byte) 28,
        (byte) 111,
        (byte) 163,
        (byte) 33,
        (byte) 14,
        (byte) 50,
        (byte) 213,
        (byte) 113,
        (byte) 94,
        (byte) 27,
        (byte) 162,
        (byte) 106
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 237,
        (byte) 97,
        (byte) 232,
        (byte) 250,
        (byte) 191,
        (byte) 126,
        (byte) 96 /*0x60*/,
        (byte) 18,
        (byte) 170,
        (byte) 53,
        (byte) 191,
        (byte) 59,
        (byte) 69,
        (byte) 177,
        (byte) 186,
        (byte) 94,
        (byte) 243,
        (byte) 170,
        (byte) 201,
        (byte) 244,
        (byte) 134,
        (byte) 39,
        (byte) 225,
        (byte) 163,
        (byte) 195,
        (byte) 172,
        (byte) 67,
        (byte) 121,
        (byte) 191,
        (byte) 100,
        (byte) 183,
        (byte) 56,
        (byte) 198,
        (byte) 135,
        (byte) 161,
        (byte) 173,
        (byte) 25,
        (byte) 133,
        (byte) 66,
        (byte) 161,
        (byte) 144 /*0x90*/,
        (byte) 127 /*0x7F*/,
        (byte) 219,
        (byte) 187,
        (byte) 8,
        (byte) 54,
        (byte) 229,
        (byte) 188,
        (byte) 214,
        (byte) 159,
        (byte) 69,
        (byte) 214,
        (byte) 234,
        (byte) 22,
        (byte) 17
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 201,
        (byte) 251,
        (byte) 69,
        (byte) 99,
        (byte) 42,
        (byte) 241,
        (byte) 45,
        (byte) 198,
        (byte) 19,
        (byte) 186,
        (byte) 40,
        (byte) 59,
        (byte) 56,
        (byte) 220,
        (byte) 183,
        (byte) 133,
        (byte) 19,
        (byte) 215,
        (byte) 71,
        (byte) 246,
        (byte) 248,
        (byte) 95,
        (byte) 179,
        (byte) 91,
        (byte) 187,
        (byte) 194,
        (byte) 119,
        (byte) 107,
        (byte) 87,
        (byte) 204,
        (byte) 121,
        (byte) 180,
        (byte) 165,
        (byte) 63 /*0x3F*/,
        (byte) 64 /*0x40*/,
        (byte) 152,
        (byte) 246,
        (byte) 37,
        (byte) 91,
        (byte) 67,
        (byte) 131,
        (byte) 86,
        (byte) 91,
        (byte) 233,
        (byte) 248,
        (byte) 211,
        (byte) 250,
        (byte) 248,
        (byte) 185,
        (byte) 216,
        (byte) 7,
        (byte) 202,
        (byte) 34,
        (byte) 253,
        (byte) 183
      };
      byte[] numArray5 = new byte[55];
      numArray5[32 /*0x20*/] = (byte) 146;
      numArray5[41] = (byte) 25;
      numArray5[27] = (byte) 80 /*0x50*/;
      numArray5[3] = (byte) 21;
      numArray5[49] = (byte) 46;
      numArray5[30] = (byte) 175;
      numArray5[6] = (byte) 72;
      numArray5[7] = (byte) 14;
      numArray5[43] = (byte) 95;
      numArray5[9] = (byte) 43;
      numArray5[10] = (byte) 219;
      numArray5[11] = (byte) 58;
      numArray5[52] = (byte) 171;
      numArray5[22] = (byte) 179;
      numArray5[14] = (byte) 250;
      numArray5[0] = (byte) 131;
      numArray5[8] = (byte) 220;
      numArray5[1] = (byte) 184;
      numArray5[18] = (byte) 225;
      numArray5[28] = (byte) 112 /*0x70*/;
      numArray5[20] = (byte) 209;
      numArray5[25] = (byte) 166;
      numArray5[23] = (byte) 6;
      numArray5[12] = (byte) 91;
      numArray5[51] = (byte) 217;
      numArray5[38] = (byte) 109;
      numArray5[29] = (byte) 49;
      numArray5[13] = (byte) 74;
      numArray5[4] = (byte) 111;
      numArray5[16 /*0x10*/] = (byte) 110;
      numArray5[40] = (byte) 109;
      numArray5[31 /*0x1F*/] = (byte) 148;
      numArray5[26] = (byte) 85;
      numArray5[19] = (byte) 62;
      numArray5[34] = (byte) 165;
      numArray5[17] = (byte) 6;
      numArray5[36] = (byte) 237;
      numArray5[44] = (byte) 194;
      numArray5[33] = (byte) 233;
      numArray5[39] = (byte) 144 /*0x90*/;
      numArray5[21] = (byte) 157;
      numArray5[35] = (byte) 90;
      numArray5[15] = (byte) 26;
      numArray5[5] = (byte) 83;
      numArray5[2] = (byte) 104;
      numArray5[50] = (byte) 242;
      numArray5[46] = (byte) 179;
      numArray5[24] = (byte) 8;
      numArray5[48 /*0x30*/] = (byte) 160 /*0xA0*/;
      numArray5[37] = (byte) 62;
      numArray5[47] = (byte) 140;
      numArray5[53] = (byte) 159;
      numArray5[42] = (byte) 184;
      numArray5[45] = (byte) 49;
      numArray5[54] = (byte) 216;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[18] = (byte) 187;
      numArray6[12] = (byte) 206;
      numArray6[4] = (byte) 15;
      numArray6[43] = (byte) 80 /*0x50*/;
      numArray6[24] = (byte) 33;
      numArray6[5] = (byte) 240 /*0xF0*/;
      numArray6[3] = (byte) 251;
      numArray6[13] = (byte) 131;
      numArray6[8] = (byte) 226;
      numArray6[52] = (byte) 89;
      numArray6[10] = (byte) 112 /*0x70*/;
      numArray6[11] = (byte) 196;
      numArray6[42] = (byte) 162;
      numArray6[2] = (byte) 160 /*0xA0*/;
      numArray6[35] = (byte) 187;
      numArray6[20] = (byte) 33;
      numArray6[16 /*0x10*/] = (byte) 58;
      numArray6[41] = (byte) 23;
      numArray6[7] = (byte) 144 /*0x90*/;
      numArray6[28] = (byte) 97;
      numArray6[0] = (byte) 135;
      numArray6[23] = (byte) 198;
      numArray6[22] = (byte) 170;
      numArray6[37] = (byte) 205;
      numArray6[39] = (byte) 247;
      numArray6[40] = (byte) 146;
      numArray6[26] = (byte) 54;
      numArray6[47] = (byte) 197;
      numArray6[53] = (byte) 117;
      numArray6[29] = (byte) 114;
      numArray6[14] = (byte) 113;
      numArray6[31 /*0x1F*/] = (byte) 115;
      numArray6[32 /*0x20*/] = (byte) 112 /*0x70*/;
      numArray6[33] = (byte) 211;
      numArray6[1] = (byte) 192 /*0xC0*/;
      numArray6[46] = (byte) 204;
      numArray6[25] = (byte) 24;
      numArray6[6] = (byte) 129;
      numArray6[38] = (byte) 178;
      numArray6[21] = (byte) 221;
      numArray6[34] = (byte) 132;
      numArray6[54] = (byte) 92;
      numArray6[27] = (byte) 171;
      numArray6[19] = (byte) 239;
      numArray6[36] = (byte) 82;
      numArray6[45] = (byte) 152;
      numArray6[44] = (byte) 144 /*0x90*/;
      numArray6[15] = (byte) 138;
      numArray6[48 /*0x30*/] = (byte) 167;
      numArray6[49] = (byte) 195;
      numArray6[50] = (byte) 228;
      numArray6[51] = (byte) 216;
      numArray6[30] = (byte) 30;
      numArray6[17] = (byte) 177;
      numArray6[9] = (byte) 236;
      byte[] numArray7 = new byte[55]
      {
        (byte) 150,
        (byte) 168,
        (byte) 18,
        (byte) 247,
        (byte) 87,
        (byte) 72,
        (byte) 119,
        (byte) 76,
        (byte) 174,
        (byte) 241,
        (byte) 59,
        (byte) 23,
        (byte) 183,
        (byte) 79,
        (byte) 212,
        (byte) 86,
        (byte) 237,
        (byte) 247,
        (byte) 52,
        (byte) 217,
        (byte) 28,
        (byte) 88,
        (byte) 216,
        (byte) 88,
        (byte) 195,
        (byte) 159,
        (byte) 235,
        (byte) 85,
        (byte) 122,
        (byte) 241,
        (byte) 210,
        (byte) 185,
        (byte) 2,
        (byte) 207,
        (byte) 88,
        (byte) 175,
        (byte) 136,
        (byte) 39,
        (byte) 169,
        (byte) 172,
        (byte) 84,
        (byte) 45,
        (byte) 36,
        (byte) 61,
        (byte) 67,
        (byte) 82,
        (byte) 219,
        (byte) 76,
        (byte) 99,
        (byte) 136,
        (byte) 192 /*0xC0*/,
        (byte) 31 /*0x1F*/,
        (byte) 93,
        (byte) 32 /*0x20*/,
        (byte) 126
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 165,
        (byte) 82,
        (byte) 170,
        (byte) 73,
        (byte) 209,
        (byte) 144 /*0x90*/,
        (byte) 224 /*0xE0*/,
        (byte) 136,
        (byte) 228,
        (byte) 88,
        (byte) 131,
        (byte) 35,
        (byte) 16 /*0x10*/,
        (byte) 191,
        (byte) 121,
        (byte) 66,
        (byte) 135,
        (byte) 159,
        (byte) 33,
        (byte) 7,
        (byte) 214,
        (byte) 161,
        (byte) 66,
        (byte) 122,
        (byte) 169,
        (byte) 18,
        (byte) 18,
        (byte) 23,
        (byte) 107,
        (byte) 132,
        (byte) 226,
        (byte) 36,
        (byte) 108,
        (byte) 231,
        (byte) 210,
        (byte) 160 /*0xA0*/,
        (byte) 17,
        (byte) 244,
        (byte) 235,
        (byte) 87,
        (byte) 83,
        (byte) 250,
        (byte) 175,
        (byte) 236,
        (byte) 77,
        (byte) 163,
        (byte) 224 /*0xE0*/,
        (byte) 204,
        (byte) 253,
        (byte) 151,
        (byte) 14,
        (byte) 120,
        (byte) 49,
        (byte) 103,
        (byte) 230
      };
      byte[] numArray9 = new byte[55];
      numArray9[0] = (byte) 162;
      numArray9[5] = (byte) 216;
      numArray9[30] = (byte) 92;
      numArray9[37] = (byte) 208 /*0xD0*/;
      numArray9[4] = (byte) 218;
      numArray9[20] = (byte) 170;
      numArray9[44] = (byte) 215;
      numArray9[7] = (byte) 121;
      numArray9[8] = (byte) 69;
      numArray9[17] = (byte) 239;
      numArray9[10] = (byte) 252;
      numArray9[11] = (byte) 253;
      numArray9[40] = (byte) 220;
      numArray9[43] = (byte) 109;
      numArray9[6] = (byte) 221;
      numArray9[15] = (byte) 138;
      numArray9[16 /*0x10*/] = (byte) 206;
      numArray9[3] = (byte) 13;
      numArray9[14] = (byte) 65;
      numArray9[49] = (byte) 221;
      numArray9[13] = (byte) 148;
      numArray9[45] = (byte) 111;
      numArray9[26] = (byte) 108;
      numArray9[46] = (byte) 62;
      numArray9[18] = (byte) 206;
      numArray9[25] = (byte) 174;
      numArray9[42] = (byte) 29;
      numArray9[27] = (byte) 66;
      numArray9[28] = (byte) 213;
      numArray9[29] = (byte) 53;
      numArray9[53] = (byte) 113;
      numArray9[31 /*0x1F*/] = (byte) 68;
      numArray9[32 /*0x20*/] = (byte) 183;
      numArray9[12] = (byte) 238;
      numArray9[34] = (byte) 75;
      numArray9[35] = (byte) 218;
      numArray9[24] = (byte) 147;
      numArray9[2] = (byte) 242;
      numArray9[52] = (byte) 113;
      numArray9[41] = (byte) 211;
      numArray9[54] = (byte) 244;
      numArray9[50] = (byte) 118;
      numArray9[33] = (byte) 4;
      numArray9[23] = (byte) 62;
      numArray9[21] = (byte) 97;
      numArray9[51] = (byte) 56;
      numArray9[39] = (byte) 163;
      numArray9[47] = (byte) 92;
      numArray9[48 /*0x30*/] = (byte) 248;
      numArray9[38] = (byte) 77;
      numArray9[19] = (byte) 182;
      numArray9[36] = (byte) 34;
      numArray9[9] = (byte) 178;
      numArray9[22] = (byte) 117;
      numArray9[1] = (byte) 126;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 2,
        (byte) 152,
        (byte) 140,
        (byte) 55,
        (byte) 174,
        (byte) 71,
        (byte) 233,
        (byte) 13,
        (byte) 106,
        (byte) 178,
        (byte) 39,
        (byte) 28,
        (byte) 72,
        (byte) 79,
        (byte) 62,
        (byte) 135,
        (byte) 239,
        (byte) 41,
        (byte) 254,
        (byte) 163,
        (byte) 223,
        (byte) 61,
        (byte) 26,
        (byte) 122,
        (byte) 103,
        (byte) 246,
        (byte) 109,
        (byte) 251,
        (byte) 164,
        (byte) 139,
        (byte) 81,
        (byte) 247,
        (byte) 191,
        (byte) 214,
        (byte) 222,
        (byte) 188,
        (byte) 161,
        (byte) 162,
        (byte) 219,
        (byte) 149,
        (byte) 101,
        (byte) 104,
        (byte) 132,
        (byte) 57,
        (byte) 144 /*0x90*/,
        (byte) 194,
        (byte) 218,
        (byte) 23,
        (byte) 60,
        (byte) 61,
        (byte) 120,
        (byte) 137,
        (byte) 10,
        (byte) 108,
        (byte) 218
      };
      byte[] numArray11 = new byte[55];
      numArray11[7] = (byte) 123;
      numArray11[53] = (byte) 17;
      numArray11[4] = (byte) 66;
      numArray11[3] = (byte) 123;
      numArray11[15] = (byte) 27;
      numArray11[47] = (byte) 190;
      numArray11[37] = (byte) 25;
      numArray11[41] = (byte) 127 /*0x7F*/;
      numArray11[11] = (byte) 131;
      numArray11[2] = (byte) 77;
      numArray11[48 /*0x30*/] = (byte) 176 /*0xB0*/;
      numArray11[18] = (byte) 129;
      numArray11[46] = (byte) 132;
      numArray11[33] = (byte) 143;
      numArray11[14] = (byte) 24;
      numArray11[40] = (byte) 6;
      numArray11[38] = (byte) 114;
      numArray11[13] = (byte) 214;
      numArray11[54] = (byte) 180;
      numArray11[19] = (byte) 96 /*0x60*/;
      numArray11[20] = (byte) 56;
      numArray11[21] = (byte) 164;
      numArray11[22] = (byte) 170;
      numArray11[17] = (byte) 87;
      numArray11[32 /*0x20*/] = (byte) 112 /*0x70*/;
      numArray11[25] = (byte) 226;
      numArray11[26] = (byte) 209;
      numArray11[35] = (byte) 57;
      numArray11[24] = (byte) 21;
      numArray11[29] = (byte) 225;
      numArray11[30] = (byte) 193;
      numArray11[31 /*0x1F*/] = (byte) 54;
      numArray11[5] = (byte) 25;
      numArray11[36] = (byte) 12;
      numArray11[34] = (byte) 208 /*0xD0*/;
      numArray11[44] = (byte) 66;
      numArray11[6] = (byte) 120;
      numArray11[52] = (byte) 223;
      numArray11[0] = (byte) 50;
      numArray11[39] = (byte) 93;
      numArray11[1] = (byte) 165;
      numArray11[16 /*0x10*/] = (byte) 4;
      numArray11[42] = (byte) 134;
      numArray11[43] = (byte) 36;
      numArray11[10] = (byte) 230;
      numArray11[45] = (byte) 55;
      numArray11[9] = (byte) 239;
      numArray11[28] = (byte) 26;
      numArray11[8] = (byte) 235;
      numArray11[49] = (byte) 171;
      numArray11[50] = (byte) 123;
      numArray11[51] = (byte) 21;
      numArray11[23] = (byte) 232;
      numArray11[27] = (byte) 58;
      numArray11[12] = (byte) 174;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[12]
      {
        (byte) 39,
        (byte) 43,
        (byte) 156,
        (byte) 131,
        (byte) 48 /*0x30*/,
        (byte) 2,
        (byte) 203,
        (byte) 153,
        (byte) 67,
        (byte) 10,
        (byte) 117,
        (byte) 87
      };
      byte[] numArray13 = new byte[12];
      numArray13[10] = (byte) 60;
      numArray13[1] = (byte) 245;
      numArray13[5] = (byte) 9;
      numArray13[0] = (byte) 30;
      numArray13[6] = (byte) 159;
      numArray13[3] = (byte) 244;
      numArray13[8] = (byte) 242;
      numArray13[7] = (byte) 149;
      numArray13[4] = (byte) 149;
      numArray13[9] = (byte) 124;
      numArray13[2] = (byte) 127 /*0x7F*/;
      numArray13[11] = (byte) 70;
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 275] ^= numArray13[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray14 = new byte[287];
    byte[] numArray15 = new byte[55]
    {
      (byte) 231,
      (byte) 139,
      (byte) 74,
      (byte) 37,
      (byte) 153,
      (byte) 13,
      (byte) 162,
      (byte) 104,
      (byte) 167,
      (byte) 134,
      (byte) 56,
      (byte) 69,
      (byte) 253,
      (byte) 218,
      (byte) 62,
      (byte) 28,
      (byte) 91,
      (byte) 243,
      (byte) 79,
      (byte) 118,
      (byte) 0,
      (byte) 177,
      (byte) 17,
      (byte) 244,
      (byte) 101,
      (byte) 235,
      (byte) 248,
      (byte) 146,
      (byte) 156,
      (byte) 235,
      (byte) 234,
      (byte) 211,
      (byte) 44,
      (byte) 243,
      (byte) 228,
      (byte) 13,
      (byte) 248,
      (byte) 228,
      (byte) 219,
      (byte) 73,
      (byte) 161,
      (byte) 238,
      (byte) 210,
      (byte) 163,
      (byte) 117,
      (byte) 130,
      (byte) 122,
      (byte) 57,
      (byte) 202,
      (byte) 109,
      (byte) 228,
      (byte) 190,
      (byte) 97,
      (byte) 141,
      (byte) 14
    };
    byte[] numArray16 = new byte[55];
    numArray16[44] = (byte) 51;
    numArray16[1] = (byte) 21;
    numArray16[6] = (byte) 252;
    numArray16[26] = (byte) 192 /*0xC0*/;
    numArray16[40] = (byte) 132;
    numArray16[47] = (byte) 199;
    numArray16[2] = (byte) 28;
    numArray16[9] = (byte) 188;
    numArray16[53] = (byte) 111;
    numArray16[22] = (byte) 45;
    numArray16[7] = (byte) 148;
    numArray16[11] = (byte) 73;
    numArray16[32 /*0x20*/] = (byte) 161;
    numArray16[46] = (byte) 10;
    numArray16[14] = (byte) 161;
    numArray16[12] = (byte) 103;
    numArray16[16 /*0x10*/] = (byte) 13;
    numArray16[17] = (byte) 183;
    numArray16[18] = (byte) 6;
    numArray16[3] = (byte) 8;
    numArray16[10] = (byte) 84;
    numArray16[24] = (byte) 152;
    numArray16[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    numArray16[23] = (byte) 34;
    numArray16[8] = (byte) 21;
    numArray16[25] = (byte) 136;
    numArray16[49] = (byte) 232;
    numArray16[19] = (byte) 149;
    numArray16[28] = (byte) 10;
    numArray16[29] = (byte) 141;
    numArray16[27] = (byte) 225;
    numArray16[39] = (byte) 202;
    numArray16[41] = (byte) 12;
    numArray16[15] = (byte) 197;
    numArray16[0] = (byte) 13;
    numArray16[35] = (byte) 219;
    numArray16[36] = (byte) 114;
    numArray16[37] = (byte) 177;
    numArray16[38] = (byte) 130;
    numArray16[13] = (byte) 74;
    numArray16[20] = (byte) 2;
    numArray16[30] = (byte) 41;
    numArray16[42] = (byte) 217;
    numArray16[43] = (byte) 150;
    numArray16[34] = (byte) 92;
    numArray16[45] = (byte) 43;
    numArray16[4] = (byte) 108;
    numArray16[54] = (byte) 148;
    numArray16[48 /*0x30*/] = (byte) 73;
    numArray16[21] = (byte) 165;
    numArray16[50] = (byte) 205;
    numArray16[51] = (byte) 151;
    numArray16[52] = (byte) 20;
    numArray16[33] = (byte) 51;
    numArray16[5] = (byte) 158;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray14, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 215,
      (byte) 219,
      (byte) 60,
      (byte) 84,
      (byte) 71,
      (byte) 156,
      (byte) 181,
      (byte) 113,
      (byte) 243,
      (byte) 121,
      (byte) 37,
      (byte) 70,
      (byte) 156,
      (byte) 129,
      (byte) 80 /*0x50*/,
      (byte) 233,
      (byte) 208 /*0xD0*/,
      (byte) 65,
      (byte) 238,
      (byte) 199,
      (byte) 103,
      (byte) 111,
      (byte) 122,
      (byte) 0,
      (byte) 124,
      (byte) 46,
      (byte) 229,
      (byte) 152,
      (byte) 132,
      (byte) 22,
      (byte) 230,
      (byte) 232,
      (byte) 88,
      (byte) 136,
      (byte) 203,
      (byte) 73,
      (byte) 241,
      (byte) 224 /*0xE0*/,
      (byte) 75,
      (byte) 135,
      (byte) 98,
      (byte) 149,
      (byte) 28,
      (byte) 239,
      (byte) 75,
      (byte) 253,
      (byte) 252,
      (byte) 199,
      (byte) 29,
      (byte) 189,
      (byte) 139,
      (byte) 119,
      (byte) 13,
      (byte) 143,
      (byte) 164
    };
    byte[] numArray18 = new byte[55];
    numArray18[32 /*0x20*/] = (byte) 30;
    numArray18[6] = (byte) 152;
    numArray18[18] = (byte) 94;
    numArray18[42] = (byte) 0;
    numArray18[4] = (byte) 207;
    numArray18[5] = (byte) 102;
    numArray18[43] = (byte) 12;
    numArray18[7] = (byte) 138;
    numArray18[27] = (byte) 100;
    numArray18[45] = (byte) 46;
    numArray18[10] = (byte) 114;
    numArray18[11] = (byte) 136;
    numArray18[49] = (byte) 77;
    numArray18[13] = (byte) 242;
    numArray18[51] = (byte) 18;
    numArray18[35] = (byte) 27;
    numArray18[16 /*0x10*/] = (byte) 23;
    numArray18[23] = (byte) 92;
    numArray18[26] = (byte) 93;
    numArray18[19] = (byte) 247;
    numArray18[36] = (byte) 220;
    numArray18[21] = (byte) 250;
    numArray18[22] = (byte) 21;
    numArray18[9] = (byte) 243;
    numArray18[50] = (byte) 246;
    numArray18[14] = (byte) 95;
    numArray18[29] = (byte) 43;
    numArray18[15] = (byte) 58;
    numArray18[28] = (byte) 67;
    numArray18[20] = (byte) 149;
    numArray18[1] = (byte) 116;
    numArray18[31 /*0x1F*/] = (byte) 252;
    numArray18[52] = (byte) 230;
    numArray18[33] = (byte) 226;
    numArray18[34] = (byte) 113;
    numArray18[0] = (byte) 193;
    numArray18[30] = (byte) 105;
    numArray18[37] = (byte) 241;
    numArray18[38] = (byte) 140;
    numArray18[39] = (byte) 223;
    numArray18[40] = (byte) 24;
    numArray18[41] = (byte) 70;
    numArray18[8] = (byte) 252;
    numArray18[3] = (byte) 117;
    numArray18[44] = (byte) 104;
    numArray18[54] = (byte) 184;
    numArray18[46] = (byte) 206;
    numArray18[47] = (byte) 152;
    numArray18[25] = (byte) 19;
    numArray18[17] = (byte) 108;
    numArray18[12] = (byte) 34;
    numArray18[48 /*0x30*/] = (byte) 211;
    numArray18[2] = (byte) 167;
    numArray18[53] = (byte) 87;
    numArray18[24] = (byte) 11;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray14, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 55] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[45] = (byte) 189;
    numArray19[1] = (byte) 89;
    numArray19[2] = (byte) 231;
    numArray19[3] = (byte) 152;
    numArray19[13] = (byte) 65;
    numArray19[52] = (byte) 170;
    numArray19[42] = (byte) 171;
    numArray19[5] = (byte) 37;
    numArray19[25] = (byte) 99;
    numArray19[9] = (byte) 35;
    numArray19[24] = (byte) 13;
    numArray19[11] = (byte) 83;
    numArray19[12] = (byte) 30;
    numArray19[35] = (byte) 174;
    numArray19[39] = (byte) 131;
    numArray19[15] = (byte) 196;
    numArray19[10] = (byte) 49;
    numArray19[18] = (byte) 40;
    numArray19[14] = (byte) 232;
    numArray19[6] = (byte) 81;
    numArray19[20] = (byte) 46;
    numArray19[21] = (byte) 167;
    numArray19[22] = (byte) 111;
    numArray19[23] = (byte) 73;
    numArray19[44] = (byte) 182;
    numArray19[8] = (byte) 98;
    numArray19[34] = (byte) 48 /*0x30*/;
    numArray19[17] = (byte) 32 /*0x20*/;
    numArray19[28] = (byte) 239;
    numArray19[7] = (byte) 36;
    numArray19[30] = (byte) 160 /*0xA0*/;
    numArray19[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    numArray19[32 /*0x20*/] = (byte) 79;
    numArray19[0] = (byte) 133;
    numArray19[26] = (byte) 1;
    numArray19[43] = (byte) 235;
    numArray19[36] = (byte) 74;
    numArray19[37] = (byte) 56;
    numArray19[38] = (byte) 163;
    numArray19[46] = (byte) 249;
    numArray19[40] = (byte) 237;
    numArray19[41] = (byte) 55;
    numArray19[19] = (byte) 19;
    numArray19[33] = (byte) 17;
    numArray19[29] = (byte) 51;
    numArray19[4] = (byte) 161;
    numArray19[27] = (byte) 240 /*0xF0*/;
    numArray19[47] = (byte) 131;
    numArray19[48 /*0x30*/] = (byte) 178;
    numArray19[49] = (byte) 187;
    numArray19[50] = (byte) 16 /*0x10*/;
    numArray19[51] = (byte) 9;
    numArray19[16 /*0x10*/] = (byte) 149;
    numArray19[53] = (byte) 0;
    numArray19[54] = (byte) 168;
    byte[] numArray20 = new byte[55]
    {
      (byte) 26,
      (byte) 243,
      (byte) 119,
      (byte) 72,
      (byte) 240 /*0xF0*/,
      (byte) 31 /*0x1F*/,
      (byte) 114,
      (byte) 216,
      (byte) 105,
      (byte) 179,
      (byte) 167,
      (byte) 185,
      (byte) 48 /*0x30*/,
      (byte) 26,
      (byte) 95,
      (byte) 69,
      (byte) 161,
      (byte) 154,
      (byte) 170,
      (byte) 222,
      (byte) 40,
      (byte) 190,
      (byte) 233,
      (byte) 109,
      (byte) 22,
      (byte) 17,
      (byte) 24,
      (byte) 42,
      byte.MaxValue,
      (byte) 159,
      (byte) 113,
      (byte) 133,
      (byte) 171,
      (byte) 162,
      (byte) 228,
      (byte) 109,
      (byte) 135,
      (byte) 104,
      (byte) 45,
      (byte) 85,
      (byte) 48 /*0x30*/,
      (byte) 249,
      (byte) 16 /*0x10*/,
      (byte) 120,
      (byte) 47,
      (byte) 168,
      (byte) 120,
      (byte) 13,
      (byte) 109,
      (byte) 175,
      (byte) 24,
      (byte) 10,
      (byte) 139,
      (byte) 61,
      (byte) 98
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray14, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 110] ^= numArray20[index];
    byte[] numArray21 = new byte[55]
    {
      (byte) 11,
      (byte) 59,
      (byte) 212,
      (byte) 165,
      (byte) 36,
      (byte) 1,
      (byte) 12,
      (byte) 236,
      (byte) 188,
      (byte) 181,
      (byte) 164,
      (byte) 191,
      (byte) 116,
      (byte) 205,
      (byte) 176 /*0xB0*/,
      (byte) 23,
      (byte) 69,
      (byte) 154,
      (byte) 244,
      (byte) 126,
      (byte) 26,
      (byte) 253,
      (byte) 149,
      (byte) 125,
      (byte) 11,
      (byte) 196,
      (byte) 242,
      (byte) 111,
      (byte) 9,
      (byte) 95,
      (byte) 115,
      (byte) 182,
      (byte) 109,
      (byte) 93,
      (byte) 160 /*0xA0*/,
      (byte) 113,
      (byte) 143,
      (byte) 185,
      (byte) 103,
      (byte) 0,
      (byte) 112 /*0x70*/,
      (byte) 123,
      (byte) 217,
      (byte) 3,
      (byte) 71,
      (byte) 179,
      (byte) 4,
      (byte) 115,
      (byte) 3,
      (byte) 117,
      (byte) 192 /*0xC0*/,
      (byte) 63 /*0x3F*/,
      (byte) 189,
      (byte) 101,
      (byte) 76
    };
    byte[] numArray22 = new byte[55];
    numArray22[50] = (byte) 109;
    numArray22[14] = (byte) 123;
    numArray22[2] = (byte) 230;
    numArray22[3] = (byte) 104;
    numArray22[4] = (byte) 107;
    numArray22[5] = (byte) 162;
    numArray22[6] = (byte) 161;
    numArray22[15] = (byte) 159;
    numArray22[8] = (byte) 42;
    numArray22[51] = (byte) 147;
    numArray22[10] = (byte) 241;
    numArray22[9] = (byte) 211;
    numArray22[12] = (byte) 254;
    numArray22[13] = (byte) 51;
    numArray22[37] = (byte) 19;
    numArray22[27] = (byte) 22;
    numArray22[1] = (byte) 48 /*0x30*/;
    numArray22[17] = (byte) 106;
    numArray22[18] = (byte) 191;
    numArray22[28] = (byte) 182;
    numArray22[20] = (byte) 185;
    numArray22[25] = (byte) 199;
    numArray22[22] = (byte) 218;
    numArray22[47] = (byte) 182;
    numArray22[23] = (byte) 235;
    numArray22[31 /*0x1F*/] = (byte) 238;
    numArray22[38] = (byte) 72;
    numArray22[32 /*0x20*/] = (byte) 79;
    numArray22[34] = (byte) 175;
    numArray22[29] = (byte) 47;
    numArray22[0] = (byte) 226;
    numArray22[33] = (byte) 222;
    numArray22[40] = (byte) 73;
    numArray22[30] = (byte) 127 /*0x7F*/;
    numArray22[7] = (byte) 28;
    numArray22[35] = (byte) 0;
    numArray22[36] = (byte) 36;
    numArray22[54] = (byte) 25;
    numArray22[52] = (byte) 35;
    numArray22[39] = (byte) 161;
    numArray22[11] = (byte) 78;
    numArray22[41] = (byte) 197;
    numArray22[45] = (byte) 134;
    numArray22[21] = (byte) 100;
    numArray22[44] = (byte) 100;
    numArray22[42] = (byte) 59;
    numArray22[46] = (byte) 112 /*0x70*/;
    numArray22[16 /*0x10*/] = (byte) 40;
    numArray22[48 /*0x30*/] = (byte) 69;
    numArray22[26] = (byte) 193;
    numArray22[49] = (byte) 145;
    numArray22[19] = (byte) 218;
    numArray22[24] = (byte) 58;
    numArray22[43] = (byte) 120;
    numArray22[53] = (byte) 231;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray14, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 165] ^= numArray22[index];
    byte[] numArray23 = new byte[55]
    {
      (byte) 32 /*0x20*/,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 145,
      (byte) 49,
      (byte) 78,
      (byte) 189,
      (byte) 13,
      (byte) 244,
      (byte) 73,
      (byte) 6,
      (byte) 154,
      (byte) 115,
      (byte) 78,
      (byte) 108,
      (byte) 43,
      (byte) 2,
      (byte) 59,
      (byte) 6,
      (byte) 167,
      (byte) 207,
      (byte) 220,
      (byte) 246,
      (byte) 211,
      (byte) 55,
      (byte) 167,
      (byte) 166,
      (byte) 107,
      (byte) 39,
      (byte) 47,
      (byte) 177,
      (byte) 100,
      (byte) 167,
      (byte) 183,
      (byte) 24,
      (byte) 221,
      (byte) 103,
      (byte) 95,
      (byte) 160 /*0xA0*/,
      (byte) 74,
      (byte) 190,
      (byte) 180,
      (byte) 100,
      (byte) 24,
      (byte) 230,
      (byte) 172,
      (byte) 80 /*0x50*/,
      (byte) 158,
      (byte) 199,
      (byte) 224 /*0xE0*/,
      (byte) 199,
      (byte) 27,
      (byte) 165,
      (byte) 67,
      (byte) 68
    };
    byte[] numArray24 = new byte[55]
    {
      (byte) 169,
      (byte) 253,
      (byte) 8,
      (byte) 237,
      (byte) 209,
      (byte) 61,
      (byte) 9,
      (byte) 138,
      (byte) 177,
      (byte) 29,
      (byte) 184,
      (byte) 43,
      (byte) 99,
      (byte) 118,
      (byte) 48 /*0x30*/,
      (byte) 154,
      (byte) 244,
      (byte) 234,
      (byte) 217,
      (byte) 192 /*0xC0*/,
      (byte) 123,
      (byte) 49,
      (byte) 151,
      (byte) 169,
      (byte) 239,
      (byte) 249,
      (byte) 172,
      (byte) 106,
      (byte) 84,
      (byte) 221,
      (byte) 200,
      (byte) 226,
      (byte) 171,
      (byte) 61,
      (byte) 181,
      (byte) 209,
      (byte) 47,
      (byte) 25,
      (byte) 130,
      (byte) 189,
      (byte) 194,
      (byte) 243,
      (byte) 93,
      (byte) 231,
      (byte) 218,
      (byte) 180,
      (byte) 224 /*0xE0*/,
      (byte) 104,
      (byte) 169,
      (byte) 47,
      (byte) 135,
      (byte) 39,
      (byte) 251,
      (byte) 207,
      (byte) 138
    };
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray14, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 220] ^= numArray24[index];
    byte[] numArray25 = new byte[12]
    {
      (byte) 205,
      (byte) 89,
      (byte) 137,
      (byte) 167,
      (byte) 82,
      (byte) 230,
      (byte) 197,
      (byte) 151,
      (byte) 161,
      (byte) 242,
      (byte) 187,
      (byte) 54
    };
    byte[] numArray26 = new byte[12];
    numArray26[0] = (byte) 92;
    numArray26[1] = (byte) 60;
    numArray26[6] = (byte) 54;
    numArray26[4] = (byte) 232;
    numArray26[10] = (byte) 84;
    numArray26[5] = (byte) 14;
    numArray26[8] = (byte) 68;
    numArray26[2] = (byte) 59;
    numArray26[7] = (byte) 173;
    numArray26[9] = (byte) 44;
    numArray26[3] = (byte) 177;
    numArray26[11] = (byte) 243;
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray14, 275, 12);
    for (int index = 0; index < 12; ++index)
      numArray14[index + 275] ^= numArray26[index];
    return Encoding.UTF8.GetString(numArray14);
  }

  internal static string ssp_appserver_13461()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 149,
        (byte) 190,
        (byte) 100,
        (byte) 115,
        (byte) 212,
        (byte) 146,
        (byte) 154,
        (byte) 5,
        (byte) 23,
        (byte) 162,
        (byte) 108,
        (byte) 206,
        (byte) 159,
        (byte) 128 /*0x80*/,
        (byte) 69,
        (byte) 185,
        (byte) 148,
        (byte) 104,
        (byte) 56,
        (byte) 27,
        (byte) 35,
        (byte) 129,
        (byte) 233,
        (byte) 90,
        (byte) 40,
        (byte) 204,
        (byte) 109,
        (byte) 224 /*0xE0*/,
        (byte) 206,
        (byte) 50,
        (byte) 159,
        (byte) 175,
        (byte) 158,
        (byte) 93,
        (byte) 2,
        (byte) 226,
        (byte) 142,
        (byte) 72,
        (byte) 98,
        (byte) 252,
        (byte) 42,
        (byte) 157
      };
      byte[] numArray3 = new byte[42]
      {
        (byte) 118,
        (byte) 248,
        (byte) 135,
        (byte) 164,
        (byte) 179,
        (byte) 87,
        (byte) 208 /*0xD0*/,
        (byte) 151,
        (byte) 2,
        (byte) 112 /*0x70*/,
        (byte) 125,
        (byte) 142,
        (byte) 40,
        (byte) 3,
        (byte) 183,
        (byte) 191,
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 27,
        (byte) 152,
        (byte) 173,
        (byte) 49,
        (byte) 136,
        (byte) 94,
        (byte) 209,
        (byte) 147,
        (byte) 63 /*0x3F*/,
        (byte) 14,
        (byte) 74,
        (byte) 92,
        (byte) 43,
        (byte) 208 /*0xD0*/,
        (byte) 187,
        (byte) 98,
        (byte) 172,
        (byte) 77,
        (byte) 90,
        (byte) 115,
        (byte) 22,
        (byte) 119,
        (byte) 36,
        (byte) 51
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42];
    numArray5[5] = (byte) 194;
    numArray5[1] = (byte) 216;
    numArray5[28] = (byte) 59;
    numArray5[3] = (byte) 167;
    numArray5[34] = (byte) 219;
    numArray5[20] = (byte) 189;
    numArray5[6] = (byte) 159;
    numArray5[7] = (byte) 106;
    numArray5[36] = (byte) 15;
    numArray5[9] = (byte) 68;
    numArray5[25] = (byte) 43;
    numArray5[17] = (byte) 55;
    numArray5[19] = (byte) 3;
    numArray5[40] = byte.MaxValue;
    numArray5[4] = (byte) 210;
    numArray5[15] = (byte) 37;
    numArray5[16 /*0x10*/] = (byte) 185;
    numArray5[27] = (byte) 5;
    numArray5[38] = (byte) 252;
    numArray5[0] = (byte) 76;
    numArray5[10] = (byte) 20;
    numArray5[21] = (byte) 47;
    numArray5[13] = (byte) 74;
    numArray5[23] = (byte) 126;
    numArray5[26] = (byte) 76;
    numArray5[22] = (byte) 65;
    numArray5[18] = (byte) 228;
    numArray5[29] = (byte) 241;
    numArray5[24] = (byte) 98;
    numArray5[12] = (byte) 9;
    numArray5[11] = (byte) 213;
    numArray5[31 /*0x1F*/] = (byte) 253;
    numArray5[32 /*0x20*/] = (byte) 207;
    numArray5[33] = (byte) 222;
    numArray5[39] = (byte) 111;
    numArray5[14] = (byte) 7;
    numArray5[2] = (byte) 93;
    numArray5[37] = (byte) 57;
    numArray5[30] = (byte) 134;
    numArray5[8] = (byte) 43;
    numArray5[35] = (byte) 243;
    numArray5[41] = (byte) 68;
    byte[] numArray6 = new byte[42]
    {
      (byte) 113,
      (byte) 112 /*0x70*/,
      (byte) 10,
      (byte) 227,
      (byte) 247,
      (byte) 120,
      (byte) 42,
      (byte) 91,
      (byte) 65,
      (byte) 141,
      (byte) 38,
      (byte) 161,
      (byte) 65,
      (byte) 111,
      (byte) 176 /*0xB0*/,
      (byte) 153,
      (byte) 160 /*0xA0*/,
      (byte) 164,
      (byte) 170,
      (byte) 20,
      (byte) 207,
      (byte) 184,
      (byte) 103,
      (byte) 91,
      (byte) 50,
      (byte) 235,
      (byte) 196,
      (byte) 29,
      (byte) 218,
      (byte) 46,
      (byte) 100,
      (byte) 185,
      (byte) 197,
      (byte) 38,
      (byte) 15,
      (byte) 209,
      (byte) 90,
      (byte) 144 /*0x90*/,
      (byte) 51,
      (byte) 32 /*0x20*/,
      (byte) 221,
      (byte) 116
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13462()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[85];
      byte[] numArray2 = new byte[55]
      {
        (byte) 31 /*0x1F*/,
        (byte) 190,
        (byte) 92,
        (byte) 48 /*0x30*/,
        (byte) 244,
        (byte) 85,
        (byte) 249,
        (byte) 102,
        (byte) 21,
        (byte) 58,
        (byte) 91,
        (byte) 214,
        (byte) 6,
        (byte) 90,
        (byte) 143,
        (byte) 70,
        (byte) 193,
        (byte) 241,
        (byte) 42,
        (byte) 42,
        (byte) 19,
        (byte) 70,
        (byte) 97,
        (byte) 110,
        (byte) 77,
        (byte) 239,
        (byte) 0,
        (byte) 40,
        (byte) 238,
        (byte) 202,
        (byte) 185,
        (byte) 157,
        (byte) 184,
        (byte) 78,
        (byte) 10,
        (byte) 176 /*0xB0*/,
        (byte) 183,
        (byte) 229,
        (byte) 139,
        (byte) 230,
        (byte) 123,
        (byte) 157,
        (byte) 99,
        (byte) 129,
        (byte) 204,
        (byte) 186,
        (byte) 78,
        (byte) 76,
        (byte) 190,
        (byte) 101,
        (byte) 162,
        (byte) 142,
        (byte) 245,
        (byte) 52,
        (byte) 164
      };
      byte[] numArray3 = new byte[55];
      numArray3[28] = (byte) 134;
      numArray3[1] = (byte) 176 /*0xB0*/;
      numArray3[16 /*0x10*/] = (byte) 119;
      numArray3[3] = (byte) 198;
      numArray3[6] = (byte) 73;
      numArray3[41] = (byte) 180;
      numArray3[45] = (byte) 14;
      numArray3[5] = (byte) 82;
      numArray3[8] = (byte) 140;
      numArray3[39] = (byte) 88;
      numArray3[10] = (byte) 102;
      numArray3[42] = (byte) 68;
      numArray3[44] = (byte) 101;
      numArray3[13] = (byte) 164;
      numArray3[14] = (byte) 215;
      numArray3[15] = (byte) 38;
      numArray3[35] = (byte) 48 /*0x30*/;
      numArray3[17] = (byte) 162;
      numArray3[18] = (byte) 155;
      numArray3[48 /*0x30*/] = (byte) 254;
      numArray3[20] = (byte) 37;
      numArray3[47] = (byte) 84;
      numArray3[19] = (byte) 164;
      numArray3[23] = (byte) 125;
      numArray3[12] = (byte) 132;
      numArray3[25] = (byte) 202;
      numArray3[26] = (byte) 19;
      numArray3[27] = (byte) 156;
      numArray3[4] = (byte) 114;
      numArray3[11] = (byte) 5;
      numArray3[30] = (byte) 136;
      numArray3[31 /*0x1F*/] = (byte) 98;
      numArray3[36] = (byte) 152;
      numArray3[33] = (byte) 140;
      numArray3[32 /*0x20*/] = (byte) 39;
      numArray3[22] = (byte) 96 /*0x60*/;
      numArray3[24] = (byte) 187;
      numArray3[21] = (byte) 25;
      numArray3[38] = (byte) 199;
      numArray3[2] = (byte) 16 /*0x10*/;
      numArray3[29] = (byte) 1;
      numArray3[50] = (byte) 79;
      numArray3[34] = (byte) 213;
      numArray3[43] = (byte) 176 /*0xB0*/;
      numArray3[40] = (byte) 223;
      numArray3[7] = (byte) 214;
      numArray3[46] = (byte) 156;
      numArray3[37] = (byte) 108;
      numArray3[53] = (byte) 61;
      numArray3[49] = (byte) 76;
      numArray3[0] = (byte) 23;
      numArray3[9] = (byte) 58;
      numArray3[52] = (byte) 97;
      numArray3[51] = (byte) 39;
      numArray3[54] = (byte) 189;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30]
      {
        (byte) 75,
        (byte) 80 /*0x50*/,
        (byte) 98,
        (byte) 161,
        (byte) 103,
        (byte) 238,
        (byte) 33,
        (byte) 85,
        (byte) 23,
        (byte) 153,
        (byte) 15,
        (byte) 29,
        (byte) 140,
        (byte) 196,
        (byte) 218,
        (byte) 223,
        (byte) 179,
        (byte) 5,
        (byte) 80 /*0x50*/,
        (byte) 202,
        (byte) 223,
        (byte) 148,
        (byte) 245,
        (byte) 12,
        (byte) 182,
        (byte) 137,
        (byte) 178,
        (byte) 99,
        (byte) 1,
        (byte) 246
      };
      byte[] numArray5 = new byte[30]
      {
        (byte) 234,
        (byte) 190,
        (byte) 222,
        (byte) 176 /*0xB0*/,
        (byte) 129,
        (byte) 212,
        (byte) 38,
        (byte) 232,
        (byte) 129,
        (byte) 64 /*0x40*/,
        (byte) 98,
        (byte) 115,
        (byte) 22,
        (byte) 72,
        (byte) 123,
        (byte) 29,
        (byte) 71,
        (byte) 51,
        (byte) 243,
        (byte) 186,
        (byte) 10,
        (byte) 199,
        (byte) 253,
        (byte) 74,
        (byte) 167,
        (byte) 150,
        (byte) 117,
        (byte) 74,
        (byte) 57,
        (byte) 244
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[85];
    byte[] numArray7 = new byte[55];
    numArray7[24] = (byte) 86;
    numArray7[12] = (byte) 224 /*0xE0*/;
    numArray7[46] = (byte) 214;
    numArray7[27] = (byte) 60;
    numArray7[4] = (byte) 240 /*0xF0*/;
    numArray7[35] = (byte) 70;
    numArray7[16 /*0x10*/] = (byte) 73;
    numArray7[7] = (byte) 247;
    numArray7[41] = (byte) 211;
    numArray7[36] = (byte) 41;
    numArray7[14] = (byte) 18;
    numArray7[11] = (byte) 133;
    numArray7[1] = (byte) 126;
    numArray7[17] = (byte) 253;
    numArray7[33] = (byte) 225;
    numArray7[10] = (byte) 3;
    numArray7[0] = (byte) 214;
    numArray7[6] = (byte) 54;
    numArray7[18] = (byte) 127 /*0x7F*/;
    numArray7[38] = (byte) 195;
    numArray7[20] = (byte) 43;
    numArray7[21] = (byte) 156;
    numArray7[22] = (byte) 182;
    numArray7[48 /*0x30*/] = (byte) 160 /*0xA0*/;
    numArray7[43] = (byte) 157;
    numArray7[25] = (byte) 234;
    numArray7[29] = (byte) 84;
    numArray7[8] = (byte) 170;
    numArray7[28] = (byte) 190;
    numArray7[45] = (byte) 134;
    numArray7[30] = (byte) 32 /*0x20*/;
    numArray7[31 /*0x1F*/] = (byte) 153;
    numArray7[32 /*0x20*/] = (byte) 149;
    numArray7[26] = (byte) 221;
    numArray7[34] = (byte) 181;
    numArray7[52] = (byte) 127 /*0x7F*/;
    numArray7[19] = (byte) 222;
    numArray7[5] = (byte) 66;
    numArray7[23] = (byte) 91;
    numArray7[39] = (byte) 194;
    numArray7[40] = (byte) 25;
    numArray7[3] = (byte) 35;
    numArray7[42] = (byte) 52;
    numArray7[44] = (byte) 241;
    numArray7[9] = (byte) 246;
    numArray7[13] = (byte) 83;
    numArray7[37] = (byte) 187;
    numArray7[2] = (byte) 177;
    numArray7[15] = (byte) 234;
    numArray7[47] = (byte) 54;
    numArray7[50] = (byte) 184;
    numArray7[51] = (byte) 248;
    numArray7[49] = (byte) 120;
    numArray7[53] = (byte) 156;
    numArray7[54] = (byte) 15;
    byte[] numArray8 = new byte[55]
    {
      (byte) 10,
      (byte) 137,
      (byte) 191,
      (byte) 132,
      (byte) 235,
      (byte) 206,
      (byte) 172,
      (byte) 74,
      (byte) 214,
      (byte) 31 /*0x1F*/,
      (byte) 20,
      (byte) 185,
      (byte) 175,
      (byte) 61,
      (byte) 218,
      (byte) 58,
      (byte) 15,
      (byte) 147,
      (byte) 3,
      (byte) 14,
      (byte) 205,
      (byte) 111,
      (byte) 126,
      (byte) 118,
      (byte) 165,
      (byte) 250,
      (byte) 23,
      (byte) 212,
      (byte) 69,
      (byte) 7,
      (byte) 143,
      (byte) 35,
      (byte) 17,
      (byte) 65,
      (byte) 71,
      (byte) 248,
      (byte) 195,
      (byte) 75,
      (byte) 83,
      (byte) 41,
      (byte) 57,
      (byte) 156,
      (byte) 41,
      (byte) 119,
      (byte) 49,
      (byte) 17,
      (byte) 239,
      (byte) 50,
      (byte) 10,
      (byte) 239,
      (byte) 21,
      (byte) 234,
      (byte) 148,
      (byte) 254,
      (byte) 146
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[30]
    {
      (byte) 237,
      (byte) 253,
      (byte) 241,
      (byte) 190,
      (byte) 94,
      (byte) 242,
      (byte) 186,
      (byte) 173,
      (byte) 79,
      (byte) 247,
      (byte) 44,
      (byte) 34,
      (byte) 77,
      (byte) 238,
      (byte) 160 /*0xA0*/,
      (byte) 124,
      (byte) 96 /*0x60*/,
      (byte) 164,
      (byte) 189,
      (byte) 137,
      (byte) 80 /*0x50*/,
      (byte) 225,
      (byte) 58,
      (byte) 59,
      (byte) 15,
      (byte) 199,
      (byte) 142,
      (byte) 113,
      (byte) 105,
      (byte) 87
    };
    byte[] numArray10 = new byte[30]
    {
      (byte) 239,
      (byte) 135,
      (byte) 208 /*0xD0*/,
      (byte) 86,
      (byte) 115,
      (byte) 11,
      (byte) 91,
      (byte) 249,
      (byte) 120,
      (byte) 187,
      (byte) 239,
      (byte) 48 /*0x30*/,
      (byte) 90,
      (byte) 68,
      (byte) 241,
      (byte) 78,
      (byte) 223,
      (byte) 58,
      (byte) 77,
      (byte) 25,
      (byte) 33,
      (byte) 179,
      (byte) 24,
      (byte) 103,
      (byte) 251,
      (byte) 179,
      (byte) 240 /*0xF0*/,
      (byte) 140,
      (byte) 133,
      (byte) 14
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 30);
    for (int index = 0; index < 30; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[16 /*0x10*/];
    byte[] response = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_13393.sspq, 845, (Array) numArray11, 0, 16 /*0x10*/);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13393.sspr, 845, (Array) numArray11, 0, 16 /*0x10*/);
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

  internal static string ssp_appserver_13463()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 51,
        (byte) 249,
        (byte) 71,
        (byte) 94,
        (byte) 11,
        (byte) 195,
        (byte) 181,
        (byte) 174,
        (byte) 150,
        (byte) 100,
        (byte) 72,
        (byte) 68,
        (byte) 247,
        (byte) 213,
        (byte) 17,
        (byte) 120,
        (byte) 126,
        (byte) 8,
        (byte) 176 /*0xB0*/,
        (byte) 176 /*0xB0*/,
        (byte) 193,
        (byte) 226,
        (byte) 21,
        (byte) 102,
        (byte) 54,
        (byte) 169,
        (byte) 155,
        (byte) 144 /*0x90*/,
        (byte) 153,
        (byte) 65,
        (byte) 197,
        (byte) 232,
        (byte) 39,
        (byte) 245,
        (byte) 238,
        (byte) 32 /*0x20*/,
        (byte) 98,
        (byte) 112 /*0x70*/,
        (byte) 218,
        (byte) 45,
        (byte) 22,
        (byte) 240 /*0xF0*/
      };
      byte[] numArray3 = new byte[42];
      numArray3[4] = (byte) 86;
      numArray3[1] = (byte) 70;
      numArray3[2] = (byte) 209;
      numArray3[3] = (byte) 159;
      numArray3[0] = (byte) 102;
      numArray3[5] = (byte) 67;
      numArray3[41] = (byte) 9;
      numArray3[9] = (byte) 80 /*0x50*/;
      numArray3[8] = byte.MaxValue;
      numArray3[39] = (byte) 240 /*0xF0*/;
      numArray3[10] = (byte) 81;
      numArray3[6] = (byte) 75;
      numArray3[12] = (byte) 100;
      numArray3[7] = (byte) 26;
      numArray3[19] = (byte) 204;
      numArray3[36] = (byte) 13;
      numArray3[33] = (byte) 48 /*0x30*/;
      numArray3[38] = (byte) 113;
      numArray3[18] = (byte) 172;
      numArray3[15] = (byte) 167;
      numArray3[20] = (byte) 79;
      numArray3[11] = (byte) 175;
      numArray3[37] = (byte) 239;
      numArray3[23] = (byte) 58;
      numArray3[13] = (byte) 170;
      numArray3[25] = (byte) 145;
      numArray3[22] = (byte) 222;
      numArray3[21] = (byte) 179;
      numArray3[28] = (byte) 160 /*0xA0*/;
      numArray3[27] = (byte) 43;
      numArray3[30] = (byte) 223;
      numArray3[31 /*0x1F*/] = (byte) 172;
      numArray3[32 /*0x20*/] = (byte) 109;
      numArray3[14] = (byte) 54;
      numArray3[34] = (byte) 215;
      numArray3[17] = (byte) 43;
      numArray3[35] = (byte) 203;
      numArray3[26] = (byte) 115;
      numArray3[29] = (byte) 127 /*0x7F*/;
      numArray3[16 /*0x10*/] = (byte) 114;
      numArray3[40] = (byte) 160 /*0xA0*/;
      numArray3[24] = (byte) 138;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42];
    numArray5[10] = (byte) 213;
    numArray5[17] = (byte) 230;
    numArray5[14] = (byte) 61;
    numArray5[41] = (byte) 125;
    numArray5[15] = (byte) 225;
    numArray5[20] = (byte) 86;
    numArray5[6] = (byte) 120;
    numArray5[12] = (byte) 121;
    numArray5[30] = (byte) 119;
    numArray5[3] = (byte) 60;
    numArray5[38] = (byte) 149;
    numArray5[11] = (byte) 164;
    numArray5[5] = (byte) 72;
    numArray5[21] = (byte) 248;
    numArray5[16 /*0x10*/] = (byte) 168;
    numArray5[28] = (byte) 84;
    numArray5[37] = (byte) 180;
    numArray5[9] = (byte) 219;
    numArray5[0] = (byte) 148;
    numArray5[19] = (byte) 146;
    numArray5[2] = (byte) 158;
    numArray5[13] = (byte) 184;
    numArray5[22] = (byte) 208 /*0xD0*/;
    numArray5[33] = (byte) 81;
    numArray5[36] = (byte) 73;
    numArray5[25] = (byte) 85;
    numArray5[26] = (byte) 199;
    numArray5[27] = (byte) 247;
    numArray5[1] = (byte) 84;
    numArray5[4] = (byte) 128 /*0x80*/;
    numArray5[40] = (byte) 181;
    numArray5[31 /*0x1F*/] = (byte) 27;
    numArray5[8] = (byte) 240 /*0xF0*/;
    numArray5[18] = (byte) 249;
    numArray5[34] = (byte) 136;
    numArray5[35] = (byte) 174;
    numArray5[29] = (byte) 47;
    numArray5[23] = (byte) 152;
    numArray5[7] = (byte) 5;
    numArray5[24] = (byte) 18;
    numArray5[39] = (byte) 197;
    numArray5[32 /*0x20*/] = (byte) 154;
    byte[] numArray6 = new byte[42]
    {
      (byte) 82,
      (byte) 168,
      (byte) 129,
      (byte) 203,
      (byte) 248,
      (byte) 93,
      (byte) 179,
      (byte) 66,
      (byte) 198,
      (byte) 199,
      (byte) 163,
      (byte) 20,
      (byte) 12,
      (byte) 110,
      (byte) 187,
      (byte) 137,
      (byte) 79,
      (byte) 49,
      (byte) 59,
      (byte) 5,
      (byte) 56,
      (byte) 129,
      (byte) 122,
      (byte) 205,
      (byte) 181,
      (byte) 245,
      (byte) 5,
      (byte) 86,
      (byte) 99,
      (byte) 78,
      (byte) 35,
      (byte) 207,
      (byte) 51,
      (byte) 80 /*0x50*/,
      (byte) 29,
      (byte) 122,
      (byte) 74,
      (byte) 49,
      (byte) 213,
      (byte) 224 /*0xE0*/,
      (byte) 91,
      (byte) 155
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13464()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[205];
      byte[] numArray2 = new byte[55];
      numArray2[47] = (byte) 89;
      numArray2[35] = (byte) 67;
      numArray2[2] = (byte) 42;
      numArray2[32 /*0x20*/] = (byte) 222;
      numArray2[3] = (byte) 67;
      numArray2[5] = (byte) 105;
      numArray2[11] = (byte) 39;
      numArray2[27] = (byte) 98;
      numArray2[31 /*0x1F*/] = (byte) 161;
      numArray2[45] = (byte) 142;
      numArray2[10] = (byte) 146;
      numArray2[25] = (byte) 67;
      numArray2[23] = (byte) 168;
      numArray2[15] = (byte) 156;
      numArray2[7] = (byte) 52;
      numArray2[50] = (byte) 10;
      numArray2[44] = (byte) 73;
      numArray2[17] = (byte) 89;
      numArray2[49] = (byte) 46;
      numArray2[19] = (byte) 11;
      numArray2[20] = (byte) 148;
      numArray2[21] = (byte) 224 /*0xE0*/;
      numArray2[22] = (byte) 76;
      numArray2[13] = (byte) 233;
      numArray2[24] = (byte) 79;
      numArray2[0] = (byte) 152;
      numArray2[26] = (byte) 232;
      numArray2[9] = (byte) 230;
      numArray2[28] = (byte) 181;
      numArray2[29] = (byte) 77;
      numArray2[30] = (byte) 224 /*0xE0*/;
      numArray2[33] = (byte) 224 /*0xE0*/;
      numArray2[4] = (byte) 139;
      numArray2[38] = (byte) 105;
      numArray2[34] = (byte) 144 /*0x90*/;
      numArray2[6] = (byte) 67;
      numArray2[36] = (byte) 100;
      numArray2[37] = (byte) 85;
      numArray2[8] = (byte) 204;
      numArray2[39] = (byte) 231;
      numArray2[40] = (byte) 241;
      numArray2[16 /*0x10*/] = (byte) 89;
      numArray2[42] = (byte) 11;
      numArray2[43] = (byte) 105;
      numArray2[48 /*0x30*/] = (byte) 167;
      numArray2[12] = (byte) 34;
      numArray2[46] = (byte) 195;
      numArray2[14] = (byte) 120;
      numArray2[51] = (byte) 135;
      numArray2[41] = (byte) 168;
      numArray2[18] = (byte) 101;
      numArray2[1] = (byte) 236;
      numArray2[52] = (byte) 91;
      numArray2[53] = (byte) 208 /*0xD0*/;
      numArray2[54] = (byte) 147;
      byte[] numArray3 = new byte[55]
      {
        (byte) 22,
        (byte) 147,
        (byte) 165,
        (byte) 58,
        (byte) 1,
        (byte) 172,
        (byte) 35,
        (byte) 5,
        (byte) 132,
        (byte) 187,
        (byte) 148,
        (byte) 246,
        (byte) 129,
        (byte) 184,
        (byte) 211,
        (byte) 187,
        (byte) 135,
        (byte) 22,
        (byte) 123,
        (byte) 84,
        (byte) 69,
        (byte) 169,
        (byte) 101,
        (byte) 30,
        (byte) 123,
        (byte) 3,
        (byte) 28,
        (byte) 8,
        (byte) 123,
        (byte) 102,
        (byte) 179,
        (byte) 146,
        (byte) 243,
        (byte) 149,
        (byte) 112 /*0x70*/,
        (byte) 128 /*0x80*/,
        (byte) 191,
        (byte) 204,
        (byte) 110,
        (byte) 27,
        (byte) 28,
        (byte) 113,
        (byte) 23,
        (byte) 57,
        (byte) 162,
        (byte) 6,
        (byte) 167,
        (byte) 12,
        (byte) 247,
        (byte) 225,
        (byte) 100,
        (byte) 157,
        (byte) 161,
        (byte) 34,
        (byte) 125
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[41] = (byte) 179;
      numArray4[35] = (byte) 47;
      numArray4[2] = (byte) 199;
      numArray4[3] = (byte) 240 /*0xF0*/;
      numArray4[4] = (byte) 37;
      numArray4[31 /*0x1F*/] = (byte) 165;
      numArray4[8] = (byte) 169;
      numArray4[46] = (byte) 224 /*0xE0*/;
      numArray4[34] = (byte) 192 /*0xC0*/;
      numArray4[9] = (byte) 58;
      numArray4[10] = (byte) 245;
      numArray4[7] = (byte) 174;
      numArray4[38] = (byte) 234;
      numArray4[13] = (byte) 84;
      numArray4[14] = (byte) 162;
      numArray4[50] = (byte) 198;
      numArray4[16 /*0x10*/] = (byte) 23;
      numArray4[32 /*0x20*/] = (byte) 245;
      numArray4[29] = (byte) 104;
      numArray4[19] = (byte) 73;
      numArray4[15] = (byte) 207;
      numArray4[21] = (byte) 6;
      numArray4[11] = (byte) 129;
      numArray4[30] = (byte) 88;
      numArray4[17] = (byte) 10;
      numArray4[25] = (byte) 73;
      numArray4[33] = (byte) 229;
      numArray4[27] = (byte) 60;
      numArray4[28] = (byte) 192 /*0xC0*/;
      numArray4[49] = (byte) 200;
      numArray4[42] = (byte) 175;
      numArray4[48 /*0x30*/] = (byte) 100;
      numArray4[20] = (byte) 128 /*0x80*/;
      numArray4[24] = (byte) 143;
      numArray4[39] = (byte) 50;
      numArray4[23] = (byte) 254;
      numArray4[36] = (byte) 89;
      numArray4[44] = (byte) 140;
      numArray4[1] = (byte) 50;
      numArray4[0] = (byte) 34;
      numArray4[54] = (byte) 60;
      numArray4[26] = (byte) 144 /*0x90*/;
      numArray4[47] = (byte) 162;
      numArray4[43] = (byte) 12;
      numArray4[22] = (byte) 122;
      numArray4[18] = (byte) 194;
      numArray4[37] = (byte) 118;
      numArray4[6] = (byte) 60;
      numArray4[5] = (byte) 247;
      numArray4[12] = (byte) 145;
      numArray4[45] = (byte) 126;
      numArray4[51] = (byte) 172;
      numArray4[52] = (byte) 229;
      numArray4[53] = (byte) 153;
      numArray4[40] = (byte) 79;
      byte[] numArray5 = new byte[55]
      {
        (byte) 42,
        (byte) 133,
        (byte) 10,
        (byte) 254,
        (byte) 81,
        (byte) 228,
        (byte) 117,
        (byte) 125,
        (byte) 16 /*0x10*/,
        (byte) 108,
        (byte) 89,
        (byte) 106,
        (byte) 132,
        (byte) 67,
        (byte) 2,
        (byte) 234,
        (byte) 215,
        (byte) 104,
        (byte) 18,
        (byte) 43,
        (byte) 174,
        (byte) 40,
        (byte) 237,
        (byte) 108,
        (byte) 224 /*0xE0*/,
        (byte) 232,
        (byte) 136,
        (byte) 208 /*0xD0*/,
        (byte) 198,
        (byte) 28,
        (byte) 250,
        (byte) 183,
        (byte) 30,
        (byte) 52,
        (byte) 12,
        (byte) 88,
        (byte) 225,
        (byte) 96 /*0x60*/,
        (byte) 13,
        (byte) 228,
        (byte) 145,
        (byte) 4,
        (byte) 137,
        (byte) 79,
        (byte) 44,
        (byte) 24,
        (byte) 124,
        (byte) 14,
        (byte) 224 /*0xE0*/,
        (byte) 55,
        (byte) 51,
        (byte) 34,
        (byte) 6,
        (byte) 199,
        (byte) 6
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 85,
        (byte) 80 /*0x50*/,
        (byte) 116,
        (byte) 121,
        (byte) 39,
        (byte) 244,
        (byte) 159,
        (byte) 53,
        (byte) 144 /*0x90*/,
        (byte) 47,
        (byte) 3,
        (byte) 238,
        (byte) 251,
        (byte) 227,
        (byte) 140,
        (byte) 18,
        (byte) 197,
        (byte) 170,
        (byte) 7,
        (byte) 233,
        (byte) 68,
        (byte) 54,
        (byte) 29,
        (byte) 52,
        (byte) 223,
        (byte) 230,
        (byte) 165,
        (byte) 124,
        (byte) 31 /*0x1F*/,
        (byte) 105,
        (byte) 187,
        (byte) 57,
        (byte) 224 /*0xE0*/,
        (byte) 46,
        (byte) 59,
        (byte) 196,
        (byte) 153,
        (byte) 80 /*0x50*/,
        (byte) 40,
        (byte) 209,
        (byte) 206,
        (byte) 244,
        (byte) 171,
        (byte) 195,
        (byte) 48 /*0x30*/,
        (byte) 32 /*0x20*/,
        (byte) 246,
        (byte) 110,
        (byte) 153,
        (byte) 121,
        (byte) 144 /*0x90*/,
        (byte) 245,
        (byte) 104,
        (byte) 219,
        (byte) 4
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 170,
        (byte) 157,
        (byte) 214,
        (byte) 191,
        (byte) 211,
        (byte) 149,
        (byte) 134,
        (byte) 190,
        (byte) 193,
        (byte) 81,
        (byte) 119,
        (byte) 153,
        (byte) 12,
        (byte) 122,
        (byte) 61,
        (byte) 101,
        (byte) 12,
        (byte) 181,
        (byte) 138,
        (byte) 11,
        (byte) 32 /*0x20*/,
        (byte) 29,
        (byte) 190,
        (byte) 206,
        (byte) 12,
        (byte) 247,
        (byte) 118,
        (byte) 193,
        (byte) 243,
        (byte) 90,
        (byte) 69,
        (byte) 236,
        (byte) 167,
        (byte) 37,
        (byte) 26,
        (byte) 44,
        (byte) 196,
        (byte) 105,
        (byte) 35,
        (byte) 227,
        (byte) 46,
        (byte) 205,
        (byte) 99,
        (byte) 48 /*0x30*/,
        (byte) 119,
        (byte) 203,
        (byte) 183,
        (byte) 108,
        (byte) 137,
        byte.MaxValue,
        (byte) 206,
        (byte) 181,
        (byte) 175,
        (byte) 216,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[40]
      {
        (byte) 63 /*0x3F*/,
        (byte) 154,
        (byte) 64 /*0x40*/,
        (byte) 78,
        (byte) 35,
        (byte) 21,
        (byte) 39,
        (byte) 115,
        (byte) 202,
        (byte) 14,
        (byte) 204,
        (byte) 168,
        (byte) 199,
        (byte) 121,
        (byte) 116,
        (byte) 130,
        (byte) 53,
        (byte) 27,
        (byte) 79,
        (byte) 139,
        (byte) 90,
        (byte) 4,
        (byte) 191,
        (byte) 239,
        (byte) 54,
        (byte) 226,
        (byte) 165,
        (byte) 217,
        (byte) 198,
        (byte) 208 /*0xD0*/,
        (byte) 4,
        (byte) 41,
        (byte) 28,
        (byte) 146,
        (byte) 90,
        (byte) 182,
        (byte) 231,
        (byte) 130,
        (byte) 170,
        (byte) 223
      };
      byte[] numArray9 = new byte[40]
      {
        (byte) 177,
        (byte) 33,
        (byte) 75,
        (byte) 60,
        (byte) 121,
        (byte) 217,
        (byte) 116,
        (byte) 90,
        (byte) 61,
        (byte) 91,
        (byte) 41,
        (byte) 16 /*0x10*/,
        (byte) 199,
        (byte) 167,
        (byte) 248,
        (byte) 169,
        (byte) 17,
        (byte) 218,
        (byte) 208 /*0xD0*/,
        (byte) 111,
        (byte) 43,
        (byte) 214,
        (byte) 187,
        (byte) 82,
        (byte) 149,
        (byte) 178,
        (byte) 147,
        (byte) 9,
        (byte) 124,
        (byte) 34,
        (byte) 236,
        (byte) 16 /*0x10*/,
        (byte) 96 /*0x60*/,
        (byte) 125,
        (byte) 254,
        (byte) 171,
        (byte) 169,
        (byte) 85,
        (byte) 0,
        (byte) 22
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[205];
    byte[] numArray11 = new byte[55];
    numArray11[23] = (byte) 140;
    numArray11[43] = (byte) 116;
    numArray11[16 /*0x10*/] = (byte) 64 /*0x40*/;
    numArray11[3] = (byte) 209;
    numArray11[51] = (byte) 111;
    numArray11[53] = (byte) 161;
    numArray11[6] = (byte) 115;
    numArray11[11] = (byte) 216;
    numArray11[8] = (byte) 192 /*0xC0*/;
    numArray11[9] = (byte) 181;
    numArray11[15] = (byte) 23;
    numArray11[10] = (byte) 148;
    numArray11[41] = (byte) 158;
    numArray11[13] = (byte) 219;
    numArray11[26] = (byte) 190;
    numArray11[38] = (byte) 165;
    numArray11[14] = (byte) 80 /*0x50*/;
    numArray11[17] = (byte) 111;
    numArray11[7] = (byte) 50;
    numArray11[19] = (byte) 173;
    numArray11[20] = (byte) 249;
    numArray11[21] = (byte) 9;
    numArray11[0] = (byte) 17;
    numArray11[32 /*0x20*/] = (byte) 10;
    numArray11[18] = (byte) 78;
    numArray11[25] = (byte) 31 /*0x1F*/;
    numArray11[28] = (byte) 178;
    numArray11[27] = (byte) 155;
    numArray11[52] = (byte) 54;
    numArray11[29] = (byte) 112 /*0x70*/;
    numArray11[30] = (byte) 111;
    numArray11[48 /*0x30*/] = (byte) 150;
    numArray11[45] = (byte) 186;
    numArray11[33] = (byte) 158;
    numArray11[34] = (byte) 173;
    numArray11[5] = (byte) 28;
    numArray11[36] = (byte) 112 /*0x70*/;
    numArray11[37] = (byte) 17;
    numArray11[4] = (byte) 0;
    numArray11[39] = (byte) 188;
    numArray11[12] = (byte) 58;
    numArray11[46] = (byte) 31 /*0x1F*/;
    numArray11[42] = (byte) 68;
    numArray11[40] = (byte) 82;
    numArray11[44] = (byte) 136;
    numArray11[31 /*0x1F*/] = (byte) 6;
    numArray11[24] = (byte) 53;
    numArray11[47] = (byte) 219;
    numArray11[2] = (byte) 198;
    numArray11[1] = (byte) 42;
    numArray11[50] = (byte) 106;
    numArray11[22] = (byte) 162;
    numArray11[35] = (byte) 197;
    numArray11[49] = (byte) 106;
    numArray11[54] = (byte) 108;
    byte[] numArray12 = new byte[55]
    {
      (byte) 48 /*0x30*/,
      (byte) 8,
      (byte) 213,
      (byte) 32 /*0x20*/,
      (byte) 202,
      (byte) 162,
      (byte) 217,
      (byte) 16 /*0x10*/,
      (byte) 232,
      (byte) 244,
      (byte) 197,
      (byte) 159,
      (byte) 165,
      (byte) 229,
      (byte) 141,
      (byte) 58,
      (byte) 1,
      (byte) 109,
      (byte) 161,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 194,
      (byte) 186,
      (byte) 173,
      (byte) 30,
      (byte) 206,
      (byte) 11,
      (byte) 5,
      (byte) 132,
      (byte) 155,
      (byte) 190,
      (byte) 121,
      (byte) 122,
      (byte) 0,
      (byte) 182,
      (byte) 189,
      (byte) 6,
      (byte) 98,
      (byte) 240 /*0xF0*/,
      (byte) 164,
      (byte) 39,
      (byte) 136,
      (byte) 167,
      (byte) 252,
      (byte) 232,
      (byte) 226,
      (byte) 214,
      (byte) 53,
      (byte) 29,
      (byte) 250,
      (byte) 49,
      (byte) 174,
      (byte) 109,
      (byte) 114,
      (byte) 149
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 53,
      (byte) 33,
      (byte) 225,
      (byte) 123,
      (byte) 211,
      (byte) 56,
      (byte) 74,
      (byte) 69,
      (byte) 189,
      (byte) 100,
      (byte) 106,
      (byte) 137,
      (byte) 76,
      (byte) 113,
      (byte) 186,
      (byte) 6,
      (byte) 50,
      (byte) 224 /*0xE0*/,
      (byte) 61,
      (byte) 208 /*0xD0*/,
      byte.MaxValue,
      (byte) 9,
      (byte) 141,
      (byte) 124,
      (byte) 53,
      (byte) 250,
      (byte) 132,
      (byte) 203,
      (byte) 90,
      (byte) 43,
      (byte) 33,
      (byte) 32 /*0x20*/,
      (byte) 96 /*0x60*/,
      (byte) 229,
      (byte) 0,
      (byte) 82,
      (byte) 225,
      (byte) 227,
      (byte) 166,
      (byte) 229,
      (byte) 169,
      (byte) 233,
      (byte) 61,
      (byte) 136,
      (byte) 24,
      (byte) 35,
      (byte) 190,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 158,
      (byte) 114,
      (byte) 102,
      (byte) 212,
      (byte) 115,
      (byte) 89
    };
    byte[] numArray14 = new byte[55];
    numArray14[30] = (byte) 134;
    numArray14[1] = (byte) 46;
    numArray14[2] = (byte) 137;
    numArray14[3] = (byte) 168;
    numArray14[14] = (byte) 164;
    numArray14[35] = (byte) 158;
    numArray14[54] = (byte) 244;
    numArray14[10] = (byte) 86;
    numArray14[8] = (byte) 229;
    numArray14[9] = (byte) 198;
    numArray14[16 /*0x10*/] = (byte) 140;
    numArray14[11] = (byte) 158;
    numArray14[7] = (byte) 242;
    numArray14[5] = (byte) 58;
    numArray14[42] = (byte) 64 /*0x40*/;
    numArray14[15] = (byte) 155;
    numArray14[0] = (byte) 188;
    numArray14[17] = (byte) 170;
    numArray14[33] = (byte) 39;
    numArray14[46] = (byte) 88;
    numArray14[4] = (byte) 25;
    numArray14[27] = (byte) 117;
    numArray14[22] = (byte) 166;
    numArray14[23] = (byte) 55;
    numArray14[24] = (byte) 237;
    numArray14[49] = (byte) 58;
    numArray14[26] = (byte) 251;
    numArray14[44] = (byte) 4;
    numArray14[28] = (byte) 239;
    numArray14[29] = (byte) 89;
    numArray14[6] = (byte) 194;
    numArray14[31 /*0x1F*/] = (byte) 170;
    numArray14[32 /*0x20*/] = (byte) 188;
    numArray14[12] = (byte) 253;
    numArray14[47] = (byte) 177;
    numArray14[25] = (byte) 240 /*0xF0*/;
    numArray14[36] = (byte) 56;
    numArray14[37] = (byte) 21;
    numArray14[38] = (byte) 184;
    numArray14[39] = (byte) 102;
    numArray14[40] = (byte) 100;
    numArray14[41] = (byte) 140;
    numArray14[19] = (byte) 185;
    numArray14[43] = (byte) 115;
    numArray14[51] = (byte) 159;
    numArray14[45] = (byte) 62;
    numArray14[21] = (byte) 34;
    numArray14[20] = (byte) 222;
    numArray14[48 /*0x30*/] = (byte) 8;
    numArray14[13] = (byte) 83;
    numArray14[50] = (byte) 214;
    numArray14[53] = (byte) 158;
    numArray14[52] = (byte) 22;
    numArray14[34] = (byte) 144 /*0x90*/;
    numArray14[18] = (byte) 37;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 1,
      (byte) 52,
      (byte) 157,
      (byte) 130,
      (byte) 52,
      (byte) 47,
      (byte) 217,
      (byte) 134,
      (byte) 254,
      (byte) 59,
      (byte) 219,
      (byte) 156,
      (byte) 168,
      (byte) 168,
      (byte) 177,
      (byte) 108,
      (byte) 37,
      (byte) 252,
      (byte) 23,
      (byte) 32 /*0x20*/,
      (byte) 191,
      (byte) 149,
      (byte) 22,
      (byte) 228,
      (byte) 79,
      (byte) 50,
      (byte) 65,
      (byte) 148,
      (byte) 144 /*0x90*/,
      (byte) 207,
      (byte) 161,
      (byte) 72,
      (byte) 116,
      (byte) 48 /*0x30*/,
      (byte) 123,
      (byte) 105,
      (byte) 47,
      (byte) 175,
      (byte) 246,
      (byte) 207,
      (byte) 209,
      (byte) 191,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 123,
      (byte) 69,
      (byte) 111,
      (byte) 210,
      (byte) 113,
      (byte) 191,
      (byte) 87,
      (byte) 80 /*0x50*/,
      (byte) 43,
      (byte) 63 /*0x3F*/,
      (byte) 207
    };
    byte[] numArray16 = new byte[55];
    numArray16[52] = (byte) 198;
    numArray16[54] = (byte) 64 /*0x40*/;
    numArray16[33] = (byte) 79;
    numArray16[16 /*0x10*/] = (byte) 29;
    numArray16[4] = (byte) 6;
    numArray16[5] = (byte) 145;
    numArray16[6] = (byte) 205;
    numArray16[7] = (byte) 204;
    numArray16[48 /*0x30*/] = (byte) 139;
    numArray16[19] = (byte) 180;
    numArray16[13] = (byte) 223;
    numArray16[34] = (byte) 214;
    numArray16[3] = (byte) 94;
    numArray16[35] = (byte) 108;
    numArray16[14] = (byte) 190;
    numArray16[0] = (byte) 159;
    numArray16[45] = (byte) 199;
    numArray16[11] = (byte) 182;
    numArray16[18] = (byte) 22;
    numArray16[21] = (byte) 159;
    numArray16[20] = (byte) 175;
    numArray16[17] = (byte) 211;
    numArray16[22] = (byte) 174;
    numArray16[1] = (byte) 38;
    numArray16[53] = (byte) 50;
    numArray16[25] = (byte) 166;
    numArray16[46] = (byte) 75;
    numArray16[27] = (byte) 184;
    numArray16[2] = (byte) 77;
    numArray16[38] = (byte) 57;
    numArray16[30] = (byte) 107;
    numArray16[31 /*0x1F*/] = (byte) 228;
    numArray16[32 /*0x20*/] = (byte) 206;
    numArray16[10] = (byte) 204;
    numArray16[51] = (byte) 166;
    numArray16[29] = (byte) 100;
    numArray16[36] = (byte) 96 /*0x60*/;
    numArray16[24] = (byte) 91;
    numArray16[12] = (byte) 153;
    numArray16[39] = (byte) 7;
    numArray16[28] = (byte) 124;
    numArray16[41] = (byte) 12;
    numArray16[42] = (byte) 59;
    numArray16[40] = (byte) 137;
    numArray16[44] = (byte) 2;
    numArray16[43] = (byte) 11;
    numArray16[15] = (byte) 107;
    numArray16[26] = (byte) 33;
    numArray16[37] = (byte) 96 /*0x60*/;
    numArray16[23] = (byte) 206;
    numArray16[8] = (byte) 122;
    numArray16[9] = (byte) 66;
    numArray16[47] = (byte) 208 /*0xD0*/;
    numArray16[49] = (byte) 81;
    numArray16[50] = (byte) 241;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[40]
    {
      (byte) 80 /*0x50*/,
      (byte) 40,
      (byte) 63 /*0x3F*/,
      (byte) 127 /*0x7F*/,
      (byte) 34,
      (byte) 37,
      (byte) 182,
      (byte) 72,
      (byte) 119,
      (byte) 243,
      (byte) 22,
      (byte) 30,
      (byte) 252,
      (byte) 198,
      (byte) 248,
      (byte) 17,
      (byte) 103,
      (byte) 194,
      (byte) 54,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 129,
      (byte) 236,
      (byte) 203,
      (byte) 201,
      (byte) 249,
      (byte) 78,
      (byte) 149,
      (byte) 7,
      (byte) 56,
      (byte) 69,
      (byte) 145,
      (byte) 102,
      (byte) 132,
      (byte) 38,
      (byte) 84,
      (byte) 181,
      (byte) 98,
      (byte) 106,
      (byte) 163
    };
    byte[] numArray18 = new byte[40]
    {
      (byte) 63 /*0x3F*/,
      (byte) 144 /*0x90*/,
      (byte) 44,
      (byte) 193,
      (byte) 34,
      (byte) 126,
      (byte) 27,
      (byte) 76,
      (byte) 250,
      (byte) 165,
      (byte) 73,
      (byte) 206,
      (byte) 23,
      (byte) 166,
      (byte) 143,
      (byte) 221,
      (byte) 227,
      (byte) 101,
      (byte) 63 /*0x3F*/,
      (byte) 231,
      (byte) 231,
      (byte) 246,
      (byte) 193,
      (byte) 157,
      (byte) 104,
      (byte) 137,
      (byte) 74,
      (byte) 51,
      (byte) 37,
      (byte) 236,
      (byte) 206,
      (byte) 193,
      (byte) 103,
      (byte) 74,
      (byte) 52,
      (byte) 140,
      (byte) 166,
      (byte) 133,
      (byte) 149,
      (byte) 127 /*0x7F*/
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 40);
    for (int index = 0; index < 40; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13465()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[228];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 65;
      numArray2[1] = (byte) 135;
      numArray2[10] = (byte) 150;
      numArray2[3] = (byte) 187;
      numArray2[4] = (byte) 233;
      numArray2[5] = (byte) 56;
      numArray2[6] = (byte) 124;
      numArray2[12] = (byte) 59;
      numArray2[22] = (byte) 183;
      numArray2[9] = (byte) 133;
      numArray2[28] = (byte) 88;
      numArray2[11] = (byte) 207;
      numArray2[17] = (byte) 33;
      numArray2[13] = (byte) 115;
      numArray2[14] = (byte) 181;
      numArray2[15] = (byte) 41;
      numArray2[16 /*0x10*/] = (byte) 215;
      numArray2[45] = (byte) 86;
      numArray2[52] = (byte) 156;
      numArray2[19] = (byte) 63 /*0x3F*/;
      numArray2[20] = (byte) 173;
      numArray2[21] = (byte) 132;
      numArray2[2] = (byte) 44;
      numArray2[54] = (byte) 124;
      numArray2[47] = (byte) 65;
      numArray2[7] = (byte) 45;
      numArray2[26] = (byte) 92;
      numArray2[27] = (byte) 5;
      numArray2[48 /*0x30*/] = (byte) 172;
      numArray2[29] = (byte) 147;
      numArray2[30] = (byte) 55;
      numArray2[25] = (byte) 163;
      numArray2[40] = (byte) 188;
      numArray2[23] = (byte) 120;
      numArray2[41] = (byte) 24;
      numArray2[44] = (byte) 238;
      numArray2[36] = (byte) 204;
      numArray2[8] = (byte) 152;
      numArray2[38] = (byte) 171;
      numArray2[37] = (byte) 147;
      numArray2[32 /*0x20*/] = (byte) 16 /*0x10*/;
      numArray2[33] = (byte) 194;
      numArray2[42] = (byte) 249;
      numArray2[43] = (byte) 225;
      numArray2[24] = (byte) 160 /*0xA0*/;
      numArray2[39] = (byte) 207;
      numArray2[34] = (byte) 234;
      numArray2[35] = (byte) 66;
      numArray2[50] = (byte) 204;
      numArray2[0] = (byte) 224 /*0xE0*/;
      numArray2[46] = (byte) 25;
      numArray2[51] = (byte) 202;
      numArray2[31 /*0x1F*/] = (byte) 83;
      numArray2[49] = (byte) 65;
      numArray2[53] = (byte) 49;
      byte[] numArray3 = new byte[55];
      numArray3[11] = (byte) 117;
      numArray3[38] = (byte) 73;
      numArray3[27] = (byte) 82;
      numArray3[3] = (byte) 123;
      numArray3[10] = (byte) 118;
      numArray3[36] = (byte) 235;
      numArray3[6] = (byte) 88;
      numArray3[1] = (byte) 168;
      numArray3[54] = (byte) 116;
      numArray3[9] = (byte) 39;
      numArray3[5] = (byte) 217;
      numArray3[51] = (byte) 131;
      numArray3[12] = (byte) 250;
      numArray3[48 /*0x30*/] = (byte) 135;
      numArray3[14] = (byte) 126;
      numArray3[40] = (byte) 19;
      numArray3[44] = (byte) 42;
      numArray3[17] = (byte) 146;
      numArray3[7] = (byte) 241;
      numArray3[19] = (byte) 188;
      numArray3[20] = (byte) 167;
      numArray3[21] = (byte) 32 /*0x20*/;
      numArray3[32 /*0x20*/] = (byte) 207;
      numArray3[23] = (byte) 186;
      numArray3[45] = (byte) 130;
      numArray3[42] = (byte) 174;
      numArray3[26] = (byte) 244;
      numArray3[31 /*0x1F*/] = (byte) 184;
      numArray3[28] = (byte) 143;
      numArray3[29] = (byte) 124;
      numArray3[30] = (byte) 204;
      numArray3[15] = (byte) 42;
      numArray3[22] = (byte) 154;
      numArray3[33] = (byte) 183;
      numArray3[34] = (byte) 142;
      numArray3[24] = (byte) 63 /*0x3F*/;
      numArray3[37] = (byte) 75;
      numArray3[4] = (byte) 39;
      numArray3[47] = (byte) 232;
      numArray3[39] = (byte) 220;
      numArray3[18] = (byte) 9;
      numArray3[41] = (byte) 192 /*0xC0*/;
      numArray3[13] = (byte) 241;
      numArray3[43] = (byte) 9;
      numArray3[2] = (byte) 51;
      numArray3[8] = (byte) 117;
      numArray3[35] = (byte) 139;
      numArray3[16 /*0x10*/] = (byte) 60;
      numArray3[25] = (byte) 127 /*0x7F*/;
      numArray3[49] = (byte) 8;
      numArray3[50] = (byte) 225;
      numArray3[46] = (byte) 116;
      numArray3[52] = (byte) 160 /*0xA0*/;
      numArray3[53] = (byte) 228;
      numArray3[0] = (byte) 29;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 77,
        (byte) 31 /*0x1F*/,
        (byte) 212,
        (byte) 94,
        (byte) 0,
        (byte) 138,
        (byte) 1,
        (byte) 199,
        (byte) 254,
        (byte) 174,
        (byte) 211,
        (byte) 58,
        (byte) 135,
        (byte) 89,
        (byte) 36,
        (byte) 104,
        (byte) 76,
        (byte) 23,
        (byte) 204,
        (byte) 228,
        (byte) 142,
        (byte) 190,
        (byte) 247,
        (byte) 123,
        (byte) 162,
        (byte) 228,
        (byte) 148,
        (byte) 129,
        (byte) 212,
        (byte) 226,
        (byte) 191,
        (byte) 202,
        (byte) 135,
        (byte) 193,
        (byte) 158,
        (byte) 135,
        (byte) 211,
        (byte) 211,
        (byte) 38,
        (byte) 250,
        (byte) 178,
        (byte) 236,
        (byte) 32 /*0x20*/,
        (byte) 11,
        (byte) 153,
        (byte) 132,
        (byte) 178,
        (byte) 68,
        (byte) 152,
        (byte) 220,
        (byte) 94,
        (byte) 89,
        (byte) 199,
        (byte) 82,
        (byte) 41
      };
      byte[] numArray5 = new byte[55];
      numArray5[10] = (byte) 15;
      numArray5[52] = (byte) 92;
      numArray5[41] = (byte) 132;
      numArray5[21] = byte.MaxValue;
      numArray5[38] = (byte) 183;
      numArray5[5] = (byte) 235;
      numArray5[27] = (byte) 210;
      numArray5[7] = (byte) 81;
      numArray5[8] = (byte) 95;
      numArray5[13] = (byte) 143;
      numArray5[36] = (byte) 147;
      numArray5[44] = (byte) 181;
      numArray5[12] = (byte) 235;
      numArray5[32 /*0x20*/] = (byte) 209;
      numArray5[53] = (byte) 77;
      numArray5[15] = (byte) 189;
      numArray5[16 /*0x10*/] = (byte) 82;
      numArray5[50] = (byte) 96 /*0x60*/;
      numArray5[18] = (byte) 81;
      numArray5[24] = (byte) 212;
      numArray5[43] = (byte) 79;
      numArray5[37] = (byte) 76;
      numArray5[33] = (byte) 126;
      numArray5[2] = (byte) 61;
      numArray5[23] = (byte) 46;
      numArray5[0] = (byte) 85;
      numArray5[26] = (byte) 116;
      numArray5[4] = (byte) 209;
      numArray5[28] = (byte) 66;
      numArray5[25] = (byte) 112 /*0x70*/;
      numArray5[30] = (byte) 27;
      numArray5[31 /*0x1F*/] = (byte) 9;
      numArray5[14] = (byte) 72;
      numArray5[34] = (byte) 79;
      numArray5[19] = (byte) 12;
      numArray5[29] = (byte) 15;
      numArray5[40] = (byte) 169;
      numArray5[11] = (byte) 66;
      numArray5[54] = (byte) 247;
      numArray5[39] = (byte) 132;
      numArray5[3] = (byte) 123;
      numArray5[6] = (byte) 155;
      numArray5[42] = (byte) 214;
      numArray5[47] = (byte) 180;
      numArray5[22] = (byte) 213;
      numArray5[45] = (byte) 94;
      numArray5[46] = (byte) 85;
      numArray5[1] = (byte) 131;
      numArray5[48 /*0x30*/] = (byte) 226;
      numArray5[49] = (byte) 249;
      numArray5[17] = (byte) 6;
      numArray5[51] = (byte) 206;
      numArray5[20] = (byte) 3;
      numArray5[9] = (byte) 134;
      numArray5[35] = (byte) 38;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[35] = (byte) 122;
      numArray6[48 /*0x30*/] = (byte) 134;
      numArray6[2] = (byte) 233;
      numArray6[3] = (byte) 166;
      numArray6[39] = (byte) 252;
      numArray6[41] = (byte) 148;
      numArray6[6] = (byte) 153;
      numArray6[24] = (byte) 197;
      numArray6[47] = (byte) 46;
      numArray6[46] = (byte) 188;
      numArray6[34] = (byte) 3;
      numArray6[11] = (byte) 170;
      numArray6[12] = (byte) 61;
      numArray6[13] = (byte) 25;
      numArray6[38] = (byte) 156;
      numArray6[15] = (byte) 230;
      numArray6[16 /*0x10*/] = (byte) 205;
      numArray6[17] = (byte) 254;
      numArray6[8] = (byte) 6;
      numArray6[53] = (byte) 87;
      numArray6[20] = (byte) 207;
      numArray6[10] = (byte) 46;
      numArray6[30] = (byte) 19;
      numArray6[5] = (byte) 251;
      numArray6[36] = (byte) 32 /*0x20*/;
      numArray6[25] = (byte) 238;
      numArray6[51] = (byte) 33;
      numArray6[28] = (byte) 253;
      numArray6[52] = (byte) 221;
      numArray6[7] = (byte) 136;
      numArray6[19] = (byte) 153;
      numArray6[31 /*0x1F*/] = (byte) 16 /*0x10*/;
      numArray6[32 /*0x20*/] = (byte) 31 /*0x1F*/;
      numArray6[50] = (byte) 39;
      numArray6[26] = (byte) 175;
      numArray6[29] = (byte) 13;
      numArray6[42] = (byte) 137;
      numArray6[37] = (byte) 94;
      numArray6[23] = (byte) 69;
      numArray6[27] = (byte) 156;
      numArray6[40] = (byte) 107;
      numArray6[18] = (byte) 169;
      numArray6[9] = (byte) 127 /*0x7F*/;
      numArray6[43] = (byte) 81;
      numArray6[44] = (byte) 190;
      numArray6[45] = (byte) 223;
      numArray6[14] = (byte) 96 /*0x60*/;
      numArray6[1] = (byte) 157;
      numArray6[4] = (byte) 150;
      numArray6[49] = (byte) 129;
      numArray6[33] = (byte) 36;
      numArray6[22] = (byte) 77;
      numArray6[0] = (byte) 119;
      numArray6[21] = (byte) 81;
      numArray6[54] = (byte) 107;
      byte[] numArray7 = new byte[55]
      {
        (byte) 209,
        (byte) 145,
        (byte) 183,
        (byte) 156,
        (byte) 31 /*0x1F*/,
        (byte) 228,
        (byte) 132,
        (byte) 221,
        (byte) 237,
        (byte) 248,
        (byte) 166,
        (byte) 72,
        (byte) 124,
        (byte) 178,
        (byte) 25,
        (byte) 209,
        (byte) 26,
        (byte) 182,
        (byte) 211,
        (byte) 206,
        (byte) 84,
        (byte) 227,
        (byte) 24,
        (byte) 140,
        (byte) 19,
        (byte) 215,
        (byte) 154,
        (byte) 171,
        (byte) 186,
        (byte) 6,
        (byte) 224 /*0xE0*/,
        (byte) 248,
        (byte) 63 /*0x3F*/,
        (byte) 199,
        (byte) 69,
        (byte) 175,
        (byte) 54,
        (byte) 108,
        (byte) 157,
        (byte) 17,
        (byte) 187,
        (byte) 123,
        (byte) 220,
        (byte) 17,
        (byte) 237,
        (byte) 191,
        (byte) 183,
        (byte) 120,
        (byte) 130,
        (byte) 121,
        (byte) 143,
        (byte) 146,
        (byte) 189,
        (byte) 175,
        (byte) 147
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 29,
        (byte) 250,
        (byte) 118,
        (byte) 202,
        (byte) 231,
        (byte) 15,
        (byte) 19,
        (byte) 122,
        (byte) 235,
        (byte) 195,
        (byte) 97,
        (byte) 55,
        (byte) 186,
        (byte) 73,
        (byte) 241,
        (byte) 110,
        (byte) 251,
        (byte) 192 /*0xC0*/,
        (byte) 49,
        (byte) 55,
        (byte) 22,
        (byte) 230,
        (byte) 145,
        (byte) 142,
        (byte) 105,
        (byte) 116,
        (byte) 192 /*0xC0*/,
        (byte) 44,
        (byte) 50,
        (byte) 1,
        (byte) 158,
        (byte) 136,
        (byte) 128 /*0x80*/,
        (byte) 215,
        (byte) 95,
        (byte) 130,
        (byte) 125,
        (byte) 230,
        (byte) 134,
        (byte) 250,
        (byte) 124,
        (byte) 107,
        (byte) 180,
        (byte) 145,
        (byte) 65,
        (byte) 224 /*0xE0*/,
        (byte) 230,
        (byte) 114,
        (byte) 198,
        (byte) 81,
        (byte) 53,
        (byte) 105,
        (byte) 254,
        (byte) 37,
        (byte) 60
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 43,
        (byte) 249,
        (byte) 177,
        (byte) 186,
        (byte) 190,
        (byte) 164,
        byte.MaxValue,
        (byte) 211,
        (byte) 15,
        (byte) 22,
        (byte) 53,
        (byte) 174,
        (byte) 199,
        (byte) 227,
        (byte) 7,
        (byte) 225,
        (byte) 107,
        (byte) 54,
        (byte) 248,
        (byte) 1,
        (byte) 105,
        (byte) 80 /*0x50*/,
        (byte) 45,
        (byte) 233,
        (byte) 144 /*0x90*/,
        (byte) 14,
        (byte) 197,
        (byte) 16 /*0x10*/,
        (byte) 36,
        (byte) 183,
        (byte) 247,
        (byte) 4,
        (byte) 146,
        (byte) 243,
        (byte) 129,
        (byte) 203,
        (byte) 57,
        (byte) 199,
        (byte) 100,
        (byte) 141,
        (byte) 216,
        (byte) 81,
        (byte) 58,
        (byte) 13,
        (byte) 179,
        (byte) 222,
        (byte) 118,
        (byte) 63 /*0x3F*/,
        (byte) 236,
        (byte) 79,
        (byte) 75,
        (byte) 139,
        (byte) 199,
        (byte) 78,
        (byte) 77
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[8]
      {
        (byte) 232,
        (byte) 96 /*0x60*/,
        (byte) 115,
        (byte) 78,
        (byte) 231,
        (byte) 72,
        (byte) 234,
        (byte) 37
      };
      byte[] numArray11 = new byte[8];
      numArray11[7] = (byte) 181;
      numArray11[1] = (byte) 70;
      numArray11[2] = (byte) 37;
      numArray11[3] = (byte) 134;
      numArray11[0] = (byte) 82;
      numArray11[5] = (byte) 8;
      numArray11[6] = (byte) 127 /*0x7F*/;
      numArray11[4] = (byte) 161;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[228];
    byte[] numArray13 = new byte[55]
    {
      (byte) 49,
      byte.MaxValue,
      (byte) 76,
      (byte) 136,
      (byte) 142,
      (byte) 77,
      (byte) 101,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 52,
      (byte) 138,
      (byte) 90,
      (byte) 154,
      (byte) 41,
      (byte) 25,
      (byte) 149,
      (byte) 133,
      (byte) 18,
      (byte) 171,
      (byte) 149,
      (byte) 34,
      (byte) 202,
      (byte) 108,
      (byte) 104,
      (byte) 42,
      (byte) 2,
      (byte) 137,
      (byte) 88,
      (byte) 97,
      (byte) 32 /*0x20*/,
      (byte) 47,
      (byte) 198,
      (byte) 106,
      (byte) 144 /*0x90*/,
      (byte) 4,
      (byte) 105,
      (byte) 155,
      (byte) 216,
      (byte) 229,
      (byte) 1,
      (byte) 129,
      (byte) 103,
      (byte) 30,
      (byte) 240 /*0xF0*/,
      (byte) 78,
      (byte) 92,
      (byte) 73,
      (byte) 192 /*0xC0*/,
      (byte) 85,
      (byte) 90,
      (byte) 143,
      (byte) 39,
      (byte) 139,
      (byte) 38,
      (byte) 136
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 166,
      (byte) 46,
      (byte) 128 /*0x80*/,
      (byte) 20,
      (byte) 195,
      (byte) 79,
      (byte) 112 /*0x70*/,
      (byte) 205,
      (byte) 183,
      (byte) 66,
      (byte) 129,
      (byte) 133,
      (byte) 226,
      (byte) 142,
      (byte) 8,
      (byte) 178,
      (byte) 25,
      (byte) 77,
      (byte) 130,
      (byte) 92,
      (byte) 205,
      (byte) 113,
      (byte) 113,
      (byte) 137,
      (byte) 29,
      (byte) 221,
      (byte) 14,
      (byte) 59,
      (byte) 34,
      (byte) 214,
      (byte) 188,
      (byte) 19,
      (byte) 28,
      (byte) 53,
      (byte) 132,
      (byte) 99,
      (byte) 226,
      (byte) 172,
      (byte) 8,
      (byte) 163,
      (byte) 6,
      (byte) 166,
      (byte) 213,
      (byte) 202,
      (byte) 152,
      (byte) 219,
      (byte) 105,
      (byte) 121,
      (byte) 19,
      (byte) 102,
      (byte) 50,
      (byte) 99,
      (byte) 147,
      (byte) 62,
      (byte) 246
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 227,
      (byte) 94,
      (byte) 139,
      (byte) 196,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 171,
      (byte) 5,
      (byte) 195,
      (byte) 240 /*0xF0*/,
      (byte) 9,
      (byte) 36,
      (byte) 175,
      (byte) 249,
      (byte) 173,
      (byte) 45,
      (byte) 170,
      (byte) 19,
      (byte) 187,
      (byte) 172,
      (byte) 124,
      (byte) 25,
      (byte) 206,
      (byte) 184,
      (byte) 200,
      (byte) 2,
      (byte) 6,
      (byte) 145,
      (byte) 7,
      (byte) 59,
      (byte) 67,
      (byte) 7,
      (byte) 157,
      (byte) 214,
      (byte) 183,
      (byte) 122,
      (byte) 89,
      (byte) 61,
      (byte) 115,
      (byte) 232,
      (byte) 114,
      (byte) 234,
      (byte) 139,
      (byte) 107,
      (byte) 63 /*0x3F*/,
      (byte) 155,
      (byte) 166,
      (byte) 97,
      (byte) 226,
      (byte) 249,
      (byte) 225,
      (byte) 18,
      (byte) 205,
      (byte) 187,
      (byte) 22
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 99,
      (byte) 13,
      (byte) 142,
      (byte) 46,
      (byte) 12,
      (byte) 118,
      (byte) 70,
      (byte) 166,
      (byte) 131,
      (byte) 233,
      (byte) 32 /*0x20*/,
      (byte) 81,
      (byte) 207,
      (byte) 24,
      (byte) 22,
      (byte) 248,
      (byte) 252,
      (byte) 66,
      (byte) 134,
      (byte) 51,
      (byte) 84,
      (byte) 126,
      (byte) 92,
      (byte) 51,
      (byte) 122,
      (byte) 78,
      (byte) 25,
      (byte) 55,
      (byte) 170,
      (byte) 47,
      (byte) 43,
      (byte) 233,
      (byte) 153,
      (byte) 189,
      (byte) 55,
      (byte) 42,
      (byte) 207,
      (byte) 58,
      (byte) 190,
      (byte) 104,
      (byte) 143,
      (byte) 70,
      byte.MaxValue,
      (byte) 42,
      (byte) 164,
      (byte) 161,
      (byte) 199,
      (byte) 166,
      (byte) 56,
      (byte) 229,
      (byte) 15,
      (byte) 110,
      (byte) 78,
      (byte) 78,
      (byte) 15
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 210,
      (byte) 251,
      (byte) 11,
      (byte) 121,
      (byte) 26,
      (byte) 254,
      (byte) 135,
      (byte) 172,
      (byte) 1,
      (byte) 135,
      (byte) 216,
      (byte) 66,
      (byte) 50,
      (byte) 212,
      (byte) 109,
      (byte) 160 /*0xA0*/,
      (byte) 211,
      (byte) 57,
      (byte) 146,
      (byte) 141,
      (byte) 98,
      (byte) 120,
      (byte) 44,
      (byte) 91,
      (byte) 165,
      (byte) 175,
      (byte) 14,
      (byte) 89,
      (byte) 48 /*0x30*/,
      (byte) 58,
      (byte) 226,
      (byte) 192 /*0xC0*/,
      (byte) 237,
      (byte) 234,
      (byte) 134,
      (byte) 65,
      (byte) 104,
      (byte) 110,
      (byte) 88,
      (byte) 236,
      (byte) 4,
      (byte) 190,
      (byte) 62,
      (byte) 20,
      (byte) 205,
      (byte) 30,
      (byte) 175,
      (byte) 37,
      (byte) 116,
      (byte) 114,
      (byte) 250,
      (byte) 129,
      (byte) 132,
      (byte) 60,
      (byte) 45
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 94,
      (byte) 110,
      (byte) 185,
      (byte) 142,
      (byte) 167,
      (byte) 92,
      (byte) 64 /*0x40*/,
      (byte) 44,
      (byte) 114,
      (byte) 2,
      (byte) 230,
      (byte) 197,
      (byte) 165,
      (byte) 207,
      (byte) 72,
      (byte) 103,
      (byte) 28,
      (byte) 111,
      (byte) 55,
      (byte) 116,
      (byte) 33,
      (byte) 119,
      (byte) 69,
      (byte) 219,
      (byte) 154,
      (byte) 53,
      (byte) 138,
      (byte) 19,
      (byte) 176 /*0xB0*/,
      (byte) 19,
      (byte) 122,
      (byte) 152,
      (byte) 19,
      (byte) 40,
      (byte) 169,
      (byte) 142,
      (byte) 239,
      (byte) 169,
      (byte) 19,
      (byte) 164,
      (byte) 207,
      (byte) 80 /*0x50*/,
      (byte) 217,
      (byte) 33,
      (byte) 112 /*0x70*/,
      (byte) 122,
      (byte) 55,
      (byte) 40,
      (byte) 83,
      (byte) 114,
      (byte) 101,
      (byte) 32 /*0x20*/,
      (byte) 189,
      (byte) 248,
      (byte) 41
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 175,
      (byte) 212,
      (byte) 107,
      (byte) 239,
      (byte) 46,
      (byte) 124,
      (byte) 199,
      (byte) 164,
      (byte) 24,
      (byte) 84,
      (byte) 72,
      (byte) 77,
      (byte) 44,
      (byte) 56,
      (byte) 100,
      (byte) 224 /*0xE0*/,
      (byte) 166,
      (byte) 137,
      (byte) 164,
      (byte) 58,
      (byte) 23,
      (byte) 141,
      (byte) 134,
      byte.MaxValue,
      (byte) 232,
      (byte) 228,
      (byte) 190,
      (byte) 63 /*0x3F*/,
      (byte) 96 /*0x60*/,
      (byte) 59,
      (byte) 176 /*0xB0*/,
      (byte) 188,
      (byte) 44,
      (byte) 113,
      (byte) 184,
      (byte) 154,
      (byte) 237,
      (byte) 252,
      (byte) 213,
      (byte) 66,
      (byte) 118,
      (byte) 111,
      (byte) 181,
      (byte) 202,
      (byte) 103,
      (byte) 215,
      (byte) 72,
      (byte) 58,
      (byte) 150,
      (byte) 161,
      (byte) 222,
      (byte) 178,
      (byte) 137,
      (byte) 31 /*0x1F*/,
      (byte) 183
    };
    byte[] numArray20 = new byte[55];
    numArray20[4] = (byte) 164;
    numArray20[1] = (byte) 199;
    numArray20[2] = (byte) 146;
    numArray20[5] = (byte) 165;
    numArray20[37] = (byte) 211;
    numArray20[9] = (byte) 210;
    numArray20[6] = (byte) 252;
    numArray20[33] = (byte) 239;
    numArray20[47] = (byte) 171;
    numArray20[50] = (byte) 254;
    numArray20[27] = (byte) 27;
    numArray20[11] = (byte) 225;
    numArray20[43] = (byte) 35;
    numArray20[13] = (byte) 97;
    numArray20[24] = (byte) 139;
    numArray20[15] = (byte) 239;
    numArray20[16 /*0x10*/] = (byte) 127 /*0x7F*/;
    numArray20[18] = (byte) 10;
    numArray20[14] = (byte) 213;
    numArray20[51] = (byte) 81;
    numArray20[10] = (byte) 136;
    numArray20[45] = (byte) 136;
    numArray20[22] = (byte) 19;
    numArray20[12] = (byte) 183;
    numArray20[32 /*0x20*/] = (byte) 153;
    numArray20[25] = (byte) 87;
    numArray20[26] = (byte) 218;
    numArray20[0] = (byte) 91;
    numArray20[17] = (byte) 230;
    numArray20[54] = (byte) 112 /*0x70*/;
    numArray20[30] = (byte) 108;
    numArray20[31 /*0x1F*/] = (byte) 231;
    numArray20[7] = (byte) 165;
    numArray20[49] = (byte) 209;
    numArray20[34] = (byte) 67;
    numArray20[35] = (byte) 15;
    numArray20[38] = (byte) 132;
    numArray20[29] = (byte) 197;
    numArray20[21] = (byte) 207;
    numArray20[3] = (byte) 230;
    numArray20[40] = (byte) 246;
    numArray20[41] = (byte) 29;
    numArray20[42] = (byte) 174;
    numArray20[36] = (byte) 57;
    numArray20[44] = (byte) 207;
    numArray20[20] = (byte) 139;
    numArray20[48 /*0x30*/] = (byte) 4;
    numArray20[28] = (byte) 136;
    numArray20[23] = (byte) 120;
    numArray20[46] = (byte) 208 /*0xD0*/;
    numArray20[19] = (byte) 225;
    numArray20[39] = (byte) 93;
    numArray20[52] = (byte) 69;
    numArray20[53] = (byte) 64 /*0x40*/;
    numArray20[8] = (byte) 20;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[8];
    numArray21[1] = (byte) 246;
    numArray21[5] = (byte) 133;
    numArray21[3] = (byte) 57;
    numArray21[0] = (byte) 211;
    numArray21[6] = (byte) 127 /*0x7F*/;
    numArray21[4] = (byte) 222;
    numArray21[2] = (byte) 172;
    numArray21[7] = (byte) 166;
    byte[] numArray22 = new byte[8];
    numArray22[3] = (byte) 97;
    numArray22[1] = (byte) 204;
    numArray22[2] = (byte) 71;
    numArray22[0] = (byte) 141;
    numArray22[6] = (byte) 192 /*0xC0*/;
    numArray22[5] = (byte) 0;
    numArray22[4] = (byte) 149;
    numArray22[7] = (byte) 232;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 8);
    for (int index = 0; index < 8; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_13466()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[246];
      byte[] numArray2 = new byte[55];
      numArray2[20] = (byte) 126;
      numArray2[1] = (byte) 12;
      numArray2[2] = (byte) 178;
      numArray2[36] = (byte) 98;
      numArray2[44] = (byte) 197;
      numArray2[5] = (byte) 187;
      numArray2[6] = (byte) 174;
      numArray2[11] = (byte) 165;
      numArray2[52] = (byte) 142;
      numArray2[9] = (byte) 74;
      numArray2[27] = (byte) 22;
      numArray2[8] = (byte) 223;
      numArray2[12] = (byte) 89;
      numArray2[39] = (byte) 194;
      numArray2[14] = (byte) 131;
      numArray2[15] = (byte) 162;
      numArray2[0] = (byte) 254;
      numArray2[17] = (byte) 210;
      numArray2[18] = (byte) 78;
      numArray2[19] = (byte) 61;
      numArray2[48 /*0x30*/] = (byte) 57;
      numArray2[21] = (byte) 215;
      numArray2[37] = (byte) 85;
      numArray2[23] = (byte) 133;
      numArray2[24] = (byte) 146;
      numArray2[28] = (byte) 156;
      numArray2[38] = (byte) 154;
      numArray2[26] = (byte) 251;
      numArray2[50] = (byte) 128 /*0x80*/;
      numArray2[29] = (byte) 158;
      numArray2[54] = (byte) 171;
      numArray2[7] = (byte) 254;
      numArray2[32 /*0x20*/] = (byte) 117;
      numArray2[33] = (byte) 85;
      numArray2[34] = (byte) 167;
      numArray2[31 /*0x1F*/] = (byte) 1;
      numArray2[10] = (byte) 15;
      numArray2[35] = (byte) 75;
      numArray2[51] = (byte) 246;
      numArray2[25] = (byte) 73;
      numArray2[40] = (byte) 194;
      numArray2[41] = (byte) 183;
      numArray2[30] = (byte) 112 /*0x70*/;
      numArray2[43] = (byte) 146;
      numArray2[22] = (byte) 74;
      numArray2[45] = (byte) 215;
      numArray2[46] = (byte) 165;
      numArray2[47] = (byte) 22;
      numArray2[53] = (byte) 210;
      numArray2[49] = (byte) 184;
      numArray2[3] = (byte) 170;
      numArray2[16 /*0x10*/] = (byte) 142;
      numArray2[13] = (byte) 128 /*0x80*/;
      numArray2[42] = (byte) 72;
      numArray2[4] = (byte) 77;
      byte[] numArray3 = new byte[55]
      {
        (byte) 1,
        (byte) 129,
        (byte) 2,
        (byte) 51,
        (byte) 252,
        (byte) 168,
        (byte) 133,
        (byte) 230,
        (byte) 128 /*0x80*/,
        (byte) 139,
        (byte) 98,
        (byte) 236,
        (byte) 114,
        (byte) 124,
        (byte) 222,
        (byte) 220,
        (byte) 188,
        (byte) 160 /*0xA0*/,
        (byte) 18,
        (byte) 19,
        (byte) 182,
        (byte) 155,
        (byte) 249,
        (byte) 180,
        (byte) 156,
        (byte) 238,
        (byte) 156,
        (byte) 214,
        (byte) 203,
        (byte) 26,
        (byte) 144 /*0x90*/,
        (byte) 161,
        (byte) 52,
        (byte) 110,
        (byte) 163,
        (byte) 12,
        (byte) 111,
        (byte) 112 /*0x70*/,
        (byte) 192 /*0xC0*/,
        (byte) 214,
        (byte) 105,
        (byte) 86,
        (byte) 121,
        (byte) 196,
        (byte) 203,
        (byte) 232,
        (byte) 144 /*0x90*/,
        (byte) 73,
        (byte) 228,
        (byte) 188,
        (byte) 157,
        (byte) 182,
        (byte) 72,
        (byte) 245,
        (byte) 217
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 109,
        (byte) 92,
        (byte) 96 /*0x60*/,
        (byte) 218,
        (byte) 186,
        (byte) 125,
        (byte) 33,
        (byte) 96 /*0x60*/,
        (byte) 155,
        (byte) 141,
        (byte) 91,
        (byte) 89,
        (byte) 174,
        (byte) 10,
        (byte) 250,
        (byte) 161,
        (byte) 138,
        (byte) 81,
        (byte) 113,
        (byte) 135,
        byte.MaxValue,
        (byte) 7,
        (byte) 20,
        (byte) 2,
        (byte) 168,
        (byte) 220,
        (byte) 108,
        (byte) 214,
        (byte) 187,
        (byte) 113,
        (byte) 76,
        (byte) 197,
        (byte) 230,
        (byte) 127 /*0x7F*/,
        (byte) 200,
        (byte) 225,
        (byte) 53,
        (byte) 40,
        (byte) 111,
        (byte) 215,
        (byte) 230,
        (byte) 88,
        (byte) 28,
        (byte) 26,
        (byte) 69,
        (byte) 227,
        (byte) 82,
        (byte) 109,
        (byte) 77,
        (byte) 28,
        (byte) 119,
        (byte) 5,
        (byte) 114,
        (byte) 17,
        (byte) 10
      };
      byte[] numArray5 = new byte[55];
      numArray5[4] = (byte) 129;
      numArray5[9] = (byte) 69;
      numArray5[2] = (byte) 99;
      numArray5[38] = (byte) 247;
      numArray5[35] = (byte) 180;
      numArray5[5] = (byte) 228;
      numArray5[29] = (byte) 28;
      numArray5[6] = (byte) 31 /*0x1F*/;
      numArray5[39] = (byte) 195;
      numArray5[24] = (byte) 168;
      numArray5[10] = (byte) 130;
      numArray5[11] = (byte) 155;
      numArray5[12] = (byte) 169;
      numArray5[13] = (byte) 167;
      numArray5[14] = (byte) 28;
      numArray5[15] = (byte) 48 /*0x30*/;
      numArray5[16 /*0x10*/] = (byte) 173;
      numArray5[7] = (byte) 138;
      numArray5[18] = (byte) 134;
      numArray5[28] = (byte) 0;
      numArray5[30] = (byte) 69;
      numArray5[46] = (byte) 87;
      numArray5[22] = (byte) 89;
      numArray5[23] = (byte) 168;
      numArray5[37] = (byte) 208 /*0xD0*/;
      numArray5[44] = (byte) 187;
      numArray5[26] = (byte) 48 /*0x30*/;
      numArray5[27] = (byte) 103;
      numArray5[21] = (byte) 230;
      numArray5[48 /*0x30*/] = (byte) 181;
      numArray5[40] = (byte) 200;
      numArray5[52] = (byte) 119;
      numArray5[1] = (byte) 188;
      numArray5[33] = (byte) 7;
      numArray5[34] = (byte) 226;
      numArray5[50] = (byte) 120;
      numArray5[36] = (byte) 79;
      numArray5[20] = (byte) 231;
      numArray5[19] = (byte) 85;
      numArray5[45] = (byte) 182;
      numArray5[3] = (byte) 60;
      numArray5[41] = (byte) 61;
      numArray5[17] = (byte) 91;
      numArray5[43] = (byte) 194;
      numArray5[0] = (byte) 218;
      numArray5[8] = (byte) 34;
      numArray5[42] = (byte) 142;
      numArray5[53] = (byte) 238;
      numArray5[32 /*0x20*/] = (byte) 8;
      numArray5[49] = (byte) 65;
      numArray5[54] = (byte) 170;
      numArray5[51] = (byte) 141;
      numArray5[25] = (byte) 183;
      numArray5[31 /*0x1F*/] = (byte) 135;
      numArray5[47] = (byte) 144 /*0x90*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[5] = (byte) 5;
      numArray6[41] = (byte) 222;
      numArray6[2] = (byte) 83;
      numArray6[15] = (byte) 80 /*0x50*/;
      numArray6[32 /*0x20*/] = (byte) 232;
      numArray6[39] = (byte) 173;
      numArray6[28] = (byte) 35;
      numArray6[7] = (byte) 250;
      numArray6[3] = (byte) 18;
      numArray6[12] = (byte) 126;
      numArray6[31 /*0x1F*/] = (byte) 17;
      numArray6[11] = (byte) 223;
      numArray6[38] = (byte) 218;
      numArray6[16 /*0x10*/] = (byte) 212;
      numArray6[14] = (byte) 106;
      numArray6[18] = (byte) 244;
      numArray6[33] = (byte) 76;
      numArray6[13] = (byte) 148;
      numArray6[9] = (byte) 157;
      numArray6[19] = (byte) 247;
      numArray6[8] = (byte) 67;
      numArray6[0] = (byte) 160 /*0xA0*/;
      numArray6[22] = (byte) 83;
      numArray6[37] = (byte) 179;
      numArray6[24] = (byte) 212;
      numArray6[25] = (byte) 84;
      numArray6[26] = (byte) 237;
      numArray6[27] = (byte) 227;
      numArray6[6] = (byte) 47;
      numArray6[44] = (byte) 103;
      numArray6[40] = (byte) 195;
      numArray6[54] = (byte) 44;
      numArray6[10] = (byte) 187;
      numArray6[30] = (byte) 100;
      numArray6[20] = (byte) 32 /*0x20*/;
      numArray6[35] = (byte) 137;
      numArray6[36] = (byte) 61;
      numArray6[21] = (byte) 12;
      numArray6[47] = (byte) 28;
      numArray6[23] = (byte) 104;
      numArray6[4] = (byte) 193;
      numArray6[45] = (byte) 74;
      numArray6[17] = (byte) 5;
      numArray6[43] = (byte) 211;
      numArray6[29] = (byte) 121;
      numArray6[42] = (byte) 108;
      numArray6[46] = (byte) 182;
      numArray6[51] = (byte) 106;
      numArray6[48 /*0x30*/] = (byte) 171;
      numArray6[49] = (byte) 211;
      numArray6[34] = (byte) 55;
      numArray6[1] = (byte) 109;
      numArray6[52] = (byte) 110;
      numArray6[53] = (byte) 130;
      numArray6[50] = (byte) 4;
      byte[] numArray7 = new byte[55]
      {
        (byte) 141,
        (byte) 216,
        (byte) 26,
        (byte) 27,
        (byte) 76,
        (byte) 209,
        (byte) 193,
        (byte) 122,
        (byte) 141,
        (byte) 122,
        (byte) 147,
        (byte) 58,
        (byte) 70,
        (byte) 134,
        (byte) 13,
        (byte) 30,
        (byte) 218,
        (byte) 9,
        (byte) 22,
        (byte) 80 /*0x50*/,
        (byte) 251,
        (byte) 62,
        (byte) 29,
        (byte) 200,
        (byte) 96 /*0x60*/,
        (byte) 32 /*0x20*/,
        (byte) 120,
        (byte) 50,
        (byte) 165,
        (byte) 84,
        (byte) 193,
        (byte) 135,
        (byte) 160 /*0xA0*/,
        (byte) 137,
        (byte) 44,
        (byte) 171,
        (byte) 181,
        (byte) 91,
        (byte) 114,
        (byte) 61,
        (byte) 178,
        (byte) 7,
        (byte) 234,
        (byte) 218,
        (byte) 9,
        (byte) 214,
        (byte) 231,
        (byte) 160 /*0xA0*/,
        (byte) 225,
        (byte) 129,
        (byte) 63 /*0x3F*/,
        (byte) 48 /*0x30*/,
        (byte) 100,
        (byte) 68,
        (byte) 202
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[20] = (byte) 231;
      numArray8[16 /*0x10*/] = (byte) 176 /*0xB0*/;
      numArray8[2] = (byte) 207;
      numArray8[3] = (byte) 70;
      numArray8[53] = (byte) 244;
      numArray8[5] = (byte) 135;
      numArray8[43] = (byte) 194;
      numArray8[21] = (byte) 66;
      numArray8[8] = (byte) 5;
      numArray8[9] = (byte) 145;
      numArray8[1] = (byte) 180;
      numArray8[11] = (byte) 184;
      numArray8[12] = (byte) 112 /*0x70*/;
      numArray8[7] = (byte) 63 /*0x3F*/;
      numArray8[47] = (byte) 0;
      numArray8[41] = (byte) 90;
      numArray8[26] = (byte) 143;
      numArray8[17] = (byte) 188;
      numArray8[6] = (byte) 85;
      numArray8[52] = (byte) 176 /*0xB0*/;
      numArray8[13] = (byte) 192 /*0xC0*/;
      numArray8[19] = (byte) 254;
      numArray8[36] = (byte) 157;
      numArray8[38] = (byte) 100;
      numArray8[51] = (byte) 145;
      numArray8[25] = (byte) 110;
      numArray8[10] = (byte) 18;
      numArray8[27] = (byte) 145;
      numArray8[42] = (byte) 184;
      numArray8[0] = (byte) 234;
      numArray8[30] = (byte) 24;
      numArray8[31 /*0x1F*/] = (byte) 52;
      numArray8[32 /*0x20*/] = (byte) 176 /*0xB0*/;
      numArray8[23] = (byte) 24;
      numArray8[34] = (byte) 103;
      numArray8[35] = (byte) 192 /*0xC0*/;
      numArray8[39] = (byte) 128 /*0x80*/;
      numArray8[37] = (byte) 79;
      numArray8[14] = (byte) 172;
      numArray8[29] = (byte) 123;
      numArray8[40] = (byte) 76;
      numArray8[22] = (byte) 54;
      numArray8[33] = (byte) 216;
      numArray8[28] = (byte) 186;
      numArray8[44] = (byte) 113;
      numArray8[45] = (byte) 7;
      numArray8[46] = (byte) 89;
      numArray8[18] = (byte) 58;
      numArray8[48 /*0x30*/] = (byte) 54;
      numArray8[49] = (byte) 128 /*0x80*/;
      numArray8[50] = (byte) 119;
      numArray8[4] = (byte) 18;
      numArray8[24] = (byte) 17;
      numArray8[15] = (byte) 190;
      numArray8[54] = (byte) 177;
      byte[] numArray9 = new byte[55]
      {
        (byte) 63 /*0x3F*/,
        (byte) 107,
        (byte) 126,
        (byte) 31 /*0x1F*/,
        (byte) 227,
        (byte) 171,
        (byte) 151,
        (byte) 92,
        (byte) 7,
        (byte) 145,
        (byte) 153,
        (byte) 80 /*0x50*/,
        (byte) 1,
        (byte) 138,
        (byte) 234,
        (byte) 252,
        (byte) 219,
        (byte) 129,
        (byte) 227,
        (byte) 144 /*0x90*/,
        (byte) 245,
        (byte) 249,
        (byte) 50,
        (byte) 76,
        (byte) 103,
        (byte) 59,
        (byte) 147,
        (byte) 16 /*0x10*/,
        (byte) 67,
        (byte) 188,
        (byte) 65,
        (byte) 105,
        (byte) 151,
        (byte) 161,
        (byte) 26,
        (byte) 229,
        (byte) 185,
        (byte) 71,
        (byte) 250,
        (byte) 53,
        (byte) 81,
        (byte) 70,
        (byte) 88,
        (byte) 2,
        (byte) 163,
        (byte) 6,
        (byte) 70,
        (byte) 221,
        (byte) 187,
        (byte) 71,
        (byte) 140,
        (byte) 224 /*0xE0*/,
        (byte) 234,
        (byte) 148,
        (byte) 95
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[26];
      numArray10[11] = (byte) 153;
      numArray10[1] = (byte) 44;
      numArray10[2] = (byte) 143;
      numArray10[7] = (byte) 241;
      numArray10[25] = (byte) 15;
      numArray10[21] = (byte) 19;
      numArray10[6] = (byte) 134;
      numArray10[16 /*0x10*/] = (byte) 50;
      numArray10[8] = (byte) 89;
      numArray10[23] = (byte) 83;
      numArray10[10] = (byte) 113;
      numArray10[18] = (byte) 183;
      numArray10[0] = (byte) 15;
      numArray10[3] = (byte) 104;
      numArray10[14] = (byte) 71;
      numArray10[17] = (byte) 40;
      numArray10[13] = (byte) 57;
      numArray10[12] = (byte) 168;
      numArray10[22] = (byte) 8;
      numArray10[19] = (byte) 92;
      numArray10[20] = (byte) 42;
      numArray10[4] = (byte) 104;
      numArray10[9] = (byte) 75;
      numArray10[15] = (byte) 151;
      numArray10[24] = (byte) 231;
      numArray10[5] = (byte) 75;
      byte[] numArray11 = new byte[26];
      numArray11[5] = (byte) 54;
      numArray11[13] = (byte) 92;
      numArray11[2] = (byte) 82;
      numArray11[0] = (byte) 126;
      numArray11[19] = (byte) 218;
      numArray11[14] = (byte) 173;
      numArray11[9] = (byte) 31 /*0x1F*/;
      numArray11[24] = (byte) 193;
      numArray11[8] = (byte) 173;
      numArray11[23] = (byte) 9;
      numArray11[10] = (byte) 222;
      numArray11[11] = (byte) 240 /*0xF0*/;
      numArray11[21] = (byte) 3;
      numArray11[4] = (byte) 26;
      numArray11[15] = (byte) 150;
      numArray11[12] = (byte) 110;
      numArray11[7] = (byte) 12;
      numArray11[17] = (byte) 70;
      numArray11[18] = (byte) 58;
      numArray11[16 /*0x10*/] = (byte) 216;
      numArray11[20] = (byte) 242;
      numArray11[1] = (byte) 111;
      numArray11[22] = (byte) 116;
      numArray11[3] = (byte) 11;
      numArray11[6] = (byte) 180;
      numArray11[25] = (byte) 171;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[246];
    byte[] numArray13 = new byte[55]
    {
      (byte) 179,
      (byte) 32 /*0x20*/,
      (byte) 172,
      (byte) 202,
      (byte) 94,
      (byte) 150,
      (byte) 225,
      (byte) 88,
      (byte) 219,
      (byte) 201,
      (byte) 244,
      (byte) 157,
      (byte) 200,
      (byte) 24,
      (byte) 16 /*0x10*/,
      (byte) 180,
      (byte) 110,
      (byte) 227,
      (byte) 226,
      (byte) 23,
      (byte) 50,
      (byte) 197,
      (byte) 115,
      (byte) 59,
      (byte) 177,
      (byte) 19,
      (byte) 62,
      (byte) 11,
      (byte) 14,
      (byte) 34,
      (byte) 208 /*0xD0*/,
      (byte) 58,
      (byte) 168,
      (byte) 235,
      (byte) 190,
      (byte) 119,
      (byte) 246,
      (byte) 0,
      (byte) 115,
      (byte) 216,
      (byte) 21,
      (byte) 23,
      (byte) 133,
      (byte) 144 /*0x90*/,
      (byte) 114,
      (byte) 116,
      (byte) 62,
      (byte) 22,
      (byte) 164,
      (byte) 28,
      (byte) 106,
      (byte) 112 /*0x70*/,
      (byte) 32 /*0x20*/,
      (byte) 193,
      (byte) 58
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 217,
      (byte) 22,
      (byte) 56,
      (byte) 14,
      (byte) 80 /*0x50*/,
      (byte) 231,
      (byte) 27,
      (byte) 156,
      (byte) 99,
      (byte) 227,
      (byte) 223,
      (byte) 221,
      (byte) 195,
      (byte) 59,
      (byte) 174,
      (byte) 135,
      (byte) 105,
      (byte) 56,
      (byte) 44,
      (byte) 235,
      (byte) 254,
      (byte) 173,
      (byte) 84,
      (byte) 117,
      (byte) 159,
      (byte) 239,
      (byte) 23,
      (byte) 214,
      (byte) 5,
      (byte) 210,
      (byte) 77,
      (byte) 155,
      (byte) 17,
      (byte) 50,
      (byte) 110,
      (byte) 37,
      (byte) 42,
      (byte) 62,
      (byte) 247,
      (byte) 202,
      (byte) 134,
      (byte) 8,
      (byte) 237,
      (byte) 191,
      (byte) 241,
      (byte) 121,
      (byte) 30,
      (byte) 25,
      (byte) 194,
      (byte) 26,
      (byte) 129,
      (byte) 26,
      (byte) 98,
      (byte) 135,
      (byte) 202
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[47] = (byte) 94;
    numArray15[35] = (byte) 231;
    numArray15[21] = (byte) 200;
    numArray15[46] = (byte) 11;
    numArray15[1] = (byte) 25;
    numArray15[5] = (byte) 74;
    numArray15[6] = (byte) 185;
    numArray15[2] = (byte) 75;
    numArray15[13] = (byte) 189;
    numArray15[54] = (byte) 201;
    numArray15[10] = (byte) 221;
    numArray15[48 /*0x30*/] = (byte) 252;
    numArray15[49] = (byte) 0;
    numArray15[0] = (byte) 113;
    numArray15[33] = (byte) 202;
    numArray15[15] = (byte) 112 /*0x70*/;
    numArray15[16 /*0x10*/] = (byte) 9;
    numArray15[27] = (byte) 183;
    numArray15[14] = (byte) 110;
    numArray15[19] = (byte) 63 /*0x3F*/;
    numArray15[4] = (byte) 213;
    numArray15[18] = (byte) 27;
    numArray15[28] = (byte) 95;
    numArray15[23] = (byte) 213;
    numArray15[3] = (byte) 87;
    numArray15[51] = (byte) 192 /*0xC0*/;
    numArray15[26] = (byte) 241;
    numArray15[44] = (byte) 96 /*0x60*/;
    numArray15[29] = (byte) 25;
    numArray15[25] = (byte) 103;
    numArray15[22] = (byte) 60;
    numArray15[7] = (byte) 249;
    numArray15[32 /*0x20*/] = (byte) 208 /*0xD0*/;
    numArray15[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
    numArray15[34] = (byte) 133;
    numArray15[9] = (byte) 245;
    numArray15[36] = (byte) 39;
    numArray15[37] = (byte) 128 /*0x80*/;
    numArray15[38] = (byte) 63 /*0x3F*/;
    numArray15[39] = (byte) 205;
    numArray15[40] = (byte) 203;
    numArray15[41] = (byte) 150;
    numArray15[50] = (byte) 22;
    numArray15[43] = (byte) 127 /*0x7F*/;
    numArray15[30] = (byte) 230;
    numArray15[20] = (byte) 227;
    numArray15[17] = (byte) 1;
    numArray15[24] = (byte) 39;
    numArray15[42] = (byte) 184;
    numArray15[11] = (byte) 89;
    numArray15[45] = (byte) 188;
    numArray15[8] = (byte) 186;
    numArray15[52] = (byte) 31 /*0x1F*/;
    numArray15[53] = (byte) 174;
    numArray15[12] = (byte) 153;
    byte[] numArray16 = new byte[55]
    {
      (byte) 182,
      (byte) 19,
      (byte) 122,
      (byte) 200,
      (byte) 184,
      (byte) 35,
      (byte) 243,
      (byte) 40,
      (byte) 153,
      (byte) 203,
      (byte) 208 /*0xD0*/,
      (byte) 75,
      (byte) 93,
      (byte) 54,
      (byte) 186,
      (byte) 111,
      (byte) 231,
      (byte) 142,
      (byte) 1,
      (byte) 179,
      (byte) 99,
      (byte) 81,
      (byte) 242,
      (byte) 112 /*0x70*/,
      (byte) 22,
      (byte) 203,
      (byte) 53,
      (byte) 7,
      (byte) 35,
      (byte) 102,
      (byte) 251,
      (byte) 79,
      (byte) 0,
      (byte) 249,
      (byte) 155,
      (byte) 20,
      (byte) 50,
      (byte) 103,
      (byte) 140,
      (byte) 163,
      (byte) 142,
      (byte) 96 /*0x60*/,
      (byte) 201,
      (byte) 166,
      (byte) 85,
      (byte) 219,
      (byte) 215,
      (byte) 50,
      (byte) 87,
      (byte) 85,
      (byte) 151,
      (byte) 37,
      (byte) 91,
      (byte) 236,
      (byte) 164
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[44] = (byte) 112 /*0x70*/;
    numArray17[53] = (byte) 159;
    numArray17[50] = (byte) 247;
    numArray17[37] = (byte) 173;
    numArray17[31 /*0x1F*/] = (byte) 132;
    numArray17[49] = (byte) 12;
    numArray17[39] = (byte) 194;
    numArray17[7] = (byte) 233;
    numArray17[8] = (byte) 155;
    numArray17[12] = (byte) 125;
    numArray17[33] = (byte) 233;
    numArray17[11] = (byte) 91;
    numArray17[2] = (byte) 19;
    numArray17[13] = (byte) 150;
    numArray17[14] = (byte) 56;
    numArray17[15] = (byte) 241;
    numArray17[40] = (byte) 123;
    numArray17[18] = (byte) 79;
    numArray17[6] = (byte) 156;
    numArray17[34] = (byte) 176 /*0xB0*/;
    numArray17[20] = (byte) 76;
    numArray17[21] = (byte) 64 /*0x40*/;
    numArray17[22] = (byte) 55;
    numArray17[23] = (byte) 30;
    numArray17[24] = (byte) 107;
    numArray17[32 /*0x20*/] = (byte) 68;
    numArray17[26] = (byte) 167;
    numArray17[27] = (byte) 118;
    numArray17[28] = (byte) 39;
    numArray17[29] = (byte) 192 /*0xC0*/;
    numArray17[30] = (byte) 230;
    numArray17[25] = (byte) 70;
    numArray17[19] = (byte) 248;
    numArray17[17] = (byte) 169;
    numArray17[1] = (byte) 196;
    numArray17[35] = (byte) 139;
    numArray17[36] = (byte) 189;
    numArray17[16 /*0x10*/] = (byte) 217;
    numArray17[38] = (byte) 131;
    numArray17[45] = (byte) 202;
    numArray17[41] = (byte) 134;
    numArray17[3] = (byte) 198;
    numArray17[42] = (byte) 152;
    numArray17[43] = (byte) 204;
    numArray17[51] = (byte) 145;
    numArray17[10] = (byte) 204;
    numArray17[46] = (byte) 247;
    numArray17[47] = (byte) 34;
    numArray17[54] = (byte) 211;
    numArray17[9] = (byte) 103;
    numArray17[48 /*0x30*/] = (byte) 84;
    numArray17[0] = (byte) 1;
    numArray17[52] = (byte) 45;
    numArray17[5] = (byte) 58;
    numArray17[4] = (byte) 70;
    byte[] numArray18 = new byte[55];
    numArray18[47] = (byte) 117;
    numArray18[1] = (byte) 109;
    numArray18[2] = (byte) 43;
    numArray18[3] = (byte) 206;
    numArray18[45] = (byte) 8;
    numArray18[46] = (byte) 253;
    numArray18[31 /*0x1F*/] = (byte) 249;
    numArray18[0] = (byte) 103;
    numArray18[43] = (byte) 93;
    numArray18[9] = (byte) 19;
    numArray18[10] = (byte) 94;
    numArray18[38] = (byte) 4;
    numArray18[12] = (byte) 232;
    numArray18[14] = (byte) 220;
    numArray18[23] = (byte) 54;
    numArray18[15] = (byte) 73;
    numArray18[7] = (byte) 128 /*0x80*/;
    numArray18[4] = (byte) 119;
    numArray18[18] = (byte) 243;
    numArray18[19] = (byte) 229;
    numArray18[20] = (byte) 26;
    numArray18[21] = (byte) 71;
    numArray18[22] = (byte) 54;
    numArray18[25] = (byte) 231;
    numArray18[24] = (byte) 121;
    numArray18[16 /*0x10*/] = (byte) 69;
    numArray18[51] = (byte) 140;
    numArray18[36] = (byte) 69;
    numArray18[28] = (byte) 72;
    numArray18[29] = (byte) 16 /*0x10*/;
    numArray18[27] = (byte) 242;
    numArray18[11] = (byte) 24;
    numArray18[6] = (byte) 237;
    numArray18[44] = (byte) 56;
    numArray18[17] = (byte) 27;
    numArray18[35] = (byte) 229;
    numArray18[26] = (byte) 25;
    numArray18[37] = (byte) 1;
    numArray18[30] = (byte) 249;
    numArray18[33] = (byte) 104;
    numArray18[48 /*0x30*/] = (byte) 16 /*0x10*/;
    numArray18[41] = (byte) 114;
    numArray18[42] = (byte) 102;
    numArray18[8] = (byte) 146;
    numArray18[32 /*0x20*/] = (byte) 42;
    numArray18[39] = (byte) 250;
    numArray18[40] = (byte) 43;
    numArray18[5] = (byte) 248;
    numArray18[13] = (byte) 235;
    numArray18[49] = (byte) 173;
    numArray18[50] = (byte) 103;
    numArray18[34] = (byte) 181;
    numArray18[52] = (byte) 144 /*0x90*/;
    numArray18[53] = (byte) 124;
    numArray18[54] = (byte) 213;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[15] = (byte) 208 /*0xD0*/;
    numArray19[1] = (byte) 204;
    numArray19[21] = (byte) 78;
    numArray19[3] = (byte) 197;
    numArray19[4] = (byte) 136;
    numArray19[38] = (byte) 201;
    numArray19[2] = (byte) 199;
    numArray19[7] = (byte) 66;
    numArray19[20] = (byte) 254;
    numArray19[5] = (byte) 100;
    numArray19[10] = (byte) 118;
    numArray19[8] = (byte) 58;
    numArray19[31 /*0x1F*/] = (byte) 249;
    numArray19[11] = (byte) 31 /*0x1F*/;
    numArray19[14] = (byte) 63 /*0x3F*/;
    numArray19[42] = (byte) 252;
    numArray19[16 /*0x10*/] = (byte) 60;
    numArray19[17] = (byte) 213;
    numArray19[18] = (byte) 225;
    numArray19[30] = (byte) 134;
    numArray19[34] = (byte) 190;
    numArray19[6] = (byte) 17;
    numArray19[22] = (byte) 171;
    numArray19[0] = (byte) 193;
    numArray19[24] = (byte) 3;
    numArray19[25] = (byte) 130;
    numArray19[26] = (byte) 212;
    numArray19[27] = (byte) 13;
    numArray19[28] = (byte) 30;
    numArray19[13] = (byte) 235;
    numArray19[48 /*0x30*/] = (byte) 177;
    numArray19[43] = (byte) 82;
    numArray19[50] = (byte) 6;
    numArray19[37] = (byte) 92;
    numArray19[47] = (byte) 130;
    numArray19[23] = (byte) 4;
    numArray19[36] = (byte) 9;
    numArray19[45] = (byte) 38;
    numArray19[29] = (byte) 115;
    numArray19[54] = (byte) 4;
    numArray19[33] = (byte) 218;
    numArray19[41] = (byte) 222;
    numArray19[39] = (byte) 185;
    numArray19[51] = (byte) 38;
    numArray19[44] = (byte) 110;
    numArray19[49] = (byte) 250;
    numArray19[46] = (byte) 39;
    numArray19[35] = (byte) 205;
    numArray19[32 /*0x20*/] = (byte) 170;
    numArray19[19] = (byte) 62;
    numArray19[12] = (byte) 208 /*0xD0*/;
    numArray19[52] = (byte) 44;
    numArray19[9] = (byte) 239;
    numArray19[53] = (byte) 238;
    numArray19[40] = (byte) 154;
    byte[] numArray20 = new byte[55]
    {
      (byte) 35,
      (byte) 18,
      (byte) 150,
      (byte) 232,
      (byte) 31 /*0x1F*/,
      (byte) 38,
      (byte) 169,
      (byte) 186,
      (byte) 235,
      (byte) 172,
      (byte) 185,
      (byte) 136,
      (byte) 111,
      (byte) 28,
      (byte) 68,
      (byte) 163,
      (byte) 69,
      (byte) 236,
      (byte) 238,
      (byte) 149,
      (byte) 69,
      (byte) 100,
      (byte) 220,
      (byte) 142,
      (byte) 27,
      (byte) 124,
      (byte) 111,
      (byte) 75,
      (byte) 29,
      (byte) 61,
      (byte) 77,
      (byte) 111,
      (byte) 135,
      (byte) 157,
      (byte) 30,
      (byte) 107,
      (byte) 8,
      (byte) 134,
      (byte) 6,
      (byte) 182,
      (byte) 241,
      (byte) 60,
      (byte) 194,
      (byte) 216,
      (byte) 173,
      (byte) 88,
      (byte) 201,
      (byte) 214,
      (byte) 88,
      (byte) 69,
      (byte) 35,
      (byte) 205,
      (byte) 35,
      (byte) 12,
      (byte) 50
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[26]
    {
      (byte) 45,
      (byte) 227,
      (byte) 171,
      (byte) 63 /*0x3F*/,
      (byte) 169,
      (byte) 0,
      (byte) 90,
      (byte) 9,
      (byte) 175,
      (byte) 184,
      (byte) 31 /*0x1F*/,
      (byte) 223,
      (byte) 198,
      (byte) 127 /*0x7F*/,
      (byte) 69,
      (byte) 72,
      (byte) 254,
      (byte) 13,
      (byte) 101,
      (byte) 29,
      (byte) 111,
      (byte) 163,
      (byte) 43,
      (byte) 40,
      (byte) 251,
      (byte) 27
    };
    byte[] numArray22 = new byte[26]
    {
      (byte) 139,
      (byte) 102,
      (byte) 117,
      (byte) 233,
      (byte) 45,
      (byte) 189,
      (byte) 103,
      byte.MaxValue,
      (byte) 14,
      (byte) 70,
      (byte) 174,
      (byte) 207,
      (byte) 95,
      (byte) 232,
      (byte) 93,
      (byte) 250,
      (byte) 248,
      (byte) 231,
      (byte) 30,
      (byte) 115,
      (byte) 158,
      (byte) 201,
      (byte) 92,
      (byte) 36,
      (byte) 169,
      (byte) 73
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 26);
    for (int index = 0; index < 26; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[47];
    byte[] response = new byte[47];
    Array.Copy((Array) sc_13393.sspq, 861, (Array) numArray23, 0, 47);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13393.sspr, 861, (Array) numArray23, 0, 47);
    for (int index = 0; index < numArray23.Length; ++index)
    {
      if ((int) numArray23[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_13467()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[206];
      byte[] numArray2 = new byte[55]
      {
        (byte) 22,
        (byte) 133,
        (byte) 163,
        (byte) 59,
        (byte) 99,
        (byte) 114,
        (byte) 194,
        (byte) 52,
        (byte) 115,
        (byte) 41,
        (byte) 138,
        (byte) 142,
        (byte) 254,
        (byte) 149,
        (byte) 166,
        (byte) 95,
        (byte) 170,
        (byte) 197,
        (byte) 26,
        (byte) 92,
        (byte) 178,
        (byte) 229,
        (byte) 194,
        (byte) 149,
        (byte) 77,
        (byte) 83,
        (byte) 162,
        (byte) 241,
        (byte) 28,
        (byte) 211,
        (byte) 191,
        (byte) 200,
        (byte) 41,
        (byte) 133,
        (byte) 107,
        (byte) 190,
        (byte) 234,
        (byte) 212,
        (byte) 177,
        (byte) 82,
        (byte) 35,
        (byte) 18,
        (byte) 185,
        (byte) 137,
        (byte) 88,
        (byte) 216,
        (byte) 143,
        (byte) 153,
        (byte) 110,
        (byte) 193,
        (byte) 1,
        (byte) 125,
        (byte) 117,
        (byte) 4,
        (byte) 128 /*0x80*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[7] = (byte) 31 /*0x1F*/;
      numArray3[20] = (byte) 127 /*0x7F*/;
      numArray3[5] = (byte) 183;
      numArray3[3] = (byte) 104;
      numArray3[25] = (byte) 242;
      numArray3[39] = (byte) 62;
      numArray3[52] = (byte) 94;
      numArray3[1] = (byte) 144 /*0x90*/;
      numArray3[41] = (byte) 4;
      numArray3[26] = (byte) 32 /*0x20*/;
      numArray3[10] = (byte) 43;
      numArray3[47] = (byte) 246;
      numArray3[12] = (byte) 18;
      numArray3[13] = (byte) 76;
      numArray3[50] = (byte) 145;
      numArray3[9] = (byte) 79;
      numArray3[16 /*0x10*/] = (byte) 234;
      numArray3[54] = (byte) 227;
      numArray3[18] = (byte) 89;
      numArray3[19] = (byte) 176 /*0xB0*/;
      numArray3[17] = (byte) 218;
      numArray3[11] = (byte) 228;
      numArray3[22] = (byte) 43;
      numArray3[23] = (byte) 164;
      numArray3[2] = (byte) 150;
      numArray3[42] = (byte) 82;
      numArray3[6] = (byte) 213;
      numArray3[0] = (byte) 254;
      numArray3[40] = (byte) 250;
      numArray3[29] = (byte) 35;
      numArray3[30] = (byte) 235;
      numArray3[31 /*0x1F*/] = (byte) 70;
      numArray3[32 /*0x20*/] = (byte) 85;
      numArray3[33] = (byte) 231;
      numArray3[36] = (byte) 148;
      numArray3[43] = (byte) 79;
      numArray3[21] = (byte) 74;
      numArray3[4] = (byte) 87;
      numArray3[49] = (byte) 122;
      numArray3[51] = (byte) 75;
      numArray3[14] = (byte) 57;
      numArray3[53] = (byte) 138;
      numArray3[24] = (byte) 241;
      numArray3[8] = (byte) 48 /*0x30*/;
      numArray3[44] = (byte) 65;
      numArray3[45] = (byte) 104;
      numArray3[46] = (byte) 162;
      numArray3[38] = (byte) 66;
      numArray3[48 /*0x30*/] = (byte) 85;
      numArray3[15] = (byte) 251;
      numArray3[34] = (byte) 198;
      numArray3[37] = (byte) 157;
      numArray3[35] = (byte) 80 /*0x50*/;
      numArray3[28] = (byte) 252;
      numArray3[27] = (byte) 90;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 208 /*0xD0*/,
        (byte) 121,
        (byte) 75,
        (byte) 199,
        (byte) 77,
        (byte) 188,
        (byte) 205,
        (byte) 40,
        (byte) 111,
        (byte) 63 /*0x3F*/,
        (byte) 116,
        (byte) 239,
        (byte) 236,
        (byte) 151,
        (byte) 240 /*0xF0*/,
        (byte) 115,
        (byte) 69,
        (byte) 229,
        (byte) 163,
        (byte) 19,
        (byte) 25,
        (byte) 166,
        (byte) 2,
        (byte) 174,
        (byte) 152,
        (byte) 253,
        (byte) 13,
        (byte) 167,
        (byte) 216,
        (byte) 165,
        (byte) 89,
        (byte) 35,
        (byte) 54,
        (byte) 113,
        (byte) 12,
        (byte) 108,
        (byte) 85,
        (byte) 187,
        (byte) 5,
        (byte) 62,
        (byte) 74,
        (byte) 247,
        (byte) 39,
        (byte) 51,
        (byte) 161,
        (byte) 128 /*0x80*/,
        (byte) 152,
        (byte) 181,
        (byte) 58,
        (byte) 245,
        (byte) 104,
        (byte) 230,
        (byte) 61,
        (byte) 240 /*0xF0*/,
        (byte) 112 /*0x70*/
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 179,
        (byte) 222,
        (byte) 32 /*0x20*/,
        (byte) 124,
        (byte) 220,
        byte.MaxValue,
        (byte) 182,
        (byte) 245,
        (byte) 254,
        (byte) 167,
        (byte) 177,
        (byte) 89,
        (byte) 111,
        (byte) 3,
        (byte) 183,
        (byte) 76,
        (byte) 9,
        (byte) 248,
        (byte) 206,
        (byte) 93,
        (byte) 206,
        (byte) 209,
        (byte) 29,
        (byte) 142,
        (byte) 222,
        (byte) 91,
        (byte) 135,
        (byte) 165,
        (byte) 169,
        (byte) 179,
        (byte) 171,
        (byte) 19,
        (byte) 231,
        (byte) 147,
        (byte) 236,
        (byte) 220,
        (byte) 28,
        (byte) 191,
        (byte) 93,
        (byte) 1,
        (byte) 70,
        (byte) 142,
        (byte) 85,
        (byte) 71,
        (byte) 192 /*0xC0*/,
        (byte) 201,
        (byte) 222,
        (byte) 179,
        (byte) 1,
        (byte) 155,
        (byte) 222,
        (byte) 157,
        (byte) 172,
        (byte) 172,
        (byte) 155
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 157,
        (byte) 46,
        (byte) 79,
        (byte) 175,
        (byte) 38,
        (byte) 79,
        (byte) 201,
        (byte) 230,
        (byte) 81,
        (byte) 185,
        (byte) 123,
        (byte) 139,
        (byte) 131,
        (byte) 29,
        (byte) 137,
        (byte) 223,
        (byte) 101,
        (byte) 200,
        (byte) 34,
        (byte) 126,
        (byte) 207,
        (byte) 13,
        (byte) 24,
        (byte) 100,
        (byte) 88,
        (byte) 108,
        (byte) 226,
        (byte) 60,
        (byte) 114,
        (byte) 229,
        (byte) 69,
        (byte) 0,
        (byte) 225,
        (byte) 245,
        (byte) 184,
        (byte) 141,
        (byte) 182,
        (byte) 143,
        (byte) 36,
        (byte) 167,
        (byte) 93,
        (byte) 76,
        (byte) 79,
        (byte) 198,
        (byte) 29,
        (byte) 41,
        (byte) 183,
        (byte) 216,
        (byte) 222,
        (byte) 6,
        (byte) 216,
        (byte) 40,
        (byte) 78,
        (byte) 171,
        (byte) 168
      };
      byte[] numArray7 = new byte[55];
      numArray7[27] = (byte) 96 /*0x60*/;
      numArray7[47] = (byte) 75;
      numArray7[2] = (byte) 2;
      numArray7[3] = (byte) 31 /*0x1F*/;
      numArray7[4] = (byte) 246;
      numArray7[5] = (byte) 8;
      numArray7[45] = (byte) 49;
      numArray7[7] = (byte) 103;
      numArray7[16 /*0x10*/] = (byte) 145;
      numArray7[9] = (byte) 66;
      numArray7[22] = (byte) 51;
      numArray7[11] = (byte) 81;
      numArray7[12] = (byte) 246;
      numArray7[26] = (byte) 29;
      numArray7[14] = (byte) 90;
      numArray7[1] = (byte) 38;
      numArray7[42] = (byte) 221;
      numArray7[17] = (byte) 51;
      numArray7[18] = (byte) 151;
      numArray7[49] = (byte) 136;
      numArray7[23] = (byte) 241;
      numArray7[20] = (byte) 222;
      numArray7[28] = (byte) 226;
      numArray7[19] = (byte) 91;
      numArray7[24] = (byte) 3;
      numArray7[25] = (byte) 192 /*0xC0*/;
      numArray7[6] = (byte) 196;
      numArray7[13] = (byte) 187;
      numArray7[0] = (byte) 204;
      numArray7[29] = (byte) 191;
      numArray7[38] = (byte) 145;
      numArray7[31 /*0x1F*/] = (byte) 126;
      numArray7[37] = (byte) 162;
      numArray7[32 /*0x20*/] = (byte) 52;
      numArray7[10] = (byte) 138;
      numArray7[35] = (byte) 5;
      numArray7[39] = (byte) 250;
      numArray7[34] = (byte) 167;
      numArray7[52] = (byte) 170;
      numArray7[43] = (byte) 168;
      numArray7[15] = (byte) 206;
      numArray7[46] = (byte) 54;
      numArray7[30] = (byte) 132;
      numArray7[33] = (byte) 73;
      numArray7[44] = (byte) 173;
      numArray7[41] = (byte) 242;
      numArray7[50] = (byte) 63 /*0x3F*/;
      numArray7[51] = (byte) 82;
      numArray7[48 /*0x30*/] = (byte) 66;
      numArray7[36] = (byte) 97;
      numArray7[21] = (byte) 247;
      numArray7[8] = (byte) 98;
      numArray7[40] = (byte) 59;
      numArray7[53] = (byte) 45;
      numArray7[54] = (byte) 247;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[41];
      numArray8[35] = (byte) 16 /*0x10*/;
      numArray8[17] = (byte) 79;
      numArray8[23] = (byte) 140;
      numArray8[28] = (byte) 153;
      numArray8[14] = (byte) 157;
      numArray8[5] = (byte) 7;
      numArray8[27] = (byte) 10;
      numArray8[38] = (byte) 162;
      numArray8[8] = (byte) 200;
      numArray8[9] = (byte) 115;
      numArray8[29] = (byte) 221;
      numArray8[7] = (byte) 87;
      numArray8[11] = (byte) 60;
      numArray8[13] = (byte) 93;
      numArray8[4] = (byte) 46;
      numArray8[12] = (byte) 131;
      numArray8[16 /*0x10*/] = (byte) 63 /*0x3F*/;
      numArray8[22] = (byte) 174;
      numArray8[18] = (byte) 169;
      numArray8[10] = (byte) 65;
      numArray8[2] = (byte) 4;
      numArray8[21] = (byte) 164;
      numArray8[19] = (byte) 219;
      numArray8[3] = (byte) 98;
      numArray8[24] = (byte) 151;
      numArray8[25] = (byte) 123;
      numArray8[6] = (byte) 95;
      numArray8[37] = (byte) 216;
      numArray8[15] = (byte) 57;
      numArray8[1] = (byte) 39;
      numArray8[30] = (byte) 170;
      numArray8[31 /*0x1F*/] = (byte) 39;
      numArray8[32 /*0x20*/] = (byte) 87;
      numArray8[33] = (byte) 179;
      numArray8[34] = (byte) 40;
      numArray8[40] = (byte) 130;
      numArray8[36] = (byte) 102;
      numArray8[20] = (byte) 254;
      numArray8[26] = (byte) 143;
      numArray8[39] = (byte) 209;
      numArray8[0] = (byte) 163;
      byte[] numArray9 = new byte[41]
      {
        (byte) 92,
        (byte) 176 /*0xB0*/,
        (byte) 6,
        (byte) 210,
        (byte) 196,
        (byte) 204,
        (byte) 190,
        (byte) 101,
        (byte) 15,
        (byte) 21,
        (byte) 186,
        (byte) 248,
        (byte) 84,
        (byte) 78,
        (byte) 218,
        (byte) 62,
        (byte) 189,
        (byte) 67,
        (byte) 82,
        (byte) 239,
        (byte) 213,
        (byte) 47,
        (byte) 119,
        (byte) 29,
        (byte) 152,
        (byte) 7,
        (byte) 237,
        (byte) 187,
        (byte) 16 /*0x10*/,
        byte.MaxValue,
        (byte) 0,
        (byte) 85,
        (byte) 164,
        (byte) 134,
        (byte) 222,
        (byte) 171,
        (byte) 105,
        (byte) 10,
        (byte) 116,
        (byte) 67,
        (byte) 159
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[206];
    byte[] numArray11 = new byte[55];
    numArray11[53] = (byte) 216;
    numArray11[1] = (byte) 171;
    numArray11[33] = (byte) 162;
    numArray11[3] = (byte) 238;
    numArray11[4] = (byte) 40;
    numArray11[49] = (byte) 16 /*0x10*/;
    numArray11[31 /*0x1F*/] = (byte) 174;
    numArray11[54] = (byte) 26;
    numArray11[27] = (byte) 209;
    numArray11[9] = (byte) 89;
    numArray11[36] = (byte) 131;
    numArray11[11] = (byte) 62;
    numArray11[51] = (byte) 199;
    numArray11[13] = (byte) 68;
    numArray11[14] = (byte) 206;
    numArray11[43] = (byte) 126;
    numArray11[2] = (byte) 97;
    numArray11[23] = (byte) 157;
    numArray11[18] = (byte) 215;
    numArray11[19] = (byte) 118;
    numArray11[20] = (byte) 46;
    numArray11[40] = (byte) 227;
    numArray11[28] = (byte) 104;
    numArray11[24] = (byte) 194;
    numArray11[5] = (byte) 151;
    numArray11[25] = (byte) 53;
    numArray11[26] = (byte) 216;
    numArray11[7] = (byte) 14;
    numArray11[44] = (byte) 42;
    numArray11[47] = (byte) 92;
    numArray11[30] = (byte) 102;
    numArray11[38] = (byte) 12;
    numArray11[32 /*0x20*/] = (byte) 110;
    numArray11[22] = (byte) 163;
    numArray11[34] = (byte) 89;
    numArray11[29] = (byte) 150;
    numArray11[17] = (byte) 73;
    numArray11[37] = (byte) 225;
    numArray11[0] = (byte) 111;
    numArray11[6] = (byte) 239;
    numArray11[52] = (byte) 206;
    numArray11[39] = (byte) 245;
    numArray11[42] = (byte) 190;
    numArray11[8] = (byte) 238;
    numArray11[41] = (byte) 88;
    numArray11[16 /*0x10*/] = (byte) 142;
    numArray11[46] = (byte) 218;
    numArray11[10] = (byte) 45;
    numArray11[48 /*0x30*/] = (byte) 215;
    numArray11[35] = (byte) 109;
    numArray11[50] = (byte) 141;
    numArray11[15] = (byte) 80 /*0x50*/;
    numArray11[21] = (byte) 141;
    numArray11[12] = (byte) 239;
    numArray11[45] = (byte) 196;
    byte[] numArray12 = new byte[55]
    {
      (byte) 160 /*0xA0*/,
      (byte) 198,
      (byte) 111,
      (byte) 136,
      (byte) 19,
      (byte) 19,
      (byte) 212,
      (byte) 222,
      (byte) 232,
      (byte) 155,
      (byte) 148,
      (byte) 247,
      (byte) 179,
      (byte) 118,
      (byte) 83,
      (byte) 72,
      (byte) 224 /*0xE0*/,
      (byte) 88,
      (byte) 191,
      (byte) 106,
      (byte) 59,
      (byte) 98,
      (byte) 24,
      (byte) 29,
      (byte) 12,
      (byte) 53,
      (byte) 202,
      (byte) 100,
      (byte) 176 /*0xB0*/,
      (byte) 104,
      (byte) 47,
      (byte) 63 /*0x3F*/,
      (byte) 213,
      (byte) 184,
      (byte) 236,
      (byte) 59,
      (byte) 222,
      (byte) 107,
      (byte) 231,
      (byte) 170,
      (byte) 12,
      (byte) 52,
      (byte) 19,
      (byte) 29,
      (byte) 238,
      (byte) 71,
      (byte) 216,
      (byte) 222,
      (byte) 219,
      (byte) 96 /*0x60*/,
      (byte) 215,
      (byte) 242,
      (byte) 166,
      (byte) 91,
      (byte) 9
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 81,
      (byte) 46,
      (byte) 152,
      (byte) 66,
      (byte) 34,
      (byte) 77,
      (byte) 78,
      (byte) 83,
      (byte) 159,
      (byte) 57,
      (byte) 196,
      (byte) 116,
      (byte) 250,
      (byte) 13,
      (byte) 246,
      (byte) 35,
      (byte) 170,
      (byte) 204,
      (byte) 36,
      (byte) 104,
      (byte) 154,
      (byte) 199,
      (byte) 8,
      (byte) 205,
      (byte) 10,
      (byte) 131,
      (byte) 20,
      (byte) 40,
      (byte) 20,
      (byte) 242,
      (byte) 80 /*0x50*/,
      (byte) 68,
      (byte) 100,
      (byte) 131,
      (byte) 110,
      (byte) 135,
      (byte) 129,
      (byte) 244,
      (byte) 164,
      (byte) 100,
      (byte) 24,
      (byte) 55,
      (byte) 12,
      (byte) 187,
      (byte) 201,
      (byte) 253,
      (byte) 124,
      (byte) 54,
      (byte) 234,
      (byte) 103,
      (byte) 66,
      (byte) 121,
      (byte) 32 /*0x20*/,
      (byte) 170,
      (byte) 71
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 110,
      (byte) 231,
      (byte) 104,
      (byte) 9,
      (byte) 248,
      (byte) 157,
      (byte) 100,
      (byte) 181,
      (byte) 130,
      (byte) 229,
      (byte) 122,
      (byte) 107,
      (byte) 60,
      (byte) 16 /*0x10*/,
      (byte) 19,
      (byte) 29,
      (byte) 79,
      (byte) 194,
      (byte) 10,
      (byte) 24,
      (byte) 198,
      (byte) 22,
      (byte) 59,
      (byte) 30,
      (byte) 227,
      (byte) 124,
      (byte) 200,
      (byte) 18,
      (byte) 98,
      (byte) 54,
      (byte) 221,
      (byte) 250,
      (byte) 187,
      (byte) 231,
      (byte) 14,
      (byte) 157,
      (byte) 226,
      (byte) 183,
      (byte) 205,
      (byte) 211,
      (byte) 132,
      (byte) 221,
      (byte) 223,
      (byte) 120,
      (byte) 245,
      (byte) 9,
      (byte) 72,
      (byte) 52,
      (byte) 95,
      (byte) 121,
      (byte) 104,
      (byte) 251,
      (byte) 8,
      (byte) 24,
      (byte) 38
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 203,
      (byte) 55,
      (byte) 158,
      (byte) 57,
      (byte) 38,
      (byte) 63 /*0x3F*/,
      (byte) 172,
      (byte) 233,
      (byte) 127 /*0x7F*/,
      (byte) 15,
      (byte) 118,
      (byte) 159,
      (byte) 110,
      (byte) 161,
      (byte) 181,
      (byte) 40,
      (byte) 191,
      (byte) 80 /*0x50*/,
      (byte) 98,
      (byte) 186,
      (byte) 77,
      (byte) 3,
      (byte) 13,
      (byte) 7,
      (byte) 222,
      (byte) 62,
      (byte) 172,
      (byte) 176 /*0xB0*/,
      (byte) 214,
      (byte) 36,
      (byte) 186,
      (byte) 60,
      (byte) 228,
      (byte) 98,
      (byte) 182,
      (byte) 188,
      (byte) 5,
      (byte) 43,
      (byte) 241,
      (byte) 248,
      (byte) 96 /*0x60*/,
      (byte) 209,
      (byte) 73,
      (byte) 237,
      (byte) 5,
      (byte) 150,
      (byte) 123,
      (byte) 197,
      (byte) 141,
      (byte) 81,
      (byte) 96 /*0x60*/,
      (byte) 90,
      (byte) 229,
      (byte) 207,
      (byte) 216
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 177,
      (byte) 175,
      (byte) 35,
      (byte) 16 /*0x10*/,
      (byte) 62,
      (byte) 253,
      (byte) 51,
      (byte) 64 /*0x40*/,
      (byte) 162,
      (byte) 2,
      (byte) 39,
      (byte) 89,
      (byte) 219,
      (byte) 157,
      (byte) 75,
      (byte) 242,
      (byte) 23,
      (byte) 17,
      (byte) 240 /*0xF0*/,
      (byte) 115,
      (byte) 141,
      (byte) 0,
      (byte) 8,
      (byte) 145,
      (byte) 33,
      (byte) 146,
      (byte) 251,
      (byte) 67,
      (byte) 201,
      (byte) 139,
      (byte) 238,
      (byte) 96 /*0x60*/,
      (byte) 41,
      (byte) 26,
      (byte) 142,
      (byte) 233,
      (byte) 35,
      (byte) 153,
      (byte) 236,
      (byte) 23,
      (byte) 165,
      (byte) 145,
      (byte) 134,
      (byte) 202,
      (byte) 234,
      (byte) 77,
      (byte) 225,
      (byte) 166,
      (byte) 10,
      (byte) 163,
      (byte) 20,
      (byte) 224 /*0xE0*/,
      (byte) 25,
      (byte) 104,
      (byte) 13
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[41];
    numArray17[39] = (byte) 191;
    numArray17[30] = (byte) 20;
    numArray17[2] = (byte) 232;
    numArray17[37] = (byte) 102;
    numArray17[4] = (byte) 252;
    numArray17[25] = (byte) 34;
    numArray17[6] = (byte) 91;
    numArray17[12] = (byte) 248;
    numArray17[5] = (byte) 18;
    numArray17[24] = (byte) 230;
    numArray17[10] = (byte) 12;
    numArray17[1] = (byte) 90;
    numArray17[36] = (byte) 8;
    numArray17[27] = (byte) 195;
    numArray17[15] = (byte) 94;
    numArray17[32 /*0x20*/] = (byte) 237;
    numArray17[16 /*0x10*/] = (byte) 105;
    numArray17[23] = (byte) 194;
    numArray17[38] = (byte) 100;
    numArray17[29] = (byte) 87;
    numArray17[20] = (byte) 31 /*0x1F*/;
    numArray17[28] = (byte) 95;
    numArray17[22] = (byte) 71;
    numArray17[7] = (byte) 85;
    numArray17[13] = (byte) 34;
    numArray17[34] = (byte) 110;
    numArray17[14] = (byte) 148;
    numArray17[3] = (byte) 146;
    numArray17[35] = (byte) 208 /*0xD0*/;
    numArray17[11] = (byte) 235;
    numArray17[21] = (byte) 1;
    numArray17[31 /*0x1F*/] = (byte) 138;
    numArray17[8] = (byte) 58;
    numArray17[33] = (byte) 146;
    numArray17[17] = (byte) 90;
    numArray17[19] = (byte) 185;
    numArray17[9] = (byte) 187;
    numArray17[18] = (byte) 123;
    numArray17[0] = (byte) 151;
    numArray17[26] = (byte) 133;
    numArray17[40] = (byte) 238;
    byte[] numArray18 = new byte[41];
    numArray18[40] = (byte) 239;
    numArray18[11] = (byte) 55;
    numArray18[2] = (byte) 55;
    numArray18[35] = (byte) 81;
    numArray18[3] = (byte) 135;
    numArray18[30] = (byte) 212;
    numArray18[6] = (byte) 235;
    numArray18[7] = (byte) 31 /*0x1F*/;
    numArray18[17] = (byte) 226;
    numArray18[9] = (byte) 116;
    numArray18[10] = (byte) 50;
    numArray18[4] = (byte) 55;
    numArray18[12] = (byte) 103;
    numArray18[13] = (byte) 6;
    numArray18[14] = (byte) 223;
    numArray18[1] = (byte) 167;
    numArray18[16 /*0x10*/] = (byte) 110;
    numArray18[23] = (byte) 241;
    numArray18[18] = (byte) 56;
    numArray18[19] = (byte) 183;
    numArray18[20] = (byte) 225;
    numArray18[21] = (byte) 27;
    numArray18[22] = (byte) 254;
    numArray18[0] = (byte) 195;
    numArray18[32 /*0x20*/] = (byte) 97;
    numArray18[25] = (byte) 87;
    numArray18[26] = (byte) 183;
    numArray18[27] = (byte) 43;
    numArray18[33] = (byte) 25;
    numArray18[29] = (byte) 201;
    numArray18[24] = (byte) 229;
    numArray18[31 /*0x1F*/] = (byte) 159;
    numArray18[37] = (byte) 78;
    numArray18[38] = (byte) 114;
    numArray18[15] = (byte) 89;
    numArray18[28] = (byte) 252;
    numArray18[36] = (byte) 223;
    numArray18[5] = (byte) 51;
    numArray18[34] = (byte) 5;
    numArray18[39] = (byte) 214;
    numArray18[8] = (byte) 84;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 41);
    for (int index = 0; index < 41; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_13468(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 138,
      (byte) 117,
      (byte) 228,
      (byte) 76,
      (byte) 55,
      (byte) 102,
      (byte) 48 /*0x30*/,
      (byte) 95,
      (byte) 221,
      (byte) 238,
      (byte) 189,
      (byte) 100,
      (byte) 33,
      (byte) 141,
      (byte) 154,
      (byte) 5,
      (byte) 120,
      (byte) 23,
      (byte) 16 /*0x10*/,
      (byte) 27,
      (byte) 63 /*0x3F*/,
      (byte) 252,
      (byte) 180,
      (byte) 17,
      (byte) 226,
      (byte) 46,
      (byte) 152,
      (byte) 189,
      (byte) 81,
      (byte) 158,
      (byte) 3,
      (byte) 50,
      (byte) 47,
      (byte) 19,
      (byte) 21,
      (byte) 206,
      (byte) 158,
      (byte) 19,
      (byte) 110,
      (byte) 107,
      (byte) 110,
      (byte) 235,
      (byte) 88,
      (byte) 178,
      (byte) 211,
      (byte) 162,
      (byte) 228,
      (byte) 198
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 183,
      (byte) 253,
      (byte) 39,
      (byte) 52,
      (byte) 125,
      (byte) 29,
      (byte) 141,
      (byte) 180,
      (byte) 239,
      (byte) 14,
      (byte) 152,
      (byte) 189,
      (byte) 218,
      (byte) 222,
      (byte) 241,
      (byte) 233,
      (byte) 205,
      (byte) 12,
      (byte) 196,
      (byte) 119,
      (byte) 238,
      (byte) 107,
      (byte) 54,
      (byte) 99,
      (byte) 30,
      (byte) 68,
      (byte) 130,
      (byte) 40,
      (byte) 115,
      (byte) 56,
      (byte) 86,
      (byte) 205,
      (byte) 237,
      (byte) 230,
      (byte) 214,
      (byte) 150,
      (byte) 24,
      (byte) 236,
      (byte) 160 /*0xA0*/,
      (byte) 130,
      (byte) 145,
      (byte) 252,
      (byte) 52,
      (byte) 172,
      (byte) 11,
      (byte) 67,
      (byte) 92,
      (byte) 193
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[49];
    byte[] response2 = new byte[49];
    Array.Copy((Array) sc_13393.sspq, 908, (Array) numArray2, 0, 49);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13393.sspr, 908, (Array) numArray2, 0, 49);
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

  internal static string ssp_appserver_13469()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41];
      numArray2[25] = (byte) 66;
      numArray2[1] = (byte) 184;
      numArray2[4] = (byte) 68;
      numArray2[23] = (byte) 154;
      numArray2[17] = (byte) 214;
      numArray2[27] = (byte) 177;
      numArray2[22] = (byte) 189;
      numArray2[36] = (byte) 9;
      numArray2[8] = (byte) 234;
      numArray2[9] = (byte) 189;
      numArray2[10] = (byte) 149;
      numArray2[11] = (byte) 41;
      numArray2[12] = (byte) 191;
      numArray2[13] = (byte) 251;
      numArray2[14] = (byte) 57;
      numArray2[15] = (byte) 146;
      numArray2[16 /*0x10*/] = (byte) 2;
      numArray2[39] = (byte) 19;
      numArray2[24] = (byte) 187;
      numArray2[19] = (byte) 51;
      numArray2[20] = (byte) 39;
      numArray2[6] = (byte) 128 /*0x80*/;
      numArray2[26] = (byte) 9;
      numArray2[2] = (byte) 113;
      numArray2[40] = (byte) 130;
      numArray2[21] = (byte) 241;
      numArray2[5] = (byte) 188;
      numArray2[3] = (byte) 253;
      numArray2[28] = (byte) 117;
      numArray2[7] = (byte) 215;
      numArray2[35] = (byte) 129;
      numArray2[31 /*0x1F*/] = (byte) 244;
      numArray2[32 /*0x20*/] = (byte) 211;
      numArray2[29] = (byte) 137;
      numArray2[34] = (byte) 132;
      numArray2[33] = (byte) 115;
      numArray2[0] = (byte) 77;
      numArray2[18] = (byte) 186;
      numArray2[38] = (byte) 208 /*0xD0*/;
      numArray2[30] = (byte) 60;
      numArray2[37] = (byte) 62;
      byte[] numArray3 = new byte[41]
      {
        (byte) 112 /*0x70*/,
        (byte) 165,
        (byte) 112 /*0x70*/,
        (byte) 225,
        (byte) 130,
        (byte) 20,
        (byte) 43,
        (byte) 73,
        (byte) 63 /*0x3F*/,
        (byte) 55,
        (byte) 129,
        (byte) 39,
        (byte) 20,
        (byte) 247,
        (byte) 192 /*0xC0*/,
        (byte) 101,
        (byte) 86,
        (byte) 168,
        (byte) 91,
        (byte) 22,
        (byte) 209,
        (byte) 191,
        (byte) 95,
        (byte) 233,
        (byte) 129,
        (byte) 148,
        (byte) 205,
        (byte) 153,
        (byte) 199,
        (byte) 140,
        (byte) 235,
        (byte) 74,
        (byte) 213,
        (byte) 81,
        (byte) 33,
        (byte) 169,
        (byte) 100,
        (byte) 18,
        (byte) 246,
        (byte) 50,
        (byte) 165
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 76,
      (byte) 103,
      (byte) 153,
      (byte) 196,
      (byte) 40,
      (byte) 134,
      (byte) 50,
      (byte) 251,
      (byte) 206,
      (byte) 96 /*0x60*/,
      (byte) 179,
      (byte) 173,
      (byte) 59,
      (byte) 208 /*0xD0*/,
      (byte) 149,
      (byte) 42,
      (byte) 91,
      (byte) 41,
      (byte) 9,
      (byte) 194,
      (byte) 83,
      (byte) 233,
      (byte) 230,
      (byte) 137,
      (byte) 151,
      (byte) 7,
      (byte) 142,
      (byte) 114,
      (byte) 8,
      (byte) 101,
      (byte) 75,
      (byte) 223,
      (byte) 57,
      (byte) 239,
      (byte) 52,
      (byte) 57,
      (byte) 189,
      (byte) 134,
      (byte) 78,
      (byte) 50,
      (byte) 86
    };
    byte[] numArray6 = new byte[41]
    {
      (byte) 77,
      (byte) 99,
      (byte) 20,
      (byte) 191,
      (byte) 8,
      (byte) 143,
      (byte) 248,
      (byte) 106,
      (byte) 173,
      (byte) 205,
      (byte) 25,
      (byte) 112 /*0x70*/,
      (byte) 16 /*0x10*/,
      (byte) 179,
      (byte) 217,
      (byte) 183,
      (byte) 35,
      (byte) 213,
      (byte) 126,
      (byte) 203,
      (byte) 19,
      (byte) 141,
      (byte) 201,
      (byte) 4,
      (byte) 220,
      (byte) 57,
      (byte) 184,
      (byte) 83,
      (byte) 71,
      (byte) 54,
      (byte) 242,
      (byte) 121,
      (byte) 205,
      (byte) 226,
      (byte) 194,
      (byte) 217,
      (byte) 240 /*0xF0*/,
      (byte) 36,
      (byte) 198,
      (byte) 187,
      (byte) 233
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13470()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[82];
      byte[] numArray2 = new byte[55]
      {
        (byte) 105,
        (byte) 24,
        (byte) 155,
        (byte) 206,
        (byte) 16 /*0x10*/,
        (byte) 64 /*0x40*/,
        (byte) 62,
        (byte) 230,
        (byte) 102,
        (byte) 30,
        (byte) 99,
        (byte) 128 /*0x80*/,
        (byte) 16 /*0x10*/,
        (byte) 44,
        (byte) 33,
        (byte) 247,
        (byte) 192 /*0xC0*/,
        (byte) 243,
        (byte) 104,
        (byte) 20,
        (byte) 212,
        (byte) 116,
        (byte) 76,
        (byte) 155,
        (byte) 92,
        (byte) 109,
        (byte) 212,
        (byte) 219,
        (byte) 214,
        (byte) 107,
        (byte) 137,
        (byte) 177,
        (byte) 160 /*0xA0*/,
        (byte) 139,
        (byte) 46,
        (byte) 238,
        (byte) 242,
        (byte) 164,
        (byte) 247,
        (byte) 36,
        (byte) 13,
        (byte) 248,
        (byte) 210,
        (byte) 208 /*0xD0*/,
        (byte) 209,
        (byte) 185,
        (byte) 173,
        (byte) 90,
        (byte) 44,
        (byte) 58,
        (byte) 150,
        (byte) 213,
        (byte) 207,
        (byte) 49,
        (byte) 41
      };
      byte[] numArray3 = new byte[55];
      numArray3[46] = (byte) 43;
      numArray3[1] = (byte) 199;
      numArray3[52] = (byte) 157;
      numArray3[2] = (byte) 215;
      numArray3[4] = (byte) 123;
      numArray3[5] = (byte) 247;
      numArray3[6] = (byte) 152;
      numArray3[31 /*0x1F*/] = (byte) 26;
      numArray3[18] = (byte) 86;
      numArray3[40] = (byte) 198;
      numArray3[10] = (byte) 240 /*0xF0*/;
      numArray3[11] = (byte) 31 /*0x1F*/;
      numArray3[45] = (byte) 203;
      numArray3[13] = (byte) 156;
      numArray3[0] = (byte) 249;
      numArray3[23] = (byte) 55;
      numArray3[7] = (byte) 155;
      numArray3[17] = (byte) 171;
      numArray3[36] = (byte) 231;
      numArray3[53] = (byte) 119;
      numArray3[20] = (byte) 172;
      numArray3[54] = (byte) 83;
      numArray3[22] = (byte) 108;
      numArray3[43] = (byte) 25;
      numArray3[24] = (byte) 41;
      numArray3[25] = (byte) 186;
      numArray3[21] = (byte) 97;
      numArray3[28] = (byte) 176 /*0xB0*/;
      numArray3[19] = (byte) 111;
      numArray3[29] = (byte) 94;
      numArray3[30] = (byte) 41;
      numArray3[27] = (byte) 135;
      numArray3[32 /*0x20*/] = (byte) 40;
      numArray3[38] = (byte) 46;
      numArray3[34] = (byte) 88;
      numArray3[48 /*0x30*/] = (byte) 206;
      numArray3[16 /*0x10*/] = (byte) 83;
      numArray3[33] = (byte) 201;
      numArray3[12] = (byte) 248;
      numArray3[39] = (byte) 18;
      numArray3[3] = (byte) 11;
      numArray3[41] = (byte) 119;
      numArray3[9] = (byte) 216;
      numArray3[26] = (byte) 182;
      numArray3[44] = (byte) 37;
      numArray3[35] = (byte) 187;
      numArray3[49] = (byte) 233;
      numArray3[15] = (byte) 225;
      numArray3[47] = (byte) 127 /*0x7F*/;
      numArray3[14] = (byte) 46;
      numArray3[50] = (byte) 69;
      numArray3[51] = (byte) 63 /*0x3F*/;
      numArray3[8] = (byte) 243;
      numArray3[37] = (byte) 5;
      numArray3[42] = (byte) 95;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[27]
      {
        (byte) 53,
        (byte) 206,
        (byte) 226,
        (byte) 242,
        (byte) 179,
        (byte) 115,
        (byte) 38,
        (byte) 224 /*0xE0*/,
        (byte) 209,
        (byte) 113,
        (byte) 34,
        (byte) 196,
        (byte) 15,
        (byte) 18,
        (byte) 233,
        (byte) 166,
        (byte) 138,
        (byte) 93,
        (byte) 242,
        (byte) 202,
        (byte) 64 /*0x40*/,
        (byte) 171,
        (byte) 201,
        (byte) 151,
        (byte) 29,
        (byte) 179,
        (byte) 102
      };
      byte[] numArray5 = new byte[27];
      numArray5[9] = (byte) 242;
      numArray5[5] = (byte) 136;
      numArray5[2] = (byte) 152;
      numArray5[3] = (byte) 120;
      numArray5[4] = (byte) 52;
      numArray5[13] = (byte) 192 /*0xC0*/;
      numArray5[20] = (byte) 108;
      numArray5[17] = (byte) 165;
      numArray5[8] = (byte) 101;
      numArray5[16 /*0x10*/] = (byte) 0;
      numArray5[6] = (byte) 237;
      numArray5[1] = (byte) 186;
      numArray5[0] = (byte) 187;
      numArray5[18] = (byte) 47;
      numArray5[14] = (byte) 27;
      numArray5[12] = (byte) 173;
      numArray5[24] = (byte) 68;
      numArray5[22] = (byte) 197;
      numArray5[11] = (byte) 82;
      numArray5[19] = (byte) 124;
      numArray5[7] = (byte) 75;
      numArray5[21] = (byte) 72;
      numArray5[10] = (byte) 181;
      numArray5[23] = (byte) 182;
      numArray5[25] = (byte) 133;
      numArray5[15] = (byte) 225;
      numArray5[26] = (byte) 68;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[82];
    byte[] numArray7 = new byte[55]
    {
      (byte) 74,
      (byte) 213,
      (byte) 0,
      (byte) 13,
      (byte) 204,
      (byte) 6,
      (byte) 124,
      (byte) 44,
      (byte) 14,
      (byte) 223,
      (byte) 59,
      (byte) 85,
      (byte) 133,
      (byte) 52,
      (byte) 197,
      (byte) 102,
      (byte) 125,
      (byte) 25,
      (byte) 229,
      (byte) 94,
      (byte) 97,
      (byte) 146,
      (byte) 134,
      (byte) 200,
      (byte) 66,
      (byte) 200,
      (byte) 165,
      (byte) 82,
      (byte) 120,
      (byte) 184,
      (byte) 93,
      (byte) 196,
      (byte) 51,
      (byte) 94,
      (byte) 39,
      (byte) 240 /*0xF0*/,
      (byte) 246,
      (byte) 103,
      (byte) 228,
      (byte) 84,
      (byte) 208 /*0xD0*/,
      (byte) 59,
      (byte) 246,
      (byte) 184,
      (byte) 175,
      (byte) 205,
      (byte) 11,
      byte.MaxValue,
      (byte) 60,
      (byte) 101,
      (byte) 221,
      (byte) 111,
      (byte) 42,
      (byte) 42,
      (byte) 163
    };
    byte[] numArray8 = new byte[55];
    numArray8[15] = (byte) 202;
    numArray8[1] = (byte) 225;
    numArray8[3] = (byte) 22;
    numArray8[36] = (byte) 69;
    numArray8[12] = (byte) 84;
    numArray8[5] = (byte) 134;
    numArray8[31 /*0x1F*/] = (byte) 50;
    numArray8[7] = (byte) 74;
    numArray8[8] = (byte) 107;
    numArray8[9] = (byte) 122;
    numArray8[10] = (byte) 176 /*0xB0*/;
    numArray8[50] = (byte) 215;
    numArray8[47] = (byte) 201;
    numArray8[13] = (byte) 242;
    numArray8[37] = (byte) 224 /*0xE0*/;
    numArray8[39] = (byte) 234;
    numArray8[16 /*0x10*/] = (byte) 54;
    numArray8[17] = (byte) 160 /*0xA0*/;
    numArray8[18] = (byte) 196;
    numArray8[19] = (byte) 132;
    numArray8[29] = (byte) 85;
    numArray8[20] = (byte) 40;
    numArray8[43] = (byte) 5;
    numArray8[2] = (byte) 91;
    numArray8[24] = (byte) 79;
    numArray8[35] = (byte) 61;
    numArray8[49] = (byte) 107;
    numArray8[33] = (byte) 151;
    numArray8[28] = (byte) 29;
    numArray8[40] = (byte) 58;
    numArray8[30] = (byte) 162;
    numArray8[52] = (byte) 214;
    numArray8[14] = (byte) 204;
    numArray8[48 /*0x30*/] = (byte) 39;
    numArray8[34] = (byte) 36;
    numArray8[0] = (byte) 216;
    numArray8[25] = (byte) 119;
    numArray8[42] = (byte) 4;
    numArray8[38] = (byte) 69;
    numArray8[21] = (byte) 54;
    numArray8[53] = (byte) 234;
    numArray8[22] = (byte) 114;
    numArray8[27] = (byte) 21;
    numArray8[32 /*0x20*/] = (byte) 252;
    numArray8[44] = (byte) 135;
    numArray8[45] = (byte) 128 /*0x80*/;
    numArray8[46] = (byte) 34;
    numArray8[6] = (byte) 94;
    numArray8[26] = (byte) 229;
    numArray8[23] = (byte) 10;
    numArray8[41] = (byte) 82;
    numArray8[51] = (byte) 12;
    numArray8[4] = (byte) 20;
    numArray8[11] = (byte) 249;
    numArray8[54] = (byte) 132;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[27];
    numArray9[22] = (byte) 201;
    numArray9[1] = (byte) 85;
    numArray9[2] = (byte) 71;
    numArray9[3] = (byte) 186;
    numArray9[20] = (byte) 151;
    numArray9[0] = (byte) 31 /*0x1F*/;
    numArray9[13] = (byte) 177;
    numArray9[4] = (byte) 19;
    numArray9[19] = (byte) 250;
    numArray9[6] = (byte) 122;
    numArray9[10] = (byte) 108;
    numArray9[12] = (byte) 50;
    numArray9[21] = (byte) 223;
    numArray9[8] = (byte) 221;
    numArray9[15] = (byte) 83;
    numArray9[7] = (byte) 147;
    numArray9[16 /*0x10*/] = (byte) 157;
    numArray9[17] = (byte) 231;
    numArray9[18] = (byte) 157;
    numArray9[5] = (byte) 160 /*0xA0*/;
    numArray9[14] = (byte) 119;
    numArray9[26] = (byte) 30;
    numArray9[11] = (byte) 157;
    numArray9[23] = (byte) 216;
    numArray9[24] = (byte) 178;
    numArray9[25] = (byte) 174;
    numArray9[9] = (byte) 25;
    byte[] numArray10 = new byte[27];
    numArray10[13] = (byte) 50;
    numArray10[19] = (byte) 182;
    numArray10[2] = (byte) 111;
    numArray10[16 /*0x10*/] = (byte) 133;
    numArray10[18] = (byte) 112 /*0x70*/;
    numArray10[7] = (byte) 131;
    numArray10[0] = (byte) 146;
    numArray10[20] = (byte) 95;
    numArray10[8] = (byte) 192 /*0xC0*/;
    numArray10[5] = (byte) 51;
    numArray10[10] = (byte) 37;
    numArray10[11] = (byte) 43;
    numArray10[12] = (byte) 126;
    numArray10[6] = (byte) 65;
    numArray10[14] = (byte) 106;
    numArray10[1] = (byte) 246;
    numArray10[3] = (byte) 158;
    numArray10[17] = (byte) 93;
    numArray10[15] = (byte) 36;
    numArray10[9] = (byte) 27;
    numArray10[4] = (byte) 47;
    numArray10[21] = (byte) 166;
    numArray10[23] = (byte) 60;
    numArray10[22] = (byte) 221;
    numArray10[24] = (byte) 150;
    numArray10[25] = (byte) 232;
    numArray10[26] = (byte) 210;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 27);
    for (int index = 0; index < 27; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[11];
    byte[] response = new byte[11];
    Array.Copy((Array) sc_13393.sspq, 957, (Array) numArray11, 0, 11);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13393.sspr, 957, (Array) numArray11, 0, 11);
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

  internal static int ssp_appserver_13471(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 227,
      (byte) 72,
      (byte) 11,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 32 /*0x20*/,
      (byte) 46,
      (byte) 150,
      (byte) 96 /*0x60*/,
      (byte) 43,
      (byte) 78,
      (byte) 244,
      (byte) 187,
      (byte) 85,
      (byte) 14,
      (byte) 171,
      (byte) 235,
      (byte) 166,
      (byte) 162,
      (byte) 20,
      (byte) 215,
      (byte) 248,
      (byte) 210,
      (byte) 26,
      (byte) 218,
      (byte) 178,
      (byte) 115,
      (byte) 142,
      (byte) 53,
      (byte) 38,
      (byte) 78,
      (byte) 19,
      (byte) 50,
      (byte) 4,
      (byte) 83,
      (byte) 24,
      (byte) 80 /*0x50*/,
      (byte) 87,
      (byte) 170,
      (byte) 163,
      (byte) 157,
      (byte) 110,
      (byte) 84,
      (byte) 49,
      (byte) 225,
      (byte) 66,
      (byte) 163,
      (byte) 102
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 225,
      (byte) 231,
      (byte) 82,
      (byte) 95,
      (byte) 82,
      (byte) 43,
      (byte) 198,
      (byte) 165,
      (byte) 216,
      (byte) 136,
      (byte) 183,
      (byte) 224 /*0xE0*/,
      (byte) 155,
      (byte) 158,
      (byte) 23,
      (byte) 105,
      (byte) 31 /*0x1F*/,
      (byte) 137,
      (byte) 236,
      (byte) 218,
      (byte) 76,
      (byte) 173,
      (byte) 235,
      (byte) 229,
      (byte) 160 /*0xA0*/,
      (byte) 82,
      (byte) 211,
      (byte) 155,
      (byte) 192 /*0xC0*/,
      (byte) 139,
      (byte) 0,
      (byte) 128 /*0x80*/,
      (byte) 217,
      (byte) 3,
      (byte) 149,
      (byte) 150,
      (byte) 91,
      (byte) 102,
      (byte) 133,
      (byte) 101,
      (byte) 52,
      (byte) 136,
      (byte) 169,
      (byte) 166,
      (byte) 119,
      (byte) 146,
      (byte) 235,
      (byte) 56
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13472()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[77];
      byte[] numArray2 = new byte[55];
      numArray2[38] = (byte) 162;
      numArray2[32 /*0x20*/] = (byte) 228;
      numArray2[2] = (byte) 168;
      numArray2[8] = (byte) 172;
      numArray2[4] = (byte) 117;
      numArray2[5] = (byte) 240 /*0xF0*/;
      numArray2[7] = (byte) 126;
      numArray2[31 /*0x1F*/] = (byte) 177;
      numArray2[6] = (byte) 71;
      numArray2[9] = (byte) 160 /*0xA0*/;
      numArray2[10] = (byte) 97;
      numArray2[11] = (byte) 58;
      numArray2[43] = (byte) 253;
      numArray2[13] = (byte) 58;
      numArray2[14] = (byte) 193;
      numArray2[15] = (byte) 183;
      numArray2[48 /*0x30*/] = (byte) 128 /*0x80*/;
      numArray2[0] = (byte) 30;
      numArray2[18] = (byte) 91;
      numArray2[19] = (byte) 148;
      numArray2[12] = (byte) 220;
      numArray2[34] = (byte) 245;
      numArray2[53] = (byte) 174;
      numArray2[24] = (byte) 163;
      numArray2[47] = (byte) 167;
      numArray2[3] = (byte) 235;
      numArray2[28] = (byte) 14;
      numArray2[27] = (byte) 152;
      numArray2[22] = (byte) 118;
      numArray2[29] = (byte) 12;
      numArray2[30] = (byte) 113;
      numArray2[45] = (byte) 26;
      numArray2[42] = (byte) 247;
      numArray2[51] = (byte) 37;
      numArray2[40] = (byte) 15;
      numArray2[35] = (byte) 133;
      numArray2[36] = (byte) 91;
      numArray2[37] = (byte) 103;
      numArray2[20] = (byte) 251;
      numArray2[39] = (byte) 95;
      numArray2[33] = (byte) 141;
      numArray2[16 /*0x10*/] = (byte) 2;
      numArray2[17] = (byte) 246;
      numArray2[26] = (byte) 57;
      numArray2[44] = (byte) 13;
      numArray2[21] = (byte) 254;
      numArray2[46] = (byte) 84;
      numArray2[23] = (byte) 104;
      numArray2[41] = (byte) 43;
      numArray2[49] = (byte) 236;
      numArray2[1] = (byte) 173;
      numArray2[25] = (byte) 161;
      numArray2[52] = (byte) 41;
      numArray2[54] = (byte) 196;
      numArray2[50] = (byte) 100;
      byte[] numArray3 = new byte[55]
      {
        (byte) 121,
        (byte) 112 /*0x70*/,
        (byte) 37,
        (byte) 238,
        (byte) 183,
        (byte) 86,
        (byte) 228,
        (byte) 191,
        (byte) 25,
        (byte) 105,
        (byte) 85,
        (byte) 251,
        (byte) 20,
        (byte) 143,
        (byte) 213,
        (byte) 216,
        (byte) 253,
        (byte) 252,
        (byte) 30,
        (byte) 18,
        (byte) 166,
        (byte) 40,
        (byte) 10,
        (byte) 109,
        (byte) 212,
        (byte) 199,
        (byte) 231,
        (byte) 67,
        (byte) 95,
        (byte) 251,
        (byte) 71,
        (byte) 249,
        (byte) 225,
        (byte) 194,
        (byte) 49,
        (byte) 154,
        (byte) 159,
        (byte) 188,
        (byte) 97,
        (byte) 78,
        (byte) 95,
        (byte) 52,
        (byte) 219,
        (byte) 248,
        (byte) 244,
        (byte) 247,
        (byte) 209,
        (byte) 242,
        (byte) 140,
        (byte) 232,
        (byte) 242,
        (byte) 99,
        (byte) 42,
        (byte) 206,
        (byte) 183
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22];
      numArray4[15] = (byte) 132;
      numArray4[16 /*0x10*/] = (byte) 83;
      numArray4[9] = (byte) 31 /*0x1F*/;
      numArray4[1] = (byte) 191;
      numArray4[4] = (byte) 151;
      numArray4[19] = (byte) 176 /*0xB0*/;
      numArray4[6] = (byte) 248;
      numArray4[5] = (byte) 77;
      numArray4[8] = (byte) 40;
      numArray4[11] = (byte) 110;
      numArray4[10] = (byte) 133;
      numArray4[0] = (byte) 228;
      numArray4[12] = (byte) 236;
      numArray4[13] = (byte) 63 /*0x3F*/;
      numArray4[7] = (byte) 226;
      numArray4[3] = (byte) 38;
      numArray4[2] = (byte) 135;
      numArray4[17] = (byte) 37;
      numArray4[18] = (byte) 20;
      numArray4[20] = (byte) 169;
      numArray4[14] = (byte) 122;
      numArray4[21] = (byte) 254;
      byte[] numArray5 = new byte[22]
      {
        (byte) 209,
        (byte) 120,
        (byte) 212,
        (byte) 211,
        (byte) 204,
        (byte) 184,
        (byte) 66,
        (byte) 186,
        (byte) 119,
        (byte) 81,
        (byte) 153,
        (byte) 143,
        (byte) 34,
        (byte) 125,
        (byte) 193,
        (byte) 245,
        (byte) 176 /*0xB0*/,
        (byte) 96 /*0x60*/,
        (byte) 127 /*0x7F*/,
        (byte) 104,
        (byte) 22,
        (byte) 11
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[77];
    byte[] numArray7 = new byte[55];
    numArray7[52] = (byte) 208 /*0xD0*/;
    numArray7[1] = (byte) 79;
    numArray7[7] = (byte) 19;
    numArray7[13] = (byte) 188;
    numArray7[43] = (byte) 101;
    numArray7[9] = (byte) 251;
    numArray7[41] = (byte) 126;
    numArray7[26] = (byte) 166;
    numArray7[8] = (byte) 229;
    numArray7[51] = (byte) 145;
    numArray7[10] = (byte) 65;
    numArray7[11] = (byte) 246;
    numArray7[38] = (byte) 8;
    numArray7[27] = (byte) 82;
    numArray7[0] = (byte) 80 /*0x50*/;
    numArray7[36] = (byte) 45;
    numArray7[16 /*0x10*/] = (byte) 206;
    numArray7[14] = (byte) 26;
    numArray7[18] = (byte) 176 /*0xB0*/;
    numArray7[31 /*0x1F*/] = (byte) 197;
    numArray7[20] = (byte) 90;
    numArray7[17] = (byte) 19;
    numArray7[22] = (byte) 25;
    numArray7[23] = (byte) 61;
    numArray7[24] = (byte) 209;
    numArray7[49] = (byte) 133;
    numArray7[3] = (byte) 218;
    numArray7[42] = (byte) 82;
    numArray7[28] = (byte) 188;
    numArray7[29] = (byte) 202;
    numArray7[30] = (byte) 30;
    numArray7[6] = (byte) 104;
    numArray7[32 /*0x20*/] = (byte) 59;
    numArray7[33] = (byte) 122;
    numArray7[34] = (byte) 64 /*0x40*/;
    numArray7[53] = (byte) 65;
    numArray7[19] = (byte) 177;
    numArray7[37] = (byte) 127 /*0x7F*/;
    numArray7[47] = (byte) 177;
    numArray7[21] = (byte) 8;
    numArray7[25] = (byte) 93;
    numArray7[2] = (byte) 77;
    numArray7[12] = (byte) 197;
    numArray7[4] = (byte) 3;
    numArray7[44] = (byte) 6;
    numArray7[45] = (byte) 139;
    numArray7[46] = (byte) 2;
    numArray7[35] = (byte) 142;
    numArray7[48 /*0x30*/] = (byte) 68;
    numArray7[50] = (byte) 102;
    numArray7[39] = (byte) 244;
    numArray7[54] = (byte) 254;
    numArray7[15] = (byte) 104;
    numArray7[40] = (byte) 84;
    numArray7[5] = (byte) 224 /*0xE0*/;
    byte[] numArray8 = new byte[55]
    {
      (byte) 191,
      (byte) 47,
      (byte) 7,
      (byte) 238,
      (byte) 67,
      (byte) 42,
      (byte) 25,
      (byte) 142,
      (byte) 42,
      (byte) 78,
      (byte) 3,
      (byte) 121,
      (byte) 230,
      (byte) 231,
      (byte) 118,
      (byte) 91,
      (byte) 158,
      (byte) 247,
      (byte) 23,
      (byte) 151,
      (byte) 240 /*0xF0*/,
      (byte) 114,
      (byte) 230,
      (byte) 240 /*0xF0*/,
      (byte) 236,
      (byte) 58,
      (byte) 221,
      (byte) 244,
      (byte) 227,
      (byte) 123,
      (byte) 19,
      (byte) 192 /*0xC0*/,
      (byte) 34,
      (byte) 151,
      (byte) 251,
      (byte) 124,
      (byte) 40,
      (byte) 240 /*0xF0*/,
      (byte) 14,
      (byte) 42,
      byte.MaxValue,
      (byte) 94,
      (byte) 148,
      (byte) 118,
      (byte) 137,
      (byte) 15,
      (byte) 75,
      (byte) 30,
      (byte) 106,
      (byte) 169,
      (byte) 67,
      (byte) 217,
      (byte) 125,
      (byte) 122,
      (byte) 112 /*0x70*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[22];
    numArray9[0] = (byte) 216;
    numArray9[11] = (byte) 131;
    numArray9[19] = (byte) 126;
    numArray9[3] = (byte) 69;
    numArray9[4] = (byte) 7;
    numArray9[5] = (byte) 32 /*0x20*/;
    numArray9[15] = (byte) 167;
    numArray9[2] = (byte) 75;
    numArray9[8] = (byte) 134;
    numArray9[13] = (byte) 29;
    numArray9[6] = (byte) 183;
    numArray9[10] = (byte) 207;
    numArray9[14] = (byte) 215;
    numArray9[1] = (byte) 194;
    numArray9[17] = (byte) 29;
    numArray9[9] = (byte) 193;
    numArray9[16 /*0x10*/] = (byte) 0;
    numArray9[7] = (byte) 80 /*0x50*/;
    numArray9[18] = (byte) 180;
    numArray9[20] = (byte) 44;
    numArray9[12] = (byte) 49;
    numArray9[21] = (byte) 172;
    byte[] numArray10 = new byte[22]
    {
      (byte) 225,
      (byte) 1,
      (byte) 107,
      (byte) 7,
      (byte) 63 /*0x3F*/,
      (byte) 196,
      (byte) 7,
      (byte) 24,
      (byte) 250,
      (byte) 31 /*0x1F*/,
      (byte) 5,
      (byte) 214,
      (byte) 14,
      (byte) 1,
      (byte) 160 /*0xA0*/,
      (byte) 12,
      (byte) 38,
      (byte) 120,
      byte.MaxValue,
      (byte) 118,
      (byte) 124,
      (byte) 169
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 22);
    for (int index = 0; index < 22; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13473()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 64 /*0x40*/,
        (byte) 223,
        (byte) 20,
        (byte) 199,
        (byte) 110,
        (byte) 20,
        (byte) 83,
        (byte) 44,
        (byte) 77,
        (byte) 168,
        (byte) 134,
        (byte) 178,
        (byte) 51,
        (byte) 107,
        (byte) 75,
        (byte) 166
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 44,
        (byte) 178,
        (byte) 21,
        (byte) 153,
        (byte) 42,
        (byte) 197,
        (byte) 82,
        (byte) 251,
        (byte) 54,
        (byte) 11,
        (byte) 3,
        (byte) 247,
        (byte) 110,
        (byte) 45,
        (byte) 174,
        (byte) 72
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_13393.sspq, 968, (Array) numArray4, 0, 33);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13393.sspr, 968, (Array) numArray4, 0, 33);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 46,
      (byte) 82,
      (byte) 28,
      (byte) 78,
      (byte) 137,
      (byte) 225,
      (byte) 192 /*0xC0*/,
      (byte) 20,
      (byte) 94,
      (byte) 132,
      (byte) 230,
      (byte) 237,
      (byte) 122,
      (byte) 227,
      (byte) 114,
      (byte) 96 /*0x60*/
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 238,
      (byte) 183,
      (byte) 138,
      (byte) 160 /*0xA0*/,
      (byte) 157,
      (byte) 148,
      (byte) 71,
      (byte) 24,
      (byte) 231,
      (byte) 196,
      (byte) 242,
      (byte) 220,
      (byte) 222,
      (byte) 140,
      (byte) 37,
      (byte) 73
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13475(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 1;
    sourceArray1[1] = (byte) 193;
    sourceArray1[9] = (byte) 75;
    sourceArray1[32 /*0x20*/] = (byte) 229;
    sourceArray1[4] = (byte) 156;
    sourceArray1[5] = (byte) 96 /*0x60*/;
    sourceArray1[6] = (byte) 218;
    sourceArray1[7] = (byte) 235;
    sourceArray1[8] = (byte) 9;
    sourceArray1[23] = (byte) 127 /*0x7F*/;
    sourceArray1[10] = (byte) 3;
    sourceArray1[11] = (byte) 73;
    sourceArray1[2] = (byte) 189;
    sourceArray1[42] = (byte) 85;
    sourceArray1[40] = (byte) 229;
    sourceArray1[24] = (byte) 129;
    sourceArray1[16 /*0x10*/] = (byte) 214;
    sourceArray1[17] = (byte) 65;
    sourceArray1[3] = (byte) 35;
    sourceArray1[34] = (byte) 91;
    sourceArray1[15] = (byte) 238;
    sourceArray1[21] = (byte) 38;
    sourceArray1[43] = (byte) 56;
    sourceArray1[19] = (byte) 42;
    sourceArray1[44] = (byte) 227;
    sourceArray1[25] = (byte) 115;
    sourceArray1[26] = (byte) 161;
    sourceArray1[27] = (byte) 96 /*0x60*/;
    sourceArray1[38] = (byte) 237;
    sourceArray1[29] = (byte) 197;
    sourceArray1[30] = (byte) 130;
    sourceArray1[31 /*0x1F*/] = (byte) 3;
    sourceArray1[22] = (byte) 147;
    sourceArray1[33] = (byte) 197;
    sourceArray1[28] = (byte) 179;
    sourceArray1[37] = (byte) 43;
    sourceArray1[36] = (byte) 95;
    sourceArray1[41] = (byte) 41;
    sourceArray1[13] = (byte) 159;
    sourceArray1[39] = (byte) 128 /*0x80*/;
    sourceArray1[18] = (byte) 107;
    sourceArray1[20] = (byte) 60;
    sourceArray1[12] = (byte) 120;
    sourceArray1[35] = (byte) 145;
    sourceArray1[45] = (byte) 163;
    sourceArray1[14] = (byte) 109;
    sourceArray1[0] = (byte) 211;
    sourceArray1[47] = (byte) 67;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 218;
    sourceArray2[3] = (byte) 249;
    sourceArray2[15] = (byte) 131;
    sourceArray2[7] = (byte) 241;
    sourceArray2[10] = (byte) 183;
    sourceArray2[16 /*0x10*/] = (byte) 234;
    sourceArray2[8] = (byte) 72;
    sourceArray2[32 /*0x20*/] = (byte) 230;
    sourceArray2[22] = (byte) 103;
    sourceArray2[9] = (byte) 149;
    sourceArray2[17] = (byte) 211;
    sourceArray2[20] = (byte) 72;
    sourceArray2[4] = (byte) 157;
    sourceArray2[19] = (byte) 9;
    sourceArray2[14] = (byte) 162;
    sourceArray2[11] = (byte) 165;
    sourceArray2[25] = (byte) 30;
    sourceArray2[0] = (byte) 217;
    sourceArray2[18] = (byte) 233;
    sourceArray2[12] = (byte) 254;
    sourceArray2[6] = (byte) 247;
    sourceArray2[21] = (byte) 252;
    sourceArray2[1] = (byte) 156;
    sourceArray2[23] = (byte) 98;
    sourceArray2[44] = (byte) 37;
    sourceArray2[5] = (byte) 179;
    sourceArray2[28] = (byte) 110;
    sourceArray2[46] = (byte) 97;
    sourceArray2[40] = (byte) 114;
    sourceArray2[29] = (byte) 219;
    sourceArray2[30] = (byte) 85;
    sourceArray2[31 /*0x1F*/] = (byte) 113;
    sourceArray2[26] = (byte) 104;
    sourceArray2[33] = (byte) 115;
    sourceArray2[34] = (byte) 159;
    sourceArray2[35] = (byte) 32 /*0x20*/;
    sourceArray2[36] = (byte) 53;
    sourceArray2[37] = (byte) 207;
    sourceArray2[38] = (byte) 111;
    sourceArray2[39] = (byte) 176 /*0xB0*/;
    sourceArray2[2] = (byte) 24;
    sourceArray2[41] = (byte) 177;
    sourceArray2[42] = (byte) 31 /*0x1F*/;
    sourceArray2[43] = (byte) 246;
    sourceArray2[13] = (byte) 132;
    sourceArray2[45] = (byte) 47;
    sourceArray2[24] = (byte) 171;
    sourceArray2[47] = (byte) 41;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13476()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[349];
      byte[] numArray2 = new byte[55];
      numArray2[23] = (byte) 232;
      numArray2[1] = (byte) 95;
      numArray2[10] = (byte) 175;
      numArray2[36] = (byte) 138;
      numArray2[4] = (byte) 67;
      numArray2[15] = (byte) 66;
      numArray2[5] = (byte) 143;
      numArray2[7] = (byte) 96 /*0x60*/;
      numArray2[8] = (byte) 206;
      numArray2[9] = (byte) 105;
      numArray2[29] = (byte) 203;
      numArray2[11] = (byte) 119;
      numArray2[12] = (byte) 130;
      numArray2[13] = (byte) 183;
      numArray2[14] = (byte) 222;
      numArray2[18] = (byte) 232;
      numArray2[24] = (byte) 150;
      numArray2[46] = (byte) 78;
      numArray2[22] = (byte) 3;
      numArray2[19] = (byte) 232;
      numArray2[20] = (byte) 37;
      numArray2[0] = (byte) 15;
      numArray2[45] = (byte) 231;
      numArray2[41] = (byte) 62;
      numArray2[26] = (byte) 62;
      numArray2[25] = (byte) 253;
      numArray2[44] = (byte) 133;
      numArray2[27] = (byte) 230;
      numArray2[40] = (byte) 196;
      numArray2[6] = (byte) 244;
      numArray2[38] = (byte) 123;
      numArray2[30] = (byte) 196;
      numArray2[32 /*0x20*/] = (byte) 235;
      numArray2[33] = (byte) 92;
      numArray2[34] = (byte) 126;
      numArray2[28] = (byte) 117;
      numArray2[35] = (byte) 202;
      numArray2[37] = (byte) 35;
      numArray2[39] = (byte) 91;
      numArray2[50] = (byte) 85;
      numArray2[17] = (byte) 29;
      numArray2[53] = (byte) 152;
      numArray2[42] = (byte) 85;
      numArray2[43] = (byte) 245;
      numArray2[3] = (byte) 252;
      numArray2[21] = (byte) 146;
      numArray2[16 /*0x10*/] = (byte) 94;
      numArray2[47] = (byte) 234;
      numArray2[48 /*0x30*/] = (byte) 42;
      numArray2[31 /*0x1F*/] = (byte) 232;
      numArray2[2] = (byte) 78;
      numArray2[51] = (byte) 180;
      numArray2[52] = (byte) 168;
      numArray2[49] = (byte) 39;
      numArray2[54] = (byte) 159;
      byte[] numArray3 = new byte[55]
      {
        (byte) 167,
        (byte) 148,
        (byte) 245,
        (byte) 29,
        (byte) 0,
        (byte) 215,
        (byte) 161,
        (byte) 225,
        (byte) 30,
        (byte) 69,
        (byte) 188,
        (byte) 162,
        (byte) 39,
        (byte) 127 /*0x7F*/,
        (byte) 160 /*0xA0*/,
        (byte) 70,
        (byte) 182,
        (byte) 125,
        (byte) 12,
        (byte) 97,
        (byte) 12,
        (byte) 7,
        (byte) 147,
        (byte) 35,
        (byte) 144 /*0x90*/,
        (byte) 251,
        (byte) 127 /*0x7F*/,
        (byte) 142,
        (byte) 1,
        (byte) 199,
        (byte) 206,
        (byte) 142,
        (byte) 62,
        (byte) 203,
        (byte) 74,
        (byte) 95,
        (byte) 16 /*0x10*/,
        (byte) 39,
        (byte) 144 /*0x90*/,
        (byte) 153,
        (byte) 217,
        (byte) 134,
        (byte) 181,
        (byte) 241,
        (byte) 226,
        (byte) 28,
        (byte) 101,
        (byte) 165,
        (byte) 197,
        (byte) 114,
        (byte) 182,
        (byte) 93,
        (byte) 34,
        (byte) 23,
        (byte) 224 /*0xE0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 1,
        (byte) 182,
        (byte) 151,
        (byte) 253,
        (byte) 80 /*0x50*/,
        (byte) 155,
        (byte) 139,
        (byte) 149,
        (byte) 134,
        (byte) 57,
        (byte) 104,
        (byte) 54,
        (byte) 164,
        (byte) 147,
        (byte) 125,
        (byte) 107,
        (byte) 130,
        (byte) 109,
        (byte) 146,
        (byte) 234,
        (byte) 190,
        (byte) 165,
        (byte) 84,
        (byte) 133,
        (byte) 161,
        (byte) 165,
        (byte) 223,
        (byte) 92,
        (byte) 252,
        (byte) 36,
        (byte) 222,
        (byte) 4,
        (byte) 194,
        (byte) 33,
        (byte) 34,
        (byte) 7,
        (byte) 158,
        (byte) 11,
        (byte) 101,
        (byte) 184,
        (byte) 199,
        (byte) 186,
        (byte) 55,
        (byte) 34,
        (byte) 192 /*0xC0*/,
        (byte) 123,
        (byte) 114,
        (byte) 219,
        (byte) 156,
        (byte) 167,
        (byte) 152,
        (byte) 6,
        (byte) 37,
        (byte) 85,
        (byte) 143
      };
      byte[] numArray5 = new byte[55];
      numArray5[15] = (byte) 205;
      numArray5[42] = (byte) 105;
      numArray5[26] = (byte) 67;
      numArray5[39] = (byte) 3;
      numArray5[4] = (byte) 80 /*0x50*/;
      numArray5[50] = (byte) 251;
      numArray5[6] = (byte) 208 /*0xD0*/;
      numArray5[7] = (byte) 178;
      numArray5[8] = (byte) 203;
      numArray5[40] = (byte) 116;
      numArray5[10] = (byte) 12;
      numArray5[1] = (byte) 201;
      numArray5[12] = (byte) 140;
      numArray5[27] = (byte) 61;
      numArray5[25] = (byte) 229;
      numArray5[45] = (byte) 75;
      numArray5[17] = (byte) 19;
      numArray5[35] = (byte) 94;
      numArray5[51] = (byte) 138;
      numArray5[11] = (byte) 148;
      numArray5[20] = (byte) 111;
      numArray5[36] = (byte) 176 /*0xB0*/;
      numArray5[22] = (byte) 98;
      numArray5[23] = (byte) 171;
      numArray5[18] = (byte) 119;
      numArray5[0] = (byte) 115;
      numArray5[38] = (byte) 52;
      numArray5[3] = (byte) 140;
      numArray5[28] = (byte) 48 /*0x30*/;
      numArray5[41] = (byte) 94;
      numArray5[30] = (byte) 221;
      numArray5[53] = (byte) 225;
      numArray5[32 /*0x20*/] = (byte) 181;
      numArray5[33] = (byte) 14;
      numArray5[21] = (byte) 57;
      numArray5[19] = (byte) 146;
      numArray5[46] = (byte) 102;
      numArray5[37] = (byte) 163;
      numArray5[31 /*0x1F*/] = (byte) 86;
      numArray5[44] = (byte) 33;
      numArray5[47] = (byte) 205;
      numArray5[24] = (byte) 207;
      numArray5[13] = (byte) 10;
      numArray5[43] = (byte) 120;
      numArray5[29] = (byte) 105;
      numArray5[5] = (byte) 96 /*0x60*/;
      numArray5[34] = (byte) 177;
      numArray5[16 /*0x10*/] = (byte) 186;
      numArray5[48 /*0x30*/] = (byte) 174;
      numArray5[49] = (byte) 11;
      numArray5[14] = (byte) 194;
      numArray5[2] = (byte) 18;
      numArray5[9] = (byte) 72;
      numArray5[52] = (byte) 64 /*0x40*/;
      numArray5[54] = (byte) 252;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 78,
        (byte) 53,
        (byte) 119,
        (byte) 184,
        (byte) 110,
        (byte) 68,
        (byte) 215,
        (byte) 130,
        (byte) 133,
        (byte) 6,
        (byte) 240 /*0xF0*/,
        (byte) 170,
        (byte) 208 /*0xD0*/,
        (byte) 65,
        (byte) 199,
        (byte) 76,
        (byte) 221,
        (byte) 138,
        (byte) 225,
        (byte) 152,
        (byte) 107,
        (byte) 103,
        (byte) 30,
        (byte) 68,
        (byte) 152,
        (byte) 154,
        (byte) 122,
        (byte) 105,
        (byte) 217,
        (byte) 147,
        (byte) 58,
        (byte) 6,
        (byte) 215,
        (byte) 117,
        (byte) 61,
        (byte) 121,
        (byte) 67,
        (byte) 68,
        (byte) 41,
        (byte) 221,
        (byte) 254,
        (byte) 68,
        (byte) 215,
        (byte) 0,
        (byte) 254,
        (byte) 192 /*0xC0*/,
        (byte) 85,
        (byte) 152,
        (byte) 90,
        (byte) 177,
        (byte) 109,
        (byte) 167,
        (byte) 170,
        (byte) 86,
        (byte) 196
      };
      byte[] numArray7 = new byte[55];
      numArray7[45] = (byte) 224 /*0xE0*/;
      numArray7[30] = (byte) 217;
      numArray7[49] = (byte) 33;
      numArray7[29] = (byte) 133;
      numArray7[1] = (byte) 38;
      numArray7[5] = (byte) 240 /*0xF0*/;
      numArray7[11] = (byte) 14;
      numArray7[42] = (byte) 2;
      numArray7[8] = (byte) 137;
      numArray7[9] = (byte) 166;
      numArray7[10] = (byte) 134;
      numArray7[27] = (byte) 79;
      numArray7[12] = (byte) 44;
      numArray7[13] = (byte) 155;
      numArray7[14] = (byte) 50;
      numArray7[40] = (byte) 207;
      numArray7[16 /*0x10*/] = (byte) 172;
      numArray7[44] = (byte) 230;
      numArray7[18] = (byte) 117;
      numArray7[19] = (byte) 159;
      numArray7[20] = (byte) 209;
      numArray7[38] = (byte) 192 /*0xC0*/;
      numArray7[22] = (byte) 10;
      numArray7[48 /*0x30*/] = (byte) 32 /*0x20*/;
      numArray7[4] = (byte) 214;
      numArray7[54] = (byte) 131;
      numArray7[53] = (byte) 124;
      numArray7[46] = (byte) 157;
      numArray7[28] = (byte) 8;
      numArray7[7] = (byte) 174;
      numArray7[2] = (byte) 55;
      numArray7[3] = (byte) 64 /*0x40*/;
      numArray7[32 /*0x20*/] = (byte) 41;
      numArray7[6] = (byte) 63 /*0x3F*/;
      numArray7[26] = (byte) 157;
      numArray7[25] = (byte) 167;
      numArray7[36] = (byte) 15;
      numArray7[23] = (byte) 202;
      numArray7[0] = (byte) 214;
      numArray7[39] = (byte) 218;
      numArray7[37] = (byte) 96 /*0x60*/;
      numArray7[41] = (byte) 207;
      numArray7[24] = (byte) 203;
      numArray7[43] = (byte) 2;
      numArray7[50] = (byte) 224 /*0xE0*/;
      numArray7[31 /*0x1F*/] = (byte) 253;
      numArray7[35] = (byte) 52;
      numArray7[47] = (byte) 171;
      numArray7[34] = (byte) 236;
      numArray7[33] = (byte) 177;
      numArray7[17] = (byte) 10;
      numArray7[51] = (byte) 69;
      numArray7[52] = (byte) 41;
      numArray7[15] = (byte) 112 /*0x70*/;
      numArray7[21] = (byte) 202;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 236,
        (byte) 71,
        (byte) 142,
        (byte) 233,
        (byte) 95,
        (byte) 129,
        (byte) 203,
        (byte) 57,
        (byte) 141,
        (byte) 82,
        (byte) 169,
        (byte) 172,
        (byte) 234,
        (byte) 247,
        (byte) 117,
        (byte) 12,
        (byte) 241,
        (byte) 9,
        (byte) 138,
        (byte) 170,
        (byte) 233,
        (byte) 50,
        (byte) 236,
        (byte) 75,
        (byte) 75,
        (byte) 0,
        (byte) 126,
        (byte) 82,
        (byte) 76,
        (byte) 182,
        (byte) 196,
        (byte) 19,
        (byte) 0,
        (byte) 106,
        (byte) 36,
        (byte) 221,
        (byte) 198,
        (byte) 41,
        (byte) 49,
        (byte) 26,
        (byte) 26,
        (byte) 251,
        (byte) 134,
        (byte) 153,
        (byte) 80 /*0x50*/,
        (byte) 232,
        (byte) 35,
        (byte) 177,
        (byte) 94,
        (byte) 58,
        (byte) 181,
        (byte) 178,
        (byte) 19,
        (byte) 172,
        (byte) 15
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 84,
        (byte) 199,
        (byte) 209,
        (byte) 86,
        (byte) 217,
        (byte) 133,
        (byte) 29,
        (byte) 248,
        (byte) 66,
        (byte) 59,
        (byte) 102,
        (byte) 29,
        (byte) 87,
        (byte) 173,
        (byte) 253,
        (byte) 115,
        (byte) 174,
        (byte) 156,
        (byte) 232,
        (byte) 243,
        (byte) 36,
        (byte) 135,
        (byte) 170,
        (byte) 197,
        (byte) 27,
        (byte) 120,
        (byte) 97,
        (byte) 232,
        (byte) 75,
        (byte) 178,
        (byte) 182,
        (byte) 170,
        (byte) 248,
        (byte) 43,
        (byte) 189,
        (byte) 114,
        (byte) 168,
        (byte) 171,
        (byte) 212,
        (byte) 182,
        (byte) 64 /*0x40*/,
        (byte) 187,
        (byte) 90,
        (byte) 191,
        (byte) 84,
        (byte) 112 /*0x70*/,
        (byte) 134,
        (byte) 107,
        (byte) 162,
        (byte) 8,
        (byte) 98,
        (byte) 104,
        (byte) 155,
        (byte) 74,
        (byte) 225
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 67,
        (byte) 157,
        (byte) 94,
        (byte) 3,
        (byte) 121,
        (byte) 237,
        (byte) 215,
        (byte) 142,
        (byte) 150,
        (byte) 172,
        (byte) 128 /*0x80*/,
        (byte) 210,
        (byte) 151,
        (byte) 69,
        (byte) 199,
        (byte) 216,
        (byte) 112 /*0x70*/,
        (byte) 143,
        (byte) 238,
        (byte) 172,
        (byte) 82,
        (byte) 250,
        (byte) 99,
        (byte) 221,
        (byte) 205,
        (byte) 243,
        (byte) 17,
        (byte) 1,
        (byte) 17,
        byte.MaxValue,
        (byte) 84,
        (byte) 90,
        (byte) 107,
        (byte) 130,
        (byte) 131,
        (byte) 140,
        (byte) 210,
        (byte) 23,
        (byte) 160 /*0xA0*/,
        (byte) 14,
        (byte) 174,
        (byte) 10,
        (byte) 186,
        (byte) 60,
        (byte) 162,
        (byte) 222,
        (byte) 198,
        (byte) 244,
        (byte) 237,
        (byte) 218,
        (byte) 162,
        (byte) 28,
        (byte) 33,
        (byte) 12,
        (byte) 8
      };
      byte[] numArray11 = new byte[55];
      numArray11[34] = (byte) 4;
      numArray11[1] = (byte) 119;
      numArray11[14] = (byte) 7;
      numArray11[22] = (byte) 100;
      numArray11[40] = (byte) 109;
      numArray11[5] = (byte) 159;
      numArray11[2] = (byte) 88;
      numArray11[21] = (byte) 54;
      numArray11[8] = (byte) 111;
      numArray11[9] = (byte) 142;
      numArray11[41] = (byte) 92;
      numArray11[37] = (byte) 41;
      numArray11[54] = (byte) 50;
      numArray11[30] = (byte) 57;
      numArray11[31 /*0x1F*/] = (byte) 123;
      numArray11[15] = (byte) 223;
      numArray11[32 /*0x20*/] = (byte) 149;
      numArray11[51] = (byte) 181;
      numArray11[53] = (byte) 63 /*0x3F*/;
      numArray11[19] = (byte) 51;
      numArray11[20] = (byte) 24;
      numArray11[28] = (byte) 147;
      numArray11[39] = (byte) 141;
      numArray11[23] = (byte) 164;
      numArray11[4] = (byte) 244;
      numArray11[11] = (byte) 167;
      numArray11[26] = (byte) 14;
      numArray11[27] = (byte) 193;
      numArray11[36] = (byte) 129;
      numArray11[25] = (byte) 47;
      numArray11[3] = (byte) 11;
      numArray11[24] = (byte) 157;
      numArray11[0] = (byte) 126;
      numArray11[7] = (byte) 151;
      numArray11[18] = (byte) 102;
      numArray11[49] = (byte) 85;
      numArray11[6] = (byte) 234;
      numArray11[16 /*0x10*/] = (byte) 149;
      numArray11[38] = (byte) 223;
      numArray11[35] = (byte) 207;
      numArray11[47] = (byte) 23;
      numArray11[10] = (byte) 9;
      numArray11[42] = (byte) 87;
      numArray11[43] = (byte) 22;
      numArray11[44] = (byte) 196;
      numArray11[33] = (byte) 194;
      numArray11[46] = (byte) 155;
      numArray11[45] = (byte) 179;
      numArray11[48 /*0x30*/] = (byte) 9;
      numArray11[29] = (byte) 195;
      numArray11[50] = (byte) 106;
      numArray11[12] = (byte) 235;
      numArray11[52] = (byte) 212;
      numArray11[13] = (byte) 114;
      numArray11[17] = (byte) 142;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[55]
      {
        (byte) 69,
        (byte) 70,
        (byte) 88,
        (byte) 190,
        (byte) 194,
        (byte) 70,
        (byte) 56,
        (byte) 32 /*0x20*/,
        (byte) 236,
        (byte) 8,
        (byte) 155,
        (byte) 134,
        (byte) 232,
        (byte) 179,
        (byte) 120,
        (byte) 211,
        (byte) 172,
        (byte) 36,
        (byte) 186,
        (byte) 27,
        (byte) 66,
        (byte) 190,
        (byte) 120,
        (byte) 124,
        (byte) 233,
        (byte) 97,
        (byte) 98,
        (byte) 20,
        (byte) 10,
        (byte) 247,
        (byte) 80 /*0x50*/,
        (byte) 185,
        (byte) 50,
        (byte) 63 /*0x3F*/,
        (byte) 12,
        (byte) 154,
        (byte) 195,
        (byte) 116,
        (byte) 22,
        (byte) 70,
        (byte) 235,
        (byte) 195,
        (byte) 144 /*0x90*/,
        (byte) 110,
        (byte) 20,
        (byte) 172,
        (byte) 201,
        (byte) 33,
        (byte) 209,
        (byte) 89,
        (byte) 148,
        (byte) 234,
        (byte) 6,
        (byte) 237,
        (byte) 207
      };
      byte[] numArray13 = new byte[55]
      {
        (byte) 215,
        byte.MaxValue,
        (byte) 96 /*0x60*/,
        (byte) 93,
        (byte) 95,
        (byte) 162,
        (byte) 67,
        (byte) 186,
        (byte) 71,
        (byte) 252,
        (byte) 174,
        (byte) 244,
        (byte) 102,
        (byte) 149,
        (byte) 243,
        (byte) 232,
        (byte) 48 /*0x30*/,
        (byte) 60,
        (byte) 17,
        (byte) 212,
        (byte) 74,
        (byte) 130,
        (byte) 182,
        (byte) 12,
        (byte) 253,
        (byte) 112 /*0x70*/,
        (byte) 182,
        (byte) 51,
        (byte) 172,
        (byte) 45,
        (byte) 66,
        (byte) 91,
        (byte) 212,
        (byte) 193,
        (byte) 245,
        (byte) 87,
        (byte) 24,
        (byte) 144 /*0x90*/,
        (byte) 110,
        (byte) 56,
        (byte) 22,
        (byte) 208 /*0xD0*/,
        (byte) 243,
        (byte) 54,
        (byte) 11,
        (byte) 42,
        (byte) 61,
        (byte) 164,
        (byte) 58,
        (byte) 219,
        (byte) 62,
        (byte) 15,
        (byte) 198,
        (byte) 109,
        (byte) 61
      };
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 275] ^= numArray13[index];
      byte[] numArray14 = new byte[19]
      {
        (byte) 219,
        (byte) 212,
        (byte) 97,
        (byte) 228,
        (byte) 36,
        (byte) 22,
        (byte) 244,
        (byte) 184,
        (byte) 226,
        (byte) 84,
        (byte) 168,
        (byte) 166,
        (byte) 221,
        (byte) 16 /*0x10*/,
        (byte) 56,
        (byte) 14,
        (byte) 115,
        (byte) 47,
        (byte) 127 /*0x7F*/
      };
      byte[] numArray15 = new byte[19]
      {
        (byte) 225,
        (byte) 224 /*0xE0*/,
        byte.MaxValue,
        (byte) 229,
        (byte) 50,
        (byte) 59,
        (byte) 111,
        (byte) 77,
        (byte) 9,
        (byte) 48 /*0x30*/,
        (byte) 43,
        (byte) 100,
        (byte) 142,
        (byte) 44,
        (byte) 136,
        (byte) 254,
        (byte) 39,
        (byte) 145,
        (byte) 197
      };
      key.Query(true, 335, numArray14, numArray14);
      Array.Copy((Array) numArray14, 0, (Array) numArray1, 330, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 330] ^= numArray15[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray16 = new byte[349];
    byte[] numArray17 = new byte[55]
    {
      (byte) 61,
      (byte) 151,
      (byte) 65,
      (byte) 66,
      (byte) 176 /*0xB0*/,
      (byte) 99,
      (byte) 101,
      (byte) 179,
      (byte) 68,
      (byte) 112 /*0x70*/,
      (byte) 133,
      (byte) 204,
      (byte) 231,
      (byte) 160 /*0xA0*/,
      (byte) 181,
      (byte) 98,
      (byte) 198,
      (byte) 173,
      (byte) 155,
      (byte) 182,
      (byte) 50,
      (byte) 88,
      (byte) 33,
      (byte) 122,
      (byte) 243,
      (byte) 73,
      (byte) 92,
      (byte) 220,
      (byte) 146,
      (byte) 189,
      (byte) 80 /*0x50*/,
      (byte) 90,
      (byte) 197,
      (byte) 115,
      (byte) 150,
      (byte) 202,
      (byte) 163,
      (byte) 225,
      (byte) 102,
      (byte) 145,
      (byte) 82,
      (byte) 47,
      (byte) 15,
      (byte) 56,
      (byte) 33,
      (byte) 17,
      (byte) 40,
      (byte) 151,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 104,
      (byte) 209,
      (byte) 145,
      (byte) 83,
      (byte) 53
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 86,
      (byte) 79,
      (byte) 178,
      (byte) 115,
      (byte) 221,
      (byte) 156,
      (byte) 39,
      (byte) 45,
      (byte) 213,
      (byte) 165,
      (byte) 203,
      (byte) 234,
      (byte) 176 /*0xB0*/,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 179,
      (byte) 124,
      (byte) 249,
      (byte) 106,
      (byte) 188,
      (byte) 119,
      (byte) 118,
      (byte) 56,
      (byte) 42,
      (byte) 38,
      (byte) 102,
      (byte) 2,
      (byte) 170,
      (byte) 198,
      (byte) 239,
      (byte) 62,
      (byte) 138,
      (byte) 10,
      (byte) 190,
      (byte) 89,
      (byte) 35,
      (byte) 13,
      (byte) 84,
      (byte) 213,
      (byte) 45,
      (byte) 27,
      (byte) 141,
      (byte) 249,
      (byte) 78,
      (byte) 190,
      (byte) 235,
      (byte) 217,
      (byte) 220,
      (byte) 89,
      (byte) 65,
      (byte) 174,
      (byte) 117,
      (byte) 87,
      (byte) 198,
      (byte) 5
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray16, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[13] = (byte) 191;
    numArray19[30] = (byte) 113;
    numArray19[40] = (byte) 136;
    numArray19[3] = (byte) 6;
    numArray19[36] = (byte) 254;
    numArray19[14] = (byte) 228;
    numArray19[11] = (byte) 9;
    numArray19[37] = (byte) 139;
    numArray19[8] = (byte) 43;
    numArray19[16 /*0x10*/] = (byte) 10;
    numArray19[41] = (byte) 25;
    numArray19[38] = (byte) 200;
    numArray19[12] = (byte) 235;
    numArray19[45] = (byte) 141;
    numArray19[51] = (byte) 238;
    numArray19[15] = (byte) 71;
    numArray19[35] = (byte) 146;
    numArray19[17] = (byte) 100;
    numArray19[18] = (byte) 2;
    numArray19[19] = (byte) 72;
    numArray19[20] = (byte) 232;
    numArray19[21] = (byte) 88;
    numArray19[22] = (byte) 226;
    numArray19[39] = (byte) 15;
    numArray19[10] = (byte) 116;
    numArray19[25] = (byte) 131;
    numArray19[44] = (byte) 180;
    numArray19[27] = (byte) 85;
    numArray19[6] = (byte) 135;
    numArray19[1] = (byte) 79;
    numArray19[46] = (byte) 109;
    numArray19[31 /*0x1F*/] = (byte) 62;
    numArray19[32 /*0x20*/] = (byte) 177;
    numArray19[33] = (byte) 83;
    numArray19[28] = (byte) 126;
    numArray19[0] = (byte) 198;
    numArray19[24] = (byte) 197;
    numArray19[34] = (byte) 220;
    numArray19[53] = (byte) 156;
    numArray19[54] = (byte) 141;
    numArray19[7] = (byte) 31 /*0x1F*/;
    numArray19[29] = (byte) 200;
    numArray19[42] = (byte) 246;
    numArray19[26] = (byte) 129;
    numArray19[47] = (byte) 81;
    numArray19[52] = (byte) 50;
    numArray19[4] = (byte) 137;
    numArray19[2] = (byte) 103;
    numArray19[48 /*0x30*/] = (byte) 131;
    numArray19[49] = (byte) 138;
    numArray19[50] = (byte) 135;
    numArray19[43] = (byte) 10;
    numArray19[5] = (byte) 32 /*0x20*/;
    numArray19[9] = (byte) 7;
    numArray19[23] = (byte) 32 /*0x20*/;
    byte[] numArray20 = new byte[55]
    {
      (byte) 248,
      (byte) 163,
      (byte) 191,
      (byte) 148,
      (byte) 182,
      (byte) 185,
      (byte) 125,
      (byte) 22,
      (byte) 99,
      (byte) 112 /*0x70*/,
      (byte) 22,
      (byte) 20,
      (byte) 144 /*0x90*/,
      (byte) 11,
      (byte) 236,
      (byte) 66,
      (byte) 196,
      (byte) 125,
      (byte) 147,
      (byte) 30,
      (byte) 182,
      (byte) 251,
      (byte) 33,
      (byte) 160 /*0xA0*/,
      (byte) 28,
      (byte) 171,
      (byte) 223,
      (byte) 60,
      (byte) 170,
      (byte) 87,
      (byte) 37,
      (byte) 172,
      (byte) 32 /*0x20*/,
      (byte) 158,
      (byte) 81,
      (byte) 78,
      (byte) 72,
      (byte) 108,
      (byte) 40,
      (byte) 102,
      (byte) 123,
      (byte) 98,
      (byte) 26,
      (byte) 222,
      (byte) 0,
      (byte) 39,
      (byte) 206,
      (byte) 144 /*0x90*/,
      (byte) 65,
      (byte) 218,
      (byte) 26,
      (byte) 56,
      (byte) 124,
      (byte) 63 /*0x3F*/,
      (byte) 236
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray16, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index + 55] ^= numArray20[index];
    byte[] numArray21 = new byte[55];
    numArray21[1] = (byte) 9;
    numArray21[46] = (byte) 6;
    numArray21[25] = (byte) 159;
    numArray21[23] = (byte) 63 /*0x3F*/;
    numArray21[47] = (byte) 177;
    numArray21[18] = (byte) 86;
    numArray21[6] = (byte) 33;
    numArray21[28] = (byte) 47;
    numArray21[36] = (byte) 208 /*0xD0*/;
    numArray21[9] = (byte) 65;
    numArray21[10] = (byte) 209;
    numArray21[44] = (byte) 4;
    numArray21[7] = (byte) 18;
    numArray21[45] = (byte) 249;
    numArray21[14] = (byte) 52;
    numArray21[13] = (byte) 144 /*0x90*/;
    numArray21[38] = (byte) 245;
    numArray21[17] = (byte) 106;
    numArray21[12] = (byte) 43;
    numArray21[19] = (byte) 206;
    numArray21[11] = (byte) 116;
    numArray21[21] = (byte) 237;
    numArray21[22] = (byte) 1;
    numArray21[4] = (byte) 97;
    numArray21[24] = (byte) 194;
    numArray21[50] = (byte) 188;
    numArray21[2] = (byte) 240 /*0xF0*/;
    numArray21[49] = (byte) 121;
    numArray21[41] = (byte) 98;
    numArray21[8] = (byte) 42;
    numArray21[30] = (byte) 58;
    numArray21[31 /*0x1F*/] = (byte) 7;
    numArray21[32 /*0x20*/] = (byte) 99;
    numArray21[33] = (byte) 146;
    numArray21[34] = (byte) 221;
    numArray21[35] = (byte) 64 /*0x40*/;
    numArray21[15] = (byte) 218;
    numArray21[37] = (byte) 2;
    numArray21[3] = (byte) 109;
    numArray21[39] = (byte) 203;
    numArray21[40] = (byte) 114;
    numArray21[5] = (byte) 172;
    numArray21[42] = (byte) 83;
    numArray21[43] = (byte) 244;
    numArray21[51] = (byte) 112 /*0x70*/;
    numArray21[27] = (byte) 56;
    numArray21[48 /*0x30*/] = (byte) 211;
    numArray21[0] = (byte) 218;
    numArray21[16 /*0x10*/] = (byte) 131;
    numArray21[29] = (byte) 203;
    numArray21[20] = (byte) 99;
    numArray21[26] = (byte) 178;
    numArray21[52] = (byte) 103;
    numArray21[53] = (byte) 61;
    numArray21[54] = (byte) 48 /*0x30*/;
    byte[] numArray22 = new byte[55]
    {
      (byte) 127 /*0x7F*/,
      (byte) 108,
      (byte) 63 /*0x3F*/,
      (byte) 38,
      (byte) 207,
      (byte) 8,
      (byte) 103,
      (byte) 70,
      (byte) 168,
      (byte) 165,
      (byte) 253,
      (byte) 105,
      (byte) 232,
      (byte) 236,
      (byte) 251,
      (byte) 205,
      (byte) 120,
      (byte) 35,
      (byte) 199,
      (byte) 205,
      (byte) 159,
      (byte) 216,
      (byte) 164,
      (byte) 114,
      (byte) 121,
      (byte) 175,
      (byte) 2,
      (byte) 101,
      (byte) 188,
      (byte) 149,
      (byte) 92,
      (byte) 46,
      (byte) 161,
      (byte) 167,
      (byte) 106,
      (byte) 210,
      (byte) 177,
      (byte) 93,
      (byte) 198,
      (byte) 184,
      (byte) 24,
      (byte) 35,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 188,
      (byte) 153,
      (byte) 211,
      (byte) 32 /*0x20*/,
      (byte) 240 /*0xF0*/,
      (byte) 200,
      (byte) 87,
      (byte) 101,
      (byte) 199,
      (byte) 231,
      (byte) 52
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray16, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index + 110] ^= numArray22[index];
    byte[] numArray23 = new byte[55];
    numArray23[7] = (byte) 60;
    numArray23[1] = (byte) 225;
    numArray23[2] = (byte) 40;
    numArray23[3] = (byte) 248;
    numArray23[4] = (byte) 132;
    numArray23[39] = (byte) 85;
    numArray23[6] = (byte) 79;
    numArray23[51] = (byte) 15;
    numArray23[10] = (byte) 56;
    numArray23[52] = (byte) 175;
    numArray23[31 /*0x1F*/] = (byte) 50;
    numArray23[29] = (byte) 195;
    numArray23[17] = (byte) 210;
    numArray23[13] = (byte) 233;
    numArray23[36] = (byte) 10;
    numArray23[25] = (byte) 133;
    numArray23[16 /*0x10*/] = (byte) 185;
    numArray23[24] = (byte) 2;
    numArray23[33] = (byte) 60;
    numArray23[15] = (byte) 66;
    numArray23[20] = (byte) 31 /*0x1F*/;
    numArray23[49] = (byte) 184;
    numArray23[22] = (byte) 225;
    numArray23[19] = (byte) 164;
    numArray23[35] = (byte) 117;
    numArray23[23] = (byte) 44;
    numArray23[14] = (byte) 41;
    numArray23[34] = (byte) 169;
    numArray23[28] = (byte) 207;
    numArray23[5] = (byte) 43;
    numArray23[30] = (byte) 67;
    numArray23[11] = (byte) 195;
    numArray23[18] = (byte) 128 /*0x80*/;
    numArray23[43] = (byte) 252;
    numArray23[8] = (byte) 41;
    numArray23[47] = (byte) 134;
    numArray23[26] = (byte) 71;
    numArray23[37] = (byte) 217;
    numArray23[38] = (byte) 4;
    numArray23[0] = (byte) 118;
    numArray23[40] = (byte) 165;
    numArray23[41] = (byte) 176 /*0xB0*/;
    numArray23[42] = (byte) 17;
    numArray23[32 /*0x20*/] = (byte) 155;
    numArray23[44] = (byte) 51;
    numArray23[45] = (byte) 58;
    numArray23[46] = (byte) 34;
    numArray23[21] = byte.MaxValue;
    numArray23[12] = (byte) 188;
    numArray23[27] = (byte) 211;
    numArray23[50] = (byte) 35;
    numArray23[48 /*0x30*/] = (byte) 18;
    numArray23[9] = (byte) 235;
    numArray23[53] = (byte) 175;
    numArray23[54] = (byte) 219;
    byte[] numArray24 = new byte[55];
    numArray24[13] = (byte) 203;
    numArray24[1] = (byte) 160 /*0xA0*/;
    numArray24[42] = (byte) 138;
    numArray24[40] = (byte) 120;
    numArray24[54] = (byte) 65;
    numArray24[5] = (byte) 52;
    numArray24[20] = (byte) 173;
    numArray24[51] = (byte) 152;
    numArray24[7] = (byte) 175;
    numArray24[39] = (byte) 112 /*0x70*/;
    numArray24[10] = (byte) 52;
    numArray24[50] = (byte) 205;
    numArray24[9] = (byte) 152;
    numArray24[21] = (byte) 18;
    numArray24[0] = (byte) 67;
    numArray24[4] = (byte) 59;
    numArray24[16 /*0x10*/] = (byte) 110;
    numArray24[3] = (byte) 43;
    numArray24[18] = (byte) 23;
    numArray24[11] = (byte) 41;
    numArray24[17] = (byte) 174;
    numArray24[48 /*0x30*/] = (byte) 167;
    numArray24[25] = (byte) 62;
    numArray24[15] = (byte) 7;
    numArray24[14] = (byte) 134;
    numArray24[6] = (byte) 191;
    numArray24[12] = (byte) 127 /*0x7F*/;
    numArray24[53] = (byte) 83;
    numArray24[28] = (byte) 130;
    numArray24[29] = (byte) 0;
    numArray24[30] = (byte) 167;
    numArray24[8] = (byte) 115;
    numArray24[32 /*0x20*/] = (byte) 243;
    numArray24[46] = (byte) 43;
    numArray24[34] = (byte) 16 /*0x10*/;
    numArray24[35] = (byte) 87;
    numArray24[2] = (byte) 138;
    numArray24[37] = (byte) 228;
    numArray24[38] = (byte) 82;
    numArray24[31 /*0x1F*/] = (byte) 209;
    numArray24[19] = (byte) 219;
    numArray24[41] = (byte) 170;
    numArray24[24] = (byte) 69;
    numArray24[43] = (byte) 158;
    numArray24[44] = (byte) 69;
    numArray24[26] = (byte) 101;
    numArray24[36] = (byte) 201;
    numArray24[47] = (byte) 9;
    numArray24[22] = (byte) 103;
    numArray24[49] = (byte) 39;
    numArray24[23] = (byte) 21;
    numArray24[33] = (byte) 66;
    numArray24[52] = (byte) 131;
    numArray24[45] = (byte) 238;
    numArray24[27] = (byte) 193;
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray16, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index + 165] ^= numArray24[index];
    byte[] numArray25 = new byte[55]
    {
      (byte) 56,
      (byte) 120,
      (byte) 220,
      (byte) 170,
      (byte) 90,
      (byte) 193,
      (byte) 107,
      (byte) 132,
      (byte) 132,
      (byte) 2,
      (byte) 42,
      (byte) 98,
      (byte) 226,
      (byte) 31 /*0x1F*/,
      (byte) 70,
      (byte) 95,
      (byte) 109,
      (byte) 105,
      (byte) 66,
      (byte) 96 /*0x60*/,
      (byte) 179,
      (byte) 32 /*0x20*/,
      (byte) 224 /*0xE0*/,
      (byte) 61,
      (byte) 62,
      (byte) 63 /*0x3F*/,
      (byte) 63 /*0x3F*/,
      (byte) 19,
      (byte) 118,
      (byte) 53,
      (byte) 96 /*0x60*/,
      (byte) 181,
      (byte) 101,
      (byte) 209,
      (byte) 210,
      (byte) 61,
      (byte) 139,
      (byte) 162,
      (byte) 19,
      (byte) 41,
      (byte) 119,
      (byte) 89,
      (byte) 38,
      (byte) 210,
      (byte) 88,
      (byte) 215,
      (byte) 63 /*0x3F*/,
      (byte) 55,
      (byte) 1,
      (byte) 26,
      (byte) 91,
      (byte) 113,
      (byte) 252,
      (byte) 228,
      (byte) 213
    };
    byte[] numArray26 = new byte[55]
    {
      (byte) 66,
      (byte) 20,
      (byte) 13,
      (byte) 196,
      (byte) 2,
      (byte) 116,
      (byte) 183,
      (byte) 251,
      (byte) 190,
      (byte) 100,
      (byte) 155,
      (byte) 82,
      (byte) 89,
      (byte) 56,
      (byte) 221,
      (byte) 4,
      (byte) 211,
      (byte) 6,
      (byte) 135,
      (byte) 93,
      (byte) 38,
      (byte) 120,
      (byte) 105,
      (byte) 176 /*0xB0*/,
      (byte) 88,
      (byte) 148,
      (byte) 230,
      (byte) 104,
      (byte) 93,
      (byte) 145,
      (byte) 225,
      (byte) 98,
      (byte) 212,
      (byte) 193,
      (byte) 182,
      (byte) 93,
      (byte) 230,
      (byte) 233,
      (byte) 37,
      (byte) 5,
      (byte) 199,
      (byte) 138,
      (byte) 42,
      (byte) 135,
      (byte) 156,
      (byte) 108,
      (byte) 147,
      (byte) 85,
      byte.MaxValue,
      (byte) 184,
      (byte) 49,
      (byte) 65,
      (byte) 82,
      (byte) 165,
      (byte) 67
    };
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray16, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index + 220] ^= numArray26[index];
    byte[] numArray27 = new byte[55];
    numArray27[40] = (byte) 215;
    numArray27[1] = (byte) 107;
    numArray27[4] = (byte) 88;
    numArray27[3] = (byte) 130;
    numArray27[42] = (byte) 227;
    numArray27[14] = (byte) 203;
    numArray27[52] = (byte) 122;
    numArray27[41] = (byte) 48 /*0x30*/;
    numArray27[5] = (byte) 14;
    numArray27[9] = (byte) 253;
    numArray27[10] = (byte) 16 /*0x10*/;
    numArray27[11] = (byte) 76;
    numArray27[12] = (byte) 9;
    numArray27[53] = (byte) 28;
    numArray27[33] = (byte) 164;
    numArray27[54] = (byte) 138;
    numArray27[32 /*0x20*/] = (byte) 187;
    numArray27[23] = byte.MaxValue;
    numArray27[51] = (byte) 250;
    numArray27[26] = (byte) 92;
    numArray27[0] = (byte) 159;
    numArray27[22] = (byte) 103;
    numArray27[15] = (byte) 86;
    numArray27[2] = (byte) 27;
    numArray27[43] = (byte) 218;
    numArray27[47] = (byte) 70;
    numArray27[28] = (byte) 239;
    numArray27[21] = (byte) 228;
    numArray27[6] = (byte) 78;
    numArray27[29] = (byte) 184;
    numArray27[30] = (byte) 128 /*0x80*/;
    numArray27[31 /*0x1F*/] = (byte) 252;
    numArray27[7] = (byte) 162;
    numArray27[27] = (byte) 217;
    numArray27[34] = (byte) 236;
    numArray27[16 /*0x10*/] = (byte) 24;
    numArray27[36] = (byte) 247;
    numArray27[37] = (byte) 167;
    numArray27[38] = (byte) 30;
    numArray27[25] = (byte) 18;
    numArray27[17] = (byte) 233;
    numArray27[13] = (byte) 212;
    numArray27[49] = (byte) 27;
    numArray27[20] = (byte) 13;
    numArray27[44] = (byte) 46;
    numArray27[39] = (byte) 75;
    numArray27[45] = (byte) 234;
    numArray27[35] = (byte) 54;
    numArray27[48 /*0x30*/] = (byte) 144 /*0x90*/;
    numArray27[24] = (byte) 8;
    numArray27[50] = (byte) 228;
    numArray27[46] = (byte) 248;
    numArray27[18] = (byte) 90;
    numArray27[8] = (byte) 116;
    numArray27[19] = (byte) 250;
    byte[] numArray28 = new byte[55]
    {
      (byte) 22,
      (byte) 219,
      (byte) 109,
      (byte) 241,
      (byte) 111,
      (byte) 164,
      (byte) 144 /*0x90*/,
      (byte) 181,
      (byte) 168,
      byte.MaxValue,
      (byte) 203,
      (byte) 254,
      (byte) 196,
      (byte) 216,
      (byte) 148,
      (byte) 207,
      (byte) 219,
      (byte) 184,
      (byte) 106,
      (byte) 155,
      (byte) 195,
      (byte) 97,
      (byte) 243,
      (byte) 154,
      (byte) 229,
      (byte) 199,
      (byte) 146,
      (byte) 233,
      (byte) 183,
      (byte) 183,
      (byte) 147,
      (byte) 245,
      (byte) 62,
      (byte) 195,
      (byte) 90,
      (byte) 2,
      (byte) 235,
      (byte) 242,
      (byte) 235,
      (byte) 105,
      (byte) 6,
      (byte) 167,
      (byte) 242,
      (byte) 247,
      (byte) 132,
      (byte) 194,
      (byte) 223,
      (byte) 199,
      (byte) 112 /*0x70*/,
      (byte) 17,
      (byte) 85,
      (byte) 224 /*0xE0*/,
      (byte) 42,
      (byte) 140,
      (byte) 61
    };
    key.Query(true, 335, numArray27, numArray27);
    Array.Copy((Array) numArray27, 0, (Array) numArray16, 275, 55);
    for (int index = 0; index < 55; ++index)
      numArray16[index + 275] ^= numArray28[index];
    byte[] numArray29 = new byte[19];
    numArray29[0] = (byte) 74;
    numArray29[15] = (byte) 242;
    numArray29[2] = (byte) 44;
    numArray29[10] = (byte) 73;
    numArray29[18] = (byte) 95;
    numArray29[5] = (byte) 222;
    numArray29[6] = (byte) 231;
    numArray29[17] = (byte) 191;
    numArray29[8] = (byte) 147;
    numArray29[9] = (byte) 241;
    numArray29[1] = (byte) 92;
    numArray29[11] = (byte) 182;
    numArray29[12] = (byte) 99;
    numArray29[4] = (byte) 42;
    numArray29[14] = (byte) 67;
    numArray29[7] = (byte) 246;
    numArray29[3] = (byte) 155;
    numArray29[13] = (byte) 130;
    numArray29[16 /*0x10*/] = (byte) 117;
    byte[] numArray30 = new byte[19];
    numArray30[7] = (byte) 242;
    numArray30[1] = (byte) 133;
    numArray30[2] = (byte) 179;
    numArray30[3] = (byte) 241;
    numArray30[6] = (byte) 53;
    numArray30[5] = (byte) 165;
    numArray30[18] = (byte) 134;
    numArray30[15] = (byte) 212;
    numArray30[17] = (byte) 66;
    numArray30[9] = (byte) 187;
    numArray30[14] = (byte) 57;
    numArray30[11] = (byte) 44;
    numArray30[12] = (byte) 142;
    numArray30[4] = (byte) 238;
    numArray30[16 /*0x10*/] = (byte) 61;
    numArray30[13] = (byte) 68;
    numArray30[10] = (byte) 164;
    numArray30[0] = (byte) 233;
    numArray30[8] = (byte) 69;
    key.Query(true, 335, numArray29, numArray29);
    Array.Copy((Array) numArray29, 0, (Array) numArray16, 330, 19);
    for (int index = 0; index < 19; ++index)
      numArray16[index + 330] ^= numArray30[index];
    return Encoding.UTF8.GetString(numArray16);
  }

  internal static string ssp_appserver_13477()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[119];
      byte[] numArray2 = new byte[55];
      numArray2[31 /*0x1F*/] = (byte) 26;
      numArray2[1] = (byte) 160 /*0xA0*/;
      numArray2[44] = (byte) 127 /*0x7F*/;
      numArray2[42] = (byte) 188;
      numArray2[4] = (byte) 220;
      numArray2[5] = (byte) 63 /*0x3F*/;
      numArray2[27] = (byte) 111;
      numArray2[7] = (byte) 241;
      numArray2[8] = (byte) 114;
      numArray2[9] = (byte) 70;
      numArray2[37] = (byte) 29;
      numArray2[11] = (byte) 135;
      numArray2[3] = (byte) 75;
      numArray2[46] = (byte) 140;
      numArray2[14] = (byte) 199;
      numArray2[15] = (byte) 47;
      numArray2[30] = (byte) 5;
      numArray2[17] = (byte) 60;
      numArray2[18] = (byte) 16 /*0x10*/;
      numArray2[45] = (byte) 178;
      numArray2[48 /*0x30*/] = (byte) 129;
      numArray2[53] = (byte) 38;
      numArray2[22] = (byte) 24;
      numArray2[23] = (byte) 30;
      numArray2[21] = (byte) 50;
      numArray2[25] = (byte) 31 /*0x1F*/;
      numArray2[26] = (byte) 151;
      numArray2[16 /*0x10*/] = (byte) 90;
      numArray2[28] = (byte) 253;
      numArray2[29] = (byte) 55;
      numArray2[40] = (byte) 236;
      numArray2[0] = (byte) 133;
      numArray2[20] = (byte) 221;
      numArray2[13] = (byte) 153;
      numArray2[24] = (byte) 108;
      numArray2[12] = (byte) 192 /*0xC0*/;
      numArray2[36] = (byte) 9;
      numArray2[54] = (byte) 233;
      numArray2[38] = (byte) 108;
      numArray2[39] = (byte) 104;
      numArray2[35] = (byte) 198;
      numArray2[41] = (byte) 72;
      numArray2[43] = (byte) 246;
      numArray2[6] = (byte) 170;
      numArray2[2] = (byte) 125;
      numArray2[50] = (byte) 62;
      numArray2[10] = (byte) 119;
      numArray2[32 /*0x20*/] = (byte) 91;
      numArray2[34] = (byte) 186;
      numArray2[49] = (byte) 100;
      numArray2[47] = (byte) 69;
      numArray2[51] = (byte) 105;
      numArray2[52] = (byte) 211;
      numArray2[33] = (byte) 215;
      numArray2[19] = (byte) 182;
      byte[] numArray3 = new byte[55]
      {
        (byte) 95,
        (byte) 169,
        (byte) 253,
        (byte) 231,
        (byte) 127 /*0x7F*/,
        (byte) 97,
        (byte) 169,
        (byte) 137,
        (byte) 232,
        (byte) 137,
        (byte) 105,
        (byte) 29,
        (byte) 162,
        (byte) 180,
        (byte) 215,
        (byte) 96 /*0x60*/,
        (byte) 65,
        (byte) 124,
        (byte) 28,
        (byte) 91,
        (byte) 103,
        (byte) 248,
        (byte) 23,
        (byte) 88,
        (byte) 221,
        (byte) 209,
        byte.MaxValue,
        (byte) 245,
        (byte) 90,
        (byte) 106,
        (byte) 153,
        (byte) 100,
        (byte) 37,
        (byte) 125,
        (byte) 195,
        (byte) 101,
        (byte) 84,
        (byte) 2,
        (byte) 62,
        (byte) 59,
        (byte) 19,
        (byte) 30,
        (byte) 21,
        (byte) 96 /*0x60*/,
        (byte) 175,
        (byte) 195,
        (byte) 71,
        (byte) 168,
        (byte) 185,
        (byte) 5,
        (byte) 85,
        (byte) 202,
        (byte) 118,
        (byte) 106,
        (byte) 141
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 191,
        (byte) 37,
        (byte) 42,
        (byte) 85,
        (byte) 76,
        (byte) 91,
        (byte) 66,
        (byte) 165,
        (byte) 22,
        (byte) 246,
        (byte) 250,
        (byte) 120,
        (byte) 196,
        (byte) 123,
        byte.MaxValue,
        (byte) 31 /*0x1F*/,
        (byte) 231,
        (byte) 23,
        (byte) 65,
        (byte) 246,
        (byte) 107,
        (byte) 125,
        (byte) 50,
        (byte) 239,
        (byte) 235,
        (byte) 109,
        (byte) 91,
        (byte) 247,
        (byte) 1,
        (byte) 28,
        (byte) 217,
        (byte) 154,
        (byte) 139,
        (byte) 214,
        (byte) 235,
        (byte) 23,
        (byte) 133,
        (byte) 106,
        (byte) 101,
        (byte) 248,
        (byte) 62,
        (byte) 16 /*0x10*/,
        (byte) 11,
        (byte) 144 /*0x90*/,
        (byte) 169,
        (byte) 238,
        (byte) 187,
        (byte) 194,
        (byte) 1,
        (byte) 122,
        (byte) 67,
        (byte) 68,
        (byte) 18,
        (byte) 229,
        (byte) 251
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 127 /*0x7F*/,
        (byte) 73,
        (byte) 235,
        (byte) 57,
        (byte) 78,
        (byte) 29,
        (byte) 123,
        (byte) 143,
        (byte) 37,
        (byte) 251,
        (byte) 149,
        (byte) 248,
        (byte) 226,
        (byte) 97,
        (byte) 173,
        (byte) 254,
        (byte) 68,
        (byte) 206,
        (byte) 0,
        (byte) 91,
        (byte) 85,
        (byte) 87,
        (byte) 136,
        (byte) 109,
        (byte) 17,
        (byte) 172,
        (byte) 10,
        (byte) 200,
        (byte) 228,
        (byte) 179,
        (byte) 167,
        (byte) 249,
        (byte) 132,
        (byte) 156,
        (byte) 96 /*0x60*/,
        (byte) 159,
        (byte) 145,
        (byte) 112 /*0x70*/,
        (byte) 79,
        (byte) 111,
        (byte) 121,
        (byte) 236,
        (byte) 106,
        (byte) 27,
        (byte) 34,
        (byte) 112 /*0x70*/,
        (byte) 208 /*0xD0*/,
        (byte) 254,
        (byte) 200,
        (byte) 216,
        (byte) 154,
        (byte) 203,
        (byte) 90,
        (byte) 138,
        (byte) 26
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[9]
      {
        (byte) 59,
        (byte) 106,
        (byte) 223,
        (byte) 0,
        (byte) 75,
        (byte) 163,
        (byte) 249,
        (byte) 28,
        (byte) 95
      };
      byte[] numArray7 = new byte[9]
      {
        (byte) 97,
        (byte) 83,
        (byte) 187,
        (byte) 47,
        (byte) 242,
        (byte) 85,
        (byte) 239,
        (byte) 200,
        (byte) 42
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[119];
    byte[] numArray9 = new byte[55];
    numArray9[19] = (byte) 218;
    numArray9[50] = (byte) 136;
    numArray9[35] = (byte) 224 /*0xE0*/;
    numArray9[38] = (byte) 201;
    numArray9[7] = (byte) 113;
    numArray9[15] = (byte) 217;
    numArray9[6] = (byte) 91;
    numArray9[13] = (byte) 245;
    numArray9[8] = (byte) 162;
    numArray9[21] = (byte) 36;
    numArray9[10] = (byte) 253;
    numArray9[53] = (byte) 19;
    numArray9[12] = (byte) 152;
    numArray9[37] = (byte) 146;
    numArray9[14] = (byte) 134;
    numArray9[51] = (byte) 215;
    numArray9[16 /*0x10*/] = (byte) 148;
    numArray9[17] = (byte) 136;
    numArray9[31 /*0x1F*/] = (byte) 1;
    numArray9[24] = (byte) 133;
    numArray9[20] = (byte) 169;
    numArray9[49] = (byte) 32 /*0x20*/;
    numArray9[22] = (byte) 73;
    numArray9[52] = (byte) 69;
    numArray9[25] = (byte) 247;
    numArray9[41] = (byte) 208 /*0xD0*/;
    numArray9[30] = (byte) 74;
    numArray9[27] = (byte) 108;
    numArray9[28] = (byte) 17;
    numArray9[32 /*0x20*/] = (byte) 205;
    numArray9[18] = (byte) 166;
    numArray9[33] = (byte) 49;
    numArray9[34] = (byte) 25;
    numArray9[0] = (byte) 232;
    numArray9[11] = (byte) 177;
    numArray9[36] = (byte) 198;
    numArray9[9] = (byte) 69;
    numArray9[4] = (byte) 73;
    numArray9[5] = (byte) 183;
    numArray9[39] = (byte) 45;
    numArray9[40] = (byte) 160 /*0xA0*/;
    numArray9[1] = (byte) 154;
    numArray9[44] = (byte) 109;
    numArray9[43] = (byte) 245;
    numArray9[2] = (byte) 125;
    numArray9[45] = (byte) 45;
    numArray9[46] = (byte) 103;
    numArray9[47] = (byte) 85;
    numArray9[42] = (byte) 81;
    numArray9[29] = (byte) 116;
    numArray9[23] = (byte) 0;
    numArray9[3] = (byte) 178;
    numArray9[26] = (byte) 243;
    numArray9[48 /*0x30*/] = (byte) 29;
    numArray9[54] = (byte) 231;
    byte[] numArray10 = new byte[55];
    numArray10[51] = (byte) 47;
    numArray10[1] = (byte) 194;
    numArray10[2] = (byte) 224 /*0xE0*/;
    numArray10[3] = (byte) 37;
    numArray10[43] = (byte) 156;
    numArray10[18] = (byte) 117;
    numArray10[28] = (byte) 34;
    numArray10[19] = (byte) 222;
    numArray10[9] = (byte) 64 /*0x40*/;
    numArray10[5] = (byte) 224 /*0xE0*/;
    numArray10[23] = (byte) 31 /*0x1F*/;
    numArray10[0] = (byte) 109;
    numArray10[38] = (byte) 153;
    numArray10[49] = (byte) 142;
    numArray10[41] = (byte) 227;
    numArray10[46] = (byte) 10;
    numArray10[13] = (byte) 245;
    numArray10[31 /*0x1F*/] = (byte) 107;
    numArray10[50] = (byte) 43;
    numArray10[25] = (byte) 65;
    numArray10[27] = (byte) 199;
    numArray10[21] = (byte) 198;
    numArray10[22] = (byte) 185;
    numArray10[20] = (byte) 34;
    numArray10[12] = (byte) 132;
    numArray10[17] = (byte) 94;
    numArray10[16 /*0x10*/] = (byte) 169;
    numArray10[33] = (byte) 142;
    numArray10[53] = (byte) 220;
    numArray10[29] = (byte) 109;
    numArray10[30] = (byte) 34;
    numArray10[10] = (byte) 3;
    numArray10[32 /*0x20*/] = (byte) 190;
    numArray10[26] = (byte) 186;
    numArray10[34] = (byte) 87;
    numArray10[40] = (byte) 177;
    numArray10[7] = (byte) 46;
    numArray10[37] = (byte) 83;
    numArray10[8] = (byte) 191;
    numArray10[39] = (byte) 230;
    numArray10[54] = (byte) 48 /*0x30*/;
    numArray10[35] = (byte) 6;
    numArray10[42] = (byte) 165;
    numArray10[36] = (byte) 25;
    numArray10[44] = (byte) 107;
    numArray10[45] = (byte) 150;
    numArray10[15] = (byte) 142;
    numArray10[4] = (byte) 206;
    numArray10[48 /*0x30*/] = (byte) 22;
    numArray10[11] = (byte) 44;
    numArray10[47] = (byte) 44;
    numArray10[24] = (byte) 115;
    numArray10[52] = (byte) 218;
    numArray10[14] = (byte) 107;
    numArray10[6] = (byte) 160 /*0xA0*/;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 201,
      (byte) 197,
      (byte) 51,
      (byte) 1,
      (byte) 198,
      (byte) 139,
      (byte) 220,
      (byte) 45,
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 226,
      (byte) 34,
      (byte) 132,
      (byte) 106,
      (byte) 10,
      (byte) 147,
      (byte) 205,
      (byte) 116,
      (byte) 101,
      (byte) 207,
      (byte) 230,
      (byte) 238,
      (byte) 239,
      (byte) 171,
      (byte) 51,
      (byte) 121,
      (byte) 159,
      (byte) 123,
      (byte) 138,
      (byte) 49,
      (byte) 141,
      (byte) 147,
      (byte) 37,
      (byte) 239,
      (byte) 149,
      (byte) 204,
      (byte) 128 /*0x80*/,
      (byte) 64 /*0x40*/,
      (byte) 76,
      (byte) 119,
      (byte) 3,
      (byte) 69,
      (byte) 225,
      (byte) 213,
      (byte) 1,
      (byte) 206,
      (byte) 218,
      (byte) 217,
      (byte) 224 /*0xE0*/,
      (byte) 60,
      (byte) 239,
      (byte) 221,
      (byte) 66,
      (byte) 111,
      (byte) 188
    };
    byte[] numArray12 = new byte[55];
    numArray12[44] = (byte) 174;
    numArray12[24] = (byte) 104;
    numArray12[2] = (byte) 74;
    numArray12[10] = (byte) 125;
    numArray12[0] = (byte) 107;
    numArray12[14] = (byte) 93;
    numArray12[39] = (byte) 152;
    numArray12[9] = (byte) 100;
    numArray12[8] = (byte) 187;
    numArray12[42] = (byte) 58;
    numArray12[32 /*0x20*/] = (byte) 125;
    numArray12[11] = (byte) 41;
    numArray12[12] = (byte) 206;
    numArray12[13] = (byte) 59;
    numArray12[35] = (byte) 159;
    numArray12[15] = (byte) 97;
    numArray12[16 /*0x10*/] = (byte) 27;
    numArray12[5] = (byte) 160 /*0xA0*/;
    numArray12[18] = (byte) 192 /*0xC0*/;
    numArray12[4] = (byte) 113;
    numArray12[1] = (byte) 33;
    numArray12[21] = (byte) 76;
    numArray12[33] = (byte) 24;
    numArray12[54] = (byte) 129;
    numArray12[41] = (byte) 249;
    numArray12[25] = (byte) 104;
    numArray12[26] = (byte) 46;
    numArray12[27] = (byte) 233;
    numArray12[20] = (byte) 161;
    numArray12[28] = (byte) 106;
    numArray12[7] = (byte) 142;
    numArray12[31 /*0x1F*/] = (byte) 85;
    numArray12[30] = (byte) 179;
    numArray12[6] = (byte) 146;
    numArray12[34] = (byte) 15;
    numArray12[19] = (byte) 56;
    numArray12[36] = (byte) 215;
    numArray12[37] = (byte) 39;
    numArray12[38] = (byte) 112 /*0x70*/;
    numArray12[23] = (byte) 176 /*0xB0*/;
    numArray12[40] = (byte) 163;
    numArray12[3] = (byte) 219;
    numArray12[17] = (byte) 62;
    numArray12[51] = (byte) 246;
    numArray12[22] = (byte) 232;
    numArray12[45] = (byte) 79;
    numArray12[29] = (byte) 56;
    numArray12[46] = (byte) 204;
    numArray12[47] = (byte) 17;
    numArray12[49] = (byte) 37;
    numArray12[48 /*0x30*/] = (byte) 29;
    numArray12[43] = (byte) 79;
    numArray12[52] = (byte) 42;
    numArray12[53] = (byte) 62;
    numArray12[50] = (byte) 77;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[9]
    {
      (byte) 221,
      (byte) 104,
      (byte) 174,
      (byte) 38,
      (byte) 198,
      (byte) 84,
      (byte) 221,
      (byte) 13,
      (byte) 252
    };
    byte[] numArray14 = new byte[9]
    {
      (byte) 90,
      (byte) 156,
      (byte) 205,
      (byte) 71,
      (byte) 121,
      (byte) 65,
      (byte) 97,
      (byte) 242,
      (byte) 32 /*0x20*/
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 9);
    for (int index = 0; index < 9; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13478()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[221];
      byte[] numArray2 = new byte[55]
      {
        (byte) 26,
        (byte) 25,
        (byte) 42,
        (byte) 98,
        (byte) 42,
        (byte) 254,
        (byte) 75,
        (byte) 1,
        (byte) 247,
        (byte) 147,
        (byte) 88,
        (byte) 193,
        (byte) 248,
        (byte) 36,
        (byte) 223,
        (byte) 250,
        (byte) 253,
        (byte) 17,
        (byte) 24,
        (byte) 238,
        (byte) 58,
        (byte) 27,
        (byte) 10,
        (byte) 80 /*0x50*/,
        (byte) 100,
        (byte) 219,
        (byte) 78,
        (byte) 228,
        (byte) 10,
        (byte) 181,
        (byte) 137,
        (byte) 227,
        (byte) 234,
        (byte) 92,
        (byte) 15,
        (byte) 72,
        (byte) 138,
        (byte) 241,
        (byte) 121,
        (byte) 28,
        (byte) 129,
        (byte) 186,
        (byte) 229,
        (byte) 213,
        (byte) 85,
        (byte) 100,
        (byte) 206,
        (byte) 47,
        (byte) 253,
        (byte) 18,
        byte.MaxValue,
        (byte) 54,
        (byte) 73,
        (byte) 43,
        (byte) 11
      };
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 38;
      numArray3[1] = (byte) 141;
      numArray3[24] = (byte) 70;
      numArray3[3] = (byte) 76;
      numArray3[4] = (byte) 149;
      numArray3[34] = (byte) 97;
      numArray3[20] = (byte) 90;
      numArray3[43] = (byte) 85;
      numArray3[8] = (byte) 230;
      numArray3[21] = (byte) 236;
      numArray3[2] = (byte) 228;
      numArray3[47] = (byte) 0;
      numArray3[5] = (byte) 108;
      numArray3[35] = (byte) 234;
      numArray3[14] = (byte) 243;
      numArray3[49] = (byte) 208 /*0xD0*/;
      numArray3[48 /*0x30*/] = (byte) 239;
      numArray3[17] = byte.MaxValue;
      numArray3[18] = (byte) 247;
      numArray3[19] = (byte) 21;
      numArray3[25] = (byte) 244;
      numArray3[40] = (byte) 176 /*0xB0*/;
      numArray3[23] = (byte) 74;
      numArray3[0] = (byte) 101;
      numArray3[13] = (byte) 234;
      numArray3[15] = (byte) 38;
      numArray3[26] = (byte) 191;
      numArray3[16 /*0x10*/] = (byte) 106;
      numArray3[12] = (byte) 125;
      numArray3[22] = (byte) 235;
      numArray3[30] = (byte) 224 /*0xE0*/;
      numArray3[31 /*0x1F*/] = (byte) 143;
      numArray3[32 /*0x20*/] = (byte) 174;
      numArray3[6] = (byte) 204;
      numArray3[50] = (byte) 247;
      numArray3[37] = (byte) 134;
      numArray3[36] = (byte) 147;
      numArray3[42] = (byte) 112 /*0x70*/;
      numArray3[38] = (byte) 230;
      numArray3[7] = (byte) 115;
      numArray3[39] = (byte) 143;
      numArray3[33] = (byte) 167;
      numArray3[27] = (byte) 70;
      numArray3[11] = (byte) 162;
      numArray3[44] = (byte) 205;
      numArray3[45] = (byte) 33;
      numArray3[46] = (byte) 5;
      numArray3[29] = (byte) 59;
      numArray3[28] = (byte) 211;
      numArray3[41] = (byte) 169;
      numArray3[9] = (byte) 42;
      numArray3[10] = (byte) 241;
      numArray3[52] = (byte) 138;
      numArray3[53] = (byte) 153;
      numArray3[54] = (byte) 228;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[10] = (byte) 74;
      numArray4[1] = (byte) 253;
      numArray4[21] = (byte) 25;
      numArray4[3] = (byte) 144 /*0x90*/;
      numArray4[2] = (byte) 64 /*0x40*/;
      numArray4[12] = (byte) 93;
      numArray4[6] = (byte) 139;
      numArray4[38] = (byte) 155;
      numArray4[25] = (byte) 129;
      numArray4[9] = (byte) 23;
      numArray4[31 /*0x1F*/] = (byte) 198;
      numArray4[36] = (byte) 234;
      numArray4[34] = (byte) 208 /*0xD0*/;
      numArray4[30] = (byte) 1;
      numArray4[14] = (byte) 24;
      numArray4[51] = (byte) 106;
      numArray4[16 /*0x10*/] = (byte) 143;
      numArray4[50] = (byte) 190;
      numArray4[4] = (byte) 127 /*0x7F*/;
      numArray4[40] = (byte) 24;
      numArray4[20] = (byte) 105;
      numArray4[46] = (byte) 9;
      numArray4[24] = (byte) 4;
      numArray4[23] = (byte) 218;
      numArray4[13] = (byte) 191;
      numArray4[15] = (byte) 108;
      numArray4[11] = (byte) 4;
      numArray4[27] = (byte) 194;
      numArray4[28] = (byte) 180;
      numArray4[17] = (byte) 82;
      numArray4[18] = (byte) 187;
      numArray4[49] = (byte) 50;
      numArray4[32 /*0x20*/] = (byte) 71;
      numArray4[33] = (byte) 96 /*0x60*/;
      numArray4[22] = (byte) 114;
      numArray4[35] = (byte) 245;
      numArray4[47] = (byte) 227;
      numArray4[37] = (byte) 219;
      numArray4[44] = (byte) 232;
      numArray4[39] = (byte) 198;
      numArray4[19] = (byte) 145;
      numArray4[52] = (byte) 207;
      numArray4[42] = (byte) 5;
      numArray4[43] = (byte) 117;
      numArray4[8] = (byte) 219;
      numArray4[45] = (byte) 22;
      numArray4[26] = (byte) 85;
      numArray4[29] = (byte) 75;
      numArray4[48 /*0x30*/] = (byte) 219;
      numArray4[0] = (byte) 223;
      numArray4[7] = (byte) 182;
      numArray4[41] = (byte) 21;
      numArray4[5] = (byte) 254;
      numArray4[53] = (byte) 55;
      numArray4[54] = (byte) 111;
      byte[] numArray5 = new byte[55];
      numArray5[4] = (byte) 73;
      numArray5[27] = (byte) 124;
      numArray5[40] = (byte) 136;
      numArray5[19] = (byte) 213;
      numArray5[33] = (byte) 194;
      numArray5[6] = (byte) 91;
      numArray5[2] = (byte) 205;
      numArray5[46] = (byte) 33;
      numArray5[3] = (byte) 48 /*0x30*/;
      numArray5[9] = (byte) 41;
      numArray5[10] = (byte) 107;
      numArray5[11] = (byte) 212;
      numArray5[1] = (byte) 28;
      numArray5[21] = (byte) 70;
      numArray5[44] = (byte) 254;
      numArray5[15] = (byte) 207;
      numArray5[16 /*0x10*/] = (byte) 174;
      numArray5[18] = (byte) 131;
      numArray5[13] = (byte) 135;
      numArray5[17] = (byte) 99;
      numArray5[26] = (byte) 19;
      numArray5[0] = (byte) 221;
      numArray5[32 /*0x20*/] = (byte) 198;
      numArray5[37] = (byte) 197;
      numArray5[24] = (byte) 159;
      numArray5[25] = (byte) 162;
      numArray5[12] = (byte) 153;
      numArray5[8] = (byte) 235;
      numArray5[30] = (byte) 89;
      numArray5[29] = (byte) 113;
      numArray5[38] = (byte) 82;
      numArray5[31 /*0x1F*/] = (byte) 50;
      numArray5[43] = (byte) 133;
      numArray5[23] = (byte) 134;
      numArray5[34] = (byte) 157;
      numArray5[35] = (byte) 24;
      numArray5[36] = (byte) 225;
      numArray5[47] = (byte) 138;
      numArray5[42] = (byte) 196;
      numArray5[5] = (byte) 241;
      numArray5[39] = (byte) 174;
      numArray5[41] = (byte) 21;
      numArray5[7] = (byte) 72;
      numArray5[14] = (byte) 68;
      numArray5[28] = (byte) 9;
      numArray5[45] = (byte) 132;
      numArray5[20] = (byte) 110;
      numArray5[22] = (byte) 63 /*0x3F*/;
      numArray5[48 /*0x30*/] = (byte) 36;
      numArray5[49] = (byte) 218;
      numArray5[50] = (byte) 211;
      numArray5[51] = (byte) 119;
      numArray5[52] = (byte) 194;
      numArray5[53] = (byte) 203;
      numArray5[54] = (byte) 182;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 230,
        (byte) 11,
        (byte) 26,
        (byte) 183,
        (byte) 180,
        (byte) 174,
        (byte) 129,
        (byte) 156,
        (byte) 111,
        (byte) 91,
        (byte) 53,
        (byte) 141,
        (byte) 217,
        (byte) 237,
        (byte) 182,
        (byte) 4,
        (byte) 239,
        (byte) 189,
        (byte) 173,
        (byte) 42,
        (byte) 190,
        (byte) 181,
        (byte) 232,
        (byte) 106,
        (byte) 110,
        (byte) 42,
        (byte) 31 /*0x1F*/,
        (byte) 219,
        (byte) 75,
        (byte) 26,
        (byte) 191,
        (byte) 251,
        (byte) 65,
        (byte) 237,
        (byte) 42,
        (byte) 163,
        (byte) 251,
        (byte) 48 /*0x30*/,
        (byte) 184,
        (byte) 229,
        (byte) 241,
        (byte) 67,
        (byte) 104,
        (byte) 80 /*0x50*/,
        (byte) 203,
        (byte) 130,
        (byte) 40,
        (byte) 197,
        (byte) 239,
        (byte) 148,
        (byte) 19,
        (byte) 214,
        (byte) 44,
        (byte) 234,
        (byte) 48 /*0x30*/
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 216,
        (byte) 171,
        (byte) 39,
        (byte) 119,
        (byte) 186,
        (byte) 42,
        (byte) 2,
        (byte) 97,
        (byte) 137,
        (byte) 60,
        (byte) 128 /*0x80*/,
        (byte) 237,
        (byte) 239,
        (byte) 76,
        (byte) 61,
        (byte) 12,
        (byte) 178,
        (byte) 232,
        (byte) 150,
        (byte) 7,
        (byte) 67,
        (byte) 118,
        (byte) 36,
        (byte) 99,
        (byte) 247,
        (byte) 239,
        (byte) 230,
        (byte) 241,
        (byte) 248,
        (byte) 75,
        (byte) 147,
        (byte) 201,
        (byte) 180,
        (byte) 129,
        (byte) 59,
        (byte) 32 /*0x20*/,
        (byte) 112 /*0x70*/,
        (byte) 253,
        (byte) 249,
        (byte) 117,
        (byte) 123,
        (byte) 8,
        (byte) 105,
        (byte) 224 /*0xE0*/,
        (byte) 138,
        (byte) 21,
        (byte) 172,
        (byte) 224 /*0xE0*/,
        (byte) 225,
        (byte) 113,
        (byte) 245,
        (byte) 132,
        (byte) 201,
        (byte) 111,
        (byte) 169
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 176 /*0xB0*/,
        (byte) 72,
        (byte) 137,
        (byte) 142,
        (byte) 165,
        (byte) 81,
        (byte) 84,
        (byte) 156,
        (byte) 57,
        (byte) 76,
        (byte) 235,
        (byte) 194,
        (byte) 18,
        (byte) 8,
        (byte) 190,
        (byte) 4,
        (byte) 200,
        (byte) 57,
        (byte) 123,
        (byte) 76,
        (byte) 151,
        (byte) 159,
        (byte) 236,
        (byte) 1,
        (byte) 90,
        (byte) 236,
        (byte) 183,
        (byte) 7,
        (byte) 233,
        (byte) 207,
        (byte) 86,
        (byte) 189,
        (byte) 248,
        (byte) 128 /*0x80*/,
        (byte) 75,
        (byte) 231,
        (byte) 47,
        (byte) 86,
        (byte) 75,
        (byte) 241,
        (byte) 19,
        (byte) 180,
        (byte) 145,
        (byte) 53,
        (byte) 61,
        (byte) 2,
        (byte) 221,
        (byte) 139,
        (byte) 84,
        (byte) 209,
        (byte) 100,
        (byte) 176 /*0xB0*/,
        (byte) 181,
        (byte) 228,
        (byte) 139
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 61,
        (byte) 227,
        (byte) 8,
        (byte) 154,
        (byte) 87,
        (byte) 180,
        (byte) 74,
        (byte) 235,
        (byte) 129,
        (byte) 205,
        (byte) 22,
        (byte) 124,
        (byte) 137,
        (byte) 127 /*0x7F*/,
        (byte) 120,
        (byte) 0,
        (byte) 21,
        (byte) 112 /*0x70*/,
        (byte) 242,
        (byte) 124,
        (byte) 59,
        (byte) 253,
        (byte) 224 /*0xE0*/,
        (byte) 210,
        (byte) 122,
        (byte) 71,
        (byte) 83,
        (byte) 143,
        (byte) 31 /*0x1F*/,
        (byte) 103,
        (byte) 194,
        (byte) 168,
        (byte) 19,
        (byte) 182,
        (byte) 98,
        (byte) 59,
        (byte) 138,
        (byte) 50,
        (byte) 182,
        (byte) 154,
        (byte) 208 /*0xD0*/,
        (byte) 81,
        (byte) 128 /*0x80*/,
        (byte) 41,
        (byte) 88,
        (byte) 92,
        (byte) 31 /*0x1F*/,
        (byte) 188,
        (byte) 166,
        (byte) 172,
        (byte) 41,
        (byte) 146,
        (byte) 179,
        (byte) 50,
        (byte) 146
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[1]{ (byte) 136 };
      byte[] numArray11 = new byte[1]{ (byte) 135 };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[221];
    byte[] numArray13 = new byte[55]
    {
      (byte) 179,
      (byte) 106,
      (byte) 245,
      (byte) 46,
      (byte) 202,
      (byte) 145,
      (byte) 189,
      (byte) 214,
      (byte) 212,
      (byte) 113,
      (byte) 238,
      (byte) 137,
      (byte) 56,
      (byte) 227,
      (byte) 13,
      (byte) 182,
      (byte) 196,
      (byte) 87,
      (byte) 14,
      (byte) 43,
      (byte) 179,
      (byte) 23,
      (byte) 106,
      (byte) 166,
      (byte) 7,
      (byte) 20,
      (byte) 245,
      (byte) 58,
      (byte) 139,
      (byte) 167,
      (byte) 33,
      (byte) 145,
      (byte) 87,
      (byte) 198,
      (byte) 0,
      (byte) 39,
      (byte) 63 /*0x3F*/,
      (byte) 14,
      (byte) 56,
      (byte) 75,
      (byte) 205,
      (byte) 125,
      (byte) 253,
      (byte) 242,
      (byte) 194,
      (byte) 43,
      (byte) 171,
      (byte) 167,
      (byte) 3,
      (byte) 165,
      (byte) 195,
      (byte) 220,
      (byte) 171,
      (byte) 110,
      (byte) 154
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 77,
      (byte) 78,
      (byte) 113,
      (byte) 19,
      (byte) 45,
      (byte) 16 /*0x10*/,
      (byte) 1,
      (byte) 84,
      (byte) 150,
      (byte) 1,
      (byte) 184,
      (byte) 133,
      (byte) 205,
      (byte) 120,
      (byte) 176 /*0xB0*/,
      (byte) 222,
      (byte) 162,
      (byte) 97,
      (byte) 19,
      (byte) 238,
      (byte) 167,
      (byte) 241,
      (byte) 185,
      (byte) 214,
      (byte) 161,
      (byte) 155,
      (byte) 74,
      (byte) 104,
      (byte) 12,
      (byte) 53,
      (byte) 132,
      (byte) 239,
      (byte) 112 /*0x70*/,
      (byte) 150,
      (byte) 90,
      (byte) 247,
      (byte) 132,
      (byte) 4,
      (byte) 237,
      (byte) 175,
      (byte) 236,
      (byte) 218,
      (byte) 143,
      (byte) 6,
      (byte) 167,
      (byte) 10,
      (byte) 122,
      (byte) 100,
      (byte) 231,
      (byte) 239,
      (byte) 226,
      (byte) 251,
      (byte) 232,
      (byte) 134,
      (byte) 100
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[13] = (byte) 118;
    numArray15[1] = (byte) 38;
    numArray15[37] = (byte) 174;
    numArray15[19] = (byte) 26;
    numArray15[46] = (byte) 28;
    numArray15[10] = (byte) 114;
    numArray15[6] = (byte) 45;
    numArray15[7] = (byte) 80 /*0x50*/;
    numArray15[3] = (byte) 156;
    numArray15[9] = (byte) 49;
    numArray15[31 /*0x1F*/] = (byte) 44;
    numArray15[11] = (byte) 226;
    numArray15[12] = (byte) 230;
    numArray15[0] = (byte) 79;
    numArray15[48 /*0x30*/] = (byte) 13;
    numArray15[52] = (byte) 100;
    numArray15[50] = (byte) 231;
    numArray15[17] = (byte) 93;
    numArray15[36] = (byte) 130;
    numArray15[54] = (byte) 136;
    numArray15[15] = (byte) 31 /*0x1F*/;
    numArray15[24] = (byte) 77;
    numArray15[29] = (byte) 230;
    numArray15[21] = (byte) 227;
    numArray15[20] = (byte) 119;
    numArray15[25] = (byte) 54;
    numArray15[18] = (byte) 53;
    numArray15[27] = (byte) 227;
    numArray15[28] = (byte) 68;
    numArray15[35] = (byte) 231;
    numArray15[4] = (byte) 131;
    numArray15[16 /*0x10*/] = (byte) 195;
    numArray15[32 /*0x20*/] = (byte) 100;
    numArray15[33] = (byte) 39;
    numArray15[2] = (byte) 181;
    numArray15[23] = (byte) 104;
    numArray15[22] = (byte) 220;
    numArray15[47] = (byte) 46;
    numArray15[34] = (byte) 240 /*0xF0*/;
    numArray15[39] = (byte) 50;
    numArray15[42] = (byte) 100;
    numArray15[41] = (byte) 162;
    numArray15[38] = (byte) 74;
    numArray15[43] = (byte) 249;
    numArray15[44] = (byte) 57;
    numArray15[5] = (byte) 75;
    numArray15[51] = (byte) 16 /*0x10*/;
    numArray15[26] = (byte) 229;
    numArray15[14] = (byte) 181;
    numArray15[49] = (byte) 189;
    numArray15[30] = (byte) 233;
    numArray15[53] = (byte) 130;
    numArray15[45] = (byte) 197;
    numArray15[8] = (byte) 93;
    numArray15[40] = (byte) 207;
    byte[] numArray16 = new byte[55]
    {
      (byte) 221,
      (byte) 29,
      (byte) 119,
      (byte) 168,
      (byte) 134,
      (byte) 40,
      (byte) 32 /*0x20*/,
      (byte) 149,
      (byte) 57,
      (byte) 77,
      (byte) 113,
      (byte) 112 /*0x70*/,
      (byte) 211,
      (byte) 160 /*0xA0*/,
      (byte) 110,
      (byte) 114,
      (byte) 254,
      (byte) 165,
      (byte) 67,
      (byte) 74,
      (byte) 102,
      (byte) 119,
      (byte) 121,
      (byte) 181,
      (byte) 212,
      (byte) 253,
      (byte) 234,
      (byte) 34,
      (byte) 216,
      (byte) 116,
      (byte) 186,
      (byte) 175,
      (byte) 139,
      (byte) 197,
      (byte) 156,
      (byte) 123,
      (byte) 18,
      (byte) 166,
      (byte) 64 /*0x40*/,
      (byte) 8,
      (byte) 103,
      (byte) 135,
      (byte) 142,
      (byte) 6,
      (byte) 242,
      (byte) 97,
      (byte) 64 /*0x40*/,
      (byte) 230,
      (byte) 166,
      (byte) 80 /*0x50*/,
      (byte) 51,
      (byte) 240 /*0xF0*/,
      (byte) 170,
      (byte) 20,
      (byte) 212
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 30,
      (byte) 199,
      (byte) 50,
      (byte) 174,
      (byte) 115,
      (byte) 132,
      (byte) 119,
      (byte) 111,
      (byte) 127 /*0x7F*/,
      (byte) 155,
      (byte) 32 /*0x20*/,
      (byte) 1,
      (byte) 49,
      (byte) 105,
      (byte) 181,
      (byte) 20,
      (byte) 54,
      (byte) 197,
      (byte) 62,
      (byte) 144 /*0x90*/,
      (byte) 48 /*0x30*/,
      (byte) 60,
      (byte) 64 /*0x40*/,
      (byte) 188,
      (byte) 14,
      (byte) 150,
      (byte) 103,
      (byte) 44,
      (byte) 219,
      (byte) 101,
      (byte) 171,
      (byte) 234,
      (byte) 152,
      (byte) 52,
      (byte) 21,
      (byte) 151,
      (byte) 203,
      (byte) 30,
      (byte) 17,
      (byte) 17,
      (byte) 118,
      (byte) 66,
      (byte) 115,
      (byte) 171,
      (byte) 60,
      (byte) 166,
      (byte) 14,
      (byte) 182,
      (byte) 28,
      (byte) 233,
      (byte) 43,
      (byte) 114,
      (byte) 117,
      (byte) 91,
      (byte) 55
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 71,
      (byte) 155,
      (byte) 38,
      (byte) 217,
      (byte) 97,
      (byte) 153,
      (byte) 73,
      (byte) 253,
      (byte) 226,
      (byte) 167,
      (byte) 77,
      (byte) 226,
      (byte) 185,
      (byte) 132,
      (byte) 179,
      (byte) 138,
      (byte) 115,
      (byte) 44,
      (byte) 140,
      (byte) 166,
      (byte) 61,
      (byte) 87,
      (byte) 111,
      (byte) 175,
      (byte) 39,
      (byte) 87,
      (byte) 196,
      (byte) 109,
      (byte) 195,
      (byte) 100,
      (byte) 38,
      (byte) 153,
      (byte) 6,
      (byte) 51,
      (byte) 250,
      (byte) 247,
      (byte) 146,
      (byte) 25,
      (byte) 46,
      (byte) 63 /*0x3F*/,
      (byte) 109,
      (byte) 55,
      (byte) 15,
      (byte) 236,
      (byte) 45,
      (byte) 61,
      (byte) 151,
      (byte) 193,
      (byte) 144 /*0x90*/,
      (byte) 22,
      (byte) 76,
      (byte) 210,
      (byte) 115,
      (byte) 183,
      (byte) 125
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[15] = (byte) 179;
    numArray19[30] = (byte) 60;
    numArray19[42] = (byte) 6;
    numArray19[19] = (byte) 200;
    numArray19[49] = (byte) 190;
    numArray19[5] = (byte) 147;
    numArray19[22] = (byte) 104;
    numArray19[28] = (byte) 56;
    numArray19[35] = (byte) 126;
    numArray19[9] = (byte) 105;
    numArray19[4] = (byte) 75;
    numArray19[11] = (byte) 83;
    numArray19[7] = (byte) 240 /*0xF0*/;
    numArray19[13] = (byte) 72;
    numArray19[33] = (byte) 17;
    numArray19[12] = (byte) 30;
    numArray19[45] = (byte) 98;
    numArray19[17] = (byte) 26;
    numArray19[39] = (byte) 174;
    numArray19[3] = (byte) 28;
    numArray19[34] = (byte) 85;
    numArray19[18] = (byte) 237;
    numArray19[54] = (byte) 241;
    numArray19[53] = (byte) 25;
    numArray19[41] = (byte) 187;
    numArray19[25] = (byte) 16 /*0x10*/;
    numArray19[26] = (byte) 242;
    numArray19[27] = (byte) 58;
    numArray19[21] = (byte) 127 /*0x7F*/;
    numArray19[29] = (byte) 116;
    numArray19[52] = (byte) 74;
    numArray19[31 /*0x1F*/] = (byte) 252;
    numArray19[14] = (byte) 24;
    numArray19[48 /*0x30*/] = (byte) 18;
    numArray19[51] = (byte) 65;
    numArray19[47] = (byte) 160 /*0xA0*/;
    numArray19[36] = (byte) 52;
    numArray19[37] = (byte) 245;
    numArray19[38] = (byte) 42;
    numArray19[32 /*0x20*/] = (byte) 51;
    numArray19[1] = (byte) 159;
    numArray19[10] = (byte) 252;
    numArray19[44] = (byte) 17;
    numArray19[43] = (byte) 60;
    numArray19[2] = (byte) 221;
    numArray19[40] = (byte) 90;
    numArray19[46] = (byte) 254;
    numArray19[23] = (byte) 136;
    numArray19[8] = (byte) 54;
    numArray19[6] = (byte) 25;
    numArray19[50] = (byte) 73;
    numArray19[20] = (byte) 148;
    numArray19[16 /*0x10*/] = (byte) 44;
    numArray19[0] = (byte) 118;
    numArray19[24] = (byte) 177;
    byte[] numArray20 = new byte[55];
    numArray20[33] = (byte) 127 /*0x7F*/;
    numArray20[3] = (byte) 119;
    numArray20[2] = (byte) 225;
    numArray20[1] = (byte) 101;
    numArray20[42] = (byte) 68;
    numArray20[5] = (byte) 18;
    numArray20[6] = (byte) 54;
    numArray20[7] = (byte) 59;
    numArray20[8] = (byte) 74;
    numArray20[31 /*0x1F*/] = (byte) 162;
    numArray20[21] = (byte) 226;
    numArray20[0] = (byte) 64 /*0x40*/;
    numArray20[53] = (byte) 91;
    numArray20[51] = (byte) 29;
    numArray20[39] = (byte) 57;
    numArray20[44] = (byte) 149;
    numArray20[20] = (byte) 227;
    numArray20[14] = (byte) 144 /*0x90*/;
    numArray20[18] = (byte) 117;
    numArray20[9] = (byte) 108;
    numArray20[11] = (byte) 237;
    numArray20[50] = (byte) 160 /*0xA0*/;
    numArray20[22] = (byte) 11;
    numArray20[43] = (byte) 146;
    numArray20[12] = (byte) 182;
    numArray20[23] = (byte) 93;
    numArray20[10] = (byte) 221;
    numArray20[27] = (byte) 11;
    numArray20[15] = (byte) 243;
    numArray20[19] = (byte) 48 /*0x30*/;
    numArray20[16 /*0x10*/] = (byte) 14;
    numArray20[32 /*0x20*/] = (byte) 55;
    numArray20[26] = (byte) 124;
    numArray20[28] = (byte) 47;
    numArray20[34] = (byte) 254;
    numArray20[35] = (byte) 238;
    numArray20[36] = (byte) 91;
    numArray20[25] = (byte) 43;
    numArray20[38] = (byte) 75;
    numArray20[4] = (byte) 177;
    numArray20[40] = (byte) 171;
    numArray20[41] = (byte) 200;
    numArray20[17] = (byte) 51;
    numArray20[29] = (byte) 52;
    numArray20[49] = (byte) 254;
    numArray20[45] = (byte) 48 /*0x30*/;
    numArray20[46] = (byte) 0;
    numArray20[47] = (byte) 245;
    numArray20[48 /*0x30*/] = (byte) 110;
    numArray20[13] = (byte) 159;
    numArray20[37] = (byte) 112 /*0x70*/;
    numArray20[24] = (byte) 252;
    numArray20[52] = (byte) 148;
    numArray20[30] = (byte) 43;
    numArray20[54] = (byte) 108;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[1]{ (byte) 121 };
    byte[] numArray22 = new byte[1]{ (byte) 16 /*0x10*/ };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 1);
    for (int index = 0; index < 1; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_13479()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[254];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 140;
      numArray2[19] = (byte) 135;
      numArray2[39] = (byte) 100;
      numArray2[29] = (byte) 94;
      numArray2[28] = (byte) 135;
      numArray2[5] = (byte) 180;
      numArray2[26] = (byte) 150;
      numArray2[12] = (byte) 12;
      numArray2[13] = (byte) 132;
      numArray2[9] = (byte) 59;
      numArray2[17] = (byte) 28;
      numArray2[44] = (byte) 102;
      numArray2[25] = (byte) 195;
      numArray2[35] = (byte) 84;
      numArray2[14] = (byte) 64 /*0x40*/;
      numArray2[23] = (byte) 204;
      numArray2[16 /*0x10*/] = (byte) 211;
      numArray2[7] = (byte) 159;
      numArray2[27] = (byte) 79;
      numArray2[24] = (byte) 183;
      numArray2[10] = (byte) 115;
      numArray2[21] = (byte) 180;
      numArray2[20] = (byte) 156;
      numArray2[8] = (byte) 64 /*0x40*/;
      numArray2[1] = (byte) 136;
      numArray2[32 /*0x20*/] = (byte) 185;
      numArray2[46] = (byte) 55;
      numArray2[40] = (byte) 16 /*0x10*/;
      numArray2[15] = (byte) 161;
      numArray2[3] = (byte) 153;
      numArray2[30] = (byte) 188;
      numArray2[31 /*0x1F*/] = (byte) 19;
      numArray2[50] = (byte) 59;
      numArray2[33] = (byte) 198;
      numArray2[0] = (byte) 124;
      numArray2[11] = (byte) 48 /*0x30*/;
      numArray2[36] = (byte) 29;
      numArray2[4] = (byte) 229;
      numArray2[2] = (byte) 166;
      numArray2[38] = (byte) 31 /*0x1F*/;
      numArray2[22] = (byte) 89;
      numArray2[41] = byte.MaxValue;
      numArray2[47] = (byte) 197;
      numArray2[43] = (byte) 21;
      numArray2[6] = (byte) 22;
      numArray2[45] = (byte) 134;
      numArray2[49] = (byte) 215;
      numArray2[53] = (byte) 129;
      numArray2[48 /*0x30*/] = (byte) 172;
      numArray2[34] = (byte) 50;
      numArray2[42] = (byte) 36;
      numArray2[51] = (byte) 236;
      numArray2[52] = (byte) 29;
      numArray2[37] = (byte) 161;
      numArray2[54] = (byte) 101;
      byte[] numArray3 = new byte[55]
      {
        (byte) 3,
        (byte) 84,
        (byte) 75,
        (byte) 120,
        (byte) 186,
        (byte) 165,
        (byte) 5,
        (byte) 33,
        (byte) 121,
        (byte) 240 /*0xF0*/,
        (byte) 192 /*0xC0*/,
        (byte) 70,
        (byte) 72,
        (byte) 183,
        (byte) 113,
        (byte) 189,
        (byte) 190,
        (byte) 114,
        (byte) 66,
        (byte) 245,
        (byte) 17,
        (byte) 30,
        (byte) 143,
        (byte) 173,
        (byte) 171,
        (byte) 51,
        (byte) 227,
        (byte) 225,
        (byte) 143,
        (byte) 137,
        (byte) 231,
        (byte) 191,
        (byte) 163,
        (byte) 43,
        (byte) 125,
        (byte) 151,
        (byte) 120,
        (byte) 42,
        (byte) 142,
        (byte) 189,
        (byte) 40,
        (byte) 88,
        (byte) 194,
        (byte) 149,
        (byte) 47,
        (byte) 136,
        (byte) 231,
        (byte) 175,
        (byte) 37,
        (byte) 131,
        (byte) 120,
        (byte) 45,
        (byte) 98,
        (byte) 115,
        (byte) 154
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[36] = (byte) 103;
      numArray4[1] = (byte) 177;
      numArray4[27] = (byte) 24;
      numArray4[3] = (byte) 77;
      numArray4[0] = (byte) 115;
      numArray4[5] = (byte) 103;
      numArray4[6] = (byte) 191;
      numArray4[26] = (byte) 92;
      numArray4[8] = (byte) 105;
      numArray4[9] = (byte) 63 /*0x3F*/;
      numArray4[10] = (byte) 229;
      numArray4[53] = (byte) 235;
      numArray4[12] = (byte) 199;
      numArray4[49] = (byte) 239;
      numArray4[18] = (byte) 181;
      numArray4[7] = (byte) 174;
      numArray4[42] = (byte) 157;
      numArray4[17] = (byte) 46;
      numArray4[20] = (byte) 173;
      numArray4[19] = (byte) 77;
      numArray4[51] = (byte) 220;
      numArray4[15] = byte.MaxValue;
      numArray4[22] = (byte) 33;
      numArray4[34] = (byte) 207;
      numArray4[41] = (byte) 233;
      numArray4[25] = (byte) 226;
      numArray4[13] = (byte) 64 /*0x40*/;
      numArray4[14] = (byte) 114;
      numArray4[24] = (byte) 93;
      numArray4[29] = (byte) 1;
      numArray4[30] = (byte) 45;
      numArray4[31 /*0x1F*/] = (byte) 153;
      numArray4[40] = (byte) 130;
      numArray4[21] = (byte) 95;
      numArray4[16 /*0x10*/] = (byte) 226;
      numArray4[35] = (byte) 14;
      numArray4[11] = (byte) 232;
      numArray4[39] = (byte) 128 /*0x80*/;
      numArray4[38] = (byte) 36;
      numArray4[28] = (byte) 224 /*0xE0*/;
      numArray4[33] = (byte) 59;
      numArray4[44] = (byte) 10;
      numArray4[4] = (byte) 36;
      numArray4[43] = (byte) 207;
      numArray4[23] = (byte) 155;
      numArray4[32 /*0x20*/] = (byte) 48 /*0x30*/;
      numArray4[46] = (byte) 248;
      numArray4[47] = (byte) 18;
      numArray4[48 /*0x30*/] = (byte) 150;
      numArray4[37] = (byte) 249;
      numArray4[50] = (byte) 18;
      numArray4[2] = (byte) 149;
      numArray4[52] = (byte) 18;
      numArray4[45] = (byte) 138;
      numArray4[54] = (byte) 60;
      byte[] numArray5 = new byte[55]
      {
        (byte) 183,
        (byte) 254,
        (byte) 133,
        (byte) 109,
        (byte) 113,
        (byte) 130,
        (byte) 26,
        (byte) 145,
        (byte) 55,
        (byte) 107,
        (byte) 110,
        (byte) 158,
        (byte) 76,
        (byte) 174,
        (byte) 132,
        (byte) 74,
        (byte) 239,
        (byte) 64 /*0x40*/,
        (byte) 240 /*0xF0*/,
        (byte) 56,
        (byte) 207,
        (byte) 230,
        (byte) 14,
        (byte) 192 /*0xC0*/,
        (byte) 83,
        (byte) 56,
        (byte) 22,
        (byte) 106,
        (byte) 160 /*0xA0*/,
        (byte) 71,
        (byte) 65,
        (byte) 67,
        (byte) 94,
        (byte) 173,
        (byte) 86,
        (byte) 136,
        (byte) 71,
        (byte) 76,
        (byte) 121,
        (byte) 201,
        (byte) 169,
        (byte) 9,
        (byte) 156,
        (byte) 64 /*0x40*/,
        (byte) 98,
        (byte) 211,
        (byte) 159,
        (byte) 111,
        (byte) 101,
        (byte) 209,
        (byte) 203,
        (byte) 165,
        (byte) 162,
        (byte) 14,
        (byte) 178
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[21] = (byte) 210;
      numArray6[1] = (byte) 30;
      numArray6[2] = (byte) 89;
      numArray6[3] = (byte) 193;
      numArray6[29] = (byte) 169;
      numArray6[5] = (byte) 221;
      numArray6[54] = (byte) 169;
      numArray6[4] = (byte) 37;
      numArray6[6] = (byte) 119;
      numArray6[50] = (byte) 30;
      numArray6[49] = (byte) 171;
      numArray6[9] = (byte) 115;
      numArray6[12] = (byte) 229;
      numArray6[27] = (byte) 199;
      numArray6[14] = (byte) 224 /*0xE0*/;
      numArray6[18] = (byte) 169;
      numArray6[16 /*0x10*/] = (byte) 191;
      numArray6[17] = (byte) 239;
      numArray6[37] = (byte) 180;
      numArray6[19] = (byte) 140;
      numArray6[20] = (byte) 147;
      numArray6[10] = (byte) 141;
      numArray6[40] = (byte) 189;
      numArray6[52] = (byte) 207;
      numArray6[48 /*0x30*/] = (byte) 72;
      numArray6[32 /*0x20*/] = (byte) 106;
      numArray6[26] = (byte) 152;
      numArray6[11] = (byte) 181;
      numArray6[28] = (byte) 127 /*0x7F*/;
      numArray6[46] = (byte) 75;
      numArray6[34] = (byte) 3;
      numArray6[41] = byte.MaxValue;
      numArray6[13] = (byte) 43;
      numArray6[30] = (byte) 35;
      numArray6[42] = (byte) 248;
      numArray6[35] = (byte) 234;
      numArray6[47] = (byte) 44;
      numArray6[23] = (byte) 124;
      numArray6[38] = (byte) 181;
      numArray6[39] = (byte) 61;
      numArray6[8] = (byte) 197;
      numArray6[51] = (byte) 50;
      numArray6[15] = (byte) 39;
      numArray6[33] = (byte) 65;
      numArray6[44] = (byte) 223;
      numArray6[43] = (byte) 95;
      numArray6[25] = (byte) 79;
      numArray6[22] = byte.MaxValue;
      numArray6[24] = (byte) 168;
      numArray6[45] = (byte) 236;
      numArray6[31 /*0x1F*/] = (byte) 134;
      numArray6[0] = (byte) 105;
      numArray6[36] = (byte) 136;
      numArray6[53] = (byte) 196;
      numArray6[7] = (byte) 191;
      byte[] numArray7 = new byte[55]
      {
        (byte) 83,
        (byte) 117,
        (byte) 37,
        (byte) 149,
        (byte) 23,
        (byte) 215,
        (byte) 48 /*0x30*/,
        (byte) 99,
        (byte) 248,
        (byte) 175,
        (byte) 130,
        (byte) 99,
        (byte) 184,
        (byte) 178,
        (byte) 190,
        (byte) 159,
        (byte) 234,
        (byte) 55,
        (byte) 157,
        (byte) 207,
        (byte) 1,
        (byte) 134,
        (byte) 168,
        (byte) 73,
        (byte) 241,
        (byte) 78,
        (byte) 104,
        (byte) 227,
        (byte) 68,
        (byte) 117,
        (byte) 142,
        (byte) 148,
        (byte) 10,
        (byte) 192 /*0xC0*/,
        (byte) 232,
        (byte) 233,
        (byte) 204,
        (byte) 216,
        (byte) 239,
        (byte) 176 /*0xB0*/,
        (byte) 57,
        (byte) 82,
        (byte) 78,
        (byte) 72,
        (byte) 3,
        (byte) 203,
        (byte) 101,
        (byte) 242,
        (byte) 195,
        (byte) 223,
        (byte) 207,
        (byte) 30,
        (byte) 232,
        (byte) 40,
        (byte) 251
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 64 /*0x40*/,
        (byte) 93,
        (byte) 210,
        (byte) 215,
        (byte) 49,
        (byte) 108,
        (byte) 145,
        (byte) 94,
        (byte) 80 /*0x50*/,
        (byte) 166,
        (byte) 11,
        (byte) 188,
        (byte) 41,
        (byte) 243,
        (byte) 111,
        (byte) 89,
        (byte) 113,
        (byte) 193,
        (byte) 213,
        (byte) 254,
        (byte) 143,
        (byte) 196,
        (byte) 27,
        (byte) 91,
        (byte) 104,
        (byte) 44,
        (byte) 199,
        (byte) 119,
        (byte) 248,
        (byte) 82,
        (byte) 174,
        (byte) 131,
        (byte) 173,
        (byte) 76,
        (byte) 107,
        (byte) 62,
        (byte) 13,
        (byte) 33,
        (byte) 219,
        (byte) 82,
        (byte) 73,
        (byte) 178,
        (byte) 136,
        (byte) 250,
        (byte) 156,
        (byte) 72,
        (byte) 40,
        (byte) 24,
        (byte) 164,
        (byte) 159,
        (byte) 242,
        (byte) 52,
        (byte) 63 /*0x3F*/,
        (byte) 191,
        (byte) 239
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 10,
        (byte) 122,
        (byte) 238,
        (byte) 196,
        (byte) 127 /*0x7F*/,
        (byte) 36,
        (byte) 72,
        (byte) 45,
        (byte) 203,
        (byte) 112 /*0x70*/,
        (byte) 47,
        (byte) 136,
        (byte) 33,
        (byte) 11,
        (byte) 187,
        (byte) 20,
        (byte) 170,
        (byte) 25,
        (byte) 226,
        (byte) 89,
        (byte) 8,
        (byte) 48 /*0x30*/,
        (byte) 157,
        (byte) 118,
        (byte) 17,
        (byte) 54,
        (byte) 141,
        (byte) 53,
        (byte) 117,
        (byte) 171,
        (byte) 162,
        (byte) 205,
        (byte) 219,
        (byte) 150,
        (byte) 119,
        (byte) 118,
        (byte) 97,
        (byte) 157,
        (byte) 212,
        (byte) 226,
        (byte) 75,
        (byte) 90,
        (byte) 9,
        (byte) 135,
        (byte) 30,
        (byte) 210,
        (byte) 118,
        (byte) 195,
        (byte) 100,
        (byte) 72,
        (byte) 250,
        (byte) 88,
        (byte) 185,
        (byte) 215,
        (byte) 240 /*0xF0*/
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[34]
      {
        (byte) 118,
        (byte) 111,
        (byte) 5,
        (byte) 164,
        (byte) 26,
        (byte) 101,
        (byte) 253,
        (byte) 247,
        (byte) 136,
        (byte) 14,
        (byte) 220,
        (byte) 136,
        (byte) 49,
        (byte) 75,
        (byte) 80 /*0x50*/,
        (byte) 230,
        (byte) 131,
        (byte) 180,
        (byte) 221,
        (byte) 59,
        (byte) 32 /*0x20*/,
        (byte) 191,
        (byte) 31 /*0x1F*/,
        (byte) 3,
        (byte) 174,
        (byte) 211,
        (byte) 89,
        (byte) 96 /*0x60*/,
        (byte) 122,
        (byte) 67,
        (byte) 238,
        (byte) 93,
        (byte) 238,
        (byte) 252
      };
      byte[] numArray11 = new byte[34];
      numArray11[11] = (byte) 228;
      numArray11[20] = (byte) 202;
      numArray11[2] = (byte) 89;
      numArray11[3] = (byte) 20;
      numArray11[18] = (byte) 165;
      numArray11[26] = (byte) 118;
      numArray11[14] = (byte) 30;
      numArray11[7] = (byte) 168;
      numArray11[15] = (byte) 180;
      numArray11[9] = (byte) 253;
      numArray11[10] = (byte) 26;
      numArray11[23] = (byte) 10;
      numArray11[29] = (byte) 198;
      numArray11[13] = (byte) 200;
      numArray11[32 /*0x20*/] = (byte) 10;
      numArray11[16 /*0x10*/] = (byte) 96 /*0x60*/;
      numArray11[30] = (byte) 240 /*0xF0*/;
      numArray11[6] = (byte) 86;
      numArray11[28] = (byte) 251;
      numArray11[19] = (byte) 159;
      numArray11[17] = (byte) 71;
      numArray11[21] = (byte) 117;
      numArray11[22] = (byte) 89;
      numArray11[0] = (byte) 64 /*0x40*/;
      numArray11[24] = (byte) 58;
      numArray11[25] = (byte) 242;
      numArray11[8] = (byte) 236;
      numArray11[27] = (byte) 12;
      numArray11[1] = (byte) 90;
      numArray11[31 /*0x1F*/] = (byte) 0;
      numArray11[4] = (byte) 63 /*0x3F*/;
      numArray11[12] = (byte) 152;
      numArray11[5] = (byte) 157;
      numArray11[33] = (byte) 100;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[254];
    byte[] numArray13 = new byte[55]
    {
      (byte) 214,
      (byte) 133,
      (byte) 145,
      (byte) 176 /*0xB0*/,
      (byte) 29,
      (byte) 171,
      (byte) 228,
      (byte) 183,
      (byte) 30,
      (byte) 96 /*0x60*/,
      (byte) 5,
      (byte) 189,
      (byte) 43,
      (byte) 136,
      (byte) 228,
      (byte) 75,
      (byte) 173,
      (byte) 218,
      (byte) 224 /*0xE0*/,
      (byte) 97,
      (byte) 211,
      (byte) 183,
      (byte) 231,
      (byte) 63 /*0x3F*/,
      (byte) 237,
      (byte) 134,
      (byte) 207,
      (byte) 27,
      (byte) 204,
      (byte) 246,
      (byte) 61,
      (byte) 159,
      (byte) 4,
      (byte) 62,
      (byte) 228,
      (byte) 132,
      (byte) 128 /*0x80*/,
      (byte) 144 /*0x90*/,
      (byte) 4,
      (byte) 11,
      (byte) 107,
      (byte) 75,
      (byte) 215,
      (byte) 230,
      (byte) 7,
      (byte) 223,
      (byte) 81,
      (byte) 188,
      (byte) 59,
      (byte) 52,
      (byte) 236,
      (byte) 90,
      (byte) 212,
      (byte) 60,
      (byte) 60
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 81,
      (byte) 224 /*0xE0*/,
      (byte) 60,
      (byte) 244,
      (byte) 112 /*0x70*/,
      (byte) 231,
      (byte) 101,
      (byte) 254,
      (byte) 208 /*0xD0*/,
      (byte) 198,
      (byte) 206,
      (byte) 71,
      (byte) 122,
      (byte) 93,
      (byte) 76,
      (byte) 188,
      (byte) 154,
      (byte) 219,
      (byte) 254,
      (byte) 154,
      (byte) 234,
      (byte) 217,
      (byte) 72,
      (byte) 172,
      (byte) 40,
      (byte) 6,
      (byte) 176 /*0xB0*/,
      (byte) 57,
      (byte) 32 /*0x20*/,
      (byte) 210,
      (byte) 206,
      (byte) 28,
      (byte) 244,
      (byte) 152,
      (byte) 32 /*0x20*/,
      (byte) 152,
      (byte) 175,
      (byte) 114,
      (byte) 216,
      (byte) 223,
      (byte) 245,
      (byte) 98,
      (byte) 75,
      (byte) 12,
      (byte) 198,
      (byte) 63 /*0x3F*/,
      (byte) 232,
      (byte) 10,
      (byte) 142,
      (byte) 38,
      (byte) 133,
      (byte) 190,
      (byte) 254,
      (byte) 182,
      (byte) 169
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 81,
      (byte) 195,
      (byte) 150,
      (byte) 220,
      (byte) 201,
      (byte) 234,
      (byte) 40,
      (byte) 34,
      (byte) 102,
      (byte) 185,
      (byte) 237,
      (byte) 163,
      (byte) 95,
      (byte) 202,
      (byte) 29,
      (byte) 214,
      (byte) 213,
      (byte) 245,
      (byte) 136,
      (byte) 224 /*0xE0*/,
      (byte) 1,
      (byte) 56,
      (byte) 13,
      (byte) 53,
      (byte) 19,
      (byte) 223,
      (byte) 152,
      (byte) 10,
      (byte) 226,
      (byte) 203,
      (byte) 199,
      (byte) 1,
      (byte) 197,
      (byte) 230,
      (byte) 47,
      (byte) 205,
      (byte) 110,
      (byte) 47,
      (byte) 161,
      (byte) 96 /*0x60*/,
      (byte) 174,
      (byte) 195,
      (byte) 160 /*0xA0*/,
      (byte) 38,
      (byte) 99,
      (byte) 24,
      (byte) 219,
      (byte) 106,
      (byte) 206,
      (byte) 132,
      byte.MaxValue,
      (byte) 70,
      (byte) 180,
      (byte) 63 /*0x3F*/,
      (byte) 157
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 17,
      (byte) 6,
      (byte) 12,
      (byte) 109,
      (byte) 159,
      (byte) 118,
      (byte) 250,
      (byte) 59,
      (byte) 24,
      (byte) 14,
      (byte) 178,
      (byte) 19,
      (byte) 150,
      (byte) 185,
      (byte) 246,
      (byte) 229,
      (byte) 155,
      (byte) 197,
      (byte) 56,
      (byte) 49,
      (byte) 65,
      (byte) 73,
      (byte) 122,
      (byte) 129,
      (byte) 47,
      (byte) 45,
      (byte) 213,
      (byte) 172,
      (byte) 27,
      (byte) 145,
      (byte) 101,
      (byte) 220,
      (byte) 243,
      (byte) 23,
      (byte) 34,
      (byte) 150,
      (byte) 79,
      (byte) 190,
      (byte) 12,
      (byte) 195,
      (byte) 49,
      (byte) 222,
      (byte) 15,
      (byte) 246,
      (byte) 244,
      (byte) 238,
      (byte) 66,
      (byte) 136,
      (byte) 227,
      (byte) 201,
      (byte) 76,
      (byte) 178,
      (byte) 243,
      (byte) 217,
      (byte) 235
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[49] = (byte) 242;
    numArray17[2] = (byte) 234;
    numArray17[9] = (byte) 222;
    numArray17[38] = (byte) 144 /*0x90*/;
    numArray17[4] = (byte) 30;
    numArray17[5] = (byte) 63 /*0x3F*/;
    numArray17[43] = (byte) 58;
    numArray17[42] = (byte) 119;
    numArray17[8] = (byte) 235;
    numArray17[18] = (byte) 61;
    numArray17[30] = (byte) 134;
    numArray17[19] = (byte) 151;
    numArray17[12] = (byte) 80 /*0x50*/;
    numArray17[32 /*0x20*/] = (byte) 23;
    numArray17[14] = (byte) 165;
    numArray17[15] = (byte) 84;
    numArray17[3] = (byte) 82;
    numArray17[17] = (byte) 49;
    numArray17[31 /*0x1F*/] = (byte) 73;
    numArray17[21] = (byte) 209;
    numArray17[20] = (byte) 231;
    numArray17[52] = (byte) 95;
    numArray17[22] = (byte) 250;
    numArray17[23] = (byte) 3;
    numArray17[48 /*0x30*/] = (byte) 89;
    numArray17[11] = (byte) 154;
    numArray17[16 /*0x10*/] = (byte) 39;
    numArray17[26] = (byte) 94;
    numArray17[50] = (byte) 141;
    numArray17[10] = (byte) 121;
    numArray17[29] = (byte) 76;
    numArray17[34] = (byte) 12;
    numArray17[47] = (byte) 28;
    numArray17[13] = (byte) 103;
    numArray17[37] = (byte) 14;
    numArray17[35] = (byte) 102;
    numArray17[39] = (byte) 131;
    numArray17[28] = (byte) 128 /*0x80*/;
    numArray17[25] = (byte) 59;
    numArray17[41] = (byte) 52;
    numArray17[40] = (byte) 5;
    numArray17[1] = (byte) 200;
    numArray17[36] = (byte) 183;
    numArray17[0] = (byte) 97;
    numArray17[44] = (byte) 98;
    numArray17[45] = (byte) 207;
    numArray17[33] = (byte) 23;
    numArray17[24] = (byte) 46;
    numArray17[27] = (byte) 204;
    numArray17[7] = (byte) 241;
    numArray17[6] = (byte) 155;
    numArray17[51] = (byte) 14;
    numArray17[46] = (byte) 126;
    numArray17[53] = (byte) 128 /*0x80*/;
    numArray17[54] = (byte) 205;
    byte[] numArray18 = new byte[55]
    {
      (byte) 62,
      (byte) 135,
      (byte) 32 /*0x20*/,
      (byte) 65,
      (byte) 177,
      (byte) 79,
      (byte) 94,
      (byte) 55,
      (byte) 74,
      (byte) 221,
      (byte) 88,
      (byte) 106,
      (byte) 202,
      (byte) 242,
      (byte) 198,
      (byte) 212,
      (byte) 129,
      (byte) 1,
      (byte) 37,
      (byte) 26,
      (byte) 245,
      (byte) 36,
      (byte) 163,
      (byte) 221,
      (byte) 209,
      (byte) 254,
      (byte) 162,
      (byte) 46,
      (byte) 134,
      (byte) 52,
      (byte) 186,
      (byte) 143,
      (byte) 111,
      (byte) 110,
      (byte) 71,
      (byte) 210,
      (byte) 198,
      (byte) 210,
      (byte) 8,
      (byte) 29,
      (byte) 159,
      (byte) 168,
      (byte) 97,
      (byte) 100,
      (byte) 40,
      (byte) 148,
      (byte) 63 /*0x3F*/,
      (byte) 25,
      (byte) 228,
      (byte) 5,
      (byte) 202,
      (byte) 60,
      (byte) 141,
      (byte) 143,
      (byte) 230
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 68,
      (byte) 197,
      (byte) 36,
      (byte) 159,
      (byte) 92,
      (byte) 226,
      (byte) 223,
      (byte) 223,
      (byte) 70,
      (byte) 235,
      (byte) 238,
      (byte) 222,
      (byte) 166,
      (byte) 203,
      (byte) 46,
      (byte) 242,
      (byte) 23,
      (byte) 91,
      (byte) 239,
      (byte) 128 /*0x80*/,
      (byte) 221,
      (byte) 110,
      (byte) 17,
      (byte) 120,
      (byte) 128 /*0x80*/,
      (byte) 88,
      (byte) 56,
      (byte) 75,
      (byte) 10,
      (byte) 66,
      (byte) 168,
      (byte) 47,
      (byte) 70,
      (byte) 139,
      (byte) 140,
      (byte) 37,
      (byte) 137,
      (byte) 2,
      (byte) 218,
      (byte) 225,
      (byte) 249,
      (byte) 2,
      (byte) 236,
      (byte) 97,
      (byte) 86,
      (byte) 227,
      (byte) 115,
      (byte) 138,
      (byte) 69,
      (byte) 123,
      (byte) 142,
      (byte) 146,
      (byte) 43,
      (byte) 181,
      (byte) 47
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 120,
      (byte) 134,
      (byte) 139,
      (byte) 231,
      (byte) 57,
      (byte) 35,
      (byte) 148,
      (byte) 21,
      (byte) 128 /*0x80*/,
      (byte) 61,
      (byte) 128 /*0x80*/,
      (byte) 43,
      (byte) 11,
      (byte) 207,
      (byte) 155,
      (byte) 248,
      (byte) 229,
      (byte) 133,
      (byte) 10,
      (byte) 47,
      (byte) 112 /*0x70*/,
      (byte) 36,
      (byte) 53,
      (byte) 177,
      (byte) 167,
      (byte) 163,
      (byte) 143,
      (byte) 110,
      (byte) 187,
      (byte) 135,
      (byte) 75,
      (byte) 200,
      (byte) 21,
      (byte) 210,
      (byte) 102,
      (byte) 143,
      (byte) 1,
      (byte) 166,
      (byte) 77,
      (byte) 186,
      (byte) 217,
      (byte) 25,
      (byte) 98,
      (byte) 40,
      (byte) 228,
      (byte) 168,
      (byte) 243,
      (byte) 115,
      (byte) 169,
      (byte) 193,
      (byte) 245,
      (byte) 157,
      (byte) 34,
      (byte) 211,
      (byte) 109
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[34];
    numArray21[12] = (byte) 9;
    numArray21[21] = (byte) 33;
    numArray21[2] = (byte) 3;
    numArray21[31 /*0x1F*/] = (byte) 91;
    numArray21[4] = (byte) 147;
    numArray21[0] = (byte) 172;
    numArray21[33] = (byte) 113;
    numArray21[7] = (byte) 66;
    numArray21[22] = (byte) 157;
    numArray21[16 /*0x10*/] = (byte) 13;
    numArray21[10] = (byte) 27;
    numArray21[13] = (byte) 105;
    numArray21[3] = (byte) 169;
    numArray21[20] = (byte) 176 /*0xB0*/;
    numArray21[14] = (byte) 20;
    numArray21[25] = (byte) 6;
    numArray21[24] = (byte) 174;
    numArray21[17] = (byte) 135;
    numArray21[18] = (byte) 218;
    numArray21[19] = (byte) 130;
    numArray21[6] = (byte) 3;
    numArray21[5] = (byte) 213;
    numArray21[15] = (byte) 129;
    numArray21[23] = (byte) 229;
    numArray21[1] = (byte) 31 /*0x1F*/;
    numArray21[27] = (byte) 179;
    numArray21[26] = (byte) 143;
    numArray21[32 /*0x20*/] = (byte) 182;
    numArray21[28] = (byte) 22;
    numArray21[29] = (byte) 3;
    numArray21[30] = (byte) 144 /*0x90*/;
    numArray21[9] = (byte) 151;
    numArray21[11] = (byte) 62;
    numArray21[8] = (byte) 191;
    byte[] numArray22 = new byte[34];
    numArray22[30] = (byte) 57;
    numArray22[25] = (byte) 226;
    numArray22[21] = (byte) 101;
    numArray22[3] = (byte) 169;
    numArray22[4] = (byte) 49;
    numArray22[5] = (byte) 168;
    numArray22[6] = (byte) 75;
    numArray22[29] = (byte) 246;
    numArray22[12] = (byte) 100;
    numArray22[7] = (byte) 85;
    numArray22[10] = (byte) 95;
    numArray22[11] = (byte) 136;
    numArray22[13] = (byte) 97;
    numArray22[33] = (byte) 227;
    numArray22[14] = (byte) 234;
    numArray22[0] = (byte) 223;
    numArray22[16 /*0x10*/] = (byte) 85;
    numArray22[31 /*0x1F*/] = (byte) 37;
    numArray22[18] = (byte) 87;
    numArray22[19] = (byte) 57;
    numArray22[1] = (byte) 28;
    numArray22[23] = (byte) 236;
    numArray22[22] = (byte) 4;
    numArray22[20] = (byte) 86;
    numArray22[24] = (byte) 60;
    numArray22[8] = (byte) 189;
    numArray22[26] = (byte) 216;
    numArray22[27] = (byte) 228;
    numArray22[28] = (byte) 4;
    numArray22[2] = (byte) 74;
    numArray22[9] = (byte) 41;
    numArray22[15] = (byte) 76;
    numArray22[32 /*0x20*/] = (byte) 157;
    numArray22[17] = (byte) 139;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 34);
    for (int index = 0; index < 34; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[51];
    byte[] response = new byte[51];
    Array.Copy((Array) sc_13393.sspq, 1001, (Array) numArray23, 0, 51);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13393.sspr, 1001, (Array) numArray23, 0, 51);
    for (int index = 0; index < numArray23.Length; ++index)
    {
      if ((int) numArray23[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray12);
  }
}
