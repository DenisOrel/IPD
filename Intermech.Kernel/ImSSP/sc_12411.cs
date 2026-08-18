// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12411
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12411
{
  private static byte[] sspq = new byte[322]
  {
    (byte) 62,
    (byte) 179,
    (byte) 115,
    (byte) 122,
    (byte) 23,
    (byte) 61,
    (byte) 136,
    (byte) 186,
    (byte) 216,
    byte.MaxValue,
    (byte) 45,
    (byte) 195,
    (byte) 29,
    (byte) 124,
    (byte) 213,
    (byte) 236,
    (byte) 202,
    (byte) 103,
    (byte) 49,
    (byte) 154,
    (byte) 239,
    (byte) 195,
    (byte) 166,
    (byte) 174,
    (byte) 213,
    (byte) 172,
    (byte) 204,
    (byte) 154,
    (byte) 75,
    (byte) 77,
    (byte) 203,
    (byte) 47,
    (byte) 206,
    (byte) 7,
    (byte) 64 /*0x40*/,
    (byte) 208 /*0xD0*/,
    (byte) 229,
    (byte) 188,
    (byte) 101,
    (byte) 155,
    (byte) 146,
    (byte) 130,
    (byte) 68,
    (byte) 48 /*0x30*/,
    (byte) 96 /*0x60*/,
    (byte) 173,
    (byte) 169,
    (byte) 213,
    (byte) 195,
    (byte) 128 /*0x80*/,
    (byte) 121,
    (byte) 212,
    (byte) 220,
    (byte) 124,
    (byte) 118,
    (byte) 57,
    (byte) 225,
    (byte) 164,
    (byte) 55,
    (byte) 88,
    (byte) 10,
    (byte) 29,
    (byte) 230,
    (byte) 122,
    (byte) 133,
    (byte) 124,
    (byte) 87,
    (byte) 17,
    (byte) 61,
    (byte) 208 /*0xD0*/,
    (byte) 207,
    (byte) 205,
    (byte) 145,
    (byte) 97,
    (byte) 103,
    (byte) 23,
    (byte) 63 /*0x3F*/,
    (byte) 94,
    (byte) 172,
    (byte) 214,
    (byte) 43,
    (byte) 212,
    (byte) 147,
    (byte) 98,
    (byte) 91,
    (byte) 195,
    (byte) 109,
    (byte) 82,
    (byte) 160 /*0xA0*/,
    (byte) 41,
    (byte) 102,
    (byte) 13,
    (byte) 220,
    (byte) 203,
    (byte) 17,
    (byte) 39,
    (byte) 144 /*0x90*/,
    (byte) 23,
    (byte) 180,
    (byte) 69,
    (byte) 100,
    (byte) 248,
    (byte) 164,
    (byte) 86,
    (byte) 205,
    (byte) 57,
    (byte) 230,
    (byte) 136,
    (byte) 217,
    (byte) 197,
    (byte) 133,
    (byte) 245,
    (byte) 241,
    (byte) 253,
    (byte) 225,
    (byte) 41,
    (byte) 238,
    (byte) 80 /*0x50*/,
    (byte) 151,
    (byte) 166,
    (byte) 43,
    (byte) 151,
    (byte) 19,
    (byte) 116,
    (byte) 46,
    (byte) 132,
    (byte) 226,
    (byte) 175,
    (byte) 62,
    (byte) 128 /*0x80*/,
    (byte) 153,
    (byte) 187,
    (byte) 100,
    (byte) 250,
    (byte) 214,
    (byte) 154,
    (byte) 99,
    (byte) 234,
    (byte) 161,
    (byte) 145,
    (byte) 146,
    (byte) 167,
    (byte) 233,
    (byte) 177,
    (byte) 86,
    (byte) 237,
    (byte) 238,
    (byte) 75,
    (byte) 122,
    (byte) 177,
    (byte) 115,
    (byte) 114,
    (byte) 80 /*0x50*/,
    (byte) 216,
    (byte) 68,
    (byte) 99,
    (byte) 90,
    (byte) 129,
    (byte) 224 /*0xE0*/,
    (byte) 103,
    (byte) 23,
    (byte) 212,
    (byte) 54,
    (byte) 165,
    (byte) 66,
    (byte) 221,
    (byte) 35,
    (byte) 158,
    (byte) 37,
    (byte) 160 /*0xA0*/,
    (byte) 23,
    (byte) 175,
    (byte) 152,
    (byte) 39,
    (byte) 221,
    (byte) 246,
    (byte) 247,
    (byte) 185,
    (byte) 77,
    (byte) 234,
    (byte) 83,
    (byte) 13,
    (byte) 40,
    (byte) 250,
    (byte) 102,
    (byte) 72,
    (byte) 55,
    (byte) 150,
    byte.MaxValue,
    (byte) 85,
    (byte) 186,
    (byte) 191,
    (byte) 57,
    (byte) 1,
    (byte) 1,
    (byte) 249,
    (byte) 185,
    (byte) 207,
    (byte) 95,
    (byte) 91,
    (byte) 189,
    (byte) 144 /*0x90*/,
    (byte) 206,
    (byte) 29,
    (byte) 239,
    (byte) 235,
    (byte) 111,
    (byte) 132,
    (byte) 92,
    (byte) 217,
    (byte) 199,
    (byte) 106,
    (byte) 218,
    (byte) 19,
    (byte) 233,
    (byte) 215,
    (byte) 214,
    (byte) 92,
    (byte) 92,
    (byte) 109,
    (byte) 190,
    (byte) 50,
    (byte) 81,
    (byte) 65,
    (byte) 46,
    (byte) 250,
    (byte) 183,
    (byte) 183,
    (byte) 122,
    (byte) 83,
    (byte) 27,
    (byte) 224 /*0xE0*/,
    (byte) 195,
    (byte) 239,
    (byte) 182,
    (byte) 203,
    (byte) 36,
    (byte) 246,
    (byte) 136,
    (byte) 3,
    (byte) 93,
    (byte) 195,
    (byte) 136,
    (byte) 153,
    (byte) 120,
    (byte) 166,
    (byte) 119,
    (byte) 175,
    (byte) 208 /*0xD0*/,
    (byte) 156,
    (byte) 45,
    (byte) 202,
    (byte) 217,
    (byte) 219,
    (byte) 97,
    (byte) 129,
    (byte) 47,
    (byte) 254,
    (byte) 172,
    (byte) 152,
    (byte) 130,
    (byte) 36,
    (byte) 30,
    (byte) 47,
    (byte) 96 /*0x60*/,
    (byte) 209,
    (byte) 226,
    (byte) 82,
    (byte) 218,
    (byte) 64 /*0x40*/,
    (byte) 71,
    (byte) 231,
    (byte) 131,
    (byte) 243,
    (byte) 248,
    (byte) 155,
    (byte) 78,
    (byte) 118,
    (byte) 246,
    (byte) 238,
    (byte) 190,
    (byte) 46,
    (byte) 249,
    (byte) 219,
    (byte) 98,
    (byte) 236,
    (byte) 86,
    (byte) 28,
    (byte) 90,
    (byte) 170,
    (byte) 123,
    (byte) 195,
    (byte) 93,
    (byte) 144 /*0x90*/,
    (byte) 183,
    (byte) 222,
    (byte) 126,
    (byte) 237,
    (byte) 223,
    (byte) 126,
    (byte) 213,
    (byte) 45,
    (byte) 218,
    (byte) 191,
    (byte) 178,
    (byte) 122,
    (byte) 38,
    (byte) 2,
    (byte) 233,
    (byte) 198,
    (byte) 218,
    (byte) 156,
    (byte) 74,
    (byte) 35,
    (byte) 246,
    (byte) 102,
    (byte) 70,
    (byte) 221,
    (byte) 89,
    (byte) 81,
    (byte) 129,
    (byte) 36
  };
  private static byte[] sspr = new byte[322]
  {
    (byte) 231,
    (byte) 215,
    (byte) 189,
    (byte) 178,
    (byte) 188,
    (byte) 29,
    (byte) 137,
    (byte) 207,
    (byte) 116,
    (byte) 237,
    (byte) 89,
    (byte) 135,
    (byte) 194,
    (byte) 63 /*0x3F*/,
    (byte) 159,
    (byte) 79,
    (byte) 97,
    (byte) 179,
    (byte) 68,
    (byte) 178,
    (byte) 41,
    (byte) 245,
    (byte) 155,
    (byte) 121,
    (byte) 25,
    (byte) 4,
    (byte) 187,
    (byte) 42,
    (byte) 167,
    (byte) 227,
    (byte) 67,
    (byte) 137,
    (byte) 22,
    (byte) 230,
    (byte) 203,
    (byte) 219,
    (byte) 88,
    (byte) 165,
    (byte) 43,
    (byte) 248,
    (byte) 180,
    (byte) 65,
    (byte) 55,
    (byte) 25,
    (byte) 128 /*0x80*/,
    (byte) 50,
    (byte) 86,
    (byte) 81,
    (byte) 227,
    (byte) 69,
    (byte) 42,
    (byte) 140,
    (byte) 177,
    (byte) 28,
    (byte) 226,
    (byte) 146,
    (byte) 140,
    (byte) 156,
    (byte) 126,
    (byte) 118,
    (byte) 213,
    (byte) 201,
    (byte) 195,
    (byte) 5,
    (byte) 217,
    (byte) 6,
    (byte) 89,
    (byte) 51,
    (byte) 127 /*0x7F*/,
    (byte) 249,
    (byte) 64 /*0x40*/,
    (byte) 232,
    (byte) 238,
    (byte) 112 /*0x70*/,
    (byte) 35,
    (byte) 216,
    (byte) 62,
    (byte) 74,
    (byte) 235,
    (byte) 159,
    (byte) 72,
    (byte) 251,
    (byte) 113,
    (byte) 50,
    (byte) 1,
    (byte) 157,
    (byte) 121,
    (byte) 193,
    (byte) 21,
    (byte) 38,
    (byte) 47,
    (byte) 86,
    (byte) 60,
    (byte) 59,
    (byte) 206,
    (byte) 43,
    (byte) 98,
    (byte) 85,
    (byte) 41,
    (byte) 22,
    (byte) 187,
    (byte) 10,
    (byte) 67,
    (byte) 118,
    (byte) 85,
    (byte) 157,
    (byte) 151,
    (byte) 53,
    (byte) 220,
    (byte) 112 /*0x70*/,
    (byte) 133,
    (byte) 129,
    (byte) 62,
    (byte) 176 /*0xB0*/,
    (byte) 161,
    (byte) 30,
    (byte) 155,
    (byte) 61,
    (byte) 197,
    (byte) 177,
    (byte) 119,
    (byte) 113,
    (byte) 212,
    (byte) 241,
    (byte) 221,
    (byte) 242,
    (byte) 179,
    (byte) 232,
    (byte) 5,
    (byte) 65,
    (byte) 151,
    (byte) 12,
    (byte) 90,
    (byte) 28,
    (byte) 223,
    (byte) 84,
    (byte) 88,
    (byte) 154,
    (byte) 218,
    (byte) 81,
    (byte) 152,
    (byte) 94,
    (byte) 163,
    (byte) 175,
    (byte) 162,
    (byte) 218,
    (byte) 154,
    (byte) 105,
    (byte) 36,
    (byte) 33,
    (byte) 132,
    (byte) 85,
    (byte) 120,
    (byte) 145,
    (byte) 57,
    (byte) 47,
    (byte) 196,
    (byte) 137,
    (byte) 230,
    (byte) 124,
    (byte) 142,
    (byte) 201,
    (byte) 237,
    (byte) 93,
    (byte) 5,
    (byte) 134,
    (byte) 34,
    (byte) 162,
    (byte) 217,
    (byte) 88,
    (byte) 139,
    (byte) 148,
    (byte) 49,
    (byte) 3,
    (byte) 123,
    (byte) 18,
    (byte) 161,
    (byte) 163,
    (byte) 212,
    (byte) 230,
    (byte) 31 /*0x1F*/,
    (byte) 58,
    (byte) 183,
    (byte) 23,
    (byte) 147,
    (byte) 240 /*0xF0*/,
    (byte) 5,
    (byte) 97,
    (byte) 147,
    (byte) 107,
    (byte) 243,
    (byte) 118,
    (byte) 92,
    (byte) 212,
    (byte) 210,
    (byte) 89,
    (byte) 56,
    (byte) 81,
    (byte) 97,
    (byte) 193,
    (byte) 113,
    (byte) 146,
    (byte) 93,
    (byte) 251,
    (byte) 228,
    (byte) 150,
    (byte) 83,
    (byte) 108,
    (byte) 37,
    (byte) 82,
    (byte) 215,
    (byte) 231,
    (byte) 105,
    (byte) 162,
    (byte) 231,
    (byte) 59,
    (byte) 69,
    (byte) 15,
    (byte) 228,
    (byte) 101,
    (byte) 166,
    (byte) 236,
    (byte) 18,
    (byte) 58,
    (byte) 28,
    (byte) 91,
    (byte) 221,
    (byte) 242,
    (byte) 77,
    (byte) 207,
    (byte) 73,
    (byte) 200,
    (byte) 157,
    (byte) 204,
    (byte) 225,
    (byte) 119,
    (byte) 176 /*0xB0*/,
    (byte) 52,
    (byte) 76,
    (byte) 27,
    (byte) 48 /*0x30*/,
    (byte) 106,
    (byte) 27,
    (byte) 190,
    (byte) 121,
    (byte) 143,
    (byte) 5,
    (byte) 243,
    (byte) 145,
    (byte) 35,
    (byte) 60,
    (byte) 73,
    (byte) 113,
    (byte) 196,
    (byte) 229,
    (byte) 175,
    (byte) 209,
    (byte) 209,
    (byte) 10,
    (byte) 83,
    (byte) 186,
    (byte) 125,
    (byte) 37,
    (byte) 45,
    (byte) 222,
    (byte) 85,
    (byte) 112 /*0x70*/,
    (byte) 154,
    (byte) 208 /*0xD0*/,
    (byte) 215,
    (byte) 163,
    (byte) 236,
    (byte) 217,
    (byte) 97,
    (byte) 33,
    (byte) 243,
    (byte) 198,
    (byte) 41,
    (byte) 146,
    (byte) 32 /*0x20*/,
    (byte) 21,
    (byte) 244,
    (byte) 56,
    (byte) 146,
    (byte) 63 /*0x3F*/,
    (byte) 151,
    (byte) 251,
    (byte) 225,
    (byte) 105,
    (byte) 185,
    (byte) 217,
    (byte) 179,
    (byte) 1,
    (byte) 176 /*0xB0*/,
    (byte) 60,
    (byte) 147,
    (byte) 223,
    (byte) 51,
    (byte) 78,
    (byte) 124,
    (byte) 100,
    (byte) 34,
    (byte) 205,
    (byte) 182,
    (byte) 135,
    (byte) 101,
    (byte) 200,
    (byte) 253,
    (byte) 203,
    (byte) 138,
    (byte) 155,
    (byte) 162,
    (byte) 22,
    (byte) 185,
    (byte) 29,
    (byte) 245,
    (byte) 47,
    (byte) 81,
    (byte) 245,
    (byte) 74,
    (byte) 79,
    (byte) 123
  };

  internal static int ssp_appserver_12412(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 128 /*0x80*/,
      (byte) 45,
      (byte) 3,
      (byte) 31 /*0x1F*/,
      (byte) 118,
      (byte) 248,
      (byte) 187,
      (byte) 35,
      (byte) 235,
      (byte) 81,
      (byte) 152,
      (byte) 70,
      (byte) 235,
      (byte) 185,
      (byte) 146,
      (byte) 79,
      (byte) 96 /*0x60*/,
      (byte) 125,
      (byte) 125,
      (byte) 74,
      (byte) 47,
      (byte) 101,
      (byte) 55,
      (byte) 4,
      (byte) 54,
      (byte) 75,
      (byte) 147,
      (byte) 81,
      (byte) 233,
      (byte) 13,
      (byte) 154,
      (byte) 74,
      (byte) 185,
      (byte) 168,
      (byte) 18,
      (byte) 89,
      (byte) 80 /*0x50*/,
      (byte) 62,
      (byte) 135,
      (byte) 75,
      (byte) 51,
      (byte) 34,
      (byte) 119,
      (byte) 5,
      (byte) 35,
      (byte) 203,
      (byte) 246,
      (byte) 26
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 207;
    sourceArray2[46] = (byte) 184;
    sourceArray2[39] = (byte) 155;
    sourceArray2[26] = (byte) 60;
    sourceArray2[17] = (byte) 208 /*0xD0*/;
    sourceArray2[38] = (byte) 48 /*0x30*/;
    sourceArray2[44] = (byte) 16 /*0x10*/;
    sourceArray2[7] = (byte) 48 /*0x30*/;
    sourceArray2[25] = (byte) 169;
    sourceArray2[20] = (byte) 13;
    sourceArray2[37] = (byte) 153;
    sourceArray2[11] = (byte) 140;
    sourceArray2[3] = (byte) 222;
    sourceArray2[2] = (byte) 60;
    sourceArray2[14] = (byte) 22;
    sourceArray2[13] = (byte) 31 /*0x1F*/;
    sourceArray2[9] = (byte) 240 /*0xF0*/;
    sourceArray2[21] = (byte) 134;
    sourceArray2[6] = (byte) 45;
    sourceArray2[10] = (byte) 50;
    sourceArray2[4] = (byte) 11;
    sourceArray2[8] = (byte) 65;
    sourceArray2[24] = (byte) 137;
    sourceArray2[23] = (byte) 230;
    sourceArray2[5] = (byte) 106;
    sourceArray2[35] = (byte) 93;
    sourceArray2[18] = (byte) 238;
    sourceArray2[12] = (byte) 201;
    sourceArray2[28] = (byte) 45;
    sourceArray2[29] = (byte) 15;
    sourceArray2[30] = (byte) 245;
    sourceArray2[15] = (byte) 223;
    sourceArray2[31 /*0x1F*/] = (byte) 44;
    sourceArray2[0] = (byte) 101;
    sourceArray2[34] = (byte) 168;
    sourceArray2[16 /*0x10*/] = (byte) 132;
    sourceArray2[36] = (byte) 168;
    sourceArray2[27] = (byte) 71;
    sourceArray2[1] = (byte) 9;
    sourceArray2[22] = (byte) 238;
    sourceArray2[40] = (byte) 35;
    sourceArray2[41] = (byte) 131;
    sourceArray2[42] = (byte) 17;
    sourceArray2[43] = (byte) 207;
    sourceArray2[32 /*0x20*/] = (byte) 207;
    sourceArray2[45] = (byte) 168;
    sourceArray2[33] = (byte) 188;
    sourceArray2[47] = (byte) 157;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[30];
    byte[] response2 = new byte[30];
    Array.Copy((Array) sc_12411.sspq, 0, (Array) numArray2, 0, 30);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12411.sspr, 0, (Array) numArray2, 0, 30);
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

  internal static int ssp_appserver_12413(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 227,
      (byte) 11,
      (byte) 44,
      (byte) 162,
      (byte) 5,
      (byte) 36,
      (byte) 187,
      (byte) 111,
      (byte) 147,
      (byte) 164,
      (byte) 240 /*0xF0*/,
      (byte) 156,
      (byte) 201,
      (byte) 160 /*0xA0*/,
      (byte) 52,
      (byte) 217,
      (byte) 134,
      (byte) 249,
      byte.MaxValue,
      (byte) 129,
      (byte) 103,
      (byte) 180,
      (byte) 230,
      (byte) 123,
      (byte) 12,
      (byte) 196,
      (byte) 164,
      (byte) 107,
      (byte) 112 /*0x70*/,
      (byte) 238,
      (byte) 110,
      (byte) 139,
      (byte) 218,
      (byte) 217,
      (byte) 223,
      (byte) 17,
      (byte) 80 /*0x50*/,
      (byte) 51,
      (byte) 231,
      (byte) 166,
      (byte) 171,
      (byte) 194,
      (byte) 6,
      (byte) 74,
      (byte) 210,
      (byte) 10,
      (byte) 229,
      (byte) 52
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[11] = (byte) 202;
    sourceArray2[1] = (byte) 16 /*0x10*/;
    sourceArray2[2] = (byte) 127 /*0x7F*/;
    sourceArray2[3] = (byte) 43;
    sourceArray2[4] = (byte) 95;
    sourceArray2[45] = (byte) 128 /*0x80*/;
    sourceArray2[35] = (byte) 41;
    sourceArray2[46] = (byte) 50;
    sourceArray2[8] = (byte) 66;
    sourceArray2[33] = (byte) 72;
    sourceArray2[5] = (byte) 150;
    sourceArray2[27] = (byte) 118;
    sourceArray2[42] = (byte) 14;
    sourceArray2[13] = (byte) 201;
    sourceArray2[14] = (byte) 69;
    sourceArray2[37] = (byte) 146;
    sourceArray2[16 /*0x10*/] = (byte) 245;
    sourceArray2[17] = (byte) 200;
    sourceArray2[18] = (byte) 203;
    sourceArray2[28] = (byte) 174;
    sourceArray2[21] = (byte) 179;
    sourceArray2[44] = (byte) 99;
    sourceArray2[22] = (byte) 139;
    sourceArray2[23] = (byte) 94;
    sourceArray2[43] = (byte) 56;
    sourceArray2[6] = (byte) 175;
    sourceArray2[9] = (byte) 218;
    sourceArray2[12] = (byte) 173;
    sourceArray2[7] = (byte) 157;
    sourceArray2[26] = (byte) 93;
    sourceArray2[10] = (byte) 91;
    sourceArray2[20] = (byte) 64 /*0x40*/;
    sourceArray2[40] = (byte) 91;
    sourceArray2[34] = (byte) 106;
    sourceArray2[24] = (byte) 65;
    sourceArray2[32 /*0x20*/] = (byte) 123;
    sourceArray2[36] = (byte) 15;
    sourceArray2[25] = (byte) 175;
    sourceArray2[38] = (byte) 214;
    sourceArray2[39] = (byte) 148;
    sourceArray2[19] = (byte) 217;
    sourceArray2[41] = (byte) 131;
    sourceArray2[30] = (byte) 81;
    sourceArray2[29] = (byte) 47;
    sourceArray2[0] = (byte) 128 /*0x80*/;
    sourceArray2[31 /*0x1F*/] = (byte) 228;
    sourceArray2[15] = (byte) 72;
    sourceArray2[47] = (byte) 116;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12414(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 25;
    sourceArray1[22] = (byte) 15;
    sourceArray1[34] = (byte) 234;
    sourceArray1[3] = (byte) 107;
    sourceArray1[7] = (byte) 205;
    sourceArray1[30] = (byte) 75;
    sourceArray1[6] = (byte) 244;
    sourceArray1[1] = (byte) 115;
    sourceArray1[8] = (byte) 71;
    sourceArray1[14] = (byte) 14;
    sourceArray1[19] = (byte) 40;
    sourceArray1[20] = (byte) 138;
    sourceArray1[15] = (byte) 93;
    sourceArray1[33] = (byte) 186;
    sourceArray1[31 /*0x1F*/] = (byte) 134;
    sourceArray1[9] = (byte) 129;
    sourceArray1[16 /*0x10*/] = (byte) 130;
    sourceArray1[4] = (byte) 193;
    sourceArray1[2] = (byte) 83;
    sourceArray1[43] = (byte) 47;
    sourceArray1[5] = (byte) 64 /*0x40*/;
    sourceArray1[37] = (byte) 17;
    sourceArray1[11] = (byte) 7;
    sourceArray1[23] = (byte) 56;
    sourceArray1[38] = (byte) 25;
    sourceArray1[25] = (byte) 181;
    sourceArray1[26] = (byte) 107;
    sourceArray1[24] = (byte) 232;
    sourceArray1[28] = (byte) 122;
    sourceArray1[44] = (byte) 97;
    sourceArray1[0] = (byte) 244;
    sourceArray1[46] = (byte) 9;
    sourceArray1[32 /*0x20*/] = (byte) 60;
    sourceArray1[47] = (byte) 69;
    sourceArray1[13] = (byte) 97;
    sourceArray1[27] = (byte) 49;
    sourceArray1[35] = (byte) 253;
    sourceArray1[21] = (byte) 3;
    sourceArray1[17] = (byte) 202;
    sourceArray1[39] = (byte) 219;
    sourceArray1[40] = (byte) 73;
    sourceArray1[41] = (byte) 105;
    sourceArray1[42] = (byte) 165;
    sourceArray1[36] = (byte) 159;
    sourceArray1[29] = (byte) 226;
    sourceArray1[45] = (byte) 151;
    sourceArray1[12] = (byte) 147;
    sourceArray1[10] = (byte) 138;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 53,
      (byte) 107,
      (byte) 220,
      (byte) 40,
      (byte) 253,
      (byte) 180,
      (byte) 236,
      (byte) 116,
      (byte) 234,
      (byte) 44,
      (byte) 115,
      (byte) 188,
      (byte) 88,
      (byte) 237,
      (byte) 254,
      (byte) 19,
      (byte) 87,
      (byte) 2,
      (byte) 93,
      (byte) 71,
      (byte) 85,
      (byte) 38,
      (byte) 186,
      (byte) 4,
      (byte) 109,
      (byte) 59,
      (byte) 163,
      (byte) 87,
      (byte) 243,
      (byte) 132,
      (byte) 182,
      (byte) 153,
      (byte) 212,
      (byte) 6,
      (byte) 162,
      (byte) 220,
      (byte) 17,
      (byte) 149,
      (byte) 56,
      (byte) 212,
      (byte) 185,
      (byte) 45,
      (byte) 63 /*0x3F*/,
      (byte) 138,
      (byte) 43,
      (byte) 14,
      (byte) 127 /*0x7F*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[49];
    byte[] response2 = new byte[49];
    Array.Copy((Array) sc_12411.sspq, 30, (Array) numArray2, 0, 49);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12411.sspr, 30, (Array) numArray2, 0, 49);
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

  internal static int ssp_appserver_12415(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 65,
      (byte) 125,
      (byte) 121,
      (byte) 227,
      (byte) 247,
      (byte) 83,
      (byte) 82,
      (byte) 144 /*0x90*/,
      (byte) 6,
      (byte) 239,
      (byte) 64 /*0x40*/,
      (byte) 133,
      (byte) 13,
      (byte) 144 /*0x90*/,
      (byte) 117,
      (byte) 156,
      (byte) 202,
      (byte) 171,
      (byte) 228,
      (byte) 85,
      (byte) 151,
      (byte) 152,
      (byte) 242,
      (byte) 176 /*0xB0*/,
      (byte) 171,
      (byte) 66,
      (byte) 122,
      (byte) 20,
      (byte) 95,
      (byte) 249,
      (byte) 130,
      (byte) 247,
      (byte) 222,
      (byte) 122,
      (byte) 201,
      (byte) 159,
      (byte) 109,
      (byte) 150,
      (byte) 142,
      (byte) 40,
      (byte) 5,
      (byte) 128 /*0x80*/,
      (byte) 54,
      (byte) 225,
      (byte) 143,
      (byte) 72,
      (byte) 131,
      (byte) 209
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 229,
      (byte) 200,
      (byte) 116,
      (byte) 180,
      (byte) 217,
      (byte) 86,
      (byte) 100,
      (byte) 156,
      (byte) 229,
      (byte) 159,
      (byte) 181,
      (byte) 240 /*0xF0*/,
      (byte) 111,
      (byte) 22,
      (byte) 111,
      (byte) 146,
      (byte) 15,
      (byte) 103,
      (byte) 73,
      (byte) 77,
      (byte) 137,
      (byte) 216,
      (byte) 217,
      (byte) 213,
      (byte) 121,
      (byte) 227,
      (byte) 95,
      (byte) 140,
      (byte) 50,
      (byte) 192 /*0xC0*/,
      (byte) 162,
      (byte) 178,
      (byte) 242,
      (byte) 26,
      (byte) 82,
      (byte) 109,
      (byte) 70,
      (byte) 175,
      (byte) 234,
      (byte) 204,
      (byte) 251,
      (byte) 153,
      (byte) 95,
      (byte) 180,
      (byte) 148,
      (byte) 102,
      (byte) 218,
      (byte) 49
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12416(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[44] = (byte) 40;
    sourceArray1[1] = (byte) 69;
    sourceArray1[2] = (byte) 169;
    sourceArray1[39] = (byte) 95;
    sourceArray1[4] = (byte) 200;
    sourceArray1[27] = (byte) 191;
    sourceArray1[6] = (byte) 180;
    sourceArray1[14] = (byte) 146;
    sourceArray1[8] = (byte) 62;
    sourceArray1[9] = (byte) 49;
    sourceArray1[10] = (byte) 13;
    sourceArray1[13] = (byte) 167;
    sourceArray1[12] = (byte) 73;
    sourceArray1[22] = (byte) 23;
    sourceArray1[11] = (byte) 108;
    sourceArray1[35] = (byte) 53;
    sourceArray1[16 /*0x10*/] = (byte) 213;
    sourceArray1[43] = (byte) 79;
    sourceArray1[18] = (byte) 51;
    sourceArray1[3] = (byte) 138;
    sourceArray1[20] = (byte) 187;
    sourceArray1[21] = (byte) 80 /*0x50*/;
    sourceArray1[31 /*0x1F*/] = (byte) 132;
    sourceArray1[23] = (byte) 98;
    sourceArray1[24] = (byte) 17;
    sourceArray1[37] = (byte) 20;
    sourceArray1[25] = (byte) 33;
    sourceArray1[15] = (byte) 199;
    sourceArray1[5] = (byte) 55;
    sourceArray1[32 /*0x20*/] = (byte) 207;
    sourceArray1[0] = (byte) 3;
    sourceArray1[17] = (byte) 46;
    sourceArray1[36] = (byte) 32 /*0x20*/;
    sourceArray1[33] = (byte) 46;
    sourceArray1[34] = (byte) 218;
    sourceArray1[45] = (byte) 116;
    sourceArray1[19] = (byte) 29;
    sourceArray1[28] = (byte) 169;
    sourceArray1[30] = (byte) 228;
    sourceArray1[38] = (byte) 165;
    sourceArray1[40] = (byte) 217;
    sourceArray1[46] = (byte) 70;
    sourceArray1[42] = (byte) 198;
    sourceArray1[41] = (byte) 79;
    sourceArray1[26] = (byte) 99;
    sourceArray1[7] = (byte) 217;
    sourceArray1[29] = (byte) 33;
    sourceArray1[47] = (byte) 161;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 101,
      (byte) 137,
      (byte) 73,
      (byte) 128 /*0x80*/,
      (byte) 202,
      (byte) 47,
      (byte) 238,
      (byte) 104,
      (byte) 253,
      (byte) 67,
      (byte) 198,
      (byte) 204,
      (byte) 216,
      (byte) 153,
      (byte) 179,
      (byte) 249,
      (byte) 88,
      (byte) 14,
      (byte) 37,
      (byte) 54,
      (byte) 114,
      (byte) 42,
      (byte) 90,
      (byte) 149,
      (byte) 112 /*0x70*/,
      (byte) 172,
      (byte) 67,
      (byte) 132,
      (byte) 198,
      (byte) 175,
      (byte) 44,
      (byte) 127 /*0x7F*/,
      (byte) 187,
      (byte) 190,
      (byte) 54,
      (byte) 38,
      (byte) 142,
      (byte) 143,
      (byte) 111,
      (byte) 138,
      (byte) 65,
      (byte) 126,
      (byte) 131,
      (byte) 180,
      (byte) 177,
      (byte) 182,
      (byte) 63 /*0x3F*/,
      (byte) 25
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[26];
    byte[] response2 = new byte[26];
    Array.Copy((Array) sc_12411.sspq, 79, (Array) numArray2, 0, 26);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12411.sspr, 79, (Array) numArray2, 0, 26);
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

  internal static string ssp_appserver_12417()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[105];
      byte[] numArray2 = new byte[55]
      {
        (byte) 138,
        (byte) 71,
        (byte) 96 /*0x60*/,
        (byte) 77,
        (byte) 223,
        (byte) 196,
        (byte) 152,
        (byte) 198,
        (byte) 175,
        (byte) 237,
        (byte) 58,
        (byte) 61,
        (byte) 169,
        (byte) 212,
        (byte) 213,
        (byte) 231,
        (byte) 128 /*0x80*/,
        (byte) 254,
        (byte) 160 /*0xA0*/,
        (byte) 204,
        (byte) 190,
        (byte) 51,
        (byte) 122,
        (byte) 188,
        (byte) 142,
        (byte) 146,
        (byte) 77,
        (byte) 90,
        (byte) 114,
        (byte) 51,
        (byte) 48 /*0x30*/,
        (byte) 61,
        (byte) 148,
        (byte) 53,
        (byte) 57,
        (byte) 37,
        (byte) 241,
        (byte) 83,
        (byte) 73,
        (byte) 162,
        (byte) 116,
        (byte) 22,
        (byte) 27,
        (byte) 35,
        (byte) 237,
        (byte) 83,
        (byte) 110,
        (byte) 224 /*0xE0*/,
        (byte) 112 /*0x70*/,
        (byte) 124,
        (byte) 119,
        (byte) 165,
        (byte) 182,
        (byte) 1,
        (byte) 103
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 165,
        (byte) 136,
        (byte) 48 /*0x30*/,
        (byte) 98,
        (byte) 246,
        (byte) 70,
        (byte) 232,
        (byte) 92,
        (byte) 240 /*0xF0*/,
        (byte) 52,
        (byte) 251,
        (byte) 14,
        (byte) 162,
        (byte) 40,
        (byte) 247,
        (byte) 58,
        (byte) 129,
        (byte) 66,
        (byte) 144 /*0x90*/,
        (byte) 4,
        (byte) 2,
        (byte) 6,
        (byte) 191,
        (byte) 58,
        (byte) 234,
        (byte) 19,
        (byte) 120,
        (byte) 210,
        (byte) 199,
        (byte) 98,
        (byte) 40,
        (byte) 26,
        (byte) 178,
        (byte) 211,
        (byte) 209,
        (byte) 58,
        (byte) 158,
        (byte) 220,
        (byte) 249,
        (byte) 12,
        (byte) 92,
        (byte) 38,
        (byte) 29,
        (byte) 19,
        (byte) 208 /*0xD0*/,
        (byte) 48 /*0x30*/,
        (byte) 84,
        (byte) 49,
        (byte) 16 /*0x10*/,
        (byte) 213,
        (byte) 206,
        (byte) 45,
        (byte) 251,
        (byte) 218,
        (byte) 156
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[50]
      {
        (byte) 163,
        (byte) 190,
        (byte) 76,
        (byte) 118,
        (byte) 62,
        (byte) 83,
        (byte) 124,
        (byte) 188,
        (byte) 252,
        (byte) 168,
        (byte) 115,
        (byte) 155,
        (byte) 175,
        (byte) 245,
        (byte) 45,
        (byte) 180,
        (byte) 253,
        (byte) 87,
        (byte) 168,
        (byte) 24,
        (byte) 87,
        (byte) 207,
        (byte) 59,
        (byte) 36,
        (byte) 109,
        (byte) 234,
        (byte) 246,
        (byte) 187,
        (byte) 139,
        (byte) 135,
        (byte) 147,
        (byte) 205,
        (byte) 226,
        (byte) 88,
        (byte) 78,
        (byte) 149,
        (byte) 253,
        (byte) 43,
        (byte) 54,
        (byte) 194,
        (byte) 159,
        (byte) 102,
        (byte) 240 /*0xF0*/,
        (byte) 170,
        (byte) 250,
        (byte) 69,
        (byte) 192 /*0xC0*/,
        (byte) 135,
        (byte) 74,
        (byte) 240 /*0xF0*/
      };
      byte[] numArray5 = new byte[50];
      numArray5[37] = (byte) 46;
      numArray5[28] = (byte) 123;
      numArray5[2] = (byte) 193;
      numArray5[12] = (byte) 134;
      numArray5[46] = (byte) 235;
      numArray5[14] = (byte) 14;
      numArray5[44] = (byte) 33;
      numArray5[7] = (byte) 254;
      numArray5[8] = (byte) 170;
      numArray5[17] = (byte) 52;
      numArray5[10] = (byte) 68;
      numArray5[13] = (byte) 45;
      numArray5[15] = (byte) 166;
      numArray5[24] = (byte) 85;
      numArray5[5] = (byte) 146;
      numArray5[27] = (byte) 246;
      numArray5[29] = (byte) 94;
      numArray5[19] = (byte) 71;
      numArray5[0] = (byte) 20;
      numArray5[30] = (byte) 200;
      numArray5[20] = (byte) 3;
      numArray5[35] = (byte) 199;
      numArray5[4] = (byte) 126;
      numArray5[36] = (byte) 108;
      numArray5[1] = (byte) 211;
      numArray5[25] = (byte) 85;
      numArray5[26] = (byte) 41;
      numArray5[23] = (byte) 127 /*0x7F*/;
      numArray5[16 /*0x10*/] = (byte) 221;
      numArray5[22] = (byte) 47;
      numArray5[31 /*0x1F*/] = (byte) 73;
      numArray5[9] = (byte) 246;
      numArray5[32 /*0x20*/] = (byte) 214;
      numArray5[33] = (byte) 93;
      numArray5[34] = (byte) 190;
      numArray5[21] = (byte) 42;
      numArray5[11] = (byte) 77;
      numArray5[41] = (byte) 116;
      numArray5[38] = (byte) 115;
      numArray5[39] = (byte) 66;
      numArray5[45] = (byte) 142;
      numArray5[3] = (byte) 184;
      numArray5[42] = (byte) 143;
      numArray5[43] = (byte) 112 /*0x70*/;
      numArray5[18] = (byte) 212;
      numArray5[40] = (byte) 252;
      numArray5[6] = (byte) 199;
      numArray5[47] = (byte) 138;
      numArray5[48 /*0x30*/] = (byte) 79;
      numArray5[49] = (byte) 128 /*0x80*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 50);
      for (int index = 0; index < 50; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[31 /*0x1F*/];
      byte[] response = new byte[31 /*0x1F*/];
      Array.Copy((Array) sc_12411.sspq, 105, (Array) numArray6, 0, 31 /*0x1F*/);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12411.sspr, 105, (Array) numArray6, 0, 31 /*0x1F*/);
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
    byte[] numArray8 = new byte[55];
    numArray8[47] = (byte) 131;
    numArray8[1] = (byte) 142;
    numArray8[2] = (byte) 189;
    numArray8[3] = (byte) 97;
    numArray8[53] = (byte) 25;
    numArray8[0] = (byte) 192 /*0xC0*/;
    numArray8[6] = (byte) 53;
    numArray8[7] = (byte) 222;
    numArray8[21] = (byte) 245;
    numArray8[9] = (byte) 131;
    numArray8[10] = (byte) 33;
    numArray8[12] = (byte) 64 /*0x40*/;
    numArray8[40] = (byte) 98;
    numArray8[23] = (byte) 47;
    numArray8[32 /*0x20*/] = (byte) 39;
    numArray8[48 /*0x30*/] = (byte) 8;
    numArray8[16 /*0x10*/] = (byte) 157;
    numArray8[17] = (byte) 211;
    numArray8[15] = (byte) 142;
    numArray8[35] = (byte) 95;
    numArray8[20] = (byte) 120;
    numArray8[30] = (byte) 144 /*0x90*/;
    numArray8[38] = (byte) 189;
    numArray8[13] = (byte) 186;
    numArray8[46] = (byte) 111;
    numArray8[25] = (byte) 54;
    numArray8[26] = (byte) 74;
    numArray8[44] = (byte) 98;
    numArray8[28] = (byte) 43;
    numArray8[39] = (byte) 202;
    numArray8[51] = (byte) 144 /*0x90*/;
    numArray8[31 /*0x1F*/] = (byte) 246;
    numArray8[5] = (byte) 96 /*0x60*/;
    numArray8[42] = (byte) 23;
    numArray8[34] = (byte) 104;
    numArray8[14] = (byte) 119;
    numArray8[18] = (byte) 106;
    numArray8[37] = (byte) 54;
    numArray8[29] = (byte) 202;
    numArray8[4] = (byte) 232;
    numArray8[33] = (byte) 70;
    numArray8[36] = (byte) 147;
    numArray8[43] = (byte) 170;
    numArray8[52] = (byte) 144 /*0x90*/;
    numArray8[24] = (byte) 86;
    numArray8[45] = (byte) 11;
    numArray8[54] = (byte) 34;
    numArray8[11] = (byte) 215;
    numArray8[8] = (byte) 92;
    numArray8[49] = (byte) 11;
    numArray8[50] = (byte) 29;
    numArray8[22] = (byte) 177;
    numArray8[41] = (byte) 53;
    numArray8[19] = (byte) 188;
    numArray8[27] = (byte) 89;
    byte[] numArray9 = new byte[55];
    numArray9[44] = (byte) 71;
    numArray9[1] = (byte) 88;
    numArray9[10] = (byte) 128 /*0x80*/;
    numArray9[52] = (byte) 50;
    numArray9[4] = (byte) 185;
    numArray9[0] = (byte) 242;
    numArray9[6] = (byte) 178;
    numArray9[7] = (byte) 204;
    numArray9[8] = (byte) 204;
    numArray9[27] = (byte) 95;
    numArray9[15] = (byte) 4;
    numArray9[25] = (byte) 96 /*0x60*/;
    numArray9[12] = (byte) 2;
    numArray9[23] = (byte) 67;
    numArray9[3] = (byte) 24;
    numArray9[30] = (byte) 237;
    numArray9[53] = (byte) 95;
    numArray9[40] = (byte) 106;
    numArray9[18] = (byte) 87;
    numArray9[19] = (byte) 147;
    numArray9[49] = (byte) 238;
    numArray9[13] = (byte) 168;
    numArray9[17] = (byte) 199;
    numArray9[20] = (byte) 83;
    numArray9[5] = (byte) 68;
    numArray9[51] = (byte) 195;
    numArray9[37] = (byte) 212;
    numArray9[11] = (byte) 169;
    numArray9[28] = (byte) 207;
    numArray9[29] = (byte) 206;
    numArray9[47] = (byte) 109;
    numArray9[14] = (byte) 80 /*0x50*/;
    numArray9[32 /*0x20*/] = (byte) 39;
    numArray9[33] = (byte) 162;
    numArray9[34] = (byte) 189;
    numArray9[35] = (byte) 220;
    numArray9[36] = (byte) 5;
    numArray9[2] = (byte) 49;
    numArray9[38] = (byte) 241;
    numArray9[39] = (byte) 70;
    numArray9[24] = (byte) 176 /*0xB0*/;
    numArray9[43] = (byte) 199;
    numArray9[42] = (byte) 194;
    numArray9[21] = (byte) 148;
    numArray9[50] = (byte) 63 /*0x3F*/;
    numArray9[45] = (byte) 213;
    numArray9[46] = (byte) 208 /*0xD0*/;
    numArray9[9] = (byte) 70;
    numArray9[48 /*0x30*/] = (byte) 17;
    numArray9[22] = (byte) 163;
    numArray9[31 /*0x1F*/] = (byte) 190;
    numArray9[16 /*0x10*/] = (byte) 86;
    numArray9[26] = (byte) 232;
    numArray9[41] = (byte) 150;
    numArray9[54] = (byte) 221;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[50]
    {
      (byte) 44,
      (byte) 207,
      (byte) 153,
      (byte) 187,
      (byte) 44,
      (byte) 129,
      (byte) 236,
      (byte) 108,
      (byte) 78,
      (byte) 82,
      (byte) 143,
      (byte) 2,
      (byte) 232,
      (byte) 178,
      (byte) 207,
      (byte) 234,
      (byte) 132,
      (byte) 217,
      (byte) 209,
      (byte) 182,
      (byte) 138,
      (byte) 33,
      (byte) 54,
      (byte) 177,
      (byte) 20,
      (byte) 3,
      (byte) 248,
      (byte) 125,
      (byte) 22,
      (byte) 110,
      (byte) 100,
      (byte) 41,
      (byte) 28,
      (byte) 38,
      (byte) 24,
      (byte) 157,
      (byte) 134,
      (byte) 170,
      (byte) 206,
      (byte) 110,
      (byte) 180,
      (byte) 88,
      (byte) 36,
      (byte) 102,
      (byte) 16 /*0x10*/,
      (byte) 125,
      (byte) 115,
      (byte) 103,
      (byte) 170,
      (byte) 132
    };
    byte[] numArray11 = new byte[50];
    numArray11[35] = (byte) 43;
    numArray11[7] = (byte) 88;
    numArray11[36] = (byte) 237;
    numArray11[46] = (byte) 136;
    numArray11[4] = (byte) 2;
    numArray11[5] = (byte) 142;
    numArray11[12] = (byte) 57;
    numArray11[31 /*0x1F*/] = (byte) 138;
    numArray11[18] = (byte) 183;
    numArray11[30] = (byte) 113;
    numArray11[10] = (byte) 56;
    numArray11[8] = (byte) 191;
    numArray11[3] = (byte) 40;
    numArray11[28] = (byte) 115;
    numArray11[14] = (byte) 88;
    numArray11[48 /*0x30*/] = (byte) 173;
    numArray11[43] = (byte) 145;
    numArray11[17] = (byte) 160 /*0xA0*/;
    numArray11[16 /*0x10*/] = (byte) 92;
    numArray11[26] = (byte) 21;
    numArray11[1] = (byte) 241;
    numArray11[21] = (byte) 32 /*0x20*/;
    numArray11[22] = (byte) 40;
    numArray11[23] = (byte) 110;
    numArray11[24] = (byte) 61;
    numArray11[25] = (byte) 187;
    numArray11[0] = (byte) 127 /*0x7F*/;
    numArray11[9] = (byte) 202;
    numArray11[6] = (byte) 179;
    numArray11[15] = (byte) 75;
    numArray11[19] = (byte) 146;
    numArray11[27] = (byte) 88;
    numArray11[40] = (byte) 55;
    numArray11[33] = (byte) 239;
    numArray11[34] = (byte) 32 /*0x20*/;
    numArray11[20] = (byte) 77;
    numArray11[45] = (byte) 203;
    numArray11[37] = (byte) 45;
    numArray11[2] = (byte) 35;
    numArray11[39] = (byte) 253;
    numArray11[11] = (byte) 63 /*0x3F*/;
    numArray11[41] = (byte) 68;
    numArray11[42] = (byte) 146;
    numArray11[29] = (byte) 215;
    numArray11[44] = (byte) 90;
    numArray11[13] = (byte) 9;
    numArray11[38] = (byte) 141;
    numArray11[47] = (byte) 47;
    numArray11[32 /*0x20*/] = (byte) 115;
    numArray11[49] = (byte) 191;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 50);
    for (int index = 0; index < 50; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static int ssp_appserver_12418(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 167,
      (byte) 6,
      (byte) 232,
      (byte) 220,
      (byte) 200,
      (byte) 14,
      (byte) 201,
      (byte) 70,
      (byte) 15,
      (byte) 152,
      (byte) 247,
      (byte) 157,
      (byte) 196,
      (byte) 243,
      (byte) 154,
      (byte) 135,
      (byte) 168,
      (byte) 149,
      (byte) 140,
      (byte) 46,
      (byte) 197,
      (byte) 214,
      (byte) 100,
      (byte) 124,
      (byte) 189,
      (byte) 47,
      (byte) 183,
      (byte) 86,
      (byte) 245,
      byte.MaxValue,
      (byte) 222,
      (byte) 195,
      (byte) 89,
      (byte) 113,
      (byte) 68,
      (byte) 77,
      (byte) 246,
      (byte) 97,
      (byte) 99,
      (byte) 254,
      (byte) 58,
      (byte) 88,
      (byte) 243,
      (byte) 13,
      (byte) 238,
      (byte) 167,
      (byte) 61,
      (byte) 80 /*0x50*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[14] = (byte) 219;
    sourceArray2[0] = (byte) 104;
    sourceArray2[30] = (byte) 44;
    sourceArray2[47] = (byte) 160 /*0xA0*/;
    sourceArray2[4] = (byte) 17;
    sourceArray2[5] = (byte) 82;
    sourceArray2[6] = (byte) 3;
    sourceArray2[18] = (byte) 120;
    sourceArray2[19] = (byte) 196;
    sourceArray2[42] = (byte) 205;
    sourceArray2[10] = (byte) 241;
    sourceArray2[11] = (byte) 254;
    sourceArray2[12] = (byte) 113;
    sourceArray2[13] = (byte) 66;
    sourceArray2[20] = (byte) 241;
    sourceArray2[15] = (byte) 90;
    sourceArray2[44] = (byte) 119;
    sourceArray2[16 /*0x10*/] = (byte) 229;
    sourceArray2[9] = (byte) 188;
    sourceArray2[3] = (byte) 91;
    sourceArray2[17] = (byte) 197;
    sourceArray2[46] = (byte) 244;
    sourceArray2[37] = (byte) 63 /*0x3F*/;
    sourceArray2[23] = (byte) 21;
    sourceArray2[1] = (byte) 253;
    sourceArray2[25] = (byte) 117;
    sourceArray2[26] = (byte) 214;
    sourceArray2[22] = (byte) 141;
    sourceArray2[28] = (byte) 97;
    sourceArray2[29] = (byte) 202;
    sourceArray2[31 /*0x1F*/] = (byte) 133;
    sourceArray2[35] = (byte) 171;
    sourceArray2[8] = (byte) 155;
    sourceArray2[45] = (byte) 78;
    sourceArray2[33] = (byte) 51;
    sourceArray2[32 /*0x20*/] = (byte) 1;
    sourceArray2[36] = (byte) 235;
    sourceArray2[38] = (byte) 233;
    sourceArray2[34] = (byte) 204;
    sourceArray2[24] = (byte) 137;
    sourceArray2[40] = (byte) 47;
    sourceArray2[41] = (byte) 248;
    sourceArray2[7] = (byte) 17;
    sourceArray2[43] = (byte) 150;
    sourceArray2[21] = (byte) 108;
    sourceArray2[27] = (byte) 7;
    sourceArray2[39] = (byte) 180;
    sourceArray2[2] = (byte) 77;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[12];
    byte[] response2 = new byte[12];
    Array.Copy((Array) sc_12411.sspq, 136, (Array) numArray2, 0, 12);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12411.sspr, 136, (Array) numArray2, 0, 12);
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

  internal static int ssp_appserver_12419(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 166,
      (byte) 240 /*0xF0*/,
      (byte) 249,
      (byte) 126,
      (byte) 254,
      (byte) 248,
      (byte) 176 /*0xB0*/,
      (byte) 51,
      (byte) 60,
      (byte) 59,
      (byte) 161,
      (byte) 6,
      (byte) 4,
      (byte) 104,
      (byte) 231,
      (byte) 43,
      (byte) 102,
      (byte) 254,
      (byte) 3,
      (byte) 212,
      (byte) 178,
      (byte) 7,
      (byte) 197,
      (byte) 231,
      (byte) 63 /*0x3F*/,
      (byte) 105,
      (byte) 216,
      (byte) 209,
      (byte) 185,
      (byte) 41,
      (byte) 133,
      (byte) 96 /*0x60*/,
      (byte) 173,
      (byte) 57,
      (byte) 35,
      (byte) 182,
      (byte) 206,
      (byte) 136,
      (byte) 229,
      (byte) 90,
      (byte) 9,
      (byte) 48 /*0x30*/,
      (byte) 174,
      (byte) 101,
      (byte) 239,
      (byte) 139,
      (byte) 163,
      (byte) 128 /*0x80*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 71,
      (byte) 0,
      (byte) 224 /*0xE0*/,
      (byte) 215,
      (byte) 244,
      (byte) 134,
      (byte) 109,
      (byte) 108,
      (byte) 35,
      (byte) 212,
      (byte) 172,
      (byte) 105,
      (byte) 22,
      (byte) 19,
      (byte) 225,
      (byte) 75,
      (byte) 34,
      (byte) 88,
      (byte) 128 /*0x80*/,
      (byte) 98,
      (byte) 199,
      (byte) 165,
      (byte) 68,
      (byte) 77,
      (byte) 188,
      (byte) 99,
      (byte) 14,
      (byte) 61,
      (byte) 20,
      (byte) 195,
      (byte) 129,
      (byte) 157,
      (byte) 212,
      (byte) 93,
      (byte) 115,
      (byte) 241,
      (byte) 95,
      (byte) 12,
      (byte) 43,
      (byte) 121,
      (byte) 8,
      (byte) 23,
      (byte) 25,
      (byte) 177,
      (byte) 74,
      (byte) 76,
      (byte) 183,
      (byte) 130
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12420()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[103];
      byte[] numArray2 = new byte[55];
      numArray2[32 /*0x20*/] = (byte) 162;
      numArray2[0] = (byte) 71;
      numArray2[1] = (byte) 7;
      numArray2[3] = (byte) 159;
      numArray2[4] = (byte) 42;
      numArray2[37] = (byte) 131;
      numArray2[33] = (byte) 149;
      numArray2[20] = (byte) 24;
      numArray2[42] = (byte) 58;
      numArray2[28] = (byte) 197;
      numArray2[45] = (byte) 156;
      numArray2[9] = (byte) 222;
      numArray2[23] = (byte) 129;
      numArray2[13] = (byte) 65;
      numArray2[14] = (byte) 89;
      numArray2[47] = (byte) 173;
      numArray2[24] = (byte) 88;
      numArray2[17] = (byte) 49;
      numArray2[10] = (byte) 122;
      numArray2[19] = (byte) 221;
      numArray2[49] = (byte) 216;
      numArray2[25] = (byte) 96 /*0x60*/;
      numArray2[22] = (byte) 203;
      numArray2[27] = (byte) 179;
      numArray2[18] = (byte) 93;
      numArray2[8] = (byte) 31 /*0x1F*/;
      numArray2[26] = (byte) 248;
      numArray2[52] = (byte) 204;
      numArray2[15] = (byte) 203;
      numArray2[29] = (byte) 87;
      numArray2[30] = (byte) 7;
      numArray2[31 /*0x1F*/] = (byte) 142;
      numArray2[2] = (byte) 86;
      numArray2[39] = (byte) 187;
      numArray2[34] = (byte) 24;
      numArray2[35] = (byte) 165;
      numArray2[36] = (byte) 226;
      numArray2[41] = (byte) 80 /*0x50*/;
      numArray2[38] = (byte) 139;
      numArray2[48 /*0x30*/] = (byte) 50;
      numArray2[40] = (byte) 73;
      numArray2[11] = (byte) 18;
      numArray2[6] = (byte) 63 /*0x3F*/;
      numArray2[43] = (byte) 42;
      numArray2[44] = (byte) 181;
      numArray2[21] = (byte) 169;
      numArray2[5] = (byte) 18;
      numArray2[53] = (byte) 169;
      numArray2[54] = (byte) 214;
      numArray2[16 /*0x10*/] = (byte) 69;
      numArray2[50] = (byte) 59;
      numArray2[51] = (byte) 153;
      numArray2[12] = (byte) 182;
      numArray2[46] = (byte) 175;
      numArray2[7] = (byte) 149;
      byte[] numArray3 = new byte[55]
      {
        (byte) 62,
        (byte) 139,
        (byte) 120,
        (byte) 79,
        (byte) 229,
        (byte) 142,
        byte.MaxValue,
        (byte) 68,
        (byte) 172,
        (byte) 214,
        (byte) 40,
        (byte) 229,
        (byte) 224 /*0xE0*/,
        (byte) 95,
        (byte) 14,
        (byte) 224 /*0xE0*/,
        (byte) 167,
        (byte) 143,
        (byte) 195,
        (byte) 128 /*0x80*/,
        (byte) 196,
        (byte) 132,
        (byte) 41,
        (byte) 151,
        (byte) 64 /*0x40*/,
        (byte) 10,
        (byte) 82,
        (byte) 118,
        (byte) 126,
        (byte) 233,
        (byte) 46,
        (byte) 9,
        (byte) 106,
        (byte) 47,
        (byte) 99,
        (byte) 124,
        (byte) 245,
        (byte) 64 /*0x40*/,
        (byte) 237,
        (byte) 84,
        (byte) 28,
        (byte) 83,
        (byte) 56,
        (byte) 39,
        (byte) 154,
        (byte) 227,
        (byte) 162,
        (byte) 91,
        (byte) 163,
        (byte) 98,
        (byte) 149,
        (byte) 37,
        (byte) 254,
        (byte) 28,
        (byte) 195
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/]
      {
        (byte) 8,
        (byte) 139,
        (byte) 25,
        (byte) 207,
        (byte) 11,
        (byte) 226,
        (byte) 171,
        (byte) 105,
        (byte) 41,
        (byte) 119,
        (byte) 98,
        (byte) 173,
        (byte) 74,
        (byte) 219,
        (byte) 133,
        (byte) 227,
        byte.MaxValue,
        (byte) 215,
        (byte) 72,
        (byte) 253,
        (byte) 77,
        (byte) 2,
        (byte) 143,
        (byte) 34,
        (byte) 197,
        (byte) 0,
        (byte) 254,
        (byte) 162,
        (byte) 122,
        (byte) 150,
        (byte) 227,
        (byte) 176 /*0xB0*/,
        (byte) 76,
        (byte) 204,
        (byte) 4,
        (byte) 176 /*0xB0*/,
        (byte) 168,
        (byte) 210,
        (byte) 177,
        (byte) 158,
        (byte) 198,
        (byte) 234,
        (byte) 160 /*0xA0*/,
        (byte) 180,
        (byte) 43,
        (byte) 215,
        (byte) 52,
        (byte) 19
      };
      byte[] numArray5 = new byte[48 /*0x30*/];
      numArray5[20] = (byte) 35;
      numArray5[18] = (byte) 94;
      numArray5[2] = (byte) 254;
      numArray5[40] = (byte) 51;
      numArray5[16 /*0x10*/] = (byte) 246;
      numArray5[47] = (byte) 219;
      numArray5[0] = (byte) 43;
      numArray5[4] = (byte) 89;
      numArray5[8] = (byte) 162;
      numArray5[39] = (byte) 0;
      numArray5[10] = (byte) 135;
      numArray5[11] = (byte) 233;
      numArray5[12] = (byte) 213;
      numArray5[25] = (byte) 89;
      numArray5[14] = (byte) 251;
      numArray5[15] = (byte) 82;
      numArray5[33] = (byte) 127 /*0x7F*/;
      numArray5[17] = (byte) 213;
      numArray5[6] = (byte) 237;
      numArray5[27] = (byte) 244;
      numArray5[3] = (byte) 104;
      numArray5[38] = (byte) 150;
      numArray5[1] = (byte) 85;
      numArray5[23] = (byte) 170;
      numArray5[24] = (byte) 238;
      numArray5[21] = (byte) 15;
      numArray5[26] = (byte) 58;
      numArray5[13] = (byte) 199;
      numArray5[28] = (byte) 235;
      numArray5[29] = (byte) 148;
      numArray5[30] = (byte) 159;
      numArray5[31 /*0x1F*/] = (byte) 47;
      numArray5[42] = (byte) 194;
      numArray5[5] = (byte) 205;
      numArray5[34] = (byte) 34;
      numArray5[9] = (byte) 70;
      numArray5[7] = (byte) 154;
      numArray5[44] = (byte) 74;
      numArray5[22] = (byte) 28;
      numArray5[32 /*0x20*/] = (byte) 207;
      numArray5[35] = (byte) 26;
      numArray5[41] = (byte) 138;
      numArray5[19] = (byte) 105;
      numArray5[43] = (byte) 42;
      numArray5[37] = (byte) 131;
      numArray5[45] = (byte) 6;
      numArray5[36] = (byte) 77;
      numArray5[46] = (byte) 81;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[103];
    byte[] numArray7 = new byte[55]
    {
      (byte) 121,
      (byte) 117,
      (byte) 182,
      (byte) 60,
      (byte) 31 /*0x1F*/,
      (byte) 213,
      (byte) 254,
      (byte) 23,
      (byte) 28,
      (byte) 250,
      (byte) 236,
      (byte) 165,
      (byte) 32 /*0x20*/,
      byte.MaxValue,
      (byte) 27,
      (byte) 62,
      (byte) 249,
      (byte) 92,
      (byte) 133,
      (byte) 39,
      (byte) 238,
      (byte) 200,
      (byte) 70,
      (byte) 1,
      (byte) 217,
      (byte) 31 /*0x1F*/,
      (byte) 136,
      (byte) 100,
      (byte) 127 /*0x7F*/,
      (byte) 239,
      (byte) 122,
      (byte) 103,
      (byte) 84,
      (byte) 186,
      (byte) 125,
      (byte) 120,
      (byte) 21,
      (byte) 173,
      (byte) 141,
      (byte) 147,
      (byte) 109,
      (byte) 141,
      (byte) 205,
      (byte) 32 /*0x20*/,
      (byte) 144 /*0x90*/,
      (byte) 88,
      (byte) 148,
      (byte) 179,
      (byte) 173,
      (byte) 103,
      (byte) 192 /*0xC0*/,
      (byte) 96 /*0x60*/,
      (byte) 117,
      (byte) 229,
      (byte) 49
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 92,
      (byte) 2,
      (byte) 152,
      (byte) 190,
      (byte) 48 /*0x30*/,
      (byte) 249,
      (byte) 144 /*0x90*/,
      (byte) 184,
      (byte) 155,
      (byte) 229,
      (byte) 34,
      (byte) 30,
      (byte) 179,
      (byte) 9,
      (byte) 219,
      (byte) 10,
      (byte) 42,
      (byte) 88,
      (byte) 160 /*0xA0*/,
      (byte) 155,
      (byte) 33,
      (byte) 1,
      (byte) 108,
      (byte) 112 /*0x70*/,
      (byte) 133,
      (byte) 14,
      (byte) 127 /*0x7F*/,
      (byte) 226,
      (byte) 89,
      (byte) 247,
      (byte) 64 /*0x40*/,
      (byte) 139,
      (byte) 79,
      (byte) 223,
      (byte) 135,
      (byte) 203,
      (byte) 30,
      (byte) 125,
      (byte) 224 /*0xE0*/,
      (byte) 84,
      (byte) 242,
      (byte) 6,
      (byte) 63 /*0x3F*/,
      (byte) 77,
      (byte) 143,
      (byte) 245,
      (byte) 12,
      (byte) 78,
      (byte) 100,
      (byte) 241,
      (byte) 152,
      (byte) 214,
      (byte) 27,
      (byte) 43,
      (byte) 29
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[48 /*0x30*/]
    {
      (byte) 181,
      (byte) 98,
      (byte) 0,
      (byte) 77,
      (byte) 153,
      (byte) 152,
      (byte) 45,
      (byte) 128 /*0x80*/,
      (byte) 132,
      (byte) 80 /*0x50*/,
      (byte) 102,
      (byte) 130,
      (byte) 162,
      (byte) 41,
      (byte) 76,
      (byte) 206,
      (byte) 241,
      (byte) 186,
      (byte) 97,
      (byte) 251,
      (byte) 38,
      (byte) 112 /*0x70*/,
      (byte) 129,
      (byte) 139,
      (byte) 213,
      (byte) 182,
      (byte) 24,
      (byte) 47,
      (byte) 26,
      (byte) 242,
      (byte) 110,
      (byte) 136,
      (byte) 203,
      (byte) 73,
      (byte) 95,
      (byte) 74,
      (byte) 141,
      (byte) 70,
      (byte) 211,
      (byte) 245,
      (byte) 159,
      (byte) 159,
      (byte) 11,
      (byte) 7,
      (byte) 45,
      (byte) 49,
      (byte) 10,
      (byte) 61
    };
    byte[] numArray10 = new byte[48 /*0x30*/];
    numArray10[29] = (byte) 34;
    numArray10[32 /*0x20*/] = (byte) 29;
    numArray10[2] = (byte) 29;
    numArray10[3] = (byte) 170;
    numArray10[11] = (byte) 247;
    numArray10[46] = (byte) 231;
    numArray10[36] = (byte) 113;
    numArray10[7] = (byte) 113;
    numArray10[34] = (byte) 54;
    numArray10[20] = (byte) 109;
    numArray10[6] = (byte) 134;
    numArray10[33] = (byte) 44;
    numArray10[12] = (byte) 39;
    numArray10[13] = (byte) 169;
    numArray10[14] = (byte) 132;
    numArray10[15] = (byte) 0;
    numArray10[16 /*0x10*/] = (byte) 244;
    numArray10[9] = (byte) 147;
    numArray10[5] = (byte) 58;
    numArray10[10] = (byte) 188;
    numArray10[44] = (byte) 247;
    numArray10[23] = (byte) 27;
    numArray10[22] = (byte) 84;
    numArray10[18] = (byte) 152;
    numArray10[24] = (byte) 48 /*0x30*/;
    numArray10[4] = (byte) 47;
    numArray10[17] = (byte) 27;
    numArray10[27] = (byte) 94;
    numArray10[28] = (byte) 65;
    numArray10[43] = (byte) 5;
    numArray10[30] = (byte) 70;
    numArray10[47] = (byte) 220;
    numArray10[0] = (byte) 28;
    numArray10[31 /*0x1F*/] = (byte) 204;
    numArray10[26] = (byte) 216;
    numArray10[35] = (byte) 157;
    numArray10[40] = (byte) 226;
    numArray10[37] = (byte) 242;
    numArray10[38] = (byte) 249;
    numArray10[39] = (byte) 81;
    numArray10[1] = (byte) 173;
    numArray10[8] = (byte) 120;
    numArray10[42] = (byte) 16 /*0x10*/;
    numArray10[41] = (byte) 22;
    numArray10[25] = (byte) 165;
    numArray10[45] = (byte) 249;
    numArray10[19] = (byte) 179;
    numArray10[21] = (byte) 147;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_12411.sspq, 148, (Array) numArray11, 0, 52);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12411.sspr, 148, (Array) numArray11, 0, 52);
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

  internal static string ssp_appserver_12421()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[101];
      byte[] numArray2 = new byte[55];
      numArray2[48 /*0x30*/] = (byte) 248;
      numArray2[14] = (byte) 9;
      numArray2[2] = (byte) 217;
      numArray2[3] = (byte) 154;
      numArray2[4] = (byte) 195;
      numArray2[46] = (byte) 68;
      numArray2[16 /*0x10*/] = (byte) 60;
      numArray2[7] = (byte) 250;
      numArray2[8] = (byte) 77;
      numArray2[9] = (byte) 107;
      numArray2[10] = (byte) 161;
      numArray2[11] = (byte) 51;
      numArray2[22] = (byte) 208 /*0xD0*/;
      numArray2[24] = (byte) 87;
      numArray2[6] = (byte) 40;
      numArray2[13] = (byte) 19;
      numArray2[39] = (byte) 110;
      numArray2[17] = (byte) 62;
      numArray2[18] = (byte) 249;
      numArray2[40] = (byte) 15;
      numArray2[1] = (byte) 42;
      numArray2[42] = (byte) 177;
      numArray2[47] = (byte) 237;
      numArray2[23] = (byte) 230;
      numArray2[27] = (byte) 38;
      numArray2[0] = (byte) 135;
      numArray2[26] = (byte) 150;
      numArray2[34] = (byte) 204;
      numArray2[52] = (byte) 101;
      numArray2[15] = (byte) 171;
      numArray2[33] = (byte) 148;
      numArray2[31 /*0x1F*/] = (byte) 89;
      numArray2[30] = (byte) 39;
      numArray2[43] = (byte) 233;
      numArray2[12] = (byte) 245;
      numArray2[29] = (byte) 61;
      numArray2[28] = (byte) 228;
      numArray2[38] = (byte) 163;
      numArray2[44] = (byte) 25;
      numArray2[5] = (byte) 218;
      numArray2[21] = (byte) 159;
      numArray2[35] = (byte) 227;
      numArray2[37] = (byte) 96 /*0x60*/;
      numArray2[41] = (byte) 155;
      numArray2[25] = (byte) 118;
      numArray2[45] = (byte) 140;
      numArray2[20] = (byte) 140;
      numArray2[19] = (byte) 165;
      numArray2[32 /*0x20*/] = (byte) 241;
      numArray2[49] = (byte) 77;
      numArray2[50] = (byte) 124;
      numArray2[51] = (byte) 145;
      numArray2[36] = (byte) 78;
      numArray2[53] = (byte) 182;
      numArray2[54] = (byte) 201;
      byte[] numArray3 = new byte[55];
      numArray3[34] = (byte) 203;
      numArray3[19] = (byte) 14;
      numArray3[2] = (byte) 142;
      numArray3[49] = (byte) 62;
      numArray3[4] = (byte) 122;
      numArray3[5] = (byte) 169;
      numArray3[48 /*0x30*/] = (byte) 20;
      numArray3[7] = (byte) 235;
      numArray3[45] = (byte) 25;
      numArray3[9] = (byte) 141;
      numArray3[53] = (byte) 250;
      numArray3[10] = (byte) 56;
      numArray3[43] = (byte) 124;
      numArray3[51] = (byte) 173;
      numArray3[13] = (byte) 127 /*0x7F*/;
      numArray3[33] = (byte) 74;
      numArray3[42] = (byte) 206;
      numArray3[21] = (byte) 42;
      numArray3[12] = (byte) 164;
      numArray3[16 /*0x10*/] = (byte) 159;
      numArray3[20] = (byte) 84;
      numArray3[1] = (byte) 57;
      numArray3[6] = (byte) 243;
      numArray3[25] = (byte) 248;
      numArray3[24] = (byte) 12;
      numArray3[29] = (byte) 22;
      numArray3[26] = (byte) 38;
      numArray3[11] = (byte) 83;
      numArray3[27] = (byte) 67;
      numArray3[18] = (byte) 116;
      numArray3[30] = (byte) 53;
      numArray3[31 /*0x1F*/] = (byte) 144 /*0x90*/;
      numArray3[32 /*0x20*/] = (byte) 49;
      numArray3[8] = (byte) 166;
      numArray3[3] = (byte) 9;
      numArray3[35] = (byte) 204;
      numArray3[36] = (byte) 202;
      numArray3[37] = (byte) 122;
      numArray3[17] = (byte) 214;
      numArray3[39] = (byte) 220;
      numArray3[40] = (byte) 137;
      numArray3[28] = (byte) 168;
      numArray3[0] = (byte) 199;
      numArray3[46] = (byte) 61;
      numArray3[44] = (byte) 227;
      numArray3[22] = (byte) 101;
      numArray3[41] = (byte) 61;
      numArray3[47] = (byte) 122;
      numArray3[14] = (byte) 104;
      numArray3[15] = (byte) 154;
      numArray3[23] = (byte) 26;
      numArray3[38] = (byte) 242;
      numArray3[52] = (byte) 29;
      numArray3[50] = (byte) 35;
      numArray3[54] = (byte) 228;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[46]
      {
        (byte) 44,
        (byte) 245,
        (byte) 204,
        (byte) 151,
        (byte) 120,
        (byte) 216,
        (byte) 47,
        (byte) 12,
        (byte) 58,
        (byte) 235,
        (byte) 162,
        (byte) 120,
        (byte) 137,
        (byte) 205,
        (byte) 24,
        (byte) 11,
        (byte) 208 /*0xD0*/,
        (byte) 82,
        (byte) 100,
        (byte) 249,
        (byte) 73,
        (byte) 125,
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 191,
        (byte) 40,
        (byte) 183,
        (byte) 62,
        (byte) 241,
        (byte) 160 /*0xA0*/,
        (byte) 134,
        (byte) 134,
        (byte) 60,
        (byte) 226,
        (byte) 7,
        (byte) 176 /*0xB0*/,
        (byte) 110,
        (byte) 249,
        (byte) 191,
        (byte) 45,
        (byte) 35,
        (byte) 235,
        (byte) 67,
        (byte) 87,
        (byte) 171,
        (byte) 203
      };
      byte[] numArray5 = new byte[46];
      numArray5[17] = (byte) 243;
      numArray5[1] = (byte) 216;
      numArray5[11] = (byte) 74;
      numArray5[9] = (byte) 227;
      numArray5[18] = (byte) 155;
      numArray5[28] = (byte) 65;
      numArray5[6] = (byte) 72;
      numArray5[7] = (byte) 89;
      numArray5[12] = (byte) 224 /*0xE0*/;
      numArray5[35] = (byte) 180;
      numArray5[10] = (byte) 123;
      numArray5[13] = (byte) 33;
      numArray5[21] = (byte) 54;
      numArray5[2] = (byte) 236;
      numArray5[14] = (byte) 61;
      numArray5[23] = (byte) 158;
      numArray5[19] = (byte) 60;
      numArray5[5] = (byte) 138;
      numArray5[3] = (byte) 139;
      numArray5[8] = (byte) 15;
      numArray5[20] = (byte) 22;
      numArray5[22] = (byte) 226;
      numArray5[43] = (byte) 109;
      numArray5[38] = (byte) 29;
      numArray5[24] = (byte) 234;
      numArray5[25] = (byte) 169;
      numArray5[15] = (byte) 104;
      numArray5[27] = (byte) 120;
      numArray5[26] = (byte) 44;
      numArray5[29] = (byte) 239;
      numArray5[40] = (byte) 210;
      numArray5[31 /*0x1F*/] = (byte) 9;
      numArray5[32 /*0x20*/] = (byte) 206;
      numArray5[33] = (byte) 70;
      numArray5[0] = (byte) 252;
      numArray5[4] = (byte) 181;
      numArray5[42] = (byte) 134;
      numArray5[37] = (byte) 245;
      numArray5[44] = (byte) 33;
      numArray5[39] = (byte) 144 /*0x90*/;
      numArray5[34] = (byte) 0;
      numArray5[41] = (byte) 128 /*0x80*/;
      numArray5[30] = (byte) 74;
      numArray5[36] = (byte) 104;
      numArray5[16 /*0x10*/] = (byte) 88;
      numArray5[45] = (byte) 67;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[101];
    byte[] numArray7 = new byte[55]
    {
      (byte) 49,
      (byte) 207,
      (byte) 169,
      (byte) 241,
      (byte) 143,
      (byte) 143,
      (byte) 2,
      (byte) 90,
      (byte) 97,
      (byte) 7,
      (byte) 210,
      (byte) 175,
      (byte) 208 /*0xD0*/,
      (byte) 221,
      (byte) 139,
      (byte) 117,
      (byte) 206,
      (byte) 118,
      (byte) 226,
      (byte) 208 /*0xD0*/,
      (byte) 138,
      (byte) 124,
      (byte) 206,
      (byte) 163,
      (byte) 33,
      (byte) 157,
      (byte) 179,
      (byte) 174,
      (byte) 109,
      (byte) 109,
      (byte) 250,
      (byte) 180,
      (byte) 34,
      (byte) 43,
      (byte) 164,
      (byte) 142,
      (byte) 143,
      (byte) 246,
      (byte) 197,
      (byte) 101,
      (byte) 123,
      (byte) 59,
      (byte) 19,
      (byte) 69,
      (byte) 36,
      (byte) 147,
      (byte) 173,
      (byte) 169,
      (byte) 109,
      (byte) 226,
      (byte) 44,
      (byte) 44,
      (byte) 63 /*0x3F*/,
      (byte) 159,
      (byte) 129
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 122,
      (byte) 173,
      (byte) 203,
      (byte) 250,
      (byte) 177,
      (byte) 232,
      (byte) 227,
      (byte) 5,
      (byte) 194,
      (byte) 157,
      (byte) 208 /*0xD0*/,
      (byte) 8,
      (byte) 200,
      (byte) 144 /*0x90*/,
      (byte) 217,
      byte.MaxValue,
      (byte) 186,
      (byte) 149,
      (byte) 93,
      (byte) 230,
      (byte) 217,
      (byte) 71,
      (byte) 232,
      (byte) 30,
      (byte) 8,
      (byte) 106,
      (byte) 213,
      (byte) 77,
      (byte) 144 /*0x90*/,
      (byte) 93,
      (byte) 13,
      (byte) 240 /*0xF0*/,
      (byte) 82,
      (byte) 187,
      (byte) 64 /*0x40*/,
      (byte) 184,
      (byte) 3,
      (byte) 117,
      (byte) 204,
      (byte) 200,
      (byte) 194,
      (byte) 9,
      (byte) 68,
      (byte) 89,
      (byte) 5,
      (byte) 39,
      (byte) 2,
      (byte) 30,
      (byte) 225,
      (byte) 109,
      (byte) 85,
      (byte) 171,
      (byte) 165,
      (byte) 20,
      (byte) 140
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[46];
    numArray9[31 /*0x1F*/] = (byte) 169;
    numArray9[25] = (byte) 64 /*0x40*/;
    numArray9[2] = (byte) 242;
    numArray9[3] = (byte) 219;
    numArray9[4] = (byte) 84;
    numArray9[9] = (byte) 141;
    numArray9[23] = (byte) 239;
    numArray9[7] = (byte) 12;
    numArray9[6] = (byte) 244;
    numArray9[33] = (byte) 141;
    numArray9[28] = (byte) 206;
    numArray9[20] = (byte) 137;
    numArray9[12] = (byte) 252;
    numArray9[13] = (byte) 229;
    numArray9[1] = (byte) 208 /*0xD0*/;
    numArray9[5] = (byte) 236;
    numArray9[21] = (byte) 126;
    numArray9[14] = (byte) 216;
    numArray9[17] = (byte) 84;
    numArray9[41] = (byte) 56;
    numArray9[36] = (byte) 211;
    numArray9[0] = (byte) 6;
    numArray9[32 /*0x20*/] = (byte) 239;
    numArray9[16 /*0x10*/] = (byte) 125;
    numArray9[24] = (byte) 9;
    numArray9[11] = (byte) 57;
    numArray9[19] = (byte) 104;
    numArray9[27] = (byte) 64 /*0x40*/;
    numArray9[39] = (byte) 79;
    numArray9[29] = (byte) 117;
    numArray9[18] = (byte) 78;
    numArray9[45] = (byte) 112 /*0x70*/;
    numArray9[38] = (byte) 39;
    numArray9[22] = (byte) 142;
    numArray9[34] = (byte) 137;
    numArray9[35] = (byte) 37;
    numArray9[10] = (byte) 165;
    numArray9[37] = (byte) 138;
    numArray9[15] = (byte) 176 /*0xB0*/;
    numArray9[26] = (byte) 139;
    numArray9[40] = (byte) 31 /*0x1F*/;
    numArray9[30] = (byte) 130;
    numArray9[42] = (byte) 6;
    numArray9[43] = (byte) 6;
    numArray9[44] = (byte) 8;
    numArray9[8] = (byte) 29;
    byte[] numArray10 = new byte[46]
    {
      (byte) 24,
      (byte) 182,
      (byte) 241,
      (byte) 54,
      (byte) 231,
      (byte) 43,
      (byte) 21,
      (byte) 222,
      (byte) 174,
      (byte) 81,
      (byte) 148,
      (byte) 211,
      (byte) 98,
      (byte) 72,
      (byte) 3,
      (byte) 40,
      (byte) 254,
      (byte) 84,
      (byte) 88,
      (byte) 85,
      (byte) 111,
      (byte) 79,
      (byte) 67,
      (byte) 223,
      (byte) 248,
      (byte) 65,
      (byte) 4,
      (byte) 21,
      (byte) 248,
      (byte) 52,
      (byte) 36,
      (byte) 202,
      (byte) 218,
      (byte) 226,
      (byte) 31 /*0x1F*/,
      (byte) 125,
      (byte) 3,
      (byte) 158,
      (byte) 110,
      (byte) 132,
      (byte) 150,
      (byte) 212,
      (byte) 192 /*0xC0*/,
      (byte) 234,
      (byte) 138,
      (byte) 29
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 46);
    for (int index = 0; index < 46; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12422()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[104];
      byte[] numArray2 = new byte[55];
      numArray2[1] = (byte) 94;
      numArray2[51] = (byte) 128 /*0x80*/;
      numArray2[15] = (byte) 186;
      numArray2[12] = (byte) 2;
      numArray2[52] = (byte) 207;
      numArray2[5] = (byte) 0;
      numArray2[6] = (byte) 160 /*0xA0*/;
      numArray2[27] = (byte) 90;
      numArray2[8] = (byte) 158;
      numArray2[49] = (byte) 41;
      numArray2[43] = (byte) 226;
      numArray2[31 /*0x1F*/] = (byte) 26;
      numArray2[10] = (byte) 29;
      numArray2[39] = (byte) 207;
      numArray2[14] = (byte) 15;
      numArray2[36] = (byte) 140;
      numArray2[3] = (byte) 12;
      numArray2[4] = (byte) 38;
      numArray2[0] = (byte) 250;
      numArray2[9] = (byte) 207;
      numArray2[20] = (byte) 198;
      numArray2[21] = (byte) 236;
      numArray2[2] = (byte) 251;
      numArray2[30] = (byte) 58;
      numArray2[24] = (byte) 134;
      numArray2[22] = (byte) 245;
      numArray2[17] = (byte) 56;
      numArray2[42] = (byte) 179;
      numArray2[28] = (byte) 206;
      numArray2[29] = (byte) 12;
      numArray2[19] = (byte) 210;
      numArray2[16 /*0x10*/] = (byte) 27;
      numArray2[32 /*0x20*/] = (byte) 17;
      numArray2[33] = (byte) 221;
      numArray2[34] = (byte) 86;
      numArray2[35] = (byte) 25;
      numArray2[54] = (byte) 164;
      numArray2[37] = (byte) 219;
      numArray2[48 /*0x30*/] = (byte) 241;
      numArray2[11] = (byte) 51;
      numArray2[38] = (byte) 114;
      numArray2[23] = (byte) 210;
      numArray2[25] = (byte) 166;
      numArray2[41] = (byte) 74;
      numArray2[44] = (byte) 26;
      numArray2[45] = (byte) 66;
      numArray2[46] = (byte) 160 /*0xA0*/;
      numArray2[47] = (byte) 238;
      numArray2[7] = (byte) 162;
      numArray2[13] = (byte) 39;
      numArray2[50] = (byte) 181;
      numArray2[26] = (byte) 90;
      numArray2[18] = (byte) 67;
      numArray2[53] = (byte) 26;
      numArray2[40] = (byte) 238;
      byte[] numArray3 = new byte[55]
      {
        (byte) 202,
        (byte) 158,
        (byte) 223,
        (byte) 199,
        (byte) 102,
        (byte) 117,
        (byte) 6,
        (byte) 81,
        (byte) 42,
        (byte) 68,
        (byte) 222,
        (byte) 228,
        (byte) 47,
        (byte) 23,
        (byte) 21,
        (byte) 76,
        (byte) 102,
        (byte) 93,
        (byte) 166,
        (byte) 24,
        (byte) 38,
        (byte) 72,
        (byte) 217,
        (byte) 214,
        (byte) 142,
        (byte) 219,
        (byte) 12,
        (byte) 4,
        (byte) 197,
        (byte) 175,
        (byte) 60,
        (byte) 36,
        (byte) 140,
        (byte) 190,
        (byte) 203,
        (byte) 214,
        (byte) 134,
        (byte) 114,
        (byte) 124,
        (byte) 163,
        (byte) 4,
        (byte) 215,
        (byte) 79,
        (byte) 147,
        (byte) 38,
        (byte) 218,
        (byte) 211,
        (byte) 235,
        (byte) 18,
        (byte) 139,
        (byte) 223,
        (byte) 43,
        (byte) 59,
        (byte) 235,
        (byte) 151
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49]
      {
        (byte) 112 /*0x70*/,
        (byte) 65,
        (byte) 121,
        (byte) 57,
        (byte) 220,
        (byte) 46,
        (byte) 60,
        (byte) 218,
        (byte) 184,
        (byte) 42,
        (byte) 235,
        (byte) 135,
        (byte) 69,
        (byte) 203,
        (byte) 234,
        (byte) 212,
        (byte) 75,
        (byte) 58,
        (byte) 6,
        (byte) 23,
        (byte) 197,
        (byte) 161,
        (byte) 36,
        (byte) 138,
        (byte) 78,
        (byte) 109,
        (byte) 170,
        (byte) 223,
        (byte) 0,
        (byte) 240 /*0xF0*/,
        (byte) 191,
        (byte) 17,
        (byte) 115,
        (byte) 91,
        (byte) 243,
        (byte) 19,
        (byte) 120,
        (byte) 239,
        (byte) 204,
        (byte) 71,
        (byte) 228,
        (byte) 115,
        (byte) 95,
        (byte) 245,
        (byte) 144 /*0x90*/,
        (byte) 150,
        (byte) 181,
        (byte) 47,
        (byte) 139
      };
      byte[] numArray5 = new byte[49]
      {
        (byte) 49,
        (byte) 227,
        (byte) 172,
        (byte) 219,
        (byte) 243,
        (byte) 135,
        (byte) 99,
        (byte) 241,
        (byte) 45,
        (byte) 145,
        (byte) 222,
        (byte) 70,
        (byte) 98,
        (byte) 55,
        (byte) 215,
        (byte) 9,
        (byte) 58,
        (byte) 160 /*0xA0*/,
        (byte) 78,
        (byte) 154,
        (byte) 75,
        (byte) 191,
        (byte) 155,
        (byte) 98,
        (byte) 96 /*0x60*/,
        (byte) 143,
        (byte) 107,
        (byte) 185,
        (byte) 23,
        (byte) 35,
        (byte) 175,
        (byte) 136,
        (byte) 53,
        (byte) 127 /*0x7F*/,
        (byte) 136,
        (byte) 9,
        (byte) 26,
        (byte) 148,
        (byte) 42,
        (byte) 172,
        (byte) 217,
        (byte) 100,
        (byte) 198,
        (byte) 243,
        (byte) 219,
        (byte) 132,
        (byte) 82,
        (byte) 29,
        (byte) 241
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[44];
      byte[] response = new byte[44];
      Array.Copy((Array) sc_12411.sspq, 200, (Array) numArray6, 0, 44);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12411.sspr, 200, (Array) numArray6, 0, 44);
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
    byte[] numArray7 = new byte[104];
    byte[] numArray8 = new byte[55]
    {
      (byte) 151,
      (byte) 138,
      (byte) 181,
      (byte) 130,
      (byte) 204,
      (byte) 87,
      (byte) 181,
      (byte) 137,
      (byte) 178,
      (byte) 143,
      (byte) 99,
      (byte) 138,
      (byte) 62,
      (byte) 140,
      (byte) 238,
      (byte) 244,
      (byte) 51,
      (byte) 183,
      (byte) 9,
      (byte) 8,
      (byte) 190,
      (byte) 231,
      (byte) 167,
      (byte) 235,
      (byte) 93,
      (byte) 30,
      (byte) 179,
      (byte) 223,
      (byte) 211,
      (byte) 89,
      (byte) 16 /*0x10*/,
      (byte) 250,
      (byte) 170,
      (byte) 184,
      (byte) 170,
      (byte) 164,
      (byte) 68,
      (byte) 52,
      (byte) 235,
      (byte) 34,
      (byte) 158,
      (byte) 146,
      (byte) 45,
      (byte) 51,
      (byte) 139,
      (byte) 44,
      (byte) 140,
      (byte) 151,
      (byte) 7,
      (byte) 148,
      (byte) 207,
      (byte) 165,
      (byte) 186,
      (byte) 127 /*0x7F*/,
      (byte) 219
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 192 /*0xC0*/,
      (byte) 74,
      (byte) 244,
      (byte) 133,
      (byte) 214,
      (byte) 69,
      (byte) 64 /*0x40*/,
      (byte) 186,
      (byte) 31 /*0x1F*/,
      (byte) 10,
      (byte) 68,
      (byte) 65,
      (byte) 19,
      (byte) 65,
      (byte) 132,
      (byte) 210,
      (byte) 200,
      (byte) 233,
      (byte) 59,
      (byte) 2,
      (byte) 165,
      (byte) 171,
      (byte) 69,
      (byte) 217,
      (byte) 231,
      (byte) 199,
      (byte) 87,
      (byte) 8,
      (byte) 189,
      (byte) 3,
      (byte) 9,
      (byte) 201,
      (byte) 105,
      (byte) 114,
      (byte) 130,
      (byte) 254,
      (byte) 39,
      (byte) 43,
      (byte) 209,
      (byte) 7,
      (byte) 202,
      (byte) 197,
      (byte) 27,
      (byte) 246,
      (byte) 118,
      (byte) 168,
      (byte) 66,
      (byte) 81,
      (byte) 133,
      (byte) 97,
      (byte) 15,
      (byte) 41,
      (byte) 27,
      (byte) 216,
      (byte) 226
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[49]
    {
      (byte) 202,
      (byte) 37,
      (byte) 173,
      (byte) 164,
      (byte) 213,
      (byte) 181,
      (byte) 89,
      (byte) 126,
      (byte) 104,
      (byte) 223,
      (byte) 134,
      (byte) 226,
      (byte) 74,
      (byte) 175,
      (byte) 254,
      (byte) 204,
      (byte) 230,
      (byte) 106,
      (byte) 44,
      (byte) 185,
      (byte) 116,
      (byte) 95,
      (byte) 48 /*0x30*/,
      (byte) 145,
      (byte) 27,
      (byte) 215,
      (byte) 63 /*0x3F*/,
      (byte) 137,
      (byte) 106,
      (byte) 64 /*0x40*/,
      (byte) 42,
      (byte) 221,
      (byte) 250,
      (byte) 193,
      (byte) 122,
      (byte) 15,
      (byte) 61,
      (byte) 126,
      (byte) 200,
      (byte) 220,
      (byte) 212,
      (byte) 181,
      (byte) 35,
      (byte) 76,
      (byte) 6,
      (byte) 181,
      (byte) 189,
      (byte) 228,
      (byte) 208 /*0xD0*/
    };
    byte[] numArray11 = new byte[49];
    numArray11[24] = (byte) 31 /*0x1F*/;
    numArray11[15] = (byte) 117;
    numArray11[41] = (byte) 189;
    numArray11[3] = (byte) 233;
    numArray11[4] = (byte) 241;
    numArray11[5] = (byte) 163;
    numArray11[1] = (byte) 30;
    numArray11[31 /*0x1F*/] = (byte) 39;
    numArray11[48 /*0x30*/] = (byte) 189;
    numArray11[19] = (byte) 123;
    numArray11[45] = (byte) 170;
    numArray11[37] = (byte) 211;
    numArray11[40] = (byte) 116;
    numArray11[13] = (byte) 251;
    numArray11[14] = (byte) 208 /*0xD0*/;
    numArray11[46] = (byte) 142;
    numArray11[16 /*0x10*/] = (byte) 163;
    numArray11[22] = (byte) 45;
    numArray11[18] = (byte) 113;
    numArray11[11] = (byte) 201;
    numArray11[20] = (byte) 116;
    numArray11[21] = (byte) 83;
    numArray11[6] = (byte) 250;
    numArray11[9] = (byte) 201;
    numArray11[10] = (byte) 135;
    numArray11[25] = (byte) 69;
    numArray11[44] = (byte) 188;
    numArray11[27] = (byte) 6;
    numArray11[28] = (byte) 88;
    numArray11[39] = (byte) 222;
    numArray11[30] = (byte) 205;
    numArray11[29] = (byte) 215;
    numArray11[12] = (byte) 116;
    numArray11[23] = byte.MaxValue;
    numArray11[34] = (byte) 48 /*0x30*/;
    numArray11[35] = (byte) 146;
    numArray11[33] = (byte) 198;
    numArray11[36] = (byte) 171;
    numArray11[32 /*0x20*/] = (byte) 192 /*0xC0*/;
    numArray11[0] = (byte) 183;
    numArray11[17] = (byte) 39;
    numArray11[7] = (byte) 29;
    numArray11[42] = (byte) 205;
    numArray11[2] = (byte) 133;
    numArray11[26] = (byte) 239;
    numArray11[8] = (byte) 132;
    numArray11[43] = (byte) 228;
    numArray11[47] = (byte) 10;
    numArray11[38] = (byte) 119;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 49);
    for (int index = 0; index < 49; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12423()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[104];
      byte[] numArray2 = new byte[55];
      numArray2[29] = (byte) 65;
      numArray2[1] = (byte) 224 /*0xE0*/;
      numArray2[2] = (byte) 74;
      numArray2[51] = (byte) 246;
      numArray2[4] = (byte) 115;
      numArray2[5] = (byte) 94;
      numArray2[26] = (byte) 234;
      numArray2[7] = (byte) 175;
      numArray2[8] = (byte) 47;
      numArray2[9] = (byte) 16 /*0x10*/;
      numArray2[53] = (byte) 234;
      numArray2[25] = (byte) 215;
      numArray2[3] = (byte) 107;
      numArray2[13] = (byte) 28;
      numArray2[44] = (byte) 143;
      numArray2[15] = (byte) 131;
      numArray2[22] = (byte) 213;
      numArray2[21] = (byte) 38;
      numArray2[20] = (byte) 52;
      numArray2[19] = (byte) 227;
      numArray2[33] = (byte) 228;
      numArray2[18] = (byte) 123;
      numArray2[24] = (byte) 113;
      numArray2[11] = (byte) 34;
      numArray2[42] = (byte) 223;
      numArray2[41] = (byte) 163;
      numArray2[17] = (byte) 160 /*0xA0*/;
      numArray2[16 /*0x10*/] = (byte) 187;
      numArray2[28] = (byte) 30;
      numArray2[30] = (byte) 77;
      numArray2[43] = (byte) 10;
      numArray2[31 /*0x1F*/] = (byte) 46;
      numArray2[48 /*0x30*/] = (byte) 237;
      numArray2[12] = (byte) 228;
      numArray2[34] = (byte) 172;
      numArray2[35] = (byte) 216;
      numArray2[36] = (byte) 70;
      numArray2[37] = (byte) 0;
      numArray2[38] = (byte) 160 /*0xA0*/;
      numArray2[39] = (byte) 207;
      numArray2[40] = (byte) 242;
      numArray2[10] = (byte) 145;
      numArray2[46] = (byte) 230;
      numArray2[49] = (byte) 29;
      numArray2[27] = (byte) 224 /*0xE0*/;
      numArray2[45] = (byte) 211;
      numArray2[23] = (byte) 109;
      numArray2[47] = (byte) 73;
      numArray2[0] = (byte) 174;
      numArray2[14] = (byte) 22;
      numArray2[6] = (byte) 17;
      numArray2[50] = (byte) 62;
      numArray2[52] = (byte) 225;
      numArray2[32 /*0x20*/] = (byte) 152;
      numArray2[54] = (byte) 0;
      byte[] numArray3 = new byte[55]
      {
        (byte) 97,
        (byte) 173,
        (byte) 199,
        (byte) 215,
        (byte) 122,
        (byte) 80 /*0x50*/,
        (byte) 132,
        (byte) 241,
        (byte) 33,
        (byte) 113,
        (byte) 84,
        (byte) 242,
        (byte) 140,
        (byte) 176 /*0xB0*/,
        (byte) 246,
        (byte) 116,
        (byte) 201,
        (byte) 10,
        (byte) 87,
        (byte) 130,
        (byte) 111,
        (byte) 195,
        (byte) 253,
        (byte) 164,
        (byte) 110,
        (byte) 159,
        (byte) 132,
        (byte) 144 /*0x90*/,
        (byte) 94,
        (byte) 213,
        (byte) 157,
        (byte) 27,
        (byte) 126,
        (byte) 78,
        (byte) 50,
        (byte) 165,
        (byte) 65,
        (byte) 220,
        (byte) 90,
        (byte) 45,
        (byte) 213,
        (byte) 240 /*0xF0*/,
        (byte) 14,
        (byte) 227,
        (byte) 117,
        (byte) 100,
        (byte) 79,
        (byte) 204,
        (byte) 148,
        (byte) 217,
        (byte) 219,
        (byte) 66,
        (byte) 242,
        (byte) 66,
        (byte) 19
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49]
      {
        byte.MaxValue,
        (byte) 91,
        (byte) 170,
        (byte) 211,
        (byte) 61,
        (byte) 144 /*0x90*/,
        (byte) 236,
        (byte) 107,
        (byte) 22,
        (byte) 219,
        (byte) 27,
        (byte) 75,
        (byte) 111,
        (byte) 212,
        (byte) 180,
        (byte) 250,
        (byte) 80 /*0x50*/,
        (byte) 46,
        (byte) 126,
        (byte) 86,
        (byte) 131,
        (byte) 177,
        (byte) 84,
        (byte) 10,
        (byte) 199,
        (byte) 67,
        (byte) 31 /*0x1F*/,
        (byte) 126,
        (byte) 95,
        (byte) 22,
        (byte) 181,
        (byte) 115,
        (byte) 227,
        (byte) 142,
        (byte) 104,
        (byte) 170,
        (byte) 89,
        (byte) 135,
        (byte) 133,
        (byte) 129,
        (byte) 4,
        (byte) 220,
        (byte) 21,
        (byte) 110,
        (byte) 202,
        (byte) 141,
        (byte) 64 /*0x40*/,
        (byte) 12,
        (byte) 206
      };
      byte[] numArray5 = new byte[49]
      {
        (byte) 9,
        (byte) 1,
        (byte) 15,
        (byte) 17,
        (byte) 169,
        (byte) 223,
        (byte) 34,
        (byte) 194,
        (byte) 137,
        (byte) 134,
        (byte) 191,
        (byte) 248,
        (byte) 73,
        (byte) 131,
        (byte) 88,
        (byte) 226,
        (byte) 218,
        (byte) 252,
        (byte) 195,
        (byte) 50,
        (byte) 154,
        (byte) 163,
        (byte) 21,
        (byte) 155,
        (byte) 177,
        (byte) 10,
        (byte) 205,
        (byte) 228,
        (byte) 144 /*0x90*/,
        (byte) 147,
        (byte) 227,
        (byte) 143,
        (byte) 85,
        (byte) 18,
        (byte) 31 /*0x1F*/,
        (byte) 104,
        (byte) 191,
        (byte) 73,
        (byte) 180,
        (byte) 29,
        (byte) 187,
        (byte) 217,
        (byte) 142,
        (byte) 236,
        (byte) 179,
        (byte) 186,
        (byte) 114,
        (byte) 154,
        (byte) 194
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[104];
    byte[] numArray7 = new byte[55]
    {
      (byte) 241,
      (byte) 234,
      (byte) 187,
      (byte) 78,
      (byte) 7,
      (byte) 160 /*0xA0*/,
      (byte) 11,
      (byte) 249,
      (byte) 113,
      (byte) 109,
      (byte) 11,
      (byte) 0,
      (byte) 37,
      (byte) 167,
      (byte) 103,
      (byte) 225,
      (byte) 107,
      (byte) 9,
      (byte) 161,
      (byte) 201,
      (byte) 169,
      (byte) 252,
      (byte) 81,
      (byte) 167,
      (byte) 155,
      (byte) 230,
      (byte) 64 /*0x40*/,
      (byte) 57,
      (byte) 3,
      (byte) 19,
      (byte) 176 /*0xB0*/,
      (byte) 99,
      (byte) 49,
      (byte) 235,
      (byte) 130,
      (byte) 249,
      (byte) 210,
      (byte) 25,
      (byte) 163,
      (byte) 185,
      (byte) 123,
      (byte) 139,
      (byte) 16 /*0x10*/,
      (byte) 151,
      (byte) 208 /*0xD0*/,
      (byte) 21,
      (byte) 72,
      (byte) 34,
      (byte) 37,
      (byte) 94,
      (byte) 160 /*0xA0*/,
      (byte) 196,
      (byte) 90,
      (byte) 27,
      (byte) 32 /*0x20*/
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 20,
      (byte) 52,
      (byte) 223,
      (byte) 102,
      (byte) 67,
      (byte) 254,
      (byte) 120,
      (byte) 94,
      (byte) 221,
      byte.MaxValue,
      (byte) 154,
      (byte) 139,
      (byte) 13,
      (byte) 185,
      (byte) 94,
      (byte) 86,
      (byte) 2,
      (byte) 55,
      (byte) 128 /*0x80*/,
      (byte) 12,
      (byte) 120,
      (byte) 226,
      (byte) 128 /*0x80*/,
      (byte) 75,
      (byte) 198,
      (byte) 234,
      (byte) 172,
      (byte) 21,
      (byte) 165,
      (byte) 205,
      (byte) 11,
      (byte) 225,
      (byte) 129,
      (byte) 149,
      (byte) 58,
      (byte) 117,
      (byte) 206,
      (byte) 166,
      (byte) 96 /*0x60*/,
      (byte) 91,
      (byte) 25,
      (byte) 56,
      (byte) 216,
      (byte) 241,
      (byte) 125,
      (byte) 131,
      (byte) 14,
      (byte) 119,
      (byte) 197,
      (byte) 130,
      (byte) 126,
      (byte) 70,
      (byte) 224 /*0xE0*/,
      (byte) 182,
      (byte) 236
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[49]
    {
      (byte) 20,
      (byte) 179,
      (byte) 33,
      (byte) 95,
      (byte) 210,
      (byte) 246,
      (byte) 94,
      (byte) 189,
      (byte) 168,
      (byte) 206,
      (byte) 58,
      (byte) 37,
      (byte) 110,
      (byte) 14,
      (byte) 81,
      (byte) 102,
      (byte) 232,
      (byte) 77,
      (byte) 30,
      (byte) 236,
      (byte) 185,
      (byte) 214,
      (byte) 133,
      (byte) 157,
      (byte) 29,
      (byte) 19,
      (byte) 164,
      (byte) 114,
      (byte) 235,
      (byte) 8,
      (byte) 21,
      (byte) 203,
      (byte) 140,
      (byte) 96 /*0x60*/,
      (byte) 190,
      (byte) 102,
      (byte) 130,
      (byte) 89,
      (byte) 169,
      (byte) 148,
      (byte) 178,
      (byte) 218,
      (byte) 39,
      (byte) 71,
      (byte) 14,
      (byte) 253,
      (byte) 183,
      (byte) 80 /*0x50*/,
      (byte) 78
    };
    byte[] numArray10 = new byte[49];
    numArray10[21] = (byte) 75;
    numArray10[29] = (byte) 190;
    numArray10[4] = (byte) 212;
    numArray10[3] = (byte) 138;
    numArray10[13] = (byte) 185;
    numArray10[32 /*0x20*/] = (byte) 32 /*0x20*/;
    numArray10[15] = (byte) 105;
    numArray10[42] = (byte) 193;
    numArray10[46] = (byte) 77;
    numArray10[24] = (byte) 87;
    numArray10[10] = (byte) 143;
    numArray10[11] = (byte) 167;
    numArray10[12] = (byte) 55;
    numArray10[23] = (byte) 158;
    numArray10[14] = (byte) 128 /*0x80*/;
    numArray10[17] = (byte) 79;
    numArray10[16 /*0x10*/] = (byte) 227;
    numArray10[20] = (byte) 63 /*0x3F*/;
    numArray10[9] = (byte) 165;
    numArray10[2] = (byte) 31 /*0x1F*/;
    numArray10[48 /*0x30*/] = (byte) 37;
    numArray10[33] = (byte) 223;
    numArray10[7] = (byte) 80 /*0x50*/;
    numArray10[19] = (byte) 82;
    numArray10[34] = (byte) 12;
    numArray10[25] = (byte) 13;
    numArray10[26] = (byte) 132;
    numArray10[6] = (byte) 32 /*0x20*/;
    numArray10[43] = (byte) 186;
    numArray10[38] = (byte) 133;
    numArray10[18] = (byte) 201;
    numArray10[31 /*0x1F*/] = (byte) 43;
    numArray10[44] = (byte) 88;
    numArray10[8] = (byte) 86;
    numArray10[0] = (byte) 62;
    numArray10[28] = (byte) 143;
    numArray10[36] = (byte) 213;
    numArray10[37] = (byte) 226;
    numArray10[39] = (byte) 218;
    numArray10[22] = (byte) 104;
    numArray10[40] = (byte) 112 /*0x70*/;
    numArray10[41] = (byte) 60;
    numArray10[30] = (byte) 187;
    numArray10[1] = (byte) 169;
    numArray10[27] = (byte) 239;
    numArray10[45] = (byte) 54;
    numArray10[5] = (byte) 155;
    numArray10[47] = (byte) 13;
    numArray10[35] = (byte) 171;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 49);
    for (int index = 0; index < 49; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12424()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        byte.MaxValue,
        (byte) 99,
        (byte) 123,
        (byte) 228,
        (byte) 42,
        (byte) 119,
        (byte) 207,
        byte.MaxValue,
        (byte) 142,
        (byte) 238
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 23,
        (byte) 12,
        (byte) 59,
        (byte) 244,
        (byte) 136,
        (byte) 8,
        (byte) 144 /*0x90*/,
        (byte) 231,
        (byte) 205,
        (byte) 86
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
      (byte) 253,
      (byte) 199,
      (byte) 68,
      (byte) 10,
      (byte) 113,
      (byte) 164,
      (byte) 21,
      (byte) 141,
      (byte) 206,
      (byte) 178
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 168,
      (byte) 88,
      (byte) 114,
      (byte) 18,
      (byte) 138,
      (byte) 227,
      (byte) 204,
      (byte) 11,
      (byte) 194,
      (byte) 20
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12425()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[104];
      byte[] numArray2 = new byte[55]
      {
        (byte) 114,
        (byte) 214,
        (byte) 39,
        (byte) 176 /*0xB0*/,
        (byte) 238,
        (byte) 188,
        (byte) 178,
        (byte) 84,
        (byte) 192 /*0xC0*/,
        (byte) 18,
        (byte) 177,
        (byte) 28,
        (byte) 82,
        (byte) 180,
        (byte) 128 /*0x80*/,
        (byte) 242,
        (byte) 74,
        (byte) 9,
        (byte) 245,
        (byte) 135,
        (byte) 68,
        byte.MaxValue,
        (byte) 196,
        (byte) 125,
        (byte) 28,
        (byte) 251,
        (byte) 78,
        (byte) 27,
        (byte) 48 /*0x30*/,
        (byte) 176 /*0xB0*/,
        (byte) 150,
        (byte) 109,
        (byte) 113,
        (byte) 47,
        (byte) 92,
        (byte) 41,
        (byte) 103,
        (byte) 62,
        (byte) 7,
        (byte) 101,
        (byte) 175,
        (byte) 168,
        (byte) 6,
        (byte) 50,
        (byte) 89,
        (byte) 85,
        (byte) 82,
        (byte) 97,
        (byte) 12,
        (byte) 220,
        (byte) 103,
        (byte) 218,
        (byte) 130,
        (byte) 49,
        (byte) 2
      };
      byte[] numArray3 = new byte[55];
      numArray3[13] = (byte) 21;
      numArray3[12] = (byte) 122;
      numArray3[2] = (byte) 155;
      numArray3[3] = (byte) 52;
      numArray3[42] = (byte) 151;
      numArray3[46] = (byte) 227;
      numArray3[24] = (byte) 246;
      numArray3[28] = (byte) 33;
      numArray3[8] = (byte) 90;
      numArray3[9] = (byte) 145;
      numArray3[34] = (byte) 174;
      numArray3[23] = (byte) 103;
      numArray3[45] = (byte) 154;
      numArray3[52] = (byte) 117;
      numArray3[10] = (byte) 24;
      numArray3[40] = (byte) 251;
      numArray3[16 /*0x10*/] = (byte) 192 /*0xC0*/;
      numArray3[4] = (byte) 10;
      numArray3[53] = (byte) 147;
      numArray3[48 /*0x30*/] = (byte) 206;
      numArray3[20] = (byte) 214;
      numArray3[21] = (byte) 39;
      numArray3[22] = (byte) 190;
      numArray3[1] = (byte) 8;
      numArray3[7] = (byte) 170;
      numArray3[25] = (byte) 130;
      numArray3[26] = (byte) 23;
      numArray3[27] = (byte) 8;
      numArray3[36] = (byte) 99;
      numArray3[29] = (byte) 142;
      numArray3[30] = (byte) 129;
      numArray3[6] = (byte) 14;
      numArray3[32 /*0x20*/] = (byte) 239;
      numArray3[33] = (byte) 113;
      numArray3[44] = (byte) 62;
      numArray3[35] = (byte) 23;
      numArray3[5] = (byte) 166;
      numArray3[37] = (byte) 138;
      numArray3[38] = (byte) 108;
      numArray3[39] = (byte) 220;
      numArray3[54] = (byte) 247;
      numArray3[41] = (byte) 159;
      numArray3[31 /*0x1F*/] = (byte) 141;
      numArray3[43] = (byte) 112 /*0x70*/;
      numArray3[14] = (byte) 7;
      numArray3[47] = (byte) 5;
      numArray3[15] = (byte) 217;
      numArray3[19] = (byte) 92;
      numArray3[11] = (byte) 249;
      numArray3[49] = (byte) 207;
      numArray3[50] = (byte) 33;
      numArray3[51] = (byte) 39;
      numArray3[18] = (byte) 204;
      numArray3[17] = (byte) 47;
      numArray3[0] = (byte) 200;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49]
      {
        (byte) 134,
        (byte) 204,
        (byte) 74,
        (byte) 128 /*0x80*/,
        (byte) 136,
        (byte) 206,
        (byte) 198,
        (byte) 191,
        (byte) 89,
        (byte) 105,
        (byte) 246,
        (byte) 109,
        (byte) 65,
        (byte) 206,
        (byte) 97,
        (byte) 238,
        (byte) 150,
        (byte) 191,
        (byte) 22,
        (byte) 1,
        (byte) 218,
        (byte) 14,
        (byte) 28,
        (byte) 121,
        (byte) 227,
        (byte) 90,
        (byte) 4,
        (byte) 237,
        (byte) 227,
        (byte) 247,
        (byte) 231,
        (byte) 198,
        (byte) 111,
        (byte) 149,
        (byte) 54,
        (byte) 222,
        (byte) 201,
        (byte) 120,
        (byte) 144 /*0x90*/,
        (byte) 182,
        (byte) 48 /*0x30*/,
        (byte) 248,
        (byte) 188,
        (byte) 65,
        (byte) 57,
        (byte) 156,
        (byte) 115,
        (byte) 129,
        (byte) 250
      };
      byte[] numArray5 = new byte[49]
      {
        (byte) 35,
        (byte) 238,
        (byte) 6,
        (byte) 47,
        (byte) 107,
        (byte) 40,
        (byte) 197,
        (byte) 182,
        (byte) 34,
        (byte) 232,
        (byte) 102,
        (byte) 12,
        (byte) 86,
        (byte) 188,
        (byte) 162,
        (byte) 150,
        (byte) 68,
        (byte) 85,
        (byte) 85,
        (byte) 167,
        (byte) 140,
        (byte) 30,
        (byte) 212,
        (byte) 201,
        (byte) 64 /*0x40*/,
        (byte) 139,
        (byte) 14,
        (byte) 149,
        (byte) 243,
        (byte) 58,
        (byte) 62,
        (byte) 252,
        (byte) 196,
        (byte) 139,
        (byte) 3,
        (byte) 38,
        (byte) 27,
        (byte) 181,
        (byte) 93,
        (byte) 133,
        (byte) 9,
        (byte) 70,
        (byte) 210,
        (byte) 88,
        (byte) 196,
        (byte) 56,
        (byte) 211,
        (byte) 153,
        (byte) 44
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_12411.sspq, 244, (Array) numArray6, 0, 45);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_12411.sspr, 244, (Array) numArray6, 0, 45);
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
    byte[] numArray7 = new byte[104];
    byte[] numArray8 = new byte[55]
    {
      (byte) 191,
      (byte) 9,
      (byte) 157,
      (byte) 199,
      (byte) 232,
      (byte) 119,
      (byte) 198,
      (byte) 61,
      (byte) 105,
      (byte) 239,
      (byte) 167,
      (byte) 196,
      (byte) 37,
      (byte) 213,
      (byte) 143,
      (byte) 85,
      (byte) 70,
      (byte) 70,
      (byte) 230,
      (byte) 227,
      (byte) 197,
      (byte) 51,
      (byte) 185,
      (byte) 8,
      (byte) 189,
      (byte) 217,
      (byte) 83,
      (byte) 42,
      (byte) 67,
      (byte) 204,
      (byte) 116,
      (byte) 122,
      (byte) 198,
      (byte) 247,
      (byte) 26,
      (byte) 55,
      (byte) 169,
      (byte) 32 /*0x20*/,
      (byte) 154,
      (byte) 123,
      (byte) 50,
      (byte) 93,
      (byte) 229,
      (byte) 134,
      (byte) 78,
      (byte) 41,
      (byte) 153,
      (byte) 89,
      (byte) 105,
      (byte) 133,
      (byte) 28,
      (byte) 165,
      (byte) 112 /*0x70*/,
      (byte) 56,
      (byte) 149
    };
    byte[] numArray9 = new byte[55];
    numArray9[46] = (byte) 109;
    numArray9[1] = (byte) 44;
    numArray9[2] = (byte) 129;
    numArray9[3] = (byte) 249;
    numArray9[30] = (byte) 78;
    numArray9[38] = (byte) 194;
    numArray9[14] = (byte) 219;
    numArray9[35] = (byte) 98;
    numArray9[8] = (byte) 98;
    numArray9[45] = (byte) 47;
    numArray9[10] = (byte) 229;
    numArray9[11] = (byte) 241;
    numArray9[12] = (byte) 50;
    numArray9[13] = (byte) 64 /*0x40*/;
    numArray9[20] = (byte) 233;
    numArray9[15] = (byte) 201;
    numArray9[16 /*0x10*/] = (byte) 14;
    numArray9[17] = (byte) 247;
    numArray9[43] = (byte) 162;
    numArray9[19] = (byte) 243;
    numArray9[4] = (byte) 108;
    numArray9[50] = (byte) 250;
    numArray9[9] = (byte) 78;
    numArray9[42] = (byte) 26;
    numArray9[7] = (byte) 73;
    numArray9[25] = (byte) 5;
    numArray9[26] = (byte) 192 /*0xC0*/;
    numArray9[27] = (byte) 205;
    numArray9[0] = (byte) 33;
    numArray9[34] = (byte) 46;
    numArray9[29] = (byte) 191;
    numArray9[21] = (byte) 140;
    numArray9[32 /*0x20*/] = (byte) 12;
    numArray9[33] = (byte) 19;
    numArray9[6] = (byte) 129;
    numArray9[31 /*0x1F*/] = (byte) 128 /*0x80*/;
    numArray9[36] = (byte) 29;
    numArray9[37] = (byte) 215;
    numArray9[18] = (byte) 73;
    numArray9[39] = (byte) 86;
    numArray9[40] = (byte) 187;
    numArray9[47] = (byte) 5;
    numArray9[5] = (byte) 250;
    numArray9[22] = (byte) 108;
    numArray9[44] = (byte) 3;
    numArray9[41] = (byte) 178;
    numArray9[23] = (byte) 115;
    numArray9[24] = (byte) 227;
    numArray9[48 /*0x30*/] = (byte) 73;
    numArray9[51] = (byte) 224 /*0xE0*/;
    numArray9[28] = (byte) 34;
    numArray9[49] = (byte) 136;
    numArray9[52] = (byte) 5;
    numArray9[53] = (byte) 17;
    numArray9[54] = (byte) 252;
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[49];
    numArray10[10] = (byte) 204;
    numArray10[1] = (byte) 117;
    numArray10[37] = (byte) 46;
    numArray10[27] = (byte) 186;
    numArray10[36] = (byte) 188;
    numArray10[20] = (byte) 38;
    numArray10[6] = (byte) 236;
    numArray10[7] = (byte) 172;
    numArray10[8] = (byte) 34;
    numArray10[47] = (byte) 64 /*0x40*/;
    numArray10[9] = (byte) 112 /*0x70*/;
    numArray10[12] = (byte) 113;
    numArray10[31 /*0x1F*/] = (byte) 52;
    numArray10[13] = (byte) 153;
    numArray10[2] = (byte) 206;
    numArray10[21] = (byte) 151;
    numArray10[41] = (byte) 94;
    numArray10[17] = (byte) 25;
    numArray10[4] = (byte) 218;
    numArray10[0] = (byte) 220;
    numArray10[28] = (byte) 213;
    numArray10[38] = (byte) 189;
    numArray10[22] = (byte) 104;
    numArray10[23] = (byte) 214;
    numArray10[24] = (byte) 91;
    numArray10[3] = (byte) 41;
    numArray10[26] = (byte) 228;
    numArray10[15] = (byte) 33;
    numArray10[14] = (byte) 15;
    numArray10[11] = (byte) 43;
    numArray10[30] = (byte) 5;
    numArray10[46] = (byte) 159;
    numArray10[32 /*0x20*/] = (byte) 79;
    numArray10[33] = (byte) 202;
    numArray10[34] = (byte) 55;
    numArray10[35] = (byte) 161;
    numArray10[18] = (byte) 4;
    numArray10[19] = (byte) 5;
    numArray10[16 /*0x10*/] = (byte) 18;
    numArray10[39] = (byte) 38;
    numArray10[40] = (byte) 92;
    numArray10[45] = (byte) 115;
    numArray10[42] = byte.MaxValue;
    numArray10[25] = (byte) 176 /*0xB0*/;
    numArray10[43] = (byte) 186;
    numArray10[29] = (byte) 4;
    numArray10[44] = (byte) 158;
    numArray10[5] = (byte) 61;
    numArray10[48 /*0x30*/] = (byte) 230;
    byte[] numArray11 = new byte[49]
    {
      (byte) 127 /*0x7F*/,
      (byte) 110,
      (byte) 96 /*0x60*/,
      (byte) 216,
      (byte) 41,
      (byte) 202,
      (byte) 187,
      (byte) 203,
      (byte) 31 /*0x1F*/,
      (byte) 246,
      (byte) 243,
      (byte) 178,
      (byte) 2,
      (byte) 5,
      (byte) 130,
      (byte) 157,
      (byte) 103,
      (byte) 65,
      (byte) 71,
      (byte) 246,
      (byte) 226,
      (byte) 69,
      (byte) 193,
      (byte) 68,
      (byte) 227,
      (byte) 113,
      (byte) 94,
      (byte) 137,
      (byte) 104,
      (byte) 194,
      (byte) 32 /*0x20*/,
      (byte) 144 /*0x90*/,
      (byte) 161,
      (byte) 225,
      (byte) 204,
      (byte) 156,
      (byte) 190,
      (byte) 73,
      (byte) 32 /*0x20*/,
      (byte) 10,
      (byte) 189,
      (byte) 239,
      (byte) 54,
      (byte) 4,
      (byte) 46,
      (byte) 132,
      (byte) 181,
      (byte) 59,
      (byte) 19
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 49);
    for (int index = 0; index < 49; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_12426()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[102];
      byte[] numArray2 = new byte[55];
      numArray2[23] = (byte) 159;
      numArray2[7] = (byte) 45;
      numArray2[39] = (byte) 191;
      numArray2[52] = (byte) 121;
      numArray2[33] = (byte) 24;
      numArray2[5] = (byte) 75;
      numArray2[6] = (byte) 144 /*0x90*/;
      numArray2[16 /*0x10*/] = (byte) 13;
      numArray2[40] = (byte) 133;
      numArray2[9] = (byte) 96 /*0x60*/;
      numArray2[24] = (byte) 130;
      numArray2[11] = (byte) 229;
      numArray2[19] = (byte) 198;
      numArray2[13] = (byte) 181;
      numArray2[17] = (byte) 213;
      numArray2[12] = (byte) 24;
      numArray2[49] = (byte) 174;
      numArray2[32 /*0x20*/] = (byte) 81;
      numArray2[31 /*0x1F*/] = (byte) 49;
      numArray2[20] = (byte) 210;
      numArray2[15] = (byte) 217;
      numArray2[4] = (byte) 18;
      numArray2[22] = (byte) 218;
      numArray2[2] = (byte) 115;
      numArray2[36] = (byte) 138;
      numArray2[25] = (byte) 200;
      numArray2[26] = (byte) 16 /*0x10*/;
      numArray2[48 /*0x30*/] = (byte) 204;
      numArray2[28] = (byte) 64 /*0x40*/;
      numArray2[10] = (byte) 192 /*0xC0*/;
      numArray2[30] = (byte) 93;
      numArray2[37] = (byte) 1;
      numArray2[8] = (byte) 172;
      numArray2[41] = (byte) 166;
      numArray2[21] = (byte) 110;
      numArray2[35] = (byte) 92;
      numArray2[46] = (byte) 122;
      numArray2[1] = (byte) 120;
      numArray2[38] = (byte) 131;
      numArray2[44] = (byte) 4;
      numArray2[14] = (byte) 93;
      numArray2[29] = (byte) 183;
      numArray2[42] = (byte) 70;
      numArray2[43] = (byte) 51;
      numArray2[34] = (byte) 197;
      numArray2[45] = (byte) 123;
      numArray2[0] = (byte) 222;
      numArray2[47] = (byte) 24;
      numArray2[18] = (byte) 140;
      numArray2[27] = (byte) 126;
      numArray2[50] = (byte) 210;
      numArray2[51] = (byte) 121;
      numArray2[3] = (byte) 15;
      numArray2[53] = (byte) 208 /*0xD0*/;
      numArray2[54] = (byte) 38;
      byte[] numArray3 = new byte[55]
      {
        (byte) 49,
        (byte) 31 /*0x1F*/,
        (byte) 125,
        (byte) 114,
        (byte) 199,
        (byte) 46,
        (byte) 91,
        (byte) 58,
        (byte) 234,
        (byte) 209,
        (byte) 239,
        (byte) 134,
        (byte) 37,
        (byte) 55,
        (byte) 185,
        (byte) 62,
        (byte) 163,
        (byte) 28,
        (byte) 75,
        (byte) 183,
        (byte) 24,
        (byte) 200,
        (byte) 206,
        (byte) 34,
        (byte) 34,
        (byte) 82,
        (byte) 153,
        (byte) 126,
        (byte) 73,
        (byte) 212,
        (byte) 172,
        (byte) 16 /*0x10*/,
        (byte) 213,
        (byte) 17,
        (byte) 139,
        (byte) 233,
        (byte) 219,
        (byte) 82,
        (byte) 2,
        (byte) 232,
        (byte) 111,
        (byte) 235,
        (byte) 93,
        (byte) 109,
        (byte) 91,
        (byte) 159,
        (byte) 253,
        (byte) 57,
        (byte) 16 /*0x10*/,
        (byte) 182,
        (byte) 158,
        (byte) 26,
        (byte) 231,
        (byte) 254,
        (byte) 169
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[47]
      {
        (byte) 56,
        (byte) 133,
        (byte) 251,
        (byte) 66,
        (byte) 95,
        (byte) 90,
        (byte) 15,
        (byte) 30,
        (byte) 213,
        (byte) 19,
        (byte) 14,
        (byte) 154,
        (byte) 67,
        (byte) 151,
        (byte) 151,
        (byte) 23,
        (byte) 71,
        (byte) 205,
        (byte) 114,
        (byte) 214,
        (byte) 116,
        (byte) 37,
        (byte) 31 /*0x1F*/,
        (byte) 211,
        (byte) 207,
        (byte) 230,
        (byte) 51,
        (byte) 30,
        (byte) 188,
        (byte) 12,
        (byte) 241,
        (byte) 247,
        (byte) 14,
        (byte) 198,
        (byte) 126,
        (byte) 239,
        (byte) 62,
        (byte) 66,
        (byte) 19,
        (byte) 95,
        (byte) 66,
        (byte) 19,
        (byte) 160 /*0xA0*/,
        (byte) 212,
        (byte) 107,
        (byte) 142,
        (byte) 136
      };
      byte[] numArray5 = new byte[47]
      {
        (byte) 229,
        (byte) 6,
        (byte) 62,
        (byte) 66,
        (byte) 106,
        (byte) 24,
        (byte) 82,
        (byte) 33,
        (byte) 4,
        (byte) 181,
        (byte) 143,
        (byte) 69,
        (byte) 184,
        (byte) 213,
        (byte) 161,
        (byte) 102,
        (byte) 95,
        (byte) 73,
        (byte) 16 /*0x10*/,
        (byte) 192 /*0xC0*/,
        (byte) 169,
        (byte) 48 /*0x30*/,
        (byte) 147,
        (byte) 224 /*0xE0*/,
        (byte) 43,
        (byte) 15,
        (byte) 7,
        (byte) 207,
        (byte) 44,
        (byte) 143,
        (byte) 243,
        (byte) 68,
        (byte) 123,
        (byte) 221,
        (byte) 159,
        (byte) 204,
        (byte) 107,
        (byte) 56,
        (byte) 185,
        (byte) 198,
        (byte) 42,
        (byte) 33,
        (byte) 146,
        (byte) 164,
        (byte) 229,
        (byte) 211,
        (byte) 241
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 47);
      for (int index = 0; index < 47; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[102];
    byte[] numArray7 = new byte[55];
    numArray7[45] = (byte) 81;
    numArray7[4] = (byte) 130;
    numArray7[2] = (byte) 135;
    numArray7[15] = (byte) 23;
    numArray7[28] = (byte) 104;
    numArray7[10] = (byte) 38;
    numArray7[6] = (byte) 101;
    numArray7[37] = (byte) 156;
    numArray7[8] = (byte) 115;
    numArray7[9] = (byte) 121;
    numArray7[24] = (byte) 147;
    numArray7[11] = (byte) 241;
    numArray7[12] = (byte) 97;
    numArray7[39] = (byte) 107;
    numArray7[48 /*0x30*/] = (byte) 178;
    numArray7[42] = (byte) 207;
    numArray7[16 /*0x10*/] = (byte) 140;
    numArray7[54] = (byte) 54;
    numArray7[18] = (byte) 25;
    numArray7[19] = (byte) 168;
    numArray7[20] = (byte) 217;
    numArray7[21] = (byte) 5;
    numArray7[0] = (byte) 100;
    numArray7[23] = (byte) 19;
    numArray7[22] = (byte) 185;
    numArray7[25] = (byte) 97;
    numArray7[26] = (byte) 133;
    numArray7[27] = (byte) 75;
    numArray7[33] = (byte) 172;
    numArray7[29] = (byte) 65;
    numArray7[30] = (byte) 94;
    numArray7[31 /*0x1F*/] = (byte) 108;
    numArray7[36] = (byte) 112 /*0x70*/;
    numArray7[7] = (byte) 40;
    numArray7[34] = (byte) 168;
    numArray7[52] = (byte) 173;
    numArray7[1] = (byte) 193;
    numArray7[43] = (byte) 154;
    numArray7[38] = (byte) 68;
    numArray7[32 /*0x20*/] = (byte) 238;
    numArray7[14] = (byte) 33;
    numArray7[41] = (byte) 22;
    numArray7[17] = (byte) 124;
    numArray7[44] = (byte) 56;
    numArray7[5] = (byte) 18;
    numArray7[40] = (byte) 137;
    numArray7[46] = (byte) 177;
    numArray7[47] = (byte) 215;
    numArray7[35] = (byte) 47;
    numArray7[49] = (byte) 84;
    numArray7[50] = (byte) 2;
    numArray7[51] = (byte) 11;
    numArray7[13] = (byte) 201;
    numArray7[53] = (byte) 135;
    numArray7[3] = (byte) 113;
    byte[] numArray8 = new byte[55];
    numArray8[51] = (byte) 236;
    numArray8[1] = (byte) 48 /*0x30*/;
    numArray8[33] = (byte) 153;
    numArray8[40] = (byte) 125;
    numArray8[42] = (byte) 237;
    numArray8[34] = (byte) 21;
    numArray8[5] = (byte) 205;
    numArray8[4] = (byte) 202;
    numArray8[0] = (byte) 46;
    numArray8[9] = (byte) 135;
    numArray8[25] = (byte) 102;
    numArray8[11] = (byte) 176 /*0xB0*/;
    numArray8[12] = (byte) 199;
    numArray8[13] = (byte) 52;
    numArray8[38] = (byte) 149;
    numArray8[52] = (byte) 220;
    numArray8[16 /*0x10*/] = (byte) 65;
    numArray8[17] = (byte) 232;
    numArray8[18] = (byte) 150;
    numArray8[15] = (byte) 21;
    numArray8[30] = (byte) 47;
    numArray8[21] = (byte) 107;
    numArray8[22] = (byte) 126;
    numArray8[23] = (byte) 155;
    numArray8[24] = (byte) 243;
    numArray8[8] = (byte) 125;
    numArray8[29] = (byte) 48 /*0x30*/;
    numArray8[27] = (byte) 100;
    numArray8[46] = (byte) 225;
    numArray8[14] = (byte) 92;
    numArray8[2] = (byte) 222;
    numArray8[31 /*0x1F*/] = (byte) 212;
    numArray8[32 /*0x20*/] = (byte) 88;
    numArray8[28] = (byte) 65;
    numArray8[49] = (byte) 157;
    numArray8[54] = (byte) 238;
    numArray8[36] = (byte) 95;
    numArray8[37] = (byte) 239;
    numArray8[53] = (byte) 170;
    numArray8[39] = (byte) 99;
    numArray8[44] = (byte) 154;
    numArray8[19] = (byte) 117;
    numArray8[41] = (byte) 127 /*0x7F*/;
    numArray8[7] = (byte) 71;
    numArray8[43] = (byte) 70;
    numArray8[45] = byte.MaxValue;
    numArray8[26] = (byte) 16 /*0x10*/;
    numArray8[47] = (byte) 164;
    numArray8[48 /*0x30*/] = (byte) 126;
    numArray8[20] = (byte) 112 /*0x70*/;
    numArray8[50] = (byte) 102;
    numArray8[10] = (byte) 130;
    numArray8[3] = (byte) 43;
    numArray8[6] = (byte) 11;
    numArray8[35] = (byte) 34;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[47];
    numArray9[36] = (byte) 242;
    numArray9[17] = (byte) 191;
    numArray9[2] = (byte) 191;
    numArray9[3] = (byte) 231;
    numArray9[4] = (byte) 168;
    numArray9[9] = (byte) 156;
    numArray9[6] = (byte) 199;
    numArray9[7] = (byte) 73;
    numArray9[28] = (byte) 191;
    numArray9[14] = (byte) 14;
    numArray9[1] = (byte) 169;
    numArray9[11] = (byte) 181;
    numArray9[12] = (byte) 141;
    numArray9[13] = (byte) 69;
    numArray9[44] = (byte) 132;
    numArray9[41] = (byte) 111;
    numArray9[22] = (byte) 63 /*0x3F*/;
    numArray9[33] = (byte) 242;
    numArray9[45] = (byte) 41;
    numArray9[34] = (byte) 41;
    numArray9[20] = (byte) 229;
    numArray9[21] = (byte) 219;
    numArray9[30] = (byte) 69;
    numArray9[23] = (byte) 243;
    numArray9[24] = (byte) 23;
    numArray9[27] = (byte) 73;
    numArray9[0] = (byte) 58;
    numArray9[35] = (byte) 120;
    numArray9[15] = (byte) 99;
    numArray9[19] = (byte) 238;
    numArray9[18] = (byte) 195;
    numArray9[5] = (byte) 69;
    numArray9[46] = (byte) 105;
    numArray9[8] = (byte) 62;
    numArray9[25] = (byte) 226;
    numArray9[42] = (byte) 67;
    numArray9[37] = (byte) 22;
    numArray9[29] = (byte) 141;
    numArray9[39] = (byte) 100;
    numArray9[38] = (byte) 71;
    numArray9[40] = (byte) 226;
    numArray9[16 /*0x10*/] = (byte) 208 /*0xD0*/;
    numArray9[26] = (byte) 99;
    numArray9[43] = (byte) 23;
    numArray9[32 /*0x20*/] = (byte) 144 /*0x90*/;
    numArray9[10] = (byte) 58;
    numArray9[31 /*0x1F*/] = (byte) 253;
    byte[] numArray10 = new byte[47];
    numArray10[40] = (byte) 47;
    numArray10[42] = (byte) 122;
    numArray10[33] = (byte) 132;
    numArray10[3] = (byte) 152;
    numArray10[11] = (byte) 106;
    numArray10[5] = (byte) 145;
    numArray10[1] = (byte) 169;
    numArray10[2] = (byte) 15;
    numArray10[8] = (byte) 139;
    numArray10[9] = (byte) 94;
    numArray10[10] = (byte) 68;
    numArray10[46] = (byte) 54;
    numArray10[12] = (byte) 215;
    numArray10[19] = (byte) 78;
    numArray10[37] = (byte) 174;
    numArray10[43] = (byte) 183;
    numArray10[24] = (byte) 231;
    numArray10[14] = (byte) 179;
    numArray10[16 /*0x10*/] = (byte) 215;
    numArray10[26] = (byte) 56;
    numArray10[20] = (byte) 49;
    numArray10[21] = (byte) 189;
    numArray10[6] = (byte) 112 /*0x70*/;
    numArray10[23] = (byte) 119;
    numArray10[0] = (byte) 190;
    numArray10[25] = (byte) 76;
    numArray10[29] = (byte) 23;
    numArray10[7] = (byte) 98;
    numArray10[28] = (byte) 12;
    numArray10[15] = (byte) 246;
    numArray10[30] = (byte) 206;
    numArray10[44] = (byte) 237;
    numArray10[32 /*0x20*/] = (byte) 67;
    numArray10[36] = (byte) 170;
    numArray10[34] = (byte) 254;
    numArray10[35] = (byte) 242;
    numArray10[18] = (byte) 76;
    numArray10[13] = (byte) 92;
    numArray10[22] = (byte) 39;
    numArray10[39] = (byte) 138;
    numArray10[31 /*0x1F*/] = (byte) 68;
    numArray10[41] = (byte) 127 /*0x7F*/;
    numArray10[27] = (byte) 52;
    numArray10[17] = (byte) 96 /*0x60*/;
    numArray10[38] = (byte) 232;
    numArray10[4] = (byte) 242;
    numArray10[45] = (byte) 67;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 47);
    for (int index = 0; index < 47; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12427(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 6;
    sourceArray1[11] = (byte) 80 /*0x50*/;
    sourceArray1[2] = (byte) 52;
    sourceArray1[9] = (byte) 206;
    sourceArray1[4] = (byte) 91;
    sourceArray1[24] = (byte) 57;
    sourceArray1[18] = (byte) 95;
    sourceArray1[34] = (byte) 130;
    sourceArray1[7] = (byte) 181;
    sourceArray1[17] = (byte) 237;
    sourceArray1[10] = (byte) 192 /*0xC0*/;
    sourceArray1[0] = (byte) 200;
    sourceArray1[19] = (byte) 195;
    sourceArray1[1] = (byte) 100;
    sourceArray1[14] = (byte) 49;
    sourceArray1[15] = (byte) 138;
    sourceArray1[3] = (byte) 69;
    sourceArray1[38] = (byte) 77;
    sourceArray1[8] = (byte) 197;
    sourceArray1[32 /*0x20*/] = (byte) 79;
    sourceArray1[20] = (byte) 220;
    sourceArray1[42] = (byte) 97;
    sourceArray1[22] = (byte) 248;
    sourceArray1[23] = (byte) 161;
    sourceArray1[40] = (byte) 244;
    sourceArray1[6] = (byte) 15;
    sourceArray1[26] = (byte) 173;
    sourceArray1[44] = (byte) 234;
    sourceArray1[27] = (byte) 250;
    sourceArray1[29] = (byte) 171;
    sourceArray1[12] = (byte) 181;
    sourceArray1[31 /*0x1F*/] = (byte) 112 /*0x70*/;
    sourceArray1[21] = (byte) 54;
    sourceArray1[33] = (byte) 188;
    sourceArray1[46] = (byte) 130;
    sourceArray1[28] = (byte) 65;
    sourceArray1[36] = (byte) 194;
    sourceArray1[37] = (byte) 151;
    sourceArray1[39] = (byte) 208 /*0xD0*/;
    sourceArray1[47] = (byte) 217;
    sourceArray1[25] = (byte) 208 /*0xD0*/;
    sourceArray1[43] = (byte) 27;
    sourceArray1[35] = (byte) 15;
    sourceArray1[41] = (byte) 177;
    sourceArray1[5] = (byte) 191;
    sourceArray1[45] = (byte) 178;
    sourceArray1[13] = (byte) 159;
    sourceArray1[30] = (byte) 221;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 194,
      (byte) 69,
      (byte) 2,
      (byte) 33,
      (byte) 118,
      (byte) 94,
      (byte) 89,
      (byte) 111,
      (byte) 124,
      (byte) 103,
      (byte) 59,
      (byte) 96 /*0x60*/,
      (byte) 234,
      (byte) 30,
      (byte) 107,
      (byte) 203,
      (byte) 201,
      (byte) 24,
      (byte) 179,
      (byte) 35,
      (byte) 192 /*0xC0*/,
      (byte) 57,
      (byte) 76,
      (byte) 223,
      (byte) 47,
      (byte) 242,
      (byte) 120,
      (byte) 112 /*0x70*/,
      (byte) 48 /*0x30*/,
      (byte) 240 /*0xF0*/,
      (byte) 47,
      (byte) 4,
      (byte) 158,
      (byte) 56,
      (byte) 210,
      (byte) 178,
      (byte) 88,
      (byte) 36,
      (byte) 110,
      (byte) 235,
      (byte) 7,
      (byte) 241,
      (byte) 159,
      (byte) 18,
      (byte) 197,
      (byte) 203,
      (byte) 135,
      (byte) 122
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12428(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 137,
      (byte) 188,
      (byte) 53,
      (byte) 119,
      (byte) 97,
      (byte) 203,
      (byte) 175,
      (byte) 110,
      (byte) 196,
      (byte) 188,
      (byte) 141,
      (byte) 220,
      (byte) 133,
      (byte) 61,
      (byte) 62,
      (byte) 113,
      (byte) 114,
      (byte) 63 /*0x3F*/,
      (byte) 127 /*0x7F*/,
      (byte) 30,
      (byte) 238,
      (byte) 43,
      (byte) 43,
      (byte) 125,
      (byte) 153,
      (byte) 227,
      (byte) 222,
      (byte) 208 /*0xD0*/,
      (byte) 240 /*0xF0*/,
      (byte) 238,
      (byte) 142,
      (byte) 179,
      (byte) 29,
      (byte) 211,
      (byte) 195,
      (byte) 23,
      (byte) 110,
      (byte) 137,
      (byte) 121,
      (byte) 165,
      (byte) 130,
      (byte) 164,
      (byte) 8,
      (byte) 53,
      (byte) 23,
      (byte) 82,
      (byte) 5,
      (byte) 132
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 9,
      (byte) 138,
      (byte) 233,
      (byte) 11,
      (byte) 221,
      (byte) 119,
      (byte) 130,
      (byte) 11,
      (byte) 232,
      (byte) 107,
      (byte) 180,
      (byte) 217,
      (byte) 0,
      (byte) 61,
      (byte) 239,
      (byte) 169,
      (byte) 42,
      (byte) 68,
      (byte) 181,
      (byte) 213,
      (byte) 194,
      (byte) 67,
      (byte) 173,
      (byte) 212,
      (byte) 20,
      (byte) 26,
      (byte) 185,
      (byte) 38,
      (byte) 80 /*0x50*/,
      (byte) 96 /*0x60*/,
      (byte) 22,
      (byte) 58,
      (byte) 115,
      (byte) 57,
      (byte) 187,
      (byte) 229,
      (byte) 84,
      (byte) 237,
      (byte) 131,
      (byte) 168,
      (byte) 79,
      (byte) 22,
      (byte) 34,
      (byte) 40,
      (byte) 0,
      (byte) 83,
      (byte) 116,
      (byte) 53
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[33];
    byte[] response2 = new byte[33];
    Array.Copy((Array) sc_12411.sspq, 289, (Array) numArray2, 0, 33);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12411.sspr, 289, (Array) numArray2, 0, 33);
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

  internal static string ssp_appserver_12429()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[188];
      byte[] numArray2 = new byte[55]
      {
        (byte) 8,
        (byte) 161,
        (byte) 15,
        (byte) 101,
        (byte) 135,
        (byte) 183,
        (byte) 170,
        (byte) 251,
        (byte) 183,
        (byte) 162,
        (byte) 37,
        (byte) 50,
        (byte) 7,
        (byte) 61,
        (byte) 147,
        (byte) 83,
        (byte) 10,
        (byte) 88,
        (byte) 108,
        (byte) 224 /*0xE0*/,
        (byte) 65,
        (byte) 208 /*0xD0*/,
        (byte) 223,
        (byte) 121,
        (byte) 58,
        (byte) 154,
        (byte) 81,
        (byte) 151,
        (byte) 160 /*0xA0*/,
        (byte) 148,
        (byte) 141,
        (byte) 252,
        (byte) 177,
        (byte) 167,
        (byte) 209,
        (byte) 29,
        (byte) 162,
        (byte) 20,
        (byte) 159,
        (byte) 74,
        (byte) 62,
        (byte) 202,
        (byte) 34,
        (byte) 249,
        (byte) 36,
        (byte) 240 /*0xF0*/,
        (byte) 239,
        (byte) 41,
        (byte) 179,
        (byte) 137,
        (byte) 78,
        (byte) 248,
        (byte) 21,
        (byte) 67,
        (byte) 69
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 203,
        (byte) 165,
        (byte) 89,
        (byte) 240 /*0xF0*/,
        (byte) 6,
        (byte) 31 /*0x1F*/,
        (byte) 222,
        (byte) 125,
        (byte) 7,
        (byte) 100,
        (byte) 154,
        (byte) 135,
        (byte) 112 /*0x70*/,
        (byte) 143,
        (byte) 139,
        (byte) 72,
        (byte) 244,
        (byte) 23,
        (byte) 249,
        (byte) 214,
        (byte) 170,
        (byte) 121,
        (byte) 101,
        (byte) 18,
        (byte) 234,
        (byte) 210,
        (byte) 160 /*0xA0*/,
        (byte) 60,
        (byte) 54,
        (byte) 27,
        (byte) 210,
        (byte) 246,
        (byte) 201,
        (byte) 44,
        (byte) 143,
        (byte) 144 /*0x90*/,
        (byte) 225,
        (byte) 246,
        (byte) 2,
        (byte) 131,
        (byte) 219,
        (byte) 84,
        (byte) 254,
        (byte) 61,
        (byte) 222,
        (byte) 68,
        (byte) 179,
        (byte) 218,
        (byte) 39,
        (byte) 48 /*0x30*/,
        (byte) 190,
        (byte) 30,
        (byte) 91,
        (byte) 76,
        (byte) 12
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 150,
        (byte) 42,
        (byte) 236,
        (byte) 101,
        (byte) 18,
        (byte) 90,
        (byte) 35,
        (byte) 20,
        (byte) 41,
        (byte) 129,
        (byte) 95,
        (byte) 107,
        (byte) 117,
        (byte) 177,
        (byte) 245,
        (byte) 185,
        (byte) 33,
        (byte) 162,
        (byte) 118,
        (byte) 174,
        (byte) 229,
        (byte) 85,
        (byte) 74,
        (byte) 170,
        (byte) 40,
        (byte) 228,
        (byte) 23,
        (byte) 69,
        (byte) 149,
        (byte) 231,
        (byte) 110,
        (byte) 97,
        (byte) 153,
        (byte) 91,
        (byte) 11,
        (byte) 233,
        (byte) 178,
        (byte) 229,
        (byte) 43,
        (byte) 112 /*0x70*/,
        (byte) 254,
        (byte) 228,
        (byte) 250,
        (byte) 111,
        (byte) 143,
        (byte) 189,
        (byte) 62,
        (byte) 216,
        (byte) 88,
        (byte) 63 /*0x3F*/,
        (byte) 243,
        (byte) 160 /*0xA0*/,
        (byte) 206,
        (byte) 137,
        (byte) 220
      };
      byte[] numArray5 = new byte[55];
      numArray5[46] = (byte) 104;
      numArray5[1] = (byte) 19;
      numArray5[15] = (byte) 73;
      numArray5[3] = (byte) 142;
      numArray5[4] = (byte) 57;
      numArray5[30] = (byte) 30;
      numArray5[52] = (byte) 143;
      numArray5[7] = (byte) 40;
      numArray5[5] = (byte) 228;
      numArray5[33] = (byte) 87;
      numArray5[10] = (byte) 236;
      numArray5[24] = (byte) 66;
      numArray5[12] = (byte) 135;
      numArray5[13] = (byte) 52;
      numArray5[17] = (byte) 242;
      numArray5[26] = (byte) 158;
      numArray5[31 /*0x1F*/] = (byte) 3;
      numArray5[27] = byte.MaxValue;
      numArray5[18] = (byte) 144 /*0x90*/;
      numArray5[19] = (byte) 176 /*0xB0*/;
      numArray5[20] = (byte) 3;
      numArray5[37] = (byte) 47;
      numArray5[22] = (byte) 121;
      numArray5[23] = (byte) 225;
      numArray5[29] = (byte) 48 /*0x30*/;
      numArray5[51] = (byte) 19;
      numArray5[53] = (byte) 12;
      numArray5[41] = (byte) 120;
      numArray5[14] = (byte) 29;
      numArray5[21] = (byte) 117;
      numArray5[36] = (byte) 31 /*0x1F*/;
      numArray5[8] = (byte) 242;
      numArray5[48 /*0x30*/] = (byte) 249;
      numArray5[38] = (byte) 207;
      numArray5[34] = (byte) 35;
      numArray5[2] = (byte) 13;
      numArray5[28] = (byte) 0;
      numArray5[32 /*0x20*/] = (byte) 136;
      numArray5[6] = (byte) 142;
      numArray5[16 /*0x10*/] = (byte) 18;
      numArray5[40] = (byte) 85;
      numArray5[25] = (byte) 158;
      numArray5[35] = (byte) 127 /*0x7F*/;
      numArray5[43] = (byte) 193;
      numArray5[44] = (byte) 232;
      numArray5[45] = (byte) 47;
      numArray5[39] = (byte) 160 /*0xA0*/;
      numArray5[47] = (byte) 170;
      numArray5[11] = (byte) 103;
      numArray5[49] = (byte) 204;
      numArray5[50] = (byte) 100;
      numArray5[9] = (byte) 195;
      numArray5[0] = (byte) 98;
      numArray5[54] = (byte) 33;
      numArray5[42] = (byte) 56;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 38,
        (byte) 210,
        (byte) 136,
        (byte) 4,
        (byte) 241,
        (byte) 41,
        (byte) 78,
        (byte) 70,
        (byte) 107,
        (byte) 194,
        (byte) 153,
        (byte) 218,
        (byte) 2,
        (byte) 149,
        (byte) 148,
        (byte) 122,
        (byte) 216,
        (byte) 142,
        (byte) 156,
        (byte) 161,
        (byte) 126,
        (byte) 138,
        (byte) 127 /*0x7F*/,
        (byte) 128 /*0x80*/,
        (byte) 12,
        (byte) 159,
        (byte) 29,
        (byte) 60,
        (byte) 163,
        (byte) 202,
        (byte) 53,
        (byte) 165,
        (byte) 227,
        (byte) 3,
        (byte) 74,
        (byte) 212,
        (byte) 116,
        (byte) 41,
        (byte) 47,
        (byte) 59,
        (byte) 105,
        (byte) 22,
        (byte) 38,
        (byte) 40,
        (byte) 139,
        (byte) 87,
        (byte) 73,
        (byte) 163,
        (byte) 34,
        (byte) 174,
        (byte) 240 /*0xF0*/,
        (byte) 76,
        (byte) 62,
        (byte) 68,
        (byte) 135
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 108,
        (byte) 149,
        (byte) 178,
        (byte) 89,
        (byte) 15,
        (byte) 210,
        (byte) 71,
        (byte) 128 /*0x80*/,
        (byte) 186,
        (byte) 7,
        (byte) 51,
        (byte) 92,
        (byte) 53,
        (byte) 96 /*0x60*/,
        (byte) 145,
        (byte) 87,
        (byte) 148,
        (byte) 90,
        (byte) 85,
        (byte) 237,
        (byte) 153,
        (byte) 26,
        (byte) 228,
        (byte) 161,
        (byte) 167,
        (byte) 129,
        (byte) 28,
        (byte) 232,
        (byte) 163,
        (byte) 210,
        (byte) 233,
        (byte) 128 /*0x80*/,
        (byte) 212,
        (byte) 29,
        (byte) 90,
        (byte) 42,
        (byte) 195,
        (byte) 227,
        (byte) 58,
        (byte) 221,
        (byte) 108,
        (byte) 22,
        (byte) 173,
        (byte) 54,
        (byte) 174,
        (byte) 229,
        (byte) 27,
        (byte) 22,
        (byte) 141,
        (byte) 40,
        (byte) 132,
        (byte) 105,
        (byte) 228,
        (byte) 106,
        (byte) 83
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[23];
      numArray8[5] = (byte) 162;
      numArray8[16 /*0x10*/] = (byte) 96 /*0x60*/;
      numArray8[17] = (byte) 55;
      numArray8[11] = (byte) 75;
      numArray8[19] = (byte) 99;
      numArray8[20] = (byte) 86;
      numArray8[4] = (byte) 18;
      numArray8[0] = (byte) 7;
      numArray8[8] = (byte) 244;
      numArray8[9] = (byte) 247;
      numArray8[7] = (byte) 141;
      numArray8[2] = (byte) 81;
      numArray8[1] = (byte) 247;
      numArray8[13] = (byte) 6;
      numArray8[14] = (byte) 175;
      numArray8[15] = (byte) 94;
      numArray8[10] = byte.MaxValue;
      numArray8[12] = (byte) 192 /*0xC0*/;
      numArray8[18] = (byte) 104;
      numArray8[6] = (byte) 160 /*0xA0*/;
      numArray8[3] = (byte) 11;
      numArray8[21] = (byte) 245;
      numArray8[22] = (byte) 180;
      byte[] numArray9 = new byte[23]
      {
        (byte) 136,
        (byte) 132,
        (byte) 80 /*0x50*/,
        (byte) 187,
        (byte) 221,
        (byte) 221,
        (byte) 16 /*0x10*/,
        (byte) 26,
        (byte) 141,
        (byte) 26,
        (byte) 17,
        (byte) 203,
        (byte) 102,
        (byte) 185,
        (byte) 73,
        (byte) 42,
        (byte) 19,
        (byte) 76,
        (byte) 44,
        (byte) 59,
        (byte) 29,
        (byte) 202,
        (byte) 131
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[188];
    byte[] numArray11 = new byte[55]
    {
      (byte) 237,
      (byte) 221,
      (byte) 12,
      (byte) 31 /*0x1F*/,
      (byte) 56,
      (byte) 253,
      (byte) 17,
      (byte) 66,
      (byte) 252,
      (byte) 145,
      (byte) 248,
      (byte) 174,
      (byte) 198,
      (byte) 164,
      (byte) 141,
      (byte) 87,
      (byte) 131,
      (byte) 157,
      (byte) 62,
      (byte) 154,
      (byte) 125,
      (byte) 129,
      (byte) 48 /*0x30*/,
      (byte) 224 /*0xE0*/,
      (byte) 244,
      (byte) 222,
      (byte) 186,
      (byte) 94,
      (byte) 84,
      (byte) 11,
      (byte) 34,
      (byte) 184,
      (byte) 76,
      (byte) 153,
      (byte) 251,
      (byte) 137,
      (byte) 146,
      (byte) 92,
      (byte) 117,
      (byte) 131,
      (byte) 90,
      (byte) 238,
      (byte) 225,
      (byte) 158,
      (byte) 95,
      (byte) 197,
      (byte) 190,
      (byte) 247,
      (byte) 9,
      (byte) 8,
      (byte) 198,
      (byte) 186,
      (byte) 35,
      (byte) 14,
      (byte) 194
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 109,
      (byte) 97,
      (byte) 249,
      (byte) 51,
      (byte) 156,
      (byte) 231,
      (byte) 30,
      (byte) 73,
      (byte) 64 /*0x40*/,
      (byte) 67,
      (byte) 238,
      (byte) 16 /*0x10*/,
      (byte) 185,
      (byte) 75,
      (byte) 169,
      (byte) 200,
      (byte) 166,
      (byte) 229,
      (byte) 106,
      (byte) 66,
      (byte) 205,
      (byte) 147,
      (byte) 195,
      (byte) 170,
      (byte) 23,
      (byte) 22,
      (byte) 9,
      (byte) 122,
      (byte) 217,
      (byte) 248,
      (byte) 122,
      (byte) 136,
      (byte) 42,
      (byte) 136,
      (byte) 77,
      (byte) 164,
      (byte) 0,
      (byte) 53,
      (byte) 53,
      (byte) 39,
      (byte) 221,
      (byte) 109,
      (byte) 165,
      (byte) 193,
      (byte) 145,
      (byte) 254,
      (byte) 246,
      (byte) 104,
      (byte) 7,
      (byte) 35,
      (byte) 53,
      (byte) 244,
      (byte) 60,
      (byte) 103,
      (byte) 196
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 44,
      (byte) 42,
      (byte) 90,
      (byte) 66,
      (byte) 179,
      (byte) 238,
      (byte) 240 /*0xF0*/,
      (byte) 111,
      (byte) 63 /*0x3F*/,
      (byte) 97,
      (byte) 45,
      (byte) 169,
      (byte) 3,
      (byte) 197,
      (byte) 38,
      (byte) 40,
      (byte) 187,
      (byte) 63 /*0x3F*/,
      (byte) 143,
      (byte) 159,
      (byte) 146,
      (byte) 128 /*0x80*/,
      (byte) 46,
      (byte) 31 /*0x1F*/,
      (byte) 195,
      (byte) 85,
      (byte) 75,
      (byte) 68,
      (byte) 103,
      (byte) 149,
      (byte) 62,
      (byte) 214,
      (byte) 143,
      (byte) 221,
      (byte) 101,
      (byte) 50,
      (byte) 27,
      (byte) 65,
      (byte) 167,
      (byte) 0,
      (byte) 241,
      (byte) 47,
      (byte) 31 /*0x1F*/,
      (byte) 99,
      (byte) 145,
      (byte) 77,
      (byte) 6,
      (byte) 66,
      (byte) 160 /*0xA0*/,
      (byte) 146,
      (byte) 104,
      (byte) 206,
      (byte) 51,
      (byte) 204,
      (byte) 26
    };
    byte[] numArray14 = new byte[55]
    {
      (byte) 3,
      (byte) 44,
      (byte) 131,
      (byte) 200,
      (byte) 195,
      (byte) 241,
      (byte) 41,
      (byte) 115,
      (byte) 15,
      (byte) 246,
      (byte) 171,
      (byte) 170,
      (byte) 234,
      (byte) 224 /*0xE0*/,
      (byte) 206,
      (byte) 49,
      (byte) 253,
      (byte) 133,
      (byte) 211,
      (byte) 224 /*0xE0*/,
      (byte) 181,
      (byte) 41,
      (byte) 148,
      (byte) 178,
      (byte) 164,
      (byte) 86,
      (byte) 193,
      (byte) 165,
      (byte) 207,
      (byte) 185,
      (byte) 243,
      (byte) 185,
      (byte) 64 /*0x40*/,
      (byte) 76,
      (byte) 188,
      (byte) 185,
      (byte) 27,
      (byte) 115,
      (byte) 200,
      (byte) 235,
      (byte) 98,
      (byte) 176 /*0xB0*/,
      (byte) 184,
      (byte) 86,
      (byte) 162,
      (byte) 128 /*0x80*/,
      (byte) 90,
      (byte) 69,
      (byte) 11,
      (byte) 98,
      (byte) 52,
      (byte) 214,
      (byte) 167,
      (byte) 200,
      (byte) 223
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 58,
      (byte) 126,
      (byte) 93,
      (byte) 162,
      (byte) 171,
      (byte) 8,
      (byte) 170,
      (byte) 35,
      (byte) 86,
      (byte) 25,
      (byte) 37,
      (byte) 96 /*0x60*/,
      (byte) 244,
      (byte) 13,
      (byte) 121,
      (byte) 152,
      (byte) 143,
      (byte) 174,
      (byte) 99,
      (byte) 97,
      (byte) 202,
      (byte) 142,
      (byte) 117,
      (byte) 78,
      (byte) 8,
      (byte) 164,
      (byte) 212,
      (byte) 45,
      (byte) 111,
      (byte) 163,
      (byte) 194,
      (byte) 194,
      (byte) 186,
      (byte) 53,
      (byte) 224 /*0xE0*/,
      (byte) 194,
      (byte) 73,
      (byte) 93,
      (byte) 69,
      (byte) 38,
      (byte) 117,
      (byte) 64 /*0x40*/,
      (byte) 58,
      (byte) 52,
      (byte) 224 /*0xE0*/,
      (byte) 226,
      (byte) 171,
      (byte) 133,
      (byte) 238,
      (byte) 53,
      (byte) 164,
      (byte) 30,
      (byte) 183,
      (byte) 74,
      (byte) 49
    };
    byte[] numArray16 = new byte[55];
    numArray16[19] = (byte) 98;
    numArray16[1] = (byte) 122;
    numArray16[0] = (byte) 128 /*0x80*/;
    numArray16[6] = (byte) 183;
    numArray16[4] = (byte) 55;
    numArray16[5] = (byte) 79;
    numArray16[42] = (byte) 41;
    numArray16[3] = (byte) 146;
    numArray16[8] = (byte) 96 /*0x60*/;
    numArray16[23] = (byte) 180;
    numArray16[10] = (byte) 32 /*0x20*/;
    numArray16[11] = (byte) 100;
    numArray16[12] = (byte) 215;
    numArray16[13] = (byte) 142;
    numArray16[14] = byte.MaxValue;
    numArray16[40] = (byte) 229;
    numArray16[43] = (byte) 64 /*0x40*/;
    numArray16[35] = (byte) 52;
    numArray16[31 /*0x1F*/] = (byte) 235;
    numArray16[37] = (byte) 129;
    numArray16[52] = (byte) 133;
    numArray16[29] = (byte) 69;
    numArray16[18] = (byte) 59;
    numArray16[30] = (byte) 0;
    numArray16[24] = (byte) 162;
    numArray16[25] = (byte) 70;
    numArray16[54] = (byte) 150;
    numArray16[27] = (byte) 221;
    numArray16[20] = (byte) 154;
    numArray16[17] = (byte) 216;
    numArray16[22] = (byte) 73;
    numArray16[44] = (byte) 84;
    numArray16[32 /*0x20*/] = (byte) 235;
    numArray16[49] = (byte) 112 /*0x70*/;
    numArray16[2] = (byte) 206;
    numArray16[33] = (byte) 26;
    numArray16[36] = (byte) 156;
    numArray16[21] = (byte) 226;
    numArray16[38] = (byte) 38;
    numArray16[39] = (byte) 182;
    numArray16[9] = (byte) 57;
    numArray16[15] = (byte) 148;
    numArray16[16 /*0x10*/] = (byte) 151;
    numArray16[26] = (byte) 11;
    numArray16[34] = (byte) 26;
    numArray16[45] = (byte) 183;
    numArray16[48 /*0x30*/] = (byte) 112 /*0x70*/;
    numArray16[47] = (byte) 206;
    numArray16[41] = (byte) 55;
    numArray16[28] = (byte) 251;
    numArray16[50] = (byte) 129;
    numArray16[51] = (byte) 197;
    numArray16[7] = (byte) 183;
    numArray16[53] = (byte) 227;
    numArray16[46] = (byte) 60;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[23]
    {
      (byte) 160 /*0xA0*/,
      (byte) 115,
      (byte) 14,
      (byte) 228,
      (byte) 62,
      (byte) 121,
      (byte) 180,
      (byte) 180,
      (byte) 87,
      (byte) 53,
      (byte) 186,
      (byte) 247,
      (byte) 253,
      (byte) 21,
      (byte) 109,
      (byte) 43,
      (byte) 72,
      (byte) 244,
      (byte) 254,
      (byte) 203,
      (byte) 101,
      (byte) 240 /*0xF0*/,
      (byte) 233
    };
    byte[] numArray18 = new byte[23];
    numArray18[21] = (byte) 159;
    numArray18[8] = (byte) 111;
    numArray18[22] = (byte) 230;
    numArray18[3] = (byte) 152;
    numArray18[1] = (byte) 238;
    numArray18[9] = (byte) 248;
    numArray18[17] = (byte) 126;
    numArray18[7] = (byte) 46;
    numArray18[13] = (byte) 84;
    numArray18[11] = (byte) 70;
    numArray18[10] = (byte) 195;
    numArray18[12] = (byte) 179;
    numArray18[14] = (byte) 106;
    numArray18[0] = (byte) 188;
    numArray18[18] = (byte) 170;
    numArray18[15] = (byte) 38;
    numArray18[2] = (byte) 245;
    numArray18[6] = (byte) 79;
    numArray18[20] = (byte) 93;
    numArray18[19] = (byte) 154;
    numArray18[16 /*0x10*/] = (byte) 243;
    numArray18[5] = (byte) 68;
    numArray18[4] = (byte) 139;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 23);
    for (int index = 0; index < 23; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_12430()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[159];
      byte[] numArray2 = new byte[55];
      numArray2[25] = (byte) 61;
      numArray2[52] = (byte) 85;
      numArray2[2] = (byte) 168;
      numArray2[3] = (byte) 141;
      numArray2[39] = (byte) 233;
      numArray2[5] = (byte) 176 /*0xB0*/;
      numArray2[38] = (byte) 144 /*0x90*/;
      numArray2[7] = (byte) 2;
      numArray2[36] = (byte) 181;
      numArray2[32 /*0x20*/] = (byte) 134;
      numArray2[10] = (byte) 36;
      numArray2[13] = (byte) 199;
      numArray2[12] = (byte) 198;
      numArray2[50] = (byte) 69;
      numArray2[53] = (byte) 232;
      numArray2[45] = (byte) 171;
      numArray2[6] = (byte) 138;
      numArray2[0] = (byte) 50;
      numArray2[18] = (byte) 123;
      numArray2[19] = (byte) 28;
      numArray2[20] = (byte) 252;
      numArray2[30] = (byte) 205;
      numArray2[28] = (byte) 217;
      numArray2[44] = (byte) 238;
      numArray2[14] = (byte) 36;
      numArray2[21] = (byte) 57;
      numArray2[26] = (byte) 202;
      numArray2[27] = (byte) 226;
      numArray2[43] = (byte) 129;
      numArray2[24] = (byte) 248;
      numArray2[16 /*0x10*/] = (byte) 27;
      numArray2[23] = (byte) 235;
      numArray2[9] = (byte) 55;
      numArray2[33] = (byte) 99;
      numArray2[34] = (byte) 93;
      numArray2[35] = (byte) 168;
      numArray2[46] = (byte) 97;
      numArray2[37] = (byte) 78;
      numArray2[31 /*0x1F*/] = (byte) 131;
      numArray2[17] = (byte) 10;
      numArray2[40] = (byte) 198;
      numArray2[41] = (byte) 45;
      numArray2[4] = (byte) 97;
      numArray2[11] = (byte) 40;
      numArray2[42] = (byte) 31 /*0x1F*/;
      numArray2[22] = (byte) 59;
      numArray2[29] = (byte) 148;
      numArray2[47] = (byte) 95;
      numArray2[8] = (byte) 238;
      numArray2[49] = (byte) 214;
      numArray2[1] = (byte) 105;
      numArray2[51] = (byte) 47;
      numArray2[48 /*0x30*/] = (byte) 226;
      numArray2[15] = (byte) 92;
      numArray2[54] = (byte) 142;
      byte[] numArray3 = new byte[55];
      numArray3[41] = (byte) 149;
      numArray3[3] = (byte) 41;
      numArray3[2] = (byte) 149;
      numArray3[51] = (byte) 61;
      numArray3[47] = (byte) 170;
      numArray3[5] = (byte) 194;
      numArray3[22] = byte.MaxValue;
      numArray3[7] = (byte) 196;
      numArray3[8] = (byte) 254;
      numArray3[9] = (byte) 119;
      numArray3[43] = (byte) 144 /*0x90*/;
      numArray3[11] = (byte) 233;
      numArray3[37] = (byte) 245;
      numArray3[13] = (byte) 89;
      numArray3[34] = (byte) 39;
      numArray3[0] = (byte) 90;
      numArray3[50] = (byte) 172;
      numArray3[17] = (byte) 147;
      numArray3[18] = (byte) 141;
      numArray3[19] = (byte) 0;
      numArray3[36] = (byte) 245;
      numArray3[24] = (byte) 85;
      numArray3[27] = (byte) 143;
      numArray3[23] = (byte) 88;
      numArray3[52] = (byte) 208 /*0xD0*/;
      numArray3[25] = (byte) 139;
      numArray3[26] = (byte) 54;
      numArray3[20] = (byte) 45;
      numArray3[15] = (byte) 144 /*0x90*/;
      numArray3[10] = (byte) 209;
      numArray3[31 /*0x1F*/] = (byte) 7;
      numArray3[32 /*0x20*/] = (byte) 51;
      numArray3[29] = (byte) 47;
      numArray3[21] = (byte) 188;
      numArray3[1] = (byte) 133;
      numArray3[35] = (byte) 74;
      numArray3[44] = (byte) 191;
      numArray3[6] = (byte) 150;
      numArray3[30] = (byte) 150;
      numArray3[38] = (byte) 191;
      numArray3[40] = (byte) 29;
      numArray3[39] = (byte) 164;
      numArray3[42] = (byte) 77;
      numArray3[12] = (byte) 125;
      numArray3[14] = (byte) 54;
      numArray3[28] = byte.MaxValue;
      numArray3[46] = (byte) 162;
      numArray3[33] = (byte) 17;
      numArray3[48 /*0x30*/] = (byte) 30;
      numArray3[45] = (byte) 225;
      numArray3[4] = (byte) 41;
      numArray3[49] = (byte) 28;
      numArray3[16 /*0x10*/] = (byte) 191;
      numArray3[53] = (byte) 82;
      numArray3[54] = (byte) 183;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[46] = (byte) 102;
      numArray4[1] = (byte) 4;
      numArray4[41] = (byte) 102;
      numArray4[49] = (byte) 191;
      numArray4[4] = (byte) 16 /*0x10*/;
      numArray4[5] = (byte) 82;
      numArray4[34] = (byte) 36;
      numArray4[39] = (byte) 140;
      numArray4[0] = (byte) 18;
      numArray4[9] = (byte) 245;
      numArray4[21] = (byte) 211;
      numArray4[26] = (byte) 82;
      numArray4[23] = (byte) 105;
      numArray4[2] = (byte) 108;
      numArray4[6] = (byte) 54;
      numArray4[15] = (byte) 233;
      numArray4[37] = (byte) 132;
      numArray4[17] = (byte) 196;
      numArray4[18] = (byte) 234;
      numArray4[19] = (byte) 157;
      numArray4[35] = (byte) 109;
      numArray4[14] = (byte) 160 /*0xA0*/;
      numArray4[22] = (byte) 59;
      numArray4[12] = (byte) 253;
      numArray4[24] = (byte) 134;
      numArray4[25] = (byte) 52;
      numArray4[20] = (byte) 86;
      numArray4[53] = (byte) 236;
      numArray4[28] = (byte) 62;
      numArray4[38] = (byte) 100;
      numArray4[30] = (byte) 180;
      numArray4[31 /*0x1F*/] = (byte) 32 /*0x20*/;
      numArray4[32 /*0x20*/] = (byte) 252;
      numArray4[7] = (byte) 229;
      numArray4[43] = (byte) 17;
      numArray4[40] = (byte) 129;
      numArray4[27] = (byte) 112 /*0x70*/;
      numArray4[8] = (byte) 7;
      numArray4[36] = (byte) 183;
      numArray4[10] = (byte) 118;
      numArray4[11] = (byte) 127 /*0x7F*/;
      numArray4[33] = (byte) 49;
      numArray4[42] = (byte) 211;
      numArray4[52] = (byte) 10;
      numArray4[44] = (byte) 252;
      numArray4[13] = (byte) 119;
      numArray4[29] = (byte) 54;
      numArray4[47] = (byte) 9;
      numArray4[48 /*0x30*/] = (byte) 196;
      numArray4[3] = (byte) 4;
      numArray4[50] = (byte) 47;
      numArray4[45] = (byte) 112 /*0x70*/;
      numArray4[51] = (byte) 148;
      numArray4[16 /*0x10*/] = (byte) 5;
      numArray4[54] = (byte) 124;
      byte[] numArray5 = new byte[55]
      {
        (byte) 223,
        (byte) 25,
        (byte) 98,
        (byte) 136,
        (byte) 44,
        (byte) 244,
        (byte) 223,
        (byte) 132,
        (byte) 190,
        (byte) 96 /*0x60*/,
        (byte) 94,
        (byte) 78,
        (byte) 188,
        (byte) 25,
        (byte) 127 /*0x7F*/,
        (byte) 6,
        (byte) 16 /*0x10*/,
        (byte) 253,
        (byte) 224 /*0xE0*/,
        (byte) 1,
        (byte) 225,
        (byte) 129,
        (byte) 87,
        (byte) 189,
        (byte) 251,
        (byte) 186,
        (byte) 175,
        (byte) 96 /*0x60*/,
        (byte) 132,
        (byte) 47,
        (byte) 169,
        (byte) 210,
        (byte) 148,
        (byte) 27,
        (byte) 59,
        (byte) 180,
        (byte) 40,
        (byte) 60,
        (byte) 68,
        (byte) 252,
        (byte) 147,
        (byte) 25,
        (byte) 64 /*0x40*/,
        (byte) 172,
        (byte) 190,
        (byte) 20,
        (byte) 111,
        (byte) 75,
        (byte) 9,
        (byte) 99,
        (byte) 147,
        (byte) 89,
        (byte) 193,
        (byte) 88,
        (byte) 148
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[49]
      {
        (byte) 149,
        (byte) 104,
        (byte) 13,
        (byte) 182,
        (byte) 25,
        (byte) 39,
        (byte) 202,
        (byte) 32 /*0x20*/,
        (byte) 63 /*0x3F*/,
        (byte) 114,
        (byte) 22,
        (byte) 109,
        (byte) 141,
        (byte) 126,
        (byte) 122,
        (byte) 215,
        (byte) 209,
        (byte) 95,
        (byte) 48 /*0x30*/,
        (byte) 254,
        (byte) 89,
        (byte) 199,
        (byte) 40,
        (byte) 51,
        (byte) 147,
        (byte) 26,
        (byte) 242,
        (byte) 89,
        (byte) 236,
        (byte) 105,
        (byte) 63 /*0x3F*/,
        (byte) 40,
        (byte) 167,
        (byte) 6,
        (byte) 31 /*0x1F*/,
        (byte) 157,
        (byte) 123,
        (byte) 213,
        (byte) 161,
        (byte) 152,
        (byte) 55,
        (byte) 176 /*0xB0*/,
        (byte) 102,
        (byte) 122,
        (byte) 131,
        (byte) 72,
        (byte) 3,
        (byte) 14,
        (byte) 51
      };
      byte[] numArray7 = new byte[49]
      {
        (byte) 67,
        (byte) 17,
        (byte) 155,
        (byte) 78,
        (byte) 144 /*0x90*/,
        (byte) 31 /*0x1F*/,
        (byte) 53,
        (byte) 187,
        (byte) 14,
        (byte) 2,
        (byte) 246,
        (byte) 209,
        (byte) 54,
        (byte) 12,
        (byte) 185,
        (byte) 83,
        (byte) 244,
        (byte) 235,
        (byte) 230,
        (byte) 209,
        (byte) 182,
        (byte) 144 /*0x90*/,
        (byte) 188,
        (byte) 213,
        (byte) 51,
        (byte) 233,
        (byte) 97,
        (byte) 37,
        (byte) 34,
        (byte) 87,
        (byte) 155,
        (byte) 95,
        (byte) 108,
        (byte) 5,
        (byte) 201,
        (byte) 61,
        (byte) 165,
        (byte) 155,
        (byte) 27,
        (byte) 165,
        (byte) 161,
        (byte) 195,
        (byte) 197,
        (byte) 174,
        (byte) 251,
        (byte) 29,
        (byte) 191,
        (byte) 135,
        (byte) 125
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[159];
    byte[] numArray9 = new byte[55]
    {
      (byte) 91,
      (byte) 63 /*0x3F*/,
      (byte) 69,
      (byte) 205,
      (byte) 64 /*0x40*/,
      (byte) 218,
      (byte) 35,
      (byte) 254,
      (byte) 52,
      (byte) 113,
      (byte) 51,
      (byte) 214,
      (byte) 51,
      (byte) 238,
      (byte) 197,
      (byte) 135,
      (byte) 95,
      (byte) 220,
      (byte) 91,
      (byte) 251,
      (byte) 189,
      (byte) 151,
      (byte) 199,
      (byte) 169,
      (byte) 96 /*0x60*/,
      (byte) 173,
      (byte) 175,
      (byte) 75,
      (byte) 23,
      (byte) 228,
      (byte) 37,
      (byte) 143,
      (byte) 141,
      (byte) 206,
      (byte) 204,
      (byte) 103,
      (byte) 193,
      (byte) 81,
      (byte) 93,
      (byte) 161,
      (byte) 178,
      (byte) 162,
      (byte) 100,
      (byte) 198,
      (byte) 124,
      (byte) 144 /*0x90*/,
      (byte) 71,
      (byte) 149,
      (byte) 130,
      (byte) 124,
      (byte) 175,
      (byte) 86,
      (byte) 1,
      (byte) 223,
      (byte) 179
    };
    byte[] numArray10 = new byte[55];
    numArray10[0] = (byte) 95;
    numArray10[1] = (byte) 218;
    numArray10[45] = (byte) 232;
    numArray10[51] = (byte) 191;
    numArray10[26] = (byte) 211;
    numArray10[5] = (byte) 122;
    numArray10[31 /*0x1F*/] = (byte) 252;
    numArray10[7] = (byte) 151;
    numArray10[27] = (byte) 30;
    numArray10[44] = (byte) 37;
    numArray10[10] = (byte) 50;
    numArray10[23] = (byte) 170;
    numArray10[12] = (byte) 190;
    numArray10[11] = (byte) 169;
    numArray10[14] = (byte) 19;
    numArray10[29] = (byte) 139;
    numArray10[16 /*0x10*/] = (byte) 132;
    numArray10[35] = (byte) 85;
    numArray10[30] = (byte) 209;
    numArray10[19] = (byte) 183;
    numArray10[46] = (byte) 36;
    numArray10[17] = (byte) 115;
    numArray10[22] = (byte) 143;
    numArray10[13] = (byte) 15;
    numArray10[24] = (byte) 4;
    numArray10[2] = (byte) 46;
    numArray10[43] = (byte) 228;
    numArray10[52] = (byte) 24;
    numArray10[42] = (byte) 52;
    numArray10[8] = (byte) 202;
    numArray10[28] = (byte) 195;
    numArray10[36] = (byte) 123;
    numArray10[47] = (byte) 190;
    numArray10[4] = (byte) 63 /*0x3F*/;
    numArray10[20] = (byte) 87;
    numArray10[18] = (byte) 2;
    numArray10[48 /*0x30*/] = (byte) 63 /*0x3F*/;
    numArray10[37] = (byte) 61;
    numArray10[38] = (byte) 143;
    numArray10[54] = (byte) 55;
    numArray10[40] = (byte) 236;
    numArray10[41] = (byte) 215;
    numArray10[3] = (byte) 239;
    numArray10[32 /*0x20*/] = (byte) 247;
    numArray10[49] = (byte) 181;
    numArray10[21] = (byte) 70;
    numArray10[39] = (byte) 215;
    numArray10[15] = (byte) 126;
    numArray10[9] = (byte) 214;
    numArray10[25] = (byte) 91;
    numArray10[50] = (byte) 108;
    numArray10[34] = (byte) 241;
    numArray10[33] = (byte) 250;
    numArray10[6] = (byte) 111;
    numArray10[53] = (byte) 131;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 111,
      (byte) 156,
      (byte) 43,
      (byte) 16 /*0x10*/,
      (byte) 15,
      (byte) 76,
      (byte) 26,
      (byte) 124,
      (byte) 22,
      (byte) 155,
      (byte) 74,
      (byte) 25,
      (byte) 110,
      (byte) 160 /*0xA0*/,
      (byte) 41,
      (byte) 179,
      (byte) 67,
      (byte) 111,
      (byte) 95,
      (byte) 30,
      (byte) 133,
      (byte) 85,
      (byte) 137,
      (byte) 132,
      (byte) 166,
      (byte) 164,
      (byte) 228,
      (byte) 147,
      (byte) 247,
      (byte) 71,
      (byte) 23,
      (byte) 130,
      (byte) 119,
      (byte) 119,
      (byte) 182,
      (byte) 71,
      (byte) 159,
      (byte) 102,
      (byte) 202,
      (byte) 110,
      (byte) 249,
      (byte) 254,
      (byte) 192 /*0xC0*/,
      (byte) 172,
      (byte) 151,
      (byte) 90,
      (byte) 181,
      (byte) 21,
      (byte) 119,
      (byte) 36,
      (byte) 2,
      (byte) 38,
      (byte) 56,
      (byte) 113,
      (byte) 242
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 199,
      (byte) 199,
      (byte) 14,
      (byte) 13,
      (byte) 149,
      (byte) 51,
      (byte) 90,
      (byte) 108,
      (byte) 28,
      (byte) 39,
      (byte) 81,
      (byte) 121,
      (byte) 84,
      (byte) 57,
      (byte) 229,
      (byte) 169,
      (byte) 36,
      (byte) 215,
      (byte) 249,
      (byte) 115,
      (byte) 116,
      (byte) 38,
      (byte) 92,
      (byte) 210,
      (byte) 219,
      (byte) 216,
      (byte) 231,
      (byte) 97,
      (byte) 203,
      (byte) 185,
      (byte) 78,
      (byte) 73,
      (byte) 112 /*0x70*/,
      (byte) 229,
      (byte) 206,
      (byte) 127 /*0x7F*/,
      (byte) 219,
      (byte) 107,
      (byte) 17,
      (byte) 4,
      (byte) 158,
      (byte) 40,
      (byte) 144 /*0x90*/,
      (byte) 25,
      (byte) 41,
      (byte) 190,
      (byte) 247,
      (byte) 129,
      (byte) 192 /*0xC0*/,
      (byte) 175,
      (byte) 2,
      (byte) 215,
      (byte) 37,
      (byte) 98,
      (byte) 153
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[49]
    {
      (byte) 167,
      (byte) 106,
      (byte) 107,
      (byte) 104,
      (byte) 135,
      (byte) 133,
      (byte) 207,
      (byte) 131,
      (byte) 199,
      (byte) 161,
      (byte) 247,
      (byte) 234,
      (byte) 226,
      (byte) 20,
      (byte) 221,
      (byte) 165,
      (byte) 241,
      (byte) 37,
      (byte) 134,
      (byte) 148,
      (byte) 220,
      (byte) 237,
      (byte) 75,
      (byte) 238,
      (byte) 207,
      (byte) 27,
      (byte) 210,
      (byte) 68,
      (byte) 240 /*0xF0*/,
      (byte) 74,
      (byte) 6,
      (byte) 132,
      (byte) 156,
      (byte) 52,
      (byte) 248,
      (byte) 252,
      (byte) 94,
      (byte) 6,
      (byte) 114,
      (byte) 121,
      (byte) 241,
      (byte) 40,
      (byte) 207,
      (byte) 185,
      (byte) 190,
      (byte) 43,
      (byte) 149,
      (byte) 15,
      (byte) 37
    };
    byte[] numArray14 = new byte[49]
    {
      (byte) 46,
      (byte) 197,
      (byte) 29,
      (byte) 101,
      (byte) 159,
      (byte) 55,
      (byte) 28,
      (byte) 121,
      (byte) 99,
      (byte) 89,
      (byte) 150,
      (byte) 137,
      (byte) 99,
      (byte) 159,
      (byte) 38,
      (byte) 165,
      (byte) 28,
      (byte) 157,
      (byte) 172,
      (byte) 87,
      (byte) 70,
      (byte) 90,
      (byte) 11,
      (byte) 72,
      (byte) 244,
      (byte) 66,
      (byte) 159,
      (byte) 123,
      (byte) 189,
      (byte) 53,
      (byte) 53,
      (byte) 113,
      (byte) 125,
      (byte) 143,
      (byte) 70,
      (byte) 106,
      (byte) 171,
      (byte) 188,
      (byte) 120,
      (byte) 143,
      (byte) 172,
      (byte) 118,
      (byte) 209,
      (byte) 68,
      (byte) 211,
      (byte) 120,
      (byte) 228,
      (byte) 100,
      (byte) 171
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 49);
    for (int index = 0; index < 49; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
