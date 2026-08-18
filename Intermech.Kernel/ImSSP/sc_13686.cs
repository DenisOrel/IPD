// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13686
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13686
{
  private static byte[] sspq = new byte[1189]
  {
    (byte) 90,
    (byte) 232,
    (byte) 236,
    (byte) 97,
    (byte) 189,
    (byte) 125,
    (byte) 76,
    (byte) 73,
    (byte) 133,
    (byte) 35,
    (byte) 217,
    (byte) 227,
    (byte) 177,
    (byte) 151,
    byte.MaxValue,
    (byte) 207,
    (byte) 2,
    (byte) 99,
    (byte) 5,
    (byte) 214,
    (byte) 26,
    (byte) 29,
    (byte) 148,
    (byte) 157,
    (byte) 139,
    (byte) 162,
    (byte) 163,
    (byte) 13,
    (byte) 207,
    (byte) 34,
    (byte) 248,
    (byte) 167,
    (byte) 23,
    (byte) 41,
    (byte) 73,
    (byte) 77,
    (byte) 203,
    (byte) 93,
    (byte) 9,
    (byte) 105,
    (byte) 146,
    (byte) 39,
    (byte) 165,
    (byte) 188,
    (byte) 218,
    (byte) 3,
    (byte) 14,
    (byte) 125,
    (byte) 52,
    (byte) 92,
    (byte) 40,
    (byte) 64 /*0x40*/,
    (byte) 58,
    (byte) 144 /*0x90*/,
    (byte) 64 /*0x40*/,
    (byte) 202,
    (byte) 182,
    (byte) 155,
    (byte) 15,
    (byte) 114,
    (byte) 115,
    (byte) 209,
    (byte) 4,
    (byte) 170,
    (byte) 172,
    (byte) 186,
    (byte) 76,
    (byte) 10,
    (byte) 134,
    (byte) 60,
    (byte) 78,
    (byte) 176 /*0xB0*/,
    (byte) 53,
    (byte) 67,
    (byte) 177,
    (byte) 254,
    (byte) 247,
    (byte) 238,
    (byte) 79,
    (byte) 20,
    (byte) 52,
    (byte) 167,
    (byte) 161,
    (byte) 224 /*0xE0*/,
    (byte) 62,
    (byte) 4,
    (byte) 171,
    (byte) 113,
    (byte) 235,
    (byte) 114,
    (byte) 178,
    (byte) 26,
    (byte) 216,
    (byte) 9,
    (byte) 89,
    (byte) 131,
    (byte) 17,
    (byte) 113,
    (byte) 28,
    (byte) 237,
    (byte) 232,
    (byte) 31 /*0x1F*/,
    (byte) 92,
    (byte) 162,
    (byte) 82,
    (byte) 145,
    (byte) 50,
    (byte) 59,
    (byte) 159,
    (byte) 186,
    (byte) 179,
    (byte) 151,
    (byte) 52,
    (byte) 237,
    (byte) 64 /*0x40*/,
    (byte) 211,
    (byte) 245,
    (byte) 71,
    (byte) 104,
    (byte) 243,
    (byte) 77,
    (byte) 6,
    (byte) 53,
    (byte) 95,
    (byte) 182,
    (byte) 15,
    (byte) 0,
    (byte) 226,
    (byte) 127 /*0x7F*/,
    (byte) 191,
    (byte) 136,
    (byte) 3,
    (byte) 165,
    (byte) 96 /*0x60*/,
    (byte) 59,
    (byte) 16 /*0x10*/,
    (byte) 153,
    (byte) 149,
    (byte) 165,
    (byte) 247,
    (byte) 53,
    (byte) 105,
    (byte) 146,
    (byte) 150,
    (byte) 149,
    (byte) 185,
    (byte) 217,
    (byte) 107,
    (byte) 28,
    (byte) 35,
    (byte) 197,
    (byte) 78,
    (byte) 41,
    (byte) 217,
    (byte) 173,
    (byte) 30,
    (byte) 241,
    (byte) 174,
    (byte) 23,
    (byte) 166,
    (byte) 1,
    (byte) 134,
    (byte) 130,
    (byte) 243,
    (byte) 91,
    (byte) 228,
    (byte) 59,
    (byte) 31 /*0x1F*/,
    (byte) 115,
    (byte) 131,
    (byte) 163,
    (byte) 227,
    (byte) 37,
    (byte) 235,
    (byte) 75,
    (byte) 31 /*0x1F*/,
    (byte) 56,
    (byte) 8,
    (byte) 73,
    (byte) 210,
    (byte) 206,
    (byte) 12,
    (byte) 233,
    (byte) 218,
    (byte) 176 /*0xB0*/,
    (byte) 238,
    (byte) 102,
    (byte) 74,
    (byte) 204,
    (byte) 9,
    (byte) 136,
    (byte) 230,
    (byte) 76,
    (byte) 142,
    (byte) 34,
    (byte) 181,
    (byte) 34,
    (byte) 191,
    (byte) 154,
    (byte) 131,
    (byte) 5,
    (byte) 87,
    (byte) 113,
    (byte) 184,
    (byte) 213,
    (byte) 38,
    (byte) 114,
    (byte) 142,
    (byte) 197,
    (byte) 125,
    (byte) 68,
    (byte) 28,
    (byte) 185,
    (byte) 86,
    (byte) 208 /*0xD0*/,
    (byte) 53,
    (byte) 250,
    (byte) 75,
    (byte) 57,
    (byte) 28,
    (byte) 131,
    (byte) 87,
    (byte) 49,
    (byte) 6,
    (byte) 43,
    (byte) 203,
    (byte) 215,
    (byte) 6,
    (byte) 146,
    (byte) 253,
    (byte) 209,
    (byte) 106,
    (byte) 238,
    (byte) 6,
    (byte) 163,
    byte.MaxValue,
    (byte) 25,
    (byte) 62,
    (byte) 77,
    (byte) 46,
    (byte) 251,
    (byte) 183,
    (byte) 219,
    (byte) 133,
    (byte) 211,
    (byte) 118,
    (byte) 105,
    (byte) 89,
    (byte) 36,
    (byte) 157,
    (byte) 102,
    (byte) 239,
    (byte) 91,
    (byte) 213,
    (byte) 109,
    (byte) 191,
    (byte) 241,
    (byte) 14,
    (byte) 165,
    (byte) 60,
    (byte) 112 /*0x70*/,
    (byte) 76,
    (byte) 92,
    (byte) 83,
    (byte) 125,
    (byte) 176 /*0xB0*/,
    (byte) 149,
    (byte) 11,
    (byte) 248,
    (byte) 21,
    (byte) 221,
    (byte) 61,
    (byte) 88,
    (byte) 48 /*0x30*/,
    (byte) 155,
    (byte) 127 /*0x7F*/,
    (byte) 199,
    (byte) 31 /*0x1F*/,
    (byte) 53,
    (byte) 204,
    (byte) 131,
    (byte) 146,
    (byte) 93,
    (byte) 230,
    (byte) 241,
    (byte) 148,
    (byte) 32 /*0x20*/,
    (byte) 45,
    (byte) 63 /*0x3F*/,
    (byte) 25,
    (byte) 250,
    (byte) 24,
    (byte) 51,
    (byte) 122,
    (byte) 175,
    (byte) 235,
    (byte) 63 /*0x3F*/,
    (byte) 121,
    (byte) 60,
    (byte) 234,
    (byte) 230,
    (byte) 158,
    (byte) 105,
    (byte) 221,
    (byte) 100,
    (byte) 155,
    (byte) 51,
    (byte) 236,
    (byte) 28,
    (byte) 148,
    (byte) 140,
    (byte) 49,
    (byte) 44,
    (byte) 195,
    (byte) 95,
    (byte) 198,
    (byte) 72,
    (byte) 56,
    (byte) 252,
    (byte) 177,
    (byte) 81,
    (byte) 31 /*0x1F*/,
    (byte) 138,
    (byte) 159,
    (byte) 189,
    (byte) 174,
    (byte) 186,
    (byte) 26,
    (byte) 253,
    (byte) 182,
    (byte) 16 /*0x10*/,
    (byte) 31 /*0x1F*/,
    (byte) 176 /*0xB0*/,
    (byte) 227,
    (byte) 14,
    (byte) 161,
    (byte) 162,
    (byte) 83,
    (byte) 125,
    (byte) 198,
    (byte) 125,
    (byte) 210,
    (byte) 7,
    (byte) 131,
    (byte) 60,
    (byte) 211,
    (byte) 138,
    (byte) 94,
    (byte) 234,
    (byte) 48 /*0x30*/,
    (byte) 220,
    (byte) 179,
    (byte) 29,
    byte.MaxValue,
    (byte) 182,
    (byte) 48 /*0x30*/,
    (byte) 106,
    (byte) 111,
    (byte) 12,
    (byte) 8,
    (byte) 155,
    (byte) 191,
    (byte) 125,
    (byte) 64 /*0x40*/,
    (byte) 70,
    (byte) 21,
    (byte) 218,
    (byte) 46,
    (byte) 163,
    (byte) 207,
    (byte) 44,
    (byte) 60,
    (byte) 231,
    (byte) 8,
    (byte) 115,
    (byte) 248,
    (byte) 78,
    (byte) 16 /*0x10*/,
    (byte) 243,
    (byte) 136,
    (byte) 154,
    (byte) 42,
    (byte) 28,
    (byte) 244,
    (byte) 30,
    (byte) 13,
    (byte) 189,
    (byte) 28,
    (byte) 115,
    (byte) 61,
    (byte) 166,
    (byte) 132,
    (byte) 20,
    (byte) 254,
    (byte) 127 /*0x7F*/,
    (byte) 235,
    (byte) 168,
    (byte) 133,
    (byte) 188,
    (byte) 110,
    (byte) 154,
    (byte) 225,
    (byte) 177,
    (byte) 208 /*0xD0*/,
    (byte) 209,
    (byte) 34,
    (byte) 27,
    (byte) 156,
    (byte) 247,
    (byte) 79,
    (byte) 13,
    (byte) 215,
    (byte) 41,
    (byte) 199,
    (byte) 74,
    (byte) 13,
    (byte) 33,
    (byte) 38,
    (byte) 173,
    (byte) 95,
    (byte) 249,
    (byte) 24,
    (byte) 51,
    (byte) 103,
    (byte) 67,
    (byte) 127 /*0x7F*/,
    (byte) 237,
    (byte) 109,
    (byte) 53,
    (byte) 241,
    (byte) 6,
    (byte) 172,
    (byte) 155,
    (byte) 118,
    (byte) 167,
    (byte) 144 /*0x90*/,
    (byte) 71,
    (byte) 109,
    (byte) 80 /*0x50*/,
    (byte) 170,
    (byte) 212,
    (byte) 165,
    (byte) 140,
    (byte) 80 /*0x50*/,
    (byte) 104,
    (byte) 86,
    (byte) 111,
    (byte) 246,
    (byte) 17,
    (byte) 63 /*0x3F*/,
    (byte) 112 /*0x70*/,
    (byte) 56,
    (byte) 180,
    (byte) 59,
    (byte) 14,
    (byte) 217,
    (byte) 228,
    (byte) 53,
    (byte) 227,
    (byte) 182,
    (byte) 234,
    (byte) 23,
    (byte) 167,
    (byte) 36,
    (byte) 224 /*0xE0*/,
    (byte) 47,
    (byte) 184,
    (byte) 156,
    (byte) 155,
    (byte) 19,
    (byte) 195,
    (byte) 41,
    (byte) 191,
    (byte) 29,
    (byte) 122,
    (byte) 62,
    (byte) 224 /*0xE0*/,
    (byte) 252,
    (byte) 196,
    (byte) 203,
    (byte) 250,
    (byte) 208 /*0xD0*/,
    (byte) 32 /*0x20*/,
    (byte) 166,
    (byte) 223,
    (byte) 245,
    (byte) 28,
    (byte) 39,
    (byte) 37,
    (byte) 175,
    (byte) 213,
    (byte) 25,
    (byte) 37,
    (byte) 209,
    (byte) 91,
    (byte) 51,
    (byte) 61,
    (byte) 185,
    (byte) 13,
    (byte) 139,
    (byte) 250,
    (byte) 86,
    (byte) 113,
    (byte) 225,
    (byte) 155,
    (byte) 123,
    (byte) 213,
    (byte) 195,
    (byte) 91,
    (byte) 185,
    (byte) 186,
    (byte) 198,
    (byte) 195,
    (byte) 85,
    (byte) 199,
    (byte) 89,
    (byte) 78,
    (byte) 192 /*0xC0*/,
    (byte) 122,
    (byte) 131,
    (byte) 147,
    (byte) 30,
    (byte) 241,
    (byte) 220,
    (byte) 133,
    (byte) 32 /*0x20*/,
    (byte) 94,
    (byte) 228,
    (byte) 123,
    (byte) 184,
    (byte) 97,
    (byte) 196,
    (byte) 19,
    (byte) 48 /*0x30*/,
    (byte) 213,
    (byte) 116,
    (byte) 82,
    (byte) 171,
    (byte) 105,
    (byte) 87,
    (byte) 124,
    (byte) 186,
    (byte) 244,
    (byte) 97,
    (byte) 201,
    (byte) 39,
    (byte) 112 /*0x70*/,
    (byte) 61,
    (byte) 197,
    (byte) 12,
    (byte) 233,
    (byte) 179,
    (byte) 70,
    (byte) 65,
    (byte) 14,
    (byte) 13,
    (byte) 180,
    (byte) 199,
    (byte) 128 /*0x80*/,
    (byte) 89,
    (byte) 59,
    (byte) 191,
    (byte) 207,
    (byte) 3,
    (byte) 202,
    (byte) 54,
    (byte) 137,
    (byte) 148,
    (byte) 151,
    (byte) 39,
    (byte) 111,
    (byte) 75,
    (byte) 87,
    (byte) 29,
    (byte) 73,
    (byte) 132,
    (byte) 186,
    (byte) 120,
    (byte) 238,
    (byte) 91,
    (byte) 159,
    (byte) 177,
    (byte) 152,
    (byte) 24,
    (byte) 227,
    (byte) 83,
    (byte) 141,
    (byte) 116,
    (byte) 195,
    (byte) 31 /*0x1F*/,
    (byte) 110,
    (byte) 108,
    (byte) 3,
    (byte) 61,
    (byte) 181,
    byte.MaxValue,
    (byte) 221,
    (byte) 87,
    (byte) 17,
    (byte) 239,
    (byte) 210,
    (byte) 61,
    (byte) 214,
    (byte) 76,
    (byte) 40,
    (byte) 52,
    (byte) 36,
    (byte) 18,
    (byte) 51,
    (byte) 83,
    (byte) 114,
    (byte) 108,
    (byte) 7,
    (byte) 159,
    (byte) 85,
    (byte) 34,
    (byte) 118,
    (byte) 165,
    (byte) 188,
    (byte) 108,
    (byte) 228,
    (byte) 99,
    (byte) 132,
    (byte) 194,
    (byte) 184,
    (byte) 136,
    (byte) 96 /*0x60*/,
    (byte) 185,
    (byte) 10,
    (byte) 240 /*0xF0*/,
    (byte) 40,
    (byte) 246,
    (byte) 153,
    (byte) 148,
    (byte) 148,
    (byte) 237,
    (byte) 117,
    (byte) 168,
    (byte) 249,
    (byte) 57,
    (byte) 17,
    (byte) 219,
    (byte) 6,
    (byte) 42,
    (byte) 80 /*0x50*/,
    (byte) 69,
    (byte) 53,
    (byte) 112 /*0x70*/,
    (byte) 103,
    (byte) 65,
    (byte) 129,
    (byte) 237,
    (byte) 224 /*0xE0*/,
    (byte) 144 /*0x90*/,
    (byte) 115,
    (byte) 245,
    (byte) 44,
    (byte) 56,
    (byte) 233,
    (byte) 68,
    (byte) 17,
    (byte) 35,
    (byte) 68,
    (byte) 241,
    (byte) 150,
    (byte) 7,
    (byte) 36,
    (byte) 146,
    (byte) 209,
    (byte) 140,
    (byte) 87,
    (byte) 224 /*0xE0*/,
    (byte) 185,
    (byte) 37,
    (byte) 95,
    (byte) 172,
    (byte) 123,
    (byte) 180,
    (byte) 225,
    (byte) 161,
    byte.MaxValue,
    (byte) 43,
    (byte) 82,
    (byte) 183,
    (byte) 170,
    (byte) 47,
    (byte) 3,
    (byte) 189,
    (byte) 208 /*0xD0*/,
    (byte) 25,
    (byte) 144 /*0x90*/,
    (byte) 181,
    (byte) 177,
    (byte) 249,
    (byte) 71,
    (byte) 200,
    (byte) 210,
    (byte) 62,
    (byte) 177,
    (byte) 78,
    (byte) 42,
    (byte) 171,
    (byte) 205,
    (byte) 215,
    (byte) 106,
    (byte) 154,
    (byte) 114,
    (byte) 165,
    (byte) 56,
    (byte) 34,
    (byte) 229,
    (byte) 238,
    (byte) 208 /*0xD0*/,
    (byte) 72,
    (byte) 88,
    (byte) 130,
    (byte) 248,
    (byte) 160 /*0xA0*/,
    (byte) 21,
    (byte) 231,
    (byte) 6,
    (byte) 183,
    (byte) 253,
    (byte) 45,
    (byte) 198,
    (byte) 138,
    (byte) 148,
    (byte) 172,
    (byte) 132,
    (byte) 176 /*0xB0*/,
    (byte) 152,
    (byte) 41,
    (byte) 141,
    (byte) 37,
    (byte) 66,
    (byte) 58,
    (byte) 155,
    (byte) 130,
    (byte) 96 /*0x60*/,
    (byte) 63 /*0x3F*/,
    (byte) 228,
    (byte) 94,
    (byte) 83,
    (byte) 118,
    (byte) 143,
    (byte) 136,
    (byte) 17,
    (byte) 0,
    (byte) 83,
    (byte) 29,
    (byte) 12,
    (byte) 170,
    (byte) 32 /*0x20*/,
    (byte) 121,
    (byte) 87,
    (byte) 25,
    (byte) 213,
    (byte) 30,
    (byte) 50,
    (byte) 253,
    (byte) 169,
    (byte) 200,
    (byte) 219,
    (byte) 199,
    (byte) 131,
    (byte) 221,
    (byte) 76,
    (byte) 139,
    (byte) 97,
    (byte) 104,
    (byte) 229,
    (byte) 21,
    (byte) 2,
    (byte) 170,
    (byte) 194,
    (byte) 164,
    (byte) 185,
    (byte) 66,
    (byte) 168,
    (byte) 100,
    (byte) 39,
    (byte) 232,
    (byte) 88,
    (byte) 211,
    (byte) 45,
    (byte) 208 /*0xD0*/,
    (byte) 69,
    (byte) 78,
    (byte) 245,
    (byte) 55,
    (byte) 79,
    (byte) 172,
    (byte) 193,
    (byte) 221,
    (byte) 231,
    (byte) 121,
    (byte) 27,
    (byte) 200,
    (byte) 209,
    (byte) 220,
    (byte) 219,
    (byte) 156,
    (byte) 239,
    (byte) 225,
    (byte) 251,
    (byte) 12,
    (byte) 0,
    (byte) 222,
    (byte) 83,
    (byte) 26,
    (byte) 38,
    (byte) 238,
    (byte) 117,
    (byte) 222,
    (byte) 145,
    (byte) 100,
    (byte) 161,
    (byte) 102,
    (byte) 45,
    (byte) 240 /*0xF0*/,
    (byte) 248,
    (byte) 50,
    (byte) 195,
    (byte) 226,
    (byte) 75,
    (byte) 90,
    (byte) 213,
    (byte) 39,
    (byte) 231,
    (byte) 80 /*0x50*/,
    (byte) 92,
    (byte) 208 /*0xD0*/,
    (byte) 104,
    (byte) 170,
    (byte) 70,
    byte.MaxValue,
    (byte) 211,
    (byte) 215,
    (byte) 66,
    (byte) 140,
    (byte) 119,
    (byte) 171,
    (byte) 165,
    (byte) 60,
    (byte) 233,
    (byte) 159,
    (byte) 150,
    (byte) 119,
    (byte) 88,
    (byte) 253,
    (byte) 167,
    (byte) 20,
    (byte) 134,
    (byte) 186,
    (byte) 121,
    (byte) 94,
    (byte) 10,
    (byte) 247,
    (byte) 220,
    (byte) 232,
    (byte) 9,
    (byte) 252,
    (byte) 234,
    (byte) 220,
    (byte) 180,
    (byte) 251,
    (byte) 12,
    (byte) 108,
    (byte) 140,
    (byte) 197,
    (byte) 14,
    (byte) 231,
    (byte) 157,
    (byte) 178,
    (byte) 224 /*0xE0*/,
    (byte) 146,
    (byte) 85,
    (byte) 191,
    (byte) 237,
    byte.MaxValue,
    (byte) 237,
    (byte) 116,
    (byte) 70,
    (byte) 143,
    (byte) 77,
    (byte) 171,
    (byte) 116,
    (byte) 159,
    (byte) 129,
    (byte) 61,
    (byte) 172,
    (byte) 17,
    (byte) 3,
    (byte) 131,
    (byte) 218,
    (byte) 17,
    (byte) 214,
    (byte) 4,
    (byte) 136,
    (byte) 69,
    (byte) 193,
    (byte) 137,
    (byte) 52,
    (byte) 105,
    (byte) 125,
    (byte) 141,
    (byte) 67,
    (byte) 164,
    (byte) 136,
    (byte) 184,
    (byte) 10,
    (byte) 178,
    (byte) 25,
    (byte) 121,
    (byte) 83,
    (byte) 162,
    (byte) 184,
    (byte) 40,
    (byte) 63 /*0x3F*/,
    (byte) 239,
    (byte) 16 /*0x10*/,
    (byte) 151,
    (byte) 173,
    (byte) 18,
    (byte) 125,
    (byte) 174,
    (byte) 247,
    (byte) 240 /*0xF0*/,
    (byte) 102,
    (byte) 38,
    (byte) 20,
    (byte) 100,
    (byte) 7,
    (byte) 148,
    (byte) 101,
    (byte) 191,
    (byte) 169,
    (byte) 175,
    (byte) 42,
    (byte) 223,
    (byte) 147,
    (byte) 9,
    (byte) 158,
    (byte) 139,
    (byte) 160 /*0xA0*/,
    (byte) 43,
    (byte) 212,
    (byte) 169,
    (byte) 75,
    (byte) 73,
    (byte) 218,
    (byte) 75,
    (byte) 163,
    (byte) 68,
    (byte) 170,
    (byte) 147,
    (byte) 22,
    (byte) 35,
    (byte) 224 /*0xE0*/,
    (byte) 144 /*0x90*/,
    (byte) 216,
    (byte) 237,
    (byte) 126,
    (byte) 241,
    (byte) 111,
    (byte) 75,
    (byte) 170,
    (byte) 177,
    (byte) 251,
    (byte) 66,
    (byte) 191,
    (byte) 173,
    (byte) 21,
    (byte) 180,
    (byte) 78,
    (byte) 183,
    (byte) 134,
    (byte) 251,
    (byte) 190,
    (byte) 66,
    (byte) 85,
    (byte) 182,
    (byte) 251,
    (byte) 206,
    (byte) 168,
    (byte) 129,
    (byte) 170,
    (byte) 57,
    (byte) 131,
    (byte) 27,
    (byte) 93,
    (byte) 169,
    (byte) 56,
    (byte) 150,
    (byte) 94,
    (byte) 47,
    (byte) 197,
    (byte) 50,
    (byte) 51,
    (byte) 76,
    (byte) 188,
    (byte) 252,
    (byte) 5,
    (byte) 190,
    (byte) 26,
    (byte) 87,
    (byte) 120,
    (byte) 226,
    (byte) 129,
    (byte) 39,
    (byte) 134,
    (byte) 209,
    (byte) 50,
    (byte) 141,
    (byte) 233,
    (byte) 216,
    (byte) 152,
    (byte) 35,
    (byte) 15,
    (byte) 110,
    (byte) 190,
    (byte) 17,
    (byte) 6,
    (byte) 106,
    (byte) 152,
    (byte) 121,
    (byte) 4,
    (byte) 37,
    (byte) 123,
    (byte) 74,
    (byte) 156,
    (byte) 212,
    (byte) 75,
    (byte) 194,
    (byte) 38,
    (byte) 83,
    (byte) 56,
    (byte) 125,
    (byte) 48 /*0x30*/,
    (byte) 193,
    (byte) 76,
    (byte) 13,
    (byte) 153,
    (byte) 160 /*0xA0*/,
    (byte) 95,
    (byte) 208 /*0xD0*/,
    (byte) 145,
    (byte) 35,
    (byte) 28,
    (byte) 186,
    (byte) 17,
    (byte) 254,
    (byte) 142,
    (byte) 119,
    (byte) 137,
    (byte) 72,
    (byte) 64 /*0x40*/,
    (byte) 177,
    (byte) 93,
    (byte) 124,
    (byte) 220,
    (byte) 248,
    (byte) 124,
    (byte) 249,
    (byte) 181,
    (byte) 62,
    (byte) 9,
    (byte) 43,
    (byte) 114,
    (byte) 54,
    (byte) 89,
    (byte) 95,
    (byte) 235,
    (byte) 102,
    (byte) 99,
    (byte) 27,
    (byte) 244,
    (byte) 96 /*0x60*/,
    (byte) 15,
    (byte) 5,
    (byte) 62,
    (byte) 4,
    (byte) 30,
    (byte) 193,
    (byte) 181,
    (byte) 24,
    (byte) 5,
    (byte) 142,
    (byte) 199,
    (byte) 56,
    (byte) 48 /*0x30*/,
    (byte) 109,
    (byte) 155,
    (byte) 215,
    (byte) 214,
    (byte) 111,
    (byte) 161,
    (byte) 92,
    (byte) 87,
    (byte) 204,
    (byte) 76,
    (byte) 2,
    (byte) 143,
    (byte) 166,
    (byte) 127 /*0x7F*/,
    (byte) 142,
    (byte) 205,
    (byte) 148,
    (byte) 138,
    (byte) 183,
    (byte) 118,
    (byte) 93,
    (byte) 254,
    (byte) 221,
    (byte) 56,
    (byte) 37,
    (byte) 55,
    (byte) 169,
    (byte) 94,
    (byte) 7,
    (byte) 37,
    (byte) 33,
    (byte) 105,
    (byte) 145,
    (byte) 137,
    (byte) 202,
    (byte) 190,
    (byte) 191,
    (byte) 92,
    (byte) 10,
    (byte) 131,
    (byte) 146,
    (byte) 182,
    (byte) 20,
    (byte) 29,
    (byte) 245,
    (byte) 221,
    (byte) 43,
    (byte) 38,
    (byte) 130,
    (byte) 228,
    (byte) 228,
    (byte) 141,
    (byte) 85,
    (byte) 64 /*0x40*/,
    (byte) 108,
    (byte) 176 /*0xB0*/,
    (byte) 127 /*0x7F*/,
    (byte) 89,
    (byte) 70,
    (byte) 53,
    (byte) 40,
    (byte) 133,
    (byte) 170,
    (byte) 32 /*0x20*/,
    (byte) 52,
    (byte) 156,
    (byte) 75,
    (byte) 30,
    (byte) 8,
    (byte) 103,
    (byte) 143,
    (byte) 247,
    (byte) 85,
    (byte) 168,
    (byte) 217,
    (byte) 186,
    (byte) 134,
    (byte) 125,
    (byte) 106,
    (byte) 85,
    (byte) 45,
    (byte) 252,
    (byte) 225,
    (byte) 199,
    (byte) 111,
    (byte) 173,
    (byte) 78,
    (byte) 34,
    (byte) 252,
    (byte) 143,
    (byte) 160 /*0xA0*/,
    (byte) 205,
    (byte) 65,
    byte.MaxValue,
    (byte) 10,
    (byte) 193,
    (byte) 215,
    (byte) 212
  };
  private static byte[] sspr = new byte[1189]
  {
    (byte) 200,
    (byte) 131,
    (byte) 170,
    (byte) 34,
    (byte) 233,
    (byte) 33,
    (byte) 78,
    (byte) 115,
    (byte) 93,
    (byte) 122,
    (byte) 210,
    (byte) 83,
    (byte) 33,
    (byte) 196,
    (byte) 195,
    (byte) 27,
    (byte) 30,
    (byte) 19,
    (byte) 186,
    (byte) 34,
    (byte) 108,
    (byte) 7,
    (byte) 27,
    (byte) 13,
    (byte) 178,
    (byte) 16 /*0x10*/,
    (byte) 95,
    (byte) 160 /*0xA0*/,
    (byte) 193,
    (byte) 105,
    (byte) 202,
    (byte) 115,
    (byte) 201,
    (byte) 6,
    (byte) 54,
    (byte) 122,
    (byte) 90,
    (byte) 84,
    (byte) 129,
    (byte) 163,
    (byte) 128 /*0x80*/,
    (byte) 199,
    (byte) 54,
    (byte) 13,
    (byte) 178,
    (byte) 193,
    (byte) 94,
    (byte) 100,
    (byte) 32 /*0x20*/,
    (byte) 84,
    (byte) 111,
    (byte) 210,
    (byte) 232,
    (byte) 144 /*0x90*/,
    (byte) 24,
    (byte) 245,
    (byte) 26,
    (byte) 56,
    (byte) 63 /*0x3F*/,
    (byte) 115,
    (byte) 3,
    (byte) 186,
    (byte) 219,
    (byte) 73,
    (byte) 221,
    (byte) 171,
    (byte) 62,
    (byte) 225,
    (byte) 232,
    (byte) 58,
    (byte) 152,
    (byte) 112 /*0x70*/,
    (byte) 164,
    (byte) 136,
    (byte) 161,
    (byte) 13,
    (byte) 177,
    (byte) 252,
    (byte) 135,
    (byte) 199,
    (byte) 14,
    (byte) 196,
    (byte) 146,
    (byte) 222,
    (byte) 84,
    (byte) 52,
    (byte) 191,
    (byte) 14,
    (byte) 49,
    (byte) 109,
    (byte) 7,
    (byte) 76,
    (byte) 51,
    (byte) 24,
    (byte) 56,
    (byte) 154,
    (byte) 225,
    (byte) 68,
    (byte) 144 /*0x90*/,
    (byte) 214,
    (byte) 13,
    (byte) 16 /*0x10*/,
    (byte) 193,
    (byte) 102,
    (byte) 121,
    (byte) 161,
    (byte) 133,
    (byte) 134,
    (byte) 209,
    (byte) 221,
    (byte) 223,
    (byte) 42,
    (byte) 209,
    (byte) 140,
    (byte) 249,
    (byte) 165,
    (byte) 10,
    (byte) 35,
    (byte) 24,
    (byte) 128 /*0x80*/,
    (byte) 198,
    (byte) 236,
    (byte) 115,
    (byte) 205,
    (byte) 106,
    (byte) 8,
    (byte) 132,
    (byte) 80 /*0x50*/,
    (byte) 221,
    (byte) 3,
    (byte) 161,
    (byte) 178,
    (byte) 238,
    (byte) 91,
    (byte) 31 /*0x1F*/,
    (byte) 57,
    (byte) 231,
    (byte) 155,
    (byte) 97,
    (byte) 149,
    (byte) 155,
    (byte) 72,
    (byte) 238,
    (byte) 30,
    (byte) 162,
    (byte) 65,
    (byte) 94,
    (byte) 105,
    (byte) 114,
    (byte) 91,
    (byte) 102,
    (byte) 175,
    (byte) 8,
    (byte) 199,
    (byte) 14,
    (byte) 81,
    (byte) 218,
    (byte) 9,
    (byte) 251,
    (byte) 79,
    (byte) 149,
    (byte) 244,
    (byte) 125,
    (byte) 181,
    (byte) 221,
    (byte) 221,
    (byte) 25,
    (byte) 150,
    (byte) 55,
    (byte) 177,
    (byte) 37,
    (byte) 34,
    (byte) 250,
    (byte) 6,
    (byte) 66,
    (byte) 75,
    (byte) 186,
    (byte) 18,
    (byte) 212,
    (byte) 196,
    (byte) 199,
    (byte) 172,
    (byte) 232,
    (byte) 76,
    (byte) 110,
    (byte) 86,
    (byte) 12,
    (byte) 9,
    (byte) 153,
    (byte) 198,
    (byte) 129,
    (byte) 211,
    (byte) 135,
    (byte) 226,
    (byte) 51,
    (byte) 7,
    (byte) 164,
    (byte) 224 /*0xE0*/,
    (byte) 119,
    (byte) 247,
    (byte) 59,
    (byte) 71,
    (byte) 239,
    (byte) 65,
    (byte) 236,
    (byte) 94,
    (byte) 58,
    (byte) 231,
    (byte) 118,
    (byte) 43,
    (byte) 35,
    (byte) 78,
    (byte) 142,
    (byte) 117,
    (byte) 86,
    (byte) 8,
    (byte) 69,
    (byte) 10,
    (byte) 190,
    (byte) 165,
    (byte) 250,
    (byte) 242,
    (byte) 46,
    (byte) 25,
    (byte) 211,
    (byte) 145,
    (byte) 168,
    (byte) 64 /*0x40*/,
    (byte) 100,
    (byte) 226,
    (byte) 242,
    (byte) 11,
    (byte) 122,
    (byte) 10,
    (byte) 42,
    (byte) 219,
    (byte) 88,
    (byte) 71,
    (byte) 213,
    (byte) 176 /*0xB0*/,
    (byte) 26,
    (byte) 122,
    (byte) 142,
    (byte) 141,
    (byte) 110,
    (byte) 153,
    (byte) 18,
    (byte) 233,
    (byte) 5,
    (byte) 92,
    (byte) 167,
    (byte) 203,
    (byte) 77,
    (byte) 15,
    (byte) 113,
    (byte) 251,
    (byte) 187,
    (byte) 204,
    (byte) 151,
    (byte) 244,
    (byte) 207,
    (byte) 204,
    (byte) 207,
    (byte) 156,
    (byte) 38,
    (byte) 188,
    (byte) 208 /*0xD0*/,
    (byte) 165,
    (byte) 26,
    (byte) 142,
    (byte) 62,
    (byte) 43,
    (byte) 152,
    (byte) 215,
    (byte) 239,
    (byte) 155,
    (byte) 186,
    (byte) 17,
    (byte) 132,
    (byte) 254,
    byte.MaxValue,
    (byte) 143,
    (byte) 116,
    (byte) 173,
    (byte) 209,
    (byte) 20,
    (byte) 252,
    (byte) 125,
    (byte) 146,
    (byte) 194,
    (byte) 244,
    (byte) 55,
    (byte) 186,
    (byte) 44,
    (byte) 179,
    (byte) 141,
    (byte) 40,
    (byte) 34,
    (byte) 90,
    (byte) 192 /*0xC0*/,
    (byte) 177,
    (byte) 185,
    (byte) 34,
    (byte) 9,
    (byte) 95,
    (byte) 151,
    (byte) 53,
    (byte) 240 /*0xF0*/,
    (byte) 161,
    (byte) 29,
    (byte) 178,
    (byte) 13,
    (byte) 107,
    (byte) 78,
    (byte) 248,
    (byte) 75,
    (byte) 76,
    (byte) 242,
    (byte) 90,
    (byte) 21,
    (byte) 95,
    (byte) 155,
    (byte) 22,
    (byte) 185,
    (byte) 141,
    (byte) 123,
    (byte) 87,
    (byte) 239,
    (byte) 206,
    (byte) 33,
    (byte) 26,
    (byte) 109,
    (byte) 152,
    (byte) 148,
    (byte) 224 /*0xE0*/,
    (byte) 201,
    (byte) 248,
    (byte) 243,
    (byte) 210,
    (byte) 80 /*0x50*/,
    (byte) 158,
    (byte) 117,
    (byte) 92,
    (byte) 11,
    (byte) 191,
    (byte) 123,
    (byte) 190,
    (byte) 44,
    (byte) 19,
    (byte) 231,
    (byte) 27,
    (byte) 152,
    (byte) 8,
    (byte) 47,
    (byte) 123,
    (byte) 203,
    (byte) 88,
    (byte) 196,
    (byte) 91,
    (byte) 32 /*0x20*/,
    (byte) 190,
    (byte) 30,
    (byte) 2,
    (byte) 52,
    (byte) 221,
    (byte) 135,
    (byte) 114,
    (byte) 142,
    (byte) 132,
    (byte) 100,
    (byte) 26,
    (byte) 133,
    (byte) 148,
    (byte) 43,
    (byte) 17,
    (byte) 230,
    (byte) 211,
    (byte) 158,
    (byte) 147,
    (byte) 188,
    (byte) 182,
    (byte) 16 /*0x10*/,
    (byte) 101,
    (byte) 40,
    (byte) 82,
    (byte) 148,
    (byte) 168,
    (byte) 114,
    (byte) 93,
    (byte) 192 /*0xC0*/,
    (byte) 148,
    (byte) 161,
    (byte) 236,
    (byte) 14,
    (byte) 120,
    (byte) 46,
    (byte) 30,
    (byte) 95,
    (byte) 134,
    (byte) 91,
    (byte) 250,
    byte.MaxValue,
    (byte) 164,
    (byte) 32 /*0x20*/,
    (byte) 130,
    (byte) 72,
    (byte) 126,
    (byte) 234,
    (byte) 212,
    (byte) 232,
    (byte) 3,
    (byte) 84,
    (byte) 18,
    (byte) 70,
    (byte) 154,
    (byte) 97,
    (byte) 212,
    (byte) 182,
    (byte) 197,
    (byte) 196,
    (byte) 240 /*0xF0*/,
    (byte) 30,
    (byte) 67,
    (byte) 172,
    (byte) 153,
    (byte) 17,
    (byte) 63 /*0x3F*/,
    (byte) 31 /*0x1F*/,
    (byte) 187,
    (byte) 238,
    (byte) 174,
    (byte) 240 /*0xF0*/,
    (byte) 3,
    (byte) 115,
    (byte) 166,
    (byte) 59,
    (byte) 9,
    (byte) 82,
    (byte) 49,
    (byte) 9,
    (byte) 137,
    (byte) 23,
    (byte) 127 /*0x7F*/,
    (byte) 11,
    (byte) 243,
    (byte) 216,
    (byte) 89,
    (byte) 242,
    (byte) 150,
    (byte) 131,
    (byte) 215,
    (byte) 58,
    (byte) 156,
    (byte) 228,
    (byte) 54,
    (byte) 122,
    (byte) 51,
    (byte) 169,
    (byte) 69,
    (byte) 211,
    (byte) 31 /*0x1F*/,
    (byte) 201,
    (byte) 58,
    (byte) 122,
    (byte) 133,
    (byte) 54,
    (byte) 53,
    (byte) 232,
    (byte) 143,
    (byte) 6,
    (byte) 167,
    (byte) 53,
    (byte) 22,
    (byte) 132,
    (byte) 35,
    (byte) 11,
    (byte) 227,
    (byte) 228,
    (byte) 154,
    (byte) 147,
    (byte) 189,
    (byte) 172,
    (byte) 66,
    (byte) 47,
    (byte) 166,
    (byte) 73,
    (byte) 169,
    (byte) 149,
    (byte) 80 /*0x50*/,
    (byte) 44,
    (byte) 91,
    (byte) 70,
    (byte) 95,
    (byte) 21,
    (byte) 199,
    (byte) 207,
    (byte) 162,
    (byte) 64 /*0x40*/,
    (byte) 3,
    (byte) 144 /*0x90*/,
    (byte) 53,
    (byte) 35,
    (byte) 216,
    (byte) 233,
    (byte) 7,
    (byte) 216,
    (byte) 106,
    (byte) 237,
    (byte) 19,
    (byte) 50,
    (byte) 97,
    (byte) 115,
    (byte) 39,
    (byte) 11,
    (byte) 212,
    (byte) 99,
    (byte) 234,
    (byte) 172,
    (byte) 177,
    (byte) 24,
    (byte) 195,
    (byte) 229,
    (byte) 124,
    (byte) 141,
    (byte) 6,
    (byte) 242,
    (byte) 198,
    (byte) 74,
    (byte) 198,
    (byte) 74,
    (byte) 156,
    (byte) 244,
    (byte) 85,
    (byte) 164,
    (byte) 120,
    (byte) 172,
    (byte) 168,
    (byte) 211,
    (byte) 119,
    (byte) 116,
    (byte) 123,
    (byte) 229,
    (byte) 169,
    (byte) 133,
    (byte) 224 /*0xE0*/,
    (byte) 135,
    (byte) 243,
    (byte) 10,
    (byte) 39,
    (byte) 239,
    (byte) 85,
    (byte) 193,
    (byte) 49,
    (byte) 232,
    (byte) 46,
    (byte) 229,
    (byte) 64 /*0x40*/,
    (byte) 199,
    (byte) 36,
    (byte) 98,
    (byte) 20,
    (byte) 188,
    (byte) 19,
    (byte) 65,
    (byte) 11,
    (byte) 9,
    (byte) 205,
    (byte) 90,
    (byte) 133,
    (byte) 158,
    (byte) 50,
    (byte) 58,
    (byte) 228,
    (byte) 112 /*0x70*/,
    (byte) 166,
    (byte) 245,
    (byte) 191,
    (byte) 102,
    (byte) 143,
    (byte) 51,
    (byte) 17,
    (byte) 7,
    (byte) 91,
    (byte) 143,
    (byte) 177,
    (byte) 198,
    (byte) 17,
    (byte) 250,
    (byte) 239,
    (byte) 104,
    (byte) 84,
    (byte) 252,
    (byte) 165,
    (byte) 217,
    (byte) 198,
    (byte) 250,
    (byte) 135,
    (byte) 123,
    (byte) 78,
    (byte) 42,
    (byte) 219,
    (byte) 37,
    (byte) 130,
    (byte) 214,
    (byte) 250,
    (byte) 103,
    (byte) 58,
    (byte) 122,
    (byte) 196,
    (byte) 49,
    (byte) 34,
    (byte) 80 /*0x50*/,
    (byte) 209,
    (byte) 87,
    (byte) 136,
    (byte) 183,
    (byte) 51,
    (byte) 236,
    (byte) 128 /*0x80*/,
    (byte) 120,
    (byte) 50,
    (byte) 63 /*0x3F*/,
    (byte) 102,
    (byte) 214,
    (byte) 121,
    (byte) 225,
    (byte) 108,
    (byte) 83,
    (byte) 184,
    (byte) 193,
    (byte) 126,
    (byte) 207,
    (byte) 110,
    (byte) 199,
    (byte) 198,
    (byte) 142,
    (byte) 182,
    (byte) 94,
    (byte) 137,
    (byte) 235,
    (byte) 237,
    (byte) 243,
    (byte) 235,
    (byte) 253,
    (byte) 223,
    (byte) 121,
    (byte) 34,
    (byte) 196,
    (byte) 142,
    (byte) 86,
    (byte) 9,
    (byte) 10,
    (byte) 49,
    (byte) 103,
    (byte) 136,
    (byte) 206,
    (byte) 240 /*0xF0*/,
    (byte) 85,
    (byte) 17,
    (byte) 238,
    (byte) 224 /*0xE0*/,
    (byte) 151,
    (byte) 196,
    (byte) 1,
    (byte) 13,
    (byte) 59,
    (byte) 132,
    (byte) 99,
    (byte) 137,
    (byte) 73,
    (byte) 71,
    (byte) 106,
    (byte) 51,
    (byte) 211,
    (byte) 155,
    (byte) 120,
    (byte) 48 /*0x30*/,
    (byte) 73,
    (byte) 149,
    (byte) 71,
    (byte) 92,
    (byte) 220,
    (byte) 40,
    (byte) 60,
    (byte) 90,
    (byte) 96 /*0x60*/,
    (byte) 6,
    (byte) 77,
    (byte) 145,
    (byte) 173,
    (byte) 148,
    (byte) 67,
    (byte) 39,
    (byte) 22,
    (byte) 23,
    (byte) 71,
    (byte) 121,
    (byte) 13,
    (byte) 88,
    (byte) 84,
    (byte) 140,
    (byte) 136,
    (byte) 47,
    (byte) 230,
    (byte) 250,
    (byte) 63 /*0x3F*/,
    (byte) 72,
    (byte) 199,
    (byte) 247,
    (byte) 237,
    (byte) 22,
    (byte) 140,
    (byte) 39,
    (byte) 163,
    (byte) 232,
    (byte) 117,
    (byte) 246,
    (byte) 17,
    (byte) 239,
    (byte) 210,
    (byte) 106,
    (byte) 199,
    (byte) 40,
    (byte) 143,
    (byte) 59,
    (byte) 74,
    (byte) 18,
    (byte) 203,
    (byte) 83,
    (byte) 182,
    (byte) 167,
    (byte) 142,
    (byte) 94,
    (byte) 168,
    (byte) 170,
    (byte) 20,
    (byte) 130,
    (byte) 249,
    (byte) 136,
    (byte) 45,
    (byte) 69,
    (byte) 238,
    (byte) 245,
    (byte) 54,
    (byte) 112 /*0x70*/,
    (byte) 185,
    (byte) 96 /*0x60*/,
    (byte) 50,
    (byte) 70,
    (byte) 70,
    (byte) 67,
    (byte) 57,
    (byte) 191,
    (byte) 154,
    (byte) 175,
    (byte) 121,
    (byte) 111,
    (byte) 79,
    (byte) 52,
    (byte) 186,
    (byte) 68,
    (byte) 11,
    (byte) 196,
    (byte) 158,
    (byte) 122,
    (byte) 82,
    (byte) 158,
    (byte) 112 /*0x70*/,
    (byte) 112 /*0x70*/,
    (byte) 130,
    (byte) 231,
    (byte) 113,
    (byte) 193,
    (byte) 30,
    (byte) 125,
    (byte) 223,
    (byte) 215,
    (byte) 154,
    (byte) 85,
    (byte) 4,
    (byte) 108,
    (byte) 195,
    (byte) 201,
    (byte) 218,
    (byte) 182,
    (byte) 147,
    (byte) 171,
    (byte) 43,
    (byte) 57,
    (byte) 218,
    (byte) 177,
    (byte) 107,
    (byte) 108,
    (byte) 228,
    (byte) 31 /*0x1F*/,
    (byte) 201,
    (byte) 116,
    (byte) 71,
    (byte) 26,
    (byte) 212,
    (byte) 133,
    (byte) 252,
    (byte) 63 /*0x3F*/,
    (byte) 91,
    (byte) 23,
    (byte) 191,
    (byte) 36,
    (byte) 80 /*0x50*/,
    (byte) 14,
    (byte) 220,
    (byte) 65,
    (byte) 137,
    (byte) 176 /*0xB0*/,
    (byte) 214,
    (byte) 2,
    (byte) 6,
    (byte) 122,
    (byte) 180,
    (byte) 94,
    (byte) 123,
    (byte) 80 /*0x50*/,
    (byte) 206,
    (byte) 198,
    (byte) 33,
    (byte) 129,
    (byte) 164,
    (byte) 43,
    (byte) 131,
    (byte) 59,
    (byte) 216,
    (byte) 106,
    (byte) 213,
    (byte) 160 /*0xA0*/,
    (byte) 250,
    (byte) 189,
    (byte) 90,
    (byte) 74,
    (byte) 21,
    (byte) 158,
    (byte) 63 /*0x3F*/,
    (byte) 37,
    (byte) 163,
    (byte) 201,
    (byte) 209,
    (byte) 116,
    (byte) 22,
    (byte) 36,
    (byte) 146,
    (byte) 123,
    (byte) 2,
    (byte) 107,
    (byte) 215,
    (byte) 178,
    (byte) 238,
    (byte) 47,
    (byte) 108,
    (byte) 157,
    (byte) 57,
    (byte) 114,
    (byte) 123,
    (byte) 99,
    (byte) 114,
    (byte) 0,
    (byte) 120,
    (byte) 64 /*0x40*/,
    (byte) 23,
    (byte) 98,
    (byte) 146,
    (byte) 107,
    (byte) 55,
    (byte) 101,
    (byte) 3,
    (byte) 136,
    (byte) 129,
    (byte) 78,
    (byte) 161,
    (byte) 122,
    (byte) 121,
    (byte) 155,
    (byte) 129,
    (byte) 12,
    (byte) 168,
    (byte) 229,
    (byte) 245,
    (byte) 216,
    (byte) 27,
    (byte) 154,
    (byte) 173,
    (byte) 139,
    (byte) 94,
    (byte) 207,
    (byte) 20,
    (byte) 112 /*0x70*/,
    (byte) 153,
    (byte) 65,
    (byte) 129,
    (byte) 47,
    (byte) 156,
    (byte) 250,
    (byte) 126,
    (byte) 230,
    (byte) 173,
    (byte) 79,
    (byte) 243,
    (byte) 37,
    (byte) 205,
    (byte) 114,
    (byte) 237,
    (byte) 205,
    (byte) 171,
    (byte) 116,
    (byte) 190,
    (byte) 4,
    (byte) 125,
    (byte) 217,
    (byte) 41,
    (byte) 69,
    (byte) 232,
    (byte) 164,
    (byte) 180,
    (byte) 52,
    (byte) 19,
    (byte) 162,
    (byte) 161,
    (byte) 235,
    (byte) 23,
    (byte) 230,
    (byte) 242,
    (byte) 68,
    (byte) 43,
    (byte) 70,
    (byte) 100,
    (byte) 5,
    (byte) 204,
    (byte) 194,
    (byte) 224 /*0xE0*/,
    (byte) 82,
    (byte) 173,
    (byte) 180,
    (byte) 174,
    (byte) 177,
    (byte) 182,
    (byte) 36,
    (byte) 237,
    (byte) 13,
    (byte) 216,
    (byte) 27,
    (byte) 156,
    (byte) 108,
    (byte) 7,
    (byte) 0,
    (byte) 201,
    (byte) 199,
    (byte) 185,
    (byte) 40,
    byte.MaxValue,
    (byte) 99,
    (byte) 144 /*0x90*/,
    (byte) 24,
    (byte) 38,
    (byte) 248,
    (byte) 75,
    (byte) 148,
    (byte) 209,
    (byte) 157,
    (byte) 227,
    (byte) 242,
    (byte) 222,
    (byte) 253,
    (byte) 127 /*0x7F*/,
    (byte) 194,
    (byte) 3,
    (byte) 91,
    (byte) 82,
    (byte) 244,
    (byte) 9,
    (byte) 31 /*0x1F*/,
    (byte) 227,
    (byte) 251,
    (byte) 0,
    (byte) 48 /*0x30*/,
    (byte) 112 /*0x70*/,
    (byte) 234,
    (byte) 20,
    (byte) 146,
    (byte) 42,
    (byte) 162,
    (byte) 160 /*0xA0*/,
    (byte) 128 /*0x80*/,
    (byte) 150,
    (byte) 238,
    (byte) 60,
    (byte) 149,
    (byte) 212,
    (byte) 18,
    (byte) 45,
    (byte) 31 /*0x1F*/,
    (byte) 1,
    (byte) 30,
    (byte) 219,
    (byte) 110,
    (byte) 224 /*0xE0*/,
    (byte) 108,
    (byte) 77,
    (byte) 94,
    (byte) 223,
    (byte) 98,
    (byte) 52,
    (byte) 111,
    (byte) 13,
    (byte) 152,
    (byte) 62,
    (byte) 243,
    (byte) 1,
    (byte) 152,
    (byte) 223,
    (byte) 110,
    (byte) 186,
    (byte) 122,
    (byte) 47,
    (byte) 78,
    (byte) 58,
    (byte) 66,
    (byte) 94,
    (byte) 144 /*0x90*/,
    (byte) 179,
    (byte) 215,
    (byte) 206,
    (byte) 209,
    (byte) 132,
    (byte) 144 /*0x90*/,
    (byte) 39,
    (byte) 88,
    (byte) 122,
    (byte) 231,
    (byte) 75,
    (byte) 23,
    (byte) 13,
    (byte) 183,
    (byte) 188,
    (byte) 36,
    (byte) 203,
    (byte) 68,
    (byte) 212,
    (byte) 245,
    (byte) 45,
    (byte) 186,
    (byte) 39,
    (byte) 89,
    (byte) 1,
    (byte) 37,
    (byte) 67,
    (byte) 95,
    (byte) 107,
    (byte) 144 /*0x90*/,
    (byte) 209,
    (byte) 100,
    (byte) 42,
    (byte) 38,
    (byte) 34,
    (byte) 16 /*0x10*/,
    (byte) 254,
    (byte) 20,
    (byte) 236,
    (byte) 15,
    (byte) 115,
    (byte) 189,
    (byte) 28,
    (byte) 98,
    (byte) 253,
    (byte) 226,
    (byte) 11,
    (byte) 179,
    (byte) 200,
    (byte) 24,
    (byte) 67,
    (byte) 173,
    (byte) 44,
    (byte) 13,
    (byte) 3,
    (byte) 221,
    (byte) 249,
    (byte) 158,
    (byte) 244,
    (byte) 55,
    (byte) 204,
    (byte) 157,
    (byte) 102,
    (byte) 134,
    (byte) 63 /*0x3F*/,
    (byte) 47,
    (byte) 166,
    (byte) 179,
    (byte) 196,
    (byte) 55,
    (byte) 224 /*0xE0*/,
    (byte) 155,
    (byte) 202,
    (byte) 53,
    (byte) 219,
    (byte) 208 /*0xD0*/,
    (byte) 99,
    (byte) 135,
    (byte) 229,
    (byte) 27,
    (byte) 250,
    (byte) 32 /*0x20*/,
    (byte) 252,
    (byte) 198,
    (byte) 93,
    (byte) 83,
    (byte) 190,
    (byte) 35,
    (byte) 23,
    (byte) 22,
    (byte) 152,
    (byte) 105,
    (byte) 112 /*0x70*/,
    (byte) 185,
    (byte) 16 /*0x10*/,
    (byte) 184,
    (byte) 52,
    (byte) 105,
    (byte) 197,
    (byte) 32 /*0x20*/,
    (byte) 126,
    (byte) 17,
    (byte) 64 /*0x40*/,
    (byte) 188,
    (byte) 91,
    (byte) 205,
    (byte) 137,
    (byte) 193,
    (byte) 119,
    (byte) 46,
    (byte) 2,
    (byte) 168,
    (byte) 242,
    (byte) 198,
    (byte) 219,
    (byte) 30,
    (byte) 225,
    (byte) 132,
    (byte) 68,
    (byte) 144 /*0x90*/,
    (byte) 81,
    (byte) 107,
    (byte) 5,
    (byte) 49,
    (byte) 68,
    (byte) 204,
    (byte) 108,
    (byte) 202,
    (byte) 239,
    (byte) 100,
    (byte) 206,
    (byte) 247,
    (byte) 83,
    (byte) 63 /*0x3F*/,
    (byte) 4,
    (byte) 228,
    (byte) 5,
    (byte) 200,
    (byte) 231,
    (byte) 220,
    (byte) 75,
    (byte) 11,
    (byte) 135,
    (byte) 187,
    (byte) 171,
    (byte) 206,
    (byte) 225,
    (byte) 174,
    (byte) 153,
    (byte) 235,
    (byte) 80 /*0x50*/,
    (byte) 70,
    (byte) 64 /*0x40*/,
    (byte) 15,
    (byte) 90,
    (byte) 239,
    (byte) 90,
    (byte) 160 /*0xA0*/,
    (byte) 122,
    (byte) 234,
    (byte) 206,
    (byte) 107,
    (byte) 241,
    (byte) 80 /*0x50*/
  };

  internal static int ssp_appserver_13687(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 76;
    sourceArray1[23] = (byte) 102;
    sourceArray1[47] = (byte) 66;
    sourceArray1[30] = (byte) 78;
    sourceArray1[34] = (byte) 96 /*0x60*/;
    sourceArray1[22] = (byte) 226;
    sourceArray1[6] = (byte) 86;
    sourceArray1[7] = (byte) 82;
    sourceArray1[8] = (byte) 116;
    sourceArray1[38] = (byte) 13;
    sourceArray1[36] = (byte) 254;
    sourceArray1[11] = (byte) 153;
    sourceArray1[45] = (byte) 79;
    sourceArray1[13] = (byte) 120;
    sourceArray1[18] = (byte) 16 /*0x10*/;
    sourceArray1[41] = (byte) 191;
    sourceArray1[16 /*0x10*/] = (byte) 157;
    sourceArray1[33] = (byte) 38;
    sourceArray1[17] = (byte) 116;
    sourceArray1[19] = (byte) 176 /*0xB0*/;
    sourceArray1[15] = (byte) 16 /*0x10*/;
    sourceArray1[21] = (byte) 215;
    sourceArray1[3] = (byte) 192 /*0xC0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray1[24] = (byte) 236;
    sourceArray1[2] = (byte) 83;
    sourceArray1[14] = (byte) 0;
    sourceArray1[32 /*0x20*/] = (byte) 125;
    sourceArray1[28] = (byte) 227;
    sourceArray1[29] = (byte) 119;
    sourceArray1[0] = (byte) 103;
    sourceArray1[12] = (byte) 47;
    sourceArray1[1] = (byte) 96 /*0x60*/;
    sourceArray1[10] = (byte) 57;
    sourceArray1[20] = (byte) 78;
    sourceArray1[35] = (byte) 75;
    sourceArray1[39] = (byte) 96 /*0x60*/;
    sourceArray1[37] = (byte) 41;
    sourceArray1[25] = (byte) 192 /*0xC0*/;
    sourceArray1[9] = (byte) 192 /*0xC0*/;
    sourceArray1[40] = (byte) 9;
    sourceArray1[27] = (byte) 10;
    sourceArray1[42] = (byte) 14;
    sourceArray1[43] = (byte) 57;
    sourceArray1[44] = (byte) 114;
    sourceArray1[26] = (byte) 18;
    sourceArray1[46] = (byte) 34;
    sourceArray1[5] = (byte) 107;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[8] = (byte) 22;
    sourceArray2[1] = (byte) 70;
    sourceArray2[46] = (byte) 192 /*0xC0*/;
    sourceArray2[3] = (byte) 176 /*0xB0*/;
    sourceArray2[36] = (byte) 152;
    sourceArray2[47] = (byte) 68;
    sourceArray2[29] = (byte) 81;
    sourceArray2[7] = (byte) 66;
    sourceArray2[28] = (byte) 250;
    sourceArray2[0] = (byte) 113;
    sourceArray2[10] = (byte) 248;
    sourceArray2[11] = (byte) 7;
    sourceArray2[21] = (byte) 141;
    sourceArray2[32 /*0x20*/] = (byte) 29;
    sourceArray2[18] = (byte) 206;
    sourceArray2[15] = (byte) 20;
    sourceArray2[44] = (byte) 3;
    sourceArray2[23] = (byte) 89;
    sourceArray2[45] = (byte) 145;
    sourceArray2[19] = (byte) 212;
    sourceArray2[20] = (byte) 140;
    sourceArray2[17] = (byte) 187;
    sourceArray2[14] = (byte) 165;
    sourceArray2[4] = (byte) 18;
    sourceArray2[24] = (byte) 228;
    sourceArray2[25] = (byte) 153;
    sourceArray2[26] = (byte) 62;
    sourceArray2[27] = (byte) 141;
    sourceArray2[39] = (byte) 180;
    sourceArray2[5] = (byte) 178;
    sourceArray2[42] = (byte) 238;
    sourceArray2[31 /*0x1F*/] = (byte) 200;
    sourceArray2[38] = (byte) 72;
    sourceArray2[13] = (byte) 106;
    sourceArray2[16 /*0x10*/] = (byte) 221;
    sourceArray2[9] = (byte) 205;
    sourceArray2[37] = (byte) 9;
    sourceArray2[2] = (byte) 90;
    sourceArray2[34] = (byte) 196;
    sourceArray2[12] = (byte) 73;
    sourceArray2[40] = (byte) 132;
    sourceArray2[22] = (byte) 110;
    sourceArray2[35] = (byte) 239;
    sourceArray2[43] = (byte) 0;
    sourceArray2[6] = (byte) 45;
    sourceArray2[33] = (byte) 122;
    sourceArray2[30] = (byte) 23;
    sourceArray2[41] = (byte) 185;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13688(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 63 /*0x3F*/;
    sourceArray1[28] = (byte) 77;
    sourceArray1[37] = (byte) 160 /*0xA0*/;
    sourceArray1[9] = (byte) 78;
    sourceArray1[2] = (byte) 224 /*0xE0*/;
    sourceArray1[5] = (byte) 58;
    sourceArray1[6] = (byte) 74;
    sourceArray1[3] = (byte) 23;
    sourceArray1[8] = (byte) 225;
    sourceArray1[27] = (byte) 89;
    sourceArray1[10] = (byte) 210;
    sourceArray1[42] = (byte) 196;
    sourceArray1[19] = (byte) 204;
    sourceArray1[13] = (byte) 192 /*0xC0*/;
    sourceArray1[14] = (byte) 186;
    sourceArray1[15] = (byte) 147;
    sourceArray1[16 /*0x10*/] = (byte) 15;
    sourceArray1[36] = (byte) 93;
    sourceArray1[40] = (byte) 30;
    sourceArray1[32 /*0x20*/] = (byte) 0;
    sourceArray1[20] = (byte) 104;
    sourceArray1[33] = (byte) 81;
    sourceArray1[22] = (byte) 68;
    sourceArray1[23] = (byte) 70;
    sourceArray1[46] = (byte) 128 /*0x80*/;
    sourceArray1[25] = (byte) 240 /*0xF0*/;
    sourceArray1[12] = (byte) 51;
    sourceArray1[26] = (byte) 128 /*0x80*/;
    sourceArray1[18] = (byte) 175;
    sourceArray1[7] = (byte) 213;
    sourceArray1[4] = (byte) 34;
    sourceArray1[31 /*0x1F*/] = (byte) 232;
    sourceArray1[34] = (byte) 41;
    sourceArray1[38] = (byte) 191;
    sourceArray1[21] = (byte) 226;
    sourceArray1[35] = (byte) 88;
    sourceArray1[0] = (byte) 126;
    sourceArray1[39] = (byte) 14;
    sourceArray1[30] = (byte) 239;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[44] = (byte) 88;
    sourceArray1[41] = (byte) 175;
    sourceArray1[24] = (byte) 243;
    sourceArray1[45] = (byte) 38;
    sourceArray1[17] = (byte) 242;
    sourceArray1[11] = (byte) 223;
    sourceArray1[43] = (byte) 154;
    sourceArray1[47] = (byte) 46;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 245;
    sourceArray2[31 /*0x1F*/] = (byte) 159;
    sourceArray2[21] = (byte) 162;
    sourceArray2[3] = (byte) 113;
    sourceArray2[4] = (byte) 236;
    sourceArray2[30] = (byte) 197;
    sourceArray2[42] = (byte) 106;
    sourceArray2[7] = (byte) 48 /*0x30*/;
    sourceArray2[10] = (byte) 19;
    sourceArray2[41] = (byte) 204;
    sourceArray2[5] = (byte) 21;
    sourceArray2[47] = (byte) 113;
    sourceArray2[12] = (byte) 61;
    sourceArray2[13] = (byte) 153;
    sourceArray2[14] = (byte) 7;
    sourceArray2[33] = (byte) 161;
    sourceArray2[16 /*0x10*/] = (byte) 7;
    sourceArray2[17] = (byte) 162;
    sourceArray2[6] = (byte) 72;
    sourceArray2[29] = (byte) 105;
    sourceArray2[36] = (byte) 20;
    sourceArray2[22] = (byte) 233;
    sourceArray2[20] = (byte) 42;
    sourceArray2[19] = (byte) 144 /*0x90*/;
    sourceArray2[0] = (byte) 198;
    sourceArray2[25] = (byte) 141;
    sourceArray2[44] = (byte) 196;
    sourceArray2[27] = (byte) 121;
    sourceArray2[28] = (byte) 19;
    sourceArray2[23] = (byte) 235;
    sourceArray2[9] = (byte) 91;
    sourceArray2[2] = (byte) 25;
    sourceArray2[43] = (byte) 144 /*0x90*/;
    sourceArray2[15] = (byte) 8;
    sourceArray2[34] = (byte) 138;
    sourceArray2[18] = (byte) 26;
    sourceArray2[24] = (byte) 128 /*0x80*/;
    sourceArray2[37] = (byte) 218;
    sourceArray2[38] = (byte) 97;
    sourceArray2[8] = (byte) 48 /*0x30*/;
    sourceArray2[40] = (byte) 183;
    sourceArray2[45] = (byte) 184;
    sourceArray2[11] = (byte) 92;
    sourceArray2[26] = (byte) 100;
    sourceArray2[32 /*0x20*/] = (byte) 68;
    sourceArray2[46] = (byte) 142;
    sourceArray2[1] = (byte) 254;
    sourceArray2[35] = (byte) 94;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13689(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 226;
    sourceArray1[6] = (byte) 22;
    sourceArray1[2] = (byte) 136;
    sourceArray1[3] = (byte) 199;
    sourceArray1[37] = (byte) 51;
    sourceArray1[5] = (byte) 4;
    sourceArray1[15] = (byte) 223;
    sourceArray1[0] = (byte) 236;
    sourceArray1[4] = (byte) 199;
    sourceArray1[43] = (byte) 245;
    sourceArray1[10] = (byte) 186;
    sourceArray1[11] = (byte) 152;
    sourceArray1[12] = (byte) 45;
    sourceArray1[13] = (byte) 124;
    sourceArray1[14] = (byte) 36;
    sourceArray1[18] = (byte) 44;
    sourceArray1[16 /*0x10*/] = (byte) 129;
    sourceArray1[32 /*0x20*/] = (byte) 204;
    sourceArray1[21] = (byte) 174;
    sourceArray1[46] = (byte) 233;
    sourceArray1[30] = (byte) 236;
    sourceArray1[20] = (byte) 238;
    sourceArray1[17] = (byte) 51;
    sourceArray1[23] = (byte) 224 /*0xE0*/;
    sourceArray1[24] = (byte) 158;
    sourceArray1[34] = (byte) 95;
    sourceArray1[26] = (byte) 178;
    sourceArray1[27] = (byte) 39;
    sourceArray1[8] = (byte) 142;
    sourceArray1[29] = (byte) 248;
    sourceArray1[28] = (byte) 8;
    sourceArray1[31 /*0x1F*/] = (byte) 92;
    sourceArray1[9] = (byte) 28;
    sourceArray1[33] = (byte) 214;
    sourceArray1[1] = (byte) 192 /*0xC0*/;
    sourceArray1[35] = (byte) 191;
    sourceArray1[36] = (byte) 9;
    sourceArray1[22] = (byte) 90;
    sourceArray1[38] = (byte) 166;
    sourceArray1[39] = (byte) 123;
    sourceArray1[40] = (byte) 110;
    sourceArray1[41] = (byte) 240 /*0xF0*/;
    sourceArray1[42] = (byte) 42;
    sourceArray1[47] = (byte) 59;
    sourceArray1[44] = (byte) 3;
    sourceArray1[45] = (byte) 36;
    sourceArray1[19] = (byte) 209;
    sourceArray1[25] = (byte) 92;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[3] = (byte) 34;
    sourceArray2[26] = (byte) 121;
    sourceArray2[2] = (byte) 179;
    sourceArray2[47] = (byte) 89;
    sourceArray2[4] = (byte) 123;
    sourceArray2[16 /*0x10*/] = (byte) 77;
    sourceArray2[6] = (byte) 61;
    sourceArray2[7] = (byte) 80 /*0x50*/;
    sourceArray2[42] = (byte) 71;
    sourceArray2[10] = (byte) 6;
    sourceArray2[11] = (byte) 231;
    sourceArray2[37] = (byte) 244;
    sourceArray2[1] = (byte) 99;
    sourceArray2[13] = (byte) 72;
    sourceArray2[36] = (byte) 23;
    sourceArray2[5] = (byte) 66;
    sourceArray2[33] = (byte) 107;
    sourceArray2[39] = (byte) 129;
    sourceArray2[18] = (byte) 114;
    sourceArray2[19] = (byte) 79;
    sourceArray2[20] = (byte) 200;
    sourceArray2[32 /*0x20*/] = (byte) 133;
    sourceArray2[22] = (byte) 14;
    sourceArray2[23] = (byte) 232;
    sourceArray2[0] = (byte) 54;
    sourceArray2[25] = (byte) 137;
    sourceArray2[30] = (byte) 235;
    sourceArray2[27] = (byte) 76;
    sourceArray2[8] = (byte) 153;
    sourceArray2[31 /*0x1F*/] = (byte) 155;
    sourceArray2[28] = (byte) 38;
    sourceArray2[35] = (byte) 251;
    sourceArray2[12] = (byte) 134;
    sourceArray2[17] = (byte) 0;
    sourceArray2[34] = (byte) 29;
    sourceArray2[9] = (byte) 79;
    sourceArray2[15] = (byte) 179;
    sourceArray2[14] = (byte) 204;
    sourceArray2[38] = (byte) 193;
    sourceArray2[29] = (byte) 219;
    sourceArray2[46] = (byte) 246;
    sourceArray2[41] = (byte) 75;
    sourceArray2[24] = (byte) 44;
    sourceArray2[43] = (byte) 28;
    sourceArray2[44] = (byte) 102;
    sourceArray2[45] = (byte) 219;
    sourceArray2[40] = (byte) 78;
    sourceArray2[21] = (byte) 247;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13690(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 77,
      (byte) 210,
      (byte) 114,
      (byte) 103,
      (byte) 74,
      (byte) 185,
      (byte) 5,
      (byte) 49,
      (byte) 34,
      (byte) 116,
      (byte) 137,
      (byte) 254,
      (byte) 88,
      (byte) 177,
      (byte) 14,
      (byte) 251,
      (byte) 202,
      (byte) 98,
      (byte) 148,
      (byte) 140,
      (byte) 25,
      (byte) 131,
      (byte) 234,
      (byte) 108,
      (byte) 5,
      (byte) 109,
      (byte) 179,
      (byte) 204,
      (byte) 76,
      (byte) 148,
      (byte) 180,
      (byte) 221,
      (byte) 230,
      (byte) 14,
      (byte) 58,
      (byte) 16 /*0x10*/,
      (byte) 115,
      (byte) 135,
      (byte) 85,
      (byte) 125,
      (byte) 28,
      (byte) 173,
      (byte) 54,
      (byte) 78,
      (byte) 174,
      (byte) 40,
      (byte) 180,
      (byte) 135
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 125,
      (byte) 119,
      (byte) 183,
      (byte) 215,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 146,
      (byte) 150,
      (byte) 162,
      (byte) 181,
      (byte) 73,
      (byte) 60,
      (byte) 11,
      (byte) 70,
      (byte) 32 /*0x20*/,
      (byte) 222,
      (byte) 59,
      (byte) 140,
      (byte) 26,
      (byte) 13,
      (byte) 162,
      (byte) 115,
      (byte) 68,
      (byte) 67,
      (byte) 91,
      (byte) 146,
      (byte) 8,
      (byte) 251,
      (byte) 196,
      (byte) 25,
      (byte) 180,
      (byte) 129,
      (byte) 224 /*0xE0*/,
      (byte) 80 /*0x50*/,
      (byte) 64 /*0x40*/,
      (byte) 123,
      (byte) 195,
      (byte) 67,
      (byte) 235,
      (byte) 25,
      (byte) 237,
      (byte) 8,
      (byte) 142,
      (byte) 47,
      (byte) 55,
      (byte) 53,
      (byte) 152,
      (byte) 204
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13691(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 208 /*0xD0*/,
      (byte) 40,
      (byte) 202,
      (byte) 232,
      (byte) 6,
      (byte) 98,
      (byte) 220,
      (byte) 5,
      (byte) 56,
      (byte) 229,
      (byte) 141,
      (byte) 191,
      (byte) 171,
      (byte) 24,
      (byte) 73,
      (byte) 118,
      (byte) 254,
      (byte) 135,
      (byte) 66,
      (byte) 12,
      (byte) 22,
      (byte) 111,
      (byte) 107,
      (byte) 76,
      (byte) 156,
      (byte) 189,
      (byte) 220,
      (byte) 185,
      (byte) 44,
      (byte) 84,
      (byte) 51,
      (byte) 246,
      (byte) 191,
      (byte) 219,
      (byte) 21,
      (byte) 230,
      (byte) 112 /*0x70*/,
      (byte) 39,
      (byte) 108,
      (byte) 37,
      (byte) 120,
      (byte) 120,
      (byte) 228,
      (byte) 200,
      (byte) 168,
      (byte) 142,
      (byte) 3,
      (byte) 16 /*0x10*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 179,
      (byte) 203,
      (byte) 53,
      (byte) 194,
      (byte) 178,
      (byte) 190,
      (byte) 175,
      (byte) 111,
      (byte) 5,
      (byte) 177,
      (byte) 84,
      (byte) 166,
      (byte) 49,
      (byte) 11,
      (byte) 134,
      (byte) 30,
      (byte) 9,
      (byte) 134,
      (byte) 226,
      (byte) 218,
      (byte) 46,
      (byte) 28,
      (byte) 203,
      (byte) 36,
      (byte) 179,
      (byte) 175,
      (byte) 223,
      (byte) 73,
      (byte) 233,
      (byte) 191,
      (byte) 29,
      (byte) 42,
      (byte) 167,
      (byte) 139,
      (byte) 64 /*0x40*/,
      (byte) 156,
      (byte) 4,
      (byte) 203,
      (byte) 227,
      (byte) 247,
      (byte) 235,
      (byte) 228,
      (byte) 92,
      (byte) 148,
      (byte) 229,
      (byte) 129,
      (byte) 220,
      (byte) 116
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[28];
    byte[] response2 = new byte[28];
    Array.Copy((Array) sc_13686.sspq, 0, (Array) numArray2, 0, 28);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 0, (Array) numArray2, 0, 28);
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

  internal static int ssp_appserver_13692(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 77,
      (byte) 27,
      (byte) 218,
      (byte) 161,
      (byte) 155,
      (byte) 215,
      (byte) 239,
      (byte) 180,
      (byte) 40,
      (byte) 243,
      (byte) 218,
      (byte) 35,
      (byte) 159,
      (byte) 85,
      (byte) 159,
      (byte) 0,
      (byte) 22,
      (byte) 17,
      (byte) 142,
      (byte) 51,
      (byte) 13,
      (byte) 8,
      (byte) 11,
      (byte) 163,
      (byte) 118,
      (byte) 61,
      (byte) 84,
      (byte) 78,
      (byte) 214,
      (byte) 26,
      (byte) 217,
      (byte) 63 /*0x3F*/,
      (byte) 144 /*0x90*/,
      (byte) 184,
      (byte) 213,
      (byte) 189,
      (byte) 8,
      (byte) 15,
      (byte) 251,
      (byte) 40,
      (byte) 224 /*0xE0*/,
      (byte) 77,
      (byte) 58,
      (byte) 18,
      (byte) 251,
      (byte) 247,
      (byte) 125,
      (byte) 62
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 138,
      (byte) 143,
      (byte) 180,
      (byte) 29,
      (byte) 183,
      (byte) 144 /*0x90*/,
      (byte) 234,
      (byte) 240 /*0xF0*/,
      (byte) 231,
      (byte) 8,
      (byte) 10,
      (byte) 195,
      (byte) 152,
      (byte) 10,
      (byte) 251,
      (byte) 89,
      (byte) 101,
      (byte) 30,
      (byte) 89,
      (byte) 70,
      (byte) 124,
      (byte) 222,
      (byte) 216,
      (byte) 34,
      (byte) 140,
      (byte) 92,
      (byte) 181,
      (byte) 226,
      (byte) 71,
      (byte) 43,
      (byte) 117,
      (byte) 73,
      (byte) 146,
      (byte) 178,
      (byte) 135,
      (byte) 71,
      (byte) 166,
      (byte) 139,
      (byte) 201,
      (byte) 103,
      (byte) 156,
      (byte) 201,
      (byte) 169,
      (byte) 29,
      (byte) 56,
      (byte) 56,
      (byte) 1,
      (byte) 118
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13693(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 133,
      (byte) 190,
      (byte) 69,
      (byte) 15,
      (byte) 55,
      (byte) 43,
      (byte) 94,
      (byte) 131,
      (byte) 161,
      (byte) 222,
      (byte) 14,
      (byte) 25,
      (byte) 155,
      (byte) 146,
      (byte) 231,
      (byte) 127 /*0x7F*/,
      (byte) 225,
      (byte) 247,
      (byte) 6,
      (byte) 22,
      (byte) 101,
      (byte) 83,
      (byte) 191,
      (byte) 206,
      (byte) 249,
      (byte) 167,
      (byte) 126,
      (byte) 191,
      (byte) 21,
      (byte) 79,
      (byte) 75,
      (byte) 142,
      (byte) 71,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      (byte) 249,
      (byte) 143,
      (byte) 173,
      (byte) 209,
      (byte) 228,
      (byte) 156,
      (byte) 165,
      (byte) 62,
      (byte) 111,
      (byte) 43,
      (byte) 248,
      (byte) 96 /*0x60*/,
      (byte) 100
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 141;
    sourceArray2[5] = (byte) 0;
    sourceArray2[29] = (byte) 118;
    sourceArray2[3] = (byte) 3;
    sourceArray2[27] = (byte) 83;
    sourceArray2[13] = (byte) 75;
    sourceArray2[6] = (byte) 129;
    sourceArray2[47] = (byte) 27;
    sourceArray2[2] = (byte) 228;
    sourceArray2[14] = (byte) 218;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[33] = (byte) 163;
    sourceArray2[4] = (byte) 32 /*0x20*/;
    sourceArray2[37] = (byte) 85;
    sourceArray2[44] = (byte) 209;
    sourceArray2[15] = (byte) 38;
    sourceArray2[7] = (byte) 221;
    sourceArray2[17] = (byte) 15;
    sourceArray2[31 /*0x1F*/] = (byte) 83;
    sourceArray2[35] = (byte) 244;
    sourceArray2[20] = (byte) 178;
    sourceArray2[25] = (byte) 60;
    sourceArray2[22] = (byte) 243;
    sourceArray2[30] = (byte) 217;
    sourceArray2[41] = (byte) 151;
    sourceArray2[45] = (byte) 174;
    sourceArray2[42] = (byte) 64 /*0x40*/;
    sourceArray2[28] = (byte) 167;
    sourceArray2[39] = (byte) 176 /*0xB0*/;
    sourceArray2[8] = (byte) 84;
    sourceArray2[26] = (byte) 148;
    sourceArray2[21] = (byte) 181;
    sourceArray2[32 /*0x20*/] = (byte) 26;
    sourceArray2[12] = (byte) 25;
    sourceArray2[34] = (byte) 226;
    sourceArray2[16 /*0x10*/] = (byte) 133;
    sourceArray2[36] = (byte) 116;
    sourceArray2[10] = (byte) 63 /*0x3F*/;
    sourceArray2[38] = (byte) 141;
    sourceArray2[23] = (byte) 249;
    sourceArray2[40] = (byte) 171;
    sourceArray2[0] = (byte) 129;
    sourceArray2[18] = (byte) 14;
    sourceArray2[43] = (byte) 135;
    sourceArray2[11] = (byte) 195;
    sourceArray2[24] = (byte) 120;
    sourceArray2[46] = (byte) 12;
    sourceArray2[9] = (byte) 193;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13694(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[22] = (byte) 175;
    sourceArray1[13] = (byte) 72;
    sourceArray1[45] = (byte) 67;
    sourceArray1[3] = (byte) 206;
    sourceArray1[31 /*0x1F*/] = (byte) 115;
    sourceArray1[41] = (byte) 199;
    sourceArray1[6] = (byte) 98;
    sourceArray1[7] = (byte) 3;
    sourceArray1[43] = (byte) 187;
    sourceArray1[0] = (byte) 133;
    sourceArray1[10] = (byte) 174;
    sourceArray1[11] = (byte) 212;
    sourceArray1[12] = (byte) 21;
    sourceArray1[47] = (byte) 249;
    sourceArray1[4] = (byte) 232;
    sourceArray1[15] = (byte) 116;
    sourceArray1[27] = (byte) 234;
    sourceArray1[9] = (byte) 28;
    sourceArray1[35] = (byte) 60;
    sourceArray1[19] = (byte) 119;
    sourceArray1[2] = (byte) 169;
    sourceArray1[20] = (byte) 8;
    sourceArray1[24] = (byte) 215;
    sourceArray1[25] = (byte) 157;
    sourceArray1[30] = (byte) 39;
    sourceArray1[1] = (byte) 220;
    sourceArray1[14] = (byte) 146;
    sourceArray1[5] = (byte) 109;
    sourceArray1[28] = (byte) 122;
    sourceArray1[29] = (byte) 136;
    sourceArray1[17] = (byte) 30;
    sourceArray1[18] = (byte) 218;
    sourceArray1[32 /*0x20*/] = (byte) 247;
    sourceArray1[23] = (byte) 242;
    sourceArray1[34] = (byte) 193;
    sourceArray1[21] = (byte) 52;
    sourceArray1[36] = (byte) 128 /*0x80*/;
    sourceArray1[37] = (byte) 135;
    sourceArray1[8] = (byte) 88;
    sourceArray1[39] = (byte) 75;
    sourceArray1[40] = (byte) 165;
    sourceArray1[26] = (byte) 227;
    sourceArray1[42] = (byte) 78;
    sourceArray1[16 /*0x10*/] = (byte) 23;
    sourceArray1[44] = (byte) 76;
    sourceArray1[38] = (byte) 74;
    sourceArray1[46] = (byte) 129;
    sourceArray1[33] = (byte) 219;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[11] = (byte) 82;
    sourceArray2[1] = (byte) 13;
    sourceArray2[38] = (byte) 44;
    sourceArray2[22] = (byte) 68;
    sourceArray2[19] = (byte) 6;
    sourceArray2[5] = (byte) 233;
    sourceArray2[28] = (byte) 104;
    sourceArray2[7] = (byte) 179;
    sourceArray2[8] = (byte) 166;
    sourceArray2[9] = (byte) 155;
    sourceArray2[33] = (byte) 163;
    sourceArray2[23] = (byte) 15;
    sourceArray2[39] = (byte) 235;
    sourceArray2[13] = (byte) 91;
    sourceArray2[30] = (byte) 238;
    sourceArray2[34] = (byte) 88;
    sourceArray2[16 /*0x10*/] = (byte) 108;
    sourceArray2[27] = (byte) 177;
    sourceArray2[18] = (byte) 214;
    sourceArray2[41] = (byte) 152;
    sourceArray2[4] = (byte) 16 /*0x10*/;
    sourceArray2[21] = (byte) 223;
    sourceArray2[36] = (byte) 183;
    sourceArray2[17] = (byte) 212;
    sourceArray2[45] = (byte) 33;
    sourceArray2[25] = (byte) 143;
    sourceArray2[12] = (byte) 11;
    sourceArray2[10] = (byte) 73;
    sourceArray2[14] = (byte) 116;
    sourceArray2[29] = (byte) 11;
    sourceArray2[47] = (byte) 6;
    sourceArray2[31 /*0x1F*/] = (byte) 151;
    sourceArray2[32 /*0x20*/] = (byte) 32 /*0x20*/;
    sourceArray2[3] = (byte) 248;
    sourceArray2[46] = (byte) 137;
    sourceArray2[35] = (byte) 242;
    sourceArray2[2] = (byte) 101;
    sourceArray2[37] = (byte) 19;
    sourceArray2[6] = (byte) 45;
    sourceArray2[0] = (byte) 151;
    sourceArray2[20] = (byte) 170;
    sourceArray2[40] = (byte) 214;
    sourceArray2[42] = (byte) 82;
    sourceArray2[43] = (byte) 0;
    sourceArray2[44] = (byte) 6;
    sourceArray2[26] = (byte) 136;
    sourceArray2[24] = (byte) 205;
    sourceArray2[15] = (byte) 2;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_13686.sspq, 28, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 28, (Array) numArray2, 0, 37);
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

  internal static string ssp_appserver_13695()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[208 /*0xD0*/];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 192 /*0xC0*/;
      numArray2[6] = (byte) 234;
      numArray2[18] = (byte) 250;
      numArray2[3] = (byte) 225;
      numArray2[28] = (byte) 246;
      numArray2[21] = (byte) 134;
      numArray2[54] = (byte) 238;
      numArray2[7] = (byte) 242;
      numArray2[8] = (byte) 68;
      numArray2[9] = (byte) 53;
      numArray2[10] = (byte) 88;
      numArray2[11] = (byte) 165;
      numArray2[48 /*0x30*/] = (byte) 172;
      numArray2[26] = (byte) 77;
      numArray2[44] = (byte) 114;
      numArray2[15] = (byte) 58;
      numArray2[16 /*0x10*/] = (byte) 249;
      numArray2[17] = (byte) 114;
      numArray2[32 /*0x20*/] = (byte) 16 /*0x10*/;
      numArray2[14] = (byte) 198;
      numArray2[52] = (byte) 176 /*0xB0*/;
      numArray2[33] = (byte) 126;
      numArray2[5] = (byte) 25;
      numArray2[23] = (byte) 126;
      numArray2[24] = (byte) 150;
      numArray2[25] = (byte) 48 /*0x30*/;
      numArray2[1] = (byte) 3;
      numArray2[31 /*0x1F*/] = (byte) 250;
      numArray2[30] = (byte) 35;
      numArray2[39] = (byte) 14;
      numArray2[35] = (byte) 10;
      numArray2[19] = (byte) 183;
      numArray2[0] = (byte) 80 /*0x50*/;
      numArray2[34] = (byte) 179;
      numArray2[51] = (byte) 145;
      numArray2[45] = (byte) 16 /*0x10*/;
      numArray2[36] = (byte) 51;
      numArray2[12] = (byte) 7;
      numArray2[38] = (byte) 84;
      numArray2[20] = (byte) 108;
      numArray2[13] = (byte) 1;
      numArray2[41] = (byte) 72;
      numArray2[42] = (byte) 75;
      numArray2[2] = (byte) 74;
      numArray2[4] = (byte) 7;
      numArray2[40] = (byte) 86;
      numArray2[22] = (byte) 232;
      numArray2[47] = (byte) 13;
      numArray2[37] = (byte) 83;
      numArray2[49] = (byte) 57;
      numArray2[50] = (byte) 216;
      numArray2[46] = (byte) 147;
      numArray2[43] = (byte) 11;
      numArray2[53] = (byte) 244;
      numArray2[27] = (byte) 151;
      byte[] numArray3 = new byte[55];
      numArray3[46] = (byte) 58;
      numArray3[1] = (byte) 148;
      numArray3[2] = (byte) 59;
      numArray3[25] = (byte) 36;
      numArray3[23] = (byte) 80 /*0x50*/;
      numArray3[5] = (byte) 197;
      numArray3[51] = (byte) 66;
      numArray3[43] = byte.MaxValue;
      numArray3[31 /*0x1F*/] = (byte) 146;
      numArray3[26] = (byte) 164;
      numArray3[20] = (byte) 51;
      numArray3[11] = (byte) 16 /*0x10*/;
      numArray3[21] = (byte) 202;
      numArray3[13] = (byte) 63 /*0x3F*/;
      numArray3[47] = (byte) 238;
      numArray3[15] = (byte) 232;
      numArray3[38] = (byte) 100;
      numArray3[17] = (byte) 195;
      numArray3[4] = (byte) 25;
      numArray3[19] = (byte) 34;
      numArray3[18] = (byte) 67;
      numArray3[29] = (byte) 125;
      numArray3[22] = (byte) 33;
      numArray3[16 /*0x10*/] = (byte) 5;
      numArray3[36] = (byte) 247;
      numArray3[40] = (byte) 18;
      numArray3[27] = (byte) 242;
      numArray3[35] = (byte) 170;
      numArray3[28] = (byte) 192 /*0xC0*/;
      numArray3[39] = (byte) 9;
      numArray3[6] = (byte) 132;
      numArray3[24] = (byte) 38;
      numArray3[32 /*0x20*/] = (byte) 67;
      numArray3[8] = byte.MaxValue;
      numArray3[34] = (byte) 201;
      numArray3[50] = (byte) 201;
      numArray3[9] = (byte) 231;
      numArray3[37] = (byte) 127 /*0x7F*/;
      numArray3[14] = (byte) 42;
      numArray3[10] = (byte) 210;
      numArray3[7] = (byte) 93;
      numArray3[48 /*0x30*/] = (byte) 130;
      numArray3[42] = (byte) 132;
      numArray3[30] = (byte) 30;
      numArray3[44] = (byte) 48 /*0x30*/;
      numArray3[41] = (byte) 141;
      numArray3[3] = (byte) 224 /*0xE0*/;
      numArray3[12] = (byte) 73;
      numArray3[33] = (byte) 196;
      numArray3[49] = (byte) 35;
      numArray3[45] = (byte) 22;
      numArray3[0] = (byte) 245;
      numArray3[52] = (byte) 93;
      numArray3[53] = (byte) 73;
      numArray3[54] = (byte) 163;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 199,
        (byte) 184,
        (byte) 169,
        (byte) 11,
        (byte) 107,
        (byte) 179,
        (byte) 229,
        (byte) 100,
        (byte) 142,
        (byte) 241,
        (byte) 50,
        (byte) 8,
        (byte) 22,
        (byte) 245,
        (byte) 87,
        (byte) 93,
        (byte) 242,
        (byte) 190,
        (byte) 202,
        (byte) 69,
        (byte) 107,
        (byte) 246,
        (byte) 10,
        (byte) 125,
        (byte) 36,
        (byte) 212,
        (byte) 246,
        (byte) 31 /*0x1F*/,
        (byte) 72,
        (byte) 104,
        (byte) 80 /*0x50*/,
        (byte) 212,
        (byte) 62,
        (byte) 91,
        (byte) 225,
        (byte) 140,
        (byte) 153,
        (byte) 170,
        (byte) 50,
        (byte) 90,
        (byte) 71,
        (byte) 20,
        (byte) 115,
        (byte) 15,
        (byte) 225,
        (byte) 82,
        (byte) 15,
        (byte) 159,
        (byte) 63 /*0x3F*/,
        (byte) 119,
        (byte) 42,
        (byte) 120,
        (byte) 38,
        (byte) 214,
        (byte) 236
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 147,
        (byte) 12,
        (byte) 174,
        (byte) 109,
        (byte) 54,
        (byte) 45,
        (byte) 202,
        (byte) 162,
        (byte) 233,
        (byte) 138,
        (byte) 117,
        (byte) 38,
        (byte) 23,
        (byte) 120,
        (byte) 21,
        (byte) 71,
        (byte) 249,
        (byte) 16 /*0x10*/,
        (byte) 119,
        (byte) 95,
        (byte) 225,
        (byte) 187,
        (byte) 13,
        (byte) 124,
        (byte) 112 /*0x70*/,
        (byte) 182,
        (byte) 90,
        (byte) 1,
        (byte) 86,
        (byte) 83,
        (byte) 182,
        (byte) 42,
        (byte) 238,
        (byte) 245,
        (byte) 248,
        (byte) 87,
        (byte) 250,
        (byte) 26,
        (byte) 211,
        (byte) 190,
        (byte) 140,
        (byte) 221,
        (byte) 232,
        (byte) 174,
        (byte) 219,
        (byte) 19,
        (byte) 67,
        (byte) 155,
        (byte) 22,
        (byte) 51,
        (byte) 132,
        (byte) 61,
        (byte) 115,
        (byte) 112 /*0x70*/,
        (byte) 12
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[37] = (byte) 157;
      numArray6[51] = (byte) 216;
      numArray6[2] = (byte) 150;
      numArray6[36] = (byte) 155;
      numArray6[4] = (byte) 198;
      numArray6[16 /*0x10*/] = (byte) 112 /*0x70*/;
      numArray6[6] = (byte) 38;
      numArray6[7] = (byte) 161;
      numArray6[50] = (byte) 182;
      numArray6[10] = (byte) 187;
      numArray6[5] = (byte) 27;
      numArray6[47] = (byte) 179;
      numArray6[12] = (byte) 202;
      numArray6[13] = (byte) 195;
      numArray6[54] = (byte) 179;
      numArray6[8] = (byte) 93;
      numArray6[44] = (byte) 83;
      numArray6[17] = (byte) 254;
      numArray6[18] = (byte) 87;
      numArray6[11] = (byte) 19;
      numArray6[20] = (byte) 251;
      numArray6[38] = (byte) 129;
      numArray6[22] = (byte) 209;
      numArray6[23] = (byte) 197;
      numArray6[24] = (byte) 87;
      numArray6[25] = (byte) 224 /*0xE0*/;
      numArray6[3] = (byte) 188;
      numArray6[27] = (byte) 62;
      numArray6[28] = (byte) 216;
      numArray6[29] = (byte) 173;
      numArray6[0] = (byte) 44;
      numArray6[46] = (byte) 116;
      numArray6[26] = (byte) 209;
      numArray6[15] = (byte) 140;
      numArray6[14] = (byte) 71;
      numArray6[35] = (byte) 195;
      numArray6[52] = (byte) 149;
      numArray6[39] = (byte) 249;
      numArray6[21] = (byte) 156;
      numArray6[30] = (byte) 103;
      numArray6[40] = (byte) 76;
      numArray6[41] = (byte) 110;
      numArray6[53] = (byte) 78;
      numArray6[34] = (byte) 109;
      numArray6[42] = (byte) 131;
      numArray6[45] = (byte) 139;
      numArray6[31 /*0x1F*/] = (byte) 110;
      numArray6[43] = (byte) 136;
      numArray6[32 /*0x20*/] = (byte) 16 /*0x10*/;
      numArray6[49] = (byte) 235;
      numArray6[48 /*0x30*/] = (byte) 226;
      numArray6[9] = (byte) 202;
      numArray6[33] = (byte) 222;
      numArray6[1] = (byte) 231;
      numArray6[19] = (byte) 131;
      byte[] numArray7 = new byte[55];
      numArray7[27] = (byte) 174;
      numArray7[0] = (byte) 128 /*0x80*/;
      numArray7[2] = (byte) 7;
      numArray7[53] = (byte) 30;
      numArray7[8] = (byte) 94;
      numArray7[5] = (byte) 67;
      numArray7[34] = (byte) 209;
      numArray7[3] = (byte) 252;
      numArray7[32 /*0x20*/] = (byte) 19;
      numArray7[9] = (byte) 188;
      numArray7[26] = (byte) 248;
      numArray7[48 /*0x30*/] = (byte) 225;
      numArray7[38] = (byte) 44;
      numArray7[12] = (byte) 47;
      numArray7[14] = (byte) 4;
      numArray7[44] = (byte) 111;
      numArray7[43] = (byte) 179;
      numArray7[36] = (byte) 130;
      numArray7[22] = (byte) 37;
      numArray7[42] = (byte) 52;
      numArray7[13] = (byte) 6;
      numArray7[21] = (byte) 218;
      numArray7[31 /*0x1F*/] = (byte) 203;
      numArray7[11] = (byte) 246;
      numArray7[24] = (byte) 110;
      numArray7[25] = (byte) 233;
      numArray7[10] = (byte) 180;
      numArray7[6] = (byte) 66;
      numArray7[28] = (byte) 243;
      numArray7[41] = (byte) 203;
      numArray7[30] = (byte) 99;
      numArray7[54] = (byte) 245;
      numArray7[1] = (byte) 169;
      numArray7[7] = (byte) 201;
      numArray7[4] = (byte) 157;
      numArray7[35] = (byte) 103;
      numArray7[15] = (byte) 151;
      numArray7[37] = (byte) 200;
      numArray7[40] = (byte) 1;
      numArray7[39] = (byte) 43;
      numArray7[18] = (byte) 137;
      numArray7[16 /*0x10*/] = (byte) 74;
      numArray7[17] = (byte) 195;
      numArray7[19] = (byte) 237;
      numArray7[45] = (byte) 123;
      numArray7[29] = (byte) 196;
      numArray7[46] = (byte) 62;
      numArray7[47] = (byte) 10;
      numArray7[20] = (byte) 159;
      numArray7[49] = (byte) 171;
      numArray7[50] = (byte) 27;
      numArray7[51] = (byte) 113;
      numArray7[52] = (byte) 199;
      numArray7[33] = (byte) 66;
      numArray7[23] = (byte) 233;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[43]
      {
        (byte) 242,
        (byte) 84,
        (byte) 151,
        (byte) 108,
        (byte) 142,
        (byte) 232,
        (byte) 164,
        (byte) 160 /*0xA0*/,
        (byte) 212,
        (byte) 151,
        (byte) 187,
        (byte) 18,
        (byte) 248,
        (byte) 188,
        (byte) 251,
        (byte) 192 /*0xC0*/,
        (byte) 43,
        (byte) 249,
        (byte) 28,
        (byte) 43,
        (byte) 207,
        (byte) 220,
        (byte) 253,
        (byte) 26,
        (byte) 218,
        (byte) 120,
        (byte) 31 /*0x1F*/,
        (byte) 204,
        (byte) 184,
        (byte) 23,
        (byte) 100,
        (byte) 198,
        (byte) 52,
        (byte) 51,
        (byte) 120,
        (byte) 75,
        (byte) 137,
        (byte) 23,
        (byte) 74,
        (byte) 225,
        (byte) 116,
        (byte) 178,
        (byte) 187
      };
      byte[] numArray9 = new byte[43];
      numArray9[32 /*0x20*/] = (byte) 202;
      numArray9[39] = (byte) 177;
      numArray9[27] = (byte) 104;
      numArray9[3] = (byte) 40;
      numArray9[1] = (byte) 72;
      numArray9[5] = (byte) 248;
      numArray9[0] = (byte) 195;
      numArray9[7] = (byte) 38;
      numArray9[42] = (byte) 126;
      numArray9[36] = (byte) 81;
      numArray9[29] = (byte) 30;
      numArray9[4] = (byte) 172;
      numArray9[12] = (byte) 152;
      numArray9[13] = (byte) 194;
      numArray9[15] = (byte) 86;
      numArray9[34] = (byte) 91;
      numArray9[16 /*0x10*/] = (byte) 166;
      numArray9[28] = (byte) 35;
      numArray9[38] = (byte) 103;
      numArray9[19] = (byte) 198;
      numArray9[10] = (byte) 88;
      numArray9[18] = (byte) 190;
      numArray9[22] = (byte) 132;
      numArray9[23] = (byte) 60;
      numArray9[24] = (byte) 248;
      numArray9[31 /*0x1F*/] = (byte) 56;
      numArray9[26] = (byte) 249;
      numArray9[20] = (byte) 108;
      numArray9[41] = (byte) 83;
      numArray9[6] = (byte) 11;
      numArray9[30] = (byte) 18;
      numArray9[21] = (byte) 65;
      numArray9[14] = (byte) 223;
      numArray9[40] = (byte) 66;
      numArray9[33] = (byte) 44;
      numArray9[35] = (byte) 54;
      numArray9[2] = (byte) 66;
      numArray9[37] = (byte) 119;
      numArray9[11] = (byte) 29;
      numArray9[9] = (byte) 210;
      numArray9[25] = (byte) 142;
      numArray9[8] = (byte) 219;
      numArray9[17] = (byte) 13;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[208 /*0xD0*/];
    byte[] numArray11 = new byte[55];
    numArray11[25] = (byte) 43;
    numArray11[52] = (byte) 187;
    numArray11[2] = (byte) 13;
    numArray11[3] = (byte) 133;
    numArray11[4] = (byte) 52;
    numArray11[8] = (byte) 253;
    numArray11[18] = (byte) 231;
    numArray11[37] = (byte) 143;
    numArray11[0] = (byte) 98;
    numArray11[40] = (byte) 27;
    numArray11[44] = (byte) 246;
    numArray11[11] = (byte) 245;
    numArray11[12] = (byte) 200;
    numArray11[13] = (byte) 211;
    numArray11[5] = (byte) 24;
    numArray11[15] = (byte) 131;
    numArray11[32 /*0x20*/] = (byte) 175;
    numArray11[17] = (byte) 156;
    numArray11[26] = (byte) 192 /*0xC0*/;
    numArray11[42] = (byte) 35;
    numArray11[20] = (byte) 190;
    numArray11[14] = (byte) 157;
    numArray11[49] = (byte) 123;
    numArray11[16 /*0x10*/] = (byte) 72;
    numArray11[21] = (byte) 244;
    numArray11[24] = (byte) 137;
    numArray11[53] = (byte) 223;
    numArray11[27] = (byte) 89;
    numArray11[28] = (byte) 232;
    numArray11[9] = (byte) 124;
    numArray11[54] = (byte) 153;
    numArray11[31 /*0x1F*/] = (byte) 81;
    numArray11[23] = (byte) 110;
    numArray11[33] = (byte) 108;
    numArray11[34] = (byte) 224 /*0xE0*/;
    numArray11[7] = (byte) 202;
    numArray11[36] = (byte) 40;
    numArray11[19] = (byte) 78;
    numArray11[38] = (byte) 123;
    numArray11[6] = (byte) 155;
    numArray11[30] = (byte) 27;
    numArray11[41] = (byte) 82;
    numArray11[35] = (byte) 187;
    numArray11[43] = (byte) 209;
    numArray11[22] = (byte) 8;
    numArray11[1] = (byte) 244;
    numArray11[39] = (byte) 34;
    numArray11[47] = (byte) 53;
    numArray11[48 /*0x30*/] = (byte) 226;
    numArray11[46] = (byte) 34;
    numArray11[50] = (byte) 113;
    numArray11[51] = (byte) 122;
    numArray11[29] = (byte) 3;
    numArray11[10] = (byte) 23;
    numArray11[45] = (byte) 17;
    byte[] numArray12 = new byte[55];
    numArray12[6] = (byte) 24;
    numArray12[1] = (byte) 28;
    numArray12[2] = (byte) 144 /*0x90*/;
    numArray12[13] = (byte) 131;
    numArray12[3] = (byte) 160 /*0xA0*/;
    numArray12[5] = (byte) 71;
    numArray12[49] = (byte) 122;
    numArray12[36] = (byte) 100;
    numArray12[47] = (byte) 156;
    numArray12[9] = (byte) 249;
    numArray12[11] = (byte) 156;
    numArray12[29] = (byte) 220;
    numArray12[4] = (byte) 161;
    numArray12[10] = (byte) 235;
    numArray12[14] = (byte) 216;
    numArray12[30] = (byte) 49;
    numArray12[16 /*0x10*/] = (byte) 189;
    numArray12[17] = (byte) 57;
    numArray12[46] = (byte) 48 /*0x30*/;
    numArray12[19] = (byte) 16 /*0x10*/;
    numArray12[8] = (byte) 43;
    numArray12[53] = (byte) 164;
    numArray12[31 /*0x1F*/] = (byte) 83;
    numArray12[23] = (byte) 41;
    numArray12[21] = (byte) 85;
    numArray12[25] = (byte) 67;
    numArray12[22] = (byte) 20;
    numArray12[39] = (byte) 119;
    numArray12[28] = (byte) 157;
    numArray12[12] = (byte) 123;
    numArray12[50] = (byte) 108;
    numArray12[24] = (byte) 246;
    numArray12[32 /*0x20*/] = (byte) 145;
    numArray12[48 /*0x30*/] = (byte) 108;
    numArray12[42] = (byte) 205;
    numArray12[35] = (byte) 133;
    numArray12[43] = (byte) 120;
    numArray12[37] = (byte) 48 /*0x30*/;
    numArray12[38] = (byte) 44;
    numArray12[26] = (byte) 129;
    numArray12[40] = (byte) 16 /*0x10*/;
    numArray12[41] = (byte) 10;
    numArray12[15] = (byte) 249;
    numArray12[27] = (byte) 119;
    numArray12[44] = (byte) 219;
    numArray12[45] = (byte) 208 /*0xD0*/;
    numArray12[52] = (byte) 158;
    numArray12[51] = (byte) 2;
    numArray12[54] = (byte) 136;
    numArray12[7] = (byte) 193;
    numArray12[33] = (byte) 142;
    numArray12[0] = (byte) 103;
    numArray12[34] = (byte) 123;
    numArray12[18] = (byte) 121;
    numArray12[20] = (byte) 179;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 222,
      (byte) 215,
      (byte) 189,
      (byte) 134,
      (byte) 83,
      (byte) 42,
      (byte) 144 /*0x90*/,
      (byte) 121,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 227,
      (byte) 168,
      (byte) 246,
      (byte) 109,
      (byte) 22,
      (byte) 60,
      (byte) 252,
      (byte) 75,
      (byte) 2,
      (byte) 233,
      (byte) 22,
      (byte) 71,
      (byte) 87,
      (byte) 5,
      (byte) 130,
      (byte) 135,
      (byte) 250,
      (byte) 71,
      (byte) 156,
      (byte) 163,
      (byte) 210,
      (byte) 116,
      (byte) 227,
      (byte) 44,
      (byte) 131,
      (byte) 55,
      (byte) 180,
      (byte) 119,
      (byte) 187,
      (byte) 207,
      (byte) 46,
      (byte) 54,
      (byte) 111,
      (byte) 1,
      (byte) 89,
      (byte) 28,
      (byte) 249,
      (byte) 227,
      (byte) 123,
      (byte) 110,
      byte.MaxValue,
      (byte) 5,
      (byte) 46,
      (byte) 9,
      (byte) 215
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 172,
      (byte) 254,
      (byte) 176 /*0xB0*/,
      (byte) 221,
      (byte) 104,
      (byte) 20,
      (byte) 117,
      (byte) 97,
      (byte) 254,
      (byte) 6,
      (byte) 119,
      (byte) 55,
      (byte) 217,
      (byte) 62,
      (byte) 124,
      (byte) 105,
      (byte) 235,
      (byte) 213,
      (byte) 182,
      (byte) 144 /*0x90*/,
      (byte) 168,
      (byte) 28,
      (byte) 177,
      (byte) 19,
      (byte) 60,
      (byte) 177,
      (byte) 119,
      (byte) 126,
      (byte) 157,
      (byte) 17,
      (byte) 15,
      (byte) 19,
      (byte) 60,
      (byte) 132,
      (byte) 83,
      (byte) 130,
      (byte) 148,
      (byte) 171,
      (byte) 244,
      (byte) 12,
      (byte) 193,
      (byte) 150,
      (byte) 157,
      (byte) 97,
      (byte) 254,
      (byte) 103,
      (byte) 22,
      (byte) 193,
      (byte) 31 /*0x1F*/,
      (byte) 20,
      (byte) 178,
      (byte) 137,
      (byte) 113,
      (byte) 204,
      (byte) 170
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 183,
      (byte) 3,
      (byte) 203,
      (byte) 48 /*0x30*/,
      (byte) 73,
      (byte) 221,
      (byte) 237,
      (byte) 128 /*0x80*/,
      (byte) 111,
      (byte) 197,
      (byte) 201,
      (byte) 233,
      (byte) 222,
      (byte) 135,
      (byte) 133,
      (byte) 143,
      (byte) 51,
      (byte) 187,
      (byte) 224 /*0xE0*/,
      (byte) 216,
      (byte) 86,
      (byte) 172,
      (byte) 102,
      (byte) 142,
      (byte) 23,
      (byte) 203,
      (byte) 46,
      (byte) 164,
      (byte) 204,
      (byte) 69,
      (byte) 217,
      (byte) 84,
      (byte) 236,
      (byte) 184,
      (byte) 253,
      (byte) 115,
      (byte) 240 /*0xF0*/,
      (byte) 158,
      (byte) 229,
      (byte) 89,
      (byte) 130,
      (byte) 128 /*0x80*/,
      (byte) 233,
      (byte) 86,
      (byte) 48 /*0x30*/,
      (byte) 249,
      (byte) 156,
      (byte) 230,
      (byte) 121,
      (byte) 117,
      (byte) 251,
      (byte) 78,
      (byte) 247,
      (byte) 190,
      (byte) 40
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 186,
      (byte) 50,
      (byte) 143,
      (byte) 200,
      (byte) 211,
      (byte) 197,
      (byte) 8,
      (byte) 8,
      (byte) 84,
      (byte) 11,
      (byte) 50,
      (byte) 254,
      (byte) 156,
      (byte) 180,
      (byte) 45,
      (byte) 55,
      (byte) 22,
      (byte) 57,
      (byte) 199,
      (byte) 184,
      (byte) 53,
      (byte) 186,
      (byte) 137,
      (byte) 218,
      (byte) 77,
      (byte) 95,
      (byte) 233,
      (byte) 183,
      (byte) 35,
      (byte) 127 /*0x7F*/,
      (byte) 22,
      (byte) 11,
      (byte) 235,
      (byte) 171,
      (byte) 140,
      (byte) 131,
      (byte) 73,
      (byte) 165,
      (byte) 7,
      (byte) 200,
      (byte) 215,
      (byte) 131,
      (byte) 36,
      (byte) 32 /*0x20*/,
      (byte) 233,
      (byte) 209,
      (byte) 43,
      (byte) 21,
      (byte) 148,
      (byte) 254,
      (byte) 81,
      (byte) 18,
      (byte) 98,
      (byte) 17,
      (byte) 236
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[43]
    {
      (byte) 61,
      (byte) 24,
      (byte) 125,
      (byte) 174,
      (byte) 73,
      (byte) 180,
      (byte) 41,
      (byte) 236,
      (byte) 116,
      (byte) 252,
      (byte) 38,
      (byte) 138,
      (byte) 17,
      (byte) 159,
      (byte) 148,
      (byte) 213,
      (byte) 134,
      (byte) 85,
      (byte) 214,
      (byte) 194,
      (byte) 15,
      (byte) 93,
      (byte) 29,
      (byte) 47,
      (byte) 71,
      (byte) 42,
      (byte) 86,
      (byte) 74,
      (byte) 138,
      (byte) 92,
      (byte) 45,
      (byte) 143,
      (byte) 242,
      (byte) 214,
      (byte) 178,
      (byte) 133,
      (byte) 104,
      (byte) 170,
      (byte) 57,
      (byte) 204,
      (byte) 192 /*0xC0*/,
      (byte) 106,
      (byte) 187
    };
    byte[] numArray18 = new byte[43]
    {
      (byte) 179,
      (byte) 95,
      (byte) 197,
      (byte) 97,
      (byte) 110,
      (byte) 115,
      (byte) 49,
      (byte) 199,
      (byte) 66,
      (byte) 78,
      (byte) 187,
      (byte) 191,
      (byte) 94,
      (byte) 129,
      (byte) 79,
      (byte) 36,
      (byte) 122,
      (byte) 167,
      (byte) 3,
      (byte) 172,
      (byte) 96 /*0x60*/,
      (byte) 204,
      (byte) 138,
      (byte) 50,
      (byte) 87,
      (byte) 243,
      (byte) 205,
      (byte) 125,
      (byte) 172,
      (byte) 148,
      (byte) 143,
      (byte) 126,
      (byte) 249,
      (byte) 193,
      (byte) 74,
      (byte) 201,
      (byte) 150,
      (byte) 232,
      (byte) 245,
      (byte) 1,
      (byte) 9,
      (byte) 244,
      (byte) 1
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 43);
    for (int index = 0; index < 43; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13696()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[142];
      byte[] numArray2 = new byte[55]
      {
        (byte) 214,
        (byte) 102,
        (byte) 121,
        (byte) 113,
        (byte) 105,
        (byte) 82,
        (byte) 72,
        (byte) 141,
        (byte) 223,
        (byte) 251,
        (byte) 77,
        (byte) 103,
        (byte) 30,
        (byte) 93,
        (byte) 223,
        (byte) 97,
        (byte) 23,
        (byte) 207,
        (byte) 150,
        (byte) 151,
        (byte) 59,
        (byte) 236,
        (byte) 14,
        (byte) 109,
        (byte) 220,
        (byte) 28,
        (byte) 10,
        (byte) 2,
        (byte) 165,
        (byte) 161,
        (byte) 33,
        (byte) 186,
        (byte) 234,
        (byte) 194,
        (byte) 128 /*0x80*/,
        (byte) 227,
        (byte) 17,
        (byte) 235,
        (byte) 196,
        (byte) 60,
        (byte) 169,
        (byte) 73,
        (byte) 210,
        (byte) 138,
        (byte) 26,
        (byte) 67,
        (byte) 26,
        (byte) 110,
        (byte) 225,
        (byte) 9,
        (byte) 164,
        (byte) 45,
        (byte) 222,
        (byte) 196,
        (byte) 44
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 123,
        (byte) 67,
        (byte) 142,
        (byte) 205,
        (byte) 11,
        (byte) 0,
        (byte) 243,
        (byte) 18,
        (byte) 94,
        (byte) 164,
        (byte) 247,
        (byte) 182,
        (byte) 95,
        (byte) 117,
        (byte) 223,
        (byte) 190,
        (byte) 133,
        (byte) 12,
        (byte) 29,
        (byte) 158,
        (byte) 202,
        (byte) 164,
        (byte) 128 /*0x80*/,
        (byte) 229,
        (byte) 235,
        (byte) 124,
        (byte) 203,
        (byte) 189,
        (byte) 14,
        (byte) 15,
        (byte) 204,
        (byte) 119,
        (byte) 235,
        (byte) 57,
        (byte) 17,
        (byte) 37,
        (byte) 252,
        (byte) 98,
        (byte) 239,
        (byte) 192 /*0xC0*/,
        (byte) 94,
        (byte) 199,
        (byte) 222,
        (byte) 165,
        (byte) 10,
        (byte) 42,
        (byte) 23,
        (byte) 99,
        (byte) 165,
        (byte) 35,
        (byte) 192 /*0xC0*/,
        (byte) 34,
        (byte) 29,
        (byte) 155,
        (byte) 44
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[11] = (byte) 221;
      numArray4[1] = (byte) 149;
      numArray4[2] = (byte) 76;
      numArray4[50] = (byte) 132;
      numArray4[4] = (byte) 70;
      numArray4[34] = (byte) 5;
      numArray4[13] = (byte) 79;
      numArray4[27] = (byte) 253;
      numArray4[30] = (byte) 37;
      numArray4[9] = (byte) 124;
      numArray4[35] = (byte) 86;
      numArray4[23] = (byte) 1;
      numArray4[5] = (byte) 231;
      numArray4[42] = (byte) 135;
      numArray4[3] = (byte) 72;
      numArray4[15] = (byte) 10;
      numArray4[37] = (byte) 244;
      numArray4[17] = (byte) 238;
      numArray4[36] = (byte) 119;
      numArray4[52] = (byte) 58;
      numArray4[20] = (byte) 131;
      numArray4[51] = (byte) 214;
      numArray4[22] = (byte) 153;
      numArray4[18] = (byte) 241;
      numArray4[24] = (byte) 155;
      numArray4[25] = (byte) 32 /*0x20*/;
      numArray4[26] = (byte) 156;
      numArray4[49] = (byte) 34;
      numArray4[16 /*0x10*/] = (byte) 90;
      numArray4[53] = (byte) 207;
      numArray4[8] = (byte) 147;
      numArray4[31 /*0x1F*/] = (byte) 150;
      numArray4[32 /*0x20*/] = (byte) 150;
      numArray4[10] = (byte) 91;
      numArray4[29] = (byte) 219;
      numArray4[44] = (byte) 191;
      numArray4[38] = (byte) 99;
      numArray4[12] = (byte) 251;
      numArray4[33] = (byte) 139;
      numArray4[39] = (byte) 97;
      numArray4[14] = (byte) 43;
      numArray4[41] = (byte) 177;
      numArray4[19] = (byte) 25;
      numArray4[6] = (byte) 61;
      numArray4[21] = (byte) 82;
      numArray4[45] = (byte) 145;
      numArray4[46] = (byte) 230;
      numArray4[47] = (byte) 185;
      numArray4[48 /*0x30*/] = (byte) 120;
      numArray4[0] = (byte) 11;
      numArray4[43] = (byte) 198;
      numArray4[28] = (byte) 244;
      numArray4[7] = (byte) 217;
      numArray4[54] = (byte) 212;
      numArray4[40] = (byte) 149;
      byte[] numArray5 = new byte[55];
      numArray5[21] = (byte) 108;
      numArray5[1] = (byte) 61;
      numArray5[35] = (byte) 201;
      numArray5[9] = (byte) 165;
      numArray5[4] = (byte) 147;
      numArray5[22] = (byte) 41;
      numArray5[41] = (byte) 222;
      numArray5[7] = (byte) 86;
      numArray5[14] = (byte) 169;
      numArray5[45] = (byte) 253;
      numArray5[5] = (byte) 15;
      numArray5[11] = (byte) 19;
      numArray5[29] = (byte) 113;
      numArray5[12] = (byte) 228;
      numArray5[37] = (byte) 15;
      numArray5[42] = (byte) 2;
      numArray5[16 /*0x10*/] = (byte) 210;
      numArray5[10] = (byte) 98;
      numArray5[15] = (byte) 166;
      numArray5[19] = (byte) 138;
      numArray5[20] = (byte) 133;
      numArray5[2] = (byte) 216;
      numArray5[6] = (byte) 239;
      numArray5[18] = (byte) 92;
      numArray5[24] = (byte) 223;
      numArray5[28] = (byte) 165;
      numArray5[26] = (byte) 117;
      numArray5[46] = (byte) 71;
      numArray5[43] = (byte) 213;
      numArray5[31 /*0x1F*/] = (byte) 183;
      numArray5[25] = (byte) 29;
      numArray5[47] = (byte) 33;
      numArray5[32 /*0x20*/] = (byte) 47;
      numArray5[33] = (byte) 226;
      numArray5[49] = (byte) 156;
      numArray5[3] = (byte) 173;
      numArray5[36] = (byte) 10;
      numArray5[44] = (byte) 249;
      numArray5[38] = (byte) 28;
      numArray5[39] = (byte) 116;
      numArray5[30] = (byte) 126;
      numArray5[23] = (byte) 152;
      numArray5[17] = (byte) 21;
      numArray5[27] = (byte) 245;
      numArray5[8] = (byte) 116;
      numArray5[40] = (byte) 156;
      numArray5[48 /*0x30*/] = (byte) 193;
      numArray5[34] = (byte) 63 /*0x3F*/;
      numArray5[53] = (byte) 179;
      numArray5[13] = (byte) 140;
      numArray5[50] = (byte) 45;
      numArray5[51] = (byte) 180;
      numArray5[52] = (byte) 41;
      numArray5[0] = (byte) 134;
      numArray5[54] = (byte) 8;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[32 /*0x20*/]
      {
        (byte) 192 /*0xC0*/,
        (byte) 226,
        (byte) 38,
        (byte) 243,
        (byte) 217,
        (byte) 210,
        (byte) 38,
        (byte) 6,
        (byte) 183,
        (byte) 2,
        (byte) 3,
        (byte) 161,
        (byte) 72,
        (byte) 66,
        (byte) 122,
        (byte) 249,
        (byte) 202,
        (byte) 150,
        (byte) 78,
        (byte) 137,
        (byte) 254,
        (byte) 182,
        (byte) 245,
        (byte) 6,
        (byte) 3,
        (byte) 184,
        (byte) 23,
        (byte) 57,
        (byte) 2,
        (byte) 129,
        (byte) 98,
        (byte) 185
      };
      byte[] numArray7 = new byte[32 /*0x20*/]
      {
        (byte) 8,
        (byte) 169,
        (byte) 199,
        (byte) 106,
        (byte) 247,
        (byte) 133,
        (byte) 43,
        (byte) 246,
        (byte) 198,
        (byte) 103,
        (byte) 244,
        (byte) 12,
        (byte) 56,
        (byte) 227,
        (byte) 199,
        (byte) 154,
        (byte) 220,
        (byte) 126,
        (byte) 200,
        (byte) 11,
        (byte) 4,
        (byte) 194,
        (byte) 41,
        (byte) 12,
        (byte) 84,
        (byte) 98,
        (byte) 74,
        (byte) 13,
        (byte) 220,
        (byte) 7,
        (byte) 97,
        (byte) 202
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[142];
    byte[] numArray9 = new byte[55];
    numArray9[8] = (byte) 200;
    numArray9[34] = (byte) 183;
    numArray9[25] = (byte) 187;
    numArray9[3] = (byte) 191;
    numArray9[49] = (byte) 26;
    numArray9[12] = (byte) 231;
    numArray9[6] = (byte) 95;
    numArray9[7] = (byte) 218;
    numArray9[24] = (byte) 32 /*0x20*/;
    numArray9[9] = (byte) 185;
    numArray9[10] = (byte) 207;
    numArray9[11] = (byte) 224 /*0xE0*/;
    numArray9[45] = (byte) 215;
    numArray9[5] = (byte) 73;
    numArray9[44] = (byte) 52;
    numArray9[15] = (byte) 102;
    numArray9[17] = (byte) 172;
    numArray9[48 /*0x30*/] = (byte) 252;
    numArray9[18] = (byte) 108;
    numArray9[37] = (byte) 214;
    numArray9[30] = (byte) 74;
    numArray9[21] = (byte) 237;
    numArray9[2] = (byte) 187;
    numArray9[23] = (byte) 239;
    numArray9[47] = (byte) 180;
    numArray9[38] = (byte) 106;
    numArray9[26] = (byte) 236;
    numArray9[35] = (byte) 204;
    numArray9[14] = (byte) 6;
    numArray9[16 /*0x10*/] = (byte) 131;
    numArray9[1] = (byte) 44;
    numArray9[31 /*0x1F*/] = (byte) 58;
    numArray9[32 /*0x20*/] = (byte) 218;
    numArray9[33] = (byte) 223;
    numArray9[4] = (byte) 228;
    numArray9[19] = (byte) 198;
    numArray9[36] = (byte) 15;
    numArray9[28] = (byte) 54;
    numArray9[0] = (byte) 194;
    numArray9[39] = (byte) 63 /*0x3F*/;
    numArray9[20] = (byte) 9;
    numArray9[29] = (byte) 160 /*0xA0*/;
    numArray9[42] = (byte) 44;
    numArray9[43] = (byte) 77;
    numArray9[54] = (byte) 89;
    numArray9[13] = (byte) 132;
    numArray9[46] = (byte) 47;
    numArray9[27] = (byte) 3;
    numArray9[40] = (byte) 158;
    numArray9[50] = (byte) 222;
    numArray9[41] = (byte) 220;
    numArray9[51] = (byte) 12;
    numArray9[52] = (byte) 250;
    numArray9[53] = (byte) 167;
    numArray9[22] = (byte) 31 /*0x1F*/;
    byte[] numArray10 = new byte[55]
    {
      (byte) 187,
      (byte) 107,
      (byte) 1,
      (byte) 68,
      (byte) 218,
      (byte) 242,
      (byte) 253,
      (byte) 1,
      (byte) 117,
      (byte) 81,
      (byte) 34,
      (byte) 133,
      (byte) 130,
      (byte) 7,
      (byte) 242,
      (byte) 52,
      byte.MaxValue,
      (byte) 69,
      (byte) 65,
      (byte) 128 /*0x80*/,
      (byte) 135,
      (byte) 193,
      (byte) 154,
      (byte) 143,
      (byte) 36,
      (byte) 54,
      (byte) 75,
      (byte) 221,
      (byte) 81,
      (byte) 223,
      (byte) 170,
      (byte) 70,
      (byte) 91,
      (byte) 228,
      (byte) 216,
      (byte) 185,
      (byte) 175,
      (byte) 237,
      (byte) 243,
      (byte) 106,
      (byte) 122,
      (byte) 64 /*0x40*/,
      (byte) 51,
      (byte) 190,
      (byte) 54,
      (byte) 18,
      (byte) 48 /*0x30*/,
      (byte) 55,
      (byte) 4,
      (byte) 252,
      (byte) 93,
      (byte) 139,
      (byte) 143,
      (byte) 135,
      (byte) 33
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[32 /*0x20*/] = (byte) 204;
    numArray11[40] = (byte) 160 /*0xA0*/;
    numArray11[42] = (byte) 74;
    numArray11[2] = (byte) 157;
    numArray11[9] = (byte) 210;
    numArray11[54] = (byte) 69;
    numArray11[6] = (byte) 101;
    numArray11[12] = (byte) 121;
    numArray11[52] = (byte) 53;
    numArray11[21] = (byte) 27;
    numArray11[30] = (byte) 179;
    numArray11[11] = (byte) 235;
    numArray11[8] = (byte) 85;
    numArray11[13] = (byte) 19;
    numArray11[14] = (byte) 117;
    numArray11[15] = (byte) 0;
    numArray11[47] = (byte) 177;
    numArray11[17] = (byte) 229;
    numArray11[34] = (byte) 50;
    numArray11[24] = (byte) 80 /*0x50*/;
    numArray11[10] = (byte) 130;
    numArray11[23] = (byte) 100;
    numArray11[22] = (byte) 118;
    numArray11[16 /*0x10*/] = (byte) 75;
    numArray11[51] = (byte) 154;
    numArray11[37] = (byte) 188;
    numArray11[31 /*0x1F*/] = (byte) 90;
    numArray11[27] = (byte) 145;
    numArray11[28] = (byte) 116;
    numArray11[29] = (byte) 113;
    numArray11[0] = (byte) 16 /*0x10*/;
    numArray11[46] = (byte) 73;
    numArray11[4] = (byte) 85;
    numArray11[33] = (byte) 133;
    numArray11[44] = (byte) 164;
    numArray11[35] = (byte) 225;
    numArray11[36] = (byte) 164;
    numArray11[1] = (byte) 85;
    numArray11[38] = (byte) 237;
    numArray11[39] = (byte) 174;
    numArray11[19] = (byte) 187;
    numArray11[41] = (byte) 174;
    numArray11[20] = (byte) 244;
    numArray11[43] = byte.MaxValue;
    numArray11[7] = (byte) 48 /*0x30*/;
    numArray11[45] = (byte) 251;
    numArray11[3] = (byte) 37;
    numArray11[26] = (byte) 86;
    numArray11[48 /*0x30*/] = (byte) 188;
    numArray11[49] = (byte) 201;
    numArray11[50] = (byte) 130;
    numArray11[18] = (byte) 245;
    numArray11[5] = (byte) 137;
    numArray11[53] = (byte) 90;
    numArray11[25] = (byte) 68;
    byte[] numArray12 = new byte[55]
    {
      (byte) 223,
      (byte) 51,
      (byte) 50,
      (byte) 210,
      (byte) 225,
      (byte) 127 /*0x7F*/,
      (byte) 21,
      (byte) 104,
      (byte) 225,
      (byte) 174,
      (byte) 157,
      (byte) 42,
      (byte) 111,
      (byte) 117,
      (byte) 47,
      (byte) 10,
      (byte) 137,
      (byte) 30,
      (byte) 89,
      (byte) 83,
      (byte) 54,
      (byte) 254,
      (byte) 176 /*0xB0*/,
      (byte) 98,
      (byte) 28,
      (byte) 184,
      (byte) 85,
      (byte) 141,
      (byte) 219,
      (byte) 254,
      (byte) 173,
      (byte) 42,
      (byte) 203,
      (byte) 151,
      (byte) 112 /*0x70*/,
      (byte) 169,
      (byte) 236,
      (byte) 52,
      (byte) 174,
      (byte) 122,
      (byte) 123,
      (byte) 18,
      (byte) 81,
      (byte) 19,
      (byte) 236,
      (byte) 157,
      (byte) 38,
      (byte) 131,
      (byte) 90,
      (byte) 8,
      (byte) 11,
      (byte) 159,
      (byte) 174,
      (byte) 118,
      (byte) 40
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[32 /*0x20*/];
    numArray13[0] = (byte) 121;
    numArray13[1] = (byte) 128 /*0x80*/;
    numArray13[2] = (byte) 21;
    numArray13[24] = (byte) 213;
    numArray13[29] = (byte) 53;
    numArray13[27] = (byte) 251;
    numArray13[6] = (byte) 244;
    numArray13[7] = (byte) 7;
    numArray13[3] = (byte) 140;
    numArray13[21] = (byte) 67;
    numArray13[15] = (byte) 27;
    numArray13[11] = (byte) 163;
    numArray13[10] = (byte) 179;
    numArray13[18] = (byte) 84;
    numArray13[14] = (byte) 166;
    numArray13[31 /*0x1F*/] = (byte) 64 /*0x40*/;
    numArray13[16 /*0x10*/] = (byte) 202;
    numArray13[17] = (byte) 79;
    numArray13[5] = (byte) 16 /*0x10*/;
    numArray13[9] = (byte) 173;
    numArray13[20] = (byte) 151;
    numArray13[4] = byte.MaxValue;
    numArray13[22] = (byte) 120;
    numArray13[23] = (byte) 165;
    numArray13[26] = (byte) 62;
    numArray13[25] = (byte) 171;
    numArray13[13] = (byte) 241;
    numArray13[19] = (byte) 107;
    numArray13[28] = (byte) 117;
    numArray13[8] = (byte) 3;
    numArray13[30] = (byte) 51;
    numArray13[12] = (byte) 144 /*0x90*/;
    byte[] numArray14 = new byte[32 /*0x20*/];
    numArray14[27] = (byte) 90;
    numArray14[1] = (byte) 117;
    numArray14[17] = (byte) 158;
    numArray14[3] = (byte) 83;
    numArray14[4] = (byte) 241;
    numArray14[7] = (byte) 122;
    numArray14[10] = (byte) 151;
    numArray14[9] = byte.MaxValue;
    numArray14[8] = (byte) 123;
    numArray14[18] = (byte) 123;
    numArray14[5] = (byte) 38;
    numArray14[11] = (byte) 212;
    numArray14[15] = (byte) 80 /*0x50*/;
    numArray14[19] = (byte) 152;
    numArray14[14] = (byte) 186;
    numArray14[2] = (byte) 73;
    numArray14[16 /*0x10*/] = (byte) 83;
    numArray14[6] = (byte) 132;
    numArray14[13] = (byte) 116;
    numArray14[22] = (byte) 114;
    numArray14[0] = (byte) 113;
    numArray14[24] = (byte) 81;
    numArray14[21] = (byte) 171;
    numArray14[23] = (byte) 203;
    numArray14[29] = (byte) 120;
    numArray14[25] = (byte) 227;
    numArray14[26] = (byte) 143;
    numArray14[12] = (byte) 131;
    numArray14[28] = (byte) 14;
    numArray14[20] = (byte) 245;
    numArray14[30] = (byte) 24;
    numArray14[31 /*0x1F*/] = (byte) 185;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13697()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[152];
      byte[] numArray2 = new byte[55]
      {
        (byte) 162,
        (byte) 164,
        (byte) 122,
        (byte) 14,
        (byte) 50,
        (byte) 74,
        (byte) 109,
        (byte) 56,
        (byte) 191,
        (byte) 17,
        (byte) 107,
        (byte) 189,
        (byte) 107,
        (byte) 14,
        (byte) 66,
        (byte) 13,
        (byte) 74,
        (byte) 162,
        (byte) 189,
        (byte) 54,
        (byte) 223,
        (byte) 43,
        (byte) 41,
        (byte) 69,
        (byte) 222,
        (byte) 252,
        (byte) 161,
        (byte) 10,
        (byte) 99,
        (byte) 129,
        (byte) 119,
        (byte) 32 /*0x20*/,
        (byte) 149,
        (byte) 214,
        (byte) 6,
        (byte) 110,
        (byte) 73,
        (byte) 159,
        (byte) 187,
        (byte) 198,
        (byte) 214,
        (byte) 91,
        (byte) 159,
        (byte) 186,
        (byte) 79,
        (byte) 162,
        (byte) 235,
        (byte) 42,
        (byte) 54,
        (byte) 1,
        (byte) 45,
        (byte) 164,
        (byte) 236,
        (byte) 125,
        (byte) 78
      };
      byte[] numArray3 = new byte[55];
      numArray3[13] = (byte) 111;
      numArray3[1] = (byte) 211;
      numArray3[47] = (byte) 188;
      numArray3[39] = (byte) 169;
      numArray3[17] = (byte) 166;
      numArray3[48 /*0x30*/] = (byte) 193;
      numArray3[14] = (byte) 67;
      numArray3[7] = (byte) 20;
      numArray3[18] = (byte) 0;
      numArray3[16 /*0x10*/] = (byte) 6;
      numArray3[10] = (byte) 18;
      numArray3[21] = (byte) 187;
      numArray3[12] = (byte) 94;
      numArray3[50] = (byte) 63 /*0x3F*/;
      numArray3[22] = (byte) 70;
      numArray3[19] = (byte) 33;
      numArray3[6] = (byte) 157;
      numArray3[43] = (byte) 64 /*0x40*/;
      numArray3[15] = (byte) 109;
      numArray3[8] = (byte) 228;
      numArray3[20] = (byte) 150;
      numArray3[24] = (byte) 75;
      numArray3[41] = (byte) 51;
      numArray3[2] = (byte) 17;
      numArray3[49] = (byte) 139;
      numArray3[54] = (byte) 254;
      numArray3[26] = (byte) 150;
      numArray3[27] = (byte) 61;
      numArray3[28] = (byte) 74;
      numArray3[31 /*0x1F*/] = (byte) 91;
      numArray3[23] = (byte) 72;
      numArray3[11] = (byte) 154;
      numArray3[32 /*0x20*/] = (byte) 111;
      numArray3[33] = (byte) 26;
      numArray3[34] = (byte) 68;
      numArray3[35] = (byte) 31 /*0x1F*/;
      numArray3[3] = (byte) 54;
      numArray3[36] = (byte) 216;
      numArray3[38] = (byte) 243;
      numArray3[25] = (byte) 88;
      numArray3[40] = (byte) 79;
      numArray3[9] = (byte) 39;
      numArray3[42] = (byte) 53;
      numArray3[37] = (byte) 84;
      numArray3[44] = (byte) 233;
      numArray3[45] = (byte) 101;
      numArray3[46] = (byte) 38;
      numArray3[4] = (byte) 64 /*0x40*/;
      numArray3[30] = (byte) 159;
      numArray3[5] = (byte) 206;
      numArray3[29] = (byte) 234;
      numArray3[51] = (byte) 205;
      numArray3[52] = (byte) 124;
      numArray3[53] = (byte) 192 /*0xC0*/;
      numArray3[0] = (byte) 8;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[12] = (byte) 224 /*0xE0*/;
      numArray4[1] = (byte) 244;
      numArray4[38] = (byte) 229;
      numArray4[3] = (byte) 49;
      numArray4[40] = (byte) 118;
      numArray4[5] = (byte) 115;
      numArray4[0] = (byte) 63 /*0x3F*/;
      numArray4[46] = (byte) 129;
      numArray4[14] = (byte) 59;
      numArray4[30] = (byte) 250;
      numArray4[44] = (byte) 72;
      numArray4[11] = (byte) 120;
      numArray4[13] = (byte) 175;
      numArray4[21] = (byte) 241;
      numArray4[6] = (byte) 234;
      numArray4[10] = (byte) 13;
      numArray4[52] = (byte) 206;
      numArray4[17] = (byte) 237;
      numArray4[18] = (byte) 252;
      numArray4[19] = (byte) 43;
      numArray4[20] = (byte) 78;
      numArray4[16 /*0x10*/] = (byte) 217;
      numArray4[22] = (byte) 193;
      numArray4[23] = (byte) 186;
      numArray4[39] = (byte) 18;
      numArray4[54] = (byte) 189;
      numArray4[26] = (byte) 36;
      numArray4[9] = (byte) 17;
      numArray4[28] = (byte) 21;
      numArray4[29] = (byte) 63 /*0x3F*/;
      numArray4[24] = (byte) 85;
      numArray4[36] = (byte) 96 /*0x60*/;
      numArray4[37] = (byte) 236;
      numArray4[42] = (byte) 237;
      numArray4[34] = (byte) 212;
      numArray4[2] = (byte) 36;
      numArray4[33] = (byte) 231;
      numArray4[27] = (byte) 209;
      numArray4[32 /*0x20*/] = (byte) 165;
      numArray4[7] = (byte) 199;
      numArray4[15] = (byte) 98;
      numArray4[41] = (byte) 88;
      numArray4[47] = (byte) 141;
      numArray4[43] = (byte) 104;
      numArray4[35] = (byte) 122;
      numArray4[45] = (byte) 169;
      numArray4[50] = (byte) 27;
      numArray4[4] = (byte) 142;
      numArray4[48 /*0x30*/] = (byte) 70;
      numArray4[49] = (byte) 91;
      numArray4[8] = (byte) 22;
      numArray4[51] = (byte) 47;
      numArray4[31 /*0x1F*/] = (byte) 188;
      numArray4[25] = (byte) 113;
      numArray4[53] = (byte) 191;
      byte[] numArray5 = new byte[55]
      {
        (byte) 174,
        (byte) 172,
        byte.MaxValue,
        (byte) 32 /*0x20*/,
        (byte) 39,
        (byte) 31 /*0x1F*/,
        (byte) 179,
        (byte) 100,
        (byte) 38,
        (byte) 29,
        (byte) 2,
        (byte) 173,
        (byte) 17,
        (byte) 120,
        (byte) 249,
        (byte) 90,
        (byte) 220,
        (byte) 166,
        (byte) 53,
        (byte) 159,
        (byte) 70,
        (byte) 252,
        (byte) 110,
        (byte) 248,
        (byte) 128 /*0x80*/,
        (byte) 247,
        (byte) 142,
        (byte) 144 /*0x90*/,
        (byte) 150,
        (byte) 196,
        (byte) 188,
        (byte) 97,
        (byte) 131,
        (byte) 151,
        (byte) 150,
        (byte) 178,
        (byte) 223,
        (byte) 207,
        (byte) 160 /*0xA0*/,
        (byte) 128 /*0x80*/,
        (byte) 248,
        (byte) 174,
        (byte) 187,
        (byte) 153,
        (byte) 254,
        (byte) 249,
        (byte) 123,
        (byte) 237,
        (byte) 228,
        (byte) 214,
        (byte) 58,
        (byte) 102,
        (byte) 32 /*0x20*/,
        (byte) 248,
        (byte) 56
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[42];
      numArray6[24] = (byte) 228;
      numArray6[8] = (byte) 100;
      numArray6[2] = (byte) 44;
      numArray6[17] = (byte) 85;
      numArray6[27] = (byte) 245;
      numArray6[1] = (byte) 47;
      numArray6[31 /*0x1F*/] = (byte) 99;
      numArray6[35] = (byte) 225;
      numArray6[25] = (byte) 4;
      numArray6[9] = (byte) 236;
      numArray6[30] = (byte) 219;
      numArray6[11] = (byte) 84;
      numArray6[10] = (byte) 206;
      numArray6[19] = (byte) 238;
      numArray6[12] = (byte) 121;
      numArray6[4] = (byte) 141;
      numArray6[15] = (byte) 63 /*0x3F*/;
      numArray6[28] = (byte) 162;
      numArray6[18] = (byte) 80 /*0x50*/;
      numArray6[3] = (byte) 185;
      numArray6[20] = (byte) 6;
      numArray6[16 /*0x10*/] = (byte) 39;
      numArray6[22] = (byte) 213;
      numArray6[23] = (byte) 103;
      numArray6[21] = (byte) 123;
      numArray6[32 /*0x20*/] = (byte) 62;
      numArray6[26] = (byte) 96 /*0x60*/;
      numArray6[6] = (byte) 178;
      numArray6[13] = (byte) 11;
      numArray6[29] = (byte) 9;
      numArray6[39] = (byte) 173;
      numArray6[14] = (byte) 219;
      numArray6[7] = (byte) 76;
      numArray6[33] = (byte) 107;
      numArray6[0] = (byte) 248;
      numArray6[34] = (byte) 254;
      numArray6[36] = (byte) 156;
      numArray6[37] = (byte) 10;
      numArray6[38] = (byte) 208 /*0xD0*/;
      numArray6[40] = (byte) 99;
      numArray6[41] = (byte) 5;
      numArray6[5] = (byte) 248;
      byte[] numArray7 = new byte[42]
      {
        (byte) 118,
        (byte) 134,
        (byte) 253,
        (byte) 29,
        (byte) 203,
        (byte) 162,
        (byte) 23,
        (byte) 171,
        (byte) 186,
        (byte) 150,
        (byte) 110,
        (byte) 55,
        (byte) 165,
        (byte) 145,
        (byte) 182,
        (byte) 138,
        (byte) 18,
        (byte) 22,
        (byte) 147,
        (byte) 207,
        (byte) 221,
        (byte) 84,
        (byte) 114,
        (byte) 229,
        (byte) 146,
        (byte) 75,
        (byte) 103,
        (byte) 77,
        (byte) 250,
        (byte) 108,
        (byte) 151,
        (byte) 145,
        (byte) 0,
        (byte) 183,
        (byte) 106,
        (byte) 95,
        (byte) 187,
        (byte) 106,
        (byte) 156,
        (byte) 132,
        (byte) 188,
        (byte) 238
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[152];
    byte[] numArray9 = new byte[55]
    {
      (byte) 227,
      (byte) 150,
      (byte) 129,
      (byte) 135,
      (byte) 215,
      (byte) 53,
      (byte) 70,
      (byte) 20,
      (byte) 165,
      (byte) 91,
      (byte) 56,
      (byte) 218,
      (byte) 80 /*0x50*/,
      (byte) 72,
      (byte) 254,
      (byte) 65,
      (byte) 88,
      (byte) 68,
      (byte) 160 /*0xA0*/,
      (byte) 47,
      (byte) 211,
      (byte) 206,
      (byte) 212,
      (byte) 148,
      (byte) 110,
      (byte) 24,
      (byte) 78,
      (byte) 145,
      (byte) 134,
      (byte) 26,
      (byte) 238,
      (byte) 14,
      (byte) 233,
      (byte) 125,
      (byte) 50,
      (byte) 120,
      (byte) 139,
      (byte) 114,
      (byte) 204,
      (byte) 231,
      (byte) 62,
      (byte) 142,
      (byte) 229,
      (byte) 164,
      (byte) 175,
      (byte) 17,
      (byte) 92,
      (byte) 61,
      (byte) 249,
      (byte) 208 /*0xD0*/,
      (byte) 74,
      (byte) 118,
      (byte) 172,
      (byte) 247,
      (byte) 25
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 132,
      (byte) 123,
      (byte) 57,
      (byte) 231,
      (byte) 100,
      (byte) 7,
      (byte) 53,
      (byte) 135,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 66,
      (byte) 126,
      (byte) 112 /*0x70*/,
      (byte) 224 /*0xE0*/,
      (byte) 137,
      (byte) 125,
      (byte) 76,
      (byte) 19,
      (byte) 161,
      (byte) 54,
      (byte) 131,
      (byte) 72,
      (byte) 47,
      (byte) 231,
      (byte) 110,
      (byte) 225,
      (byte) 31 /*0x1F*/,
      (byte) 21,
      (byte) 69,
      (byte) 86,
      (byte) 70,
      (byte) 79,
      (byte) 1,
      (byte) 228,
      (byte) 11,
      (byte) 207,
      (byte) 103,
      (byte) 20,
      (byte) 119,
      (byte) 22,
      (byte) 71,
      (byte) 254,
      (byte) 154,
      (byte) 108,
      (byte) 12,
      (byte) 79,
      (byte) 180,
      (byte) 81,
      (byte) 236,
      (byte) 188,
      (byte) 188,
      (byte) 37,
      (byte) 21,
      (byte) 164,
      (byte) 211
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 236,
      (byte) 25,
      (byte) 190,
      (byte) 230,
      (byte) 181,
      (byte) 141,
      (byte) 139,
      (byte) 109,
      (byte) 41,
      (byte) 240 /*0xF0*/,
      (byte) 210,
      (byte) 21,
      (byte) 133,
      (byte) 181,
      (byte) 229,
      (byte) 120,
      (byte) 184,
      (byte) 240 /*0xF0*/,
      (byte) 68,
      (byte) 233,
      (byte) 47,
      (byte) 229,
      (byte) 92,
      (byte) 188,
      (byte) 16 /*0x10*/,
      (byte) 151,
      (byte) 181,
      (byte) 208 /*0xD0*/,
      (byte) 119,
      (byte) 98,
      (byte) 241,
      (byte) 99,
      (byte) 29,
      (byte) 183,
      (byte) 159,
      (byte) 88,
      (byte) 13,
      (byte) 1,
      (byte) 137,
      (byte) 1,
      (byte) 90,
      (byte) 55,
      (byte) 134,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 40,
      (byte) 116,
      (byte) 27,
      (byte) 100,
      (byte) 254,
      (byte) 243,
      (byte) 7,
      (byte) 213,
      (byte) 43,
      (byte) 145
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 187,
      (byte) 37,
      (byte) 210,
      (byte) 211,
      (byte) 59,
      (byte) 51,
      (byte) 209,
      (byte) 29,
      (byte) 232,
      (byte) 147,
      (byte) 40,
      (byte) 41,
      (byte) 57,
      (byte) 153,
      (byte) 146,
      (byte) 225,
      (byte) 8,
      (byte) 106,
      (byte) 171,
      (byte) 155,
      (byte) 82,
      (byte) 78,
      (byte) 237,
      (byte) 21,
      (byte) 244,
      (byte) 80 /*0x50*/,
      (byte) 82,
      (byte) 220,
      (byte) 23,
      (byte) 252,
      (byte) 144 /*0x90*/,
      (byte) 21,
      (byte) 46,
      (byte) 175,
      (byte) 15,
      (byte) 173,
      (byte) 28,
      (byte) 189,
      (byte) 161,
      (byte) 217,
      (byte) 132,
      (byte) 34,
      (byte) 11,
      (byte) 154,
      (byte) 213,
      (byte) 229,
      (byte) 51,
      (byte) 246,
      (byte) 96 /*0x60*/,
      (byte) 180,
      (byte) 36,
      (byte) 53,
      (byte) 31 /*0x1F*/,
      (byte) 130,
      (byte) 40
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[42];
    numArray13[21] = (byte) 50;
    numArray13[1] = (byte) 213;
    numArray13[20] = (byte) 60;
    numArray13[17] = (byte) 143;
    numArray13[4] = (byte) 186;
    numArray13[5] = (byte) 245;
    numArray13[3] = (byte) 215;
    numArray13[7] = byte.MaxValue;
    numArray13[39] = (byte) 65;
    numArray13[12] = (byte) 140;
    numArray13[10] = (byte) 206;
    numArray13[37] = (byte) 41;
    numArray13[9] = (byte) 248;
    numArray13[2] = (byte) 29;
    numArray13[14] = (byte) 182;
    numArray13[8] = (byte) 110;
    numArray13[34] = (byte) 11;
    numArray13[32 /*0x20*/] = (byte) 152;
    numArray13[18] = (byte) 248;
    numArray13[31 /*0x1F*/] = (byte) 37;
    numArray13[13] = (byte) 106;
    numArray13[11] = (byte) 21;
    numArray13[22] = (byte) 232;
    numArray13[19] = (byte) 94;
    numArray13[24] = (byte) 154;
    numArray13[35] = (byte) 44;
    numArray13[26] = (byte) 121;
    numArray13[27] = (byte) 110;
    numArray13[23] = (byte) 35;
    numArray13[29] = (byte) 142;
    numArray13[30] = (byte) 241;
    numArray13[0] = (byte) 2;
    numArray13[15] = (byte) 102;
    numArray13[25] = (byte) 107;
    numArray13[6] = (byte) 199;
    numArray13[28] = (byte) 233;
    numArray13[16 /*0x10*/] = (byte) 67;
    numArray13[33] = (byte) 155;
    numArray13[38] = (byte) 159;
    numArray13[36] = (byte) 107;
    numArray13[40] = (byte) 191;
    numArray13[41] = (byte) 32 /*0x20*/;
    byte[] numArray14 = new byte[42];
    numArray14[38] = (byte) 244;
    numArray14[1] = (byte) 130;
    numArray14[13] = (byte) 250;
    numArray14[39] = (byte) 153;
    numArray14[4] = (byte) 108;
    numArray14[22] = (byte) 1;
    numArray14[6] = (byte) 233;
    numArray14[40] = (byte) 121;
    numArray14[8] = (byte) 41;
    numArray14[10] = (byte) 14;
    numArray14[28] = (byte) 39;
    numArray14[17] = (byte) 221;
    numArray14[21] = (byte) 23;
    numArray14[7] = (byte) 184;
    numArray14[12] = (byte) 61;
    numArray14[0] = (byte) 163;
    numArray14[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray14[37] = (byte) 180;
    numArray14[18] = (byte) 141;
    numArray14[19] = (byte) 245;
    numArray14[9] = (byte) 105;
    numArray14[2] = (byte) 106;
    numArray14[26] = (byte) 193;
    numArray14[11] = (byte) 233;
    numArray14[41] = (byte) 238;
    numArray14[24] = (byte) 13;
    numArray14[14] = (byte) 95;
    numArray14[27] = (byte) 85;
    numArray14[23] = (byte) 128 /*0x80*/;
    numArray14[29] = (byte) 138;
    numArray14[30] = (byte) 109;
    numArray14[3] = (byte) 76;
    numArray14[32 /*0x20*/] = (byte) 14;
    numArray14[33] = (byte) 87;
    numArray14[25] = (byte) 26;
    numArray14[35] = (byte) 252;
    numArray14[36] = (byte) 149;
    numArray14[15] = (byte) 149;
    numArray14[31 /*0x1F*/] = (byte) 221;
    numArray14[20] = (byte) 31 /*0x1F*/;
    numArray14[34] = (byte) 51;
    numArray14[5] = (byte) 6;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 42);
    for (int index = 0; index < 42; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13698()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[57];
      byte[] numArray2 = new byte[55];
      numArray2[26] = (byte) 16 /*0x10*/;
      numArray2[16 /*0x10*/] = (byte) 245;
      numArray2[22] = (byte) 88;
      numArray2[3] = (byte) 129;
      numArray2[4] = (byte) 12;
      numArray2[5] = (byte) 173;
      numArray2[8] = (byte) 162;
      numArray2[7] = (byte) 125;
      numArray2[23] = (byte) 117;
      numArray2[40] = (byte) 230;
      numArray2[6] = (byte) 176 /*0xB0*/;
      numArray2[29] = (byte) 28;
      numArray2[51] = (byte) 234;
      numArray2[9] = (byte) 133;
      numArray2[48 /*0x30*/] = (byte) 162;
      numArray2[15] = (byte) 13;
      numArray2[1] = (byte) 218;
      numArray2[28] = (byte) 106;
      numArray2[52] = (byte) 8;
      numArray2[19] = (byte) 46;
      numArray2[24] = (byte) 223;
      numArray2[42] = (byte) 43;
      numArray2[12] = (byte) 231;
      numArray2[20] = (byte) 63 /*0x3F*/;
      numArray2[13] = (byte) 181;
      numArray2[25] = (byte) 250;
      numArray2[18] = (byte) 150;
      numArray2[27] = (byte) 16 /*0x10*/;
      numArray2[17] = (byte) 67;
      numArray2[37] = (byte) 251;
      numArray2[30] = (byte) 73;
      numArray2[31 /*0x1F*/] = (byte) 14;
      numArray2[36] = (byte) 161;
      numArray2[33] = (byte) 207;
      numArray2[34] = (byte) 83;
      numArray2[35] = (byte) 223;
      numArray2[32 /*0x20*/] = (byte) 239;
      numArray2[10] = (byte) 167;
      numArray2[38] = (byte) 155;
      numArray2[39] = (byte) 12;
      numArray2[47] = (byte) 32 /*0x20*/;
      numArray2[41] = (byte) 169;
      numArray2[0] = (byte) 17;
      numArray2[21] = (byte) 60;
      numArray2[44] = (byte) 10;
      numArray2[45] = (byte) 114;
      numArray2[14] = (byte) 129;
      numArray2[11] = (byte) 170;
      numArray2[43] = (byte) 93;
      numArray2[49] = (byte) 59;
      numArray2[46] = (byte) 166;
      numArray2[50] = (byte) 148;
      numArray2[2] = (byte) 145;
      numArray2[53] = (byte) 196;
      numArray2[54] = (byte) 249;
      byte[] numArray3 = new byte[55]
      {
        (byte) 1,
        (byte) 33,
        (byte) 154,
        (byte) 86,
        (byte) 174,
        (byte) 94,
        (byte) 176 /*0xB0*/,
        (byte) 39,
        (byte) 42,
        (byte) 131,
        (byte) 62,
        (byte) 108,
        (byte) 59,
        (byte) 171,
        (byte) 9,
        (byte) 44,
        (byte) 115,
        (byte) 144 /*0x90*/,
        (byte) 52,
        (byte) 97,
        (byte) 187,
        (byte) 184,
        (byte) 248,
        (byte) 199,
        (byte) 243,
        (byte) 81,
        (byte) 231,
        (byte) 84,
        (byte) 76,
        (byte) 151,
        (byte) 86,
        (byte) 60,
        (byte) 18,
        (byte) 126,
        (byte) 0,
        (byte) 38,
        (byte) 50,
        (byte) 243,
        (byte) 47,
        (byte) 121,
        (byte) 210,
        (byte) 35,
        (byte) 35,
        (byte) 53,
        (byte) 152,
        (byte) 186,
        (byte) 131,
        (byte) 91,
        (byte) 242,
        (byte) 211,
        (byte) 14,
        (byte) 180,
        (byte) 196,
        (byte) 70,
        (byte) 162
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[2]{ (byte) 0, (byte) 52 };
      numArray4[0] = (byte) 177;
      byte[] numArray5 = new byte[2]{ (byte) 76, (byte) 26 };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[57];
    byte[] numArray7 = new byte[55]
    {
      (byte) 221,
      (byte) 147,
      (byte) 142,
      (byte) 64 /*0x40*/,
      (byte) 226,
      (byte) 69,
      (byte) 231,
      (byte) 160 /*0xA0*/,
      (byte) 29,
      (byte) 26,
      (byte) 1,
      (byte) 0,
      (byte) 108,
      (byte) 217,
      (byte) 248,
      (byte) 181,
      (byte) 196,
      (byte) 49,
      (byte) 109,
      (byte) 94,
      (byte) 238,
      (byte) 27,
      (byte) 135,
      (byte) 175,
      (byte) 115,
      (byte) 187,
      (byte) 214,
      (byte) 210,
      (byte) 36,
      (byte) 204,
      (byte) 194,
      (byte) 19,
      (byte) 176 /*0xB0*/,
      (byte) 167,
      (byte) 68,
      (byte) 222,
      (byte) 41,
      (byte) 78,
      (byte) 219,
      (byte) 187,
      (byte) 231,
      (byte) 248,
      (byte) 37,
      (byte) 100,
      (byte) 122,
      (byte) 98,
      (byte) 87,
      (byte) 173,
      (byte) 184,
      (byte) 127 /*0x7F*/,
      (byte) 248,
      (byte) 123,
      (byte) 196,
      (byte) 134,
      (byte) 14
    };
    byte[] numArray8 = new byte[55];
    numArray8[45] = (byte) 223;
    numArray8[1] = (byte) 165;
    numArray8[41] = (byte) 240 /*0xF0*/;
    numArray8[42] = (byte) 34;
    numArray8[4] = (byte) 220;
    numArray8[5] = (byte) 207;
    numArray8[6] = (byte) 76;
    numArray8[10] = (byte) 9;
    numArray8[8] = (byte) 194;
    numArray8[9] = (byte) 171;
    numArray8[11] = (byte) 36;
    numArray8[3] = (byte) 187;
    numArray8[12] = (byte) 127 /*0x7F*/;
    numArray8[51] = (byte) 193;
    numArray8[36] = (byte) 203;
    numArray8[40] = (byte) 101;
    numArray8[48 /*0x30*/] = (byte) 166;
    numArray8[14] = (byte) 162;
    numArray8[18] = (byte) 34;
    numArray8[43] = (byte) 205;
    numArray8[13] = (byte) 78;
    numArray8[49] = (byte) 21;
    numArray8[44] = (byte) 182;
    numArray8[17] = (byte) 226;
    numArray8[24] = (byte) 13;
    numArray8[7] = (byte) 161;
    numArray8[26] = (byte) 239;
    numArray8[15] = (byte) 75;
    numArray8[29] = (byte) 80 /*0x50*/;
    numArray8[47] = (byte) 103;
    numArray8[30] = (byte) 11;
    numArray8[31 /*0x1F*/] = (byte) 214;
    numArray8[25] = (byte) 70;
    numArray8[33] = (byte) 88;
    numArray8[23] = (byte) 92;
    numArray8[2] = (byte) 109;
    numArray8[0] = (byte) 213;
    numArray8[37] = (byte) 232;
    numArray8[38] = (byte) 248;
    numArray8[19] = (byte) 209;
    numArray8[16 /*0x10*/] = (byte) 200;
    numArray8[27] = (byte) 188;
    numArray8[35] = (byte) 78;
    numArray8[52] = (byte) 99;
    numArray8[21] = (byte) 222;
    numArray8[34] = (byte) 11;
    numArray8[22] = (byte) 186;
    numArray8[20] = (byte) 176 /*0xB0*/;
    numArray8[46] = (byte) 76;
    numArray8[28] = (byte) 52;
    numArray8[50] = (byte) 125;
    numArray8[39] = (byte) 87;
    numArray8[32 /*0x20*/] = (byte) 208 /*0xD0*/;
    numArray8[53] = (byte) 76;
    numArray8[54] = (byte) 1;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[2]{ (byte) 0, (byte) 189 };
    numArray9[0] = (byte) 169;
    byte[] numArray10 = new byte[2]{ (byte) 7, (byte) 151 };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 2);
    for (int index = 0; index < 2; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13699()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[52];
      byte[] numArray2 = new byte[52]
      {
        (byte) 158,
        (byte) 39,
        (byte) 44,
        (byte) 19,
        (byte) 71,
        (byte) 65,
        (byte) 245,
        (byte) 161,
        (byte) 102,
        (byte) 164,
        (byte) 120,
        (byte) 33,
        (byte) 21,
        (byte) 169,
        (byte) 254,
        (byte) 210,
        (byte) 17,
        (byte) 123,
        (byte) 177,
        (byte) 81,
        (byte) 32 /*0x20*/,
        (byte) 62,
        (byte) 25,
        (byte) 127 /*0x7F*/,
        (byte) 181,
        (byte) 233,
        (byte) 161,
        (byte) 195,
        (byte) 241,
        (byte) 8,
        (byte) 214,
        (byte) 165,
        (byte) 85,
        (byte) 251,
        (byte) 237,
        (byte) 235,
        (byte) 101,
        (byte) 4,
        (byte) 86,
        (byte) 177,
        (byte) 140,
        (byte) 132,
        (byte) 171,
        (byte) 106,
        (byte) 160 /*0xA0*/,
        (byte) 170,
        (byte) 210,
        byte.MaxValue,
        (byte) 190,
        (byte) 240 /*0xF0*/,
        (byte) 50,
        (byte) 128 /*0x80*/
      };
      byte[] numArray3 = new byte[52];
      numArray3[32 /*0x20*/] = (byte) 124;
      numArray3[4] = (byte) 102;
      numArray3[2] = (byte) 101;
      numArray3[3] = (byte) 205;
      numArray3[26] = (byte) 77;
      numArray3[50] = (byte) 44;
      numArray3[31 /*0x1F*/] = (byte) 40;
      numArray3[10] = (byte) 17;
      numArray3[8] = (byte) 241;
      numArray3[9] = (byte) 197;
      numArray3[33] = (byte) 252;
      numArray3[42] = (byte) 196;
      numArray3[11] = (byte) 125;
      numArray3[19] = (byte) 151;
      numArray3[15] = (byte) 62;
      numArray3[51] = (byte) 215;
      numArray3[48 /*0x30*/] = byte.MaxValue;
      numArray3[17] = (byte) 19;
      numArray3[13] = (byte) 224 /*0xE0*/;
      numArray3[6] = (byte) 186;
      numArray3[45] = (byte) 122;
      numArray3[21] = (byte) 174;
      numArray3[18] = (byte) 154;
      numArray3[0] = (byte) 93;
      numArray3[24] = (byte) 254;
      numArray3[38] = (byte) 19;
      numArray3[47] = (byte) 49;
      numArray3[27] = (byte) 125;
      numArray3[28] = (byte) 215;
      numArray3[7] = (byte) 253;
      numArray3[30] = (byte) 54;
      numArray3[23] = (byte) 181;
      numArray3[1] = (byte) 35;
      numArray3[12] = (byte) 58;
      numArray3[35] = (byte) 47;
      numArray3[46] = (byte) 68;
      numArray3[36] = (byte) 232;
      numArray3[37] = (byte) 224 /*0xE0*/;
      numArray3[14] = (byte) 34;
      numArray3[25] = (byte) 123;
      numArray3[40] = (byte) 31 /*0x1F*/;
      numArray3[41] = (byte) 241;
      numArray3[16 /*0x10*/] = (byte) 139;
      numArray3[43] = (byte) 228;
      numArray3[44] = (byte) 142;
      numArray3[5] = (byte) 73;
      numArray3[20] = (byte) 67;
      numArray3[22] = (byte) 99;
      numArray3[29] = (byte) 185;
      numArray3[39] = (byte) 172;
      numArray3[49] = (byte) 207;
      numArray3[34] = (byte) 57;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[52];
    byte[] numArray5 = new byte[52];
    numArray5[2] = (byte) 84;
    numArray5[35] = (byte) 192 /*0xC0*/;
    numArray5[7] = (byte) 71;
    numArray5[3] = (byte) 203;
    numArray5[20] = (byte) 44;
    numArray5[13] = (byte) 82;
    numArray5[6] = (byte) 83;
    numArray5[24] = (byte) 70;
    numArray5[8] = byte.MaxValue;
    numArray5[18] = (byte) 179;
    numArray5[10] = (byte) 174;
    numArray5[38] = (byte) 128 /*0x80*/;
    numArray5[21] = (byte) 238;
    numArray5[46] = (byte) 37;
    numArray5[22] = (byte) 105;
    numArray5[15] = (byte) 15;
    numArray5[16 /*0x10*/] = (byte) 44;
    numArray5[17] = (byte) 52;
    numArray5[49] = (byte) 169;
    numArray5[19] = (byte) 102;
    numArray5[28] = (byte) 94;
    numArray5[14] = (byte) 163;
    numArray5[25] = (byte) 18;
    numArray5[23] = (byte) 199;
    numArray5[40] = (byte) 83;
    numArray5[48 /*0x30*/] = (byte) 59;
    numArray5[26] = (byte) 17;
    numArray5[27] = (byte) 43;
    numArray5[11] = (byte) 136;
    numArray5[9] = (byte) 45;
    numArray5[12] = (byte) 119;
    numArray5[45] = (byte) 196;
    numArray5[32 /*0x20*/] = (byte) 175;
    numArray5[31 /*0x1F*/] = (byte) 125;
    numArray5[5] = (byte) 205;
    numArray5[47] = (byte) 122;
    numArray5[36] = (byte) 183;
    numArray5[43] = (byte) 45;
    numArray5[29] = (byte) 93;
    numArray5[33] = (byte) 251;
    numArray5[39] = (byte) 235;
    numArray5[41] = (byte) 177;
    numArray5[42] = (byte) 18;
    numArray5[51] = (byte) 118;
    numArray5[44] = (byte) 38;
    numArray5[34] = (byte) 91;
    numArray5[1] = (byte) 104;
    numArray5[0] = (byte) 37;
    numArray5[30] = (byte) 89;
    numArray5[4] = (byte) 93;
    numArray5[50] = (byte) 23;
    numArray5[37] = (byte) 195;
    byte[] numArray6 = new byte[52]
    {
      (byte) 123,
      (byte) 95,
      (byte) 16 /*0x10*/,
      (byte) 63 /*0x3F*/,
      (byte) 5,
      (byte) 143,
      (byte) 218,
      (byte) 185,
      (byte) 241,
      (byte) 82,
      (byte) 161,
      (byte) 89,
      (byte) 226,
      (byte) 180,
      (byte) 17,
      (byte) 118,
      (byte) 67,
      (byte) 159,
      (byte) 127 /*0x7F*/,
      (byte) 139,
      (byte) 123,
      (byte) 220,
      (byte) 22,
      (byte) 243,
      (byte) 157,
      (byte) 83,
      (byte) 30,
      (byte) 129,
      (byte) 116,
      (byte) 217,
      (byte) 112 /*0x70*/,
      (byte) 52,
      (byte) 137,
      (byte) 56,
      (byte) 36,
      (byte) 58,
      (byte) 18,
      (byte) 103,
      (byte) 2,
      (byte) 120,
      (byte) 80 /*0x50*/,
      (byte) 35,
      (byte) 18,
      (byte) 204,
      (byte) 39,
      (byte) 72,
      (byte) 141,
      (byte) 233,
      (byte) 155,
      (byte) 189,
      (byte) 200,
      (byte) 232
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 52);
    for (int index = 0; index < 52; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13700()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 171;
      numArray2[1] = (byte) 179;
      numArray2[2] = (byte) 90;
      numArray2[8] = (byte) 55;
      numArray2[4] = (byte) 235;
      numArray2[7] = (byte) 36;
      numArray2[6] = (byte) 93;
      numArray2[5] = (byte) 2;
      numArray2[3] = (byte) 195;
      numArray2[9] = (byte) 51;
      byte[] numArray3 = new byte[10]
      {
        (byte) 245,
        (byte) 144 /*0x90*/,
        (byte) 107,
        (byte) 242,
        (byte) 237,
        (byte) 85,
        (byte) 39,
        (byte) 163,
        (byte) 88,
        (byte) 99
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
      (byte) 107,
      (byte) 225,
      (byte) 172,
      (byte) 150,
      (byte) 67,
      (byte) 20,
      (byte) 151,
      (byte) 99,
      (byte) 152,
      (byte) 32 /*0x20*/
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 76,
      (byte) 17,
      (byte) 89,
      (byte) 21,
      (byte) 136,
      (byte) 152,
      (byte) 175,
      (byte) 68,
      (byte) 139,
      (byte) 93
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13701(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 63 /*0x3F*/,
      (byte) 59,
      (byte) 217,
      (byte) 11,
      (byte) 244,
      (byte) 149,
      (byte) 21,
      (byte) 104,
      (byte) 112 /*0x70*/,
      (byte) 3,
      (byte) 251,
      (byte) 176 /*0xB0*/,
      (byte) 55,
      (byte) 35,
      (byte) 129,
      (byte) 68,
      (byte) 112 /*0x70*/,
      (byte) 195,
      (byte) 0,
      (byte) 238,
      (byte) 226,
      (byte) 62,
      (byte) 107,
      (byte) 25,
      (byte) 15,
      (byte) 160 /*0xA0*/,
      (byte) 207,
      (byte) 194,
      (byte) 209,
      (byte) 225,
      (byte) 163,
      (byte) 148,
      (byte) 7,
      (byte) 36,
      (byte) 238,
      (byte) 240 /*0xF0*/,
      (byte) 165,
      (byte) 140,
      (byte) 242,
      (byte) 29,
      (byte) 86,
      (byte) 120,
      (byte) 60,
      (byte) 147,
      (byte) 71,
      (byte) 99,
      (byte) 95,
      (byte) 63 /*0x3F*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 197;
    sourceArray2[4] = (byte) 119;
    sourceArray2[2] = (byte) 61;
    sourceArray2[25] = (byte) 46;
    sourceArray2[29] = (byte) 212;
    sourceArray2[5] = (byte) 37;
    sourceArray2[13] = (byte) 85;
    sourceArray2[7] = (byte) 236;
    sourceArray2[8] = (byte) 206;
    sourceArray2[6] = (byte) 140;
    sourceArray2[10] = (byte) 43;
    sourceArray2[11] = (byte) 172;
    sourceArray2[12] = (byte) 144 /*0x90*/;
    sourceArray2[24] = (byte) 172;
    sourceArray2[30] = (byte) 160 /*0xA0*/;
    sourceArray2[17] = (byte) 168;
    sourceArray2[16 /*0x10*/] = (byte) 107;
    sourceArray2[37] = (byte) 156;
    sourceArray2[47] = (byte) 24;
    sourceArray2[19] = (byte) 131;
    sourceArray2[44] = (byte) 191;
    sourceArray2[21] = (byte) 239;
    sourceArray2[22] = (byte) 87;
    sourceArray2[15] = (byte) 185;
    sourceArray2[20] = (byte) 58;
    sourceArray2[1] = (byte) 180;
    sourceArray2[26] = (byte) 21;
    sourceArray2[27] = (byte) 145;
    sourceArray2[28] = (byte) 107;
    sourceArray2[9] = (byte) 71;
    sourceArray2[18] = (byte) 243;
    sourceArray2[31 /*0x1F*/] = (byte) 253;
    sourceArray2[32 /*0x20*/] = (byte) 153;
    sourceArray2[0] = (byte) 185;
    sourceArray2[39] = (byte) 26;
    sourceArray2[23] = (byte) 168;
    sourceArray2[3] = (byte) 9;
    sourceArray2[36] = (byte) 144 /*0x90*/;
    sourceArray2[38] = (byte) 33;
    sourceArray2[34] = (byte) 171;
    sourceArray2[40] = (byte) 16 /*0x10*/;
    sourceArray2[41] = (byte) 16 /*0x10*/;
    sourceArray2[14] = (byte) 53;
    sourceArray2[43] = (byte) 47;
    sourceArray2[35] = (byte) 20;
    sourceArray2[45] = (byte) 110;
    sourceArray2[46] = (byte) 47;
    sourceArray2[33] = (byte) 158;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[20];
    byte[] response2 = new byte[20];
    Array.Copy((Array) sc_13686.sspq, 65, (Array) numArray2, 0, 20);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 65, (Array) numArray2, 0, 20);
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

  internal static int ssp_appserver_13702(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 87;
    sourceArray1[18] = (byte) 113;
    sourceArray1[2] = (byte) 112 /*0x70*/;
    sourceArray1[27] = (byte) 35;
    sourceArray1[17] = (byte) 96 /*0x60*/;
    sourceArray1[5] = (byte) 50;
    sourceArray1[6] = (byte) 138;
    sourceArray1[38] = (byte) 251;
    sourceArray1[35] = (byte) 194;
    sourceArray1[33] = (byte) 62;
    sourceArray1[10] = (byte) 164;
    sourceArray1[11] = (byte) 240 /*0xF0*/;
    sourceArray1[23] = (byte) 41;
    sourceArray1[45] = (byte) 253;
    sourceArray1[14] = (byte) 133;
    sourceArray1[15] = (byte) 37;
    sourceArray1[16 /*0x10*/] = (byte) 185;
    sourceArray1[13] = (byte) 243;
    sourceArray1[30] = (byte) 205;
    sourceArray1[43] = (byte) 169;
    sourceArray1[20] = (byte) 233;
    sourceArray1[0] = (byte) 9;
    sourceArray1[22] = (byte) 239;
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[24] = (byte) 247;
    sourceArray1[37] = (byte) 130;
    sourceArray1[26] = (byte) 138;
    sourceArray1[3] = (byte) 185;
    sourceArray1[28] = (byte) 93;
    sourceArray1[29] = (byte) 63 /*0x3F*/;
    sourceArray1[9] = (byte) 84;
    sourceArray1[4] = (byte) 83;
    sourceArray1[7] = (byte) 148;
    sourceArray1[8] = (byte) 136;
    sourceArray1[34] = (byte) 67;
    sourceArray1[1] = (byte) 71;
    sourceArray1[32 /*0x20*/] = (byte) 246;
    sourceArray1[12] = (byte) 241;
    sourceArray1[19] = (byte) 70;
    sourceArray1[39] = (byte) 125;
    sourceArray1[40] = (byte) 171;
    sourceArray1[41] = (byte) 175;
    sourceArray1[42] = (byte) 41;
    sourceArray1[44] = (byte) 83;
    sourceArray1[36] = (byte) 21;
    sourceArray1[25] = (byte) 65;
    sourceArray1[46] = (byte) 94;
    sourceArray1[47] = (byte) 32 /*0x20*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 44,
      (byte) 222,
      (byte) 93,
      (byte) 234,
      (byte) 61,
      (byte) 74,
      (byte) 168,
      (byte) 31 /*0x1F*/,
      (byte) 79,
      (byte) 217,
      (byte) 126,
      (byte) 237,
      (byte) 146,
      (byte) 232,
      (byte) 205,
      (byte) 64 /*0x40*/,
      (byte) 67,
      (byte) 16 /*0x10*/,
      (byte) 179,
      (byte) 16 /*0x10*/,
      (byte) 65,
      (byte) 172,
      (byte) 82,
      (byte) 112 /*0x70*/,
      (byte) 122,
      byte.MaxValue,
      (byte) 15,
      (byte) 190,
      (byte) 101,
      (byte) 180,
      (byte) 211,
      (byte) 254,
      (byte) 186,
      (byte) 223,
      (byte) 57,
      (byte) 87,
      (byte) 221,
      (byte) 127 /*0x7F*/,
      (byte) 61,
      (byte) 160 /*0xA0*/,
      (byte) 69,
      (byte) 26,
      (byte) 165,
      (byte) 116,
      (byte) 147,
      (byte) 52,
      (byte) 166,
      (byte) 173
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[13];
    byte[] response2 = new byte[13];
    Array.Copy((Array) sc_13686.sspq, 85, (Array) numArray2, 0, 13);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 85, (Array) numArray2, 0, 13);
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

  internal static string ssp_appserver_13703()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 46,
        (byte) 63 /*0x3F*/,
        (byte) 241,
        (byte) 91,
        (byte) 242,
        (byte) 57,
        (byte) 133,
        (byte) 167,
        (byte) 8,
        (byte) 30
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 230,
        (byte) 235,
        (byte) 0,
        (byte) 41,
        (byte) 243,
        (byte) 85,
        (byte) 34,
        (byte) 151,
        (byte) 26,
        (byte) 47
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[9] = (byte) 248;
    numArray5[1] = (byte) 30;
    numArray5[7] = (byte) 245;
    numArray5[3] = (byte) 193;
    numArray5[0] = (byte) 123;
    numArray5[5] = (byte) 36;
    numArray5[2] = (byte) 86;
    numArray5[6] = (byte) 48 /*0x30*/;
    numArray5[8] = (byte) 56;
    numArray5[4] = (byte) 78;
    byte[] numArray6 = new byte[10]
    {
      (byte) 21,
      (byte) 176 /*0xB0*/,
      (byte) 171,
      (byte) 6,
      (byte) 202,
      (byte) 138,
      (byte) 232,
      (byte) 154,
      (byte) 4,
      (byte) 216
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[54];
    byte[] response = new byte[54];
    Array.Copy((Array) sc_13686.sspq, 98, (Array) numArray7, 0, 54);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13686.sspr, 98, (Array) numArray7, 0, 54);
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

  internal static string ssp_appserver_13704()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[91];
      byte[] numArray2 = new byte[55];
      numArray2[45] = (byte) 100;
      numArray2[1] = (byte) 160 /*0xA0*/;
      numArray2[5] = (byte) 236;
      numArray2[3] = (byte) 234;
      numArray2[4] = (byte) 253;
      numArray2[17] = (byte) 58;
      numArray2[39] = (byte) 169;
      numArray2[7] = (byte) 145;
      numArray2[8] = (byte) 79;
      numArray2[30] = (byte) 92;
      numArray2[36] = (byte) 90;
      numArray2[11] = (byte) 49;
      numArray2[12] = (byte) 176 /*0xB0*/;
      numArray2[15] = (byte) 177;
      numArray2[26] = (byte) 252;
      numArray2[18] = (byte) 96 /*0x60*/;
      numArray2[49] = (byte) 185;
      numArray2[31 /*0x1F*/] = (byte) 250;
      numArray2[10] = (byte) 224 /*0xE0*/;
      numArray2[19] = (byte) 161;
      numArray2[20] = (byte) 112 /*0x70*/;
      numArray2[14] = (byte) 212;
      numArray2[22] = (byte) 228;
      numArray2[47] = (byte) 199;
      numArray2[24] = (byte) 158;
      numArray2[35] = (byte) 192 /*0xC0*/;
      numArray2[51] = (byte) 232;
      numArray2[27] = (byte) 54;
      numArray2[28] = (byte) 149;
      numArray2[29] = (byte) 198;
      numArray2[40] = (byte) 98;
      numArray2[2] = (byte) 225;
      numArray2[25] = (byte) 201;
      numArray2[33] = (byte) 24;
      numArray2[34] = (byte) 124;
      numArray2[32 /*0x20*/] = (byte) 58;
      numArray2[13] = (byte) 242;
      numArray2[37] = (byte) 38;
      numArray2[38] = (byte) 155;
      numArray2[6] = (byte) 66;
      numArray2[21] = (byte) 189;
      numArray2[43] = (byte) 173;
      numArray2[42] = (byte) 229;
      numArray2[54] = (byte) 122;
      numArray2[46] = (byte) 229;
      numArray2[16 /*0x10*/] = (byte) 47;
      numArray2[0] = (byte) 40;
      numArray2[44] = (byte) 178;
      numArray2[23] = (byte) 115;
      numArray2[50] = (byte) 237;
      numArray2[48 /*0x30*/] = (byte) 5;
      numArray2[41] = (byte) 102;
      numArray2[9] = (byte) 187;
      numArray2[53] = (byte) 173;
      numArray2[52] = (byte) 101;
      byte[] numArray3 = new byte[55]
      {
        (byte) 12,
        (byte) 14,
        (byte) 127 /*0x7F*/,
        (byte) 218,
        (byte) 90,
        (byte) 165,
        (byte) 138,
        (byte) 164,
        (byte) 93,
        (byte) 157,
        (byte) 48 /*0x30*/,
        (byte) 81,
        (byte) 187,
        (byte) 212,
        (byte) 236,
        (byte) 188,
        (byte) 117,
        (byte) 147,
        (byte) 141,
        (byte) 39,
        (byte) 217,
        (byte) 84,
        (byte) 17,
        (byte) 149,
        (byte) 48 /*0x30*/,
        (byte) 13,
        (byte) 134,
        (byte) 120,
        (byte) 82,
        (byte) 187,
        (byte) 165,
        (byte) 5,
        (byte) 180,
        (byte) 157,
        (byte) 145,
        (byte) 223,
        (byte) 97,
        (byte) 239,
        (byte) 40,
        (byte) 188,
        (byte) 30,
        (byte) 133,
        (byte) 49,
        (byte) 133,
        (byte) 16 /*0x10*/,
        (byte) 133,
        (byte) 111,
        (byte) 240 /*0xF0*/,
        (byte) 253,
        (byte) 38,
        (byte) 67,
        (byte) 63 /*0x3F*/,
        (byte) 74,
        (byte) 178,
        (byte) 53
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36]
      {
        (byte) 172,
        (byte) 17,
        (byte) 78,
        (byte) 185,
        (byte) 219,
        (byte) 31 /*0x1F*/,
        (byte) 176 /*0xB0*/,
        (byte) 236,
        (byte) 81,
        (byte) 106,
        (byte) 249,
        (byte) 100,
        (byte) 15,
        (byte) 106,
        (byte) 47,
        (byte) 54,
        (byte) 253,
        (byte) 233,
        (byte) 159,
        (byte) 123,
        (byte) 232,
        (byte) 244,
        (byte) 204,
        (byte) 151,
        (byte) 228,
        (byte) 64 /*0x40*/,
        (byte) 120,
        (byte) 245,
        (byte) 41,
        (byte) 230,
        (byte) 30,
        (byte) 45,
        (byte) 126,
        (byte) 127 /*0x7F*/,
        (byte) 63 /*0x3F*/,
        (byte) 200
      };
      byte[] numArray5 = new byte[36]
      {
        (byte) 252,
        (byte) 161,
        (byte) 149,
        (byte) 49,
        (byte) 78,
        (byte) 252,
        (byte) 110,
        (byte) 153,
        (byte) 171,
        (byte) 217,
        (byte) 26,
        (byte) 5,
        (byte) 25,
        (byte) 205,
        (byte) 181,
        (byte) 55,
        (byte) 213,
        (byte) 67,
        (byte) 193,
        (byte) 211,
        (byte) 31 /*0x1F*/,
        (byte) 97,
        (byte) 44,
        (byte) 174,
        (byte) 37,
        (byte) 217,
        (byte) 220,
        (byte) 41,
        (byte) 125,
        (byte) 230,
        (byte) 170,
        (byte) 21,
        (byte) 240 /*0xF0*/,
        (byte) 81,
        (byte) 215,
        (byte) 177
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[91];
    byte[] numArray7 = new byte[55]
    {
      (byte) 75,
      (byte) 171,
      (byte) 98,
      (byte) 122,
      (byte) 2,
      (byte) 83,
      (byte) 236,
      (byte) 225,
      (byte) 248,
      (byte) 180,
      (byte) 20,
      (byte) 176 /*0xB0*/,
      (byte) 132,
      (byte) 209,
      (byte) 23,
      (byte) 156,
      (byte) 183,
      (byte) 68,
      (byte) 183,
      (byte) 213,
      (byte) 155,
      (byte) 166,
      (byte) 187,
      (byte) 194,
      (byte) 106,
      (byte) 75,
      (byte) 204,
      (byte) 229,
      (byte) 144 /*0x90*/,
      (byte) 42,
      (byte) 102,
      (byte) 27,
      (byte) 252,
      (byte) 110,
      (byte) 84,
      (byte) 208 /*0xD0*/,
      (byte) 134,
      (byte) 121,
      (byte) 202,
      (byte) 67,
      (byte) 247,
      (byte) 94,
      (byte) 128 /*0x80*/,
      (byte) 65,
      (byte) 21,
      (byte) 54,
      (byte) 154,
      (byte) 91,
      (byte) 103,
      (byte) 227,
      (byte) 136,
      (byte) 132,
      (byte) 98,
      (byte) 43,
      (byte) 223
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 173,
      (byte) 199,
      (byte) 80 /*0x50*/,
      (byte) 113,
      (byte) 153,
      (byte) 191,
      (byte) 37,
      (byte) 35,
      (byte) 58,
      (byte) 162,
      (byte) 91,
      (byte) 88,
      (byte) 188,
      (byte) 27,
      (byte) 197,
      (byte) 186,
      (byte) 189,
      (byte) 13,
      (byte) 9,
      (byte) 180,
      (byte) 75,
      (byte) 162,
      (byte) 192 /*0xC0*/,
      (byte) 254,
      (byte) 86,
      (byte) 200,
      (byte) 216,
      (byte) 157,
      (byte) 199,
      (byte) 171,
      (byte) 62,
      (byte) 214,
      (byte) 141,
      (byte) 68,
      (byte) 99,
      (byte) 24,
      (byte) 58,
      (byte) 228,
      (byte) 167,
      (byte) 125,
      (byte) 246,
      (byte) 113,
      (byte) 199,
      (byte) 36,
      byte.MaxValue,
      (byte) 95,
      (byte) 144 /*0x90*/,
      (byte) 56,
      (byte) 2,
      (byte) 192 /*0xC0*/,
      (byte) 99,
      (byte) 158,
      (byte) 89,
      (byte) 245,
      (byte) 46
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[36];
    numArray9[2] = (byte) 183;
    numArray9[3] = (byte) 134;
    numArray9[4] = (byte) 7;
    numArray9[32 /*0x20*/] = (byte) 175;
    numArray9[29] = (byte) 185;
    numArray9[0] = (byte) 159;
    numArray9[20] = (byte) 122;
    numArray9[7] = (byte) 77;
    numArray9[8] = (byte) 248;
    numArray9[22] = (byte) 52;
    numArray9[10] = (byte) 22;
    numArray9[12] = (byte) 71;
    numArray9[9] = (byte) 180;
    numArray9[13] = (byte) 21;
    numArray9[17] = (byte) 221;
    numArray9[15] = (byte) 122;
    numArray9[16 /*0x10*/] = (byte) 115;
    numArray9[1] = (byte) 16 /*0x10*/;
    numArray9[11] = (byte) 125;
    numArray9[25] = (byte) 126;
    numArray9[33] = (byte) 223;
    numArray9[21] = (byte) 173;
    numArray9[19] = (byte) 38;
    numArray9[14] = (byte) 61;
    numArray9[24] = (byte) 112 /*0x70*/;
    numArray9[23] = (byte) 177;
    numArray9[34] = (byte) 231;
    numArray9[6] = (byte) 205;
    numArray9[28] = (byte) 226;
    numArray9[5] = (byte) 81;
    numArray9[30] = (byte) 209;
    numArray9[31 /*0x1F*/] = (byte) 143;
    numArray9[18] = (byte) 60;
    numArray9[26] = (byte) 173;
    numArray9[27] = (byte) 162;
    numArray9[35] = (byte) 105;
    byte[] numArray10 = new byte[36]
    {
      (byte) 42,
      (byte) 4,
      (byte) 19,
      (byte) 225,
      (byte) 233,
      (byte) 238,
      (byte) 107,
      (byte) 160 /*0xA0*/,
      (byte) 114,
      (byte) 198,
      (byte) 107,
      (byte) 228,
      (byte) 54,
      (byte) 193,
      (byte) 159,
      (byte) 136,
      (byte) 188,
      (byte) 177,
      (byte) 253,
      (byte) 10,
      (byte) 218,
      (byte) 188,
      (byte) 45,
      (byte) 28,
      (byte) 155,
      (byte) 138,
      (byte) 164,
      (byte) 1,
      (byte) 40,
      (byte) 109,
      (byte) 56,
      (byte) 69,
      (byte) 153,
      (byte) 25,
      (byte) 68,
      (byte) 42
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 36);
    for (int index = 0; index < 36; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13705()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 144 /*0x90*/,
        (byte) 178,
        (byte) 45,
        (byte) 146,
        (byte) 93,
        (byte) 223,
        (byte) 87,
        (byte) 216,
        (byte) 201,
        (byte) 131
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 0,
        (byte) 81,
        (byte) 6,
        (byte) 131,
        (byte) 69,
        (byte) 46,
        (byte) 200,
        (byte) 236,
        (byte) 129,
        (byte) 136
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 105;
    numArray5[2] = (byte) 13;
    numArray5[1] = (byte) 141;
    numArray5[3] = (byte) 166;
    numArray5[4] = (byte) 102;
    numArray5[7] = (byte) 136;
    numArray5[0] = (byte) 245;
    numArray5[6] = (byte) 135;
    numArray5[8] = (byte) 222;
    numArray5[9] = (byte) 159;
    byte[] numArray6 = new byte[10];
    numArray6[7] = (byte) 182;
    numArray6[8] = (byte) 173;
    numArray6[1] = (byte) 35;
    numArray6[3] = (byte) 3;
    numArray6[2] = (byte) 167;
    numArray6[5] = (byte) 114;
    numArray6[6] = (byte) 26;
    numArray6[9] = (byte) 61;
    numArray6[0] = (byte) 249;
    numArray6[4] = (byte) 31 /*0x1F*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13706()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 56,
        (byte) 70,
        (byte) 90,
        (byte) 15,
        (byte) 134,
        (byte) 197,
        (byte) 236,
        (byte) 254,
        (byte) 178,
        (byte) 18
      };
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 115;
      numArray3[7] = (byte) 71;
      numArray3[0] = (byte) 197;
      numArray3[3] = (byte) 192 /*0xC0*/;
      numArray3[9] = (byte) 53;
      numArray3[5] = (byte) 14;
      numArray3[6] = (byte) 98;
      numArray3[4] = (byte) 159;
      numArray3[1] = (byte) 53;
      numArray3[2] = (byte) 243;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 53,
      (byte) 35,
      (byte) 59,
      (byte) 165,
      (byte) 179,
      (byte) 246,
      (byte) 104,
      (byte) 64 /*0x40*/,
      (byte) 98,
      (byte) 117
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 226,
      (byte) 138,
      (byte) 231,
      (byte) 133,
      (byte) 95,
      (byte) 73,
      (byte) 15,
      (byte) 201,
      (byte) 58,
      (byte) 240 /*0xF0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_13686.sspq, 152, (Array) numArray7, 0, 49);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13686.sspr, 152, (Array) numArray7, 0, 49);
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

  internal static string ssp_appserver_13707()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 1,
        (byte) 55,
        (byte) 194,
        (byte) 76,
        (byte) 34,
        (byte) 61,
        (byte) 142,
        (byte) 141,
        (byte) 229,
        (byte) 233
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 65,
        (byte) 55,
        (byte) 177,
        (byte) 58,
        (byte) 49,
        (byte) 214,
        (byte) 127 /*0x7F*/,
        (byte) 39,
        (byte) 79,
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
      (byte) 244,
      (byte) 157,
      (byte) 168,
      (byte) 66,
      (byte) 108,
      (byte) 126,
      (byte) 240 /*0xF0*/,
      (byte) 82,
      (byte) 105,
      (byte) 250
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 96 /*0x60*/,
      (byte) 92,
      (byte) 40,
      (byte) 227,
      (byte) 216,
      (byte) 239,
      (byte) 188,
      (byte) 94,
      (byte) 101,
      (byte) 195
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13708()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[205];
      byte[] numArray2 = new byte[55]
      {
        (byte) 150,
        (byte) 252,
        (byte) 121,
        (byte) 109,
        (byte) 198,
        (byte) 205,
        (byte) 138,
        (byte) 10,
        (byte) 177,
        (byte) 23,
        (byte) 100,
        (byte) 203,
        (byte) 40,
        (byte) 188,
        (byte) 63 /*0x3F*/,
        (byte) 36,
        (byte) 159,
        (byte) 130,
        (byte) 7,
        (byte) 181,
        (byte) 45,
        (byte) 23,
        (byte) 122,
        (byte) 124,
        (byte) 45,
        (byte) 5,
        (byte) 20,
        (byte) 240 /*0xF0*/,
        (byte) 152,
        (byte) 61,
        (byte) 5,
        (byte) 181,
        (byte) 139,
        (byte) 116,
        (byte) 248,
        (byte) 144 /*0x90*/,
        (byte) 108,
        (byte) 123,
        (byte) 158,
        (byte) 119,
        (byte) 78,
        (byte) 234,
        (byte) 235,
        (byte) 144 /*0x90*/,
        (byte) 222,
        (byte) 160 /*0xA0*/,
        (byte) 85,
        (byte) 52,
        (byte) 19,
        (byte) 69,
        (byte) 91,
        (byte) 59,
        (byte) 143,
        (byte) 225,
        (byte) 155
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 242,
        (byte) 101,
        (byte) 136,
        (byte) 155,
        (byte) 184,
        (byte) 184,
        (byte) 189,
        (byte) 233,
        (byte) 9,
        (byte) 106,
        (byte) 228,
        (byte) 205,
        (byte) 193,
        (byte) 70,
        (byte) 212,
        (byte) 251,
        (byte) 185,
        (byte) 125,
        (byte) 76,
        (byte) 175,
        (byte) 249,
        (byte) 110,
        (byte) 65,
        (byte) 183,
        (byte) 71,
        (byte) 239,
        (byte) 220,
        (byte) 92,
        (byte) 186,
        (byte) 216,
        (byte) 120,
        (byte) 44,
        (byte) 181,
        (byte) 50,
        (byte) 169,
        (byte) 126,
        (byte) 237,
        (byte) 231,
        (byte) 116,
        (byte) 137,
        (byte) 151,
        (byte) 83,
        (byte) 188,
        (byte) 203,
        (byte) 13,
        (byte) 240 /*0xF0*/,
        (byte) 183,
        (byte) 101,
        (byte) 31 /*0x1F*/,
        (byte) 116,
        (byte) 45,
        (byte) 139,
        (byte) 143,
        (byte) 17,
        (byte) 91
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 252,
        (byte) 63 /*0x3F*/,
        (byte) 193,
        (byte) 185,
        (byte) 153,
        (byte) 114,
        (byte) 116,
        (byte) 17,
        (byte) 174,
        (byte) 64 /*0x40*/,
        (byte) 180,
        (byte) 196,
        (byte) 174,
        (byte) 184,
        (byte) 35,
        (byte) 228,
        (byte) 144 /*0x90*/,
        (byte) 103,
        (byte) 67,
        (byte) 44,
        (byte) 233,
        (byte) 158,
        (byte) 140,
        (byte) 175,
        (byte) 223,
        (byte) 221,
        (byte) 83,
        (byte) 226,
        (byte) 202,
        (byte) 38,
        (byte) 170,
        (byte) 216,
        (byte) 122,
        (byte) 14,
        (byte) 111,
        (byte) 59,
        (byte) 220,
        (byte) 190,
        (byte) 180,
        (byte) 120,
        (byte) 119,
        (byte) 127 /*0x7F*/,
        (byte) 48 /*0x30*/,
        (byte) 96 /*0x60*/,
        (byte) 161,
        (byte) 112 /*0x70*/,
        (byte) 100,
        (byte) 141,
        (byte) 55,
        (byte) 86,
        (byte) 254,
        (byte) 121,
        (byte) 88,
        (byte) 81,
        (byte) 152
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 126,
        (byte) 158,
        (byte) 94,
        (byte) 52,
        (byte) 209,
        (byte) 64 /*0x40*/,
        (byte) 60,
        (byte) 175,
        (byte) 120,
        (byte) 2,
        (byte) 165,
        (byte) 200,
        (byte) 111,
        (byte) 217,
        (byte) 43,
        (byte) 134,
        (byte) 49,
        (byte) 177,
        byte.MaxValue,
        (byte) 120,
        (byte) 28,
        (byte) 67,
        (byte) 141,
        (byte) 102,
        (byte) 95,
        (byte) 12,
        (byte) 38,
        (byte) 237,
        (byte) 23,
        (byte) 94,
        (byte) 44,
        (byte) 150,
        (byte) 50,
        (byte) 63 /*0x3F*/,
        (byte) 7,
        (byte) 21,
        (byte) 132,
        (byte) 199,
        (byte) 66,
        (byte) 87,
        (byte) 120,
        (byte) 181,
        (byte) 145,
        (byte) 50,
        (byte) 110,
        (byte) 89,
        (byte) 182,
        (byte) 72,
        (byte) 34,
        (byte) 54,
        (byte) 47,
        (byte) 182,
        (byte) 59,
        (byte) 10,
        (byte) 64 /*0x40*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[1] = (byte) 217;
      numArray6[49] = (byte) 198;
      numArray6[20] = (byte) 43;
      numArray6[3] = (byte) 31 /*0x1F*/;
      numArray6[13] = (byte) 7;
      numArray6[6] = (byte) 136;
      numArray6[14] = (byte) 210;
      numArray6[7] = (byte) 149;
      numArray6[44] = (byte) 195;
      numArray6[26] = (byte) 115;
      numArray6[10] = (byte) 160 /*0xA0*/;
      numArray6[11] = (byte) 72;
      numArray6[12] = (byte) 232;
      numArray6[9] = (byte) 74;
      numArray6[0] = (byte) 17;
      numArray6[36] = (byte) 248;
      numArray6[16 /*0x10*/] = (byte) 203;
      numArray6[28] = (byte) 49;
      numArray6[18] = (byte) 106;
      numArray6[19] = (byte) 109;
      numArray6[54] = (byte) 135;
      numArray6[35] = (byte) 228;
      numArray6[22] = (byte) 111;
      numArray6[23] = (byte) 121;
      numArray6[15] = (byte) 179;
      numArray6[38] = (byte) 229;
      numArray6[48 /*0x30*/] = (byte) 43;
      numArray6[27] = (byte) 39;
      numArray6[50] = (byte) 141;
      numArray6[29] = (byte) 127 /*0x7F*/;
      numArray6[51] = (byte) 156;
      numArray6[31 /*0x1F*/] = (byte) 117;
      numArray6[32 /*0x20*/] = (byte) 17;
      numArray6[33] = (byte) 136;
      numArray6[34] = (byte) 203;
      numArray6[52] = (byte) 46;
      numArray6[8] = (byte) 219;
      numArray6[2] = (byte) 188;
      numArray6[42] = (byte) 197;
      numArray6[39] = (byte) 56;
      numArray6[40] = (byte) 167;
      numArray6[4] = (byte) 146;
      numArray6[37] = (byte) 88;
      numArray6[43] = (byte) 38;
      numArray6[25] = (byte) 171;
      numArray6[45] = (byte) 68;
      numArray6[46] = (byte) 79;
      numArray6[24] = (byte) 198;
      numArray6[21] = (byte) 14;
      numArray6[5] = (byte) 70;
      numArray6[41] = (byte) 203;
      numArray6[17] = (byte) 181;
      numArray6[47] = (byte) 79;
      numArray6[53] = (byte) 240 /*0xF0*/;
      numArray6[30] = (byte) 40;
      byte[] numArray7 = new byte[55];
      numArray7[13] = (byte) 162;
      numArray7[51] = (byte) 153;
      numArray7[2] = (byte) 99;
      numArray7[50] = (byte) 9;
      numArray7[36] = (byte) 189;
      numArray7[1] = (byte) 165;
      numArray7[6] = (byte) 173;
      numArray7[7] = (byte) 166;
      numArray7[8] = (byte) 180;
      numArray7[9] = (byte) 180;
      numArray7[16 /*0x10*/] = (byte) 98;
      numArray7[10] = (byte) 81;
      numArray7[39] = (byte) 230;
      numArray7[5] = (byte) 163;
      numArray7[14] = (byte) 224 /*0xE0*/;
      numArray7[34] = (byte) 115;
      numArray7[33] = (byte) 135;
      numArray7[17] = (byte) 79;
      numArray7[12] = (byte) 46;
      numArray7[19] = (byte) 215;
      numArray7[20] = (byte) 70;
      numArray7[21] = byte.MaxValue;
      numArray7[45] = (byte) 59;
      numArray7[30] = (byte) 1;
      numArray7[24] = (byte) 137;
      numArray7[53] = (byte) 186;
      numArray7[26] = (byte) 131;
      numArray7[40] = (byte) 5;
      numArray7[28] = (byte) 181;
      numArray7[29] = (byte) 53;
      numArray7[52] = (byte) 27;
      numArray7[0] = (byte) 147;
      numArray7[32 /*0x20*/] = (byte) 137;
      numArray7[11] = (byte) 17;
      numArray7[18] = (byte) 227;
      numArray7[35] = (byte) 209;
      numArray7[27] = (byte) 155;
      numArray7[22] = (byte) 35;
      numArray7[37] = (byte) 157;
      numArray7[4] = (byte) 35;
      numArray7[25] = (byte) 63 /*0x3F*/;
      numArray7[41] = (byte) 162;
      numArray7[42] = (byte) 74;
      numArray7[43] = (byte) 13;
      numArray7[44] = (byte) 118;
      numArray7[3] = (byte) 219;
      numArray7[46] = (byte) 65;
      numArray7[47] = (byte) 8;
      numArray7[31 /*0x1F*/] = (byte) 106;
      numArray7[54] = (byte) 198;
      numArray7[49] = (byte) 215;
      numArray7[23] = (byte) 102;
      numArray7[15] = (byte) 134;
      numArray7[48 /*0x30*/] = (byte) 205;
      numArray7[38] = (byte) 150;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[40]
      {
        (byte) 112 /*0x70*/,
        (byte) 100,
        (byte) 30,
        (byte) 159,
        (byte) 167,
        (byte) 151,
        (byte) 146,
        (byte) 213,
        (byte) 152,
        (byte) 30,
        (byte) 133,
        (byte) 99,
        (byte) 22,
        (byte) 222,
        (byte) 73,
        (byte) 95,
        (byte) 23,
        (byte) 159,
        (byte) 135,
        (byte) 223,
        (byte) 213,
        (byte) 104,
        (byte) 123,
        (byte) 220,
        (byte) 110,
        (byte) 241,
        (byte) 92,
        (byte) 45,
        (byte) 198,
        (byte) 28,
        (byte) 77,
        (byte) 168,
        (byte) 52,
        (byte) 128 /*0x80*/,
        (byte) 183,
        (byte) 196,
        (byte) 84,
        (byte) 23,
        (byte) 57,
        (byte) 216
      };
      byte[] numArray9 = new byte[40];
      numArray9[3] = (byte) 222;
      numArray9[6] = (byte) 151;
      numArray9[28] = (byte) 5;
      numArray9[38] = (byte) 247;
      numArray9[4] = (byte) 79;
      numArray9[2] = (byte) 182;
      numArray9[7] = (byte) 208 /*0xD0*/;
      numArray9[22] = (byte) 21;
      numArray9[5] = (byte) 111;
      numArray9[29] = (byte) 87;
      numArray9[14] = (byte) 137;
      numArray9[11] = (byte) 64 /*0x40*/;
      numArray9[31 /*0x1F*/] = (byte) 2;
      numArray9[13] = (byte) 133;
      numArray9[20] = (byte) 12;
      numArray9[34] = (byte) 89;
      numArray9[32 /*0x20*/] = (byte) 124;
      numArray9[0] = (byte) 62;
      numArray9[30] = (byte) 66;
      numArray9[19] = (byte) 167;
      numArray9[17] = (byte) 11;
      numArray9[8] = (byte) 234;
      numArray9[27] = (byte) 18;
      numArray9[23] = (byte) 210;
      numArray9[24] = (byte) 129;
      numArray9[25] = (byte) 21;
      numArray9[12] = (byte) 87;
      numArray9[1] = (byte) 145;
      numArray9[10] = (byte) 177;
      numArray9[26] = (byte) 185;
      numArray9[15] = (byte) 215;
      numArray9[18] = (byte) 157;
      numArray9[9] = (byte) 205;
      numArray9[33] = (byte) 249;
      numArray9[16 /*0x10*/] = (byte) 125;
      numArray9[35] = (byte) 172;
      numArray9[36] = (byte) 162;
      numArray9[37] = (byte) 165;
      numArray9[21] = (byte) 93;
      numArray9[39] = (byte) 208 /*0xD0*/;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[205];
    byte[] numArray11 = new byte[55];
    numArray11[15] = (byte) 8;
    numArray11[53] = (byte) 54;
    numArray11[42] = (byte) 250;
    numArray11[0] = (byte) 43;
    numArray11[4] = (byte) 56;
    numArray11[5] = (byte) 123;
    numArray11[2] = (byte) 66;
    numArray11[27] = (byte) 187;
    numArray11[8] = (byte) 17;
    numArray11[48 /*0x30*/] = (byte) 173;
    numArray11[10] = (byte) 131;
    numArray11[11] = (byte) 156;
    numArray11[12] = (byte) 8;
    numArray11[13] = (byte) 138;
    numArray11[14] = (byte) 158;
    numArray11[40] = (byte) 120;
    numArray11[36] = (byte) 142;
    numArray11[38] = (byte) 246;
    numArray11[37] = (byte) 220;
    numArray11[19] = byte.MaxValue;
    numArray11[20] = (byte) 217;
    numArray11[6] = (byte) 174;
    numArray11[22] = (byte) 23;
    numArray11[23] = (byte) 164;
    numArray11[31 /*0x1F*/] = (byte) 121;
    numArray11[52] = (byte) 29;
    numArray11[26] = (byte) 201;
    numArray11[16 /*0x10*/] = (byte) 45;
    numArray11[28] = (byte) 71;
    numArray11[29] = (byte) 112 /*0x70*/;
    numArray11[30] = (byte) 0;
    numArray11[21] = (byte) 142;
    numArray11[32 /*0x20*/] = (byte) 5;
    numArray11[45] = (byte) 18;
    numArray11[46] = (byte) 105;
    numArray11[9] = (byte) 230;
    numArray11[35] = (byte) 120;
    numArray11[34] = (byte) 227;
    numArray11[39] = (byte) 27;
    numArray11[17] = (byte) 14;
    numArray11[54] = (byte) 245;
    numArray11[41] = (byte) 170;
    numArray11[51] = (byte) 33;
    numArray11[43] = (byte) 26;
    numArray11[44] = (byte) 227;
    numArray11[3] = (byte) 0;
    numArray11[33] = (byte) 150;
    numArray11[47] = (byte) 177;
    numArray11[1] = (byte) 4;
    numArray11[49] = (byte) 153;
    numArray11[50] = (byte) 164;
    numArray11[7] = (byte) 218;
    numArray11[24] = (byte) 248;
    numArray11[18] = (byte) 30;
    numArray11[25] = (byte) 137;
    byte[] numArray12 = new byte[55];
    numArray12[45] = (byte) 13;
    numArray12[1] = (byte) 188;
    numArray12[2] = (byte) 242;
    numArray12[3] = (byte) 21;
    numArray12[0] = (byte) 39;
    numArray12[5] = (byte) 238;
    numArray12[6] = (byte) 33;
    numArray12[51] = (byte) 36;
    numArray12[24] = (byte) 160 /*0xA0*/;
    numArray12[32 /*0x20*/] = (byte) 33;
    numArray12[39] = (byte) 253;
    numArray12[29] = (byte) 84;
    numArray12[12] = (byte) 121;
    numArray12[4] = (byte) 221;
    numArray12[37] = (byte) 177;
    numArray12[28] = (byte) 202;
    numArray12[16 /*0x10*/] = (byte) 28;
    numArray12[17] = (byte) 146;
    numArray12[18] = (byte) 98;
    numArray12[15] = (byte) 93;
    numArray12[20] = (byte) 173;
    numArray12[23] = (byte) 243;
    numArray12[40] = (byte) 69;
    numArray12[54] = (byte) 131;
    numArray12[14] = (byte) 154;
    numArray12[13] = (byte) 79;
    numArray12[21] = (byte) 106;
    numArray12[27] = (byte) 64 /*0x40*/;
    numArray12[38] = (byte) 139;
    numArray12[46] = (byte) 109;
    numArray12[52] = (byte) 181;
    numArray12[7] = (byte) 218;
    numArray12[11] = (byte) 254;
    numArray12[25] = (byte) 185;
    numArray12[34] = (byte) 182;
    numArray12[35] = (byte) 142;
    numArray12[36] = (byte) 111;
    numArray12[9] = (byte) 9;
    numArray12[41] = (byte) 129;
    numArray12[22] = (byte) 93;
    numArray12[30] = (byte) 52;
    numArray12[26] = (byte) 48 /*0x30*/;
    numArray12[42] = (byte) 43;
    numArray12[8] = (byte) 147;
    numArray12[44] = (byte) 204;
    numArray12[48 /*0x30*/] = (byte) 192 /*0xC0*/;
    numArray12[19] = (byte) 28;
    numArray12[47] = (byte) 66;
    numArray12[33] = (byte) 207;
    numArray12[49] = (byte) 30;
    numArray12[50] = (byte) 36;
    numArray12[43] = (byte) 131;
    numArray12[10] = (byte) 105;
    numArray12[53] = (byte) 193;
    numArray12[31 /*0x1F*/] = (byte) 248;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[23] = (byte) 3;
    numArray13[9] = (byte) 170;
    numArray13[54] = (byte) 141;
    numArray13[37] = (byte) 68;
    numArray13[32 /*0x20*/] = (byte) 113;
    numArray13[46] = (byte) 235;
    numArray13[43] = (byte) 6;
    numArray13[3] = (byte) 139;
    numArray13[8] = (byte) 109;
    numArray13[38] = (byte) 67;
    numArray13[26] = (byte) 247;
    numArray13[11] = (byte) 33;
    numArray13[21] = (byte) 189;
    numArray13[13] = (byte) 115;
    numArray13[4] = (byte) 92;
    numArray13[15] = (byte) 26;
    numArray13[16 /*0x10*/] = (byte) 201;
    numArray13[52] = (byte) 204;
    numArray13[6] = (byte) 91;
    numArray13[19] = (byte) 182;
    numArray13[20] = (byte) 153;
    numArray13[5] = (byte) 84;
    numArray13[22] = (byte) 185;
    numArray13[39] = (byte) 223;
    numArray13[44] = (byte) 47;
    numArray13[17] = (byte) 97;
    numArray13[7] = (byte) 46;
    numArray13[36] = (byte) 80 /*0x50*/;
    numArray13[28] = (byte) 160 /*0xA0*/;
    numArray13[29] = (byte) 192 /*0xC0*/;
    numArray13[30] = (byte) 112 /*0x70*/;
    numArray13[41] = (byte) 0;
    numArray13[51] = (byte) 76;
    numArray13[33] = (byte) 116;
    numArray13[34] = (byte) 32 /*0x20*/;
    numArray13[12] = byte.MaxValue;
    numArray13[53] = (byte) 22;
    numArray13[49] = (byte) 28;
    numArray13[0] = (byte) 244;
    numArray13[1] = (byte) 182;
    numArray13[40] = (byte) 41;
    numArray13[10] = (byte) 246;
    numArray13[42] = (byte) 189;
    numArray13[35] = (byte) 251;
    numArray13[50] = (byte) 182;
    numArray13[27] = (byte) 162;
    numArray13[31 /*0x1F*/] = (byte) 43;
    numArray13[47] = (byte) 36;
    numArray13[48 /*0x30*/] = (byte) 12;
    numArray13[24] = (byte) 98;
    numArray13[2] = (byte) 102;
    numArray13[25] = (byte) 156;
    numArray13[18] = (byte) 226;
    numArray13[14] = (byte) 202;
    numArray13[45] = (byte) 137;
    byte[] numArray14 = new byte[55]
    {
      (byte) 174,
      (byte) 209,
      (byte) 78,
      (byte) 190,
      (byte) 140,
      (byte) 36,
      (byte) 30,
      (byte) 240 /*0xF0*/,
      (byte) 172,
      (byte) 165,
      (byte) 49,
      (byte) 140,
      (byte) 54,
      (byte) 167,
      (byte) 111,
      (byte) 80 /*0x50*/,
      (byte) 87,
      (byte) 111,
      (byte) 167,
      (byte) 212,
      (byte) 215,
      (byte) 154,
      (byte) 10,
      (byte) 103,
      (byte) 46,
      (byte) 158,
      (byte) 39,
      (byte) 196,
      (byte) 55,
      (byte) 95,
      (byte) 227,
      (byte) 62,
      (byte) 121,
      (byte) 46,
      (byte) 96 /*0x60*/,
      (byte) 99,
      (byte) 87,
      (byte) 234,
      (byte) 60,
      (byte) 49,
      (byte) 191,
      (byte) 224 /*0xE0*/,
      (byte) 138,
      (byte) 221,
      (byte) 229,
      (byte) 123,
      (byte) 201,
      (byte) 149,
      (byte) 43,
      (byte) 199,
      (byte) 1,
      (byte) 138,
      (byte) 196,
      (byte) 43,
      (byte) 73
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 143,
      (byte) 81,
      (byte) 15,
      (byte) 137,
      (byte) 34,
      (byte) 10,
      (byte) 162,
      (byte) 22,
      (byte) 109,
      (byte) 84,
      (byte) 163,
      (byte) 105,
      (byte) 208 /*0xD0*/,
      (byte) 219,
      (byte) 105,
      (byte) 154,
      (byte) 128 /*0x80*/,
      (byte) 27,
      (byte) 39,
      (byte) 238,
      (byte) 229,
      (byte) 112 /*0x70*/,
      (byte) 254,
      (byte) 81,
      (byte) 2,
      (byte) 87,
      (byte) 49,
      (byte) 215,
      (byte) 245,
      (byte) 148,
      (byte) 208 /*0xD0*/,
      (byte) 244,
      (byte) 178,
      (byte) 178,
      (byte) 94,
      (byte) 201,
      (byte) 115,
      (byte) 249,
      (byte) 78,
      (byte) 104,
      (byte) 110,
      (byte) 181,
      (byte) 246,
      (byte) 133,
      (byte) 188,
      (byte) 103,
      (byte) 109,
      (byte) 118,
      (byte) 201,
      (byte) 147,
      (byte) 72,
      (byte) 110,
      (byte) 204,
      (byte) 72,
      (byte) 101
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 185,
      (byte) 239,
      (byte) 21,
      (byte) 169,
      (byte) 79,
      (byte) 168,
      (byte) 158,
      (byte) 254,
      (byte) 236,
      (byte) 98,
      (byte) 194,
      (byte) 21,
      (byte) 18,
      (byte) 88,
      (byte) 127 /*0x7F*/,
      (byte) 215,
      (byte) 97,
      (byte) 73,
      (byte) 116,
      (byte) 116,
      (byte) 7,
      (byte) 33,
      (byte) 180,
      (byte) 21,
      (byte) 6,
      (byte) 154,
      (byte) 118,
      (byte) 238,
      (byte) 41,
      (byte) 87,
      (byte) 189,
      (byte) 19,
      (byte) 210,
      (byte) 71,
      (byte) 207,
      (byte) 150,
      (byte) 123,
      (byte) 30,
      (byte) 159,
      (byte) 76,
      (byte) 217,
      (byte) 185,
      (byte) 99,
      (byte) 4,
      (byte) 111,
      (byte) 209,
      (byte) 194,
      (byte) 87,
      (byte) 114,
      (byte) 56,
      (byte) 80 /*0x50*/,
      (byte) 5,
      (byte) 159,
      (byte) 180,
      (byte) 88
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[40]
    {
      (byte) 190,
      (byte) 134,
      (byte) 203,
      (byte) 216,
      (byte) 202,
      (byte) 32 /*0x20*/,
      (byte) 216,
      (byte) 131,
      (byte) 175,
      (byte) 182,
      (byte) 30,
      (byte) 125,
      (byte) 159,
      (byte) 38,
      (byte) 135,
      (byte) 205,
      (byte) 190,
      (byte) 127 /*0x7F*/,
      (byte) 48 /*0x30*/,
      (byte) 122,
      (byte) 121,
      (byte) 148,
      (byte) 154,
      (byte) 234,
      (byte) 186,
      (byte) 14,
      (byte) 97,
      (byte) 75,
      (byte) 98,
      (byte) 39,
      (byte) 106,
      (byte) 77,
      (byte) 64 /*0x40*/,
      (byte) 119,
      (byte) 181,
      (byte) 114,
      (byte) 131,
      (byte) 143,
      (byte) 164,
      (byte) 149
    };
    byte[] numArray18 = new byte[40]
    {
      (byte) 126,
      (byte) 220,
      (byte) 128 /*0x80*/,
      (byte) 196,
      (byte) 83,
      (byte) 107,
      (byte) 163,
      (byte) 110,
      (byte) 35,
      (byte) 148,
      (byte) 77,
      (byte) 45,
      (byte) 137,
      (byte) 88,
      (byte) 143,
      (byte) 230,
      (byte) 101,
      (byte) 122,
      (byte) 2,
      (byte) 173,
      (byte) 146,
      (byte) 112 /*0x70*/,
      (byte) 146,
      (byte) 1,
      (byte) 140,
      (byte) 122,
      (byte) 89,
      (byte) 60,
      (byte) 169,
      (byte) 169,
      (byte) 69,
      (byte) 15,
      (byte) 201,
      (byte) 220,
      (byte) 179,
      (byte) 82,
      (byte) 194,
      (byte) 143,
      (byte) 159,
      (byte) 72
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 40);
    for (int index = 0; index < 40; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13709()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 78;
      numArray2[1] = (byte) 249;
      numArray2[4] = (byte) 193;
      numArray2[2] = (byte) 41;
      numArray2[3] = (byte) 108;
      numArray2[5] = (byte) 161;
      numArray2[7] = (byte) 200;
      numArray2[9] = (byte) 163;
      numArray2[8] = (byte) 53;
      numArray2[6] = (byte) 13;
      byte[] numArray3 = new byte[10]
      {
        (byte) 104,
        (byte) 76,
        (byte) 79,
        (byte) 101,
        (byte) 38,
        (byte) 170,
        (byte) 51,
        (byte) 136,
        (byte) 150,
        (byte) 118
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 120;
    numArray5[4] = (byte) 47;
    numArray5[1] = (byte) 113;
    numArray5[3] = (byte) 73;
    numArray5[5] = (byte) 219;
    numArray5[0] = (byte) 40;
    numArray5[6] = (byte) 154;
    numArray5[7] = (byte) 93;
    numArray5[9] = (byte) 135;
    numArray5[2] = (byte) 20;
    byte[] numArray6 = new byte[10]
    {
      (byte) 28,
      (byte) 217,
      (byte) 49,
      (byte) 8,
      (byte) 83,
      (byte) 201,
      (byte) 161,
      (byte) 174,
      (byte) 4,
      (byte) 135
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13710()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[85];
      byte[] numArray2 = new byte[55];
      numArray2[15] = (byte) 236;
      numArray2[16 /*0x10*/] = (byte) 154;
      numArray2[36] = (byte) 243;
      numArray2[3] = (byte) 214;
      numArray2[23] = (byte) 214;
      numArray2[24] = (byte) 201;
      numArray2[6] = (byte) 133;
      numArray2[43] = (byte) 3;
      numArray2[22] = (byte) 93;
      numArray2[41] = (byte) 132;
      numArray2[10] = (byte) 1;
      numArray2[14] = (byte) 222;
      numArray2[12] = (byte) 172;
      numArray2[1] = (byte) 113;
      numArray2[25] = (byte) 2;
      numArray2[31 /*0x1F*/] = (byte) 138;
      numArray2[26] = (byte) 97;
      numArray2[42] = (byte) 30;
      numArray2[18] = (byte) 96 /*0x60*/;
      numArray2[19] = (byte) 161;
      numArray2[7] = (byte) 101;
      numArray2[49] = (byte) 182;
      numArray2[2] = (byte) 232;
      numArray2[17] = (byte) 70;
      numArray2[44] = (byte) 228;
      numArray2[45] = (byte) 249;
      numArray2[5] = (byte) 223;
      numArray2[27] = (byte) 109;
      numArray2[35] = (byte) 190;
      numArray2[29] = (byte) 185;
      numArray2[30] = (byte) 159;
      numArray2[50] = (byte) 125;
      numArray2[32 /*0x20*/] = (byte) 228;
      numArray2[33] = (byte) 141;
      numArray2[34] = (byte) 167;
      numArray2[11] = (byte) 251;
      numArray2[4] = (byte) 144 /*0x90*/;
      numArray2[37] = (byte) 196;
      numArray2[38] = (byte) 205;
      numArray2[39] = (byte) 153;
      numArray2[40] = (byte) 2;
      numArray2[54] = (byte) 133;
      numArray2[21] = (byte) 68;
      numArray2[13] = (byte) 87;
      numArray2[20] = (byte) 1;
      numArray2[0] = (byte) 162;
      numArray2[46] = (byte) 221;
      numArray2[47] = (byte) 104;
      numArray2[48 /*0x30*/] = (byte) 59;
      numArray2[28] = (byte) 169;
      numArray2[53] = (byte) 234;
      numArray2[51] = (byte) 86;
      numArray2[52] = (byte) 61;
      numArray2[9] = (byte) 189;
      numArray2[8] = (byte) 125;
      byte[] numArray3 = new byte[55];
      numArray3[18] = (byte) 110;
      numArray3[28] = (byte) 169;
      numArray3[4] = (byte) 246;
      numArray3[3] = (byte) 71;
      numArray3[40] = (byte) 192 /*0xC0*/;
      numArray3[5] = (byte) 196;
      numArray3[43] = (byte) 131;
      numArray3[1] = (byte) 220;
      numArray3[8] = (byte) 237;
      numArray3[9] = (byte) 231;
      numArray3[10] = (byte) 211;
      numArray3[7] = (byte) 83;
      numArray3[45] = byte.MaxValue;
      numArray3[13] = (byte) 95;
      numArray3[16 /*0x10*/] = (byte) 237;
      numArray3[15] = (byte) 137;
      numArray3[48 /*0x30*/] = (byte) 114;
      numArray3[35] = (byte) 174;
      numArray3[23] = (byte) 252;
      numArray3[38] = (byte) 244;
      numArray3[20] = (byte) 206;
      numArray3[21] = (byte) 147;
      numArray3[22] = (byte) 113;
      numArray3[39] = (byte) 104;
      numArray3[0] = (byte) 122;
      numArray3[25] = (byte) 220;
      numArray3[26] = (byte) 221;
      numArray3[27] = (byte) 50;
      numArray3[2] = (byte) 120;
      numArray3[29] = (byte) 9;
      numArray3[30] = (byte) 126;
      numArray3[47] = (byte) 242;
      numArray3[32 /*0x20*/] = (byte) 31 /*0x1F*/;
      numArray3[33] = (byte) 69;
      numArray3[14] = (byte) 25;
      numArray3[53] = (byte) 67;
      numArray3[34] = (byte) 15;
      numArray3[50] = (byte) 249;
      numArray3[42] = (byte) 40;
      numArray3[36] = (byte) 229;
      numArray3[37] = (byte) 169;
      numArray3[41] = (byte) 183;
      numArray3[31 /*0x1F*/] = (byte) 164;
      numArray3[6] = (byte) 94;
      numArray3[44] = (byte) 230;
      numArray3[11] = (byte) 129;
      numArray3[46] = (byte) 253;
      numArray3[17] = (byte) 135;
      numArray3[24] = (byte) 63 /*0x3F*/;
      numArray3[49] = (byte) 102;
      numArray3[19] = (byte) 23;
      numArray3[51] = (byte) 215;
      numArray3[52] = (byte) 244;
      numArray3[12] = (byte) 155;
      numArray3[54] = (byte) 205;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30]
      {
        (byte) 152,
        (byte) 78,
        (byte) 2,
        (byte) 68,
        (byte) 208 /*0xD0*/,
        (byte) 146,
        (byte) 245,
        (byte) 105,
        (byte) 85,
        (byte) 110,
        (byte) 26,
        (byte) 89,
        (byte) 131,
        (byte) 82,
        (byte) 160 /*0xA0*/,
        (byte) 98,
        (byte) 22,
        (byte) 176 /*0xB0*/,
        (byte) 178,
        (byte) 249,
        (byte) 98,
        (byte) 67,
        (byte) 39,
        (byte) 192 /*0xC0*/,
        (byte) 241,
        (byte) 110,
        (byte) 142,
        (byte) 7,
        (byte) 198,
        (byte) 11
      };
      byte[] numArray5 = new byte[30];
      numArray5[15] = (byte) 7;
      numArray5[1] = (byte) 162;
      numArray5[27] = (byte) 205;
      numArray5[26] = (byte) 138;
      numArray5[20] = (byte) 143;
      numArray5[18] = (byte) 38;
      numArray5[11] = (byte) 117;
      numArray5[7] = (byte) 45;
      numArray5[8] = (byte) 57;
      numArray5[9] = (byte) 164;
      numArray5[0] = (byte) 44;
      numArray5[13] = (byte) 114;
      numArray5[29] = (byte) 26;
      numArray5[6] = (byte) 36;
      numArray5[21] = (byte) 38;
      numArray5[2] = (byte) 155;
      numArray5[17] = (byte) 204;
      numArray5[4] = (byte) 80 /*0x50*/;
      numArray5[25] = (byte) 32 /*0x20*/;
      numArray5[19] = (byte) 123;
      numArray5[10] = (byte) 155;
      numArray5[3] = (byte) 237;
      numArray5[22] = (byte) 247;
      numArray5[23] = (byte) 19;
      numArray5[12] = (byte) 112 /*0x70*/;
      numArray5[24] = (byte) 140;
      numArray5[5] = (byte) 145;
      numArray5[16 /*0x10*/] = (byte) 95;
      numArray5[28] = (byte) 67;
      numArray5[14] = (byte) 251;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 30);
      for (int index = 0; index < 30; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[39];
      byte[] response = new byte[39];
      Array.Copy((Array) sc_13686.sspq, 201, (Array) numArray6, 0, 39);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13686.sspr, 201, (Array) numArray6, 0, 39);
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
    byte[] numArray7 = new byte[85];
    byte[] numArray8 = new byte[55]
    {
      (byte) 119,
      (byte) 180,
      (byte) 238,
      (byte) 104,
      (byte) 93,
      (byte) 87,
      (byte) 40,
      (byte) 254,
      (byte) 86,
      (byte) 11,
      (byte) 88,
      (byte) 22,
      (byte) 202,
      (byte) 208 /*0xD0*/,
      (byte) 31 /*0x1F*/,
      (byte) 242,
      (byte) 37,
      (byte) 107,
      (byte) 140,
      (byte) 10,
      (byte) 79,
      (byte) 108,
      (byte) 251,
      (byte) 199,
      (byte) 172,
      (byte) 218,
      (byte) 46,
      (byte) 179,
      (byte) 179,
      (byte) 209,
      (byte) 28,
      (byte) 188,
      (byte) 253,
      (byte) 10,
      (byte) 164,
      (byte) 207,
      (byte) 105,
      (byte) 201,
      (byte) 168,
      (byte) 212,
      (byte) 162,
      (byte) 216,
      (byte) 176 /*0xB0*/,
      (byte) 134,
      (byte) 164,
      (byte) 186,
      (byte) 159,
      (byte) 30,
      (byte) 231,
      (byte) 177,
      (byte) 217,
      (byte) 173,
      (byte) 177,
      (byte) 67,
      (byte) 223
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 46,
      (byte) 48 /*0x30*/,
      (byte) 36,
      (byte) 246,
      (byte) 222,
      (byte) 142,
      (byte) 46,
      (byte) 252,
      (byte) 195,
      (byte) 140,
      (byte) 93,
      (byte) 32 /*0x20*/,
      (byte) 17,
      (byte) 249,
      (byte) 68,
      (byte) 200,
      (byte) 129,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 155,
      (byte) 126,
      (byte) 43,
      (byte) 97,
      (byte) 208 /*0xD0*/,
      (byte) 223,
      (byte) 24,
      (byte) 200,
      (byte) 148,
      (byte) 159,
      (byte) 181,
      (byte) 192 /*0xC0*/,
      (byte) 14,
      (byte) 170,
      (byte) 228,
      (byte) 45,
      (byte) 187,
      (byte) 63 /*0x3F*/,
      (byte) 28,
      (byte) 98,
      (byte) 109,
      (byte) 29,
      (byte) 16 /*0x10*/,
      (byte) 196,
      (byte) 89,
      (byte) 148,
      (byte) 182,
      (byte) 186,
      (byte) 12,
      (byte) 247,
      (byte) 176 /*0xB0*/,
      (byte) 238,
      (byte) 57,
      (byte) 184,
      (byte) 20,
      (byte) 108
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[30]
    {
      (byte) 38,
      (byte) 187,
      (byte) 130,
      (byte) 41,
      (byte) 117,
      (byte) 75,
      (byte) 132,
      (byte) 239,
      (byte) 156,
      (byte) 24,
      (byte) 192 /*0xC0*/,
      (byte) 43,
      (byte) 79,
      (byte) 136,
      (byte) 92,
      (byte) 194,
      (byte) 184,
      (byte) 103,
      (byte) 118,
      (byte) 188,
      (byte) 116,
      (byte) 35,
      (byte) 241,
      (byte) 59,
      (byte) 15,
      (byte) 204,
      (byte) 1,
      (byte) 248,
      (byte) 110,
      (byte) 10
    };
    byte[] numArray11 = new byte[30];
    numArray11[15] = (byte) 227;
    numArray11[8] = (byte) 104;
    numArray11[19] = (byte) 170;
    numArray11[4] = (byte) 64 /*0x40*/;
    numArray11[2] = (byte) 5;
    numArray11[3] = (byte) 106;
    numArray11[20] = (byte) 60;
    numArray11[7] = (byte) 14;
    numArray11[26] = (byte) 73;
    numArray11[16 /*0x10*/] = (byte) 251;
    numArray11[10] = (byte) 194;
    numArray11[6] = (byte) 219;
    numArray11[17] = byte.MaxValue;
    numArray11[13] = (byte) 61;
    numArray11[23] = (byte) 248;
    numArray11[9] = (byte) 200;
    numArray11[5] = (byte) 122;
    numArray11[24] = (byte) 100;
    numArray11[0] = (byte) 204;
    numArray11[27] = (byte) 188;
    numArray11[14] = (byte) 211;
    numArray11[21] = (byte) 217;
    numArray11[22] = (byte) 58;
    numArray11[18] = (byte) 77;
    numArray11[12] = (byte) 206;
    numArray11[11] = (byte) 133;
    numArray11[25] = (byte) 247;
    numArray11[1] = (byte) 166;
    numArray11[28] = (byte) 14;
    numArray11[29] = (byte) 228;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 30);
    for (int index = 0; index < 30; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13711()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 94,
        (byte) 149,
        (byte) 146,
        (byte) 56,
        (byte) 70,
        (byte) 78,
        (byte) 156,
        (byte) 14,
        (byte) 58,
        (byte) 232,
        (byte) 144 /*0x90*/,
        (byte) 221,
        (byte) 27,
        (byte) 79,
        (byte) 92,
        (byte) 29,
        (byte) 52,
        (byte) 217,
        (byte) 122
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 241,
        (byte) 78,
        (byte) 100,
        (byte) 235,
        (byte) 166,
        (byte) 205,
        (byte) 227,
        (byte) 34,
        (byte) 223,
        (byte) 25,
        (byte) 106,
        (byte) 129,
        (byte) 147,
        (byte) 98,
        (byte) 219,
        (byte) 155,
        (byte) 111,
        (byte) 205,
        (byte) 170
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 193,
      (byte) 74,
      (byte) 211,
      (byte) 62,
      (byte) 67,
      (byte) 222,
      (byte) 144 /*0x90*/,
      (byte) 131,
      (byte) 68,
      (byte) 159,
      (byte) 21,
      (byte) 118,
      (byte) 67,
      (byte) 73,
      (byte) 96 /*0x60*/,
      (byte) 95,
      (byte) 225,
      (byte) 47,
      (byte) 78
    };
    byte[] numArray6 = new byte[19];
    numArray6[18] = (byte) 47;
    numArray6[4] = (byte) 172;
    numArray6[17] = byte.MaxValue;
    numArray6[3] = (byte) 14;
    numArray6[6] = (byte) 27;
    numArray6[13] = (byte) 84;
    numArray6[9] = (byte) 125;
    numArray6[7] = (byte) 115;
    numArray6[8] = (byte) 215;
    numArray6[14] = (byte) 193;
    numArray6[10] = (byte) 153;
    numArray6[11] = (byte) 64 /*0x40*/;
    numArray6[12] = (byte) 162;
    numArray6[5] = (byte) 241;
    numArray6[0] = (byte) 98;
    numArray6[15] = (byte) 132;
    numArray6[16 /*0x10*/] = (byte) 31 /*0x1F*/;
    numArray6[1] = (byte) 157;
    numArray6[2] = (byte) 246;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13712()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[144 /*0x90*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 163,
        (byte) 241,
        (byte) 199,
        (byte) 164,
        (byte) 11,
        (byte) 19,
        (byte) 189,
        (byte) 212,
        (byte) 247,
        (byte) 75,
        (byte) 52,
        (byte) 38,
        (byte) 60,
        (byte) 64 /*0x40*/,
        (byte) 140,
        (byte) 109,
        (byte) 86,
        (byte) 201,
        (byte) 243,
        (byte) 148,
        (byte) 14,
        (byte) 212,
        (byte) 141,
        (byte) 23,
        (byte) 246,
        (byte) 195,
        (byte) 117,
        (byte) 25,
        (byte) 250,
        (byte) 218,
        (byte) 36,
        (byte) 144 /*0x90*/,
        (byte) 109,
        (byte) 234,
        (byte) 220,
        (byte) 204,
        (byte) 157,
        (byte) 67,
        (byte) 110,
        (byte) 215,
        (byte) 63 /*0x3F*/,
        (byte) 160 /*0xA0*/,
        (byte) 120,
        (byte) 198,
        (byte) 201,
        (byte) 90,
        (byte) 68,
        (byte) 146,
        (byte) 186,
        (byte) 246,
        (byte) 177,
        (byte) 65,
        (byte) 202,
        (byte) 233,
        (byte) 168
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 122,
        (byte) 180,
        (byte) 182,
        (byte) 218,
        (byte) 40,
        (byte) 17,
        (byte) 251,
        (byte) 35,
        (byte) 176 /*0xB0*/,
        (byte) 22,
        (byte) 202,
        (byte) 154,
        (byte) 72,
        (byte) 33,
        (byte) 124,
        (byte) 151,
        (byte) 226,
        (byte) 69,
        (byte) 14,
        (byte) 46,
        (byte) 208 /*0xD0*/,
        (byte) 102,
        (byte) 175,
        (byte) 245,
        (byte) 202,
        (byte) 118,
        (byte) 213,
        (byte) 133,
        (byte) 195,
        (byte) 29,
        (byte) 189,
        (byte) 226,
        (byte) 176 /*0xB0*/,
        (byte) 52,
        (byte) 27,
        (byte) 79,
        byte.MaxValue,
        (byte) 104,
        (byte) 72,
        (byte) 107,
        (byte) 137,
        (byte) 55,
        (byte) 201,
        (byte) 250,
        (byte) 70,
        (byte) 120,
        (byte) 123,
        (byte) 154,
        (byte) 214,
        (byte) 169,
        (byte) 187,
        (byte) 249,
        (byte) 141,
        (byte) 234,
        (byte) 53
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[53] = (byte) 115;
      numArray4[15] = (byte) 90;
      numArray4[2] = (byte) 72;
      numArray4[3] = (byte) 158;
      numArray4[27] = (byte) 40;
      numArray4[52] = (byte) 58;
      numArray4[6] = (byte) 29;
      numArray4[7] = (byte) 38;
      numArray4[21] = (byte) 78;
      numArray4[1] = (byte) 174;
      numArray4[40] = (byte) 91;
      numArray4[29] = (byte) 197;
      numArray4[12] = (byte) 192 /*0xC0*/;
      numArray4[34] = (byte) 237;
      numArray4[46] = (byte) 249;
      numArray4[47] = (byte) 38;
      numArray4[18] = (byte) 109;
      numArray4[43] = (byte) 162;
      numArray4[33] = (byte) 103;
      numArray4[19] = (byte) 91;
      numArray4[20] = (byte) 198;
      numArray4[35] = (byte) 149;
      numArray4[37] = (byte) 56;
      numArray4[23] = (byte) 127 /*0x7F*/;
      numArray4[24] = (byte) 226;
      numArray4[10] = (byte) 81;
      numArray4[5] = (byte) 218;
      numArray4[51] = (byte) 148;
      numArray4[28] = (byte) 227;
      numArray4[0] = (byte) 176 /*0xB0*/;
      numArray4[30] = (byte) 132;
      numArray4[31 /*0x1F*/] = (byte) 65;
      numArray4[45] = (byte) 46;
      numArray4[16 /*0x10*/] = (byte) 28;
      numArray4[22] = (byte) 155;
      numArray4[11] = (byte) 100;
      numArray4[39] = (byte) 197;
      numArray4[9] = (byte) 184;
      numArray4[38] = (byte) 155;
      numArray4[49] = (byte) 230;
      numArray4[14] = (byte) 18;
      numArray4[41] = (byte) 159;
      numArray4[42] = (byte) 36;
      numArray4[26] = (byte) 3;
      numArray4[44] = (byte) 242;
      numArray4[36] = (byte) 38;
      numArray4[13] = (byte) 208 /*0xD0*/;
      numArray4[25] = (byte) 94;
      numArray4[48 /*0x30*/] = (byte) 159;
      numArray4[17] = (byte) 84;
      numArray4[4] = (byte) 163;
      numArray4[8] = (byte) 164;
      numArray4[32 /*0x20*/] = (byte) 13;
      numArray4[54] = (byte) 157;
      numArray4[50] = (byte) 174;
      byte[] numArray5 = new byte[55]
      {
        (byte) 210,
        (byte) 125,
        (byte) 98,
        (byte) 47,
        (byte) 110,
        (byte) 21,
        (byte) 156,
        (byte) 51,
        (byte) 139,
        (byte) 170,
        (byte) 147,
        (byte) 197,
        (byte) 170,
        (byte) 247,
        (byte) 34,
        (byte) 89,
        (byte) 233,
        (byte) 223,
        (byte) 111,
        (byte) 100,
        (byte) 234,
        (byte) 219,
        (byte) 185,
        (byte) 82,
        (byte) 147,
        (byte) 39,
        (byte) 148,
        (byte) 69,
        (byte) 132,
        (byte) 207,
        (byte) 171,
        (byte) 71,
        (byte) 214,
        (byte) 118,
        (byte) 21,
        (byte) 183,
        (byte) 115,
        (byte) 143,
        (byte) 210,
        (byte) 80 /*0x50*/,
        (byte) 108,
        (byte) 219,
        (byte) 60,
        (byte) 119,
        (byte) 40,
        (byte) 214,
        (byte) 45,
        (byte) 213,
        (byte) 140,
        (byte) 215,
        (byte) 85,
        (byte) 240 /*0xF0*/,
        (byte) 20,
        (byte) 245,
        (byte) 182
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[34];
      numArray6[31 /*0x1F*/] = (byte) 83;
      numArray6[29] = (byte) 116;
      numArray6[26] = (byte) 74;
      numArray6[2] = (byte) 224 /*0xE0*/;
      numArray6[14] = (byte) 103;
      numArray6[5] = (byte) 70;
      numArray6[6] = (byte) 199;
      numArray6[1] = (byte) 8;
      numArray6[22] = (byte) 219;
      numArray6[9] = (byte) 188;
      numArray6[4] = (byte) 107;
      numArray6[11] = (byte) 86;
      numArray6[23] = (byte) 195;
      numArray6[13] = (byte) 214;
      numArray6[20] = (byte) 139;
      numArray6[15] = (byte) 99;
      numArray6[16 /*0x10*/] = (byte) 184;
      numArray6[19] = (byte) 228;
      numArray6[8] = (byte) 8;
      numArray6[33] = (byte) 104;
      numArray6[3] = (byte) 249;
      numArray6[21] = (byte) 170;
      numArray6[12] = (byte) 250;
      numArray6[7] = (byte) 68;
      numArray6[10] = (byte) 142;
      numArray6[25] = (byte) 103;
      numArray6[18] = (byte) 202;
      numArray6[0] = (byte) 60;
      numArray6[24] = (byte) 152;
      numArray6[27] = (byte) 77;
      numArray6[17] = (byte) 153;
      numArray6[30] = (byte) 38;
      numArray6[32 /*0x20*/] = (byte) 83;
      numArray6[28] = (byte) 20;
      byte[] numArray7 = new byte[34]
      {
        (byte) 214,
        (byte) 177,
        (byte) 55,
        (byte) 246,
        (byte) 249,
        (byte) 234,
        (byte) 58,
        (byte) 51,
        (byte) 36,
        (byte) 145,
        (byte) 160 /*0xA0*/,
        (byte) 14,
        (byte) 174,
        (byte) 14,
        (byte) 252,
        (byte) 33,
        (byte) 222,
        (byte) 68,
        (byte) 63 /*0x3F*/,
        (byte) 121,
        (byte) 15,
        (byte) 171,
        (byte) 198,
        (byte) 116,
        (byte) 181,
        (byte) 220,
        (byte) 219,
        (byte) 40,
        (byte) 64 /*0x40*/,
        (byte) 22,
        (byte) 122,
        (byte) 253,
        (byte) 100,
        (byte) 170
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[144 /*0x90*/];
    byte[] numArray9 = new byte[55]
    {
      (byte) 194,
      (byte) 157,
      (byte) 181,
      (byte) 102,
      (byte) 97,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 177,
      (byte) 171,
      (byte) 63 /*0x3F*/,
      (byte) 110,
      (byte) 233,
      (byte) 156,
      (byte) 153,
      (byte) 76,
      (byte) 167,
      byte.MaxValue,
      (byte) 218,
      (byte) 142,
      (byte) 70,
      (byte) 89,
      (byte) 149,
      (byte) 247,
      (byte) 25,
      (byte) 3,
      (byte) 250,
      (byte) 69,
      (byte) 133,
      (byte) 214,
      (byte) 233,
      (byte) 149,
      (byte) 37,
      (byte) 112 /*0x70*/,
      (byte) 138,
      (byte) 23,
      (byte) 59,
      (byte) 246,
      (byte) 237,
      (byte) 44,
      (byte) 194,
      (byte) 110,
      (byte) 106,
      (byte) 181,
      (byte) 196,
      (byte) 142,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 250,
      (byte) 73,
      (byte) 148,
      (byte) 4,
      (byte) 183,
      (byte) 39,
      (byte) 173,
      (byte) 147
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 201,
      (byte) 45,
      (byte) 4,
      (byte) 58,
      (byte) 12,
      (byte) 139,
      (byte) 168,
      (byte) 236,
      (byte) 27,
      (byte) 45,
      (byte) 23,
      (byte) 84,
      (byte) 55,
      (byte) 190,
      (byte) 199,
      (byte) 169,
      (byte) 183,
      (byte) 72,
      (byte) 198,
      (byte) 113,
      (byte) 20,
      (byte) 191,
      (byte) 204,
      (byte) 77,
      (byte) 149,
      (byte) 95,
      (byte) 220,
      (byte) 196,
      (byte) 178,
      (byte) 6,
      (byte) 72,
      (byte) 166,
      (byte) 34,
      (byte) 185,
      (byte) 52,
      (byte) 218,
      (byte) 201,
      (byte) 26,
      (byte) 203,
      (byte) 77,
      (byte) 179,
      (byte) 202,
      (byte) 56,
      (byte) 242,
      (byte) 35,
      (byte) 253,
      (byte) 37,
      (byte) 86,
      (byte) 9,
      (byte) 74,
      (byte) 120,
      (byte) 82,
      (byte) 64 /*0x40*/,
      (byte) 195,
      (byte) 235
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 156,
      (byte) 98,
      (byte) 103,
      (byte) 138,
      (byte) 58,
      (byte) 219,
      (byte) 193,
      (byte) 169,
      (byte) 72,
      (byte) 121,
      (byte) 17,
      (byte) 130,
      (byte) 16 /*0x10*/,
      (byte) 176 /*0xB0*/,
      (byte) 17,
      (byte) 173,
      (byte) 204,
      (byte) 216,
      (byte) 238,
      (byte) 224 /*0xE0*/,
      (byte) 50,
      (byte) 138,
      (byte) 114,
      (byte) 102,
      (byte) 75,
      (byte) 60,
      (byte) 66,
      (byte) 229,
      (byte) 222,
      (byte) 254,
      (byte) 195,
      (byte) 246,
      (byte) 234,
      (byte) 149,
      (byte) 90,
      (byte) 138,
      (byte) 197,
      (byte) 55,
      (byte) 51,
      (byte) 143,
      (byte) 243,
      (byte) 27,
      (byte) 69,
      (byte) 22,
      (byte) 129,
      (byte) 120,
      (byte) 56,
      (byte) 227,
      (byte) 242,
      (byte) 9,
      (byte) 91,
      (byte) 213,
      (byte) 164,
      (byte) 76,
      (byte) 116
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 106,
      (byte) 249,
      (byte) 229,
      (byte) 201,
      (byte) 38,
      (byte) 70,
      (byte) 92,
      (byte) 96 /*0x60*/,
      (byte) 253,
      (byte) 71,
      (byte) 40,
      (byte) 161,
      (byte) 112 /*0x70*/,
      (byte) 119,
      (byte) 215,
      (byte) 241,
      (byte) 126,
      (byte) 164,
      (byte) 189,
      (byte) 115,
      (byte) 39,
      (byte) 232,
      (byte) 144 /*0x90*/,
      (byte) 133,
      (byte) 83,
      (byte) 35,
      (byte) 230,
      (byte) 126,
      (byte) 240 /*0xF0*/,
      (byte) 176 /*0xB0*/,
      (byte) 233,
      (byte) 154,
      (byte) 204,
      (byte) 121,
      (byte) 230,
      (byte) 12,
      (byte) 158,
      (byte) 9,
      (byte) 40,
      (byte) 139,
      (byte) 190,
      (byte) 178,
      (byte) 72,
      (byte) 71,
      (byte) 121,
      (byte) 182,
      (byte) 222,
      (byte) 103,
      (byte) 192 /*0xC0*/,
      (byte) 186,
      (byte) 51,
      (byte) 147,
      (byte) 125,
      (byte) 3,
      (byte) 109
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[34];
    numArray13[5] = (byte) 202;
    numArray13[9] = (byte) 151;
    numArray13[25] = (byte) 243;
    numArray13[3] = (byte) 153;
    numArray13[32 /*0x20*/] = (byte) 73;
    numArray13[33] = (byte) 154;
    numArray13[6] = (byte) 74;
    numArray13[28] = (byte) 238;
    numArray13[26] = (byte) 213;
    numArray13[23] = (byte) 3;
    numArray13[10] = (byte) 123;
    numArray13[20] = (byte) 91;
    numArray13[12] = (byte) 134;
    numArray13[13] = (byte) 232;
    numArray13[14] = (byte) 148;
    numArray13[22] = (byte) 210;
    numArray13[16 /*0x10*/] = (byte) 95;
    numArray13[15] = (byte) 164;
    numArray13[18] = (byte) 23;
    numArray13[17] = (byte) 237;
    numArray13[2] = (byte) 254;
    numArray13[21] = (byte) 228;
    numArray13[31 /*0x1F*/] = (byte) 33;
    numArray13[0] = (byte) 0;
    numArray13[1] = (byte) 63 /*0x3F*/;
    numArray13[8] = (byte) 26;
    numArray13[7] = (byte) 182;
    numArray13[27] = (byte) 162;
    numArray13[19] = (byte) 69;
    numArray13[29] = (byte) 81;
    numArray13[30] = (byte) 157;
    numArray13[11] = (byte) 85;
    numArray13[24] = (byte) 94;
    numArray13[4] = (byte) 220;
    byte[] numArray14 = new byte[34]
    {
      (byte) 243,
      (byte) 247,
      (byte) 175,
      (byte) 236,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 93,
      (byte) 46,
      (byte) 235,
      (byte) 241,
      (byte) 128 /*0x80*/,
      (byte) 123,
      (byte) 161,
      (byte) 127 /*0x7F*/,
      (byte) 161,
      (byte) 199,
      (byte) 239,
      (byte) 63 /*0x3F*/,
      (byte) 61,
      (byte) 127 /*0x7F*/,
      (byte) 228,
      (byte) 68,
      (byte) 180,
      (byte) 182,
      (byte) 110,
      (byte) 34,
      (byte) 44,
      (byte) 71,
      (byte) 119,
      (byte) 78,
      (byte) 13,
      (byte) 101,
      (byte) 223,
      (byte) 11
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 34);
    for (int index = 0; index < 34; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static int ssp_appserver_13713(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 198,
      (byte) 74,
      (byte) 251,
      (byte) 125,
      (byte) 241,
      (byte) 70,
      (byte) 208 /*0xD0*/,
      (byte) 135,
      (byte) 180,
      (byte) 219,
      (byte) 189,
      (byte) 12,
      (byte) 61,
      (byte) 196,
      (byte) 125,
      (byte) 6,
      (byte) 168,
      (byte) 140,
      (byte) 58,
      (byte) 187,
      (byte) 45,
      (byte) 91,
      (byte) 82,
      (byte) 151,
      (byte) 157,
      (byte) 231,
      (byte) 99,
      (byte) 130,
      (byte) 127 /*0x7F*/,
      (byte) 253,
      (byte) 43,
      (byte) 193,
      (byte) 219,
      (byte) 137,
      (byte) 150,
      (byte) 128 /*0x80*/,
      (byte) 153,
      (byte) 158,
      (byte) 122,
      (byte) 180,
      (byte) 106,
      (byte) 97,
      (byte) 65,
      (byte) 204,
      (byte) 11,
      (byte) 188,
      (byte) 187,
      (byte) 149
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 50;
    sourceArray2[1] = (byte) 227;
    sourceArray2[22] = (byte) 9;
    sourceArray2[3] = (byte) 24;
    sourceArray2[4] = (byte) 225;
    sourceArray2[36] = (byte) 27;
    sourceArray2[6] = (byte) 191;
    sourceArray2[40] = (byte) 145;
    sourceArray2[0] = (byte) 48 /*0x30*/;
    sourceArray2[17] = (byte) 171;
    sourceArray2[43] = (byte) 71;
    sourceArray2[21] = (byte) 53;
    sourceArray2[12] = (byte) 73;
    sourceArray2[13] = (byte) 223;
    sourceArray2[45] = (byte) 235;
    sourceArray2[30] = (byte) 45;
    sourceArray2[16 /*0x10*/] = (byte) 144 /*0x90*/;
    sourceArray2[8] = (byte) 43;
    sourceArray2[27] = (byte) 194;
    sourceArray2[19] = (byte) 88;
    sourceArray2[32 /*0x20*/] = (byte) 212;
    sourceArray2[34] = (byte) 229;
    sourceArray2[31 /*0x1F*/] = (byte) 25;
    sourceArray2[7] = (byte) 174;
    sourceArray2[23] = (byte) 101;
    sourceArray2[25] = (byte) 213;
    sourceArray2[24] = (byte) 220;
    sourceArray2[14] = (byte) 185;
    sourceArray2[28] = (byte) 176 /*0xB0*/;
    sourceArray2[29] = (byte) 231;
    sourceArray2[10] = (byte) 161;
    sourceArray2[18] = (byte) 75;
    sourceArray2[15] = (byte) 79;
    sourceArray2[39] = (byte) 141;
    sourceArray2[46] = (byte) 221;
    sourceArray2[35] = (byte) 19;
    sourceArray2[2] = (byte) 44;
    sourceArray2[37] = (byte) 190;
    sourceArray2[38] = (byte) 174;
    sourceArray2[26] = (byte) 133;
    sourceArray2[41] = (byte) 227;
    sourceArray2[33] = (byte) 179;
    sourceArray2[42] = (byte) 21;
    sourceArray2[9] = (byte) 124;
    sourceArray2[44] = (byte) 248;
    sourceArray2[5] = (byte) 137;
    sourceArray2[11] = (byte) 110;
    sourceArray2[47] = (byte) 80 /*0x50*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13714(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[9] = (byte) 81;
    sourceArray1[1] = (byte) 104;
    sourceArray1[24] = (byte) 55;
    sourceArray1[5] = (byte) 254;
    sourceArray1[8] = (byte) 221;
    sourceArray1[26] = (byte) 160 /*0xA0*/;
    sourceArray1[6] = (byte) 174;
    sourceArray1[10] = (byte) 113;
    sourceArray1[41] = (byte) 209;
    sourceArray1[44] = (byte) 22;
    sourceArray1[45] = (byte) 94;
    sourceArray1[11] = (byte) 215;
    sourceArray1[2] = (byte) 145;
    sourceArray1[13] = (byte) 222;
    sourceArray1[14] = (byte) 21;
    sourceArray1[35] = (byte) 101;
    sourceArray1[7] = (byte) 93;
    sourceArray1[29] = (byte) 113;
    sourceArray1[18] = (byte) 148;
    sourceArray1[19] = (byte) 111;
    sourceArray1[20] = (byte) 2;
    sourceArray1[21] = (byte) 211;
    sourceArray1[22] = (byte) 2;
    sourceArray1[23] = (byte) 116;
    sourceArray1[0] = (byte) 7;
    sourceArray1[25] = (byte) 75;
    sourceArray1[3] = (byte) 120;
    sourceArray1[27] = (byte) 180;
    sourceArray1[28] = (byte) 52;
    sourceArray1[40] = (byte) 222;
    sourceArray1[30] = (byte) 142;
    sourceArray1[43] = (byte) 217;
    sourceArray1[47] = (byte) 145;
    sourceArray1[33] = (byte) 85;
    sourceArray1[34] = (byte) 36;
    sourceArray1[32 /*0x20*/] = (byte) 125;
    sourceArray1[36] = (byte) 98;
    sourceArray1[37] = (byte) 33;
    sourceArray1[38] = (byte) 17;
    sourceArray1[39] = (byte) 29;
    sourceArray1[16 /*0x10*/] = (byte) 109;
    sourceArray1[4] = (byte) 113;
    sourceArray1[15] = (byte) 90;
    sourceArray1[12] = (byte) 254;
    sourceArray1[17] = (byte) 51;
    sourceArray1[42] = (byte) 3;
    sourceArray1[46] = (byte) 192 /*0xC0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 106;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[8] = (byte) 163;
    sourceArray2[31 /*0x1F*/] = (byte) 202;
    sourceArray2[37] = (byte) 239;
    sourceArray2[3] = (byte) 99;
    sourceArray2[4] = (byte) 124;
    sourceArray2[14] = (byte) 67;
    sourceArray2[42] = (byte) 72;
    sourceArray2[28] = (byte) 107;
    sourceArray2[0] = (byte) 148;
    sourceArray2[13] = (byte) 248;
    sourceArray2[40] = (byte) 201;
    sourceArray2[7] = (byte) 248;
    sourceArray2[12] = (byte) 51;
    sourceArray2[46] = (byte) 184;
    sourceArray2[1] = (byte) 121;
    sourceArray2[15] = (byte) 50;
    sourceArray2[16 /*0x10*/] = (byte) 110;
    sourceArray2[17] = (byte) 128 /*0x80*/;
    sourceArray2[18] = (byte) 125;
    sourceArray2[19] = (byte) 29;
    sourceArray2[20] = (byte) 129;
    sourceArray2[21] = (byte) 166;
    sourceArray2[38] = (byte) 230;
    sourceArray2[23] = (byte) 86;
    sourceArray2[11] = (byte) 166;
    sourceArray2[9] = (byte) 184;
    sourceArray2[26] = (byte) 234;
    sourceArray2[10] = (byte) 162;
    sourceArray2[43] = (byte) 118;
    sourceArray2[25] = (byte) 11;
    sourceArray2[44] = (byte) 146;
    sourceArray2[6] = (byte) 38;
    sourceArray2[32 /*0x20*/] = (byte) 231;
    sourceArray2[33] = (byte) 251;
    sourceArray2[24] = (byte) 54;
    sourceArray2[5] = (byte) 110;
    sourceArray2[36] = (byte) 198;
    sourceArray2[35] = (byte) 22;
    sourceArray2[27] = (byte) 141;
    sourceArray2[39] = (byte) 16 /*0x10*/;
    sourceArray2[22] = (byte) 97;
    sourceArray2[41] = (byte) 108;
    sourceArray2[2] = (byte) 139;
    sourceArray2[30] = (byte) 168;
    sourceArray2[47] = (byte) 200;
    sourceArray2[45] = (byte) 224 /*0xE0*/;
    sourceArray2[29] = (byte) 70;
    sourceArray2[34] = (byte) 107;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13715()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 80 /*0x50*/,
        (byte) 114,
        (byte) 226,
        (byte) 240 /*0xF0*/,
        (byte) 176 /*0xB0*/,
        (byte) 138,
        (byte) 232,
        (byte) 135,
        (byte) 57,
        (byte) 13
      };
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 62;
      numArray3[7] = (byte) 19;
      numArray3[2] = (byte) 165;
      numArray3[6] = (byte) 42;
      numArray3[4] = (byte) 139;
      numArray3[0] = (byte) 194;
      numArray3[9] = (byte) 54;
      numArray3[5] = (byte) 135;
      numArray3[8] = (byte) 201;
      numArray3[3] = (byte) 145;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[6] = (byte) 105;
    numArray5[4] = (byte) 86;
    numArray5[2] = (byte) 4;
    numArray5[1] = (byte) 16 /*0x10*/;
    numArray5[7] = (byte) 59;
    numArray5[3] = (byte) 137;
    numArray5[0] = (byte) 19;
    numArray5[5] = (byte) 174;
    numArray5[8] = (byte) 238;
    numArray5[9] = (byte) 42;
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 7;
    numArray6[1] = (byte) 76;
    numArray6[6] = (byte) 181;
    numArray6[9] = (byte) 210;
    numArray6[7] = (byte) 11;
    numArray6[4] = (byte) 74;
    numArray6[5] = (byte) 157;
    numArray6[2] = (byte) 5;
    numArray6[8] = (byte) 186;
    numArray6[0] = (byte) 222;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13716()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 206;
      numArray2[4] = (byte) 146;
      numArray2[1] = (byte) 224 /*0xE0*/;
      numArray2[9] = (byte) 239;
      numArray2[8] = (byte) 109;
      numArray2[5] = (byte) 216;
      numArray2[6] = (byte) 208 /*0xD0*/;
      numArray2[3] = (byte) 1;
      numArray2[7] = (byte) 209;
      numArray2[2] = (byte) 171;
      byte[] numArray3 = new byte[10];
      numArray3[4] = (byte) 231;
      numArray3[9] = (byte) 57;
      numArray3[6] = (byte) 27;
      numArray3[7] = (byte) 1;
      numArray3[3] = (byte) 59;
      numArray3[5] = (byte) 171;
      numArray3[0] = (byte) 69;
      numArray3[2] = (byte) 184;
      numArray3[8] = (byte) 128 /*0x80*/;
      numArray3[1] = (byte) 129;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46];
      byte[] response = new byte[46];
      Array.Copy((Array) sc_13686.sspq, 240 /*0xF0*/, (Array) numArray4, 0, 46);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13686.sspr, 240 /*0xF0*/, (Array) numArray4, 0, 46);
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
      (byte) 134,
      (byte) 109,
      (byte) 87,
      (byte) 25,
      (byte) 102,
      (byte) 3,
      (byte) 98,
      (byte) 129,
      (byte) 119,
      (byte) 174
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 1,
      (byte) 146,
      (byte) 117,
      (byte) 175,
      (byte) 200,
      (byte) 52,
      (byte) 208 /*0xD0*/,
      (byte) 186,
      (byte) 78,
      (byte) 239
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13717()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 224 /*0xE0*/,
        (byte) 94,
        (byte) 96 /*0x60*/,
        (byte) 15,
        (byte) 192 /*0xC0*/,
        (byte) 41,
        (byte) 151,
        (byte) 42,
        (byte) 57,
        (byte) 238,
        (byte) 247,
        (byte) 209,
        (byte) 216
      };
      byte[] numArray3 = new byte[13]
      {
        (byte) 12,
        (byte) 248,
        (byte) 115,
        (byte) 201,
        (byte) 208 /*0xD0*/,
        (byte) 249,
        (byte) 117,
        (byte) 112 /*0x70*/,
        (byte) 148,
        (byte) 112 /*0x70*/,
        (byte) 173,
        (byte) 51,
        (byte) 120
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13];
    numArray5[4] = (byte) 41;
    numArray5[3] = (byte) 233;
    numArray5[0] = (byte) 239;
    numArray5[2] = (byte) 98;
    numArray5[1] = (byte) 118;
    numArray5[5] = (byte) 40;
    numArray5[12] = (byte) 185;
    numArray5[7] = (byte) 222;
    numArray5[9] = (byte) 217;
    numArray5[8] = (byte) 174;
    numArray5[10] = (byte) 218;
    numArray5[6] = (byte) 180;
    numArray5[11] = (byte) 214;
    byte[] numArray6 = new byte[13];
    numArray6[0] = (byte) 202;
    numArray6[1] = (byte) 232;
    numArray6[6] = byte.MaxValue;
    numArray6[11] = (byte) 199;
    numArray6[3] = (byte) 207;
    numArray6[5] = (byte) 27;
    numArray6[2] = (byte) 148;
    numArray6[7] = (byte) 218;
    numArray6[8] = (byte) 230;
    numArray6[4] = (byte) 155;
    numArray6[10] = (byte) 225;
    numArray6[12] = (byte) 0;
    numArray6[9] = (byte) 243;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13718()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 202;
      numArray2[2] = (byte) 192 /*0xC0*/;
      numArray2[9] = (byte) 237;
      numArray2[3] = (byte) 113;
      numArray2[0] = (byte) 230;
      numArray2[1] = (byte) 78;
      numArray2[8] = (byte) 198;
      numArray2[7] = (byte) 184;
      numArray2[6] = (byte) 184;
      numArray2[4] = (byte) 77;
      byte[] numArray3 = new byte[10]
      {
        (byte) 184,
        (byte) 213,
        (byte) 78,
        (byte) 34,
        (byte) 77,
        (byte) 138,
        (byte) 155,
        (byte) 33,
        (byte) 204,
        (byte) 48 /*0x30*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_13686.sspq, 286, (Array) numArray4, 0, 49);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13686.sspr, 286, (Array) numArray4, 0, 49);
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
      (byte) 172,
      (byte) 82,
      (byte) 59,
      (byte) 74,
      (byte) 182,
      (byte) 219,
      (byte) 51,
      (byte) 16 /*0x10*/,
      (byte) 171,
      (byte) 140
    };
    byte[] numArray7 = new byte[10];
    numArray7[4] = (byte) 92;
    numArray7[0] = (byte) 24;
    numArray7[1] = byte.MaxValue;
    numArray7[2] = (byte) 8;
    numArray7[7] = (byte) 62;
    numArray7[5] = (byte) 9;
    numArray7[8] = (byte) 193;
    numArray7[3] = (byte) 39;
    numArray7[6] = (byte) 25;
    numArray7[9] = (byte) 10;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13719()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 181,
        (byte) 224 /*0xE0*/,
        (byte) 24,
        (byte) 239,
        (byte) 102,
        (byte) 131,
        (byte) 108,
        (byte) 129,
        (byte) 73,
        (byte) 180
      };
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 213;
      numArray3[1] = (byte) 146;
      numArray3[2] = (byte) 33;
      numArray3[0] = (byte) 172;
      numArray3[3] = (byte) 27;
      numArray3[7] = (byte) 148;
      numArray3[4] = (byte) 236;
      numArray3[6] = (byte) 197;
      numArray3[5] = (byte) 218;
      numArray3[9] = (byte) 165;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      byte.MaxValue,
      (byte) 19,
      (byte) 19,
      (byte) 9,
      (byte) 162,
      (byte) 37,
      (byte) 74,
      (byte) 71,
      (byte) 153,
      (byte) 135
    };
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 187;
    numArray6[1] = (byte) 83;
    numArray6[2] = (byte) 83;
    numArray6[3] = (byte) 69;
    numArray6[5] = (byte) 127 /*0x7F*/;
    numArray6[7] = (byte) 87;
    numArray6[8] = (byte) 50;
    numArray6[6] = (byte) 222;
    numArray6[0] = (byte) 122;
    numArray6[4] = (byte) 47;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13720()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 183;
      numArray2[8] = (byte) 242;
      numArray2[0] = (byte) 25;
      numArray2[2] = (byte) 180;
      numArray2[6] = (byte) 184;
      numArray2[5] = (byte) 52;
      numArray2[3] = (byte) 123;
      numArray2[7] = (byte) 42;
      numArray2[9] = (byte) 150;
      numArray2[1] = (byte) 55;
      byte[] numArray3 = new byte[10];
      numArray3[1] = (byte) 127 /*0x7F*/;
      numArray3[5] = (byte) 249;
      numArray3[2] = (byte) 50;
      numArray3[3] = (byte) 102;
      numArray3[8] = (byte) 203;
      numArray3[7] = (byte) 113;
      numArray3[9] = (byte) 53;
      numArray3[4] = (byte) 97;
      numArray3[6] = (byte) 163;
      numArray3[0] = (byte) 196;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 126,
      (byte) 239,
      (byte) 106,
      (byte) 113,
      (byte) 206,
      (byte) 144 /*0x90*/,
      (byte) 101,
      (byte) 207,
      (byte) 241,
      (byte) 161
    };
    byte[] numArray6 = new byte[10];
    numArray6[7] = (byte) 219;
    numArray6[9] = (byte) 32 /*0x20*/;
    numArray6[2] = (byte) 57;
    numArray6[3] = (byte) 217;
    numArray6[0] = (byte) 114;
    numArray6[8] = (byte) 114;
    numArray6[6] = (byte) 19;
    numArray6[1] = (byte) 195;
    numArray6[5] = (byte) 22;
    numArray6[4] = (byte) 103;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[40];
    byte[] response = new byte[40];
    Array.Copy((Array) sc_13686.sspq, 335, (Array) numArray7, 0, 40);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13686.sspr, 335, (Array) numArray7, 0, 40);
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

  internal static string ssp_appserver_13721()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55];
      numArray2[17] = (byte) 117;
      numArray2[1] = (byte) 37;
      numArray2[2] = (byte) 44;
      numArray2[18] = (byte) 42;
      numArray2[20] = (byte) 84;
      numArray2[47] = (byte) 12;
      numArray2[40] = (byte) 189;
      numArray2[51] = (byte) 114;
      numArray2[32 /*0x20*/] = (byte) 139;
      numArray2[28] = (byte) 4;
      numArray2[10] = (byte) 107;
      numArray2[11] = (byte) 164;
      numArray2[3] = (byte) 132;
      numArray2[13] = (byte) 150;
      numArray2[14] = (byte) 54;
      numArray2[15] = (byte) 51;
      numArray2[16 /*0x10*/] = (byte) 70;
      numArray2[50] = (byte) 245;
      numArray2[23] = (byte) 39;
      numArray2[6] = (byte) 2;
      numArray2[48 /*0x30*/] = (byte) 153;
      numArray2[21] = (byte) 199;
      numArray2[22] = (byte) 61;
      numArray2[38] = (byte) 227;
      numArray2[24] = (byte) 249;
      numArray2[44] = (byte) 169;
      numArray2[26] = (byte) 187;
      numArray2[27] = (byte) 26;
      numArray2[34] = (byte) 249;
      numArray2[41] = (byte) 10;
      numArray2[4] = (byte) 36;
      numArray2[31 /*0x1F*/] = (byte) 20;
      numArray2[7] = (byte) 114;
      numArray2[33] = (byte) 231;
      numArray2[9] = (byte) 156;
      numArray2[35] = (byte) 69;
      numArray2[36] = (byte) 66;
      numArray2[37] = (byte) 135;
      numArray2[5] = (byte) 247;
      numArray2[39] = (byte) 163;
      numArray2[25] = (byte) 91;
      numArray2[46] = (byte) 36;
      numArray2[43] = (byte) 169;
      numArray2[42] = (byte) 206;
      numArray2[29] = (byte) 160 /*0xA0*/;
      numArray2[45] = (byte) 53;
      numArray2[53] = (byte) 181;
      numArray2[49] = (byte) 248;
      numArray2[52] = (byte) 221;
      numArray2[8] = (byte) 113;
      numArray2[12] = (byte) 9;
      numArray2[19] = (byte) 190;
      numArray2[0] = (byte) 55;
      numArray2[30] = (byte) 203;
      numArray2[54] = (byte) 123;
      byte[] numArray3 = new byte[55]
      {
        (byte) 88,
        (byte) 80 /*0x50*/,
        (byte) 19,
        (byte) 225,
        (byte) 50,
        (byte) 55,
        (byte) 155,
        (byte) 46,
        (byte) 238,
        (byte) 107,
        (byte) 206,
        (byte) 71,
        (byte) 249,
        (byte) 44,
        (byte) 133,
        (byte) 219,
        (byte) 4,
        (byte) 4,
        (byte) 52,
        (byte) 114,
        (byte) 94,
        (byte) 232,
        (byte) 29,
        (byte) 12,
        (byte) 238,
        (byte) 25,
        (byte) 184,
        (byte) 88,
        (byte) 252,
        (byte) 141,
        (byte) 180,
        (byte) 193,
        (byte) 143,
        (byte) 172,
        (byte) 108,
        (byte) 132,
        (byte) 113,
        (byte) 161,
        (byte) 223,
        (byte) 154,
        (byte) 41,
        (byte) 1,
        (byte) 70,
        (byte) 111,
        (byte) 66,
        (byte) 137,
        (byte) 247,
        (byte) 205,
        (byte) 176 /*0xB0*/,
        (byte) 98,
        (byte) 120,
        (byte) 45,
        (byte) 102,
        (byte) 60,
        (byte) 121
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15];
      numArray4[8] = (byte) 31 /*0x1F*/;
      numArray4[4] = (byte) 102;
      numArray4[2] = (byte) 146;
      numArray4[11] = (byte) 19;
      numArray4[10] = (byte) 182;
      numArray4[1] = (byte) 198;
      numArray4[5] = (byte) 99;
      numArray4[6] = (byte) 39;
      numArray4[12] = (byte) 147;
      numArray4[9] = (byte) 68;
      numArray4[0] = (byte) 135;
      numArray4[13] = (byte) 179;
      numArray4[7] = (byte) 119;
      numArray4[3] = (byte) 140;
      numArray4[14] = (byte) 193;
      byte[] numArray5 = new byte[15]
      {
        (byte) 100,
        (byte) 173,
        (byte) 235,
        (byte) 11,
        (byte) 170,
        (byte) 180,
        (byte) 67,
        (byte) 52,
        (byte) 63 /*0x3F*/,
        (byte) 228,
        (byte) 103,
        (byte) 241,
        (byte) 124,
        (byte) 46,
        (byte) 60
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55]
    {
      (byte) 10,
      (byte) 181,
      (byte) 54,
      (byte) 135,
      (byte) 219,
      (byte) 110,
      (byte) 56,
      (byte) 213,
      (byte) 96 /*0x60*/,
      (byte) 51,
      (byte) 215,
      (byte) 75,
      (byte) 90,
      (byte) 20,
      (byte) 85,
      (byte) 117,
      (byte) 104,
      (byte) 55,
      (byte) 136,
      (byte) 131,
      (byte) 123,
      (byte) 152,
      (byte) 93,
      (byte) 30,
      (byte) 93,
      (byte) 75,
      (byte) 141,
      (byte) 96 /*0x60*/,
      (byte) 180,
      byte.MaxValue,
      (byte) 63 /*0x3F*/,
      (byte) 34,
      (byte) 123,
      (byte) 1,
      (byte) 136,
      (byte) 253,
      (byte) 139,
      (byte) 240 /*0xF0*/,
      (byte) 67,
      (byte) 189,
      (byte) 117,
      (byte) 37,
      (byte) 90,
      (byte) 247,
      (byte) 209,
      (byte) 233,
      (byte) 229,
      (byte) 196,
      (byte) 173,
      (byte) 254,
      (byte) 15,
      (byte) 168,
      (byte) 152,
      (byte) 183,
      (byte) 151
    };
    byte[] numArray8 = new byte[55];
    numArray8[2] = (byte) 145;
    numArray8[1] = (byte) 142;
    numArray8[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray8[19] = (byte) 192 /*0xC0*/;
    numArray8[4] = (byte) 247;
    numArray8[5] = (byte) 164;
    numArray8[6] = (byte) 59;
    numArray8[43] = (byte) 69;
    numArray8[28] = (byte) 161;
    numArray8[26] = (byte) 13;
    numArray8[10] = (byte) 216;
    numArray8[3] = (byte) 176 /*0xB0*/;
    numArray8[12] = (byte) 169;
    numArray8[17] = (byte) 16 /*0x10*/;
    numArray8[41] = (byte) 98;
    numArray8[32 /*0x20*/] = (byte) 156;
    numArray8[33] = (byte) 134;
    numArray8[9] = (byte) 215;
    numArray8[18] = (byte) 185;
    numArray8[8] = (byte) 12;
    numArray8[20] = (byte) 161;
    numArray8[21] = (byte) 122;
    numArray8[22] = (byte) 166;
    numArray8[54] = (byte) 73;
    numArray8[29] = (byte) 148;
    numArray8[15] = (byte) 161;
    numArray8[44] = (byte) 30;
    numArray8[25] = (byte) 250;
    numArray8[31 /*0x1F*/] = (byte) 216;
    numArray8[0] = (byte) 86;
    numArray8[30] = (byte) 240 /*0xF0*/;
    numArray8[27] = (byte) 122;
    numArray8[23] = (byte) 184;
    numArray8[47] = (byte) 220;
    numArray8[34] = (byte) 10;
    numArray8[13] = (byte) 186;
    numArray8[42] = (byte) 44;
    numArray8[37] = (byte) 120;
    numArray8[38] = (byte) 112 /*0x70*/;
    numArray8[24] = (byte) 128 /*0x80*/;
    numArray8[40] = (byte) 7;
    numArray8[39] = (byte) 28;
    numArray8[11] = (byte) 116;
    numArray8[36] = (byte) 8;
    numArray8[35] = (byte) 10;
    numArray8[14] = (byte) 166;
    numArray8[46] = (byte) 151;
    numArray8[7] = (byte) 66;
    numArray8[48 /*0x30*/] = (byte) 131;
    numArray8[49] = (byte) 237;
    numArray8[50] = (byte) 212;
    numArray8[51] = (byte) 47;
    numArray8[52] = (byte) 8;
    numArray8[53] = (byte) 80 /*0x50*/;
    numArray8[45] = (byte) 161;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15]
    {
      (byte) 202,
      (byte) 181,
      (byte) 41,
      (byte) 24,
      (byte) 35,
      (byte) 107,
      (byte) 112 /*0x70*/,
      (byte) 93,
      (byte) 42,
      (byte) 12,
      (byte) 239,
      (byte) 117,
      (byte) 179,
      (byte) 118,
      (byte) 177
    };
    byte[] numArray10 = new byte[15];
    numArray10[7] = (byte) 206;
    numArray10[6] = (byte) 7;
    numArray10[13] = (byte) 213;
    numArray10[11] = (byte) 34;
    numArray10[1] = (byte) 112 /*0x70*/;
    numArray10[5] = (byte) 27;
    numArray10[8] = (byte) 244;
    numArray10[12] = (byte) 91;
    numArray10[10] = (byte) 36;
    numArray10[9] = (byte) 66;
    numArray10[3] = (byte) 169;
    numArray10[2] = (byte) 212;
    numArray10[4] = (byte) 96 /*0x60*/;
    numArray10[0] = (byte) 147;
    numArray10[14] = (byte) 7;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13722()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[151];
      byte[] numArray2 = new byte[55]
      {
        (byte) 168,
        (byte) 18,
        (byte) 138,
        (byte) 195,
        (byte) 122,
        (byte) 152,
        (byte) 136,
        (byte) 165,
        (byte) 242,
        (byte) 13,
        (byte) 155,
        (byte) 193,
        (byte) 161,
        (byte) 158,
        (byte) 229,
        (byte) 117,
        (byte) 221,
        (byte) 59,
        (byte) 241,
        (byte) 240 /*0xF0*/,
        (byte) 143,
        (byte) 154,
        (byte) 90,
        (byte) 78,
        (byte) 135,
        (byte) 197,
        (byte) 202,
        (byte) 22,
        (byte) 124,
        (byte) 109,
        (byte) 243,
        (byte) 200,
        (byte) 148,
        (byte) 1,
        (byte) 205,
        (byte) 159,
        (byte) 180,
        (byte) 44,
        (byte) 244,
        (byte) 184,
        (byte) 187,
        (byte) 188,
        (byte) 62,
        (byte) 76,
        (byte) 62,
        (byte) 207,
        (byte) 188,
        (byte) 2,
        (byte) 116,
        (byte) 107,
        (byte) 223,
        (byte) 221,
        (byte) 54,
        (byte) 241,
        (byte) 33
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 131,
        (byte) 67,
        (byte) 101,
        (byte) 113,
        (byte) 244,
        (byte) 48 /*0x30*/,
        (byte) 199,
        (byte) 82,
        (byte) 122,
        (byte) 88,
        (byte) 86,
        (byte) 133,
        (byte) 53,
        (byte) 33,
        (byte) 115,
        (byte) 187,
        (byte) 111,
        (byte) 87,
        (byte) 210,
        (byte) 101,
        (byte) 22,
        (byte) 160 /*0xA0*/,
        (byte) 34,
        (byte) 115,
        (byte) 122,
        (byte) 74,
        (byte) 199,
        (byte) 213,
        (byte) 161,
        (byte) 193,
        (byte) 191,
        (byte) 45,
        (byte) 127 /*0x7F*/,
        (byte) 212,
        (byte) 106,
        (byte) 175,
        (byte) 39,
        (byte) 229,
        (byte) 242,
        (byte) 6,
        (byte) 15,
        (byte) 37,
        (byte) 203,
        (byte) 91,
        (byte) 191,
        (byte) 45,
        (byte) 40,
        (byte) 47,
        (byte) 54,
        (byte) 227,
        (byte) 170,
        (byte) 247,
        (byte) 46,
        (byte) 23,
        (byte) 24
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[21] = (byte) 16 /*0x10*/;
      numArray4[1] = (byte) 59;
      numArray4[35] = (byte) 241;
      numArray4[52] = (byte) 79;
      numArray4[4] = (byte) 113;
      numArray4[53] = (byte) 177;
      numArray4[6] = (byte) 132;
      numArray4[7] = (byte) 26;
      numArray4[49] = (byte) 149;
      numArray4[8] = (byte) 211;
      numArray4[10] = (byte) 160 /*0xA0*/;
      numArray4[17] = (byte) 52;
      numArray4[45] = (byte) 49;
      numArray4[2] = (byte) 181;
      numArray4[36] = (byte) 63 /*0x3F*/;
      numArray4[3] = (byte) 205;
      numArray4[16 /*0x10*/] = (byte) 56;
      numArray4[54] = (byte) 169;
      numArray4[18] = (byte) 210;
      numArray4[50] = (byte) 254;
      numArray4[20] = (byte) 13;
      numArray4[31 /*0x1F*/] = (byte) 223;
      numArray4[32 /*0x20*/] = (byte) 191;
      numArray4[23] = (byte) 9;
      numArray4[33] = (byte) 231;
      numArray4[25] = (byte) 162;
      numArray4[22] = (byte) 184;
      numArray4[24] = (byte) 200;
      numArray4[28] = (byte) 21;
      numArray4[29] = (byte) 145;
      numArray4[30] = (byte) 163;
      numArray4[15] = (byte) 71;
      numArray4[26] = (byte) 16 /*0x10*/;
      numArray4[19] = (byte) 92;
      numArray4[34] = (byte) 120;
      numArray4[39] = (byte) 147;
      numArray4[9] = (byte) 45;
      numArray4[5] = (byte) 41;
      numArray4[38] = (byte) 73;
      numArray4[14] = (byte) 176 /*0xB0*/;
      numArray4[40] = (byte) 185;
      numArray4[41] = (byte) 50;
      numArray4[42] = (byte) 107;
      numArray4[43] = (byte) 161;
      numArray4[44] = (byte) 28;
      numArray4[11] = (byte) 209;
      numArray4[46] = (byte) 45;
      numArray4[47] = (byte) 170;
      numArray4[48 /*0x30*/] = (byte) 46;
      numArray4[51] = (byte) 188;
      numArray4[37] = (byte) 235;
      numArray4[13] = (byte) 193;
      numArray4[0] = (byte) 60;
      numArray4[12] = (byte) 70;
      numArray4[27] = (byte) 142;
      byte[] numArray5 = new byte[55]
      {
        (byte) 176 /*0xB0*/,
        (byte) 85,
        (byte) 151,
        (byte) 56,
        (byte) 108,
        (byte) 130,
        (byte) 239,
        (byte) 213,
        (byte) 128 /*0x80*/,
        (byte) 2,
        (byte) 194,
        (byte) 220,
        (byte) 70,
        (byte) 229,
        (byte) 155,
        (byte) 52,
        (byte) 141,
        (byte) 32 /*0x20*/,
        (byte) 152,
        (byte) 250,
        (byte) 163,
        (byte) 0,
        (byte) 88,
        (byte) 67,
        (byte) 66,
        (byte) 7,
        (byte) 69,
        (byte) 128 /*0x80*/,
        (byte) 85,
        (byte) 152,
        (byte) 218,
        (byte) 215,
        (byte) 98,
        (byte) 29,
        (byte) 198,
        (byte) 143,
        (byte) 138,
        (byte) 245,
        (byte) 3,
        (byte) 185,
        (byte) 57,
        (byte) 219,
        (byte) 171,
        (byte) 173,
        (byte) 180,
        (byte) 221,
        (byte) 60,
        (byte) 211,
        (byte) 160 /*0xA0*/,
        (byte) 127 /*0x7F*/,
        (byte) 95,
        (byte) 137,
        (byte) 201,
        (byte) 62,
        (byte) 240 /*0xF0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[41];
      numArray6[12] = (byte) 82;
      numArray6[1] = (byte) 110;
      numArray6[2] = (byte) 90;
      numArray6[3] = (byte) 178;
      numArray6[4] = (byte) 149;
      numArray6[8] = (byte) 47;
      numArray6[6] = (byte) 173;
      numArray6[32 /*0x20*/] = (byte) 198;
      numArray6[31 /*0x1F*/] = (byte) 111;
      numArray6[9] = (byte) 4;
      numArray6[10] = (byte) 33;
      numArray6[5] = (byte) 132;
      numArray6[18] = (byte) 173;
      numArray6[13] = (byte) 117;
      numArray6[34] = (byte) 213;
      numArray6[27] = (byte) 226;
      numArray6[40] = (byte) 123;
      numArray6[17] = (byte) 223;
      numArray6[7] = (byte) 162;
      numArray6[29] = (byte) 129;
      numArray6[39] = (byte) 169;
      numArray6[21] = (byte) 145;
      numArray6[20] = byte.MaxValue;
      numArray6[23] = (byte) 166;
      numArray6[24] = (byte) 5;
      numArray6[19] = (byte) 123;
      numArray6[33] = (byte) 1;
      numArray6[26] = (byte) 242;
      numArray6[15] = (byte) 112 /*0x70*/;
      numArray6[14] = (byte) 102;
      numArray6[16 /*0x10*/] = (byte) 197;
      numArray6[35] = (byte) 120;
      numArray6[36] = (byte) 32 /*0x20*/;
      numArray6[22] = (byte) 119;
      numArray6[30] = (byte) 254;
      numArray6[0] = (byte) 190;
      numArray6[11] = (byte) 251;
      numArray6[37] = (byte) 199;
      numArray6[38] = (byte) 45;
      numArray6[25] = (byte) 242;
      numArray6[28] = (byte) 22;
      byte[] numArray7 = new byte[41]
      {
        (byte) 217,
        (byte) 201,
        (byte) 159,
        (byte) 230,
        (byte) 253,
        (byte) 197,
        (byte) 57,
        (byte) 79,
        (byte) 165,
        (byte) 5,
        (byte) 170,
        (byte) 19,
        (byte) 35,
        (byte) 176 /*0xB0*/,
        (byte) 4,
        (byte) 237,
        (byte) 189,
        (byte) 233,
        (byte) 49,
        (byte) 114,
        (byte) 2,
        (byte) 122,
        (byte) 251,
        (byte) 35,
        (byte) 236,
        (byte) 214,
        (byte) 51,
        (byte) 116,
        (byte) 103,
        (byte) 70,
        (byte) 39,
        (byte) 67,
        (byte) 206,
        (byte) 90,
        (byte) 1,
        (byte) 37,
        (byte) 111,
        (byte) 8,
        (byte) 207,
        (byte) 137,
        (byte) 90
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[151];
    byte[] numArray9 = new byte[55]
    {
      (byte) 207,
      (byte) 78,
      (byte) 187,
      (byte) 148,
      (byte) 167,
      (byte) 129,
      (byte) 108,
      (byte) 27,
      (byte) 137,
      (byte) 23,
      (byte) 175,
      (byte) 232,
      (byte) 126,
      (byte) 20,
      (byte) 250,
      (byte) 47,
      (byte) 203,
      (byte) 120,
      (byte) 93,
      (byte) 15,
      (byte) 76,
      (byte) 124,
      (byte) 28,
      (byte) 10,
      (byte) 226,
      (byte) 200,
      (byte) 153,
      (byte) 186,
      (byte) 13,
      (byte) 205,
      (byte) 182,
      (byte) 52,
      (byte) 214,
      (byte) 118,
      (byte) 54,
      (byte) 100,
      (byte) 122,
      (byte) 188,
      (byte) 248,
      (byte) 107,
      (byte) 228,
      (byte) 226,
      (byte) 40,
      (byte) 237,
      (byte) 80 /*0x50*/,
      (byte) 162,
      (byte) 230,
      (byte) 72,
      (byte) 254,
      (byte) 112 /*0x70*/,
      (byte) 11,
      (byte) 98,
      (byte) 57,
      (byte) 167,
      (byte) 235
    };
    byte[] numArray10 = new byte[55];
    numArray10[51] = (byte) 52;
    numArray10[27] = (byte) 162;
    numArray10[17] = (byte) 103;
    numArray10[0] = (byte) 29;
    numArray10[48 /*0x30*/] = (byte) 110;
    numArray10[32 /*0x20*/] = (byte) 93;
    numArray10[6] = (byte) 203;
    numArray10[29] = (byte) 209;
    numArray10[36] = (byte) 202;
    numArray10[9] = (byte) 161;
    numArray10[10] = (byte) 240 /*0xF0*/;
    numArray10[11] = (byte) 153;
    numArray10[12] = (byte) 5;
    numArray10[28] = (byte) 119;
    numArray10[14] = (byte) 8;
    numArray10[43] = (byte) 3;
    numArray10[30] = (byte) 247;
    numArray10[16 /*0x10*/] = (byte) 156;
    numArray10[18] = (byte) 37;
    numArray10[19] = (byte) 204;
    numArray10[20] = (byte) 254;
    numArray10[21] = (byte) 78;
    numArray10[25] = (byte) 74;
    numArray10[2] = (byte) 44;
    numArray10[24] = (byte) 182;
    numArray10[7] = (byte) 228;
    numArray10[26] = (byte) 119;
    numArray10[23] = (byte) 113;
    numArray10[47] = (byte) 97;
    numArray10[22] = (byte) 103;
    numArray10[37] = (byte) 47;
    numArray10[13] = (byte) 159;
    numArray10[49] = (byte) 223;
    numArray10[4] = (byte) 147;
    numArray10[34] = (byte) 9;
    numArray10[41] = (byte) 18;
    numArray10[53] = (byte) 67;
    numArray10[5] = (byte) 184;
    numArray10[40] = (byte) 223;
    numArray10[39] = (byte) 125;
    numArray10[46] = (byte) 6;
    numArray10[35] = (byte) 228;
    numArray10[42] = (byte) 132;
    numArray10[44] = (byte) 252;
    numArray10[45] = (byte) 41;
    numArray10[3] = (byte) 87;
    numArray10[33] = (byte) 110;
    numArray10[15] = (byte) 153;
    numArray10[31 /*0x1F*/] = (byte) 94;
    numArray10[50] = (byte) 15;
    numArray10[52] = (byte) 128 /*0x80*/;
    numArray10[1] = (byte) 8;
    numArray10[38] = (byte) 159;
    numArray10[8] = (byte) 66;
    numArray10[54] = (byte) 132;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[26] = (byte) 226;
    numArray11[12] = (byte) 100;
    numArray11[1] = (byte) 113;
    numArray11[3] = (byte) 150;
    numArray11[4] = (byte) 246;
    numArray11[5] = (byte) 112 /*0x70*/;
    numArray11[39] = (byte) 99;
    numArray11[20] = (byte) 162;
    numArray11[24] = (byte) 231;
    numArray11[53] = (byte) 24;
    numArray11[41] = (byte) 127 /*0x7F*/;
    numArray11[48 /*0x30*/] = (byte) 248;
    numArray11[52] = (byte) 235;
    numArray11[30] = (byte) 179;
    numArray11[8] = (byte) 127 /*0x7F*/;
    numArray11[15] = (byte) 103;
    numArray11[33] = (byte) 47;
    numArray11[17] = (byte) 249;
    numArray11[36] = (byte) 25;
    numArray11[47] = (byte) 206;
    numArray11[44] = (byte) 99;
    numArray11[21] = (byte) 227;
    numArray11[51] = (byte) 130;
    numArray11[23] = (byte) 1;
    numArray11[2] = (byte) 254;
    numArray11[25] = (byte) 83;
    numArray11[6] = (byte) 250;
    numArray11[0] = (byte) 247;
    numArray11[28] = (byte) 113;
    numArray11[29] = (byte) 179;
    numArray11[10] = (byte) 123;
    numArray11[13] = (byte) 236;
    numArray11[49] = (byte) 123;
    numArray11[14] = (byte) 3;
    numArray11[34] = (byte) 209;
    numArray11[16 /*0x10*/] = (byte) 110;
    numArray11[31 /*0x1F*/] = (byte) 10;
    numArray11[37] = (byte) 236;
    numArray11[38] = (byte) 59;
    numArray11[11] = (byte) 250;
    numArray11[32 /*0x20*/] = (byte) 227;
    numArray11[40] = (byte) 113;
    numArray11[18] = (byte) 149;
    numArray11[43] = (byte) 173;
    numArray11[7] = (byte) 12;
    numArray11[22] = (byte) 175;
    numArray11[46] = (byte) 3;
    numArray11[19] = (byte) 231;
    numArray11[27] = (byte) 197;
    numArray11[9] = (byte) 226;
    numArray11[50] = (byte) 217;
    numArray11[35] = (byte) 152;
    numArray11[42] = (byte) 87;
    numArray11[45] = (byte) 111;
    numArray11[54] = (byte) 96 /*0x60*/;
    byte[] numArray12 = new byte[55];
    numArray12[30] = (byte) 37;
    numArray12[16 /*0x10*/] = (byte) 244;
    numArray12[12] = (byte) 142;
    numArray12[23] = (byte) 164;
    numArray12[4] = (byte) 64 /*0x40*/;
    numArray12[0] = (byte) 156;
    numArray12[37] = (byte) 45;
    numArray12[38] = (byte) 19;
    numArray12[29] = (byte) 168;
    numArray12[5] = (byte) 32 /*0x20*/;
    numArray12[27] = (byte) 56;
    numArray12[35] = (byte) 174;
    numArray12[19] = (byte) 249;
    numArray12[10] = (byte) 228;
    numArray12[14] = (byte) 83;
    numArray12[15] = (byte) 19;
    numArray12[40] = (byte) 193;
    numArray12[17] = (byte) 81;
    numArray12[18] = (byte) 39;
    numArray12[21] = (byte) 237;
    numArray12[48 /*0x30*/] = (byte) 164;
    numArray12[49] = (byte) 173;
    numArray12[22] = (byte) 223;
    numArray12[20] = (byte) 229;
    numArray12[24] = (byte) 219;
    numArray12[36] = (byte) 28;
    numArray12[26] = (byte) 43;
    numArray12[45] = (byte) 245;
    numArray12[28] = (byte) 9;
    numArray12[44] = (byte) 26;
    numArray12[34] = (byte) 211;
    numArray12[31 /*0x1F*/] = (byte) 198;
    numArray12[43] = (byte) 57;
    numArray12[33] = (byte) 87;
    numArray12[50] = (byte) 101;
    numArray12[11] = (byte) 159;
    numArray12[3] = (byte) 253;
    numArray12[7] = (byte) 32 /*0x20*/;
    numArray12[41] = (byte) 225;
    numArray12[39] = (byte) 9;
    numArray12[32 /*0x20*/] = (byte) 21;
    numArray12[1] = (byte) 57;
    numArray12[42] = (byte) 222;
    numArray12[9] = (byte) 14;
    numArray12[51] = (byte) 217;
    numArray12[13] = (byte) 16 /*0x10*/;
    numArray12[46] = (byte) 242;
    numArray12[47] = (byte) 17;
    numArray12[25] = (byte) 246;
    numArray12[8] = (byte) 143;
    numArray12[52] = byte.MaxValue;
    numArray12[2] = (byte) 73;
    numArray12[6] = (byte) 236;
    numArray12[53] = (byte) 50;
    numArray12[54] = (byte) 61;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[41];
    numArray13[10] = (byte) 211;
    numArray13[1] = (byte) 25;
    numArray13[14] = (byte) 61;
    numArray13[26] = (byte) 204;
    numArray13[17] = (byte) 101;
    numArray13[5] = (byte) 220;
    numArray13[6] = (byte) 111;
    numArray13[7] = (byte) 147;
    numArray13[21] = (byte) 11;
    numArray13[9] = (byte) 34;
    numArray13[2] = (byte) 210;
    numArray13[0] = (byte) 178;
    numArray13[40] = (byte) 43;
    numArray13[13] = (byte) 114;
    numArray13[39] = (byte) 97;
    numArray13[27] = (byte) 108;
    numArray13[16 /*0x10*/] = (byte) 3;
    numArray13[12] = (byte) 126;
    numArray13[18] = (byte) 1;
    numArray13[19] = (byte) 101;
    numArray13[8] = (byte) 236;
    numArray13[32 /*0x20*/] = (byte) 175;
    numArray13[22] = (byte) 16 /*0x10*/;
    numArray13[4] = (byte) 42;
    numArray13[24] = (byte) 7;
    numArray13[25] = (byte) 68;
    numArray13[15] = (byte) 224 /*0xE0*/;
    numArray13[31 /*0x1F*/] = (byte) 148;
    numArray13[28] = (byte) 205;
    numArray13[29] = (byte) 8;
    numArray13[30] = (byte) 191;
    numArray13[11] = (byte) 38;
    numArray13[3] = (byte) 27;
    numArray13[23] = (byte) 23;
    numArray13[34] = (byte) 11;
    numArray13[35] = (byte) 237;
    numArray13[36] = (byte) 115;
    numArray13[37] = (byte) 61;
    numArray13[38] = (byte) 169;
    numArray13[20] = (byte) 5;
    numArray13[33] = (byte) 217;
    byte[] numArray14 = new byte[41];
    numArray14[18] = (byte) 142;
    numArray14[12] = (byte) 59;
    numArray14[2] = (byte) 60;
    numArray14[17] = (byte) 138;
    numArray14[4] = (byte) 84;
    numArray14[5] = (byte) 162;
    numArray14[30] = (byte) 222;
    numArray14[7] = (byte) 90;
    numArray14[20] = (byte) 152;
    numArray14[9] = (byte) 9;
    numArray14[11] = (byte) 15;
    numArray14[1] = (byte) 218;
    numArray14[35] = (byte) 197;
    numArray14[19] = (byte) 1;
    numArray14[14] = (byte) 224 /*0xE0*/;
    numArray14[33] = (byte) 132;
    numArray14[16 /*0x10*/] = (byte) 32 /*0x20*/;
    numArray14[13] = (byte) 151;
    numArray14[3] = (byte) 157;
    numArray14[15] = (byte) 227;
    numArray14[0] = (byte) 191;
    numArray14[28] = (byte) 38;
    numArray14[27] = (byte) 55;
    numArray14[6] = (byte) 125;
    numArray14[31 /*0x1F*/] = (byte) 108;
    numArray14[25] = (byte) 124;
    numArray14[24] = (byte) 141;
    numArray14[26] = (byte) 170;
    numArray14[22] = (byte) 20;
    numArray14[29] = (byte) 241;
    numArray14[39] = (byte) 97;
    numArray14[21] = (byte) 232;
    numArray14[32 /*0x20*/] = (byte) 140;
    numArray14[23] = (byte) 244;
    numArray14[34] = (byte) 92;
    numArray14[10] = (byte) 43;
    numArray14[36] = (byte) 252;
    numArray14[37] = (byte) 138;
    numArray14[38] = (byte) 75;
    numArray14[8] = (byte) 87;
    numArray14[40] = (byte) 197;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 41);
    for (int index = 0; index < 41; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[54];
    byte[] response = new byte[54];
    Array.Copy((Array) sc_13686.sspq, 375, (Array) numArray15, 0, 54);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_13686.sspr, 375, (Array) numArray15, 0, 54);
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

  internal static int ssp_appserver_13723(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 200,
      (byte) 83,
      (byte) 133,
      (byte) 238,
      (byte) 184,
      (byte) 121,
      (byte) 75,
      (byte) 35,
      (byte) 252,
      (byte) 242,
      (byte) 33,
      (byte) 36,
      (byte) 36,
      (byte) 48 /*0x30*/,
      (byte) 206,
      (byte) 90,
      (byte) 77,
      (byte) 180,
      (byte) 114,
      (byte) 202,
      (byte) 129,
      (byte) 215,
      (byte) 93,
      (byte) 116,
      (byte) 129,
      (byte) 174,
      (byte) 73,
      (byte) 141,
      (byte) 82,
      (byte) 53,
      (byte) 173,
      (byte) 102,
      (byte) 71,
      (byte) 254,
      (byte) 68,
      (byte) 131,
      (byte) 163,
      (byte) 217,
      (byte) 146,
      (byte) 148,
      (byte) 214,
      (byte) 3,
      (byte) 177,
      (byte) 108,
      (byte) 216,
      (byte) 244,
      (byte) 15,
      (byte) 55
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 21,
      (byte) 176 /*0xB0*/,
      (byte) 196,
      (byte) 28,
      (byte) 85,
      (byte) 141,
      (byte) 234,
      (byte) 206,
      (byte) 159,
      (byte) 156,
      (byte) 161,
      (byte) 174,
      (byte) 62,
      (byte) 239,
      (byte) 97,
      (byte) 146,
      (byte) 195,
      (byte) 228,
      (byte) 236,
      (byte) 154,
      (byte) 6,
      (byte) 90,
      (byte) 225,
      (byte) 159,
      (byte) 165,
      (byte) 125,
      (byte) 136,
      (byte) 191,
      (byte) 33,
      (byte) 223,
      (byte) 169,
      (byte) 135,
      (byte) 182,
      (byte) 154,
      (byte) 41,
      (byte) 246,
      (byte) 69,
      (byte) 9,
      (byte) 156,
      (byte) 249,
      (byte) 157,
      (byte) 166,
      (byte) 116,
      (byte) 132,
      (byte) 94,
      (byte) 160 /*0xA0*/,
      (byte) 224 /*0xE0*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13724(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 230,
      (byte) 206,
      (byte) 241,
      (byte) 246,
      (byte) 16 /*0x10*/,
      (byte) 108,
      (byte) 10,
      (byte) 47,
      (byte) 189,
      (byte) 208 /*0xD0*/,
      (byte) 199,
      (byte) 69,
      (byte) 186,
      (byte) 221,
      (byte) 37,
      (byte) 235,
      (byte) 74,
      (byte) 182,
      (byte) 116,
      (byte) 33,
      (byte) 221,
      (byte) 243,
      (byte) 126,
      (byte) 38,
      (byte) 241,
      (byte) 156,
      (byte) 141,
      (byte) 159,
      (byte) 17,
      (byte) 25,
      (byte) 46,
      (byte) 149,
      (byte) 244,
      (byte) 164,
      (byte) 97,
      (byte) 48 /*0x30*/,
      (byte) 83,
      (byte) 141,
      (byte) 147,
      (byte) 108,
      (byte) 136,
      (byte) 100,
      (byte) 25,
      (byte) 50,
      (byte) 233,
      (byte) 39,
      (byte) 29,
      (byte) 22
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 84,
      (byte) 213,
      (byte) 109,
      (byte) 175,
      (byte) 81,
      (byte) 151,
      (byte) 89,
      (byte) 247,
      (byte) 242,
      (byte) 134,
      (byte) 195,
      (byte) 3,
      (byte) 190,
      (byte) 138,
      (byte) 60,
      (byte) 74,
      (byte) 15,
      (byte) 76,
      (byte) 96 /*0x60*/,
      (byte) 235,
      (byte) 55,
      (byte) 47,
      (byte) 57,
      (byte) 248,
      (byte) 24,
      (byte) 198,
      (byte) 132,
      (byte) 42,
      (byte) 138,
      (byte) 238,
      (byte) 38,
      (byte) 175,
      (byte) 231,
      (byte) 232,
      (byte) 192 /*0xC0*/,
      (byte) 230,
      (byte) 239,
      (byte) 97,
      (byte) 23,
      (byte) 110,
      (byte) 254,
      (byte) 247,
      (byte) 248,
      (byte) 164,
      (byte) 110,
      (byte) 35,
      (byte) 227,
      (byte) 25
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13725(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[19] = (byte) 35;
    sourceArray1[1] = (byte) 228;
    sourceArray1[2] = (byte) 242;
    sourceArray1[31 /*0x1F*/] = (byte) 239;
    sourceArray1[34] = (byte) 93;
    sourceArray1[13] = (byte) 80 /*0x50*/;
    sourceArray1[46] = (byte) 182;
    sourceArray1[39] = (byte) 136;
    sourceArray1[8] = (byte) 106;
    sourceArray1[9] = (byte) 29;
    sourceArray1[15] = (byte) 124;
    sourceArray1[11] = (byte) 161;
    sourceArray1[12] = (byte) 50;
    sourceArray1[21] = (byte) 153;
    sourceArray1[14] = (byte) 22;
    sourceArray1[37] = (byte) 224 /*0xE0*/;
    sourceArray1[27] = (byte) 127 /*0x7F*/;
    sourceArray1[41] = (byte) 142;
    sourceArray1[6] = (byte) 116;
    sourceArray1[22] = (byte) 121;
    sourceArray1[20] = (byte) 7;
    sourceArray1[4] = (byte) 203;
    sourceArray1[10] = (byte) 164;
    sourceArray1[24] = (byte) 75;
    sourceArray1[3] = (byte) 79;
    sourceArray1[25] = (byte) 61;
    sourceArray1[0] = (byte) 151;
    sourceArray1[17] = (byte) 152;
    sourceArray1[16 /*0x10*/] = (byte) 245;
    sourceArray1[29] = (byte) 182;
    sourceArray1[30] = (byte) 197;
    sourceArray1[26] = (byte) 202;
    sourceArray1[32 /*0x20*/] = (byte) 193;
    sourceArray1[33] = (byte) 106;
    sourceArray1[44] = (byte) 117;
    sourceArray1[35] = (byte) 191;
    sourceArray1[36] = (byte) 110;
    sourceArray1[45] = (byte) 144 /*0x90*/;
    sourceArray1[7] = (byte) 222;
    sourceArray1[18] = (byte) 189;
    sourceArray1[40] = (byte) 30;
    sourceArray1[5] = (byte) 4;
    sourceArray1[42] = (byte) 29;
    sourceArray1[43] = (byte) 71;
    sourceArray1[23] = (byte) 217;
    sourceArray1[47] = (byte) 214;
    sourceArray1[38] = (byte) 135;
    sourceArray1[28] = (byte) 119;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 97;
    sourceArray2[39] = (byte) 217;
    sourceArray2[23] = (byte) 30;
    sourceArray2[3] = (byte) 173;
    sourceArray2[38] = (byte) 13;
    sourceArray2[0] = (byte) 170;
    sourceArray2[6] = (byte) 170;
    sourceArray2[1] = (byte) 220;
    sourceArray2[8] = (byte) 66;
    sourceArray2[27] = (byte) 135;
    sourceArray2[28] = (byte) 99;
    sourceArray2[14] = (byte) 200;
    sourceArray2[12] = (byte) 33;
    sourceArray2[13] = (byte) 124;
    sourceArray2[35] = (byte) 19;
    sourceArray2[4] = (byte) 222;
    sourceArray2[36] = (byte) 221;
    sourceArray2[17] = (byte) 165;
    sourceArray2[7] = (byte) 238;
    sourceArray2[19] = (byte) 69;
    sourceArray2[10] = (byte) 40;
    sourceArray2[21] = (byte) 150;
    sourceArray2[22] = (byte) 37;
    sourceArray2[43] = (byte) 214;
    sourceArray2[24] = (byte) 122;
    sourceArray2[25] = (byte) 57;
    sourceArray2[40] = (byte) 174;
    sourceArray2[37] = (byte) 19;
    sourceArray2[9] = (byte) 35;
    sourceArray2[20] = (byte) 104;
    sourceArray2[30] = (byte) 94;
    sourceArray2[31 /*0x1F*/] = (byte) 132;
    sourceArray2[11] = (byte) 39;
    sourceArray2[33] = (byte) 92;
    sourceArray2[34] = (byte) 102;
    sourceArray2[15] = (byte) 159;
    sourceArray2[32 /*0x20*/] = (byte) 11;
    sourceArray2[2] = (byte) 219;
    sourceArray2[16 /*0x10*/] = (byte) 29;
    sourceArray2[18] = (byte) 178;
    sourceArray2[46] = (byte) 233;
    sourceArray2[41] = (byte) 137;
    sourceArray2[42] = (byte) 95;
    sourceArray2[5] = (byte) 240 /*0xF0*/;
    sourceArray2[44] = (byte) 213;
    sourceArray2[45] = (byte) 46;
    sourceArray2[29] = (byte) 43;
    sourceArray2[47] = (byte) 193;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[49];
    byte[] response2 = new byte[49];
    Array.Copy((Array) sc_13686.sspq, 429, (Array) numArray2, 0, 49);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 429, (Array) numArray2, 0, 49);
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

  internal static string ssp_appserver_13726()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 71,
        (byte) 109,
        (byte) 35,
        (byte) 143,
        (byte) 82,
        (byte) 129,
        (byte) 205,
        (byte) 247,
        (byte) 95,
        (byte) 104,
        (byte) 206,
        (byte) 171,
        (byte) 17,
        (byte) 138,
        (byte) 153,
        (byte) 192 /*0xC0*/,
        (byte) 189,
        (byte) 254,
        (byte) 103,
        (byte) 111,
        (byte) 56,
        (byte) 158,
        (byte) 210,
        (byte) 91,
        (byte) 254,
        (byte) 124,
        (byte) 70
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 131,
        (byte) 51,
        (byte) 188,
        (byte) 35,
        (byte) 133,
        (byte) 97,
        (byte) 165,
        (byte) 254,
        (byte) 225,
        (byte) 75,
        (byte) 162,
        (byte) 31 /*0x1F*/,
        (byte) 116,
        (byte) 249,
        (byte) 3,
        (byte) 217,
        (byte) 118,
        (byte) 216,
        byte.MaxValue,
        (byte) 121,
        (byte) 214,
        (byte) 88,
        (byte) 22,
        (byte) 158,
        (byte) 21,
        (byte) 12,
        (byte) 204
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
      (byte) 207,
      (byte) 248,
      (byte) 232,
      (byte) 106,
      (byte) 155,
      (byte) 72,
      (byte) 199,
      (byte) 135,
      (byte) 85,
      (byte) 163,
      (byte) 18,
      (byte) 55,
      (byte) 88,
      (byte) 52,
      (byte) 204,
      (byte) 103,
      (byte) 177,
      (byte) 158,
      (byte) 37,
      (byte) 93,
      (byte) 189,
      (byte) 64 /*0x40*/,
      (byte) 121,
      (byte) 140,
      (byte) 146,
      (byte) 169,
      (byte) 155
    };
    byte[] numArray6 = new byte[27]
    {
      (byte) 6,
      (byte) 170,
      (byte) 103,
      (byte) 8,
      (byte) 69,
      (byte) 1,
      (byte) 196,
      (byte) 236,
      (byte) 119,
      (byte) 183,
      (byte) 129,
      (byte) 42,
      (byte) 86,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 91,
      (byte) 186,
      (byte) 226,
      (byte) 251,
      (byte) 129,
      (byte) 167,
      (byte) 242,
      (byte) 94,
      (byte) 47,
      (byte) 203,
      (byte) 91,
      (byte) 186
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13727(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[6] = (byte) 238;
    sourceArray1[1] = (byte) 159;
    sourceArray1[2] = (byte) 152;
    sourceArray1[39] = (byte) 11;
    sourceArray1[4] = (byte) 195;
    sourceArray1[5] = (byte) 166;
    sourceArray1[20] = (byte) 83;
    sourceArray1[19] = (byte) 167;
    sourceArray1[37] = (byte) 245;
    sourceArray1[9] = (byte) 53;
    sourceArray1[21] = (byte) 56;
    sourceArray1[41] = (byte) 212;
    sourceArray1[12] = (byte) 92;
    sourceArray1[13] = (byte) 0;
    sourceArray1[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    sourceArray1[15] = (byte) 157;
    sourceArray1[7] = (byte) 80 /*0x50*/;
    sourceArray1[17] = (byte) 150;
    sourceArray1[18] = (byte) 202;
    sourceArray1[38] = (byte) 163;
    sourceArray1[11] = (byte) 196;
    sourceArray1[8] = (byte) 248;
    sourceArray1[22] = (byte) 3;
    sourceArray1[23] = (byte) 143;
    sourceArray1[24] = (byte) 127 /*0x7F*/;
    sourceArray1[25] = (byte) 91;
    sourceArray1[33] = (byte) 190;
    sourceArray1[27] = (byte) 242;
    sourceArray1[28] = (byte) 172;
    sourceArray1[29] = byte.MaxValue;
    sourceArray1[40] = (byte) 175;
    sourceArray1[35] = (byte) 185;
    sourceArray1[26] = (byte) 216;
    sourceArray1[0] = (byte) 97;
    sourceArray1[34] = (byte) 214;
    sourceArray1[16 /*0x10*/] = (byte) 142;
    sourceArray1[44] = (byte) 151;
    sourceArray1[42] = (byte) 101;
    sourceArray1[3] = (byte) 214;
    sourceArray1[36] = (byte) 156;
    sourceArray1[32 /*0x20*/] = (byte) 104;
    sourceArray1[14] = (byte) 69;
    sourceArray1[10] = (byte) 168;
    sourceArray1[43] = (byte) 135;
    sourceArray1[30] = (byte) 246;
    sourceArray1[45] = (byte) 180;
    sourceArray1[46] = (byte) 48 /*0x30*/;
    sourceArray1[47] = (byte) 145;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 164;
    sourceArray2[1] = (byte) 49;
    sourceArray2[45] = (byte) 38;
    sourceArray2[30] = (byte) 83;
    sourceArray2[4] = (byte) 252;
    sourceArray2[5] = (byte) 84;
    sourceArray2[39] = (byte) 99;
    sourceArray2[25] = (byte) 14;
    sourceArray2[8] = (byte) 87;
    sourceArray2[46] = (byte) 98;
    sourceArray2[31 /*0x1F*/] = (byte) 115;
    sourceArray2[36] = (byte) 102;
    sourceArray2[12] = (byte) 128 /*0x80*/;
    sourceArray2[33] = (byte) 10;
    sourceArray2[14] = (byte) 84;
    sourceArray2[29] = (byte) 162;
    sourceArray2[27] = (byte) 75;
    sourceArray2[17] = (byte) 75;
    sourceArray2[18] = (byte) 23;
    sourceArray2[2] = (byte) 166;
    sourceArray2[20] = (byte) 19;
    sourceArray2[19] = (byte) 250;
    sourceArray2[22] = (byte) 1;
    sourceArray2[23] = (byte) 176 /*0xB0*/;
    sourceArray2[24] = (byte) 62;
    sourceArray2[7] = (byte) 74;
    sourceArray2[26] = (byte) 57;
    sourceArray2[0] = (byte) 219;
    sourceArray2[28] = (byte) 91;
    sourceArray2[11] = (byte) 237;
    sourceArray2[3] = (byte) 7;
    sourceArray2[42] = (byte) 226;
    sourceArray2[32 /*0x20*/] = (byte) 37;
    sourceArray2[34] = (byte) 171;
    sourceArray2[9] = (byte) 1;
    sourceArray2[35] = (byte) 118;
    sourceArray2[10] = (byte) 46;
    sourceArray2[37] = (byte) 209;
    sourceArray2[38] = (byte) 202;
    sourceArray2[15] = (byte) 59;
    sourceArray2[40] = (byte) 230;
    sourceArray2[41] = (byte) 126;
    sourceArray2[16 /*0x10*/] = (byte) 223;
    sourceArray2[43] = (byte) 234;
    sourceArray2[21] = (byte) 62;
    sourceArray2[44] = (byte) 234;
    sourceArray2[6] = (byte) 59;
    sourceArray2[47] = (byte) 112 /*0x70*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13728(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[3] = (byte) 56;
    sourceArray1[30] = (byte) 114;
    sourceArray1[2] = (byte) 59;
    sourceArray1[47] = (byte) 95;
    sourceArray1[4] = (byte) 2;
    sourceArray1[23] = (byte) 69;
    sourceArray1[46] = (byte) 162;
    sourceArray1[21] = (byte) 166;
    sourceArray1[9] = (byte) 103;
    sourceArray1[11] = (byte) 165;
    sourceArray1[13] = (byte) 201;
    sourceArray1[45] = (byte) 127 /*0x7F*/;
    sourceArray1[1] = (byte) 69;
    sourceArray1[24] = (byte) 13;
    sourceArray1[14] = (byte) 86;
    sourceArray1[28] = (byte) 164;
    sourceArray1[19] = (byte) 25;
    sourceArray1[17] = (byte) 235;
    sourceArray1[18] = (byte) 137;
    sourceArray1[5] = (byte) 185;
    sourceArray1[20] = (byte) 165;
    sourceArray1[32 /*0x20*/] = (byte) 233;
    sourceArray1[16 /*0x10*/] = (byte) 236;
    sourceArray1[33] = (byte) 184;
    sourceArray1[42] = (byte) 244;
    sourceArray1[25] = (byte) 81;
    sourceArray1[38] = (byte) 49;
    sourceArray1[27] = (byte) 76;
    sourceArray1[10] = (byte) 144 /*0x90*/;
    sourceArray1[29] = (byte) 34;
    sourceArray1[34] = (byte) 203;
    sourceArray1[31 /*0x1F*/] = (byte) 236;
    sourceArray1[8] = (byte) 235;
    sourceArray1[43] = (byte) 79;
    sourceArray1[22] = (byte) 182;
    sourceArray1[35] = byte.MaxValue;
    sourceArray1[36] = (byte) 18;
    sourceArray1[0] = (byte) 182;
    sourceArray1[12] = (byte) 81;
    sourceArray1[39] = (byte) 207;
    sourceArray1[40] = (byte) 162;
    sourceArray1[41] = (byte) 229;
    sourceArray1[37] = (byte) 47;
    sourceArray1[26] = (byte) 140;
    sourceArray1[44] = (byte) 31 /*0x1F*/;
    sourceArray1[6] = (byte) 208 /*0xD0*/;
    sourceArray1[7] = (byte) 178;
    sourceArray1[15] = (byte) 52;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 19,
      (byte) 74,
      byte.MaxValue,
      (byte) 137,
      (byte) 244,
      (byte) 76,
      (byte) 0,
      (byte) 189,
      (byte) 177,
      (byte) 241,
      (byte) 124,
      (byte) 128 /*0x80*/,
      (byte) 4,
      (byte) 210,
      (byte) 119,
      (byte) 161,
      (byte) 11,
      (byte) 241,
      (byte) 98,
      (byte) 193,
      (byte) 16 /*0x10*/,
      (byte) 195,
      (byte) 207,
      (byte) 154,
      (byte) 178,
      (byte) 15,
      (byte) 24,
      (byte) 15,
      (byte) 73,
      (byte) 64 /*0x40*/,
      (byte) 212,
      (byte) 128 /*0x80*/,
      (byte) 137,
      (byte) 62,
      (byte) 46,
      (byte) 121,
      (byte) 181,
      (byte) 125,
      (byte) 121,
      (byte) 240 /*0xF0*/,
      (byte) 221,
      (byte) 217,
      (byte) 117,
      (byte) 192 /*0xC0*/,
      (byte) 77,
      (byte) 22,
      (byte) 79,
      (byte) 102
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13729(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 9,
      (byte) 97,
      (byte) 140,
      (byte) 229,
      (byte) 175,
      (byte) 87,
      (byte) 62,
      (byte) 127 /*0x7F*/,
      (byte) 152,
      (byte) 92,
      (byte) 38,
      (byte) 208 /*0xD0*/,
      (byte) 156,
      (byte) 2,
      byte.MaxValue,
      (byte) 26,
      (byte) 113,
      (byte) 19,
      (byte) 149,
      (byte) 176 /*0xB0*/,
      (byte) 201,
      (byte) 56,
      (byte) 65,
      (byte) 230,
      (byte) 6,
      (byte) 185,
      (byte) 58,
      (byte) 23,
      (byte) 89,
      (byte) 233,
      (byte) 134,
      (byte) 13,
      (byte) 170,
      (byte) 202,
      (byte) 166,
      (byte) 203,
      (byte) 220,
      (byte) 183,
      (byte) 6,
      (byte) 9,
      (byte) 181,
      (byte) 114,
      (byte) 199,
      (byte) 95,
      (byte) 74,
      (byte) 75,
      (byte) 104,
      (byte) 125
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 218,
      (byte) 118,
      (byte) 174,
      (byte) 88,
      (byte) 177,
      (byte) 163,
      (byte) 254,
      (byte) 247,
      (byte) 52,
      (byte) 131,
      (byte) 43,
      (byte) 222,
      (byte) 142,
      byte.MaxValue,
      (byte) 57,
      (byte) 174,
      (byte) 64 /*0x40*/,
      (byte) 104,
      (byte) 140,
      (byte) 106,
      (byte) 164,
      (byte) 245,
      (byte) 69,
      (byte) 18,
      (byte) 21,
      (byte) 189,
      (byte) 111,
      (byte) 120,
      (byte) 29,
      (byte) 212,
      (byte) 181,
      (byte) 252,
      (byte) 131,
      (byte) 240 /*0xF0*/,
      (byte) 11,
      (byte) 180,
      (byte) 70,
      (byte) 91,
      (byte) 20,
      (byte) 3,
      (byte) 109,
      (byte) 226,
      (byte) 71,
      (byte) 167,
      (byte) 110,
      (byte) 9,
      (byte) 117,
      (byte) 46
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[27];
    byte[] response2 = new byte[27];
    Array.Copy((Array) sc_13686.sspq, 478, (Array) numArray2, 0, 27);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 478, (Array) numArray2, 0, 27);
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

  internal static int ssp_appserver_13730(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[10] = (byte) 54;
    sourceArray1[1] = (byte) 116;
    sourceArray1[32 /*0x20*/] = (byte) 37;
    sourceArray1[3] = (byte) 131;
    sourceArray1[17] = (byte) 137;
    sourceArray1[7] = (byte) 208 /*0xD0*/;
    sourceArray1[6] = (byte) 223;
    sourceArray1[19] = (byte) 38;
    sourceArray1[30] = (byte) 62;
    sourceArray1[8] = (byte) 213;
    sourceArray1[5] = (byte) 72;
    sourceArray1[11] = (byte) 215;
    sourceArray1[12] = (byte) 16 /*0x10*/;
    sourceArray1[27] = (byte) 208 /*0xD0*/;
    sourceArray1[16 /*0x10*/] = (byte) 24;
    sourceArray1[15] = (byte) 77;
    sourceArray1[37] = (byte) 43;
    sourceArray1[21] = (byte) 33;
    sourceArray1[29] = (byte) 94;
    sourceArray1[0] = (byte) 188;
    sourceArray1[20] = (byte) 150;
    sourceArray1[2] = (byte) 86;
    sourceArray1[26] = (byte) 57;
    sourceArray1[23] = (byte) 197;
    sourceArray1[47] = (byte) 159;
    sourceArray1[25] = (byte) 9;
    sourceArray1[40] = (byte) 70;
    sourceArray1[46] = (byte) 253;
    sourceArray1[28] = (byte) 163;
    sourceArray1[13] = (byte) 236;
    sourceArray1[24] = (byte) 172;
    sourceArray1[31 /*0x1F*/] = (byte) 10;
    sourceArray1[22] = (byte) 110;
    sourceArray1[33] = (byte) 164;
    sourceArray1[34] = (byte) 96 /*0x60*/;
    sourceArray1[9] = (byte) 171;
    sourceArray1[18] = (byte) 48 /*0x30*/;
    sourceArray1[35] = (byte) 178;
    sourceArray1[38] = (byte) 102;
    sourceArray1[39] = (byte) 209;
    sourceArray1[41] = (byte) 0;
    sourceArray1[36] = (byte) 207;
    sourceArray1[42] = (byte) 134;
    sourceArray1[43] = (byte) 136;
    sourceArray1[44] = (byte) 7;
    sourceArray1[45] = (byte) 225;
    sourceArray1[14] = (byte) 142;
    sourceArray1[4] = (byte) 111;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 115,
      (byte) 240 /*0xF0*/,
      (byte) 253,
      (byte) 109,
      (byte) 226,
      (byte) 96 /*0x60*/,
      (byte) 104,
      (byte) 148,
      (byte) 5,
      (byte) 55,
      (byte) 62,
      (byte) 80 /*0x50*/,
      (byte) 203,
      (byte) 33,
      (byte) 207,
      (byte) 224 /*0xE0*/,
      (byte) 6,
      (byte) 30,
      (byte) 89,
      (byte) 39,
      (byte) 132,
      (byte) 231,
      (byte) 99,
      (byte) 178,
      (byte) 156,
      (byte) 163,
      (byte) 127 /*0x7F*/,
      (byte) 201,
      (byte) 20,
      (byte) 177,
      (byte) 219,
      (byte) 233,
      (byte) 27,
      (byte) 176 /*0xB0*/,
      (byte) 190,
      (byte) 99,
      (byte) 10,
      (byte) 116,
      (byte) 107,
      (byte) 201,
      (byte) 218,
      (byte) 118,
      (byte) 162,
      (byte) 240 /*0xF0*/,
      (byte) 41,
      (byte) 185,
      (byte) 225,
      (byte) 181
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13731(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 146,
      (byte) 19,
      (byte) 144 /*0x90*/,
      (byte) 139,
      (byte) 108,
      (byte) 52,
      (byte) 12,
      (byte) 180,
      (byte) 35,
      (byte) 254,
      (byte) 31 /*0x1F*/,
      (byte) 210,
      (byte) 13,
      (byte) 90,
      (byte) 117,
      (byte) 169,
      (byte) 147,
      (byte) 113,
      (byte) 133,
      (byte) 140,
      (byte) 33,
      (byte) 128 /*0x80*/,
      (byte) 198,
      (byte) 46,
      (byte) 62,
      (byte) 152,
      (byte) 35,
      (byte) 156,
      (byte) 168,
      (byte) 195,
      (byte) 140,
      (byte) 202,
      (byte) 226,
      (byte) 155,
      (byte) 120,
      (byte) 133,
      (byte) 212,
      (byte) 14,
      (byte) 152,
      (byte) 179,
      (byte) 147,
      (byte) 220,
      (byte) 199,
      (byte) 198,
      (byte) 141,
      (byte) 107,
      (byte) 71,
      (byte) 165
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[14] = (byte) 120;
    sourceArray2[1] = (byte) 148;
    sourceArray2[42] = (byte) 89;
    sourceArray2[11] = (byte) 68;
    sourceArray2[4] = (byte) 141;
    sourceArray2[7] = (byte) 93;
    sourceArray2[6] = (byte) 154;
    sourceArray2[37] = (byte) 206;
    sourceArray2[8] = (byte) 114;
    sourceArray2[41] = (byte) 214;
    sourceArray2[10] = (byte) 65;
    sourceArray2[22] = (byte) 80 /*0x50*/;
    sourceArray2[35] = (byte) 247;
    sourceArray2[5] = (byte) 204;
    sourceArray2[29] = (byte) 43;
    sourceArray2[15] = (byte) 186;
    sourceArray2[2] = (byte) 146;
    sourceArray2[17] = (byte) 142;
    sourceArray2[25] = (byte) 162;
    sourceArray2[40] = (byte) 130;
    sourceArray2[20] = (byte) 33;
    sourceArray2[21] = (byte) 45;
    sourceArray2[3] = (byte) 84;
    sourceArray2[23] = (byte) 188;
    sourceArray2[24] = (byte) 118;
    sourceArray2[12] = (byte) 250;
    sourceArray2[26] = (byte) 174;
    sourceArray2[27] = (byte) 178;
    sourceArray2[16 /*0x10*/] = (byte) 225;
    sourceArray2[34] = (byte) 112 /*0x70*/;
    sourceArray2[36] = (byte) 115;
    sourceArray2[31 /*0x1F*/] = (byte) 7;
    sourceArray2[32 /*0x20*/] = (byte) 136;
    sourceArray2[13] = (byte) 81;
    sourceArray2[0] = (byte) 240 /*0xF0*/;
    sourceArray2[44] = (byte) 29;
    sourceArray2[30] = (byte) 226;
    sourceArray2[19] = (byte) 162;
    sourceArray2[18] = (byte) 65;
    sourceArray2[38] = (byte) 168;
    sourceArray2[33] = (byte) 196;
    sourceArray2[39] = (byte) 191;
    sourceArray2[9] = (byte) 77;
    sourceArray2[43] = (byte) 195;
    sourceArray2[28] = (byte) 163;
    sourceArray2[45] = (byte) 113;
    sourceArray2[46] = (byte) 115;
    sourceArray2[47] = (byte) 249;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[50];
    byte[] response2 = new byte[50];
    Array.Copy((Array) sc_13686.sspq, 505, (Array) numArray2, 0, 50);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 505, (Array) numArray2, 0, 50);
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

  internal static int ssp_appserver_13732(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 138,
      (byte) 206,
      (byte) 23,
      (byte) 28,
      (byte) 240 /*0xF0*/,
      (byte) 71,
      (byte) 163,
      (byte) 87,
      (byte) 246,
      (byte) 178,
      (byte) 147,
      (byte) 127 /*0x7F*/,
      (byte) 83,
      (byte) 6,
      (byte) 162,
      (byte) 86,
      (byte) 34,
      (byte) 6,
      (byte) 217,
      (byte) 41,
      (byte) 143,
      (byte) 19,
      (byte) 93,
      (byte) 176 /*0xB0*/,
      (byte) 33,
      (byte) 30,
      (byte) 111,
      (byte) 207,
      (byte) 174,
      (byte) 125,
      (byte) 209,
      (byte) 33,
      (byte) 208 /*0xD0*/,
      (byte) 149,
      (byte) 199,
      (byte) 220,
      (byte) 82,
      (byte) 58,
      (byte) 139,
      (byte) 114,
      (byte) 81,
      (byte) 10,
      (byte) 210,
      (byte) 117,
      (byte) 221,
      (byte) 189,
      (byte) 114,
      (byte) 6
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[45] = (byte) 81;
    sourceArray2[33] = (byte) 192 /*0xC0*/;
    sourceArray2[22] = (byte) 61;
    sourceArray2[32 /*0x20*/] = (byte) 229;
    sourceArray2[41] = (byte) 31 /*0x1F*/;
    sourceArray2[5] = (byte) 140;
    sourceArray2[18] = (byte) 41;
    sourceArray2[43] = (byte) 216;
    sourceArray2[8] = (byte) 66;
    sourceArray2[28] = (byte) 241;
    sourceArray2[10] = (byte) 155;
    sourceArray2[11] = (byte) 235;
    sourceArray2[26] = (byte) 240 /*0xF0*/;
    sourceArray2[13] = (byte) 145;
    sourceArray2[14] = (byte) 41;
    sourceArray2[15] = (byte) 242;
    sourceArray2[16 /*0x10*/] = (byte) 44;
    sourceArray2[17] = (byte) 154;
    sourceArray2[9] = (byte) 136;
    sourceArray2[47] = (byte) 35;
    sourceArray2[20] = (byte) 107;
    sourceArray2[0] = (byte) 169;
    sourceArray2[29] = (byte) 228;
    sourceArray2[2] = (byte) 220;
    sourceArray2[24] = (byte) 192 /*0xC0*/;
    sourceArray2[12] = (byte) 4;
    sourceArray2[37] = (byte) 244;
    sourceArray2[40] = (byte) 179;
    sourceArray2[7] = (byte) 12;
    sourceArray2[21] = (byte) 102;
    sourceArray2[3] = (byte) 28;
    sourceArray2[25] = (byte) 143;
    sourceArray2[30] = (byte) 165;
    sourceArray2[19] = (byte) 244;
    sourceArray2[34] = (byte) 120;
    sourceArray2[35] = (byte) 239;
    sourceArray2[36] = (byte) 74;
    sourceArray2[38] = (byte) 44;
    sourceArray2[1] = (byte) 238;
    sourceArray2[39] = (byte) 251;
    sourceArray2[27] = (byte) 239;
    sourceArray2[23] = (byte) 141;
    sourceArray2[42] = (byte) 234;
    sourceArray2[44] = (byte) 123;
    sourceArray2[6] = (byte) 87;
    sourceArray2[31 /*0x1F*/] = (byte) 174;
    sourceArray2[46] = (byte) 234;
    sourceArray2[4] = (byte) 253;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[25];
    byte[] response2 = new byte[25];
    Array.Copy((Array) sc_13686.sspq, 555, (Array) numArray2, 0, 25);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 555, (Array) numArray2, 0, 25);
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

  internal static int ssp_appserver_13733(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 122;
    sourceArray1[5] = (byte) 82;
    sourceArray1[2] = (byte) 90;
    sourceArray1[3] = (byte) 36;
    sourceArray1[45] = (byte) 94;
    sourceArray1[8] = (byte) 246;
    sourceArray1[42] = (byte) 48 /*0x30*/;
    sourceArray1[46] = (byte) 254;
    sourceArray1[10] = (byte) 8;
    sourceArray1[9] = (byte) 161;
    sourceArray1[24] = (byte) 9;
    sourceArray1[36] = (byte) 169;
    sourceArray1[12] = (byte) 31 /*0x1F*/;
    sourceArray1[13] = (byte) 215;
    sourceArray1[14] = (byte) 29;
    sourceArray1[11] = (byte) 248;
    sourceArray1[0] = (byte) 195;
    sourceArray1[30] = (byte) 216;
    sourceArray1[18] = (byte) 224 /*0xE0*/;
    sourceArray1[23] = (byte) 50;
    sourceArray1[20] = (byte) 167;
    sourceArray1[21] = (byte) 145;
    sourceArray1[31 /*0x1F*/] = (byte) 36;
    sourceArray1[28] = (byte) 83;
    sourceArray1[19] = (byte) 87;
    sourceArray1[40] = (byte) 46;
    sourceArray1[26] = (byte) 75;
    sourceArray1[25] = (byte) 60;
    sourceArray1[47] = (byte) 113;
    sourceArray1[29] = (byte) 59;
    sourceArray1[27] = (byte) 231;
    sourceArray1[4] = (byte) 230;
    sourceArray1[32 /*0x20*/] = (byte) 65;
    sourceArray1[33] = (byte) 146;
    sourceArray1[22] = (byte) 43;
    sourceArray1[35] = (byte) 3;
    sourceArray1[1] = (byte) 203;
    sourceArray1[37] = (byte) 166;
    sourceArray1[34] = (byte) 252;
    sourceArray1[39] = (byte) 54;
    sourceArray1[6] = (byte) 102;
    sourceArray1[41] = (byte) 65;
    sourceArray1[7] = (byte) 86;
    sourceArray1[43] = (byte) 205;
    sourceArray1[44] = (byte) 66;
    sourceArray1[17] = (byte) 25;
    sourceArray1[15] = (byte) 241;
    sourceArray1[38] = (byte) 80 /*0x50*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 229,
      (byte) 65,
      (byte) 227,
      (byte) 140,
      (byte) 74,
      (byte) 118,
      (byte) 247,
      (byte) 155,
      (byte) 32 /*0x20*/,
      (byte) 137,
      (byte) 150,
      (byte) 234,
      (byte) 193,
      (byte) 116,
      (byte) 95,
      (byte) 134,
      (byte) 230,
      (byte) 26,
      (byte) 238,
      (byte) 84,
      (byte) 172,
      (byte) 129,
      (byte) 1,
      (byte) 197,
      (byte) 0,
      (byte) 82,
      (byte) 212,
      (byte) 116,
      (byte) 86,
      (byte) 55,
      (byte) 71,
      (byte) 179,
      (byte) 205,
      (byte) 201,
      (byte) 70,
      (byte) 75,
      (byte) 191,
      (byte) 141,
      (byte) 45,
      (byte) 171,
      (byte) 111,
      (byte) 5,
      (byte) 44,
      (byte) 247,
      (byte) 49,
      (byte) 173,
      (byte) 152,
      (byte) 166
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13734(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[32 /*0x20*/] = (byte) 144 /*0x90*/;
    sourceArray1[40] = (byte) 217;
    sourceArray1[30] = (byte) 236;
    sourceArray1[3] = (byte) 129;
    sourceArray1[4] = (byte) 41;
    sourceArray1[9] = (byte) 208 /*0xD0*/;
    sourceArray1[6] = (byte) 208 /*0xD0*/;
    sourceArray1[7] = (byte) 152;
    sourceArray1[17] = (byte) 125;
    sourceArray1[18] = (byte) 196;
    sourceArray1[5] = (byte) 11;
    sourceArray1[16 /*0x10*/] = (byte) 121;
    sourceArray1[12] = (byte) 111;
    sourceArray1[24] = (byte) 198;
    sourceArray1[25] = (byte) 72;
    sourceArray1[1] = (byte) 149;
    sourceArray1[15] = (byte) 254;
    sourceArray1[14] = (byte) 45;
    sourceArray1[21] = (byte) 252;
    sourceArray1[44] = (byte) 32 /*0x20*/;
    sourceArray1[20] = (byte) 55;
    sourceArray1[19] = (byte) 58;
    sourceArray1[22] = (byte) 17;
    sourceArray1[23] = (byte) 181;
    sourceArray1[26] = (byte) 34;
    sourceArray1[42] = (byte) 244;
    sourceArray1[27] = (byte) 122;
    sourceArray1[11] = (byte) 57;
    sourceArray1[28] = (byte) 121;
    sourceArray1[29] = (byte) 39;
    sourceArray1[2] = (byte) 78;
    sourceArray1[0] = (byte) 114;
    sourceArray1[47] = (byte) 57;
    sourceArray1[33] = (byte) 4;
    sourceArray1[34] = (byte) 203;
    sourceArray1[45] = (byte) 72;
    sourceArray1[8] = (byte) 155;
    sourceArray1[37] = (byte) 8;
    sourceArray1[38] = (byte) 19;
    sourceArray1[39] = (byte) 248;
    sourceArray1[10] = (byte) 67;
    sourceArray1[41] = (byte) 231;
    sourceArray1[36] = (byte) 49;
    sourceArray1[13] = (byte) 108;
    sourceArray1[31 /*0x1F*/] = (byte) 88;
    sourceArray1[43] = (byte) 250;
    sourceArray1[46] = (byte) 28;
    sourceArray1[35] = (byte) 68;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 212,
      (byte) 86,
      (byte) 89,
      (byte) 68,
      (byte) 138,
      (byte) 109,
      (byte) 186,
      (byte) 130,
      (byte) 162,
      (byte) 64 /*0x40*/,
      (byte) 253,
      (byte) 157,
      (byte) 139,
      (byte) 17,
      (byte) 168,
      (byte) 130,
      (byte) 251,
      (byte) 130,
      (byte) 18,
      (byte) 132,
      (byte) 123,
      (byte) 189,
      (byte) 24,
      (byte) 249,
      (byte) 159,
      (byte) 163,
      (byte) 195,
      (byte) 174,
      (byte) 75,
      (byte) 253,
      (byte) 230,
      (byte) 48 /*0x30*/,
      (byte) 228,
      (byte) 40,
      (byte) 61,
      (byte) 94,
      (byte) 189,
      (byte) 217,
      (byte) 236,
      (byte) 50,
      (byte) 69,
      (byte) 80 /*0x50*/,
      (byte) 93,
      (byte) 35,
      (byte) 139,
      (byte) 68,
      (byte) 218,
      (byte) 157
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[27];
    byte[] response2 = new byte[27];
    Array.Copy((Array) sc_13686.sspq, 580, (Array) numArray2, 0, 27);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 580, (Array) numArray2, 0, 27);
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

  internal static int ssp_appserver_13735(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 135,
      (byte) 134,
      (byte) 1,
      (byte) 96 /*0x60*/,
      (byte) 222,
      (byte) 122,
      (byte) 80 /*0x50*/,
      (byte) 96 /*0x60*/,
      (byte) 228,
      (byte) 15,
      (byte) 102,
      (byte) 164,
      (byte) 167,
      (byte) 116,
      (byte) 230,
      (byte) 83,
      (byte) 148,
      (byte) 15,
      (byte) 125,
      (byte) 17,
      (byte) 165,
      (byte) 113,
      (byte) 111,
      (byte) 38,
      (byte) 99,
      (byte) 97,
      (byte) 219,
      (byte) 76,
      (byte) 239,
      (byte) 174,
      (byte) 47,
      (byte) 86,
      (byte) 184,
      (byte) 229,
      (byte) 152,
      (byte) 231,
      (byte) 106,
      (byte) 163,
      (byte) 94,
      (byte) 58,
      (byte) 175,
      (byte) 125,
      (byte) 114,
      (byte) 2,
      (byte) 136,
      (byte) 45,
      (byte) 149
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 217,
      (byte) 20,
      (byte) 236,
      (byte) 86,
      (byte) 36,
      (byte) 148,
      byte.MaxValue,
      (byte) 242,
      (byte) 176 /*0xB0*/,
      (byte) 10,
      (byte) 123,
      (byte) 19,
      (byte) 98,
      (byte) 39,
      (byte) 0,
      (byte) 146,
      (byte) 141,
      (byte) 27,
      (byte) 230,
      (byte) 86,
      (byte) 63 /*0x3F*/,
      (byte) 192 /*0xC0*/,
      (byte) 10,
      (byte) 29,
      (byte) 242,
      (byte) 115,
      (byte) 15,
      (byte) 48 /*0x30*/,
      (byte) 214,
      (byte) 248,
      (byte) 29,
      (byte) 180,
      (byte) 106,
      (byte) 240 /*0xF0*/,
      (byte) 223,
      (byte) 244,
      (byte) 70,
      (byte) 249,
      (byte) 59,
      (byte) 188,
      (byte) 194,
      (byte) 126,
      (byte) 176 /*0xB0*/,
      (byte) 8,
      (byte) 50,
      (byte) 20,
      (byte) 40,
      (byte) 249
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13736(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 185,
      (byte) 169,
      (byte) 56,
      (byte) 21,
      (byte) 23,
      (byte) 219,
      (byte) 252,
      (byte) 125,
      (byte) 159,
      (byte) 3,
      (byte) 112 /*0x70*/,
      (byte) 53,
      (byte) 223,
      (byte) 16 /*0x10*/,
      (byte) 235,
      (byte) 193,
      (byte) 219,
      (byte) 254,
      (byte) 232,
      (byte) 241,
      (byte) 177,
      (byte) 66,
      (byte) 98,
      (byte) 39,
      (byte) 254,
      (byte) 108,
      (byte) 157,
      (byte) 53,
      (byte) 204,
      (byte) 120,
      (byte) 230,
      (byte) 26,
      (byte) 14,
      (byte) 26,
      (byte) 157,
      (byte) 14,
      (byte) 173,
      (byte) 131,
      (byte) 207,
      (byte) 143,
      (byte) 166,
      (byte) 224 /*0xE0*/,
      (byte) 155,
      (byte) 171,
      (byte) 13,
      (byte) 61,
      (byte) 34,
      (byte) 120
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 53;
    sourceArray2[39] = (byte) 57;
    sourceArray2[2] = (byte) 225;
    sourceArray2[35] = (byte) 182;
    sourceArray2[45] = (byte) 250;
    sourceArray2[11] = (byte) 78;
    sourceArray2[6] = (byte) 140;
    sourceArray2[21] = (byte) 192 /*0xC0*/;
    sourceArray2[27] = (byte) 212;
    sourceArray2[9] = (byte) 48 /*0x30*/;
    sourceArray2[10] = (byte) 51;
    sourceArray2[32 /*0x20*/] = (byte) 14;
    sourceArray2[12] = (byte) 48 /*0x30*/;
    sourceArray2[3] = (byte) 240 /*0xF0*/;
    sourceArray2[14] = (byte) 146;
    sourceArray2[7] = (byte) 57;
    sourceArray2[16 /*0x10*/] = (byte) 54;
    sourceArray2[25] = (byte) 49;
    sourceArray2[18] = (byte) 251;
    sourceArray2[20] = (byte) 91;
    sourceArray2[34] = (byte) 86;
    sourceArray2[26] = (byte) 116;
    sourceArray2[22] = (byte) 75;
    sourceArray2[23] = (byte) 45;
    sourceArray2[40] = (byte) 173;
    sourceArray2[17] = (byte) 212;
    sourceArray2[38] = (byte) 187;
    sourceArray2[42] = (byte) 170;
    sourceArray2[30] = (byte) 166;
    sourceArray2[29] = (byte) 225;
    sourceArray2[33] = (byte) 33;
    sourceArray2[24] = (byte) 80 /*0x50*/;
    sourceArray2[8] = (byte) 195;
    sourceArray2[5] = (byte) 209;
    sourceArray2[19] = (byte) 2;
    sourceArray2[4] = (byte) 94;
    sourceArray2[0] = (byte) 158;
    sourceArray2[37] = (byte) 121;
    sourceArray2[36] = (byte) 92;
    sourceArray2[15] = (byte) 89;
    sourceArray2[31 /*0x1F*/] = (byte) 189;
    sourceArray2[28] = (byte) 155;
    sourceArray2[1] = (byte) 149;
    sourceArray2[43] = (byte) 169;
    sourceArray2[44] = (byte) 223;
    sourceArray2[13] = (byte) 52;
    sourceArray2[46] = (byte) 137;
    sourceArray2[47] = (byte) 180;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13737(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[32 /*0x20*/] = (byte) 99;
    sourceArray1[15] = (byte) 212;
    sourceArray1[2] = (byte) 101;
    sourceArray1[3] = (byte) 191;
    sourceArray1[4] = (byte) 46;
    sourceArray1[13] = (byte) 96 /*0x60*/;
    sourceArray1[6] = (byte) 147;
    sourceArray1[41] = (byte) 253;
    sourceArray1[8] = (byte) 145;
    sourceArray1[36] = (byte) 142;
    sourceArray1[1] = (byte) 6;
    sourceArray1[29] = (byte) 160 /*0xA0*/;
    sourceArray1[12] = (byte) 84;
    sourceArray1[23] = (byte) 21;
    sourceArray1[14] = (byte) 25;
    sourceArray1[46] = (byte) 23;
    sourceArray1[24] = (byte) 119;
    sourceArray1[17] = (byte) 51;
    sourceArray1[18] = (byte) 156;
    sourceArray1[42] = (byte) 38;
    sourceArray1[26] = (byte) 10;
    sourceArray1[38] = (byte) 196;
    sourceArray1[22] = (byte) 75;
    sourceArray1[19] = (byte) 39;
    sourceArray1[47] = (byte) 195;
    sourceArray1[33] = (byte) 171;
    sourceArray1[45] = (byte) 166;
    sourceArray1[35] = (byte) 30;
    sourceArray1[27] = (byte) 7;
    sourceArray1[44] = (byte) 54;
    sourceArray1[34] = (byte) 236;
    sourceArray1[25] = (byte) 235;
    sourceArray1[9] = (byte) 207;
    sourceArray1[30] = (byte) 48 /*0x30*/;
    sourceArray1[16 /*0x10*/] = (byte) 184;
    sourceArray1[7] = (byte) 99;
    sourceArray1[31 /*0x1F*/] = (byte) 41;
    sourceArray1[37] = (byte) 57;
    sourceArray1[20] = (byte) 198;
    sourceArray1[39] = (byte) 72;
    sourceArray1[40] = (byte) 154;
    sourceArray1[0] = (byte) 93;
    sourceArray1[5] = (byte) 55;
    sourceArray1[43] = (byte) 86;
    sourceArray1[11] = (byte) 245;
    sourceArray1[28] = (byte) 108;
    sourceArray1[10] = (byte) 144 /*0x90*/;
    sourceArray1[21] = (byte) 12;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 94;
    sourceArray2[1] = (byte) 213;
    sourceArray2[22] = (byte) 247;
    sourceArray2[3] = (byte) 224 /*0xE0*/;
    sourceArray2[4] = (byte) 0;
    sourceArray2[5] = (byte) 66;
    sourceArray2[30] = (byte) 11;
    sourceArray2[7] = (byte) 168;
    sourceArray2[45] = (byte) 109;
    sourceArray2[8] = (byte) 55;
    sourceArray2[10] = (byte) 31 /*0x1F*/;
    sourceArray2[11] = (byte) 78;
    sourceArray2[12] = (byte) 167;
    sourceArray2[41] = (byte) 172;
    sourceArray2[14] = (byte) 227;
    sourceArray2[15] = (byte) 139;
    sourceArray2[16 /*0x10*/] = (byte) 8;
    sourceArray2[40] = (byte) 71;
    sourceArray2[18] = (byte) 254;
    sourceArray2[27] = (byte) 137;
    sourceArray2[20] = (byte) 35;
    sourceArray2[32 /*0x20*/] = (byte) 234;
    sourceArray2[46] = (byte) 4;
    sourceArray2[43] = (byte) 138;
    sourceArray2[24] = (byte) 40;
    sourceArray2[25] = (byte) 163;
    sourceArray2[26] = (byte) 194;
    sourceArray2[21] = (byte) 99;
    sourceArray2[28] = (byte) 148;
    sourceArray2[19] = (byte) 172;
    sourceArray2[33] = (byte) 17;
    sourceArray2[44] = (byte) 19;
    sourceArray2[9] = (byte) 106;
    sourceArray2[36] = (byte) 196;
    sourceArray2[35] = (byte) 131;
    sourceArray2[31 /*0x1F*/] = (byte) 144 /*0x90*/;
    sourceArray2[39] = (byte) 41;
    sourceArray2[23] = (byte) 175;
    sourceArray2[17] = (byte) 165;
    sourceArray2[2] = (byte) 33;
    sourceArray2[29] = (byte) 202;
    sourceArray2[0] = (byte) 130;
    sourceArray2[42] = (byte) 143;
    sourceArray2[13] = (byte) 145;
    sourceArray2[34] = (byte) 91;
    sourceArray2[6] = (byte) 93;
    sourceArray2[47] = (byte) 157;
    sourceArray2[38] = (byte) 99;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[46];
    byte[] response2 = new byte[46];
    Array.Copy((Array) sc_13686.sspq, 607, (Array) numArray2, 0, 46);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 607, (Array) numArray2, 0, 46);
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

  internal static string ssp_appserver_13738()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[99];
      byte[] numArray2 = new byte[55]
      {
        (byte) 54,
        (byte) 55,
        (byte) 76,
        (byte) 20,
        (byte) 120,
        (byte) 21,
        (byte) 109,
        (byte) 194,
        (byte) 7,
        (byte) 67,
        (byte) 42,
        (byte) 123,
        (byte) 210,
        (byte) 114,
        (byte) 10,
        (byte) 103,
        (byte) 173,
        (byte) 205,
        (byte) 171,
        (byte) 130,
        (byte) 91,
        (byte) 9,
        (byte) 92,
        (byte) 90,
        (byte) 113,
        (byte) 115,
        (byte) 83,
        (byte) 239,
        (byte) 81,
        (byte) 200,
        (byte) 111,
        (byte) 59,
        (byte) 22,
        (byte) 214,
        (byte) 93,
        (byte) 92,
        (byte) 161,
        (byte) 97,
        (byte) 47,
        (byte) 227,
        (byte) 204,
        (byte) 78,
        (byte) 211,
        (byte) 194,
        (byte) 0,
        (byte) 213,
        (byte) 114,
        (byte) 194,
        (byte) 102,
        (byte) 0,
        (byte) 148,
        (byte) 179,
        (byte) 65,
        (byte) 93,
        (byte) 104
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 141,
        (byte) 252,
        (byte) 54,
        (byte) 89,
        (byte) 50,
        (byte) 49,
        (byte) 26,
        (byte) 105,
        (byte) 237,
        (byte) 43,
        (byte) 240 /*0xF0*/,
        (byte) 108,
        (byte) 195,
        (byte) 192 /*0xC0*/,
        (byte) 30,
        (byte) 188,
        (byte) 155,
        (byte) 131,
        (byte) 42,
        (byte) 186,
        (byte) 44,
        (byte) 227,
        (byte) 87,
        (byte) 203,
        (byte) 227,
        (byte) 150,
        (byte) 157,
        (byte) 207,
        (byte) 170,
        (byte) 227,
        (byte) 220,
        (byte) 226,
        (byte) 62,
        (byte) 183,
        (byte) 179,
        (byte) 53,
        (byte) 59,
        (byte) 170,
        (byte) 88,
        (byte) 131,
        (byte) 67,
        (byte) 110,
        (byte) 67,
        (byte) 77,
        (byte) 56,
        (byte) 156,
        (byte) 162,
        (byte) 246,
        (byte) 215,
        (byte) 37,
        (byte) 129,
        (byte) 113,
        (byte) 52,
        (byte) 95,
        (byte) 155
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[44]
      {
        (byte) 241,
        (byte) 162,
        (byte) 5,
        (byte) 194,
        (byte) 125,
        (byte) 113,
        (byte) 62,
        (byte) 212,
        (byte) 239,
        (byte) 101,
        (byte) 252,
        (byte) 21,
        (byte) 173,
        (byte) 197,
        (byte) 76,
        (byte) 157,
        (byte) 200,
        (byte) 182,
        (byte) 12,
        (byte) 53,
        (byte) 153,
        (byte) 90,
        (byte) 156,
        (byte) 0,
        (byte) 144 /*0x90*/,
        (byte) 137,
        (byte) 81,
        (byte) 52,
        (byte) 187,
        (byte) 249,
        (byte) 184,
        (byte) 110,
        (byte) 48 /*0x30*/,
        (byte) 129,
        (byte) 176 /*0xB0*/,
        (byte) 190,
        (byte) 42,
        (byte) 33,
        (byte) 79,
        (byte) 221,
        (byte) 149,
        (byte) 238,
        (byte) 16 /*0x10*/,
        (byte) 112 /*0x70*/
      };
      byte[] numArray5 = new byte[44]
      {
        (byte) 157,
        (byte) 183,
        (byte) 9,
        (byte) 95,
        (byte) 11,
        (byte) 229,
        (byte) 227,
        (byte) 29,
        (byte) 193,
        (byte) 124,
        (byte) 125,
        (byte) 24,
        (byte) 199,
        (byte) 225,
        (byte) 140,
        (byte) 132,
        (byte) 207,
        (byte) 142,
        (byte) 164,
        (byte) 91,
        (byte) 107,
        (byte) 82,
        (byte) 136,
        (byte) 178,
        (byte) 90,
        (byte) 172,
        (byte) 203,
        (byte) 83,
        (byte) 192 /*0xC0*/,
        (byte) 117,
        (byte) 135,
        (byte) 238,
        (byte) 75,
        (byte) 189,
        (byte) 211,
        (byte) 24,
        (byte) 62,
        (byte) 205,
        (byte) 59,
        (byte) 175,
        (byte) 160 /*0xA0*/,
        (byte) 130,
        (byte) 216,
        (byte) 102
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[99];
    byte[] numArray7 = new byte[55];
    numArray7[34] = (byte) 22;
    numArray7[12] = (byte) 243;
    numArray7[44] = (byte) 38;
    numArray7[9] = (byte) 5;
    numArray7[33] = (byte) 208 /*0xD0*/;
    numArray7[11] = (byte) 49;
    numArray7[17] = (byte) 172;
    numArray7[45] = (byte) 239;
    numArray7[22] = (byte) 141;
    numArray7[29] = (byte) 73;
    numArray7[10] = (byte) 4;
    numArray7[37] = (byte) 127 /*0x7F*/;
    numArray7[35] = (byte) 235;
    numArray7[2] = (byte) 21;
    numArray7[1] = (byte) 72;
    numArray7[49] = (byte) 214;
    numArray7[16 /*0x10*/] = (byte) 246;
    numArray7[21] = (byte) 21;
    numArray7[13] = (byte) 31 /*0x1F*/;
    numArray7[19] = (byte) 229;
    numArray7[20] = (byte) 43;
    numArray7[39] = (byte) 18;
    numArray7[14] = (byte) 10;
    numArray7[53] = (byte) 199;
    numArray7[24] = (byte) 166;
    numArray7[25] = (byte) 102;
    numArray7[54] = (byte) 140;
    numArray7[0] = (byte) 58;
    numArray7[18] = (byte) 142;
    numArray7[23] = (byte) 115;
    numArray7[30] = (byte) 44;
    numArray7[31 /*0x1F*/] = (byte) 137;
    numArray7[32 /*0x20*/] = (byte) 227;
    numArray7[6] = (byte) 248;
    numArray7[28] = (byte) 171;
    numArray7[7] = (byte) 65;
    numArray7[36] = (byte) 166;
    numArray7[15] = (byte) 24;
    numArray7[5] = (byte) 91;
    numArray7[4] = (byte) 167;
    numArray7[50] = (byte) 26;
    numArray7[8] = (byte) 150;
    numArray7[38] = (byte) 16 /*0x10*/;
    numArray7[43] = (byte) 185;
    numArray7[42] = (byte) 25;
    numArray7[41] = (byte) 64 /*0x40*/;
    numArray7[26] = (byte) 47;
    numArray7[47] = (byte) 189;
    numArray7[48 /*0x30*/] = (byte) 139;
    numArray7[27] = (byte) 160 /*0xA0*/;
    numArray7[40] = (byte) 55;
    numArray7[51] = (byte) 195;
    numArray7[52] = (byte) 232;
    numArray7[3] = (byte) 121;
    numArray7[46] = (byte) 91;
    byte[] numArray8 = new byte[55];
    numArray8[53] = (byte) 2;
    numArray8[0] = (byte) 188;
    numArray8[41] = (byte) 5;
    numArray8[3] = (byte) 53;
    numArray8[11] = (byte) 27;
    numArray8[4] = (byte) 153;
    numArray8[43] = (byte) 174;
    numArray8[7] = (byte) 125;
    numArray8[8] = (byte) 86;
    numArray8[52] = (byte) 90;
    numArray8[10] = (byte) 32 /*0x20*/;
    numArray8[33] = (byte) 43;
    numArray8[2] = (byte) 22;
    numArray8[17] = (byte) 116;
    numArray8[25] = (byte) 168;
    numArray8[15] = (byte) 237;
    numArray8[50] = (byte) 169;
    numArray8[6] = (byte) 219;
    numArray8[18] = (byte) 143;
    numArray8[19] = (byte) 124;
    numArray8[1] = (byte) 254;
    numArray8[20] = (byte) 30;
    numArray8[22] = (byte) 16 /*0x10*/;
    numArray8[21] = (byte) 111;
    numArray8[45] = (byte) 166;
    numArray8[13] = (byte) 38;
    numArray8[27] = (byte) 222;
    numArray8[26] = (byte) 95;
    numArray8[28] = (byte) 130;
    numArray8[29] = (byte) 98;
    numArray8[30] = (byte) 19;
    numArray8[24] = (byte) 225;
    numArray8[32 /*0x20*/] = (byte) 79;
    numArray8[47] = (byte) 116;
    numArray8[34] = (byte) 216;
    numArray8[35] = (byte) 64 /*0x40*/;
    numArray8[16 /*0x10*/] = (byte) 168;
    numArray8[37] = (byte) 33;
    numArray8[44] = (byte) 146;
    numArray8[39] = (byte) 110;
    numArray8[40] = (byte) 115;
    numArray8[12] = (byte) 43;
    numArray8[38] = (byte) 135;
    numArray8[9] = (byte) 100;
    numArray8[23] = (byte) 248;
    numArray8[14] = (byte) 84;
    numArray8[46] = (byte) 143;
    numArray8[51] = (byte) 179;
    numArray8[48 /*0x30*/] = (byte) 60;
    numArray8[49] = (byte) 239;
    numArray8[31 /*0x1F*/] = (byte) 65;
    numArray8[54] = (byte) 16 /*0x10*/;
    numArray8[36] = (byte) 17;
    numArray8[42] = (byte) 250;
    numArray8[5] = (byte) 166;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[44]
    {
      (byte) 179,
      (byte) 4,
      (byte) 100,
      (byte) 252,
      (byte) 154,
      (byte) 80 /*0x50*/,
      (byte) 146,
      (byte) 47,
      (byte) 163,
      (byte) 49,
      (byte) 59,
      (byte) 78,
      (byte) 163,
      (byte) 191,
      (byte) 56,
      (byte) 50,
      (byte) 104,
      (byte) 49,
      (byte) 89,
      (byte) 129,
      (byte) 134,
      (byte) 207,
      (byte) 168,
      (byte) 118,
      (byte) 33,
      (byte) 41,
      (byte) 122,
      (byte) 177,
      (byte) 229,
      (byte) 93,
      (byte) 181,
      (byte) 67,
      (byte) 208 /*0xD0*/,
      (byte) 87,
      (byte) 251,
      (byte) 140,
      (byte) 53,
      (byte) 48 /*0x30*/,
      (byte) 1,
      (byte) 151,
      (byte) 27,
      (byte) 62,
      (byte) 46,
      (byte) 60
    };
    byte[] numArray10 = new byte[44]
    {
      (byte) 72,
      (byte) 25,
      (byte) 33,
      (byte) 204,
      (byte) 128 /*0x80*/,
      (byte) 220,
      (byte) 180,
      (byte) 91,
      (byte) 41,
      (byte) 32 /*0x20*/,
      (byte) 165,
      (byte) 166,
      (byte) 96 /*0x60*/,
      (byte) 101,
      (byte) 68,
      (byte) 27,
      (byte) 160 /*0xA0*/,
      (byte) 67,
      (byte) 106,
      (byte) 166,
      (byte) 225,
      (byte) 215,
      (byte) 98,
      (byte) 214,
      (byte) 126,
      (byte) 55,
      (byte) 208 /*0xD0*/,
      (byte) 94,
      (byte) 190,
      (byte) 72,
      (byte) 156,
      (byte) 211,
      (byte) 110,
      (byte) 103,
      (byte) 213,
      (byte) 9,
      (byte) 222,
      (byte) 248,
      (byte) 154,
      (byte) 106,
      (byte) 229,
      (byte) 20,
      (byte) 253,
      (byte) 95
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 44);
    for (int index = 0; index < 44; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_13739(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 41,
      (byte) 111,
      (byte) 26,
      (byte) 5,
      (byte) 119,
      (byte) 107,
      (byte) 228,
      (byte) 92,
      (byte) 237,
      (byte) 218,
      (byte) 113,
      (byte) 213,
      (byte) 225,
      (byte) 73,
      (byte) 69,
      (byte) 123,
      (byte) 247,
      (byte) 126,
      (byte) 75,
      (byte) 195,
      (byte) 171,
      (byte) 252,
      (byte) 55,
      (byte) 48 /*0x30*/,
      (byte) 193,
      (byte) 101,
      (byte) 71,
      (byte) 160 /*0xA0*/,
      (byte) 107,
      (byte) 87,
      (byte) 211,
      (byte) 77,
      (byte) 67,
      (byte) 15,
      (byte) 188,
      (byte) 237,
      (byte) 155,
      (byte) 61,
      (byte) 89,
      (byte) 236,
      (byte) 22,
      (byte) 47,
      (byte) 71,
      (byte) 85,
      (byte) 143,
      (byte) 194,
      (byte) 58,
      (byte) 245
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 160 /*0xA0*/,
      (byte) 237,
      (byte) 54,
      (byte) 29,
      (byte) 3,
      (byte) 224 /*0xE0*/,
      (byte) 244,
      (byte) 161,
      (byte) 199,
      (byte) 243,
      (byte) 239,
      (byte) 129,
      (byte) 49,
      (byte) 121,
      (byte) 18,
      (byte) 238,
      (byte) 239,
      (byte) 215,
      (byte) 27,
      (byte) 119,
      (byte) 126,
      (byte) 32 /*0x20*/,
      (byte) 197,
      (byte) 177,
      (byte) 67,
      (byte) 21,
      (byte) 90,
      (byte) 126,
      (byte) 208 /*0xD0*/,
      (byte) 195,
      (byte) 190,
      (byte) 237,
      (byte) 41,
      (byte) 35,
      (byte) 102,
      (byte) 67,
      (byte) 136,
      (byte) 191,
      (byte) 72,
      (byte) 201,
      (byte) 126,
      (byte) 71,
      (byte) 252,
      (byte) 198,
      (byte) 85,
      (byte) 99,
      (byte) 141,
      (byte) 34
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[47];
    byte[] response2 = new byte[47];
    Array.Copy((Array) sc_13686.sspq, 653, (Array) numArray2, 0, 47);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 653, (Array) numArray2, 0, 47);
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

  internal static string ssp_appserver_13740()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[84];
      byte[] numArray2 = new byte[55]
      {
        (byte) 47,
        (byte) 104,
        (byte) 104,
        (byte) 145,
        (byte) 224 /*0xE0*/,
        (byte) 110,
        (byte) 170,
        (byte) 48 /*0x30*/,
        (byte) 198,
        (byte) 121,
        (byte) 119,
        (byte) 86,
        (byte) 72,
        (byte) 68,
        (byte) 230,
        (byte) 209,
        (byte) 140,
        (byte) 71,
        (byte) 102,
        (byte) 4,
        (byte) 103,
        (byte) 193,
        (byte) 183,
        (byte) 252,
        (byte) 162,
        (byte) 47,
        (byte) 191,
        (byte) 221,
        (byte) 150,
        (byte) 218,
        (byte) 61,
        (byte) 2,
        (byte) 147,
        (byte) 54,
        (byte) 207,
        (byte) 194,
        (byte) 234,
        (byte) 38,
        (byte) 201,
        (byte) 193,
        (byte) 88,
        (byte) 140,
        (byte) 10,
        (byte) 159,
        (byte) 24,
        (byte) 95,
        (byte) 105,
        (byte) 180,
        (byte) 97,
        (byte) 247,
        (byte) 69,
        (byte) 247,
        (byte) 75,
        (byte) 73,
        (byte) 3
      };
      byte[] numArray3 = new byte[55];
      numArray3[44] = (byte) 126;
      numArray3[11] = (byte) 42;
      numArray3[0] = (byte) 84;
      numArray3[21] = (byte) 39;
      numArray3[4] = (byte) 199;
      numArray3[1] = (byte) 132;
      numArray3[6] = (byte) 50;
      numArray3[49] = (byte) 137;
      numArray3[8] = (byte) 70;
      numArray3[7] = (byte) 220;
      numArray3[50] = (byte) 126;
      numArray3[35] = (byte) 39;
      numArray3[12] = (byte) 243;
      numArray3[51] = (byte) 69;
      numArray3[14] = byte.MaxValue;
      numArray3[15] = (byte) 109;
      numArray3[19] = (byte) 247;
      numArray3[13] = (byte) 11;
      numArray3[18] = (byte) 198;
      numArray3[17] = (byte) 141;
      numArray3[20] = (byte) 210;
      numArray3[22] = (byte) 131;
      numArray3[29] = (byte) 183;
      numArray3[23] = (byte) 80 /*0x50*/;
      numArray3[2] = (byte) 184;
      numArray3[25] = (byte) 14;
      numArray3[26] = (byte) 158;
      numArray3[52] = (byte) 234;
      numArray3[16 /*0x10*/] = (byte) 42;
      numArray3[37] = (byte) 225;
      numArray3[30] = (byte) 34;
      numArray3[31 /*0x1F*/] = (byte) 124;
      numArray3[27] = (byte) 153;
      numArray3[3] = (byte) 171;
      numArray3[9] = (byte) 52;
      numArray3[28] = (byte) 109;
      numArray3[36] = (byte) 165;
      numArray3[39] = (byte) 179;
      numArray3[34] = (byte) 134;
      numArray3[42] = (byte) 234;
      numArray3[40] = (byte) 52;
      numArray3[41] = (byte) 94;
      numArray3[43] = (byte) 53;
      numArray3[24] = (byte) 82;
      numArray3[33] = (byte) 16 /*0x10*/;
      numArray3[5] = (byte) 181;
      numArray3[46] = (byte) 155;
      numArray3[47] = (byte) 113;
      numArray3[48 /*0x30*/] = (byte) 243;
      numArray3[32 /*0x20*/] = (byte) 166;
      numArray3[10] = (byte) 22;
      numArray3[38] = (byte) 155;
      numArray3[45] = (byte) 154;
      numArray3[53] = (byte) 227;
      numArray3[54] = (byte) 229;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[29]
      {
        (byte) 136,
        (byte) 172,
        (byte) 235,
        (byte) 163,
        (byte) 80 /*0x50*/,
        (byte) 250,
        (byte) 87,
        (byte) 246,
        (byte) 189,
        (byte) 120,
        (byte) 224 /*0xE0*/,
        (byte) 20,
        (byte) 19,
        (byte) 104,
        (byte) 47,
        (byte) 101,
        (byte) 146,
        (byte) 32 /*0x20*/,
        (byte) 231,
        (byte) 152,
        (byte) 180,
        (byte) 63 /*0x3F*/,
        (byte) 165,
        (byte) 111,
        (byte) 203,
        (byte) 63 /*0x3F*/,
        (byte) 143,
        (byte) 181,
        (byte) 204
      };
      byte[] numArray5 = new byte[29];
      numArray5[17] = (byte) 93;
      numArray5[1] = (byte) 192 /*0xC0*/;
      numArray5[11] = (byte) 190;
      numArray5[4] = (byte) 204;
      numArray5[6] = (byte) 27;
      numArray5[5] = (byte) 130;
      numArray5[3] = (byte) 5;
      numArray5[28] = (byte) 24;
      numArray5[8] = (byte) 16 /*0x10*/;
      numArray5[7] = (byte) 97;
      numArray5[10] = (byte) 51;
      numArray5[21] = (byte) 5;
      numArray5[12] = (byte) 156;
      numArray5[13] = (byte) 76;
      numArray5[14] = (byte) 164;
      numArray5[15] = (byte) 4;
      numArray5[20] = (byte) 12;
      numArray5[0] = (byte) 217;
      numArray5[24] = (byte) 204;
      numArray5[16 /*0x10*/] = (byte) 61;
      numArray5[18] = (byte) 78;
      numArray5[23] = (byte) 71;
      numArray5[22] = (byte) 40;
      numArray5[9] = (byte) 10;
      numArray5[2] = (byte) 58;
      numArray5[25] = (byte) 102;
      numArray5[26] = (byte) 71;
      numArray5[27] = (byte) 155;
      numArray5[19] = (byte) 173;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_13686.sspq, 700, (Array) numArray6, 0, 36);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13686.sspr, 700, (Array) numArray6, 0, 36);
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
    byte[] numArray7 = new byte[84];
    byte[] numArray8 = new byte[55]
    {
      (byte) 86,
      (byte) 159,
      (byte) 192 /*0xC0*/,
      (byte) 142,
      (byte) 53,
      (byte) 9,
      (byte) 220,
      (byte) 241,
      (byte) 152,
      (byte) 83,
      (byte) 78,
      (byte) 155,
      (byte) 240 /*0xF0*/,
      (byte) 132,
      (byte) 131,
      (byte) 54,
      (byte) 251,
      (byte) 131,
      (byte) 154,
      (byte) 114,
      (byte) 174,
      (byte) 159,
      (byte) 17,
      (byte) 224 /*0xE0*/,
      (byte) 6,
      (byte) 198,
      (byte) 32 /*0x20*/,
      (byte) 135,
      (byte) 137,
      (byte) 187,
      (byte) 96 /*0x60*/,
      (byte) 171,
      (byte) 5,
      (byte) 30,
      (byte) 43,
      (byte) 89,
      (byte) 224 /*0xE0*/,
      (byte) 134,
      (byte) 177,
      (byte) 253,
      (byte) 219,
      (byte) 11,
      (byte) 148,
      (byte) 115,
      (byte) 59,
      (byte) 248,
      (byte) 188,
      (byte) 0,
      (byte) 45,
      (byte) 98,
      (byte) 45,
      (byte) 10,
      (byte) 138,
      (byte) 208 /*0xD0*/,
      (byte) 139
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 105,
      (byte) 29,
      (byte) 9,
      (byte) 45,
      (byte) 199,
      (byte) 114,
      (byte) 128 /*0x80*/,
      (byte) 19,
      (byte) 99,
      (byte) 26,
      (byte) 55,
      (byte) 44,
      (byte) 25,
      (byte) 80 /*0x50*/,
      (byte) 5,
      (byte) 37,
      (byte) 243,
      (byte) 106,
      (byte) 118,
      (byte) 210,
      (byte) 21,
      (byte) 9,
      (byte) 162,
      (byte) 36,
      (byte) 34,
      (byte) 244,
      (byte) 205,
      (byte) 248,
      (byte) 160 /*0xA0*/,
      (byte) 61,
      (byte) 38,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 101,
      (byte) 240 /*0xF0*/,
      (byte) 61,
      (byte) 23,
      (byte) 93,
      (byte) 102,
      (byte) 138,
      (byte) 229,
      (byte) 240 /*0xF0*/,
      (byte) 24,
      (byte) 225,
      (byte) 209,
      (byte) 227,
      (byte) 54,
      (byte) 116,
      (byte) 116,
      (byte) 154,
      (byte) 92,
      (byte) 193,
      (byte) 244,
      (byte) 17,
      (byte) 180
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[29]
    {
      (byte) 223,
      (byte) 136,
      (byte) 111,
      (byte) 232,
      (byte) 85,
      (byte) 104,
      (byte) 221,
      (byte) 242,
      (byte) 163,
      (byte) 149,
      (byte) 210,
      (byte) 88,
      (byte) 42,
      (byte) 85,
      (byte) 116,
      (byte) 210,
      (byte) 157,
      (byte) 151,
      (byte) 163,
      (byte) 11,
      (byte) 157,
      (byte) 165,
      (byte) 13,
      (byte) 10,
      (byte) 32 /*0x20*/,
      (byte) 90,
      (byte) 38,
      (byte) 142,
      (byte) 86
    };
    byte[] numArray11 = new byte[29];
    numArray11[27] = (byte) 196;
    numArray11[5] = (byte) 169;
    numArray11[2] = (byte) 172;
    numArray11[3] = (byte) 50;
    numArray11[4] = (byte) 82;
    numArray11[23] = (byte) 38;
    numArray11[22] = (byte) 80 /*0x50*/;
    numArray11[7] = (byte) 129;
    numArray11[8] = (byte) 82;
    numArray11[0] = (byte) 216;
    numArray11[26] = (byte) 113;
    numArray11[11] = (byte) 159;
    numArray11[12] = (byte) 90;
    numArray11[13] = (byte) 134;
    numArray11[25] = (byte) 122;
    numArray11[15] = (byte) 102;
    numArray11[28] = (byte) 224 /*0xE0*/;
    numArray11[10] = (byte) 19;
    numArray11[18] = (byte) 138;
    numArray11[21] = (byte) 196;
    numArray11[20] = (byte) 155;
    numArray11[14] = (byte) 137;
    numArray11[16 /*0x10*/] = (byte) 72;
    numArray11[19] = (byte) 178;
    numArray11[24] = (byte) 221;
    numArray11[17] = (byte) 132;
    numArray11[1] = (byte) 89;
    numArray11[6] = (byte) 181;
    numArray11[9] = (byte) 7;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 29);
    for (int index = 0; index < 29; ++index)
      numArray7[index + 55] ^= numArray11[index];
    byte[] numArray12 = new byte[52];
    byte[] response1 = new byte[52];
    Array.Copy((Array) sc_13686.sspq, 736, (Array) numArray12, 0, 52);
    key.Query(true, 335, numArray12, response1);
    Array.Copy((Array) sc_13686.sspr, 736, (Array) numArray12, 0, 52);
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

  internal static int ssp_appserver_13741(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 121,
      (byte) 124,
      (byte) 80 /*0x50*/,
      (byte) 99,
      (byte) 150,
      (byte) 28,
      (byte) 131,
      (byte) 140,
      (byte) 242,
      (byte) 178,
      (byte) 60,
      (byte) 134,
      (byte) 159,
      (byte) 234,
      (byte) 55,
      (byte) 246,
      (byte) 167,
      (byte) 118,
      (byte) 182,
      (byte) 179,
      (byte) 96 /*0x60*/,
      (byte) 146,
      (byte) 118,
      (byte) 205,
      (byte) 213,
      (byte) 192 /*0xC0*/,
      (byte) 111,
      (byte) 8,
      (byte) 154,
      (byte) 179,
      (byte) 152,
      (byte) 254,
      (byte) 231,
      (byte) 56,
      (byte) 238,
      (byte) 104,
      (byte) 197,
      (byte) 159,
      (byte) 19,
      (byte) 166,
      (byte) 32 /*0x20*/,
      (byte) 44,
      (byte) 80 /*0x50*/,
      (byte) 245,
      (byte) 40,
      (byte) 160 /*0xA0*/,
      (byte) 73,
      (byte) 109
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 94,
      (byte) 129,
      (byte) 147,
      (byte) 151,
      (byte) 240 /*0xF0*/,
      (byte) 225,
      (byte) 188,
      (byte) 246,
      (byte) 30,
      (byte) 198,
      (byte) 136,
      (byte) 151,
      (byte) 187,
      (byte) 228,
      (byte) 210,
      (byte) 161,
      (byte) 85,
      (byte) 206,
      (byte) 150,
      (byte) 203,
      (byte) 202,
      (byte) 235,
      (byte) 129,
      (byte) 59,
      (byte) 222,
      (byte) 22,
      (byte) 38,
      (byte) 32 /*0x20*/,
      (byte) 20,
      (byte) 194,
      (byte) 215,
      (byte) 176 /*0xB0*/,
      (byte) 72,
      (byte) 112 /*0x70*/,
      (byte) 120,
      (byte) 188,
      (byte) 69,
      (byte) 156,
      (byte) 49,
      (byte) 202,
      (byte) 115,
      (byte) 149,
      (byte) 33,
      byte.MaxValue,
      (byte) 78,
      (byte) 13,
      (byte) 174,
      (byte) 167
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13742()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[98];
      byte[] numArray2 = new byte[55];
      numArray2[12] = (byte) 26;
      numArray2[1] = (byte) 240 /*0xF0*/;
      numArray2[22] = (byte) 146;
      numArray2[24] = (byte) 66;
      numArray2[35] = (byte) 247;
      numArray2[28] = (byte) 119;
      numArray2[6] = (byte) 141;
      numArray2[0] = (byte) 152;
      numArray2[34] = (byte) 101;
      numArray2[15] = (byte) 102;
      numArray2[10] = (byte) 242;
      numArray2[11] = (byte) 63 /*0x3F*/;
      numArray2[17] = (byte) 207;
      numArray2[52] = (byte) 5;
      numArray2[20] = (byte) 98;
      numArray2[14] = (byte) 158;
      numArray2[7] = (byte) 213;
      numArray2[46] = (byte) 219;
      numArray2[18] = (byte) 49;
      numArray2[21] = (byte) 15;
      numArray2[2] = (byte) 199;
      numArray2[9] = (byte) 123;
      numArray2[13] = (byte) 62;
      numArray2[45] = (byte) 16 /*0x10*/;
      numArray2[40] = (byte) 18;
      numArray2[25] = (byte) 128 /*0x80*/;
      numArray2[26] = (byte) 165;
      numArray2[5] = (byte) 130;
      numArray2[37] = (byte) 8;
      numArray2[29] = (byte) 2;
      numArray2[4] = (byte) 5;
      numArray2[31 /*0x1F*/] = (byte) 99;
      numArray2[32 /*0x20*/] = (byte) 138;
      numArray2[33] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 252;
      numArray2[41] = (byte) 62;
      numArray2[36] = (byte) 113;
      numArray2[8] = (byte) 172;
      numArray2[38] = (byte) 98;
      numArray2[54] = (byte) 108;
      numArray2[42] = (byte) 250;
      numArray2[27] = (byte) 53;
      numArray2[44] = (byte) 58;
      numArray2[43] = (byte) 165;
      numArray2[53] = (byte) 32 /*0x20*/;
      numArray2[39] = (byte) 67;
      numArray2[3] = (byte) 230;
      numArray2[30] = (byte) 108;
      numArray2[48 /*0x30*/] = (byte) 1;
      numArray2[49] = (byte) 226;
      numArray2[50] = (byte) 113;
      numArray2[51] = (byte) 203;
      numArray2[23] = (byte) 52;
      numArray2[19] = (byte) 7;
      numArray2[47] = (byte) 234;
      byte[] numArray3 = new byte[55]
      {
        (byte) 56,
        (byte) 25,
        (byte) 172,
        (byte) 154,
        (byte) 154,
        (byte) 231,
        (byte) 175,
        (byte) 111,
        (byte) 47,
        (byte) 69,
        (byte) 51,
        (byte) 159,
        (byte) 89,
        (byte) 0,
        (byte) 196,
        (byte) 166,
        (byte) 12,
        (byte) 182,
        (byte) 28,
        (byte) 122,
        (byte) 67,
        (byte) 213,
        (byte) 219,
        (byte) 177,
        (byte) 82,
        (byte) 250,
        (byte) 185,
        (byte) 8,
        (byte) 115,
        (byte) 90,
        (byte) 139,
        (byte) 44,
        (byte) 43,
        (byte) 145,
        (byte) 3,
        (byte) 81,
        (byte) 201,
        (byte) 29,
        (byte) 81,
        (byte) 86,
        (byte) 98,
        (byte) 116,
        (byte) 75,
        (byte) 142,
        (byte) 113,
        (byte) 220,
        (byte) 174,
        (byte) 246,
        (byte) 141,
        (byte) 183,
        (byte) 171,
        (byte) 211,
        (byte) 25,
        (byte) 215,
        (byte) 158
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43]
      {
        (byte) 152,
        (byte) 114,
        (byte) 223,
        (byte) 33,
        (byte) 37,
        (byte) 200,
        (byte) 25,
        (byte) 109,
        (byte) 48 /*0x30*/,
        (byte) 74,
        (byte) 244,
        (byte) 251,
        (byte) 12,
        (byte) 22,
        (byte) 144 /*0x90*/,
        (byte) 142,
        (byte) 135,
        (byte) 59,
        (byte) 67,
        (byte) 195,
        (byte) 87,
        (byte) 79,
        (byte) 213,
        (byte) 27,
        (byte) 168,
        (byte) 216,
        (byte) 166,
        (byte) 19,
        (byte) 65,
        (byte) 71,
        (byte) 62,
        (byte) 19,
        (byte) 104,
        (byte) 43,
        (byte) 69,
        (byte) 241,
        (byte) 254,
        (byte) 68,
        (byte) 148,
        (byte) 23,
        (byte) 55,
        (byte) 41,
        (byte) 122
      };
      byte[] numArray5 = new byte[43];
      numArray5[7] = (byte) 49;
      numArray5[11] = (byte) 193;
      numArray5[9] = (byte) 21;
      numArray5[3] = (byte) 44;
      numArray5[4] = (byte) 214;
      numArray5[5] = (byte) 108;
      numArray5[33] = (byte) 38;
      numArray5[20] = (byte) 220;
      numArray5[8] = (byte) 80 /*0x50*/;
      numArray5[38] = (byte) 183;
      numArray5[34] = (byte) 72;
      numArray5[2] = (byte) 243;
      numArray5[12] = (byte) 187;
      numArray5[13] = (byte) 120;
      numArray5[41] = (byte) 47;
      numArray5[15] = (byte) 12;
      numArray5[26] = (byte) 117;
      numArray5[17] = (byte) 136;
      numArray5[37] = (byte) 91;
      numArray5[19] = (byte) 81;
      numArray5[35] = (byte) 18;
      numArray5[21] = (byte) 39;
      numArray5[22] = (byte) 88;
      numArray5[10] = (byte) 98;
      numArray5[36] = (byte) 153;
      numArray5[25] = (byte) 198;
      numArray5[28] = (byte) 197;
      numArray5[27] = (byte) 236;
      numArray5[18] = (byte) 251;
      numArray5[29] = (byte) 116;
      numArray5[30] = (byte) 168;
      numArray5[31 /*0x1F*/] = (byte) 220;
      numArray5[32 /*0x20*/] = (byte) 246;
      numArray5[14] = (byte) 63 /*0x3F*/;
      numArray5[6] = (byte) 226;
      numArray5[40] = (byte) 89;
      numArray5[24] = (byte) 48 /*0x30*/;
      numArray5[16 /*0x10*/] = (byte) 215;
      numArray5[1] = (byte) 197;
      numArray5[39] = (byte) 60;
      numArray5[42] = (byte) 11;
      numArray5[23] = (byte) 222;
      numArray5[0] = (byte) 199;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[98];
    byte[] numArray7 = new byte[55]
    {
      (byte) 251,
      (byte) 84,
      (byte) 59,
      (byte) 203,
      (byte) 71,
      (byte) 246,
      (byte) 50,
      (byte) 253,
      (byte) 145,
      (byte) 6,
      (byte) 180,
      (byte) 132,
      (byte) 108,
      (byte) 6,
      (byte) 79,
      (byte) 64 /*0x40*/,
      (byte) 76,
      (byte) 73,
      (byte) 218,
      (byte) 37,
      (byte) 234,
      (byte) 117,
      (byte) 218,
      (byte) 60,
      (byte) 231,
      (byte) 194,
      (byte) 82,
      (byte) 223,
      (byte) 186,
      (byte) 49,
      (byte) 188,
      (byte) 210,
      (byte) 218,
      (byte) 206,
      (byte) 213,
      (byte) 73,
      (byte) 84,
      (byte) 119,
      (byte) 94,
      (byte) 53,
      (byte) 89,
      (byte) 48 /*0x30*/,
      (byte) 67,
      (byte) 187,
      (byte) 210,
      (byte) 13,
      (byte) 30,
      (byte) 201,
      (byte) 214,
      (byte) 48 /*0x30*/,
      (byte) 107,
      (byte) 5,
      (byte) 52,
      (byte) 147,
      (byte) 40
    };
    byte[] numArray8 = new byte[55];
    numArray8[34] = (byte) 254;
    numArray8[18] = (byte) 78;
    numArray8[2] = (byte) 144 /*0x90*/;
    numArray8[3] = (byte) 90;
    numArray8[11] = (byte) 175;
    numArray8[37] = (byte) 205;
    numArray8[6] = (byte) 81;
    numArray8[45] = (byte) 4;
    numArray8[8] = (byte) 227;
    numArray8[9] = (byte) 89;
    numArray8[20] = (byte) 24;
    numArray8[7] = (byte) 139;
    numArray8[51] = (byte) 199;
    numArray8[5] = (byte) 219;
    numArray8[53] = (byte) 191;
    numArray8[17] = (byte) 181;
    numArray8[16 /*0x10*/] = (byte) 94;
    numArray8[27] = (byte) 103;
    numArray8[1] = (byte) 65;
    numArray8[0] = (byte) 126;
    numArray8[13] = (byte) 54;
    numArray8[21] = (byte) 89;
    numArray8[35] = (byte) 223;
    numArray8[23] = (byte) 223;
    numArray8[24] = (byte) 63 /*0x3F*/;
    numArray8[25] = (byte) 198;
    numArray8[26] = (byte) 162;
    numArray8[29] = (byte) 94;
    numArray8[28] = (byte) 140;
    numArray8[15] = (byte) 144 /*0x90*/;
    numArray8[30] = (byte) 106;
    numArray8[31 /*0x1F*/] = (byte) 203;
    numArray8[32 /*0x20*/] = (byte) 251;
    numArray8[19] = (byte) 13;
    numArray8[41] = (byte) 206;
    numArray8[12] = (byte) 236;
    numArray8[42] = (byte) 113;
    numArray8[22] = (byte) 54;
    numArray8[38] = (byte) 227;
    numArray8[39] = (byte) 104;
    numArray8[40] = (byte) 17;
    numArray8[4] = (byte) 174;
    numArray8[52] = (byte) 219;
    numArray8[14] = (byte) 115;
    numArray8[44] = (byte) 155;
    numArray8[33] = (byte) 115;
    numArray8[46] = (byte) 209;
    numArray8[10] = (byte) 124;
    numArray8[48 /*0x30*/] = (byte) 209;
    numArray8[49] = (byte) 212;
    numArray8[50] = (byte) 180;
    numArray8[43] = (byte) 216;
    numArray8[36] = (byte) 21;
    numArray8[47] = (byte) 135;
    numArray8[54] = (byte) 243;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[43]
    {
      (byte) 149,
      (byte) 10,
      (byte) 225,
      (byte) 25,
      (byte) 73,
      (byte) 184,
      (byte) 154,
      (byte) 147,
      (byte) 67,
      (byte) 99,
      (byte) 247,
      (byte) 225,
      (byte) 53,
      (byte) 149,
      (byte) 65,
      (byte) 17,
      (byte) 19,
      (byte) 196,
      (byte) 204,
      (byte) 78,
      (byte) 173,
      (byte) 119,
      (byte) 119,
      (byte) 34,
      (byte) 241,
      (byte) 55,
      (byte) 68,
      (byte) 46,
      (byte) 156,
      (byte) 14,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 251,
      (byte) 117,
      (byte) 192 /*0xC0*/,
      (byte) 136,
      (byte) 247,
      (byte) 120,
      (byte) 189,
      (byte) 52,
      (byte) 73,
      (byte) 222,
      (byte) 174
    };
    byte[] numArray10 = new byte[43];
    numArray10[30] = (byte) 36;
    numArray10[0] = (byte) 79;
    numArray10[37] = (byte) 14;
    numArray10[39] = (byte) 71;
    numArray10[12] = (byte) 165;
    numArray10[11] = (byte) 254;
    numArray10[20] = (byte) 40;
    numArray10[27] = (byte) 100;
    numArray10[38] = (byte) 100;
    numArray10[9] = (byte) 213;
    numArray10[17] = (byte) 66;
    numArray10[21] = (byte) 192 /*0xC0*/;
    numArray10[26] = (byte) 219;
    numArray10[1] = (byte) 21;
    numArray10[14] = (byte) 71;
    numArray10[36] = (byte) 70;
    numArray10[16 /*0x10*/] = (byte) 214;
    numArray10[32 /*0x20*/] = (byte) 241;
    numArray10[18] = (byte) 89;
    numArray10[42] = (byte) 76;
    numArray10[13] = (byte) 237;
    numArray10[10] = (byte) 100;
    numArray10[15] = (byte) 39;
    numArray10[23] = (byte) 50;
    numArray10[24] = (byte) 118;
    numArray10[3] = (byte) 221;
    numArray10[22] = (byte) 105;
    numArray10[19] = (byte) 32 /*0x20*/;
    numArray10[28] = (byte) 170;
    numArray10[29] = (byte) 153;
    numArray10[7] = (byte) 61;
    numArray10[31 /*0x1F*/] = (byte) 153;
    numArray10[4] = (byte) 120;
    numArray10[33] = (byte) 194;
    numArray10[2] = (byte) 13;
    numArray10[35] = (byte) 202;
    numArray10[5] = (byte) 232;
    numArray10[25] = (byte) 91;
    numArray10[41] = (byte) 229;
    numArray10[34] = (byte) 32 /*0x20*/;
    numArray10[40] = (byte) 123;
    numArray10[8] = (byte) 250;
    numArray10[6] = (byte) 108;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 43);
    for (int index = 0; index < 43; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[45];
    byte[] response = new byte[45];
    Array.Copy((Array) sc_13686.sspq, 788, (Array) numArray11, 0, 45);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13686.sspr, 788, (Array) numArray11, 0, 45);
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

  internal static int ssp_appserver_13743(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 61,
      (byte) 163,
      (byte) 177,
      (byte) 129,
      (byte) 134,
      (byte) 245,
      (byte) 233,
      (byte) 79,
      (byte) 218,
      (byte) 17,
      (byte) 97,
      (byte) 55,
      (byte) 226,
      (byte) 76,
      (byte) 130,
      (byte) 215,
      (byte) 181,
      (byte) 154,
      (byte) 225,
      (byte) 227,
      (byte) 168,
      (byte) 217,
      (byte) 13,
      (byte) 149,
      (byte) 161,
      (byte) 54,
      (byte) 216,
      (byte) 246,
      (byte) 3,
      (byte) 190,
      (byte) 86,
      (byte) 134,
      (byte) 181,
      (byte) 37,
      (byte) 83,
      (byte) 193,
      (byte) 240 /*0xF0*/,
      (byte) 228,
      (byte) 166,
      (byte) 250,
      (byte) 14,
      (byte) 227,
      (byte) 232,
      (byte) 208 /*0xD0*/,
      (byte) 234,
      (byte) 34,
      (byte) 243,
      (byte) 219
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 102,
      (byte) 60,
      (byte) 52,
      (byte) 130,
      (byte) 197,
      (byte) 13,
      (byte) 201,
      (byte) 31 /*0x1F*/,
      (byte) 241,
      (byte) 254,
      (byte) 49,
      (byte) 130,
      (byte) 165,
      (byte) 71,
      (byte) 66,
      (byte) 45,
      (byte) 248,
      (byte) 74,
      (byte) 83,
      (byte) 185,
      (byte) 246,
      (byte) 178,
      (byte) 198,
      (byte) 4,
      (byte) 140,
      (byte) 146,
      (byte) 220,
      (byte) 37,
      (byte) 114,
      (byte) 197,
      (byte) 105,
      byte.MaxValue,
      (byte) 122,
      (byte) 220,
      (byte) 113,
      (byte) 252,
      (byte) 57,
      (byte) 31 /*0x1F*/,
      (byte) 49,
      (byte) 233,
      (byte) 245,
      (byte) 86,
      (byte) 115,
      (byte) 75,
      (byte) 56,
      (byte) 21,
      (byte) 123,
      (byte) 112 /*0x70*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13744(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 27;
    sourceArray1[47] = (byte) 160 /*0xA0*/;
    sourceArray1[29] = (byte) 145;
    sourceArray1[3] = (byte) 150;
    sourceArray1[25] = (byte) 171;
    sourceArray1[5] = (byte) 224 /*0xE0*/;
    sourceArray1[33] = byte.MaxValue;
    sourceArray1[10] = (byte) 28;
    sourceArray1[18] = (byte) 208 /*0xD0*/;
    sourceArray1[24] = (byte) 60;
    sourceArray1[34] = (byte) 217;
    sourceArray1[11] = (byte) 187;
    sourceArray1[20] = (byte) 230;
    sourceArray1[13] = (byte) 129;
    sourceArray1[2] = (byte) 100;
    sourceArray1[15] = (byte) 248;
    sourceArray1[16 /*0x10*/] = (byte) 193;
    sourceArray1[17] = (byte) 231;
    sourceArray1[8] = (byte) 243;
    sourceArray1[19] = (byte) 243;
    sourceArray1[28] = (byte) 253;
    sourceArray1[21] = (byte) 213;
    sourceArray1[22] = (byte) 197;
    sourceArray1[23] = (byte) 240 /*0xF0*/;
    sourceArray1[27] = (byte) 66;
    sourceArray1[9] = (byte) 75;
    sourceArray1[26] = (byte) 126;
    sourceArray1[1] = (byte) 16 /*0x10*/;
    sourceArray1[41] = (byte) 221;
    sourceArray1[12] = (byte) 219;
    sourceArray1[30] = (byte) 139;
    sourceArray1[31 /*0x1F*/] = (byte) 23;
    sourceArray1[32 /*0x20*/] = (byte) 32 /*0x20*/;
    sourceArray1[14] = (byte) 189;
    sourceArray1[43] = (byte) 151;
    sourceArray1[35] = (byte) 20;
    sourceArray1[36] = (byte) 239;
    sourceArray1[37] = (byte) 43;
    sourceArray1[38] = (byte) 213;
    sourceArray1[45] = (byte) 21;
    sourceArray1[7] = (byte) 28;
    sourceArray1[40] = (byte) 86;
    sourceArray1[42] = (byte) 26;
    sourceArray1[39] = (byte) 4;
    sourceArray1[44] = (byte) 204;
    sourceArray1[6] = (byte) 80 /*0x50*/;
    sourceArray1[46] = (byte) 15;
    sourceArray1[0] = (byte) 219;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 221,
      (byte) 1,
      (byte) 25,
      (byte) 135,
      (byte) 162,
      (byte) 57,
      (byte) 183,
      (byte) 101,
      (byte) 52,
      (byte) 237,
      (byte) 72,
      (byte) 177,
      (byte) 166,
      (byte) 77,
      (byte) 46,
      (byte) 21,
      (byte) 220,
      (byte) 57,
      (byte) 56,
      (byte) 131,
      (byte) 226,
      (byte) 74,
      (byte) 119,
      (byte) 28,
      (byte) 199,
      (byte) 89,
      (byte) 141,
      (byte) 216,
      (byte) 251,
      (byte) 112 /*0x70*/,
      (byte) 99,
      (byte) 100,
      (byte) 55,
      (byte) 56,
      (byte) 8,
      (byte) 248,
      (byte) 124,
      (byte) 91,
      (byte) 207,
      (byte) 174,
      (byte) 121,
      (byte) 231,
      (byte) 90,
      (byte) 156,
      (byte) 75,
      (byte) 14,
      (byte) 117,
      (byte) 209
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13745(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 51,
      (byte) 248,
      (byte) 141,
      (byte) 218,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 10,
      (byte) 164,
      (byte) 150,
      (byte) 75,
      (byte) 28,
      (byte) 79,
      (byte) 51,
      (byte) 210,
      (byte) 131,
      (byte) 2,
      (byte) 5,
      (byte) 116,
      (byte) 198,
      (byte) 195,
      (byte) 188,
      (byte) 124,
      (byte) 212,
      (byte) 224 /*0xE0*/,
      (byte) 141,
      (byte) 196,
      (byte) 172,
      (byte) 41,
      (byte) 85,
      (byte) 75,
      (byte) 2,
      (byte) 87,
      (byte) 93,
      (byte) 171,
      (byte) 105,
      (byte) 229,
      (byte) 126,
      (byte) 64 /*0x40*/,
      (byte) 224 /*0xE0*/,
      (byte) 122,
      (byte) 54,
      (byte) 57,
      (byte) 200,
      (byte) 135,
      (byte) 172,
      (byte) 44,
      (byte) 158,
      (byte) 146
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 184,
      (byte) 52,
      (byte) 16 /*0x10*/,
      (byte) 179,
      (byte) 15,
      (byte) 207,
      (byte) 42,
      (byte) 166,
      (byte) 229,
      (byte) 182,
      (byte) 74,
      (byte) 48 /*0x30*/,
      (byte) 137,
      (byte) 189,
      (byte) 238,
      (byte) 13,
      (byte) 233,
      (byte) 78,
      (byte) 151,
      (byte) 24,
      (byte) 31 /*0x1F*/,
      (byte) 49,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 41,
      (byte) 66,
      (byte) 189,
      (byte) 15,
      byte.MaxValue,
      (byte) 18,
      (byte) 218,
      (byte) 180,
      (byte) 101,
      (byte) 114,
      (byte) 235,
      (byte) 117,
      (byte) 216,
      (byte) 209,
      (byte) 229,
      (byte) 76,
      (byte) 112 /*0x70*/,
      (byte) 110,
      (byte) 17,
      (byte) 204,
      (byte) 98,
      (byte) 253,
      (byte) 35,
      (byte) 72
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13746(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 3,
      (byte) 190,
      (byte) 182,
      (byte) 35,
      (byte) 173,
      (byte) 76,
      (byte) 72,
      (byte) 210,
      (byte) 225,
      (byte) 79,
      (byte) 75,
      (byte) 72,
      (byte) 197,
      (byte) 241,
      (byte) 135,
      (byte) 105,
      (byte) 158,
      (byte) 132,
      (byte) 197,
      (byte) 148,
      (byte) 75,
      (byte) 141,
      (byte) 130,
      (byte) 180,
      (byte) 77,
      (byte) 52,
      (byte) 96 /*0x60*/,
      (byte) 38,
      (byte) 26,
      (byte) 9,
      (byte) 15,
      (byte) 146,
      (byte) 191,
      (byte) 70,
      (byte) 90,
      (byte) 237,
      (byte) 179,
      (byte) 193,
      (byte) 208 /*0xD0*/,
      (byte) 243,
      (byte) 151,
      (byte) 168,
      (byte) 85,
      (byte) 234,
      (byte) 124,
      (byte) 62,
      (byte) 230,
      (byte) 58
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 196,
      (byte) 215,
      (byte) 20,
      (byte) 95,
      (byte) 157,
      (byte) 242,
      (byte) 235,
      (byte) 198,
      (byte) 124,
      (byte) 161,
      (byte) 242,
      (byte) 216,
      (byte) 197,
      (byte) 5,
      (byte) 254,
      (byte) 221,
      (byte) 227,
      (byte) 163,
      (byte) 183,
      (byte) 135,
      (byte) 44,
      (byte) 232,
      (byte) 120,
      (byte) 163,
      (byte) 237,
      (byte) 125,
      (byte) 96 /*0x60*/,
      (byte) 97,
      (byte) 129,
      (byte) 181,
      (byte) 96 /*0x60*/,
      (byte) 210,
      (byte) 121,
      (byte) 127 /*0x7F*/,
      (byte) 165,
      (byte) 102,
      (byte) 218,
      (byte) 113,
      (byte) 15,
      (byte) 190,
      (byte) 211,
      (byte) 39,
      (byte) 70,
      (byte) 22,
      (byte) 87,
      (byte) 229,
      (byte) 154,
      (byte) 81
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13747()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[131];
      byte[] numArray2 = new byte[55];
      numArray2[27] = (byte) 14;
      numArray2[34] = (byte) 158;
      numArray2[2] = (byte) 196;
      numArray2[3] = (byte) 117;
      numArray2[4] = (byte) 60;
      numArray2[21] = (byte) 209;
      numArray2[53] = (byte) 164;
      numArray2[17] = (byte) 154;
      numArray2[8] = (byte) 235;
      numArray2[9] = (byte) 34;
      numArray2[5] = (byte) 1;
      numArray2[11] = (byte) 215;
      numArray2[54] = (byte) 12;
      numArray2[30] = (byte) 15;
      numArray2[14] = (byte) 137;
      numArray2[6] = (byte) 214;
      numArray2[29] = (byte) 37;
      numArray2[15] = (byte) 126;
      numArray2[39] = (byte) 178;
      numArray2[16 /*0x10*/] = (byte) 151;
      numArray2[20] = (byte) 180;
      numArray2[19] = (byte) 221;
      numArray2[22] = (byte) 252;
      numArray2[18] = (byte) 181;
      numArray2[35] = (byte) 17;
      numArray2[25] = (byte) 151;
      numArray2[10] = (byte) 184;
      numArray2[26] = (byte) 56;
      numArray2[28] = (byte) 77;
      numArray2[49] = (byte) 103;
      numArray2[13] = (byte) 185;
      numArray2[24] = (byte) 56;
      numArray2[0] = (byte) 243;
      numArray2[33] = (byte) 79;
      numArray2[42] = (byte) 7;
      numArray2[50] = (byte) 163;
      numArray2[36] = (byte) 130;
      numArray2[1] = (byte) 174;
      numArray2[38] = (byte) 176 /*0xB0*/;
      numArray2[48 /*0x30*/] = (byte) 181;
      numArray2[40] = (byte) 234;
      numArray2[37] = (byte) 228;
      numArray2[41] = (byte) 86;
      numArray2[44] = (byte) 239;
      numArray2[7] = (byte) 121;
      numArray2[45] = (byte) 125;
      numArray2[12] = (byte) 63 /*0x3F*/;
      numArray2[47] = (byte) 195;
      numArray2[46] = (byte) 171;
      numArray2[23] = (byte) 163;
      numArray2[43] = (byte) 117;
      numArray2[32 /*0x20*/] = (byte) 136;
      numArray2[51] = (byte) 124;
      numArray2[52] = (byte) 115;
      numArray2[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
      byte[] numArray3 = new byte[55]
      {
        (byte) 29,
        (byte) 67,
        (byte) 81,
        (byte) 8,
        (byte) 53,
        (byte) 227,
        (byte) 214,
        (byte) 233,
        (byte) 126,
        (byte) 155,
        (byte) 235,
        (byte) 13,
        (byte) 149,
        (byte) 6,
        (byte) 48 /*0x30*/,
        (byte) 117,
        (byte) 191,
        (byte) 247,
        (byte) 240 /*0xF0*/,
        (byte) 3,
        (byte) 32 /*0x20*/,
        (byte) 120,
        (byte) 8,
        (byte) 82,
        (byte) 145,
        (byte) 212,
        (byte) 166,
        (byte) 92,
        (byte) 192 /*0xC0*/,
        (byte) 167,
        (byte) 250,
        (byte) 186,
        (byte) 46,
        (byte) 149,
        (byte) 145,
        (byte) 44,
        (byte) 12,
        (byte) 67,
        (byte) 175,
        (byte) 166,
        (byte) 131,
        (byte) 42,
        (byte) 63 /*0x3F*/,
        (byte) 59,
        (byte) 197,
        (byte) 166,
        (byte) 55,
        (byte) 139,
        (byte) 73,
        (byte) 0,
        (byte) 111,
        (byte) 46,
        (byte) 101,
        (byte) 217,
        (byte) 245
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 109,
        (byte) 131,
        (byte) 185,
        (byte) 84,
        (byte) 111,
        (byte) 70,
        (byte) 67,
        (byte) 128 /*0x80*/,
        (byte) 74,
        (byte) 106,
        (byte) 173,
        (byte) 156,
        (byte) 24,
        (byte) 154,
        (byte) 11,
        (byte) 222,
        (byte) 26,
        (byte) 208 /*0xD0*/,
        (byte) 50,
        (byte) 136,
        (byte) 37,
        (byte) 139,
        (byte) 139,
        (byte) 48 /*0x30*/,
        (byte) 203,
        (byte) 27,
        (byte) 122,
        (byte) 145,
        byte.MaxValue,
        (byte) 13,
        (byte) 50,
        (byte) 146,
        (byte) 37,
        (byte) 53,
        (byte) 244,
        (byte) 35,
        (byte) 81,
        (byte) 148,
        (byte) 178,
        (byte) 228,
        (byte) 253,
        (byte) 225,
        (byte) 222,
        (byte) 196,
        (byte) 13,
        (byte) 12,
        (byte) 36,
        (byte) 209,
        (byte) 228,
        (byte) 180,
        (byte) 84,
        (byte) 224 /*0xE0*/,
        (byte) 125,
        (byte) 206,
        (byte) 4
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 89,
        (byte) 145,
        (byte) 204,
        (byte) 53,
        (byte) 233,
        (byte) 54,
        (byte) 152,
        (byte) 151,
        (byte) 62,
        (byte) 175,
        (byte) 74,
        (byte) 67,
        (byte) 40,
        (byte) 38,
        (byte) 121,
        (byte) 55,
        (byte) 187,
        (byte) 159,
        (byte) 176 /*0xB0*/,
        (byte) 37,
        (byte) 239,
        (byte) 84,
        (byte) 50,
        (byte) 132,
        (byte) 202,
        (byte) 36,
        (byte) 58,
        (byte) 254,
        (byte) 100,
        (byte) 140,
        (byte) 218,
        (byte) 246,
        (byte) 22,
        (byte) 111,
        (byte) 83,
        (byte) 63 /*0x3F*/,
        (byte) 142,
        (byte) 75,
        (byte) 169,
        (byte) 96 /*0x60*/,
        (byte) 215,
        (byte) 80 /*0x50*/,
        (byte) 5,
        (byte) 185,
        (byte) 145,
        (byte) 128 /*0x80*/,
        (byte) 77,
        (byte) 244,
        (byte) 132,
        (byte) 187,
        (byte) 20,
        (byte) 199,
        (byte) 87,
        (byte) 48 /*0x30*/,
        (byte) 195
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[21]
      {
        (byte) 136,
        (byte) 136,
        (byte) 84,
        (byte) 204,
        (byte) 178,
        (byte) 129,
        (byte) 60,
        (byte) 55,
        (byte) 27,
        (byte) 118,
        (byte) 227,
        (byte) 166,
        (byte) 231,
        (byte) 140,
        (byte) 134,
        (byte) 30,
        (byte) 77,
        (byte) 138,
        (byte) 67,
        (byte) 173,
        (byte) 199
      };
      byte[] numArray7 = new byte[21]
      {
        (byte) 200,
        (byte) 30,
        (byte) 203,
        (byte) 109,
        (byte) 54,
        (byte) 235,
        (byte) 248,
        (byte) 185,
        (byte) 167,
        (byte) 38,
        (byte) 223,
        (byte) 196,
        (byte) 192 /*0xC0*/,
        (byte) 68,
        (byte) 198,
        (byte) 220,
        (byte) 42,
        (byte) 24,
        (byte) 44,
        (byte) 19,
        (byte) 115
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
      (byte) 168,
      (byte) 172,
      (byte) 75,
      (byte) 184,
      (byte) 82,
      (byte) 21,
      (byte) 87,
      (byte) 48 /*0x30*/,
      (byte) 17,
      (byte) 44,
      (byte) 201,
      (byte) 11,
      (byte) 93,
      (byte) 24,
      (byte) 75,
      (byte) 96 /*0x60*/,
      (byte) 161,
      (byte) 140,
      (byte) 242,
      (byte) 149,
      (byte) 64 /*0x40*/,
      (byte) 42,
      (byte) 252,
      (byte) 39,
      (byte) 206,
      (byte) 194,
      (byte) 47,
      (byte) 67,
      (byte) 193,
      (byte) 53,
      (byte) 47,
      (byte) 101,
      (byte) 201,
      (byte) 92,
      (byte) 35,
      (byte) 5,
      (byte) 54,
      (byte) 131,
      (byte) 229,
      (byte) 5,
      (byte) 31 /*0x1F*/,
      (byte) 179,
      (byte) 148,
      (byte) 187,
      (byte) 195,
      (byte) 122,
      (byte) 192 /*0xC0*/,
      (byte) 53,
      (byte) 62,
      (byte) 227,
      (byte) 11,
      (byte) 59,
      (byte) 48 /*0x30*/,
      (byte) 42,
      (byte) 182
    };
    byte[] numArray10 = new byte[55];
    numArray10[43] = (byte) 59;
    numArray10[48 /*0x30*/] = (byte) 168;
    numArray10[2] = (byte) 191;
    numArray10[3] = (byte) 61;
    numArray10[4] = (byte) 38;
    numArray10[39] = (byte) 163;
    numArray10[25] = (byte) 45;
    numArray10[7] = (byte) 36;
    numArray10[44] = (byte) 16 /*0x10*/;
    numArray10[51] = (byte) 67;
    numArray10[36] = (byte) 153;
    numArray10[12] = (byte) 24;
    numArray10[19] = (byte) 197;
    numArray10[13] = (byte) 128 /*0x80*/;
    numArray10[14] = (byte) 153;
    numArray10[31 /*0x1F*/] = (byte) 121;
    numArray10[16 /*0x10*/] = (byte) 18;
    numArray10[17] = (byte) 103;
    numArray10[35] = (byte) 63 /*0x3F*/;
    numArray10[6] = (byte) 143;
    numArray10[20] = (byte) 40;
    numArray10[21] = (byte) 126;
    numArray10[24] = (byte) 173;
    numArray10[23] = (byte) 83;
    numArray10[40] = (byte) 232;
    numArray10[8] = (byte) 54;
    numArray10[28] = (byte) 186;
    numArray10[27] = (byte) 127 /*0x7F*/;
    numArray10[18] = (byte) 19;
    numArray10[29] = (byte) 144 /*0x90*/;
    numArray10[30] = (byte) 110;
    numArray10[26] = (byte) 44;
    numArray10[32 /*0x20*/] = (byte) 230;
    numArray10[1] = (byte) 119;
    numArray10[45] = (byte) 149;
    numArray10[49] = (byte) 125;
    numArray10[33] = (byte) 89;
    numArray10[0] = (byte) 33;
    numArray10[38] = (byte) 33;
    numArray10[5] = (byte) 83;
    numArray10[37] = (byte) 36;
    numArray10[41] = (byte) 139;
    numArray10[42] = (byte) 43;
    numArray10[11] = (byte) 209;
    numArray10[9] = (byte) 167;
    numArray10[34] = (byte) 222;
    numArray10[46] = (byte) 84;
    numArray10[47] = (byte) 145;
    numArray10[22] = (byte) 77;
    numArray10[10] = (byte) 87;
    numArray10[53] = (byte) 150;
    numArray10[50] = (byte) 118;
    numArray10[52] = (byte) 18;
    numArray10[15] = (byte) 191;
    numArray10[54] = (byte) 180;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[3] = (byte) 235;
    numArray11[45] = (byte) 238;
    numArray11[2] = (byte) 142;
    numArray11[23] = (byte) 65;
    numArray11[4] = (byte) 14;
    numArray11[5] = (byte) 145;
    numArray11[41] = (byte) 182;
    numArray11[7] = (byte) 7;
    numArray11[44] = (byte) 101;
    numArray11[9] = (byte) 163;
    numArray11[10] = (byte) 90;
    numArray11[38] = (byte) 253;
    numArray11[12] = (byte) 240 /*0xF0*/;
    numArray11[13] = (byte) 23;
    numArray11[14] = (byte) 132;
    numArray11[15] = (byte) 82;
    numArray11[16 /*0x10*/] = (byte) 205;
    numArray11[53] = (byte) 9;
    numArray11[18] = (byte) 73;
    numArray11[24] = (byte) 186;
    numArray11[19] = (byte) 222;
    numArray11[21] = (byte) 3;
    numArray11[33] = (byte) 231;
    numArray11[8] = (byte) 46;
    numArray11[43] = (byte) 148;
    numArray11[25] = (byte) 12;
    numArray11[26] = (byte) 37;
    numArray11[6] = (byte) 178;
    numArray11[28] = (byte) 96 /*0x60*/;
    numArray11[29] = (byte) 241;
    numArray11[39] = (byte) 178;
    numArray11[31 /*0x1F*/] = (byte) 125;
    numArray11[22] = (byte) 26;
    numArray11[1] = (byte) 151;
    numArray11[34] = (byte) 82;
    numArray11[35] = (byte) 1;
    numArray11[36] = (byte) 198;
    numArray11[11] = (byte) 80 /*0x50*/;
    numArray11[49] = (byte) 65;
    numArray11[54] = (byte) 235;
    numArray11[17] = (byte) 105;
    numArray11[42] = (byte) 49;
    numArray11[48 /*0x30*/] = (byte) 104;
    numArray11[40] = (byte) 4;
    numArray11[0] = (byte) 191;
    numArray11[32 /*0x20*/] = (byte) 188;
    numArray11[37] = (byte) 51;
    numArray11[52] = (byte) 223;
    numArray11[30] = (byte) 234;
    numArray11[27] = (byte) 59;
    numArray11[50] = (byte) 118;
    numArray11[51] = (byte) 74;
    numArray11[46] = (byte) 7;
    numArray11[20] = (byte) 130;
    numArray11[47] = (byte) 161;
    byte[] numArray12 = new byte[55];
    numArray12[29] = (byte) 126;
    numArray12[17] = (byte) 215;
    numArray12[2] = (byte) 24;
    numArray12[52] = (byte) 153;
    numArray12[16 /*0x10*/] = (byte) 153;
    numArray12[5] = (byte) 121;
    numArray12[23] = (byte) 28;
    numArray12[7] = (byte) 213;
    numArray12[10] = (byte) 221;
    numArray12[22] = (byte) 179;
    numArray12[3] = (byte) 91;
    numArray12[11] = (byte) 60;
    numArray12[45] = (byte) 148;
    numArray12[13] = (byte) 243;
    numArray12[4] = (byte) 66;
    numArray12[15] = (byte) 55;
    numArray12[12] = (byte) 144 /*0x90*/;
    numArray12[20] = (byte) 62;
    numArray12[38] = (byte) 160 /*0xA0*/;
    numArray12[19] = (byte) 8;
    numArray12[40] = (byte) 151;
    numArray12[44] = (byte) 86;
    numArray12[43] = (byte) 187;
    numArray12[21] = (byte) 147;
    numArray12[41] = (byte) 177;
    numArray12[25] = (byte) 249;
    numArray12[26] = (byte) 94;
    numArray12[1] = (byte) 65;
    numArray12[28] = (byte) 247;
    numArray12[48 /*0x30*/] = (byte) 177;
    numArray12[39] = (byte) 249;
    numArray12[18] = (byte) 167;
    numArray12[32 /*0x20*/] = (byte) 12;
    numArray12[33] = (byte) 120;
    numArray12[34] = (byte) 165;
    numArray12[35] = (byte) 183;
    numArray12[36] = (byte) 163;
    numArray12[37] = (byte) 95;
    numArray12[54] = (byte) 167;
    numArray12[30] = (byte) 131;
    numArray12[27] = (byte) 81;
    numArray12[9] = (byte) 8;
    numArray12[42] = (byte) 71;
    numArray12[47] = (byte) 250;
    numArray12[53] = (byte) 55;
    numArray12[14] = (byte) 69;
    numArray12[46] = (byte) 166;
    numArray12[50] = (byte) 38;
    numArray12[24] = (byte) 70;
    numArray12[49] = (byte) 250;
    numArray12[0] = (byte) 141;
    numArray12[51] = (byte) 160 /*0xA0*/;
    numArray12[6] = (byte) 248;
    numArray12[31 /*0x1F*/] = (byte) 235;
    numArray12[8] = (byte) 123;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[21]
    {
      (byte) 30,
      (byte) 244,
      (byte) 246,
      (byte) 63 /*0x3F*/,
      (byte) 183,
      (byte) 31 /*0x1F*/,
      (byte) 35,
      (byte) 88,
      (byte) 232,
      (byte) 94,
      (byte) 48 /*0x30*/,
      (byte) 129,
      (byte) 111,
      (byte) 13,
      (byte) 249,
      (byte) 116,
      (byte) 241,
      (byte) 97,
      (byte) 52,
      (byte) 172,
      (byte) 231
    };
    byte[] numArray14 = new byte[21];
    numArray14[2] = (byte) 24;
    numArray14[1] = (byte) 141;
    numArray14[5] = (byte) 40;
    numArray14[3] = (byte) 62;
    numArray14[16 /*0x10*/] = (byte) 197;
    numArray14[7] = (byte) 105;
    numArray14[6] = (byte) 26;
    numArray14[4] = (byte) 133;
    numArray14[0] = (byte) 44;
    numArray14[9] = (byte) 249;
    numArray14[10] = (byte) 150;
    numArray14[18] = (byte) 131;
    numArray14[8] = (byte) 159;
    numArray14[13] = (byte) 38;
    numArray14[19] = (byte) 136;
    numArray14[14] = (byte) 183;
    numArray14[11] = (byte) 137;
    numArray14[17] = (byte) 7;
    numArray14[15] = (byte) 171;
    numArray14[20] = (byte) 88;
    numArray14[12] = (byte) 69;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 21);
    for (int index = 0; index < 21; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13748()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[184];
      byte[] numArray2 = new byte[55]
      {
        (byte) 119,
        (byte) 149,
        (byte) 74,
        (byte) 247,
        (byte) 7,
        (byte) 131,
        (byte) 46,
        (byte) 95,
        (byte) 100,
        (byte) 22,
        (byte) 253,
        (byte) 224 /*0xE0*/,
        (byte) 68,
        (byte) 176 /*0xB0*/,
        (byte) 88,
        (byte) 199,
        (byte) 3,
        (byte) 139,
        (byte) 194,
        (byte) 184,
        (byte) 225,
        (byte) 46,
        (byte) 37,
        (byte) 80 /*0x50*/,
        (byte) 110,
        (byte) 210,
        (byte) 47,
        (byte) 204,
        (byte) 157,
        (byte) 126,
        (byte) 166,
        (byte) 219,
        (byte) 86,
        (byte) 208 /*0xD0*/,
        (byte) 142,
        (byte) 49,
        (byte) 17,
        (byte) 94,
        (byte) 85,
        (byte) 25,
        (byte) 74,
        (byte) 65,
        (byte) 221,
        (byte) 209,
        (byte) 172,
        (byte) 38,
        (byte) 218,
        (byte) 129,
        (byte) 16 /*0x10*/,
        (byte) 70,
        (byte) 102,
        (byte) 215,
        (byte) 88,
        (byte) 7,
        (byte) 73
      };
      byte[] numArray3 = new byte[55];
      numArray3[23] = (byte) 14;
      numArray3[1] = (byte) 159;
      numArray3[24] = (byte) 210;
      numArray3[3] = (byte) 239;
      numArray3[39] = (byte) 10;
      numArray3[51] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 194;
      numArray3[52] = (byte) 42;
      numArray3[8] = (byte) 237;
      numArray3[54] = (byte) 12;
      numArray3[10] = (byte) 92;
      numArray3[18] = (byte) 62;
      numArray3[22] = (byte) 167;
      numArray3[13] = (byte) 234;
      numArray3[29] = (byte) 105;
      numArray3[2] = (byte) 165;
      numArray3[15] = (byte) 188;
      numArray3[17] = (byte) 123;
      numArray3[4] = (byte) 220;
      numArray3[5] = (byte) 92;
      numArray3[9] = (byte) 71;
      numArray3[38] = (byte) 161;
      numArray3[47] = (byte) 148;
      numArray3[48 /*0x30*/] = (byte) 47;
      numArray3[14] = (byte) 59;
      numArray3[45] = (byte) 107;
      numArray3[44] = (byte) 2;
      numArray3[26] = (byte) 137;
      numArray3[28] = (byte) 47;
      numArray3[50] = (byte) 135;
      numArray3[30] = (byte) 187;
      numArray3[19] = (byte) 110;
      numArray3[32 /*0x20*/] = (byte) 135;
      numArray3[33] = (byte) 125;
      numArray3[34] = (byte) 130;
      numArray3[11] = (byte) 46;
      numArray3[36] = (byte) 111;
      numArray3[37] = (byte) 252;
      numArray3[40] = (byte) 243;
      numArray3[12] = (byte) 131;
      numArray3[16 /*0x10*/] = (byte) 90;
      numArray3[0] = (byte) 209;
      numArray3[42] = (byte) 156;
      numArray3[43] = (byte) 110;
      numArray3[7] = (byte) 35;
      numArray3[41] = (byte) 126;
      numArray3[49] = (byte) 49;
      numArray3[31 /*0x1F*/] = (byte) 6;
      numArray3[46] = (byte) 126;
      numArray3[35] = (byte) 26;
      numArray3[25] = (byte) 93;
      numArray3[21] = (byte) 126;
      numArray3[27] = (byte) 75;
      numArray3[53] = (byte) 132;
      numArray3[20] = (byte) 29;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 85,
        (byte) 65,
        (byte) 61,
        (byte) 165,
        (byte) 166,
        (byte) 107,
        (byte) 207,
        (byte) 161,
        (byte) 159,
        (byte) 57,
        (byte) 238,
        (byte) 220,
        (byte) 101,
        (byte) 159,
        (byte) 207,
        (byte) 21,
        (byte) 227,
        (byte) 134,
        (byte) 32 /*0x20*/,
        (byte) 89,
        (byte) 243,
        (byte) 25,
        (byte) 169,
        (byte) 146,
        (byte) 78,
        (byte) 210,
        (byte) 198,
        (byte) 85,
        (byte) 45,
        (byte) 210,
        (byte) 181,
        (byte) 220,
        (byte) 77,
        (byte) 166,
        (byte) 193,
        (byte) 148,
        (byte) 226,
        (byte) 31 /*0x1F*/,
        (byte) 65,
        (byte) 19,
        (byte) 39,
        (byte) 191,
        (byte) 234,
        (byte) 99,
        (byte) 119,
        (byte) 131,
        (byte) 132,
        (byte) 240 /*0xF0*/,
        (byte) 242,
        (byte) 140,
        (byte) 203,
        (byte) 19,
        (byte) 137,
        (byte) 158,
        (byte) 39
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 102,
        (byte) 188,
        (byte) 80 /*0x50*/,
        (byte) 48 /*0x30*/,
        (byte) 198,
        (byte) 253,
        (byte) 19,
        (byte) 193,
        (byte) 126,
        (byte) 91,
        (byte) 141,
        (byte) 23,
        (byte) 120,
        (byte) 165,
        (byte) 44,
        (byte) 5,
        (byte) 135,
        (byte) 74,
        (byte) 66,
        (byte) 163,
        (byte) 135,
        (byte) 251,
        (byte) 236,
        (byte) 48 /*0x30*/,
        (byte) 16 /*0x10*/,
        (byte) 54,
        (byte) 76,
        (byte) 180,
        (byte) 242,
        (byte) 68,
        (byte) 215,
        (byte) 2,
        (byte) 156,
        (byte) 242,
        (byte) 180,
        (byte) 208 /*0xD0*/,
        (byte) 215,
        (byte) 34,
        (byte) 38,
        (byte) 11,
        (byte) 140,
        (byte) 74,
        (byte) 226,
        (byte) 104,
        (byte) 24,
        (byte) 152,
        (byte) 19,
        (byte) 46,
        (byte) 94,
        (byte) 142,
        (byte) 111,
        (byte) 17,
        (byte) 89,
        (byte) 254,
        (byte) 152
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 171,
        (byte) 87,
        (byte) 211,
        (byte) 164,
        (byte) 121,
        (byte) 116,
        (byte) 204,
        (byte) 132,
        (byte) 17,
        (byte) 142,
        (byte) 190,
        (byte) 221,
        (byte) 59,
        (byte) 50,
        (byte) 196,
        (byte) 68,
        (byte) 13,
        (byte) 50,
        (byte) 52,
        (byte) 46,
        (byte) 70,
        (byte) 26,
        (byte) 203,
        (byte) 78,
        (byte) 213,
        (byte) 98,
        (byte) 160 /*0xA0*/,
        (byte) 7,
        (byte) 162,
        (byte) 83,
        (byte) 175,
        (byte) 127 /*0x7F*/,
        (byte) 133,
        (byte) 252,
        (byte) 55,
        (byte) 200,
        (byte) 110,
        (byte) 110,
        (byte) 174,
        (byte) 74,
        (byte) 30,
        (byte) 223,
        (byte) 101,
        (byte) 244,
        (byte) 197,
        (byte) 19,
        (byte) 183,
        (byte) 89,
        (byte) 7,
        (byte) 206,
        (byte) 123,
        (byte) 108,
        (byte) 242,
        (byte) 13,
        (byte) 81
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 48 /*0x30*/,
        (byte) 53,
        (byte) 2,
        (byte) 210,
        (byte) 139,
        (byte) 197,
        (byte) 126,
        (byte) 201,
        (byte) 11,
        (byte) 3,
        (byte) 53,
        (byte) 3,
        (byte) 144 /*0x90*/,
        (byte) 143,
        (byte) 252,
        (byte) 13,
        (byte) 206,
        (byte) 21,
        (byte) 54,
        (byte) 83,
        (byte) 243,
        (byte) 243,
        (byte) 195,
        (byte) 79,
        (byte) 156,
        (byte) 221,
        (byte) 47,
        (byte) 98,
        (byte) 169,
        (byte) 115,
        (byte) 184,
        (byte) 3,
        (byte) 15,
        (byte) 88,
        (byte) 135,
        (byte) 233,
        (byte) 27,
        (byte) 241,
        (byte) 197,
        (byte) 74,
        (byte) 2,
        (byte) 58,
        (byte) 244,
        (byte) 137,
        (byte) 206,
        (byte) 154,
        (byte) 146,
        (byte) 179,
        (byte) 18,
        (byte) 78,
        (byte) 209,
        (byte) 86,
        (byte) 72,
        (byte) 133,
        (byte) 114
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[19]
      {
        (byte) 132,
        (byte) 207,
        (byte) 23,
        (byte) 211,
        (byte) 197,
        (byte) 226,
        (byte) 190,
        (byte) 223,
        (byte) 63 /*0x3F*/,
        (byte) 88,
        (byte) 62,
        (byte) 251,
        (byte) 252,
        (byte) 214,
        (byte) 159,
        (byte) 132,
        (byte) 233,
        (byte) 16 /*0x10*/,
        (byte) 102
      };
      byte[] numArray9 = new byte[19]
      {
        (byte) 96 /*0x60*/,
        (byte) 116,
        (byte) 239,
        (byte) 81,
        (byte) 141,
        (byte) 29,
        (byte) 142,
        (byte) 249,
        (byte) 34,
        (byte) 99,
        (byte) 56,
        (byte) 209,
        (byte) 18,
        (byte) 61,
        (byte) 89,
        (byte) 194,
        (byte) 216,
        (byte) 87,
        (byte) 54
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_13686.sspq, 833, (Array) numArray10, 0, 48 /*0x30*/);
      key.Query(true, 335, numArray10, response);
      Array.Copy((Array) sc_13686.sspr, 833, (Array) numArray10, 0, 48 /*0x30*/);
      for (int index = 0; index < numArray10.Length; ++index)
      {
        if ((int) numArray10[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray11 = new byte[184];
    byte[] numArray12 = new byte[55]
    {
      (byte) 114,
      (byte) 184,
      (byte) 105,
      (byte) 13,
      (byte) 195,
      (byte) 2,
      (byte) 8,
      (byte) 49,
      (byte) 232,
      (byte) 245,
      (byte) 219,
      (byte) 251,
      (byte) 90,
      (byte) 131,
      (byte) 123,
      (byte) 193,
      (byte) 201,
      (byte) 136,
      (byte) 237,
      (byte) 147,
      (byte) 42,
      (byte) 234,
      (byte) 146,
      (byte) 238,
      (byte) 192 /*0xC0*/,
      (byte) 119,
      (byte) 212,
      (byte) 218,
      (byte) 230,
      (byte) 8,
      (byte) 75,
      (byte) 10,
      (byte) 220,
      (byte) 251,
      (byte) 150,
      (byte) 68,
      (byte) 128 /*0x80*/,
      (byte) 207,
      (byte) 240 /*0xF0*/,
      (byte) 249,
      (byte) 4,
      (byte) 58,
      (byte) 164,
      (byte) 208 /*0xD0*/,
      (byte) 51,
      (byte) 235,
      (byte) 66,
      (byte) 47,
      (byte) 101,
      (byte) 209,
      (byte) 140,
      (byte) 55,
      byte.MaxValue,
      (byte) 108,
      (byte) 194
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 84,
      (byte) 166,
      (byte) 246,
      (byte) 144 /*0x90*/,
      (byte) 190,
      (byte) 23,
      (byte) 120,
      (byte) 9,
      (byte) 174,
      (byte) 236,
      (byte) 10,
      (byte) 251,
      (byte) 45,
      (byte) 190,
      (byte) 209,
      (byte) 96 /*0x60*/,
      (byte) 238,
      (byte) 125,
      (byte) 89,
      (byte) 173,
      (byte) 98,
      (byte) 96 /*0x60*/,
      (byte) 51,
      (byte) 3,
      (byte) 13,
      (byte) 27,
      (byte) 230,
      (byte) 240 /*0xF0*/,
      (byte) 232,
      (byte) 16 /*0x10*/,
      (byte) 63 /*0x3F*/,
      (byte) 113,
      (byte) 110,
      (byte) 10,
      (byte) 86,
      (byte) 117,
      (byte) 49,
      (byte) 152,
      (byte) 195,
      (byte) 173,
      (byte) 61,
      (byte) 211,
      (byte) 122,
      (byte) 103,
      (byte) 40,
      (byte) 113,
      (byte) 168,
      (byte) 97,
      (byte) 100,
      (byte) 123,
      (byte) 122,
      (byte) 239,
      (byte) 64 /*0x40*/,
      (byte) 198,
      (byte) 223
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray11, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index] ^= numArray13[index];
    byte[] numArray14 = new byte[55];
    numArray14[46] = (byte) 47;
    numArray14[48 /*0x30*/] = (byte) 146;
    numArray14[2] = (byte) 76;
    numArray14[54] = (byte) 98;
    numArray14[4] = (byte) 45;
    numArray14[5] = (byte) 75;
    numArray14[6] = (byte) 223;
    numArray14[32 /*0x20*/] = (byte) 14;
    numArray14[31 /*0x1F*/] = (byte) 56;
    numArray14[9] = (byte) 30;
    numArray14[35] = (byte) 130;
    numArray14[11] = (byte) 236;
    numArray14[27] = (byte) 124;
    numArray14[13] = (byte) 64 /*0x40*/;
    numArray14[14] = (byte) 249;
    numArray14[43] = (byte) 204;
    numArray14[51] = (byte) 221;
    numArray14[17] = (byte) 143;
    numArray14[18] = (byte) 239;
    numArray14[19] = (byte) 70;
    numArray14[21] = (byte) 186;
    numArray14[12] = (byte) 54;
    numArray14[1] = (byte) 77;
    numArray14[44] = (byte) 226;
    numArray14[40] = (byte) 196;
    numArray14[33] = (byte) 31 /*0x1F*/;
    numArray14[26] = (byte) 20;
    numArray14[3] = (byte) 46;
    numArray14[47] = (byte) 144 /*0x90*/;
    numArray14[29] = (byte) 10;
    numArray14[30] = (byte) 111;
    numArray14[20] = (byte) 194;
    numArray14[53] = (byte) 66;
    numArray14[15] = (byte) 151;
    numArray14[34] = (byte) 4;
    numArray14[28] = (byte) 20;
    numArray14[39] = (byte) 239;
    numArray14[37] = (byte) 203;
    numArray14[38] = (byte) 214;
    numArray14[45] = (byte) 66;
    numArray14[23] = (byte) 52;
    numArray14[41] = (byte) 38;
    numArray14[52] = (byte) 210;
    numArray14[0] = (byte) 123;
    numArray14[7] = (byte) 170;
    numArray14[22] = (byte) 212;
    numArray14[8] = (byte) 209;
    numArray14[16 /*0x10*/] = (byte) 244;
    numArray14[24] = (byte) 165;
    numArray14[49] = (byte) 159;
    numArray14[50] = (byte) 10;
    numArray14[25] = (byte) 117;
    numArray14[36] = (byte) 164;
    numArray14[10] = (byte) 95;
    numArray14[42] = (byte) 98;
    byte[] numArray15 = new byte[55]
    {
      (byte) 94,
      (byte) 100,
      (byte) 72,
      (byte) 51,
      (byte) 127 /*0x7F*/,
      (byte) 55,
      (byte) 85,
      (byte) 10,
      (byte) 127 /*0x7F*/,
      (byte) 96 /*0x60*/,
      (byte) 93,
      (byte) 62,
      (byte) 239,
      (byte) 168,
      (byte) 135,
      (byte) 253,
      (byte) 178,
      (byte) 48 /*0x30*/,
      (byte) 183,
      (byte) 230,
      (byte) 148,
      (byte) 41,
      (byte) 176 /*0xB0*/,
      (byte) 48 /*0x30*/,
      (byte) 191,
      (byte) 32 /*0x20*/,
      (byte) 180,
      (byte) 95,
      (byte) 61,
      (byte) 122,
      (byte) 22,
      (byte) 162,
      (byte) 107,
      (byte) 21,
      (byte) 49,
      (byte) 245,
      (byte) 33,
      (byte) 222,
      (byte) 83,
      (byte) 231,
      (byte) 243,
      (byte) 159,
      (byte) 27,
      (byte) 54,
      (byte) 188,
      (byte) 206,
      (byte) 18,
      (byte) 45,
      (byte) 179,
      (byte) 122,
      (byte) 38,
      (byte) 182,
      (byte) 8,
      (byte) 156,
      (byte) 243
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray11, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 55] ^= numArray15[index];
    byte[] numArray16 = new byte[55]
    {
      (byte) 197,
      (byte) 38,
      (byte) 114,
      (byte) 178,
      (byte) 122,
      (byte) 33,
      (byte) 198,
      (byte) 28,
      (byte) 193,
      (byte) 18,
      (byte) 89,
      (byte) 247,
      (byte) 109,
      (byte) 101,
      (byte) 231,
      (byte) 155,
      (byte) 70,
      (byte) 32 /*0x20*/,
      (byte) 11,
      (byte) 189,
      (byte) 233,
      (byte) 150,
      byte.MaxValue,
      (byte) 45,
      (byte) 46,
      (byte) 205,
      (byte) 200,
      (byte) 30,
      (byte) 110,
      (byte) 147,
      (byte) 11,
      (byte) 247,
      (byte) 220,
      (byte) 152,
      (byte) 199,
      (byte) 27,
      (byte) 171,
      (byte) 7,
      (byte) 231,
      (byte) 111,
      (byte) 213,
      (byte) 1,
      (byte) 250,
      (byte) 81,
      (byte) 191,
      (byte) 93,
      (byte) 174,
      (byte) 71,
      (byte) 250,
      (byte) 137,
      (byte) 140,
      (byte) 160 /*0xA0*/,
      (byte) 186,
      (byte) 170,
      (byte) 135
    };
    byte[] numArray17 = new byte[55];
    numArray17[49] = (byte) 116;
    numArray17[27] = (byte) 188;
    numArray17[45] = (byte) 175;
    numArray17[3] = (byte) 101;
    numArray17[23] = (byte) 174;
    numArray17[37] = (byte) 79;
    numArray17[6] = (byte) 153;
    numArray17[7] = (byte) 27;
    numArray17[25] = (byte) 55;
    numArray17[9] = (byte) 179;
    numArray17[44] = (byte) 120;
    numArray17[48 /*0x30*/] = (byte) 60;
    numArray17[50] = (byte) 18;
    numArray17[29] = (byte) 229;
    numArray17[28] = (byte) 246;
    numArray17[15] = (byte) 152;
    numArray17[16 /*0x10*/] = (byte) 86;
    numArray17[17] = (byte) 230;
    numArray17[42] = (byte) 130;
    numArray17[32 /*0x20*/] = (byte) 218;
    numArray17[20] = (byte) 209;
    numArray17[21] = (byte) 134;
    numArray17[2] = (byte) 54;
    numArray17[14] = (byte) 200;
    numArray17[24] = (byte) 161;
    numArray17[4] = (byte) 45;
    numArray17[26] = (byte) 110;
    numArray17[46] = (byte) 98;
    numArray17[35] = (byte) 19;
    numArray17[8] = (byte) 21;
    numArray17[30] = (byte) 37;
    numArray17[31 /*0x1F*/] = (byte) 74;
    numArray17[54] = (byte) 50;
    numArray17[33] = (byte) 1;
    numArray17[34] = (byte) 232;
    numArray17[41] = (byte) 118;
    numArray17[36] = (byte) 232;
    numArray17[0] = (byte) 177;
    numArray17[38] = (byte) 111;
    numArray17[12] = (byte) 83;
    numArray17[1] = (byte) 13;
    numArray17[5] = (byte) 143;
    numArray17[18] = (byte) 105;
    numArray17[43] = (byte) 3;
    numArray17[47] = (byte) 57;
    numArray17[39] = (byte) 131;
    numArray17[19] = (byte) 50;
    numArray17[13] = (byte) 245;
    numArray17[40] = (byte) 39;
    numArray17[52] = (byte) 185;
    numArray17[10] = (byte) 115;
    numArray17[51] = (byte) 239;
    numArray17[11] = (byte) 240 /*0xF0*/;
    numArray17[53] = (byte) 146;
    numArray17[22] = (byte) 67;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray11, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 110] ^= numArray17[index];
    byte[] numArray18 = new byte[19];
    numArray18[10] = (byte) 195;
    numArray18[3] = (byte) 216;
    numArray18[11] = (byte) 4;
    numArray18[18] = (byte) 148;
    numArray18[6] = (byte) 38;
    numArray18[5] = (byte) 201;
    numArray18[1] = (byte) 178;
    numArray18[2] = (byte) 141;
    numArray18[15] = (byte) 254;
    numArray18[9] = (byte) 233;
    numArray18[8] = (byte) 36;
    numArray18[4] = (byte) 131;
    numArray18[0] = (byte) 214;
    numArray18[16 /*0x10*/] = (byte) 134;
    numArray18[14] = (byte) 187;
    numArray18[7] = (byte) 154;
    numArray18[12] = (byte) 214;
    numArray18[17] = (byte) 186;
    numArray18[13] = (byte) 136;
    byte[] numArray19 = new byte[19]
    {
      (byte) 40,
      (byte) 128 /*0x80*/,
      (byte) 120,
      (byte) 127 /*0x7F*/,
      (byte) 224 /*0xE0*/,
      (byte) 77,
      (byte) 108,
      (byte) 233,
      (byte) 228,
      (byte) 193,
      byte.MaxValue,
      (byte) 89,
      (byte) 141,
      (byte) 74,
      (byte) 129,
      (byte) 11,
      (byte) 59,
      (byte) 177,
      (byte) 224 /*0xE0*/
    };
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray11, 165, 19);
    for (int index = 0; index < 19; ++index)
      numArray11[index + 165] ^= numArray19[index];
    return Encoding.UTF8.GetString(numArray11);
  }

  internal static string ssp_appserver_13749()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[173];
      byte[] numArray2 = new byte[55];
      numArray2[52] = (byte) 12;
      numArray2[8] = (byte) 150;
      numArray2[2] = (byte) 22;
      numArray2[3] = (byte) 164;
      numArray2[54] = (byte) 25;
      numArray2[5] = (byte) 16 /*0x10*/;
      numArray2[49] = (byte) 99;
      numArray2[26] = (byte) 249;
      numArray2[17] = (byte) 141;
      numArray2[31 /*0x1F*/] = (byte) 10;
      numArray2[9] = (byte) 9;
      numArray2[11] = (byte) 197;
      numArray2[24] = (byte) 0;
      numArray2[30] = (byte) 15;
      numArray2[21] = (byte) 5;
      numArray2[15] = (byte) 73;
      numArray2[16 /*0x10*/] = (byte) 1;
      numArray2[20] = (byte) 150;
      numArray2[22] = (byte) 123;
      numArray2[32 /*0x20*/] = (byte) 40;
      numArray2[33] = (byte) 178;
      numArray2[25] = (byte) 90;
      numArray2[35] = (byte) 54;
      numArray2[45] = (byte) 63 /*0x3F*/;
      numArray2[1] = (byte) 88;
      numArray2[6] = (byte) 144 /*0x90*/;
      numArray2[7] = (byte) 104;
      numArray2[19] = (byte) 108;
      numArray2[28] = (byte) 82;
      numArray2[29] = (byte) 72;
      numArray2[13] = (byte) 198;
      numArray2[39] = (byte) 58;
      numArray2[47] = (byte) 200;
      numArray2[0] = (byte) 57;
      numArray2[34] = (byte) 214;
      numArray2[4] = (byte) 87;
      numArray2[36] = (byte) 46;
      numArray2[37] = (byte) 50;
      numArray2[38] = (byte) 158;
      numArray2[43] = (byte) 101;
      numArray2[12] = (byte) 22;
      numArray2[40] = (byte) 41;
      numArray2[42] = (byte) 136;
      numArray2[14] = (byte) 35;
      numArray2[44] = (byte) 102;
      numArray2[18] = (byte) 126;
      numArray2[46] = (byte) 101;
      numArray2[27] = (byte) 235;
      numArray2[48 /*0x30*/] = (byte) 172;
      numArray2[41] = (byte) 155;
      numArray2[23] = (byte) 82;
      numArray2[51] = (byte) 29;
      numArray2[10] = (byte) 10;
      numArray2[53] = (byte) 58;
      numArray2[50] = (byte) 60;
      byte[] numArray3 = new byte[55]
      {
        (byte) 42,
        (byte) 129,
        (byte) 234,
        (byte) 167,
        (byte) 19,
        (byte) 175,
        (byte) 32 /*0x20*/,
        (byte) 143,
        (byte) 75,
        (byte) 177,
        (byte) 154,
        (byte) 239,
        (byte) 252,
        (byte) 52,
        (byte) 6,
        (byte) 19,
        (byte) 184,
        (byte) 244,
        (byte) 181,
        (byte) 105,
        (byte) 75,
        (byte) 191,
        (byte) 108,
        (byte) 167,
        (byte) 235,
        (byte) 76,
        (byte) 28,
        (byte) 80 /*0x50*/,
        (byte) 104,
        (byte) 131,
        (byte) 147,
        (byte) 150,
        (byte) 15,
        (byte) 0,
        (byte) 150,
        (byte) 65,
        (byte) 102,
        (byte) 51,
        (byte) 119,
        (byte) 159,
        (byte) 87,
        (byte) 243,
        (byte) 219,
        (byte) 199,
        byte.MaxValue,
        byte.MaxValue,
        (byte) 88,
        (byte) 233,
        (byte) 28,
        (byte) 192 /*0xC0*/,
        (byte) 204,
        (byte) 123,
        (byte) 28,
        (byte) 139,
        (byte) 8
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 215,
        (byte) 227,
        (byte) 8,
        (byte) 231,
        (byte) 103,
        (byte) 38,
        (byte) 65,
        (byte) 7,
        (byte) 2,
        (byte) 174,
        (byte) 203,
        (byte) 76,
        (byte) 148,
        (byte) 175,
        (byte) 65,
        (byte) 77,
        (byte) 33,
        (byte) 191,
        (byte) 116,
        (byte) 66,
        (byte) 169,
        (byte) 133,
        (byte) 30,
        (byte) 242,
        (byte) 87,
        (byte) 127 /*0x7F*/,
        (byte) 166,
        (byte) 159,
        (byte) 195,
        (byte) 53,
        (byte) 246,
        (byte) 121,
        (byte) 236,
        (byte) 145,
        (byte) 147,
        (byte) 169,
        (byte) 152,
        (byte) 59,
        (byte) 28,
        (byte) 95,
        (byte) 248,
        (byte) 100,
        (byte) 100,
        (byte) 243,
        (byte) 54,
        (byte) 139,
        (byte) 43,
        (byte) 153,
        (byte) 43,
        (byte) 225,
        (byte) 166,
        (byte) 56,
        (byte) 170,
        (byte) 116,
        (byte) 29
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 23,
        (byte) 207,
        (byte) 80 /*0x50*/,
        (byte) 234,
        (byte) 56,
        (byte) 253,
        (byte) 134,
        (byte) 214,
        (byte) 79,
        (byte) 17,
        (byte) 95,
        (byte) 249,
        (byte) 130,
        (byte) 51,
        (byte) 131,
        (byte) 101,
        (byte) 47,
        (byte) 212,
        (byte) 111,
        (byte) 122,
        (byte) 239,
        (byte) 138,
        (byte) 7,
        (byte) 57,
        (byte) 59,
        (byte) 16 /*0x10*/,
        (byte) 238,
        (byte) 169,
        (byte) 14,
        (byte) 72,
        (byte) 185,
        (byte) 123,
        (byte) 113,
        (byte) 15,
        (byte) 177,
        (byte) 219,
        (byte) 247,
        (byte) 187,
        (byte) 48 /*0x30*/,
        (byte) 236,
        (byte) 35,
        (byte) 86,
        (byte) 174,
        (byte) 218,
        (byte) 37,
        (byte) 102,
        (byte) 0,
        (byte) 4,
        (byte) 124,
        (byte) 196,
        (byte) 170,
        (byte) 154,
        (byte) 67,
        (byte) 216,
        (byte) 199
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 21,
        (byte) 143,
        (byte) 232,
        (byte) 127 /*0x7F*/,
        (byte) 162,
        (byte) 67,
        (byte) 204,
        (byte) 183,
        (byte) 82,
        (byte) 96 /*0x60*/,
        (byte) 2,
        (byte) 28,
        (byte) 152,
        (byte) 180,
        (byte) 230,
        (byte) 162,
        (byte) 20,
        (byte) 73,
        (byte) 69,
        (byte) 33,
        (byte) 42,
        (byte) 188,
        (byte) 204,
        (byte) 84,
        (byte) 13,
        (byte) 115,
        (byte) 225,
        (byte) 142,
        (byte) 190,
        (byte) 206,
        (byte) 119,
        (byte) 115,
        (byte) 64 /*0x40*/,
        (byte) 228,
        (byte) 130,
        (byte) 172,
        (byte) 52,
        (byte) 224 /*0xE0*/,
        (byte) 85,
        (byte) 32 /*0x20*/,
        (byte) 151,
        (byte) 59,
        (byte) 228,
        (byte) 42,
        (byte) 40,
        (byte) 124,
        (byte) 147,
        (byte) 44,
        (byte) 192 /*0xC0*/,
        (byte) 149,
        (byte) 149,
        (byte) 42,
        (byte) 216,
        (byte) 49,
        (byte) 154
      };
      byte[] numArray7 = new byte[55];
      numArray7[32 /*0x20*/] = (byte) 217;
      numArray7[1] = (byte) 238;
      numArray7[7] = (byte) 156;
      numArray7[0] = (byte) 251;
      numArray7[35] = (byte) 75;
      numArray7[30] = (byte) 248;
      numArray7[6] = (byte) 95;
      numArray7[38] = (byte) 34;
      numArray7[8] = (byte) 154;
      numArray7[9] = (byte) 233;
      numArray7[13] = (byte) 10;
      numArray7[20] = (byte) 229;
      numArray7[12] = (byte) 94;
      numArray7[5] = (byte) 0;
      numArray7[14] = (byte) 26;
      numArray7[10] = (byte) 9;
      numArray7[2] = (byte) 128 /*0x80*/;
      numArray7[54] = (byte) 35;
      numArray7[46] = (byte) 48 /*0x30*/;
      numArray7[37] = (byte) 7;
      numArray7[34] = (byte) 228;
      numArray7[40] = (byte) 45;
      numArray7[16 /*0x10*/] = (byte) 230;
      numArray7[23] = (byte) 64 /*0x40*/;
      numArray7[24] = (byte) 8;
      numArray7[17] = (byte) 118;
      numArray7[18] = (byte) 76;
      numArray7[27] = (byte) 119;
      numArray7[28] = (byte) 38;
      numArray7[51] = (byte) 103;
      numArray7[43] = (byte) 252;
      numArray7[31 /*0x1F*/] = (byte) 58;
      numArray7[25] = (byte) 156;
      numArray7[33] = (byte) 244;
      numArray7[45] = (byte) 227;
      numArray7[52] = (byte) 97;
      numArray7[11] = (byte) 76;
      numArray7[36] = (byte) 201;
      numArray7[3] = (byte) 13;
      numArray7[22] = (byte) 152;
      numArray7[4] = (byte) 221;
      numArray7[41] = (byte) 128 /*0x80*/;
      numArray7[19] = (byte) 46;
      numArray7[26] = (byte) 197;
      numArray7[44] = (byte) 242;
      numArray7[21] = (byte) 23;
      numArray7[39] = (byte) 85;
      numArray7[47] = (byte) 173;
      numArray7[48 /*0x30*/] = (byte) 39;
      numArray7[53] = (byte) 149;
      numArray7[42] = (byte) 79;
      numArray7[50] = (byte) 108;
      numArray7[15] = (byte) 234;
      numArray7[29] = (byte) 92;
      numArray7[49] = (byte) 247;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[8];
      numArray8[5] = (byte) 208 /*0xD0*/;
      numArray8[1] = (byte) 224 /*0xE0*/;
      numArray8[6] = (byte) 74;
      numArray8[2] = (byte) 78;
      numArray8[4] = (byte) 111;
      numArray8[3] = (byte) 172;
      numArray8[0] = (byte) 104;
      numArray8[7] = (byte) 156;
      byte[] numArray9 = new byte[8]
      {
        (byte) 233,
        (byte) 77,
        (byte) 90,
        (byte) 4,
        (byte) 137,
        (byte) 202,
        (byte) 167,
        (byte) 29
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[173];
    byte[] numArray11 = new byte[55]
    {
      (byte) 119,
      (byte) 95,
      (byte) 145,
      (byte) 239,
      (byte) 0,
      (byte) 186,
      (byte) 71,
      (byte) 2,
      (byte) 215,
      (byte) 21,
      (byte) 230,
      (byte) 104,
      (byte) 158,
      (byte) 168,
      (byte) 199,
      (byte) 55,
      (byte) 141,
      (byte) 215,
      (byte) 17,
      (byte) 233,
      (byte) 114,
      (byte) 22,
      (byte) 165,
      (byte) 166,
      (byte) 214,
      (byte) 117,
      (byte) 242,
      (byte) 52,
      (byte) 88,
      (byte) 154,
      (byte) 106,
      (byte) 78,
      (byte) 118,
      byte.MaxValue,
      (byte) 9,
      (byte) 194,
      (byte) 214,
      (byte) 241,
      (byte) 78,
      (byte) 242,
      (byte) 39,
      (byte) 205,
      (byte) 114,
      (byte) 135,
      (byte) 12,
      (byte) 98,
      (byte) 203,
      (byte) 95,
      (byte) 137,
      (byte) 150,
      (byte) 56,
      (byte) 244,
      (byte) 83,
      (byte) 70,
      (byte) 58
    };
    byte[] numArray12 = new byte[55];
    numArray12[32 /*0x20*/] = (byte) 250;
    numArray12[52] = (byte) 103;
    numArray12[2] = (byte) 70;
    numArray12[36] = (byte) 107;
    numArray12[4] = (byte) 220;
    numArray12[9] = (byte) 203;
    numArray12[14] = (byte) 72;
    numArray12[20] = (byte) 200;
    numArray12[8] = (byte) 52;
    numArray12[27] = (byte) 56;
    numArray12[11] = (byte) 197;
    numArray12[42] = (byte) 155;
    numArray12[12] = (byte) 6;
    numArray12[13] = (byte) 148;
    numArray12[45] = (byte) 35;
    numArray12[15] = (byte) 38;
    numArray12[50] = (byte) 135;
    numArray12[17] = (byte) 125;
    numArray12[7] = (byte) 99;
    numArray12[40] = (byte) 4;
    numArray12[33] = (byte) 12;
    numArray12[5] = (byte) 136;
    numArray12[24] = (byte) 199;
    numArray12[30] = (byte) 83;
    numArray12[53] = (byte) 10;
    numArray12[25] = (byte) 140;
    numArray12[47] = (byte) 197;
    numArray12[28] = (byte) 155;
    numArray12[29] = (byte) 44;
    numArray12[34] = (byte) 179;
    numArray12[22] = (byte) 195;
    numArray12[6] = (byte) 16 /*0x10*/;
    numArray12[23] = (byte) 66;
    numArray12[39] = (byte) 122;
    numArray12[3] = (byte) 154;
    numArray12[35] = (byte) 129;
    numArray12[0] = (byte) 143;
    numArray12[37] = (byte) 159;
    numArray12[38] = (byte) 108;
    numArray12[41] = (byte) 3;
    numArray12[46] = (byte) 222;
    numArray12[26] = (byte) 72;
    numArray12[1] = (byte) 77;
    numArray12[43] = (byte) 201;
    numArray12[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    numArray12[21] = (byte) 178;
    numArray12[10] = (byte) 16 /*0x10*/;
    numArray12[18] = (byte) 170;
    numArray12[49] = (byte) 44;
    numArray12[19] = (byte) 11;
    numArray12[44] = (byte) 8;
    numArray12[16 /*0x10*/] = (byte) 158;
    numArray12[51] = (byte) 19;
    numArray12[48 /*0x30*/] = (byte) 70;
    numArray12[54] = (byte) 73;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 82,
      (byte) 165,
      (byte) 224 /*0xE0*/,
      (byte) 53,
      (byte) 103,
      (byte) 231,
      (byte) 219,
      (byte) 53,
      (byte) 157,
      (byte) 194,
      (byte) 217,
      (byte) 247,
      (byte) 252,
      (byte) 117,
      (byte) 195,
      (byte) 197,
      (byte) 244,
      (byte) 172,
      (byte) 128 /*0x80*/,
      (byte) 110,
      (byte) 66,
      (byte) 36,
      (byte) 2,
      (byte) 125,
      (byte) 210,
      (byte) 38,
      (byte) 140,
      (byte) 197,
      (byte) 9,
      (byte) 198,
      (byte) 170,
      (byte) 47,
      (byte) 234,
      (byte) 108,
      (byte) 152,
      (byte) 50,
      (byte) 227,
      (byte) 245,
      (byte) 173,
      (byte) 60,
      (byte) 89,
      (byte) 166,
      (byte) 215,
      (byte) 174,
      (byte) 82,
      (byte) 201,
      (byte) 135,
      (byte) 191,
      (byte) 216,
      (byte) 165,
      (byte) 63 /*0x3F*/,
      (byte) 216,
      (byte) 20,
      (byte) 79,
      (byte) 89
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 125,
      (byte) 46,
      (byte) 100,
      (byte) 207,
      (byte) 113,
      (byte) 236,
      (byte) 196,
      (byte) 153,
      (byte) 158,
      (byte) 187,
      (byte) 218,
      (byte) 111,
      (byte) 15,
      (byte) 84,
      (byte) 44,
      (byte) 71,
      (byte) 228,
      (byte) 92,
      (byte) 155,
      (byte) 235,
      (byte) 155,
      (byte) 156,
      (byte) 180,
      (byte) 163,
      (byte) 55,
      (byte) 26,
      (byte) 22,
      (byte) 77,
      (byte) 100,
      (byte) 131,
      (byte) 248,
      (byte) 215,
      (byte) 227,
      (byte) 56,
      (byte) 209,
      (byte) 12,
      (byte) 46,
      (byte) 226,
      (byte) 35,
      (byte) 231,
      (byte) 217,
      (byte) 129,
      (byte) 87,
      (byte) 191,
      (byte) 220,
      (byte) 115,
      (byte) 29,
      (byte) 52,
      (byte) 252,
      (byte) 176 /*0xB0*/,
      (byte) 101,
      (byte) 33,
      (byte) 246,
      (byte) 58,
      (byte) 27
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[24] = (byte) 204;
    numArray15[54] = (byte) 79;
    numArray15[2] = (byte) 64 /*0x40*/;
    numArray15[3] = (byte) 91;
    numArray15[50] = (byte) 208 /*0xD0*/;
    numArray15[5] = (byte) 43;
    numArray15[6] = (byte) 214;
    numArray15[7] = (byte) 23;
    numArray15[42] = (byte) 168;
    numArray15[14] = byte.MaxValue;
    numArray15[10] = (byte) 144 /*0x90*/;
    numArray15[21] = (byte) 42;
    numArray15[12] = (byte) 145;
    numArray15[13] = (byte) 226;
    numArray15[29] = (byte) 26;
    numArray15[4] = (byte) 22;
    numArray15[51] = (byte) 58;
    numArray15[17] = (byte) 20;
    numArray15[18] = (byte) 155;
    numArray15[19] = (byte) 76;
    numArray15[20] = (byte) 83;
    numArray15[15] = byte.MaxValue;
    numArray15[22] = (byte) 8;
    numArray15[23] = (byte) 159;
    numArray15[8] = (byte) 170;
    numArray15[41] = (byte) 151;
    numArray15[26] = (byte) 100;
    numArray15[16 /*0x10*/] = (byte) 68;
    numArray15[28] = (byte) 210;
    numArray15[9] = (byte) 13;
    numArray15[36] = (byte) 108;
    numArray15[49] = (byte) 216;
    numArray15[32 /*0x20*/] = (byte) 106;
    numArray15[44] = (byte) 247;
    numArray15[34] = (byte) 128 /*0x80*/;
    numArray15[35] = (byte) 146;
    numArray15[43] = (byte) 188;
    numArray15[37] = (byte) 225;
    numArray15[45] = (byte) 226;
    numArray15[1] = (byte) 98;
    numArray15[40] = (byte) 0;
    numArray15[48 /*0x30*/] = (byte) 64 /*0x40*/;
    numArray15[25] = (byte) 236;
    numArray15[0] = (byte) 99;
    numArray15[11] = (byte) 221;
    numArray15[39] = (byte) 123;
    numArray15[46] = (byte) 71;
    numArray15[47] = (byte) 195;
    numArray15[38] = (byte) 67;
    numArray15[31 /*0x1F*/] = (byte) 19;
    numArray15[53] = (byte) 225;
    numArray15[27] = (byte) 134;
    numArray15[52] = (byte) 189;
    numArray15[30] = (byte) 154;
    numArray15[33] = (byte) 143;
    byte[] numArray16 = new byte[55];
    numArray16[39] = (byte) 38;
    numArray16[0] = (byte) 106;
    numArray16[2] = (byte) 120;
    numArray16[6] = (byte) 42;
    numArray16[4] = (byte) 156;
    numArray16[51] = (byte) 215;
    numArray16[53] = (byte) 2;
    numArray16[36] = (byte) 88;
    numArray16[8] = (byte) 57;
    numArray16[23] = (byte) 110;
    numArray16[54] = (byte) 145;
    numArray16[14] = (byte) 110;
    numArray16[9] = (byte) 209;
    numArray16[12] = (byte) 156;
    numArray16[13] = (byte) 45;
    numArray16[46] = (byte) 183;
    numArray16[16 /*0x10*/] = (byte) 141;
    numArray16[17] = (byte) 35;
    numArray16[18] = (byte) 36;
    numArray16[41] = (byte) 204;
    numArray16[20] = (byte) 91;
    numArray16[21] = (byte) 87;
    numArray16[22] = (byte) 7;
    numArray16[28] = (byte) 241;
    numArray16[30] = (byte) 35;
    numArray16[25] = (byte) 58;
    numArray16[1] = (byte) 232;
    numArray16[42] = (byte) 84;
    numArray16[7] = (byte) 57;
    numArray16[24] = (byte) 15;
    numArray16[3] = (byte) 56;
    numArray16[31 /*0x1F*/] = (byte) 184;
    numArray16[32 /*0x20*/] = (byte) 43;
    numArray16[33] = (byte) 2;
    numArray16[27] = (byte) 157;
    numArray16[26] = (byte) 9;
    numArray16[5] = (byte) 38;
    numArray16[37] = (byte) 174;
    numArray16[38] = (byte) 231;
    numArray16[11] = (byte) 19;
    numArray16[35] = (byte) 251;
    numArray16[40] = (byte) 198;
    numArray16[15] = (byte) 17;
    numArray16[34] = (byte) 139;
    numArray16[44] = (byte) 42;
    numArray16[45] = (byte) 219;
    numArray16[43] = (byte) 161;
    numArray16[47] = (byte) 218;
    numArray16[48 /*0x30*/] = (byte) 16 /*0x10*/;
    numArray16[49] = (byte) 195;
    numArray16[50] = (byte) 163;
    numArray16[10] = (byte) 93;
    numArray16[52] = (byte) 223;
    numArray16[19] = (byte) 109;
    numArray16[29] = (byte) 104;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[8];
    numArray17[7] = (byte) 28;
    numArray17[4] = (byte) 151;
    numArray17[2] = (byte) 37;
    numArray17[3] = (byte) 232;
    numArray17[1] = (byte) 41;
    numArray17[5] = (byte) 149;
    numArray17[6] = (byte) 103;
    numArray17[0] = (byte) 203;
    byte[] numArray18 = new byte[8]
    {
      (byte) 150,
      (byte) 19,
      (byte) 141,
      (byte) 144 /*0x90*/,
      (byte) 216,
      (byte) 113,
      (byte) 212,
      (byte) 53
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 8);
    for (int index = 0; index < 8; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13750()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[185];
      byte[] numArray2 = new byte[55]
      {
        (byte) 204,
        (byte) 218,
        (byte) 245,
        (byte) 220,
        (byte) 247,
        (byte) 209,
        (byte) 58,
        (byte) 129,
        (byte) 67,
        (byte) 133,
        (byte) 247,
        (byte) 130,
        (byte) 67,
        (byte) 34,
        (byte) 5,
        (byte) 133,
        (byte) 112 /*0x70*/,
        (byte) 164,
        (byte) 14,
        (byte) 112 /*0x70*/,
        (byte) 109,
        (byte) 26,
        (byte) 227,
        (byte) 140,
        (byte) 107,
        (byte) 189,
        (byte) 153,
        (byte) 131,
        (byte) 251,
        (byte) 145,
        (byte) 82,
        (byte) 5,
        (byte) 197,
        (byte) 184,
        (byte) 61,
        (byte) 240 /*0xF0*/,
        (byte) 51,
        (byte) 72,
        (byte) 240 /*0xF0*/,
        (byte) 198,
        (byte) 91,
        (byte) 230,
        (byte) 217,
        (byte) 233,
        (byte) 0,
        (byte) 149,
        (byte) 69,
        (byte) 229,
        (byte) 242,
        (byte) 219,
        (byte) 177,
        (byte) 58,
        (byte) 231,
        (byte) 242,
        (byte) 39
      };
      byte[] numArray3 = new byte[55];
      numArray3[23] = (byte) 176 /*0xB0*/;
      numArray3[3] = (byte) 78;
      numArray3[44] = (byte) 228;
      numArray3[26] = (byte) 92;
      numArray3[20] = (byte) 89;
      numArray3[12] = (byte) 129;
      numArray3[14] = (byte) 216;
      numArray3[0] = (byte) 213;
      numArray3[50] = (byte) 216;
      numArray3[47] = (byte) 18;
      numArray3[7] = (byte) 91;
      numArray3[11] = (byte) 126;
      numArray3[38] = (byte) 160 /*0xA0*/;
      numArray3[13] = (byte) 55;
      numArray3[54] = (byte) 171;
      numArray3[24] = (byte) 141;
      numArray3[52] = (byte) 103;
      numArray3[17] = (byte) 216;
      numArray3[18] = (byte) 241;
      numArray3[19] = (byte) 41;
      numArray3[41] = (byte) 75;
      numArray3[16 /*0x10*/] = (byte) 70;
      numArray3[22] = (byte) 136;
      numArray3[1] = (byte) 151;
      numArray3[25] = (byte) 154;
      numArray3[49] = (byte) 23;
      numArray3[21] = (byte) 11;
      numArray3[27] = (byte) 85;
      numArray3[40] = (byte) 72;
      numArray3[8] = (byte) 129;
      numArray3[28] = (byte) 35;
      numArray3[31 /*0x1F*/] = (byte) 226;
      numArray3[32 /*0x20*/] = (byte) 163;
      numArray3[33] = (byte) 3;
      numArray3[34] = (byte) 202;
      numArray3[35] = (byte) 23;
      numArray3[29] = (byte) 179;
      numArray3[5] = (byte) 164;
      numArray3[36] = (byte) 197;
      numArray3[39] = (byte) 38;
      numArray3[15] = (byte) 37;
      numArray3[10] = (byte) 107;
      numArray3[42] = (byte) 147;
      numArray3[43] = (byte) 179;
      numArray3[9] = (byte) 48 /*0x30*/;
      numArray3[45] = (byte) 22;
      numArray3[46] = (byte) 3;
      numArray3[4] = (byte) 1;
      numArray3[30] = (byte) 107;
      numArray3[6] = (byte) 142;
      numArray3[48 /*0x30*/] = (byte) 239;
      numArray3[51] = (byte) 207;
      numArray3[2] = (byte) 154;
      numArray3[53] = (byte) 74;
      numArray3[37] = (byte) 195;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 219,
        (byte) 153,
        (byte) 194,
        (byte) 132,
        (byte) 37,
        (byte) 101,
        (byte) 210,
        (byte) 27,
        (byte) 213,
        (byte) 233,
        (byte) 251,
        (byte) 89,
        (byte) 27,
        (byte) 135,
        (byte) 78,
        (byte) 254,
        (byte) 186,
        (byte) 112 /*0x70*/,
        (byte) 38,
        (byte) 26,
        (byte) 66,
        (byte) 127 /*0x7F*/,
        (byte) 254,
        (byte) 190,
        (byte) 180,
        (byte) 179,
        (byte) 150,
        (byte) 85,
        (byte) 146,
        (byte) 126,
        (byte) 245,
        (byte) 239,
        (byte) 26,
        (byte) 250,
        (byte) 238,
        (byte) 63 /*0x3F*/,
        (byte) 244,
        (byte) 8,
        (byte) 127 /*0x7F*/,
        (byte) 251,
        (byte) 83,
        (byte) 155,
        (byte) 56,
        (byte) 239,
        (byte) 189,
        (byte) 166,
        (byte) 193,
        (byte) 1,
        (byte) 59,
        (byte) 135,
        (byte) 228,
        (byte) 109,
        (byte) 122,
        (byte) 86,
        (byte) 187
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 118,
        (byte) 5,
        (byte) 235,
        (byte) 191,
        (byte) 21,
        (byte) 199,
        (byte) 112 /*0x70*/,
        (byte) 18,
        (byte) 22,
        (byte) 14,
        (byte) 105,
        (byte) 230,
        (byte) 89,
        (byte) 148,
        (byte) 9,
        (byte) 85,
        (byte) 223,
        (byte) 153,
        (byte) 102,
        (byte) 231,
        (byte) 251,
        (byte) 235,
        (byte) 96 /*0x60*/,
        (byte) 221,
        (byte) 100,
        (byte) 13,
        (byte) 109,
        (byte) 11,
        (byte) 146,
        (byte) 204,
        (byte) 187,
        (byte) 209,
        (byte) 131,
        (byte) 141,
        (byte) 99,
        (byte) 80 /*0x50*/,
        (byte) 71,
        (byte) 34,
        (byte) 179,
        (byte) 161,
        (byte) 210,
        (byte) 81,
        (byte) 171,
        (byte) 53,
        (byte) 243,
        (byte) 44,
        (byte) 197,
        (byte) 83,
        (byte) 133,
        (byte) 3,
        (byte) 54,
        (byte) 119,
        (byte) 219,
        (byte) 221,
        (byte) 54
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 3,
        (byte) 208 /*0xD0*/,
        (byte) 113,
        (byte) 206,
        (byte) 124,
        (byte) 136,
        (byte) 86,
        (byte) 243,
        (byte) 12,
        (byte) 63 /*0x3F*/,
        (byte) 33,
        (byte) 152,
        (byte) 15,
        (byte) 10,
        (byte) 177,
        (byte) 240 /*0xF0*/,
        (byte) 43,
        (byte) 198,
        (byte) 166,
        (byte) 71,
        (byte) 14,
        (byte) 0,
        (byte) 14,
        (byte) 242,
        (byte) 149,
        (byte) 26,
        (byte) 10,
        (byte) 154,
        (byte) 136,
        (byte) 116,
        (byte) 195,
        (byte) 62,
        (byte) 146,
        (byte) 163,
        (byte) 240 /*0xF0*/,
        (byte) 56,
        (byte) 14,
        (byte) 44,
        (byte) 215,
        (byte) 18,
        (byte) 225,
        (byte) 252,
        (byte) 177,
        (byte) 102,
        (byte) 160 /*0xA0*/,
        (byte) 104,
        (byte) 43,
        (byte) 125,
        (byte) 50,
        (byte) 124,
        (byte) 78,
        (byte) 144 /*0x90*/,
        (byte) 21,
        (byte) 20,
        (byte) 141
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 2,
        (byte) 15,
        (byte) 147,
        (byte) 108,
        (byte) 54,
        (byte) 30,
        (byte) 95,
        (byte) 103,
        (byte) 104,
        (byte) 230,
        (byte) 228,
        (byte) 11,
        (byte) 79,
        (byte) 241,
        (byte) 112 /*0x70*/,
        (byte) 88,
        (byte) 206,
        (byte) 212,
        (byte) 196,
        (byte) 127 /*0x7F*/,
        (byte) 92,
        (byte) 47,
        (byte) 189,
        (byte) 210,
        (byte) 97,
        (byte) 151,
        (byte) 153,
        (byte) 42,
        (byte) 143,
        (byte) 30,
        (byte) 224 /*0xE0*/,
        (byte) 220,
        (byte) 50,
        (byte) 4,
        (byte) 215,
        (byte) 171,
        (byte) 29,
        (byte) 63 /*0x3F*/,
        (byte) 81,
        (byte) 109,
        (byte) 2,
        byte.MaxValue,
        (byte) 75,
        (byte) 11,
        (byte) 151,
        (byte) 239,
        (byte) 153,
        (byte) 15,
        (byte) 32 /*0x20*/,
        (byte) 180,
        (byte) 236,
        (byte) 108,
        (byte) 103,
        (byte) 235,
        (byte) 250
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[20];
      numArray8[9] = (byte) 6;
      numArray8[2] = (byte) 97;
      numArray8[16 /*0x10*/] = (byte) 148;
      numArray8[13] = (byte) 226;
      numArray8[4] = (byte) 188;
      numArray8[14] = (byte) 254;
      numArray8[18] = (byte) 17;
      numArray8[7] = (byte) 124;
      numArray8[8] = (byte) 226;
      numArray8[5] = (byte) 107;
      numArray8[19] = (byte) 78;
      numArray8[6] = (byte) 151;
      numArray8[12] = (byte) 121;
      numArray8[10] = (byte) 108;
      numArray8[3] = (byte) 126;
      numArray8[15] = (byte) 239;
      numArray8[0] = (byte) 126;
      numArray8[17] = (byte) 101;
      numArray8[11] = (byte) 25;
      numArray8[1] = (byte) 56;
      byte[] numArray9 = new byte[20];
      numArray9[11] = (byte) 123;
      numArray9[1] = (byte) 155;
      numArray9[4] = (byte) 155;
      numArray9[3] = (byte) 120;
      numArray9[14] = (byte) 214;
      numArray9[5] = (byte) 152;
      numArray9[8] = (byte) 227;
      numArray9[10] = (byte) 191;
      numArray9[16 /*0x10*/] = (byte) 20;
      numArray9[9] = (byte) 80 /*0x50*/;
      numArray9[17] = (byte) 47;
      numArray9[15] = (byte) 26;
      numArray9[12] = (byte) 144 /*0x90*/;
      numArray9[2] = (byte) 81;
      numArray9[13] = (byte) 32 /*0x20*/;
      numArray9[0] = (byte) 242;
      numArray9[6] = (byte) 66;
      numArray9[7] = (byte) 0;
      numArray9[18] = (byte) 81;
      numArray9[19] = (byte) 20;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[185];
    byte[] numArray11 = new byte[55]
    {
      (byte) 184,
      (byte) 136,
      (byte) 85,
      (byte) 43,
      (byte) 3,
      (byte) 83,
      (byte) 10,
      (byte) 122,
      (byte) 0,
      (byte) 63 /*0x3F*/,
      (byte) 128 /*0x80*/,
      (byte) 52,
      (byte) 189,
      (byte) 148,
      (byte) 201,
      (byte) 48 /*0x30*/,
      (byte) 41,
      (byte) 173,
      (byte) 113,
      (byte) 213,
      (byte) 146,
      (byte) 169,
      (byte) 206,
      (byte) 154,
      (byte) 57,
      (byte) 91,
      (byte) 98,
      (byte) 231,
      (byte) 2,
      (byte) 163,
      (byte) 208 /*0xD0*/,
      (byte) 49,
      (byte) 168,
      (byte) 161,
      (byte) 5,
      (byte) 218,
      (byte) 104,
      (byte) 55,
      (byte) 182,
      (byte) 70,
      (byte) 69,
      (byte) 38,
      (byte) 128 /*0x80*/,
      (byte) 111,
      (byte) 250,
      (byte) 127 /*0x7F*/,
      (byte) 92,
      (byte) 152,
      (byte) 66,
      (byte) 65,
      (byte) 199,
      (byte) 167,
      (byte) 238,
      (byte) 155,
      (byte) 181
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 104,
      (byte) 65,
      (byte) 129,
      (byte) 217,
      (byte) 183,
      (byte) 124,
      (byte) 22,
      (byte) 241,
      (byte) 11,
      (byte) 241,
      (byte) 219,
      (byte) 81,
      (byte) 178,
      (byte) 34,
      (byte) 192 /*0xC0*/,
      (byte) 36,
      (byte) 157,
      (byte) 84,
      (byte) 203,
      (byte) 171,
      (byte) 223,
      (byte) 138,
      (byte) 202,
      (byte) 28,
      (byte) 83,
      (byte) 143,
      (byte) 42,
      (byte) 173,
      (byte) 165,
      (byte) 100,
      (byte) 215,
      (byte) 117,
      (byte) 42,
      (byte) 40,
      (byte) 34,
      (byte) 116,
      (byte) 165,
      (byte) 52,
      (byte) 122,
      (byte) 130,
      (byte) 179,
      (byte) 196,
      (byte) 102,
      (byte) 45,
      (byte) 176 /*0xB0*/,
      (byte) 78,
      (byte) 69,
      (byte) 193,
      (byte) 197,
      (byte) 165,
      (byte) 73,
      (byte) 163,
      (byte) 142,
      (byte) 16 /*0x10*/,
      (byte) 68
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 186,
      (byte) 242,
      (byte) 168,
      (byte) 241,
      (byte) 35,
      (byte) 119,
      (byte) 92,
      (byte) 110,
      (byte) 79,
      (byte) 140,
      (byte) 28,
      (byte) 142,
      (byte) 238,
      (byte) 199,
      (byte) 241,
      (byte) 247,
      (byte) 42,
      (byte) 144 /*0x90*/,
      (byte) 108,
      (byte) 40,
      (byte) 95,
      (byte) 160 /*0xA0*/,
      (byte) 186,
      (byte) 254,
      (byte) 201,
      (byte) 164,
      (byte) 193,
      (byte) 219,
      (byte) 41,
      (byte) 66,
      (byte) 12,
      (byte) 235,
      (byte) 245,
      (byte) 33,
      (byte) 174,
      (byte) 69,
      (byte) 13,
      (byte) 85,
      (byte) 34,
      (byte) 175,
      (byte) 35,
      (byte) 0,
      (byte) 171,
      (byte) 242,
      (byte) 64 /*0x40*/,
      (byte) 10,
      (byte) 83,
      (byte) 121,
      (byte) 214,
      (byte) 175,
      (byte) 196,
      (byte) 11,
      (byte) 73,
      (byte) 231,
      (byte) 90
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 43,
      (byte) 168,
      (byte) 240 /*0xF0*/,
      (byte) 108,
      (byte) 34,
      (byte) 130,
      (byte) 229,
      (byte) 89,
      (byte) 140,
      (byte) 103,
      (byte) 252,
      (byte) 47,
      (byte) 8,
      (byte) 77,
      (byte) 121,
      (byte) 196,
      (byte) 63 /*0x3F*/,
      (byte) 113,
      (byte) 22,
      (byte) 131,
      (byte) 13,
      (byte) 154,
      (byte) 85,
      (byte) 211,
      (byte) 110,
      (byte) 253,
      (byte) 238,
      (byte) 184,
      (byte) 207,
      (byte) 136,
      (byte) 204,
      (byte) 232,
      (byte) 225,
      (byte) 150,
      (byte) 176 /*0xB0*/,
      (byte) 140,
      (byte) 213,
      (byte) 31 /*0x1F*/,
      (byte) 25,
      (byte) 72,
      (byte) 177,
      (byte) 201,
      (byte) 38,
      (byte) 156,
      (byte) 168,
      (byte) 98,
      (byte) 111,
      (byte) 61,
      (byte) 51,
      (byte) 240 /*0xF0*/,
      (byte) 46,
      (byte) 225,
      (byte) 191,
      (byte) 123,
      (byte) 72
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 169,
      (byte) 39,
      (byte) 126,
      (byte) 182,
      (byte) 129,
      (byte) 69,
      (byte) 12,
      (byte) 128 /*0x80*/,
      (byte) 49,
      (byte) 152,
      (byte) 205,
      (byte) 25,
      (byte) 171,
      (byte) 233,
      (byte) 212,
      (byte) 109,
      (byte) 189,
      (byte) 4,
      (byte) 94,
      (byte) 245,
      (byte) 199,
      (byte) 190,
      (byte) 191,
      (byte) 80 /*0x50*/,
      (byte) 71,
      (byte) 4,
      (byte) 122,
      (byte) 126,
      (byte) 0,
      (byte) 161,
      (byte) 14,
      (byte) 199,
      (byte) 147,
      (byte) 13,
      (byte) 100,
      (byte) 46,
      (byte) 163,
      (byte) 248,
      (byte) 162,
      (byte) 187,
      (byte) 158,
      (byte) 114,
      (byte) 14,
      (byte) 113,
      (byte) 134,
      (byte) 95,
      (byte) 42,
      (byte) 197,
      (byte) 239,
      (byte) 157,
      (byte) 140,
      (byte) 226,
      (byte) 251,
      (byte) 37,
      (byte) 112 /*0x70*/
    };
    byte[] numArray16 = new byte[55];
    numArray16[1] = (byte) 227;
    numArray16[34] = byte.MaxValue;
    numArray16[2] = (byte) 233;
    numArray16[33] = (byte) 195;
    numArray16[4] = (byte) 52;
    numArray16[5] = (byte) 180;
    numArray16[6] = (byte) 133;
    numArray16[7] = (byte) 30;
    numArray16[14] = (byte) 60;
    numArray16[31 /*0x1F*/] = (byte) 18;
    numArray16[43] = (byte) 200;
    numArray16[25] = (byte) 105;
    numArray16[12] = (byte) 156;
    numArray16[18] = (byte) 243;
    numArray16[8] = (byte) 40;
    numArray16[40] = (byte) 129;
    numArray16[30] = (byte) 137;
    numArray16[17] = (byte) 92;
    numArray16[50] = (byte) 140;
    numArray16[19] = (byte) 71;
    numArray16[3] = (byte) 200;
    numArray16[20] = (byte) 137;
    numArray16[22] = (byte) 156;
    numArray16[0] = (byte) 207;
    numArray16[11] = (byte) 154;
    numArray16[26] = (byte) 73;
    numArray16[54] = (byte) 48 /*0x30*/;
    numArray16[27] = (byte) 167;
    numArray16[41] = (byte) 222;
    numArray16[29] = (byte) 25;
    numArray16[39] = (byte) 87;
    numArray16[42] = (byte) 169;
    numArray16[32 /*0x20*/] = (byte) 194;
    numArray16[15] = (byte) 243;
    numArray16[23] = (byte) 94;
    numArray16[35] = (byte) 247;
    numArray16[36] = (byte) 5;
    numArray16[37] = (byte) 204;
    numArray16[38] = (byte) 10;
    numArray16[13] = (byte) 170;
    numArray16[16 /*0x10*/] = (byte) 11;
    numArray16[9] = (byte) 122;
    numArray16[10] = (byte) 81;
    numArray16[24] = (byte) 247;
    numArray16[44] = (byte) 215;
    numArray16[45] = (byte) 178;
    numArray16[21] = (byte) 195;
    numArray16[47] = (byte) 191;
    numArray16[48 /*0x30*/] = (byte) 235;
    numArray16[28] = (byte) 199;
    numArray16[49] = (byte) 46;
    numArray16[51] = (byte) 114;
    numArray16[52] = (byte) 49;
    numArray16[53] = (byte) 88;
    numArray16[46] = (byte) 122;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[20]
    {
      (byte) 99,
      (byte) 135,
      (byte) 162,
      (byte) 22,
      (byte) 254,
      (byte) 28,
      (byte) 122,
      (byte) 94,
      (byte) 58,
      (byte) 24,
      (byte) 2,
      (byte) 12,
      (byte) 223,
      (byte) 85,
      (byte) 58,
      (byte) 60,
      (byte) 178,
      (byte) 70,
      (byte) 98,
      (byte) 48 /*0x30*/
    };
    byte[] numArray18 = new byte[20]
    {
      (byte) 68,
      (byte) 75,
      (byte) 10,
      (byte) 62,
      (byte) 144 /*0x90*/,
      (byte) 236,
      (byte) 198,
      (byte) 145,
      (byte) 177,
      (byte) 122,
      (byte) 73,
      (byte) 174,
      (byte) 60,
      (byte) 16 /*0x10*/,
      (byte) 103,
      (byte) 181,
      (byte) 23,
      (byte) 149,
      (byte) 80 /*0x50*/,
      (byte) 203
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 20);
    for (int index = 0; index < 20; ++index)
      numArray10[index + 165] ^= numArray18[index];
    byte[] numArray19 = new byte[36];
    byte[] response = new byte[36];
    Array.Copy((Array) sc_13686.sspq, 881, (Array) numArray19, 0, 36);
    key.Query(true, 335, numArray19, response);
    Array.Copy((Array) sc_13686.sspr, 881, (Array) numArray19, 0, 36);
    for (int index = 0; index < numArray19.Length; ++index)
    {
      if ((int) numArray19[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13751()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[181];
      byte[] numArray2 = new byte[55]
      {
        (byte) 44,
        (byte) 178,
        (byte) 198,
        (byte) 120,
        (byte) 128 /*0x80*/,
        (byte) 206,
        (byte) 51,
        (byte) 67,
        (byte) 55,
        (byte) 185,
        (byte) 216,
        (byte) 52,
        (byte) 139,
        (byte) 44,
        (byte) 222,
        (byte) 4,
        (byte) 76,
        (byte) 196,
        (byte) 64 /*0x40*/,
        (byte) 220,
        (byte) 17,
        (byte) 192 /*0xC0*/,
        (byte) 103,
        (byte) 207,
        (byte) 23,
        (byte) 200,
        (byte) 50,
        (byte) 250,
        (byte) 248,
        (byte) 138,
        (byte) 47,
        (byte) 56,
        (byte) 88,
        (byte) 73,
        (byte) 215,
        (byte) 213,
        (byte) 127 /*0x7F*/,
        (byte) 72,
        (byte) 9,
        (byte) 101,
        (byte) 54,
        (byte) 58,
        (byte) 86,
        (byte) 138,
        (byte) 204,
        (byte) 83,
        (byte) 45,
        (byte) 221,
        (byte) 212,
        (byte) 20,
        (byte) 44,
        (byte) 142,
        (byte) 97,
        (byte) 220,
        (byte) 122
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 44,
        (byte) 157,
        (byte) 5,
        (byte) 137,
        (byte) 222,
        (byte) 231,
        (byte) 67,
        (byte) 232,
        (byte) 54,
        (byte) 154,
        (byte) 37,
        (byte) 119,
        (byte) 26,
        (byte) 164,
        (byte) 226,
        (byte) 112 /*0x70*/,
        (byte) 228,
        (byte) 57,
        (byte) 250,
        (byte) 55,
        (byte) 225,
        (byte) 245,
        (byte) 136,
        (byte) 93,
        (byte) 183,
        (byte) 247,
        (byte) 128 /*0x80*/,
        (byte) 114,
        (byte) 159,
        (byte) 101,
        (byte) 93,
        (byte) 190,
        (byte) 243,
        (byte) 106,
        (byte) 158,
        (byte) 192 /*0xC0*/,
        (byte) 112 /*0x70*/,
        (byte) 78,
        (byte) 38,
        (byte) 104,
        (byte) 26,
        (byte) 31 /*0x1F*/,
        (byte) 223,
        (byte) 165,
        (byte) 59,
        (byte) 62,
        (byte) 77,
        (byte) 89,
        (byte) 176 /*0xB0*/,
        (byte) 245,
        (byte) 45,
        (byte) 230,
        (byte) 173,
        (byte) 117,
        (byte) 70
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 247,
        (byte) 97,
        (byte) 183,
        (byte) 155,
        (byte) 56,
        (byte) 75,
        (byte) 172,
        (byte) 138,
        (byte) 160 /*0xA0*/,
        (byte) 219,
        (byte) 65,
        (byte) 84,
        (byte) 8,
        (byte) 133,
        (byte) 4,
        (byte) 187,
        (byte) 219,
        (byte) 166,
        (byte) 215,
        (byte) 105,
        (byte) 221,
        (byte) 2,
        (byte) 195,
        (byte) 155,
        (byte) 84,
        (byte) 38,
        (byte) 227,
        (byte) 93,
        (byte) 160 /*0xA0*/,
        (byte) 214,
        (byte) 124,
        (byte) 206,
        (byte) 107,
        (byte) 138,
        (byte) 136,
        (byte) 230,
        (byte) 81,
        (byte) 201,
        (byte) 253,
        (byte) 239,
        (byte) 169,
        (byte) 0,
        (byte) 43,
        (byte) 119,
        (byte) 235,
        (byte) 137,
        (byte) 203,
        (byte) 143,
        (byte) 40,
        (byte) 211,
        (byte) 134,
        (byte) 53,
        (byte) 163,
        (byte) 2,
        (byte) 110
      };
      byte[] numArray5 = new byte[55];
      numArray5[12] = (byte) 49;
      numArray5[1] = (byte) 82;
      numArray5[23] = (byte) 48 /*0x30*/;
      numArray5[22] = (byte) 147;
      numArray5[2] = (byte) 207;
      numArray5[5] = (byte) 169;
      numArray5[41] = (byte) 238;
      numArray5[6] = (byte) 243;
      numArray5[8] = (byte) 235;
      numArray5[28] = (byte) 48 /*0x30*/;
      numArray5[10] = (byte) 53;
      numArray5[47] = (byte) 253;
      numArray5[43] = byte.MaxValue;
      numArray5[42] = (byte) 4;
      numArray5[14] = (byte) 117;
      numArray5[15] = (byte) 61;
      numArray5[16 /*0x10*/] = (byte) 14;
      numArray5[13] = (byte) 19;
      numArray5[48 /*0x30*/] = (byte) 18;
      numArray5[31 /*0x1F*/] = (byte) 95;
      numArray5[0] = (byte) 48 /*0x30*/;
      numArray5[21] = (byte) 20;
      numArray5[24] = (byte) 135;
      numArray5[49] = (byte) 27;
      numArray5[38] = (byte) 57;
      numArray5[25] = (byte) 178;
      numArray5[51] = (byte) 111;
      numArray5[27] = (byte) 79;
      numArray5[17] = (byte) 93;
      numArray5[29] = (byte) 242;
      numArray5[30] = (byte) 40;
      numArray5[18] = (byte) 134;
      numArray5[32 /*0x20*/] = (byte) 64 /*0x40*/;
      numArray5[3] = (byte) 160 /*0xA0*/;
      numArray5[34] = (byte) 192 /*0xC0*/;
      numArray5[35] = (byte) 60;
      numArray5[39] = (byte) 134;
      numArray5[9] = (byte) 222;
      numArray5[19] = (byte) 41;
      numArray5[46] = (byte) 184;
      numArray5[54] = (byte) 79;
      numArray5[50] = (byte) 34;
      numArray5[11] = (byte) 26;
      numArray5[37] = (byte) 159;
      numArray5[44] = (byte) 173;
      numArray5[45] = (byte) 147;
      numArray5[4] = (byte) 235;
      numArray5[33] = (byte) 136;
      numArray5[40] = (byte) 162;
      numArray5[7] = (byte) 234;
      numArray5[26] = (byte) 67;
      numArray5[20] = (byte) 144 /*0x90*/;
      numArray5[52] = (byte) 195;
      numArray5[53] = (byte) 77;
      numArray5[36] = (byte) 223;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 108,
        (byte) 123,
        (byte) 184,
        (byte) 20,
        (byte) 25,
        (byte) 28,
        (byte) 122,
        (byte) 23,
        (byte) 71,
        (byte) 129,
        (byte) 97,
        (byte) 104,
        (byte) 212,
        (byte) 53,
        (byte) 143,
        (byte) 190,
        (byte) 97,
        (byte) 101,
        (byte) 198,
        (byte) 118,
        (byte) 51,
        (byte) 55,
        (byte) 44,
        (byte) 12,
        (byte) 192 /*0xC0*/,
        (byte) 42,
        (byte) 101,
        (byte) 218,
        (byte) 186,
        (byte) 112 /*0x70*/,
        (byte) 37,
        (byte) 139,
        (byte) 119,
        (byte) 247,
        (byte) 200,
        (byte) 6,
        (byte) 50,
        (byte) 115,
        (byte) 64 /*0x40*/,
        (byte) 250,
        (byte) 230,
        (byte) 0,
        (byte) 49,
        (byte) 29,
        (byte) 236,
        (byte) 41,
        (byte) 54,
        (byte) 50,
        (byte) 229,
        (byte) 206,
        (byte) 89,
        (byte) 220,
        (byte) 202,
        (byte) 166,
        (byte) 30
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 52,
        (byte) 142,
        (byte) 103,
        (byte) 21,
        (byte) 183,
        (byte) 219,
        (byte) 85,
        (byte) 142,
        (byte) 167,
        (byte) 13,
        (byte) 149,
        (byte) 30,
        (byte) 137,
        (byte) 12,
        (byte) 8,
        (byte) 239,
        (byte) 186,
        (byte) 100,
        (byte) 57,
        (byte) 199,
        (byte) 144 /*0x90*/,
        (byte) 31 /*0x1F*/,
        (byte) 242,
        (byte) 44,
        (byte) 18,
        (byte) 43,
        (byte) 223,
        (byte) 116,
        (byte) 117,
        (byte) 211,
        (byte) 244,
        (byte) 137,
        (byte) 243,
        (byte) 31 /*0x1F*/,
        (byte) 37,
        (byte) 147,
        (byte) 228,
        (byte) 43,
        (byte) 212,
        (byte) 80 /*0x50*/,
        (byte) 208 /*0xD0*/,
        (byte) 253,
        (byte) 134,
        (byte) 18,
        (byte) 154,
        (byte) 38,
        (byte) 159,
        (byte) 242,
        (byte) 234,
        (byte) 185,
        (byte) 50,
        (byte) 165,
        (byte) 58,
        (byte) 63 /*0x3F*/,
        (byte) 165
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[16 /*0x10*/]
      {
        (byte) 171,
        (byte) 83,
        (byte) 238,
        (byte) 183,
        (byte) 160 /*0xA0*/,
        (byte) 93,
        (byte) 214,
        (byte) 92,
        (byte) 214,
        (byte) 241,
        (byte) 220,
        (byte) 109,
        (byte) 136,
        (byte) 139,
        (byte) 238,
        (byte) 37
      };
      byte[] numArray9 = new byte[16 /*0x10*/]
      {
        (byte) 1,
        (byte) 247,
        (byte) 193,
        (byte) 34,
        (byte) 34,
        (byte) 184,
        (byte) 92,
        (byte) 91,
        (byte) 149,
        (byte) 88,
        (byte) 135,
        (byte) 120,
        (byte) 187,
        (byte) 187,
        (byte) 125,
        (byte) 202
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[181];
    byte[] numArray11 = new byte[55]
    {
      (byte) 5,
      (byte) 15,
      (byte) 31 /*0x1F*/,
      (byte) 150,
      (byte) 244,
      (byte) 50,
      (byte) 245,
      (byte) 235,
      (byte) 23,
      (byte) 72,
      (byte) 192 /*0xC0*/,
      (byte) 208 /*0xD0*/,
      (byte) 217,
      (byte) 146,
      (byte) 184,
      (byte) 134,
      (byte) 31 /*0x1F*/,
      (byte) 82,
      (byte) 67,
      (byte) 69,
      (byte) 163,
      (byte) 91,
      (byte) 192 /*0xC0*/,
      (byte) 22,
      (byte) 56,
      (byte) 89,
      (byte) 196,
      (byte) 92,
      (byte) 67,
      (byte) 107,
      (byte) 183,
      (byte) 254,
      (byte) 22,
      (byte) 235,
      (byte) 48 /*0x30*/,
      (byte) 63 /*0x3F*/,
      (byte) 140,
      (byte) 188,
      (byte) 250,
      (byte) 132,
      (byte) 171,
      (byte) 114,
      (byte) 191,
      (byte) 20,
      (byte) 22,
      (byte) 33,
      (byte) 3,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 4,
      (byte) 206,
      (byte) 210,
      (byte) 137,
      (byte) 74,
      (byte) 143
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 111,
      (byte) 134,
      (byte) 189,
      (byte) 146,
      (byte) 43,
      (byte) 59,
      (byte) 35,
      (byte) 111,
      (byte) 225,
      (byte) 228,
      (byte) 127 /*0x7F*/,
      (byte) 133,
      (byte) 39,
      (byte) 21,
      (byte) 170,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 153,
      (byte) 111,
      (byte) 68,
      (byte) 9,
      (byte) 231,
      (byte) 52,
      (byte) 45,
      (byte) 193,
      (byte) 220,
      (byte) 237,
      (byte) 151,
      (byte) 6,
      (byte) 195,
      (byte) 90,
      (byte) 126,
      (byte) 135,
      (byte) 154,
      (byte) 153,
      (byte) 237,
      (byte) 117,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 78,
      (byte) 23,
      (byte) 209,
      (byte) 30,
      (byte) 235,
      (byte) 186,
      (byte) 14,
      (byte) 105,
      (byte) 80 /*0x50*/,
      (byte) 218,
      (byte) 23,
      (byte) 91,
      (byte) 225,
      (byte) 204,
      (byte) 247,
      (byte) 180
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 8,
      (byte) 94,
      (byte) 154,
      (byte) 109,
      (byte) 152,
      (byte) 168,
      (byte) 171,
      (byte) 144 /*0x90*/,
      (byte) 193,
      (byte) 185,
      (byte) 237,
      (byte) 98,
      (byte) 121,
      (byte) 250,
      (byte) 99,
      (byte) 87,
      (byte) 190,
      (byte) 154,
      (byte) 49,
      (byte) 155,
      (byte) 172,
      (byte) 0,
      (byte) 22,
      (byte) 86,
      (byte) 233,
      (byte) 139,
      (byte) 62,
      (byte) 157,
      (byte) 229,
      (byte) 117,
      (byte) 204,
      (byte) 92,
      (byte) 133,
      (byte) 240 /*0xF0*/,
      (byte) 131,
      (byte) 93,
      (byte) 95,
      (byte) 23,
      (byte) 18,
      (byte) 201,
      (byte) 19,
      (byte) 131,
      (byte) 85,
      (byte) 103,
      (byte) 21,
      (byte) 120,
      (byte) 87,
      (byte) 10,
      (byte) 106,
      (byte) 122,
      (byte) 202,
      (byte) 175,
      (byte) 94,
      (byte) 14,
      (byte) 161
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 164,
      (byte) 155,
      (byte) 169,
      (byte) 188,
      (byte) 194,
      (byte) 128 /*0x80*/,
      (byte) 165,
      (byte) 174,
      (byte) 221,
      (byte) 7,
      (byte) 82,
      (byte) 26,
      (byte) 210,
      (byte) 135,
      (byte) 220,
      (byte) 153,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 176 /*0xB0*/,
      (byte) 130,
      (byte) 170,
      (byte) 172,
      (byte) 44,
      (byte) 238,
      (byte) 251,
      (byte) 59,
      (byte) 98,
      (byte) 169,
      (byte) 132,
      (byte) 242,
      (byte) 241,
      (byte) 169,
      (byte) 61,
      (byte) 195,
      (byte) 22,
      (byte) 54,
      (byte) 182,
      (byte) 94,
      (byte) 145,
      (byte) 73,
      (byte) 218,
      (byte) 71,
      (byte) 155,
      (byte) 17,
      (byte) 31 /*0x1F*/,
      (byte) 104,
      (byte) 119,
      (byte) 195,
      (byte) 94,
      (byte) 14,
      (byte) 79,
      (byte) 251,
      (byte) 204,
      (byte) 252,
      (byte) 66
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 67,
      (byte) 131,
      (byte) 13,
      (byte) 105,
      (byte) 13,
      (byte) 171,
      (byte) 75,
      (byte) 237,
      (byte) 145,
      (byte) 220,
      (byte) 136,
      (byte) 118,
      (byte) 6,
      (byte) 248,
      (byte) 166,
      (byte) 136,
      (byte) 103,
      (byte) 135,
      (byte) 41,
      (byte) 171,
      (byte) 175,
      (byte) 64 /*0x40*/,
      (byte) 113,
      (byte) 52,
      (byte) 52,
      (byte) 51,
      (byte) 106,
      (byte) 170,
      (byte) 28,
      (byte) 253,
      (byte) 119,
      (byte) 78,
      (byte) 123,
      (byte) 21,
      (byte) 28,
      (byte) 147,
      (byte) 5,
      (byte) 96 /*0x60*/,
      (byte) 5,
      (byte) 216,
      (byte) 9,
      (byte) 121,
      (byte) 132,
      (byte) 156,
      (byte) 206,
      (byte) 148,
      (byte) 100,
      (byte) 130,
      (byte) 36,
      (byte) 39,
      (byte) 247,
      (byte) 134,
      (byte) 120,
      (byte) 93,
      (byte) 153
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 141,
      (byte) 12,
      (byte) 196,
      (byte) 188,
      (byte) 223,
      (byte) 148,
      (byte) 58,
      (byte) 190,
      (byte) 8,
      (byte) 54,
      (byte) 155,
      (byte) 122,
      (byte) 191,
      (byte) 130,
      (byte) 211,
      (byte) 170,
      (byte) 229,
      (byte) 46,
      (byte) 250,
      (byte) 27,
      (byte) 217,
      (byte) 48 /*0x30*/,
      (byte) 218,
      (byte) 187,
      (byte) 52,
      (byte) 109,
      (byte) 155,
      (byte) 217,
      (byte) 35,
      (byte) 74,
      (byte) 157,
      (byte) 254,
      (byte) 14,
      (byte) 163,
      (byte) 207,
      (byte) 22,
      (byte) 63 /*0x3F*/,
      (byte) 222,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 86,
      (byte) 131,
      (byte) 168,
      (byte) 20,
      (byte) 43,
      (byte) 79,
      (byte) 114,
      (byte) 34,
      (byte) 19,
      (byte) 38,
      (byte) 39,
      (byte) 27,
      (byte) 34,
      (byte) 128 /*0x80*/,
      (byte) 117
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[16 /*0x10*/]
    {
      (byte) 216,
      (byte) 53,
      (byte) 218,
      (byte) 65,
      (byte) 160 /*0xA0*/,
      (byte) 209,
      (byte) 148,
      (byte) 101,
      (byte) 58,
      (byte) 251,
      (byte) 241,
      (byte) 219,
      (byte) 18,
      (byte) 130,
      (byte) 6,
      (byte) 130
    };
    byte[] numArray18 = new byte[16 /*0x10*/]
    {
      (byte) 241,
      (byte) 233,
      (byte) 80 /*0x50*/,
      (byte) 26,
      (byte) 229,
      (byte) 209,
      (byte) 75,
      (byte) 11,
      (byte) 169,
      (byte) 27,
      (byte) 117,
      (byte) 122,
      (byte) 59,
      (byte) 55,
      (byte) 63 /*0x3F*/,
      (byte) 54
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13752()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[252];
      byte[] numArray2 = new byte[55]
      {
        (byte) 213,
        (byte) 102,
        (byte) 160 /*0xA0*/,
        (byte) 206,
        (byte) 231,
        (byte) 134,
        (byte) 244,
        (byte) 6,
        (byte) 240 /*0xF0*/,
        (byte) 214,
        (byte) 29,
        (byte) 124,
        (byte) 24,
        (byte) 129,
        (byte) 44,
        (byte) 43,
        (byte) 42,
        (byte) 150,
        (byte) 105,
        (byte) 168,
        (byte) 240 /*0xF0*/,
        (byte) 54,
        (byte) 148,
        (byte) 152,
        (byte) 115,
        (byte) 166,
        (byte) 17,
        (byte) 165,
        (byte) 240 /*0xF0*/,
        (byte) 4,
        (byte) 39,
        (byte) 187,
        (byte) 99,
        (byte) 71,
        (byte) 61,
        (byte) 114,
        (byte) 148,
        (byte) 12,
        (byte) 90,
        (byte) 26,
        (byte) 220,
        (byte) 142,
        (byte) 116,
        (byte) 99,
        (byte) 221,
        (byte) 116,
        (byte) 195,
        (byte) 145,
        (byte) 85,
        (byte) 219,
        (byte) 87,
        (byte) 186,
        (byte) 56,
        (byte) 140,
        (byte) 203
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 90,
        (byte) 113,
        (byte) 65,
        (byte) 74,
        (byte) 228,
        (byte) 17,
        (byte) 196,
        (byte) 115,
        (byte) 77,
        (byte) 219,
        (byte) 13,
        (byte) 227,
        (byte) 89,
        (byte) 243,
        (byte) 229,
        (byte) 243,
        (byte) 65,
        (byte) 132,
        (byte) 53,
        (byte) 122,
        (byte) 181,
        (byte) 53,
        (byte) 6,
        (byte) 214,
        (byte) 114,
        (byte) 81,
        (byte) 217,
        (byte) 138,
        (byte) 200,
        (byte) 254,
        (byte) 98,
        (byte) 194,
        (byte) 248,
        (byte) 52,
        (byte) 176 /*0xB0*/,
        (byte) 196,
        (byte) 78,
        (byte) 128 /*0x80*/,
        (byte) 168,
        (byte) 122,
        (byte) 145,
        (byte) 67,
        (byte) 226,
        (byte) 20,
        (byte) 135,
        (byte) 82,
        (byte) 177,
        (byte) 140,
        (byte) 43,
        (byte) 4,
        (byte) 75,
        (byte) 37,
        (byte) 122,
        (byte) 235,
        (byte) 205
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[25] = (byte) 79;
      numArray4[28] = (byte) 109;
      numArray4[2] = (byte) 64 /*0x40*/;
      numArray4[3] = (byte) 117;
      numArray4[4] = (byte) 79;
      numArray4[37] = (byte) 164;
      numArray4[6] = (byte) 236;
      numArray4[7] = (byte) 174;
      numArray4[0] = (byte) 7;
      numArray4[40] = (byte) 136;
      numArray4[33] = (byte) 159;
      numArray4[23] = (byte) 227;
      numArray4[12] = (byte) 186;
      numArray4[13] = (byte) 150;
      numArray4[41] = (byte) 180;
      numArray4[44] = (byte) 241;
      numArray4[16 /*0x10*/] = (byte) 79;
      numArray4[29] = (byte) 131;
      numArray4[18] = (byte) 98;
      numArray4[19] = (byte) 194;
      numArray4[31 /*0x1F*/] = (byte) 123;
      numArray4[11] = (byte) 67;
      numArray4[42] = (byte) 123;
      numArray4[22] = (byte) 254;
      numArray4[24] = (byte) 127 /*0x7F*/;
      numArray4[30] = (byte) 215;
      numArray4[26] = (byte) 21;
      numArray4[1] = (byte) 91;
      numArray4[47] = (byte) 68;
      numArray4[46] = (byte) 155;
      numArray4[20] = (byte) 48 /*0x30*/;
      numArray4[9] = (byte) 24;
      numArray4[54] = (byte) 161;
      numArray4[17] = (byte) 204;
      numArray4[34] = (byte) 141;
      numArray4[35] = (byte) 176 /*0xB0*/;
      numArray4[36] = (byte) 26;
      numArray4[27] = (byte) 173;
      numArray4[38] = (byte) 141;
      numArray4[39] = (byte) 120;
      numArray4[45] = (byte) 77;
      numArray4[10] = (byte) 209;
      numArray4[21] = (byte) 187;
      numArray4[43] = (byte) 92;
      numArray4[32 /*0x20*/] = (byte) 158;
      numArray4[51] = (byte) 192 /*0xC0*/;
      numArray4[8] = (byte) 98;
      numArray4[15] = (byte) 60;
      numArray4[48 /*0x30*/] = (byte) 56;
      numArray4[49] = (byte) 125;
      numArray4[50] = (byte) 233;
      numArray4[5] = (byte) 21;
      numArray4[52] = (byte) 105;
      numArray4[53] = (byte) 220;
      numArray4[14] = (byte) 30;
      byte[] numArray5 = new byte[55];
      numArray5[6] = (byte) 85;
      numArray5[38] = (byte) 205;
      numArray5[34] = (byte) 98;
      numArray5[3] = (byte) 194;
      numArray5[4] = (byte) 7;
      numArray5[47] = (byte) 174;
      numArray5[49] = (byte) 175;
      numArray5[7] = (byte) 34;
      numArray5[8] = (byte) 135;
      numArray5[42] = (byte) 235;
      numArray5[44] = (byte) 202;
      numArray5[11] = (byte) 254;
      numArray5[12] = (byte) 185;
      numArray5[13] = (byte) 204;
      numArray5[10] = (byte) 60;
      numArray5[1] = (byte) 34;
      numArray5[16 /*0x10*/] = (byte) 79;
      numArray5[9] = (byte) 168;
      numArray5[18] = (byte) 154;
      numArray5[45] = (byte) 35;
      numArray5[20] = (byte) 13;
      numArray5[21] = (byte) 77;
      numArray5[37] = (byte) 112 /*0x70*/;
      numArray5[29] = (byte) 22;
      numArray5[19] = (byte) 148;
      numArray5[15] = (byte) 107;
      numArray5[26] = (byte) 59;
      numArray5[51] = (byte) 122;
      numArray5[28] = (byte) 176 /*0xB0*/;
      numArray5[33] = (byte) 253;
      numArray5[22] = (byte) 75;
      numArray5[46] = (byte) 105;
      numArray5[32 /*0x20*/] = (byte) 34;
      numArray5[17] = (byte) 138;
      numArray5[23] = (byte) 233;
      numArray5[35] = (byte) 109;
      numArray5[36] = (byte) 88;
      numArray5[40] = (byte) 51;
      numArray5[14] = (byte) 29;
      numArray5[39] = (byte) 153;
      numArray5[41] = (byte) 109;
      numArray5[27] = (byte) 134;
      numArray5[31 /*0x1F*/] = (byte) 7;
      numArray5[43] = (byte) 157;
      numArray5[2] = (byte) 95;
      numArray5[24] = (byte) 9;
      numArray5[5] = (byte) 183;
      numArray5[25] = (byte) 169;
      numArray5[48 /*0x30*/] = (byte) 75;
      numArray5[0] = (byte) 233;
      numArray5[50] = (byte) 221;
      numArray5[30] = (byte) 172;
      numArray5[52] = (byte) 134;
      numArray5[53] = (byte) 58;
      numArray5[54] = (byte) 154;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[37] = (byte) 173;
      numArray6[31 /*0x1F*/] = (byte) 185;
      numArray6[47] = (byte) 231;
      numArray6[3] = (byte) 155;
      numArray6[4] = (byte) 29;
      numArray6[52] = (byte) 53;
      numArray6[6] = (byte) 234;
      numArray6[18] = (byte) 232;
      numArray6[8] = (byte) 177;
      numArray6[9] = (byte) 194;
      numArray6[10] = (byte) 69;
      numArray6[11] = (byte) 144 /*0x90*/;
      numArray6[12] = (byte) 133;
      numArray6[13] = (byte) 16 /*0x10*/;
      numArray6[14] = (byte) 16 /*0x10*/;
      numArray6[15] = (byte) 113;
      numArray6[29] = (byte) 132;
      numArray6[50] = (byte) 229;
      numArray6[33] = (byte) 97;
      numArray6[34] = (byte) 65;
      numArray6[28] = (byte) 198;
      numArray6[21] = (byte) 0;
      numArray6[36] = (byte) 40;
      numArray6[23] = (byte) 208 /*0xD0*/;
      numArray6[24] = (byte) 31 /*0x1F*/;
      numArray6[25] = (byte) 254;
      numArray6[26] = (byte) 156;
      numArray6[38] = (byte) 98;
      numArray6[7] = (byte) 148;
      numArray6[46] = (byte) 40;
      numArray6[0] = (byte) 18;
      numArray6[20] = (byte) 149;
      numArray6[30] = (byte) 110;
      numArray6[16 /*0x10*/] = (byte) 40;
      numArray6[32 /*0x20*/] = (byte) 206;
      numArray6[35] = (byte) 175;
      numArray6[19] = (byte) 76;
      numArray6[53] = (byte) 27;
      numArray6[22] = (byte) 194;
      numArray6[48 /*0x30*/] = (byte) 50;
      numArray6[27] = (byte) 23;
      numArray6[41] = (byte) 92;
      numArray6[42] = (byte) 166;
      numArray6[43] = (byte) 3;
      numArray6[44] = (byte) 29;
      numArray6[45] = (byte) 228;
      numArray6[1] = (byte) 59;
      numArray6[17] = (byte) 112 /*0x70*/;
      numArray6[39] = (byte) 97;
      numArray6[49] = (byte) 93;
      numArray6[5] = (byte) 191;
      numArray6[51] = (byte) 65;
      numArray6[2] = (byte) 24;
      numArray6[40] = (byte) 109;
      numArray6[54] = (byte) 160 /*0xA0*/;
      byte[] numArray7 = new byte[55]
      {
        (byte) 21,
        (byte) 111,
        (byte) 219,
        (byte) 171,
        (byte) 147,
        (byte) 248,
        (byte) 207,
        (byte) 37,
        (byte) 124,
        (byte) 168,
        (byte) 48 /*0x30*/,
        (byte) 190,
        (byte) 16 /*0x10*/,
        (byte) 223,
        (byte) 239,
        (byte) 83,
        (byte) 138,
        (byte) 225,
        (byte) 61,
        (byte) 254,
        (byte) 199,
        (byte) 85,
        (byte) 235,
        (byte) 16 /*0x10*/,
        (byte) 223,
        (byte) 106,
        (byte) 43,
        (byte) 117,
        (byte) 86,
        (byte) 226,
        (byte) 202,
        (byte) 87,
        (byte) 198,
        (byte) 230,
        (byte) 122,
        (byte) 106,
        (byte) 84,
        (byte) 129,
        (byte) 36,
        (byte) 86,
        (byte) 73,
        (byte) 195,
        (byte) 241,
        (byte) 153,
        (byte) 141,
        (byte) 178,
        (byte) 234,
        (byte) 180,
        (byte) 5,
        (byte) 155,
        (byte) 161,
        (byte) 5,
        (byte) 60,
        (byte) 72,
        (byte) 176 /*0xB0*/
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 148,
        (byte) 63 /*0x3F*/,
        (byte) 73,
        (byte) 110,
        (byte) 238,
        (byte) 101,
        (byte) 141,
        (byte) 40,
        (byte) 7,
        (byte) 14,
        (byte) 99,
        (byte) 160 /*0xA0*/,
        (byte) 41,
        (byte) 104,
        (byte) 72,
        (byte) 201,
        (byte) 72,
        (byte) 68,
        (byte) 171,
        (byte) 224 /*0xE0*/,
        (byte) 15,
        (byte) 33,
        (byte) 25,
        (byte) 209,
        (byte) 156,
        (byte) 18,
        (byte) 84,
        (byte) 52,
        (byte) 247,
        (byte) 212,
        (byte) 118,
        (byte) 88,
        (byte) 118,
        (byte) 125,
        (byte) 76,
        (byte) 242,
        (byte) 250,
        (byte) 208 /*0xD0*/,
        (byte) 171,
        (byte) 39,
        (byte) 236,
        (byte) 135,
        (byte) 137,
        (byte) 20,
        (byte) 160 /*0xA0*/,
        (byte) 120,
        (byte) 168,
        (byte) 171,
        (byte) 34,
        (byte) 106,
        (byte) 102,
        (byte) 248,
        (byte) 188,
        (byte) 216,
        (byte) 170
      };
      byte[] numArray9 = new byte[55];
      numArray9[16 /*0x10*/] = (byte) 19;
      numArray9[19] = (byte) 146;
      numArray9[2] = (byte) 229;
      numArray9[3] = (byte) 235;
      numArray9[27] = (byte) 176 /*0xB0*/;
      numArray9[5] = (byte) 41;
      numArray9[33] = (byte) 41;
      numArray9[7] = (byte) 45;
      numArray9[8] = (byte) 128 /*0x80*/;
      numArray9[43] = (byte) 101;
      numArray9[22] = (byte) 83;
      numArray9[42] = (byte) 150;
      numArray9[12] = (byte) 34;
      numArray9[13] = (byte) 211;
      numArray9[14] = (byte) 40;
      numArray9[15] = (byte) 98;
      numArray9[25] = (byte) 244;
      numArray9[35] = (byte) 129;
      numArray9[47] = (byte) 37;
      numArray9[10] = (byte) 54;
      numArray9[20] = (byte) 244;
      numArray9[1] = (byte) 164;
      numArray9[37] = (byte) 53;
      numArray9[23] = (byte) 214;
      numArray9[24] = (byte) 104;
      numArray9[11] = (byte) 154;
      numArray9[17] = (byte) 245;
      numArray9[0] = (byte) 157;
      numArray9[52] = (byte) 27;
      numArray9[30] = (byte) 139;
      numArray9[54] = (byte) 12;
      numArray9[31 /*0x1F*/] = (byte) 116;
      numArray9[32 /*0x20*/] = (byte) 110;
      numArray9[28] = (byte) 161;
      numArray9[39] = (byte) 93;
      numArray9[29] = (byte) 37;
      numArray9[36] = (byte) 29;
      numArray9[38] = (byte) 246;
      numArray9[18] = (byte) 164;
      numArray9[6] = (byte) 172;
      numArray9[9] = (byte) 48 /*0x30*/;
      numArray9[41] = (byte) 27;
      numArray9[34] = (byte) 254;
      numArray9[50] = (byte) 205;
      numArray9[44] = (byte) 71;
      numArray9[45] = (byte) 7;
      numArray9[46] = (byte) 228;
      numArray9[4] = (byte) 107;
      numArray9[48 /*0x30*/] = (byte) 164;
      numArray9[49] = (byte) 148;
      numArray9[21] = (byte) 127 /*0x7F*/;
      numArray9[51] = (byte) 246;
      numArray9[26] = (byte) 225;
      numArray9[53] = (byte) 153;
      numArray9[40] = (byte) 161;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[32 /*0x20*/]
      {
        (byte) 72,
        (byte) 173,
        (byte) 114,
        (byte) 103,
        (byte) 71,
        (byte) 164,
        (byte) 196,
        (byte) 24,
        (byte) 110,
        (byte) 140,
        (byte) 216,
        (byte) 66,
        (byte) 137,
        (byte) 77,
        (byte) 121,
        (byte) 201,
        (byte) 109,
        (byte) 96 /*0x60*/,
        (byte) 48 /*0x30*/,
        (byte) 65,
        (byte) 135,
        (byte) 153,
        (byte) 214,
        (byte) 109,
        (byte) 27,
        (byte) 68,
        (byte) 241,
        (byte) 127 /*0x7F*/,
        (byte) 31 /*0x1F*/,
        (byte) 100,
        (byte) 185,
        (byte) 176 /*0xB0*/
      };
      byte[] numArray11 = new byte[32 /*0x20*/];
      numArray11[31 /*0x1F*/] = (byte) 44;
      numArray11[12] = (byte) 77;
      numArray11[2] = (byte) 60;
      numArray11[3] = (byte) 56;
      numArray11[4] = (byte) 198;
      numArray11[5] = (byte) 36;
      numArray11[26] = (byte) 58;
      numArray11[7] = (byte) 197;
      numArray11[8] = (byte) 145;
      numArray11[28] = (byte) 116;
      numArray11[20] = (byte) 6;
      numArray11[11] = (byte) 33;
      numArray11[18] = (byte) 221;
      numArray11[13] = (byte) 137;
      numArray11[24] = (byte) 72;
      numArray11[15] = (byte) 159;
      numArray11[16 /*0x10*/] = (byte) 169;
      numArray11[21] = (byte) 72;
      numArray11[17] = (byte) 35;
      numArray11[23] = (byte) 20;
      numArray11[6] = (byte) 196;
      numArray11[9] = (byte) 215;
      numArray11[22] = (byte) 66;
      numArray11[14] = (byte) 127 /*0x7F*/;
      numArray11[10] = (byte) 103;
      numArray11[25] = (byte) 7;
      numArray11[19] = (byte) 63 /*0x3F*/;
      numArray11[27] = (byte) 95;
      numArray11[1] = (byte) 1;
      numArray11[29] = (byte) 47;
      numArray11[30] = (byte) 243;
      numArray11[0] = (byte) 231;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[252];
    byte[] numArray13 = new byte[55];
    numArray13[27] = byte.MaxValue;
    numArray13[20] = (byte) 102;
    numArray13[15] = (byte) 9;
    numArray13[33] = (byte) 8;
    numArray13[4] = (byte) 190;
    numArray13[54] = (byte) 220;
    numArray13[14] = (byte) 163;
    numArray13[52] = (byte) 157;
    numArray13[12] = (byte) 207;
    numArray13[9] = (byte) 113;
    numArray13[10] = (byte) 20;
    numArray13[11] = (byte) 72;
    numArray13[5] = (byte) 21;
    numArray13[13] = (byte) 1;
    numArray13[29] = (byte) 107;
    numArray13[21] = (byte) 14;
    numArray13[17] = (byte) 153;
    numArray13[18] = (byte) 53;
    numArray13[7] = (byte) 175;
    numArray13[16 /*0x10*/] = (byte) 137;
    numArray13[42] = (byte) 121;
    numArray13[31 /*0x1F*/] = (byte) 238;
    numArray13[6] = (byte) 126;
    numArray13[23] = (byte) 122;
    numArray13[28] = (byte) 76;
    numArray13[32 /*0x20*/] = (byte) 252;
    numArray13[2] = (byte) 39;
    numArray13[44] = (byte) 194;
    numArray13[19] = (byte) 190;
    numArray13[1] = (byte) 20;
    numArray13[30] = (byte) 30;
    numArray13[22] = (byte) 138;
    numArray13[26] = (byte) 72;
    numArray13[35] = (byte) 56;
    numArray13[25] = (byte) 98;
    numArray13[40] = (byte) 115;
    numArray13[36] = (byte) 78;
    numArray13[37] = (byte) 40;
    numArray13[38] = (byte) 12;
    numArray13[53] = (byte) 105;
    numArray13[49] = (byte) 59;
    numArray13[41] = (byte) 233;
    numArray13[0] = (byte) 224 /*0xE0*/;
    numArray13[43] = (byte) 61;
    numArray13[34] = (byte) 99;
    numArray13[45] = (byte) 188;
    numArray13[39] = (byte) 110;
    numArray13[47] = (byte) 1;
    numArray13[48 /*0x30*/] = (byte) 15;
    numArray13[50] = (byte) 111;
    numArray13[3] = (byte) 108;
    numArray13[51] = (byte) 112 /*0x70*/;
    numArray13[46] = (byte) 124;
    numArray13[8] = (byte) 34;
    numArray13[24] = (byte) 87;
    byte[] numArray14 = new byte[55];
    numArray14[11] = (byte) 238;
    numArray14[29] = (byte) 208 /*0xD0*/;
    numArray14[25] = (byte) 117;
    numArray14[2] = (byte) 95;
    numArray14[6] = (byte) 3;
    numArray14[46] = (byte) 43;
    numArray14[10] = (byte) 91;
    numArray14[1] = (byte) 241;
    numArray14[8] = (byte) 74;
    numArray14[9] = (byte) 68;
    numArray14[18] = (byte) 87;
    numArray14[36] = (byte) 200;
    numArray14[13] = (byte) 172;
    numArray14[20] = (byte) 85;
    numArray14[33] = (byte) 204;
    numArray14[52] = (byte) 201;
    numArray14[16 /*0x10*/] = (byte) 78;
    numArray14[17] = (byte) 245;
    numArray14[39] = (byte) 6;
    numArray14[19] = (byte) 43;
    numArray14[30] = (byte) 98;
    numArray14[21] = (byte) 15;
    numArray14[22] = (byte) 146;
    numArray14[23] = (byte) 50;
    numArray14[53] = (byte) 198;
    numArray14[12] = (byte) 159;
    numArray14[26] = (byte) 214;
    numArray14[41] = (byte) 149;
    numArray14[28] = (byte) 41;
    numArray14[14] = (byte) 228;
    numArray14[40] = (byte) 210;
    numArray14[31 /*0x1F*/] = (byte) 159;
    numArray14[32 /*0x20*/] = (byte) 147;
    numArray14[45] = (byte) 144 /*0x90*/;
    numArray14[34] = (byte) 154;
    numArray14[35] = (byte) 219;
    numArray14[15] = (byte) 122;
    numArray14[38] = (byte) 79;
    numArray14[0] = (byte) 62;
    numArray14[3] = (byte) 6;
    numArray14[7] = (byte) 165;
    numArray14[37] = (byte) 14;
    numArray14[42] = (byte) 250;
    numArray14[43] = (byte) 152;
    numArray14[4] = (byte) 138;
    numArray14[24] = (byte) 201;
    numArray14[5] = (byte) 52;
    numArray14[47] = (byte) 62;
    numArray14[48 /*0x30*/] = (byte) 151;
    numArray14[49] = (byte) 147;
    numArray14[50] = (byte) 81;
    numArray14[51] = (byte) 254;
    numArray14[27] = (byte) 3;
    numArray14[44] = (byte) 187;
    numArray14[54] = (byte) 105;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[1] = (byte) 80 /*0x50*/;
    numArray15[47] = (byte) 253;
    numArray15[14] = (byte) 180;
    numArray15[3] = (byte) 162;
    numArray15[35] = (byte) 72;
    numArray15[52] = (byte) 165;
    numArray15[41] = (byte) 107;
    numArray15[30] = (byte) 171;
    numArray15[8] = (byte) 93;
    numArray15[50] = (byte) 169;
    numArray15[10] = (byte) 207;
    numArray15[15] = (byte) 162;
    numArray15[9] = (byte) 67;
    numArray15[44] = (byte) 73;
    numArray15[7] = (byte) 217;
    numArray15[37] = (byte) 121;
    numArray15[16 /*0x10*/] = (byte) 8;
    numArray15[23] = (byte) 22;
    numArray15[18] = (byte) 195;
    numArray15[19] = (byte) 204;
    numArray15[20] = (byte) 30;
    numArray15[11] = (byte) 183;
    numArray15[31 /*0x1F*/] = (byte) 99;
    numArray15[12] = (byte) 74;
    numArray15[2] = (byte) 69;
    numArray15[51] = (byte) 218;
    numArray15[4] = (byte) 65;
    numArray15[27] = (byte) 42;
    numArray15[25] = (byte) 101;
    numArray15[24] = (byte) 140;
    numArray15[36] = (byte) 244;
    numArray15[29] = (byte) 198;
    numArray15[32 /*0x20*/] = (byte) 236;
    numArray15[21] = (byte) 87;
    numArray15[0] = (byte) 214;
    numArray15[38] = (byte) 37;
    numArray15[17] = (byte) 238;
    numArray15[22] = (byte) 112 /*0x70*/;
    numArray15[48 /*0x30*/] = (byte) 13;
    numArray15[39] = (byte) 114;
    numArray15[40] = (byte) 55;
    numArray15[26] = (byte) 40;
    numArray15[13] = (byte) 105;
    numArray15[43] = (byte) 117;
    numArray15[34] = (byte) 216;
    numArray15[45] = (byte) 158;
    numArray15[46] = (byte) 156;
    numArray15[6] = (byte) 207;
    numArray15[28] = (byte) 196;
    numArray15[5] = (byte) 193;
    numArray15[42] = (byte) 104;
    numArray15[49] = (byte) 100;
    numArray15[33] = (byte) 152;
    numArray15[53] = (byte) 156;
    numArray15[54] = (byte) 186;
    byte[] numArray16 = new byte[55]
    {
      (byte) 212,
      (byte) 58,
      (byte) 82,
      (byte) 6,
      (byte) 217,
      (byte) 189,
      (byte) 148,
      (byte) 197,
      (byte) 51,
      (byte) 171,
      (byte) 96 /*0x60*/,
      (byte) 76,
      (byte) 192 /*0xC0*/,
      (byte) 133,
      (byte) 105,
      (byte) 238,
      (byte) 133,
      (byte) 230,
      (byte) 228,
      (byte) 230,
      (byte) 190,
      (byte) 230,
      (byte) 235,
      (byte) 239,
      (byte) 66,
      (byte) 66,
      (byte) 230,
      (byte) 94,
      (byte) 122,
      (byte) 179,
      (byte) 52,
      (byte) 244,
      (byte) 187,
      (byte) 67,
      (byte) 129,
      (byte) 157,
      (byte) 10,
      (byte) 96 /*0x60*/,
      (byte) 159,
      (byte) 42,
      (byte) 19,
      (byte) 58,
      (byte) 231,
      (byte) 205,
      (byte) 153,
      (byte) 212,
      (byte) 226,
      (byte) 20,
      (byte) 118,
      (byte) 200,
      (byte) 66,
      (byte) 104,
      (byte) 184,
      (byte) 128 /*0x80*/,
      (byte) 199
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 98,
      (byte) 39,
      (byte) 19,
      (byte) 110,
      (byte) 135,
      (byte) 181,
      (byte) 121,
      (byte) 199,
      (byte) 173,
      (byte) 124,
      (byte) 212,
      (byte) 55,
      (byte) 1,
      (byte) 233,
      (byte) 42,
      (byte) 127 /*0x7F*/,
      (byte) 120,
      (byte) 1,
      (byte) 93,
      (byte) 28,
      (byte) 29,
      (byte) 238,
      (byte) 26,
      (byte) 222,
      (byte) 190,
      (byte) 122,
      (byte) 169,
      (byte) 1,
      (byte) 190,
      (byte) 106,
      (byte) 71,
      (byte) 214,
      (byte) 202,
      (byte) 132,
      (byte) 98,
      (byte) 61,
      (byte) 53,
      (byte) 105,
      (byte) 12,
      (byte) 160 /*0xA0*/,
      (byte) 88,
      (byte) 22,
      (byte) 49,
      (byte) 86,
      (byte) 21,
      (byte) 53,
      (byte) 160 /*0xA0*/,
      (byte) 204,
      (byte) 241,
      (byte) 147,
      (byte) 219,
      (byte) 6,
      (byte) 170,
      (byte) 114,
      (byte) 199
    };
    byte[] numArray18 = new byte[55]
    {
      (byte) 2,
      (byte) 47,
      (byte) 88,
      (byte) 188,
      (byte) 168,
      (byte) 108,
      (byte) 83,
      byte.MaxValue,
      (byte) 172,
      (byte) 179,
      (byte) 64 /*0x40*/,
      (byte) 143,
      (byte) 110,
      (byte) 37,
      (byte) 60,
      (byte) 57,
      (byte) 25,
      (byte) 61,
      (byte) 192 /*0xC0*/,
      (byte) 43,
      (byte) 42,
      (byte) 45,
      (byte) 143,
      (byte) 244,
      (byte) 21,
      (byte) 165,
      (byte) 209,
      (byte) 166,
      (byte) 67,
      (byte) 109,
      (byte) 130,
      (byte) 80 /*0x50*/,
      (byte) 107,
      (byte) 224 /*0xE0*/,
      (byte) 71,
      (byte) 73,
      (byte) 29,
      (byte) 159,
      (byte) 209,
      (byte) 19,
      (byte) 111,
      (byte) 117,
      (byte) 23,
      (byte) 186,
      (byte) 148,
      (byte) 9,
      (byte) 165,
      (byte) 133,
      (byte) 253,
      (byte) 248,
      (byte) 185,
      (byte) 225,
      (byte) 144 /*0x90*/,
      (byte) 247,
      (byte) 171
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 53,
      (byte) 176 /*0xB0*/,
      (byte) 12,
      (byte) 11,
      (byte) 120,
      (byte) 8,
      (byte) 66,
      (byte) 52,
      (byte) 254,
      (byte) 109,
      (byte) 125,
      (byte) 134,
      (byte) 237,
      (byte) 73,
      (byte) 14,
      (byte) 245,
      (byte) 188,
      (byte) 196,
      (byte) 6,
      (byte) 189,
      (byte) 195,
      (byte) 200,
      (byte) 136,
      (byte) 218,
      (byte) 220,
      (byte) 16 /*0x10*/,
      (byte) 43,
      (byte) 142,
      (byte) 65,
      (byte) 43,
      (byte) 87,
      (byte) 189,
      (byte) 239,
      (byte) 48 /*0x30*/,
      (byte) 0,
      (byte) 185,
      byte.MaxValue,
      (byte) 148,
      (byte) 223,
      (byte) 13,
      (byte) 238,
      (byte) 34,
      (byte) 23,
      (byte) 200,
      (byte) 35,
      (byte) 70,
      (byte) 3,
      (byte) 73,
      (byte) 205,
      (byte) 16 /*0x10*/,
      (byte) 237,
      (byte) 174,
      (byte) 192 /*0xC0*/,
      (byte) 181,
      (byte) 72
    };
    byte[] numArray20 = new byte[55];
    numArray20[10] = (byte) 238;
    numArray20[44] = (byte) 125;
    numArray20[2] = (byte) 53;
    numArray20[42] = (byte) 143;
    numArray20[31 /*0x1F*/] = (byte) 151;
    numArray20[5] = (byte) 37;
    numArray20[6] = (byte) 41;
    numArray20[7] = (byte) 75;
    numArray20[43] = (byte) 213;
    numArray20[9] = (byte) 11;
    numArray20[18] = (byte) 41;
    numArray20[11] = (byte) 135;
    numArray20[52] = (byte) 150;
    numArray20[50] = (byte) 28;
    numArray20[14] = (byte) 233;
    numArray20[1] = (byte) 125;
    numArray20[16 /*0x10*/] = (byte) 212;
    numArray20[39] = (byte) 243;
    numArray20[37] = (byte) 136;
    numArray20[0] = (byte) 27;
    numArray20[20] = (byte) 78;
    numArray20[29] = (byte) 89;
    numArray20[36] = (byte) 232;
    numArray20[23] = (byte) 36;
    numArray20[12] = (byte) 7;
    numArray20[25] = (byte) 4;
    numArray20[3] = (byte) 38;
    numArray20[27] = (byte) 65;
    numArray20[28] = (byte) 25;
    numArray20[46] = (byte) 82;
    numArray20[30] = (byte) 250;
    numArray20[35] = (byte) 104;
    numArray20[8] = (byte) 55;
    numArray20[33] = (byte) 111;
    numArray20[34] = (byte) 139;
    numArray20[17] = (byte) 116;
    numArray20[26] = (byte) 157;
    numArray20[38] = (byte) 21;
    numArray20[24] = (byte) 188;
    numArray20[15] = (byte) 246;
    numArray20[40] = (byte) 26;
    numArray20[41] = (byte) 202;
    numArray20[32 /*0x20*/] = (byte) 46;
    numArray20[21] = (byte) 41;
    numArray20[4] = (byte) 209;
    numArray20[45] = (byte) 112 /*0x70*/;
    numArray20[19] = (byte) 36;
    numArray20[47] = (byte) 45;
    numArray20[48 /*0x30*/] = (byte) 61;
    numArray20[49] = (byte) 147;
    numArray20[13] = (byte) 202;
    numArray20[51] = (byte) 203;
    numArray20[22] = (byte) 69;
    numArray20[53] = (byte) 215;
    numArray20[54] = (byte) 15;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[32 /*0x20*/];
    numArray21[16 /*0x10*/] = (byte) 200;
    numArray21[12] = (byte) 195;
    numArray21[23] = (byte) 197;
    numArray21[8] = (byte) 194;
    numArray21[2] = (byte) 91;
    numArray21[5] = (byte) 194;
    numArray21[6] = (byte) 148;
    numArray21[7] = (byte) 108;
    numArray21[11] = (byte) 182;
    numArray21[31 /*0x1F*/] = (byte) 65;
    numArray21[10] = (byte) 212;
    numArray21[21] = (byte) 82;
    numArray21[18] = (byte) 113;
    numArray21[4] = (byte) 243;
    numArray21[14] = (byte) 194;
    numArray21[15] = (byte) 109;
    numArray21[30] = (byte) 82;
    numArray21[17] = (byte) 149;
    numArray21[3] = (byte) 251;
    numArray21[19] = (byte) 107;
    numArray21[0] = (byte) 123;
    numArray21[20] = (byte) 14;
    numArray21[22] = (byte) 230;
    numArray21[28] = (byte) 224 /*0xE0*/;
    numArray21[24] = (byte) 225;
    numArray21[9] = (byte) 93;
    numArray21[26] = (byte) 21;
    numArray21[27] = (byte) 121;
    numArray21[13] = (byte) 39;
    numArray21[29] = (byte) 53;
    numArray21[25] = (byte) 72;
    numArray21[1] = (byte) 152;
    byte[] numArray22 = new byte[32 /*0x20*/]
    {
      (byte) 186,
      (byte) 65,
      (byte) 232,
      (byte) 57,
      (byte) 230,
      (byte) 232,
      (byte) 29,
      (byte) 194,
      (byte) 163,
      (byte) 244,
      (byte) 170,
      (byte) 138,
      (byte) 121,
      (byte) 91,
      (byte) 216,
      (byte) 78,
      (byte) 43,
      (byte) 118,
      (byte) 16 /*0x10*/,
      (byte) 101,
      (byte) 162,
      (byte) 49,
      (byte) 4,
      (byte) 231,
      (byte) 98,
      (byte) 208 /*0xD0*/,
      (byte) 105,
      (byte) 62,
      (byte) 109,
      (byte) 46,
      (byte) 77,
      (byte) 156
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_13753()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[303];
      byte[] numArray2 = new byte[55];
      numArray2[18] = (byte) 11;
      numArray2[6] = (byte) 187;
      numArray2[10] = (byte) 81;
      numArray2[3] = (byte) 147;
      numArray2[17] = (byte) 1;
      numArray2[5] = (byte) 3;
      numArray2[14] = (byte) 118;
      numArray2[8] = (byte) 177;
      numArray2[23] = (byte) 132;
      numArray2[27] = (byte) 149;
      numArray2[39] = (byte) 49;
      numArray2[1] = (byte) 245;
      numArray2[2] = (byte) 82;
      numArray2[29] = (byte) 20;
      numArray2[21] = (byte) 40;
      numArray2[43] = (byte) 29;
      numArray2[45] = (byte) 131;
      numArray2[46] = (byte) 22;
      numArray2[7] = (byte) 200;
      numArray2[0] = (byte) 214;
      numArray2[22] = (byte) 26;
      numArray2[12] = (byte) 148;
      numArray2[16 /*0x10*/] = (byte) 53;
      numArray2[28] = (byte) 185;
      numArray2[15] = (byte) 133;
      numArray2[42] = (byte) 119;
      numArray2[26] = (byte) 192 /*0xC0*/;
      numArray2[24] = (byte) 26;
      numArray2[25] = (byte) 155;
      numArray2[48 /*0x30*/] = (byte) 164;
      numArray2[30] = (byte) 70;
      numArray2[31 /*0x1F*/] = (byte) 223;
      numArray2[32 /*0x20*/] = (byte) 105;
      numArray2[33] = (byte) 27;
      numArray2[41] = (byte) 143;
      numArray2[35] = (byte) 12;
      numArray2[4] = (byte) 79;
      numArray2[37] = (byte) 108;
      numArray2[38] = (byte) 0;
      numArray2[53] = (byte) 91;
      numArray2[40] = (byte) 149;
      numArray2[9] = (byte) 148;
      numArray2[11] = (byte) 150;
      numArray2[20] = (byte) 210;
      numArray2[44] = (byte) 68;
      numArray2[51] = (byte) 9;
      numArray2[49] = (byte) 6;
      numArray2[47] = (byte) 139;
      numArray2[34] = (byte) 29;
      numArray2[36] = (byte) 8;
      numArray2[50] = (byte) 87;
      numArray2[19] = (byte) 133;
      numArray2[52] = (byte) 135;
      numArray2[13] = (byte) 33;
      numArray2[54] = (byte) 198;
      byte[] numArray3 = new byte[55];
      numArray3[41] = (byte) 230;
      numArray3[25] = (byte) 54;
      numArray3[12] = (byte) 26;
      numArray3[9] = (byte) 213;
      numArray3[50] = (byte) 28;
      numArray3[5] = (byte) 149;
      numArray3[46] = (byte) 94;
      numArray3[36] = (byte) 200;
      numArray3[8] = (byte) 56;
      numArray3[16 /*0x10*/] = (byte) 90;
      numArray3[28] = (byte) 55;
      numArray3[26] = (byte) 51;
      numArray3[4] = (byte) 163;
      numArray3[10] = (byte) 23;
      numArray3[14] = (byte) 44;
      numArray3[2] = (byte) 249;
      numArray3[27] = (byte) 244;
      numArray3[52] = (byte) 120;
      numArray3[18] = (byte) 14;
      numArray3[19] = (byte) 125;
      numArray3[20] = (byte) 5;
      numArray3[13] = (byte) 171;
      numArray3[47] = (byte) 84;
      numArray3[49] = (byte) 144 /*0x90*/;
      numArray3[24] = (byte) 97;
      numArray3[11] = (byte) 93;
      numArray3[7] = (byte) 130;
      numArray3[3] = (byte) 136;
      numArray3[15] = (byte) 224 /*0xE0*/;
      numArray3[29] = (byte) 233;
      numArray3[6] = (byte) 100;
      numArray3[0] = (byte) 77;
      numArray3[32 /*0x20*/] = (byte) 121;
      numArray3[37] = (byte) 246;
      numArray3[43] = (byte) 32 /*0x20*/;
      numArray3[35] = (byte) 155;
      numArray3[34] = (byte) 69;
      numArray3[31 /*0x1F*/] = (byte) 86;
      numArray3[38] = (byte) 4;
      numArray3[33] = (byte) 56;
      numArray3[40] = (byte) 166;
      numArray3[17] = (byte) 100;
      numArray3[42] = (byte) 119;
      numArray3[39] = (byte) 123;
      numArray3[44] = (byte) 14;
      numArray3[45] = (byte) 16 /*0x10*/;
      numArray3[30] = (byte) 225;
      numArray3[21] = (byte) 89;
      numArray3[48 /*0x30*/] = (byte) 221;
      numArray3[23] = (byte) 162;
      numArray3[1] = (byte) 80 /*0x50*/;
      numArray3[51] = (byte) 2;
      numArray3[22] = (byte) 141;
      numArray3[53] = (byte) 233;
      numArray3[54] = (byte) 103;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 32 /*0x20*/,
        (byte) 160 /*0xA0*/,
        (byte) 75,
        (byte) 96 /*0x60*/,
        (byte) 124,
        (byte) 52,
        (byte) 178,
        (byte) 219,
        (byte) 112 /*0x70*/,
        (byte) 124,
        (byte) 82,
        (byte) 33,
        (byte) 227,
        (byte) 148,
        (byte) 194,
        (byte) 153,
        (byte) 11,
        (byte) 66,
        (byte) 160 /*0xA0*/,
        (byte) 222,
        (byte) 113,
        (byte) 181,
        (byte) 9,
        (byte) 105,
        (byte) 186,
        (byte) 181,
        (byte) 224 /*0xE0*/,
        (byte) 116,
        (byte) 225,
        (byte) 230,
        (byte) 180,
        (byte) 14,
        (byte) 78,
        (byte) 167,
        (byte) 141,
        (byte) 114,
        (byte) 208 /*0xD0*/,
        (byte) 127 /*0x7F*/,
        (byte) 192 /*0xC0*/,
        (byte) 237,
        (byte) 70,
        (byte) 226,
        (byte) 71,
        (byte) 186,
        (byte) 41,
        (byte) 209,
        (byte) 30,
        (byte) 178,
        (byte) 247,
        (byte) 186,
        (byte) 183,
        (byte) 170,
        (byte) 33,
        (byte) 70,
        (byte) 60
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 13,
        (byte) 1,
        (byte) 15,
        (byte) 41,
        (byte) 101,
        (byte) 161,
        (byte) 205,
        (byte) 54,
        (byte) 214,
        (byte) 179,
        (byte) 179,
        (byte) 138,
        (byte) 61,
        (byte) 124,
        (byte) 155,
        (byte) 102,
        (byte) 47,
        (byte) 105,
        (byte) 229,
        (byte) 116,
        (byte) 168,
        (byte) 218,
        (byte) 171,
        (byte) 2,
        (byte) 175,
        (byte) 111,
        (byte) 128 /*0x80*/,
        (byte) 189,
        (byte) 175,
        (byte) 164,
        (byte) 21,
        (byte) 119,
        (byte) 123,
        (byte) 174,
        (byte) 36,
        (byte) 201,
        (byte) 123,
        (byte) 25,
        (byte) 170,
        (byte) 194,
        (byte) 140,
        (byte) 36,
        (byte) 131,
        (byte) 8,
        (byte) 182,
        (byte) 187,
        (byte) 220,
        (byte) 162,
        (byte) 171,
        (byte) 152,
        (byte) 34,
        (byte) 254,
        (byte) 76,
        (byte) 187,
        (byte) 225
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 225,
        (byte) 165,
        (byte) 198,
        (byte) 84,
        (byte) 61,
        (byte) 249,
        (byte) 137,
        (byte) 199,
        (byte) 67,
        (byte) 210,
        (byte) 59,
        (byte) 7,
        (byte) 79,
        (byte) 200,
        (byte) 139,
        (byte) 130,
        (byte) 178,
        (byte) 151,
        (byte) 142,
        (byte) 110,
        (byte) 79,
        (byte) 64 /*0x40*/,
        (byte) 233,
        (byte) 151,
        (byte) 46,
        (byte) 125,
        (byte) 39,
        (byte) 47,
        (byte) 9,
        (byte) 236,
        (byte) 7,
        (byte) 106,
        (byte) 34,
        (byte) 220,
        (byte) 238,
        (byte) 217,
        (byte) 249,
        (byte) 153,
        (byte) 8,
        (byte) 232,
        (byte) 189,
        (byte) 242,
        (byte) 229,
        (byte) 254,
        (byte) 226,
        (byte) 171,
        (byte) 168,
        (byte) 241,
        (byte) 44,
        (byte) 40,
        (byte) 111,
        (byte) 174,
        (byte) 173,
        (byte) 252,
        (byte) 70
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 62,
        (byte) 94,
        (byte) 174,
        (byte) 4,
        (byte) 130,
        (byte) 179,
        (byte) 162,
        (byte) 121,
        (byte) 237,
        (byte) 223,
        (byte) 173,
        (byte) 87,
        (byte) 110,
        (byte) 102,
        (byte) 36,
        (byte) 118,
        (byte) 171,
        (byte) 80 /*0x50*/,
        (byte) 89,
        (byte) 177,
        (byte) 122,
        (byte) 100,
        (byte) 7,
        (byte) 84,
        (byte) 141,
        (byte) 38,
        (byte) 205,
        (byte) 116,
        (byte) 186,
        (byte) 108,
        (byte) 66,
        (byte) 231,
        (byte) 110,
        (byte) 64 /*0x40*/,
        (byte) 247,
        (byte) 157,
        (byte) 64 /*0x40*/,
        (byte) 142,
        (byte) 127 /*0x7F*/,
        (byte) 108,
        (byte) 10,
        (byte) 136,
        (byte) 198,
        (byte) 140,
        (byte) 136,
        (byte) 237,
        (byte) 132,
        (byte) 228,
        (byte) 216,
        (byte) 178,
        (byte) 145,
        (byte) 59,
        (byte) 172,
        (byte) 211,
        (byte) 63 /*0x3F*/
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 46,
        (byte) 38,
        (byte) 191,
        (byte) 210,
        (byte) 90,
        (byte) 190,
        (byte) 86,
        (byte) 203,
        (byte) 209,
        (byte) 228,
        (byte) 114,
        (byte) 218,
        (byte) 177,
        (byte) 136,
        (byte) 25,
        (byte) 143,
        (byte) 166,
        (byte) 176 /*0xB0*/,
        (byte) 93,
        (byte) 90,
        (byte) 234,
        (byte) 181,
        (byte) 130,
        (byte) 213,
        (byte) 54,
        (byte) 2,
        (byte) 225,
        (byte) 125,
        (byte) 88,
        (byte) 189,
        (byte) 38,
        (byte) 149,
        (byte) 77,
        (byte) 29,
        (byte) 203,
        (byte) 115,
        (byte) 72,
        (byte) 22,
        (byte) 98,
        (byte) 52,
        (byte) 143,
        (byte) 51,
        (byte) 17,
        (byte) 198,
        (byte) 53,
        (byte) 22,
        (byte) 122,
        (byte) 160 /*0xA0*/,
        (byte) 85,
        (byte) 31 /*0x1F*/,
        (byte) 7,
        (byte) 75,
        (byte) 233,
        (byte) 70,
        (byte) 209
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 163,
        (byte) 39,
        (byte) 68,
        (byte) 12,
        (byte) 199,
        (byte) 24,
        (byte) 91,
        (byte) 52,
        (byte) 53,
        (byte) 224 /*0xE0*/,
        (byte) 245,
        (byte) 194,
        (byte) 239,
        (byte) 1,
        (byte) 72,
        (byte) 87,
        (byte) 129,
        (byte) 203,
        (byte) 54,
        (byte) 3,
        (byte) 252,
        (byte) 179,
        (byte) 213,
        (byte) 130,
        (byte) 136,
        (byte) 104,
        (byte) 223,
        (byte) 123,
        (byte) 147,
        (byte) 21,
        (byte) 243,
        (byte) 162,
        (byte) 227,
        (byte) 89,
        (byte) 233,
        (byte) 165,
        (byte) 51,
        (byte) 244,
        (byte) 125,
        (byte) 36,
        (byte) 150,
        (byte) 36,
        (byte) 114,
        (byte) 142,
        (byte) 153,
        (byte) 141,
        (byte) 69,
        (byte) 88,
        (byte) 10,
        (byte) 71,
        (byte) 237,
        (byte) 240 /*0xF0*/,
        (byte) 47,
        (byte) 112 /*0x70*/,
        (byte) 227
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55];
      numArray10[22] = (byte) 31 /*0x1F*/;
      numArray10[53] = (byte) 213;
      numArray10[54] = (byte) 230;
      numArray10[28] = (byte) 218;
      numArray10[4] = (byte) 216;
      numArray10[5] = (byte) 114;
      numArray10[39] = (byte) 192 /*0xC0*/;
      numArray10[0] = (byte) 234;
      numArray10[1] = (byte) 155;
      numArray10[9] = (byte) 140;
      numArray10[49] = (byte) 253;
      numArray10[46] = (byte) 175;
      numArray10[12] = (byte) 166;
      numArray10[32 /*0x20*/] = (byte) 81;
      numArray10[27] = (byte) 182;
      numArray10[33] = (byte) 207;
      numArray10[16 /*0x10*/] = (byte) 40;
      numArray10[17] = (byte) 203;
      numArray10[7] = (byte) 190;
      numArray10[19] = (byte) 164;
      numArray10[20] = (byte) 188;
      numArray10[50] = (byte) 193;
      numArray10[43] = (byte) 138;
      numArray10[40] = (byte) 252;
      numArray10[24] = (byte) 64 /*0x40*/;
      numArray10[13] = (byte) 70;
      numArray10[45] = (byte) 227;
      numArray10[15] = (byte) 78;
      numArray10[47] = (byte) 216;
      numArray10[26] = (byte) 57;
      numArray10[8] = (byte) 220;
      numArray10[14] = (byte) 144 /*0x90*/;
      numArray10[36] = (byte) 10;
      numArray10[18] = (byte) 183;
      numArray10[34] = (byte) 94;
      numArray10[35] = (byte) 122;
      numArray10[23] = (byte) 135;
      numArray10[37] = (byte) 87;
      numArray10[25] = (byte) 40;
      numArray10[31 /*0x1F*/] = (byte) 76;
      numArray10[30] = (byte) 7;
      numArray10[41] = (byte) 7;
      numArray10[6] = (byte) 20;
      numArray10[10] = (byte) 147;
      numArray10[44] = (byte) 230;
      numArray10[3] = (byte) 220;
      numArray10[38] = (byte) 182;
      numArray10[29] = (byte) 171;
      numArray10[48 /*0x30*/] = (byte) 118;
      numArray10[21] = (byte) 160 /*0xA0*/;
      numArray10[11] = (byte) 189;
      numArray10[51] = (byte) 123;
      numArray10[52] = (byte) 25;
      numArray10[42] = (byte) 88;
      numArray10[2] = (byte) 88;
      byte[] numArray11 = new byte[55]
      {
        (byte) 72,
        (byte) 236,
        (byte) 185,
        (byte) 18,
        (byte) 194,
        (byte) 134,
        (byte) 125,
        (byte) 101,
        (byte) 244,
        (byte) 59,
        (byte) 233,
        (byte) 104,
        (byte) 133,
        (byte) 128 /*0x80*/,
        (byte) 45,
        (byte) 111,
        (byte) 54,
        (byte) 213,
        (byte) 144 /*0x90*/,
        (byte) 149,
        (byte) 82,
        (byte) 73,
        (byte) 43,
        (byte) 153,
        (byte) 198,
        (byte) 205,
        (byte) 44,
        (byte) 190,
        (byte) 81,
        (byte) 182,
        (byte) 107,
        (byte) 66,
        (byte) 148,
        byte.MaxValue,
        (byte) 220,
        (byte) 50,
        (byte) 129,
        (byte) 71,
        (byte) 101,
        (byte) 73,
        (byte) 3,
        (byte) 213,
        (byte) 105,
        (byte) 111,
        (byte) 29,
        (byte) 78,
        (byte) 88,
        (byte) 83,
        (byte) 200,
        (byte) 83,
        (byte) 69,
        (byte) 185,
        (byte) 119,
        (byte) 124,
        (byte) 202
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[28]
      {
        (byte) 47,
        (byte) 62,
        (byte) 56,
        (byte) 216,
        (byte) 30,
        (byte) 7,
        (byte) 69,
        (byte) 249,
        (byte) 21,
        (byte) 239,
        (byte) 236,
        (byte) 211,
        (byte) 22,
        (byte) 9,
        (byte) 22,
        (byte) 15,
        (byte) 112 /*0x70*/,
        (byte) 8,
        (byte) 145,
        (byte) 31 /*0x1F*/,
        (byte) 144 /*0x90*/,
        (byte) 1,
        (byte) 229,
        (byte) 159,
        (byte) 112 /*0x70*/,
        (byte) 222,
        (byte) 35,
        (byte) 251
      };
      byte[] numArray13 = new byte[28]
      {
        (byte) 238,
        (byte) 235,
        (byte) 135,
        (byte) 122,
        (byte) 114,
        (byte) 55,
        (byte) 250,
        (byte) 6,
        (byte) 17,
        (byte) 69,
        (byte) 36,
        (byte) 9,
        (byte) 206,
        (byte) 164,
        (byte) 133,
        (byte) 198,
        (byte) 40,
        (byte) 70,
        (byte) 80 /*0x50*/,
        (byte) 82,
        (byte) 73,
        (byte) 5,
        (byte) 144 /*0x90*/,
        (byte) 125,
        (byte) 73,
        (byte) 137,
        (byte) 61,
        (byte) 220
      };
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index + 275] ^= numArray13[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray14 = new byte[303];
    byte[] numArray15 = new byte[55]
    {
      (byte) 184,
      (byte) 18,
      (byte) 24,
      (byte) 11,
      (byte) 124,
      (byte) 205,
      (byte) 174,
      (byte) 128 /*0x80*/,
      (byte) 28,
      (byte) 226,
      (byte) 11,
      (byte) 137,
      (byte) 136,
      (byte) 236,
      (byte) 28,
      (byte) 74,
      (byte) 175,
      (byte) 217,
      (byte) 87,
      (byte) 178,
      (byte) 38,
      (byte) 159,
      (byte) 226,
      (byte) 86,
      (byte) 45,
      (byte) 88,
      (byte) 152,
      (byte) 103,
      (byte) 63 /*0x3F*/,
      (byte) 227,
      (byte) 74,
      (byte) 212,
      (byte) 5,
      (byte) 161,
      (byte) 52,
      (byte) 193,
      (byte) 254,
      (byte) 254,
      (byte) 153,
      (byte) 65,
      (byte) 193,
      (byte) 135,
      (byte) 117,
      (byte) 61,
      (byte) 132,
      (byte) 119,
      (byte) 93,
      (byte) 178,
      (byte) 84,
      (byte) 148,
      (byte) 115,
      (byte) 33,
      (byte) 205,
      (byte) 92,
      (byte) 39
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 242,
      (byte) 42,
      (byte) 130,
      (byte) 43,
      (byte) 91,
      (byte) 142,
      (byte) 189,
      (byte) 100,
      (byte) 153,
      (byte) 168,
      (byte) 27,
      (byte) 110,
      (byte) 167,
      (byte) 39,
      (byte) 98,
      (byte) 143,
      (byte) 87,
      (byte) 108,
      (byte) 190,
      (byte) 210,
      (byte) 182,
      (byte) 235,
      (byte) 54,
      (byte) 75,
      (byte) 27,
      (byte) 78,
      (byte) 88,
      (byte) 60,
      (byte) 103,
      (byte) 187,
      (byte) 70,
      (byte) 154,
      (byte) 6,
      (byte) 114,
      (byte) 132,
      (byte) 124,
      (byte) 33,
      (byte) 101,
      (byte) 169,
      (byte) 165,
      (byte) 196,
      (byte) 252,
      (byte) 65,
      (byte) 125,
      (byte) 86,
      (byte) 44,
      (byte) 77,
      (byte) 115,
      (byte) 225,
      (byte) 86,
      (byte) 176 /*0xB0*/,
      (byte) 94,
      (byte) 193,
      (byte) 216,
      (byte) 54
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray14, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[1] = (byte) 221;
    numArray17[31 /*0x1F*/] = (byte) 122;
    numArray17[2] = (byte) 35;
    numArray17[11] = (byte) 184;
    numArray17[13] = (byte) 75;
    numArray17[53] = (byte) 239;
    numArray17[47] = (byte) 72;
    numArray17[6] = (byte) 56;
    numArray17[16 /*0x10*/] = (byte) 21;
    numArray17[45] = (byte) 250;
    numArray17[4] = (byte) 70;
    numArray17[41] = (byte) 166;
    numArray17[38] = (byte) 122;
    numArray17[22] = (byte) 181;
    numArray17[7] = (byte) 151;
    numArray17[15] = (byte) 63 /*0x3F*/;
    numArray17[26] = (byte) 171;
    numArray17[27] = (byte) 111;
    numArray17[18] = (byte) 158;
    numArray17[19] = (byte) 41;
    numArray17[0] = (byte) 194;
    numArray17[5] = (byte) 64 /*0x40*/;
    numArray17[10] = (byte) 230;
    numArray17[49] = (byte) 225;
    numArray17[24] = (byte) 230;
    numArray17[25] = (byte) 18;
    numArray17[14] = (byte) 177;
    numArray17[36] = (byte) 117;
    numArray17[28] = (byte) 41;
    numArray17[29] = (byte) 6;
    numArray17[30] = (byte) 169;
    numArray17[23] = (byte) 250;
    numArray17[52] = (byte) 136;
    numArray17[33] = (byte) 146;
    numArray17[34] = (byte) 60;
    numArray17[35] = (byte) 33;
    numArray17[12] = (byte) 90;
    numArray17[8] = (byte) 54;
    numArray17[20] = (byte) 165;
    numArray17[39] = (byte) 71;
    numArray17[40] = (byte) 61;
    numArray17[17] = (byte) 240 /*0xF0*/;
    numArray17[42] = (byte) 78;
    numArray17[3] = (byte) 52;
    numArray17[44] = (byte) 119;
    numArray17[9] = (byte) 131;
    numArray17[46] = (byte) 250;
    numArray17[51] = (byte) 108;
    numArray17[48 /*0x30*/] = (byte) 194;
    numArray17[54] = (byte) 137;
    numArray17[43] = (byte) 243;
    numArray17[32 /*0x20*/] = (byte) 26;
    numArray17[50] = (byte) 183;
    numArray17[21] = (byte) 209;
    numArray17[37] = (byte) 12;
    byte[] numArray18 = new byte[55]
    {
      (byte) 142,
      (byte) 222,
      (byte) 175,
      (byte) 44,
      (byte) 147,
      (byte) 25,
      (byte) 26,
      (byte) 152,
      (byte) 48 /*0x30*/,
      (byte) 67,
      (byte) 44,
      (byte) 120,
      (byte) 76,
      (byte) 179,
      (byte) 10,
      (byte) 15,
      (byte) 132,
      (byte) 74,
      (byte) 246,
      (byte) 104,
      (byte) 36,
      (byte) 97,
      (byte) 232,
      (byte) 121,
      (byte) 51,
      (byte) 148,
      (byte) 223,
      (byte) 19,
      (byte) 138,
      (byte) 125,
      (byte) 119,
      (byte) 3,
      (byte) 228,
      (byte) 193,
      (byte) 59,
      (byte) 175,
      (byte) 118,
      (byte) 149,
      (byte) 94,
      (byte) 208 /*0xD0*/,
      (byte) 225,
      (byte) 132,
      (byte) 178,
      (byte) 87,
      (byte) 8,
      (byte) 212,
      (byte) 72,
      (byte) 19,
      (byte) 101,
      (byte) 244,
      (byte) 9,
      (byte) 30,
      (byte) 94,
      (byte) 149,
      (byte) 19
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray14, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 55] ^= numArray18[index];
    byte[] numArray19 = new byte[55];
    numArray19[50] = (byte) 139;
    numArray19[6] = (byte) 225;
    numArray19[52] = (byte) 195;
    numArray19[3] = (byte) 50;
    numArray19[24] = (byte) 40;
    numArray19[11] = (byte) 195;
    numArray19[33] = (byte) 146;
    numArray19[7] = (byte) 157;
    numArray19[17] = (byte) 119;
    numArray19[53] = (byte) 7;
    numArray19[10] = (byte) 190;
    numArray19[36] = (byte) 43;
    numArray19[12] = (byte) 151;
    numArray19[18] = (byte) 198;
    numArray19[4] = (byte) 27;
    numArray19[15] = (byte) 46;
    numArray19[16 /*0x10*/] = (byte) 165;
    numArray19[43] = (byte) 234;
    numArray19[30] = (byte) 39;
    numArray19[48 /*0x30*/] = (byte) 100;
    numArray19[5] = (byte) 38;
    numArray19[21] = (byte) 120;
    numArray19[22] = (byte) 142;
    numArray19[39] = (byte) 17;
    numArray19[1] = (byte) 98;
    numArray19[25] = (byte) 122;
    numArray19[26] = (byte) 46;
    numArray19[0] = (byte) 165;
    numArray19[49] = (byte) 199;
    numArray19[29] = (byte) 192 /*0xC0*/;
    numArray19[23] = (byte) 229;
    numArray19[31 /*0x1F*/] = (byte) 126;
    numArray19[46] = (byte) 155;
    numArray19[9] = (byte) 114;
    numArray19[34] = (byte) 167;
    numArray19[35] = (byte) 91;
    numArray19[19] = (byte) 101;
    numArray19[37] = (byte) 127 /*0x7F*/;
    numArray19[38] = (byte) 89;
    numArray19[32 /*0x20*/] = (byte) 222;
    numArray19[40] = (byte) 60;
    numArray19[41] = (byte) 123;
    numArray19[14] = (byte) 74;
    numArray19[20] = (byte) 168;
    numArray19[44] = (byte) 116;
    numArray19[45] = (byte) 118;
    numArray19[42] = (byte) 144 /*0x90*/;
    numArray19[47] = (byte) 155;
    numArray19[8] = (byte) 122;
    numArray19[13] = (byte) 124;
    numArray19[2] = (byte) 27;
    numArray19[51] = (byte) 140;
    numArray19[54] = (byte) 189;
    numArray19[27] = (byte) 160 /*0xA0*/;
    numArray19[28] = (byte) 70;
    byte[] numArray20 = new byte[55]
    {
      (byte) 246,
      (byte) 75,
      (byte) 195,
      (byte) 137,
      (byte) 213,
      (byte) 50,
      (byte) 79,
      (byte) 12,
      (byte) 49,
      (byte) 85,
      (byte) 244,
      (byte) 12,
      (byte) 30,
      (byte) 183,
      (byte) 55,
      (byte) 247,
      (byte) 214,
      (byte) 223,
      (byte) 212,
      (byte) 169,
      (byte) 45,
      (byte) 188,
      (byte) 83,
      (byte) 223,
      (byte) 72,
      (byte) 23,
      (byte) 164,
      (byte) 210,
      (byte) 239,
      (byte) 65,
      (byte) 10,
      (byte) 166,
      (byte) 85,
      (byte) 244,
      (byte) 93,
      (byte) 138,
      (byte) 220,
      (byte) 246,
      (byte) 253,
      (byte) 63 /*0x3F*/,
      (byte) 196,
      (byte) 247,
      (byte) 115,
      (byte) 45,
      (byte) 105,
      (byte) 224 /*0xE0*/,
      (byte) 207,
      (byte) 34,
      (byte) 124,
      (byte) 181,
      (byte) 70,
      (byte) 18,
      (byte) 181,
      byte.MaxValue,
      (byte) 156
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray14, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 110] ^= numArray20[index];
    byte[] numArray21 = new byte[55]
    {
      (byte) 198,
      (byte) 87,
      (byte) 193,
      (byte) 100,
      (byte) 5,
      (byte) 215,
      (byte) 133,
      (byte) 176 /*0xB0*/,
      (byte) 132,
      (byte) 13,
      (byte) 47,
      (byte) 89,
      (byte) 102,
      (byte) 14,
      (byte) 176 /*0xB0*/,
      (byte) 101,
      (byte) 155,
      (byte) 45,
      (byte) 9,
      (byte) 17,
      (byte) 5,
      (byte) 59,
      (byte) 33,
      (byte) 72,
      (byte) 204,
      (byte) 198,
      (byte) 187,
      (byte) 207,
      (byte) 44,
      (byte) 172,
      (byte) 248,
      (byte) 68,
      (byte) 53,
      (byte) 89,
      (byte) 46,
      (byte) 30,
      (byte) 247,
      (byte) 180,
      (byte) 245,
      (byte) 76,
      (byte) 81,
      (byte) 238,
      (byte) 176 /*0xB0*/,
      (byte) 131,
      (byte) 103,
      (byte) 109,
      (byte) 14,
      (byte) 180,
      (byte) 180,
      (byte) 8,
      (byte) 114,
      (byte) 176 /*0xB0*/,
      (byte) 134,
      (byte) 51,
      (byte) 41
    };
    byte[] numArray22 = new byte[55]
    {
      (byte) 140,
      (byte) 242,
      (byte) 223,
      (byte) 245,
      (byte) 203,
      (byte) 213,
      (byte) 231,
      (byte) 9,
      (byte) 148,
      (byte) 11,
      (byte) 53,
      (byte) 89,
      (byte) 148,
      (byte) 119,
      (byte) 54,
      (byte) 144 /*0x90*/,
      (byte) 106,
      (byte) 159,
      (byte) 177,
      (byte) 93,
      (byte) 68,
      (byte) 110,
      (byte) 190,
      (byte) 34,
      (byte) 78,
      (byte) 246,
      (byte) 192 /*0xC0*/,
      (byte) 226,
      (byte) 205,
      (byte) 189,
      (byte) 112 /*0x70*/,
      (byte) 70,
      (byte) 73,
      (byte) 27,
      (byte) 229,
      (byte) 174,
      (byte) 69,
      (byte) 221,
      (byte) 203,
      (byte) 240 /*0xF0*/,
      (byte) 247,
      (byte) 226,
      (byte) 76,
      byte.MaxValue,
      (byte) 170,
      (byte) 192 /*0xC0*/,
      (byte) 49,
      (byte) 199,
      (byte) 246,
      (byte) 243,
      (byte) 170,
      (byte) 176 /*0xB0*/,
      (byte) 177,
      (byte) 151,
      (byte) 65
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray14, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 165] ^= numArray22[index];
    byte[] numArray23 = new byte[55]
    {
      (byte) 121,
      (byte) 152,
      (byte) 62,
      (byte) 27,
      (byte) 181,
      (byte) 132,
      (byte) 215,
      (byte) 21,
      (byte) 234,
      (byte) 35,
      (byte) 13,
      (byte) 224 /*0xE0*/,
      (byte) 146,
      (byte) 110,
      (byte) 230,
      (byte) 56,
      (byte) 189,
      (byte) 192 /*0xC0*/,
      (byte) 22,
      (byte) 138,
      (byte) 157,
      (byte) 224 /*0xE0*/,
      (byte) 95,
      (byte) 184,
      (byte) 7,
      (byte) 27,
      (byte) 35,
      (byte) 58,
      (byte) 71,
      (byte) 189,
      (byte) 2,
      (byte) 47,
      (byte) 162,
      (byte) 232,
      (byte) 126,
      (byte) 28,
      (byte) 183,
      (byte) 48 /*0x30*/,
      (byte) 155,
      (byte) 105,
      (byte) 216,
      (byte) 110,
      (byte) 125,
      (byte) 73,
      (byte) 1,
      (byte) 212,
      (byte) 33,
      (byte) 154,
      (byte) 139,
      (byte) 120,
      (byte) 201,
      (byte) 115,
      (byte) 19,
      (byte) 0,
      (byte) 152
    };
    byte[] numArray24 = new byte[55]
    {
      byte.MaxValue,
      (byte) 144 /*0x90*/,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 167,
      (byte) 89,
      (byte) 110,
      (byte) 208 /*0xD0*/,
      (byte) 159,
      (byte) 110,
      (byte) 26,
      (byte) 133,
      (byte) 193,
      (byte) 37,
      (byte) 159,
      (byte) 61,
      (byte) 157,
      (byte) 181,
      (byte) 17,
      (byte) 38,
      (byte) 29,
      (byte) 199,
      (byte) 110,
      (byte) 237,
      (byte) 53,
      (byte) 40,
      (byte) 104,
      (byte) 203,
      (byte) 162,
      (byte) 211,
      (byte) 156,
      (byte) 233,
      (byte) 233,
      (byte) 197,
      (byte) 137,
      (byte) 68,
      (byte) 178,
      (byte) 101,
      (byte) 229,
      (byte) 42,
      (byte) 7,
      (byte) 69,
      (byte) 26,
      (byte) 123,
      (byte) 152,
      (byte) 241,
      (byte) 44,
      (byte) 194,
      (byte) 196,
      (byte) 221,
      (byte) 88,
      (byte) 102,
      (byte) 92,
      (byte) 200,
      (byte) 223
    };
    key.Query(true, 335, numArray23, numArray23);
    Array.Copy((Array) numArray23, 0, (Array) numArray14, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray14[index + 220] ^= numArray24[index];
    byte[] numArray25 = new byte[28]
    {
      (byte) 134,
      (byte) 20,
      (byte) 154,
      (byte) 96 /*0x60*/,
      (byte) 157,
      (byte) 205,
      (byte) 44,
      (byte) 33,
      (byte) 221,
      (byte) 61,
      (byte) 170,
      (byte) 240 /*0xF0*/,
      (byte) 202,
      (byte) 128 /*0x80*/,
      (byte) 140,
      (byte) 36,
      (byte) 173,
      (byte) 178,
      (byte) 47,
      (byte) 98,
      (byte) 21,
      (byte) 182,
      (byte) 50,
      (byte) 249,
      (byte) 137,
      (byte) 175,
      (byte) 207,
      (byte) 243
    };
    byte[] numArray26 = new byte[28]
    {
      (byte) 238,
      (byte) 127 /*0x7F*/,
      (byte) 254,
      (byte) 58,
      (byte) 7,
      (byte) 146,
      (byte) 104,
      (byte) 14,
      (byte) 107,
      (byte) 140,
      (byte) 116,
      (byte) 43,
      (byte) 29,
      (byte) 24,
      (byte) 213,
      (byte) 59,
      (byte) 201,
      (byte) 10,
      (byte) 153,
      (byte) 104,
      (byte) 31 /*0x1F*/,
      (byte) 239,
      (byte) 133,
      (byte) 223,
      (byte) 77,
      (byte) 63 /*0x3F*/,
      (byte) 138,
      (byte) 49
    };
    key.Query(true, 335, numArray25, numArray25);
    Array.Copy((Array) numArray25, 0, (Array) numArray14, 275, 28);
    for (int index = 0; index < 28; ++index)
      numArray14[index + 275] ^= numArray26[index];
    return Encoding.UTF8.GetString(numArray14);
  }

  internal static string ssp_appserver_13754()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[90];
      byte[] numArray2 = new byte[55];
      numArray2[7] = (byte) 160 /*0xA0*/;
      numArray2[33] = (byte) 90;
      numArray2[2] = (byte) 48 /*0x30*/;
      numArray2[29] = (byte) 57;
      numArray2[24] = (byte) 225;
      numArray2[10] = (byte) 35;
      numArray2[14] = (byte) 192 /*0xC0*/;
      numArray2[32 /*0x20*/] = (byte) 155;
      numArray2[54] = (byte) 231;
      numArray2[44] = (byte) 146;
      numArray2[17] = (byte) 78;
      numArray2[11] = (byte) 150;
      numArray2[12] = (byte) 119;
      numArray2[13] = (byte) 170;
      numArray2[52] = (byte) 182;
      numArray2[15] = (byte) 247;
      numArray2[16 /*0x10*/] = (byte) 61;
      numArray2[35] = (byte) 231;
      numArray2[5] = (byte) 8;
      numArray2[31 /*0x1F*/] = (byte) 193;
      numArray2[22] = (byte) 35;
      numArray2[21] = (byte) 54;
      numArray2[38] = (byte) 35;
      numArray2[39] = (byte) 164;
      numArray2[36] = (byte) 79;
      numArray2[25] = (byte) 160 /*0xA0*/;
      numArray2[26] = (byte) 215;
      numArray2[9] = (byte) 39;
      numArray2[28] = (byte) 254;
      numArray2[18] = (byte) 101;
      numArray2[34] = (byte) 11;
      numArray2[8] = (byte) 80 /*0x50*/;
      numArray2[41] = (byte) 143;
      numArray2[19] = (byte) 78;
      numArray2[3] = (byte) 27;
      numArray2[6] = (byte) 59;
      numArray2[43] = (byte) 238;
      numArray2[37] = (byte) 113;
      numArray2[23] = (byte) 207;
      numArray2[0] = (byte) 229;
      numArray2[27] = (byte) 237;
      numArray2[20] = (byte) 104;
      numArray2[42] = (byte) 17;
      numArray2[30] = (byte) 146;
      numArray2[1] = (byte) 226;
      numArray2[45] = (byte) 156;
      numArray2[46] = (byte) 130;
      numArray2[47] = (byte) 223;
      numArray2[48 /*0x30*/] = (byte) 238;
      numArray2[49] = (byte) 215;
      numArray2[50] = (byte) 182;
      numArray2[51] = (byte) 64 /*0x40*/;
      numArray2[40] = (byte) 119;
      numArray2[53] = (byte) 83;
      numArray2[4] = (byte) 1;
      byte[] numArray3 = new byte[55];
      numArray3[5] = (byte) 70;
      numArray3[30] = (byte) 214;
      numArray3[43] = (byte) 252;
      numArray3[14] = (byte) 224 /*0xE0*/;
      numArray3[4] = (byte) 64 /*0x40*/;
      numArray3[53] = (byte) 111;
      numArray3[1] = (byte) 143;
      numArray3[51] = (byte) 177;
      numArray3[2] = (byte) 110;
      numArray3[10] = (byte) 71;
      numArray3[45] = (byte) 220;
      numArray3[11] = (byte) 84;
      numArray3[12] = (byte) 132;
      numArray3[36] = (byte) 226;
      numArray3[50] = (byte) 223;
      numArray3[15] = (byte) 139;
      numArray3[26] = (byte) 73;
      numArray3[25] = (byte) 179;
      numArray3[34] = (byte) 64 /*0x40*/;
      numArray3[44] = (byte) 36;
      numArray3[0] = (byte) 55;
      numArray3[31 /*0x1F*/] = (byte) 171;
      numArray3[13] = (byte) 57;
      numArray3[23] = (byte) 78;
      numArray3[6] = (byte) 210;
      numArray3[21] = (byte) 202;
      numArray3[8] = (byte) 42;
      numArray3[27] = (byte) 44;
      numArray3[28] = (byte) 147;
      numArray3[9] = (byte) 189;
      numArray3[22] = (byte) 20;
      numArray3[7] = (byte) 1;
      numArray3[32 /*0x20*/] = (byte) 232;
      numArray3[33] = (byte) 118;
      numArray3[16 /*0x10*/] = (byte) 89;
      numArray3[35] = (byte) 72;
      numArray3[46] = (byte) 112 /*0x70*/;
      numArray3[48 /*0x30*/] = (byte) 140;
      numArray3[38] = (byte) 90;
      numArray3[17] = (byte) 206;
      numArray3[40] = (byte) 95;
      numArray3[41] = (byte) 23;
      numArray3[42] = (byte) 26;
      numArray3[39] = (byte) 123;
      numArray3[20] = (byte) 149;
      numArray3[3] = (byte) 101;
      numArray3[37] = (byte) 80 /*0x50*/;
      numArray3[47] = (byte) 115;
      numArray3[29] = (byte) 211;
      numArray3[49] = (byte) 160 /*0xA0*/;
      numArray3[24] = (byte) 107;
      numArray3[18] = (byte) 207;
      numArray3[52] = (byte) 83;
      numArray3[19] = (byte) 125;
      numArray3[54] = (byte) 71;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      numArray4[16 /*0x10*/] = (byte) 19;
      numArray4[4] = (byte) 100;
      numArray4[23] = (byte) 250;
      numArray4[13] = (byte) 237;
      numArray4[6] = (byte) 252;
      numArray4[5] = (byte) 133;
      numArray4[30] = (byte) 251;
      numArray4[19] = (byte) 196;
      numArray4[0] = (byte) 22;
      numArray4[2] = (byte) 139;
      numArray4[10] = (byte) 67;
      numArray4[33] = (byte) 241;
      numArray4[12] = (byte) 18;
      numArray4[18] = (byte) 122;
      numArray4[9] = (byte) 231;
      numArray4[15] = (byte) 233;
      numArray4[14] = (byte) 80 /*0x50*/;
      numArray4[17] = (byte) 208 /*0xD0*/;
      numArray4[28] = (byte) 142;
      numArray4[29] = (byte) 110;
      numArray4[20] = (byte) 200;
      numArray4[21] = (byte) 54;
      numArray4[22] = (byte) 180;
      numArray4[34] = (byte) 180;
      numArray4[25] = (byte) 132;
      numArray4[3] = (byte) 128 /*0x80*/;
      numArray4[26] = (byte) 126;
      numArray4[27] = (byte) 66;
      numArray4[7] = (byte) 8;
      numArray4[1] = (byte) 80 /*0x50*/;
      numArray4[24] = (byte) 186;
      numArray4[11] = (byte) 112 /*0x70*/;
      numArray4[32 /*0x20*/] = (byte) 114;
      numArray4[31 /*0x1F*/] = (byte) 66;
      numArray4[8] = (byte) 66;
      byte[] numArray5 = new byte[35];
      numArray5[26] = (byte) 147;
      numArray5[18] = (byte) 137;
      numArray5[24] = (byte) 34;
      numArray5[3] = (byte) 25;
      numArray5[5] = (byte) 166;
      numArray5[17] = (byte) 21;
      numArray5[9] = (byte) 152;
      numArray5[10] = (byte) 193;
      numArray5[15] = (byte) 224 /*0xE0*/;
      numArray5[34] = (byte) 134;
      numArray5[1] = (byte) 120;
      numArray5[23] = (byte) 156;
      numArray5[13] = (byte) 108;
      numArray5[32 /*0x20*/] = (byte) 151;
      numArray5[14] = (byte) 0;
      numArray5[0] = (byte) 192 /*0xC0*/;
      numArray5[16 /*0x10*/] = (byte) 201;
      numArray5[19] = (byte) 44;
      numArray5[22] = (byte) 180;
      numArray5[27] = (byte) 236;
      numArray5[4] = (byte) 250;
      numArray5[21] = (byte) 21;
      numArray5[8] = (byte) 106;
      numArray5[11] = (byte) 194;
      numArray5[31 /*0x1F*/] = (byte) 223;
      numArray5[25] = (byte) 38;
      numArray5[20] = (byte) 222;
      numArray5[12] = (byte) 80 /*0x50*/;
      numArray5[7] = (byte) 30;
      numArray5[28] = (byte) 187;
      numArray5[30] = (byte) 252;
      numArray5[29] = (byte) 153;
      numArray5[6] = (byte) 11;
      numArray5[33] = (byte) 36;
      numArray5[2] = (byte) 40;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[26];
      byte[] response = new byte[26];
      Array.Copy((Array) sc_13686.sspq, 917, (Array) numArray6, 0, 26);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13686.sspr, 917, (Array) numArray6, 0, 26);
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
    byte[] numArray7 = new byte[90];
    byte[] numArray8 = new byte[55];
    numArray8[22] = (byte) 235;
    numArray8[14] = (byte) 210;
    numArray8[2] = (byte) 149;
    numArray8[42] = (byte) 47;
    numArray8[7] = (byte) 152;
    numArray8[27] = (byte) 112 /*0x70*/;
    numArray8[6] = (byte) 128 /*0x80*/;
    numArray8[15] = (byte) 47;
    numArray8[8] = (byte) 228;
    numArray8[9] = (byte) 149;
    numArray8[25] = (byte) 211;
    numArray8[11] = (byte) 81;
    numArray8[39] = (byte) 155;
    numArray8[13] = (byte) 235;
    numArray8[32 /*0x20*/] = (byte) 6;
    numArray8[51] = (byte) 22;
    numArray8[35] = (byte) 109;
    numArray8[17] = (byte) 201;
    numArray8[18] = (byte) 41;
    numArray8[0] = byte.MaxValue;
    numArray8[20] = (byte) 104;
    numArray8[52] = (byte) 105;
    numArray8[3] = byte.MaxValue;
    numArray8[23] = (byte) 159;
    numArray8[24] = (byte) 55;
    numArray8[47] = (byte) 212;
    numArray8[43] = (byte) 207;
    numArray8[38] = (byte) 84;
    numArray8[28] = (byte) 143;
    numArray8[12] = (byte) 92;
    numArray8[30] = (byte) 95;
    numArray8[31 /*0x1F*/] = (byte) 92;
    numArray8[48 /*0x30*/] = (byte) 214;
    numArray8[33] = (byte) 248;
    numArray8[5] = (byte) 85;
    numArray8[41] = (byte) 141;
    numArray8[36] = (byte) 232;
    numArray8[16 /*0x10*/] = (byte) 228;
    numArray8[1] = (byte) 164;
    numArray8[4] = (byte) 163;
    numArray8[21] = (byte) 29;
    numArray8[10] = (byte) 103;
    numArray8[26] = (byte) 231;
    numArray8[34] = (byte) 173;
    numArray8[44] = (byte) 160 /*0xA0*/;
    numArray8[45] = (byte) 67;
    numArray8[19] = (byte) 17;
    numArray8[46] = (byte) 8;
    numArray8[37] = (byte) 174;
    numArray8[49] = (byte) 127 /*0x7F*/;
    numArray8[50] = (byte) 70;
    numArray8[40] = (byte) 229;
    numArray8[29] = (byte) 253;
    numArray8[53] = (byte) 247;
    numArray8[54] = (byte) 54;
    byte[] numArray9 = new byte[55];
    numArray9[51] = (byte) 154;
    numArray9[23] = (byte) 201;
    numArray9[2] = (byte) 31 /*0x1F*/;
    numArray9[32 /*0x20*/] = (byte) 190;
    numArray9[4] = (byte) 101;
    numArray9[0] = (byte) 133;
    numArray9[34] = (byte) 139;
    numArray9[9] = (byte) 214;
    numArray9[8] = (byte) 11;
    numArray9[24] = (byte) 245;
    numArray9[45] = (byte) 167;
    numArray9[11] = (byte) 114;
    numArray9[48 /*0x30*/] = (byte) 251;
    numArray9[18] = (byte) 254;
    numArray9[14] = (byte) 88;
    numArray9[27] = (byte) 37;
    numArray9[16 /*0x10*/] = (byte) 68;
    numArray9[17] = (byte) 205;
    numArray9[50] = (byte) 105;
    numArray9[12] = (byte) 181;
    numArray9[20] = (byte) 246;
    numArray9[21] = (byte) 181;
    numArray9[52] = (byte) 179;
    numArray9[22] = (byte) 173;
    numArray9[26] = (byte) 118;
    numArray9[5] = (byte) 176 /*0xB0*/;
    numArray9[7] = (byte) 242;
    numArray9[53] = (byte) 237;
    numArray9[28] = (byte) 204;
    numArray9[19] = (byte) 242;
    numArray9[30] = (byte) 33;
    numArray9[31 /*0x1F*/] = (byte) 14;
    numArray9[44] = (byte) 73;
    numArray9[40] = (byte) 150;
    numArray9[6] = (byte) 113;
    numArray9[25] = (byte) 167;
    numArray9[36] = (byte) 204;
    numArray9[37] = (byte) 38;
    numArray9[1] = (byte) 81;
    numArray9[10] = (byte) 88;
    numArray9[15] = (byte) 152;
    numArray9[41] = (byte) 217;
    numArray9[29] = (byte) 84;
    numArray9[43] = (byte) 116;
    numArray9[49] = (byte) 197;
    numArray9[39] = (byte) 47;
    numArray9[46] = (byte) 89;
    numArray9[47] = (byte) 68;
    numArray9[38] = (byte) 148;
    numArray9[33] = (byte) 211;
    numArray9[35] = (byte) 173;
    numArray9[42] = (byte) 249;
    numArray9[13] = (byte) 211;
    numArray9[3] = (byte) 109;
    numArray9[54] = (byte) 159;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[35]
    {
      (byte) 141,
      (byte) 214,
      (byte) 220,
      (byte) 223,
      (byte) 77,
      (byte) 34,
      (byte) 136,
      (byte) 68,
      (byte) 3,
      (byte) 248,
      (byte) 59,
      (byte) 153,
      (byte) 155,
      (byte) 91,
      (byte) 56,
      (byte) 110,
      (byte) 141,
      (byte) 247,
      (byte) 204,
      (byte) 140,
      (byte) 212,
      (byte) 237,
      (byte) 62,
      (byte) 36,
      (byte) 87,
      (byte) 104,
      (byte) 245,
      (byte) 226,
      (byte) 246,
      (byte) 153,
      (byte) 204,
      (byte) 96 /*0x60*/,
      (byte) 138,
      (byte) 250,
      (byte) 108
    };
    byte[] numArray11 = new byte[35]
    {
      (byte) 55,
      (byte) 69,
      (byte) 56,
      (byte) 14,
      (byte) 73,
      (byte) 206,
      (byte) 74,
      (byte) 34,
      (byte) 245,
      (byte) 56,
      (byte) 88,
      (byte) 122,
      (byte) 244,
      (byte) 28,
      (byte) 238,
      (byte) 153,
      (byte) 20,
      (byte) 62,
      (byte) 108,
      (byte) 45,
      (byte) 83,
      (byte) 153,
      (byte) 30,
      (byte) 127 /*0x7F*/,
      (byte) 151,
      (byte) 194,
      (byte) 181,
      (byte) 220,
      (byte) 110,
      (byte) 230,
      (byte) 202,
      (byte) 8,
      (byte) 7,
      (byte) 186,
      (byte) 132
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 35);
    for (int index = 0; index < 35; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13755()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[92];
      byte[] numArray2 = new byte[55]
      {
        (byte) 103,
        (byte) 144 /*0x90*/,
        (byte) 105,
        (byte) 120,
        (byte) 196,
        (byte) 199,
        (byte) 139,
        (byte) 36,
        (byte) 134,
        (byte) 138,
        (byte) 66,
        (byte) 239,
        (byte) 127 /*0x7F*/,
        (byte) 57,
        (byte) 194,
        (byte) 71,
        (byte) 160 /*0xA0*/,
        (byte) 152,
        (byte) 184,
        (byte) 135,
        (byte) 228,
        (byte) 45,
        (byte) 165,
        (byte) 77,
        (byte) 106,
        (byte) 43,
        (byte) 218,
        (byte) 15,
        (byte) 160 /*0xA0*/,
        (byte) 239,
        (byte) 250,
        (byte) 201,
        (byte) 153,
        (byte) 229,
        (byte) 100,
        (byte) 251,
        (byte) 107,
        (byte) 116,
        (byte) 122,
        (byte) 92,
        (byte) 203,
        (byte) 68,
        (byte) 110,
        (byte) 87,
        (byte) 179,
        (byte) 169,
        (byte) 77,
        (byte) 39,
        (byte) 199,
        (byte) 178,
        (byte) 203,
        (byte) 166,
        (byte) 235,
        (byte) 107,
        (byte) 44
      };
      byte[] numArray3 = new byte[55];
      numArray3[24] = (byte) 31 /*0x1F*/;
      numArray3[1] = (byte) 167;
      numArray3[2] = (byte) 187;
      numArray3[3] = (byte) 226;
      numArray3[17] = (byte) 49;
      numArray3[5] = (byte) 142;
      numArray3[12] = (byte) 241;
      numArray3[7] = (byte) 149;
      numArray3[8] = (byte) 253;
      numArray3[44] = (byte) 180;
      numArray3[49] = (byte) 113;
      numArray3[11] = (byte) 169;
      numArray3[42] = (byte) 25;
      numArray3[50] = (byte) 42;
      numArray3[14] = (byte) 152;
      numArray3[13] = (byte) 46;
      numArray3[20] = (byte) 120;
      numArray3[6] = (byte) 227;
      numArray3[18] = (byte) 124;
      numArray3[19] = (byte) 68;
      numArray3[16 /*0x10*/] = (byte) 95;
      numArray3[21] = (byte) 96 /*0x60*/;
      numArray3[52] = (byte) 210;
      numArray3[40] = (byte) 245;
      numArray3[22] = byte.MaxValue;
      numArray3[31 /*0x1F*/] = (byte) 117;
      numArray3[32 /*0x20*/] = (byte) 247;
      numArray3[43] = (byte) 192 /*0xC0*/;
      numArray3[28] = (byte) 162;
      numArray3[23] = (byte) 235;
      numArray3[30] = (byte) 136;
      numArray3[9] = (byte) 221;
      numArray3[25] = (byte) 191;
      numArray3[33] = (byte) 115;
      numArray3[34] = (byte) 94;
      numArray3[35] = (byte) 146;
      numArray3[10] = (byte) 124;
      numArray3[47] = (byte) 208 /*0xD0*/;
      numArray3[38] = (byte) 32 /*0x20*/;
      numArray3[0] = (byte) 160 /*0xA0*/;
      numArray3[26] = (byte) 208 /*0xD0*/;
      numArray3[39] = (byte) 82;
      numArray3[27] = (byte) 43;
      numArray3[4] = (byte) 12;
      numArray3[15] = (byte) 44;
      numArray3[45] = (byte) 252;
      numArray3[46] = (byte) 164;
      numArray3[37] = (byte) 156;
      numArray3[48 /*0x30*/] = (byte) 123;
      numArray3[41] = (byte) 220;
      numArray3[29] = (byte) 240 /*0xF0*/;
      numArray3[51] = (byte) 179;
      numArray3[36] = (byte) 107;
      numArray3[53] = (byte) 56;
      numArray3[54] = (byte) 202;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37]
      {
        (byte) 130,
        (byte) 77,
        (byte) 186,
        (byte) 57,
        (byte) 51,
        (byte) 150,
        (byte) 116,
        (byte) 60,
        (byte) 154,
        (byte) 40,
        (byte) 223,
        (byte) 169,
        (byte) 182,
        (byte) 141,
        (byte) 197,
        (byte) 60,
        (byte) 235,
        (byte) 107,
        (byte) 224 /*0xE0*/,
        (byte) 232,
        (byte) 134,
        (byte) 94,
        (byte) 109,
        (byte) 41,
        (byte) 96 /*0x60*/,
        (byte) 70,
        (byte) 23,
        (byte) 131,
        (byte) 204,
        (byte) 78,
        (byte) 125,
        (byte) 45,
        (byte) 174,
        (byte) 115,
        (byte) 205,
        (byte) 24,
        (byte) 38
      };
      byte[] numArray5 = new byte[37]
      {
        (byte) 138,
        (byte) 246,
        (byte) 175,
        (byte) 215,
        (byte) 161,
        (byte) 121,
        (byte) 27,
        (byte) 252,
        (byte) 192 /*0xC0*/,
        (byte) 34,
        (byte) 212,
        (byte) 61,
        (byte) 127 /*0x7F*/,
        (byte) 204,
        (byte) 148,
        (byte) 138,
        (byte) 99,
        (byte) 44,
        (byte) 67,
        (byte) 53,
        (byte) 192 /*0xC0*/,
        (byte) 62,
        (byte) 211,
        (byte) 140,
        (byte) 141,
        (byte) 44,
        (byte) 56,
        (byte) 104,
        (byte) 204,
        (byte) 15,
        (byte) 123,
        (byte) 168,
        (byte) 40,
        (byte) 78,
        (byte) 36,
        (byte) 156,
        (byte) 166
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_13686.sspq, 943, (Array) numArray6, 0, 53);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13686.sspr, 943, (Array) numArray6, 0, 53);
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
    byte[] numArray7 = new byte[92];
    byte[] numArray8 = new byte[55];
    numArray8[20] = (byte) 241;
    numArray8[13] = (byte) 243;
    numArray8[2] = (byte) 27;
    numArray8[1] = (byte) 112 /*0x70*/;
    numArray8[16 /*0x10*/] = (byte) 101;
    numArray8[5] = (byte) 34;
    numArray8[6] = (byte) 174;
    numArray8[7] = (byte) 8;
    numArray8[8] = (byte) 137;
    numArray8[32 /*0x20*/] = (byte) 86;
    numArray8[27] = (byte) 223;
    numArray8[35] = (byte) 21;
    numArray8[3] = (byte) 84;
    numArray8[47] = (byte) 143;
    numArray8[25] = (byte) 112 /*0x70*/;
    numArray8[15] = (byte) 26;
    numArray8[41] = (byte) 242;
    numArray8[10] = (byte) 53;
    numArray8[12] = (byte) 245;
    numArray8[19] = (byte) 34;
    numArray8[49] = (byte) 66;
    numArray8[9] = (byte) 45;
    numArray8[22] = (byte) 72;
    numArray8[26] = (byte) 46;
    numArray8[23] = (byte) 238;
    numArray8[17] = (byte) 157;
    numArray8[50] = (byte) 67;
    numArray8[36] = (byte) 223;
    numArray8[38] = (byte) 193;
    numArray8[42] = (byte) 68;
    numArray8[30] = (byte) 116;
    numArray8[45] = (byte) 187;
    numArray8[14] = (byte) 13;
    numArray8[21] = (byte) 176 /*0xB0*/;
    numArray8[34] = (byte) 11;
    numArray8[40] = (byte) 8;
    numArray8[52] = (byte) 90;
    numArray8[37] = (byte) 106;
    numArray8[46] = (byte) 67;
    numArray8[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    numArray8[24] = (byte) 161;
    numArray8[18] = (byte) 59;
    numArray8[0] = (byte) 171;
    numArray8[43] = (byte) 123;
    numArray8[44] = (byte) 75;
    numArray8[29] = (byte) 22;
    numArray8[33] = (byte) 31 /*0x1F*/;
    numArray8[28] = (byte) 228;
    numArray8[39] = (byte) 50;
    numArray8[4] = (byte) 128 /*0x80*/;
    numArray8[11] = (byte) 239;
    numArray8[51] = (byte) 154;
    numArray8[48 /*0x30*/] = (byte) 13;
    numArray8[53] = (byte) 60;
    numArray8[54] = (byte) 61;
    byte[] numArray9 = new byte[55]
    {
      (byte) 87,
      (byte) 140,
      (byte) 165,
      (byte) 157,
      (byte) 41,
      (byte) 211,
      (byte) 54,
      (byte) 90,
      (byte) 16 /*0x10*/,
      (byte) 198,
      (byte) 159,
      (byte) 3,
      (byte) 128 /*0x80*/,
      (byte) 125,
      (byte) 219,
      (byte) 29,
      (byte) 52,
      (byte) 121,
      (byte) 253,
      (byte) 58,
      (byte) 69,
      (byte) 6,
      (byte) 45,
      (byte) 169,
      (byte) 238,
      (byte) 204,
      (byte) 82,
      (byte) 138,
      (byte) 236,
      (byte) 177,
      byte.MaxValue,
      (byte) 188,
      (byte) 163,
      (byte) 110,
      (byte) 213,
      (byte) 229,
      (byte) 179,
      (byte) 83,
      (byte) 54,
      (byte) 148,
      (byte) 24,
      (byte) 126,
      (byte) 253,
      (byte) 197,
      (byte) 128 /*0x80*/,
      (byte) 41,
      byte.MaxValue,
      (byte) 21,
      (byte) 158,
      (byte) 182,
      (byte) 143,
      (byte) 60,
      (byte) 89,
      (byte) 122,
      (byte) 52
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[37];
    numArray10[23] = (byte) 191;
    numArray10[10] = (byte) 244;
    numArray10[6] = (byte) 160 /*0xA0*/;
    numArray10[21] = (byte) 12;
    numArray10[4] = (byte) 252;
    numArray10[2] = (byte) 226;
    numArray10[1] = (byte) 205;
    numArray10[3] = (byte) 85;
    numArray10[35] = (byte) 232;
    numArray10[9] = (byte) 127 /*0x7F*/;
    numArray10[5] = (byte) 131;
    numArray10[11] = (byte) 214;
    numArray10[0] = (byte) 139;
    numArray10[13] = (byte) 137;
    numArray10[14] = (byte) 68;
    numArray10[15] = (byte) 42;
    numArray10[27] = (byte) 138;
    numArray10[17] = (byte) 223;
    numArray10[18] = (byte) 48 /*0x30*/;
    numArray10[25] = (byte) 188;
    numArray10[20] = (byte) 11;
    numArray10[16 /*0x10*/] = (byte) 203;
    numArray10[7] = (byte) 223;
    numArray10[22] = (byte) 194;
    numArray10[24] = (byte) 246;
    numArray10[8] = (byte) 145;
    numArray10[26] = (byte) 154;
    numArray10[12] = (byte) 9;
    numArray10[28] = (byte) 26;
    numArray10[34] = (byte) 38;
    numArray10[30] = (byte) 246;
    numArray10[29] = (byte) 211;
    numArray10[32 /*0x20*/] = (byte) 129;
    numArray10[31 /*0x1F*/] = (byte) 162;
    numArray10[19] = (byte) 153;
    numArray10[33] = (byte) 147;
    numArray10[36] = (byte) 206;
    byte[] numArray11 = new byte[37]
    {
      (byte) 252,
      (byte) 202,
      (byte) 41,
      (byte) 74,
      (byte) 111,
      (byte) 21,
      (byte) 164,
      (byte) 83,
      (byte) 120,
      (byte) 132,
      (byte) 234,
      (byte) 74,
      (byte) 5,
      (byte) 87,
      (byte) 179,
      (byte) 246,
      (byte) 74,
      (byte) 88,
      (byte) 40,
      (byte) 87,
      (byte) 58,
      (byte) 95,
      (byte) 33,
      (byte) 221,
      (byte) 154,
      (byte) 221,
      (byte) 132,
      (byte) 205,
      (byte) 221,
      (byte) 22,
      (byte) 165,
      (byte) 51,
      (byte) 168,
      (byte) 161,
      (byte) 60,
      (byte) 29,
      (byte) 167
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 37);
    for (int index = 0; index < 37; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13756()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[87];
      byte[] numArray2 = new byte[55];
      numArray2[0] = (byte) 69;
      numArray2[10] = (byte) 68;
      numArray2[38] = (byte) 143;
      numArray2[33] = (byte) 234;
      numArray2[8] = (byte) 148;
      numArray2[5] = (byte) 134;
      numArray2[47] = (byte) 176 /*0xB0*/;
      numArray2[7] = (byte) 179;
      numArray2[42] = (byte) 189;
      numArray2[9] = (byte) 139;
      numArray2[48 /*0x30*/] = (byte) 90;
      numArray2[11] = (byte) 167;
      numArray2[12] = (byte) 221;
      numArray2[39] = (byte) 62;
      numArray2[14] = (byte) 136;
      numArray2[18] = (byte) 29;
      numArray2[16 /*0x10*/] = (byte) 97;
      numArray2[17] = (byte) 27;
      numArray2[31 /*0x1F*/] = (byte) 38;
      numArray2[40] = (byte) 112 /*0x70*/;
      numArray2[20] = (byte) 25;
      numArray2[53] = (byte) 192 /*0xC0*/;
      numArray2[22] = (byte) 123;
      numArray2[26] = (byte) 56;
      numArray2[24] = (byte) 25;
      numArray2[44] = (byte) 49;
      numArray2[30] = (byte) 141;
      numArray2[27] = (byte) 162;
      numArray2[28] = (byte) 161;
      numArray2[29] = (byte) 40;
      numArray2[41] = (byte) 213;
      numArray2[54] = (byte) 6;
      numArray2[32 /*0x20*/] = (byte) 119;
      numArray2[21] = (byte) 39;
      numArray2[34] = (byte) 221;
      numArray2[4] = (byte) 134;
      numArray2[36] = (byte) 144 /*0x90*/;
      numArray2[37] = (byte) 204;
      numArray2[50] = (byte) 131;
      numArray2[13] = (byte) 171;
      numArray2[46] = (byte) 125;
      numArray2[1] = (byte) 37;
      numArray2[49] = (byte) 164;
      numArray2[43] = (byte) 201;
      numArray2[19] = (byte) 71;
      numArray2[52] = (byte) 10;
      numArray2[23] = (byte) 150;
      numArray2[3] = (byte) 28;
      numArray2[25] = (byte) 167;
      numArray2[2] = (byte) 175;
      numArray2[15] = (byte) 253;
      numArray2[51] = (byte) 77;
      numArray2[35] = (byte) 57;
      numArray2[45] = (byte) 188;
      numArray2[6] = (byte) 77;
      byte[] numArray3 = new byte[55]
      {
        (byte) 190,
        (byte) 135,
        (byte) 231,
        (byte) 91,
        (byte) 23,
        (byte) 90,
        (byte) 160 /*0xA0*/,
        (byte) 80 /*0x50*/,
        (byte) 91,
        (byte) 85,
        (byte) 180,
        (byte) 98,
        (byte) 194,
        (byte) 166,
        (byte) 22,
        (byte) 135,
        (byte) 153,
        (byte) 164,
        (byte) 96 /*0x60*/,
        (byte) 201,
        (byte) 19,
        (byte) 250,
        (byte) 123,
        (byte) 253,
        (byte) 123,
        (byte) 201,
        (byte) 24,
        (byte) 113,
        (byte) 159,
        (byte) 35,
        (byte) 60,
        (byte) 222,
        (byte) 152,
        (byte) 208 /*0xD0*/,
        (byte) 125,
        (byte) 15,
        (byte) 228,
        (byte) 131,
        (byte) 110,
        (byte) 186,
        (byte) 34,
        (byte) 155,
        (byte) 215,
        (byte) 10,
        (byte) 39,
        (byte) 133,
        (byte) 87,
        (byte) 252,
        (byte) 51,
        (byte) 141,
        (byte) 0,
        (byte) 35,
        (byte) 212,
        (byte) 86,
        (byte) 169
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      numArray4[19] = (byte) 24;
      numArray4[1] = (byte) 166;
      numArray4[14] = (byte) 205;
      numArray4[17] = (byte) 16 /*0x10*/;
      numArray4[4] = (byte) 98;
      numArray4[8] = (byte) 1;
      numArray4[28] = (byte) 126;
      numArray4[7] = (byte) 190;
      numArray4[25] = (byte) 60;
      numArray4[9] = (byte) 119;
      numArray4[10] = (byte) 14;
      numArray4[11] = (byte) 13;
      numArray4[6] = (byte) 198;
      numArray4[13] = (byte) 246;
      numArray4[31 /*0x1F*/] = (byte) 34;
      numArray4[15] = (byte) 218;
      numArray4[2] = (byte) 180;
      numArray4[0] = (byte) 180;
      numArray4[12] = (byte) 192 /*0xC0*/;
      numArray4[5] = (byte) 20;
      numArray4[20] = (byte) 231;
      numArray4[21] = (byte) 81;
      numArray4[23] = (byte) 159;
      numArray4[3] = (byte) 198;
      numArray4[24] = (byte) 164;
      numArray4[30] = (byte) 205;
      numArray4[27] = (byte) 183;
      numArray4[22] = (byte) 207;
      numArray4[26] = (byte) 198;
      numArray4[16 /*0x10*/] = (byte) 239;
      numArray4[18] = (byte) 128 /*0x80*/;
      numArray4[29] = (byte) 241;
      byte[] numArray5 = new byte[32 /*0x20*/]
      {
        (byte) 217,
        (byte) 79,
        (byte) 18,
        (byte) 100,
        (byte) 194,
        (byte) 188,
        (byte) 166,
        (byte) 229,
        (byte) 205,
        (byte) 71,
        (byte) 138,
        (byte) 225,
        (byte) 215,
        (byte) 59,
        (byte) 7,
        (byte) 114,
        (byte) 195,
        byte.MaxValue,
        (byte) 62,
        (byte) 173,
        (byte) 71,
        (byte) 194,
        (byte) 140,
        (byte) 71,
        (byte) 68,
        (byte) 41,
        (byte) 94,
        (byte) 91,
        (byte) 254,
        (byte) 76,
        (byte) 65,
        (byte) 161
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[87];
    byte[] numArray7 = new byte[55]
    {
      (byte) 42,
      (byte) 51,
      (byte) 170,
      (byte) 222,
      (byte) 198,
      (byte) 173,
      (byte) 209,
      (byte) 140,
      (byte) 219,
      (byte) 63 /*0x3F*/,
      (byte) 185,
      (byte) 34,
      (byte) 205,
      (byte) 242,
      (byte) 133,
      (byte) 150,
      (byte) 151,
      (byte) 181,
      (byte) 244,
      (byte) 225,
      (byte) 113,
      (byte) 133,
      (byte) 94,
      (byte) 75,
      (byte) 231,
      (byte) 6,
      (byte) 61,
      (byte) 95,
      (byte) 124,
      (byte) 203,
      (byte) 1,
      (byte) 108,
      (byte) 144 /*0x90*/,
      (byte) 213,
      (byte) 31 /*0x1F*/,
      (byte) 12,
      (byte) 118,
      (byte) 8,
      (byte) 117,
      (byte) 44,
      (byte) 111,
      (byte) 14,
      (byte) 53,
      (byte) 35,
      (byte) 29,
      (byte) 242,
      (byte) 84,
      (byte) 51,
      (byte) 201,
      (byte) 121,
      (byte) 169,
      (byte) 90,
      (byte) 181,
      (byte) 10,
      (byte) 142
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 28,
      (byte) 32 /*0x20*/,
      (byte) 159,
      (byte) 69,
      (byte) 177,
      (byte) 222,
      (byte) 143,
      (byte) 2,
      (byte) 223,
      (byte) 65,
      (byte) 211,
      (byte) 199,
      (byte) 30,
      (byte) 180,
      (byte) 48 /*0x30*/,
      (byte) 202,
      (byte) 103,
      (byte) 195,
      (byte) 144 /*0x90*/,
      (byte) 82,
      (byte) 226,
      (byte) 207,
      (byte) 117,
      (byte) 227,
      (byte) 227,
      (byte) 1,
      (byte) 16 /*0x10*/,
      (byte) 95,
      (byte) 228,
      (byte) 126,
      (byte) 9,
      (byte) 9,
      (byte) 3,
      (byte) 173,
      (byte) 87,
      (byte) 63 /*0x3F*/,
      (byte) 118,
      (byte) 5,
      (byte) 75,
      (byte) 100,
      (byte) 153,
      (byte) 146,
      (byte) 217,
      (byte) 38,
      (byte) 168,
      (byte) 212,
      (byte) 133,
      (byte) 66,
      (byte) 50,
      (byte) 4,
      (byte) 206,
      (byte) 140,
      (byte) 7,
      (byte) 29,
      (byte) 101
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[32 /*0x20*/]
    {
      (byte) 95,
      (byte) 246,
      (byte) 191,
      (byte) 112 /*0x70*/,
      (byte) 114,
      (byte) 16 /*0x10*/,
      (byte) 16 /*0x10*/,
      (byte) 62,
      (byte) 76,
      (byte) 145,
      (byte) 248,
      (byte) 211,
      (byte) 120,
      (byte) 32 /*0x20*/,
      (byte) 142,
      (byte) 64 /*0x40*/,
      (byte) 200,
      (byte) 114,
      (byte) 99,
      (byte) 79,
      (byte) 58,
      (byte) 45,
      (byte) 245,
      (byte) 178,
      (byte) 10,
      (byte) 150,
      (byte) 3,
      (byte) 33,
      (byte) 166,
      (byte) 183,
      (byte) 134,
      (byte) 71
    };
    byte[] numArray10 = new byte[32 /*0x20*/];
    numArray10[25] = (byte) 45;
    numArray10[1] = (byte) 113;
    numArray10[2] = (byte) 226;
    numArray10[11] = (byte) 193;
    numArray10[19] = (byte) 121;
    numArray10[8] = (byte) 212;
    numArray10[6] = (byte) 108;
    numArray10[24] = (byte) 186;
    numArray10[30] = (byte) 250;
    numArray10[9] = (byte) 242;
    numArray10[10] = (byte) 34;
    numArray10[4] = (byte) 205;
    numArray10[31 /*0x1F*/] = (byte) 40;
    numArray10[23] = (byte) 196;
    numArray10[14] = (byte) 139;
    numArray10[15] = (byte) 167;
    numArray10[16 /*0x10*/] = (byte) 108;
    numArray10[21] = (byte) 65;
    numArray10[18] = (byte) 56;
    numArray10[13] = (byte) 240 /*0xF0*/;
    numArray10[20] = (byte) 55;
    numArray10[27] = (byte) 136;
    numArray10[22] = (byte) 234;
    numArray10[12] = (byte) 139;
    numArray10[0] = (byte) 127 /*0x7F*/;
    numArray10[26] = (byte) 102;
    numArray10[28] = (byte) 141;
    numArray10[7] = (byte) 38;
    numArray10[3] = (byte) 138;
    numArray10[17] = (byte) 170;
    numArray10[5] = (byte) 117;
    numArray10[29] = (byte) 189;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13757()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[58];
      byte[] numArray2 = new byte[55]
      {
        (byte) 10,
        (byte) 123,
        (byte) 138,
        (byte) 11,
        (byte) 116,
        (byte) 101,
        (byte) 41,
        (byte) 85,
        (byte) 92,
        (byte) 149,
        (byte) 85,
        (byte) 170,
        (byte) 68,
        (byte) 170,
        (byte) 6,
        (byte) 140,
        (byte) 202,
        (byte) 239,
        (byte) 27,
        (byte) 36,
        (byte) 157,
        (byte) 227,
        (byte) 17,
        (byte) 168,
        (byte) 86,
        (byte) 33,
        (byte) 240 /*0xF0*/,
        (byte) 55,
        (byte) 78,
        (byte) 59,
        (byte) 85,
        (byte) 171,
        (byte) 7,
        (byte) 16 /*0x10*/,
        (byte) 78,
        (byte) 158,
        (byte) 144 /*0x90*/,
        (byte) 225,
        (byte) 34,
        (byte) 197,
        (byte) 44,
        (byte) 69,
        (byte) 214,
        (byte) 108,
        (byte) 183,
        (byte) 187,
        (byte) 150,
        (byte) 98,
        (byte) 231,
        (byte) 11,
        (byte) 203,
        (byte) 27,
        (byte) 149,
        (byte) 51,
        (byte) 162
      };
      byte[] numArray3 = new byte[55];
      numArray3[29] = (byte) 91;
      numArray3[1] = (byte) 111;
      numArray3[2] = (byte) 229;
      numArray3[52] = (byte) 218;
      numArray3[4] = (byte) 65;
      numArray3[5] = (byte) 14;
      numArray3[6] = (byte) 57;
      numArray3[12] = (byte) 237;
      numArray3[8] = (byte) 41;
      numArray3[34] = (byte) 173;
      numArray3[32 /*0x20*/] = (byte) 17;
      numArray3[23] = (byte) 130;
      numArray3[47] = (byte) 225;
      numArray3[13] = (byte) 127 /*0x7F*/;
      numArray3[19] = (byte) 158;
      numArray3[15] = (byte) 182;
      numArray3[14] = (byte) 163;
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[18] = (byte) 17;
      numArray3[30] = (byte) 14;
      numArray3[11] = (byte) 212;
      numArray3[35] = (byte) 25;
      numArray3[22] = (byte) 159;
      numArray3[10] = (byte) 225;
      numArray3[7] = (byte) 100;
      numArray3[31 /*0x1F*/] = (byte) 126;
      numArray3[26] = (byte) 134;
      numArray3[53] = (byte) 173;
      numArray3[16 /*0x10*/] = (byte) 244;
      numArray3[38] = (byte) 114;
      numArray3[24] = (byte) 76;
      numArray3[3] = (byte) 247;
      numArray3[21] = (byte) 10;
      numArray3[42] = (byte) 127 /*0x7F*/;
      numArray3[39] = (byte) 99;
      numArray3[48 /*0x30*/] = (byte) 110;
      numArray3[36] = (byte) 70;
      numArray3[27] = (byte) 108;
      numArray3[33] = (byte) 84;
      numArray3[49] = (byte) 36;
      numArray3[17] = (byte) 199;
      numArray3[41] = (byte) 123;
      numArray3[50] = (byte) 237;
      numArray3[43] = (byte) 215;
      numArray3[44] = (byte) 229;
      numArray3[20] = (byte) 146;
      numArray3[46] = (byte) 96 /*0x60*/;
      numArray3[45] = (byte) 21;
      numArray3[9] = (byte) 140;
      numArray3[40] = (byte) 58;
      numArray3[28] = (byte) 165;
      numArray3[51] = (byte) 164;
      numArray3[37] = (byte) 124;
      numArray3[25] = (byte) 86;
      numArray3[54] = (byte) 252;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[3]
      {
        (byte) 0,
        (byte) 0,
        (byte) 200
      };
      numArray4[1] = (byte) 26;
      numArray4[0] = (byte) 240 /*0xF0*/;
      byte[] numArray5 = new byte[3]
      {
        (byte) 60,
        (byte) 162,
        (byte) 228
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_13686.sspq, 996, (Array) numArray6, 0, 54);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13686.sspr, 996, (Array) numArray6, 0, 54);
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
    numArray8[11] = (byte) 83;
    numArray8[1] = (byte) 162;
    numArray8[2] = (byte) 35;
    numArray8[5] = (byte) 171;
    numArray8[4] = (byte) 142;
    numArray8[29] = (byte) 200;
    numArray8[14] = (byte) 138;
    numArray8[7] = (byte) 155;
    numArray8[18] = (byte) 102;
    numArray8[49] = (byte) 173;
    numArray8[53] = (byte) 133;
    numArray8[24] = (byte) 34;
    numArray8[12] = (byte) 103;
    numArray8[8] = (byte) 37;
    numArray8[37] = (byte) 134;
    numArray8[15] = (byte) 178;
    numArray8[16 /*0x10*/] = (byte) 133;
    numArray8[17] = (byte) 112 /*0x70*/;
    numArray8[54] = (byte) 27;
    numArray8[19] = (byte) 150;
    numArray8[20] = (byte) 159;
    numArray8[13] = (byte) 55;
    numArray8[22] = (byte) 242;
    numArray8[23] = (byte) 78;
    numArray8[9] = (byte) 160 /*0xA0*/;
    numArray8[25] = (byte) 177;
    numArray8[35] = (byte) 250;
    numArray8[27] = (byte) 102;
    numArray8[6] = (byte) 245;
    numArray8[45] = (byte) 249;
    numArray8[30] = (byte) 232;
    numArray8[50] = (byte) 74;
    numArray8[46] = (byte) 165;
    numArray8[33] = (byte) 192 /*0xC0*/;
    numArray8[0] = (byte) 54;
    numArray8[3] = (byte) 196;
    numArray8[43] = (byte) 144 /*0x90*/;
    numArray8[26] = (byte) 119;
    numArray8[28] = (byte) 10;
    numArray8[39] = (byte) 122;
    numArray8[34] = (byte) 19;
    numArray8[41] = (byte) 157;
    numArray8[10] = (byte) 83;
    numArray8[32 /*0x20*/] = (byte) 9;
    numArray8[44] = (byte) 109;
    numArray8[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    numArray8[36] = (byte) 176 /*0xB0*/;
    numArray8[47] = (byte) 98;
    numArray8[48 /*0x30*/] = (byte) 21;
    numArray8[40] = (byte) 93;
    numArray8[21] = (byte) 27;
    numArray8[38] = (byte) 52;
    numArray8[52] = (byte) 189;
    numArray8[42] = (byte) 87;
    numArray8[51] = (byte) 33;
    byte[] numArray9 = new byte[55];
    numArray9[41] = (byte) 90;
    numArray9[1] = (byte) 92;
    numArray9[34] = (byte) 3;
    numArray9[21] = (byte) 209;
    numArray9[4] = (byte) 226;
    numArray9[5] = (byte) 150;
    numArray9[45] = (byte) 122;
    numArray9[29] = (byte) 58;
    numArray9[8] = (byte) 232;
    numArray9[12] = (byte) 23;
    numArray9[17] = (byte) 204;
    numArray9[11] = (byte) 44;
    numArray9[42] = (byte) 171;
    numArray9[13] = (byte) 181;
    numArray9[14] = (byte) 71;
    numArray9[15] = (byte) 69;
    numArray9[16 /*0x10*/] = (byte) 101;
    numArray9[0] = (byte) 152;
    numArray9[49] = (byte) 212;
    numArray9[19] = (byte) 247;
    numArray9[20] = (byte) 54;
    numArray9[37] = (byte) 93;
    numArray9[22] = (byte) 251;
    numArray9[23] = (byte) 120;
    numArray9[24] = (byte) 200;
    numArray9[28] = (byte) 217;
    numArray9[26] = (byte) 78;
    numArray9[51] = (byte) 62;
    numArray9[10] = (byte) 53;
    numArray9[48 /*0x30*/] = (byte) 188;
    numArray9[18] = (byte) 140;
    numArray9[52] = (byte) 41;
    numArray9[36] = (byte) 208 /*0xD0*/;
    numArray9[40] = (byte) 220;
    numArray9[25] = (byte) 198;
    numArray9[2] = (byte) 169;
    numArray9[31 /*0x1F*/] = (byte) 145;
    numArray9[46] = (byte) 226;
    numArray9[38] = (byte) 172;
    numArray9[39] = (byte) 123;
    numArray9[44] = (byte) 235;
    numArray9[9] = (byte) 54;
    numArray9[27] = (byte) 208 /*0xD0*/;
    numArray9[43] = (byte) 62;
    numArray9[3] = (byte) 140;
    numArray9[33] = (byte) 61;
    numArray9[7] = (byte) 101;
    numArray9[6] = (byte) 128 /*0x80*/;
    numArray9[47] = (byte) 79;
    numArray9[30] = (byte) 0;
    numArray9[50] = (byte) 187;
    numArray9[32 /*0x20*/] = (byte) 39;
    numArray9[35] = (byte) 101;
    numArray9[53] = (byte) 122;
    numArray9[54] = (byte) 2;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[3]
    {
      (byte) 15,
      (byte) 1,
      (byte) 195
    };
    byte[] numArray11 = new byte[3]
    {
      (byte) 69,
      (byte) 60,
      (byte) 23
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 3);
    for (int index = 0; index < 3; ++index)
      numArray7[index + 55] ^= numArray11[index];
    byte[] numArray12 = new byte[11];
    byte[] response1 = new byte[11];
    Array.Copy((Array) sc_13686.sspq, 1050, (Array) numArray12, 0, 11);
    key.Query(true, 335, numArray12, response1);
    Array.Copy((Array) sc_13686.sspr, 1050, (Array) numArray12, 0, 11);
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

  internal static int ssp_appserver_13758(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 165,
      (byte) 58,
      (byte) 142,
      (byte) 17,
      (byte) 193,
      (byte) 139,
      (byte) 44,
      (byte) 0,
      (byte) 222,
      (byte) 36,
      (byte) 85,
      (byte) 104,
      (byte) 18,
      (byte) 60,
      (byte) 214,
      (byte) 104,
      (byte) 195,
      (byte) 222,
      (byte) 26,
      (byte) 84,
      (byte) 111,
      (byte) 212,
      (byte) 172,
      (byte) 91,
      (byte) 132,
      (byte) 139,
      (byte) 247,
      (byte) 68,
      (byte) 127 /*0x7F*/,
      (byte) 165,
      (byte) 176 /*0xB0*/,
      (byte) 4,
      (byte) 3,
      (byte) 226,
      (byte) 28,
      (byte) 212,
      (byte) 114,
      (byte) 177,
      (byte) 136,
      (byte) 232,
      (byte) 86,
      (byte) 172,
      (byte) 50,
      (byte) 218,
      (byte) 57,
      (byte) 14,
      (byte) 118,
      (byte) 11
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 70,
      (byte) 130,
      (byte) 8,
      (byte) 6,
      (byte) 219,
      (byte) 137,
      (byte) 1,
      (byte) 73,
      (byte) 219,
      (byte) 2,
      (byte) 181,
      (byte) 174,
      (byte) 249,
      (byte) 198,
      (byte) 100,
      (byte) 123,
      (byte) 140,
      (byte) 238,
      (byte) 68,
      (byte) 222,
      (byte) 71,
      (byte) 29,
      (byte) 236,
      (byte) 241,
      (byte) 30,
      (byte) 121,
      (byte) 78,
      (byte) 201,
      (byte) 70,
      (byte) 194,
      (byte) 88,
      (byte) 133,
      (byte) 242,
      (byte) 136,
      (byte) 43,
      (byte) 138,
      (byte) 62,
      (byte) 142,
      (byte) 27,
      (byte) 83,
      (byte) 133,
      (byte) 55,
      (byte) 32 /*0x20*/,
      (byte) 203,
      (byte) 94,
      (byte) 176 /*0xB0*/,
      (byte) 178,
      (byte) 155
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13759(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[45] = (byte) 101;
    sourceArray1[39] = (byte) 36;
    sourceArray1[2] = (byte) 208 /*0xD0*/;
    sourceArray1[6] = (byte) 53;
    sourceArray1[41] = (byte) 84;
    sourceArray1[5] = (byte) 140;
    sourceArray1[24] = (byte) 210;
    sourceArray1[28] = (byte) 235;
    sourceArray1[9] = (byte) 129;
    sourceArray1[29] = (byte) 218;
    sourceArray1[18] = (byte) 17;
    sourceArray1[11] = (byte) 116;
    sourceArray1[4] = (byte) 51;
    sourceArray1[37] = (byte) 69;
    sourceArray1[23] = (byte) 162;
    sourceArray1[15] = (byte) 31 /*0x1F*/;
    sourceArray1[32 /*0x20*/] = (byte) 18;
    sourceArray1[17] = (byte) 245;
    sourceArray1[1] = (byte) 60;
    sourceArray1[25] = (byte) 33;
    sourceArray1[20] = (byte) 49;
    sourceArray1[21] = (byte) 253;
    sourceArray1[14] = (byte) 48 /*0x30*/;
    sourceArray1[27] = (byte) 21;
    sourceArray1[19] = (byte) 86;
    sourceArray1[10] = (byte) 57;
    sourceArray1[26] = (byte) 118;
    sourceArray1[16 /*0x10*/] = (byte) 1;
    sourceArray1[0] = (byte) 203;
    sourceArray1[13] = (byte) 101;
    sourceArray1[30] = (byte) 54;
    sourceArray1[7] = (byte) 148;
    sourceArray1[12] = (byte) 80 /*0x50*/;
    sourceArray1[33] = (byte) 66;
    sourceArray1[34] = (byte) 56;
    sourceArray1[31 /*0x1F*/] = (byte) 11;
    sourceArray1[36] = (byte) 76;
    sourceArray1[35] = (byte) 165;
    sourceArray1[3] = (byte) 142;
    sourceArray1[22] = (byte) 195;
    sourceArray1[40] = (byte) 82;
    sourceArray1[43] = (byte) 42;
    sourceArray1[42] = (byte) 111;
    sourceArray1[38] = (byte) 63 /*0x3F*/;
    sourceArray1[44] = (byte) 249;
    sourceArray1[8] = (byte) 231;
    sourceArray1[46] = (byte) 233;
    sourceArray1[47] = (byte) 169;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[2] = (byte) 223;
    sourceArray2[1] = (byte) 236;
    sourceArray2[8] = (byte) 8;
    sourceArray2[3] = (byte) 158;
    sourceArray2[4] = (byte) 129;
    sourceArray2[16 /*0x10*/] = (byte) 47;
    sourceArray2[34] = (byte) 58;
    sourceArray2[24] = (byte) 126;
    sourceArray2[21] = (byte) 46;
    sourceArray2[9] = (byte) 235;
    sourceArray2[11] = (byte) 180;
    sourceArray2[7] = (byte) 179;
    sourceArray2[19] = (byte) 205;
    sourceArray2[38] = (byte) 135;
    sourceArray2[14] = (byte) 53;
    sourceArray2[15] = (byte) 96 /*0x60*/;
    sourceArray2[42] = (byte) 68;
    sourceArray2[17] = (byte) 157;
    sourceArray2[18] = (byte) 81;
    sourceArray2[23] = (byte) 166;
    sourceArray2[20] = (byte) 57;
    sourceArray2[5] = (byte) 9;
    sourceArray2[22] = (byte) 123;
    sourceArray2[46] = (byte) 66;
    sourceArray2[30] = (byte) 193;
    sourceArray2[29] = (byte) 5;
    sourceArray2[0] = (byte) 163;
    sourceArray2[37] = (byte) 121;
    sourceArray2[31 /*0x1F*/] = (byte) 88;
    sourceArray2[28] = (byte) 1;
    sourceArray2[41] = (byte) 224 /*0xE0*/;
    sourceArray2[26] = (byte) 19;
    sourceArray2[43] = (byte) 164;
    sourceArray2[33] = (byte) 147;
    sourceArray2[44] = (byte) 239;
    sourceArray2[35] = (byte) 157;
    sourceArray2[36] = (byte) 50;
    sourceArray2[6] = (byte) 244;
    sourceArray2[10] = (byte) 74;
    sourceArray2[39] = (byte) 58;
    sourceArray2[40] = (byte) 135;
    sourceArray2[32 /*0x20*/] = (byte) 122;
    sourceArray2[12] = (byte) 85;
    sourceArray2[25] = (byte) 32 /*0x20*/;
    sourceArray2[27] = (byte) 168;
    sourceArray2[45] = (byte) 158;
    sourceArray2[13] = (byte) 60;
    sourceArray2[47] = (byte) 235;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13760(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[47] = (byte) 47;
    sourceArray1[1] = (byte) 224 /*0xE0*/;
    sourceArray1[0] = (byte) 195;
    sourceArray1[3] = (byte) 198;
    sourceArray1[4] = (byte) 118;
    sourceArray1[12] = (byte) 22;
    sourceArray1[6] = (byte) 108;
    sourceArray1[38] = (byte) 66;
    sourceArray1[20] = (byte) 124;
    sourceArray1[33] = (byte) 51;
    sourceArray1[10] = (byte) 99;
    sourceArray1[17] = (byte) 40;
    sourceArray1[30] = (byte) 19;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[14] = (byte) 112 /*0x70*/;
    sourceArray1[15] = (byte) 130;
    sourceArray1[24] = (byte) 215;
    sourceArray1[8] = (byte) 186;
    sourceArray1[18] = (byte) 168;
    sourceArray1[43] = (byte) 183;
    sourceArray1[25] = (byte) 38;
    sourceArray1[21] = (byte) 250;
    sourceArray1[22] = (byte) 60;
    sourceArray1[23] = (byte) 195;
    sourceArray1[32 /*0x20*/] = (byte) 249;
    sourceArray1[41] = (byte) 222;
    sourceArray1[26] = (byte) 191;
    sourceArray1[7] = (byte) 126;
    sourceArray1[28] = (byte) 96 /*0x60*/;
    sourceArray1[29] = (byte) 149;
    sourceArray1[39] = (byte) 187;
    sourceArray1[31 /*0x1F*/] = (byte) 28;
    sourceArray1[13] = (byte) 186;
    sourceArray1[5] = (byte) 33;
    sourceArray1[2] = (byte) 0;
    sourceArray1[19] = (byte) 231;
    sourceArray1[34] = (byte) 28;
    sourceArray1[37] = (byte) 102;
    sourceArray1[35] = (byte) 241;
    sourceArray1[9] = (byte) 92;
    sourceArray1[40] = (byte) 252;
    sourceArray1[11] = (byte) 123;
    sourceArray1[42] = (byte) 148;
    sourceArray1[46] = (byte) 44;
    sourceArray1[44] = (byte) 131;
    sourceArray1[45] = (byte) 72;
    sourceArray1[36] = (byte) 49;
    sourceArray1[16 /*0x10*/] = (byte) 18;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 122,
      (byte) 242,
      (byte) 214,
      (byte) 236,
      (byte) 111,
      (byte) 80 /*0x50*/,
      (byte) 113,
      (byte) 42,
      (byte) 127 /*0x7F*/,
      (byte) 112 /*0x70*/,
      (byte) 74,
      (byte) 59,
      (byte) 208 /*0xD0*/,
      (byte) 218,
      (byte) 4,
      (byte) 122,
      (byte) 25,
      (byte) 118,
      (byte) 68,
      (byte) 154,
      (byte) 116,
      (byte) 166,
      (byte) 119,
      (byte) 144 /*0x90*/,
      (byte) 87,
      (byte) 108,
      (byte) 74,
      (byte) 7,
      (byte) 88,
      (byte) 233,
      (byte) 73,
      (byte) 34,
      (byte) 145,
      (byte) 167,
      (byte) 147,
      (byte) 186,
      (byte) 63 /*0x3F*/,
      (byte) 101,
      (byte) 231,
      (byte) 45,
      (byte) 249,
      (byte) 32 /*0x20*/,
      (byte) 232,
      (byte) 211,
      (byte) 163,
      (byte) 60,
      (byte) 228,
      (byte) 15
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13761(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 73,
      (byte) 145,
      (byte) 151,
      (byte) 41,
      (byte) 110,
      (byte) 60,
      (byte) 2,
      (byte) 101,
      (byte) 57,
      (byte) 99,
      (byte) 29,
      (byte) 254,
      (byte) 96 /*0x60*/,
      (byte) 85,
      (byte) 209,
      (byte) 98,
      (byte) 56,
      (byte) 168,
      (byte) 88,
      (byte) 24,
      (byte) 216,
      (byte) 134,
      (byte) 74,
      (byte) 48 /*0x30*/,
      (byte) 93,
      (byte) 52,
      (byte) 242,
      (byte) 148,
      (byte) 74,
      (byte) 221,
      (byte) 245,
      (byte) 23,
      (byte) 2,
      (byte) 198,
      (byte) 177,
      byte.MaxValue,
      (byte) 97,
      (byte) 187,
      (byte) 13,
      (byte) 180,
      (byte) 43,
      (byte) 67,
      (byte) 35,
      (byte) 240 /*0xF0*/,
      (byte) 48 /*0x30*/,
      (byte) 245,
      (byte) 22,
      (byte) 157
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 100;
    sourceArray2[1] = (byte) 178;
    sourceArray2[2] = (byte) 207;
    sourceArray2[3] = (byte) 80 /*0x50*/;
    sourceArray2[4] = (byte) 53;
    sourceArray2[5] = (byte) 120;
    sourceArray2[15] = (byte) 148;
    sourceArray2[20] = (byte) 111;
    sourceArray2[8] = (byte) 189;
    sourceArray2[30] = (byte) 173;
    sourceArray2[21] = (byte) 71;
    sourceArray2[11] = (byte) 163;
    sourceArray2[37] = (byte) 168;
    sourceArray2[6] = (byte) 85;
    sourceArray2[17] = (byte) 59;
    sourceArray2[0] = (byte) 189;
    sourceArray2[18] = (byte) 43;
    sourceArray2[13] = (byte) 68;
    sourceArray2[27] = (byte) 173;
    sourceArray2[19] = (byte) 112 /*0x70*/;
    sourceArray2[23] = (byte) 162;
    sourceArray2[31 /*0x1F*/] = (byte) 52;
    sourceArray2[22] = (byte) 190;
    sourceArray2[24] = (byte) 208 /*0xD0*/;
    sourceArray2[45] = (byte) 127 /*0x7F*/;
    sourceArray2[25] = (byte) 253;
    sourceArray2[10] = (byte) 125;
    sourceArray2[16 /*0x10*/] = (byte) 221;
    sourceArray2[28] = (byte) 254;
    sourceArray2[29] = (byte) 94;
    sourceArray2[36] = (byte) 191;
    sourceArray2[26] = (byte) 20;
    sourceArray2[32 /*0x20*/] = (byte) 26;
    sourceArray2[33] = (byte) 50;
    sourceArray2[34] = (byte) 226;
    sourceArray2[35] = (byte) 1;
    sourceArray2[12] = (byte) 96 /*0x60*/;
    sourceArray2[38] = (byte) 175;
    sourceArray2[40] = (byte) 189;
    sourceArray2[39] = (byte) 157;
    sourceArray2[43] = (byte) 24;
    sourceArray2[9] = (byte) 184;
    sourceArray2[42] = (byte) 142;
    sourceArray2[7] = (byte) 234;
    sourceArray2[14] = (byte) 163;
    sourceArray2[44] = (byte) 55;
    sourceArray2[46] = (byte) 91;
    sourceArray2[47] = (byte) 188;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[51];
    byte[] response2 = new byte[51];
    Array.Copy((Array) sc_13686.sspq, 1061, (Array) numArray2, 0, 51);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 1061, (Array) numArray2, 0, 51);
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

  internal static int ssp_appserver_13762(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 68,
      (byte) 37,
      (byte) 218,
      (byte) 187,
      (byte) 67,
      (byte) 156,
      (byte) 57,
      (byte) 123,
      (byte) 100,
      (byte) 57,
      (byte) 6,
      (byte) 235,
      (byte) 196,
      (byte) 20,
      (byte) 143,
      (byte) 244,
      (byte) 122,
      (byte) 152,
      (byte) 8,
      (byte) 67,
      (byte) 184,
      (byte) 60,
      (byte) 117,
      (byte) 120,
      (byte) 66,
      (byte) 39,
      (byte) 75,
      (byte) 112 /*0x70*/,
      (byte) 3,
      (byte) 15,
      (byte) 194,
      (byte) 210,
      (byte) 85,
      (byte) 216,
      (byte) 147,
      (byte) 177,
      (byte) 149,
      (byte) 230,
      (byte) 24,
      (byte) 186,
      (byte) 147,
      (byte) 209,
      (byte) 158,
      (byte) 85,
      (byte) 159,
      (byte) 147,
      (byte) 201,
      (byte) 185
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 144 /*0x90*/;
    sourceArray2[1] = (byte) 158;
    sourceArray2[2] = (byte) 190;
    sourceArray2[30] = (byte) 160 /*0xA0*/;
    sourceArray2[18] = (byte) 14;
    sourceArray2[39] = (byte) 44;
    sourceArray2[22] = (byte) 23;
    sourceArray2[0] = (byte) 38;
    sourceArray2[8] = (byte) 81;
    sourceArray2[9] = (byte) 94;
    sourceArray2[10] = (byte) 233;
    sourceArray2[20] = (byte) 58;
    sourceArray2[12] = (byte) 171;
    sourceArray2[46] = (byte) 105;
    sourceArray2[14] = (byte) 69;
    sourceArray2[15] = (byte) 163;
    sourceArray2[16 /*0x10*/] = (byte) 174;
    sourceArray2[5] = (byte) 63 /*0x3F*/;
    sourceArray2[37] = (byte) 223;
    sourceArray2[19] = (byte) 183;
    sourceArray2[21] = (byte) 16 /*0x10*/;
    sourceArray2[38] = (byte) 106;
    sourceArray2[11] = (byte) 27;
    sourceArray2[23] = (byte) 207;
    sourceArray2[3] = (byte) 177;
    sourceArray2[40] = (byte) 52;
    sourceArray2[24] = (byte) 40;
    sourceArray2[42] = (byte) 171;
    sourceArray2[6] = (byte) 213;
    sourceArray2[29] = (byte) 116;
    sourceArray2[32 /*0x20*/] = (byte) 140;
    sourceArray2[31 /*0x1F*/] = (byte) 94;
    sourceArray2[13] = (byte) 130;
    sourceArray2[43] = (byte) 224 /*0xE0*/;
    sourceArray2[34] = (byte) 86;
    sourceArray2[35] = (byte) 60;
    sourceArray2[36] = (byte) 77;
    sourceArray2[41] = (byte) 244;
    sourceArray2[33] = (byte) 69;
    sourceArray2[17] = (byte) 20;
    sourceArray2[4] = (byte) 19;
    sourceArray2[28] = (byte) 224 /*0xE0*/;
    sourceArray2[27] = (byte) 88;
    sourceArray2[7] = (byte) 69;
    sourceArray2[44] = (byte) 203;
    sourceArray2[45] = (byte) 120;
    sourceArray2[25] = (byte) 82;
    sourceArray2[47] = (byte) 142;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[41];
    byte[] response2 = new byte[41];
    Array.Copy((Array) sc_13686.sspq, 1112, (Array) numArray2, 0, 41);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 1112, (Array) numArray2, 0, 41);
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

  internal static int ssp_appserver_13763(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 184;
    sourceArray1[10] = (byte) 76;
    sourceArray1[2] = (byte) 111;
    sourceArray1[24] = (byte) 29;
    sourceArray1[25] = (byte) 26;
    sourceArray1[3] = (byte) 169;
    sourceArray1[6] = (byte) 169;
    sourceArray1[36] = (byte) 109;
    sourceArray1[7] = (byte) 111;
    sourceArray1[9] = (byte) 197;
    sourceArray1[35] = (byte) 211;
    sourceArray1[11] = (byte) 181;
    sourceArray1[12] = (byte) 233;
    sourceArray1[13] = (byte) 192 /*0xC0*/;
    sourceArray1[14] = (byte) 67;
    sourceArray1[15] = (byte) 71;
    sourceArray1[43] = (byte) 144 /*0x90*/;
    sourceArray1[40] = (byte) 59;
    sourceArray1[17] = (byte) 130;
    sourceArray1[42] = (byte) 62;
    sourceArray1[44] = (byte) 243;
    sourceArray1[19] = (byte) 34;
    sourceArray1[29] = (byte) 213;
    sourceArray1[21] = (byte) 148;
    sourceArray1[30] = (byte) 90;
    sourceArray1[16 /*0x10*/] = (byte) 12;
    sourceArray1[26] = (byte) 62;
    sourceArray1[27] = (byte) 102;
    sourceArray1[31 /*0x1F*/] = (byte) 12;
    sourceArray1[28] = (byte) 21;
    sourceArray1[45] = (byte) 183;
    sourceArray1[23] = (byte) 80 /*0x50*/;
    sourceArray1[32 /*0x20*/] = (byte) 64 /*0x40*/;
    sourceArray1[39] = (byte) 67;
    sourceArray1[34] = (byte) 112 /*0x70*/;
    sourceArray1[8] = (byte) 79;
    sourceArray1[22] = (byte) 20;
    sourceArray1[37] = (byte) 129;
    sourceArray1[38] = (byte) 131;
    sourceArray1[5] = (byte) 62;
    sourceArray1[0] = (byte) 173;
    sourceArray1[41] = (byte) 117;
    sourceArray1[46] = (byte) 198;
    sourceArray1[18] = (byte) 220;
    sourceArray1[4] = (byte) 163;
    sourceArray1[20] = (byte) 64 /*0x40*/;
    sourceArray1[1] = (byte) 33;
    sourceArray1[47] = (byte) 191;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[46] = (byte) 8;
    sourceArray2[8] = (byte) 137;
    sourceArray2[35] = (byte) 181;
    sourceArray2[16 /*0x10*/] = (byte) 103;
    sourceArray2[4] = (byte) 182;
    sourceArray2[27] = (byte) 228;
    sourceArray2[6] = (byte) 207;
    sourceArray2[28] = (byte) 235;
    sourceArray2[26] = (byte) 1;
    sourceArray2[9] = (byte) 197;
    sourceArray2[10] = (byte) 116;
    sourceArray2[24] = (byte) 193;
    sourceArray2[12] = (byte) 217;
    sourceArray2[23] = (byte) 226;
    sourceArray2[14] = (byte) 29;
    sourceArray2[15] = (byte) 66;
    sourceArray2[40] = (byte) 241;
    sourceArray2[7] = (byte) 75;
    sourceArray2[18] = (byte) 234;
    sourceArray2[31 /*0x1F*/] = (byte) 47;
    sourceArray2[20] = (byte) 252;
    sourceArray2[21] = (byte) 101;
    sourceArray2[39] = (byte) 198;
    sourceArray2[47] = (byte) 212;
    sourceArray2[3] = (byte) 2;
    sourceArray2[25] = (byte) 33;
    sourceArray2[33] = (byte) 56;
    sourceArray2[11] = (byte) 73;
    sourceArray2[13] = (byte) 197;
    sourceArray2[29] = (byte) 42;
    sourceArray2[30] = (byte) 207;
    sourceArray2[17] = (byte) 82;
    sourceArray2[32 /*0x20*/] = (byte) 107;
    sourceArray2[1] = (byte) 151;
    sourceArray2[38] = (byte) 172;
    sourceArray2[19] = (byte) 90;
    sourceArray2[44] = (byte) 194;
    sourceArray2[37] = (byte) 158;
    sourceArray2[36] = (byte) 229;
    sourceArray2[22] = (byte) 12;
    sourceArray2[5] = (byte) 200;
    sourceArray2[41] = (byte) 7;
    sourceArray2[42] = (byte) 99;
    sourceArray2[43] = (byte) 43;
    sourceArray2[2] = (byte) 202;
    sourceArray2[45] = (byte) 133;
    sourceArray2[34] = (byte) 150;
    sourceArray2[0] = (byte) 16 /*0x10*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13764(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 112 /*0x70*/;
    sourceArray1[24] = (byte) 190;
    sourceArray1[2] = (byte) 216;
    sourceArray1[3] = (byte) 245;
    sourceArray1[4] = (byte) 112 /*0x70*/;
    sourceArray1[1] = (byte) 6;
    sourceArray1[43] = (byte) 176 /*0xB0*/;
    sourceArray1[7] = (byte) 1;
    sourceArray1[15] = (byte) 160 /*0xA0*/;
    sourceArray1[20] = (byte) 118;
    sourceArray1[10] = (byte) 153;
    sourceArray1[11] = (byte) 6;
    sourceArray1[46] = (byte) 112 /*0x70*/;
    sourceArray1[13] = (byte) 186;
    sourceArray1[12] = (byte) 159;
    sourceArray1[0] = (byte) 193;
    sourceArray1[16 /*0x10*/] = (byte) 48 /*0x30*/;
    sourceArray1[17] = (byte) 42;
    sourceArray1[23] = (byte) 81;
    sourceArray1[42] = (byte) 155;
    sourceArray1[35] = (byte) 232;
    sourceArray1[44] = (byte) 88;
    sourceArray1[29] = (byte) 101;
    sourceArray1[6] = (byte) 185;
    sourceArray1[5] = (byte) 215;
    sourceArray1[41] = (byte) 189;
    sourceArray1[22] = (byte) 70;
    sourceArray1[14] = (byte) 125;
    sourceArray1[27] = (byte) 9;
    sourceArray1[30] = (byte) 110;
    sourceArray1[47] = (byte) 152;
    sourceArray1[25] = (byte) 164;
    sourceArray1[32 /*0x20*/] = (byte) 200;
    sourceArray1[18] = (byte) 25;
    sourceArray1[34] = (byte) 148;
    sourceArray1[31 /*0x1F*/] = (byte) 254;
    sourceArray1[36] = (byte) 214;
    sourceArray1[37] = (byte) 156;
    sourceArray1[38] = (byte) 237;
    sourceArray1[28] = (byte) 69;
    sourceArray1[8] = (byte) 117;
    sourceArray1[33] = (byte) 66;
    sourceArray1[19] = (byte) 3;
    sourceArray1[40] = (byte) 105;
    sourceArray1[26] = (byte) 138;
    sourceArray1[45] = (byte) 8;
    sourceArray1[9] = (byte) 251;
    sourceArray1[39] = (byte) 209;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[25] = (byte) 144 /*0x90*/;
    sourceArray2[13] = (byte) 39;
    sourceArray2[0] = (byte) 47;
    sourceArray2[3] = (byte) 161;
    sourceArray2[17] = (byte) 105;
    sourceArray2[5] = (byte) 247;
    sourceArray2[6] = (byte) 161;
    sourceArray2[7] = (byte) 114;
    sourceArray2[8] = (byte) 149;
    sourceArray2[14] = (byte) 143;
    sourceArray2[34] = (byte) 226;
    sourceArray2[1] = (byte) 214;
    sourceArray2[43] = (byte) 217;
    sourceArray2[27] = (byte) 129;
    sourceArray2[47] = (byte) 51;
    sourceArray2[15] = (byte) 11;
    sourceArray2[16 /*0x10*/] = (byte) 195;
    sourceArray2[37] = (byte) 106;
    sourceArray2[4] = (byte) 103;
    sourceArray2[29] = (byte) 113;
    sourceArray2[20] = (byte) 190;
    sourceArray2[38] = (byte) 172;
    sourceArray2[22] = (byte) 77;
    sourceArray2[23] = (byte) 148;
    sourceArray2[19] = (byte) 151;
    sourceArray2[24] = (byte) 175;
    sourceArray2[9] = (byte) 76;
    sourceArray2[28] = (byte) 183;
    sourceArray2[31 /*0x1F*/] = (byte) 13;
    sourceArray2[42] = (byte) 214;
    sourceArray2[30] = (byte) 185;
    sourceArray2[21] = (byte) 222;
    sourceArray2[32 /*0x20*/] = (byte) 66;
    sourceArray2[33] = (byte) 43;
    sourceArray2[35] = (byte) 233;
    sourceArray2[10] = (byte) 207;
    sourceArray2[36] = (byte) 229;
    sourceArray2[44] = (byte) 115;
    sourceArray2[12] = (byte) 190;
    sourceArray2[39] = (byte) 152;
    sourceArray2[40] = (byte) 231;
    sourceArray2[41] = (byte) 40;
    sourceArray2[2] = (byte) 105;
    sourceArray2[26] = (byte) 74;
    sourceArray2[18] = (byte) 120;
    sourceArray2[45] = (byte) 209;
    sourceArray2[46] = (byte) 16 /*0x10*/;
    sourceArray2[11] = (byte) 80 /*0x50*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13765(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[43] = (byte) 155;
    sourceArray1[1] = (byte) 16 /*0x10*/;
    sourceArray1[2] = (byte) 222;
    sourceArray1[17] = (byte) 183;
    sourceArray1[9] = byte.MaxValue;
    sourceArray1[11] = (byte) 159;
    sourceArray1[0] = (byte) 39;
    sourceArray1[25] = (byte) 167;
    sourceArray1[24] = (byte) 219;
    sourceArray1[20] = (byte) 119;
    sourceArray1[10] = (byte) 106;
    sourceArray1[29] = (byte) 25;
    sourceArray1[42] = (byte) 236;
    sourceArray1[13] = (byte) 101;
    sourceArray1[14] = (byte) 80 /*0x50*/;
    sourceArray1[15] = (byte) 223;
    sourceArray1[31 /*0x1F*/] = (byte) 185;
    sourceArray1[3] = (byte) 124;
    sourceArray1[18] = (byte) 82;
    sourceArray1[19] = (byte) 150;
    sourceArray1[16 /*0x10*/] = (byte) 48 /*0x30*/;
    sourceArray1[21] = (byte) 221;
    sourceArray1[40] = (byte) 94;
    sourceArray1[23] = (byte) 112 /*0x70*/;
    sourceArray1[7] = (byte) 171;
    sourceArray1[26] = (byte) 172;
    sourceArray1[28] = (byte) 8;
    sourceArray1[27] = (byte) 113;
    sourceArray1[6] = (byte) 140;
    sourceArray1[32 /*0x20*/] = (byte) 74;
    sourceArray1[30] = (byte) 111;
    sourceArray1[37] = (byte) 206;
    sourceArray1[4] = (byte) 243;
    sourceArray1[33] = (byte) 144 /*0x90*/;
    sourceArray1[34] = (byte) 156;
    sourceArray1[39] = (byte) 141;
    sourceArray1[5] = (byte) 174;
    sourceArray1[35] = (byte) 207;
    sourceArray1[38] = (byte) 127 /*0x7F*/;
    sourceArray1[8] = (byte) 145;
    sourceArray1[12] = (byte) 185;
    sourceArray1[41] = (byte) 116;
    sourceArray1[22] = (byte) 13;
    sourceArray1[36] = (byte) 215;
    sourceArray1[44] = (byte) 72;
    sourceArray1[45] = (byte) 147;
    sourceArray1[46] = (byte) 207;
    sourceArray1[47] = (byte) 253;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 92,
      (byte) 6,
      (byte) 157,
      (byte) 82,
      (byte) 233,
      (byte) 142,
      (byte) 188,
      (byte) 145,
      (byte) 20,
      (byte) 160 /*0xA0*/,
      (byte) 120,
      (byte) 24,
      (byte) 41,
      (byte) 208 /*0xD0*/,
      (byte) 127 /*0x7F*/,
      (byte) 61,
      (byte) 162,
      (byte) 190,
      (byte) 237,
      (byte) 166,
      (byte) 157,
      (byte) 213,
      (byte) 154,
      (byte) 146,
      (byte) 57,
      (byte) 83,
      byte.MaxValue,
      (byte) 204,
      (byte) 64 /*0x40*/,
      (byte) 203,
      (byte) 112 /*0x70*/,
      (byte) 108,
      (byte) 8,
      (byte) 180,
      (byte) 181,
      (byte) 106,
      (byte) 170,
      (byte) 192 /*0xC0*/,
      (byte) 43,
      (byte) 25,
      (byte) 21,
      (byte) 46,
      (byte) 220,
      (byte) 52,
      (byte) 59,
      (byte) 97,
      (byte) 251,
      (byte) 230
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13766(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 165,
      (byte) 52,
      (byte) 251,
      (byte) 120,
      (byte) 53,
      (byte) 13,
      (byte) 168,
      (byte) 22,
      (byte) 15,
      (byte) 89,
      (byte) 144 /*0x90*/,
      (byte) 69,
      (byte) 89,
      (byte) 115,
      (byte) 81,
      (byte) 38,
      (byte) 26,
      (byte) 247,
      (byte) 101,
      (byte) 100,
      (byte) 90,
      (byte) 171,
      (byte) 117,
      (byte) 180,
      (byte) 158,
      (byte) 53,
      (byte) 5,
      (byte) 56,
      (byte) 244,
      (byte) 49,
      (byte) 124,
      (byte) 101,
      (byte) 144 /*0x90*/,
      (byte) 10,
      (byte) 50,
      (byte) 122,
      (byte) 248,
      (byte) 16 /*0x10*/,
      (byte) 76,
      (byte) 29,
      (byte) 163,
      (byte) 228,
      (byte) 110,
      (byte) 249,
      (byte) 60,
      (byte) 35,
      (byte) 211,
      (byte) 191
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 150,
      (byte) 87,
      (byte) 136,
      (byte) 147,
      (byte) 179,
      (byte) 174,
      (byte) 238,
      (byte) 127 /*0x7F*/,
      (byte) 76,
      (byte) 240 /*0xF0*/,
      (byte) 49,
      (byte) 241,
      (byte) 145,
      (byte) 55,
      (byte) 46,
      (byte) 79,
      (byte) 35,
      (byte) 163,
      (byte) 146,
      (byte) 212,
      (byte) 92,
      (byte) 4,
      (byte) 162,
      (byte) 74,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 177,
      (byte) 224 /*0xE0*/,
      (byte) 195,
      (byte) 5,
      (byte) 131,
      (byte) 28,
      (byte) 41,
      (byte) 1,
      (byte) 135,
      (byte) 97,
      (byte) 160 /*0xA0*/,
      (byte) 185,
      (byte) 209,
      (byte) 183,
      (byte) 111,
      (byte) 51,
      (byte) 103,
      (byte) 58,
      (byte) 143,
      (byte) 42,
      (byte) 13
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13767(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[42] = (byte) 51;
    sourceArray1[1] = (byte) 227;
    sourceArray1[28] = (byte) 69;
    sourceArray1[3] = (byte) 190;
    sourceArray1[17] = (byte) 46;
    sourceArray1[5] = (byte) 217;
    sourceArray1[43] = (byte) 194;
    sourceArray1[21] = (byte) 192 /*0xC0*/;
    sourceArray1[8] = (byte) 111;
    sourceArray1[45] = (byte) 28;
    sourceArray1[14] = (byte) 78;
    sourceArray1[44] = (byte) 144 /*0x90*/;
    sourceArray1[12] = (byte) 42;
    sourceArray1[13] = (byte) 181;
    sourceArray1[30] = (byte) 216;
    sourceArray1[32 /*0x20*/] = (byte) 0;
    sourceArray1[9] = (byte) 34;
    sourceArray1[16 /*0x10*/] = (byte) 214;
    sourceArray1[18] = (byte) 9;
    sourceArray1[2] = (byte) 250;
    sourceArray1[4] = (byte) 71;
    sourceArray1[41] = (byte) 21;
    sourceArray1[20] = (byte) 72;
    sourceArray1[23] = (byte) 61;
    sourceArray1[24] = (byte) 124;
    sourceArray1[25] = (byte) 119;
    sourceArray1[26] = (byte) 123;
    sourceArray1[27] = (byte) 98;
    sourceArray1[38] = (byte) 176 /*0xB0*/;
    sourceArray1[40] = (byte) 76;
    sourceArray1[15] = (byte) 65;
    sourceArray1[6] = byte.MaxValue;
    sourceArray1[46] = (byte) 17;
    sourceArray1[33] = (byte) 191;
    sourceArray1[34] = (byte) 92;
    sourceArray1[35] = (byte) 38;
    sourceArray1[36] = (byte) 56;
    sourceArray1[37] = (byte) 188;
    sourceArray1[19] = (byte) 171;
    sourceArray1[10] = (byte) 221;
    sourceArray1[39] = (byte) 55;
    sourceArray1[31 /*0x1F*/] = (byte) 122;
    sourceArray1[47] = (byte) 110;
    sourceArray1[11] = (byte) 220;
    sourceArray1[0] = (byte) 51;
    sourceArray1[22] = (byte) 117;
    sourceArray1[7] = (byte) 7;
    sourceArray1[29] = (byte) 254;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 62,
      (byte) 248,
      (byte) 83,
      (byte) 119,
      (byte) 249,
      (byte) 164,
      (byte) 202,
      (byte) 189,
      (byte) 130,
      (byte) 76,
      (byte) 11,
      (byte) 12,
      (byte) 191,
      (byte) 43,
      (byte) 7,
      (byte) 24,
      (byte) 87,
      (byte) 58,
      (byte) 160 /*0xA0*/,
      (byte) 111,
      (byte) 237,
      (byte) 232,
      (byte) 100,
      (byte) 139,
      (byte) 87,
      (byte) 28,
      (byte) 152,
      (byte) 183,
      (byte) 214,
      (byte) 183,
      (byte) 186,
      (byte) 226,
      (byte) 149,
      (byte) 97,
      (byte) 168,
      (byte) 25,
      (byte) 55,
      (byte) 40,
      (byte) 246,
      (byte) 211,
      (byte) 236,
      (byte) 163,
      (byte) 81,
      (byte) 8,
      (byte) 41,
      (byte) 50,
      (byte) 77,
      (byte) 122
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13768(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[6] = (byte) 220;
    sourceArray1[1] = (byte) 22;
    sourceArray1[2] = (byte) 92;
    sourceArray1[3] = (byte) 117;
    sourceArray1[28] = (byte) 174;
    sourceArray1[23] = (byte) 111;
    sourceArray1[39] = (byte) 122;
    sourceArray1[40] = (byte) 240 /*0xF0*/;
    sourceArray1[33] = (byte) 31 /*0x1F*/;
    sourceArray1[9] = (byte) 230;
    sourceArray1[10] = (byte) 186;
    sourceArray1[5] = (byte) 111;
    sourceArray1[24] = (byte) 141;
    sourceArray1[13] = (byte) 105;
    sourceArray1[14] = (byte) 81;
    sourceArray1[15] = (byte) 191;
    sourceArray1[19] = (byte) 150;
    sourceArray1[12] = (byte) 226;
    sourceArray1[18] = (byte) 95;
    sourceArray1[16 /*0x10*/] = (byte) 252;
    sourceArray1[17] = (byte) 214;
    sourceArray1[21] = (byte) 10;
    sourceArray1[22] = (byte) 110;
    sourceArray1[46] = (byte) 151;
    sourceArray1[42] = (byte) 53;
    sourceArray1[25] = (byte) 160 /*0xA0*/;
    sourceArray1[26] = (byte) 157;
    sourceArray1[27] = (byte) 248;
    sourceArray1[7] = (byte) 115;
    sourceArray1[29] = (byte) 224 /*0xE0*/;
    sourceArray1[32 /*0x20*/] = (byte) 127 /*0x7F*/;
    sourceArray1[31 /*0x1F*/] = (byte) 59;
    sourceArray1[4] = (byte) 72;
    sourceArray1[20] = (byte) 173;
    sourceArray1[41] = (byte) 187;
    sourceArray1[44] = (byte) 253;
    sourceArray1[30] = (byte) 202;
    sourceArray1[43] = (byte) 3;
    sourceArray1[38] = (byte) 82;
    sourceArray1[0] = (byte) 165;
    sourceArray1[34] = (byte) 192 /*0xC0*/;
    sourceArray1[11] = (byte) 15;
    sourceArray1[36] = (byte) 129;
    sourceArray1[35] = (byte) 246;
    sourceArray1[8] = (byte) 43;
    sourceArray1[45] = (byte) 107;
    sourceArray1[47] = (byte) 119;
    sourceArray1[37] = (byte) 80 /*0x50*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 156,
      (byte) 31 /*0x1F*/,
      (byte) 76,
      (byte) 229,
      (byte) 248,
      (byte) 157,
      (byte) 200,
      (byte) 29,
      (byte) 15,
      (byte) 232,
      (byte) 252,
      (byte) 196,
      (byte) 192 /*0xC0*/,
      (byte) 76,
      (byte) 253,
      (byte) 61,
      (byte) 125,
      (byte) 65,
      (byte) 39,
      (byte) 212,
      (byte) 55,
      (byte) 17,
      (byte) 54,
      (byte) 141,
      (byte) 13,
      (byte) 231,
      (byte) 45,
      (byte) 5,
      (byte) 11,
      (byte) 30,
      (byte) 54,
      (byte) 123,
      (byte) 53,
      (byte) 5,
      (byte) 50,
      (byte) 168,
      (byte) 97,
      (byte) 97,
      (byte) 130,
      (byte) 146,
      (byte) 38,
      (byte) 199,
      (byte) 249,
      (byte) 170,
      (byte) 42,
      (byte) 4,
      (byte) 177,
      (byte) 167
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13769(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 212,
      (byte) 206,
      (byte) 44,
      (byte) 178,
      (byte) 170,
      (byte) 104,
      (byte) 52,
      (byte) 239,
      (byte) 162,
      (byte) 197,
      (byte) 61,
      (byte) 122,
      (byte) 197,
      (byte) 158,
      (byte) 172,
      (byte) 114,
      (byte) 7,
      (byte) 192 /*0xC0*/,
      (byte) 61,
      (byte) 93,
      (byte) 166,
      (byte) 55,
      (byte) 131,
      (byte) 13,
      (byte) 169,
      (byte) 67,
      (byte) 217,
      (byte) 60,
      (byte) 153,
      (byte) 250,
      (byte) 235,
      (byte) 130,
      (byte) 244,
      (byte) 71,
      (byte) 52,
      (byte) 87,
      (byte) 68,
      (byte) 10,
      (byte) 223,
      (byte) 117,
      (byte) 237,
      (byte) 249,
      (byte) 232,
      (byte) 96 /*0x60*/,
      (byte) 16 /*0x10*/,
      (byte) 25,
      (byte) 163,
      (byte) 12
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 131,
      (byte) 32 /*0x20*/,
      (byte) 212,
      (byte) 91,
      (byte) 151,
      (byte) 243,
      (byte) 37,
      (byte) 19,
      (byte) 216,
      (byte) 62,
      (byte) 90,
      (byte) 236,
      (byte) 126,
      (byte) 74,
      (byte) 190,
      (byte) 241,
      (byte) 184,
      (byte) 70,
      (byte) 202,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 116,
      (byte) 89,
      (byte) 10,
      (byte) 67,
      (byte) 112 /*0x70*/,
      (byte) 243,
      (byte) 249,
      (byte) 46,
      (byte) 66,
      (byte) 2,
      (byte) 92,
      (byte) 125,
      (byte) 167,
      (byte) 101,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 31 /*0x1F*/,
      (byte) 45,
      (byte) 128 /*0x80*/,
      (byte) 207,
      (byte) 79,
      (byte) 170,
      (byte) 139,
      (byte) 144 /*0x90*/,
      (byte) 211,
      (byte) 64 /*0x40*/,
      (byte) 173
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[36];
    byte[] response2 = new byte[36];
    Array.Copy((Array) sc_13686.sspq, 1153, (Array) numArray2, 0, 36);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13686.sspr, 1153, (Array) numArray2, 0, 36);
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

  internal static int ssp_appserver_13770(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 12,
      (byte) 37,
      (byte) 1,
      (byte) 120,
      (byte) 230,
      (byte) 246,
      (byte) 164,
      (byte) 64 /*0x40*/,
      (byte) 134,
      (byte) 86,
      (byte) 147,
      (byte) 37,
      (byte) 161,
      (byte) 165,
      (byte) 108,
      (byte) 243,
      (byte) 72,
      (byte) 227,
      (byte) 176 /*0xB0*/,
      (byte) 135,
      (byte) 77,
      (byte) 40,
      (byte) 34,
      (byte) 121,
      (byte) 198,
      (byte) 194,
      (byte) 181,
      (byte) 85,
      (byte) 158,
      (byte) 24,
      (byte) 227,
      (byte) 178,
      (byte) 106,
      (byte) 210,
      (byte) 72,
      (byte) 7,
      (byte) 250,
      (byte) 104,
      (byte) 224 /*0xE0*/,
      (byte) 121,
      (byte) 8,
      (byte) 14,
      (byte) 155,
      (byte) 166,
      (byte) 252,
      (byte) 123,
      (byte) 43,
      (byte) 155
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 244;
    sourceArray2[1] = (byte) 4;
    sourceArray2[2] = (byte) 212;
    sourceArray2[3] = (byte) 188;
    sourceArray2[35] = (byte) 214;
    sourceArray2[5] = (byte) 105;
    sourceArray2[6] = (byte) 35;
    sourceArray2[7] = (byte) 2;
    sourceArray2[8] = (byte) 128 /*0x80*/;
    sourceArray2[32 /*0x20*/] = (byte) 237;
    sourceArray2[4] = (byte) 136;
    sourceArray2[41] = (byte) 125;
    sourceArray2[17] = (byte) 112 /*0x70*/;
    sourceArray2[12] = (byte) 93;
    sourceArray2[42] = (byte) 184;
    sourceArray2[15] = (byte) 160 /*0xA0*/;
    sourceArray2[0] = (byte) 2;
    sourceArray2[21] = (byte) 35;
    sourceArray2[16 /*0x10*/] = (byte) 27;
    sourceArray2[23] = (byte) 7;
    sourceArray2[24] = (byte) 233;
    sourceArray2[13] = (byte) 78;
    sourceArray2[22] = (byte) 117;
    sourceArray2[27] = (byte) 131;
    sourceArray2[25] = (byte) 55;
    sourceArray2[33] = (byte) 57;
    sourceArray2[26] = (byte) 239;
    sourceArray2[14] = (byte) 95;
    sourceArray2[28] = (byte) 186;
    sourceArray2[18] = (byte) 1;
    sourceArray2[43] = (byte) 145;
    sourceArray2[31 /*0x1F*/] = (byte) 107;
    sourceArray2[11] = (byte) 92;
    sourceArray2[34] = (byte) 129;
    sourceArray2[10] = (byte) 113;
    sourceArray2[36] = (byte) 192 /*0xC0*/;
    sourceArray2[29] = (byte) 116;
    sourceArray2[37] = (byte) 204;
    sourceArray2[38] = (byte) 18;
    sourceArray2[20] = (byte) 118;
    sourceArray2[30] = (byte) 30;
    sourceArray2[39] = (byte) 10;
    sourceArray2[40] = (byte) 81;
    sourceArray2[19] = (byte) 147;
    sourceArray2[44] = (byte) 41;
    sourceArray2[45] = (byte) 160 /*0xA0*/;
    sourceArray2[46] = (byte) 46;
    sourceArray2[47] = (byte) 252;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13771(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 91,
      (byte) 62,
      (byte) 46,
      (byte) 30,
      (byte) 153,
      (byte) 242,
      (byte) 235,
      (byte) 13,
      (byte) 10,
      (byte) 86,
      (byte) 134,
      (byte) 12,
      (byte) 102,
      (byte) 141,
      (byte) 38,
      (byte) 139,
      (byte) 26,
      (byte) 208 /*0xD0*/,
      (byte) 132,
      (byte) 82,
      (byte) 219,
      (byte) 70,
      (byte) 248,
      (byte) 96 /*0x60*/,
      (byte) 134,
      (byte) 40,
      (byte) 70,
      byte.MaxValue,
      (byte) 195,
      (byte) 211,
      (byte) 95,
      (byte) 73,
      (byte) 97,
      (byte) 138,
      (byte) 58,
      (byte) 219,
      (byte) 147,
      (byte) 105,
      (byte) 5,
      (byte) 252,
      (byte) 162,
      (byte) 209,
      (byte) 126,
      (byte) 13,
      (byte) 125,
      (byte) 101,
      (byte) 80 /*0x50*/,
      (byte) 46
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 251;
    sourceArray2[42] = (byte) 197;
    sourceArray2[2] = (byte) 195;
    sourceArray2[3] = (byte) 240 /*0xF0*/;
    sourceArray2[39] = (byte) 55;
    sourceArray2[0] = (byte) 184;
    sourceArray2[6] = (byte) 175;
    sourceArray2[7] = (byte) 85;
    sourceArray2[4] = (byte) 12;
    sourceArray2[37] = (byte) 103;
    sourceArray2[16 /*0x10*/] = (byte) 184;
    sourceArray2[11] = (byte) 238;
    sourceArray2[5] = (byte) 106;
    sourceArray2[13] = (byte) 227;
    sourceArray2[32 /*0x20*/] = (byte) 137;
    sourceArray2[15] = (byte) 225;
    sourceArray2[22] = (byte) 178;
    sourceArray2[17] = (byte) 6;
    sourceArray2[18] = (byte) 29;
    sourceArray2[19] = (byte) 17;
    sourceArray2[31 /*0x1F*/] = (byte) 209;
    sourceArray2[21] = (byte) 87;
    sourceArray2[34] = (byte) 154;
    sourceArray2[23] = (byte) 225;
    sourceArray2[24] = (byte) 148;
    sourceArray2[12] = (byte) 44;
    sourceArray2[45] = (byte) 28;
    sourceArray2[27] = (byte) 37;
    sourceArray2[28] = (byte) 53;
    sourceArray2[29] = (byte) 198;
    sourceArray2[30] = (byte) 173;
    sourceArray2[35] = (byte) 241;
    sourceArray2[26] = (byte) 172;
    sourceArray2[33] = (byte) 14;
    sourceArray2[47] = (byte) 57;
    sourceArray2[10] = (byte) 172;
    sourceArray2[36] = (byte) 63 /*0x3F*/;
    sourceArray2[20] = (byte) 108;
    sourceArray2[9] = (byte) 160 /*0xA0*/;
    sourceArray2[41] = (byte) 149;
    sourceArray2[38] = (byte) 155;
    sourceArray2[40] = (byte) 209;
    sourceArray2[1] = (byte) 75;
    sourceArray2[43] = (byte) 226;
    sourceArray2[8] = (byte) 217;
    sourceArray2[25] = (byte) 70;
    sourceArray2[46] = (byte) 66;
    sourceArray2[14] = (byte) 132;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13772(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 93;
    sourceArray1[1] = (byte) 219;
    sourceArray1[2] = (byte) 197;
    sourceArray1[3] = (byte) 54;
    sourceArray1[23] = (byte) 181;
    sourceArray1[5] = (byte) 146;
    sourceArray1[32 /*0x20*/] = (byte) 161;
    sourceArray1[39] = (byte) 112 /*0x70*/;
    sourceArray1[40] = (byte) 116;
    sourceArray1[9] = (byte) 76;
    sourceArray1[10] = (byte) 90;
    sourceArray1[15] = (byte) 254;
    sourceArray1[12] = (byte) 117;
    sourceArray1[13] = (byte) 152;
    sourceArray1[16 /*0x10*/] = (byte) 180;
    sourceArray1[42] = (byte) 201;
    sourceArray1[45] = (byte) 216;
    sourceArray1[24] = (byte) 50;
    sourceArray1[18] = (byte) 244;
    sourceArray1[19] = (byte) 162;
    sourceArray1[0] = (byte) 116;
    sourceArray1[7] = (byte) 25;
    sourceArray1[22] = (byte) 45;
    sourceArray1[47] = (byte) 252;
    sourceArray1[34] = (byte) 99;
    sourceArray1[14] = (byte) 148;
    sourceArray1[26] = (byte) 54;
    sourceArray1[36] = (byte) 40;
    sourceArray1[46] = (byte) 92;
    sourceArray1[29] = (byte) 130;
    sourceArray1[30] = (byte) 129;
    sourceArray1[31 /*0x1F*/] = (byte) 212;
    sourceArray1[33] = (byte) 102;
    sourceArray1[11] = (byte) 236;
    sourceArray1[44] = (byte) 58;
    sourceArray1[35] = (byte) 59;
    sourceArray1[4] = (byte) 159;
    sourceArray1[25] = (byte) 223;
    sourceArray1[38] = (byte) 226;
    sourceArray1[6] = (byte) 38;
    sourceArray1[27] = (byte) 55;
    sourceArray1[41] = (byte) 126;
    sourceArray1[28] = (byte) 249;
    sourceArray1[43] = (byte) 217;
    sourceArray1[8] = (byte) 219;
    sourceArray1[21] = (byte) 54;
    sourceArray1[20] = (byte) 18;
    sourceArray1[17] = (byte) 181;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 30;
    sourceArray2[1] = (byte) 172;
    sourceArray2[3] = (byte) 85;
    sourceArray2[41] = (byte) 150;
    sourceArray2[17] = (byte) 253;
    sourceArray2[18] = (byte) 27;
    sourceArray2[8] = (byte) 228;
    sourceArray2[7] = (byte) 36;
    sourceArray2[2] = (byte) 23;
    sourceArray2[9] = (byte) 97;
    sourceArray2[10] = (byte) 225;
    sourceArray2[11] = (byte) 19;
    sourceArray2[12] = (byte) 12;
    sourceArray2[5] = (byte) 115;
    sourceArray2[13] = (byte) 213;
    sourceArray2[15] = (byte) 157;
    sourceArray2[16 /*0x10*/] = (byte) 88;
    sourceArray2[32 /*0x20*/] = (byte) 80 /*0x50*/;
    sourceArray2[6] = (byte) 101;
    sourceArray2[19] = (byte) 27;
    sourceArray2[30] = (byte) 204;
    sourceArray2[21] = (byte) 218;
    sourceArray2[31 /*0x1F*/] = (byte) 171;
    sourceArray2[23] = (byte) 158;
    sourceArray2[29] = (byte) 178;
    sourceArray2[25] = (byte) 242;
    sourceArray2[20] = (byte) 39;
    sourceArray2[27] = (byte) 197;
    sourceArray2[28] = (byte) 221;
    sourceArray2[0] = (byte) 95;
    sourceArray2[4] = (byte) 197;
    sourceArray2[38] = (byte) 15;
    sourceArray2[46] = (byte) 188;
    sourceArray2[33] = (byte) 47;
    sourceArray2[34] = (byte) 254;
    sourceArray2[14] = (byte) 51;
    sourceArray2[36] = (byte) 74;
    sourceArray2[37] = (byte) 162;
    sourceArray2[39] = (byte) 4;
    sourceArray2[22] = (byte) 210;
    sourceArray2[24] = (byte) 163;
    sourceArray2[40] = (byte) 121;
    sourceArray2[42] = (byte) 21;
    sourceArray2[43] = (byte) 107;
    sourceArray2[35] = (byte) 204;
    sourceArray2[45] = (byte) 94;
    sourceArray2[26] = (byte) 44;
    sourceArray2[47] = (byte) 135;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13773(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 243,
      (byte) 15,
      (byte) 128 /*0x80*/,
      (byte) 197,
      (byte) 51,
      (byte) 224 /*0xE0*/,
      (byte) 71,
      (byte) 252,
      (byte) 184,
      (byte) 153,
      (byte) 34,
      (byte) 127 /*0x7F*/,
      (byte) 12,
      (byte) 48 /*0x30*/,
      (byte) 198,
      (byte) 223,
      (byte) 210,
      (byte) 20,
      (byte) 206,
      (byte) 222,
      (byte) 6,
      (byte) 174,
      (byte) 95,
      (byte) 125,
      (byte) 119,
      (byte) 168,
      (byte) 193,
      (byte) 179,
      (byte) 166,
      (byte) 184,
      (byte) 151,
      (byte) 59,
      (byte) 76,
      (byte) 205,
      (byte) 122,
      (byte) 154,
      (byte) 180,
      (byte) 56,
      (byte) 18,
      (byte) 229,
      (byte) 132,
      (byte) 58,
      (byte) 186,
      (byte) 124,
      (byte) 62,
      (byte) 179,
      (byte) 37,
      (byte) 226
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 15,
      (byte) 166,
      (byte) 150,
      (byte) 4,
      (byte) 191,
      (byte) 34,
      (byte) 199,
      (byte) 110,
      (byte) 195,
      (byte) 196,
      (byte) 25,
      (byte) 164,
      (byte) 139,
      (byte) 174,
      (byte) 189,
      (byte) 19,
      (byte) 109,
      (byte) 112 /*0x70*/,
      (byte) 225,
      (byte) 111,
      (byte) 213,
      (byte) 197,
      (byte) 204,
      (byte) 100,
      (byte) 138,
      (byte) 248,
      (byte) 114,
      (byte) 65,
      (byte) 31 /*0x1F*/,
      (byte) 138,
      (byte) 44,
      (byte) 161,
      (byte) 48 /*0x30*/,
      (byte) 229,
      (byte) 91,
      (byte) 246,
      (byte) 0,
      (byte) 244,
      (byte) 204,
      (byte) 6,
      (byte) 196,
      (byte) 159,
      (byte) 59,
      (byte) 37,
      (byte) 56,
      (byte) 218,
      (byte) 4,
      (byte) 93
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13774(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 221,
      (byte) 69,
      (byte) 175,
      (byte) 3,
      (byte) 78,
      (byte) 138,
      (byte) 31 /*0x1F*/,
      (byte) 148,
      (byte) 85,
      byte.MaxValue,
      (byte) 2,
      (byte) 231,
      (byte) 50,
      (byte) 63 /*0x3F*/,
      (byte) 181,
      (byte) 44,
      (byte) 42,
      (byte) 154,
      (byte) 1,
      (byte) 233,
      (byte) 89,
      (byte) 204,
      (byte) 243,
      (byte) 227,
      (byte) 62,
      (byte) 195,
      (byte) 130,
      (byte) 222,
      (byte) 230,
      (byte) 218,
      (byte) 53,
      (byte) 120,
      (byte) 135,
      (byte) 122,
      (byte) 239,
      (byte) 53,
      (byte) 234,
      (byte) 177,
      (byte) 233,
      (byte) 135,
      (byte) 198,
      (byte) 239,
      (byte) 228,
      (byte) 186,
      (byte) 121,
      (byte) 87,
      (byte) 129
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[15] = (byte) 219;
    sourceArray2[1] = (byte) 118;
    sourceArray2[41] = (byte) 36;
    sourceArray2[3] = (byte) 24;
    sourceArray2[42] = (byte) 144 /*0x90*/;
    sourceArray2[5] = (byte) 102;
    sourceArray2[6] = (byte) 160 /*0xA0*/;
    sourceArray2[9] = (byte) 64 /*0x40*/;
    sourceArray2[17] = (byte) 49;
    sourceArray2[20] = (byte) 134;
    sourceArray2[33] = (byte) 136;
    sourceArray2[8] = (byte) 59;
    sourceArray2[46] = (byte) 96 /*0x60*/;
    sourceArray2[21] = (byte) 94;
    sourceArray2[14] = (byte) 175;
    sourceArray2[45] = (byte) 254;
    sourceArray2[10] = (byte) 207;
    sourceArray2[47] = (byte) 138;
    sourceArray2[37] = (byte) 23;
    sourceArray2[28] = (byte) 153;
    sourceArray2[2] = (byte) 36;
    sourceArray2[7] = (byte) 35;
    sourceArray2[22] = (byte) 175;
    sourceArray2[23] = (byte) 134;
    sourceArray2[13] = (byte) 185;
    sourceArray2[25] = (byte) 65;
    sourceArray2[19] = (byte) 26;
    sourceArray2[27] = (byte) 244;
    sourceArray2[18] = (byte) 88;
    sourceArray2[29] = (byte) 89;
    sourceArray2[30] = (byte) 214;
    sourceArray2[35] = (byte) 20;
    sourceArray2[31 /*0x1F*/] = (byte) 8;
    sourceArray2[4] = (byte) 46;
    sourceArray2[34] = (byte) 169;
    sourceArray2[24] = (byte) 49;
    sourceArray2[36] = (byte) 243;
    sourceArray2[12] = (byte) 38;
    sourceArray2[38] = (byte) 53;
    sourceArray2[39] = (byte) 44;
    sourceArray2[40] = (byte) 49;
    sourceArray2[32 /*0x20*/] = (byte) 111;
    sourceArray2[0] = (byte) 4;
    sourceArray2[43] = (byte) 163;
    sourceArray2[44] = (byte) 21;
    sourceArray2[11] = (byte) 62;
    sourceArray2[26] = (byte) 42;
    sourceArray2[16 /*0x10*/] = (byte) 78;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13775(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 247,
      (byte) 201,
      (byte) 151,
      (byte) 172,
      (byte) 95,
      (byte) 39,
      (byte) 56,
      (byte) 163,
      (byte) 168,
      (byte) 14,
      (byte) 39,
      (byte) 24,
      (byte) 99,
      (byte) 221,
      (byte) 73,
      (byte) 107,
      (byte) 143,
      (byte) 105,
      (byte) 64 /*0x40*/,
      (byte) 125,
      (byte) 45,
      (byte) 164,
      (byte) 182,
      (byte) 112 /*0x70*/,
      (byte) 157,
      (byte) 36,
      (byte) 140,
      (byte) 82,
      (byte) 250,
      (byte) 156,
      (byte) 40,
      (byte) 127 /*0x7F*/,
      (byte) 72,
      (byte) 120,
      (byte) 240 /*0xF0*/,
      (byte) 120,
      (byte) 5,
      (byte) 5,
      (byte) 230,
      (byte) 19,
      (byte) 20,
      (byte) 184,
      (byte) 19,
      (byte) 243,
      (byte) 183,
      (byte) 202,
      (byte) 186,
      (byte) 15
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 172,
      (byte) 87,
      (byte) 152,
      (byte) 123,
      (byte) 36,
      (byte) 60,
      (byte) 12,
      (byte) 120,
      (byte) 202,
      (byte) 42,
      (byte) 25,
      (byte) 114,
      (byte) 164,
      (byte) 235,
      (byte) 25,
      (byte) 175,
      (byte) 2,
      (byte) 14,
      (byte) 141,
      (byte) 246,
      (byte) 250,
      (byte) 66,
      (byte) 36,
      (byte) 221,
      (byte) 143,
      (byte) 12,
      (byte) 59,
      (byte) 16 /*0x10*/,
      (byte) 184,
      (byte) 88,
      (byte) 145,
      (byte) 24,
      (byte) 77,
      (byte) 247,
      (byte) 60,
      (byte) 30,
      (byte) 29,
      (byte) 196,
      (byte) 248,
      (byte) 14,
      (byte) 180,
      (byte) 104,
      (byte) 54,
      (byte) 162,
      (byte) 44,
      (byte) 44,
      (byte) 206,
      (byte) 9
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13776(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 124;
    sourceArray1[1] = (byte) 194;
    sourceArray1[26] = (byte) 210;
    sourceArray1[29] = (byte) 61;
    sourceArray1[0] = (byte) 114;
    sourceArray1[5] = (byte) 14;
    sourceArray1[6] = (byte) 59;
    sourceArray1[30] = (byte) 231;
    sourceArray1[33] = (byte) 87;
    sourceArray1[9] = (byte) 168;
    sourceArray1[10] = (byte) 11;
    sourceArray1[35] = (byte) 182;
    sourceArray1[12] = (byte) 168;
    sourceArray1[13] = (byte) 207;
    sourceArray1[41] = (byte) 120;
    sourceArray1[15] = (byte) 236;
    sourceArray1[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    sourceArray1[19] = (byte) 131;
    sourceArray1[21] = (byte) 105;
    sourceArray1[44] = (byte) 7;
    sourceArray1[47] = (byte) 251;
    sourceArray1[8] = (byte) 229;
    sourceArray1[18] = (byte) 138;
    sourceArray1[23] = (byte) 144 /*0x90*/;
    sourceArray1[7] = (byte) 154;
    sourceArray1[24] = (byte) 194;
    sourceArray1[20] = (byte) 45;
    sourceArray1[17] = (byte) 102;
    sourceArray1[14] = (byte) 234;
    sourceArray1[27] = (byte) 105;
    sourceArray1[11] = (byte) 229;
    sourceArray1[31 /*0x1F*/] = (byte) 166;
    sourceArray1[32 /*0x20*/] = (byte) 80 /*0x50*/;
    sourceArray1[40] = (byte) 170;
    sourceArray1[34] = (byte) 214;
    sourceArray1[45] = (byte) 14;
    sourceArray1[36] = (byte) 157;
    sourceArray1[37] = (byte) 101;
    sourceArray1[38] = (byte) 222;
    sourceArray1[39] = (byte) 246;
    sourceArray1[3] = (byte) 19;
    sourceArray1[2] = (byte) 82;
    sourceArray1[42] = (byte) 46;
    sourceArray1[43] = (byte) 16 /*0x10*/;
    sourceArray1[4] = (byte) 38;
    sourceArray1[22] = (byte) 116;
    sourceArray1[46] = (byte) 15;
    sourceArray1[25] = (byte) 184;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 206,
      (byte) 158,
      (byte) 145,
      (byte) 183,
      (byte) 103,
      (byte) 181,
      (byte) 230,
      (byte) 35,
      (byte) 16 /*0x10*/,
      (byte) 186,
      (byte) 4,
      (byte) 247,
      (byte) 99,
      (byte) 222,
      (byte) 149,
      (byte) 242,
      (byte) 168,
      (byte) 84,
      (byte) 236,
      (byte) 244,
      (byte) 83,
      (byte) 249,
      (byte) 244,
      (byte) 189,
      (byte) 162,
      (byte) 71,
      (byte) 124,
      (byte) 236,
      (byte) 113,
      (byte) 251,
      (byte) 235,
      (byte) 25,
      (byte) 251,
      byte.MaxValue,
      (byte) 197,
      (byte) 31 /*0x1F*/,
      (byte) 0,
      (byte) 164,
      (byte) 53,
      (byte) 27,
      (byte) 158,
      (byte) 0,
      (byte) 37,
      (byte) 245,
      (byte) 125,
      byte.MaxValue,
      (byte) 6,
      (byte) 109
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
