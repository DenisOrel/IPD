// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13496
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13496
{
  private static byte[] sspq = new byte[69]
  {
    (byte) 165,
    (byte) 99,
    (byte) 45,
    (byte) 60,
    (byte) 246,
    (byte) 205,
    (byte) 53,
    (byte) 198,
    (byte) 69,
    (byte) 7,
    (byte) 237,
    (byte) 185,
    (byte) 231,
    (byte) 186,
    (byte) 229,
    (byte) 100,
    (byte) 253,
    (byte) 34,
    (byte) 117,
    (byte) 209,
    (byte) 55,
    (byte) 136,
    (byte) 146,
    (byte) 222,
    (byte) 2,
    (byte) 189,
    (byte) 219,
    (byte) 99,
    (byte) 116,
    (byte) 215,
    (byte) 20,
    (byte) 179,
    (byte) 204,
    (byte) 41,
    (byte) 139,
    (byte) 109,
    (byte) 72,
    (byte) 81,
    (byte) 226,
    (byte) 219,
    (byte) 35,
    (byte) 208 /*0xD0*/,
    (byte) 132,
    (byte) 192 /*0xC0*/,
    (byte) 37,
    (byte) 41,
    (byte) 190,
    (byte) 156,
    (byte) 225,
    (byte) 40,
    (byte) 198,
    (byte) 155,
    (byte) 124,
    (byte) 80 /*0x50*/,
    (byte) 19,
    (byte) 140,
    (byte) 99,
    (byte) 8,
    (byte) 145,
    (byte) 97,
    (byte) 122,
    (byte) 81,
    (byte) 254,
    (byte) 244,
    (byte) 127 /*0x7F*/,
    (byte) 129,
    (byte) 41,
    (byte) 248,
    (byte) 236
  };
  private static byte[] sspr = new byte[69]
  {
    (byte) 9,
    (byte) 93,
    (byte) 117,
    (byte) 144 /*0x90*/,
    (byte) 180,
    (byte) 111,
    (byte) 3,
    (byte) 124,
    (byte) 103,
    (byte) 62,
    (byte) 66,
    (byte) 45,
    (byte) 243,
    (byte) 51,
    (byte) 239,
    (byte) 246,
    (byte) 232,
    (byte) 25,
    (byte) 19,
    (byte) 61,
    (byte) 114,
    (byte) 47,
    (byte) 238,
    (byte) 219,
    (byte) 193,
    (byte) 108,
    (byte) 127 /*0x7F*/,
    (byte) 114,
    (byte) 203,
    (byte) 136,
    (byte) 238,
    (byte) 224 /*0xE0*/,
    (byte) 78,
    (byte) 250,
    (byte) 217,
    (byte) 178,
    (byte) 106,
    (byte) 230,
    (byte) 26,
    (byte) 158,
    (byte) 179,
    (byte) 144 /*0x90*/,
    (byte) 78,
    (byte) 246,
    (byte) 218,
    (byte) 5,
    (byte) 76,
    (byte) 203,
    (byte) 182,
    (byte) 209,
    (byte) 81,
    (byte) 229,
    (byte) 253,
    (byte) 232,
    (byte) 218,
    (byte) 223,
    (byte) 114,
    (byte) 226,
    (byte) 40,
    (byte) 173,
    (byte) 245,
    (byte) 161,
    (byte) 192 /*0xC0*/,
    (byte) 213,
    (byte) 195,
    (byte) 79,
    (byte) 38,
    (byte) 42,
    (byte) 173
  };

  internal static int ssp_appserver_13497(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 164;
    sourceArray1[30] = (byte) 20;
    sourceArray1[1] = (byte) 41;
    sourceArray1[3] = (byte) 46;
    sourceArray1[19] = (byte) 100;
    sourceArray1[23] = (byte) 12;
    sourceArray1[6] = (byte) 170;
    sourceArray1[10] = (byte) 225;
    sourceArray1[27] = (byte) 30;
    sourceArray1[9] = (byte) 120;
    sourceArray1[33] = (byte) 18;
    sourceArray1[11] = (byte) 112 /*0x70*/;
    sourceArray1[28] = (byte) 105;
    sourceArray1[13] = (byte) 200;
    sourceArray1[46] = (byte) 245;
    sourceArray1[15] = (byte) 34;
    sourceArray1[2] = (byte) 89;
    sourceArray1[7] = (byte) 180;
    sourceArray1[16 /*0x10*/] = (byte) 148;
    sourceArray1[35] = (byte) 83;
    sourceArray1[25] = (byte) 197;
    sourceArray1[31 /*0x1F*/] = (byte) 118;
    sourceArray1[22] = (byte) 39;
    sourceArray1[38] = (byte) 160 /*0xA0*/;
    sourceArray1[4] = (byte) 124;
    sourceArray1[20] = (byte) 80 /*0x50*/;
    sourceArray1[26] = (byte) 18;
    sourceArray1[18] = (byte) 94;
    sourceArray1[5] = (byte) 237;
    sourceArray1[29] = (byte) 252;
    sourceArray1[8] = (byte) 222;
    sourceArray1[39] = (byte) 226;
    sourceArray1[32 /*0x20*/] = (byte) 3;
    sourceArray1[17] = (byte) 237;
    sourceArray1[34] = (byte) 90;
    sourceArray1[0] = (byte) 178;
    sourceArray1[36] = (byte) 217;
    sourceArray1[37] = (byte) 210;
    sourceArray1[14] = (byte) 167;
    sourceArray1[43] = (byte) 223;
    sourceArray1[40] = (byte) 140;
    sourceArray1[41] = (byte) 67;
    sourceArray1[21] = (byte) 232;
    sourceArray1[42] = (byte) 11;
    sourceArray1[44] = (byte) 166;
    sourceArray1[45] = (byte) 18;
    sourceArray1[24] = (byte) 67;
    sourceArray1[47] = (byte) 156;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 21,
      (byte) 37,
      (byte) 82,
      (byte) 81,
      (byte) 73,
      (byte) 118,
      (byte) 219,
      (byte) 152,
      (byte) 123,
      (byte) 121,
      (byte) 163,
      (byte) 164,
      (byte) 129,
      (byte) 78,
      (byte) 110,
      (byte) 1,
      (byte) 20,
      (byte) 208 /*0xD0*/,
      (byte) 226,
      (byte) 235,
      (byte) 141,
      (byte) 59,
      (byte) 127 /*0x7F*/,
      (byte) 231,
      (byte) 42,
      (byte) 187,
      (byte) 69,
      (byte) 200,
      (byte) 142,
      (byte) 8,
      (byte) 60,
      (byte) 15,
      (byte) 63 /*0x3F*/,
      (byte) 119,
      (byte) 62,
      (byte) 62,
      (byte) 127 /*0x7F*/,
      (byte) 136,
      (byte) 106,
      (byte) 69,
      (byte) 209,
      (byte) 7,
      (byte) 91,
      (byte) 110,
      (byte) 24,
      (byte) 20,
      (byte) 106,
      (byte) 66
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[38];
    byte[] response2 = new byte[38];
    Array.Copy((Array) sc_13496.sspq, 0, (Array) numArray2, 0, 38);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13496.sspr, 0, (Array) numArray2, 0, 38);
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

  internal static string ssp_appserver_13498()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[137];
      byte[] numArray2 = new byte[55]
      {
        (byte) 159,
        (byte) 92,
        (byte) 108,
        (byte) 159,
        (byte) 247,
        (byte) 162,
        (byte) 152,
        (byte) 163,
        (byte) 44,
        (byte) 4,
        (byte) 163,
        (byte) 2,
        (byte) 214,
        (byte) 179,
        (byte) 170,
        (byte) 3,
        (byte) 29,
        (byte) 208 /*0xD0*/,
        (byte) 79,
        (byte) 57,
        (byte) 56,
        (byte) 241,
        (byte) 210,
        (byte) 81,
        (byte) 110,
        (byte) 242,
        (byte) 58,
        (byte) 236,
        (byte) 190,
        (byte) 46,
        (byte) 120,
        (byte) 27,
        (byte) 172,
        (byte) 239,
        (byte) 214,
        (byte) 117,
        (byte) 202,
        (byte) 232,
        (byte) 155,
        (byte) 60,
        (byte) 105,
        (byte) 25,
        (byte) 175,
        (byte) 95,
        (byte) 209,
        (byte) 212,
        (byte) 61,
        byte.MaxValue,
        (byte) 34,
        (byte) 38,
        (byte) 110,
        (byte) 156,
        (byte) 249,
        (byte) 23,
        (byte) 250
      };
      byte[] numArray3 = new byte[55];
      numArray3[46] = (byte) 117;
      numArray3[16 /*0x10*/] = (byte) 71;
      numArray3[20] = (byte) 109;
      numArray3[43] = (byte) 89;
      numArray3[3] = (byte) 62;
      numArray3[2] = (byte) 167;
      numArray3[6] = (byte) 38;
      numArray3[36] = (byte) 216;
      numArray3[8] = (byte) 36;
      numArray3[17] = (byte) 243;
      numArray3[7] = (byte) 109;
      numArray3[11] = (byte) 211;
      numArray3[1] = (byte) 153;
      numArray3[13] = (byte) 142;
      numArray3[9] = (byte) 178;
      numArray3[34] = (byte) 69;
      numArray3[38] = (byte) 23;
      numArray3[4] = (byte) 95;
      numArray3[18] = (byte) 29;
      numArray3[19] = (byte) 16 /*0x10*/;
      numArray3[10] = (byte) 245;
      numArray3[49] = (byte) 236;
      numArray3[15] = (byte) 114;
      numArray3[52] = (byte) 141;
      numArray3[32 /*0x20*/] = (byte) 79;
      numArray3[51] = (byte) 248;
      numArray3[26] = (byte) 191;
      numArray3[27] = (byte) 187;
      numArray3[28] = (byte) 148;
      numArray3[29] = (byte) 91;
      numArray3[30] = (byte) 52;
      numArray3[31 /*0x1F*/] = (byte) 197;
      numArray3[45] = (byte) 183;
      numArray3[33] = (byte) 80 /*0x50*/;
      numArray3[21] = (byte) 117;
      numArray3[35] = (byte) 140;
      numArray3[5] = (byte) 164;
      numArray3[37] = (byte) 168;
      numArray3[24] = (byte) 41;
      numArray3[39] = (byte) 40;
      numArray3[40] = (byte) 170;
      numArray3[50] = (byte) 16 /*0x10*/;
      numArray3[42] = byte.MaxValue;
      numArray3[22] = (byte) 55;
      numArray3[44] = (byte) 153;
      numArray3[25] = (byte) 93;
      numArray3[48 /*0x30*/] = (byte) 195;
      numArray3[47] = (byte) 254;
      numArray3[23] = (byte) 204;
      numArray3[14] = (byte) 45;
      numArray3[0] = (byte) 133;
      numArray3[12] = (byte) 97;
      numArray3[41] = (byte) 94;
      numArray3[53] = (byte) 10;
      numArray3[54] = byte.MaxValue;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 155,
        (byte) 150,
        (byte) 79,
        (byte) 44,
        (byte) 225,
        (byte) 77,
        (byte) 61,
        (byte) 175,
        (byte) 101,
        (byte) 193,
        (byte) 242,
        (byte) 181,
        (byte) 72,
        (byte) 137,
        (byte) 124,
        (byte) 158,
        (byte) 61,
        (byte) 18,
        (byte) 235,
        (byte) 143,
        (byte) 159,
        (byte) 53,
        (byte) 28,
        (byte) 94,
        (byte) 24,
        (byte) 221,
        (byte) 170,
        (byte) 133,
        (byte) 117,
        (byte) 59,
        (byte) 77,
        (byte) 25,
        (byte) 187,
        (byte) 213,
        (byte) 20,
        (byte) 33,
        (byte) 71,
        (byte) 168,
        (byte) 220,
        (byte) 229,
        (byte) 116,
        (byte) 36,
        (byte) 229,
        (byte) 188,
        (byte) 99,
        (byte) 109,
        (byte) 165,
        (byte) 100,
        (byte) 190,
        (byte) 200,
        (byte) 168,
        (byte) 52,
        (byte) 239,
        (byte) 220,
        (byte) 119
      };
      byte[] numArray5 = new byte[55];
      numArray5[14] = (byte) 141;
      numArray5[46] = (byte) 185;
      numArray5[33] = (byte) 61;
      numArray5[3] = (byte) 159;
      numArray5[4] = (byte) 233;
      numArray5[12] = (byte) 27;
      numArray5[50] = (byte) 141;
      numArray5[38] = (byte) 120;
      numArray5[5] = (byte) 184;
      numArray5[34] = (byte) 232;
      numArray5[27] = (byte) 244;
      numArray5[11] = (byte) 80 /*0x50*/;
      numArray5[1] = (byte) 117;
      numArray5[13] = (byte) 2;
      numArray5[42] = (byte) 174;
      numArray5[15] = (byte) 159;
      numArray5[16 /*0x10*/] = (byte) 165;
      numArray5[7] = (byte) 113;
      numArray5[0] = (byte) 59;
      numArray5[23] = (byte) 157;
      numArray5[35] = (byte) 78;
      numArray5[21] = (byte) 120;
      numArray5[22] = (byte) 56;
      numArray5[17] = (byte) 213;
      numArray5[41] = (byte) 99;
      numArray5[25] = (byte) 86;
      numArray5[26] = (byte) 18;
      numArray5[31 /*0x1F*/] = (byte) 164;
      numArray5[20] = (byte) 114;
      numArray5[29] = (byte) 193;
      numArray5[30] = (byte) 121;
      numArray5[18] = (byte) 75;
      numArray5[32 /*0x20*/] = byte.MaxValue;
      numArray5[8] = (byte) 223;
      numArray5[36] = (byte) 234;
      numArray5[24] = (byte) 112 /*0x70*/;
      numArray5[47] = (byte) 7;
      numArray5[37] = (byte) 22;
      numArray5[10] = (byte) 147;
      numArray5[39] = (byte) 61;
      numArray5[40] = (byte) 104;
      numArray5[2] = (byte) 74;
      numArray5[52] = (byte) 210;
      numArray5[43] = (byte) 227;
      numArray5[44] = (byte) 200;
      numArray5[45] = (byte) 73;
      numArray5[54] = (byte) 69;
      numArray5[9] = (byte) 77;
      numArray5[48 /*0x30*/] = (byte) 161;
      numArray5[49] = (byte) 121;
      numArray5[6] = (byte) 86;
      numArray5[51] = (byte) 0;
      numArray5[19] = (byte) 40;
      numArray5[53] = (byte) 61;
      numArray5[28] = (byte) 206;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[27]
      {
        (byte) 245,
        (byte) 104,
        (byte) 204,
        (byte) 92,
        (byte) 240 /*0xF0*/,
        (byte) 141,
        (byte) 149,
        (byte) 90,
        (byte) 175,
        (byte) 26,
        (byte) 21,
        (byte) 197,
        (byte) 179,
        (byte) 1,
        (byte) 0,
        (byte) 93,
        (byte) 194,
        (byte) 93,
        (byte) 145,
        (byte) 148,
        (byte) 22,
        (byte) 112 /*0x70*/,
        (byte) 55,
        (byte) 6,
        (byte) 87,
        (byte) 100,
        (byte) 60
      };
      byte[] numArray7 = new byte[27]
      {
        (byte) 182,
        (byte) 58,
        (byte) 249,
        (byte) 178,
        (byte) 77,
        (byte) 45,
        (byte) 64 /*0x40*/,
        (byte) 199,
        (byte) 152,
        (byte) 186,
        (byte) 117,
        (byte) 123,
        (byte) 55,
        (byte) 33,
        (byte) 85,
        (byte) 89,
        (byte) 80 /*0x50*/,
        (byte) 130,
        (byte) 153,
        (byte) 71,
        (byte) 118,
        (byte) 156,
        (byte) 201,
        (byte) 195,
        (byte) 233,
        (byte) 132,
        (byte) 33
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[137];
    byte[] numArray9 = new byte[55]
    {
      (byte) 28,
      (byte) 97,
      (byte) 154,
      (byte) 139,
      (byte) 96 /*0x60*/,
      (byte) 43,
      (byte) 12,
      (byte) 230,
      (byte) 204,
      (byte) 93,
      (byte) 38,
      (byte) 238,
      (byte) 218,
      (byte) 89,
      (byte) 27,
      (byte) 42,
      (byte) 175,
      (byte) 111,
      (byte) 19,
      (byte) 161,
      (byte) 39,
      (byte) 100,
      (byte) 107,
      (byte) 128 /*0x80*/,
      (byte) 5,
      (byte) 133,
      (byte) 49,
      (byte) 198,
      (byte) 5,
      (byte) 140,
      (byte) 80 /*0x50*/,
      (byte) 22,
      (byte) 63 /*0x3F*/,
      (byte) 112 /*0x70*/,
      (byte) 253,
      (byte) 180,
      (byte) 43,
      (byte) 40,
      (byte) 160 /*0xA0*/,
      (byte) 212,
      (byte) 81,
      (byte) 220,
      (byte) 144 /*0x90*/,
      (byte) 52,
      (byte) 110,
      (byte) 39,
      (byte) 150,
      (byte) 221,
      (byte) 234,
      (byte) 44,
      (byte) 180,
      (byte) 205,
      (byte) 108,
      (byte) 121,
      (byte) 23
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 157,
      (byte) 123,
      (byte) 125,
      (byte) 38,
      (byte) 240 /*0xF0*/,
      (byte) 8,
      (byte) 105,
      (byte) 236,
      (byte) 152,
      (byte) 10,
      (byte) 45,
      (byte) 0,
      (byte) 21,
      (byte) 232,
      (byte) 184,
      (byte) 219,
      (byte) 205,
      (byte) 151,
      (byte) 109,
      (byte) 253,
      (byte) 204,
      (byte) 172,
      (byte) 6,
      (byte) 12,
      (byte) 202,
      (byte) 57,
      (byte) 215,
      (byte) 179,
      (byte) 30,
      (byte) 220,
      (byte) 184,
      (byte) 120,
      (byte) 249,
      (byte) 216,
      (byte) 34,
      (byte) 138,
      (byte) 179,
      (byte) 7,
      (byte) 41,
      (byte) 190,
      (byte) 67,
      (byte) 190,
      (byte) 107,
      (byte) 225,
      (byte) 147,
      (byte) 54,
      (byte) 114,
      (byte) 176 /*0xB0*/,
      (byte) 3,
      (byte) 223,
      (byte) 241,
      (byte) 2,
      (byte) 167,
      (byte) 7,
      (byte) 31 /*0x1F*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 25,
      (byte) 134,
      (byte) 219,
      (byte) 250,
      (byte) 70,
      (byte) 224 /*0xE0*/,
      (byte) 64 /*0x40*/,
      (byte) 12,
      (byte) 15,
      (byte) 174,
      (byte) 105,
      (byte) 92,
      (byte) 103,
      byte.MaxValue,
      (byte) 135,
      (byte) 206,
      (byte) 62,
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 212,
      (byte) 54,
      (byte) 18,
      (byte) 221,
      (byte) 238,
      (byte) 84,
      (byte) 219,
      (byte) 216,
      (byte) 215,
      (byte) 131,
      (byte) 169,
      (byte) 198,
      (byte) 89,
      (byte) 69,
      (byte) 190,
      (byte) 17,
      (byte) 161,
      (byte) 197,
      (byte) 218,
      (byte) 10,
      (byte) 252,
      (byte) 79,
      (byte) 39,
      (byte) 94,
      (byte) 120,
      (byte) 44,
      (byte) 129,
      (byte) 130,
      (byte) 45,
      (byte) 45,
      (byte) 254,
      (byte) 46,
      (byte) 40,
      (byte) 204,
      (byte) 227,
      (byte) 191
    };
    byte[] numArray12 = new byte[55];
    numArray12[36] = (byte) 43;
    numArray12[17] = (byte) 82;
    numArray12[8] = (byte) 141;
    numArray12[11] = (byte) 80 /*0x50*/;
    numArray12[4] = (byte) 149;
    numArray12[14] = (byte) 105;
    numArray12[6] = (byte) 63 /*0x3F*/;
    numArray12[45] = (byte) 61;
    numArray12[52] = (byte) 167;
    numArray12[51] = (byte) 166;
    numArray12[10] = (byte) 7;
    numArray12[50] = (byte) 82;
    numArray12[42] = (byte) 237;
    numArray12[13] = (byte) 122;
    numArray12[34] = (byte) 241;
    numArray12[35] = (byte) 197;
    numArray12[48 /*0x30*/] = (byte) 46;
    numArray12[7] = (byte) 125;
    numArray12[18] = (byte) 126;
    numArray12[0] = (byte) 154;
    numArray12[9] = (byte) 70;
    numArray12[38] = (byte) 227;
    numArray12[22] = (byte) 179;
    numArray12[23] = (byte) 224 /*0xE0*/;
    numArray12[24] = (byte) 69;
    numArray12[2] = (byte) 182;
    numArray12[28] = (byte) 14;
    numArray12[27] = (byte) 210;
    numArray12[44] = (byte) 121;
    numArray12[29] = (byte) 71;
    numArray12[30] = (byte) 136;
    numArray12[31 /*0x1F*/] = (byte) 188;
    numArray12[3] = (byte) 188;
    numArray12[40] = (byte) 223;
    numArray12[46] = (byte) 240 /*0xF0*/;
    numArray12[39] = (byte) 1;
    numArray12[26] = (byte) 28;
    numArray12[12] = (byte) 1;
    numArray12[37] = (byte) 156;
    numArray12[19] = (byte) 96 /*0x60*/;
    numArray12[25] = (byte) 208 /*0xD0*/;
    numArray12[41] = (byte) 78;
    numArray12[15] = (byte) 3;
    numArray12[43] = (byte) 9;
    numArray12[5] = (byte) 212;
    numArray12[47] = (byte) 126;
    numArray12[33] = (byte) 243;
    numArray12[16 /*0x10*/] = (byte) 173;
    numArray12[49] = (byte) 214;
    numArray12[1] = (byte) 120;
    numArray12[32 /*0x20*/] = (byte) 98;
    numArray12[20] = (byte) 118;
    numArray12[21] = (byte) 172;
    numArray12[53] = (byte) 162;
    numArray12[54] = (byte) 124;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[27];
    numArray13[19] = (byte) 187;
    numArray13[1] = (byte) 211;
    numArray13[25] = (byte) 97;
    numArray13[3] = (byte) 92;
    numArray13[4] = (byte) 191;
    numArray13[5] = (byte) 5;
    numArray13[11] = (byte) 227;
    numArray13[9] = (byte) 181;
    numArray13[12] = (byte) 236;
    numArray13[26] = (byte) 141;
    numArray13[10] = (byte) 170;
    numArray13[24] = (byte) 107;
    numArray13[17] = (byte) 158;
    numArray13[13] = (byte) 47;
    numArray13[6] = (byte) 234;
    numArray13[0] = (byte) 42;
    numArray13[16 /*0x10*/] = (byte) 90;
    numArray13[8] = (byte) 131;
    numArray13[18] = (byte) 113;
    numArray13[22] = (byte) 104;
    numArray13[20] = (byte) 212;
    numArray13[21] = (byte) 165;
    numArray13[15] = (byte) 164;
    numArray13[23] = (byte) 128 /*0x80*/;
    numArray13[14] = (byte) 46;
    numArray13[7] = (byte) 74;
    numArray13[2] = (byte) 16 /*0x10*/;
    byte[] numArray14 = new byte[27];
    numArray14[20] = (byte) 26;
    numArray14[1] = (byte) 162;
    numArray14[2] = (byte) 216;
    numArray14[9] = (byte) 242;
    numArray14[0] = (byte) 164;
    numArray14[5] = (byte) 188;
    numArray14[19] = (byte) 134;
    numArray14[7] = (byte) 89;
    numArray14[17] = (byte) 210;
    numArray14[10] = (byte) 118;
    numArray14[11] = (byte) 239;
    numArray14[6] = (byte) 117;
    numArray14[15] = (byte) 21;
    numArray14[8] = (byte) 235;
    numArray14[4] = (byte) 67;
    numArray14[3] = (byte) 184;
    numArray14[16 /*0x10*/] = (byte) 187;
    numArray14[13] = (byte) 13;
    numArray14[18] = (byte) 132;
    numArray14[12] = (byte) 253;
    numArray14[14] = (byte) 177;
    numArray14[21] = (byte) 224 /*0xE0*/;
    numArray14[22] = (byte) 177;
    numArray14[23] = (byte) 34;
    numArray14[24] = (byte) 207;
    numArray14[25] = (byte) 227;
    numArray14[26] = (byte) 161;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 27);
    for (int index = 0; index < 27; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13499()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[144 /*0x90*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 65,
        (byte) 15,
        (byte) 31 /*0x1F*/,
        (byte) 70,
        (byte) 42,
        (byte) 246,
        byte.MaxValue,
        (byte) 167,
        (byte) 200,
        (byte) 32 /*0x20*/,
        (byte) 212,
        (byte) 182,
        (byte) 37,
        (byte) 0,
        (byte) 209,
        (byte) 48 /*0x30*/,
        (byte) 36,
        (byte) 69,
        (byte) 206,
        (byte) 42,
        (byte) 97,
        (byte) 72,
        (byte) 101,
        (byte) 234,
        (byte) 223,
        (byte) 121,
        (byte) 182,
        (byte) 62,
        (byte) 224 /*0xE0*/,
        (byte) 88,
        (byte) 176 /*0xB0*/,
        (byte) 36,
        (byte) 20,
        (byte) 136,
        (byte) 114,
        (byte) 76,
        (byte) 12,
        (byte) 240 /*0xF0*/,
        (byte) 86,
        (byte) 214,
        (byte) 21,
        (byte) 26,
        (byte) 176 /*0xB0*/,
        (byte) 184,
        (byte) 215,
        (byte) 216,
        (byte) 230,
        (byte) 179,
        (byte) 127 /*0x7F*/,
        (byte) 223,
        (byte) 224 /*0xE0*/,
        (byte) 129,
        (byte) 69,
        (byte) 80 /*0x50*/,
        (byte) 138
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 38,
        (byte) 196,
        (byte) 180,
        (byte) 152,
        (byte) 217,
        (byte) 238,
        (byte) 156,
        (byte) 86,
        (byte) 231,
        (byte) 11,
        (byte) 64 /*0x40*/,
        (byte) 124,
        (byte) 138,
        (byte) 201,
        (byte) 125,
        (byte) 181,
        (byte) 57,
        (byte) 229,
        (byte) 66,
        (byte) 241,
        (byte) 204,
        (byte) 110,
        (byte) 62,
        (byte) 76,
        (byte) 108,
        (byte) 103,
        (byte) 248,
        (byte) 144 /*0x90*/,
        (byte) 215,
        (byte) 223,
        (byte) 125,
        byte.MaxValue,
        (byte) 17,
        (byte) 172,
        (byte) 219,
        (byte) 137,
        (byte) 33,
        (byte) 225,
        (byte) 64 /*0x40*/,
        (byte) 7,
        (byte) 7,
        (byte) 150,
        (byte) 124,
        (byte) 103,
        (byte) 247,
        (byte) 209,
        (byte) 45,
        (byte) 8,
        (byte) 88,
        (byte) 85,
        (byte) 168,
        (byte) 119,
        (byte) 97,
        (byte) 186,
        (byte) 54
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[29] = (byte) 222;
      numArray4[13] = (byte) 36;
      numArray4[2] = (byte) 176 /*0xB0*/;
      numArray4[23] = (byte) 146;
      numArray4[4] = (byte) 192 /*0xC0*/;
      numArray4[5] = (byte) 198;
      numArray4[16 /*0x10*/] = (byte) 248;
      numArray4[7] = (byte) 112 /*0x70*/;
      numArray4[3] = (byte) 64 /*0x40*/;
      numArray4[34] = (byte) 215;
      numArray4[10] = (byte) 156;
      numArray4[6] = (byte) 111;
      numArray4[12] = (byte) 133;
      numArray4[24] = (byte) 3;
      numArray4[14] = (byte) 52;
      numArray4[33] = (byte) 161;
      numArray4[1] = (byte) 102;
      numArray4[25] = (byte) 190;
      numArray4[38] = (byte) 84;
      numArray4[37] = (byte) 149;
      numArray4[15] = (byte) 239;
      numArray4[35] = (byte) 137;
      numArray4[22] = (byte) 6;
      numArray4[8] = (byte) 176 /*0xB0*/;
      numArray4[52] = (byte) 67;
      numArray4[44] = (byte) 73;
      numArray4[28] = (byte) 46;
      numArray4[27] = (byte) 119;
      numArray4[49] = (byte) 146;
      numArray4[51] = (byte) 58;
      numArray4[30] = (byte) 196;
      numArray4[21] = (byte) 18;
      numArray4[32 /*0x20*/] = (byte) 120;
      numArray4[19] = (byte) 152;
      numArray4[18] = (byte) 70;
      numArray4[17] = (byte) 21;
      numArray4[36] = (byte) 165;
      numArray4[46] = (byte) 250;
      numArray4[53] = (byte) 97;
      numArray4[39] = (byte) 84;
      numArray4[40] = (byte) 64 /*0x40*/;
      numArray4[41] = (byte) 71;
      numArray4[42] = (byte) 39;
      numArray4[43] = (byte) 55;
      numArray4[31 /*0x1F*/] = (byte) 254;
      numArray4[45] = (byte) 36;
      numArray4[50] = (byte) 77;
      numArray4[0] = (byte) 52;
      numArray4[48 /*0x30*/] = (byte) 172;
      numArray4[9] = (byte) 161;
      numArray4[47] = (byte) 186;
      numArray4[26] = (byte) 1;
      numArray4[11] = (byte) 142;
      numArray4[20] = (byte) 173;
      numArray4[54] = (byte) 253;
      byte[] numArray5 = new byte[55];
      numArray5[26] = (byte) 22;
      numArray5[20] = (byte) 154;
      numArray5[34] = (byte) 48 /*0x30*/;
      numArray5[6] = (byte) 224 /*0xE0*/;
      numArray5[38] = (byte) 114;
      numArray5[5] = (byte) 165;
      numArray5[8] = (byte) 142;
      numArray5[48 /*0x30*/] = (byte) 91;
      numArray5[54] = (byte) 185;
      numArray5[28] = (byte) 178;
      numArray5[10] = (byte) 14;
      numArray5[4] = (byte) 101;
      numArray5[12] = (byte) 214;
      numArray5[13] = (byte) 136;
      numArray5[14] = (byte) 80 /*0x50*/;
      numArray5[15] = (byte) 26;
      numArray5[16 /*0x10*/] = (byte) 106;
      numArray5[17] = (byte) 92;
      numArray5[19] = (byte) 254;
      numArray5[25] = (byte) 225;
      numArray5[11] = (byte) 243;
      numArray5[7] = (byte) 231;
      numArray5[22] = (byte) 155;
      numArray5[23] = (byte) 135;
      numArray5[24] = byte.MaxValue;
      numArray5[3] = (byte) 14;
      numArray5[0] = (byte) 171;
      numArray5[27] = (byte) 85;
      numArray5[30] = (byte) 240 /*0xF0*/;
      numArray5[29] = (byte) 196;
      numArray5[43] = (byte) 25;
      numArray5[31 /*0x1F*/] = (byte) 148;
      numArray5[47] = (byte) 163;
      numArray5[18] = (byte) 155;
      numArray5[50] = (byte) 54;
      numArray5[1] = (byte) 110;
      numArray5[36] = (byte) 67;
      numArray5[37] = (byte) 89;
      numArray5[2] = (byte) 138;
      numArray5[39] = (byte) 4;
      numArray5[40] = (byte) 99;
      numArray5[41] = (byte) 7;
      numArray5[46] = (byte) 44;
      numArray5[51] = (byte) 18;
      numArray5[32 /*0x20*/] = (byte) 221;
      numArray5[45] = (byte) 177;
      numArray5[44] = (byte) 38;
      numArray5[9] = (byte) 33;
      numArray5[42] = (byte) 214;
      numArray5[21] = (byte) 231;
      numArray5[33] = (byte) 36;
      numArray5[35] = (byte) 143;
      numArray5[52] = (byte) 62;
      numArray5[53] = (byte) 81;
      numArray5[49] = (byte) 234;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[34];
      numArray6[9] = (byte) 241;
      numArray6[1] = (byte) 142;
      numArray6[2] = (byte) 143;
      numArray6[0] = (byte) 54;
      numArray6[16 /*0x10*/] = (byte) 79;
      numArray6[26] = (byte) 202;
      numArray6[4] = (byte) 99;
      numArray6[5] = (byte) 85;
      numArray6[8] = (byte) 123;
      numArray6[24] = (byte) 0;
      numArray6[10] = (byte) 50;
      numArray6[3] = (byte) 164;
      numArray6[30] = (byte) 18;
      numArray6[13] = (byte) 15;
      numArray6[20] = (byte) 105;
      numArray6[21] = (byte) 214;
      numArray6[7] = (byte) 64 /*0x40*/;
      numArray6[17] = (byte) 243;
      numArray6[18] = (byte) 2;
      numArray6[19] = (byte) 126;
      numArray6[11] = (byte) 22;
      numArray6[12] = (byte) 231;
      numArray6[22] = (byte) 191;
      numArray6[23] = (byte) 214;
      numArray6[14] = (byte) 15;
      numArray6[32 /*0x20*/] = (byte) 221;
      numArray6[28] = (byte) 7;
      numArray6[27] = (byte) 197;
      numArray6[29] = (byte) 120;
      numArray6[15] = (byte) 90;
      numArray6[25] = (byte) 71;
      numArray6[31 /*0x1F*/] = (byte) 213;
      numArray6[6] = (byte) 153;
      numArray6[33] = (byte) 123;
      byte[] numArray7 = new byte[34]
      {
        (byte) 144 /*0x90*/,
        (byte) 214,
        (byte) 254,
        (byte) 131,
        (byte) 55,
        (byte) 237,
        (byte) 63 /*0x3F*/,
        (byte) 105,
        (byte) 173,
        (byte) 236,
        (byte) 68,
        (byte) 150,
        (byte) 17,
        (byte) 253,
        (byte) 97,
        (byte) 165,
        (byte) 168,
        (byte) 163,
        (byte) 187,
        (byte) 129,
        (byte) 234,
        (byte) 93,
        (byte) 230,
        (byte) 113,
        (byte) 114,
        (byte) 134,
        (byte) 121,
        (byte) 19,
        (byte) 26,
        (byte) 191,
        (byte) 141,
        (byte) 50,
        (byte) 8,
        (byte) 132
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[144 /*0x90*/];
    byte[] numArray9 = new byte[55];
    numArray9[47] = (byte) 41;
    numArray9[17] = (byte) 199;
    numArray9[23] = (byte) 126;
    numArray9[3] = (byte) 2;
    numArray9[4] = (byte) 160 /*0xA0*/;
    numArray9[38] = (byte) 19;
    numArray9[22] = (byte) 156;
    numArray9[8] = (byte) 62;
    numArray9[2] = (byte) 250;
    numArray9[30] = (byte) 45;
    numArray9[42] = (byte) 102;
    numArray9[11] = (byte) 244;
    numArray9[12] = (byte) 88;
    numArray9[13] = (byte) 216;
    numArray9[28] = (byte) 73;
    numArray9[15] = (byte) 161;
    numArray9[26] = (byte) 74;
    numArray9[0] = (byte) 226;
    numArray9[18] = (byte) 27;
    numArray9[10] = (byte) 82;
    numArray9[9] = (byte) 174;
    numArray9[52] = (byte) 120;
    numArray9[31 /*0x1F*/] = (byte) 228;
    numArray9[29] = (byte) 137;
    numArray9[36] = (byte) 58;
    numArray9[1] = (byte) 59;
    numArray9[19] = (byte) 156;
    numArray9[27] = (byte) 64 /*0x40*/;
    numArray9[50] = (byte) 125;
    numArray9[53] = (byte) 7;
    numArray9[6] = (byte) 161;
    numArray9[16 /*0x10*/] = (byte) 187;
    numArray9[24] = (byte) 167;
    numArray9[33] = (byte) 218;
    numArray9[34] = (byte) 189;
    numArray9[35] = (byte) 203;
    numArray9[48 /*0x30*/] = (byte) 210;
    numArray9[37] = (byte) 32 /*0x20*/;
    numArray9[43] = (byte) 109;
    numArray9[25] = (byte) 144 /*0x90*/;
    numArray9[40] = (byte) 45;
    numArray9[41] = (byte) 136;
    numArray9[21] = (byte) 200;
    numArray9[54] = (byte) 9;
    numArray9[44] = (byte) 55;
    numArray9[14] = (byte) 11;
    numArray9[20] = (byte) 203;
    numArray9[7] = (byte) 48 /*0x30*/;
    numArray9[39] = (byte) 215;
    numArray9[49] = (byte) 186;
    numArray9[45] = (byte) 224 /*0xE0*/;
    numArray9[51] = (byte) 164;
    numArray9[5] = (byte) 137;
    numArray9[32 /*0x20*/] = (byte) 44;
    numArray9[46] = (byte) 224 /*0xE0*/;
    byte[] numArray10 = new byte[55];
    numArray10[3] = (byte) 30;
    numArray10[1] = (byte) 155;
    numArray10[21] = (byte) 11;
    numArray10[47] = (byte) 142;
    numArray10[7] = (byte) 165;
    numArray10[6] = (byte) 157;
    numArray10[51] = (byte) 189;
    numArray10[46] = (byte) 213;
    numArray10[15] = (byte) 176 /*0xB0*/;
    numArray10[9] = (byte) 77;
    numArray10[10] = (byte) 206;
    numArray10[11] = (byte) 133;
    numArray10[12] = (byte) 22;
    numArray10[23] = (byte) 114;
    numArray10[52] = (byte) 186;
    numArray10[13] = (byte) 232;
    numArray10[17] = (byte) 82;
    numArray10[34] = (byte) 1;
    numArray10[24] = (byte) 137;
    numArray10[19] = (byte) 98;
    numArray10[38] = (byte) 125;
    numArray10[29] = (byte) 87;
    numArray10[22] = (byte) 175;
    numArray10[18] = (byte) 34;
    numArray10[25] = (byte) 183;
    numArray10[16 /*0x10*/] = (byte) 211;
    numArray10[36] = (byte) 188;
    numArray10[39] = (byte) 184;
    numArray10[28] = (byte) 106;
    numArray10[54] = (byte) 226;
    numArray10[30] = (byte) 8;
    numArray10[48 /*0x30*/] = (byte) 151;
    numArray10[14] = (byte) 70;
    numArray10[45] = (byte) 29;
    numArray10[41] = (byte) 59;
    numArray10[27] = (byte) 97;
    numArray10[42] = (byte) 30;
    numArray10[37] = (byte) 169;
    numArray10[20] = (byte) 125;
    numArray10[33] = (byte) 59;
    numArray10[40] = (byte) 235;
    numArray10[2] = (byte) 117;
    numArray10[0] = (byte) 61;
    numArray10[43] = (byte) 15;
    numArray10[44] = (byte) 51;
    numArray10[4] = (byte) 210;
    numArray10[31 /*0x1F*/] = (byte) 205;
    numArray10[8] = (byte) 155;
    numArray10[32 /*0x20*/] = (byte) 176 /*0xB0*/;
    numArray10[49] = (byte) 15;
    numArray10[50] = (byte) 162;
    numArray10[35] = (byte) 136;
    numArray10[5] = (byte) 247;
    numArray10[53] = (byte) 80 /*0x50*/;
    numArray10[26] = (byte) 69;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 183,
      (byte) 227,
      (byte) 18,
      (byte) 12,
      (byte) 24,
      (byte) 29,
      (byte) 129,
      (byte) 93,
      (byte) 202,
      (byte) 20,
      (byte) 162,
      (byte) 139,
      (byte) 87,
      (byte) 31 /*0x1F*/,
      (byte) 160 /*0xA0*/,
      (byte) 11,
      (byte) 49,
      (byte) 200,
      (byte) 90,
      (byte) 168,
      (byte) 74,
      (byte) 180,
      (byte) 55,
      (byte) 151,
      (byte) 122,
      (byte) 89,
      (byte) 145,
      (byte) 25,
      (byte) 89,
      (byte) 173,
      (byte) 151,
      (byte) 230,
      (byte) 228,
      (byte) 139,
      (byte) 209,
      (byte) 79,
      (byte) 146,
      (byte) 109,
      (byte) 193,
      (byte) 41,
      (byte) 134,
      (byte) 152,
      (byte) 254,
      (byte) 98,
      (byte) 19,
      (byte) 12,
      (byte) 40,
      (byte) 138,
      (byte) 110,
      (byte) 52,
      (byte) 88,
      (byte) 45,
      (byte) 201,
      (byte) 94,
      (byte) 229
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 53,
      (byte) 150,
      (byte) 251,
      (byte) 91,
      (byte) 221,
      (byte) 195,
      (byte) 86,
      (byte) 219,
      (byte) 136,
      (byte) 130,
      (byte) 122,
      (byte) 233,
      (byte) 229,
      (byte) 97,
      (byte) 251,
      (byte) 139,
      (byte) 125,
      (byte) 114,
      (byte) 22,
      (byte) 154,
      (byte) 113,
      (byte) 108,
      (byte) 24,
      (byte) 177,
      (byte) 104,
      (byte) 237,
      (byte) 43,
      (byte) 106,
      (byte) 148,
      (byte) 187,
      (byte) 233,
      (byte) 51,
      (byte) 43,
      (byte) 54,
      (byte) 107,
      (byte) 25,
      (byte) 217,
      (byte) 162,
      (byte) 16 /*0x10*/,
      (byte) 239,
      (byte) 14,
      (byte) 189,
      (byte) 133,
      (byte) 187,
      (byte) 140,
      (byte) 248,
      (byte) 151,
      (byte) 59,
      (byte) 106,
      (byte) 94,
      (byte) 72,
      (byte) 251,
      (byte) 134,
      (byte) 10,
      (byte) 231
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[34];
    numArray13[19] = (byte) 182;
    numArray13[32 /*0x20*/] = (byte) 206;
    numArray13[2] = (byte) 191;
    numArray13[8] = (byte) 188;
    numArray13[4] = (byte) 103;
    numArray13[28] = (byte) 105;
    numArray13[31 /*0x1F*/] = (byte) 166;
    numArray13[7] = (byte) 125;
    numArray13[24] = (byte) 6;
    numArray13[9] = (byte) 167;
    numArray13[33] = (byte) 163;
    numArray13[5] = (byte) 4;
    numArray13[16 /*0x10*/] = (byte) 49;
    numArray13[0] = (byte) 122;
    numArray13[14] = (byte) 195;
    numArray13[26] = (byte) 36;
    numArray13[15] = (byte) 7;
    numArray13[21] = (byte) 132;
    numArray13[6] = (byte) 28;
    numArray13[17] = (byte) 34;
    numArray13[20] = (byte) 213;
    numArray13[22] = (byte) 110;
    numArray13[11] = (byte) 77;
    numArray13[23] = (byte) 49;
    numArray13[27] = (byte) 84;
    numArray13[25] = (byte) 170;
    numArray13[1] = (byte) 146;
    numArray13[10] = (byte) 204;
    numArray13[12] = (byte) 80 /*0x50*/;
    numArray13[29] = (byte) 29;
    numArray13[30] = (byte) 133;
    numArray13[3] = (byte) 140;
    numArray13[18] = (byte) 177;
    numArray13[13] = (byte) 121;
    byte[] numArray14 = new byte[34];
    numArray14[8] = (byte) 211;
    numArray14[9] = (byte) 89;
    numArray14[16 /*0x10*/] = (byte) 13;
    numArray14[3] = (byte) 75;
    numArray14[4] = (byte) 219;
    numArray14[2] = (byte) 0;
    numArray14[18] = (byte) 6;
    numArray14[7] = (byte) 42;
    numArray14[1] = (byte) 24;
    numArray14[23] = (byte) 131;
    numArray14[10] = (byte) 39;
    numArray14[0] = (byte) 228;
    numArray14[24] = (byte) 34;
    numArray14[12] = (byte) 212;
    numArray14[33] = (byte) 211;
    numArray14[15] = (byte) 10;
    numArray14[20] = (byte) 93;
    numArray14[11] = (byte) 225;
    numArray14[6] = (byte) 197;
    numArray14[19] = (byte) 212;
    numArray14[14] = (byte) 136;
    numArray14[21] = (byte) 43;
    numArray14[22] = (byte) 35;
    numArray14[31 /*0x1F*/] = (byte) 147;
    numArray14[28] = (byte) 176 /*0xB0*/;
    numArray14[25] = (byte) 221;
    numArray14[26] = (byte) 195;
    numArray14[27] = (byte) 30;
    numArray14[5] = (byte) 81;
    numArray14[29] = (byte) 220;
    numArray14[30] = (byte) 227;
    numArray14[17] = (byte) 245;
    numArray14[32 /*0x20*/] = (byte) 82;
    numArray14[13] = (byte) 118;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 34);
    for (int index = 0; index < 34; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13500()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[140];
      byte[] numArray2 = new byte[55]
      {
        (byte) 56,
        (byte) 23,
        (byte) 119,
        (byte) 231,
        (byte) 201,
        (byte) 27,
        (byte) 185,
        (byte) 189,
        (byte) 47,
        (byte) 118,
        (byte) 7,
        (byte) 207,
        (byte) 25,
        (byte) 215,
        (byte) 180,
        (byte) 252,
        (byte) 225,
        (byte) 63 /*0x3F*/,
        (byte) 50,
        (byte) 145,
        (byte) 124,
        (byte) 161,
        (byte) 103,
        (byte) 186,
        (byte) 29,
        (byte) 116,
        (byte) 198,
        (byte) 158,
        (byte) 9,
        (byte) 29,
        (byte) 15,
        (byte) 19,
        (byte) 34,
        (byte) 128 /*0x80*/,
        (byte) 17,
        (byte) 91,
        (byte) 10,
        (byte) 228,
        (byte) 88,
        (byte) 155,
        (byte) 186,
        (byte) 89,
        (byte) 50,
        (byte) 63 /*0x3F*/,
        (byte) 18,
        (byte) 2,
        (byte) 158,
        (byte) 107,
        (byte) 130,
        (byte) 195,
        (byte) 142,
        (byte) 26,
        (byte) 121,
        (byte) 143,
        (byte) 136
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 231,
        (byte) 123,
        (byte) 83,
        (byte) 183,
        (byte) 125,
        (byte) 3,
        (byte) 147,
        (byte) 168,
        (byte) 189,
        (byte) 129,
        (byte) 200,
        (byte) 44,
        (byte) 73,
        (byte) 90,
        (byte) 46,
        byte.MaxValue,
        (byte) 224 /*0xE0*/,
        (byte) 47,
        (byte) 33,
        (byte) 100,
        (byte) 20,
        (byte) 188,
        (byte) 78,
        (byte) 243,
        (byte) 151,
        (byte) 9,
        (byte) 11,
        (byte) 109,
        (byte) 206,
        (byte) 238,
        (byte) 228,
        (byte) 166,
        (byte) 235,
        (byte) 3,
        (byte) 20,
        (byte) 152,
        (byte) 112 /*0x70*/,
        (byte) 139,
        (byte) 84,
        (byte) 213,
        (byte) 241,
        (byte) 77,
        (byte) 151,
        (byte) 187,
        (byte) 251,
        (byte) 163,
        (byte) 78,
        (byte) 123,
        (byte) 241,
        (byte) 212,
        (byte) 138,
        (byte) 98,
        (byte) 105,
        (byte) 18,
        (byte) 192 /*0xC0*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 189,
        (byte) 201,
        (byte) 84,
        (byte) 84,
        (byte) 27,
        (byte) 180,
        (byte) 18,
        (byte) 102,
        (byte) 243,
        (byte) 173,
        (byte) 153,
        (byte) 198,
        (byte) 89,
        (byte) 241,
        (byte) 253,
        (byte) 230,
        (byte) 151,
        (byte) 214,
        (byte) 34,
        (byte) 110,
        (byte) 40,
        (byte) 167,
        (byte) 27,
        (byte) 213,
        (byte) 91,
        (byte) 28,
        (byte) 69,
        (byte) 143,
        (byte) 244,
        (byte) 154,
        (byte) 146,
        (byte) 248,
        (byte) 122,
        (byte) 171,
        (byte) 77,
        (byte) 26,
        (byte) 3,
        (byte) 70,
        (byte) 159,
        (byte) 165,
        (byte) 32 /*0x20*/,
        (byte) 216,
        (byte) 31 /*0x1F*/,
        (byte) 84,
        (byte) 5,
        (byte) 164,
        (byte) 41,
        (byte) 209,
        (byte) 144 /*0x90*/,
        (byte) 51,
        (byte) 225,
        (byte) 185,
        (byte) 101,
        (byte) 40,
        (byte) 135
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 203,
        (byte) 147,
        (byte) 13,
        (byte) 103,
        (byte) 33,
        (byte) 169,
        (byte) 153,
        (byte) 116,
        (byte) 38,
        (byte) 183,
        (byte) 183,
        (byte) 201,
        (byte) 105,
        (byte) 190,
        (byte) 4,
        (byte) 28,
        (byte) 138,
        (byte) 102,
        (byte) 105,
        (byte) 197,
        (byte) 252,
        (byte) 135,
        byte.MaxValue,
        (byte) 139,
        (byte) 172,
        (byte) 33,
        (byte) 121,
        (byte) 162,
        (byte) 252,
        (byte) 130,
        (byte) 45,
        (byte) 98,
        (byte) 115,
        (byte) 90,
        (byte) 160 /*0xA0*/,
        (byte) 170,
        (byte) 88,
        (byte) 84,
        (byte) 165,
        (byte) 250,
        (byte) 29,
        (byte) 74,
        (byte) 83,
        (byte) 36,
        (byte) 84,
        (byte) 111,
        (byte) 117,
        (byte) 77,
        (byte) 92,
        (byte) 72,
        (byte) 97,
        (byte) 126,
        (byte) 110,
        (byte) 105,
        (byte) 13
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[30];
      numArray6[26] = (byte) 192 /*0xC0*/;
      numArray6[1] = (byte) 249;
      numArray6[18] = (byte) 80 /*0x50*/;
      numArray6[25] = (byte) 254;
      numArray6[4] = (byte) 130;
      numArray6[27] = (byte) 185;
      numArray6[6] = (byte) 88;
      numArray6[0] = (byte) 126;
      numArray6[8] = (byte) 161;
      numArray6[9] = (byte) 76;
      numArray6[10] = (byte) 173;
      numArray6[19] = (byte) 136;
      numArray6[12] = (byte) 227;
      numArray6[13] = (byte) 227;
      numArray6[14] = (byte) 81;
      numArray6[5] = (byte) 114;
      numArray6[16 /*0x10*/] = (byte) 125;
      numArray6[17] = (byte) 78;
      numArray6[21] = (byte) 208 /*0xD0*/;
      numArray6[2] = byte.MaxValue;
      numArray6[29] = (byte) 146;
      numArray6[7] = (byte) 26;
      numArray6[22] = (byte) 182;
      numArray6[23] = (byte) 242;
      numArray6[11] = (byte) 164;
      numArray6[15] = (byte) 54;
      numArray6[3] = (byte) 110;
      numArray6[24] = (byte) 116;
      numArray6[28] = (byte) 216;
      numArray6[20] = (byte) 198;
      byte[] numArray7 = new byte[30]
      {
        (byte) 189,
        (byte) 183,
        (byte) 151,
        (byte) 95,
        (byte) 249,
        (byte) 241,
        (byte) 72,
        (byte) 113,
        (byte) 100,
        (byte) 225,
        (byte) 68,
        (byte) 89,
        (byte) 56,
        (byte) 10,
        (byte) 194,
        (byte) 102,
        (byte) 227,
        (byte) 185,
        (byte) 82,
        (byte) 52,
        (byte) 221,
        (byte) 34,
        (byte) 215,
        (byte) 228,
        (byte) 243,
        (byte) 210,
        (byte) 52,
        (byte) 44,
        (byte) 44,
        (byte) 20
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
      (byte) 14,
      (byte) 250,
      (byte) 137,
      (byte) 108,
      (byte) 119,
      (byte) 94,
      (byte) 4,
      (byte) 150,
      (byte) 70,
      (byte) 176 /*0xB0*/,
      (byte) 234,
      (byte) 194,
      (byte) 141,
      (byte) 109,
      (byte) 94,
      (byte) 119,
      (byte) 71,
      (byte) 81,
      (byte) 112 /*0x70*/,
      (byte) 41,
      (byte) 86,
      (byte) 170,
      (byte) 120,
      (byte) 237,
      (byte) 171,
      (byte) 132,
      (byte) 215,
      (byte) 9,
      (byte) 148,
      (byte) 60,
      (byte) 26,
      (byte) 180,
      (byte) 38,
      (byte) 247,
      (byte) 85,
      (byte) 92,
      (byte) 242,
      (byte) 192 /*0xC0*/,
      (byte) 53,
      (byte) 242,
      (byte) 139,
      (byte) 236,
      (byte) 161,
      (byte) 41,
      (byte) 76,
      (byte) 202,
      (byte) 90,
      (byte) 64 /*0x40*/,
      (byte) 227,
      (byte) 148,
      (byte) 179,
      (byte) 50,
      (byte) 65,
      (byte) 118,
      (byte) 236
    };
    byte[] numArray10 = new byte[55];
    numArray10[17] = (byte) 34;
    numArray10[10] = (byte) 127 /*0x7F*/;
    numArray10[2] = (byte) 126;
    numArray10[3] = (byte) 28;
    numArray10[4] = (byte) 78;
    numArray10[37] = (byte) 35;
    numArray10[40] = (byte) 59;
    numArray10[7] = (byte) 117;
    numArray10[1] = (byte) 74;
    numArray10[9] = (byte) 149;
    numArray10[52] = (byte) 65;
    numArray10[11] = (byte) 164;
    numArray10[29] = (byte) 202;
    numArray10[45] = (byte) 19;
    numArray10[14] = (byte) 83;
    numArray10[15] = (byte) 105;
    numArray10[20] = (byte) 117;
    numArray10[51] = (byte) 55;
    numArray10[36] = (byte) 134;
    numArray10[19] = (byte) 30;
    numArray10[38] = (byte) 189;
    numArray10[13] = (byte) 65;
    numArray10[48 /*0x30*/] = (byte) 98;
    numArray10[23] = (byte) 104;
    numArray10[43] = (byte) 166;
    numArray10[25] = (byte) 8;
    numArray10[26] = (byte) 111;
    numArray10[47] = (byte) 79;
    numArray10[21] = (byte) 84;
    numArray10[54] = (byte) 45;
    numArray10[0] = (byte) 238;
    numArray10[44] = byte.MaxValue;
    numArray10[32 /*0x20*/] = (byte) 116;
    numArray10[12] = (byte) 169;
    numArray10[34] = (byte) 155;
    numArray10[53] = (byte) 106;
    numArray10[22] = (byte) 66;
    numArray10[28] = (byte) 210;
    numArray10[6] = (byte) 63 /*0x3F*/;
    numArray10[39] = (byte) 246;
    numArray10[5] = (byte) 119;
    numArray10[41] = (byte) 103;
    numArray10[42] = (byte) 123;
    numArray10[33] = (byte) 21;
    numArray10[8] = (byte) 223;
    numArray10[24] = (byte) 78;
    numArray10[46] = (byte) 198;
    numArray10[27] = (byte) 158;
    numArray10[31 /*0x1F*/] = (byte) 243;
    numArray10[35] = (byte) 36;
    numArray10[50] = (byte) 108;
    numArray10[30] = (byte) 147;
    numArray10[16 /*0x10*/] = (byte) 45;
    numArray10[18] = (byte) 82;
    numArray10[49] = (byte) 152;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 250,
      (byte) 223,
      (byte) 199,
      (byte) 240 /*0xF0*/,
      (byte) 213,
      (byte) 2,
      (byte) 129,
      (byte) 174,
      (byte) 195,
      (byte) 235,
      (byte) 137,
      (byte) 36,
      (byte) 40,
      (byte) 45,
      (byte) 135,
      (byte) 216,
      (byte) 17,
      (byte) 13,
      (byte) 126,
      (byte) 114,
      (byte) 187,
      (byte) 137,
      (byte) 178,
      (byte) 101,
      (byte) 108,
      (byte) 68,
      (byte) 239,
      (byte) 92,
      (byte) 209,
      (byte) 174,
      (byte) 215,
      (byte) 136,
      (byte) 152,
      (byte) 165,
      (byte) 91,
      (byte) 31 /*0x1F*/,
      (byte) 123,
      (byte) 123,
      (byte) 187,
      (byte) 232,
      (byte) 21,
      (byte) 228,
      (byte) 83,
      (byte) 67,
      (byte) 59,
      (byte) 4,
      (byte) 185,
      (byte) 226,
      (byte) 164,
      (byte) 61,
      (byte) 178,
      (byte) 203,
      (byte) 4,
      (byte) 231,
      (byte) 120
    };
    byte[] numArray12 = new byte[55];
    numArray12[30] = (byte) 115;
    numArray12[40] = (byte) 215;
    numArray12[2] = (byte) 87;
    numArray12[3] = (byte) 227;
    numArray12[8] = (byte) 237;
    numArray12[19] = (byte) 117;
    numArray12[26] = (byte) 235;
    numArray12[16 /*0x10*/] = (byte) 104;
    numArray12[12] = (byte) 223;
    numArray12[9] = (byte) 196;
    numArray12[48 /*0x30*/] = (byte) 84;
    numArray12[21] = (byte) 154;
    numArray12[1] = (byte) 156;
    numArray12[13] = (byte) 149;
    numArray12[14] = (byte) 208 /*0xD0*/;
    numArray12[33] = (byte) 215;
    numArray12[24] = (byte) 7;
    numArray12[6] = (byte) 40;
    numArray12[18] = (byte) 238;
    numArray12[32 /*0x20*/] = (byte) 18;
    numArray12[25] = (byte) 59;
    numArray12[11] = (byte) 253;
    numArray12[28] = (byte) 34;
    numArray12[22] = (byte) 124;
    numArray12[7] = (byte) 56;
    numArray12[41] = (byte) 20;
    numArray12[44] = (byte) 229;
    numArray12[37] = (byte) 50;
    numArray12[5] = (byte) 27;
    numArray12[29] = (byte) 192 /*0xC0*/;
    numArray12[4] = (byte) 31 /*0x1F*/;
    numArray12[31 /*0x1F*/] = (byte) 214;
    numArray12[35] = (byte) 36;
    numArray12[36] = (byte) 49;
    numArray12[34] = (byte) 12;
    numArray12[23] = (byte) 132;
    numArray12[27] = (byte) 210;
    numArray12[17] = (byte) 68;
    numArray12[38] = (byte) 82;
    numArray12[39] = (byte) 120;
    numArray12[53] = (byte) 208 /*0xD0*/;
    numArray12[51] = (byte) 181;
    numArray12[42] = (byte) 190;
    numArray12[43] = (byte) 107;
    numArray12[20] = byte.MaxValue;
    numArray12[45] = (byte) 180;
    numArray12[46] = (byte) 204;
    numArray12[47] = (byte) 78;
    numArray12[10] = (byte) 153;
    numArray12[49] = (byte) 4;
    numArray12[50] = (byte) 93;
    numArray12[15] = (byte) 167;
    numArray12[52] = (byte) 219;
    numArray12[0] = (byte) 51;
    numArray12[54] = (byte) 61;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[30]
    {
      (byte) 55,
      (byte) 27,
      (byte) 6,
      (byte) 5,
      (byte) 108,
      (byte) 71,
      (byte) 25,
      (byte) 6,
      (byte) 124,
      (byte) 219,
      (byte) 85,
      (byte) 231,
      (byte) 117,
      (byte) 68,
      (byte) 159,
      (byte) 39,
      (byte) 190,
      (byte) 108,
      (byte) 206,
      (byte) 248,
      (byte) 63 /*0x3F*/,
      (byte) 240 /*0xF0*/,
      (byte) 240 /*0xF0*/,
      (byte) 83,
      (byte) 253,
      (byte) 124,
      (byte) 181,
      (byte) 223,
      (byte) 147,
      (byte) 20
    };
    byte[] numArray14 = new byte[30]
    {
      (byte) 214,
      (byte) 155,
      (byte) 209,
      (byte) 220,
      (byte) 74,
      (byte) 243,
      (byte) 141,
      (byte) 241,
      (byte) 191,
      (byte) 165,
      (byte) 38,
      (byte) 214,
      (byte) 148,
      (byte) 57,
      (byte) 84,
      (byte) 65,
      (byte) 206,
      (byte) 159,
      (byte) 10,
      (byte) 137,
      (byte) 195,
      (byte) 49,
      (byte) 252,
      (byte) 195,
      (byte) 93,
      (byte) 134,
      (byte) 108,
      (byte) 151,
      (byte) 248,
      (byte) 129
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 30);
    for (int index = 0; index < 30; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[16 /*0x10*/];
    byte[] response = new byte[16 /*0x10*/];
    Array.Copy((Array) sc_13496.sspq, 38, (Array) numArray15, 0, 16 /*0x10*/);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_13496.sspr, 38, (Array) numArray15, 0, 16 /*0x10*/);
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

  internal static string ssp_appserver_13501()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[144 /*0x90*/];
      byte[] numArray2 = new byte[55]
      {
        (byte) 29,
        (byte) 212,
        (byte) 66,
        (byte) 126,
        (byte) 70,
        (byte) 43,
        (byte) 179,
        (byte) 8,
        (byte) 239,
        (byte) 146,
        (byte) 57,
        (byte) 114,
        (byte) 103,
        (byte) 181,
        (byte) 203,
        (byte) 55,
        (byte) 64 /*0x40*/,
        (byte) 98,
        (byte) 195,
        (byte) 4,
        (byte) 238,
        (byte) 232,
        (byte) 13,
        (byte) 211,
        (byte) 21,
        (byte) 50,
        (byte) 83,
        (byte) 113,
        (byte) 212,
        (byte) 182,
        (byte) 26,
        (byte) 99,
        (byte) 66,
        (byte) 100,
        (byte) 8,
        (byte) 182,
        (byte) 109,
        (byte) 182,
        (byte) 155,
        (byte) 176 /*0xB0*/,
        (byte) 111,
        (byte) 200,
        (byte) 148,
        (byte) 46,
        (byte) 235,
        (byte) 182,
        (byte) 86,
        (byte) 231,
        (byte) 22,
        (byte) 240 /*0xF0*/,
        (byte) 133,
        (byte) 56,
        (byte) 185,
        (byte) 76,
        (byte) 194
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 195,
        (byte) 253,
        (byte) 138,
        (byte) 8,
        (byte) 50,
        (byte) 174,
        (byte) 201,
        (byte) 106,
        (byte) 145,
        (byte) 82,
        (byte) 167,
        (byte) 185,
        (byte) 126,
        (byte) 190,
        (byte) 253,
        (byte) 180,
        (byte) 91,
        (byte) 234,
        (byte) 192 /*0xC0*/,
        (byte) 225,
        (byte) 224 /*0xE0*/,
        (byte) 141,
        (byte) 201,
        (byte) 27,
        (byte) 164,
        (byte) 124,
        (byte) 247,
        (byte) 29,
        (byte) 67,
        (byte) 87,
        (byte) 196,
        (byte) 92,
        (byte) 1,
        (byte) 75,
        (byte) 136,
        (byte) 241,
        (byte) 38,
        (byte) 253,
        (byte) 91,
        (byte) 234,
        (byte) 140,
        (byte) 123,
        (byte) 123,
        (byte) 128 /*0x80*/,
        (byte) 136,
        (byte) 106,
        (byte) 224 /*0xE0*/,
        (byte) 246,
        (byte) 220,
        (byte) 166,
        (byte) 225,
        (byte) 49,
        (byte) 49,
        (byte) 64 /*0x40*/,
        (byte) 136
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 111,
        (byte) 104,
        (byte) 20,
        (byte) 215,
        (byte) 66,
        (byte) 51,
        (byte) 57,
        (byte) 119,
        (byte) 213,
        (byte) 48 /*0x30*/,
        (byte) 3,
        (byte) 172,
        (byte) 21,
        (byte) 202,
        (byte) 137,
        (byte) 198,
        (byte) 18,
        (byte) 148,
        (byte) 38,
        (byte) 84,
        (byte) 222,
        (byte) 231,
        (byte) 95,
        (byte) 219,
        (byte) 107,
        (byte) 90,
        (byte) 227,
        (byte) 49,
        (byte) 225,
        (byte) 169,
        (byte) 246,
        (byte) 162,
        (byte) 211,
        (byte) 239,
        (byte) 254,
        (byte) 78,
        (byte) 134,
        (byte) 216,
        (byte) 45,
        (byte) 148,
        (byte) 90,
        (byte) 182,
        (byte) 21,
        (byte) 134,
        (byte) 82,
        (byte) 59,
        (byte) 1,
        (byte) 38,
        (byte) 251,
        (byte) 114,
        (byte) 166,
        (byte) 184,
        (byte) 155,
        (byte) 224 /*0xE0*/,
        (byte) 232
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 242,
        (byte) 165,
        (byte) 76,
        (byte) 124,
        (byte) 91,
        (byte) 45,
        (byte) 201,
        (byte) 158,
        (byte) 202,
        (byte) 240 /*0xF0*/,
        (byte) 158,
        (byte) 18,
        (byte) 97,
        (byte) 177,
        (byte) 220,
        (byte) 82,
        (byte) 34,
        (byte) 197,
        (byte) 209,
        (byte) 227,
        (byte) 117,
        (byte) 39,
        (byte) 149,
        (byte) 134,
        (byte) 240 /*0xF0*/,
        (byte) 247,
        (byte) 67,
        (byte) 49,
        (byte) 95,
        (byte) 5,
        (byte) 5,
        (byte) 164,
        (byte) 205,
        (byte) 12,
        (byte) 123,
        (byte) 254,
        (byte) 125,
        (byte) 67,
        (byte) 142,
        (byte) 77,
        (byte) 95,
        (byte) 113,
        (byte) 148,
        (byte) 186,
        (byte) 193,
        (byte) 179,
        (byte) 235,
        (byte) 67,
        (byte) 40,
        (byte) 238,
        (byte) 161,
        (byte) 216,
        (byte) 192 /*0xC0*/,
        (byte) 24,
        (byte) 80 /*0x50*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[34]
      {
        (byte) 56,
        (byte) 107,
        (byte) 231,
        (byte) 79,
        (byte) 8,
        (byte) 150,
        (byte) 44,
        (byte) 58,
        (byte) 97,
        (byte) 216,
        (byte) 38,
        (byte) 203,
        (byte) 59,
        (byte) 63 /*0x3F*/,
        (byte) 58,
        (byte) 102,
        (byte) 145,
        (byte) 249,
        (byte) 39,
        (byte) 209,
        (byte) 217,
        (byte) 136,
        (byte) 48 /*0x30*/,
        (byte) 89,
        (byte) 189,
        (byte) 53,
        (byte) 111,
        (byte) 3,
        (byte) 61,
        (byte) 6,
        (byte) 242,
        (byte) 6,
        (byte) 118,
        (byte) 69
      };
      byte[] numArray7 = new byte[34]
      {
        (byte) 173,
        (byte) 58,
        (byte) 167,
        (byte) 227,
        (byte) 251,
        (byte) 209,
        (byte) 6,
        (byte) 228,
        (byte) 122,
        (byte) 83,
        (byte) 48 /*0x30*/,
        (byte) 111,
        (byte) 119,
        (byte) 144 /*0x90*/,
        (byte) 167,
        (byte) 184,
        (byte) 41,
        (byte) 198,
        (byte) 179,
        (byte) 125,
        (byte) 20,
        (byte) 76,
        (byte) 130,
        (byte) 58,
        (byte) 110,
        (byte) 212,
        (byte) 175,
        (byte) 220,
        (byte) 2,
        (byte) 23,
        (byte) 38,
        (byte) 186,
        (byte) 133,
        (byte) 83
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 34);
      for (int index = 0; index < 34; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[144 /*0x90*/];
    byte[] numArray9 = new byte[55];
    numArray9[42] = (byte) 98;
    numArray9[6] = (byte) 98;
    numArray9[19] = (byte) 59;
    numArray9[1] = (byte) 82;
    numArray9[3] = (byte) 233;
    numArray9[29] = (byte) 85;
    numArray9[38] = (byte) 141;
    numArray9[7] = (byte) 219;
    numArray9[9] = (byte) 192 /*0xC0*/;
    numArray9[0] = (byte) 69;
    numArray9[5] = (byte) 241;
    numArray9[18] = (byte) 175;
    numArray9[12] = (byte) 81;
    numArray9[13] = (byte) 10;
    numArray9[14] = (byte) 230;
    numArray9[40] = (byte) 206;
    numArray9[37] = (byte) 231;
    numArray9[17] = (byte) 48 /*0x30*/;
    numArray9[4] = (byte) 76;
    numArray9[15] = (byte) 193;
    numArray9[2] = (byte) 32 /*0x20*/;
    numArray9[20] = (byte) 141;
    numArray9[23] = (byte) 39;
    numArray9[25] = (byte) 252;
    numArray9[24] = (byte) 40;
    numArray9[46] = (byte) 236;
    numArray9[26] = (byte) 90;
    numArray9[27] = (byte) 230;
    numArray9[22] = (byte) 211;
    numArray9[16 /*0x10*/] = (byte) 182;
    numArray9[30] = (byte) 245;
    numArray9[31 /*0x1F*/] = (byte) 241;
    numArray9[32 /*0x20*/] = (byte) 206;
    numArray9[28] = (byte) 31 /*0x1F*/;
    numArray9[34] = (byte) 76;
    numArray9[35] = (byte) 167;
    numArray9[36] = (byte) 7;
    numArray9[39] = (byte) 151;
    numArray9[8] = (byte) 90;
    numArray9[49] = (byte) 68;
    numArray9[45] = (byte) 151;
    numArray9[41] = (byte) 126;
    numArray9[47] = (byte) 252;
    numArray9[21] = (byte) 146;
    numArray9[44] = (byte) 61;
    numArray9[52] = (byte) 158;
    numArray9[33] = (byte) 32 /*0x20*/;
    numArray9[48 /*0x30*/] = (byte) 11;
    numArray9[43] = (byte) 116;
    numArray9[10] = (byte) 50;
    numArray9[50] = (byte) 191;
    numArray9[51] = (byte) 21;
    numArray9[11] = (byte) 107;
    numArray9[53] = (byte) 49;
    numArray9[54] = (byte) 184;
    byte[] numArray10 = new byte[55];
    numArray10[31 /*0x1F*/] = (byte) 36;
    numArray10[1] = (byte) 252;
    numArray10[50] = (byte) 65;
    numArray10[45] = (byte) 2;
    numArray10[4] = (byte) 237;
    numArray10[46] = (byte) 241;
    numArray10[11] = (byte) 166;
    numArray10[15] = (byte) 83;
    numArray10[8] = (byte) 148;
    numArray10[21] = (byte) 110;
    numArray10[53] = (byte) 128 /*0x80*/;
    numArray10[36] = (byte) 25;
    numArray10[12] = (byte) 65;
    numArray10[13] = (byte) 185;
    numArray10[14] = (byte) 136;
    numArray10[3] = (byte) 71;
    numArray10[41] = (byte) 54;
    numArray10[17] = (byte) 222;
    numArray10[18] = (byte) 47;
    numArray10[19] = (byte) 152;
    numArray10[20] = (byte) 118;
    numArray10[0] = (byte) 239;
    numArray10[40] = (byte) 7;
    numArray10[23] = (byte) 48 /*0x30*/;
    numArray10[10] = (byte) 96 /*0x60*/;
    numArray10[52] = (byte) 207;
    numArray10[26] = (byte) 76;
    numArray10[27] = (byte) 205;
    numArray10[28] = (byte) 11;
    numArray10[29] = (byte) 254;
    numArray10[30] = (byte) 87;
    numArray10[38] = (byte) 43;
    numArray10[32 /*0x20*/] = (byte) 7;
    numArray10[34] = (byte) 65;
    numArray10[42] = (byte) 103;
    numArray10[35] = (byte) 91;
    numArray10[43] = (byte) 118;
    numArray10[37] = (byte) 216;
    numArray10[16 /*0x10*/] = (byte) 60;
    numArray10[39] = (byte) 51;
    numArray10[22] = (byte) 213;
    numArray10[24] = (byte) 117;
    numArray10[5] = (byte) 112 /*0x70*/;
    numArray10[2] = (byte) 238;
    numArray10[44] = (byte) 5;
    numArray10[51] = (byte) 37;
    numArray10[7] = (byte) 190;
    numArray10[47] = (byte) 230;
    numArray10[48 /*0x30*/] = (byte) 13;
    numArray10[49] = (byte) 62;
    numArray10[54] = (byte) 194;
    numArray10[6] = (byte) 239;
    numArray10[9] = (byte) 73;
    numArray10[25] = (byte) 10;
    numArray10[33] = (byte) 91;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 33,
      (byte) 152,
      (byte) 236,
      (byte) 24,
      (byte) 148,
      (byte) 144 /*0x90*/,
      (byte) 201,
      (byte) 123,
      (byte) 33,
      (byte) 40,
      (byte) 11,
      (byte) 12,
      (byte) 14,
      (byte) 173,
      (byte) 177,
      (byte) 104,
      (byte) 79,
      (byte) 106,
      (byte) 104,
      (byte) 227,
      (byte) 243,
      (byte) 223,
      (byte) 158,
      (byte) 97,
      (byte) 4,
      (byte) 86,
      (byte) 113,
      (byte) 92,
      (byte) 97,
      (byte) 93,
      (byte) 46,
      (byte) 95,
      (byte) 103,
      (byte) 154,
      (byte) 221,
      (byte) 94,
      (byte) 16 /*0x10*/,
      (byte) 176 /*0xB0*/,
      (byte) 26,
      (byte) 82,
      (byte) 37,
      (byte) 68,
      (byte) 236,
      (byte) 208 /*0xD0*/,
      (byte) 231,
      (byte) 14,
      (byte) 167,
      (byte) 49,
      (byte) 151,
      (byte) 164,
      (byte) 30,
      (byte) 62,
      (byte) 245,
      (byte) 204,
      (byte) 121
    };
    byte[] numArray12 = new byte[55];
    numArray12[11] = (byte) 193;
    numArray12[38] = (byte) 129;
    numArray12[48 /*0x30*/] = (byte) 172;
    numArray12[30] = (byte) 174;
    numArray12[4] = (byte) 198;
    numArray12[5] = (byte) 123;
    numArray12[6] = (byte) 153;
    numArray12[17] = (byte) 7;
    numArray12[49] = (byte) 87;
    numArray12[9] = (byte) 228;
    numArray12[29] = (byte) 167;
    numArray12[13] = (byte) 202;
    numArray12[44] = (byte) 6;
    numArray12[20] = (byte) 6;
    numArray12[28] = (byte) 101;
    numArray12[37] = (byte) 78;
    numArray12[16 /*0x10*/] = (byte) 160 /*0xA0*/;
    numArray12[40] = (byte) 204;
    numArray12[1] = (byte) 211;
    numArray12[46] = (byte) 232;
    numArray12[39] = (byte) 166;
    numArray12[21] = (byte) 82;
    numArray12[22] = (byte) 45;
    numArray12[23] = (byte) 63 /*0x3F*/;
    numArray12[24] = (byte) 204;
    numArray12[25] = (byte) 228;
    numArray12[10] = (byte) 194;
    numArray12[47] = (byte) 202;
    numArray12[27] = (byte) 89;
    numArray12[2] = (byte) 18;
    numArray12[12] = (byte) 120;
    numArray12[31 /*0x1F*/] = (byte) 157;
    numArray12[45] = (byte) 11;
    numArray12[33] = (byte) 21;
    numArray12[34] = (byte) 209;
    numArray12[41] = (byte) 167;
    numArray12[36] = (byte) 165;
    numArray12[42] = (byte) 242;
    numArray12[19] = (byte) 22;
    numArray12[32 /*0x20*/] = (byte) 189;
    numArray12[18] = (byte) 163;
    numArray12[3] = (byte) 33;
    numArray12[35] = (byte) 3;
    numArray12[43] = (byte) 22;
    numArray12[8] = (byte) 192 /*0xC0*/;
    numArray12[0] = (byte) 70;
    numArray12[52] = (byte) 178;
    numArray12[50] = (byte) 118;
    numArray12[14] = (byte) 151;
    numArray12[15] = (byte) 172;
    numArray12[26] = (byte) 132;
    numArray12[51] = (byte) 39;
    numArray12[7] = (byte) 238;
    numArray12[53] = (byte) 98;
    numArray12[54] = (byte) 1;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[34];
    numArray13[31 /*0x1F*/] = (byte) 86;
    numArray13[28] = (byte) 1;
    numArray13[17] = (byte) 5;
    numArray13[27] = (byte) 192 /*0xC0*/;
    numArray13[4] = (byte) 168;
    numArray13[6] = (byte) 132;
    numArray13[12] = (byte) 238;
    numArray13[10] = (byte) 82;
    numArray13[3] = (byte) 222;
    numArray13[9] = (byte) 10;
    numArray13[13] = (byte) 211;
    numArray13[11] = (byte) 51;
    numArray13[1] = (byte) 28;
    numArray13[2] = (byte) 143;
    numArray13[0] = (byte) 17;
    numArray13[24] = (byte) 221;
    numArray13[16 /*0x10*/] = (byte) 225;
    numArray13[18] = (byte) 133;
    numArray13[25] = (byte) 127 /*0x7F*/;
    numArray13[19] = (byte) 200;
    numArray13[20] = (byte) 247;
    numArray13[21] = (byte) 61;
    numArray13[14] = (byte) 2;
    numArray13[23] = (byte) 122;
    numArray13[7] = (byte) 9;
    numArray13[5] = (byte) 93;
    numArray13[26] = (byte) 112 /*0x70*/;
    numArray13[22] = (byte) 141;
    numArray13[30] = (byte) 141;
    numArray13[29] = (byte) 138;
    numArray13[15] = (byte) 115;
    numArray13[8] = (byte) 64 /*0x40*/;
    numArray13[32 /*0x20*/] = (byte) 75;
    numArray13[33] = (byte) 57;
    byte[] numArray14 = new byte[34];
    numArray14[2] = (byte) 169;
    numArray14[1] = (byte) 156;
    numArray14[27] = (byte) 245;
    numArray14[18] = (byte) 50;
    numArray14[4] = (byte) 101;
    numArray14[5] = (byte) 54;
    numArray14[8] = (byte) 202;
    numArray14[15] = (byte) 230;
    numArray14[19] = (byte) 150;
    numArray14[9] = (byte) 72;
    numArray14[10] = (byte) 92;
    numArray14[30] = (byte) 42;
    numArray14[12] = (byte) 48 /*0x30*/;
    numArray14[32 /*0x20*/] = (byte) 233;
    numArray14[6] = (byte) 197;
    numArray14[3] = (byte) 236;
    numArray14[13] = (byte) 101;
    numArray14[17] = (byte) 160 /*0xA0*/;
    numArray14[33] = (byte) 151;
    numArray14[14] = (byte) 39;
    numArray14[20] = (byte) 167;
    numArray14[31 /*0x1F*/] = (byte) 52;
    numArray14[22] = (byte) 198;
    numArray14[23] = (byte) 72;
    numArray14[24] = (byte) 137;
    numArray14[25] = (byte) 47;
    numArray14[26] = (byte) 148;
    numArray14[7] = (byte) 109;
    numArray14[21] = (byte) 173;
    numArray14[29] = (byte) 149;
    numArray14[0] = (byte) 89;
    numArray14[16 /*0x10*/] = (byte) 132;
    numArray14[28] = (byte) 217;
    numArray14[11] = (byte) 105;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 34);
    for (int index = 0; index < 34; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[15];
    byte[] response = new byte[15];
    Array.Copy((Array) sc_13496.sspq, 54, (Array) numArray15, 0, 15);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_13496.sspr, 54, (Array) numArray15, 0, 15);
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
}
