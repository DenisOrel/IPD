// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12586
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12586
{
  private static byte[] sspq = new byte[912]
  {
    (byte) 27,
    (byte) 97,
    (byte) 36,
    (byte) 78,
    (byte) 197,
    (byte) 108,
    (byte) 166,
    (byte) 85,
    (byte) 157,
    (byte) 177,
    (byte) 141,
    (byte) 244,
    (byte) 132,
    (byte) 164,
    (byte) 57,
    (byte) 85,
    (byte) 49,
    (byte) 111,
    (byte) 106,
    (byte) 65,
    (byte) 178,
    (byte) 118,
    (byte) 163,
    (byte) 160 /*0xA0*/,
    (byte) 141,
    (byte) 19,
    (byte) 110,
    (byte) 64 /*0x40*/,
    (byte) 122,
    (byte) 226,
    (byte) 139,
    (byte) 196,
    (byte) 83,
    (byte) 204,
    (byte) 243,
    (byte) 92,
    (byte) 45,
    (byte) 232,
    (byte) 50,
    (byte) 220,
    (byte) 116,
    (byte) 10,
    (byte) 17,
    (byte) 42,
    (byte) 72,
    (byte) 238,
    (byte) 3,
    (byte) 59,
    (byte) 104,
    (byte) 223,
    (byte) 94,
    (byte) 70,
    (byte) 126,
    (byte) 81,
    (byte) 155,
    (byte) 171,
    (byte) 137,
    (byte) 219,
    (byte) 64 /*0x40*/,
    (byte) 42,
    (byte) 17,
    (byte) 14,
    (byte) 119,
    (byte) 208 /*0xD0*/,
    (byte) 89,
    (byte) 78,
    (byte) 136,
    (byte) 49,
    (byte) 89,
    (byte) 13,
    (byte) 210,
    (byte) 229,
    (byte) 242,
    (byte) 133,
    (byte) 170,
    (byte) 250,
    (byte) 171,
    (byte) 48 /*0x30*/,
    (byte) 69,
    (byte) 208 /*0xD0*/,
    (byte) 194,
    (byte) 88,
    (byte) 142,
    (byte) 7,
    (byte) 204,
    (byte) 72,
    (byte) 249,
    (byte) 82,
    (byte) 63 /*0x3F*/,
    (byte) 120,
    (byte) 246,
    (byte) 15,
    (byte) 137,
    (byte) 29,
    (byte) 225,
    (byte) 79,
    (byte) 110,
    (byte) 20,
    (byte) 196,
    (byte) 123,
    (byte) 23,
    (byte) 210,
    (byte) 41,
    (byte) 115,
    (byte) 48 /*0x30*/,
    (byte) 179,
    (byte) 131,
    (byte) 134,
    (byte) 109,
    (byte) 135,
    (byte) 208 /*0xD0*/,
    (byte) 33,
    (byte) 0,
    (byte) 138,
    (byte) 122,
    (byte) 114,
    (byte) 132,
    (byte) 129,
    (byte) 158,
    (byte) 52,
    (byte) 193,
    (byte) 40,
    (byte) 65,
    (byte) 203,
    (byte) 95,
    (byte) 159,
    (byte) 57,
    (byte) 142,
    (byte) 30,
    (byte) 91,
    (byte) 138,
    (byte) 165,
    (byte) 98,
    (byte) 61,
    (byte) 77,
    (byte) 108,
    (byte) 208 /*0xD0*/,
    (byte) 192 /*0xC0*/,
    (byte) 246,
    (byte) 92,
    (byte) 224 /*0xE0*/,
    (byte) 17,
    (byte) 246,
    (byte) 191,
    (byte) 20,
    (byte) 168,
    (byte) 38,
    (byte) 214,
    (byte) 139,
    (byte) 18,
    (byte) 161,
    (byte) 96 /*0x60*/,
    (byte) 95,
    (byte) 175,
    (byte) 8,
    (byte) 145,
    (byte) 22,
    (byte) 5,
    (byte) 81,
    (byte) 8,
    (byte) 98,
    (byte) 40,
    (byte) 113,
    (byte) 198,
    (byte) 209,
    (byte) 223,
    (byte) 158,
    (byte) 19,
    (byte) 200,
    (byte) 139,
    (byte) 27,
    (byte) 21,
    (byte) 35,
    (byte) 151,
    (byte) 162,
    (byte) 29,
    (byte) 43,
    (byte) 42,
    (byte) 198,
    (byte) 188,
    (byte) 118,
    (byte) 148,
    (byte) 179,
    (byte) 188,
    (byte) 22,
    (byte) 240 /*0xF0*/,
    (byte) 99,
    (byte) 45,
    (byte) 169,
    (byte) 75,
    (byte) 114,
    (byte) 90,
    (byte) 116,
    (byte) 41,
    (byte) 59,
    (byte) 39,
    (byte) 86,
    (byte) 15,
    (byte) 145,
    (byte) 232,
    (byte) 184,
    (byte) 86,
    (byte) 74,
    (byte) 68,
    (byte) 47,
    (byte) 232,
    (byte) 18,
    (byte) 69,
    (byte) 107,
    (byte) 192 /*0xC0*/,
    (byte) 40,
    (byte) 162,
    (byte) 90,
    (byte) 144 /*0x90*/,
    (byte) 39,
    (byte) 214,
    (byte) 150,
    (byte) 141,
    (byte) 135,
    (byte) 211,
    (byte) 196,
    (byte) 60,
    (byte) 147,
    (byte) 159,
    (byte) 7,
    (byte) 111,
    (byte) 90,
    (byte) 54,
    (byte) 57,
    (byte) 117,
    (byte) 131,
    (byte) 200,
    (byte) 170,
    (byte) 80 /*0x50*/,
    (byte) 91,
    (byte) 147,
    (byte) 111,
    (byte) 49,
    (byte) 210,
    (byte) 28,
    (byte) 36,
    (byte) 253,
    (byte) 133,
    (byte) 23,
    (byte) 191,
    (byte) 3,
    (byte) 175,
    (byte) 198,
    (byte) 119,
    byte.MaxValue,
    (byte) 61,
    (byte) 181,
    (byte) 40,
    (byte) 5,
    (byte) 157,
    (byte) 64 /*0x40*/,
    (byte) 45,
    (byte) 78,
    (byte) 0,
    (byte) 46,
    (byte) 36,
    (byte) 10,
    (byte) 14,
    (byte) 91,
    (byte) 249,
    (byte) 75,
    (byte) 241,
    (byte) 135,
    (byte) 18,
    (byte) 213,
    (byte) 58,
    (byte) 191,
    (byte) 118,
    (byte) 207,
    (byte) 93,
    (byte) 181,
    (byte) 230,
    (byte) 160 /*0xA0*/,
    (byte) 253,
    (byte) 212,
    (byte) 42,
    (byte) 51,
    (byte) 180,
    (byte) 254,
    (byte) 185,
    (byte) 192 /*0xC0*/,
    (byte) 202,
    (byte) 119,
    (byte) 136,
    (byte) 114,
    (byte) 159,
    (byte) 71,
    (byte) 157,
    (byte) 145,
    (byte) 31 /*0x1F*/,
    (byte) 118,
    (byte) 85,
    (byte) 76,
    (byte) 206,
    (byte) 40,
    (byte) 93,
    (byte) 42,
    (byte) 135,
    (byte) 194,
    (byte) 19,
    (byte) 180,
    (byte) 14,
    (byte) 136,
    (byte) 23,
    (byte) 142,
    (byte) 144 /*0x90*/,
    (byte) 7,
    (byte) 73,
    (byte) 32 /*0x20*/,
    byte.MaxValue,
    (byte) 64 /*0x40*/,
    (byte) 231,
    (byte) 135,
    (byte) 254,
    (byte) 146,
    (byte) 152,
    (byte) 9,
    (byte) 228,
    (byte) 19,
    (byte) 66,
    (byte) 215,
    (byte) 219,
    (byte) 100,
    (byte) 152,
    (byte) 148,
    (byte) 39,
    (byte) 2,
    (byte) 114,
    (byte) 119,
    (byte) 104,
    (byte) 58,
    (byte) 14,
    (byte) 5,
    (byte) 148,
    (byte) 103,
    (byte) 100,
    (byte) 37,
    (byte) 205,
    (byte) 126,
    (byte) 77,
    (byte) 127 /*0x7F*/,
    (byte) 220,
    (byte) 46,
    (byte) 105,
    (byte) 117,
    (byte) 22,
    (byte) 170,
    (byte) 106,
    (byte) 34,
    (byte) 241,
    (byte) 111,
    (byte) 56,
    (byte) 78,
    (byte) 251,
    (byte) 53,
    (byte) 82,
    (byte) 123,
    (byte) 208 /*0xD0*/,
    (byte) 44,
    (byte) 98,
    (byte) 183,
    (byte) 68,
    (byte) 66,
    (byte) 208 /*0xD0*/,
    (byte) 42,
    (byte) 191,
    (byte) 6,
    (byte) 78,
    (byte) 172,
    (byte) 159,
    (byte) 125,
    (byte) 99,
    (byte) 107,
    (byte) 117,
    (byte) 238,
    (byte) 202,
    (byte) 122,
    (byte) 175,
    (byte) 13,
    (byte) 37,
    (byte) 28,
    (byte) 79,
    (byte) 176 /*0xB0*/,
    (byte) 204,
    (byte) 212,
    (byte) 178,
    (byte) 105,
    (byte) 110,
    (byte) 159,
    (byte) 107,
    (byte) 114,
    (byte) 83,
    (byte) 37,
    (byte) 218,
    (byte) 16 /*0x10*/,
    (byte) 237,
    (byte) 88,
    (byte) 81,
    (byte) 28,
    (byte) 115,
    (byte) 56,
    (byte) 113,
    (byte) 17,
    (byte) 114,
    (byte) 167,
    (byte) 3,
    (byte) 190,
    (byte) 90,
    (byte) 184,
    (byte) 207,
    (byte) 130,
    (byte) 249,
    (byte) 79,
    (byte) 122,
    (byte) 105,
    (byte) 29,
    (byte) 247,
    (byte) 113,
    (byte) 215,
    (byte) 87,
    (byte) 131,
    (byte) 187,
    (byte) 195,
    (byte) 7,
    (byte) 119,
    (byte) 235,
    (byte) 145,
    (byte) 254,
    (byte) 207,
    (byte) 216,
    (byte) 133,
    (byte) 139,
    (byte) 184,
    (byte) 28,
    (byte) 219,
    (byte) 9,
    (byte) 228,
    (byte) 153,
    (byte) 32 /*0x20*/,
    (byte) 187,
    (byte) 141,
    (byte) 126,
    (byte) 193,
    (byte) 192 /*0xC0*/,
    (byte) 131,
    (byte) 1,
    (byte) 152,
    (byte) 74,
    (byte) 107,
    (byte) 184,
    (byte) 171,
    (byte) 102,
    (byte) 179,
    (byte) 42,
    (byte) 19,
    (byte) 35,
    (byte) 66,
    (byte) 212,
    (byte) 53,
    (byte) 71,
    (byte) 104,
    (byte) 90,
    (byte) 70,
    (byte) 92,
    (byte) 122,
    (byte) 0,
    (byte) 232,
    (byte) 247,
    (byte) 160 /*0xA0*/,
    (byte) 119,
    (byte) 73,
    (byte) 119,
    (byte) 82,
    (byte) 17,
    (byte) 106,
    (byte) 236,
    (byte) 200,
    (byte) 115,
    (byte) 61,
    (byte) 71,
    (byte) 25,
    (byte) 174,
    (byte) 190,
    (byte) 158,
    (byte) 72,
    (byte) 67,
    (byte) 207,
    (byte) 136,
    (byte) 5,
    (byte) 223,
    (byte) 141,
    (byte) 161,
    (byte) 36,
    (byte) 80 /*0x50*/,
    (byte) 209,
    (byte) 12,
    (byte) 103,
    (byte) 194,
    (byte) 225,
    (byte) 2,
    (byte) 91,
    (byte) 49,
    (byte) 211,
    (byte) 174,
    (byte) 142,
    (byte) 117,
    (byte) 233,
    (byte) 2,
    (byte) 11,
    (byte) 187,
    (byte) 137,
    (byte) 41,
    (byte) 143,
    (byte) 26,
    (byte) 4,
    (byte) 231,
    (byte) 107,
    (byte) 69,
    (byte) 27,
    (byte) 202,
    (byte) 110,
    (byte) 34,
    (byte) 123,
    (byte) 11,
    (byte) 28,
    (byte) 33,
    (byte) 0,
    (byte) 91,
    (byte) 193,
    (byte) 43,
    (byte) 161,
    (byte) 210,
    (byte) 105,
    (byte) 81,
    (byte) 186,
    (byte) 21,
    (byte) 148,
    (byte) 229,
    (byte) 132,
    (byte) 123,
    (byte) 213,
    (byte) 209,
    (byte) 153,
    (byte) 248,
    (byte) 107,
    (byte) 107,
    (byte) 237,
    (byte) 136,
    (byte) 177,
    (byte) 128 /*0x80*/,
    (byte) 99,
    (byte) 101,
    (byte) 139,
    (byte) 202,
    (byte) 42,
    (byte) 85,
    (byte) 0,
    (byte) 82,
    (byte) 225,
    (byte) 163,
    (byte) 207,
    (byte) 142,
    (byte) 58,
    (byte) 57,
    (byte) 215,
    (byte) 230,
    (byte) 186,
    (byte) 179,
    (byte) 238,
    (byte) 250,
    (byte) 16 /*0x10*/,
    (byte) 199,
    (byte) 87,
    (byte) 230,
    (byte) 206,
    byte.MaxValue,
    (byte) 76,
    (byte) 7,
    (byte) 116,
    (byte) 52,
    (byte) 135,
    (byte) 253,
    (byte) 84,
    byte.MaxValue,
    (byte) 153,
    (byte) 237,
    (byte) 215,
    (byte) 42,
    (byte) 49,
    (byte) 165,
    (byte) 88,
    (byte) 118,
    (byte) 102,
    (byte) 103,
    (byte) 251,
    (byte) 129,
    (byte) 74,
    (byte) 221,
    (byte) 209,
    (byte) 158,
    (byte) 227,
    (byte) 138,
    (byte) 4,
    (byte) 66,
    (byte) 11,
    (byte) 104,
    (byte) 66,
    (byte) 61,
    (byte) 131,
    (byte) 202,
    (byte) 249,
    (byte) 182,
    (byte) 48 /*0x30*/,
    (byte) 189,
    (byte) 254,
    (byte) 182,
    (byte) 26,
    (byte) 250,
    (byte) 189,
    (byte) 163,
    (byte) 154,
    (byte) 221,
    (byte) 58,
    (byte) 141,
    (byte) 150,
    (byte) 110,
    (byte) 75,
    (byte) 212,
    (byte) 144 /*0x90*/,
    (byte) 24,
    (byte) 122,
    (byte) 211,
    (byte) 52,
    (byte) 118,
    (byte) 214,
    (byte) 172,
    (byte) 189,
    (byte) 212,
    (byte) 135,
    (byte) 150,
    (byte) 111,
    (byte) 191,
    (byte) 142,
    (byte) 83,
    (byte) 92,
    (byte) 156,
    (byte) 240 /*0xF0*/,
    (byte) 245,
    (byte) 166,
    (byte) 193,
    (byte) 17,
    (byte) 143,
    (byte) 95,
    (byte) 49,
    (byte) 171,
    (byte) 98,
    (byte) 126,
    (byte) 22,
    (byte) 196,
    (byte) 192 /*0xC0*/,
    (byte) 104,
    (byte) 148,
    (byte) 173,
    (byte) 105,
    (byte) 193,
    (byte) 67,
    (byte) 186,
    (byte) 98,
    (byte) 204,
    (byte) 194,
    (byte) 96 /*0x60*/,
    (byte) 30,
    (byte) 30,
    (byte) 124,
    (byte) 2,
    (byte) 139,
    (byte) 50,
    (byte) 46,
    (byte) 60,
    (byte) 158,
    (byte) 124,
    (byte) 245,
    (byte) 195,
    (byte) 157,
    (byte) 133,
    (byte) 224 /*0xE0*/,
    (byte) 48 /*0x30*/,
    (byte) 213,
    (byte) 208 /*0xD0*/,
    (byte) 43,
    (byte) 62,
    (byte) 94,
    (byte) 13,
    (byte) 173,
    (byte) 143,
    (byte) 227,
    (byte) 95,
    (byte) 211,
    (byte) 124,
    (byte) 168,
    (byte) 61,
    (byte) 166,
    (byte) 138,
    (byte) 75,
    (byte) 42,
    (byte) 74,
    (byte) 63 /*0x3F*/,
    (byte) 174,
    (byte) 32 /*0x20*/,
    (byte) 155,
    (byte) 191,
    (byte) 253,
    (byte) 66,
    (byte) 134,
    (byte) 118,
    (byte) 173,
    (byte) 178,
    (byte) 174,
    (byte) 135,
    (byte) 37,
    (byte) 252,
    (byte) 64 /*0x40*/,
    (byte) 153,
    (byte) 171,
    (byte) 123,
    (byte) 242,
    (byte) 235,
    (byte) 163,
    (byte) 59,
    (byte) 97,
    (byte) 196,
    (byte) 150,
    (byte) 213,
    (byte) 207,
    (byte) 88,
    (byte) 32 /*0x20*/,
    (byte) 233,
    (byte) 201,
    (byte) 228,
    (byte) 197,
    (byte) 147,
    (byte) 237,
    (byte) 134,
    (byte) 127 /*0x7F*/,
    (byte) 245,
    (byte) 216,
    (byte) 36,
    (byte) 147,
    (byte) 168,
    (byte) 221,
    (byte) 135,
    (byte) 9,
    (byte) 180,
    (byte) 10,
    (byte) 57,
    (byte) 124,
    (byte) 168,
    (byte) 132,
    (byte) 176 /*0xB0*/,
    (byte) 169,
    (byte) 137,
    (byte) 198,
    (byte) 1,
    (byte) 68,
    (byte) 84,
    (byte) 194,
    (byte) 251,
    (byte) 26,
    (byte) 103,
    (byte) 222,
    (byte) 132,
    (byte) 155,
    (byte) 32 /*0x20*/,
    (byte) 242,
    (byte) 132,
    (byte) 22,
    (byte) 162,
    (byte) 141,
    (byte) 179,
    (byte) 44,
    (byte) 191,
    (byte) 228,
    (byte) 40,
    (byte) 139,
    (byte) 19,
    (byte) 41,
    (byte) 216,
    (byte) 217,
    (byte) 130,
    (byte) 109,
    (byte) 147,
    (byte) 218,
    (byte) 82,
    (byte) 186,
    (byte) 231,
    (byte) 190,
    (byte) 1,
    (byte) 125,
    (byte) 198,
    (byte) 222,
    (byte) 74,
    (byte) 144 /*0x90*/,
    (byte) 15,
    (byte) 156,
    (byte) 69,
    (byte) 164,
    (byte) 143,
    (byte) 152,
    (byte) 50,
    (byte) 160 /*0xA0*/,
    byte.MaxValue,
    (byte) 47,
    (byte) 150,
    (byte) 81,
    (byte) 248,
    (byte) 89,
    (byte) 146,
    (byte) 213,
    (byte) 82,
    (byte) 24,
    (byte) 167,
    (byte) 64 /*0x40*/,
    (byte) 65,
    (byte) 41,
    (byte) 126,
    (byte) 97,
    (byte) 221,
    (byte) 103,
    (byte) 252,
    (byte) 25,
    (byte) 39,
    (byte) 228,
    (byte) 97,
    (byte) 17,
    (byte) 244,
    (byte) 96 /*0x60*/,
    (byte) 169,
    (byte) 216,
    (byte) 78,
    (byte) 11,
    (byte) 38,
    (byte) 224 /*0xE0*/,
    (byte) 98,
    (byte) 16 /*0x10*/,
    (byte) 28,
    (byte) 249,
    (byte) 12,
    (byte) 4,
    (byte) 20,
    (byte) 109,
    (byte) 29,
    (byte) 201,
    (byte) 55,
    (byte) 170,
    (byte) 27,
    (byte) 181,
    (byte) 211,
    (byte) 89,
    (byte) 16 /*0x10*/,
    (byte) 86,
    (byte) 188,
    (byte) 176 /*0xB0*/,
    (byte) 205,
    (byte) 209,
    (byte) 60,
    (byte) 139,
    (byte) 47,
    (byte) 148,
    (byte) 135,
    (byte) 10,
    (byte) 84,
    (byte) 103,
    (byte) 56,
    (byte) 236,
    (byte) 107,
    (byte) 104,
    (byte) 96 /*0x60*/,
    (byte) 45,
    (byte) 13,
    (byte) 81,
    (byte) 120,
    (byte) 87,
    (byte) 38,
    (byte) 58,
    (byte) 108,
    (byte) 73,
    (byte) 2,
    (byte) 36,
    (byte) 186,
    (byte) 226,
    (byte) 114,
    (byte) 232,
    (byte) 176 /*0xB0*/,
    (byte) 178,
    (byte) 73,
    (byte) 215,
    (byte) 216,
    (byte) 69,
    (byte) 193,
    (byte) 90,
    (byte) 0,
    (byte) 245,
    (byte) 208 /*0xD0*/,
    (byte) 66,
    (byte) 212,
    (byte) 64 /*0x40*/,
    (byte) 17
  };
  private static byte[] sspr = new byte[912]
  {
    (byte) 154,
    (byte) 117,
    (byte) 68,
    (byte) 82,
    (byte) 61,
    (byte) 68,
    (byte) 246,
    (byte) 115,
    (byte) 243,
    (byte) 80 /*0x50*/,
    (byte) 33,
    (byte) 248,
    (byte) 132,
    (byte) 26,
    (byte) 120,
    (byte) 227,
    (byte) 173,
    (byte) 35,
    (byte) 87,
    (byte) 20,
    (byte) 18,
    (byte) 24,
    (byte) 77,
    (byte) 83,
    (byte) 47,
    (byte) 83,
    (byte) 13,
    (byte) 98,
    (byte) 36,
    (byte) 240 /*0xF0*/,
    (byte) 24,
    (byte) 213,
    (byte) 137,
    (byte) 184,
    (byte) 58,
    (byte) 151,
    (byte) 89,
    (byte) 156,
    (byte) 35,
    (byte) 4,
    (byte) 207,
    (byte) 62,
    (byte) 39,
    (byte) 54,
    (byte) 47,
    (byte) 10,
    (byte) 11,
    (byte) 121,
    (byte) 124,
    (byte) 217,
    (byte) 182,
    (byte) 81,
    (byte) 0,
    (byte) 8,
    (byte) 110,
    (byte) 186,
    (byte) 52,
    (byte) 32 /*0x20*/,
    (byte) 248,
    (byte) 63 /*0x3F*/,
    (byte) 107,
    (byte) 127 /*0x7F*/,
    (byte) 54,
    (byte) 67,
    (byte) 67,
    (byte) 254,
    (byte) 107,
    (byte) 134,
    (byte) 172,
    (byte) 63 /*0x3F*/,
    (byte) 80 /*0x50*/,
    (byte) 201,
    (byte) 143,
    (byte) 229,
    (byte) 250,
    (byte) 172,
    (byte) 128 /*0x80*/,
    (byte) 47,
    (byte) 24,
    (byte) 30,
    (byte) 237,
    (byte) 240 /*0xF0*/,
    (byte) 96 /*0x60*/,
    (byte) 244,
    (byte) 181,
    (byte) 176 /*0xB0*/,
    (byte) 200,
    (byte) 130,
    (byte) 57,
    (byte) 109,
    (byte) 59,
    (byte) 108,
    (byte) 252,
    (byte) 48 /*0x30*/,
    (byte) 97,
    (byte) 108,
    (byte) 89,
    (byte) 76,
    (byte) 146,
    (byte) 139,
    (byte) 101,
    (byte) 145,
    (byte) 75,
    (byte) 88,
    (byte) 226,
    (byte) 235,
    (byte) 6,
    (byte) 236,
    (byte) 10,
    byte.MaxValue,
    (byte) 151,
    (byte) 154,
    (byte) 60,
    (byte) 17,
    (byte) 205,
    (byte) 193,
    (byte) 248,
    (byte) 204,
    (byte) 62,
    (byte) 188,
    (byte) 176 /*0xB0*/,
    (byte) 186,
    (byte) 201,
    (byte) 189,
    (byte) 25,
    (byte) 155,
    (byte) 109,
    (byte) 74,
    (byte) 249,
    (byte) 169,
    (byte) 82,
    (byte) 86,
    (byte) 228,
    (byte) 188,
    (byte) 12,
    (byte) 31 /*0x1F*/,
    (byte) 71,
    (byte) 169,
    (byte) 146,
    (byte) 220,
    (byte) 43,
    (byte) 63 /*0x3F*/,
    (byte) 220,
    (byte) 32 /*0x20*/,
    (byte) 157,
    (byte) 79,
    (byte) 141,
    (byte) 187,
    (byte) 16 /*0x10*/,
    (byte) 145,
    (byte) 20,
    (byte) 86,
    (byte) 45,
    (byte) 144 /*0x90*/,
    (byte) 77,
    (byte) 136,
    (byte) 83,
    (byte) 141,
    (byte) 100,
    (byte) 192 /*0xC0*/,
    (byte) 112 /*0x70*/,
    (byte) 224 /*0xE0*/,
    (byte) 196,
    (byte) 145,
    (byte) 201,
    (byte) 3,
    (byte) 38,
    (byte) 121,
    (byte) 97,
    (byte) 62,
    (byte) 172,
    (byte) 147,
    (byte) 19,
    (byte) 24,
    (byte) 67,
    (byte) 36,
    (byte) 43,
    (byte) 70,
    (byte) 163,
    (byte) 35,
    (byte) 31 /*0x1F*/,
    (byte) 139,
    (byte) 195,
    (byte) 181,
    (byte) 60,
    (byte) 170,
    (byte) 152,
    (byte) 205,
    (byte) 46,
    (byte) 227,
    (byte) 92,
    (byte) 196,
    (byte) 211,
    (byte) 126,
    (byte) 161,
    (byte) 22,
    (byte) 208 /*0xD0*/,
    (byte) 6,
    (byte) 103,
    (byte) 103,
    (byte) 112 /*0x70*/,
    (byte) 45,
    (byte) 164,
    (byte) 110,
    (byte) 180,
    (byte) 213,
    byte.MaxValue,
    (byte) 80 /*0x50*/,
    (byte) 51,
    (byte) 64 /*0x40*/,
    (byte) 97,
    (byte) 134,
    (byte) 237,
    (byte) 199,
    (byte) 196,
    (byte) 48 /*0x30*/,
    (byte) 51,
    (byte) 51,
    (byte) 243,
    (byte) 33,
    (byte) 153,
    (byte) 74,
    (byte) 162,
    (byte) 142,
    (byte) 14,
    (byte) 163,
    (byte) 131,
    (byte) 110,
    (byte) 164,
    (byte) 8,
    (byte) 213,
    (byte) 235,
    (byte) 190,
    (byte) 88,
    (byte) 13,
    (byte) 155,
    (byte) 219,
    (byte) 148,
    (byte) 109,
    (byte) 82,
    (byte) 212,
    (byte) 158,
    (byte) 31 /*0x1F*/,
    (byte) 229,
    (byte) 120,
    (byte) 17,
    (byte) 216,
    (byte) 209,
    (byte) 234,
    (byte) 4,
    (byte) 191,
    (byte) 6,
    (byte) 175,
    (byte) 76,
    (byte) 72,
    (byte) 220,
    (byte) 103,
    (byte) 73,
    (byte) 162,
    (byte) 112 /*0x70*/,
    (byte) 87,
    (byte) 79,
    (byte) 40,
    (byte) 129,
    (byte) 178,
    (byte) 182,
    (byte) 94,
    (byte) 225,
    (byte) 69,
    (byte) 139,
    (byte) 235,
    (byte) 186,
    (byte) 156,
    (byte) 245,
    (byte) 147,
    (byte) 224 /*0xE0*/,
    (byte) 242,
    (byte) 9,
    (byte) 70,
    (byte) 151,
    (byte) 115,
    (byte) 18,
    (byte) 63 /*0x3F*/,
    (byte) 171,
    (byte) 179,
    (byte) 212,
    (byte) 191,
    (byte) 159,
    (byte) 71,
    (byte) 12,
    (byte) 241,
    (byte) 244,
    (byte) 149,
    (byte) 77,
    (byte) 97,
    (byte) 183,
    (byte) 126,
    (byte) 73,
    (byte) 46,
    (byte) 55,
    (byte) 28,
    (byte) 118,
    (byte) 114,
    (byte) 225,
    (byte) 88,
    (byte) 45,
    (byte) 156,
    (byte) 82,
    (byte) 102,
    (byte) 254,
    (byte) 158,
    (byte) 171,
    (byte) 25,
    (byte) 105,
    (byte) 90,
    (byte) 112 /*0x70*/,
    (byte) 163,
    (byte) 173,
    (byte) 203,
    (byte) 94,
    (byte) 92,
    (byte) 87,
    (byte) 27,
    (byte) 188,
    (byte) 161,
    (byte) 63 /*0x3F*/,
    (byte) 211,
    (byte) 203,
    (byte) 89,
    (byte) 167,
    (byte) 53,
    (byte) 146,
    (byte) 174,
    (byte) 39,
    (byte) 251,
    (byte) 164,
    (byte) 251,
    (byte) 85,
    (byte) 206,
    (byte) 156,
    (byte) 97,
    (byte) 218,
    (byte) 111,
    (byte) 126,
    (byte) 228,
    (byte) 95,
    (byte) 15,
    (byte) 249,
    (byte) 107,
    (byte) 163,
    (byte) 19,
    (byte) 32 /*0x20*/,
    (byte) 219,
    (byte) 113,
    (byte) 244,
    (byte) 119,
    (byte) 39,
    (byte) 66,
    (byte) 164,
    (byte) 91,
    (byte) 159,
    (byte) 134,
    (byte) 103,
    (byte) 52,
    (byte) 117,
    (byte) 241,
    (byte) 153,
    (byte) 247,
    (byte) 68,
    (byte) 179,
    (byte) 252,
    (byte) 61,
    (byte) 118,
    (byte) 9,
    (byte) 21,
    (byte) 250,
    (byte) 174,
    (byte) 77,
    (byte) 206,
    (byte) 145,
    (byte) 144 /*0x90*/,
    (byte) 213,
    (byte) 81,
    (byte) 24,
    (byte) 129,
    (byte) 89,
    (byte) 147,
    (byte) 189,
    (byte) 163,
    byte.MaxValue,
    (byte) 81,
    (byte) 177,
    (byte) 127 /*0x7F*/,
    byte.MaxValue,
    (byte) 12,
    (byte) 161,
    (byte) 251,
    (byte) 76,
    (byte) 178,
    (byte) 236,
    (byte) 146,
    (byte) 147,
    (byte) 38,
    (byte) 252,
    (byte) 133,
    (byte) 24,
    (byte) 0,
    (byte) 147,
    (byte) 119,
    (byte) 130,
    (byte) 136,
    (byte) 160 /*0xA0*/,
    (byte) 183,
    (byte) 53,
    (byte) 241,
    (byte) 84,
    (byte) 172,
    (byte) 91,
    (byte) 76,
    (byte) 142,
    (byte) 169,
    (byte) 7,
    (byte) 45,
    (byte) 157,
    (byte) 4,
    (byte) 200,
    (byte) 7,
    (byte) 73,
    (byte) 152,
    (byte) 77,
    (byte) 154,
    (byte) 236,
    (byte) 33,
    (byte) 229,
    (byte) 102,
    (byte) 127 /*0x7F*/,
    (byte) 64 /*0x40*/,
    (byte) 93,
    (byte) 212,
    (byte) 85,
    (byte) 181,
    (byte) 52,
    (byte) 147,
    (byte) 46,
    (byte) 179,
    (byte) 254,
    (byte) 185,
    (byte) 105,
    (byte) 117,
    (byte) 10,
    (byte) 204,
    (byte) 15,
    (byte) 51,
    (byte) 219,
    (byte) 107,
    (byte) 214,
    (byte) 40,
    (byte) 76,
    (byte) 104,
    (byte) 65,
    (byte) 131,
    (byte) 210,
    (byte) 91,
    (byte) 196,
    (byte) 179,
    (byte) 123,
    (byte) 127 /*0x7F*/,
    (byte) 96 /*0x60*/,
    (byte) 243,
    (byte) 63 /*0x3F*/,
    (byte) 181,
    (byte) 73,
    (byte) 140,
    (byte) 254,
    (byte) 44,
    (byte) 178,
    (byte) 25,
    (byte) 114,
    (byte) 189,
    (byte) 106,
    (byte) 229,
    (byte) 47,
    (byte) 104,
    (byte) 15,
    (byte) 200,
    (byte) 233,
    (byte) 52,
    (byte) 143,
    (byte) 112 /*0x70*/,
    (byte) 86,
    (byte) 61,
    (byte) 79,
    (byte) 147,
    (byte) 36,
    (byte) 176 /*0xB0*/,
    (byte) 71,
    (byte) 75,
    (byte) 113,
    (byte) 35,
    (byte) 206,
    (byte) 147,
    (byte) 179,
    (byte) 254,
    (byte) 42,
    (byte) 234,
    (byte) 215,
    (byte) 211,
    (byte) 46,
    (byte) 234,
    (byte) 176 /*0xB0*/,
    (byte) 95,
    (byte) 81,
    (byte) 30,
    (byte) 57,
    (byte) 236,
    (byte) 186,
    (byte) 191,
    (byte) 222,
    (byte) 114,
    (byte) 62,
    (byte) 245,
    (byte) 187,
    (byte) 226,
    (byte) 111,
    (byte) 9,
    (byte) 159,
    (byte) 249,
    (byte) 42,
    (byte) 98,
    (byte) 161,
    (byte) 134,
    (byte) 156,
    (byte) 111,
    byte.MaxValue,
    (byte) 129,
    (byte) 25,
    (byte) 53,
    (byte) 34,
    (byte) 203,
    (byte) 156,
    (byte) 97,
    (byte) 155,
    (byte) 35,
    (byte) 252,
    (byte) 241,
    (byte) 146,
    (byte) 203,
    (byte) 186,
    (byte) 130,
    (byte) 212,
    (byte) 182,
    (byte) 3,
    (byte) 220,
    (byte) 127 /*0x7F*/,
    (byte) 24,
    (byte) 164,
    (byte) 41,
    (byte) 49,
    (byte) 166,
    (byte) 235,
    (byte) 57,
    (byte) 238,
    (byte) 173,
    (byte) 172,
    (byte) 242,
    (byte) 253,
    (byte) 53,
    (byte) 128 /*0x80*/,
    (byte) 239,
    (byte) 150,
    (byte) 110,
    (byte) 67,
    (byte) 151,
    (byte) 211,
    (byte) 151,
    (byte) 103,
    (byte) 107,
    (byte) 227,
    (byte) 61,
    (byte) 184,
    (byte) 48 /*0x30*/,
    (byte) 38,
    (byte) 251,
    (byte) 66,
    (byte) 156,
    (byte) 203,
    (byte) 31 /*0x1F*/,
    (byte) 91,
    (byte) 178,
    (byte) 186,
    (byte) 123,
    (byte) 135,
    (byte) 52,
    (byte) 119,
    (byte) 144 /*0x90*/,
    byte.MaxValue,
    (byte) 21,
    (byte) 58,
    (byte) 242,
    (byte) 226,
    (byte) 119,
    (byte) 248,
    (byte) 207,
    (byte) 180,
    (byte) 165,
    (byte) 243,
    (byte) 129,
    (byte) 130,
    (byte) 219,
    (byte) 196,
    (byte) 233,
    (byte) 172,
    (byte) 161,
    (byte) 102,
    (byte) 96 /*0x60*/,
    (byte) 196,
    (byte) 2,
    (byte) 73,
    (byte) 160 /*0xA0*/,
    (byte) 16 /*0x10*/,
    (byte) 37,
    (byte) 79,
    (byte) 230,
    (byte) 64 /*0x40*/,
    (byte) 161,
    (byte) 142,
    (byte) 118,
    (byte) 67,
    (byte) 8,
    (byte) 55,
    (byte) 13,
    (byte) 40,
    (byte) 25,
    (byte) 217,
    (byte) 225,
    (byte) 176 /*0xB0*/,
    (byte) 234,
    (byte) 39,
    (byte) 83,
    (byte) 148,
    (byte) 31 /*0x1F*/,
    (byte) 15,
    (byte) 134,
    (byte) 142,
    (byte) 165,
    (byte) 253,
    (byte) 60,
    (byte) 129,
    (byte) 54,
    (byte) 236,
    (byte) 97,
    (byte) 160 /*0xA0*/,
    (byte) 153,
    (byte) 105,
    (byte) 94,
    (byte) 36,
    (byte) 61,
    (byte) 220,
    (byte) 147,
    (byte) 120,
    (byte) 142,
    (byte) 57,
    (byte) 124,
    (byte) 84,
    (byte) 121,
    (byte) 247,
    (byte) 219,
    (byte) 104,
    (byte) 20,
    (byte) 240 /*0xF0*/,
    (byte) 24,
    (byte) 200,
    (byte) 13,
    (byte) 101,
    (byte) 111,
    (byte) 21,
    (byte) 11,
    (byte) 53,
    (byte) 180,
    (byte) 89,
    (byte) 111,
    (byte) 92,
    (byte) 33,
    (byte) 132,
    (byte) 165,
    (byte) 196,
    (byte) 76,
    (byte) 134,
    (byte) 82,
    (byte) 241,
    (byte) 61,
    (byte) 233,
    (byte) 129,
    (byte) 253,
    (byte) 198,
    (byte) 85,
    (byte) 42,
    (byte) 120,
    (byte) 221,
    (byte) 143,
    (byte) 169,
    (byte) 253,
    (byte) 137,
    (byte) 252,
    (byte) 40,
    (byte) 58,
    (byte) 253,
    (byte) 15,
    (byte) 90,
    (byte) 234,
    (byte) 249,
    (byte) 198,
    (byte) 203,
    (byte) 131,
    (byte) 172,
    (byte) 0,
    (byte) 140,
    (byte) 140,
    (byte) 173,
    (byte) 238,
    (byte) 80 /*0x50*/,
    (byte) 222,
    (byte) 249,
    (byte) 192 /*0xC0*/,
    (byte) 33,
    (byte) 93,
    (byte) 107,
    (byte) 194,
    (byte) 193,
    (byte) 76,
    (byte) 69,
    (byte) 20,
    (byte) 115,
    (byte) 27,
    (byte) 73,
    (byte) 89,
    (byte) 23,
    (byte) 247,
    (byte) 18,
    (byte) 78,
    (byte) 169,
    (byte) 127 /*0x7F*/,
    (byte) 29,
    (byte) 53,
    (byte) 8,
    (byte) 165,
    (byte) 235,
    (byte) 203,
    (byte) 243,
    (byte) 132,
    (byte) 149,
    (byte) 151,
    (byte) 132,
    (byte) 28,
    (byte) 72,
    (byte) 66,
    (byte) 60,
    (byte) 93,
    (byte) 154,
    (byte) 99,
    (byte) 225,
    (byte) 188,
    (byte) 198,
    (byte) 173,
    (byte) 215,
    (byte) 157,
    (byte) 131,
    (byte) 33,
    (byte) 68,
    (byte) 157,
    (byte) 45,
    (byte) 92,
    (byte) 248,
    (byte) 80 /*0x50*/,
    (byte) 24,
    (byte) 85,
    (byte) 73,
    (byte) 60,
    (byte) 127 /*0x7F*/,
    (byte) 208 /*0xD0*/,
    (byte) 121,
    (byte) 0,
    (byte) 98,
    (byte) 84,
    (byte) 168,
    (byte) 0,
    (byte) 93,
    (byte) 227,
    (byte) 216,
    (byte) 139,
    (byte) 63 /*0x3F*/,
    (byte) 222,
    (byte) 133,
    (byte) 82,
    (byte) 44,
    (byte) 102,
    (byte) 166,
    (byte) 210,
    (byte) 135,
    (byte) 172,
    (byte) 188,
    (byte) 154,
    (byte) 24,
    (byte) 56,
    (byte) 194,
    (byte) 162,
    (byte) 24,
    (byte) 211,
    (byte) 160 /*0xA0*/,
    (byte) 29,
    (byte) 41,
    (byte) 46,
    (byte) 158,
    (byte) 147,
    (byte) 174,
    (byte) 72,
    (byte) 242,
    (byte) 223,
    (byte) 130,
    (byte) 134,
    (byte) 154,
    (byte) 77,
    (byte) 108,
    (byte) 180,
    (byte) 139,
    (byte) 55,
    (byte) 36,
    (byte) 48 /*0x30*/,
    (byte) 147,
    (byte) 47,
    (byte) 170,
    (byte) 209,
    (byte) 10,
    (byte) 61,
    (byte) 202,
    (byte) 234,
    (byte) 107,
    (byte) 51,
    (byte) 165,
    (byte) 55,
    (byte) 173,
    (byte) 210,
    (byte) 185,
    (byte) 82,
    (byte) 100,
    (byte) 56,
    (byte) 253,
    (byte) 68,
    (byte) 10,
    (byte) 101,
    (byte) 149,
    (byte) 122,
    (byte) 201,
    (byte) 245,
    (byte) 77,
    (byte) 103,
    (byte) 36,
    (byte) 207,
    (byte) 183,
    (byte) 251,
    (byte) 134,
    (byte) 83,
    (byte) 212,
    (byte) 218,
    (byte) 122,
    (byte) 90,
    (byte) 1,
    (byte) 158,
    (byte) 118,
    (byte) 39,
    (byte) 40,
    (byte) 28,
    (byte) 223,
    (byte) 193,
    (byte) 24,
    (byte) 54,
    (byte) 227,
    (byte) 56,
    (byte) 234,
    (byte) 21,
    (byte) 138,
    (byte) 190,
    (byte) 216,
    (byte) 131,
    (byte) 207,
    (byte) 162,
    (byte) 102,
    (byte) 246,
    (byte) 235,
    (byte) 145,
    (byte) 177,
    (byte) 28,
    (byte) 181,
    (byte) 190,
    (byte) 49,
    (byte) 184,
    (byte) 91,
    (byte) 147,
    (byte) 81,
    (byte) 15,
    (byte) 8,
    (byte) 29,
    (byte) 140,
    (byte) 26,
    (byte) 212,
    (byte) 146,
    (byte) 0,
    (byte) 179,
    (byte) 142,
    (byte) 239,
    (byte) 67,
    (byte) 54
  };

  internal static int ssp_appserver_12587(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 114,
      (byte) 156,
      (byte) 109,
      (byte) 97,
      (byte) 124,
      (byte) 6,
      (byte) 17,
      (byte) 27,
      (byte) 49,
      (byte) 219,
      (byte) 86,
      (byte) 15,
      (byte) 149,
      (byte) 218,
      (byte) 227,
      (byte) 150,
      byte.MaxValue,
      (byte) 125,
      (byte) 167,
      (byte) 58,
      (byte) 185,
      (byte) 24,
      (byte) 46,
      (byte) 99,
      (byte) 43,
      (byte) 89,
      (byte) 181,
      (byte) 15,
      (byte) 149,
      (byte) 43,
      (byte) 247,
      (byte) 193,
      (byte) 60,
      (byte) 151,
      (byte) 226,
      (byte) 171,
      (byte) 188,
      (byte) 115,
      (byte) 148,
      (byte) 193,
      (byte) 130,
      (byte) 81,
      (byte) 7,
      (byte) 246,
      (byte) 143,
      (byte) 152,
      (byte) 229,
      (byte) 34
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[17] = (byte) 46;
    sourceArray2[20] = (byte) 150;
    sourceArray2[34] = (byte) 169;
    sourceArray2[3] = (byte) 118;
    sourceArray2[6] = (byte) 227;
    sourceArray2[5] = (byte) 11;
    sourceArray2[31 /*0x1F*/] = (byte) 213;
    sourceArray2[7] = (byte) 48 /*0x30*/;
    sourceArray2[4] = (byte) 81;
    sourceArray2[9] = (byte) 11;
    sourceArray2[27] = (byte) 226;
    sourceArray2[30] = (byte) 41;
    sourceArray2[24] = (byte) 231;
    sourceArray2[26] = (byte) 203;
    sourceArray2[18] = (byte) 12;
    sourceArray2[15] = (byte) 0;
    sourceArray2[16 /*0x10*/] = (byte) 36;
    sourceArray2[1] = (byte) 184;
    sourceArray2[19] = (byte) 147;
    sourceArray2[2] = (byte) 22;
    sourceArray2[43] = (byte) 172;
    sourceArray2[21] = (byte) 49;
    sourceArray2[22] = (byte) 44;
    sourceArray2[10] = (byte) 153;
    sourceArray2[14] = (byte) 116;
    sourceArray2[13] = (byte) 110;
    sourceArray2[25] = (byte) 181;
    sourceArray2[8] = (byte) 180;
    sourceArray2[28] = (byte) 122;
    sourceArray2[29] = (byte) 157;
    sourceArray2[42] = (byte) 206;
    sourceArray2[47] = (byte) 147;
    sourceArray2[32 /*0x20*/] = (byte) 2;
    sourceArray2[33] = (byte) 37;
    sourceArray2[45] = (byte) 227;
    sourceArray2[38] = (byte) 10;
    sourceArray2[36] = (byte) 214;
    sourceArray2[37] = (byte) 26;
    sourceArray2[23] = (byte) 124;
    sourceArray2[39] = (byte) 243;
    sourceArray2[40] = (byte) 37;
    sourceArray2[41] = (byte) 72;
    sourceArray2[0] = (byte) 39;
    sourceArray2[35] = (byte) 176 /*0xB0*/;
    sourceArray2[44] = (byte) 71;
    sourceArray2[12] = (byte) 192 /*0xC0*/;
    sourceArray2[46] = (byte) 9;
    sourceArray2[11] = (byte) 76;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12588()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[264];
      byte[] numArray2 = new byte[55];
      numArray2[11] = (byte) 53;
      numArray2[1] = (byte) 232;
      numArray2[28] = (byte) 117;
      numArray2[3] = (byte) 214;
      numArray2[4] = (byte) 29;
      numArray2[0] = (byte) 197;
      numArray2[6] = (byte) 0;
      numArray2[16 /*0x10*/] = (byte) 1;
      numArray2[49] = (byte) 177;
      numArray2[8] = (byte) 117;
      numArray2[43] = (byte) 202;
      numArray2[45] = (byte) 7;
      numArray2[47] = (byte) 156;
      numArray2[2] = (byte) 42;
      numArray2[48 /*0x30*/] = (byte) 193;
      numArray2[15] = (byte) 190;
      numArray2[20] = (byte) 141;
      numArray2[44] = (byte) 64 /*0x40*/;
      numArray2[7] = (byte) 211;
      numArray2[35] = (byte) 138;
      numArray2[25] = (byte) 40;
      numArray2[21] = (byte) 197;
      numArray2[18] = (byte) 189;
      numArray2[23] = (byte) 111;
      numArray2[29] = (byte) 57;
      numArray2[19] = (byte) 213;
      numArray2[26] = (byte) 77;
      numArray2[27] = (byte) 119;
      numArray2[41] = (byte) 51;
      numArray2[32 /*0x20*/] = (byte) 37;
      numArray2[17] = (byte) 231;
      numArray2[31 /*0x1F*/] = (byte) 15;
      numArray2[46] = (byte) 122;
      numArray2[14] = (byte) 176 /*0xB0*/;
      numArray2[34] = (byte) 206;
      numArray2[54] = (byte) 108;
      numArray2[36] = (byte) 103;
      numArray2[37] = (byte) 42;
      numArray2[38] = (byte) 128 /*0x80*/;
      numArray2[22] = (byte) 0;
      numArray2[40] = (byte) 13;
      numArray2[5] = (byte) 86;
      numArray2[12] = (byte) 55;
      numArray2[9] = (byte) 51;
      numArray2[10] = (byte) 72;
      numArray2[30] = (byte) 155;
      numArray2[42] = (byte) 152;
      numArray2[33] = (byte) 217;
      numArray2[24] = (byte) 250;
      numArray2[13] = (byte) 161;
      numArray2[50] = (byte) 126;
      numArray2[51] = (byte) 228;
      numArray2[52] = (byte) 67;
      numArray2[53] = (byte) 61;
      numArray2[39] = (byte) 241;
      byte[] numArray3 = new byte[55];
      numArray3[43] = (byte) 175;
      numArray3[1] = (byte) 66;
      numArray3[0] = (byte) 243;
      numArray3[19] = (byte) 194;
      numArray3[4] = (byte) 74;
      numArray3[5] = (byte) 167;
      numArray3[33] = (byte) 203;
      numArray3[7] = (byte) 181;
      numArray3[18] = (byte) 125;
      numArray3[10] = (byte) 239;
      numArray3[28] = (byte) 102;
      numArray3[15] = (byte) 42;
      numArray3[12] = (byte) 82;
      numArray3[13] = (byte) 210;
      numArray3[9] = (byte) 171;
      numArray3[6] = (byte) 120;
      numArray3[17] = (byte) 135;
      numArray3[50] = (byte) 173;
      numArray3[49] = (byte) 216;
      numArray3[29] = (byte) 144 /*0x90*/;
      numArray3[20] = (byte) 54;
      numArray3[21] = (byte) 63 /*0x3F*/;
      numArray3[16 /*0x10*/] = (byte) 196;
      numArray3[23] = (byte) 106;
      numArray3[53] = (byte) 201;
      numArray3[25] = (byte) 147;
      numArray3[26] = (byte) 8;
      numArray3[44] = (byte) 240 /*0xF0*/;
      numArray3[32 /*0x20*/] = (byte) 64 /*0x40*/;
      numArray3[34] = (byte) 24;
      numArray3[30] = (byte) 0;
      numArray3[31 /*0x1F*/] = (byte) 138;
      numArray3[42] = (byte) 178;
      numArray3[27] = (byte) 162;
      numArray3[8] = (byte) 80 /*0x50*/;
      numArray3[52] = (byte) 4;
      numArray3[36] = (byte) 243;
      numArray3[37] = (byte) 241;
      numArray3[38] = (byte) 30;
      numArray3[39] = (byte) 245;
      numArray3[40] = (byte) 228;
      numArray3[35] = (byte) 68;
      numArray3[14] = (byte) 231;
      numArray3[41] = (byte) 72;
      numArray3[47] = (byte) 167;
      numArray3[3] = (byte) 37;
      numArray3[46] = (byte) 107;
      numArray3[11] = (byte) 95;
      numArray3[48 /*0x30*/] = (byte) 17;
      numArray3[51] = (byte) 62;
      numArray3[2] = (byte) 210;
      numArray3[22] = (byte) 160 /*0xA0*/;
      numArray3[24] = (byte) 68;
      numArray3[45] = (byte) 195;
      numArray3[54] = (byte) 211;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[18] = (byte) 73;
      numArray4[1] = (byte) 237;
      numArray4[35] = (byte) 121;
      numArray4[0] = (byte) 101;
      numArray4[53] = (byte) 11;
      numArray4[4] = (byte) 185;
      numArray4[6] = (byte) 110;
      numArray4[28] = (byte) 153;
      numArray4[46] = (byte) 136;
      numArray4[9] = (byte) 180;
      numArray4[43] = (byte) 27;
      numArray4[36] = (byte) 145;
      numArray4[52] = (byte) 69;
      numArray4[19] = (byte) 246;
      numArray4[7] = (byte) 166;
      numArray4[14] = (byte) 32 /*0x20*/;
      numArray4[44] = (byte) 148;
      numArray4[17] = (byte) 148;
      numArray4[31 /*0x1F*/] = (byte) 203;
      numArray4[47] = (byte) 34;
      numArray4[20] = (byte) 178;
      numArray4[5] = (byte) 48 /*0x30*/;
      numArray4[22] = (byte) 43;
      numArray4[23] = (byte) 102;
      numArray4[24] = (byte) 36;
      numArray4[8] = (byte) 22;
      numArray4[26] = (byte) 187;
      numArray4[27] = (byte) 241;
      numArray4[33] = (byte) 66;
      numArray4[29] = (byte) 194;
      numArray4[30] = (byte) 68;
      numArray4[13] = (byte) 140;
      numArray4[21] = (byte) 140;
      numArray4[16 /*0x10*/] = (byte) 84;
      numArray4[40] = (byte) 107;
      numArray4[34] = (byte) 185;
      numArray4[15] = (byte) 150;
      numArray4[37] = (byte) 31 /*0x1F*/;
      numArray4[38] = (byte) 246;
      numArray4[39] = (byte) 10;
      numArray4[54] = (byte) 251;
      numArray4[41] = (byte) 171;
      numArray4[42] = (byte) 132;
      numArray4[10] = (byte) 150;
      numArray4[50] = (byte) 62;
      numArray4[45] = (byte) 185;
      numArray4[3] = (byte) 112 /*0x70*/;
      numArray4[11] = (byte) 9;
      numArray4[48 /*0x30*/] = (byte) 0;
      numArray4[49] = (byte) 11;
      numArray4[25] = byte.MaxValue;
      numArray4[51] = (byte) 154;
      numArray4[2] = (byte) 180;
      numArray4[32 /*0x20*/] = (byte) 249;
      numArray4[12] = (byte) 145;
      byte[] numArray5 = new byte[55]
      {
        (byte) 71,
        (byte) 144 /*0x90*/,
        (byte) 7,
        (byte) 57,
        (byte) 26,
        (byte) 128 /*0x80*/,
        (byte) 250,
        (byte) 95,
        (byte) 30,
        (byte) 210,
        (byte) 24,
        (byte) 41,
        (byte) 219,
        (byte) 227,
        (byte) 112 /*0x70*/,
        (byte) 153,
        (byte) 79,
        (byte) 201,
        (byte) 194,
        (byte) 121,
        (byte) 23,
        (byte) 200,
        (byte) 146,
        (byte) 111,
        (byte) 146,
        (byte) 238,
        (byte) 18,
        (byte) 50,
        (byte) 3,
        (byte) 221,
        (byte) 200,
        (byte) 118,
        (byte) 38,
        (byte) 248,
        (byte) 239,
        (byte) 232,
        (byte) 89,
        (byte) 3,
        (byte) 16 /*0x10*/,
        (byte) 185,
        (byte) 100,
        (byte) 237,
        (byte) 107,
        (byte) 28,
        (byte) 178,
        (byte) 71,
        (byte) 250,
        (byte) 221,
        (byte) 40,
        (byte) 203,
        (byte) 31 /*0x1F*/,
        (byte) 63 /*0x3F*/,
        (byte) 79,
        (byte) 239,
        (byte) 143
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 123,
        (byte) 224 /*0xE0*/,
        (byte) 66,
        (byte) 220,
        (byte) 139,
        (byte) 127 /*0x7F*/,
        (byte) 207,
        (byte) 198,
        (byte) 68,
        (byte) 33,
        (byte) 7,
        (byte) 242,
        (byte) 240 /*0xF0*/,
        (byte) 192 /*0xC0*/,
        (byte) 96 /*0x60*/,
        (byte) 96 /*0x60*/,
        (byte) 168,
        (byte) 155,
        (byte) 104,
        (byte) 214,
        (byte) 100,
        (byte) 8,
        (byte) 193,
        (byte) 60,
        (byte) 73,
        (byte) 151,
        (byte) 135,
        (byte) 122,
        (byte) 172,
        (byte) 127 /*0x7F*/,
        (byte) 109,
        (byte) 81,
        (byte) 15,
        (byte) 4,
        (byte) 200,
        (byte) 243,
        (byte) 250,
        (byte) 185,
        (byte) 99,
        (byte) 30,
        (byte) 98,
        (byte) 228,
        (byte) 133,
        (byte) 135,
        (byte) 51,
        (byte) 93,
        (byte) 105,
        (byte) 70,
        (byte) 169,
        (byte) 156,
        (byte) 94,
        (byte) 229,
        (byte) 140,
        (byte) 250,
        (byte) 77
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 191,
        (byte) 165,
        (byte) 107,
        (byte) 108,
        (byte) 135,
        (byte) 124,
        (byte) 243,
        (byte) 91,
        (byte) 60,
        (byte) 75,
        (byte) 194,
        (byte) 76,
        (byte) 31 /*0x1F*/,
        (byte) 170,
        (byte) 246,
        (byte) 23,
        (byte) 244,
        (byte) 117,
        (byte) 102,
        (byte) 61,
        (byte) 34,
        (byte) 38,
        (byte) 118,
        (byte) 182,
        (byte) 38,
        (byte) 189,
        (byte) 220,
        (byte) 15,
        (byte) 248,
        (byte) 185,
        (byte) 197,
        (byte) 191,
        (byte) 61,
        (byte) 151,
        (byte) 227,
        (byte) 173,
        (byte) 242,
        (byte) 4,
        (byte) 160 /*0xA0*/,
        (byte) 166,
        (byte) 245,
        (byte) 218,
        (byte) 80 /*0x50*/,
        (byte) 27,
        (byte) 0,
        (byte) 75,
        (byte) 245,
        (byte) 44,
        (byte) 58,
        (byte) 50,
        (byte) 131,
        (byte) 49,
        (byte) 71,
        (byte) 43,
        (byte) 175
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 6,
        (byte) 147,
        (byte) 243,
        (byte) 248,
        (byte) 84,
        (byte) 26,
        (byte) 196,
        (byte) 180,
        (byte) 181,
        (byte) 226,
        (byte) 235,
        (byte) 247,
        (byte) 108,
        (byte) 101,
        (byte) 238,
        (byte) 56,
        (byte) 73,
        (byte) 116,
        (byte) 127 /*0x7F*/,
        (byte) 131,
        (byte) 58,
        (byte) 9,
        (byte) 235,
        (byte) 46,
        (byte) 65,
        (byte) 208 /*0xD0*/,
        (byte) 15,
        (byte) 33,
        (byte) 135,
        (byte) 196,
        (byte) 20,
        (byte) 183,
        (byte) 85,
        (byte) 213,
        (byte) 244,
        (byte) 38,
        (byte) 111,
        (byte) 38,
        (byte) 13,
        (byte) 202,
        (byte) 192 /*0xC0*/,
        (byte) 210,
        (byte) 125,
        (byte) 122,
        (byte) 125,
        (byte) 78,
        (byte) 61,
        (byte) 55,
        (byte) 111,
        (byte) 173,
        (byte) 67,
        (byte) 134,
        (byte) 206,
        (byte) 239,
        (byte) 227
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 47,
        (byte) 5,
        (byte) 177,
        (byte) 181,
        (byte) 43,
        (byte) 44,
        (byte) 193,
        (byte) 165,
        (byte) 152,
        (byte) 39,
        (byte) 28,
        (byte) 190,
        (byte) 64 /*0x40*/,
        (byte) 67,
        (byte) 86,
        (byte) 86,
        (byte) 228,
        (byte) 170,
        (byte) 37,
        (byte) 63 /*0x3F*/,
        (byte) 224 /*0xE0*/,
        (byte) 193,
        (byte) 28,
        (byte) 220,
        (byte) 136,
        (byte) 181,
        (byte) 116,
        (byte) 124,
        (byte) 172,
        (byte) 251,
        (byte) 193,
        (byte) 42,
        (byte) 200,
        (byte) 127 /*0x7F*/,
        (byte) 166,
        (byte) 180,
        (byte) 185,
        (byte) 13,
        (byte) 33,
        (byte) 4,
        (byte) 128 /*0x80*/,
        (byte) 187,
        (byte) 250,
        (byte) 117,
        (byte) 232,
        (byte) 93,
        (byte) 14,
        (byte) 20,
        (byte) 104,
        (byte) 39,
        (byte) 70,
        (byte) 214,
        (byte) 170,
        (byte) 170,
        (byte) 252
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[44];
      numArray10[26] = (byte) 218;
      numArray10[35] = (byte) 112 /*0x70*/;
      numArray10[20] = (byte) 113;
      numArray10[3] = (byte) 11;
      numArray10[25] = (byte) 177;
      numArray10[37] = (byte) 191;
      numArray10[36] = (byte) 136;
      numArray10[7] = (byte) 206;
      numArray10[41] = (byte) 171;
      numArray10[9] = (byte) 162;
      numArray10[39] = (byte) 165;
      numArray10[11] = (byte) 112 /*0x70*/;
      numArray10[12] = (byte) 200;
      numArray10[18] = (byte) 103;
      numArray10[13] = (byte) 200;
      numArray10[2] = (byte) 132;
      numArray10[38] = (byte) 29;
      numArray10[17] = (byte) 194;
      numArray10[14] = (byte) 34;
      numArray10[19] = (byte) 139;
      numArray10[8] = (byte) 51;
      numArray10[0] = (byte) 197;
      numArray10[10] = (byte) 151;
      numArray10[23] = (byte) 147;
      numArray10[24] = (byte) 216;
      numArray10[5] = (byte) 45;
      numArray10[4] = (byte) 163;
      numArray10[33] = (byte) 127 /*0x7F*/;
      numArray10[28] = (byte) 141;
      numArray10[6] = (byte) 240 /*0xF0*/;
      numArray10[30] = (byte) 101;
      numArray10[15] = (byte) 103;
      numArray10[32 /*0x20*/] = (byte) 247;
      numArray10[29] = (byte) 233;
      numArray10[34] = (byte) 170;
      numArray10[21] = (byte) 176 /*0xB0*/;
      numArray10[27] = (byte) 129;
      numArray10[22] = (byte) 242;
      numArray10[16 /*0x10*/] = (byte) 180;
      numArray10[31 /*0x1F*/] = (byte) 241;
      numArray10[40] = (byte) 141;
      numArray10[1] = (byte) 182;
      numArray10[42] = (byte) 73;
      numArray10[43] = (byte) 171;
      byte[] numArray11 = new byte[44]
      {
        (byte) 13,
        (byte) 27,
        (byte) 3,
        (byte) 200,
        (byte) 161,
        (byte) 83,
        (byte) 245,
        (byte) 40,
        (byte) 107,
        (byte) 111,
        (byte) 4,
        (byte) 28,
        (byte) 12,
        (byte) 186,
        (byte) 111,
        (byte) 243,
        (byte) 0,
        (byte) 197,
        (byte) 103,
        (byte) 142,
        (byte) 208 /*0xD0*/,
        (byte) 41,
        (byte) 55,
        (byte) 58,
        (byte) 147,
        (byte) 77,
        (byte) 203,
        (byte) 248,
        (byte) 102,
        (byte) 66,
        (byte) 226,
        (byte) 35,
        (byte) 198,
        (byte) 11,
        (byte) 28,
        (byte) 250,
        (byte) 201,
        (byte) 110,
        (byte) 100,
        (byte) 121,
        (byte) 124,
        (byte) 222,
        (byte) 234,
        (byte) 177
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[264];
    byte[] numArray13 = new byte[55]
    {
      (byte) 151,
      (byte) 140,
      (byte) 44,
      (byte) 82,
      (byte) 29,
      (byte) 106,
      byte.MaxValue,
      (byte) 111,
      (byte) 0,
      (byte) 77,
      (byte) 93,
      (byte) 198,
      (byte) 110,
      (byte) 65,
      (byte) 35,
      (byte) 135,
      (byte) 185,
      (byte) 115,
      (byte) 111,
      (byte) 174,
      (byte) 124,
      (byte) 3,
      (byte) 33,
      (byte) 229,
      (byte) 93,
      (byte) 15,
      (byte) 210,
      (byte) 122,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 243,
      (byte) 0,
      (byte) 136,
      (byte) 6,
      (byte) 70,
      (byte) 104,
      (byte) 121,
      (byte) 111,
      (byte) 224 /*0xE0*/,
      (byte) 90,
      (byte) 103,
      (byte) 215,
      (byte) 237,
      (byte) 196,
      (byte) 173,
      (byte) 46,
      (byte) 241,
      (byte) 44,
      (byte) 172,
      (byte) 177,
      (byte) 187,
      (byte) 18,
      (byte) 217,
      (byte) 231,
      (byte) 47
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 179,
      (byte) 171,
      (byte) 71,
      (byte) 115,
      (byte) 127 /*0x7F*/,
      (byte) 249,
      (byte) 83,
      (byte) 113,
      (byte) 185,
      (byte) 48 /*0x30*/,
      (byte) 248,
      (byte) 21,
      (byte) 121,
      (byte) 68,
      (byte) 125,
      (byte) 173,
      (byte) 177,
      (byte) 78,
      (byte) 235,
      (byte) 41,
      (byte) 155,
      (byte) 204,
      (byte) 203,
      (byte) 82,
      (byte) 190,
      (byte) 179,
      (byte) 62,
      (byte) 235,
      (byte) 199,
      (byte) 30,
      (byte) 92,
      (byte) 149,
      (byte) 6,
      (byte) 72,
      (byte) 218,
      (byte) 233,
      (byte) 133,
      (byte) 91,
      (byte) 29,
      (byte) 82,
      (byte) 202,
      (byte) 147,
      (byte) 163,
      (byte) 211,
      (byte) 77,
      (byte) 91,
      (byte) 233,
      (byte) 112 /*0x70*/,
      (byte) 155,
      (byte) 183,
      (byte) 69,
      (byte) 151,
      (byte) 217,
      (byte) 164,
      (byte) 88
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 70,
      (byte) 245,
      (byte) 13,
      (byte) 152,
      (byte) 132,
      (byte) 107,
      (byte) 207,
      (byte) 90,
      (byte) 92,
      (byte) 61,
      (byte) 104,
      (byte) 250,
      (byte) 186,
      (byte) 14,
      (byte) 74,
      (byte) 146,
      (byte) 21,
      (byte) 70,
      (byte) 165,
      (byte) 22,
      (byte) 92,
      (byte) 55,
      (byte) 46,
      (byte) 30,
      (byte) 165,
      (byte) 205,
      (byte) 82,
      (byte) 116,
      (byte) 224 /*0xE0*/,
      (byte) 38,
      (byte) 161,
      (byte) 214,
      (byte) 179,
      (byte) 253,
      (byte) 50,
      (byte) 121,
      (byte) 211,
      (byte) 194,
      (byte) 226,
      (byte) 107,
      (byte) 29,
      (byte) 106,
      (byte) 81,
      (byte) 241,
      (byte) 136,
      (byte) 50,
      (byte) 241,
      (byte) 162,
      (byte) 113,
      (byte) 127 /*0x7F*/,
      (byte) 196,
      (byte) 160 /*0xA0*/,
      (byte) 138,
      (byte) 124,
      (byte) 14
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 208 /*0xD0*/,
      (byte) 157,
      (byte) 140,
      (byte) 83,
      (byte) 231,
      (byte) 240 /*0xF0*/,
      (byte) 17,
      (byte) 58,
      (byte) 41,
      (byte) 224 /*0xE0*/,
      (byte) 236,
      (byte) 12,
      (byte) 114,
      (byte) 160 /*0xA0*/,
      (byte) 254,
      (byte) 186,
      (byte) 34,
      (byte) 118,
      (byte) 86,
      (byte) 226,
      (byte) 88,
      (byte) 95,
      (byte) 63 /*0x3F*/,
      (byte) 115,
      (byte) 56,
      (byte) 221,
      (byte) 17,
      (byte) 249,
      (byte) 224 /*0xE0*/,
      (byte) 201,
      (byte) 58,
      (byte) 48 /*0x30*/,
      (byte) 10,
      (byte) 182,
      (byte) 45,
      (byte) 67,
      (byte) 92,
      (byte) 190,
      (byte) 202,
      (byte) 27,
      (byte) 186,
      (byte) 17,
      (byte) 233,
      (byte) 108,
      (byte) 192 /*0xC0*/,
      (byte) 143,
      (byte) 242,
      (byte) 10,
      (byte) 150,
      (byte) 164,
      (byte) 224 /*0xE0*/,
      (byte) 219,
      (byte) 231,
      (byte) 122,
      (byte) 238
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 239,
      (byte) 242,
      (byte) 183,
      (byte) 24,
      (byte) 91,
      (byte) 121,
      (byte) 54,
      (byte) 156,
      (byte) 145,
      (byte) 70,
      (byte) 189,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 82,
      (byte) 100,
      (byte) 57,
      (byte) 248,
      (byte) 242,
      (byte) 230,
      (byte) 61,
      (byte) 149,
      (byte) 114,
      (byte) 219,
      (byte) 176 /*0xB0*/,
      (byte) 2,
      (byte) 97,
      (byte) 98,
      (byte) 0,
      (byte) 76,
      (byte) 54,
      (byte) 238,
      (byte) 36,
      (byte) 178,
      (byte) 227,
      (byte) 207,
      (byte) 43,
      (byte) 82,
      (byte) 194,
      (byte) 240 /*0xF0*/,
      (byte) 26,
      (byte) 84,
      (byte) 247,
      (byte) 236,
      (byte) 52,
      (byte) 198,
      (byte) 160 /*0xA0*/,
      (byte) 191,
      (byte) 113,
      (byte) 60,
      (byte) 80 /*0x50*/,
      (byte) 131,
      (byte) 27,
      (byte) 39,
      (byte) 138
    };
    byte[] numArray18 = new byte[55];
    numArray18[38] = (byte) 205;
    numArray18[1] = (byte) 42;
    numArray18[28] = (byte) 202;
    numArray18[13] = (byte) 55;
    numArray18[4] = (byte) 146;
    numArray18[36] = (byte) 226;
    numArray18[15] = (byte) 187;
    numArray18[7] = (byte) 71;
    numArray18[25] = (byte) 82;
    numArray18[6] = (byte) 158;
    numArray18[10] = (byte) 96 /*0x60*/;
    numArray18[5] = (byte) 116;
    numArray18[40] = (byte) 109;
    numArray18[22] = (byte) 232;
    numArray18[0] = (byte) 35;
    numArray18[18] = (byte) 21;
    numArray18[39] = (byte) 75;
    numArray18[17] = (byte) 210;
    numArray18[37] = (byte) 166;
    numArray18[3] = (byte) 232;
    numArray18[20] = (byte) 20;
    numArray18[47] = (byte) 64 /*0x40*/;
    numArray18[30] = (byte) 122;
    numArray18[23] = (byte) 107;
    numArray18[24] = (byte) 111;
    numArray18[44] = (byte) 101;
    numArray18[2] = (byte) 80 /*0x50*/;
    numArray18[27] = (byte) 94;
    numArray18[11] = (byte) 63 /*0x3F*/;
    numArray18[12] = (byte) 43;
    numArray18[29] = (byte) 80 /*0x50*/;
    numArray18[31 /*0x1F*/] = (byte) 14;
    numArray18[34] = (byte) 123;
    numArray18[33] = (byte) 179;
    numArray18[9] = (byte) 169;
    numArray18[35] = (byte) 187;
    numArray18[16 /*0x10*/] = (byte) 99;
    numArray18[48 /*0x30*/] = (byte) 13;
    numArray18[51] = (byte) 113;
    numArray18[32 /*0x20*/] = (byte) 251;
    numArray18[8] = (byte) 142;
    numArray18[41] = (byte) 248;
    numArray18[42] = (byte) 92;
    numArray18[43] = (byte) 48 /*0x30*/;
    numArray18[49] = (byte) 41;
    numArray18[45] = (byte) 134;
    numArray18[46] = (byte) 214;
    numArray18[21] = (byte) 125;
    numArray18[14] = (byte) 93;
    numArray18[26] = (byte) 137;
    numArray18[50] = (byte) 97;
    numArray18[53] = (byte) 28;
    numArray18[52] = (byte) 229;
    numArray18[19] = (byte) 62;
    numArray18[54] = (byte) 4;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 51,
      (byte) 127 /*0x7F*/,
      (byte) 246,
      (byte) 118,
      (byte) 226,
      (byte) 214,
      (byte) 72,
      (byte) 92,
      (byte) 252,
      (byte) 186,
      (byte) 246,
      (byte) 83,
      (byte) 50,
      (byte) 233,
      (byte) 252,
      (byte) 204,
      (byte) 41,
      (byte) 132,
      (byte) 46,
      (byte) 234,
      (byte) 170,
      (byte) 65,
      (byte) 217,
      (byte) 223,
      (byte) 189,
      (byte) 2,
      (byte) 194,
      (byte) 145,
      (byte) 101,
      (byte) 185,
      (byte) 161,
      (byte) 133,
      (byte) 204,
      (byte) 87,
      (byte) 28,
      (byte) 106,
      (byte) 115,
      (byte) 97,
      (byte) 100,
      (byte) 226,
      (byte) 106,
      (byte) 127 /*0x7F*/,
      (byte) 105,
      (byte) 85,
      (byte) 139,
      (byte) 144 /*0x90*/,
      (byte) 188,
      (byte) 25,
      (byte) 110,
      (byte) 128 /*0x80*/,
      (byte) 239,
      (byte) 214,
      (byte) 115,
      (byte) 209,
      (byte) 78
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 212,
      (byte) 193,
      (byte) 95,
      (byte) 22,
      (byte) 154,
      (byte) 137,
      (byte) 195,
      (byte) 210,
      (byte) 143,
      (byte) 110,
      (byte) 35,
      (byte) 46,
      (byte) 172,
      (byte) 253,
      (byte) 104,
      (byte) 246,
      (byte) 236,
      (byte) 148,
      (byte) 172,
      (byte) 103,
      (byte) 158,
      (byte) 183,
      (byte) 139,
      (byte) 197,
      (byte) 151,
      (byte) 202,
      (byte) 193,
      (byte) 156,
      (byte) 183,
      (byte) 245,
      (byte) 23,
      (byte) 35,
      (byte) 4,
      (byte) 12,
      (byte) 76,
      (byte) 170,
      (byte) 62,
      (byte) 63 /*0x3F*/,
      (byte) 165,
      (byte) 58,
      (byte) 180,
      (byte) 156,
      (byte) 209,
      (byte) 116,
      (byte) 160 /*0xA0*/,
      (byte) 82,
      (byte) 221,
      byte.MaxValue,
      (byte) 38,
      (byte) 117,
      (byte) 143,
      (byte) 206,
      (byte) 183,
      (byte) 65,
      (byte) 190
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[44];
    numArray21[9] = (byte) 134;
    numArray21[1] = (byte) 184;
    numArray21[2] = (byte) 224 /*0xE0*/;
    numArray21[3] = (byte) 20;
    numArray21[4] = (byte) 183;
    numArray21[28] = (byte) 203;
    numArray21[39] = (byte) 135;
    numArray21[40] = (byte) 38;
    numArray21[7] = (byte) 24;
    numArray21[23] = (byte) 70;
    numArray21[42] = (byte) 25;
    numArray21[33] = (byte) 196;
    numArray21[12] = (byte) 145;
    numArray21[13] = (byte) 88;
    numArray21[0] = (byte) 22;
    numArray21[15] = (byte) 32 /*0x20*/;
    numArray21[16 /*0x10*/] = (byte) 76;
    numArray21[17] = (byte) 128 /*0x80*/;
    numArray21[6] = (byte) 67;
    numArray21[38] = (byte) 179;
    numArray21[20] = (byte) 131;
    numArray21[18] = (byte) 43;
    numArray21[22] = (byte) 45;
    numArray21[30] = (byte) 250;
    numArray21[24] = (byte) 168;
    numArray21[29] = (byte) 17;
    numArray21[14] = (byte) 199;
    numArray21[27] = (byte) 201;
    numArray21[8] = (byte) 113;
    numArray21[25] = (byte) 34;
    numArray21[26] = (byte) 228;
    numArray21[31 /*0x1F*/] = (byte) 9;
    numArray21[32 /*0x20*/] = (byte) 41;
    numArray21[19] = (byte) 216;
    numArray21[34] = (byte) 93;
    numArray21[35] = (byte) 169;
    numArray21[36] = (byte) 131;
    numArray21[5] = (byte) 247;
    numArray21[10] = (byte) 219;
    numArray21[21] = (byte) 84;
    numArray21[37] = (byte) 205;
    numArray21[11] = (byte) 205;
    numArray21[43] = (byte) 9;
    numArray21[41] = (byte) 61;
    byte[] numArray22 = new byte[44];
    numArray22[30] = (byte) 70;
    numArray22[26] = (byte) 2;
    numArray22[18] = (byte) 38;
    numArray22[3] = (byte) 35;
    numArray22[41] = (byte) 93;
    numArray22[5] = (byte) 13;
    numArray22[7] = (byte) 57;
    numArray22[24] = (byte) 108;
    numArray22[1] = (byte) 90;
    numArray22[0] = (byte) 3;
    numArray22[28] = (byte) 223;
    numArray22[11] = (byte) 160 /*0xA0*/;
    numArray22[35] = (byte) 75;
    numArray22[6] = (byte) 199;
    numArray22[17] = (byte) 103;
    numArray22[36] = (byte) 77;
    numArray22[16 /*0x10*/] = (byte) 42;
    numArray22[27] = (byte) 171;
    numArray22[19] = (byte) 42;
    numArray22[14] = (byte) 202;
    numArray22[20] = (byte) 97;
    numArray22[4] = (byte) 74;
    numArray22[38] = (byte) 135;
    numArray22[23] = (byte) 13;
    numArray22[31 /*0x1F*/] = (byte) 2;
    numArray22[25] = (byte) 164;
    numArray22[34] = (byte) 58;
    numArray22[9] = (byte) 27;
    numArray22[2] = (byte) 29;
    numArray22[32 /*0x20*/] = (byte) 122;
    numArray22[12] = (byte) 180;
    numArray22[43] = (byte) 221;
    numArray22[29] = (byte) 101;
    numArray22[33] = (byte) 244;
    numArray22[13] = (byte) 74;
    numArray22[21] = (byte) 58;
    numArray22[15] = (byte) 7;
    numArray22[37] = (byte) 93;
    numArray22[8] = (byte) 136;
    numArray22[22] = (byte) 38;
    numArray22[40] = (byte) 197;
    numArray22[39] = (byte) 28;
    numArray22[42] = (byte) 100;
    numArray22[10] = (byte) 250;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 44);
    for (int index = 0; index < 44; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12589()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[263];
      byte[] numArray2 = new byte[55]
      {
        (byte) 142,
        (byte) 151,
        (byte) 252,
        (byte) 66,
        (byte) 37,
        (byte) 104,
        (byte) 167,
        (byte) 198,
        (byte) 20,
        (byte) 182,
        (byte) 179,
        (byte) 82,
        (byte) 207,
        (byte) 30,
        (byte) 211,
        (byte) 184,
        (byte) 69,
        (byte) 154,
        (byte) 36,
        (byte) 20,
        (byte) 228,
        (byte) 206,
        (byte) 8,
        (byte) 101,
        (byte) 139,
        (byte) 213,
        (byte) 80 /*0x50*/,
        (byte) 99,
        (byte) 133,
        (byte) 6,
        (byte) 192 /*0xC0*/,
        (byte) 223,
        (byte) 89,
        (byte) 25,
        (byte) 199,
        (byte) 64 /*0x40*/,
        (byte) 59,
        (byte) 50,
        (byte) 189,
        (byte) 130,
        (byte) 236,
        (byte) 100,
        (byte) 74,
        (byte) 73,
        byte.MaxValue,
        (byte) 202,
        (byte) 223,
        (byte) 15,
        (byte) 49,
        (byte) 157,
        (byte) 59,
        (byte) 11,
        (byte) 49,
        (byte) 160 /*0xA0*/,
        (byte) 81
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 207,
        (byte) 37,
        (byte) 95,
        (byte) 249,
        (byte) 171,
        (byte) 48 /*0x30*/,
        (byte) 16 /*0x10*/,
        (byte) 131,
        (byte) 60,
        (byte) 186,
        (byte) 47,
        (byte) 236,
        (byte) 70,
        (byte) 50,
        (byte) 134,
        (byte) 46,
        (byte) 53,
        (byte) 1,
        (byte) 60,
        (byte) 30,
        (byte) 217,
        (byte) 168,
        (byte) 228,
        (byte) 115,
        (byte) 186,
        (byte) 246,
        (byte) 60,
        (byte) 90,
        (byte) 175,
        (byte) 247,
        byte.MaxValue,
        (byte) 197,
        (byte) 121,
        (byte) 150,
        (byte) 164,
        (byte) 2,
        (byte) 94,
        (byte) 64 /*0x40*/,
        (byte) 238,
        (byte) 90,
        (byte) 245,
        (byte) 58,
        (byte) 67,
        (byte) 30,
        (byte) 39,
        (byte) 68,
        (byte) 99,
        (byte) 227,
        (byte) 34,
        (byte) 41,
        (byte) 12,
        (byte) 103,
        (byte) 35,
        (byte) 23,
        (byte) 134
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 150,
        (byte) 182,
        (byte) 80 /*0x50*/,
        (byte) 23,
        (byte) 68,
        (byte) 64 /*0x40*/,
        (byte) 14,
        (byte) 245,
        (byte) 212,
        (byte) 249,
        (byte) 182,
        (byte) 87,
        (byte) 146,
        (byte) 125,
        (byte) 18,
        (byte) 221,
        (byte) 161,
        (byte) 40,
        (byte) 127 /*0x7F*/,
        (byte) 131,
        (byte) 190,
        (byte) 101,
        (byte) 193,
        (byte) 245,
        (byte) 65,
        (byte) 49,
        (byte) 232,
        (byte) 202,
        (byte) 181,
        (byte) 174,
        (byte) 184,
        (byte) 199,
        (byte) 7,
        (byte) 177,
        (byte) 164,
        (byte) 123,
        (byte) 109,
        (byte) 107,
        (byte) 167,
        (byte) 35,
        (byte) 60,
        (byte) 84,
        (byte) 43,
        (byte) 209,
        (byte) 41,
        (byte) 183,
        (byte) 159,
        (byte) 31 /*0x1F*/,
        (byte) 41,
        (byte) 46,
        (byte) 143,
        (byte) 120,
        (byte) 209,
        (byte) 60,
        (byte) 209
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 101,
        (byte) 243,
        (byte) 249,
        (byte) 86,
        (byte) 115,
        (byte) 133,
        (byte) 32 /*0x20*/,
        (byte) 188,
        (byte) 216,
        (byte) 117,
        (byte) 93,
        (byte) 9,
        (byte) 235,
        (byte) 183,
        (byte) 185,
        (byte) 49,
        (byte) 178,
        (byte) 98,
        (byte) 36,
        (byte) 125,
        (byte) 20,
        (byte) 156,
        (byte) 240 /*0xF0*/,
        (byte) 177,
        (byte) 245,
        (byte) 157,
        (byte) 244,
        (byte) 226,
        (byte) 247,
        (byte) 66,
        (byte) 209,
        (byte) 22,
        (byte) 88,
        (byte) 49,
        (byte) 184,
        (byte) 164,
        (byte) 186,
        (byte) 81,
        (byte) 237,
        (byte) 28,
        (byte) 191,
        (byte) 15,
        (byte) 129,
        (byte) 163,
        (byte) 240 /*0xF0*/,
        (byte) 163,
        (byte) 111,
        (byte) 177,
        (byte) 15,
        (byte) 112 /*0x70*/,
        (byte) 203,
        (byte) 203,
        (byte) 206,
        (byte) 2,
        (byte) 219
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 90,
        (byte) 214,
        (byte) 19,
        (byte) 88,
        (byte) 67,
        (byte) 64 /*0x40*/,
        (byte) 38,
        (byte) 64 /*0x40*/,
        (byte) 238,
        (byte) 80 /*0x50*/,
        (byte) 225,
        (byte) 217,
        (byte) 150,
        (byte) 111,
        (byte) 54,
        (byte) 128 /*0x80*/,
        (byte) 91,
        (byte) 71,
        (byte) 106,
        (byte) 58,
        (byte) 240 /*0xF0*/,
        (byte) 204,
        (byte) 137,
        (byte) 146,
        (byte) 200,
        (byte) 161,
        (byte) 135,
        (byte) 30,
        (byte) 63 /*0x3F*/,
        (byte) 246,
        (byte) 202,
        (byte) 224 /*0xE0*/,
        (byte) 115,
        (byte) 134,
        (byte) 19,
        (byte) 148,
        (byte) 148,
        (byte) 203,
        (byte) 248,
        (byte) 19,
        (byte) 4,
        (byte) 144 /*0x90*/,
        (byte) 58,
        (byte) 103,
        (byte) 189,
        (byte) 70,
        (byte) 146,
        (byte) 190,
        (byte) 88,
        (byte) 248,
        (byte) 117,
        (byte) 245,
        (byte) 102,
        (byte) 246,
        (byte) 5
      };
      byte[] numArray7 = new byte[55];
      numArray7[51] = (byte) 44;
      numArray7[32 /*0x20*/] = (byte) 240 /*0xF0*/;
      numArray7[2] = (byte) 52;
      numArray7[10] = (byte) 197;
      numArray7[45] = (byte) 243;
      numArray7[5] = (byte) 169;
      numArray7[40] = (byte) 168;
      numArray7[7] = (byte) 9;
      numArray7[8] = (byte) 73;
      numArray7[38] = (byte) 176 /*0xB0*/;
      numArray7[17] = (byte) 52;
      numArray7[44] = (byte) 168;
      numArray7[12] = (byte) 21;
      numArray7[6] = (byte) 9;
      numArray7[46] = (byte) 37;
      numArray7[15] = (byte) 178;
      numArray7[16 /*0x10*/] = (byte) 74;
      numArray7[1] = (byte) 31 /*0x1F*/;
      numArray7[33] = (byte) 56;
      numArray7[19] = (byte) 55;
      numArray7[52] = (byte) 64 /*0x40*/;
      numArray7[4] = (byte) 81;
      numArray7[34] = (byte) 221;
      numArray7[23] = (byte) 53;
      numArray7[24] = (byte) 165;
      numArray7[25] = (byte) 61;
      numArray7[47] = (byte) 119;
      numArray7[27] = (byte) 235;
      numArray7[49] = (byte) 187;
      numArray7[37] = (byte) 58;
      numArray7[30] = (byte) 111;
      numArray7[28] = (byte) 227;
      numArray7[0] = (byte) 155;
      numArray7[36] = (byte) 25;
      numArray7[26] = (byte) 139;
      numArray7[35] = (byte) 61;
      numArray7[29] = (byte) 67;
      numArray7[21] = (byte) 181;
      numArray7[11] = (byte) 43;
      numArray7[18] = (byte) 94;
      numArray7[22] = (byte) 162;
      numArray7[41] = (byte) 62;
      numArray7[42] = (byte) 246;
      numArray7[43] = (byte) 75;
      numArray7[39] = (byte) 31 /*0x1F*/;
      numArray7[20] = (byte) 79;
      numArray7[13] = (byte) 42;
      numArray7[3] = (byte) 141;
      numArray7[48 /*0x30*/] = (byte) 109;
      numArray7[14] = byte.MaxValue;
      numArray7[50] = (byte) 42;
      numArray7[9] = (byte) 197;
      numArray7[31 /*0x1F*/] = (byte) 182;
      numArray7[53] = (byte) 97;
      numArray7[54] = (byte) 138;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 172,
        (byte) 12,
        (byte) 147,
        (byte) 239,
        (byte) 65,
        (byte) 184,
        (byte) 119,
        (byte) 177,
        (byte) 135,
        (byte) 233,
        (byte) 143,
        (byte) 215,
        (byte) 89,
        (byte) 217,
        (byte) 84,
        (byte) 248,
        (byte) 251,
        (byte) 56,
        (byte) 207,
        (byte) 79,
        (byte) 63 /*0x3F*/,
        (byte) 23,
        (byte) 162,
        (byte) 141,
        (byte) 103,
        (byte) 15,
        (byte) 59,
        (byte) 155,
        (byte) 45,
        (byte) 105,
        (byte) 187,
        (byte) 219,
        (byte) 136,
        (byte) 28,
        (byte) 102,
        (byte) 47,
        (byte) 195,
        (byte) 107,
        (byte) 187,
        (byte) 215,
        (byte) 202,
        (byte) 49,
        (byte) 53,
        (byte) 246,
        (byte) 242,
        (byte) 138,
        (byte) 92,
        (byte) 50,
        (byte) 144 /*0x90*/,
        (byte) 175,
        (byte) 20,
        (byte) 136,
        (byte) 214,
        (byte) 173,
        (byte) 236
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 35,
        (byte) 230,
        (byte) 141,
        (byte) 52,
        (byte) 91,
        (byte) 145,
        (byte) 191,
        (byte) 133,
        (byte) 243,
        (byte) 162,
        (byte) 172,
        (byte) 57,
        (byte) 254,
        (byte) 226,
        (byte) 189,
        (byte) 199,
        (byte) 47,
        (byte) 223,
        (byte) 226,
        (byte) 212,
        (byte) 51,
        (byte) 33,
        (byte) 48 /*0x30*/,
        (byte) 7,
        (byte) 179,
        (byte) 214,
        (byte) 141,
        (byte) 245,
        (byte) 210,
        (byte) 175,
        (byte) 100,
        (byte) 76,
        (byte) 206,
        (byte) 62,
        (byte) 204,
        (byte) 161,
        (byte) 219,
        (byte) 201,
        (byte) 140,
        (byte) 115,
        (byte) 180,
        (byte) 71,
        (byte) 186,
        (byte) 233,
        (byte) 190,
        (byte) 104,
        (byte) 127 /*0x7F*/,
        (byte) 74,
        (byte) 25,
        (byte) 233,
        (byte) 165,
        (byte) 33,
        (byte) 185,
        (byte) 215,
        (byte) 149
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[43]
      {
        (byte) 5,
        (byte) 136,
        (byte) 49,
        (byte) 125,
        (byte) 219,
        (byte) 131,
        (byte) 30,
        (byte) 45,
        (byte) 180,
        (byte) 78,
        (byte) 61,
        (byte) 114,
        (byte) 37,
        (byte) 52,
        (byte) 144 /*0x90*/,
        (byte) 124,
        (byte) 248,
        (byte) 132,
        (byte) 14,
        (byte) 226,
        (byte) 172,
        (byte) 155,
        (byte) 221,
        (byte) 178,
        (byte) 9,
        (byte) 11,
        (byte) 125,
        (byte) 84,
        (byte) 50,
        (byte) 5,
        (byte) 219,
        (byte) 48 /*0x30*/,
        (byte) 209,
        (byte) 196,
        (byte) 189,
        (byte) 226,
        (byte) 137,
        (byte) 90,
        (byte) 238,
        (byte) 173,
        (byte) 22,
        (byte) 200,
        (byte) 61
      };
      byte[] numArray11 = new byte[43];
      numArray11[28] = (byte) 155;
      numArray11[1] = (byte) 64 /*0x40*/;
      numArray11[2] = (byte) 66;
      numArray11[36] = (byte) 242;
      numArray11[25] = (byte) 58;
      numArray11[38] = (byte) 236;
      numArray11[8] = (byte) 121;
      numArray11[7] = (byte) 11;
      numArray11[19] = (byte) 123;
      numArray11[24] = (byte) 47;
      numArray11[10] = (byte) 5;
      numArray11[26] = (byte) 188;
      numArray11[12] = (byte) 58;
      numArray11[13] = (byte) 217;
      numArray11[4] = (byte) 16 /*0x10*/;
      numArray11[29] = (byte) 113;
      numArray11[16 /*0x10*/] = (byte) 100;
      numArray11[17] = (byte) 229;
      numArray11[5] = (byte) 154;
      numArray11[37] = (byte) 44;
      numArray11[20] = (byte) 93;
      numArray11[40] = (byte) 52;
      numArray11[34] = (byte) 250;
      numArray11[23] = (byte) 68;
      numArray11[33] = (byte) 135;
      numArray11[15] = (byte) 101;
      numArray11[27] = (byte) 73;
      numArray11[42] = (byte) 63 /*0x3F*/;
      numArray11[31 /*0x1F*/] = (byte) 65;
      numArray11[11] = (byte) 172;
      numArray11[35] = (byte) 1;
      numArray11[41] = (byte) 174;
      numArray11[32 /*0x20*/] = (byte) 77;
      numArray11[14] = (byte) 212;
      numArray11[6] = (byte) 204;
      numArray11[30] = (byte) 103;
      numArray11[18] = (byte) 177;
      numArray11[3] = (byte) 169;
      numArray11[21] = (byte) 45;
      numArray11[39] = (byte) 189;
      numArray11[9] = (byte) 186;
      numArray11[22] = (byte) 59;
      numArray11[0] = (byte) 65;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[263];
    byte[] numArray13 = new byte[55]
    {
      (byte) 82,
      (byte) 55,
      (byte) 11,
      (byte) 230,
      (byte) 30,
      (byte) 167,
      (byte) 77,
      (byte) 205,
      (byte) 142,
      (byte) 199,
      (byte) 19,
      (byte) 31 /*0x1F*/,
      (byte) 9,
      (byte) 2,
      (byte) 213,
      (byte) 25,
      (byte) 62,
      (byte) 147,
      (byte) 125,
      (byte) 231,
      (byte) 219,
      (byte) 13,
      (byte) 9,
      (byte) 235,
      (byte) 42,
      (byte) 48 /*0x30*/,
      (byte) 2,
      (byte) 248,
      (byte) 212,
      (byte) 135,
      (byte) 192 /*0xC0*/,
      (byte) 186,
      (byte) 129,
      (byte) 90,
      (byte) 37,
      (byte) 118,
      (byte) 231,
      (byte) 184,
      (byte) 119,
      (byte) 185,
      (byte) 126,
      (byte) 21,
      (byte) 207,
      (byte) 218,
      (byte) 127 /*0x7F*/,
      (byte) 41,
      (byte) 150,
      (byte) 0,
      (byte) 251,
      (byte) 13,
      (byte) 233,
      (byte) 31 /*0x1F*/,
      (byte) 212,
      (byte) 55,
      (byte) 43
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 147,
      (byte) 225,
      (byte) 207,
      (byte) 249,
      (byte) 143,
      (byte) 107,
      (byte) 72,
      (byte) 243,
      (byte) 133,
      (byte) 84,
      (byte) 253,
      (byte) 150,
      (byte) 105,
      (byte) 238,
      (byte) 55,
      (byte) 17,
      (byte) 218,
      (byte) 233,
      (byte) 2,
      (byte) 47,
      (byte) 200,
      (byte) 234,
      (byte) 214,
      (byte) 67,
      (byte) 70,
      (byte) 24,
      (byte) 110,
      (byte) 71,
      (byte) 142,
      (byte) 212,
      (byte) 54,
      (byte) 218,
      (byte) 87,
      (byte) 181,
      (byte) 29,
      (byte) 160 /*0xA0*/,
      (byte) 177,
      (byte) 138,
      (byte) 241,
      (byte) 129,
      (byte) 230,
      (byte) 76,
      (byte) 124,
      (byte) 132,
      (byte) 156,
      (byte) 107,
      (byte) 59,
      (byte) 148,
      (byte) 155,
      (byte) 24,
      (byte) 61,
      (byte) 244,
      (byte) 43,
      (byte) 61,
      (byte) 169
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[31 /*0x1F*/] = (byte) 46;
    numArray15[12] = (byte) 95;
    numArray15[30] = (byte) 74;
    numArray15[18] = (byte) 197;
    numArray15[4] = (byte) 85;
    numArray15[41] = (byte) 211;
    numArray15[52] = (byte) 101;
    numArray15[17] = (byte) 105;
    numArray15[0] = (byte) 31 /*0x1F*/;
    numArray15[16 /*0x10*/] = (byte) 195;
    numArray15[32 /*0x20*/] = (byte) 223;
    numArray15[11] = (byte) 221;
    numArray15[26] = (byte) 198;
    numArray15[13] = (byte) 49;
    numArray15[24] = (byte) 1;
    numArray15[33] = (byte) 71;
    numArray15[46] = (byte) 156;
    numArray15[38] = (byte) 40;
    numArray15[28] = (byte) 161;
    numArray15[19] = (byte) 62;
    numArray15[14] = (byte) 134;
    numArray15[3] = (byte) 164;
    numArray15[22] = (byte) 1;
    numArray15[23] = (byte) 153;
    numArray15[25] = (byte) 252;
    numArray15[49] = (byte) 4;
    numArray15[2] = (byte) 238;
    numArray15[27] = (byte) 158;
    numArray15[21] = (byte) 107;
    numArray15[29] = (byte) 229;
    numArray15[8] = (byte) 88;
    numArray15[15] = (byte) 27;
    numArray15[35] = (byte) 253;
    numArray15[50] = (byte) 16 /*0x10*/;
    numArray15[34] = (byte) 200;
    numArray15[10] = (byte) 175;
    numArray15[6] = (byte) 6;
    numArray15[1] = (byte) 187;
    numArray15[51] = (byte) 206;
    numArray15[39] = (byte) 27;
    numArray15[40] = (byte) 200;
    numArray15[37] = (byte) 20;
    numArray15[42] = (byte) 39;
    numArray15[43] = (byte) 140;
    numArray15[44] = (byte) 230;
    numArray15[45] = (byte) 35;
    numArray15[5] = (byte) 249;
    numArray15[47] = (byte) 219;
    numArray15[48 /*0x30*/] = (byte) 98;
    numArray15[9] = (byte) 157;
    numArray15[20] = (byte) 47;
    numArray15[36] = (byte) 137;
    numArray15[7] = (byte) 6;
    numArray15[53] = (byte) 6;
    numArray15[54] = byte.MaxValue;
    byte[] numArray16 = new byte[55]
    {
      (byte) 7,
      (byte) 202,
      (byte) 197,
      (byte) 147,
      (byte) 94,
      (byte) 233,
      (byte) 214,
      (byte) 18,
      (byte) 182,
      (byte) 209,
      (byte) 68,
      (byte) 170,
      (byte) 211,
      (byte) 71,
      (byte) 173,
      (byte) 208 /*0xD0*/,
      (byte) 88,
      (byte) 232,
      (byte) 86,
      (byte) 241,
      (byte) 50,
      (byte) 184,
      (byte) 232,
      (byte) 17,
      (byte) 4,
      (byte) 228,
      (byte) 147,
      (byte) 109,
      (byte) 107,
      (byte) 210,
      (byte) 143,
      (byte) 203,
      (byte) 167,
      (byte) 3,
      (byte) 241,
      (byte) 249,
      (byte) 149,
      (byte) 178,
      (byte) 174,
      (byte) 38,
      (byte) 226,
      (byte) 12,
      (byte) 244,
      (byte) 183,
      (byte) 155,
      (byte) 46,
      (byte) 178,
      (byte) 47,
      (byte) 214,
      (byte) 80 /*0x50*/,
      (byte) 67,
      (byte) 226,
      (byte) 220,
      (byte) 235,
      (byte) 39
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[51] = (byte) 125;
    numArray17[48 /*0x30*/] = (byte) 36;
    numArray17[2] = (byte) 248;
    numArray17[21] = (byte) 76;
    numArray17[4] = (byte) 44;
    numArray17[5] = (byte) 200;
    numArray17[6] = (byte) 158;
    numArray17[16 /*0x10*/] = (byte) 194;
    numArray17[8] = (byte) 184;
    numArray17[39] = (byte) 165;
    numArray17[10] = (byte) 207;
    numArray17[23] = (byte) 219;
    numArray17[12] = (byte) 70;
    numArray17[43] = (byte) 0;
    numArray17[14] = (byte) 61;
    numArray17[26] = (byte) 43;
    numArray17[1] = (byte) 196;
    numArray17[50] = (byte) 248;
    numArray17[17] = (byte) 20;
    numArray17[13] = (byte) 93;
    numArray17[18] = (byte) 238;
    numArray17[28] = (byte) 237;
    numArray17[33] = (byte) 253;
    numArray17[15] = (byte) 111;
    numArray17[24] = (byte) 120;
    numArray17[25] = (byte) 97;
    numArray17[30] = (byte) 71;
    numArray17[52] = (byte) 180;
    numArray17[0] = (byte) 162;
    numArray17[29] = (byte) 13;
    numArray17[32 /*0x20*/] = (byte) 165;
    numArray17[19] = (byte) 145;
    numArray17[27] = (byte) 114;
    numArray17[7] = (byte) 215;
    numArray17[53] = (byte) 221;
    numArray17[44] = (byte) 181;
    numArray17[36] = (byte) 216;
    numArray17[37] = (byte) 160 /*0xA0*/;
    numArray17[38] = (byte) 52;
    numArray17[22] = (byte) 185;
    numArray17[40] = (byte) 20;
    numArray17[35] = (byte) 172;
    numArray17[42] = (byte) 172;
    numArray17[9] = (byte) 36;
    numArray17[41] = (byte) 97;
    numArray17[20] = (byte) 60;
    numArray17[46] = (byte) 147;
    numArray17[47] = (byte) 20;
    numArray17[11] = (byte) 84;
    numArray17[31 /*0x1F*/] = (byte) 165;
    numArray17[45] = (byte) 29;
    numArray17[49] = (byte) 30;
    numArray17[3] = (byte) 72;
    numArray17[34] = (byte) 146;
    numArray17[54] = (byte) 9;
    byte[] numArray18 = new byte[55];
    numArray18[48 /*0x30*/] = (byte) 211;
    numArray18[28] = (byte) 11;
    numArray18[31 /*0x1F*/] = (byte) 105;
    numArray18[44] = (byte) 241;
    numArray18[4] = (byte) 186;
    numArray18[5] = (byte) 205;
    numArray18[6] = (byte) 44;
    numArray18[23] = (byte) 202;
    numArray18[7] = (byte) 64 /*0x40*/;
    numArray18[9] = (byte) 130;
    numArray18[8] = (byte) 190;
    numArray18[15] = (byte) 219;
    numArray18[12] = (byte) 174;
    numArray18[13] = (byte) 63 /*0x3F*/;
    numArray18[14] = (byte) 197;
    numArray18[37] = (byte) 111;
    numArray18[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray18[33] = (byte) 67;
    numArray18[3] = (byte) 46;
    numArray18[17] = (byte) 42;
    numArray18[25] = (byte) 17;
    numArray18[40] = (byte) 239;
    numArray18[27] = (byte) 59;
    numArray18[49] = (byte) 97;
    numArray18[24] = (byte) 250;
    numArray18[10] = (byte) 71;
    numArray18[26] = (byte) 88;
    numArray18[19] = (byte) 59;
    numArray18[35] = (byte) 114;
    numArray18[0] = (byte) 186;
    numArray18[30] = (byte) 77;
    numArray18[32 /*0x20*/] = (byte) 220;
    numArray18[41] = (byte) 78;
    numArray18[20] = (byte) 39;
    numArray18[21] = (byte) 199;
    numArray18[50] = (byte) 60;
    numArray18[36] = (byte) 56;
    numArray18[18] = (byte) 228;
    numArray18[38] = (byte) 240 /*0xF0*/;
    numArray18[1] = (byte) 223;
    numArray18[22] = (byte) 28;
    numArray18[39] = (byte) 74;
    numArray18[42] = (byte) 211;
    numArray18[43] = (byte) 226;
    numArray18[29] = (byte) 144 /*0x90*/;
    numArray18[54] = (byte) 89;
    numArray18[46] = (byte) 249;
    numArray18[47] = (byte) 49;
    numArray18[11] = (byte) 188;
    numArray18[45] = (byte) 232;
    numArray18[34] = (byte) 111;
    numArray18[51] = (byte) 65;
    numArray18[52] = (byte) 62;
    numArray18[53] = (byte) 142;
    numArray18[2] = (byte) 17;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 91,
      (byte) 136,
      (byte) 120,
      (byte) 185,
      (byte) 33,
      (byte) 210,
      (byte) 89,
      (byte) 230,
      (byte) 181,
      (byte) 129,
      (byte) 141,
      (byte) 8,
      (byte) 162,
      (byte) 23,
      (byte) 245,
      (byte) 6,
      (byte) 189,
      (byte) 137,
      (byte) 8,
      (byte) 52,
      (byte) 133,
      (byte) 72,
      (byte) 100,
      (byte) 139,
      (byte) 17,
      (byte) 25,
      (byte) 158,
      (byte) 184,
      (byte) 4,
      (byte) 88,
      (byte) 152,
      (byte) 252,
      (byte) 111,
      (byte) 145,
      (byte) 105,
      (byte) 211,
      (byte) 245,
      (byte) 200,
      (byte) 233,
      (byte) 136,
      (byte) 16 /*0x10*/,
      (byte) 42,
      (byte) 157,
      (byte) 5,
      (byte) 106,
      (byte) 178,
      (byte) 0,
      (byte) 146,
      (byte) 143,
      (byte) 33,
      (byte) 110,
      (byte) 68,
      (byte) 193,
      (byte) 226,
      (byte) 55
    };
    byte[] numArray20 = new byte[55];
    numArray20[44] = (byte) 142;
    numArray20[40] = (byte) 103;
    numArray20[37] = (byte) 140;
    numArray20[3] = (byte) 48 /*0x30*/;
    numArray20[33] = (byte) 35;
    numArray20[4] = (byte) 119;
    numArray20[29] = (byte) 186;
    numArray20[47] = (byte) 35;
    numArray20[36] = (byte) 78;
    numArray20[2] = (byte) 113;
    numArray20[10] = (byte) 225;
    numArray20[11] = (byte) 131;
    numArray20[12] = (byte) 216;
    numArray20[19] = (byte) 243;
    numArray20[45] = (byte) 81;
    numArray20[15] = (byte) 33;
    numArray20[24] = (byte) 110;
    numArray20[17] = (byte) 60;
    numArray20[18] = (byte) 12;
    numArray20[13] = (byte) 158;
    numArray20[20] = (byte) 242;
    numArray20[21] = (byte) 133;
    numArray20[43] = (byte) 185;
    numArray20[23] = (byte) 37;
    numArray20[5] = (byte) 94;
    numArray20[25] = (byte) 127 /*0x7F*/;
    numArray20[26] = (byte) 76;
    numArray20[9] = (byte) 221;
    numArray20[0] = (byte) 212;
    numArray20[54] = (byte) 216;
    numArray20[27] = (byte) 128 /*0x80*/;
    numArray20[31 /*0x1F*/] = (byte) 227;
    numArray20[1] = (byte) 235;
    numArray20[6] = (byte) 73;
    numArray20[34] = (byte) 130;
    numArray20[7] = (byte) 49;
    numArray20[51] = (byte) 122;
    numArray20[49] = (byte) 233;
    numArray20[38] = (byte) 196;
    numArray20[39] = (byte) 229;
    numArray20[52] = (byte) 131;
    numArray20[41] = (byte) 11;
    numArray20[42] = (byte) 39;
    numArray20[16 /*0x10*/] = (byte) 44;
    numArray20[28] = (byte) 217;
    numArray20[32 /*0x20*/] = (byte) 193;
    numArray20[46] = (byte) 24;
    numArray20[35] = (byte) 192 /*0xC0*/;
    numArray20[30] = (byte) 213;
    numArray20[14] = (byte) 160 /*0xA0*/;
    numArray20[50] = (byte) 246;
    numArray20[48 /*0x30*/] = (byte) 23;
    numArray20[8] = (byte) 123;
    numArray20[53] = (byte) 172;
    numArray20[22] = (byte) 158;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[43]
    {
      (byte) 203,
      (byte) 73,
      (byte) 238,
      (byte) 40,
      (byte) 118,
      (byte) 253,
      (byte) 114,
      (byte) 149,
      (byte) 86,
      (byte) 12,
      (byte) 182,
      (byte) 87,
      (byte) 114,
      (byte) 8,
      (byte) 212,
      (byte) 191,
      (byte) 43,
      (byte) 179,
      (byte) 18,
      (byte) 106,
      (byte) 65,
      (byte) 71,
      (byte) 69,
      (byte) 41,
      (byte) 38,
      (byte) 188,
      (byte) 211,
      (byte) 112 /*0x70*/,
      (byte) 175,
      (byte) 248,
      (byte) 206,
      (byte) 126,
      (byte) 238,
      (byte) 235,
      (byte) 60,
      (byte) 215,
      (byte) 242,
      (byte) 44,
      (byte) 249,
      (byte) 75,
      (byte) 205,
      (byte) 135,
      (byte) 131
    };
    byte[] numArray22 = new byte[43]
    {
      (byte) 102,
      (byte) 124,
      (byte) 180,
      (byte) 223,
      (byte) 100,
      (byte) 218,
      (byte) 161,
      (byte) 118,
      (byte) 135,
      (byte) 23,
      (byte) 70,
      (byte) 227,
      (byte) 148,
      (byte) 205,
      (byte) 245,
      (byte) 154,
      (byte) 118,
      (byte) 143,
      (byte) 76,
      (byte) 25,
      (byte) 245,
      (byte) 203,
      (byte) 73,
      (byte) 240 /*0xF0*/,
      (byte) 144 /*0x90*/,
      (byte) 151,
      (byte) 60,
      (byte) 193,
      (byte) 60,
      (byte) 254,
      (byte) 133,
      (byte) 102,
      (byte) 59,
      (byte) 194,
      (byte) 96 /*0x60*/,
      (byte) 90,
      (byte) 51,
      (byte) 199,
      (byte) 151,
      (byte) 76,
      (byte) 198,
      (byte) 154,
      (byte) 183
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 43);
    for (int index = 0; index < 43; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static int ssp_appserver_12590(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 203;
    sourceArray1[22] = (byte) 204;
    sourceArray1[2] = (byte) 22;
    sourceArray1[3] = (byte) 10;
    sourceArray1[40] = (byte) 104;
    sourceArray1[39] = (byte) 240 /*0xF0*/;
    sourceArray1[6] = (byte) 130;
    sourceArray1[7] = (byte) 132;
    sourceArray1[13] = (byte) 1;
    sourceArray1[9] = (byte) 197;
    sourceArray1[45] = (byte) 64 /*0x40*/;
    sourceArray1[11] = (byte) 185;
    sourceArray1[25] = (byte) 199;
    sourceArray1[30] = (byte) 240 /*0xF0*/;
    sourceArray1[14] = (byte) 229;
    sourceArray1[15] = (byte) 94;
    sourceArray1[19] = (byte) 135;
    sourceArray1[17] = (byte) 55;
    sourceArray1[27] = (byte) 186;
    sourceArray1[31 /*0x1F*/] = (byte) 153;
    sourceArray1[36] = (byte) 225;
    sourceArray1[21] = (byte) 253;
    sourceArray1[12] = (byte) 41;
    sourceArray1[23] = byte.MaxValue;
    sourceArray1[1] = (byte) 229;
    sourceArray1[28] = (byte) 196;
    sourceArray1[16 /*0x10*/] = (byte) 62;
    sourceArray1[20] = (byte) 203;
    sourceArray1[10] = (byte) 1;
    sourceArray1[29] = (byte) 56;
    sourceArray1[24] = (byte) 185;
    sourceArray1[43] = (byte) 45;
    sourceArray1[32 /*0x20*/] = (byte) 194;
    sourceArray1[41] = (byte) 184;
    sourceArray1[34] = (byte) 203;
    sourceArray1[35] = (byte) 201;
    sourceArray1[5] = (byte) 253;
    sourceArray1[37] = (byte) 195;
    sourceArray1[18] = (byte) 105;
    sourceArray1[8] = (byte) 4;
    sourceArray1[42] = (byte) 211;
    sourceArray1[26] = (byte) 156;
    sourceArray1[38] = (byte) 181;
    sourceArray1[4] = (byte) 200;
    sourceArray1[44] = (byte) 42;
    sourceArray1[0] = (byte) 165;
    sourceArray1[46] = (byte) 33;
    sourceArray1[47] = (byte) 204;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 90,
      (byte) 137,
      (byte) 227,
      (byte) 209,
      (byte) 196,
      (byte) 9,
      (byte) 8,
      (byte) 133,
      (byte) 48 /*0x30*/,
      (byte) 74,
      (byte) 217,
      (byte) 180,
      (byte) 210,
      (byte) 116,
      (byte) 78,
      (byte) 61,
      (byte) 143,
      (byte) 128 /*0x80*/,
      (byte) 219,
      (byte) 103,
      (byte) 86,
      (byte) 190,
      (byte) 224 /*0xE0*/,
      (byte) 122,
      (byte) 8,
      (byte) 207,
      (byte) 74,
      (byte) 39,
      (byte) 5,
      (byte) 22,
      (byte) 234,
      (byte) 129,
      (byte) 149,
      (byte) 160 /*0xA0*/,
      (byte) 221,
      (byte) 215,
      (byte) 30,
      (byte) 251,
      (byte) 142,
      (byte) 44,
      (byte) 213,
      (byte) 198,
      (byte) 117,
      (byte) 8,
      (byte) 44,
      (byte) 60,
      (byte) 162,
      (byte) 5
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12591(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 195,
      (byte) 149,
      (byte) 133,
      (byte) 168,
      (byte) 83,
      (byte) 126,
      (byte) 170,
      (byte) 84,
      (byte) 41,
      (byte) 125,
      (byte) 120,
      (byte) 66,
      (byte) 215,
      (byte) 90,
      (byte) 143,
      (byte) 176 /*0xB0*/,
      (byte) 31 /*0x1F*/,
      (byte) 98,
      (byte) 229,
      (byte) 241,
      (byte) 168,
      (byte) 206,
      (byte) 18,
      (byte) 107,
      (byte) 178,
      (byte) 8,
      (byte) 206,
      (byte) 232,
      (byte) 127 /*0x7F*/,
      (byte) 25,
      (byte) 35,
      (byte) 71,
      (byte) 17,
      (byte) 48 /*0x30*/,
      (byte) 227,
      (byte) 59,
      (byte) 146,
      (byte) 247,
      (byte) 21,
      (byte) 112 /*0x70*/,
      (byte) 203,
      (byte) 249,
      (byte) 101,
      (byte) 166,
      (byte) 56,
      (byte) 135,
      (byte) 155,
      (byte) 89
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 229,
      (byte) 225,
      (byte) 127 /*0x7F*/,
      (byte) 149,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 230,
      (byte) 226,
      (byte) 77,
      (byte) 17,
      (byte) 51,
      (byte) 242,
      (byte) 239,
      (byte) 225,
      (byte) 4,
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 9,
      (byte) 90,
      (byte) 16 /*0x10*/,
      (byte) 132,
      (byte) 38,
      (byte) 232,
      (byte) 76,
      (byte) 77,
      (byte) 193,
      (byte) 56,
      (byte) 166,
      (byte) 226,
      (byte) 80 /*0x50*/,
      (byte) 230,
      (byte) 181,
      (byte) 8,
      (byte) 218,
      (byte) 36,
      (byte) 233,
      (byte) 92,
      (byte) 23,
      (byte) 169,
      (byte) 141,
      (byte) 188,
      (byte) 67,
      (byte) 12,
      (byte) 25,
      (byte) 177,
      (byte) 60,
      (byte) 132,
      (byte) 6
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12592(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 209,
      (byte) 160 /*0xA0*/,
      (byte) 244,
      (byte) 131,
      (byte) 26,
      (byte) 245,
      (byte) 75,
      (byte) 176 /*0xB0*/,
      (byte) 194,
      (byte) 213,
      (byte) 16 /*0x10*/,
      (byte) 90,
      (byte) 49,
      (byte) 48 /*0x30*/,
      (byte) 166,
      (byte) 119,
      (byte) 84,
      (byte) 30,
      (byte) 40,
      (byte) 77,
      (byte) 116,
      (byte) 37,
      (byte) 174,
      (byte) 105,
      (byte) 108,
      (byte) 50,
      (byte) 43,
      (byte) 109,
      (byte) 116,
      (byte) 231,
      (byte) 145,
      (byte) 63 /*0x3F*/,
      (byte) 126,
      (byte) 211,
      (byte) 64 /*0x40*/,
      (byte) 144 /*0x90*/,
      (byte) 239,
      (byte) 183,
      (byte) 119,
      (byte) 1,
      (byte) 201,
      (byte) 192 /*0xC0*/,
      (byte) 9,
      (byte) 36,
      (byte) 84,
      (byte) 75,
      (byte) 6,
      (byte) 86
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[18] = (byte) 132;
    sourceArray2[19] = (byte) 20;
    sourceArray2[4] = (byte) 138;
    sourceArray2[3] = (byte) 128 /*0x80*/;
    sourceArray2[14] = (byte) 102;
    sourceArray2[5] = (byte) 148;
    sourceArray2[39] = (byte) 36;
    sourceArray2[16 /*0x10*/] = (byte) 2;
    sourceArray2[8] = (byte) 155;
    sourceArray2[20] = (byte) 159;
    sourceArray2[25] = (byte) 80 /*0x50*/;
    sourceArray2[11] = (byte) 201;
    sourceArray2[38] = (byte) 193;
    sourceArray2[10] = (byte) 140;
    sourceArray2[24] = (byte) 102;
    sourceArray2[34] = (byte) 230;
    sourceArray2[1] = (byte) 83;
    sourceArray2[2] = (byte) 152;
    sourceArray2[43] = (byte) 22;
    sourceArray2[30] = byte.MaxValue;
    sourceArray2[29] = (byte) 143;
    sourceArray2[13] = (byte) 118;
    sourceArray2[22] = (byte) 221;
    sourceArray2[6] = (byte) 190;
    sourceArray2[26] = (byte) 73;
    sourceArray2[36] = (byte) 206;
    sourceArray2[12] = (byte) 230;
    sourceArray2[27] = (byte) 74;
    sourceArray2[28] = (byte) 54;
    sourceArray2[0] = (byte) 245;
    sourceArray2[45] = (byte) 107;
    sourceArray2[15] = (byte) 180;
    sourceArray2[32 /*0x20*/] = (byte) 193;
    sourceArray2[33] = (byte) 118;
    sourceArray2[23] = (byte) 12;
    sourceArray2[35] = (byte) 119;
    sourceArray2[21] = (byte) 187;
    sourceArray2[31 /*0x1F*/] = (byte) 90;
    sourceArray2[9] = (byte) 63 /*0x3F*/;
    sourceArray2[7] = (byte) 247;
    sourceArray2[40] = (byte) 50;
    sourceArray2[41] = (byte) 19;
    sourceArray2[42] = (byte) 254;
    sourceArray2[17] = (byte) 230;
    sourceArray2[37] = (byte) 241;
    sourceArray2[44] = (byte) 130;
    sourceArray2[46] = (byte) 75;
    sourceArray2[47] = (byte) 70;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12593()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[75];
      byte[] numArray2 = new byte[55]
      {
        (byte) 112 /*0x70*/,
        (byte) 168,
        (byte) 101,
        (byte) 222,
        (byte) 89,
        (byte) 16 /*0x10*/,
        (byte) 42,
        (byte) 138,
        (byte) 211,
        (byte) 74,
        (byte) 144 /*0x90*/,
        (byte) 103,
        (byte) 118,
        (byte) 246,
        (byte) 187,
        (byte) 244,
        (byte) 101,
        (byte) 169,
        (byte) 160 /*0xA0*/,
        (byte) 131,
        (byte) 67,
        (byte) 179,
        (byte) 137,
        (byte) 92,
        (byte) 119,
        (byte) 36,
        (byte) 251,
        (byte) 21,
        (byte) 238,
        (byte) 152,
        (byte) 48 /*0x30*/,
        (byte) 237,
        (byte) 169,
        (byte) 145,
        (byte) 175,
        (byte) 132,
        (byte) 112 /*0x70*/,
        (byte) 154,
        (byte) 57,
        (byte) 42,
        (byte) 150,
        (byte) 3,
        (byte) 84,
        (byte) 137,
        (byte) 3,
        (byte) 55,
        (byte) 238,
        (byte) 170,
        (byte) 213,
        (byte) 67,
        (byte) 133,
        (byte) 218,
        (byte) 141,
        (byte) 93,
        (byte) 64 /*0x40*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 43,
        (byte) 221,
        (byte) 73,
        (byte) 149,
        (byte) 121,
        (byte) 240 /*0xF0*/,
        (byte) 110,
        (byte) 35,
        (byte) 174,
        (byte) 153,
        (byte) 79,
        (byte) 143,
        (byte) 111,
        (byte) 235,
        (byte) 104,
        (byte) 250,
        (byte) 61,
        (byte) 229,
        (byte) 235,
        (byte) 247,
        (byte) 230,
        (byte) 90,
        (byte) 147,
        (byte) 71,
        (byte) 15,
        (byte) 120,
        (byte) 54,
        (byte) 52,
        (byte) 128 /*0x80*/,
        (byte) 194,
        (byte) 241,
        (byte) 210,
        (byte) 72,
        (byte) 164,
        (byte) 146,
        (byte) 68,
        (byte) 71,
        (byte) 68,
        (byte) 221,
        (byte) 198,
        (byte) 51,
        (byte) 199,
        (byte) 5,
        (byte) 20,
        (byte) 53,
        (byte) 45,
        (byte) 181,
        (byte) 0,
        (byte) 98,
        (byte) 121,
        (byte) 84,
        (byte) 25,
        (byte) 133,
        (byte) 246,
        (byte) 151
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20]
      {
        (byte) 8,
        (byte) 102,
        (byte) 52,
        (byte) 20,
        (byte) 20,
        (byte) 156,
        (byte) 58,
        (byte) 26,
        (byte) 162,
        (byte) 189,
        (byte) 228,
        (byte) 70,
        (byte) 54,
        (byte) 132,
        (byte) 203,
        (byte) 111,
        (byte) 117,
        (byte) 237,
        (byte) 63 /*0x3F*/,
        (byte) 94
      };
      byte[] numArray5 = new byte[20]
      {
        (byte) 207,
        (byte) 58,
        (byte) 150,
        (byte) 29,
        (byte) 38,
        (byte) 184,
        (byte) 69,
        (byte) 220,
        (byte) 222,
        (byte) 93,
        (byte) 117,
        (byte) 119,
        (byte) 224 /*0xE0*/,
        (byte) 66,
        (byte) 194,
        (byte) 116,
        (byte) 6,
        (byte) 233,
        (byte) 192 /*0xC0*/,
        (byte) 236
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[75];
    byte[] numArray7 = new byte[55];
    numArray7[12] = (byte) 52;
    numArray7[21] = (byte) 9;
    numArray7[2] = (byte) 64 /*0x40*/;
    numArray7[3] = (byte) 170;
    numArray7[48 /*0x30*/] = (byte) 96 /*0x60*/;
    numArray7[5] = (byte) 85;
    numArray7[6] = (byte) 95;
    numArray7[7] = (byte) 76;
    numArray7[13] = (byte) 5;
    numArray7[9] = (byte) 242;
    numArray7[11] = (byte) 103;
    numArray7[10] = (byte) 102;
    numArray7[23] = (byte) 158;
    numArray7[34] = (byte) 254;
    numArray7[14] = (byte) 21;
    numArray7[15] = (byte) 129;
    numArray7[16 /*0x10*/] = (byte) 103;
    numArray7[17] = (byte) 26;
    numArray7[25] = (byte) 185;
    numArray7[19] = (byte) 96 /*0x60*/;
    numArray7[45] = (byte) 28;
    numArray7[49] = (byte) 107;
    numArray7[44] = (byte) 208 /*0xD0*/;
    numArray7[4] = (byte) 19;
    numArray7[24] = (byte) 191;
    numArray7[51] = (byte) 68;
    numArray7[26] = (byte) 107;
    numArray7[27] = (byte) 23;
    numArray7[28] = (byte) 112 /*0x70*/;
    numArray7[8] = (byte) 81;
    numArray7[54] = (byte) 198;
    numArray7[31 /*0x1F*/] = (byte) 29;
    numArray7[53] = (byte) 248;
    numArray7[33] = (byte) 38;
    numArray7[41] = (byte) 113;
    numArray7[0] = (byte) 54;
    numArray7[40] = (byte) 67;
    numArray7[37] = (byte) 241;
    numArray7[30] = (byte) 94;
    numArray7[36] = (byte) 211;
    numArray7[39] = (byte) 196;
    numArray7[22] = (byte) 99;
    numArray7[18] = (byte) 110;
    numArray7[43] = (byte) 84;
    numArray7[20] = (byte) 121;
    numArray7[38] = (byte) 39;
    numArray7[46] = (byte) 151;
    numArray7[1] = (byte) 81;
    numArray7[52] = (byte) 145;
    numArray7[47] = (byte) 105;
    numArray7[50] = (byte) 216;
    numArray7[29] = (byte) 0;
    numArray7[35] = (byte) 195;
    numArray7[32 /*0x20*/] = (byte) 175;
    numArray7[42] = (byte) 146;
    byte[] numArray8 = new byte[55];
    numArray8[34] = (byte) 167;
    numArray8[1] = (byte) 186;
    numArray8[2] = (byte) 33;
    numArray8[48 /*0x30*/] = (byte) 216;
    numArray8[4] = (byte) 37;
    numArray8[5] = (byte) 247;
    numArray8[6] = (byte) 214;
    numArray8[7] = (byte) 157;
    numArray8[0] = (byte) 160 /*0xA0*/;
    numArray8[31 /*0x1F*/] = (byte) 197;
    numArray8[10] = (byte) 81;
    numArray8[11] = (byte) 24;
    numArray8[12] = (byte) 204;
    numArray8[13] = (byte) 29;
    numArray8[21] = (byte) 82;
    numArray8[24] = (byte) 59;
    numArray8[33] = (byte) 92;
    numArray8[53] = (byte) 84;
    numArray8[18] = (byte) 169;
    numArray8[19] = (byte) 241;
    numArray8[9] = (byte) 68;
    numArray8[42] = (byte) 20;
    numArray8[15] = (byte) 119;
    numArray8[23] = (byte) 210;
    numArray8[20] = (byte) 196;
    numArray8[44] = (byte) 195;
    numArray8[26] = (byte) 238;
    numArray8[27] = (byte) 188;
    numArray8[22] = (byte) 218;
    numArray8[29] = (byte) 243;
    numArray8[30] = (byte) 3;
    numArray8[25] = (byte) 165;
    numArray8[50] = (byte) 88;
    numArray8[41] = (byte) 44;
    numArray8[35] = (byte) 229;
    numArray8[3] = (byte) 153;
    numArray8[54] = (byte) 130;
    numArray8[32 /*0x20*/] = (byte) 71;
    numArray8[38] = (byte) 91;
    numArray8[39] = (byte) 56;
    numArray8[28] = (byte) 230;
    numArray8[14] = (byte) 63 /*0x3F*/;
    numArray8[37] = (byte) 218;
    numArray8[17] = (byte) 69;
    numArray8[8] = (byte) 240 /*0xF0*/;
    numArray8[45] = (byte) 195;
    numArray8[43] = (byte) 15;
    numArray8[47] = (byte) 192 /*0xC0*/;
    numArray8[40] = byte.MaxValue;
    numArray8[49] = (byte) 62;
    numArray8[16 /*0x10*/] = (byte) 82;
    numArray8[51] = (byte) 202;
    numArray8[52] = (byte) 198;
    numArray8[46] = (byte) 235;
    numArray8[36] = (byte) 40;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[20]
    {
      (byte) 142,
      (byte) 8,
      (byte) 157,
      (byte) 73,
      (byte) 230,
      (byte) 143,
      (byte) 99,
      (byte) 175,
      (byte) 22,
      (byte) 37,
      (byte) 12,
      (byte) 26,
      (byte) 132,
      (byte) 30,
      (byte) 174,
      (byte) 142,
      (byte) 220,
      (byte) 182,
      (byte) 181,
      (byte) 28
    };
    byte[] numArray10 = new byte[20];
    numArray10[0] = (byte) 108;
    numArray10[1] = (byte) 219;
    numArray10[4] = (byte) 172;
    numArray10[14] = (byte) 160 /*0xA0*/;
    numArray10[2] = (byte) 213;
    numArray10[5] = (byte) 46;
    numArray10[6] = (byte) 211;
    numArray10[7] = (byte) 139;
    numArray10[8] = (byte) 8;
    numArray10[9] = (byte) 228;
    numArray10[10] = (byte) 126;
    numArray10[19] = (byte) 165;
    numArray10[13] = (byte) 123;
    numArray10[11] = (byte) 84;
    numArray10[12] = (byte) 76;
    numArray10[3] = (byte) 254;
    numArray10[16 /*0x10*/] = (byte) 203;
    numArray10[17] = (byte) 252;
    numArray10[18] = (byte) 48 /*0x30*/;
    numArray10[15] = (byte) 163;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 20);
    for (int index = 0; index < 20; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12594()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 205,
        (byte) 18,
        (byte) 14,
        (byte) 35,
        (byte) 70,
        (byte) 156
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 183,
        (byte) 72,
        (byte) 18,
        (byte) 20,
        (byte) 194,
        (byte) 91
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6];
    numArray5[4] = (byte) 223;
    numArray5[3] = (byte) 45;
    numArray5[2] = (byte) 21;
    numArray5[0] = (byte) 99;
    numArray5[5] = (byte) 215;
    numArray5[1] = (byte) 137;
    byte[] numArray6 = new byte[6];
    numArray6[2] = (byte) 160 /*0xA0*/;
    numArray6[1] = (byte) 113;
    numArray6[0] = (byte) 103;
    numArray6[3] = (byte) 235;
    numArray6[4] = (byte) 116;
    numArray6[5] = (byte) 196;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12595()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55]
      {
        (byte) 109,
        (byte) 195,
        (byte) 158,
        (byte) 241,
        (byte) 96 /*0x60*/,
        (byte) 7,
        (byte) 227,
        (byte) 138,
        (byte) 1,
        (byte) 79,
        (byte) 31 /*0x1F*/,
        (byte) 194,
        (byte) 54,
        (byte) 221,
        (byte) 185,
        (byte) 137,
        (byte) 25,
        (byte) 209,
        (byte) 180,
        (byte) 164,
        (byte) 35,
        (byte) 135,
        (byte) 43,
        (byte) 57,
        (byte) 24,
        (byte) 146,
        (byte) 241,
        (byte) 211,
        (byte) 78,
        (byte) 6,
        (byte) 9,
        (byte) 209,
        (byte) 12,
        (byte) 252,
        (byte) 28,
        (byte) 82,
        (byte) 142,
        (byte) 237,
        (byte) 119,
        (byte) 201,
        (byte) 166,
        (byte) 7,
        (byte) 15,
        (byte) 58,
        (byte) 228,
        (byte) 242,
        (byte) 72,
        (byte) 64 /*0x40*/,
        (byte) 150,
        (byte) 110,
        (byte) 124,
        (byte) 97,
        (byte) 125,
        (byte) 227,
        (byte) 67
      };
      byte[] numArray3 = new byte[55];
      numArray3[42] = (byte) 2;
      numArray3[1] = byte.MaxValue;
      numArray3[9] = (byte) 81;
      numArray3[3] = (byte) 8;
      numArray3[11] = (byte) 44;
      numArray3[40] = (byte) 167;
      numArray3[47] = (byte) 147;
      numArray3[34] = (byte) 201;
      numArray3[5] = (byte) 177;
      numArray3[41] = (byte) 44;
      numArray3[10] = (byte) 33;
      numArray3[50] = (byte) 122;
      numArray3[31 /*0x1F*/] = (byte) 136;
      numArray3[26] = (byte) 100;
      numArray3[38] = (byte) 133;
      numArray3[15] = (byte) 210;
      numArray3[8] = (byte) 133;
      numArray3[17] = (byte) 127 /*0x7F*/;
      numArray3[18] = (byte) 128 /*0x80*/;
      numArray3[19] = (byte) 41;
      numArray3[20] = (byte) 254;
      numArray3[7] = (byte) 140;
      numArray3[22] = (byte) 185;
      numArray3[37] = (byte) 176 /*0xB0*/;
      numArray3[28] = (byte) 206;
      numArray3[25] = (byte) 138;
      numArray3[6] = (byte) 178;
      numArray3[27] = (byte) 20;
      numArray3[35] = (byte) 4;
      numArray3[12] = (byte) 207;
      numArray3[30] = (byte) 115;
      numArray3[36] = (byte) 194;
      numArray3[32 /*0x20*/] = (byte) 202;
      numArray3[33] = (byte) 12;
      numArray3[16 /*0x10*/] = (byte) 21;
      numArray3[4] = (byte) 144 /*0x90*/;
      numArray3[0] = (byte) 176 /*0xB0*/;
      numArray3[23] = (byte) 70;
      numArray3[49] = (byte) 55;
      numArray3[39] = (byte) 49;
      numArray3[24] = (byte) 26;
      numArray3[29] = (byte) 177;
      numArray3[13] = (byte) 52;
      numArray3[43] = (byte) 109;
      numArray3[44] = (byte) 135;
      numArray3[21] = (byte) 78;
      numArray3[46] = (byte) 181;
      numArray3[45] = (byte) 199;
      numArray3[48 /*0x30*/] = (byte) 209;
      numArray3[14] = (byte) 91;
      numArray3[2] = (byte) 146;
      numArray3[51] = (byte) 252;
      numArray3[52] = (byte) 178;
      numArray3[53] = (byte) 66;
      numArray3[54] = (byte) 113;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15]
      {
        (byte) 15,
        (byte) 179,
        (byte) 132,
        (byte) 100,
        (byte) 209,
        (byte) 93,
        (byte) 111,
        (byte) 158,
        (byte) 248,
        (byte) 139,
        (byte) 128 /*0x80*/,
        (byte) 138,
        (byte) 171,
        (byte) 208 /*0xD0*/,
        (byte) 22
      };
      byte[] numArray5 = new byte[15];
      numArray5[10] = (byte) 31 /*0x1F*/;
      numArray5[3] = (byte) 62;
      numArray5[1] = (byte) 13;
      numArray5[2] = (byte) 248;
      numArray5[4] = (byte) 134;
      numArray5[5] = (byte) 73;
      numArray5[14] = (byte) 181;
      numArray5[6] = (byte) 118;
      numArray5[8] = (byte) 119;
      numArray5[13] = (byte) 134;
      numArray5[9] = (byte) 40;
      numArray5[11] = (byte) 94;
      numArray5[12] = (byte) 168;
      numArray5[7] = (byte) 241;
      numArray5[0] = (byte) 41;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[16 /*0x10*/];
      byte[] response = new byte[16 /*0x10*/];
      Array.Copy((Array) sc_12586.sspq, 0, (Array) numArray6, 0, 16 /*0x10*/);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12586.sspr, 0, (Array) numArray6, 0, 16 /*0x10*/);
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
    byte[] numArray7 = new byte[70];
    byte[] numArray8 = new byte[55]
    {
      (byte) 90,
      (byte) 126,
      (byte) 213,
      (byte) 11,
      (byte) 171,
      (byte) 80 /*0x50*/,
      (byte) 202,
      (byte) 74,
      (byte) 50,
      (byte) 195,
      (byte) 242,
      (byte) 53,
      (byte) 253,
      (byte) 54,
      (byte) 71,
      (byte) 239,
      (byte) 195,
      (byte) 152,
      (byte) 181,
      (byte) 180,
      (byte) 140,
      (byte) 233,
      (byte) 161,
      (byte) 229,
      (byte) 188,
      (byte) 166,
      (byte) 33,
      (byte) 55,
      (byte) 172,
      (byte) 61,
      (byte) 137,
      (byte) 199,
      (byte) 0,
      (byte) 75,
      (byte) 103,
      (byte) 198,
      (byte) 188,
      (byte) 187,
      (byte) 103,
      (byte) 203,
      (byte) 150,
      (byte) 218,
      (byte) 27,
      (byte) 2,
      (byte) 122,
      (byte) 112 /*0x70*/,
      (byte) 87,
      (byte) 103,
      (byte) 105,
      (byte) 158,
      (byte) 49,
      (byte) 236,
      (byte) 239,
      (byte) 132,
      (byte) 201
    };
    byte[] numArray9 = new byte[55];
    numArray9[6] = (byte) 5;
    numArray9[27] = (byte) 167;
    numArray9[52] = (byte) 28;
    numArray9[50] = (byte) 240 /*0xF0*/;
    numArray9[4] = (byte) 228;
    numArray9[5] = (byte) 227;
    numArray9[53] = (byte) 174;
    numArray9[1] = (byte) 134;
    numArray9[43] = (byte) 14;
    numArray9[12] = (byte) 105;
    numArray9[38] = (byte) 163;
    numArray9[8] = (byte) 205;
    numArray9[32 /*0x20*/] = (byte) 169;
    numArray9[35] = (byte) 43;
    numArray9[14] = (byte) 254;
    numArray9[15] = (byte) 132;
    numArray9[16 /*0x10*/] = (byte) 73;
    numArray9[17] = (byte) 202;
    numArray9[49] = (byte) 82;
    numArray9[19] = (byte) 27;
    numArray9[42] = (byte) 229;
    numArray9[20] = (byte) 72;
    numArray9[22] = (byte) 131;
    numArray9[47] = (byte) 72;
    numArray9[24] = (byte) 17;
    numArray9[25] = (byte) 113;
    numArray9[26] = (byte) 206;
    numArray9[23] = (byte) 236;
    numArray9[28] = (byte) 14;
    numArray9[21] = (byte) 231;
    numArray9[36] = (byte) 166;
    numArray9[51] = (byte) 204;
    numArray9[3] = (byte) 208 /*0xD0*/;
    numArray9[33] = (byte) 174;
    numArray9[34] = (byte) 202;
    numArray9[13] = (byte) 79;
    numArray9[2] = (byte) 59;
    numArray9[11] = (byte) 35;
    numArray9[0] = (byte) 110;
    numArray9[39] = (byte) 96 /*0x60*/;
    numArray9[40] = (byte) 32 /*0x20*/;
    numArray9[7] = (byte) 27;
    numArray9[9] = (byte) 147;
    numArray9[10] = (byte) 249;
    numArray9[44] = (byte) 6;
    numArray9[45] = (byte) 132;
    numArray9[29] = (byte) 136;
    numArray9[31 /*0x1F*/] = (byte) 88;
    numArray9[48 /*0x30*/] = (byte) 241;
    numArray9[18] = (byte) 122;
    numArray9[37] = (byte) 130;
    numArray9[41] = (byte) 148;
    numArray9[46] = (byte) 131;
    numArray9[30] = (byte) 244;
    numArray9[54] = (byte) 201;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[15]
    {
      (byte) 133,
      (byte) 4,
      (byte) 181,
      (byte) 117,
      (byte) 198,
      (byte) 143,
      (byte) 232,
      (byte) 157,
      (byte) 83,
      (byte) 184,
      (byte) 47,
      (byte) 216,
      (byte) 151,
      (byte) 103,
      (byte) 155
    };
    byte[] numArray11 = new byte[15];
    numArray11[10] = (byte) 241;
    numArray11[12] = (byte) 162;
    numArray11[2] = (byte) 3;
    numArray11[14] = (byte) 60;
    numArray11[0] = (byte) 44;
    numArray11[4] = (byte) 192 /*0xC0*/;
    numArray11[6] = (byte) 131;
    numArray11[5] = (byte) 20;
    numArray11[8] = (byte) 169;
    numArray11[9] = (byte) 120;
    numArray11[13] = (byte) 86;
    numArray11[11] = (byte) 166;
    numArray11[7] = (byte) 155;
    numArray11[1] = (byte) 70;
    numArray11[3] = (byte) 254;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12596()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[41];
      byte[] numArray2 = new byte[41];
      numArray2[20] = (byte) 103;
      numArray2[1] = (byte) 103;
      numArray2[36] = (byte) 33;
      numArray2[3] = (byte) 38;
      numArray2[30] = (byte) 163;
      numArray2[5] = (byte) 173;
      numArray2[6] = (byte) 61;
      numArray2[26] = (byte) 237;
      numArray2[7] = (byte) 42;
      numArray2[33] = (byte) 235;
      numArray2[10] = (byte) 118;
      numArray2[25] = (byte) 226;
      numArray2[34] = (byte) 34;
      numArray2[13] = (byte) 3;
      numArray2[9] = (byte) 131;
      numArray2[15] = (byte) 237;
      numArray2[16 /*0x10*/] = (byte) 183;
      numArray2[11] = (byte) 191;
      numArray2[18] = (byte) 163;
      numArray2[14] = (byte) 142;
      numArray2[8] = (byte) 252;
      numArray2[2] = (byte) 172;
      numArray2[22] = (byte) 245;
      numArray2[23] = (byte) 12;
      numArray2[24] = (byte) 83;
      numArray2[17] = (byte) 75;
      numArray2[4] = (byte) 60;
      numArray2[27] = (byte) 6;
      numArray2[28] = (byte) 188;
      numArray2[29] = (byte) 128 /*0x80*/;
      numArray2[38] = (byte) 221;
      numArray2[31 /*0x1F*/] = (byte) 196;
      numArray2[32 /*0x20*/] = (byte) 133;
      numArray2[21] = (byte) 106;
      numArray2[39] = (byte) 198;
      numArray2[35] = (byte) 51;
      numArray2[19] = (byte) 251;
      numArray2[37] = (byte) 38;
      numArray2[0] = (byte) 69;
      numArray2[12] = (byte) 81;
      numArray2[40] = (byte) 105;
      byte[] numArray3 = new byte[41];
      numArray3[35] = (byte) 190;
      numArray3[1] = (byte) 177;
      numArray3[20] = (byte) 84;
      numArray3[38] = (byte) 225;
      numArray3[12] = (byte) 122;
      numArray3[7] = (byte) 106;
      numArray3[6] = (byte) 114;
      numArray3[30] = (byte) 183;
      numArray3[8] = (byte) 26;
      numArray3[9] = (byte) 254;
      numArray3[37] = (byte) 144 /*0x90*/;
      numArray3[11] = (byte) 110;
      numArray3[22] = (byte) 204;
      numArray3[14] = (byte) 123;
      numArray3[13] = (byte) 80 /*0x50*/;
      numArray3[15] = (byte) 231;
      numArray3[16 /*0x10*/] = (byte) 93;
      numArray3[5] = (byte) 166;
      numArray3[18] = (byte) 85;
      numArray3[0] = (byte) 86;
      numArray3[2] = (byte) 145;
      numArray3[19] = (byte) 202;
      numArray3[17] = (byte) 5;
      numArray3[23] = (byte) 44;
      numArray3[24] = (byte) 148;
      numArray3[25] = (byte) 141;
      numArray3[26] = (byte) 142;
      numArray3[27] = (byte) 244;
      numArray3[36] = (byte) 193;
      numArray3[29] = (byte) 14;
      numArray3[31 /*0x1F*/] = (byte) 223;
      numArray3[21] = (byte) 122;
      numArray3[32 /*0x20*/] = (byte) 100;
      numArray3[33] = (byte) 96 /*0x60*/;
      numArray3[28] = (byte) 124;
      numArray3[3] = (byte) 175;
      numArray3[4] = (byte) 15;
      numArray3[10] = (byte) 172;
      numArray3[34] = (byte) 49;
      numArray3[39] = (byte) 220;
      numArray3[40] = (byte) 13;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 41);
      for (int index = 0; index < 41; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[41];
    byte[] numArray5 = new byte[41]
    {
      (byte) 61,
      (byte) 234,
      (byte) 214,
      (byte) 94,
      (byte) 125,
      (byte) 167,
      (byte) 189,
      (byte) 87,
      (byte) 198,
      (byte) 35,
      (byte) 146,
      (byte) 187,
      (byte) 161,
      (byte) 217,
      (byte) 127 /*0x7F*/,
      (byte) 18,
      (byte) 104,
      (byte) 30,
      (byte) 183,
      (byte) 186,
      (byte) 187,
      (byte) 111,
      (byte) 120,
      (byte) 148,
      (byte) 176 /*0xB0*/,
      (byte) 151,
      (byte) 140,
      (byte) 167,
      (byte) 206,
      (byte) 30,
      (byte) 109,
      (byte) 33,
      (byte) 95,
      (byte) 221,
      (byte) 78,
      (byte) 112 /*0x70*/,
      (byte) 20,
      (byte) 103,
      (byte) 11,
      (byte) 231,
      (byte) 74
    };
    byte[] numArray6 = new byte[41]
    {
      (byte) 22,
      (byte) 162,
      (byte) 186,
      (byte) 150,
      (byte) 156,
      (byte) 64 /*0x40*/,
      (byte) 231,
      (byte) 90,
      (byte) 244,
      (byte) 229,
      (byte) 64 /*0x40*/,
      (byte) 139,
      (byte) 47,
      (byte) 123,
      (byte) 228,
      (byte) 93,
      (byte) 91,
      (byte) 95,
      (byte) 172,
      (byte) 155,
      (byte) 59,
      (byte) 209,
      (byte) 150,
      (byte) 59,
      (byte) 7,
      (byte) 173,
      (byte) 84,
      (byte) 56,
      (byte) 99,
      (byte) 114,
      (byte) 232,
      (byte) 143,
      (byte) 225,
      (byte) 157,
      (byte) 166,
      (byte) 211,
      (byte) 251,
      (byte) 108,
      (byte) 24,
      (byte) 77,
      (byte) 55
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 41);
    for (int index = 0; index < 41; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12597(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 4,
      (byte) 61,
      (byte) 133,
      (byte) 161,
      (byte) 162,
      (byte) 160 /*0xA0*/,
      (byte) 227,
      (byte) 222,
      (byte) 142,
      (byte) 205,
      (byte) 114,
      (byte) 65,
      (byte) 173,
      (byte) 230,
      (byte) 27,
      (byte) 21,
      (byte) 137,
      (byte) 168,
      (byte) 181,
      (byte) 112 /*0x70*/,
      (byte) 92,
      (byte) 195,
      (byte) 139,
      (byte) 14,
      (byte) 253,
      (byte) 1,
      (byte) 19,
      (byte) 207,
      (byte) 111,
      (byte) 77,
      (byte) 83,
      (byte) 144 /*0x90*/,
      (byte) 181,
      (byte) 233,
      (byte) 146,
      (byte) 78,
      (byte) 34,
      (byte) 44,
      (byte) 123,
      (byte) 205,
      (byte) 62,
      (byte) 128 /*0x80*/,
      (byte) 79,
      (byte) 128 /*0x80*/,
      (byte) 253,
      (byte) 211,
      (byte) 185,
      (byte) 36
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 67,
      (byte) 166,
      (byte) 16 /*0x10*/,
      (byte) 42,
      (byte) 123,
      (byte) 39,
      (byte) 11,
      (byte) 2,
      (byte) 104,
      (byte) 253,
      (byte) 154,
      (byte) 237,
      (byte) 54,
      (byte) 177,
      (byte) 8,
      (byte) 14,
      (byte) 217,
      (byte) 245,
      (byte) 123,
      (byte) 121,
      (byte) 209,
      (byte) 217,
      (byte) 132,
      (byte) 143,
      (byte) 192 /*0xC0*/,
      (byte) 88,
      (byte) 116,
      (byte) 30,
      (byte) 116,
      (byte) 203,
      (byte) 170,
      (byte) 55,
      (byte) 95,
      (byte) 51,
      (byte) 178,
      (byte) 96 /*0x60*/,
      (byte) 58,
      (byte) 145,
      (byte) 175,
      (byte) 136,
      (byte) 69,
      (byte) 154,
      (byte) 127 /*0x7F*/,
      (byte) 76,
      (byte) 11,
      (byte) 192 /*0xC0*/,
      (byte) 68,
      (byte) 133
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[46];
    byte[] response2 = new byte[46];
    Array.Copy((Array) sc_12586.sspq, 16 /*0x10*/, (Array) numArray2, 0, 46);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12586.sspr, 16 /*0x10*/, (Array) numArray2, 0, 46);
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

  internal static string ssp_appserver_12598()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[72];
      byte[] numArray2 = new byte[55]
      {
        (byte) 27,
        (byte) 71,
        (byte) 244,
        (byte) 208 /*0xD0*/,
        byte.MaxValue,
        (byte) 64 /*0x40*/,
        (byte) 136,
        (byte) 70,
        (byte) 72,
        (byte) 60,
        (byte) 142,
        (byte) 204,
        (byte) 168,
        (byte) 101,
        (byte) 121,
        (byte) 88,
        (byte) 158,
        (byte) 16 /*0x10*/,
        (byte) 51,
        (byte) 216,
        (byte) 154,
        (byte) 131,
        (byte) 69,
        (byte) 69,
        (byte) 2,
        (byte) 48 /*0x30*/,
        (byte) 71,
        (byte) 211,
        (byte) 82,
        (byte) 168,
        (byte) 7,
        (byte) 202,
        (byte) 102,
        (byte) 3,
        (byte) 245,
        (byte) 226,
        (byte) 84,
        (byte) 29,
        (byte) 3,
        (byte) 250,
        (byte) 224 /*0xE0*/,
        (byte) 152,
        (byte) 21,
        (byte) 9,
        (byte) 240 /*0xF0*/,
        (byte) 132,
        (byte) 243,
        (byte) 70,
        (byte) 16 /*0x10*/,
        (byte) 130,
        (byte) 161,
        (byte) 26,
        (byte) 221,
        (byte) 53,
        (byte) 107
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 77,
        (byte) 121,
        (byte) 139,
        (byte) 65,
        (byte) 123,
        (byte) 42,
        (byte) 19,
        (byte) 14,
        (byte) 23,
        (byte) 35,
        (byte) 109,
        (byte) 62,
        (byte) 63 /*0x3F*/,
        (byte) 247,
        (byte) 199,
        (byte) 32 /*0x20*/,
        (byte) 91,
        (byte) 222,
        (byte) 103,
        (byte) 125,
        (byte) 53,
        (byte) 237,
        (byte) 168,
        (byte) 226,
        (byte) 14,
        (byte) 63 /*0x3F*/,
        (byte) 178,
        (byte) 192 /*0xC0*/,
        (byte) 206,
        (byte) 85,
        (byte) 20,
        (byte) 72,
        (byte) 77,
        (byte) 102,
        (byte) 96 /*0x60*/,
        (byte) 226,
        (byte) 185,
        (byte) 0,
        (byte) 198,
        (byte) 81,
        (byte) 183,
        (byte) 1,
        (byte) 36,
        (byte) 2,
        (byte) 120,
        (byte) 141,
        (byte) 45,
        (byte) 212,
        (byte) 106,
        (byte) 83,
        (byte) 89,
        (byte) 112 /*0x70*/,
        (byte) 95,
        (byte) 239,
        (byte) 194
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17];
      numArray4[6] = (byte) 160 /*0xA0*/;
      numArray4[11] = (byte) 111;
      numArray4[3] = (byte) 248;
      numArray4[0] = (byte) 174;
      numArray4[4] = (byte) 181;
      numArray4[1] = (byte) 206;
      numArray4[7] = (byte) 53;
      numArray4[10] = (byte) 165;
      numArray4[8] = (byte) 195;
      numArray4[5] = (byte) 244;
      numArray4[9] = (byte) 102;
      numArray4[13] = (byte) 3;
      numArray4[12] = (byte) 150;
      numArray4[2] = (byte) 207;
      numArray4[14] = (byte) 128 /*0x80*/;
      numArray4[15] = (byte) 13;
      numArray4[16 /*0x10*/] = (byte) 56;
      byte[] numArray5 = new byte[17]
      {
        (byte) 88,
        (byte) 183,
        (byte) 80 /*0x50*/,
        (byte) 157,
        (byte) 53,
        (byte) 97,
        (byte) 134,
        (byte) 195,
        (byte) 25,
        (byte) 218,
        (byte) 151,
        (byte) 236,
        (byte) 16 /*0x10*/,
        (byte) 123,
        (byte) 71,
        (byte) 172,
        (byte) 150
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[72];
    byte[] numArray7 = new byte[55];
    numArray7[2] = (byte) 163;
    numArray7[1] = (byte) 43;
    numArray7[30] = (byte) 182;
    numArray7[41] = (byte) 239;
    numArray7[4] = (byte) 211;
    numArray7[5] = (byte) 45;
    numArray7[21] = (byte) 247;
    numArray7[7] = (byte) 78;
    numArray7[8] = (byte) 215;
    numArray7[9] = (byte) 121;
    numArray7[13] = (byte) 165;
    numArray7[43] = (byte) 72;
    numArray7[12] = (byte) 12;
    numArray7[11] = (byte) 32 /*0x20*/;
    numArray7[44] = (byte) 80 /*0x50*/;
    numArray7[15] = (byte) 105;
    numArray7[16 /*0x10*/] = (byte) 42;
    numArray7[14] = (byte) 62;
    numArray7[18] = (byte) 89;
    numArray7[0] = (byte) 2;
    numArray7[17] = (byte) 197;
    numArray7[38] = (byte) 104;
    numArray7[51] = (byte) 26;
    numArray7[19] = (byte) 80 /*0x50*/;
    numArray7[24] = (byte) 152;
    numArray7[26] = (byte) 214;
    numArray7[36] = (byte) 94;
    numArray7[27] = (byte) 54;
    numArray7[6] = (byte) 30;
    numArray7[37] = (byte) 247;
    numArray7[42] = (byte) 41;
    numArray7[20] = (byte) 145;
    numArray7[32 /*0x20*/] = (byte) 49;
    numArray7[39] = (byte) 40;
    numArray7[10] = (byte) 127 /*0x7F*/;
    numArray7[35] = (byte) 204;
    numArray7[45] = (byte) 230;
    numArray7[23] = (byte) 37;
    numArray7[50] = (byte) 50;
    numArray7[29] = (byte) 17;
    numArray7[40] = (byte) 95;
    numArray7[48 /*0x30*/] = (byte) 242;
    numArray7[33] = (byte) 216;
    numArray7[49] = (byte) 46;
    numArray7[22] = (byte) 242;
    numArray7[25] = (byte) 72;
    numArray7[46] = (byte) 224 /*0xE0*/;
    numArray7[47] = (byte) 72;
    numArray7[34] = (byte) 54;
    numArray7[28] = (byte) 13;
    numArray7[3] = (byte) 117;
    numArray7[31 /*0x1F*/] = (byte) 94;
    numArray7[52] = (byte) 76;
    numArray7[53] = (byte) 254;
    numArray7[54] = (byte) 162;
    byte[] numArray8 = new byte[55]
    {
      (byte) 209,
      (byte) 152,
      (byte) 222,
      (byte) 170,
      (byte) 204,
      (byte) 82,
      (byte) 213,
      (byte) 47,
      (byte) 94,
      (byte) 208 /*0xD0*/,
      (byte) 121,
      (byte) 80 /*0x50*/,
      (byte) 206,
      (byte) 31 /*0x1F*/,
      (byte) 37,
      (byte) 3,
      (byte) 119,
      (byte) 185,
      (byte) 245,
      (byte) 6,
      (byte) 75,
      (byte) 14,
      (byte) 51,
      (byte) 222,
      (byte) 233,
      (byte) 56,
      (byte) 38,
      (byte) 59,
      (byte) 181,
      (byte) 160 /*0xA0*/,
      (byte) 5,
      (byte) 219,
      (byte) 116,
      (byte) 64 /*0x40*/,
      (byte) 218,
      (byte) 118,
      (byte) 131,
      (byte) 185,
      (byte) 14,
      (byte) 105,
      (byte) 171,
      (byte) 213,
      (byte) 119,
      (byte) 222,
      (byte) 89,
      (byte) 124,
      (byte) 82,
      (byte) 116,
      (byte) 104,
      (byte) 226,
      (byte) 133,
      (byte) 12,
      (byte) 235,
      (byte) 129,
      (byte) 241
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[17];
    numArray9[6] = (byte) 231;
    numArray9[13] = (byte) 41;
    numArray9[2] = (byte) 35;
    numArray9[12] = (byte) 221;
    numArray9[11] = (byte) 66;
    numArray9[14] = (byte) 24;
    numArray9[5] = (byte) 98;
    numArray9[7] = (byte) 40;
    numArray9[8] = (byte) 14;
    numArray9[9] = (byte) 187;
    numArray9[3] = (byte) 200;
    numArray9[1] = (byte) 42;
    numArray9[15] = (byte) 128 /*0x80*/;
    numArray9[10] = (byte) 253;
    numArray9[16 /*0x10*/] = (byte) 95;
    numArray9[4] = (byte) 163;
    numArray9[0] = (byte) 168;
    byte[] numArray10 = new byte[17];
    numArray10[6] = (byte) 105;
    numArray10[1] = (byte) 60;
    numArray10[7] = (byte) 250;
    numArray10[3] = (byte) 154;
    numArray10[15] = (byte) 37;
    numArray10[5] = (byte) 136;
    numArray10[2] = (byte) 1;
    numArray10[4] = (byte) 13;
    numArray10[8] = (byte) 31 /*0x1F*/;
    numArray10[9] = (byte) 182;
    numArray10[10] = (byte) 4;
    numArray10[16 /*0x10*/] = (byte) 218;
    numArray10[12] = (byte) 41;
    numArray10[13] = (byte) 125;
    numArray10[14] = (byte) 66;
    numArray10[11] = (byte) 85;
    numArray10[0] = (byte) 248;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 17);
    for (int index = 0; index < 17; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12599()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 2,
        (byte) 113,
        (byte) 104,
        (byte) 19,
        (byte) 223,
        (byte) 167,
        (byte) 7,
        (byte) 74,
        (byte) 24,
        (byte) 150,
        (byte) 199,
        (byte) 83,
        (byte) 216,
        (byte) 241,
        (byte) 40,
        (byte) 199,
        (byte) 85,
        (byte) 165,
        (byte) 167
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 187,
        (byte) 113,
        (byte) 214,
        (byte) 75,
        (byte) 93,
        (byte) 236,
        (byte) 212,
        (byte) 187,
        (byte) 206,
        (byte) 207,
        (byte) 162,
        (byte) 219,
        (byte) 64 /*0x40*/,
        (byte) 253,
        (byte) 13,
        (byte) 219,
        (byte) 94,
        (byte) 45,
        (byte) 57
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[4] = (byte) 192 /*0xC0*/;
    numArray5[1] = (byte) 11;
    numArray5[2] = (byte) 29;
    numArray5[10] = (byte) 27;
    numArray5[16 /*0x10*/] = (byte) 244;
    numArray5[5] = (byte) 248;
    numArray5[6] = (byte) 161;
    numArray5[7] = (byte) 57;
    numArray5[12] = (byte) 54;
    numArray5[11] = (byte) 153;
    numArray5[3] = (byte) 32 /*0x20*/;
    numArray5[0] = (byte) 125;
    numArray5[17] = (byte) 165;
    numArray5[13] = (byte) 126;
    numArray5[14] = (byte) 80 /*0x50*/;
    numArray5[15] = (byte) 245;
    numArray5[8] = (byte) 146;
    numArray5[9] = (byte) 42;
    numArray5[18] = (byte) 116;
    byte[] numArray6 = new byte[19];
    numArray6[14] = (byte) 247;
    numArray6[13] = (byte) 44;
    numArray6[0] = (byte) 242;
    numArray6[3] = (byte) 245;
    numArray6[1] = (byte) 210;
    numArray6[2] = (byte) 14;
    numArray6[6] = (byte) 208 /*0xD0*/;
    numArray6[4] = (byte) 187;
    numArray6[8] = (byte) 49;
    numArray6[7] = (byte) 186;
    numArray6[10] = (byte) 24;
    numArray6[9] = (byte) 254;
    numArray6[17] = (byte) 112 /*0x70*/;
    numArray6[12] = (byte) 87;
    numArray6[5] = (byte) 155;
    numArray6[15] = (byte) 133;
    numArray6[16 /*0x10*/] = (byte) 30;
    numArray6[18] = (byte) 85;
    numArray6[11] = (byte) 235;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12600()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55]
      {
        (byte) 45,
        (byte) 211,
        (byte) 164,
        (byte) 197,
        (byte) 53,
        (byte) 246,
        (byte) 186,
        (byte) 139,
        (byte) 243,
        byte.MaxValue,
        (byte) 52,
        (byte) 190,
        (byte) 101,
        (byte) 106,
        (byte) 40,
        (byte) 162,
        (byte) 74,
        (byte) 12,
        (byte) 139,
        (byte) 76,
        (byte) 20,
        (byte) 253,
        (byte) 6,
        (byte) 59,
        (byte) 98,
        (byte) 109,
        (byte) 239,
        (byte) 253,
        (byte) 28,
        (byte) 56,
        (byte) 28,
        (byte) 105,
        (byte) 45,
        (byte) 214,
        (byte) 122,
        (byte) 11,
        (byte) 95,
        (byte) 215,
        (byte) 1,
        (byte) 72,
        (byte) 1,
        (byte) 30,
        (byte) 132,
        (byte) 96 /*0x60*/,
        (byte) 125,
        (byte) 67,
        (byte) 19,
        (byte) 73,
        (byte) 85,
        (byte) 53,
        (byte) 217,
        (byte) 205,
        (byte) 202,
        (byte) 107,
        (byte) 36
      };
      byte[] numArray3 = new byte[55];
      numArray3[44] = (byte) 229;
      numArray3[1] = (byte) 115;
      numArray3[52] = (byte) 132;
      numArray3[16 /*0x10*/] = (byte) 59;
      numArray3[9] = (byte) 80 /*0x50*/;
      numArray3[5] = (byte) 194;
      numArray3[6] = (byte) 111;
      numArray3[7] = (byte) 22;
      numArray3[53] = (byte) 177;
      numArray3[2] = (byte) 164;
      numArray3[10] = (byte) 213;
      numArray3[48 /*0x30*/] = (byte) 152;
      numArray3[3] = (byte) 183;
      numArray3[13] = (byte) 154;
      numArray3[24] = (byte) 99;
      numArray3[15] = (byte) 158;
      numArray3[4] = (byte) 190;
      numArray3[34] = (byte) 133;
      numArray3[25] = (byte) 218;
      numArray3[29] = (byte) 88;
      numArray3[8] = (byte) 225;
      numArray3[21] = (byte) 163;
      numArray3[22] = (byte) 109;
      numArray3[23] = (byte) 27;
      numArray3[42] = (byte) 241;
      numArray3[49] = (byte) 19;
      numArray3[26] = (byte) 66;
      numArray3[27] = (byte) 54;
      numArray3[28] = (byte) 0;
      numArray3[14] = (byte) 141;
      numArray3[30] = (byte) 234;
      numArray3[31 /*0x1F*/] = (byte) 171;
      numArray3[32 /*0x20*/] = (byte) 47;
      numArray3[47] = (byte) 214;
      numArray3[19] = (byte) 195;
      numArray3[35] = (byte) 190;
      numArray3[17] = (byte) 184;
      numArray3[37] = (byte) 5;
      numArray3[38] = (byte) 146;
      numArray3[0] = (byte) 42;
      numArray3[39] = (byte) 67;
      numArray3[41] = (byte) 107;
      numArray3[11] = (byte) 194;
      numArray3[43] = (byte) 5;
      numArray3[18] = (byte) 195;
      numArray3[45] = (byte) 5;
      numArray3[46] = (byte) 168;
      numArray3[40] = (byte) 68;
      numArray3[12] = (byte) 46;
      numArray3[51] = (byte) 25;
      numArray3[20] = (byte) 71;
      numArray3[50] = (byte) 53;
      numArray3[36] = (byte) 74;
      numArray3[33] = (byte) 29;
      numArray3[54] = (byte) 210;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15]
      {
        (byte) 170,
        (byte) 68,
        (byte) 109,
        (byte) 98,
        (byte) 21,
        (byte) 135,
        (byte) 90,
        (byte) 147,
        (byte) 42,
        (byte) 206,
        (byte) 221,
        (byte) 164,
        (byte) 223,
        (byte) 76,
        (byte) 254
      };
      byte[] numArray5 = new byte[15];
      numArray5[14] = (byte) 124;
      numArray5[1] = (byte) 212;
      numArray5[2] = (byte) 175;
      numArray5[9] = (byte) 108;
      numArray5[3] = (byte) 37;
      numArray5[4] = (byte) 65;
      numArray5[6] = (byte) 81;
      numArray5[11] = (byte) 217;
      numArray5[13] = (byte) 121;
      numArray5[8] = (byte) 72;
      numArray5[10] = (byte) 201;
      numArray5[0] = (byte) 80 /*0x50*/;
      numArray5[12] = (byte) 59;
      numArray5[7] = (byte) 126;
      numArray5[5] = (byte) 193;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55]
    {
      (byte) 23,
      (byte) 75,
      (byte) 47,
      (byte) 204,
      (byte) 223,
      (byte) 47,
      (byte) 40,
      (byte) 111,
      (byte) 234,
      (byte) 136,
      (byte) 227,
      (byte) 166,
      (byte) 181,
      (byte) 88,
      (byte) 165,
      (byte) 102,
      (byte) 242,
      (byte) 133,
      (byte) 25,
      (byte) 232,
      (byte) 8,
      (byte) 254,
      (byte) 159,
      (byte) 216,
      (byte) 151,
      (byte) 22,
      (byte) 166,
      (byte) 146,
      (byte) 97,
      (byte) 6,
      (byte) 18,
      (byte) 45,
      (byte) 91,
      (byte) 17,
      (byte) 199,
      (byte) 1,
      (byte) 207,
      (byte) 4,
      (byte) 34,
      (byte) 215,
      (byte) 30,
      (byte) 63 /*0x3F*/,
      (byte) 61,
      (byte) 131,
      (byte) 136,
      (byte) 47,
      (byte) 150,
      (byte) 22,
      (byte) 79,
      (byte) 146,
      (byte) 154,
      (byte) 147,
      (byte) 135,
      (byte) 154,
      (byte) 139
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 117,
      (byte) 110,
      (byte) 129,
      (byte) 160 /*0xA0*/,
      (byte) 32 /*0x20*/,
      (byte) 201,
      (byte) 113,
      (byte) 187,
      (byte) 34,
      (byte) 134,
      (byte) 246,
      (byte) 117,
      (byte) 55,
      (byte) 137,
      (byte) 244,
      (byte) 238,
      (byte) 96 /*0x60*/,
      (byte) 40,
      (byte) 154,
      (byte) 45,
      (byte) 104,
      (byte) 244,
      (byte) 155,
      (byte) 240 /*0xF0*/,
      (byte) 115,
      (byte) 23,
      (byte) 233,
      (byte) 125,
      (byte) 130,
      (byte) 142,
      (byte) 247,
      (byte) 201,
      (byte) 168,
      (byte) 66,
      (byte) 110,
      (byte) 64 /*0x40*/,
      (byte) 14,
      (byte) 44,
      (byte) 77,
      (byte) 244,
      (byte) 198,
      (byte) 12,
      (byte) 29,
      (byte) 230,
      (byte) 234,
      (byte) 103,
      (byte) 130,
      (byte) 119,
      (byte) 52,
      (byte) 55,
      (byte) 190,
      (byte) 1,
      (byte) 37,
      (byte) 43,
      (byte) 36
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15];
    numArray9[0] = (byte) 87;
    numArray9[1] = (byte) 52;
    numArray9[4] = (byte) 149;
    numArray9[12] = (byte) 164;
    numArray9[9] = (byte) 74;
    numArray9[5] = (byte) 14;
    numArray9[13] = (byte) 151;
    numArray9[6] = (byte) 228;
    numArray9[8] = (byte) 181;
    numArray9[7] = (byte) 117;
    numArray9[10] = (byte) 163;
    numArray9[11] = (byte) 144 /*0x90*/;
    numArray9[3] = (byte) 137;
    numArray9[2] = (byte) 200;
    numArray9[14] = (byte) 238;
    byte[] numArray10 = new byte[15]
    {
      (byte) 221,
      (byte) 240 /*0xF0*/,
      (byte) 237,
      (byte) 174,
      (byte) 135,
      (byte) 220,
      (byte) 53,
      (byte) 2,
      (byte) 192 /*0xC0*/,
      (byte) 11,
      (byte) 50,
      (byte) 253,
      (byte) 89,
      (byte) 232,
      (byte) 86
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[34];
    byte[] response = new byte[34];
    Array.Copy((Array) sc_12586.sspq, 62, (Array) numArray11, 0, 34);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12586.sspr, 62, (Array) numArray11, 0, 34);
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

  internal static string ssp_appserver_12601()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[76];
      byte[] numArray2 = new byte[55]
      {
        (byte) 30,
        (byte) 8,
        (byte) 243,
        (byte) 65,
        (byte) 150,
        (byte) 102,
        (byte) 15,
        (byte) 125,
        (byte) 131,
        (byte) 142,
        (byte) 9,
        (byte) 153,
        (byte) 211,
        (byte) 192 /*0xC0*/,
        (byte) 78,
        (byte) 97,
        (byte) 241,
        (byte) 195,
        (byte) 85,
        (byte) 198,
        (byte) 111,
        (byte) 3,
        (byte) 17,
        (byte) 85,
        (byte) 54,
        (byte) 242,
        (byte) 225,
        (byte) 22,
        (byte) 194,
        (byte) 91,
        (byte) 212,
        (byte) 163,
        (byte) 90,
        (byte) 218,
        (byte) 141,
        (byte) 39,
        (byte) 81,
        (byte) 142,
        (byte) 43,
        (byte) 242,
        (byte) 127 /*0x7F*/,
        (byte) 138,
        (byte) 72,
        (byte) 185,
        (byte) 239,
        (byte) 184,
        (byte) 108,
        (byte) 183,
        (byte) 168,
        (byte) 174,
        (byte) 151,
        (byte) 89,
        (byte) 139,
        (byte) 136,
        (byte) 177
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 187,
        (byte) 150,
        (byte) 65,
        (byte) 7,
        (byte) 217,
        (byte) 253,
        (byte) 108,
        (byte) 223,
        (byte) 184,
        (byte) 46,
        (byte) 117,
        (byte) 131,
        (byte) 145,
        (byte) 22,
        (byte) 244,
        (byte) 133,
        (byte) 39,
        (byte) 11,
        (byte) 218,
        (byte) 84,
        (byte) 82,
        (byte) 92,
        (byte) 84,
        (byte) 20,
        (byte) 162,
        (byte) 56,
        (byte) 98,
        (byte) 190,
        (byte) 140,
        (byte) 202,
        (byte) 165,
        (byte) 239,
        (byte) 156,
        (byte) 110,
        (byte) 117,
        (byte) 60,
        (byte) 51,
        (byte) 0,
        (byte) 161,
        byte.MaxValue,
        (byte) 178,
        (byte) 179,
        (byte) 229,
        (byte) 160 /*0xA0*/,
        (byte) 156,
        (byte) 26,
        (byte) 139,
        (byte) 74,
        (byte) 252,
        (byte) 218,
        (byte) 16 /*0x10*/,
        (byte) 115,
        (byte) 63 /*0x3F*/,
        (byte) 95,
        (byte) 157
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[21]
      {
        (byte) 158,
        (byte) 11,
        (byte) 164,
        (byte) 132,
        (byte) 248,
        (byte) 186,
        (byte) 39,
        (byte) 185,
        (byte) 101,
        (byte) 62,
        (byte) 249,
        (byte) 51,
        (byte) 38,
        (byte) 16 /*0x10*/,
        (byte) 99,
        (byte) 152,
        (byte) 99,
        (byte) 70,
        (byte) 229,
        (byte) 187,
        (byte) 87
      };
      byte[] numArray5 = new byte[21]
      {
        (byte) 181,
        (byte) 176 /*0xB0*/,
        (byte) 180,
        (byte) 154,
        (byte) 0,
        (byte) 246,
        (byte) 20,
        (byte) 221,
        (byte) 197,
        (byte) 175,
        (byte) 194,
        (byte) 131,
        (byte) 32 /*0x20*/,
        (byte) 206,
        (byte) 162,
        (byte) 211,
        (byte) 237,
        (byte) 106,
        (byte) 120,
        (byte) 117,
        (byte) 220
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 21);
      for (int index = 0; index < 21; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_12586.sspq, 96 /*0x60*/, (Array) numArray6, 0, 49);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12586.sspr, 96 /*0x60*/, (Array) numArray6, 0, 49);
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
    byte[] numArray7 = new byte[76];
    byte[] numArray8 = new byte[55];
    numArray8[18] = (byte) 18;
    numArray8[43] = (byte) 132;
    numArray8[6] = (byte) 236;
    numArray8[3] = (byte) 2;
    numArray8[4] = (byte) 19;
    numArray8[7] = (byte) 118;
    numArray8[40] = (byte) 157;
    numArray8[45] = (byte) 219;
    numArray8[26] = (byte) 196;
    numArray8[29] = (byte) 84;
    numArray8[10] = (byte) 239;
    numArray8[11] = (byte) 144 /*0x90*/;
    numArray8[12] = (byte) 93;
    numArray8[13] = (byte) 19;
    numArray8[46] = (byte) 115;
    numArray8[27] = (byte) 138;
    numArray8[31 /*0x1F*/] = (byte) 50;
    numArray8[24] = (byte) 14;
    numArray8[5] = (byte) 165;
    numArray8[37] = (byte) 139;
    numArray8[20] = (byte) 42;
    numArray8[15] = (byte) 121;
    numArray8[22] = (byte) 195;
    numArray8[19] = (byte) 252;
    numArray8[32 /*0x20*/] = (byte) 129;
    numArray8[36] = (byte) 94;
    numArray8[9] = (byte) 185;
    numArray8[21] = (byte) 190;
    numArray8[28] = (byte) 65;
    numArray8[8] = (byte) 144 /*0x90*/;
    numArray8[25] = (byte) 162;
    numArray8[34] = (byte) 197;
    numArray8[35] = (byte) 72;
    numArray8[33] = (byte) 182;
    numArray8[1] = (byte) 87;
    numArray8[16 /*0x10*/] = (byte) 244;
    numArray8[17] = (byte) 104;
    numArray8[2] = (byte) 218;
    numArray8[38] = (byte) 11;
    numArray8[39] = (byte) 181;
    numArray8[41] = (byte) 46;
    numArray8[14] = (byte) 145;
    numArray8[42] = (byte) 210;
    numArray8[44] = (byte) 94;
    numArray8[47] = (byte) 172;
    numArray8[50] = (byte) 235;
    numArray8[23] = (byte) 60;
    numArray8[30] = (byte) 176 /*0xB0*/;
    numArray8[48 /*0x30*/] = (byte) 101;
    numArray8[49] = (byte) 227;
    numArray8[52] = (byte) 227;
    numArray8[51] = (byte) 168;
    numArray8[0] = (byte) 153;
    numArray8[53] = (byte) 195;
    numArray8[54] = (byte) 234;
    byte[] numArray9 = new byte[55]
    {
      (byte) 63 /*0x3F*/,
      (byte) 253,
      (byte) 135,
      (byte) 228,
      (byte) 75,
      (byte) 75,
      (byte) 185,
      (byte) 174,
      (byte) 212,
      (byte) 4,
      (byte) 71,
      (byte) 94,
      (byte) 110,
      (byte) 134,
      (byte) 115,
      (byte) 92,
      (byte) 95,
      (byte) 88,
      (byte) 21,
      (byte) 168,
      (byte) 135,
      (byte) 73,
      (byte) 92,
      (byte) 84,
      (byte) 73,
      (byte) 212,
      (byte) 37,
      (byte) 10,
      (byte) 76,
      (byte) 74,
      (byte) 22,
      (byte) 68,
      (byte) 125,
      (byte) 242,
      (byte) 169,
      (byte) 96 /*0x60*/,
      (byte) 162,
      (byte) 156,
      (byte) 193,
      (byte) 121,
      (byte) 168,
      (byte) 42,
      (byte) 229,
      (byte) 208 /*0xD0*/,
      (byte) 108,
      (byte) 145,
      (byte) 106,
      (byte) 86,
      (byte) 29,
      (byte) 158,
      (byte) 165,
      (byte) 91,
      (byte) 186,
      (byte) 206,
      (byte) 229
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[21]
    {
      (byte) 191,
      (byte) 194,
      (byte) 64 /*0x40*/,
      (byte) 199,
      (byte) 59,
      (byte) 215,
      (byte) 211,
      (byte) 0,
      (byte) 111,
      (byte) 79,
      (byte) 217,
      (byte) 99,
      (byte) 149,
      (byte) 91,
      (byte) 210,
      (byte) 192 /*0xC0*/,
      (byte) 134,
      (byte) 34,
      (byte) 3,
      (byte) 86,
      (byte) 194
    };
    byte[] numArray11 = new byte[21]
    {
      (byte) 193,
      (byte) 2,
      (byte) 179,
      (byte) 180,
      (byte) 60,
      (byte) 183,
      (byte) 21,
      (byte) 38,
      (byte) 37,
      (byte) 149,
      (byte) 62,
      (byte) 217,
      (byte) 18,
      (byte) 128 /*0x80*/,
      (byte) 153,
      (byte) 94,
      (byte) 104,
      (byte) 232,
      (byte) 63 /*0x3F*/,
      (byte) 129,
      (byte) 254
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 21);
    for (int index = 0; index < 21; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12602()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 10,
        (byte) 149,
        (byte) 230,
        (byte) 187,
        (byte) 198,
        (byte) 90,
        (byte) 222,
        (byte) 252,
        (byte) 94
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 190,
        (byte) 18,
        (byte) 246,
        (byte) 168,
        (byte) 15,
        (byte) 157,
        (byte) 206,
        (byte) 2,
        (byte) 122
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[1] = (byte) 0;
    numArray5[7] = (byte) 243;
    numArray5[2] = (byte) 32 /*0x20*/;
    numArray5[3] = (byte) 194;
    numArray5[0] = (byte) 13;
    numArray5[5] = (byte) 48 /*0x30*/;
    numArray5[8] = (byte) 250;
    numArray5[6] = (byte) 125;
    numArray5[4] = (byte) 122;
    byte[] numArray6 = new byte[9]
    {
      (byte) 102,
      (byte) 75,
      (byte) 60,
      (byte) 111,
      (byte) 189,
      (byte) 32 /*0x20*/,
      (byte) 14,
      (byte) 141,
      (byte) 72
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12603()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 49,
        (byte) 65,
        (byte) 236
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 0,
        (byte) 154,
        (byte) 3
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
      (byte) 91,
      (byte) 254,
      (byte) 102
    };
    byte[] numArray6 = new byte[3]
    {
      (byte) 0,
      (byte) 0,
      (byte) 202
    };
    numArray6[0] = (byte) 205;
    numArray6[1] = (byte) 202;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12604()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[70];
      byte[] numArray2 = new byte[55]
      {
        (byte) 111,
        (byte) 192 /*0xC0*/,
        (byte) 131,
        (byte) 41,
        (byte) 55,
        (byte) 47,
        (byte) 197,
        (byte) 24,
        (byte) 243,
        (byte) 100,
        (byte) 204,
        (byte) 126,
        (byte) 119,
        (byte) 123,
        (byte) 0,
        (byte) 206,
        (byte) 222,
        (byte) 214,
        (byte) 53,
        (byte) 135,
        (byte) 135,
        (byte) 248,
        (byte) 147,
        (byte) 80 /*0x50*/,
        (byte) 17,
        (byte) 176 /*0xB0*/,
        (byte) 37,
        (byte) 135,
        (byte) 30,
        (byte) 8,
        (byte) 4,
        (byte) 74,
        (byte) 115,
        (byte) 117,
        (byte) 111,
        (byte) 76,
        (byte) 174,
        (byte) 24,
        (byte) 180,
        (byte) 22,
        (byte) 227,
        (byte) 221,
        (byte) 6,
        (byte) 2,
        (byte) 106,
        (byte) 220,
        (byte) 135,
        (byte) 234,
        (byte) 121,
        (byte) 84,
        (byte) 3,
        (byte) 188,
        (byte) 18,
        (byte) 134,
        (byte) 95
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 137,
        (byte) 201,
        (byte) 186,
        (byte) 225,
        (byte) 2,
        (byte) 92,
        (byte) 20,
        (byte) 232,
        (byte) 251,
        (byte) 64 /*0x40*/,
        (byte) 78,
        (byte) 231,
        (byte) 237,
        (byte) 48 /*0x30*/,
        (byte) 110,
        (byte) 99,
        (byte) 252,
        (byte) 211,
        (byte) 211,
        (byte) 221,
        (byte) 94,
        (byte) 112 /*0x70*/,
        (byte) 233,
        (byte) 44,
        (byte) 71,
        (byte) 76,
        (byte) 182,
        (byte) 173,
        (byte) 249,
        (byte) 138,
        (byte) 88,
        (byte) 225,
        (byte) 36,
        (byte) 39,
        (byte) 235,
        (byte) 156,
        (byte) 133,
        (byte) 156,
        (byte) 36,
        (byte) 229,
        (byte) 77,
        (byte) 180,
        (byte) 210,
        (byte) 85,
        (byte) 95,
        (byte) 108,
        (byte) 44,
        (byte) 188,
        (byte) 246,
        (byte) 2,
        (byte) 238,
        (byte) 169,
        (byte) 237,
        (byte) 158,
        (byte) 61
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15]
      {
        (byte) 51,
        (byte) 24,
        (byte) 135,
        (byte) 10,
        (byte) 62,
        (byte) 249,
        (byte) 235,
        (byte) 95,
        (byte) 129,
        (byte) 10,
        (byte) 1,
        (byte) 12,
        (byte) 179,
        (byte) 31 /*0x1F*/,
        (byte) 198
      };
      byte[] numArray5 = new byte[15];
      numArray5[9] = (byte) 245;
      numArray5[1] = (byte) 210;
      numArray5[13] = (byte) 115;
      numArray5[7] = (byte) 152;
      numArray5[4] = (byte) 22;
      numArray5[5] = (byte) 151;
      numArray5[6] = (byte) 202;
      numArray5[14] = (byte) 126;
      numArray5[8] = (byte) 205;
      numArray5[3] = (byte) 167;
      numArray5[10] = (byte) 226;
      numArray5[11] = (byte) 125;
      numArray5[12] = (byte) 193;
      numArray5[2] = (byte) 0;
      numArray5[0] = (byte) 212;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[70];
    byte[] numArray7 = new byte[55]
    {
      (byte) 31 /*0x1F*/,
      (byte) 123,
      (byte) 0,
      (byte) 44,
      (byte) 12,
      (byte) 164,
      (byte) 227,
      (byte) 27,
      (byte) 163,
      (byte) 232,
      (byte) 209,
      (byte) 134,
      (byte) 222,
      (byte) 197,
      (byte) 52,
      (byte) 60,
      (byte) 154,
      (byte) 244,
      (byte) 226,
      (byte) 181,
      (byte) 132,
      (byte) 157,
      (byte) 77,
      (byte) 226,
      (byte) 94,
      (byte) 207,
      (byte) 48 /*0x30*/,
      (byte) 47,
      (byte) 230,
      (byte) 23,
      (byte) 74,
      (byte) 29,
      (byte) 235,
      (byte) 19,
      (byte) 148,
      (byte) 164,
      (byte) 198,
      (byte) 245,
      (byte) 18,
      byte.MaxValue,
      (byte) 198,
      (byte) 214,
      (byte) 236,
      (byte) 253,
      (byte) 99,
      (byte) 162,
      (byte) 92,
      (byte) 72,
      (byte) 149,
      (byte) 199,
      (byte) 173,
      (byte) 67,
      (byte) 11,
      (byte) 96 /*0x60*/,
      (byte) 116
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 84,
      (byte) 92,
      (byte) 76,
      (byte) 183,
      (byte) 119,
      (byte) 97,
      (byte) 90,
      (byte) 221,
      (byte) 87,
      (byte) 250,
      (byte) 195,
      (byte) 41,
      (byte) 111,
      (byte) 3,
      (byte) 44,
      (byte) 77,
      (byte) 163,
      (byte) 104,
      (byte) 207,
      (byte) 191,
      (byte) 151,
      (byte) 144 /*0x90*/,
      (byte) 28,
      (byte) 159,
      (byte) 29,
      (byte) 86,
      (byte) 232,
      (byte) 243,
      (byte) 211,
      (byte) 237,
      (byte) 213,
      (byte) 227,
      (byte) 121,
      (byte) 242,
      (byte) 150,
      (byte) 218,
      (byte) 53,
      (byte) 251,
      (byte) 76,
      (byte) 62,
      (byte) 44,
      (byte) 150,
      (byte) 251,
      (byte) 30,
      (byte) 5,
      (byte) 48 /*0x30*/,
      (byte) 85,
      (byte) 192 /*0xC0*/,
      (byte) 95,
      (byte) 74,
      (byte) 92,
      (byte) 32 /*0x20*/,
      (byte) 78,
      (byte) 156,
      (byte) 120
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[15]
    {
      (byte) 29,
      (byte) 206,
      (byte) 60,
      (byte) 116,
      (byte) 180,
      (byte) 52,
      (byte) 140,
      (byte) 158,
      (byte) 180,
      (byte) 154,
      (byte) 202,
      (byte) 49,
      (byte) 143,
      (byte) 118,
      (byte) 165
    };
    byte[] numArray10 = new byte[15];
    numArray10[13] = (byte) 101;
    numArray10[2] = (byte) 108;
    numArray10[8] = (byte) 220;
    numArray10[3] = (byte) 241;
    numArray10[10] = (byte) 99;
    numArray10[0] = (byte) 102;
    numArray10[1] = (byte) 186;
    numArray10[5] = (byte) 244;
    numArray10[12] = (byte) 186;
    numArray10[9] = (byte) 66;
    numArray10[4] = (byte) 2;
    numArray10[11] = (byte) 148;
    numArray10[7] = (byte) 30;
    numArray10[6] = (byte) 251;
    numArray10[14] = (byte) 148;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 15);
    for (int index = 0; index < 15; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[30];
    byte[] response = new byte[30];
    Array.Copy((Array) sc_12586.sspq, 145, (Array) numArray11, 0, 30);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12586.sspr, 145, (Array) numArray11, 0, 30);
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

  internal static string ssp_appserver_12605()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 43,
        (byte) 31 /*0x1F*/,
        (byte) 25,
        (byte) 206,
        (byte) 126,
        (byte) 10,
        (byte) 87,
        (byte) 219,
        (byte) 11
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 95,
        (byte) 54,
        (byte) 231,
        (byte) 82,
        (byte) 67,
        (byte) 196,
        (byte) 146,
        (byte) 103,
        (byte) 254
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 179,
      (byte) 225,
      (byte) 209,
      (byte) 147,
      (byte) 205,
      (byte) 96 /*0x60*/,
      (byte) 164,
      (byte) 232,
      (byte) 25
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 198,
      (byte) 129,
      (byte) 167,
      (byte) 65,
      (byte) 197,
      (byte) 230,
      (byte) 83,
      (byte) 200,
      (byte) 185
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12606()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[101];
      byte[] numArray2 = new byte[55];
      numArray2[47] = (byte) 214;
      numArray2[1] = (byte) 13;
      numArray2[36] = (byte) 87;
      numArray2[3] = (byte) 247;
      numArray2[49] = (byte) 23;
      numArray2[37] = (byte) 228;
      numArray2[6] = (byte) 200;
      numArray2[0] = (byte) 19;
      numArray2[30] = (byte) 83;
      numArray2[9] = (byte) 102;
      numArray2[10] = (byte) 246;
      numArray2[32 /*0x20*/] = (byte) 130;
      numArray2[34] = (byte) 106;
      numArray2[13] = (byte) 138;
      numArray2[17] = (byte) 186;
      numArray2[14] = (byte) 87;
      numArray2[5] = (byte) 61;
      numArray2[27] = (byte) 243;
      numArray2[33] = (byte) 196;
      numArray2[18] = (byte) 129;
      numArray2[8] = (byte) 249;
      numArray2[21] = (byte) 95;
      numArray2[16 /*0x10*/] = (byte) 149;
      numArray2[23] = (byte) 185;
      numArray2[24] = (byte) 135;
      numArray2[11] = (byte) 236;
      numArray2[26] = (byte) 194;
      numArray2[22] = (byte) 231;
      numArray2[28] = (byte) 186;
      numArray2[48 /*0x30*/] = (byte) 76;
      numArray2[50] = (byte) 131;
      numArray2[2] = byte.MaxValue;
      numArray2[4] = (byte) 139;
      numArray2[20] = (byte) 206;
      numArray2[19] = (byte) 239;
      numArray2[25] = (byte) 72;
      numArray2[41] = (byte) 195;
      numArray2[12] = (byte) 130;
      numArray2[15] = (byte) 209;
      numArray2[39] = (byte) 19;
      numArray2[40] = (byte) 39;
      numArray2[51] = (byte) 29;
      numArray2[42] = (byte) 186;
      numArray2[43] = (byte) 123;
      numArray2[44] = (byte) 218;
      numArray2[31 /*0x1F*/] = (byte) 223;
      numArray2[46] = (byte) 74;
      numArray2[38] = (byte) 128 /*0x80*/;
      numArray2[35] = (byte) 252;
      numArray2[7] = (byte) 12;
      numArray2[45] = (byte) 166;
      numArray2[29] = (byte) 245;
      numArray2[52] = (byte) 190;
      numArray2[53] = (byte) 167;
      numArray2[54] = (byte) 59;
      byte[] numArray3 = new byte[55]
      {
        (byte) 11,
        (byte) 40,
        (byte) 141,
        (byte) 160 /*0xA0*/,
        (byte) 152,
        (byte) 213,
        (byte) 21,
        (byte) 123,
        (byte) 10,
        (byte) 146,
        (byte) 118,
        (byte) 107,
        (byte) 253,
        (byte) 254,
        (byte) 228,
        (byte) 196,
        (byte) 207,
        (byte) 102,
        (byte) 174,
        (byte) 75,
        (byte) 63 /*0x3F*/,
        (byte) 191,
        (byte) 249,
        (byte) 249,
        (byte) 70,
        (byte) 108,
        (byte) 252,
        (byte) 121,
        (byte) 181,
        (byte) 160 /*0xA0*/,
        (byte) 222,
        (byte) 16 /*0x10*/,
        (byte) 210,
        (byte) 136,
        (byte) 121,
        (byte) 76,
        (byte) 196,
        (byte) 24,
        (byte) 96 /*0x60*/,
        (byte) 190,
        (byte) 194,
        (byte) 187,
        (byte) 144 /*0x90*/,
        (byte) 94,
        (byte) 136,
        (byte) 189,
        (byte) 169,
        (byte) 38,
        (byte) 182,
        (byte) 39,
        (byte) 88,
        (byte) 26,
        (byte) 120,
        (byte) 190,
        (byte) 54
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46]
      {
        (byte) 102,
        (byte) 143,
        (byte) 70,
        (byte) 143,
        (byte) 45,
        (byte) 174,
        (byte) 145,
        (byte) 220,
        (byte) 246,
        (byte) 144 /*0x90*/,
        (byte) 54,
        (byte) 104,
        (byte) 132,
        (byte) 103,
        (byte) 61,
        (byte) 254,
        (byte) 213,
        (byte) 117,
        (byte) 154,
        (byte) 239,
        (byte) 3,
        (byte) 27,
        (byte) 164,
        (byte) 113,
        (byte) 52,
        (byte) 181,
        (byte) 5,
        (byte) 160 /*0xA0*/,
        (byte) 83,
        (byte) 207,
        (byte) 250,
        (byte) 88,
        (byte) 168,
        (byte) 28,
        (byte) 171,
        (byte) 229,
        (byte) 246,
        (byte) 8,
        (byte) 37,
        (byte) 194,
        (byte) 37,
        (byte) 140,
        (byte) 204,
        (byte) 172,
        (byte) 86,
        (byte) 78
      };
      byte[] numArray5 = new byte[46]
      {
        (byte) 150,
        (byte) 198,
        (byte) 147,
        (byte) 239,
        (byte) 117,
        (byte) 159,
        (byte) 109,
        (byte) 242,
        (byte) 232,
        (byte) 179,
        (byte) 181,
        (byte) 157,
        (byte) 216,
        (byte) 212,
        (byte) 222,
        (byte) 159,
        (byte) 57,
        (byte) 161,
        (byte) 74,
        (byte) 191,
        (byte) 133,
        (byte) 171,
        (byte) 75,
        (byte) 222,
        (byte) 158,
        (byte) 85,
        (byte) 38,
        (byte) 119,
        (byte) 232,
        (byte) 208 /*0xD0*/,
        (byte) 39,
        (byte) 44,
        (byte) 89,
        (byte) 49,
        (byte) 247,
        (byte) 199,
        (byte) 230,
        (byte) 151,
        (byte) 188,
        (byte) 167,
        (byte) 96 /*0x60*/,
        (byte) 92,
        (byte) 40,
        (byte) 225,
        (byte) 134,
        (byte) 150
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[101];
    byte[] numArray7 = new byte[55]
    {
      (byte) 228,
      (byte) 122,
      (byte) 105,
      (byte) 149,
      (byte) 30,
      (byte) 182,
      (byte) 75,
      (byte) 234,
      (byte) 212,
      (byte) 91,
      (byte) 119,
      (byte) 169,
      (byte) 240 /*0xF0*/,
      (byte) 136,
      (byte) 38,
      (byte) 149,
      (byte) 221,
      (byte) 192 /*0xC0*/,
      (byte) 103,
      (byte) 188,
      (byte) 88,
      (byte) 7,
      (byte) 205,
      (byte) 80 /*0x50*/,
      (byte) 216,
      (byte) 201,
      (byte) 119,
      (byte) 78,
      (byte) 247,
      (byte) 27,
      (byte) 229,
      (byte) 72,
      (byte) 206,
      (byte) 12,
      (byte) 111,
      (byte) 84,
      (byte) 144 /*0x90*/,
      (byte) 195,
      (byte) 137,
      (byte) 101,
      (byte) 95,
      (byte) 250,
      (byte) 33,
      (byte) 172,
      (byte) 173,
      (byte) 24,
      (byte) 52,
      (byte) 3,
      (byte) 165,
      (byte) 23,
      (byte) 120,
      (byte) 141,
      (byte) 5,
      (byte) 122,
      (byte) 60
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 18,
      (byte) 219,
      (byte) 94,
      (byte) 207,
      (byte) 90,
      (byte) 217,
      (byte) 105,
      (byte) 45,
      (byte) 69,
      (byte) 188,
      (byte) 49,
      (byte) 169,
      (byte) 195,
      (byte) 36,
      (byte) 116,
      (byte) 112 /*0x70*/,
      (byte) 49,
      (byte) 219,
      (byte) 197,
      (byte) 208 /*0xD0*/,
      (byte) 31 /*0x1F*/,
      (byte) 38,
      (byte) 146,
      (byte) 74,
      (byte) 220,
      (byte) 28,
      (byte) 22,
      (byte) 159,
      (byte) 170,
      (byte) 249,
      (byte) 182,
      (byte) 21,
      (byte) 7,
      (byte) 196,
      (byte) 40,
      (byte) 238,
      (byte) 241,
      (byte) 115,
      (byte) 88,
      (byte) 36,
      (byte) 71,
      (byte) 131,
      (byte) 33,
      (byte) 153,
      (byte) 3,
      (byte) 54,
      (byte) 149,
      (byte) 51,
      (byte) 172,
      (byte) 199,
      (byte) 176 /*0xB0*/,
      (byte) 69,
      (byte) 65,
      (byte) 200,
      (byte) 232
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[46];
    numArray9[21] = (byte) 162;
    numArray9[1] = (byte) 188;
    numArray9[2] = (byte) 85;
    numArray9[3] = (byte) 63 /*0x3F*/;
    numArray9[42] = (byte) 126;
    numArray9[5] = (byte) 243;
    numArray9[27] = (byte) 162;
    numArray9[4] = (byte) 252;
    numArray9[24] = (byte) 115;
    numArray9[12] = (byte) 210;
    numArray9[10] = (byte) 228;
    numArray9[11] = (byte) 25;
    numArray9[30] = (byte) 84;
    numArray9[7] = (byte) 117;
    numArray9[14] = (byte) 25;
    numArray9[15] = (byte) 54;
    numArray9[6] = (byte) 185;
    numArray9[9] = (byte) 218;
    numArray9[36] = (byte) 52;
    numArray9[40] = (byte) 104;
    numArray9[20] = (byte) 32 /*0x20*/;
    numArray9[34] = (byte) 119;
    numArray9[22] = (byte) 192 /*0xC0*/;
    numArray9[23] = (byte) 149;
    numArray9[8] = (byte) 47;
    numArray9[29] = (byte) 66;
    numArray9[26] = (byte) 224 /*0xE0*/;
    numArray9[16 /*0x10*/] = (byte) 1;
    numArray9[28] = (byte) 87;
    numArray9[18] = (byte) 94;
    numArray9[13] = (byte) 4;
    numArray9[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    numArray9[37] = (byte) 37;
    numArray9[33] = (byte) 167;
    numArray9[35] = (byte) 6;
    numArray9[0] = (byte) 25;
    numArray9[25] = (byte) 168;
    numArray9[32 /*0x20*/] = (byte) 146;
    numArray9[38] = (byte) 98;
    numArray9[39] = (byte) 6;
    numArray9[45] = (byte) 234;
    numArray9[41] = (byte) 54;
    numArray9[19] = (byte) 229;
    numArray9[43] = (byte) 61;
    numArray9[44] = (byte) 79;
    numArray9[17] = (byte) 92;
    byte[] numArray10 = new byte[46]
    {
      (byte) 218,
      (byte) 18,
      (byte) 56,
      (byte) 142,
      (byte) 63 /*0x3F*/,
      (byte) 44,
      (byte) 160 /*0xA0*/,
      (byte) 216,
      (byte) 15,
      (byte) 225,
      (byte) 169,
      (byte) 78,
      (byte) 106,
      (byte) 32 /*0x20*/,
      (byte) 50,
      (byte) 136,
      (byte) 96 /*0x60*/,
      (byte) 106,
      (byte) 21,
      (byte) 122,
      (byte) 172,
      (byte) 96 /*0x60*/,
      (byte) 224 /*0xE0*/,
      (byte) 162,
      (byte) 31 /*0x1F*/,
      (byte) 43,
      (byte) 91,
      (byte) 198,
      (byte) 96 /*0x60*/,
      (byte) 196,
      (byte) 210,
      (byte) 17,
      (byte) 131,
      (byte) 53,
      (byte) 118,
      (byte) 30,
      (byte) 183,
      (byte) 36,
      (byte) 115,
      (byte) 0,
      (byte) 66,
      (byte) 0,
      (byte) 137,
      (byte) 44,
      (byte) 114,
      (byte) 160 /*0xA0*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 46);
    for (int index = 0; index < 46; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12607()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[80 /*0x50*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 25,
        (byte) 146,
        (byte) 183,
        (byte) 59,
        (byte) 98,
        (byte) 168,
        (byte) 132,
        (byte) 75,
        (byte) 85,
        (byte) 39,
        (byte) 36,
        (byte) 134,
        (byte) 121,
        (byte) 162,
        (byte) 83,
        (byte) 122,
        (byte) 122,
        (byte) 67,
        (byte) 127 /*0x7F*/,
        (byte) 68,
        (byte) 211,
        (byte) 29,
        (byte) 173,
        (byte) 79,
        (byte) 34,
        (byte) 236,
        (byte) 16 /*0x10*/,
        (byte) 141,
        (byte) 9,
        (byte) 141,
        (byte) 130,
        (byte) 235,
        (byte) 98,
        (byte) 44,
        (byte) 208 /*0xD0*/,
        (byte) 144 /*0x90*/,
        (byte) 202,
        (byte) 9,
        (byte) 38,
        (byte) 43,
        (byte) 73,
        (byte) 248,
        (byte) 171,
        (byte) 23,
        (byte) 18,
        (byte) 107,
        (byte) 32 /*0x20*/,
        (byte) 119,
        (byte) 172,
        (byte) 143,
        (byte) 250,
        (byte) 22,
        (byte) 143,
        (byte) 176 /*0xB0*/,
        (byte) 126
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 19,
        (byte) 93,
        (byte) 93,
        (byte) 166,
        (byte) 166,
        (byte) 87,
        (byte) 29,
        (byte) 202,
        (byte) 6,
        (byte) 40,
        (byte) 88,
        (byte) 141,
        (byte) 42,
        (byte) 45,
        (byte) 160 /*0xA0*/,
        (byte) 4,
        (byte) 5,
        (byte) 64 /*0x40*/,
        (byte) 135,
        (byte) 109,
        (byte) 9,
        (byte) 130,
        (byte) 189,
        (byte) 244,
        (byte) 161,
        (byte) 113,
        (byte) 34,
        (byte) 89,
        (byte) 35,
        (byte) 61,
        (byte) 203,
        (byte) 254,
        (byte) 214,
        (byte) 37,
        (byte) 44,
        (byte) 214,
        (byte) 203,
        (byte) 68,
        (byte) 178,
        (byte) 84,
        (byte) 102,
        (byte) 18,
        (byte) 238,
        (byte) 194,
        (byte) 179,
        (byte) 15,
        (byte) 186,
        (byte) 18,
        (byte) 159,
        (byte) 219,
        (byte) 150,
        (byte) 83,
        (byte) 151,
        (byte) 161,
        (byte) 99
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25]
      {
        (byte) 104,
        (byte) 24,
        (byte) 117,
        (byte) 152,
        (byte) 247,
        (byte) 65,
        (byte) 199,
        (byte) 164,
        (byte) 57,
        (byte) 36,
        (byte) 77,
        (byte) 210,
        (byte) 194,
        (byte) 176 /*0xB0*/,
        (byte) 112 /*0x70*/,
        (byte) 28,
        (byte) 83,
        (byte) 250,
        (byte) 219,
        (byte) 113,
        (byte) 149,
        (byte) 61,
        (byte) 183,
        (byte) 203,
        (byte) 152
      };
      byte[] numArray5 = new byte[25]
      {
        (byte) 43,
        (byte) 48 /*0x30*/,
        (byte) 131,
        (byte) 85,
        (byte) 251,
        (byte) 117,
        (byte) 237,
        (byte) 135,
        (byte) 84,
        (byte) 156,
        (byte) 96 /*0x60*/,
        (byte) 251,
        (byte) 59,
        (byte) 246,
        (byte) 14,
        (byte) 22,
        (byte) 188,
        (byte) 142,
        (byte) 96 /*0x60*/,
        (byte) 7,
        (byte) 235,
        (byte) 197,
        (byte) 104,
        (byte) 78,
        (byte) 48 /*0x30*/
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
      (byte) 108,
      (byte) 92,
      (byte) 178,
      (byte) 174,
      (byte) 212,
      (byte) 171,
      (byte) 98,
      (byte) 240 /*0xF0*/,
      (byte) 177,
      (byte) 147,
      (byte) 82,
      (byte) 182,
      (byte) 227,
      (byte) 114,
      (byte) 216,
      (byte) 71,
      (byte) 210,
      (byte) 165,
      (byte) 233,
      (byte) 222,
      (byte) 137,
      (byte) 250,
      (byte) 59,
      (byte) 182,
      (byte) 162,
      (byte) 138,
      (byte) 80 /*0x50*/,
      (byte) 42,
      (byte) 97,
      (byte) 169,
      (byte) 0,
      (byte) 21,
      (byte) 199,
      (byte) 52,
      (byte) 56,
      (byte) 221,
      (byte) 114,
      (byte) 159,
      (byte) 253,
      (byte) 23,
      (byte) 78,
      (byte) 127 /*0x7F*/,
      (byte) 107,
      (byte) 192 /*0xC0*/,
      (byte) 167,
      (byte) 36,
      (byte) 141,
      (byte) 34,
      (byte) 100,
      (byte) 111,
      (byte) 248,
      (byte) 242,
      (byte) 248,
      (byte) 155,
      (byte) 10
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 91,
      (byte) 97,
      (byte) 205,
      (byte) 31 /*0x1F*/,
      (byte) 72,
      (byte) 231,
      (byte) 43,
      (byte) 201,
      (byte) 189,
      (byte) 136,
      (byte) 20,
      (byte) 188,
      (byte) 12,
      (byte) 74,
      (byte) 143,
      (byte) 29,
      (byte) 242,
      (byte) 71,
      (byte) 221,
      (byte) 67,
      (byte) 147,
      (byte) 5,
      (byte) 230,
      (byte) 163,
      (byte) 209,
      (byte) 88,
      (byte) 217,
      (byte) 117,
      (byte) 65,
      (byte) 31 /*0x1F*/,
      (byte) 40,
      (byte) 182,
      (byte) 164,
      (byte) 144 /*0x90*/,
      (byte) 36,
      (byte) 81,
      (byte) 182,
      (byte) 69,
      (byte) 66,
      (byte) 122,
      (byte) 232,
      (byte) 77,
      (byte) 145,
      (byte) 158,
      (byte) 253,
      (byte) 151,
      (byte) 149,
      (byte) 60,
      (byte) 250,
      (byte) 227,
      (byte) 57,
      (byte) 239,
      (byte) 227,
      (byte) 78,
      (byte) 122
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[25]
    {
      (byte) 251,
      (byte) 11,
      (byte) 73,
      (byte) 91,
      (byte) 55,
      (byte) 143,
      (byte) 7,
      (byte) 146,
      (byte) 60,
      (byte) 238,
      (byte) 174,
      (byte) 58,
      (byte) 251,
      (byte) 213,
      (byte) 51,
      (byte) 168,
      (byte) 210,
      (byte) 144 /*0x90*/,
      (byte) 9,
      (byte) 142,
      (byte) 123,
      (byte) 14,
      (byte) 126,
      (byte) 252,
      (byte) 45
    };
    byte[] numArray10 = new byte[25]
    {
      (byte) 178,
      (byte) 94,
      (byte) 220,
      (byte) 139,
      (byte) 230,
      (byte) 17,
      (byte) 170,
      (byte) 104,
      (byte) 128 /*0x80*/,
      (byte) 141,
      (byte) 157,
      (byte) 216,
      (byte) 60,
      (byte) 139,
      (byte) 67,
      (byte) 79,
      (byte) 62,
      (byte) 92,
      (byte) 244,
      (byte) 52,
      (byte) 236,
      (byte) 163,
      (byte) 223,
      (byte) 199,
      (byte) 114
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 25);
    for (int index = 0; index < 25; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[13];
    byte[] response = new byte[13];
    Array.Copy((Array) sc_12586.sspq, 175, (Array) numArray11, 0, 13);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12586.sspr, 175, (Array) numArray11, 0, 13);
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

  internal static int ssp_appserver_12608(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 223,
      (byte) 232,
      (byte) 104,
      (byte) 154,
      (byte) 197,
      (byte) 74,
      (byte) 37,
      (byte) 10,
      (byte) 0,
      (byte) 227,
      (byte) 164,
      (byte) 237,
      (byte) 141,
      (byte) 77,
      (byte) 45,
      (byte) 112 /*0x70*/,
      (byte) 156,
      (byte) 86,
      (byte) 132,
      (byte) 31 /*0x1F*/,
      (byte) 195,
      (byte) 132,
      (byte) 115,
      (byte) 63 /*0x3F*/,
      (byte) 94,
      (byte) 158,
      (byte) 244,
      (byte) 22,
      (byte) 121,
      (byte) 24,
      (byte) 42,
      (byte) 77,
      (byte) 163,
      (byte) 72,
      (byte) 29,
      (byte) 69,
      (byte) 94,
      (byte) 87,
      (byte) 234,
      (byte) 195,
      (byte) 247,
      (byte) 32 /*0x20*/,
      (byte) 41,
      (byte) 226,
      (byte) 23,
      (byte) 136,
      (byte) 7,
      (byte) 102
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 178,
      (byte) 169,
      (byte) 66,
      (byte) 47,
      (byte) 26,
      (byte) 7,
      (byte) 183,
      (byte) 209,
      (byte) 228,
      (byte) 129,
      (byte) 142,
      (byte) 104,
      (byte) 126,
      (byte) 14,
      (byte) 45,
      (byte) 33,
      (byte) 133,
      (byte) 139,
      (byte) 43,
      (byte) 121,
      (byte) 166,
      (byte) 20,
      (byte) 64 /*0x40*/,
      (byte) 164,
      (byte) 164,
      (byte) 164,
      (byte) 81,
      (byte) 127 /*0x7F*/,
      (byte) 143,
      (byte) 186,
      (byte) 19,
      (byte) 2,
      (byte) 177,
      (byte) 159,
      (byte) 146,
      (byte) 172,
      (byte) 171,
      (byte) 199,
      (byte) 223,
      (byte) 17,
      (byte) 157,
      (byte) 86,
      (byte) 219,
      (byte) 99,
      (byte) 203,
      (byte) 105,
      (byte) 135,
      (byte) 185
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[33];
    byte[] response2 = new byte[33];
    Array.Copy((Array) sc_12586.sspq, 188, (Array) numArray2, 0, 33);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12586.sspr, 188, (Array) numArray2, 0, 33);
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

  internal static string ssp_appserver_12609()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[71];
      byte[] numArray2 = new byte[55]
      {
        (byte) 164,
        (byte) 87,
        (byte) 213,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 52,
        (byte) 27,
        (byte) 195,
        (byte) 109,
        (byte) 231,
        (byte) 143,
        (byte) 64 /*0x40*/,
        (byte) 42,
        (byte) 192 /*0xC0*/,
        (byte) 61,
        (byte) 194,
        (byte) 55,
        (byte) 166,
        (byte) 147,
        (byte) 14,
        (byte) 161,
        (byte) 232,
        (byte) 35,
        (byte) 208 /*0xD0*/,
        (byte) 0,
        (byte) 228,
        (byte) 2,
        (byte) 101,
        (byte) 145,
        (byte) 200,
        (byte) 136,
        (byte) 189,
        (byte) 36,
        (byte) 155,
        (byte) 178,
        (byte) 242,
        (byte) 111,
        (byte) 108,
        (byte) 24,
        (byte) 222,
        (byte) 41,
        (byte) 168,
        (byte) 83,
        (byte) 64 /*0x40*/,
        (byte) 18,
        (byte) 25,
        (byte) 102,
        (byte) 102,
        (byte) 152,
        (byte) 211,
        (byte) 86,
        (byte) 225,
        (byte) 64 /*0x40*/,
        (byte) 181,
        (byte) 123
      };
      byte[] numArray3 = new byte[55];
      numArray3[17] = (byte) 20;
      numArray3[8] = (byte) 17;
      numArray3[2] = (byte) 41;
      numArray3[3] = (byte) 26;
      numArray3[42] = (byte) 203;
      numArray3[5] = (byte) 117;
      numArray3[33] = (byte) 124;
      numArray3[48 /*0x30*/] = (byte) 16 /*0x10*/;
      numArray3[19] = (byte) 106;
      numArray3[32 /*0x20*/] = (byte) 237;
      numArray3[37] = (byte) 103;
      numArray3[9] = (byte) 177;
      numArray3[12] = (byte) 62;
      numArray3[11] = (byte) 44;
      numArray3[14] = (byte) 123;
      numArray3[51] = (byte) 138;
      numArray3[16 /*0x10*/] = (byte) 185;
      numArray3[35] = (byte) 83;
      numArray3[18] = (byte) 71;
      numArray3[0] = (byte) 136;
      numArray3[20] = (byte) 199;
      numArray3[21] = (byte) 123;
      numArray3[25] = (byte) 195;
      numArray3[1] = (byte) 185;
      numArray3[13] = (byte) 69;
      numArray3[46] = (byte) 136;
      numArray3[26] = (byte) 155;
      numArray3[27] = (byte) 234;
      numArray3[28] = (byte) 120;
      numArray3[29] = (byte) 114;
      numArray3[30] = (byte) 110;
      numArray3[31 /*0x1F*/] = (byte) 188;
      numArray3[40] = (byte) 72;
      numArray3[36] = (byte) 35;
      numArray3[50] = (byte) 175;
      numArray3[6] = (byte) 251;
      numArray3[7] = (byte) 238;
      numArray3[44] = (byte) 143;
      numArray3[52] = (byte) 123;
      numArray3[39] = (byte) 54;
      numArray3[15] = (byte) 150;
      numArray3[22] = (byte) 235;
      numArray3[4] = (byte) 251;
      numArray3[43] = (byte) 145;
      numArray3[23] = (byte) 51;
      numArray3[45] = (byte) 10;
      numArray3[49] = (byte) 248;
      numArray3[47] = (byte) 36;
      numArray3[41] = (byte) 230;
      numArray3[10] = (byte) 218;
      numArray3[53] = (byte) 138;
      numArray3[34] = (byte) 37;
      numArray3[38] = (byte) 206;
      numArray3[24] = (byte) 223;
      numArray3[54] = (byte) 237;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[16 /*0x10*/];
      numArray4[4] = (byte) 233;
      numArray4[2] = (byte) 109;
      numArray4[11] = (byte) 24;
      numArray4[0] = (byte) 72;
      numArray4[10] = (byte) 111;
      numArray4[5] = (byte) 116;
      numArray4[14] = (byte) 32 /*0x20*/;
      numArray4[7] = (byte) 184;
      numArray4[8] = (byte) 85;
      numArray4[9] = (byte) 174;
      numArray4[3] = (byte) 150;
      numArray4[1] = (byte) 219;
      numArray4[12] = (byte) 141;
      numArray4[13] = (byte) 234;
      numArray4[6] = (byte) 49;
      numArray4[15] = (byte) 97;
      byte[] numArray5 = new byte[16 /*0x10*/];
      numArray5[15] = (byte) 31 /*0x1F*/;
      numArray5[1] = (byte) 120;
      numArray5[4] = (byte) 188;
      numArray5[0] = (byte) 89;
      numArray5[12] = (byte) 219;
      numArray5[5] = (byte) 59;
      numArray5[3] = (byte) 235;
      numArray5[2] = (byte) 78;
      numArray5[7] = (byte) 91;
      numArray5[9] = (byte) 199;
      numArray5[14] = (byte) 26;
      numArray5[11] = (byte) 187;
      numArray5[6] = (byte) 227;
      numArray5[10] = (byte) 252;
      numArray5[8] = (byte) 19;
      numArray5[13] = (byte) 217;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[71];
    byte[] numArray7 = new byte[55];
    numArray7[38] = (byte) 86;
    numArray7[1] = (byte) 254;
    numArray7[2] = (byte) 65;
    numArray7[8] = (byte) 105;
    numArray7[28] = (byte) 5;
    numArray7[41] = (byte) 193;
    numArray7[11] = (byte) 101;
    numArray7[12] = (byte) 99;
    numArray7[44] = (byte) 50;
    numArray7[46] = (byte) 134;
    numArray7[54] = (byte) 140;
    numArray7[45] = (byte) 195;
    numArray7[37] = (byte) 27;
    numArray7[13] = (byte) 16 /*0x10*/;
    numArray7[14] = (byte) 208 /*0xD0*/;
    numArray7[15] = (byte) 74;
    numArray7[16 /*0x10*/] = (byte) 33;
    numArray7[39] = (byte) 220;
    numArray7[5] = (byte) 241;
    numArray7[19] = (byte) 162;
    numArray7[0] = (byte) 96 /*0x60*/;
    numArray7[51] = (byte) 204;
    numArray7[22] = (byte) 129;
    numArray7[23] = (byte) 104;
    numArray7[24] = (byte) 216;
    numArray7[27] = (byte) 62;
    numArray7[26] = (byte) 155;
    numArray7[25] = (byte) 146;
    numArray7[9] = (byte) 55;
    numArray7[29] = (byte) 191;
    numArray7[30] = (byte) 168;
    numArray7[36] = (byte) 140;
    numArray7[3] = (byte) 80 /*0x50*/;
    numArray7[33] = (byte) 112 /*0x70*/;
    numArray7[34] = (byte) 61;
    numArray7[4] = (byte) 203;
    numArray7[17] = (byte) 201;
    numArray7[6] = (byte) 199;
    numArray7[48 /*0x30*/] = (byte) 150;
    numArray7[31 /*0x1F*/] = (byte) 125;
    numArray7[52] = (byte) 23;
    numArray7[20] = (byte) 245;
    numArray7[21] = (byte) 231;
    numArray7[43] = (byte) 202;
    numArray7[40] = (byte) 247;
    numArray7[35] = (byte) 159;
    numArray7[10] = (byte) 74;
    numArray7[18] = (byte) 82;
    numArray7[42] = (byte) 32 /*0x20*/;
    numArray7[49] = (byte) 244;
    numArray7[50] = (byte) 158;
    numArray7[47] = (byte) 61;
    numArray7[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray7[53] = (byte) 79;
    numArray7[7] = (byte) 82;
    byte[] numArray8 = new byte[55];
    numArray8[42] = (byte) 249;
    numArray8[5] = (byte) 149;
    numArray8[34] = (byte) 209;
    numArray8[3] = (byte) 229;
    numArray8[26] = (byte) 30;
    numArray8[52] = (byte) 13;
    numArray8[6] = (byte) 218;
    numArray8[51] = (byte) 71;
    numArray8[25] = (byte) 34;
    numArray8[9] = (byte) 18;
    numArray8[14] = (byte) 14;
    numArray8[44] = (byte) 22;
    numArray8[12] = (byte) 118;
    numArray8[50] = (byte) 144 /*0x90*/;
    numArray8[2] = (byte) 183;
    numArray8[15] = (byte) 109;
    numArray8[16 /*0x10*/] = (byte) 228;
    numArray8[27] = (byte) 38;
    numArray8[18] = (byte) 243;
    numArray8[19] = (byte) 244;
    numArray8[20] = (byte) 177;
    numArray8[17] = (byte) 10;
    numArray8[33] = (byte) 15;
    numArray8[10] = (byte) 143;
    numArray8[49] = (byte) 100;
    numArray8[29] = (byte) 21;
    numArray8[7] = (byte) 241;
    numArray8[23] = (byte) 238;
    numArray8[28] = (byte) 14;
    numArray8[1] = (byte) 201;
    numArray8[30] = (byte) 175;
    numArray8[31 /*0x1F*/] = (byte) 96 /*0x60*/;
    numArray8[24] = (byte) 182;
    numArray8[4] = (byte) 96 /*0x60*/;
    numArray8[11] = (byte) 230;
    numArray8[48 /*0x30*/] = (byte) 46;
    numArray8[35] = (byte) 207;
    numArray8[37] = (byte) 2;
    numArray8[38] = (byte) 26;
    numArray8[54] = (byte) 118;
    numArray8[22] = (byte) 62;
    numArray8[41] = (byte) 95;
    numArray8[8] = (byte) 75;
    numArray8[13] = (byte) 127 /*0x7F*/;
    numArray8[39] = (byte) 106;
    numArray8[45] = (byte) 9;
    numArray8[46] = (byte) 142;
    numArray8[47] = (byte) 5;
    numArray8[21] = (byte) 52;
    numArray8[0] = (byte) 238;
    numArray8[43] = (byte) 93;
    numArray8[36] = (byte) 132;
    numArray8[32 /*0x20*/] = (byte) 109;
    numArray8[53] = (byte) 78;
    numArray8[40] = (byte) 137;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[16 /*0x10*/];
    numArray9[13] = (byte) 249;
    numArray9[9] = (byte) 135;
    numArray9[4] = (byte) 158;
    numArray9[3] = (byte) 6;
    numArray9[12] = (byte) 174;
    numArray9[5] = (byte) 98;
    numArray9[14] = (byte) 35;
    numArray9[1] = (byte) 13;
    numArray9[8] = (byte) 162;
    numArray9[6] = (byte) 254;
    numArray9[0] = (byte) 211;
    numArray9[11] = (byte) 252;
    numArray9[10] = (byte) 132;
    numArray9[2] = (byte) 179;
    numArray9[7] = (byte) 46;
    numArray9[15] = (byte) 100;
    byte[] numArray10 = new byte[16 /*0x10*/];
    numArray10[13] = (byte) 190;
    numArray10[7] = (byte) 230;
    numArray10[2] = (byte) 177;
    numArray10[6] = (byte) 80 /*0x50*/;
    numArray10[4] = (byte) 10;
    numArray10[8] = (byte) 186;
    numArray10[3] = (byte) 52;
    numArray10[9] = (byte) 183;
    numArray10[1] = (byte) 16 /*0x10*/;
    numArray10[12] = (byte) 202;
    numArray10[10] = (byte) 56;
    numArray10[0] = (byte) 150;
    numArray10[11] = (byte) 84;
    numArray10[5] = (byte) 30;
    numArray10[14] = (byte) 250;
    numArray10[15] = (byte) 253;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12610()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[62];
      byte[] numArray2 = new byte[55];
      numArray2[31 /*0x1F*/] = (byte) 225;
      numArray2[1] = (byte) 254;
      numArray2[5] = (byte) 190;
      numArray2[3] = (byte) 2;
      numArray2[51] = (byte) 31 /*0x1F*/;
      numArray2[23] = (byte) 152;
      numArray2[53] = (byte) 111;
      numArray2[18] = (byte) 39;
      numArray2[8] = (byte) 181;
      numArray2[13] = (byte) 146;
      numArray2[10] = (byte) 174;
      numArray2[19] = (byte) 222;
      numArray2[12] = (byte) 130;
      numArray2[7] = (byte) 180;
      numArray2[14] = (byte) 74;
      numArray2[15] = (byte) 152;
      numArray2[16 /*0x10*/] = (byte) 187;
      numArray2[30] = (byte) 67;
      numArray2[20] = (byte) 188;
      numArray2[17] = (byte) 241;
      numArray2[24] = (byte) 115;
      numArray2[42] = (byte) 30;
      numArray2[22] = (byte) 115;
      numArray2[37] = (byte) 58;
      numArray2[44] = (byte) 171;
      numArray2[25] = (byte) 69;
      numArray2[26] = (byte) 246;
      numArray2[27] = (byte) 57;
      numArray2[28] = (byte) 64 /*0x40*/;
      numArray2[29] = (byte) 121;
      numArray2[21] = (byte) 180;
      numArray2[54] = (byte) 254;
      numArray2[32 /*0x20*/] = (byte) 196;
      numArray2[50] = (byte) 119;
      numArray2[52] = (byte) 119;
      numArray2[34] = (byte) 225;
      numArray2[36] = (byte) 191;
      numArray2[41] = (byte) 60;
      numArray2[38] = (byte) 174;
      numArray2[39] = (byte) 126;
      numArray2[40] = (byte) 22;
      numArray2[9] = (byte) 170;
      numArray2[46] = (byte) 193;
      numArray2[43] = (byte) 69;
      numArray2[2] = (byte) 85;
      numArray2[45] = (byte) 204;
      numArray2[47] = (byte) 108;
      numArray2[4] = (byte) 131;
      numArray2[48 /*0x30*/] = (byte) 19;
      numArray2[49] = (byte) 16 /*0x10*/;
      numArray2[11] = (byte) 144 /*0x90*/;
      numArray2[35] = (byte) 179;
      numArray2[0] = (byte) 16 /*0x10*/;
      numArray2[33] = (byte) 73;
      numArray2[6] = (byte) 110;
      byte[] numArray3 = new byte[55]
      {
        (byte) 217,
        (byte) 101,
        (byte) 57,
        (byte) 130,
        (byte) 148,
        (byte) 1,
        (byte) 12,
        (byte) 76,
        (byte) 10,
        (byte) 142,
        (byte) 221,
        (byte) 210,
        (byte) 47,
        (byte) 57,
        (byte) 52,
        (byte) 16 /*0x10*/,
        (byte) 179,
        (byte) 75,
        (byte) 221,
        (byte) 172,
        (byte) 42,
        (byte) 245,
        (byte) 219,
        (byte) 158,
        (byte) 185,
        (byte) 14,
        (byte) 62,
        (byte) 153,
        (byte) 56,
        (byte) 168,
        (byte) 215,
        (byte) 197,
        (byte) 56,
        (byte) 21,
        (byte) 193,
        (byte) 125,
        (byte) 234,
        (byte) 86,
        (byte) 104,
        (byte) 39,
        (byte) 247,
        (byte) 239,
        (byte) 100,
        (byte) 48 /*0x30*/,
        (byte) 115,
        (byte) 9,
        (byte) 80 /*0x50*/,
        (byte) 196,
        (byte) 194,
        (byte) 135,
        (byte) 65,
        (byte) 96 /*0x60*/,
        (byte) 104,
        (byte) 192 /*0xC0*/,
        (byte) 202
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[7];
      numArray4[1] = (byte) 232;
      numArray4[2] = (byte) 186;
      numArray4[0] = (byte) 208 /*0xD0*/;
      numArray4[4] = (byte) 15;
      numArray4[5] = (byte) 67;
      numArray4[3] = (byte) 62;
      numArray4[6] = (byte) 57;
      byte[] numArray5 = new byte[7];
      numArray5[4] = (byte) 42;
      numArray5[0] = (byte) 181;
      numArray5[2] = (byte) 131;
      numArray5[3] = (byte) 32 /*0x20*/;
      numArray5[1] = (byte) 56;
      numArray5[5] = (byte) 185;
      numArray5[6] = (byte) 5;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[62];
    byte[] numArray7 = new byte[55]
    {
      (byte) 205,
      (byte) 168,
      (byte) 248,
      (byte) 10,
      (byte) 71,
      (byte) 155,
      (byte) 172,
      (byte) 204,
      (byte) 188,
      (byte) 203,
      (byte) 50,
      (byte) 194,
      (byte) 181,
      (byte) 120,
      (byte) 210,
      (byte) 133,
      (byte) 169,
      (byte) 154,
      (byte) 122,
      (byte) 139,
      (byte) 155,
      (byte) 143,
      (byte) 14,
      (byte) 97,
      (byte) 34,
      (byte) 8,
      (byte) 220,
      (byte) 194,
      (byte) 199,
      (byte) 21,
      (byte) 13,
      (byte) 192 /*0xC0*/,
      (byte) 67,
      (byte) 191,
      (byte) 113,
      (byte) 102,
      (byte) 74,
      (byte) 46,
      (byte) 252,
      (byte) 200,
      (byte) 201,
      (byte) 32 /*0x20*/,
      (byte) 112 /*0x70*/,
      (byte) 163,
      (byte) 164,
      (byte) 18,
      (byte) 30,
      (byte) 122,
      (byte) 19,
      (byte) 49,
      (byte) 8,
      (byte) 181,
      (byte) 149,
      (byte) 26,
      (byte) 94
    };
    byte[] numArray8 = new byte[55];
    numArray8[12] = (byte) 27;
    numArray8[6] = (byte) 195;
    numArray8[1] = (byte) 51;
    numArray8[3] = (byte) 219;
    numArray8[9] = (byte) 171;
    numArray8[53] = (byte) 234;
    numArray8[36] = (byte) 159;
    numArray8[47] = (byte) 95;
    numArray8[11] = (byte) 23;
    numArray8[38] = (byte) 233;
    numArray8[10] = (byte) 244;
    numArray8[32 /*0x20*/] = (byte) 157;
    numArray8[5] = (byte) 110;
    numArray8[13] = (byte) 145;
    numArray8[37] = (byte) 28;
    numArray8[41] = (byte) 31 /*0x1F*/;
    numArray8[16 /*0x10*/] = (byte) 215;
    numArray8[17] = (byte) 225;
    numArray8[18] = (byte) 200;
    numArray8[51] = (byte) 60;
    numArray8[20] = (byte) 193;
    numArray8[21] = (byte) 13;
    numArray8[49] = (byte) 220;
    numArray8[34] = (byte) 130;
    numArray8[44] = (byte) 42;
    numArray8[14] = (byte) 246;
    numArray8[26] = (byte) 148;
    numArray8[27] = (byte) 5;
    numArray8[28] = (byte) 149;
    numArray8[22] = (byte) 81;
    numArray8[2] = (byte) 126;
    numArray8[31 /*0x1F*/] = (byte) 164;
    numArray8[7] = (byte) 17;
    numArray8[45] = (byte) 156;
    numArray8[30] = (byte) 194;
    numArray8[29] = (byte) 92;
    numArray8[46] = (byte) 91;
    numArray8[25] = (byte) 188;
    numArray8[0] = (byte) 142;
    numArray8[24] = (byte) 115;
    numArray8[40] = (byte) 101;
    numArray8[19] = (byte) 147;
    numArray8[42] = (byte) 246;
    numArray8[15] = (byte) 149;
    numArray8[50] = (byte) 222;
    numArray8[8] = (byte) 149;
    numArray8[35] = (byte) 65;
    numArray8[23] = (byte) 56;
    numArray8[48 /*0x30*/] = (byte) 81;
    numArray8[43] = (byte) 105;
    numArray8[33] = (byte) 89;
    numArray8[39] = (byte) 113;
    numArray8[52] = (byte) 201;
    numArray8[4] = (byte) 36;
    numArray8[54] = (byte) 184;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[7];
    numArray9[3] = (byte) 93;
    numArray9[1] = (byte) 216;
    numArray9[5] = (byte) 112 /*0x70*/;
    numArray9[0] = (byte) 218;
    numArray9[4] = (byte) 57;
    numArray9[2] = (byte) 84;
    numArray9[6] = (byte) 154;
    byte[] numArray10 = new byte[7]
    {
      (byte) 142,
      (byte) 181,
      (byte) 141,
      (byte) 197,
      (byte) 36,
      (byte) 222,
      (byte) 228
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 7);
    for (int index = 0; index < 7; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12611()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 96 /*0x60*/,
        (byte) 86,
        (byte) 133,
        (byte) 138,
        (byte) 124,
        (byte) 193,
        (byte) 238,
        (byte) 8,
        (byte) 158
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 197,
        (byte) 236,
        (byte) 129,
        (byte) 173,
        (byte) 130,
        (byte) 94,
        (byte) 173,
        (byte) 112 /*0x70*/,
        (byte) 96 /*0x60*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 208 /*0xD0*/,
      (byte) 124,
      (byte) 20,
      (byte) 156,
      (byte) 216,
      (byte) 109,
      (byte) 104,
      (byte) 170,
      (byte) 165
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 107,
      (byte) 6,
      (byte) 101,
      (byte) 226,
      (byte) 154,
      (byte) 22,
      (byte) 143,
      (byte) 55,
      (byte) 37
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[50];
    byte[] response = new byte[50];
    Array.Copy((Array) sc_12586.sspq, 221, (Array) numArray7, 0, 50);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12586.sspr, 221, (Array) numArray7, 0, 50);
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

  internal static int ssp_appserver_12612(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[20] = (byte) 163;
    sourceArray1[1] = (byte) 91;
    sourceArray1[18] = (byte) 33;
    sourceArray1[3] = (byte) 183;
    sourceArray1[4] = (byte) 124;
    sourceArray1[22] = (byte) 25;
    sourceArray1[7] = (byte) 192 /*0xC0*/;
    sourceArray1[47] = (byte) 171;
    sourceArray1[8] = (byte) 147;
    sourceArray1[2] = (byte) 239;
    sourceArray1[10] = (byte) 58;
    sourceArray1[36] = (byte) 173;
    sourceArray1[12] = (byte) 205;
    sourceArray1[35] = (byte) 170;
    sourceArray1[17] = (byte) 101;
    sourceArray1[9] = (byte) 191;
    sourceArray1[6] = (byte) 4;
    sourceArray1[44] = (byte) 212;
    sourceArray1[41] = (byte) 110;
    sourceArray1[19] = (byte) 48 /*0x30*/;
    sourceArray1[39] = (byte) 110;
    sourceArray1[38] = (byte) 177;
    sourceArray1[43] = (byte) 75;
    sourceArray1[40] = (byte) 252;
    sourceArray1[24] = (byte) 41;
    sourceArray1[25] = (byte) 30;
    sourceArray1[26] = (byte) 115;
    sourceArray1[27] = (byte) 17;
    sourceArray1[28] = (byte) 39;
    sourceArray1[29] = (byte) 179;
    sourceArray1[16 /*0x10*/] = (byte) 27;
    sourceArray1[30] = (byte) 161;
    sourceArray1[32 /*0x20*/] = (byte) 201;
    sourceArray1[33] = (byte) 221;
    sourceArray1[15] = (byte) 179;
    sourceArray1[21] = (byte) 247;
    sourceArray1[0] = (byte) 18;
    sourceArray1[37] = (byte) 137;
    sourceArray1[13] = (byte) 160 /*0xA0*/;
    sourceArray1[5] = (byte) 224 /*0xE0*/;
    sourceArray1[42] = (byte) 144 /*0x90*/;
    sourceArray1[46] = (byte) 43;
    sourceArray1[34] = (byte) 22;
    sourceArray1[14] = (byte) 164;
    sourceArray1[23] = (byte) 238;
    sourceArray1[45] = (byte) 35;
    sourceArray1[31 /*0x1F*/] = (byte) 196;
    sourceArray1[11] = (byte) 56;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[10] = (byte) 193;
    sourceArray2[1] = (byte) 92;
    sourceArray2[15] = (byte) 212;
    sourceArray2[36] = (byte) 172;
    sourceArray2[4] = (byte) 233;
    sourceArray2[27] = (byte) 192 /*0xC0*/;
    sourceArray2[45] = (byte) 148;
    sourceArray2[39] = (byte) 248;
    sourceArray2[2] = (byte) 20;
    sourceArray2[9] = (byte) 139;
    sourceArray2[0] = (byte) 133;
    sourceArray2[11] = (byte) 16 /*0x10*/;
    sourceArray2[25] = (byte) 39;
    sourceArray2[24] = (byte) 141;
    sourceArray2[31 /*0x1F*/] = (byte) 224 /*0xE0*/;
    sourceArray2[26] = (byte) 212;
    sourceArray2[16 /*0x10*/] = (byte) 199;
    sourceArray2[3] = (byte) 215;
    sourceArray2[42] = (byte) 148;
    sourceArray2[19] = (byte) 167;
    sourceArray2[20] = (byte) 108;
    sourceArray2[21] = (byte) 78;
    sourceArray2[22] = (byte) 70;
    sourceArray2[23] = (byte) 64 /*0x40*/;
    sourceArray2[17] = (byte) 15;
    sourceArray2[8] = (byte) 135;
    sourceArray2[18] = (byte) 107;
    sourceArray2[40] = (byte) 150;
    sourceArray2[34] = (byte) 40;
    sourceArray2[29] = (byte) 40;
    sourceArray2[14] = (byte) 238;
    sourceArray2[13] = (byte) 238;
    sourceArray2[32 /*0x20*/] = (byte) 180;
    sourceArray2[33] = (byte) 117;
    sourceArray2[30] = (byte) 158;
    sourceArray2[35] = (byte) 253;
    sourceArray2[28] = (byte) 12;
    sourceArray2[37] = (byte) 62;
    sourceArray2[6] = (byte) 91;
    sourceArray2[47] = (byte) 26;
    sourceArray2[38] = (byte) 249;
    sourceArray2[41] = (byte) 73;
    sourceArray2[12] = (byte) 149;
    sourceArray2[43] = (byte) 7;
    sourceArray2[44] = (byte) 225;
    sourceArray2[7] = (byte) 45;
    sourceArray2[46] = (byte) 192 /*0xC0*/;
    sourceArray2[5] = (byte) 29;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12613()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 232,
        (byte) 92,
        (byte) 221
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 208 /*0xD0*/,
        (byte) 171,
        (byte) 81
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_12586.sspq, 271, (Array) numArray4, 0, 43);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12586.sspr, 271, (Array) numArray4, 0, 43);
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
    byte[] numArray5 = new byte[3];
    byte[] numArray6 = new byte[3]
    {
      (byte) 156,
      (byte) 66,
      (byte) 52
    };
    byte[] numArray7 = new byte[3]
    {
      (byte) 211,
      (byte) 92,
      (byte) 51
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12614()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[240 /*0xF0*/];
      byte[] numArray2 = new byte[55];
      numArray2[30] = (byte) 248;
      numArray2[8] = (byte) 230;
      numArray2[2] = (byte) 88;
      numArray2[24] = (byte) 152;
      numArray2[4] = (byte) 75;
      numArray2[41] = (byte) 217;
      numArray2[45] = (byte) 53;
      numArray2[19] = (byte) 178;
      numArray2[28] = (byte) 221;
      numArray2[9] = (byte) 95;
      numArray2[35] = (byte) 32 /*0x20*/;
      numArray2[42] = (byte) 49;
      numArray2[50] = (byte) 66;
      numArray2[13] = (byte) 118;
      numArray2[20] = (byte) 66;
      numArray2[15] = (byte) 172;
      numArray2[6] = (byte) 228;
      numArray2[1] = (byte) 9;
      numArray2[11] = (byte) 210;
      numArray2[18] = (byte) 5;
      numArray2[10] = (byte) 153;
      numArray2[21] = (byte) 96 /*0x60*/;
      numArray2[49] = (byte) 150;
      numArray2[23] = (byte) 226;
      numArray2[7] = (byte) 175;
      numArray2[25] = (byte) 254;
      numArray2[27] = (byte) 245;
      numArray2[12] = (byte) 213;
      numArray2[48 /*0x30*/] = (byte) 155;
      numArray2[29] = (byte) 66;
      numArray2[34] = (byte) 147;
      numArray2[31 /*0x1F*/] = (byte) 40;
      numArray2[32 /*0x20*/] = byte.MaxValue;
      numArray2[33] = (byte) 101;
      numArray2[14] = (byte) 0;
      numArray2[44] = (byte) 246;
      numArray2[22] = (byte) 250;
      numArray2[37] = (byte) 110;
      numArray2[52] = (byte) 29;
      numArray2[39] = (byte) 146;
      numArray2[40] = (byte) 41;
      numArray2[17] = (byte) 235;
      numArray2[38] = (byte) 200;
      numArray2[16 /*0x10*/] = (byte) 241;
      numArray2[0] = (byte) 202;
      numArray2[26] = (byte) 125;
      numArray2[46] = (byte) 115;
      numArray2[47] = (byte) 169;
      numArray2[51] = (byte) 113;
      numArray2[43] = (byte) 46;
      numArray2[5] = (byte) 33;
      numArray2[36] = (byte) 47;
      numArray2[3] = (byte) 84;
      numArray2[53] = (byte) 29;
      numArray2[54] = (byte) 128 /*0x80*/;
      byte[] numArray3 = new byte[55]
      {
        (byte) 190,
        (byte) 106,
        (byte) 7,
        (byte) 176 /*0xB0*/,
        (byte) 217,
        (byte) 254,
        (byte) 50,
        (byte) 187,
        (byte) 212,
        (byte) 71,
        (byte) 244,
        (byte) 12,
        (byte) 179,
        (byte) 102,
        (byte) 243,
        (byte) 217,
        (byte) 41,
        (byte) 68,
        (byte) 39,
        (byte) 250,
        (byte) 194,
        (byte) 141,
        (byte) 18,
        (byte) 151,
        (byte) 10,
        (byte) 132,
        (byte) 245,
        (byte) 213,
        (byte) 209,
        (byte) 209,
        (byte) 198,
        (byte) 170,
        (byte) 22,
        (byte) 113,
        (byte) 253,
        (byte) 144 /*0x90*/,
        (byte) 252,
        (byte) 48 /*0x30*/,
        (byte) 118,
        (byte) 22,
        (byte) 60,
        (byte) 74,
        (byte) 170,
        (byte) 213,
        (byte) 97,
        (byte) 20,
        (byte) 4,
        (byte) 107,
        (byte) 160 /*0xA0*/,
        (byte) 97,
        (byte) 206,
        (byte) 36,
        (byte) 211,
        (byte) 225,
        (byte) 215
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 137,
        (byte) 89,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 148,
        (byte) 138,
        (byte) 163,
        (byte) 223,
        (byte) 101,
        (byte) 241,
        (byte) 140,
        (byte) 186,
        (byte) 207,
        (byte) 194,
        (byte) 208 /*0xD0*/,
        (byte) 168,
        (byte) 133,
        (byte) 77,
        (byte) 97,
        (byte) 161,
        (byte) 3,
        (byte) 102,
        (byte) 0,
        (byte) 136,
        (byte) 12,
        (byte) 157,
        (byte) 45,
        (byte) 65,
        (byte) 56,
        (byte) 96 /*0x60*/,
        (byte) 77,
        (byte) 203,
        (byte) 239,
        (byte) 204,
        (byte) 48 /*0x30*/,
        (byte) 89,
        (byte) 185,
        (byte) 29,
        (byte) 166,
        (byte) 124,
        (byte) 234,
        (byte) 232,
        (byte) 32 /*0x20*/,
        (byte) 113,
        (byte) 118,
        (byte) 12,
        (byte) 237,
        (byte) 86,
        (byte) 238,
        (byte) 166,
        (byte) 37,
        (byte) 71,
        (byte) 182,
        (byte) 152,
        (byte) 65
      };
      byte[] numArray5 = new byte[55];
      numArray5[48 /*0x30*/] = (byte) 92;
      numArray5[50] = (byte) 186;
      numArray5[24] = (byte) 209;
      numArray5[12] = (byte) 175;
      numArray5[4] = (byte) 35;
      numArray5[45] = (byte) 189;
      numArray5[6] = (byte) 92;
      numArray5[33] = (byte) 87;
      numArray5[25] = (byte) 217;
      numArray5[39] = (byte) 186;
      numArray5[11] = (byte) 75;
      numArray5[16 /*0x10*/] = (byte) 233;
      numArray5[18] = (byte) 64 /*0x40*/;
      numArray5[13] = (byte) 215;
      numArray5[3] = (byte) 174;
      numArray5[15] = (byte) 38;
      numArray5[26] = (byte) 45;
      numArray5[17] = (byte) 200;
      numArray5[30] = (byte) 93;
      numArray5[19] = (byte) 192 /*0xC0*/;
      numArray5[44] = (byte) 224 /*0xE0*/;
      numArray5[40] = (byte) 188;
      numArray5[22] = (byte) 185;
      numArray5[23] = (byte) 235;
      numArray5[5] = (byte) 31 /*0x1F*/;
      numArray5[14] = (byte) 141;
      numArray5[36] = (byte) 190;
      numArray5[27] = (byte) 9;
      numArray5[28] = (byte) 200;
      numArray5[29] = (byte) 8;
      numArray5[54] = (byte) 58;
      numArray5[34] = (byte) 217;
      numArray5[21] = (byte) 161;
      numArray5[35] = (byte) 171;
      numArray5[9] = (byte) 8;
      numArray5[51] = (byte) 99;
      numArray5[8] = (byte) 16 /*0x10*/;
      numArray5[7] = (byte) 29;
      numArray5[38] = (byte) 204;
      numArray5[37] = (byte) 172;
      numArray5[20] = (byte) 135;
      numArray5[31 /*0x1F*/] = (byte) 39;
      numArray5[42] = (byte) 18;
      numArray5[32 /*0x20*/] = (byte) 63 /*0x3F*/;
      numArray5[0] = (byte) 13;
      numArray5[41] = (byte) 124;
      numArray5[46] = (byte) 22;
      numArray5[47] = (byte) 76;
      numArray5[10] = (byte) 63 /*0x3F*/;
      numArray5[49] = (byte) 121;
      numArray5[43] = (byte) 41;
      numArray5[52] = (byte) 109;
      numArray5[1] = (byte) 116;
      numArray5[53] = (byte) 177;
      numArray5[2] = (byte) 145;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 39,
        (byte) 195,
        (byte) 69,
        (byte) 133,
        (byte) 15,
        (byte) 20,
        (byte) 200,
        (byte) 158,
        (byte) 171,
        (byte) 116,
        (byte) 119,
        (byte) 133,
        (byte) 119,
        (byte) 111,
        (byte) 14,
        (byte) 246,
        (byte) 197,
        (byte) 46,
        (byte) 218,
        (byte) 41,
        (byte) 194,
        (byte) 92,
        (byte) 191,
        (byte) 198,
        (byte) 18,
        (byte) 219,
        (byte) 219,
        (byte) 125,
        (byte) 100,
        (byte) 68,
        (byte) 117,
        (byte) 100,
        (byte) 47,
        (byte) 55,
        (byte) 87,
        (byte) 150,
        (byte) 106,
        (byte) 154,
        (byte) 121,
        (byte) 44,
        (byte) 83,
        (byte) 46,
        (byte) 10,
        (byte) 204,
        (byte) 114,
        (byte) 62,
        byte.MaxValue,
        (byte) 71,
        (byte) 175,
        (byte) 170,
        (byte) 11,
        (byte) 178,
        (byte) 75,
        (byte) 180,
        (byte) 6
      };
      byte[] numArray7 = new byte[55];
      numArray7[28] = (byte) 66;
      numArray7[7] = (byte) 93;
      numArray7[0] = (byte) 132;
      numArray7[18] = (byte) 28;
      numArray7[35] = (byte) 145;
      numArray7[38] = (byte) 71;
      numArray7[13] = (byte) 20;
      numArray7[43] = (byte) 235;
      numArray7[46] = (byte) 14;
      numArray7[8] = (byte) 26;
      numArray7[10] = (byte) 233;
      numArray7[45] = (byte) 46;
      numArray7[12] = (byte) 35;
      numArray7[11] = (byte) 253;
      numArray7[27] = (byte) 127 /*0x7F*/;
      numArray7[15] = (byte) 61;
      numArray7[16 /*0x10*/] = (byte) 127 /*0x7F*/;
      numArray7[17] = (byte) 89;
      numArray7[33] = (byte) 121;
      numArray7[19] = (byte) 138;
      numArray7[20] = (byte) 238;
      numArray7[21] = (byte) 145;
      numArray7[22] = (byte) 67;
      numArray7[41] = (byte) 103;
      numArray7[24] = (byte) 132;
      numArray7[44] = (byte) 77;
      numArray7[37] = (byte) 197;
      numArray7[26] = (byte) 104;
      numArray7[6] = (byte) 174;
      numArray7[29] = (byte) 196;
      numArray7[2] = (byte) 155;
      numArray7[5] = (byte) 162;
      numArray7[53] = (byte) 82;
      numArray7[52] = (byte) 84;
      numArray7[32 /*0x20*/] = (byte) 196;
      numArray7[42] = (byte) 147;
      numArray7[36] = (byte) 46;
      numArray7[23] = (byte) 20;
      numArray7[14] = (byte) 200;
      numArray7[39] = (byte) 224 /*0xE0*/;
      numArray7[40] = (byte) 185;
      numArray7[4] = (byte) 16 /*0x10*/;
      numArray7[48 /*0x30*/] = (byte) 97;
      numArray7[3] = (byte) 196;
      numArray7[34] = (byte) 43;
      numArray7[31 /*0x1F*/] = (byte) 241;
      numArray7[1] = (byte) 114;
      numArray7[47] = (byte) 31 /*0x1F*/;
      numArray7[9] = (byte) 9;
      numArray7[49] = (byte) 33;
      numArray7[50] = (byte) 243;
      numArray7[51] = (byte) 35;
      numArray7[25] = (byte) 247;
      numArray7[30] = (byte) 0;
      numArray7[54] = (byte) 233;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[25] = (byte) 150;
      numArray8[46] = (byte) 82;
      numArray8[2] = (byte) 86;
      numArray8[53] = (byte) 201;
      numArray8[29] = (byte) 123;
      numArray8[5] = (byte) 127 /*0x7F*/;
      numArray8[17] = (byte) 206;
      numArray8[0] = (byte) 43;
      numArray8[48 /*0x30*/] = (byte) 219;
      numArray8[9] = (byte) 218;
      numArray8[15] = (byte) 110;
      numArray8[11] = (byte) 139;
      numArray8[12] = (byte) 229;
      numArray8[20] = (byte) 37;
      numArray8[14] = (byte) 4;
      numArray8[1] = (byte) 158;
      numArray8[13] = (byte) 160 /*0xA0*/;
      numArray8[39] = (byte) 117;
      numArray8[26] = (byte) 155;
      numArray8[38] = (byte) 139;
      numArray8[28] = (byte) 196;
      numArray8[36] = (byte) 239;
      numArray8[22] = (byte) 235;
      numArray8[4] = (byte) 124;
      numArray8[42] = (byte) 102;
      numArray8[7] = (byte) 60;
      numArray8[44] = (byte) 177;
      numArray8[27] = (byte) 89;
      numArray8[40] = (byte) 118;
      numArray8[3] = (byte) 165;
      numArray8[19] = (byte) 174;
      numArray8[31 /*0x1F*/] = (byte) 190;
      numArray8[32 /*0x20*/] = (byte) 19;
      numArray8[33] = (byte) 130;
      numArray8[34] = (byte) 192 /*0xC0*/;
      numArray8[8] = (byte) 249;
      numArray8[52] = (byte) 211;
      numArray8[37] = (byte) 77;
      numArray8[45] = (byte) 164;
      numArray8[6] = (byte) 45;
      numArray8[21] = (byte) 244;
      numArray8[30] = (byte) 157;
      numArray8[24] = (byte) 99;
      numArray8[43] = (byte) 13;
      numArray8[41] = (byte) 144 /*0x90*/;
      numArray8[16 /*0x10*/] = (byte) 186;
      numArray8[10] = (byte) 14;
      numArray8[47] = (byte) 125;
      numArray8[23] = (byte) 28;
      numArray8[49] = (byte) 144 /*0x90*/;
      numArray8[50] = (byte) 227;
      numArray8[51] = (byte) 15;
      numArray8[35] = (byte) 136;
      numArray8[18] = (byte) 158;
      numArray8[54] = (byte) 234;
      byte[] numArray9 = new byte[55];
      numArray9[25] = (byte) 82;
      numArray9[1] = (byte) 201;
      numArray9[39] = (byte) 233;
      numArray9[51] = (byte) 141;
      numArray9[46] = (byte) 50;
      numArray9[15] = (byte) 147;
      numArray9[6] = (byte) 175;
      numArray9[7] = (byte) 66;
      numArray9[8] = (byte) 1;
      numArray9[9] = (byte) 194;
      numArray9[10] = (byte) 154;
      numArray9[11] = (byte) 92;
      numArray9[12] = (byte) 120;
      numArray9[27] = (byte) 126;
      numArray9[24] = (byte) 64 /*0x40*/;
      numArray9[23] = (byte) 175;
      numArray9[33] = (byte) 145;
      numArray9[14] = (byte) 230;
      numArray9[20] = (byte) 221;
      numArray9[3] = (byte) 68;
      numArray9[2] = (byte) 76;
      numArray9[21] = (byte) 252;
      numArray9[22] = (byte) 178;
      numArray9[36] = (byte) 221;
      numArray9[45] = (byte) 131;
      numArray9[0] = (byte) 71;
      numArray9[26] = (byte) 27;
      numArray9[4] = (byte) 216;
      numArray9[49] = (byte) 178;
      numArray9[54] = (byte) 49;
      numArray9[17] = (byte) 143;
      numArray9[31 /*0x1F*/] = (byte) 212;
      numArray9[41] = (byte) 51;
      numArray9[44] = (byte) 107;
      numArray9[35] = (byte) 113;
      numArray9[19] = (byte) 67;
      numArray9[34] = (byte) 10;
      numArray9[37] = (byte) 64 /*0x40*/;
      numArray9[38] = (byte) 67;
      numArray9[30] = (byte) 0;
      numArray9[40] = (byte) 168;
      numArray9[5] = (byte) 8;
      numArray9[42] = (byte) 236;
      numArray9[43] = (byte) 243;
      numArray9[13] = (byte) 25;
      numArray9[29] = (byte) 195;
      numArray9[53] = (byte) 5;
      numArray9[47] = (byte) 160 /*0xA0*/;
      numArray9[48 /*0x30*/] = (byte) 110;
      numArray9[32 /*0x20*/] = (byte) 186;
      numArray9[50] = (byte) 141;
      numArray9[16 /*0x10*/] = (byte) 66;
      numArray9[52] = (byte) 170;
      numArray9[18] = (byte) 123;
      numArray9[28] = (byte) 224 /*0xE0*/;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[20];
      numArray10[7] = (byte) 236;
      numArray10[1] = (byte) 239;
      numArray10[5] = (byte) 34;
      numArray10[12] = (byte) 169;
      numArray10[3] = (byte) 68;
      numArray10[8] = (byte) 39;
      numArray10[14] = (byte) 216;
      numArray10[0] = (byte) 197;
      numArray10[19] = (byte) 163;
      numArray10[6] = (byte) 197;
      numArray10[10] = (byte) 137;
      numArray10[9] = (byte) 116;
      numArray10[2] = (byte) 76;
      numArray10[4] = (byte) 55;
      numArray10[13] = (byte) 229;
      numArray10[15] = (byte) 154;
      numArray10[16 /*0x10*/] = (byte) 201;
      numArray10[17] = (byte) 73;
      numArray10[18] = (byte) 169;
      numArray10[11] = (byte) 119;
      byte[] numArray11 = new byte[20];
      numArray11[9] = (byte) 67;
      numArray11[15] = (byte) 2;
      numArray11[2] = (byte) 46;
      numArray11[17] = (byte) 164;
      numArray11[4] = (byte) 8;
      numArray11[13] = (byte) 149;
      numArray11[6] = (byte) 206;
      numArray11[18] = (byte) 161;
      numArray11[11] = (byte) 25;
      numArray11[7] = (byte) 13;
      numArray11[10] = (byte) 244;
      numArray11[16 /*0x10*/] = (byte) 234;
      numArray11[12] = (byte) 219;
      numArray11[8] = (byte) 170;
      numArray11[1] = (byte) 147;
      numArray11[14] = (byte) 210;
      numArray11[0] = (byte) 180;
      numArray11[3] = (byte) 87;
      numArray11[5] = (byte) 102;
      numArray11[19] = (byte) 56;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[240 /*0xF0*/];
    byte[] numArray13 = new byte[55]
    {
      (byte) 251,
      (byte) 183,
      (byte) 29,
      (byte) 155,
      (byte) 118,
      (byte) 198,
      (byte) 62,
      (byte) 227,
      (byte) 65,
      (byte) 53,
      (byte) 248,
      (byte) 103,
      (byte) 182,
      (byte) 61,
      (byte) 9,
      (byte) 152,
      (byte) 109,
      (byte) 88,
      (byte) 119,
      (byte) 206,
      (byte) 0,
      (byte) 224 /*0xE0*/,
      (byte) 11,
      (byte) 61,
      (byte) 124,
      (byte) 83,
      (byte) 104,
      (byte) 62,
      (byte) 157,
      (byte) 127 /*0x7F*/,
      (byte) 66,
      (byte) 105,
      (byte) 130,
      (byte) 219,
      (byte) 215,
      (byte) 210,
      (byte) 123,
      (byte) 50,
      (byte) 151,
      (byte) 155,
      (byte) 254,
      (byte) 176 /*0xB0*/,
      (byte) 9,
      (byte) 123,
      (byte) 53,
      (byte) 247,
      (byte) 41,
      (byte) 120,
      (byte) 81,
      (byte) 129,
      (byte) 155,
      (byte) 121,
      (byte) 208 /*0xD0*/,
      (byte) 235,
      (byte) 89
    };
    byte[] numArray14 = new byte[55];
    numArray14[0] = (byte) 187;
    numArray14[31 /*0x1F*/] = (byte) 32 /*0x20*/;
    numArray14[2] = (byte) 148;
    numArray14[35] = (byte) 132;
    numArray14[44] = (byte) 75;
    numArray14[1] = (byte) 110;
    numArray14[6] = (byte) 220;
    numArray14[38] = (byte) 160 /*0xA0*/;
    numArray14[26] = (byte) 247;
    numArray14[9] = (byte) 135;
    numArray14[10] = (byte) 79;
    numArray14[29] = (byte) 212;
    numArray14[12] = (byte) 2;
    numArray14[22] = (byte) 219;
    numArray14[43] = (byte) 105;
    numArray14[15] = (byte) 198;
    numArray14[39] = (byte) 230;
    numArray14[17] = (byte) 48 /*0x30*/;
    numArray14[18] = (byte) 185;
    numArray14[19] = (byte) 49;
    numArray14[20] = (byte) 121;
    numArray14[21] = (byte) 84;
    numArray14[25] = (byte) 235;
    numArray14[49] = (byte) 167;
    numArray14[34] = (byte) 192 /*0xC0*/;
    numArray14[5] = (byte) 130;
    numArray14[8] = (byte) 192 /*0xC0*/;
    numArray14[27] = (byte) 196;
    numArray14[14] = (byte) 236;
    numArray14[23] = (byte) 132;
    numArray14[30] = (byte) 247;
    numArray14[16 /*0x10*/] = (byte) 85;
    numArray14[32 /*0x20*/] = (byte) 207;
    numArray14[33] = (byte) 150;
    numArray14[48 /*0x30*/] = (byte) 63 /*0x3F*/;
    numArray14[4] = (byte) 157;
    numArray14[36] = (byte) 6;
    numArray14[28] = (byte) 196;
    numArray14[53] = (byte) 78;
    numArray14[54] = (byte) 109;
    numArray14[40] = (byte) 20;
    numArray14[41] = (byte) 182;
    numArray14[47] = (byte) 187;
    numArray14[24] = (byte) 113;
    numArray14[37] = (byte) 105;
    numArray14[45] = (byte) 11;
    numArray14[13] = (byte) 183;
    numArray14[52] = (byte) 163;
    numArray14[42] = (byte) 252;
    numArray14[11] = (byte) 183;
    numArray14[50] = (byte) 210;
    numArray14[51] = (byte) 55;
    numArray14[3] = (byte) 179;
    numArray14[46] = (byte) 215;
    numArray14[7] = (byte) 59;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 215,
      (byte) 154,
      (byte) 248,
      (byte) 109,
      (byte) 19,
      (byte) 42,
      (byte) 222,
      (byte) 41,
      (byte) 244,
      (byte) 78,
      (byte) 87,
      (byte) 170,
      (byte) 16 /*0x10*/,
      (byte) 128 /*0x80*/,
      (byte) 39,
      (byte) 76,
      (byte) 157,
      (byte) 210,
      (byte) 230,
      (byte) 143,
      (byte) 7,
      (byte) 98,
      (byte) 114,
      (byte) 218,
      (byte) 223,
      (byte) 210,
      (byte) 183,
      (byte) 130,
      (byte) 17,
      (byte) 164,
      (byte) 124,
      (byte) 121,
      (byte) 119,
      (byte) 11,
      (byte) 196,
      (byte) 120,
      (byte) 123,
      (byte) 209,
      (byte) 234,
      (byte) 62,
      (byte) 104,
      (byte) 95,
      (byte) 177,
      (byte) 196,
      (byte) 247,
      (byte) 103,
      (byte) 9,
      (byte) 227,
      (byte) 56,
      (byte) 107,
      (byte) 87,
      (byte) 72,
      (byte) 34,
      (byte) 181,
      (byte) 20
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 90,
      (byte) 60,
      (byte) 47,
      (byte) 176 /*0xB0*/,
      (byte) 198,
      (byte) 100,
      (byte) 151,
      (byte) 120,
      (byte) 102,
      (byte) 4,
      (byte) 43,
      (byte) 142,
      (byte) 211,
      (byte) 33,
      (byte) 156,
      (byte) 51,
      (byte) 202,
      (byte) 25,
      (byte) 59,
      (byte) 70,
      (byte) 60,
      (byte) 53,
      (byte) 90,
      (byte) 69,
      (byte) 73,
      (byte) 167,
      (byte) 244,
      (byte) 8,
      (byte) 207,
      (byte) 209,
      (byte) 63 /*0x3F*/,
      (byte) 120,
      (byte) 170,
      (byte) 8,
      (byte) 87,
      (byte) 147,
      (byte) 126,
      (byte) 68,
      (byte) 45,
      (byte) 186,
      (byte) 18,
      (byte) 127 /*0x7F*/,
      (byte) 230,
      (byte) 207,
      (byte) 71,
      (byte) 72,
      (byte) 35,
      (byte) 133,
      (byte) 204,
      (byte) 121,
      (byte) 14,
      (byte) 19,
      (byte) 41,
      (byte) 21,
      (byte) 239
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[29] = (byte) 86;
    numArray17[44] = (byte) 4;
    numArray17[2] = (byte) 64 /*0x40*/;
    numArray17[47] = (byte) 155;
    numArray17[4] = (byte) 92;
    numArray17[5] = (byte) 24;
    numArray17[6] = (byte) 80 /*0x50*/;
    numArray17[35] = (byte) 67;
    numArray17[13] = (byte) 208 /*0xD0*/;
    numArray17[9] = (byte) 51;
    numArray17[28] = (byte) 51;
    numArray17[38] = (byte) 12;
    numArray17[12] = (byte) 174;
    numArray17[46] = (byte) 138;
    numArray17[3] = (byte) 115;
    numArray17[15] = (byte) 231;
    numArray17[16 /*0x10*/] = (byte) 167;
    numArray17[17] = (byte) 38;
    numArray17[18] = (byte) 167;
    numArray17[23] = (byte) 201;
    numArray17[20] = (byte) 173;
    numArray17[34] = (byte) 94;
    numArray17[53] = (byte) 196;
    numArray17[22] = (byte) 120;
    numArray17[52] = (byte) 108;
    numArray17[25] = (byte) 73;
    numArray17[26] = (byte) 165;
    numArray17[11] = (byte) 188;
    numArray17[31 /*0x1F*/] = (byte) 246;
    numArray17[37] = (byte) 202;
    numArray17[30] = (byte) 193;
    numArray17[24] = (byte) 211;
    numArray17[32 /*0x20*/] = (byte) 78;
    numArray17[33] = (byte) 80 /*0x50*/;
    numArray17[0] = (byte) 29;
    numArray17[42] = (byte) 24;
    numArray17[8] = (byte) 222;
    numArray17[50] = (byte) 123;
    numArray17[19] = (byte) 176 /*0xB0*/;
    numArray17[39] = (byte) 71;
    numArray17[27] = (byte) 77;
    numArray17[14] = (byte) 204;
    numArray17[7] = (byte) 209;
    numArray17[43] = (byte) 51;
    numArray17[48 /*0x30*/] = (byte) 139;
    numArray17[1] = (byte) 141;
    numArray17[10] = (byte) 5;
    numArray17[36] = (byte) 169;
    numArray17[45] = (byte) 38;
    numArray17[49] = (byte) 96 /*0x60*/;
    numArray17[21] = (byte) 227;
    numArray17[51] = (byte) 143;
    numArray17[40] = (byte) 97;
    numArray17[41] = (byte) 242;
    numArray17[54] = (byte) 122;
    byte[] numArray18 = new byte[55]
    {
      (byte) 125,
      (byte) 147,
      (byte) 155,
      (byte) 196,
      (byte) 140,
      (byte) 2,
      (byte) 104,
      (byte) 77,
      (byte) 39,
      (byte) 44,
      (byte) 67,
      (byte) 210,
      (byte) 60,
      (byte) 238,
      (byte) 212,
      (byte) 68,
      (byte) 24,
      (byte) 142,
      (byte) 173,
      (byte) 14,
      (byte) 137,
      (byte) 48 /*0x30*/,
      (byte) 236,
      (byte) 118,
      (byte) 92,
      (byte) 234,
      (byte) 5,
      (byte) 106,
      (byte) 167,
      (byte) 216,
      (byte) 27,
      (byte) 57,
      (byte) 105,
      (byte) 102,
      (byte) 83,
      (byte) 203,
      (byte) 108,
      (byte) 86,
      (byte) 66,
      (byte) 210,
      (byte) 8,
      (byte) 186,
      (byte) 229,
      (byte) 96 /*0x60*/,
      (byte) 206,
      (byte) 179,
      (byte) 101,
      (byte) 93,
      (byte) 27,
      (byte) 235,
      (byte) 90,
      (byte) 10,
      (byte) 26,
      (byte) 43,
      (byte) 8
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 178,
      (byte) 244,
      (byte) 84,
      (byte) 103,
      (byte) 64 /*0x40*/,
      (byte) 203,
      (byte) 173,
      (byte) 62,
      (byte) 133,
      (byte) 227,
      (byte) 135,
      (byte) 78,
      (byte) 91,
      (byte) 11,
      (byte) 178,
      (byte) 33,
      (byte) 139,
      (byte) 240 /*0xF0*/,
      (byte) 103,
      (byte) 59,
      (byte) 130,
      (byte) 206,
      (byte) 243,
      (byte) 147,
      (byte) 226,
      (byte) 192 /*0xC0*/,
      (byte) 175,
      (byte) 60,
      (byte) 189,
      (byte) 125,
      (byte) 19,
      (byte) 24,
      (byte) 181,
      (byte) 92,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 164,
      (byte) 109,
      (byte) 29,
      (byte) 136,
      (byte) 226,
      (byte) 131,
      (byte) 112 /*0x70*/,
      (byte) 213,
      (byte) 165,
      (byte) 218,
      (byte) 166,
      (byte) 23,
      (byte) 235,
      (byte) 83,
      (byte) 128 /*0x80*/,
      (byte) 133,
      (byte) 86,
      (byte) 51,
      (byte) 78
    };
    byte[] numArray20 = new byte[55];
    numArray20[11] = (byte) 191;
    numArray20[27] = (byte) 215;
    numArray20[2] = (byte) 89;
    numArray20[3] = (byte) 164;
    numArray20[45] = (byte) 61;
    numArray20[47] = (byte) 19;
    numArray20[43] = (byte) 226;
    numArray20[33] = (byte) 147;
    numArray20[8] = (byte) 180;
    numArray20[52] = (byte) 6;
    numArray20[10] = (byte) 190;
    numArray20[23] = (byte) 183;
    numArray20[12] = (byte) 125;
    numArray20[13] = (byte) 2;
    numArray20[14] = (byte) 182;
    numArray20[40] = (byte) 211;
    numArray20[16 /*0x10*/] = (byte) 196;
    numArray20[21] = (byte) 189;
    numArray20[54] = (byte) 152;
    numArray20[32 /*0x20*/] = (byte) 198;
    numArray20[20] = (byte) 214;
    numArray20[25] = (byte) 176 /*0xB0*/;
    numArray20[37] = (byte) 125;
    numArray20[51] = (byte) 79;
    numArray20[24] = (byte) 93;
    numArray20[9] = (byte) 192 /*0xC0*/;
    numArray20[41] = (byte) 126;
    numArray20[29] = (byte) 244;
    numArray20[28] = (byte) 158;
    numArray20[17] = (byte) 5;
    numArray20[30] = (byte) 128 /*0x80*/;
    numArray20[31 /*0x1F*/] = (byte) 76;
    numArray20[44] = (byte) 178;
    numArray20[36] = (byte) 135;
    numArray20[34] = (byte) 113;
    numArray20[1] = (byte) 113;
    numArray20[18] = (byte) 233;
    numArray20[35] = (byte) 151;
    numArray20[39] = (byte) 32 /*0x20*/;
    numArray20[7] = (byte) 220;
    numArray20[6] = (byte) 3;
    numArray20[5] = (byte) 19;
    numArray20[42] = (byte) 166;
    numArray20[4] = (byte) 38;
    numArray20[15] = (byte) 28;
    numArray20[26] = (byte) 226;
    numArray20[49] = (byte) 239;
    numArray20[46] = (byte) 35;
    numArray20[19] = (byte) 190;
    numArray20[38] = (byte) 208 /*0xD0*/;
    numArray20[0] = (byte) 88;
    numArray20[22] = (byte) 221;
    numArray20[48 /*0x30*/] = (byte) 202;
    numArray20[53] = (byte) 254;
    numArray20[50] = (byte) 192 /*0xC0*/;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[20];
    numArray21[15] = (byte) 53;
    numArray21[1] = (byte) 125;
    numArray21[2] = (byte) 102;
    numArray21[3] = (byte) 132;
    numArray21[4] = (byte) 31 /*0x1F*/;
    numArray21[5] = (byte) 22;
    numArray21[17] = (byte) 230;
    numArray21[7] = (byte) 65;
    numArray21[8] = (byte) 172;
    numArray21[9] = (byte) 22;
    numArray21[6] = (byte) 10;
    numArray21[11] = (byte) 240 /*0xF0*/;
    numArray21[12] = (byte) 70;
    numArray21[10] = (byte) 241;
    numArray21[13] = (byte) 153;
    numArray21[14] = (byte) 54;
    numArray21[19] = (byte) 244;
    numArray21[0] = (byte) 211;
    numArray21[18] = (byte) 252;
    numArray21[16 /*0x10*/] = (byte) 115;
    byte[] numArray22 = new byte[20]
    {
      (byte) 6,
      (byte) 199,
      (byte) 207,
      (byte) 139,
      (byte) 46,
      (byte) 88,
      (byte) 29,
      (byte) 221,
      (byte) 213,
      (byte) 217,
      (byte) 192 /*0xC0*/,
      (byte) 100,
      (byte) 77,
      (byte) 84,
      (byte) 103,
      (byte) 59,
      (byte) 125,
      (byte) 112 /*0x70*/,
      (byte) 209,
      (byte) 28
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 20);
    for (int index = 0; index < 20; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12615()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[266];
      byte[] numArray2 = new byte[55];
      numArray2[7] = (byte) 243;
      numArray2[19] = (byte) 98;
      numArray2[2] = (byte) 41;
      numArray2[28] = (byte) 148;
      numArray2[49] = (byte) 20;
      numArray2[33] = (byte) 154;
      numArray2[6] = (byte) 3;
      numArray2[34] = (byte) 189;
      numArray2[8] = (byte) 83;
      numArray2[9] = (byte) 145;
      numArray2[10] = (byte) 203;
      numArray2[11] = (byte) 27;
      numArray2[12] = (byte) 183;
      numArray2[25] = (byte) 204;
      numArray2[14] = (byte) 120;
      numArray2[15] = (byte) 61;
      numArray2[18] = (byte) 101;
      numArray2[17] = (byte) 110;
      numArray2[54] = (byte) 190;
      numArray2[38] = (byte) 111;
      numArray2[4] = (byte) 43;
      numArray2[48 /*0x30*/] = (byte) 111;
      numArray2[22] = (byte) 219;
      numArray2[53] = (byte) 39;
      numArray2[32 /*0x20*/] = (byte) 46;
      numArray2[44] = (byte) 134;
      numArray2[31 /*0x1F*/] = (byte) 83;
      numArray2[0] = (byte) 181;
      numArray2[16 /*0x10*/] = (byte) 129;
      numArray2[52] = (byte) 94;
      numArray2[39] = (byte) 93;
      numArray2[3] = (byte) 60;
      numArray2[26] = (byte) 254;
      numArray2[27] = (byte) 139;
      numArray2[23] = (byte) 244;
      numArray2[35] = (byte) 112 /*0x70*/;
      numArray2[36] = (byte) 184;
      numArray2[37] = (byte) 87;
      numArray2[24] = (byte) 136;
      numArray2[47] = (byte) 67;
      numArray2[40] = (byte) 71;
      numArray2[45] = (byte) 245;
      numArray2[43] = (byte) 131;
      numArray2[41] = (byte) 181;
      numArray2[30] = (byte) 149;
      numArray2[29] = (byte) 15;
      numArray2[46] = (byte) 178;
      numArray2[13] = (byte) 243;
      numArray2[42] = (byte) 105;
      numArray2[1] = (byte) 155;
      numArray2[50] = (byte) 106;
      numArray2[51] = (byte) 240 /*0xF0*/;
      numArray2[21] = (byte) 51;
      numArray2[20] = (byte) 109;
      numArray2[5] = (byte) 125;
      byte[] numArray3 = new byte[55];
      numArray3[16 /*0x10*/] = (byte) 10;
      numArray3[12] = (byte) 102;
      numArray3[40] = (byte) 142;
      numArray3[3] = (byte) 50;
      numArray3[4] = (byte) 49;
      numArray3[36] = (byte) 27;
      numArray3[6] = (byte) 95;
      numArray3[7] = (byte) 114;
      numArray3[46] = (byte) 19;
      numArray3[13] = (byte) 247;
      numArray3[10] = (byte) 197;
      numArray3[11] = (byte) 180;
      numArray3[2] = (byte) 4;
      numArray3[14] = (byte) 233;
      numArray3[5] = (byte) 5;
      numArray3[35] = (byte) 166;
      numArray3[15] = (byte) 179;
      numArray3[39] = (byte) 116;
      numArray3[34] = (byte) 158;
      numArray3[19] = (byte) 26;
      numArray3[20] = (byte) 222;
      numArray3[21] = (byte) 92;
      numArray3[22] = (byte) 222;
      numArray3[32 /*0x20*/] = (byte) 171;
      numArray3[24] = (byte) 229;
      numArray3[25] = (byte) 252;
      numArray3[26] = (byte) 12;
      numArray3[27] = (byte) 85;
      numArray3[28] = (byte) 149;
      numArray3[29] = (byte) 205;
      numArray3[8] = (byte) 184;
      numArray3[31 /*0x1F*/] = (byte) 17;
      numArray3[33] = (byte) 2;
      numArray3[44] = (byte) 138;
      numArray3[0] = (byte) 162;
      numArray3[43] = (byte) 150;
      numArray3[48 /*0x30*/] = (byte) 4;
      numArray3[18] = (byte) 111;
      numArray3[38] = (byte) 69;
      numArray3[42] = (byte) 95;
      numArray3[30] = (byte) 25;
      numArray3[41] = (byte) 151;
      numArray3[52] = (byte) 111;
      numArray3[23] = (byte) 31 /*0x1F*/;
      numArray3[45] = (byte) 90;
      numArray3[51] = (byte) 181;
      numArray3[54] = (byte) 5;
      numArray3[47] = (byte) 149;
      numArray3[1] = (byte) 31 /*0x1F*/;
      numArray3[49] = (byte) 254;
      numArray3[50] = (byte) 251;
      numArray3[17] = (byte) 166;
      numArray3[9] = (byte) 50;
      numArray3[53] = (byte) 190;
      numArray3[37] = (byte) 149;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[50] = (byte) 28;
      numArray4[1] = (byte) 148;
      numArray4[2] = (byte) 191;
      numArray4[3] = (byte) 24;
      numArray4[31 /*0x1F*/] = (byte) 51;
      numArray4[33] = (byte) 178;
      numArray4[6] = (byte) 59;
      numArray4[7] = (byte) 164;
      numArray4[8] = (byte) 228;
      numArray4[20] = (byte) 162;
      numArray4[10] = (byte) 107;
      numArray4[11] = (byte) 119;
      numArray4[12] = (byte) 15;
      numArray4[16 /*0x10*/] = (byte) 16 /*0x10*/;
      numArray4[28] = (byte) 174;
      numArray4[9] = (byte) 235;
      numArray4[4] = (byte) 164;
      numArray4[30] = (byte) 56;
      numArray4[18] = (byte) 237;
      numArray4[22] = (byte) 105;
      numArray4[40] = (byte) 73;
      numArray4[24] = (byte) 153;
      numArray4[44] = (byte) 230;
      numArray4[23] = (byte) 51;
      numArray4[38] = (byte) 208 /*0xD0*/;
      numArray4[41] = (byte) 138;
      numArray4[5] = (byte) 239;
      numArray4[27] = (byte) 170;
      numArray4[39] = (byte) 133;
      numArray4[29] = (byte) 113;
      numArray4[0] = (byte) 105;
      numArray4[48 /*0x30*/] = (byte) 182;
      numArray4[54] = (byte) 242;
      numArray4[35] = (byte) 239;
      numArray4[34] = (byte) 82;
      numArray4[17] = (byte) 213;
      numArray4[47] = (byte) 70;
      numArray4[21] = (byte) 210;
      numArray4[32 /*0x20*/] = (byte) 111;
      numArray4[14] = (byte) 3;
      numArray4[46] = (byte) 254;
      numArray4[45] = (byte) 155;
      numArray4[42] = (byte) 166;
      numArray4[43] = (byte) 54;
      numArray4[37] = (byte) 1;
      numArray4[53] = (byte) 245;
      numArray4[36] = (byte) 11;
      numArray4[13] = (byte) 70;
      numArray4[26] = (byte) 99;
      numArray4[49] = (byte) 225;
      numArray4[15] = (byte) 34;
      numArray4[51] = (byte) 236;
      numArray4[52] = (byte) 181;
      numArray4[19] = (byte) 30;
      numArray4[25] = (byte) 167;
      byte[] numArray5 = new byte[55]
      {
        (byte) 210,
        (byte) 146,
        (byte) 55,
        (byte) 157,
        (byte) 156,
        (byte) 162,
        (byte) 53,
        (byte) 165,
        (byte) 69,
        (byte) 35,
        (byte) 198,
        (byte) 104,
        (byte) 169,
        (byte) 115,
        (byte) 224 /*0xE0*/,
        (byte) 147,
        (byte) 54,
        (byte) 209,
        (byte) 132,
        (byte) 141,
        (byte) 22,
        (byte) 11,
        (byte) 164,
        (byte) 245,
        (byte) 161,
        (byte) 170,
        (byte) 17,
        (byte) 177,
        (byte) 215,
        (byte) 174,
        (byte) 202,
        (byte) 229,
        (byte) 58,
        (byte) 236,
        (byte) 60,
        (byte) 242,
        (byte) 17,
        (byte) 253,
        (byte) 137,
        (byte) 247,
        (byte) 127 /*0x7F*/,
        (byte) 172,
        (byte) 107,
        (byte) 190,
        (byte) 226,
        (byte) 67,
        (byte) 31 /*0x1F*/,
        (byte) 189,
        (byte) 208 /*0xD0*/,
        (byte) 98,
        (byte) 198,
        (byte) 175,
        (byte) 27,
        (byte) 146,
        (byte) 171
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 63 /*0x3F*/,
        (byte) 128 /*0x80*/,
        (byte) 84,
        (byte) 110,
        (byte) 157,
        (byte) 40,
        (byte) 30,
        (byte) 102,
        (byte) 186,
        (byte) 141,
        (byte) 157,
        (byte) 56,
        (byte) 80 /*0x50*/,
        (byte) 152,
        (byte) 139,
        (byte) 49,
        (byte) 109,
        (byte) 28,
        (byte) 132,
        (byte) 248,
        (byte) 175,
        (byte) 158,
        (byte) 105,
        (byte) 7,
        (byte) 203,
        (byte) 82,
        (byte) 3,
        (byte) 70,
        (byte) 100,
        (byte) 47,
        (byte) 90,
        (byte) 229,
        (byte) 80 /*0x50*/,
        (byte) 205,
        (byte) 63 /*0x3F*/,
        (byte) 2,
        (byte) 127 /*0x7F*/,
        (byte) 138,
        (byte) 26,
        (byte) 107,
        (byte) 20,
        (byte) 28,
        (byte) 243,
        (byte) 6,
        (byte) 4,
        (byte) 25,
        (byte) 81,
        (byte) 49,
        (byte) 131,
        (byte) 182,
        (byte) 224 /*0xE0*/,
        (byte) 3,
        (byte) 85,
        (byte) 114,
        (byte) 71
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 145,
        (byte) 217,
        (byte) 43,
        (byte) 49,
        (byte) 179,
        (byte) 223,
        (byte) 217,
        (byte) 97,
        (byte) 182,
        (byte) 149,
        (byte) 160 /*0xA0*/,
        (byte) 154,
        (byte) 111,
        (byte) 85,
        (byte) 60,
        (byte) 39,
        (byte) 232,
        (byte) 235,
        (byte) 143,
        (byte) 19,
        (byte) 93,
        (byte) 182,
        (byte) 93,
        (byte) 90,
        (byte) 139,
        (byte) 31 /*0x1F*/,
        (byte) 196,
        (byte) 67,
        (byte) 209,
        (byte) 220,
        (byte) 120,
        (byte) 51,
        (byte) 95,
        (byte) 140,
        (byte) 58,
        (byte) 141,
        (byte) 232,
        (byte) 176 /*0xB0*/,
        (byte) 42,
        (byte) 18,
        (byte) 115,
        (byte) 220,
        (byte) 84,
        (byte) 252,
        (byte) 172,
        (byte) 203,
        (byte) 123,
        (byte) 47,
        (byte) 69,
        (byte) 10,
        (byte) 0,
        (byte) 7,
        (byte) 203,
        (byte) 220,
        (byte) 51
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 77,
        (byte) 163,
        (byte) 74,
        (byte) 36,
        (byte) 216,
        (byte) 2,
        (byte) 138,
        (byte) 51,
        (byte) 183,
        (byte) 232,
        (byte) 130,
        (byte) 89,
        (byte) 136,
        (byte) 177,
        (byte) 227,
        (byte) 2,
        (byte) 25,
        (byte) 228,
        (byte) 147,
        (byte) 170,
        (byte) 99,
        (byte) 3,
        (byte) 178,
        (byte) 1,
        (byte) 210,
        (byte) 194,
        (byte) 173,
        (byte) 78,
        (byte) 86,
        (byte) 144 /*0x90*/,
        (byte) 221,
        (byte) 133,
        (byte) 158,
        (byte) 236,
        (byte) 50,
        (byte) 231,
        (byte) 208 /*0xD0*/,
        (byte) 70,
        (byte) 59,
        (byte) 26,
        (byte) 104,
        (byte) 210,
        (byte) 76,
        (byte) 48 /*0x30*/,
        (byte) 206,
        (byte) 216,
        (byte) 250,
        (byte) 4,
        (byte) 253,
        (byte) 1,
        (byte) 59,
        (byte) 142,
        (byte) 180,
        (byte) 246,
        (byte) 178
      };
      byte[] numArray9 = new byte[55];
      numArray9[12] = (byte) 34;
      numArray9[1] = (byte) 127 /*0x7F*/;
      numArray9[2] = (byte) 183;
      numArray9[22] = (byte) 61;
      numArray9[4] = (byte) 85;
      numArray9[5] = (byte) 252;
      numArray9[29] = (byte) 50;
      numArray9[49] = (byte) 217;
      numArray9[8] = (byte) 77;
      numArray9[20] = (byte) 59;
      numArray9[10] = (byte) 98;
      numArray9[11] = (byte) 245;
      numArray9[42] = (byte) 167;
      numArray9[35] = (byte) 117;
      numArray9[45] = (byte) 118;
      numArray9[15] = (byte) 164;
      numArray9[16 /*0x10*/] = (byte) 140;
      numArray9[17] = (byte) 6;
      numArray9[36] = (byte) 239;
      numArray9[9] = (byte) 64 /*0x40*/;
      numArray9[3] = (byte) 180;
      numArray9[21] = (byte) 174;
      numArray9[43] = (byte) 79;
      numArray9[23] = (byte) 241;
      numArray9[13] = (byte) 87;
      numArray9[25] = (byte) 69;
      numArray9[0] = (byte) 145;
      numArray9[33] = (byte) 118;
      numArray9[37] = (byte) 145;
      numArray9[6] = (byte) 12;
      numArray9[30] = (byte) 52;
      numArray9[31 /*0x1F*/] = (byte) 250;
      numArray9[32 /*0x20*/] = (byte) 10;
      numArray9[40] = (byte) 12;
      numArray9[19] = (byte) 18;
      numArray9[14] = (byte) 14;
      numArray9[41] = (byte) 149;
      numArray9[53] = (byte) 166;
      numArray9[38] = (byte) 211;
      numArray9[39] = (byte) 142;
      numArray9[27] = (byte) 231;
      numArray9[34] = (byte) 180;
      numArray9[28] = (byte) 81;
      numArray9[26] = (byte) 138;
      numArray9[44] = (byte) 69;
      numArray9[47] = (byte) 35;
      numArray9[46] = (byte) 102;
      numArray9[51] = (byte) 173;
      numArray9[48 /*0x30*/] = (byte) 186;
      numArray9[7] = (byte) 226;
      numArray9[50] = (byte) 199;
      numArray9[52] = (byte) 47;
      numArray9[24] = (byte) 51;
      numArray9[18] = (byte) 230;
      numArray9[54] = (byte) 157;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[46]
      {
        (byte) 21,
        (byte) 87,
        (byte) 77,
        (byte) 171,
        (byte) 234,
        (byte) 135,
        (byte) 210,
        (byte) 187,
        (byte) 43,
        (byte) 18,
        (byte) 98,
        (byte) 151,
        (byte) 5,
        (byte) 170,
        (byte) 221,
        (byte) 130,
        (byte) 178,
        (byte) 20,
        (byte) 155,
        (byte) 145,
        (byte) 91,
        (byte) 98,
        (byte) 220,
        (byte) 105,
        (byte) 35,
        (byte) 79,
        (byte) 134,
        (byte) 234,
        (byte) 104,
        (byte) 149,
        (byte) 243,
        (byte) 23,
        (byte) 84,
        (byte) 135,
        (byte) 114,
        (byte) 157,
        (byte) 216,
        (byte) 26,
        (byte) 66,
        (byte) 110,
        (byte) 82,
        (byte) 106,
        (byte) 165,
        (byte) 10,
        (byte) 251,
        (byte) 238
      };
      byte[] numArray11 = new byte[46]
      {
        (byte) 179,
        (byte) 105,
        (byte) 36,
        (byte) 150,
        (byte) 29,
        (byte) 174,
        (byte) 96 /*0x60*/,
        (byte) 179,
        (byte) 32 /*0x20*/,
        (byte) 167,
        (byte) 25,
        (byte) 17,
        (byte) 186,
        (byte) 87,
        (byte) 120,
        (byte) 174,
        (byte) 90,
        (byte) 162,
        (byte) 249,
        (byte) 253,
        (byte) 219,
        (byte) 51,
        (byte) 181,
        (byte) 18,
        (byte) 64 /*0x40*/,
        (byte) 176 /*0xB0*/,
        (byte) 90,
        (byte) 145,
        (byte) 39,
        (byte) 209,
        (byte) 61,
        (byte) 135,
        (byte) 35,
        (byte) 139,
        (byte) 124,
        (byte) 101,
        (byte) 48 /*0x30*/,
        (byte) 254,
        (byte) 116,
        (byte) 180,
        (byte) 245,
        (byte) 31 /*0x1F*/,
        (byte) 210,
        (byte) 7,
        (byte) 178,
        (byte) 25
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[266];
    byte[] numArray13 = new byte[55];
    numArray13[34] = (byte) 4;
    numArray13[15] = (byte) 31 /*0x1F*/;
    numArray13[7] = (byte) 122;
    numArray13[3] = (byte) 134;
    numArray13[28] = (byte) 209;
    numArray13[5] = (byte) 19;
    numArray13[44] = (byte) 167;
    numArray13[17] = (byte) 66;
    numArray13[25] = (byte) 63 /*0x3F*/;
    numArray13[48 /*0x30*/] = (byte) 15;
    numArray13[52] = (byte) 126;
    numArray13[11] = (byte) 1;
    numArray13[38] = (byte) 83;
    numArray13[13] = (byte) 82;
    numArray13[12] = (byte) 231;
    numArray13[9] = (byte) 136;
    numArray13[18] = (byte) 126;
    numArray13[19] = (byte) 55;
    numArray13[6] = (byte) 179;
    numArray13[36] = (byte) 83;
    numArray13[16 /*0x10*/] = (byte) 75;
    numArray13[21] = (byte) 170;
    numArray13[22] = (byte) 151;
    numArray13[2] = (byte) 159;
    numArray13[24] = (byte) 44;
    numArray13[20] = (byte) 63 /*0x3F*/;
    numArray13[29] = (byte) 109;
    numArray13[27] = (byte) 188;
    numArray13[50] = (byte) 176 /*0xB0*/;
    numArray13[14] = (byte) 149;
    numArray13[46] = (byte) 106;
    numArray13[23] = (byte) 210;
    numArray13[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray13[33] = (byte) 94;
    numArray13[31 /*0x1F*/] = (byte) 83;
    numArray13[35] = (byte) 12;
    numArray13[49] = (byte) 235;
    numArray13[37] = (byte) 145;
    numArray13[4] = (byte) 1;
    numArray13[39] = (byte) 115;
    numArray13[26] = (byte) 240 /*0xF0*/;
    numArray13[8] = (byte) 228;
    numArray13[10] = (byte) 155;
    numArray13[43] = (byte) 146;
    numArray13[41] = (byte) 35;
    numArray13[45] = (byte) 109;
    numArray13[1] = (byte) 137;
    numArray13[47] = (byte) 24;
    numArray13[42] = (byte) 243;
    numArray13[40] = (byte) 190;
    numArray13[30] = (byte) 53;
    numArray13[51] = (byte) 171;
    numArray13[0] = (byte) 100;
    numArray13[53] = (byte) 237;
    numArray13[54] = (byte) 76;
    byte[] numArray14 = new byte[55];
    numArray14[12] = (byte) 193;
    numArray14[1] = (byte) 187;
    numArray14[2] = (byte) 29;
    numArray14[31 /*0x1F*/] = (byte) 20;
    numArray14[4] = (byte) 209;
    numArray14[0] = (byte) 246;
    numArray14[6] = (byte) 17;
    numArray14[7] = (byte) 168;
    numArray14[54] = (byte) 82;
    numArray14[9] = (byte) 17;
    numArray14[13] = (byte) 211;
    numArray14[8] = (byte) 33;
    numArray14[29] = (byte) 40;
    numArray14[37] = (byte) 129;
    numArray14[14] = (byte) 142;
    numArray14[15] = (byte) 79;
    numArray14[48 /*0x30*/] = (byte) 51;
    numArray14[17] = (byte) 75;
    numArray14[18] = (byte) 141;
    numArray14[35] = (byte) 71;
    numArray14[20] = (byte) 38;
    numArray14[21] = (byte) 75;
    numArray14[53] = (byte) 193;
    numArray14[43] = (byte) 88;
    numArray14[11] = (byte) 86;
    numArray14[25] = (byte) 26;
    numArray14[26] = (byte) 212;
    numArray14[22] = (byte) 50;
    numArray14[5] = (byte) 254;
    numArray14[3] = (byte) 154;
    numArray14[16 /*0x10*/] = (byte) 107;
    numArray14[28] = (byte) 27;
    numArray14[32 /*0x20*/] = (byte) 152;
    numArray14[39] = (byte) 174;
    numArray14[33] = (byte) 157;
    numArray14[19] = (byte) 178;
    numArray14[23] = (byte) 34;
    numArray14[50] = (byte) 210;
    numArray14[38] = (byte) 170;
    numArray14[42] = (byte) 161;
    numArray14[40] = (byte) 232;
    numArray14[41] = (byte) 87;
    numArray14[27] = (byte) 122;
    numArray14[10] = (byte) 214;
    numArray14[44] = (byte) 182;
    numArray14[45] = (byte) 200;
    numArray14[46] = (byte) 223;
    numArray14[47] = (byte) 179;
    numArray14[49] = (byte) 180;
    numArray14[30] = (byte) 120;
    numArray14[24] = (byte) 129;
    numArray14[51] = (byte) 48 /*0x30*/;
    numArray14[52] = (byte) 104;
    numArray14[36] = (byte) 52;
    numArray14[34] = (byte) 27;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[26] = (byte) 233;
    numArray15[0] = (byte) 213;
    numArray15[28] = (byte) 113;
    numArray15[39] = (byte) 187;
    numArray15[38] = (byte) 87;
    numArray15[5] = (byte) 253;
    numArray15[6] = (byte) 245;
    numArray15[25] = (byte) 11;
    numArray15[8] = (byte) 16 /*0x10*/;
    numArray15[52] = (byte) 165;
    numArray15[14] = (byte) 232;
    numArray15[10] = (byte) 150;
    numArray15[18] = (byte) 233;
    numArray15[13] = (byte) 95;
    numArray15[19] = (byte) 122;
    numArray15[15] = (byte) 186;
    numArray15[44] = (byte) 211;
    numArray15[17] = (byte) 53;
    numArray15[37] = (byte) 200;
    numArray15[36] = (byte) 172;
    numArray15[35] = (byte) 240 /*0xF0*/;
    numArray15[21] = (byte) 223;
    numArray15[22] = (byte) 81;
    numArray15[23] = (byte) 138;
    numArray15[43] = (byte) 112 /*0x70*/;
    numArray15[24] = (byte) 69;
    numArray15[47] = (byte) 124;
    numArray15[33] = (byte) 8;
    numArray15[7] = (byte) 250;
    numArray15[29] = (byte) 169;
    numArray15[3] = (byte) 61;
    numArray15[31 /*0x1F*/] = (byte) 82;
    numArray15[32 /*0x20*/] = (byte) 129;
    numArray15[45] = (byte) 13;
    numArray15[16 /*0x10*/] = (byte) 181;
    numArray15[20] = (byte) 78;
    numArray15[1] = (byte) 170;
    numArray15[11] = (byte) 186;
    numArray15[46] = (byte) 219;
    numArray15[12] = (byte) 61;
    numArray15[40] = (byte) 203;
    numArray15[41] = (byte) 125;
    numArray15[42] = (byte) 219;
    numArray15[30] = (byte) 59;
    numArray15[4] = (byte) 37;
    numArray15[2] = (byte) 248;
    numArray15[9] = (byte) 47;
    numArray15[34] = (byte) 14;
    numArray15[48 /*0x30*/] = (byte) 139;
    numArray15[49] = (byte) 162;
    numArray15[50] = (byte) 2;
    numArray15[51] = (byte) 141;
    numArray15[27] = (byte) 81;
    numArray15[53] = (byte) 76;
    numArray15[54] = (byte) 216;
    byte[] numArray16 = new byte[55]
    {
      (byte) 6,
      (byte) 180,
      (byte) 176 /*0xB0*/,
      (byte) 17,
      (byte) 66,
      (byte) 64 /*0x40*/,
      (byte) 0,
      (byte) 131,
      (byte) 82,
      (byte) 11,
      (byte) 96 /*0x60*/,
      (byte) 75,
      (byte) 4,
      (byte) 135,
      (byte) 63 /*0x3F*/,
      (byte) 146,
      (byte) 46,
      (byte) 76,
      (byte) 174,
      (byte) 130,
      (byte) 173,
      (byte) 20,
      (byte) 31 /*0x1F*/,
      (byte) 248,
      (byte) 72,
      (byte) 195,
      (byte) 59,
      (byte) 42,
      (byte) 143,
      (byte) 10,
      (byte) 185,
      (byte) 31 /*0x1F*/,
      (byte) 42,
      (byte) 10,
      (byte) 169,
      (byte) 123,
      (byte) 240 /*0xF0*/,
      (byte) 71,
      (byte) 161,
      (byte) 228,
      (byte) 156,
      (byte) 181,
      (byte) 55,
      (byte) 209,
      (byte) 200,
      (byte) 109,
      (byte) 66,
      (byte) 145,
      (byte) 171,
      (byte) 199,
      (byte) 93,
      (byte) 150,
      (byte) 13,
      (byte) 192 /*0xC0*/,
      (byte) 215
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 183,
      (byte) 191,
      (byte) 234,
      (byte) 167,
      (byte) 221,
      (byte) 185,
      (byte) 191,
      (byte) 68,
      (byte) 151,
      (byte) 4,
      (byte) 4,
      (byte) 56,
      (byte) 95,
      (byte) 207,
      (byte) 28,
      (byte) 160 /*0xA0*/,
      (byte) 134,
      (byte) 136,
      (byte) 220,
      (byte) 142,
      (byte) 99,
      (byte) 38,
      (byte) 99,
      (byte) 136,
      (byte) 64 /*0x40*/,
      (byte) 50,
      (byte) 57,
      (byte) 146,
      (byte) 254,
      (byte) 53,
      (byte) 150,
      (byte) 41,
      (byte) 37,
      (byte) 31 /*0x1F*/,
      (byte) 134,
      (byte) 17,
      (byte) 83,
      byte.MaxValue,
      (byte) 73,
      (byte) 58,
      (byte) 181,
      (byte) 56,
      (byte) 248,
      (byte) 207,
      (byte) 162,
      (byte) 243,
      (byte) 52,
      (byte) 197,
      (byte) 248,
      (byte) 15,
      (byte) 240 /*0xF0*/,
      (byte) 21,
      (byte) 42,
      (byte) 100,
      (byte) 21
    };
    byte[] numArray18 = new byte[55];
    numArray18[3] = (byte) 27;
    numArray18[42] = (byte) 6;
    numArray18[19] = (byte) 243;
    numArray18[18] = (byte) 159;
    numArray18[4] = (byte) 73;
    numArray18[5] = (byte) 92;
    numArray18[6] = (byte) 75;
    numArray18[41] = (byte) 69;
    numArray18[40] = (byte) 46;
    numArray18[9] = (byte) 124;
    numArray18[10] = (byte) 178;
    numArray18[48 /*0x30*/] = (byte) 161;
    numArray18[12] = (byte) 138;
    numArray18[17] = (byte) 166;
    numArray18[26] = (byte) 238;
    numArray18[15] = (byte) 127 /*0x7F*/;
    numArray18[16 /*0x10*/] = (byte) 197;
    numArray18[34] = (byte) 234;
    numArray18[30] = (byte) 95;
    numArray18[39] = (byte) 107;
    numArray18[20] = (byte) 48 /*0x30*/;
    numArray18[25] = (byte) 186;
    numArray18[1] = (byte) 241;
    numArray18[24] = (byte) 11;
    numArray18[35] = (byte) 52;
    numArray18[14] = (byte) 8;
    numArray18[7] = (byte) 8;
    numArray18[27] = (byte) 113;
    numArray18[13] = (byte) 31 /*0x1F*/;
    numArray18[32 /*0x20*/] = (byte) 206;
    numArray18[21] = (byte) 33;
    numArray18[31 /*0x1F*/] = (byte) 196;
    numArray18[8] = (byte) 233;
    numArray18[33] = (byte) 141;
    numArray18[36] = (byte) 59;
    numArray18[0] = (byte) 12;
    numArray18[49] = (byte) 69;
    numArray18[37] = (byte) 25;
    numArray18[38] = (byte) 209;
    numArray18[11] = (byte) 20;
    numArray18[54] = (byte) 69;
    numArray18[29] = (byte) 222;
    numArray18[43] = (byte) 107;
    numArray18[23] = (byte) 33;
    numArray18[44] = (byte) 16 /*0x10*/;
    numArray18[45] = (byte) 224 /*0xE0*/;
    numArray18[46] = (byte) 195;
    numArray18[47] = (byte) 39;
    numArray18[2] = (byte) 135;
    numArray18[28] = (byte) 253;
    numArray18[50] = (byte) 89;
    numArray18[51] = (byte) 236;
    numArray18[52] = (byte) 189;
    numArray18[53] = (byte) 27;
    numArray18[22] = (byte) 182;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 124,
      (byte) 27,
      (byte) 172,
      (byte) 11,
      (byte) 146,
      (byte) 86,
      (byte) 29,
      (byte) 236,
      (byte) 220,
      (byte) 82,
      (byte) 54,
      (byte) 82,
      (byte) 85,
      (byte) 92,
      (byte) 238,
      (byte) 159,
      (byte) 230,
      (byte) 79,
      (byte) 56,
      (byte) 151,
      (byte) 49,
      (byte) 29,
      (byte) 107,
      (byte) 10,
      (byte) 248,
      (byte) 173,
      (byte) 45,
      (byte) 218,
      (byte) 171,
      (byte) 116,
      (byte) 52,
      (byte) 31 /*0x1F*/,
      (byte) 148,
      (byte) 237,
      (byte) 117,
      (byte) 125,
      (byte) 242,
      (byte) 175,
      (byte) 246,
      (byte) 155,
      (byte) 121,
      (byte) 117,
      (byte) 235,
      (byte) 141,
      (byte) 189,
      (byte) 41,
      (byte) 151,
      (byte) 101,
      (byte) 123,
      (byte) 54,
      (byte) 162,
      (byte) 60,
      (byte) 178,
      (byte) 202,
      (byte) 150
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 243,
      (byte) 178,
      (byte) 75,
      (byte) 162,
      (byte) 131,
      (byte) 197,
      (byte) 122,
      (byte) 50,
      (byte) 36,
      (byte) 193,
      (byte) 117,
      (byte) 241,
      (byte) 196,
      (byte) 55,
      (byte) 52,
      (byte) 0,
      (byte) 161,
      (byte) 30,
      (byte) 54,
      (byte) 66,
      (byte) 79,
      (byte) 106,
      (byte) 85,
      (byte) 169,
      (byte) 42,
      (byte) 192 /*0xC0*/,
      (byte) 164,
      (byte) 17,
      (byte) 49,
      (byte) 149,
      (byte) 52,
      (byte) 231,
      (byte) 54,
      (byte) 68,
      (byte) 254,
      (byte) 175,
      (byte) 92,
      (byte) 246,
      (byte) 183,
      (byte) 120,
      (byte) 219,
      (byte) 40,
      (byte) 28,
      (byte) 23,
      (byte) 151,
      (byte) 103,
      (byte) 97,
      (byte) 106,
      (byte) 165,
      (byte) 6,
      (byte) 110,
      (byte) 253,
      (byte) 203,
      (byte) 49,
      (byte) 64 /*0x40*/
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[46];
    numArray21[22] = (byte) 207;
    numArray21[5] = (byte) 65;
    numArray21[44] = (byte) 48 /*0x30*/;
    numArray21[4] = (byte) 66;
    numArray21[13] = (byte) 173;
    numArray21[3] = (byte) 41;
    numArray21[36] = (byte) 18;
    numArray21[7] = (byte) 30;
    numArray21[8] = (byte) 22;
    numArray21[34] = (byte) 79;
    numArray21[30] = (byte) 202;
    numArray21[21] = (byte) 77;
    numArray21[12] = (byte) 113;
    numArray21[23] = (byte) 21;
    numArray21[14] = (byte) 55;
    numArray21[15] = (byte) 88;
    numArray21[42] = (byte) 166;
    numArray21[17] = (byte) 30;
    numArray21[18] = (byte) 188;
    numArray21[27] = (byte) 209;
    numArray21[20] = (byte) 221;
    numArray21[16 /*0x10*/] = (byte) 156;
    numArray21[33] = (byte) 205;
    numArray21[35] = (byte) 155;
    numArray21[24] = (byte) 200;
    numArray21[40] = (byte) 132;
    numArray21[26] = (byte) 116;
    numArray21[11] = (byte) 230;
    numArray21[10] = (byte) 103;
    numArray21[29] = (byte) 109;
    numArray21[19] = (byte) 49;
    numArray21[2] = (byte) 228;
    numArray21[32 /*0x20*/] = (byte) 22;
    numArray21[25] = (byte) 95;
    numArray21[45] = (byte) 127 /*0x7F*/;
    numArray21[41] = (byte) 111;
    numArray21[9] = (byte) 190;
    numArray21[37] = (byte) 114;
    numArray21[39] = (byte) 52;
    numArray21[0] = (byte) 162;
    numArray21[1] = (byte) 19;
    numArray21[31 /*0x1F*/] = (byte) 87;
    numArray21[6] = (byte) 103;
    numArray21[43] = (byte) 107;
    numArray21[38] = (byte) 54;
    numArray21[28] = (byte) 247;
    byte[] numArray22 = new byte[46];
    numArray22[38] = (byte) 127 /*0x7F*/;
    numArray22[1] = (byte) 104;
    numArray22[29] = (byte) 41;
    numArray22[3] = (byte) 229;
    numArray22[4] = (byte) 186;
    numArray22[5] = (byte) 231;
    numArray22[9] = (byte) 120;
    numArray22[23] = (byte) 79;
    numArray22[2] = (byte) 34;
    numArray22[36] = (byte) 180;
    numArray22[25] = byte.MaxValue;
    numArray22[6] = (byte) 19;
    numArray22[12] = (byte) 5;
    numArray22[13] = (byte) 179;
    numArray22[31 /*0x1F*/] = (byte) 161;
    numArray22[15] = (byte) 71;
    numArray22[11] = (byte) 133;
    numArray22[28] = (byte) 180;
    numArray22[40] = (byte) 39;
    numArray22[19] = (byte) 131;
    numArray22[20] = (byte) 70;
    numArray22[21] = (byte) 183;
    numArray22[22] = (byte) 118;
    numArray22[0] = (byte) 239;
    numArray22[18] = (byte) 215;
    numArray22[16 /*0x10*/] = (byte) 177;
    numArray22[26] = (byte) 22;
    numArray22[27] = (byte) 101;
    numArray22[10] = (byte) 0;
    numArray22[33] = (byte) 72;
    numArray22[30] = (byte) 18;
    numArray22[7] = (byte) 185;
    numArray22[39] = (byte) 180;
    numArray22[17] = (byte) 162;
    numArray22[34] = (byte) 50;
    numArray22[35] = (byte) 86;
    numArray22[37] = (byte) 147;
    numArray22[14] = (byte) 127 /*0x7F*/;
    numArray22[32 /*0x20*/] = (byte) 132;
    numArray22[8] = (byte) 83;
    numArray22[24] = (byte) 122;
    numArray22[41] = (byte) 105;
    numArray22[42] = (byte) 87;
    numArray22[43] = (byte) 102;
    numArray22[44] = (byte) 210;
    numArray22[45] = (byte) 57;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 46);
    for (int index = 0; index < 46; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12616()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[2] = (byte) 76;
      numArray2[1] = (byte) 152;
      numArray2[7] = (byte) 133;
      numArray2[3] = (byte) 119;
      numArray2[4] = (byte) 90;
      numArray2[5] = (byte) 249;
      numArray2[6] = (byte) 199;
      numArray2[8] = (byte) 192 /*0xC0*/;
      numArray2[0] = (byte) 105;
      byte[] numArray3 = new byte[9]
      {
        (byte) 229,
        (byte) 200,
        (byte) 205,
        (byte) 206,
        (byte) 14,
        (byte) 37,
        (byte) 59,
        (byte) 80 /*0x50*/,
        (byte) 112 /*0x70*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 194,
      (byte) 62,
      (byte) 5,
      (byte) 244,
      (byte) 36,
      (byte) 126,
      (byte) 157,
      (byte) 34,
      (byte) 152
    };
    byte[] numArray6 = new byte[9];
    numArray6[4] = (byte) 107;
    numArray6[1] = (byte) 117;
    numArray6[8] = (byte) 235;
    numArray6[0] = (byte) 245;
    numArray6[3] = (byte) 101;
    numArray6[7] = (byte) 125;
    numArray6[6] = (byte) 230;
    numArray6[2] = (byte) 124;
    numArray6[5] = (byte) 44;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12617()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[238];
      byte[] numArray2 = new byte[55]
      {
        (byte) 237,
        (byte) 251,
        (byte) 74,
        (byte) 55,
        (byte) 208 /*0xD0*/,
        (byte) 47,
        (byte) 246,
        (byte) 10,
        (byte) 219,
        (byte) 157,
        (byte) 43,
        (byte) 246,
        (byte) 167,
        (byte) 89,
        (byte) 71,
        (byte) 131,
        (byte) 54,
        (byte) 123,
        (byte) 41,
        (byte) 98,
        (byte) 40,
        (byte) 155,
        (byte) 201,
        (byte) 39,
        (byte) 36,
        (byte) 76,
        (byte) 177,
        (byte) 178,
        (byte) 191,
        (byte) 243,
        (byte) 175,
        (byte) 98,
        (byte) 152,
        (byte) 197,
        (byte) 80 /*0x50*/,
        (byte) 9,
        (byte) 169,
        (byte) 236,
        (byte) 173,
        (byte) 109,
        (byte) 44,
        (byte) 141,
        (byte) 191,
        (byte) 69,
        (byte) 78,
        (byte) 113,
        (byte) 213,
        (byte) 180,
        (byte) 206,
        (byte) 72,
        (byte) 112 /*0x70*/,
        (byte) 165,
        (byte) 162,
        (byte) 55,
        (byte) 213
      };
      byte[] numArray3 = new byte[55];
      numArray3[43] = (byte) 83;
      numArray3[1] = (byte) 82;
      numArray3[2] = (byte) 215;
      numArray3[15] = (byte) 126;
      numArray3[4] = (byte) 224 /*0xE0*/;
      numArray3[5] = (byte) 4;
      numArray3[31 /*0x1F*/] = (byte) 75;
      numArray3[17] = (byte) 14;
      numArray3[8] = (byte) 191;
      numArray3[9] = (byte) 251;
      numArray3[23] = (byte) 184;
      numArray3[20] = (byte) 102;
      numArray3[6] = (byte) 184;
      numArray3[13] = (byte) 128 /*0x80*/;
      numArray3[14] = (byte) 144 /*0x90*/;
      numArray3[39] = (byte) 28;
      numArray3[37] = (byte) 187;
      numArray3[16 /*0x10*/] = (byte) 148;
      numArray3[10] = (byte) 27;
      numArray3[19] = (byte) 227;
      numArray3[36] = (byte) 105;
      numArray3[26] = (byte) 163;
      numArray3[21] = (byte) 237;
      numArray3[3] = (byte) 54;
      numArray3[24] = (byte) 185;
      numArray3[18] = (byte) 227;
      numArray3[22] = (byte) 118;
      numArray3[42] = (byte) 47;
      numArray3[12] = (byte) 106;
      numArray3[29] = (byte) 92;
      numArray3[30] = (byte) 208 /*0xD0*/;
      numArray3[34] = (byte) 39;
      numArray3[32 /*0x20*/] = (byte) 60;
      numArray3[33] = (byte) 226;
      numArray3[25] = (byte) 238;
      numArray3[35] = (byte) 120;
      numArray3[47] = (byte) 207;
      numArray3[27] = (byte) 42;
      numArray3[38] = (byte) 85;
      numArray3[50] = (byte) 221;
      numArray3[40] = (byte) 168;
      numArray3[45] = (byte) 139;
      numArray3[28] = (byte) 98;
      numArray3[7] = (byte) 5;
      numArray3[0] = (byte) 84;
      numArray3[49] = (byte) 76;
      numArray3[46] = (byte) 58;
      numArray3[44] = (byte) 169;
      numArray3[48 /*0x30*/] = (byte) 99;
      numArray3[54] = (byte) 132;
      numArray3[41] = (byte) 239;
      numArray3[51] = (byte) 82;
      numArray3[52] = (byte) 31 /*0x1F*/;
      numArray3[53] = (byte) 9;
      numArray3[11] = (byte) 3;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[10] = (byte) 241;
      numArray4[12] = (byte) 102;
      numArray4[2] = (byte) 12;
      numArray4[3] = (byte) 130;
      numArray4[51] = (byte) 178;
      numArray4[50] = (byte) 24;
      numArray4[52] = (byte) 227;
      numArray4[0] = (byte) 73;
      numArray4[48 /*0x30*/] = (byte) 182;
      numArray4[9] = (byte) 252;
      numArray4[38] = (byte) 136;
      numArray4[13] = (byte) 82;
      numArray4[5] = (byte) 211;
      numArray4[8] = (byte) 9;
      numArray4[14] = (byte) 111;
      numArray4[31 /*0x1F*/] = (byte) 181;
      numArray4[16 /*0x10*/] = (byte) 205;
      numArray4[41] = (byte) 117;
      numArray4[24] = (byte) 80 /*0x50*/;
      numArray4[19] = (byte) 4;
      numArray4[15] = (byte) 164;
      numArray4[21] = (byte) 84;
      numArray4[22] = (byte) 159;
      numArray4[23] = (byte) 132;
      numArray4[18] = (byte) 87;
      numArray4[17] = (byte) 78;
      numArray4[34] = (byte) 61;
      numArray4[27] = (byte) 92;
      numArray4[28] = (byte) 50;
      numArray4[29] = (byte) 216;
      numArray4[30] = (byte) 96 /*0x60*/;
      numArray4[20] = (byte) 43;
      numArray4[32 /*0x20*/] = (byte) 136;
      numArray4[26] = (byte) 8;
      numArray4[25] = (byte) 83;
      numArray4[35] = (byte) 122;
      numArray4[36] = (byte) 29;
      numArray4[37] = (byte) 108;
      numArray4[4] = (byte) 95;
      numArray4[33] = (byte) 41;
      numArray4[40] = (byte) 114;
      numArray4[6] = (byte) 100;
      numArray4[1] = (byte) 95;
      numArray4[53] = (byte) 113;
      numArray4[44] = (byte) 1;
      numArray4[45] = (byte) 161;
      numArray4[46] = (byte) 42;
      numArray4[11] = (byte) 10;
      numArray4[7] = (byte) 226;
      numArray4[49] = (byte) 51;
      numArray4[42] = (byte) 123;
      numArray4[39] = (byte) 112 /*0x70*/;
      numArray4[47] = (byte) 28;
      numArray4[43] = (byte) 22;
      numArray4[54] = (byte) 130;
      byte[] numArray5 = new byte[55]
      {
        (byte) 125,
        (byte) 34,
        (byte) 84,
        (byte) 153,
        (byte) 186,
        (byte) 45,
        (byte) 139,
        (byte) 0,
        (byte) 232,
        (byte) 205,
        (byte) 55,
        (byte) 125,
        (byte) 160 /*0xA0*/,
        (byte) 65,
        (byte) 29,
        (byte) 158,
        (byte) 104,
        (byte) 100,
        (byte) 115,
        (byte) 121,
        (byte) 141,
        (byte) 113,
        (byte) 22,
        (byte) 48 /*0x30*/,
        (byte) 42,
        (byte) 85,
        (byte) 99,
        (byte) 65,
        (byte) 173,
        (byte) 100,
        (byte) 103,
        (byte) 241,
        (byte) 112 /*0x70*/,
        (byte) 10,
        (byte) 119,
        (byte) 122,
        (byte) 157,
        (byte) 173,
        (byte) 138,
        (byte) 219,
        (byte) 43,
        (byte) 10,
        (byte) 79,
        (byte) 172,
        (byte) 204,
        (byte) 161,
        (byte) 167,
        (byte) 41,
        (byte) 89,
        (byte) 76,
        (byte) 226,
        (byte) 181,
        (byte) 98,
        (byte) 202,
        (byte) 185
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 5,
        (byte) 131,
        (byte) 226,
        (byte) 223,
        (byte) 97,
        (byte) 177,
        (byte) 112 /*0x70*/,
        (byte) 237,
        (byte) 106,
        (byte) 107,
        (byte) 148,
        (byte) 203,
        (byte) 59,
        (byte) 242,
        (byte) 38,
        (byte) 72,
        (byte) 66,
        (byte) 39,
        (byte) 240 /*0xF0*/,
        (byte) 142,
        (byte) 13,
        (byte) 189,
        (byte) 143,
        (byte) 59,
        (byte) 227,
        (byte) 229,
        (byte) 82,
        (byte) 8,
        (byte) 193,
        (byte) 213,
        (byte) 55,
        (byte) 219,
        (byte) 45,
        (byte) 129,
        (byte) 143,
        (byte) 183,
        (byte) 154,
        (byte) 197,
        (byte) 8,
        (byte) 19,
        (byte) 217,
        (byte) 141,
        (byte) 90,
        (byte) 179,
        (byte) 89,
        (byte) 60,
        (byte) 133,
        (byte) 186,
        (byte) 144 /*0x90*/,
        (byte) 164,
        (byte) 185,
        (byte) 183,
        (byte) 219,
        (byte) 140,
        (byte) 42
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 140,
        (byte) 112 /*0x70*/,
        (byte) 0,
        (byte) 57,
        (byte) 166,
        (byte) 115,
        (byte) 96 /*0x60*/,
        (byte) 67,
        (byte) 157,
        (byte) 246,
        (byte) 61,
        (byte) 57,
        (byte) 207,
        (byte) 193,
        (byte) 135,
        (byte) 191,
        (byte) 226,
        (byte) 30,
        (byte) 242,
        (byte) 45,
        (byte) 220,
        (byte) 68,
        (byte) 151,
        (byte) 146,
        (byte) 18,
        (byte) 160 /*0xA0*/,
        (byte) 110,
        (byte) 237,
        (byte) 13,
        (byte) 216,
        (byte) 34,
        (byte) 146,
        (byte) 244,
        (byte) 177,
        (byte) 154,
        (byte) 133,
        (byte) 89,
        (byte) 181,
        (byte) 127 /*0x7F*/,
        (byte) 236,
        (byte) 222,
        (byte) 224 /*0xE0*/,
        (byte) 184,
        (byte) 177,
        (byte) 117,
        (byte) 198,
        (byte) 132,
        (byte) 253,
        (byte) 133,
        (byte) 121,
        (byte) 94,
        (byte) 92,
        (byte) 87,
        (byte) 74,
        (byte) 178
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[12] = (byte) 111;
      numArray8[1] = (byte) 251;
      numArray8[38] = (byte) 113;
      numArray8[39] = (byte) 97;
      numArray8[7] = (byte) 52;
      numArray8[45] = (byte) 88;
      numArray8[0] = (byte) 167;
      numArray8[19] = (byte) 99;
      numArray8[21] = (byte) 225;
      numArray8[9] = (byte) 158;
      numArray8[30] = (byte) 160 /*0xA0*/;
      numArray8[11] = (byte) 98;
      numArray8[36] = (byte) 139;
      numArray8[6] = (byte) 148;
      numArray8[43] = (byte) 157;
      numArray8[15] = (byte) 31 /*0x1F*/;
      numArray8[2] = (byte) 124;
      numArray8[16 /*0x10*/] = (byte) 221;
      numArray8[18] = (byte) 128 /*0x80*/;
      numArray8[8] = (byte) 184;
      numArray8[20] = (byte) 220;
      numArray8[5] = (byte) 44;
      numArray8[22] = (byte) 167;
      numArray8[23] = (byte) 95;
      numArray8[27] = (byte) 28;
      numArray8[24] = (byte) 151;
      numArray8[26] = (byte) 28;
      numArray8[29] = (byte) 0;
      numArray8[17] = (byte) 181;
      numArray8[25] = (byte) 120;
      numArray8[41] = (byte) 127 /*0x7F*/;
      numArray8[31 /*0x1F*/] = (byte) 87;
      numArray8[32 /*0x20*/] = (byte) 72;
      numArray8[33] = (byte) 196;
      numArray8[34] = (byte) 242;
      numArray8[4] = (byte) 104;
      numArray8[14] = (byte) 125;
      numArray8[37] = (byte) 180;
      numArray8[51] = (byte) 215;
      numArray8[3] = (byte) 95;
      numArray8[10] = (byte) 185;
      numArray8[40] = (byte) 198;
      numArray8[35] = (byte) 201;
      numArray8[53] = (byte) 228;
      numArray8[44] = (byte) 204;
      numArray8[28] = (byte) 128 /*0x80*/;
      numArray8[46] = (byte) 33;
      numArray8[42] = (byte) 160 /*0xA0*/;
      numArray8[48 /*0x30*/] = (byte) 60;
      numArray8[49] = (byte) 151;
      numArray8[50] = (byte) 253;
      numArray8[47] = (byte) 247;
      numArray8[52] = (byte) 170;
      numArray8[13] = (byte) 173;
      numArray8[54] = (byte) 22;
      byte[] numArray9 = new byte[55]
      {
        (byte) 74,
        (byte) 38,
        (byte) 213,
        (byte) 57,
        (byte) 22,
        (byte) 192 /*0xC0*/,
        (byte) 145,
        (byte) 150,
        (byte) 111,
        (byte) 4,
        (byte) 105,
        (byte) 92,
        (byte) 185,
        (byte) 55,
        (byte) 103,
        (byte) 211,
        (byte) 198,
        (byte) 62,
        (byte) 206,
        (byte) 245,
        (byte) 67,
        (byte) 202,
        (byte) 85,
        (byte) 2,
        (byte) 17,
        (byte) 162,
        (byte) 170,
        (byte) 172,
        (byte) 228,
        (byte) 0,
        (byte) 24,
        (byte) 18,
        (byte) 233,
        (byte) 148,
        (byte) 21,
        (byte) 57,
        (byte) 154,
        (byte) 178,
        (byte) 4,
        (byte) 70,
        (byte) 95,
        (byte) 243,
        (byte) 106,
        (byte) 156,
        (byte) 116,
        (byte) 117,
        (byte) 223,
        (byte) 115,
        (byte) 103,
        (byte) 170,
        byte.MaxValue,
        (byte) 69,
        (byte) 103,
        (byte) 170,
        (byte) 214
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[18];
      numArray10[10] = (byte) 253;
      numArray10[1] = (byte) 136;
      numArray10[0] = (byte) 139;
      numArray10[16 /*0x10*/] = (byte) 205;
      numArray10[4] = (byte) 152;
      numArray10[6] = (byte) 181;
      numArray10[2] = (byte) 153;
      numArray10[7] = (byte) 53;
      numArray10[8] = (byte) 180;
      numArray10[17] = (byte) 78;
      numArray10[11] = (byte) 141;
      numArray10[3] = (byte) 233;
      numArray10[12] = (byte) 63 /*0x3F*/;
      numArray10[13] = (byte) 37;
      numArray10[9] = (byte) 181;
      numArray10[15] = (byte) 195;
      numArray10[14] = (byte) 60;
      numArray10[5] = (byte) 204;
      byte[] numArray11 = new byte[18];
      numArray11[11] = (byte) 199;
      numArray11[8] = (byte) 70;
      numArray11[13] = (byte) 131;
      numArray11[3] = (byte) 47;
      numArray11[15] = (byte) 206;
      numArray11[5] = (byte) 221;
      numArray11[7] = (byte) 180;
      numArray11[10] = (byte) 80 /*0x50*/;
      numArray11[4] = (byte) 78;
      numArray11[9] = (byte) 154;
      numArray11[0] = (byte) 146;
      numArray11[1] = (byte) 90;
      numArray11[12] = (byte) 146;
      numArray11[6] = (byte) 231;
      numArray11[14] = (byte) 20;
      numArray11[2] = (byte) 127 /*0x7F*/;
      numArray11[16 /*0x10*/] = (byte) 111;
      numArray11[17] = (byte) 43;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_12586.sspq, 314, (Array) numArray12, 0, 42);
      key.Query(true, 335, numArray12, response);
      Array.Copy((Array) sc_12586.sspr, 314, (Array) numArray12, 0, 42);
      for (int index = 0; index < numArray12.Length; ++index)
      {
        if ((int) numArray12[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray13 = new byte[238];
    byte[] numArray14 = new byte[55]
    {
      (byte) 202,
      (byte) 53,
      (byte) 155,
      (byte) 193,
      (byte) 242,
      (byte) 89,
      (byte) 95,
      (byte) 146,
      (byte) 3,
      (byte) 219,
      (byte) 241,
      (byte) 16 /*0x10*/,
      (byte) 80 /*0x50*/,
      (byte) 197,
      (byte) 49,
      (byte) 226,
      (byte) 243,
      (byte) 151,
      (byte) 46,
      (byte) 114,
      (byte) 59,
      (byte) 102,
      (byte) 197,
      (byte) 195,
      (byte) 251,
      (byte) 173,
      (byte) 251,
      (byte) 34,
      (byte) 125,
      (byte) 124,
      (byte) 151,
      (byte) 79,
      (byte) 134,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 217,
      (byte) 106,
      (byte) 211,
      (byte) 116,
      (byte) 10,
      (byte) 197,
      (byte) 59,
      (byte) 122,
      (byte) 114,
      (byte) 142,
      (byte) 204,
      (byte) 222,
      (byte) 164,
      (byte) 68,
      (byte) 52,
      (byte) 183,
      (byte) 211,
      (byte) 244,
      (byte) 126,
      (byte) 51
    };
    byte[] numArray15 = new byte[55];
    numArray15[28] = (byte) 122;
    numArray15[1] = (byte) 126;
    numArray15[49] = (byte) 137;
    numArray15[3] = (byte) 141;
    numArray15[52] = (byte) 195;
    numArray15[2] = (byte) 65;
    numArray15[21] = (byte) 126;
    numArray15[7] = (byte) 102;
    numArray15[12] = (byte) 164;
    numArray15[39] = (byte) 160 /*0xA0*/;
    numArray15[10] = (byte) 83;
    numArray15[11] = (byte) 25;
    numArray15[40] = (byte) 120;
    numArray15[35] = (byte) 14;
    numArray15[14] = (byte) 17;
    numArray15[15] = (byte) 177;
    numArray15[5] = (byte) 170;
    numArray15[17] = (byte) 92;
    numArray15[31 /*0x1F*/] = (byte) 78;
    numArray15[19] = (byte) 75;
    numArray15[13] = (byte) 246;
    numArray15[26] = (byte) 246;
    numArray15[6] = (byte) 50;
    numArray15[23] = (byte) 166;
    numArray15[45] = (byte) 79;
    numArray15[44] = (byte) 204;
    numArray15[53] = (byte) 52;
    numArray15[30] = (byte) 220;
    numArray15[9] = (byte) 250;
    numArray15[29] = (byte) 153;
    numArray15[8] = (byte) 198;
    numArray15[48 /*0x30*/] = (byte) 54;
    numArray15[32 /*0x20*/] = (byte) 131;
    numArray15[24] = (byte) 56;
    numArray15[34] = (byte) 108;
    numArray15[33] = (byte) 223;
    numArray15[36] = (byte) 170;
    numArray15[37] = (byte) 146;
    numArray15[38] = (byte) 227;
    numArray15[16 /*0x10*/] = (byte) 246;
    numArray15[42] = (byte) 5;
    numArray15[41] = (byte) 209;
    numArray15[22] = (byte) 121;
    numArray15[0] = (byte) 169;
    numArray15[20] = (byte) 90;
    numArray15[43] = (byte) 19;
    numArray15[46] = (byte) 161;
    numArray15[47] = (byte) 106;
    numArray15[25] = (byte) 173;
    numArray15[18] = (byte) 219;
    numArray15[50] = (byte) 120;
    numArray15[51] = (byte) 95;
    numArray15[27] = (byte) 131;
    numArray15[4] = (byte) 78;
    numArray15[54] = (byte) 254;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray13, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index] ^= numArray15[index];
    byte[] numArray16 = new byte[55]
    {
      (byte) 163,
      (byte) 150,
      (byte) 119,
      (byte) 6,
      (byte) 94,
      (byte) 204,
      (byte) 193,
      (byte) 127 /*0x7F*/,
      (byte) 71,
      (byte) 129,
      (byte) 147,
      (byte) 72,
      (byte) 238,
      (byte) 230,
      (byte) 18,
      (byte) 243,
      (byte) 118,
      (byte) 37,
      (byte) 217,
      (byte) 247,
      (byte) 37,
      (byte) 73,
      (byte) 237,
      (byte) 72,
      (byte) 8,
      (byte) 242,
      (byte) 140,
      (byte) 45,
      (byte) 19,
      (byte) 137,
      (byte) 55,
      (byte) 144 /*0x90*/,
      (byte) 147,
      (byte) 77,
      (byte) 238,
      (byte) 119,
      (byte) 184,
      (byte) 165,
      (byte) 41,
      (byte) 89,
      (byte) 50,
      (byte) 175,
      (byte) 163,
      (byte) 192 /*0xC0*/,
      (byte) 242,
      (byte) 6,
      (byte) 253,
      (byte) 213,
      (byte) 132,
      (byte) 190,
      (byte) 90,
      (byte) 171,
      (byte) 90,
      (byte) 129,
      (byte) 246
    };
    byte[] numArray17 = new byte[55];
    numArray17[6] = (byte) 218;
    numArray17[4] = (byte) 36;
    numArray17[14] = (byte) 249;
    numArray17[16 /*0x10*/] = (byte) 98;
    numArray17[30] = (byte) 19;
    numArray17[5] = (byte) 54;
    numArray17[0] = (byte) 59;
    numArray17[7] = (byte) 96 /*0x60*/;
    numArray17[8] = (byte) 144 /*0x90*/;
    numArray17[12] = (byte) 216;
    numArray17[10] = (byte) 45;
    numArray17[28] = (byte) 234;
    numArray17[9] = (byte) 78;
    numArray17[24] = (byte) 93;
    numArray17[1] = (byte) 70;
    numArray17[37] = (byte) 124;
    numArray17[15] = (byte) 81;
    numArray17[17] = (byte) 147;
    numArray17[18] = (byte) 105;
    numArray17[13] = (byte) 112 /*0x70*/;
    numArray17[20] = (byte) 218;
    numArray17[21] = (byte) 50;
    numArray17[33] = (byte) 124;
    numArray17[26] = (byte) 163;
    numArray17[11] = (byte) 6;
    numArray17[25] = (byte) 141;
    numArray17[31 /*0x1F*/] = (byte) 5;
    numArray17[27] = (byte) 0;
    numArray17[42] = (byte) 143;
    numArray17[2] = (byte) 130;
    numArray17[53] = (byte) 4;
    numArray17[46] = (byte) 97;
    numArray17[32 /*0x20*/] = (byte) 177;
    numArray17[47] = (byte) 185;
    numArray17[40] = (byte) 124;
    numArray17[29] = (byte) 70;
    numArray17[36] = (byte) 150;
    numArray17[3] = (byte) 36;
    numArray17[38] = (byte) 174;
    numArray17[39] = (byte) 233;
    numArray17[51] = (byte) 19;
    numArray17[41] = (byte) 38;
    numArray17[52] = (byte) 200;
    numArray17[43] = (byte) 170;
    numArray17[23] = (byte) 226;
    numArray17[45] = (byte) 5;
    numArray17[34] = (byte) 36;
    numArray17[19] = (byte) 185;
    numArray17[48 /*0x30*/] = (byte) 1;
    numArray17[49] = (byte) 51;
    numArray17[50] = (byte) 171;
    numArray17[44] = (byte) 233;
    numArray17[54] = (byte) 118;
    numArray17[22] = (byte) 164;
    numArray17[35] = (byte) 37;
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray13, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 55] ^= numArray17[index];
    byte[] numArray18 = new byte[55];
    numArray18[20] = (byte) 182;
    numArray18[5] = (byte) 22;
    numArray18[50] = (byte) 116;
    numArray18[52] = (byte) 77;
    numArray18[26] = (byte) 209;
    numArray18[22] = (byte) 175;
    numArray18[6] = (byte) 143;
    numArray18[7] = (byte) 189;
    numArray18[2] = (byte) 118;
    numArray18[23] = (byte) 153;
    numArray18[40] = (byte) 86;
    numArray18[34] = (byte) 176 /*0xB0*/;
    numArray18[12] = (byte) 205;
    numArray18[53] = (byte) 244;
    numArray18[14] = (byte) 42;
    numArray18[21] = (byte) 101;
    numArray18[16 /*0x10*/] = (byte) 102;
    numArray18[17] = (byte) 39;
    numArray18[15] = (byte) 61;
    numArray18[8] = (byte) 204;
    numArray18[28] = (byte) 219;
    numArray18[43] = (byte) 174;
    numArray18[33] = (byte) 82;
    numArray18[37] = (byte) 143;
    numArray18[36] = (byte) 71;
    numArray18[25] = (byte) 191;
    numArray18[1] = (byte) 253;
    numArray18[27] = (byte) 166;
    numArray18[30] = (byte) 86;
    numArray18[38] = (byte) 179;
    numArray18[3] = (byte) 138;
    numArray18[31 /*0x1F*/] = (byte) 214;
    numArray18[32 /*0x20*/] = (byte) 223;
    numArray18[0] = (byte) 216;
    numArray18[13] = (byte) 177;
    numArray18[51] = (byte) 148;
    numArray18[10] = (byte) 74;
    numArray18[29] = (byte) 91;
    numArray18[11] = (byte) 100;
    numArray18[39] = (byte) 44;
    numArray18[4] = (byte) 234;
    numArray18[41] = (byte) 184;
    numArray18[42] = (byte) 35;
    numArray18[24] = (byte) 216;
    numArray18[44] = (byte) 17;
    numArray18[45] = (byte) 121;
    numArray18[46] = (byte) 28;
    numArray18[18] = (byte) 50;
    numArray18[48 /*0x30*/] = (byte) 94;
    numArray18[49] = (byte) 115;
    numArray18[47] = (byte) 209;
    numArray18[19] = (byte) 191;
    numArray18[9] = (byte) 162;
    numArray18[35] = (byte) 169;
    numArray18[54] = (byte) 197;
    byte[] numArray19 = new byte[55]
    {
      (byte) 249,
      (byte) 168,
      (byte) 43,
      (byte) 120,
      (byte) 95,
      (byte) 177,
      (byte) 210,
      (byte) 41,
      (byte) 162,
      (byte) 100,
      (byte) 199,
      (byte) 129,
      (byte) 76,
      (byte) 203,
      (byte) 12,
      (byte) 166,
      (byte) 15,
      (byte) 144 /*0x90*/,
      (byte) 194,
      (byte) 137,
      (byte) 121,
      (byte) 88,
      (byte) 197,
      (byte) 25,
      (byte) 58,
      (byte) 82,
      (byte) 14,
      (byte) 214,
      (byte) 75,
      (byte) 183,
      (byte) 69,
      (byte) 33,
      (byte) 107,
      byte.MaxValue,
      (byte) 33,
      (byte) 250,
      (byte) 214,
      (byte) 215,
      (byte) 151,
      (byte) 61,
      (byte) 101,
      (byte) 86,
      (byte) 150,
      (byte) 254,
      (byte) 149,
      (byte) 29,
      (byte) 253,
      (byte) 96 /*0x60*/,
      (byte) 82,
      (byte) 2,
      (byte) 23,
      (byte) 77,
      (byte) 98,
      (byte) 200,
      (byte) 167
    };
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray13, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 110] ^= numArray19[index];
    byte[] numArray20 = new byte[55]
    {
      (byte) 84,
      (byte) 82,
      (byte) 136,
      (byte) 68,
      (byte) 160 /*0xA0*/,
      (byte) 122,
      (byte) 137,
      (byte) 137,
      (byte) 235,
      (byte) 154,
      (byte) 238,
      (byte) 196,
      (byte) 38,
      (byte) 138,
      (byte) 249,
      (byte) 235,
      (byte) 30,
      (byte) 251,
      (byte) 39,
      (byte) 142,
      (byte) 159,
      (byte) 234,
      (byte) 107,
      (byte) 1,
      (byte) 47,
      (byte) 107,
      (byte) 233,
      (byte) 73,
      (byte) 161,
      (byte) 64 /*0x40*/,
      (byte) 94,
      (byte) 196,
      (byte) 10,
      (byte) 15,
      (byte) 22,
      (byte) 138,
      byte.MaxValue,
      (byte) 162,
      (byte) 205,
      (byte) 91,
      (byte) 41,
      (byte) 216,
      (byte) 178,
      (byte) 89,
      (byte) 179,
      (byte) 12,
      (byte) 182,
      (byte) 243,
      (byte) 163,
      (byte) 53,
      (byte) 40,
      (byte) 144 /*0x90*/,
      (byte) 160 /*0xA0*/,
      (byte) 164,
      (byte) 173
    };
    byte[] numArray21 = new byte[55];
    numArray21[8] = (byte) 201;
    numArray21[46] = (byte) 106;
    numArray21[16 /*0x10*/] = (byte) 3;
    numArray21[20] = (byte) 61;
    numArray21[24] = (byte) 8;
    numArray21[18] = (byte) 190;
    numArray21[15] = (byte) 55;
    numArray21[13] = (byte) 76;
    numArray21[34] = (byte) 171;
    numArray21[31 /*0x1F*/] = (byte) 135;
    numArray21[10] = (byte) 61;
    numArray21[52] = (byte) 172;
    numArray21[12] = (byte) 93;
    numArray21[33] = (byte) 1;
    numArray21[43] = (byte) 78;
    numArray21[54] = (byte) 17;
    numArray21[9] = (byte) 40;
    numArray21[17] = (byte) 3;
    numArray21[4] = (byte) 208 /*0xD0*/;
    numArray21[19] = (byte) 184;
    numArray21[42] = (byte) 98;
    numArray21[21] = (byte) 245;
    numArray21[22] = (byte) 212;
    numArray21[23] = (byte) 29;
    numArray21[11] = (byte) 48 /*0x30*/;
    numArray21[2] = (byte) 238;
    numArray21[26] = (byte) 213;
    numArray21[27] = (byte) 112 /*0x70*/;
    numArray21[28] = (byte) 166;
    numArray21[6] = (byte) 37;
    numArray21[30] = (byte) 7;
    numArray21[7] = (byte) 251;
    numArray21[3] = (byte) 214;
    numArray21[50] = (byte) 139;
    numArray21[36] = (byte) 50;
    numArray21[35] = (byte) 189;
    numArray21[45] = (byte) 254;
    numArray21[37] = (byte) 232;
    numArray21[38] = (byte) 180;
    numArray21[41] = (byte) 57;
    numArray21[39] = (byte) 10;
    numArray21[29] = (byte) 17;
    numArray21[1] = (byte) 236;
    numArray21[5] = (byte) 189;
    numArray21[53] = (byte) 86;
    numArray21[49] = (byte) 118;
    numArray21[48 /*0x30*/] = (byte) 112 /*0x70*/;
    numArray21[47] = (byte) 40;
    numArray21[40] = (byte) 60;
    numArray21[0] = (byte) 97;
    numArray21[25] = (byte) 236;
    numArray21[51] = (byte) 63 /*0x3F*/;
    numArray21[44] = (byte) 89;
    numArray21[14] = (byte) 195;
    numArray21[32 /*0x20*/] = (byte) 208 /*0xD0*/;
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray13, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray13[index + 165] ^= numArray21[index];
    byte[] numArray22 = new byte[18]
    {
      (byte) 57,
      (byte) 241,
      (byte) 127 /*0x7F*/,
      (byte) 154,
      (byte) 154,
      (byte) 123,
      (byte) 146,
      (byte) 108,
      (byte) 6,
      (byte) 83,
      (byte) 180,
      (byte) 199,
      (byte) 202,
      (byte) 116,
      (byte) 238,
      (byte) 148,
      (byte) 1,
      (byte) 123
    };
    byte[] numArray23 = new byte[18]
    {
      (byte) 82,
      (byte) 61,
      (byte) 70,
      (byte) 97,
      (byte) 218,
      (byte) 213,
      (byte) 89,
      (byte) 187,
      (byte) 179,
      (byte) 107,
      (byte) 101,
      (byte) 78,
      (byte) 233,
      (byte) 165,
      (byte) 192 /*0xC0*/,
      (byte) 162,
      (byte) 163,
      (byte) 72
    };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray13, 220, 18);
    for (int index = 0; index < 18; ++index)
      numArray13[index + 220] ^= numArray23[index];
    return Encoding.UTF8.GetString(numArray13);
  }

  internal static string ssp_appserver_12618()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[5] = (byte) 107;
      numArray2[3] = (byte) 113;
      numArray2[2] = (byte) 119;
      numArray2[8] = (byte) 39;
      numArray2[1] = (byte) 21;
      numArray2[4] = (byte) 35;
      numArray2[0] = (byte) 208 /*0xD0*/;
      numArray2[7] = (byte) 80 /*0x50*/;
      numArray2[6] = (byte) 108;
      byte[] numArray3 = new byte[9]
      {
        (byte) 32 /*0x20*/,
        (byte) 60,
        (byte) 116,
        (byte) 34,
        (byte) 248,
        (byte) 63 /*0x3F*/,
        (byte) 247,
        (byte) 145,
        (byte) 156
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[2] = (byte) 183;
    numArray5[1] = (byte) 235;
    numArray5[8] = (byte) 195;
    numArray5[0] = byte.MaxValue;
    numArray5[4] = (byte) 212;
    numArray5[5] = (byte) 190;
    numArray5[6] = (byte) 205;
    numArray5[7] = (byte) 210;
    numArray5[3] = (byte) 106;
    byte[] numArray6 = new byte[9]
    {
      (byte) 145,
      (byte) 235,
      (byte) 212,
      (byte) 246,
      (byte) 30,
      (byte) 50,
      (byte) 224 /*0xE0*/,
      (byte) 150,
      (byte) 205
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[39];
    byte[] response = new byte[39];
    Array.Copy((Array) sc_12586.sspq, 356, (Array) numArray7, 0, 39);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12586.sspr, 356, (Array) numArray7, 0, 39);
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

  internal static string ssp_appserver_12619()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[100];
      byte[] numArray2 = new byte[55]
      {
        (byte) 1,
        (byte) 151,
        (byte) 173,
        (byte) 76,
        (byte) 156,
        (byte) 49,
        (byte) 80 /*0x50*/,
        (byte) 64 /*0x40*/,
        (byte) 99,
        (byte) 34,
        (byte) 9,
        (byte) 122,
        (byte) 97,
        (byte) 56,
        (byte) 199,
        (byte) 205,
        (byte) 217,
        (byte) 12,
        (byte) 106,
        (byte) 215,
        (byte) 204,
        (byte) 195,
        (byte) 6,
        (byte) 69,
        (byte) 104,
        (byte) 147,
        (byte) 118,
        (byte) 252,
        (byte) 120,
        (byte) 85,
        (byte) 152,
        (byte) 175,
        (byte) 65,
        (byte) 77,
        (byte) 123,
        (byte) 204,
        (byte) 189,
        (byte) 40,
        (byte) 82,
        (byte) 153,
        (byte) 42,
        (byte) 111,
        (byte) 169,
        (byte) 3,
        (byte) 19,
        (byte) 213,
        (byte) 78,
        (byte) 148,
        (byte) 12,
        (byte) 228,
        (byte) 144 /*0x90*/,
        (byte) 98,
        (byte) 53,
        (byte) 147,
        (byte) 133
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 125,
        (byte) 159,
        (byte) 235,
        (byte) 113,
        (byte) 163,
        (byte) 227,
        (byte) 115,
        (byte) 218,
        (byte) 73,
        (byte) 221,
        (byte) 226,
        (byte) 61,
        (byte) 212,
        (byte) 162,
        (byte) 246,
        (byte) 27,
        (byte) 38,
        (byte) 210,
        (byte) 52,
        (byte) 170,
        (byte) 166,
        (byte) 67,
        (byte) 116,
        (byte) 199,
        (byte) 127 /*0x7F*/,
        (byte) 127 /*0x7F*/,
        (byte) 243,
        (byte) 80 /*0x50*/,
        (byte) 138,
        (byte) 228,
        (byte) 56,
        (byte) 80 /*0x50*/,
        (byte) 59,
        (byte) 173,
        (byte) 66,
        (byte) 46,
        (byte) 246,
        (byte) 41,
        (byte) 25,
        (byte) 243,
        (byte) 45,
        (byte) 154,
        (byte) 119,
        (byte) 68,
        (byte) 41,
        (byte) 66,
        (byte) 150,
        (byte) 192 /*0xC0*/,
        (byte) 198,
        (byte) 70,
        (byte) 134,
        (byte) 199,
        (byte) 199,
        (byte) 43,
        (byte) 76
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45]
      {
        (byte) 104,
        (byte) 228,
        (byte) 8,
        (byte) 187,
        (byte) 83,
        (byte) 199,
        (byte) 13,
        (byte) 138,
        (byte) 89,
        (byte) 57,
        (byte) 20,
        (byte) 188,
        (byte) 251,
        (byte) 15,
        (byte) 176 /*0xB0*/,
        (byte) 185,
        (byte) 208 /*0xD0*/,
        (byte) 172,
        (byte) 93,
        (byte) 25,
        (byte) 3,
        (byte) 50,
        (byte) 146,
        (byte) 69,
        (byte) 225,
        (byte) 240 /*0xF0*/,
        (byte) 147,
        (byte) 197,
        (byte) 54,
        (byte) 122,
        (byte) 173,
        (byte) 199,
        (byte) 61,
        (byte) 60,
        (byte) 85,
        (byte) 68,
        (byte) 74,
        (byte) 222,
        (byte) 215,
        (byte) 167,
        (byte) 155,
        (byte) 121,
        (byte) 185,
        (byte) 156,
        (byte) 58
      };
      byte[] numArray5 = new byte[45];
      numArray5[12] = (byte) 30;
      numArray5[29] = (byte) 215;
      numArray5[39] = (byte) 216;
      numArray5[3] = (byte) 85;
      numArray5[4] = (byte) 60;
      numArray5[5] = (byte) 246;
      numArray5[17] = (byte) 3;
      numArray5[7] = (byte) 148;
      numArray5[16 /*0x10*/] = (byte) 35;
      numArray5[40] = (byte) 130;
      numArray5[22] = (byte) 76;
      numArray5[28] = (byte) 228;
      numArray5[21] = (byte) 64 /*0x40*/;
      numArray5[9] = (byte) 32 /*0x20*/;
      numArray5[14] = byte.MaxValue;
      numArray5[41] = (byte) 23;
      numArray5[24] = (byte) 150;
      numArray5[10] = (byte) 175;
      numArray5[18] = (byte) 82;
      numArray5[19] = (byte) 63 /*0x3F*/;
      numArray5[11] = (byte) 250;
      numArray5[6] = (byte) 215;
      numArray5[26] = (byte) 234;
      numArray5[8] = (byte) 78;
      numArray5[2] = (byte) 99;
      numArray5[25] = (byte) 81;
      numArray5[42] = (byte) 198;
      numArray5[27] = (byte) 228;
      numArray5[13] = (byte) 126;
      numArray5[43] = (byte) 103;
      numArray5[30] = (byte) 165;
      numArray5[31 /*0x1F*/] = (byte) 198;
      numArray5[0] = (byte) 252;
      numArray5[33] = (byte) 186;
      numArray5[34] = (byte) 1;
      numArray5[35] = (byte) 239;
      numArray5[36] = (byte) 145;
      numArray5[37] = (byte) 51;
      numArray5[38] = (byte) 178;
      numArray5[1] = (byte) 66;
      numArray5[23] = (byte) 16 /*0x10*/;
      numArray5[15] = (byte) 94;
      numArray5[32 /*0x20*/] = (byte) 237;
      numArray5[20] = (byte) 16 /*0x10*/;
      numArray5[44] = (byte) 159;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[21];
      byte[] response = new byte[21];
      Array.Copy((Array) sc_12586.sspq, 395, (Array) numArray6, 0, 21);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12586.sspr, 395, (Array) numArray6, 0, 21);
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
    byte[] numArray7 = new byte[100];
    byte[] numArray8 = new byte[55];
    numArray8[6] = (byte) 103;
    numArray8[38] = (byte) 74;
    numArray8[2] = (byte) 198;
    numArray8[3] = (byte) 201;
    numArray8[22] = (byte) 115;
    numArray8[8] = (byte) 154;
    numArray8[35] = (byte) 242;
    numArray8[7] = (byte) 15;
    numArray8[9] = (byte) 121;
    numArray8[50] = (byte) 32 /*0x20*/;
    numArray8[10] = (byte) 232;
    numArray8[11] = (byte) 237;
    numArray8[34] = (byte) 251;
    numArray8[13] = (byte) 67;
    numArray8[14] = (byte) 41;
    numArray8[54] = (byte) 244;
    numArray8[51] = (byte) 116;
    numArray8[17] = (byte) 230;
    numArray8[18] = (byte) 29;
    numArray8[19] = (byte) 176 /*0xB0*/;
    numArray8[42] = (byte) 129;
    numArray8[32 /*0x20*/] = (byte) 53;
    numArray8[26] = (byte) 44;
    numArray8[23] = (byte) 85;
    numArray8[44] = (byte) 80 /*0x50*/;
    numArray8[53] = (byte) 31 /*0x1F*/;
    numArray8[1] = (byte) 97;
    numArray8[25] = (byte) 180;
    numArray8[28] = (byte) 171;
    numArray8[29] = (byte) 97;
    numArray8[30] = (byte) 151;
    numArray8[16 /*0x10*/] = (byte) 157;
    numArray8[15] = (byte) 100;
    numArray8[24] = (byte) 164;
    numArray8[43] = (byte) 249;
    numArray8[33] = (byte) 242;
    numArray8[36] = (byte) 95;
    numArray8[37] = (byte) 17;
    numArray8[0] = (byte) 119;
    numArray8[47] = (byte) 46;
    numArray8[40] = (byte) 241;
    numArray8[45] = (byte) 132;
    numArray8[27] = (byte) 112 /*0x70*/;
    numArray8[41] = (byte) 65;
    numArray8[31 /*0x1F*/] = (byte) 207;
    numArray8[20] = (byte) 142;
    numArray8[46] = (byte) 177;
    numArray8[21] = (byte) 161;
    numArray8[48 /*0x30*/] = (byte) 125;
    numArray8[49] = (byte) 158;
    numArray8[52] = (byte) 154;
    numArray8[12] = (byte) 96 /*0x60*/;
    numArray8[5] = (byte) 174;
    numArray8[39] = (byte) 63 /*0x3F*/;
    numArray8[4] = (byte) 197;
    byte[] numArray9 = new byte[55]
    {
      (byte) 239,
      (byte) 188,
      (byte) 20,
      (byte) 166,
      (byte) 104,
      (byte) 34,
      (byte) 201,
      (byte) 49,
      (byte) 85,
      (byte) 48 /*0x30*/,
      (byte) 109,
      (byte) 218,
      (byte) 28,
      (byte) 98,
      (byte) 47,
      (byte) 16 /*0x10*/,
      (byte) 68,
      (byte) 115,
      (byte) 165,
      (byte) 150,
      (byte) 91,
      (byte) 239,
      (byte) 163,
      (byte) 77,
      (byte) 15,
      (byte) 0,
      (byte) 180,
      (byte) 252,
      (byte) 123,
      (byte) 32 /*0x20*/,
      (byte) 149,
      (byte) 191,
      (byte) 240 /*0xF0*/,
      (byte) 246,
      (byte) 112 /*0x70*/,
      (byte) 84,
      (byte) 17,
      (byte) 229,
      (byte) 213,
      (byte) 2,
      (byte) 162,
      (byte) 160 /*0xA0*/,
      (byte) 142,
      (byte) 8,
      (byte) 51,
      (byte) 77,
      (byte) 123,
      (byte) 10,
      (byte) 178,
      (byte) 5,
      (byte) 208 /*0xD0*/,
      (byte) 19,
      (byte) 33,
      (byte) 82,
      (byte) 172
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[45];
    numArray10[26] = (byte) 115;
    numArray10[1] = (byte) 5;
    numArray10[8] = (byte) 228;
    numArray10[43] = (byte) 41;
    numArray10[4] = (byte) 4;
    numArray10[12] = (byte) 52;
    numArray10[6] = (byte) 248;
    numArray10[13] = (byte) 23;
    numArray10[2] = (byte) 14;
    numArray10[11] = (byte) 34;
    numArray10[0] = (byte) 108;
    numArray10[20] = (byte) 96 /*0x60*/;
    numArray10[10] = (byte) 110;
    numArray10[18] = (byte) 110;
    numArray10[34] = (byte) 129;
    numArray10[19] = (byte) 214;
    numArray10[5] = (byte) 86;
    numArray10[17] = (byte) 195;
    numArray10[38] = (byte) 247;
    numArray10[25] = (byte) 172;
    numArray10[33] = (byte) 141;
    numArray10[21] = (byte) 204;
    numArray10[22] = (byte) 83;
    numArray10[23] = (byte) 190;
    numArray10[24] = (byte) 23;
    numArray10[44] = (byte) 213;
    numArray10[35] = (byte) 230;
    numArray10[27] = (byte) 172;
    numArray10[7] = (byte) 190;
    numArray10[16 /*0x10*/] = (byte) 32 /*0x20*/;
    numArray10[30] = (byte) 181;
    numArray10[31 /*0x1F*/] = (byte) 87;
    numArray10[32 /*0x20*/] = (byte) 111;
    numArray10[3] = (byte) 131;
    numArray10[41] = (byte) 90;
    numArray10[9] = (byte) 162;
    numArray10[36] = (byte) 42;
    numArray10[37] = (byte) 58;
    numArray10[28] = (byte) 74;
    numArray10[39] = (byte) 134;
    numArray10[40] = (byte) 80 /*0x50*/;
    numArray10[14] = (byte) 160 /*0xA0*/;
    numArray10[42] = (byte) 83;
    numArray10[29] = (byte) 120;
    numArray10[15] = (byte) 98;
    byte[] numArray11 = new byte[45]
    {
      (byte) 127 /*0x7F*/,
      (byte) 3,
      (byte) 168,
      (byte) 13,
      (byte) 5,
      (byte) 56,
      (byte) 22,
      (byte) 177,
      (byte) 220,
      (byte) 25,
      (byte) 141,
      (byte) 164,
      (byte) 126,
      (byte) 104,
      (byte) 201,
      (byte) 120,
      (byte) 228,
      (byte) 106,
      (byte) 37,
      (byte) 179,
      (byte) 145,
      (byte) 225,
      (byte) 10,
      (byte) 138,
      (byte) 3,
      (byte) 167,
      (byte) 250,
      (byte) 157,
      (byte) 163,
      (byte) 236,
      (byte) 143,
      (byte) 107,
      (byte) 61,
      (byte) 223,
      (byte) 138,
      (byte) 143,
      (byte) 106,
      (byte) 41,
      (byte) 3,
      (byte) 46,
      (byte) 226,
      (byte) 59,
      (byte) 20,
      (byte) 203,
      (byte) 75
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 45);
    for (int index = 0; index < 45; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12620()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[103];
      byte[] numArray2 = new byte[55];
      numArray2[6] = (byte) 16 /*0x10*/;
      numArray2[4] = (byte) 60;
      numArray2[29] = (byte) 179;
      numArray2[20] = (byte) 190;
      numArray2[52] = (byte) 116;
      numArray2[44] = (byte) 151;
      numArray2[1] = (byte) 5;
      numArray2[7] = (byte) 107;
      numArray2[8] = (byte) 236;
      numArray2[47] = (byte) 205;
      numArray2[10] = (byte) 61;
      numArray2[51] = (byte) 170;
      numArray2[12] = (byte) 208 /*0xD0*/;
      numArray2[42] = (byte) 5;
      numArray2[21] = (byte) 241;
      numArray2[28] = (byte) 45;
      numArray2[16 /*0x10*/] = (byte) 38;
      numArray2[22] = (byte) 145;
      numArray2[18] = (byte) 102;
      numArray2[50] = (byte) 186;
      numArray2[14] = (byte) 89;
      numArray2[33] = (byte) 45;
      numArray2[34] = (byte) 68;
      numArray2[15] = (byte) 14;
      numArray2[39] = (byte) 42;
      numArray2[35] = (byte) 128 /*0x80*/;
      numArray2[26] = (byte) 249;
      numArray2[27] = (byte) 34;
      numArray2[38] = (byte) 236;
      numArray2[49] = (byte) 106;
      numArray2[30] = (byte) 134;
      numArray2[31 /*0x1F*/] = (byte) 93;
      numArray2[19] = (byte) 23;
      numArray2[13] = (byte) 80 /*0x50*/;
      numArray2[32 /*0x20*/] = (byte) 22;
      numArray2[25] = (byte) 212;
      numArray2[36] = (byte) 31 /*0x1F*/;
      numArray2[37] = (byte) 144 /*0x90*/;
      numArray2[45] = (byte) 39;
      numArray2[24] = (byte) 141;
      numArray2[40] = (byte) 67;
      numArray2[41] = (byte) 46;
      numArray2[17] = (byte) 137;
      numArray2[43] = (byte) 127 /*0x7F*/;
      numArray2[9] = (byte) 57;
      numArray2[48 /*0x30*/] = (byte) 55;
      numArray2[46] = (byte) 71;
      numArray2[53] = (byte) 184;
      numArray2[3] = (byte) 232;
      numArray2[5] = (byte) 190;
      numArray2[11] = (byte) 181;
      numArray2[2] = (byte) 230;
      numArray2[0] = (byte) 163;
      numArray2[23] = (byte) 159;
      numArray2[54] = (byte) 224 /*0xE0*/;
      byte[] numArray3 = new byte[55];
      numArray3[20] = (byte) 244;
      numArray3[1] = (byte) 157;
      numArray3[2] = (byte) 104;
      numArray3[4] = (byte) 61;
      numArray3[45] = (byte) 68;
      numArray3[5] = (byte) 91;
      numArray3[50] = (byte) 32 /*0x20*/;
      numArray3[7] = (byte) 179;
      numArray3[8] = (byte) 177;
      numArray3[30] = (byte) 202;
      numArray3[34] = (byte) 205;
      numArray3[11] = (byte) 219;
      numArray3[12] = (byte) 140;
      numArray3[13] = (byte) 252;
      numArray3[14] = (byte) 175;
      numArray3[38] = (byte) 117;
      numArray3[19] = (byte) 12;
      numArray3[35] = (byte) 182;
      numArray3[42] = (byte) 144 /*0x90*/;
      numArray3[37] = (byte) 11;
      numArray3[0] = (byte) 107;
      numArray3[43] = (byte) 206;
      numArray3[54] = (byte) 67;
      numArray3[52] = (byte) 245;
      numArray3[24] = (byte) 121;
      numArray3[21] = (byte) 207;
      numArray3[48 /*0x30*/] = (byte) 60;
      numArray3[16 /*0x10*/] = (byte) 123;
      numArray3[10] = (byte) 205;
      numArray3[29] = (byte) 103;
      numArray3[28] = (byte) 251;
      numArray3[31 /*0x1F*/] = (byte) 57;
      numArray3[23] = (byte) 58;
      numArray3[32 /*0x20*/] = (byte) 212;
      numArray3[6] = (byte) 3;
      numArray3[41] = (byte) 115;
      numArray3[26] = (byte) 247;
      numArray3[39] = (byte) 42;
      numArray3[27] = (byte) 227;
      numArray3[25] = (byte) 65;
      numArray3[40] = (byte) 151;
      numArray3[36] = (byte) 117;
      numArray3[9] = (byte) 77;
      numArray3[49] = (byte) 183;
      numArray3[44] = (byte) 55;
      numArray3[22] = (byte) 100;
      numArray3[46] = (byte) 226;
      numArray3[47] = (byte) 7;
      numArray3[17] = (byte) 189;
      numArray3[3] = (byte) 120;
      numArray3[33] = (byte) 167;
      numArray3[51] = (byte) 251;
      numArray3[18] = (byte) 48 /*0x30*/;
      numArray3[53] = (byte) 7;
      numArray3[15] = (byte) 228;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/]
      {
        (byte) 237,
        (byte) 23,
        (byte) 241,
        (byte) 46,
        (byte) 116,
        (byte) 52,
        (byte) 107,
        (byte) 7,
        (byte) 72,
        (byte) 200,
        (byte) 91,
        (byte) 177,
        (byte) 33,
        (byte) 125,
        (byte) 32 /*0x20*/,
        (byte) 109,
        (byte) 11,
        (byte) 41,
        (byte) 44,
        (byte) 145,
        (byte) 80 /*0x50*/,
        (byte) 147,
        (byte) 150,
        (byte) 155,
        (byte) 38,
        (byte) 251,
        (byte) 225,
        (byte) 16 /*0x10*/,
        (byte) 218,
        (byte) 77,
        (byte) 16 /*0x10*/,
        (byte) 74,
        (byte) 33,
        (byte) 142,
        (byte) 171,
        (byte) 41,
        (byte) 184,
        (byte) 157,
        (byte) 224 /*0xE0*/,
        (byte) 63 /*0x3F*/,
        (byte) 34,
        (byte) 237,
        (byte) 117,
        (byte) 125,
        (byte) 241,
        (byte) 147,
        (byte) 111,
        (byte) 231
      };
      byte[] numArray5 = new byte[48 /*0x30*/]
      {
        (byte) 114,
        (byte) 229,
        (byte) 210,
        (byte) 222,
        (byte) 92,
        (byte) 206,
        (byte) 181,
        (byte) 201,
        (byte) 61,
        (byte) 208 /*0xD0*/,
        (byte) 165,
        (byte) 98,
        (byte) 157,
        (byte) 112 /*0x70*/,
        (byte) 182,
        (byte) 49,
        (byte) 227,
        (byte) 249,
        (byte) 148,
        (byte) 124,
        (byte) 24,
        (byte) 6,
        (byte) 158,
        (byte) 41,
        (byte) 237,
        (byte) 70,
        (byte) 111,
        (byte) 139,
        (byte) 157,
        (byte) 215,
        (byte) 29,
        (byte) 62,
        (byte) 72,
        (byte) 146,
        (byte) 231,
        (byte) 82,
        (byte) 65,
        (byte) 174,
        (byte) 58,
        (byte) 126,
        (byte) 3,
        (byte) 146,
        (byte) 76,
        (byte) 62,
        (byte) 225,
        (byte) 240 /*0xF0*/,
        (byte) 109,
        (byte) 3
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[103];
    byte[] numArray7 = new byte[55];
    numArray7[9] = (byte) 83;
    numArray7[19] = (byte) 180;
    numArray7[46] = (byte) 122;
    numArray7[3] = (byte) 204;
    numArray7[4] = (byte) 90;
    numArray7[25] = (byte) 29;
    numArray7[52] = (byte) 172;
    numArray7[1] = (byte) 132;
    numArray7[15] = (byte) 143;
    numArray7[26] = (byte) 24;
    numArray7[48 /*0x30*/] = (byte) 224 /*0xE0*/;
    numArray7[11] = (byte) 45;
    numArray7[45] = (byte) 192 /*0xC0*/;
    numArray7[43] = (byte) 236;
    numArray7[10] = (byte) 90;
    numArray7[40] = (byte) 99;
    numArray7[16 /*0x10*/] = (byte) 147;
    numArray7[17] = (byte) 210;
    numArray7[51] = (byte) 21;
    numArray7[39] = (byte) 168;
    numArray7[7] = (byte) 7;
    numArray7[20] = (byte) 110;
    numArray7[22] = (byte) 237;
    numArray7[23] = (byte) 111;
    numArray7[24] = (byte) 98;
    numArray7[21] = (byte) 72;
    numArray7[13] = (byte) 184;
    numArray7[27] = (byte) 170;
    numArray7[28] = (byte) 146;
    numArray7[54] = (byte) 232;
    numArray7[30] = (byte) 249;
    numArray7[14] = (byte) 16 /*0x10*/;
    numArray7[32 /*0x20*/] = (byte) 83;
    numArray7[12] = (byte) 251;
    numArray7[29] = (byte) 140;
    numArray7[35] = (byte) 198;
    numArray7[37] = (byte) 193;
    numArray7[31 /*0x1F*/] = (byte) 109;
    numArray7[38] = (byte) 80 /*0x50*/;
    numArray7[36] = (byte) 10;
    numArray7[49] = (byte) 4;
    numArray7[41] = (byte) 149;
    numArray7[42] = (byte) 104;
    numArray7[33] = (byte) 246;
    numArray7[5] = (byte) 128 /*0x80*/;
    numArray7[44] = (byte) 199;
    numArray7[6] = (byte) 85;
    numArray7[47] = (byte) 197;
    numArray7[34] = (byte) 210;
    numArray7[53] = (byte) 210;
    numArray7[8] = (byte) 120;
    numArray7[2] = (byte) 23;
    numArray7[0] = (byte) 62;
    numArray7[50] = (byte) 220;
    numArray7[18] = (byte) 246;
    byte[] numArray8 = new byte[55]
    {
      (byte) 110,
      (byte) 166,
      (byte) 182,
      (byte) 231,
      (byte) 223,
      (byte) 190,
      (byte) 254,
      (byte) 253,
      (byte) 101,
      (byte) 32 /*0x20*/,
      (byte) 183,
      (byte) 181,
      (byte) 98,
      (byte) 17,
      (byte) 24,
      (byte) 253,
      (byte) 189,
      (byte) 232,
      (byte) 130,
      (byte) 217,
      (byte) 189,
      (byte) 192 /*0xC0*/,
      (byte) 112 /*0x70*/,
      (byte) 89,
      (byte) 99,
      (byte) 69,
      (byte) 186,
      (byte) 75,
      (byte) 29,
      (byte) 33,
      (byte) 5,
      (byte) 195,
      (byte) 140,
      (byte) 236,
      (byte) 27,
      (byte) 7,
      (byte) 242,
      (byte) 80 /*0x50*/,
      (byte) 138,
      (byte) 172,
      (byte) 249,
      (byte) 152,
      (byte) 203,
      (byte) 37,
      (byte) 67,
      (byte) 20,
      (byte) 133,
      (byte) 245,
      (byte) 122,
      (byte) 245,
      (byte) 188,
      (byte) 68,
      (byte) 43,
      (byte) 146,
      (byte) 60
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[48 /*0x30*/];
    numArray9[31 /*0x1F*/] = (byte) 253;
    numArray9[5] = (byte) 230;
    numArray9[39] = (byte) 58;
    numArray9[3] = (byte) 20;
    numArray9[0] = (byte) 0;
    numArray9[4] = (byte) 224 /*0xE0*/;
    numArray9[6] = (byte) 60;
    numArray9[45] = (byte) 214;
    numArray9[30] = byte.MaxValue;
    numArray9[26] = (byte) 14;
    numArray9[47] = (byte) 92;
    numArray9[18] = (byte) 184;
    numArray9[12] = (byte) 26;
    numArray9[25] = (byte) 52;
    numArray9[19] = (byte) 100;
    numArray9[15] = (byte) 110;
    numArray9[16 /*0x10*/] = (byte) 12;
    numArray9[43] = (byte) 140;
    numArray9[28] = (byte) 79;
    numArray9[1] = (byte) 200;
    numArray9[20] = (byte) 109;
    numArray9[9] = (byte) 89;
    numArray9[36] = (byte) 120;
    numArray9[40] = (byte) 63 /*0x3F*/;
    numArray9[24] = (byte) 115;
    numArray9[10] = (byte) 247;
    numArray9[14] = (byte) 121;
    numArray9[27] = (byte) 165;
    numArray9[22] = (byte) 187;
    numArray9[2] = (byte) 154;
    numArray9[13] = (byte) 34;
    numArray9[37] = (byte) 44;
    numArray9[32 /*0x20*/] = (byte) 199;
    numArray9[33] = (byte) 153;
    numArray9[34] = (byte) 97;
    numArray9[17] = (byte) 153;
    numArray9[8] = (byte) 45;
    numArray9[29] = (byte) 97;
    numArray9[38] = (byte) 215;
    numArray9[11] = (byte) 9;
    numArray9[7] = (byte) 117;
    numArray9[41] = (byte) 29;
    numArray9[42] = (byte) 172;
    numArray9[23] = (byte) 45;
    numArray9[44] = (byte) 125;
    numArray9[35] = (byte) 143;
    numArray9[46] = (byte) 124;
    numArray9[21] = (byte) 205;
    byte[] numArray10 = new byte[48 /*0x30*/];
    numArray10[6] = (byte) 54;
    numArray10[1] = (byte) 137;
    numArray10[36] = (byte) 98;
    numArray10[27] = (byte) 146;
    numArray10[0] = (byte) 142;
    numArray10[5] = (byte) 171;
    numArray10[29] = (byte) 15;
    numArray10[42] = (byte) 169;
    numArray10[8] = (byte) 86;
    numArray10[9] = (byte) 73;
    numArray10[21] = (byte) 1;
    numArray10[11] = (byte) 10;
    numArray10[33] = (byte) 38;
    numArray10[10] = (byte) 87;
    numArray10[14] = (byte) 184;
    numArray10[37] = (byte) 71;
    numArray10[16 /*0x10*/] = (byte) 159;
    numArray10[17] = (byte) 205;
    numArray10[22] = (byte) 179;
    numArray10[19] = (byte) 145;
    numArray10[32 /*0x20*/] = (byte) 22;
    numArray10[34] = (byte) 4;
    numArray10[26] = (byte) 131;
    numArray10[3] = (byte) 150;
    numArray10[24] = (byte) 242;
    numArray10[25] = (byte) 1;
    numArray10[18] = (byte) 103;
    numArray10[23] = (byte) 92;
    numArray10[28] = (byte) 60;
    numArray10[20] = (byte) 162;
    numArray10[30] = (byte) 81;
    numArray10[31 /*0x1F*/] = (byte) 186;
    numArray10[41] = (byte) 44;
    numArray10[4] = (byte) 62;
    numArray10[2] = (byte) 103;
    numArray10[35] = (byte) 202;
    numArray10[46] = (byte) 106;
    numArray10[15] = (byte) 145;
    numArray10[38] = (byte) 167;
    numArray10[39] = (byte) 207;
    numArray10[40] = (byte) 211;
    numArray10[13] = (byte) 93;
    numArray10[7] = (byte) 29;
    numArray10[43] = (byte) 201;
    numArray10[44] = (byte) 156;
    numArray10[45] = (byte) 84;
    numArray10[12] = (byte) 149;
    numArray10[47] = (byte) 62;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12621()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[264];
      byte[] numArray2 = new byte[55]
      {
        (byte) 54,
        (byte) 61,
        (byte) 38,
        (byte) 252,
        (byte) 36,
        (byte) 241,
        (byte) 48 /*0x30*/,
        (byte) 103,
        (byte) 236,
        (byte) 6,
        (byte) 62,
        (byte) 67,
        (byte) 29,
        (byte) 143,
        (byte) 190,
        (byte) 15,
        (byte) 127 /*0x7F*/,
        (byte) 212,
        (byte) 227,
        (byte) 144 /*0x90*/,
        (byte) 251,
        (byte) 56,
        (byte) 14,
        (byte) 20,
        (byte) 254,
        (byte) 31 /*0x1F*/,
        (byte) 79,
        (byte) 135,
        (byte) 161,
        (byte) 117,
        (byte) 122,
        (byte) 196,
        (byte) 155,
        (byte) 205,
        (byte) 95,
        (byte) 222,
        (byte) 85,
        (byte) 92,
        (byte) 27,
        (byte) 9,
        (byte) 251,
        (byte) 161,
        (byte) 207,
        (byte) 16 /*0x10*/,
        (byte) 47,
        (byte) 184,
        (byte) 127 /*0x7F*/,
        (byte) 98,
        (byte) 14,
        (byte) 123,
        (byte) 25,
        (byte) 69,
        (byte) 191,
        (byte) 190,
        (byte) 139
      };
      byte[] numArray3 = new byte[55];
      numArray3[42] = (byte) 80 /*0x50*/;
      numArray3[25] = (byte) 55;
      numArray3[14] = (byte) 49;
      numArray3[53] = (byte) 149;
      numArray3[41] = (byte) 244;
      numArray3[39] = (byte) 215;
      numArray3[6] = (byte) 12;
      numArray3[7] = (byte) 69;
      numArray3[8] = (byte) 131;
      numArray3[9] = (byte) 229;
      numArray3[10] = (byte) 181;
      numArray3[47] = (byte) 37;
      numArray3[12] = (byte) 89;
      numArray3[13] = (byte) 102;
      numArray3[27] = (byte) 118;
      numArray3[0] = (byte) 80 /*0x50*/;
      numArray3[21] = (byte) 168;
      numArray3[35] = (byte) 161;
      numArray3[4] = (byte) 78;
      numArray3[19] = (byte) 98;
      numArray3[20] = (byte) 110;
      numArray3[36] = (byte) 7;
      numArray3[17] = (byte) 89;
      numArray3[23] = (byte) 86;
      numArray3[5] = (byte) 25;
      numArray3[24] = (byte) 129;
      numArray3[2] = (byte) 250;
      numArray3[40] = (byte) 136;
      numArray3[28] = (byte) 196;
      numArray3[29] = (byte) 105;
      numArray3[30] = (byte) 216;
      numArray3[31 /*0x1F*/] = (byte) 59;
      numArray3[38] = (byte) 69;
      numArray3[3] = (byte) 214;
      numArray3[34] = (byte) 84;
      numArray3[16 /*0x10*/] = (byte) 117;
      numArray3[11] = (byte) 199;
      numArray3[18] = (byte) 152;
      numArray3[50] = (byte) 68;
      numArray3[46] = (byte) 80 /*0x50*/;
      numArray3[43] = (byte) 76;
      numArray3[26] = (byte) 91;
      numArray3[1] = (byte) 172;
      numArray3[15] = (byte) 211;
      numArray3[44] = (byte) 37;
      numArray3[45] = (byte) 13;
      numArray3[22] = (byte) 52;
      numArray3[33] = (byte) 235;
      numArray3[32 /*0x20*/] = (byte) 42;
      numArray3[37] = (byte) 143;
      numArray3[49] = (byte) 28;
      numArray3[51] = (byte) 213;
      numArray3[52] = (byte) 70;
      numArray3[48 /*0x30*/] = (byte) 36;
      numArray3[54] = (byte) 228;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 45,
        (byte) 183,
        (byte) 100,
        (byte) 180,
        (byte) 48 /*0x30*/,
        (byte) 160 /*0xA0*/,
        (byte) 26,
        (byte) 87,
        (byte) 116,
        (byte) 204,
        (byte) 12,
        (byte) 206,
        (byte) 47,
        (byte) 118,
        (byte) 196,
        (byte) 162,
        (byte) 114,
        (byte) 68,
        (byte) 127 /*0x7F*/,
        (byte) 16 /*0x10*/,
        (byte) 121,
        (byte) 46,
        (byte) 54,
        (byte) 112 /*0x70*/,
        (byte) 186,
        (byte) 74,
        (byte) 101,
        (byte) 101,
        (byte) 167,
        (byte) 32 /*0x20*/,
        (byte) 178,
        (byte) 17,
        (byte) 203,
        (byte) 126,
        (byte) 21,
        (byte) 167,
        (byte) 178,
        (byte) 179,
        (byte) 32 /*0x20*/,
        (byte) 58,
        (byte) 61,
        (byte) 44,
        (byte) 65,
        (byte) 67,
        (byte) 35,
        (byte) 100,
        (byte) 254,
        (byte) 74,
        (byte) 49,
        (byte) 234,
        (byte) 115,
        (byte) 229,
        (byte) 177,
        (byte) 133,
        (byte) 183
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 41,
        (byte) 236,
        (byte) 35,
        (byte) 45,
        (byte) 68,
        (byte) 160 /*0xA0*/,
        (byte) 176 /*0xB0*/,
        (byte) 1,
        (byte) 19,
        (byte) 103,
        (byte) 77,
        (byte) 178,
        (byte) 196,
        (byte) 98,
        (byte) 31 /*0x1F*/,
        (byte) 9,
        (byte) 138,
        (byte) 13,
        (byte) 242,
        (byte) 253,
        (byte) 83,
        (byte) 225,
        (byte) 232,
        (byte) 216,
        (byte) 166,
        (byte) 166,
        (byte) 6,
        (byte) 64 /*0x40*/,
        (byte) 252,
        (byte) 124,
        (byte) 140,
        (byte) 186,
        (byte) 247,
        (byte) 52,
        (byte) 113,
        (byte) 147,
        (byte) 94,
        (byte) 3,
        (byte) 42,
        (byte) 68,
        (byte) 149,
        (byte) 141,
        (byte) 8,
        (byte) 234,
        (byte) 77,
        (byte) 125,
        (byte) 215,
        (byte) 70,
        (byte) 69,
        (byte) 172,
        (byte) 238,
        (byte) 27,
        (byte) 39,
        (byte) 98,
        (byte) 40
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 244,
        (byte) 250,
        (byte) 230,
        (byte) 59,
        (byte) 181,
        (byte) 175,
        (byte) 83,
        (byte) 25,
        (byte) 251,
        (byte) 3,
        (byte) 177,
        (byte) 96 /*0x60*/,
        (byte) 29,
        (byte) 240 /*0xF0*/,
        (byte) 190,
        (byte) 35,
        (byte) 68,
        (byte) 210,
        (byte) 77,
        (byte) 236,
        (byte) 243,
        (byte) 77,
        (byte) 86,
        (byte) 75,
        (byte) 26,
        (byte) 51,
        (byte) 189,
        (byte) 173,
        (byte) 205,
        (byte) 96 /*0x60*/,
        (byte) 69,
        (byte) 199,
        (byte) 22,
        (byte) 38,
        (byte) 189,
        (byte) 184,
        (byte) 58,
        byte.MaxValue,
        (byte) 136,
        (byte) 124,
        (byte) 238,
        (byte) 42,
        (byte) 39,
        (byte) 96 /*0x60*/,
        (byte) 77,
        (byte) 233,
        (byte) 19,
        (byte) 250,
        (byte) 180,
        (byte) 194,
        (byte) 108,
        (byte) 179,
        (byte) 105,
        (byte) 18,
        (byte) 140
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 95,
        (byte) 42,
        (byte) 93,
        (byte) 72,
        (byte) 204,
        (byte) 63 /*0x3F*/,
        (byte) 122,
        (byte) 32 /*0x20*/,
        (byte) 205,
        (byte) 214,
        (byte) 243,
        (byte) 250,
        (byte) 210,
        (byte) 87,
        (byte) 170,
        (byte) 24,
        (byte) 99,
        (byte) 147,
        (byte) 240 /*0xF0*/,
        (byte) 238,
        (byte) 176 /*0xB0*/,
        (byte) 220,
        (byte) 52,
        (byte) 234,
        (byte) 105,
        (byte) 46,
        (byte) 19,
        (byte) 110,
        (byte) 77,
        (byte) 134,
        (byte) 105,
        (byte) 88,
        (byte) 103,
        (byte) 202,
        (byte) 247,
        (byte) 9,
        (byte) 185,
        (byte) 238,
        (byte) 230,
        (byte) 55,
        (byte) 22,
        (byte) 229,
        (byte) 21,
        byte.MaxValue,
        (byte) 171,
        (byte) 83,
        (byte) 122,
        (byte) 51,
        (byte) 83,
        (byte) 98,
        (byte) 33,
        (byte) 46,
        (byte) 252,
        (byte) 236,
        (byte) 203
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 154,
        (byte) 32 /*0x20*/,
        (byte) 129,
        (byte) 242,
        (byte) 166,
        (byte) 76,
        (byte) 154,
        (byte) 189,
        (byte) 233,
        (byte) 74,
        (byte) 247,
        (byte) 50,
        (byte) 5,
        (byte) 232,
        (byte) 69,
        (byte) 73,
        (byte) 209,
        (byte) 254,
        (byte) 201,
        (byte) 37,
        (byte) 245,
        (byte) 254,
        (byte) 48 /*0x30*/,
        (byte) 31 /*0x1F*/,
        (byte) 195,
        (byte) 249,
        (byte) 11,
        (byte) 244,
        (byte) 146,
        (byte) 94,
        (byte) 4,
        (byte) 153,
        (byte) 22,
        (byte) 20,
        (byte) 160 /*0xA0*/,
        (byte) 222,
        (byte) 6,
        (byte) 137,
        (byte) 72,
        (byte) 222,
        (byte) 141,
        (byte) 163,
        (byte) 99,
        (byte) 159,
        (byte) 28,
        (byte) 199,
        (byte) 174,
        (byte) 217,
        (byte) 39,
        (byte) 105,
        (byte) 151,
        (byte) 19,
        (byte) 195,
        (byte) 130,
        (byte) 175
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 56,
        (byte) 21,
        (byte) 73,
        (byte) 240 /*0xF0*/,
        (byte) 210,
        (byte) 55,
        (byte) 203,
        (byte) 29,
        (byte) 20,
        (byte) 249,
        (byte) 213,
        (byte) 119,
        (byte) 34,
        (byte) 78,
        (byte) 46,
        (byte) 48 /*0x30*/,
        (byte) 53,
        (byte) 129,
        (byte) 118,
        (byte) 58,
        (byte) 162,
        (byte) 138,
        (byte) 241,
        (byte) 34,
        (byte) 86,
        (byte) 83,
        (byte) 221,
        (byte) 183,
        (byte) 46,
        (byte) 211,
        (byte) 217,
        (byte) 11,
        (byte) 48 /*0x30*/,
        (byte) 148,
        (byte) 217,
        (byte) 17,
        (byte) 109,
        (byte) 250,
        (byte) 151,
        (byte) 47,
        (byte) 133,
        (byte) 245,
        (byte) 6,
        (byte) 158,
        (byte) 13,
        (byte) 16 /*0x10*/,
        (byte) 133,
        (byte) 242,
        (byte) 74,
        (byte) 206,
        (byte) 53,
        (byte) 47,
        (byte) 196,
        (byte) 127 /*0x7F*/,
        (byte) 173
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[44];
      numArray10[19] = (byte) 155;
      numArray10[2] = (byte) 173;
      numArray10[0] = (byte) 97;
      numArray10[3] = (byte) 64 /*0x40*/;
      numArray10[4] = (byte) 165;
      numArray10[24] = (byte) 49;
      numArray10[23] = (byte) 74;
      numArray10[1] = (byte) 46;
      numArray10[20] = (byte) 144 /*0x90*/;
      numArray10[9] = (byte) 27;
      numArray10[17] = (byte) 151;
      numArray10[11] = (byte) 71;
      numArray10[12] = (byte) 102;
      numArray10[21] = (byte) 3;
      numArray10[33] = (byte) 238;
      numArray10[13] = (byte) 67;
      numArray10[16 /*0x10*/] = (byte) 235;
      numArray10[5] = (byte) 43;
      numArray10[39] = (byte) 119;
      numArray10[18] = (byte) 81;
      numArray10[6] = (byte) 139;
      numArray10[15] = (byte) 182;
      numArray10[22] = (byte) 251;
      numArray10[41] = (byte) 89;
      numArray10[7] = (byte) 69;
      numArray10[25] = (byte) 205;
      numArray10[27] = (byte) 199;
      numArray10[34] = (byte) 164;
      numArray10[28] = (byte) 240 /*0xF0*/;
      numArray10[29] = (byte) 214;
      numArray10[30] = (byte) 148;
      numArray10[31 /*0x1F*/] = (byte) 101;
      numArray10[14] = (byte) 71;
      numArray10[8] = (byte) 93;
      numArray10[36] = (byte) 154;
      numArray10[35] = (byte) 91;
      numArray10[32 /*0x20*/] = (byte) 5;
      numArray10[37] = (byte) 157;
      numArray10[38] = (byte) 231;
      numArray10[26] = (byte) 40;
      numArray10[40] = (byte) 216;
      numArray10[10] = (byte) 241;
      numArray10[42] = (byte) 191;
      numArray10[43] = (byte) 172;
      byte[] numArray11 = new byte[44]
      {
        (byte) 78,
        (byte) 16 /*0x10*/,
        (byte) 59,
        (byte) 157,
        (byte) 47,
        (byte) 40,
        (byte) 247,
        (byte) 18,
        (byte) 10,
        (byte) 175,
        (byte) 62,
        (byte) 196,
        (byte) 58,
        (byte) 3,
        (byte) 68,
        (byte) 100,
        (byte) 112 /*0x70*/,
        (byte) 104,
        (byte) 144 /*0x90*/,
        (byte) 78,
        (byte) 202,
        (byte) 248,
        (byte) 233,
        (byte) 69,
        (byte) 47,
        (byte) 57,
        (byte) 205,
        (byte) 34,
        (byte) 216,
        (byte) 185,
        (byte) 124,
        (byte) 67,
        (byte) 142,
        (byte) 226,
        (byte) 64 /*0x40*/,
        (byte) 108,
        (byte) 36,
        (byte) 181,
        (byte) 218,
        (byte) 11,
        (byte) 65,
        (byte) 211,
        (byte) 195,
        (byte) 70
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[264];
    byte[] numArray13 = new byte[55]
    {
      (byte) 56,
      (byte) 141,
      (byte) 15,
      (byte) 117,
      (byte) 20,
      (byte) 4,
      (byte) 55,
      (byte) 36,
      (byte) 48 /*0x30*/,
      (byte) 79,
      (byte) 73,
      (byte) 135,
      (byte) 98,
      (byte) 65,
      (byte) 137,
      (byte) 58,
      (byte) 131,
      (byte) 61,
      (byte) 41,
      (byte) 26,
      (byte) 227,
      (byte) 212,
      (byte) 112 /*0x70*/,
      (byte) 204,
      (byte) 0,
      (byte) 76,
      (byte) 193,
      (byte) 137,
      (byte) 221,
      (byte) 132,
      (byte) 61,
      (byte) 233,
      (byte) 88,
      (byte) 196,
      (byte) 227,
      (byte) 192 /*0xC0*/,
      (byte) 160 /*0xA0*/,
      (byte) 100,
      (byte) 174,
      (byte) 50,
      (byte) 12,
      (byte) 43,
      (byte) 93,
      (byte) 79,
      (byte) 195,
      (byte) 123,
      (byte) 20,
      (byte) 81,
      (byte) 115,
      (byte) 110,
      (byte) 133,
      (byte) 199,
      (byte) 101,
      (byte) 44,
      (byte) 69
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 25,
      (byte) 172,
      (byte) 146,
      (byte) 67,
      (byte) 152,
      (byte) 103,
      (byte) 251,
      (byte) 232,
      (byte) 46,
      (byte) 133,
      (byte) 124,
      (byte) 94,
      (byte) 100,
      (byte) 0,
      (byte) 221,
      (byte) 193,
      (byte) 54,
      (byte) 57,
      (byte) 223,
      (byte) 49,
      (byte) 2,
      (byte) 164,
      (byte) 185,
      (byte) 124,
      (byte) 133,
      (byte) 141,
      (byte) 37,
      (byte) 93,
      (byte) 154,
      (byte) 20,
      (byte) 99,
      (byte) 212,
      (byte) 70,
      (byte) 49,
      (byte) 93,
      (byte) 135,
      (byte) 115,
      (byte) 98,
      (byte) 248,
      (byte) 44,
      (byte) 128 /*0x80*/,
      (byte) 59,
      (byte) 47,
      (byte) 28,
      (byte) 65,
      (byte) 22,
      (byte) 242,
      (byte) 212,
      (byte) 125,
      (byte) 194,
      (byte) 42,
      (byte) 136,
      (byte) 224 /*0xE0*/,
      (byte) 24,
      (byte) 209
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 66,
      (byte) 116,
      (byte) 199,
      (byte) 66,
      (byte) 174,
      (byte) 70,
      (byte) 172,
      (byte) 18,
      (byte) 95,
      (byte) 46,
      (byte) 158,
      (byte) 94,
      (byte) 201,
      (byte) 154,
      (byte) 30,
      (byte) 142,
      (byte) 246,
      (byte) 13,
      (byte) 183,
      (byte) 119,
      (byte) 32 /*0x20*/,
      (byte) 9,
      (byte) 133,
      (byte) 56,
      (byte) 112 /*0x70*/,
      (byte) 21,
      (byte) 111,
      byte.MaxValue,
      (byte) 117,
      (byte) 132,
      (byte) 44,
      (byte) 127 /*0x7F*/,
      (byte) 127 /*0x7F*/,
      (byte) 162,
      (byte) 93,
      (byte) 240 /*0xF0*/,
      (byte) 231,
      (byte) 240 /*0xF0*/,
      (byte) 94,
      (byte) 228,
      (byte) 177,
      (byte) 61,
      (byte) 99,
      (byte) 76,
      (byte) 117,
      (byte) 243,
      (byte) 170,
      (byte) 212,
      (byte) 102,
      (byte) 56,
      (byte) 110,
      (byte) 30,
      (byte) 141,
      (byte) 236,
      (byte) 253
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 47,
      (byte) 29,
      (byte) 170,
      (byte) 148,
      (byte) 237,
      (byte) 249,
      (byte) 157,
      (byte) 222,
      (byte) 215,
      (byte) 74,
      (byte) 48 /*0x30*/,
      (byte) 144 /*0x90*/,
      (byte) 57,
      (byte) 81,
      (byte) 8,
      (byte) 121,
      (byte) 33,
      (byte) 91,
      (byte) 68,
      (byte) 123,
      (byte) 104,
      (byte) 225,
      (byte) 211,
      (byte) 210,
      (byte) 119,
      (byte) 31 /*0x1F*/,
      (byte) 108,
      (byte) 135,
      (byte) 246,
      (byte) 143,
      (byte) 29,
      (byte) 39,
      (byte) 153,
      (byte) 183,
      (byte) 144 /*0x90*/,
      (byte) 103,
      (byte) 104,
      (byte) 186,
      (byte) 213,
      (byte) 200,
      (byte) 219,
      (byte) 29,
      (byte) 55,
      (byte) 122,
      (byte) 149,
      (byte) 62,
      (byte) 15,
      (byte) 191,
      (byte) 96 /*0x60*/,
      (byte) 81,
      (byte) 96 /*0x60*/,
      (byte) 89,
      (byte) 145,
      (byte) 141,
      (byte) 245
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55];
    numArray17[6] = (byte) 219;
    numArray17[9] = (byte) 223;
    numArray17[5] = (byte) 156;
    numArray17[3] = (byte) 215;
    numArray17[41] = (byte) 73;
    numArray17[47] = (byte) 144 /*0x90*/;
    numArray17[52] = (byte) 25;
    numArray17[7] = (byte) 2;
    numArray17[31 /*0x1F*/] = (byte) 165;
    numArray17[14] = (byte) 6;
    numArray17[1] = (byte) 210;
    numArray17[11] = (byte) 60;
    numArray17[12] = (byte) 46;
    numArray17[53] = (byte) 147;
    numArray17[21] = (byte) 194;
    numArray17[26] = (byte) 60;
    numArray17[16 /*0x10*/] = (byte) 211;
    numArray17[17] = (byte) 198;
    numArray17[18] = (byte) 227;
    numArray17[19] = (byte) 165;
    numArray17[20] = (byte) 57;
    numArray17[54] = (byte) 182;
    numArray17[38] = (byte) 3;
    numArray17[13] = (byte) 167;
    numArray17[24] = (byte) 155;
    numArray17[2] = (byte) 152;
    numArray17[4] = (byte) 173;
    numArray17[27] = (byte) 21;
    numArray17[28] = (byte) 13;
    numArray17[29] = (byte) 242;
    numArray17[30] = (byte) 146;
    numArray17[40] = (byte) 136;
    numArray17[45] = (byte) 152;
    numArray17[0] = (byte) 143;
    numArray17[34] = (byte) 87;
    numArray17[36] = (byte) 197;
    numArray17[23] = (byte) 107;
    numArray17[49] = (byte) 111;
    numArray17[25] = (byte) 152;
    numArray17[39] = (byte) 160 /*0xA0*/;
    numArray17[43] = (byte) 19;
    numArray17[22] = (byte) 124;
    numArray17[42] = (byte) 86;
    numArray17[35] = (byte) 29;
    numArray17[44] = (byte) 131;
    numArray17[15] = (byte) 175;
    numArray17[50] = (byte) 185;
    numArray17[32 /*0x20*/] = (byte) 36;
    numArray17[48 /*0x30*/] = (byte) 103;
    numArray17[8] = (byte) 31 /*0x1F*/;
    numArray17[10] = (byte) 38;
    numArray17[46] = (byte) 56;
    numArray17[51] = (byte) 38;
    numArray17[33] = (byte) 0;
    numArray17[37] = (byte) 13;
    byte[] numArray18 = new byte[55];
    numArray18[4] = (byte) 66;
    numArray18[1] = (byte) 162;
    numArray18[17] = (byte) 83;
    numArray18[51] = byte.MaxValue;
    numArray18[43] = (byte) 176 /*0xB0*/;
    numArray18[5] = (byte) 82;
    numArray18[6] = (byte) 172;
    numArray18[54] = (byte) 6;
    numArray18[8] = (byte) 202;
    numArray18[34] = (byte) 209;
    numArray18[3] = (byte) 176 /*0xB0*/;
    numArray18[36] = (byte) 175;
    numArray18[12] = (byte) 29;
    numArray18[13] = (byte) 62;
    numArray18[9] = (byte) 192 /*0xC0*/;
    numArray18[23] = (byte) 141;
    numArray18[30] = (byte) 117;
    numArray18[18] = (byte) 226;
    numArray18[16 /*0x10*/] = (byte) 77;
    numArray18[19] = (byte) 90;
    numArray18[20] = (byte) 140;
    numArray18[15] = (byte) 93;
    numArray18[21] = (byte) 45;
    numArray18[52] = (byte) 101;
    numArray18[35] = (byte) 32 /*0x20*/;
    numArray18[25] = (byte) 156;
    numArray18[26] = (byte) 116;
    numArray18[22] = (byte) 51;
    numArray18[28] = (byte) 39;
    numArray18[29] = (byte) 135;
    numArray18[10] = (byte) 213;
    numArray18[50] = (byte) 229;
    numArray18[32 /*0x20*/] = (byte) 219;
    numArray18[33] = (byte) 49;
    numArray18[2] = (byte) 166;
    numArray18[42] = (byte) 231;
    numArray18[39] = (byte) 162;
    numArray18[14] = (byte) 28;
    numArray18[11] = (byte) 157;
    numArray18[27] = (byte) 178;
    numArray18[40] = (byte) 136;
    numArray18[41] = (byte) 9;
    numArray18[38] = (byte) 145;
    numArray18[7] = (byte) 31 /*0x1F*/;
    numArray18[44] = (byte) 63 /*0x3F*/;
    numArray18[45] = (byte) 61;
    numArray18[46] = (byte) 61;
    numArray18[47] = (byte) 66;
    numArray18[48 /*0x30*/] = (byte) 9;
    numArray18[49] = (byte) 160 /*0xA0*/;
    numArray18[24] = (byte) 165;
    numArray18[0] = (byte) 133;
    numArray18[31 /*0x1F*/] = (byte) 235;
    numArray18[53] = (byte) 106;
    numArray18[37] = (byte) 136;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 98,
      (byte) 182,
      (byte) 97,
      (byte) 73,
      (byte) 232,
      (byte) 61,
      (byte) 176 /*0xB0*/,
      (byte) 219,
      (byte) 63 /*0x3F*/,
      (byte) 107,
      (byte) 95,
      (byte) 154,
      (byte) 69,
      (byte) 1,
      (byte) 93,
      (byte) 153,
      (byte) 118,
      (byte) 175,
      (byte) 153,
      (byte) 15,
      (byte) 189,
      (byte) 213,
      (byte) 136,
      (byte) 44,
      (byte) 238,
      (byte) 208 /*0xD0*/,
      (byte) 229,
      (byte) 69,
      (byte) 116,
      (byte) 225,
      (byte) 0,
      (byte) 85,
      (byte) 11,
      (byte) 144 /*0x90*/,
      (byte) 238,
      (byte) 225,
      (byte) 216,
      (byte) 66,
      (byte) 195,
      (byte) 194,
      (byte) 131,
      (byte) 9,
      (byte) 206,
      (byte) 142,
      (byte) 200,
      (byte) 225,
      (byte) 25,
      (byte) 50,
      (byte) 161,
      (byte) 248,
      (byte) 93,
      (byte) 106,
      (byte) 147,
      (byte) 14,
      (byte) 141
    };
    byte[] numArray20 = new byte[55];
    numArray20[21] = (byte) 7;
    numArray20[44] = (byte) 119;
    numArray20[2] = (byte) 141;
    numArray20[3] = (byte) 197;
    numArray20[13] = (byte) 114;
    numArray20[5] = (byte) 55;
    numArray20[6] = (byte) 146;
    numArray20[7] = (byte) 137;
    numArray20[34] = (byte) 7;
    numArray20[54] = (byte) 217;
    numArray20[10] = (byte) 127 /*0x7F*/;
    numArray20[11] = (byte) 4;
    numArray20[53] = (byte) 16 /*0x10*/;
    numArray20[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    numArray20[14] = (byte) 23;
    numArray20[15] = (byte) 234;
    numArray20[20] = (byte) 100;
    numArray20[45] = (byte) 227;
    numArray20[37] = (byte) 122;
    numArray20[19] = (byte) 95;
    numArray20[1] = (byte) 33;
    numArray20[43] = (byte) 192 /*0xC0*/;
    numArray20[26] = (byte) 240 /*0xF0*/;
    numArray20[51] = (byte) 156;
    numArray20[24] = (byte) 177;
    numArray20[22] = (byte) 65;
    numArray20[16 /*0x10*/] = (byte) 250;
    numArray20[27] = (byte) 176 /*0xB0*/;
    numArray20[12] = (byte) 32 /*0x20*/;
    numArray20[29] = (byte) 61;
    numArray20[4] = (byte) 180;
    numArray20[31 /*0x1F*/] = (byte) 187;
    numArray20[35] = (byte) 57;
    numArray20[33] = (byte) 106;
    numArray20[25] = (byte) 106;
    numArray20[48 /*0x30*/] = (byte) 174;
    numArray20[36] = (byte) 145;
    numArray20[17] = (byte) 128 /*0x80*/;
    numArray20[38] = (byte) 11;
    numArray20[46] = (byte) 142;
    numArray20[28] = (byte) 135;
    numArray20[41] = (byte) 43;
    numArray20[42] = (byte) 254;
    numArray20[8] = (byte) 49;
    numArray20[18] = (byte) 61;
    numArray20[49] = (byte) 50;
    numArray20[9] = (byte) 202;
    numArray20[47] = (byte) 169;
    numArray20[39] = (byte) 242;
    numArray20[30] = (byte) 235;
    numArray20[50] = (byte) 155;
    numArray20[23] = (byte) 231;
    numArray20[52] = (byte) 43;
    numArray20[0] = (byte) 77;
    numArray20[40] = (byte) 178;
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[44]
    {
      (byte) 209,
      (byte) 78,
      (byte) 95,
      (byte) 164,
      (byte) 198,
      (byte) 234,
      (byte) 217,
      (byte) 212,
      (byte) 71,
      (byte) 145,
      (byte) 177,
      (byte) 176 /*0xB0*/,
      (byte) 200,
      (byte) 29,
      (byte) 76,
      (byte) 64 /*0x40*/,
      (byte) 108,
      (byte) 115,
      (byte) 18,
      (byte) 150,
      (byte) 214,
      (byte) 164,
      (byte) 185,
      (byte) 83,
      (byte) 147,
      (byte) 126,
      (byte) 69,
      (byte) 91,
      (byte) 23,
      (byte) 5,
      (byte) 122,
      (byte) 42,
      (byte) 220,
      (byte) 127 /*0x7F*/,
      (byte) 210,
      (byte) 152,
      (byte) 75,
      (byte) 198,
      (byte) 82,
      (byte) 184,
      (byte) 150,
      (byte) 79,
      (byte) 118,
      (byte) 193
    };
    byte[] numArray22 = new byte[44];
    numArray22[12] = (byte) 133;
    numArray22[1] = (byte) 113;
    numArray22[32 /*0x20*/] = (byte) 14;
    numArray22[40] = (byte) 236;
    numArray22[30] = (byte) 241;
    numArray22[5] = (byte) 174;
    numArray22[21] = (byte) 129;
    numArray22[26] = (byte) 59;
    numArray22[6] = (byte) 207;
    numArray22[9] = (byte) 5;
    numArray22[17] = (byte) 30;
    numArray22[11] = (byte) 10;
    numArray22[10] = (byte) 252;
    numArray22[0] = (byte) 179;
    numArray22[14] = (byte) 34;
    numArray22[15] = (byte) 68;
    numArray22[16 /*0x10*/] = (byte) 13;
    numArray22[13] = (byte) 166;
    numArray22[39] = (byte) 201;
    numArray22[19] = (byte) 165;
    numArray22[20] = (byte) 210;
    numArray22[27] = (byte) 220;
    numArray22[43] = (byte) 123;
    numArray22[23] = (byte) 197;
    numArray22[24] = (byte) 110;
    numArray22[25] = (byte) 199;
    numArray22[3] = (byte) 15;
    numArray22[8] = (byte) 17;
    numArray22[7] = (byte) 245;
    numArray22[29] = (byte) 4;
    numArray22[18] = (byte) 126;
    numArray22[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
    numArray22[2] = (byte) 61;
    numArray22[33] = (byte) 184;
    numArray22[34] = (byte) 251;
    numArray22[35] = (byte) 243;
    numArray22[36] = (byte) 113;
    numArray22[38] = (byte) 168;
    numArray22[22] = (byte) 123;
    numArray22[28] = (byte) 71;
    numArray22[42] = (byte) 151;
    numArray22[41] = (byte) 211;
    numArray22[37] = (byte) 228;
    numArray22[4] = (byte) 104;
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 44);
    for (int index = 0; index < 44; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12622()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[1] = (byte) 10;
      numArray2[2] = (byte) 19;
      numArray2[5] = (byte) 52;
      numArray2[3] = (byte) 23;
      numArray2[4] = (byte) 146;
      numArray2[7] = (byte) 42;
      numArray2[6] = (byte) 169;
      numArray2[0] = (byte) 181;
      numArray2[8] = (byte) 154;
      byte[] numArray3 = new byte[9];
      numArray3[5] = (byte) 64 /*0x40*/;
      numArray3[4] = (byte) 61;
      numArray3[2] = (byte) 48 /*0x30*/;
      numArray3[3] = (byte) 13;
      numArray3[6] = (byte) 93;
      numArray3[8] = (byte) 43;
      numArray3[7] = (byte) 70;
      numArray3[1] = (byte) 159;
      numArray3[0] = (byte) 106;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 37,
      (byte) 165,
      (byte) 176 /*0xB0*/,
      (byte) 181,
      (byte) 241,
      (byte) 22,
      (byte) 70,
      (byte) 232,
      (byte) 245
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 202,
      (byte) 72,
      (byte) 197,
      (byte) 136,
      (byte) 82,
      (byte) 155,
      (byte) 202,
      (byte) 145,
      (byte) 160 /*0xA0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12623()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[81];
      byte[] numArray2 = new byte[55]
      {
        (byte) 12,
        (byte) 92,
        (byte) 86,
        (byte) 145,
        (byte) 11,
        (byte) 123,
        (byte) 246,
        (byte) 0,
        (byte) 78,
        (byte) 166,
        (byte) 104,
        (byte) 211,
        (byte) 201,
        (byte) 227,
        (byte) 153,
        (byte) 23,
        (byte) 175,
        (byte) 168,
        (byte) 252,
        (byte) 6,
        (byte) 181,
        (byte) 239,
        (byte) 41,
        (byte) 120,
        (byte) 146,
        (byte) 184,
        (byte) 215,
        (byte) 37,
        (byte) 5,
        (byte) 223,
        (byte) 5,
        (byte) 76,
        (byte) 245,
        (byte) 97,
        (byte) 26,
        (byte) 29,
        (byte) 169,
        (byte) 81,
        (byte) 128 /*0x80*/,
        (byte) 49,
        (byte) 46,
        (byte) 229,
        (byte) 215,
        byte.MaxValue,
        (byte) 71,
        (byte) 226,
        (byte) 40,
        (byte) 51,
        (byte) 173,
        (byte) 208 /*0xD0*/,
        (byte) 70,
        (byte) 80 /*0x50*/,
        (byte) 83,
        (byte) 111,
        (byte) 213
      };
      byte[] numArray3 = new byte[55];
      numArray3[11] = (byte) 86;
      numArray3[42] = (byte) 108;
      numArray3[2] = (byte) 99;
      numArray3[34] = (byte) 9;
      numArray3[31 /*0x1F*/] = (byte) 91;
      numArray3[30] = (byte) 51;
      numArray3[6] = (byte) 112 /*0x70*/;
      numArray3[24] = (byte) 226;
      numArray3[8] = (byte) 183;
      numArray3[45] = (byte) 202;
      numArray3[40] = (byte) 159;
      numArray3[29] = (byte) 103;
      numArray3[12] = (byte) 23;
      numArray3[13] = (byte) 177;
      numArray3[7] = (byte) 237;
      numArray3[19] = (byte) 190;
      numArray3[16 /*0x10*/] = (byte) 151;
      numArray3[17] = (byte) 72;
      numArray3[18] = (byte) 66;
      numArray3[3] = (byte) 149;
      numArray3[4] = (byte) 27;
      numArray3[50] = (byte) 151;
      numArray3[25] = (byte) 247;
      numArray3[23] = (byte) 251;
      numArray3[46] = (byte) 155;
      numArray3[5] = (byte) 154;
      numArray3[15] = (byte) 133;
      numArray3[27] = (byte) 219;
      numArray3[28] = (byte) 192 /*0xC0*/;
      numArray3[9] = (byte) 228;
      numArray3[38] = (byte) 216;
      numArray3[14] = (byte) 208 /*0xD0*/;
      numArray3[32 /*0x20*/] = (byte) 114;
      numArray3[33] = (byte) 208 /*0xD0*/;
      numArray3[10] = (byte) 177;
      numArray3[35] = (byte) 193;
      numArray3[36] = (byte) 137;
      numArray3[52] = (byte) 79;
      numArray3[0] = (byte) 40;
      numArray3[39] = (byte) 40;
      numArray3[49] = (byte) 6;
      numArray3[41] = (byte) 81;
      numArray3[37] = (byte) 73;
      numArray3[43] = (byte) 66;
      numArray3[44] = (byte) 121;
      numArray3[1] = (byte) 71;
      numArray3[26] = (byte) 61;
      numArray3[20] = (byte) 38;
      numArray3[48 /*0x30*/] = (byte) 202;
      numArray3[51] = (byte) 27;
      numArray3[47] = (byte) 38;
      numArray3[22] = (byte) 209;
      numArray3[21] = (byte) 84;
      numArray3[53] = (byte) 228;
      numArray3[54] = (byte) 233;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[26]
      {
        (byte) 174,
        (byte) 77,
        (byte) 194,
        byte.MaxValue,
        (byte) 87,
        (byte) 197,
        (byte) 212,
        (byte) 235,
        (byte) 34,
        (byte) 88,
        (byte) 218,
        (byte) 203,
        (byte) 145,
        (byte) 164,
        (byte) 9,
        (byte) 249,
        (byte) 61,
        (byte) 122,
        (byte) 228,
        (byte) 174,
        (byte) 202,
        (byte) 3,
        (byte) 171,
        (byte) 207,
        (byte) 58,
        (byte) 71
      };
      byte[] numArray5 = new byte[26]
      {
        (byte) 203,
        (byte) 21,
        (byte) 77,
        (byte) 226,
        (byte) 113,
        (byte) 71,
        (byte) 119,
        (byte) 7,
        (byte) 98,
        (byte) 178,
        (byte) 66,
        (byte) 20,
        (byte) 186,
        (byte) 57,
        byte.MaxValue,
        (byte) 149,
        (byte) 35,
        (byte) 230,
        (byte) 86,
        (byte) 35,
        (byte) 72,
        (byte) 150,
        byte.MaxValue,
        (byte) 155,
        (byte) 228,
        (byte) 202
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 26);
      for (int index = 0; index < 26; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[81];
    byte[] numArray7 = new byte[55]
    {
      (byte) 113,
      (byte) 237,
      (byte) 135,
      (byte) 248,
      (byte) 225,
      (byte) 27,
      (byte) 5,
      (byte) 107,
      (byte) 142,
      (byte) 6,
      (byte) 37,
      (byte) 148,
      (byte) 116,
      (byte) 10,
      (byte) 172,
      (byte) 230,
      (byte) 158,
      (byte) 160 /*0xA0*/,
      (byte) 189,
      (byte) 223,
      (byte) 48 /*0x30*/,
      (byte) 74,
      (byte) 30,
      (byte) 32 /*0x20*/,
      (byte) 86,
      (byte) 62,
      (byte) 171,
      (byte) 161,
      (byte) 242,
      (byte) 196,
      (byte) 16 /*0x10*/,
      (byte) 242,
      (byte) 36,
      (byte) 95,
      (byte) 177,
      (byte) 106,
      (byte) 82,
      (byte) 210,
      (byte) 175,
      (byte) 137,
      (byte) 4,
      (byte) 141,
      (byte) 175,
      (byte) 115,
      (byte) 190,
      (byte) 26,
      (byte) 86,
      (byte) 249,
      byte.MaxValue,
      (byte) 233,
      (byte) 155,
      (byte) 166,
      (byte) 21,
      (byte) 208 /*0xD0*/,
      (byte) 91
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 220,
      (byte) 252,
      (byte) 160 /*0xA0*/,
      byte.MaxValue,
      (byte) 129,
      (byte) 103,
      (byte) 110,
      (byte) 12,
      (byte) 132,
      (byte) 175,
      (byte) 83,
      (byte) 6,
      (byte) 5,
      (byte) 177,
      (byte) 23,
      (byte) 45,
      (byte) 179,
      (byte) 125,
      (byte) 133,
      (byte) 2,
      (byte) 231,
      (byte) 86,
      (byte) 216,
      (byte) 219,
      (byte) 97,
      (byte) 210,
      (byte) 118,
      (byte) 9,
      (byte) 153,
      (byte) 148,
      (byte) 168,
      (byte) 17,
      (byte) 237,
      (byte) 131,
      (byte) 60,
      (byte) 245,
      (byte) 72,
      (byte) 196,
      (byte) 41,
      (byte) 220,
      (byte) 51,
      (byte) 55,
      (byte) 111,
      (byte) 9,
      (byte) 93,
      (byte) 148,
      (byte) 206,
      (byte) 68,
      (byte) 151,
      (byte) 241,
      (byte) 20,
      (byte) 252,
      (byte) 15,
      (byte) 206,
      (byte) 163
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[26];
    numArray9[19] = (byte) 155;
    numArray9[8] = (byte) 51;
    numArray9[18] = (byte) 162;
    numArray9[15] = (byte) 241;
    numArray9[9] = (byte) 111;
    numArray9[0] = (byte) 102;
    numArray9[6] = (byte) 201;
    numArray9[13] = (byte) 147;
    numArray9[2] = (byte) 163;
    numArray9[17] = (byte) 218;
    numArray9[3] = (byte) 112 /*0x70*/;
    numArray9[11] = (byte) 89;
    numArray9[1] = (byte) 162;
    numArray9[12] = (byte) 65;
    numArray9[14] = (byte) 203;
    numArray9[7] = (byte) 245;
    numArray9[4] = (byte) 99;
    numArray9[25] = (byte) 162;
    numArray9[10] = (byte) 20;
    numArray9[16 /*0x10*/] = (byte) 52;
    numArray9[20] = (byte) 170;
    numArray9[21] = (byte) 97;
    numArray9[22] = (byte) 59;
    numArray9[23] = (byte) 232;
    numArray9[24] = (byte) 177;
    numArray9[5] = (byte) 121;
    byte[] numArray10 = new byte[26]
    {
      (byte) 31 /*0x1F*/,
      (byte) 139,
      (byte) 91,
      (byte) 155,
      (byte) 108,
      (byte) 110,
      (byte) 230,
      (byte) 254,
      (byte) 47,
      (byte) 177,
      (byte) 33,
      (byte) 0,
      (byte) 103,
      (byte) 152,
      (byte) 247,
      (byte) 36,
      (byte) 82,
      (byte) 224 /*0xE0*/,
      (byte) 203,
      (byte) 163,
      (byte) 121,
      (byte) 22,
      (byte) 106,
      (byte) 198,
      (byte) 209,
      (byte) 234
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 26);
    for (int index = 0; index < 26; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12624()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[108];
      byte[] numArray2 = new byte[55]
      {
        (byte) 102,
        (byte) 187,
        (byte) 190,
        (byte) 193,
        (byte) 112 /*0x70*/,
        (byte) 160 /*0xA0*/,
        (byte) 139,
        (byte) 87,
        (byte) 28,
        (byte) 16 /*0x10*/,
        (byte) 206,
        (byte) 166,
        (byte) 167,
        (byte) 155,
        (byte) 229,
        (byte) 27,
        (byte) 57,
        (byte) 142,
        (byte) 181,
        (byte) 43,
        (byte) 214,
        (byte) 145,
        (byte) 166,
        (byte) 17,
        (byte) 228,
        (byte) 179,
        (byte) 89,
        (byte) 180,
        (byte) 250,
        (byte) 93,
        (byte) 75,
        byte.MaxValue,
        (byte) 117,
        (byte) 231,
        (byte) 201,
        (byte) 80 /*0x50*/,
        (byte) 90,
        (byte) 223,
        (byte) 162,
        (byte) 69,
        (byte) 243,
        (byte) 51,
        (byte) 142,
        (byte) 49,
        (byte) 209,
        (byte) 23,
        (byte) 173,
        (byte) 52,
        (byte) 208 /*0xD0*/,
        (byte) 198,
        (byte) 187,
        (byte) 41,
        (byte) 214,
        (byte) 250,
        (byte) 45
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 141,
        (byte) 91,
        (byte) 9,
        (byte) 47,
        (byte) 114,
        (byte) 153,
        (byte) 126,
        (byte) 226,
        (byte) 132,
        (byte) 153,
        (byte) 28,
        (byte) 53,
        (byte) 12,
        (byte) 225,
        (byte) 237,
        (byte) 150,
        (byte) 253,
        (byte) 67,
        (byte) 62,
        (byte) 248,
        (byte) 237,
        (byte) 207,
        (byte) 103,
        (byte) 231,
        (byte) 202,
        (byte) 87,
        (byte) 151,
        (byte) 193,
        (byte) 197,
        (byte) 190,
        (byte) 148,
        (byte) 48 /*0x30*/,
        (byte) 184,
        (byte) 214,
        (byte) 231,
        (byte) 144 /*0x90*/,
        (byte) 247,
        (byte) 116,
        (byte) 179,
        (byte) 21,
        (byte) 31 /*0x1F*/,
        (byte) 149,
        (byte) 231,
        (byte) 181,
        (byte) 46,
        (byte) 208 /*0xD0*/,
        (byte) 112 /*0x70*/,
        (byte) 15,
        (byte) 68,
        (byte) 211,
        (byte) 210,
        (byte) 106,
        (byte) 22,
        (byte) 136,
        (byte) 16 /*0x10*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53]
      {
        (byte) 173,
        (byte) 221,
        (byte) 189,
        (byte) 70,
        (byte) 214,
        (byte) 92,
        (byte) 190,
        (byte) 196,
        (byte) 207,
        (byte) 48 /*0x30*/,
        (byte) 192 /*0xC0*/,
        (byte) 209,
        (byte) 148,
        (byte) 192 /*0xC0*/,
        (byte) 90,
        (byte) 236,
        (byte) 111,
        (byte) 55,
        (byte) 163,
        (byte) 2,
        (byte) 116,
        (byte) 63 /*0x3F*/,
        (byte) 204,
        (byte) 5,
        (byte) 36,
        (byte) 228,
        (byte) 52,
        (byte) 161,
        (byte) 33,
        (byte) 41,
        (byte) 123,
        (byte) 185,
        (byte) 244,
        (byte) 59,
        (byte) 173,
        (byte) 2,
        (byte) 229,
        byte.MaxValue,
        (byte) 28,
        (byte) 215,
        (byte) 207,
        (byte) 109,
        (byte) 2,
        (byte) 230,
        (byte) 237,
        (byte) 99,
        (byte) 59,
        (byte) 5,
        (byte) 97,
        (byte) 61,
        (byte) 102,
        (byte) 194,
        (byte) 138
      };
      byte[] numArray5 = new byte[53]
      {
        (byte) 252,
        (byte) 227,
        (byte) 86,
        (byte) 187,
        (byte) 179,
        (byte) 57,
        (byte) 135,
        (byte) 180,
        (byte) 83,
        (byte) 201,
        (byte) 128 /*0x80*/,
        (byte) 23,
        (byte) 20,
        (byte) 23,
        (byte) 193,
        (byte) 173,
        (byte) 208 /*0xD0*/,
        (byte) 233,
        (byte) 86,
        (byte) 85,
        (byte) 167,
        (byte) 64 /*0x40*/,
        (byte) 32 /*0x20*/,
        (byte) 209,
        (byte) 148,
        (byte) 152,
        (byte) 81,
        (byte) 132,
        (byte) 230,
        (byte) 11,
        (byte) 247,
        (byte) 156,
        (byte) 76,
        (byte) 221,
        (byte) 197,
        (byte) 129,
        (byte) 117,
        (byte) 7,
        (byte) 83,
        (byte) 221,
        (byte) 169,
        (byte) 11,
        (byte) 20,
        (byte) 214,
        (byte) 96 /*0x60*/,
        (byte) 19,
        (byte) 57,
        (byte) 139,
        (byte) 126,
        (byte) 168,
        (byte) 116,
        (byte) 252,
        (byte) 4
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[108];
    byte[] numArray7 = new byte[55]
    {
      (byte) 16 /*0x10*/,
      (byte) 183,
      (byte) 33,
      (byte) 242,
      (byte) 40,
      (byte) 29,
      (byte) 162,
      (byte) 165,
      (byte) 181,
      (byte) 6,
      (byte) 220,
      (byte) 89,
      (byte) 19,
      (byte) 87,
      (byte) 6,
      (byte) 112 /*0x70*/,
      (byte) 26,
      (byte) 203,
      (byte) 51,
      (byte) 6,
      (byte) 60,
      (byte) 229,
      (byte) 24,
      (byte) 193,
      (byte) 168,
      (byte) 47,
      (byte) 63 /*0x3F*/,
      (byte) 228,
      (byte) 194,
      (byte) 185,
      (byte) 47,
      (byte) 33,
      (byte) 154,
      (byte) 63 /*0x3F*/,
      (byte) 241,
      (byte) 45,
      (byte) 221,
      (byte) 42,
      (byte) 175,
      (byte) 177,
      (byte) 202,
      (byte) 92,
      (byte) 49,
      (byte) 231,
      (byte) 134,
      (byte) 226,
      (byte) 242,
      (byte) 10,
      (byte) 54,
      (byte) 246,
      (byte) 167,
      (byte) 190,
      (byte) 158,
      (byte) 154,
      (byte) 48 /*0x30*/
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 129,
      (byte) 5,
      (byte) 48 /*0x30*/,
      (byte) 200,
      (byte) 65,
      (byte) 243,
      (byte) 125,
      (byte) 243,
      (byte) 232,
      (byte) 224 /*0xE0*/,
      (byte) 170,
      (byte) 237,
      (byte) 74,
      (byte) 230,
      (byte) 24,
      (byte) 128 /*0x80*/,
      (byte) 108,
      (byte) 86,
      (byte) 93,
      (byte) 228,
      (byte) 31 /*0x1F*/,
      (byte) 226,
      (byte) 104,
      (byte) 205,
      (byte) 199,
      (byte) 194,
      (byte) 98,
      (byte) 165,
      (byte) 210,
      (byte) 120,
      (byte) 139,
      (byte) 229,
      (byte) 107,
      (byte) 152,
      (byte) 36,
      (byte) 143,
      (byte) 17,
      (byte) 148,
      (byte) 133,
      (byte) 151,
      (byte) 193,
      (byte) 48 /*0x30*/,
      (byte) 164,
      (byte) 151,
      (byte) 51,
      (byte) 125,
      (byte) 243,
      (byte) 248,
      (byte) 179,
      (byte) 214,
      (byte) 162,
      (byte) 26,
      (byte) 209,
      (byte) 53,
      (byte) 246
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[53]
    {
      (byte) 95,
      (byte) 73,
      (byte) 248,
      (byte) 221,
      (byte) 190,
      (byte) 227,
      (byte) 252,
      (byte) 215,
      (byte) 187,
      (byte) 190,
      (byte) 211,
      (byte) 101,
      (byte) 217,
      (byte) 146,
      (byte) 240 /*0xF0*/,
      (byte) 28,
      (byte) 129,
      (byte) 59,
      (byte) 223,
      (byte) 179,
      (byte) 49,
      (byte) 59,
      (byte) 198,
      (byte) 61,
      (byte) 53,
      (byte) 173,
      (byte) 204,
      (byte) 17,
      (byte) 112 /*0x70*/,
      (byte) 130,
      byte.MaxValue,
      (byte) 15,
      (byte) 198,
      (byte) 142,
      (byte) 125,
      (byte) 225,
      (byte) 182,
      (byte) 212,
      (byte) 12,
      (byte) 121,
      (byte) 53,
      (byte) 15,
      (byte) 203,
      (byte) 35,
      (byte) 30,
      (byte) 164,
      (byte) 93,
      (byte) 100,
      (byte) 183,
      (byte) 161,
      (byte) 28,
      (byte) 94,
      (byte) 80 /*0x50*/
    };
    byte[] numArray10 = new byte[53]
    {
      (byte) 98,
      (byte) 77,
      (byte) 141,
      (byte) 126,
      (byte) 171,
      (byte) 54,
      (byte) 55,
      (byte) 68,
      (byte) 191,
      (byte) 12,
      (byte) 67,
      (byte) 204,
      (byte) 26,
      (byte) 79,
      (byte) 244,
      (byte) 220,
      (byte) 153,
      (byte) 143,
      (byte) 30,
      (byte) 16 /*0x10*/,
      (byte) 17,
      (byte) 221,
      (byte) 43,
      (byte) 240 /*0xF0*/,
      (byte) 19,
      (byte) 232,
      (byte) 25,
      (byte) 162,
      (byte) 107,
      (byte) 254,
      (byte) 131,
      (byte) 123,
      (byte) 162,
      (byte) 229,
      (byte) 52,
      (byte) 86,
      (byte) 4,
      (byte) 237,
      (byte) 253,
      (byte) 86,
      (byte) 12,
      (byte) 239,
      (byte) 223,
      (byte) 230,
      (byte) 165,
      (byte) 80 /*0x50*/,
      (byte) 150,
      (byte) 180,
      (byte) 251,
      (byte) 235,
      (byte) 164,
      (byte) 33,
      (byte) 239
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 53);
    for (int index = 0; index < 53; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12625()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[117];
      byte[] numArray2 = new byte[55]
      {
        (byte) 187,
        (byte) 167,
        (byte) 16 /*0x10*/,
        (byte) 23,
        (byte) 234,
        (byte) 103,
        (byte) 56,
        (byte) 191,
        (byte) 196,
        (byte) 159,
        (byte) 75,
        (byte) 20,
        (byte) 15,
        (byte) 102,
        (byte) 173,
        (byte) 47,
        (byte) 101,
        (byte) 126,
        (byte) 33,
        (byte) 112 /*0x70*/,
        (byte) 162,
        (byte) 30,
        (byte) 145,
        (byte) 80 /*0x50*/,
        (byte) 103,
        (byte) 90,
        (byte) 206,
        (byte) 101,
        (byte) 160 /*0xA0*/,
        (byte) 190,
        (byte) 79,
        (byte) 31 /*0x1F*/,
        (byte) 89,
        (byte) 93,
        (byte) 250,
        (byte) 44,
        (byte) 251,
        (byte) 98,
        (byte) 74,
        (byte) 168,
        (byte) 146,
        (byte) 100,
        (byte) 89,
        (byte) 5,
        (byte) 18,
        (byte) 79,
        (byte) 168,
        (byte) 59,
        (byte) 111,
        (byte) 47,
        (byte) 234,
        (byte) 248,
        (byte) 77,
        (byte) 179,
        (byte) 225
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 68,
        (byte) 34,
        (byte) 72,
        (byte) 178,
        (byte) 196,
        (byte) 62,
        (byte) 23,
        (byte) 182,
        (byte) 21,
        (byte) 42,
        (byte) 165,
        (byte) 198,
        (byte) 39,
        (byte) 122,
        (byte) 44,
        (byte) 192 /*0xC0*/,
        (byte) 1,
        (byte) 167,
        (byte) 121,
        (byte) 193,
        (byte) 230,
        (byte) 193,
        (byte) 27,
        (byte) 56,
        (byte) 19,
        (byte) 117,
        (byte) 99,
        (byte) 162,
        (byte) 70,
        (byte) 7,
        (byte) 196,
        (byte) 233,
        (byte) 125,
        (byte) 239,
        (byte) 100,
        (byte) 60,
        (byte) 172,
        (byte) 49,
        (byte) 25,
        (byte) 192 /*0xC0*/,
        (byte) 70,
        (byte) 97,
        (byte) 216,
        (byte) 246,
        (byte) 56,
        (byte) 212,
        (byte) 204,
        (byte) 193,
        (byte) 2,
        (byte) 106,
        (byte) 24,
        (byte) 184,
        (byte) 44,
        (byte) 70,
        (byte) 147
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 192 /*0xC0*/,
        (byte) 175,
        (byte) 144 /*0x90*/,
        (byte) 152,
        (byte) 210,
        (byte) 30,
        (byte) 6,
        (byte) 80 /*0x50*/,
        (byte) 43,
        (byte) 187,
        (byte) 177,
        (byte) 108,
        (byte) 128 /*0x80*/,
        (byte) 52,
        (byte) 3,
        (byte) 52,
        (byte) 120,
        (byte) 221,
        (byte) 61,
        (byte) 196,
        (byte) 140,
        (byte) 62,
        (byte) 24,
        (byte) 177,
        (byte) 146,
        (byte) 246,
        (byte) 113,
        (byte) 211,
        (byte) 86,
        (byte) 210,
        (byte) 165,
        (byte) 124,
        (byte) 93,
        (byte) 21,
        (byte) 58,
        (byte) 179,
        (byte) 186,
        (byte) 15,
        (byte) 192 /*0xC0*/,
        (byte) 69,
        (byte) 83,
        (byte) 181,
        (byte) 230,
        (byte) 148,
        (byte) 246,
        (byte) 207,
        (byte) 238,
        (byte) 250,
        (byte) 231,
        (byte) 196,
        (byte) 212,
        (byte) 214,
        (byte) 163,
        (byte) 216,
        (byte) 138
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 64 /*0x40*/,
        (byte) 75,
        (byte) 42,
        (byte) 217,
        (byte) 141,
        (byte) 137,
        (byte) 1,
        (byte) 167,
        (byte) 242,
        (byte) 173,
        (byte) 46,
        (byte) 108,
        (byte) 69,
        (byte) 89,
        (byte) 71,
        (byte) 92,
        (byte) 41,
        (byte) 238,
        (byte) 249,
        (byte) 95,
        (byte) 118,
        (byte) 228,
        (byte) 215,
        (byte) 251,
        (byte) 35,
        (byte) 33,
        (byte) 35,
        (byte) 164,
        (byte) 27,
        (byte) 27,
        (byte) 182,
        (byte) 105,
        (byte) 100,
        (byte) 238,
        (byte) 51,
        (byte) 35,
        (byte) 201,
        (byte) 229,
        (byte) 87,
        (byte) 200,
        (byte) 235,
        (byte) 82,
        (byte) 138,
        (byte) 38,
        (byte) 128 /*0x80*/,
        (byte) 86,
        (byte) 212,
        (byte) 175,
        (byte) 128 /*0x80*/,
        (byte) 56,
        (byte) 239,
        (byte) 30,
        (byte) 182,
        (byte) 201,
        (byte) 30
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[7]
      {
        (byte) 222,
        (byte) 6,
        (byte) 219,
        (byte) 172,
        (byte) 51,
        (byte) 249,
        (byte) 89
      };
      byte[] numArray7 = new byte[7]
      {
        (byte) 55,
        (byte) 181,
        (byte) 29,
        (byte) 4,
        (byte) 250,
        (byte) 114,
        (byte) 188
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_12586.sspq, 416, (Array) numArray8, 0, 53);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12586.sspr, 416, (Array) numArray8, 0, 53);
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
    byte[] numArray9 = new byte[117];
    byte[] numArray10 = new byte[55]
    {
      (byte) 137,
      (byte) 223,
      (byte) 64 /*0x40*/,
      (byte) 51,
      (byte) 158,
      (byte) 95,
      (byte) 56,
      (byte) 94,
      (byte) 81,
      (byte) 27,
      (byte) 202,
      (byte) 4,
      (byte) 249,
      (byte) 3,
      (byte) 132,
      (byte) 117,
      (byte) 83,
      (byte) 70,
      (byte) 146,
      (byte) 22,
      (byte) 45,
      (byte) 30,
      (byte) 216,
      (byte) 142,
      (byte) 9,
      (byte) 184,
      (byte) 198,
      (byte) 16 /*0x10*/,
      (byte) 99,
      (byte) 216,
      (byte) 82,
      (byte) 50,
      (byte) 56,
      (byte) 100,
      (byte) 54,
      (byte) 164,
      (byte) 192 /*0xC0*/,
      (byte) 101,
      (byte) 172,
      (byte) 11,
      (byte) 127 /*0x7F*/,
      (byte) 77,
      (byte) 21,
      (byte) 151,
      (byte) 95,
      (byte) 14,
      (byte) 41,
      (byte) 63 /*0x3F*/,
      (byte) 79,
      (byte) 191,
      (byte) 244,
      (byte) 33,
      (byte) 200,
      (byte) 47,
      (byte) 47
    };
    byte[] numArray11 = new byte[55]
    {
      (byte) 18,
      (byte) 127 /*0x7F*/,
      (byte) 102,
      (byte) 186,
      (byte) 38,
      (byte) 90,
      (byte) 36,
      (byte) 218,
      (byte) 83,
      (byte) 228,
      (byte) 109,
      (byte) 233,
      (byte) 176 /*0xB0*/,
      (byte) 54,
      (byte) 164,
      (byte) 53,
      (byte) 188,
      (byte) 20,
      (byte) 168,
      (byte) 192 /*0xC0*/,
      (byte) 136,
      (byte) 141,
      (byte) 20,
      (byte) 249,
      (byte) 124,
      (byte) 234,
      (byte) 232,
      (byte) 20,
      (byte) 84,
      (byte) 42,
      (byte) 113,
      (byte) 6,
      (byte) 57,
      (byte) 214,
      (byte) 107,
      (byte) 213,
      (byte) 176 /*0xB0*/,
      (byte) 9,
      (byte) 254,
      (byte) 154,
      (byte) 145,
      (byte) 78,
      (byte) 160 /*0xA0*/,
      (byte) 234,
      (byte) 200,
      (byte) 177,
      (byte) 15,
      (byte) 222,
      (byte) 225,
      (byte) 122,
      (byte) 19,
      (byte) 179,
      (byte) 2,
      (byte) 248,
      (byte) 120
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 178,
      (byte) 41,
      (byte) 230,
      (byte) 152,
      (byte) 40,
      (byte) 250,
      (byte) 120,
      (byte) 200,
      (byte) 151,
      (byte) 204,
      (byte) 149,
      (byte) 204,
      (byte) 224 /*0xE0*/,
      (byte) 181,
      (byte) 238,
      (byte) 154,
      (byte) 134,
      (byte) 150,
      (byte) 223,
      (byte) 8,
      (byte) 64 /*0x40*/,
      (byte) 46,
      (byte) 251,
      (byte) 142,
      (byte) 135,
      (byte) 192 /*0xC0*/,
      (byte) 163,
      (byte) 228,
      (byte) 49,
      (byte) 106,
      (byte) 9,
      (byte) 52,
      (byte) 203,
      (byte) 156,
      (byte) 14,
      (byte) 59,
      (byte) 197,
      (byte) 115,
      (byte) 87,
      (byte) 82,
      (byte) 156,
      (byte) 206,
      (byte) 200,
      (byte) 66,
      (byte) 147,
      (byte) 115,
      (byte) 111,
      (byte) 10,
      (byte) 6,
      (byte) 134,
      (byte) 65,
      (byte) 152,
      (byte) 79,
      (byte) 98,
      (byte) 198
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 30,
      (byte) 218,
      (byte) 124,
      (byte) 153,
      (byte) 140,
      (byte) 29,
      (byte) 61,
      (byte) 204,
      (byte) 73,
      (byte) 71,
      (byte) 90,
      (byte) 3,
      (byte) 93,
      (byte) 47,
      (byte) 185,
      (byte) 175,
      (byte) 81,
      (byte) 47,
      (byte) 225,
      (byte) 111,
      (byte) 166,
      (byte) 115,
      (byte) 216,
      (byte) 110,
      (byte) 183,
      (byte) 203,
      (byte) 17,
      (byte) 112 /*0x70*/,
      (byte) 68,
      (byte) 135,
      (byte) 143,
      (byte) 170,
      (byte) 199,
      (byte) 120,
      (byte) 199,
      (byte) 113,
      (byte) 107,
      (byte) 249,
      (byte) 106,
      (byte) 84,
      (byte) 39,
      (byte) 144 /*0x90*/,
      (byte) 174,
      (byte) 243,
      (byte) 123,
      (byte) 202,
      (byte) 165,
      (byte) 215,
      (byte) 175,
      (byte) 2,
      (byte) 189,
      (byte) 77,
      (byte) 120,
      (byte) 172,
      (byte) 140
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[7]
    {
      (byte) 87,
      (byte) 17,
      (byte) 104,
      (byte) 86,
      (byte) 148,
      (byte) 151,
      (byte) 45
    };
    byte[] numArray15 = new byte[7]
    {
      (byte) 16 /*0x10*/,
      (byte) 13,
      (byte) 198,
      (byte) 109,
      (byte) 254,
      (byte) 110,
      (byte) 189
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 7);
    for (int index = 0; index < 7; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12626()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[1] = (byte) 178;
      numArray2[3] = (byte) 19;
      numArray2[5] = (byte) 141;
      numArray2[6] = (byte) 108;
      numArray2[4] = (byte) 63 /*0x3F*/;
      numArray2[8] = (byte) 102;
      numArray2[0] = (byte) 167;
      numArray2[7] = (byte) 114;
      numArray2[2] = (byte) 213;
      byte[] numArray3 = new byte[9]
      {
        (byte) 135,
        (byte) 28,
        (byte) 96 /*0x60*/,
        (byte) 78,
        (byte) 139,
        (byte) 155,
        (byte) 118,
        (byte) 251,
        (byte) 153
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[3] = (byte) 97;
    numArray5[1] = (byte) 12;
    numArray5[2] = (byte) 109;
    numArray5[0] = (byte) 138;
    numArray5[4] = (byte) 239;
    numArray5[7] = (byte) 55;
    numArray5[6] = (byte) 7;
    numArray5[5] = (byte) 251;
    numArray5[8] = (byte) 154;
    byte[] numArray6 = new byte[9]
    {
      (byte) 174,
      (byte) 47,
      (byte) 175,
      (byte) 159,
      (byte) 237,
      (byte) 7,
      (byte) 68,
      (byte) 176 /*0xB0*/,
      (byte) 0
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12627()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[74];
      byte[] numArray2 = new byte[55]
      {
        (byte) 117,
        (byte) 93,
        (byte) 253,
        (byte) 23,
        (byte) 189,
        (byte) 183,
        (byte) 235,
        (byte) 4,
        (byte) 81,
        (byte) 123,
        (byte) 153,
        (byte) 108,
        (byte) 28,
        (byte) 165,
        (byte) 103,
        (byte) 178,
        (byte) 80 /*0x50*/,
        (byte) 146,
        (byte) 100,
        (byte) 59,
        (byte) 203,
        (byte) 57,
        (byte) 199,
        (byte) 162,
        (byte) 35,
        (byte) 54,
        (byte) 137,
        (byte) 250,
        (byte) 251,
        (byte) 24,
        (byte) 35,
        (byte) 113,
        (byte) 130,
        (byte) 109,
        (byte) 151,
        (byte) 16 /*0x10*/,
        (byte) 100,
        (byte) 215,
        (byte) 62,
        (byte) 80 /*0x50*/,
        (byte) 75,
        (byte) 150,
        (byte) 46,
        (byte) 184,
        (byte) 216,
        (byte) 191,
        (byte) 70,
        (byte) 174,
        (byte) 73,
        (byte) 123,
        (byte) 8,
        (byte) 47,
        (byte) 146,
        (byte) 24,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[49] = (byte) 244;
      numArray3[1] = (byte) 120;
      numArray3[27] = (byte) 239;
      numArray3[18] = (byte) 70;
      numArray3[29] = (byte) 48 /*0x30*/;
      numArray3[36] = (byte) 8;
      numArray3[11] = (byte) 186;
      numArray3[17] = (byte) 90;
      numArray3[0] = (byte) 126;
      numArray3[8] = (byte) 15;
      numArray3[39] = (byte) 104;
      numArray3[44] = (byte) 237;
      numArray3[9] = (byte) 184;
      numArray3[13] = (byte) 13;
      numArray3[7] = (byte) 54;
      numArray3[15] = (byte) 130;
      numArray3[2] = (byte) 167;
      numArray3[37] = (byte) 236;
      numArray3[54] = (byte) 84;
      numArray3[34] = (byte) 80 /*0x50*/;
      numArray3[32 /*0x20*/] = (byte) 174;
      numArray3[19] = (byte) 67;
      numArray3[22] = (byte) 215;
      numArray3[28] = (byte) 232;
      numArray3[24] = (byte) 8;
      numArray3[47] = (byte) 119;
      numArray3[26] = (byte) 19;
      numArray3[25] = (byte) 103;
      numArray3[43] = (byte) 146;
      numArray3[21] = (byte) 139;
      numArray3[51] = (byte) 106;
      numArray3[31 /*0x1F*/] = (byte) 163;
      numArray3[23] = (byte) 121;
      numArray3[33] = (byte) 113;
      numArray3[40] = (byte) 188;
      numArray3[20] = (byte) 131;
      numArray3[14] = (byte) 82;
      numArray3[5] = (byte) 113;
      numArray3[12] = (byte) 198;
      numArray3[46] = (byte) 111;
      numArray3[35] = (byte) 53;
      numArray3[41] = (byte) 113;
      numArray3[4] = (byte) 171;
      numArray3[38] = (byte) 237;
      numArray3[42] = (byte) 253;
      numArray3[45] = (byte) 63 /*0x3F*/;
      numArray3[10] = (byte) 74;
      numArray3[3] = (byte) 221;
      numArray3[48 /*0x30*/] = (byte) 247;
      numArray3[6] = (byte) 71;
      numArray3[50] = (byte) 234;
      numArray3[16 /*0x10*/] = (byte) 191;
      numArray3[52] = (byte) 190;
      numArray3[53] = (byte) 160 /*0xA0*/;
      numArray3[30] = (byte) 80 /*0x50*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[19]
      {
        (byte) 146,
        (byte) 143,
        (byte) 150,
        (byte) 81,
        (byte) 64 /*0x40*/,
        (byte) 137,
        (byte) 39,
        (byte) 100,
        (byte) 71,
        (byte) 129,
        (byte) 145,
        (byte) 178,
        (byte) 220,
        (byte) 69,
        (byte) 148,
        (byte) 156,
        (byte) 240 /*0xF0*/,
        (byte) 150,
        (byte) 168
      };
      byte[] numArray5 = new byte[19]
      {
        (byte) 39,
        (byte) 50,
        (byte) 52,
        (byte) 29,
        (byte) 91,
        (byte) 49,
        (byte) 194,
        (byte) 133,
        (byte) 32 /*0x20*/,
        (byte) 153,
        (byte) 196,
        (byte) 115,
        (byte) 77,
        (byte) 103,
        (byte) 150,
        (byte) 11,
        (byte) 88,
        (byte) 234,
        (byte) 176 /*0xB0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[74];
    byte[] numArray7 = new byte[55]
    {
      (byte) 143,
      (byte) 87,
      (byte) 59,
      (byte) 190,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 23,
      (byte) 97,
      (byte) 64 /*0x40*/,
      (byte) 1,
      (byte) 41,
      (byte) 244,
      (byte) 118,
      (byte) 84,
      (byte) 33,
      (byte) 209,
      (byte) 24,
      (byte) 3,
      (byte) 206,
      (byte) 251,
      (byte) 106,
      (byte) 83,
      (byte) 7,
      (byte) 237,
      (byte) 21,
      (byte) 232,
      (byte) 251,
      (byte) 63 /*0x3F*/,
      (byte) 142,
      (byte) 146,
      (byte) 114,
      (byte) 28,
      (byte) 215,
      (byte) 122,
      (byte) 0,
      (byte) 62,
      (byte) 22,
      (byte) 129,
      (byte) 55,
      (byte) 166,
      (byte) 76,
      (byte) 229,
      (byte) 64 /*0x40*/,
      (byte) 143,
      (byte) 137,
      (byte) 231,
      (byte) 206,
      (byte) 63 /*0x3F*/,
      (byte) 122,
      (byte) 30,
      (byte) 125,
      (byte) 165,
      (byte) 69,
      (byte) 180,
      (byte) 173
    };
    byte[] numArray8 = new byte[55];
    numArray8[12] = (byte) 22;
    numArray8[31 /*0x1F*/] = (byte) 186;
    numArray8[38] = (byte) 50;
    numArray8[3] = (byte) 49;
    numArray8[4] = (byte) 125;
    numArray8[16 /*0x10*/] = (byte) 115;
    numArray8[50] = (byte) 95;
    numArray8[7] = (byte) 30;
    numArray8[8] = (byte) 198;
    numArray8[9] = (byte) 186;
    numArray8[15] = (byte) 20;
    numArray8[11] = (byte) 219;
    numArray8[34] = (byte) 26;
    numArray8[23] = (byte) 121;
    numArray8[2] = (byte) 35;
    numArray8[0] = (byte) 143;
    numArray8[19] = (byte) 8;
    numArray8[17] = (byte) 25;
    numArray8[25] = (byte) 81;
    numArray8[14] = (byte) 239;
    numArray8[20] = (byte) 241;
    numArray8[36] = (byte) 190;
    numArray8[5] = (byte) 69;
    numArray8[21] = (byte) 171;
    numArray8[43] = (byte) 207;
    numArray8[10] = (byte) 173;
    numArray8[53] = (byte) 82;
    numArray8[27] = (byte) 26;
    numArray8[28] = (byte) 33;
    numArray8[46] = (byte) 127 /*0x7F*/;
    numArray8[30] = (byte) 199;
    numArray8[52] = (byte) 12;
    numArray8[32 /*0x20*/] = (byte) 235;
    numArray8[33] = (byte) 177;
    numArray8[26] = (byte) 189;
    numArray8[49] = (byte) 102;
    numArray8[39] = (byte) 164;
    numArray8[37] = (byte) 92;
    numArray8[29] = (byte) 172;
    numArray8[44] = (byte) 10;
    numArray8[51] = (byte) 6;
    numArray8[41] = (byte) 129;
    numArray8[42] = (byte) 181;
    numArray8[1] = (byte) 236;
    numArray8[6] = (byte) 71;
    numArray8[45] = (byte) 213;
    numArray8[18] = (byte) 252;
    numArray8[47] = (byte) 26;
    numArray8[48 /*0x30*/] = (byte) 208 /*0xD0*/;
    numArray8[13] = (byte) 67;
    numArray8[22] = (byte) 45;
    numArray8[40] = (byte) 172;
    numArray8[24] = (byte) 75;
    numArray8[35] = (byte) 247;
    numArray8[54] = (byte) 163;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[19]
    {
      (byte) 234,
      (byte) 122,
      (byte) 79,
      (byte) 201,
      (byte) 19,
      (byte) 99,
      (byte) 153,
      (byte) 194,
      (byte) 54,
      (byte) 249,
      (byte) 47,
      (byte) 143,
      (byte) 245,
      (byte) 243,
      (byte) 190,
      (byte) 254,
      (byte) 6,
      (byte) 57,
      (byte) 113
    };
    byte[] numArray10 = new byte[19];
    numArray10[1] = (byte) 44;
    numArray10[18] = (byte) 87;
    numArray10[10] = (byte) 0;
    numArray10[6] = (byte) 124;
    numArray10[4] = (byte) 90;
    numArray10[5] = (byte) 40;
    numArray10[13] = (byte) 132;
    numArray10[7] = (byte) 175;
    numArray10[8] = (byte) 87;
    numArray10[9] = (byte) 140;
    numArray10[11] = (byte) 160 /*0xA0*/;
    numArray10[16 /*0x10*/] = (byte) 244;
    numArray10[12] = (byte) 96 /*0x60*/;
    numArray10[3] = (byte) 96 /*0x60*/;
    numArray10[14] = (byte) 189;
    numArray10[0] = (byte) 213;
    numArray10[2] = (byte) 183;
    numArray10[17] = (byte) 35;
    numArray10[15] = (byte) 126;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 19);
    for (int index = 0; index < 19; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12628()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55];
      numArray2[38] = (byte) 138;
      numArray2[1] = (byte) 209;
      numArray2[40] = (byte) 4;
      numArray2[3] = (byte) 91;
      numArray2[33] = (byte) 57;
      numArray2[5] = (byte) 111;
      numArray2[6] = (byte) 40;
      numArray2[7] = (byte) 217;
      numArray2[44] = (byte) 235;
      numArray2[2] = (byte) 162;
      numArray2[10] = (byte) 184;
      numArray2[32 /*0x20*/] = (byte) 135;
      numArray2[12] = (byte) 97;
      numArray2[13] = (byte) 2;
      numArray2[19] = (byte) 108;
      numArray2[15] = (byte) 8;
      numArray2[9] = (byte) 204;
      numArray2[25] = (byte) 224 /*0xE0*/;
      numArray2[50] = (byte) 94;
      numArray2[49] = (byte) 117;
      numArray2[17] = (byte) 230;
      numArray2[21] = (byte) 125;
      numArray2[22] = (byte) 220;
      numArray2[23] = (byte) 140;
      numArray2[26] = (byte) 21;
      numArray2[14] = (byte) 23;
      numArray2[11] = (byte) 168;
      numArray2[36] = (byte) 164;
      numArray2[43] = (byte) 81;
      numArray2[29] = (byte) 174;
      numArray2[0] = (byte) 70;
      numArray2[27] = (byte) 96 /*0x60*/;
      numArray2[18] = (byte) 86;
      numArray2[30] = (byte) 216;
      numArray2[34] = (byte) 161;
      numArray2[35] = (byte) 145;
      numArray2[20] = (byte) 205;
      numArray2[37] = (byte) 116;
      numArray2[47] = (byte) 219;
      numArray2[39] = (byte) 157;
      numArray2[42] = (byte) 160 /*0xA0*/;
      numArray2[41] = (byte) 27;
      numArray2[52] = (byte) 88;
      numArray2[16 /*0x10*/] = (byte) 249;
      numArray2[54] = (byte) 112 /*0x70*/;
      numArray2[45] = (byte) 244;
      numArray2[46] = (byte) 179;
      numArray2[24] = (byte) 242;
      numArray2[48 /*0x30*/] = (byte) 70;
      numArray2[8] = (byte) 24;
      numArray2[4] = (byte) 162;
      numArray2[51] = (byte) 122;
      numArray2[28] = (byte) 129;
      numArray2[53] = (byte) 140;
      numArray2[31 /*0x1F*/] = (byte) 194;
      byte[] numArray3 = new byte[55]
      {
        (byte) 7,
        (byte) 5,
        (byte) 13,
        (byte) 244,
        (byte) 158,
        (byte) 80 /*0x50*/,
        (byte) 248,
        (byte) 3,
        (byte) 27,
        (byte) 99,
        (byte) 127 /*0x7F*/,
        (byte) 253,
        (byte) 169,
        (byte) 209,
        (byte) 71,
        (byte) 208 /*0xD0*/,
        (byte) 216,
        (byte) 176 /*0xB0*/,
        (byte) 155,
        (byte) 254,
        (byte) 245,
        (byte) 83,
        (byte) 16 /*0x10*/,
        (byte) 112 /*0x70*/,
        (byte) 159,
        (byte) 12,
        (byte) 51,
        (byte) 117,
        (byte) 196,
        (byte) 151,
        (byte) 205,
        (byte) 239,
        (byte) 57,
        (byte) 179,
        (byte) 251,
        (byte) 177,
        (byte) 21,
        (byte) 167,
        (byte) 71,
        (byte) 89,
        (byte) 245,
        (byte) 81,
        (byte) 166,
        (byte) 167,
        (byte) 207,
        (byte) 31 /*0x1F*/,
        (byte) 177,
        (byte) 185,
        (byte) 210,
        (byte) 60,
        (byte) 36,
        (byte) 43,
        (byte) 196,
        (byte) 249,
        (byte) 135
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18]
      {
        (byte) 117,
        (byte) 107,
        (byte) 147,
        (byte) 31 /*0x1F*/,
        (byte) 174,
        (byte) 76,
        (byte) 27,
        (byte) 148,
        (byte) 139,
        (byte) 36,
        (byte) 94,
        (byte) 136,
        (byte) 232,
        (byte) 212,
        (byte) 120,
        (byte) 146,
        (byte) 118,
        (byte) 43
      };
      byte[] numArray5 = new byte[18];
      numArray5[0] = (byte) 28;
      numArray5[7] = (byte) 153;
      numArray5[2] = (byte) 123;
      numArray5[14] = (byte) 80 /*0x50*/;
      numArray5[10] = (byte) 206;
      numArray5[5] = (byte) 240 /*0xF0*/;
      numArray5[8] = (byte) 27;
      numArray5[11] = (byte) 24;
      numArray5[4] = (byte) 56;
      numArray5[13] = (byte) 137;
      numArray5[6] = (byte) 149;
      numArray5[16 /*0x10*/] = (byte) 70;
      numArray5[1] = (byte) 252;
      numArray5[12] = (byte) 231;
      numArray5[9] = (byte) 88;
      numArray5[15] = (byte) 109;
      numArray5[3] = (byte) 20;
      numArray5[17] = (byte) 132;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55]
    {
      (byte) 117,
      (byte) 27,
      (byte) 201,
      (byte) 105,
      (byte) 175,
      (byte) 45,
      (byte) 241,
      (byte) 215,
      (byte) 61,
      (byte) 97,
      (byte) 140,
      (byte) 39,
      (byte) 249,
      (byte) 207,
      (byte) 68,
      (byte) 59,
      (byte) 200,
      (byte) 65,
      (byte) 242,
      (byte) 94,
      (byte) 84,
      (byte) 83,
      (byte) 166,
      (byte) 84,
      (byte) 191,
      (byte) 77,
      (byte) 83,
      (byte) 155,
      (byte) 180,
      (byte) 187,
      (byte) 216,
      (byte) 92,
      (byte) 82,
      (byte) 157,
      (byte) 121,
      (byte) 139,
      (byte) 100,
      (byte) 66,
      (byte) 121,
      (byte) 116,
      (byte) 206,
      (byte) 130,
      (byte) 187,
      (byte) 18,
      (byte) 179,
      (byte) 164,
      (byte) 220,
      (byte) 58,
      (byte) 168,
      (byte) 248,
      (byte) 80 /*0x50*/,
      (byte) 94,
      (byte) 67,
      (byte) 197,
      (byte) 56
    };
    byte[] numArray8 = new byte[55];
    numArray8[42] = (byte) 83;
    numArray8[49] = (byte) 155;
    numArray8[2] = (byte) 232;
    numArray8[24] = (byte) 117;
    numArray8[0] = (byte) 19;
    numArray8[33] = (byte) 149;
    numArray8[44] = (byte) 236;
    numArray8[23] = (byte) 142;
    numArray8[8] = (byte) 26;
    numArray8[1] = (byte) 126;
    numArray8[40] = (byte) 182;
    numArray8[11] = (byte) 170;
    numArray8[12] = (byte) 170;
    numArray8[13] = (byte) 254;
    numArray8[14] = (byte) 35;
    numArray8[36] = (byte) 228;
    numArray8[3] = (byte) 206;
    numArray8[17] = (byte) 24;
    numArray8[51] = (byte) 242;
    numArray8[19] = (byte) 49;
    numArray8[20] = (byte) 31 /*0x1F*/;
    numArray8[32 /*0x20*/] = (byte) 69;
    numArray8[22] = (byte) 242;
    numArray8[31 /*0x1F*/] = (byte) 241;
    numArray8[9] = (byte) 240 /*0xF0*/;
    numArray8[25] = (byte) 194;
    numArray8[7] = (byte) 172;
    numArray8[27] = (byte) 76;
    numArray8[28] = (byte) 127 /*0x7F*/;
    numArray8[29] = byte.MaxValue;
    numArray8[30] = (byte) 159;
    numArray8[18] = (byte) 90;
    numArray8[53] = (byte) 35;
    numArray8[26] = (byte) 82;
    numArray8[35] = (byte) 105;
    numArray8[47] = (byte) 50;
    numArray8[41] = (byte) 155;
    numArray8[37] = (byte) 190;
    numArray8[38] = (byte) 94;
    numArray8[39] = (byte) 135;
    numArray8[21] = (byte) 174;
    numArray8[16 /*0x10*/] = (byte) 57;
    numArray8[4] = (byte) 241;
    numArray8[15] = (byte) 96 /*0x60*/;
    numArray8[10] = (byte) 167;
    numArray8[34] = (byte) 163;
    numArray8[54] = (byte) 213;
    numArray8[46] = (byte) 17;
    numArray8[6] = (byte) 37;
    numArray8[43] = (byte) 202;
    numArray8[50] = (byte) 21;
    numArray8[48 /*0x30*/] = (byte) 228;
    numArray8[52] = (byte) 231;
    numArray8[5] = (byte) 74;
    numArray8[45] = (byte) 34;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18]
    {
      (byte) 211,
      (byte) 143,
      (byte) 240 /*0xF0*/,
      (byte) 192 /*0xC0*/,
      (byte) 98,
      (byte) 103,
      (byte) 23,
      (byte) 15,
      (byte) 22,
      (byte) 71,
      (byte) 247,
      byte.MaxValue,
      (byte) 161,
      (byte) 231,
      (byte) 238,
      (byte) 175,
      (byte) 227,
      (byte) 23
    };
    byte[] numArray10 = new byte[18]
    {
      (byte) 155,
      (byte) 164,
      (byte) 165,
      (byte) 107,
      (byte) 56,
      (byte) 180,
      (byte) 101,
      (byte) 8,
      (byte) 148,
      (byte) 169,
      (byte) 98,
      (byte) 242,
      (byte) 110,
      (byte) 20,
      (byte) 5,
      (byte) 166,
      (byte) 145,
      (byte) 113
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12629()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 75,
        (byte) 240 /*0xF0*/,
        (byte) 52,
        (byte) 4,
        (byte) 154,
        (byte) 160 /*0xA0*/,
        (byte) 226,
        (byte) 83,
        (byte) 88
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 3,
        (byte) 57,
        (byte) 46,
        (byte) 245,
        (byte) 142,
        (byte) 191,
        (byte) 149,
        (byte) 175,
        (byte) 241
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[1] = (byte) 9;
    numArray5[6] = (byte) 34;
    numArray5[4] = (byte) 22;
    numArray5[3] = (byte) 218;
    numArray5[0] = (byte) 28;
    numArray5[5] = (byte) 246;
    numArray5[2] = (byte) 38;
    numArray5[7] = (byte) 122;
    numArray5[8] = (byte) 108;
    byte[] numArray6 = new byte[9]
    {
      (byte) 169,
      (byte) 148,
      (byte) 6,
      (byte) 199,
      (byte) 251,
      (byte) 31 /*0x1F*/,
      (byte) 145,
      (byte) 237,
      (byte) 191
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12630()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[72];
      byte[] numArray2 = new byte[55];
      numArray2[5] = (byte) 118;
      numArray2[1] = (byte) 0;
      numArray2[2] = (byte) 139;
      numArray2[54] = (byte) 3;
      numArray2[9] = (byte) 247;
      numArray2[11] = (byte) 107;
      numArray2[12] = (byte) 202;
      numArray2[7] = (byte) 195;
      numArray2[4] = (byte) 204;
      numArray2[50] = (byte) 29;
      numArray2[3] = (byte) 94;
      numArray2[43] = (byte) 145;
      numArray2[41] = (byte) 168;
      numArray2[13] = (byte) 229;
      numArray2[28] = (byte) 113;
      numArray2[15] = (byte) 8;
      numArray2[16 /*0x10*/] = (byte) 1;
      numArray2[48 /*0x30*/] = (byte) 94;
      numArray2[18] = (byte) 59;
      numArray2[19] = (byte) 143;
      numArray2[25] = (byte) 97;
      numArray2[21] = (byte) 194;
      numArray2[8] = (byte) 238;
      numArray2[40] = (byte) 77;
      numArray2[47] = (byte) 246;
      numArray2[6] = (byte) 45;
      numArray2[34] = (byte) 197;
      numArray2[27] = (byte) 250;
      numArray2[38] = (byte) 42;
      numArray2[0] = (byte) 67;
      numArray2[33] = (byte) 103;
      numArray2[46] = (byte) 110;
      numArray2[32 /*0x20*/] = (byte) 94;
      numArray2[14] = (byte) 136;
      numArray2[23] = (byte) 79;
      numArray2[35] = (byte) 38;
      numArray2[36] = (byte) 78;
      numArray2[37] = (byte) 183;
      numArray2[10] = (byte) 50;
      numArray2[39] = (byte) 172;
      numArray2[20] = (byte) 177;
      numArray2[53] = (byte) 71;
      numArray2[42] = (byte) 153;
      numArray2[26] = (byte) 197;
      numArray2[44] = (byte) 178;
      numArray2[45] = (byte) 225;
      numArray2[29] = (byte) 58;
      numArray2[52] = (byte) 239;
      numArray2[17] = (byte) 125;
      numArray2[49] = (byte) 217;
      numArray2[31 /*0x1F*/] = (byte) 161;
      numArray2[22] = (byte) 102;
      numArray2[30] = (byte) 13;
      numArray2[24] = (byte) 182;
      numArray2[51] = (byte) 116;
      byte[] numArray3 = new byte[55]
      {
        (byte) 37,
        (byte) 102,
        (byte) 72,
        (byte) 147,
        (byte) 5,
        (byte) 205,
        (byte) 226,
        (byte) 35,
        (byte) 125,
        (byte) 22,
        (byte) 178,
        (byte) 32 /*0x20*/,
        (byte) 163,
        (byte) 83,
        (byte) 205,
        (byte) 206,
        (byte) 110,
        (byte) 137,
        (byte) 237,
        (byte) 176 /*0xB0*/,
        (byte) 139,
        (byte) 142,
        (byte) 108,
        (byte) 0,
        (byte) 119,
        (byte) 43,
        (byte) 3,
        (byte) 30,
        (byte) 77,
        (byte) 136,
        (byte) 120,
        (byte) 49,
        (byte) 227,
        (byte) 21,
        (byte) 219,
        (byte) 46,
        (byte) 220,
        (byte) 131,
        (byte) 206,
        (byte) 170,
        (byte) 147,
        (byte) 11,
        (byte) 27,
        (byte) 147,
        (byte) 186,
        (byte) 32 /*0x20*/,
        (byte) 156,
        (byte) 229,
        (byte) 211,
        (byte) 243,
        (byte) 61,
        (byte) 109,
        (byte) 122,
        (byte) 231,
        (byte) 253
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17]
      {
        (byte) 158,
        (byte) 210,
        (byte) 119,
        (byte) 226,
        (byte) 150,
        (byte) 186,
        (byte) 151,
        (byte) 81,
        (byte) 222,
        (byte) 246,
        (byte) 254,
        (byte) 218,
        (byte) 213,
        (byte) 167,
        (byte) 57,
        (byte) 147,
        (byte) 148
      };
      byte[] numArray5 = new byte[17]
      {
        (byte) 64 /*0x40*/,
        (byte) 237,
        (byte) 125,
        (byte) 17,
        byte.MaxValue,
        (byte) 183,
        (byte) 139,
        (byte) 37,
        (byte) 217,
        (byte) 217,
        (byte) 168,
        (byte) 87,
        (byte) 212,
        (byte) 51,
        (byte) 142,
        (byte) 164,
        (byte) 33
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[17];
      byte[] response = new byte[17];
      Array.Copy((Array) sc_12586.sspq, 469, (Array) numArray6, 0, 17);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12586.sspr, 469, (Array) numArray6, 0, 17);
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
    byte[] numArray7 = new byte[72];
    byte[] numArray8 = new byte[55]
    {
      (byte) 132,
      (byte) 155,
      (byte) 77,
      (byte) 85,
      (byte) 68,
      (byte) 222,
      (byte) 13,
      (byte) 252,
      (byte) 38,
      (byte) 120,
      (byte) 154,
      (byte) 141,
      (byte) 203,
      (byte) 222,
      (byte) 71,
      (byte) 20,
      (byte) 150,
      (byte) 36,
      (byte) 209,
      (byte) 142,
      (byte) 234,
      (byte) 246,
      (byte) 67,
      (byte) 78,
      (byte) 218,
      (byte) 140,
      (byte) 229,
      (byte) 147,
      (byte) 238,
      (byte) 12,
      (byte) 51,
      (byte) 251,
      (byte) 75,
      (byte) 123,
      (byte) 163,
      (byte) 22,
      (byte) 246,
      (byte) 216,
      (byte) 54,
      (byte) 33,
      (byte) 219,
      (byte) 251,
      (byte) 121,
      byte.MaxValue,
      (byte) 175,
      (byte) 233,
      (byte) 134,
      (byte) 50,
      (byte) 34,
      (byte) 216,
      (byte) 50,
      (byte) 79,
      (byte) 107,
      (byte) 184,
      (byte) 46
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 203,
      (byte) 93,
      (byte) 177,
      (byte) 112 /*0x70*/,
      (byte) 169,
      (byte) 23,
      (byte) 113,
      (byte) 210,
      (byte) 230,
      (byte) 116,
      (byte) 107,
      (byte) 157,
      (byte) 208 /*0xD0*/,
      (byte) 228,
      (byte) 159,
      (byte) 167,
      (byte) 33,
      (byte) 228,
      (byte) 181,
      (byte) 239,
      (byte) 249,
      (byte) 252,
      (byte) 104,
      (byte) 8,
      (byte) 89,
      (byte) 243,
      (byte) 64 /*0x40*/,
      (byte) 60,
      (byte) 142,
      (byte) 46,
      (byte) 116,
      (byte) 63 /*0x3F*/,
      (byte) 85,
      (byte) 43,
      (byte) 134,
      (byte) 205,
      (byte) 98,
      (byte) 16 /*0x10*/,
      (byte) 193,
      (byte) 22,
      (byte) 212,
      (byte) 43,
      (byte) 58,
      (byte) 248,
      (byte) 9,
      (byte) 55,
      (byte) 145,
      (byte) 179,
      (byte) 246,
      (byte) 187,
      (byte) 86,
      (byte) 243,
      (byte) 49,
      (byte) 156,
      (byte) 243
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[17]
    {
      (byte) 254,
      (byte) 123,
      (byte) 183,
      (byte) 249,
      (byte) 121,
      (byte) 13,
      (byte) 25,
      (byte) 68,
      (byte) 158,
      (byte) 66,
      (byte) 79,
      (byte) 59,
      (byte) 49,
      (byte) 29,
      (byte) 189,
      (byte) 238,
      (byte) 176 /*0xB0*/
    };
    byte[] numArray11 = new byte[17];
    numArray11[12] = (byte) 193;
    numArray11[1] = (byte) 28;
    numArray11[2] = (byte) 206;
    numArray11[3] = (byte) 159;
    numArray11[15] = (byte) 24;
    numArray11[0] = (byte) 203;
    numArray11[6] = (byte) 109;
    numArray11[4] = (byte) 219;
    numArray11[8] = (byte) 183;
    numArray11[9] = (byte) 219;
    numArray11[10] = (byte) 73;
    numArray11[11] = (byte) 163;
    numArray11[5] = (byte) 86;
    numArray11[16 /*0x10*/] = (byte) 132;
    numArray11[14] = (byte) 45;
    numArray11[13] = (byte) 31 /*0x1F*/;
    numArray11[7] = (byte) 35;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 17);
    for (int index = 0; index < 17; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12631()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[189];
      byte[] numArray2 = new byte[55]
      {
        (byte) 97,
        (byte) 237,
        (byte) 95,
        (byte) 187,
        (byte) 16 /*0x10*/,
        (byte) 167,
        (byte) 54,
        (byte) 247,
        (byte) 241,
        (byte) 186,
        (byte) 117,
        (byte) 97,
        (byte) 187,
        (byte) 5,
        (byte) 62,
        (byte) 17,
        (byte) 21,
        (byte) 95,
        (byte) 139,
        (byte) 178,
        (byte) 71,
        (byte) 8,
        (byte) 247,
        (byte) 63 /*0x3F*/,
        (byte) 96 /*0x60*/,
        (byte) 14,
        (byte) 175,
        (byte) 2,
        (byte) 38,
        (byte) 114,
        (byte) 38,
        (byte) 96 /*0x60*/,
        (byte) 111,
        (byte) 136,
        (byte) 81,
        (byte) 2,
        (byte) 29,
        (byte) 136,
        (byte) 142,
        (byte) 72,
        (byte) 206,
        (byte) 186,
        (byte) 127 /*0x7F*/,
        (byte) 91,
        (byte) 101,
        (byte) 130,
        (byte) 212,
        (byte) 141,
        (byte) 15,
        (byte) 3,
        (byte) 168,
        (byte) 152,
        (byte) 209,
        (byte) 203,
        (byte) 224 /*0xE0*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 82,
        (byte) 177,
        (byte) 0,
        (byte) 47,
        (byte) 187,
        (byte) 0,
        (byte) 109,
        (byte) 189,
        (byte) 171,
        (byte) 215,
        (byte) 62,
        (byte) 13,
        (byte) 18,
        (byte) 117,
        (byte) 154,
        (byte) 73,
        (byte) 2,
        (byte) 250,
        (byte) 67,
        (byte) 93,
        (byte) 207,
        (byte) 96 /*0x60*/,
        (byte) 88,
        (byte) 142,
        (byte) 5,
        (byte) 191,
        (byte) 39,
        (byte) 13,
        (byte) 182,
        (byte) 18,
        (byte) 82,
        (byte) 64 /*0x40*/,
        (byte) 86,
        (byte) 143,
        (byte) 196,
        (byte) 28,
        (byte) 19,
        (byte) 212,
        (byte) 196,
        (byte) 169,
        (byte) 208 /*0xD0*/,
        (byte) 246,
        (byte) 158,
        (byte) 103,
        (byte) 145,
        (byte) 83,
        (byte) 234,
        (byte) 174,
        (byte) 130,
        (byte) 190,
        (byte) 185,
        (byte) 90,
        (byte) 6,
        (byte) 0,
        (byte) 8
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[44] = (byte) 160 /*0xA0*/;
      numArray4[1] = (byte) 90;
      numArray4[14] = (byte) 141;
      numArray4[52] = (byte) 84;
      numArray4[23] = (byte) 57;
      numArray4[5] = (byte) 209;
      numArray4[6] = (byte) 126;
      numArray4[7] = (byte) 148;
      numArray4[43] = (byte) 145;
      numArray4[34] = (byte) 252;
      numArray4[24] = (byte) 33;
      numArray4[8] = (byte) 20;
      numArray4[30] = (byte) 164;
      numArray4[13] = (byte) 244;
      numArray4[48 /*0x30*/] = (byte) 27;
      numArray4[9] = (byte) 241;
      numArray4[49] = (byte) 198;
      numArray4[53] = (byte) 207;
      numArray4[11] = (byte) 179;
      numArray4[19] = (byte) 137;
      numArray4[12] = (byte) 59;
      numArray4[21] = (byte) 218;
      numArray4[22] = (byte) 221;
      numArray4[2] = (byte) 2;
      numArray4[0] = (byte) 32 /*0x20*/;
      numArray4[25] = (byte) 243;
      numArray4[33] = (byte) 13;
      numArray4[27] = (byte) 201;
      numArray4[28] = (byte) 141;
      numArray4[29] = (byte) 143;
      numArray4[3] = (byte) 248;
      numArray4[31 /*0x1F*/] = (byte) 167;
      numArray4[32 /*0x20*/] = (byte) 248;
      numArray4[39] = (byte) 233;
      numArray4[40] = (byte) 0;
      numArray4[35] = (byte) 45;
      numArray4[36] = (byte) 139;
      numArray4[37] = (byte) 80 /*0x50*/;
      numArray4[45] = (byte) 217;
      numArray4[17] = (byte) 228;
      numArray4[10] = (byte) 51;
      numArray4[41] = (byte) 199;
      numArray4[42] = (byte) 81;
      numArray4[26] = (byte) 34;
      numArray4[18] = (byte) 140;
      numArray4[38] = (byte) 153;
      numArray4[46] = (byte) 211;
      numArray4[20] = (byte) 155;
      numArray4[16 /*0x10*/] = (byte) 66;
      numArray4[54] = (byte) 89;
      numArray4[50] = (byte) 143;
      numArray4[51] = (byte) 230;
      numArray4[4] = (byte) 52;
      numArray4[47] = (byte) 15;
      numArray4[15] = (byte) 209;
      byte[] numArray5 = new byte[55]
      {
        (byte) 12,
        (byte) 181,
        (byte) 183,
        (byte) 215,
        (byte) 59,
        (byte) 184,
        (byte) 180,
        (byte) 59,
        (byte) 251,
        (byte) 247,
        (byte) 112 /*0x70*/,
        (byte) 225,
        (byte) 131,
        (byte) 56,
        (byte) 76,
        (byte) 13,
        (byte) 177,
        (byte) 169,
        (byte) 253,
        (byte) 2,
        (byte) 54,
        (byte) 172,
        (byte) 149,
        (byte) 156,
        (byte) 247,
        (byte) 18,
        (byte) 14,
        (byte) 135,
        (byte) 5,
        (byte) 127 /*0x7F*/,
        (byte) 7,
        (byte) 111,
        (byte) 224 /*0xE0*/,
        (byte) 1,
        (byte) 34,
        (byte) 128 /*0x80*/,
        (byte) 175,
        (byte) 99,
        (byte) 108,
        (byte) 153,
        (byte) 168,
        (byte) 23,
        (byte) 153,
        (byte) 95,
        (byte) 131,
        (byte) 83,
        (byte) 57,
        (byte) 223,
        (byte) 134,
        (byte) 236,
        (byte) 76,
        (byte) 30,
        (byte) 198,
        (byte) 30,
        (byte) 85
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[32 /*0x20*/] = (byte) 250;
      numArray6[48 /*0x30*/] = (byte) 37;
      numArray6[45] = (byte) 215;
      numArray6[15] = (byte) 241;
      numArray6[4] = (byte) 6;
      numArray6[5] = (byte) 63 /*0x3F*/;
      numArray6[2] = (byte) 32 /*0x20*/;
      numArray6[11] = (byte) 64 /*0x40*/;
      numArray6[25] = (byte) 241;
      numArray6[9] = (byte) 146;
      numArray6[10] = (byte) 122;
      numArray6[20] = (byte) 7;
      numArray6[8] = (byte) 56;
      numArray6[51] = (byte) 7;
      numArray6[21] = (byte) 132;
      numArray6[34] = (byte) 146;
      numArray6[19] = (byte) 188;
      numArray6[17] = (byte) 62;
      numArray6[18] = (byte) 18;
      numArray6[43] = (byte) 139;
      numArray6[35] = (byte) 236;
      numArray6[27] = (byte) 99;
      numArray6[22] = (byte) 42;
      numArray6[7] = (byte) 143;
      numArray6[24] = (byte) 251;
      numArray6[13] = (byte) 131;
      numArray6[26] = (byte) 69;
      numArray6[49] = (byte) 170;
      numArray6[28] = (byte) 33;
      numArray6[29] = (byte) 162;
      numArray6[30] = (byte) 227;
      numArray6[31 /*0x1F*/] = (byte) 46;
      numArray6[39] = (byte) 129;
      numArray6[1] = (byte) 141;
      numArray6[52] = (byte) 114;
      numArray6[16 /*0x10*/] = (byte) 103;
      numArray6[36] = (byte) 56;
      numArray6[37] = (byte) 160 /*0xA0*/;
      numArray6[38] = (byte) 68;
      numArray6[0] = (byte) 47;
      numArray6[40] = (byte) 72;
      numArray6[41] = (byte) 181;
      numArray6[42] = (byte) 220;
      numArray6[3] = (byte) 154;
      numArray6[44] = (byte) 42;
      numArray6[54] = (byte) 40;
      numArray6[46] = (byte) 15;
      numArray6[47] = (byte) 253;
      numArray6[33] = (byte) 129;
      numArray6[12] = (byte) 173;
      numArray6[23] = (byte) 122;
      numArray6[50] = (byte) 73;
      numArray6[14] = (byte) 119;
      numArray6[53] = byte.MaxValue;
      numArray6[6] = (byte) 132;
      byte[] numArray7 = new byte[55];
      numArray7[47] = (byte) 9;
      numArray7[1] = (byte) 194;
      numArray7[2] = (byte) 220;
      numArray7[36] = (byte) 139;
      numArray7[4] = (byte) 194;
      numArray7[23] = (byte) 233;
      numArray7[14] = (byte) 27;
      numArray7[7] = (byte) 146;
      numArray7[30] = (byte) 184;
      numArray7[50] = (byte) 16 /*0x10*/;
      numArray7[10] = (byte) 108;
      numArray7[24] = (byte) 200;
      numArray7[52] = (byte) 69;
      numArray7[5] = (byte) 99;
      numArray7[18] = (byte) 2;
      numArray7[25] = (byte) 108;
      numArray7[16 /*0x10*/] = (byte) 108;
      numArray7[13] = (byte) 1;
      numArray7[53] = (byte) 94;
      numArray7[0] = (byte) 76;
      numArray7[20] = (byte) 198;
      numArray7[21] = (byte) 45;
      numArray7[22] = (byte) 107;
      numArray7[6] = (byte) 80 /*0x50*/;
      numArray7[35] = (byte) 128 /*0x80*/;
      numArray7[19] = (byte) 210;
      numArray7[26] = (byte) 9;
      numArray7[27] = (byte) 219;
      numArray7[28] = (byte) 155;
      numArray7[29] = (byte) 215;
      numArray7[32 /*0x20*/] = (byte) 227;
      numArray7[31 /*0x1F*/] = (byte) 154;
      numArray7[3] = (byte) 8;
      numArray7[15] = (byte) 18;
      numArray7[38] = (byte) 82;
      numArray7[9] = (byte) 134;
      numArray7[17] = (byte) 160 /*0xA0*/;
      numArray7[8] = (byte) 240 /*0xF0*/;
      numArray7[51] = (byte) 146;
      numArray7[39] = (byte) 85;
      numArray7[40] = (byte) 28;
      numArray7[41] = (byte) 92;
      numArray7[42] = (byte) 42;
      numArray7[43] = (byte) 48 /*0x30*/;
      numArray7[44] = (byte) 128 /*0x80*/;
      numArray7[48 /*0x30*/] = (byte) 159;
      numArray7[46] = (byte) 143;
      numArray7[45] = (byte) 42;
      numArray7[11] = (byte) 251;
      numArray7[33] = (byte) 238;
      numArray7[49] = (byte) 161;
      numArray7[37] = (byte) 75;
      numArray7[34] = (byte) 230;
      numArray7[12] = (byte) 54;
      numArray7[54] = (byte) 31 /*0x1F*/;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[24]
      {
        (byte) 228,
        (byte) 231,
        (byte) 51,
        (byte) 118,
        (byte) 64 /*0x40*/,
        (byte) 114,
        (byte) 104,
        (byte) 4,
        (byte) 182,
        (byte) 221,
        (byte) 80 /*0x50*/,
        (byte) 40,
        (byte) 60,
        (byte) 234,
        (byte) 64 /*0x40*/,
        (byte) 208 /*0xD0*/,
        (byte) 50,
        (byte) 10,
        (byte) 186,
        (byte) 88,
        (byte) 186,
        (byte) 35,
        (byte) 78,
        (byte) 239
      };
      byte[] numArray9 = new byte[24];
      numArray9[18] = (byte) 120;
      numArray9[9] = (byte) 13;
      numArray9[13] = (byte) 45;
      numArray9[23] = (byte) 60;
      numArray9[4] = (byte) 97;
      numArray9[19] = (byte) 177;
      numArray9[11] = (byte) 219;
      numArray9[7] = (byte) 183;
      numArray9[8] = (byte) 123;
      numArray9[1] = (byte) 179;
      numArray9[10] = (byte) 86;
      numArray9[22] = (byte) 50;
      numArray9[15] = (byte) 244;
      numArray9[12] = (byte) 223;
      numArray9[14] = (byte) 115;
      numArray9[2] = (byte) 138;
      numArray9[16 /*0x10*/] = (byte) 183;
      numArray9[17] = (byte) 150;
      numArray9[3] = (byte) 71;
      numArray9[20] = (byte) 42;
      numArray9[6] = (byte) 199;
      numArray9[21] = (byte) 40;
      numArray9[5] = (byte) 66;
      numArray9[0] = (byte) 139;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[189];
    byte[] numArray11 = new byte[55];
    numArray11[0] = (byte) 50;
    numArray11[10] = (byte) 80 /*0x50*/;
    numArray11[2] = (byte) 75;
    numArray11[3] = (byte) 69;
    numArray11[35] = (byte) 165;
    numArray11[38] = (byte) 149;
    numArray11[6] = (byte) 68;
    numArray11[7] = (byte) 62;
    numArray11[8] = (byte) 54;
    numArray11[9] = (byte) 183;
    numArray11[18] = (byte) 162;
    numArray11[11] = (byte) 118;
    numArray11[32 /*0x20*/] = (byte) 29;
    numArray11[23] = (byte) 184;
    numArray11[15] = (byte) 234;
    numArray11[37] = (byte) 127 /*0x7F*/;
    numArray11[16 /*0x10*/] = (byte) 119;
    numArray11[17] = (byte) 147;
    numArray11[43] = (byte) 37;
    numArray11[34] = byte.MaxValue;
    numArray11[20] = (byte) 210;
    numArray11[21] = (byte) 8;
    numArray11[4] = (byte) 193;
    numArray11[47] = (byte) 188;
    numArray11[24] = (byte) 182;
    numArray11[5] = (byte) 153;
    numArray11[46] = (byte) 228;
    numArray11[26] = (byte) 231;
    numArray11[28] = (byte) 93;
    numArray11[29] = (byte) 173;
    numArray11[22] = (byte) 221;
    numArray11[31 /*0x1F*/] = (byte) 154;
    numArray11[33] = (byte) 92;
    numArray11[13] = (byte) 171;
    numArray11[1] = (byte) 17;
    numArray11[44] = (byte) 79;
    numArray11[36] = (byte) 242;
    numArray11[53] = (byte) 104;
    numArray11[19] = (byte) 8;
    numArray11[39] = (byte) 4;
    numArray11[25] = (byte) 138;
    numArray11[54] = (byte) 210;
    numArray11[42] = (byte) 203;
    numArray11[12] = (byte) 179;
    numArray11[52] = (byte) 218;
    numArray11[14] = (byte) 154;
    numArray11[40] = (byte) 132;
    numArray11[41] = (byte) 33;
    numArray11[48 /*0x30*/] = (byte) 36;
    numArray11[49] = (byte) 240 /*0xF0*/;
    numArray11[50] = (byte) 229;
    numArray11[51] = (byte) 76;
    numArray11[27] = (byte) 48 /*0x30*/;
    numArray11[30] = (byte) 219;
    numArray11[45] = (byte) 142;
    byte[] numArray12 = new byte[55]
    {
      (byte) 157,
      (byte) 85,
      (byte) 23,
      (byte) 61,
      (byte) 134,
      (byte) 72,
      (byte) 219,
      (byte) 50,
      (byte) 218,
      (byte) 161,
      (byte) 1,
      (byte) 161,
      (byte) 225,
      (byte) 141,
      (byte) 223,
      (byte) 204,
      (byte) 18,
      (byte) 187,
      (byte) 217,
      (byte) 103,
      (byte) 117,
      (byte) 40,
      (byte) 190,
      (byte) 195,
      (byte) 69,
      (byte) 223,
      (byte) 180,
      (byte) 221,
      (byte) 238,
      (byte) 114,
      (byte) 150,
      (byte) 142,
      (byte) 21,
      (byte) 171,
      (byte) 169,
      (byte) 132,
      (byte) 209,
      (byte) 91,
      (byte) 29,
      (byte) 175,
      (byte) 247,
      (byte) 29,
      (byte) 177,
      (byte) 208 /*0xD0*/,
      (byte) 129,
      (byte) 53,
      (byte) 204,
      (byte) 228,
      (byte) 169,
      (byte) 24,
      (byte) 197,
      (byte) 102,
      (byte) 53,
      (byte) 223,
      (byte) 10
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 41,
      (byte) 102,
      (byte) 143,
      (byte) 135,
      (byte) 7,
      (byte) 114,
      (byte) 194,
      (byte) 76,
      (byte) 245,
      (byte) 164,
      (byte) 206,
      (byte) 215,
      (byte) 131,
      (byte) 151,
      (byte) 8,
      (byte) 158,
      (byte) 74,
      (byte) 19,
      (byte) 113,
      (byte) 250,
      (byte) 110,
      (byte) 208 /*0xD0*/,
      (byte) 243,
      (byte) 26,
      (byte) 26,
      (byte) 221,
      (byte) 116,
      (byte) 217,
      (byte) 64 /*0x40*/,
      (byte) 130,
      (byte) 207,
      (byte) 104,
      (byte) 230,
      (byte) 246,
      (byte) 54,
      (byte) 136,
      (byte) 207,
      (byte) 203,
      (byte) 104,
      (byte) 205,
      (byte) 127 /*0x7F*/,
      (byte) 253,
      (byte) 163,
      (byte) 145,
      (byte) 190,
      (byte) 56,
      (byte) 170,
      (byte) 97,
      (byte) 229,
      (byte) 176 /*0xB0*/,
      (byte) 243,
      (byte) 209,
      (byte) 175,
      (byte) 243,
      (byte) 12
    };
    byte[] numArray14 = new byte[55];
    numArray14[40] = (byte) 227;
    numArray14[1] = (byte) 56;
    numArray14[2] = (byte) 126;
    numArray14[10] = (byte) 146;
    numArray14[4] = (byte) 118;
    numArray14[5] = (byte) 235;
    numArray14[20] = (byte) 228;
    numArray14[7] = (byte) 114;
    numArray14[46] = (byte) 189;
    numArray14[51] = (byte) 201;
    numArray14[54] = (byte) 220;
    numArray14[11] = (byte) 30;
    numArray14[12] = (byte) 177;
    numArray14[49] = (byte) 45;
    numArray14[13] = (byte) 129;
    numArray14[22] = (byte) 169;
    numArray14[16 /*0x10*/] = (byte) 130;
    numArray14[17] = (byte) 28;
    numArray14[38] = (byte) 151;
    numArray14[27] = (byte) 135;
    numArray14[23] = (byte) 235;
    numArray14[21] = (byte) 235;
    numArray14[52] = (byte) 73;
    numArray14[48 /*0x30*/] = (byte) 176 /*0xB0*/;
    numArray14[24] = (byte) 240 /*0xF0*/;
    numArray14[25] = (byte) 186;
    numArray14[26] = (byte) 79;
    numArray14[14] = (byte) 234;
    numArray14[6] = (byte) 4;
    numArray14[15] = (byte) 239;
    numArray14[47] = (byte) 46;
    numArray14[31 /*0x1F*/] = (byte) 33;
    numArray14[32 /*0x20*/] = (byte) 194;
    numArray14[34] = (byte) 58;
    numArray14[35] = (byte) 231;
    numArray14[29] = (byte) 112 /*0x70*/;
    numArray14[19] = (byte) 2;
    numArray14[37] = (byte) 129;
    numArray14[36] = (byte) 197;
    numArray14[9] = (byte) 21;
    numArray14[45] = (byte) 94;
    numArray14[41] = (byte) 242;
    numArray14[42] = (byte) 228;
    numArray14[43] = (byte) 32 /*0x20*/;
    numArray14[18] = (byte) 228;
    numArray14[33] = (byte) 185;
    numArray14[28] = (byte) 230;
    numArray14[3] = (byte) 142;
    numArray14[44] = (byte) 173;
    numArray14[8] = (byte) 240 /*0xF0*/;
    numArray14[50] = (byte) 216;
    numArray14[39] = (byte) 147;
    numArray14[30] = (byte) 107;
    numArray14[53] = (byte) 119;
    numArray14[0] = (byte) 200;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 25,
      (byte) 138,
      (byte) 247,
      (byte) 234,
      (byte) 244,
      (byte) 122,
      (byte) 228,
      (byte) 213,
      (byte) 173,
      (byte) 254,
      (byte) 163,
      (byte) 131,
      (byte) 106,
      (byte) 117,
      (byte) 31 /*0x1F*/,
      (byte) 108,
      (byte) 211,
      (byte) 227,
      (byte) 126,
      (byte) 253,
      (byte) 165,
      (byte) 3,
      (byte) 154,
      (byte) 20,
      (byte) 18,
      (byte) 143,
      (byte) 175,
      (byte) 26,
      (byte) 216,
      (byte) 172,
      (byte) 121,
      (byte) 78,
      (byte) 177,
      (byte) 3,
      (byte) 63 /*0x3F*/,
      (byte) 212,
      (byte) 70,
      (byte) 229,
      (byte) 162,
      (byte) 119,
      (byte) 74,
      (byte) 231,
      (byte) 171,
      (byte) 72,
      (byte) 122,
      (byte) 125,
      (byte) 250,
      (byte) 108,
      (byte) 84,
      (byte) 186,
      (byte) 56,
      (byte) 149,
      (byte) 29,
      (byte) 47,
      (byte) 108
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 218,
      (byte) 243,
      (byte) 190,
      (byte) 85,
      (byte) 105,
      (byte) 178,
      (byte) 131,
      (byte) 232,
      (byte) 78,
      (byte) 166,
      (byte) 147,
      (byte) 17,
      (byte) 208 /*0xD0*/,
      (byte) 9,
      (byte) 5,
      (byte) 124,
      (byte) 175,
      (byte) 27,
      (byte) 191,
      (byte) 27,
      (byte) 66,
      (byte) 190,
      (byte) 233,
      (byte) 197,
      (byte) 178,
      (byte) 50,
      (byte) 53,
      (byte) 182,
      (byte) 228,
      (byte) 78,
      (byte) 89,
      (byte) 193,
      (byte) 248,
      (byte) 161,
      (byte) 250,
      (byte) 148,
      (byte) 179,
      (byte) 98,
      (byte) 25,
      (byte) 139,
      (byte) 220,
      (byte) 163,
      (byte) 132,
      (byte) 181,
      (byte) 150,
      (byte) 29,
      (byte) 249,
      (byte) 204,
      (byte) 176 /*0xB0*/,
      (byte) 84,
      (byte) 237,
      (byte) 222,
      (byte) 5,
      (byte) 14,
      (byte) 157
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[24];
    numArray17[2] = (byte) 100;
    numArray17[11] = (byte) 53;
    numArray17[17] = (byte) 178;
    numArray17[7] = (byte) 133;
    numArray17[20] = (byte) 81;
    numArray17[8] = (byte) 85;
    numArray17[16 /*0x10*/] = (byte) 7;
    numArray17[18] = (byte) 189;
    numArray17[12] = (byte) 75;
    numArray17[3] = (byte) 203;
    numArray17[10] = (byte) 103;
    numArray17[21] = (byte) 154;
    numArray17[1] = (byte) 216;
    numArray17[0] = (byte) 227;
    numArray17[14] = (byte) 238;
    numArray17[15] = (byte) 64 /*0x40*/;
    numArray17[23] = (byte) 149;
    numArray17[9] = (byte) 210;
    numArray17[5] = (byte) 253;
    numArray17[19] = (byte) 88;
    numArray17[6] = (byte) 1;
    numArray17[4] = (byte) 202;
    numArray17[22] = (byte) 148;
    numArray17[13] = (byte) 37;
    byte[] numArray18 = new byte[24]
    {
      (byte) 208 /*0xD0*/,
      (byte) 252,
      (byte) 101,
      (byte) 104,
      (byte) 186,
      (byte) 10,
      (byte) 54,
      (byte) 130,
      (byte) 224 /*0xE0*/,
      (byte) 167,
      (byte) 124,
      (byte) 105,
      (byte) 172,
      (byte) 240 /*0xF0*/,
      (byte) 201,
      (byte) 254,
      (byte) 207,
      (byte) 51,
      (byte) 96 /*0x60*/,
      (byte) 68,
      (byte) 248,
      (byte) 227,
      (byte) 208 /*0xD0*/,
      (byte) 43
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 24);
    for (int index = 0; index < 24; ++index)
      numArray10[index + 165] ^= numArray18[index];
    byte[] numArray19 = new byte[54];
    byte[] response = new byte[54];
    Array.Copy((Array) sc_12586.sspq, 486, (Array) numArray19, 0, 54);
    key.Query(true, 335, numArray19, response);
    Array.Copy((Array) sc_12586.sspr, 486, (Array) numArray19, 0, 54);
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

  internal static string ssp_appserver_12632()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[67];
      byte[] numArray2 = new byte[55]
      {
        (byte) 219,
        (byte) 15,
        (byte) 15,
        (byte) 210,
        (byte) 90,
        (byte) 205,
        (byte) 125,
        (byte) 73,
        (byte) 85,
        (byte) 99,
        (byte) 17,
        (byte) 9,
        (byte) 75,
        (byte) 233,
        (byte) 161,
        (byte) 34,
        (byte) 166,
        (byte) 23,
        (byte) 148,
        (byte) 56,
        (byte) 182,
        (byte) 199,
        (byte) 145,
        (byte) 108,
        (byte) 116,
        (byte) 141,
        (byte) 65,
        (byte) 224 /*0xE0*/,
        (byte) 26,
        (byte) 182,
        (byte) 192 /*0xC0*/,
        (byte) 62,
        (byte) 96 /*0x60*/,
        (byte) 185,
        (byte) 11,
        (byte) 91,
        (byte) 53,
        (byte) 72,
        (byte) 111,
        (byte) 161,
        (byte) 165,
        (byte) 49,
        (byte) 181,
        (byte) 185,
        (byte) 16 /*0x10*/,
        (byte) 247,
        (byte) 15,
        (byte) 216,
        (byte) 138,
        (byte) 127 /*0x7F*/,
        (byte) 43,
        (byte) 82,
        (byte) 189,
        (byte) 129,
        (byte) 148
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 18,
        (byte) 217,
        (byte) 221,
        (byte) 97,
        (byte) 1,
        (byte) 115,
        (byte) 195,
        (byte) 156,
        (byte) 133,
        (byte) 214,
        (byte) 78,
        (byte) 8,
        (byte) 33,
        (byte) 53,
        (byte) 143,
        (byte) 215,
        (byte) 160 /*0xA0*/,
        (byte) 98,
        (byte) 125,
        (byte) 40,
        (byte) 98,
        (byte) 201,
        (byte) 178,
        (byte) 156,
        (byte) 10,
        (byte) 127 /*0x7F*/,
        (byte) 206,
        (byte) 111,
        (byte) 193,
        (byte) 68,
        (byte) 185,
        (byte) 66,
        (byte) 90,
        (byte) 154,
        (byte) 2,
        (byte) 132,
        (byte) 41,
        (byte) 47,
        (byte) 10,
        (byte) 191,
        (byte) 162,
        (byte) 97,
        (byte) 218,
        (byte) 197,
        (byte) 162,
        (byte) 33,
        (byte) 179,
        (byte) 189,
        (byte) 189,
        (byte) 70,
        (byte) 31 /*0x1F*/,
        (byte) 243,
        (byte) 163,
        (byte) 82,
        (byte) 221
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      numArray4[5] = (byte) 47;
      numArray4[1] = (byte) 94;
      numArray4[0] = (byte) 163;
      numArray4[8] = (byte) 210;
      numArray4[6] = (byte) 159;
      numArray4[4] = (byte) 156;
      numArray4[2] = (byte) 204;
      numArray4[7] = (byte) 162;
      numArray4[10] = (byte) 145;
      numArray4[9] = (byte) 168;
      numArray4[3] = (byte) 139;
      numArray4[11] = (byte) 221;
      byte[] numArray5 = new byte[12]
      {
        (byte) 166,
        (byte) 154,
        (byte) 124,
        (byte) 233,
        (byte) 208 /*0xD0*/,
        (byte) 97,
        (byte) 75,
        (byte) 49,
        (byte) 64 /*0x40*/,
        (byte) 245,
        (byte) 125,
        (byte) 189
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[67];
    byte[] numArray7 = new byte[55];
    numArray7[26] = (byte) 2;
    numArray7[1] = (byte) 110;
    numArray7[23] = (byte) 202;
    numArray7[42] = (byte) 116;
    numArray7[4] = (byte) 37;
    numArray7[46] = (byte) 130;
    numArray7[6] = (byte) 220;
    numArray7[11] = (byte) 8;
    numArray7[49] = (byte) 30;
    numArray7[31 /*0x1F*/] = (byte) 34;
    numArray7[10] = (byte) 21;
    numArray7[0] = (byte) 138;
    numArray7[25] = (byte) 14;
    numArray7[13] = (byte) 22;
    numArray7[14] = (byte) 22;
    numArray7[15] = (byte) 61;
    numArray7[16 /*0x10*/] = (byte) 78;
    numArray7[17] = (byte) 126;
    numArray7[18] = (byte) 250;
    numArray7[47] = (byte) 251;
    numArray7[20] = (byte) 146;
    numArray7[38] = (byte) 107;
    numArray7[22] = (byte) 170;
    numArray7[3] = (byte) 222;
    numArray7[7] = (byte) 146;
    numArray7[30] = (byte) 78;
    numArray7[41] = (byte) 90;
    numArray7[2] = (byte) 129;
    numArray7[28] = (byte) 70;
    numArray7[29] = (byte) 224 /*0xE0*/;
    numArray7[12] = (byte) 2;
    numArray7[32 /*0x20*/] = (byte) 70;
    numArray7[33] = (byte) 148;
    numArray7[48 /*0x30*/] = (byte) 193;
    numArray7[52] = (byte) 110;
    numArray7[35] = (byte) 52;
    numArray7[36] = (byte) 81;
    numArray7[9] = (byte) 163;
    numArray7[50] = (byte) 46;
    numArray7[5] = (byte) 247;
    numArray7[40] = (byte) 105;
    numArray7[53] = (byte) 18;
    numArray7[27] = (byte) 166;
    numArray7[43] = (byte) 3;
    numArray7[44] = (byte) 220;
    numArray7[45] = (byte) 138;
    numArray7[24] = (byte) 238;
    numArray7[21] = (byte) 204;
    numArray7[51] = (byte) 0;
    numArray7[34] = (byte) 20;
    numArray7[8] = (byte) 234;
    numArray7[19] = (byte) 68;
    numArray7[39] = (byte) 229;
    numArray7[37] = (byte) 202;
    numArray7[54] = (byte) 8;
    byte[] numArray8 = new byte[55]
    {
      (byte) 179,
      (byte) 209,
      (byte) 30,
      (byte) 4,
      (byte) 229,
      (byte) 14,
      (byte) 119,
      (byte) 199,
      (byte) 138,
      (byte) 83,
      (byte) 38,
      (byte) 230,
      (byte) 145,
      (byte) 127 /*0x7F*/,
      (byte) 62,
      (byte) 102,
      (byte) 231,
      (byte) 61,
      (byte) 188,
      (byte) 212,
      (byte) 74,
      (byte) 106,
      (byte) 250,
      (byte) 77,
      (byte) 161,
      (byte) 222,
      (byte) 88,
      (byte) 108,
      (byte) 219,
      (byte) 213,
      (byte) 67,
      (byte) 13,
      (byte) 237,
      (byte) 25,
      (byte) 67,
      (byte) 38,
      (byte) 59,
      (byte) 95,
      (byte) 87,
      (byte) 138,
      (byte) 126,
      (byte) 111,
      (byte) 43,
      (byte) 196,
      (byte) 235,
      (byte) 13,
      (byte) 233,
      (byte) 158,
      (byte) 224 /*0xE0*/,
      (byte) 11,
      (byte) 133,
      (byte) 73,
      (byte) 182,
      (byte) 243,
      (byte) 174
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[12];
    numArray9[1] = (byte) 139;
    numArray9[11] = (byte) 171;
    numArray9[10] = (byte) 225;
    numArray9[3] = (byte) 238;
    numArray9[2] = (byte) 138;
    numArray9[9] = (byte) 142;
    numArray9[4] = (byte) 81;
    numArray9[7] = (byte) 7;
    numArray9[8] = (byte) 63 /*0x3F*/;
    numArray9[0] = (byte) 200;
    numArray9[6] = (byte) 87;
    numArray9[5] = (byte) 35;
    byte[] numArray10 = new byte[12]
    {
      (byte) 158,
      (byte) 159,
      (byte) 94,
      (byte) 132,
      (byte) 170,
      (byte) 60,
      (byte) 153,
      (byte) 223,
      (byte) 138,
      (byte) 55,
      (byte) 55,
      (byte) 91
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 12);
    for (int index = 0; index < 12; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12633()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55];
      numArray2[30] = (byte) 225;
      numArray2[1] = (byte) 90;
      numArray2[2] = (byte) 252;
      numArray2[3] = (byte) 188;
      numArray2[9] = (byte) 2;
      numArray2[29] = (byte) 19;
      numArray2[36] = (byte) 146;
      numArray2[7] = (byte) 78;
      numArray2[8] = (byte) 202;
      numArray2[18] = (byte) 109;
      numArray2[16 /*0x10*/] = (byte) 51;
      numArray2[5] = (byte) 84;
      numArray2[12] = (byte) 18;
      numArray2[14] = (byte) 170;
      numArray2[22] = (byte) 53;
      numArray2[50] = (byte) 67;
      numArray2[13] = (byte) 166;
      numArray2[51] = (byte) 190;
      numArray2[15] = (byte) 57;
      numArray2[49] = (byte) 98;
      numArray2[25] = (byte) 119;
      numArray2[47] = (byte) 83;
      numArray2[24] = (byte) 166;
      numArray2[23] = (byte) 143;
      numArray2[11] = (byte) 234;
      numArray2[37] = (byte) 37;
      numArray2[32 /*0x20*/] = (byte) 46;
      numArray2[27] = (byte) 195;
      numArray2[28] = (byte) 239;
      numArray2[45] = (byte) 62;
      numArray2[33] = (byte) 81;
      numArray2[41] = (byte) 90;
      numArray2[0] = (byte) 95;
      numArray2[20] = (byte) 1;
      numArray2[34] = (byte) 167;
      numArray2[39] = (byte) 29;
      numArray2[44] = (byte) 199;
      numArray2[6] = (byte) 210;
      numArray2[38] = (byte) 186;
      numArray2[26] = (byte) 34;
      numArray2[40] = (byte) 125;
      numArray2[17] = (byte) 78;
      numArray2[42] = (byte) 242;
      numArray2[21] = (byte) 49;
      numArray2[52] = (byte) 152;
      numArray2[31 /*0x1F*/] = (byte) 76;
      numArray2[46] = (byte) 218;
      numArray2[4] = (byte) 241;
      numArray2[48 /*0x30*/] = (byte) 89;
      numArray2[53] = (byte) 19;
      numArray2[10] = (byte) 224 /*0xE0*/;
      numArray2[43] = (byte) 33;
      numArray2[19] = (byte) 225;
      numArray2[35] = (byte) 228;
      numArray2[54] = (byte) 5;
      byte[] numArray3 = new byte[55]
      {
        (byte) 95,
        (byte) 117,
        (byte) 75,
        (byte) 19,
        (byte) 234,
        (byte) 57,
        (byte) 4,
        (byte) 141,
        (byte) 223,
        (byte) 226,
        (byte) 188,
        (byte) 11,
        (byte) 137,
        (byte) 165,
        (byte) 202,
        (byte) 110,
        (byte) 223,
        (byte) 7,
        (byte) 2,
        (byte) 115,
        (byte) 32 /*0x20*/,
        (byte) 105,
        (byte) 203,
        (byte) 16 /*0x10*/,
        (byte) 31 /*0x1F*/,
        (byte) 89,
        (byte) 196,
        (byte) 238,
        (byte) 58,
        (byte) 181,
        (byte) 140,
        (byte) 90,
        (byte) 32 /*0x20*/,
        (byte) 224 /*0xE0*/,
        (byte) 167,
        (byte) 228,
        (byte) 165,
        (byte) 90,
        (byte) 42,
        (byte) 101,
        (byte) 167,
        (byte) 178,
        (byte) 240 /*0xF0*/,
        (byte) 59,
        (byte) 23,
        (byte) 144 /*0x90*/,
        (byte) 85,
        (byte) 79,
        (byte) 34,
        (byte) 26,
        (byte) 199,
        (byte) 176 /*0xB0*/,
        (byte) 114,
        (byte) 148,
        (byte) 47
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8]
      {
        (byte) 164,
        (byte) 172,
        (byte) 219,
        (byte) 97,
        (byte) 164,
        (byte) 209,
        (byte) 230,
        byte.MaxValue
      };
      byte[] numArray5 = new byte[8];
      numArray5[1] = (byte) 236;
      numArray5[6] = (byte) 235;
      numArray5[2] = (byte) 197;
      numArray5[0] = (byte) 222;
      numArray5[3] = (byte) 242;
      numArray5[4] = (byte) 250;
      numArray5[7] = (byte) 15;
      numArray5[5] = (byte) 89;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_12586.sspq, 540, (Array) numArray6, 0, 53);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12586.sspr, 540, (Array) numArray6, 0, 53);
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
      (byte) 128 /*0x80*/,
      (byte) 171,
      (byte) 20,
      (byte) 65,
      (byte) 4,
      (byte) 224 /*0xE0*/,
      (byte) 172,
      (byte) 71,
      (byte) 151,
      (byte) 95,
      (byte) 32 /*0x20*/,
      (byte) 133,
      (byte) 177,
      (byte) 58,
      (byte) 153,
      (byte) 182,
      (byte) 112 /*0x70*/,
      (byte) 156,
      (byte) 14,
      (byte) 164,
      (byte) 251,
      (byte) 42,
      (byte) 9,
      (byte) 249,
      (byte) 222,
      (byte) 172,
      (byte) 28,
      (byte) 209,
      (byte) 58,
      (byte) 58,
      (byte) 160 /*0xA0*/,
      (byte) 233,
      (byte) 14,
      (byte) 143,
      (byte) 51,
      (byte) 66,
      (byte) 229,
      (byte) 206,
      (byte) 194,
      (byte) 118,
      (byte) 58,
      (byte) 135,
      (byte) 104,
      (byte) 236,
      (byte) 230,
      (byte) 239,
      (byte) 131,
      (byte) 77,
      (byte) 99,
      (byte) 157,
      (byte) 143,
      (byte) 252,
      (byte) 223,
      (byte) 50,
      (byte) 47
    };
    byte[] numArray9 = new byte[55];
    numArray9[24] = (byte) 115;
    numArray9[43] = (byte) 198;
    numArray9[2] = (byte) 65;
    numArray9[18] = (byte) 201;
    numArray9[38] = (byte) 89;
    numArray9[8] = (byte) 195;
    numArray9[0] = (byte) 151;
    numArray9[7] = (byte) 211;
    numArray9[48 /*0x30*/] = (byte) 148;
    numArray9[9] = (byte) 184;
    numArray9[20] = (byte) 205;
    numArray9[11] = (byte) 187;
    numArray9[12] = (byte) 171;
    numArray9[6] = (byte) 234;
    numArray9[14] = (byte) 20;
    numArray9[37] = (byte) 1;
    numArray9[16 /*0x10*/] = (byte) 79;
    numArray9[17] = (byte) 96 /*0x60*/;
    numArray9[19] = (byte) 113;
    numArray9[3] = (byte) 147;
    numArray9[50] = (byte) 46;
    numArray9[21] = (byte) 38;
    numArray9[22] = (byte) 104;
    numArray9[36] = (byte) 148;
    numArray9[5] = (byte) 148;
    numArray9[54] = (byte) 41;
    numArray9[26] = (byte) 153;
    numArray9[27] = (byte) 74;
    numArray9[28] = (byte) 104;
    numArray9[29] = (byte) 216;
    numArray9[53] = (byte) 216;
    numArray9[10] = (byte) 138;
    numArray9[32 /*0x20*/] = (byte) 83;
    numArray9[39] = (byte) 10;
    numArray9[34] = (byte) 215;
    numArray9[31 /*0x1F*/] = (byte) 211;
    numArray9[33] = (byte) 221;
    numArray9[25] = (byte) 226;
    numArray9[35] = (byte) 32 /*0x20*/;
    numArray9[13] = (byte) 185;
    numArray9[23] = (byte) 0;
    numArray9[41] = (byte) 70;
    numArray9[42] = (byte) 134;
    numArray9[47] = (byte) 209;
    numArray9[44] = (byte) 240 /*0xF0*/;
    numArray9[45] = (byte) 78;
    numArray9[15] = (byte) 199;
    numArray9[49] = (byte) 90;
    numArray9[40] = (byte) 16 /*0x10*/;
    numArray9[30] = (byte) 204;
    numArray9[46] = (byte) 232;
    numArray9[51] = (byte) 123;
    numArray9[52] = (byte) 106;
    numArray9[1] = (byte) 41;
    numArray9[4] = (byte) 86;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[8];
    numArray10[4] = (byte) 107;
    numArray10[1] = (byte) 116;
    numArray10[6] = (byte) 176 /*0xB0*/;
    numArray10[0] = (byte) 8;
    numArray10[2] = (byte) 46;
    numArray10[5] = (byte) 110;
    numArray10[7] = (byte) 27;
    numArray10[3] = (byte) 245;
    byte[] numArray11 = new byte[8]
    {
      (byte) 146,
      (byte) 154,
      (byte) 85,
      (byte) 250,
      (byte) 223,
      (byte) 126,
      (byte) 15,
      (byte) 26
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12634()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[112 /*0x70*/];
      byte[] numArray2 = new byte[55];
      numArray2[24] = (byte) 159;
      numArray2[0] = (byte) 239;
      numArray2[26] = (byte) 84;
      numArray2[33] = (byte) 221;
      numArray2[10] = (byte) 20;
      numArray2[5] = (byte) 98;
      numArray2[6] = (byte) 209;
      numArray2[7] = (byte) 191;
      numArray2[8] = (byte) 129;
      numArray2[9] = (byte) 193;
      numArray2[20] = (byte) 91;
      numArray2[11] = (byte) 163;
      numArray2[35] = (byte) 164;
      numArray2[39] = (byte) 9;
      numArray2[27] = (byte) 196;
      numArray2[16 /*0x10*/] = (byte) 59;
      numArray2[43] = (byte) 108;
      numArray2[12] = (byte) 74;
      numArray2[18] = (byte) 244;
      numArray2[17] = (byte) 188;
      numArray2[21] = (byte) 17;
      numArray2[38] = (byte) 108;
      numArray2[53] = (byte) 234;
      numArray2[23] = (byte) 222;
      numArray2[4] = (byte) 137;
      numArray2[25] = (byte) 27;
      numArray2[1] = (byte) 99;
      numArray2[14] = (byte) 193;
      numArray2[29] = (byte) 179;
      numArray2[13] = (byte) 25;
      numArray2[30] = (byte) 98;
      numArray2[44] = (byte) 134;
      numArray2[32 /*0x20*/] = (byte) 196;
      numArray2[28] = (byte) 180;
      numArray2[42] = (byte) 154;
      numArray2[22] = (byte) 129;
      numArray2[36] = (byte) 254;
      numArray2[41] = (byte) 241;
      numArray2[15] = (byte) 245;
      numArray2[2] = (byte) 117;
      numArray2[3] = (byte) 199;
      numArray2[31 /*0x1F*/] = (byte) 189;
      numArray2[19] = (byte) 13;
      numArray2[34] = (byte) 151;
      numArray2[37] = (byte) 103;
      numArray2[45] = (byte) 30;
      numArray2[46] = (byte) 223;
      numArray2[47] = (byte) 157;
      numArray2[48 /*0x30*/] = (byte) 70;
      numArray2[40] = (byte) 157;
      numArray2[50] = (byte) 51;
      numArray2[51] = (byte) 41;
      numArray2[52] = (byte) 21;
      numArray2[49] = (byte) 66;
      numArray2[54] = (byte) 90;
      byte[] numArray3 = new byte[55]
      {
        (byte) 97,
        (byte) 150,
        (byte) 220,
        (byte) 233,
        (byte) 54,
        (byte) 89,
        (byte) 218,
        (byte) 78,
        (byte) 81,
        (byte) 91,
        (byte) 15,
        (byte) 182,
        (byte) 58,
        (byte) 59,
        (byte) 217,
        (byte) 137,
        (byte) 128 /*0x80*/,
        (byte) 65,
        (byte) 226,
        (byte) 170,
        (byte) 130,
        (byte) 69,
        (byte) 29,
        (byte) 75,
        (byte) 58,
        (byte) 82,
        (byte) 9,
        (byte) 68,
        (byte) 209,
        (byte) 137,
        (byte) 43,
        (byte) 103,
        (byte) 33,
        (byte) 157,
        (byte) 4,
        (byte) 157,
        (byte) 39,
        (byte) 40,
        (byte) 220,
        (byte) 38,
        (byte) 62,
        (byte) 3,
        (byte) 44,
        (byte) 252,
        (byte) 87,
        (byte) 93,
        (byte) 197,
        (byte) 240 /*0xF0*/,
        (byte) 187,
        (byte) 97,
        (byte) 42,
        (byte) 1,
        (byte) 116,
        (byte) 63 /*0x3F*/,
        (byte) 222
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 63 /*0x3F*/,
        (byte) 166,
        (byte) 48 /*0x30*/,
        (byte) 86,
        (byte) 142,
        (byte) 152,
        (byte) 77,
        (byte) 37,
        (byte) 175,
        (byte) 5,
        (byte) 234,
        (byte) 200,
        (byte) 90,
        (byte) 143,
        (byte) 148,
        (byte) 80 /*0x50*/,
        (byte) 239,
        (byte) 223,
        (byte) 132,
        (byte) 169,
        (byte) 7,
        (byte) 40,
        (byte) 66,
        (byte) 203,
        (byte) 155,
        (byte) 84,
        (byte) 38,
        (byte) 32 /*0x20*/,
        (byte) 95,
        (byte) 125,
        (byte) 95,
        (byte) 146,
        (byte) 82,
        (byte) 244,
        (byte) 40,
        (byte) 153,
        (byte) 206,
        (byte) 165,
        (byte) 2,
        (byte) 77,
        (byte) 127 /*0x7F*/,
        (byte) 232,
        (byte) 128 /*0x80*/,
        (byte) 57,
        (byte) 134,
        (byte) 83,
        (byte) 207,
        (byte) 187,
        (byte) 202,
        (byte) 137,
        (byte) 58,
        (byte) 154,
        (byte) 108,
        (byte) 190,
        (byte) 198
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 245,
        (byte) 55,
        (byte) 211,
        (byte) 254,
        (byte) 117,
        (byte) 145,
        (byte) 219,
        (byte) 219,
        (byte) 247,
        (byte) 181,
        (byte) 154,
        (byte) 69,
        (byte) 188,
        (byte) 251,
        (byte) 204,
        (byte) 254,
        (byte) 172,
        (byte) 213,
        (byte) 219,
        (byte) 186,
        (byte) 21,
        (byte) 201,
        (byte) 98,
        (byte) 251,
        (byte) 176 /*0xB0*/,
        (byte) 100,
        (byte) 93,
        (byte) 226,
        (byte) 167,
        (byte) 16 /*0x10*/,
        (byte) 190,
        (byte) 42,
        (byte) 128 /*0x80*/,
        (byte) 13,
        (byte) 162,
        (byte) 39,
        (byte) 70,
        (byte) 112 /*0x70*/,
        (byte) 238,
        (byte) 217,
        (byte) 246,
        (byte) 149,
        (byte) 246,
        (byte) 41,
        (byte) 216,
        (byte) 10,
        (byte) 91,
        (byte) 217,
        (byte) 201,
        (byte) 200,
        (byte) 220,
        (byte) 57,
        (byte) 157,
        (byte) 36,
        (byte) 126
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[2]
      {
        (byte) 101,
        (byte) 187
      };
      byte[] numArray7 = new byte[2]
      {
        (byte) 251,
        (byte) 216
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[10];
      byte[] response = new byte[10];
      Array.Copy((Array) sc_12586.sspq, 593, (Array) numArray8, 0, 10);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12586.sspr, 593, (Array) numArray8, 0, 10);
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
    byte[] numArray9 = new byte[112 /*0x70*/];
    byte[] numArray10 = new byte[55]
    {
      (byte) 54,
      (byte) 41,
      (byte) 30,
      (byte) 168,
      (byte) 9,
      (byte) 68,
      (byte) 71,
      (byte) 133,
      (byte) 107,
      (byte) 86,
      (byte) 214,
      (byte) 66,
      (byte) 197,
      (byte) 38,
      (byte) 147,
      (byte) 45,
      (byte) 18,
      (byte) 54,
      (byte) 105,
      (byte) 233,
      (byte) 200,
      (byte) 219,
      (byte) 82,
      (byte) 34,
      (byte) 78,
      (byte) 147,
      (byte) 46,
      (byte) 220,
      (byte) 104,
      (byte) 84,
      (byte) 206,
      (byte) 7,
      (byte) 239,
      (byte) 188,
      (byte) 53,
      (byte) 148,
      (byte) 168,
      (byte) 109,
      (byte) 89,
      (byte) 33,
      (byte) 115,
      (byte) 179,
      (byte) 7,
      (byte) 214,
      (byte) 239,
      (byte) 239,
      (byte) 224 /*0xE0*/,
      (byte) 49,
      (byte) 226,
      (byte) 57,
      (byte) 84,
      (byte) 197,
      (byte) 24,
      (byte) 97,
      (byte) 21
    };
    byte[] numArray11 = new byte[55];
    numArray11[33] = (byte) 42;
    numArray11[1] = (byte) 150;
    numArray11[2] = (byte) 75;
    numArray11[19] = (byte) 165;
    numArray11[47] = (byte) 27;
    numArray11[50] = (byte) 20;
    numArray11[4] = (byte) 36;
    numArray11[7] = (byte) 25;
    numArray11[8] = (byte) 68;
    numArray11[30] = (byte) 187;
    numArray11[10] = (byte) 45;
    numArray11[11] = (byte) 149;
    numArray11[12] = (byte) 32 /*0x20*/;
    numArray11[40] = (byte) 229;
    numArray11[36] = (byte) 145;
    numArray11[46] = (byte) 191;
    numArray11[6] = (byte) 237;
    numArray11[20] = (byte) 77;
    numArray11[32 /*0x20*/] = (byte) 96 /*0x60*/;
    numArray11[42] = (byte) 28;
    numArray11[51] = (byte) 158;
    numArray11[21] = (byte) 51;
    numArray11[22] = (byte) 7;
    numArray11[54] = (byte) 125;
    numArray11[24] = (byte) 235;
    numArray11[25] = (byte) 16 /*0x10*/;
    numArray11[52] = (byte) 159;
    numArray11[27] = (byte) 237;
    numArray11[28] = (byte) 133;
    numArray11[16 /*0x10*/] = (byte) 92;
    numArray11[45] = (byte) 121;
    numArray11[31 /*0x1F*/] = (byte) 217;
    numArray11[0] = (byte) 47;
    numArray11[17] = (byte) 93;
    numArray11[26] = (byte) 86;
    numArray11[35] = (byte) 147;
    numArray11[34] = (byte) 71;
    numArray11[37] = (byte) 58;
    numArray11[14] = (byte) 27;
    numArray11[9] = (byte) 201;
    numArray11[15] = (byte) 193;
    numArray11[41] = (byte) 52;
    numArray11[13] = (byte) 129;
    numArray11[43] = (byte) 105;
    numArray11[44] = (byte) 69;
    numArray11[39] = (byte) 209;
    numArray11[49] = (byte) 61;
    numArray11[5] = (byte) 216;
    numArray11[48 /*0x30*/] = (byte) 47;
    numArray11[23] = (byte) 189;
    numArray11[3] = (byte) 19;
    numArray11[18] = (byte) 99;
    numArray11[53] = (byte) 249;
    numArray11[29] = (byte) 207;
    numArray11[38] = (byte) 220;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[29] = (byte) 30;
    numArray12[1] = (byte) 251;
    numArray12[2] = (byte) 194;
    numArray12[54] = (byte) 44;
    numArray12[4] = (byte) 112 /*0x70*/;
    numArray12[39] = (byte) 170;
    numArray12[41] = (byte) 208 /*0xD0*/;
    numArray12[35] = (byte) 24;
    numArray12[50] = (byte) 148;
    numArray12[9] = (byte) 196;
    numArray12[10] = (byte) 97;
    numArray12[23] = (byte) 219;
    numArray12[3] = (byte) 24;
    numArray12[13] = (byte) 217;
    numArray12[14] = (byte) 145;
    numArray12[15] = (byte) 248;
    numArray12[16 /*0x10*/] = (byte) 59;
    numArray12[17] = (byte) 108;
    numArray12[19] = (byte) 227;
    numArray12[32 /*0x20*/] = (byte) 82;
    numArray12[20] = (byte) 45;
    numArray12[37] = (byte) 243;
    numArray12[22] = (byte) 195;
    numArray12[7] = (byte) 126;
    numArray12[34] = (byte) 225;
    numArray12[47] = (byte) 218;
    numArray12[5] = (byte) 236;
    numArray12[6] = (byte) 22;
    numArray12[25] = (byte) 22;
    numArray12[53] = (byte) 164;
    numArray12[30] = (byte) 162;
    numArray12[26] = (byte) 123;
    numArray12[21] = (byte) 16 /*0x10*/;
    numArray12[33] = (byte) 99;
    numArray12[18] = (byte) 125;
    numArray12[27] = (byte) 79;
    numArray12[36] = (byte) 33;
    numArray12[28] = (byte) 32 /*0x20*/;
    numArray12[38] = (byte) 152;
    numArray12[31 /*0x1F*/] = (byte) 167;
    numArray12[40] = (byte) 218;
    numArray12[46] = (byte) 162;
    numArray12[0] = (byte) 202;
    numArray12[43] = (byte) 215;
    numArray12[11] = (byte) 23;
    numArray12[45] = (byte) 41;
    numArray12[12] = (byte) 64 /*0x40*/;
    numArray12[42] = (byte) 66;
    numArray12[49] = (byte) 240 /*0xF0*/;
    numArray12[44] = (byte) 117;
    numArray12[48 /*0x30*/] = (byte) 31 /*0x1F*/;
    numArray12[51] = (byte) 127 /*0x7F*/;
    numArray12[52] = (byte) 130;
    numArray12[8] = (byte) 223;
    numArray12[24] = (byte) 188;
    byte[] numArray13 = new byte[55];
    numArray13[54] = (byte) 146;
    numArray13[1] = (byte) 239;
    numArray13[2] = (byte) 46;
    numArray13[3] = (byte) 174;
    numArray13[21] = (byte) 190;
    numArray13[5] = (byte) 249;
    numArray13[14] = (byte) 10;
    numArray13[7] = (byte) 11;
    numArray13[10] = (byte) 151;
    numArray13[18] = (byte) 110;
    numArray13[39] = (byte) 173;
    numArray13[41] = (byte) 104;
    numArray13[45] = (byte) 212;
    numArray13[17] = (byte) 130;
    numArray13[52] = (byte) 148;
    numArray13[29] = (byte) 129;
    numArray13[16 /*0x10*/] = (byte) 66;
    numArray13[28] = (byte) 250;
    numArray13[8] = (byte) 5;
    numArray13[19] = (byte) 238;
    numArray13[20] = (byte) 135;
    numArray13[13] = (byte) 56;
    numArray13[22] = (byte) 203;
    numArray13[23] = (byte) 183;
    numArray13[49] = (byte) 23;
    numArray13[46] = (byte) 168;
    numArray13[26] = (byte) 220;
    numArray13[53] = (byte) 74;
    numArray13[47] = (byte) 59;
    numArray13[38] = (byte) 13;
    numArray13[40] = (byte) 98;
    numArray13[51] = (byte) 175;
    numArray13[32 /*0x20*/] = (byte) 202;
    numArray13[33] = (byte) 148;
    numArray13[34] = (byte) 49;
    numArray13[35] = (byte) 25;
    numArray13[4] = (byte) 65;
    numArray13[37] = (byte) 226;
    numArray13[9] = (byte) 57;
    numArray13[27] = (byte) 123;
    numArray13[31 /*0x1F*/] = (byte) 179;
    numArray13[24] = (byte) 75;
    numArray13[42] = (byte) 87;
    numArray13[43] = (byte) 124;
    numArray13[6] = (byte) 62;
    numArray13[12] = (byte) 251;
    numArray13[44] = (byte) 184;
    numArray13[25] = (byte) 103;
    numArray13[48 /*0x30*/] = byte.MaxValue;
    numArray13[30] = (byte) 44;
    numArray13[50] = (byte) 26;
    numArray13[0] = (byte) 144 /*0x90*/;
    numArray13[36] = (byte) 151;
    numArray13[15] = (byte) 199;
    numArray13[11] = (byte) 176 /*0xB0*/;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[2]
    {
      (byte) 214,
      (byte) 156
    };
    byte[] numArray15 = new byte[2]
    {
      (byte) 152,
      (byte) 133
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 2);
    for (int index = 0; index < 2; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12635()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[159];
      byte[] numArray2 = new byte[55]
      {
        (byte) 248,
        (byte) 134,
        (byte) 29,
        (byte) 2,
        (byte) 64 /*0x40*/,
        (byte) 148,
        (byte) 4,
        (byte) 227,
        (byte) 230,
        (byte) 60,
        (byte) 50,
        (byte) 120,
        (byte) 167,
        (byte) 0,
        (byte) 126,
        (byte) 131,
        (byte) 222,
        (byte) 143,
        (byte) 172,
        (byte) 125,
        (byte) 152,
        (byte) 165,
        (byte) 95,
        (byte) 99,
        (byte) 109,
        (byte) 130,
        (byte) 156,
        (byte) 126,
        (byte) 226,
        (byte) 47,
        (byte) 32 /*0x20*/,
        (byte) 85,
        (byte) 112 /*0x70*/,
        (byte) 41,
        (byte) 83,
        (byte) 48 /*0x30*/,
        (byte) 9,
        (byte) 76,
        (byte) 222,
        (byte) 124,
        (byte) 8,
        (byte) 153,
        (byte) 178,
        (byte) 219,
        (byte) 225,
        (byte) 44,
        (byte) 60,
        (byte) 158,
        (byte) 151,
        (byte) 83,
        (byte) 190,
        (byte) 168,
        (byte) 101,
        (byte) 142,
        (byte) 129
      };
      byte[] numArray3 = new byte[55];
      numArray3[13] = (byte) 225;
      numArray3[1] = (byte) 164;
      numArray3[42] = (byte) 160 /*0xA0*/;
      numArray3[3] = (byte) 252;
      numArray3[6] = (byte) 19;
      numArray3[54] = (byte) 170;
      numArray3[39] = (byte) 206;
      numArray3[7] = (byte) 58;
      numArray3[4] = (byte) 64 /*0x40*/;
      numArray3[51] = (byte) 103;
      numArray3[8] = (byte) 82;
      numArray3[10] = (byte) 83;
      numArray3[12] = (byte) 216;
      numArray3[43] = (byte) 194;
      numArray3[49] = (byte) 84;
      numArray3[2] = (byte) 73;
      numArray3[16 /*0x10*/] = (byte) 37;
      numArray3[0] = (byte) 101;
      numArray3[41] = (byte) 253;
      numArray3[35] = (byte) 224 /*0xE0*/;
      numArray3[20] = (byte) 114;
      numArray3[9] = (byte) 87;
      numArray3[14] = (byte) 202;
      numArray3[23] = (byte) 98;
      numArray3[40] = (byte) 82;
      numArray3[25] = (byte) 81;
      numArray3[26] = (byte) 174;
      numArray3[22] = (byte) 10;
      numArray3[28] = (byte) 46;
      numArray3[30] = (byte) 28;
      numArray3[5] = (byte) 136;
      numArray3[38] = (byte) 202;
      numArray3[32 /*0x20*/] = (byte) 101;
      numArray3[33] = (byte) 153;
      numArray3[34] = (byte) 254;
      numArray3[17] = (byte) 248;
      numArray3[36] = (byte) 188;
      numArray3[37] = (byte) 10;
      numArray3[44] = (byte) 103;
      numArray3[31 /*0x1F*/] = (byte) 250;
      numArray3[21] = (byte) 170;
      numArray3[53] = (byte) 155;
      numArray3[29] = (byte) 232;
      numArray3[19] = (byte) 226;
      numArray3[18] = (byte) 214;
      numArray3[45] = (byte) 163;
      numArray3[46] = byte.MaxValue;
      numArray3[15] = (byte) 68;
      numArray3[47] = (byte) 90;
      numArray3[52] = (byte) 239;
      numArray3[50] = (byte) 63 /*0x3F*/;
      numArray3[11] = (byte) 180;
      numArray3[24] = (byte) 100;
      numArray3[48 /*0x30*/] = (byte) 63 /*0x3F*/;
      numArray3[27] = (byte) 71;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[13] = (byte) 43;
      numArray4[31 /*0x1F*/] = (byte) 37;
      numArray4[2] = (byte) 118;
      numArray4[3] = (byte) 249;
      numArray4[7] = (byte) 164;
      numArray4[5] = (byte) 113;
      numArray4[0] = (byte) 56;
      numArray4[43] = byte.MaxValue;
      numArray4[8] = (byte) 237;
      numArray4[32 /*0x20*/] = (byte) 159;
      numArray4[30] = (byte) 199;
      numArray4[21] = (byte) 125;
      numArray4[28] = (byte) 129;
      numArray4[19] = (byte) 247;
      numArray4[14] = (byte) 216;
      numArray4[12] = (byte) 67;
      numArray4[16 /*0x10*/] = (byte) 170;
      numArray4[17] = (byte) 241;
      numArray4[18] = (byte) 101;
      numArray4[40] = (byte) 180;
      numArray4[36] = (byte) 169;
      numArray4[35] = (byte) 101;
      numArray4[51] = (byte) 41;
      numArray4[39] = (byte) 39;
      numArray4[24] = (byte) 13;
      numArray4[20] = (byte) 196;
      numArray4[38] = (byte) 228;
      numArray4[27] = (byte) 66;
      numArray4[1] = (byte) 83;
      numArray4[29] = (byte) 185;
      numArray4[4] = (byte) 158;
      numArray4[25] = (byte) 156;
      numArray4[10] = (byte) 214;
      numArray4[33] = (byte) 103;
      numArray4[49] = (byte) 101;
      numArray4[23] = (byte) 136;
      numArray4[6] = (byte) 71;
      numArray4[37] = (byte) 139;
      numArray4[44] = (byte) 231;
      numArray4[11] = (byte) 173;
      numArray4[47] = (byte) 173;
      numArray4[9] = (byte) 37;
      numArray4[42] = (byte) 23;
      numArray4[34] = (byte) 21;
      numArray4[15] = (byte) 145;
      numArray4[26] = (byte) 181;
      numArray4[46] = (byte) 7;
      numArray4[41] = (byte) 27;
      numArray4[48 /*0x30*/] = (byte) 85;
      numArray4[53] = (byte) 105;
      numArray4[50] = (byte) 139;
      numArray4[52] = (byte) 30;
      numArray4[45] = (byte) 67;
      numArray4[22] = (byte) 28;
      numArray4[54] = (byte) 221;
      byte[] numArray5 = new byte[55]
      {
        (byte) 95,
        (byte) 144 /*0x90*/,
        (byte) 69,
        (byte) 232,
        (byte) 23,
        (byte) 180,
        (byte) 190,
        (byte) 90,
        (byte) 195,
        (byte) 234,
        (byte) 176 /*0xB0*/,
        (byte) 22,
        (byte) 77,
        (byte) 89,
        (byte) 190,
        (byte) 144 /*0x90*/,
        (byte) 210,
        (byte) 1,
        (byte) 48 /*0x30*/,
        (byte) 169,
        (byte) 205,
        (byte) 23,
        (byte) 150,
        (byte) 253,
        (byte) 95,
        (byte) 201,
        (byte) 210,
        (byte) 132,
        (byte) 195,
        (byte) 27,
        (byte) 225,
        (byte) 48 /*0x30*/,
        (byte) 202,
        (byte) 59,
        (byte) 55,
        (byte) 87,
        (byte) 26,
        (byte) 122,
        (byte) 111,
        (byte) 250,
        (byte) 35,
        (byte) 225,
        (byte) 87,
        (byte) 4,
        (byte) 58,
        (byte) 140,
        (byte) 211,
        (byte) 67,
        (byte) 96 /*0x60*/,
        (byte) 253,
        (byte) 28,
        (byte) 145,
        (byte) 60,
        (byte) 144 /*0x90*/,
        (byte) 242
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[49]
      {
        (byte) 77,
        (byte) 32 /*0x20*/,
        (byte) 109,
        (byte) 179,
        (byte) 23,
        (byte) 6,
        (byte) 4,
        (byte) 191,
        (byte) 170,
        (byte) 217,
        (byte) 80 /*0x50*/,
        (byte) 188,
        (byte) 69,
        (byte) 43,
        (byte) 126,
        (byte) 43,
        (byte) 210,
        (byte) 147,
        (byte) 176 /*0xB0*/,
        (byte) 130,
        (byte) 123,
        (byte) 106,
        (byte) 150,
        (byte) 105,
        (byte) 113,
        (byte) 11,
        (byte) 158,
        (byte) 194,
        (byte) 130,
        (byte) 38,
        (byte) 14,
        (byte) 86,
        (byte) 6,
        (byte) 20,
        (byte) 110,
        (byte) 127 /*0x7F*/,
        (byte) 116,
        (byte) 183,
        (byte) 152,
        (byte) 242,
        (byte) 223,
        (byte) 225,
        (byte) 140,
        (byte) 91,
        (byte) 219,
        (byte) 218,
        (byte) 156,
        (byte) 82,
        (byte) 238
      };
      byte[] numArray7 = new byte[49]
      {
        (byte) 147,
        (byte) 117,
        (byte) 4,
        (byte) 10,
        (byte) 83,
        (byte) 73,
        (byte) 1,
        (byte) 244,
        (byte) 105,
        (byte) 223,
        (byte) 183,
        (byte) 62,
        (byte) 199,
        (byte) 154,
        (byte) 139,
        (byte) 75,
        (byte) 74,
        (byte) 147,
        (byte) 224 /*0xE0*/,
        (byte) 233,
        (byte) 211,
        (byte) 80 /*0x50*/,
        (byte) 13,
        (byte) 190,
        (byte) 232,
        (byte) 156,
        (byte) 157,
        (byte) 194,
        (byte) 110,
        (byte) 159,
        (byte) 161,
        (byte) 7,
        (byte) 141,
        (byte) 154,
        (byte) 239,
        (byte) 236,
        (byte) 172,
        (byte) 11,
        (byte) 35,
        (byte) 157,
        (byte) 206,
        (byte) 207,
        (byte) 233,
        (byte) 44,
        (byte) 48 /*0x30*/,
        (byte) 166,
        (byte) 114,
        (byte) 6,
        (byte) 57
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[36];
      byte[] response = new byte[36];
      Array.Copy((Array) sc_12586.sspq, 603, (Array) numArray8, 0, 36);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12586.sspr, 603, (Array) numArray8, 0, 36);
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
    byte[] numArray9 = new byte[159];
    byte[] numArray10 = new byte[55];
    numArray10[11] = (byte) 36;
    numArray10[33] = (byte) 186;
    numArray10[22] = (byte) 79;
    numArray10[3] = (byte) 117;
    numArray10[39] = (byte) 51;
    numArray10[5] = (byte) 138;
    numArray10[6] = (byte) 157;
    numArray10[7] = (byte) 163;
    numArray10[8] = (byte) 8;
    numArray10[45] = (byte) 114;
    numArray10[15] = (byte) 58;
    numArray10[17] = (byte) 153;
    numArray10[4] = (byte) 140;
    numArray10[21] = (byte) 5;
    numArray10[14] = (byte) 239;
    numArray10[41] = (byte) 247;
    numArray10[35] = (byte) 9;
    numArray10[24] = (byte) 171;
    numArray10[13] = (byte) 149;
    numArray10[43] = (byte) 170;
    numArray10[51] = (byte) 225;
    numArray10[1] = (byte) 245;
    numArray10[42] = (byte) 212;
    numArray10[46] = (byte) 2;
    numArray10[23] = (byte) 118;
    numArray10[9] = (byte) 144 /*0x90*/;
    numArray10[44] = (byte) 172;
    numArray10[32 /*0x20*/] = (byte) 129;
    numArray10[28] = (byte) 172;
    numArray10[12] = (byte) 107;
    numArray10[30] = (byte) 80 /*0x50*/;
    numArray10[18] = (byte) 197;
    numArray10[52] = (byte) 13;
    numArray10[19] = (byte) 45;
    numArray10[20] = (byte) 3;
    numArray10[29] = (byte) 107;
    numArray10[36] = (byte) 70;
    numArray10[37] = (byte) 165;
    numArray10[38] = (byte) 109;
    numArray10[31 /*0x1F*/] = (byte) 38;
    numArray10[40] = (byte) 6;
    numArray10[2] = (byte) 112 /*0x70*/;
    numArray10[26] = (byte) 28;
    numArray10[34] = (byte) 49;
    numArray10[0] = (byte) 139;
    numArray10[27] = (byte) 110;
    numArray10[10] = (byte) 225;
    numArray10[16 /*0x10*/] = (byte) 243;
    numArray10[48 /*0x30*/] = (byte) 228;
    numArray10[49] = (byte) 86;
    numArray10[50] = (byte) 36;
    numArray10[25] = (byte) 157;
    numArray10[47] = (byte) 182;
    numArray10[53] = (byte) 6;
    numArray10[54] = (byte) 114;
    byte[] numArray11 = new byte[55]
    {
      (byte) 4,
      (byte) 6,
      (byte) 175,
      (byte) 35,
      (byte) 252,
      (byte) 89,
      (byte) 185,
      (byte) 81,
      (byte) 243,
      (byte) 173,
      (byte) 71,
      (byte) 110,
      (byte) 110,
      (byte) 188,
      (byte) 185,
      (byte) 213,
      (byte) 250,
      (byte) 164,
      (byte) 168,
      (byte) 124,
      (byte) 95,
      (byte) 84,
      (byte) 131,
      (byte) 139,
      (byte) 207,
      (byte) 91,
      (byte) 164,
      (byte) 178,
      (byte) 99,
      (byte) 221,
      (byte) 96 /*0x60*/,
      (byte) 19,
      (byte) 170,
      (byte) 39,
      (byte) 30,
      (byte) 219,
      (byte) 4,
      (byte) 174,
      (byte) 101,
      (byte) 214,
      (byte) 189,
      (byte) 246,
      (byte) 9,
      (byte) 205,
      (byte) 234,
      (byte) 78,
      (byte) 216,
      (byte) 127 /*0x7F*/,
      (byte) 236,
      (byte) 149,
      (byte) 29,
      (byte) 226,
      (byte) 198,
      (byte) 116,
      (byte) 38
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[48 /*0x30*/] = (byte) 98;
    numArray12[1] = (byte) 144 /*0x90*/;
    numArray12[2] = (byte) 84;
    numArray12[3] = (byte) 7;
    numArray12[4] = (byte) 165;
    numArray12[22] = (byte) 106;
    numArray12[53] = (byte) 91;
    numArray12[14] = (byte) 114;
    numArray12[8] = (byte) 161;
    numArray12[9] = (byte) 40;
    numArray12[10] = (byte) 104;
    numArray12[36] = (byte) 41;
    numArray12[43] = (byte) 165;
    numArray12[13] = (byte) 166;
    numArray12[51] = (byte) 142;
    numArray12[41] = (byte) 151;
    numArray12[15] = (byte) 173;
    numArray12[42] = (byte) 79;
    numArray12[47] = (byte) 189;
    numArray12[40] = (byte) 225;
    numArray12[20] = (byte) 59;
    numArray12[30] = (byte) 155;
    numArray12[31 /*0x1F*/] = (byte) 226;
    numArray12[23] = (byte) 73;
    numArray12[24] = (byte) 19;
    numArray12[54] = (byte) 196;
    numArray12[26] = (byte) 242;
    numArray12[52] = (byte) 90;
    numArray12[5] = (byte) 160 /*0xA0*/;
    numArray12[29] = (byte) 158;
    numArray12[25] = (byte) 138;
    numArray12[16 /*0x10*/] = (byte) 204;
    numArray12[32 /*0x20*/] = (byte) 87;
    numArray12[33] = (byte) 99;
    numArray12[34] = (byte) 29;
    numArray12[35] = (byte) 75;
    numArray12[0] = (byte) 47;
    numArray12[37] = (byte) 190;
    numArray12[38] = (byte) 49;
    numArray12[39] = (byte) 95;
    numArray12[12] = (byte) 130;
    numArray12[19] = (byte) 249;
    numArray12[6] = (byte) 86;
    numArray12[17] = (byte) 80 /*0x50*/;
    numArray12[44] = (byte) 232;
    numArray12[45] = (byte) 155;
    numArray12[46] = (byte) 124;
    numArray12[7] = (byte) 209;
    numArray12[49] = (byte) 7;
    numArray12[11] = (byte) 183;
    numArray12[50] = (byte) 175;
    numArray12[21] = (byte) 216;
    numArray12[27] = (byte) 113;
    numArray12[28] = (byte) 185;
    numArray12[18] = (byte) 12;
    byte[] numArray13 = new byte[55]
    {
      (byte) 34,
      (byte) 119,
      (byte) 186,
      (byte) 136,
      (byte) 33,
      (byte) 237,
      (byte) 235,
      (byte) 45,
      (byte) 128 /*0x80*/,
      (byte) 85,
      (byte) 29,
      (byte) 223,
      (byte) 8,
      (byte) 213,
      (byte) 250,
      (byte) 94,
      (byte) 87,
      (byte) 148,
      (byte) 3,
      (byte) 109,
      (byte) 221,
      (byte) 66,
      (byte) 126,
      (byte) 107,
      (byte) 78,
      (byte) 208 /*0xD0*/,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 216,
      (byte) 100,
      (byte) 54,
      (byte) 23,
      (byte) 32 /*0x20*/,
      byte.MaxValue,
      (byte) 218,
      (byte) 64 /*0x40*/,
      (byte) 162,
      (byte) 154,
      (byte) 229,
      (byte) 215,
      (byte) 96 /*0x60*/,
      (byte) 103,
      (byte) 139,
      (byte) 182,
      (byte) 74,
      (byte) 202,
      (byte) 56,
      (byte) 83,
      (byte) 138,
      (byte) 174,
      (byte) 159,
      (byte) 97,
      (byte) 158,
      (byte) 153,
      (byte) 220
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[49]
    {
      (byte) 170,
      (byte) 125,
      (byte) 12,
      (byte) 121,
      (byte) 214,
      (byte) 103,
      (byte) 20,
      (byte) 129,
      (byte) 254,
      (byte) 99,
      (byte) 127 /*0x7F*/,
      (byte) 194,
      (byte) 1,
      (byte) 101,
      (byte) 67,
      (byte) 7,
      (byte) 251,
      (byte) 27,
      (byte) 36,
      (byte) 60,
      (byte) 172,
      (byte) 254,
      (byte) 59,
      (byte) 176 /*0xB0*/,
      (byte) 1,
      (byte) 211,
      (byte) 45,
      (byte) 77,
      (byte) 155,
      (byte) 218,
      (byte) 185,
      (byte) 126,
      (byte) 248,
      (byte) 197,
      (byte) 115,
      (byte) 77,
      (byte) 10,
      (byte) 55,
      (byte) 38,
      (byte) 24,
      (byte) 56,
      (byte) 150,
      (byte) 98,
      (byte) 216,
      (byte) 222,
      (byte) 114,
      (byte) 58,
      (byte) 30,
      (byte) 94
    };
    byte[] numArray15 = new byte[49];
    numArray15[6] = (byte) 132;
    numArray15[17] = (byte) 97;
    numArray15[16 /*0x10*/] = (byte) 151;
    numArray15[3] = (byte) 105;
    numArray15[4] = (byte) 24;
    numArray15[47] = (byte) 122;
    numArray15[28] = (byte) 61;
    numArray15[7] = (byte) 108;
    numArray15[5] = (byte) 167;
    numArray15[26] = (byte) 201;
    numArray15[13] = (byte) 93;
    numArray15[11] = (byte) 203;
    numArray15[12] = (byte) 97;
    numArray15[2] = (byte) 123;
    numArray15[14] = (byte) 160 /*0xA0*/;
    numArray15[15] = (byte) 108;
    numArray15[8] = (byte) 231;
    numArray15[9] = (byte) 69;
    numArray15[18] = (byte) 100;
    numArray15[41] = (byte) 246;
    numArray15[20] = (byte) 250;
    numArray15[21] = (byte) 128 /*0x80*/;
    numArray15[22] = (byte) 104;
    numArray15[23] = (byte) 101;
    numArray15[24] = (byte) 224 /*0xE0*/;
    numArray15[25] = (byte) 185;
    numArray15[39] = (byte) 92;
    numArray15[37] = (byte) 168;
    numArray15[30] = (byte) 80 /*0x50*/;
    numArray15[40] = (byte) 238;
    numArray15[35] = (byte) 120;
    numArray15[29] = (byte) 237;
    numArray15[46] = (byte) 209;
    numArray15[33] = (byte) 29;
    numArray15[27] = (byte) 191;
    numArray15[38] = (byte) 188;
    numArray15[36] = (byte) 35;
    numArray15[31 /*0x1F*/] = (byte) 222;
    numArray15[34] = (byte) 178;
    numArray15[1] = (byte) 43;
    numArray15[19] = (byte) 207;
    numArray15[32 /*0x20*/] = (byte) 131;
    numArray15[42] = (byte) 81;
    numArray15[43] = (byte) 52;
    numArray15[44] = (byte) 111;
    numArray15[45] = (byte) 19;
    numArray15[48 /*0x30*/] = (byte) 74;
    numArray15[10] = (byte) 73;
    numArray15[0] = (byte) 2;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 49);
    for (int index = 0; index < 49; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12636()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 44,
        (byte) 104,
        (byte) 108,
        (byte) 237,
        (byte) 49,
        (byte) 239,
        (byte) 141,
        (byte) 157,
        (byte) 15
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 68,
        (byte) 31 /*0x1F*/,
        (byte) 149,
        (byte) 130,
        (byte) 68,
        (byte) 84,
        (byte) 30,
        (byte) 180,
        (byte) 188
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 106,
      (byte) 42,
      (byte) 216,
      (byte) 179,
      (byte) 252,
      (byte) 24,
      (byte) 126,
      (byte) 97,
      (byte) 41
    };
    byte[] numArray6 = new byte[9];
    numArray6[8] = (byte) 196;
    numArray6[1] = (byte) 101;
    numArray6[3] = (byte) 52;
    numArray6[6] = (byte) 124;
    numArray6[4] = (byte) 53;
    numArray6[0] = (byte) 223;
    numArray6[5] = (byte) 199;
    numArray6[7] = (byte) 68;
    numArray6[2] = (byte) 244;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12637()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9];
      numArray2[0] = (byte) 226;
      numArray2[3] = (byte) 59;
      numArray2[5] = (byte) 146;
      numArray2[7] = (byte) 116;
      numArray2[1] = (byte) 134;
      numArray2[4] = (byte) 96 /*0x60*/;
      numArray2[6] = (byte) 217;
      numArray2[2] = (byte) 197;
      numArray2[8] = (byte) 184;
      byte[] numArray3 = new byte[9]
      {
        (byte) 254,
        (byte) 58,
        (byte) 215,
        (byte) 231,
        (byte) 176 /*0xB0*/,
        (byte) 22,
        (byte) 50,
        (byte) 78,
        (byte) 154
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 2,
      (byte) 125,
      (byte) 29,
      (byte) 232,
      (byte) 94,
      (byte) 194,
      (byte) 251,
      (byte) 57,
      (byte) 177
    };
    byte[] numArray6 = new byte[9];
    numArray6[6] = (byte) 7;
    numArray6[1] = (byte) 160 /*0xA0*/;
    numArray6[0] = (byte) 246;
    numArray6[4] = (byte) 66;
    numArray6[5] = (byte) 80 /*0x50*/;
    numArray6[7] = (byte) 5;
    numArray6[2] = (byte) 53;
    numArray6[3] = (byte) 129;
    numArray6[8] = (byte) 137;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12638()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[75];
      byte[] numArray2 = new byte[55]
      {
        (byte) 190,
        (byte) 44,
        (byte) 46,
        (byte) 26,
        (byte) 121,
        (byte) 249,
        (byte) 103,
        (byte) 27,
        (byte) 31 /*0x1F*/,
        (byte) 147,
        (byte) 158,
        (byte) 195,
        (byte) 223,
        (byte) 8,
        (byte) 167,
        (byte) 82,
        (byte) 2,
        (byte) 208 /*0xD0*/,
        (byte) 221,
        (byte) 59,
        (byte) 6,
        (byte) 129,
        (byte) 138,
        (byte) 210,
        (byte) 231,
        (byte) 108,
        (byte) 70,
        (byte) 15,
        (byte) 129,
        (byte) 93,
        (byte) 116,
        (byte) 190,
        (byte) 33,
        (byte) 145,
        (byte) 190,
        (byte) 241,
        (byte) 186,
        (byte) 68,
        (byte) 73,
        (byte) 2,
        (byte) 209,
        (byte) 150,
        (byte) 138,
        (byte) 3,
        (byte) 101,
        (byte) 13,
        (byte) 103,
        (byte) 74,
        (byte) 70,
        (byte) 110,
        (byte) 181,
        (byte) 26,
        (byte) 170,
        (byte) 121,
        (byte) 105
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 6,
        (byte) 189,
        (byte) 15,
        (byte) 133,
        (byte) 214,
        (byte) 168,
        (byte) 63 /*0x3F*/,
        (byte) 162,
        (byte) 114,
        (byte) 59,
        (byte) 77,
        (byte) 65,
        (byte) 208 /*0xD0*/,
        (byte) 215,
        (byte) 254,
        (byte) 84,
        (byte) 167,
        (byte) 209,
        (byte) 21,
        (byte) 179,
        (byte) 116,
        (byte) 116,
        (byte) 50,
        (byte) 250,
        (byte) 18,
        (byte) 124,
        (byte) 146,
        (byte) 80 /*0x50*/,
        (byte) 235,
        (byte) 107,
        (byte) 178,
        (byte) 119,
        (byte) 131,
        (byte) 11,
        (byte) 107,
        (byte) 230,
        (byte) 170,
        (byte) 238,
        (byte) 17,
        (byte) 59,
        (byte) 124,
        (byte) 65,
        (byte) 48 /*0x30*/,
        (byte) 104,
        (byte) 96 /*0x60*/,
        (byte) 207,
        (byte) 116,
        (byte) 94,
        (byte) 117,
        (byte) 239,
        (byte) 240 /*0xF0*/,
        (byte) 15,
        (byte) 172,
        (byte) 181,
        (byte) 230
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20]
      {
        (byte) 91,
        (byte) 51,
        (byte) 12,
        (byte) 179,
        (byte) 88,
        (byte) 154,
        (byte) 194,
        (byte) 31 /*0x1F*/,
        (byte) 223,
        (byte) 162,
        (byte) 77,
        (byte) 73,
        (byte) 181,
        (byte) 152,
        (byte) 190,
        (byte) 135,
        (byte) 219,
        (byte) 105,
        (byte) 112 /*0x70*/,
        (byte) 246
      };
      byte[] numArray5 = new byte[20]
      {
        (byte) 113,
        (byte) 242,
        (byte) 144 /*0x90*/,
        (byte) 118,
        (byte) 50,
        (byte) 67,
        (byte) 54,
        (byte) 178,
        (byte) 4,
        (byte) 106,
        (byte) 34,
        (byte) 200,
        (byte) 114,
        (byte) 173,
        (byte) 137,
        (byte) 239,
        (byte) 128 /*0x80*/,
        (byte) 122,
        (byte) 214,
        (byte) 221
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[75];
    byte[] numArray7 = new byte[55]
    {
      (byte) 4,
      (byte) 252,
      (byte) 200,
      (byte) 18,
      (byte) 168,
      (byte) 89,
      (byte) 19,
      (byte) 167,
      (byte) 40,
      (byte) 129,
      (byte) 80 /*0x50*/,
      (byte) 94,
      (byte) 17,
      (byte) 60,
      (byte) 79,
      (byte) 121,
      (byte) 251,
      (byte) 200,
      (byte) 228,
      (byte) 133,
      (byte) 107,
      (byte) 26,
      (byte) 0,
      (byte) 145,
      (byte) 214,
      (byte) 246,
      (byte) 147,
      (byte) 207,
      (byte) 16 /*0x10*/,
      (byte) 249,
      (byte) 70,
      (byte) 26,
      (byte) 238,
      (byte) 89,
      (byte) 7,
      (byte) 182,
      (byte) 144 /*0x90*/,
      (byte) 135,
      (byte) 25,
      (byte) 197,
      (byte) 203,
      (byte) 251,
      (byte) 37,
      (byte) 199,
      (byte) 101,
      (byte) 57,
      (byte) 173,
      (byte) 74,
      (byte) 140,
      (byte) 240 /*0xF0*/,
      (byte) 116,
      (byte) 46,
      (byte) 163,
      (byte) 232,
      (byte) 109
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 136,
      (byte) 17,
      (byte) 205,
      (byte) 137,
      (byte) 111,
      (byte) 233,
      (byte) 83,
      (byte) 159,
      (byte) 127 /*0x7F*/,
      (byte) 235,
      (byte) 68,
      (byte) 176 /*0xB0*/,
      (byte) 240 /*0xF0*/,
      (byte) 24,
      (byte) 237,
      (byte) 240 /*0xF0*/,
      (byte) 188,
      (byte) 50,
      (byte) 174,
      (byte) 15,
      (byte) 83,
      (byte) 240 /*0xF0*/,
      (byte) 174,
      (byte) 124,
      (byte) 12,
      (byte) 230,
      (byte) 214,
      (byte) 245,
      (byte) 119,
      (byte) 52,
      (byte) 202,
      (byte) 77,
      (byte) 21,
      (byte) 233,
      (byte) 250,
      (byte) 69,
      (byte) 56,
      (byte) 179,
      (byte) 4,
      (byte) 121,
      (byte) 151,
      (byte) 110,
      (byte) 167,
      (byte) 122,
      (byte) 109,
      (byte) 82,
      (byte) 95,
      (byte) 250,
      (byte) 218,
      (byte) 205,
      (byte) 242,
      (byte) 65,
      (byte) 73,
      (byte) 31 /*0x1F*/,
      (byte) 26
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[20]
    {
      (byte) 55,
      (byte) 60,
      (byte) 177,
      (byte) 198,
      (byte) 68,
      (byte) 150,
      (byte) 46,
      (byte) 59,
      (byte) 53,
      (byte) 112 /*0x70*/,
      (byte) 183,
      (byte) 52,
      (byte) 152,
      (byte) 232,
      (byte) 116,
      (byte) 175,
      (byte) 30,
      (byte) 185,
      (byte) 112 /*0x70*/,
      (byte) 244
    };
    byte[] numArray10 = new byte[20]
    {
      (byte) 143,
      (byte) 100,
      (byte) 137,
      (byte) 68,
      (byte) 247,
      (byte) 20,
      (byte) 64 /*0x40*/,
      (byte) 74,
      (byte) 23,
      (byte) 84,
      (byte) 80 /*0x50*/,
      (byte) 253,
      (byte) 46,
      (byte) 146,
      (byte) 100,
      (byte) 172,
      (byte) 237,
      (byte) 222,
      (byte) 179,
      (byte) 145
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 20);
    for (int index = 0; index < 20; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12639(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 6;
    sourceArray1[1] = (byte) 32 /*0x20*/;
    sourceArray1[27] = (byte) 252;
    sourceArray1[47] = (byte) 50;
    sourceArray1[43] = (byte) 143;
    sourceArray1[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    sourceArray1[6] = (byte) 236;
    sourceArray1[35] = (byte) 117;
    sourceArray1[8] = (byte) 187;
    sourceArray1[11] = (byte) 104;
    sourceArray1[0] = (byte) 67;
    sourceArray1[2] = (byte) 132;
    sourceArray1[12] = byte.MaxValue;
    sourceArray1[18] = (byte) 201;
    sourceArray1[14] = (byte) 100;
    sourceArray1[15] = (byte) 247;
    sourceArray1[30] = (byte) 11;
    sourceArray1[36] = (byte) 170;
    sourceArray1[42] = (byte) 220;
    sourceArray1[7] = (byte) 29;
    sourceArray1[20] = (byte) 21;
    sourceArray1[17] = (byte) 47;
    sourceArray1[37] = (byte) 35;
    sourceArray1[10] = (byte) 74;
    sourceArray1[22] = (byte) 7;
    sourceArray1[21] = (byte) 243;
    sourceArray1[25] = (byte) 132;
    sourceArray1[9] = (byte) 89;
    sourceArray1[28] = (byte) 246;
    sourceArray1[40] = (byte) 203;
    sourceArray1[41] = (byte) 23;
    sourceArray1[29] = (byte) 194;
    sourceArray1[32 /*0x20*/] = (byte) 103;
    sourceArray1[33] = (byte) 124;
    sourceArray1[34] = (byte) 1;
    sourceArray1[16 /*0x10*/] = (byte) 140;
    sourceArray1[19] = (byte) 245;
    sourceArray1[39] = (byte) 106;
    sourceArray1[38] = (byte) 190;
    sourceArray1[4] = (byte) 119;
    sourceArray1[45] = (byte) 70;
    sourceArray1[13] = (byte) 135;
    sourceArray1[24] = (byte) 127 /*0x7F*/;
    sourceArray1[23] = (byte) 0;
    sourceArray1[44] = (byte) 41;
    sourceArray1[3] = (byte) 23;
    sourceArray1[46] = (byte) 30;
    sourceArray1[26] = (byte) 46;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 203,
      (byte) 10,
      (byte) 79,
      (byte) 198,
      (byte) 159,
      (byte) 122,
      (byte) 38,
      (byte) 89,
      (byte) 16 /*0x10*/,
      (byte) 171,
      (byte) 58,
      (byte) 154,
      (byte) 70,
      (byte) 157,
      (byte) 126,
      (byte) 56,
      (byte) 87,
      (byte) 118,
      (byte) 167,
      (byte) 115,
      (byte) 68,
      (byte) 232,
      (byte) 49,
      (byte) 249,
      (byte) 130,
      (byte) 155,
      (byte) 228,
      (byte) 56,
      byte.MaxValue,
      (byte) 35,
      (byte) 210,
      (byte) 55,
      (byte) 106,
      (byte) 192 /*0xC0*/,
      (byte) 55,
      (byte) 32 /*0x20*/,
      (byte) 91,
      (byte) 232,
      (byte) 126,
      (byte) 59,
      (byte) 160 /*0xA0*/,
      (byte) 54,
      (byte) 216,
      (byte) 60,
      (byte) 195,
      (byte) 79,
      (byte) 190
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12640(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 155,
      (byte) 172,
      (byte) 29,
      (byte) 176 /*0xB0*/,
      (byte) 68,
      (byte) 138,
      (byte) 115,
      (byte) 41,
      (byte) 53,
      (byte) 55,
      (byte) 161,
      (byte) 173,
      (byte) 96 /*0x60*/,
      (byte) 140,
      (byte) 219,
      (byte) 254,
      (byte) 46,
      (byte) 228,
      (byte) 61,
      (byte) 220,
      (byte) 236,
      (byte) 58,
      (byte) 211,
      (byte) 145,
      (byte) 38,
      (byte) 221,
      (byte) 164,
      (byte) 204,
      (byte) 79,
      (byte) 13,
      (byte) 51,
      (byte) 53,
      (byte) 87,
      (byte) 241,
      (byte) 203,
      (byte) 79,
      (byte) 211,
      (byte) 72,
      (byte) 207,
      (byte) 133,
      (byte) 89,
      (byte) 234,
      (byte) 19,
      (byte) 122,
      (byte) 187,
      (byte) 121,
      (byte) 42,
      (byte) 64 /*0x40*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 136,
      (byte) 216,
      (byte) 61,
      (byte) 167,
      (byte) 145,
      (byte) 33,
      (byte) 27,
      (byte) 148,
      (byte) 155,
      (byte) 144 /*0x90*/,
      (byte) 195,
      (byte) 44,
      (byte) 173,
      (byte) 210,
      (byte) 169,
      (byte) 51,
      (byte) 29,
      (byte) 70,
      (byte) 98,
      (byte) 243,
      (byte) 95,
      (byte) 156,
      (byte) 231,
      (byte) 57,
      (byte) 48 /*0x30*/,
      byte.MaxValue,
      (byte) 253,
      (byte) 79,
      (byte) 227,
      (byte) 52,
      (byte) 116,
      (byte) 95,
      (byte) 84,
      (byte) 202,
      (byte) 222,
      (byte) 205,
      (byte) 8,
      (byte) 200,
      (byte) 3,
      (byte) 174,
      (byte) 214,
      (byte) 71,
      (byte) 72,
      (byte) 250,
      (byte) 3,
      (byte) 161,
      (byte) 140,
      (byte) 194
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12641(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 72;
    sourceArray1[32 /*0x20*/] = (byte) 52;
    sourceArray1[2] = (byte) 96 /*0x60*/;
    sourceArray1[3] = (byte) 217;
    sourceArray1[4] = (byte) 139;
    sourceArray1[5] = (byte) 138;
    sourceArray1[6] = (byte) 103;
    sourceArray1[41] = (byte) 160 /*0xA0*/;
    sourceArray1[23] = (byte) 39;
    sourceArray1[37] = (byte) 117;
    sourceArray1[10] = (byte) 39;
    sourceArray1[11] = (byte) 4;
    sourceArray1[36] = (byte) 106;
    sourceArray1[39] = (byte) 189;
    sourceArray1[14] = (byte) 110;
    sourceArray1[19] = (byte) 20;
    sourceArray1[0] = (byte) 162;
    sourceArray1[17] = (byte) 42;
    sourceArray1[18] = (byte) 229;
    sourceArray1[7] = (byte) 229;
    sourceArray1[27] = (byte) 178;
    sourceArray1[21] = (byte) 130;
    sourceArray1[22] = (byte) 2;
    sourceArray1[29] = (byte) 21;
    sourceArray1[16 /*0x10*/] = (byte) 140;
    sourceArray1[25] = (byte) 214;
    sourceArray1[26] = (byte) 172;
    sourceArray1[24] = (byte) 88;
    sourceArray1[1] = (byte) 89;
    sourceArray1[15] = (byte) 192 /*0xC0*/;
    sourceArray1[40] = (byte) 104;
    sourceArray1[28] = (byte) 100;
    sourceArray1[31 /*0x1F*/] = (byte) 76;
    sourceArray1[13] = (byte) 85;
    sourceArray1[12] = (byte) 107;
    sourceArray1[35] = (byte) 152;
    sourceArray1[30] = (byte) 113;
    sourceArray1[47] = (byte) 35;
    sourceArray1[8] = (byte) 166;
    sourceArray1[20] = (byte) 183;
    sourceArray1[9] = (byte) 218;
    sourceArray1[34] = (byte) 180;
    sourceArray1[42] = (byte) 179;
    sourceArray1[43] = (byte) 15;
    sourceArray1[44] = (byte) 235;
    sourceArray1[45] = (byte) 118;
    sourceArray1[46] = (byte) 79;
    sourceArray1[33] = (byte) 48 /*0x30*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 168,
      (byte) 136,
      (byte) 221,
      (byte) 17,
      (byte) 140,
      (byte) 31 /*0x1F*/,
      (byte) 163,
      (byte) 43,
      (byte) 104,
      (byte) 178,
      (byte) 241,
      (byte) 25,
      (byte) 28,
      (byte) 233,
      (byte) 84,
      (byte) 29,
      (byte) 79,
      (byte) 72,
      (byte) 23,
      (byte) 174,
      (byte) 212,
      (byte) 178,
      (byte) 119,
      (byte) 8,
      (byte) 104,
      (byte) 194,
      (byte) 150,
      (byte) 216,
      (byte) 49,
      (byte) 52,
      (byte) 233,
      (byte) 18,
      (byte) 35,
      (byte) 9,
      (byte) 120,
      (byte) 183,
      (byte) 55,
      (byte) 36,
      (byte) 142,
      (byte) 42,
      (byte) 161,
      (byte) 128 /*0x80*/,
      (byte) 184,
      (byte) 191,
      (byte) 153,
      (byte) 48 /*0x30*/,
      (byte) 177,
      (byte) 90
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12642(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 21,
      (byte) 130,
      (byte) 230,
      (byte) 41,
      (byte) 19,
      (byte) 209,
      (byte) 179,
      (byte) 61,
      (byte) 54,
      (byte) 80 /*0x50*/,
      (byte) 99,
      (byte) 253,
      (byte) 158,
      (byte) 46,
      (byte) 85,
      (byte) 192 /*0xC0*/,
      (byte) 155,
      (byte) 165,
      (byte) 169,
      (byte) 107,
      (byte) 132,
      (byte) 87,
      (byte) 12,
      (byte) 147,
      (byte) 37,
      (byte) 35,
      (byte) 77,
      (byte) 72,
      (byte) 249,
      (byte) 247,
      (byte) 173,
      (byte) 75,
      (byte) 44,
      (byte) 59,
      (byte) 113,
      (byte) 6,
      (byte) 14,
      (byte) 187,
      (byte) 215,
      (byte) 237,
      (byte) 188,
      (byte) 12,
      (byte) 173,
      (byte) 67,
      (byte) 222,
      (byte) 51,
      (byte) 85,
      (byte) 101
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[37] = (byte) 188;
    sourceArray2[13] = (byte) 90;
    sourceArray2[2] = (byte) 215;
    sourceArray2[3] = (byte) 24;
    sourceArray2[4] = (byte) 165;
    sourceArray2[23] = (byte) 157;
    sourceArray2[26] = (byte) 226;
    sourceArray2[6] = (byte) 55;
    sourceArray2[8] = (byte) 237;
    sourceArray2[38] = (byte) 24;
    sourceArray2[7] = (byte) 218;
    sourceArray2[18] = (byte) 167;
    sourceArray2[10] = (byte) 179;
    sourceArray2[0] = (byte) 170;
    sourceArray2[35] = (byte) 239;
    sourceArray2[15] = (byte) 172;
    sourceArray2[45] = (byte) 252;
    sourceArray2[17] = (byte) 17;
    sourceArray2[12] = (byte) 76;
    sourceArray2[9] = (byte) 150;
    sourceArray2[20] = (byte) 118;
    sourceArray2[11] = (byte) 191;
    sourceArray2[22] = (byte) 199;
    sourceArray2[5] = (byte) 35;
    sourceArray2[42] = (byte) 179;
    sourceArray2[1] = (byte) 167;
    sourceArray2[31 /*0x1F*/] = (byte) 80 /*0x50*/;
    sourceArray2[27] = (byte) 119;
    sourceArray2[28] = (byte) 91;
    sourceArray2[25] = (byte) 144 /*0x90*/;
    sourceArray2[30] = (byte) 183;
    sourceArray2[21] = (byte) 31 /*0x1F*/;
    sourceArray2[40] = (byte) 7;
    sourceArray2[33] = (byte) 188;
    sourceArray2[16 /*0x10*/] = (byte) 100;
    sourceArray2[41] = (byte) 194;
    sourceArray2[36] = (byte) 234;
    sourceArray2[24] = (byte) 158;
    sourceArray2[19] = (byte) 150;
    sourceArray2[39] = (byte) 63 /*0x3F*/;
    sourceArray2[32 /*0x20*/] = (byte) 251;
    sourceArray2[14] = (byte) 221;
    sourceArray2[29] = (byte) 212;
    sourceArray2[43] = (byte) 204;
    sourceArray2[44] = (byte) 14;
    sourceArray2[34] = (byte) 201;
    sourceArray2[46] = (byte) 50;
    sourceArray2[47] = (byte) 247;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12643()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[149];
      byte[] numArray2 = new byte[55]
      {
        (byte) 97,
        (byte) 242,
        (byte) 168,
        (byte) 58,
        (byte) 65,
        (byte) 164,
        (byte) 174,
        (byte) 65,
        (byte) 245,
        (byte) 170,
        (byte) 20,
        (byte) 172,
        (byte) 157,
        (byte) 17,
        (byte) 34,
        (byte) 155,
        (byte) 172,
        (byte) 61,
        (byte) 204,
        (byte) 114,
        (byte) 202,
        (byte) 73,
        (byte) 73,
        (byte) 9,
        (byte) 190,
        (byte) 246,
        (byte) 39,
        (byte) 230,
        (byte) 82,
        (byte) 93,
        (byte) 175,
        (byte) 60,
        (byte) 40,
        (byte) 240 /*0xF0*/,
        (byte) 144 /*0x90*/,
        (byte) 218,
        (byte) 63 /*0x3F*/,
        (byte) 131,
        (byte) 197,
        (byte) 206,
        (byte) 130,
        (byte) 12,
        (byte) 146,
        (byte) 111,
        (byte) 159,
        (byte) 228,
        (byte) 30,
        (byte) 166,
        (byte) 196,
        (byte) 206,
        (byte) 164,
        (byte) 139,
        (byte) 161,
        (byte) 46,
        (byte) 251
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 234,
        (byte) 222,
        (byte) 242,
        (byte) 54,
        (byte) 75,
        (byte) 50,
        (byte) 77,
        (byte) 182,
        (byte) 194,
        (byte) 165,
        (byte) 156,
        (byte) 177,
        (byte) 136,
        (byte) 47,
        (byte) 250,
        (byte) 252,
        (byte) 125,
        (byte) 184,
        (byte) 198,
        (byte) 50,
        (byte) 116,
        (byte) 60,
        (byte) 6,
        (byte) 6,
        (byte) 24,
        (byte) 172,
        (byte) 168,
        (byte) 31 /*0x1F*/,
        (byte) 43,
        (byte) 228,
        (byte) 149,
        (byte) 160 /*0xA0*/,
        (byte) 215,
        (byte) 0,
        (byte) 157,
        (byte) 117,
        (byte) 196,
        (byte) 48 /*0x30*/,
        (byte) 199,
        (byte) 64 /*0x40*/,
        (byte) 97,
        (byte) 183,
        (byte) 129,
        (byte) 146,
        (byte) 245,
        (byte) 201,
        (byte) 37,
        (byte) 173,
        (byte) 41,
        (byte) 251,
        (byte) 176 /*0xB0*/,
        (byte) 161,
        (byte) 137,
        (byte) 155,
        (byte) 226
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[17] = (byte) 240 /*0xF0*/;
      numArray4[1] = (byte) 124;
      numArray4[46] = (byte) 124;
      numArray4[3] = (byte) 125;
      numArray4[30] = (byte) 249;
      numArray4[5] = (byte) 238;
      numArray4[29] = (byte) 154;
      numArray4[10] = (byte) 173;
      numArray4[44] = (byte) 113;
      numArray4[9] = (byte) 132;
      numArray4[33] = (byte) 49;
      numArray4[11] = (byte) 128 /*0x80*/;
      numArray4[12] = (byte) 72;
      numArray4[35] = (byte) 7;
      numArray4[34] = (byte) 159;
      numArray4[26] = (byte) 131;
      numArray4[16 /*0x10*/] = (byte) 254;
      numArray4[25] = (byte) 33;
      numArray4[18] = (byte) 189;
      numArray4[42] = (byte) 199;
      numArray4[21] = (byte) 105;
      numArray4[2] = (byte) 75;
      numArray4[50] = (byte) 162;
      numArray4[23] = (byte) 215;
      numArray4[8] = (byte) 216;
      numArray4[22] = (byte) 217;
      numArray4[6] = (byte) 152;
      numArray4[27] = (byte) 184;
      numArray4[20] = (byte) 218;
      numArray4[24] = (byte) 128 /*0x80*/;
      numArray4[31 /*0x1F*/] = (byte) 244;
      numArray4[38] = (byte) 186;
      numArray4[32 /*0x20*/] = (byte) 120;
      numArray4[28] = (byte) 35;
      numArray4[19] = (byte) 182;
      numArray4[4] = (byte) 200;
      numArray4[36] = (byte) 122;
      numArray4[37] = (byte) 80 /*0x50*/;
      numArray4[54] = (byte) 207;
      numArray4[39] = (byte) 29;
      numArray4[40] = (byte) 250;
      numArray4[41] = (byte) 116;
      numArray4[52] = (byte) 61;
      numArray4[43] = (byte) 96 /*0x60*/;
      numArray4[13] = (byte) 91;
      numArray4[0] = (byte) 159;
      numArray4[14] = (byte) 41;
      numArray4[47] = (byte) 114;
      numArray4[48 /*0x30*/] = (byte) 123;
      numArray4[49] = (byte) 20;
      numArray4[15] = (byte) 119;
      numArray4[45] = (byte) 38;
      numArray4[7] = (byte) 250;
      numArray4[53] = (byte) 97;
      numArray4[51] = (byte) 62;
      byte[] numArray5 = new byte[55];
      numArray5[19] = (byte) 186;
      numArray5[1] = (byte) 236;
      numArray5[13] = (byte) 253;
      numArray5[9] = (byte) 65;
      numArray5[21] = (byte) 222;
      numArray5[6] = (byte) 147;
      numArray5[54] = (byte) 141;
      numArray5[32 /*0x20*/] = (byte) 52;
      numArray5[8] = (byte) 99;
      numArray5[5] = (byte) 201;
      numArray5[40] = (byte) 183;
      numArray5[43] = (byte) 165;
      numArray5[37] = (byte) 39;
      numArray5[2] = (byte) 92;
      numArray5[14] = (byte) 153;
      numArray5[45] = (byte) 42;
      numArray5[16 /*0x10*/] = (byte) 79;
      numArray5[15] = (byte) 173;
      numArray5[46] = (byte) 169;
      numArray5[18] = (byte) 187;
      numArray5[3] = (byte) 235;
      numArray5[25] = (byte) 184;
      numArray5[48 /*0x30*/] = (byte) 62;
      numArray5[23] = (byte) 36;
      numArray5[24] = (byte) 19;
      numArray5[52] = (byte) 13;
      numArray5[26] = (byte) 242;
      numArray5[11] = (byte) 212;
      numArray5[28] = (byte) 21;
      numArray5[29] = (byte) 207;
      numArray5[30] = (byte) 64 /*0x40*/;
      numArray5[20] = (byte) 208 /*0xD0*/;
      numArray5[12] = (byte) 209;
      numArray5[33] = (byte) 85;
      numArray5[34] = (byte) 240 /*0xF0*/;
      numArray5[35] = (byte) 26;
      numArray5[36] = (byte) 244;
      numArray5[51] = (byte) 166;
      numArray5[17] = (byte) 66;
      numArray5[39] = (byte) 53;
      numArray5[4] = (byte) 70;
      numArray5[41] = (byte) 238;
      numArray5[42] = (byte) 217;
      numArray5[7] = (byte) 181;
      numArray5[44] = (byte) 20;
      numArray5[27] = (byte) 196;
      numArray5[31 /*0x1F*/] = (byte) 132;
      numArray5[47] = (byte) 146;
      numArray5[10] = (byte) 82;
      numArray5[49] = (byte) 171;
      numArray5[0] = (byte) 72;
      numArray5[50] = (byte) 197;
      numArray5[22] = (byte) 20;
      numArray5[53] = (byte) 166;
      numArray5[38] = (byte) 117;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[39];
      numArray6[27] = (byte) 103;
      numArray6[29] = (byte) 105;
      numArray6[1] = (byte) 102;
      numArray6[3] = (byte) 254;
      numArray6[4] = (byte) 193;
      numArray6[5] = (byte) 207;
      numArray6[22] = (byte) 14;
      numArray6[7] = (byte) 26;
      numArray6[9] = (byte) 126;
      numArray6[15] = (byte) 17;
      numArray6[16 /*0x10*/] = (byte) 147;
      numArray6[11] = (byte) 123;
      numArray6[12] = (byte) 128 /*0x80*/;
      numArray6[18] = (byte) 242;
      numArray6[14] = (byte) 166;
      numArray6[21] = (byte) 246;
      numArray6[30] = (byte) 110;
      numArray6[37] = (byte) 90;
      numArray6[2] = (byte) 194;
      numArray6[19] = (byte) 210;
      numArray6[20] = (byte) 155;
      numArray6[38] = (byte) 181;
      numArray6[31 /*0x1F*/] = (byte) 86;
      numArray6[23] = (byte) 143;
      numArray6[24] = (byte) 246;
      numArray6[25] = (byte) 69;
      numArray6[26] = (byte) 213;
      numArray6[10] = (byte) 189;
      numArray6[34] = (byte) 154;
      numArray6[0] = (byte) 95;
      numArray6[13] = (byte) 251;
      numArray6[28] = (byte) 206;
      numArray6[36] = (byte) 78;
      numArray6[33] = (byte) 182;
      numArray6[6] = (byte) 65;
      numArray6[32 /*0x20*/] = (byte) 91;
      numArray6[17] = (byte) 42;
      numArray6[8] = (byte) 231;
      numArray6[35] = (byte) 76;
      byte[] numArray7 = new byte[39]
      {
        (byte) 32 /*0x20*/,
        (byte) 47,
        (byte) 144 /*0x90*/,
        (byte) 140,
        (byte) 39,
        (byte) 51,
        (byte) 141,
        (byte) 150,
        (byte) 122,
        (byte) 228,
        (byte) 175,
        (byte) 235,
        (byte) 205,
        (byte) 140,
        (byte) 177,
        (byte) 184,
        (byte) 12,
        (byte) 190,
        (byte) 31 /*0x1F*/,
        (byte) 166,
        (byte) 249,
        (byte) 3,
        (byte) 241,
        (byte) 71,
        (byte) 227,
        (byte) 2,
        (byte) 148,
        (byte) 63 /*0x3F*/,
        (byte) 78,
        (byte) 217,
        (byte) 208 /*0xD0*/,
        (byte) 56,
        (byte) 101,
        (byte) 251,
        (byte) 29,
        (byte) 226,
        (byte) 244,
        (byte) 178,
        (byte) 148
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[149];
    byte[] numArray9 = new byte[55];
    numArray9[21] = (byte) 198;
    numArray9[1] = (byte) 113;
    numArray9[2] = (byte) 250;
    numArray9[53] = (byte) 68;
    numArray9[4] = (byte) 150;
    numArray9[17] = (byte) 158;
    numArray9[20] = (byte) 83;
    numArray9[7] = (byte) 204;
    numArray9[8] = (byte) 49;
    numArray9[9] = (byte) 243;
    numArray9[36] = (byte) 81;
    numArray9[33] = (byte) 83;
    numArray9[12] = (byte) 58;
    numArray9[30] = (byte) 249;
    numArray9[14] = (byte) 41;
    numArray9[15] = (byte) 161;
    numArray9[0] = (byte) 174;
    numArray9[28] = (byte) 60;
    numArray9[24] = (byte) 227;
    numArray9[52] = (byte) 17;
    numArray9[50] = (byte) 212;
    numArray9[41] = (byte) 252;
    numArray9[22] = (byte) 154;
    numArray9[23] = (byte) 17;
    numArray9[49] = (byte) 12;
    numArray9[6] = (byte) 179;
    numArray9[26] = (byte) 196;
    numArray9[27] = (byte) 217;
    numArray9[19] = (byte) 201;
    numArray9[29] = (byte) 67;
    numArray9[11] = (byte) 82;
    numArray9[31 /*0x1F*/] = (byte) 150;
    numArray9[10] = (byte) 27;
    numArray9[25] = (byte) 99;
    numArray9[34] = (byte) 74;
    numArray9[43] = (byte) 102;
    numArray9[35] = (byte) 104;
    numArray9[37] = (byte) 48 /*0x30*/;
    numArray9[38] = (byte) 121;
    numArray9[5] = (byte) 166;
    numArray9[32 /*0x20*/] = (byte) 250;
    numArray9[16 /*0x10*/] = (byte) 81;
    numArray9[40] = (byte) 108;
    numArray9[42] = (byte) 47;
    numArray9[44] = (byte) 167;
    numArray9[45] = (byte) 230;
    numArray9[46] = (byte) 58;
    numArray9[51] = (byte) 191;
    numArray9[48 /*0x30*/] = (byte) 165;
    numArray9[13] = (byte) 13;
    numArray9[18] = (byte) 139;
    numArray9[47] = (byte) 221;
    numArray9[39] = (byte) 192 /*0xC0*/;
    numArray9[3] = (byte) 77;
    numArray9[54] = (byte) 213;
    byte[] numArray10 = new byte[55];
    numArray10[40] = (byte) 235;
    numArray10[53] = (byte) 222;
    numArray10[39] = (byte) 17;
    numArray10[19] = (byte) 139;
    numArray10[1] = (byte) 234;
    numArray10[5] = (byte) 5;
    numArray10[43] = (byte) 35;
    numArray10[12] = (byte) 3;
    numArray10[21] = (byte) 51;
    numArray10[29] = (byte) 198;
    numArray10[10] = (byte) 121;
    numArray10[11] = (byte) 25;
    numArray10[6] = (byte) 91;
    numArray10[13] = (byte) 193;
    numArray10[14] = (byte) 144 /*0x90*/;
    numArray10[15] = (byte) 219;
    numArray10[16 /*0x10*/] = (byte) 101;
    numArray10[44] = (byte) 202;
    numArray10[18] = (byte) 231;
    numArray10[54] = (byte) 6;
    numArray10[20] = (byte) 156;
    numArray10[3] = (byte) 207;
    numArray10[22] = (byte) 63 /*0x3F*/;
    numArray10[23] = (byte) 165;
    numArray10[25] = (byte) 245;
    numArray10[36] = (byte) 63 /*0x3F*/;
    numArray10[26] = (byte) 59;
    numArray10[27] = (byte) 233;
    numArray10[34] = (byte) 134;
    numArray10[45] = (byte) 57;
    numArray10[30] = (byte) 184;
    numArray10[38] = (byte) 172;
    numArray10[32 /*0x20*/] = (byte) 41;
    numArray10[35] = (byte) 38;
    numArray10[47] = (byte) 146;
    numArray10[33] = (byte) 14;
    numArray10[4] = (byte) 120;
    numArray10[17] = (byte) 165;
    numArray10[51] = (byte) 116;
    numArray10[28] = (byte) 250;
    numArray10[0] = (byte) 209;
    numArray10[7] = (byte) 227;
    numArray10[42] = (byte) 220;
    numArray10[8] = (byte) 166;
    numArray10[41] = (byte) 116;
    numArray10[48 /*0x30*/] = (byte) 53;
    numArray10[46] = (byte) 95;
    numArray10[24] = (byte) 63 /*0x3F*/;
    numArray10[37] = (byte) 39;
    numArray10[49] = (byte) 216;
    numArray10[50] = (byte) 131;
    numArray10[2] = (byte) 124;
    numArray10[52] = (byte) 243;
    numArray10[31 /*0x1F*/] = (byte) 237;
    numArray10[9] = (byte) 164;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 55,
      (byte) 252,
      (byte) 55,
      (byte) 49,
      (byte) 88,
      (byte) 237,
      (byte) 195,
      (byte) 34,
      (byte) 194,
      (byte) 212,
      (byte) 103,
      (byte) 81,
      (byte) 63 /*0x3F*/,
      (byte) 1,
      (byte) 254,
      (byte) 163,
      (byte) 11,
      (byte) 220,
      (byte) 223,
      (byte) 145,
      (byte) 29,
      (byte) 38,
      (byte) 132,
      (byte) 230,
      (byte) 16 /*0x10*/,
      (byte) 150,
      (byte) 160 /*0xA0*/,
      (byte) 97,
      (byte) 226,
      (byte) 228,
      (byte) 174,
      (byte) 106,
      (byte) 17,
      (byte) 20,
      (byte) 175,
      (byte) 94,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 148,
      (byte) 29,
      byte.MaxValue,
      (byte) 136,
      (byte) 95,
      (byte) 22,
      (byte) 133,
      (byte) 105,
      (byte) 119,
      (byte) 210,
      (byte) 91,
      (byte) 24,
      (byte) 78,
      (byte) 66,
      (byte) 181,
      (byte) 108,
      (byte) 189
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 100,
      (byte) 169,
      (byte) 135,
      (byte) 58,
      (byte) 189,
      (byte) 222,
      (byte) 107,
      (byte) 23,
      (byte) 206,
      (byte) 67,
      (byte) 33,
      (byte) 71,
      (byte) 166,
      (byte) 207,
      (byte) 176 /*0xB0*/,
      (byte) 3,
      (byte) 165,
      (byte) 62,
      (byte) 182,
      (byte) 120,
      (byte) 194,
      (byte) 201,
      (byte) 57,
      (byte) 48 /*0x30*/,
      (byte) 5,
      (byte) 224 /*0xE0*/,
      (byte) 80 /*0x50*/,
      (byte) 177,
      (byte) 89,
      (byte) 187,
      (byte) 89,
      (byte) 152,
      (byte) 48 /*0x30*/,
      (byte) 135,
      (byte) 73,
      (byte) 151,
      (byte) 72,
      (byte) 36,
      (byte) 207,
      (byte) 113,
      (byte) 100,
      (byte) 212,
      (byte) 14,
      (byte) 164,
      (byte) 223,
      (byte) 110,
      (byte) 237,
      (byte) 250,
      (byte) 3,
      (byte) 162,
      (byte) 143,
      (byte) 78,
      (byte) 33,
      (byte) 119,
      (byte) 53
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[39]
    {
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 193,
      (byte) 76,
      (byte) 97,
      (byte) 63 /*0x3F*/,
      (byte) 221,
      (byte) 38,
      (byte) 252,
      (byte) 63 /*0x3F*/,
      (byte) 43,
      (byte) 81,
      (byte) 159,
      (byte) 158,
      (byte) 90,
      (byte) 72,
      (byte) 193,
      (byte) 145,
      (byte) 149,
      (byte) 199,
      (byte) 109,
      (byte) 96 /*0x60*/,
      (byte) 166,
      (byte) 31 /*0x1F*/,
      (byte) 206,
      (byte) 6,
      (byte) 202,
      (byte) 149,
      (byte) 108,
      (byte) 181,
      (byte) 164,
      (byte) 86,
      (byte) 158,
      (byte) 230,
      (byte) 179,
      (byte) 143,
      (byte) 211,
      (byte) 208 /*0xD0*/,
      (byte) 158
    };
    byte[] numArray14 = new byte[39]
    {
      (byte) 43,
      (byte) 76,
      (byte) 189,
      (byte) 240 /*0xF0*/,
      (byte) 46,
      (byte) 74,
      (byte) 35,
      (byte) 187,
      (byte) 182,
      (byte) 103,
      (byte) 185,
      (byte) 57,
      (byte) 156,
      (byte) 172,
      (byte) 253,
      (byte) 171,
      (byte) 137,
      (byte) 147,
      (byte) 110,
      (byte) 148,
      (byte) 158,
      (byte) 119,
      (byte) 33,
      (byte) 155,
      (byte) 91,
      (byte) 154,
      (byte) 38,
      (byte) 37,
      (byte) 2,
      (byte) 4,
      (byte) 193,
      (byte) 187,
      (byte) 45,
      (byte) 51,
      (byte) 161,
      (byte) 56,
      (byte) 10,
      (byte) 56,
      (byte) 104
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 39);
    for (int index = 0; index < 39; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12644()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[162];
      byte[] numArray2 = new byte[55]
      {
        (byte) 42,
        (byte) 76,
        (byte) 128 /*0x80*/,
        (byte) 187,
        (byte) 122,
        (byte) 244,
        (byte) 250,
        (byte) 244,
        (byte) 219,
        (byte) 40,
        (byte) 74,
        (byte) 10,
        (byte) 240 /*0xF0*/,
        (byte) 122,
        (byte) 161,
        (byte) 126,
        (byte) 90,
        (byte) 117,
        (byte) 72,
        (byte) 185,
        (byte) 71,
        (byte) 154,
        (byte) 212,
        (byte) 178,
        (byte) 185,
        (byte) 234,
        (byte) 202,
        (byte) 116,
        (byte) 241,
        (byte) 242,
        (byte) 77,
        (byte) 70,
        (byte) 125,
        (byte) 215,
        (byte) 124,
        (byte) 211,
        (byte) 77,
        (byte) 75,
        (byte) 79,
        (byte) 161,
        (byte) 114,
        (byte) 193,
        (byte) 201,
        (byte) 46,
        (byte) 84,
        (byte) 220,
        (byte) 195,
        (byte) 21,
        (byte) 49,
        (byte) 82,
        (byte) 67,
        (byte) 210,
        (byte) 57,
        (byte) 54,
        (byte) 65
      };
      byte[] numArray3 = new byte[55];
      numArray3[14] = (byte) 195;
      numArray3[11] = (byte) 116;
      numArray3[2] = (byte) 14;
      numArray3[40] = (byte) 25;
      numArray3[4] = (byte) 202;
      numArray3[8] = (byte) 249;
      numArray3[6] = (byte) 43;
      numArray3[7] = (byte) 84;
      numArray3[23] = (byte) 176 /*0xB0*/;
      numArray3[26] = (byte) 169;
      numArray3[49] = (byte) 181;
      numArray3[22] = (byte) 27;
      numArray3[3] = (byte) 187;
      numArray3[13] = (byte) 116;
      numArray3[44] = (byte) 143;
      numArray3[15] = (byte) 172;
      numArray3[16 /*0x10*/] = (byte) 157;
      numArray3[5] = (byte) 137;
      numArray3[18] = (byte) 79;
      numArray3[9] = (byte) 73;
      numArray3[28] = (byte) 169;
      numArray3[27] = (byte) 20;
      numArray3[32 /*0x20*/] = (byte) 97;
      numArray3[50] = (byte) 198;
      numArray3[24] = (byte) 0;
      numArray3[42] = (byte) 238;
      numArray3[17] = (byte) 184;
      numArray3[12] = (byte) 253;
      numArray3[0] = (byte) 51;
      numArray3[29] = (byte) 30;
      numArray3[30] = (byte) 29;
      numArray3[43] = (byte) 191;
      numArray3[51] = (byte) 189;
      numArray3[33] = (byte) 242;
      numArray3[35] = (byte) 137;
      numArray3[37] = (byte) 204;
      numArray3[36] = (byte) 196;
      numArray3[53] = (byte) 232;
      numArray3[38] = (byte) 120;
      numArray3[39] = (byte) 135;
      numArray3[54] = (byte) 175;
      numArray3[41] = (byte) 196;
      numArray3[21] = (byte) 82;
      numArray3[25] = (byte) 206;
      numArray3[1] = (byte) 161;
      numArray3[45] = (byte) 253;
      numArray3[46] = (byte) 36;
      numArray3[47] = (byte) 124;
      numArray3[48 /*0x30*/] = (byte) 112 /*0x70*/;
      numArray3[19] = (byte) 101;
      numArray3[34] = (byte) 147;
      numArray3[31 /*0x1F*/] = (byte) 36;
      numArray3[52] = (byte) 55;
      numArray3[20] = (byte) 5;
      numArray3[10] = (byte) 250;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 112 /*0x70*/,
        (byte) 37,
        (byte) 103,
        (byte) 116,
        (byte) 66,
        (byte) 100,
        (byte) 58,
        (byte) 96 /*0x60*/,
        (byte) 246,
        (byte) 70,
        (byte) 130,
        (byte) 16 /*0x10*/,
        (byte) 115,
        (byte) 177,
        (byte) 198,
        (byte) 101,
        (byte) 45,
        (byte) 90,
        (byte) 200,
        (byte) 241,
        (byte) 82,
        (byte) 13,
        (byte) 140,
        (byte) 125,
        (byte) 157,
        (byte) 224 /*0xE0*/,
        (byte) 125,
        (byte) 122,
        (byte) 206,
        (byte) 122,
        (byte) 86,
        (byte) 218,
        (byte) 47,
        (byte) 138,
        (byte) 15,
        (byte) 34,
        (byte) 89,
        (byte) 4,
        (byte) 170,
        (byte) 89,
        (byte) 166,
        (byte) 242,
        (byte) 89,
        (byte) 9,
        (byte) 39,
        (byte) 7,
        (byte) 239,
        (byte) 111,
        (byte) 109,
        (byte) 71,
        (byte) 101,
        (byte) 149,
        (byte) 242,
        (byte) 204,
        (byte) 126
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 121,
        (byte) 25,
        (byte) 159,
        (byte) 112 /*0x70*/,
        (byte) 143,
        (byte) 24,
        (byte) 232,
        (byte) 192 /*0xC0*/,
        (byte) 251,
        (byte) 207,
        (byte) 208 /*0xD0*/,
        (byte) 4,
        (byte) 116,
        (byte) 158,
        (byte) 200,
        (byte) 73,
        (byte) 184,
        (byte) 96 /*0x60*/,
        (byte) 107,
        (byte) 132,
        (byte) 133,
        (byte) 161,
        (byte) 246,
        (byte) 80 /*0x50*/,
        (byte) 149,
        (byte) 56,
        (byte) 212,
        (byte) 250,
        (byte) 252,
        (byte) 236,
        (byte) 12,
        (byte) 106,
        (byte) 236,
        (byte) 181,
        (byte) 229,
        (byte) 18,
        (byte) 233,
        (byte) 145,
        (byte) 65,
        (byte) 231,
        (byte) 73,
        (byte) 141,
        (byte) 247,
        (byte) 90,
        byte.MaxValue,
        (byte) 44,
        (byte) 79,
        (byte) 44,
        (byte) 53,
        (byte) 21,
        (byte) 202,
        (byte) 248,
        (byte) 67,
        (byte) 40,
        (byte) 90
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[52]
      {
        (byte) 234,
        (byte) 215,
        (byte) 97,
        (byte) 230,
        (byte) 191,
        (byte) 146,
        (byte) 124,
        (byte) 239,
        (byte) 168,
        (byte) 224 /*0xE0*/,
        (byte) 233,
        (byte) 162,
        (byte) 164,
        (byte) 12,
        (byte) 41,
        (byte) 175,
        (byte) 110,
        (byte) 74,
        (byte) 96 /*0x60*/,
        (byte) 180,
        (byte) 131,
        (byte) 85,
        (byte) 149,
        (byte) 240 /*0xF0*/,
        (byte) 14,
        (byte) 12,
        (byte) 134,
        (byte) 20,
        (byte) 193,
        (byte) 231,
        (byte) 98,
        (byte) 11,
        (byte) 173,
        (byte) 137,
        (byte) 111,
        (byte) 44,
        (byte) 72,
        (byte) 115,
        (byte) 230,
        (byte) 96 /*0x60*/,
        (byte) 220,
        (byte) 25,
        (byte) 127 /*0x7F*/,
        (byte) 39,
        (byte) 6,
        (byte) 205,
        (byte) 201,
        (byte) 59,
        (byte) 59,
        (byte) 229,
        (byte) 132,
        (byte) 129
      };
      byte[] numArray7 = new byte[52];
      numArray7[31 /*0x1F*/] = (byte) 148;
      numArray7[1] = (byte) 219;
      numArray7[15] = (byte) 57;
      numArray7[29] = (byte) 238;
      numArray7[12] = (byte) 238;
      numArray7[6] = (byte) 218;
      numArray7[7] = (byte) 156;
      numArray7[19] = (byte) 177;
      numArray7[8] = (byte) 124;
      numArray7[35] = (byte) 69;
      numArray7[10] = (byte) 128 /*0x80*/;
      numArray7[4] = (byte) 9;
      numArray7[3] = (byte) 53;
      numArray7[37] = (byte) 115;
      numArray7[9] = (byte) 73;
      numArray7[13] = (byte) 226;
      numArray7[16 /*0x10*/] = (byte) 106;
      numArray7[17] = (byte) 188;
      numArray7[18] = (byte) 22;
      numArray7[2] = (byte) 177;
      numArray7[20] = (byte) 162;
      numArray7[21] = (byte) 62;
      numArray7[51] = (byte) 157;
      numArray7[30] = (byte) 238;
      numArray7[24] = (byte) 113;
      numArray7[34] = (byte) 87;
      numArray7[26] = (byte) 3;
      numArray7[27] = (byte) 31 /*0x1F*/;
      numArray7[28] = (byte) 113;
      numArray7[33] = (byte) 145;
      numArray7[25] = (byte) 17;
      numArray7[48 /*0x30*/] = (byte) 186;
      numArray7[41] = (byte) 27;
      numArray7[11] = (byte) 243;
      numArray7[46] = (byte) 157;
      numArray7[45] = (byte) 21;
      numArray7[36] = (byte) 5;
      numArray7[38] = (byte) 137;
      numArray7[23] = (byte) 37;
      numArray7[39] = (byte) 229;
      numArray7[40] = (byte) 21;
      numArray7[50] = (byte) 96 /*0x60*/;
      numArray7[42] = (byte) 134;
      numArray7[32 /*0x20*/] = (byte) 227;
      numArray7[44] = (byte) 163;
      numArray7[0] = (byte) 136;
      numArray7[49] = (byte) 132;
      numArray7[47] = (byte) 180;
      numArray7[22] = (byte) 232;
      numArray7[5] = (byte) 165;
      numArray7[14] = (byte) 38;
      numArray7[43] = (byte) 226;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[29];
      byte[] response = new byte[29];
      Array.Copy((Array) sc_12586.sspq, 639, (Array) numArray8, 0, 29);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12586.sspr, 639, (Array) numArray8, 0, 29);
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
    byte[] numArray9 = new byte[162];
    byte[] numArray10 = new byte[55];
    numArray10[32 /*0x20*/] = (byte) 218;
    numArray10[1] = (byte) 91;
    numArray10[2] = (byte) 218;
    numArray10[44] = (byte) 90;
    numArray10[4] = (byte) 206;
    numArray10[46] = (byte) 21;
    numArray10[51] = (byte) 135;
    numArray10[7] = (byte) 28;
    numArray10[8] = (byte) 22;
    numArray10[9] = (byte) 79;
    numArray10[38] = (byte) 33;
    numArray10[5] = (byte) 40;
    numArray10[14] = (byte) 151;
    numArray10[13] = (byte) 230;
    numArray10[24] = (byte) 235;
    numArray10[15] = (byte) 98;
    numArray10[16 /*0x10*/] = (byte) 13;
    numArray10[17] = (byte) 56;
    numArray10[18] = (byte) 226;
    numArray10[28] = (byte) 166;
    numArray10[20] = (byte) 220;
    numArray10[21] = (byte) 45;
    numArray10[25] = (byte) 115;
    numArray10[23] = (byte) 41;
    numArray10[22] = (byte) 239;
    numArray10[29] = (byte) 15;
    numArray10[26] = (byte) 115;
    numArray10[47] = (byte) 124;
    numArray10[11] = (byte) 195;
    numArray10[30] = (byte) 198;
    numArray10[54] = (byte) 209;
    numArray10[31 /*0x1F*/] = (byte) 46;
    numArray10[10] = (byte) 4;
    numArray10[33] = (byte) 18;
    numArray10[34] = (byte) 244;
    numArray10[35] = (byte) 247;
    numArray10[36] = (byte) 129;
    numArray10[41] = (byte) 164;
    numArray10[27] = (byte) 45;
    numArray10[39] = (byte) 205;
    numArray10[40] = (byte) 222;
    numArray10[49] = (byte) 180;
    numArray10[42] = (byte) 250;
    numArray10[43] = (byte) 63 /*0x3F*/;
    numArray10[37] = (byte) 216;
    numArray10[45] = (byte) 109;
    numArray10[52] = (byte) 134;
    numArray10[48 /*0x30*/] = (byte) 230;
    numArray10[0] = (byte) 154;
    numArray10[12] = (byte) 149;
    numArray10[50] = (byte) 183;
    numArray10[53] = (byte) 139;
    numArray10[6] = (byte) 199;
    numArray10[3] = (byte) 196;
    numArray10[19] = (byte) 8;
    byte[] numArray11 = new byte[55]
    {
      (byte) 142,
      (byte) 69,
      (byte) 96 /*0x60*/,
      (byte) 40,
      (byte) 48 /*0x30*/,
      (byte) 151,
      (byte) 197,
      (byte) 79,
      (byte) 243,
      (byte) 188,
      (byte) 177,
      (byte) 170,
      (byte) 38,
      (byte) 53,
      (byte) 44,
      (byte) 17,
      (byte) 231,
      (byte) 13,
      (byte) 132,
      (byte) 117,
      (byte) 170,
      (byte) 125,
      (byte) 50,
      (byte) 207,
      (byte) 7,
      (byte) 9,
      (byte) 38,
      (byte) 46,
      (byte) 105,
      (byte) 40,
      (byte) 62,
      (byte) 81,
      (byte) 31 /*0x1F*/,
      (byte) 60,
      (byte) 185,
      (byte) 225,
      (byte) 219,
      (byte) 111,
      (byte) 252,
      (byte) 27,
      (byte) 203,
      (byte) 228,
      (byte) 8,
      (byte) 33,
      (byte) 57,
      (byte) 219,
      (byte) 199,
      (byte) 142,
      (byte) 209,
      (byte) 106,
      (byte) 19,
      (byte) 130,
      (byte) 103,
      (byte) 78,
      (byte) 44
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[16 /*0x10*/] = (byte) 253;
    numArray12[1] = (byte) 90;
    numArray12[12] = (byte) 20;
    numArray12[4] = (byte) 82;
    numArray12[35] = (byte) 164;
    numArray12[15] = (byte) 80 /*0x50*/;
    numArray12[6] = (byte) 174;
    numArray12[7] = (byte) 73;
    numArray12[40] = (byte) 69;
    numArray12[47] = (byte) 195;
    numArray12[5] = (byte) 131;
    numArray12[11] = (byte) 105;
    numArray12[21] = (byte) 75;
    numArray12[13] = (byte) 15;
    numArray12[14] = (byte) 53;
    numArray12[50] = (byte) 115;
    numArray12[45] = (byte) 33;
    numArray12[17] = (byte) 230;
    numArray12[25] = (byte) 0;
    numArray12[0] = (byte) 225;
    numArray12[20] = (byte) 250;
    numArray12[30] = (byte) 237;
    numArray12[38] = (byte) 241;
    numArray12[23] = (byte) 203;
    numArray12[39] = (byte) 171;
    numArray12[22] = (byte) 249;
    numArray12[49] = (byte) 32 /*0x20*/;
    numArray12[27] = (byte) 214;
    numArray12[3] = (byte) 191;
    numArray12[29] = (byte) 54;
    numArray12[33] = (byte) 208 /*0xD0*/;
    numArray12[31 /*0x1F*/] = (byte) 167;
    numArray12[26] = (byte) 31 /*0x1F*/;
    numArray12[52] = (byte) 144 /*0x90*/;
    numArray12[34] = (byte) 239;
    numArray12[10] = (byte) 66;
    numArray12[37] = (byte) 88;
    numArray12[2] = (byte) 253;
    numArray12[43] = (byte) 234;
    numArray12[8] = (byte) 47;
    numArray12[42] = (byte) 84;
    numArray12[41] = (byte) 108;
    numArray12[32 /*0x20*/] = (byte) 77;
    numArray12[44] = (byte) 80 /*0x50*/;
    numArray12[36] = (byte) 200;
    numArray12[18] = (byte) 8;
    numArray12[46] = (byte) 50;
    numArray12[51] = (byte) 122;
    numArray12[48 /*0x30*/] = (byte) 76;
    numArray12[9] = (byte) 154;
    numArray12[28] = (byte) 206;
    numArray12[24] = (byte) 106;
    numArray12[19] = (byte) 62;
    numArray12[53] = (byte) 251;
    numArray12[54] = (byte) 193;
    byte[] numArray13 = new byte[55];
    numArray13[39] = (byte) 99;
    numArray13[45] = (byte) 60;
    numArray13[10] = (byte) 78;
    numArray13[13] = (byte) 18;
    numArray13[4] = (byte) 248;
    numArray13[28] = (byte) 128 /*0x80*/;
    numArray13[51] = (byte) 113;
    numArray13[7] = (byte) 169;
    numArray13[54] = (byte) 110;
    numArray13[18] = (byte) 133;
    numArray13[8] = (byte) 238;
    numArray13[11] = (byte) 181;
    numArray13[12] = (byte) 116;
    numArray13[22] = (byte) 123;
    numArray13[31 /*0x1F*/] = (byte) 243;
    numArray13[15] = (byte) 93;
    numArray13[38] = (byte) 45;
    numArray13[17] = (byte) 22;
    numArray13[3] = (byte) 240 /*0xF0*/;
    numArray13[34] = (byte) 143;
    numArray13[20] = (byte) 209;
    numArray13[30] = (byte) 5;
    numArray13[35] = (byte) 1;
    numArray13[23] = (byte) 144 /*0x90*/;
    numArray13[52] = (byte) 219;
    numArray13[19] = (byte) 114;
    numArray13[9] = (byte) 228;
    numArray13[24] = (byte) 123;
    numArray13[2] = (byte) 167;
    numArray13[32 /*0x20*/] = (byte) 3;
    numArray13[37] = (byte) 90;
    numArray13[6] = (byte) 67;
    numArray13[14] = (byte) 166;
    numArray13[33] = (byte) 115;
    numArray13[46] = (byte) 39;
    numArray13[29] = (byte) 218;
    numArray13[5] = (byte) 16 /*0x10*/;
    numArray13[48 /*0x30*/] = (byte) 217;
    numArray13[0] = (byte) 143;
    numArray13[21] = (byte) 203;
    numArray13[40] = (byte) 29;
    numArray13[41] = (byte) 111;
    numArray13[42] = (byte) 96 /*0x60*/;
    numArray13[43] = (byte) 63 /*0x3F*/;
    numArray13[27] = (byte) 53;
    numArray13[1] = (byte) 163;
    numArray13[44] = (byte) 177;
    numArray13[16 /*0x10*/] = (byte) 220;
    numArray13[25] = (byte) 249;
    numArray13[49] = (byte) 180;
    numArray13[50] = (byte) 25;
    numArray13[36] = (byte) 228;
    numArray13[47] = (byte) 10;
    numArray13[53] = (byte) 16 /*0x10*/;
    numArray13[26] = (byte) 126;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[52];
    numArray14[13] = (byte) 18;
    numArray14[1] = (byte) 198;
    numArray14[2] = (byte) 164;
    numArray14[20] = (byte) 202;
    numArray14[4] = (byte) 128 /*0x80*/;
    numArray14[48 /*0x30*/] = (byte) 134;
    numArray14[14] = (byte) 167;
    numArray14[5] = (byte) 184;
    numArray14[8] = (byte) 161;
    numArray14[7] = (byte) 76;
    numArray14[10] = (byte) 89;
    numArray14[11] = (byte) 11;
    numArray14[34] = (byte) 236;
    numArray14[31 /*0x1F*/] = (byte) 68;
    numArray14[12] = (byte) 94;
    numArray14[27] = (byte) 197;
    numArray14[16 /*0x10*/] = (byte) 212;
    numArray14[17] = (byte) 110;
    numArray14[45] = (byte) 209;
    numArray14[19] = (byte) 117;
    numArray14[24] = (byte) 221;
    numArray14[21] = (byte) 148;
    numArray14[22] = (byte) 9;
    numArray14[23] = (byte) 37;
    numArray14[37] = (byte) 242;
    numArray14[25] = (byte) 97;
    numArray14[26] = (byte) 109;
    numArray14[18] = (byte) 122;
    numArray14[28] = (byte) 187;
    numArray14[29] = (byte) 195;
    numArray14[0] = (byte) 242;
    numArray14[51] = (byte) 6;
    numArray14[3] = (byte) 78;
    numArray14[33] = (byte) 109;
    numArray14[41] = (byte) 185;
    numArray14[35] = (byte) 50;
    numArray14[36] = (byte) 119;
    numArray14[30] = (byte) 218;
    numArray14[38] = (byte) 157;
    numArray14[39] = (byte) 105;
    numArray14[6] = (byte) 65;
    numArray14[44] = (byte) 97;
    numArray14[42] = (byte) 143;
    numArray14[9] = (byte) 122;
    numArray14[32 /*0x20*/] = (byte) 184;
    numArray14[40] = (byte) 228;
    numArray14[15] = (byte) 57;
    numArray14[47] = (byte) 149;
    numArray14[46] = (byte) 14;
    numArray14[49] = (byte) 248;
    numArray14[50] = (byte) 74;
    numArray14[43] = (byte) 191;
    byte[] numArray15 = new byte[52]
    {
      (byte) 249,
      (byte) 153,
      (byte) 0,
      (byte) 95,
      (byte) 82,
      (byte) 130,
      (byte) 83,
      (byte) 195,
      (byte) 210,
      (byte) 134,
      (byte) 53,
      (byte) 254,
      (byte) 81,
      (byte) 7,
      (byte) 160 /*0xA0*/,
      (byte) 84,
      (byte) 234,
      (byte) 128 /*0x80*/,
      (byte) 225,
      (byte) 105,
      (byte) 84,
      (byte) 208 /*0xD0*/,
      (byte) 200,
      (byte) 202,
      (byte) 201,
      (byte) 179,
      (byte) 139,
      (byte) 232,
      (byte) 140,
      (byte) 100,
      (byte) 215,
      (byte) 25,
      (byte) 127 /*0x7F*/,
      (byte) 19,
      (byte) 39,
      (byte) 29,
      (byte) 230,
      (byte) 140,
      (byte) 34,
      (byte) 174,
      (byte) 80 /*0x50*/,
      (byte) 77,
      (byte) 11,
      (byte) 219,
      (byte) 99,
      (byte) 5,
      (byte) 203,
      (byte) 148,
      (byte) 101,
      (byte) 228,
      (byte) 148,
      (byte) 59
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 52);
    for (int index = 0; index < 52; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static int ssp_appserver_12645(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 140;
    sourceArray1[1] = (byte) 88;
    sourceArray1[6] = (byte) 114;
    sourceArray1[3] = (byte) 187;
    sourceArray1[9] = (byte) 101;
    sourceArray1[5] = (byte) 204;
    sourceArray1[41] = (byte) 43;
    sourceArray1[15] = byte.MaxValue;
    sourceArray1[40] = (byte) 172;
    sourceArray1[33] = (byte) 48 /*0x30*/;
    sourceArray1[7] = (byte) 61;
    sourceArray1[11] = (byte) 236;
    sourceArray1[26] = (byte) 53;
    sourceArray1[23] = (byte) 41;
    sourceArray1[44] = (byte) 84;
    sourceArray1[32 /*0x20*/] = (byte) 27;
    sourceArray1[16 /*0x10*/] = (byte) 202;
    sourceArray1[0] = (byte) 165;
    sourceArray1[18] = (byte) 83;
    sourceArray1[19] = (byte) 241;
    sourceArray1[8] = (byte) 206;
    sourceArray1[21] = (byte) 233;
    sourceArray1[22] = (byte) 83;
    sourceArray1[13] = (byte) 59;
    sourceArray1[24] = (byte) 119;
    sourceArray1[30] = (byte) 16 /*0x10*/;
    sourceArray1[39] = (byte) 177;
    sourceArray1[27] = (byte) 104;
    sourceArray1[28] = (byte) 44;
    sourceArray1[20] = (byte) 41;
    sourceArray1[2] = (byte) 115;
    sourceArray1[43] = (byte) 76;
    sourceArray1[25] = (byte) 141;
    sourceArray1[31 /*0x1F*/] = (byte) 159;
    sourceArray1[34] = (byte) 15;
    sourceArray1[35] = (byte) 54;
    sourceArray1[36] = (byte) 1;
    sourceArray1[37] = (byte) 59;
    sourceArray1[38] = (byte) 27;
    sourceArray1[45] = (byte) 95;
    sourceArray1[10] = (byte) 203;
    sourceArray1[14] = (byte) 150;
    sourceArray1[17] = (byte) 96 /*0x60*/;
    sourceArray1[47] = (byte) 225;
    sourceArray1[12] = (byte) 38;
    sourceArray1[42] = (byte) 89;
    sourceArray1[46] = (byte) 88;
    sourceArray1[4] = (byte) 101;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 229,
      (byte) 132,
      (byte) 12,
      (byte) 174,
      (byte) 251,
      (byte) 234,
      (byte) 43,
      (byte) 76,
      (byte) 128 /*0x80*/,
      (byte) 26,
      (byte) 221,
      (byte) 161,
      (byte) 130,
      (byte) 87,
      (byte) 116,
      (byte) 205,
      (byte) 148,
      (byte) 244,
      (byte) 188,
      (byte) 31 /*0x1F*/,
      (byte) 164,
      (byte) 20,
      (byte) 253,
      (byte) 143,
      (byte) 77,
      (byte) 220,
      (byte) 39,
      (byte) 56,
      (byte) 88,
      (byte) 169,
      (byte) 218,
      (byte) 164,
      (byte) 225,
      (byte) 139,
      (byte) 164,
      (byte) 44,
      (byte) 250,
      (byte) 161,
      (byte) 163,
      (byte) 249,
      (byte) 47,
      (byte) 163,
      (byte) 10,
      (byte) 241,
      (byte) 139,
      (byte) 8,
      (byte) 17,
      (byte) 122
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12646()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55]
      {
        (byte) 142,
        (byte) 98,
        (byte) 181,
        (byte) 122,
        (byte) 21,
        (byte) 220,
        (byte) 194,
        (byte) 64 /*0x40*/,
        (byte) 213,
        (byte) 212,
        (byte) 43,
        (byte) 253,
        (byte) 117,
        (byte) 45,
        (byte) 163,
        (byte) 67,
        (byte) 121,
        (byte) 247,
        (byte) 32 /*0x20*/,
        (byte) 75,
        (byte) 111,
        (byte) 208 /*0xD0*/,
        (byte) 152,
        (byte) 215,
        (byte) 46,
        (byte) 70,
        (byte) 151,
        (byte) 1,
        (byte) 135,
        (byte) 161,
        (byte) 227,
        (byte) 119,
        (byte) 16 /*0x10*/,
        (byte) 172,
        (byte) 136,
        (byte) 210,
        (byte) 158,
        (byte) 21,
        (byte) 4,
        (byte) 68,
        (byte) 70,
        (byte) 124,
        (byte) 42,
        (byte) 138,
        (byte) 40,
        (byte) 51,
        (byte) 66,
        (byte) 202,
        (byte) 160 /*0xA0*/,
        (byte) 214,
        (byte) 41,
        (byte) 35,
        (byte) 247,
        (byte) 171,
        (byte) 184
      };
      byte[] numArray3 = new byte[55];
      numArray3[54] = (byte) 118;
      numArray3[34] = (byte) 91;
      numArray3[2] = (byte) 162;
      numArray3[3] = (byte) 37;
      numArray3[38] = (byte) 185;
      numArray3[5] = (byte) 152;
      numArray3[45] = (byte) 97;
      numArray3[4] = (byte) 227;
      numArray3[32 /*0x20*/] = (byte) 72;
      numArray3[0] = (byte) 91;
      numArray3[53] = (byte) 210;
      numArray3[49] = (byte) 106;
      numArray3[12] = (byte) 15;
      numArray3[37] = (byte) 148;
      numArray3[15] = (byte) 34;
      numArray3[48 /*0x30*/] = (byte) 43;
      numArray3[16 /*0x10*/] = (byte) 91;
      numArray3[17] = (byte) 22;
      numArray3[18] = (byte) 136;
      numArray3[19] = (byte) 21;
      numArray3[24] = (byte) 241;
      numArray3[50] = (byte) 19;
      numArray3[11] = (byte) 239;
      numArray3[29] = (byte) 113;
      numArray3[13] = (byte) 18;
      numArray3[52] = (byte) 108;
      numArray3[26] = (byte) 57;
      numArray3[14] = (byte) 111;
      numArray3[31 /*0x1F*/] = (byte) 60;
      numArray3[7] = (byte) 18;
      numArray3[42] = (byte) 118;
      numArray3[21] = (byte) 164;
      numArray3[9] = (byte) 77;
      numArray3[8] = (byte) 18;
      numArray3[6] = (byte) 71;
      numArray3[35] = (byte) 226;
      numArray3[43] = (byte) 168;
      numArray3[30] = (byte) 59;
      numArray3[1] = (byte) 244;
      numArray3[39] = (byte) 242;
      numArray3[40] = (byte) 222;
      numArray3[41] = (byte) 107;
      numArray3[36] = (byte) 165;
      numArray3[28] = (byte) 243;
      numArray3[44] = (byte) 85;
      numArray3[10] = (byte) 116;
      numArray3[46] = (byte) 173;
      numArray3[47] = (byte) 216;
      numArray3[33] = (byte) 58;
      numArray3[27] = (byte) 250;
      numArray3[23] = (byte) 170;
      numArray3[22] = (byte) 116;
      numArray3[20] = (byte) 142;
      numArray3[51] = (byte) 243;
      numArray3[25] = (byte) 220;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18];
      numArray4[2] = (byte) 203;
      numArray4[1] = (byte) 80 /*0x50*/;
      numArray4[16 /*0x10*/] = (byte) 221;
      numArray4[3] = (byte) 119;
      numArray4[4] = (byte) 97;
      numArray4[10] = (byte) 212;
      numArray4[12] = (byte) 199;
      numArray4[14] = (byte) 155;
      numArray4[8] = (byte) 51;
      numArray4[9] = (byte) 136;
      numArray4[5] = (byte) 7;
      numArray4[11] = (byte) 218;
      numArray4[0] = (byte) 46;
      numArray4[6] = (byte) 188;
      numArray4[15] = (byte) 189;
      numArray4[7] = (byte) 48 /*0x30*/;
      numArray4[13] = (byte) 228;
      numArray4[17] = (byte) 128 /*0x80*/;
      byte[] numArray5 = new byte[18]
      {
        (byte) 101,
        (byte) 177,
        (byte) 150,
        (byte) 12,
        (byte) 158,
        (byte) 124,
        (byte) 111,
        (byte) 66,
        (byte) 128 /*0x80*/,
        (byte) 206,
        (byte) 20,
        (byte) 19,
        (byte) 28,
        (byte) 135,
        (byte) 165,
        (byte) 79,
        (byte) 44,
        (byte) 5
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55];
    numArray7[48 /*0x30*/] = (byte) 72;
    numArray7[1] = (byte) 57;
    numArray7[2] = (byte) 105;
    numArray7[0] = (byte) 189;
    numArray7[4] = (byte) 189;
    numArray7[27] = (byte) 40;
    numArray7[36] = (byte) 11;
    numArray7[7] = (byte) 9;
    numArray7[43] = (byte) 137;
    numArray7[24] = (byte) 207;
    numArray7[10] = (byte) 114;
    numArray7[11] = (byte) 62;
    numArray7[26] = (byte) 166;
    numArray7[5] = (byte) 165;
    numArray7[53] = (byte) 184;
    numArray7[15] = (byte) 71;
    numArray7[16 /*0x10*/] = (byte) 244;
    numArray7[17] = (byte) 156;
    numArray7[45] = (byte) 38;
    numArray7[33] = (byte) 92;
    numArray7[20] = (byte) 209;
    numArray7[25] = (byte) 202;
    numArray7[42] = (byte) 51;
    numArray7[23] = (byte) 161;
    numArray7[22] = (byte) 9;
    numArray7[47] = (byte) 150;
    numArray7[54] = (byte) 81;
    numArray7[34] = (byte) 113;
    numArray7[28] = (byte) 122;
    numArray7[6] = (byte) 74;
    numArray7[3] = (byte) 64 /*0x40*/;
    numArray7[31 /*0x1F*/] = (byte) 84;
    numArray7[32 /*0x20*/] = (byte) 85;
    numArray7[29] = (byte) 83;
    numArray7[30] = (byte) 34;
    numArray7[35] = (byte) 118;
    numArray7[52] = (byte) 40;
    numArray7[49] = (byte) 5;
    numArray7[38] = (byte) 246;
    numArray7[13] = (byte) 151;
    numArray7[40] = (byte) 93;
    numArray7[41] = (byte) 134;
    numArray7[39] = (byte) 228;
    numArray7[37] = (byte) 250;
    numArray7[44] = (byte) 28;
    numArray7[8] = (byte) 76;
    numArray7[9] = (byte) 41;
    numArray7[46] = (byte) 152;
    numArray7[51] = (byte) 220;
    numArray7[14] = (byte) 104;
    numArray7[50] = (byte) 156;
    numArray7[18] = (byte) 65;
    numArray7[12] = (byte) 37;
    numArray7[21] = (byte) 135;
    numArray7[19] = (byte) 207;
    byte[] numArray8 = new byte[55]
    {
      (byte) 164,
      byte.MaxValue,
      (byte) 218,
      (byte) 162,
      (byte) 139,
      (byte) 187,
      (byte) 251,
      (byte) 92,
      (byte) 245,
      (byte) 214,
      (byte) 110,
      (byte) 239,
      (byte) 3,
      (byte) 103,
      (byte) 87,
      (byte) 38,
      (byte) 71,
      (byte) 117,
      (byte) 145,
      (byte) 205,
      (byte) 32 /*0x20*/,
      (byte) 75,
      (byte) 213,
      (byte) 32 /*0x20*/,
      (byte) 247,
      (byte) 90,
      (byte) 133,
      (byte) 134,
      (byte) 52,
      (byte) 88,
      (byte) 189,
      (byte) 159,
      (byte) 218,
      (byte) 46,
      (byte) 186,
      (byte) 28,
      (byte) 27,
      (byte) 6,
      (byte) 178,
      (byte) 77,
      (byte) 82,
      (byte) 127 /*0x7F*/,
      (byte) 135,
      (byte) 133,
      (byte) 221,
      (byte) 190,
      (byte) 175,
      (byte) 25,
      (byte) 152,
      (byte) 21,
      (byte) 103,
      (byte) 133,
      (byte) 2,
      (byte) 102,
      (byte) 225
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18];
    numArray9[12] = (byte) 172;
    numArray9[1] = (byte) 199;
    numArray9[13] = (byte) 248;
    numArray9[3] = (byte) 237;
    numArray9[15] = (byte) 162;
    numArray9[10] = (byte) 190;
    numArray9[6] = (byte) 66;
    numArray9[16 /*0x10*/] = (byte) 118;
    numArray9[8] = (byte) 242;
    numArray9[2] = (byte) 193;
    numArray9[4] = (byte) 66;
    numArray9[11] = (byte) 233;
    numArray9[14] = (byte) 112 /*0x70*/;
    numArray9[9] = (byte) 75;
    numArray9[7] = (byte) 193;
    numArray9[0] = (byte) 67;
    numArray9[5] = (byte) 25;
    numArray9[17] = (byte) 55;
    byte[] numArray10 = new byte[18]
    {
      (byte) 47,
      (byte) 88,
      (byte) 153,
      (byte) 209,
      (byte) 102,
      (byte) 220,
      (byte) 145,
      (byte) 138,
      (byte) 106,
      (byte) 247,
      (byte) 124,
      (byte) 62,
      (byte) 42,
      (byte) 52,
      (byte) 182,
      (byte) 239,
      (byte) 131,
      (byte) 223
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12647(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[11] = (byte) 178;
    sourceArray1[13] = (byte) 178;
    sourceArray1[37] = (byte) 82;
    sourceArray1[3] = (byte) 16 /*0x10*/;
    sourceArray1[41] = (byte) 224 /*0xE0*/;
    sourceArray1[0] = (byte) 192 /*0xC0*/;
    sourceArray1[19] = (byte) 109;
    sourceArray1[7] = (byte) 50;
    sourceArray1[29] = (byte) 96 /*0x60*/;
    sourceArray1[9] = (byte) 85;
    sourceArray1[34] = (byte) 61;
    sourceArray1[16 /*0x10*/] = (byte) 94;
    sourceArray1[43] = (byte) 11;
    sourceArray1[27] = (byte) 248;
    sourceArray1[46] = (byte) 145;
    sourceArray1[15] = (byte) 92;
    sourceArray1[4] = (byte) 135;
    sourceArray1[17] = (byte) 79;
    sourceArray1[14] = (byte) 188;
    sourceArray1[1] = (byte) 11;
    sourceArray1[6] = (byte) 208 /*0xD0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 62;
    sourceArray1[22] = (byte) 145;
    sourceArray1[36] = (byte) 120;
    sourceArray1[12] = (byte) 200;
    sourceArray1[25] = (byte) 143;
    sourceArray1[26] = (byte) 22;
    sourceArray1[28] = (byte) 174;
    sourceArray1[23] = (byte) 243;
    sourceArray1[44] = (byte) 72;
    sourceArray1[35] = (byte) 123;
    sourceArray1[30] = (byte) 125;
    sourceArray1[32 /*0x20*/] = (byte) 117;
    sourceArray1[33] = (byte) 33;
    sourceArray1[20] = (byte) 46;
    sourceArray1[2] = (byte) 242;
    sourceArray1[8] = (byte) 148;
    sourceArray1[10] = (byte) 14;
    sourceArray1[38] = (byte) 23;
    sourceArray1[39] = (byte) 223;
    sourceArray1[40] = (byte) 220;
    sourceArray1[18] = (byte) 184;
    sourceArray1[42] = (byte) 122;
    sourceArray1[24] = (byte) 185;
    sourceArray1[21] = (byte) 178;
    sourceArray1[45] = (byte) 81;
    sourceArray1[5] = (byte) 169;
    sourceArray1[47] = (byte) 228;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 170,
      (byte) 119,
      (byte) 48 /*0x30*/,
      (byte) 125,
      (byte) 144 /*0x90*/,
      (byte) 53,
      (byte) 18,
      (byte) 152,
      (byte) 132,
      (byte) 110,
      (byte) 202,
      (byte) 246,
      (byte) 39,
      (byte) 36,
      (byte) 2,
      (byte) 34,
      (byte) 246,
      (byte) 189,
      (byte) 112 /*0x70*/,
      (byte) 46,
      (byte) 204,
      (byte) 112 /*0x70*/,
      (byte) 66,
      (byte) 116,
      (byte) 84,
      (byte) 128 /*0x80*/,
      (byte) 87,
      (byte) 242,
      (byte) 38,
      (byte) 208 /*0xD0*/,
      (byte) 207,
      (byte) 76,
      (byte) 66,
      (byte) 199,
      (byte) 143,
      (byte) 196,
      (byte) 60,
      (byte) 230,
      (byte) 120,
      (byte) 64 /*0x40*/,
      (byte) 205,
      (byte) 101,
      (byte) 19,
      (byte) 115,
      (byte) 254,
      (byte) 99,
      (byte) 133,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12648()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 20,
        (byte) 234,
        (byte) 111
      };
      byte[] numArray3 = new byte[3]
      {
        (byte) 254,
        (byte) 74,
        (byte) 151
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
      (byte) 0,
      (byte) 222
    };
    numArray5[1] = (byte) 102;
    numArray5[0] = (byte) 113;
    byte[] numArray6 = new byte[3]
    {
      (byte) 203,
      (byte) 82,
      (byte) 134
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12649()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55];
      numArray2[37] = (byte) 49;
      numArray2[21] = (byte) 178;
      numArray2[2] = (byte) 181;
      numArray2[22] = (byte) 200;
      numArray2[24] = (byte) 98;
      numArray2[5] = (byte) 55;
      numArray2[6] = (byte) 79;
      numArray2[7] = (byte) 67;
      numArray2[8] = (byte) 63 /*0x3F*/;
      numArray2[0] = (byte) 34;
      numArray2[46] = (byte) 217;
      numArray2[11] = (byte) 25;
      numArray2[43] = (byte) 23;
      numArray2[20] = (byte) 194;
      numArray2[35] = (byte) 33;
      numArray2[15] = (byte) 241;
      numArray2[16 /*0x10*/] = (byte) 81;
      numArray2[17] = (byte) 50;
      numArray2[18] = (byte) 84;
      numArray2[52] = (byte) 154;
      numArray2[41] = (byte) 43;
      numArray2[54] = (byte) 158;
      numArray2[9] = (byte) 105;
      numArray2[23] = (byte) 167;
      numArray2[45] = (byte) 88;
      numArray2[25] = (byte) 152;
      numArray2[44] = (byte) 196;
      numArray2[4] = (byte) 209;
      numArray2[26] = (byte) 38;
      numArray2[29] = (byte) 13;
      numArray2[53] = (byte) 230;
      numArray2[14] = (byte) 241;
      numArray2[47] = (byte) 81;
      numArray2[27] = (byte) 219;
      numArray2[34] = (byte) 226;
      numArray2[51] = (byte) 33;
      numArray2[1] = (byte) 58;
      numArray2[12] = (byte) 141;
      numArray2[36] = (byte) 145;
      numArray2[39] = (byte) 221;
      numArray2[33] = (byte) 235;
      numArray2[28] = (byte) 91;
      numArray2[42] = (byte) 194;
      numArray2[13] = (byte) 140;
      numArray2[32 /*0x20*/] = (byte) 36;
      numArray2[30] = (byte) 37;
      numArray2[50] = (byte) 162;
      numArray2[3] = (byte) 2;
      numArray2[48 /*0x30*/] = (byte) 137;
      numArray2[49] = (byte) 227;
      numArray2[10] = (byte) 17;
      numArray2[40] = (byte) 154;
      numArray2[31 /*0x1F*/] = (byte) 12;
      numArray2[19] = (byte) 19;
      numArray2[38] = (byte) 166;
      byte[] numArray3 = new byte[55]
      {
        (byte) 197,
        (byte) 177,
        (byte) 3,
        (byte) 35,
        (byte) 93,
        (byte) 227,
        (byte) 110,
        (byte) 129,
        (byte) 146,
        (byte) 214,
        (byte) 212,
        (byte) 221,
        (byte) 94,
        (byte) 135,
        (byte) 226,
        (byte) 228,
        (byte) 36,
        (byte) 8,
        (byte) 111,
        (byte) 21,
        (byte) 98,
        (byte) 83,
        (byte) 18,
        (byte) 128 /*0x80*/,
        (byte) 137,
        (byte) 170,
        (byte) 127 /*0x7F*/,
        (byte) 199,
        (byte) 115,
        (byte) 209,
        (byte) 126,
        (byte) 105,
        (byte) 176 /*0xB0*/,
        (byte) 70,
        (byte) 125,
        (byte) 233,
        (byte) 88,
        (byte) 245,
        (byte) 52,
        (byte) 116,
        (byte) 205,
        (byte) 178,
        (byte) 97,
        (byte) 120,
        (byte) 12,
        (byte) 132,
        (byte) 45,
        (byte) 56,
        (byte) 113,
        (byte) 161,
        (byte) 119,
        (byte) 129,
        (byte) 21,
        (byte) 147,
        (byte) 107
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18]
      {
        (byte) 44,
        (byte) 149,
        (byte) 122,
        (byte) 59,
        (byte) 34,
        (byte) 112 /*0x70*/,
        (byte) 199,
        (byte) 64 /*0x40*/,
        (byte) 146,
        (byte) 182,
        (byte) 85,
        (byte) 56,
        (byte) 33,
        (byte) 221,
        (byte) 142,
        (byte) 31 /*0x1F*/,
        (byte) 170,
        (byte) 83
      };
      byte[] numArray5 = new byte[18];
      numArray5[6] = (byte) 127 /*0x7F*/;
      numArray5[3] = (byte) 45;
      numArray5[8] = (byte) 166;
      numArray5[7] = (byte) 18;
      numArray5[0] = (byte) 247;
      numArray5[2] = (byte) 82;
      numArray5[4] = (byte) 195;
      numArray5[16 /*0x10*/] = (byte) 70;
      numArray5[5] = (byte) 223;
      numArray5[1] = (byte) 137;
      numArray5[10] = (byte) 87;
      numArray5[11] = (byte) 246;
      numArray5[12] = (byte) 136;
      numArray5[13] = (byte) 100;
      numArray5[9] = (byte) 67;
      numArray5[15] = (byte) 125;
      numArray5[14] = (byte) 180;
      numArray5[17] = (byte) 87;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55]
    {
      (byte) 210,
      (byte) 200,
      (byte) 146,
      (byte) 45,
      (byte) 173,
      (byte) 226,
      (byte) 111,
      (byte) 152,
      (byte) 166,
      (byte) 244,
      (byte) 118,
      (byte) 120,
      (byte) 25,
      (byte) 180,
      (byte) 32 /*0x20*/,
      (byte) 229,
      (byte) 10,
      (byte) 237,
      (byte) 242,
      (byte) 14,
      (byte) 138,
      (byte) 125,
      (byte) 223,
      (byte) 210,
      (byte) 67,
      (byte) 228,
      (byte) 15,
      (byte) 69,
      (byte) 52,
      (byte) 134,
      (byte) 145,
      (byte) 44,
      (byte) 25,
      (byte) 79,
      (byte) 128 /*0x80*/,
      (byte) 151,
      (byte) 74,
      (byte) 245,
      (byte) 250,
      (byte) 166,
      (byte) 189,
      byte.MaxValue,
      (byte) 76,
      (byte) 243,
      (byte) 247,
      (byte) 213,
      (byte) 184,
      (byte) 145,
      (byte) 209,
      (byte) 7,
      (byte) 76,
      (byte) 14,
      (byte) 0,
      (byte) 131,
      (byte) 121
    };
    byte[] numArray8 = new byte[55];
    numArray8[22] = (byte) 119;
    numArray8[32 /*0x20*/] = (byte) 31 /*0x1F*/;
    numArray8[2] = (byte) 87;
    numArray8[27] = (byte) 177;
    numArray8[34] = (byte) 143;
    numArray8[49] = (byte) 224 /*0xE0*/;
    numArray8[6] = (byte) 138;
    numArray8[3] = (byte) 120;
    numArray8[8] = (byte) 42;
    numArray8[33] = (byte) 201;
    numArray8[23] = (byte) 176 /*0xB0*/;
    numArray8[54] = (byte) 222;
    numArray8[9] = (byte) 98;
    numArray8[13] = (byte) 163;
    numArray8[14] = (byte) 152;
    numArray8[39] = (byte) 198;
    numArray8[16 /*0x10*/] = (byte) 104;
    numArray8[17] = (byte) 254;
    numArray8[18] = (byte) 79;
    numArray8[1] = (byte) 147;
    numArray8[20] = (byte) 43;
    numArray8[41] = (byte) 130;
    numArray8[0] = (byte) 145;
    numArray8[42] = (byte) 119;
    numArray8[44] = (byte) 183;
    numArray8[25] = (byte) 43;
    numArray8[26] = (byte) 89;
    numArray8[7] = (byte) 37;
    numArray8[28] = (byte) 136;
    numArray8[29] = (byte) 168;
    numArray8[30] = (byte) 7;
    numArray8[12] = (byte) 246;
    numArray8[21] = (byte) 240 /*0xF0*/;
    numArray8[40] = (byte) 104;
    numArray8[31 /*0x1F*/] = (byte) 57;
    numArray8[35] = (byte) 224 /*0xE0*/;
    numArray8[38] = (byte) 181;
    numArray8[19] = (byte) 24;
    numArray8[37] = (byte) 90;
    numArray8[24] = (byte) 19;
    numArray8[15] = (byte) 156;
    numArray8[36] = (byte) 234;
    numArray8[11] = (byte) 246;
    numArray8[43] = (byte) 38;
    numArray8[10] = (byte) 222;
    numArray8[45] = (byte) 205;
    numArray8[46] = (byte) 224 /*0xE0*/;
    numArray8[47] = (byte) 85;
    numArray8[48 /*0x30*/] = (byte) 137;
    numArray8[4] = (byte) 2;
    numArray8[50] = (byte) 137;
    numArray8[51] = (byte) 245;
    numArray8[52] = (byte) 159;
    numArray8[53] = (byte) 112 /*0x70*/;
    numArray8[5] = (byte) 50;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18]
    {
      (byte) 229,
      (byte) 123,
      (byte) 157,
      (byte) 83,
      (byte) 181,
      (byte) 55,
      (byte) 229,
      (byte) 223,
      (byte) 28,
      (byte) 201,
      (byte) 244,
      (byte) 7,
      (byte) 218,
      (byte) 16 /*0x10*/,
      (byte) 9,
      (byte) 194,
      (byte) 120,
      (byte) 188
    };
    byte[] numArray10 = new byte[18];
    numArray10[16 /*0x10*/] = (byte) 136;
    numArray10[7] = (byte) 34;
    numArray10[4] = (byte) 84;
    numArray10[3] = (byte) 49;
    numArray10[13] = (byte) 68;
    numArray10[5] = (byte) 209;
    numArray10[2] = (byte) 85;
    numArray10[11] = (byte) 144 /*0x90*/;
    numArray10[0] = (byte) 188;
    numArray10[9] = (byte) 0;
    numArray10[10] = (byte) 117;
    numArray10[6] = (byte) 123;
    numArray10[12] = (byte) 115;
    numArray10[15] = (byte) 185;
    numArray10[14] = (byte) 157;
    numArray10[1] = (byte) 203;
    numArray10[8] = (byte) 30;
    numArray10[17] = (byte) 64 /*0x40*/;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12650()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[77];
      byte[] numArray2 = new byte[55]
      {
        (byte) 71,
        (byte) 177,
        (byte) 145,
        (byte) 172,
        (byte) 250,
        (byte) 85,
        (byte) 110,
        (byte) 92,
        (byte) 46,
        (byte) 88,
        (byte) 59,
        (byte) 118,
        byte.MaxValue,
        (byte) 32 /*0x20*/,
        (byte) 162,
        (byte) 185,
        (byte) 130,
        (byte) 96 /*0x60*/,
        (byte) 50,
        (byte) 138,
        (byte) 119,
        (byte) 226,
        (byte) 119,
        (byte) 164,
        (byte) 101,
        (byte) 13,
        (byte) 0,
        (byte) 41,
        (byte) 107,
        (byte) 120,
        (byte) 45,
        (byte) 248,
        (byte) 222,
        (byte) 139,
        (byte) 188,
        (byte) 168,
        (byte) 154,
        (byte) 75,
        (byte) 65,
        (byte) 203,
        (byte) 189,
        (byte) 100,
        (byte) 213,
        (byte) 15,
        (byte) 60,
        (byte) 237,
        (byte) 230,
        (byte) 230,
        (byte) 14,
        (byte) 235,
        (byte) 13,
        (byte) 51,
        (byte) 77,
        (byte) 84,
        (byte) 236
      };
      byte[] numArray3 = new byte[55];
      numArray3[42] = (byte) 11;
      numArray3[1] = (byte) 17;
      numArray3[2] = (byte) 191;
      numArray3[3] = (byte) 235;
      numArray3[11] = (byte) 210;
      numArray3[5] = (byte) 139;
      numArray3[6] = (byte) 32 /*0x20*/;
      numArray3[53] = (byte) 34;
      numArray3[21] = (byte) 187;
      numArray3[16 /*0x10*/] = (byte) 80 /*0x50*/;
      numArray3[47] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 11;
      numArray3[34] = (byte) 124;
      numArray3[13] = (byte) 5;
      numArray3[9] = (byte) 147;
      numArray3[15] = (byte) 218;
      numArray3[41] = (byte) 43;
      numArray3[0] = (byte) 13;
      numArray3[10] = (byte) 138;
      numArray3[19] = (byte) 41;
      numArray3[20] = (byte) 7;
      numArray3[36] = (byte) 116;
      numArray3[46] = (byte) 74;
      numArray3[23] = (byte) 248;
      numArray3[24] = (byte) 167;
      numArray3[22] = (byte) 234;
      numArray3[29] = (byte) 115;
      numArray3[4] = (byte) 252;
      numArray3[38] = (byte) 45;
      numArray3[12] = (byte) 148;
      numArray3[7] = (byte) 131;
      numArray3[31 /*0x1F*/] = (byte) 229;
      numArray3[32 /*0x20*/] = (byte) 30;
      numArray3[40] = (byte) 56;
      numArray3[50] = (byte) 231;
      numArray3[35] = (byte) 92;
      numArray3[17] = (byte) 237;
      numArray3[52] = (byte) 11;
      numArray3[37] = (byte) 22;
      numArray3[39] = (byte) 62;
      numArray3[25] = (byte) 241;
      numArray3[33] = (byte) 18;
      numArray3[27] = (byte) 89;
      numArray3[43] = (byte) 218;
      numArray3[44] = (byte) 193;
      numArray3[45] = (byte) 130;
      numArray3[18] = (byte) 156;
      numArray3[30] = (byte) 34;
      numArray3[48 /*0x30*/] = (byte) 40;
      numArray3[49] = (byte) 49;
      numArray3[28] = (byte) 212;
      numArray3[51] = (byte) 233;
      numArray3[8] = (byte) 104;
      numArray3[26] = (byte) 122;
      numArray3[54] = (byte) 142;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22]
      {
        (byte) 133,
        (byte) 206,
        (byte) 198,
        (byte) 59,
        (byte) 41,
        (byte) 112 /*0x70*/,
        (byte) 74,
        (byte) 48 /*0x30*/,
        (byte) 152,
        (byte) 14,
        (byte) 212,
        (byte) 11,
        (byte) 139,
        (byte) 113,
        (byte) 50,
        (byte) 203,
        (byte) 239,
        (byte) 84,
        (byte) 159,
        (byte) 237,
        (byte) 131,
        (byte) 110
      };
      byte[] numArray5 = new byte[22]
      {
        (byte) 76,
        (byte) 184,
        (byte) 126,
        (byte) 84,
        (byte) 150,
        (byte) 56,
        (byte) 188,
        (byte) 114,
        (byte) 221,
        (byte) 168,
        (byte) 30,
        (byte) 86,
        (byte) 125,
        (byte) 244,
        (byte) 36,
        (byte) 120,
        (byte) 177,
        (byte) 67,
        (byte) 101,
        (byte) 52,
        (byte) 205,
        (byte) 43
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[77];
    byte[] numArray7 = new byte[55]
    {
      (byte) 5,
      (byte) 179,
      (byte) 148,
      (byte) 230,
      (byte) 196,
      (byte) 2,
      (byte) 198,
      (byte) 1,
      (byte) 155,
      (byte) 39,
      (byte) 54,
      (byte) 141,
      (byte) 60,
      (byte) 78,
      (byte) 3,
      (byte) 212,
      (byte) 179,
      (byte) 97,
      (byte) 70,
      (byte) 157,
      (byte) 120,
      (byte) 137,
      (byte) 91,
      (byte) 164,
      (byte) 73,
      (byte) 247,
      (byte) 51,
      (byte) 57,
      (byte) 105,
      (byte) 79,
      (byte) 22,
      (byte) 170,
      (byte) 17,
      (byte) 164,
      (byte) 36,
      (byte) 188,
      (byte) 182,
      (byte) 74,
      (byte) 211,
      (byte) 11,
      (byte) 14,
      (byte) 9,
      (byte) 240 /*0xF0*/,
      (byte) 73,
      (byte) 251,
      (byte) 61,
      (byte) 179,
      (byte) 5,
      (byte) 155,
      (byte) 25,
      (byte) 207,
      (byte) 13,
      (byte) 220,
      (byte) 22,
      (byte) 29
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 163,
      (byte) 249,
      (byte) 48 /*0x30*/,
      (byte) 56,
      (byte) 50,
      (byte) 68,
      (byte) 248,
      (byte) 111,
      (byte) 35,
      (byte) 130,
      (byte) 174,
      (byte) 185,
      (byte) 236,
      (byte) 214,
      (byte) 144 /*0x90*/,
      (byte) 220,
      (byte) 214,
      (byte) 242,
      (byte) 185,
      (byte) 236,
      (byte) 43,
      (byte) 86,
      (byte) 113,
      (byte) 242,
      (byte) 170,
      (byte) 173,
      (byte) 21,
      (byte) 102,
      (byte) 144 /*0x90*/,
      (byte) 87,
      (byte) 77,
      (byte) 155,
      (byte) 162,
      (byte) 13,
      (byte) 214,
      (byte) 240 /*0xF0*/,
      (byte) 114,
      (byte) 247,
      (byte) 36,
      (byte) 99,
      (byte) 40,
      (byte) 176 /*0xB0*/,
      (byte) 96 /*0x60*/,
      (byte) 48 /*0x30*/,
      (byte) 115,
      (byte) 32 /*0x20*/,
      (byte) 12,
      (byte) 158,
      (byte) 94,
      (byte) 49,
      (byte) 118,
      (byte) 229,
      (byte) 131,
      (byte) 229,
      (byte) 47
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[22];
    numArray9[20] = (byte) 17;
    numArray9[11] = (byte) 42;
    numArray9[2] = (byte) 205;
    numArray9[3] = (byte) 207;
    numArray9[4] = (byte) 140;
    numArray9[13] = (byte) 30;
    numArray9[6] = (byte) 75;
    numArray9[18] = (byte) 110;
    numArray9[21] = (byte) 168;
    numArray9[14] = (byte) 105;
    numArray9[5] = (byte) 152;
    numArray9[7] = (byte) 16 /*0x10*/;
    numArray9[12] = (byte) 70;
    numArray9[10] = (byte) 151;
    numArray9[8] = (byte) 140;
    numArray9[0] = (byte) 240 /*0xF0*/;
    numArray9[16 /*0x10*/] = (byte) 59;
    numArray9[17] = (byte) 142;
    numArray9[15] = (byte) 141;
    numArray9[19] = (byte) 153;
    numArray9[1] = (byte) 156;
    numArray9[9] = (byte) 123;
    byte[] numArray10 = new byte[22]
    {
      (byte) 100,
      (byte) 133,
      (byte) 0,
      (byte) 4,
      (byte) 164,
      (byte) 30,
      (byte) 217,
      (byte) 57,
      (byte) 123,
      (byte) 100,
      (byte) 233,
      (byte) 163,
      (byte) 244,
      (byte) 10,
      (byte) 13,
      (byte) 44,
      (byte) 81,
      (byte) 123,
      (byte) 28,
      (byte) 237,
      (byte) 233,
      (byte) 231
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 22);
    for (int index = 0; index < 22; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12651()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[63 /*0x3F*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 101,
        (byte) 127 /*0x7F*/,
        (byte) 161,
        (byte) 136,
        (byte) 175,
        (byte) 226,
        (byte) 69,
        (byte) 71,
        (byte) 145,
        (byte) 137,
        (byte) 161,
        (byte) 239,
        (byte) 72,
        (byte) 203,
        (byte) 7,
        (byte) 189,
        (byte) 128 /*0x80*/,
        (byte) 16 /*0x10*/,
        (byte) 130,
        (byte) 225,
        (byte) 87,
        (byte) 188,
        (byte) 134,
        (byte) 254,
        (byte) 12,
        (byte) 252,
        (byte) 245,
        (byte) 216,
        (byte) 57,
        (byte) 74,
        (byte) 39,
        (byte) 83,
        (byte) 72,
        (byte) 84,
        (byte) 26,
        (byte) 137,
        (byte) 44,
        (byte) 115,
        (byte) 215,
        (byte) 197,
        (byte) 31 /*0x1F*/,
        (byte) 127 /*0x7F*/,
        (byte) 28,
        (byte) 125,
        (byte) 155,
        (byte) 146,
        (byte) 41,
        (byte) 216,
        (byte) 49,
        (byte) 177,
        (byte) 64 /*0x40*/,
        (byte) 11,
        (byte) 88,
        (byte) 115,
        (byte) 163
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 24,
        (byte) 83,
        (byte) 192 /*0xC0*/,
        (byte) 229,
        (byte) 199,
        (byte) 116,
        (byte) 152,
        (byte) 189,
        (byte) 77,
        (byte) 164,
        (byte) 157,
        (byte) 79,
        (byte) 216,
        (byte) 143,
        (byte) 172,
        (byte) 185,
        (byte) 184,
        (byte) 6,
        (byte) 192 /*0xC0*/,
        (byte) 123,
        (byte) 135,
        (byte) 195,
        (byte) 61,
        (byte) 240 /*0xF0*/,
        (byte) 249,
        (byte) 237,
        (byte) 179,
        (byte) 132,
        (byte) 76,
        (byte) 254,
        (byte) 186,
        (byte) 42,
        (byte) 42,
        (byte) 111,
        (byte) 171,
        (byte) 3,
        (byte) 171,
        (byte) 77,
        (byte) 29,
        (byte) 65,
        (byte) 161,
        (byte) 84,
        (byte) 79,
        (byte) 68,
        (byte) 199,
        (byte) 246,
        (byte) 214,
        (byte) 90,
        (byte) 223,
        (byte) 212,
        (byte) 151,
        (byte) 242,
        (byte) 33,
        (byte) 183,
        (byte) 53
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[8];
      numArray4[0] = (byte) 162;
      numArray4[1] = (byte) 123;
      numArray4[6] = (byte) 70;
      numArray4[5] = (byte) 14;
      numArray4[4] = (byte) 108;
      numArray4[3] = (byte) 237;
      numArray4[2] = (byte) 178;
      numArray4[7] = (byte) 97;
      byte[] numArray5 = new byte[8]
      {
        (byte) 113,
        (byte) 228,
        (byte) 214,
        (byte) 248,
        (byte) 204,
        (byte) 187,
        (byte) 129,
        (byte) 230
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[63 /*0x3F*/];
    byte[] numArray7 = new byte[55];
    numArray7[48 /*0x30*/] = (byte) 236;
    numArray7[54] = (byte) 103;
    numArray7[39] = (byte) 209;
    numArray7[37] = (byte) 1;
    numArray7[4] = (byte) 210;
    numArray7[5] = (byte) 28;
    numArray7[21] = (byte) 213;
    numArray7[50] = (byte) 21;
    numArray7[28] = (byte) 32 /*0x20*/;
    numArray7[9] = (byte) 28;
    numArray7[32 /*0x20*/] = (byte) 177;
    numArray7[29] = (byte) 51;
    numArray7[12] = (byte) 176 /*0xB0*/;
    numArray7[13] = (byte) 113;
    numArray7[11] = (byte) 213;
    numArray7[51] = (byte) 20;
    numArray7[33] = (byte) 203;
    numArray7[17] = (byte) 36;
    numArray7[44] = (byte) 82;
    numArray7[19] = (byte) 179;
    numArray7[20] = (byte) 113;
    numArray7[36] = (byte) 63 /*0x3F*/;
    numArray7[6] = (byte) 166;
    numArray7[23] = (byte) 77;
    numArray7[24] = (byte) 236;
    numArray7[46] = (byte) 66;
    numArray7[26] = (byte) 40;
    numArray7[7] = (byte) 135;
    numArray7[15] = (byte) 137;
    numArray7[16 /*0x10*/] = (byte) 247;
    numArray7[43] = (byte) 69;
    numArray7[31 /*0x1F*/] = (byte) 154;
    numArray7[42] = (byte) 61;
    numArray7[27] = (byte) 180;
    numArray7[34] = (byte) 9;
    numArray7[35] = (byte) 239;
    numArray7[14] = (byte) 144 /*0x90*/;
    numArray7[1] = (byte) 9;
    numArray7[38] = (byte) 224 /*0xE0*/;
    numArray7[18] = (byte) 215;
    numArray7[25] = (byte) 54;
    numArray7[41] = (byte) 134;
    numArray7[2] = (byte) 99;
    numArray7[8] = (byte) 9;
    numArray7[3] = (byte) 121;
    numArray7[45] = (byte) 208 /*0xD0*/;
    numArray7[40] = (byte) 197;
    numArray7[47] = (byte) 63 /*0x3F*/;
    numArray7[30] = (byte) 210;
    numArray7[49] = (byte) 7;
    numArray7[0] = (byte) 160 /*0xA0*/;
    numArray7[22] = (byte) 28;
    numArray7[52] = (byte) 229;
    numArray7[53] = (byte) 14;
    numArray7[10] = (byte) 251;
    byte[] numArray8 = new byte[55];
    numArray8[33] = (byte) 149;
    numArray8[1] = (byte) 117;
    numArray8[2] = (byte) 51;
    numArray8[54] = (byte) 125;
    numArray8[4] = (byte) 222;
    numArray8[44] = (byte) 32 /*0x20*/;
    numArray8[21] = (byte) 45;
    numArray8[7] = (byte) 74;
    numArray8[8] = (byte) 17;
    numArray8[9] = (byte) 75;
    numArray8[40] = (byte) 230;
    numArray8[6] = (byte) 250;
    numArray8[15] = (byte) 65;
    numArray8[14] = (byte) 210;
    numArray8[13] = (byte) 121;
    numArray8[24] = (byte) 196;
    numArray8[29] = (byte) 225;
    numArray8[17] = (byte) 153;
    numArray8[20] = (byte) 190;
    numArray8[19] = (byte) 177;
    numArray8[42] = (byte) 144 /*0x90*/;
    numArray8[31 /*0x1F*/] = (byte) 149;
    numArray8[51] = (byte) 38;
    numArray8[23] = (byte) 142;
    numArray8[35] = (byte) 210;
    numArray8[25] = (byte) 94;
    numArray8[52] = (byte) 140;
    numArray8[0] = (byte) 6;
    numArray8[28] = (byte) 244;
    numArray8[27] = (byte) 80 /*0x50*/;
    numArray8[30] = (byte) 178;
    numArray8[5] = (byte) 143;
    numArray8[11] = (byte) 138;
    numArray8[50] = (byte) 231;
    numArray8[12] = (byte) 119;
    numArray8[22] = (byte) 175;
    numArray8[36] = (byte) 106;
    numArray8[37] = (byte) 219;
    numArray8[3] = (byte) 34;
    numArray8[39] = (byte) 105;
    numArray8[10] = (byte) 36;
    numArray8[41] = (byte) 252;
    numArray8[38] = (byte) 30;
    numArray8[43] = (byte) 131;
    numArray8[18] = (byte) 5;
    numArray8[45] = (byte) 1;
    numArray8[46] = (byte) 74;
    numArray8[49] = (byte) 230;
    numArray8[48 /*0x30*/] = (byte) 203;
    numArray8[34] = (byte) 69;
    numArray8[16 /*0x10*/] = (byte) 239;
    numArray8[26] = (byte) 196;
    numArray8[32 /*0x20*/] = (byte) 148;
    numArray8[53] = (byte) 237;
    numArray8[47] = (byte) 18;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[8];
    numArray9[2] = (byte) 17;
    numArray9[7] = (byte) 156;
    numArray9[1] = (byte) 1;
    numArray9[3] = (byte) 250;
    numArray9[0] = (byte) 19;
    numArray9[5] = (byte) 241;
    numArray9[6] = (byte) 187;
    numArray9[4] = (byte) 175;
    byte[] numArray10 = new byte[8]
    {
      (byte) 136,
      (byte) 233,
      (byte) 146,
      (byte) 88,
      (byte) 185,
      (byte) 25,
      (byte) 165,
      (byte) 221
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 8);
    for (int index = 0; index < 8; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[11];
    byte[] response = new byte[11];
    Array.Copy((Array) sc_12586.sspq, 668, (Array) numArray11, 0, 11);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12586.sspr, 668, (Array) numArray11, 0, 11);
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

  internal static string ssp_appserver_12652()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 9,
        (byte) 178,
        (byte) 248,
        (byte) 1,
        (byte) 137,
        (byte) 215,
        (byte) 248,
        (byte) 223,
        (byte) 242
      };
      byte[] numArray3 = new byte[9];
      numArray3[3] = (byte) 31 /*0x1F*/;
      numArray3[1] = (byte) 138;
      numArray3[7] = (byte) 22;
      numArray3[2] = (byte) 230;
      numArray3[5] = (byte) 218;
      numArray3[6] = (byte) 227;
      numArray3[4] = (byte) 190;
      numArray3[0] = (byte) 78;
      numArray3[8] = (byte) 99;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 127 /*0x7F*/,
      (byte) 208 /*0xD0*/,
      (byte) 81,
      (byte) 190,
      (byte) 86,
      (byte) 152,
      (byte) 227,
      (byte) 47,
      (byte) 90
    };
    byte[] numArray6 = new byte[9];
    numArray6[6] = (byte) 187;
    numArray6[1] = (byte) 14;
    numArray6[8] = (byte) 43;
    numArray6[3] = (byte) 2;
    numArray6[7] = (byte) 218;
    numArray6[4] = (byte) 12;
    numArray6[5] = (byte) 243;
    numArray6[0] = (byte) 212;
    numArray6[2] = (byte) 62;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12653()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[68];
      byte[] numArray2 = new byte[55];
      numArray2[26] = (byte) 0;
      numArray2[1] = (byte) 81;
      numArray2[34] = (byte) 60;
      numArray2[15] = (byte) 67;
      numArray2[7] = (byte) 67;
      numArray2[46] = (byte) 136;
      numArray2[23] = (byte) 173;
      numArray2[17] = (byte) 63 /*0x3F*/;
      numArray2[30] = (byte) 226;
      numArray2[14] = (byte) 166;
      numArray2[54] = (byte) 249;
      numArray2[11] = (byte) 128 /*0x80*/;
      numArray2[16 /*0x10*/] = (byte) 51;
      numArray2[50] = (byte) 168;
      numArray2[42] = (byte) 201;
      numArray2[4] = (byte) 208 /*0xD0*/;
      numArray2[41] = (byte) 23;
      numArray2[45] = (byte) 225;
      numArray2[13] = (byte) 225;
      numArray2[19] = (byte) 175;
      numArray2[20] = (byte) 151;
      numArray2[21] = (byte) 87;
      numArray2[22] = (byte) 185;
      numArray2[33] = (byte) 238;
      numArray2[2] = (byte) 20;
      numArray2[6] = (byte) 60;
      numArray2[35] = (byte) 0;
      numArray2[12] = (byte) 138;
      numArray2[28] = (byte) 159;
      numArray2[29] = (byte) 143;
      numArray2[32 /*0x20*/] = (byte) 154;
      numArray2[31 /*0x1F*/] = (byte) 26;
      numArray2[10] = (byte) 231;
      numArray2[3] = (byte) 119;
      numArray2[53] = (byte) 108;
      numArray2[18] = (byte) 21;
      numArray2[36] = (byte) 169;
      numArray2[37] = (byte) 223;
      numArray2[38] = (byte) 199;
      numArray2[24] = (byte) 225;
      numArray2[40] = (byte) 242;
      numArray2[0] = (byte) 148;
      numArray2[9] = (byte) 190;
      numArray2[43] = (byte) 248;
      numArray2[8] = (byte) 180;
      numArray2[39] = (byte) 21;
      numArray2[5] = (byte) 206;
      numArray2[47] = (byte) 28;
      numArray2[48 /*0x30*/] = (byte) 117;
      numArray2[49] = (byte) 57;
      numArray2[25] = (byte) 126;
      numArray2[51] = (byte) 62;
      numArray2[52] = (byte) 100;
      numArray2[44] = (byte) 153;
      numArray2[27] = (byte) 104;
      byte[] numArray3 = new byte[55]
      {
        (byte) 106,
        (byte) 154,
        (byte) 164,
        (byte) 185,
        (byte) 188,
        (byte) 167,
        (byte) 26,
        (byte) 32 /*0x20*/,
        (byte) 218,
        (byte) 143,
        (byte) 31 /*0x1F*/,
        (byte) 237,
        (byte) 135,
        (byte) 136,
        (byte) 1,
        (byte) 29,
        (byte) 105,
        (byte) 62,
        (byte) 109,
        (byte) 172,
        byte.MaxValue,
        (byte) 105,
        (byte) 41,
        (byte) 123,
        (byte) 48 /*0x30*/,
        (byte) 189,
        (byte) 63 /*0x3F*/,
        (byte) 170,
        (byte) 129,
        (byte) 60,
        (byte) 188,
        (byte) 116,
        (byte) 39,
        (byte) 132,
        (byte) 192 /*0xC0*/,
        (byte) 74,
        (byte) 91,
        (byte) 45,
        (byte) 88,
        (byte) 45,
        (byte) 32 /*0x20*/,
        (byte) 26,
        (byte) 243,
        (byte) 30,
        (byte) 150,
        (byte) 250,
        (byte) 171,
        (byte) 131,
        (byte) 130,
        (byte) 94,
        (byte) 243,
        (byte) 172,
        (byte) 238,
        (byte) 9,
        (byte) 235
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13];
      numArray4[8] = (byte) 23;
      numArray4[1] = (byte) 150;
      numArray4[10] = (byte) 167;
      numArray4[3] = (byte) 233;
      numArray4[12] = (byte) 60;
      numArray4[5] = (byte) 61;
      numArray4[6] = (byte) 61;
      numArray4[7] = (byte) 55;
      numArray4[11] = (byte) 165;
      numArray4[0] = (byte) 82;
      numArray4[2] = (byte) 92;
      numArray4[4] = (byte) 224 /*0xE0*/;
      numArray4[9] = (byte) 184;
      byte[] numArray5 = new byte[13]
      {
        (byte) 191,
        (byte) 13,
        (byte) 186,
        (byte) 78,
        (byte) 56,
        (byte) 79,
        (byte) 242,
        (byte) 192 /*0xC0*/,
        (byte) 169,
        (byte) 101,
        (byte) 147,
        (byte) 105,
        (byte) 163
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[68];
    byte[] numArray7 = new byte[55]
    {
      (byte) 81,
      (byte) 126,
      (byte) 123,
      (byte) 80 /*0x50*/,
      (byte) 147,
      (byte) 240 /*0xF0*/,
      (byte) 178,
      (byte) 25,
      (byte) 93,
      (byte) 61,
      (byte) 54,
      (byte) 102,
      (byte) 229,
      (byte) 181,
      (byte) 66,
      (byte) 68,
      (byte) 209,
      (byte) 147,
      (byte) 73,
      (byte) 73,
      (byte) 197,
      (byte) 171,
      (byte) 17,
      (byte) 109,
      (byte) 196,
      (byte) 50,
      (byte) 10,
      (byte) 144 /*0x90*/,
      (byte) 82,
      (byte) 124,
      (byte) 189,
      (byte) 119,
      (byte) 170,
      (byte) 91,
      (byte) 197,
      (byte) 57,
      (byte) 43,
      (byte) 84,
      (byte) 168,
      (byte) 102,
      (byte) 88,
      (byte) 231,
      (byte) 91,
      (byte) 96 /*0x60*/,
      (byte) 213,
      (byte) 132,
      (byte) 190,
      (byte) 146,
      (byte) 195,
      (byte) 197,
      (byte) 155,
      (byte) 144 /*0x90*/,
      (byte) 124,
      (byte) 73,
      (byte) 23
    };
    byte[] numArray8 = new byte[55];
    numArray8[29] = (byte) 88;
    numArray8[47] = (byte) 245;
    numArray8[2] = (byte) 164;
    numArray8[0] = (byte) 51;
    numArray8[4] = (byte) 232;
    numArray8[17] = (byte) 158;
    numArray8[20] = (byte) 186;
    numArray8[7] = (byte) 9;
    numArray8[8] = (byte) 221;
    numArray8[51] = (byte) 225;
    numArray8[43] = (byte) 179;
    numArray8[11] = (byte) 148;
    numArray8[54] = (byte) 1;
    numArray8[13] = (byte) 123;
    numArray8[12] = (byte) 85;
    numArray8[15] = (byte) 126;
    numArray8[22] = (byte) 233;
    numArray8[19] = (byte) 159;
    numArray8[18] = (byte) 140;
    numArray8[48 /*0x30*/] = (byte) 139;
    numArray8[37] = (byte) 162;
    numArray8[21] = (byte) 144 /*0x90*/;
    numArray8[1] = (byte) 138;
    numArray8[41] = (byte) 187;
    numArray8[24] = (byte) 98;
    numArray8[3] = (byte) 80 /*0x50*/;
    numArray8[26] = (byte) 1;
    numArray8[27] = (byte) 252;
    numArray8[28] = (byte) 59;
    numArray8[42] = (byte) 214;
    numArray8[38] = (byte) 7;
    numArray8[35] = (byte) 205;
    numArray8[32 /*0x20*/] = (byte) 134;
    numArray8[33] = (byte) 95;
    numArray8[30] = (byte) 172;
    numArray8[31 /*0x1F*/] = (byte) 200;
    numArray8[36] = (byte) 247;
    numArray8[46] = (byte) 220;
    numArray8[34] = (byte) 61;
    numArray8[9] = (byte) 9;
    numArray8[40] = (byte) 54;
    numArray8[10] = (byte) 36;
    numArray8[5] = (byte) 59;
    numArray8[45] = (byte) 250;
    numArray8[44] = (byte) 128 /*0x80*/;
    numArray8[16 /*0x10*/] = (byte) 90;
    numArray8[14] = (byte) 179;
    numArray8[39] = (byte) 91;
    numArray8[25] = (byte) 181;
    numArray8[49] = (byte) 137;
    numArray8[50] = (byte) 22;
    numArray8[23] = (byte) 251;
    numArray8[52] = (byte) 197;
    numArray8[53] = (byte) 249;
    numArray8[6] = (byte) 133;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[13];
    numArray9[6] = byte.MaxValue;
    numArray9[1] = (byte) 76;
    numArray9[2] = (byte) 236;
    numArray9[9] = (byte) 90;
    numArray9[4] = (byte) 85;
    numArray9[5] = (byte) 58;
    numArray9[12] = (byte) 60;
    numArray9[3] = (byte) 109;
    numArray9[8] = (byte) 196;
    numArray9[7] = (byte) 84;
    numArray9[10] = (byte) 70;
    numArray9[11] = (byte) 208 /*0xD0*/;
    numArray9[0] = (byte) 118;
    byte[] numArray10 = new byte[13]
    {
      (byte) 152,
      (byte) 168,
      (byte) 48 /*0x30*/,
      (byte) 5,
      (byte) 175,
      (byte) 30,
      (byte) 250,
      (byte) 226,
      (byte) 25,
      (byte) 163,
      (byte) 142,
      (byte) 134,
      (byte) 35
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 13);
    for (int index = 0; index < 13; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12654()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 7,
        (byte) 9,
        (byte) 201,
        (byte) 14,
        (byte) 166,
        (byte) 123,
        (byte) 91,
        (byte) 19,
        (byte) 224 /*0xE0*/
      };
      byte[] numArray3 = new byte[9]
      {
        (byte) 221,
        (byte) 145,
        (byte) 127 /*0x7F*/,
        (byte) 192 /*0xC0*/,
        (byte) 183,
        (byte) 67,
        (byte) 34,
        (byte) 142,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9]
    {
      (byte) 80 /*0x50*/,
      (byte) 110,
      (byte) 237,
      (byte) 210,
      (byte) 126,
      (byte) 177,
      (byte) 105,
      (byte) 4,
      (byte) 55
    };
    byte[] numArray6 = new byte[9]
    {
      (byte) 189,
      (byte) 135,
      (byte) 96 /*0x60*/,
      (byte) 196,
      (byte) 2,
      (byte) 163,
      (byte) 92,
      (byte) 158,
      (byte) 92
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12655(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 171;
    sourceArray1[36] = (byte) 122;
    sourceArray1[17] = (byte) 214;
    sourceArray1[18] = (byte) 92;
    sourceArray1[4] = (byte) 90;
    sourceArray1[47] = (byte) 47;
    sourceArray1[43] = (byte) 176 /*0xB0*/;
    sourceArray1[9] = (byte) 60;
    sourceArray1[8] = (byte) 82;
    sourceArray1[11] = (byte) 45;
    sourceArray1[27] = (byte) 129;
    sourceArray1[5] = (byte) 61;
    sourceArray1[12] = (byte) 85;
    sourceArray1[14] = (byte) 70;
    sourceArray1[24] = (byte) 147;
    sourceArray1[15] = (byte) 79;
    sourceArray1[16 /*0x10*/] = (byte) 218;
    sourceArray1[45] = (byte) 81;
    sourceArray1[20] = (byte) 49;
    sourceArray1[41] = (byte) 237;
    sourceArray1[0] = (byte) 213;
    sourceArray1[44] = (byte) 204;
    sourceArray1[22] = (byte) 83;
    sourceArray1[23] = (byte) 2;
    sourceArray1[46] = (byte) 91;
    sourceArray1[25] = (byte) 47;
    sourceArray1[32 /*0x20*/] = (byte) 34;
    sourceArray1[42] = (byte) 27;
    sourceArray1[39] = (byte) 60;
    sourceArray1[29] = (byte) 105;
    sourceArray1[30] = (byte) 140;
    sourceArray1[7] = (byte) 99;
    sourceArray1[2] = (byte) 152;
    sourceArray1[33] = (byte) 178;
    sourceArray1[10] = (byte) 6;
    sourceArray1[35] = (byte) 175;
    sourceArray1[3] = (byte) 67;
    sourceArray1[37] = (byte) 156;
    sourceArray1[38] = (byte) 140;
    sourceArray1[19] = (byte) 69;
    sourceArray1[40] = (byte) 188;
    sourceArray1[34] = (byte) 165;
    sourceArray1[31 /*0x1F*/] = (byte) 75;
    sourceArray1[13] = (byte) 57;
    sourceArray1[6] = (byte) 206;
    sourceArray1[26] = (byte) 88;
    sourceArray1[28] = (byte) 41;
    sourceArray1[21] = (byte) 9;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 149,
      (byte) 154,
      (byte) 197,
      (byte) 45,
      (byte) 191,
      (byte) 243,
      (byte) 108,
      (byte) 199,
      (byte) 144 /*0x90*/,
      (byte) 215,
      (byte) 13,
      (byte) 123,
      (byte) 243,
      (byte) 6,
      (byte) 87,
      (byte) 126,
      (byte) 98,
      (byte) 200,
      (byte) 249,
      (byte) 5,
      (byte) 53,
      (byte) 214,
      (byte) 218,
      (byte) 179,
      (byte) 219,
      (byte) 62,
      (byte) 183,
      (byte) 249,
      (byte) 125,
      (byte) 4,
      (byte) 36,
      (byte) 43,
      (byte) 26,
      (byte) 198,
      (byte) 93,
      (byte) 91,
      (byte) 23,
      (byte) 252,
      (byte) 88,
      (byte) 75,
      (byte) 229,
      (byte) 162,
      (byte) 121,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 246,
      (byte) 225,
      (byte) 189
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12656(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 184,
      (byte) 155,
      (byte) 39,
      (byte) 19,
      (byte) 133,
      (byte) 92,
      (byte) 2,
      (byte) 28,
      (byte) 125,
      (byte) 222,
      (byte) 181,
      (byte) 95,
      (byte) 110,
      (byte) 87,
      (byte) 187,
      (byte) 138,
      (byte) 248,
      (byte) 67,
      (byte) 202,
      (byte) 146,
      (byte) 141,
      (byte) 179,
      (byte) 187,
      (byte) 63 /*0x3F*/,
      (byte) 240 /*0xF0*/,
      (byte) 82,
      (byte) 232,
      (byte) 199,
      (byte) 26,
      (byte) 145,
      (byte) 187,
      (byte) 162,
      (byte) 37,
      (byte) 251,
      (byte) 26,
      (byte) 141,
      (byte) 156,
      (byte) 45,
      (byte) 32 /*0x20*/,
      (byte) 136,
      (byte) 38,
      (byte) 96 /*0x60*/,
      (byte) 114,
      (byte) 250,
      (byte) 161,
      (byte) 132,
      (byte) 125,
      (byte) 9
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 76;
    sourceArray2[4] = (byte) 167;
    sourceArray2[29] = (byte) 83;
    sourceArray2[37] = (byte) 50;
    sourceArray2[35] = byte.MaxValue;
    sourceArray2[9] = (byte) 36;
    sourceArray2[0] = (byte) 174;
    sourceArray2[8] = (byte) 5;
    sourceArray2[39] = (byte) 193;
    sourceArray2[27] = (byte) 84;
    sourceArray2[19] = (byte) 192 /*0xC0*/;
    sourceArray2[11] = (byte) 105;
    sourceArray2[12] = (byte) 55;
    sourceArray2[13] = (byte) 96 /*0x60*/;
    sourceArray2[14] = (byte) 241;
    sourceArray2[46] = (byte) 106;
    sourceArray2[16 /*0x10*/] = (byte) 84;
    sourceArray2[17] = (byte) 48 /*0x30*/;
    sourceArray2[15] = (byte) 42;
    sourceArray2[28] = (byte) 2;
    sourceArray2[32 /*0x20*/] = (byte) 5;
    sourceArray2[21] = (byte) 21;
    sourceArray2[22] = (byte) 137;
    sourceArray2[10] = (byte) 137;
    sourceArray2[24] = (byte) 33;
    sourceArray2[25] = (byte) 136;
    sourceArray2[26] = (byte) 195;
    sourceArray2[42] = (byte) 62;
    sourceArray2[2] = (byte) 9;
    sourceArray2[7] = (byte) 31 /*0x1F*/;
    sourceArray2[30] = (byte) 112 /*0x70*/;
    sourceArray2[31 /*0x1F*/] = (byte) 30;
    sourceArray2[18] = (byte) 239;
    sourceArray2[33] = (byte) 26;
    sourceArray2[6] = (byte) 107;
    sourceArray2[3] = (byte) 40;
    sourceArray2[36] = (byte) 206;
    sourceArray2[41] = (byte) 146;
    sourceArray2[38] = (byte) 158;
    sourceArray2[20] = (byte) 64 /*0x40*/;
    sourceArray2[40] = (byte) 56;
    sourceArray2[1] = (byte) 168;
    sourceArray2[23] = (byte) 181;
    sourceArray2[43] = (byte) 74;
    sourceArray2[44] = (byte) 0;
    sourceArray2[45] = (byte) 227;
    sourceArray2[34] = (byte) 17;
    sourceArray2[47] = (byte) 166;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12657()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[72];
      byte[] numArray2 = new byte[55];
      numArray2[45] = (byte) 141;
      numArray2[1] = (byte) 9;
      numArray2[6] = (byte) 204;
      numArray2[0] = (byte) 73;
      numArray2[4] = (byte) 246;
      numArray2[5] = (byte) 238;
      numArray2[16 /*0x10*/] = (byte) 16 /*0x10*/;
      numArray2[26] = (byte) 193;
      numArray2[8] = (byte) 19;
      numArray2[9] = (byte) 139;
      numArray2[46] = (byte) 106;
      numArray2[10] = (byte) 196;
      numArray2[13] = (byte) 183;
      numArray2[47] = (byte) 82;
      numArray2[27] = (byte) 52;
      numArray2[25] = (byte) 57;
      numArray2[38] = (byte) 63 /*0x3F*/;
      numArray2[14] = (byte) 94;
      numArray2[18] = (byte) 20;
      numArray2[39] = (byte) 232;
      numArray2[20] = (byte) 167;
      numArray2[21] = (byte) 148;
      numArray2[22] = (byte) 48 /*0x30*/;
      numArray2[36] = (byte) 17;
      numArray2[24] = (byte) 128 /*0x80*/;
      numArray2[19] = (byte) 85;
      numArray2[3] = (byte) 111;
      numArray2[44] = (byte) 79;
      numArray2[41] = (byte) 120;
      numArray2[34] = (byte) 34;
      numArray2[30] = (byte) 206;
      numArray2[31 /*0x1F*/] = (byte) 186;
      numArray2[32 /*0x20*/] = (byte) 232;
      numArray2[33] = (byte) 16 /*0x10*/;
      numArray2[15] = (byte) 149;
      numArray2[11] = (byte) 31 /*0x1F*/;
      numArray2[7] = (byte) 243;
      numArray2[35] = (byte) 203;
      numArray2[28] = (byte) 46;
      numArray2[2] = (byte) 205;
      numArray2[37] = (byte) 187;
      numArray2[40] = (byte) 0;
      numArray2[42] = (byte) 146;
      numArray2[43] = (byte) 182;
      numArray2[48 /*0x30*/] = (byte) 143;
      numArray2[23] = (byte) 40;
      numArray2[29] = (byte) 127 /*0x7F*/;
      numArray2[12] = (byte) 73;
      numArray2[53] = (byte) 130;
      numArray2[17] = (byte) 203;
      numArray2[50] = (byte) 219;
      numArray2[51] = (byte) 147;
      numArray2[52] = (byte) 113;
      numArray2[49] = (byte) 126;
      numArray2[54] = (byte) 229;
      byte[] numArray3 = new byte[55]
      {
        (byte) 115,
        (byte) 21,
        (byte) 177,
        (byte) 249,
        (byte) 137,
        (byte) 89,
        (byte) 85,
        (byte) 42,
        (byte) 227,
        (byte) 223,
        (byte) 108,
        (byte) 139,
        (byte) 151,
        (byte) 112 /*0x70*/,
        (byte) 220,
        (byte) 116,
        (byte) 69,
        (byte) 229,
        (byte) 49,
        (byte) 179,
        (byte) 136,
        (byte) 144 /*0x90*/,
        (byte) 112 /*0x70*/,
        (byte) 230,
        (byte) 182,
        (byte) 125,
        (byte) 191,
        (byte) 201,
        (byte) 252,
        (byte) 184,
        (byte) 53,
        (byte) 192 /*0xC0*/,
        (byte) 213,
        (byte) 192 /*0xC0*/,
        (byte) 99,
        (byte) 92,
        (byte) 48 /*0x30*/,
        (byte) 55,
        (byte) 230,
        (byte) 159,
        (byte) 183,
        (byte) 12,
        (byte) 244,
        (byte) 31 /*0x1F*/,
        (byte) 55,
        (byte) 57,
        (byte) 243,
        (byte) 24,
        (byte) 149,
        (byte) 153,
        (byte) 31 /*0x1F*/,
        (byte) 75,
        (byte) 149,
        (byte) 35,
        (byte) 9
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17];
      numArray4[6] = (byte) 241;
      numArray4[1] = (byte) 252;
      numArray4[2] = (byte) 238;
      numArray4[3] = (byte) 188;
      numArray4[4] = (byte) 241;
      numArray4[5] = (byte) 21;
      numArray4[0] = (byte) 204;
      numArray4[7] = (byte) 130;
      numArray4[14] = (byte) 137;
      numArray4[11] = (byte) 39;
      numArray4[9] = (byte) 208 /*0xD0*/;
      numArray4[10] = (byte) 14;
      numArray4[12] = (byte) 161;
      numArray4[13] = (byte) 23;
      numArray4[15] = (byte) 9;
      numArray4[8] = (byte) 192 /*0xC0*/;
      numArray4[16 /*0x10*/] = (byte) 111;
      byte[] numArray5 = new byte[17]
      {
        (byte) 20,
        (byte) 157,
        (byte) 184,
        (byte) 165,
        (byte) 46,
        (byte) 127 /*0x7F*/,
        (byte) 45,
        (byte) 161,
        (byte) 228,
        (byte) 128 /*0x80*/,
        (byte) 117,
        (byte) 141,
        (byte) 8,
        (byte) 41,
        (byte) 22,
        (byte) 5,
        (byte) 0
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[72];
    byte[] numArray7 = new byte[55];
    numArray7[50] = (byte) 12;
    numArray7[38] = (byte) 8;
    numArray7[33] = (byte) 116;
    numArray7[3] = (byte) 12;
    numArray7[1] = (byte) 118;
    numArray7[21] = (byte) 9;
    numArray7[6] = (byte) 133;
    numArray7[7] = (byte) 34;
    numArray7[8] = (byte) 149;
    numArray7[2] = (byte) 157;
    numArray7[19] = (byte) 96 /*0x60*/;
    numArray7[11] = (byte) 252;
    numArray7[12] = (byte) 254;
    numArray7[31 /*0x1F*/] = (byte) 119;
    numArray7[14] = (byte) 63 /*0x3F*/;
    numArray7[15] = (byte) 59;
    numArray7[16 /*0x10*/] = (byte) 174;
    numArray7[17] = (byte) 223;
    numArray7[18] = (byte) 57;
    numArray7[47] = (byte) 44;
    numArray7[34] = (byte) 111;
    numArray7[4] = (byte) 119;
    numArray7[5] = (byte) 132;
    numArray7[23] = (byte) 95;
    numArray7[25] = (byte) 79;
    numArray7[30] = (byte) 14;
    numArray7[9] = (byte) 90;
    numArray7[27] = (byte) 151;
    numArray7[35] = (byte) 118;
    numArray7[49] = (byte) 55;
    numArray7[20] = (byte) 80 /*0x50*/;
    numArray7[52] = (byte) 102;
    numArray7[39] = (byte) 13;
    numArray7[51] = (byte) 179;
    numArray7[22] = (byte) 72;
    numArray7[44] = (byte) 211;
    numArray7[36] = (byte) 222;
    numArray7[37] = (byte) 248;
    numArray7[28] = (byte) 155;
    numArray7[29] = (byte) 108;
    numArray7[40] = (byte) 21;
    numArray7[41] = (byte) 122;
    numArray7[42] = (byte) 29;
    numArray7[43] = (byte) 79;
    numArray7[24] = (byte) 211;
    numArray7[48 /*0x30*/] = (byte) 249;
    numArray7[46] = (byte) 52;
    numArray7[10] = (byte) 226;
    numArray7[45] = (byte) 32 /*0x20*/;
    numArray7[0] = byte.MaxValue;
    numArray7[13] = (byte) 214;
    numArray7[32 /*0x20*/] = (byte) 185;
    numArray7[26] = (byte) 189;
    numArray7[53] = (byte) 110;
    numArray7[54] = (byte) 67;
    byte[] numArray8 = new byte[55];
    numArray8[38] = (byte) 23;
    numArray8[26] = (byte) 43;
    numArray8[21] = (byte) 25;
    numArray8[32 /*0x20*/] = (byte) 203;
    numArray8[50] = (byte) 70;
    numArray8[6] = (byte) 3;
    numArray8[48 /*0x30*/] = (byte) 236;
    numArray8[13] = (byte) 95;
    numArray8[41] = (byte) 167;
    numArray8[9] = (byte) 18;
    numArray8[51] = (byte) 177;
    numArray8[11] = (byte) 238;
    numArray8[12] = (byte) 85;
    numArray8[49] = (byte) 71;
    numArray8[8] = (byte) 182;
    numArray8[15] = (byte) 170;
    numArray8[1] = (byte) 62;
    numArray8[7] = (byte) 33;
    numArray8[18] = (byte) 202;
    numArray8[19] = (byte) 213;
    numArray8[14] = (byte) 117;
    numArray8[16 /*0x10*/] = (byte) 19;
    numArray8[3] = (byte) 129;
    numArray8[23] = (byte) 79;
    numArray8[24] = (byte) 135;
    numArray8[40] = (byte) 101;
    numArray8[53] = (byte) 225;
    numArray8[25] = (byte) 38;
    numArray8[28] = (byte) 67;
    numArray8[29] = (byte) 76;
    numArray8[47] = (byte) 110;
    numArray8[31 /*0x1F*/] = (byte) 84;
    numArray8[2] = (byte) 72;
    numArray8[22] = (byte) 38;
    numArray8[34] = (byte) 19;
    numArray8[35] = (byte) 154;
    numArray8[30] = (byte) 92;
    numArray8[37] = (byte) 46;
    numArray8[20] = (byte) 65;
    numArray8[39] = (byte) 40;
    numArray8[46] = (byte) 20;
    numArray8[33] = (byte) 24;
    numArray8[54] = (byte) 9;
    numArray8[43] = (byte) 6;
    numArray8[44] = (byte) 122;
    numArray8[45] = (byte) 151;
    numArray8[36] = (byte) 138;
    numArray8[42] = (byte) 203;
    numArray8[4] = (byte) 48 /*0x30*/;
    numArray8[5] = (byte) 243;
    numArray8[27] = (byte) 103;
    numArray8[10] = (byte) 101;
    numArray8[52] = (byte) 147;
    numArray8[0] = (byte) 74;
    numArray8[17] = (byte) 109;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[17];
    numArray9[1] = (byte) 174;
    numArray9[8] = (byte) 132;
    numArray9[15] = (byte) 35;
    numArray9[5] = (byte) 156;
    numArray9[4] = (byte) 226;
    numArray9[6] = (byte) 191;
    numArray9[2] = (byte) 159;
    numArray9[7] = (byte) 114;
    numArray9[11] = (byte) 124;
    numArray9[3] = (byte) 108;
    numArray9[10] = (byte) 149;
    numArray9[14] = (byte) 187;
    numArray9[12] = (byte) 111;
    numArray9[13] = (byte) 205;
    numArray9[9] = (byte) 208 /*0xD0*/;
    numArray9[0] = (byte) 140;
    numArray9[16 /*0x10*/] = (byte) 214;
    byte[] numArray10 = new byte[17]
    {
      (byte) 197,
      (byte) 186,
      (byte) 142,
      (byte) 221,
      (byte) 222,
      (byte) 45,
      (byte) 59,
      (byte) 125,
      (byte) 145,
      (byte) 25,
      (byte) 230,
      (byte) 180,
      (byte) 49,
      (byte) 130,
      (byte) 119,
      (byte) 89,
      (byte) 22
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 17);
    for (int index = 0; index < 17; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12658()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 244,
        (byte) 49,
        (byte) 109,
        (byte) 159,
        (byte) 136,
        (byte) 20,
        (byte) 190,
        (byte) 93,
        (byte) 63 /*0x3F*/
      };
      byte[] numArray3 = new byte[9];
      numArray3[0] = (byte) 43;
      numArray3[1] = (byte) 71;
      numArray3[2] = (byte) 149;
      numArray3[7] = (byte) 105;
      numArray3[4] = (byte) 77;
      numArray3[3] = (byte) 89;
      numArray3[5] = (byte) 240 /*0xF0*/;
      numArray3[6] = (byte) 119;
      numArray3[8] = (byte) 118;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[1] = (byte) 49;
    numArray5[3] = (byte) 218;
    numArray5[2] = (byte) 22;
    numArray5[6] = (byte) 92;
    numArray5[5] = (byte) 168;
    numArray5[8] = (byte) 156;
    numArray5[0] = (byte) 35;
    numArray5[7] = (byte) 31 /*0x1F*/;
    numArray5[4] = (byte) 3;
    byte[] numArray6 = new byte[9]
    {
      (byte) 173,
      (byte) 132,
      (byte) 35,
      (byte) 139,
      (byte) 75,
      (byte) 102,
      (byte) 139,
      (byte) 154,
      (byte) 139
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12659()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[142];
      byte[] numArray2 = new byte[55];
      numArray2[45] = (byte) 12;
      numArray2[1] = (byte) 3;
      numArray2[41] = (byte) 38;
      numArray2[35] = (byte) 18;
      numArray2[54] = (byte) 222;
      numArray2[5] = (byte) 194;
      numArray2[44] = (byte) 63 /*0x3F*/;
      numArray2[7] = (byte) 118;
      numArray2[2] = (byte) 212;
      numArray2[14] = (byte) 86;
      numArray2[42] = (byte) 127 /*0x7F*/;
      numArray2[6] = (byte) 18;
      numArray2[21] = (byte) 227;
      numArray2[13] = (byte) 0;
      numArray2[47] = (byte) 226;
      numArray2[15] = (byte) 196;
      numArray2[33] = (byte) 162;
      numArray2[8] = (byte) 236;
      numArray2[10] = (byte) 175;
      numArray2[18] = (byte) 97;
      numArray2[20] = (byte) 162;
      numArray2[39] = (byte) 154;
      numArray2[50] = (byte) 9;
      numArray2[23] = (byte) 212;
      numArray2[24] = (byte) 75;
      numArray2[25] = (byte) 90;
      numArray2[53] = (byte) 83;
      numArray2[27] = (byte) 238;
      numArray2[11] = (byte) 38;
      numArray2[9] = (byte) 207;
      numArray2[12] = (byte) 209;
      numArray2[0] = (byte) 27;
      numArray2[32 /*0x20*/] = (byte) 234;
      numArray2[22] = (byte) 116;
      numArray2[34] = (byte) 21;
      numArray2[28] = (byte) 23;
      numArray2[36] = (byte) 6;
      numArray2[37] = (byte) 0;
      numArray2[38] = (byte) 51;
      numArray2[29] = (byte) 231;
      numArray2[40] = (byte) 13;
      numArray2[31 /*0x1F*/] = (byte) 38;
      numArray2[19] = (byte) 62;
      numArray2[43] = (byte) 96 /*0x60*/;
      numArray2[3] = (byte) 2;
      numArray2[16 /*0x10*/] = (byte) 133;
      numArray2[46] = (byte) 135;
      numArray2[49] = (byte) 127 /*0x7F*/;
      numArray2[30] = (byte) 224 /*0xE0*/;
      numArray2[48 /*0x30*/] = (byte) 4;
      numArray2[17] = (byte) 108;
      numArray2[51] = (byte) 111;
      numArray2[52] = (byte) 251;
      numArray2[4] = (byte) 117;
      numArray2[26] = (byte) 147;
      byte[] numArray3 = new byte[55];
      numArray3[20] = (byte) 244;
      numArray3[0] = (byte) 221;
      numArray3[2] = (byte) 182;
      numArray3[13] = (byte) 144 /*0x90*/;
      numArray3[4] = (byte) 177;
      numArray3[17] = (byte) 34;
      numArray3[6] = (byte) 127 /*0x7F*/;
      numArray3[39] = (byte) 4;
      numArray3[32 /*0x20*/] = (byte) 64 /*0x40*/;
      numArray3[7] = (byte) 160 /*0xA0*/;
      numArray3[35] = (byte) 67;
      numArray3[11] = (byte) 144 /*0x90*/;
      numArray3[12] = (byte) 252;
      numArray3[48 /*0x30*/] = (byte) 142;
      numArray3[25] = (byte) 199;
      numArray3[19] = (byte) 73;
      numArray3[16 /*0x10*/] = (byte) 62;
      numArray3[10] = (byte) 96 /*0x60*/;
      numArray3[18] = (byte) 131;
      numArray3[31 /*0x1F*/] = (byte) 254;
      numArray3[52] = (byte) 119;
      numArray3[40] = (byte) 191;
      numArray3[22] = (byte) 155;
      numArray3[37] = (byte) 59;
      numArray3[26] = (byte) 88;
      numArray3[24] = (byte) 254;
      numArray3[45] = (byte) 206;
      numArray3[27] = (byte) 141;
      numArray3[28] = (byte) 18;
      numArray3[1] = (byte) 153;
      numArray3[15] = (byte) 217;
      numArray3[21] = (byte) 1;
      numArray3[23] = (byte) 122;
      numArray3[33] = (byte) 66;
      numArray3[34] = (byte) 66;
      numArray3[46] = (byte) 64 /*0x40*/;
      numArray3[36] = (byte) 184;
      numArray3[49] = (byte) 132;
      numArray3[38] = (byte) 164;
      numArray3[29] = (byte) 2;
      numArray3[8] = (byte) 247;
      numArray3[5] = (byte) 178;
      numArray3[42] = (byte) 166;
      numArray3[43] = (byte) 94;
      numArray3[54] = (byte) 230;
      numArray3[50] = (byte) 175;
      numArray3[9] = (byte) 24;
      numArray3[47] = (byte) 73;
      numArray3[41] = (byte) 244;
      numArray3[44] = (byte) 141;
      numArray3[14] = (byte) 96 /*0x60*/;
      numArray3[51] = (byte) 48 /*0x30*/;
      numArray3[3] = (byte) 109;
      numArray3[53] = (byte) 138;
      numArray3[30] = (byte) 110;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 140,
        (byte) 9,
        (byte) 205,
        (byte) 195,
        (byte) 15,
        (byte) 241,
        (byte) 207,
        (byte) 66,
        (byte) 104,
        (byte) 50,
        (byte) 8,
        (byte) 4,
        (byte) 182,
        (byte) 28,
        (byte) 104,
        (byte) 81,
        (byte) 109,
        (byte) 150,
        (byte) 183,
        (byte) 151,
        (byte) 173,
        (byte) 134,
        (byte) 68,
        (byte) 140,
        (byte) 209,
        (byte) 40,
        (byte) 205,
        (byte) 12,
        (byte) 38,
        (byte) 27,
        (byte) 152,
        (byte) 181,
        (byte) 31 /*0x1F*/,
        (byte) 48 /*0x30*/,
        (byte) 14,
        (byte) 200,
        (byte) 3,
        (byte) 41,
        (byte) 160 /*0xA0*/,
        (byte) 12,
        (byte) 131,
        (byte) 177,
        (byte) 239,
        (byte) 247,
        (byte) 229,
        (byte) 228,
        (byte) 74,
        (byte) 235,
        (byte) 227,
        (byte) 195,
        (byte) 108,
        (byte) 68,
        (byte) 244,
        (byte) 82,
        (byte) 42
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 242,
        (byte) 34,
        (byte) 221,
        (byte) 142,
        (byte) 89,
        (byte) 1,
        (byte) 106,
        (byte) 92,
        (byte) 42,
        (byte) 193,
        (byte) 232,
        (byte) 28,
        (byte) 238,
        (byte) 230,
        (byte) 152,
        (byte) 201,
        (byte) 185,
        (byte) 182,
        (byte) 90,
        (byte) 176 /*0xB0*/,
        (byte) 68,
        (byte) 58,
        (byte) 164,
        (byte) 18,
        (byte) 7,
        (byte) 200,
        (byte) 13,
        (byte) 41,
        (byte) 118,
        (byte) 22,
        (byte) 49,
        (byte) 43,
        (byte) 39,
        (byte) 183,
        (byte) 221,
        (byte) 169,
        (byte) 153,
        (byte) 65,
        (byte) 101,
        (byte) 143,
        (byte) 152,
        (byte) 198,
        (byte) 178,
        (byte) 108,
        (byte) 17,
        (byte) 219,
        (byte) 178,
        (byte) 213,
        (byte) 208 /*0xD0*/,
        (byte) 215,
        (byte) 207,
        (byte) 133,
        (byte) 190,
        (byte) 218,
        (byte) 177
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[32 /*0x20*/]
      {
        (byte) 64 /*0x40*/,
        (byte) 242,
        (byte) 231,
        (byte) 35,
        (byte) 194,
        (byte) 27,
        (byte) 77,
        (byte) 153,
        (byte) 83,
        (byte) 228,
        (byte) 133,
        (byte) 14,
        (byte) 160 /*0xA0*/,
        (byte) 77,
        (byte) 68,
        (byte) 246,
        (byte) 171,
        (byte) 19,
        (byte) 252,
        (byte) 151,
        (byte) 76,
        (byte) 167,
        (byte) 236,
        (byte) 222,
        (byte) 226,
        (byte) 41,
        (byte) 99,
        (byte) 174,
        (byte) 84,
        (byte) 192 /*0xC0*/,
        (byte) 204,
        (byte) 6
      };
      byte[] numArray7 = new byte[32 /*0x20*/]
      {
        (byte) 224 /*0xE0*/,
        (byte) 50,
        (byte) 45,
        (byte) 26,
        (byte) 162,
        (byte) 83,
        (byte) 247,
        (byte) 12,
        (byte) 149,
        (byte) 0,
        (byte) 135,
        (byte) 105,
        (byte) 122,
        (byte) 168,
        (byte) 86,
        (byte) 251,
        (byte) 158,
        (byte) 75,
        (byte) 1,
        (byte) 222,
        (byte) 91,
        (byte) 200,
        (byte) 188,
        (byte) 216,
        (byte) 145,
        (byte) 103,
        (byte) 127 /*0x7F*/,
        (byte) 107,
        (byte) 110,
        (byte) 226,
        (byte) 250,
        (byte) 236
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[142];
    byte[] numArray9 = new byte[55];
    numArray9[12] = (byte) 117;
    numArray9[1] = (byte) 185;
    numArray9[2] = (byte) 232;
    numArray9[42] = (byte) 236;
    numArray9[4] = (byte) 149;
    numArray9[29] = (byte) 23;
    numArray9[7] = (byte) 145;
    numArray9[49] = (byte) 118;
    numArray9[39] = (byte) 244;
    numArray9[25] = (byte) 184;
    numArray9[32 /*0x20*/] = (byte) 157;
    numArray9[11] = (byte) 67;
    numArray9[5] = (byte) 162;
    numArray9[46] = (byte) 238;
    numArray9[14] = (byte) 77;
    numArray9[44] = (byte) 4;
    numArray9[3] = (byte) 192 /*0xC0*/;
    numArray9[17] = (byte) 46;
    numArray9[18] = (byte) 111;
    numArray9[19] = (byte) 121;
    numArray9[20] = (byte) 153;
    numArray9[21] = (byte) 5;
    numArray9[30] = (byte) 127 /*0x7F*/;
    numArray9[38] = (byte) 102;
    numArray9[24] = (byte) 153;
    numArray9[0] = (byte) 55;
    numArray9[26] = (byte) 158;
    numArray9[37] = (byte) 83;
    numArray9[27] = (byte) 69;
    numArray9[45] = (byte) 230;
    numArray9[33] = (byte) 139;
    numArray9[31 /*0x1F*/] = (byte) 6;
    numArray9[22] = (byte) 9;
    numArray9[54] = (byte) 41;
    numArray9[34] = (byte) 12;
    numArray9[23] = (byte) 65;
    numArray9[36] = (byte) 194;
    numArray9[9] = (byte) 196;
    numArray9[15] = (byte) 73;
    numArray9[10] = (byte) 221;
    numArray9[40] = (byte) 238;
    numArray9[41] = (byte) 21;
    numArray9[53] = (byte) 137;
    numArray9[6] = (byte) 94;
    numArray9[35] = (byte) 60;
    numArray9[8] = byte.MaxValue;
    numArray9[47] = (byte) 134;
    numArray9[50] = (byte) 141;
    numArray9[48 /*0x30*/] = (byte) 6;
    numArray9[28] = (byte) 147;
    numArray9[43] = (byte) 25;
    numArray9[51] = (byte) 216;
    numArray9[52] = (byte) 153;
    numArray9[13] = (byte) 40;
    numArray9[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    byte[] numArray10 = new byte[55];
    numArray10[23] = (byte) 6;
    numArray10[1] = (byte) 201;
    numArray10[2] = (byte) 150;
    numArray10[27] = (byte) 188;
    numArray10[46] = (byte) 202;
    numArray10[54] = (byte) 247;
    numArray10[6] = (byte) 139;
    numArray10[7] = (byte) 212;
    numArray10[0] = (byte) 9;
    numArray10[9] = (byte) 142;
    numArray10[14] = (byte) 88;
    numArray10[5] = (byte) 12;
    numArray10[12] = (byte) 206;
    numArray10[3] = (byte) 184;
    numArray10[10] = (byte) 228;
    numArray10[42] = (byte) 79;
    numArray10[11] = (byte) 132;
    numArray10[48 /*0x30*/] = (byte) 82;
    numArray10[33] = (byte) 215;
    numArray10[44] = (byte) 192 /*0xC0*/;
    numArray10[20] = (byte) 53;
    numArray10[21] = (byte) 186;
    numArray10[22] = (byte) 92;
    numArray10[34] = (byte) 248;
    numArray10[24] = (byte) 229;
    numArray10[25] = (byte) 57;
    numArray10[8] = (byte) 108;
    numArray10[4] = (byte) 216;
    numArray10[28] = (byte) 140;
    numArray10[17] = (byte) 12;
    numArray10[30] = (byte) 188;
    numArray10[31 /*0x1F*/] = (byte) 98;
    numArray10[32 /*0x20*/] = (byte) 136;
    numArray10[38] = (byte) 140;
    numArray10[15] = (byte) 111;
    numArray10[47] = (byte) 228;
    numArray10[26] = (byte) 76;
    numArray10[36] = (byte) 6;
    numArray10[50] = (byte) 178;
    numArray10[35] = (byte) 136;
    numArray10[40] = (byte) 6;
    numArray10[41] = (byte) 244;
    numArray10[29] = (byte) 70;
    numArray10[51] = (byte) 247;
    numArray10[18] = (byte) 181;
    numArray10[45] = (byte) 239;
    numArray10[19] = (byte) 126;
    numArray10[39] = (byte) 181;
    numArray10[37] = (byte) 236;
    numArray10[49] = (byte) 232;
    numArray10[13] = (byte) 201;
    numArray10[43] = (byte) 147;
    numArray10[52] = (byte) 2;
    numArray10[53] = (byte) 92;
    numArray10[16 /*0x10*/] = (byte) 251;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[32 /*0x20*/] = (byte) 189;
    numArray11[13] = (byte) 185;
    numArray11[22] = (byte) 194;
    numArray11[46] = (byte) 183;
    numArray11[43] = (byte) 52;
    numArray11[18] = (byte) 57;
    numArray11[27] = (byte) 6;
    numArray11[52] = (byte) 254;
    numArray11[8] = (byte) 28;
    numArray11[9] = (byte) 123;
    numArray11[7] = (byte) 7;
    numArray11[14] = (byte) 9;
    numArray11[12] = (byte) 185;
    numArray11[53] = (byte) 151;
    numArray11[30] = (byte) 205;
    numArray11[15] = (byte) 201;
    numArray11[16 /*0x10*/] = (byte) 131;
    numArray11[41] = (byte) 44;
    numArray11[51] = (byte) 140;
    numArray11[19] = (byte) 109;
    numArray11[20] = (byte) 231;
    numArray11[21] = (byte) 76;
    numArray11[0] = (byte) 90;
    numArray11[23] = (byte) 76;
    numArray11[2] = (byte) 76;
    numArray11[31 /*0x1F*/] = (byte) 131;
    numArray11[24] = (byte) 131;
    numArray11[36] = (byte) 41;
    numArray11[28] = (byte) 198;
    numArray11[1] = (byte) 3;
    numArray11[34] = (byte) 86;
    numArray11[5] = (byte) 182;
    numArray11[39] = (byte) 125;
    numArray11[33] = (byte) 39;
    numArray11[48 /*0x30*/] = (byte) 216;
    numArray11[35] = (byte) 179;
    numArray11[6] = (byte) 118;
    numArray11[3] = (byte) 62;
    numArray11[29] = (byte) 184;
    numArray11[4] = (byte) 146;
    numArray11[40] = (byte) 47;
    numArray11[26] = (byte) 166;
    numArray11[42] = (byte) 166;
    numArray11[25] = (byte) 215;
    numArray11[54] = (byte) 188;
    numArray11[45] = (byte) 172;
    numArray11[17] = (byte) 188;
    numArray11[11] = (byte) 126;
    numArray11[38] = (byte) 103;
    numArray11[49] = (byte) 7;
    numArray11[37] = (byte) 199;
    numArray11[47] = (byte) 203;
    numArray11[10] = (byte) 204;
    numArray11[44] = (byte) 174;
    numArray11[50] = (byte) 18;
    byte[] numArray12 = new byte[55]
    {
      (byte) 202,
      (byte) 228,
      (byte) 117,
      (byte) 99,
      (byte) 216,
      (byte) 182,
      (byte) 242,
      (byte) 98,
      (byte) 191,
      (byte) 254,
      (byte) 192 /*0xC0*/,
      (byte) 247,
      (byte) 77,
      (byte) 254,
      (byte) 112 /*0x70*/,
      (byte) 133,
      (byte) 62,
      (byte) 226,
      (byte) 154,
      (byte) 191,
      (byte) 150,
      (byte) 240 /*0xF0*/,
      (byte) 11,
      (byte) 143,
      (byte) 243,
      (byte) 126,
      (byte) 45,
      (byte) 103,
      (byte) 88,
      (byte) 145,
      (byte) 58,
      (byte) 199,
      (byte) 227,
      (byte) 191,
      (byte) 239,
      (byte) 141,
      (byte) 128 /*0x80*/,
      (byte) 248,
      (byte) 36,
      (byte) 74,
      (byte) 121,
      (byte) 95,
      (byte) 86,
      (byte) 116,
      (byte) 219,
      (byte) 135,
      (byte) 252,
      (byte) 103,
      (byte) 34,
      (byte) 151,
      (byte) 221,
      (byte) 42,
      (byte) 130,
      (byte) 212,
      (byte) 136
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[32 /*0x20*/]
    {
      (byte) 98,
      (byte) 227,
      (byte) 153,
      (byte) 53,
      (byte) 66,
      (byte) 130,
      (byte) 190,
      (byte) 108,
      (byte) 196,
      (byte) 155,
      (byte) 40,
      (byte) 26,
      (byte) 227,
      (byte) 118,
      (byte) 175,
      (byte) 166,
      (byte) 79,
      (byte) 127 /*0x7F*/,
      (byte) 207,
      (byte) 98,
      (byte) 7,
      (byte) 56,
      (byte) 235,
      (byte) 252,
      (byte) 113,
      (byte) 171,
      (byte) 87,
      (byte) 78,
      (byte) 9,
      (byte) 66,
      (byte) 204,
      (byte) 104
    };
    byte[] numArray14 = new byte[32 /*0x20*/]
    {
      (byte) 41,
      (byte) 26,
      (byte) 98,
      (byte) 216,
      (byte) 73,
      (byte) 74,
      (byte) 179,
      (byte) 226,
      (byte) 107,
      (byte) 144 /*0x90*/,
      (byte) 196,
      (byte) 70,
      (byte) 32 /*0x20*/,
      (byte) 72,
      (byte) 69,
      (byte) 35,
      (byte) 33,
      (byte) 67,
      (byte) 169,
      (byte) 120,
      (byte) 0,
      (byte) 71,
      (byte) 51,
      (byte) 187,
      (byte) 100,
      (byte) 154,
      (byte) 16 /*0x10*/,
      (byte) 93,
      (byte) 179,
      (byte) 27,
      (byte) 124,
      (byte) 59
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12660()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[177];
      byte[] numArray2 = new byte[55];
      numArray2[45] = (byte) 102;
      numArray2[30] = (byte) 59;
      numArray2[16 /*0x10*/] = (byte) 49;
      numArray2[51] = (byte) 215;
      numArray2[7] = (byte) 79;
      numArray2[39] = (byte) 238;
      numArray2[12] = (byte) 167;
      numArray2[29] = (byte) 191;
      numArray2[8] = (byte) 252;
      numArray2[9] = (byte) 79;
      numArray2[10] = (byte) 159;
      numArray2[19] = (byte) 41;
      numArray2[11] = (byte) 241;
      numArray2[13] = (byte) 145;
      numArray2[14] = (byte) 243;
      numArray2[15] = (byte) 218;
      numArray2[37] = (byte) 103;
      numArray2[4] = (byte) 132;
      numArray2[18] = (byte) 170;
      numArray2[5] = (byte) 25;
      numArray2[40] = (byte) 152;
      numArray2[21] = (byte) 157;
      numArray2[22] = (byte) 196;
      numArray2[23] = (byte) 150;
      numArray2[0] = (byte) 19;
      numArray2[25] = (byte) 111;
      numArray2[26] = (byte) 98;
      numArray2[54] = (byte) 48 /*0x30*/;
      numArray2[28] = (byte) 207;
      numArray2[17] = (byte) 7;
      numArray2[31 /*0x1F*/] = (byte) 34;
      numArray2[38] = (byte) 221;
      numArray2[32 /*0x20*/] = (byte) 174;
      numArray2[52] = (byte) 21;
      numArray2[34] = (byte) 146;
      numArray2[35] = (byte) 23;
      numArray2[50] = (byte) 54;
      numArray2[2] = (byte) 124;
      numArray2[48 /*0x30*/] = (byte) 78;
      numArray2[20] = (byte) 247;
      numArray2[36] = (byte) 2;
      numArray2[53] = (byte) 158;
      numArray2[42] = byte.MaxValue;
      numArray2[43] = (byte) 34;
      numArray2[44] = (byte) 12;
      numArray2[1] = (byte) 55;
      numArray2[46] = (byte) 167;
      numArray2[24] = (byte) 43;
      numArray2[33] = (byte) 134;
      numArray2[49] = (byte) 242;
      numArray2[27] = (byte) 130;
      numArray2[3] = (byte) 104;
      numArray2[47] = (byte) 120;
      numArray2[6] = (byte) 160 /*0xA0*/;
      numArray2[41] = (byte) 156;
      byte[] numArray3 = new byte[55]
      {
        (byte) 221,
        (byte) 161,
        (byte) 26,
        (byte) 41,
        (byte) 197,
        (byte) 110,
        (byte) 168,
        (byte) 243,
        (byte) 74,
        (byte) 138,
        (byte) 83,
        (byte) 143,
        (byte) 221,
        (byte) 191,
        (byte) 127 /*0x7F*/,
        (byte) 58,
        (byte) 252,
        (byte) 88,
        (byte) 2,
        (byte) 242,
        (byte) 164,
        (byte) 117,
        (byte) 23,
        (byte) 248,
        (byte) 20,
        (byte) 174,
        (byte) 24,
        (byte) 239,
        (byte) 246,
        (byte) 174,
        (byte) 50,
        (byte) 192 /*0xC0*/,
        (byte) 135,
        (byte) 210,
        (byte) 160 /*0xA0*/,
        (byte) 224 /*0xE0*/,
        (byte) 67,
        (byte) 165,
        (byte) 136,
        (byte) 152,
        (byte) 186,
        (byte) 213,
        (byte) 201,
        (byte) 136,
        (byte) 128 /*0x80*/,
        (byte) 129,
        (byte) 102,
        (byte) 40,
        (byte) 192 /*0xC0*/,
        (byte) 149,
        (byte) 182,
        (byte) 155,
        (byte) 212,
        (byte) 195,
        (byte) 89
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 92,
        (byte) 119,
        (byte) 77,
        (byte) 204,
        (byte) 234,
        (byte) 163,
        (byte) 179,
        (byte) 174,
        (byte) 111,
        (byte) 51,
        (byte) 202,
        (byte) 225,
        (byte) 56,
        (byte) 186,
        (byte) 159,
        (byte) 97,
        (byte) 133,
        (byte) 23,
        (byte) 63 /*0x3F*/,
        (byte) 218,
        (byte) 147,
        (byte) 249,
        (byte) 162,
        (byte) 64 /*0x40*/,
        (byte) 112 /*0x70*/,
        (byte) 193,
        (byte) 194,
        (byte) 28,
        (byte) 183,
        (byte) 180,
        (byte) 2,
        (byte) 129,
        (byte) 67,
        (byte) 210,
        (byte) 192 /*0xC0*/,
        (byte) 178,
        (byte) 122,
        (byte) 106,
        (byte) 142,
        (byte) 120,
        (byte) 15,
        (byte) 96 /*0x60*/,
        (byte) 33,
        (byte) 240 /*0xF0*/,
        (byte) 188,
        (byte) 152,
        (byte) 58,
        (byte) 96 /*0x60*/,
        (byte) 204,
        (byte) 75,
        (byte) 211,
        (byte) 246,
        (byte) 149,
        (byte) 254,
        (byte) 62
      };
      byte[] numArray5 = new byte[55];
      numArray5[38] = (byte) 83;
      numArray5[1] = (byte) 116;
      numArray5[39] = (byte) 26;
      numArray5[10] = (byte) 127 /*0x7F*/;
      numArray5[24] = (byte) 212;
      numArray5[51] = (byte) 64 /*0x40*/;
      numArray5[29] = (byte) 12;
      numArray5[7] = (byte) 80 /*0x50*/;
      numArray5[41] = (byte) 8;
      numArray5[9] = (byte) 63 /*0x3F*/;
      numArray5[43] = (byte) 113;
      numArray5[11] = (byte) 88;
      numArray5[12] = (byte) 108;
      numArray5[36] = (byte) 129;
      numArray5[17] = (byte) 76;
      numArray5[40] = (byte) 177;
      numArray5[23] = (byte) 167;
      numArray5[54] = (byte) 225;
      numArray5[16 /*0x10*/] = (byte) 82;
      numArray5[30] = (byte) 81;
      numArray5[4] = (byte) 230;
      numArray5[21] = (byte) 99;
      numArray5[22] = (byte) 11;
      numArray5[53] = (byte) 179;
      numArray5[18] = (byte) 149;
      numArray5[25] = (byte) 204;
      numArray5[26] = (byte) 32 /*0x20*/;
      numArray5[3] = (byte) 48 /*0x30*/;
      numArray5[28] = (byte) 33;
      numArray5[35] = (byte) 238;
      numArray5[33] = (byte) 127 /*0x7F*/;
      numArray5[31 /*0x1F*/] = (byte) 75;
      numArray5[2] = (byte) 144 /*0x90*/;
      numArray5[13] = (byte) 116;
      numArray5[34] = (byte) 109;
      numArray5[32 /*0x20*/] = (byte) 68;
      numArray5[14] = (byte) 216;
      numArray5[37] = (byte) 102;
      numArray5[0] = (byte) 98;
      numArray5[15] = (byte) 132;
      numArray5[49] = (byte) 19;
      numArray5[5] = (byte) 118;
      numArray5[42] = (byte) 102;
      numArray5[48 /*0x30*/] = (byte) 133;
      numArray5[44] = (byte) 224 /*0xE0*/;
      numArray5[8] = (byte) 38;
      numArray5[46] = (byte) 61;
      numArray5[47] = (byte) 223;
      numArray5[6] = (byte) 171;
      numArray5[20] = (byte) 243;
      numArray5[50] = (byte) 11;
      numArray5[45] = (byte) 19;
      numArray5[52] = (byte) 44;
      numArray5[27] = (byte) 197;
      numArray5[19] = (byte) 188;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 135,
        (byte) 133,
        (byte) 162,
        (byte) 127 /*0x7F*/,
        (byte) 70,
        (byte) 137,
        (byte) 218,
        (byte) 36,
        (byte) 93,
        (byte) 89,
        (byte) 209,
        (byte) 155,
        (byte) 16 /*0x10*/,
        (byte) 121,
        (byte) 13,
        (byte) 35,
        (byte) 47,
        (byte) 82,
        (byte) 199,
        (byte) 113,
        (byte) 242,
        (byte) 18,
        (byte) 221,
        (byte) 178,
        (byte) 250,
        (byte) 112 /*0x70*/,
        (byte) 8,
        (byte) 40,
        (byte) 158,
        (byte) 29,
        (byte) 17,
        (byte) 135,
        (byte) 210,
        (byte) 185,
        (byte) 229,
        (byte) 136,
        (byte) 194,
        (byte) 23,
        (byte) 50,
        (byte) 225,
        (byte) 139,
        (byte) 154,
        (byte) 20,
        (byte) 160 /*0xA0*/,
        (byte) 3,
        (byte) 203,
        (byte) 207,
        (byte) 234,
        (byte) 170,
        (byte) 73,
        (byte) 158,
        (byte) 225,
        (byte) 68,
        (byte) 25,
        (byte) 48 /*0x30*/
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 224 /*0xE0*/,
        (byte) 118,
        (byte) 84,
        (byte) 149,
        (byte) 72,
        (byte) 87,
        (byte) 30,
        (byte) 182,
        (byte) 53,
        (byte) 233,
        (byte) 160 /*0xA0*/,
        (byte) 120,
        (byte) 131,
        (byte) 241,
        (byte) 212,
        (byte) 108,
        (byte) 17,
        (byte) 157,
        (byte) 222,
        (byte) 235,
        (byte) 101,
        (byte) 201,
        (byte) 143,
        (byte) 96 /*0x60*/,
        (byte) 45,
        (byte) 98,
        (byte) 209,
        (byte) 32 /*0x20*/,
        (byte) 7,
        (byte) 140,
        (byte) 215,
        (byte) 240 /*0xF0*/,
        (byte) 65,
        (byte) 153,
        (byte) 44,
        (byte) 117,
        (byte) 136,
        (byte) 108,
        (byte) 249,
        (byte) 247,
        (byte) 141,
        (byte) 54,
        (byte) 192 /*0xC0*/,
        (byte) 132,
        (byte) 136,
        (byte) 209,
        (byte) 108,
        (byte) 87,
        (byte) 252,
        (byte) 211,
        (byte) 28,
        (byte) 83,
        (byte) 206,
        (byte) 237,
        (byte) 205
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[12]
      {
        (byte) 240 /*0xF0*/,
        (byte) 133,
        (byte) 213,
        (byte) 128 /*0x80*/,
        (byte) 177,
        (byte) 134,
        (byte) 63 /*0x3F*/,
        (byte) 72,
        (byte) 73,
        (byte) 201,
        (byte) 86,
        (byte) 147
      };
      byte[] numArray9 = new byte[12]
      {
        (byte) 141,
        (byte) 97,
        (byte) 154,
        (byte) 97,
        (byte) 189,
        (byte) 52,
        (byte) 164,
        (byte) 213,
        (byte) 45,
        (byte) 140,
        (byte) 235,
        (byte) 32 /*0x20*/
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[177];
    byte[] numArray11 = new byte[55];
    numArray11[51] = (byte) 35;
    numArray11[14] = (byte) 67;
    numArray11[44] = (byte) 51;
    numArray11[33] = (byte) 133;
    numArray11[16 /*0x10*/] = (byte) 136;
    numArray11[37] = (byte) 216;
    numArray11[48 /*0x30*/] = (byte) 5;
    numArray11[7] = (byte) 46;
    numArray11[19] = (byte) 210;
    numArray11[49] = (byte) 16 /*0x10*/;
    numArray11[35] = (byte) 94;
    numArray11[2] = (byte) 229;
    numArray11[15] = (byte) 184;
    numArray11[13] = (byte) 48 /*0x30*/;
    numArray11[27] = (byte) 186;
    numArray11[3] = (byte) 88;
    numArray11[46] = (byte) 192 /*0xC0*/;
    numArray11[17] = (byte) 112 /*0x70*/;
    numArray11[18] = (byte) 225;
    numArray11[54] = (byte) 134;
    numArray11[20] = (byte) 145;
    numArray11[1] = (byte) 230;
    numArray11[22] = (byte) 96 /*0x60*/;
    numArray11[42] = (byte) 52;
    numArray11[24] = (byte) 20;
    numArray11[32 /*0x20*/] = (byte) 232;
    numArray11[26] = (byte) 87;
    numArray11[10] = (byte) 126;
    numArray11[28] = (byte) 2;
    numArray11[29] = (byte) 120;
    numArray11[30] = (byte) 86;
    numArray11[21] = (byte) 92;
    numArray11[8] = (byte) 161;
    numArray11[39] = (byte) 105;
    numArray11[34] = (byte) 193;
    numArray11[4] = (byte) 253;
    numArray11[36] = (byte) 52;
    numArray11[52] = (byte) 164;
    numArray11[38] = (byte) 119;
    numArray11[5] = (byte) 61;
    numArray11[40] = (byte) 66;
    numArray11[6] = (byte) 174;
    numArray11[11] = (byte) 84;
    numArray11[43] = (byte) 98;
    numArray11[45] = (byte) 49;
    numArray11[0] = (byte) 31 /*0x1F*/;
    numArray11[23] = (byte) 217;
    numArray11[47] = (byte) 24;
    numArray11[25] = (byte) 212;
    numArray11[31 /*0x1F*/] = (byte) 37;
    numArray11[50] = (byte) 206;
    numArray11[12] = (byte) 127 /*0x7F*/;
    numArray11[41] = (byte) 87;
    numArray11[53] = (byte) 143;
    numArray11[9] = (byte) 30;
    byte[] numArray12 = new byte[55]
    {
      (byte) 10,
      (byte) 26,
      (byte) 22,
      (byte) 121,
      (byte) 176 /*0xB0*/,
      (byte) 54,
      (byte) 69,
      (byte) 236,
      (byte) 57,
      (byte) 239,
      (byte) 73,
      (byte) 9,
      (byte) 63 /*0x3F*/,
      (byte) 246,
      (byte) 53,
      (byte) 1,
      (byte) 49,
      (byte) 19,
      (byte) 254,
      (byte) 113,
      (byte) 91,
      (byte) 26,
      (byte) 244,
      (byte) 113,
      (byte) 93,
      (byte) 189,
      (byte) 116,
      (byte) 88,
      (byte) 19,
      (byte) 98,
      (byte) 134,
      (byte) 15,
      (byte) 105,
      (byte) 133,
      (byte) 30,
      (byte) 91,
      (byte) 93,
      (byte) 145,
      (byte) 162,
      (byte) 101,
      (byte) 203,
      (byte) 222,
      (byte) 216,
      (byte) 193,
      (byte) 49,
      (byte) 51,
      (byte) 143,
      (byte) 161,
      (byte) 205,
      byte.MaxValue,
      (byte) 22,
      (byte) 100,
      (byte) 126,
      (byte) 48 /*0x30*/,
      byte.MaxValue
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[11] = (byte) 111;
    numArray13[23] = (byte) 78;
    numArray13[2] = (byte) 20;
    numArray13[3] = (byte) 167;
    numArray13[31 /*0x1F*/] = (byte) 178;
    numArray13[46] = (byte) 21;
    numArray13[33] = (byte) 79;
    numArray13[7] = (byte) 147;
    numArray13[8] = (byte) 215;
    numArray13[9] = (byte) 18;
    numArray13[10] = (byte) 251;
    numArray13[41] = (byte) 7;
    numArray13[44] = (byte) 202;
    numArray13[0] = (byte) 101;
    numArray13[14] = (byte) 27;
    numArray13[12] = (byte) 12;
    numArray13[19] = (byte) 29;
    numArray13[17] = (byte) 64 /*0x40*/;
    numArray13[25] = (byte) 147;
    numArray13[51] = (byte) 190;
    numArray13[20] = (byte) 98;
    numArray13[21] = (byte) 203;
    numArray13[18] = (byte) 14;
    numArray13[36] = (byte) 58;
    numArray13[43] = (byte) 178;
    numArray13[4] = (byte) 62;
    numArray13[45] = (byte) 58;
    numArray13[16 /*0x10*/] = (byte) 243;
    numArray13[28] = (byte) 9;
    numArray13[29] = (byte) 84;
    numArray13[30] = (byte) 90;
    numArray13[42] = (byte) 3;
    numArray13[32 /*0x20*/] = (byte) 101;
    numArray13[49] = (byte) 127 /*0x7F*/;
    numArray13[34] = (byte) 194;
    numArray13[35] = (byte) 51;
    numArray13[40] = (byte) 140;
    numArray13[37] = (byte) 220;
    numArray13[38] = (byte) 16 /*0x10*/;
    numArray13[39] = (byte) 240 /*0xF0*/;
    numArray13[52] = (byte) 61;
    numArray13[5] = (byte) 58;
    numArray13[47] = (byte) 47;
    numArray13[13] = (byte) 92;
    numArray13[6] = (byte) 126;
    numArray13[22] = (byte) 29;
    numArray13[27] = (byte) 139;
    numArray13[1] = (byte) 96 /*0x60*/;
    numArray13[48 /*0x30*/] = (byte) 2;
    numArray13[53] = (byte) 124;
    numArray13[50] = (byte) 57;
    numArray13[54] = (byte) 38;
    numArray13[26] = (byte) 101;
    numArray13[24] = (byte) 148;
    numArray13[15] = (byte) 170;
    byte[] numArray14 = new byte[55]
    {
      (byte) 130,
      (byte) 158,
      (byte) 59,
      (byte) 170,
      (byte) 57,
      (byte) 39,
      (byte) 188,
      (byte) 87,
      (byte) 64 /*0x40*/,
      (byte) 2,
      (byte) 222,
      (byte) 215,
      (byte) 52,
      (byte) 118,
      (byte) 181,
      (byte) 134,
      (byte) 174,
      (byte) 101,
      (byte) 66,
      (byte) 50,
      (byte) 211,
      (byte) 51,
      (byte) 5,
      (byte) 227,
      (byte) 75,
      (byte) 254,
      (byte) 124,
      (byte) 58,
      (byte) 140,
      (byte) 179,
      (byte) 252,
      (byte) 157,
      (byte) 1,
      (byte) 84,
      (byte) 0,
      (byte) 167,
      (byte) 127 /*0x7F*/,
      (byte) 20,
      (byte) 91,
      (byte) 193,
      (byte) 49,
      (byte) 47,
      (byte) 232,
      (byte) 185,
      (byte) 149,
      (byte) 251,
      (byte) 52,
      (byte) 83,
      (byte) 93,
      (byte) 224 /*0xE0*/,
      (byte) 46,
      (byte) 37,
      (byte) 51,
      (byte) 95,
      (byte) 87
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 66,
      (byte) 183,
      (byte) 44,
      (byte) 160 /*0xA0*/,
      (byte) 106,
      (byte) 0,
      (byte) 104,
      (byte) 28,
      (byte) 212,
      (byte) 33,
      (byte) 146,
      (byte) 114,
      (byte) 247,
      (byte) 252,
      (byte) 75,
      (byte) 202,
      (byte) 3,
      (byte) 247,
      (byte) 222,
      (byte) 72,
      (byte) 113,
      (byte) 1,
      (byte) 41,
      (byte) 37,
      (byte) 159,
      (byte) 109,
      (byte) 122,
      (byte) 248,
      (byte) 13,
      (byte) 84,
      (byte) 252,
      (byte) 95,
      (byte) 85,
      (byte) 24,
      (byte) 50,
      (byte) 18,
      (byte) 56,
      (byte) 122,
      (byte) 47,
      (byte) 123,
      (byte) 23,
      (byte) 221,
      (byte) 173,
      (byte) 14,
      (byte) 198,
      (byte) 35,
      (byte) 35,
      (byte) 19,
      (byte) 214,
      (byte) 195,
      (byte) 27,
      (byte) 243,
      (byte) 236,
      (byte) 138,
      (byte) 103
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 141,
      (byte) 216,
      (byte) 243,
      (byte) 13,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 242,
      (byte) 61,
      (byte) 155,
      (byte) 81,
      (byte) 46,
      (byte) 158,
      (byte) 28,
      (byte) 214,
      (byte) 66,
      (byte) 247,
      (byte) 127 /*0x7F*/,
      (byte) 14,
      (byte) 145,
      (byte) 165,
      (byte) 246,
      (byte) 239,
      (byte) 48 /*0x30*/,
      (byte) 90,
      (byte) 168,
      (byte) 132,
      (byte) 53,
      (byte) 16 /*0x10*/,
      (byte) 200,
      (byte) 124,
      (byte) 120,
      (byte) 27,
      (byte) 49,
      (byte) 155,
      (byte) 223,
      (byte) 191,
      (byte) 24,
      (byte) 62,
      (byte) 1,
      (byte) 87,
      (byte) 220,
      (byte) 83,
      (byte) 214,
      (byte) 245,
      (byte) 40,
      (byte) 58,
      (byte) 44,
      (byte) 89,
      (byte) 199,
      (byte) 57,
      (byte) 214,
      (byte) 17,
      (byte) 107,
      (byte) 48 /*0x30*/,
      (byte) 244
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[12]
    {
      (byte) 7,
      (byte) 0,
      (byte) 252,
      (byte) 134,
      (byte) 112 /*0x70*/,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 36,
      (byte) 51,
      (byte) 29,
      (byte) 223,
      (byte) 202
    };
    byte[] numArray18 = new byte[12];
    numArray18[2] = (byte) 184;
    numArray18[1] = (byte) 218;
    numArray18[11] = (byte) 15;
    numArray18[3] = (byte) 213;
    numArray18[10] = (byte) 166;
    numArray18[5] = (byte) 253;
    numArray18[9] = (byte) 175;
    numArray18[7] = (byte) 116;
    numArray18[8] = (byte) 142;
    numArray18[4] = (byte) 73;
    numArray18[6] = (byte) 86;
    numArray18[0] = (byte) 140;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 12);
    for (int index = 0; index < 12; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_12661(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[17] = (byte) 157;
    sourceArray1[18] = (byte) 50;
    sourceArray1[24] = (byte) 235;
    sourceArray1[3] = (byte) 241;
    sourceArray1[4] = (byte) 132;
    sourceArray1[0] = (byte) 114;
    sourceArray1[6] = (byte) 121;
    sourceArray1[7] = (byte) 51;
    sourceArray1[8] = (byte) 245;
    sourceArray1[9] = (byte) 167;
    sourceArray1[25] = (byte) 114;
    sourceArray1[11] = (byte) 45;
    sourceArray1[34] = (byte) 192 /*0xC0*/;
    sourceArray1[13] = (byte) 146;
    sourceArray1[33] = (byte) 27;
    sourceArray1[15] = (byte) 68;
    sourceArray1[37] = (byte) 216;
    sourceArray1[36] = (byte) 45;
    sourceArray1[19] = (byte) 146;
    sourceArray1[22] = (byte) 89;
    sourceArray1[20] = (byte) 43;
    sourceArray1[38] = (byte) 180;
    sourceArray1[12] = (byte) 233;
    sourceArray1[23] = (byte) 211;
    sourceArray1[14] = (byte) 6;
    sourceArray1[1] = (byte) 202;
    sourceArray1[5] = (byte) 49;
    sourceArray1[27] = (byte) 133;
    sourceArray1[2] = (byte) 177;
    sourceArray1[29] = (byte) 31 /*0x1F*/;
    sourceArray1[30] = (byte) 79;
    sourceArray1[45] = (byte) 39;
    sourceArray1[32 /*0x20*/] = (byte) 70;
    sourceArray1[16 /*0x10*/] = (byte) 73;
    sourceArray1[31 /*0x1F*/] = (byte) 198;
    sourceArray1[35] = (byte) 82;
    sourceArray1[28] = (byte) 177;
    sourceArray1[47] = (byte) 204;
    sourceArray1[21] = (byte) 137;
    sourceArray1[39] = (byte) 96 /*0x60*/;
    sourceArray1[40] = (byte) 15;
    sourceArray1[41] = (byte) 149;
    sourceArray1[26] = (byte) 168;
    sourceArray1[43] = (byte) 137;
    sourceArray1[44] = (byte) 198;
    sourceArray1[10] = (byte) 205;
    sourceArray1[46] = (byte) 49;
    sourceArray1[42] = (byte) 109;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 160 /*0xA0*/,
      (byte) 21,
      (byte) 169,
      (byte) 77,
      (byte) 243,
      (byte) 108,
      (byte) 212,
      (byte) 33,
      (byte) 110,
      (byte) 18,
      (byte) 227,
      (byte) 26,
      (byte) 80 /*0x50*/,
      (byte) 108,
      (byte) 54,
      (byte) 136,
      (byte) 79,
      (byte) 251,
      (byte) 66,
      (byte) 239,
      (byte) 244,
      (byte) 27,
      (byte) 17,
      (byte) 233,
      (byte) 181,
      (byte) 176 /*0xB0*/,
      (byte) 204,
      (byte) 209,
      (byte) 177,
      (byte) 32 /*0x20*/,
      (byte) 48 /*0x30*/,
      (byte) 75,
      (byte) 56,
      (byte) 57,
      (byte) 253,
      (byte) 63 /*0x3F*/,
      (byte) 196,
      (byte) 224 /*0xE0*/,
      (byte) 126,
      (byte) 232,
      (byte) 79,
      (byte) 38,
      (byte) 206,
      (byte) 186,
      (byte) 220,
      (byte) 103,
      (byte) 115,
      (byte) 178
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12662()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[86];
      byte[] numArray2 = new byte[55];
      numArray2[25] = (byte) 198;
      numArray2[1] = (byte) 100;
      numArray2[38] = (byte) 188;
      numArray2[39] = (byte) 85;
      numArray2[42] = (byte) 125;
      numArray2[5] = (byte) 172;
      numArray2[17] = (byte) 213;
      numArray2[16 /*0x10*/] = (byte) 229;
      numArray2[8] = (byte) 34;
      numArray2[49] = (byte) 40;
      numArray2[6] = (byte) 154;
      numArray2[11] = (byte) 160 /*0xA0*/;
      numArray2[12] = (byte) 208 /*0xD0*/;
      numArray2[34] = (byte) 132;
      numArray2[14] = (byte) 209;
      numArray2[30] = (byte) 130;
      numArray2[47] = (byte) 143;
      numArray2[29] = (byte) 104;
      numArray2[3] = (byte) 194;
      numArray2[7] = (byte) 69;
      numArray2[52] = (byte) 197;
      numArray2[19] = (byte) 140;
      numArray2[22] = (byte) 143;
      numArray2[37] = (byte) 77;
      numArray2[24] = (byte) 209;
      numArray2[21] = (byte) 70;
      numArray2[28] = (byte) 250;
      numArray2[27] = (byte) 70;
      numArray2[44] = (byte) 10;
      numArray2[46] = (byte) 127 /*0x7F*/;
      numArray2[23] = (byte) 250;
      numArray2[31 /*0x1F*/] = (byte) 131;
      numArray2[32 /*0x20*/] = (byte) 158;
      numArray2[33] = (byte) 77;
      numArray2[40] = (byte) 49;
      numArray2[35] = (byte) 220;
      numArray2[36] = (byte) 224 /*0xE0*/;
      numArray2[48 /*0x30*/] = (byte) 209;
      numArray2[18] = (byte) 211;
      numArray2[20] = (byte) 87;
      numArray2[0] = (byte) 64 /*0x40*/;
      numArray2[41] = (byte) 106;
      numArray2[13] = (byte) 65;
      numArray2[43] = (byte) 247;
      numArray2[15] = (byte) 174;
      numArray2[45] = (byte) 65;
      numArray2[10] = (byte) 205;
      numArray2[4] = (byte) 239;
      numArray2[2] = (byte) 251;
      numArray2[26] = (byte) 159;
      numArray2[50] = (byte) 53;
      numArray2[51] = (byte) 62;
      numArray2[53] = (byte) 165;
      numArray2[9] = (byte) 71;
      numArray2[54] = (byte) 103;
      byte[] numArray3 = new byte[55]
      {
        (byte) 44,
        (byte) 30,
        (byte) 134,
        (byte) 208 /*0xD0*/,
        (byte) 194,
        (byte) 48 /*0x30*/,
        (byte) 32 /*0x20*/,
        (byte) 154,
        (byte) 56,
        (byte) 196,
        (byte) 159,
        (byte) 120,
        (byte) 226,
        (byte) 67,
        (byte) 47,
        (byte) 221,
        (byte) 66,
        (byte) 53,
        (byte) 0,
        (byte) 192 /*0xC0*/,
        (byte) 0,
        (byte) 215,
        (byte) 99,
        (byte) 239,
        (byte) 224 /*0xE0*/,
        (byte) 55,
        (byte) 185,
        (byte) 145,
        (byte) 85,
        (byte) 179,
        (byte) 30,
        (byte) 7,
        (byte) 189,
        (byte) 169,
        (byte) 57,
        (byte) 61,
        (byte) 140,
        (byte) 13,
        (byte) 15,
        (byte) 144 /*0x90*/,
        (byte) 117,
        (byte) 56,
        (byte) 218,
        (byte) 22,
        (byte) 154,
        (byte) 117,
        (byte) 103,
        (byte) 51,
        (byte) 126,
        (byte) 171,
        (byte) 229,
        (byte) 1,
        (byte) 59,
        (byte) 165,
        (byte) 99
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/];
      numArray4[12] = (byte) 236;
      numArray4[1] = (byte) 97;
      numArray4[20] = (byte) 102;
      numArray4[15] = (byte) 132;
      numArray4[4] = (byte) 222;
      numArray4[0] = (byte) 17;
      numArray4[6] = (byte) 135;
      numArray4[7] = (byte) 130;
      numArray4[26] = (byte) 119;
      numArray4[9] = (byte) 9;
      numArray4[10] = (byte) 156;
      numArray4[21] = (byte) 47;
      numArray4[29] = (byte) 118;
      numArray4[3] = (byte) 253;
      numArray4[16 /*0x10*/] = (byte) 52;
      numArray4[30] = (byte) 174;
      numArray4[11] = (byte) 51;
      numArray4[17] = (byte) 124;
      numArray4[18] = (byte) 205;
      numArray4[19] = (byte) 5;
      numArray4[8] = (byte) 139;
      numArray4[14] = (byte) 60;
      numArray4[22] = (byte) 79;
      numArray4[5] = (byte) 120;
      numArray4[24] = (byte) 61;
      numArray4[23] = (byte) 74;
      numArray4[2] = (byte) 225;
      numArray4[27] = (byte) 149;
      numArray4[28] = (byte) 28;
      numArray4[13] = (byte) 239;
      numArray4[25] = (byte) 152;
      byte[] numArray5 = new byte[31 /*0x1F*/]
      {
        (byte) 26,
        (byte) 91,
        (byte) 230,
        (byte) 100,
        (byte) 104,
        (byte) 131,
        (byte) 1,
        (byte) 174,
        (byte) 87,
        (byte) 12,
        (byte) 94,
        (byte) 13,
        (byte) 13,
        (byte) 226,
        (byte) 76,
        (byte) 119,
        (byte) 112 /*0x70*/,
        (byte) 31 /*0x1F*/,
        (byte) 161,
        (byte) 38,
        (byte) 151,
        (byte) 71,
        (byte) 194,
        (byte) 71,
        (byte) 248,
        (byte) 210,
        (byte) 193,
        (byte) 174,
        (byte) 29,
        (byte) 166,
        (byte) 183
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
      (byte) 49,
      (byte) 217,
      (byte) 32 /*0x20*/,
      (byte) 32 /*0x20*/,
      (byte) 66,
      (byte) 210,
      (byte) 233,
      (byte) 125,
      (byte) 38,
      (byte) 201,
      (byte) 182,
      (byte) 250,
      (byte) 25,
      (byte) 234,
      (byte) 157,
      (byte) 167,
      (byte) 11,
      (byte) 52,
      (byte) 122,
      (byte) 99,
      (byte) 224 /*0xE0*/,
      (byte) 190,
      (byte) 45,
      (byte) 245,
      (byte) 236,
      (byte) 231,
      (byte) 26,
      (byte) 138,
      (byte) 186,
      (byte) 165,
      (byte) 74,
      (byte) 74,
      (byte) 31 /*0x1F*/,
      (byte) 219,
      (byte) 46,
      (byte) 197,
      (byte) 208 /*0xD0*/,
      (byte) 74,
      (byte) 30,
      (byte) 208 /*0xD0*/,
      (byte) 40,
      (byte) 80 /*0x50*/,
      (byte) 133,
      (byte) 97,
      (byte) 31 /*0x1F*/,
      (byte) 230,
      (byte) 248,
      (byte) 172,
      (byte) 34,
      (byte) 157,
      (byte) 245,
      (byte) 189,
      (byte) 234,
      (byte) 177,
      (byte) 122
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 245,
      (byte) 197,
      (byte) 46,
      (byte) 97,
      (byte) 102,
      (byte) 154,
      (byte) 233,
      (byte) 114,
      (byte) 110,
      (byte) 16 /*0x10*/,
      (byte) 247,
      (byte) 8,
      (byte) 23,
      (byte) 70,
      (byte) 115,
      (byte) 171,
      (byte) 249,
      (byte) 181,
      (byte) 95,
      (byte) 210,
      (byte) 184,
      (byte) 18,
      (byte) 253,
      (byte) 32 /*0x20*/,
      (byte) 159,
      (byte) 81,
      (byte) 7,
      (byte) 122,
      (byte) 26,
      (byte) 38,
      (byte) 65,
      (byte) 57,
      (byte) 164,
      (byte) 135,
      (byte) 33,
      (byte) 191,
      (byte) 182,
      (byte) 243,
      (byte) 78,
      (byte) 144 /*0x90*/,
      (byte) 200,
      (byte) 224 /*0xE0*/,
      (byte) 77,
      (byte) 146,
      (byte) 164,
      (byte) 7,
      (byte) 142,
      (byte) 47,
      (byte) 109,
      (byte) 153,
      (byte) 223,
      (byte) 224 /*0xE0*/,
      (byte) 142,
      (byte) 70,
      (byte) 80 /*0x50*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[31 /*0x1F*/]
    {
      (byte) 42,
      (byte) 52,
      (byte) 91,
      (byte) 183,
      (byte) 95,
      (byte) 194,
      (byte) 129,
      (byte) 127 /*0x7F*/,
      (byte) 108,
      (byte) 219,
      (byte) 62,
      (byte) 187,
      (byte) 216,
      (byte) 215,
      (byte) 192 /*0xC0*/,
      (byte) 22,
      (byte) 170,
      (byte) 58,
      (byte) 143,
      (byte) 57,
      (byte) 204,
      (byte) 214,
      (byte) 5,
      (byte) 239,
      (byte) 109,
      (byte) 104,
      (byte) 28,
      (byte) 175,
      (byte) 141,
      (byte) 95,
      (byte) 109
    };
    byte[] numArray10 = new byte[31 /*0x1F*/]
    {
      (byte) 17,
      (byte) 141,
      (byte) 130,
      (byte) 8,
      (byte) 214,
      (byte) 124,
      (byte) 20,
      (byte) 115,
      (byte) 91,
      (byte) 77,
      (byte) 110,
      (byte) 204,
      (byte) 30,
      (byte) 103,
      (byte) 41,
      (byte) 254,
      (byte) 187,
      (byte) 160 /*0xA0*/,
      (byte) 38,
      (byte) 126,
      (byte) 50,
      (byte) 38,
      (byte) 92,
      (byte) 78,
      (byte) 119,
      (byte) 27,
      (byte) 164,
      (byte) 218,
      (byte) 217,
      (byte) 211,
      (byte) 4
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12663()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[54];
      byte[] numArray2 = new byte[54];
      numArray2[4] = (byte) 141;
      numArray2[1] = (byte) 195;
      numArray2[32 /*0x20*/] = (byte) 191;
      numArray2[35] = (byte) 83;
      numArray2[19] = (byte) 223;
      numArray2[3] = (byte) 201;
      numArray2[24] = (byte) 247;
      numArray2[7] = (byte) 178;
      numArray2[23] = (byte) 73;
      numArray2[9] = (byte) 186;
      numArray2[10] = (byte) 241;
      numArray2[41] = (byte) 35;
      numArray2[11] = (byte) 73;
      numArray2[47] = (byte) 122;
      numArray2[17] = (byte) 234;
      numArray2[15] = (byte) 64 /*0x40*/;
      numArray2[43] = (byte) 52;
      numArray2[21] = (byte) 46;
      numArray2[18] = (byte) 57;
      numArray2[52] = (byte) 131;
      numArray2[25] = (byte) 166;
      numArray2[39] = (byte) 52;
      numArray2[2] = (byte) 24;
      numArray2[5] = (byte) 4;
      numArray2[38] = (byte) 2;
      numArray2[28] = (byte) 13;
      numArray2[14] = (byte) 166;
      numArray2[6] = (byte) 226;
      numArray2[8] = (byte) 11;
      numArray2[29] = (byte) 109;
      numArray2[30] = (byte) 221;
      numArray2[26] = (byte) 209;
      numArray2[37] = (byte) 112 /*0x70*/;
      numArray2[33] = (byte) 50;
      numArray2[34] = (byte) 170;
      numArray2[31 /*0x1F*/] = byte.MaxValue;
      numArray2[36] = (byte) 145;
      numArray2[22] = (byte) 108;
      numArray2[13] = (byte) 53;
      numArray2[45] = (byte) 30;
      numArray2[40] = (byte) 233;
      numArray2[12] = (byte) 198;
      numArray2[42] = (byte) 250;
      numArray2[16 /*0x10*/] = (byte) 119;
      numArray2[44] = (byte) 124;
      numArray2[0] = (byte) 22;
      numArray2[46] = (byte) 86;
      numArray2[48 /*0x30*/] = (byte) 230;
      numArray2[27] = (byte) 242;
      numArray2[49] = (byte) 30;
      numArray2[50] = (byte) 246;
      numArray2[51] = (byte) 117;
      numArray2[53] = (byte) 57;
      numArray2[20] = (byte) 84;
      byte[] numArray3 = new byte[54]
      {
        (byte) 28,
        (byte) 66,
        (byte) 165,
        (byte) 20,
        (byte) 205,
        (byte) 216,
        (byte) 101,
        (byte) 111,
        (byte) 4,
        (byte) 80 /*0x50*/,
        (byte) 191,
        (byte) 138,
        (byte) 234,
        (byte) 127 /*0x7F*/,
        (byte) 179,
        (byte) 149,
        (byte) 134,
        (byte) 85,
        (byte) 17,
        (byte) 61,
        (byte) 243,
        (byte) 25,
        (byte) 68,
        (byte) 220,
        (byte) 151,
        (byte) 73,
        (byte) 145,
        (byte) 152,
        (byte) 120,
        (byte) 112 /*0x70*/,
        (byte) 194,
        (byte) 97,
        (byte) 209,
        (byte) 174,
        (byte) 96 /*0x60*/,
        (byte) 154,
        (byte) 216,
        (byte) 53,
        (byte) 161,
        (byte) 63 /*0x3F*/,
        (byte) 52,
        (byte) 242,
        (byte) 58,
        (byte) 199,
        (byte) 170,
        (byte) 162,
        (byte) 201,
        (byte) 61,
        (byte) 22,
        (byte) 66,
        (byte) 159,
        (byte) 14,
        (byte) 4,
        (byte) 11
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[54];
    byte[] numArray5 = new byte[54];
    numArray5[33] = (byte) 15;
    numArray5[4] = (byte) 219;
    numArray5[2] = (byte) 249;
    numArray5[20] = (byte) 216;
    numArray5[21] = (byte) 222;
    numArray5[44] = (byte) 144 /*0x90*/;
    numArray5[8] = (byte) 115;
    numArray5[6] = (byte) 183;
    numArray5[3] = (byte) 151;
    numArray5[43] = (byte) 21;
    numArray5[10] = (byte) 3;
    numArray5[11] = (byte) 178;
    numArray5[47] = (byte) 59;
    numArray5[13] = (byte) 87;
    numArray5[14] = (byte) 237;
    numArray5[35] = (byte) 190;
    numArray5[40] = (byte) 198;
    numArray5[18] = (byte) 252;
    numArray5[12] = (byte) 200;
    numArray5[32 /*0x20*/] = (byte) 193;
    numArray5[24] = (byte) 141;
    numArray5[0] = (byte) 32 /*0x20*/;
    numArray5[22] = (byte) 65;
    numArray5[9] = (byte) 117;
    numArray5[17] = (byte) 3;
    numArray5[25] = (byte) 104;
    numArray5[26] = (byte) 120;
    numArray5[45] = (byte) 15;
    numArray5[28] = (byte) 135;
    numArray5[31 /*0x1F*/] = (byte) 67;
    numArray5[19] = (byte) 178;
    numArray5[37] = (byte) 131;
    numArray5[36] = (byte) 164;
    numArray5[7] = (byte) 38;
    numArray5[34] = (byte) 220;
    numArray5[5] = (byte) 228;
    numArray5[29] = (byte) 219;
    numArray5[48 /*0x30*/] = (byte) 130;
    numArray5[38] = (byte) 37;
    numArray5[39] = (byte) 21;
    numArray5[23] = (byte) 68;
    numArray5[46] = (byte) 38;
    numArray5[42] = (byte) 228;
    numArray5[52] = (byte) 70;
    numArray5[27] = (byte) 126;
    numArray5[16 /*0x10*/] = (byte) 205;
    numArray5[50] = (byte) 191;
    numArray5[41] = (byte) 52;
    numArray5[1] = (byte) 3;
    numArray5[49] = (byte) 134;
    numArray5[30] = (byte) 253;
    numArray5[51] = (byte) 122;
    numArray5[15] = (byte) 104;
    numArray5[53] = (byte) 109;
    byte[] numArray6 = new byte[54]
    {
      (byte) 237,
      (byte) 198,
      (byte) 106,
      (byte) 131,
      (byte) 170,
      (byte) 91,
      (byte) 233,
      (byte) 131,
      (byte) 40,
      (byte) 245,
      (byte) 118,
      (byte) 47,
      (byte) 196,
      (byte) 95,
      (byte) 177,
      (byte) 69,
      (byte) 147,
      (byte) 95,
      (byte) 188,
      (byte) 81,
      (byte) 181,
      (byte) 205,
      (byte) 220,
      (byte) 156,
      (byte) 144 /*0x90*/,
      (byte) 51,
      (byte) 178,
      (byte) 181,
      (byte) 227,
      (byte) 2,
      (byte) 112 /*0x70*/,
      (byte) 30,
      (byte) 78,
      (byte) 192 /*0xC0*/,
      (byte) 36,
      (byte) 216,
      (byte) 223,
      (byte) 177,
      (byte) 197,
      (byte) 204,
      (byte) 232,
      (byte) 167,
      (byte) 71,
      (byte) 144 /*0x90*/,
      (byte) 182,
      (byte) 205,
      (byte) 251,
      (byte) 225,
      (byte) 19,
      (byte) 216,
      (byte) 200,
      (byte) 28,
      (byte) 254,
      (byte) 2
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 54);
    for (int index = 0; index < 54; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[28];
    byte[] response = new byte[28];
    Array.Copy((Array) sc_12586.sspq, 679, (Array) numArray7, 0, 28);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12586.sspr, 679, (Array) numArray7, 0, 28);
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

  internal static string ssp_appserver_12664()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50];
      numArray2[17] = (byte) 35;
      numArray2[1] = (byte) 210;
      numArray2[2] = (byte) 253;
      numArray2[14] = (byte) 128 /*0x80*/;
      numArray2[4] = (byte) 103;
      numArray2[29] = (byte) 178;
      numArray2[12] = (byte) 167;
      numArray2[6] = (byte) 238;
      numArray2[48 /*0x30*/] = (byte) 223;
      numArray2[9] = (byte) 81;
      numArray2[10] = (byte) 248;
      numArray2[37] = (byte) 142;
      numArray2[8] = (byte) 122;
      numArray2[5] = (byte) 236;
      numArray2[16 /*0x10*/] = (byte) 223;
      numArray2[15] = (byte) 232;
      numArray2[30] = (byte) 148;
      numArray2[13] = (byte) 12;
      numArray2[27] = (byte) 43;
      numArray2[19] = (byte) 164;
      numArray2[20] = (byte) 113;
      numArray2[21] = (byte) 32 /*0x20*/;
      numArray2[35] = (byte) 203;
      numArray2[23] = (byte) 41;
      numArray2[24] = (byte) 153;
      numArray2[25] = (byte) 6;
      numArray2[26] = (byte) 30;
      numArray2[44] = (byte) 21;
      numArray2[41] = (byte) 171;
      numArray2[31 /*0x1F*/] = (byte) 96 /*0x60*/;
      numArray2[18] = (byte) 237;
      numArray2[28] = (byte) 94;
      numArray2[11] = (byte) 206;
      numArray2[33] = (byte) 15;
      numArray2[22] = (byte) 34;
      numArray2[34] = (byte) 36;
      numArray2[36] = (byte) 135;
      numArray2[32 /*0x20*/] = (byte) 214;
      numArray2[38] = (byte) 200;
      numArray2[39] = (byte) 232;
      numArray2[49] = (byte) 235;
      numArray2[42] = (byte) 195;
      numArray2[3] = (byte) 82;
      numArray2[45] = (byte) 222;
      numArray2[40] = (byte) 226;
      numArray2[7] = (byte) 127 /*0x7F*/;
      numArray2[46] = (byte) 231;
      numArray2[47] = (byte) 214;
      numArray2[43] = (byte) 13;
      numArray2[0] = (byte) 158;
      byte[] numArray3 = new byte[50]
      {
        (byte) 146,
        (byte) 231,
        (byte) 104,
        (byte) 88,
        (byte) 33,
        (byte) 21,
        (byte) 210,
        (byte) 35,
        (byte) 221,
        (byte) 243,
        (byte) 176 /*0xB0*/,
        (byte) 71,
        (byte) 35,
        (byte) 180,
        (byte) 22,
        (byte) 95,
        (byte) 92,
        (byte) 241,
        (byte) 153,
        (byte) 237,
        (byte) 239,
        (byte) 100,
        (byte) 178,
        (byte) 79,
        (byte) 60,
        (byte) 210,
        (byte) 49,
        (byte) 45,
        (byte) 148,
        (byte) 45,
        (byte) 232,
        (byte) 156,
        (byte) 134,
        (byte) 131,
        (byte) 120,
        (byte) 123,
        (byte) 115,
        (byte) 241,
        (byte) 73,
        (byte) 194,
        (byte) 37,
        (byte) 120,
        (byte) 29,
        (byte) 121,
        (byte) 201,
        (byte) 90,
        (byte) 122,
        (byte) 127 /*0x7F*/,
        (byte) 47,
        (byte) 138
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_12586.sspq, 707, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12586.sspr, 707, (Array) numArray4, 0, 48 /*0x30*/);
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
    numArray6[6] = (byte) 145;
    numArray6[36] = (byte) 160 /*0xA0*/;
    numArray6[32 /*0x20*/] = (byte) 250;
    numArray6[42] = (byte) 249;
    numArray6[9] = (byte) 55;
    numArray6[14] = (byte) 214;
    numArray6[16 /*0x10*/] = (byte) 40;
    numArray6[30] = (byte) 206;
    numArray6[26] = (byte) 186;
    numArray6[2] = (byte) 245;
    numArray6[10] = (byte) 95;
    numArray6[11] = (byte) 156;
    numArray6[18] = (byte) 45;
    numArray6[13] = (byte) 191;
    numArray6[40] = (byte) 143;
    numArray6[38] = (byte) 124;
    numArray6[4] = (byte) 83;
    numArray6[17] = (byte) 115;
    numArray6[47] = (byte) 198;
    numArray6[43] = (byte) 172;
    numArray6[20] = (byte) 196;
    numArray6[21] = (byte) 232;
    numArray6[22] = (byte) 201;
    numArray6[31 /*0x1F*/] = (byte) 87;
    numArray6[5] = (byte) 174;
    numArray6[0] = (byte) 73;
    numArray6[45] = (byte) 247;
    numArray6[7] = (byte) 222;
    numArray6[33] = (byte) 93;
    numArray6[29] = (byte) 163;
    numArray6[3] = (byte) 192 /*0xC0*/;
    numArray6[23] = (byte) 194;
    numArray6[19] = (byte) 85;
    numArray6[27] = (byte) 25;
    numArray6[34] = (byte) 23;
    numArray6[35] = (byte) 34;
    numArray6[48 /*0x30*/] = (byte) 169;
    numArray6[37] = (byte) 20;
    numArray6[25] = (byte) 15;
    numArray6[39] = (byte) 170;
    numArray6[8] = (byte) 208 /*0xD0*/;
    numArray6[12] = (byte) 115;
    numArray6[1] = (byte) 162;
    numArray6[24] = (byte) 145;
    numArray6[44] = (byte) 101;
    numArray6[28] = (byte) 182;
    numArray6[46] = (byte) 121;
    numArray6[41] = (byte) 10;
    numArray6[15] = (byte) 40;
    numArray6[49] = (byte) 204;
    byte[] numArray7 = new byte[50]
    {
      (byte) 183,
      (byte) 198,
      (byte) 213,
      (byte) 135,
      (byte) 15,
      (byte) 175,
      (byte) 222,
      (byte) 184,
      (byte) 75,
      (byte) 72,
      (byte) 165,
      (byte) 214,
      (byte) 34,
      (byte) 181,
      (byte) 2,
      (byte) 17,
      (byte) 65,
      byte.MaxValue,
      (byte) 22,
      (byte) 32 /*0x20*/,
      (byte) 143,
      (byte) 165,
      (byte) 14,
      (byte) 76,
      (byte) 231,
      (byte) 95,
      byte.MaxValue,
      (byte) 94,
      (byte) 82,
      (byte) 145,
      (byte) 231,
      (byte) 251,
      (byte) 91,
      (byte) 52,
      (byte) 220,
      (byte) 158,
      (byte) 160 /*0xA0*/,
      (byte) 245,
      (byte) 43,
      (byte) 132,
      (byte) 212,
      (byte) 232,
      (byte) 242,
      (byte) 131,
      (byte) 112 /*0x70*/,
      (byte) 80 /*0x50*/,
      (byte) 98,
      (byte) 50,
      (byte) 50,
      (byte) 171
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12665(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[2] = (byte) 227;
    sourceArray1[1] = (byte) 186;
    sourceArray1[32 /*0x20*/] = (byte) 123;
    sourceArray1[35] = (byte) 97;
    sourceArray1[4] = (byte) 95;
    sourceArray1[23] = (byte) 204;
    sourceArray1[27] = (byte) 218;
    sourceArray1[42] = (byte) 78;
    sourceArray1[8] = (byte) 149;
    sourceArray1[19] = (byte) 105;
    sourceArray1[39] = (byte) 70;
    sourceArray1[11] = (byte) 6;
    sourceArray1[21] = (byte) 48 /*0x30*/;
    sourceArray1[13] = (byte) 56;
    sourceArray1[14] = (byte) 77;
    sourceArray1[6] = (byte) 187;
    sourceArray1[36] = (byte) 12;
    sourceArray1[26] = (byte) 33;
    sourceArray1[18] = (byte) 240 /*0xF0*/;
    sourceArray1[47] = (byte) 148;
    sourceArray1[20] = (byte) 25;
    sourceArray1[25] = (byte) 215;
    sourceArray1[3] = (byte) 140;
    sourceArray1[37] = (byte) 27;
    sourceArray1[24] = (byte) 173;
    sourceArray1[17] = (byte) 208 /*0xD0*/;
    sourceArray1[15] = (byte) 24;
    sourceArray1[10] = (byte) 0;
    sourceArray1[28] = (byte) 0;
    sourceArray1[29] = (byte) 68;
    sourceArray1[9] = (byte) 174;
    sourceArray1[31 /*0x1F*/] = (byte) 213;
    sourceArray1[5] = (byte) 113;
    sourceArray1[33] = (byte) 164;
    sourceArray1[34] = (byte) 88;
    sourceArray1[16 /*0x10*/] = (byte) 250;
    sourceArray1[40] = (byte) 143;
    sourceArray1[22] = (byte) 190;
    sourceArray1[38] = (byte) 229;
    sourceArray1[0] = (byte) 155;
    sourceArray1[12] = (byte) 62;
    sourceArray1[41] = (byte) 151;
    sourceArray1[30] = (byte) 49;
    sourceArray1[43] = (byte) 137;
    sourceArray1[44] = (byte) 182;
    sourceArray1[45] = (byte) 18;
    sourceArray1[46] = (byte) 163;
    sourceArray1[7] = (byte) 213;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 174,
      (byte) 191,
      (byte) 234,
      (byte) 76,
      (byte) 64 /*0x40*/,
      (byte) 140,
      (byte) 221,
      byte.MaxValue,
      (byte) 81,
      (byte) 192 /*0xC0*/,
      (byte) 204,
      (byte) 230,
      (byte) 25,
      (byte) 41,
      (byte) 228,
      (byte) 77,
      (byte) 137,
      (byte) 77,
      (byte) 233,
      (byte) 42,
      (byte) 53,
      (byte) 103,
      (byte) 240 /*0xF0*/,
      (byte) 20,
      (byte) 87,
      (byte) 214,
      (byte) 228,
      (byte) 203,
      (byte) 210,
      (byte) 96 /*0x60*/,
      (byte) 183,
      (byte) 132,
      (byte) 3,
      (byte) 91,
      (byte) 228,
      (byte) 233,
      byte.MaxValue,
      (byte) 228,
      (byte) 1,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 56,
      (byte) 76,
      (byte) 60,
      (byte) 245,
      (byte) 91,
      (byte) 140,
      (byte) 55
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[40];
    byte[] response2 = new byte[40];
    Array.Copy((Array) sc_12586.sspq, 755, (Array) numArray2, 0, 40);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12586.sspr, 755, (Array) numArray2, 0, 40);
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

  internal static int ssp_appserver_12666(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 62,
      (byte) 208 /*0xD0*/,
      (byte) 99,
      (byte) 159,
      (byte) 243,
      (byte) 247,
      (byte) 8,
      (byte) 52,
      (byte) 178,
      (byte) 167,
      (byte) 150,
      (byte) 46,
      (byte) 6,
      (byte) 236,
      (byte) 232,
      (byte) 193,
      (byte) 133,
      (byte) 34,
      (byte) 218,
      (byte) 27,
      (byte) 208 /*0xD0*/,
      (byte) 132,
      (byte) 233,
      (byte) 165,
      (byte) 158,
      (byte) 117,
      (byte) 49,
      (byte) 42,
      (byte) 233,
      (byte) 33,
      (byte) 152,
      (byte) 249,
      (byte) 232,
      (byte) 97,
      (byte) 92,
      (byte) 80 /*0x50*/,
      (byte) 143,
      (byte) 229,
      (byte) 35,
      (byte) 170,
      (byte) 132,
      (byte) 126,
      (byte) 121,
      (byte) 24,
      (byte) 78,
      (byte) 249,
      (byte) 133,
      (byte) 39
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 115,
      (byte) 216,
      (byte) 12,
      (byte) 232,
      (byte) 64 /*0x40*/,
      (byte) 165,
      (byte) 160 /*0xA0*/,
      (byte) 196,
      (byte) 154,
      (byte) 17,
      (byte) 129,
      (byte) 74,
      (byte) 36,
      (byte) 21,
      (byte) 238,
      (byte) 213,
      (byte) 37,
      (byte) 126,
      (byte) 5,
      (byte) 238,
      (byte) 15,
      (byte) 68,
      (byte) 82,
      (byte) 90,
      (byte) 202,
      (byte) 8,
      (byte) 225,
      (byte) 223,
      (byte) 172,
      (byte) 117,
      (byte) 211,
      (byte) 102,
      (byte) 204,
      (byte) 26,
      (byte) 205,
      (byte) 193,
      (byte) 207,
      (byte) 179,
      (byte) 38,
      (byte) 153,
      (byte) 127 /*0x7F*/,
      (byte) 54,
      (byte) 127 /*0x7F*/,
      (byte) 212,
      (byte) 58,
      (byte) 230,
      (byte) 76,
      (byte) 8
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[24];
    byte[] response2 = new byte[24];
    Array.Copy((Array) sc_12586.sspq, 795, (Array) numArray2, 0, 24);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12586.sspr, 795, (Array) numArray2, 0, 24);
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

  internal static int ssp_appserver_12667(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 86,
      (byte) 164,
      (byte) 240 /*0xF0*/,
      (byte) 10,
      (byte) 233,
      (byte) 211,
      (byte) 205,
      (byte) 56,
      (byte) 102,
      (byte) 13,
      (byte) 15,
      (byte) 162,
      (byte) 105,
      (byte) 208 /*0xD0*/,
      (byte) 228,
      (byte) 165,
      (byte) 228,
      (byte) 166,
      (byte) 52,
      (byte) 232,
      (byte) 8,
      (byte) 178,
      (byte) 179,
      (byte) 224 /*0xE0*/,
      (byte) 151,
      (byte) 168,
      (byte) 227,
      (byte) 179,
      (byte) 95,
      (byte) 99,
      (byte) 68,
      (byte) 0,
      (byte) 174,
      (byte) 21,
      (byte) 103,
      (byte) 92,
      (byte) 141,
      (byte) 187,
      (byte) 244,
      (byte) 119,
      (byte) 194,
      (byte) 214,
      (byte) 53,
      (byte) 55,
      (byte) 148,
      (byte) 237,
      (byte) 46,
      (byte) 97
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 116,
      (byte) 71,
      (byte) 215,
      (byte) 146,
      (byte) 149,
      (byte) 7,
      (byte) 243,
      (byte) 236,
      (byte) 248,
      (byte) 188,
      (byte) 106,
      (byte) 202,
      (byte) 170,
      (byte) 198,
      (byte) 150,
      (byte) 84,
      (byte) 18,
      (byte) 166,
      (byte) 204,
      (byte) 33,
      (byte) 87,
      (byte) 154,
      (byte) 129,
      (byte) 143,
      (byte) 135,
      (byte) 0,
      (byte) 224 /*0xE0*/,
      (byte) 132,
      (byte) 35,
      (byte) 114,
      (byte) 119,
      (byte) 211,
      (byte) 207,
      (byte) 103,
      (byte) 181,
      (byte) 250,
      (byte) 79,
      (byte) 82,
      (byte) 28,
      (byte) 103,
      (byte) 62,
      (byte) 253,
      (byte) 206,
      (byte) 223,
      (byte) 69,
      (byte) 112 /*0x70*/,
      (byte) 67,
      (byte) 35
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12668(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 34,
      (byte) 200,
      (byte) 164,
      (byte) 240 /*0xF0*/,
      (byte) 15,
      (byte) 115,
      (byte) 64 /*0x40*/,
      (byte) 239,
      (byte) 26,
      (byte) 217,
      (byte) 3,
      (byte) 201,
      (byte) 15,
      (byte) 243,
      (byte) 83,
      (byte) 105,
      (byte) 135,
      (byte) 24,
      (byte) 41,
      (byte) 177,
      (byte) 189,
      (byte) 37,
      (byte) 50,
      (byte) 124,
      (byte) 123,
      (byte) 76,
      (byte) 169,
      (byte) 122,
      (byte) 129,
      (byte) 246,
      (byte) 5,
      (byte) 24,
      (byte) 83,
      (byte) 227,
      (byte) 140,
      (byte) 77,
      (byte) 194,
      (byte) 107,
      (byte) 125,
      (byte) 72,
      (byte) 179,
      (byte) 135,
      (byte) 168,
      (byte) 253,
      (byte) 49,
      (byte) 110,
      (byte) 133,
      (byte) 249
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[3] = (byte) 111;
    sourceArray2[1] = (byte) 27;
    sourceArray2[15] = (byte) 20;
    sourceArray2[22] = (byte) 162;
    sourceArray2[4] = (byte) 132;
    sourceArray2[42] = (byte) 27;
    sourceArray2[40] = (byte) 214;
    sourceArray2[38] = (byte) 254;
    sourceArray2[46] = (byte) 243;
    sourceArray2[9] = (byte) 23;
    sourceArray2[10] = (byte) 182;
    sourceArray2[7] = (byte) 69;
    sourceArray2[12] = (byte) 85;
    sourceArray2[13] = (byte) 25;
    sourceArray2[14] = (byte) 56;
    sourceArray2[18] = (byte) 87;
    sourceArray2[16 /*0x10*/] = (byte) 62;
    sourceArray2[17] = (byte) 6;
    sourceArray2[6] = (byte) 219;
    sourceArray2[19] = (byte) 216;
    sourceArray2[8] = (byte) 222;
    sourceArray2[21] = (byte) 25;
    sourceArray2[44] = (byte) 22;
    sourceArray2[23] = (byte) 10;
    sourceArray2[35] = (byte) 182;
    sourceArray2[25] = (byte) 135;
    sourceArray2[26] = (byte) 115;
    sourceArray2[24] = (byte) 44;
    sourceArray2[28] = (byte) 74;
    sourceArray2[29] = (byte) 102;
    sourceArray2[30] = (byte) 221;
    sourceArray2[31 /*0x1F*/] = (byte) 206;
    sourceArray2[34] = (byte) 62;
    sourceArray2[33] = (byte) 209;
    sourceArray2[20] = (byte) 98;
    sourceArray2[37] = (byte) 9;
    sourceArray2[36] = (byte) 235;
    sourceArray2[2] = (byte) 162;
    sourceArray2[45] = (byte) 245;
    sourceArray2[27] = (byte) 66;
    sourceArray2[43] = (byte) 7;
    sourceArray2[5] = (byte) 82;
    sourceArray2[41] = (byte) 188;
    sourceArray2[39] = (byte) 219;
    sourceArray2[32 /*0x20*/] = (byte) 108;
    sourceArray2[0] = (byte) 183;
    sourceArray2[11] = (byte) 84;
    sourceArray2[47] = (byte) 254;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[46];
    byte[] response2 = new byte[46];
    Array.Copy((Array) sc_12586.sspq, 819, (Array) numArray2, 0, 46);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12586.sspr, 819, (Array) numArray2, 0, 46);
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

  internal static int ssp_appserver_12669(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 189;
    sourceArray1[1] = (byte) 101;
    sourceArray1[38] = (byte) 214;
    sourceArray1[13] = (byte) 198;
    sourceArray1[8] = (byte) 176 /*0xB0*/;
    sourceArray1[22] = (byte) 251;
    sourceArray1[39] = (byte) 244;
    sourceArray1[37] = (byte) 232;
    sourceArray1[36] = (byte) 75;
    sourceArray1[9] = (byte) 186;
    sourceArray1[10] = (byte) 134;
    sourceArray1[3] = (byte) 52;
    sourceArray1[14] = (byte) 222;
    sourceArray1[20] = (byte) 82;
    sourceArray1[23] = (byte) 94;
    sourceArray1[43] = (byte) 99;
    sourceArray1[16 /*0x10*/] = (byte) 13;
    sourceArray1[26] = (byte) 245;
    sourceArray1[7] = (byte) 151;
    sourceArray1[19] = (byte) 217;
    sourceArray1[6] = (byte) 82;
    sourceArray1[21] = (byte) 143;
    sourceArray1[2] = (byte) 197;
    sourceArray1[17] = (byte) 133;
    sourceArray1[24] = (byte) 173;
    sourceArray1[25] = (byte) 183;
    sourceArray1[12] = (byte) 241;
    sourceArray1[44] = (byte) 66;
    sourceArray1[28] = (byte) 141;
    sourceArray1[29] = (byte) 79;
    sourceArray1[30] = (byte) 199;
    sourceArray1[11] = (byte) 70;
    sourceArray1[15] = (byte) 48 /*0x30*/;
    sourceArray1[33] = (byte) 152;
    sourceArray1[32 /*0x20*/] = (byte) 77;
    sourceArray1[27] = (byte) 63 /*0x3F*/;
    sourceArray1[46] = (byte) 184;
    sourceArray1[5] = (byte) 23;
    sourceArray1[35] = (byte) 252;
    sourceArray1[34] = (byte) 35;
    sourceArray1[40] = (byte) 177;
    sourceArray1[41] = (byte) 125;
    sourceArray1[42] = (byte) 189;
    sourceArray1[18] = (byte) 84;
    sourceArray1[4] = (byte) 35;
    sourceArray1[45] = (byte) 240 /*0xF0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 248;
    sourceArray1[47] = (byte) 84;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 13,
      (byte) 7,
      (byte) 13,
      (byte) 169,
      (byte) 9,
      (byte) 210,
      (byte) 109,
      (byte) 250,
      (byte) 179,
      (byte) 189,
      (byte) 192 /*0xC0*/,
      (byte) 222,
      (byte) 83,
      (byte) 121,
      (byte) 217,
      (byte) 61,
      (byte) 4,
      (byte) 185,
      (byte) 10,
      (byte) 91,
      (byte) 88,
      (byte) 95,
      (byte) 227,
      (byte) 87,
      (byte) 35,
      (byte) 222,
      (byte) 77,
      (byte) 176 /*0xB0*/,
      (byte) 148,
      (byte) 43,
      (byte) 15,
      (byte) 28,
      (byte) 69,
      (byte) 45,
      (byte) 177,
      (byte) 192 /*0xC0*/,
      (byte) 105,
      (byte) 211,
      (byte) 93,
      (byte) 116,
      (byte) 4,
      (byte) 0,
      (byte) 8,
      (byte) 167,
      (byte) 214,
      (byte) 113,
      (byte) 75,
      (byte) 75
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12670()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[75];
      byte[] numArray2 = new byte[55]
      {
        (byte) 248,
        (byte) 95,
        (byte) 176 /*0xB0*/,
        (byte) 244,
        (byte) 190,
        (byte) 38,
        (byte) 219,
        (byte) 160 /*0xA0*/,
        (byte) 189,
        (byte) 102,
        (byte) 92,
        (byte) 208 /*0xD0*/,
        (byte) 74,
        (byte) 201,
        (byte) 109,
        (byte) 129,
        (byte) 170,
        (byte) 32 /*0x20*/,
        (byte) 92,
        (byte) 182,
        (byte) 151,
        (byte) 143,
        (byte) 253,
        (byte) 174,
        (byte) 78,
        (byte) 0,
        (byte) 196,
        (byte) 68,
        (byte) 142,
        (byte) 136,
        (byte) 98,
        (byte) 116,
        (byte) 40,
        (byte) 103,
        (byte) 197,
        (byte) 159,
        (byte) 240 /*0xF0*/,
        (byte) 115,
        (byte) 43,
        (byte) 220,
        (byte) 96 /*0x60*/,
        (byte) 86,
        (byte) 172,
        (byte) 245,
        (byte) 153,
        (byte) 195,
        (byte) 153,
        (byte) 151,
        (byte) 252,
        (byte) 80 /*0x50*/,
        (byte) 244,
        (byte) 82,
        (byte) 0,
        (byte) 186,
        (byte) 29
      };
      byte[] numArray3 = new byte[55];
      numArray3[19] = (byte) 189;
      numArray3[1] = (byte) 238;
      numArray3[23] = (byte) 245;
      numArray3[3] = (byte) 179;
      numArray3[4] = (byte) 141;
      numArray3[48 /*0x30*/] = (byte) 241;
      numArray3[21] = (byte) 82;
      numArray3[7] = (byte) 114;
      numArray3[44] = (byte) 163;
      numArray3[9] = (byte) 183;
      numArray3[10] = (byte) 8;
      numArray3[50] = (byte) 230;
      numArray3[6] = (byte) 147;
      numArray3[2] = (byte) 37;
      numArray3[39] = (byte) 193;
      numArray3[49] = (byte) 123;
      numArray3[35] = (byte) 120;
      numArray3[46] = (byte) 168;
      numArray3[18] = (byte) 8;
      numArray3[15] = (byte) 178;
      numArray3[20] = (byte) 243;
      numArray3[8] = (byte) 158;
      numArray3[36] = (byte) 31 /*0x1F*/;
      numArray3[38] = (byte) 53;
      numArray3[24] = (byte) 201;
      numArray3[11] = (byte) 224 /*0xE0*/;
      numArray3[51] = (byte) 107;
      numArray3[27] = (byte) 52;
      numArray3[28] = (byte) 68;
      numArray3[12] = (byte) 207;
      numArray3[42] = (byte) 51;
      numArray3[31 /*0x1F*/] = (byte) 124;
      numArray3[32 /*0x20*/] = (byte) 220;
      numArray3[33] = (byte) 128 /*0x80*/;
      numArray3[34] = (byte) 67;
      numArray3[16 /*0x10*/] = (byte) 60;
      numArray3[22] = (byte) 222;
      numArray3[37] = (byte) 207;
      numArray3[14] = (byte) 137;
      numArray3[13] = (byte) 114;
      numArray3[25] = (byte) 250;
      numArray3[41] = (byte) 49;
      numArray3[26] = (byte) 123;
      numArray3[5] = (byte) 68;
      numArray3[30] = (byte) 218;
      numArray3[45] = (byte) 166;
      numArray3[29] = (byte) 195;
      numArray3[47] = (byte) 59;
      numArray3[43] = (byte) 167;
      numArray3[53] = (byte) 42;
      numArray3[17] = (byte) 218;
      numArray3[40] = (byte) 31 /*0x1F*/;
      numArray3[52] = (byte) 223;
      numArray3[0] = (byte) 143;
      numArray3[54] = (byte) 208 /*0xD0*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20]
      {
        (byte) 236,
        (byte) 28,
        (byte) 87,
        (byte) 54,
        (byte) 79,
        (byte) 212,
        (byte) 160 /*0xA0*/,
        (byte) 245,
        (byte) 114,
        (byte) 58,
        (byte) 153,
        (byte) 168,
        (byte) 144 /*0x90*/,
        (byte) 163,
        (byte) 106,
        (byte) 134,
        (byte) 5,
        (byte) 139,
        (byte) 252,
        (byte) 107
      };
      byte[] numArray5 = new byte[20];
      numArray5[2] = (byte) 38;
      numArray5[17] = (byte) 102;
      numArray5[4] = (byte) 9;
      numArray5[3] = (byte) 250;
      numArray5[13] = (byte) 206;
      numArray5[1] = (byte) 105;
      numArray5[6] = (byte) 173;
      numArray5[8] = (byte) 170;
      numArray5[7] = (byte) 165;
      numArray5[9] = (byte) 97;
      numArray5[10] = (byte) 75;
      numArray5[15] = (byte) 71;
      numArray5[12] = (byte) 107;
      numArray5[11] = (byte) 225;
      numArray5[14] = (byte) 84;
      numArray5[18] = (byte) 139;
      numArray5[5] = (byte) 197;
      numArray5[16 /*0x10*/] = (byte) 216;
      numArray5[19] = (byte) 168;
      numArray5[0] = (byte) 208 /*0xD0*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[75];
    byte[] numArray7 = new byte[55];
    numArray7[24] = (byte) 173;
    numArray7[51] = (byte) 25;
    numArray7[2] = (byte) 234;
    numArray7[28] = (byte) 194;
    numArray7[4] = (byte) 27;
    numArray7[1] = (byte) 29;
    numArray7[6] = (byte) 216;
    numArray7[27] = (byte) 111;
    numArray7[30] = (byte) 140;
    numArray7[9] = (byte) 223;
    numArray7[10] = (byte) 26;
    numArray7[19] = (byte) 68;
    numArray7[0] = (byte) 224 /*0xE0*/;
    numArray7[54] = (byte) 192 /*0xC0*/;
    numArray7[14] = (byte) 167;
    numArray7[18] = (byte) 200;
    numArray7[13] = (byte) 172;
    numArray7[3] = (byte) 157;
    numArray7[40] = (byte) 215;
    numArray7[43] = (byte) 142;
    numArray7[45] = (byte) 158;
    numArray7[50] = (byte) 68;
    numArray7[22] = (byte) 43;
    numArray7[25] = (byte) 162;
    numArray7[7] = (byte) 122;
    numArray7[12] = (byte) 137;
    numArray7[34] = (byte) 143;
    numArray7[32 /*0x20*/] = (byte) 25;
    numArray7[42] = (byte) 46;
    numArray7[15] = (byte) 156;
    numArray7[31 /*0x1F*/] = (byte) 148;
    numArray7[47] = (byte) 4;
    numArray7[33] = (byte) 36;
    numArray7[26] = (byte) 186;
    numArray7[21] = (byte) 49;
    numArray7[23] = (byte) 210;
    numArray7[36] = (byte) 175;
    numArray7[29] = (byte) 16 /*0x10*/;
    numArray7[5] = (byte) 81;
    numArray7[39] = (byte) 134;
    numArray7[49] = (byte) 167;
    numArray7[17] = (byte) 86;
    numArray7[38] = (byte) 98;
    numArray7[16 /*0x10*/] = (byte) 54;
    numArray7[44] = (byte) 187;
    numArray7[8] = (byte) 218;
    numArray7[46] = (byte) 119;
    numArray7[37] = (byte) 123;
    numArray7[48 /*0x30*/] = (byte) 110;
    numArray7[35] = (byte) 146;
    numArray7[41] = (byte) 121;
    numArray7[11] = (byte) 68;
    numArray7[20] = (byte) 126;
    numArray7[53] = (byte) 183;
    numArray7[52] = (byte) 14;
    byte[] numArray8 = new byte[55]
    {
      (byte) 37,
      (byte) 14,
      (byte) 100,
      (byte) 148,
      (byte) 16 /*0x10*/,
      (byte) 87,
      (byte) 104,
      (byte) 149,
      (byte) 144 /*0x90*/,
      (byte) 80 /*0x50*/,
      (byte) 125,
      (byte) 3,
      (byte) 183,
      (byte) 64 /*0x40*/,
      (byte) 33,
      (byte) 232,
      (byte) 19,
      (byte) 228,
      (byte) 228,
      (byte) 142,
      (byte) 183,
      (byte) 139,
      (byte) 199,
      (byte) 47,
      (byte) 114,
      (byte) 227,
      (byte) 184,
      (byte) 87,
      (byte) 175,
      (byte) 235,
      (byte) 108,
      (byte) 228,
      (byte) 34,
      (byte) 73,
      (byte) 94,
      (byte) 141,
      (byte) 72,
      (byte) 180,
      (byte) 76,
      (byte) 85,
      (byte) 85,
      (byte) 52,
      (byte) 200,
      (byte) 100,
      (byte) 89,
      (byte) 144 /*0x90*/,
      (byte) 194,
      (byte) 206,
      (byte) 229,
      (byte) 231,
      (byte) 245,
      (byte) 74,
      (byte) 139,
      (byte) 85,
      (byte) 132
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[20]
    {
      byte.MaxValue,
      (byte) 224 /*0xE0*/,
      (byte) 78,
      (byte) 224 /*0xE0*/,
      (byte) 103,
      (byte) 158,
      (byte) 253,
      (byte) 223,
      (byte) 1,
      (byte) 199,
      (byte) 1,
      (byte) 60,
      (byte) 117,
      (byte) 84,
      (byte) 47,
      (byte) 10,
      (byte) 83,
      (byte) 253,
      (byte) 69,
      (byte) 158
    };
    byte[] numArray10 = new byte[20]
    {
      (byte) 127 /*0x7F*/,
      (byte) 205,
      (byte) 64 /*0x40*/,
      (byte) 247,
      (byte) 63 /*0x3F*/,
      (byte) 100,
      (byte) 117,
      (byte) 35,
      (byte) 135,
      (byte) 193,
      (byte) 165,
      (byte) 21,
      (byte) 122,
      (byte) 217,
      (byte) 28,
      (byte) 167,
      (byte) 125,
      (byte) 153,
      (byte) 39,
      (byte) 122
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 20);
    for (int index = 0; index < 20; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12671(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 167,
      (byte) 95,
      (byte) 66,
      (byte) 77,
      (byte) 184,
      (byte) 24,
      (byte) 168,
      (byte) 53,
      (byte) 74,
      (byte) 152,
      (byte) 171,
      (byte) 155,
      (byte) 14,
      (byte) 7,
      (byte) 34,
      (byte) 103,
      (byte) 92,
      (byte) 51,
      (byte) 172,
      (byte) 17,
      (byte) 170,
      (byte) 228,
      (byte) 102,
      (byte) 90,
      (byte) 27,
      (byte) 121,
      (byte) 76,
      (byte) 117,
      (byte) 12,
      (byte) 243,
      (byte) 28,
      (byte) 69,
      (byte) 53,
      (byte) 17,
      (byte) 191,
      (byte) 62,
      (byte) 186,
      (byte) 145,
      (byte) 72,
      (byte) 175,
      (byte) 89,
      (byte) 6,
      (byte) 42,
      (byte) 188,
      (byte) 132,
      (byte) 110,
      (byte) 68,
      (byte) 246
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 119,
      (byte) 149,
      (byte) 242,
      (byte) 51,
      (byte) 65,
      (byte) 124,
      (byte) 30,
      (byte) 25,
      (byte) 61,
      (byte) 211,
      (byte) 101,
      (byte) 73,
      (byte) 151,
      (byte) 21,
      (byte) 77,
      (byte) 215,
      (byte) 26,
      (byte) 227,
      (byte) 169,
      (byte) 150,
      (byte) 67,
      (byte) 187,
      (byte) 16 /*0x10*/,
      (byte) 183,
      (byte) 135,
      (byte) 17,
      (byte) 131,
      (byte) 75,
      (byte) 184,
      (byte) 71,
      (byte) 164,
      (byte) 69,
      (byte) 4,
      (byte) 55,
      (byte) 53,
      (byte) 246,
      (byte) 230,
      (byte) 142,
      (byte) 180,
      (byte) 195,
      (byte) 170,
      (byte) 200,
      (byte) 119,
      (byte) 244,
      (byte) 7,
      (byte) 227,
      (byte) 177,
      (byte) 212
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12672(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[47] = (byte) 9;
    sourceArray1[1] = (byte) 36;
    sourceArray1[2] = (byte) 195;
    sourceArray1[6] = (byte) 63 /*0x3F*/;
    sourceArray1[0] = (byte) 6;
    sourceArray1[22] = (byte) 163;
    sourceArray1[43] = (byte) 221;
    sourceArray1[7] = (byte) 203;
    sourceArray1[8] = (byte) 50;
    sourceArray1[9] = (byte) 41;
    sourceArray1[13] = (byte) 101;
    sourceArray1[11] = (byte) 123;
    sourceArray1[12] = (byte) 102;
    sourceArray1[37] = (byte) 68;
    sourceArray1[46] = (byte) 30;
    sourceArray1[15] = (byte) 71;
    sourceArray1[16 /*0x10*/] = (byte) 227;
    sourceArray1[39] = (byte) 94;
    sourceArray1[25] = (byte) 214;
    sourceArray1[19] = (byte) 235;
    sourceArray1[45] = (byte) 64 /*0x40*/;
    sourceArray1[21] = (byte) 184;
    sourceArray1[24] = (byte) 170;
    sourceArray1[32 /*0x20*/] = (byte) 109;
    sourceArray1[4] = (byte) 232;
    sourceArray1[23] = (byte) 185;
    sourceArray1[26] = (byte) 172;
    sourceArray1[10] = (byte) 86;
    sourceArray1[28] = (byte) 86;
    sourceArray1[3] = (byte) 249;
    sourceArray1[30] = (byte) 212;
    sourceArray1[33] = (byte) 86;
    sourceArray1[17] = (byte) 173;
    sourceArray1[36] = (byte) 252;
    sourceArray1[34] = (byte) 47;
    sourceArray1[41] = (byte) 227;
    sourceArray1[18] = (byte) 202;
    sourceArray1[5] = (byte) 50;
    sourceArray1[38] = (byte) 209;
    sourceArray1[40] = (byte) 8;
    sourceArray1[29] = (byte) 82;
    sourceArray1[35] = (byte) 236;
    sourceArray1[27] = (byte) 52;
    sourceArray1[20] = (byte) 252;
    sourceArray1[44] = (byte) 211;
    sourceArray1[14] = (byte) 253;
    sourceArray1[31 /*0x1F*/] = (byte) 238;
    sourceArray1[42] = (byte) 38;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 251,
      (byte) 17,
      (byte) 132,
      (byte) 109,
      (byte) 135,
      (byte) 54,
      (byte) 128 /*0x80*/,
      (byte) 33,
      (byte) 189,
      (byte) 58,
      (byte) 155,
      (byte) 11,
      (byte) 207,
      (byte) 152,
      (byte) 155,
      (byte) 63 /*0x3F*/,
      (byte) 93,
      (byte) 131,
      (byte) 224 /*0xE0*/,
      (byte) 88,
      (byte) 58,
      (byte) 73,
      (byte) 42,
      (byte) 195,
      (byte) 52,
      (byte) 158,
      (byte) 142,
      (byte) 176 /*0xB0*/,
      (byte) 185,
      (byte) 176 /*0xB0*/,
      (byte) 34,
      (byte) 32 /*0x20*/,
      (byte) 36,
      (byte) 142,
      (byte) 96 /*0x60*/,
      (byte) 156,
      (byte) 6,
      (byte) 81,
      (byte) 3,
      (byte) 83,
      (byte) 6,
      (byte) 2,
      (byte) 166,
      (byte) 19,
      (byte) 109,
      (byte) 148,
      (byte) 209,
      (byte) 125
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12673()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[75];
      byte[] numArray2 = new byte[55];
      numArray2[10] = (byte) 189;
      numArray2[1] = (byte) 14;
      numArray2[2] = (byte) 211;
      numArray2[29] = (byte) 230;
      numArray2[4] = (byte) 98;
      numArray2[42] = (byte) 199;
      numArray2[31 /*0x1F*/] = (byte) 73;
      numArray2[51] = (byte) 162;
      numArray2[35] = (byte) 169;
      numArray2[32 /*0x20*/] = (byte) 62;
      numArray2[21] = (byte) 101;
      numArray2[11] = (byte) 186;
      numArray2[50] = (byte) 221;
      numArray2[5] = (byte) 159;
      numArray2[20] = (byte) 180;
      numArray2[15] = (byte) 226;
      numArray2[16 /*0x10*/] = (byte) 31 /*0x1F*/;
      numArray2[39] = (byte) 115;
      numArray2[18] = (byte) 203;
      numArray2[3] = (byte) 54;
      numArray2[12] = (byte) 34;
      numArray2[38] = (byte) 70;
      numArray2[22] = (byte) 171;
      numArray2[23] = (byte) 85;
      numArray2[24] = (byte) 165;
      numArray2[45] = (byte) 177;
      numArray2[6] = (byte) 174;
      numArray2[27] = (byte) 28;
      numArray2[0] = (byte) 50;
      numArray2[26] = (byte) 169;
      numArray2[30] = (byte) 169;
      numArray2[8] = (byte) 225;
      numArray2[47] = (byte) 15;
      numArray2[52] = (byte) 123;
      numArray2[34] = (byte) 55;
      numArray2[53] = (byte) 125;
      numArray2[36] = (byte) 208 /*0xD0*/;
      numArray2[37] = (byte) 181;
      numArray2[17] = (byte) 204;
      numArray2[7] = (byte) 206;
      numArray2[40] = (byte) 156;
      numArray2[41] = (byte) 229;
      numArray2[28] = (byte) 146;
      numArray2[43] = (byte) 138;
      numArray2[44] = (byte) 35;
      numArray2[9] = (byte) 217;
      numArray2[46] = (byte) 207;
      numArray2[19] = (byte) 10;
      numArray2[49] = (byte) 246;
      numArray2[13] = (byte) 229;
      numArray2[33] = (byte) 123;
      numArray2[14] = (byte) 90;
      numArray2[48 /*0x30*/] = (byte) 100;
      numArray2[25] = (byte) 89;
      numArray2[54] = (byte) 182;
      byte[] numArray3 = new byte[55];
      numArray3[8] = (byte) 200;
      numArray3[1] = (byte) 7;
      numArray3[20] = (byte) 207;
      numArray3[33] = (byte) 76;
      numArray3[4] = (byte) 183;
      numArray3[5] = (byte) 204;
      numArray3[50] = (byte) 138;
      numArray3[41] = (byte) 49;
      numArray3[43] = (byte) 193;
      numArray3[15] = (byte) 142;
      numArray3[36] = (byte) 216;
      numArray3[11] = (byte) 4;
      numArray3[12] = (byte) 170;
      numArray3[47] = (byte) 85;
      numArray3[30] = (byte) 254;
      numArray3[10] = (byte) 59;
      numArray3[44] = (byte) 75;
      numArray3[2] = (byte) 37;
      numArray3[28] = (byte) 166;
      numArray3[19] = (byte) 39;
      numArray3[42] = (byte) 186;
      numArray3[17] = (byte) 184;
      numArray3[53] = (byte) 164;
      numArray3[6] = (byte) 212;
      numArray3[24] = (byte) 98;
      numArray3[3] = (byte) 229;
      numArray3[26] = (byte) 10;
      numArray3[22] = (byte) 46;
      numArray3[34] = (byte) 220;
      numArray3[29] = (byte) 199;
      numArray3[31 /*0x1F*/] = (byte) 107;
      numArray3[37] = (byte) 80 /*0x50*/;
      numArray3[32 /*0x20*/] = (byte) 58;
      numArray3[54] = (byte) 166;
      numArray3[40] = (byte) 78;
      numArray3[7] = (byte) 96 /*0x60*/;
      numArray3[27] = (byte) 25;
      numArray3[35] = (byte) 236;
      numArray3[38] = (byte) 123;
      numArray3[39] = (byte) 201;
      numArray3[52] = (byte) 80 /*0x50*/;
      numArray3[16 /*0x10*/] = (byte) 173;
      numArray3[23] = (byte) 211;
      numArray3[13] = (byte) 19;
      numArray3[9] = (byte) 57;
      numArray3[45] = (byte) 2;
      numArray3[46] = (byte) 9;
      numArray3[0] = (byte) 183;
      numArray3[48 /*0x30*/] = (byte) 1;
      numArray3[49] = (byte) 83;
      numArray3[18] = (byte) 144 /*0x90*/;
      numArray3[51] = (byte) 97;
      numArray3[14] = (byte) 210;
      numArray3[21] = (byte) 63 /*0x3F*/;
      numArray3[25] = (byte) 29;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20]
      {
        (byte) 201,
        (byte) 223,
        (byte) 30,
        (byte) 35,
        (byte) 211,
        (byte) 231,
        (byte) 93,
        (byte) 61,
        (byte) 250,
        (byte) 9,
        (byte) 5,
        (byte) 8,
        (byte) 100,
        (byte) 183,
        (byte) 191,
        (byte) 139,
        (byte) 183,
        (byte) 51,
        (byte) 27,
        (byte) 249
      };
      byte[] numArray5 = new byte[20]
      {
        (byte) 96 /*0x60*/,
        (byte) 4,
        (byte) 231,
        (byte) 77,
        (byte) 148,
        (byte) 232,
        (byte) 29,
        (byte) 37,
        (byte) 223,
        (byte) 90,
        (byte) 6,
        (byte) 206,
        (byte) 130,
        (byte) 111,
        (byte) 164,
        (byte) 19,
        (byte) 167,
        (byte) 211,
        (byte) 201,
        (byte) 61
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[75];
    byte[] numArray7 = new byte[55]
    {
      (byte) 27,
      (byte) 27,
      (byte) 188,
      (byte) 147,
      (byte) 104,
      (byte) 23,
      (byte) 95,
      (byte) 5,
      (byte) 25,
      (byte) 223,
      (byte) 137,
      (byte) 61,
      (byte) 166,
      (byte) 159,
      byte.MaxValue,
      (byte) 92,
      (byte) 34,
      (byte) 123,
      (byte) 245,
      (byte) 16 /*0x10*/,
      (byte) 112 /*0x70*/,
      (byte) 64 /*0x40*/,
      (byte) 20,
      (byte) 229,
      (byte) 109,
      (byte) 109,
      (byte) 72,
      (byte) 146,
      (byte) 119,
      (byte) 167,
      (byte) 97,
      (byte) 233,
      (byte) 83,
      (byte) 155,
      (byte) 2,
      (byte) 7,
      (byte) 23,
      (byte) 84,
      (byte) 244,
      (byte) 38,
      (byte) 155,
      (byte) 4,
      (byte) 236,
      (byte) 40,
      (byte) 218,
      (byte) 121,
      (byte) 24,
      (byte) 235,
      (byte) 184,
      (byte) 214,
      (byte) 249,
      (byte) 125,
      (byte) 156,
      (byte) 101,
      (byte) 210
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 146,
      (byte) 74,
      (byte) 198,
      (byte) 177,
      (byte) 183,
      (byte) 53,
      (byte) 171,
      (byte) 165,
      (byte) 125,
      (byte) 132,
      (byte) 79,
      (byte) 250,
      (byte) 211,
      (byte) 199,
      (byte) 163,
      (byte) 4,
      (byte) 128 /*0x80*/,
      (byte) 34,
      (byte) 164,
      (byte) 39,
      (byte) 167,
      (byte) 247,
      (byte) 125,
      (byte) 146,
      (byte) 104,
      (byte) 147,
      (byte) 177,
      (byte) 127 /*0x7F*/,
      (byte) 81,
      (byte) 118,
      (byte) 142,
      (byte) 233,
      (byte) 73,
      (byte) 124,
      (byte) 43,
      (byte) 164,
      (byte) 238,
      (byte) 150,
      (byte) 165,
      (byte) 190,
      (byte) 73,
      (byte) 46,
      (byte) 87,
      (byte) 235,
      (byte) 219,
      (byte) 75,
      (byte) 1,
      (byte) 210,
      (byte) 142,
      (byte) 125,
      (byte) 83,
      (byte) 68,
      (byte) 60
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[20]
    {
      (byte) 214,
      (byte) 38,
      (byte) 250,
      (byte) 206,
      (byte) 205,
      (byte) 141,
      (byte) 114,
      (byte) 125,
      (byte) 159,
      (byte) 234,
      (byte) 10,
      (byte) 156,
      (byte) 247,
      (byte) 69,
      (byte) 205,
      (byte) 135,
      (byte) 207,
      (byte) 117,
      (byte) 108,
      (byte) 84
    };
    byte[] numArray10 = new byte[20]
    {
      (byte) 118,
      (byte) 245,
      (byte) 74,
      (byte) 119,
      (byte) 72,
      (byte) 217,
      (byte) 132,
      (byte) 238,
      (byte) 141,
      (byte) 120,
      (byte) 33,
      (byte) 92,
      (byte) 142,
      (byte) 5,
      (byte) 132,
      (byte) 126,
      (byte) 171,
      (byte) 239,
      (byte) 162,
      (byte) 22
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 20);
    for (int index = 0; index < 20; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12674()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[74];
      byte[] numArray2 = new byte[55]
      {
        (byte) 92,
        (byte) 218,
        (byte) 161,
        (byte) 174,
        (byte) 227,
        (byte) 77,
        (byte) 237,
        (byte) 201,
        (byte) 39,
        (byte) 1,
        (byte) 64 /*0x40*/,
        (byte) 46,
        (byte) 249,
        (byte) 87,
        (byte) 206,
        (byte) 118,
        (byte) 121,
        (byte) 170,
        (byte) 59,
        (byte) 143,
        (byte) 14,
        (byte) 78,
        (byte) 192 /*0xC0*/,
        (byte) 145,
        (byte) 44,
        (byte) 246,
        (byte) 205,
        (byte) 240 /*0xF0*/,
        (byte) 102,
        (byte) 134,
        (byte) 26,
        (byte) 204,
        (byte) 50,
        (byte) 166,
        (byte) 114,
        (byte) 242,
        (byte) 236,
        (byte) 196,
        (byte) 154,
        (byte) 82,
        (byte) 252,
        (byte) 65,
        (byte) 117,
        (byte) 246,
        (byte) 4,
        (byte) 158,
        (byte) 141,
        (byte) 120,
        (byte) 167,
        (byte) 245,
        (byte) 218,
        (byte) 65,
        (byte) 207,
        (byte) 99,
        (byte) 70
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 118,
        (byte) 45,
        (byte) 120,
        (byte) 155,
        (byte) 15,
        (byte) 3,
        (byte) 156,
        (byte) 245,
        (byte) 102,
        (byte) 80 /*0x50*/,
        (byte) 236,
        (byte) 158,
        (byte) 97,
        (byte) 72,
        (byte) 124,
        (byte) 114,
        (byte) 15,
        (byte) 17,
        (byte) 21,
        (byte) 96 /*0x60*/,
        (byte) 238,
        (byte) 238,
        (byte) 44,
        (byte) 56,
        (byte) 192 /*0xC0*/,
        (byte) 169,
        (byte) 134,
        (byte) 50,
        (byte) 251,
        (byte) 170,
        (byte) 225,
        (byte) 139,
        (byte) 242,
        (byte) 149,
        (byte) 239,
        (byte) 77,
        (byte) 117,
        (byte) 222,
        (byte) 239,
        (byte) 71,
        (byte) 237,
        (byte) 47,
        (byte) 112 /*0x70*/,
        (byte) 168,
        (byte) 226,
        (byte) 73,
        (byte) 25,
        (byte) 90,
        (byte) 232,
        (byte) 95,
        (byte) 248,
        (byte) 147,
        (byte) 95,
        (byte) 121,
        (byte) 199
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[19]
      {
        (byte) 16 /*0x10*/,
        (byte) 129,
        (byte) 237,
        (byte) 127 /*0x7F*/,
        (byte) 253,
        (byte) 99,
        (byte) 160 /*0xA0*/,
        (byte) 230,
        (byte) 116,
        (byte) 251,
        (byte) 83,
        (byte) 229,
        (byte) 219,
        (byte) 138,
        (byte) 181,
        (byte) 16 /*0x10*/,
        (byte) 232,
        (byte) 146,
        (byte) 204
      };
      byte[] numArray5 = new byte[19]
      {
        (byte) 44,
        (byte) 5,
        (byte) 180,
        (byte) 206,
        (byte) 217,
        (byte) 44,
        (byte) 205,
        (byte) 135,
        (byte) 223,
        (byte) 179,
        (byte) 205,
        (byte) 201,
        (byte) 14,
        (byte) 250,
        (byte) 199,
        (byte) 158,
        (byte) 29,
        (byte) 131,
        (byte) 58
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[74];
    byte[] numArray7 = new byte[55]
    {
      (byte) 141,
      (byte) 105,
      (byte) 85,
      (byte) 72,
      (byte) 112 /*0x70*/,
      (byte) 172,
      (byte) 216,
      (byte) 253,
      (byte) 207,
      (byte) 95,
      (byte) 100,
      (byte) 217,
      (byte) 219,
      (byte) 66,
      (byte) 172,
      (byte) 210,
      byte.MaxValue,
      (byte) 222,
      (byte) 212,
      (byte) 15,
      (byte) 144 /*0x90*/,
      (byte) 163,
      (byte) 58,
      (byte) 146,
      (byte) 204,
      (byte) 107,
      (byte) 36,
      (byte) 49,
      (byte) 230,
      (byte) 208 /*0xD0*/,
      (byte) 121,
      (byte) 52,
      (byte) 79,
      (byte) 131,
      (byte) 244,
      (byte) 131,
      (byte) 42,
      (byte) 180,
      (byte) 242,
      (byte) 243,
      (byte) 14,
      (byte) 118,
      (byte) 44,
      (byte) 243,
      (byte) 129,
      (byte) 2,
      (byte) 174,
      (byte) 115,
      (byte) 99,
      (byte) 21,
      (byte) 147,
      (byte) 238,
      (byte) 5,
      (byte) 186,
      (byte) 252
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 131,
      (byte) 195,
      (byte) 156,
      (byte) 194,
      (byte) 242,
      (byte) 166,
      (byte) 66,
      (byte) 6,
      (byte) 180,
      (byte) 49,
      (byte) 211,
      (byte) 239,
      (byte) 29,
      (byte) 221,
      (byte) 196,
      (byte) 99,
      (byte) 17,
      (byte) 45,
      (byte) 43,
      (byte) 238,
      (byte) 180,
      (byte) 81,
      (byte) 215,
      (byte) 162,
      (byte) 81,
      (byte) 139,
      (byte) 182,
      (byte) 174,
      (byte) 21,
      (byte) 98,
      (byte) 140,
      (byte) 114,
      (byte) 14,
      (byte) 181,
      (byte) 167,
      (byte) 126,
      (byte) 171,
      (byte) 90,
      (byte) 121,
      (byte) 226,
      (byte) 236,
      (byte) 135,
      (byte) 162,
      (byte) 167,
      (byte) 107,
      (byte) 138,
      (byte) 84,
      (byte) 185,
      (byte) 20,
      (byte) 20,
      (byte) 232,
      (byte) 138,
      (byte) 185,
      (byte) 133,
      (byte) 93
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[19]
    {
      (byte) 194,
      (byte) 124,
      (byte) 5,
      (byte) 136,
      (byte) 166,
      (byte) 22,
      (byte) 254,
      (byte) 229,
      (byte) 47,
      (byte) 137,
      (byte) 87,
      (byte) 77,
      (byte) 40,
      (byte) 168,
      (byte) 74,
      (byte) 235,
      (byte) 224 /*0xE0*/,
      (byte) 0,
      (byte) 25
    };
    byte[] numArray10 = new byte[19]
    {
      (byte) 52,
      (byte) 94,
      (byte) 156,
      (byte) 182,
      (byte) 139,
      (byte) 99,
      (byte) 144 /*0x90*/,
      (byte) 186,
      (byte) 130,
      (byte) 72,
      (byte) 175,
      (byte) 157,
      (byte) 139,
      (byte) 240 /*0xF0*/,
      (byte) 19,
      (byte) 229,
      (byte) 131,
      (byte) 121,
      (byte) 131
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 19);
    for (int index = 0; index < 19; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[47];
    byte[] response = new byte[47];
    Array.Copy((Array) sc_12586.sspq, 865, (Array) numArray11, 0, 47);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12586.sspr, 865, (Array) numArray11, 0, 47);
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

  internal static string ssp_appserver_12675()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 116,
        (byte) 152,
        (byte) 185,
        (byte) 73,
        (byte) 57,
        (byte) 192 /*0xC0*/,
        (byte) 81,
        (byte) 100,
        (byte) 138,
        (byte) 147
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 135,
        (byte) 138,
        (byte) 59,
        (byte) 5,
        (byte) 238,
        (byte) 6,
        (byte) 224 /*0xE0*/,
        (byte) 77,
        (byte) 219,
        (byte) 90
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[3] = (byte) 151;
    numArray5[4] = (byte) 108;
    numArray5[6] = (byte) 133;
    numArray5[1] = (byte) 218;
    numArray5[2] = (byte) 165;
    numArray5[5] = (byte) 183;
    numArray5[7] = (byte) 163;
    numArray5[0] = (byte) 10;
    numArray5[8] = (byte) 148;
    numArray5[9] = (byte) 143;
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 135;
    numArray6[0] = (byte) 63 /*0x3F*/;
    numArray6[3] = byte.MaxValue;
    numArray6[4] = (byte) 10;
    numArray6[2] = (byte) 45;
    numArray6[9] = (byte) 218;
    numArray6[6] = (byte) 198;
    numArray6[7] = (byte) 169;
    numArray6[8] = (byte) 160 /*0xA0*/;
    numArray6[5] = (byte) 111;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
