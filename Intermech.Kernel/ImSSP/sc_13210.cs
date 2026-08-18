// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13210
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13210
{
  private static byte[] sspq = new byte[536]
  {
    (byte) 149,
    (byte) 239,
    (byte) 135,
    (byte) 153,
    (byte) 1,
    (byte) 144 /*0x90*/,
    (byte) 202,
    (byte) 141,
    (byte) 9,
    (byte) 28,
    (byte) 253,
    (byte) 12,
    (byte) 115,
    (byte) 212,
    (byte) 128 /*0x80*/,
    (byte) 182,
    (byte) 128 /*0x80*/,
    (byte) 200,
    (byte) 116,
    (byte) 220,
    (byte) 234,
    (byte) 64 /*0x40*/,
    (byte) 59,
    (byte) 134,
    (byte) 184,
    (byte) 134,
    (byte) 96 /*0x60*/,
    (byte) 104,
    (byte) 193,
    (byte) 60,
    (byte) 31 /*0x1F*/,
    (byte) 27,
    (byte) 247,
    (byte) 177,
    (byte) 44,
    (byte) 237,
    (byte) 143,
    (byte) 219,
    (byte) 89,
    (byte) 65,
    (byte) 128 /*0x80*/,
    (byte) 187,
    (byte) 131,
    (byte) 174,
    (byte) 78,
    (byte) 249,
    (byte) 225,
    (byte) 197,
    (byte) 234,
    (byte) 246,
    (byte) 30,
    (byte) 119,
    (byte) 108,
    (byte) 118,
    (byte) 102,
    (byte) 110,
    (byte) 99,
    (byte) 81,
    (byte) 151,
    (byte) 14,
    (byte) 83,
    (byte) 73,
    (byte) 52,
    (byte) 132,
    (byte) 248,
    (byte) 59,
    (byte) 186,
    (byte) 39,
    (byte) 142,
    (byte) 106,
    (byte) 149,
    (byte) 145,
    (byte) 53,
    (byte) 127 /*0x7F*/,
    (byte) 38,
    (byte) 237,
    (byte) 95,
    (byte) 197,
    (byte) 35,
    (byte) 25,
    (byte) 175,
    (byte) 119,
    (byte) 62,
    (byte) 176 /*0xB0*/,
    (byte) 146,
    (byte) 192 /*0xC0*/,
    (byte) 98,
    (byte) 201,
    (byte) 78,
    (byte) 61,
    (byte) 108,
    (byte) 79,
    (byte) 33,
    (byte) 2,
    (byte) 161,
    (byte) 225,
    (byte) 42,
    (byte) 251,
    (byte) 20,
    (byte) 28,
    (byte) 183,
    (byte) 188,
    (byte) 179,
    (byte) 204,
    (byte) 51,
    (byte) 108,
    (byte) 219,
    (byte) 160 /*0xA0*/,
    (byte) 88,
    (byte) 223,
    (byte) 73,
    (byte) 112 /*0x70*/,
    (byte) 129,
    (byte) 237,
    (byte) 8,
    (byte) 44,
    (byte) 88,
    (byte) 24,
    (byte) 248,
    (byte) 112 /*0x70*/,
    (byte) 129,
    (byte) 252,
    (byte) 66,
    (byte) 166,
    (byte) 25,
    (byte) 223,
    (byte) 222,
    (byte) 146,
    (byte) 34,
    (byte) 24,
    (byte) 74,
    (byte) 162,
    (byte) 96 /*0x60*/,
    (byte) 194,
    (byte) 214,
    (byte) 48 /*0x30*/,
    (byte) 37,
    (byte) 27,
    (byte) 63 /*0x3F*/,
    (byte) 18,
    (byte) 46,
    (byte) 251,
    (byte) 30,
    (byte) 146,
    (byte) 240 /*0xF0*/,
    (byte) 128 /*0x80*/,
    (byte) 179,
    (byte) 228,
    (byte) 171,
    (byte) 169,
    (byte) 77,
    (byte) 19,
    (byte) 112 /*0x70*/,
    (byte) 179,
    (byte) 121,
    (byte) 70,
    (byte) 182,
    (byte) 81,
    (byte) 175,
    (byte) 115,
    (byte) 172,
    (byte) 214,
    (byte) 183,
    (byte) 186,
    (byte) 218,
    (byte) 24,
    (byte) 29,
    (byte) 0,
    (byte) 207,
    (byte) 78,
    (byte) 195,
    (byte) 104,
    (byte) 209,
    (byte) 19,
    (byte) 164,
    (byte) 48 /*0x30*/,
    (byte) 48 /*0x30*/,
    (byte) 208 /*0xD0*/,
    (byte) 61,
    (byte) 55,
    (byte) 47,
    (byte) 190,
    (byte) 112 /*0x70*/,
    (byte) 69,
    (byte) 115,
    (byte) 111,
    (byte) 25,
    (byte) 139,
    (byte) 144 /*0x90*/,
    (byte) 139,
    (byte) 59,
    (byte) 174,
    (byte) 247,
    (byte) 170,
    (byte) 108,
    (byte) 205,
    (byte) 146,
    (byte) 241,
    (byte) 180,
    (byte) 71,
    (byte) 91,
    (byte) 160 /*0xA0*/,
    (byte) 81,
    (byte) 38,
    (byte) 14,
    (byte) 129,
    (byte) 129,
    (byte) 88,
    (byte) 15,
    (byte) 190,
    (byte) 141,
    (byte) 16 /*0x10*/,
    (byte) 249,
    (byte) 106,
    (byte) 6,
    (byte) 213,
    (byte) 133,
    (byte) 145,
    (byte) 57,
    (byte) 115,
    (byte) 88,
    (byte) 95,
    (byte) 19,
    (byte) 143,
    (byte) 102,
    (byte) 251,
    (byte) 101,
    (byte) 211,
    (byte) 137,
    (byte) 72,
    (byte) 74,
    (byte) 117,
    (byte) 188,
    (byte) 150,
    (byte) 100,
    (byte) 34,
    (byte) 158,
    (byte) 187,
    (byte) 132,
    (byte) 162,
    (byte) 126,
    (byte) 207,
    (byte) 211,
    (byte) 50,
    (byte) 118,
    (byte) 24,
    (byte) 244,
    (byte) 249,
    (byte) 176 /*0xB0*/,
    (byte) 63 /*0x3F*/,
    (byte) 66,
    (byte) 106,
    (byte) 30,
    (byte) 153,
    (byte) 231,
    (byte) 70,
    (byte) 113,
    (byte) 214,
    (byte) 230,
    (byte) 142,
    (byte) 76,
    (byte) 108,
    (byte) 63 /*0x3F*/,
    (byte) 92,
    (byte) 184,
    (byte) 46,
    (byte) 148,
    (byte) 96 /*0x60*/,
    (byte) 154,
    (byte) 104,
    (byte) 48 /*0x30*/,
    (byte) 177,
    (byte) 232,
    (byte) 120,
    (byte) 74,
    (byte) 173,
    (byte) 85,
    (byte) 122,
    (byte) 74,
    (byte) 1,
    (byte) 165,
    (byte) 175,
    (byte) 241,
    (byte) 25,
    (byte) 18,
    (byte) 179,
    (byte) 16 /*0x10*/,
    (byte) 94,
    (byte) 224 /*0xE0*/,
    (byte) 216,
    (byte) 227,
    (byte) 92,
    (byte) 169,
    (byte) 49,
    (byte) 69,
    (byte) 190,
    (byte) 31 /*0x1F*/,
    (byte) 49,
    (byte) 33,
    (byte) 74,
    (byte) 159,
    (byte) 237,
    (byte) 9,
    (byte) 241,
    (byte) 135,
    (byte) 209,
    (byte) 77,
    (byte) 134,
    (byte) 166,
    (byte) 33,
    (byte) 92,
    (byte) 47,
    (byte) 116,
    (byte) 183,
    (byte) 152,
    (byte) 248,
    (byte) 72,
    (byte) 210,
    (byte) 81,
    (byte) 17,
    (byte) 81,
    (byte) 72,
    (byte) 214,
    (byte) 84,
    (byte) 50,
    (byte) 14,
    (byte) 70,
    (byte) 102,
    (byte) 160 /*0xA0*/,
    (byte) 216,
    (byte) 38,
    (byte) 15,
    (byte) 47,
    (byte) 20,
    (byte) 194,
    (byte) 41,
    (byte) 33,
    (byte) 109,
    (byte) 7,
    (byte) 115,
    (byte) 87,
    (byte) 151,
    (byte) 141,
    (byte) 156,
    (byte) 102,
    (byte) 240 /*0xF0*/,
    (byte) 233,
    (byte) 170,
    (byte) 83,
    (byte) 140,
    (byte) 122,
    (byte) 43,
    (byte) 164,
    (byte) 187,
    (byte) 155,
    (byte) 97,
    (byte) 250,
    (byte) 198,
    (byte) 245,
    (byte) 78,
    (byte) 233,
    (byte) 211,
    (byte) 133,
    (byte) 95,
    (byte) 97,
    (byte) 151,
    (byte) 196,
    (byte) 160 /*0xA0*/,
    (byte) 44,
    (byte) 102,
    (byte) 223,
    (byte) 7,
    (byte) 129,
    (byte) 8,
    (byte) 13,
    (byte) 43,
    (byte) 73,
    (byte) 80 /*0x50*/,
    (byte) 73,
    (byte) 41,
    (byte) 159,
    (byte) 254,
    (byte) 168,
    (byte) 15,
    (byte) 117,
    (byte) 33,
    (byte) 228,
    (byte) 176 /*0xB0*/,
    (byte) 39,
    (byte) 217,
    (byte) 28,
    (byte) 241,
    (byte) 84,
    (byte) 199,
    (byte) 79,
    (byte) 75,
    (byte) 82,
    (byte) 186,
    (byte) 239,
    (byte) 250,
    (byte) 190,
    (byte) 92,
    (byte) 126,
    (byte) 153,
    (byte) 241,
    (byte) 88,
    (byte) 107,
    (byte) 12,
    (byte) 167,
    (byte) 35,
    (byte) 65,
    (byte) 134,
    (byte) 182,
    (byte) 17,
    (byte) 32 /*0x20*/,
    (byte) 173,
    (byte) 0,
    (byte) 57,
    (byte) 2,
    (byte) 94,
    (byte) 76,
    (byte) 69,
    (byte) 205,
    (byte) 135,
    (byte) 251,
    (byte) 85,
    (byte) 120,
    (byte) 5,
    (byte) 204,
    (byte) 211,
    (byte) 81,
    (byte) 131,
    (byte) 53,
    (byte) 189,
    (byte) 28,
    (byte) 225,
    (byte) 214,
    (byte) 150,
    (byte) 89,
    (byte) 21,
    (byte) 16 /*0x10*/,
    (byte) 221,
    (byte) 156,
    (byte) 113,
    (byte) 177,
    (byte) 64 /*0x40*/,
    (byte) 114,
    (byte) 134,
    (byte) 35,
    (byte) 90,
    (byte) 46,
    (byte) 24,
    (byte) 239,
    (byte) 201,
    (byte) 191,
    (byte) 63 /*0x3F*/,
    (byte) 178,
    (byte) 203,
    (byte) 188,
    (byte) 195,
    (byte) 203,
    (byte) 187,
    (byte) 93,
    (byte) 203,
    (byte) 48 /*0x30*/,
    (byte) 52,
    (byte) 50,
    (byte) 31 /*0x1F*/,
    (byte) 4,
    (byte) 223,
    (byte) 227,
    (byte) 29,
    (byte) 76,
    (byte) 14,
    (byte) 136,
    (byte) 15,
    (byte) 10,
    (byte) 200,
    (byte) 22,
    (byte) 62,
    (byte) 243,
    (byte) 124,
    (byte) 51,
    (byte) 95,
    (byte) 0,
    (byte) 76,
    (byte) 189,
    (byte) 6,
    (byte) 201,
    (byte) 148,
    (byte) 29,
    (byte) 203,
    (byte) 198,
    (byte) 238,
    (byte) 100,
    (byte) 116,
    (byte) 51,
    (byte) 96 /*0x60*/,
    (byte) 89,
    (byte) 53,
    (byte) 7,
    (byte) 131,
    (byte) 49,
    (byte) 71,
    (byte) 250,
    (byte) 87,
    (byte) 130,
    (byte) 89,
    (byte) 107,
    (byte) 2,
    (byte) 245,
    (byte) 149,
    (byte) 195,
    (byte) 93,
    (byte) 237,
    (byte) 171,
    (byte) 180,
    (byte) 230,
    (byte) 251,
    (byte) 192 /*0xC0*/,
    (byte) 15,
    (byte) 217,
    (byte) 67,
    (byte) 40,
    (byte) 227,
    (byte) 29,
    (byte) 121,
    (byte) 31 /*0x1F*/,
    (byte) 116,
    (byte) 178,
    (byte) 202,
    (byte) 182,
    (byte) 76,
    (byte) 114,
    (byte) 98,
    (byte) 161
  };
  private static byte[] sspr = new byte[536]
  {
    (byte) 232,
    (byte) 210,
    (byte) 118,
    (byte) 139,
    (byte) 93,
    (byte) 99,
    (byte) 55,
    (byte) 2,
    (byte) 253,
    (byte) 130,
    (byte) 71,
    (byte) 174,
    (byte) 31 /*0x1F*/,
    (byte) 190,
    (byte) 203,
    (byte) 44,
    (byte) 144 /*0x90*/,
    (byte) 27,
    (byte) 117,
    (byte) 137,
    (byte) 159,
    (byte) 43,
    (byte) 189,
    (byte) 236,
    (byte) 238,
    (byte) 34,
    (byte) 3,
    (byte) 46,
    (byte) 97,
    (byte) 217,
    (byte) 28,
    (byte) 158,
    (byte) 242,
    (byte) 65,
    (byte) 191,
    (byte) 246,
    (byte) 53,
    (byte) 157,
    (byte) 93,
    (byte) 170,
    (byte) 220,
    (byte) 31 /*0x1F*/,
    (byte) 89,
    (byte) 130,
    (byte) 142,
    (byte) 204,
    (byte) 224 /*0xE0*/,
    (byte) 240 /*0xF0*/,
    (byte) 41,
    (byte) 66,
    (byte) 194,
    (byte) 49,
    (byte) 7,
    (byte) 144 /*0x90*/,
    (byte) 181,
    (byte) 254,
    (byte) 190,
    (byte) 190,
    (byte) 134,
    (byte) 251,
    (byte) 65,
    (byte) 226,
    (byte) 111,
    (byte) 113,
    (byte) 95,
    (byte) 141,
    (byte) 4,
    (byte) 102,
    (byte) 235,
    (byte) 88,
    (byte) 76,
    (byte) 5,
    (byte) 162,
    (byte) 147,
    (byte) 218,
    (byte) 0,
    (byte) 160 /*0xA0*/,
    (byte) 102,
    (byte) 218,
    (byte) 67,
    (byte) 186,
    (byte) 231,
    (byte) 232,
    (byte) 91,
    (byte) 35,
    (byte) 132,
    (byte) 34,
    (byte) 169,
    (byte) 90,
    (byte) 183,
    (byte) 17,
    (byte) 250,
    (byte) 97,
    (byte) 195,
    (byte) 215,
    (byte) 57,
    (byte) 166,
    (byte) 145,
    (byte) 116,
    (byte) 141,
    (byte) 238,
    (byte) 192 /*0xC0*/,
    (byte) 176 /*0xB0*/,
    (byte) 233,
    (byte) 20,
    (byte) 72,
    (byte) 129,
    (byte) 199,
    (byte) 237,
    (byte) 6,
    (byte) 226,
    (byte) 79,
    (byte) 251,
    (byte) 157,
    (byte) 138,
    (byte) 112 /*0x70*/,
    (byte) 13,
    (byte) 157,
    (byte) 188,
    (byte) 190,
    (byte) 41,
    (byte) 196,
    (byte) 33,
    (byte) 74,
    (byte) 26,
    (byte) 1,
    (byte) 173,
    (byte) 30,
    (byte) 53,
    (byte) 18,
    (byte) 141,
    (byte) 76,
    (byte) 210,
    (byte) 109,
    (byte) 140,
    (byte) 35,
    (byte) 68,
    (byte) 152,
    (byte) 32 /*0x20*/,
    (byte) 172,
    (byte) 48 /*0x30*/,
    (byte) 128 /*0x80*/,
    (byte) 39,
    (byte) 72,
    (byte) 234,
    (byte) 189,
    (byte) 17,
    (byte) 82,
    (byte) 247,
    (byte) 20,
    (byte) 184,
    (byte) 159,
    (byte) 109,
    (byte) 226,
    (byte) 152,
    (byte) 177,
    (byte) 199,
    (byte) 132,
    (byte) 173,
    (byte) 15,
    (byte) 127 /*0x7F*/,
    (byte) 143,
    (byte) 103,
    (byte) 177,
    (byte) 148,
    (byte) 161,
    (byte) 213,
    (byte) 180,
    (byte) 59,
    (byte) 231,
    (byte) 22,
    (byte) 202,
    (byte) 226,
    (byte) 254,
    (byte) 148,
    (byte) 248,
    (byte) 107,
    (byte) 188,
    (byte) 145,
    (byte) 115,
    (byte) 224 /*0xE0*/,
    (byte) 158,
    (byte) 48 /*0x30*/,
    (byte) 29,
    (byte) 221,
    (byte) 94,
    (byte) 6,
    (byte) 38,
    (byte) 142,
    (byte) 37,
    (byte) 127 /*0x7F*/,
    (byte) 234,
    (byte) 86,
    (byte) 123,
    (byte) 161,
    (byte) 75,
    (byte) 174,
    (byte) 152,
    (byte) 144 /*0x90*/,
    (byte) 3,
    (byte) 244,
    (byte) 24,
    (byte) 113,
    (byte) 104,
    (byte) 192 /*0xC0*/,
    (byte) 56,
    (byte) 228,
    (byte) 153,
    (byte) 206,
    (byte) 143,
    (byte) 230,
    (byte) 130,
    (byte) 24,
    (byte) 133,
    (byte) 184,
    (byte) 159,
    (byte) 30,
    (byte) 33,
    (byte) 179,
    (byte) 188,
    (byte) 34,
    (byte) 245,
    (byte) 163,
    (byte) 242,
    (byte) 208 /*0xD0*/,
    (byte) 27,
    (byte) 250,
    (byte) 170,
    (byte) 134,
    (byte) 236,
    (byte) 188,
    (byte) 61,
    (byte) 34,
    (byte) 27,
    (byte) 37,
    (byte) 169,
    (byte) 153,
    (byte) 95,
    (byte) 233,
    (byte) 162,
    (byte) 188,
    (byte) 212,
    (byte) 99,
    (byte) 226,
    (byte) 178,
    (byte) 50,
    (byte) 233,
    (byte) 165,
    (byte) 104,
    (byte) 45,
    (byte) 52,
    (byte) 157,
    (byte) 158,
    (byte) 89,
    (byte) 158,
    (byte) 13,
    (byte) 232,
    (byte) 148,
    (byte) 91,
    (byte) 242,
    (byte) 133,
    (byte) 51,
    (byte) 24,
    (byte) 217,
    (byte) 227,
    (byte) 138,
    (byte) 92,
    (byte) 241,
    (byte) 145,
    (byte) 90,
    (byte) 92,
    (byte) 0,
    (byte) 21,
    (byte) 201,
    (byte) 241,
    (byte) 14,
    (byte) 18,
    (byte) 176 /*0xB0*/,
    (byte) 154,
    (byte) 181,
    (byte) 188,
    (byte) 41,
    (byte) 31 /*0x1F*/,
    (byte) 27,
    (byte) 199,
    (byte) 155,
    (byte) 226,
    (byte) 10,
    (byte) 70,
    (byte) 111,
    (byte) 156,
    (byte) 46,
    (byte) 55,
    (byte) 57,
    (byte) 103,
    (byte) 101,
    (byte) 206,
    (byte) 245,
    (byte) 127 /*0x7F*/,
    (byte) 60,
    (byte) 82,
    (byte) 110,
    (byte) 197,
    (byte) 179,
    (byte) 61,
    (byte) 168,
    (byte) 179,
    (byte) 244,
    (byte) 92,
    (byte) 237,
    (byte) 98,
    (byte) 97,
    (byte) 92,
    (byte) 241,
    (byte) 195,
    (byte) 54,
    (byte) 237,
    (byte) 122,
    (byte) 143,
    (byte) 148,
    (byte) 30,
    (byte) 5,
    (byte) 110,
    (byte) 19,
    (byte) 107,
    (byte) 128 /*0x80*/,
    (byte) 34,
    (byte) 91,
    (byte) 157,
    (byte) 24,
    (byte) 156,
    (byte) 31 /*0x1F*/,
    (byte) 233,
    (byte) 178,
    (byte) 173,
    (byte) 188,
    (byte) 125,
    (byte) 92,
    (byte) 156,
    (byte) 11,
    (byte) 167,
    (byte) 185,
    (byte) 203,
    (byte) 137,
    (byte) 160 /*0xA0*/,
    (byte) 77,
    (byte) 44,
    (byte) 100,
    (byte) 73,
    (byte) 42,
    (byte) 165,
    (byte) 250,
    (byte) 45,
    (byte) 131,
    (byte) 138,
    (byte) 188,
    (byte) 228,
    (byte) 89,
    (byte) 18,
    (byte) 228,
    (byte) 92,
    (byte) 52,
    (byte) 100,
    (byte) 240 /*0xF0*/,
    (byte) 99,
    (byte) 93,
    (byte) 80 /*0x50*/,
    (byte) 88,
    (byte) 241,
    (byte) 60,
    (byte) 121,
    (byte) 106,
    (byte) 136,
    (byte) 224 /*0xE0*/,
    (byte) 77,
    (byte) 241,
    (byte) 163,
    (byte) 52,
    (byte) 151,
    (byte) 82,
    (byte) 123,
    (byte) 211,
    (byte) 87,
    (byte) 183,
    (byte) 185,
    (byte) 235,
    (byte) 46,
    (byte) 191,
    (byte) 41,
    (byte) 94,
    (byte) 186,
    (byte) 122,
    (byte) 86,
    (byte) 24,
    (byte) 71,
    (byte) 51,
    (byte) 142,
    (byte) 142,
    (byte) 81,
    (byte) 230,
    (byte) 34,
    (byte) 0,
    (byte) 138,
    (byte) 157,
    (byte) 46,
    (byte) 46,
    (byte) 175,
    (byte) 136,
    (byte) 44,
    (byte) 137,
    (byte) 12,
    (byte) 120,
    (byte) 151,
    (byte) 221,
    (byte) 175,
    (byte) 161,
    (byte) 145,
    (byte) 183,
    (byte) 242,
    (byte) 37,
    (byte) 112 /*0x70*/,
    (byte) 131,
    (byte) 142,
    (byte) 143,
    (byte) 142,
    (byte) 118,
    (byte) 88,
    (byte) 165,
    (byte) 32 /*0x20*/,
    (byte) 163,
    (byte) 36,
    (byte) 6,
    (byte) 20,
    (byte) 227,
    (byte) 62,
    (byte) 31 /*0x1F*/,
    (byte) 76,
    (byte) 163,
    (byte) 37,
    (byte) 182,
    (byte) 46,
    (byte) 203,
    (byte) 227,
    (byte) 90,
    (byte) 218,
    (byte) 236,
    (byte) 124,
    (byte) 147,
    (byte) 249,
    (byte) 179,
    (byte) 81,
    (byte) 231,
    (byte) 59,
    (byte) 246,
    (byte) 93,
    (byte) 20,
    (byte) 209,
    (byte) 188,
    (byte) 242,
    (byte) 182,
    (byte) 246,
    (byte) 81,
    (byte) 46,
    (byte) 26,
    (byte) 134,
    (byte) 134,
    (byte) 7,
    (byte) 70,
    (byte) 60,
    (byte) 218,
    (byte) 4,
    (byte) 142,
    (byte) 231,
    (byte) 122,
    (byte) 156,
    (byte) 100,
    (byte) 173,
    (byte) 121,
    (byte) 214,
    (byte) 168,
    (byte) 106,
    (byte) 180,
    (byte) 12,
    (byte) 181,
    (byte) 115,
    (byte) 233,
    (byte) 121,
    (byte) 59,
    (byte) 214,
    (byte) 72,
    (byte) 85,
    (byte) 204,
    (byte) 242,
    (byte) 166,
    (byte) 193,
    (byte) 225,
    (byte) 221,
    (byte) 182,
    (byte) 158,
    (byte) 192 /*0xC0*/,
    (byte) 202,
    (byte) 253,
    (byte) 110,
    (byte) 239,
    (byte) 241,
    (byte) 54,
    (byte) 192 /*0xC0*/,
    (byte) 209,
    (byte) 52,
    (byte) 136,
    (byte) 41,
    (byte) 152,
    (byte) 66,
    (byte) 72,
    (byte) 221,
    (byte) 82,
    (byte) 110,
    (byte) 119,
    (byte) 9,
    (byte) 19,
    (byte) 95,
    (byte) 162,
    (byte) 245,
    (byte) 217,
    (byte) 220,
    (byte) 45,
    (byte) 82,
    (byte) 34,
    (byte) 83,
    (byte) 113,
    (byte) 175,
    (byte) 108,
    (byte) 249,
    (byte) 85,
    (byte) 62,
    (byte) 114
  };

  internal static int ssp_appserver_13211(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[36] = (byte) 1;
    sourceArray1[1] = (byte) 119;
    sourceArray1[2] = (byte) 134;
    sourceArray1[9] = (byte) 37;
    sourceArray1[4] = (byte) 162;
    sourceArray1[18] = (byte) 213;
    sourceArray1[6] = (byte) 3;
    sourceArray1[25] = (byte) 188;
    sourceArray1[30] = (byte) 74;
    sourceArray1[5] = (byte) 130;
    sourceArray1[13] = (byte) 139;
    sourceArray1[11] = (byte) 106;
    sourceArray1[42] = (byte) 0;
    sourceArray1[7] = (byte) 22;
    sourceArray1[45] = (byte) 199;
    sourceArray1[26] = (byte) 25;
    sourceArray1[16 /*0x10*/] = (byte) 42;
    sourceArray1[17] = (byte) 215;
    sourceArray1[43] = (byte) 208 /*0xD0*/;
    sourceArray1[19] = (byte) 223;
    sourceArray1[20] = (byte) 165;
    sourceArray1[21] = (byte) 131;
    sourceArray1[14] = (byte) 202;
    sourceArray1[15] = (byte) 115;
    sourceArray1[24] = (byte) 100;
    sourceArray1[34] = (byte) 111;
    sourceArray1[47] = (byte) 124;
    sourceArray1[27] = (byte) 126;
    sourceArray1[44] = (byte) 231;
    sourceArray1[29] = (byte) 207;
    sourceArray1[3] = (byte) 1;
    sourceArray1[23] = (byte) 178;
    sourceArray1[12] = (byte) 5;
    sourceArray1[37] = (byte) 174;
    sourceArray1[32 /*0x20*/] = (byte) 39;
    sourceArray1[35] = (byte) 206;
    sourceArray1[31 /*0x1F*/] = (byte) 181;
    sourceArray1[8] = (byte) 227;
    sourceArray1[33] = (byte) 215;
    sourceArray1[39] = (byte) 128 /*0x80*/;
    sourceArray1[40] = (byte) 96 /*0x60*/;
    sourceArray1[38] = (byte) 31 /*0x1F*/;
    sourceArray1[0] = (byte) 92;
    sourceArray1[10] = (byte) 195;
    sourceArray1[22] = (byte) 223;
    sourceArray1[41] = (byte) 223;
    sourceArray1[46] = (byte) 186;
    sourceArray1[28] = (byte) 116;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[34] = (byte) 167;
    sourceArray2[7] = (byte) 90;
    sourceArray2[2] = (byte) 167;
    sourceArray2[1] = (byte) 154;
    sourceArray2[4] = (byte) 128 /*0x80*/;
    sourceArray2[47] = (byte) 60;
    sourceArray2[32 /*0x20*/] = (byte) 62;
    sourceArray2[3] = (byte) 115;
    sourceArray2[12] = (byte) 185;
    sourceArray2[9] = (byte) 180;
    sourceArray2[15] = (byte) 186;
    sourceArray2[11] = (byte) 175;
    sourceArray2[33] = (byte) 78;
    sourceArray2[13] = (byte) 84;
    sourceArray2[14] = (byte) 60;
    sourceArray2[39] = (byte) 63 /*0x3F*/;
    sourceArray2[25] = (byte) 100;
    sourceArray2[17] = (byte) 121;
    sourceArray2[19] = (byte) 160 /*0xA0*/;
    sourceArray2[31 /*0x1F*/] = (byte) 45;
    sourceArray2[20] = (byte) 42;
    sourceArray2[21] = (byte) 218;
    sourceArray2[22] = (byte) 88;
    sourceArray2[23] = (byte) 147;
    sourceArray2[24] = (byte) 193;
    sourceArray2[5] = (byte) 205;
    sourceArray2[35] = (byte) 213;
    sourceArray2[27] = (byte) 218;
    sourceArray2[28] = (byte) 188;
    sourceArray2[40] = (byte) 175;
    sourceArray2[18] = (byte) 9;
    sourceArray2[0] = (byte) 86;
    sourceArray2[30] = (byte) 161;
    sourceArray2[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    sourceArray2[8] = (byte) 20;
    sourceArray2[10] = (byte) 246;
    sourceArray2[29] = (byte) 115;
    sourceArray2[37] = (byte) 34;
    sourceArray2[38] = (byte) 53;
    sourceArray2[6] = (byte) 224 /*0xE0*/;
    sourceArray2[26] = (byte) 87;
    sourceArray2[41] = (byte) 129;
    sourceArray2[42] = (byte) 61;
    sourceArray2[43] = (byte) 103;
    sourceArray2[44] = (byte) 33;
    sourceArray2[45] = (byte) 8;
    sourceArray2[46] = (byte) 226;
    sourceArray2[36] = (byte) 16 /*0x10*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13212(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 76,
      (byte) 157,
      (byte) 103,
      (byte) 197,
      (byte) 244,
      (byte) 91,
      (byte) 200,
      (byte) 228,
      (byte) 218,
      (byte) 118,
      (byte) 239,
      (byte) 254,
      (byte) 57,
      (byte) 150,
      (byte) 125,
      (byte) 5,
      (byte) 190,
      (byte) 112 /*0x70*/,
      (byte) 24,
      (byte) 229,
      (byte) 112 /*0x70*/,
      (byte) 143,
      (byte) 51,
      (byte) 64 /*0x40*/,
      (byte) 161,
      (byte) 223,
      (byte) 194,
      (byte) 83,
      (byte) 177,
      (byte) 68,
      (byte) 24,
      (byte) 34,
      (byte) 85,
      (byte) 42,
      (byte) 239,
      (byte) 5,
      (byte) 32 /*0x20*/,
      (byte) 208 /*0xD0*/,
      (byte) 36,
      (byte) 149,
      (byte) 81,
      (byte) 143,
      (byte) 98,
      (byte) 148,
      (byte) 166,
      (byte) 134,
      (byte) 231,
      (byte) 38
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 193,
      (byte) 38,
      (byte) 97,
      (byte) 202,
      (byte) 252,
      (byte) 67,
      (byte) 213,
      (byte) 120,
      (byte) 202,
      (byte) 8,
      (byte) 186,
      (byte) 124,
      (byte) 137,
      (byte) 92,
      (byte) 12,
      (byte) 6,
      (byte) 55,
      (byte) 239,
      (byte) 114,
      (byte) 48 /*0x30*/,
      (byte) 167,
      (byte) 233,
      (byte) 129,
      (byte) 37,
      (byte) 25,
      (byte) 191,
      (byte) 136,
      (byte) 170,
      (byte) 194,
      (byte) 251,
      (byte) 133,
      (byte) 230,
      (byte) 250,
      (byte) 125,
      (byte) 80 /*0x50*/,
      (byte) 148,
      (byte) 100,
      (byte) 168,
      (byte) 89,
      (byte) 193,
      (byte) 26,
      (byte) 97,
      (byte) 44,
      (byte) 99,
      (byte) 92,
      (byte) 15,
      (byte) 20,
      (byte) 117
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13213(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 45,
      (byte) 227,
      (byte) 189,
      (byte) 105,
      (byte) 39,
      (byte) 36,
      (byte) 20,
      (byte) 152,
      (byte) 117,
      (byte) 51,
      (byte) 150,
      (byte) 93,
      (byte) 205,
      (byte) 168,
      (byte) 15,
      (byte) 166,
      (byte) 120,
      (byte) 228,
      (byte) 237,
      (byte) 75,
      (byte) 131,
      (byte) 148,
      (byte) 225,
      (byte) 44,
      (byte) 159,
      (byte) 154,
      (byte) 27,
      (byte) 247,
      (byte) 155,
      (byte) 105,
      (byte) 191,
      (byte) 193,
      (byte) 220,
      (byte) 23,
      (byte) 40,
      (byte) 70,
      (byte) 50,
      (byte) 156,
      (byte) 249,
      (byte) 172,
      (byte) 16 /*0x10*/,
      (byte) 252,
      (byte) 219,
      (byte) 91,
      (byte) 204,
      (byte) 54,
      (byte) 216,
      (byte) 222
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 160 /*0xA0*/,
      (byte) 98,
      (byte) 85,
      (byte) 168,
      (byte) 233,
      (byte) 135,
      (byte) 14,
      (byte) 163,
      (byte) 106,
      (byte) 70,
      (byte) 133,
      (byte) 228,
      (byte) 245,
      (byte) 124,
      (byte) 135,
      (byte) 91,
      (byte) 44,
      (byte) 244,
      (byte) 7,
      (byte) 152,
      (byte) 245,
      (byte) 196,
      (byte) 238,
      (byte) 233,
      (byte) 96 /*0x60*/,
      (byte) 51,
      (byte) 48 /*0x30*/,
      (byte) 29,
      (byte) 94,
      (byte) 14,
      (byte) 229,
      (byte) 59,
      (byte) 60,
      (byte) 26,
      (byte) 106,
      (byte) 159,
      (byte) 72,
      (byte) 116,
      (byte) 116,
      (byte) 77,
      (byte) 198,
      (byte) 144 /*0x90*/,
      (byte) 123,
      (byte) 253,
      (byte) 218,
      (byte) 23,
      (byte) 182,
      (byte) 128 /*0x80*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[37];
    byte[] response2 = new byte[37];
    Array.Copy((Array) sc_13210.sspq, 0, (Array) numArray2, 0, 37);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13210.sspr, 0, (Array) numArray2, 0, 37);
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

  internal static int ssp_appserver_13214(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 161,
      (byte) 209,
      (byte) 108,
      (byte) 163,
      (byte) 243,
      byte.MaxValue,
      (byte) 207,
      (byte) 232,
      (byte) 247,
      (byte) 182,
      (byte) 34,
      (byte) 93,
      (byte) 15,
      (byte) 11,
      (byte) 149,
      (byte) 34,
      (byte) 84,
      (byte) 180,
      (byte) 1,
      (byte) 225,
      (byte) 123,
      (byte) 47,
      (byte) 97,
      (byte) 203,
      (byte) 204,
      (byte) 253,
      (byte) 225,
      (byte) 20,
      (byte) 193,
      (byte) 167,
      (byte) 147,
      (byte) 38,
      (byte) 242,
      (byte) 131,
      (byte) 210,
      (byte) 10,
      (byte) 126,
      (byte) 157,
      (byte) 72,
      (byte) 134,
      (byte) 232,
      (byte) 157,
      (byte) 105,
      (byte) 178,
      (byte) 158,
      (byte) 223,
      (byte) 15,
      (byte) 15
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 111,
      (byte) 35,
      (byte) 97,
      (byte) 119,
      (byte) 132,
      (byte) 201,
      (byte) 82,
      (byte) 107,
      (byte) 246,
      (byte) 36,
      (byte) 145,
      (byte) 37,
      (byte) 10,
      (byte) 146,
      (byte) 92,
      (byte) 50,
      (byte) 151,
      (byte) 149,
      (byte) 106,
      (byte) 31 /*0x1F*/,
      (byte) 246,
      (byte) 246,
      (byte) 69,
      (byte) 17,
      (byte) 229,
      (byte) 213,
      (byte) 137,
      (byte) 184,
      (byte) 168,
      (byte) 38,
      (byte) 95,
      (byte) 144 /*0x90*/,
      (byte) 125,
      (byte) 14,
      (byte) 232,
      (byte) 228,
      (byte) 227,
      (byte) 246,
      (byte) 59,
      (byte) 183,
      (byte) 185,
      (byte) 188,
      (byte) 213,
      (byte) 238,
      (byte) 143,
      (byte) 101,
      (byte) 37,
      (byte) 122
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13215()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[4] = (byte) 112 /*0x70*/;
      numArray2[1] = (byte) 172;
      numArray2[2] = (byte) 201;
      numArray2[8] = (byte) 29;
      numArray2[3] = (byte) 114;
      numArray2[5] = (byte) 160 /*0xA0*/;
      numArray2[13] = (byte) 251;
      numArray2[34] = (byte) 153;
      numArray2[32 /*0x20*/] = (byte) 206;
      numArray2[16 /*0x10*/] = (byte) 57;
      numArray2[24] = (byte) 198;
      numArray2[31 /*0x1F*/] = (byte) 50;
      numArray2[23] = (byte) 221;
      numArray2[26] = (byte) 44;
      numArray2[33] = (byte) 90;
      numArray2[30] = (byte) 179;
      numArray2[12] = (byte) 34;
      numArray2[0] = (byte) 141;
      numArray2[18] = (byte) 164;
      numArray2[19] = (byte) 183;
      numArray2[9] = (byte) 186;
      numArray2[7] = (byte) 246;
      numArray2[22] = (byte) 248;
      numArray2[35] = (byte) 69;
      numArray2[6] = (byte) 215;
      numArray2[25] = (byte) 175;
      numArray2[21] = (byte) 123;
      numArray2[27] = (byte) 236;
      numArray2[15] = (byte) 140;
      numArray2[29] = (byte) 210;
      numArray2[28] = (byte) 237;
      numArray2[14] = (byte) 107;
      numArray2[20] = (byte) 107;
      numArray2[11] = (byte) 80 /*0x50*/;
      numArray2[17] = (byte) 139;
      numArray2[10] = (byte) 38;
      byte[] numArray3 = new byte[36]
      {
        (byte) 100,
        (byte) 217,
        (byte) 30,
        (byte) 59,
        (byte) 150,
        (byte) 38,
        (byte) 146,
        (byte) 177,
        (byte) 178,
        (byte) 23,
        (byte) 131,
        (byte) 214,
        (byte) 219,
        (byte) 224 /*0xE0*/,
        (byte) 123,
        (byte) 180,
        (byte) 16 /*0x10*/,
        (byte) 38,
        (byte) 47,
        (byte) 114,
        (byte) 29,
        (byte) 20,
        (byte) 102,
        (byte) 84,
        (byte) 103,
        (byte) 109,
        (byte) 122,
        (byte) 183,
        (byte) 136,
        (byte) 232,
        (byte) 21,
        (byte) 27,
        (byte) 113,
        (byte) 91,
        (byte) 211,
        (byte) 167
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
      (byte) 25,
      (byte) 18,
      (byte) 203,
      (byte) 102,
      (byte) 174,
      (byte) 31 /*0x1F*/,
      (byte) 137,
      (byte) 120,
      (byte) 106,
      (byte) 252,
      (byte) 221,
      (byte) 72,
      (byte) 172,
      (byte) 141,
      (byte) 135,
      (byte) 145,
      (byte) 205,
      (byte) 180,
      (byte) 216,
      (byte) 105,
      (byte) 218,
      (byte) 73,
      (byte) 29,
      (byte) 38,
      (byte) 131,
      (byte) 149,
      (byte) 199,
      (byte) 65,
      (byte) 77,
      (byte) 176 /*0xB0*/,
      (byte) 253,
      (byte) 228,
      (byte) 34,
      (byte) 48 /*0x30*/,
      (byte) 178,
      byte.MaxValue
    };
    byte[] numArray6 = new byte[36];
    numArray6[18] = (byte) 215;
    numArray6[1] = (byte) 169;
    numArray6[23] = (byte) 116;
    numArray6[3] = (byte) 38;
    numArray6[22] = (byte) 236;
    numArray6[25] = (byte) 239;
    numArray6[2] = (byte) 165;
    numArray6[7] = (byte) 33;
    numArray6[8] = (byte) 232;
    numArray6[9] = (byte) 24;
    numArray6[10] = (byte) 254;
    numArray6[26] = (byte) 54;
    numArray6[12] = (byte) 122;
    numArray6[16 /*0x10*/] = (byte) 34;
    numArray6[30] = (byte) 80 /*0x50*/;
    numArray6[14] = (byte) 103;
    numArray6[13] = (byte) 224 /*0xE0*/;
    numArray6[4] = (byte) 247;
    numArray6[0] = (byte) 160 /*0xA0*/;
    numArray6[19] = (byte) 116;
    numArray6[5] = (byte) 153;
    numArray6[15] = (byte) 186;
    numArray6[21] = (byte) 166;
    numArray6[32 /*0x20*/] = (byte) 28;
    numArray6[35] = (byte) 219;
    numArray6[11] = (byte) 52;
    numArray6[24] = (byte) 254;
    numArray6[27] = (byte) 101;
    numArray6[28] = (byte) 80 /*0x50*/;
    numArray6[29] = (byte) 124;
    numArray6[17] = (byte) 240 /*0xF0*/;
    numArray6[31 /*0x1F*/] = (byte) 165;
    numArray6[6] = (byte) 197;
    numArray6[33] = (byte) 12;
    numArray6[20] = (byte) 250;
    numArray6[34] = (byte) 17;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13216()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 171,
        (byte) 206,
        (byte) 252,
        (byte) 216,
        (byte) 191,
        (byte) 141,
        (byte) 101,
        (byte) 184,
        (byte) 172,
        (byte) 217,
        (byte) 203,
        (byte) 196,
        (byte) 25,
        (byte) 226,
        (byte) 10,
        (byte) 68,
        (byte) 163,
        (byte) 228,
        (byte) 74
      };
      byte[] numArray3 = new byte[19];
      numArray3[7] = (byte) 190;
      numArray3[3] = (byte) 139;
      numArray3[10] = (byte) 233;
      numArray3[11] = (byte) 169;
      numArray3[4] = (byte) 237;
      numArray3[5] = (byte) 28;
      numArray3[6] = (byte) 210;
      numArray3[0] = (byte) 70;
      numArray3[8] = (byte) 202;
      numArray3[12] = (byte) 92;
      numArray3[2] = (byte) 66;
      numArray3[17] = (byte) 174;
      numArray3[15] = (byte) 225;
      numArray3[1] = (byte) 74;
      numArray3[16 /*0x10*/] = (byte) 25;
      numArray3[14] = (byte) 7;
      numArray3[9] = (byte) 98;
      numArray3[13] = (byte) 200;
      numArray3[18] = (byte) 60;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[16 /*0x10*/] = (byte) 204;
    numArray5[1] = (byte) 13;
    numArray5[4] = (byte) 97;
    numArray5[3] = (byte) 46;
    numArray5[12] = (byte) 220;
    numArray5[5] = (byte) 98;
    numArray5[6] = (byte) 248;
    numArray5[7] = (byte) 125;
    numArray5[2] = (byte) 38;
    numArray5[9] = (byte) 164;
    numArray5[11] = (byte) 235;
    numArray5[13] = (byte) 33;
    numArray5[14] = (byte) 199;
    numArray5[0] = (byte) 114;
    numArray5[18] = (byte) 101;
    numArray5[15] = (byte) 127 /*0x7F*/;
    numArray5[8] = (byte) 99;
    numArray5[17] = (byte) 82;
    numArray5[10] = (byte) 210;
    byte[] numArray6 = new byte[19];
    numArray6[18] = (byte) 235;
    numArray6[14] = (byte) 95;
    numArray6[2] = (byte) 51;
    numArray6[8] = (byte) 183;
    numArray6[4] = (byte) 132;
    numArray6[5] = (byte) 210;
    numArray6[6] = (byte) 9;
    numArray6[12] = (byte) 248;
    numArray6[3] = (byte) 44;
    numArray6[0] = (byte) 206;
    numArray6[10] = (byte) 185;
    numArray6[11] = (byte) 244;
    numArray6[1] = (byte) 18;
    numArray6[13] = (byte) 122;
    numArray6[16 /*0x10*/] = (byte) 57;
    numArray6[15] = (byte) 21;
    numArray6[7] = (byte) 30;
    numArray6[17] = (byte) 98;
    numArray6[9] = (byte) 38;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13217()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 28;
      numArray2[7] = (byte) 77;
      numArray2[0] = (byte) 59;
      numArray2[3] = (byte) 11;
      numArray2[2] = (byte) 199;
      numArray2[9] = (byte) 86;
      numArray2[4] = (byte) 71;
      numArray2[5] = (byte) 105;
      numArray2[8] = (byte) 240 /*0xF0*/;
      numArray2[1] = (byte) 153;
      byte[] numArray3 = new byte[10];
      numArray3[2] = (byte) 234;
      numArray3[1] = (byte) 16 /*0x10*/;
      numArray3[0] = (byte) 36;
      numArray3[5] = (byte) 46;
      numArray3[4] = (byte) 53;
      numArray3[7] = (byte) 124;
      numArray3[6] = (byte) 6;
      numArray3[3] = (byte) 227;
      numArray3[8] = (byte) 46;
      numArray3[9] = (byte) 212;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 1,
      (byte) 43,
      (byte) 60,
      (byte) 193,
      (byte) 12,
      (byte) 82,
      (byte) 141,
      (byte) 230,
      (byte) 62,
      (byte) 109
    };
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 16 /*0x10*/;
    numArray6[0] = (byte) 171;
    numArray6[3] = (byte) 102;
    numArray6[5] = (byte) 78;
    numArray6[4] = (byte) 199;
    numArray6[6] = (byte) 55;
    numArray6[7] = (byte) 155;
    numArray6[8] = (byte) 74;
    numArray6[2] = (byte) 87;
    numArray6[9] = (byte) 229;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13218()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33];
      numArray2[19] = (byte) 242;
      numArray2[2] = (byte) 174;
      numArray2[3] = (byte) 130;
      numArray2[28] = (byte) 135;
      numArray2[4] = (byte) 45;
      numArray2[5] = (byte) 30;
      numArray2[6] = (byte) 55;
      numArray2[26] = (byte) 204;
      numArray2[8] = (byte) 15;
      numArray2[9] = (byte) 132;
      numArray2[30] = (byte) 13;
      numArray2[24] = (byte) 21;
      numArray2[11] = (byte) 157;
      numArray2[32 /*0x20*/] = (byte) 79;
      numArray2[14] = (byte) 202;
      numArray2[7] = (byte) 231;
      numArray2[16 /*0x10*/] = (byte) 100;
      numArray2[10] = (byte) 108;
      numArray2[18] = (byte) 161;
      numArray2[15] = (byte) 127 /*0x7F*/;
      numArray2[13] = (byte) 129;
      numArray2[21] = (byte) 175;
      numArray2[20] = (byte) 236;
      numArray2[23] = (byte) 18;
      numArray2[12] = (byte) 254;
      numArray2[25] = (byte) 90;
      numArray2[29] = (byte) 103;
      numArray2[27] = (byte) 114;
      numArray2[17] = (byte) 250;
      numArray2[31 /*0x1F*/] = (byte) 235;
      numArray2[0] = (byte) 6;
      numArray2[22] = (byte) 85;
      numArray2[1] = (byte) 43;
      byte[] numArray3 = new byte[33]
      {
        (byte) 158,
        (byte) 51,
        (byte) 170,
        (byte) 190,
        (byte) 108,
        (byte) 123,
        (byte) 195,
        (byte) 239,
        (byte) 107,
        (byte) 169,
        (byte) 66,
        (byte) 195,
        (byte) 168,
        (byte) 95,
        (byte) 160 /*0xA0*/,
        (byte) 114,
        (byte) 77,
        (byte) 115,
        (byte) 123,
        (byte) 179,
        (byte) 2,
        (byte) 95,
        (byte) 52,
        (byte) 252,
        (byte) 234,
        (byte) 195,
        (byte) 52,
        (byte) 165,
        (byte) 65,
        (byte) 243,
        (byte) 97,
        (byte) 0,
        (byte) 131
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[20];
      byte[] response = new byte[20];
      Array.Copy((Array) sc_13210.sspq, 37, (Array) numArray4, 0, 20);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 37, (Array) numArray4, 0, 20);
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
    byte[] numArray5 = new byte[33];
    byte[] numArray6 = new byte[33]
    {
      (byte) 10,
      (byte) 7,
      (byte) 158,
      (byte) 230,
      (byte) 206,
      (byte) 113,
      (byte) 7,
      (byte) 89,
      (byte) 143,
      (byte) 151,
      (byte) 230,
      (byte) 118,
      (byte) 26,
      (byte) 145,
      (byte) 140,
      (byte) 150,
      (byte) 95,
      (byte) 244,
      (byte) 45,
      (byte) 237,
      (byte) 168,
      (byte) 136,
      (byte) 162,
      (byte) 138,
      (byte) 13,
      (byte) 103,
      (byte) 206,
      (byte) 65,
      (byte) 48 /*0x30*/,
      (byte) 10,
      (byte) 11,
      (byte) 231,
      (byte) 176 /*0xB0*/
    };
    byte[] numArray7 = new byte[33]
    {
      (byte) 178,
      (byte) 55,
      (byte) 7,
      (byte) 239,
      (byte) 203,
      (byte) 35,
      (byte) 75,
      (byte) 67,
      (byte) 50,
      (byte) 249,
      (byte) 43,
      (byte) 118,
      (byte) 227,
      (byte) 220,
      (byte) 26,
      (byte) 254,
      (byte) 137,
      (byte) 100,
      (byte) 209,
      (byte) 142,
      (byte) 83,
      (byte) 59,
      (byte) 166,
      (byte) 179,
      (byte) 245,
      (byte) 153,
      (byte) 233,
      (byte) 117,
      (byte) 181,
      (byte) 68,
      (byte) 122,
      (byte) 100,
      (byte) 196
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13219()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[10] = (byte) 100;
      numArray2[1] = (byte) 177;
      numArray2[2] = (byte) 74;
      numArray2[3] = (byte) 124;
      numArray2[4] = (byte) 85;
      numArray2[8] = (byte) 78;
      numArray2[6] = (byte) 98;
      numArray2[7] = (byte) 250;
      numArray2[9] = (byte) 208 /*0xD0*/;
      numArray2[13] = (byte) 12;
      numArray2[14] = (byte) 129;
      numArray2[16 /*0x10*/] = (byte) 129;
      numArray2[12] = (byte) 224 /*0xE0*/;
      numArray2[5] = (byte) 191;
      numArray2[11] = (byte) 146;
      numArray2[15] = (byte) 57;
      numArray2[0] = (byte) 90;
      numArray2[17] = (byte) 186;
      numArray2[18] = (byte) 130;
      byte[] numArray3 = new byte[19]
      {
        (byte) 156,
        (byte) 189,
        (byte) 141,
        (byte) 136,
        (byte) 75,
        (byte) 57,
        (byte) 22,
        (byte) 231,
        (byte) 10,
        (byte) 30,
        (byte) 218,
        (byte) 34,
        (byte) 189,
        (byte) 118,
        (byte) 24,
        (byte) 8,
        (byte) 250,
        (byte) 247,
        (byte) 254
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
      (byte) 144 /*0x90*/,
      (byte) 190,
      (byte) 150,
      (byte) 105,
      (byte) 175,
      (byte) 79,
      (byte) 253,
      (byte) 137,
      (byte) 174,
      (byte) 46,
      (byte) 168,
      (byte) 94,
      (byte) 111,
      (byte) 167,
      (byte) 128 /*0x80*/,
      (byte) 74,
      (byte) 167,
      (byte) 182,
      (byte) 44
    };
    byte[] numArray6 = new byte[19];
    numArray6[16 /*0x10*/] = (byte) 81;
    numArray6[10] = (byte) 108;
    numArray6[14] = (byte) 122;
    numArray6[3] = (byte) 162;
    numArray6[4] = (byte) 163;
    numArray6[6] = (byte) 114;
    numArray6[9] = (byte) 68;
    numArray6[0] = (byte) 29;
    numArray6[8] = (byte) 159;
    numArray6[12] = (byte) 232;
    numArray6[5] = (byte) 146;
    numArray6[11] = (byte) 134;
    numArray6[17] = (byte) 59;
    numArray6[1] = (byte) 173;
    numArray6[13] = (byte) 197;
    numArray6[15] = (byte) 119;
    numArray6[7] = (byte) 123;
    numArray6[2] = (byte) 125;
    numArray6[18] = (byte) 171;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[14];
    byte[] response = new byte[14];
    Array.Copy((Array) sc_13210.sspq, 57, (Array) numArray7, 0, 14);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13210.sspr, 57, (Array) numArray7, 0, 14);
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

  internal static string ssp_appserver_13220()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 125,
        (byte) 182,
        (byte) 213,
        (byte) 74,
        (byte) 246,
        (byte) 73,
        (byte) 87,
        (byte) 29,
        (byte) 65,
        (byte) 164,
        (byte) 75,
        (byte) 35
      };
      byte[] numArray3 = new byte[12];
      numArray3[4] = (byte) 209;
      numArray3[1] = (byte) 194;
      numArray3[2] = (byte) 209;
      numArray3[9] = (byte) 72;
      numArray3[0] = (byte) 102;
      numArray3[11] = (byte) 219;
      numArray3[3] = (byte) 66;
      numArray3[6] = (byte) 92;
      numArray3[8] = (byte) 122;
      numArray3[5] = (byte) 53;
      numArray3[10] = (byte) 121;
      numArray3[7] = (byte) 65;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 26,
      (byte) 87,
      (byte) 64 /*0x40*/,
      (byte) 42,
      (byte) 185,
      (byte) 238,
      (byte) 229,
      (byte) 83,
      (byte) 129,
      (byte) 64 /*0x40*/,
      (byte) 142,
      (byte) 144 /*0x90*/
    };
    byte[] numArray6 = new byte[12];
    numArray6[10] = (byte) 246;
    numArray6[0] = (byte) 112 /*0x70*/;
    numArray6[7] = (byte) 169;
    numArray6[3] = (byte) 193;
    numArray6[4] = (byte) 201;
    numArray6[5] = (byte) 206;
    numArray6[2] = (byte) 196;
    numArray6[8] = (byte) 171;
    numArray6[6] = (byte) 48 /*0x30*/;
    numArray6[9] = (byte) 210;
    numArray6[1] = (byte) 18;
    numArray6[11] = (byte) 22;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13221()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 113,
        (byte) 87,
        (byte) 68,
        (byte) 121,
        (byte) 116,
        (byte) 2
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 211,
        (byte) 119,
        (byte) 44,
        (byte) 239,
        (byte) 224 /*0xE0*/,
        (byte) 55
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[39];
      byte[] response = new byte[39];
      Array.Copy((Array) sc_13210.sspq, 71, (Array) numArray4, 0, 39);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 71, (Array) numArray4, 0, 39);
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
    byte[] numArray5 = new byte[6];
    byte[] numArray6 = new byte[6];
    numArray6[4] = (byte) 60;
    numArray6[1] = (byte) 14;
    numArray6[2] = (byte) 184;
    numArray6[3] = (byte) 126;
    numArray6[5] = (byte) 204;
    numArray6[0] = (byte) 185;
    byte[] numArray7 = new byte[6]
    {
      (byte) 156,
      (byte) 0,
      (byte) 0,
      (byte) 11,
      (byte) 0,
      (byte) 0
    };
    numArray7[2] = (byte) 75;
    numArray7[1] = (byte) 86;
    numArray7[4] = (byte) 111;
    numArray7[5] = (byte) 80 /*0x50*/;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13222()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 176 /*0xB0*/,
        (byte) 6,
        (byte) 97,
        (byte) 243,
        (byte) 195,
        (byte) 113,
        (byte) 125,
        (byte) 38,
        (byte) 228,
        (byte) 194
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 184,
        (byte) 171,
        (byte) 77,
        (byte) 14,
        (byte) 135,
        (byte) 132,
        (byte) 145,
        (byte) 185,
        (byte) 181,
        (byte) 58
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
      (byte) 94,
      (byte) 150,
      (byte) 163,
      (byte) 198,
      (byte) 213,
      (byte) 174,
      (byte) 132,
      (byte) 73,
      (byte) 91,
      (byte) 10
    };
    byte[] numArray6 = new byte[10];
    numArray6[1] = (byte) 28;
    numArray6[9] = (byte) 122;
    numArray6[2] = (byte) 213;
    numArray6[3] = (byte) 130;
    numArray6[4] = (byte) 203;
    numArray6[5] = (byte) 108;
    numArray6[8] = (byte) 154;
    numArray6[7] = (byte) 219;
    numArray6[6] = (byte) 204;
    numArray6[0] = (byte) 99;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13223()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[40];
      byte[] numArray2 = new byte[40];
      numArray2[5] = (byte) 212;
      numArray2[29] = (byte) 82;
      numArray2[28] = (byte) 93;
      numArray2[19] = (byte) 252;
      numArray2[0] = (byte) 72;
      numArray2[20] = (byte) 227;
      numArray2[10] = (byte) 53;
      numArray2[7] = (byte) 199;
      numArray2[6] = (byte) 78;
      numArray2[9] = (byte) 4;
      numArray2[30] = (byte) 46;
      numArray2[12] = (byte) 205;
      numArray2[25] = (byte) 42;
      numArray2[35] = (byte) 204;
      numArray2[14] = (byte) 8;
      numArray2[15] = (byte) 150;
      numArray2[16 /*0x10*/] = (byte) 62;
      numArray2[17] = (byte) 26;
      numArray2[2] = (byte) 193;
      numArray2[18] = (byte) 84;
      numArray2[22] = (byte) 152;
      numArray2[21] = (byte) 37;
      numArray2[11] = (byte) 148;
      numArray2[23] = (byte) 57;
      numArray2[38] = (byte) 189;
      numArray2[39] = (byte) 135;
      numArray2[26] = (byte) 60;
      numArray2[27] = byte.MaxValue;
      numArray2[13] = (byte) 108;
      numArray2[34] = (byte) 125;
      numArray2[33] = (byte) 134;
      numArray2[31 /*0x1F*/] = (byte) 0;
      numArray2[32 /*0x20*/] = (byte) 229;
      numArray2[8] = (byte) 127 /*0x7F*/;
      numArray2[37] = (byte) 90;
      numArray2[1] = (byte) 9;
      numArray2[36] = (byte) 236;
      numArray2[3] = (byte) 41;
      numArray2[4] = (byte) 140;
      numArray2[24] = (byte) 116;
      byte[] numArray3 = new byte[40];
      numArray3[2] = (byte) 37;
      numArray3[1] = (byte) 14;
      numArray3[25] = (byte) 93;
      numArray3[3] = (byte) 177;
      numArray3[19] = (byte) 197;
      numArray3[5] = (byte) 145;
      numArray3[17] = (byte) 9;
      numArray3[7] = (byte) 98;
      numArray3[36] = (byte) 226;
      numArray3[24] = (byte) 125;
      numArray3[34] = (byte) 217;
      numArray3[13] = (byte) 5;
      numArray3[30] = (byte) 53;
      numArray3[9] = (byte) 252;
      numArray3[14] = (byte) 52;
      numArray3[8] = (byte) 116;
      numArray3[27] = byte.MaxValue;
      numArray3[32 /*0x20*/] = (byte) 207;
      numArray3[0] = (byte) 191;
      numArray3[16 /*0x10*/] = (byte) 166;
      numArray3[20] = (byte) 112 /*0x70*/;
      numArray3[12] = (byte) 104;
      numArray3[22] = (byte) 203;
      numArray3[10] = (byte) 119;
      numArray3[15] = (byte) 114;
      numArray3[11] = (byte) 206;
      numArray3[26] = (byte) 36;
      numArray3[18] = (byte) 39;
      numArray3[39] = (byte) 9;
      numArray3[33] = (byte) 191;
      numArray3[21] = (byte) 193;
      numArray3[31 /*0x1F*/] = (byte) 152;
      numArray3[28] = (byte) 164;
      numArray3[23] = (byte) 87;
      numArray3[29] = (byte) 214;
      numArray3[35] = (byte) 123;
      numArray3[6] = (byte) 201;
      numArray3[37] = (byte) 13;
      numArray3[38] = (byte) 253;
      numArray3[4] = (byte) 131;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[40];
    byte[] numArray5 = new byte[40];
    numArray5[39] = (byte) 95;
    numArray5[16 /*0x10*/] = (byte) 121;
    numArray5[2] = (byte) 70;
    numArray5[6] = (byte) 212;
    numArray5[33] = (byte) 238;
    numArray5[5] = (byte) 221;
    numArray5[3] = (byte) 61;
    numArray5[7] = (byte) 224 /*0xE0*/;
    numArray5[14] = (byte) 197;
    numArray5[12] = (byte) 18;
    numArray5[34] = (byte) 185;
    numArray5[27] = (byte) 97;
    numArray5[31 /*0x1F*/] = (byte) 142;
    numArray5[13] = (byte) 177;
    numArray5[20] = (byte) 64 /*0x40*/;
    numArray5[15] = (byte) 12;
    numArray5[23] = (byte) 17;
    numArray5[17] = (byte) 23;
    numArray5[8] = (byte) 181;
    numArray5[19] = (byte) 188;
    numArray5[38] = (byte) 218;
    numArray5[21] = (byte) 13;
    numArray5[22] = (byte) 93;
    numArray5[0] = (byte) 148;
    numArray5[24] = (byte) 242;
    numArray5[25] = (byte) 244;
    numArray5[26] = (byte) 87;
    numArray5[36] = (byte) 140;
    numArray5[28] = (byte) 193;
    numArray5[29] = (byte) 104;
    numArray5[11] = (byte) 60;
    numArray5[37] = (byte) 189;
    numArray5[32 /*0x20*/] = (byte) 70;
    numArray5[9] = (byte) 34;
    numArray5[30] = (byte) 111;
    numArray5[4] = (byte) 2;
    numArray5[1] = (byte) 224 /*0xE0*/;
    numArray5[35] = (byte) 158;
    numArray5[10] = (byte) 104;
    numArray5[18] = (byte) 185;
    byte[] numArray6 = new byte[40]
    {
      (byte) 112 /*0x70*/,
      (byte) 154,
      (byte) 214,
      (byte) 98,
      (byte) 216,
      (byte) 187,
      (byte) 36,
      (byte) 187,
      (byte) 55,
      (byte) 70,
      (byte) 167,
      (byte) 41,
      (byte) 35,
      (byte) 45,
      (byte) 133,
      (byte) 45,
      (byte) 78,
      (byte) 151,
      (byte) 10,
      (byte) 158,
      (byte) 212,
      (byte) 113,
      (byte) 223,
      (byte) 125,
      (byte) 236,
      (byte) 244,
      (byte) 207,
      (byte) 176 /*0xB0*/,
      (byte) 224 /*0xE0*/,
      (byte) 242,
      (byte) 92,
      (byte) 69,
      (byte) 28,
      (byte) 214,
      (byte) 205,
      (byte) 93,
      (byte) 47,
      (byte) 170,
      (byte) 68,
      (byte) 103
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 40);
    for (int index = 0; index < 40; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13224()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[17] = (byte) 47;
      numArray2[1] = (byte) 165;
      numArray2[6] = (byte) 27;
      numArray2[3] = (byte) 132;
      numArray2[4] = (byte) 54;
      numArray2[18] = (byte) 239;
      numArray2[10] = (byte) 217;
      numArray2[14] = (byte) 40;
      numArray2[8] = (byte) 114;
      numArray2[15] = (byte) 15;
      numArray2[11] = (byte) 45;
      numArray2[7] = (byte) 87;
      numArray2[12] = (byte) 12;
      numArray2[13] = (byte) 73;
      numArray2[0] = (byte) 41;
      numArray2[2] = (byte) 9;
      numArray2[16 /*0x10*/] = (byte) 130;
      numArray2[5] = (byte) 50;
      numArray2[9] = (byte) 204;
      byte[] numArray3 = new byte[19]
      {
        (byte) 244,
        (byte) 121,
        (byte) 66,
        (byte) 208 /*0xD0*/,
        (byte) 98,
        (byte) 223,
        (byte) 206,
        (byte) 72,
        (byte) 52,
        (byte) 68,
        (byte) 38,
        (byte) 90,
        (byte) 194,
        (byte) 176 /*0xB0*/,
        (byte) 216,
        (byte) 43,
        (byte) 68,
        (byte) 200,
        (byte) 93
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[30];
      byte[] response = new byte[30];
      Array.Copy((Array) sc_13210.sspq, 110, (Array) numArray4, 0, 30);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 110, (Array) numArray4, 0, 30);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19];
    numArray6[9] = (byte) 227;
    numArray6[3] = (byte) 216;
    numArray6[16 /*0x10*/] = (byte) 154;
    numArray6[18] = (byte) 244;
    numArray6[17] = (byte) 125;
    numArray6[5] = (byte) 27;
    numArray6[6] = (byte) 169;
    numArray6[15] = (byte) 91;
    numArray6[8] = (byte) 155;
    numArray6[1] = (byte) 41;
    numArray6[10] = (byte) 169;
    numArray6[11] = (byte) 244;
    numArray6[12] = (byte) 215;
    numArray6[7] = (byte) 184;
    numArray6[14] = (byte) 149;
    numArray6[13] = (byte) 251;
    numArray6[4] = (byte) 58;
    numArray6[0] = (byte) 65;
    numArray6[2] = (byte) 18;
    byte[] numArray7 = new byte[19];
    numArray7[5] = (byte) 11;
    numArray7[1] = (byte) 136;
    numArray7[2] = (byte) 63 /*0x3F*/;
    numArray7[14] = (byte) 126;
    numArray7[9] = (byte) 9;
    numArray7[12] = (byte) 152;
    numArray7[0] = (byte) 87;
    numArray7[18] = (byte) 147;
    numArray7[8] = (byte) 151;
    numArray7[4] = (byte) 36;
    numArray7[6] = (byte) 198;
    numArray7[11] = (byte) 232;
    numArray7[3] = (byte) 110;
    numArray7[13] = (byte) 124;
    numArray7[7] = (byte) 89;
    numArray7[15] = (byte) 101;
    numArray7[16 /*0x10*/] = (byte) 111;
    numArray7[17] = (byte) 157;
    numArray7[10] = (byte) 222;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13225()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 235,
        (byte) 160 /*0xA0*/,
        (byte) 195,
        (byte) 220,
        (byte) 108,
        (byte) 27,
        (byte) 241,
        (byte) 204,
        (byte) 62,
        (byte) 113,
        (byte) 47,
        (byte) 17
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 31 /*0x1F*/,
        (byte) 146,
        (byte) 138,
        (byte) 52,
        (byte) 195,
        (byte) 21,
        (byte) 129,
        (byte) 78,
        (byte) 154,
        (byte) 148,
        (byte) 16 /*0x10*/,
        (byte) 254
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[2] = (byte) 131;
    numArray5[7] = (byte) 156;
    numArray5[1] = (byte) 99;
    numArray5[3] = (byte) 135;
    numArray5[0] = (byte) 99;
    numArray5[5] = (byte) 172;
    numArray5[6] = (byte) 145;
    numArray5[4] = (byte) 218;
    numArray5[8] = (byte) 66;
    numArray5[9] = (byte) 94;
    numArray5[10] = (byte) 21;
    numArray5[11] = (byte) 138;
    byte[] numArray6 = new byte[12];
    numArray6[2] = (byte) 138;
    numArray6[8] = (byte) 100;
    numArray6[5] = (byte) 113;
    numArray6[0] = (byte) 15;
    numArray6[4] = (byte) 35;
    numArray6[9] = (byte) 155;
    numArray6[6] = (byte) 104;
    numArray6[7] = (byte) 152;
    numArray6[1] = (byte) 73;
    numArray6[11] = (byte) 86;
    numArray6[10] = (byte) 209;
    numArray6[3] = (byte) 214;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13226()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 90,
        (byte) 4,
        (byte) 129,
        (byte) 125,
        (byte) 86,
        (byte) 144 /*0x90*/,
        (byte) 139,
        (byte) 132,
        (byte) 110,
        (byte) 167,
        (byte) 192 /*0xC0*/,
        (byte) 58,
        (byte) 4
      };
      byte[] numArray3 = new byte[13];
      numArray3[11] = (byte) 122;
      numArray3[1] = (byte) 92;
      numArray3[2] = (byte) 200;
      numArray3[8] = (byte) 188;
      numArray3[3] = (byte) 197;
      numArray3[5] = (byte) 188;
      numArray3[6] = (byte) 31 /*0x1F*/;
      numArray3[4] = (byte) 200;
      numArray3[7] = (byte) 31 /*0x1F*/;
      numArray3[9] = (byte) 49;
      numArray3[0] = (byte) 107;
      numArray3[10] = (byte) 67;
      numArray3[12] = (byte) 12;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_13210.sspq, 140, (Array) numArray4, 0, 12);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 140, (Array) numArray4, 0, 12);
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
    byte[] numArray6 = new byte[13]
    {
      (byte) 129,
      (byte) 98,
      (byte) 166,
      (byte) 91,
      (byte) 44,
      (byte) 34,
      (byte) 44,
      (byte) 83,
      (byte) 33,
      (byte) 26,
      (byte) 38,
      (byte) 183,
      (byte) 74
    };
    byte[] numArray7 = new byte[13]
    {
      (byte) 50,
      (byte) 156,
      (byte) 241,
      (byte) 176 /*0xB0*/,
      (byte) 30,
      (byte) 57,
      (byte) 206,
      (byte) 39,
      (byte) 31 /*0x1F*/,
      (byte) 14,
      (byte) 12,
      (byte) 204,
      (byte) 248
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13227()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 113,
        (byte) 153,
        (byte) 58,
        (byte) 190,
        (byte) 192 /*0xC0*/,
        (byte) 121,
        (byte) 196,
        (byte) 27,
        (byte) 114,
        (byte) 128 /*0x80*/
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 251,
        (byte) 143,
        (byte) 82,
        (byte) 11,
        (byte) 211,
        (byte) 54,
        (byte) 61,
        (byte) 97,
        (byte) 198,
        (byte) 57
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
      (byte) 213,
      (byte) 163,
      (byte) 179,
      (byte) 1,
      (byte) 32 /*0x20*/,
      (byte) 48 /*0x30*/,
      (byte) 53,
      (byte) 108,
      (byte) 185,
      (byte) 213
    };
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 230;
    numArray6[1] = (byte) 182;
    numArray6[2] = (byte) 207;
    numArray6[0] = (byte) 214;
    numArray6[6] = (byte) 212;
    numArray6[9] = (byte) 220;
    numArray6[4] = (byte) 133;
    numArray6[7] = (byte) 147;
    numArray6[8] = (byte) 144 /*0x90*/;
    numArray6[5] = (byte) 55;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13228()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[142];
      byte[] numArray2 = new byte[55];
      numArray2[43] = (byte) 134;
      numArray2[33] = (byte) 159;
      numArray2[14] = (byte) 151;
      numArray2[3] = (byte) 225;
      numArray2[0] = (byte) 85;
      numArray2[16 /*0x10*/] = (byte) 111;
      numArray2[20] = (byte) 20;
      numArray2[21] = (byte) 75;
      numArray2[6] = (byte) 161;
      numArray2[9] = (byte) 5;
      numArray2[28] = (byte) 88;
      numArray2[11] = (byte) 130;
      numArray2[34] = (byte) 172;
      numArray2[13] = (byte) 73;
      numArray2[29] = (byte) 110;
      numArray2[15] = (byte) 65;
      numArray2[24] = (byte) 239;
      numArray2[17] = (byte) 73;
      numArray2[18] = (byte) 184;
      numArray2[12] = (byte) 4;
      numArray2[38] = (byte) 44;
      numArray2[45] = (byte) 82;
      numArray2[8] = (byte) 214;
      numArray2[23] = (byte) 101;
      numArray2[10] = (byte) 69;
      numArray2[25] = (byte) 181;
      numArray2[26] = (byte) 42;
      numArray2[27] = (byte) 72;
      numArray2[46] = (byte) 98;
      numArray2[49] = (byte) 205;
      numArray2[30] = (byte) 236;
      numArray2[31 /*0x1F*/] = (byte) 154;
      numArray2[42] = (byte) 156;
      numArray2[39] = (byte) 254;
      numArray2[7] = (byte) 197;
      numArray2[35] = (byte) 96 /*0x60*/;
      numArray2[36] = (byte) 70;
      numArray2[22] = (byte) 53;
      numArray2[5] = (byte) 186;
      numArray2[37] = (byte) 84;
      numArray2[40] = (byte) 51;
      numArray2[41] = (byte) 95;
      numArray2[2] = (byte) 233;
      numArray2[32 /*0x20*/] = (byte) 147;
      numArray2[52] = (byte) 130;
      numArray2[1] = (byte) 192 /*0xC0*/;
      numArray2[47] = (byte) 253;
      numArray2[4] = (byte) 64 /*0x40*/;
      numArray2[48 /*0x30*/] = (byte) 160 /*0xA0*/;
      numArray2[44] = (byte) 206;
      numArray2[50] = (byte) 157;
      numArray2[51] = (byte) 230;
      numArray2[19] = (byte) 195;
      numArray2[53] = (byte) 76;
      numArray2[54] = (byte) 254;
      byte[] numArray3 = new byte[55]
      {
        (byte) 178,
        (byte) 137,
        (byte) 20,
        (byte) 187,
        (byte) 144 /*0x90*/,
        (byte) 194,
        (byte) 2,
        (byte) 242,
        (byte) 234,
        (byte) 16 /*0x10*/,
        (byte) 7,
        (byte) 120,
        (byte) 190,
        (byte) 181,
        (byte) 243,
        (byte) 236,
        (byte) 162,
        (byte) 16 /*0x10*/,
        (byte) 157,
        (byte) 161,
        (byte) 242,
        (byte) 212,
        (byte) 152,
        (byte) 209,
        (byte) 30,
        (byte) 176 /*0xB0*/,
        (byte) 165,
        (byte) 249,
        (byte) 46,
        (byte) 44,
        (byte) 197,
        (byte) 120,
        (byte) 82,
        (byte) 198,
        (byte) 107,
        (byte) 75,
        (byte) 20,
        (byte) 42,
        (byte) 216,
        (byte) 119,
        (byte) 142,
        (byte) 216,
        (byte) 3,
        (byte) 215,
        (byte) 167,
        (byte) 217,
        (byte) 199,
        (byte) 126,
        (byte) 170,
        (byte) 3,
        (byte) 150,
        (byte) 118,
        (byte) 158,
        (byte) 248,
        (byte) 152
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[2] = (byte) 55;
      numArray4[44] = (byte) 55;
      numArray4[31 /*0x1F*/] = (byte) 86;
      numArray4[3] = (byte) 98;
      numArray4[4] = (byte) 176 /*0xB0*/;
      numArray4[11] = (byte) 251;
      numArray4[49] = (byte) 77;
      numArray4[7] = (byte) 17;
      numArray4[48 /*0x30*/] = (byte) 201;
      numArray4[9] = (byte) 168;
      numArray4[10] = (byte) 196;
      numArray4[35] = (byte) 15;
      numArray4[16 /*0x10*/] = (byte) 74;
      numArray4[13] = (byte) 55;
      numArray4[29] = (byte) 80 /*0x50*/;
      numArray4[15] = (byte) 123;
      numArray4[17] = (byte) 180;
      numArray4[50] = (byte) 126;
      numArray4[52] = (byte) 136;
      numArray4[19] = (byte) 122;
      numArray4[39] = (byte) 16 /*0x10*/;
      numArray4[27] = (byte) 79;
      numArray4[22] = (byte) 58;
      numArray4[38] = (byte) 42;
      numArray4[43] = (byte) 186;
      numArray4[25] = (byte) 62;
      numArray4[36] = (byte) 105;
      numArray4[23] = (byte) 86;
      numArray4[28] = (byte) 60;
      numArray4[0] = (byte) 236;
      numArray4[30] = (byte) 138;
      numArray4[54] = (byte) 153;
      numArray4[32 /*0x20*/] = (byte) 71;
      numArray4[18] = (byte) 193;
      numArray4[34] = (byte) 213;
      numArray4[12] = (byte) 97;
      numArray4[8] = (byte) 145;
      numArray4[37] = (byte) 12;
      numArray4[14] = (byte) 184;
      numArray4[42] = (byte) 146;
      numArray4[40] = (byte) 158;
      numArray4[41] = (byte) 91;
      numArray4[21] = (byte) 115;
      numArray4[6] = (byte) 150;
      numArray4[33] = (byte) 250;
      numArray4[20] = (byte) 65;
      numArray4[46] = (byte) 7;
      numArray4[47] = (byte) 155;
      numArray4[1] = (byte) 83;
      numArray4[5] = (byte) 226;
      numArray4[24] = (byte) 204;
      numArray4[51] = (byte) 7;
      numArray4[26] = (byte) 183;
      numArray4[45] = (byte) 101;
      numArray4[53] = (byte) 224 /*0xE0*/;
      byte[] numArray5 = new byte[55];
      numArray5[15] = (byte) 106;
      numArray5[51] = (byte) 67;
      numArray5[27] = (byte) 193;
      numArray5[3] = (byte) 62;
      numArray5[22] = (byte) 15;
      numArray5[49] = (byte) 25;
      numArray5[6] = (byte) 15;
      numArray5[7] = (byte) 212;
      numArray5[8] = (byte) 249;
      numArray5[9] = (byte) 196;
      numArray5[10] = (byte) 91;
      numArray5[44] = (byte) 229;
      numArray5[43] = (byte) 94;
      numArray5[13] = (byte) 86;
      numArray5[33] = (byte) 18;
      numArray5[23] = (byte) 49;
      numArray5[0] = (byte) 146;
      numArray5[17] = (byte) 15;
      numArray5[21] = (byte) 22;
      numArray5[19] = (byte) 153;
      numArray5[20] = (byte) 177;
      numArray5[29] = (byte) 240 /*0xF0*/;
      numArray5[36] = (byte) 253;
      numArray5[42] = (byte) 16 /*0x10*/;
      numArray5[24] = (byte) 178;
      numArray5[48 /*0x30*/] = (byte) 143;
      numArray5[41] = (byte) 50;
      numArray5[50] = (byte) 228;
      numArray5[28] = (byte) 177;
      numArray5[32 /*0x20*/] = (byte) 202;
      numArray5[45] = (byte) 197;
      numArray5[11] = (byte) 58;
      numArray5[31 /*0x1F*/] = (byte) 137;
      numArray5[46] = (byte) 64 /*0x40*/;
      numArray5[34] = (byte) 208 /*0xD0*/;
      numArray5[35] = (byte) 21;
      numArray5[25] = (byte) 181;
      numArray5[37] = (byte) 88;
      numArray5[38] = (byte) 5;
      numArray5[39] = byte.MaxValue;
      numArray5[40] = (byte) 155;
      numArray5[52] = (byte) 237;
      numArray5[14] = (byte) 107;
      numArray5[30] = (byte) 236;
      numArray5[18] = (byte) 217;
      numArray5[1] = (byte) 101;
      numArray5[47] = (byte) 217;
      numArray5[26] = (byte) 228;
      numArray5[2] = (byte) 78;
      numArray5[4] = (byte) 70;
      numArray5[5] = (byte) 88;
      numArray5[12] = (byte) 218;
      numArray5[16 /*0x10*/] = (byte) 208 /*0xD0*/;
      numArray5[53] = (byte) 30;
      numArray5[54] = (byte) 200;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[32 /*0x20*/];
      numArray6[19] = (byte) 176 /*0xB0*/;
      numArray6[7] = (byte) 149;
      numArray6[2] = (byte) 164;
      numArray6[3] = (byte) 43;
      numArray6[6] = (byte) 223;
      numArray6[5] = (byte) 51;
      numArray6[0] = (byte) 110;
      numArray6[10] = (byte) 87;
      numArray6[28] = (byte) 88;
      numArray6[13] = (byte) 198;
      numArray6[15] = (byte) 83;
      numArray6[20] = (byte) 223;
      numArray6[25] = (byte) 245;
      numArray6[21] = (byte) 43;
      numArray6[14] = (byte) 128 /*0x80*/;
      numArray6[16 /*0x10*/] = (byte) 242;
      numArray6[1] = (byte) 121;
      numArray6[17] = (byte) 238;
      numArray6[18] = (byte) 190;
      numArray6[22] = (byte) 80 /*0x50*/;
      numArray6[4] = (byte) 93;
      numArray6[11] = (byte) 63 /*0x3F*/;
      numArray6[8] = (byte) 169;
      numArray6[23] = (byte) 250;
      numArray6[24] = (byte) 175;
      numArray6[29] = (byte) 233;
      numArray6[26] = (byte) 2;
      numArray6[27] = (byte) 22;
      numArray6[12] = (byte) 83;
      numArray6[9] = (byte) 234;
      numArray6[30] = (byte) 225;
      numArray6[31 /*0x1F*/] = (byte) 89;
      byte[] numArray7 = new byte[32 /*0x20*/]
      {
        (byte) 204,
        (byte) 120,
        (byte) 55,
        (byte) 55,
        (byte) 164,
        (byte) 93,
        (byte) 76,
        (byte) 19,
        (byte) 77,
        (byte) 88,
        (byte) 118,
        (byte) 183,
        (byte) 20,
        (byte) 3,
        (byte) 5,
        (byte) 10,
        (byte) 29,
        (byte) 75,
        (byte) 40,
        (byte) 60,
        (byte) 251,
        (byte) 19,
        (byte) 196,
        (byte) 101,
        (byte) 108,
        (byte) 203,
        (byte) 209,
        (byte) 141,
        (byte) 171,
        (byte) 8,
        (byte) 135,
        (byte) 30
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 32 /*0x20*/);
      for (int index = 0; index < 32 /*0x20*/; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[34];
      byte[] response = new byte[34];
      Array.Copy((Array) sc_13210.sspq, 152, (Array) numArray8, 0, 34);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13210.sspr, 152, (Array) numArray8, 0, 34);
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
    byte[] numArray9 = new byte[142];
    byte[] numArray10 = new byte[55]
    {
      (byte) 121,
      (byte) 69,
      (byte) 222,
      (byte) 211,
      (byte) 219,
      (byte) 69,
      (byte) 167,
      (byte) 98,
      (byte) 149,
      (byte) 81,
      (byte) 242,
      (byte) 193,
      (byte) 141,
      (byte) 23,
      (byte) 171,
      (byte) 60,
      (byte) 240 /*0xF0*/,
      (byte) 186,
      (byte) 59,
      (byte) 24,
      (byte) 217,
      (byte) 93,
      (byte) 14,
      (byte) 73,
      (byte) 21,
      (byte) 229,
      (byte) 19,
      (byte) 119,
      (byte) 67,
      (byte) 237,
      (byte) 102,
      (byte) 114,
      (byte) 71,
      (byte) 106,
      (byte) 159,
      (byte) 216,
      (byte) 33,
      (byte) 251,
      (byte) 114,
      (byte) 125,
      (byte) 192 /*0xC0*/,
      (byte) 110,
      (byte) 125,
      (byte) 83,
      (byte) 62,
      (byte) 111,
      (byte) 67,
      (byte) 37,
      (byte) 140,
      (byte) 242,
      (byte) 206,
      (byte) 181,
      (byte) 9,
      (byte) 90,
      (byte) 149
    };
    byte[] numArray11 = new byte[55];
    numArray11[44] = (byte) 71;
    numArray11[1] = (byte) 165;
    numArray11[41] = (byte) 27;
    numArray11[17] = (byte) 180;
    numArray11[28] = (byte) 160 /*0xA0*/;
    numArray11[36] = (byte) 137;
    numArray11[38] = (byte) 72;
    numArray11[7] = (byte) 79;
    numArray11[33] = (byte) 28;
    numArray11[11] = (byte) 137;
    numArray11[25] = (byte) 240 /*0xF0*/;
    numArray11[10] = (byte) 236;
    numArray11[12] = (byte) 78;
    numArray11[0] = (byte) 102;
    numArray11[14] = (byte) 64 /*0x40*/;
    numArray11[15] = (byte) 233;
    numArray11[4] = (byte) 47;
    numArray11[32 /*0x20*/] = (byte) 247;
    numArray11[29] = (byte) 240 /*0xF0*/;
    numArray11[19] = (byte) 44;
    numArray11[37] = (byte) 216;
    numArray11[21] = (byte) 186;
    numArray11[35] = (byte) 226;
    numArray11[23] = (byte) 85;
    numArray11[24] = byte.MaxValue;
    numArray11[8] = (byte) 166;
    numArray11[22] = (byte) 192 /*0xC0*/;
    numArray11[27] = (byte) 74;
    numArray11[45] = (byte) 125;
    numArray11[53] = (byte) 118;
    numArray11[30] = (byte) 40;
    numArray11[31 /*0x1F*/] = (byte) 220;
    numArray11[13] = (byte) 184;
    numArray11[9] = (byte) 11;
    numArray11[34] = (byte) 50;
    numArray11[39] = (byte) 233;
    numArray11[2] = (byte) 250;
    numArray11[18] = (byte) 245;
    numArray11[3] = (byte) 234;
    numArray11[26] = (byte) 46;
    numArray11[40] = (byte) 99;
    numArray11[47] = (byte) 183;
    numArray11[42] = (byte) 192 /*0xC0*/;
    numArray11[43] = (byte) 162;
    numArray11[6] = (byte) 136;
    numArray11[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray11[46] = (byte) 157;
    numArray11[48 /*0x30*/] = (byte) 22;
    numArray11[20] = (byte) 60;
    numArray11[5] = (byte) 229;
    numArray11[49] = (byte) 140;
    numArray11[51] = (byte) 195;
    numArray11[52] = (byte) 153;
    numArray11[50] = (byte) 82;
    numArray11[54] = (byte) 186;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 139,
      (byte) 23,
      (byte) 84,
      (byte) 66,
      (byte) 67,
      (byte) 206,
      (byte) 136,
      (byte) 57,
      (byte) 23,
      (byte) 141,
      (byte) 77,
      (byte) 243,
      (byte) 49,
      (byte) 143,
      (byte) 183,
      (byte) 203,
      (byte) 112 /*0x70*/,
      (byte) 198,
      (byte) 27,
      (byte) 11,
      (byte) 82,
      (byte) 76,
      (byte) 177,
      (byte) 128 /*0x80*/,
      (byte) 64 /*0x40*/,
      (byte) 50,
      (byte) 121,
      (byte) 70,
      (byte) 67,
      (byte) 28,
      (byte) 234,
      (byte) 169,
      (byte) 73,
      (byte) 37,
      (byte) 0,
      (byte) 37,
      (byte) 39,
      (byte) 9,
      (byte) 78,
      (byte) 129,
      (byte) 76,
      (byte) 46,
      (byte) 166,
      (byte) 103,
      (byte) 211,
      (byte) 186,
      (byte) 135,
      (byte) 72,
      (byte) 33,
      (byte) 162,
      (byte) 219,
      (byte) 59,
      (byte) 15,
      (byte) 47,
      (byte) 41
    };
    byte[] numArray13 = new byte[55];
    numArray13[6] = (byte) 218;
    numArray13[5] = (byte) 139;
    numArray13[2] = (byte) 74;
    numArray13[38] = (byte) 123;
    numArray13[43] = (byte) 206;
    numArray13[3] = (byte) 67;
    numArray13[10] = (byte) 9;
    numArray13[23] = (byte) 27;
    numArray13[24] = (byte) 42;
    numArray13[48 /*0x30*/] = (byte) 12;
    numArray13[17] = (byte) 2;
    numArray13[20] = (byte) 88;
    numArray13[35] = (byte) 40;
    numArray13[1] = (byte) 66;
    numArray13[31 /*0x1F*/] = (byte) 77;
    numArray13[52] = (byte) 15;
    numArray13[53] = (byte) 51;
    numArray13[13] = (byte) 223;
    numArray13[18] = (byte) 63 /*0x3F*/;
    numArray13[19] = (byte) 140;
    numArray13[0] = (byte) 241;
    numArray13[15] = (byte) 97;
    numArray13[22] = (byte) 237;
    numArray13[42] = (byte) 130;
    numArray13[45] = (byte) 240 /*0xF0*/;
    numArray13[25] = (byte) 23;
    numArray13[46] = (byte) 109;
    numArray13[44] = (byte) 162;
    numArray13[7] = (byte) 90;
    numArray13[29] = (byte) 170;
    numArray13[30] = (byte) 9;
    numArray13[12] = (byte) 52;
    numArray13[4] = (byte) 65;
    numArray13[9] = (byte) 151;
    numArray13[16 /*0x10*/] = (byte) 114;
    numArray13[34] = (byte) 108;
    numArray13[36] = (byte) 159;
    numArray13[37] = (byte) 44;
    numArray13[14] = (byte) 192 /*0xC0*/;
    numArray13[33] = (byte) 38;
    numArray13[40] = (byte) 178;
    numArray13[41] = (byte) 233;
    numArray13[8] = (byte) 74;
    numArray13[21] = (byte) 35;
    numArray13[11] = (byte) 254;
    numArray13[27] = (byte) 6;
    numArray13[26] = (byte) 139;
    numArray13[47] = (byte) 25;
    numArray13[28] = (byte) 126;
    numArray13[49] = (byte) 220;
    numArray13[50] = (byte) 218;
    numArray13[32 /*0x20*/] = (byte) 197;
    numArray13[51] = (byte) 25;
    numArray13[39] = (byte) 41;
    numArray13[54] = (byte) 121;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[32 /*0x20*/];
    numArray14[4] = (byte) 29;
    numArray14[1] = (byte) 123;
    numArray14[0] = (byte) 39;
    numArray14[3] = (byte) 36;
    numArray14[8] = (byte) 10;
    numArray14[16 /*0x10*/] = (byte) 109;
    numArray14[6] = (byte) 185;
    numArray14[23] = (byte) 47;
    numArray14[31 /*0x1F*/] = (byte) 246;
    numArray14[29] = (byte) 35;
    numArray14[12] = (byte) 211;
    numArray14[11] = (byte) 205;
    numArray14[25] = (byte) 148;
    numArray14[13] = (byte) 80 /*0x50*/;
    numArray14[7] = (byte) 244;
    numArray14[15] = (byte) 58;
    numArray14[24] = (byte) 119;
    numArray14[17] = (byte) 179;
    numArray14[18] = (byte) 235;
    numArray14[19] = (byte) 249;
    numArray14[20] = (byte) 132;
    numArray14[9] = (byte) 143;
    numArray14[22] = (byte) 117;
    numArray14[27] = (byte) 205;
    numArray14[30] = (byte) 170;
    numArray14[28] = (byte) 235;
    numArray14[26] = (byte) 162;
    numArray14[21] = (byte) 125;
    numArray14[14] = (byte) 95;
    numArray14[5] = (byte) 85;
    numArray14[10] = (byte) 16 /*0x10*/;
    numArray14[2] = (byte) 101;
    byte[] numArray15 = new byte[32 /*0x20*/]
    {
      (byte) 241,
      (byte) 57,
      (byte) 21,
      (byte) 189,
      (byte) 250,
      (byte) 139,
      (byte) 96 /*0x60*/,
      (byte) 24,
      (byte) 61,
      (byte) 157,
      (byte) 226,
      (byte) 193,
      (byte) 164,
      (byte) 58,
      (byte) 40,
      (byte) 62,
      (byte) 106,
      (byte) 196,
      (byte) 79,
      (byte) 148,
      (byte) 223,
      (byte) 113,
      (byte) 71,
      (byte) 86,
      (byte) 154,
      (byte) 254,
      (byte) 20,
      (byte) 65,
      (byte) 96 /*0x60*/,
      (byte) 165,
      (byte) 227,
      (byte) 144 /*0x90*/
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 32 /*0x20*/);
    for (int index = 0; index < 32 /*0x20*/; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static int ssp_appserver_13229(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 7;
    sourceArray1[24] = (byte) 101;
    sourceArray1[2] = (byte) 102;
    sourceArray1[40] = (byte) 227;
    sourceArray1[13] = (byte) 184;
    sourceArray1[18] = (byte) 134;
    sourceArray1[17] = (byte) 219;
    sourceArray1[9] = (byte) 196;
    sourceArray1[8] = (byte) 244;
    sourceArray1[36] = (byte) 161;
    sourceArray1[3] = (byte) 197;
    sourceArray1[19] = (byte) 9;
    sourceArray1[28] = (byte) 62;
    sourceArray1[15] = (byte) 27;
    sourceArray1[46] = (byte) 189;
    sourceArray1[6] = (byte) 180;
    sourceArray1[16 /*0x10*/] = (byte) 237;
    sourceArray1[11] = (byte) 216;
    sourceArray1[1] = (byte) 98;
    sourceArray1[33] = (byte) 137;
    sourceArray1[12] = (byte) 244;
    sourceArray1[10] = (byte) 139;
    sourceArray1[22] = (byte) 162;
    sourceArray1[0] = (byte) 211;
    sourceArray1[14] = (byte) 98;
    sourceArray1[25] = (byte) 48 /*0x30*/;
    sourceArray1[26] = (byte) 5;
    sourceArray1[27] = (byte) 199;
    sourceArray1[31 /*0x1F*/] = (byte) 68;
    sourceArray1[29] = (byte) 156;
    sourceArray1[30] = (byte) 81;
    sourceArray1[20] = (byte) 130;
    sourceArray1[32 /*0x20*/] = (byte) 139;
    sourceArray1[21] = (byte) 134;
    sourceArray1[34] = (byte) 65;
    sourceArray1[44] = (byte) 34;
    sourceArray1[47] = (byte) 52;
    sourceArray1[43] = (byte) 113;
    sourceArray1[37] = (byte) 247;
    sourceArray1[39] = (byte) 153;
    sourceArray1[7] = (byte) 88;
    sourceArray1[41] = (byte) 80 /*0x50*/;
    sourceArray1[42] = (byte) 17;
    sourceArray1[23] = (byte) 89;
    sourceArray1[4] = (byte) 8;
    sourceArray1[45] = (byte) 175;
    sourceArray1[38] = (byte) 17;
    sourceArray1[35] = (byte) 38;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 55,
      (byte) 110,
      (byte) 52,
      (byte) 36,
      (byte) 51,
      (byte) 117,
      (byte) 91,
      (byte) 234,
      (byte) 21,
      (byte) 248,
      (byte) 145,
      (byte) 66,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 190,
      (byte) 248,
      (byte) 101,
      (byte) 125,
      (byte) 163,
      (byte) 89,
      (byte) 58,
      (byte) 170,
      (byte) 9,
      (byte) 128 /*0x80*/,
      (byte) 26,
      (byte) 127 /*0x7F*/,
      (byte) 183,
      (byte) 24,
      (byte) 202,
      (byte) 18,
      (byte) 142,
      (byte) 135,
      (byte) 215,
      (byte) 153,
      (byte) 154,
      (byte) 197,
      (byte) 171,
      (byte) 87,
      (byte) 78,
      (byte) 173,
      (byte) 198,
      (byte) 14,
      (byte) 172,
      (byte) 24,
      (byte) 251,
      (byte) 98,
      (byte) 89,
      (byte) 54
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13230()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[1] = (byte) 82;
      numArray2[11] = (byte) 142;
      numArray2[26] = (byte) 94;
      numArray2[3] = (byte) 107;
      numArray2[4] = (byte) 80 /*0x50*/;
      numArray2[15] = (byte) 49;
      numArray2[6] = (byte) 115;
      numArray2[20] = (byte) 34;
      numArray2[8] = (byte) 2;
      numArray2[9] = (byte) 113;
      numArray2[18] = (byte) 179;
      numArray2[35] = (byte) 32 /*0x20*/;
      numArray2[12] = (byte) 212;
      numArray2[13] = (byte) 170;
      numArray2[14] = (byte) 100;
      numArray2[2] = (byte) 190;
      numArray2[16 /*0x10*/] = (byte) 205;
      numArray2[21] = (byte) 233;
      numArray2[17] = (byte) 166;
      numArray2[7] = (byte) 52;
      numArray2[22] = (byte) 132;
      numArray2[24] = (byte) 212;
      numArray2[5] = (byte) 1;
      numArray2[23] = (byte) 129;
      numArray2[29] = (byte) 48 /*0x30*/;
      numArray2[25] = (byte) 229;
      numArray2[33] = (byte) 92;
      numArray2[27] = (byte) 144 /*0x90*/;
      numArray2[19] = (byte) 95;
      numArray2[0] = (byte) 98;
      numArray2[30] = (byte) 251;
      numArray2[31 /*0x1F*/] = (byte) 187;
      numArray2[32 /*0x20*/] = (byte) 50;
      numArray2[28] = (byte) 84;
      numArray2[34] = (byte) 82;
      numArray2[10] = (byte) 216;
      byte[] numArray3 = new byte[36]
      {
        (byte) 33,
        (byte) 229,
        (byte) 53,
        (byte) 114,
        (byte) 12,
        (byte) 184,
        (byte) 50,
        (byte) 38,
        (byte) 55,
        (byte) 50,
        (byte) 229,
        (byte) 216,
        (byte) 16 /*0x10*/,
        (byte) 173,
        (byte) 59,
        (byte) 134,
        (byte) 77,
        (byte) 160 /*0xA0*/,
        (byte) 112 /*0x70*/,
        (byte) 214,
        (byte) 95,
        (byte) 10,
        (byte) 191,
        (byte) 28,
        (byte) 43,
        (byte) 133,
        (byte) 193,
        (byte) 141,
        (byte) 157,
        (byte) 55,
        (byte) 55,
        (byte) 230,
        (byte) 215,
        (byte) 180,
        (byte) 170,
        (byte) 126
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
      (byte) 50,
      (byte) 136,
      (byte) 13,
      (byte) 101,
      (byte) 252,
      (byte) 36,
      (byte) 113,
      (byte) 186,
      (byte) 238,
      (byte) 7,
      (byte) 107,
      (byte) 181,
      (byte) 148,
      (byte) 162,
      (byte) 218,
      (byte) 82,
      (byte) 214,
      (byte) 30,
      (byte) 184,
      byte.MaxValue,
      (byte) 187,
      (byte) 16 /*0x10*/,
      (byte) 143,
      (byte) 0,
      (byte) 254,
      (byte) 123,
      (byte) 71,
      (byte) 149,
      (byte) 145,
      (byte) 72,
      (byte) 15,
      (byte) 12,
      (byte) 244,
      (byte) 221,
      (byte) 135,
      (byte) 239
    };
    byte[] numArray6 = new byte[36]
    {
      (byte) 147,
      (byte) 104,
      (byte) 131,
      (byte) 63 /*0x3F*/,
      (byte) 49,
      (byte) 51,
      (byte) 138,
      (byte) 195,
      (byte) 213,
      (byte) 205,
      (byte) 231,
      (byte) 48 /*0x30*/,
      (byte) 205,
      (byte) 212,
      (byte) 90,
      (byte) 65,
      (byte) 30,
      (byte) 0,
      (byte) 37,
      (byte) 53,
      (byte) 239,
      (byte) 250,
      (byte) 90,
      (byte) 154,
      (byte) 164,
      (byte) 140,
      (byte) 168,
      (byte) 229,
      (byte) 100,
      (byte) 69,
      (byte) 194,
      (byte) 158,
      (byte) 5,
      (byte) 67,
      (byte) 92,
      (byte) 87
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13231()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 48 /*0x30*/,
        (byte) 40,
        (byte) 241,
        (byte) 202,
        (byte) 10,
        (byte) 104,
        (byte) 200,
        (byte) 214,
        (byte) 125,
        (byte) 240 /*0xF0*/,
        (byte) 71,
        (byte) 250,
        (byte) 202,
        (byte) 17,
        (byte) 4,
        (byte) 33,
        (byte) 249,
        (byte) 13,
        (byte) 103
      };
      byte[] numArray3 = new byte[19];
      numArray3[9] = (byte) 244;
      numArray3[8] = (byte) 159;
      numArray3[2] = (byte) 26;
      numArray3[5] = (byte) 26;
      numArray3[0] = (byte) 92;
      numArray3[1] = (byte) 171;
      numArray3[6] = (byte) 82;
      numArray3[7] = (byte) 50;
      numArray3[17] = (byte) 203;
      numArray3[11] = (byte) 41;
      numArray3[10] = (byte) 2;
      numArray3[4] = (byte) 70;
      numArray3[12] = (byte) 51;
      numArray3[13] = (byte) 32 /*0x20*/;
      numArray3[14] = (byte) 82;
      numArray3[15] = (byte) 180;
      numArray3[16 /*0x10*/] = (byte) 1;
      numArray3[3] = (byte) 158;
      numArray3[18] = (byte) 227;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[11] = (byte) 111;
    numArray5[0] = (byte) 113;
    numArray5[12] = (byte) 24;
    numArray5[5] = (byte) 65;
    numArray5[2] = (byte) 161;
    numArray5[1] = (byte) 24;
    numArray5[15] = byte.MaxValue;
    numArray5[8] = (byte) 101;
    numArray5[4] = (byte) 21;
    numArray5[17] = (byte) 209;
    numArray5[10] = (byte) 178;
    numArray5[7] = (byte) 217;
    numArray5[6] = (byte) 227;
    numArray5[13] = (byte) 105;
    numArray5[14] = (byte) 84;
    numArray5[3] = (byte) 0;
    numArray5[16 /*0x10*/] = (byte) 75;
    numArray5[9] = (byte) 246;
    numArray5[18] = (byte) 171;
    byte[] numArray6 = new byte[19]
    {
      (byte) 13,
      (byte) 120,
      (byte) 133,
      (byte) 189,
      (byte) 96 /*0x60*/,
      (byte) 73,
      (byte) 212,
      (byte) 106,
      (byte) 114,
      (byte) 173,
      (byte) 81,
      (byte) 126,
      (byte) 91,
      (byte) 21,
      (byte) 172,
      (byte) 179,
      (byte) 158,
      (byte) 44,
      (byte) 147
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13232()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[7] = (byte) 147;
      numArray2[1] = (byte) 243;
      numArray2[9] = (byte) 243;
      numArray2[3] = (byte) 144 /*0x90*/;
      numArray2[6] = (byte) 122;
      numArray2[5] = (byte) 8;
      numArray2[8] = (byte) 212;
      numArray2[4] = (byte) 108;
      numArray2[0] = (byte) 181;
      numArray2[2] = (byte) 224 /*0xE0*/;
      numArray2[10] = (byte) 74;
      numArray2[11] = (byte) 239;
      byte[] numArray3 = new byte[12];
      numArray3[0] = (byte) 154;
      numArray3[11] = (byte) 239;
      numArray3[4] = (byte) 69;
      numArray3[10] = (byte) 64 /*0x40*/;
      numArray3[2] = (byte) 115;
      numArray3[5] = (byte) 183;
      numArray3[8] = (byte) 34;
      numArray3[7] = (byte) 154;
      numArray3[3] = (byte) 82;
      numArray3[6] = (byte) 189;
      numArray3[9] = (byte) 68;
      numArray3[1] = (byte) 17;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[7] = (byte) 43;
    numArray5[1] = (byte) 200;
    numArray5[2] = (byte) 222;
    numArray5[3] = (byte) 46;
    numArray5[0] = (byte) 79;
    numArray5[10] = (byte) 69;
    numArray5[11] = (byte) 236;
    numArray5[4] = (byte) 65;
    numArray5[8] = (byte) 180;
    numArray5[9] = (byte) 62;
    numArray5[5] = (byte) 206;
    numArray5[6] = (byte) 225;
    byte[] numArray6 = new byte[12]
    {
      (byte) 129,
      (byte) 195,
      (byte) 66,
      (byte) 102,
      (byte) 204,
      (byte) 118,
      (byte) 121,
      (byte) 98,
      (byte) 53,
      (byte) 249,
      (byte) 213,
      (byte) 176 /*0xB0*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13233()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[9];
      byte[] numArray2 = new byte[9]
      {
        (byte) 108,
        (byte) 176 /*0xB0*/,
        (byte) 230,
        (byte) 196,
        (byte) 1,
        (byte) 151,
        (byte) 153,
        (byte) 216,
        (byte) 137
      };
      byte[] numArray3 = new byte[9];
      numArray3[1] = (byte) 195;
      numArray3[3] = (byte) 0;
      numArray3[2] = (byte) 225;
      numArray3[4] = (byte) 228;
      numArray3[8] = (byte) 134;
      numArray3[5] = (byte) 196;
      numArray3[0] = (byte) 77;
      numArray3[7] = (byte) 144 /*0x90*/;
      numArray3[6] = (byte) 0;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[9];
    byte[] numArray5 = new byte[9];
    numArray5[0] = (byte) 191;
    numArray5[4] = (byte) 196;
    numArray5[1] = (byte) 180;
    numArray5[3] = (byte) 56;
    numArray5[7] = (byte) 223;
    numArray5[5] = (byte) 230;
    numArray5[6] = (byte) 70;
    numArray5[8] = (byte) 194;
    numArray5[2] = (byte) 108;
    byte[] numArray6 = new byte[9]
    {
      (byte) 82,
      (byte) 229,
      (byte) 48 /*0x30*/,
      (byte) 243,
      (byte) 31 /*0x1F*/,
      (byte) 102,
      (byte) 219,
      (byte) 45,
      (byte) 205
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 9);
    for (int index = 0; index < 9; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13234()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[2] = (byte) 122;
      numArray2[16 /*0x10*/] = (byte) 61;
      numArray2[9] = (byte) 88;
      numArray2[1] = (byte) 178;
      numArray2[6] = (byte) 149;
      numArray2[5] = (byte) 191;
      numArray2[13] = (byte) 251;
      numArray2[7] = (byte) 233;
      numArray2[8] = (byte) 117;
      numArray2[12] = (byte) 172;
      numArray2[10] = (byte) 97;
      numArray2[4] = (byte) 2;
      numArray2[3] = (byte) 52;
      numArray2[11] = (byte) 155;
      numArray2[14] = (byte) 121;
      numArray2[15] = (byte) 140;
      numArray2[0] = (byte) 28;
      numArray2[17] = (byte) 177;
      byte[] numArray3 = new byte[18]
      {
        (byte) 203,
        (byte) 232,
        (byte) 73,
        (byte) 88,
        (byte) 128 /*0x80*/,
        (byte) 222,
        (byte) 39,
        (byte) 174,
        (byte) 207,
        (byte) 4,
        (byte) 239,
        (byte) 185,
        (byte) 251,
        (byte) 197,
        (byte) 88,
        (byte) 149,
        (byte) 116,
        (byte) 73
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18];
    numArray5[0] = (byte) 135;
    numArray5[10] = (byte) 244;
    numArray5[2] = (byte) 216;
    numArray5[14] = (byte) 108;
    numArray5[1] = (byte) 73;
    numArray5[8] = (byte) 8;
    numArray5[6] = (byte) 31 /*0x1F*/;
    numArray5[7] = (byte) 18;
    numArray5[15] = (byte) 37;
    numArray5[17] = (byte) 112 /*0x70*/;
    numArray5[5] = (byte) 176 /*0xB0*/;
    numArray5[11] = (byte) 173;
    numArray5[3] = (byte) 138;
    numArray5[13] = (byte) 69;
    numArray5[9] = (byte) 222;
    numArray5[12] = (byte) 249;
    numArray5[16 /*0x10*/] = (byte) 231;
    numArray5[4] = (byte) 228;
    byte[] numArray6 = new byte[18]
    {
      (byte) 215,
      (byte) 139,
      (byte) 84,
      (byte) 19,
      (byte) 26,
      (byte) 31 /*0x1F*/,
      (byte) 19,
      (byte) 197,
      (byte) 218,
      (byte) 118,
      (byte) 151,
      (byte) 151,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 91,
      (byte) 167,
      (byte) 127 /*0x7F*/,
      (byte) 50
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13235()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[61];
      byte[] numArray2 = new byte[55];
      numArray2[7] = (byte) 123;
      numArray2[49] = (byte) 190;
      numArray2[2] = (byte) 61;
      numArray2[20] = (byte) 16 /*0x10*/;
      numArray2[4] = (byte) 189;
      numArray2[5] = (byte) 31 /*0x1F*/;
      numArray2[43] = (byte) 42;
      numArray2[53] = (byte) 209;
      numArray2[47] = (byte) 78;
      numArray2[42] = (byte) 186;
      numArray2[38] = (byte) 122;
      numArray2[11] = (byte) 74;
      numArray2[12] = (byte) 254;
      numArray2[34] = (byte) 78;
      numArray2[14] = (byte) 64 /*0x40*/;
      numArray2[28] = (byte) 120;
      numArray2[54] = (byte) 249;
      numArray2[13] = (byte) 94;
      numArray2[41] = (byte) 36;
      numArray2[19] = (byte) 141;
      numArray2[22] = (byte) 56;
      numArray2[21] = (byte) 170;
      numArray2[17] = (byte) 161;
      numArray2[23] = (byte) 164;
      numArray2[24] = (byte) 31 /*0x1F*/;
      numArray2[25] = (byte) 38;
      numArray2[26] = (byte) 0;
      numArray2[27] = (byte) 43;
      numArray2[0] = (byte) 227;
      numArray2[29] = (byte) 189;
      numArray2[30] = (byte) 54;
      numArray2[1] = (byte) 77;
      numArray2[39] = (byte) 136;
      numArray2[32 /*0x20*/] = (byte) 17;
      numArray2[16 /*0x10*/] = (byte) 175;
      numArray2[6] = (byte) 7;
      numArray2[36] = (byte) 59;
      numArray2[37] = (byte) 125;
      numArray2[50] = (byte) 0;
      numArray2[48 /*0x30*/] = (byte) 141;
      numArray2[40] = (byte) 79;
      numArray2[10] = (byte) 176 /*0xB0*/;
      numArray2[9] = (byte) 174;
      numArray2[33] = (byte) 182;
      numArray2[15] = (byte) 14;
      numArray2[45] = (byte) 116;
      numArray2[46] = (byte) 208 /*0xD0*/;
      numArray2[35] = (byte) 85;
      numArray2[3] = (byte) 28;
      numArray2[44] = (byte) 178;
      numArray2[8] = (byte) 185;
      numArray2[51] = (byte) 77;
      numArray2[52] = (byte) 200;
      numArray2[31 /*0x1F*/] = (byte) 60;
      numArray2[18] = (byte) 226;
      byte[] numArray3 = new byte[55]
      {
        (byte) 208 /*0xD0*/,
        (byte) 215,
        (byte) 92,
        (byte) 223,
        (byte) 46,
        (byte) 214,
        (byte) 120,
        (byte) 52,
        (byte) 175,
        (byte) 167,
        (byte) 89,
        (byte) 37,
        (byte) 210,
        (byte) 185,
        (byte) 142,
        (byte) 25,
        (byte) 19,
        (byte) 170,
        (byte) 193,
        (byte) 61,
        (byte) 130,
        (byte) 131,
        (byte) 101,
        (byte) 209,
        (byte) 254,
        (byte) 13,
        (byte) 113,
        (byte) 217,
        (byte) 237,
        (byte) 122,
        (byte) 79,
        (byte) 2,
        (byte) 91,
        (byte) 105,
        (byte) 69,
        (byte) 61,
        (byte) 163,
        (byte) 14,
        (byte) 101,
        (byte) 171,
        (byte) 239,
        (byte) 36,
        (byte) 93,
        (byte) 211,
        (byte) 15,
        (byte) 113,
        (byte) 165,
        (byte) 226,
        (byte) 159,
        (byte) 210,
        (byte) 85,
        (byte) 251,
        (byte) 173,
        (byte) 14,
        (byte) 125
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[6];
      numArray4[3] = (byte) 108;
      numArray4[0] = (byte) 166;
      numArray4[2] = (byte) 64 /*0x40*/;
      numArray4[4] = (byte) 63 /*0x3F*/;
      numArray4[1] = (byte) 82;
      numArray4[5] = (byte) 72;
      byte[] numArray5 = new byte[6];
      numArray5[4] = (byte) 95;
      numArray5[3] = (byte) 181;
      numArray5[2] = (byte) 72;
      numArray5[1] = (byte) 238;
      numArray5[0] = (byte) 45;
      numArray5[5] = (byte) 138;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[61];
    byte[] numArray7 = new byte[55];
    numArray7[13] = (byte) 252;
    numArray7[1] = (byte) 110;
    numArray7[2] = (byte) 205;
    numArray7[3] = (byte) 116;
    numArray7[25] = (byte) 247;
    numArray7[37] = (byte) 248;
    numArray7[36] = (byte) 115;
    numArray7[7] = (byte) 194;
    numArray7[39] = (byte) 121;
    numArray7[21] = (byte) 61;
    numArray7[15] = (byte) 162;
    numArray7[11] = (byte) 204;
    numArray7[33] = (byte) 171;
    numArray7[12] = (byte) 58;
    numArray7[14] = (byte) 47;
    numArray7[24] = (byte) 67;
    numArray7[26] = (byte) 188;
    numArray7[16 /*0x10*/] = (byte) 252;
    numArray7[53] = (byte) 179;
    numArray7[19] = (byte) 126;
    numArray7[43] = (byte) 74;
    numArray7[10] = (byte) 176 /*0xB0*/;
    numArray7[22] = (byte) 69;
    numArray7[23] = (byte) 253;
    numArray7[4] = (byte) 251;
    numArray7[17] = (byte) 51;
    numArray7[8] = (byte) 4;
    numArray7[27] = (byte) 30;
    numArray7[28] = (byte) 228;
    numArray7[41] = (byte) 203;
    numArray7[30] = (byte) 201;
    numArray7[9] = (byte) 172;
    numArray7[32 /*0x20*/] = (byte) 23;
    numArray7[6] = (byte) 238;
    numArray7[34] = (byte) 243;
    numArray7[35] = (byte) 227;
    numArray7[5] = (byte) 206;
    numArray7[29] = (byte) 125;
    numArray7[51] = (byte) 150;
    numArray7[18] = (byte) 217;
    numArray7[20] = (byte) 36;
    numArray7[54] = (byte) 54;
    numArray7[48 /*0x30*/] = (byte) 128 /*0x80*/;
    numArray7[42] = (byte) 251;
    numArray7[31 /*0x1F*/] = (byte) 66;
    numArray7[40] = (byte) 81;
    numArray7[46] = (byte) 112 /*0x70*/;
    numArray7[47] = (byte) 51;
    numArray7[38] = (byte) 68;
    numArray7[49] = (byte) 159;
    numArray7[50] = (byte) 244;
    numArray7[45] = (byte) 46;
    numArray7[52] = (byte) 241;
    numArray7[44] = (byte) 218;
    numArray7[0] = (byte) 179;
    byte[] numArray8 = new byte[55]
    {
      (byte) 189,
      (byte) 97,
      (byte) 32 /*0x20*/,
      (byte) 188,
      (byte) 149,
      (byte) 172,
      (byte) 74,
      (byte) 105,
      (byte) 63 /*0x3F*/,
      (byte) 225,
      (byte) 2,
      (byte) 235,
      (byte) 4,
      (byte) 15,
      (byte) 115,
      (byte) 189,
      (byte) 90,
      (byte) 162,
      (byte) 124,
      (byte) 130,
      (byte) 181,
      (byte) 184,
      (byte) 74,
      (byte) 166,
      (byte) 4,
      (byte) 77,
      (byte) 172,
      byte.MaxValue,
      (byte) 52,
      (byte) 192 /*0xC0*/,
      (byte) 191,
      (byte) 176 /*0xB0*/,
      (byte) 28,
      (byte) 100,
      (byte) 4,
      (byte) 241,
      (byte) 191,
      (byte) 69,
      (byte) 203,
      (byte) 82,
      (byte) 74,
      (byte) 14,
      (byte) 80 /*0x50*/,
      (byte) 207,
      (byte) 65,
      (byte) 125,
      (byte) 25,
      (byte) 96 /*0x60*/,
      (byte) 124,
      (byte) 3,
      (byte) 61,
      (byte) 203,
      (byte) 155,
      (byte) 216,
      (byte) 25
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[6]
    {
      (byte) 171,
      (byte) 28,
      (byte) 124,
      (byte) 137,
      (byte) 49,
      (byte) 153
    };
    byte[] numArray10 = new byte[6]
    {
      (byte) 84,
      (byte) 15,
      (byte) 230,
      (byte) 29,
      (byte) 165,
      (byte) 87
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 6);
    for (int index = 0; index < 6; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_13210.sspq, 186, (Array) numArray11, 0, 10);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_13210.sspr, 186, (Array) numArray11, 0, 10);
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

  internal static string ssp_appserver_13236()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[62];
      byte[] numArray2 = new byte[55]
      {
        (byte) 183,
        (byte) 154,
        (byte) 165,
        (byte) 94,
        (byte) 104,
        (byte) 212,
        (byte) 151,
        (byte) 139,
        (byte) 190,
        (byte) 220,
        (byte) 152,
        (byte) 73,
        (byte) 140,
        (byte) 248,
        (byte) 171,
        (byte) 196,
        (byte) 222,
        (byte) 80 /*0x50*/,
        (byte) 83,
        (byte) 118,
        (byte) 182,
        (byte) 79,
        (byte) 41,
        (byte) 200,
        (byte) 158,
        (byte) 165,
        (byte) 228,
        (byte) 181,
        (byte) 248,
        (byte) 154,
        (byte) 220,
        (byte) 112 /*0x70*/,
        (byte) 102,
        (byte) 155,
        (byte) 244,
        (byte) 130,
        (byte) 214,
        (byte) 42,
        (byte) 201,
        (byte) 196,
        (byte) 103,
        (byte) 36,
        (byte) 93,
        (byte) 116,
        (byte) 128 /*0x80*/,
        (byte) 66,
        (byte) 4,
        (byte) 147,
        (byte) 143,
        (byte) 233,
        (byte) 25,
        (byte) 90,
        (byte) 24,
        (byte) 86,
        (byte) 220
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 145,
        (byte) 188,
        (byte) 229,
        (byte) 91,
        (byte) 115,
        (byte) 79,
        (byte) 75,
        (byte) 251,
        (byte) 240 /*0xF0*/,
        (byte) 211,
        (byte) 131,
        (byte) 82,
        (byte) 218,
        (byte) 215,
        (byte) 115,
        (byte) 142,
        (byte) 166,
        (byte) 156,
        (byte) 181,
        (byte) 135,
        (byte) 2,
        (byte) 188,
        (byte) 241,
        (byte) 115,
        (byte) 102,
        (byte) 180,
        (byte) 137,
        (byte) 210,
        (byte) 31 /*0x1F*/,
        (byte) 36,
        (byte) 15,
        (byte) 36,
        (byte) 95,
        (byte) 69,
        (byte) 108,
        (byte) 81,
        (byte) 88,
        (byte) 0,
        (byte) 60,
        (byte) 87,
        (byte) 234,
        (byte) 241,
        (byte) 113,
        (byte) 207,
        (byte) 251,
        (byte) 64 /*0x40*/,
        (byte) 244,
        (byte) 85,
        (byte) 132,
        (byte) 100,
        (byte) 23,
        (byte) 204,
        (byte) 175,
        (byte) 185,
        (byte) 102
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[7]
      {
        (byte) 221,
        (byte) 192 /*0xC0*/,
        (byte) 195,
        (byte) 239,
        (byte) 225,
        (byte) 147,
        (byte) 36
      };
      byte[] numArray5 = new byte[7];
      numArray5[3] = (byte) 164;
      numArray5[1] = (byte) 167;
      numArray5[2] = (byte) 126;
      numArray5[0] = byte.MaxValue;
      numArray5[4] = (byte) 190;
      numArray5[5] = (byte) 72;
      numArray5[6] = (byte) 226;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[51];
      byte[] response = new byte[51];
      Array.Copy((Array) sc_13210.sspq, 196, (Array) numArray6, 0, 51);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13210.sspr, 196, (Array) numArray6, 0, 51);
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
    byte[] numArray7 = new byte[62];
    byte[] numArray8 = new byte[55]
    {
      (byte) 73,
      (byte) 128 /*0x80*/,
      (byte) 185,
      (byte) 137,
      (byte) 1,
      (byte) 184,
      (byte) 65,
      (byte) 238,
      (byte) 252,
      (byte) 141,
      (byte) 193,
      (byte) 191,
      (byte) 18,
      (byte) 28,
      (byte) 173,
      (byte) 207,
      (byte) 199,
      (byte) 116,
      (byte) 23,
      (byte) 84,
      (byte) 99,
      (byte) 5,
      (byte) 228,
      (byte) 54,
      (byte) 202,
      (byte) 12,
      (byte) 150,
      (byte) 245,
      (byte) 137,
      (byte) 140,
      (byte) 224 /*0xE0*/,
      (byte) 25,
      (byte) 77,
      (byte) 6,
      (byte) 27,
      (byte) 83,
      (byte) 207,
      (byte) 190,
      (byte) 73,
      (byte) 189,
      (byte) 123,
      (byte) 167,
      (byte) 104,
      (byte) 149,
      (byte) 221,
      (byte) 251,
      (byte) 113,
      (byte) 134,
      (byte) 208 /*0xD0*/,
      (byte) 0,
      (byte) 11,
      (byte) 190,
      (byte) 95,
      (byte) 196,
      (byte) 145
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 45,
      (byte) 148,
      (byte) 91,
      (byte) 52,
      (byte) 205,
      (byte) 149,
      (byte) 157,
      (byte) 77,
      (byte) 113,
      (byte) 64 /*0x40*/,
      (byte) 180,
      (byte) 141,
      (byte) 63 /*0x3F*/,
      (byte) 159,
      (byte) 48 /*0x30*/,
      (byte) 234,
      (byte) 17,
      (byte) 157,
      (byte) 198,
      (byte) 254,
      (byte) 189,
      (byte) 23,
      (byte) 158,
      (byte) 21,
      (byte) 201,
      (byte) 139,
      (byte) 115,
      (byte) 89,
      (byte) 36,
      (byte) 61,
      (byte) 243,
      (byte) 232,
      (byte) 72,
      (byte) 236,
      (byte) 147,
      (byte) 218,
      (byte) 6,
      (byte) 28,
      (byte) 95,
      (byte) 50,
      (byte) 66,
      (byte) 200,
      (byte) 190,
      (byte) 86,
      (byte) 38,
      (byte) 99,
      (byte) 178,
      (byte) 46,
      (byte) 208 /*0xD0*/,
      (byte) 9,
      (byte) 66,
      (byte) 107,
      (byte) 166,
      (byte) 191,
      (byte) 2
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[7]
    {
      (byte) 131,
      (byte) 190,
      (byte) 244,
      (byte) 33,
      (byte) 75,
      (byte) 100,
      (byte) 111
    };
    byte[] numArray11 = new byte[7];
    numArray11[1] = (byte) 102;
    numArray11[5] = (byte) 178;
    numArray11[0] = (byte) 230;
    numArray11[3] = (byte) 19;
    numArray11[2] = (byte) 112 /*0x70*/;
    numArray11[4] = (byte) 54;
    numArray11[6] = (byte) 137;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 7);
    for (int index = 0; index < 7; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13237()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 89
      };
      numArray2[1] = (byte) 156;
      numArray2[2] = (byte) 78;
      numArray2[0] = (byte) 187;
      byte[] numArray3 = new byte[4]
      {
        (byte) 47,
        (byte) 15,
        (byte) 219,
        (byte) 250
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[4];
    byte[] numArray5 = new byte[4]
    {
      (byte) 186,
      (byte) 137,
      (byte) 117,
      (byte) 215
    };
    byte[] numArray6 = new byte[4]
    {
      (byte) 145,
      (byte) 218,
      (byte) 173,
      (byte) 123
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13238()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 232,
        (byte) 53,
        (byte) 137,
        (byte) 101,
        (byte) 134,
        (byte) 118,
        (byte) 0,
        (byte) 2,
        (byte) 146,
        (byte) 5,
        (byte) 234
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 38,
        (byte) 148,
        (byte) 59,
        (byte) 114,
        (byte) 147,
        (byte) 88,
        (byte) 59,
        (byte) 201,
        (byte) 85,
        (byte) 140,
        (byte) 3
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[4] = (byte) 39;
    numArray5[1] = (byte) 239;
    numArray5[2] = (byte) 90;
    numArray5[10] = (byte) 151;
    numArray5[5] = (byte) 220;
    numArray5[3] = (byte) 17;
    numArray5[8] = (byte) 237;
    numArray5[7] = (byte) 137;
    numArray5[6] = (byte) 238;
    numArray5[0] = (byte) 42;
    numArray5[9] = (byte) 232;
    byte[] numArray6 = new byte[11];
    numArray6[10] = (byte) 112 /*0x70*/;
    numArray6[1] = (byte) 19;
    numArray6[2] = (byte) 80 /*0x50*/;
    numArray6[9] = (byte) 222;
    numArray6[4] = (byte) 115;
    numArray6[7] = (byte) 10;
    numArray6[6] = (byte) 162;
    numArray6[0] = (byte) 142;
    numArray6[8] = (byte) 206;
    numArray6[3] = (byte) 34;
    numArray6[5] = (byte) 202;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13239()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[29] = (byte) 248;
      numArray2[6] = (byte) 160 /*0xA0*/;
      numArray2[0] = (byte) 139;
      numArray2[3] = (byte) 10;
      numArray2[4] = (byte) 95;
      numArray2[5] = (byte) 71;
      numArray2[27] = (byte) 74;
      numArray2[7] = (byte) 206;
      numArray2[8] = (byte) 93;
      numArray2[11] = (byte) 250;
      numArray2[18] = (byte) 6;
      numArray2[1] = (byte) 8;
      numArray2[12] = (byte) 187;
      numArray2[19] = (byte) 170;
      numArray2[17] = (byte) 231;
      numArray2[15] = (byte) 93;
      numArray2[28] = (byte) 248;
      numArray2[31 /*0x1F*/] = (byte) 145;
      numArray2[13] = (byte) 72;
      numArray2[2] = (byte) 102;
      numArray2[20] = (byte) 37;
      numArray2[21] = (byte) 135;
      numArray2[9] = (byte) 212;
      numArray2[10] = (byte) 77;
      numArray2[24] = (byte) 111;
      numArray2[33] = (byte) 221;
      numArray2[26] = (byte) 56;
      numArray2[14] = (byte) 65;
      numArray2[22] = (byte) 120;
      numArray2[16 /*0x10*/] = (byte) 3;
      numArray2[30] = (byte) 55;
      numArray2[23] = (byte) 84;
      numArray2[32 /*0x20*/] = (byte) 247;
      numArray2[25] = (byte) 249;
      numArray2[34] = (byte) 50;
      numArray2[35] = (byte) 161;
      byte[] numArray3 = new byte[36];
      numArray3[34] = (byte) 132;
      numArray3[1] = (byte) 25;
      numArray3[2] = (byte) 145;
      numArray3[21] = (byte) 252;
      numArray3[4] = (byte) 67;
      numArray3[20] = (byte) 9;
      numArray3[6] = (byte) 218;
      numArray3[26] = (byte) 65;
      numArray3[7] = (byte) 98;
      numArray3[9] = (byte) 160 /*0xA0*/;
      numArray3[5] = (byte) 205;
      numArray3[19] = (byte) 124;
      numArray3[12] = (byte) 92;
      numArray3[10] = (byte) 39;
      numArray3[14] = (byte) 80 /*0x50*/;
      numArray3[15] = (byte) 180;
      numArray3[16 /*0x10*/] = (byte) 253;
      numArray3[17] = (byte) 145;
      numArray3[18] = (byte) 145;
      numArray3[33] = (byte) 20;
      numArray3[31 /*0x1F*/] = (byte) 239;
      numArray3[13] = (byte) 90;
      numArray3[22] = (byte) 250;
      numArray3[23] = (byte) 146;
      numArray3[32 /*0x20*/] = (byte) 194;
      numArray3[3] = (byte) 149;
      numArray3[24] = (byte) 44;
      numArray3[27] = (byte) 180;
      numArray3[28] = (byte) 92;
      numArray3[11] = (byte) 69;
      numArray3[30] = (byte) 137;
      numArray3[0] = (byte) 125;
      numArray3[29] = (byte) 1;
      numArray3[25] = (byte) 51;
      numArray3[8] = (byte) 173;
      numArray3[35] = (byte) 187;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[24];
      byte[] response = new byte[24];
      Array.Copy((Array) sc_13210.sspq, 247, (Array) numArray4, 0, 24);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 247, (Array) numArray4, 0, 24);
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
    byte[] numArray5 = new byte[36];
    byte[] numArray6 = new byte[36]
    {
      (byte) 117,
      (byte) 46,
      (byte) 194,
      (byte) 9,
      (byte) 18,
      (byte) 131,
      (byte) 48 /*0x30*/,
      (byte) 243,
      (byte) 135,
      (byte) 32 /*0x20*/,
      (byte) 6,
      (byte) 77,
      (byte) 96 /*0x60*/,
      (byte) 138,
      (byte) 227,
      (byte) 113,
      (byte) 45,
      (byte) 86,
      (byte) 56,
      (byte) 16 /*0x10*/,
      (byte) 31 /*0x1F*/,
      (byte) 112 /*0x70*/,
      (byte) 47,
      (byte) 168,
      (byte) 85,
      (byte) 98,
      (byte) 149,
      (byte) 168,
      (byte) 230,
      (byte) 18,
      (byte) 144 /*0x90*/,
      (byte) 94,
      (byte) 71,
      (byte) 15,
      (byte) 205,
      (byte) 59
    };
    byte[] numArray7 = new byte[36]
    {
      (byte) 124,
      (byte) 81,
      (byte) 118,
      (byte) 31 /*0x1F*/,
      (byte) 92,
      (byte) 44,
      (byte) 157,
      (byte) 128 /*0x80*/,
      (byte) 40,
      (byte) 208 /*0xD0*/,
      (byte) 95,
      (byte) 206,
      (byte) 56,
      (byte) 106,
      (byte) 134,
      (byte) 230,
      (byte) 71,
      (byte) 79,
      byte.MaxValue,
      (byte) 31 /*0x1F*/,
      (byte) 9,
      (byte) 38,
      (byte) 33,
      (byte) 68,
      (byte) 67,
      (byte) 10,
      (byte) 213,
      (byte) 60,
      (byte) 79,
      (byte) 19,
      (byte) 144 /*0x90*/,
      (byte) 237,
      (byte) 95,
      (byte) 223,
      (byte) 62,
      (byte) 158
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13240()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[10] = (byte) 103;
      numArray2[1] = (byte) 226;
      numArray2[11] = (byte) 10;
      numArray2[5] = (byte) 31 /*0x1F*/;
      numArray2[17] = (byte) 26;
      numArray2[3] = (byte) 110;
      numArray2[2] = (byte) 95;
      numArray2[12] = (byte) 98;
      numArray2[8] = (byte) 212;
      numArray2[9] = (byte) 188;
      numArray2[6] = (byte) 222;
      numArray2[4] = (byte) 70;
      numArray2[7] = (byte) 209;
      numArray2[13] = (byte) 87;
      numArray2[14] = (byte) 17;
      numArray2[15] = (byte) 5;
      numArray2[16 /*0x10*/] = byte.MaxValue;
      numArray2[0] = (byte) 64 /*0x40*/;
      numArray2[18] = (byte) 179;
      byte[] numArray3 = new byte[19]
      {
        (byte) 149,
        (byte) 121,
        (byte) 22,
        (byte) 85,
        (byte) 42,
        (byte) 91,
        (byte) 98,
        (byte) 126,
        (byte) 89,
        (byte) 103,
        (byte) 186,
        (byte) 208 /*0xD0*/,
        (byte) 222,
        (byte) 202,
        (byte) 86,
        (byte) 61,
        (byte) 130,
        (byte) 119,
        (byte) 232
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19];
    numArray5[0] = (byte) 196;
    numArray5[16 /*0x10*/] = (byte) 235;
    numArray5[2] = (byte) 41;
    numArray5[11] = (byte) 220;
    numArray5[1] = (byte) 211;
    numArray5[9] = (byte) 59;
    numArray5[6] = (byte) 240 /*0xF0*/;
    numArray5[7] = (byte) 253;
    numArray5[3] = (byte) 171;
    numArray5[12] = (byte) 37;
    numArray5[10] = (byte) 65;
    numArray5[5] = (byte) 252;
    numArray5[8] = (byte) 200;
    numArray5[13] = (byte) 212;
    numArray5[14] = (byte) 184;
    numArray5[15] = (byte) 213;
    numArray5[4] = (byte) 134;
    numArray5[17] = (byte) 222;
    numArray5[18] = (byte) 158;
    byte[] numArray6 = new byte[19]
    {
      (byte) 190,
      (byte) 99,
      (byte) 68,
      (byte) 178,
      (byte) 94,
      (byte) 37,
      (byte) 166,
      (byte) 198,
      (byte) 238,
      (byte) 181,
      (byte) 153,
      (byte) 236,
      (byte) 71,
      (byte) 36,
      (byte) 109,
      (byte) 221,
      (byte) 170,
      (byte) 0,
      (byte) 76
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13241()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[42];
      byte[] numArray2 = new byte[42]
      {
        (byte) 244,
        (byte) 108,
        (byte) 213,
        (byte) 191,
        (byte) 91,
        (byte) 161,
        (byte) 23,
        (byte) 102,
        (byte) 36,
        (byte) 226,
        (byte) 195,
        (byte) 28,
        (byte) 246,
        (byte) 176 /*0xB0*/,
        (byte) 167,
        (byte) 130,
        (byte) 223,
        (byte) 253,
        (byte) 16 /*0x10*/,
        (byte) 104,
        (byte) 40,
        (byte) 211,
        (byte) 253,
        (byte) 136,
        (byte) 95,
        (byte) 249,
        (byte) 171,
        (byte) 4,
        (byte) 242,
        (byte) 34,
        (byte) 214,
        (byte) 243,
        (byte) 4,
        (byte) 57,
        (byte) 31 /*0x1F*/,
        (byte) 192 /*0xC0*/,
        (byte) 209,
        (byte) 11,
        (byte) 45,
        (byte) 74,
        (byte) 111,
        (byte) 110
      };
      byte[] numArray3 = new byte[42]
      {
        (byte) 153,
        (byte) 95,
        (byte) 129,
        (byte) 192 /*0xC0*/,
        (byte) 49,
        (byte) 209,
        (byte) 234,
        (byte) 156,
        (byte) 217,
        (byte) 223,
        (byte) 74,
        (byte) 245,
        (byte) 39,
        (byte) 220,
        (byte) 157,
        (byte) 254,
        (byte) 71,
        (byte) 220,
        (byte) 59,
        (byte) 157,
        (byte) 95,
        (byte) 251,
        (byte) 132,
        (byte) 230,
        (byte) 216,
        (byte) 162,
        (byte) 16 /*0x10*/,
        (byte) 83,
        (byte) 68,
        byte.MaxValue,
        (byte) 2,
        (byte) 242,
        (byte) 178,
        (byte) 69,
        (byte) 6,
        (byte) 111,
        (byte) 139,
        (byte) 14,
        (byte) 150,
        (byte) 205,
        (byte) 107,
        (byte) 36
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 42);
      for (int index = 0; index < 42; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[42];
    byte[] numArray5 = new byte[42];
    numArray5[37] = (byte) 247;
    numArray5[1] = (byte) 54;
    numArray5[20] = (byte) 207;
    numArray5[23] = (byte) 145;
    numArray5[2] = (byte) 121;
    numArray5[5] = (byte) 233;
    numArray5[33] = (byte) 147;
    numArray5[7] = (byte) 154;
    numArray5[17] = (byte) 116;
    numArray5[22] = (byte) 188;
    numArray5[10] = (byte) 33;
    numArray5[34] = (byte) 254;
    numArray5[12] = (byte) 76;
    numArray5[13] = (byte) 96 /*0x60*/;
    numArray5[30] = (byte) 185;
    numArray5[15] = (byte) 246;
    numArray5[41] = (byte) 89;
    numArray5[8] = (byte) 172;
    numArray5[18] = (byte) 2;
    numArray5[28] = (byte) 198;
    numArray5[4] = (byte) 96 /*0x60*/;
    numArray5[21] = (byte) 23;
    numArray5[25] = (byte) 177;
    numArray5[3] = (byte) 12;
    numArray5[16 /*0x10*/] = (byte) 19;
    numArray5[26] = (byte) 212;
    numArray5[32 /*0x20*/] = (byte) 162;
    numArray5[38] = (byte) 145;
    numArray5[35] = (byte) 195;
    numArray5[29] = (byte) 249;
    numArray5[39] = (byte) 56;
    numArray5[31 /*0x1F*/] = (byte) 3;
    numArray5[19] = (byte) 12;
    numArray5[24] = (byte) 231;
    numArray5[6] = (byte) 10;
    numArray5[0] = (byte) 97;
    numArray5[36] = (byte) 49;
    numArray5[27] = (byte) 208 /*0xD0*/;
    numArray5[14] = (byte) 98;
    numArray5[9] = (byte) 43;
    numArray5[40] = (byte) 203;
    numArray5[11] = (byte) 110;
    byte[] numArray6 = new byte[42]
    {
      (byte) 233,
      (byte) 68,
      (byte) 189,
      (byte) 55,
      (byte) 176 /*0xB0*/,
      (byte) 11,
      (byte) 249,
      (byte) 150,
      (byte) 45,
      (byte) 27,
      (byte) 172,
      (byte) 54,
      (byte) 85,
      (byte) 8,
      (byte) 206,
      (byte) 220,
      (byte) 116,
      (byte) 81,
      (byte) 225,
      (byte) 240 /*0xF0*/,
      (byte) 104,
      (byte) 201,
      (byte) 53,
      (byte) 215,
      (byte) 38,
      (byte) 43,
      (byte) 74,
      (byte) 145,
      (byte) 12,
      (byte) 48 /*0x30*/,
      (byte) 184,
      (byte) 20,
      (byte) 67,
      (byte) 109,
      (byte) 8,
      (byte) 117,
      (byte) 129,
      (byte) 216,
      (byte) 3,
      (byte) 200,
      (byte) 41,
      (byte) 13
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 42);
    for (int index = 0; index < 42; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13242()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 110,
        (byte) 180,
        (byte) 73,
        (byte) 73,
        (byte) 209,
        (byte) 225,
        (byte) 35,
        (byte) 145,
        (byte) 132,
        (byte) 165,
        (byte) 156,
        (byte) 223
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 146,
        (byte) 103,
        (byte) 250,
        (byte) 38,
        (byte) 103,
        (byte) 30,
        (byte) 212,
        (byte) 159,
        (byte) 25,
        (byte) 231,
        (byte) 55,
        (byte) 139
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 44,
      (byte) 142,
      (byte) 64 /*0x40*/,
      (byte) 22,
      (byte) 4,
      (byte) 14,
      (byte) 175,
      (byte) 138,
      (byte) 202,
      (byte) 84,
      (byte) 143,
      (byte) 131
    };
    byte[] numArray6 = new byte[12]
    {
      (byte) 10,
      (byte) 218,
      (byte) 93,
      (byte) 71,
      (byte) 155,
      (byte) 2,
      (byte) 211,
      (byte) 125,
      (byte) 46,
      (byte) 113,
      (byte) 230,
      (byte) 228
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[25];
    byte[] response = new byte[25];
    Array.Copy((Array) sc_13210.sspq, 271, (Array) numArray7, 0, 25);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13210.sspr, 271, (Array) numArray7, 0, 25);
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

  internal static string ssp_appserver_13243()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 127 /*0x7F*/,
        (byte) 245,
        (byte) 65,
        (byte) 93,
        (byte) 56,
        (byte) 92,
        (byte) 34
      };
      byte[] numArray3 = new byte[7];
      numArray3[5] = (byte) 199;
      numArray3[0] = (byte) 64 /*0x40*/;
      numArray3[2] = (byte) 166;
      numArray3[3] = (byte) 48 /*0x30*/;
      numArray3[4] = (byte) 173;
      numArray3[6] = (byte) 158;
      numArray3[1] = (byte) 97;
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
      (byte) 94,
      (byte) 190,
      (byte) 113,
      (byte) 183,
      (byte) 160 /*0xA0*/,
      (byte) 127 /*0x7F*/
    };
    byte[] numArray6 = new byte[7];
    numArray6[3] = (byte) 32 /*0x20*/;
    numArray6[1] = (byte) 153;
    numArray6[2] = (byte) 49;
    numArray6[0] = (byte) 45;
    numArray6[4] = (byte) 115;
    numArray6[5] = (byte) 44;
    numArray6[6] = (byte) 106;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13244()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7];
      numArray2[0] = (byte) 133;
      numArray2[2] = (byte) 164;
      numArray2[1] = (byte) 8;
      numArray2[3] = (byte) 109;
      numArray2[4] = (byte) 82;
      numArray2[5] = (byte) 206;
      numArray2[6] = (byte) 138;
      byte[] numArray3 = new byte[7];
      numArray3[6] = (byte) 248;
      numArray3[5] = (byte) 219;
      numArray3[0] = (byte) 8;
      numArray3[3] = (byte) 146;
      numArray3[2] = (byte) 92;
      numArray3[1] = (byte) 48 /*0x30*/;
      numArray3[4] = (byte) 117;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 45,
      (byte) 0,
      (byte) 164,
      (byte) 213,
      (byte) 227,
      (byte) 64 /*0x40*/,
      (byte) 217
    };
    byte[] numArray6 = new byte[7]
    {
      (byte) 172,
      (byte) 242,
      (byte) 85,
      (byte) 232,
      (byte) 150,
      (byte) 203,
      (byte) 203
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13245()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[2] = (byte) 240 /*0xF0*/;
      numArray2[9] = (byte) 74;
      numArray2[3] = (byte) 11;
      numArray2[7] = (byte) 227;
      numArray2[4] = (byte) 239;
      numArray2[5] = (byte) 231;
      numArray2[6] = (byte) 143;
      numArray2[1] = (byte) 112 /*0x70*/;
      numArray2[8] = (byte) 124;
      numArray2[0] = (byte) 237;
      byte[] numArray3 = new byte[10]
      {
        (byte) 208 /*0xD0*/,
        (byte) 182,
        (byte) 149,
        (byte) 88,
        (byte) 122,
        (byte) 142,
        (byte) 210,
        (byte) 150,
        (byte) 95,
        (byte) 146
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
      (byte) 63 /*0x3F*/,
      (byte) 82,
      (byte) 42,
      (byte) 180,
      (byte) 68,
      (byte) 124,
      (byte) 41,
      (byte) 227,
      (byte) 45,
      (byte) 1
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 148,
      (byte) 139,
      (byte) 218,
      (byte) 137,
      (byte) 141,
      (byte) 235,
      (byte) 216,
      (byte) 136,
      (byte) 63 /*0x3F*/,
      (byte) 92
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13246(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 16 /*0x10*/,
      (byte) 209,
      (byte) 56,
      (byte) 113,
      (byte) 69,
      (byte) 238,
      (byte) 104,
      (byte) 166,
      (byte) 245,
      (byte) 129,
      (byte) 112 /*0x70*/,
      (byte) 93,
      (byte) 141,
      (byte) 107,
      (byte) 77,
      (byte) 243,
      (byte) 94,
      (byte) 167,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 185,
      (byte) 197,
      (byte) 50,
      (byte) 75,
      (byte) 103,
      (byte) 83,
      (byte) 102,
      (byte) 151,
      (byte) 88,
      (byte) 110,
      (byte) 219,
      (byte) 79,
      (byte) 0,
      (byte) 113,
      (byte) 123,
      (byte) 249,
      (byte) 213,
      (byte) 191,
      (byte) 1,
      (byte) 125,
      (byte) 215,
      (byte) 172,
      (byte) 53,
      (byte) 51,
      (byte) 212,
      (byte) 30,
      (byte) 155,
      (byte) 119
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 64 /*0x40*/,
      (byte) 100,
      (byte) 221,
      (byte) 253,
      (byte) 135,
      (byte) 47,
      (byte) 19,
      (byte) 123,
      (byte) 82,
      (byte) 241,
      (byte) 254,
      (byte) 215,
      (byte) 248,
      (byte) 83,
      (byte) 55,
      (byte) 29,
      (byte) 154,
      (byte) 16 /*0x10*/,
      (byte) 30,
      (byte) 167,
      (byte) 116,
      (byte) 205,
      (byte) 243,
      (byte) 145,
      (byte) 129,
      (byte) 42,
      (byte) 204,
      (byte) 67,
      (byte) 114,
      (byte) 3,
      (byte) 93,
      (byte) 238,
      (byte) 225,
      (byte) 122,
      byte.MaxValue,
      (byte) 19,
      (byte) 182,
      (byte) 197,
      (byte) 16 /*0x10*/,
      (byte) 145,
      (byte) 174,
      (byte) 107,
      (byte) 126,
      (byte) 187,
      (byte) 132,
      (byte) 13,
      (byte) 139,
      (byte) 46
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[15];
    byte[] response2 = new byte[15];
    Array.Copy((Array) sc_13210.sspq, 296, (Array) numArray2, 0, 15);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13210.sspr, 296, (Array) numArray2, 0, 15);
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

  internal static string ssp_appserver_13247()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[193];
      byte[] numArray2 = new byte[55];
      numArray2[9] = (byte) 254;
      numArray2[1] = (byte) 84;
      numArray2[40] = (byte) 88;
      numArray2[3] = (byte) 159;
      numArray2[4] = (byte) 166;
      numArray2[5] = (byte) 135;
      numArray2[44] = (byte) 118;
      numArray2[8] = (byte) 144 /*0x90*/;
      numArray2[19] = (byte) 206;
      numArray2[36] = (byte) 238;
      numArray2[10] = (byte) 118;
      numArray2[11] = (byte) 73;
      numArray2[50] = (byte) 249;
      numArray2[13] = (byte) 238;
      numArray2[14] = (byte) 183;
      numArray2[15] = (byte) 75;
      numArray2[16 /*0x10*/] = (byte) 140;
      numArray2[17] = (byte) 10;
      numArray2[18] = (byte) 47;
      numArray2[6] = (byte) 217;
      numArray2[34] = (byte) 134;
      numArray2[49] = (byte) 1;
      numArray2[37] = (byte) 66;
      numArray2[23] = (byte) 231;
      numArray2[42] = (byte) 186;
      numArray2[25] = (byte) 15;
      numArray2[30] = (byte) 141;
      numArray2[2] = (byte) 30;
      numArray2[31 /*0x1F*/] = (byte) 179;
      numArray2[35] = (byte) 139;
      numArray2[20] = (byte) 79;
      numArray2[0] = (byte) 240 /*0xF0*/;
      numArray2[32 /*0x20*/] = (byte) 194;
      numArray2[33] = (byte) 127 /*0x7F*/;
      numArray2[29] = (byte) 174;
      numArray2[28] = (byte) 95;
      numArray2[52] = (byte) 241;
      numArray2[47] = (byte) 153;
      numArray2[24] = (byte) 181;
      numArray2[38] = (byte) 43;
      numArray2[26] = (byte) 245;
      numArray2[45] = (byte) 192 /*0xC0*/;
      numArray2[27] = (byte) 5;
      numArray2[48 /*0x30*/] = (byte) 25;
      numArray2[53] = (byte) 141;
      numArray2[43] = (byte) 160 /*0xA0*/;
      numArray2[46] = (byte) 58;
      numArray2[39] = (byte) 151;
      numArray2[22] = (byte) 89;
      numArray2[7] = (byte) 12;
      numArray2[21] = (byte) 48 /*0x30*/;
      numArray2[51] = (byte) 155;
      numArray2[41] = (byte) 21;
      numArray2[12] = (byte) 183;
      numArray2[54] = (byte) 204;
      byte[] numArray3 = new byte[55]
      {
        (byte) 186,
        (byte) 218,
        (byte) 168,
        (byte) 7,
        (byte) 0,
        (byte) 160 /*0xA0*/,
        (byte) 191,
        (byte) 15,
        (byte) 218,
        (byte) 152,
        (byte) 127 /*0x7F*/,
        (byte) 108,
        (byte) 111,
        (byte) 83,
        (byte) 187,
        (byte) 126,
        (byte) 231,
        (byte) 213,
        (byte) 134,
        (byte) 123,
        (byte) 211,
        (byte) 115,
        (byte) 2,
        (byte) 73,
        (byte) 213,
        (byte) 232,
        (byte) 145,
        (byte) 195,
        (byte) 58,
        (byte) 210,
        (byte) 102,
        (byte) 60,
        (byte) 13,
        (byte) 191,
        (byte) 83,
        (byte) 178,
        (byte) 15,
        (byte) 79,
        (byte) 61,
        (byte) 127 /*0x7F*/,
        (byte) 27,
        (byte) 228,
        (byte) 40,
        (byte) 100,
        (byte) 169,
        (byte) 75,
        (byte) 82,
        (byte) 75,
        (byte) 42,
        (byte) 242,
        (byte) 174,
        (byte) 135,
        (byte) 114,
        (byte) 240 /*0xF0*/,
        (byte) 64 /*0x40*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 198,
        (byte) 244,
        (byte) 193,
        (byte) 52,
        (byte) 231,
        (byte) 80 /*0x50*/,
        (byte) 44,
        (byte) 127 /*0x7F*/,
        (byte) 0,
        (byte) 82,
        (byte) 131,
        (byte) 161,
        (byte) 155,
        (byte) 165,
        (byte) 241,
        (byte) 74,
        (byte) 145,
        (byte) 241,
        (byte) 210,
        (byte) 156,
        (byte) 138,
        (byte) 188,
        (byte) 187,
        (byte) 210,
        (byte) 43,
        (byte) 254,
        (byte) 184,
        (byte) 6,
        (byte) 52,
        (byte) 105,
        (byte) 29,
        (byte) 34,
        (byte) 122,
        (byte) 79,
        (byte) 241,
        (byte) 99,
        (byte) 210,
        (byte) 209,
        (byte) 157,
        (byte) 23,
        (byte) 172,
        (byte) 174,
        (byte) 179,
        (byte) 231,
        (byte) 127 /*0x7F*/,
        (byte) 143,
        (byte) 82,
        byte.MaxValue,
        (byte) 189,
        (byte) 113,
        (byte) 199,
        (byte) 37,
        (byte) 190,
        (byte) 174,
        (byte) 182
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 17,
        (byte) 117,
        (byte) 87,
        (byte) 96 /*0x60*/,
        (byte) 146,
        (byte) 208 /*0xD0*/,
        (byte) 127 /*0x7F*/,
        (byte) 244,
        (byte) 251,
        (byte) 7,
        (byte) 86,
        (byte) 199,
        (byte) 54,
        (byte) 116,
        (byte) 25,
        (byte) 19,
        (byte) 28,
        (byte) 191,
        (byte) 26,
        (byte) 187,
        (byte) 79,
        (byte) 88,
        (byte) 229,
        byte.MaxValue,
        (byte) 54,
        (byte) 243,
        (byte) 178,
        (byte) 180,
        (byte) 212,
        (byte) 47,
        (byte) 159,
        (byte) 5,
        (byte) 136,
        (byte) 209,
        (byte) 244,
        (byte) 95,
        (byte) 75,
        (byte) 218,
        (byte) 174,
        (byte) 216,
        (byte) 15,
        (byte) 46,
        (byte) 220,
        (byte) 220,
        (byte) 146,
        (byte) 22,
        (byte) 208 /*0xD0*/,
        (byte) 238,
        (byte) 207,
        (byte) 49,
        (byte) 103,
        (byte) 125,
        (byte) 101,
        (byte) 19,
        (byte) 247
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[17] = (byte) 240 /*0xF0*/;
      numArray6[1] = (byte) 9;
      numArray6[2] = (byte) 233;
      numArray6[14] = (byte) 191;
      numArray6[28] = (byte) 248;
      numArray6[5] = (byte) 75;
      numArray6[6] = (byte) 150;
      numArray6[38] = (byte) 133;
      numArray6[8] = (byte) 97;
      numArray6[20] = (byte) 212;
      numArray6[7] = (byte) 52;
      numArray6[39] = (byte) 99;
      numArray6[54] = (byte) 100;
      numArray6[13] = (byte) 120;
      numArray6[40] = (byte) 28;
      numArray6[34] = (byte) 227;
      numArray6[3] = (byte) 151;
      numArray6[12] = (byte) 142;
      numArray6[18] = (byte) 219;
      numArray6[4] = (byte) 38;
      numArray6[11] = (byte) 179;
      numArray6[21] = (byte) 82;
      numArray6[0] = (byte) 239;
      numArray6[23] = (byte) 171;
      numArray6[24] = (byte) 185;
      numArray6[19] = (byte) 9;
      numArray6[26] = (byte) 194;
      numArray6[27] = (byte) 162;
      numArray6[10] = (byte) 75;
      numArray6[29] = (byte) 99;
      numArray6[31 /*0x1F*/] = (byte) 152;
      numArray6[46] = (byte) 46;
      numArray6[32 /*0x20*/] = (byte) 232;
      numArray6[16 /*0x10*/] = (byte) 187;
      numArray6[33] = (byte) 42;
      numArray6[35] = (byte) 200;
      numArray6[51] = (byte) 189;
      numArray6[37] = (byte) 128 /*0x80*/;
      numArray6[30] = (byte) 204;
      numArray6[42] = (byte) 29;
      numArray6[22] = (byte) 155;
      numArray6[41] = (byte) 178;
      numArray6[36] = (byte) 179;
      numArray6[43] = (byte) 104;
      numArray6[44] = (byte) 6;
      numArray6[45] = (byte) 75;
      numArray6[15] = (byte) 84;
      numArray6[47] = (byte) 249;
      numArray6[48 /*0x30*/] = (byte) 7;
      numArray6[25] = (byte) 73;
      numArray6[50] = (byte) 3;
      numArray6[49] = (byte) 208 /*0xD0*/;
      numArray6[52] = (byte) 218;
      numArray6[53] = byte.MaxValue;
      numArray6[9] = (byte) 38;
      byte[] numArray7 = new byte[55]
      {
        (byte) 34,
        (byte) 2,
        (byte) 252,
        (byte) 52,
        (byte) 16 /*0x10*/,
        (byte) 160 /*0xA0*/,
        (byte) 205,
        (byte) 109,
        (byte) 120,
        (byte) 222,
        (byte) 84,
        (byte) 0,
        (byte) 194,
        (byte) 157,
        (byte) 83,
        (byte) 221,
        (byte) 235,
        (byte) 169,
        (byte) 179,
        (byte) 66,
        (byte) 240 /*0xF0*/,
        (byte) 134,
        (byte) 3,
        (byte) 191,
        (byte) 10,
        (byte) 38,
        (byte) 59,
        (byte) 119,
        (byte) 211,
        (byte) 134,
        (byte) 148,
        (byte) 32 /*0x20*/,
        (byte) 73,
        (byte) 174,
        (byte) 163,
        (byte) 14,
        (byte) 162,
        (byte) 139,
        (byte) 13,
        (byte) 113,
        (byte) 213,
        (byte) 160 /*0xA0*/,
        (byte) 253,
        (byte) 199,
        (byte) 92,
        (byte) 182,
        (byte) 248,
        (byte) 206,
        (byte) 109,
        (byte) 250,
        (byte) 102,
        (byte) 40,
        (byte) 105,
        (byte) 157,
        (byte) 159
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[28]
      {
        (byte) 153,
        (byte) 111,
        (byte) 195,
        (byte) 191,
        (byte) 89,
        (byte) 20,
        (byte) 158,
        (byte) 74,
        (byte) 244,
        (byte) 226,
        (byte) 157,
        (byte) 78,
        (byte) 155,
        (byte) 42,
        (byte) 153,
        (byte) 195,
        (byte) 156,
        (byte) 196,
        (byte) 171,
        (byte) 21,
        (byte) 36,
        (byte) 80 /*0x50*/,
        (byte) 238,
        (byte) 78,
        (byte) 100,
        (byte) 138,
        (byte) 248,
        (byte) 151
      };
      byte[] numArray9 = new byte[28]
      {
        (byte) 134,
        (byte) 181,
        (byte) 165,
        (byte) 41,
        (byte) 22,
        (byte) 197,
        (byte) 211,
        (byte) 179,
        (byte) 153,
        (byte) 82,
        (byte) 177,
        (byte) 135,
        (byte) 185,
        (byte) 133,
        (byte) 82,
        (byte) 94,
        (byte) 67,
        (byte) 224 /*0xE0*/,
        (byte) 99,
        (byte) 116,
        (byte) 32 /*0x20*/,
        (byte) 149,
        (byte) 30,
        (byte) 97,
        (byte) 107,
        (byte) 220,
        (byte) 87,
        (byte) 252
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[193];
    byte[] numArray11 = new byte[55];
    numArray11[26] = (byte) 116;
    numArray11[3] = (byte) 221;
    numArray11[21] = (byte) 151;
    numArray11[6] = (byte) 27;
    numArray11[4] = (byte) 50;
    numArray11[5] = (byte) 229;
    numArray11[37] = (byte) 77;
    numArray11[16 /*0x10*/] = (byte) 5;
    numArray11[35] = (byte) 112 /*0x70*/;
    numArray11[25] = (byte) 129;
    numArray11[1] = (byte) 222;
    numArray11[22] = (byte) 82;
    numArray11[12] = (byte) 156;
    numArray11[13] = (byte) 46;
    numArray11[14] = (byte) 21;
    numArray11[45] = (byte) 240 /*0xF0*/;
    numArray11[52] = (byte) 46;
    numArray11[9] = (byte) 129;
    numArray11[18] = (byte) 143;
    numArray11[46] = (byte) 81;
    numArray11[11] = (byte) 95;
    numArray11[48 /*0x30*/] = (byte) 78;
    numArray11[39] = (byte) 221;
    numArray11[23] = (byte) 22;
    numArray11[20] = (byte) 78;
    numArray11[40] = (byte) 129;
    numArray11[15] = (byte) 116;
    numArray11[28] = (byte) 206;
    numArray11[33] = (byte) 13;
    numArray11[10] = (byte) 195;
    numArray11[8] = (byte) 97;
    numArray11[31 /*0x1F*/] = (byte) 141;
    numArray11[32 /*0x20*/] = (byte) 153;
    numArray11[19] = (byte) 197;
    numArray11[50] = (byte) 242;
    numArray11[24] = (byte) 72;
    numArray11[7] = (byte) 206;
    numArray11[36] = (byte) 123;
    numArray11[2] = (byte) 32 /*0x20*/;
    numArray11[38] = (byte) 103;
    numArray11[17] = (byte) 47;
    numArray11[41] = (byte) 245;
    numArray11[42] = (byte) 73;
    numArray11[43] = (byte) 148;
    numArray11[44] = (byte) 96 /*0x60*/;
    numArray11[27] = (byte) 91;
    numArray11[29] = (byte) 28;
    numArray11[47] = (byte) 32 /*0x20*/;
    numArray11[0] = (byte) 130;
    numArray11[49] = (byte) 22;
    numArray11[30] = (byte) 7;
    numArray11[51] = (byte) 103;
    numArray11[34] = (byte) 26;
    numArray11[53] = (byte) 149;
    numArray11[54] = (byte) 135;
    byte[] numArray12 = new byte[55];
    numArray12[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray12[41] = (byte) 249;
    numArray12[29] = (byte) 158;
    numArray12[3] = (byte) 240 /*0xF0*/;
    numArray12[44] = (byte) 213;
    numArray12[43] = (byte) 218;
    numArray12[6] = (byte) 8;
    numArray12[10] = (byte) 132;
    numArray12[23] = (byte) 210;
    numArray12[9] = (byte) 50;
    numArray12[5] = (byte) 144 /*0x90*/;
    numArray12[4] = (byte) 195;
    numArray12[12] = (byte) 123;
    numArray12[35] = (byte) 154;
    numArray12[14] = (byte) 117;
    numArray12[15] = (byte) 218;
    numArray12[17] = (byte) 174;
    numArray12[7] = (byte) 158;
    numArray12[18] = (byte) 57;
    numArray12[19] = (byte) 80 /*0x50*/;
    numArray12[20] = (byte) 10;
    numArray12[21] = (byte) 76;
    numArray12[22] = (byte) 203;
    numArray12[1] = (byte) 133;
    numArray12[24] = (byte) 244;
    numArray12[25] = (byte) 117;
    numArray12[26] = (byte) 67;
    numArray12[27] = (byte) 33;
    numArray12[54] = (byte) 112 /*0x70*/;
    numArray12[11] = (byte) 135;
    numArray12[31 /*0x1F*/] = (byte) 37;
    numArray12[28] = (byte) 61;
    numArray12[30] = (byte) 93;
    numArray12[33] = (byte) 109;
    numArray12[34] = (byte) 198;
    numArray12[13] = (byte) 237;
    numArray12[36] = (byte) 164;
    numArray12[37] = (byte) 103;
    numArray12[38] = (byte) 174;
    numArray12[39] = (byte) 125;
    numArray12[40] = (byte) 173;
    numArray12[0] = (byte) 104;
    numArray12[42] = (byte) 232;
    numArray12[8] = (byte) 93;
    numArray12[32 /*0x20*/] = (byte) 33;
    numArray12[45] = (byte) 63 /*0x3F*/;
    numArray12[48 /*0x30*/] = (byte) 31 /*0x1F*/;
    numArray12[47] = (byte) 117;
    numArray12[46] = (byte) 112 /*0x70*/;
    numArray12[49] = (byte) 41;
    numArray12[50] = (byte) 158;
    numArray12[51] = (byte) 33;
    numArray12[52] = (byte) 222;
    numArray12[53] = (byte) 75;
    numArray12[2] = (byte) 138;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 200,
      (byte) 14,
      (byte) 20,
      (byte) 212,
      (byte) 78,
      (byte) 173,
      (byte) 45,
      (byte) 217,
      (byte) 244,
      (byte) 16 /*0x10*/,
      (byte) 176 /*0xB0*/,
      (byte) 81,
      (byte) 172,
      (byte) 108,
      (byte) 130,
      (byte) 221,
      (byte) 242,
      (byte) 117,
      (byte) 183,
      (byte) 97,
      (byte) 98,
      (byte) 56,
      (byte) 198,
      (byte) 180,
      (byte) 31 /*0x1F*/,
      (byte) 125,
      (byte) 6,
      (byte) 177,
      (byte) 114,
      (byte) 169,
      (byte) 229,
      (byte) 143,
      (byte) 63 /*0x3F*/,
      (byte) 233,
      (byte) 84,
      (byte) 110,
      (byte) 32 /*0x20*/,
      (byte) 125,
      (byte) 191,
      byte.MaxValue,
      (byte) 186,
      (byte) 102,
      (byte) 95,
      (byte) 86,
      (byte) 8,
      (byte) 214,
      (byte) 59,
      (byte) 40,
      (byte) 186,
      (byte) 176 /*0xB0*/,
      (byte) 71,
      (byte) 87,
      (byte) 17,
      (byte) 241,
      (byte) 201
    };
    byte[] numArray14 = new byte[55];
    numArray14[38] = (byte) 13;
    numArray14[43] = (byte) 134;
    numArray14[4] = (byte) 98;
    numArray14[5] = (byte) 244;
    numArray14[46] = (byte) 254;
    numArray14[36] = (byte) 136;
    numArray14[29] = (byte) 153;
    numArray14[13] = (byte) 155;
    numArray14[52] = (byte) 95;
    numArray14[41] = (byte) 20;
    numArray14[10] = (byte) 70;
    numArray14[11] = (byte) 40;
    numArray14[12] = (byte) 17;
    numArray14[34] = (byte) 109;
    numArray14[26] = (byte) 62;
    numArray14[51] = (byte) 68;
    numArray14[16 /*0x10*/] = (byte) 131;
    numArray14[23] = (byte) 217;
    numArray14[2] = (byte) 90;
    numArray14[50] = (byte) 245;
    numArray14[20] = (byte) 190;
    numArray14[21] = (byte) 221;
    numArray14[22] = (byte) 169;
    numArray14[17] = (byte) 118;
    numArray14[7] = (byte) 73;
    numArray14[25] = (byte) 74;
    numArray14[47] = (byte) 218;
    numArray14[27] = (byte) 254;
    numArray14[24] = (byte) 231;
    numArray14[14] = (byte) 85;
    numArray14[30] = (byte) 99;
    numArray14[9] = (byte) 197;
    numArray14[32 /*0x20*/] = (byte) 34;
    numArray14[33] = (byte) 214;
    numArray14[0] = (byte) 71;
    numArray14[35] = (byte) 230;
    numArray14[6] = (byte) 237;
    numArray14[3] = (byte) 29;
    numArray14[18] = (byte) 70;
    numArray14[39] = (byte) 36;
    numArray14[40] = (byte) 236;
    numArray14[15] = (byte) 21;
    numArray14[42] = (byte) 139;
    numArray14[44] = (byte) 225;
    numArray14[37] = (byte) 42;
    numArray14[45] = (byte) 53;
    numArray14[28] = (byte) 234;
    numArray14[1] = (byte) 52;
    numArray14[48 /*0x30*/] = (byte) 188;
    numArray14[49] = (byte) 65;
    numArray14[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray14[54] = (byte) 67;
    numArray14[19] = (byte) 0;
    numArray14[53] = byte.MaxValue;
    numArray14[8] = (byte) 145;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 180,
      (byte) 106,
      (byte) 143,
      (byte) 200,
      (byte) 132,
      (byte) 164,
      (byte) 232,
      (byte) 157,
      (byte) 163,
      (byte) 196,
      (byte) 162,
      (byte) 84,
      (byte) 237,
      (byte) 251,
      (byte) 171,
      (byte) 199,
      (byte) 95,
      (byte) 104,
      (byte) 251,
      (byte) 196,
      (byte) 68,
      (byte) 197,
      (byte) 133,
      (byte) 201,
      (byte) 205,
      (byte) 81,
      (byte) 206,
      (byte) 143,
      (byte) 236,
      (byte) 36,
      (byte) 3,
      (byte) 81,
      (byte) 12,
      (byte) 78,
      (byte) 12,
      (byte) 94,
      (byte) 95,
      (byte) 144 /*0x90*/,
      (byte) 186,
      (byte) 91,
      (byte) 43,
      (byte) 142,
      (byte) 67,
      byte.MaxValue,
      (byte) 214,
      (byte) 188,
      (byte) 185,
      (byte) 249,
      (byte) 38,
      (byte) 217,
      (byte) 250,
      (byte) 225,
      (byte) 211,
      (byte) 242,
      (byte) 147
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 201,
      (byte) 215,
      (byte) 208 /*0xD0*/,
      (byte) 196,
      (byte) 118,
      (byte) 171,
      (byte) 85,
      (byte) 32 /*0x20*/,
      (byte) 98,
      (byte) 170,
      (byte) 221,
      (byte) 18,
      (byte) 5,
      (byte) 95,
      (byte) 215,
      (byte) 102,
      (byte) 118,
      (byte) 174,
      (byte) 196,
      (byte) 244,
      (byte) 62,
      (byte) 73,
      (byte) 254,
      (byte) 89,
      (byte) 163,
      (byte) 224 /*0xE0*/,
      (byte) 7,
      (byte) 135,
      (byte) 183,
      (byte) 188,
      (byte) 238,
      (byte) 22,
      (byte) 96 /*0x60*/,
      (byte) 117,
      (byte) 29,
      (byte) 24,
      (byte) 129,
      (byte) 217,
      (byte) 180,
      (byte) 111,
      (byte) 192 /*0xC0*/,
      (byte) 12,
      (byte) 49,
      (byte) 58,
      (byte) 83,
      (byte) 180,
      (byte) 12,
      (byte) 146,
      (byte) 73,
      (byte) 52,
      (byte) 43,
      (byte) 210,
      (byte) 65,
      (byte) 113,
      (byte) 189
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[28]
    {
      (byte) 197,
      (byte) 250,
      (byte) 50,
      (byte) 214,
      (byte) 89,
      (byte) 177,
      (byte) 127 /*0x7F*/,
      (byte) 192 /*0xC0*/,
      (byte) 80 /*0x50*/,
      (byte) 71,
      (byte) 158,
      (byte) 238,
      (byte) 76,
      (byte) 104,
      (byte) 206,
      (byte) 174,
      (byte) 159,
      (byte) 152,
      (byte) 182,
      (byte) 129,
      (byte) 133,
      (byte) 242,
      (byte) 17,
      (byte) 151,
      (byte) 213,
      (byte) 105,
      (byte) 18,
      (byte) 41
    };
    byte[] numArray18 = new byte[28]
    {
      (byte) 126,
      (byte) 19,
      (byte) 17,
      (byte) 231,
      (byte) 118,
      (byte) 75,
      (byte) 56,
      (byte) 201,
      (byte) 1,
      (byte) 127 /*0x7F*/,
      (byte) 80 /*0x50*/,
      (byte) 138,
      (byte) 31 /*0x1F*/,
      (byte) 85,
      (byte) 101,
      (byte) 16 /*0x10*/,
      (byte) 72,
      (byte) 87,
      (byte) 191,
      (byte) 185,
      (byte) 10,
      (byte) 146,
      (byte) 252,
      (byte) 164,
      (byte) 220,
      (byte) 101,
      (byte) 206,
      (byte) 121
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 28);
    for (int index = 0; index < 28; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_13248()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7];
      numArray2[3] = (byte) 149;
      numArray2[1] = (byte) 121;
      numArray2[4] = (byte) 135;
      numArray2[6] = (byte) 210;
      numArray2[2] = (byte) 153;
      numArray2[5] = (byte) 133;
      numArray2[0] = (byte) 29;
      byte[] numArray3 = new byte[7];
      numArray3[1] = (byte) 145;
      numArray3[2] = (byte) 198;
      numArray3[0] = (byte) 5;
      numArray3[5] = (byte) 41;
      numArray3[4] = (byte) 72;
      numArray3[3] = (byte) 150;
      numArray3[6] = (byte) 252;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7];
    numArray5[2] = (byte) 0;
    numArray5[4] = (byte) 159;
    numArray5[1] = (byte) 155;
    numArray5[3] = (byte) 240 /*0xF0*/;
    numArray5[0] = (byte) 232;
    numArray5[5] = (byte) 85;
    numArray5[6] = (byte) 158;
    byte[] numArray6 = new byte[7]
    {
      (byte) 86,
      (byte) 152,
      (byte) 67,
      (byte) 243,
      (byte) 161,
      (byte) 126,
      (byte) 111
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13249()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[50];
      byte[] numArray2 = new byte[50]
      {
        (byte) 189,
        (byte) 71,
        (byte) 108,
        (byte) 157,
        (byte) 124,
        (byte) 202,
        (byte) 143,
        (byte) 160 /*0xA0*/,
        (byte) 254,
        (byte) 239,
        (byte) 74,
        (byte) 192 /*0xC0*/,
        (byte) 222,
        (byte) 176 /*0xB0*/,
        (byte) 26,
        (byte) 198,
        (byte) 173,
        (byte) 235,
        (byte) 132,
        (byte) 226,
        (byte) 61,
        (byte) 110,
        (byte) 159,
        (byte) 179,
        (byte) 71,
        (byte) 56,
        (byte) 58,
        (byte) 6,
        (byte) 162,
        (byte) 243,
        (byte) 10,
        (byte) 25,
        (byte) 147,
        (byte) 3,
        (byte) 175,
        (byte) 23,
        (byte) 102,
        (byte) 75,
        (byte) 31 /*0x1F*/,
        (byte) 53,
        (byte) 96 /*0x60*/,
        (byte) 254,
        (byte) 87,
        (byte) 174,
        (byte) 136,
        (byte) 108,
        (byte) 3,
        (byte) 32 /*0x20*/,
        (byte) 184,
        (byte) 8
      };
      byte[] numArray3 = new byte[50];
      numArray3[23] = (byte) 10;
      numArray3[0] = (byte) 128 /*0x80*/;
      numArray3[2] = (byte) 69;
      numArray3[44] = (byte) 195;
      numArray3[10] = (byte) 152;
      numArray3[24] = (byte) 172;
      numArray3[6] = (byte) 228;
      numArray3[18] = (byte) 75;
      numArray3[28] = (byte) 251;
      numArray3[9] = (byte) 211;
      numArray3[1] = (byte) 250;
      numArray3[42] = (byte) 250;
      numArray3[14] = (byte) 61;
      numArray3[13] = (byte) 135;
      numArray3[11] = (byte) 17;
      numArray3[27] = (byte) 91;
      numArray3[15] = (byte) 158;
      numArray3[17] = (byte) 182;
      numArray3[32 /*0x20*/] = (byte) 168;
      numArray3[37] = (byte) 152;
      numArray3[20] = (byte) 78;
      numArray3[21] = (byte) 116;
      numArray3[22] = (byte) 42;
      numArray3[12] = (byte) 215;
      numArray3[8] = (byte) 25;
      numArray3[45] = (byte) 251;
      numArray3[26] = (byte) 118;
      numArray3[34] = (byte) 180;
      numArray3[49] = (byte) 3;
      numArray3[29] = (byte) 27;
      numArray3[30] = (byte) 67;
      numArray3[43] = (byte) 222;
      numArray3[4] = (byte) 216;
      numArray3[47] = (byte) 73;
      numArray3[25] = (byte) 102;
      numArray3[33] = (byte) 179;
      numArray3[19] = (byte) 76;
      numArray3[41] = (byte) 101;
      numArray3[38] = (byte) 157;
      numArray3[39] = (byte) 207;
      numArray3[40] = (byte) 228;
      numArray3[16 /*0x10*/] = (byte) 95;
      numArray3[3] = (byte) 74;
      numArray3[48 /*0x30*/] = (byte) 170;
      numArray3[31 /*0x1F*/] = (byte) 213;
      numArray3[36] = (byte) 60;
      numArray3[46] = (byte) 32 /*0x20*/;
      numArray3[35] = (byte) 198;
      numArray3[5] = (byte) 200;
      numArray3[7] = (byte) 191;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[50];
    byte[] numArray5 = new byte[50]
    {
      (byte) 182,
      (byte) 168,
      (byte) 42,
      (byte) 111,
      (byte) 70,
      (byte) 98,
      (byte) 92,
      (byte) 55,
      (byte) 182,
      (byte) 143,
      (byte) 122,
      (byte) 13,
      (byte) 118,
      (byte) 59,
      (byte) 99,
      (byte) 163,
      (byte) 21,
      (byte) 172,
      (byte) 126,
      (byte) 116,
      (byte) 160 /*0xA0*/,
      (byte) 146,
      (byte) 178,
      (byte) 107,
      (byte) 240 /*0xF0*/,
      (byte) 169,
      (byte) 169,
      (byte) 62,
      (byte) 202,
      (byte) 38,
      (byte) 210,
      (byte) 155,
      (byte) 14,
      (byte) 250,
      (byte) 151,
      (byte) 241,
      (byte) 61,
      (byte) 96 /*0x60*/,
      (byte) 62,
      (byte) 76,
      (byte) 10,
      (byte) 40,
      (byte) 185,
      (byte) 29,
      (byte) 123,
      (byte) 206,
      (byte) 46,
      (byte) 157,
      (byte) 157,
      (byte) 169
    };
    byte[] numArray6 = new byte[50];
    numArray6[35] = (byte) 153;
    numArray6[0] = (byte) 82;
    numArray6[37] = (byte) 63 /*0x3F*/;
    numArray6[39] = (byte) 189;
    numArray6[23] = (byte) 85;
    numArray6[5] = (byte) 244;
    numArray6[45] = (byte) 234;
    numArray6[40] = (byte) 180;
    numArray6[44] = (byte) 113;
    numArray6[46] = (byte) 99;
    numArray6[30] = (byte) 176 /*0xB0*/;
    numArray6[11] = byte.MaxValue;
    numArray6[12] = (byte) 54;
    numArray6[25] = (byte) 9;
    numArray6[14] = (byte) 85;
    numArray6[15] = (byte) 226;
    numArray6[16 /*0x10*/] = (byte) 105;
    numArray6[4] = (byte) 221;
    numArray6[18] = (byte) 191;
    numArray6[19] = (byte) 20;
    numArray6[36] = (byte) 165;
    numArray6[41] = (byte) 18;
    numArray6[7] = (byte) 52;
    numArray6[49] = (byte) 154;
    numArray6[22] = (byte) 227;
    numArray6[17] = (byte) 7;
    numArray6[9] = (byte) 211;
    numArray6[27] = (byte) 108;
    numArray6[28] = (byte) 224 /*0xE0*/;
    numArray6[33] = (byte) 223;
    numArray6[20] = (byte) 114;
    numArray6[26] = (byte) 190;
    numArray6[6] = (byte) 238;
    numArray6[31 /*0x1F*/] = (byte) 190;
    numArray6[1] = (byte) 19;
    numArray6[2] = (byte) 4;
    numArray6[42] = (byte) 60;
    numArray6[21] = (byte) 106;
    numArray6[38] = (byte) 112 /*0x70*/;
    numArray6[32 /*0x20*/] = (byte) 89;
    numArray6[24] = (byte) 10;
    numArray6[29] = (byte) 64 /*0x40*/;
    numArray6[8] = (byte) 181;
    numArray6[43] = (byte) 65;
    numArray6[34] = (byte) 131;
    numArray6[3] = (byte) 181;
    numArray6[10] = (byte) 167;
    numArray6[47] = (byte) 75;
    numArray6[48 /*0x30*/] = (byte) 69;
    numArray6[13] = (byte) 49;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 50);
    for (int index = 0; index < 50; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13250(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 254,
      (byte) 62,
      (byte) 144 /*0x90*/,
      (byte) 71,
      (byte) 6,
      (byte) 74,
      (byte) 96 /*0x60*/,
      (byte) 206,
      (byte) 91,
      (byte) 33,
      (byte) 172,
      (byte) 124,
      (byte) 74,
      (byte) 29,
      (byte) 150,
      (byte) 6,
      (byte) 164,
      (byte) 1,
      (byte) 47,
      (byte) 117,
      (byte) 231,
      (byte) 211,
      (byte) 204,
      (byte) 157,
      (byte) 126,
      (byte) 188,
      (byte) 175,
      (byte) 52,
      (byte) 84,
      (byte) 84,
      (byte) 201,
      (byte) 19,
      (byte) 117,
      (byte) 222,
      (byte) 115,
      (byte) 232,
      (byte) 73,
      (byte) 19,
      (byte) 116,
      (byte) 40,
      (byte) 155,
      (byte) 175,
      (byte) 134,
      (byte) 128 /*0x80*/,
      (byte) 231,
      (byte) 111,
      (byte) 112 /*0x70*/,
      (byte) 11
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 30,
      (byte) 32 /*0x20*/,
      (byte) 131,
      (byte) 209,
      (byte) 97,
      (byte) 27,
      (byte) 130,
      (byte) 232,
      (byte) 27,
      (byte) 35,
      (byte) 44,
      (byte) 241,
      (byte) 222,
      (byte) 174,
      (byte) 36,
      (byte) 5,
      (byte) 123,
      (byte) 113,
      (byte) 72,
      (byte) 153,
      (byte) 86,
      (byte) 216,
      (byte) 190,
      (byte) 129,
      (byte) 91,
      (byte) 93,
      (byte) 56,
      (byte) 101,
      (byte) 15,
      (byte) 130,
      (byte) 71,
      (byte) 27,
      (byte) 176 /*0xB0*/,
      (byte) 63 /*0x3F*/,
      (byte) 52,
      (byte) 109,
      (byte) 120,
      (byte) 88,
      (byte) 104,
      (byte) 11,
      (byte) 47,
      (byte) 180,
      (byte) 246,
      (byte) 175,
      (byte) 70,
      (byte) 220,
      (byte) 11,
      (byte) 134
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13251()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[67];
      byte[] numArray2 = new byte[55]
      {
        (byte) 201,
        (byte) 183,
        (byte) 91,
        (byte) 43,
        (byte) 113,
        (byte) 242,
        (byte) 201,
        (byte) 73,
        (byte) 155,
        (byte) 44,
        (byte) 121,
        (byte) 7,
        (byte) 87,
        (byte) 153,
        (byte) 7,
        (byte) 108,
        (byte) 170,
        (byte) 173,
        (byte) 188,
        (byte) 144 /*0x90*/,
        (byte) 140,
        (byte) 7,
        (byte) 217,
        (byte) 55,
        (byte) 76,
        (byte) 75,
        (byte) 181,
        (byte) 15,
        (byte) 76,
        (byte) 163,
        (byte) 105,
        (byte) 191,
        (byte) 41,
        (byte) 25,
        (byte) 73,
        (byte) 189,
        (byte) 131,
        (byte) 71,
        (byte) 118,
        (byte) 87,
        (byte) 74,
        (byte) 42,
        (byte) 141,
        (byte) 110,
        (byte) 53,
        (byte) 127 /*0x7F*/,
        (byte) 41,
        (byte) 78,
        (byte) 103,
        (byte) 4,
        (byte) 96 /*0x60*/,
        (byte) 29,
        (byte) 216,
        (byte) 154,
        (byte) 36
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 92,
        (byte) 115,
        (byte) 16 /*0x10*/,
        (byte) 40,
        (byte) 114,
        (byte) 235,
        (byte) 56,
        (byte) 203,
        (byte) 126,
        (byte) 192 /*0xC0*/,
        (byte) 47,
        (byte) 32 /*0x20*/,
        (byte) 0,
        (byte) 164,
        (byte) 180,
        (byte) 61,
        (byte) 107,
        (byte) 251,
        (byte) 38,
        (byte) 169,
        (byte) 36,
        (byte) 192 /*0xC0*/,
        (byte) 109,
        (byte) 149,
        (byte) 112 /*0x70*/,
        (byte) 219,
        (byte) 179,
        (byte) 166,
        (byte) 188,
        (byte) 89,
        (byte) 223,
        (byte) 229,
        (byte) 236,
        (byte) 135,
        (byte) 221,
        (byte) 208 /*0xD0*/,
        (byte) 154,
        (byte) 203,
        (byte) 40,
        (byte) 247,
        (byte) 225,
        (byte) 87,
        (byte) 54,
        (byte) 4,
        (byte) 172,
        (byte) 5,
        (byte) 61,
        (byte) 31 /*0x1F*/,
        (byte) 147,
        (byte) 33,
        (byte) 14,
        (byte) 93,
        (byte) 73,
        (byte) 186,
        (byte) 166
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      numArray4[6] = (byte) 247;
      numArray4[3] = (byte) 60;
      numArray4[2] = (byte) 38;
      numArray4[7] = (byte) 195;
      numArray4[11] = (byte) 79;
      numArray4[5] = (byte) 253;
      numArray4[4] = (byte) 185;
      numArray4[1] = (byte) 222;
      numArray4[8] = (byte) 186;
      numArray4[0] = (byte) 35;
      numArray4[10] = (byte) 62;
      numArray4[9] = (byte) 221;
      byte[] numArray5 = new byte[12];
      numArray5[11] = (byte) 209;
      numArray5[7] = (byte) 208 /*0xD0*/;
      numArray5[2] = (byte) 22;
      numArray5[3] = (byte) 175;
      numArray5[4] = (byte) 217;
      numArray5[1] = (byte) 26;
      numArray5[0] = (byte) 85;
      numArray5[5] = (byte) 177;
      numArray5[8] = (byte) 99;
      numArray5[6] = (byte) 136;
      numArray5[10] = (byte) 128 /*0x80*/;
      numArray5[9] = (byte) 196;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[67];
    byte[] numArray7 = new byte[55];
    numArray7[4] = (byte) 162;
    numArray7[34] = (byte) 52;
    numArray7[2] = (byte) 171;
    numArray7[20] = (byte) 18;
    numArray7[35] = (byte) 228;
    numArray7[5] = (byte) 234;
    numArray7[53] = (byte) 69;
    numArray7[13] = (byte) 195;
    numArray7[0] = (byte) 45;
    numArray7[9] = (byte) 254;
    numArray7[10] = (byte) 230;
    numArray7[11] = (byte) 115;
    numArray7[31 /*0x1F*/] = (byte) 230;
    numArray7[43] = (byte) 68;
    numArray7[8] = (byte) 27;
    numArray7[15] = (byte) 114;
    numArray7[16 /*0x10*/] = (byte) 33;
    numArray7[17] = (byte) 17;
    numArray7[22] = (byte) 181;
    numArray7[19] = (byte) 213;
    numArray7[3] = (byte) 123;
    numArray7[21] = (byte) 182;
    numArray7[51] = (byte) 87;
    numArray7[41] = (byte) 138;
    numArray7[24] = (byte) 211;
    numArray7[54] = (byte) 126;
    numArray7[12] = (byte) 84;
    numArray7[27] = (byte) 200;
    numArray7[28] = (byte) 105;
    numArray7[29] = (byte) 137;
    numArray7[30] = (byte) 249;
    numArray7[23] = (byte) 119;
    numArray7[32 /*0x20*/] = (byte) 74;
    numArray7[33] = (byte) 180;
    numArray7[42] = (byte) 44;
    numArray7[1] = (byte) 149;
    numArray7[36] = (byte) 191;
    numArray7[45] = (byte) 115;
    numArray7[38] = (byte) 43;
    numArray7[39] = (byte) 55;
    numArray7[40] = (byte) 104;
    numArray7[18] = (byte) 3;
    numArray7[14] = (byte) 72;
    numArray7[6] = (byte) 254;
    numArray7[44] = (byte) 101;
    numArray7[50] = (byte) 61;
    numArray7[7] = (byte) 29;
    numArray7[47] = (byte) 49;
    numArray7[48 /*0x30*/] = (byte) 62;
    numArray7[49] = (byte) 109;
    numArray7[46] = (byte) 58;
    numArray7[26] = (byte) 73;
    numArray7[52] = (byte) 76;
    numArray7[37] = (byte) 134;
    numArray7[25] = (byte) 77;
    byte[] numArray8 = new byte[55];
    numArray8[5] = (byte) 36;
    numArray8[8] = (byte) 35;
    numArray8[24] = (byte) 15;
    numArray8[13] = (byte) 10;
    numArray8[14] = (byte) 42;
    numArray8[40] = (byte) 31 /*0x1F*/;
    numArray8[6] = (byte) 91;
    numArray8[22] = (byte) 203;
    numArray8[1] = (byte) 201;
    numArray8[9] = (byte) 118;
    numArray8[10] = (byte) 192 /*0xC0*/;
    numArray8[11] = (byte) 24;
    numArray8[12] = (byte) 250;
    numArray8[28] = (byte) 147;
    numArray8[42] = (byte) 113;
    numArray8[44] = (byte) 190;
    numArray8[16 /*0x10*/] = (byte) 212;
    numArray8[17] = (byte) 77;
    numArray8[27] = (byte) 44;
    numArray8[19] = (byte) 119;
    numArray8[20] = (byte) 90;
    numArray8[32 /*0x20*/] = (byte) 206;
    numArray8[7] = (byte) 78;
    numArray8[23] = (byte) 53;
    numArray8[53] = (byte) 201;
    numArray8[46] = (byte) 7;
    numArray8[26] = (byte) 112 /*0x70*/;
    numArray8[2] = (byte) 88;
    numArray8[45] = (byte) 206;
    numArray8[29] = (byte) 35;
    numArray8[30] = (byte) 79;
    numArray8[3] = (byte) 12;
    numArray8[21] = (byte) 198;
    numArray8[34] = (byte) 199;
    numArray8[25] = (byte) 24;
    numArray8[35] = (byte) 200;
    numArray8[18] = (byte) 2;
    numArray8[37] = (byte) 235;
    numArray8[48 /*0x30*/] = (byte) 137;
    numArray8[0] = (byte) 202;
    numArray8[15] = (byte) 127 /*0x7F*/;
    numArray8[41] = (byte) 249;
    numArray8[39] = (byte) 163;
    numArray8[43] = (byte) 58;
    numArray8[33] = (byte) 246;
    numArray8[38] = (byte) 33;
    numArray8[31 /*0x1F*/] = (byte) 67;
    numArray8[47] = (byte) 69;
    numArray8[51] = (byte) 2;
    numArray8[49] = (byte) 135;
    numArray8[50] = (byte) 96 /*0x60*/;
    numArray8[36] = (byte) 109;
    numArray8[52] = (byte) 14;
    numArray8[4] = (byte) 216;
    numArray8[54] = (byte) 247;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[12]
    {
      (byte) 115,
      (byte) 50,
      (byte) 201,
      (byte) 18,
      (byte) 253,
      (byte) 118,
      (byte) 158,
      (byte) 49,
      (byte) 73,
      (byte) 245,
      (byte) 42,
      (byte) 1
    };
    byte[] numArray10 = new byte[12]
    {
      (byte) 206,
      (byte) 238,
      (byte) 174,
      (byte) 201,
      (byte) 215,
      (byte) 16 /*0x10*/,
      (byte) 185,
      (byte) 85,
      (byte) 147,
      (byte) 201,
      (byte) 247,
      (byte) 104
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 12);
    for (int index = 0; index < 12; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13252()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[56];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 207;
      numArray2[33] = (byte) 233;
      numArray2[22] = (byte) 136;
      numArray2[42] = (byte) 109;
      numArray2[51] = (byte) 117;
      numArray2[5] = (byte) 171;
      numArray2[25] = (byte) 173;
      numArray2[46] = (byte) 99;
      numArray2[23] = (byte) 166;
      numArray2[41] = (byte) 130;
      numArray2[10] = (byte) 176 /*0xB0*/;
      numArray2[11] = (byte) 193;
      numArray2[12] = (byte) 205;
      numArray2[1] = (byte) 181;
      numArray2[14] = (byte) 144 /*0x90*/;
      numArray2[15] = (byte) 250;
      numArray2[16 /*0x10*/] = (byte) 211;
      numArray2[40] = (byte) 69;
      numArray2[3] = (byte) 157;
      numArray2[19] = (byte) 100;
      numArray2[6] = (byte) 237;
      numArray2[21] = (byte) 251;
      numArray2[13] = (byte) 209;
      numArray2[28] = (byte) 6;
      numArray2[24] = (byte) 179;
      numArray2[17] = (byte) 84;
      numArray2[2] = (byte) 148;
      numArray2[4] = (byte) 80 /*0x50*/;
      numArray2[26] = (byte) 183;
      numArray2[9] = (byte) 50;
      numArray2[30] = (byte) 178;
      numArray2[27] = (byte) 50;
      numArray2[32 /*0x20*/] = (byte) 192 /*0xC0*/;
      numArray2[52] = (byte) 197;
      numArray2[36] = (byte) 175;
      numArray2[45] = (byte) 220;
      numArray2[20] = (byte) 204;
      numArray2[37] = (byte) 95;
      numArray2[38] = (byte) 239;
      numArray2[39] = (byte) 199;
      numArray2[7] = (byte) 70;
      numArray2[31 /*0x1F*/] = (byte) 212;
      numArray2[34] = (byte) 34;
      numArray2[35] = (byte) 231;
      numArray2[44] = (byte) 198;
      numArray2[18] = (byte) 247;
      numArray2[8] = (byte) 165;
      numArray2[47] = (byte) 120;
      numArray2[48 /*0x30*/] = (byte) 100;
      numArray2[49] = (byte) 47;
      numArray2[50] = (byte) 45;
      numArray2[0] = (byte) 39;
      numArray2[43] = (byte) 203;
      numArray2[53] = (byte) 193;
      numArray2[54] = (byte) 172;
      byte[] numArray3 = new byte[55];
      numArray3[52] = (byte) 203;
      numArray3[10] = (byte) 28;
      numArray3[7] = (byte) 242;
      numArray3[28] = (byte) 26;
      numArray3[3] = (byte) 13;
      numArray3[47] = (byte) 69;
      numArray3[46] = (byte) 59;
      numArray3[44] = (byte) 43;
      numArray3[8] = (byte) 25;
      numArray3[29] = (byte) 64 /*0x40*/;
      numArray3[49] = (byte) 32 /*0x20*/;
      numArray3[11] = (byte) 155;
      numArray3[40] = (byte) 236;
      numArray3[13] = (byte) 169;
      numArray3[14] = (byte) 225;
      numArray3[15] = (byte) 125;
      numArray3[17] = (byte) 103;
      numArray3[2] = (byte) 25;
      numArray3[18] = (byte) 89;
      numArray3[51] = (byte) 203;
      numArray3[31 /*0x1F*/] = (byte) 185;
      numArray3[21] = (byte) 19;
      numArray3[22] = (byte) 29;
      numArray3[23] = (byte) 206;
      numArray3[48 /*0x30*/] = (byte) 241;
      numArray3[25] = (byte) 106;
      numArray3[26] = (byte) 242;
      numArray3[34] = (byte) 160 /*0xA0*/;
      numArray3[16 /*0x10*/] = (byte) 233;
      numArray3[27] = (byte) 53;
      numArray3[19] = (byte) 82;
      numArray3[1] = (byte) 217;
      numArray3[6] = (byte) 51;
      numArray3[33] = (byte) 251;
      numArray3[5] = (byte) 117;
      numArray3[37] = (byte) 180;
      numArray3[36] = (byte) 71;
      numArray3[43] = (byte) 20;
      numArray3[20] = (byte) 105;
      numArray3[39] = (byte) 33;
      numArray3[30] = (byte) 10;
      numArray3[41] = (byte) 154;
      numArray3[42] = (byte) 120;
      numArray3[4] = (byte) 180;
      numArray3[32 /*0x20*/] = (byte) 163;
      numArray3[45] = (byte) 78;
      numArray3[9] = (byte) 161;
      numArray3[0] = (byte) 192 /*0xC0*/;
      numArray3[35] = (byte) 214;
      numArray3[24] = (byte) 134;
      numArray3[50] = (byte) 143;
      numArray3[12] = (byte) 70;
      numArray3[38] = (byte) 178;
      numArray3[53] = (byte) 254;
      numArray3[54] = (byte) 150;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[1]{ (byte) 22 };
      byte[] numArray5 = new byte[1]{ (byte) 23 };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[56];
    byte[] numArray7 = new byte[55]
    {
      (byte) 216,
      (byte) 117,
      (byte) 75,
      (byte) 23,
      (byte) 85,
      (byte) 163,
      (byte) 206,
      (byte) 95,
      (byte) 162,
      (byte) 211,
      (byte) 155,
      (byte) 40,
      (byte) 61,
      (byte) 244,
      (byte) 46,
      (byte) 146,
      (byte) 105,
      (byte) 216,
      (byte) 32 /*0x20*/,
      (byte) 243,
      (byte) 99,
      (byte) 69,
      (byte) 151,
      (byte) 45,
      (byte) 52,
      (byte) 61,
      (byte) 79,
      (byte) 127 /*0x7F*/,
      (byte) 178,
      (byte) 77,
      (byte) 158,
      (byte) 75,
      (byte) 96 /*0x60*/,
      (byte) 222,
      (byte) 198,
      (byte) 100,
      (byte) 24,
      (byte) 240 /*0xF0*/,
      (byte) 200,
      (byte) 54,
      (byte) 7,
      (byte) 178,
      (byte) 43,
      (byte) 136,
      (byte) 31 /*0x1F*/,
      (byte) 207,
      (byte) 253,
      (byte) 25,
      (byte) 44,
      (byte) 19,
      (byte) 149,
      (byte) 62,
      (byte) 246,
      (byte) 156,
      (byte) 220
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 244,
      (byte) 146,
      (byte) 35,
      (byte) 206,
      (byte) 126,
      (byte) 207,
      (byte) 26,
      (byte) 48 /*0x30*/,
      (byte) 56,
      (byte) 150,
      (byte) 140,
      (byte) 170,
      (byte) 4,
      (byte) 217,
      (byte) 91,
      (byte) 9,
      (byte) 86,
      (byte) 108,
      (byte) 47,
      (byte) 184,
      (byte) 6,
      (byte) 125,
      (byte) 11,
      (byte) 92,
      (byte) 135,
      (byte) 105,
      (byte) 165,
      (byte) 165,
      (byte) 35,
      (byte) 117,
      (byte) 70,
      (byte) 53,
      (byte) 188,
      (byte) 74,
      (byte) 21,
      (byte) 136,
      (byte) 91,
      (byte) 233,
      (byte) 201,
      (byte) 149,
      (byte) 250,
      (byte) 127 /*0x7F*/,
      (byte) 76,
      (byte) 26,
      (byte) 147,
      (byte) 140,
      (byte) 196,
      (byte) 97,
      (byte) 76,
      (byte) 118,
      (byte) 91,
      (byte) 238,
      (byte) 186,
      (byte) 246,
      (byte) 227
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[1]{ (byte) 30 };
    byte[] numArray10 = new byte[1]{ (byte) 196 };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 1);
    for (int index = 0; index < 1; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13253()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 85,
        (byte) 52,
        (byte) 79,
        (byte) 199,
        (byte) 94,
        (byte) 45,
        (byte) 202,
        (byte) 122,
        (byte) 120,
        (byte) 16 /*0x10*/,
        (byte) 234,
        (byte) 235
      };
      byte[] numArray3 = new byte[12];
      numArray3[7] = (byte) 250;
      numArray3[1] = (byte) 4;
      numArray3[2] = (byte) 143;
      numArray3[3] = (byte) 3;
      numArray3[9] = (byte) 132;
      numArray3[8] = (byte) 90;
      numArray3[6] = (byte) 207;
      numArray3[11] = (byte) 89;
      numArray3[5] = (byte) 194;
      numArray3[0] = (byte) 174;
      numArray3[10] = (byte) 17;
      numArray3[4] = (byte) 117;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 133,
      (byte) 249,
      (byte) 13,
      (byte) 176 /*0xB0*/,
      (byte) 226,
      (byte) 184,
      (byte) 5,
      (byte) 214,
      (byte) 84,
      (byte) 208 /*0xD0*/,
      (byte) 7,
      (byte) 159
    };
    byte[] numArray6 = new byte[12];
    numArray6[7] = (byte) 27;
    numArray6[1] = (byte) 33;
    numArray6[2] = (byte) 161;
    numArray6[11] = (byte) 208 /*0xD0*/;
    numArray6[3] = (byte) 192 /*0xC0*/;
    numArray6[4] = (byte) 30;
    numArray6[6] = (byte) 106;
    numArray6[5] = (byte) 214;
    numArray6[8] = (byte) 108;
    numArray6[10] = (byte) 129;
    numArray6[9] = (byte) 214;
    numArray6[0] = (byte) 246;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13254(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 232;
    sourceArray1[15] = (byte) 93;
    sourceArray1[9] = (byte) 7;
    sourceArray1[3] = (byte) 91;
    sourceArray1[4] = (byte) 250;
    sourceArray1[7] = (byte) 106;
    sourceArray1[6] = (byte) 129;
    sourceArray1[24] = (byte) 215;
    sourceArray1[8] = (byte) 95;
    sourceArray1[42] = (byte) 206;
    sourceArray1[25] = (byte) 185;
    sourceArray1[0] = (byte) 211;
    sourceArray1[30] = (byte) 195;
    sourceArray1[13] = (byte) 185;
    sourceArray1[10] = (byte) 51;
    sourceArray1[22] = (byte) 73;
    sourceArray1[29] = (byte) 216;
    sourceArray1[17] = (byte) 73;
    sourceArray1[11] = (byte) 171;
    sourceArray1[19] = (byte) 5;
    sourceArray1[2] = (byte) 3;
    sourceArray1[21] = (byte) 50;
    sourceArray1[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    sourceArray1[1] = (byte) 250;
    sourceArray1[23] = (byte) 18;
    sourceArray1[12] = (byte) 67;
    sourceArray1[34] = (byte) 129;
    sourceArray1[28] = (byte) 143;
    sourceArray1[18] = (byte) 241;
    sourceArray1[20] = (byte) 155;
    sourceArray1[33] = (byte) 57;
    sourceArray1[26] = (byte) 181;
    sourceArray1[32 /*0x20*/] = (byte) 246;
    sourceArray1[36] = (byte) 3;
    sourceArray1[47] = (byte) 21;
    sourceArray1[35] = (byte) 213;
    sourceArray1[38] = (byte) 241;
    sourceArray1[37] = (byte) 67;
    sourceArray1[40] = (byte) 23;
    sourceArray1[39] = (byte) 29;
    sourceArray1[31 /*0x1F*/] = (byte) 187;
    sourceArray1[41] = (byte) 178;
    sourceArray1[45] = (byte) 126;
    sourceArray1[43] = (byte) 4;
    sourceArray1[44] = (byte) 125;
    sourceArray1[5] = (byte) 68;
    sourceArray1[14] = (byte) 61;
    sourceArray1[46] = (byte) 54;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 199,
      byte.MaxValue,
      (byte) 15,
      (byte) 187,
      (byte) 9,
      (byte) 48 /*0x30*/,
      (byte) 225,
      (byte) 170,
      (byte) 145,
      (byte) 78,
      (byte) 84,
      (byte) 74,
      (byte) 10,
      (byte) 24,
      (byte) 125,
      (byte) 26,
      (byte) 42,
      (byte) 11,
      (byte) 57,
      (byte) 43,
      (byte) 111,
      (byte) 195,
      (byte) 134,
      (byte) 0,
      (byte) 184,
      (byte) 96 /*0x60*/,
      (byte) 8,
      (byte) 253,
      (byte) 152,
      (byte) 214,
      (byte) 58,
      (byte) 183,
      (byte) 194,
      (byte) 227,
      (byte) 239,
      (byte) 60,
      (byte) 141,
      (byte) 106,
      (byte) 142,
      (byte) 4,
      (byte) 115,
      (byte) 73,
      (byte) 103,
      (byte) 85,
      byte.MaxValue,
      (byte) 236,
      (byte) 9,
      (byte) 89
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13255()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[40];
      byte[] numArray2 = new byte[40];
      numArray2[36] = (byte) 194;
      numArray2[6] = (byte) 125;
      numArray2[2] = (byte) 221;
      numArray2[3] = (byte) 108;
      numArray2[9] = (byte) 24;
      numArray2[37] = (byte) 168;
      numArray2[39] = (byte) 196;
      numArray2[16 /*0x10*/] = (byte) 131;
      numArray2[8] = (byte) 27;
      numArray2[33] = (byte) 142;
      numArray2[7] = (byte) 54;
      numArray2[26] = (byte) 157;
      numArray2[12] = (byte) 91;
      numArray2[1] = (byte) 44;
      numArray2[24] = (byte) 67;
      numArray2[15] = (byte) 238;
      numArray2[32 /*0x20*/] = (byte) 131;
      numArray2[17] = (byte) 108;
      numArray2[38] = (byte) 168;
      numArray2[20] = (byte) 198;
      numArray2[27] = (byte) 166;
      numArray2[13] = (byte) 43;
      numArray2[19] = (byte) 85;
      numArray2[34] = (byte) 143;
      numArray2[35] = (byte) 86;
      numArray2[5] = (byte) 219;
      numArray2[23] = (byte) 88;
      numArray2[22] = (byte) 210;
      numArray2[28] = (byte) 69;
      numArray2[29] = (byte) 109;
      numArray2[4] = (byte) 197;
      numArray2[31 /*0x1F*/] = (byte) 29;
      numArray2[21] = (byte) 199;
      numArray2[10] = (byte) 60;
      numArray2[0] = (byte) 201;
      numArray2[11] = (byte) 108;
      numArray2[30] = (byte) 18;
      numArray2[14] = (byte) 189;
      numArray2[18] = (byte) 231;
      numArray2[25] = (byte) 72;
      byte[] numArray3 = new byte[40]
      {
        (byte) 69,
        (byte) 42,
        (byte) 217,
        (byte) 93,
        (byte) 147,
        (byte) 239,
        (byte) 110,
        (byte) 64 /*0x40*/,
        (byte) 2,
        (byte) 59,
        (byte) 35,
        (byte) 252,
        (byte) 180,
        (byte) 236,
        (byte) 187,
        (byte) 132,
        (byte) 159,
        (byte) 138,
        (byte) 188,
        (byte) 160 /*0xA0*/,
        (byte) 130,
        (byte) 33,
        (byte) 68,
        (byte) 176 /*0xB0*/,
        (byte) 143,
        (byte) 81,
        (byte) 169,
        (byte) 249,
        (byte) 19,
        (byte) 249,
        (byte) 233,
        (byte) 24,
        (byte) 35,
        (byte) 184,
        (byte) 24,
        (byte) 82,
        (byte) 17,
        (byte) 199,
        (byte) 92,
        (byte) 35
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 40);
      for (int index = 0; index < 40; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[40];
    byte[] numArray5 = new byte[40];
    numArray5[33] = (byte) 36;
    numArray5[37] = (byte) 96 /*0x60*/;
    numArray5[22] = (byte) 33;
    numArray5[3] = (byte) 189;
    numArray5[4] = (byte) 24;
    numArray5[18] = (byte) 233;
    numArray5[27] = (byte) 12;
    numArray5[7] = (byte) 253;
    numArray5[8] = (byte) 61;
    numArray5[1] = (byte) 113;
    numArray5[30] = (byte) 157;
    numArray5[11] = (byte) 133;
    numArray5[5] = (byte) 78;
    numArray5[0] = (byte) 165;
    numArray5[21] = (byte) 115;
    numArray5[15] = (byte) 23;
    numArray5[25] = (byte) 135;
    numArray5[10] = (byte) 43;
    numArray5[20] = (byte) 141;
    numArray5[19] = (byte) 248;
    numArray5[32 /*0x20*/] = (byte) 53;
    numArray5[34] = (byte) 75;
    numArray5[38] = (byte) 208 /*0xD0*/;
    numArray5[14] = (byte) 36;
    numArray5[24] = (byte) 60;
    numArray5[6] = (byte) 141;
    numArray5[26] = (byte) 243;
    numArray5[29] = (byte) 209;
    numArray5[28] = (byte) 165;
    numArray5[16 /*0x10*/] = (byte) 133;
    numArray5[36] = (byte) 94;
    numArray5[31 /*0x1F*/] = (byte) 235;
    numArray5[13] = (byte) 136;
    numArray5[12] = (byte) 154;
    numArray5[2] = (byte) 168;
    numArray5[35] = (byte) 59;
    numArray5[23] = (byte) 165;
    numArray5[9] = (byte) 127 /*0x7F*/;
    numArray5[17] = (byte) 207;
    numArray5[39] = (byte) 126;
    byte[] numArray6 = new byte[40]
    {
      (byte) 240 /*0xF0*/,
      (byte) 83,
      (byte) 99,
      (byte) 40,
      (byte) 116,
      (byte) 227,
      (byte) 11,
      (byte) 247,
      (byte) 7,
      (byte) 186,
      (byte) 50,
      (byte) 237,
      (byte) 196,
      (byte) 16 /*0x10*/,
      (byte) 152,
      (byte) 27,
      (byte) 139,
      (byte) 157,
      (byte) 90,
      (byte) 240 /*0xF0*/,
      (byte) 215,
      (byte) 10,
      (byte) 212,
      (byte) 106,
      (byte) 131,
      (byte) 131,
      (byte) 59,
      (byte) 222,
      (byte) 224 /*0xE0*/,
      (byte) 112 /*0x70*/,
      (byte) 52,
      (byte) 128 /*0x80*/,
      (byte) 252,
      (byte) 122,
      (byte) 67,
      (byte) 201,
      (byte) 20,
      (byte) 78,
      (byte) 84,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 40);
    for (int index = 0; index < 40; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13256()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[16 /*0x10*/] = (byte) 76;
      numArray2[1] = (byte) 225;
      numArray2[7] = (byte) 91;
      numArray2[3] = (byte) 36;
      numArray2[9] = (byte) 63 /*0x3F*/;
      numArray2[5] = (byte) 25;
      numArray2[0] = (byte) 163;
      numArray2[10] = (byte) 34;
      numArray2[8] = (byte) 223;
      numArray2[2] = (byte) 67;
      numArray2[18] = (byte) 134;
      numArray2[14] = (byte) 120;
      numArray2[15] = (byte) 89;
      numArray2[13] = (byte) 130;
      numArray2[11] = (byte) 142;
      numArray2[4] = (byte) 0;
      numArray2[6] = (byte) 236;
      numArray2[17] = (byte) 210;
      numArray2[12] = (byte) 17;
      byte[] numArray3 = new byte[19]
      {
        (byte) 190,
        (byte) 164,
        (byte) 174,
        (byte) 8,
        (byte) 140,
        (byte) 190,
        (byte) 126,
        (byte) 49,
        (byte) 226,
        (byte) 8,
        (byte) 14,
        (byte) 199,
        (byte) 73,
        (byte) 227,
        (byte) 179,
        (byte) 131,
        (byte) 30,
        (byte) 167,
        (byte) 71
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
      (byte) 94,
      (byte) 69,
      (byte) 191,
      (byte) 71,
      (byte) 90,
      (byte) 19,
      (byte) 2,
      (byte) 78,
      (byte) 178,
      (byte) 82,
      (byte) 107,
      (byte) 168,
      (byte) 82,
      (byte) 37,
      (byte) 155,
      (byte) 36,
      (byte) 5,
      (byte) 252,
      (byte) 0
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 160 /*0xA0*/,
      (byte) 46,
      (byte) 183,
      (byte) 244,
      (byte) 93,
      (byte) 50,
      (byte) 187,
      (byte) 73,
      (byte) 34,
      (byte) 170,
      (byte) 108,
      (byte) 200,
      (byte) 162,
      (byte) 242,
      (byte) 86,
      (byte) 68,
      (byte) 177,
      (byte) 128 /*0x80*/,
      (byte) 137
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13257()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 237,
        (byte) 246,
        (byte) 73,
        (byte) 193,
        (byte) 186,
        (byte) 120,
        (byte) 93,
        (byte) 32 /*0x20*/,
        (byte) 93,
        (byte) 97,
        (byte) 236,
        (byte) 132
      };
      byte[] numArray3 = new byte[12];
      numArray3[5] = (byte) 6;
      numArray3[1] = (byte) 78;
      numArray3[7] = (byte) 132;
      numArray3[3] = (byte) 208 /*0xD0*/;
      numArray3[10] = (byte) 45;
      numArray3[2] = (byte) 184;
      numArray3[6] = (byte) 56;
      numArray3[8] = (byte) 204;
      numArray3[0] = (byte) 119;
      numArray3[4] = (byte) 210;
      numArray3[9] = (byte) 183;
      numArray3[11] = (byte) 188;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_13210.sspq, 311, (Array) numArray4, 0, 45);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 311, (Array) numArray4, 0, 45);
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
      (byte) 99,
      (byte) 238,
      (byte) 93,
      (byte) 215,
      (byte) 60,
      (byte) 140,
      (byte) 138,
      (byte) 84,
      (byte) 181,
      (byte) 231,
      (byte) 0,
      (byte) 96 /*0x60*/
    };
    byte[] numArray7 = new byte[12];
    numArray7[8] = (byte) 124;
    numArray7[1] = (byte) 89;
    numArray7[2] = (byte) 119;
    numArray7[3] = (byte) 132;
    numArray7[6] = (byte) 101;
    numArray7[5] = (byte) 219;
    numArray7[0] = (byte) 236;
    numArray7[7] = (byte) 183;
    numArray7[11] = (byte) 116;
    numArray7[9] = (byte) 93;
    numArray7[10] = (byte) 37;
    numArray7[4] = (byte) 10;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13258()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 93,
        (byte) 251,
        (byte) 106,
        (byte) 116,
        (byte) 120,
        (byte) 97,
        (byte) 197,
        (byte) 51,
        (byte) 16 /*0x10*/,
        (byte) 47,
        (byte) 54,
        (byte) 203,
        (byte) 13
      };
      byte[] numArray3 = new byte[13]
      {
        (byte) 189,
        (byte) 22,
        (byte) 199,
        (byte) 103,
        (byte) 109,
        (byte) 67,
        (byte) 221,
        (byte) 66,
        (byte) 8,
        (byte) 81,
        (byte) 47,
        (byte) 220,
        (byte) 211
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13];
    numArray5[0] = (byte) 239;
    numArray5[12] = (byte) 0;
    numArray5[4] = (byte) 180;
    numArray5[3] = (byte) 227;
    numArray5[5] = (byte) 159;
    numArray5[8] = (byte) 1;
    numArray5[6] = (byte) 96 /*0x60*/;
    numArray5[7] = (byte) 198;
    numArray5[1] = (byte) 137;
    numArray5[2] = (byte) 180;
    numArray5[10] = (byte) 166;
    numArray5[11] = (byte) 41;
    numArray5[9] = (byte) 225;
    byte[] numArray6 = new byte[13];
    numArray6[12] = (byte) 180;
    numArray6[11] = (byte) 175;
    numArray6[2] = (byte) 64 /*0x40*/;
    numArray6[5] = (byte) 108;
    numArray6[10] = (byte) 224 /*0xE0*/;
    numArray6[0] = (byte) 139;
    numArray6[6] = (byte) 106;
    numArray6[7] = (byte) 62;
    numArray6[8] = (byte) 63 /*0x3F*/;
    numArray6[9] = (byte) 134;
    numArray6[1] = (byte) 2;
    numArray6[3] = (byte) 211;
    numArray6[4] = (byte) 12;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13259()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 150,
        (byte) 187,
        (byte) 155,
        (byte) 50,
        (byte) 245,
        (byte) 125,
        (byte) 151,
        (byte) 81,
        (byte) 151,
        (byte) 65
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 245,
        (byte) 232,
        (byte) 229,
        (byte) 225,
        (byte) 126,
        (byte) 13,
        (byte) 145,
        (byte) 22,
        (byte) 230,
        (byte) 52
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 198;
    numArray5[3] = (byte) 16 /*0x10*/;
    numArray5[2] = (byte) 57;
    numArray5[4] = (byte) 212;
    numArray5[0] = (byte) 161;
    numArray5[7] = (byte) 18;
    numArray5[6] = (byte) 31 /*0x1F*/;
    numArray5[1] = (byte) 33;
    numArray5[9] = (byte) 36;
    numArray5[8] = (byte) 53;
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 199;
    numArray6[1] = (byte) 17;
    numArray6[2] = (byte) 147;
    numArray6[5] = (byte) 98;
    numArray6[3] = (byte) 19;
    numArray6[4] = (byte) 6;
    numArray6[6] = (byte) 199;
    numArray6[0] = (byte) 168;
    numArray6[7] = (byte) 235;
    numArray6[8] = (byte) 5;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[43];
    byte[] response = new byte[43];
    Array.Copy((Array) sc_13210.sspq, 356, (Array) numArray7, 0, 43);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13210.sspr, 356, (Array) numArray7, 0, 43);
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

  internal static int ssp_appserver_13260(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 209,
      (byte) 169,
      (byte) 183,
      (byte) 48 /*0x30*/,
      (byte) 149,
      (byte) 146,
      (byte) 55,
      (byte) 24,
      (byte) 63 /*0x3F*/,
      (byte) 164,
      (byte) 170,
      (byte) 124,
      (byte) 204,
      (byte) 175,
      (byte) 235,
      (byte) 43,
      (byte) 118,
      (byte) 233,
      (byte) 63 /*0x3F*/,
      (byte) 178,
      (byte) 19,
      (byte) 120,
      (byte) 82,
      (byte) 74,
      (byte) 237,
      (byte) 178,
      (byte) 185,
      (byte) 185,
      (byte) 193,
      (byte) 216,
      (byte) 143,
      (byte) 96 /*0x60*/,
      (byte) 9,
      (byte) 222,
      (byte) 206,
      (byte) 84,
      (byte) 241,
      (byte) 222,
      (byte) 223,
      (byte) 142,
      (byte) 173,
      (byte) 152,
      (byte) 231,
      (byte) 52,
      (byte) 114,
      (byte) 116,
      (byte) 136,
      (byte) 24
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 59,
      (byte) 129,
      (byte) 249,
      (byte) 29,
      (byte) 83,
      (byte) 207,
      (byte) 202,
      (byte) 194,
      (byte) 81,
      (byte) 165,
      (byte) 223,
      (byte) 44,
      (byte) 229,
      (byte) 141,
      (byte) 10,
      (byte) 168,
      (byte) 102,
      (byte) 234,
      (byte) 85,
      (byte) 254,
      (byte) 62,
      (byte) 90,
      (byte) 79,
      (byte) 36,
      (byte) 250,
      (byte) 205,
      (byte) 178,
      (byte) 108,
      (byte) 169,
      (byte) 196,
      (byte) 240 /*0xF0*/,
      (byte) 71,
      (byte) 98,
      (byte) 95,
      (byte) 212,
      (byte) 51,
      (byte) 208 /*0xD0*/,
      (byte) 245,
      (byte) 77,
      (byte) 30,
      (byte) 187,
      (byte) 109,
      (byte) 98,
      (byte) 115,
      (byte) 236,
      (byte) 69,
      (byte) 62,
      (byte) 194
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_13261(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 141,
      (byte) 55,
      (byte) 88,
      (byte) 86,
      (byte) 109,
      (byte) 67,
      (byte) 227,
      (byte) 217,
      (byte) 125,
      (byte) 87,
      (byte) 102,
      (byte) 21,
      (byte) 73,
      (byte) 115,
      (byte) 237,
      (byte) 225,
      (byte) 150,
      (byte) 57,
      (byte) 151,
      (byte) 10,
      (byte) 177,
      (byte) 5,
      (byte) 123,
      (byte) 144 /*0x90*/,
      (byte) 29,
      (byte) 216,
      (byte) 120,
      (byte) 42,
      (byte) 160 /*0xA0*/,
      (byte) 84,
      (byte) 160 /*0xA0*/,
      (byte) 252,
      (byte) 190,
      (byte) 176 /*0xB0*/,
      (byte) 123,
      (byte) 167,
      (byte) 117,
      (byte) 254,
      (byte) 132,
      (byte) 155,
      (byte) 252,
      (byte) 180,
      (byte) 47,
      (byte) 64 /*0x40*/,
      (byte) 129,
      (byte) 31 /*0x1F*/,
      (byte) 40,
      (byte) 1
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[17] = (byte) 42;
    sourceArray2[4] = (byte) 73;
    sourceArray2[47] = (byte) 254;
    sourceArray2[3] = (byte) 100;
    sourceArray2[37] = (byte) 113;
    sourceArray2[5] = (byte) 155;
    sourceArray2[8] = (byte) 252;
    sourceArray2[7] = (byte) 212;
    sourceArray2[46] = (byte) 96 /*0x60*/;
    sourceArray2[9] = (byte) 196;
    sourceArray2[27] = (byte) 201;
    sourceArray2[25] = (byte) 82;
    sourceArray2[12] = (byte) 220;
    sourceArray2[0] = (byte) 121;
    sourceArray2[6] = (byte) 241;
    sourceArray2[15] = (byte) 239;
    sourceArray2[16 /*0x10*/] = (byte) 130;
    sourceArray2[40] = (byte) 252;
    sourceArray2[2] = (byte) 62;
    sourceArray2[36] = (byte) 47;
    sourceArray2[28] = (byte) 99;
    sourceArray2[21] = (byte) 104;
    sourceArray2[22] = (byte) 250;
    sourceArray2[23] = (byte) 31 /*0x1F*/;
    sourceArray2[10] = (byte) 186;
    sourceArray2[35] = (byte) 106;
    sourceArray2[1] = (byte) 243;
    sourceArray2[18] = (byte) 69;
    sourceArray2[41] = (byte) 203;
    sourceArray2[29] = (byte) 135;
    sourceArray2[24] = (byte) 39;
    sourceArray2[31 /*0x1F*/] = (byte) 96 /*0x60*/;
    sourceArray2[32 /*0x20*/] = (byte) 0;
    sourceArray2[13] = (byte) 52;
    sourceArray2[38] = (byte) 200;
    sourceArray2[30] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 167;
    sourceArray2[39] = (byte) 229;
    sourceArray2[20] = (byte) 169;
    sourceArray2[19] = (byte) 126;
    sourceArray2[34] = (byte) 42;
    sourceArray2[11] = (byte) 35;
    sourceArray2[42] = (byte) 94;
    sourceArray2[43] = (byte) 233;
    sourceArray2[44] = (byte) 170;
    sourceArray2[45] = (byte) 92;
    sourceArray2[26] = (byte) 223;
    sourceArray2[33] = (byte) 59;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13262()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[239];
      byte[] numArray2 = new byte[55]
      {
        (byte) 201,
        (byte) 142,
        (byte) 9,
        (byte) 85,
        (byte) 204,
        (byte) 54,
        (byte) 49,
        (byte) 71,
        (byte) 184,
        (byte) 2,
        (byte) 178,
        (byte) 129,
        (byte) 184,
        (byte) 58,
        (byte) 229,
        (byte) 82,
        (byte) 42,
        (byte) 208 /*0xD0*/,
        (byte) 162,
        (byte) 252,
        (byte) 179,
        (byte) 93,
        (byte) 232,
        (byte) 5,
        (byte) 90,
        (byte) 190,
        (byte) 210,
        (byte) 198,
        (byte) 43,
        (byte) 202,
        (byte) 128 /*0x80*/,
        (byte) 207,
        (byte) 176 /*0xB0*/,
        (byte) 249,
        (byte) 53,
        (byte) 33,
        (byte) 109,
        (byte) 57,
        (byte) 156,
        (byte) 80 /*0x50*/,
        (byte) 137,
        (byte) 111,
        (byte) 63 /*0x3F*/,
        (byte) 175,
        (byte) 63 /*0x3F*/,
        (byte) 106,
        (byte) 244,
        (byte) 139,
        (byte) 9,
        (byte) 126,
        (byte) 24,
        (byte) 171,
        (byte) 99,
        (byte) 142,
        (byte) 99
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 143,
        (byte) 69,
        (byte) 143,
        (byte) 8,
        (byte) 119,
        (byte) 186,
        (byte) 40,
        (byte) 204,
        (byte) 68,
        (byte) 65,
        (byte) 253,
        (byte) 170,
        (byte) 154,
        (byte) 243,
        (byte) 34,
        (byte) 42,
        (byte) 176 /*0xB0*/,
        (byte) 43,
        (byte) 26,
        (byte) 137,
        (byte) 66,
        (byte) 129,
        (byte) 230,
        (byte) 116,
        (byte) 22,
        (byte) 25,
        (byte) 199,
        (byte) 200,
        (byte) 136,
        (byte) 134,
        (byte) 179,
        (byte) 172,
        (byte) 240 /*0xF0*/,
        (byte) 138,
        (byte) 234,
        (byte) 194,
        (byte) 1,
        (byte) 210,
        (byte) 130,
        (byte) 172,
        (byte) 78,
        (byte) 18,
        (byte) 154,
        (byte) 160 /*0xA0*/,
        (byte) 149,
        (byte) 172,
        (byte) 201,
        (byte) 107,
        (byte) 50,
        (byte) 124,
        (byte) 102,
        (byte) 8,
        (byte) 191,
        (byte) 208 /*0xD0*/,
        (byte) 153
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 4,
        (byte) 251,
        (byte) 13,
        (byte) 99,
        (byte) 106,
        (byte) 27,
        (byte) 237,
        (byte) 129,
        (byte) 227,
        (byte) 209,
        (byte) 191,
        (byte) 5,
        (byte) 196,
        (byte) 229,
        (byte) 241,
        (byte) 51,
        (byte) 82,
        (byte) 115,
        (byte) 67,
        (byte) 29,
        (byte) 56,
        (byte) 197,
        (byte) 240 /*0xF0*/,
        (byte) 201,
        (byte) 71,
        (byte) 188,
        (byte) 173,
        (byte) 178,
        (byte) 213,
        (byte) 107,
        (byte) 33,
        (byte) 150,
        (byte) 202,
        (byte) 122,
        (byte) 105,
        (byte) 62,
        (byte) 142,
        (byte) 236,
        (byte) 30,
        (byte) 83,
        (byte) 82,
        (byte) 45,
        (byte) 92,
        (byte) 152,
        (byte) 53,
        (byte) 133,
        (byte) 69,
        (byte) 152,
        (byte) 39,
        (byte) 119,
        (byte) 16 /*0x10*/,
        (byte) 27,
        (byte) 31 /*0x1F*/,
        (byte) 77,
        (byte) 109
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 131,
        (byte) 205,
        (byte) 251,
        (byte) 64 /*0x40*/,
        (byte) 235,
        (byte) 112 /*0x70*/,
        (byte) 95,
        (byte) 37,
        (byte) 205,
        (byte) 15,
        (byte) 78,
        (byte) 158,
        (byte) 212,
        (byte) 8,
        (byte) 180,
        (byte) 145,
        (byte) 64 /*0x40*/,
        (byte) 188,
        (byte) 196,
        (byte) 165,
        (byte) 43,
        (byte) 63 /*0x3F*/,
        (byte) 198,
        (byte) 98,
        (byte) 201,
        (byte) 166,
        (byte) 48 /*0x30*/,
        (byte) 111,
        (byte) 95,
        (byte) 220,
        (byte) 73,
        (byte) 42,
        (byte) 204,
        (byte) 20,
        (byte) 22,
        (byte) 20,
        (byte) 3,
        (byte) 9,
        (byte) 189,
        (byte) 183,
        (byte) 150,
        (byte) 185,
        (byte) 242,
        (byte) 181,
        (byte) 13,
        (byte) 52,
        (byte) 166,
        (byte) 217,
        (byte) 187,
        (byte) 232,
        (byte) 77,
        (byte) 72,
        (byte) 153,
        (byte) 59,
        (byte) 244
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 67,
        (byte) 28,
        (byte) 173,
        (byte) 110,
        (byte) 59,
        (byte) 172,
        (byte) 119,
        (byte) 175,
        (byte) 40,
        (byte) 58,
        (byte) 73,
        (byte) 124,
        (byte) 178,
        (byte) 164,
        (byte) 102,
        (byte) 84,
        (byte) 239,
        (byte) 203,
        (byte) 11,
        (byte) 104,
        (byte) 87,
        (byte) 147,
        (byte) 193,
        (byte) 119,
        (byte) 20,
        (byte) 138,
        (byte) 93,
        (byte) 90,
        (byte) 6,
        (byte) 118,
        (byte) 124,
        (byte) 12,
        (byte) 41,
        (byte) 51,
        (byte) 74,
        (byte) 208 /*0xD0*/,
        (byte) 112 /*0x70*/,
        (byte) 229,
        (byte) 22,
        (byte) 129,
        (byte) 229,
        (byte) 232,
        (byte) 13,
        (byte) 74,
        (byte) 251,
        (byte) 28,
        (byte) 116,
        (byte) 211,
        (byte) 170,
        (byte) 198,
        (byte) 48 /*0x30*/,
        (byte) 129,
        (byte) 98,
        (byte) 214,
        (byte) 179
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 33,
        (byte) 163,
        (byte) 109,
        (byte) 7,
        (byte) 142,
        (byte) 62,
        (byte) 127 /*0x7F*/,
        (byte) 69,
        (byte) 161,
        (byte) 149,
        (byte) 122,
        (byte) 55,
        (byte) 39,
        (byte) 168,
        (byte) 169,
        (byte) 6,
        (byte) 126,
        (byte) 82,
        (byte) 76,
        (byte) 123,
        (byte) 180,
        (byte) 216,
        (byte) 84,
        (byte) 97,
        (byte) 50,
        (byte) 180,
        (byte) 19,
        (byte) 16 /*0x10*/,
        (byte) 118,
        (byte) 190,
        (byte) 238,
        (byte) 87,
        (byte) 236,
        (byte) 217,
        (byte) 88,
        (byte) 239,
        (byte) 245,
        (byte) 225,
        (byte) 104,
        (byte) 244,
        (byte) 83,
        (byte) 44,
        (byte) 208 /*0xD0*/,
        (byte) 97,
        (byte) 72,
        (byte) 127 /*0x7F*/,
        (byte) 85,
        (byte) 22,
        (byte) 236,
        (byte) 86,
        (byte) 108,
        (byte) 143,
        (byte) 38,
        (byte) 142,
        (byte) 244
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 54,
        (byte) 89,
        (byte) 177,
        (byte) 79,
        (byte) 28,
        (byte) 169,
        (byte) 177,
        (byte) 190,
        (byte) 60,
        (byte) 82,
        (byte) 127 /*0x7F*/,
        (byte) 89,
        (byte) 149,
        (byte) 228,
        (byte) 9,
        (byte) 181,
        (byte) 137,
        (byte) 131,
        (byte) 110,
        (byte) 134,
        (byte) 118,
        (byte) 123,
        (byte) 248,
        (byte) 21,
        (byte) 138,
        (byte) 176 /*0xB0*/,
        (byte) 64 /*0x40*/,
        (byte) 70,
        (byte) 250,
        (byte) 170,
        (byte) 83,
        (byte) 127 /*0x7F*/,
        (byte) 154,
        (byte) 118,
        (byte) 57,
        (byte) 140,
        (byte) 101,
        (byte) 50,
        (byte) 200,
        (byte) 63 /*0x3F*/,
        (byte) 91,
        (byte) 140,
        (byte) 190,
        (byte) 202,
        (byte) 245,
        (byte) 121,
        (byte) 21,
        (byte) 226,
        (byte) 38,
        (byte) 204,
        (byte) 216,
        (byte) 82,
        (byte) 68,
        (byte) 41,
        (byte) 227
      };
      byte[] numArray9 = new byte[55];
      numArray9[14] = (byte) 170;
      numArray9[46] = (byte) 33;
      numArray9[33] = (byte) 165;
      numArray9[35] = (byte) 142;
      numArray9[20] = (byte) 239;
      numArray9[3] = (byte) 89;
      numArray9[4] = (byte) 170;
      numArray9[52] = (byte) 210;
      numArray9[8] = (byte) 31 /*0x1F*/;
      numArray9[9] = (byte) 93;
      numArray9[30] = (byte) 223;
      numArray9[11] = (byte) 173;
      numArray9[12] = (byte) 74;
      numArray9[13] = (byte) 90;
      numArray9[10] = (byte) 46;
      numArray9[41] = (byte) 93;
      numArray9[16 /*0x10*/] = (byte) 66;
      numArray9[6] = (byte) 103;
      numArray9[5] = (byte) 130;
      numArray9[19] = (byte) 176 /*0xB0*/;
      numArray9[18] = (byte) 99;
      numArray9[21] = (byte) 162;
      numArray9[39] = (byte) 158;
      numArray9[23] = (byte) 42;
      numArray9[53] = (byte) 192 /*0xC0*/;
      numArray9[24] = (byte) 253;
      numArray9[26] = (byte) 27;
      numArray9[27] = (byte) 154;
      numArray9[28] = (byte) 232;
      numArray9[0] = (byte) 89;
      numArray9[47] = (byte) 79;
      numArray9[31 /*0x1F*/] = (byte) 131;
      numArray9[1] = (byte) 136;
      numArray9[54] = (byte) 138;
      numArray9[7] = (byte) 121;
      numArray9[36] = (byte) 200;
      numArray9[42] = (byte) 74;
      numArray9[37] = (byte) 94;
      numArray9[2] = (byte) 102;
      numArray9[32 /*0x20*/] = (byte) 168;
      numArray9[40] = (byte) 188;
      numArray9[45] = (byte) 214;
      numArray9[17] = (byte) 223;
      numArray9[43] = (byte) 107;
      numArray9[51] = (byte) 17;
      numArray9[25] = (byte) 183;
      numArray9[44] = (byte) 238;
      numArray9[49] = (byte) 93;
      numArray9[48 /*0x30*/] = (byte) 62;
      numArray9[15] = (byte) 1;
      numArray9[50] = (byte) 33;
      numArray9[34] = (byte) 243;
      numArray9[29] = (byte) 87;
      numArray9[38] = (byte) 109;
      numArray9[22] = (byte) 11;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[19];
      numArray10[11] = (byte) 44;
      numArray10[1] = (byte) 180;
      numArray10[2] = (byte) 209;
      numArray10[7] = (byte) 254;
      numArray10[17] = (byte) 59;
      numArray10[6] = (byte) 192 /*0xC0*/;
      numArray10[5] = (byte) 149;
      numArray10[4] = (byte) 74;
      numArray10[8] = (byte) 196;
      numArray10[16 /*0x10*/] = (byte) 88;
      numArray10[18] = (byte) 33;
      numArray10[0] = (byte) 122;
      numArray10[12] = (byte) 80 /*0x50*/;
      numArray10[13] = (byte) 133;
      numArray10[9] = (byte) 77;
      numArray10[15] = (byte) 95;
      numArray10[14] = (byte) 234;
      numArray10[10] = (byte) 141;
      numArray10[3] = (byte) 11;
      byte[] numArray11 = new byte[19]
      {
        (byte) 105,
        (byte) 16 /*0x10*/,
        (byte) 57,
        (byte) 89,
        (byte) 32 /*0x20*/,
        (byte) 47,
        (byte) 155,
        (byte) 118,
        (byte) 113,
        (byte) 49,
        (byte) 79,
        (byte) 219,
        (byte) 202,
        (byte) 175,
        (byte) 35,
        (byte) 52,
        (byte) 117,
        (byte) 227,
        (byte) 17
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[239];
    byte[] numArray13 = new byte[55]
    {
      (byte) 205,
      (byte) 48 /*0x30*/,
      (byte) 109,
      (byte) 104,
      (byte) 11,
      (byte) 88,
      (byte) 247,
      (byte) 150,
      (byte) 55,
      (byte) 189,
      (byte) 194,
      (byte) 216,
      (byte) 110,
      (byte) 207,
      (byte) 79,
      (byte) 94,
      (byte) 44,
      (byte) 228,
      (byte) 211,
      (byte) 228,
      (byte) 110,
      (byte) 148,
      (byte) 159,
      (byte) 217,
      (byte) 244,
      (byte) 172,
      (byte) 87,
      (byte) 54,
      (byte) 49,
      (byte) 213,
      (byte) 140,
      (byte) 150,
      (byte) 238,
      (byte) 35,
      (byte) 149,
      (byte) 250,
      (byte) 233,
      (byte) 60,
      (byte) 27,
      (byte) 30,
      (byte) 186,
      (byte) 187,
      (byte) 49,
      (byte) 103,
      (byte) 177,
      (byte) 192 /*0xC0*/,
      (byte) 17,
      (byte) 41,
      (byte) 0,
      (byte) 90,
      (byte) 93,
      (byte) 231,
      (byte) 209,
      (byte) 78,
      (byte) 253
    };
    byte[] numArray14 = new byte[55];
    numArray14[49] = (byte) 46;
    numArray14[27] = (byte) 243;
    numArray14[32 /*0x20*/] = (byte) 117;
    numArray14[28] = (byte) 201;
    numArray14[48 /*0x30*/] = (byte) 209;
    numArray14[42] = (byte) 16 /*0x10*/;
    numArray14[6] = (byte) 46;
    numArray14[29] = (byte) 224 /*0xE0*/;
    numArray14[8] = (byte) 143;
    numArray14[35] = (byte) 242;
    numArray14[26] = (byte) 40;
    numArray14[38] = (byte) 230;
    numArray14[31 /*0x1F*/] = (byte) 158;
    numArray14[13] = (byte) 214;
    numArray14[10] = byte.MaxValue;
    numArray14[15] = (byte) 248;
    numArray14[16 /*0x10*/] = (byte) 29;
    numArray14[17] = (byte) 43;
    numArray14[9] = (byte) 100;
    numArray14[19] = (byte) 19;
    numArray14[20] = (byte) 125;
    numArray14[11] = (byte) 44;
    numArray14[22] = (byte) 145;
    numArray14[3] = (byte) 50;
    numArray14[24] = (byte) 65;
    numArray14[25] = (byte) 179;
    numArray14[34] = (byte) 159;
    numArray14[30] = (byte) 215;
    numArray14[37] = (byte) 253;
    numArray14[1] = (byte) 1;
    numArray14[39] = (byte) 15;
    numArray14[21] = (byte) 165;
    numArray14[4] = (byte) 24;
    numArray14[33] = (byte) 153;
    numArray14[0] = (byte) 229;
    numArray14[53] = (byte) 227;
    numArray14[36] = (byte) 44;
    numArray14[18] = (byte) 69;
    numArray14[47] = (byte) 85;
    numArray14[12] = (byte) 199;
    numArray14[23] = (byte) 235;
    numArray14[41] = (byte) 217;
    numArray14[7] = (byte) 134;
    numArray14[43] = (byte) 89;
    numArray14[44] = (byte) 141;
    numArray14[5] = (byte) 197;
    numArray14[46] = (byte) 99;
    numArray14[52] = (byte) 82;
    numArray14[14] = (byte) 167;
    numArray14[2] = (byte) 52;
    numArray14[45] = (byte) 109;
    numArray14[51] = (byte) 129;
    numArray14[40] = (byte) 254;
    numArray14[50] = (byte) 7;
    numArray14[54] = (byte) 154;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 108,
      (byte) 62,
      (byte) 196,
      (byte) 211,
      (byte) 138,
      (byte) 141,
      (byte) 233,
      (byte) 139,
      (byte) 103,
      (byte) 74,
      (byte) 37,
      (byte) 220,
      (byte) 234,
      (byte) 30,
      (byte) 38,
      (byte) 13,
      (byte) 151,
      (byte) 49,
      (byte) 93,
      (byte) 146,
      (byte) 0,
      (byte) 161,
      (byte) 94,
      (byte) 212,
      (byte) 62,
      (byte) 138,
      (byte) 241,
      (byte) 10,
      (byte) 135,
      (byte) 168,
      (byte) 123,
      (byte) 220,
      (byte) 136,
      (byte) 17,
      (byte) 233,
      (byte) 138,
      (byte) 247,
      (byte) 74,
      (byte) 110,
      (byte) 201,
      (byte) 231,
      (byte) 171,
      (byte) 30,
      (byte) 139,
      (byte) 160 /*0xA0*/,
      (byte) 19,
      (byte) 193,
      (byte) 199,
      (byte) 71,
      (byte) 16 /*0x10*/,
      (byte) 132,
      (byte) 243,
      (byte) 236,
      (byte) 66,
      (byte) 88
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 0,
      (byte) 82,
      (byte) 91,
      (byte) 81,
      (byte) 179,
      (byte) 194,
      (byte) 245,
      (byte) 69,
      (byte) 80 /*0x50*/,
      (byte) 108,
      (byte) 197,
      (byte) 164,
      (byte) 172,
      (byte) 218,
      (byte) 35,
      (byte) 71,
      (byte) 126,
      (byte) 186,
      (byte) 185,
      (byte) 209,
      (byte) 119,
      (byte) 136,
      (byte) 108,
      (byte) 172,
      (byte) 134,
      (byte) 65,
      (byte) 24,
      (byte) 202,
      (byte) 18,
      (byte) 83,
      (byte) 116,
      (byte) 40,
      (byte) 245,
      (byte) 174,
      (byte) 218,
      (byte) 1,
      (byte) 172,
      (byte) 63 /*0x3F*/,
      (byte) 36,
      (byte) 151,
      (byte) 153,
      (byte) 72,
      (byte) 161,
      (byte) 35,
      (byte) 137,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 144 /*0x90*/,
      (byte) 103,
      (byte) 55,
      (byte) 196,
      (byte) 216,
      (byte) 232,
      (byte) 0,
      (byte) 228
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 239,
      (byte) 148,
      (byte) 0,
      (byte) 156,
      (byte) 222,
      (byte) 3,
      (byte) 191,
      (byte) 207,
      (byte) 72,
      (byte) 84,
      (byte) 217,
      (byte) 53,
      (byte) 155,
      (byte) 21,
      (byte) 77,
      (byte) 194,
      (byte) 147,
      (byte) 171,
      (byte) 84,
      (byte) 131,
      (byte) 211,
      (byte) 52,
      (byte) 42,
      (byte) 201,
      (byte) 41,
      (byte) 195,
      (byte) 118,
      (byte) 36,
      (byte) 136,
      (byte) 240 /*0xF0*/,
      (byte) 69,
      (byte) 185,
      (byte) 31 /*0x1F*/,
      (byte) 170,
      (byte) 8,
      (byte) 181,
      (byte) 110,
      (byte) 45,
      (byte) 8,
      (byte) 168,
      (byte) 95,
      (byte) 187,
      (byte) 88,
      (byte) 190,
      (byte) 231,
      (byte) 145,
      (byte) 172,
      (byte) 251,
      (byte) 54,
      (byte) 48 /*0x30*/,
      (byte) 88,
      (byte) 150,
      (byte) 3,
      (byte) 71,
      (byte) 12
    };
    byte[] numArray18 = new byte[55];
    numArray18[35] = (byte) 156;
    numArray18[12] = (byte) 190;
    numArray18[45] = (byte) 203;
    numArray18[0] = (byte) 204;
    numArray18[2] = (byte) 220;
    numArray18[20] = (byte) 61;
    numArray18[29] = (byte) 253;
    numArray18[7] = (byte) 76;
    numArray18[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    numArray18[24] = (byte) 99;
    numArray18[10] = (byte) 120;
    numArray18[50] = (byte) 42;
    numArray18[11] = (byte) 232;
    numArray18[48 /*0x30*/] = (byte) 200;
    numArray18[26] = (byte) 53;
    numArray18[15] = (byte) 79;
    numArray18[52] = (byte) 84;
    numArray18[17] = (byte) 134;
    numArray18[18] = (byte) 239;
    numArray18[19] = (byte) 65;
    numArray18[14] = (byte) 86;
    numArray18[21] = (byte) 234;
    numArray18[30] = (byte) 189;
    numArray18[4] = (byte) 127 /*0x7F*/;
    numArray18[13] = (byte) 133;
    numArray18[25] = (byte) 56;
    numArray18[51] = (byte) 235;
    numArray18[8] = (byte) 97;
    numArray18[28] = (byte) 158;
    numArray18[27] = (byte) 36;
    numArray18[39] = (byte) 52;
    numArray18[31 /*0x1F*/] = (byte) 239;
    numArray18[1] = (byte) 137;
    numArray18[33] = (byte) 154;
    numArray18[34] = (byte) 198;
    numArray18[3] = (byte) 182;
    numArray18[36] = (byte) 193;
    numArray18[9] = (byte) 103;
    numArray18[49] = (byte) 5;
    numArray18[41] = (byte) 127 /*0x7F*/;
    numArray18[40] = (byte) 89;
    numArray18[5] = (byte) 24;
    numArray18[23] = (byte) 210;
    numArray18[43] = (byte) 110;
    numArray18[44] = (byte) 57;
    numArray18[42] = (byte) 149;
    numArray18[6] = (byte) 25;
    numArray18[47] = (byte) 74;
    numArray18[38] = (byte) 172;
    numArray18[46] = (byte) 141;
    numArray18[22] = (byte) 36;
    numArray18[37] = (byte) 37;
    numArray18[16 /*0x10*/] = (byte) 14;
    numArray18[53] = (byte) 14;
    numArray18[54] = (byte) 6;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 84,
      (byte) 217,
      (byte) 4,
      (byte) 17,
      (byte) 173,
      (byte) 239,
      (byte) 193,
      (byte) 95,
      (byte) 106,
      (byte) 61,
      (byte) 82,
      (byte) 191,
      (byte) 75,
      (byte) 29,
      (byte) 171,
      (byte) 181,
      (byte) 144 /*0x90*/,
      (byte) 95,
      (byte) 163,
      (byte) 220,
      (byte) 188,
      (byte) 26,
      (byte) 28,
      (byte) 91,
      (byte) 141,
      (byte) 77,
      (byte) 124,
      (byte) 164,
      (byte) 188,
      (byte) 95,
      (byte) 181,
      (byte) 169,
      (byte) 110,
      (byte) 78,
      (byte) 226,
      (byte) 225,
      (byte) 233,
      (byte) 10,
      (byte) 22,
      (byte) 132,
      (byte) 61,
      (byte) 80 /*0x50*/,
      (byte) 93,
      (byte) 61,
      (byte) 104,
      (byte) 46,
      (byte) 83,
      (byte) 189,
      (byte) 71,
      (byte) 61,
      (byte) 197,
      (byte) 195,
      (byte) 81,
      (byte) 2,
      (byte) 65
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 17,
      (byte) 249,
      (byte) 72,
      (byte) 242,
      (byte) 205,
      (byte) 161,
      (byte) 227,
      (byte) 247,
      (byte) 110,
      (byte) 171,
      (byte) 172,
      (byte) 104,
      (byte) 115,
      (byte) 125,
      (byte) 22,
      (byte) 242,
      (byte) 28,
      (byte) 176 /*0xB0*/,
      (byte) 76,
      (byte) 219,
      (byte) 62,
      (byte) 182,
      (byte) 120,
      (byte) 174,
      (byte) 126,
      (byte) 13,
      (byte) 230,
      (byte) 141,
      (byte) 89,
      (byte) 119,
      (byte) 2,
      (byte) 130,
      (byte) 166,
      (byte) 71,
      (byte) 191,
      (byte) 69,
      (byte) 101,
      (byte) 50,
      (byte) 117,
      (byte) 8,
      (byte) 41,
      (byte) 245,
      (byte) 251,
      (byte) 185,
      (byte) 226,
      (byte) 227,
      (byte) 4,
      (byte) 112 /*0x70*/,
      (byte) 232,
      (byte) 12,
      (byte) 194,
      (byte) 250,
      (byte) 103,
      (byte) 105,
      (byte) 156
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[19]
    {
      (byte) 168,
      (byte) 132,
      (byte) 95,
      (byte) 115,
      (byte) 29,
      (byte) 163,
      (byte) 11,
      (byte) 136,
      (byte) 169,
      (byte) 81,
      (byte) 253,
      (byte) 59,
      (byte) 201,
      (byte) 203,
      (byte) 134,
      (byte) 72,
      (byte) 31 /*0x1F*/,
      (byte) 158,
      (byte) 107
    };
    byte[] numArray22 = new byte[19]
    {
      (byte) 214,
      (byte) 35,
      (byte) 166,
      (byte) 32 /*0x20*/,
      (byte) 120,
      (byte) 101,
      (byte) 244,
      (byte) 218,
      (byte) 134,
      (byte) 66,
      (byte) 55,
      (byte) 217,
      (byte) 161,
      (byte) 63 /*0x3F*/,
      (byte) 65,
      (byte) 190,
      (byte) 55,
      (byte) 224 /*0xE0*/,
      (byte) 73
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 19);
    for (int index = 0; index < 19; ++index)
      numArray12[index + 220] ^= numArray22[index];
    byte[] numArray23 = new byte[17];
    byte[] response = new byte[17];
    Array.Copy((Array) sc_13210.sspq, 399, (Array) numArray23, 0, 17);
    key.Query(true, 335, numArray23, response);
    Array.Copy((Array) sc_13210.sspr, 399, (Array) numArray23, 0, 17);
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

  internal static int ssp_appserver_13263(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 36;
    sourceArray1[30] = (byte) 126;
    sourceArray1[4] = (byte) 125;
    sourceArray1[3] = (byte) 130;
    sourceArray1[6] = (byte) 63 /*0x3F*/;
    sourceArray1[8] = (byte) 50;
    sourceArray1[15] = (byte) 101;
    sourceArray1[7] = (byte) 122;
    sourceArray1[21] = (byte) 81;
    sourceArray1[2] = (byte) 85;
    sourceArray1[0] = (byte) 191;
    sourceArray1[11] = (byte) 125;
    sourceArray1[1] = (byte) 221;
    sourceArray1[28] = (byte) 146;
    sourceArray1[14] = (byte) 37;
    sourceArray1[16 /*0x10*/] = (byte) 180;
    sourceArray1[26] = (byte) 138;
    sourceArray1[17] = (byte) 192 /*0xC0*/;
    sourceArray1[25] = (byte) 165;
    sourceArray1[46] = (byte) 69;
    sourceArray1[20] = (byte) 151;
    sourceArray1[24] = (byte) 147;
    sourceArray1[22] = (byte) 229;
    sourceArray1[23] = (byte) 241;
    sourceArray1[31 /*0x1F*/] = (byte) 189;
    sourceArray1[33] = (byte) 42;
    sourceArray1[19] = (byte) 113;
    sourceArray1[10] = (byte) 214;
    sourceArray1[29] = (byte) 13;
    sourceArray1[39] = (byte) 12;
    sourceArray1[13] = (byte) 181;
    sourceArray1[34] = (byte) 51;
    sourceArray1[32 /*0x20*/] = (byte) 107;
    sourceArray1[9] = (byte) 99;
    sourceArray1[27] = byte.MaxValue;
    sourceArray1[35] = (byte) 128 /*0x80*/;
    sourceArray1[36] = (byte) 164;
    sourceArray1[5] = (byte) 114;
    sourceArray1[38] = (byte) 223;
    sourceArray1[43] = (byte) 8;
    sourceArray1[40] = (byte) 89;
    sourceArray1[41] = (byte) 118;
    sourceArray1[42] = (byte) 38;
    sourceArray1[47] = (byte) 139;
    sourceArray1[44] = (byte) 54;
    sourceArray1[37] = (byte) 60;
    sourceArray1[12] = (byte) 222;
    sourceArray1[45] = (byte) 42;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 7;
    sourceArray2[1] = (byte) 123;
    sourceArray2[23] = (byte) 45;
    sourceArray2[25] = (byte) 113;
    sourceArray2[30] = (byte) 145;
    sourceArray2[5] = (byte) 150;
    sourceArray2[17] = (byte) 252;
    sourceArray2[11] = (byte) 114;
    sourceArray2[4] = (byte) 240 /*0xF0*/;
    sourceArray2[9] = (byte) 1;
    sourceArray2[36] = (byte) 74;
    sourceArray2[10] = (byte) 105;
    sourceArray2[40] = (byte) 200;
    sourceArray2[16 /*0x10*/] = (byte) 51;
    sourceArray2[14] = (byte) 1;
    sourceArray2[39] = (byte) 116;
    sourceArray2[2] = (byte) 225;
    sourceArray2[15] = (byte) 57;
    sourceArray2[18] = (byte) 173;
    sourceArray2[38] = (byte) 27;
    sourceArray2[12] = (byte) 6;
    sourceArray2[7] = (byte) 192 /*0xC0*/;
    sourceArray2[22] = (byte) 107;
    sourceArray2[21] = (byte) 253;
    sourceArray2[24] = (byte) 160 /*0xA0*/;
    sourceArray2[6] = (byte) 2;
    sourceArray2[3] = (byte) 147;
    sourceArray2[27] = (byte) 103;
    sourceArray2[35] = (byte) 20;
    sourceArray2[29] = (byte) 20;
    sourceArray2[20] = (byte) 14;
    sourceArray2[31 /*0x1F*/] = (byte) 213;
    sourceArray2[32 /*0x20*/] = (byte) 179;
    sourceArray2[28] = (byte) 30;
    sourceArray2[13] = (byte) 1;
    sourceArray2[34] = (byte) 2;
    sourceArray2[8] = byte.MaxValue;
    sourceArray2[37] = (byte) 78;
    sourceArray2[0] = (byte) 6;
    sourceArray2[19] = (byte) 164;
    sourceArray2[26] = (byte) 231;
    sourceArray2[41] = (byte) 114;
    sourceArray2[47] = (byte) 134;
    sourceArray2[43] = (byte) 178;
    sourceArray2[44] = (byte) 10;
    sourceArray2[45] = (byte) 158;
    sourceArray2[46] = (byte) 89;
    sourceArray2[33] = (byte) 245;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13264()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37];
      numArray2[14] = (byte) 124;
      numArray2[6] = (byte) 55;
      numArray2[2] = (byte) 120;
      numArray2[3] = (byte) 230;
      numArray2[7] = (byte) 145;
      numArray2[30] = (byte) 55;
      numArray2[29] = (byte) 152;
      numArray2[10] = (byte) 157;
      numArray2[34] = (byte) 72;
      numArray2[9] = (byte) 78;
      numArray2[18] = (byte) 97;
      numArray2[8] = (byte) 234;
      numArray2[21] = (byte) 169;
      numArray2[13] = (byte) 166;
      numArray2[5] = (byte) 90;
      numArray2[22] = (byte) 243;
      numArray2[4] = (byte) 143;
      numArray2[15] = (byte) 201;
      numArray2[0] = (byte) 144 /*0x90*/;
      numArray2[19] = (byte) 10;
      numArray2[23] = (byte) 189;
      numArray2[16 /*0x10*/] = (byte) 107;
      numArray2[1] = (byte) 60;
      numArray2[11] = (byte) 17;
      numArray2[26] = (byte) 41;
      numArray2[25] = (byte) 165;
      numArray2[17] = (byte) 24;
      numArray2[12] = (byte) 185;
      numArray2[28] = (byte) 196;
      numArray2[24] = (byte) 247;
      numArray2[27] = (byte) 34;
      numArray2[31 /*0x1F*/] = (byte) 177;
      numArray2[32 /*0x20*/] = (byte) 232;
      numArray2[33] = (byte) 98;
      numArray2[35] = (byte) 183;
      numArray2[20] = (byte) 52;
      numArray2[36] = (byte) 133;
      byte[] numArray3 = new byte[37];
      numArray3[13] = (byte) 117;
      numArray3[29] = (byte) 103;
      numArray3[10] = (byte) 251;
      numArray3[3] = (byte) 128 /*0x80*/;
      numArray3[5] = (byte) 205;
      numArray3[33] = (byte) 251;
      numArray3[25] = (byte) 19;
      numArray3[7] = (byte) 122;
      numArray3[8] = (byte) 96 /*0x60*/;
      numArray3[12] = (byte) 77;
      numArray3[14] = (byte) 155;
      numArray3[20] = (byte) 129;
      numArray3[16 /*0x10*/] = (byte) 37;
      numArray3[4] = (byte) 153;
      numArray3[24] = (byte) 39;
      numArray3[15] = (byte) 39;
      numArray3[28] = (byte) 30;
      numArray3[27] = (byte) 147;
      numArray3[18] = (byte) 37;
      numArray3[19] = (byte) 1;
      numArray3[35] = (byte) 65;
      numArray3[21] = (byte) 7;
      numArray3[22] = (byte) 59;
      numArray3[23] = (byte) 152;
      numArray3[11] = (byte) 200;
      numArray3[6] = (byte) 12;
      numArray3[0] = (byte) 103;
      numArray3[2] = (byte) 120;
      numArray3[17] = (byte) 45;
      numArray3[30] = (byte) 66;
      numArray3[36] = (byte) 251;
      numArray3[31 /*0x1F*/] = (byte) 197;
      numArray3[32 /*0x20*/] = (byte) 223;
      numArray3[26] = (byte) 25;
      numArray3[34] = (byte) 210;
      numArray3[1] = (byte) 164;
      numArray3[9] = (byte) 65;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      byte[] response = new byte[37];
      Array.Copy((Array) sc_13210.sspq, 416, (Array) numArray4, 0, 37);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 416, (Array) numArray4, 0, 37);
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
      (byte) 50,
      (byte) 66,
      (byte) 20,
      (byte) 168,
      (byte) 52,
      (byte) 159,
      (byte) 44,
      (byte) 60,
      (byte) 222,
      (byte) 32 /*0x20*/,
      (byte) 54,
      (byte) 152,
      (byte) 153,
      (byte) 236,
      (byte) 62,
      (byte) 39,
      (byte) 182,
      (byte) 128 /*0x80*/,
      (byte) 105,
      (byte) 224 /*0xE0*/,
      (byte) 236,
      (byte) 78,
      (byte) 110,
      (byte) 168,
      (byte) 92,
      (byte) 213,
      (byte) 187,
      (byte) 226,
      (byte) 242,
      (byte) 225,
      (byte) 114,
      (byte) 165,
      (byte) 57,
      (byte) 148,
      (byte) 2,
      (byte) 122,
      (byte) 7
    };
    byte[] numArray7 = new byte[37]
    {
      (byte) 193,
      (byte) 115,
      (byte) 241,
      (byte) 70,
      (byte) 245,
      (byte) 254,
      (byte) 86,
      (byte) 120,
      (byte) 201,
      (byte) 32 /*0x20*/,
      (byte) 163,
      (byte) 139,
      (byte) 23,
      (byte) 41,
      (byte) 253,
      byte.MaxValue,
      (byte) 214,
      (byte) 174,
      (byte) 107,
      (byte) 251,
      (byte) 102,
      (byte) 187,
      (byte) 13,
      (byte) 183,
      (byte) 36,
      (byte) 127 /*0x7F*/,
      (byte) 166,
      (byte) 42,
      (byte) 110,
      (byte) 217,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 242,
      (byte) 232,
      (byte) 195,
      (byte) 101,
      (byte) 199
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13265()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[14] = (byte) 227;
      numArray2[1] = (byte) 49;
      numArray2[13] = (byte) 2;
      numArray2[3] = (byte) 102;
      numArray2[4] = (byte) 217;
      numArray2[5] = (byte) 24;
      numArray2[6] = (byte) 84;
      numArray2[16 /*0x10*/] = (byte) 107;
      numArray2[17] = (byte) 75;
      numArray2[9] = (byte) 109;
      numArray2[10] = (byte) 218;
      numArray2[8] = (byte) 94;
      numArray2[12] = (byte) 55;
      numArray2[0] = (byte) 217;
      numArray2[18] = (byte) 94;
      numArray2[15] = (byte) 212;
      numArray2[2] = (byte) 189;
      numArray2[11] = (byte) 196;
      numArray2[7] = (byte) 52;
      byte[] numArray3 = new byte[19]
      {
        (byte) 201,
        (byte) 68,
        (byte) 181,
        (byte) 132,
        (byte) 82,
        (byte) 61,
        (byte) 26,
        (byte) 185,
        (byte) 43,
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 65,
        (byte) 211,
        (byte) 161,
        (byte) 77,
        (byte) 103,
        (byte) 116,
        (byte) 210,
        (byte) 163
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
      (byte) 174,
      (byte) 148,
      (byte) 116,
      (byte) 151,
      (byte) 224 /*0xE0*/,
      (byte) 1,
      (byte) 145,
      (byte) 31 /*0x1F*/,
      (byte) 165,
      (byte) 155,
      (byte) 237,
      (byte) 68,
      (byte) 19,
      (byte) 227,
      (byte) 143,
      (byte) 52,
      (byte) 14,
      (byte) 212,
      (byte) 122
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 247,
      (byte) 179,
      (byte) 43,
      (byte) 66,
      (byte) 15,
      (byte) 220,
      (byte) 10,
      (byte) 131,
      (byte) 129,
      (byte) 10,
      (byte) 64 /*0x40*/,
      (byte) 78,
      (byte) 43,
      (byte) 73,
      (byte) 130,
      (byte) 147,
      (byte) 31 /*0x1F*/,
      (byte) 10,
      (byte) 14
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13266()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[4] = (byte) 239;
      numArray2[1] = (byte) 252;
      numArray2[9] = (byte) 160 /*0xA0*/;
      numArray2[3] = (byte) 131;
      numArray2[2] = (byte) 81;
      numArray2[5] = (byte) 198;
      numArray2[6] = (byte) 36;
      numArray2[10] = (byte) 125;
      numArray2[0] = (byte) 240 /*0xF0*/;
      numArray2[7] = (byte) 40;
      numArray2[8] = (byte) 200;
      numArray2[11] = (byte) 155;
      byte[] numArray3 = new byte[12]
      {
        (byte) 83,
        (byte) 250,
        (byte) 216,
        (byte) 14,
        (byte) 200,
        (byte) 97,
        (byte) 235,
        (byte) 142,
        (byte) 89,
        (byte) 44,
        (byte) 75,
        (byte) 183
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[1] = (byte) 110;
    numArray5[9] = (byte) 143;
    numArray5[2] = (byte) 9;
    numArray5[3] = (byte) 169;
    numArray5[4] = (byte) 251;
    numArray5[11] = (byte) 162;
    numArray5[6] = (byte) 85;
    numArray5[7] = (byte) 132;
    numArray5[8] = (byte) 154;
    numArray5[0] = (byte) 161;
    numArray5[10] = (byte) 125;
    numArray5[5] = (byte) 61;
    byte[] numArray6 = new byte[12];
    numArray6[1] = (byte) 250;
    numArray6[10] = (byte) 70;
    numArray6[9] = (byte) 160 /*0xA0*/;
    numArray6[3] = (byte) 163;
    numArray6[4] = (byte) 38;
    numArray6[5] = (byte) 242;
    numArray6[6] = (byte) 184;
    numArray6[7] = (byte) 229;
    numArray6[8] = (byte) 219;
    numArray6[11] = (byte) 233;
    numArray6[2] = (byte) 240 /*0xF0*/;
    numArray6[0] = (byte) 181;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13267()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 95,
        (byte) 4,
        (byte) 124,
        (byte) 33,
        (byte) 223,
        (byte) 208 /*0xD0*/,
        (byte) 113,
        (byte) 156,
        (byte) 17,
        (byte) 106
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 189,
        (byte) 40,
        (byte) 134,
        (byte) 28,
        (byte) 40,
        (byte) 32 /*0x20*/,
        (byte) 223,
        (byte) 254,
        (byte) 250,
        (byte) 210
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[0] = (byte) 171;
    numArray5[8] = (byte) 155;
    numArray5[2] = (byte) 169;
    numArray5[3] = (byte) 74;
    numArray5[5] = (byte) 9;
    numArray5[7] = (byte) 234;
    numArray5[6] = (byte) 89;
    numArray5[4] = (byte) 236;
    numArray5[1] = (byte) 60;
    numArray5[9] = (byte) 35;
    byte[] numArray6 = new byte[10]
    {
      (byte) 137,
      (byte) 157,
      (byte) 44,
      (byte) 158,
      (byte) 128 /*0x80*/,
      (byte) 152,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 92
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13268()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33];
      numArray2[1] = (byte) 61;
      numArray2[32 /*0x20*/] = (byte) 148;
      numArray2[2] = (byte) 207;
      numArray2[10] = (byte) 191;
      numArray2[18] = (byte) 196;
      numArray2[5] = (byte) 168;
      numArray2[12] = (byte) 216;
      numArray2[6] = (byte) 246;
      numArray2[8] = (byte) 42;
      numArray2[9] = (byte) 43;
      numArray2[26] = (byte) 88;
      numArray2[28] = (byte) 73;
      numArray2[7] = (byte) 108;
      numArray2[13] = (byte) 112 /*0x70*/;
      numArray2[21] = (byte) 133;
      numArray2[11] = (byte) 162;
      numArray2[16 /*0x10*/] = (byte) 40;
      numArray2[25] = (byte) 156;
      numArray2[0] = (byte) 2;
      numArray2[19] = (byte) 224 /*0xE0*/;
      numArray2[4] = (byte) 182;
      numArray2[15] = (byte) 237;
      numArray2[22] = (byte) 21;
      numArray2[14] = (byte) 210;
      numArray2[24] = (byte) 246;
      numArray2[23] = (byte) 153;
      numArray2[3] = (byte) 48 /*0x30*/;
      numArray2[27] = (byte) 4;
      numArray2[17] = (byte) 115;
      numArray2[29] = (byte) 180;
      numArray2[30] = (byte) 157;
      numArray2[31 /*0x1F*/] = (byte) 49;
      numArray2[20] = (byte) 68;
      byte[] numArray3 = new byte[33]
      {
        (byte) 220,
        (byte) 218,
        (byte) 9,
        (byte) 131,
        (byte) 41,
        (byte) 60,
        (byte) 188,
        (byte) 87,
        (byte) 99,
        (byte) 184,
        (byte) 85,
        (byte) 149,
        (byte) 74,
        (byte) 223,
        (byte) 145,
        (byte) 133,
        (byte) 22,
        (byte) 175,
        (byte) 168,
        (byte) 187,
        (byte) 28,
        (byte) 216,
        (byte) 249,
        (byte) 238,
        (byte) 192 /*0xC0*/,
        (byte) 30,
        (byte) 232,
        (byte) 60,
        (byte) 54,
        (byte) 205,
        (byte) 95,
        (byte) 235,
        (byte) 145
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 36,
      (byte) 178,
      (byte) 0,
      (byte) 63 /*0x3F*/,
      (byte) 209,
      (byte) 179,
      (byte) 121,
      (byte) 19,
      (byte) 118,
      (byte) 32 /*0x20*/,
      (byte) 30,
      (byte) 117,
      (byte) 16 /*0x10*/,
      (byte) 113,
      (byte) 138,
      (byte) 200,
      (byte) 39,
      (byte) 124,
      (byte) 204,
      (byte) 246,
      (byte) 201,
      (byte) 134,
      (byte) 4,
      (byte) 238,
      (byte) 25,
      (byte) 162,
      (byte) 59,
      (byte) 167,
      (byte) 250,
      (byte) 16 /*0x10*/,
      (byte) 234,
      (byte) 202,
      (byte) 124
    };
    byte[] numArray6 = new byte[33]
    {
      (byte) 47,
      (byte) 22,
      (byte) 59,
      (byte) 184,
      (byte) 20,
      (byte) 213,
      (byte) 138,
      (byte) 230,
      (byte) 98,
      (byte) 32 /*0x20*/,
      (byte) 103,
      (byte) 97,
      (byte) 136,
      (byte) 202,
      (byte) 132,
      (byte) 210,
      (byte) 12,
      (byte) 69,
      (byte) 197,
      (byte) 193,
      (byte) 24,
      (byte) 137,
      (byte) 210,
      (byte) 161,
      (byte) 46,
      (byte) 16 /*0x10*/,
      (byte) 242,
      (byte) 110,
      (byte) 124,
      (byte) 236,
      (byte) 160 /*0xA0*/,
      (byte) 6,
      (byte) 163
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13269()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 55,
        (byte) 227,
        (byte) 19,
        (byte) 189,
        (byte) 105,
        (byte) 177,
        (byte) 21,
        (byte) 27,
        (byte) 164,
        (byte) 63 /*0x3F*/,
        (byte) 127 /*0x7F*/,
        (byte) 152,
        (byte) 173,
        (byte) 102,
        (byte) 17,
        (byte) 65,
        (byte) 180,
        (byte) 96 /*0x60*/,
        (byte) 158
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 49,
        (byte) 223,
        (byte) 201,
        (byte) 109,
        (byte) 62,
        (byte) 1,
        (byte) 220,
        (byte) 238,
        (byte) 11,
        (byte) 148,
        byte.MaxValue,
        (byte) 173,
        (byte) 153,
        (byte) 24,
        (byte) 201,
        (byte) 124,
        (byte) 230,
        (byte) 49,
        (byte) 79
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
      (byte) 47,
      (byte) 120,
      (byte) 209,
      (byte) 24,
      (byte) 101,
      (byte) 54,
      (byte) 84,
      (byte) 248,
      (byte) 242,
      (byte) 3,
      (byte) 125,
      (byte) 221,
      (byte) 239,
      (byte) 152,
      (byte) 184,
      (byte) 56,
      (byte) 95,
      (byte) 212,
      (byte) 238
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 93,
      (byte) 83,
      (byte) 21,
      (byte) 195,
      (byte) 137,
      (byte) 243,
      (byte) 207,
      (byte) 212,
      (byte) 83,
      (byte) 133,
      (byte) 0,
      (byte) 218,
      (byte) 168,
      (byte) 29,
      (byte) 63 /*0x3F*/,
      (byte) 209,
      (byte) 182,
      (byte) 152,
      (byte) 244
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13270()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 14,
        (byte) 76,
        (byte) 170,
        (byte) 216,
        (byte) 136,
        (byte) 200,
        (byte) 190,
        (byte) 208 /*0xD0*/,
        (byte) 94,
        (byte) 217,
        (byte) 16 /*0x10*/,
        (byte) 180
      };
      byte[] numArray3 = new byte[12];
      numArray3[7] = (byte) 218;
      numArray3[0] = (byte) 31 /*0x1F*/;
      numArray3[8] = (byte) 74;
      numArray3[3] = (byte) 85;
      numArray3[11] = (byte) 225;
      numArray3[5] = (byte) 127 /*0x7F*/;
      numArray3[6] = (byte) 92;
      numArray3[4] = (byte) 13;
      numArray3[1] = (byte) 103;
      numArray3[9] = (byte) 239;
      numArray3[2] = (byte) 28;
      numArray3[10] = (byte) 91;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      byte[] response = new byte[35];
      Array.Copy((Array) sc_13210.sspq, 453, (Array) numArray4, 0, 35);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 453, (Array) numArray4, 0, 35);
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
    byte[] numArray6 = new byte[12];
    numArray6[7] = (byte) 144 /*0x90*/;
    numArray6[1] = (byte) 153;
    numArray6[9] = (byte) 252;
    numArray6[8] = (byte) 129;
    numArray6[6] = (byte) 133;
    numArray6[4] = (byte) 222;
    numArray6[0] = (byte) 158;
    numArray6[5] = (byte) 40;
    numArray6[2] = (byte) 94;
    numArray6[3] = (byte) 232;
    numArray6[10] = (byte) 123;
    numArray6[11] = (byte) 24;
    byte[] numArray7 = new byte[12]
    {
      (byte) 150,
      (byte) 114,
      (byte) 93,
      (byte) 3,
      (byte) 252,
      (byte) 102,
      (byte) 253,
      (byte) 224 /*0xE0*/,
      (byte) 102,
      (byte) 248,
      (byte) 9,
      (byte) 110
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_13271()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 0,
        (byte) 254,
        (byte) 0,
        (byte) 228,
        (byte) 118,
        (byte) 0
      };
      numArray2[2] = (byte) 202;
      numArray2[0] = (byte) 136;
      numArray2[5] = (byte) 43;
      byte[] numArray3 = new byte[6];
      numArray3[2] = (byte) 203;
      numArray3[0] = (byte) 214;
      numArray3[4] = (byte) 173;
      numArray3[3] = (byte) 115;
      numArray3[1] = (byte) 118;
      numArray3[5] = (byte) 165;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[15];
      byte[] response = new byte[15];
      Array.Copy((Array) sc_13210.sspq, 488, (Array) numArray4, 0, 15);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13210.sspr, 488, (Array) numArray4, 0, 15);
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
    byte[] numArray5 = new byte[6];
    byte[] numArray6 = new byte[6]
    {
      (byte) 151,
      (byte) 161,
      (byte) 30,
      (byte) 0,
      (byte) 97,
      (byte) 0
    };
    numArray6[3] = (byte) 122;
    numArray6[5] = (byte) 23;
    byte[] numArray7 = new byte[6]
    {
      (byte) 199,
      (byte) 80 /*0x50*/,
      (byte) 106,
      (byte) 232,
      (byte) 33,
      (byte) 218
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_13272(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 159,
      (byte) 235,
      (byte) 52,
      (byte) 54,
      (byte) 198,
      (byte) 170,
      (byte) 101,
      (byte) 146,
      (byte) 6,
      (byte) 11,
      (byte) 199,
      (byte) 222,
      (byte) 201,
      (byte) 83,
      (byte) 240 /*0xF0*/,
      (byte) 11,
      (byte) 219,
      (byte) 181,
      (byte) 92,
      (byte) 214,
      (byte) 125,
      (byte) 78,
      (byte) 133,
      (byte) 46,
      (byte) 104,
      (byte) 17,
      (byte) 98,
      (byte) 126,
      (byte) 204,
      (byte) 60,
      (byte) 215,
      (byte) 124,
      (byte) 215,
      (byte) 130,
      (byte) 149,
      (byte) 27,
      (byte) 78,
      (byte) 251,
      (byte) 208 /*0xD0*/,
      (byte) 246,
      (byte) 184,
      (byte) 232,
      (byte) 90,
      (byte) 11,
      (byte) 211,
      (byte) 78,
      (byte) 65,
      (byte) 21
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[33] = (byte) 63 /*0x3F*/;
    sourceArray2[22] = (byte) 91;
    sourceArray2[5] = (byte) 215;
    sourceArray2[3] = (byte) 136;
    sourceArray2[28] = (byte) 84;
    sourceArray2[4] = (byte) 128 /*0x80*/;
    sourceArray2[7] = (byte) 42;
    sourceArray2[20] = (byte) 107;
    sourceArray2[41] = (byte) 21;
    sourceArray2[9] = (byte) 162;
    sourceArray2[10] = (byte) 203;
    sourceArray2[11] = (byte) 237;
    sourceArray2[19] = (byte) 56;
    sourceArray2[34] = (byte) 247;
    sourceArray2[32 /*0x20*/] = (byte) 229;
    sourceArray2[15] = (byte) 236;
    sourceArray2[16 /*0x10*/] = (byte) 6;
    sourceArray2[17] = (byte) 215;
    sourceArray2[14] = (byte) 12;
    sourceArray2[18] = (byte) 96 /*0x60*/;
    sourceArray2[12] = (byte) 223;
    sourceArray2[21] = (byte) 221;
    sourceArray2[24] = (byte) 142;
    sourceArray2[23] = (byte) 233;
    sourceArray2[45] = (byte) 177;
    sourceArray2[25] = (byte) 222;
    sourceArray2[0] = (byte) 126;
    sourceArray2[27] = (byte) 27;
    sourceArray2[6] = (byte) 23;
    sourceArray2[29] = (byte) 66;
    sourceArray2[30] = (byte) 100;
    sourceArray2[2] = (byte) 63 /*0x3F*/;
    sourceArray2[40] = (byte) 153;
    sourceArray2[13] = (byte) 99;
    sourceArray2[42] = (byte) 4;
    sourceArray2[26] = (byte) 48 /*0x30*/;
    sourceArray2[1] = (byte) 98;
    sourceArray2[37] = (byte) 214;
    sourceArray2[38] = (byte) 200;
    sourceArray2[39] = (byte) 76;
    sourceArray2[35] = (byte) 118;
    sourceArray2[8] = (byte) 148;
    sourceArray2[31 /*0x1F*/] = (byte) 196;
    sourceArray2[43] = (byte) 201;
    sourceArray2[44] = (byte) 199;
    sourceArray2[36] = (byte) 204;
    sourceArray2[46] = (byte) 93;
    sourceArray2[47] = (byte) 102;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[33];
    byte[] response2 = new byte[33];
    Array.Copy((Array) sc_13210.sspq, 503, (Array) numArray2, 0, 33);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13210.sspr, 503, (Array) numArray2, 0, 33);
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

  internal static string ssp_appserver_13273()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[148];
      byte[] numArray2 = new byte[55];
      numArray2[40] = (byte) 86;
      numArray2[48 /*0x30*/] = (byte) 149;
      numArray2[2] = (byte) 245;
      numArray2[4] = (byte) 92;
      numArray2[35] = (byte) 17;
      numArray2[5] = (byte) 133;
      numArray2[38] = (byte) 178;
      numArray2[7] = (byte) 44;
      numArray2[8] = (byte) 143;
      numArray2[31 /*0x1F*/] = (byte) 250;
      numArray2[10] = (byte) 230;
      numArray2[49] = (byte) 3;
      numArray2[54] = (byte) 112 /*0x70*/;
      numArray2[21] = (byte) 99;
      numArray2[9] = (byte) 102;
      numArray2[39] = (byte) 83;
      numArray2[16 /*0x10*/] = (byte) 17;
      numArray2[34] = (byte) 225;
      numArray2[18] = (byte) 183;
      numArray2[19] = (byte) 148;
      numArray2[11] = (byte) 199;
      numArray2[14] = (byte) 52;
      numArray2[32 /*0x20*/] = (byte) 75;
      numArray2[23] = (byte) 222;
      numArray2[24] = (byte) 98;
      numArray2[15] = (byte) 179;
      numArray2[26] = (byte) 184;
      numArray2[0] = (byte) 210;
      numArray2[13] = (byte) 121;
      numArray2[6] = (byte) 237;
      numArray2[30] = (byte) 81;
      numArray2[43] = (byte) 182;
      numArray2[45] = (byte) 87;
      numArray2[33] = (byte) 218;
      numArray2[12] = (byte) 156;
      numArray2[44] = (byte) 146;
      numArray2[36] = (byte) 5;
      numArray2[37] = (byte) 27;
      numArray2[46] = (byte) 216;
      numArray2[17] = (byte) 19;
      numArray2[29] = (byte) 72;
      numArray2[41] = (byte) 252;
      numArray2[42] = (byte) 83;
      numArray2[20] = (byte) 96 /*0x60*/;
      numArray2[28] = (byte) 62;
      numArray2[25] = (byte) 228;
      numArray2[22] = (byte) 221;
      numArray2[47] = (byte) 57;
      numArray2[3] = (byte) 164;
      numArray2[51] = (byte) 94;
      numArray2[50] = (byte) 59;
      numArray2[27] = (byte) 56;
      numArray2[52] = (byte) 171;
      numArray2[53] = (byte) 13;
      numArray2[1] = (byte) 212;
      byte[] numArray3 = new byte[55]
      {
        (byte) 21,
        (byte) 114,
        (byte) 122,
        (byte) 230,
        (byte) 238,
        (byte) 153,
        (byte) 191,
        (byte) 87,
        (byte) 236,
        (byte) 208 /*0xD0*/,
        (byte) 146,
        (byte) 115,
        (byte) 48 /*0x30*/,
        (byte) 178,
        (byte) 99,
        (byte) 173,
        (byte) 160 /*0xA0*/,
        (byte) 23,
        (byte) 39,
        (byte) 108,
        (byte) 109,
        (byte) 18,
        (byte) 174,
        (byte) 25,
        (byte) 247,
        (byte) 28,
        (byte) 130,
        (byte) 15,
        (byte) 92,
        (byte) 226,
        (byte) 86,
        (byte) 182,
        (byte) 111,
        (byte) 170,
        (byte) 235,
        (byte) 63 /*0x3F*/,
        (byte) 53,
        (byte) 145,
        (byte) 225,
        (byte) 189,
        (byte) 24,
        (byte) 150,
        (byte) 99,
        (byte) 209,
        (byte) 4,
        (byte) 128 /*0x80*/,
        (byte) 25,
        (byte) 233,
        (byte) 159,
        (byte) 30,
        (byte) 121,
        (byte) 144 /*0x90*/,
        (byte) 177,
        (byte) 21,
        (byte) 22
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 202,
        (byte) 55,
        (byte) 106,
        (byte) 78,
        (byte) 17,
        (byte) 179,
        (byte) 56,
        (byte) 33,
        (byte) 156,
        (byte) 61,
        (byte) 88,
        (byte) 37,
        (byte) 7,
        (byte) 189,
        (byte) 28,
        (byte) 37,
        (byte) 222,
        (byte) 156,
        (byte) 86,
        (byte) 2,
        (byte) 132,
        (byte) 166,
        (byte) 46,
        (byte) 246,
        (byte) 222,
        (byte) 47,
        (byte) 228,
        (byte) 200,
        (byte) 244,
        (byte) 8,
        (byte) 111,
        (byte) 94,
        (byte) 19,
        (byte) 74,
        (byte) 36,
        (byte) 161,
        (byte) 208 /*0xD0*/,
        (byte) 55,
        (byte) 143,
        (byte) 90,
        (byte) 212,
        (byte) 128 /*0x80*/,
        (byte) 9,
        (byte) 158,
        (byte) 189,
        (byte) 123,
        (byte) 107,
        (byte) 152,
        (byte) 40,
        (byte) 216,
        (byte) 104,
        (byte) 50,
        (byte) 180,
        (byte) 92,
        (byte) 75
      };
      byte[] numArray5 = new byte[55];
      numArray5[51] = (byte) 89;
      numArray5[1] = (byte) 48 /*0x30*/;
      numArray5[29] = (byte) 251;
      numArray5[3] = (byte) 100;
      numArray5[0] = (byte) 204;
      numArray5[27] = (byte) 161;
      numArray5[4] = (byte) 245;
      numArray5[7] = (byte) 117;
      numArray5[10] = (byte) 16 /*0x10*/;
      numArray5[48 /*0x30*/] = (byte) 193;
      numArray5[8] = (byte) 50;
      numArray5[18] = (byte) 208 /*0xD0*/;
      numArray5[34] = (byte) 245;
      numArray5[13] = (byte) 253;
      numArray5[14] = (byte) 190;
      numArray5[44] = (byte) 143;
      numArray5[19] = (byte) 28;
      numArray5[17] = (byte) 149;
      numArray5[24] = (byte) 110;
      numArray5[47] = (byte) 38;
      numArray5[11] = (byte) 54;
      numArray5[21] = (byte) 12;
      numArray5[23] = (byte) 151;
      numArray5[50] = (byte) 124;
      numArray5[46] = (byte) 179;
      numArray5[54] = (byte) 52;
      numArray5[53] = (byte) 63 /*0x3F*/;
      numArray5[16 /*0x10*/] = (byte) 203;
      numArray5[28] = (byte) 254;
      numArray5[15] = (byte) 141;
      numArray5[30] = (byte) 4;
      numArray5[31 /*0x1F*/] = (byte) 164;
      numArray5[32 /*0x20*/] = (byte) 234;
      numArray5[37] = (byte) 29;
      numArray5[6] = (byte) 89;
      numArray5[35] = (byte) 49;
      numArray5[36] = (byte) 48 /*0x30*/;
      numArray5[52] = (byte) 160 /*0xA0*/;
      numArray5[5] = (byte) 107;
      numArray5[39] = (byte) 130;
      numArray5[25] = (byte) 224 /*0xE0*/;
      numArray5[41] = (byte) 49;
      numArray5[9] = (byte) 210;
      numArray5[43] = (byte) 125;
      numArray5[20] = (byte) 204;
      numArray5[38] = (byte) 143;
      numArray5[45] = (byte) 19;
      numArray5[22] = (byte) 183;
      numArray5[12] = (byte) 217;
      numArray5[42] = (byte) 128 /*0x80*/;
      numArray5[49] = (byte) 237;
      numArray5[26] = (byte) 163;
      numArray5[40] = (byte) 83;
      numArray5[2] = (byte) 52;
      numArray5[33] = (byte) 61;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[38];
      numArray6[3] = (byte) 130;
      numArray6[16 /*0x10*/] = (byte) 111;
      numArray6[2] = (byte) 29;
      numArray6[23] = (byte) 21;
      numArray6[35] = (byte) 186;
      numArray6[12] = (byte) 226;
      numArray6[6] = (byte) 243;
      numArray6[7] = (byte) 146;
      numArray6[32 /*0x20*/] = (byte) 120;
      numArray6[25] = (byte) 243;
      numArray6[15] = (byte) 94;
      numArray6[11] = (byte) 152;
      numArray6[8] = (byte) 80 /*0x50*/;
      numArray6[28] = (byte) 22;
      numArray6[14] = (byte) 223;
      numArray6[10] = (byte) 189;
      numArray6[5] = (byte) 16 /*0x10*/;
      numArray6[17] = (byte) 202;
      numArray6[18] = (byte) 176 /*0xB0*/;
      numArray6[31 /*0x1F*/] = (byte) 183;
      numArray6[13] = (byte) 248;
      numArray6[1] = (byte) 13;
      numArray6[21] = (byte) 119;
      numArray6[20] = (byte) 193;
      numArray6[24] = (byte) 76;
      numArray6[0] = (byte) 58;
      numArray6[29] = (byte) 241;
      numArray6[27] = (byte) 222;
      numArray6[9] = (byte) 90;
      numArray6[26] = (byte) 152;
      numArray6[19] = (byte) 37;
      numArray6[30] = (byte) 137;
      numArray6[22] = (byte) 249;
      numArray6[33] = (byte) 32 /*0x20*/;
      numArray6[34] = (byte) 34;
      numArray6[36] = (byte) 144 /*0x90*/;
      numArray6[4] = (byte) 208 /*0xD0*/;
      numArray6[37] = (byte) 9;
      byte[] numArray7 = new byte[38];
      numArray7[31 /*0x1F*/] = (byte) 190;
      numArray7[15] = (byte) 137;
      numArray7[2] = (byte) 92;
      numArray7[3] = (byte) 232;
      numArray7[1] = (byte) 86;
      numArray7[5] = (byte) 213;
      numArray7[26] = (byte) 89;
      numArray7[7] = (byte) 3;
      numArray7[9] = (byte) 135;
      numArray7[11] = (byte) 122;
      numArray7[10] = (byte) 75;
      numArray7[30] = (byte) 217;
      numArray7[12] = (byte) 135;
      numArray7[13] = (byte) 80 /*0x50*/;
      numArray7[27] = (byte) 253;
      numArray7[17] = (byte) 225;
      numArray7[8] = (byte) 52;
      numArray7[19] = (byte) 169;
      numArray7[6] = (byte) 0;
      numArray7[23] = (byte) 61;
      numArray7[28] = (byte) 241;
      numArray7[37] = (byte) 132;
      numArray7[22] = (byte) 37;
      numArray7[32 /*0x20*/] = (byte) 144 /*0x90*/;
      numArray7[24] = (byte) 191;
      numArray7[25] = (byte) 49;
      numArray7[35] = (byte) 136;
      numArray7[4] = (byte) 207;
      numArray7[16 /*0x10*/] = (byte) 137;
      numArray7[29] = (byte) 53;
      numArray7[21] = (byte) 204;
      numArray7[14] = (byte) 15;
      numArray7[20] = (byte) 31 /*0x1F*/;
      numArray7[33] = (byte) 247;
      numArray7[34] = (byte) 135;
      numArray7[18] = (byte) 204;
      numArray7[36] = (byte) 139;
      numArray7[0] = (byte) 219;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[148];
    byte[] numArray9 = new byte[55];
    numArray9[40] = (byte) 62;
    numArray9[26] = (byte) 54;
    numArray9[53] = (byte) 20;
    numArray9[42] = (byte) 182;
    numArray9[19] = (byte) 87;
    numArray9[5] = (byte) 89;
    numArray9[47] = (byte) 111;
    numArray9[24] = (byte) 248;
    numArray9[8] = (byte) 68;
    numArray9[11] = (byte) 163;
    numArray9[10] = (byte) 126;
    numArray9[36] = (byte) 146;
    numArray9[33] = (byte) 83;
    numArray9[13] = (byte) 195;
    numArray9[48 /*0x30*/] = (byte) 205;
    numArray9[15] = (byte) 172;
    numArray9[41] = (byte) 61;
    numArray9[17] = (byte) 157;
    numArray9[18] = (byte) 34;
    numArray9[27] = (byte) 160 /*0xA0*/;
    numArray9[20] = (byte) 63 /*0x3F*/;
    numArray9[45] = (byte) 132;
    numArray9[22] = (byte) 198;
    numArray9[3] = (byte) 229;
    numArray9[28] = (byte) 125;
    numArray9[25] = (byte) 87;
    numArray9[49] = (byte) 123;
    numArray9[1] = (byte) 182;
    numArray9[12] = (byte) 54;
    numArray9[29] = (byte) 166;
    numArray9[0] = (byte) 45;
    numArray9[54] = (byte) 217;
    numArray9[32 /*0x20*/] = (byte) 94;
    numArray9[34] = (byte) 147;
    numArray9[30] = (byte) 128 /*0x80*/;
    numArray9[35] = (byte) 93;
    numArray9[6] = (byte) 119;
    numArray9[21] = (byte) 83;
    numArray9[38] = (byte) 62;
    numArray9[39] = (byte) 174;
    numArray9[50] = (byte) 168;
    numArray9[7] = (byte) 126;
    numArray9[16 /*0x10*/] = (byte) 58;
    numArray9[14] = (byte) 240 /*0xF0*/;
    numArray9[44] = (byte) 174;
    numArray9[31 /*0x1F*/] = (byte) 4;
    numArray9[46] = (byte) 136;
    numArray9[4] = (byte) 9;
    numArray9[23] = (byte) 248;
    numArray9[52] = (byte) 143;
    numArray9[43] = (byte) 77;
    numArray9[51] = (byte) 180;
    numArray9[9] = (byte) 148;
    numArray9[2] = (byte) 171;
    numArray9[37] = (byte) 5;
    byte[] numArray10 = new byte[55]
    {
      (byte) 37,
      (byte) 86,
      (byte) 195,
      (byte) 222,
      (byte) 173,
      (byte) 18,
      (byte) 30,
      (byte) 208 /*0xD0*/,
      (byte) 172,
      (byte) 137,
      (byte) 41,
      (byte) 136,
      (byte) 198,
      (byte) 79,
      (byte) 133,
      (byte) 141,
      (byte) 133,
      (byte) 122,
      (byte) 22,
      (byte) 5,
      (byte) 77,
      (byte) 189,
      (byte) 0,
      (byte) 74,
      (byte) 88,
      (byte) 229,
      (byte) 9,
      (byte) 245,
      (byte) 146,
      (byte) 177,
      (byte) 6,
      (byte) 53,
      (byte) 252,
      (byte) 221,
      (byte) 219,
      (byte) 109,
      (byte) 105,
      (byte) 82,
      (byte) 131,
      (byte) 81,
      (byte) 39,
      (byte) 241,
      (byte) 24,
      (byte) 226,
      (byte) 92,
      (byte) 215,
      (byte) 101,
      (byte) 194,
      (byte) 47,
      (byte) 235,
      (byte) 46,
      (byte) 114,
      (byte) 98,
      (byte) 202,
      (byte) 206
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 105,
      (byte) 121,
      (byte) 226,
      (byte) 90,
      (byte) 4,
      (byte) 254,
      (byte) 159,
      (byte) 70,
      (byte) 4,
      (byte) 249,
      (byte) 104,
      (byte) 119,
      (byte) 38,
      (byte) 58,
      (byte) 27,
      (byte) 209,
      (byte) 232,
      (byte) 177,
      (byte) 196,
      (byte) 133,
      (byte) 63 /*0x3F*/,
      (byte) 20,
      (byte) 51,
      (byte) 41,
      (byte) 136,
      (byte) 152,
      (byte) 160 /*0xA0*/,
      (byte) 138,
      (byte) 119,
      (byte) 90,
      (byte) 79,
      (byte) 162,
      (byte) 124,
      (byte) 247,
      (byte) 118,
      (byte) 160 /*0xA0*/,
      (byte) 170,
      (byte) 49,
      (byte) 98,
      (byte) 62,
      (byte) 121,
      (byte) 243,
      (byte) 48 /*0x30*/,
      (byte) 70,
      (byte) 61,
      (byte) 200,
      (byte) 19,
      (byte) 38,
      (byte) 88,
      (byte) 131,
      (byte) 50,
      (byte) 73,
      (byte) 19,
      (byte) 182,
      (byte) 54
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 38,
      (byte) 172,
      (byte) 62,
      (byte) 10,
      (byte) 8,
      (byte) 57,
      (byte) 41,
      (byte) 88,
      (byte) 23,
      (byte) 6,
      (byte) 133,
      (byte) 246,
      (byte) 25,
      (byte) 200,
      (byte) 53,
      (byte) 215,
      (byte) 186,
      (byte) 232,
      (byte) 38,
      (byte) 84,
      (byte) 19,
      (byte) 187,
      (byte) 108,
      (byte) 156,
      (byte) 25,
      (byte) 204,
      (byte) 186,
      (byte) 62,
      (byte) 76,
      (byte) 232,
      (byte) 18,
      (byte) 192 /*0xC0*/,
      (byte) 82,
      (byte) 243,
      (byte) 233,
      (byte) 197,
      (byte) 121,
      (byte) 43,
      (byte) 130,
      (byte) 191,
      (byte) 177,
      (byte) 22,
      (byte) 50,
      (byte) 119,
      (byte) 77,
      (byte) 162,
      (byte) 50,
      (byte) 37,
      (byte) 132,
      (byte) 204,
      (byte) 117,
      (byte) 203,
      (byte) 193,
      (byte) 39,
      (byte) 56
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[38];
    numArray13[17] = (byte) 12;
    numArray13[35] = (byte) 94;
    numArray13[2] = (byte) 108;
    numArray13[12] = (byte) 123;
    numArray13[28] = (byte) 6;
    numArray13[5] = (byte) 206;
    numArray13[6] = (byte) 170;
    numArray13[7] = (byte) 170;
    numArray13[25] = (byte) 50;
    numArray13[9] = (byte) 250;
    numArray13[1] = (byte) 210;
    numArray13[11] = (byte) 195;
    numArray13[3] = (byte) 94;
    numArray13[26] = (byte) 244;
    numArray13[14] = (byte) 183;
    numArray13[27] = (byte) 39;
    numArray13[16 /*0x10*/] = (byte) 134;
    numArray13[20] = (byte) 237;
    numArray13[4] = (byte) 20;
    numArray13[19] = (byte) 228;
    numArray13[15] = (byte) 78;
    numArray13[21] = (byte) 235;
    numArray13[13] = (byte) 38;
    numArray13[10] = (byte) 46;
    numArray13[24] = (byte) 121;
    numArray13[37] = (byte) 223;
    numArray13[30] = (byte) 198;
    numArray13[23] = (byte) 132;
    numArray13[22] = (byte) 192 /*0xC0*/;
    numArray13[0] = (byte) 184;
    numArray13[18] = (byte) 76;
    numArray13[31 /*0x1F*/] = (byte) 192 /*0xC0*/;
    numArray13[32 /*0x20*/] = (byte) 34;
    numArray13[33] = (byte) 90;
    numArray13[34] = (byte) 241;
    numArray13[8] = (byte) 197;
    numArray13[36] = (byte) 246;
    numArray13[29] = (byte) 142;
    byte[] numArray14 = new byte[38]
    {
      (byte) 143,
      (byte) 166,
      (byte) 221,
      (byte) 254,
      (byte) 62,
      (byte) 125,
      (byte) 154,
      (byte) 33,
      (byte) 82,
      (byte) 197,
      (byte) 196,
      (byte) 184,
      (byte) 161,
      (byte) 173,
      (byte) 165,
      (byte) 226,
      (byte) 227,
      (byte) 76,
      (byte) 21,
      (byte) 12,
      (byte) 127 /*0x7F*/,
      (byte) 39,
      (byte) 104,
      (byte) 11,
      (byte) 202,
      (byte) 43,
      (byte) 247,
      (byte) 25,
      (byte) 18,
      (byte) 162,
      (byte) 11,
      (byte) 89,
      (byte) 104,
      (byte) 214,
      (byte) 86,
      (byte) 242,
      (byte) 244,
      (byte) 98
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 38);
    for (int index = 0; index < 38; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
