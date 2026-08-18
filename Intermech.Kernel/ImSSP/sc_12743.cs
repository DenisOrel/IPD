// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12743
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12743
{
  private static byte[] sspq = new byte[149]
  {
    (byte) 211,
    (byte) 28,
    (byte) 183,
    (byte) 97,
    (byte) 20,
    (byte) 249,
    (byte) 109,
    (byte) 225,
    (byte) 210,
    (byte) 133,
    (byte) 92,
    (byte) 152,
    (byte) 192 /*0xC0*/,
    (byte) 95,
    (byte) 212,
    (byte) 147,
    (byte) 245,
    (byte) 117,
    (byte) 53,
    (byte) 84,
    (byte) 76,
    (byte) 123,
    (byte) 119,
    (byte) 32 /*0x20*/,
    (byte) 106,
    (byte) 158,
    (byte) 238,
    (byte) 51,
    (byte) 116,
    (byte) 0,
    (byte) 78,
    (byte) 91,
    (byte) 196,
    (byte) 166,
    (byte) 55,
    (byte) 2,
    (byte) 254,
    (byte) 217,
    (byte) 199,
    (byte) 54,
    (byte) 144 /*0x90*/,
    (byte) 238,
    (byte) 16 /*0x10*/,
    (byte) 127 /*0x7F*/,
    (byte) 148,
    (byte) 135,
    (byte) 167,
    (byte) 64 /*0x40*/,
    (byte) 90,
    (byte) 195,
    (byte) 144 /*0x90*/,
    (byte) 118,
    (byte) 112 /*0x70*/,
    (byte) 146,
    (byte) 198,
    (byte) 242,
    (byte) 110,
    (byte) 140,
    (byte) 229,
    (byte) 73,
    (byte) 227,
    (byte) 246,
    (byte) 39,
    (byte) 113,
    (byte) 11,
    (byte) 135,
    (byte) 146,
    (byte) 167,
    (byte) 136,
    (byte) 6,
    (byte) 47,
    (byte) 56,
    (byte) 126,
    (byte) 160 /*0xA0*/,
    (byte) 101,
    (byte) 162,
    (byte) 120,
    (byte) 18,
    (byte) 61,
    (byte) 104,
    (byte) 238,
    (byte) 7,
    (byte) 65,
    (byte) 154,
    (byte) 234,
    (byte) 46,
    (byte) 232,
    (byte) 247,
    (byte) 29,
    (byte) 53,
    (byte) 108,
    (byte) 71,
    (byte) 0,
    (byte) 67,
    (byte) 242,
    (byte) 248,
    (byte) 155,
    (byte) 201,
    (byte) 178,
    (byte) 148,
    (byte) 128 /*0x80*/,
    (byte) 25,
    byte.MaxValue,
    (byte) 185,
    (byte) 55,
    (byte) 76,
    (byte) 237,
    (byte) 230,
    (byte) 156,
    (byte) 111,
    (byte) 183,
    (byte) 252,
    (byte) 212,
    (byte) 20,
    (byte) 93,
    (byte) 233,
    (byte) 150,
    (byte) 107,
    (byte) 218,
    (byte) 3,
    (byte) 108,
    (byte) 136,
    (byte) 102,
    (byte) 222,
    (byte) 211,
    (byte) 233,
    (byte) 88,
    (byte) 210,
    (byte) 115,
    (byte) 181,
    (byte) 191,
    (byte) 233,
    (byte) 238,
    (byte) 107,
    (byte) 137,
    (byte) 185,
    (byte) 35,
    (byte) 80 /*0x50*/,
    (byte) 222,
    (byte) 70,
    (byte) 126,
    (byte) 34,
    (byte) 159,
    (byte) 59,
    (byte) 105,
    (byte) 155,
    (byte) 113,
    (byte) 238,
    (byte) 152
  };
  private static byte[] sspr = new byte[149]
  {
    (byte) 42,
    (byte) 153,
    (byte) 123,
    (byte) 164,
    (byte) 145,
    (byte) 156,
    (byte) 169,
    (byte) 223,
    (byte) 171,
    (byte) 26,
    (byte) 98,
    (byte) 94,
    (byte) 228,
    (byte) 32 /*0x20*/,
    (byte) 81,
    (byte) 105,
    (byte) 208 /*0xD0*/,
    (byte) 94,
    (byte) 137,
    (byte) 79,
    (byte) 78,
    (byte) 136,
    (byte) 173,
    (byte) 88,
    (byte) 132,
    (byte) 48 /*0x30*/,
    (byte) 104,
    (byte) 7,
    (byte) 10,
    (byte) 123,
    (byte) 22,
    (byte) 46,
    (byte) 32 /*0x20*/,
    (byte) 181,
    (byte) 169,
    (byte) 167,
    (byte) 42,
    (byte) 70,
    (byte) 208 /*0xD0*/,
    (byte) 74,
    (byte) 70,
    (byte) 175,
    (byte) 124,
    (byte) 254,
    (byte) 2,
    (byte) 150,
    (byte) 175,
    (byte) 172,
    (byte) 137,
    (byte) 196,
    (byte) 26,
    (byte) 90,
    (byte) 233,
    (byte) 39,
    (byte) 100,
    (byte) 10,
    (byte) 30,
    (byte) 106,
    (byte) 124,
    (byte) 28,
    (byte) 213,
    (byte) 201,
    (byte) 232,
    (byte) 49,
    (byte) 121,
    (byte) 95,
    (byte) 115,
    (byte) 161,
    (byte) 20,
    (byte) 208 /*0xD0*/,
    (byte) 186,
    (byte) 172,
    (byte) 77,
    (byte) 194,
    (byte) 54,
    (byte) 105,
    (byte) 114,
    (byte) 99,
    (byte) 200,
    (byte) 23,
    (byte) 60,
    (byte) 36,
    (byte) 249,
    (byte) 243,
    (byte) 241,
    (byte) 185,
    (byte) 65,
    (byte) 109,
    (byte) 174,
    (byte) 102,
    (byte) 175,
    (byte) 113,
    (byte) 70,
    (byte) 233,
    (byte) 120,
    (byte) 59,
    (byte) 117,
    (byte) 243,
    (byte) 184,
    (byte) 229,
    (byte) 184,
    (byte) 105,
    (byte) 142,
    (byte) 59,
    (byte) 239,
    (byte) 50,
    (byte) 89,
    (byte) 108,
    (byte) 71,
    (byte) 40,
    (byte) 234,
    (byte) 11,
    (byte) 138,
    (byte) 195,
    (byte) 247,
    (byte) 163,
    (byte) 49,
    (byte) 236,
    (byte) 180,
    (byte) 78,
    (byte) 154,
    (byte) 63 /*0x3F*/,
    (byte) 67,
    (byte) 188,
    (byte) 105,
    (byte) 131,
    (byte) 92,
    (byte) 26,
    (byte) 237,
    (byte) 95,
    (byte) 51,
    (byte) 210,
    (byte) 79,
    (byte) 120,
    (byte) 20,
    (byte) 90,
    (byte) 102,
    (byte) 240 /*0xF0*/,
    (byte) 172,
    (byte) 126,
    (byte) 200,
    (byte) 119,
    (byte) 210,
    (byte) 191,
    (byte) 25,
    (byte) 177,
    (byte) 95,
    (byte) 4,
    (byte) 197
  };

  internal static int ssp_appserver_12744(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[15] = (byte) 194;
    sourceArray1[3] = (byte) 211;
    sourceArray1[17] = (byte) 14;
    sourceArray1[6] = (byte) 110;
    sourceArray1[4] = (byte) 218;
    sourceArray1[39] = (byte) 31 /*0x1F*/;
    sourceArray1[33] = (byte) 249;
    sourceArray1[9] = (byte) 111;
    sourceArray1[35] = (byte) 13;
    sourceArray1[10] = (byte) 37;
    sourceArray1[27] = (byte) 151;
    sourceArray1[11] = (byte) 101;
    sourceArray1[2] = (byte) 156;
    sourceArray1[7] = (byte) 182;
    sourceArray1[14] = (byte) 142;
    sourceArray1[29] = (byte) 153;
    sourceArray1[16 /*0x10*/] = (byte) 247;
    sourceArray1[34] = (byte) 186;
    sourceArray1[18] = (byte) 201;
    sourceArray1[0] = (byte) 177;
    sourceArray1[20] = (byte) 47;
    sourceArray1[42] = (byte) 163;
    sourceArray1[22] = (byte) 138;
    sourceArray1[23] = (byte) 182;
    sourceArray1[26] = (byte) 246;
    sourceArray1[24] = (byte) 1;
    sourceArray1[12] = (byte) 208 /*0xD0*/;
    sourceArray1[37] = (byte) 183;
    sourceArray1[28] = (byte) 194;
    sourceArray1[25] = (byte) 194;
    sourceArray1[30] = (byte) 25;
    sourceArray1[31 /*0x1F*/] = (byte) 91;
    sourceArray1[32 /*0x20*/] = (byte) 38;
    sourceArray1[21] = (byte) 152;
    sourceArray1[47] = (byte) 186;
    sourceArray1[19] = (byte) 134;
    sourceArray1[36] = byte.MaxValue;
    sourceArray1[5] = (byte) 229;
    sourceArray1[38] = (byte) 191;
    sourceArray1[45] = (byte) 185;
    sourceArray1[40] = (byte) 82;
    sourceArray1[41] = (byte) 6;
    sourceArray1[1] = (byte) 42;
    sourceArray1[43] = (byte) 10;
    sourceArray1[44] = (byte) 153;
    sourceArray1[8] = (byte) 220;
    sourceArray1[46] = (byte) 95;
    sourceArray1[13] = (byte) 170;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 21;
    sourceArray2[5] = (byte) 237;
    sourceArray2[2] = (byte) 237;
    sourceArray2[3] = (byte) 234;
    sourceArray2[32 /*0x20*/] = (byte) 28;
    sourceArray2[14] = (byte) 203;
    sourceArray2[23] = (byte) 187;
    sourceArray2[7] = (byte) 191;
    sourceArray2[11] = (byte) 191;
    sourceArray2[40] = (byte) 63 /*0x3F*/;
    sourceArray2[44] = (byte) 99;
    sourceArray2[21] = (byte) 122;
    sourceArray2[16 /*0x10*/] = (byte) 84;
    sourceArray2[36] = (byte) 136;
    sourceArray2[25] = (byte) 162;
    sourceArray2[41] = (byte) 136;
    sourceArray2[20] = (byte) 79;
    sourceArray2[0] = (byte) 73;
    sourceArray2[18] = (byte) 205;
    sourceArray2[19] = (byte) 155;
    sourceArray2[17] = (byte) 138;
    sourceArray2[8] = (byte) 124;
    sourceArray2[1] = (byte) 186;
    sourceArray2[35] = (byte) 147;
    sourceArray2[24] = (byte) 64 /*0x40*/;
    sourceArray2[22] = (byte) 99;
    sourceArray2[26] = (byte) 176 /*0xB0*/;
    sourceArray2[27] = (byte) 226;
    sourceArray2[12] = (byte) 162;
    sourceArray2[29] = (byte) 128 /*0x80*/;
    sourceArray2[30] = (byte) 92;
    sourceArray2[45] = (byte) 217;
    sourceArray2[39] = (byte) 110;
    sourceArray2[31 /*0x1F*/] = (byte) 213;
    sourceArray2[34] = (byte) 189;
    sourceArray2[15] = (byte) 188;
    sourceArray2[28] = (byte) 138;
    sourceArray2[37] = (byte) 171;
    sourceArray2[38] = (byte) 24;
    sourceArray2[9] = (byte) 193;
    sourceArray2[33] = (byte) 217;
    sourceArray2[6] = (byte) 158;
    sourceArray2[46] = (byte) 242;
    sourceArray2[43] = (byte) 107;
    sourceArray2[42] = (byte) 129;
    sourceArray2[10] = (byte) 45;
    sourceArray2[13] = (byte) 147;
    sourceArray2[47] = (byte) 166;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12745()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37]
      {
        (byte) 243,
        (byte) 124,
        (byte) 214,
        (byte) 79,
        (byte) 254,
        (byte) 207,
        (byte) 20,
        (byte) 75,
        (byte) 166,
        (byte) 123,
        (byte) 206,
        (byte) 35,
        (byte) 33,
        (byte) 177,
        (byte) 43,
        (byte) 40,
        (byte) 196,
        (byte) 143,
        (byte) 85,
        (byte) 124,
        (byte) 187,
        (byte) 216,
        (byte) 65,
        (byte) 84,
        (byte) 93,
        (byte) 252,
        (byte) 202,
        (byte) 82,
        byte.MaxValue,
        (byte) 197,
        (byte) 208 /*0xD0*/,
        (byte) 200,
        (byte) 89,
        (byte) 253,
        (byte) 179,
        (byte) 252,
        (byte) 226
      };
      byte[] numArray3 = new byte[37];
      numArray3[27] = (byte) 183;
      numArray3[1] = (byte) 254;
      numArray3[2] = (byte) 71;
      numArray3[3] = (byte) 185;
      numArray3[4] = (byte) 10;
      numArray3[11] = (byte) 225;
      numArray3[35] = (byte) 147;
      numArray3[18] = (byte) 189;
      numArray3[8] = (byte) 235;
      numArray3[9] = (byte) 178;
      numArray3[33] = (byte) 243;
      numArray3[29] = (byte) 61;
      numArray3[0] = (byte) 253;
      numArray3[13] = (byte) 59;
      numArray3[14] = (byte) 195;
      numArray3[31 /*0x1F*/] = (byte) 238;
      numArray3[19] = (byte) 112 /*0x70*/;
      numArray3[36] = (byte) 80 /*0x50*/;
      numArray3[5] = (byte) 124;
      numArray3[10] = (byte) 89;
      numArray3[17] = (byte) 169;
      numArray3[21] = (byte) 166;
      numArray3[22] = (byte) 170;
      numArray3[23] = (byte) 39;
      numArray3[7] = (byte) 133;
      numArray3[25] = (byte) 209;
      numArray3[15] = (byte) 198;
      numArray3[6] = (byte) 165;
      numArray3[28] = (byte) 157;
      numArray3[12] = (byte) 95;
      numArray3[30] = (byte) 99;
      numArray3[26] = (byte) 93;
      numArray3[20] = (byte) 23;
      numArray3[16 /*0x10*/] = (byte) 81;
      numArray3[34] = (byte) 67;
      numArray3[24] = (byte) 141;
      numArray3[32 /*0x20*/] = (byte) 73;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[25];
      byte[] response = new byte[25];
      Array.Copy((Array) sc_12743.sspq, 0, (Array) numArray4, 0, 25);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12743.sspr, 0, (Array) numArray4, 0, 25);
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
    byte[] numArray6 = new byte[37];
    numArray6[17] = (byte) 51;
    numArray6[1] = (byte) 192 /*0xC0*/;
    numArray6[29] = (byte) 254;
    numArray6[13] = (byte) 174;
    numArray6[4] = (byte) 156;
    numArray6[5] = (byte) 143;
    numArray6[6] = (byte) 102;
    numArray6[3] = (byte) 245;
    numArray6[31 /*0x1F*/] = (byte) 165;
    numArray6[22] = (byte) 230;
    numArray6[16 /*0x10*/] = (byte) 201;
    numArray6[28] = (byte) 78;
    numArray6[7] = (byte) 120;
    numArray6[34] = (byte) 5;
    numArray6[14] = (byte) 83;
    numArray6[15] = (byte) 114;
    numArray6[32 /*0x20*/] = (byte) 150;
    numArray6[9] = (byte) 175;
    numArray6[0] = (byte) 15;
    numArray6[19] = (byte) 199;
    numArray6[10] = (byte) 213;
    numArray6[21] = (byte) 109;
    numArray6[35] = (byte) 57;
    numArray6[18] = (byte) 67;
    numArray6[11] = (byte) 75;
    numArray6[25] = (byte) 100;
    numArray6[36] = (byte) 20;
    numArray6[27] = (byte) 4;
    numArray6[20] = (byte) 102;
    numArray6[23] = (byte) 41;
    numArray6[26] = (byte) 206;
    numArray6[24] = (byte) 102;
    numArray6[2] = (byte) 89;
    numArray6[8] = (byte) 241;
    numArray6[12] = (byte) 163;
    numArray6[30] = (byte) 87;
    numArray6[33] = (byte) 247;
    byte[] numArray7 = new byte[37]
    {
      (byte) 7,
      (byte) 57,
      (byte) 55,
      (byte) 106,
      (byte) 143,
      (byte) 153,
      (byte) 254,
      (byte) 143,
      (byte) 186,
      (byte) 16 /*0x10*/,
      (byte) 190,
      (byte) 17,
      (byte) 221,
      (byte) 75,
      (byte) 205,
      (byte) 209,
      (byte) 80 /*0x50*/,
      (byte) 214,
      (byte) 211,
      (byte) 225,
      (byte) 118,
      (byte) 10,
      (byte) 177,
      (byte) 212,
      (byte) 165,
      (byte) 76,
      (byte) 234,
      (byte) 229,
      (byte) 200,
      (byte) 87,
      (byte) 66,
      (byte) 167,
      (byte) 226,
      (byte) 134,
      (byte) 45,
      (byte) 173,
      (byte) 222
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12746(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[43] = (byte) 183;
    sourceArray1[30] = (byte) 189;
    sourceArray1[0] = (byte) 29;
    sourceArray1[3] = (byte) 33;
    sourceArray1[29] = (byte) 136;
    sourceArray1[5] = (byte) 123;
    sourceArray1[6] = (byte) 15;
    sourceArray1[24] = (byte) 200;
    sourceArray1[8] = (byte) 60;
    sourceArray1[31 /*0x1F*/] = (byte) 3;
    sourceArray1[10] = (byte) 99;
    sourceArray1[11] = (byte) 95;
    sourceArray1[12] = (byte) 229;
    sourceArray1[26] = (byte) 165;
    sourceArray1[14] = (byte) 253;
    sourceArray1[15] = (byte) 52;
    sourceArray1[41] = (byte) 190;
    sourceArray1[9] = (byte) 244;
    sourceArray1[18] = (byte) 220;
    sourceArray1[16 /*0x10*/] = (byte) 147;
    sourceArray1[20] = (byte) 206;
    sourceArray1[21] = (byte) 147;
    sourceArray1[22] = (byte) 165;
    sourceArray1[23] = (byte) 29;
    sourceArray1[38] = (byte) 189;
    sourceArray1[35] = (byte) 37;
    sourceArray1[27] = (byte) 73;
    sourceArray1[13] = (byte) 92;
    sourceArray1[28] = (byte) 230;
    sourceArray1[40] = (byte) 71;
    sourceArray1[1] = (byte) 18;
    sourceArray1[2] = (byte) 7;
    sourceArray1[33] = (byte) 4;
    sourceArray1[17] = (byte) 245;
    sourceArray1[7] = (byte) 113;
    sourceArray1[32 /*0x20*/] = (byte) 163;
    sourceArray1[36] = (byte) 185;
    sourceArray1[37] = (byte) 64 /*0x40*/;
    sourceArray1[44] = (byte) 69;
    sourceArray1[39] = (byte) 119;
    sourceArray1[4] = (byte) 108;
    sourceArray1[34] = (byte) 113;
    sourceArray1[42] = (byte) 119;
    sourceArray1[25] = (byte) 131;
    sourceArray1[45] = (byte) 186;
    sourceArray1[19] = (byte) 88;
    sourceArray1[46] = (byte) 120;
    sourceArray1[47] = (byte) 129;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 144 /*0x90*/,
      (byte) 128 /*0x80*/,
      (byte) 5,
      (byte) 12,
      (byte) 42,
      (byte) 223,
      (byte) 126,
      (byte) 29,
      (byte) 127 /*0x7F*/,
      (byte) 59,
      (byte) 102,
      (byte) 153,
      (byte) 91,
      (byte) 218,
      (byte) 6,
      (byte) 38,
      (byte) 29,
      (byte) 49,
      (byte) 34,
      (byte) 121,
      (byte) 164,
      (byte) 18,
      (byte) 121,
      (byte) 25,
      (byte) 135,
      (byte) 246,
      (byte) 55,
      (byte) 144 /*0x90*/,
      (byte) 178,
      (byte) 207,
      (byte) 160 /*0xA0*/,
      (byte) 217,
      (byte) 78,
      (byte) 159,
      (byte) 212,
      (byte) 248,
      (byte) 140,
      (byte) 161,
      (byte) 121,
      (byte) 50,
      (byte) 55,
      (byte) 229,
      (byte) 42,
      (byte) 226,
      (byte) 118,
      (byte) 134,
      (byte) 14,
      (byte) 13
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12747(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 150,
      (byte) 38,
      (byte) 157,
      (byte) 235,
      (byte) 23,
      (byte) 92,
      (byte) 204,
      (byte) 76,
      (byte) 109,
      (byte) 126,
      (byte) 18,
      (byte) 75,
      (byte) 222,
      (byte) 177,
      (byte) 124,
      (byte) 189,
      (byte) 106,
      (byte) 202,
      (byte) 30,
      (byte) 245,
      (byte) 57,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 171,
      (byte) 107,
      (byte) 100,
      (byte) 194,
      (byte) 142,
      (byte) 219,
      (byte) 89,
      (byte) 108,
      (byte) 242,
      (byte) 15,
      (byte) 115,
      (byte) 141,
      (byte) 34,
      (byte) 47,
      (byte) 94,
      (byte) 84,
      (byte) 229,
      (byte) 249,
      (byte) 90,
      (byte) 121,
      (byte) 57,
      (byte) 189,
      (byte) 155,
      (byte) 245
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[7] = (byte) 72;
    sourceArray2[16 /*0x10*/] = (byte) 170;
    sourceArray2[1] = (byte) 62;
    sourceArray2[10] = (byte) 118;
    sourceArray2[12] = (byte) 241;
    sourceArray2[24] = (byte) 66;
    sourceArray2[25] = (byte) 34;
    sourceArray2[4] = (byte) 20;
    sourceArray2[42] = (byte) 50;
    sourceArray2[9] = (byte) 14;
    sourceArray2[45] = (byte) 88;
    sourceArray2[13] = (byte) 8;
    sourceArray2[30] = (byte) 6;
    sourceArray2[28] = (byte) 153;
    sourceArray2[39] = (byte) 156;
    sourceArray2[15] = (byte) 155;
    sourceArray2[41] = (byte) 183;
    sourceArray2[8] = (byte) 159;
    sourceArray2[18] = (byte) 27;
    sourceArray2[19] = (byte) 29;
    sourceArray2[20] = (byte) 54;
    sourceArray2[21] = (byte) 11;
    sourceArray2[27] = (byte) 26;
    sourceArray2[23] = (byte) 117;
    sourceArray2[14] = (byte) 253;
    sourceArray2[0] = (byte) 240 /*0xF0*/;
    sourceArray2[43] = (byte) 233;
    sourceArray2[29] = (byte) 74;
    sourceArray2[22] = (byte) 59;
    sourceArray2[26] = (byte) 176 /*0xB0*/;
    sourceArray2[44] = (byte) 70;
    sourceArray2[46] = (byte) 201;
    sourceArray2[32 /*0x20*/] = (byte) 77;
    sourceArray2[3] = (byte) 27;
    sourceArray2[34] = (byte) 225;
    sourceArray2[35] = (byte) 235;
    sourceArray2[36] = (byte) 57;
    sourceArray2[37] = (byte) 176 /*0xB0*/;
    sourceArray2[38] = (byte) 205;
    sourceArray2[33] = (byte) 201;
    sourceArray2[40] = (byte) 177;
    sourceArray2[6] = (byte) 234;
    sourceArray2[11] = (byte) 128 /*0x80*/;
    sourceArray2[5] = (byte) 21;
    sourceArray2[31 /*0x1F*/] = (byte) 124;
    sourceArray2[17] = (byte) 26;
    sourceArray2[2] = (byte) 90;
    sourceArray2[47] = (byte) 134;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12748(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 94,
      (byte) 138,
      (byte) 206,
      (byte) 172,
      (byte) 232,
      (byte) 75,
      (byte) 65,
      (byte) 142,
      (byte) 12,
      (byte) 126,
      (byte) 11,
      (byte) 30,
      (byte) 4,
      (byte) 175,
      (byte) 68,
      (byte) 199,
      (byte) 180,
      (byte) 11,
      (byte) 18,
      (byte) 91,
      (byte) 242,
      (byte) 54,
      (byte) 85,
      (byte) 41,
      (byte) 60,
      (byte) 80 /*0x50*/,
      (byte) 46,
      (byte) 211,
      (byte) 235,
      (byte) 64 /*0x40*/,
      (byte) 245,
      (byte) 35,
      (byte) 191,
      (byte) 79,
      (byte) 114,
      (byte) 228,
      (byte) 3,
      (byte) 220,
      (byte) 125,
      (byte) 183,
      (byte) 116,
      (byte) 158,
      (byte) 249,
      (byte) 151,
      (byte) 238,
      (byte) 253,
      (byte) 57,
      (byte) 47
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 225;
    sourceArray2[1] = (byte) 233;
    sourceArray2[45] = (byte) 144 /*0x90*/;
    sourceArray2[27] = (byte) 237;
    sourceArray2[4] = (byte) 62;
    sourceArray2[5] = (byte) 127 /*0x7F*/;
    sourceArray2[6] = (byte) 186;
    sourceArray2[36] = (byte) 30;
    sourceArray2[26] = (byte) 224 /*0xE0*/;
    sourceArray2[47] = (byte) 26;
    sourceArray2[16 /*0x10*/] = (byte) 146;
    sourceArray2[41] = (byte) 8;
    sourceArray2[12] = (byte) 169;
    sourceArray2[0] = (byte) 175;
    sourceArray2[13] = (byte) 78;
    sourceArray2[31 /*0x1F*/] = (byte) 182;
    sourceArray2[21] = (byte) 242;
    sourceArray2[17] = (byte) 169;
    sourceArray2[19] = (byte) 188;
    sourceArray2[46] = (byte) 189;
    sourceArray2[20] = (byte) 157;
    sourceArray2[40] = (byte) 177;
    sourceArray2[22] = (byte) 108;
    sourceArray2[32 /*0x20*/] = (byte) 225;
    sourceArray2[24] = (byte) 20;
    sourceArray2[25] = (byte) 139;
    sourceArray2[14] = (byte) 248;
    sourceArray2[23] = (byte) 235;
    sourceArray2[28] = (byte) 149;
    sourceArray2[29] = (byte) 79;
    sourceArray2[7] = (byte) 243;
    sourceArray2[33] = (byte) 236;
    sourceArray2[30] = (byte) 185;
    sourceArray2[8] = (byte) 31 /*0x1F*/;
    sourceArray2[34] = (byte) 98;
    sourceArray2[35] = (byte) 17;
    sourceArray2[11] = (byte) 22;
    sourceArray2[37] = (byte) 63 /*0x3F*/;
    sourceArray2[38] = (byte) 105;
    sourceArray2[39] = (byte) 162;
    sourceArray2[10] = (byte) 60;
    sourceArray2[15] = (byte) 159;
    sourceArray2[3] = (byte) 67;
    sourceArray2[43] = (byte) 232;
    sourceArray2[44] = (byte) 191;
    sourceArray2[9] = (byte) 252;
    sourceArray2[18] = (byte) 1;
    sourceArray2[2] = (byte) 21;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12749(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 133,
      (byte) 242,
      (byte) 254,
      (byte) 192 /*0xC0*/,
      (byte) 46,
      (byte) 79,
      (byte) 248,
      (byte) 227,
      (byte) 86,
      (byte) 233,
      (byte) 118,
      (byte) 105,
      (byte) 48 /*0x30*/,
      (byte) 128 /*0x80*/,
      (byte) 29,
      (byte) 247,
      (byte) 59,
      (byte) 146,
      (byte) 112 /*0x70*/,
      (byte) 24,
      (byte) 106,
      (byte) 16 /*0x10*/,
      (byte) 241,
      (byte) 175,
      (byte) 101,
      (byte) 106,
      (byte) 168,
      (byte) 192 /*0xC0*/,
      (byte) 225,
      (byte) 88,
      (byte) 16 /*0x10*/,
      (byte) 175,
      (byte) 51,
      (byte) 160 /*0xA0*/,
      (byte) 145,
      (byte) 12,
      (byte) 120,
      (byte) 166,
      (byte) 10,
      (byte) 201,
      (byte) 215,
      (byte) 16 /*0x10*/,
      (byte) 77,
      (byte) 97,
      (byte) 56,
      (byte) 244,
      (byte) 61,
      (byte) 101
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 189,
      (byte) 132,
      (byte) 71,
      (byte) 53,
      (byte) 181,
      (byte) 13,
      (byte) 21,
      (byte) 201,
      (byte) 181,
      (byte) 221,
      (byte) 197,
      (byte) 133,
      (byte) 198,
      (byte) 226,
      (byte) 199,
      (byte) 89,
      (byte) 49,
      (byte) 168,
      (byte) 17,
      (byte) 89,
      (byte) 152,
      (byte) 132,
      (byte) 252,
      (byte) 145,
      (byte) 173,
      (byte) 240 /*0xF0*/,
      (byte) 171,
      (byte) 235,
      (byte) 254,
      (byte) 174,
      (byte) 1,
      (byte) 225,
      (byte) 71,
      (byte) 17,
      (byte) 126,
      (byte) 223,
      (byte) 205,
      (byte) 225,
      (byte) 233,
      (byte) 98,
      (byte) 101,
      (byte) 109,
      (byte) 94,
      (byte) 224 /*0xE0*/,
      (byte) 250,
      (byte) 185,
      (byte) 47,
      (byte) 61
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[28];
    byte[] response2 = new byte[28];
    Array.Copy((Array) sc_12743.sspq, 25, (Array) numArray2, 0, 28);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12743.sspr, 25, (Array) numArray2, 0, 28);
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

  internal static int ssp_appserver_12750(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[38] = (byte) 182;
    sourceArray1[1] = (byte) 210;
    sourceArray1[2] = (byte) 151;
    sourceArray1[3] = (byte) 117;
    sourceArray1[34] = (byte) 242;
    sourceArray1[47] = (byte) 160 /*0xA0*/;
    sourceArray1[8] = (byte) 12;
    sourceArray1[6] = (byte) 108;
    sourceArray1[30] = (byte) 215;
    sourceArray1[13] = (byte) 160 /*0xA0*/;
    sourceArray1[18] = (byte) 190;
    sourceArray1[9] = (byte) 41;
    sourceArray1[12] = (byte) 34;
    sourceArray1[17] = (byte) 113;
    sourceArray1[14] = (byte) 13;
    sourceArray1[15] = (byte) 78;
    sourceArray1[16 /*0x10*/] = (byte) 135;
    sourceArray1[44] = (byte) 72;
    sourceArray1[27] = (byte) 217;
    sourceArray1[19] = (byte) 172;
    sourceArray1[43] = (byte) 86;
    sourceArray1[11] = (byte) 70;
    sourceArray1[22] = (byte) 142;
    sourceArray1[33] = (byte) 99;
    sourceArray1[10] = (byte) 75;
    sourceArray1[25] = (byte) 247;
    sourceArray1[23] = (byte) 44;
    sourceArray1[21] = (byte) 107;
    sourceArray1[28] = (byte) 177;
    sourceArray1[20] = (byte) 118;
    sourceArray1[46] = (byte) 133;
    sourceArray1[24] = (byte) 18;
    sourceArray1[32 /*0x20*/] = (byte) 151;
    sourceArray1[0] = (byte) 68;
    sourceArray1[7] = (byte) 29;
    sourceArray1[5] = (byte) 69;
    sourceArray1[36] = (byte) 102;
    sourceArray1[37] = (byte) 99;
    sourceArray1[4] = (byte) 16 /*0x10*/;
    sourceArray1[31 /*0x1F*/] = (byte) 234;
    sourceArray1[40] = (byte) 35;
    sourceArray1[41] = (byte) 209;
    sourceArray1[42] = (byte) 3;
    sourceArray1[39] = (byte) 240 /*0xF0*/;
    sourceArray1[35] = (byte) 47;
    sourceArray1[45] = (byte) 65;
    sourceArray1[26] = (byte) 174;
    sourceArray1[29] = (byte) 111;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 102,
      (byte) 79,
      (byte) 84,
      (byte) 37,
      (byte) 54,
      (byte) 13,
      (byte) 100,
      (byte) 109,
      (byte) 81,
      (byte) 20,
      (byte) 166,
      (byte) 71,
      (byte) 113,
      (byte) 73,
      (byte) 191,
      (byte) 115,
      (byte) 197,
      (byte) 67,
      (byte) 196,
      (byte) 80 /*0x50*/,
      (byte) 187,
      (byte) 107,
      (byte) 122,
      (byte) 79,
      (byte) 140,
      (byte) 183,
      (byte) 164,
      (byte) 103,
      (byte) 170,
      (byte) 167,
      (byte) 98,
      (byte) 242,
      (byte) 249,
      (byte) 162,
      (byte) 233,
      (byte) 119,
      (byte) 81,
      (byte) 251,
      (byte) 10,
      (byte) 90,
      (byte) 139,
      (byte) 68,
      (byte) 133,
      (byte) 238,
      (byte) 174,
      (byte) 190,
      (byte) 71,
      (byte) 86
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[20];
    byte[] response2 = new byte[20];
    Array.Copy((Array) sc_12743.sspq, 53, (Array) numArray2, 0, 20);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12743.sspr, 53, (Array) numArray2, 0, 20);
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

  internal static int ssp_appserver_12751(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 250;
    sourceArray1[1] = (byte) 148;
    sourceArray1[2] = (byte) 242;
    sourceArray1[3] = (byte) 237;
    sourceArray1[16 /*0x10*/] = (byte) 176 /*0xB0*/;
    sourceArray1[5] = (byte) 30;
    sourceArray1[19] = (byte) 85;
    sourceArray1[27] = (byte) 209;
    sourceArray1[38] = (byte) 194;
    sourceArray1[25] = (byte) 109;
    sourceArray1[8] = (byte) 64 /*0x40*/;
    sourceArray1[45] = (byte) 140;
    sourceArray1[12] = (byte) 61;
    sourceArray1[0] = (byte) 38;
    sourceArray1[22] = (byte) 167;
    sourceArray1[15] = (byte) 29;
    sourceArray1[10] = (byte) 51;
    sourceArray1[28] = (byte) 137;
    sourceArray1[18] = (byte) 137;
    sourceArray1[21] = (byte) 112 /*0x70*/;
    sourceArray1[6] = (byte) 115;
    sourceArray1[32 /*0x20*/] = (byte) 192 /*0xC0*/;
    sourceArray1[31 /*0x1F*/] = (byte) 134;
    sourceArray1[23] = (byte) 223;
    sourceArray1[24] = (byte) 115;
    sourceArray1[47] = (byte) 251;
    sourceArray1[26] = (byte) 245;
    sourceArray1[20] = (byte) 31 /*0x1F*/;
    sourceArray1[7] = (byte) 249;
    sourceArray1[43] = (byte) 112 /*0x70*/;
    sourceArray1[30] = (byte) 45;
    sourceArray1[41] = (byte) 253;
    sourceArray1[13] = (byte) 171;
    sourceArray1[29] = (byte) 53;
    sourceArray1[4] = (byte) 35;
    sourceArray1[9] = (byte) 1;
    sourceArray1[35] = (byte) 94;
    sourceArray1[37] = (byte) 158;
    sourceArray1[34] = (byte) 159;
    sourceArray1[39] = (byte) 43;
    sourceArray1[44] = (byte) 216;
    sourceArray1[17] = (byte) 127 /*0x7F*/;
    sourceArray1[11] = (byte) 251;
    sourceArray1[42] = (byte) 231;
    sourceArray1[40] = (byte) 111;
    sourceArray1[14] = (byte) 27;
    sourceArray1[46] = (byte) 85;
    sourceArray1[36] = (byte) 124;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 80 /*0x50*/;
    sourceArray2[1] = (byte) 170;
    sourceArray2[2] = (byte) 49;
    sourceArray2[9] = (byte) 70;
    sourceArray2[4] = (byte) 85;
    sourceArray2[5] = (byte) 179;
    sourceArray2[35] = (byte) 50;
    sourceArray2[46] = (byte) 112 /*0x70*/;
    sourceArray2[31 /*0x1F*/] = (byte) 81;
    sourceArray2[45] = (byte) 106;
    sourceArray2[7] = (byte) 132;
    sourceArray2[11] = (byte) 170;
    sourceArray2[33] = (byte) 123;
    sourceArray2[10] = (byte) 217;
    sourceArray2[14] = (byte) 182;
    sourceArray2[15] = (byte) 10;
    sourceArray2[16 /*0x10*/] = (byte) 152;
    sourceArray2[34] = (byte) 203;
    sourceArray2[18] = (byte) 126;
    sourceArray2[19] = (byte) 57;
    sourceArray2[20] = (byte) 139;
    sourceArray2[21] = (byte) 191;
    sourceArray2[41] = (byte) 168;
    sourceArray2[23] = (byte) 240 /*0xF0*/;
    sourceArray2[25] = (byte) 90;
    sourceArray2[0] = (byte) 240 /*0xF0*/;
    sourceArray2[26] = (byte) 180;
    sourceArray2[27] = (byte) 221;
    sourceArray2[22] = (byte) 82;
    sourceArray2[36] = (byte) 114;
    sourceArray2[30] = (byte) 218;
    sourceArray2[29] = (byte) 235;
    sourceArray2[32 /*0x20*/] = (byte) 55;
    sourceArray2[3] = (byte) 31 /*0x1F*/;
    sourceArray2[40] = (byte) 36;
    sourceArray2[44] = (byte) 105;
    sourceArray2[28] = (byte) 92;
    sourceArray2[12] = (byte) 120;
    sourceArray2[38] = (byte) 221;
    sourceArray2[37] = (byte) 143;
    sourceArray2[17] = (byte) 85;
    sourceArray2[13] = (byte) 166;
    sourceArray2[42] = (byte) 40;
    sourceArray2[43] = (byte) 51;
    sourceArray2[24] = (byte) 76;
    sourceArray2[8] = (byte) 139;
    sourceArray2[6] = (byte) 77;
    sourceArray2[47] = (byte) 251;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12752(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[33] = (byte) 68;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[2] = (byte) 189;
    sourceArray1[5] = (byte) 14;
    sourceArray1[4] = (byte) 62;
    sourceArray1[39] = (byte) 205;
    sourceArray1[35] = (byte) 135;
    sourceArray1[7] = (byte) 82;
    sourceArray1[8] = (byte) 204;
    sourceArray1[13] = (byte) 58;
    sourceArray1[10] = (byte) 17;
    sourceArray1[40] = (byte) 144 /*0x90*/;
    sourceArray1[12] = (byte) 73;
    sourceArray1[26] = (byte) 33;
    sourceArray1[27] = (byte) 74;
    sourceArray1[15] = (byte) 193;
    sourceArray1[16 /*0x10*/] = (byte) 129;
    sourceArray1[17] = (byte) 25;
    sourceArray1[3] = (byte) 137;
    sourceArray1[19] = (byte) 252;
    sourceArray1[46] = (byte) 49;
    sourceArray1[38] = (byte) 92;
    sourceArray1[22] = (byte) 135;
    sourceArray1[23] = (byte) 120;
    sourceArray1[37] = (byte) 34;
    sourceArray1[45] = (byte) 202;
    sourceArray1[34] = (byte) 137;
    sourceArray1[20] = (byte) 153;
    sourceArray1[28] = (byte) 148;
    sourceArray1[30] = (byte) 143;
    sourceArray1[18] = (byte) 46;
    sourceArray1[31 /*0x1F*/] = (byte) 215;
    sourceArray1[29] = (byte) 103;
    sourceArray1[11] = (byte) 128 /*0x80*/;
    sourceArray1[24] = (byte) 8;
    sourceArray1[44] = (byte) 70;
    sourceArray1[36] = (byte) 214;
    sourceArray1[6] = (byte) 5;
    sourceArray1[25] = (byte) 227;
    sourceArray1[47] = (byte) 191;
    sourceArray1[14] = (byte) 212;
    sourceArray1[41] = (byte) 178;
    sourceArray1[42] = (byte) 61;
    sourceArray1[43] = (byte) 83;
    sourceArray1[9] = (byte) 98;
    sourceArray1[0] = (byte) 210;
    sourceArray1[21] = (byte) 37;
    sourceArray1[32 /*0x20*/] = (byte) 89;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[4] = (byte) 33;
    sourceArray2[7] = (byte) 93;
    sourceArray2[2] = (byte) 184;
    sourceArray2[3] = (byte) 148;
    sourceArray2[35] = (byte) 111;
    sourceArray2[13] = (byte) 64 /*0x40*/;
    sourceArray2[6] = (byte) 93;
    sourceArray2[44] = (byte) 138;
    sourceArray2[23] = (byte) 227;
    sourceArray2[25] = (byte) 83;
    sourceArray2[22] = (byte) 138;
    sourceArray2[8] = (byte) 166;
    sourceArray2[12] = (byte) 161;
    sourceArray2[16 /*0x10*/] = (byte) 162;
    sourceArray2[14] = (byte) 233;
    sourceArray2[31 /*0x1F*/] = (byte) 89;
    sourceArray2[9] = (byte) 165;
    sourceArray2[30] = (byte) 96 /*0x60*/;
    sourceArray2[45] = (byte) 183;
    sourceArray2[19] = (byte) 115;
    sourceArray2[1] = (byte) 133;
    sourceArray2[27] = (byte) 197;
    sourceArray2[15] = (byte) 201;
    sourceArray2[33] = (byte) 226;
    sourceArray2[24] = (byte) 239;
    sourceArray2[39] = (byte) 115;
    sourceArray2[26] = (byte) 56;
    sourceArray2[10] = (byte) 105;
    sourceArray2[41] = (byte) 214;
    sourceArray2[29] = (byte) 60;
    sourceArray2[11] = (byte) 44;
    sourceArray2[0] = (byte) 243;
    sourceArray2[32 /*0x20*/] = (byte) 224 /*0xE0*/;
    sourceArray2[18] = (byte) 78;
    sourceArray2[34] = (byte) 201;
    sourceArray2[20] = (byte) 20;
    sourceArray2[36] = (byte) 46;
    sourceArray2[37] = (byte) 162;
    sourceArray2[38] = (byte) 135;
    sourceArray2[47] = (byte) 152;
    sourceArray2[40] = (byte) 100;
    sourceArray2[28] = (byte) 250;
    sourceArray2[42] = (byte) 91;
    sourceArray2[43] = (byte) 155;
    sourceArray2[17] = (byte) 41;
    sourceArray2[21] = (byte) 230;
    sourceArray2[46] = (byte) 124;
    sourceArray2[5] = (byte) 38;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12753(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 86,
      (byte) 57,
      (byte) 95,
      (byte) 162,
      (byte) 153,
      (byte) 119,
      (byte) 63 /*0x3F*/,
      (byte) 151,
      (byte) 58,
      (byte) 17,
      (byte) 37,
      (byte) 27,
      (byte) 44,
      (byte) 81,
      (byte) 150,
      (byte) 252,
      (byte) 181,
      (byte) 32 /*0x20*/,
      (byte) 237,
      (byte) 168,
      (byte) 33,
      (byte) 54,
      (byte) 94,
      (byte) 234,
      (byte) 226,
      (byte) 225,
      (byte) 190,
      (byte) 204,
      (byte) 18,
      (byte) 187,
      (byte) 34,
      (byte) 220,
      (byte) 96 /*0x60*/,
      (byte) 96 /*0x60*/,
      (byte) 41,
      (byte) 91,
      (byte) 145,
      (byte) 108,
      (byte) 26,
      (byte) 174,
      (byte) 91,
      (byte) 239,
      (byte) 25,
      (byte) 44,
      (byte) 132,
      (byte) 56,
      (byte) 217,
      (byte) 131
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 59;
    sourceArray2[1] = (byte) 53;
    sourceArray2[2] = (byte) 4;
    sourceArray2[3] = (byte) 189;
    sourceArray2[4] = (byte) 124;
    sourceArray2[47] = (byte) 83;
    sourceArray2[21] = (byte) 219;
    sourceArray2[0] = (byte) 170;
    sourceArray2[27] = (byte) 57;
    sourceArray2[9] = (byte) 27;
    sourceArray2[10] = (byte) 183;
    sourceArray2[24] = (byte) 145;
    sourceArray2[7] = (byte) 25;
    sourceArray2[17] = (byte) 4;
    sourceArray2[19] = (byte) 79;
    sourceArray2[15] = (byte) 6;
    sourceArray2[16 /*0x10*/] = (byte) 205;
    sourceArray2[23] = (byte) 42;
    sourceArray2[11] = (byte) 236;
    sourceArray2[5] = (byte) 153;
    sourceArray2[14] = (byte) 245;
    sourceArray2[18] = (byte) 137;
    sourceArray2[22] = (byte) 238;
    sourceArray2[32 /*0x20*/] = (byte) 233;
    sourceArray2[6] = (byte) 153;
    sourceArray2[25] = (byte) 5;
    sourceArray2[26] = (byte) 224 /*0xE0*/;
    sourceArray2[42] = (byte) 239;
    sourceArray2[40] = (byte) 81;
    sourceArray2[29] = (byte) 53;
    sourceArray2[31 /*0x1F*/] = (byte) 132;
    sourceArray2[34] = (byte) 101;
    sourceArray2[12] = (byte) 0;
    sourceArray2[35] = (byte) 210;
    sourceArray2[13] = (byte) 61;
    sourceArray2[41] = (byte) 110;
    sourceArray2[28] = (byte) 252;
    sourceArray2[37] = (byte) 132;
    sourceArray2[38] = (byte) 53;
    sourceArray2[39] = (byte) 236;
    sourceArray2[43] = (byte) 16 /*0x10*/;
    sourceArray2[8] = (byte) 91;
    sourceArray2[33] = (byte) 252;
    sourceArray2[30] = (byte) 75;
    sourceArray2[44] = (byte) 192 /*0xC0*/;
    sourceArray2[45] = (byte) 248;
    sourceArray2[36] = (byte) 168;
    sourceArray2[46] = (byte) 159;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12754(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[14] = (byte) 126;
    sourceArray1[1] = (byte) 91;
    sourceArray1[2] = (byte) 198;
    sourceArray1[3] = (byte) 27;
    sourceArray1[30] = (byte) 132;
    sourceArray1[5] = (byte) 133;
    sourceArray1[44] = (byte) 216;
    sourceArray1[7] = (byte) 105;
    sourceArray1[24] = (byte) 77;
    sourceArray1[15] = (byte) 167;
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[11] = (byte) 14;
    sourceArray1[42] = (byte) 33;
    sourceArray1[13] = (byte) 175;
    sourceArray1[20] = (byte) 15;
    sourceArray1[43] = (byte) 6;
    sourceArray1[27] = (byte) 74;
    sourceArray1[36] = (byte) 4;
    sourceArray1[40] = (byte) 253;
    sourceArray1[19] = (byte) 250;
    sourceArray1[45] = (byte) 14;
    sourceArray1[21] = (byte) 164;
    sourceArray1[22] = (byte) 31 /*0x1F*/;
    sourceArray1[39] = (byte) 18;
    sourceArray1[8] = (byte) 6;
    sourceArray1[25] = (byte) 155;
    sourceArray1[12] = (byte) 22;
    sourceArray1[37] = (byte) 113;
    sourceArray1[28] = (byte) 100;
    sourceArray1[18] = (byte) 87;
    sourceArray1[29] = (byte) 200;
    sourceArray1[23] = (byte) 185;
    sourceArray1[32 /*0x20*/] = (byte) 42;
    sourceArray1[33] = (byte) 43;
    sourceArray1[34] = (byte) 251;
    sourceArray1[35] = (byte) 223;
    sourceArray1[26] = (byte) 161;
    sourceArray1[38] = (byte) 60;
    sourceArray1[4] = (byte) 27;
    sourceArray1[6] = (byte) 79;
    sourceArray1[17] = (byte) 39;
    sourceArray1[0] = (byte) 12;
    sourceArray1[9] = (byte) 2;
    sourceArray1[47] = (byte) 179;
    sourceArray1[41] = (byte) 66;
    sourceArray1[16 /*0x10*/] = (byte) 248;
    sourceArray1[46] = (byte) 150;
    sourceArray1[10] = (byte) 168;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 66;
    sourceArray2[1] = (byte) 81;
    sourceArray2[40] = (byte) 242;
    sourceArray2[3] = (byte) 147;
    sourceArray2[8] = (byte) 189;
    sourceArray2[5] = (byte) 114;
    sourceArray2[6] = (byte) 210;
    sourceArray2[7] = (byte) 214;
    sourceArray2[20] = (byte) 94;
    sourceArray2[38] = (byte) 203;
    sourceArray2[10] = (byte) 76;
    sourceArray2[11] = (byte) 187;
    sourceArray2[25] = (byte) 83;
    sourceArray2[23] = (byte) 117;
    sourceArray2[43] = (byte) 14;
    sourceArray2[35] = (byte) 54;
    sourceArray2[12] = (byte) 146;
    sourceArray2[32 /*0x20*/] = (byte) 193;
    sourceArray2[47] = (byte) 55;
    sourceArray2[17] = (byte) 26;
    sourceArray2[42] = (byte) 188;
    sourceArray2[16 /*0x10*/] = (byte) 220;
    sourceArray2[22] = (byte) 39;
    sourceArray2[28] = (byte) 79;
    sourceArray2[15] = (byte) 197;
    sourceArray2[0] = (byte) 174;
    sourceArray2[26] = (byte) 59;
    sourceArray2[27] = (byte) 179;
    sourceArray2[30] = (byte) 10;
    sourceArray2[29] = (byte) 73;
    sourceArray2[14] = (byte) 76;
    sourceArray2[31 /*0x1F*/] = (byte) 251;
    sourceArray2[37] = (byte) 226;
    sourceArray2[33] = (byte) 9;
    sourceArray2[34] = (byte) 10;
    sourceArray2[2] = (byte) 104;
    sourceArray2[36] = (byte) 37;
    sourceArray2[24] = (byte) 150;
    sourceArray2[18] = (byte) 222;
    sourceArray2[39] = (byte) 23;
    sourceArray2[41] = (byte) 131;
    sourceArray2[13] = (byte) 46;
    sourceArray2[4] = (byte) 204;
    sourceArray2[46] = (byte) 100;
    sourceArray2[44] = (byte) 196;
    sourceArray2[45] = (byte) 100;
    sourceArray2[9] = (byte) 250;
    sourceArray2[21] = (byte) 51;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12755(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[12] = (byte) 87;
    sourceArray1[1] = (byte) 199;
    sourceArray1[2] = (byte) 91;
    sourceArray1[3] = (byte) 252;
    sourceArray1[35] = (byte) 60;
    sourceArray1[5] = (byte) 128 /*0x80*/;
    sourceArray1[6] = (byte) 45;
    sourceArray1[11] = (byte) 7;
    sourceArray1[14] = (byte) 63 /*0x3F*/;
    sourceArray1[39] = (byte) 59;
    sourceArray1[10] = (byte) 242;
    sourceArray1[7] = (byte) 126;
    sourceArray1[28] = (byte) 16 /*0x10*/;
    sourceArray1[34] = (byte) 221;
    sourceArray1[41] = (byte) 219;
    sourceArray1[19] = (byte) 119;
    sourceArray1[16 /*0x10*/] = (byte) 68;
    sourceArray1[36] = (byte) 51;
    sourceArray1[18] = (byte) 111;
    sourceArray1[43] = (byte) 28;
    sourceArray1[20] = (byte) 32 /*0x20*/;
    sourceArray1[21] = (byte) 7;
    sourceArray1[22] = (byte) 53;
    sourceArray1[15] = (byte) 17;
    sourceArray1[25] = (byte) 222;
    sourceArray1[23] = (byte) 92;
    sourceArray1[27] = (byte) 87;
    sourceArray1[4] = (byte) 29;
    sourceArray1[26] = (byte) 55;
    sourceArray1[29] = (byte) 113;
    sourceArray1[40] = (byte) 232;
    sourceArray1[31 /*0x1F*/] = (byte) 223;
    sourceArray1[0] = (byte) 56;
    sourceArray1[33] = (byte) 236;
    sourceArray1[38] = (byte) 114;
    sourceArray1[46] = (byte) 53;
    sourceArray1[44] = (byte) 146;
    sourceArray1[8] = (byte) 223;
    sourceArray1[30] = (byte) 122;
    sourceArray1[32 /*0x20*/] = (byte) 205;
    sourceArray1[9] = (byte) 222;
    sourceArray1[37] = (byte) 163;
    sourceArray1[42] = (byte) 15;
    sourceArray1[17] = (byte) 147;
    sourceArray1[24] = (byte) 124;
    sourceArray1[45] = (byte) 99;
    sourceArray1[13] = (byte) 138;
    sourceArray1[47] = (byte) 226;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[29] = (byte) 229;
    sourceArray2[1] = (byte) 151;
    sourceArray2[24] = (byte) 211;
    sourceArray2[33] = (byte) 21;
    sourceArray2[4] = (byte) 4;
    sourceArray2[2] = (byte) 232;
    sourceArray2[6] = (byte) 21;
    sourceArray2[37] = (byte) 103;
    sourceArray2[8] = (byte) 220;
    sourceArray2[11] = (byte) 241;
    sourceArray2[9] = (byte) 30;
    sourceArray2[41] = (byte) 128 /*0x80*/;
    sourceArray2[12] = (byte) 1;
    sourceArray2[7] = (byte) 85;
    sourceArray2[16 /*0x10*/] = (byte) 224 /*0xE0*/;
    sourceArray2[47] = (byte) 119;
    sourceArray2[46] = (byte) 20;
    sourceArray2[39] = (byte) 186;
    sourceArray2[18] = (byte) 96 /*0x60*/;
    sourceArray2[19] = (byte) 24;
    sourceArray2[20] = (byte) 61;
    sourceArray2[21] = (byte) 52;
    sourceArray2[15] = (byte) 209;
    sourceArray2[10] = (byte) 77;
    sourceArray2[43] = (byte) 219;
    sourceArray2[25] = (byte) 61;
    sourceArray2[26] = (byte) 169;
    sourceArray2[22] = (byte) 150;
    sourceArray2[28] = (byte) 26;
    sourceArray2[17] = (byte) 24;
    sourceArray2[30] = (byte) 165;
    sourceArray2[23] = (byte) 51;
    sourceArray2[32 /*0x20*/] = (byte) 179;
    sourceArray2[0] = (byte) 221;
    sourceArray2[34] = (byte) 116;
    sourceArray2[35] = (byte) 241;
    sourceArray2[36] = (byte) 22;
    sourceArray2[42] = (byte) 221;
    sourceArray2[38] = (byte) 20;
    sourceArray2[40] = (byte) 43;
    sourceArray2[13] = (byte) 143;
    sourceArray2[14] = (byte) 162;
    sourceArray2[27] = (byte) 160 /*0xA0*/;
    sourceArray2[5] = (byte) 249;
    sourceArray2[44] = (byte) 147;
    sourceArray2[45] = (byte) 187;
    sourceArray2[3] = (byte) 242;
    sourceArray2[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12756(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 253,
      (byte) 46,
      (byte) 4,
      (byte) 60,
      (byte) 225,
      (byte) 241,
      (byte) 28,
      (byte) 42,
      (byte) 53,
      (byte) 7,
      (byte) 132,
      (byte) 76,
      (byte) 223,
      (byte) 229,
      (byte) 147,
      (byte) 4,
      (byte) 225,
      (byte) 25,
      (byte) 237,
      (byte) 117,
      (byte) 184,
      (byte) 213,
      (byte) 82,
      (byte) 145,
      (byte) 39,
      (byte) 21,
      (byte) 49,
      (byte) 236,
      (byte) 178,
      (byte) 139,
      (byte) 40,
      (byte) 171,
      (byte) 191,
      (byte) 79,
      (byte) 68,
      (byte) 77,
      (byte) 211,
      (byte) 26,
      (byte) 126,
      (byte) 87,
      (byte) 211,
      (byte) 179,
      (byte) 233,
      (byte) 137,
      (byte) 82,
      (byte) 242,
      (byte) 226,
      (byte) 118
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[20] = (byte) 224 /*0xE0*/;
    sourceArray2[0] = (byte) 200;
    sourceArray2[46] = (byte) 37;
    sourceArray2[36] = (byte) 142;
    sourceArray2[4] = (byte) 36;
    sourceArray2[5] = (byte) 172;
    sourceArray2[6] = (byte) 105;
    sourceArray2[33] = (byte) 165;
    sourceArray2[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    sourceArray2[34] = (byte) 183;
    sourceArray2[47] = (byte) 110;
    sourceArray2[11] = (byte) 88;
    sourceArray2[24] = (byte) 187;
    sourceArray2[13] = (byte) 107;
    sourceArray2[14] = (byte) 63 /*0x3F*/;
    sourceArray2[15] = (byte) 64 /*0x40*/;
    sourceArray2[8] = (byte) 114;
    sourceArray2[12] = (byte) 153;
    sourceArray2[18] = (byte) 225;
    sourceArray2[23] = (byte) 40;
    sourceArray2[26] = (byte) 45;
    sourceArray2[21] = (byte) 73;
    sourceArray2[30] = (byte) 143;
    sourceArray2[10] = (byte) 20;
    sourceArray2[38] = (byte) 148;
    sourceArray2[25] = (byte) 84;
    sourceArray2[3] = (byte) 17;
    sourceArray2[27] = (byte) 93;
    sourceArray2[2] = (byte) 27;
    sourceArray2[29] = (byte) 131;
    sourceArray2[17] = (byte) 112 /*0x70*/;
    sourceArray2[44] = (byte) 70;
    sourceArray2[1] = (byte) 50;
    sourceArray2[9] = (byte) 181;
    sourceArray2[45] = (byte) 24;
    sourceArray2[35] = (byte) 200;
    sourceArray2[42] = (byte) 243;
    sourceArray2[37] = (byte) 248;
    sourceArray2[16 /*0x10*/] = (byte) 39;
    sourceArray2[39] = (byte) 71;
    sourceArray2[40] = (byte) 43;
    sourceArray2[41] = (byte) 95;
    sourceArray2[28] = (byte) 90;
    sourceArray2[32 /*0x20*/] = (byte) 164;
    sourceArray2[43] = (byte) 219;
    sourceArray2[22] = (byte) 27;
    sourceArray2[19] = (byte) 112 /*0x70*/;
    sourceArray2[7] = (byte) 102;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12757(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 82,
      (byte) 213,
      (byte) 68,
      (byte) 184,
      (byte) 203,
      (byte) 124,
      (byte) 123,
      (byte) 165,
      (byte) 223,
      (byte) 59,
      (byte) 18,
      (byte) 52,
      (byte) 223,
      (byte) 152,
      (byte) 4,
      (byte) 206,
      (byte) 176 /*0xB0*/,
      (byte) 178,
      (byte) 206,
      (byte) 62,
      (byte) 20,
      (byte) 37,
      (byte) 132,
      (byte) 212,
      (byte) 209,
      (byte) 194,
      (byte) 232,
      (byte) 253,
      (byte) 254,
      (byte) 202,
      (byte) 173,
      (byte) 31 /*0x1F*/,
      (byte) 197,
      (byte) 207,
      (byte) 225,
      (byte) 229,
      (byte) 28,
      (byte) 15,
      (byte) 44,
      (byte) 148,
      (byte) 31 /*0x1F*/,
      (byte) 210,
      (byte) 139,
      (byte) 224 /*0xE0*/,
      (byte) 94,
      (byte) 43,
      (byte) 37,
      (byte) 26
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 115,
      (byte) 237,
      (byte) 15,
      (byte) 198,
      (byte) 183,
      (byte) 179,
      (byte) 42,
      (byte) 29,
      (byte) 232,
      (byte) 243,
      (byte) 169,
      (byte) 35,
      (byte) 171,
      (byte) 252,
      (byte) 150,
      (byte) 182,
      (byte) 3,
      (byte) 12,
      (byte) 112 /*0x70*/,
      (byte) 219,
      (byte) 56,
      (byte) 59,
      (byte) 200,
      (byte) 46,
      (byte) 90,
      (byte) 243,
      (byte) 72,
      (byte) 244,
      (byte) 168,
      (byte) 64 /*0x40*/,
      (byte) 38,
      (byte) 153,
      (byte) 140,
      (byte) 126,
      (byte) 192 /*0xC0*/,
      (byte) 39,
      (byte) 201,
      (byte) 78,
      (byte) 237,
      (byte) 121,
      (byte) 76,
      (byte) 46,
      (byte) 15,
      (byte) 213,
      (byte) 93,
      (byte) 41,
      (byte) 117,
      (byte) 53
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12758(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 58,
      (byte) 81,
      (byte) 44,
      (byte) 80 /*0x50*/,
      (byte) 112 /*0x70*/,
      (byte) 230,
      (byte) 9,
      (byte) 147,
      (byte) 125,
      (byte) 167,
      (byte) 20,
      (byte) 142,
      (byte) 243,
      (byte) 110,
      (byte) 15,
      (byte) 182,
      (byte) 30,
      (byte) 177,
      (byte) 160 /*0xA0*/,
      (byte) 133,
      (byte) 58,
      (byte) 31 /*0x1F*/,
      (byte) 65,
      (byte) 154,
      (byte) 163,
      (byte) 118,
      (byte) 166,
      (byte) 156,
      (byte) 206,
      (byte) 105,
      (byte) 41,
      (byte) 64 /*0x40*/,
      (byte) 32 /*0x20*/,
      (byte) 68,
      (byte) 139,
      (byte) 126,
      (byte) 180,
      (byte) 149,
      (byte) 172,
      (byte) 40,
      (byte) 35,
      (byte) 0,
      (byte) 161,
      (byte) 43,
      (byte) 118,
      (byte) 82,
      (byte) 107,
      (byte) 188
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 79,
      byte.MaxValue,
      (byte) 99,
      (byte) 246,
      (byte) 65,
      (byte) 244,
      (byte) 252,
      (byte) 179,
      (byte) 203,
      (byte) 120,
      (byte) 34,
      (byte) 143,
      (byte) 100,
      (byte) 25,
      (byte) 39,
      (byte) 86,
      (byte) 44,
      (byte) 210,
      (byte) 199,
      (byte) 222,
      (byte) 21,
      (byte) 216,
      (byte) 222,
      (byte) 62,
      (byte) 158,
      (byte) 44,
      (byte) 113,
      (byte) 122,
      (byte) 232,
      (byte) 121,
      (byte) 127 /*0x7F*/,
      (byte) 34,
      (byte) 253,
      (byte) 223,
      (byte) 27,
      (byte) 93,
      (byte) 222,
      (byte) 185,
      (byte) 127 /*0x7F*/,
      (byte) 190,
      (byte) 213,
      (byte) 180,
      (byte) 57,
      (byte) 238,
      (byte) 100,
      (byte) 158,
      (byte) 228,
      (byte) 169
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12759(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 6,
      (byte) 161,
      (byte) 26,
      (byte) 16 /*0x10*/,
      (byte) 146,
      (byte) 172,
      (byte) 249,
      (byte) 63 /*0x3F*/,
      (byte) 108,
      (byte) 197,
      (byte) 20,
      (byte) 126,
      (byte) 212,
      (byte) 109,
      (byte) 175,
      (byte) 226,
      (byte) 145,
      (byte) 2,
      (byte) 136,
      (byte) 4,
      (byte) 119,
      (byte) 98,
      (byte) 57,
      (byte) 125,
      (byte) 22,
      (byte) 35,
      (byte) 81,
      (byte) 10,
      (byte) 234,
      (byte) 134,
      (byte) 106,
      (byte) 244,
      (byte) 5,
      (byte) 86,
      (byte) 26,
      (byte) 163,
      (byte) 113,
      (byte) 110,
      (byte) 33,
      (byte) 233,
      (byte) 179,
      (byte) 232,
      (byte) 183,
      (byte) 59,
      (byte) 141,
      (byte) 173,
      (byte) 162,
      (byte) 229
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[0] = (byte) 67;
    sourceArray2[1] = (byte) 226;
    sourceArray2[2] = (byte) 3;
    sourceArray2[3] = (byte) 132;
    sourceArray2[22] = (byte) 175;
    sourceArray2[5] = (byte) 217;
    sourceArray2[6] = (byte) 141;
    sourceArray2[7] = (byte) 126;
    sourceArray2[42] = (byte) 249;
    sourceArray2[14] = (byte) 170;
    sourceArray2[25] = (byte) 225;
    sourceArray2[11] = (byte) 169;
    sourceArray2[12] = (byte) 206;
    sourceArray2[29] = (byte) 220;
    sourceArray2[20] = (byte) 16 /*0x10*/;
    sourceArray2[39] = (byte) 217;
    sourceArray2[32 /*0x20*/] = (byte) 180;
    sourceArray2[10] = (byte) 72;
    sourceArray2[18] = (byte) 50;
    sourceArray2[19] = (byte) 108;
    sourceArray2[27] = (byte) 227;
    sourceArray2[21] = (byte) 159;
    sourceArray2[46] = (byte) 10;
    sourceArray2[8] = (byte) 149;
    sourceArray2[24] = (byte) 32 /*0x20*/;
    sourceArray2[35] = (byte) 235;
    sourceArray2[26] = (byte) 1;
    sourceArray2[28] = (byte) 249;
    sourceArray2[15] = (byte) 152;
    sourceArray2[41] = (byte) 204;
    sourceArray2[13] = (byte) 83;
    sourceArray2[16 /*0x10*/] = (byte) 205;
    sourceArray2[17] = (byte) 125;
    sourceArray2[33] = (byte) 211;
    sourceArray2[34] = (byte) 248;
    sourceArray2[30] = (byte) 245;
    sourceArray2[36] = (byte) 249;
    sourceArray2[37] = (byte) 214;
    sourceArray2[23] = (byte) 59;
    sourceArray2[45] = (byte) 213;
    sourceArray2[9] = (byte) 101;
    sourceArray2[31 /*0x1F*/] = (byte) 92;
    sourceArray2[4] = (byte) 234;
    sourceArray2[43] = (byte) 46;
    sourceArray2[40] = (byte) 174;
    sourceArray2[38] = (byte) 245;
    sourceArray2[44] = (byte) 150;
    sourceArray2[47] = (byte) 198;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12760(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 36,
      (byte) 146,
      (byte) 176 /*0xB0*/,
      (byte) 104,
      (byte) 96 /*0x60*/,
      (byte) 128 /*0x80*/,
      (byte) 77,
      (byte) 205,
      (byte) 227,
      (byte) 248,
      (byte) 104,
      (byte) 172,
      (byte) 145,
      (byte) 236,
      (byte) 239,
      (byte) 159,
      (byte) 69,
      (byte) 227,
      (byte) 80 /*0x50*/,
      (byte) 23,
      (byte) 145,
      (byte) 152,
      (byte) 219,
      (byte) 208 /*0xD0*/,
      (byte) 157,
      (byte) 175,
      (byte) 45,
      (byte) 16 /*0x10*/,
      (byte) 141,
      (byte) 251,
      (byte) 217,
      (byte) 30,
      (byte) 130,
      (byte) 51,
      (byte) 93,
      (byte) 217,
      (byte) 158,
      (byte) 168,
      (byte) 161,
      (byte) 136,
      (byte) 187,
      (byte) 25,
      (byte) 1,
      (byte) 125,
      (byte) 210,
      (byte) 39,
      (byte) 227,
      (byte) 198
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 67,
      (byte) 227,
      (byte) 197,
      (byte) 76,
      (byte) 252,
      (byte) 11,
      (byte) 139,
      (byte) 83,
      (byte) 25,
      (byte) 31 /*0x1F*/,
      (byte) 187,
      (byte) 39,
      (byte) 1,
      (byte) 252,
      (byte) 68,
      (byte) 125,
      (byte) 83,
      (byte) 69,
      (byte) 212,
      (byte) 80 /*0x50*/,
      (byte) 195,
      (byte) 235,
      (byte) 58,
      (byte) 41,
      (byte) 105,
      (byte) 229,
      (byte) 81,
      (byte) 247,
      (byte) 18,
      (byte) 74,
      (byte) 172,
      (byte) 22,
      (byte) 16 /*0x10*/,
      (byte) 22,
      (byte) 247,
      (byte) 148,
      (byte) 202,
      (byte) 227,
      (byte) 192 /*0xC0*/,
      (byte) 88,
      (byte) 64 /*0x40*/,
      (byte) 245,
      (byte) 99,
      (byte) 246,
      (byte) 53,
      (byte) 31 /*0x1F*/,
      (byte) 187
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12761(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 150;
    sourceArray1[38] = (byte) 190;
    sourceArray1[43] = (byte) 217;
    sourceArray1[41] = (byte) 53;
    sourceArray1[2] = (byte) 139;
    sourceArray1[15] = (byte) 143;
    sourceArray1[6] = (byte) 177;
    sourceArray1[7] = (byte) 83;
    sourceArray1[13] = (byte) 42;
    sourceArray1[4] = (byte) 224 /*0xE0*/;
    sourceArray1[10] = (byte) 231;
    sourceArray1[11] = (byte) 30;
    sourceArray1[12] = (byte) 74;
    sourceArray1[0] = (byte) 86;
    sourceArray1[14] = (byte) 93;
    sourceArray1[19] = (byte) 66;
    sourceArray1[16 /*0x10*/] = (byte) 21;
    sourceArray1[17] = (byte) 1;
    sourceArray1[34] = (byte) 43;
    sourceArray1[42] = (byte) 126;
    sourceArray1[20] = (byte) 200;
    sourceArray1[18] = (byte) 156;
    sourceArray1[22] = (byte) 119;
    sourceArray1[3] = (byte) 182;
    sourceArray1[27] = (byte) 204;
    sourceArray1[25] = (byte) 253;
    sourceArray1[1] = (byte) 163;
    sourceArray1[24] = (byte) 94;
    sourceArray1[31 /*0x1F*/] = (byte) 173;
    sourceArray1[29] = (byte) 113;
    sourceArray1[45] = (byte) 94;
    sourceArray1[33] = (byte) 142;
    sourceArray1[32 /*0x20*/] = (byte) 236;
    sourceArray1[21] = (byte) 91;
    sourceArray1[8] = (byte) 60;
    sourceArray1[35] = (byte) 144 /*0x90*/;
    sourceArray1[36] = (byte) 209;
    sourceArray1[37] = (byte) 34;
    sourceArray1[44] = (byte) 25;
    sourceArray1[39] = (byte) 221;
    sourceArray1[40] = (byte) 239;
    sourceArray1[5] = (byte) 202;
    sourceArray1[28] = (byte) 103;
    sourceArray1[30] = (byte) 105;
    sourceArray1[47] = (byte) 252;
    sourceArray1[26] = (byte) 118;
    sourceArray1[9] = (byte) 63 /*0x3F*/;
    sourceArray1[46] = (byte) 135;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 210;
    sourceArray2[14] = (byte) 175;
    sourceArray2[15] = (byte) 215;
    sourceArray2[3] = (byte) 166;
    sourceArray2[4] = (byte) 33;
    sourceArray2[5] = (byte) 236;
    sourceArray2[6] = (byte) 170;
    sourceArray2[40] = (byte) 135;
    sourceArray2[37] = (byte) 110;
    sourceArray2[9] = (byte) 219;
    sourceArray2[10] = (byte) 231;
    sourceArray2[44] = (byte) 10;
    sourceArray2[12] = (byte) 23;
    sourceArray2[13] = (byte) 91;
    sourceArray2[30] = (byte) 77;
    sourceArray2[16 /*0x10*/] = (byte) 78;
    sourceArray2[11] = (byte) 238;
    sourceArray2[45] = (byte) 25;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[26] = (byte) 117;
    sourceArray2[22] = (byte) 25;
    sourceArray2[21] = (byte) 242;
    sourceArray2[1] = (byte) 242;
    sourceArray2[23] = (byte) 68;
    sourceArray2[38] = (byte) 130;
    sourceArray2[25] = (byte) 114;
    sourceArray2[7] = (byte) 100;
    sourceArray2[27] = (byte) 145;
    sourceArray2[20] = (byte) 151;
    sourceArray2[29] = (byte) 227;
    sourceArray2[2] = (byte) 76;
    sourceArray2[28] = (byte) 38;
    sourceArray2[32 /*0x20*/] = (byte) 114;
    sourceArray2[17] = (byte) 172;
    sourceArray2[34] = (byte) 145;
    sourceArray2[35] = (byte) 108;
    sourceArray2[36] = (byte) 105;
    sourceArray2[46] = (byte) 188;
    sourceArray2[42] = (byte) 142;
    sourceArray2[8] = (byte) 68;
    sourceArray2[33] = (byte) 214;
    sourceArray2[47] = (byte) 77;
    sourceArray2[39] = (byte) 140;
    sourceArray2[43] = (byte) 180;
    sourceArray2[19] = (byte) 154;
    sourceArray2[0] = (byte) 236;
    sourceArray2[24] = (byte) 230;
    sourceArray2[18] = (byte) 219;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12762(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 175,
      (byte) 107,
      (byte) 142,
      (byte) 208 /*0xD0*/,
      (byte) 114,
      (byte) 160 /*0xA0*/,
      (byte) 57,
      (byte) 214,
      (byte) 45,
      (byte) 80 /*0x50*/,
      (byte) 149,
      (byte) 107,
      (byte) 121,
      (byte) 190,
      (byte) 17,
      (byte) 236,
      (byte) 245,
      (byte) 242,
      (byte) 218,
      (byte) 134,
      (byte) 195,
      (byte) 92,
      (byte) 17,
      (byte) 1,
      (byte) 145,
      (byte) 42,
      (byte) 54,
      (byte) 11,
      (byte) 99,
      (byte) 189,
      (byte) 145,
      (byte) 125,
      (byte) 185,
      (byte) 248,
      (byte) 132,
      (byte) 219,
      (byte) 186,
      (byte) 87,
      (byte) 40,
      (byte) 253,
      (byte) 153,
      (byte) 211,
      (byte) 79,
      (byte) 60,
      (byte) 35,
      (byte) 230,
      (byte) 236,
      (byte) 73
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 73,
      (byte) 239,
      (byte) 78,
      (byte) 227,
      (byte) 35,
      (byte) 196,
      (byte) 127 /*0x7F*/,
      (byte) 227,
      (byte) 101,
      byte.MaxValue,
      (byte) 206,
      (byte) 7,
      (byte) 82,
      (byte) 91,
      (byte) 221,
      (byte) 14,
      (byte) 232,
      (byte) 32 /*0x20*/,
      (byte) 51,
      (byte) 36,
      (byte) 48 /*0x30*/,
      (byte) 129,
      (byte) 64 /*0x40*/,
      (byte) 89,
      (byte) 240 /*0xF0*/,
      (byte) 176 /*0xB0*/,
      (byte) 155,
      (byte) 28,
      (byte) 151,
      (byte) 42,
      (byte) 140,
      (byte) 96 /*0x60*/,
      (byte) 230,
      (byte) 26,
      (byte) 91,
      (byte) 121,
      (byte) 33,
      (byte) 78,
      (byte) 97,
      (byte) 1,
      (byte) 78,
      (byte) 60,
      (byte) 97,
      (byte) 30,
      (byte) 3,
      (byte) 253,
      (byte) 39,
      (byte) 152
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12763(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[4] = (byte) 141;
    sourceArray1[22] = (byte) 239;
    sourceArray1[6] = (byte) 71;
    sourceArray1[36] = (byte) 49;
    sourceArray1[18] = (byte) 157;
    sourceArray1[5] = (byte) 63 /*0x3F*/;
    sourceArray1[41] = (byte) 148;
    sourceArray1[7] = (byte) 105;
    sourceArray1[8] = (byte) 46;
    sourceArray1[9] = (byte) 115;
    sourceArray1[31 /*0x1F*/] = (byte) 161;
    sourceArray1[1] = (byte) 170;
    sourceArray1[40] = (byte) 224 /*0xE0*/;
    sourceArray1[12] = (byte) 23;
    sourceArray1[44] = (byte) 224 /*0xE0*/;
    sourceArray1[29] = (byte) 54;
    sourceArray1[30] = (byte) 189;
    sourceArray1[27] = (byte) 62;
    sourceArray1[14] = (byte) 128 /*0x80*/;
    sourceArray1[19] = (byte) 202;
    sourceArray1[20] = (byte) 220;
    sourceArray1[37] = (byte) 129;
    sourceArray1[32 /*0x20*/] = (byte) 173;
    sourceArray1[23] = (byte) 12;
    sourceArray1[24] = (byte) 212;
    sourceArray1[25] = (byte) 221;
    sourceArray1[2] = (byte) 83;
    sourceArray1[26] = (byte) 170;
    sourceArray1[28] = (byte) 237;
    sourceArray1[17] = (byte) 49;
    sourceArray1[3] = (byte) 90;
    sourceArray1[35] = (byte) 78;
    sourceArray1[21] = (byte) 236;
    sourceArray1[33] = (byte) 243;
    sourceArray1[34] = (byte) 207;
    sourceArray1[0] = (byte) 234;
    sourceArray1[16 /*0x10*/] = (byte) 54;
    sourceArray1[13] = (byte) 178;
    sourceArray1[38] = (byte) 45;
    sourceArray1[39] = (byte) 24;
    sourceArray1[42] = (byte) 107;
    sourceArray1[11] = (byte) 191;
    sourceArray1[15] = (byte) 57;
    sourceArray1[43] = (byte) 96 /*0x60*/;
    sourceArray1[10] = (byte) 111;
    sourceArray1[45] = (byte) 125;
    sourceArray1[46] = (byte) 11;
    sourceArray1[47] = (byte) 52;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 70;
    sourceArray2[24] = (byte) 71;
    sourceArray2[31 /*0x1F*/] = (byte) 136;
    sourceArray2[3] = (byte) 242;
    sourceArray2[8] = (byte) 204;
    sourceArray2[4] = (byte) 25;
    sourceArray2[14] = (byte) 215;
    sourceArray2[7] = (byte) 166;
    sourceArray2[15] = (byte) 45;
    sourceArray2[0] = (byte) 89;
    sourceArray2[34] = (byte) 42;
    sourceArray2[11] = (byte) 58;
    sourceArray2[39] = (byte) 25;
    sourceArray2[23] = (byte) 212;
    sourceArray2[44] = (byte) 88;
    sourceArray2[42] = (byte) 195;
    sourceArray2[35] = (byte) 180;
    sourceArray2[5] = (byte) 188;
    sourceArray2[6] = (byte) 191;
    sourceArray2[19] = (byte) 9;
    sourceArray2[20] = (byte) 226;
    sourceArray2[21] = (byte) 130;
    sourceArray2[22] = (byte) 210;
    sourceArray2[45] = (byte) 233;
    sourceArray2[16 /*0x10*/] = (byte) 210;
    sourceArray2[25] = (byte) 144 /*0x90*/;
    sourceArray2[26] = (byte) 222;
    sourceArray2[27] = (byte) 92;
    sourceArray2[28] = (byte) 149;
    sourceArray2[2] = (byte) 171;
    sourceArray2[33] = (byte) 241;
    sourceArray2[12] = (byte) 136;
    sourceArray2[32 /*0x20*/] = (byte) 228;
    sourceArray2[29] = (byte) 127 /*0x7F*/;
    sourceArray2[1] = (byte) 137;
    sourceArray2[13] = (byte) 3;
    sourceArray2[30] = (byte) 178;
    sourceArray2[37] = (byte) 95;
    sourceArray2[10] = (byte) 206;
    sourceArray2[46] = (byte) 231;
    sourceArray2[40] = (byte) 249;
    sourceArray2[18] = (byte) 34;
    sourceArray2[17] = (byte) 82;
    sourceArray2[43] = (byte) 34;
    sourceArray2[38] = (byte) 189;
    sourceArray2[36] = (byte) 146;
    sourceArray2[41] = (byte) 124;
    sourceArray2[47] = (byte) 176 /*0xB0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[39];
    byte[] response2 = new byte[39];
    Array.Copy((Array) sc_12743.sspq, 73, (Array) numArray2, 0, 39);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12743.sspr, 73, (Array) numArray2, 0, 39);
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

  internal static int ssp_appserver_12764(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 252,
      (byte) 226,
      (byte) 228,
      byte.MaxValue,
      (byte) 235,
      (byte) 127 /*0x7F*/,
      (byte) 240 /*0xF0*/,
      (byte) 50,
      (byte) 70,
      (byte) 169,
      (byte) 233,
      (byte) 32 /*0x20*/,
      (byte) 207,
      (byte) 136,
      (byte) 173,
      (byte) 108,
      (byte) 6,
      (byte) 175,
      (byte) 95,
      (byte) 169,
      (byte) 232,
      (byte) 213,
      (byte) 7,
      (byte) 135,
      (byte) 99,
      (byte) 157,
      (byte) 65,
      (byte) 1,
      (byte) 90,
      (byte) 174,
      (byte) 126,
      (byte) 112 /*0x70*/,
      (byte) 160 /*0xA0*/,
      (byte) 80 /*0x50*/,
      (byte) 71,
      (byte) 15,
      (byte) 20,
      (byte) 156,
      (byte) 222,
      (byte) 50,
      (byte) 163,
      (byte) 77,
      (byte) 211,
      (byte) 86,
      (byte) 207,
      (byte) 22,
      (byte) 124,
      (byte) 100
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 85,
      (byte) 154,
      (byte) 131,
      (byte) 120,
      (byte) 47,
      (byte) 205,
      (byte) 23,
      (byte) 208 /*0xD0*/,
      (byte) 153,
      (byte) 126,
      (byte) 54,
      (byte) 181,
      (byte) 249,
      (byte) 148,
      (byte) 242,
      (byte) 198,
      (byte) 41,
      (byte) 3,
      (byte) 154,
      (byte) 252,
      (byte) 122,
      (byte) 197,
      (byte) 1,
      (byte) 22,
      (byte) 11,
      (byte) 245,
      (byte) 12,
      (byte) 238,
      (byte) 133,
      (byte) 70,
      (byte) 153,
      (byte) 26,
      (byte) 127 /*0x7F*/,
      (byte) 243,
      (byte) 133,
      (byte) 44,
      (byte) 119,
      (byte) 18,
      (byte) 205,
      (byte) 78,
      (byte) 158,
      (byte) 225,
      (byte) 217,
      (byte) 165,
      (byte) 208 /*0xD0*/,
      (byte) 206,
      (byte) 42,
      (byte) 239
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12765(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[17] = (byte) 87;
    sourceArray1[6] = (byte) 77;
    sourceArray1[3] = (byte) 218;
    sourceArray1[24] = (byte) 177;
    sourceArray1[4] = (byte) 92;
    sourceArray1[7] = (byte) 62;
    sourceArray1[29] = (byte) 200;
    sourceArray1[1] = (byte) 200;
    sourceArray1[34] = (byte) 162;
    sourceArray1[23] = (byte) 1;
    sourceArray1[37] = (byte) 52;
    sourceArray1[43] = (byte) 117;
    sourceArray1[12] = (byte) 121;
    sourceArray1[21] = (byte) 5;
    sourceArray1[14] = (byte) 127 /*0x7F*/;
    sourceArray1[36] = (byte) 32 /*0x20*/;
    sourceArray1[16 /*0x10*/] = (byte) 157;
    sourceArray1[11] = (byte) 42;
    sourceArray1[18] = (byte) 105;
    sourceArray1[27] = (byte) 33;
    sourceArray1[8] = (byte) 238;
    sourceArray1[0] = (byte) 253;
    sourceArray1[22] = (byte) 154;
    sourceArray1[41] = (byte) 244;
    sourceArray1[5] = (byte) 156;
    sourceArray1[20] = (byte) 45;
    sourceArray1[26] = (byte) 76;
    sourceArray1[31 /*0x1F*/] = (byte) 68;
    sourceArray1[28] = (byte) 220;
    sourceArray1[30] = (byte) 42;
    sourceArray1[35] = (byte) 135;
    sourceArray1[25] = (byte) 184;
    sourceArray1[32 /*0x20*/] = (byte) 118;
    sourceArray1[33] = (byte) 211;
    sourceArray1[13] = (byte) 127 /*0x7F*/;
    sourceArray1[2] = (byte) 39;
    sourceArray1[9] = (byte) 216;
    sourceArray1[15] = (byte) 65;
    sourceArray1[19] = (byte) 208 /*0xD0*/;
    sourceArray1[39] = byte.MaxValue;
    sourceArray1[40] = (byte) 145;
    sourceArray1[46] = (byte) 85;
    sourceArray1[42] = (byte) 181;
    sourceArray1[38] = (byte) 184;
    sourceArray1[44] = (byte) 59;
    sourceArray1[45] = (byte) 43;
    sourceArray1[10] = (byte) 151;
    sourceArray1[47] = (byte) 151;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[18] = (byte) 161;
    sourceArray2[31 /*0x1F*/] = (byte) 57;
    sourceArray2[12] = (byte) 56;
    sourceArray2[3] = (byte) 154;
    sourceArray2[4] = (byte) 148;
    sourceArray2[40] = (byte) 62;
    sourceArray2[7] = (byte) 3;
    sourceArray2[42] = (byte) 84;
    sourceArray2[8] = (byte) 246;
    sourceArray2[9] = (byte) 130;
    sourceArray2[10] = (byte) 176 /*0xB0*/;
    sourceArray2[1] = (byte) 240 /*0xF0*/;
    sourceArray2[0] = (byte) 149;
    sourceArray2[39] = (byte) 88;
    sourceArray2[25] = (byte) 198;
    sourceArray2[14] = (byte) 245;
    sourceArray2[22] = (byte) 35;
    sourceArray2[38] = (byte) 40;
    sourceArray2[5] = (byte) 115;
    sourceArray2[34] = (byte) 117;
    sourceArray2[20] = (byte) 125;
    sourceArray2[24] = (byte) 127 /*0x7F*/;
    sourceArray2[13] = (byte) 105;
    sourceArray2[23] = (byte) 81;
    sourceArray2[47] = (byte) 122;
    sourceArray2[21] = (byte) 13;
    sourceArray2[17] = (byte) 67;
    sourceArray2[27] = (byte) 218;
    sourceArray2[28] = (byte) 143;
    sourceArray2[29] = (byte) 50;
    sourceArray2[30] = (byte) 8;
    sourceArray2[26] = (byte) 172;
    sourceArray2[32 /*0x20*/] = (byte) 44;
    sourceArray2[33] = (byte) 172;
    sourceArray2[37] = (byte) 206;
    sourceArray2[16 /*0x10*/] = (byte) 81;
    sourceArray2[19] = (byte) 87;
    sourceArray2[2] = (byte) 106;
    sourceArray2[6] = (byte) 73;
    sourceArray2[44] = (byte) 243;
    sourceArray2[15] = (byte) 90;
    sourceArray2[41] = (byte) 9;
    sourceArray2[11] = (byte) 152;
    sourceArray2[43] = (byte) 20;
    sourceArray2[45] = (byte) 180;
    sourceArray2[35] = (byte) 190;
    sourceArray2[46] = (byte) 19;
    sourceArray2[36] = (byte) 6;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[13];
    byte[] response2 = new byte[13];
    Array.Copy((Array) sc_12743.sspq, 112 /*0x70*/, (Array) numArray2, 0, 13);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12743.sspr, 112 /*0x70*/, (Array) numArray2, 0, 13);
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

  internal static int ssp_appserver_12766(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[18] = (byte) 246;
    sourceArray1[1] = (byte) 144 /*0x90*/;
    sourceArray1[2] = (byte) 230;
    sourceArray1[4] = (byte) 129;
    sourceArray1[39] = (byte) 208 /*0xD0*/;
    sourceArray1[5] = (byte) 150;
    sourceArray1[6] = (byte) 141;
    sourceArray1[44] = (byte) 152;
    sourceArray1[41] = (byte) 115;
    sourceArray1[36] = (byte) 27;
    sourceArray1[10] = (byte) 253;
    sourceArray1[11] = (byte) 66;
    sourceArray1[21] = (byte) 123;
    sourceArray1[20] = (byte) 165;
    sourceArray1[14] = (byte) 22;
    sourceArray1[15] = (byte) 48 /*0x30*/;
    sourceArray1[16 /*0x10*/] = (byte) 77;
    sourceArray1[3] = (byte) 170;
    sourceArray1[12] = (byte) 93;
    sourceArray1[0] = (byte) 31 /*0x1F*/;
    sourceArray1[19] = (byte) 182;
    sourceArray1[27] = (byte) 71;
    sourceArray1[29] = (byte) 185;
    sourceArray1[17] = (byte) 137;
    sourceArray1[24] = (byte) 243;
    sourceArray1[8] = (byte) 232;
    sourceArray1[45] = (byte) 40;
    sourceArray1[7] = (byte) 161;
    sourceArray1[28] = (byte) 113;
    sourceArray1[30] = (byte) 215;
    sourceArray1[32 /*0x20*/] = (byte) 106;
    sourceArray1[46] = (byte) 125;
    sourceArray1[31 /*0x1F*/] = (byte) 39;
    sourceArray1[25] = (byte) 3;
    sourceArray1[34] = (byte) 38;
    sourceArray1[35] = (byte) 156;
    sourceArray1[23] = (byte) 43;
    sourceArray1[37] = (byte) 167;
    sourceArray1[38] = (byte) 26;
    sourceArray1[9] = (byte) 243;
    sourceArray1[40] = (byte) 71;
    sourceArray1[33] = (byte) 212;
    sourceArray1[42] = (byte) 117;
    sourceArray1[43] = (byte) 246;
    sourceArray1[13] = (byte) 46;
    sourceArray1[22] = (byte) 188;
    sourceArray1[26] = (byte) 9;
    sourceArray1[47] = (byte) 213;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 86,
      (byte) 125,
      (byte) 148,
      (byte) 160 /*0xA0*/,
      (byte) 163,
      (byte) 151,
      (byte) 96 /*0x60*/,
      (byte) 133,
      (byte) 200,
      (byte) 213,
      (byte) 166,
      (byte) 155,
      (byte) 210,
      (byte) 108,
      (byte) 108,
      (byte) 14,
      (byte) 224 /*0xE0*/,
      (byte) 207,
      (byte) 107,
      (byte) 111,
      (byte) 19,
      (byte) 254,
      (byte) 91,
      (byte) 238,
      (byte) 240 /*0xF0*/,
      (byte) 151,
      (byte) 159,
      (byte) 251,
      (byte) 193,
      (byte) 131,
      (byte) 28,
      (byte) 161,
      (byte) 168,
      (byte) 23,
      (byte) 242,
      (byte) 15,
      (byte) 129,
      (byte) 173,
      (byte) 58,
      (byte) 92,
      (byte) 70,
      (byte) 206,
      (byte) 22,
      (byte) 59,
      (byte) 24,
      (byte) 220,
      (byte) 105,
      (byte) 84
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12767()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[43];
      byte[] numArray2 = new byte[43]
      {
        (byte) 80 /*0x50*/,
        (byte) 244,
        (byte) 205,
        (byte) 220,
        (byte) 67,
        (byte) 115,
        (byte) 230,
        (byte) 182,
        (byte) 137,
        (byte) 2,
        (byte) 158,
        (byte) 13,
        (byte) 41,
        (byte) 236,
        (byte) 248,
        (byte) 88,
        (byte) 83,
        (byte) 22,
        (byte) 143,
        (byte) 153,
        (byte) 138,
        (byte) 101,
        (byte) 233,
        (byte) 133,
        (byte) 177,
        (byte) 181,
        (byte) 181,
        (byte) 210,
        (byte) 228,
        (byte) 109,
        (byte) 239,
        (byte) 191,
        (byte) 236,
        (byte) 206,
        (byte) 121,
        (byte) 144 /*0x90*/,
        (byte) 103,
        (byte) 21,
        (byte) 213,
        (byte) 44,
        (byte) 156,
        (byte) 148,
        (byte) 233
      };
      byte[] numArray3 = new byte[43]
      {
        (byte) 151,
        (byte) 230,
        (byte) 0,
        (byte) 55,
        (byte) 98,
        (byte) 100,
        (byte) 235,
        (byte) 39,
        (byte) 103,
        (byte) 109,
        (byte) 242,
        (byte) 2,
        (byte) 73,
        (byte) 91,
        (byte) 12,
        (byte) 112 /*0x70*/,
        (byte) 139,
        (byte) 29,
        (byte) 32 /*0x20*/,
        (byte) 110,
        (byte) 105,
        (byte) 25,
        (byte) 129,
        (byte) 82,
        (byte) 94,
        (byte) 175,
        (byte) 51,
        (byte) 236,
        (byte) 190,
        (byte) 211,
        (byte) 219,
        (byte) 197,
        (byte) 23,
        (byte) 105,
        (byte) 15,
        (byte) 206,
        (byte) 48 /*0x30*/,
        (byte) 34,
        (byte) 83,
        (byte) 27,
        (byte) 180,
        (byte) 253,
        (byte) 20
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 43);
      for (int index = 0; index < 43; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[43];
    byte[] numArray5 = new byte[43]
    {
      (byte) 53,
      (byte) 3,
      (byte) 163,
      (byte) 233,
      (byte) 174,
      (byte) 44,
      (byte) 182,
      (byte) 64 /*0x40*/,
      (byte) 56,
      (byte) 235,
      (byte) 11,
      (byte) 71,
      (byte) 42,
      (byte) 141,
      (byte) 189,
      (byte) 20,
      (byte) 210,
      (byte) 27,
      (byte) 19,
      (byte) 222,
      (byte) 33,
      (byte) 90,
      (byte) 114,
      (byte) 103,
      (byte) 241,
      (byte) 61,
      (byte) 65,
      (byte) 99,
      (byte) 253,
      (byte) 246,
      (byte) 123,
      (byte) 76,
      (byte) 161,
      (byte) 245,
      (byte) 127 /*0x7F*/,
      (byte) 177,
      (byte) 46,
      (byte) 250,
      (byte) 190,
      (byte) 195,
      (byte) 8,
      (byte) 171,
      (byte) 150
    };
    byte[] numArray6 = new byte[43];
    numArray6[11] = (byte) 147;
    numArray6[27] = (byte) 150;
    numArray6[6] = (byte) 61;
    numArray6[31 /*0x1F*/] = (byte) 95;
    numArray6[4] = (byte) 98;
    numArray6[5] = (byte) 141;
    numArray6[19] = (byte) 65;
    numArray6[13] = (byte) 59;
    numArray6[1] = (byte) 16 /*0x10*/;
    numArray6[9] = (byte) 202;
    numArray6[2] = (byte) 147;
    numArray6[26] = (byte) 71;
    numArray6[33] = (byte) 195;
    numArray6[0] = (byte) 169;
    numArray6[14] = (byte) 142;
    numArray6[15] = (byte) 254;
    numArray6[16 /*0x10*/] = (byte) 158;
    numArray6[17] = (byte) 205;
    numArray6[18] = (byte) 190;
    numArray6[10] = (byte) 145;
    numArray6[35] = (byte) 70;
    numArray6[7] = (byte) 44;
    numArray6[22] = (byte) 200;
    numArray6[23] = (byte) 95;
    numArray6[8] = (byte) 167;
    numArray6[25] = (byte) 197;
    numArray6[20] = (byte) 18;
    numArray6[3] = (byte) 25;
    numArray6[28] = (byte) 201;
    numArray6[32 /*0x20*/] = (byte) 205;
    numArray6[30] = (byte) 61;
    numArray6[12] = (byte) 45;
    numArray6[21] = (byte) 150;
    numArray6[40] = (byte) 141;
    numArray6[34] = (byte) 94;
    numArray6[38] = (byte) 17;
    numArray6[36] = (byte) 91;
    numArray6[24] = (byte) 156;
    numArray6[29] = (byte) 13;
    numArray6[39] = (byte) 86;
    numArray6[37] = (byte) 207;
    numArray6[41] = (byte) 0;
    numArray6[42] = (byte) 33;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 43);
    for (int index = 0; index < 43; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12768()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[108];
      byte[] numArray2 = new byte[55]
      {
        (byte) 32 /*0x20*/,
        (byte) 242,
        (byte) 2,
        (byte) 77,
        (byte) 215,
        (byte) 180,
        (byte) 104,
        (byte) 84,
        (byte) 30,
        (byte) 197,
        (byte) 121,
        (byte) 181,
        (byte) 57,
        (byte) 98,
        (byte) 162,
        (byte) 234,
        (byte) 112 /*0x70*/,
        (byte) 56,
        (byte) 47,
        (byte) 36,
        (byte) 23,
        (byte) 173,
        (byte) 89,
        (byte) 38,
        (byte) 30,
        (byte) 241,
        (byte) 95,
        (byte) 206,
        (byte) 14,
        (byte) 194,
        (byte) 251,
        (byte) 10,
        (byte) 217,
        (byte) 105,
        (byte) 235,
        (byte) 244,
        (byte) 213,
        (byte) 15,
        (byte) 120,
        (byte) 32 /*0x20*/,
        (byte) 13,
        (byte) 111,
        (byte) 198,
        (byte) 29,
        (byte) 88,
        (byte) 156,
        (byte) 191,
        (byte) 145,
        (byte) 210,
        (byte) 73,
        (byte) 61,
        (byte) 139,
        (byte) 107,
        (byte) 54,
        (byte) 141
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 224 /*0xE0*/,
        (byte) 4,
        (byte) 33,
        (byte) 163,
        (byte) 47,
        (byte) 166,
        (byte) 14,
        (byte) 197,
        (byte) 126,
        (byte) 182,
        (byte) 216,
        (byte) 6,
        (byte) 155,
        (byte) 45,
        (byte) 228,
        (byte) 4,
        (byte) 195,
        (byte) 249,
        (byte) 159,
        (byte) 27,
        (byte) 16 /*0x10*/,
        (byte) 48 /*0x30*/,
        (byte) 153,
        (byte) 200,
        (byte) 50,
        (byte) 34,
        (byte) 1,
        (byte) 93,
        (byte) 78,
        (byte) 42,
        (byte) 195,
        (byte) 184,
        (byte) 122,
        (byte) 15,
        (byte) 82,
        (byte) 96 /*0x60*/,
        (byte) 51,
        (byte) 254,
        (byte) 56,
        (byte) 222,
        (byte) 18,
        (byte) 233,
        (byte) 149,
        (byte) 62,
        (byte) 29,
        (byte) 96 /*0x60*/,
        (byte) 16 /*0x10*/,
        (byte) 30,
        (byte) 19,
        (byte) 70,
        (byte) 64 /*0x40*/,
        (byte) 178,
        (byte) 210,
        (byte) 10,
        (byte) 130
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53]
      {
        (byte) 219,
        (byte) 46,
        (byte) 230,
        (byte) 85,
        (byte) 153,
        (byte) 70,
        (byte) 91,
        (byte) 202,
        (byte) 110,
        (byte) 220,
        (byte) 207,
        (byte) 119,
        (byte) 174,
        (byte) 21,
        (byte) 96 /*0x60*/,
        (byte) 191,
        (byte) 15,
        (byte) 23,
        (byte) 167,
        (byte) 231,
        (byte) 59,
        (byte) 206,
        (byte) 129,
        (byte) 49,
        (byte) 205,
        (byte) 252,
        (byte) 197,
        (byte) 132,
        (byte) 111,
        (byte) 159,
        (byte) 163,
        (byte) 220,
        (byte) 120,
        (byte) 90,
        (byte) 249,
        (byte) 224 /*0xE0*/,
        (byte) 146,
        (byte) 202,
        (byte) 116,
        (byte) 41,
        (byte) 106,
        (byte) 82,
        (byte) 233,
        (byte) 192 /*0xC0*/,
        (byte) 239,
        (byte) 26,
        (byte) 36,
        (byte) 51,
        (byte) 220,
        (byte) 204,
        (byte) 92,
        (byte) 30,
        (byte) 230
      };
      byte[] numArray5 = new byte[53]
      {
        (byte) 8,
        (byte) 132,
        (byte) 124,
        (byte) 129,
        (byte) 109,
        (byte) 245,
        (byte) 187,
        (byte) 60,
        (byte) 101,
        (byte) 216,
        (byte) 89,
        (byte) 89,
        (byte) 145,
        (byte) 19,
        (byte) 132,
        (byte) 133,
        (byte) 240 /*0xF0*/,
        (byte) 225,
        (byte) 138,
        (byte) 138,
        (byte) 130,
        (byte) 195,
        (byte) 161,
        (byte) 114,
        (byte) 36,
        (byte) 224 /*0xE0*/,
        (byte) 28,
        (byte) 40,
        (byte) 135,
        (byte) 175,
        (byte) 126,
        (byte) 146,
        (byte) 96 /*0x60*/,
        (byte) 143,
        (byte) 108,
        (byte) 146,
        (byte) 82,
        (byte) 235,
        (byte) 25,
        (byte) 154,
        (byte) 173,
        (byte) 172,
        (byte) 163,
        (byte) 220,
        (byte) 36,
        (byte) 130,
        (byte) 85,
        (byte) 199,
        (byte) 238,
        (byte) 162,
        (byte) 17,
        (byte) 210,
        (byte) 239
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[108];
    byte[] numArray7 = new byte[55];
    numArray7[44] = (byte) 26;
    numArray7[1] = (byte) 180;
    numArray7[16 /*0x10*/] = (byte) 163;
    numArray7[8] = (byte) 57;
    numArray7[4] = (byte) 129;
    numArray7[5] = (byte) 214;
    numArray7[3] = (byte) 156;
    numArray7[7] = (byte) 207;
    numArray7[48 /*0x30*/] = (byte) 158;
    numArray7[9] = (byte) 243;
    numArray7[10] = byte.MaxValue;
    numArray7[11] = (byte) 83;
    numArray7[12] = (byte) 27;
    numArray7[43] = (byte) 252;
    numArray7[33] = (byte) 236;
    numArray7[15] = (byte) 68;
    numArray7[26] = (byte) 168;
    numArray7[32 /*0x20*/] = (byte) 253;
    numArray7[24] = (byte) 215;
    numArray7[19] = (byte) 233;
    numArray7[39] = (byte) 147;
    numArray7[47] = (byte) 92;
    numArray7[22] = (byte) 65;
    numArray7[35] = (byte) 221;
    numArray7[23] = (byte) 179;
    numArray7[28] = (byte) 66;
    numArray7[45] = (byte) 209;
    numArray7[27] = (byte) 163;
    numArray7[13] = (byte) 128 /*0x80*/;
    numArray7[29] = (byte) 132;
    numArray7[30] = (byte) 246;
    numArray7[31 /*0x1F*/] = (byte) 235;
    numArray7[21] = (byte) 193;
    numArray7[20] = (byte) 229;
    numArray7[54] = (byte) 40;
    numArray7[25] = (byte) 145;
    numArray7[36] = (byte) 66;
    numArray7[37] = (byte) 215;
    numArray7[38] = (byte) 168;
    numArray7[42] = (byte) 135;
    numArray7[40] = (byte) 246;
    numArray7[41] = (byte) 25;
    numArray7[52] = (byte) 15;
    numArray7[14] = (byte) 45;
    numArray7[18] = (byte) 124;
    numArray7[0] = (byte) 173;
    numArray7[46] = (byte) 23;
    numArray7[34] = (byte) 51;
    numArray7[49] = (byte) 24;
    numArray7[6] = (byte) 91;
    numArray7[50] = (byte) 123;
    numArray7[51] = (byte) 104;
    numArray7[2] = (byte) 136;
    numArray7[17] = (byte) 125;
    numArray7[53] = (byte) 115;
    byte[] numArray8 = new byte[55]
    {
      (byte) 25,
      (byte) 88,
      (byte) 11,
      (byte) 96 /*0x60*/,
      (byte) 211,
      (byte) 63 /*0x3F*/,
      (byte) 189,
      (byte) 214,
      (byte) 108,
      (byte) 59,
      (byte) 69,
      (byte) 76,
      (byte) 73,
      (byte) 109,
      (byte) 31 /*0x1F*/,
      (byte) 88,
      (byte) 225,
      (byte) 116,
      (byte) 170,
      (byte) 238,
      (byte) 249,
      (byte) 27,
      (byte) 63 /*0x3F*/,
      (byte) 185,
      (byte) 57,
      (byte) 131,
      (byte) 52,
      (byte) 218,
      (byte) 193,
      (byte) 224 /*0xE0*/,
      (byte) 105,
      (byte) 215,
      (byte) 185,
      (byte) 93,
      (byte) 87,
      (byte) 43,
      (byte) 139,
      (byte) 233,
      (byte) 9,
      (byte) 65,
      (byte) 103,
      (byte) 167,
      (byte) 164,
      (byte) 13,
      (byte) 245,
      (byte) 103,
      (byte) 216,
      (byte) 221,
      (byte) 216,
      (byte) 64 /*0x40*/,
      (byte) 136,
      (byte) 18,
      (byte) 146,
      (byte) 124,
      (byte) 76
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[53];
    numArray9[51] = (byte) 205;
    numArray9[33] = (byte) 61;
    numArray9[37] = (byte) 131;
    numArray9[21] = (byte) 145;
    numArray9[4] = (byte) 180;
    numArray9[16 /*0x10*/] = (byte) 131;
    numArray9[6] = (byte) 171;
    numArray9[44] = (byte) 125;
    numArray9[43] = (byte) 176 /*0xB0*/;
    numArray9[9] = (byte) 203;
    numArray9[47] = (byte) 130;
    numArray9[11] = (byte) 4;
    numArray9[22] = (byte) 138;
    numArray9[34] = (byte) 48 /*0x30*/;
    numArray9[14] = (byte) 186;
    numArray9[5] = (byte) 98;
    numArray9[12] = (byte) 228;
    numArray9[17] = (byte) 179;
    numArray9[18] = (byte) 62;
    numArray9[3] = (byte) 225;
    numArray9[19] = (byte) 103;
    numArray9[39] = (byte) 41;
    numArray9[42] = (byte) 208 /*0xD0*/;
    numArray9[36] = (byte) 66;
    numArray9[7] = (byte) 111;
    numArray9[25] = (byte) 180;
    numArray9[26] = (byte) 19;
    numArray9[24] = (byte) 60;
    numArray9[28] = (byte) 251;
    numArray9[0] = (byte) 60;
    numArray9[30] = (byte) 8;
    numArray9[8] = (byte) 54;
    numArray9[27] = (byte) 90;
    numArray9[13] = (byte) 148;
    numArray9[38] = (byte) 137;
    numArray9[35] = (byte) 83;
    numArray9[1] = (byte) 190;
    numArray9[29] = (byte) 182;
    numArray9[32 /*0x20*/] = (byte) 150;
    numArray9[10] = (byte) 106;
    numArray9[40] = (byte) 39;
    numArray9[20] = (byte) 70;
    numArray9[2] = (byte) 118;
    numArray9[23] = (byte) 144 /*0x90*/;
    numArray9[15] = (byte) 29;
    numArray9[45] = (byte) 10;
    numArray9[50] = (byte) 162;
    numArray9[31 /*0x1F*/] = (byte) 103;
    numArray9[48 /*0x30*/] = (byte) 243;
    numArray9[49] = (byte) 117;
    numArray9[46] = (byte) 106;
    numArray9[41] = (byte) 143;
    numArray9[52] = (byte) 29;
    byte[] numArray10 = new byte[53]
    {
      (byte) 111,
      (byte) 21,
      (byte) 109,
      (byte) 145,
      (byte) 90,
      (byte) 199,
      (byte) 198,
      (byte) 150,
      (byte) 153,
      (byte) 87,
      (byte) 74,
      (byte) 65,
      (byte) 204,
      (byte) 150,
      (byte) 160 /*0xA0*/,
      (byte) 9,
      (byte) 113,
      (byte) 16 /*0x10*/,
      (byte) 137,
      (byte) 31 /*0x1F*/,
      (byte) 16 /*0x10*/,
      (byte) 106,
      (byte) 5,
      (byte) 243,
      (byte) 13,
      (byte) 89,
      (byte) 98,
      (byte) 107,
      (byte) 69,
      (byte) 219,
      (byte) 137,
      (byte) 161,
      (byte) 182,
      (byte) 47,
      (byte) 10,
      (byte) 128 /*0x80*/,
      (byte) 43,
      (byte) 90,
      (byte) 192 /*0xC0*/,
      (byte) 135,
      (byte) 81,
      (byte) 96 /*0x60*/,
      (byte) 92,
      (byte) 11,
      (byte) 78,
      (byte) 195,
      (byte) 122,
      (byte) 178,
      (byte) 169,
      (byte) 174,
      byte.MaxValue,
      (byte) 116,
      (byte) 237
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 53);
    for (int index = 0; index < 53; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[24];
    byte[] response = new byte[24];
    Array.Copy((Array) sc_12743.sspq, 125, (Array) numArray11, 0, 24);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12743.sspr, 125, (Array) numArray11, 0, 24);
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

  internal static int ssp_appserver_12769(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 217,
      (byte) 235,
      (byte) 115,
      (byte) 150,
      (byte) 65,
      (byte) 64 /*0x40*/,
      (byte) 38,
      (byte) 156,
      (byte) 199,
      (byte) 137,
      (byte) 106,
      (byte) 212,
      (byte) 26,
      (byte) 236,
      (byte) 45,
      (byte) 228,
      (byte) 99,
      (byte) 193,
      (byte) 120,
      (byte) 114,
      (byte) 64 /*0x40*/,
      (byte) 70,
      (byte) 28,
      (byte) 126,
      (byte) 4,
      (byte) 205,
      (byte) 161,
      (byte) 149,
      (byte) 119,
      (byte) 106,
      (byte) 58,
      (byte) 221,
      (byte) 46,
      (byte) 77,
      (byte) 15,
      (byte) 3,
      (byte) 248,
      (byte) 242,
      (byte) 119,
      (byte) 226,
      (byte) 42,
      (byte) 65,
      (byte) 127 /*0x7F*/,
      (byte) 244,
      (byte) 183,
      (byte) 202,
      (byte) 32 /*0x20*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 157;
    sourceArray2[12] = (byte) 221;
    sourceArray2[27] = (byte) 209;
    sourceArray2[3] = (byte) 2;
    sourceArray2[19] = (byte) 67;
    sourceArray2[5] = (byte) 129;
    sourceArray2[6] = (byte) 164;
    sourceArray2[31 /*0x1F*/] = (byte) 182;
    sourceArray2[17] = (byte) 47;
    sourceArray2[46] = (byte) 251;
    sourceArray2[10] = (byte) 78;
    sourceArray2[8] = (byte) 32 /*0x20*/;
    sourceArray2[9] = (byte) 135;
    sourceArray2[13] = (byte) 38;
    sourceArray2[14] = (byte) 157;
    sourceArray2[15] = (byte) 63 /*0x3F*/;
    sourceArray2[47] = (byte) 163;
    sourceArray2[2] = (byte) 196;
    sourceArray2[4] = (byte) 21;
    sourceArray2[1] = (byte) 58;
    sourceArray2[21] = (byte) 51;
    sourceArray2[30] = (byte) 41;
    sourceArray2[42] = (byte) 190;
    sourceArray2[23] = (byte) 146;
    sourceArray2[24] = (byte) 207;
    sourceArray2[36] = (byte) 119;
    sourceArray2[20] = (byte) 28;
    sourceArray2[40] = (byte) 241;
    sourceArray2[28] = (byte) 237;
    sourceArray2[25] = (byte) 80 /*0x50*/;
    sourceArray2[7] = (byte) 18;
    sourceArray2[45] = (byte) 190;
    sourceArray2[32 /*0x20*/] = (byte) 112 /*0x70*/;
    sourceArray2[33] = (byte) 71;
    sourceArray2[34] = (byte) 91;
    sourceArray2[0] = (byte) 171;
    sourceArray2[39] = (byte) 14;
    sourceArray2[16 /*0x10*/] = (byte) 70;
    sourceArray2[38] = (byte) 49;
    sourceArray2[37] = (byte) 11;
    sourceArray2[22] = (byte) 106;
    sourceArray2[41] = (byte) 234;
    sourceArray2[35] = (byte) 188;
    sourceArray2[43] = (byte) 99;
    sourceArray2[11] = (byte) 156;
    sourceArray2[44] = (byte) 247;
    sourceArray2[18] = (byte) 188;
    sourceArray2[29] = (byte) 220;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12770()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[48 /*0x30*/];
      byte[] numArray2 = new byte[48 /*0x30*/]
      {
        (byte) 43,
        (byte) 36,
        (byte) 82,
        (byte) 159,
        (byte) 67,
        (byte) 159,
        (byte) 26,
        (byte) 141,
        (byte) 134,
        (byte) 204,
        (byte) 50,
        (byte) 204,
        (byte) 167,
        (byte) 45,
        (byte) 88,
        (byte) 199,
        (byte) 120,
        (byte) 105,
        (byte) 58,
        (byte) 125,
        (byte) 236,
        (byte) 203,
        (byte) 139,
        (byte) 177,
        (byte) 96 /*0x60*/,
        (byte) 246,
        (byte) 233,
        (byte) 16 /*0x10*/,
        (byte) 70,
        (byte) 81,
        (byte) 173,
        (byte) 226,
        (byte) 164,
        (byte) 130,
        (byte) 156,
        (byte) 20,
        (byte) 90,
        (byte) 134,
        (byte) 228,
        (byte) 74,
        (byte) 197,
        (byte) 212,
        (byte) 58,
        (byte) 77,
        (byte) 233,
        (byte) 175,
        (byte) 140,
        (byte) 214
      };
      byte[] numArray3 = new byte[48 /*0x30*/];
      numArray3[31 /*0x1F*/] = (byte) 217;
      numArray3[12] = (byte) 72;
      numArray3[2] = (byte) 163;
      numArray3[21] = (byte) 96 /*0x60*/;
      numArray3[19] = (byte) 30;
      numArray3[46] = (byte) 107;
      numArray3[6] = (byte) 41;
      numArray3[25] = (byte) 223;
      numArray3[8] = (byte) 227;
      numArray3[29] = (byte) 169;
      numArray3[10] = (byte) 17;
      numArray3[11] = (byte) 177;
      numArray3[28] = (byte) 167;
      numArray3[13] = (byte) 238;
      numArray3[14] = (byte) 146;
      numArray3[45] = (byte) 63 /*0x3F*/;
      numArray3[16 /*0x10*/] = byte.MaxValue;
      numArray3[5] = (byte) 19;
      numArray3[18] = (byte) 26;
      numArray3[9] = (byte) 62;
      numArray3[39] = (byte) 61;
      numArray3[36] = (byte) 167;
      numArray3[15] = (byte) 141;
      numArray3[23] = (byte) 148;
      numArray3[7] = (byte) 10;
      numArray3[1] = (byte) 31 /*0x1F*/;
      numArray3[26] = (byte) 220;
      numArray3[4] = (byte) 29;
      numArray3[3] = (byte) 232;
      numArray3[30] = (byte) 111;
      numArray3[20] = (byte) 54;
      numArray3[33] = (byte) 240 /*0xF0*/;
      numArray3[32 /*0x20*/] = (byte) 14;
      numArray3[34] = (byte) 98;
      numArray3[44] = (byte) 129;
      numArray3[35] = (byte) 115;
      numArray3[47] = (byte) 228;
      numArray3[37] = (byte) 120;
      numArray3[38] = (byte) 168;
      numArray3[27] = (byte) 86;
      numArray3[40] = (byte) 8;
      numArray3[22] = (byte) 151;
      numArray3[42] = (byte) 183;
      numArray3[43] = (byte) 210;
      numArray3[24] = (byte) 180;
      numArray3[41] = (byte) 14;
      numArray3[17] = (byte) 15;
      numArray3[0] = (byte) 133;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[48 /*0x30*/];
    byte[] numArray5 = new byte[48 /*0x30*/]
    {
      (byte) 154,
      (byte) 135,
      (byte) 103,
      (byte) 69,
      (byte) 194,
      (byte) 206,
      (byte) 217,
      (byte) 171,
      (byte) 243,
      (byte) 237,
      (byte) 160 /*0xA0*/,
      (byte) 206,
      (byte) 90,
      (byte) 217,
      (byte) 141,
      (byte) 227,
      (byte) 164,
      byte.MaxValue,
      (byte) 60,
      (byte) 175,
      (byte) 162,
      (byte) 79,
      (byte) 40,
      (byte) 91,
      (byte) 84,
      (byte) 200,
      (byte) 179,
      (byte) 170,
      (byte) 237,
      (byte) 90,
      (byte) 251,
      (byte) 138,
      (byte) 139,
      (byte) 16 /*0x10*/,
      (byte) 45,
      (byte) 40,
      (byte) 2,
      (byte) 205,
      (byte) 70,
      (byte) 59,
      (byte) 172,
      (byte) 209,
      (byte) 12,
      (byte) 18,
      (byte) 42,
      (byte) 110,
      (byte) 33,
      (byte) 223
    };
    byte[] numArray6 = new byte[48 /*0x30*/]
    {
      (byte) 99,
      (byte) 126,
      (byte) 247,
      (byte) 58,
      (byte) 40,
      (byte) 96 /*0x60*/,
      (byte) 179,
      (byte) 244,
      byte.MaxValue,
      (byte) 250,
      (byte) 55,
      (byte) 42,
      (byte) 54,
      (byte) 171,
      (byte) 51,
      (byte) 121,
      (byte) 119,
      (byte) 76,
      (byte) 37,
      (byte) 133,
      (byte) 34,
      (byte) 34,
      (byte) 171,
      (byte) 31 /*0x1F*/,
      (byte) 230,
      (byte) 198,
      (byte) 27,
      (byte) 24,
      (byte) 174,
      (byte) 50,
      (byte) 47,
      (byte) 36,
      (byte) 37,
      (byte) 87,
      (byte) 42,
      (byte) 198,
      (byte) 108,
      (byte) 156,
      (byte) 50,
      (byte) 234,
      (byte) 178,
      (byte) 170,
      (byte) 247,
      (byte) 121,
      (byte) 48 /*0x30*/,
      (byte) 37,
      (byte) 80 /*0x50*/,
      (byte) 182
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
