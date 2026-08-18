// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13578
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13578
{
  private static byte[] sspq = new byte[528]
  {
    (byte) 113,
    (byte) 50,
    (byte) 200,
    (byte) 81,
    (byte) 203,
    (byte) 124,
    (byte) 151,
    (byte) 171,
    (byte) 209,
    (byte) 25,
    (byte) 190,
    (byte) 23,
    (byte) 9,
    (byte) 107,
    (byte) 48 /*0x30*/,
    (byte) 75,
    (byte) 150,
    (byte) 172,
    (byte) 128 /*0x80*/,
    (byte) 195,
    (byte) 155,
    (byte) 99,
    (byte) 16 /*0x10*/,
    (byte) 147,
    (byte) 254,
    (byte) 117,
    (byte) 112 /*0x70*/,
    (byte) 105,
    (byte) 37,
    (byte) 83,
    (byte) 77,
    (byte) 116,
    (byte) 168,
    (byte) 51,
    (byte) 47,
    (byte) 30,
    (byte) 25,
    (byte) 61,
    (byte) 227,
    (byte) 54,
    (byte) 155,
    (byte) 141,
    (byte) 213,
    (byte) 197,
    (byte) 7,
    (byte) 58,
    (byte) 196,
    (byte) 20,
    (byte) 128 /*0x80*/,
    (byte) 34,
    (byte) 179,
    (byte) 4,
    (byte) 72,
    (byte) 30,
    (byte) 159,
    (byte) 29,
    (byte) 153,
    (byte) 6,
    (byte) 242,
    (byte) 135,
    (byte) 236,
    (byte) 75,
    (byte) 104,
    (byte) 163,
    byte.MaxValue,
    (byte) 247,
    (byte) 21,
    (byte) 76,
    (byte) 10,
    (byte) 252,
    (byte) 25,
    (byte) 57,
    (byte) 249,
    (byte) 17,
    (byte) 212,
    (byte) 89,
    (byte) 97,
    (byte) 219,
    (byte) 18,
    (byte) 36,
    (byte) 179,
    (byte) 1,
    (byte) 39,
    (byte) 208 /*0xD0*/,
    (byte) 31 /*0x1F*/,
    (byte) 78,
    (byte) 209,
    (byte) 80 /*0x50*/,
    (byte) 161,
    (byte) 32 /*0x20*/,
    (byte) 246,
    (byte) 193,
    (byte) 239,
    (byte) 135,
    (byte) 71,
    (byte) 191,
    (byte) 226,
    (byte) 22,
    (byte) 57,
    (byte) 115,
    (byte) 205,
    (byte) 213,
    (byte) 89,
    (byte) 231,
    (byte) 204,
    (byte) 91,
    (byte) 181,
    (byte) 94,
    (byte) 41,
    (byte) 188,
    (byte) 232,
    (byte) 202,
    (byte) 63 /*0x3F*/,
    (byte) 110,
    (byte) 74,
    (byte) 77,
    (byte) 64 /*0x40*/,
    (byte) 72,
    (byte) 96 /*0x60*/,
    (byte) 236,
    (byte) 138,
    (byte) 15,
    (byte) 105,
    (byte) 84,
    (byte) 179,
    (byte) 23,
    (byte) 227,
    (byte) 31 /*0x1F*/,
    (byte) 130,
    (byte) 237,
    (byte) 218,
    (byte) 120,
    (byte) 27,
    (byte) 155,
    (byte) 152,
    (byte) 65,
    (byte) 18,
    (byte) 62,
    (byte) 111,
    (byte) 40,
    (byte) 127 /*0x7F*/,
    (byte) 183,
    (byte) 214,
    (byte) 141,
    (byte) 21,
    (byte) 202,
    (byte) 218,
    (byte) 128 /*0x80*/,
    (byte) 224 /*0xE0*/,
    (byte) 161,
    (byte) 55,
    (byte) 150,
    (byte) 99,
    (byte) 247,
    (byte) 235,
    (byte) 252,
    (byte) 55,
    (byte) 198,
    (byte) 234,
    (byte) 152,
    (byte) 41,
    (byte) 71,
    (byte) 200,
    (byte) 23,
    (byte) 28,
    (byte) 57,
    (byte) 48 /*0x30*/,
    (byte) 127 /*0x7F*/,
    (byte) 80 /*0x50*/,
    (byte) 7,
    (byte) 110,
    (byte) 91,
    (byte) 200,
    (byte) 67,
    (byte) 94,
    (byte) 184,
    (byte) 215,
    (byte) 250,
    (byte) 159,
    (byte) 70,
    (byte) 107,
    (byte) 84,
    (byte) 184,
    (byte) 211,
    (byte) 109,
    (byte) 140,
    (byte) 232,
    (byte) 38,
    (byte) 11,
    (byte) 115,
    (byte) 111,
    (byte) 49,
    (byte) 125,
    (byte) 236,
    (byte) 94,
    (byte) 105,
    (byte) 140,
    (byte) 53,
    (byte) 204,
    (byte) 167,
    (byte) 89,
    (byte) 189,
    (byte) 24,
    (byte) 158,
    (byte) 181,
    (byte) 179,
    (byte) 4,
    (byte) 42,
    (byte) 217,
    (byte) 101,
    (byte) 251,
    (byte) 188,
    (byte) 192 /*0xC0*/,
    (byte) 95,
    (byte) 29,
    (byte) 13,
    (byte) 124,
    (byte) 140,
    (byte) 123,
    (byte) 151,
    (byte) 196,
    (byte) 37,
    (byte) 160 /*0xA0*/,
    (byte) 54,
    (byte) 210,
    (byte) 214,
    (byte) 142,
    (byte) 228,
    (byte) 173,
    (byte) 114,
    (byte) 178,
    (byte) 226,
    (byte) 117,
    (byte) 32 /*0x20*/,
    (byte) 200,
    (byte) 190,
    (byte) 140,
    (byte) 64 /*0x40*/,
    (byte) 116,
    (byte) 39,
    (byte) 101,
    (byte) 246,
    (byte) 147,
    (byte) 234,
    (byte) 96 /*0x60*/,
    (byte) 160 /*0xA0*/,
    (byte) 157,
    (byte) 74,
    (byte) 211,
    (byte) 95,
    (byte) 199,
    (byte) 206,
    (byte) 58,
    (byte) 69,
    (byte) 251,
    (byte) 140,
    (byte) 245,
    (byte) 186,
    (byte) 124,
    (byte) 120,
    (byte) 106,
    (byte) 186,
    (byte) 110,
    (byte) 172,
    (byte) 232,
    (byte) 5,
    (byte) 164,
    (byte) 52,
    (byte) 111,
    (byte) 118,
    (byte) 9,
    (byte) 207,
    (byte) 167,
    (byte) 187,
    (byte) 39,
    (byte) 118,
    (byte) 74,
    (byte) 173,
    (byte) 12,
    (byte) 231,
    (byte) 250,
    (byte) 192 /*0xC0*/,
    (byte) 75,
    (byte) 127 /*0x7F*/,
    (byte) 203,
    (byte) 129,
    (byte) 133,
    (byte) 223,
    (byte) 165,
    (byte) 156,
    (byte) 87,
    (byte) 129,
    (byte) 38,
    (byte) 170,
    (byte) 115,
    (byte) 176 /*0xB0*/,
    (byte) 102,
    (byte) 213,
    (byte) 70,
    (byte) 212,
    (byte) 41,
    (byte) 1,
    (byte) 151,
    (byte) 171,
    (byte) 119,
    (byte) 15,
    (byte) 116,
    (byte) 103,
    (byte) 55,
    (byte) 189,
    (byte) 248,
    (byte) 41,
    (byte) 160 /*0xA0*/,
    (byte) 76,
    (byte) 47,
    (byte) 88,
    (byte) 188,
    (byte) 138,
    (byte) 108,
    (byte) 139,
    (byte) 53,
    (byte) 144 /*0x90*/,
    (byte) 84,
    (byte) 193,
    (byte) 27,
    (byte) 219,
    (byte) 106,
    (byte) 74,
    (byte) 227,
    (byte) 125,
    (byte) 111,
    (byte) 28,
    (byte) 164,
    (byte) 247,
    (byte) 10,
    (byte) 46,
    (byte) 22,
    (byte) 176 /*0xB0*/,
    (byte) 221,
    (byte) 42,
    (byte) 169,
    (byte) 48 /*0x30*/,
    (byte) 52,
    (byte) 149,
    (byte) 50,
    (byte) 74,
    (byte) 192 /*0xC0*/,
    (byte) 93,
    (byte) 124,
    (byte) 52,
    (byte) 202,
    (byte) 133,
    (byte) 16 /*0x10*/,
    (byte) 196,
    (byte) 97,
    (byte) 177,
    (byte) 198,
    (byte) 52,
    (byte) 69,
    (byte) 226,
    (byte) 27,
    (byte) 79,
    (byte) 120,
    (byte) 147,
    (byte) 71,
    (byte) 159,
    (byte) 102,
    (byte) 7,
    (byte) 22,
    (byte) 76,
    (byte) 107,
    (byte) 254,
    (byte) 154,
    (byte) 78,
    (byte) 53,
    (byte) 241,
    (byte) 168,
    (byte) 51,
    (byte) 18,
    byte.MaxValue,
    (byte) 211,
    (byte) 66,
    (byte) 11,
    (byte) 249,
    (byte) 210,
    (byte) 83,
    (byte) 23,
    (byte) 63 /*0x3F*/,
    (byte) 4,
    (byte) 78,
    (byte) 62,
    (byte) 107,
    (byte) 153,
    (byte) 187,
    (byte) 103,
    (byte) 152,
    (byte) 122,
    (byte) 178,
    (byte) 120,
    (byte) 28,
    (byte) 220,
    (byte) 175,
    (byte) 113,
    (byte) 103,
    (byte) 244,
    (byte) 132,
    (byte) 196,
    (byte) 68,
    (byte) 184,
    (byte) 74,
    (byte) 7,
    (byte) 46,
    (byte) 16 /*0x10*/,
    (byte) 203,
    (byte) 99,
    (byte) 14,
    (byte) 212,
    (byte) 63 /*0x3F*/,
    (byte) 169,
    (byte) 228,
    (byte) 238,
    (byte) 127 /*0x7F*/,
    (byte) 205,
    (byte) 130,
    (byte) 45,
    (byte) 90,
    (byte) 159,
    (byte) 117,
    (byte) 52,
    (byte) 217,
    (byte) 107,
    (byte) 171,
    (byte) 78,
    (byte) 103,
    (byte) 119,
    (byte) 139,
    (byte) 137,
    (byte) 237,
    (byte) 77,
    (byte) 198,
    (byte) 252,
    (byte) 174,
    (byte) 76,
    (byte) 83,
    (byte) 246,
    (byte) 98,
    (byte) 192 /*0xC0*/,
    (byte) 227,
    (byte) 109,
    (byte) 70,
    (byte) 34,
    (byte) 0,
    (byte) 193,
    (byte) 246,
    (byte) 174,
    (byte) 94,
    (byte) 20,
    (byte) 111,
    (byte) 96 /*0x60*/,
    (byte) 130,
    (byte) 147,
    (byte) 194,
    (byte) 49,
    (byte) 165,
    (byte) 218,
    (byte) 183,
    (byte) 234,
    (byte) 209,
    (byte) 58,
    (byte) 7,
    (byte) 196,
    (byte) 148,
    (byte) 226,
    (byte) 141,
    (byte) 55,
    (byte) 188,
    (byte) 120,
    (byte) 34,
    (byte) 232,
    (byte) 249,
    (byte) 67,
    (byte) 63 /*0x3F*/,
    (byte) 246,
    (byte) 17,
    (byte) 130,
    (byte) 40,
    (byte) 153,
    (byte) 23,
    (byte) 185,
    (byte) 33,
    (byte) 200,
    (byte) 118,
    (byte) 229,
    (byte) 232,
    (byte) 75,
    (byte) 247,
    (byte) 193,
    (byte) 5,
    (byte) 152,
    (byte) 150,
    (byte) 97,
    (byte) 44,
    (byte) 175,
    (byte) 96 /*0x60*/,
    (byte) 199,
    (byte) 215,
    (byte) 56,
    (byte) 20,
    (byte) 135,
    (byte) 222,
    (byte) 3,
    (byte) 142,
    (byte) 14,
    (byte) 161,
    (byte) 196,
    (byte) 105,
    (byte) 138,
    (byte) 27,
    (byte) 100,
    (byte) 249,
    (byte) 20,
    (byte) 7,
    (byte) 165,
    (byte) 155,
    (byte) 5,
    (byte) 82,
    (byte) 240 /*0xF0*/,
    (byte) 145
  };
  private static byte[] sspr = new byte[528]
  {
    (byte) 236,
    (byte) 99,
    (byte) 31 /*0x1F*/,
    (byte) 239,
    (byte) 16 /*0x10*/,
    (byte) 61,
    (byte) 89,
    (byte) 217,
    (byte) 76,
    (byte) 44,
    (byte) 112 /*0x70*/,
    (byte) 226,
    (byte) 138,
    (byte) 187,
    (byte) 229,
    (byte) 222,
    (byte) 199,
    (byte) 57,
    (byte) 61,
    (byte) 57,
    (byte) 126,
    (byte) 59,
    (byte) 168,
    (byte) 237,
    (byte) 34,
    (byte) 247,
    (byte) 135,
    (byte) 44,
    (byte) 184,
    (byte) 144 /*0x90*/,
    (byte) 59,
    (byte) 75,
    (byte) 185,
    (byte) 161,
    (byte) 99,
    (byte) 156,
    (byte) 211,
    (byte) 62,
    (byte) 217,
    (byte) 178,
    (byte) 146,
    (byte) 95,
    (byte) 240 /*0xF0*/,
    (byte) 80 /*0x50*/,
    (byte) 232,
    (byte) 105,
    (byte) 84,
    (byte) 71,
    (byte) 162,
    (byte) 143,
    (byte) 138,
    (byte) 192 /*0xC0*/,
    (byte) 132,
    (byte) 220,
    (byte) 225,
    (byte) 25,
    (byte) 207,
    (byte) 144 /*0x90*/,
    (byte) 36,
    (byte) 73,
    (byte) 87,
    (byte) 222,
    (byte) 34,
    (byte) 207,
    (byte) 231,
    (byte) 50,
    (byte) 26,
    (byte) 214,
    (byte) 105,
    (byte) 186,
    (byte) 94,
    (byte) 100,
    (byte) 169,
    (byte) 102,
    (byte) 138,
    (byte) 140,
    (byte) 229,
    (byte) 94,
    (byte) 83,
    (byte) 136,
    (byte) 254,
    (byte) 102,
    (byte) 236,
    (byte) 9,
    (byte) 104,
    (byte) 100,
    (byte) 54,
    (byte) 48 /*0x30*/,
    (byte) 154,
    (byte) 119,
    (byte) 207,
    (byte) 214,
    (byte) 44,
    (byte) 37,
    (byte) 233,
    (byte) 93,
    (byte) 198,
    (byte) 184,
    (byte) 58,
    (byte) 187,
    (byte) 154,
    (byte) 134,
    (byte) 28,
    (byte) 13,
    (byte) 188,
    (byte) 113,
    (byte) 196,
    (byte) 173,
    (byte) 89,
    (byte) 145,
    (byte) 173,
    (byte) 80 /*0x50*/,
    (byte) 215,
    (byte) 137,
    (byte) 12,
    (byte) 213,
    (byte) 147,
    (byte) 147,
    (byte) 30,
    (byte) 186,
    (byte) 66,
    (byte) 19,
    (byte) 58,
    (byte) 206,
    (byte) 231,
    (byte) 201,
    (byte) 200,
    (byte) 224 /*0xE0*/,
    (byte) 49,
    (byte) 0,
    (byte) 166,
    (byte) 211,
    (byte) 134,
    (byte) 251,
    (byte) 171,
    (byte) 197,
    (byte) 111,
    (byte) 150,
    (byte) 190,
    (byte) 128 /*0x80*/,
    (byte) 46,
    (byte) 104,
    (byte) 24,
    (byte) 122,
    (byte) 214,
    (byte) 196,
    (byte) 191,
    (byte) 152,
    (byte) 113,
    (byte) 38,
    (byte) 242,
    (byte) 246,
    (byte) 165,
    (byte) 31 /*0x1F*/,
    (byte) 164,
    (byte) 135,
    (byte) 37,
    (byte) 43,
    (byte) 228,
    (byte) 187,
    (byte) 50,
    (byte) 6,
    (byte) 208 /*0xD0*/,
    (byte) 160 /*0xA0*/,
    (byte) 75,
    (byte) 142,
    (byte) 107,
    (byte) 186,
    (byte) 31 /*0x1F*/,
    (byte) 40,
    (byte) 173,
    (byte) 98,
    (byte) 245,
    (byte) 173,
    (byte) 112 /*0x70*/,
    (byte) 200,
    (byte) 146,
    (byte) 166,
    (byte) 150,
    (byte) 224 /*0xE0*/,
    (byte) 73,
    (byte) 166,
    (byte) 111,
    (byte) 120,
    (byte) 216,
    (byte) 172,
    (byte) 52,
    (byte) 51,
    (byte) 241,
    (byte) 138,
    (byte) 161,
    (byte) 23,
    (byte) 108,
    (byte) 5,
    (byte) 197,
    (byte) 152,
    (byte) 122,
    (byte) 81,
    (byte) 64 /*0x40*/,
    (byte) 52,
    (byte) 249,
    (byte) 144 /*0x90*/,
    (byte) 246,
    (byte) 233,
    (byte) 160 /*0xA0*/,
    (byte) 171,
    (byte) 94,
    (byte) 11,
    (byte) 177,
    (byte) 198,
    (byte) 189,
    (byte) 120,
    (byte) 20,
    (byte) 14,
    (byte) 44,
    (byte) 137,
    (byte) 108,
    (byte) 191,
    (byte) 73,
    (byte) 29,
    (byte) 147,
    (byte) 21,
    (byte) 27,
    (byte) 79,
    (byte) 63 /*0x3F*/,
    (byte) 93,
    (byte) 211,
    (byte) 236,
    (byte) 136,
    (byte) 184,
    (byte) 245,
    (byte) 49,
    (byte) 85,
    (byte) 92,
    (byte) 4,
    (byte) 10,
    (byte) 161,
    (byte) 202,
    (byte) 203,
    (byte) 231,
    (byte) 112 /*0x70*/,
    (byte) 205,
    (byte) 68,
    (byte) 19,
    (byte) 76,
    (byte) 5,
    (byte) 7,
    (byte) 110,
    byte.MaxValue,
    (byte) 137,
    (byte) 1,
    (byte) 80 /*0x50*/,
    (byte) 158,
    (byte) 131,
    (byte) 216,
    (byte) 194,
    (byte) 41,
    (byte) 235,
    (byte) 141,
    (byte) 35,
    (byte) 47,
    (byte) 167,
    (byte) 247,
    (byte) 154,
    (byte) 3,
    (byte) 93,
    (byte) 205,
    (byte) 221,
    (byte) 100,
    (byte) 175,
    (byte) 80 /*0x50*/,
    (byte) 29,
    (byte) 125,
    (byte) 155,
    (byte) 149,
    (byte) 19,
    (byte) 211,
    (byte) 115,
    (byte) 24,
    (byte) 120,
    (byte) 167,
    (byte) 143,
    (byte) 201,
    (byte) 126,
    (byte) 9,
    (byte) 164,
    (byte) 174,
    (byte) 126,
    (byte) 161,
    (byte) 11,
    (byte) 87,
    (byte) 238,
    (byte) 115,
    (byte) 181,
    (byte) 70,
    (byte) 86,
    (byte) 84,
    (byte) 155,
    (byte) 70,
    (byte) 1,
    (byte) 47,
    (byte) 11,
    (byte) 235,
    (byte) 230,
    (byte) 252,
    (byte) 125,
    (byte) 159,
    (byte) 191,
    (byte) 21,
    (byte) 169,
    (byte) 136,
    (byte) 91,
    (byte) 84,
    (byte) 102,
    (byte) 190,
    (byte) 12,
    (byte) 99,
    (byte) 42,
    (byte) 200,
    (byte) 100,
    (byte) 224 /*0xE0*/,
    (byte) 43,
    (byte) 152,
    (byte) 74,
    (byte) 91,
    (byte) 27,
    (byte) 184,
    (byte) 118,
    (byte) 30,
    (byte) 233,
    (byte) 27,
    (byte) 24,
    (byte) 32 /*0x20*/,
    (byte) 234,
    (byte) 143,
    (byte) 74,
    (byte) 55,
    (byte) 223,
    (byte) 81,
    (byte) 142,
    (byte) 74,
    (byte) 22,
    (byte) 49,
    (byte) 246,
    (byte) 73,
    (byte) 138,
    (byte) 173,
    (byte) 182,
    (byte) 127 /*0x7F*/,
    (byte) 19,
    (byte) 38,
    (byte) 128 /*0x80*/,
    (byte) 2,
    (byte) 135,
    (byte) 122,
    (byte) 186,
    (byte) 119,
    (byte) 78,
    (byte) 54,
    (byte) 81,
    (byte) 209,
    (byte) 208 /*0xD0*/,
    (byte) 35,
    (byte) 3,
    (byte) 87,
    (byte) 108,
    (byte) 158,
    (byte) 26,
    (byte) 146,
    (byte) 147,
    (byte) 208 /*0xD0*/,
    (byte) 112 /*0x70*/,
    (byte) 252,
    (byte) 201,
    (byte) 106,
    (byte) 84,
    (byte) 115,
    (byte) 225,
    (byte) 119,
    (byte) 85,
    (byte) 197,
    (byte) 192 /*0xC0*/,
    (byte) 32 /*0x20*/,
    (byte) 191,
    (byte) 18,
    (byte) 28,
    (byte) 32 /*0x20*/,
    (byte) 196,
    (byte) 165,
    (byte) 239,
    (byte) 173,
    (byte) 38,
    (byte) 169,
    (byte) 160 /*0xA0*/,
    (byte) 115,
    (byte) 227,
    (byte) 190,
    (byte) 230,
    (byte) 44,
    (byte) 78,
    (byte) 48 /*0x30*/,
    (byte) 157,
    (byte) 9,
    (byte) 56,
    (byte) 178,
    (byte) 166,
    (byte) 3,
    (byte) 187,
    (byte) 70,
    (byte) 178,
    (byte) 115,
    (byte) 200,
    (byte) 240 /*0xF0*/,
    (byte) 192 /*0xC0*/,
    (byte) 134,
    (byte) 222,
    (byte) 34,
    (byte) 194,
    (byte) 147,
    (byte) 131,
    (byte) 251,
    (byte) 44,
    (byte) 28,
    (byte) 178,
    (byte) 17,
    (byte) 204,
    (byte) 178,
    (byte) 84,
    (byte) 204,
    (byte) 129,
    (byte) 229,
    (byte) 72,
    (byte) 245,
    (byte) 131,
    (byte) 49,
    (byte) 213,
    (byte) 90,
    (byte) 247,
    (byte) 230,
    (byte) 231,
    (byte) 250,
    (byte) 106,
    (byte) 192 /*0xC0*/,
    (byte) 50,
    (byte) 86,
    (byte) 38,
    (byte) 83,
    (byte) 81,
    (byte) 18,
    (byte) 69,
    (byte) 104,
    (byte) 26,
    (byte) 171,
    (byte) 112 /*0x70*/,
    (byte) 174,
    (byte) 63 /*0x3F*/,
    (byte) 218,
    (byte) 251,
    (byte) 233,
    (byte) 134,
    (byte) 111,
    (byte) 166,
    (byte) 227,
    (byte) 244,
    (byte) 154,
    (byte) 89,
    (byte) 98,
    (byte) 133,
    (byte) 27,
    (byte) 216,
    (byte) 101,
    (byte) 109,
    (byte) 148,
    (byte) 11,
    (byte) 141,
    (byte) 210,
    (byte) 185,
    (byte) 114,
    (byte) 91,
    (byte) 217,
    (byte) 75,
    (byte) 149,
    (byte) 212,
    (byte) 116,
    (byte) 171,
    (byte) 16 /*0x10*/,
    (byte) 73,
    (byte) 155,
    (byte) 159,
    (byte) 236,
    (byte) 57,
    (byte) 253,
    (byte) 53,
    (byte) 180,
    (byte) 119,
    (byte) 163,
    (byte) 90,
    (byte) 42,
    (byte) 151,
    (byte) 168,
    (byte) 120,
    (byte) 179,
    (byte) 174,
    (byte) 79,
    (byte) 159,
    (byte) 131,
    (byte) 80 /*0x50*/,
    (byte) 9,
    (byte) 160 /*0xA0*/,
    (byte) 176 /*0xB0*/,
    (byte) 210,
    (byte) 103,
    (byte) 188,
    (byte) 233,
    (byte) 0,
    (byte) 218,
    (byte) 81,
    (byte) 101,
    (byte) 152,
    (byte) 143,
    (byte) 112 /*0x70*/,
    (byte) 49,
    (byte) 21,
    (byte) 133,
    (byte) 124,
    (byte) 168,
    (byte) 209,
    (byte) 238
  };

  internal static int ssp_appserver_13579(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 28;
    sourceArray1[5] = (byte) 98;
    sourceArray1[44] = (byte) 175;
    sourceArray1[46] = (byte) 98;
    sourceArray1[26] = (byte) 148;
    sourceArray1[20] = (byte) 202;
    sourceArray1[1] = (byte) 216;
    sourceArray1[14] = (byte) 188;
    sourceArray1[8] = (byte) 238;
    sourceArray1[9] = (byte) 50;
    sourceArray1[40] = (byte) 163;
    sourceArray1[10] = (byte) 225;
    sourceArray1[31 /*0x1F*/] = (byte) 155;
    sourceArray1[24] = (byte) 22;
    sourceArray1[28] = (byte) 102;
    sourceArray1[15] = (byte) 204;
    sourceArray1[35] = (byte) 203;
    sourceArray1[38] = (byte) 207;
    sourceArray1[18] = (byte) 175;
    sourceArray1[19] = (byte) 82;
    sourceArray1[7] = (byte) 5;
    sourceArray1[21] = (byte) 193;
    sourceArray1[22] = (byte) 68;
    sourceArray1[43] = (byte) 71;
    sourceArray1[11] = (byte) 247;
    sourceArray1[4] = (byte) 83;
    sourceArray1[13] = (byte) 48 /*0x30*/;
    sourceArray1[27] = (byte) 31 /*0x1F*/;
    sourceArray1[29] = (byte) 216;
    sourceArray1[17] = (byte) 22;
    sourceArray1[30] = (byte) 65;
    sourceArray1[6] = (byte) 64 /*0x40*/;
    sourceArray1[33] = (byte) 87;
    sourceArray1[25] = (byte) 140;
    sourceArray1[34] = (byte) 162;
    sourceArray1[0] = (byte) 100;
    sourceArray1[36] = (byte) 178;
    sourceArray1[37] = (byte) 100;
    sourceArray1[32 /*0x20*/] = (byte) 158;
    sourceArray1[39] = (byte) 216;
    sourceArray1[16 /*0x10*/] = (byte) 230;
    sourceArray1[2] = (byte) 140;
    sourceArray1[42] = (byte) 119;
    sourceArray1[3] = (byte) 220;
    sourceArray1[47] = (byte) 167;
    sourceArray1[45] = (byte) 221;
    sourceArray1[41] = (byte) 117;
    sourceArray1[23] = (byte) 181;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 48 /*0x30*/,
      (byte) 229,
      (byte) 223,
      (byte) 92,
      (byte) 251,
      (byte) 211,
      (byte) 189,
      (byte) 30,
      (byte) 187,
      (byte) 28,
      (byte) 73,
      (byte) 56,
      (byte) 138,
      (byte) 40,
      (byte) 240 /*0xF0*/,
      (byte) 123,
      (byte) 46,
      (byte) 150,
      (byte) 166,
      (byte) 122,
      (byte) 115,
      (byte) 220,
      (byte) 178,
      (byte) 24,
      (byte) 177,
      (byte) 206,
      (byte) 94,
      (byte) 8,
      (byte) 211,
      (byte) 120,
      (byte) 8,
      (byte) 54,
      (byte) 227,
      (byte) 87,
      (byte) 5,
      (byte) 114,
      (byte) 90,
      (byte) 193,
      (byte) 206,
      (byte) 92,
      (byte) 154,
      (byte) 155,
      (byte) 11,
      (byte) 222,
      (byte) 88,
      (byte) 41,
      (byte) 189,
      (byte) 121
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13580()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[55];
      byte[] numArray2 = new byte[55]
      {
        (byte) 132,
        (byte) 157,
        (byte) 25,
        (byte) 119,
        (byte) 46,
        (byte) 24,
        (byte) 10,
        (byte) 15,
        (byte) 57,
        (byte) 122,
        (byte) 88,
        (byte) 150,
        (byte) 209,
        (byte) 140,
        (byte) 156,
        (byte) 224 /*0xE0*/,
        (byte) 104,
        (byte) 128 /*0x80*/,
        (byte) 216,
        (byte) 1,
        (byte) 145,
        (byte) 162,
        (byte) 61,
        (byte) 142,
        (byte) 184,
        (byte) 63 /*0x3F*/,
        (byte) 227,
        (byte) 62,
        (byte) 204,
        (byte) 43,
        (byte) 126,
        (byte) 232,
        (byte) 166,
        (byte) 156,
        (byte) 180,
        (byte) 141,
        (byte) 211,
        (byte) 64 /*0x40*/,
        (byte) 239,
        (byte) 36,
        (byte) 229,
        (byte) 166,
        (byte) 125,
        (byte) 12,
        (byte) 167,
        (byte) 161,
        (byte) 9,
        (byte) 18,
        (byte) 108,
        (byte) 42,
        (byte) 26,
        (byte) 86,
        (byte) 136,
        (byte) 85,
        (byte) 215
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 227,
        (byte) 154,
        (byte) 111,
        (byte) 130,
        (byte) 37,
        (byte) 77,
        (byte) 135,
        (byte) 74,
        (byte) 47,
        (byte) 196,
        (byte) 41,
        (byte) 240 /*0xF0*/,
        (byte) 21,
        (byte) 169,
        (byte) 41,
        (byte) 32 /*0x20*/,
        (byte) 86,
        (byte) 215,
        (byte) 23,
        (byte) 129,
        (byte) 96 /*0x60*/,
        (byte) 144 /*0x90*/,
        (byte) 101,
        (byte) 171,
        (byte) 192 /*0xC0*/,
        (byte) 127 /*0x7F*/,
        (byte) 179,
        (byte) 247,
        (byte) 37,
        (byte) 196,
        (byte) 144 /*0x90*/,
        (byte) 103,
        (byte) 213,
        (byte) 246,
        (byte) 58,
        (byte) 47,
        (byte) 206,
        (byte) 172,
        (byte) 239,
        (byte) 22,
        (byte) 198,
        (byte) 124,
        (byte) 131,
        (byte) 206,
        (byte) 89,
        (byte) 224 /*0xE0*/,
        (byte) 252,
        (byte) 23,
        (byte) 155,
        (byte) 210,
        (byte) 253,
        (byte) 57,
        (byte) 140,
        (byte) 126,
        (byte) 112 /*0x70*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[55];
    byte[] numArray5 = new byte[55];
    numArray5[23] = (byte) 208 /*0xD0*/;
    numArray5[45] = (byte) 93;
    numArray5[2] = (byte) 53;
    numArray5[27] = (byte) 39;
    numArray5[4] = (byte) 66;
    numArray5[44] = (byte) 59;
    numArray5[13] = (byte) 40;
    numArray5[7] = (byte) 112 /*0x70*/;
    numArray5[29] = (byte) 80 /*0x50*/;
    numArray5[9] = (byte) 53;
    numArray5[22] = (byte) 53;
    numArray5[10] = (byte) 215;
    numArray5[42] = (byte) 178;
    numArray5[18] = (byte) 36;
    numArray5[14] = (byte) 144 /*0x90*/;
    numArray5[35] = (byte) 0;
    numArray5[16 /*0x10*/] = (byte) 27;
    numArray5[37] = (byte) 25;
    numArray5[51] = (byte) 236;
    numArray5[19] = (byte) 48 /*0x30*/;
    numArray5[12] = (byte) 13;
    numArray5[21] = (byte) 22;
    numArray5[17] = (byte) 235;
    numArray5[1] = (byte) 239;
    numArray5[24] = (byte) 209;
    numArray5[25] = (byte) 161;
    numArray5[26] = (byte) 17;
    numArray5[5] = (byte) 13;
    numArray5[3] = (byte) 204;
    numArray5[0] = (byte) 190;
    numArray5[15] = (byte) 179;
    numArray5[31 /*0x1F*/] = (byte) 210;
    numArray5[32 /*0x20*/] = (byte) 45;
    numArray5[33] = (byte) 71;
    numArray5[8] = (byte) 239;
    numArray5[48 /*0x30*/] = (byte) 198;
    numArray5[36] = (byte) 238;
    numArray5[11] = (byte) 14;
    numArray5[38] = (byte) 99;
    numArray5[46] = (byte) 29;
    numArray5[40] = (byte) 16 /*0x10*/;
    numArray5[41] = (byte) 55;
    numArray5[39] = (byte) 112 /*0x70*/;
    numArray5[34] = (byte) 129;
    numArray5[28] = (byte) 95;
    numArray5[47] = (byte) 241;
    numArray5[53] = (byte) 46;
    numArray5[20] = (byte) 231;
    numArray5[30] = (byte) 196;
    numArray5[49] = (byte) 212;
    numArray5[50] = (byte) 57;
    numArray5[54] = (byte) 164;
    numArray5[52] = (byte) 189;
    numArray5[43] = (byte) 212;
    numArray5[6] = (byte) 252;
    byte[] numArray6 = new byte[55];
    numArray6[39] = (byte) 111;
    numArray6[1] = (byte) 204;
    numArray6[53] = (byte) 61;
    numArray6[30] = (byte) 133;
    numArray6[4] = (byte) 27;
    numArray6[14] = (byte) 137;
    numArray6[24] = (byte) 237;
    numArray6[33] = (byte) 57;
    numArray6[54] = (byte) 72;
    numArray6[9] = (byte) 213;
    numArray6[18] = (byte) 173;
    numArray6[11] = (byte) 205;
    numArray6[12] = (byte) 113;
    numArray6[37] = (byte) 171;
    numArray6[0] = (byte) 39;
    numArray6[15] = (byte) 196;
    numArray6[44] = (byte) 175;
    numArray6[17] = (byte) 224 /*0xE0*/;
    numArray6[35] = (byte) 2;
    numArray6[19] = (byte) 170;
    numArray6[20] = (byte) 87;
    numArray6[21] = (byte) 254;
    numArray6[2] = (byte) 205;
    numArray6[25] = (byte) 26;
    numArray6[6] = (byte) 115;
    numArray6[8] = (byte) 88;
    numArray6[26] = (byte) 236;
    numArray6[29] = (byte) 97;
    numArray6[48 /*0x30*/] = (byte) 16 /*0x10*/;
    numArray6[28] = (byte) 179;
    numArray6[10] = (byte) 130;
    numArray6[43] = (byte) 194;
    numArray6[32 /*0x20*/] = (byte) 119;
    numArray6[16 /*0x10*/] = (byte) 80 /*0x50*/;
    numArray6[5] = (byte) 154;
    numArray6[3] = (byte) 131;
    numArray6[23] = (byte) 153;
    numArray6[13] = (byte) 54;
    numArray6[38] = (byte) 89;
    numArray6[27] = (byte) 172;
    numArray6[40] = (byte) 214;
    numArray6[22] = (byte) 225;
    numArray6[41] = (byte) 87;
    numArray6[47] = (byte) 70;
    numArray6[31 /*0x1F*/] = (byte) 75;
    numArray6[36] = (byte) 210;
    numArray6[46] = (byte) 80 /*0x50*/;
    numArray6[7] = (byte) 106;
    numArray6[45] = (byte) 145;
    numArray6[49] = (byte) 249;
    numArray6[42] = (byte) 213;
    numArray6[51] = (byte) 142;
    numArray6[52] = (byte) 32 /*0x20*/;
    numArray6[50] = (byte) 143;
    numArray6[34] = (byte) 146;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13581()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 238,
        (byte) 6,
        (byte) 73,
        (byte) 10,
        (byte) 168,
        (byte) 38,
        (byte) 225,
        (byte) 242,
        (byte) 113,
        (byte) 57,
        (byte) 89,
        (byte) 129,
        (byte) 77,
        (byte) 31 /*0x1F*/,
        (byte) 167,
        (byte) 30,
        (byte) 232,
        (byte) 197,
        (byte) 51,
        (byte) 22,
        (byte) 92
      };
      byte[] numArray3 = new byte[21];
      numArray3[4] = (byte) 107;
      numArray3[12] = (byte) 67;
      numArray3[13] = (byte) 205;
      numArray3[3] = (byte) 223;
      numArray3[8] = (byte) 89;
      numArray3[5] = (byte) 120;
      numArray3[6] = (byte) 87;
      numArray3[10] = (byte) 113;
      numArray3[14] = (byte) 196;
      numArray3[0] = (byte) 16 /*0x10*/;
      numArray3[17] = (byte) 189;
      numArray3[15] = (byte) 175;
      numArray3[2] = (byte) 158;
      numArray3[1] = (byte) 162;
      numArray3[11] = (byte) 185;
      numArray3[9] = (byte) 129;
      numArray3[16 /*0x10*/] = (byte) 200;
      numArray3[18] = (byte) 131;
      numArray3[19] = (byte) 199;
      numArray3[7] = (byte) 209;
      numArray3[20] = (byte) 63 /*0x3F*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 82,
      (byte) 5,
      (byte) 127 /*0x7F*/,
      (byte) 31 /*0x1F*/,
      (byte) 11,
      (byte) 130,
      (byte) 46,
      (byte) 38,
      (byte) 91,
      (byte) 52,
      (byte) 247,
      (byte) 175,
      (byte) 12,
      (byte) 99,
      (byte) 27,
      (byte) 82,
      (byte) 155,
      (byte) 43,
      (byte) 44,
      (byte) 152,
      (byte) 195
    };
    byte[] numArray6 = new byte[21];
    numArray6[14] = (byte) 208 /*0xD0*/;
    numArray6[19] = (byte) 173;
    numArray6[2] = (byte) 233;
    numArray6[3] = (byte) 210;
    numArray6[4] = (byte) 43;
    numArray6[5] = (byte) 74;
    numArray6[0] = (byte) 177;
    numArray6[12] = (byte) 115;
    numArray6[8] = (byte) 43;
    numArray6[15] = (byte) 178;
    numArray6[9] = (byte) 202;
    numArray6[11] = (byte) 219;
    numArray6[10] = (byte) 212;
    numArray6[13] = (byte) 177;
    numArray6[16 /*0x10*/] = (byte) 199;
    numArray6[7] = (byte) 235;
    numArray6[20] = (byte) 194;
    numArray6[17] = (byte) 69;
    numArray6[6] = (byte) 79;
    numArray6[18] = (byte) 212;
    numArray6[1] = (byte) 147;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_13578.sspq, 0, (Array) numArray7, 0, 41);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13578.sspr, 0, (Array) numArray7, 0, 41);
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

  internal static string ssp_appserver_13582()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 214,
        (byte) 58,
        (byte) 200,
        (byte) 61,
        (byte) 27,
        (byte) 79,
        (byte) 146,
        (byte) 39,
        (byte) 216,
        (byte) 63 /*0x3F*/
      };
      byte[] numArray3 = new byte[10];
      numArray3[2] = (byte) 42;
      numArray3[1] = (byte) 58;
      numArray3[7] = (byte) 157;
      numArray3[3] = (byte) 183;
      numArray3[8] = (byte) 46;
      numArray3[5] = (byte) 193;
      numArray3[6] = (byte) 146;
      numArray3[0] = (byte) 35;
      numArray3[4] = (byte) 196;
      numArray3[9] = (byte) 176 /*0xB0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 74,
      (byte) 110,
      (byte) 214,
      (byte) 59,
      (byte) 17,
      (byte) 50,
      (byte) 89,
      (byte) 201,
      (byte) 73,
      (byte) 39
    };
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 96 /*0x60*/;
    numArray6[1] = (byte) 224 /*0xE0*/;
    numArray6[0] = (byte) 50;
    numArray6[3] = (byte) 195;
    numArray6[8] = (byte) 243;
    numArray6[5] = (byte) 179;
    numArray6[6] = (byte) 128 /*0x80*/;
    numArray6[7] = (byte) 225;
    numArray6[4] = (byte) 222;
    numArray6[9] = (byte) 129;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13583()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50];
      numArray2[12] = (byte) 69;
      numArray2[33] = (byte) 234;
      numArray2[11] = (byte) 206;
      numArray2[18] = (byte) 8;
      numArray2[4] = (byte) 140;
      numArray2[29] = (byte) 3;
      numArray2[3] = (byte) 233;
      numArray2[0] = (byte) 33;
      numArray2[8] = (byte) 88;
      numArray2[1] = (byte) 252;
      numArray2[10] = (byte) 126;
      numArray2[27] = (byte) 87;
      numArray2[49] = (byte) 9;
      numArray2[15] = (byte) 244;
      numArray2[14] = (byte) 128 /*0x80*/;
      numArray2[43] = (byte) 104;
      numArray2[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray2[17] = (byte) 118;
      numArray2[7] = (byte) 226;
      numArray2[37] = (byte) 111;
      numArray2[13] = (byte) 20;
      numArray2[22] = (byte) 76;
      numArray2[35] = (byte) 21;
      numArray2[23] = (byte) 105;
      numArray2[32 /*0x20*/] = (byte) 194;
      numArray2[48 /*0x30*/] = (byte) 86;
      numArray2[26] = (byte) 235;
      numArray2[5] = (byte) 42;
      numArray2[31 /*0x1F*/] = (byte) 40;
      numArray2[2] = (byte) 229;
      numArray2[21] = (byte) 128 /*0x80*/;
      numArray2[9] = (byte) 109;
      numArray2[25] = (byte) 253;
      numArray2[20] = (byte) 21;
      numArray2[46] = (byte) 20;
      numArray2[6] = (byte) 109;
      numArray2[28] = (byte) 135;
      numArray2[30] = (byte) 191;
      numArray2[38] = (byte) 60;
      numArray2[39] = (byte) 30;
      numArray2[40] = (byte) 186;
      numArray2[41] = (byte) 79;
      numArray2[42] = (byte) 52;
      numArray2[34] = (byte) 25;
      numArray2[44] = (byte) 76;
      numArray2[45] = (byte) 242;
      numArray2[19] = (byte) 211;
      numArray2[47] = (byte) 12;
      numArray2[24] = (byte) 146;
      numArray2[36] = (byte) 77;
      byte[] numArray3 = new byte[50];
      numArray3[41] = (byte) 198;
      numArray3[1] = (byte) 25;
      numArray3[32 /*0x20*/] = (byte) 158;
      numArray3[11] = (byte) 53;
      numArray3[46] = (byte) 45;
      numArray3[2] = (byte) 132;
      numArray3[4] = (byte) 43;
      numArray3[7] = (byte) 207;
      numArray3[8] = (byte) 18;
      numArray3[12] = (byte) 53;
      numArray3[42] = (byte) 247;
      numArray3[28] = (byte) 157;
      numArray3[22] = (byte) 12;
      numArray3[18] = (byte) 123;
      numArray3[30] = (byte) 246;
      numArray3[15] = (byte) 139;
      numArray3[16 /*0x10*/] = (byte) 247;
      numArray3[6] = (byte) 5;
      numArray3[17] = (byte) 197;
      numArray3[43] = (byte) 237;
      numArray3[20] = (byte) 165;
      numArray3[21] = (byte) 74;
      numArray3[0] = (byte) 135;
      numArray3[48 /*0x30*/] = (byte) 188;
      numArray3[33] = (byte) 51;
      numArray3[27] = (byte) 109;
      numArray3[3] = (byte) 201;
      numArray3[19] = (byte) 90;
      numArray3[25] = (byte) 210;
      numArray3[29] = (byte) 59;
      numArray3[14] = (byte) 224 /*0xE0*/;
      numArray3[31 /*0x1F*/] = (byte) 34;
      numArray3[47] = (byte) 238;
      numArray3[44] = (byte) 161;
      numArray3[34] = (byte) 27;
      numArray3[35] = (byte) 152;
      numArray3[36] = (byte) 64 /*0x40*/;
      numArray3[23] = (byte) 130;
      numArray3[38] = (byte) 170;
      numArray3[39] = (byte) 45;
      numArray3[40] = (byte) 15;
      numArray3[26] = (byte) 61;
      numArray3[49] = (byte) 242;
      numArray3[37] = (byte) 88;
      numArray3[24] = (byte) 233;
      numArray3[45] = (byte) 19;
      numArray3[5] = (byte) 31 /*0x1F*/;
      numArray3[10] = (byte) 137;
      numArray3[9] = (byte) 80 /*0x50*/;
      numArray3[13] = (byte) 60;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50]
    {
      (byte) 50,
      (byte) 176 /*0xB0*/,
      (byte) 195,
      (byte) 115,
      (byte) 144 /*0x90*/,
      (byte) 231,
      (byte) 139,
      (byte) 243,
      (byte) 209,
      (byte) 36,
      (byte) 72,
      (byte) 64 /*0x40*/,
      (byte) 231,
      (byte) 159,
      (byte) 56,
      (byte) 251,
      (byte) 119,
      (byte) 149,
      (byte) 1,
      (byte) 192 /*0xC0*/,
      (byte) 45,
      (byte) 188,
      (byte) 102,
      (byte) 248,
      (byte) 66,
      (byte) 93,
      (byte) 104,
      (byte) 98,
      (byte) 148,
      (byte) 133,
      (byte) 141,
      (byte) 232,
      (byte) 183,
      (byte) 218,
      (byte) 248,
      (byte) 117,
      (byte) 247,
      (byte) 187,
      (byte) 58,
      (byte) 44,
      (byte) 144 /*0x90*/,
      (byte) 42,
      (byte) 248,
      (byte) 187,
      (byte) 225,
      (byte) 195,
      (byte) 60,
      (byte) 252,
      (byte) 58,
      (byte) 102
    };
    byte[] numArray6 = new byte[50];
    numArray6[34] = (byte) 190;
    numArray6[31 /*0x1F*/] = (byte) 195;
    numArray6[18] = (byte) 74;
    numArray6[11] = (byte) 14;
    numArray6[46] = (byte) 139;
    numArray6[5] = (byte) 51;
    numArray6[36] = (byte) 52;
    numArray6[14] = (byte) 91;
    numArray6[47] = (byte) 115;
    numArray6[45] = (byte) 222;
    numArray6[2] = (byte) 74;
    numArray6[43] = (byte) 204;
    numArray6[12] = (byte) 248;
    numArray6[38] = (byte) 49;
    numArray6[16 /*0x10*/] = (byte) 82;
    numArray6[15] = (byte) 9;
    numArray6[25] = (byte) 11;
    numArray6[17] = (byte) 163;
    numArray6[37] = (byte) 109;
    numArray6[19] = (byte) 206;
    numArray6[32 /*0x20*/] = (byte) 113;
    numArray6[21] = (byte) 54;
    numArray6[22] = (byte) 167;
    numArray6[30] = (byte) 247;
    numArray6[48 /*0x30*/] = (byte) 122;
    numArray6[13] = (byte) 218;
    numArray6[26] = (byte) 83;
    numArray6[27] = (byte) 101;
    numArray6[28] = (byte) 74;
    numArray6[29] = (byte) 122;
    numArray6[24] = (byte) 203;
    numArray6[20] = (byte) 244;
    numArray6[9] = (byte) 192 /*0xC0*/;
    numArray6[44] = (byte) 110;
    numArray6[41] = (byte) 198;
    numArray6[35] = (byte) 189;
    numArray6[40] = (byte) 207;
    numArray6[8] = (byte) 119;
    numArray6[10] = (byte) 116;
    numArray6[39] = (byte) 239;
    numArray6[33] = (byte) 102;
    numArray6[3] = (byte) 213;
    numArray6[42] = (byte) 106;
    numArray6[6] = (byte) 125;
    numArray6[1] = (byte) 34;
    numArray6[23] = (byte) 18;
    numArray6[4] = (byte) 177;
    numArray6[7] = (byte) 163;
    numArray6[0] = (byte) 161;
    numArray6[49] = (byte) 174;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13584()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 97,
        (byte) 211,
        (byte) 142,
        (byte) 183,
        (byte) 41,
        (byte) 98,
        (byte) 178,
        (byte) 159,
        (byte) 180,
        (byte) 178,
        (byte) 18,
        (byte) 18,
        (byte) 1,
        (byte) 49,
        (byte) 86,
        (byte) 242,
        (byte) 3,
        (byte) 41,
        (byte) 152,
        (byte) 36,
        (byte) 151
      };
      byte[] numArray3 = new byte[21];
      numArray3[8] = (byte) 66;
      numArray3[17] = (byte) 89;
      numArray3[5] = (byte) 72;
      numArray3[18] = (byte) 243;
      numArray3[4] = (byte) 123;
      numArray3[1] = (byte) 181;
      numArray3[20] = (byte) 143;
      numArray3[7] = (byte) 145;
      numArray3[6] = (byte) 246;
      numArray3[0] = (byte) 101;
      numArray3[10] = (byte) 157;
      numArray3[2] = (byte) 45;
      numArray3[12] = (byte) 48 /*0x30*/;
      numArray3[13] = (byte) 171;
      numArray3[14] = (byte) 171;
      numArray3[15] = (byte) 236;
      numArray3[16 /*0x10*/] = (byte) 97;
      numArray3[11] = (byte) 193;
      numArray3[19] = (byte) 166;
      numArray3[9] = (byte) 71;
      numArray3[3] = (byte) 68;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 154,
      (byte) 102,
      (byte) 154,
      (byte) 84,
      (byte) 49,
      (byte) 125,
      (byte) 16 /*0x10*/,
      (byte) 150,
      (byte) 48 /*0x30*/,
      (byte) 223,
      (byte) 137,
      (byte) 161,
      (byte) 60,
      (byte) 144 /*0x90*/,
      (byte) 97,
      (byte) 42,
      (byte) 153,
      (byte) 25,
      (byte) 30,
      (byte) 110,
      (byte) 32 /*0x20*/
    };
    byte[] numArray6 = new byte[21];
    numArray6[7] = (byte) 104;
    numArray6[1] = (byte) 157;
    numArray6[0] = (byte) 124;
    numArray6[8] = (byte) 37;
    numArray6[4] = (byte) 7;
    numArray6[5] = (byte) 52;
    numArray6[17] = (byte) 148;
    numArray6[19] = (byte) 231;
    numArray6[18] = (byte) 239;
    numArray6[9] = (byte) 220;
    numArray6[10] = (byte) 247;
    numArray6[11] = (byte) 69;
    numArray6[6] = (byte) 201;
    numArray6[12] = (byte) 195;
    numArray6[14] = (byte) 67;
    numArray6[15] = (byte) 208 /*0xD0*/;
    numArray6[2] = (byte) 222;
    numArray6[16 /*0x10*/] = (byte) 151;
    numArray6[13] = (byte) 34;
    numArray6[3] = (byte) 43;
    numArray6[20] = (byte) 228;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13585()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[9] = (byte) 159;
      numArray2[1] = (byte) 21;
      numArray2[2] = (byte) 212;
      numArray2[5] = (byte) 114;
      numArray2[4] = (byte) 54;
      numArray2[0] = (byte) 217;
      numArray2[7] = (byte) 104;
      numArray2[6] = (byte) 89;
      numArray2[8] = (byte) 213;
      numArray2[3] = (byte) 231;
      byte[] numArray3 = new byte[10]
      {
        (byte) 13,
        (byte) 175,
        (byte) 194,
        (byte) 27,
        (byte) 108,
        (byte) 40,
        (byte) 56,
        (byte) 68,
        (byte) 38,
        (byte) 39
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_13578.sspq, 41, (Array) numArray4, 0, 36);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 41, (Array) numArray4, 0, 36);
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
    numArray6[7] = (byte) 108;
    numArray6[1] = (byte) 82;
    numArray6[2] = (byte) 45;
    numArray6[4] = (byte) 138;
    numArray6[8] = (byte) 80 /*0x50*/;
    numArray6[5] = (byte) 149;
    numArray6[6] = (byte) 17;
    numArray6[0] = (byte) 172;
    numArray6[9] = (byte) 214;
    numArray6[3] = (byte) 17;
    byte[] numArray7 = new byte[10]
    {
      (byte) 254,
      (byte) 115,
      (byte) 237,
      (byte) 20,
      (byte) 227,
      (byte) 5,
      (byte) 144 /*0x90*/,
      (byte) 41,
      (byte) 248,
      (byte) 200
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13586(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 10,
      (byte) 132,
      (byte) 9,
      (byte) 122,
      (byte) 10,
      (byte) 55,
      (byte) 74,
      (byte) 70,
      (byte) 188,
      (byte) 237,
      (byte) 180,
      (byte) 196,
      (byte) 118,
      (byte) 97,
      (byte) 182,
      (byte) 87,
      (byte) 54,
      (byte) 175,
      (byte) 32 /*0x20*/,
      (byte) 22,
      (byte) 220,
      (byte) 75,
      (byte) 193,
      (byte) 247,
      (byte) 228,
      (byte) 161,
      (byte) 250,
      (byte) 30,
      (byte) 53,
      (byte) 46,
      (byte) 230,
      (byte) 66,
      (byte) 8,
      (byte) 152,
      (byte) 128 /*0x80*/,
      (byte) 16 /*0x10*/,
      (byte) 8,
      (byte) 242,
      (byte) 69,
      (byte) 159,
      (byte) 180,
      (byte) 41,
      (byte) 2,
      (byte) 77,
      (byte) 108,
      (byte) 144 /*0x90*/,
      (byte) 226,
      (byte) 181
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 159,
      (byte) 97,
      (byte) 240 /*0xF0*/,
      (byte) 220,
      (byte) 85,
      (byte) 62,
      (byte) 217,
      (byte) 63 /*0x3F*/,
      (byte) 166,
      (byte) 199,
      (byte) 154,
      (byte) 228,
      (byte) 7,
      (byte) 223,
      (byte) 99,
      (byte) 46,
      (byte) 148,
      (byte) 190,
      (byte) 230,
      (byte) 176 /*0xB0*/,
      (byte) 48 /*0x30*/,
      (byte) 35,
      (byte) 90,
      (byte) 35,
      (byte) 2,
      (byte) 89,
      (byte) 1,
      (byte) 2,
      (byte) 230,
      (byte) 27,
      (byte) 77,
      (byte) 216,
      (byte) 87,
      (byte) 167,
      (byte) 105,
      (byte) 123,
      (byte) 142,
      (byte) 141,
      (byte) 100,
      (byte) 118,
      (byte) 236,
      (byte) 45,
      (byte) 107,
      (byte) 82,
      (byte) 194,
      (byte) 245,
      (byte) 161,
      (byte) 21
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13587()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[49];
      byte[] numArray2 = new byte[49]
      {
        (byte) 238,
        (byte) 14,
        (byte) 156,
        (byte) 191,
        (byte) 194,
        (byte) 17,
        (byte) 149,
        (byte) 105,
        (byte) 61,
        (byte) 43,
        (byte) 16 /*0x10*/,
        (byte) 142,
        (byte) 6,
        (byte) 114,
        (byte) 188,
        (byte) 221,
        (byte) 225,
        (byte) 165,
        (byte) 78,
        (byte) 68,
        (byte) 105,
        (byte) 129,
        (byte) 98,
        (byte) 178,
        (byte) 72,
        (byte) 116,
        (byte) 138,
        (byte) 84,
        (byte) 191,
        (byte) 68,
        (byte) 31 /*0x1F*/,
        (byte) 104,
        (byte) 49,
        (byte) 206,
        (byte) 83,
        (byte) 250,
        (byte) 252,
        (byte) 193,
        (byte) 77,
        (byte) 247,
        (byte) 92,
        (byte) 220,
        (byte) 98,
        (byte) 136,
        (byte) 24,
        (byte) 116,
        (byte) 70,
        (byte) 250,
        (byte) 212
      };
      byte[] numArray3 = new byte[49];
      numArray3[47] = (byte) 58;
      numArray3[1] = (byte) 236;
      numArray3[2] = (byte) 62;
      numArray3[37] = (byte) 77;
      numArray3[4] = (byte) 59;
      numArray3[29] = (byte) 192 /*0xC0*/;
      numArray3[6] = (byte) 3;
      numArray3[21] = (byte) 88;
      numArray3[12] = (byte) 241;
      numArray3[40] = (byte) 42;
      numArray3[11] = (byte) 75;
      numArray3[18] = (byte) 28;
      numArray3[30] = (byte) 153;
      numArray3[27] = (byte) 83;
      numArray3[14] = (byte) 3;
      numArray3[7] = (byte) 13;
      numArray3[46] = (byte) 163;
      numArray3[28] = (byte) 26;
      numArray3[15] = (byte) 110;
      numArray3[19] = (byte) 180;
      numArray3[20] = (byte) 95;
      numArray3[26] = (byte) 217;
      numArray3[38] = (byte) 138;
      numArray3[23] = (byte) 223;
      numArray3[3] = (byte) 7;
      numArray3[16 /*0x10*/] = (byte) 231;
      numArray3[8] = (byte) 235;
      numArray3[22] = (byte) 158;
      numArray3[5] = (byte) 177;
      numArray3[41] = (byte) 134;
      numArray3[17] = (byte) 162;
      numArray3[31 /*0x1F*/] = (byte) 244;
      numArray3[35] = (byte) 208 /*0xD0*/;
      numArray3[33] = (byte) 69;
      numArray3[34] = (byte) 81;
      numArray3[0] = (byte) 34;
      numArray3[24] = (byte) 107;
      numArray3[32 /*0x20*/] = (byte) 9;
      numArray3[9] = (byte) 172;
      numArray3[39] = (byte) 207;
      numArray3[25] = (byte) 20;
      numArray3[36] = (byte) 175;
      numArray3[42] = (byte) 147;
      numArray3[43] = (byte) 54;
      numArray3[44] = (byte) 93;
      numArray3[13] = (byte) 247;
      numArray3[45] = (byte) 94;
      numArray3[10] = (byte) 240 /*0xF0*/;
      numArray3[48 /*0x30*/] = (byte) 220;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[33];
      byte[] response = new byte[33];
      Array.Copy((Array) sc_13578.sspq, 77, (Array) numArray4, 0, 33);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 77, (Array) numArray4, 0, 33);
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
    byte[] numArray5 = new byte[49];
    byte[] numArray6 = new byte[49];
    numArray6[44] = (byte) 247;
    numArray6[36] = (byte) 83;
    numArray6[12] = (byte) 169;
    numArray6[27] = (byte) 63 /*0x3F*/;
    numArray6[20] = (byte) 53;
    numArray6[38] = (byte) 202;
    numArray6[16 /*0x10*/] = (byte) 65;
    numArray6[7] = (byte) 168;
    numArray6[8] = (byte) 1;
    numArray6[14] = (byte) 121;
    numArray6[31 /*0x1F*/] = (byte) 115;
    numArray6[11] = (byte) 195;
    numArray6[28] = (byte) 4;
    numArray6[48 /*0x30*/] = (byte) 109;
    numArray6[18] = (byte) 52;
    numArray6[0] = (byte) 31 /*0x1F*/;
    numArray6[4] = (byte) 189;
    numArray6[2] = (byte) 109;
    numArray6[26] = (byte) 168;
    numArray6[19] = (byte) 177;
    numArray6[10] = (byte) 124;
    numArray6[21] = (byte) 248;
    numArray6[22] = (byte) 161;
    numArray6[23] = (byte) 158;
    numArray6[46] = (byte) 43;
    numArray6[45] = (byte) 10;
    numArray6[9] = (byte) 150;
    numArray6[34] = (byte) 1;
    numArray6[3] = (byte) 149;
    numArray6[25] = (byte) 222;
    numArray6[13] = (byte) 222;
    numArray6[17] = (byte) 225;
    numArray6[32 /*0x20*/] = (byte) 38;
    numArray6[29] = (byte) 196;
    numArray6[1] = (byte) 181;
    numArray6[35] = (byte) 65;
    numArray6[6] = (byte) 194;
    numArray6[24] = (byte) 87;
    numArray6[5] = (byte) 195;
    numArray6[39] = (byte) 131;
    numArray6[40] = (byte) 252;
    numArray6[41] = (byte) 58;
    numArray6[42] = (byte) 20;
    numArray6[43] = (byte) 162;
    numArray6[37] = (byte) 90;
    numArray6[15] = (byte) 89;
    numArray6[33] = (byte) 228;
    numArray6[47] = (byte) 122;
    numArray6[30] = (byte) 153;
    byte[] numArray7 = new byte[49];
    numArray7[47] = (byte) 186;
    numArray7[31 /*0x1F*/] = (byte) 232;
    numArray7[13] = (byte) 109;
    numArray7[7] = (byte) 41;
    numArray7[4] = (byte) 223;
    numArray7[36] = (byte) 101;
    numArray7[3] = (byte) 128 /*0x80*/;
    numArray7[25] = (byte) 254;
    numArray7[29] = (byte) 49;
    numArray7[44] = (byte) 119;
    numArray7[42] = (byte) 171;
    numArray7[26] = (byte) 95;
    numArray7[6] = (byte) 182;
    numArray7[28] = (byte) 147;
    numArray7[35] = (byte) 201;
    numArray7[15] = (byte) 170;
    numArray7[16 /*0x10*/] = (byte) 95;
    numArray7[17] = (byte) 236;
    numArray7[2] = (byte) 27;
    numArray7[19] = (byte) 164;
    numArray7[20] = (byte) 96 /*0x60*/;
    numArray7[18] = (byte) 135;
    numArray7[22] = (byte) 62;
    numArray7[23] = (byte) 70;
    numArray7[24] = (byte) 233;
    numArray7[37] = (byte) 222;
    numArray7[14] = (byte) 76;
    numArray7[27] = (byte) 179;
    numArray7[5] = (byte) 99;
    numArray7[12] = (byte) 12;
    numArray7[30] = (byte) 67;
    numArray7[0] = (byte) 48 /*0x30*/;
    numArray7[1] = (byte) 92;
    numArray7[33] = (byte) 83;
    numArray7[34] = (byte) 157;
    numArray7[41] = (byte) 151;
    numArray7[11] = (byte) 27;
    numArray7[8] = (byte) 10;
    numArray7[38] = (byte) 142;
    numArray7[39] = (byte) 159;
    numArray7[40] = (byte) 223;
    numArray7[10] = (byte) 178;
    numArray7[32 /*0x20*/] = (byte) 126;
    numArray7[21] = (byte) 205;
    numArray7[43] = (byte) 139;
    numArray7[45] = (byte) 206;
    numArray7[46] = (byte) 62;
    numArray7[9] = (byte) 88;
    numArray7[48 /*0x30*/] = (byte) 243;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 49);
    for (int index = 0; index < 49; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13588()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[16 /*0x10*/] = (byte) 109;
      numArray2[0] = (byte) 132;
      numArray2[2] = (byte) 108;
      numArray2[4] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 254;
      numArray2[18] = (byte) 221;
      numArray2[13] = (byte) 39;
      numArray2[19] = (byte) 93;
      numArray2[8] = (byte) 236;
      numArray2[7] = (byte) 41;
      numArray2[10] = (byte) 146;
      numArray2[11] = (byte) 47;
      numArray2[12] = (byte) 137;
      numArray2[14] = (byte) 56;
      numArray2[20] = (byte) 19;
      numArray2[9] = (byte) 102;
      numArray2[1] = (byte) 68;
      numArray2[17] = (byte) 17;
      numArray2[15] = (byte) 188;
      numArray2[6] = (byte) 153;
      numArray2[3] = (byte) 45;
      byte[] numArray3 = new byte[21]
      {
        (byte) 108,
        (byte) 210,
        (byte) 145,
        (byte) 104,
        (byte) 108,
        (byte) 84,
        (byte) 132,
        (byte) 63 /*0x3F*/,
        (byte) 50,
        (byte) 36,
        (byte) 140,
        (byte) 46,
        (byte) 225,
        (byte) 99,
        (byte) 131,
        (byte) 11,
        (byte) 173,
        (byte) 34,
        (byte) 76,
        (byte) 160 /*0xA0*/,
        (byte) 88
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 160 /*0xA0*/,
      (byte) 97,
      (byte) 168,
      (byte) 93,
      (byte) 90,
      (byte) 240 /*0xF0*/,
      (byte) 26,
      (byte) 227,
      (byte) 27,
      (byte) 182,
      (byte) 151,
      (byte) 41,
      (byte) 60,
      (byte) 61,
      (byte) 124,
      (byte) 102,
      (byte) 44,
      (byte) 52,
      (byte) 201,
      (byte) 199,
      (byte) 40
    };
    byte[] numArray6 = new byte[21]
    {
      (byte) 123,
      (byte) 19,
      (byte) 68,
      (byte) 242,
      (byte) 252,
      (byte) 144 /*0x90*/,
      (byte) 199,
      (byte) 88,
      (byte) 79,
      (byte) 54,
      (byte) 11,
      (byte) 149,
      (byte) 142,
      (byte) 186,
      (byte) 150,
      byte.MaxValue,
      (byte) 122,
      (byte) 96 /*0x60*/,
      (byte) 6,
      (byte) 40,
      (byte) 94
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13589()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[2] = (byte) 177;
      numArray2[1] = (byte) 224 /*0xE0*/;
      numArray2[0] = (byte) 207;
      numArray2[9] = (byte) 79;
      numArray2[6] = (byte) 179;
      numArray2[5] = (byte) 158;
      numArray2[3] = (byte) 142;
      numArray2[7] = (byte) 9;
      numArray2[8] = (byte) 186;
      numArray2[4] = (byte) 103;
      byte[] numArray3 = new byte[10]
      {
        (byte) 63 /*0x3F*/,
        (byte) 224 /*0xE0*/,
        (byte) 204,
        (byte) 194,
        (byte) 95,
        (byte) 73,
        (byte) 75,
        (byte) 134,
        (byte) 151,
        (byte) 166
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_13578.sspq, 110, (Array) numArray4, 0, 26);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 110, (Array) numArray4, 0, 26);
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
    numArray6[1] = (byte) 172;
    numArray6[0] = (byte) 162;
    numArray6[2] = (byte) 94;
    numArray6[3] = byte.MaxValue;
    numArray6[4] = (byte) 210;
    numArray6[5] = (byte) 31 /*0x1F*/;
    numArray6[7] = (byte) 205;
    numArray6[8] = (byte) 166;
    numArray6[6] = (byte) 145;
    numArray6[9] = (byte) 157;
    byte[] numArray7 = new byte[10];
    numArray7[7] = (byte) 139;
    numArray7[0] = (byte) 88;
    numArray7[9] = (byte) 239;
    numArray7[3] = (byte) 216;
    numArray7[6] = (byte) 63 /*0x3F*/;
    numArray7[5] = (byte) 147;
    numArray7[8] = (byte) 248;
    numArray7[2] = (byte) 198;
    numArray7[1] = (byte) 31 /*0x1F*/;
    numArray7[4] = (byte) 12;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13590()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[55];
      byte[] numArray2 = new byte[55]
      {
        (byte) 171,
        (byte) 148,
        (byte) 234,
        (byte) 97,
        (byte) 26,
        (byte) 107,
        (byte) 125,
        (byte) 155,
        (byte) 173,
        (byte) 21,
        (byte) 183,
        (byte) 158,
        (byte) 95,
        (byte) 21,
        (byte) 221,
        (byte) 130,
        (byte) 234,
        (byte) 247,
        (byte) 140,
        (byte) 118,
        (byte) 38,
        (byte) 112 /*0x70*/,
        (byte) 171,
        (byte) 187,
        (byte) 154,
        (byte) 83,
        (byte) 241,
        (byte) 48 /*0x30*/,
        (byte) 197,
        (byte) 54,
        (byte) 239,
        (byte) 188,
        (byte) 110,
        (byte) 183,
        (byte) 193,
        (byte) 123,
        (byte) 109,
        (byte) 43,
        (byte) 9,
        (byte) 163,
        (byte) 164,
        (byte) 128 /*0x80*/,
        (byte) 131,
        (byte) 234,
        (byte) 235,
        (byte) 198,
        (byte) 31 /*0x1F*/,
        (byte) 24,
        (byte) 131,
        (byte) 134,
        (byte) 133,
        (byte) 183,
        (byte) 23,
        (byte) 96 /*0x60*/,
        (byte) 225
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 207,
        (byte) 70,
        (byte) 72,
        (byte) 25,
        (byte) 44,
        byte.MaxValue,
        (byte) 148,
        (byte) 131,
        (byte) 181,
        (byte) 83,
        (byte) 93,
        (byte) 6,
        (byte) 142,
        (byte) 26,
        (byte) 115,
        (byte) 158,
        (byte) 113,
        (byte) 120,
        (byte) 126,
        (byte) 229,
        (byte) 64 /*0x40*/,
        (byte) 33,
        (byte) 198,
        (byte) 153,
        (byte) 53,
        (byte) 208 /*0xD0*/,
        (byte) 61,
        (byte) 59,
        (byte) 55,
        (byte) 69,
        (byte) 49,
        (byte) 1,
        (byte) 74,
        (byte) 56,
        (byte) 235,
        (byte) 89,
        (byte) 59,
        (byte) 175,
        (byte) 217,
        (byte) 157,
        (byte) 141,
        (byte) 101,
        (byte) 65,
        (byte) 95,
        (byte) 138,
        (byte) 54,
        (byte) 14,
        (byte) 168,
        (byte) 67,
        (byte) 76,
        (byte) 58,
        (byte) 147,
        (byte) 213,
        (byte) 124,
        (byte) 189
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[55];
    byte[] numArray5 = new byte[55]
    {
      (byte) 199,
      (byte) 199,
      (byte) 121,
      (byte) 77,
      (byte) 214,
      (byte) 119,
      (byte) 38,
      (byte) 251,
      (byte) 47,
      (byte) 168,
      (byte) 83,
      (byte) 98,
      (byte) 20,
      (byte) 190,
      (byte) 237,
      (byte) 233,
      (byte) 210,
      (byte) 166,
      (byte) 236,
      (byte) 193,
      (byte) 207,
      (byte) 211,
      (byte) 51,
      (byte) 216,
      (byte) 172,
      (byte) 62,
      (byte) 177,
      (byte) 55,
      (byte) 164,
      (byte) 14,
      (byte) 213,
      (byte) 134,
      (byte) 146,
      (byte) 62,
      (byte) 99,
      (byte) 66,
      (byte) 41,
      (byte) 86,
      (byte) 169,
      (byte) 12,
      (byte) 195,
      (byte) 239,
      (byte) 150,
      (byte) 119,
      (byte) 196,
      (byte) 33,
      (byte) 114,
      (byte) 198,
      (byte) 201,
      (byte) 46,
      (byte) 141,
      (byte) 59,
      (byte) 78,
      (byte) 38,
      (byte) 95
    };
    byte[] numArray6 = new byte[55]
    {
      (byte) 166,
      (byte) 37,
      (byte) 54,
      (byte) 134,
      (byte) 206,
      (byte) 37,
      (byte) 6,
      (byte) 247,
      (byte) 131,
      (byte) 58,
      (byte) 174,
      (byte) 206,
      (byte) 204,
      (byte) 167,
      (byte) 218,
      (byte) 24,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 17,
      (byte) 217,
      (byte) 182,
      (byte) 240 /*0xF0*/,
      (byte) 51,
      (byte) 240 /*0xF0*/,
      (byte) 175,
      (byte) 92,
      (byte) 115,
      (byte) 25,
      (byte) 203,
      (byte) 238,
      (byte) 106,
      (byte) 14,
      (byte) 26,
      (byte) 168,
      (byte) 118,
      (byte) 26,
      (byte) 98,
      (byte) 227,
      (byte) 190,
      (byte) 20,
      (byte) 252,
      (byte) 95,
      (byte) 141,
      (byte) 218,
      (byte) 178,
      (byte) 40,
      (byte) 32 /*0x20*/,
      (byte) 29,
      (byte) 10,
      (byte) 216,
      (byte) 180,
      (byte) 9,
      (byte) 155,
      (byte) 214,
      (byte) 143
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13591()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[4] = (byte) 33;
      numArray2[8] = (byte) 214;
      numArray2[2] = (byte) 238;
      numArray2[11] = (byte) 191;
      numArray2[14] = (byte) 223;
      numArray2[3] = (byte) 86;
      numArray2[6] = (byte) 205;
      numArray2[7] = (byte) 207;
      numArray2[12] = (byte) 123;
      numArray2[9] = (byte) 91;
      numArray2[10] = (byte) 171;
      numArray2[20] = (byte) 139;
      numArray2[1] = (byte) 197;
      numArray2[13] = (byte) 252;
      numArray2[19] = (byte) 19;
      numArray2[15] = (byte) 254;
      numArray2[16 /*0x10*/] = (byte) 224 /*0xE0*/;
      numArray2[17] = (byte) 61;
      numArray2[18] = (byte) 188;
      numArray2[5] = (byte) 210;
      numArray2[0] = (byte) 151;
      byte[] numArray3 = new byte[21]
      {
        (byte) 4,
        (byte) 201,
        (byte) 11,
        (byte) 18,
        (byte) 73,
        (byte) 230,
        (byte) 238,
        (byte) 249,
        (byte) 231,
        (byte) 216,
        (byte) 129,
        (byte) 42,
        (byte) 33,
        (byte) 253,
        (byte) 74,
        (byte) 21,
        (byte) 236,
        (byte) 148,
        (byte) 162,
        (byte) 72,
        (byte) 0
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_13578.sspq, 136, (Array) numArray4, 0, 49);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 136, (Array) numArray4, 0, 49);
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
    byte[] numArray5 = new byte[21];
    byte[] numArray6 = new byte[21]
    {
      (byte) 96 /*0x60*/,
      (byte) 238,
      (byte) 163,
      (byte) 127 /*0x7F*/,
      (byte) 32 /*0x20*/,
      (byte) 55,
      (byte) 76,
      (byte) 84,
      (byte) 214,
      byte.MaxValue,
      (byte) 56,
      (byte) 238,
      (byte) 191,
      (byte) 113,
      (byte) 220,
      (byte) 154,
      (byte) 91,
      (byte) 47,
      (byte) 216,
      (byte) 51,
      (byte) 220
    };
    byte[] numArray7 = new byte[21]
    {
      (byte) 184,
      (byte) 129,
      (byte) 185,
      (byte) 248,
      (byte) 145,
      (byte) 191,
      (byte) 179,
      (byte) 91,
      (byte) 18,
      (byte) 125,
      (byte) 139,
      (byte) 70,
      (byte) 172,
      (byte) 164,
      (byte) 65,
      (byte) 170,
      (byte) 172,
      (byte) 235,
      (byte) 197,
      (byte) 226,
      (byte) 103
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13592()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[3] = (byte) 163;
      numArray2[1] = (byte) 60;
      numArray2[9] = (byte) 187;
      numArray2[6] = (byte) 142;
      numArray2[4] = (byte) 44;
      numArray2[2] = (byte) 38;
      numArray2[5] = (byte) 212;
      numArray2[7] = (byte) 245;
      numArray2[8] = (byte) 169;
      numArray2[0] = (byte) 37;
      byte[] numArray3 = new byte[10]
      {
        (byte) 19,
        (byte) 59,
        (byte) 130,
        (byte) 189,
        (byte) 71,
        (byte) 153,
        (byte) 29,
        (byte) 65,
        (byte) 112 /*0x70*/,
        (byte) 160 /*0xA0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 206;
    numArray5[0] = (byte) 194;
    numArray5[1] = (byte) 172;
    numArray5[3] = (byte) 37;
    numArray5[4] = (byte) 224 /*0xE0*/;
    numArray5[5] = (byte) 159;
    numArray5[6] = (byte) 144 /*0x90*/;
    numArray5[2] = (byte) 16 /*0x10*/;
    numArray5[7] = (byte) 146;
    numArray5[9] = (byte) 10;
    byte[] numArray6 = new byte[10]
    {
      (byte) 144 /*0x90*/,
      (byte) 67,
      (byte) 15,
      (byte) 173,
      (byte) 70,
      (byte) 210,
      (byte) 155,
      (byte) 5,
      (byte) 71,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13593(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 2;
    sourceArray1[9] = (byte) 99;
    sourceArray1[38] = (byte) 241;
    sourceArray1[0] = (byte) 8;
    sourceArray1[4] = (byte) 220;
    sourceArray1[24] = (byte) 71;
    sourceArray1[40] = (byte) 228;
    sourceArray1[7] = (byte) 13;
    sourceArray1[13] = (byte) 174;
    sourceArray1[44] = (byte) 185;
    sourceArray1[16 /*0x10*/] = (byte) 40;
    sourceArray1[11] = (byte) 28;
    sourceArray1[41] = (byte) 238;
    sourceArray1[6] = (byte) 104;
    sourceArray1[14] = (byte) 85;
    sourceArray1[33] = (byte) 37;
    sourceArray1[36] = (byte) 187;
    sourceArray1[17] = (byte) 124;
    sourceArray1[35] = (byte) 142;
    sourceArray1[19] = (byte) 178;
    sourceArray1[47] = (byte) 251;
    sourceArray1[21] = (byte) 213;
    sourceArray1[12] = (byte) 175;
    sourceArray1[30] = (byte) 48 /*0x30*/;
    sourceArray1[23] = (byte) 87;
    sourceArray1[25] = (byte) 24;
    sourceArray1[26] = (byte) 124;
    sourceArray1[27] = (byte) 208 /*0xD0*/;
    sourceArray1[20] = (byte) 52;
    sourceArray1[29] = (byte) 209;
    sourceArray1[3] = (byte) 220;
    sourceArray1[15] = (byte) 25;
    sourceArray1[32 /*0x20*/] = (byte) 181;
    sourceArray1[10] = (byte) 247;
    sourceArray1[18] = (byte) 130;
    sourceArray1[42] = (byte) 70;
    sourceArray1[8] = (byte) 180;
    sourceArray1[37] = (byte) 182;
    sourceArray1[2] = (byte) 213;
    sourceArray1[39] = (byte) 151;
    sourceArray1[1] = (byte) 17;
    sourceArray1[5] = (byte) 115;
    sourceArray1[34] = (byte) 215;
    sourceArray1[43] = (byte) 194;
    sourceArray1[31 /*0x1F*/] = (byte) 159;
    sourceArray1[45] = (byte) 141;
    sourceArray1[46] = (byte) 124;
    sourceArray1[22] = (byte) 252;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 158;
    sourceArray2[5] = (byte) 249;
    sourceArray2[2] = (byte) 215;
    sourceArray2[3] = (byte) 133;
    sourceArray2[44] = (byte) 99;
    sourceArray2[12] = (byte) 34;
    sourceArray2[6] = (byte) 82;
    sourceArray2[26] = (byte) 45;
    sourceArray2[11] = (byte) 83;
    sourceArray2[31 /*0x1F*/] = (byte) 39;
    sourceArray2[9] = (byte) 25;
    sourceArray2[8] = (byte) 74;
    sourceArray2[25] = (byte) 206;
    sourceArray2[13] = (byte) 169;
    sourceArray2[14] = (byte) 31 /*0x1F*/;
    sourceArray2[41] = (byte) 139;
    sourceArray2[16 /*0x10*/] = (byte) 74;
    sourceArray2[22] = (byte) 52;
    sourceArray2[18] = (byte) 199;
    sourceArray2[19] = (byte) 224 /*0xE0*/;
    sourceArray2[23] = (byte) 228;
    sourceArray2[37] = (byte) 9;
    sourceArray2[29] = (byte) 8;
    sourceArray2[39] = (byte) 191;
    sourceArray2[0] = (byte) 122;
    sourceArray2[10] = (byte) 62;
    sourceArray2[24] = (byte) 93;
    sourceArray2[27] = (byte) 39;
    sourceArray2[28] = (byte) 226;
    sourceArray2[4] = (byte) 149;
    sourceArray2[15] = (byte) 98;
    sourceArray2[20] = (byte) 24;
    sourceArray2[32 /*0x20*/] = (byte) 139;
    sourceArray2[43] = (byte) 6;
    sourceArray2[34] = (byte) 238;
    sourceArray2[35] = (byte) 184;
    sourceArray2[36] = (byte) 120;
    sourceArray2[17] = (byte) 111;
    sourceArray2[38] = (byte) 40;
    sourceArray2[30] = (byte) 108;
    sourceArray2[40] = (byte) 92;
    sourceArray2[1] = (byte) 158;
    sourceArray2[42] = (byte) 151;
    sourceArray2[33] = (byte) 0;
    sourceArray2[47] = (byte) 153;
    sourceArray2[45] = (byte) 155;
    sourceArray2[46] = (byte) 127 /*0x7F*/;
    sourceArray2[21] = (byte) 148;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13594()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[49];
      byte[] numArray2 = new byte[49];
      numArray2[27] = (byte) 204;
      numArray2[1] = (byte) 43;
      numArray2[46] = (byte) 93;
      numArray2[3] = (byte) 223;
      numArray2[0] = (byte) 5;
      numArray2[5] = (byte) 120;
      numArray2[6] = (byte) 32 /*0x20*/;
      numArray2[7] = (byte) 204;
      numArray2[20] = (byte) 71;
      numArray2[15] = (byte) 87;
      numArray2[2] = (byte) 224 /*0xE0*/;
      numArray2[11] = (byte) 127 /*0x7F*/;
      numArray2[12] = (byte) 202;
      numArray2[23] = (byte) 27;
      numArray2[14] = (byte) 85;
      numArray2[32 /*0x20*/] = (byte) 193;
      numArray2[8] = (byte) 196;
      numArray2[38] = (byte) 102;
      numArray2[18] = (byte) 48 /*0x30*/;
      numArray2[19] = (byte) 164;
      numArray2[30] = (byte) 49;
      numArray2[37] = (byte) 129;
      numArray2[34] = (byte) 87;
      numArray2[22] = (byte) 26;
      numArray2[16 /*0x10*/] = (byte) 36;
      numArray2[4] = (byte) 196;
      numArray2[35] = (byte) 30;
      numArray2[41] = (byte) 198;
      numArray2[28] = (byte) 39;
      numArray2[24] = (byte) 157;
      numArray2[44] = (byte) 191;
      numArray2[31 /*0x1F*/] = (byte) 175;
      numArray2[26] = (byte) 135;
      numArray2[33] = (byte) 10;
      numArray2[25] = (byte) 118;
      numArray2[17] = (byte) 121;
      numArray2[36] = (byte) 161;
      numArray2[39] = (byte) 50;
      numArray2[45] = (byte) 231;
      numArray2[9] = (byte) 243;
      numArray2[40] = (byte) 27;
      numArray2[13] = (byte) 45;
      numArray2[42] = (byte) 229;
      numArray2[43] = (byte) 103;
      numArray2[29] = (byte) 90;
      numArray2[21] = (byte) 126;
      numArray2[10] = (byte) 143;
      numArray2[47] = (byte) 98;
      numArray2[48 /*0x30*/] = (byte) 31 /*0x1F*/;
      byte[] numArray3 = new byte[49]
      {
        (byte) 252,
        (byte) 7,
        (byte) 52,
        (byte) 239,
        (byte) 97,
        (byte) 86,
        (byte) 160 /*0xA0*/,
        (byte) 29,
        (byte) 131,
        (byte) 7,
        (byte) 21,
        (byte) 62,
        (byte) 238,
        (byte) 190,
        (byte) 176 /*0xB0*/,
        (byte) 114,
        (byte) 221,
        (byte) 76,
        (byte) 5,
        (byte) 219,
        (byte) 177,
        (byte) 245,
        (byte) 53,
        (byte) 241,
        (byte) 195,
        (byte) 64 /*0x40*/,
        (byte) 221,
        (byte) 21,
        (byte) 37,
        (byte) 127 /*0x7F*/,
        (byte) 3,
        (byte) 65,
        (byte) 150,
        (byte) 39,
        (byte) 78,
        (byte) 12,
        (byte) 88,
        (byte) 237,
        (byte) 107,
        (byte) 128 /*0x80*/,
        (byte) 73,
        (byte) 16 /*0x10*/,
        (byte) 240 /*0xF0*/,
        (byte) 101,
        (byte) 128 /*0x80*/,
        (byte) 116,
        (byte) 32 /*0x20*/,
        (byte) 107,
        (byte) 135
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_13578.sspq, 185, (Array) numArray4, 0, 54);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 185, (Array) numArray4, 0, 54);
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
    byte[] numArray5 = new byte[49];
    byte[] numArray6 = new byte[49];
    numArray6[7] = (byte) 113;
    numArray6[1] = (byte) 136;
    numArray6[15] = (byte) 227;
    numArray6[5] = (byte) 119;
    numArray6[4] = (byte) 166;
    numArray6[25] = (byte) 178;
    numArray6[6] = (byte) 178;
    numArray6[36] = (byte) 6;
    numArray6[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray6[30] = (byte) 95;
    numArray6[10] = (byte) 81;
    numArray6[11] = (byte) 237;
    numArray6[12] = (byte) 0;
    numArray6[29] = (byte) 151;
    numArray6[14] = (byte) 52;
    numArray6[39] = (byte) 191;
    numArray6[16 /*0x10*/] = (byte) 51;
    numArray6[46] = (byte) 177;
    numArray6[18] = (byte) 225;
    numArray6[44] = (byte) 92;
    numArray6[20] = (byte) 222;
    numArray6[13] = (byte) 186;
    numArray6[22] = (byte) 31 /*0x1F*/;
    numArray6[23] = (byte) 124;
    numArray6[41] = (byte) 101;
    numArray6[2] = (byte) 189;
    numArray6[42] = (byte) 112 /*0x70*/;
    numArray6[27] = (byte) 71;
    numArray6[28] = (byte) 160 /*0xA0*/;
    numArray6[31 /*0x1F*/] = (byte) 129;
    numArray6[21] = (byte) 36;
    numArray6[47] = (byte) 40;
    numArray6[32 /*0x20*/] = (byte) 59;
    numArray6[33] = (byte) 23;
    numArray6[34] = (byte) 144 /*0x90*/;
    numArray6[35] = (byte) 15;
    numArray6[3] = (byte) 76;
    numArray6[24] = (byte) 102;
    numArray6[38] = (byte) 249;
    numArray6[37] = (byte) 217;
    numArray6[40] = (byte) 31 /*0x1F*/;
    numArray6[8] = (byte) 30;
    numArray6[17] = (byte) 29;
    numArray6[43] = (byte) 22;
    numArray6[0] = (byte) 234;
    numArray6[45] = (byte) 133;
    numArray6[19] = (byte) 204;
    numArray6[26] = (byte) 25;
    numArray6[9] = (byte) 206;
    byte[] numArray7 = new byte[49];
    numArray7[36] = (byte) 24;
    numArray7[10] = (byte) 222;
    numArray7[5] = (byte) 149;
    numArray7[2] = (byte) 28;
    numArray7[4] = (byte) 63 /*0x3F*/;
    numArray7[47] = (byte) 40;
    numArray7[6] = (byte) 70;
    numArray7[7] = (byte) 168;
    numArray7[43] = (byte) 114;
    numArray7[22] = (byte) 139;
    numArray7[1] = (byte) 158;
    numArray7[11] = (byte) 66;
    numArray7[44] = (byte) 119;
    numArray7[27] = (byte) 57;
    numArray7[14] = (byte) 171;
    numArray7[32 /*0x20*/] = (byte) 157;
    numArray7[20] = (byte) 217;
    numArray7[38] = (byte) 99;
    numArray7[18] = (byte) 221;
    numArray7[26] = (byte) 96 /*0x60*/;
    numArray7[23] = (byte) 143;
    numArray7[21] = (byte) 211;
    numArray7[45] = byte.MaxValue;
    numArray7[19] = (byte) 195;
    numArray7[24] = (byte) 39;
    numArray7[25] = (byte) 243;
    numArray7[8] = (byte) 58;
    numArray7[33] = (byte) 197;
    numArray7[28] = (byte) 12;
    numArray7[29] = (byte) 74;
    numArray7[30] = (byte) 247;
    numArray7[41] = (byte) 22;
    numArray7[16 /*0x10*/] = (byte) 77;
    numArray7[31 /*0x1F*/] = (byte) 108;
    numArray7[34] = (byte) 39;
    numArray7[42] = (byte) 160 /*0xA0*/;
    numArray7[9] = (byte) 46;
    numArray7[37] = (byte) 145;
    numArray7[40] = (byte) 88;
    numArray7[39] = (byte) 192 /*0xC0*/;
    numArray7[35] = (byte) 44;
    numArray7[13] = (byte) 161;
    numArray7[0] = (byte) 68;
    numArray7[46] = (byte) 154;
    numArray7[17] = (byte) 170;
    numArray7[3] = (byte) 142;
    numArray7[15] = (byte) 219;
    numArray7[12] = (byte) 173;
    numArray7[48 /*0x30*/] = (byte) 25;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 49);
    for (int index = 0; index < 49; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[35];
    byte[] response1 = new byte[35];
    Array.Copy((Array) sc_13578.sspq, 239, (Array) numArray8, 0, 35);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13578.sspr, 239, (Array) numArray8, 0, 35);
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

  internal static string ssp_appserver_13595()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[9] = (byte) 134;
      numArray2[13] = (byte) 77;
      numArray2[2] = (byte) 35;
      numArray2[3] = (byte) 215;
      numArray2[17] = (byte) 117;
      numArray2[1] = byte.MaxValue;
      numArray2[6] = (byte) 184;
      numArray2[7] = (byte) 246;
      numArray2[16 /*0x10*/] = (byte) 89;
      numArray2[5] = (byte) 77;
      numArray2[8] = (byte) 225;
      numArray2[11] = (byte) 40;
      numArray2[12] = (byte) 116;
      numArray2[10] = (byte) 50;
      numArray2[14] = (byte) 25;
      numArray2[15] = (byte) 250;
      numArray2[0] = (byte) 108;
      numArray2[4] = (byte) 81;
      numArray2[18] = (byte) 216;
      numArray2[19] = (byte) 189;
      numArray2[20] = (byte) 171;
      byte[] numArray3 = new byte[21]
      {
        (byte) 132,
        (byte) 25,
        (byte) 3,
        (byte) 144 /*0x90*/,
        (byte) 251,
        (byte) 24,
        (byte) 17,
        (byte) 59,
        (byte) 201,
        (byte) 33,
        (byte) 56,
        (byte) 226,
        (byte) 22,
        (byte) 157,
        (byte) 41,
        (byte) 53,
        (byte) 95,
        (byte) 158,
        (byte) 173,
        (byte) 231,
        (byte) 130
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21]
    {
      (byte) 220,
      (byte) 48 /*0x30*/,
      (byte) 231,
      (byte) 85,
      (byte) 124,
      (byte) 176 /*0xB0*/,
      (byte) 90,
      (byte) 207,
      (byte) 253,
      (byte) 185,
      (byte) 228,
      (byte) 25,
      (byte) 33,
      (byte) 123,
      (byte) 85,
      (byte) 228,
      (byte) 220,
      (byte) 154,
      (byte) 139,
      (byte) 143,
      (byte) 11
    };
    byte[] numArray6 = new byte[21]
    {
      byte.MaxValue,
      (byte) 209,
      (byte) 38,
      (byte) 201,
      (byte) 88,
      (byte) 14,
      (byte) 152,
      (byte) 0,
      (byte) 59,
      (byte) 189,
      (byte) 158,
      (byte) 204,
      (byte) 248,
      (byte) 190,
      (byte) 188,
      (byte) 37,
      (byte) 214,
      (byte) 11,
      (byte) 84,
      (byte) 148,
      (byte) 180
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[30];
    byte[] response = new byte[30];
    Array.Copy((Array) sc_13578.sspq, 274, (Array) numArray7, 0, 30);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13578.sspr, 274, (Array) numArray7, 0, 30);
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

  internal static string ssp_appserver_13596()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 16 /*0x10*/;
      numArray2[1] = (byte) 41;
      numArray2[2] = (byte) 201;
      numArray2[6] = (byte) 33;
      numArray2[4] = (byte) 57;
      numArray2[3] = (byte) 84;
      numArray2[0] = (byte) 155;
      numArray2[7] = (byte) 241;
      numArray2[8] = (byte) 209;
      numArray2[9] = (byte) 233;
      byte[] numArray3 = new byte[10]
      {
        (byte) 89,
        (byte) 35,
        (byte) 153,
        (byte) 110,
        (byte) 222,
        (byte) 51,
        (byte) 55,
        (byte) 165,
        (byte) 220,
        (byte) 220
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_13578.sspq, 304, (Array) numArray4, 0, 36);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 304, (Array) numArray4, 0, 36);
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
    numArray6[0] = (byte) 155;
    numArray6[5] = (byte) 47;
    numArray6[2] = (byte) 185;
    numArray6[3] = (byte) 20;
    numArray6[9] = (byte) 9;
    numArray6[7] = (byte) 176 /*0xB0*/;
    numArray6[1] = (byte) 185;
    numArray6[6] = (byte) 52;
    numArray6[8] = (byte) 90;
    numArray6[4] = (byte) 241;
    byte[] numArray7 = new byte[10];
    numArray7[6] = (byte) 97;
    numArray7[2] = (byte) 196;
    numArray7[5] = (byte) 116;
    numArray7[1] = (byte) 15;
    numArray7[4] = (byte) 200;
    numArray7[0] = (byte) 60;
    numArray7[9] = (byte) 137;
    numArray7[7] = (byte) 25;
    numArray7[8] = (byte) 226;
    numArray7[3] = (byte) 73;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13597()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[47];
      byte[] numArray2 = new byte[47]
      {
        (byte) 240 /*0xF0*/,
        (byte) 123,
        (byte) 214,
        (byte) 64 /*0x40*/,
        (byte) 207,
        (byte) 235,
        (byte) 166,
        (byte) 185,
        (byte) 240 /*0xF0*/,
        (byte) 217,
        (byte) 226,
        (byte) 62,
        (byte) 163,
        (byte) 62,
        (byte) 121,
        (byte) 115,
        (byte) 21,
        (byte) 196,
        (byte) 179,
        (byte) 65,
        (byte) 132,
        (byte) 114,
        (byte) 53,
        byte.MaxValue,
        (byte) 197,
        (byte) 21,
        (byte) 171,
        (byte) 139,
        (byte) 111,
        (byte) 89,
        (byte) 199,
        (byte) 143,
        (byte) 131,
        (byte) 54,
        (byte) 118,
        (byte) 1,
        (byte) 193,
        (byte) 216,
        (byte) 130,
        (byte) 17,
        (byte) 245,
        (byte) 236,
        (byte) 57,
        (byte) 170,
        (byte) 215,
        (byte) 15,
        (byte) 56
      };
      byte[] numArray3 = new byte[47]
      {
        (byte) 123,
        (byte) 30,
        (byte) 167,
        (byte) 16 /*0x10*/,
        (byte) 186,
        (byte) 61,
        (byte) 243,
        (byte) 155,
        (byte) 53,
        (byte) 5,
        (byte) 38,
        (byte) 60,
        (byte) 13,
        (byte) 112 /*0x70*/,
        (byte) 197,
        (byte) 225,
        (byte) 218,
        (byte) 247,
        (byte) 200,
        (byte) 157,
        (byte) 1,
        (byte) 248,
        (byte) 80 /*0x50*/,
        (byte) 144 /*0x90*/,
        (byte) 198,
        (byte) 100,
        (byte) 113,
        (byte) 205,
        (byte) 35,
        (byte) 165,
        (byte) 137,
        (byte) 14,
        (byte) 95,
        (byte) 250,
        (byte) 58,
        (byte) 111,
        (byte) 120,
        (byte) 247,
        (byte) 69,
        (byte) 69,
        (byte) 28,
        (byte) 81,
        (byte) 69,
        (byte) 240 /*0xF0*/,
        (byte) 61,
        (byte) 239,
        (byte) 89
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[47];
    byte[] numArray5 = new byte[47];
    numArray5[4] = (byte) 189;
    numArray5[46] = (byte) 162;
    numArray5[33] = (byte) 161;
    numArray5[39] = (byte) 248;
    numArray5[28] = (byte) 39;
    numArray5[5] = (byte) 126;
    numArray5[6] = (byte) 70;
    numArray5[7] = (byte) 214;
    numArray5[3] = (byte) 106;
    numArray5[9] = (byte) 186;
    numArray5[26] = (byte) 56;
    numArray5[43] = (byte) 199;
    numArray5[15] = (byte) 82;
    numArray5[44] = (byte) 209;
    numArray5[14] = (byte) 17;
    numArray5[8] = (byte) 122;
    numArray5[10] = (byte) 64 /*0x40*/;
    numArray5[17] = (byte) 108;
    numArray5[38] = (byte) 121;
    numArray5[19] = (byte) 120;
    numArray5[20] = (byte) 177;
    numArray5[2] = (byte) 88;
    numArray5[22] = (byte) 66;
    numArray5[1] = (byte) 125;
    numArray5[24] = (byte) 76;
    numArray5[18] = (byte) 199;
    numArray5[34] = (byte) 109;
    numArray5[27] = (byte) 139;
    numArray5[40] = (byte) 187;
    numArray5[29] = (byte) 181;
    numArray5[30] = (byte) 220;
    numArray5[36] = (byte) 55;
    numArray5[32 /*0x20*/] = (byte) 92;
    numArray5[21] = (byte) 61;
    numArray5[41] = (byte) 234;
    numArray5[35] = (byte) 74;
    numArray5[42] = (byte) 93;
    numArray5[37] = (byte) 25;
    numArray5[12] = (byte) 73;
    numArray5[13] = (byte) 150;
    numArray5[31 /*0x1F*/] = (byte) 101;
    numArray5[0] = (byte) 235;
    numArray5[25] = (byte) 152;
    numArray5[16 /*0x10*/] = (byte) 186;
    numArray5[45] = (byte) 219;
    numArray5[11] = (byte) 26;
    numArray5[23] = (byte) 161;
    byte[] numArray6 = new byte[47];
    numArray6[19] = (byte) 59;
    numArray6[45] = (byte) 203;
    numArray6[9] = (byte) 146;
    numArray6[3] = (byte) 126;
    numArray6[21] = (byte) 30;
    numArray6[5] = (byte) 131;
    numArray6[25] = (byte) 194;
    numArray6[7] = (byte) 188;
    numArray6[6] = (byte) 234;
    numArray6[31 /*0x1F*/] = (byte) 13;
    numArray6[10] = (byte) 154;
    numArray6[11] = (byte) 189;
    numArray6[28] = (byte) 117;
    numArray6[13] = (byte) 23;
    numArray6[14] = (byte) 45;
    numArray6[0] = (byte) 169;
    numArray6[24] = (byte) 211;
    numArray6[37] = (byte) 77;
    numArray6[18] = byte.MaxValue;
    numArray6[26] = (byte) 100;
    numArray6[4] = (byte) 72;
    numArray6[32 /*0x20*/] = (byte) 33;
    numArray6[22] = (byte) 182;
    numArray6[12] = (byte) 3;
    numArray6[41] = (byte) 187;
    numArray6[20] = (byte) 193;
    numArray6[2] = (byte) 53;
    numArray6[33] = (byte) 103;
    numArray6[27] = (byte) 80 /*0x50*/;
    numArray6[35] = (byte) 45;
    numArray6[30] = (byte) 162;
    numArray6[17] = (byte) 201;
    numArray6[16 /*0x10*/] = (byte) 212;
    numArray6[8] = (byte) 152;
    numArray6[34] = (byte) 170;
    numArray6[23] = (byte) 109;
    numArray6[36] = (byte) 52;
    numArray6[42] = (byte) 190;
    numArray6[38] = (byte) 82;
    numArray6[39] = (byte) 227;
    numArray6[40] = (byte) 2;
    numArray6[1] = (byte) 148;
    numArray6[15] = (byte) 16 /*0x10*/;
    numArray6[43] = (byte) 135;
    numArray6[44] = (byte) 47;
    numArray6[29] = (byte) 176 /*0xB0*/;
    numArray6[46] = (byte) 146;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 47);
    for (int index = 0; index < 47; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13598()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 78,
        (byte) 153,
        (byte) 77,
        (byte) 35,
        (byte) 3,
        (byte) 55,
        (byte) 46,
        (byte) 52,
        (byte) 27,
        (byte) 1,
        (byte) 6,
        (byte) 214,
        (byte) 15,
        (byte) 24,
        (byte) 205,
        (byte) 13,
        (byte) 57,
        (byte) 147,
        (byte) 63 /*0x3F*/,
        (byte) 201,
        (byte) 136
      };
      byte[] numArray3 = new byte[21]
      {
        (byte) 140,
        (byte) 39,
        (byte) 50,
        (byte) 67,
        (byte) 209,
        (byte) 191,
        (byte) 81,
        (byte) 150,
        (byte) 251,
        (byte) 237,
        (byte) 189,
        (byte) 200,
        (byte) 156,
        (byte) 233,
        (byte) 166,
        (byte) 52,
        (byte) 165,
        (byte) 162,
        (byte) 108,
        (byte) 230,
        (byte) 107
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21];
    numArray5[8] = (byte) 243;
    numArray5[7] = (byte) 108;
    numArray5[11] = (byte) 2;
    numArray5[3] = (byte) 210;
    numArray5[1] = (byte) 147;
    numArray5[16 /*0x10*/] = (byte) 92;
    numArray5[5] = (byte) 150;
    numArray5[14] = (byte) 24;
    numArray5[6] = (byte) 190;
    numArray5[2] = (byte) 167;
    numArray5[10] = (byte) 106;
    numArray5[4] = (byte) 183;
    numArray5[12] = (byte) 88;
    numArray5[13] = (byte) 123;
    numArray5[15] = (byte) 131;
    numArray5[9] = (byte) 122;
    numArray5[18] = (byte) 117;
    numArray5[17] = (byte) 17;
    numArray5[0] = (byte) 52;
    numArray5[19] = (byte) 215;
    numArray5[20] = (byte) 75;
    byte[] numArray6 = new byte[21]
    {
      (byte) 138,
      (byte) 7,
      (byte) 137,
      (byte) 77,
      (byte) 157,
      (byte) 249,
      (byte) 186,
      (byte) 172,
      (byte) 131,
      (byte) 134,
      (byte) 44,
      (byte) 239,
      (byte) 232,
      (byte) 13,
      (byte) 182,
      (byte) 235,
      (byte) 147,
      (byte) 194,
      (byte) 191,
      (byte) 201,
      (byte) 39
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13599()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 108;
      numArray2[1] = (byte) 189;
      numArray2[7] = (byte) 53;
      numArray2[9] = (byte) 15;
      numArray2[5] = (byte) 119;
      numArray2[8] = (byte) 15;
      numArray2[6] = (byte) 186;
      numArray2[3] = (byte) 187;
      numArray2[0] = (byte) 180;
      numArray2[2] = (byte) 123;
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 31 /*0x1F*/;
      numArray3[9] = (byte) 38;
      numArray3[6] = (byte) 223;
      numArray3[3] = (byte) 155;
      numArray3[4] = (byte) 121;
      numArray3[1] = (byte) 119;
      numArray3[0] = (byte) 141;
      numArray3[5] = (byte) 54;
      numArray3[2] = (byte) 72;
      numArray3[7] = (byte) 145;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 161,
      (byte) 233,
      (byte) 64 /*0x40*/,
      (byte) 39,
      (byte) 176 /*0xB0*/,
      (byte) 248,
      (byte) 239,
      (byte) 100,
      (byte) 86,
      (byte) 227
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 117,
      (byte) 155,
      (byte) 87,
      (byte) 99,
      (byte) 7,
      (byte) 227,
      (byte) 226,
      (byte) 162,
      (byte) 186,
      (byte) 54
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13600(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 236,
      (byte) 39,
      (byte) 153,
      (byte) 85,
      (byte) 129,
      (byte) 99,
      (byte) 84,
      (byte) 78,
      (byte) 49,
      (byte) 210,
      (byte) 199,
      (byte) 108,
      (byte) 226,
      (byte) 202,
      (byte) 161,
      (byte) 93,
      (byte) 206,
      (byte) 16 /*0x10*/,
      (byte) 70,
      (byte) 196,
      (byte) 47,
      (byte) 87,
      (byte) 244,
      (byte) 79,
      (byte) 148,
      (byte) 36,
      (byte) 230,
      (byte) 32 /*0x20*/,
      (byte) 131,
      (byte) 172,
      (byte) 193,
      (byte) 3,
      (byte) 10,
      (byte) 48 /*0x30*/,
      (byte) 3,
      (byte) 109,
      (byte) 23,
      (byte) 235,
      (byte) 245,
      (byte) 246,
      (byte) 76,
      (byte) 155,
      (byte) 15,
      (byte) 84,
      (byte) 232,
      (byte) 140,
      (byte) 13,
      (byte) 154
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 54,
      (byte) 164,
      (byte) 3,
      (byte) 224 /*0xE0*/,
      (byte) 157,
      (byte) 2,
      (byte) 187,
      (byte) 17,
      (byte) 153,
      (byte) 179,
      (byte) 112 /*0x70*/,
      (byte) 227,
      (byte) 155,
      (byte) 21,
      (byte) 153,
      (byte) 181,
      (byte) 147,
      (byte) 106,
      (byte) 225,
      (byte) 203,
      (byte) 254,
      (byte) 250,
      (byte) 164,
      (byte) 230,
      (byte) 196,
      (byte) 231,
      (byte) 114,
      (byte) 135,
      (byte) 133,
      (byte) 117,
      (byte) 61,
      (byte) 209,
      (byte) 136,
      (byte) 233,
      (byte) 175,
      (byte) 29,
      (byte) 0,
      (byte) 66,
      (byte) 240 /*0xF0*/,
      (byte) 226,
      (byte) 157,
      (byte) 38,
      (byte) 7,
      (byte) 10,
      (byte) 148,
      (byte) 20,
      (byte) 121,
      (byte) 104
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[33];
    byte[] response2 = new byte[33];
    Array.Copy((Array) sc_13578.sspq, 340, (Array) numArray2, 0, 33);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13578.sspr, 340, (Array) numArray2, 0, 33);
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

  internal static int ssp_appserver_13601(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 217,
      (byte) 252,
      (byte) 109,
      (byte) 58,
      (byte) 205,
      (byte) 42,
      (byte) 151,
      (byte) 122,
      (byte) 24,
      (byte) 209,
      (byte) 138,
      (byte) 68,
      (byte) 36,
      (byte) 43,
      (byte) 92,
      (byte) 211,
      (byte) 128 /*0x80*/,
      (byte) 63 /*0x3F*/,
      (byte) 47,
      (byte) 186,
      (byte) 90,
      (byte) 184,
      (byte) 161,
      (byte) 78,
      (byte) 147,
      (byte) 227,
      (byte) 185,
      (byte) 70,
      (byte) 47,
      (byte) 200,
      (byte) 204,
      (byte) 234,
      (byte) 37,
      (byte) 107,
      (byte) 135,
      (byte) 181,
      (byte) 89,
      (byte) 249,
      (byte) 120,
      (byte) 184,
      (byte) 132,
      (byte) 213,
      (byte) 95,
      (byte) 193,
      (byte) 188,
      (byte) 91,
      (byte) 66,
      (byte) 220
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 26,
      (byte) 193,
      (byte) 162,
      (byte) 99,
      (byte) 109,
      (byte) 194,
      (byte) 41,
      (byte) 18,
      (byte) 78,
      (byte) 168,
      (byte) 203,
      (byte) 176 /*0xB0*/,
      (byte) 187,
      (byte) 55,
      (byte) 138,
      (byte) 189,
      (byte) 164,
      (byte) 19,
      (byte) 163,
      (byte) 38,
      (byte) 57,
      (byte) 61,
      (byte) 53,
      (byte) 16 /*0x10*/,
      (byte) 139,
      (byte) 56,
      (byte) 208 /*0xD0*/,
      (byte) 120,
      (byte) 205,
      (byte) 60,
      (byte) 195,
      (byte) 41,
      (byte) 33,
      (byte) 166,
      (byte) 65,
      (byte) 92,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 45,
      (byte) 59,
      (byte) 192 /*0xC0*/,
      (byte) 243,
      (byte) 252,
      (byte) 107,
      (byte) 89,
      (byte) 167,
      (byte) 252,
      (byte) 10
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13602(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 48 /*0x30*/,
      byte.MaxValue,
      (byte) 33,
      (byte) 90,
      (byte) 31 /*0x1F*/,
      (byte) 167,
      (byte) 233,
      (byte) 113,
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 79,
      (byte) 5,
      (byte) 160 /*0xA0*/,
      (byte) 30,
      (byte) 185,
      (byte) 163,
      (byte) 167,
      (byte) 122,
      (byte) 134,
      (byte) 199,
      (byte) 118,
      (byte) 54,
      (byte) 132,
      (byte) 89,
      (byte) 224 /*0xE0*/,
      (byte) 145,
      (byte) 55,
      (byte) 161,
      (byte) 220,
      (byte) 140,
      (byte) 183,
      (byte) 222,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 245,
      (byte) 107,
      (byte) 102,
      (byte) 90,
      (byte) 57,
      (byte) 82,
      (byte) 142,
      (byte) 70,
      (byte) 51,
      (byte) 138,
      (byte) 16 /*0x10*/,
      (byte) 187,
      (byte) 28,
      (byte) 60
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 59,
      (byte) 0,
      (byte) 124,
      (byte) 107,
      (byte) 23,
      (byte) 170,
      (byte) 5,
      (byte) 35,
      (byte) 169,
      (byte) 26,
      (byte) 112 /*0x70*/,
      (byte) 241,
      (byte) 219,
      (byte) 109,
      (byte) 160 /*0xA0*/,
      (byte) 79,
      (byte) 254,
      (byte) 231,
      (byte) 242,
      (byte) 64 /*0x40*/,
      (byte) 165,
      (byte) 168,
      (byte) 239,
      (byte) 69,
      (byte) 171,
      (byte) 212,
      (byte) 128 /*0x80*/,
      (byte) 247,
      (byte) 107,
      (byte) 164,
      (byte) 230,
      (byte) 242,
      (byte) 78,
      (byte) 221,
      (byte) 182,
      (byte) 131,
      (byte) 41,
      (byte) 242,
      (byte) 77,
      (byte) 131,
      (byte) 131,
      (byte) 214,
      (byte) 151,
      (byte) 187,
      (byte) 246,
      (byte) 58,
      (byte) 166,
      (byte) 140
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13603(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 244,
      (byte) 249,
      (byte) 122,
      (byte) 199,
      (byte) 121,
      (byte) 142,
      (byte) 111,
      (byte) 72,
      (byte) 149,
      (byte) 196,
      (byte) 147,
      (byte) 113,
      (byte) 247,
      (byte) 32 /*0x20*/,
      (byte) 41,
      (byte) 77,
      (byte) 61,
      (byte) 76,
      (byte) 65,
      (byte) 52,
      (byte) 56,
      (byte) 48 /*0x30*/,
      (byte) 3,
      (byte) 249,
      (byte) 73,
      (byte) 36,
      (byte) 117,
      (byte) 251,
      (byte) 23,
      (byte) 163,
      (byte) 121,
      (byte) 185,
      (byte) 123,
      (byte) 22,
      (byte) 139,
      (byte) 251,
      (byte) 240 /*0xF0*/,
      (byte) 158,
      (byte) 225,
      (byte) 171,
      (byte) 227,
      (byte) 70,
      (byte) 177,
      (byte) 110,
      (byte) 247,
      (byte) 159,
      (byte) 196
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[36] = (byte) 221;
    sourceArray2[1] = (byte) 216;
    sourceArray2[5] = (byte) 11;
    sourceArray2[20] = (byte) 130;
    sourceArray2[4] = (byte) 133;
    sourceArray2[11] = (byte) 219;
    sourceArray2[6] = (byte) 223;
    sourceArray2[7] = (byte) 88;
    sourceArray2[43] = (byte) 87;
    sourceArray2[15] = (byte) 65;
    sourceArray2[8] = (byte) 242;
    sourceArray2[23] = (byte) 189;
    sourceArray2[37] = (byte) 68;
    sourceArray2[17] = (byte) 4;
    sourceArray2[14] = (byte) 28;
    sourceArray2[21] = (byte) 99;
    sourceArray2[45] = (byte) 12;
    sourceArray2[40] = (byte) 181;
    sourceArray2[13] = (byte) 227;
    sourceArray2[42] = (byte) 208 /*0xD0*/;
    sourceArray2[18] = (byte) 131;
    sourceArray2[22] = (byte) 238;
    sourceArray2[12] = (byte) 121;
    sourceArray2[44] = (byte) 152;
    sourceArray2[24] = (byte) 152;
    sourceArray2[25] = (byte) 151;
    sourceArray2[26] = (byte) 169;
    sourceArray2[0] = (byte) 4;
    sourceArray2[28] = (byte) 206;
    sourceArray2[29] = (byte) 29;
    sourceArray2[30] = (byte) 202;
    sourceArray2[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    sourceArray2[32 /*0x20*/] = (byte) 39;
    sourceArray2[33] = (byte) 157;
    sourceArray2[2] = (byte) 246;
    sourceArray2[35] = (byte) 160 /*0xA0*/;
    sourceArray2[19] = (byte) 140;
    sourceArray2[9] = (byte) 89;
    sourceArray2[38] = (byte) 133;
    sourceArray2[39] = (byte) 146;
    sourceArray2[27] = (byte) 134;
    sourceArray2[41] = (byte) 129;
    sourceArray2[34] = (byte) 142;
    sourceArray2[3] = (byte) 188;
    sourceArray2[16 /*0x10*/] = (byte) 75;
    sourceArray2[10] = (byte) 62;
    sourceArray2[46] = (byte) 115;
    sourceArray2[47] = (byte) 130;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13604()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[107];
      byte[] numArray2 = new byte[55]
      {
        (byte) 29,
        (byte) 169,
        (byte) 226,
        (byte) 103,
        (byte) 228,
        (byte) 222,
        (byte) 92,
        (byte) 91,
        (byte) 38,
        (byte) 222,
        (byte) 204,
        (byte) 69,
        (byte) 97,
        (byte) 127 /*0x7F*/,
        (byte) 212,
        (byte) 245,
        (byte) 160 /*0xA0*/,
        (byte) 140,
        (byte) 84,
        (byte) 72,
        (byte) 152,
        (byte) 220,
        (byte) 170,
        (byte) 138,
        (byte) 205,
        (byte) 41,
        (byte) 238,
        (byte) 127 /*0x7F*/,
        (byte) 223,
        (byte) 3,
        (byte) 225,
        (byte) 221,
        (byte) 189,
        (byte) 112 /*0x70*/,
        (byte) 227,
        (byte) 0,
        (byte) 125,
        (byte) 17,
        (byte) 3,
        (byte) 254,
        (byte) 76,
        (byte) 86,
        (byte) 214,
        (byte) 108,
        (byte) 138,
        (byte) 129,
        (byte) 163,
        (byte) 239,
        (byte) 74,
        (byte) 150,
        (byte) 242,
        (byte) 227,
        (byte) 192 /*0xC0*/,
        (byte) 120,
        (byte) 139
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 248,
        (byte) 123,
        (byte) 109,
        (byte) 134,
        (byte) 61,
        (byte) 158,
        (byte) 180,
        (byte) 97,
        (byte) 148,
        (byte) 111,
        (byte) 51,
        (byte) 48 /*0x30*/,
        (byte) 207,
        (byte) 7,
        (byte) 35,
        (byte) 23,
        (byte) 95,
        (byte) 223,
        (byte) 33,
        (byte) 15,
        (byte) 3,
        (byte) 20,
        (byte) 146,
        (byte) 22,
        (byte) 167,
        (byte) 148,
        (byte) 164,
        (byte) 250,
        (byte) 31 /*0x1F*/,
        (byte) 201,
        (byte) 30,
        (byte) 192 /*0xC0*/,
        (byte) 240 /*0xF0*/,
        (byte) 202,
        (byte) 181,
        (byte) 107,
        (byte) 202,
        (byte) 113,
        (byte) 38,
        (byte) 217,
        (byte) 34,
        (byte) 100,
        (byte) 12,
        (byte) 153,
        (byte) 155,
        (byte) 16 /*0x10*/,
        (byte) 178,
        (byte) 84,
        (byte) 84,
        (byte) 55,
        (byte) 173,
        (byte) 158,
        (byte) 20,
        (byte) 89,
        (byte) 101
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[52]
      {
        (byte) 88,
        (byte) 154,
        (byte) 187,
        (byte) 240 /*0xF0*/,
        (byte) 220,
        (byte) 124,
        (byte) 35,
        (byte) 253,
        (byte) 239,
        (byte) 136,
        (byte) 240 /*0xF0*/,
        (byte) 155,
        (byte) 212,
        (byte) 119,
        (byte) 142,
        (byte) 136,
        (byte) 86,
        (byte) 251,
        (byte) 66,
        (byte) 6,
        (byte) 61,
        byte.MaxValue,
        (byte) 75,
        (byte) 133,
        (byte) 254,
        (byte) 52,
        (byte) 72,
        (byte) 17,
        (byte) 254,
        (byte) 28,
        (byte) 69,
        (byte) 229,
        (byte) 162,
        (byte) 1,
        (byte) 36,
        (byte) 119,
        (byte) 72,
        (byte) 14,
        (byte) 111,
        (byte) 217,
        (byte) 179,
        (byte) 111,
        (byte) 154,
        (byte) 249,
        (byte) 178,
        (byte) 83,
        (byte) 118,
        (byte) 30,
        (byte) 99,
        (byte) 91,
        (byte) 106,
        (byte) 124
      };
      byte[] numArray5 = new byte[52]
      {
        (byte) 206,
        (byte) 69,
        (byte) 199,
        (byte) 242,
        (byte) 56,
        (byte) 168,
        (byte) 166,
        (byte) 90,
        (byte) 180,
        (byte) 14,
        (byte) 142,
        (byte) 99,
        (byte) 164,
        (byte) 59,
        (byte) 240 /*0xF0*/,
        (byte) 113,
        (byte) 202,
        (byte) 227,
        (byte) 10,
        (byte) 185,
        (byte) 47,
        (byte) 207,
        (byte) 157,
        (byte) 237,
        (byte) 11,
        (byte) 147,
        (byte) 142,
        (byte) 194,
        (byte) 30,
        (byte) 22,
        (byte) 123,
        (byte) 41,
        (byte) 64 /*0x40*/,
        (byte) 122,
        (byte) 195,
        (byte) 173,
        (byte) 116,
        (byte) 92,
        (byte) 50,
        (byte) 6,
        (byte) 71,
        (byte) 91,
        (byte) 33,
        (byte) 212,
        (byte) 69,
        (byte) 55,
        (byte) 150,
        (byte) 121,
        (byte) 137,
        (byte) 7,
        (byte) 21,
        (byte) 180
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[15];
      byte[] response = new byte[15];
      Array.Copy((Array) sc_13578.sspq, 373, (Array) numArray6, 0, 15);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13578.sspr, 373, (Array) numArray6, 0, 15);
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
    byte[] numArray7 = new byte[107];
    byte[] numArray8 = new byte[55]
    {
      (byte) 71,
      (byte) 139,
      (byte) 105,
      (byte) 82,
      (byte) 206,
      (byte) 100,
      (byte) 82,
      (byte) 211,
      (byte) 170,
      (byte) 189,
      (byte) 41,
      (byte) 173,
      (byte) 162,
      (byte) 178,
      (byte) 200,
      (byte) 157,
      (byte) 103,
      (byte) 63 /*0x3F*/,
      (byte) 203,
      (byte) 40,
      (byte) 19,
      (byte) 28,
      (byte) 199,
      (byte) 165,
      (byte) 161,
      (byte) 111,
      (byte) 199,
      (byte) 182,
      (byte) 167,
      (byte) 90,
      (byte) 163,
      (byte) 203,
      (byte) 75,
      (byte) 27,
      (byte) 34,
      (byte) 241,
      (byte) 223,
      (byte) 232,
      (byte) 243,
      (byte) 244,
      (byte) 251,
      (byte) 98,
      (byte) 70,
      (byte) 234,
      (byte) 9,
      (byte) 100,
      (byte) 92,
      (byte) 33,
      (byte) 122,
      (byte) 109,
      (byte) 145,
      (byte) 147,
      (byte) 135,
      (byte) 239,
      (byte) 43
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 105,
      (byte) 201,
      (byte) 84,
      (byte) 32 /*0x20*/,
      (byte) 157,
      (byte) 167,
      (byte) 142,
      (byte) 230,
      (byte) 60,
      (byte) 21,
      (byte) 243,
      (byte) 156,
      (byte) 220,
      (byte) 8,
      (byte) 123,
      (byte) 173,
      (byte) 230,
      (byte) 179,
      (byte) 106,
      (byte) 7,
      (byte) 177,
      (byte) 111,
      (byte) 145,
      (byte) 112 /*0x70*/,
      (byte) 248,
      (byte) 43,
      (byte) 210,
      (byte) 143,
      (byte) 202,
      (byte) 37,
      (byte) 170,
      (byte) 189,
      (byte) 104,
      (byte) 29,
      (byte) 52,
      (byte) 245,
      (byte) 160 /*0xA0*/,
      (byte) 217,
      (byte) 232,
      (byte) 2,
      (byte) 29,
      (byte) 79,
      (byte) 120,
      (byte) 174,
      (byte) 162,
      (byte) 13,
      (byte) 216,
      (byte) 200,
      (byte) 114,
      (byte) 83,
      (byte) 118,
      (byte) 206,
      (byte) 55,
      (byte) 62,
      (byte) 211
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[52]
    {
      (byte) 195,
      (byte) 177,
      (byte) 94,
      (byte) 156,
      (byte) 155,
      (byte) 43,
      (byte) 80 /*0x50*/,
      (byte) 26,
      (byte) 94,
      (byte) 98,
      (byte) 135,
      (byte) 143,
      (byte) 215,
      (byte) 190,
      (byte) 127 /*0x7F*/,
      (byte) 75,
      (byte) 215,
      (byte) 45,
      (byte) 177,
      (byte) 214,
      (byte) 221,
      (byte) 155,
      (byte) 60,
      (byte) 18,
      (byte) 166,
      (byte) 60,
      (byte) 57,
      (byte) 201,
      (byte) 16 /*0x10*/,
      (byte) 67,
      (byte) 92,
      (byte) 240 /*0xF0*/,
      (byte) 247,
      (byte) 45,
      (byte) 27,
      (byte) 54,
      (byte) 149,
      (byte) 87,
      (byte) 96 /*0x60*/,
      (byte) 54,
      (byte) 246,
      (byte) 208 /*0xD0*/,
      (byte) 171,
      (byte) 2,
      (byte) 213,
      (byte) 145,
      (byte) 162,
      (byte) 175,
      (byte) 18,
      (byte) 71,
      (byte) 176 /*0xB0*/,
      (byte) 194
    };
    byte[] numArray11 = new byte[52];
    numArray11[42] = (byte) 155;
    numArray11[1] = byte.MaxValue;
    numArray11[29] = (byte) 200;
    numArray11[51] = (byte) 193;
    numArray11[4] = (byte) 172;
    numArray11[35] = (byte) 88;
    numArray11[26] = (byte) 188;
    numArray11[49] = (byte) 253;
    numArray11[8] = (byte) 17;
    numArray11[9] = (byte) 228;
    numArray11[10] = (byte) 197;
    numArray11[21] = (byte) 201;
    numArray11[12] = (byte) 167;
    numArray11[13] = (byte) 75;
    numArray11[22] = (byte) 43;
    numArray11[11] = (byte) 38;
    numArray11[6] = (byte) 133;
    numArray11[17] = (byte) 16 /*0x10*/;
    numArray11[18] = (byte) 146;
    numArray11[36] = (byte) 4;
    numArray11[43] = (byte) 83;
    numArray11[7] = (byte) 175;
    numArray11[33] = (byte) 118;
    numArray11[23] = (byte) 73;
    numArray11[24] = (byte) 227;
    numArray11[25] = (byte) 62;
    numArray11[3] = (byte) 125;
    numArray11[27] = byte.MaxValue;
    numArray11[38] = (byte) 184;
    numArray11[45] = (byte) 49;
    numArray11[30] = (byte) 206;
    numArray11[34] = (byte) 110;
    numArray11[2] = (byte) 28;
    numArray11[0] = (byte) 94;
    numArray11[32 /*0x20*/] = (byte) 78;
    numArray11[28] = (byte) 221;
    numArray11[5] = (byte) 25;
    numArray11[37] = (byte) 81;
    numArray11[16 /*0x10*/] = (byte) 122;
    numArray11[48 /*0x30*/] = (byte) 177;
    numArray11[40] = (byte) 175;
    numArray11[19] = (byte) 168;
    numArray11[14] = (byte) 178;
    numArray11[31 /*0x1F*/] = (byte) 164;
    numArray11[15] = (byte) 183;
    numArray11[44] = (byte) 46;
    numArray11[46] = (byte) 195;
    numArray11[47] = (byte) 105;
    numArray11[41] = (byte) 87;
    numArray11[39] = (byte) 85;
    numArray11[50] = (byte) 128 /*0x80*/;
    numArray11[20] = (byte) 136;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 52);
    for (int index = 0; index < 52; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13605()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[47];
      byte[] numArray2 = new byte[47];
      numArray2[8] = (byte) 207;
      numArray2[10] = (byte) 135;
      numArray2[32 /*0x20*/] = (byte) 165;
      numArray2[29] = (byte) 237;
      numArray2[1] = (byte) 218;
      numArray2[41] = (byte) 220;
      numArray2[6] = (byte) 196;
      numArray2[7] = (byte) 104;
      numArray2[27] = (byte) 160 /*0xA0*/;
      numArray2[9] = (byte) 64 /*0x40*/;
      numArray2[28] = (byte) 124;
      numArray2[19] = (byte) 203;
      numArray2[2] = (byte) 55;
      numArray2[3] = (byte) 58;
      numArray2[14] = (byte) 99;
      numArray2[40] = (byte) 60;
      numArray2[16 /*0x10*/] = (byte) 134;
      numArray2[33] = (byte) 118;
      numArray2[23] = (byte) 95;
      numArray2[31 /*0x1F*/] = (byte) 97;
      numArray2[20] = (byte) 53;
      numArray2[21] = (byte) 111;
      numArray2[13] = (byte) 245;
      numArray2[24] = (byte) 62;
      numArray2[22] = (byte) 70;
      numArray2[25] = (byte) 254;
      numArray2[11] = (byte) 107;
      numArray2[12] = (byte) 32 /*0x20*/;
      numArray2[0] = (byte) 117;
      numArray2[17] = (byte) 109;
      numArray2[4] = (byte) 14;
      numArray2[46] = (byte) 30;
      numArray2[30] = (byte) 56;
      numArray2[26] = (byte) 27;
      numArray2[34] = (byte) 45;
      numArray2[35] = (byte) 108;
      numArray2[36] = (byte) 188;
      numArray2[37] = (byte) 135;
      numArray2[38] = (byte) 58;
      numArray2[5] = (byte) 224 /*0xE0*/;
      numArray2[18] = (byte) 29;
      numArray2[39] = (byte) 129;
      numArray2[42] = (byte) 160 /*0xA0*/;
      numArray2[43] = (byte) 134;
      numArray2[44] = (byte) 9;
      numArray2[45] = (byte) 144 /*0x90*/;
      numArray2[15] = (byte) 174;
      byte[] numArray3 = new byte[47]
      {
        (byte) 1,
        (byte) 119,
        (byte) 120,
        (byte) 45,
        (byte) 248,
        (byte) 217,
        (byte) 172,
        (byte) 230,
        (byte) 36,
        (byte) 22,
        (byte) 208 /*0xD0*/,
        (byte) 207,
        (byte) 118,
        (byte) 184,
        (byte) 239,
        (byte) 138,
        (byte) 213,
        (byte) 164,
        (byte) 186,
        (byte) 167,
        (byte) 150,
        (byte) 140,
        (byte) 225,
        (byte) 163,
        (byte) 231,
        (byte) 239,
        (byte) 234,
        (byte) 12,
        (byte) 111,
        (byte) 90,
        (byte) 116,
        (byte) 139,
        (byte) 176 /*0xB0*/,
        (byte) 157,
        (byte) 114,
        (byte) 77,
        (byte) 60,
        (byte) 48 /*0x30*/,
        (byte) 114,
        (byte) 4,
        (byte) 131,
        (byte) 230,
        (byte) 46,
        (byte) 44,
        (byte) 30,
        (byte) 94,
        (byte) 38
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
      (byte) 189,
      (byte) 126,
      (byte) 227,
      (byte) 202,
      (byte) 117,
      (byte) 59,
      (byte) 158,
      (byte) 108,
      (byte) 67,
      (byte) 240 /*0xF0*/,
      (byte) 118,
      (byte) 154,
      (byte) 112 /*0x70*/,
      (byte) 54,
      (byte) 212,
      (byte) 130,
      (byte) 79,
      (byte) 49,
      (byte) 172,
      (byte) 25,
      (byte) 205,
      (byte) 56,
      (byte) 241,
      (byte) 215,
      (byte) 74,
      (byte) 251,
      (byte) 161,
      (byte) 31 /*0x1F*/,
      (byte) 14,
      (byte) 117,
      (byte) 7,
      (byte) 224 /*0xE0*/,
      (byte) 118,
      (byte) 103,
      (byte) 18,
      (byte) 64 /*0x40*/,
      (byte) 114,
      (byte) 226,
      (byte) 105,
      (byte) 220,
      (byte) 109,
      (byte) 177,
      (byte) 103,
      (byte) 36,
      (byte) 215,
      (byte) 224 /*0xE0*/,
      (byte) 60
    };
    byte[] numArray6 = new byte[47];
    numArray6[13] = (byte) 141;
    numArray6[1] = (byte) 64 /*0x40*/;
    numArray6[31 /*0x1F*/] = (byte) 114;
    numArray6[27] = (byte) 173;
    numArray6[42] = (byte) 174;
    numArray6[5] = (byte) 139;
    numArray6[23] = (byte) 233;
    numArray6[7] = (byte) 82;
    numArray6[37] = (byte) 66;
    numArray6[18] = (byte) 40;
    numArray6[8] = (byte) 101;
    numArray6[21] = (byte) 108;
    numArray6[32 /*0x20*/] = (byte) 142;
    numArray6[0] = (byte) 204;
    numArray6[14] = (byte) 228;
    numArray6[20] = (byte) 130;
    numArray6[9] = (byte) 165;
    numArray6[44] = (byte) 217;
    numArray6[28] = (byte) 110;
    numArray6[33] = (byte) 194;
    numArray6[4] = (byte) 64 /*0x40*/;
    numArray6[29] = (byte) 9;
    numArray6[22] = (byte) 67;
    numArray6[25] = (byte) 77;
    numArray6[19] = (byte) 221;
    numArray6[6] = (byte) 221;
    numArray6[26] = (byte) 161;
    numArray6[16 /*0x10*/] = (byte) 95;
    numArray6[2] = (byte) 55;
    numArray6[3] = (byte) 62;
    numArray6[30] = (byte) 72;
    numArray6[11] = (byte) 230;
    numArray6[10] = (byte) 117;
    numArray6[34] = (byte) 250;
    numArray6[45] = (byte) 155;
    numArray6[35] = (byte) 167;
    numArray6[36] = (byte) 178;
    numArray6[24] = (byte) 118;
    numArray6[38] = (byte) 223;
    numArray6[39] = (byte) 94;
    numArray6[40] = (byte) 134;
    numArray6[41] = (byte) 79;
    numArray6[17] = (byte) 144 /*0x90*/;
    numArray6[43] = (byte) 201;
    numArray6[15] = byte.MaxValue;
    numArray6[12] = (byte) 183;
    numArray6[46] = (byte) 173;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 47);
    for (int index = 0; index < 47; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13606()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21];
      numArray2[13] = (byte) 191;
      numArray2[1] = (byte) 64 /*0x40*/;
      numArray2[5] = (byte) 112 /*0x70*/;
      numArray2[3] = (byte) 52;
      numArray2[16 /*0x10*/] = (byte) 186;
      numArray2[0] = (byte) 59;
      numArray2[2] = (byte) 189;
      numArray2[7] = (byte) 205;
      numArray2[11] = (byte) 7;
      numArray2[12] = (byte) 180;
      numArray2[8] = (byte) 49;
      numArray2[9] = (byte) 62;
      numArray2[19] = (byte) 175;
      numArray2[4] = (byte) 205;
      numArray2[14] = (byte) 11;
      numArray2[15] = (byte) 22;
      numArray2[10] = (byte) 83;
      numArray2[17] = (byte) 234;
      numArray2[6] = (byte) 81;
      numArray2[18] = (byte) 98;
      numArray2[20] = (byte) 147;
      byte[] numArray3 = new byte[21]
      {
        (byte) 220,
        (byte) 33,
        (byte) 158,
        (byte) 181,
        (byte) 106,
        (byte) 244,
        (byte) 3,
        (byte) 66,
        (byte) 90,
        (byte) 89,
        (byte) 33,
        (byte) 133,
        (byte) 107,
        (byte) 46,
        (byte) 61,
        (byte) 157,
        (byte) 33,
        (byte) 141,
        (byte) 100,
        (byte) 155,
        (byte) 25
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[21];
    byte[] numArray5 = new byte[21];
    numArray5[1] = (byte) 215;
    numArray5[4] = (byte) 75;
    numArray5[0] = (byte) 60;
    numArray5[3] = (byte) 90;
    numArray5[2] = (byte) 243;
    numArray5[5] = (byte) 53;
    numArray5[6] = (byte) 88;
    numArray5[19] = (byte) 176 /*0xB0*/;
    numArray5[7] = (byte) 139;
    numArray5[10] = (byte) 96 /*0x60*/;
    numArray5[11] = (byte) 123;
    numArray5[17] = (byte) 195;
    numArray5[12] = (byte) 17;
    numArray5[16 /*0x10*/] = (byte) 167;
    numArray5[14] = (byte) 53;
    numArray5[15] = (byte) 144 /*0x90*/;
    numArray5[8] = (byte) 164;
    numArray5[9] = (byte) 6;
    numArray5[13] = (byte) 111;
    numArray5[18] = (byte) 90;
    numArray5[20] = (byte) 149;
    byte[] numArray6 = new byte[21]
    {
      (byte) 193,
      (byte) 74,
      (byte) 242,
      (byte) 69,
      (byte) 150,
      (byte) 233,
      (byte) 0,
      (byte) 86,
      (byte) 196,
      (byte) 82,
      (byte) 246,
      (byte) 21,
      (byte) 37,
      (byte) 174,
      (byte) 205,
      (byte) 144 /*0x90*/,
      (byte) 205,
      (byte) 228,
      (byte) 139,
      (byte) 140,
      (byte) 82
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13607()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 129;
      numArray2[1] = (byte) 27;
      numArray2[2] = (byte) 172;
      numArray2[3] = (byte) 26;
      numArray2[9] = (byte) 252;
      numArray2[4] = (byte) 180;
      numArray2[8] = (byte) 143;
      numArray2[7] = (byte) 225;
      numArray2[0] = (byte) 155;
      numArray2[5] = (byte) 169;
      byte[] numArray3 = new byte[10]
      {
        (byte) 69,
        (byte) 247,
        (byte) 170,
        (byte) 211,
        (byte) 203,
        (byte) 64 /*0x40*/,
        (byte) 137,
        (byte) 28,
        (byte) 235,
        (byte) 103
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
      (byte) 118,
      (byte) 164,
      (byte) 88,
      (byte) 4,
      (byte) 184,
      (byte) 123,
      (byte) 99,
      (byte) 28,
      (byte) 77,
      (byte) 253
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 139,
      (byte) 209,
      (byte) 205,
      (byte) 44,
      (byte) 90,
      (byte) 95,
      (byte) 130,
      (byte) 4,
      (byte) 74,
      (byte) 125
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13608()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[251];
      byte[] numArray2 = new byte[55]
      {
        (byte) 156,
        (byte) 159,
        (byte) 125,
        (byte) 124,
        (byte) 81,
        (byte) 248,
        (byte) 138,
        (byte) 101,
        (byte) 38,
        (byte) 64 /*0x40*/,
        (byte) 220,
        (byte) 155,
        (byte) 228,
        (byte) 94,
        (byte) 209,
        (byte) 81,
        (byte) 205,
        (byte) 251,
        (byte) 18,
        (byte) 51,
        (byte) 54,
        (byte) 117,
        (byte) 91,
        (byte) 164,
        (byte) 200,
        (byte) 102,
        (byte) 44,
        (byte) 224 /*0xE0*/,
        (byte) 12,
        (byte) 199,
        (byte) 5,
        (byte) 19,
        (byte) 242,
        (byte) 246,
        (byte) 54,
        (byte) 177,
        (byte) 97,
        (byte) 230,
        (byte) 247,
        (byte) 145,
        (byte) 93,
        (byte) 16 /*0x10*/,
        (byte) 126,
        (byte) 24,
        (byte) 39,
        (byte) 29,
        (byte) 150,
        (byte) 4,
        (byte) 74,
        (byte) 252,
        (byte) 47,
        (byte) 13,
        (byte) 188,
        (byte) 224 /*0xE0*/,
        (byte) 36
      };
      byte[] numArray3 = new byte[55];
      numArray3[39] = (byte) 206;
      numArray3[43] = (byte) 177;
      numArray3[21] = (byte) 169;
      numArray3[11] = (byte) 53;
      numArray3[4] = (byte) 253;
      numArray3[5] = (byte) 80 /*0x50*/;
      numArray3[6] = (byte) 206;
      numArray3[54] = (byte) 158;
      numArray3[36] = (byte) 112 /*0x70*/;
      numArray3[32 /*0x20*/] = (byte) 149;
      numArray3[12] = (byte) 236;
      numArray3[3] = (byte) 245;
      numArray3[45] = (byte) 235;
      numArray3[9] = (byte) 246;
      numArray3[10] = (byte) 167;
      numArray3[15] = (byte) 111;
      numArray3[16 /*0x10*/] = (byte) 33;
      numArray3[40] = (byte) 14;
      numArray3[44] = (byte) 128 /*0x80*/;
      numArray3[7] = (byte) 119;
      numArray3[13] = (byte) 18;
      numArray3[37] = (byte) 38;
      numArray3[22] = (byte) 119;
      numArray3[23] = (byte) 106;
      numArray3[46] = (byte) 135;
      numArray3[25] = (byte) 204;
      numArray3[26] = (byte) 195;
      numArray3[27] = (byte) 214;
      numArray3[28] = (byte) 195;
      numArray3[29] = (byte) 69;
      numArray3[30] = (byte) 148;
      numArray3[31 /*0x1F*/] = (byte) 29;
      numArray3[18] = (byte) 174;
      numArray3[33] = (byte) 154;
      numArray3[19] = (byte) 142;
      numArray3[35] = (byte) 239;
      numArray3[0] = (byte) 228;
      numArray3[20] = (byte) 82;
      numArray3[34] = (byte) 164;
      numArray3[53] = (byte) 204;
      numArray3[38] = (byte) 145;
      numArray3[2] = (byte) 192 /*0xC0*/;
      numArray3[50] = (byte) 198;
      numArray3[42] = (byte) 192 /*0xC0*/;
      numArray3[24] = (byte) 122;
      numArray3[14] = (byte) 231;
      numArray3[48 /*0x30*/] = (byte) 155;
      numArray3[47] = (byte) 110;
      numArray3[8] = (byte) 188;
      numArray3[49] = (byte) 115;
      numArray3[41] = (byte) 47;
      numArray3[51] = (byte) 178;
      numArray3[52] = (byte) 162;
      numArray3[17] = (byte) 227;
      numArray3[1] = (byte) 119;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[21] = (byte) 30;
      numArray4[3] = (byte) 165;
      numArray4[23] = (byte) 38;
      numArray4[17] = (byte) 34;
      numArray4[4] = (byte) 20;
      numArray4[5] = (byte) 73;
      numArray4[6] = (byte) 199;
      numArray4[7] = (byte) 66;
      numArray4[53] = (byte) 88;
      numArray4[35] = (byte) 42;
      numArray4[26] = (byte) 2;
      numArray4[43] = (byte) 224 /*0xE0*/;
      numArray4[45] = (byte) 198;
      numArray4[33] = (byte) 109;
      numArray4[31 /*0x1F*/] = (byte) 211;
      numArray4[15] = (byte) 247;
      numArray4[52] = (byte) 143;
      numArray4[46] = (byte) 120;
      numArray4[28] = (byte) 239;
      numArray4[19] = (byte) 215;
      numArray4[20] = (byte) 101;
      numArray4[24] = (byte) 73;
      numArray4[22] = (byte) 53;
      numArray4[13] = (byte) 102;
      numArray4[14] = (byte) 179;
      numArray4[25] = (byte) 106;
      numArray4[11] = (byte) 23;
      numArray4[9] = (byte) 149;
      numArray4[51] = (byte) 232;
      numArray4[29] = (byte) 28;
      numArray4[36] = (byte) 240 /*0xF0*/;
      numArray4[18] = (byte) 110;
      numArray4[32 /*0x20*/] = (byte) 223;
      numArray4[54] = (byte) 154;
      numArray4[34] = (byte) 45;
      numArray4[49] = (byte) 17;
      numArray4[37] = (byte) 31 /*0x1F*/;
      numArray4[39] = (byte) 109;
      numArray4[42] = (byte) 237;
      numArray4[41] = (byte) 154;
      numArray4[40] = (byte) 88;
      numArray4[38] = (byte) 158;
      numArray4[30] = (byte) 7;
      numArray4[27] = (byte) 79;
      numArray4[44] = (byte) 140;
      numArray4[0] = (byte) 91;
      numArray4[12] = (byte) 178;
      numArray4[47] = (byte) 167;
      numArray4[48 /*0x30*/] = (byte) 148;
      numArray4[1] = (byte) 8;
      numArray4[50] = (byte) 127 /*0x7F*/;
      numArray4[8] = (byte) 113;
      numArray4[10] = (byte) 28;
      numArray4[16 /*0x10*/] = (byte) 187;
      numArray4[2] = (byte) 27;
      byte[] numArray5 = new byte[55];
      numArray5[31 /*0x1F*/] = (byte) 119;
      numArray5[39] = (byte) 205;
      numArray5[33] = (byte) 181;
      numArray5[54] = (byte) 114;
      numArray5[0] = (byte) 184;
      numArray5[5] = (byte) 165;
      numArray5[27] = (byte) 174;
      numArray5[7] = (byte) 85;
      numArray5[40] = (byte) 23;
      numArray5[9] = (byte) 241;
      numArray5[10] = (byte) 204;
      numArray5[45] = (byte) 140;
      numArray5[12] = (byte) 66;
      numArray5[46] = (byte) 13;
      numArray5[22] = (byte) 241;
      numArray5[52] = (byte) 158;
      numArray5[25] = (byte) 192 /*0xC0*/;
      numArray5[17] = (byte) 120;
      numArray5[18] = (byte) 62;
      numArray5[13] = (byte) 133;
      numArray5[26] = (byte) 190;
      numArray5[21] = (byte) 117;
      numArray5[23] = (byte) 157;
      numArray5[16 /*0x10*/] = (byte) 124;
      numArray5[24] = (byte) 0;
      numArray5[29] = (byte) 159;
      numArray5[4] = (byte) 44;
      numArray5[36] = (byte) 4;
      numArray5[28] = (byte) 196;
      numArray5[30] = (byte) 79;
      numArray5[15] = (byte) 41;
      numArray5[3] = (byte) 51;
      numArray5[1] = (byte) 134;
      numArray5[19] = (byte) 110;
      numArray5[14] = (byte) 68;
      numArray5[35] = (byte) 29;
      numArray5[44] = (byte) 166;
      numArray5[37] = (byte) 118;
      numArray5[34] = (byte) 172;
      numArray5[47] = (byte) 55;
      numArray5[20] = (byte) 162;
      numArray5[41] = (byte) 167;
      numArray5[42] = (byte) 113;
      numArray5[43] = (byte) 56;
      numArray5[8] = (byte) 166;
      numArray5[32 /*0x20*/] = (byte) 210;
      numArray5[38] = (byte) 102;
      numArray5[2] = (byte) 199;
      numArray5[48 /*0x30*/] = (byte) 49;
      numArray5[49] = (byte) 190;
      numArray5[50] = (byte) 42;
      numArray5[51] = (byte) 25;
      numArray5[6] = (byte) 242;
      numArray5[53] = (byte) 137;
      numArray5[11] = (byte) 193;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 4,
        (byte) 227,
        (byte) 249,
        (byte) 253,
        (byte) 15,
        (byte) 162,
        (byte) 254,
        (byte) 183,
        (byte) 81,
        (byte) 1,
        (byte) 111,
        (byte) 99,
        (byte) 87,
        (byte) 108,
        (byte) 235,
        (byte) 83,
        (byte) 156,
        (byte) 124,
        (byte) 176 /*0xB0*/,
        (byte) 181,
        (byte) 34,
        (byte) 25,
        (byte) 22,
        (byte) 253,
        (byte) 197,
        (byte) 212,
        (byte) 212,
        (byte) 173,
        (byte) 227,
        (byte) 254,
        (byte) 116,
        (byte) 182,
        (byte) 111,
        (byte) 95,
        (byte) 154,
        (byte) 8,
        (byte) 66,
        (byte) 63 /*0x3F*/,
        (byte) 19,
        (byte) 48 /*0x30*/,
        (byte) 226,
        (byte) 42,
        (byte) 235,
        (byte) 221,
        (byte) 82,
        (byte) 195,
        (byte) 130,
        (byte) 145,
        (byte) 32 /*0x20*/,
        (byte) 130,
        (byte) 25,
        (byte) 190,
        (byte) 155,
        (byte) 191,
        (byte) 88
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 109,
        (byte) 149,
        (byte) 111,
        (byte) 78,
        (byte) 193,
        (byte) 250,
        (byte) 249,
        (byte) 79,
        (byte) 246,
        (byte) 190,
        (byte) 23,
        (byte) 208 /*0xD0*/,
        (byte) 75,
        (byte) 235,
        (byte) 123,
        (byte) 52,
        (byte) 8,
        (byte) 19,
        (byte) 110,
        (byte) 170,
        (byte) 14,
        (byte) 83,
        (byte) 242,
        (byte) 46,
        (byte) 103,
        (byte) 120,
        (byte) 16 /*0x10*/,
        (byte) 99,
        (byte) 68,
        (byte) 102,
        (byte) 118,
        (byte) 114,
        (byte) 240 /*0xF0*/,
        (byte) 35,
        (byte) 190,
        (byte) 215,
        (byte) 253,
        (byte) 237,
        (byte) 248,
        (byte) 230,
        (byte) 92,
        (byte) 53,
        (byte) 148,
        (byte) 48 /*0x30*/,
        (byte) 45,
        (byte) 233,
        (byte) 23,
        (byte) 79,
        (byte) 32 /*0x20*/,
        (byte) 177,
        (byte) 244,
        (byte) 119,
        (byte) 249,
        (byte) 233,
        (byte) 87
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 252,
        (byte) 55,
        (byte) 59,
        (byte) 205,
        (byte) 81,
        (byte) 212,
        (byte) 83,
        (byte) 140,
        (byte) 184,
        (byte) 244,
        (byte) 248,
        (byte) 210,
        (byte) 226,
        (byte) 16 /*0x10*/,
        (byte) 93,
        (byte) 104,
        (byte) 128 /*0x80*/,
        (byte) 211,
        (byte) 247,
        (byte) 45,
        (byte) 57,
        (byte) 171,
        (byte) 1,
        (byte) 81,
        (byte) 66,
        (byte) 140,
        (byte) 96 /*0x60*/,
        (byte) 229,
        (byte) 182,
        (byte) 27,
        (byte) 175,
        (byte) 7,
        (byte) 175,
        (byte) 59,
        (byte) 70,
        (byte) 7,
        (byte) 216,
        (byte) 98,
        (byte) 145,
        (byte) 86,
        (byte) 151,
        (byte) 80 /*0x50*/,
        (byte) 153,
        (byte) 207,
        (byte) 138,
        (byte) 191,
        (byte) 61,
        (byte) 113,
        (byte) 187,
        (byte) 85,
        (byte) 26,
        (byte) 236,
        (byte) 96 /*0x60*/,
        (byte) 189,
        (byte) 147
      };
      byte[] numArray9 = new byte[55];
      numArray9[48 /*0x30*/] = (byte) 188;
      numArray9[1] = (byte) 203;
      numArray9[4] = (byte) 136;
      numArray9[34] = (byte) 14;
      numArray9[29] = (byte) 139;
      numArray9[8] = (byte) 132;
      numArray9[6] = (byte) 159;
      numArray9[47] = (byte) 236;
      numArray9[28] = (byte) 193;
      numArray9[9] = (byte) 151;
      numArray9[15] = (byte) 9;
      numArray9[46] = (byte) 71;
      numArray9[12] = (byte) 197;
      numArray9[13] = (byte) 136;
      numArray9[14] = (byte) 129;
      numArray9[20] = (byte) 193;
      numArray9[52] = (byte) 40;
      numArray9[22] = (byte) 176 /*0xB0*/;
      numArray9[24] = (byte) 187;
      numArray9[19] = (byte) 65;
      numArray9[11] = (byte) 250;
      numArray9[21] = (byte) 157;
      numArray9[23] = (byte) 131;
      numArray9[37] = (byte) 207;
      numArray9[45] = (byte) 244;
      numArray9[25] = (byte) 121;
      numArray9[26] = (byte) 34;
      numArray9[27] = (byte) 23;
      numArray9[16 /*0x10*/] = (byte) 214;
      numArray9[36] = (byte) 69;
      numArray9[30] = (byte) 188;
      numArray9[31 /*0x1F*/] = (byte) 221;
      numArray9[32 /*0x20*/] = (byte) 214;
      numArray9[5] = (byte) 71;
      numArray9[53] = (byte) 107;
      numArray9[35] = (byte) 193;
      numArray9[44] = (byte) 49;
      numArray9[3] = (byte) 194;
      numArray9[38] = (byte) 133;
      numArray9[39] = (byte) 30;
      numArray9[40] = (byte) 106;
      numArray9[54] = (byte) 52;
      numArray9[42] = (byte) 158;
      numArray9[2] = (byte) 240 /*0xF0*/;
      numArray9[43] = (byte) 45;
      numArray9[0] = (byte) 224 /*0xE0*/;
      numArray9[50] = (byte) 77;
      numArray9[33] = (byte) 242;
      numArray9[18] = (byte) 80 /*0x50*/;
      numArray9[49] = (byte) 172;
      numArray9[41] = (byte) 28;
      numArray9[17] = (byte) 146;
      numArray9[7] = (byte) 238;
      numArray9[10] = (byte) 105;
      numArray9[51] = (byte) 73;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[31 /*0x1F*/]
      {
        (byte) 34,
        (byte) 8,
        (byte) 117,
        (byte) 132,
        (byte) 68,
        (byte) 117,
        (byte) 155,
        (byte) 129,
        (byte) 74,
        (byte) 49,
        byte.MaxValue,
        (byte) 213,
        (byte) 131,
        (byte) 162,
        (byte) 71,
        (byte) 117,
        (byte) 190,
        (byte) 108,
        (byte) 111,
        (byte) 20,
        (byte) 166,
        (byte) 45,
        (byte) 229,
        (byte) 184,
        (byte) 71,
        (byte) 195,
        (byte) 156,
        (byte) 4,
        (byte) 25,
        (byte) 232,
        (byte) 202
      };
      byte[] numArray11 = new byte[31 /*0x1F*/]
      {
        (byte) 100,
        (byte) 11,
        (byte) 30,
        (byte) 35,
        (byte) 148,
        (byte) 217,
        (byte) 146,
        (byte) 24,
        (byte) 57,
        (byte) 159,
        byte.MaxValue,
        (byte) 145,
        (byte) 187,
        (byte) 73,
        (byte) 123,
        (byte) 171,
        (byte) 220,
        (byte) 238,
        (byte) 44,
        (byte) 118,
        (byte) 237,
        (byte) 65,
        (byte) 146,
        (byte) 18,
        (byte) 60,
        (byte) 100,
        (byte) 233,
        (byte) 121,
        (byte) 51,
        (byte) 113,
        (byte) 75
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[251];
    byte[] numArray13 = new byte[55]
    {
      (byte) 203,
      (byte) 206,
      (byte) 86,
      (byte) 94,
      (byte) 228,
      (byte) 4,
      (byte) 97,
      (byte) 10,
      (byte) 79,
      (byte) 107,
      (byte) 150,
      (byte) 210,
      (byte) 226,
      (byte) 34,
      (byte) 180,
      (byte) 52,
      (byte) 0,
      (byte) 186,
      (byte) 59,
      (byte) 43,
      (byte) 92,
      (byte) 245,
      (byte) 34,
      (byte) 188,
      (byte) 61,
      (byte) 129,
      (byte) 217,
      (byte) 150,
      (byte) 103,
      (byte) 98,
      (byte) 103,
      (byte) 2,
      (byte) 31 /*0x1F*/,
      (byte) 181,
      (byte) 214,
      (byte) 121,
      (byte) 30,
      (byte) 96 /*0x60*/,
      (byte) 136,
      (byte) 106,
      (byte) 178,
      (byte) 156,
      (byte) 222,
      (byte) 122,
      (byte) 34,
      (byte) 116,
      (byte) 224 /*0xE0*/,
      (byte) 121,
      (byte) 79,
      (byte) 228,
      (byte) 231,
      (byte) 15,
      (byte) 193,
      (byte) 137,
      (byte) 113
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 144 /*0x90*/,
      (byte) 17,
      (byte) 131,
      (byte) 234,
      (byte) 46,
      (byte) 104,
      (byte) 71,
      (byte) 96 /*0x60*/,
      (byte) 217,
      (byte) 12,
      (byte) 72,
      (byte) 172,
      (byte) 110,
      (byte) 107,
      (byte) 5,
      (byte) 224 /*0xE0*/,
      (byte) 75,
      (byte) 40,
      (byte) 65,
      (byte) 148,
      (byte) 39,
      (byte) 206,
      (byte) 144 /*0x90*/,
      (byte) 96 /*0x60*/,
      (byte) 45,
      (byte) 230,
      (byte) 134,
      (byte) 227,
      (byte) 221,
      (byte) 4,
      (byte) 130,
      (byte) 111,
      (byte) 21,
      (byte) 69,
      (byte) 128 /*0x80*/,
      (byte) 69,
      (byte) 110,
      (byte) 97,
      (byte) 176 /*0xB0*/,
      (byte) 77,
      (byte) 122,
      (byte) 168,
      (byte) 172,
      (byte) 12,
      (byte) 225,
      (byte) 175,
      (byte) 68,
      (byte) 249,
      (byte) 147,
      (byte) 46,
      (byte) 81,
      (byte) 240 /*0xF0*/,
      (byte) 249,
      (byte) 124,
      (byte) 100
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[1] = (byte) 32 /*0x20*/;
    numArray15[34] = (byte) 133;
    numArray15[21] = (byte) 154;
    numArray15[50] = (byte) 240 /*0xF0*/;
    numArray15[4] = (byte) 202;
    numArray15[41] = (byte) 118;
    numArray15[19] = (byte) 22;
    numArray15[7] = (byte) 4;
    numArray15[29] = (byte) 65;
    numArray15[9] = (byte) 178;
    numArray15[11] = (byte) 75;
    numArray15[47] = (byte) 206;
    numArray15[17] = (byte) 121;
    numArray15[13] = (byte) 21;
    numArray15[44] = (byte) 179;
    numArray15[23] = (byte) 161;
    numArray15[15] = (byte) 50;
    numArray15[49] = (byte) 220;
    numArray15[18] = (byte) 191;
    numArray15[45] = (byte) 45;
    numArray15[20] = (byte) 168;
    numArray15[10] = (byte) 45;
    numArray15[0] = (byte) 153;
    numArray15[24] = (byte) 95;
    numArray15[32 /*0x20*/] = (byte) 0;
    numArray15[25] = (byte) 70;
    numArray15[26] = (byte) 131;
    numArray15[16 /*0x10*/] = (byte) 209;
    numArray15[14] = (byte) 159;
    numArray15[5] = (byte) 162;
    numArray15[30] = (byte) 150;
    numArray15[31 /*0x1F*/] = (byte) 67;
    numArray15[6] = (byte) 236;
    numArray15[33] = (byte) 206;
    numArray15[43] = (byte) 194;
    numArray15[35] = (byte) 64 /*0x40*/;
    numArray15[2] = (byte) 98;
    numArray15[37] = (byte) 63 /*0x3F*/;
    numArray15[38] = (byte) 231;
    numArray15[39] = (byte) 250;
    numArray15[40] = (byte) 241;
    numArray15[36] = (byte) 37;
    numArray15[54] = (byte) 2;
    numArray15[42] = (byte) 141;
    numArray15[12] = (byte) 59;
    numArray15[51] = (byte) 16 /*0x10*/;
    numArray15[46] = (byte) 20;
    numArray15[22] = (byte) 201;
    numArray15[48 /*0x30*/] = (byte) 72;
    numArray15[8] = (byte) 148;
    numArray15[3] = (byte) 43;
    numArray15[27] = (byte) 122;
    numArray15[52] = (byte) 178;
    numArray15[53] = (byte) 191;
    numArray15[28] = (byte) 87;
    byte[] numArray16 = new byte[55]
    {
      (byte) 169,
      (byte) 1,
      (byte) 11,
      (byte) 131,
      (byte) 17,
      (byte) 82,
      (byte) 101,
      (byte) 111,
      (byte) 148,
      (byte) 24,
      (byte) 102,
      (byte) 166,
      (byte) 150,
      (byte) 230,
      (byte) 157,
      (byte) 170,
      (byte) 234,
      (byte) 170,
      (byte) 135,
      (byte) 126,
      (byte) 105,
      (byte) 199,
      (byte) 99,
      (byte) 200,
      (byte) 165,
      (byte) 61,
      (byte) 120,
      (byte) 171,
      (byte) 83,
      (byte) 233,
      (byte) 214,
      (byte) 218,
      (byte) 79,
      (byte) 171,
      (byte) 162,
      (byte) 105,
      (byte) 119,
      (byte) 63 /*0x3F*/,
      (byte) 175,
      (byte) 250,
      (byte) 231,
      (byte) 197,
      (byte) 83,
      (byte) 69,
      (byte) 227,
      (byte) 55,
      (byte) 34,
      (byte) 237,
      (byte) 169,
      (byte) 103,
      (byte) 41,
      (byte) 118,
      (byte) 71,
      (byte) 176 /*0xB0*/,
      (byte) 59
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[52] = (byte) 103;
    numArray17[21] = (byte) 37;
    numArray17[2] = (byte) 81;
    numArray17[47] = (byte) 36;
    numArray17[4] = (byte) 64 /*0x40*/;
    numArray17[5] = (byte) 49;
    numArray17[44] = (byte) 4;
    numArray17[11] = (byte) 10;
    numArray17[32 /*0x20*/] = (byte) 148;
    numArray17[9] = (byte) 10;
    numArray17[41] = (byte) 15;
    numArray17[31 /*0x1F*/] = (byte) 250;
    numArray17[50] = (byte) 2;
    numArray17[40] = (byte) 56;
    numArray17[14] = (byte) 29;
    numArray17[3] = (byte) 106;
    numArray17[16 /*0x10*/] = (byte) 234;
    numArray17[18] = (byte) 104;
    numArray17[8] = (byte) 89;
    numArray17[25] = (byte) 73;
    numArray17[7] = (byte) 150;
    numArray17[23] = (byte) 139;
    numArray17[22] = (byte) 79;
    numArray17[19] = (byte) 210;
    numArray17[24] = (byte) 118;
    numArray17[17] = (byte) 166;
    numArray17[20] = (byte) 96 /*0x60*/;
    numArray17[27] = (byte) 242;
    numArray17[28] = (byte) 7;
    numArray17[33] = (byte) 152;
    numArray17[43] = (byte) 216;
    numArray17[15] = (byte) 46;
    numArray17[34] = (byte) 143;
    numArray17[10] = (byte) 162;
    numArray17[49] = (byte) 80 /*0x50*/;
    numArray17[35] = (byte) 218;
    numArray17[0] = (byte) 211;
    numArray17[29] = (byte) 164;
    numArray17[13] = (byte) 170;
    numArray17[39] = (byte) 151;
    numArray17[38] = (byte) 62;
    numArray17[12] = (byte) 119;
    numArray17[42] = (byte) 111;
    numArray17[36] = (byte) 17;
    numArray17[1] = (byte) 153;
    numArray17[45] = (byte) 51;
    numArray17[46] = (byte) 113;
    numArray17[26] = (byte) 171;
    numArray17[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray17[30] = (byte) 166;
    numArray17[37] = (byte) 112 /*0x70*/;
    numArray17[51] = (byte) 244;
    numArray17[6] = (byte) 2;
    numArray17[53] = (byte) 31 /*0x1F*/;
    numArray17[54] = (byte) 239;
    byte[] numArray18 = new byte[55]
    {
      (byte) 139,
      (byte) 121,
      (byte) 110,
      (byte) 250,
      (byte) 34,
      (byte) 87,
      (byte) 114,
      (byte) 175,
      (byte) 159,
      (byte) 32 /*0x20*/,
      (byte) 185,
      (byte) 187,
      (byte) 177,
      (byte) 175,
      (byte) 226,
      (byte) 192 /*0xC0*/,
      (byte) 22,
      (byte) 126,
      (byte) 150,
      (byte) 11,
      (byte) 26,
      (byte) 92,
      (byte) 75,
      (byte) 142,
      (byte) 149,
      (byte) 132,
      (byte) 53,
      (byte) 147,
      (byte) 25,
      (byte) 173,
      (byte) 90,
      (byte) 41,
      (byte) 158,
      (byte) 215,
      (byte) 233,
      (byte) 211,
      (byte) 83,
      (byte) 251,
      (byte) 194,
      (byte) 86,
      (byte) 170,
      (byte) 94,
      (byte) 41,
      (byte) 239,
      (byte) 195,
      (byte) 237,
      (byte) 64 /*0x40*/,
      (byte) 118,
      (byte) 186,
      (byte) 11,
      (byte) 124,
      (byte) 68,
      (byte) 247,
      (byte) 139,
      (byte) 203
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[32 /*0x20*/] = (byte) 231;
    numArray19[1] = (byte) 176 /*0xB0*/;
    numArray19[2] = (byte) 29;
    numArray19[3] = (byte) 0;
    numArray19[21] = (byte) 42;
    numArray19[8] = (byte) 20;
    numArray19[22] = (byte) 134;
    numArray19[44] = (byte) 92;
    numArray19[38] = (byte) 203;
    numArray19[9] = (byte) 1;
    numArray19[10] = (byte) 54;
    numArray19[0] = (byte) 62;
    numArray19[25] = (byte) 181;
    numArray19[36] = (byte) 42;
    numArray19[14] = (byte) 254;
    numArray19[11] = (byte) 58;
    numArray19[16 /*0x10*/] = (byte) 191;
    numArray19[17] = (byte) 113;
    numArray19[18] = (byte) 207;
    numArray19[19] = (byte) 251;
    numArray19[24] = (byte) 124;
    numArray19[7] = (byte) 176 /*0xB0*/;
    numArray19[15] = (byte) 30;
    numArray19[41] = (byte) 197;
    numArray19[40] = (byte) 166;
    numArray19[39] = (byte) 168;
    numArray19[4] = (byte) 93;
    numArray19[26] = (byte) 129;
    numArray19[28] = (byte) 37;
    numArray19[29] = (byte) 251;
    numArray19[49] = (byte) 109;
    numArray19[31 /*0x1F*/] = (byte) 10;
    numArray19[42] = (byte) 71;
    numArray19[33] = (byte) 187;
    numArray19[34] = (byte) 12;
    numArray19[35] = (byte) 203;
    numArray19[27] = (byte) 25;
    numArray19[37] = (byte) 16 /*0x10*/;
    numArray19[46] = (byte) 72;
    numArray19[53] = (byte) 253;
    numArray19[6] = (byte) 19;
    numArray19[13] = (byte) 209;
    numArray19[30] = (byte) 239;
    numArray19[43] = (byte) 186;
    numArray19[5] = (byte) 216;
    numArray19[45] = (byte) 207;
    numArray19[20] = (byte) 0;
    numArray19[47] = (byte) 98;
    numArray19[48 /*0x30*/] = (byte) 138;
    numArray19[23] = (byte) 69;
    numArray19[50] = (byte) 215;
    numArray19[51] = (byte) 19;
    numArray19[52] = (byte) 206;
    numArray19[54] = (byte) 211;
    numArray19[12] = (byte) 179;
    byte[] numArray20 = new byte[55]
    {
      (byte) 185,
      (byte) 179,
      (byte) 53,
      (byte) 183,
      (byte) 215,
      (byte) 127 /*0x7F*/,
      (byte) 150,
      (byte) 120,
      (byte) 211,
      (byte) 253,
      (byte) 233,
      (byte) 189,
      (byte) 49,
      (byte) 123,
      (byte) 242,
      (byte) 195,
      (byte) 194,
      (byte) 141,
      (byte) 122,
      (byte) 210,
      (byte) 47,
      (byte) 78,
      (byte) 16 /*0x10*/,
      (byte) 243,
      (byte) 1,
      (byte) 46,
      (byte) 220,
      (byte) 192 /*0xC0*/,
      (byte) 235,
      (byte) 213,
      (byte) 119,
      (byte) 113,
      (byte) 209,
      (byte) 114,
      (byte) 160 /*0xA0*/,
      (byte) 66,
      (byte) 252,
      (byte) 36,
      (byte) 238,
      (byte) 49,
      (byte) 161,
      (byte) 42,
      (byte) 107,
      (byte) 104,
      (byte) 161,
      (byte) 220,
      (byte) 138,
      (byte) 64 /*0x40*/,
      (byte) 165,
      (byte) 159,
      (byte) 164,
      (byte) 127 /*0x7F*/,
      (byte) 152,
      (byte) 85,
      (byte) 3
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[31 /*0x1F*/];
    numArray21[22] = (byte) 48 /*0x30*/;
    numArray21[0] = (byte) 238;
    numArray21[20] = (byte) 35;
    numArray21[3] = (byte) 55;
    numArray21[14] = (byte) 33;
    numArray21[5] = (byte) 210;
    numArray21[6] = (byte) 245;
    numArray21[7] = (byte) 54;
    numArray21[4] = (byte) 106;
    numArray21[9] = (byte) 104;
    numArray21[15] = (byte) 130;
    numArray21[30] = (byte) 66;
    numArray21[12] = (byte) 145;
    numArray21[13] = (byte) 161;
    numArray21[10] = (byte) 16 /*0x10*/;
    numArray21[24] = (byte) 128 /*0x80*/;
    numArray21[28] = (byte) 110;
    numArray21[26] = (byte) 62;
    numArray21[1] = (byte) 214;
    numArray21[19] = (byte) 180;
    numArray21[21] = (byte) 185;
    numArray21[2] = (byte) 58;
    numArray21[23] = (byte) 28;
    numArray21[11] = (byte) 204;
    numArray21[8] = (byte) 240 /*0xF0*/;
    numArray21[25] = (byte) 55;
    numArray21[16 /*0x10*/] = (byte) 213;
    numArray21[27] = (byte) 251;
    numArray21[18] = (byte) 0;
    numArray21[29] = (byte) 148;
    numArray21[17] = (byte) 169;
    byte[] numArray22 = new byte[31 /*0x1F*/]
    {
      (byte) 203,
      (byte) 31 /*0x1F*/,
      (byte) 187,
      (byte) 180,
      (byte) 63 /*0x3F*/,
      (byte) 108,
      (byte) 191,
      (byte) 92,
      (byte) 116,
      (byte) 40,
      (byte) 41,
      (byte) 213,
      (byte) 83,
      (byte) 254,
      (byte) 224 /*0xE0*/,
      (byte) 154,
      (byte) 226,
      (byte) 65,
      (byte) 32 /*0x20*/,
      (byte) 64 /*0x40*/,
      (byte) 166,
      (byte) 102,
      (byte) 201,
      (byte) 35,
      (byte) 40,
      (byte) 94,
      (byte) 93,
      byte.MaxValue,
      (byte) 191,
      (byte) 11,
      (byte) 141
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[35];
    byte[] response = new byte[35];
    Array.Copy((Array) sc_13578.sspq, 388, (Array) numArray23, 0, 35);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13578.sspr, 388, (Array) numArray23, 0, 35);
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

  internal static int ssp_appserver_13609(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 80 /*0x50*/;
    sourceArray1[2] = (byte) 20;
    sourceArray1[6] = (byte) 136;
    sourceArray1[38] = (byte) 186;
    sourceArray1[4] = (byte) 254;
    sourceArray1[18] = (byte) 185;
    sourceArray1[10] = (byte) 7;
    sourceArray1[16 /*0x10*/] = (byte) 225;
    sourceArray1[8] = (byte) 137;
    sourceArray1[9] = (byte) 65;
    sourceArray1[22] = (byte) 205;
    sourceArray1[20] = (byte) 16 /*0x10*/;
    sourceArray1[12] = (byte) 44;
    sourceArray1[5] = (byte) 1;
    sourceArray1[24] = (byte) 89;
    sourceArray1[35] = (byte) 52;
    sourceArray1[47] = (byte) 58;
    sourceArray1[17] = (byte) 7;
    sourceArray1[11] = (byte) 193;
    sourceArray1[36] = (byte) 175;
    sourceArray1[0] = (byte) 155;
    sourceArray1[21] = (byte) 90;
    sourceArray1[13] = (byte) 5;
    sourceArray1[28] = (byte) 147;
    sourceArray1[44] = (byte) 91;
    sourceArray1[25] = (byte) 195;
    sourceArray1[26] = (byte) 59;
    sourceArray1[33] = (byte) 205;
    sourceArray1[40] = (byte) 127 /*0x7F*/;
    sourceArray1[29] = (byte) 46;
    sourceArray1[30] = (byte) 93;
    sourceArray1[31 /*0x1F*/] = (byte) 226;
    sourceArray1[32 /*0x20*/] = (byte) 250;
    sourceArray1[3] = (byte) 150;
    sourceArray1[34] = (byte) 88;
    sourceArray1[15] = (byte) 151;
    sourceArray1[46] = (byte) 168;
    sourceArray1[37] = (byte) 173;
    sourceArray1[23] = (byte) 88;
    sourceArray1[39] = (byte) 73;
    sourceArray1[14] = (byte) 67;
    sourceArray1[41] = (byte) 113;
    sourceArray1[42] = (byte) 135;
    sourceArray1[43] = (byte) 138;
    sourceArray1[19] = (byte) 71;
    sourceArray1[45] = (byte) 77;
    sourceArray1[7] = (byte) 188;
    sourceArray1[27] = (byte) 9;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = byte.MaxValue;
    sourceArray2[11] = (byte) 88;
    sourceArray2[2] = (byte) 82;
    sourceArray2[26] = (byte) 120;
    sourceArray2[4] = (byte) 238;
    sourceArray2[42] = (byte) 237;
    sourceArray2[43] = (byte) 230;
    sourceArray2[39] = (byte) 24;
    sourceArray2[8] = (byte) 49;
    sourceArray2[34] = (byte) 100;
    sourceArray2[10] = (byte) 133;
    sourceArray2[21] = (byte) 8;
    sourceArray2[12] = (byte) 217;
    sourceArray2[18] = (byte) 195;
    sourceArray2[27] = (byte) 141;
    sourceArray2[15] = (byte) 96 /*0x60*/;
    sourceArray2[16 /*0x10*/] = (byte) 148;
    sourceArray2[17] = (byte) 215;
    sourceArray2[9] = (byte) 118;
    sourceArray2[40] = (byte) 21;
    sourceArray2[22] = (byte) 168;
    sourceArray2[14] = (byte) 219;
    sourceArray2[3] = (byte) 191;
    sourceArray2[23] = (byte) 79;
    sourceArray2[24] = (byte) 98;
    sourceArray2[25] = (byte) 113;
    sourceArray2[41] = (byte) 112 /*0x70*/;
    sourceArray2[35] = (byte) 20;
    sourceArray2[45] = (byte) 134;
    sourceArray2[29] = (byte) 178;
    sourceArray2[30] = (byte) 191;
    sourceArray2[31 /*0x1F*/] = (byte) 120;
    sourceArray2[32 /*0x20*/] = (byte) 66;
    sourceArray2[1] = (byte) 55;
    sourceArray2[28] = (byte) 223;
    sourceArray2[13] = (byte) 91;
    sourceArray2[6] = (byte) 219;
    sourceArray2[37] = (byte) 32 /*0x20*/;
    sourceArray2[7] = (byte) 206;
    sourceArray2[0] = (byte) 185;
    sourceArray2[19] = (byte) 62;
    sourceArray2[38] = (byte) 195;
    sourceArray2[33] = (byte) 122;
    sourceArray2[20] = (byte) 175;
    sourceArray2[44] = (byte) 12;
    sourceArray2[36] = (byte) 179;
    sourceArray2[46] = (byte) 218;
    sourceArray2[5] = (byte) 98;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13610()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[36] = (byte) 163;
      numArray2[1] = (byte) 188;
      numArray2[13] = (byte) 93;
      numArray2[17] = (byte) 198;
      numArray2[4] = (byte) 244;
      numArray2[41] = (byte) 249;
      numArray2[2] = (byte) 36;
      numArray2[7] = (byte) 139;
      numArray2[51] = (byte) 226;
      numArray2[9] = (byte) 248;
      numArray2[39] = (byte) 243;
      numArray2[11] = (byte) 112 /*0x70*/;
      numArray2[24] = (byte) 2;
      numArray2[14] = (byte) 181;
      numArray2[37] = (byte) 241;
      numArray2[40] = (byte) 231;
      numArray2[16 /*0x10*/] = (byte) 147;
      numArray2[15] = (byte) 160 /*0xA0*/;
      numArray2[52] = (byte) 207;
      numArray2[27] = (byte) 42;
      numArray2[20] = (byte) 171;
      numArray2[30] = (byte) 235;
      numArray2[18] = (byte) 162;
      numArray2[23] = (byte) 142;
      numArray2[50] = (byte) 197;
      numArray2[10] = (byte) 250;
      numArray2[26] = (byte) 66;
      numArray2[3] = (byte) 100;
      numArray2[28] = (byte) 151;
      numArray2[47] = (byte) 94;
      numArray2[22] = (byte) 83;
      numArray2[31 /*0x1F*/] = (byte) 234;
      numArray2[32 /*0x20*/] = (byte) 192 /*0xC0*/;
      numArray2[42] = (byte) 163;
      numArray2[34] = (byte) 107;
      numArray2[35] = (byte) 49;
      numArray2[29] = (byte) 192 /*0xC0*/;
      numArray2[48 /*0x30*/] = (byte) 118;
      numArray2[38] = (byte) 178;
      numArray2[54] = (byte) 147;
      numArray2[33] = (byte) 133;
      numArray2[44] = (byte) 82;
      numArray2[19] = (byte) 248;
      numArray2[43] = (byte) 121;
      numArray2[6] = (byte) 223;
      numArray2[45] = (byte) 111;
      numArray2[12] = (byte) 19;
      numArray2[8] = (byte) 46;
      numArray2[0] = (byte) 97;
      numArray2[49] = (byte) 195;
      numArray2[46] = (byte) 54;
      numArray2[25] = (byte) 237;
      numArray2[21] = (byte) 75;
      numArray2[53] = (byte) 59;
      numArray2[5] = (byte) 168;
      byte[] numArray3 = new byte[55]
      {
        (byte) 189,
        (byte) 66,
        (byte) 99,
        (byte) 23,
        (byte) 61,
        (byte) 5,
        (byte) 60,
        (byte) 113,
        (byte) 231,
        (byte) 83,
        (byte) 109,
        (byte) 71,
        (byte) 87,
        (byte) 108,
        (byte) 127 /*0x7F*/,
        (byte) 218,
        (byte) 34,
        (byte) 96 /*0x60*/,
        (byte) 22,
        (byte) 191,
        (byte) 38,
        (byte) 219,
        (byte) 125,
        (byte) 72,
        (byte) 167,
        (byte) 192 /*0xC0*/,
        (byte) 35,
        (byte) 142,
        (byte) 252,
        (byte) 125,
        (byte) 222,
        (byte) 252,
        (byte) 229,
        (byte) 144 /*0x90*/,
        (byte) 166,
        (byte) 188,
        (byte) 173,
        (byte) 112 /*0x70*/,
        (byte) 206,
        (byte) 92,
        (byte) 161,
        (byte) 59,
        (byte) 33,
        (byte) 15,
        (byte) 29,
        (byte) 151,
        (byte) 161,
        (byte) 140,
        (byte) 180,
        (byte) 188,
        (byte) 157,
        (byte) 47,
        (byte) 227,
        (byte) 217,
        (byte) 144 /*0x90*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8]
      {
        (byte) 89,
        (byte) 206,
        (byte) 79,
        (byte) 129,
        (byte) 0,
        (byte) 31 /*0x1F*/,
        (byte) 115,
        (byte) 4
      };
      byte[] numArray5 = new byte[8];
      numArray5[3] = (byte) 65;
      numArray5[1] = (byte) 246;
      numArray5[2] = (byte) 80 /*0x50*/;
      numArray5[6] = (byte) 71;
      numArray5[4] = (byte) 6;
      numArray5[5] = (byte) 5;
      numArray5[7] = (byte) 82;
      numArray5[0] = (byte) 17;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55];
    numArray7[19] = (byte) 50;
    numArray7[26] = (byte) 209;
    numArray7[11] = (byte) 89;
    numArray7[3] = (byte) 211;
    numArray7[37] = (byte) 251;
    numArray7[5] = (byte) 119;
    numArray7[32 /*0x20*/] = (byte) 78;
    numArray7[7] = (byte) 13;
    numArray7[8] = (byte) 215;
    numArray7[44] = (byte) 251;
    numArray7[17] = (byte) 91;
    numArray7[22] = (byte) 103;
    numArray7[4] = (byte) 14;
    numArray7[13] = (byte) 232;
    numArray7[14] = (byte) 56;
    numArray7[1] = (byte) 109;
    numArray7[2] = (byte) 249;
    numArray7[10] = (byte) 249;
    numArray7[0] = (byte) 114;
    numArray7[51] = (byte) 12;
    numArray7[9] = (byte) 58;
    numArray7[31 /*0x1F*/] = (byte) 0;
    numArray7[48 /*0x30*/] = (byte) 131;
    numArray7[23] = (byte) 77;
    numArray7[18] = (byte) 12;
    numArray7[25] = (byte) 11;
    numArray7[16 /*0x10*/] = (byte) 192 /*0xC0*/;
    numArray7[6] = (byte) 193;
    numArray7[30] = (byte) 101;
    numArray7[29] = (byte) 16 /*0x10*/;
    numArray7[12] = (byte) 14;
    numArray7[42] = (byte) 156;
    numArray7[43] = (byte) 202;
    numArray7[24] = (byte) 149;
    numArray7[34] = (byte) 251;
    numArray7[35] = (byte) 33;
    numArray7[27] = (byte) 54;
    numArray7[52] = (byte) 20;
    numArray7[38] = (byte) 65;
    numArray7[15] = (byte) 179;
    numArray7[40] = (byte) 62;
    numArray7[41] = (byte) 17;
    numArray7[33] = (byte) 55;
    numArray7[21] = (byte) 194;
    numArray7[28] = (byte) 214;
    numArray7[45] = (byte) 209;
    numArray7[46] = (byte) 142;
    numArray7[47] = (byte) 178;
    numArray7[20] = (byte) 230;
    numArray7[49] = (byte) 59;
    numArray7[50] = (byte) 82;
    numArray7[36] = (byte) 95;
    numArray7[39] = (byte) 9;
    numArray7[53] = (byte) 170;
    numArray7[54] = (byte) 196;
    byte[] numArray8 = new byte[55]
    {
      (byte) 162,
      (byte) 117,
      (byte) 103,
      (byte) 131,
      (byte) 229,
      (byte) 95,
      (byte) 57,
      (byte) 193,
      (byte) 169,
      (byte) 105,
      (byte) 173,
      (byte) 109,
      (byte) 89,
      (byte) 98,
      (byte) 204,
      (byte) 137,
      (byte) 146,
      (byte) 43,
      (byte) 61,
      (byte) 26,
      (byte) 24,
      (byte) 82,
      (byte) 197,
      (byte) 198,
      (byte) 52,
      (byte) 248,
      (byte) 201,
      (byte) 177,
      (byte) 135,
      (byte) 136,
      (byte) 29,
      (byte) 134,
      (byte) 68,
      (byte) 211,
      (byte) 162,
      (byte) 20,
      (byte) 42,
      (byte) 167,
      (byte) 1,
      (byte) 237,
      (byte) 46,
      (byte) 35,
      byte.MaxValue,
      (byte) 229,
      (byte) 188,
      (byte) 136,
      (byte) 139,
      (byte) 181,
      (byte) 238,
      (byte) 221,
      (byte) 130,
      (byte) 136,
      (byte) 42,
      (byte) 202,
      (byte) 142
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8];
    numArray9[3] = (byte) 135;
    numArray9[4] = (byte) 215;
    numArray9[2] = (byte) 132;
    numArray9[7] = (byte) 124;
    numArray9[6] = (byte) 8;
    numArray9[5] = (byte) 156;
    numArray9[1] = (byte) 122;
    numArray9[0] = (byte) 90;
    byte[] numArray10 = new byte[8];
    numArray10[0] = (byte) 88;
    numArray10[1] = (byte) 97;
    numArray10[7] = (byte) 118;
    numArray10[3] = (byte) 75;
    numArray10[4] = (byte) 181;
    numArray10[5] = (byte) 171;
    numArray10[6] = (byte) 117;
    numArray10[2] = byte.MaxValue;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13611()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[21];
      byte[] numArray2 = new byte[21]
      {
        (byte) 101,
        (byte) 217,
        (byte) 237,
        (byte) 129,
        (byte) 194,
        (byte) 176 /*0xB0*/,
        (byte) 89,
        (byte) 66,
        (byte) 186,
        (byte) 220,
        (byte) 219,
        (byte) 61,
        (byte) 205,
        (byte) 120,
        (byte) 244,
        (byte) 94,
        (byte) 81,
        (byte) 37,
        (byte) 110,
        (byte) 71,
        (byte) 133
      };
      byte[] numArray3 = new byte[21];
      numArray3[5] = (byte) 237;
      numArray3[1] = (byte) 15;
      numArray3[2] = (byte) 69;
      numArray3[3] = (byte) 187;
      numArray3[7] = (byte) 128 /*0x80*/;
      numArray3[17] = (byte) 69;
      numArray3[6] = (byte) 96 /*0x60*/;
      numArray3[12] = (byte) 115;
      numArray3[8] = (byte) 84;
      numArray3[9] = (byte) 158;
      numArray3[10] = (byte) 207;
      numArray3[11] = (byte) 30;
      numArray3[4] = (byte) 105;
      numArray3[13] = (byte) 164;
      numArray3[16 /*0x10*/] = (byte) 65;
      numArray3[15] = (byte) 245;
      numArray3[0] = (byte) 59;
      numArray3[14] = (byte) 69;
      numArray3[18] = (byte) 138;
      numArray3[19] = (byte) 83;
      numArray3[20] = (byte) 254;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      byte[] response = new byte[32 /*0x20*/];
      Array.Copy((Array) sc_13578.sspq, 423, (Array) numArray4, 0, 32 /*0x20*/);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13578.sspr, 423, (Array) numArray4, 0, 32 /*0x20*/);
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
    byte[] numArray5 = new byte[21];
    byte[] numArray6 = new byte[21]
    {
      (byte) 16 /*0x10*/,
      (byte) 57,
      (byte) 182,
      (byte) 149,
      (byte) 177,
      (byte) 220,
      (byte) 71,
      (byte) 249,
      (byte) 182,
      (byte) 126,
      (byte) 44,
      (byte) 200,
      (byte) 204,
      (byte) 2,
      (byte) 87,
      (byte) 85,
      (byte) 95,
      (byte) 249,
      (byte) 254,
      (byte) 36,
      (byte) 21
    };
    byte[] numArray7 = new byte[21]
    {
      (byte) 162,
      (byte) 97,
      (byte) 34,
      (byte) 133,
      (byte) 4,
      (byte) 238,
      (byte) 40,
      (byte) 223,
      (byte) 188,
      (byte) 167,
      (byte) 186,
      (byte) 128 /*0x80*/,
      (byte) 66,
      (byte) 22,
      (byte) 128 /*0x80*/,
      (byte) 249,
      (byte) 157,
      (byte) 191,
      (byte) 34,
      (byte) 210,
      (byte) 102
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 21);
    for (int index = 0; index < 21; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13612()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 153,
        (byte) 187,
        (byte) 8,
        (byte) 237,
        (byte) 183,
        (byte) 107,
        (byte) 233,
        (byte) 87,
        (byte) 185,
        (byte) 103
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 176 /*0xB0*/,
        (byte) 170,
        (byte) 208 /*0xD0*/,
        (byte) 159,
        (byte) 31 /*0x1F*/,
        (byte) 169,
        (byte) 32 /*0x20*/,
        (byte) 248,
        (byte) 234,
        (byte) 183
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 213;
    numArray5[7] = (byte) 154;
    numArray5[1] = (byte) 233;
    numArray5[0] = (byte) 155;
    numArray5[9] = (byte) 225;
    numArray5[3] = (byte) 180;
    numArray5[6] = (byte) 202;
    numArray5[4] = (byte) 150;
    numArray5[8] = (byte) 243;
    numArray5[2] = (byte) 180;
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 173;
    numArray6[1] = (byte) 153;
    numArray6[2] = (byte) 86;
    numArray6[9] = (byte) 141;
    numArray6[4] = (byte) 60;
    numArray6[5] = (byte) 224 /*0xE0*/;
    numArray6[6] = (byte) 248;
    numArray6[7] = (byte) 54;
    numArray6[8] = (byte) 150;
    numArray6[0] = (byte) 254;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_13578.sspq, 455, (Array) numArray7, 0, 10);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13578.sspr, 455, (Array) numArray7, 0, 10);
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

  internal static string ssp_appserver_13613()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[8] = (byte) 167;
      numArray2[1] = (byte) 244;
      numArray2[4] = (byte) 133;
      numArray2[3] = (byte) 38;
      numArray2[9] = (byte) 142;
      numArray2[5] = (byte) 32 /*0x20*/;
      numArray2[0] = (byte) 252;
      numArray2[2] = (byte) 100;
      numArray2[6] = (byte) 111;
      numArray2[7] = (byte) 129;
      byte[] numArray3 = new byte[10]
      {
        (byte) 86,
        (byte) 39,
        (byte) 1,
        (byte) 215,
        (byte) 241,
        (byte) 250,
        (byte) 168,
        (byte) 132,
        (byte) 120,
        (byte) 205
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 61;
    numArray5[3] = (byte) 56;
    numArray5[2] = (byte) 98;
    numArray5[9] = (byte) 70;
    numArray5[4] = (byte) 118;
    numArray5[7] = (byte) 152;
    numArray5[6] = (byte) 104;
    numArray5[1] = (byte) 174;
    numArray5[8] = (byte) 114;
    numArray5[0] = (byte) 168;
    byte[] numArray6 = new byte[10]
    {
      (byte) 49,
      (byte) 27,
      (byte) 239,
      (byte) 242,
      (byte) 2,
      (byte) 12,
      (byte) 86,
      (byte) 141,
      (byte) 77,
      (byte) 70
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[22];
    byte[] response = new byte[22];
    Array.Copy((Array) sc_13578.sspq, 465, (Array) numArray7, 0, 22);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13578.sspr, 465, (Array) numArray7, 0, 22);
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

  internal static int ssp_appserver_13614(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 108,
      (byte) 62,
      (byte) 238,
      (byte) 26,
      (byte) 140,
      (byte) 147,
      (byte) 67,
      (byte) 206,
      (byte) 98,
      (byte) 104,
      (byte) 128 /*0x80*/,
      (byte) 139,
      (byte) 108,
      (byte) 46,
      (byte) 144 /*0x90*/,
      (byte) 173,
      (byte) 178,
      (byte) 106,
      (byte) 129,
      (byte) 210,
      (byte) 241,
      (byte) 163,
      (byte) 200,
      (byte) 9,
      (byte) 50,
      (byte) 64 /*0x40*/,
      (byte) 155,
      (byte) 130,
      (byte) 200,
      (byte) 201,
      (byte) 209,
      (byte) 30,
      (byte) 94,
      (byte) 189,
      (byte) 206,
      (byte) 201,
      (byte) 213,
      (byte) 121,
      (byte) 86,
      (byte) 250,
      (byte) 244,
      (byte) 238,
      (byte) 21,
      (byte) 185,
      (byte) 104,
      (byte) 110,
      (byte) 214,
      (byte) 17
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 79,
      (byte) 135,
      (byte) 34,
      (byte) 33,
      (byte) 120,
      (byte) 14,
      (byte) 101,
      (byte) 89,
      (byte) 240 /*0xF0*/,
      (byte) 156,
      (byte) 66,
      (byte) 103,
      (byte) 136,
      (byte) 20,
      (byte) 163,
      (byte) 93,
      (byte) 200,
      (byte) 101,
      (byte) 56,
      (byte) 191,
      byte.MaxValue,
      (byte) 82,
      (byte) 53,
      (byte) 111,
      (byte) 76,
      (byte) 40,
      (byte) 131,
      (byte) 80 /*0x50*/,
      (byte) 249,
      (byte) 226,
      (byte) 43,
      (byte) 157,
      (byte) 152,
      (byte) 107,
      (byte) 78,
      (byte) 239,
      (byte) 123,
      (byte) 226,
      (byte) 59,
      (byte) 143,
      (byte) 81,
      (byte) 36,
      (byte) 2,
      (byte) 145,
      (byte) 60,
      (byte) 172,
      (byte) 208 /*0xD0*/,
      (byte) 209
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[41];
    byte[] response2 = new byte[41];
    Array.Copy((Array) sc_13578.sspq, 487, (Array) numArray2, 0, 41);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13578.sspr, 487, (Array) numArray2, 0, 41);
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

  internal static int ssp_appserver_13615(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 232;
    sourceArray1[13] = (byte) 218;
    sourceArray1[44] = (byte) 99;
    sourceArray1[5] = (byte) 65;
    sourceArray1[4] = (byte) 6;
    sourceArray1[11] = (byte) 169;
    sourceArray1[16 /*0x10*/] = (byte) 103;
    sourceArray1[28] = (byte) 90;
    sourceArray1[8] = (byte) 159;
    sourceArray1[18] = (byte) 35;
    sourceArray1[2] = (byte) 124;
    sourceArray1[35] = (byte) 26;
    sourceArray1[12] = (byte) 143;
    sourceArray1[39] = (byte) 130;
    sourceArray1[21] = (byte) 165;
    sourceArray1[9] = (byte) 140;
    sourceArray1[10] = (byte) 150;
    sourceArray1[17] = (byte) 68;
    sourceArray1[33] = (byte) 132;
    sourceArray1[43] = (byte) 216;
    sourceArray1[20] = (byte) 109;
    sourceArray1[32 /*0x20*/] = (byte) 9;
    sourceArray1[22] = (byte) 161;
    sourceArray1[23] = (byte) 158;
    sourceArray1[24] = (byte) 130;
    sourceArray1[0] = (byte) 50;
    sourceArray1[34] = (byte) 171;
    sourceArray1[27] = (byte) 19;
    sourceArray1[40] = (byte) 66;
    sourceArray1[29] = (byte) 116;
    sourceArray1[30] = (byte) 231;
    sourceArray1[31 /*0x1F*/] = (byte) 167;
    sourceArray1[46] = (byte) 119;
    sourceArray1[25] = (byte) 90;
    sourceArray1[37] = (byte) 235;
    sourceArray1[19] = (byte) 225;
    sourceArray1[36] = (byte) 17;
    sourceArray1[3] = (byte) 35;
    sourceArray1[38] = (byte) 154;
    sourceArray1[26] = (byte) 135;
    sourceArray1[42] = (byte) 124;
    sourceArray1[41] = (byte) 216;
    sourceArray1[15] = (byte) 58;
    sourceArray1[7] = (byte) 177;
    sourceArray1[14] = (byte) 30;
    sourceArray1[45] = (byte) 107;
    sourceArray1[6] = (byte) 39;
    sourceArray1[47] = (byte) 202;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 90,
      (byte) 45,
      (byte) 197,
      (byte) 85,
      byte.MaxValue,
      (byte) 52,
      (byte) 154,
      (byte) 244,
      (byte) 100,
      byte.MaxValue,
      (byte) 93,
      (byte) 117,
      (byte) 128 /*0x80*/,
      (byte) 130,
      (byte) 153,
      (byte) 97,
      (byte) 138,
      (byte) 225,
      (byte) 47,
      (byte) 79,
      (byte) 173,
      (byte) 144 /*0x90*/,
      (byte) 244,
      (byte) 108,
      (byte) 112 /*0x70*/,
      (byte) 100,
      (byte) 227,
      (byte) 82,
      (byte) 109,
      (byte) 128 /*0x80*/,
      (byte) 224 /*0xE0*/,
      (byte) 157,
      (byte) 199,
      (byte) 66,
      (byte) 180,
      (byte) 146,
      (byte) 6,
      (byte) 240 /*0xF0*/,
      (byte) 123,
      (byte) 154,
      (byte) 22,
      (byte) 84,
      (byte) 156,
      (byte) 137,
      (byte) 2,
      (byte) 253,
      (byte) 173,
      (byte) 75
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13616(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 108,
      (byte) 154,
      (byte) 120,
      (byte) 162,
      (byte) 111,
      (byte) 2,
      (byte) 204,
      (byte) 136,
      (byte) 0,
      (byte) 100,
      (byte) 126,
      (byte) 161,
      (byte) 137,
      (byte) 201,
      (byte) 252,
      (byte) 124,
      (byte) 199,
      (byte) 162,
      (byte) 106,
      (byte) 180,
      (byte) 190,
      (byte) 27,
      (byte) 17,
      (byte) 49,
      (byte) 245,
      (byte) 48 /*0x30*/,
      (byte) 187,
      (byte) 216,
      (byte) 113,
      (byte) 18,
      (byte) 215,
      (byte) 149,
      (byte) 199,
      (byte) 75,
      (byte) 118,
      (byte) 34,
      (byte) 30,
      (byte) 236,
      (byte) 24,
      (byte) 56,
      (byte) 109,
      (byte) 88,
      (byte) 238,
      (byte) 186,
      (byte) 83,
      (byte) 145,
      (byte) 194,
      (byte) 36
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[1] = (byte) 40;
    sourceArray2[13] = (byte) 165;
    sourceArray2[36] = (byte) 214;
    sourceArray2[12] = (byte) 31 /*0x1F*/;
    sourceArray2[8] = (byte) 52;
    sourceArray2[5] = (byte) 133;
    sourceArray2[24] = (byte) 17;
    sourceArray2[40] = (byte) 175;
    sourceArray2[9] = (byte) 73;
    sourceArray2[39] = (byte) 174;
    sourceArray2[47] = (byte) 117;
    sourceArray2[11] = (byte) 113;
    sourceArray2[23] = (byte) 123;
    sourceArray2[0] = (byte) 213;
    sourceArray2[14] = (byte) 76;
    sourceArray2[37] = (byte) 212;
    sourceArray2[45] = (byte) 226;
    sourceArray2[17] = (byte) 163;
    sourceArray2[20] = (byte) 69;
    sourceArray2[4] = (byte) 187;
    sourceArray2[19] = (byte) 186;
    sourceArray2[21] = (byte) 145;
    sourceArray2[29] = (byte) 158;
    sourceArray2[10] = (byte) 210;
    sourceArray2[3] = (byte) 168;
    sourceArray2[25] = (byte) 107;
    sourceArray2[26] = (byte) 164;
    sourceArray2[27] = (byte) 122;
    sourceArray2[28] = (byte) 44;
    sourceArray2[31 /*0x1F*/] = (byte) 236;
    sourceArray2[30] = (byte) 213;
    sourceArray2[2] = (byte) 176 /*0xB0*/;
    sourceArray2[32 /*0x20*/] = (byte) 95;
    sourceArray2[33] = (byte) 249;
    sourceArray2[34] = (byte) 78;
    sourceArray2[35] = (byte) 246;
    sourceArray2[16 /*0x10*/] = (byte) 178;
    sourceArray2[15] = (byte) 12;
    sourceArray2[38] = (byte) 132;
    sourceArray2[7] = (byte) 106;
    sourceArray2[18] = (byte) 194;
    sourceArray2[41] = (byte) 12;
    sourceArray2[42] = (byte) 229;
    sourceArray2[22] = (byte) 13;
    sourceArray2[44] = (byte) 189;
    sourceArray2[43] = (byte) 52;
    sourceArray2[46] = (byte) 232;
    sourceArray2[6] = (byte) 185;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13617(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[35] = (byte) 196;
    sourceArray1[14] = (byte) 177;
    sourceArray1[2] = (byte) 241;
    sourceArray1[16 /*0x10*/] = (byte) 87;
    sourceArray1[4] = (byte) 222;
    sourceArray1[5] = (byte) 37;
    sourceArray1[34] = (byte) 233;
    sourceArray1[46] = (byte) 246;
    sourceArray1[43] = (byte) 3;
    sourceArray1[47] = (byte) 8;
    sourceArray1[44] = (byte) 102;
    sourceArray1[41] = (byte) 144 /*0x90*/;
    sourceArray1[9] = (byte) 34;
    sourceArray1[13] = (byte) 208 /*0xD0*/;
    sourceArray1[1] = (byte) 209;
    sourceArray1[15] = (byte) 232;
    sourceArray1[22] = (byte) 44;
    sourceArray1[17] = (byte) 149;
    sourceArray1[6] = (byte) 150;
    sourceArray1[19] = (byte) 27;
    sourceArray1[20] = (byte) 208 /*0xD0*/;
    sourceArray1[21] = byte.MaxValue;
    sourceArray1[7] = (byte) 119;
    sourceArray1[27] = (byte) 65;
    sourceArray1[24] = (byte) 133;
    sourceArray1[25] = (byte) 155;
    sourceArray1[18] = (byte) 99;
    sourceArray1[8] = (byte) 167;
    sourceArray1[28] = (byte) 205;
    sourceArray1[3] = (byte) 83;
    sourceArray1[30] = (byte) 44;
    sourceArray1[23] = (byte) 65;
    sourceArray1[32 /*0x20*/] = (byte) 115;
    sourceArray1[33] = (byte) 171;
    sourceArray1[40] = (byte) 54;
    sourceArray1[12] = (byte) 73;
    sourceArray1[36] = (byte) 26;
    sourceArray1[37] = (byte) 108;
    sourceArray1[38] = (byte) 121;
    sourceArray1[39] = (byte) 3;
    sourceArray1[0] = (byte) 132;
    sourceArray1[11] = (byte) 104;
    sourceArray1[10] = (byte) 50;
    sourceArray1[31 /*0x1F*/] = (byte) 41;
    sourceArray1[29] = (byte) 36;
    sourceArray1[45] = (byte) 111;
    sourceArray1[42] = (byte) 150;
    sourceArray1[26] = (byte) 175;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 18;
    sourceArray2[15] = (byte) 48 /*0x30*/;
    sourceArray2[16 /*0x10*/] = (byte) 34;
    sourceArray2[0] = (byte) 62;
    sourceArray2[35] = (byte) 153;
    sourceArray2[13] = (byte) 104;
    sourceArray2[6] = (byte) 152;
    sourceArray2[29] = (byte) 215;
    sourceArray2[45] = (byte) 178;
    sourceArray2[2] = (byte) 99;
    sourceArray2[8] = (byte) 94;
    sourceArray2[28] = (byte) 94;
    sourceArray2[17] = (byte) 187;
    sourceArray2[11] = (byte) 178;
    sourceArray2[14] = (byte) 51;
    sourceArray2[12] = (byte) 116;
    sourceArray2[23] = (byte) 222;
    sourceArray2[34] = (byte) 237;
    sourceArray2[18] = (byte) 58;
    sourceArray2[32 /*0x20*/] = (byte) 8;
    sourceArray2[1] = (byte) 71;
    sourceArray2[39] = (byte) 130;
    sourceArray2[24] = (byte) 75;
    sourceArray2[4] = (byte) 103;
    sourceArray2[3] = (byte) 174;
    sourceArray2[25] = (byte) 27;
    sourceArray2[42] = (byte) 18;
    sourceArray2[46] = (byte) 154;
    sourceArray2[33] = (byte) 105;
    sourceArray2[27] = (byte) 223;
    sourceArray2[30] = (byte) 96 /*0x60*/;
    sourceArray2[7] = (byte) 100;
    sourceArray2[43] = (byte) 123;
    sourceArray2[22] = (byte) 156;
    sourceArray2[10] = (byte) 37;
    sourceArray2[20] = (byte) 60;
    sourceArray2[36] = (byte) 124;
    sourceArray2[41] = (byte) 177;
    sourceArray2[38] = (byte) 78;
    sourceArray2[19] = (byte) 63 /*0x3F*/;
    sourceArray2[40] = (byte) 26;
    sourceArray2[31 /*0x1F*/] = (byte) 203;
    sourceArray2[9] = (byte) 154;
    sourceArray2[21] = (byte) 210;
    sourceArray2[44] = (byte) 141;
    sourceArray2[5] = (byte) 39;
    sourceArray2[26] = (byte) 52;
    sourceArray2[47] = (byte) 173;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13618()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 151,
        (byte) 191,
        (byte) 143,
        (byte) 121,
        (byte) 203,
        (byte) 215,
        (byte) 180,
        (byte) 169,
        (byte) 125,
        (byte) 158
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 161,
        (byte) 20,
        (byte) 213,
        (byte) 205,
        (byte) 245,
        (byte) 232,
        (byte) 188,
        (byte) 68,
        (byte) 81,
        (byte) 139
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
      (byte) 150,
      (byte) 101,
      (byte) 77,
      (byte) 199,
      (byte) 96 /*0x60*/,
      (byte) 87,
      (byte) 29,
      (byte) 80 /*0x50*/,
      (byte) 72
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 112 /*0x70*/,
      (byte) 148,
      (byte) 34,
      (byte) 124,
      (byte) 64 /*0x40*/,
      (byte) 190,
      (byte) 116,
      (byte) 154,
      (byte) 246,
      (byte) 14
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
