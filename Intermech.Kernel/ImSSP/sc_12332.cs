// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12332
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12332
{
  private static byte[] sspq = new byte[312]
  {
    (byte) 112 /*0x70*/,
    (byte) 174,
    (byte) 158,
    (byte) 220,
    (byte) 170,
    (byte) 58,
    (byte) 88,
    (byte) 85,
    (byte) 220,
    (byte) 38,
    (byte) 132,
    (byte) 22,
    (byte) 179,
    (byte) 142,
    (byte) 14,
    (byte) 224 /*0xE0*/,
    (byte) 52,
    (byte) 154,
    (byte) 167,
    (byte) 206,
    (byte) 154,
    (byte) 214,
    (byte) 216,
    (byte) 123,
    (byte) 10,
    (byte) 174,
    (byte) 45,
    (byte) 55,
    (byte) 65,
    (byte) 240 /*0xF0*/,
    (byte) 84,
    (byte) 61,
    (byte) 123,
    (byte) 30,
    (byte) 102,
    (byte) 137,
    (byte) 98,
    (byte) 59,
    (byte) 37,
    (byte) 105,
    (byte) 74,
    (byte) 44,
    (byte) 178,
    (byte) 148,
    (byte) 100,
    (byte) 238,
    (byte) 109,
    (byte) 134,
    (byte) 130,
    (byte) 64 /*0x40*/,
    (byte) 19,
    (byte) 31 /*0x1F*/,
    (byte) 136,
    (byte) 111,
    (byte) 101,
    (byte) 142,
    (byte) 46,
    (byte) 138,
    (byte) 191,
    (byte) 156,
    (byte) 173,
    (byte) 145,
    (byte) 28,
    (byte) 220,
    (byte) 58,
    (byte) 154,
    (byte) 108,
    (byte) 144 /*0x90*/,
    (byte) 67,
    (byte) 114,
    (byte) 121,
    (byte) 155,
    (byte) 113,
    (byte) 152,
    (byte) 10,
    (byte) 130,
    (byte) 98,
    (byte) 161,
    (byte) 231,
    (byte) 97,
    (byte) 79,
    (byte) 240 /*0xF0*/,
    (byte) 52,
    (byte) 194,
    (byte) 155,
    (byte) 35,
    (byte) 61,
    (byte) 113,
    (byte) 49,
    (byte) 192 /*0xC0*/,
    (byte) 64 /*0x40*/,
    (byte) 84,
    (byte) 112 /*0x70*/,
    (byte) 85,
    (byte) 183,
    (byte) 25,
    (byte) 219,
    (byte) 120,
    (byte) 80 /*0x50*/,
    (byte) 236,
    (byte) 123,
    (byte) 172,
    (byte) 238,
    (byte) 109,
    (byte) 126,
    (byte) 97,
    (byte) 142,
    (byte) 65,
    (byte) 46,
    (byte) 147,
    (byte) 51,
    (byte) 91,
    (byte) 37,
    (byte) 111,
    (byte) 125,
    (byte) 217,
    (byte) 15,
    (byte) 56,
    (byte) 94,
    (byte) 29,
    (byte) 17,
    (byte) 198,
    (byte) 163,
    (byte) 45,
    (byte) 149,
    (byte) 60,
    (byte) 244,
    (byte) 147,
    (byte) 181,
    (byte) 168,
    (byte) 241,
    (byte) 38,
    (byte) 253,
    (byte) 226,
    (byte) 149,
    (byte) 230,
    (byte) 236,
    (byte) 76,
    (byte) 95,
    (byte) 40,
    (byte) 22,
    (byte) 20,
    (byte) 192 /*0xC0*/,
    (byte) 88,
    (byte) 147,
    (byte) 94,
    (byte) 7,
    (byte) 229,
    (byte) 89,
    (byte) 65,
    (byte) 123,
    (byte) 141,
    (byte) 0,
    (byte) 27,
    (byte) 132,
    (byte) 234,
    (byte) 84,
    (byte) 221,
    (byte) 110,
    (byte) 12,
    (byte) 228,
    (byte) 171,
    (byte) 165,
    (byte) 1,
    (byte) 196,
    (byte) 75,
    (byte) 193,
    (byte) 222,
    (byte) 151,
    (byte) 14,
    (byte) 4,
    (byte) 174,
    (byte) 187,
    (byte) 66,
    (byte) 85,
    (byte) 0,
    (byte) 10,
    (byte) 5,
    (byte) 40,
    (byte) 69,
    (byte) 16 /*0x10*/,
    (byte) 161,
    (byte) 92,
    (byte) 18,
    (byte) 141,
    (byte) 138,
    (byte) 118,
    (byte) 32 /*0x20*/,
    (byte) 151,
    (byte) 37,
    (byte) 189,
    (byte) 97,
    (byte) 42,
    (byte) 47,
    (byte) 56,
    (byte) 7,
    (byte) 146,
    (byte) 229,
    (byte) 44,
    (byte) 26,
    (byte) 99,
    (byte) 138,
    (byte) 129,
    (byte) 78,
    (byte) 9,
    (byte) 180,
    (byte) 75,
    (byte) 99,
    (byte) 92,
    (byte) 253,
    (byte) 228,
    (byte) 159,
    (byte) 198,
    (byte) 36,
    (byte) 242,
    (byte) 93,
    (byte) 110,
    (byte) 54,
    (byte) 25,
    (byte) 163,
    (byte) 204,
    (byte) 202,
    (byte) 168,
    (byte) 215,
    (byte) 115,
    (byte) 157,
    (byte) 181,
    (byte) 13,
    (byte) 121,
    (byte) 3,
    (byte) 50,
    (byte) 96 /*0x60*/,
    (byte) 80 /*0x50*/,
    (byte) 170,
    (byte) 200,
    (byte) 16 /*0x10*/,
    (byte) 199,
    (byte) 135,
    (byte) 107,
    (byte) 191,
    (byte) 243,
    (byte) 61,
    (byte) 32 /*0x20*/,
    (byte) 109,
    (byte) 181,
    (byte) 41,
    (byte) 177,
    (byte) 12,
    (byte) 148,
    (byte) 66,
    (byte) 12,
    (byte) 234,
    (byte) 146,
    (byte) 178,
    (byte) 202,
    (byte) 250,
    (byte) 119,
    (byte) 121,
    (byte) 228,
    (byte) 55,
    (byte) 38,
    (byte) 56,
    (byte) 111,
    (byte) 21,
    (byte) 14,
    (byte) 6,
    (byte) 240 /*0xF0*/,
    (byte) 217,
    (byte) 12,
    (byte) 157,
    (byte) 105,
    (byte) 31 /*0x1F*/,
    (byte) 30,
    (byte) 71,
    (byte) 5,
    (byte) 22,
    (byte) 232,
    (byte) 178,
    (byte) 189,
    (byte) 195,
    (byte) 9,
    (byte) 150,
    (byte) 216,
    (byte) 127 /*0x7F*/,
    (byte) 65,
    (byte) 30,
    (byte) 108,
    (byte) 184,
    (byte) 46,
    (byte) 241,
    (byte) 134,
    (byte) 194,
    (byte) 76,
    (byte) 46,
    (byte) 221,
    (byte) 209,
    (byte) 66,
    (byte) 138,
    (byte) 142,
    (byte) 51,
    (byte) 126,
    (byte) 140,
    (byte) 153,
    (byte) 84,
    (byte) 23,
    (byte) 14,
    (byte) 137,
    (byte) 44,
    (byte) 236,
    (byte) 100,
    (byte) 214,
    (byte) 220
  };
  private static byte[] sspr = new byte[312]
  {
    (byte) 44,
    (byte) 130,
    (byte) 228,
    (byte) 213,
    (byte) 249,
    (byte) 135,
    (byte) 6,
    (byte) 239,
    (byte) 136,
    (byte) 189,
    (byte) 152,
    (byte) 89,
    (byte) 239,
    (byte) 81,
    (byte) 93,
    (byte) 90,
    (byte) 28,
    (byte) 239,
    (byte) 250,
    (byte) 142,
    (byte) 180,
    (byte) 228,
    (byte) 160 /*0xA0*/,
    (byte) 229,
    (byte) 75,
    (byte) 224 /*0xE0*/,
    (byte) 56,
    (byte) 208 /*0xD0*/,
    (byte) 152,
    (byte) 138,
    (byte) 231,
    (byte) 17,
    (byte) 49,
    (byte) 86,
    (byte) 2,
    (byte) 86,
    (byte) 149,
    (byte) 58,
    (byte) 253,
    (byte) 66,
    (byte) 183,
    (byte) 206,
    (byte) 195,
    (byte) 84,
    (byte) 230,
    (byte) 135,
    (byte) 175,
    (byte) 43,
    (byte) 236,
    (byte) 215,
    (byte) 113,
    (byte) 244,
    (byte) 132,
    (byte) 49,
    (byte) 106,
    (byte) 98,
    (byte) 247,
    (byte) 247,
    (byte) 132,
    (byte) 104,
    (byte) 225,
    (byte) 142,
    (byte) 154,
    (byte) 26,
    (byte) 215,
    (byte) 144 /*0x90*/,
    (byte) 182,
    (byte) 132,
    (byte) 244,
    (byte) 78,
    (byte) 219,
    (byte) 26,
    (byte) 143,
    (byte) 204,
    (byte) 60,
    (byte) 205,
    (byte) 96 /*0x60*/,
    (byte) 230,
    (byte) 209,
    (byte) 180,
    (byte) 128 /*0x80*/,
    (byte) 105,
    (byte) 149,
    (byte) 207,
    (byte) 63 /*0x3F*/,
    (byte) 50,
    (byte) 31 /*0x1F*/,
    (byte) 254,
    (byte) 165,
    (byte) 189,
    (byte) 151,
    (byte) 203,
    (byte) 156,
    (byte) 172,
    (byte) 81,
    (byte) 112 /*0x70*/,
    (byte) 15,
    (byte) 16 /*0x10*/,
    (byte) 246,
    (byte) 136,
    (byte) 60,
    (byte) 119,
    (byte) 69,
    (byte) 176 /*0xB0*/,
    (byte) 224 /*0xE0*/,
    (byte) 93,
    (byte) 175,
    (byte) 246,
    (byte) 132,
    (byte) 63 /*0x3F*/,
    (byte) 188,
    (byte) 45,
    (byte) 67,
    (byte) 160 /*0xA0*/,
    (byte) 202,
    (byte) 63 /*0x3F*/,
    (byte) 140,
    (byte) 44,
    (byte) 124,
    (byte) 210,
    (byte) 31 /*0x1F*/,
    (byte) 47,
    (byte) 60,
    (byte) 134,
    (byte) 31 /*0x1F*/,
    (byte) 133,
    (byte) 69,
    (byte) 202,
    (byte) 227,
    (byte) 169,
    (byte) 74,
    (byte) 193,
    (byte) 108,
    (byte) 150,
    (byte) 231,
    (byte) 253,
    (byte) 244,
    (byte) 11,
    (byte) 66,
    (byte) 165,
    (byte) 235,
    (byte) 14,
    (byte) 12,
    (byte) 126,
    (byte) 88,
    (byte) 184,
    (byte) 199,
    (byte) 160 /*0xA0*/,
    (byte) 187,
    (byte) 160 /*0xA0*/,
    (byte) 84,
    (byte) 198,
    (byte) 150,
    (byte) 223,
    (byte) 4,
    (byte) 29,
    (byte) 54,
    (byte) 97,
    (byte) 131,
    (byte) 246,
    (byte) 22,
    (byte) 213,
    (byte) 40,
    (byte) 235,
    (byte) 234,
    (byte) 70,
    (byte) 69,
    (byte) 67,
    (byte) 182,
    (byte) 179,
    (byte) 177,
    (byte) 26,
    (byte) 85,
    (byte) 42,
    (byte) 176 /*0xB0*/,
    (byte) 156,
    (byte) 50,
    (byte) 186,
    (byte) 104,
    (byte) 82,
    (byte) 169,
    (byte) 192 /*0xC0*/,
    (byte) 94,
    (byte) 51,
    (byte) 95,
    (byte) 132,
    (byte) 130,
    (byte) 89,
    (byte) 62,
    (byte) 35,
    (byte) 31 /*0x1F*/,
    (byte) 21,
    (byte) 186,
    (byte) 137,
    (byte) 36,
    (byte) 159,
    (byte) 225,
    (byte) 126,
    (byte) 36,
    (byte) 146,
    (byte) 151,
    (byte) 81,
    (byte) 63 /*0x3F*/,
    (byte) 193,
    (byte) 113,
    (byte) 144 /*0x90*/,
    (byte) 205,
    (byte) 214,
    (byte) 175,
    (byte) 153,
    (byte) 184,
    (byte) 176 /*0xB0*/,
    (byte) 106,
    (byte) 175,
    (byte) 221,
    (byte) 151,
    (byte) 3,
    (byte) 81,
    (byte) 99,
    (byte) 156,
    (byte) 80 /*0x50*/,
    (byte) 194,
    (byte) 9,
    (byte) 123,
    (byte) 93,
    (byte) 63 /*0x3F*/,
    (byte) 88,
    (byte) 18,
    (byte) 107,
    (byte) 254,
    (byte) 48 /*0x30*/,
    (byte) 175,
    (byte) 5,
    (byte) 157,
    (byte) 119,
    (byte) 0,
    (byte) 142,
    (byte) 151,
    (byte) 176 /*0xB0*/,
    (byte) 129,
    (byte) 117,
    (byte) 74,
    (byte) 193,
    (byte) 192 /*0xC0*/,
    (byte) 241,
    (byte) 36,
    (byte) 16 /*0x10*/,
    (byte) 101,
    (byte) 74,
    (byte) 59,
    (byte) 190,
    (byte) 243,
    (byte) 85,
    (byte) 71,
    (byte) 44,
    (byte) 222,
    (byte) 10,
    (byte) 138,
    (byte) 141,
    (byte) 203,
    (byte) 223,
    (byte) 36,
    (byte) 28,
    (byte) 143,
    (byte) 189,
    (byte) 171,
    (byte) 235,
    (byte) 212,
    (byte) 117,
    (byte) 199,
    (byte) 186,
    (byte) 131,
    (byte) 232,
    (byte) 230,
    (byte) 181,
    (byte) 152,
    (byte) 34,
    (byte) 222,
    (byte) 15,
    (byte) 195,
    (byte) 235,
    (byte) 67,
    (byte) 130,
    (byte) 247,
    (byte) 218,
    (byte) 141,
    (byte) 9,
    (byte) 14,
    (byte) 203,
    (byte) 248,
    (byte) 81,
    (byte) 219,
    (byte) 197,
    (byte) 140,
    (byte) 69,
    (byte) 133,
    (byte) 170,
    (byte) 117,
    (byte) 100,
    (byte) 145,
    (byte) 246,
    (byte) 32 /*0x20*/,
    (byte) 149,
    (byte) 162,
    (byte) 192 /*0xC0*/,
    (byte) 30,
    (byte) 20,
    (byte) 16 /*0x10*/,
    (byte) 189,
    (byte) 179,
    (byte) 78,
    (byte) 166
  };

  internal static int ssp_appserver_12333(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 118;
    sourceArray1[1] = (byte) 145;
    sourceArray1[2] = (byte) 21;
    sourceArray1[3] = (byte) 93;
    sourceArray1[16 /*0x10*/] = (byte) 147;
    sourceArray1[34] = (byte) 167;
    sourceArray1[6] = (byte) 89;
    sourceArray1[7] = (byte) 144 /*0x90*/;
    sourceArray1[28] = (byte) 253;
    sourceArray1[9] = (byte) 94;
    sourceArray1[19] = (byte) 130;
    sourceArray1[39] = (byte) 22;
    sourceArray1[37] = (byte) 171;
    sourceArray1[5] = (byte) 241;
    sourceArray1[35] = (byte) 170;
    sourceArray1[15] = (byte) 82;
    sourceArray1[31 /*0x1F*/] = (byte) 165;
    sourceArray1[17] = (byte) 169;
    sourceArray1[18] = (byte) 111;
    sourceArray1[11] = (byte) 233;
    sourceArray1[20] = (byte) 181;
    sourceArray1[12] = (byte) 133;
    sourceArray1[22] = (byte) 127 /*0x7F*/;
    sourceArray1[23] = (byte) 85;
    sourceArray1[8] = (byte) 116;
    sourceArray1[29] = (byte) 93;
    sourceArray1[26] = (byte) 18;
    sourceArray1[30] = (byte) 95;
    sourceArray1[25] = (byte) 194;
    sourceArray1[10] = (byte) 62;
    sourceArray1[21] = (byte) 11;
    sourceArray1[13] = (byte) 160 /*0xA0*/;
    sourceArray1[32 /*0x20*/] = (byte) 121;
    sourceArray1[33] = (byte) 211;
    sourceArray1[14] = (byte) 160 /*0xA0*/;
    sourceArray1[4] = (byte) 37;
    sourceArray1[36] = (byte) 245;
    sourceArray1[0] = (byte) 181;
    sourceArray1[38] = (byte) 238;
    sourceArray1[41] = (byte) 240 /*0xF0*/;
    sourceArray1[40] = (byte) 55;
    sourceArray1[44] = (byte) 219;
    sourceArray1[42] = (byte) 18;
    sourceArray1[43] = (byte) 211;
    sourceArray1[27] = (byte) 207;
    sourceArray1[45] = (byte) 113;
    sourceArray1[46] = (byte) 173;
    sourceArray1[47] = (byte) 157;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 3,
      (byte) 132,
      (byte) 181,
      (byte) 231,
      (byte) 125,
      (byte) 183,
      (byte) 20,
      (byte) 162,
      (byte) 210,
      (byte) 2,
      (byte) 48 /*0x30*/,
      (byte) 14,
      (byte) 175,
      (byte) 99,
      (byte) 76,
      (byte) 23,
      (byte) 28,
      (byte) 121,
      (byte) 89,
      (byte) 95,
      (byte) 9,
      (byte) 148,
      (byte) 230,
      (byte) 19,
      (byte) 166,
      (byte) 26,
      (byte) 6,
      (byte) 77,
      (byte) 198,
      (byte) 40,
      (byte) 228,
      (byte) 128 /*0x80*/,
      (byte) 242,
      (byte) 27,
      (byte) 117,
      (byte) 18,
      (byte) 178,
      (byte) 50,
      (byte) 17,
      (byte) 64 /*0x40*/,
      (byte) 142,
      (byte) 74,
      (byte) 155,
      (byte) 77,
      (byte) 183,
      (byte) 19,
      (byte) 110,
      (byte) 123
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12334(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 252,
      (byte) 165,
      (byte) 153,
      (byte) 127 /*0x7F*/,
      (byte) 121,
      (byte) 11,
      (byte) 72,
      (byte) 34,
      (byte) 125,
      (byte) 177,
      (byte) 102,
      (byte) 69,
      (byte) 226,
      (byte) 76,
      (byte) 106,
      (byte) 75,
      (byte) 113,
      (byte) 28,
      (byte) 100,
      (byte) 148,
      (byte) 99,
      (byte) 31 /*0x1F*/,
      (byte) 250,
      (byte) 94,
      (byte) 39,
      (byte) 36,
      (byte) 47,
      (byte) 33,
      (byte) 158,
      (byte) 103,
      (byte) 253,
      (byte) 215,
      (byte) 55,
      (byte) 1,
      (byte) 24,
      (byte) 207,
      (byte) 164,
      (byte) 162,
      (byte) 129,
      (byte) 142,
      (byte) 187,
      (byte) 232,
      (byte) 20,
      (byte) 76,
      (byte) 49,
      (byte) 239,
      (byte) 226,
      (byte) 63 /*0x3F*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 197,
      (byte) 135,
      (byte) 191,
      (byte) 97,
      (byte) 91,
      (byte) 221,
      (byte) 221,
      (byte) 64 /*0x40*/,
      (byte) 34,
      (byte) 155,
      (byte) 149,
      (byte) 50,
      (byte) 206,
      (byte) 40,
      (byte) 251,
      (byte) 28,
      (byte) 68,
      (byte) 49,
      (byte) 111,
      (byte) 166,
      (byte) 77,
      (byte) 102,
      (byte) 25,
      (byte) 157,
      (byte) 59,
      (byte) 176 /*0xB0*/,
      (byte) 253,
      (byte) 175,
      (byte) 30,
      (byte) 218,
      (byte) 10,
      (byte) 80 /*0x50*/,
      (byte) 204,
      (byte) 206,
      (byte) 212,
      (byte) 90,
      (byte) 107,
      (byte) 231,
      (byte) 220,
      (byte) 36,
      (byte) 67,
      (byte) 116,
      (byte) 193,
      (byte) 126,
      (byte) 12,
      (byte) 186,
      (byte) 84
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[45];
    byte[] response2 = new byte[45];
    Array.Copy((Array) sc_12332.sspq, 0, (Array) numArray2, 0, 45);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 0, (Array) numArray2, 0, 45);
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

  internal static int ssp_appserver_12335(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 51;
    sourceArray1[39] = (byte) 131;
    sourceArray1[13] = (byte) 13;
    sourceArray1[16 /*0x10*/] = (byte) 235;
    sourceArray1[45] = (byte) 105;
    sourceArray1[5] = (byte) 185;
    sourceArray1[26] = (byte) 61;
    sourceArray1[7] = (byte) 68;
    sourceArray1[34] = (byte) 138;
    sourceArray1[38] = (byte) 32 /*0x20*/;
    sourceArray1[8] = (byte) 153;
    sourceArray1[23] = (byte) 125;
    sourceArray1[9] = (byte) 213;
    sourceArray1[21] = (byte) 40;
    sourceArray1[14] = (byte) 32 /*0x20*/;
    sourceArray1[15] = (byte) 19;
    sourceArray1[3] = (byte) 11;
    sourceArray1[17] = (byte) 210;
    sourceArray1[30] = (byte) 68;
    sourceArray1[19] = (byte) 155;
    sourceArray1[43] = (byte) 179;
    sourceArray1[32 /*0x20*/] = (byte) 12;
    sourceArray1[1] = (byte) 225;
    sourceArray1[40] = (byte) 93;
    sourceArray1[24] = (byte) 248;
    sourceArray1[6] = (byte) 29;
    sourceArray1[42] = (byte) 205;
    sourceArray1[27] = (byte) 132;
    sourceArray1[28] = (byte) 109;
    sourceArray1[2] = (byte) 1;
    sourceArray1[20] = (byte) 149;
    sourceArray1[31 /*0x1F*/] = (byte) 23;
    sourceArray1[18] = (byte) 71;
    sourceArray1[33] = (byte) 21;
    sourceArray1[11] = (byte) 210;
    sourceArray1[0] = (byte) 162;
    sourceArray1[36] = (byte) 64 /*0x40*/;
    sourceArray1[37] = (byte) 229;
    sourceArray1[22] = (byte) 132;
    sourceArray1[25] = (byte) 116;
    sourceArray1[29] = (byte) 36;
    sourceArray1[41] = (byte) 218;
    sourceArray1[10] = (byte) 35;
    sourceArray1[12] = (byte) 251;
    sourceArray1[44] = (byte) 63 /*0x3F*/;
    sourceArray1[35] = (byte) 86;
    sourceArray1[46] = (byte) 41;
    sourceArray1[47] = (byte) 91;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[43] = (byte) 18;
    sourceArray2[14] = (byte) 109;
    sourceArray2[40] = (byte) 93;
    sourceArray2[3] = (byte) 101;
    sourceArray2[10] = (byte) 39;
    sourceArray2[5] = (byte) 187;
    sourceArray2[34] = (byte) 114;
    sourceArray2[27] = (byte) 114;
    sourceArray2[28] = (byte) 222;
    sourceArray2[11] = (byte) 60;
    sourceArray2[9] = (byte) 63 /*0x3F*/;
    sourceArray2[29] = (byte) 114;
    sourceArray2[2] = (byte) 15;
    sourceArray2[13] = (byte) 38;
    sourceArray2[6] = (byte) 1;
    sourceArray2[15] = (byte) 16 /*0x10*/;
    sourceArray2[16 /*0x10*/] = (byte) 121;
    sourceArray2[0] = (byte) 253;
    sourceArray2[31 /*0x1F*/] = (byte) 123;
    sourceArray2[19] = (byte) 115;
    sourceArray2[20] = (byte) 149;
    sourceArray2[1] = (byte) 144 /*0x90*/;
    sourceArray2[7] = (byte) 63 /*0x3F*/;
    sourceArray2[23] = (byte) 208 /*0xD0*/;
    sourceArray2[24] = (byte) 36;
    sourceArray2[25] = (byte) 165;
    sourceArray2[36] = (byte) 204;
    sourceArray2[39] = (byte) 184;
    sourceArray2[26] = (byte) 93;
    sourceArray2[12] = (byte) 202;
    sourceArray2[17] = (byte) 117;
    sourceArray2[42] = (byte) 132;
    sourceArray2[32 /*0x20*/] = (byte) 22;
    sourceArray2[46] = (byte) 253;
    sourceArray2[41] = (byte) 254;
    sourceArray2[35] = (byte) 14;
    sourceArray2[4] = (byte) 103;
    sourceArray2[37] = (byte) 110;
    sourceArray2[38] = (byte) 170;
    sourceArray2[18] = (byte) 19;
    sourceArray2[44] = (byte) 126;
    sourceArray2[22] = (byte) 76;
    sourceArray2[8] = (byte) 226;
    sourceArray2[45] = (byte) 225;
    sourceArray2[33] = (byte) 105;
    sourceArray2[21] = (byte) 180;
    sourceArray2[30] = (byte) 17;
    sourceArray2[47] = (byte) 179;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[40];
    byte[] response2 = new byte[40];
    Array.Copy((Array) sc_12332.sspq, 45, (Array) numArray2, 0, 40);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 45, (Array) numArray2, 0, 40);
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

  internal static int ssp_appserver_12336(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 163,
      (byte) 108,
      (byte) 225,
      (byte) 19,
      (byte) 179,
      (byte) 4,
      (byte) 214,
      (byte) 56,
      (byte) 211,
      (byte) 145,
      (byte) 59,
      (byte) 179,
      (byte) 129,
      (byte) 220,
      (byte) 67,
      (byte) 15,
      (byte) 198,
      (byte) 119,
      (byte) 174,
      (byte) 240 /*0xF0*/,
      (byte) 101,
      (byte) 227,
      (byte) 43,
      (byte) 140,
      (byte) 163,
      (byte) 93,
      (byte) 76,
      (byte) 188,
      (byte) 92,
      (byte) 90,
      (byte) 65,
      (byte) 78,
      (byte) 56,
      (byte) 213,
      (byte) 237,
      (byte) 214,
      (byte) 145,
      (byte) 101,
      (byte) 232,
      (byte) 109,
      (byte) 193,
      (byte) 209,
      (byte) 175,
      (byte) 208 /*0xD0*/,
      (byte) 102,
      (byte) 51,
      (byte) 224 /*0xE0*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 133,
      (byte) 105,
      (byte) 117,
      (byte) 123,
      (byte) 49,
      (byte) 81,
      (byte) 165,
      (byte) 170,
      byte.MaxValue,
      (byte) 165,
      (byte) 40,
      (byte) 148,
      (byte) 76,
      (byte) 139,
      (byte) 130,
      (byte) 104,
      (byte) 222,
      (byte) 40,
      (byte) 78,
      (byte) 87,
      (byte) 28,
      (byte) 111,
      (byte) 143,
      (byte) 226,
      (byte) 109,
      (byte) 213,
      (byte) 4,
      (byte) 161,
      (byte) 145,
      (byte) 132,
      (byte) 39,
      (byte) 68,
      (byte) 42,
      (byte) 75,
      (byte) 200,
      (byte) 247,
      (byte) 88,
      (byte) 174,
      (byte) 24,
      (byte) 29,
      (byte) 14,
      (byte) 230,
      (byte) 31 /*0x1F*/,
      (byte) 87,
      (byte) 94,
      (byte) 164,
      (byte) 30,
      (byte) 43
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[45];
    byte[] response2 = new byte[45];
    Array.Copy((Array) sc_12332.sspq, 85, (Array) numArray2, 0, 45);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 85, (Array) numArray2, 0, 45);
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

  internal static int ssp_appserver_12337(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[11] = (byte) 158;
    sourceArray1[4] = (byte) 132;
    sourceArray1[2] = (byte) 184;
    sourceArray1[3] = (byte) 51;
    sourceArray1[27] = (byte) 65;
    sourceArray1[5] = (byte) 63 /*0x3F*/;
    sourceArray1[46] = (byte) 160 /*0xA0*/;
    sourceArray1[30] = (byte) 139;
    sourceArray1[8] = (byte) 148;
    sourceArray1[9] = (byte) 252;
    sourceArray1[24] = (byte) 8;
    sourceArray1[6] = (byte) 166;
    sourceArray1[15] = (byte) 0;
    sourceArray1[31 /*0x1F*/] = (byte) 133;
    sourceArray1[22] = (byte) 219;
    sourceArray1[12] = (byte) 140;
    sourceArray1[13] = (byte) 229;
    sourceArray1[18] = (byte) 9;
    sourceArray1[16 /*0x10*/] = (byte) 215;
    sourceArray1[29] = (byte) 251;
    sourceArray1[34] = (byte) 218;
    sourceArray1[21] = (byte) 51;
    sourceArray1[33] = (byte) 252;
    sourceArray1[23] = (byte) 180;
    sourceArray1[35] = (byte) 172;
    sourceArray1[0] = (byte) 128 /*0x80*/;
    sourceArray1[26] = (byte) 215;
    sourceArray1[1] = (byte) 121;
    sourceArray1[28] = (byte) 108;
    sourceArray1[25] = (byte) 193;
    sourceArray1[7] = (byte) 154;
    sourceArray1[10] = (byte) 67;
    sourceArray1[32 /*0x20*/] = (byte) 156;
    sourceArray1[20] = (byte) 26;
    sourceArray1[38] = (byte) 85;
    sourceArray1[17] = (byte) 222;
    sourceArray1[36] = (byte) 183;
    sourceArray1[42] = (byte) 136;
    sourceArray1[14] = (byte) 46;
    sourceArray1[39] = (byte) 207;
    sourceArray1[40] = (byte) 145;
    sourceArray1[41] = (byte) 196;
    sourceArray1[37] = (byte) 74;
    sourceArray1[43] = (byte) 20;
    sourceArray1[44] = (byte) 130;
    sourceArray1[45] = (byte) 5;
    sourceArray1[19] = (byte) 163;
    sourceArray1[47] = (byte) 176 /*0xB0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 95;
    sourceArray2[34] = (byte) 233;
    sourceArray2[47] = (byte) 112 /*0x70*/;
    sourceArray2[33] = (byte) 126;
    sourceArray2[46] = (byte) 68;
    sourceArray2[15] = (byte) 147;
    sourceArray2[44] = (byte) 5;
    sourceArray2[42] = (byte) 37;
    sourceArray2[8] = (byte) 236;
    sourceArray2[27] = (byte) 225;
    sourceArray2[10] = (byte) 103;
    sourceArray2[26] = (byte) 246;
    sourceArray2[7] = (byte) 151;
    sourceArray2[24] = (byte) 86;
    sourceArray2[14] = (byte) 108;
    sourceArray2[1] = (byte) 123;
    sourceArray2[16 /*0x10*/] = (byte) 204;
    sourceArray2[17] = (byte) 157;
    sourceArray2[4] = (byte) 236;
    sourceArray2[19] = (byte) 15;
    sourceArray2[20] = (byte) 134;
    sourceArray2[21] = (byte) 22;
    sourceArray2[22] = (byte) 165;
    sourceArray2[2] = (byte) 222;
    sourceArray2[9] = (byte) 198;
    sourceArray2[25] = (byte) 33;
    sourceArray2[40] = (byte) 74;
    sourceArray2[5] = (byte) 69;
    sourceArray2[28] = (byte) 104;
    sourceArray2[29] = (byte) 209;
    sourceArray2[12] = (byte) 87;
    sourceArray2[0] = (byte) 199;
    sourceArray2[32 /*0x20*/] = (byte) 36;
    sourceArray2[3] = (byte) 19;
    sourceArray2[23] = (byte) 0;
    sourceArray2[35] = (byte) 237;
    sourceArray2[6] = (byte) 138;
    sourceArray2[37] = (byte) 24;
    sourceArray2[38] = (byte) 183;
    sourceArray2[36] = (byte) 208 /*0xD0*/;
    sourceArray2[18] = (byte) 92;
    sourceArray2[41] = (byte) 201;
    sourceArray2[30] = (byte) 6;
    sourceArray2[39] = (byte) 229;
    sourceArray2[31 /*0x1F*/] = (byte) 8;
    sourceArray2[45] = (byte) 135;
    sourceArray2[11] = (byte) 24;
    sourceArray2[43] = (byte) 83;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[18];
    byte[] response2 = new byte[18];
    Array.Copy((Array) sc_12332.sspq, 130, (Array) numArray2, 0, 18);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 130, (Array) numArray2, 0, 18);
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

  internal static string ssp_appserver_12338()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[154];
      byte[] numArray2 = new byte[55]
      {
        (byte) 100,
        (byte) 39,
        (byte) 143,
        (byte) 190,
        (byte) 135,
        (byte) 229,
        (byte) 72,
        (byte) 87,
        (byte) 87,
        (byte) 177,
        (byte) 149,
        (byte) 143,
        (byte) 60,
        (byte) 179,
        (byte) 117,
        (byte) 4,
        (byte) 171,
        (byte) 230,
        (byte) 219,
        (byte) 38,
        (byte) 60,
        (byte) 128 /*0x80*/,
        (byte) 179,
        (byte) 178,
        (byte) 89,
        (byte) 146,
        (byte) 185,
        (byte) 80 /*0x50*/,
        (byte) 13,
        (byte) 237,
        (byte) 168,
        (byte) 18,
        (byte) 6,
        (byte) 131,
        (byte) 125,
        (byte) 208 /*0xD0*/,
        (byte) 98,
        (byte) 103,
        (byte) 118,
        (byte) 253,
        (byte) 76,
        (byte) 143,
        (byte) 149,
        (byte) 155,
        (byte) 201,
        (byte) 152,
        (byte) 218,
        (byte) 96 /*0x60*/,
        (byte) 74,
        (byte) 84,
        (byte) 231,
        (byte) 119,
        (byte) 43,
        (byte) 190,
        (byte) 119
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 173,
        (byte) 79,
        (byte) 93,
        (byte) 227,
        (byte) 8,
        (byte) 131,
        (byte) 135,
        (byte) 149,
        (byte) 77,
        (byte) 112 /*0x70*/,
        (byte) 43,
        (byte) 101,
        (byte) 145,
        (byte) 83,
        (byte) 3,
        (byte) 219,
        (byte) 59,
        (byte) 196,
        (byte) 83,
        (byte) 188,
        (byte) 189,
        (byte) 102,
        (byte) 251,
        (byte) 36,
        (byte) 175,
        (byte) 117,
        (byte) 82,
        (byte) 251,
        (byte) 79,
        (byte) 5,
        (byte) 110,
        (byte) 137,
        (byte) 153,
        (byte) 235,
        (byte) 89,
        (byte) 77,
        (byte) 58,
        (byte) 213,
        (byte) 8,
        (byte) 159,
        (byte) 179,
        (byte) 79,
        (byte) 70,
        (byte) 18,
        (byte) 219,
        (byte) 166,
        (byte) 169,
        (byte) 184,
        (byte) 189,
        (byte) 181,
        (byte) 0,
        (byte) 71,
        (byte) 177,
        (byte) 244,
        (byte) 124
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[54] = (byte) 92;
      numArray4[1] = (byte) 101;
      numArray4[18] = (byte) 172;
      numArray4[3] = (byte) 44;
      numArray4[34] = (byte) 133;
      numArray4[2] = (byte) 245;
      numArray4[6] = (byte) 105;
      numArray4[11] = (byte) 54;
      numArray4[4] = (byte) 247;
      numArray4[9] = (byte) 136;
      numArray4[50] = (byte) 76;
      numArray4[51] = (byte) 228;
      numArray4[12] = (byte) 165;
      numArray4[13] = (byte) 161;
      numArray4[14] = (byte) 67;
      numArray4[33] = (byte) 91;
      numArray4[16 /*0x10*/] = (byte) 222;
      numArray4[36] = (byte) 129;
      numArray4[7] = (byte) 8;
      numArray4[52] = (byte) 202;
      numArray4[20] = (byte) 24;
      numArray4[0] = (byte) 220;
      numArray4[42] = (byte) 70;
      numArray4[23] = (byte) 183;
      numArray4[22] = (byte) 192 /*0xC0*/;
      numArray4[17] = (byte) 217;
      numArray4[26] = (byte) 184;
      numArray4[27] = (byte) 5;
      numArray4[10] = (byte) 185;
      numArray4[19] = (byte) 234;
      numArray4[29] = (byte) 231;
      numArray4[28] = (byte) 48 /*0x30*/;
      numArray4[8] = (byte) 131;
      numArray4[40] = (byte) 43;
      numArray4[15] = (byte) 112 /*0x70*/;
      numArray4[46] = (byte) 165;
      numArray4[5] = (byte) 74;
      numArray4[24] = (byte) 88;
      numArray4[38] = (byte) 85;
      numArray4[39] = (byte) 69;
      numArray4[21] = (byte) 232;
      numArray4[49] = (byte) 7;
      numArray4[25] = (byte) 5;
      numArray4[43] = (byte) 15;
      numArray4[44] = (byte) 32 /*0x20*/;
      numArray4[45] = (byte) 42;
      numArray4[35] = (byte) 2;
      numArray4[47] = (byte) 178;
      numArray4[48 /*0x30*/] = (byte) 163;
      numArray4[37] = (byte) 208 /*0xD0*/;
      numArray4[30] = (byte) 72;
      numArray4[31 /*0x1F*/] = (byte) 12;
      numArray4[41] = (byte) 146;
      numArray4[53] = (byte) 249;
      numArray4[32 /*0x20*/] = (byte) 153;
      byte[] numArray5 = new byte[55];
      numArray5[23] = (byte) 237;
      numArray5[17] = (byte) 145;
      numArray5[2] = (byte) 176 /*0xB0*/;
      numArray5[3] = (byte) 120;
      numArray5[43] = (byte) 156;
      numArray5[20] = (byte) 38;
      numArray5[6] = (byte) 60;
      numArray5[32 /*0x20*/] = (byte) 139;
      numArray5[22] = (byte) 48 /*0x30*/;
      numArray5[9] = (byte) 66;
      numArray5[4] = (byte) 97;
      numArray5[29] = (byte) 107;
      numArray5[13] = (byte) 218;
      numArray5[15] = (byte) 4;
      numArray5[14] = (byte) 208 /*0xD0*/;
      numArray5[36] = (byte) 200;
      numArray5[33] = (byte) 20;
      numArray5[44] = (byte) 20;
      numArray5[18] = (byte) 165;
      numArray5[19] = (byte) 135;
      numArray5[31 /*0x1F*/] = (byte) 74;
      numArray5[40] = (byte) 156;
      numArray5[7] = (byte) 109;
      numArray5[49] = (byte) 5;
      numArray5[35] = (byte) 12;
      numArray5[25] = (byte) 154;
      numArray5[16 /*0x10*/] = (byte) 65;
      numArray5[27] = (byte) 197;
      numArray5[28] = (byte) 1;
      numArray5[37] = (byte) 120;
      numArray5[41] = (byte) 64 /*0x40*/;
      numArray5[39] = (byte) 218;
      numArray5[1] = (byte) 238;
      numArray5[42] = (byte) 225;
      numArray5[30] = (byte) 65;
      numArray5[0] = (byte) 80 /*0x50*/;
      numArray5[51] = (byte) 141;
      numArray5[52] = (byte) 38;
      numArray5[38] = (byte) 42;
      numArray5[24] = (byte) 150;
      numArray5[21] = (byte) 126;
      numArray5[53] = (byte) 165;
      numArray5[26] = (byte) 67;
      numArray5[46] = (byte) 229;
      numArray5[8] = (byte) 29;
      numArray5[45] = (byte) 67;
      numArray5[12] = (byte) 134;
      numArray5[47] = (byte) 17;
      numArray5[48 /*0x30*/] = (byte) 134;
      numArray5[34] = (byte) 155;
      numArray5[50] = (byte) 248;
      numArray5[54] = (byte) 109;
      numArray5[5] = (byte) 111;
      numArray5[11] = (byte) 51;
      numArray5[10] = (byte) 2;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[44];
      numArray6[20] = (byte) 239;
      numArray6[1] = (byte) 4;
      numArray6[17] = (byte) 48 /*0x30*/;
      numArray6[33] = (byte) 85;
      numArray6[4] = (byte) 2;
      numArray6[5] = (byte) 155;
      numArray6[21] = (byte) 28;
      numArray6[7] = (byte) 218;
      numArray6[8] = (byte) 141;
      numArray6[3] = (byte) 26;
      numArray6[22] = (byte) 15;
      numArray6[11] = (byte) 76;
      numArray6[30] = (byte) 34;
      numArray6[15] = (byte) 79;
      numArray6[19] = (byte) 77;
      numArray6[29] = (byte) 46;
      numArray6[40] = (byte) 4;
      numArray6[36] = (byte) 68;
      numArray6[0] = (byte) 129;
      numArray6[35] = (byte) 82;
      numArray6[38] = (byte) 195;
      numArray6[13] = (byte) 13;
      numArray6[27] = (byte) 46;
      numArray6[23] = (byte) 242;
      numArray6[24] = (byte) 100;
      numArray6[25] = (byte) 128 /*0x80*/;
      numArray6[26] = (byte) 166;
      numArray6[9] = (byte) 151;
      numArray6[28] = (byte) 195;
      numArray6[34] = (byte) 28;
      numArray6[10] = (byte) 33;
      numArray6[42] = (byte) 122;
      numArray6[32 /*0x20*/] = (byte) 226;
      numArray6[6] = (byte) 165;
      numArray6[18] = (byte) 189;
      numArray6[14] = (byte) 185;
      numArray6[12] = (byte) 123;
      numArray6[37] = (byte) 15;
      numArray6[16 /*0x10*/] = (byte) 137;
      numArray6[39] = (byte) 113;
      numArray6[31 /*0x1F*/] = (byte) 28;
      numArray6[41] = (byte) 23;
      numArray6[2] = (byte) 141;
      numArray6[43] = (byte) 249;
      byte[] numArray7 = new byte[44]
      {
        (byte) 112 /*0x70*/,
        (byte) 128 /*0x80*/,
        (byte) 171,
        (byte) 78,
        (byte) 47,
        (byte) 166,
        (byte) 51,
        (byte) 216,
        (byte) 133,
        (byte) 47,
        (byte) 54,
        (byte) 167,
        (byte) 6,
        (byte) 207,
        (byte) 115,
        byte.MaxValue,
        (byte) 240 /*0xF0*/,
        (byte) 173,
        (byte) 188,
        (byte) 82,
        (byte) 18,
        (byte) 121,
        (byte) 151,
        (byte) 7,
        (byte) 216,
        (byte) 96 /*0x60*/,
        (byte) 87,
        (byte) 12,
        (byte) 139,
        (byte) 188,
        (byte) 253,
        (byte) 249,
        (byte) 25,
        (byte) 170,
        (byte) 103,
        (byte) 191,
        (byte) 47,
        (byte) 84,
        (byte) 122,
        (byte) 162,
        (byte) 145,
        (byte) 183,
        (byte) 243,
        (byte) 57
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[27];
      byte[] response = new byte[27];
      Array.Copy((Array) sc_12332.sspq, 148, (Array) numArray8, 0, 27);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12332.sspr, 148, (Array) numArray8, 0, 27);
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
    byte[] numArray9 = new byte[154];
    byte[] numArray10 = new byte[55];
    numArray10[1] = (byte) 54;
    numArray10[3] = (byte) 244;
    numArray10[2] = (byte) 149;
    numArray10[49] = (byte) 188;
    numArray10[35] = (byte) 179;
    numArray10[12] = (byte) 134;
    numArray10[21] = (byte) 105;
    numArray10[29] = (byte) 61;
    numArray10[18] = (byte) 173;
    numArray10[9] = (byte) 209;
    numArray10[10] = (byte) 187;
    numArray10[11] = (byte) 215;
    numArray10[24] = (byte) 229;
    numArray10[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    numArray10[14] = (byte) 24;
    numArray10[38] = (byte) 54;
    numArray10[15] = (byte) 82;
    numArray10[50] = (byte) 100;
    numArray10[13] = (byte) 145;
    numArray10[44] = (byte) 8;
    numArray10[6] = (byte) 98;
    numArray10[46] = (byte) 58;
    numArray10[25] = (byte) 246;
    numArray10[23] = (byte) 15;
    numArray10[53] = (byte) 81;
    numArray10[47] = (byte) 76;
    numArray10[26] = (byte) 127 /*0x7F*/;
    numArray10[27] = (byte) 17;
    numArray10[28] = (byte) 74;
    numArray10[48 /*0x30*/] = (byte) 127 /*0x7F*/;
    numArray10[5] = (byte) 8;
    numArray10[31 /*0x1F*/] = (byte) 210;
    numArray10[37] = (byte) 232;
    numArray10[34] = (byte) 30;
    numArray10[4] = (byte) 55;
    numArray10[51] = (byte) 213;
    numArray10[36] = (byte) 190;
    numArray10[52] = (byte) 13;
    numArray10[7] = (byte) 32 /*0x20*/;
    numArray10[39] = (byte) 4;
    numArray10[40] = (byte) 129;
    numArray10[41] = (byte) 138;
    numArray10[42] = (byte) 121;
    numArray10[43] = (byte) 48 /*0x30*/;
    numArray10[22] = (byte) 205;
    numArray10[45] = (byte) 116;
    numArray10[30] = (byte) 205;
    numArray10[32 /*0x20*/] = (byte) 190;
    numArray10[33] = (byte) 198;
    numArray10[0] = (byte) 101;
    numArray10[8] = (byte) 43;
    numArray10[20] = (byte) 92;
    numArray10[19] = (byte) 240 /*0xF0*/;
    numArray10[17] = (byte) 217;
    numArray10[54] = (byte) 248;
    byte[] numArray11 = new byte[55]
    {
      (byte) 224 /*0xE0*/,
      (byte) 30,
      (byte) 213,
      (byte) 127 /*0x7F*/,
      (byte) 185,
      (byte) 26,
      (byte) 211,
      (byte) 52,
      (byte) 84,
      (byte) 220,
      (byte) 245,
      (byte) 5,
      (byte) 21,
      (byte) 200,
      (byte) 89,
      (byte) 43,
      byte.MaxValue,
      (byte) 180,
      (byte) 228,
      (byte) 135,
      (byte) 174,
      (byte) 79,
      (byte) 7,
      (byte) 95,
      (byte) 171,
      (byte) 107,
      (byte) 164,
      (byte) 72,
      (byte) 143,
      (byte) 110,
      (byte) 12,
      (byte) 249,
      (byte) 83,
      (byte) 10,
      (byte) 176 /*0xB0*/,
      (byte) 161,
      (byte) 151,
      (byte) 198,
      (byte) 33,
      (byte) 160 /*0xA0*/,
      (byte) 96 /*0x60*/,
      (byte) 32 /*0x20*/,
      (byte) 79,
      (byte) 141,
      (byte) 2,
      (byte) 202,
      (byte) 31 /*0x1F*/,
      (byte) 210,
      (byte) 127 /*0x7F*/,
      (byte) 213,
      (byte) 4,
      (byte) 34,
      (byte) 122,
      (byte) 41,
      (byte) 39
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55];
    numArray12[37] = (byte) 35;
    numArray12[1] = (byte) 157;
    numArray12[2] = (byte) 133;
    numArray12[20] = (byte) 113;
    numArray12[4] = (byte) 190;
    numArray12[35] = (byte) 60;
    numArray12[6] = (byte) 20;
    numArray12[10] = (byte) 243;
    numArray12[49] = (byte) 133;
    numArray12[9] = (byte) 173;
    numArray12[36] = (byte) 112 /*0x70*/;
    numArray12[42] = (byte) 249;
    numArray12[7] = (byte) 11;
    numArray12[13] = (byte) 92;
    numArray12[14] = (byte) 221;
    numArray12[15] = (byte) 173;
    numArray12[46] = (byte) 1;
    numArray12[26] = (byte) 102;
    numArray12[39] = (byte) 109;
    numArray12[8] = (byte) 37;
    numArray12[54] = (byte) 91;
    numArray12[24] = (byte) 88;
    numArray12[33] = (byte) 125;
    numArray12[23] = (byte) 65;
    numArray12[51] = (byte) 166;
    numArray12[25] = (byte) 118;
    numArray12[16 /*0x10*/] = (byte) 47;
    numArray12[27] = (byte) 44;
    numArray12[28] = (byte) 197;
    numArray12[29] = (byte) 96 /*0x60*/;
    numArray12[17] = (byte) 60;
    numArray12[50] = (byte) 52;
    numArray12[32 /*0x20*/] = (byte) 23;
    numArray12[40] = (byte) 118;
    numArray12[43] = (byte) 173;
    numArray12[52] = (byte) 10;
    numArray12[41] = (byte) 32 /*0x20*/;
    numArray12[31 /*0x1F*/] = (byte) 166;
    numArray12[19] = (byte) 100;
    numArray12[12] = (byte) 25;
    numArray12[5] = (byte) 71;
    numArray12[11] = (byte) 79;
    numArray12[30] = (byte) 136;
    numArray12[0] = (byte) 127 /*0x7F*/;
    numArray12[22] = (byte) 22;
    numArray12[45] = (byte) 138;
    numArray12[21] = (byte) 232;
    numArray12[47] = (byte) 203;
    numArray12[48 /*0x30*/] = (byte) 208 /*0xD0*/;
    numArray12[34] = (byte) 184;
    numArray12[3] = (byte) 87;
    numArray12[18] = (byte) 28;
    numArray12[44] = (byte) 134;
    numArray12[53] = (byte) 166;
    numArray12[38] = (byte) 114;
    byte[] numArray13 = new byte[55]
    {
      (byte) 6,
      (byte) 72,
      (byte) 184,
      (byte) 51,
      (byte) 9,
      (byte) 97,
      (byte) 37,
      (byte) 40,
      (byte) 249,
      (byte) 105,
      (byte) 7,
      (byte) 126,
      (byte) 7,
      (byte) 177,
      (byte) 122,
      (byte) 217,
      (byte) 125,
      (byte) 140,
      (byte) 3,
      (byte) 191,
      (byte) 56,
      (byte) 221,
      (byte) 225,
      (byte) 153,
      (byte) 200,
      (byte) 172,
      (byte) 10,
      (byte) 216,
      (byte) 83,
      (byte) 48 /*0x30*/,
      (byte) 195,
      (byte) 166,
      (byte) 145,
      (byte) 57,
      (byte) 170,
      (byte) 92,
      (byte) 42,
      (byte) 224 /*0xE0*/,
      (byte) 224 /*0xE0*/,
      (byte) 129,
      (byte) 125,
      (byte) 2,
      (byte) 242,
      (byte) 115,
      (byte) 238,
      (byte) 240 /*0xF0*/,
      (byte) 235,
      (byte) 4,
      (byte) 237,
      (byte) 143,
      (byte) 208 /*0xD0*/,
      (byte) 54,
      (byte) 115,
      (byte) 128 /*0x80*/,
      (byte) 252
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[44]
    {
      (byte) 136,
      (byte) 48 /*0x30*/,
      (byte) 211,
      (byte) 71,
      (byte) 140,
      (byte) 5,
      (byte) 151,
      (byte) 66,
      (byte) 176 /*0xB0*/,
      (byte) 199,
      (byte) 121,
      (byte) 129,
      (byte) 234,
      (byte) 16 /*0x10*/,
      (byte) 231,
      (byte) 160 /*0xA0*/,
      (byte) 35,
      (byte) 96 /*0x60*/,
      (byte) 118,
      (byte) 184,
      (byte) 224 /*0xE0*/,
      (byte) 129,
      (byte) 124,
      (byte) 15,
      (byte) 113,
      (byte) 44,
      (byte) 247,
      (byte) 4,
      (byte) 5,
      (byte) 65,
      (byte) 185,
      (byte) 24,
      (byte) 121,
      (byte) 153,
      (byte) 164,
      (byte) 243,
      (byte) 85,
      (byte) 68,
      (byte) 1,
      (byte) 212,
      (byte) 157,
      (byte) 70,
      (byte) 136,
      (byte) 138
    };
    byte[] numArray15 = new byte[44];
    numArray15[5] = (byte) 129;
    numArray15[42] = (byte) 236;
    numArray15[2] = (byte) 165;
    numArray15[13] = (byte) 163;
    numArray15[6] = (byte) 244;
    numArray15[10] = (byte) 148;
    numArray15[15] = (byte) 39;
    numArray15[21] = (byte) 227;
    numArray15[7] = (byte) 89;
    numArray15[9] = (byte) 156;
    numArray15[0] = (byte) 107;
    numArray15[11] = (byte) 0;
    numArray15[12] = (byte) 9;
    numArray15[14] = (byte) 72;
    numArray15[41] = (byte) 194;
    numArray15[19] = (byte) 41;
    numArray15[16 /*0x10*/] = (byte) 198;
    numArray15[8] = (byte) 82;
    numArray15[18] = (byte) 74;
    numArray15[4] = (byte) 164;
    numArray15[20] = (byte) 182;
    numArray15[24] = (byte) 180;
    numArray15[22] = (byte) 99;
    numArray15[23] = (byte) 52;
    numArray15[37] = (byte) 16 /*0x10*/;
    numArray15[3] = (byte) 94;
    numArray15[26] = (byte) 95;
    numArray15[27] = (byte) 99;
    numArray15[28] = (byte) 216;
    numArray15[29] = (byte) 109;
    numArray15[30] = (byte) 73;
    numArray15[31 /*0x1F*/] = (byte) 134;
    numArray15[1] = (byte) 243;
    numArray15[35] = (byte) 157;
    numArray15[34] = (byte) 60;
    numArray15[40] = (byte) 172;
    numArray15[17] = (byte) 41;
    numArray15[32 /*0x20*/] = (byte) 105;
    numArray15[38] = (byte) 213;
    numArray15[33] = (byte) 245;
    numArray15[25] = (byte) 104;
    numArray15[39] = (byte) 10;
    numArray15[36] = (byte) 89;
    numArray15[43] = (byte) 83;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 44);
    for (int index = 0; index < 44; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12339()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[154];
      byte[] numArray2 = new byte[55]
      {
        (byte) 111,
        (byte) 160 /*0xA0*/,
        (byte) 198,
        (byte) 10,
        (byte) 10,
        (byte) 25,
        (byte) 124,
        (byte) 139,
        (byte) 158,
        (byte) 244,
        (byte) 204,
        (byte) 76,
        (byte) 240 /*0xF0*/,
        (byte) 88,
        (byte) 247,
        (byte) 242,
        (byte) 213,
        (byte) 163,
        (byte) 13,
        (byte) 193,
        (byte) 162,
        (byte) 10,
        (byte) 121,
        (byte) 79,
        (byte) 206,
        (byte) 113,
        (byte) 100,
        (byte) 171,
        (byte) 221,
        (byte) 126,
        (byte) 244,
        (byte) 205,
        (byte) 179,
        (byte) 185,
        (byte) 111,
        (byte) 191,
        (byte) 219,
        (byte) 15,
        (byte) 142,
        (byte) 223,
        (byte) 117,
        (byte) 151,
        (byte) 44,
        (byte) 242,
        (byte) 242,
        (byte) 120,
        (byte) 217,
        (byte) 177,
        (byte) 107,
        (byte) 145,
        (byte) 180,
        (byte) 184,
        (byte) 144 /*0x90*/,
        (byte) 126,
        (byte) 238
      };
      byte[] numArray3 = new byte[55];
      numArray3[47] = (byte) 72;
      numArray3[1] = (byte) 217;
      numArray3[2] = (byte) 98;
      numArray3[38] = (byte) 247;
      numArray3[4] = (byte) 119;
      numArray3[15] = (byte) 111;
      numArray3[50] = (byte) 248;
      numArray3[6] = (byte) 82;
      numArray3[8] = (byte) 111;
      numArray3[12] = (byte) 8;
      numArray3[10] = (byte) 169;
      numArray3[0] = (byte) 18;
      numArray3[19] = (byte) 123;
      numArray3[13] = (byte) 99;
      numArray3[14] = (byte) 161;
      numArray3[35] = (byte) 254;
      numArray3[16 /*0x10*/] = (byte) 116;
      numArray3[17] = (byte) 139;
      numArray3[18] = (byte) 8;
      numArray3[39] = (byte) 227;
      numArray3[30] = (byte) 121;
      numArray3[27] = (byte) 52;
      numArray3[54] = (byte) 194;
      numArray3[51] = (byte) 254;
      numArray3[24] = (byte) 179;
      numArray3[25] = (byte) 136;
      numArray3[26] = byte.MaxValue;
      numArray3[7] = (byte) 160 /*0xA0*/;
      numArray3[23] = (byte) 219;
      numArray3[29] = (byte) 83;
      numArray3[31 /*0x1F*/] = (byte) 48 /*0x30*/;
      numArray3[46] = (byte) 107;
      numArray3[32 /*0x20*/] = (byte) 124;
      numArray3[33] = (byte) 206;
      numArray3[5] = (byte) 123;
      numArray3[34] = (byte) 200;
      numArray3[36] = (byte) 183;
      numArray3[37] = byte.MaxValue;
      numArray3[28] = (byte) 92;
      numArray3[11] = (byte) 133;
      numArray3[40] = (byte) 163;
      numArray3[9] = (byte) 227;
      numArray3[42] = (byte) 184;
      numArray3[20] = (byte) 181;
      numArray3[44] = (byte) 68;
      numArray3[45] = (byte) 62;
      numArray3[22] = (byte) 210;
      numArray3[52] = (byte) 198;
      numArray3[48 /*0x30*/] = (byte) 109;
      numArray3[49] = (byte) 14;
      numArray3[43] = (byte) 116;
      numArray3[53] = (byte) 51;
      numArray3[3] = (byte) 251;
      numArray3[21] = (byte) 233;
      numArray3[41] = (byte) 25;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 26,
        (byte) 231,
        (byte) 0,
        (byte) 1,
        (byte) 143,
        (byte) 98,
        (byte) 85,
        (byte) 194,
        (byte) 195,
        (byte) 98,
        (byte) 108,
        (byte) 221,
        (byte) 160 /*0xA0*/,
        (byte) 178,
        (byte) 242,
        (byte) 31 /*0x1F*/,
        (byte) 2,
        (byte) 166,
        (byte) 33,
        (byte) 210,
        (byte) 147,
        (byte) 106,
        (byte) 117,
        (byte) 245,
        (byte) 234,
        (byte) 13,
        (byte) 209,
        (byte) 219,
        (byte) 153,
        (byte) 216,
        (byte) 2,
        (byte) 230,
        (byte) 133,
        (byte) 246,
        (byte) 74,
        (byte) 236,
        (byte) 245,
        (byte) 41,
        (byte) 21,
        (byte) 134,
        (byte) 182,
        (byte) 48 /*0x30*/,
        (byte) 47,
        (byte) 125,
        (byte) 54,
        (byte) 235,
        (byte) 146,
        (byte) 199,
        (byte) 252,
        (byte) 131,
        (byte) 97,
        (byte) 139,
        (byte) 195,
        (byte) 179,
        (byte) 13
      };
      byte[] numArray5 = new byte[55];
      numArray5[47] = (byte) 60;
      numArray5[31 /*0x1F*/] = (byte) 24;
      numArray5[2] = (byte) 83;
      numArray5[46] = (byte) 158;
      numArray5[29] = (byte) 112 /*0x70*/;
      numArray5[5] = (byte) 177;
      numArray5[34] = (byte) 212;
      numArray5[7] = (byte) 124;
      numArray5[8] = (byte) 191;
      numArray5[9] = (byte) 209;
      numArray5[25] = (byte) 202;
      numArray5[20] = (byte) 7;
      numArray5[12] = (byte) 18;
      numArray5[13] = (byte) 190;
      numArray5[14] = (byte) 83;
      numArray5[15] = (byte) 97;
      numArray5[16 /*0x10*/] = (byte) 190;
      numArray5[33] = (byte) 201;
      numArray5[23] = (byte) 238;
      numArray5[19] = (byte) 50;
      numArray5[28] = (byte) 46;
      numArray5[21] = (byte) 4;
      numArray5[45] = (byte) 85;
      numArray5[6] = (byte) 110;
      numArray5[24] = (byte) 181;
      numArray5[10] = (byte) 123;
      numArray5[53] = (byte) 78;
      numArray5[30] = (byte) 64 /*0x40*/;
      numArray5[0] = (byte) 149;
      numArray5[39] = (byte) 190;
      numArray5[17] = (byte) 253;
      numArray5[49] = (byte) 172;
      numArray5[36] = (byte) 204;
      numArray5[18] = (byte) 103;
      numArray5[22] = (byte) 136;
      numArray5[27] = (byte) 168;
      numArray5[43] = (byte) 104;
      numArray5[37] = (byte) 94;
      numArray5[38] = (byte) 29;
      numArray5[35] = (byte) 41;
      numArray5[40] = (byte) 159;
      numArray5[41] = (byte) 154;
      numArray5[3] = (byte) 182;
      numArray5[4] = (byte) 142;
      numArray5[44] = (byte) 88;
      numArray5[26] = (byte) 76;
      numArray5[11] = (byte) 3;
      numArray5[32 /*0x20*/] = (byte) 198;
      numArray5[48 /*0x30*/] = (byte) 141;
      numArray5[42] = (byte) 23;
      numArray5[54] = (byte) 205;
      numArray5[51] = (byte) 228;
      numArray5[52] = (byte) 2;
      numArray5[50] = (byte) 58;
      numArray5[1] = (byte) 5;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[44]
      {
        (byte) 76,
        (byte) 25,
        (byte) 5,
        (byte) 136,
        (byte) 153,
        (byte) 18,
        (byte) 114,
        (byte) 233,
        (byte) 97,
        (byte) 133,
        (byte) 193,
        (byte) 206,
        (byte) 140,
        (byte) 64 /*0x40*/,
        (byte) 199,
        (byte) 251,
        (byte) 164,
        (byte) 221,
        (byte) 91,
        (byte) 245,
        (byte) 218,
        (byte) 13,
        (byte) 107,
        (byte) 147,
        (byte) 169,
        (byte) 232,
        (byte) 189,
        (byte) 239,
        (byte) 123,
        (byte) 250,
        (byte) 58,
        (byte) 62,
        (byte) 37,
        byte.MaxValue,
        (byte) 156,
        (byte) 248,
        (byte) 142,
        (byte) 109,
        (byte) 163,
        (byte) 245,
        (byte) 211,
        (byte) 52,
        (byte) 79,
        (byte) 14
      };
      byte[] numArray7 = new byte[44]
      {
        (byte) 192 /*0xC0*/,
        (byte) 180,
        (byte) 28,
        (byte) 55,
        (byte) 101,
        (byte) 227,
        (byte) 118,
        (byte) 162,
        (byte) 244,
        (byte) 235,
        (byte) 60,
        byte.MaxValue,
        (byte) 159,
        (byte) 29,
        (byte) 249,
        (byte) 244,
        (byte) 18,
        (byte) 17,
        (byte) 85,
        (byte) 203,
        (byte) 146,
        (byte) 57,
        (byte) 155,
        (byte) 44,
        (byte) 57,
        (byte) 245,
        (byte) 173,
        (byte) 34,
        (byte) 202,
        (byte) 113,
        (byte) 29,
        (byte) 96 /*0x60*/,
        (byte) 118,
        (byte) 53,
        (byte) 127 /*0x7F*/,
        (byte) 73,
        (byte) 66,
        (byte) 128 /*0x80*/,
        (byte) 87,
        (byte) 111,
        (byte) 249,
        (byte) 216,
        (byte) 186,
        (byte) 95
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 44);
      for (int index = 0; index < 44; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[154];
    byte[] numArray9 = new byte[55]
    {
      (byte) 24,
      (byte) 192 /*0xC0*/,
      byte.MaxValue,
      (byte) 131,
      (byte) 185,
      (byte) 79,
      (byte) 127 /*0x7F*/,
      (byte) 159,
      (byte) 63 /*0x3F*/,
      (byte) 174,
      (byte) 113,
      (byte) 222,
      (byte) 177,
      (byte) 84,
      (byte) 130,
      (byte) 109,
      (byte) 103,
      (byte) 74,
      (byte) 131,
      (byte) 168,
      (byte) 140,
      (byte) 163,
      (byte) 207,
      (byte) 109,
      (byte) 55,
      (byte) 57,
      (byte) 102,
      (byte) 34,
      (byte) 165,
      (byte) 189,
      (byte) 92,
      (byte) 109,
      (byte) 24,
      (byte) 126,
      (byte) 165,
      (byte) 47,
      (byte) 123,
      (byte) 119,
      (byte) 129,
      (byte) 239,
      (byte) 166,
      (byte) 95,
      (byte) 93,
      (byte) 74,
      (byte) 28,
      (byte) 143,
      (byte) 242,
      (byte) 160 /*0xA0*/,
      (byte) 80 /*0x50*/,
      (byte) 198,
      (byte) 232,
      (byte) 195,
      (byte) 162,
      (byte) 232,
      (byte) 222
    };
    byte[] numArray10 = new byte[55];
    numArray10[5] = (byte) 183;
    numArray10[1] = (byte) 229;
    numArray10[35] = (byte) 129;
    numArray10[3] = (byte) 76;
    numArray10[41] = (byte) 235;
    numArray10[13] = (byte) 187;
    numArray10[6] = (byte) 38;
    numArray10[21] = (byte) 47;
    numArray10[8] = (byte) 138;
    numArray10[9] = (byte) 42;
    numArray10[10] = byte.MaxValue;
    numArray10[14] = (byte) 94;
    numArray10[24] = (byte) 105;
    numArray10[0] = (byte) 61;
    numArray10[40] = (byte) 44;
    numArray10[15] = (byte) 22;
    numArray10[16 /*0x10*/] = (byte) 243;
    numArray10[17] = (byte) 6;
    numArray10[18] = (byte) 240 /*0xF0*/;
    numArray10[52] = (byte) 243;
    numArray10[20] = (byte) 166;
    numArray10[38] = (byte) 113;
    numArray10[22] = (byte) 251;
    numArray10[2] = (byte) 100;
    numArray10[36] = (byte) 49;
    numArray10[49] = (byte) 249;
    numArray10[26] = (byte) 222;
    numArray10[43] = byte.MaxValue;
    numArray10[28] = (byte) 161;
    numArray10[29] = (byte) 78;
    numArray10[30] = (byte) 230;
    numArray10[31 /*0x1F*/] = (byte) 231;
    numArray10[32 /*0x20*/] = (byte) 80 /*0x50*/;
    numArray10[47] = (byte) 115;
    numArray10[34] = (byte) 103;
    numArray10[33] = (byte) 116;
    numArray10[53] = (byte) 211;
    numArray10[37] = (byte) 126;
    numArray10[11] = (byte) 174;
    numArray10[39] = (byte) 74;
    numArray10[25] = (byte) 106;
    numArray10[12] = (byte) 120;
    numArray10[4] = (byte) 32 /*0x20*/;
    numArray10[51] = (byte) 115;
    numArray10[44] = (byte) 66;
    numArray10[45] = (byte) 194;
    numArray10[46] = (byte) 127 /*0x7F*/;
    numArray10[23] = (byte) 63 /*0x3F*/;
    numArray10[48 /*0x30*/] = (byte) 232;
    numArray10[19] = (byte) 199;
    numArray10[42] = (byte) 95;
    numArray10[27] = (byte) 224 /*0xE0*/;
    numArray10[54] = (byte) 18;
    numArray10[50] = (byte) 19;
    numArray10[7] = (byte) 216;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 65,
      (byte) 163,
      (byte) 56,
      (byte) 35,
      (byte) 139,
      (byte) 26,
      (byte) 172,
      (byte) 184,
      (byte) 57,
      (byte) 149,
      (byte) 146,
      (byte) 126,
      (byte) 20,
      (byte) 8,
      (byte) 196,
      (byte) 199,
      (byte) 142,
      (byte) 183,
      (byte) 198,
      (byte) 106,
      (byte) 139,
      (byte) 55,
      (byte) 223,
      (byte) 135,
      (byte) 212,
      (byte) 211,
      (byte) 10,
      (byte) 221,
      (byte) 158,
      (byte) 188,
      (byte) 72,
      (byte) 237,
      (byte) 2,
      (byte) 210,
      (byte) 167,
      (byte) 135,
      (byte) 200,
      (byte) 226,
      (byte) 191,
      (byte) 75,
      (byte) 47,
      (byte) 64 /*0x40*/,
      (byte) 223,
      (byte) 52,
      (byte) 45,
      (byte) 114,
      (byte) 233,
      (byte) 204,
      (byte) 51,
      (byte) 50,
      (byte) 127 /*0x7F*/,
      (byte) 48 /*0x30*/,
      (byte) 235,
      (byte) 195,
      (byte) 224 /*0xE0*/
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 237,
      (byte) 131,
      (byte) 45,
      (byte) 198,
      (byte) 233,
      (byte) 0,
      (byte) 223,
      (byte) 157,
      (byte) 45,
      (byte) 200,
      (byte) 144 /*0x90*/,
      (byte) 49,
      (byte) 145,
      (byte) 191,
      (byte) 114,
      (byte) 97,
      (byte) 40,
      (byte) 53,
      (byte) 130,
      (byte) 183,
      (byte) 22,
      (byte) 123,
      (byte) 240 /*0xF0*/,
      (byte) 225,
      (byte) 175,
      (byte) 240 /*0xF0*/,
      (byte) 205,
      (byte) 18,
      (byte) 120,
      (byte) 180,
      (byte) 38,
      (byte) 69,
      (byte) 34,
      (byte) 52,
      (byte) 99,
      (byte) 83,
      (byte) 146,
      (byte) 154,
      (byte) 49,
      (byte) 71,
      (byte) 231,
      (byte) 201,
      (byte) 20,
      (byte) 216,
      (byte) 94,
      (byte) 15,
      (byte) 171,
      (byte) 69,
      (byte) 6,
      (byte) 201,
      (byte) 223,
      (byte) 77,
      (byte) 175,
      (byte) 25,
      (byte) 57
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[44];
    numArray13[19] = (byte) 88;
    numArray13[15] = (byte) 247;
    numArray13[2] = (byte) 183;
    numArray13[3] = (byte) 16 /*0x10*/;
    numArray13[4] = (byte) 207;
    numArray13[5] = (byte) 26;
    numArray13[6] = (byte) 124;
    numArray13[7] = (byte) 77;
    numArray13[18] = (byte) 75;
    numArray13[9] = (byte) 124;
    numArray13[31 /*0x1F*/] = (byte) 242;
    numArray13[36] = (byte) 8;
    numArray13[26] = (byte) 107;
    numArray13[24] = (byte) 61;
    numArray13[16 /*0x10*/] = (byte) 123;
    numArray13[35] = (byte) 148;
    numArray13[30] = (byte) 63 /*0x3F*/;
    numArray13[17] = (byte) 59;
    numArray13[40] = (byte) 98;
    numArray13[11] = (byte) 96 /*0x60*/;
    numArray13[1] = (byte) 78;
    numArray13[32 /*0x20*/] = (byte) 157;
    numArray13[42] = (byte) 136;
    numArray13[23] = (byte) 234;
    numArray13[20] = (byte) 170;
    numArray13[25] = (byte) 169;
    numArray13[12] = (byte) 61;
    numArray13[27] = (byte) 60;
    numArray13[28] = (byte) 23;
    numArray13[29] = (byte) 2;
    numArray13[38] = (byte) 95;
    numArray13[33] = (byte) 195;
    numArray13[13] = byte.MaxValue;
    numArray13[22] = (byte) 98;
    numArray13[34] = (byte) 23;
    numArray13[14] = (byte) 171;
    numArray13[10] = (byte) 65;
    numArray13[21] = (byte) 90;
    numArray13[8] = (byte) 164;
    numArray13[39] = (byte) 38;
    numArray13[0] = (byte) 177;
    numArray13[41] = (byte) 230;
    numArray13[37] = (byte) 58;
    numArray13[43] = (byte) 246;
    byte[] numArray14 = new byte[44]
    {
      (byte) 133,
      (byte) 124,
      (byte) 43,
      (byte) 175,
      (byte) 231,
      (byte) 188,
      (byte) 90,
      (byte) 15,
      (byte) 55,
      (byte) 39,
      (byte) 227,
      (byte) 105,
      (byte) 19,
      (byte) 86,
      (byte) 138,
      (byte) 225,
      (byte) 196,
      (byte) 47,
      (byte) 201,
      (byte) 184,
      (byte) 48 /*0x30*/,
      (byte) 207,
      (byte) 222,
      (byte) 124,
      (byte) 121,
      (byte) 101,
      (byte) 68,
      (byte) 168,
      (byte) 77,
      (byte) 33,
      (byte) 77,
      (byte) 107,
      (byte) 171,
      byte.MaxValue,
      (byte) 227,
      (byte) 60,
      (byte) 112 /*0x70*/,
      (byte) 36,
      (byte) 73,
      (byte) 143,
      (byte) 3,
      (byte) 65,
      (byte) 70,
      (byte) 10
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 44);
    for (int index = 0; index < 44; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static int ssp_appserver_12340(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 208 /*0xD0*/;
    sourceArray1[26] = (byte) 132;
    sourceArray1[2] = (byte) 35;
    sourceArray1[12] = (byte) 47;
    sourceArray1[1] = (byte) 120;
    sourceArray1[46] = (byte) 97;
    sourceArray1[6] = (byte) 88;
    sourceArray1[7] = (byte) 169;
    sourceArray1[15] = (byte) 188;
    sourceArray1[9] = (byte) 76;
    sourceArray1[10] = (byte) 189;
    sourceArray1[0] = (byte) 57;
    sourceArray1[32 /*0x20*/] = (byte) 180;
    sourceArray1[30] = (byte) 39;
    sourceArray1[14] = (byte) 112 /*0x70*/;
    sourceArray1[40] = (byte) 234;
    sourceArray1[5] = (byte) 185;
    sourceArray1[17] = (byte) 133;
    sourceArray1[18] = (byte) 127 /*0x7F*/;
    sourceArray1[39] = (byte) 179;
    sourceArray1[20] = (byte) 1;
    sourceArray1[16 /*0x10*/] = (byte) 85;
    sourceArray1[45] = (byte) 149;
    sourceArray1[37] = (byte) 18;
    sourceArray1[24] = (byte) 87;
    sourceArray1[22] = (byte) 118;
    sourceArray1[8] = (byte) 124;
    sourceArray1[13] = (byte) 170;
    sourceArray1[28] = (byte) 215;
    sourceArray1[43] = (byte) 235;
    sourceArray1[19] = (byte) 137;
    sourceArray1[23] = (byte) 215;
    sourceArray1[31 /*0x1F*/] = (byte) 111;
    sourceArray1[33] = (byte) 216;
    sourceArray1[36] = (byte) 210;
    sourceArray1[35] = (byte) 172;
    sourceArray1[34] = (byte) 162;
    sourceArray1[4] = (byte) 229;
    sourceArray1[38] = (byte) 236;
    sourceArray1[42] = (byte) 54;
    sourceArray1[27] = (byte) 241;
    sourceArray1[41] = (byte) 39;
    sourceArray1[11] = (byte) 73;
    sourceArray1[3] = (byte) 177;
    sourceArray1[44] = (byte) 171;
    sourceArray1[21] = (byte) 114;
    sourceArray1[25] = (byte) 17;
    sourceArray1[47] = (byte) 13;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 187,
      (byte) 15,
      (byte) 83,
      (byte) 7,
      (byte) 125,
      (byte) 121,
      (byte) 233,
      (byte) 143,
      (byte) 3,
      (byte) 11,
      (byte) 60,
      (byte) 120,
      (byte) 162,
      (byte) 120,
      (byte) 11,
      (byte) 243,
      (byte) 27,
      (byte) 212,
      (byte) 162,
      (byte) 108,
      (byte) 106,
      (byte) 97,
      (byte) 176 /*0xB0*/,
      (byte) 165,
      (byte) 99,
      (byte) 16 /*0x10*/,
      (byte) 229,
      (byte) 161,
      (byte) 105,
      (byte) 94,
      (byte) 3,
      (byte) 229,
      (byte) 0,
      (byte) 124,
      (byte) 78,
      (byte) 144 /*0x90*/,
      (byte) 201,
      (byte) 190,
      (byte) 160 /*0xA0*/,
      (byte) 52,
      (byte) 239,
      (byte) 148,
      (byte) 91,
      (byte) 168,
      (byte) 67,
      (byte) 95,
      (byte) 70,
      (byte) 240 /*0xF0*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[29];
    byte[] response2 = new byte[29];
    Array.Copy((Array) sc_12332.sspq, 175, (Array) numArray2, 0, 29);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 175, (Array) numArray2, 0, 29);
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

  internal static string ssp_appserver_12341()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[14] = (byte) 81;
      numArray2[2] = (byte) 195;
      numArray2[19] = (byte) 192 /*0xC0*/;
      numArray2[3] = (byte) 206;
      numArray2[4] = (byte) 193;
      numArray2[12] = (byte) 240 /*0xF0*/;
      numArray2[18] = (byte) 242;
      numArray2[8] = (byte) 98;
      numArray2[7] = (byte) 220;
      numArray2[9] = (byte) 107;
      numArray2[10] = (byte) 75;
      numArray2[0] = (byte) 131;
      numArray2[6] = (byte) 113;
      numArray2[15] = (byte) 51;
      numArray2[13] = (byte) 228;
      numArray2[16 /*0x10*/] = (byte) 102;
      numArray2[5] = (byte) 164;
      numArray2[17] = (byte) 33;
      numArray2[11] = (byte) 233;
      numArray2[1] = (byte) 177;
      byte[] numArray3 = new byte[20];
      numArray3[17] = (byte) 219;
      numArray3[1] = (byte) 238;
      numArray3[4] = (byte) 241;
      numArray3[7] = (byte) 16 /*0x10*/;
      numArray3[10] = (byte) 184;
      numArray3[5] = (byte) 149;
      numArray3[6] = (byte) 76;
      numArray3[13] = (byte) 24;
      numArray3[8] = (byte) 162;
      numArray3[2] = (byte) 181;
      numArray3[0] = (byte) 158;
      numArray3[19] = (byte) 31 /*0x1F*/;
      numArray3[12] = (byte) 162;
      numArray3[3] = (byte) 87;
      numArray3[14] = (byte) 204;
      numArray3[15] = (byte) 68;
      numArray3[9] = (byte) 245;
      numArray3[16 /*0x10*/] = (byte) 9;
      numArray3[18] = (byte) 229;
      numArray3[11] = (byte) 9;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[1] = (byte) 228;
    numArray5[7] = (byte) 128 /*0x80*/;
    numArray5[14] = (byte) 150;
    numArray5[3] = (byte) 219;
    numArray5[15] = (byte) 161;
    numArray5[5] = (byte) 219;
    numArray5[6] = (byte) 120;
    numArray5[4] = (byte) 249;
    numArray5[8] = (byte) 20;
    numArray5[9] = (byte) 74;
    numArray5[13] = (byte) 99;
    numArray5[2] = (byte) 42;
    numArray5[17] = (byte) 204;
    numArray5[10] = (byte) 113;
    numArray5[11] = (byte) 116;
    numArray5[12] = (byte) 157;
    numArray5[16 /*0x10*/] = (byte) 55;
    numArray5[18] = (byte) 36;
    numArray5[0] = (byte) 129;
    numArray5[19] = (byte) 254;
    byte[] numArray6 = new byte[20]
    {
      (byte) 179,
      (byte) 107,
      (byte) 113,
      byte.MaxValue,
      (byte) 89,
      (byte) 199,
      (byte) 236,
      (byte) 197,
      (byte) 208 /*0xD0*/,
      (byte) 182,
      (byte) 150,
      (byte) 73,
      (byte) 224 /*0xE0*/,
      (byte) 103,
      (byte) 186,
      (byte) 211,
      (byte) 74,
      (byte) 110,
      (byte) 57,
      (byte) 38
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12342()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[55];
      byte[] numArray2 = new byte[55]
      {
        (byte) 28,
        (byte) 24,
        (byte) 92,
        (byte) 244,
        (byte) 192 /*0xC0*/,
        (byte) 147,
        (byte) 64 /*0x40*/,
        (byte) 135,
        (byte) 91,
        (byte) 154,
        (byte) 166,
        (byte) 156,
        (byte) 198,
        (byte) 152,
        (byte) 90,
        (byte) 253,
        (byte) 42,
        (byte) 247,
        (byte) 3,
        (byte) 59,
        (byte) 200,
        (byte) 112 /*0x70*/,
        (byte) 193,
        (byte) 230,
        (byte) 29,
        (byte) 182,
        (byte) 223,
        (byte) 245,
        (byte) 35,
        (byte) 254,
        (byte) 156,
        (byte) 194,
        (byte) 252,
        (byte) 0,
        (byte) 116,
        (byte) 104,
        (byte) 159,
        (byte) 188,
        (byte) 193,
        (byte) 234,
        (byte) 152,
        (byte) 251,
        (byte) 166,
        (byte) 24,
        (byte) 15,
        (byte) 170,
        (byte) 25,
        (byte) 41,
        (byte) 231,
        (byte) 170,
        (byte) 251,
        (byte) 106,
        (byte) 98,
        (byte) 172,
        (byte) 85
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 147,
        (byte) 177,
        (byte) 202,
        (byte) 164,
        (byte) 16 /*0x10*/,
        (byte) 211,
        (byte) 133,
        (byte) 87,
        (byte) 171,
        (byte) 236,
        (byte) 183,
        (byte) 248,
        (byte) 59,
        (byte) 121,
        (byte) 167,
        (byte) 132,
        (byte) 37,
        (byte) 16 /*0x10*/,
        (byte) 249,
        (byte) 136,
        (byte) 8,
        (byte) 218,
        (byte) 204,
        (byte) 240 /*0xF0*/,
        (byte) 117,
        (byte) 126,
        (byte) 137,
        (byte) 235,
        (byte) 88,
        (byte) 201,
        (byte) 97,
        (byte) 170,
        (byte) 144 /*0x90*/,
        (byte) 119,
        (byte) 0,
        (byte) 6,
        (byte) 199,
        (byte) 44,
        (byte) 143,
        (byte) 199,
        (byte) 50,
        (byte) 180,
        (byte) 151,
        (byte) 22,
        (byte) 205,
        (byte) 119,
        (byte) 193,
        (byte) 67,
        (byte) 19,
        (byte) 164,
        (byte) 230,
        (byte) 174,
        (byte) 96 /*0x60*/,
        (byte) 1,
        (byte) 75
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[32 /*0x20*/];
      byte[] response = new byte[32 /*0x20*/];
      Array.Copy((Array) sc_12332.sspq, 204, (Array) numArray4, 0, 32 /*0x20*/);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12332.sspr, 204, (Array) numArray4, 0, 32 /*0x20*/);
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
    byte[] numArray5 = new byte[55];
    byte[] numArray6 = new byte[55];
    numArray6[44] = (byte) 74;
    numArray6[21] = (byte) 158;
    numArray6[30] = (byte) 79;
    numArray6[3] = (byte) 53;
    numArray6[54] = (byte) 98;
    numArray6[47] = (byte) 43;
    numArray6[5] = (byte) 152;
    numArray6[7] = (byte) 26;
    numArray6[8] = (byte) 42;
    numArray6[28] = (byte) 203;
    numArray6[16 /*0x10*/] = (byte) 73;
    numArray6[50] = (byte) 22;
    numArray6[2] = (byte) 183;
    numArray6[13] = (byte) 77;
    numArray6[14] = (byte) 140;
    numArray6[15] = (byte) 52;
    numArray6[41] = (byte) 142;
    numArray6[10] = (byte) 82;
    numArray6[31 /*0x1F*/] = (byte) 28;
    numArray6[19] = (byte) 226;
    numArray6[0] = (byte) 42;
    numArray6[20] = (byte) 174;
    numArray6[22] = (byte) 100;
    numArray6[23] = (byte) 107;
    numArray6[24] = byte.MaxValue;
    numArray6[25] = (byte) 61;
    numArray6[26] = (byte) 234;
    numArray6[40] = (byte) 128 /*0x80*/;
    numArray6[27] = (byte) 88;
    numArray6[29] = (byte) 132;
    numArray6[51] = (byte) 72;
    numArray6[18] = (byte) 137;
    numArray6[11] = (byte) 5;
    numArray6[1] = (byte) 228;
    numArray6[52] = (byte) 32 /*0x20*/;
    numArray6[35] = (byte) 138;
    numArray6[36] = (byte) 126;
    numArray6[37] = (byte) 227;
    numArray6[38] = (byte) 87;
    numArray6[39] = (byte) 52;
    numArray6[46] = (byte) 24;
    numArray6[45] = (byte) 175;
    numArray6[42] = (byte) 172;
    numArray6[43] = (byte) 104;
    numArray6[6] = (byte) 73;
    numArray6[33] = (byte) 225;
    numArray6[4] = (byte) 76;
    numArray6[17] = (byte) 8;
    numArray6[48 /*0x30*/] = (byte) 119;
    numArray6[49] = (byte) 253;
    numArray6[32 /*0x20*/] = (byte) 91;
    numArray6[12] = (byte) 245;
    numArray6[9] = (byte) 10;
    numArray6[53] = (byte) 229;
    numArray6[34] = (byte) 51;
    byte[] numArray7 = new byte[55];
    numArray7[54] = (byte) 147;
    numArray7[1] = (byte) 89;
    numArray7[24] = (byte) 66;
    numArray7[33] = (byte) 8;
    numArray7[4] = (byte) 239;
    numArray7[39] = (byte) 146;
    numArray7[13] = (byte) 140;
    numArray7[7] = (byte) 161;
    numArray7[8] = (byte) 36;
    numArray7[9] = (byte) 116;
    numArray7[5] = (byte) 99;
    numArray7[11] = (byte) 73;
    numArray7[22] = (byte) 14;
    numArray7[36] = (byte) 228;
    numArray7[14] = (byte) 142;
    numArray7[15] = (byte) 198;
    numArray7[16 /*0x10*/] = (byte) 233;
    numArray7[34] = (byte) 233;
    numArray7[0] = (byte) 162;
    numArray7[23] = (byte) 110;
    numArray7[30] = (byte) 27;
    numArray7[44] = (byte) 241;
    numArray7[6] = (byte) 203;
    numArray7[21] = (byte) 244;
    numArray7[38] = (byte) 99;
    numArray7[25] = (byte) 222;
    numArray7[26] = (byte) 63 /*0x3F*/;
    numArray7[27] = (byte) 127 /*0x7F*/;
    numArray7[41] = (byte) 13;
    numArray7[20] = (byte) 123;
    numArray7[35] = (byte) 229;
    numArray7[31 /*0x1F*/] = (byte) 145;
    numArray7[32 /*0x20*/] = (byte) 131;
    numArray7[42] = (byte) 47;
    numArray7[37] = (byte) 24;
    numArray7[12] = (byte) 31 /*0x1F*/;
    numArray7[52] = (byte) 169;
    numArray7[28] = (byte) 131;
    numArray7[29] = (byte) 53;
    numArray7[19] = (byte) 74;
    numArray7[40] = (byte) 180;
    numArray7[18] = (byte) 26;
    numArray7[3] = (byte) 59;
    numArray7[43] = (byte) 53;
    numArray7[10] = (byte) 40;
    numArray7[45] = (byte) 26;
    numArray7[46] = (byte) 221;
    numArray7[47] = (byte) 229;
    numArray7[48 /*0x30*/] = (byte) 225;
    numArray7[49] = (byte) 21;
    numArray7[50] = (byte) 39;
    numArray7[51] = (byte) 25;
    numArray7[2] = (byte) 123;
    numArray7[53] = (byte) 68;
    numArray7[17] = (byte) 247;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12343(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[27] = (byte) 63 /*0x3F*/;
    sourceArray1[45] = (byte) 5;
    sourceArray1[2] = (byte) 251;
    sourceArray1[18] = (byte) 52;
    sourceArray1[4] = (byte) 69;
    sourceArray1[5] = (byte) 11;
    sourceArray1[43] = (byte) 9;
    sourceArray1[7] = (byte) 86;
    sourceArray1[8] = (byte) 101;
    sourceArray1[9] = (byte) 114;
    sourceArray1[11] = (byte) 243;
    sourceArray1[46] = (byte) 36;
    sourceArray1[0] = (byte) 65;
    sourceArray1[13] = (byte) 55;
    sourceArray1[14] = (byte) 80 /*0x50*/;
    sourceArray1[15] = (byte) 33;
    sourceArray1[3] = (byte) 124;
    sourceArray1[12] = (byte) 10;
    sourceArray1[10] = (byte) 124;
    sourceArray1[31 /*0x1F*/] = (byte) 233;
    sourceArray1[28] = (byte) 219;
    sourceArray1[21] = (byte) 15;
    sourceArray1[22] = (byte) 241;
    sourceArray1[23] = (byte) 193;
    sourceArray1[24] = (byte) 195;
    sourceArray1[25] = (byte) 65;
    sourceArray1[26] = (byte) 50;
    sourceArray1[42] = (byte) 56;
    sourceArray1[16 /*0x10*/] = (byte) 28;
    sourceArray1[20] = (byte) 174;
    sourceArray1[6] = (byte) 65;
    sourceArray1[36] = (byte) 195;
    sourceArray1[32 /*0x20*/] = (byte) 39;
    sourceArray1[33] = (byte) 149;
    sourceArray1[34] = (byte) 86;
    sourceArray1[19] = (byte) 118;
    sourceArray1[35] = (byte) 224 /*0xE0*/;
    sourceArray1[41] = (byte) 26;
    sourceArray1[37] = (byte) 81;
    sourceArray1[39] = (byte) 204;
    sourceArray1[40] = (byte) 144 /*0x90*/;
    sourceArray1[38] = (byte) 178;
    sourceArray1[29] = (byte) 93;
    sourceArray1[44] = (byte) 71;
    sourceArray1[17] = (byte) 87;
    sourceArray1[1] = (byte) 161;
    sourceArray1[30] = (byte) 81;
    sourceArray1[47] = (byte) 197;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = (byte) 8;
    sourceArray2[6] = (byte) 137;
    sourceArray2[2] = (byte) 165;
    sourceArray2[3] = (byte) 89;
    sourceArray2[4] = (byte) 238;
    sourceArray2[5] = (byte) 128 /*0x80*/;
    sourceArray2[27] = (byte) 174;
    sourceArray2[11] = (byte) 64 /*0x40*/;
    sourceArray2[8] = (byte) 239;
    sourceArray2[9] = (byte) 0;
    sourceArray2[45] = (byte) 172;
    sourceArray2[0] = (byte) 7;
    sourceArray2[12] = (byte) 5;
    sourceArray2[26] = (byte) 87;
    sourceArray2[46] = (byte) 28;
    sourceArray2[15] = (byte) 95;
    sourceArray2[18] = (byte) 29;
    sourceArray2[30] = (byte) 84;
    sourceArray2[1] = (byte) 83;
    sourceArray2[37] = (byte) 196;
    sourceArray2[35] = (byte) 193;
    sourceArray2[21] = (byte) 224 /*0xE0*/;
    sourceArray2[14] = (byte) 129;
    sourceArray2[19] = (byte) 198;
    sourceArray2[23] = (byte) 17;
    sourceArray2[25] = (byte) 235;
    sourceArray2[20] = (byte) 172;
    sourceArray2[39] = (byte) 35;
    sourceArray2[28] = (byte) 103;
    sourceArray2[43] = (byte) 141;
    sourceArray2[29] = (byte) 19;
    sourceArray2[22] = (byte) 223;
    sourceArray2[32 /*0x20*/] = (byte) 102;
    sourceArray2[33] = (byte) 133;
    sourceArray2[40] = (byte) 254;
    sourceArray2[34] = (byte) 18;
    sourceArray2[17] = (byte) 169;
    sourceArray2[16 /*0x10*/] = (byte) 149;
    sourceArray2[38] = (byte) 241;
    sourceArray2[7] = (byte) 156;
    sourceArray2[10] = (byte) 130;
    sourceArray2[41] = (byte) 4;
    sourceArray2[13] = (byte) 63 /*0x3F*/;
    sourceArray2[36] = (byte) 209;
    sourceArray2[44] = (byte) 177;
    sourceArray2[31 /*0x1F*/] = (byte) 144 /*0x90*/;
    sourceArray2[42] = (byte) 152;
    sourceArray2[24] = (byte) 192 /*0xC0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[33];
    byte[] response2 = new byte[33];
    Array.Copy((Array) sc_12332.sspq, 236, (Array) numArray2, 0, 33);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12332.sspr, 236, (Array) numArray2, 0, 33);
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

  internal static int ssp_appserver_12344(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 237,
      (byte) 235,
      (byte) 52,
      (byte) 180,
      (byte) 191,
      (byte) 165,
      (byte) 22,
      (byte) 183,
      (byte) 93,
      (byte) 59,
      (byte) 172,
      (byte) 112 /*0x70*/,
      (byte) 249,
      (byte) 113,
      (byte) 210,
      (byte) 96 /*0x60*/,
      (byte) 25,
      (byte) 156,
      (byte) 206,
      (byte) 38,
      (byte) 78,
      (byte) 162,
      (byte) 52,
      (byte) 152,
      (byte) 186,
      (byte) 206,
      (byte) 10,
      (byte) 95,
      (byte) 174,
      (byte) 57,
      (byte) 215,
      byte.MaxValue,
      (byte) 254,
      (byte) 21,
      (byte) 31 /*0x1F*/,
      byte.MaxValue,
      (byte) 94,
      (byte) 253,
      (byte) 11,
      (byte) 85,
      (byte) 96 /*0x60*/,
      (byte) 193,
      (byte) 135,
      (byte) 251,
      (byte) 122,
      (byte) 200,
      (byte) 117,
      (byte) 164
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 235,
      (byte) 30,
      (byte) 104,
      byte.MaxValue,
      (byte) 64 /*0x40*/,
      (byte) 67,
      (byte) 4,
      (byte) 48 /*0x30*/,
      (byte) 11,
      (byte) 63 /*0x3F*/,
      (byte) 25,
      (byte) 169,
      (byte) 145,
      (byte) 88,
      (byte) 184,
      (byte) 176 /*0xB0*/,
      (byte) 24,
      (byte) 32 /*0x20*/,
      (byte) 28,
      (byte) 91,
      (byte) 232,
      (byte) 105,
      (byte) 217,
      (byte) 235,
      (byte) 183,
      (byte) 207,
      (byte) 198,
      (byte) 224 /*0xE0*/,
      (byte) 180,
      (byte) 91,
      (byte) 96 /*0x60*/,
      (byte) 44,
      (byte) 171,
      (byte) 33,
      (byte) 242,
      (byte) 140,
      (byte) 2,
      (byte) 70,
      (byte) 34,
      (byte) 33,
      (byte) 44,
      (byte) 171,
      (byte) 201,
      (byte) 24,
      (byte) 67,
      (byte) 0,
      (byte) 201,
      (byte) 35
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12345()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[288];
      byte[] numArray2 = new byte[55]
      {
        (byte) 62,
        (byte) 110,
        (byte) 223,
        (byte) 75,
        (byte) 183,
        (byte) 156,
        (byte) 250,
        (byte) 240 /*0xF0*/,
        (byte) 154,
        (byte) 183,
        (byte) 8,
        (byte) 36,
        (byte) 99,
        (byte) 173,
        (byte) 113,
        (byte) 228,
        (byte) 82,
        (byte) 211,
        (byte) 2,
        (byte) 27,
        (byte) 145,
        (byte) 120,
        (byte) 197,
        (byte) 140,
        (byte) 26,
        (byte) 56,
        (byte) 30,
        (byte) 242,
        (byte) 89,
        (byte) 102,
        (byte) 35,
        (byte) 9,
        (byte) 97,
        (byte) 155,
        (byte) 193,
        (byte) 144 /*0x90*/,
        (byte) 53,
        (byte) 211,
        (byte) 71,
        (byte) 194,
        (byte) 3,
        (byte) 26,
        (byte) 186,
        (byte) 169,
        (byte) 184,
        (byte) 98,
        (byte) 152,
        (byte) 204,
        (byte) 86,
        (byte) 144 /*0x90*/,
        (byte) 82,
        (byte) 103,
        (byte) 163,
        (byte) 187,
        (byte) 171
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 170,
        (byte) 154,
        (byte) 105,
        (byte) 6,
        (byte) 151,
        (byte) 56,
        (byte) 8,
        (byte) 117,
        (byte) 94,
        (byte) 170,
        (byte) 54,
        (byte) 42,
        (byte) 198,
        (byte) 164,
        (byte) 83,
        (byte) 218,
        (byte) 90,
        (byte) 249,
        (byte) 150,
        (byte) 24,
        (byte) 62,
        (byte) 200,
        (byte) 152,
        (byte) 74,
        (byte) 169,
        (byte) 108,
        (byte) 245,
        (byte) 144 /*0x90*/,
        (byte) 66,
        (byte) 215,
        (byte) 187,
        (byte) 112 /*0x70*/,
        (byte) 249,
        (byte) 92,
        (byte) 57,
        (byte) 245,
        (byte) 134,
        (byte) 142,
        (byte) 85,
        (byte) 86,
        (byte) 64 /*0x40*/,
        (byte) 89,
        (byte) 20,
        (byte) 142,
        (byte) 47,
        (byte) 137,
        (byte) 92,
        (byte) 27,
        (byte) 143,
        (byte) 209,
        (byte) 51,
        (byte) 223,
        (byte) 30,
        (byte) 224 /*0xE0*/,
        (byte) 165
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[43] = (byte) 160 /*0xA0*/;
      numArray4[15] = (byte) 139;
      numArray4[20] = (byte) 28;
      numArray4[3] = (byte) 125;
      numArray4[4] = (byte) 7;
      numArray4[5] = (byte) 151;
      numArray4[7] = (byte) 147;
      numArray4[37] = (byte) 68;
      numArray4[22] = (byte) 184;
      numArray4[9] = (byte) 194;
      numArray4[10] = (byte) 200;
      numArray4[36] = (byte) 200;
      numArray4[12] = (byte) 225;
      numArray4[33] = (byte) 230;
      numArray4[28] = (byte) 187;
      numArray4[46] = (byte) 117;
      numArray4[42] = (byte) 185;
      numArray4[53] = (byte) 7;
      numArray4[18] = (byte) 119;
      numArray4[19] = (byte) 190;
      numArray4[31 /*0x1F*/] = (byte) 239;
      numArray4[21] = (byte) 211;
      numArray4[17] = (byte) 24;
      numArray4[47] = (byte) 225;
      numArray4[24] = (byte) 81;
      numArray4[6] = (byte) 52;
      numArray4[26] = (byte) 201;
      numArray4[27] = (byte) 20;
      numArray4[11] = (byte) 235;
      numArray4[29] = (byte) 102;
      numArray4[30] = (byte) 166;
      numArray4[25] = (byte) 155;
      numArray4[49] = (byte) 220;
      numArray4[2] = (byte) 127 /*0x7F*/;
      numArray4[32 /*0x20*/] = (byte) 252;
      numArray4[35] = (byte) 163;
      numArray4[54] = (byte) 141;
      numArray4[16 /*0x10*/] = (byte) 64 /*0x40*/;
      numArray4[34] = (byte) 226;
      numArray4[39] = (byte) 130;
      numArray4[40] = (byte) 133;
      numArray4[41] = (byte) 21;
      numArray4[0] = (byte) 83;
      numArray4[14] = (byte) 126;
      numArray4[44] = (byte) 207;
      numArray4[45] = (byte) 225;
      numArray4[38] = (byte) 251;
      numArray4[51] = (byte) 162;
      numArray4[48 /*0x30*/] = (byte) 211;
      numArray4[1] = (byte) 48 /*0x30*/;
      numArray4[50] = (byte) 98;
      numArray4[23] = (byte) 160 /*0xA0*/;
      numArray4[52] = (byte) 52;
      numArray4[8] = byte.MaxValue;
      numArray4[13] = (byte) 175;
      byte[] numArray5 = new byte[55]
      {
        (byte) 226,
        (byte) 106,
        (byte) 31 /*0x1F*/,
        (byte) 198,
        (byte) 126,
        (byte) 73,
        (byte) 97,
        (byte) 215,
        (byte) 29,
        (byte) 168,
        (byte) 111,
        (byte) 78,
        (byte) 220,
        (byte) 126,
        (byte) 66,
        (byte) 77,
        (byte) 30,
        (byte) 10,
        (byte) 17,
        (byte) 1,
        (byte) 148,
        (byte) 121,
        (byte) 155,
        (byte) 230,
        (byte) 199,
        (byte) 174,
        (byte) 39,
        (byte) 97,
        (byte) 90,
        (byte) 49,
        (byte) 101,
        (byte) 186,
        (byte) 96 /*0x60*/,
        (byte) 155,
        (byte) 25,
        (byte) 99,
        (byte) 83,
        (byte) 69,
        (byte) 189,
        (byte) 146,
        (byte) 66,
        (byte) 126,
        (byte) 89,
        (byte) 98,
        (byte) 112 /*0x70*/,
        (byte) 166,
        (byte) 83,
        (byte) 74,
        (byte) 34,
        (byte) 191,
        (byte) 184,
        byte.MaxValue,
        (byte) 161,
        (byte) 39,
        (byte) 121
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 15,
        (byte) 188,
        (byte) 70,
        (byte) 230,
        (byte) 224 /*0xE0*/,
        (byte) 248,
        (byte) 149,
        (byte) 24,
        (byte) 29,
        (byte) 250,
        (byte) 160 /*0xA0*/,
        (byte) 106,
        (byte) 176 /*0xB0*/,
        (byte) 217,
        (byte) 226,
        (byte) 142,
        (byte) 63 /*0x3F*/,
        (byte) 211,
        (byte) 199,
        (byte) 249,
        (byte) 148,
        (byte) 216,
        (byte) 125,
        (byte) 170,
        (byte) 210,
        (byte) 72,
        (byte) 198,
        (byte) 155,
        (byte) 126,
        (byte) 123,
        (byte) 38,
        (byte) 210,
        (byte) 252,
        (byte) 202,
        (byte) 218,
        (byte) 92,
        (byte) 71,
        (byte) 205,
        (byte) 132,
        (byte) 70,
        (byte) 138,
        (byte) 107,
        (byte) 217,
        (byte) 248,
        (byte) 218,
        (byte) 170,
        (byte) 71,
        (byte) 63 /*0x3F*/,
        (byte) 132,
        (byte) 246,
        (byte) 0,
        (byte) 250,
        (byte) 191,
        (byte) 107,
        (byte) 204
      };
      byte[] numArray7 = new byte[55];
      numArray7[49] = (byte) 168;
      numArray7[17] = (byte) 176 /*0xB0*/;
      numArray7[2] = (byte) 233;
      numArray7[42] = (byte) 55;
      numArray7[28] = (byte) 45;
      numArray7[3] = (byte) 116;
      numArray7[45] = (byte) 205;
      numArray7[48 /*0x30*/] = (byte) 101;
      numArray7[22] = (byte) 60;
      numArray7[9] = (byte) 66;
      numArray7[10] = (byte) 230;
      numArray7[21] = (byte) 32 /*0x20*/;
      numArray7[12] = (byte) 106;
      numArray7[13] = (byte) 179;
      numArray7[39] = (byte) 17;
      numArray7[5] = (byte) 185;
      numArray7[16 /*0x10*/] = (byte) 88;
      numArray7[29] = (byte) 243;
      numArray7[18] = (byte) 88;
      numArray7[19] = (byte) 89;
      numArray7[20] = (byte) 9;
      numArray7[37] = (byte) 86;
      numArray7[35] = (byte) 236;
      numArray7[23] = (byte) 35;
      numArray7[40] = (byte) 27;
      numArray7[8] = (byte) 151;
      numArray7[0] = (byte) 86;
      numArray7[27] = (byte) 60;
      numArray7[43] = (byte) 178;
      numArray7[34] = (byte) 99;
      numArray7[30] = (byte) 117;
      numArray7[31 /*0x1F*/] = (byte) 236;
      numArray7[24] = (byte) 245;
      numArray7[7] = (byte) 219;
      numArray7[46] = (byte) 139;
      numArray7[38] = (byte) 39;
      numArray7[36] = (byte) 207;
      numArray7[1] = (byte) 159;
      numArray7[26] = (byte) 120;
      numArray7[33] = (byte) 11;
      numArray7[4] = (byte) 38;
      numArray7[41] = (byte) 88;
      numArray7[14] = (byte) 25;
      numArray7[50] = (byte) 162;
      numArray7[44] = byte.MaxValue;
      numArray7[15] = (byte) 28;
      numArray7[32 /*0x20*/] = (byte) 107;
      numArray7[6] = (byte) 108;
      numArray7[11] = (byte) 232;
      numArray7[25] = (byte) 68;
      numArray7[47] = (byte) 220;
      numArray7[51] = (byte) 23;
      numArray7[52] = (byte) 118;
      numArray7[53] = (byte) 246;
      numArray7[54] = (byte) 44;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 204,
        (byte) 236,
        (byte) 150,
        (byte) 171,
        (byte) 227,
        (byte) 18,
        (byte) 39,
        (byte) 168,
        (byte) 186,
        (byte) 220,
        (byte) 231,
        (byte) 90,
        (byte) 104,
        (byte) 156,
        (byte) 184,
        (byte) 98,
        (byte) 109,
        (byte) 223,
        (byte) 86,
        (byte) 198,
        (byte) 97,
        (byte) 196,
        (byte) 234,
        (byte) 159,
        (byte) 69,
        (byte) 24,
        (byte) 216,
        (byte) 236,
        (byte) 128 /*0x80*/,
        (byte) 214,
        (byte) 148,
        (byte) 90,
        (byte) 108,
        (byte) 100,
        (byte) 222,
        (byte) 205,
        (byte) 86,
        (byte) 105,
        (byte) 172,
        (byte) 192 /*0xC0*/,
        (byte) 16 /*0x10*/,
        (byte) 123,
        (byte) 81,
        (byte) 97,
        (byte) 122,
        (byte) 230,
        (byte) 149,
        (byte) 19,
        (byte) 214,
        (byte) 72,
        (byte) 208 /*0xD0*/,
        (byte) 28,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 87
      };
      byte[] numArray9 = new byte[55];
      numArray9[28] = (byte) 52;
      numArray9[0] = (byte) 181;
      numArray9[53] = (byte) 33;
      numArray9[5] = (byte) 185;
      numArray9[2] = (byte) 237;
      numArray9[41] = (byte) 187;
      numArray9[20] = (byte) 199;
      numArray9[7] = (byte) 234;
      numArray9[12] = (byte) 156;
      numArray9[36] = (byte) 99;
      numArray9[10] = (byte) 127 /*0x7F*/;
      numArray9[11] = (byte) 108;
      numArray9[3] = (byte) 14;
      numArray9[33] = (byte) 17;
      numArray9[14] = (byte) 56;
      numArray9[32 /*0x20*/] = (byte) 174;
      numArray9[16 /*0x10*/] = (byte) 68;
      numArray9[52] = (byte) 179;
      numArray9[6] = (byte) 119;
      numArray9[19] = (byte) 7;
      numArray9[37] = (byte) 147;
      numArray9[21] = (byte) 27;
      numArray9[22] = (byte) 0;
      numArray9[30] = (byte) 113;
      numArray9[24] = (byte) 62;
      numArray9[4] = (byte) 158;
      numArray9[26] = (byte) 51;
      numArray9[27] = (byte) 29;
      numArray9[34] = (byte) 236;
      numArray9[29] = (byte) 249;
      numArray9[23] = (byte) 167;
      numArray9[48 /*0x30*/] = (byte) 248;
      numArray9[17] = (byte) 8;
      numArray9[46] = (byte) 69;
      numArray9[31 /*0x1F*/] = (byte) 204;
      numArray9[35] = (byte) 8;
      numArray9[13] = (byte) 145;
      numArray9[8] = (byte) 141;
      numArray9[38] = (byte) 167;
      numArray9[39] = (byte) 195;
      numArray9[40] = (byte) 71;
      numArray9[1] = (byte) 120;
      numArray9[42] = (byte) 72;
      numArray9[43] = (byte) 38;
      numArray9[44] = (byte) 170;
      numArray9[45] = (byte) 235;
      numArray9[54] = (byte) 85;
      numArray9[9] = (byte) 93;
      numArray9[18] = (byte) 197;
      numArray9[49] = (byte) 168;
      numArray9[50] = (byte) 26;
      numArray9[15] = (byte) 6;
      numArray9[47] = (byte) 3;
      numArray9[51] = (byte) 223;
      numArray9[25] = (byte) 16 /*0x10*/;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 210,
        (byte) 6,
        (byte) 71,
        (byte) 116,
        (byte) 37,
        (byte) 239,
        (byte) 222,
        (byte) 207,
        (byte) 127 /*0x7F*/,
        (byte) 242,
        (byte) 248,
        (byte) 230,
        (byte) 30,
        (byte) 172,
        (byte) 0,
        (byte) 81,
        (byte) 15,
        (byte) 224 /*0xE0*/,
        (byte) 146,
        (byte) 6,
        (byte) 112 /*0x70*/,
        (byte) 54,
        (byte) 103,
        (byte) 241,
        (byte) 110,
        (byte) 41,
        (byte) 251,
        (byte) 57,
        (byte) 142,
        (byte) 84,
        (byte) 65,
        (byte) 154,
        (byte) 65,
        (byte) 83,
        (byte) 141,
        (byte) 252,
        (byte) 153,
        (byte) 17,
        (byte) 176 /*0xB0*/,
        (byte) 99,
        (byte) 141,
        (byte) 73,
        (byte) 110,
        (byte) 5,
        (byte) 152,
        (byte) 111,
        (byte) 204,
        (byte) 143,
        (byte) 203,
        (byte) 151,
        (byte) 168,
        (byte) 217,
        (byte) 223,
        (byte) 142,
        (byte) 8
      };
      byte[] numArray11 = new byte[55];
      numArray11[42] = (byte) 131;
      numArray11[43] = (byte) 130;
      numArray11[14] = (byte) 3;
      numArray11[27] = (byte) 171;
      numArray11[29] = (byte) 51;
      numArray11[5] = (byte) 170;
      numArray11[45] = (byte) 28;
      numArray11[7] = (byte) 200;
      numArray11[36] = (byte) 21;
      numArray11[9] = (byte) 227;
      numArray11[10] = (byte) 46;
      numArray11[11] = (byte) 202;
      numArray11[12] = (byte) 206;
      numArray11[13] = (byte) 21;
      numArray11[1] = (byte) 38;
      numArray11[15] = (byte) 150;
      numArray11[16 /*0x10*/] = (byte) 54;
      numArray11[17] = (byte) 156;
      numArray11[18] = (byte) 171;
      numArray11[19] = (byte) 86;
      numArray11[51] = (byte) 81;
      numArray11[21] = (byte) 143;
      numArray11[6] = (byte) 70;
      numArray11[54] = (byte) 197;
      numArray11[25] = (byte) 9;
      numArray11[52] = (byte) 245;
      numArray11[26] = (byte) 5;
      numArray11[31 /*0x1F*/] = (byte) 247;
      numArray11[34] = (byte) 84;
      numArray11[8] = (byte) 123;
      numArray11[30] = (byte) 6;
      numArray11[4] = (byte) 30;
      numArray11[37] = (byte) 170;
      numArray11[33] = (byte) 112 /*0x70*/;
      numArray11[20] = (byte) 49;
      numArray11[24] = (byte) 152;
      numArray11[47] = (byte) 125;
      numArray11[48 /*0x30*/] = (byte) 158;
      numArray11[46] = (byte) 88;
      numArray11[39] = (byte) 113;
      numArray11[3] = (byte) 139;
      numArray11[38] = (byte) 92;
      numArray11[50] = (byte) 164;
      numArray11[2] = (byte) 82;
      numArray11[44] = (byte) 220;
      numArray11[35] = (byte) 105;
      numArray11[0] = byte.MaxValue;
      numArray11[23] = (byte) 125;
      numArray11[41] = (byte) 158;
      numArray11[22] = (byte) 193;
      numArray11[28] = (byte) 16 /*0x10*/;
      numArray11[40] = (byte) 253;
      numArray11[32 /*0x20*/] = (byte) 16 /*0x10*/;
      numArray11[53] = (byte) 230;
      numArray11[49] = (byte) 152;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[13];
      numArray12[1] = (byte) 6;
      numArray12[0] = (byte) 252;
      numArray12[12] = (byte) 168;
      numArray12[2] = (byte) 158;
      numArray12[4] = (byte) 54;
      numArray12[5] = (byte) 74;
      numArray12[6] = (byte) 127 /*0x7F*/;
      numArray12[11] = (byte) 45;
      numArray12[8] = (byte) 163;
      numArray12[3] = (byte) 135;
      numArray12[10] = (byte) 129;
      numArray12[7] = (byte) 74;
      numArray12[9] = (byte) 241;
      byte[] numArray13 = new byte[13]
      {
        (byte) 185,
        (byte) 48 /*0x30*/,
        (byte) 244,
        (byte) 32 /*0x20*/,
        (byte) 43,
        (byte) 39,
        (byte) 107,
        (byte) 104,
        (byte) 52,
        (byte) 197,
        (byte) 244,
        (byte) 141,
        (byte) 85
      };
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 275] ^= numArray13[index];
      byte[] numArray14 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_12332.sspq, 269, (Array) numArray14, 0, 43);
      key.Query(true, 335, numArray14, response);
      Array.Copy((Array) sc_12332.sspr, 269, (Array) numArray14, 0, 43);
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
    byte[] numArray15 = new byte[288];
    byte[] numArray16 = new byte[55]
    {
      (byte) 97,
      (byte) 195,
      (byte) 5,
      (byte) 136,
      (byte) 92,
      (byte) 219,
      (byte) 187,
      (byte) 137,
      (byte) 170,
      (byte) 72,
      (byte) 143,
      (byte) 23,
      (byte) 22,
      (byte) 166,
      (byte) 211,
      (byte) 239,
      (byte) 105,
      (byte) 52,
      (byte) 211,
      (byte) 94,
      (byte) 36,
      (byte) 137,
      (byte) 68,
      (byte) 124,
      (byte) 253,
      (byte) 131,
      (byte) 104,
      (byte) 51,
      (byte) 148,
      (byte) 63 /*0x3F*/,
      (byte) 204,
      (byte) 201,
      (byte) 27,
      (byte) 169,
      (byte) 97,
      (byte) 136,
      (byte) 41,
      (byte) 85,
      (byte) 116,
      (byte) 102,
      (byte) 206,
      (byte) 4,
      (byte) 137,
      (byte) 53,
      (byte) 252,
      (byte) 129,
      (byte) 62,
      (byte) 113,
      (byte) 58,
      (byte) 153,
      (byte) 194,
      (byte) 183,
      (byte) 144 /*0x90*/,
      (byte) 119,
      (byte) 183
    };
    byte[] numArray17 = new byte[55]
    {
      (byte) 196,
      (byte) 98,
      (byte) 11,
      (byte) 74,
      (byte) 208 /*0xD0*/,
      (byte) 108,
      (byte) 195,
      (byte) 101,
      (byte) 139,
      (byte) 27,
      (byte) 28,
      (byte) 100,
      (byte) 162,
      (byte) 207,
      (byte) 251,
      (byte) 207,
      (byte) 245,
      (byte) 112 /*0x70*/,
      (byte) 224 /*0xE0*/,
      (byte) 6,
      (byte) 184,
      (byte) 159,
      (byte) 89,
      (byte) 193,
      (byte) 239,
      (byte) 243,
      (byte) 45,
      (byte) 34,
      (byte) 32 /*0x20*/,
      (byte) 225,
      (byte) 160 /*0xA0*/,
      (byte) 45,
      byte.MaxValue,
      (byte) 224 /*0xE0*/,
      (byte) 204,
      (byte) 179,
      (byte) 72,
      (byte) 24,
      (byte) 145,
      (byte) 5,
      (byte) 83,
      (byte) 85,
      (byte) 247,
      (byte) 249,
      (byte) 35,
      (byte) 222,
      (byte) 18,
      (byte) 152,
      (byte) 187,
      (byte) 234,
      (byte) 90,
      (byte) 52,
      (byte) 150,
      (byte) 82,
      (byte) 117
    };
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray15, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index] ^= numArray17[index];
    byte[] numArray18 = new byte[55]
    {
      (byte) 137,
      (byte) 138,
      (byte) 217,
      (byte) 115,
      (byte) 135,
      (byte) 245,
      (byte) 106,
      (byte) 124,
      (byte) 197,
      (byte) 252,
      (byte) 109,
      (byte) 69,
      (byte) 74,
      (byte) 198,
      (byte) 20,
      (byte) 192 /*0xC0*/,
      (byte) 108,
      (byte) 143,
      (byte) 32 /*0x20*/,
      (byte) 0,
      (byte) 15,
      (byte) 127 /*0x7F*/,
      (byte) 123,
      (byte) 69,
      (byte) 246,
      (byte) 248,
      (byte) 251,
      (byte) 124,
      (byte) 20,
      (byte) 56,
      (byte) 163,
      (byte) 241,
      (byte) 137,
      (byte) 10,
      (byte) 160 /*0xA0*/,
      (byte) 202,
      (byte) 155,
      (byte) 243,
      (byte) 71,
      (byte) 14,
      (byte) 30,
      (byte) 185,
      (byte) 54,
      (byte) 133,
      (byte) 209,
      (byte) 43,
      (byte) 240 /*0xF0*/,
      (byte) 210,
      (byte) 174,
      (byte) 247,
      (byte) 35,
      (byte) 232,
      (byte) 151,
      (byte) 214,
      (byte) 110
    };
    byte[] numArray19 = new byte[55];
    numArray19[24] = (byte) 210;
    numArray19[1] = (byte) 50;
    numArray19[5] = (byte) 5;
    numArray19[44] = byte.MaxValue;
    numArray19[4] = (byte) 217;
    numArray19[31 /*0x1F*/] = (byte) 97;
    numArray19[6] = (byte) 109;
    numArray19[36] = (byte) 99;
    numArray19[8] = (byte) 59;
    numArray19[42] = (byte) 127 /*0x7F*/;
    numArray19[39] = (byte) 189;
    numArray19[14] = (byte) 108;
    numArray19[12] = (byte) 77;
    numArray19[13] = (byte) 91;
    numArray19[17] = (byte) 17;
    numArray19[15] = (byte) 253;
    numArray19[16 /*0x10*/] = (byte) 108;
    numArray19[40] = byte.MaxValue;
    numArray19[28] = (byte) 82;
    numArray19[25] = (byte) 133;
    numArray19[10] = (byte) 246;
    numArray19[20] = (byte) 70;
    numArray19[22] = (byte) 171;
    numArray19[19] = (byte) 163;
    numArray19[33] = (byte) 50;
    numArray19[32 /*0x20*/] = (byte) 172;
    numArray19[50] = (byte) 38;
    numArray19[49] = (byte) 228;
    numArray19[9] = (byte) 153;
    numArray19[3] = (byte) 182;
    numArray19[18] = (byte) 228;
    numArray19[47] = (byte) 19;
    numArray19[34] = (byte) 218;
    numArray19[2] = (byte) 234;
    numArray19[29] = (byte) 0;
    numArray19[43] = (byte) 232;
    numArray19[11] = (byte) 170;
    numArray19[37] = (byte) 134;
    numArray19[38] = (byte) 92;
    numArray19[21] = (byte) 88;
    numArray19[0] = (byte) 162;
    numArray19[41] = (byte) 175;
    numArray19[23] = (byte) 243;
    numArray19[26] = byte.MaxValue;
    numArray19[53] = (byte) 187;
    numArray19[45] = (byte) 36;
    numArray19[46] = (byte) 240 /*0xF0*/;
    numArray19[7] = (byte) 210;
    numArray19[48 /*0x30*/] = (byte) 141;
    numArray19[30] = (byte) 195;
    numArray19[27] = (byte) 56;
    numArray19[51] = (byte) 231;
    numArray19[52] = (byte) 83;
    numArray19[35] = (byte) 192 /*0xC0*/;
    numArray19[54] = (byte) 88;
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray15, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 55] ^= numArray19[index];
    byte[] numArray20 = new byte[55]
    {
      (byte) 31 /*0x1F*/,
      (byte) 177,
      (byte) 135,
      (byte) 210,
      (byte) 31 /*0x1F*/,
      (byte) 74,
      (byte) 120,
      (byte) 157,
      (byte) 32 /*0x20*/,
      (byte) 8,
      (byte) 164,
      (byte) 143,
      (byte) 143,
      (byte) 29,
      (byte) 3,
      (byte) 144 /*0x90*/,
      (byte) 252,
      (byte) 178,
      (byte) 4,
      (byte) 158,
      (byte) 199,
      (byte) 30,
      (byte) 91,
      (byte) 87,
      (byte) 123,
      (byte) 106,
      (byte) 74,
      (byte) 106,
      (byte) 27,
      (byte) 212,
      (byte) 191,
      (byte) 131,
      (byte) 214,
      (byte) 160 /*0xA0*/,
      (byte) 231,
      (byte) 196,
      (byte) 48 /*0x30*/,
      (byte) 66,
      (byte) 16 /*0x10*/,
      (byte) 6,
      (byte) 49,
      (byte) 144 /*0x90*/,
      (byte) 52,
      (byte) 104,
      (byte) 12,
      (byte) 28,
      (byte) 213,
      (byte) 181,
      (byte) 166,
      (byte) 15,
      (byte) 136,
      (byte) 64 /*0x40*/,
      (byte) 19,
      (byte) 33,
      (byte) 82
    };
    byte[] numArray21 = new byte[55]
    {
      (byte) 77,
      (byte) 148,
      (byte) 176 /*0xB0*/,
      (byte) 113,
      (byte) 242,
      (byte) 194,
      (byte) 126,
      (byte) 98,
      (byte) 227,
      (byte) 208 /*0xD0*/,
      (byte) 132,
      (byte) 159,
      (byte) 239,
      (byte) 48 /*0x30*/,
      (byte) 96 /*0x60*/,
      (byte) 2,
      (byte) 154,
      (byte) 215,
      (byte) 51,
      (byte) 251,
      (byte) 112 /*0x70*/,
      (byte) 168,
      (byte) 155,
      (byte) 244,
      (byte) 24,
      (byte) 202,
      (byte) 221,
      (byte) 18,
      (byte) 145,
      (byte) 0,
      (byte) 64 /*0x40*/,
      (byte) 36,
      (byte) 104,
      (byte) 221,
      (byte) 118,
      (byte) 145,
      (byte) 43,
      (byte) 46,
      (byte) 33,
      (byte) 107,
      (byte) 110,
      (byte) 215,
      (byte) 199,
      (byte) 194,
      (byte) 164,
      (byte) 177,
      (byte) 11,
      (byte) 123,
      (byte) 108,
      (byte) 169,
      (byte) 179,
      (byte) 39,
      (byte) 77,
      (byte) 109,
      (byte) 190
    };
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray15, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 110] ^= numArray21[index];
    byte[] numArray22 = new byte[55]
    {
      (byte) 47,
      (byte) 86,
      (byte) 181,
      byte.MaxValue,
      (byte) 13,
      (byte) 130,
      (byte) 76,
      (byte) 72,
      (byte) 33,
      (byte) 185,
      (byte) 238,
      (byte) 53,
      (byte) 62,
      (byte) 95,
      (byte) 186,
      (byte) 162,
      (byte) 253,
      (byte) 109,
      (byte) 55,
      (byte) 233,
      (byte) 243,
      (byte) 75,
      (byte) 94,
      (byte) 149,
      (byte) 149,
      (byte) 196,
      (byte) 11,
      (byte) 76,
      (byte) 127 /*0x7F*/,
      (byte) 111,
      (byte) 180,
      (byte) 77,
      (byte) 230,
      (byte) 110,
      (byte) 1,
      (byte) 236,
      (byte) 91,
      (byte) 7,
      (byte) 35,
      (byte) 13,
      (byte) 232,
      (byte) 31 /*0x1F*/,
      (byte) 235,
      (byte) 99,
      (byte) 230,
      (byte) 127 /*0x7F*/,
      (byte) 103,
      (byte) 176 /*0xB0*/,
      (byte) 206,
      (byte) 158,
      (byte) 21,
      (byte) 180,
      (byte) 26,
      (byte) 23,
      (byte) 99
    };
    byte[] numArray23 = new byte[55]
    {
      (byte) 227,
      (byte) 149,
      (byte) 249,
      (byte) 79,
      (byte) 194,
      (byte) 176 /*0xB0*/,
      (byte) 185,
      (byte) 35,
      (byte) 162,
      (byte) 31 /*0x1F*/,
      (byte) 129,
      (byte) 173,
      (byte) 220,
      (byte) 191,
      (byte) 151,
      (byte) 183,
      (byte) 130,
      (byte) 56,
      (byte) 176 /*0xB0*/,
      (byte) 253,
      (byte) 188,
      (byte) 95,
      (byte) 139,
      (byte) 239,
      (byte) 170,
      (byte) 134,
      (byte) 122,
      (byte) 99,
      (byte) 62,
      (byte) 235,
      (byte) 57,
      (byte) 74,
      (byte) 238,
      (byte) 159,
      (byte) 111,
      (byte) 126,
      (byte) 104,
      (byte) 71,
      (byte) 11,
      (byte) 207,
      (byte) 15,
      (byte) 35,
      (byte) 190,
      (byte) 37,
      (byte) 254,
      (byte) 200,
      (byte) 45,
      (byte) 6,
      (byte) 188,
      (byte) 75,
      (byte) 148,
      (byte) 21,
      (byte) 141,
      (byte) 243,
      (byte) 242
    };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray15, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 165] ^= numArray23[index];
    byte[] numArray24 = new byte[55]
    {
      (byte) 31 /*0x1F*/,
      (byte) 106,
      (byte) 73,
      (byte) 118,
      (byte) 0,
      (byte) 201,
      (byte) 177,
      (byte) 5,
      (byte) 161,
      (byte) 78,
      (byte) 208 /*0xD0*/,
      (byte) 93,
      (byte) 205,
      (byte) 70,
      (byte) 246,
      (byte) 73,
      (byte) 42,
      (byte) 1,
      (byte) 211,
      (byte) 95,
      (byte) 251,
      (byte) 174,
      (byte) 22,
      (byte) 92,
      (byte) 91,
      (byte) 125,
      (byte) 224 /*0xE0*/,
      (byte) 89,
      (byte) 0,
      (byte) 51,
      (byte) 207,
      (byte) 10,
      (byte) 29,
      (byte) 244,
      (byte) 59,
      (byte) 156,
      (byte) 218,
      (byte) 151,
      (byte) 232,
      (byte) 86,
      (byte) 57,
      (byte) 94,
      (byte) 69,
      (byte) 48 /*0x30*/,
      (byte) 151,
      (byte) 82,
      (byte) 0,
      (byte) 87,
      (byte) 31 /*0x1F*/,
      (byte) 106,
      (byte) 240 /*0xF0*/,
      (byte) 22,
      (byte) 144 /*0x90*/,
      (byte) 132,
      (byte) 251
    };
    byte[] numArray25 = new byte[55]
    {
      (byte) 135,
      (byte) 85,
      (byte) 80 /*0x50*/,
      (byte) 19,
      (byte) 100,
      (byte) 82,
      (byte) 176 /*0xB0*/,
      (byte) 15,
      (byte) 174,
      (byte) 211,
      (byte) 178,
      (byte) 220,
      (byte) 121,
      (byte) 251,
      (byte) 236,
      (byte) 88,
      (byte) 11,
      (byte) 40,
      (byte) 3,
      (byte) 35,
      (byte) 201,
      (byte) 49,
      (byte) 63 /*0x3F*/,
      (byte) 193,
      (byte) 70,
      (byte) 45,
      (byte) 176 /*0xB0*/,
      (byte) 254,
      (byte) 181,
      (byte) 40,
      (byte) 51,
      (byte) 31 /*0x1F*/,
      (byte) 70,
      (byte) 3,
      (byte) 80 /*0x50*/,
      (byte) 42,
      (byte) 159,
      (byte) 93,
      (byte) 114,
      (byte) 39,
      (byte) 134,
      (byte) 237,
      (byte) 225,
      (byte) 127 /*0x7F*/,
      (byte) 17,
      (byte) 34,
      (byte) 17,
      (byte) 224 /*0xE0*/,
      (byte) 48 /*0x30*/,
      (byte) 126,
      (byte) 156,
      (byte) 135,
      (byte) 95,
      (byte) 154,
      (byte) 12
    };
    key.Query(true, 335, numArray24, numArray24);
    Array.Copy((Array) numArray24, 0, (Array) numArray15, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 220] ^= numArray25[index];
    byte[] numArray26 = new byte[13]
    {
      (byte) 237,
      (byte) 27,
      (byte) 130,
      (byte) 232,
      (byte) 88,
      (byte) 4,
      (byte) 109,
      (byte) 127 /*0x7F*/,
      (byte) 197,
      (byte) 64 /*0x40*/,
      (byte) 216,
      (byte) 11,
      (byte) 90
    };
    byte[] numArray27 = new byte[13];
    numArray27[6] = (byte) 145;
    numArray27[0] = (byte) 40;
    numArray27[2] = (byte) 184;
    numArray27[3] = (byte) 30;
    numArray27[4] = (byte) 135;
    numArray27[1] = (byte) 54;
    numArray27[8] = (byte) 165;
    numArray27[7] = (byte) 109;
    numArray27[9] = (byte) 254;
    numArray27[10] = (byte) 64 /*0x40*/;
    numArray27[11] = (byte) 13;
    numArray27[5] = (byte) 112 /*0x70*/;
    numArray27[12] = (byte) 0;
    key.Query(true, 335, numArray26, numArray26);
    Array.Copy((Array) numArray26, 0, (Array) numArray15, 275, 13);
    for (int index = 0; index < 13; ++index)
      numArray15[index + 275] ^= numArray27[index];
    return Encoding.UTF8.GetString(numArray15);
  }

  internal static string ssp_appserver_12346()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[156];
      byte[] numArray2 = new byte[55];
      numArray2[3] = (byte) 72;
      numArray2[22] = (byte) 94;
      numArray2[37] = (byte) 17;
      numArray2[50] = (byte) 117;
      numArray2[4] = (byte) 237;
      numArray2[11] = (byte) 177;
      numArray2[8] = (byte) 225;
      numArray2[24] = (byte) 110;
      numArray2[1] = (byte) 249;
      numArray2[9] = (byte) 89;
      numArray2[10] = (byte) 215;
      numArray2[19] = (byte) 253;
      numArray2[12] = (byte) 209;
      numArray2[0] = (byte) 152;
      numArray2[14] = (byte) 39;
      numArray2[15] = (byte) 76;
      numArray2[16 /*0x10*/] = (byte) 196;
      numArray2[17] = (byte) 251;
      numArray2[18] = (byte) 93;
      numArray2[38] = (byte) 129;
      numArray2[47] = (byte) 247;
      numArray2[33] = (byte) 75;
      numArray2[34] = (byte) 167;
      numArray2[27] = (byte) 5;
      numArray2[53] = (byte) 187;
      numArray2[25] = (byte) 40;
      numArray2[23] = (byte) 7;
      numArray2[35] = (byte) 223;
      numArray2[28] = (byte) 95;
      numArray2[21] = (byte) 93;
      numArray2[26] = (byte) 18;
      numArray2[31 /*0x1F*/] = (byte) 42;
      numArray2[32 /*0x20*/] = (byte) 197;
      numArray2[20] = (byte) 18;
      numArray2[29] = (byte) 29;
      numArray2[49] = (byte) 60;
      numArray2[36] = (byte) 64 /*0x40*/;
      numArray2[51] = (byte) 153;
      numArray2[7] = (byte) 109;
      numArray2[39] = (byte) 23;
      numArray2[40] = (byte) 178;
      numArray2[52] = (byte) 229;
      numArray2[42] = (byte) 69;
      numArray2[43] = (byte) 68;
      numArray2[44] = (byte) 146;
      numArray2[45] = (byte) 52;
      numArray2[46] = (byte) 243;
      numArray2[54] = (byte) 103;
      numArray2[48 /*0x30*/] = (byte) 84;
      numArray2[13] = (byte) 17;
      numArray2[5] = (byte) 123;
      numArray2[2] = (byte) 202;
      numArray2[30] = (byte) 111;
      numArray2[6] = (byte) 101;
      numArray2[41] = (byte) 15;
      byte[] numArray3 = new byte[55]
      {
        (byte) 241,
        (byte) 92,
        (byte) 243,
        (byte) 65,
        (byte) 217,
        (byte) 144 /*0x90*/,
        (byte) 108,
        (byte) 25,
        (byte) 183,
        (byte) 170,
        (byte) 173,
        (byte) 209,
        (byte) 177,
        (byte) 144 /*0x90*/,
        (byte) 163,
        (byte) 149,
        (byte) 121,
        (byte) 245,
        (byte) 197,
        (byte) 147,
        (byte) 21,
        (byte) 26,
        (byte) 251,
        (byte) 71,
        (byte) 254,
        (byte) 0,
        (byte) 144 /*0x90*/,
        (byte) 151,
        (byte) 94,
        (byte) 206,
        (byte) 191,
        (byte) 127 /*0x7F*/,
        (byte) 230,
        (byte) 62,
        (byte) 181,
        (byte) 180,
        (byte) 189,
        (byte) 215,
        (byte) 126,
        (byte) 102,
        (byte) 65,
        (byte) 200,
        (byte) 40,
        (byte) 147,
        (byte) 220,
        (byte) 102,
        (byte) 12,
        (byte) 170,
        (byte) 112 /*0x70*/,
        (byte) 85,
        (byte) 191,
        (byte) 223,
        (byte) 23,
        (byte) 253,
        (byte) 87
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[19] = (byte) 151;
      numArray4[1] = (byte) 159;
      numArray4[7] = (byte) 20;
      numArray4[3] = (byte) 77;
      numArray4[39] = (byte) 22;
      numArray4[50] = (byte) 7;
      numArray4[44] = (byte) 203;
      numArray4[23] = (byte) 199;
      numArray4[27] = (byte) 229;
      numArray4[47] = (byte) 164;
      numArray4[14] = (byte) 123;
      numArray4[11] = (byte) 252;
      numArray4[12] = (byte) 3;
      numArray4[20] = (byte) 191;
      numArray4[6] = (byte) 28;
      numArray4[13] = (byte) 141;
      numArray4[22] = (byte) 242;
      numArray4[4] = (byte) 174;
      numArray4[18] = (byte) 1;
      numArray4[16 /*0x10*/] = (byte) 152;
      numArray4[2] = (byte) 143;
      numArray4[21] = (byte) 143;
      numArray4[10] = (byte) 162;
      numArray4[8] = (byte) 103;
      numArray4[37] = (byte) 96 /*0x60*/;
      numArray4[25] = (byte) 164;
      numArray4[26] = (byte) 241;
      numArray4[15] = (byte) 188;
      numArray4[28] = (byte) 181;
      numArray4[29] = (byte) 166;
      numArray4[30] = (byte) 152;
      numArray4[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
      numArray4[32 /*0x20*/] = (byte) 75;
      numArray4[33] = (byte) 195;
      numArray4[52] = (byte) 98;
      numArray4[35] = (byte) 152;
      numArray4[36] = (byte) 45;
      numArray4[54] = (byte) 193;
      numArray4[38] = (byte) 164;
      numArray4[9] = (byte) 225;
      numArray4[40] = (byte) 100;
      numArray4[41] = (byte) 69;
      numArray4[42] = (byte) 126;
      numArray4[43] = (byte) 103;
      numArray4[5] = (byte) 235;
      numArray4[45] = (byte) 144 /*0x90*/;
      numArray4[46] = (byte) 8;
      numArray4[24] = (byte) 142;
      numArray4[48 /*0x30*/] = (byte) 71;
      numArray4[49] = (byte) 99;
      numArray4[17] = (byte) 247;
      numArray4[34] = (byte) 48 /*0x30*/;
      numArray4[51] = (byte) 121;
      numArray4[53] = (byte) 138;
      numArray4[0] = (byte) 77;
      byte[] numArray5 = new byte[55]
      {
        (byte) 150,
        (byte) 142,
        (byte) 135,
        (byte) 91,
        (byte) 24,
        (byte) 163,
        (byte) 23,
        (byte) 138,
        (byte) 71,
        (byte) 28,
        (byte) 90,
        (byte) 151,
        (byte) 76,
        (byte) 204,
        (byte) 122,
        (byte) 213,
        (byte) 125,
        (byte) 224 /*0xE0*/,
        (byte) 177,
        (byte) 94,
        (byte) 233,
        (byte) 176 /*0xB0*/,
        (byte) 1,
        (byte) 233,
        (byte) 31 /*0x1F*/,
        (byte) 67,
        (byte) 177,
        (byte) 208 /*0xD0*/,
        (byte) 212,
        (byte) 101,
        (byte) 184,
        (byte) 0,
        (byte) 180,
        (byte) 157,
        (byte) 69,
        (byte) 27,
        (byte) 63 /*0x3F*/,
        (byte) 224 /*0xE0*/,
        (byte) 133,
        (byte) 58,
        (byte) 194,
        (byte) 203,
        (byte) 181,
        (byte) 1,
        (byte) 211,
        (byte) 221,
        (byte) 55,
        (byte) 86,
        (byte) 15,
        (byte) 198,
        (byte) 166,
        (byte) 154,
        (byte) 205,
        (byte) 126,
        (byte) 221
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[46]
      {
        (byte) 204,
        (byte) 194,
        (byte) 110,
        (byte) 173,
        (byte) 153,
        (byte) 13,
        (byte) 33,
        (byte) 41,
        (byte) 4,
        (byte) 240 /*0xF0*/,
        (byte) 62,
        (byte) 234,
        (byte) 246,
        (byte) 42,
        (byte) 150,
        (byte) 185,
        (byte) 208 /*0xD0*/,
        (byte) 5,
        (byte) 146,
        (byte) 84,
        (byte) 108,
        (byte) 82,
        (byte) 30,
        (byte) 211,
        (byte) 6,
        (byte) 56,
        (byte) 125,
        (byte) 207,
        (byte) 249,
        (byte) 167,
        (byte) 105,
        (byte) 185,
        (byte) 198,
        (byte) 201,
        (byte) 152,
        (byte) 222,
        (byte) 44,
        (byte) 110,
        (byte) 147,
        (byte) 216,
        (byte) 155,
        (byte) 244,
        (byte) 164,
        (byte) 238,
        (byte) 138,
        (byte) 92
      };
      byte[] numArray7 = new byte[46]
      {
        (byte) 121,
        (byte) 154,
        (byte) 189,
        (byte) 34,
        (byte) 220,
        (byte) 125,
        (byte) 76,
        (byte) 161,
        (byte) 32 /*0x20*/,
        (byte) 199,
        (byte) 31 /*0x1F*/,
        (byte) 252,
        (byte) 213,
        (byte) 177,
        (byte) 249,
        (byte) 193,
        (byte) 115,
        (byte) 220,
        (byte) 35,
        (byte) 52,
        byte.MaxValue,
        (byte) 201,
        (byte) 35,
        (byte) 18,
        (byte) 133,
        (byte) 220,
        (byte) 151,
        (byte) 116,
        (byte) 0,
        (byte) 138,
        (byte) 102,
        (byte) 224 /*0xE0*/,
        (byte) 206,
        (byte) 8,
        (byte) 230,
        (byte) 174,
        (byte) 124,
        (byte) 240 /*0xF0*/,
        (byte) 244,
        (byte) 210,
        (byte) 176 /*0xB0*/,
        (byte) 32 /*0x20*/,
        (byte) 111,
        (byte) 215,
        (byte) 40,
        (byte) 2
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 46);
      for (int index = 0; index < 46; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[156];
    byte[] numArray9 = new byte[55]
    {
      (byte) 108,
      (byte) 166,
      (byte) 81,
      (byte) 181,
      (byte) 87,
      (byte) 181,
      (byte) 121,
      (byte) 163,
      (byte) 154,
      (byte) 167,
      (byte) 97,
      (byte) 144 /*0x90*/,
      (byte) 82,
      (byte) 48 /*0x30*/,
      (byte) 93,
      (byte) 134,
      (byte) 132,
      (byte) 35,
      (byte) 44,
      (byte) 145,
      (byte) 184,
      (byte) 141,
      (byte) 97,
      (byte) 113,
      (byte) 185,
      (byte) 185,
      (byte) 119,
      (byte) 173,
      (byte) 48 /*0x30*/,
      (byte) 72,
      (byte) 169,
      (byte) 201,
      (byte) 210,
      (byte) 151,
      (byte) 140,
      (byte) 1,
      (byte) 24,
      (byte) 4,
      (byte) 110,
      (byte) 20,
      (byte) 31 /*0x1F*/,
      (byte) 58,
      (byte) 145,
      (byte) 198,
      (byte) 50,
      (byte) 72,
      (byte) 73,
      (byte) 196,
      (byte) 71,
      (byte) 103,
      (byte) 6,
      (byte) 56,
      (byte) 66,
      (byte) 25,
      (byte) 128 /*0x80*/
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 241,
      (byte) 234,
      (byte) 48 /*0x30*/,
      (byte) 126,
      (byte) 104,
      (byte) 130,
      (byte) 70,
      (byte) 230,
      (byte) 156,
      (byte) 128 /*0x80*/,
      (byte) 35,
      (byte) 11,
      (byte) 4,
      (byte) 209,
      (byte) 48 /*0x30*/,
      (byte) 191,
      (byte) 33,
      (byte) 2,
      (byte) 163,
      (byte) 198,
      (byte) 199,
      (byte) 177,
      (byte) 149,
      (byte) 167,
      (byte) 205,
      (byte) 25,
      (byte) 90,
      (byte) 109,
      (byte) 170,
      (byte) 191,
      (byte) 125,
      (byte) 9,
      (byte) 178,
      (byte) 241,
      (byte) 112 /*0x70*/,
      (byte) 240 /*0xF0*/,
      (byte) 197,
      (byte) 138,
      (byte) 62,
      (byte) 121,
      (byte) 151,
      (byte) 189,
      (byte) 151,
      (byte) 226,
      (byte) 193,
      (byte) 231,
      (byte) 137,
      byte.MaxValue,
      (byte) 9,
      (byte) 160 /*0xA0*/,
      (byte) 181,
      (byte) 212,
      (byte) 127 /*0x7F*/,
      (byte) 108,
      (byte) 236
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 223,
      (byte) 251,
      (byte) 158,
      (byte) 61,
      (byte) 203,
      (byte) 115,
      (byte) 81,
      (byte) 253,
      (byte) 152,
      (byte) 190,
      (byte) 187,
      (byte) 163,
      (byte) 92,
      (byte) 110,
      (byte) 127 /*0x7F*/,
      (byte) 180,
      (byte) 23,
      (byte) 113,
      (byte) 125,
      (byte) 251,
      (byte) 154,
      (byte) 63 /*0x3F*/,
      (byte) 113,
      (byte) 112 /*0x70*/,
      (byte) 178,
      (byte) 102,
      (byte) 200,
      (byte) 66,
      (byte) 113,
      (byte) 134,
      (byte) 185,
      (byte) 22,
      (byte) 172,
      (byte) 188,
      (byte) 57,
      (byte) 9,
      (byte) 208 /*0xD0*/,
      (byte) 214,
      (byte) 83,
      (byte) 198,
      (byte) 63 /*0x3F*/,
      (byte) 201,
      (byte) 153,
      (byte) 138,
      (byte) 141,
      (byte) 32 /*0x20*/,
      (byte) 235,
      (byte) 249,
      (byte) 134,
      (byte) 131,
      (byte) 43,
      (byte) 167,
      (byte) 2,
      (byte) 227,
      (byte) 169
    };
    byte[] numArray12 = new byte[55];
    numArray12[53] = (byte) 61;
    numArray12[44] = (byte) 27;
    numArray12[15] = (byte) 181;
    numArray12[3] = (byte) 98;
    numArray12[4] = (byte) 53;
    numArray12[25] = (byte) 232;
    numArray12[32 /*0x20*/] = (byte) 73;
    numArray12[7] = (byte) 67;
    numArray12[31 /*0x1F*/] = (byte) 54;
    numArray12[24] = (byte) 170;
    numArray12[10] = (byte) 143;
    numArray12[11] = (byte) 9;
    numArray12[14] = (byte) 80 /*0x50*/;
    numArray12[13] = (byte) 12;
    numArray12[34] = (byte) 143;
    numArray12[30] = (byte) 6;
    numArray12[36] = (byte) 139;
    numArray12[2] = (byte) 41;
    numArray12[18] = (byte) 173;
    numArray12[19] = (byte) 179;
    numArray12[43] = (byte) 155;
    numArray12[21] = (byte) 191;
    numArray12[6] = (byte) 106;
    numArray12[12] = (byte) 247;
    numArray12[23] = (byte) 62;
    numArray12[33] = (byte) 89;
    numArray12[26] = (byte) 135;
    numArray12[8] = (byte) 251;
    numArray12[28] = (byte) 133;
    numArray12[17] = (byte) 214;
    numArray12[9] = (byte) 141;
    numArray12[50] = (byte) 130;
    numArray12[16 /*0x10*/] = (byte) 213;
    numArray12[29] = (byte) 86;
    numArray12[1] = (byte) 118;
    numArray12[35] = (byte) 128 /*0x80*/;
    numArray12[46] = (byte) 252;
    numArray12[37] = (byte) 49;
    numArray12[47] = (byte) 159;
    numArray12[39] = (byte) 174;
    numArray12[0] = (byte) 62;
    numArray12[41] = (byte) 185;
    numArray12[42] = (byte) 26;
    numArray12[20] = (byte) 64 /*0x40*/;
    numArray12[5] = (byte) 111;
    numArray12[45] = (byte) 253;
    numArray12[38] = (byte) 253;
    numArray12[40] = (byte) 51;
    numArray12[48 /*0x30*/] = (byte) 69;
    numArray12[49] = (byte) 130;
    numArray12[22] = (byte) 108;
    numArray12[51] = (byte) 204;
    numArray12[52] = (byte) 104;
    numArray12[27] = (byte) 66;
    numArray12[54] = (byte) 250;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[46]
    {
      (byte) 75,
      (byte) 248,
      (byte) 234,
      (byte) 219,
      (byte) 82,
      (byte) 159,
      (byte) 183,
      (byte) 242,
      (byte) 143,
      (byte) 46,
      (byte) 139,
      (byte) 154,
      (byte) 247,
      (byte) 65,
      (byte) 245,
      (byte) 47,
      (byte) 154,
      (byte) 138,
      (byte) 149,
      (byte) 30,
      (byte) 98,
      (byte) 248,
      (byte) 104,
      (byte) 46,
      (byte) 10,
      (byte) 173,
      (byte) 78,
      (byte) 113,
      (byte) 137,
      (byte) 200,
      (byte) 160 /*0xA0*/,
      (byte) 59,
      (byte) 160 /*0xA0*/,
      (byte) 221,
      (byte) 86,
      (byte) 82,
      (byte) 4,
      (byte) 64 /*0x40*/,
      (byte) 222,
      (byte) 157,
      (byte) 208 /*0xD0*/,
      (byte) 147,
      (byte) 89,
      (byte) 251,
      (byte) 192 /*0xC0*/,
      (byte) 42
    };
    byte[] numArray14 = new byte[46]
    {
      (byte) 236,
      (byte) 245,
      (byte) 187,
      (byte) 208 /*0xD0*/,
      (byte) 80 /*0x50*/,
      (byte) 162,
      (byte) 64 /*0x40*/,
      (byte) 15,
      (byte) 230,
      (byte) 178,
      (byte) 134,
      (byte) 218,
      (byte) 4,
      (byte) 177,
      (byte) 130,
      (byte) 239,
      (byte) 94,
      (byte) 109,
      (byte) 230,
      (byte) 196,
      (byte) 92,
      (byte) 185,
      (byte) 21,
      (byte) 214,
      (byte) 102,
      (byte) 46,
      byte.MaxValue,
      (byte) 34,
      (byte) 113,
      (byte) 170,
      (byte) 65,
      (byte) 44,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 20,
      (byte) 121,
      (byte) 83,
      (byte) 93,
      (byte) 180,
      (byte) 94,
      (byte) 253,
      (byte) 0,
      (byte) 60,
      (byte) 160 /*0xA0*/,
      (byte) 21,
      (byte) 44
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 46);
    for (int index = 0; index < 46; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12347()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[148];
      byte[] numArray2 = new byte[55]
      {
        (byte) 177,
        (byte) 157,
        (byte) 66,
        (byte) 218,
        (byte) 63 /*0x3F*/,
        (byte) 83,
        (byte) 144 /*0x90*/,
        (byte) 187,
        (byte) 208 /*0xD0*/,
        (byte) 165,
        (byte) 81,
        (byte) 70,
        (byte) 104,
        (byte) 199,
        (byte) 141,
        (byte) 79,
        (byte) 249,
        (byte) 25,
        (byte) 235,
        (byte) 162,
        (byte) 43,
        (byte) 89,
        (byte) 172,
        (byte) 247,
        (byte) 78,
        (byte) 206,
        (byte) 158,
        (byte) 109,
        (byte) 109,
        (byte) 224 /*0xE0*/,
        (byte) 149,
        (byte) 240 /*0xF0*/,
        (byte) 124,
        (byte) 48 /*0x30*/,
        (byte) 23,
        (byte) 3,
        (byte) 249,
        (byte) 198,
        (byte) 157,
        (byte) 3,
        (byte) 194,
        (byte) 72,
        (byte) 112 /*0x70*/,
        (byte) 57,
        (byte) 76,
        (byte) 138,
        (byte) 42,
        (byte) 146,
        (byte) 109,
        (byte) 113,
        (byte) 49,
        (byte) 254,
        (byte) 113,
        (byte) 82,
        (byte) 45
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 71,
        (byte) 42,
        (byte) 33,
        (byte) 121,
        (byte) 161,
        (byte) 196,
        (byte) 248,
        (byte) 241,
        (byte) 138,
        (byte) 82,
        (byte) 200,
        (byte) 7,
        (byte) 238,
        (byte) 28,
        (byte) 9,
        (byte) 147,
        (byte) 76,
        (byte) 90,
        (byte) 241,
        (byte) 172,
        (byte) 177,
        (byte) 252,
        (byte) 75,
        (byte) 49,
        (byte) 76,
        (byte) 7,
        (byte) 148,
        (byte) 14,
        (byte) 95,
        (byte) 97,
        (byte) 0,
        (byte) 139,
        (byte) 31 /*0x1F*/,
        (byte) 95,
        (byte) 227,
        (byte) 241,
        (byte) 153,
        (byte) 90,
        (byte) 55,
        (byte) 41,
        (byte) 42,
        (byte) 159,
        (byte) 51,
        (byte) 135,
        (byte) 171,
        (byte) 189,
        (byte) 76,
        (byte) 66,
        (byte) 253,
        (byte) 96 /*0x60*/,
        (byte) 175,
        (byte) 198,
        (byte) 64 /*0x40*/,
        (byte) 171,
        (byte) 18
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 74,
        (byte) 140,
        (byte) 113,
        (byte) 180,
        (byte) 34,
        (byte) 78,
        (byte) 240 /*0xF0*/,
        (byte) 15,
        (byte) 96 /*0x60*/,
        (byte) 201,
        (byte) 56,
        (byte) 176 /*0xB0*/,
        (byte) 137,
        (byte) 86,
        (byte) 51,
        (byte) 124,
        (byte) 232,
        (byte) 223,
        (byte) 227,
        (byte) 233,
        (byte) 114,
        (byte) 171,
        (byte) 196,
        (byte) 164,
        (byte) 12,
        byte.MaxValue,
        (byte) 252,
        (byte) 174,
        (byte) 150,
        (byte) 127 /*0x7F*/,
        (byte) 41,
        (byte) 3,
        (byte) 191,
        (byte) 18,
        (byte) 174,
        (byte) 57,
        (byte) 44,
        (byte) 78,
        (byte) 159,
        (byte) 249,
        (byte) 127 /*0x7F*/,
        (byte) 41,
        (byte) 236,
        (byte) 192 /*0xC0*/,
        (byte) 242,
        (byte) 226,
        (byte) 227,
        (byte) 27,
        (byte) 253,
        (byte) 130,
        (byte) 137,
        (byte) 115,
        (byte) 73,
        (byte) 110,
        (byte) 126
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 241,
        (byte) 228,
        (byte) 248,
        (byte) 162,
        (byte) 58,
        (byte) 208 /*0xD0*/,
        (byte) 135,
        (byte) 75,
        (byte) 69,
        (byte) 156,
        (byte) 36,
        (byte) 178,
        (byte) 44,
        (byte) 27,
        (byte) 171,
        (byte) 168,
        (byte) 37,
        (byte) 114,
        (byte) 36,
        (byte) 102,
        (byte) 141,
        (byte) 110,
        (byte) 167,
        (byte) 169,
        (byte) 50,
        (byte) 195,
        (byte) 164,
        (byte) 28,
        (byte) 88,
        (byte) 200,
        (byte) 82,
        (byte) 148,
        (byte) 205,
        (byte) 200,
        (byte) 80 /*0x50*/,
        (byte) 70,
        (byte) 151,
        (byte) 123,
        (byte) 34,
        (byte) 149,
        (byte) 246,
        (byte) 5,
        (byte) 204,
        (byte) 90,
        (byte) 81,
        (byte) 134,
        (byte) 87,
        (byte) 254,
        (byte) 244,
        (byte) 182,
        (byte) 142,
        (byte) 225,
        (byte) 150,
        (byte) 119,
        (byte) 97
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[38];
      numArray6[0] = (byte) 204;
      numArray6[18] = (byte) 72;
      numArray6[34] = (byte) 95;
      numArray6[25] = (byte) 177;
      numArray6[29] = (byte) 185;
      numArray6[5] = (byte) 121;
      numArray6[32 /*0x20*/] = (byte) 15;
      numArray6[7] = (byte) 160 /*0xA0*/;
      numArray6[20] = (byte) 4;
      numArray6[15] = (byte) 63 /*0x3F*/;
      numArray6[10] = (byte) 119;
      numArray6[11] = (byte) 167;
      numArray6[12] = (byte) 249;
      numArray6[26] = (byte) 20;
      numArray6[14] = (byte) 153;
      numArray6[13] = (byte) 79;
      numArray6[36] = (byte) 125;
      numArray6[17] = (byte) 105;
      numArray6[27] = (byte) 245;
      numArray6[19] = (byte) 191;
      numArray6[8] = (byte) 52;
      numArray6[21] = (byte) 178;
      numArray6[1] = (byte) 167;
      numArray6[2] = (byte) 34;
      numArray6[6] = (byte) 225;
      numArray6[3] = (byte) 81;
      numArray6[16 /*0x10*/] = (byte) 233;
      numArray6[23] = (byte) 28;
      numArray6[28] = (byte) 182;
      numArray6[24] = (byte) 225;
      numArray6[30] = (byte) 81;
      numArray6[31 /*0x1F*/] = (byte) 147;
      numArray6[22] = (byte) 42;
      numArray6[33] = (byte) 113;
      numArray6[35] = (byte) 167;
      numArray6[4] = (byte) 65;
      numArray6[9] = (byte) 240 /*0xF0*/;
      numArray6[37] = (byte) 167;
      byte[] numArray7 = new byte[38]
      {
        (byte) 133,
        (byte) 245,
        (byte) 238,
        (byte) 159,
        (byte) 204,
        (byte) 163,
        (byte) 59,
        (byte) 147,
        (byte) 249,
        (byte) 2,
        (byte) 225,
        (byte) 225,
        (byte) 50,
        (byte) 175,
        (byte) 124,
        (byte) 113,
        (byte) 20,
        (byte) 59,
        (byte) 232,
        (byte) 170,
        (byte) 50,
        (byte) 166,
        (byte) 5,
        (byte) 126,
        (byte) 29,
        (byte) 192 /*0xC0*/,
        (byte) 190,
        (byte) 56,
        (byte) 169,
        (byte) 186,
        (byte) 51,
        (byte) 189,
        (byte) 163,
        (byte) 146,
        (byte) 53,
        (byte) 170,
        (byte) 193,
        (byte) 30
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[148];
    byte[] numArray9 = new byte[55];
    numArray9[29] = (byte) 43;
    numArray9[12] = (byte) 118;
    numArray9[35] = (byte) 226;
    numArray9[3] = (byte) 6;
    numArray9[4] = (byte) 131;
    numArray9[5] = (byte) 90;
    numArray9[32 /*0x20*/] = (byte) 67;
    numArray9[7] = (byte) 93;
    numArray9[1] = (byte) 173;
    numArray9[42] = (byte) 104;
    numArray9[26] = (byte) 29;
    numArray9[10] = (byte) 69;
    numArray9[13] = (byte) 55;
    numArray9[40] = (byte) 82;
    numArray9[21] = (byte) 219;
    numArray9[28] = (byte) 158;
    numArray9[16 /*0x10*/] = (byte) 31 /*0x1F*/;
    numArray9[17] = (byte) 54;
    numArray9[18] = (byte) 66;
    numArray9[51] = (byte) 173;
    numArray9[20] = (byte) 136;
    numArray9[0] = (byte) 109;
    numArray9[22] = (byte) 123;
    numArray9[23] = (byte) 66;
    numArray9[52] = (byte) 178;
    numArray9[25] = (byte) 113;
    numArray9[31 /*0x1F*/] = (byte) 37;
    numArray9[6] = (byte) 76;
    numArray9[14] = (byte) 173;
    numArray9[44] = (byte) 170;
    numArray9[30] = (byte) 223;
    numArray9[2] = (byte) 225;
    numArray9[38] = (byte) 102;
    numArray9[33] = (byte) 86;
    numArray9[46] = (byte) 76;
    numArray9[19] = (byte) 82;
    numArray9[15] = (byte) 75;
    numArray9[47] = (byte) 10;
    numArray9[34] = (byte) 3;
    numArray9[39] = (byte) 32 /*0x20*/;
    numArray9[24] = (byte) 252;
    numArray9[41] = (byte) 133;
    numArray9[11] = (byte) 42;
    numArray9[43] = (byte) 218;
    numArray9[36] = (byte) 130;
    numArray9[45] = (byte) 200;
    numArray9[9] = (byte) 173;
    numArray9[54] = (byte) 39;
    numArray9[48 /*0x30*/] = (byte) 126;
    numArray9[49] = (byte) 76;
    numArray9[50] = (byte) 148;
    numArray9[27] = (byte) 217;
    numArray9[37] = (byte) 229;
    numArray9[53] = (byte) 230;
    numArray9[8] = (byte) 47;
    byte[] numArray10 = new byte[55];
    numArray10[0] = (byte) 119;
    numArray10[1] = (byte) 211;
    numArray10[43] = (byte) 101;
    numArray10[46] = (byte) 67;
    numArray10[2] = (byte) 73;
    numArray10[5] = (byte) 151;
    numArray10[3] = (byte) 167;
    numArray10[6] = (byte) 52;
    numArray10[8] = (byte) 225;
    numArray10[9] = (byte) 245;
    numArray10[44] = (byte) 101;
    numArray10[11] = (byte) 140;
    numArray10[33] = (byte) 233;
    numArray10[38] = (byte) 77;
    numArray10[18] = (byte) 190;
    numArray10[13] = (byte) 9;
    numArray10[16 /*0x10*/] = (byte) 161;
    numArray10[17] = (byte) 200;
    numArray10[24] = (byte) 83;
    numArray10[37] = (byte) 212;
    numArray10[20] = (byte) 13;
    numArray10[36] = (byte) 177;
    numArray10[4] = (byte) 235;
    numArray10[14] = (byte) 34;
    numArray10[26] = (byte) 36;
    numArray10[49] = (byte) 196;
    numArray10[54] = (byte) 48 /*0x30*/;
    numArray10[52] = (byte) 204;
    numArray10[45] = (byte) 252;
    numArray10[29] = (byte) 16 /*0x10*/;
    numArray10[30] = (byte) 175;
    numArray10[31 /*0x1F*/] = (byte) 22;
    numArray10[32 /*0x20*/] = (byte) 14;
    numArray10[23] = (byte) 195;
    numArray10[34] = (byte) 66;
    numArray10[35] = (byte) 254;
    numArray10[21] = (byte) 233;
    numArray10[7] = (byte) 123;
    numArray10[19] = (byte) 184;
    numArray10[39] = (byte) 215;
    numArray10[40] = (byte) 12;
    numArray10[41] = (byte) 94;
    numArray10[42] = (byte) 175;
    numArray10[10] = (byte) 141;
    numArray10[27] = (byte) 80 /*0x50*/;
    numArray10[25] = (byte) 219;
    numArray10[22] = (byte) 35;
    numArray10[47] = (byte) 205;
    numArray10[48 /*0x30*/] = (byte) 124;
    numArray10[12] = (byte) 245;
    numArray10[15] = (byte) 250;
    numArray10[51] = (byte) 161;
    numArray10[53] = (byte) 72;
    numArray10[28] = (byte) 205;
    numArray10[50] = (byte) 194;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 247,
      (byte) 131,
      (byte) 244,
      (byte) 238,
      (byte) 113,
      (byte) 159,
      (byte) 47,
      (byte) 69,
      (byte) 204,
      (byte) 18,
      (byte) 229,
      (byte) 111,
      (byte) 249,
      (byte) 7,
      (byte) 88,
      (byte) 21,
      (byte) 130,
      (byte) 91,
      (byte) 228,
      (byte) 23,
      (byte) 35,
      (byte) 49,
      (byte) 248,
      (byte) 188,
      (byte) 39,
      (byte) 13,
      (byte) 48 /*0x30*/,
      (byte) 66,
      (byte) 245,
      (byte) 140,
      (byte) 177,
      (byte) 34,
      (byte) 68,
      (byte) 36,
      (byte) 229,
      (byte) 130,
      (byte) 113,
      (byte) 204,
      (byte) 84,
      (byte) 184,
      (byte) 199,
      (byte) 43,
      (byte) 16 /*0x10*/,
      (byte) 70,
      (byte) 86,
      (byte) 164,
      (byte) 160 /*0xA0*/,
      (byte) 150,
      (byte) 162,
      (byte) 74,
      (byte) 210,
      (byte) 105,
      (byte) 4,
      (byte) 246,
      (byte) 216
    };
    byte[] numArray12 = new byte[55];
    numArray12[8] = (byte) 166;
    numArray12[47] = (byte) 13;
    numArray12[40] = (byte) 196;
    numArray12[19] = byte.MaxValue;
    numArray12[28] = (byte) 242;
    numArray12[34] = (byte) 71;
    numArray12[45] = (byte) 37;
    numArray12[7] = (byte) 101;
    numArray12[2] = (byte) 108;
    numArray12[4] = (byte) 187;
    numArray12[27] = (byte) 203;
    numArray12[11] = (byte) 238;
    numArray12[12] = (byte) 130;
    numArray12[36] = (byte) 53;
    numArray12[14] = (byte) 209;
    numArray12[6] = (byte) 231;
    numArray12[15] = (byte) 211;
    numArray12[17] = (byte) 103;
    numArray12[18] = (byte) 66;
    numArray12[44] = (byte) 15;
    numArray12[35] = (byte) 66;
    numArray12[21] = (byte) 97;
    numArray12[22] = (byte) 74;
    numArray12[23] = (byte) 53;
    numArray12[24] = (byte) 76;
    numArray12[20] = (byte) 10;
    numArray12[13] = (byte) 195;
    numArray12[54] = (byte) 15;
    numArray12[39] = (byte) 61;
    numArray12[51] = (byte) 223;
    numArray12[30] = (byte) 21;
    numArray12[0] = (byte) 179;
    numArray12[53] = (byte) 209;
    numArray12[33] = (byte) 28;
    numArray12[26] = (byte) 76;
    numArray12[9] = (byte) 61;
    numArray12[29] = (byte) 147;
    numArray12[37] = (byte) 153;
    numArray12[25] = (byte) 166;
    numArray12[49] = (byte) 4;
    numArray12[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray12[41] = (byte) 236;
    numArray12[38] = (byte) 114;
    numArray12[43] = (byte) 155;
    numArray12[32 /*0x20*/] = (byte) 155;
    numArray12[46] = (byte) 72;
    numArray12[31 /*0x1F*/] = (byte) 207;
    numArray12[42] = (byte) 96 /*0x60*/;
    numArray12[48 /*0x30*/] = (byte) 93;
    numArray12[3] = (byte) 64 /*0x40*/;
    numArray12[50] = (byte) 121;
    numArray12[10] = (byte) 30;
    numArray12[52] = (byte) 24;
    numArray12[5] = (byte) 251;
    numArray12[1] = (byte) 104;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[38];
    numArray13[21] = (byte) 211;
    numArray13[23] = (byte) 161;
    numArray13[2] = (byte) 245;
    numArray13[30] = (byte) 166;
    numArray13[4] = (byte) 78;
    numArray13[5] = (byte) 165;
    numArray13[6] = (byte) 134;
    numArray13[7] = (byte) 24;
    numArray13[16 /*0x10*/] = (byte) 225;
    numArray13[9] = (byte) 222;
    numArray13[10] = (byte) 136;
    numArray13[26] = (byte) 4;
    numArray13[12] = (byte) 221;
    numArray13[8] = (byte) 165;
    numArray13[14] = (byte) 176 /*0xB0*/;
    numArray13[18] = (byte) 28;
    numArray13[1] = (byte) 67;
    numArray13[0] = (byte) 210;
    numArray13[27] = (byte) 157;
    numArray13[19] = (byte) 80 /*0x50*/;
    numArray13[28] = (byte) 229;
    numArray13[36] = (byte) 251;
    numArray13[22] = (byte) 10;
    numArray13[3] = (byte) 213;
    numArray13[24] = (byte) 37;
    numArray13[13] = (byte) 107;
    numArray13[37] = (byte) 116;
    numArray13[25] = (byte) 210;
    numArray13[17] = (byte) 118;
    numArray13[29] = (byte) 69;
    numArray13[11] = (byte) 1;
    numArray13[20] = (byte) 204;
    numArray13[32 /*0x20*/] = (byte) 122;
    numArray13[33] = (byte) 30;
    numArray13[34] = (byte) 102;
    numArray13[31 /*0x1F*/] = (byte) 107;
    numArray13[15] = (byte) 21;
    numArray13[35] = (byte) 194;
    byte[] numArray14 = new byte[38];
    numArray14[1] = (byte) 35;
    numArray14[9] = (byte) 254;
    numArray14[2] = (byte) 247;
    numArray14[3] = (byte) 235;
    numArray14[4] = (byte) 143;
    numArray14[18] = (byte) 217;
    numArray14[6] = (byte) 12;
    numArray14[34] = (byte) 194;
    numArray14[8] = (byte) 67;
    numArray14[7] = (byte) 60;
    numArray14[10] = (byte) 99;
    numArray14[11] = (byte) 10;
    numArray14[16 /*0x10*/] = (byte) 111;
    numArray14[17] = (byte) 28;
    numArray14[26] = (byte) 165;
    numArray14[14] = (byte) 166;
    numArray14[22] = (byte) 27;
    numArray14[20] = (byte) 17;
    numArray14[12] = (byte) 247;
    numArray14[19] = (byte) 64 /*0x40*/;
    numArray14[35] = (byte) 33;
    numArray14[36] = (byte) 89;
    numArray14[27] = (byte) 232;
    numArray14[23] = (byte) 144 /*0x90*/;
    numArray14[24] = (byte) 185;
    numArray14[25] = (byte) 29;
    numArray14[28] = (byte) 237;
    numArray14[13] = (byte) 199;
    numArray14[0] = (byte) 61;
    numArray14[29] = (byte) 19;
    numArray14[30] = (byte) 34;
    numArray14[31 /*0x1F*/] = (byte) 193;
    numArray14[32 /*0x20*/] = (byte) 22;
    numArray14[33] = (byte) 172;
    numArray14[5] = (byte) 96 /*0x60*/;
    numArray14[21] = (byte) 97;
    numArray14[15] = (byte) 38;
    numArray14[37] = (byte) 247;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 38);
    for (int index = 0; index < 38; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12348()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[143];
      byte[] numArray2 = new byte[55];
      numArray2[28] = (byte) 177;
      numArray2[1] = (byte) 194;
      numArray2[2] = (byte) 117;
      numArray2[36] = (byte) 175;
      numArray2[38] = (byte) 132;
      numArray2[5] = (byte) 157;
      numArray2[6] = (byte) 101;
      numArray2[7] = (byte) 113;
      numArray2[34] = (byte) 124;
      numArray2[10] = (byte) 83;
      numArray2[31 /*0x1F*/] = (byte) 187;
      numArray2[11] = (byte) 165;
      numArray2[12] = (byte) 101;
      numArray2[9] = (byte) 218;
      numArray2[14] = (byte) 15;
      numArray2[15] = (byte) 57;
      numArray2[35] = (byte) 158;
      numArray2[17] = (byte) 178;
      numArray2[30] = (byte) 119;
      numArray2[26] = (byte) 79;
      numArray2[24] = (byte) 214;
      numArray2[21] = (byte) 198;
      numArray2[50] = (byte) 53;
      numArray2[49] = (byte) 187;
      numArray2[3] = (byte) 98;
      numArray2[53] = (byte) 0;
      numArray2[39] = (byte) 153;
      numArray2[27] = (byte) 193;
      numArray2[22] = (byte) 47;
      numArray2[52] = (byte) 70;
      numArray2[43] = (byte) 108;
      numArray2[44] = (byte) 136;
      numArray2[32 /*0x20*/] = (byte) 170;
      numArray2[33] = (byte) 252;
      numArray2[13] = (byte) 89;
      numArray2[23] = (byte) 203;
      numArray2[41] = (byte) 194;
      numArray2[37] = (byte) 136;
      numArray2[40] = (byte) 23;
      numArray2[29] = (byte) 198;
      numArray2[16 /*0x10*/] = (byte) 199;
      numArray2[20] = (byte) 98;
      numArray2[42] = (byte) 223;
      numArray2[0] = (byte) 16 /*0x10*/;
      numArray2[19] = (byte) 181;
      numArray2[45] = (byte) 169;
      numArray2[46] = (byte) 168;
      numArray2[47] = (byte) 190;
      numArray2[48 /*0x30*/] = (byte) 184;
      numArray2[25] = (byte) 3;
      numArray2[4] = (byte) 172;
      numArray2[51] = (byte) 230;
      numArray2[18] = (byte) 146;
      numArray2[8] = (byte) 169;
      numArray2[54] = (byte) 57;
      byte[] numArray3 = new byte[55]
      {
        (byte) 128 /*0x80*/,
        (byte) 122,
        (byte) 65,
        (byte) 21,
        (byte) 142,
        (byte) 180,
        (byte) 68,
        (byte) 1,
        (byte) 33,
        (byte) 135,
        (byte) 156,
        (byte) 51,
        (byte) 39,
        (byte) 249,
        (byte) 90,
        (byte) 71,
        (byte) 224 /*0xE0*/,
        (byte) 185,
        (byte) 213,
        (byte) 230,
        (byte) 214,
        (byte) 109,
        (byte) 148,
        (byte) 125,
        (byte) 155,
        (byte) 66,
        (byte) 123,
        (byte) 239,
        (byte) 177,
        (byte) 110,
        (byte) 62,
        (byte) 193,
        (byte) 121,
        (byte) 49,
        (byte) 11,
        (byte) 7,
        (byte) 4,
        (byte) 167,
        (byte) 229,
        (byte) 84,
        (byte) 31 /*0x1F*/,
        (byte) 213,
        (byte) 197,
        (byte) 211,
        (byte) 190,
        (byte) 1,
        (byte) 48 /*0x30*/,
        (byte) 153,
        (byte) 199,
        (byte) 120,
        (byte) 215,
        (byte) 178,
        (byte) 166,
        (byte) 210,
        (byte) 161
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 104,
        (byte) 94,
        (byte) 76,
        (byte) 189,
        (byte) 153,
        (byte) 201,
        (byte) 81,
        (byte) 210,
        (byte) 35,
        (byte) 80 /*0x50*/,
        (byte) 165,
        (byte) 172,
        (byte) 102,
        (byte) 133,
        (byte) 65,
        (byte) 82,
        (byte) 173,
        (byte) 42,
        (byte) 158,
        (byte) 233,
        (byte) 185,
        (byte) 81,
        (byte) 125,
        (byte) 28,
        (byte) 175,
        (byte) 194,
        (byte) 197,
        (byte) 153,
        (byte) 215,
        (byte) 38,
        (byte) 71,
        (byte) 42,
        (byte) 204,
        (byte) 57,
        (byte) 156,
        (byte) 158,
        (byte) 142,
        (byte) 141,
        (byte) 218,
        (byte) 56,
        (byte) 171,
        (byte) 55,
        (byte) 204,
        (byte) 94,
        (byte) 153,
        (byte) 52,
        (byte) 224 /*0xE0*/,
        (byte) 171,
        (byte) 182,
        (byte) 102,
        (byte) 161,
        (byte) 55,
        (byte) 250,
        (byte) 123,
        (byte) 24
      };
      byte[] numArray5 = new byte[55];
      numArray5[14] = (byte) 107;
      numArray5[1] = (byte) 130;
      numArray5[19] = (byte) 87;
      numArray5[45] = (byte) 122;
      numArray5[36] = (byte) 193;
      numArray5[28] = (byte) 112 /*0x70*/;
      numArray5[4] = (byte) 227;
      numArray5[22] = (byte) 53;
      numArray5[33] = (byte) 59;
      numArray5[9] = (byte) 115;
      numArray5[10] = (byte) 124;
      numArray5[8] = (byte) 20;
      numArray5[7] = (byte) 59;
      numArray5[13] = (byte) 34;
      numArray5[50] = (byte) 205;
      numArray5[15] = (byte) 142;
      numArray5[20] = (byte) 203;
      numArray5[47] = (byte) 73;
      numArray5[34] = (byte) 98;
      numArray5[31 /*0x1F*/] = (byte) 117;
      numArray5[3] = (byte) 190;
      numArray5[52] = (byte) 24;
      numArray5[12] = (byte) 52;
      numArray5[23] = (byte) 238;
      numArray5[2] = (byte) 172;
      numArray5[25] = (byte) 41;
      numArray5[16 /*0x10*/] = (byte) 217;
      numArray5[27] = (byte) 140;
      numArray5[24] = (byte) 114;
      numArray5[53] = (byte) 69;
      numArray5[30] = (byte) 29;
      numArray5[11] = (byte) 213;
      numArray5[32 /*0x20*/] = (byte) 2;
      numArray5[5] = (byte) 94;
      numArray5[49] = (byte) 232;
      numArray5[48 /*0x30*/] = (byte) 14;
      numArray5[17] = (byte) 169;
      numArray5[37] = (byte) 216;
      numArray5[38] = (byte) 111;
      numArray5[39] = (byte) 91;
      numArray5[35] = (byte) 47;
      numArray5[0] = (byte) 4;
      numArray5[42] = (byte) 238;
      numArray5[43] = (byte) 6;
      numArray5[51] = (byte) 199;
      numArray5[6] = (byte) 77;
      numArray5[46] = (byte) 123;
      numArray5[44] = (byte) 171;
      numArray5[29] = (byte) 254;
      numArray5[26] = (byte) 20;
      numArray5[21] = (byte) 175;
      numArray5[41] = (byte) 129;
      numArray5[40] = (byte) 125;
      numArray5[18] = (byte) 143;
      numArray5[54] = (byte) 96 /*0x60*/;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[33];
      numArray6[31 /*0x1F*/] = (byte) 139;
      numArray6[25] = (byte) 207;
      numArray6[22] = (byte) 37;
      numArray6[3] = (byte) 233;
      numArray6[32 /*0x20*/] = (byte) 244;
      numArray6[14] = (byte) 159;
      numArray6[1] = (byte) 148;
      numArray6[7] = (byte) 249;
      numArray6[5] = (byte) 138;
      numArray6[9] = (byte) 142;
      numArray6[19] = (byte) 182;
      numArray6[21] = (byte) 99;
      numArray6[6] = (byte) 121;
      numArray6[13] = (byte) 100;
      numArray6[26] = (byte) 204;
      numArray6[20] = (byte) 201;
      numArray6[16 /*0x10*/] = (byte) 48 /*0x30*/;
      numArray6[17] = (byte) 73;
      numArray6[12] = (byte) 228;
      numArray6[8] = (byte) 74;
      numArray6[10] = (byte) 11;
      numArray6[28] = (byte) 40;
      numArray6[4] = (byte) 212;
      numArray6[23] = (byte) 45;
      numArray6[15] = (byte) 77;
      numArray6[0] = (byte) 173;
      numArray6[2] = (byte) 245;
      numArray6[27] = (byte) 2;
      numArray6[24] = (byte) 88;
      numArray6[18] = (byte) 69;
      numArray6[30] = (byte) 19;
      numArray6[11] = (byte) 99;
      numArray6[29] = (byte) 154;
      byte[] numArray7 = new byte[33]
      {
        (byte) 171,
        (byte) 147,
        (byte) 93,
        (byte) 73,
        (byte) 177,
        (byte) 115,
        (byte) 16 /*0x10*/,
        (byte) 17,
        (byte) 227,
        (byte) 129,
        (byte) 103,
        (byte) 58,
        (byte) 140,
        (byte) 87,
        (byte) 78,
        (byte) 146,
        (byte) 223,
        (byte) 83,
        (byte) 170,
        (byte) 184,
        (byte) 157,
        (byte) 181,
        (byte) 3,
        (byte) 28,
        (byte) 224 /*0xE0*/,
        (byte) 177,
        (byte) 164,
        (byte) 242,
        (byte) 31 /*0x1F*/,
        (byte) 107,
        (byte) 2,
        (byte) 101,
        (byte) 99
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[143];
    byte[] numArray9 = new byte[55];
    numArray9[51] = (byte) 69;
    numArray9[1] = (byte) 64 /*0x40*/;
    numArray9[2] = (byte) 229;
    numArray9[46] = (byte) 48 /*0x30*/;
    numArray9[4] = (byte) 146;
    numArray9[5] = (byte) 96 /*0x60*/;
    numArray9[6] = (byte) 108;
    numArray9[8] = (byte) 95;
    numArray9[49] = (byte) 122;
    numArray9[9] = (byte) 132;
    numArray9[29] = (byte) 74;
    numArray9[10] = (byte) 250;
    numArray9[12] = (byte) 140;
    numArray9[13] = (byte) 32 /*0x20*/;
    numArray9[27] = (byte) 140;
    numArray9[15] = (byte) 53;
    numArray9[16 /*0x10*/] = (byte) 30;
    numArray9[17] = (byte) 57;
    numArray9[3] = (byte) 42;
    numArray9[11] = (byte) 159;
    numArray9[40] = (byte) 140;
    numArray9[21] = (byte) 124;
    numArray9[7] = (byte) 142;
    numArray9[23] = (byte) 185;
    numArray9[24] = (byte) 25;
    numArray9[25] = (byte) 244;
    numArray9[18] = (byte) 25;
    numArray9[54] = (byte) 74;
    numArray9[28] = (byte) 37;
    numArray9[33] = (byte) 141;
    numArray9[30] = (byte) 41;
    numArray9[0] = (byte) 180;
    numArray9[32 /*0x20*/] = (byte) 72;
    numArray9[14] = (byte) 142;
    numArray9[34] = (byte) 45;
    numArray9[35] = (byte) 89;
    numArray9[19] = (byte) 4;
    numArray9[50] = (byte) 93;
    numArray9[38] = (byte) 190;
    numArray9[41] = (byte) 187;
    numArray9[22] = (byte) 68;
    numArray9[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray9[20] = (byte) 104;
    numArray9[43] = (byte) 68;
    numArray9[44] = (byte) 151;
    numArray9[45] = (byte) 101;
    numArray9[36] = (byte) 127 /*0x7F*/;
    numArray9[47] = (byte) 85;
    numArray9[48 /*0x30*/] = (byte) 66;
    numArray9[42] = (byte) 15;
    numArray9[26] = (byte) 223;
    numArray9[52] = (byte) 84;
    numArray9[39] = (byte) 122;
    numArray9[53] = (byte) 227;
    numArray9[37] = (byte) 202;
    byte[] numArray10 = new byte[55]
    {
      (byte) 75,
      (byte) 227,
      (byte) 231,
      (byte) 19,
      (byte) 105,
      (byte) 41,
      (byte) 150,
      (byte) 59,
      (byte) 203,
      (byte) 44,
      (byte) 32 /*0x20*/,
      (byte) 138,
      (byte) 102,
      (byte) 121,
      (byte) 85,
      (byte) 44,
      (byte) 2,
      (byte) 66,
      (byte) 246,
      (byte) 6,
      (byte) 224 /*0xE0*/,
      (byte) 52,
      (byte) 234,
      (byte) 248,
      (byte) 116,
      (byte) 159,
      (byte) 155,
      (byte) 25,
      (byte) 130,
      (byte) 152,
      (byte) 151,
      (byte) 146,
      (byte) 129,
      (byte) 165,
      (byte) 249,
      (byte) 129,
      (byte) 168,
      (byte) 224 /*0xE0*/,
      (byte) 100,
      (byte) 70,
      (byte) 83,
      (byte) 94,
      (byte) 204,
      (byte) 173,
      (byte) 68,
      (byte) 198,
      (byte) 238,
      (byte) 62,
      (byte) 146,
      (byte) 104,
      (byte) 248,
      (byte) 158,
      (byte) 85,
      (byte) 93,
      (byte) 103
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[53] = (byte) 89;
    numArray11[1] = (byte) 91;
    numArray11[15] = (byte) 90;
    numArray11[3] = (byte) 248;
    numArray11[4] = (byte) 150;
    numArray11[36] = (byte) 70;
    numArray11[23] = (byte) 146;
    numArray11[33] = (byte) 96 /*0x60*/;
    numArray11[8] = (byte) 109;
    numArray11[5] = (byte) 190;
    numArray11[10] = (byte) 188;
    numArray11[48 /*0x30*/] = (byte) 111;
    numArray11[12] = (byte) 216;
    numArray11[49] = (byte) 112 /*0x70*/;
    numArray11[50] = (byte) 13;
    numArray11[31 /*0x1F*/] = (byte) 166;
    numArray11[16 /*0x10*/] = (byte) 167;
    numArray11[42] = (byte) 31 /*0x1F*/;
    numArray11[18] = (byte) 91;
    numArray11[45] = (byte) 99;
    numArray11[29] = (byte) 231;
    numArray11[21] = (byte) 149;
    numArray11[22] = (byte) 166;
    numArray11[32 /*0x20*/] = (byte) 40;
    numArray11[26] = (byte) 230;
    numArray11[25] = (byte) 61;
    numArray11[14] = (byte) 153;
    numArray11[27] = (byte) 227;
    numArray11[30] = (byte) 250;
    numArray11[2] = (byte) 217;
    numArray11[6] = (byte) 40;
    numArray11[39] = (byte) 147;
    numArray11[38] = (byte) 21;
    numArray11[28] = (byte) 24;
    numArray11[34] = (byte) 74;
    numArray11[9] = (byte) 242;
    numArray11[41] = (byte) 114;
    numArray11[37] = (byte) 0;
    numArray11[20] = (byte) 22;
    numArray11[7] = (byte) 119;
    numArray11[17] = (byte) 204;
    numArray11[19] = (byte) 114;
    numArray11[35] = (byte) 94;
    numArray11[24] = (byte) 38;
    numArray11[13] = (byte) 98;
    numArray11[40] = (byte) 210;
    numArray11[46] = (byte) 52;
    numArray11[47] = (byte) 98;
    numArray11[11] = (byte) 254;
    numArray11[43] = (byte) 27;
    numArray11[0] = (byte) 9;
    numArray11[51] = (byte) 158;
    numArray11[52] = (byte) 65;
    numArray11[44] = (byte) 117;
    numArray11[54] = (byte) 229;
    byte[] numArray12 = new byte[55]
    {
      (byte) 231,
      (byte) 40,
      (byte) 100,
      (byte) 158,
      (byte) 24,
      (byte) 130,
      (byte) 195,
      (byte) 208 /*0xD0*/,
      (byte) 134,
      (byte) 126,
      (byte) 185,
      (byte) 22,
      (byte) 178,
      (byte) 187,
      (byte) 36,
      (byte) 53,
      (byte) 158,
      (byte) 98,
      (byte) 13,
      (byte) 238,
      (byte) 227,
      (byte) 124,
      (byte) 230,
      (byte) 4,
      (byte) 239,
      byte.MaxValue,
      (byte) 80 /*0x50*/,
      (byte) 125,
      (byte) 156,
      (byte) 120,
      (byte) 93,
      (byte) 225,
      (byte) 174,
      (byte) 231,
      (byte) 28,
      (byte) 178,
      (byte) 230,
      (byte) 231,
      (byte) 19,
      (byte) 226,
      (byte) 218,
      (byte) 75,
      (byte) 159,
      (byte) 194,
      (byte) 142,
      (byte) 247,
      (byte) 139,
      (byte) 50,
      (byte) 13,
      (byte) 62,
      (byte) 41,
      (byte) 170,
      (byte) 168,
      (byte) 164,
      (byte) 235
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[33]
    {
      (byte) 41,
      (byte) 13,
      (byte) 79,
      (byte) 84,
      (byte) 209,
      (byte) 181,
      (byte) 173,
      (byte) 156,
      (byte) 177,
      (byte) 7,
      (byte) 252,
      (byte) 64 /*0x40*/,
      (byte) 145,
      (byte) 20,
      (byte) 120,
      (byte) 249,
      (byte) 13,
      (byte) 17,
      (byte) 143,
      (byte) 220,
      (byte) 104,
      (byte) 190,
      (byte) 54,
      (byte) 77,
      (byte) 56,
      (byte) 62,
      (byte) 75,
      (byte) 115,
      (byte) 116,
      (byte) 235,
      (byte) 20,
      (byte) 57,
      (byte) 217
    };
    byte[] numArray14 = new byte[33];
    numArray14[12] = (byte) 216;
    numArray14[0] = (byte) 154;
    numArray14[2] = (byte) 233;
    numArray14[3] = (byte) 43;
    numArray14[4] = (byte) 71;
    numArray14[15] = (byte) 71;
    numArray14[6] = (byte) 88;
    numArray14[29] = (byte) 83;
    numArray14[8] = (byte) 116;
    numArray14[7] = (byte) 61;
    numArray14[10] = (byte) 48 /*0x30*/;
    numArray14[13] = (byte) 106;
    numArray14[1] = (byte) 86;
    numArray14[32 /*0x20*/] = (byte) 106;
    numArray14[22] = (byte) 168;
    numArray14[5] = (byte) 232;
    numArray14[16 /*0x10*/] = (byte) 95;
    numArray14[17] = (byte) 142;
    numArray14[18] = (byte) 58;
    numArray14[19] = (byte) 193;
    numArray14[9] = (byte) 234;
    numArray14[21] = (byte) 158;
    numArray14[11] = (byte) 103;
    numArray14[23] = (byte) 196;
    numArray14[24] = (byte) 122;
    numArray14[25] = (byte) 56;
    numArray14[26] = (byte) 170;
    numArray14[27] = (byte) 162;
    numArray14[20] = (byte) 116;
    numArray14[14] = (byte) 13;
    numArray14[30] = (byte) 211;
    numArray14[31 /*0x1F*/] = (byte) 235;
    numArray14[28] = (byte) 115;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 33);
    for (int index = 0; index < 33; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
