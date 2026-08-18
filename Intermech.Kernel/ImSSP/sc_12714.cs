// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12714
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12714
{
  private static byte[] sspq = new byte[118]
  {
    (byte) 21,
    (byte) 141,
    (byte) 44,
    (byte) 175,
    (byte) 202,
    (byte) 135,
    (byte) 159,
    (byte) 60,
    (byte) 19,
    (byte) 148,
    (byte) 14,
    (byte) 185,
    (byte) 21,
    (byte) 96 /*0x60*/,
    (byte) 6,
    (byte) 88,
    (byte) 103,
    (byte) 229,
    (byte) 221,
    (byte) 108,
    (byte) 159,
    (byte) 4,
    (byte) 104,
    (byte) 6,
    (byte) 245,
    (byte) 249,
    (byte) 252,
    (byte) 175,
    (byte) 176 /*0xB0*/,
    (byte) 251,
    (byte) 217,
    (byte) 6,
    (byte) 13,
    (byte) 47,
    (byte) 194,
    (byte) 22,
    (byte) 150,
    (byte) 71,
    (byte) 128 /*0x80*/,
    (byte) 217,
    (byte) 92,
    (byte) 248,
    (byte) 224 /*0xE0*/,
    (byte) 139,
    (byte) 124,
    (byte) 121,
    (byte) 25,
    (byte) 57,
    (byte) 240 /*0xF0*/,
    (byte) 100,
    (byte) 23,
    (byte) 85,
    (byte) 32 /*0x20*/,
    (byte) 74,
    (byte) 77,
    (byte) 182,
    (byte) 144 /*0x90*/,
    (byte) 206,
    (byte) 237,
    (byte) 139,
    (byte) 172,
    (byte) 197,
    (byte) 208 /*0xD0*/,
    (byte) 133,
    (byte) 239,
    (byte) 195,
    (byte) 10,
    (byte) 82,
    (byte) 17,
    (byte) 144 /*0x90*/,
    (byte) 179,
    (byte) 192 /*0xC0*/,
    (byte) 108,
    (byte) 78,
    (byte) 218,
    (byte) 74,
    (byte) 179,
    (byte) 183,
    (byte) 144 /*0x90*/,
    (byte) 199,
    (byte) 87,
    (byte) 33,
    (byte) 88,
    (byte) 207,
    (byte) 193,
    (byte) 248,
    (byte) 69,
    (byte) 194,
    (byte) 2,
    (byte) 139,
    (byte) 34,
    (byte) 197,
    (byte) 80 /*0x50*/,
    (byte) 149,
    (byte) 143,
    (byte) 3,
    (byte) 70,
    (byte) 14,
    (byte) 58,
    (byte) 175,
    (byte) 148,
    (byte) 9,
    (byte) 57,
    (byte) 144 /*0x90*/,
    (byte) 124,
    (byte) 35,
    (byte) 157,
    (byte) 100,
    (byte) 102,
    (byte) 52,
    (byte) 193,
    (byte) 3,
    (byte) 4,
    (byte) 64 /*0x40*/,
    (byte) 62,
    (byte) 165,
    (byte) 106,
    (byte) 40
  };
  private static byte[] sspr = new byte[118]
  {
    (byte) 90,
    (byte) 61,
    (byte) 104,
    (byte) 192 /*0xC0*/,
    (byte) 116,
    (byte) 144 /*0x90*/,
    (byte) 132,
    (byte) 223,
    (byte) 99,
    (byte) 135,
    (byte) 29,
    (byte) 175,
    (byte) 41,
    (byte) 5,
    (byte) 246,
    (byte) 70,
    (byte) 88,
    (byte) 181,
    (byte) 137,
    (byte) 217,
    (byte) 10,
    (byte) 47,
    (byte) 58,
    (byte) 1,
    (byte) 87,
    (byte) 201,
    (byte) 242,
    (byte) 52,
    (byte) 20,
    (byte) 69,
    (byte) 180,
    (byte) 184,
    (byte) 61,
    (byte) 198,
    (byte) 86,
    (byte) 217,
    (byte) 108,
    (byte) 66,
    (byte) 18,
    (byte) 126,
    (byte) 200,
    (byte) 148,
    (byte) 102,
    (byte) 32 /*0x20*/,
    (byte) 65,
    (byte) 34,
    (byte) 76,
    (byte) 213,
    (byte) 231,
    (byte) 153,
    (byte) 125,
    (byte) 49,
    (byte) 138,
    (byte) 126,
    (byte) 111,
    (byte) 207,
    (byte) 152,
    (byte) 120,
    (byte) 31 /*0x1F*/,
    (byte) 92,
    (byte) 173,
    (byte) 43,
    (byte) 245,
    (byte) 114,
    (byte) 167,
    (byte) 50,
    (byte) 36,
    (byte) 67,
    (byte) 206,
    (byte) 17,
    (byte) 197,
    (byte) 230,
    (byte) 21,
    (byte) 104,
    (byte) 1,
    (byte) 247,
    (byte) 208 /*0xD0*/,
    (byte) 66,
    (byte) 32 /*0x20*/,
    (byte) 156,
    (byte) 213,
    (byte) 111,
    (byte) 47,
    (byte) 234,
    (byte) 121,
    (byte) 217,
    (byte) 24,
    (byte) 47,
    (byte) 75,
    (byte) 112 /*0x70*/,
    (byte) 168,
    (byte) 71,
    (byte) 71,
    (byte) 197,
    (byte) 116,
    (byte) 226,
    (byte) 43,
    (byte) 29,
    (byte) 98,
    (byte) 28,
    (byte) 146,
    (byte) 112 /*0x70*/,
    (byte) 67,
    (byte) 11,
    (byte) 229,
    (byte) 129,
    (byte) 159,
    (byte) 112 /*0x70*/,
    (byte) 225,
    (byte) 80 /*0x50*/,
    (byte) 154,
    (byte) 4,
    (byte) 203,
    (byte) 250,
    (byte) 4,
    (byte) 168,
    (byte) 22,
    (byte) 108
  };

  internal static string ssp_appserver_12715()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 242,
        (byte) 189,
        (byte) 112 /*0x70*/,
        (byte) 209,
        (byte) 93,
        (byte) 242,
        (byte) 174,
        (byte) 108,
        (byte) 16 /*0x10*/,
        (byte) 28
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 177,
        (byte) 21,
        (byte) 247,
        (byte) 0,
        (byte) 0,
        (byte) 157,
        (byte) 0,
        (byte) 0,
        (byte) 63 /*0x3F*/,
        (byte) 0
      };
      numArray3[4] = (byte) 3;
      numArray3[6] = (byte) 44;
      numArray3[7] = (byte) 248;
      numArray3[3] = (byte) 95;
      numArray3[9] = (byte) 88;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 209,
      (byte) 205,
      (byte) 245,
      (byte) 236,
      (byte) 185,
      (byte) 115,
      (byte) 20,
      (byte) 109,
      (byte) 139,
      (byte) 254
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 230,
      (byte) 239,
      (byte) 200,
      (byte) 116,
      (byte) 242,
      (byte) 159,
      (byte) 139,
      (byte) 79,
      (byte) 105,
      (byte) 241
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[32 /*0x20*/];
    byte[] response = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_12714.sspq, 0, (Array) numArray7, 0, 32 /*0x20*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12714.sspr, 0, (Array) numArray7, 0, 32 /*0x20*/);
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

  internal static int ssp_appserver_12716(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 148;
    sourceArray1[1] = (byte) 198;
    sourceArray1[18] = (byte) 132;
    sourceArray1[3] = (byte) 243;
    sourceArray1[28] = (byte) 140;
    sourceArray1[9] = (byte) 96 /*0x60*/;
    sourceArray1[6] = (byte) 178;
    sourceArray1[33] = (byte) 187;
    sourceArray1[5] = (byte) 141;
    sourceArray1[23] = (byte) 38;
    sourceArray1[4] = (byte) 249;
    sourceArray1[22] = (byte) 37;
    sourceArray1[12] = (byte) 93;
    sourceArray1[40] = (byte) 185;
    sourceArray1[14] = (byte) 103;
    sourceArray1[15] = (byte) 50;
    sourceArray1[2] = (byte) 41;
    sourceArray1[17] = (byte) 176 /*0xB0*/;
    sourceArray1[42] = (byte) 75;
    sourceArray1[19] = (byte) 26;
    sourceArray1[24] = (byte) 186;
    sourceArray1[11] = byte.MaxValue;
    sourceArray1[16 /*0x10*/] = (byte) 150;
    sourceArray1[30] = (byte) 186;
    sourceArray1[44] = (byte) 40;
    sourceArray1[25] = (byte) 209;
    sourceArray1[26] = (byte) 139;
    sourceArray1[27] = (byte) 239;
    sourceArray1[35] = (byte) 63 /*0x3F*/;
    sourceArray1[20] = (byte) 104;
    sourceArray1[43] = (byte) 8;
    sourceArray1[31 /*0x1F*/] = (byte) 8;
    sourceArray1[32 /*0x20*/] = (byte) 21;
    sourceArray1[45] = (byte) 246;
    sourceArray1[39] = (byte) 79;
    sourceArray1[13] = (byte) 179;
    sourceArray1[36] = (byte) 47;
    sourceArray1[37] = (byte) 116;
    sourceArray1[38] = (byte) 236;
    sourceArray1[8] = (byte) 185;
    sourceArray1[10] = (byte) 221;
    sourceArray1[41] = (byte) 10;
    sourceArray1[0] = (byte) 249;
    sourceArray1[47] = (byte) 72;
    sourceArray1[29] = (byte) 24;
    sourceArray1[21] = (byte) 16 /*0x10*/;
    sourceArray1[46] = (byte) 155;
    sourceArray1[34] = (byte) 103;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[26] = (byte) 85;
    sourceArray2[1] = (byte) 192 /*0xC0*/;
    sourceArray2[43] = (byte) 209;
    sourceArray2[3] = (byte) 172;
    sourceArray2[4] = (byte) 220;
    sourceArray2[5] = (byte) 84;
    sourceArray2[19] = (byte) 152;
    sourceArray2[2] = (byte) 44;
    sourceArray2[36] = (byte) 120;
    sourceArray2[21] = (byte) 60;
    sourceArray2[39] = (byte) 218;
    sourceArray2[20] = (byte) 188;
    sourceArray2[47] = (byte) 177;
    sourceArray2[25] = (byte) 140;
    sourceArray2[11] = (byte) 74;
    sourceArray2[24] = (byte) 218;
    sourceArray2[16 /*0x10*/] = (byte) 128 /*0x80*/;
    sourceArray2[17] = (byte) 164;
    sourceArray2[18] = (byte) 149;
    sourceArray2[29] = (byte) 144 /*0x90*/;
    sourceArray2[33] = (byte) 156;
    sourceArray2[0] = (byte) 132;
    sourceArray2[22] = (byte) 2;
    sourceArray2[15] = (byte) 194;
    sourceArray2[45] = (byte) 178;
    sourceArray2[14] = (byte) 27;
    sourceArray2[6] = (byte) 194;
    sourceArray2[7] = (byte) 173;
    sourceArray2[28] = (byte) 32 /*0x20*/;
    sourceArray2[10] = (byte) 201;
    sourceArray2[9] = (byte) 199;
    sourceArray2[31 /*0x1F*/] = (byte) 71;
    sourceArray2[32 /*0x20*/] = (byte) 249;
    sourceArray2[23] = (byte) 209;
    sourceArray2[34] = (byte) 45;
    sourceArray2[38] = (byte) 99;
    sourceArray2[12] = (byte) 213;
    sourceArray2[37] = (byte) 24;
    sourceArray2[27] = (byte) 10;
    sourceArray2[44] = (byte) 52;
    sourceArray2[40] = (byte) 246;
    sourceArray2[13] = (byte) 14;
    sourceArray2[42] = (byte) 231;
    sourceArray2[41] = (byte) 84;
    sourceArray2[8] = (byte) 161;
    sourceArray2[35] = (byte) 35;
    sourceArray2[46] = (byte) 126;
    sourceArray2[30] = (byte) 29;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12717()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[91];
      byte[] numArray2 = new byte[55];
      numArray2[41] = (byte) 7;
      numArray2[1] = (byte) 141;
      numArray2[13] = (byte) 98;
      numArray2[32 /*0x20*/] = (byte) 179;
      numArray2[4] = (byte) 191;
      numArray2[40] = (byte) 165;
      numArray2[6] = (byte) 4;
      numArray2[8] = (byte) 33;
      numArray2[34] = (byte) 48 /*0x30*/;
      numArray2[9] = (byte) 46;
      numArray2[30] = (byte) 115;
      numArray2[23] = (byte) 151;
      numArray2[46] = (byte) 21;
      numArray2[22] = (byte) 121;
      numArray2[14] = (byte) 102;
      numArray2[15] = (byte) 189;
      numArray2[16 /*0x10*/] = (byte) 55;
      numArray2[17] = (byte) 11;
      numArray2[3] = (byte) 7;
      numArray2[33] = (byte) 32 /*0x20*/;
      numArray2[29] = (byte) 160 /*0xA0*/;
      numArray2[11] = (byte) 161;
      numArray2[44] = (byte) 238;
      numArray2[24] = (byte) 102;
      numArray2[25] = (byte) 121;
      numArray2[5] = (byte) 80 /*0x50*/;
      numArray2[27] = (byte) 23;
      numArray2[21] = (byte) 181;
      numArray2[28] = (byte) 250;
      numArray2[2] = (byte) 217;
      numArray2[20] = (byte) 191;
      numArray2[31 /*0x1F*/] = (byte) 113;
      numArray2[19] = (byte) 92;
      numArray2[26] = (byte) 229;
      numArray2[0] = (byte) 209;
      numArray2[10] = (byte) 150;
      numArray2[36] = (byte) 146;
      numArray2[37] = (byte) 84;
      numArray2[38] = (byte) 89;
      numArray2[39] = (byte) 105;
      numArray2[50] = byte.MaxValue;
      numArray2[7] = (byte) 174;
      numArray2[42] = (byte) 27;
      numArray2[43] = (byte) 23;
      numArray2[51] = (byte) 78;
      numArray2[45] = (byte) 242;
      numArray2[47] = (byte) 202;
      numArray2[48 /*0x30*/] = (byte) 209;
      numArray2[35] = (byte) 90;
      numArray2[49] = (byte) 165;
      numArray2[18] = (byte) 202;
      numArray2[12] = (byte) 92;
      numArray2[52] = (byte) 77;
      numArray2[53] = (byte) 200;
      numArray2[54] = (byte) 7;
      byte[] numArray3 = new byte[55]
      {
        (byte) 121,
        (byte) 244,
        (byte) 147,
        (byte) 48 /*0x30*/,
        (byte) 229,
        (byte) 134,
        (byte) 152,
        (byte) 163,
        (byte) 120,
        (byte) 216,
        (byte) 176 /*0xB0*/,
        (byte) 76,
        (byte) 71,
        (byte) 69,
        (byte) 177,
        (byte) 18,
        (byte) 86,
        (byte) 0,
        (byte) 162,
        (byte) 149,
        (byte) 238,
        (byte) 99,
        (byte) 33,
        (byte) 153,
        (byte) 25,
        (byte) 243,
        (byte) 175,
        (byte) 155,
        (byte) 154,
        (byte) 109,
        (byte) 9,
        (byte) 174,
        (byte) 144 /*0x90*/,
        (byte) 183,
        (byte) 174,
        (byte) 209,
        (byte) 23,
        (byte) 94,
        (byte) 85,
        (byte) 177,
        (byte) 134,
        (byte) 198,
        (byte) 59,
        (byte) 231,
        (byte) 201,
        (byte) 78,
        (byte) 210,
        (byte) 19,
        (byte) 175,
        (byte) 107,
        (byte) 135,
        (byte) 177,
        (byte) 131,
        (byte) 239,
        (byte) 219
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36]
      {
        (byte) 153,
        (byte) 45,
        (byte) 226,
        (byte) 80 /*0x50*/,
        (byte) 166,
        (byte) 95,
        (byte) 224 /*0xE0*/,
        (byte) 149,
        (byte) 2,
        (byte) 3,
        (byte) 34,
        (byte) 30,
        (byte) 198,
        (byte) 218,
        (byte) 48 /*0x30*/,
        (byte) 159,
        (byte) 98,
        (byte) 252,
        (byte) 100,
        byte.MaxValue,
        (byte) 5,
        (byte) 223,
        (byte) 112 /*0x70*/,
        (byte) 137,
        (byte) 130,
        (byte) 129,
        (byte) 218,
        (byte) 132,
        (byte) 137,
        (byte) 54,
        (byte) 216,
        (byte) 201,
        (byte) 146,
        (byte) 148,
        (byte) 183,
        (byte) 132
      };
      byte[] numArray5 = new byte[36];
      numArray5[31 /*0x1F*/] = (byte) 227;
      numArray5[2] = (byte) 99;
      numArray5[6] = (byte) 221;
      numArray5[3] = (byte) 49;
      numArray5[30] = (byte) 209;
      numArray5[0] = (byte) 29;
      numArray5[11] = (byte) 201;
      numArray5[7] = (byte) 184;
      numArray5[8] = (byte) 102;
      numArray5[22] = (byte) 191;
      numArray5[10] = (byte) 201;
      numArray5[15] = (byte) 239;
      numArray5[12] = (byte) 5;
      numArray5[14] = (byte) 119;
      numArray5[4] = (byte) 244;
      numArray5[18] = (byte) 254;
      numArray5[20] = (byte) 151;
      numArray5[9] = (byte) 20;
      numArray5[16 /*0x10*/] = (byte) 195;
      numArray5[24] = (byte) 43;
      numArray5[26] = (byte) 175;
      numArray5[19] = (byte) 81;
      numArray5[13] = (byte) 89;
      numArray5[23] = (byte) 63 /*0x3F*/;
      numArray5[34] = (byte) 139;
      numArray5[25] = (byte) 144 /*0x90*/;
      numArray5[5] = (byte) 236;
      numArray5[27] = (byte) 236;
      numArray5[28] = (byte) 181;
      numArray5[29] = (byte) 129;
      numArray5[1] = (byte) 22;
      numArray5[21] = (byte) 46;
      numArray5[32 /*0x20*/] = (byte) 90;
      numArray5[33] = (byte) 145;
      numArray5[17] = (byte) 130;
      numArray5[35] = (byte) 173;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[91];
    byte[] numArray7 = new byte[55];
    numArray7[0] = (byte) 61;
    numArray7[1] = (byte) 23;
    numArray7[32 /*0x20*/] = (byte) 123;
    numArray7[50] = (byte) 150;
    numArray7[18] = (byte) 238;
    numArray7[5] = (byte) 130;
    numArray7[6] = (byte) 58;
    numArray7[17] = (byte) 224 /*0xE0*/;
    numArray7[8] = (byte) 141;
    numArray7[9] = (byte) 172;
    numArray7[45] = (byte) 123;
    numArray7[11] = (byte) 222;
    numArray7[12] = (byte) 15;
    numArray7[13] = (byte) 231;
    numArray7[35] = (byte) 162;
    numArray7[15] = (byte) 199;
    numArray7[16 /*0x10*/] = (byte) 216;
    numArray7[10] = (byte) 194;
    numArray7[4] = (byte) 8;
    numArray7[49] = (byte) 228;
    numArray7[20] = (byte) 93;
    numArray7[31 /*0x1F*/] = (byte) 43;
    numArray7[3] = (byte) 211;
    numArray7[47] = (byte) 74;
    numArray7[24] = (byte) 93;
    numArray7[25] = (byte) 237;
    numArray7[26] = (byte) 204;
    numArray7[42] = (byte) 126;
    numArray7[27] = (byte) 124;
    numArray7[39] = (byte) 161;
    numArray7[30] = (byte) 215;
    numArray7[29] = (byte) 141;
    numArray7[40] = (byte) 75;
    numArray7[7] = (byte) 152;
    numArray7[2] = (byte) 112 /*0x70*/;
    numArray7[22] = (byte) 252;
    numArray7[36] = (byte) 77;
    numArray7[37] = (byte) 133;
    numArray7[38] = (byte) 197;
    numArray7[28] = (byte) 63 /*0x3F*/;
    numArray7[19] = (byte) 111;
    numArray7[33] = (byte) 61;
    numArray7[34] = (byte) 110;
    numArray7[43] = (byte) 31 /*0x1F*/;
    numArray7[44] = (byte) 104;
    numArray7[46] = (byte) 125;
    numArray7[51] = (byte) 210;
    numArray7[21] = (byte) 188;
    numArray7[14] = (byte) 253;
    numArray7[48 /*0x30*/] = (byte) 37;
    numArray7[41] = (byte) 195;
    numArray7[23] = (byte) 212;
    numArray7[52] = (byte) 250;
    numArray7[53] = (byte) 0;
    numArray7[54] = (byte) 232;
    byte[] numArray8 = new byte[55];
    numArray8[4] = (byte) 184;
    numArray8[34] = (byte) 77;
    numArray8[2] = (byte) 187;
    numArray8[5] = (byte) 179;
    numArray8[43] = (byte) 41;
    numArray8[52] = (byte) 220;
    numArray8[32 /*0x20*/] = (byte) 29;
    numArray8[7] = (byte) 135;
    numArray8[15] = (byte) 221;
    numArray8[33] = (byte) 150;
    numArray8[12] = (byte) 215;
    numArray8[37] = (byte) 61;
    numArray8[6] = (byte) 114;
    numArray8[42] = (byte) 101;
    numArray8[14] = (byte) 110;
    numArray8[40] = (byte) 91;
    numArray8[54] = (byte) 54;
    numArray8[36] = (byte) 86;
    numArray8[18] = (byte) 152;
    numArray8[19] = (byte) 97;
    numArray8[20] = (byte) 134;
    numArray8[25] = (byte) 115;
    numArray8[44] = (byte) 122;
    numArray8[23] = (byte) 165;
    numArray8[28] = (byte) 199;
    numArray8[41] = (byte) 92;
    numArray8[26] = (byte) 52;
    numArray8[27] = (byte) 212;
    numArray8[11] = (byte) 59;
    numArray8[9] = (byte) 12;
    numArray8[30] = (byte) 8;
    numArray8[31 /*0x1F*/] = (byte) 193;
    numArray8[24] = (byte) 199;
    numArray8[10] = (byte) 15;
    numArray8[0] = (byte) 229;
    numArray8[35] = (byte) 222;
    numArray8[29] = (byte) 167;
    numArray8[17] = (byte) 57;
    numArray8[47] = (byte) 206;
    numArray8[39] = (byte) 97;
    numArray8[8] = (byte) 180;
    numArray8[22] = (byte) 201;
    numArray8[21] = (byte) 35;
    numArray8[1] = (byte) 117;
    numArray8[38] = (byte) 23;
    numArray8[45] = (byte) 48 /*0x30*/;
    numArray8[46] = (byte) 73;
    numArray8[3] = (byte) 11;
    numArray8[16 /*0x10*/] = (byte) 40;
    numArray8[49] = (byte) 170;
    numArray8[50] = (byte) 16 /*0x10*/;
    numArray8[51] = (byte) 227;
    numArray8[13] = (byte) 53;
    numArray8[53] = (byte) 121;
    numArray8[48 /*0x30*/] = (byte) 240 /*0xF0*/;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[36];
    numArray9[29] = (byte) 56;
    numArray9[1] = (byte) 203;
    numArray9[7] = (byte) 130;
    numArray9[23] = (byte) 148;
    numArray9[6] = (byte) 87;
    numArray9[18] = (byte) 195;
    numArray9[35] = (byte) 176 /*0xB0*/;
    numArray9[9] = (byte) 73;
    numArray9[4] = (byte) 217;
    numArray9[17] = (byte) 109;
    numArray9[5] = (byte) 15;
    numArray9[11] = (byte) 225;
    numArray9[12] = (byte) 140;
    numArray9[13] = (byte) 70;
    numArray9[14] = (byte) 199;
    numArray9[20] = (byte) 22;
    numArray9[16 /*0x10*/] = (byte) 172;
    numArray9[22] = (byte) 151;
    numArray9[10] = (byte) 116;
    numArray9[8] = (byte) 225;
    numArray9[3] = (byte) 134;
    numArray9[21] = (byte) 45;
    numArray9[24] = (byte) 103;
    numArray9[0] = (byte) 150;
    numArray9[15] = (byte) 113;
    numArray9[25] = (byte) 149;
    numArray9[26] = (byte) 76;
    numArray9[27] = (byte) 177;
    numArray9[28] = (byte) 7;
    numArray9[34] = (byte) 235;
    numArray9[30] = (byte) 86;
    numArray9[31 /*0x1F*/] = (byte) 231;
    numArray9[32 /*0x20*/] = (byte) 38;
    numArray9[33] = (byte) 154;
    numArray9[19] = (byte) 75;
    numArray9[2] = (byte) 230;
    byte[] numArray10 = new byte[36]
    {
      (byte) 55,
      (byte) 144 /*0x90*/,
      (byte) 251,
      (byte) 134,
      (byte) 186,
      (byte) 8,
      (byte) 100,
      (byte) 166,
      (byte) 44,
      (byte) 73,
      (byte) 200,
      (byte) 82,
      (byte) 64 /*0x40*/,
      (byte) 22,
      (byte) 233,
      (byte) 65,
      (byte) 7,
      (byte) 192 /*0xC0*/,
      (byte) 241,
      (byte) 82,
      (byte) 176 /*0xB0*/,
      (byte) 225,
      (byte) 224 /*0xE0*/,
      (byte) 91,
      (byte) 136,
      (byte) 115,
      (byte) 34,
      (byte) 85,
      (byte) 188,
      (byte) 181,
      (byte) 110,
      (byte) 208 /*0xD0*/,
      (byte) 12,
      (byte) 69,
      (byte) 162,
      (byte) 83
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 36);
    for (int index = 0; index < 36; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12718()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[272];
      byte[] numArray2 = new byte[55]
      {
        (byte) 162,
        (byte) 175,
        (byte) 217,
        (byte) 1,
        (byte) 42,
        (byte) 239,
        (byte) 120,
        (byte) 49,
        (byte) 154,
        (byte) 124,
        (byte) 140,
        (byte) 238,
        (byte) 4,
        (byte) 130,
        (byte) 124,
        (byte) 198,
        (byte) 16 /*0x10*/,
        (byte) 10,
        (byte) 154,
        (byte) 119,
        (byte) 125,
        (byte) 226,
        (byte) 244,
        (byte) 46,
        (byte) 170,
        (byte) 121,
        (byte) 2,
        (byte) 33,
        (byte) 125,
        (byte) 33,
        (byte) 186,
        (byte) 244,
        (byte) 9,
        (byte) 120,
        (byte) 198,
        (byte) 131,
        (byte) 120,
        (byte) 29,
        (byte) 25,
        (byte) 210,
        (byte) 45,
        (byte) 73,
        (byte) 14,
        (byte) 118,
        (byte) 0,
        (byte) 205,
        (byte) 236,
        (byte) 215,
        (byte) 154,
        (byte) 145,
        (byte) 109,
        (byte) 20,
        (byte) 204,
        (byte) 183,
        (byte) 160 /*0xA0*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[18] = (byte) 98;
      numArray3[20] = (byte) 182;
      numArray3[41] = (byte) 61;
      numArray3[3] = (byte) 130;
      numArray3[8] = (byte) 197;
      numArray3[15] = (byte) 100;
      numArray3[11] = (byte) 2;
      numArray3[7] = (byte) 66;
      numArray3[0] = (byte) 118;
      numArray3[53] = (byte) 99;
      numArray3[10] = (byte) 215;
      numArray3[31 /*0x1F*/] = (byte) 84;
      numArray3[12] = (byte) 156;
      numArray3[13] = (byte) 79;
      numArray3[14] = (byte) 142;
      numArray3[48 /*0x30*/] = (byte) 112 /*0x70*/;
      numArray3[16 /*0x10*/] = (byte) 69;
      numArray3[17] = (byte) 170;
      numArray3[32 /*0x20*/] = (byte) 121;
      numArray3[19] = (byte) 135;
      numArray3[6] = (byte) 213;
      numArray3[21] = (byte) 26;
      numArray3[26] = (byte) 138;
      numArray3[23] = (byte) 175;
      numArray3[24] = (byte) 17;
      numArray3[2] = (byte) 23;
      numArray3[37] = (byte) 142;
      numArray3[27] = (byte) 96 /*0x60*/;
      numArray3[1] = (byte) 149;
      numArray3[29] = (byte) 75;
      numArray3[50] = (byte) 134;
      numArray3[4] = (byte) 3;
      numArray3[9] = (byte) 220;
      numArray3[33] = (byte) 40;
      numArray3[30] = (byte) 0;
      numArray3[34] = (byte) 60;
      numArray3[36] = (byte) 120;
      numArray3[42] = (byte) 49;
      numArray3[38] = (byte) 55;
      numArray3[39] = (byte) 4;
      numArray3[40] = (byte) 206;
      numArray3[54] = (byte) 128 /*0x80*/;
      numArray3[28] = (byte) 33;
      numArray3[43] = (byte) 117;
      numArray3[44] = (byte) 58;
      numArray3[45] = (byte) 24;
      numArray3[25] = (byte) 133;
      numArray3[47] = (byte) 201;
      numArray3[35] = (byte) 209;
      numArray3[49] = (byte) 120;
      numArray3[5] = (byte) 200;
      numArray3[51] = (byte) 154;
      numArray3[52] = (byte) 72;
      numArray3[22] = (byte) 10;
      numArray3[46] = (byte) 156;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 125,
        (byte) 120,
        (byte) 36,
        (byte) 195,
        (byte) 206,
        (byte) 200,
        (byte) 196,
        (byte) 28,
        (byte) 112 /*0x70*/,
        (byte) 145,
        (byte) 180,
        (byte) 181,
        (byte) 49,
        (byte) 129,
        (byte) 42,
        (byte) 137,
        (byte) 227,
        (byte) 114,
        (byte) 159,
        (byte) 62,
        (byte) 201,
        (byte) 197,
        (byte) 244,
        (byte) 44,
        (byte) 14,
        (byte) 230,
        (byte) 169,
        (byte) 57,
        (byte) 130,
        (byte) 34,
        (byte) 229,
        (byte) 108,
        (byte) 26,
        (byte) 137,
        (byte) 79,
        (byte) 78,
        (byte) 66,
        (byte) 33,
        (byte) 8,
        (byte) 204,
        (byte) 209,
        (byte) 151,
        (byte) 247,
        (byte) 87,
        (byte) 172,
        (byte) 102,
        (byte) 111,
        (byte) 192 /*0xC0*/,
        (byte) 80 /*0x50*/,
        (byte) 154,
        (byte) 33,
        (byte) 250,
        (byte) 204,
        (byte) 173,
        (byte) 117
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 110,
        (byte) 175,
        (byte) 57,
        (byte) 38,
        (byte) 97,
        (byte) 205,
        (byte) 174,
        (byte) 71,
        (byte) 225,
        (byte) 216,
        (byte) 49,
        (byte) 9,
        (byte) 106,
        (byte) 46,
        (byte) 196,
        (byte) 144 /*0x90*/,
        (byte) 230,
        (byte) 245,
        (byte) 249,
        (byte) 250,
        (byte) 13,
        (byte) 194,
        (byte) 246,
        (byte) 0,
        (byte) 159,
        (byte) 144 /*0x90*/,
        (byte) 237,
        (byte) 33,
        (byte) 6,
        (byte) 31 /*0x1F*/,
        (byte) 125,
        (byte) 107,
        (byte) 220,
        (byte) 12,
        (byte) 168,
        (byte) 214,
        (byte) 242,
        (byte) 126,
        (byte) 121,
        (byte) 237,
        (byte) 152,
        (byte) 197,
        (byte) 77,
        (byte) 113,
        (byte) 72,
        (byte) 36,
        (byte) 7,
        (byte) 72,
        (byte) 48 /*0x30*/,
        (byte) 166,
        (byte) 183,
        (byte) 217,
        (byte) 171,
        (byte) 177,
        (byte) 214
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 248,
        (byte) 180,
        (byte) 232,
        (byte) 31 /*0x1F*/,
        (byte) 139,
        (byte) 154,
        (byte) 78,
        (byte) 171,
        (byte) 72,
        (byte) 155,
        (byte) 167,
        (byte) 50,
        (byte) 219,
        (byte) 70,
        (byte) 194,
        (byte) 106,
        (byte) 210,
        (byte) 109,
        (byte) 49,
        (byte) 206,
        (byte) 157,
        (byte) 128 /*0x80*/,
        (byte) 167,
        (byte) 118,
        (byte) 232,
        (byte) 232,
        (byte) 232,
        (byte) 0,
        (byte) 235,
        (byte) 159,
        (byte) 108,
        (byte) 20,
        (byte) 150,
        (byte) 202,
        (byte) 74,
        (byte) 108,
        (byte) 31 /*0x1F*/,
        (byte) 173,
        (byte) 70,
        (byte) 253,
        (byte) 168,
        (byte) 171,
        (byte) 99,
        (byte) 203,
        (byte) 199,
        (byte) 142,
        (byte) 116,
        (byte) 83,
        (byte) 95,
        (byte) 143,
        (byte) 250,
        (byte) 64 /*0x40*/,
        (byte) 67,
        (byte) 233,
        (byte) 231
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 42,
        (byte) 185,
        (byte) 248,
        (byte) 28,
        (byte) 225,
        (byte) 167,
        (byte) 251,
        (byte) 252,
        (byte) 9,
        (byte) 167,
        (byte) 202,
        (byte) 165,
        (byte) 168,
        (byte) 249,
        (byte) 183,
        (byte) 120,
        (byte) 25,
        (byte) 236,
        (byte) 13,
        (byte) 98,
        (byte) 77,
        (byte) 48 /*0x30*/,
        (byte) 114,
        (byte) 13,
        (byte) 12,
        (byte) 84,
        (byte) 52,
        (byte) 118,
        (byte) 127 /*0x7F*/,
        (byte) 63 /*0x3F*/,
        (byte) 139,
        (byte) 250,
        (byte) 195,
        (byte) 163,
        (byte) 42,
        (byte) 39,
        (byte) 82,
        (byte) 66,
        (byte) 156,
        (byte) 78,
        (byte) 108,
        (byte) 80 /*0x50*/,
        (byte) 68,
        (byte) 83,
        (byte) 59,
        (byte) 76,
        (byte) 19,
        (byte) 179,
        (byte) 28,
        (byte) 203,
        (byte) 150,
        (byte) 86,
        (byte) 15,
        (byte) 53,
        (byte) 33
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55]
      {
        (byte) 113,
        (byte) 55,
        (byte) 162,
        (byte) 177,
        (byte) 77,
        (byte) 192 /*0xC0*/,
        (byte) 168,
        (byte) 46,
        (byte) 147,
        (byte) 155,
        (byte) 17,
        (byte) 252,
        (byte) 86,
        (byte) 74,
        (byte) 37,
        (byte) 39,
        (byte) 51,
        (byte) 37,
        (byte) 241,
        (byte) 28,
        (byte) 220,
        (byte) 174,
        (byte) 90,
        (byte) 116,
        (byte) 148,
        (byte) 161,
        (byte) 92,
        (byte) 165,
        (byte) 44,
        (byte) 208 /*0xD0*/,
        (byte) 173,
        (byte) 227,
        (byte) 210,
        (byte) 95,
        (byte) 174,
        (byte) 117,
        (byte) 163,
        (byte) 75,
        (byte) 90,
        (byte) 235,
        (byte) 187,
        (byte) 156,
        (byte) 52,
        (byte) 242,
        (byte) 99,
        (byte) 87,
        (byte) 9,
        (byte) 68,
        (byte) 212,
        (byte) 25,
        (byte) 15,
        (byte) 195,
        (byte) 245,
        (byte) 91,
        (byte) 48 /*0x30*/
      };
      byte[] numArray9 = new byte[55]
      {
        (byte) 212,
        (byte) 248,
        (byte) 103,
        (byte) 98,
        (byte) 77,
        (byte) 190,
        (byte) 149,
        (byte) 17,
        (byte) 230,
        (byte) 4,
        (byte) 131,
        (byte) 24,
        (byte) 122,
        (byte) 217,
        (byte) 13,
        (byte) 12,
        (byte) 233,
        (byte) 77,
        byte.MaxValue,
        (byte) 45,
        (byte) 31 /*0x1F*/,
        (byte) 100,
        (byte) 158,
        (byte) 141,
        (byte) 183,
        (byte) 159,
        (byte) 122,
        (byte) 14,
        (byte) 230,
        (byte) 216,
        (byte) 110,
        (byte) 215,
        (byte) 215,
        (byte) 21,
        (byte) 154,
        (byte) 195,
        (byte) 179,
        (byte) 105,
        (byte) 118,
        (byte) 5,
        (byte) 43,
        (byte) 253,
        (byte) 158,
        (byte) 43,
        (byte) 212,
        (byte) 151,
        (byte) 132,
        (byte) 124,
        (byte) 108,
        (byte) 23,
        (byte) 230,
        (byte) 34,
        (byte) 56,
        (byte) 237,
        (byte) 61
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[52]
      {
        (byte) 45,
        (byte) 29,
        (byte) 172,
        (byte) 99,
        (byte) 3,
        (byte) 2,
        (byte) 194,
        (byte) 238,
        (byte) 45,
        (byte) 42,
        (byte) 246,
        (byte) 156,
        (byte) 175,
        (byte) 131,
        (byte) 232,
        (byte) 203,
        (byte) 5,
        (byte) 97,
        (byte) 63 /*0x3F*/,
        (byte) 122,
        (byte) 103,
        (byte) 17,
        (byte) 61,
        (byte) 152,
        (byte) 24,
        (byte) 209,
        (byte) 18,
        (byte) 193,
        (byte) 233,
        (byte) 237,
        (byte) 118,
        (byte) 46,
        (byte) 236,
        (byte) 152,
        (byte) 117,
        (byte) 46,
        (byte) 174,
        (byte) 135,
        (byte) 183,
        (byte) 154,
        (byte) 114,
        (byte) 3,
        (byte) 53,
        (byte) 45,
        (byte) 18,
        (byte) 167,
        (byte) 36,
        (byte) 149,
        (byte) 39,
        (byte) 248,
        (byte) 239,
        (byte) 250
      };
      byte[] numArray11 = new byte[52]
      {
        (byte) 249,
        (byte) 75,
        (byte) 22,
        (byte) 187,
        (byte) 4,
        (byte) 217,
        (byte) 79,
        (byte) 51,
        (byte) 54,
        (byte) 217,
        (byte) 169,
        (byte) 141,
        (byte) 41,
        (byte) 173,
        (byte) 43,
        (byte) 120,
        (byte) 38,
        (byte) 181,
        (byte) 203,
        (byte) 185,
        (byte) 70,
        (byte) 61,
        (byte) 169,
        (byte) 139,
        (byte) 108,
        (byte) 119,
        (byte) 240 /*0xF0*/,
        (byte) 96 /*0x60*/,
        (byte) 138,
        (byte) 224 /*0xE0*/,
        (byte) 147,
        (byte) 26,
        (byte) 243,
        (byte) 189,
        (byte) 233,
        (byte) 60,
        (byte) 1,
        (byte) 52,
        (byte) 9,
        (byte) 210,
        (byte) 188,
        (byte) 50,
        (byte) 44,
        (byte) 178,
        (byte) 199,
        (byte) 217,
        (byte) 221,
        (byte) 219,
        (byte) 102,
        (byte) 98,
        (byte) 122,
        (byte) 217
      };
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index + 220] ^= numArray11[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray12 = new byte[272];
    byte[] numArray13 = new byte[55]
    {
      (byte) 28,
      (byte) 223,
      (byte) 20,
      (byte) 75,
      (byte) 50,
      (byte) 239,
      (byte) 1,
      (byte) 5,
      (byte) 64 /*0x40*/,
      (byte) 179,
      (byte) 200,
      (byte) 176 /*0xB0*/,
      (byte) 23,
      (byte) 13,
      (byte) 185,
      (byte) 28,
      (byte) 126,
      (byte) 169,
      (byte) 165,
      (byte) 8,
      (byte) 100,
      (byte) 219,
      (byte) 16 /*0x10*/,
      (byte) 133,
      (byte) 112 /*0x70*/,
      (byte) 172,
      (byte) 125,
      (byte) 153,
      (byte) 240 /*0xF0*/,
      (byte) 253,
      (byte) 124,
      (byte) 62,
      (byte) 164,
      (byte) 34,
      (byte) 89,
      (byte) 78,
      (byte) 154,
      (byte) 59,
      (byte) 133,
      (byte) 170,
      (byte) 112 /*0x70*/,
      (byte) 254,
      (byte) 245,
      (byte) 121,
      (byte) 73,
      (byte) 247,
      (byte) 12,
      (byte) 136,
      (byte) 110,
      (byte) 219,
      (byte) 113,
      (byte) 80 /*0x50*/,
      (byte) 74,
      (byte) 63 /*0x3F*/,
      (byte) 17
    };
    byte[] numArray14 = new byte[55];
    numArray14[40] = (byte) 133;
    numArray14[1] = (byte) 103;
    numArray14[33] = (byte) 216;
    numArray14[3] = (byte) 243;
    numArray14[37] = (byte) 209;
    numArray14[5] = (byte) 13;
    numArray14[6] = (byte) 85;
    numArray14[28] = (byte) 43;
    numArray14[8] = (byte) 95;
    numArray14[9] = (byte) 167;
    numArray14[46] = (byte) 30;
    numArray14[11] = (byte) 127 /*0x7F*/;
    numArray14[42] = (byte) 59;
    numArray14[7] = (byte) 247;
    numArray14[14] = (byte) 207;
    numArray14[23] = (byte) 111;
    numArray14[44] = (byte) 12;
    numArray14[17] = (byte) 51;
    numArray14[18] = (byte) 87;
    numArray14[25] = (byte) 75;
    numArray14[43] = (byte) 24;
    numArray14[47] = (byte) 140;
    numArray14[22] = (byte) 205;
    numArray14[19] = (byte) 195;
    numArray14[24] = (byte) 153;
    numArray14[16 /*0x10*/] = (byte) 7;
    numArray14[26] = (byte) 207;
    numArray14[27] = (byte) 6;
    numArray14[13] = (byte) 182;
    numArray14[29] = (byte) 197;
    numArray14[12] = (byte) 75;
    numArray14[36] = (byte) 51;
    numArray14[21] = (byte) 206;
    numArray14[30] = (byte) 96 /*0x60*/;
    numArray14[15] = (byte) 154;
    numArray14[35] = (byte) 247;
    numArray14[34] = (byte) 150;
    numArray14[10] = (byte) 64 /*0x40*/;
    numArray14[4] = (byte) 192 /*0xC0*/;
    numArray14[0] = (byte) 174;
    numArray14[53] = (byte) 91;
    numArray14[41] = (byte) 14;
    numArray14[31 /*0x1F*/] = (byte) 111;
    numArray14[45] = (byte) 217;
    numArray14[2] = (byte) 12;
    numArray14[38] = (byte) 179;
    numArray14[39] = (byte) 148;
    numArray14[20] = (byte) 104;
    numArray14[48 /*0x30*/] = (byte) 195;
    numArray14[49] = (byte) 92;
    numArray14[50] = (byte) 93;
    numArray14[32 /*0x20*/] = (byte) 121;
    numArray14[52] = (byte) 25;
    numArray14[51] = (byte) 120;
    numArray14[54] = (byte) 16 /*0x10*/;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray12, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 82,
      (byte) 99,
      (byte) 230,
      (byte) 125,
      (byte) 59,
      byte.MaxValue,
      (byte) 182,
      (byte) 12,
      (byte) 150,
      (byte) 167,
      (byte) 226,
      (byte) 156,
      (byte) 235,
      (byte) 119,
      (byte) 226,
      (byte) 37,
      (byte) 195,
      (byte) 141,
      (byte) 124,
      (byte) 51,
      (byte) 84,
      (byte) 124,
      (byte) 140,
      (byte) 135,
      (byte) 33,
      (byte) 40,
      (byte) 188,
      (byte) 93,
      (byte) 0,
      (byte) 164,
      (byte) 187,
      (byte) 103,
      (byte) 189,
      (byte) 69,
      (byte) 185,
      (byte) 252,
      (byte) 25,
      (byte) 186,
      (byte) 206,
      (byte) 234,
      (byte) 252,
      (byte) 53,
      (byte) 40,
      (byte) 68,
      (byte) 53,
      (byte) 54,
      (byte) 24,
      (byte) 225,
      (byte) 76,
      (byte) 6,
      (byte) 139,
      (byte) 26,
      (byte) 46,
      (byte) 38,
      (byte) 228
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 103,
      (byte) 2,
      (byte) 105,
      (byte) 69,
      (byte) 229,
      (byte) 180,
      (byte) 11,
      (byte) 202,
      (byte) 104,
      (byte) 61,
      (byte) 50,
      (byte) 22,
      (byte) 27,
      (byte) 229,
      (byte) 179,
      (byte) 166,
      (byte) 248,
      (byte) 46,
      (byte) 52,
      (byte) 103,
      (byte) 100,
      (byte) 141,
      (byte) 156,
      (byte) 31 /*0x1F*/,
      (byte) 23,
      (byte) 193,
      (byte) 160 /*0xA0*/,
      (byte) 200,
      (byte) 70,
      (byte) 218,
      (byte) 118,
      (byte) 149,
      (byte) 246,
      (byte) 32 /*0x20*/,
      (byte) 13,
      (byte) 55,
      (byte) 70,
      (byte) 108,
      (byte) 181,
      (byte) 74,
      (byte) 87,
      (byte) 151,
      byte.MaxValue,
      (byte) 105,
      (byte) 220,
      (byte) 148,
      (byte) 132,
      (byte) 85,
      (byte) 109,
      (byte) 25,
      (byte) 105,
      (byte) 80 /*0x50*/,
      (byte) 149,
      (byte) 137,
      (byte) 27
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray12, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 55] ^= numArray16[index];
    byte[] numArray17 = new byte[55]
    {
      (byte) 94,
      (byte) 91,
      (byte) 19,
      (byte) 67,
      (byte) 88,
      (byte) 11,
      (byte) 242,
      (byte) 236,
      (byte) 123,
      (byte) 223,
      (byte) 165,
      (byte) 189,
      (byte) 230,
      (byte) 12,
      (byte) 9,
      (byte) 191,
      (byte) 146,
      (byte) 55,
      (byte) 31 /*0x1F*/,
      (byte) 83,
      (byte) 72,
      (byte) 81,
      (byte) 235,
      (byte) 16 /*0x10*/,
      (byte) 218,
      (byte) 17,
      (byte) 249,
      (byte) 25,
      (byte) 160 /*0xA0*/,
      (byte) 56,
      (byte) 150,
      (byte) 97,
      (byte) 226,
      (byte) 36,
      (byte) 186,
      (byte) 94,
      (byte) 186,
      (byte) 167,
      (byte) 163,
      (byte) 41,
      (byte) 54,
      (byte) 88,
      (byte) 186,
      (byte) 87,
      (byte) 114,
      (byte) 36,
      (byte) 101,
      (byte) 250,
      (byte) 129,
      (byte) 91,
      (byte) 156,
      (byte) 239,
      (byte) 197,
      (byte) 130,
      (byte) 142
    };
    byte[] numArray18 = new byte[55];
    numArray18[36] = (byte) 190;
    numArray18[1] = (byte) 186;
    numArray18[2] = (byte) 83;
    numArray18[3] = (byte) 141;
    numArray18[4] = (byte) 5;
    numArray18[45] = (byte) 52;
    numArray18[48 /*0x30*/] = (byte) 73;
    numArray18[9] = (byte) 103;
    numArray18[8] = (byte) 132;
    numArray18[30] = (byte) 12;
    numArray18[21] = (byte) 247;
    numArray18[11] = (byte) 86;
    numArray18[12] = (byte) 58;
    numArray18[14] = (byte) 118;
    numArray18[0] = (byte) 215;
    numArray18[15] = (byte) 54;
    numArray18[16 /*0x10*/] = (byte) 72;
    numArray18[17] = (byte) 231;
    numArray18[18] = (byte) 99;
    numArray18[54] = (byte) 43;
    numArray18[20] = (byte) 208 /*0xD0*/;
    numArray18[43] = (byte) 121;
    numArray18[22] = (byte) 164;
    numArray18[23] = (byte) 70;
    numArray18[46] = (byte) 229;
    numArray18[25] = (byte) 247;
    numArray18[28] = (byte) 21;
    numArray18[7] = (byte) 183;
    numArray18[10] = (byte) 114;
    numArray18[29] = (byte) 34;
    numArray18[41] = (byte) 89;
    numArray18[50] = (byte) 64 /*0x40*/;
    numArray18[6] = (byte) 43;
    numArray18[33] = (byte) 69;
    numArray18[32 /*0x20*/] = (byte) 121;
    numArray18[35] = (byte) 28;
    numArray18[49] = (byte) 90;
    numArray18[53] = (byte) 40;
    numArray18[38] = (byte) 220;
    numArray18[31 /*0x1F*/] = (byte) 201;
    numArray18[40] = (byte) 34;
    numArray18[26] = (byte) 1;
    numArray18[42] = (byte) 160 /*0xA0*/;
    numArray18[52] = (byte) 105;
    numArray18[44] = (byte) 177;
    numArray18[5] = (byte) 200;
    numArray18[39] = (byte) 104;
    numArray18[47] = (byte) 235;
    numArray18[24] = (byte) 28;
    numArray18[13] = (byte) 133;
    numArray18[34] = (byte) 186;
    numArray18[51] = (byte) 158;
    numArray18[19] = (byte) 3;
    numArray18[37] = (byte) 233;
    numArray18[27] = (byte) 145;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray12, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 110] ^= numArray18[index];
    byte[] numArray19 = new byte[55]
    {
      (byte) 101,
      (byte) 178,
      (byte) 156,
      (byte) 18,
      (byte) 65,
      (byte) 5,
      (byte) 165,
      (byte) 161,
      (byte) 167,
      (byte) 123,
      (byte) 43,
      (byte) 164,
      (byte) 189,
      (byte) 78,
      (byte) 172,
      (byte) 54,
      (byte) 221,
      (byte) 191,
      (byte) 227,
      (byte) 31 /*0x1F*/,
      (byte) 132,
      (byte) 112 /*0x70*/,
      (byte) 109,
      (byte) 7,
      (byte) 87,
      (byte) 193,
      (byte) 239,
      (byte) 235,
      (byte) 74,
      (byte) 204,
      (byte) 249,
      (byte) 247,
      (byte) 54,
      (byte) 16 /*0x10*/,
      (byte) 81,
      (byte) 139,
      (byte) 192 /*0xC0*/,
      (byte) 5,
      (byte) 28,
      (byte) 7,
      (byte) 209,
      (byte) 108,
      (byte) 133,
      (byte) 206,
      (byte) 119,
      (byte) 3,
      (byte) 24,
      (byte) 8,
      (byte) 4,
      (byte) 37,
      (byte) 241,
      (byte) 166,
      (byte) 187,
      (byte) 86,
      (byte) 88
    };
    byte[] numArray20 = new byte[55]
    {
      (byte) 212,
      (byte) 128 /*0x80*/,
      (byte) 60,
      (byte) 5,
      (byte) 65,
      (byte) 219,
      (byte) 48 /*0x30*/,
      (byte) 163,
      (byte) 233,
      (byte) 114,
      (byte) 119,
      (byte) 135,
      (byte) 163,
      (byte) 102,
      (byte) 128 /*0x80*/,
      (byte) 238,
      (byte) 244,
      (byte) 96 /*0x60*/,
      (byte) 27,
      (byte) 8,
      (byte) 6,
      (byte) 223,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 145,
      (byte) 16 /*0x10*/,
      (byte) 97,
      (byte) 60,
      (byte) 199,
      (byte) 61,
      (byte) 216,
      (byte) 214,
      (byte) 218,
      (byte) 28,
      (byte) 251,
      (byte) 221,
      (byte) 244,
      (byte) 224 /*0xE0*/,
      (byte) 162,
      (byte) 10,
      (byte) 134,
      (byte) 118,
      (byte) 75,
      (byte) 117,
      (byte) 208 /*0xD0*/,
      (byte) 100,
      (byte) 224 /*0xE0*/,
      (byte) 224 /*0xE0*/,
      (byte) 193,
      (byte) 17,
      (byte) 172,
      (byte) 105,
      (byte) 188,
      (byte) 9,
      (byte) 195
    };
    key.Query(true, 335, numArray19, numArray19);
    Array.Copy((Array) numArray19, 0, (Array) numArray12, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray12[index + 165] ^= numArray20[index];
    byte[] numArray21 = new byte[52]
    {
      (byte) 148,
      (byte) 187,
      (byte) 127 /*0x7F*/,
      (byte) 22,
      (byte) 186,
      (byte) 87,
      (byte) 218,
      (byte) 129,
      (byte) 52,
      (byte) 109,
      (byte) 173,
      (byte) 252,
      (byte) 32 /*0x20*/,
      (byte) 118,
      (byte) 216,
      (byte) 163,
      (byte) 220,
      (byte) 77,
      (byte) 24,
      (byte) 234,
      (byte) 159,
      (byte) 144 /*0x90*/,
      (byte) 83,
      (byte) 169,
      (byte) 231,
      (byte) 230,
      (byte) 36,
      (byte) 219,
      (byte) 83,
      (byte) 60,
      (byte) 224 /*0xE0*/,
      (byte) 184,
      (byte) 91,
      (byte) 70,
      (byte) 44,
      (byte) 73,
      (byte) 156,
      (byte) 241,
      (byte) 23,
      (byte) 21,
      (byte) 170,
      (byte) 76,
      (byte) 67,
      (byte) 150,
      (byte) 106,
      (byte) 11,
      (byte) 228,
      (byte) 175,
      (byte) 25,
      (byte) 2,
      (byte) 222,
      (byte) 9
    };
    byte[] numArray22 = new byte[52]
    {
      (byte) 28,
      (byte) 151,
      (byte) 242,
      (byte) 233,
      (byte) 106,
      (byte) 242,
      (byte) 106,
      (byte) 213,
      (byte) 231,
      (byte) 245,
      (byte) 245,
      (byte) 17,
      (byte) 0,
      (byte) 13,
      (byte) 43,
      (byte) 50,
      (byte) 156,
      (byte) 186,
      (byte) 117,
      (byte) 98,
      (byte) 96 /*0x60*/,
      (byte) 139,
      (byte) 80 /*0x50*/,
      (byte) 172,
      (byte) 157,
      (byte) 237,
      (byte) 179,
      (byte) 51,
      (byte) 207,
      (byte) 216,
      (byte) 220,
      (byte) 162,
      (byte) 197,
      (byte) 20,
      (byte) 223,
      (byte) 193,
      (byte) 33,
      (byte) 208 /*0xD0*/,
      (byte) 159,
      (byte) 160 /*0xA0*/,
      (byte) 179,
      (byte) 220,
      (byte) 135,
      (byte) 136,
      (byte) 119,
      (byte) 75,
      (byte) 102,
      (byte) 86,
      (byte) 64 /*0x40*/,
      (byte) 119,
      (byte) 88,
      (byte) 225
    };
    key.Query(true, 335, numArray21, numArray21);
    Array.Copy((Array) numArray21, 0, (Array) numArray12, 220, 52);
    for (int index = 0; index < 52; ++index)
      numArray12[index + 220] ^= numArray22[index];
    return Encoding.UTF8.GetString(numArray12);
  }

  internal static string ssp_appserver_12719()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[276];
      byte[] numArray2 = new byte[55]
      {
        (byte) 104,
        (byte) 213,
        (byte) 46,
        (byte) 211,
        (byte) 51,
        byte.MaxValue,
        (byte) 45,
        (byte) 248,
        (byte) 141,
        (byte) 245,
        (byte) 160 /*0xA0*/,
        (byte) 217,
        (byte) 74,
        (byte) 142,
        (byte) 7,
        (byte) 234,
        (byte) 54,
        (byte) 2,
        (byte) 63 /*0x3F*/,
        (byte) 82,
        (byte) 76,
        (byte) 188,
        (byte) 63 /*0x3F*/,
        (byte) 219,
        (byte) 2,
        (byte) 117,
        (byte) 194,
        (byte) 81,
        (byte) 93,
        (byte) 214,
        (byte) 120,
        (byte) 80 /*0x50*/,
        (byte) 160 /*0xA0*/,
        (byte) 40,
        (byte) 225,
        (byte) 199,
        (byte) 194,
        (byte) 67,
        (byte) 226,
        (byte) 171,
        (byte) 31 /*0x1F*/,
        (byte) 75,
        (byte) 8,
        (byte) 117,
        (byte) 106,
        (byte) 10,
        (byte) 100,
        (byte) 138,
        (byte) 251,
        (byte) 244,
        (byte) 211,
        (byte) 61,
        (byte) 204,
        (byte) 83,
        (byte) 183
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 227,
        (byte) 110,
        (byte) 167,
        (byte) 104,
        (byte) 77,
        (byte) 69,
        (byte) 29,
        (byte) 122,
        (byte) 31 /*0x1F*/,
        (byte) 204,
        (byte) 166,
        (byte) 79,
        (byte) 193,
        (byte) 241,
        (byte) 145,
        (byte) 161,
        (byte) 220,
        (byte) 167,
        (byte) 125,
        (byte) 148,
        (byte) 217,
        (byte) 86,
        (byte) 232,
        (byte) 20,
        (byte) 193,
        (byte) 209,
        (byte) 17,
        (byte) 92,
        (byte) 82,
        (byte) 19,
        (byte) 196,
        (byte) 41,
        (byte) 48 /*0x30*/,
        (byte) 11,
        (byte) 76,
        (byte) 132,
        (byte) 1,
        (byte) 157,
        (byte) 223,
        (byte) 56,
        (byte) 146,
        (byte) 14,
        (byte) 133,
        (byte) 68,
        (byte) 21,
        (byte) 253,
        (byte) 163,
        (byte) 53,
        (byte) 182,
        (byte) 225,
        (byte) 158,
        (byte) 66,
        (byte) 109,
        (byte) 58,
        (byte) 196
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[3] = (byte) 61;
      numArray4[21] = (byte) 113;
      numArray4[2] = (byte) 189;
      numArray4[12] = (byte) 63 /*0x3F*/;
      numArray4[39] = (byte) 176 /*0xB0*/;
      numArray4[5] = (byte) 154;
      numArray4[33] = (byte) 48 /*0x30*/;
      numArray4[52] = (byte) 181;
      numArray4[4] = (byte) 220;
      numArray4[23] = (byte) 17;
      numArray4[51] = (byte) 32 /*0x20*/;
      numArray4[42] = (byte) 26;
      numArray4[0] = (byte) 83;
      numArray4[13] = (byte) 40;
      numArray4[45] = (byte) 221;
      numArray4[15] = (byte) 32 /*0x20*/;
      numArray4[29] = (byte) 132;
      numArray4[17] = (byte) 51;
      numArray4[18] = (byte) 1;
      numArray4[40] = (byte) 81;
      numArray4[20] = (byte) 201;
      numArray4[11] = (byte) 173;
      numArray4[53] = (byte) 246;
      numArray4[6] = (byte) 182;
      numArray4[24] = (byte) 55;
      numArray4[9] = (byte) 57;
      numArray4[34] = (byte) 111;
      numArray4[27] = (byte) 3;
      numArray4[28] = (byte) 59;
      numArray4[14] = (byte) 242;
      numArray4[10] = (byte) 34;
      numArray4[31 /*0x1F*/] = (byte) 113;
      numArray4[16 /*0x10*/] = (byte) 148;
      numArray4[22] = (byte) 247;
      numArray4[25] = (byte) 85;
      numArray4[48 /*0x30*/] = (byte) 224 /*0xE0*/;
      numArray4[8] = (byte) 80 /*0x50*/;
      numArray4[46] = (byte) 187;
      numArray4[1] = (byte) 209;
      numArray4[44] = (byte) 66;
      numArray4[32 /*0x20*/] = (byte) 54;
      numArray4[41] = (byte) 248;
      numArray4[35] = (byte) 113;
      numArray4[43] = (byte) 209;
      numArray4[38] = (byte) 66;
      numArray4[26] = (byte) 105;
      numArray4[7] = (byte) 125;
      numArray4[47] = (byte) 203;
      numArray4[19] = (byte) 180;
      numArray4[49] = (byte) 28;
      numArray4[50] = (byte) 56;
      numArray4[36] = (byte) 36;
      numArray4[37] = (byte) 109;
      numArray4[30] = (byte) 76;
      numArray4[54] = (byte) 204;
      byte[] numArray5 = new byte[55];
      numArray5[51] = (byte) 219;
      numArray5[40] = (byte) 219;
      numArray5[2] = (byte) 105;
      numArray5[3] = (byte) 196;
      numArray5[26] = (byte) 134;
      numArray5[34] = (byte) 80 /*0x50*/;
      numArray5[25] = (byte) 7;
      numArray5[37] = (byte) 97;
      numArray5[29] = (byte) 215;
      numArray5[9] = (byte) 82;
      numArray5[10] = (byte) 38;
      numArray5[11] = (byte) 117;
      numArray5[12] = (byte) 194;
      numArray5[13] = (byte) 114;
      numArray5[14] = (byte) 155;
      numArray5[15] = (byte) 193;
      numArray5[16 /*0x10*/] = (byte) 190;
      numArray5[4] = (byte) 130;
      numArray5[18] = (byte) 253;
      numArray5[19] = (byte) 241;
      numArray5[54] = (byte) 182;
      numArray5[39] = (byte) 21;
      numArray5[22] = (byte) 105;
      numArray5[23] = (byte) 35;
      numArray5[24] = (byte) 217;
      numArray5[49] = (byte) 157;
      numArray5[20] = (byte) 196;
      numArray5[27] = (byte) 94;
      numArray5[28] = (byte) 203;
      numArray5[53] = (byte) 123;
      numArray5[30] = (byte) 230;
      numArray5[31 /*0x1F*/] = (byte) 95;
      numArray5[32 /*0x20*/] = (byte) 103;
      numArray5[33] = (byte) 96 /*0x60*/;
      numArray5[1] = (byte) 179;
      numArray5[35] = (byte) 32 /*0x20*/;
      numArray5[52] = (byte) 50;
      numArray5[6] = (byte) 119;
      numArray5[38] = (byte) 91;
      numArray5[47] = (byte) 152;
      numArray5[8] = (byte) 163;
      numArray5[41] = (byte) 194;
      numArray5[42] = (byte) 108;
      numArray5[43] = (byte) 14;
      numArray5[44] = (byte) 93;
      numArray5[0] = (byte) 208 /*0xD0*/;
      numArray5[46] = (byte) 181;
      numArray5[50] = (byte) 180;
      numArray5[36] = (byte) 13;
      numArray5[45] = (byte) 23;
      numArray5[5] = (byte) 171;
      numArray5[17] = (byte) 172;
      numArray5[21] = (byte) 17;
      numArray5[7] = (byte) 225;
      numArray5[48 /*0x30*/] = (byte) 113;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[35] = (byte) 6;
      numArray6[17] = (byte) 181;
      numArray6[49] = (byte) 147;
      numArray6[3] = (byte) 7;
      numArray6[53] = (byte) 74;
      numArray6[47] = (byte) 175;
      numArray6[6] = (byte) 68;
      numArray6[7] = (byte) 197;
      numArray6[18] = (byte) 136;
      numArray6[9] = (byte) 140;
      numArray6[13] = (byte) 235;
      numArray6[27] = (byte) 231;
      numArray6[30] = (byte) 217;
      numArray6[33] = (byte) 114;
      numArray6[14] = (byte) 1;
      numArray6[34] = (byte) 249;
      numArray6[51] = (byte) 27;
      numArray6[0] = (byte) 159;
      numArray6[5] = (byte) 36;
      numArray6[31 /*0x1F*/] = (byte) 137;
      numArray6[20] = (byte) 31 /*0x1F*/;
      numArray6[45] = (byte) 188;
      numArray6[22] = (byte) 196;
      numArray6[23] = (byte) 234;
      numArray6[40] = (byte) 138;
      numArray6[25] = (byte) 75;
      numArray6[2] = (byte) 0;
      numArray6[19] = (byte) 56;
      numArray6[24] = (byte) 165;
      numArray6[29] = (byte) 194;
      numArray6[4] = (byte) 205;
      numArray6[10] = (byte) 65;
      numArray6[32 /*0x20*/] = (byte) 127 /*0x7F*/;
      numArray6[11] = (byte) 41;
      numArray6[39] = (byte) 110;
      numArray6[28] = (byte) 0;
      numArray6[36] = (byte) 205;
      numArray6[1] = (byte) 182;
      numArray6[38] = (byte) 64 /*0x40*/;
      numArray6[26] = (byte) 99;
      numArray6[8] = (byte) 159;
      numArray6[41] = (byte) 93;
      numArray6[42] = (byte) 251;
      numArray6[43] = (byte) 163;
      numArray6[44] = (byte) 192 /*0xC0*/;
      numArray6[12] = (byte) 5;
      numArray6[46] = (byte) 113;
      numArray6[15] = (byte) 103;
      numArray6[48 /*0x30*/] = (byte) 82;
      numArray6[21] = (byte) 162;
      numArray6[50] = (byte) 173;
      numArray6[16 /*0x10*/] = (byte) 229;
      numArray6[52] = (byte) 87;
      numArray6[37] = (byte) 228;
      numArray6[54] = (byte) 82;
      byte[] numArray7 = new byte[55]
      {
        (byte) 182,
        (byte) 204,
        (byte) 35,
        (byte) 98,
        (byte) 77,
        (byte) 54,
        (byte) 183,
        (byte) 61,
        (byte) 187,
        (byte) 193,
        (byte) 159,
        (byte) 248,
        (byte) 184,
        (byte) 218,
        (byte) 130,
        (byte) 94,
        (byte) 131,
        (byte) 93,
        (byte) 128 /*0x80*/,
        (byte) 118,
        (byte) 166,
        (byte) 122,
        (byte) 178,
        (byte) 189,
        (byte) 218,
        (byte) 21,
        (byte) 206,
        (byte) 104,
        (byte) 142,
        (byte) 33,
        (byte) 249,
        (byte) 97,
        (byte) 31 /*0x1F*/,
        (byte) 174,
        (byte) 136,
        (byte) 122,
        byte.MaxValue,
        (byte) 87,
        (byte) 224 /*0xE0*/,
        (byte) 65,
        (byte) 236,
        (byte) 25,
        (byte) 6,
        (byte) 39,
        (byte) 71,
        (byte) 102,
        (byte) 24,
        (byte) 41,
        (byte) 200,
        (byte) 131,
        (byte) 212,
        (byte) 54,
        (byte) 137,
        (byte) 215,
        (byte) 197
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[55];
      numArray8[35] = (byte) 10;
      numArray8[48 /*0x30*/] = (byte) 45;
      numArray8[2] = (byte) 168;
      numArray8[37] = (byte) 161;
      numArray8[46] = (byte) 112 /*0x70*/;
      numArray8[13] = (byte) 225;
      numArray8[6] = (byte) 200;
      numArray8[42] = (byte) 132;
      numArray8[21] = (byte) 32 /*0x20*/;
      numArray8[9] = (byte) 117;
      numArray8[10] = (byte) 115;
      numArray8[11] = (byte) 244;
      numArray8[47] = (byte) 25;
      numArray8[0] = (byte) 235;
      numArray8[14] = (byte) 126;
      numArray8[15] = (byte) 182;
      numArray8[16 /*0x10*/] = (byte) 108;
      numArray8[17] = (byte) 127 /*0x7F*/;
      numArray8[18] = (byte) 40;
      numArray8[12] = (byte) 230;
      numArray8[23] = (byte) 250;
      numArray8[20] = (byte) 47;
      numArray8[5] = (byte) 87;
      numArray8[30] = (byte) 151;
      numArray8[24] = (byte) 245;
      numArray8[25] = (byte) 62;
      numArray8[26] = (byte) 114;
      numArray8[41] = (byte) 252;
      numArray8[4] = (byte) 199;
      numArray8[49] = (byte) 241;
      numArray8[43] = (byte) 156;
      numArray8[31 /*0x1F*/] = (byte) 141;
      numArray8[32 /*0x20*/] = (byte) 167;
      numArray8[33] = (byte) 66;
      numArray8[34] = (byte) 204;
      numArray8[22] = (byte) 215;
      numArray8[36] = (byte) 21;
      numArray8[1] = (byte) 119;
      numArray8[38] = (byte) 101;
      numArray8[29] = (byte) 245;
      numArray8[19] = (byte) 137;
      numArray8[44] = (byte) 106;
      numArray8[39] = (byte) 65;
      numArray8[53] = (byte) 203;
      numArray8[27] = (byte) 104;
      numArray8[45] = (byte) 202;
      numArray8[8] = (byte) 132;
      numArray8[7] = (byte) 244;
      numArray8[50] = (byte) 208 /*0xD0*/;
      numArray8[3] = (byte) 12;
      numArray8[52] = (byte) 105;
      numArray8[51] = (byte) 29;
      numArray8[40] = (byte) 84;
      numArray8[28] = (byte) 22;
      numArray8[54] = (byte) 161;
      byte[] numArray9 = new byte[55];
      numArray9[14] = (byte) 219;
      numArray9[17] = (byte) 205;
      numArray9[28] = (byte) 224 /*0xE0*/;
      numArray9[3] = (byte) 240 /*0xF0*/;
      numArray9[4] = (byte) 34;
      numArray9[54] = (byte) 70;
      numArray9[11] = (byte) 79;
      numArray9[2] = (byte) 145;
      numArray9[8] = (byte) 103;
      numArray9[22] = (byte) 190;
      numArray9[10] = (byte) 25;
      numArray9[31 /*0x1F*/] = (byte) 136;
      numArray9[12] = (byte) 106;
      numArray9[20] = (byte) 105;
      numArray9[16 /*0x10*/] = (byte) 12;
      numArray9[15] = (byte) 153;
      numArray9[36] = (byte) 189;
      numArray9[41] = (byte) 170;
      numArray9[1] = (byte) 218;
      numArray9[19] = (byte) 9;
      numArray9[9] = (byte) 226;
      numArray9[39] = (byte) 93;
      numArray9[27] = (byte) 9;
      numArray9[23] = (byte) 34;
      numArray9[33] = (byte) 215;
      numArray9[6] = (byte) 13;
      numArray9[51] = (byte) 174;
      numArray9[32 /*0x20*/] = (byte) 192 /*0xC0*/;
      numArray9[5] = (byte) 100;
      numArray9[21] = (byte) 13;
      numArray9[13] = (byte) 119;
      numArray9[50] = (byte) 211;
      numArray9[30] = (byte) 122;
      numArray9[35] = (byte) 115;
      numArray9[26] = (byte) 250;
      numArray9[46] = (byte) 51;
      numArray9[38] = (byte) 251;
      numArray9[37] = (byte) 152;
      numArray9[0] = (byte) 3;
      numArray9[44] = (byte) 117;
      numArray9[29] = (byte) 14;
      numArray9[34] = (byte) 188;
      numArray9[42] = (byte) 252;
      numArray9[43] = (byte) 107;
      numArray9[40] = (byte) 103;
      numArray9[45] = (byte) 124;
      numArray9[25] = (byte) 124;
      numArray9[47] = (byte) 225;
      numArray9[48 /*0x30*/] = (byte) 224 /*0xE0*/;
      numArray9[49] = (byte) 56;
      numArray9[7] = (byte) 248;
      numArray9[18] = (byte) 10;
      numArray9[52] = (byte) 206;
      numArray9[53] = (byte) 23;
      numArray9[24] = (byte) 121;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[55]
      {
        (byte) 18,
        (byte) 62,
        (byte) 187,
        (byte) 55,
        (byte) 165,
        (byte) 230,
        (byte) 75,
        (byte) 235,
        (byte) 32 /*0x20*/,
        (byte) 43,
        (byte) 207,
        (byte) 63 /*0x3F*/,
        (byte) 5,
        (byte) 237,
        (byte) 162,
        (byte) 30,
        (byte) 185,
        (byte) 114,
        (byte) 176 /*0xB0*/,
        (byte) 26,
        (byte) 33,
        (byte) 72,
        (byte) 160 /*0xA0*/,
        (byte) 55,
        (byte) 98,
        (byte) 208 /*0xD0*/,
        (byte) 185,
        (byte) 89,
        (byte) 178,
        (byte) 168,
        (byte) 239,
        (byte) 218,
        (byte) 75,
        (byte) 27,
        (byte) 55,
        (byte) 95,
        (byte) 100,
        (byte) 153,
        (byte) 39,
        (byte) 126,
        (byte) 64 /*0x40*/,
        (byte) 111,
        (byte) 162,
        (byte) 225,
        (byte) 249,
        (byte) 179,
        (byte) 170,
        (byte) 43,
        (byte) 35,
        (byte) 166,
        (byte) 63 /*0x3F*/,
        (byte) 227,
        (byte) 167,
        (byte) 109,
        (byte) 199
      };
      byte[] numArray11 = new byte[55];
      numArray11[49] = (byte) 39;
      numArray11[53] = (byte) 112 /*0x70*/;
      numArray11[2] = (byte) 34;
      numArray11[7] = (byte) 112 /*0x70*/;
      numArray11[4] = (byte) 151;
      numArray11[15] = (byte) 31 /*0x1F*/;
      numArray11[29] = (byte) 134;
      numArray11[28] = (byte) 201;
      numArray11[8] = (byte) 203;
      numArray11[32 /*0x20*/] = (byte) 94;
      numArray11[38] = (byte) 203;
      numArray11[11] = (byte) 118;
      numArray11[9] = (byte) 190;
      numArray11[41] = (byte) 246;
      numArray11[34] = (byte) 238;
      numArray11[10] = (byte) 231;
      numArray11[31 /*0x1F*/] = (byte) 67;
      numArray11[6] = (byte) 192 /*0xC0*/;
      numArray11[26] = (byte) 84;
      numArray11[19] = (byte) 242;
      numArray11[43] = (byte) 20;
      numArray11[18] = (byte) 49;
      numArray11[22] = (byte) 3;
      numArray11[23] = (byte) 90;
      numArray11[1] = (byte) 53;
      numArray11[25] = (byte) 18;
      numArray11[20] = (byte) 242;
      numArray11[27] = (byte) 222;
      numArray11[36] = (byte) 118;
      numArray11[42] = (byte) 42;
      numArray11[30] = (byte) 68;
      numArray11[40] = (byte) 197;
      numArray11[45] = (byte) 118;
      numArray11[16 /*0x10*/] = (byte) 248;
      numArray11[46] = (byte) 55;
      numArray11[21] = (byte) 67;
      numArray11[47] = (byte) 13;
      numArray11[37] = (byte) 145;
      numArray11[0] = (byte) 192 /*0xC0*/;
      numArray11[39] = (byte) 189;
      numArray11[3] = (byte) 72;
      numArray11[33] = (byte) 174;
      numArray11[12] = (byte) 196;
      numArray11[5] = (byte) 188;
      numArray11[44] = (byte) 115;
      numArray11[24] = (byte) 129;
      numArray11[17] = (byte) 80 /*0x50*/;
      numArray11[35] = (byte) 192 /*0xC0*/;
      numArray11[48 /*0x30*/] = (byte) 171;
      numArray11[13] = (byte) 73;
      numArray11[50] = (byte) 22;
      numArray11[52] = (byte) 20;
      numArray11[14] = (byte) 33;
      numArray11[51] = (byte) 112 /*0x70*/;
      numArray11[54] = (byte) 117;
      key.Query(true, 335, numArray10, numArray10);
      Array.Copy((Array) numArray10, 0, (Array) numArray1, 220, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 220] ^= numArray11[index];
      byte[] numArray12 = new byte[1]{ (byte) 70 };
      byte[] numArray13 = new byte[1]{ (byte) 225 };
      key.Query(true, 335, numArray12, numArray12);
      Array.Copy((Array) numArray12, 0, (Array) numArray1, 275, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index + 275] ^= numArray13[index];
      byte[] numArray14 = new byte[22];
      byte[] response = new byte[22];
      Array.Copy((Array) sc_12714.sspq, 32 /*0x20*/, (Array) numArray14, 0, 22);
      key.Query(true, 335, numArray14, response);
      Array.Copy((Array) sc_12714.sspr, 32 /*0x20*/, (Array) numArray14, 0, 22);
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
    byte[] numArray15 = new byte[276];
    byte[] numArray16 = new byte[55]
    {
      (byte) 116,
      (byte) 189,
      (byte) 73,
      (byte) 61,
      (byte) 72,
      (byte) 226,
      (byte) 154,
      (byte) 158,
      (byte) 163,
      (byte) 179,
      (byte) 146,
      (byte) 9,
      (byte) 29,
      (byte) 118,
      (byte) 65,
      (byte) 168,
      (byte) 42,
      (byte) 46,
      (byte) 95,
      (byte) 18,
      (byte) 193,
      (byte) 159,
      (byte) 148,
      (byte) 8,
      (byte) 153,
      (byte) 171,
      (byte) 197,
      (byte) 13,
      (byte) 53,
      (byte) 74,
      (byte) 134,
      (byte) 102,
      (byte) 11,
      (byte) 194,
      (byte) 186,
      (byte) 5,
      (byte) 205,
      (byte) 88,
      byte.MaxValue,
      (byte) 157,
      (byte) 63 /*0x3F*/,
      (byte) 190,
      (byte) 85,
      (byte) 44,
      (byte) 248,
      (byte) 114,
      (byte) 91,
      (byte) 162,
      (byte) 247,
      (byte) 72,
      (byte) 57,
      (byte) 233,
      (byte) 245,
      (byte) 14,
      (byte) 137
    };
    byte[] numArray17 = new byte[55]
    {
      (byte) 23,
      (byte) 84,
      (byte) 77,
      (byte) 142,
      (byte) 160 /*0xA0*/,
      (byte) 250,
      (byte) 231,
      (byte) 121,
      (byte) 72,
      (byte) 15,
      (byte) 8,
      (byte) 18,
      (byte) 156,
      (byte) 254,
      (byte) 144 /*0x90*/,
      (byte) 69,
      (byte) 88,
      (byte) 118,
      (byte) 173,
      (byte) 11,
      (byte) 104,
      (byte) 3,
      (byte) 191,
      (byte) 57,
      (byte) 123,
      (byte) 65,
      (byte) 169,
      (byte) 244,
      (byte) 122,
      (byte) 65,
      byte.MaxValue,
      (byte) 169,
      (byte) 37,
      (byte) 16 /*0x10*/,
      (byte) 63 /*0x3F*/,
      (byte) 217,
      (byte) 153,
      (byte) 75,
      (byte) 236,
      (byte) 237,
      (byte) 133,
      (byte) 59,
      (byte) 245,
      (byte) 107,
      (byte) 114,
      (byte) 211,
      (byte) 151,
      (byte) 105,
      (byte) 74,
      (byte) 215,
      (byte) 80 /*0x50*/,
      (byte) 78,
      (byte) 78,
      (byte) 182,
      (byte) 207
    };
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray15, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index] ^= numArray17[index];
    byte[] numArray18 = new byte[55];
    numArray18[19] = (byte) 73;
    numArray18[0] = (byte) 65;
    numArray18[11] = (byte) 187;
    numArray18[15] = (byte) 46;
    numArray18[4] = (byte) 141;
    numArray18[10] = (byte) 90;
    numArray18[52] = (byte) 133;
    numArray18[7] = (byte) 45;
    numArray18[8] = (byte) 44;
    numArray18[17] = (byte) 211;
    numArray18[45] = (byte) 231;
    numArray18[20] = (byte) 103;
    numArray18[12] = (byte) 60;
    numArray18[13] = (byte) 219;
    numArray18[30] = (byte) 252;
    numArray18[50] = (byte) 51;
    numArray18[38] = (byte) 177;
    numArray18[1] = (byte) 163;
    numArray18[21] = (byte) 15;
    numArray18[29] = (byte) 112 /*0x70*/;
    numArray18[5] = (byte) 104;
    numArray18[27] = (byte) 16 /*0x10*/;
    numArray18[22] = (byte) 39;
    numArray18[23] = (byte) 79;
    numArray18[24] = (byte) 35;
    numArray18[39] = (byte) 86;
    numArray18[2] = (byte) 156;
    numArray18[47] = (byte) 108;
    numArray18[36] = (byte) 98;
    numArray18[16 /*0x10*/] = (byte) 193;
    numArray18[6] = (byte) 88;
    numArray18[31 /*0x1F*/] = (byte) 57;
    numArray18[32 /*0x20*/] = (byte) 229;
    numArray18[33] = (byte) 221;
    numArray18[28] = (byte) 156;
    numArray18[18] = (byte) 3;
    numArray18[48 /*0x30*/] = (byte) 104;
    numArray18[53] = (byte) 139;
    numArray18[37] = (byte) 128 /*0x80*/;
    numArray18[34] = (byte) 139;
    numArray18[40] = (byte) 252;
    numArray18[25] = (byte) 21;
    numArray18[42] = (byte) 204;
    numArray18[43] = (byte) 115;
    numArray18[44] = (byte) 1;
    numArray18[3] = (byte) 29;
    numArray18[46] = (byte) 186;
    numArray18[9] = (byte) 254;
    numArray18[35] = (byte) 180;
    numArray18[49] = (byte) 186;
    numArray18[14] = (byte) 209;
    numArray18[51] = (byte) 59;
    numArray18[26] = (byte) 237;
    numArray18[41] = byte.MaxValue;
    numArray18[54] = (byte) 192 /*0xC0*/;
    byte[] numArray19 = new byte[55];
    numArray19[54] = (byte) 200;
    numArray19[1] = (byte) 138;
    numArray19[2] = (byte) 83;
    numArray19[51] = (byte) 216;
    numArray19[8] = (byte) 190;
    numArray19[5] = (byte) 115;
    numArray19[19] = (byte) 7;
    numArray19[40] = (byte) 204;
    numArray19[18] = (byte) 203;
    numArray19[9] = (byte) 156;
    numArray19[36] = (byte) 4;
    numArray19[13] = (byte) 14;
    numArray19[10] = (byte) 190;
    numArray19[7] = (byte) 222;
    numArray19[14] = (byte) 202;
    numArray19[15] = (byte) 154;
    numArray19[16 /*0x10*/] = (byte) 129;
    numArray19[17] = (byte) 74;
    numArray19[11] = (byte) 26;
    numArray19[39] = (byte) 221;
    numArray19[28] = (byte) 232;
    numArray19[4] = (byte) 8;
    numArray19[6] = (byte) 142;
    numArray19[23] = (byte) 84;
    numArray19[24] = (byte) 165;
    numArray19[26] = (byte) 239;
    numArray19[0] = (byte) 72;
    numArray19[27] = (byte) 248;
    numArray19[35] = (byte) 238;
    numArray19[29] = (byte) 90;
    numArray19[30] = (byte) 77;
    numArray19[31 /*0x1F*/] = (byte) 180;
    numArray19[32 /*0x20*/] = (byte) 75;
    numArray19[48 /*0x30*/] = (byte) 87;
    numArray19[34] = (byte) 168;
    numArray19[38] = (byte) 17;
    numArray19[45] = (byte) 99;
    numArray19[37] = (byte) 64 /*0x40*/;
    numArray19[3] = (byte) 15;
    numArray19[22] = (byte) 160 /*0xA0*/;
    numArray19[49] = (byte) 235;
    numArray19[41] = (byte) 5;
    numArray19[42] = (byte) 116;
    numArray19[43] = (byte) 201;
    numArray19[44] = (byte) 101;
    numArray19[12] = (byte) 128 /*0x80*/;
    numArray19[46] = (byte) 228;
    numArray19[25] = (byte) 51;
    numArray19[33] = (byte) 42;
    numArray19[52] = (byte) 73;
    numArray19[50] = (byte) 251;
    numArray19[21] = (byte) 104;
    numArray19[47] = (byte) 169;
    numArray19[53] = byte.MaxValue;
    numArray19[20] = (byte) 35;
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray15, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 55] ^= numArray19[index];
    byte[] numArray20 = new byte[55]
    {
      (byte) 144 /*0x90*/,
      (byte) 148,
      (byte) 29,
      (byte) 150,
      (byte) 64 /*0x40*/,
      (byte) 185,
      (byte) 51,
      (byte) 152,
      (byte) 144 /*0x90*/,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 142,
      (byte) 117,
      (byte) 158,
      (byte) 242,
      (byte) 202,
      (byte) 240 /*0xF0*/,
      (byte) 129,
      (byte) 56,
      (byte) 158,
      (byte) 16 /*0x10*/,
      (byte) 250,
      (byte) 147,
      (byte) 230,
      (byte) 61,
      (byte) 248,
      (byte) 187,
      (byte) 19,
      (byte) 117,
      (byte) 212,
      (byte) 117,
      (byte) 100,
      (byte) 168,
      (byte) 232,
      (byte) 134,
      (byte) 51,
      byte.MaxValue,
      (byte) 74,
      (byte) 213,
      (byte) 6,
      (byte) 95,
      (byte) 152,
      (byte) 226,
      (byte) 55,
      (byte) 153,
      (byte) 151,
      (byte) 240 /*0xF0*/,
      (byte) 153,
      (byte) 68,
      (byte) 102,
      (byte) 21,
      (byte) 37,
      (byte) 38,
      (byte) 33,
      (byte) 119
    };
    byte[] numArray21 = new byte[55]
    {
      (byte) 97,
      (byte) 6,
      (byte) 53,
      (byte) 66,
      (byte) 78,
      (byte) 183,
      (byte) 51,
      (byte) 152,
      (byte) 133,
      (byte) 133,
      (byte) 91,
      (byte) 94,
      (byte) 67,
      (byte) 72,
      (byte) 209,
      (byte) 85,
      (byte) 48 /*0x30*/,
      (byte) 185,
      (byte) 97,
      (byte) 135,
      (byte) 118,
      (byte) 129,
      (byte) 106,
      (byte) 22,
      (byte) 125,
      (byte) 201,
      (byte) 111,
      (byte) 90,
      (byte) 196,
      (byte) 76,
      (byte) 78,
      (byte) 43,
      (byte) 145,
      (byte) 60,
      (byte) 40,
      (byte) 234,
      (byte) 40,
      (byte) 177,
      (byte) 126,
      (byte) 5,
      (byte) 27,
      (byte) 106,
      (byte) 245,
      (byte) 30,
      (byte) 66,
      (byte) 50,
      (byte) 73,
      (byte) 116,
      (byte) 99,
      (byte) 140,
      (byte) 146,
      (byte) 219,
      (byte) 128 /*0x80*/,
      (byte) 14,
      (byte) 232
    };
    key.Query(true, 335, numArray20, numArray20);
    Array.Copy((Array) numArray20, 0, (Array) numArray15, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 110] ^= numArray21[index];
    byte[] numArray22 = new byte[55];
    numArray22[11] = (byte) 23;
    numArray22[1] = (byte) 88;
    numArray22[2] = (byte) 72;
    numArray22[3] = (byte) 254;
    numArray22[27] = (byte) 191;
    numArray22[9] = (byte) 229;
    numArray22[7] = (byte) 233;
    numArray22[30] = (byte) 135;
    numArray22[46] = (byte) 133;
    numArray22[18] = (byte) 119;
    numArray22[5] = (byte) 34;
    numArray22[24] = (byte) 34;
    numArray22[12] = (byte) 190;
    numArray22[13] = (byte) 203;
    numArray22[39] = (byte) 118;
    numArray22[50] = (byte) 244;
    numArray22[51] = (byte) 226;
    numArray22[17] = (byte) 63 /*0x3F*/;
    numArray22[44] = (byte) 99;
    numArray22[19] = (byte) 23;
    numArray22[36] = (byte) 140;
    numArray22[48 /*0x30*/] = (byte) 111;
    numArray22[22] = (byte) 166;
    numArray22[15] = (byte) 171;
    numArray22[26] = (byte) 15;
    numArray22[25] = (byte) 206;
    numArray22[6] = (byte) 188;
    numArray22[28] = (byte) 80 /*0x50*/;
    numArray22[40] = (byte) 38;
    numArray22[29] = (byte) 61;
    numArray22[14] = (byte) 166;
    numArray22[31 /*0x1F*/] = (byte) 113;
    numArray22[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    numArray22[47] = (byte) 155;
    numArray22[0] = (byte) 54;
    numArray22[54] = (byte) 164;
    numArray22[35] = (byte) 241;
    numArray22[4] = (byte) 32 /*0x20*/;
    numArray22[38] = (byte) 62;
    numArray22[37] = (byte) 8;
    numArray22[10] = (byte) 140;
    numArray22[41] = (byte) 78;
    numArray22[8] = (byte) 26;
    numArray22[49] = (byte) 121;
    numArray22[42] = (byte) 126;
    numArray22[45] = (byte) 129;
    numArray22[53] = (byte) 127 /*0x7F*/;
    numArray22[23] = (byte) 7;
    numArray22[33] = (byte) 51;
    numArray22[16 /*0x10*/] = (byte) 49;
    numArray22[43] = (byte) 177;
    numArray22[21] = (byte) 77;
    numArray22[52] = (byte) 0;
    numArray22[20] = (byte) 92;
    numArray22[34] = (byte) 200;
    byte[] numArray23 = new byte[55]
    {
      (byte) 227,
      (byte) 217,
      (byte) 35,
      (byte) 150,
      (byte) 121,
      (byte) 164,
      (byte) 254,
      (byte) 249,
      byte.MaxValue,
      (byte) 100,
      (byte) 218,
      (byte) 119,
      (byte) 229,
      (byte) 16 /*0x10*/,
      (byte) 167,
      (byte) 128 /*0x80*/,
      (byte) 72,
      (byte) 68,
      (byte) 231,
      (byte) 190,
      (byte) 26,
      (byte) 91,
      (byte) 127 /*0x7F*/,
      (byte) 43,
      (byte) 91,
      (byte) 201,
      (byte) 87,
      (byte) 30,
      (byte) 174,
      (byte) 252,
      (byte) 134,
      (byte) 21,
      (byte) 223,
      (byte) 168,
      (byte) 12,
      (byte) 173,
      (byte) 84,
      (byte) 112 /*0x70*/,
      (byte) 122,
      (byte) 77,
      (byte) 243,
      (byte) 156,
      (byte) 6,
      (byte) 83,
      (byte) 165,
      (byte) 73,
      (byte) 86,
      (byte) 148,
      (byte) 96 /*0x60*/,
      (byte) 237,
      (byte) 242,
      (byte) 201,
      (byte) 242,
      (byte) 170,
      (byte) 105
    };
    key.Query(true, 335, numArray22, numArray22);
    Array.Copy((Array) numArray22, 0, (Array) numArray15, 165, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 165] ^= numArray23[index];
    byte[] numArray24 = new byte[55];
    numArray24[20] = (byte) 161;
    numArray24[11] = (byte) 35;
    numArray24[25] = (byte) 148;
    numArray24[3] = (byte) 160 /*0xA0*/;
    numArray24[30] = (byte) 226;
    numArray24[7] = (byte) 184;
    numArray24[6] = (byte) 148;
    numArray24[51] = (byte) 200;
    numArray24[45] = (byte) 118;
    numArray24[32 /*0x20*/] = (byte) 195;
    numArray24[5] = (byte) 193;
    numArray24[35] = (byte) 243;
    numArray24[31 /*0x1F*/] = (byte) 249;
    numArray24[21] = (byte) 91;
    numArray24[8] = (byte) 170;
    numArray24[2] = (byte) 9;
    numArray24[13] = (byte) 212;
    numArray24[17] = (byte) 198;
    numArray24[18] = (byte) 88;
    numArray24[19] = (byte) 44;
    numArray24[34] = (byte) 173;
    numArray24[44] = (byte) 254;
    numArray24[49] = (byte) 205;
    numArray24[23] = (byte) 78;
    numArray24[16 /*0x10*/] = (byte) 158;
    numArray24[15] = (byte) 20;
    numArray24[26] = (byte) 125;
    numArray24[48 /*0x30*/] = (byte) 208 /*0xD0*/;
    numArray24[9] = (byte) 182;
    numArray24[29] = (byte) 114;
    numArray24[27] = (byte) 124;
    numArray24[42] = (byte) 157;
    numArray24[10] = (byte) 252;
    numArray24[33] = (byte) 129;
    numArray24[54] = (byte) 27;
    numArray24[14] = (byte) 251;
    numArray24[36] = (byte) 226;
    numArray24[22] = (byte) 71;
    numArray24[4] = (byte) 173;
    numArray24[39] = (byte) 243;
    numArray24[40] = (byte) 13;
    numArray24[41] = (byte) 155;
    numArray24[28] = (byte) 196;
    numArray24[43] = (byte) 163;
    numArray24[37] = (byte) 194;
    numArray24[0] = (byte) 157;
    numArray24[46] = (byte) 69;
    numArray24[47] = (byte) 133;
    numArray24[38] = (byte) 124;
    numArray24[24] = (byte) 217;
    numArray24[50] = (byte) 96 /*0x60*/;
    numArray24[12] = (byte) 117;
    numArray24[52] = (byte) 61;
    numArray24[53] = (byte) 238;
    numArray24[1] = (byte) 58;
    byte[] numArray25 = new byte[55]
    {
      (byte) 47,
      (byte) 171,
      (byte) 132,
      (byte) 137,
      (byte) 194,
      (byte) 11,
      (byte) 203,
      (byte) 254,
      (byte) 214,
      (byte) 68,
      (byte) 94,
      (byte) 53,
      (byte) 15,
      (byte) 137,
      (byte) 19,
      (byte) 195,
      (byte) 133,
      (byte) 129,
      (byte) 0,
      (byte) 46,
      (byte) 249,
      (byte) 88,
      (byte) 214,
      (byte) 230,
      (byte) 56,
      (byte) 185,
      (byte) 48 /*0x30*/,
      (byte) 44,
      (byte) 180,
      (byte) 180,
      (byte) 127 /*0x7F*/,
      (byte) 139,
      (byte) 200,
      (byte) 209,
      (byte) 210,
      (byte) 192 /*0xC0*/,
      (byte) 99,
      (byte) 217,
      (byte) 42,
      (byte) 147,
      (byte) 181,
      (byte) 3,
      (byte) 151,
      (byte) 33,
      (byte) 132,
      (byte) 9,
      (byte) 122,
      (byte) 17,
      (byte) 191,
      (byte) 64 /*0x40*/,
      (byte) 123,
      (byte) 7,
      (byte) 223,
      (byte) 192 /*0xC0*/,
      (byte) 188
    };
    key.Query(true, 335, numArray24, numArray24);
    Array.Copy((Array) numArray24, 0, (Array) numArray15, 220, 55);
    for (int index = 0; index < 55; ++index)
      numArray15[index + 220] ^= numArray25[index];
    byte[] numArray26 = new byte[1]{ (byte) 107 };
    byte[] numArray27 = new byte[1]{ (byte) 27 };
    key.Query(true, 335, numArray26, numArray26);
    Array.Copy((Array) numArray26, 0, (Array) numArray15, 275, 1);
    for (int index = 0; index < 1; ++index)
      numArray15[index + 275] ^= numArray27[index];
    return Encoding.UTF8.GetString(numArray15);
  }

  internal static string ssp_appserver_12720()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[104];
      byte[] numArray2 = new byte[55];
      numArray2[52] = (byte) 134;
      numArray2[1] = (byte) 71;
      numArray2[6] = (byte) 9;
      numArray2[34] = (byte) 22;
      numArray2[32 /*0x20*/] = (byte) 169;
      numArray2[16 /*0x10*/] = (byte) 253;
      numArray2[51] = (byte) 178;
      numArray2[10] = (byte) 40;
      numArray2[24] = (byte) 81;
      numArray2[38] = (byte) 162;
      numArray2[9] = (byte) 181;
      numArray2[11] = (byte) 95;
      numArray2[12] = (byte) 88;
      numArray2[13] = (byte) 211;
      numArray2[20] = (byte) 140;
      numArray2[15] = (byte) 75;
      numArray2[27] = (byte) 187;
      numArray2[21] = (byte) 155;
      numArray2[18] = (byte) 228;
      numArray2[36] = (byte) 141;
      numArray2[47] = (byte) 123;
      numArray2[44] = (byte) 166;
      numArray2[49] = (byte) 24;
      numArray2[3] = (byte) 141;
      numArray2[33] = (byte) 62;
      numArray2[31 /*0x1F*/] = (byte) 139;
      numArray2[48 /*0x30*/] = (byte) 22;
      numArray2[14] = (byte) 84;
      numArray2[28] = (byte) 151;
      numArray2[29] = (byte) 155;
      numArray2[30] = (byte) 99;
      numArray2[17] = (byte) 153;
      numArray2[2] = (byte) 137;
      numArray2[23] = (byte) 6;
      numArray2[7] = (byte) 225;
      numArray2[35] = (byte) 180;
      numArray2[22] = (byte) 228;
      numArray2[37] = (byte) 196;
      numArray2[19] = (byte) 90;
      numArray2[39] = (byte) 191;
      numArray2[40] = (byte) 146;
      numArray2[41] = (byte) 163;
      numArray2[5] = (byte) 8;
      numArray2[43] = (byte) 34;
      numArray2[26] = (byte) 177;
      numArray2[45] = (byte) 181;
      numArray2[46] = (byte) 42;
      numArray2[42] = (byte) 153;
      numArray2[25] = (byte) 231;
      numArray2[53] = (byte) 162;
      numArray2[50] = (byte) 202;
      numArray2[0] = (byte) 185;
      numArray2[54] = (byte) 154;
      numArray2[8] = (byte) 229;
      numArray2[4] = (byte) 187;
      byte[] numArray3 = new byte[55]
      {
        (byte) 6,
        (byte) 185,
        (byte) 110,
        (byte) 229,
        (byte) 28,
        (byte) 242,
        (byte) 198,
        (byte) 149,
        (byte) 56,
        (byte) 155,
        (byte) 195,
        byte.MaxValue,
        (byte) 194,
        (byte) 123,
        (byte) 2,
        (byte) 0,
        (byte) 166,
        (byte) 109,
        (byte) 139,
        (byte) 240 /*0xF0*/,
        (byte) 129,
        (byte) 111,
        (byte) 229,
        (byte) 244,
        (byte) 87,
        (byte) 252,
        (byte) 170,
        (byte) 85,
        (byte) 209,
        (byte) 245,
        (byte) 83,
        (byte) 248,
        (byte) 71,
        (byte) 25,
        (byte) 10,
        (byte) 188,
        (byte) 44,
        (byte) 160 /*0xA0*/,
        (byte) 175,
        (byte) 72,
        (byte) 165,
        (byte) 101,
        (byte) 1,
        (byte) 46,
        (byte) 232,
        (byte) 153,
        (byte) 29,
        (byte) 21,
        (byte) 160 /*0xA0*/,
        (byte) 81,
        (byte) 134,
        (byte) 91,
        (byte) 53,
        (byte) 23,
        (byte) 146
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49]
      {
        (byte) 34,
        (byte) 158,
        (byte) 22,
        (byte) 33,
        (byte) 168,
        (byte) 202,
        (byte) 121,
        (byte) 159,
        (byte) 152,
        (byte) 173,
        (byte) 118,
        (byte) 195,
        (byte) 79,
        (byte) 179,
        (byte) 176 /*0xB0*/,
        (byte) 41,
        (byte) 187,
        (byte) 132,
        (byte) 144 /*0x90*/,
        (byte) 179,
        (byte) 240 /*0xF0*/,
        (byte) 87,
        (byte) 157,
        (byte) 172,
        (byte) 118,
        (byte) 46,
        (byte) 36,
        (byte) 63 /*0x3F*/,
        (byte) 247,
        (byte) 162,
        (byte) 46,
        (byte) 243,
        (byte) 222,
        (byte) 175,
        (byte) 231,
        (byte) 234,
        (byte) 183,
        (byte) 71,
        (byte) 155,
        (byte) 142,
        (byte) 213,
        (byte) 49,
        (byte) 111,
        (byte) 81,
        (byte) 153,
        (byte) 251,
        (byte) 38,
        (byte) 167,
        (byte) 224 /*0xE0*/
      };
      byte[] numArray5 = new byte[49]
      {
        (byte) 90,
        (byte) 243,
        (byte) 76,
        (byte) 55,
        (byte) 215,
        (byte) 180,
        (byte) 16 /*0x10*/,
        (byte) 178,
        (byte) 15,
        (byte) 150,
        (byte) 230,
        (byte) 102,
        (byte) 74,
        (byte) 152,
        (byte) 116,
        (byte) 74,
        (byte) 164,
        (byte) 248,
        (byte) 239,
        (byte) 145,
        (byte) 144 /*0x90*/,
        (byte) 26,
        (byte) 14,
        (byte) 156,
        (byte) 118,
        (byte) 97,
        (byte) 204,
        (byte) 18,
        (byte) 235,
        (byte) 128 /*0x80*/,
        (byte) 83,
        (byte) 116,
        (byte) 24,
        (byte) 119,
        (byte) 88,
        (byte) 49,
        (byte) 47,
        (byte) 219,
        (byte) 154,
        (byte) 120,
        (byte) 48 /*0x30*/,
        (byte) 157,
        (byte) 2,
        (byte) 121,
        (byte) 188,
        (byte) 197,
        (byte) 140,
        (byte) 86,
        (byte) 130
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
      (byte) 32 /*0x20*/,
      (byte) 103,
      (byte) 177,
      (byte) 203,
      (byte) 1,
      (byte) 106,
      (byte) 170,
      (byte) 121,
      (byte) 166,
      (byte) 57,
      (byte) 168,
      (byte) 246,
      (byte) 127 /*0x7F*/,
      (byte) 130,
      (byte) 229,
      (byte) 159,
      (byte) 203,
      (byte) 198,
      (byte) 113,
      (byte) 116,
      (byte) 233,
      (byte) 1,
      (byte) 221,
      (byte) 190,
      (byte) 231,
      (byte) 75,
      (byte) 19,
      (byte) 123,
      (byte) 8,
      (byte) 125,
      (byte) 94,
      (byte) 101,
      (byte) 251,
      (byte) 217,
      (byte) 210,
      (byte) 118,
      (byte) 254,
      (byte) 28,
      (byte) 232,
      (byte) 77,
      (byte) 156,
      (byte) 33,
      (byte) 232,
      (byte) 155,
      (byte) 137,
      (byte) 121,
      (byte) 210,
      (byte) 22,
      (byte) 21,
      (byte) 134,
      (byte) 219,
      (byte) 26,
      (byte) 32 /*0x20*/,
      (byte) 49,
      (byte) 107
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 192 /*0xC0*/,
      (byte) 69,
      (byte) 196,
      (byte) 83,
      (byte) 189,
      (byte) 108,
      (byte) 189,
      (byte) 154,
      (byte) 226,
      (byte) 120,
      (byte) 172,
      (byte) 211,
      (byte) 3,
      (byte) 141,
      (byte) 69,
      (byte) 189,
      (byte) 231,
      (byte) 23,
      (byte) 20,
      (byte) 172,
      (byte) 205,
      (byte) 61,
      (byte) 225,
      (byte) 3,
      (byte) 116,
      (byte) 183,
      (byte) 79,
      (byte) 175,
      (byte) 207,
      (byte) 151,
      (byte) 97,
      (byte) 116,
      (byte) 124,
      (byte) 27,
      (byte) 204,
      (byte) 0,
      (byte) 225,
      (byte) 183,
      (byte) 133,
      (byte) 151,
      (byte) 80 /*0x50*/,
      (byte) 55,
      (byte) 69,
      (byte) 231,
      (byte) 128 /*0x80*/,
      (byte) 76,
      (byte) 170,
      (byte) 253,
      (byte) 161,
      (byte) 132,
      (byte) 162,
      (byte) 137,
      (byte) 107,
      (byte) 198,
      (byte) 219
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[49]
    {
      (byte) 243,
      (byte) 227,
      (byte) 182,
      (byte) 86,
      (byte) 47,
      (byte) 113,
      (byte) 40,
      (byte) 219,
      (byte) 66,
      (byte) 251,
      (byte) 165,
      (byte) 176 /*0xB0*/,
      (byte) 110,
      (byte) 161,
      (byte) 175,
      (byte) 222,
      (byte) 36,
      (byte) 216,
      (byte) 199,
      (byte) 25,
      (byte) 66,
      (byte) 52,
      (byte) 110,
      (byte) 121,
      (byte) 252,
      (byte) 101,
      (byte) 129,
      (byte) 161,
      (byte) 67,
      (byte) 69,
      (byte) 201,
      (byte) 110,
      (byte) 179,
      (byte) 237,
      (byte) 10,
      (byte) 56,
      (byte) 49,
      (byte) 246,
      (byte) 109,
      (byte) 175,
      (byte) 191,
      (byte) 192 /*0xC0*/,
      (byte) 70,
      (byte) 126,
      (byte) 44,
      (byte) 102,
      (byte) 167,
      (byte) 228,
      (byte) 44
    };
    byte[] numArray10 = new byte[49];
    numArray10[37] = (byte) 166;
    numArray10[44] = (byte) 213;
    numArray10[48 /*0x30*/] = (byte) 248;
    numArray10[12] = (byte) 172;
    numArray10[4] = (byte) 142;
    numArray10[35] = (byte) 195;
    numArray10[29] = (byte) 194;
    numArray10[0] = (byte) 77;
    numArray10[10] = (byte) 3;
    numArray10[9] = (byte) 83;
    numArray10[24] = (byte) 229;
    numArray10[3] = byte.MaxValue;
    numArray10[2] = (byte) 206;
    numArray10[42] = (byte) 52;
    numArray10[43] = (byte) 197;
    numArray10[15] = (byte) 148;
    numArray10[7] = (byte) 70;
    numArray10[8] = (byte) 193;
    numArray10[47] = (byte) 10;
    numArray10[19] = (byte) 125;
    numArray10[20] = (byte) 127 /*0x7F*/;
    numArray10[21] = (byte) 201;
    numArray10[1] = (byte) 223;
    numArray10[23] = (byte) 150;
    numArray10[46] = (byte) 43;
    numArray10[6] = (byte) 95;
    numArray10[26] = (byte) 52;
    numArray10[27] = (byte) 171;
    numArray10[22] = (byte) 10;
    numArray10[16 /*0x10*/] = (byte) 11;
    numArray10[17] = (byte) 53;
    numArray10[31 /*0x1F*/] = (byte) 11;
    numArray10[32 /*0x20*/] = (byte) 139;
    numArray10[33] = (byte) 147;
    numArray10[39] = (byte) 97;
    numArray10[25] = (byte) 57;
    numArray10[36] = (byte) 85;
    numArray10[40] = (byte) 117;
    numArray10[38] = (byte) 63 /*0x3F*/;
    numArray10[11] = (byte) 112 /*0x70*/;
    numArray10[13] = (byte) 188;
    numArray10[41] = (byte) 46;
    numArray10[30] = (byte) 207;
    numArray10[14] = (byte) 102;
    numArray10[5] = (byte) 30;
    numArray10[45] = (byte) 93;
    numArray10[18] = (byte) 0;
    numArray10[34] = (byte) 83;
    numArray10[28] = (byte) 27;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 49);
    for (int index = 0; index < 49; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[29];
    byte[] response = new byte[29];
    Array.Copy((Array) sc_12714.sspq, 54, (Array) numArray11, 0, 29);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12714.sspr, 54, (Array) numArray11, 0, 29);
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

  internal static string ssp_appserver_12721()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[106];
      byte[] numArray2 = new byte[55]
      {
        (byte) 163,
        (byte) 88,
        (byte) 44,
        (byte) 151,
        (byte) 79,
        (byte) 191,
        (byte) 119,
        (byte) 123,
        (byte) 130,
        (byte) 63 /*0x3F*/,
        (byte) 1,
        (byte) 224 /*0xE0*/,
        (byte) 123,
        (byte) 62,
        (byte) 5,
        (byte) 216,
        (byte) 152,
        (byte) 109,
        (byte) 38,
        (byte) 152,
        (byte) 151,
        (byte) 140,
        (byte) 136,
        (byte) 26,
        (byte) 75,
        (byte) 3,
        (byte) 2,
        (byte) 216,
        (byte) 61,
        (byte) 38,
        (byte) 51,
        (byte) 138,
        (byte) 109,
        (byte) 44,
        (byte) 225,
        (byte) 151,
        (byte) 151,
        (byte) 115,
        (byte) 2,
        (byte) 82,
        (byte) 25,
        (byte) 55,
        (byte) 61,
        (byte) 44,
        (byte) 77,
        (byte) 250,
        (byte) 206,
        (byte) 117,
        (byte) 178,
        (byte) 183,
        (byte) 46,
        (byte) 233,
        (byte) 251,
        (byte) 192 /*0xC0*/,
        (byte) 12
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 115,
        (byte) 75,
        (byte) 242,
        (byte) 4,
        (byte) 39,
        (byte) 103,
        (byte) 90,
        (byte) 40,
        (byte) 165,
        (byte) 220,
        (byte) 23,
        (byte) 242,
        (byte) 160 /*0xA0*/,
        (byte) 126,
        (byte) 135,
        (byte) 168,
        (byte) 96 /*0x60*/,
        (byte) 230,
        (byte) 56,
        (byte) 150,
        (byte) 153,
        (byte) 159,
        (byte) 33,
        (byte) 227,
        (byte) 199,
        (byte) 166,
        (byte) 205,
        (byte) 254,
        (byte) 236,
        (byte) 253,
        (byte) 250,
        (byte) 213,
        (byte) 155,
        (byte) 201,
        (byte) 71,
        (byte) 136,
        (byte) 2,
        (byte) 131,
        (byte) 243,
        (byte) 213,
        (byte) 80 /*0x50*/,
        (byte) 50,
        (byte) 197,
        (byte) 55,
        (byte) 179,
        (byte) 170,
        (byte) 206,
        (byte) 176 /*0xB0*/,
        (byte) 104,
        (byte) 15,
        (byte) 70,
        (byte) 211,
        (byte) 170,
        (byte) 236,
        (byte) 144 /*0x90*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[51]
      {
        (byte) 17,
        (byte) 76,
        (byte) 76,
        (byte) 188,
        (byte) 158,
        (byte) 61,
        (byte) 92,
        (byte) 12,
        (byte) 119,
        (byte) 115,
        (byte) 79,
        (byte) 92,
        (byte) 109,
        (byte) 65,
        (byte) 37,
        (byte) 107,
        (byte) 211,
        (byte) 127 /*0x7F*/,
        (byte) 95,
        (byte) 79,
        (byte) 91,
        (byte) 205,
        (byte) 80 /*0x50*/,
        (byte) 52,
        (byte) 141,
        (byte) 38,
        (byte) 134,
        (byte) 248,
        (byte) 73,
        (byte) 142,
        (byte) 173,
        (byte) 32 /*0x20*/,
        (byte) 203,
        (byte) 199,
        (byte) 134,
        (byte) 38,
        (byte) 182,
        (byte) 150,
        (byte) 122,
        (byte) 250,
        (byte) 225,
        (byte) 32 /*0x20*/,
        (byte) 214,
        (byte) 135,
        (byte) 127 /*0x7F*/,
        (byte) 24,
        (byte) 68,
        (byte) 118,
        (byte) 8,
        (byte) 126,
        (byte) 40
      };
      byte[] numArray5 = new byte[51]
      {
        (byte) 56,
        (byte) 63 /*0x3F*/,
        (byte) 144 /*0x90*/,
        (byte) 160 /*0xA0*/,
        (byte) 216,
        (byte) 199,
        (byte) 122,
        (byte) 63 /*0x3F*/,
        (byte) 181,
        (byte) 29,
        (byte) 76,
        (byte) 178,
        (byte) 26,
        (byte) 137,
        (byte) 233,
        (byte) 5,
        (byte) 75,
        (byte) 201,
        (byte) 203,
        (byte) 61,
        (byte) 57,
        (byte) 167,
        (byte) 251,
        (byte) 17,
        (byte) 184,
        (byte) 79,
        (byte) 132,
        (byte) 10,
        (byte) 140,
        (byte) 69,
        (byte) 88,
        (byte) 91,
        (byte) 234,
        (byte) 154,
        (byte) 145,
        (byte) 131,
        (byte) 19,
        (byte) 82,
        (byte) 242,
        (byte) 58,
        (byte) 48 /*0x30*/,
        (byte) 92,
        (byte) 24,
        (byte) 187,
        (byte) 141,
        (byte) 137,
        (byte) 175,
        (byte) 233,
        (byte) 136,
        (byte) 13,
        (byte) 220
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 51);
      for (int index = 0; index < 51; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[106];
    byte[] numArray7 = new byte[55];
    numArray7[40] = (byte) 67;
    numArray7[1] = (byte) 67;
    numArray7[2] = (byte) 251;
    numArray7[25] = (byte) 116;
    numArray7[32 /*0x20*/] = (byte) 114;
    numArray7[31 /*0x1F*/] = (byte) 108;
    numArray7[45] = (byte) 80 /*0x50*/;
    numArray7[46] = (byte) 136;
    numArray7[19] = (byte) 100;
    numArray7[9] = (byte) 232;
    numArray7[52] = (byte) 59;
    numArray7[43] = (byte) 237;
    numArray7[39] = (byte) 2;
    numArray7[4] = (byte) 46;
    numArray7[14] = (byte) 186;
    numArray7[21] = (byte) 59;
    numArray7[27] = (byte) 15;
    numArray7[17] = (byte) 60;
    numArray7[28] = (byte) 239;
    numArray7[12] = (byte) 144 /*0x90*/;
    numArray7[54] = (byte) 89;
    numArray7[0] = (byte) 116;
    numArray7[22] = (byte) 247;
    numArray7[18] = (byte) 4;
    numArray7[24] = (byte) 180;
    numArray7[7] = (byte) 99;
    numArray7[26] = (byte) 225;
    numArray7[49] = (byte) 9;
    numArray7[35] = (byte) 66;
    numArray7[15] = byte.MaxValue;
    numArray7[30] = (byte) 102;
    numArray7[10] = (byte) 198;
    numArray7[48 /*0x30*/] = (byte) 187;
    numArray7[33] = (byte) 3;
    numArray7[34] = (byte) 82;
    numArray7[41] = (byte) 125;
    numArray7[36] = (byte) 43;
    numArray7[37] = (byte) 157;
    numArray7[38] = (byte) 104;
    numArray7[44] = (byte) 193;
    numArray7[11] = (byte) 46;
    numArray7[5] = (byte) 119;
    numArray7[42] = (byte) 93;
    numArray7[20] = (byte) 109;
    numArray7[3] = (byte) 24;
    numArray7[6] = (byte) 183;
    numArray7[29] = (byte) 202;
    numArray7[47] = (byte) 157;
    numArray7[23] = (byte) 201;
    numArray7[16 /*0x10*/] = (byte) 180;
    numArray7[50] = (byte) 201;
    numArray7[51] = (byte) 235;
    numArray7[13] = (byte) 166;
    numArray7[53] = (byte) 72;
    numArray7[8] = (byte) 194;
    byte[] numArray8 = new byte[55]
    {
      (byte) 111,
      (byte) 241,
      (byte) 202,
      (byte) 90,
      (byte) 69,
      (byte) 162,
      (byte) 229,
      (byte) 228,
      (byte) 159,
      (byte) 54,
      (byte) 111,
      (byte) 227,
      (byte) 113,
      (byte) 87,
      (byte) 130,
      (byte) 22,
      (byte) 226,
      (byte) 201,
      (byte) 95,
      (byte) 191,
      (byte) 140,
      (byte) 97,
      (byte) 97,
      (byte) 160 /*0xA0*/,
      (byte) 183,
      (byte) 26,
      (byte) 132,
      (byte) 116,
      (byte) 1,
      (byte) 138,
      (byte) 214,
      (byte) 32 /*0x20*/,
      (byte) 106,
      (byte) 37,
      (byte) 249,
      (byte) 122,
      (byte) 52,
      (byte) 13,
      (byte) 13,
      (byte) 11,
      (byte) 56,
      (byte) 158,
      (byte) 56,
      (byte) 78,
      (byte) 104,
      (byte) 181,
      (byte) 251,
      (byte) 71,
      (byte) 82,
      (byte) 108,
      (byte) 208 /*0xD0*/,
      (byte) 103,
      (byte) 1,
      (byte) 238,
      (byte) 80 /*0x50*/
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[51]
    {
      (byte) 42,
      byte.MaxValue,
      (byte) 205,
      (byte) 82,
      (byte) 83,
      (byte) 142,
      (byte) 247,
      (byte) 5,
      (byte) 40,
      (byte) 145,
      (byte) 226,
      (byte) 59,
      (byte) 130,
      (byte) 219,
      (byte) 220,
      (byte) 38,
      (byte) 149,
      (byte) 133,
      (byte) 230,
      (byte) 119,
      (byte) 156,
      (byte) 160 /*0xA0*/,
      (byte) 207,
      (byte) 241,
      (byte) 250,
      (byte) 16 /*0x10*/,
      (byte) 176 /*0xB0*/,
      (byte) 77,
      (byte) 101,
      (byte) 248,
      (byte) 3,
      (byte) 63 /*0x3F*/,
      (byte) 188,
      (byte) 179,
      (byte) 206,
      (byte) 58,
      (byte) 224 /*0xE0*/,
      (byte) 26,
      (byte) 72,
      (byte) 122,
      (byte) 110,
      (byte) 103,
      (byte) 63 /*0x3F*/,
      (byte) 2,
      (byte) 24,
      (byte) 13,
      (byte) 193,
      (byte) 89,
      (byte) 214,
      (byte) 239,
      (byte) 163
    };
    byte[] numArray10 = new byte[51]
    {
      (byte) 49,
      (byte) 190,
      (byte) 143,
      (byte) 127 /*0x7F*/,
      (byte) 153,
      (byte) 34,
      (byte) 74,
      (byte) 155,
      (byte) 76,
      (byte) 119,
      (byte) 177,
      (byte) 252,
      (byte) 238,
      (byte) 214,
      (byte) 80 /*0x50*/,
      (byte) 232,
      (byte) 181,
      (byte) 58,
      (byte) 189,
      (byte) 242,
      (byte) 239,
      (byte) 125,
      (byte) 42,
      (byte) 65,
      (byte) 60,
      (byte) 74,
      (byte) 121,
      (byte) 13,
      (byte) 225,
      (byte) 52,
      (byte) 80 /*0x50*/,
      (byte) 52,
      (byte) 138,
      (byte) 189,
      (byte) 110,
      (byte) 40,
      (byte) 181,
      (byte) 106,
      (byte) 31 /*0x1F*/,
      (byte) 6,
      (byte) 146,
      (byte) 230,
      (byte) 40,
      (byte) 27,
      (byte) 173,
      (byte) 234,
      (byte) 233,
      (byte) 154,
      (byte) 49,
      (byte) 5,
      (byte) 108
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 51);
    for (int index = 0; index < 51; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12722()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[138];
      byte[] numArray2 = new byte[55]
      {
        (byte) 137,
        (byte) 47,
        (byte) 220,
        (byte) 87,
        (byte) 67,
        (byte) 221,
        (byte) 169,
        (byte) 160 /*0xA0*/,
        (byte) 48 /*0x30*/,
        (byte) 142,
        (byte) 162,
        (byte) 127 /*0x7F*/,
        (byte) 206,
        (byte) 12,
        (byte) 161,
        (byte) 69,
        (byte) 123,
        (byte) 61,
        (byte) 107,
        (byte) 9,
        (byte) 53,
        (byte) 97,
        (byte) 204,
        (byte) 216,
        (byte) 236,
        (byte) 78,
        (byte) 86,
        (byte) 119,
        (byte) 117,
        (byte) 84,
        (byte) 97,
        (byte) 188,
        (byte) 216,
        (byte) 12,
        (byte) 68,
        (byte) 11,
        (byte) 4,
        (byte) 195,
        (byte) 5,
        (byte) 157,
        (byte) 196,
        (byte) 198,
        (byte) 15,
        (byte) 116,
        (byte) 117,
        (byte) 142,
        (byte) 242,
        (byte) 77,
        (byte) 52,
        (byte) 170,
        (byte) 40,
        (byte) 100,
        (byte) 23,
        (byte) 219,
        (byte) 113
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 53,
        (byte) 143,
        (byte) 220,
        (byte) 185,
        (byte) 135,
        (byte) 193,
        (byte) 144 /*0x90*/,
        (byte) 0,
        (byte) 160 /*0xA0*/,
        (byte) 229,
        (byte) 132,
        (byte) 52,
        (byte) 90,
        (byte) 56,
        (byte) 3,
        (byte) 83,
        (byte) 27,
        (byte) 162,
        (byte) 48 /*0x30*/,
        (byte) 250,
        (byte) 27,
        (byte) 174,
        (byte) 18,
        (byte) 75,
        (byte) 146,
        (byte) 177,
        (byte) 66,
        (byte) 18,
        (byte) 32 /*0x20*/,
        (byte) 138,
        (byte) 173,
        (byte) 8,
        (byte) 89,
        (byte) 168,
        (byte) 234,
        (byte) 75,
        (byte) 137,
        (byte) 85,
        (byte) 251,
        (byte) 176 /*0xB0*/,
        (byte) 58,
        (byte) 18,
        (byte) 72,
        (byte) 252,
        (byte) 218,
        (byte) 129,
        (byte) 188,
        (byte) 19,
        (byte) 58,
        (byte) 170,
        (byte) 240 /*0xF0*/,
        (byte) 220,
        (byte) 27,
        (byte) 236,
        (byte) 175
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 3,
        (byte) 106,
        (byte) 244,
        (byte) 134,
        (byte) 49,
        (byte) 43,
        (byte) 219,
        (byte) 44,
        (byte) 230,
        (byte) 166,
        (byte) 194,
        (byte) 200,
        (byte) 150,
        (byte) 64 /*0x40*/,
        (byte) 184,
        (byte) 55,
        (byte) 206,
        (byte) 68,
        (byte) 110,
        (byte) 82,
        (byte) 88,
        (byte) 99,
        (byte) 93,
        (byte) 92,
        (byte) 9,
        (byte) 67,
        (byte) 202,
        (byte) 44,
        (byte) 253,
        (byte) 165,
        (byte) 253,
        (byte) 155,
        (byte) 28,
        (byte) 168,
        (byte) 1,
        (byte) 88,
        (byte) 16 /*0x10*/,
        (byte) 22,
        (byte) 147,
        (byte) 155,
        (byte) 51,
        (byte) 71,
        (byte) 143,
        (byte) 231,
        (byte) 48 /*0x30*/,
        (byte) 132,
        (byte) 157,
        (byte) 105,
        (byte) 112 /*0x70*/,
        (byte) 44,
        (byte) 72,
        (byte) 150,
        (byte) 2,
        (byte) 214,
        (byte) 10
      };
      byte[] numArray5 = new byte[55];
      numArray5[34] = (byte) 98;
      numArray5[31 /*0x1F*/] = (byte) 137;
      numArray5[7] = (byte) 0;
      numArray5[32 /*0x20*/] = (byte) 176 /*0xB0*/;
      numArray5[10] = (byte) 58;
      numArray5[40] = (byte) 195;
      numArray5[39] = (byte) 157;
      numArray5[46] = (byte) 253;
      numArray5[8] = (byte) 145;
      numArray5[33] = (byte) 235;
      numArray5[42] = (byte) 176 /*0xB0*/;
      numArray5[11] = (byte) 58;
      numArray5[12] = (byte) 167;
      numArray5[30] = (byte) 247;
      numArray5[14] = (byte) 244;
      numArray5[15] = (byte) 23;
      numArray5[16 /*0x10*/] = (byte) 68;
      numArray5[41] = (byte) 160 /*0xA0*/;
      numArray5[18] = (byte) 89;
      numArray5[3] = (byte) 1;
      numArray5[20] = (byte) 205;
      numArray5[9] = (byte) 214;
      numArray5[5] = (byte) 222;
      numArray5[51] = (byte) 55;
      numArray5[13] = (byte) 221;
      numArray5[25] = (byte) 243;
      numArray5[2] = (byte) 88;
      numArray5[27] = (byte) 136;
      numArray5[28] = (byte) 191;
      numArray5[21] = (byte) 178;
      numArray5[26] = (byte) 152;
      numArray5[19] = (byte) 233;
      numArray5[23] = (byte) 104;
      numArray5[22] = (byte) 36;
      numArray5[4] = (byte) 198;
      numArray5[35] = (byte) 62;
      numArray5[36] = (byte) 52;
      numArray5[52] = (byte) 114;
      numArray5[38] = (byte) 104;
      numArray5[6] = (byte) 121;
      numArray5[43] = (byte) 147;
      numArray5[50] = (byte) 245;
      numArray5[0] = (byte) 108;
      numArray5[37] = (byte) 247;
      numArray5[44] = (byte) 3;
      numArray5[45] = (byte) 164;
      numArray5[29] = (byte) 139;
      numArray5[47] = (byte) 93;
      numArray5[24] = (byte) 154;
      numArray5[49] = (byte) 34;
      numArray5[53] = (byte) 156;
      numArray5[17] = (byte) 151;
      numArray5[1] = (byte) 64 /*0x40*/;
      numArray5[48 /*0x30*/] = (byte) 195;
      numArray5[54] = (byte) 38;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[28]
      {
        (byte) 108,
        (byte) 247,
        (byte) 73,
        (byte) 254,
        (byte) 243,
        (byte) 95,
        (byte) 228,
        (byte) 67,
        (byte) 141,
        (byte) 101,
        (byte) 80 /*0x50*/,
        (byte) 221,
        (byte) 19,
        (byte) 173,
        (byte) 30,
        (byte) 12,
        (byte) 150,
        (byte) 154,
        (byte) 249,
        (byte) 133,
        (byte) 55,
        (byte) 33,
        (byte) 171,
        (byte) 245,
        (byte) 54,
        (byte) 171,
        (byte) 202,
        (byte) 203
      };
      byte[] numArray7 = new byte[28]
      {
        (byte) 59,
        (byte) 212,
        (byte) 146,
        (byte) 21,
        (byte) 110,
        (byte) 154,
        (byte) 153,
        (byte) 165,
        (byte) 231,
        (byte) 128 /*0x80*/,
        (byte) 19,
        (byte) 65,
        (byte) 72,
        (byte) 241,
        (byte) 105,
        (byte) 100,
        (byte) 91,
        (byte) 143,
        (byte) 99,
        (byte) 218,
        (byte) 181,
        (byte) 13,
        (byte) 109,
        (byte) 216,
        (byte) 156,
        (byte) 56,
        (byte) 94,
        (byte) 36
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[138];
    byte[] numArray9 = new byte[55]
    {
      (byte) 164,
      (byte) 148,
      (byte) 181,
      (byte) 12,
      (byte) 161,
      (byte) 90,
      (byte) 61,
      (byte) 144 /*0x90*/,
      (byte) 184,
      (byte) 105,
      (byte) 250,
      (byte) 124,
      (byte) 115,
      (byte) 94,
      (byte) 76,
      (byte) 253,
      (byte) 87,
      (byte) 114,
      (byte) 216,
      (byte) 153,
      (byte) 28,
      (byte) 0,
      (byte) 70,
      (byte) 94,
      (byte) 74,
      (byte) 76,
      (byte) 84,
      (byte) 199,
      (byte) 72,
      (byte) 148,
      (byte) 25,
      (byte) 193,
      (byte) 36,
      (byte) 215,
      (byte) 172,
      (byte) 72,
      (byte) 94,
      (byte) 161,
      (byte) 124,
      (byte) 178,
      (byte) 89,
      (byte) 10,
      (byte) 64 /*0x40*/,
      (byte) 28,
      (byte) 61,
      (byte) 165,
      (byte) 56,
      (byte) 151,
      (byte) 233,
      (byte) 173,
      (byte) 115,
      (byte) 88,
      (byte) 169,
      (byte) 207,
      (byte) 113
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 74,
      (byte) 217,
      (byte) 10,
      (byte) 142,
      (byte) 248,
      (byte) 117,
      (byte) 181,
      (byte) 237,
      (byte) 247,
      (byte) 105,
      (byte) 213,
      (byte) 200,
      (byte) 179,
      (byte) 191,
      (byte) 114,
      (byte) 132,
      (byte) 169,
      (byte) 77,
      (byte) 120,
      (byte) 128 /*0x80*/,
      (byte) 230,
      (byte) 74,
      (byte) 110,
      (byte) 99,
      (byte) 163,
      (byte) 174,
      (byte) 204,
      (byte) 236,
      (byte) 31 /*0x1F*/,
      (byte) 89,
      (byte) 164,
      (byte) 248,
      (byte) 164,
      (byte) 38,
      (byte) 134,
      (byte) 120,
      (byte) 251,
      (byte) 198,
      (byte) 27,
      (byte) 249,
      (byte) 230,
      (byte) 209,
      (byte) 81,
      (byte) 52,
      (byte) 37,
      (byte) 157,
      (byte) 15,
      (byte) 179,
      (byte) 217,
      (byte) 191,
      (byte) 244,
      (byte) 9,
      (byte) 11,
      (byte) 245,
      (byte) 208 /*0xD0*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[24] = (byte) 188;
    numArray11[1] = (byte) 194;
    numArray11[43] = (byte) 76;
    numArray11[3] = (byte) 6;
    numArray11[30] = (byte) 84;
    numArray11[29] = (byte) 24;
    numArray11[6] = (byte) 36;
    numArray11[7] = (byte) 79;
    numArray11[8] = (byte) 57;
    numArray11[48 /*0x30*/] = (byte) 188;
    numArray11[10] = (byte) 155;
    numArray11[49] = (byte) 98;
    numArray11[12] = (byte) 4;
    numArray11[17] = (byte) 63 /*0x3F*/;
    numArray11[35] = (byte) 222;
    numArray11[15] = (byte) 246;
    numArray11[45] = (byte) 229;
    numArray11[21] = (byte) 33;
    numArray11[18] = (byte) 18;
    numArray11[28] = (byte) 192 /*0xC0*/;
    numArray11[46] = (byte) 229;
    numArray11[22] = (byte) 165;
    numArray11[2] = (byte) 85;
    numArray11[34] = (byte) 7;
    numArray11[39] = (byte) 194;
    numArray11[40] = (byte) 27;
    numArray11[26] = (byte) 154;
    numArray11[31 /*0x1F*/] = (byte) 102;
    numArray11[25] = (byte) 222;
    numArray11[4] = (byte) 117;
    numArray11[51] = (byte) 144 /*0x90*/;
    numArray11[38] = (byte) 228;
    numArray11[32 /*0x20*/] = (byte) 8;
    numArray11[33] = (byte) 250;
    numArray11[9] = (byte) 88;
    numArray11[27] = (byte) 33;
    numArray11[36] = (byte) 38;
    numArray11[37] = (byte) 171;
    numArray11[23] = (byte) 19;
    numArray11[19] = (byte) 191;
    numArray11[5] = (byte) 72;
    numArray11[41] = (byte) 204;
    numArray11[42] = (byte) 4;
    numArray11[14] = (byte) 182;
    numArray11[20] = (byte) 42;
    numArray11[47] = (byte) 14;
    numArray11[13] = (byte) 158;
    numArray11[16 /*0x10*/] = (byte) 38;
    numArray11[11] = (byte) 177;
    numArray11[0] = (byte) 86;
    numArray11[50] = (byte) 131;
    numArray11[44] = (byte) 157;
    numArray11[52] = (byte) 94;
    numArray11[53] = (byte) 228;
    numArray11[54] = (byte) 35;
    byte[] numArray12 = new byte[55]
    {
      (byte) 157,
      (byte) 8,
      (byte) 58,
      (byte) 168,
      (byte) 147,
      (byte) 120,
      (byte) 242,
      (byte) 232,
      (byte) 58,
      (byte) 89,
      (byte) 161,
      (byte) 151,
      (byte) 148,
      (byte) 38,
      (byte) 127 /*0x7F*/,
      (byte) 36,
      (byte) 133,
      (byte) 78,
      (byte) 85,
      (byte) 173,
      (byte) 19,
      (byte) 138,
      (byte) 19,
      (byte) 171,
      (byte) 205,
      (byte) 160 /*0xA0*/,
      (byte) 139,
      (byte) 30,
      (byte) 165,
      (byte) 55,
      (byte) 66,
      (byte) 82,
      (byte) 63 /*0x3F*/,
      (byte) 97,
      (byte) 91,
      (byte) 193,
      (byte) 219,
      (byte) 37,
      (byte) 225,
      (byte) 227,
      byte.MaxValue,
      (byte) 2,
      (byte) 135,
      (byte) 63 /*0x3F*/,
      (byte) 140,
      (byte) 204,
      (byte) 196,
      (byte) 165,
      (byte) 53,
      (byte) 200,
      (byte) 247,
      (byte) 58,
      (byte) 23,
      (byte) 96 /*0x60*/,
      (byte) 203
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[28];
    numArray13[21] = (byte) 13;
    numArray13[9] = (byte) 133;
    numArray13[2] = (byte) 158;
    numArray13[3] = (byte) 33;
    numArray13[4] = (byte) 159;
    numArray13[22] = (byte) 154;
    numArray13[6] = (byte) 139;
    numArray13[7] = (byte) 63 /*0x3F*/;
    numArray13[5] = (byte) 49;
    numArray13[27] = (byte) 140;
    numArray13[15] = (byte) 12;
    numArray13[24] = (byte) 19;
    numArray13[23] = (byte) 28;
    numArray13[13] = (byte) 235;
    numArray13[14] = (byte) 24;
    numArray13[1] = (byte) 114;
    numArray13[16 /*0x10*/] = (byte) 59;
    numArray13[17] = (byte) 139;
    numArray13[8] = (byte) 43;
    numArray13[20] = (byte) 16 /*0x10*/;
    numArray13[0] = (byte) 203;
    numArray13[11] = (byte) 196;
    numArray13[12] = (byte) 21;
    numArray13[18] = (byte) 33;
    numArray13[10] = (byte) 50;
    numArray13[25] = (byte) 127 /*0x7F*/;
    numArray13[26] = (byte) 202;
    numArray13[19] = (byte) 58;
    byte[] numArray14 = new byte[28]
    {
      (byte) 142,
      (byte) 155,
      (byte) 38,
      (byte) 53,
      (byte) 147,
      (byte) 118,
      (byte) 201,
      (byte) 109,
      (byte) 94,
      (byte) 145,
      (byte) 13,
      (byte) 33,
      (byte) 159,
      (byte) 86,
      (byte) 196,
      (byte) 12,
      (byte) 18,
      (byte) 0,
      (byte) 70,
      (byte) 239,
      (byte) 39,
      (byte) 131,
      (byte) 171,
      (byte) 231,
      (byte) 67,
      (byte) 24,
      (byte) 103,
      (byte) 37
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 28);
    for (int index = 0; index < 28; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12723()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[137];
      byte[] numArray2 = new byte[55];
      numArray2[44] = (byte) 203;
      numArray2[6] = (byte) 8;
      numArray2[2] = (byte) 176 /*0xB0*/;
      numArray2[18] = (byte) 114;
      numArray2[46] = (byte) 163;
      numArray2[34] = (byte) 163;
      numArray2[3] = (byte) 49;
      numArray2[7] = (byte) 23;
      numArray2[8] = (byte) 210;
      numArray2[28] = (byte) 73;
      numArray2[38] = (byte) 20;
      numArray2[50] = (byte) 238;
      numArray2[12] = (byte) 17;
      numArray2[11] = (byte) 73;
      numArray2[14] = (byte) 225;
      numArray2[15] = (byte) 6;
      numArray2[51] = (byte) 244;
      numArray2[17] = (byte) 227;
      numArray2[13] = (byte) 21;
      numArray2[19] = (byte) 96 /*0x60*/;
      numArray2[20] = (byte) 176 /*0xB0*/;
      numArray2[21] = (byte) 33;
      numArray2[22] = (byte) 156;
      numArray2[23] = (byte) 101;
      numArray2[24] = (byte) 40;
      numArray2[25] = (byte) 45;
      numArray2[26] = (byte) 110;
      numArray2[10] = (byte) 93;
      numArray2[53] = (byte) 174;
      numArray2[29] = (byte) 89;
      numArray2[30] = (byte) 23;
      numArray2[31 /*0x1F*/] = (byte) 44;
      numArray2[40] = (byte) 170;
      numArray2[32 /*0x20*/] = (byte) 228;
      numArray2[39] = (byte) 138;
      numArray2[35] = (byte) 89;
      numArray2[49] = (byte) 173;
      numArray2[37] = (byte) 12;
      numArray2[42] = (byte) 213;
      numArray2[36] = (byte) 88;
      numArray2[1] = (byte) 254;
      numArray2[41] = (byte) 183;
      numArray2[33] = (byte) 90;
      numArray2[27] = (byte) 221;
      numArray2[5] = (byte) 31 /*0x1F*/;
      numArray2[45] = (byte) 136;
      numArray2[0] = (byte) 103;
      numArray2[16 /*0x10*/] = (byte) 207;
      numArray2[48 /*0x30*/] = (byte) 251;
      numArray2[47] = (byte) 141;
      numArray2[4] = (byte) 162;
      numArray2[9] = (byte) 41;
      numArray2[52] = (byte) 16 /*0x10*/;
      numArray2[43] = (byte) 197;
      numArray2[54] = (byte) 110;
      byte[] numArray3 = new byte[55];
      numArray3[10] = (byte) 82;
      numArray3[1] = (byte) 149;
      numArray3[18] = (byte) 179;
      numArray3[0] = (byte) 245;
      numArray3[53] = (byte) 57;
      numArray3[9] = (byte) 140;
      numArray3[45] = (byte) 96 /*0x60*/;
      numArray3[7] = (byte) 198;
      numArray3[8] = (byte) 62;
      numArray3[27] = (byte) 79;
      numArray3[44] = (byte) 110;
      numArray3[34] = (byte) 131;
      numArray3[52] = (byte) 77;
      numArray3[13] = (byte) 237;
      numArray3[6] = (byte) 105;
      numArray3[15] = (byte) 40;
      numArray3[43] = (byte) 83;
      numArray3[17] = (byte) 75;
      numArray3[40] = (byte) 156;
      numArray3[26] = (byte) 88;
      numArray3[48 /*0x30*/] = (byte) 181;
      numArray3[3] = (byte) 136;
      numArray3[30] = (byte) 172;
      numArray3[29] = (byte) 81;
      numArray3[23] = (byte) 220;
      numArray3[25] = (byte) 133;
      numArray3[24] = (byte) 108;
      numArray3[47] = (byte) 238;
      numArray3[28] = (byte) 191;
      numArray3[38] = (byte) 173;
      numArray3[32 /*0x20*/] = (byte) 83;
      numArray3[31 /*0x1F*/] = (byte) 38;
      numArray3[37] = (byte) 246;
      numArray3[33] = (byte) 70;
      numArray3[5] = (byte) 96 /*0x60*/;
      numArray3[35] = (byte) 129;
      numArray3[46] = (byte) 72;
      numArray3[36] = (byte) 194;
      numArray3[11] = (byte) 204;
      numArray3[39] = (byte) 90;
      numArray3[4] = (byte) 94;
      numArray3[41] = (byte) 110;
      numArray3[42] = (byte) 139;
      numArray3[21] = (byte) 247;
      numArray3[16 /*0x10*/] = (byte) 22;
      numArray3[2] = (byte) 202;
      numArray3[14] = (byte) 206;
      numArray3[12] = (byte) 60;
      numArray3[19] = (byte) 178;
      numArray3[49] = (byte) 91;
      numArray3[50] = (byte) 117;
      numArray3[51] = (byte) 72;
      numArray3[22] = (byte) 204;
      numArray3[20] = (byte) 97;
      numArray3[54] = (byte) 50;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 175,
        (byte) 236,
        (byte) 79,
        (byte) 12,
        (byte) 218,
        (byte) 147,
        (byte) 206,
        (byte) 241,
        (byte) 30,
        (byte) 240 /*0xF0*/,
        (byte) 86,
        (byte) 176 /*0xB0*/,
        (byte) 34,
        (byte) 67,
        (byte) 35,
        (byte) 125,
        (byte) 6,
        (byte) 254,
        (byte) 89,
        (byte) 160 /*0xA0*/,
        (byte) 96 /*0x60*/,
        (byte) 121,
        (byte) 134,
        (byte) 125,
        (byte) 189,
        (byte) 138,
        (byte) 64 /*0x40*/,
        (byte) 211,
        (byte) 9,
        (byte) 219,
        (byte) 122,
        (byte) 17,
        (byte) 89,
        (byte) 5,
        (byte) 173,
        (byte) 175,
        (byte) 99,
        (byte) 252,
        (byte) 92,
        (byte) 36,
        (byte) 127 /*0x7F*/,
        (byte) 210,
        (byte) 103,
        (byte) 4,
        (byte) 201,
        (byte) 86,
        (byte) 193,
        (byte) 25,
        (byte) 72,
        (byte) 119,
        (byte) 17,
        (byte) 195,
        (byte) 11,
        (byte) 105,
        (byte) 7
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 31 /*0x1F*/,
        (byte) 110,
        (byte) 195,
        (byte) 83,
        (byte) 29,
        (byte) 142,
        (byte) 151,
        (byte) 86,
        (byte) 145,
        (byte) 83,
        (byte) 159,
        (byte) 185,
        (byte) 12,
        (byte) 76,
        (byte) 56,
        (byte) 75,
        (byte) 146,
        (byte) 155,
        (byte) 209,
        (byte) 120,
        (byte) 22,
        (byte) 148,
        (byte) 180,
        (byte) 82,
        (byte) 16 /*0x10*/,
        (byte) 73,
        (byte) 162,
        (byte) 254,
        (byte) 203,
        (byte) 142,
        (byte) 110,
        (byte) 37,
        (byte) 226,
        (byte) 11,
        (byte) 141,
        (byte) 251,
        (byte) 136,
        (byte) 106,
        (byte) 244,
        (byte) 243,
        (byte) 123,
        (byte) 138,
        (byte) 164,
        (byte) 162,
        (byte) 42,
        (byte) 84,
        (byte) 70,
        (byte) 139,
        (byte) 92,
        (byte) 196,
        (byte) 231,
        (byte) 216,
        (byte) 100,
        (byte) 9,
        (byte) 101
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[27];
      numArray6[22] = (byte) 102;
      numArray6[5] = (byte) 231;
      numArray6[2] = (byte) 80 /*0x50*/;
      numArray6[3] = (byte) 82;
      numArray6[8] = (byte) 68;
      numArray6[9] = (byte) 86;
      numArray6[6] = (byte) 66;
      numArray6[7] = (byte) 118;
      numArray6[14] = (byte) 10;
      numArray6[17] = (byte) 33;
      numArray6[10] = (byte) 28;
      numArray6[25] = (byte) 139;
      numArray6[0] = (byte) 209;
      numArray6[23] = (byte) 201;
      numArray6[13] = (byte) 26;
      numArray6[15] = (byte) 162;
      numArray6[16 /*0x10*/] = (byte) 53;
      numArray6[11] = (byte) 150;
      numArray6[18] = (byte) 142;
      numArray6[19] = (byte) 17;
      numArray6[20] = (byte) 130;
      numArray6[21] = (byte) 180;
      numArray6[12] = (byte) 115;
      numArray6[1] = (byte) 78;
      numArray6[24] = (byte) 39;
      numArray6[4] = (byte) 251;
      numArray6[26] = (byte) 146;
      byte[] numArray7 = new byte[27]
      {
        (byte) 43,
        (byte) 204,
        (byte) 190,
        (byte) 183,
        (byte) 24,
        (byte) 50,
        (byte) 64 /*0x40*/,
        (byte) 23,
        (byte) 195,
        (byte) 219,
        (byte) 117,
        (byte) 184,
        (byte) 117,
        (byte) 213,
        (byte) 181,
        (byte) 64 /*0x40*/,
        (byte) 38,
        (byte) 239,
        (byte) 13,
        (byte) 75,
        (byte) 85,
        (byte) 192 /*0xC0*/,
        (byte) 34,
        (byte) 234,
        (byte) 209,
        (byte) 182,
        (byte) 64 /*0x40*/
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
      (byte) 134,
      (byte) 43,
      (byte) 112 /*0x70*/,
      (byte) 85,
      (byte) 121,
      (byte) 191,
      (byte) 62,
      (byte) 102,
      (byte) 30,
      (byte) 30,
      (byte) 41,
      (byte) 136,
      (byte) 93,
      (byte) 150,
      (byte) 99,
      (byte) 157,
      (byte) 163,
      (byte) 236,
      (byte) 244,
      (byte) 47,
      (byte) 102,
      (byte) 37,
      (byte) 164,
      (byte) 183,
      (byte) 53,
      (byte) 194,
      (byte) 149,
      (byte) 48 /*0x30*/,
      (byte) 168,
      (byte) 133,
      (byte) 83,
      (byte) 103,
      (byte) 116,
      (byte) 197,
      (byte) 133,
      (byte) 165,
      (byte) 20,
      (byte) 148,
      (byte) 171,
      (byte) 206,
      (byte) 70,
      (byte) 153,
      (byte) 133,
      (byte) 100,
      (byte) 170,
      (byte) 212,
      (byte) 169,
      (byte) 139,
      (byte) 114,
      (byte) 170,
      (byte) 212,
      (byte) 55,
      (byte) 238,
      (byte) 237,
      (byte) 129
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 5,
      (byte) 93,
      (byte) 241,
      (byte) 215,
      (byte) 6,
      (byte) 34,
      (byte) 178,
      (byte) 246,
      (byte) 202,
      (byte) 38,
      (byte) 189,
      (byte) 130,
      (byte) 140,
      (byte) 98,
      (byte) 210,
      (byte) 204,
      (byte) 134,
      (byte) 149,
      (byte) 6,
      (byte) 35,
      (byte) 0,
      (byte) 202,
      (byte) 64 /*0x40*/,
      (byte) 120,
      (byte) 62,
      (byte) 29,
      (byte) 136,
      (byte) 69,
      (byte) 238,
      (byte) 206,
      (byte) 254,
      (byte) 131,
      (byte) 17,
      (byte) 183,
      (byte) 5,
      (byte) 118,
      (byte) 1,
      (byte) 108,
      (byte) 116,
      (byte) 27,
      (byte) 162,
      (byte) 244,
      (byte) 132,
      (byte) 134,
      (byte) 96 /*0x60*/,
      byte.MaxValue,
      (byte) 74,
      (byte) 97,
      (byte) 197,
      (byte) 13,
      (byte) 134,
      (byte) 143,
      (byte) 100,
      (byte) 74,
      (byte) 74
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 184,
      (byte) 32 /*0x20*/,
      (byte) 182,
      (byte) 41,
      (byte) 13,
      (byte) 190,
      (byte) 152,
      (byte) 203,
      (byte) 193,
      (byte) 20,
      (byte) 152,
      (byte) 16 /*0x10*/,
      (byte) 189,
      (byte) 137,
      (byte) 15,
      (byte) 64 /*0x40*/,
      (byte) 37,
      (byte) 233,
      (byte) 205,
      (byte) 159,
      (byte) 64 /*0x40*/,
      (byte) 12,
      (byte) 96 /*0x60*/,
      (byte) 25,
      (byte) 10,
      (byte) 189,
      (byte) 253,
      (byte) 176 /*0xB0*/,
      (byte) 27,
      (byte) 121,
      (byte) 135,
      (byte) 67,
      (byte) 36,
      (byte) 30,
      (byte) 91,
      (byte) 116,
      (byte) 245,
      (byte) 165,
      (byte) 56,
      (byte) 218,
      (byte) 237,
      (byte) 152,
      (byte) 232,
      (byte) 192 /*0xC0*/,
      (byte) 17,
      (byte) 122,
      (byte) 180,
      (byte) 33,
      (byte) 197,
      (byte) 246,
      (byte) 201,
      (byte) 3,
      (byte) 180,
      (byte) 193,
      (byte) 71
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 58,
      (byte) 149,
      (byte) 226,
      (byte) 224 /*0xE0*/,
      (byte) 205,
      byte.MaxValue,
      (byte) 91,
      (byte) 229,
      (byte) 156,
      (byte) 233,
      (byte) 25,
      (byte) 147,
      (byte) 185,
      (byte) 138,
      (byte) 210,
      (byte) 126,
      (byte) 133,
      (byte) 39,
      (byte) 43,
      (byte) 172,
      (byte) 159,
      (byte) 15,
      (byte) 240 /*0xF0*/,
      (byte) 200,
      (byte) 166,
      (byte) 151,
      (byte) 133,
      (byte) 169,
      (byte) 131,
      (byte) 229,
      (byte) 230,
      (byte) 189,
      (byte) 133,
      (byte) 218,
      (byte) 186,
      (byte) 43,
      (byte) 128 /*0x80*/,
      (byte) 140,
      (byte) 97,
      (byte) 96 /*0x60*/,
      (byte) 152,
      (byte) 6,
      (byte) 158,
      (byte) 30,
      (byte) 184,
      (byte) 101,
      (byte) 216,
      (byte) 230,
      (byte) 101,
      (byte) 205,
      (byte) 194,
      (byte) 12,
      (byte) 102,
      (byte) 228,
      (byte) 53
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[27];
    numArray13[4] = (byte) 231;
    numArray13[1] = (byte) 31 /*0x1F*/;
    numArray13[2] = (byte) 79;
    numArray13[9] = (byte) 235;
    numArray13[16 /*0x10*/] = (byte) 242;
    numArray13[14] = (byte) 127 /*0x7F*/;
    numArray13[6] = (byte) 72;
    numArray13[0] = (byte) 156;
    numArray13[8] = (byte) 85;
    numArray13[24] = (byte) 220;
    numArray13[21] = (byte) 8;
    numArray13[11] = (byte) 46;
    numArray13[23] = (byte) 9;
    numArray13[13] = (byte) 75;
    numArray13[5] = (byte) 176 /*0xB0*/;
    numArray13[15] = (byte) 176 /*0xB0*/;
    numArray13[18] = (byte) 167;
    numArray13[17] = (byte) 177;
    numArray13[3] = (byte) 193;
    numArray13[19] = (byte) 35;
    numArray13[22] = (byte) 143;
    numArray13[20] = (byte) 160 /*0xA0*/;
    numArray13[7] = (byte) 140;
    numArray13[10] = (byte) 159;
    numArray13[12] = (byte) 155;
    numArray13[25] = (byte) 235;
    numArray13[26] = (byte) 36;
    byte[] numArray14 = new byte[27];
    numArray14[6] = (byte) 71;
    numArray14[1] = (byte) 6;
    numArray14[8] = (byte) 178;
    numArray14[3] = (byte) 179;
    numArray14[4] = (byte) 240 /*0xF0*/;
    numArray14[5] = (byte) 92;
    numArray14[19] = (byte) 246;
    numArray14[13] = (byte) 176 /*0xB0*/;
    numArray14[23] = (byte) 31 /*0x1F*/;
    numArray14[9] = (byte) 40;
    numArray14[0] = (byte) 132;
    numArray14[16 /*0x10*/] = (byte) 86;
    numArray14[24] = (byte) 126;
    numArray14[17] = (byte) 247;
    numArray14[14] = (byte) 0;
    numArray14[26] = (byte) 143;
    numArray14[7] = (byte) 187;
    numArray14[11] = (byte) 134;
    numArray14[18] = (byte) 78;
    numArray14[15] = (byte) 181;
    numArray14[20] = (byte) 183;
    numArray14[10] = (byte) 179;
    numArray14[22] = (byte) 177;
    numArray14[12] = (byte) 92;
    numArray14[21] = (byte) 106;
    numArray14[25] = (byte) 251;
    numArray14[2] = (byte) 134;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 27);
    for (int index = 0; index < 27; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12724()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[135];
      byte[] numArray2 = new byte[55]
      {
        (byte) 83,
        (byte) 55,
        (byte) 38,
        (byte) 149,
        (byte) 195,
        (byte) 247,
        (byte) 162,
        (byte) 53,
        (byte) 10,
        (byte) 91,
        (byte) 6,
        (byte) 41,
        (byte) 245,
        (byte) 6,
        (byte) 77,
        (byte) 115,
        (byte) 67,
        (byte) 184,
        (byte) 79,
        (byte) 139,
        (byte) 232,
        (byte) 100,
        (byte) 223,
        (byte) 4,
        (byte) 178,
        (byte) 72,
        (byte) 142,
        (byte) 82,
        (byte) 250,
        (byte) 58,
        (byte) 19,
        (byte) 50,
        (byte) 217,
        (byte) 249,
        (byte) 53,
        (byte) 144 /*0x90*/,
        (byte) 88,
        (byte) 81,
        (byte) 46,
        (byte) 2,
        (byte) 37,
        (byte) 164,
        (byte) 161,
        (byte) 100,
        (byte) 125,
        (byte) 67,
        (byte) 86,
        (byte) 249,
        (byte) 166,
        (byte) 118,
        (byte) 125,
        (byte) 191,
        (byte) 22,
        (byte) 164,
        (byte) 228
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 207,
        (byte) 195,
        (byte) 145,
        (byte) 172,
        (byte) 175,
        (byte) 97,
        (byte) 250,
        (byte) 179,
        (byte) 146,
        (byte) 129,
        (byte) 50,
        (byte) 235,
        (byte) 177,
        (byte) 79,
        (byte) 208 /*0xD0*/,
        (byte) 217,
        (byte) 197,
        (byte) 207,
        (byte) 163,
        (byte) 211,
        (byte) 162,
        (byte) 234,
        (byte) 171,
        (byte) 47,
        (byte) 9,
        (byte) 242,
        (byte) 157,
        (byte) 71,
        (byte) 110,
        (byte) 222,
        (byte) 210,
        (byte) 47,
        (byte) 236,
        (byte) 72,
        (byte) 42,
        (byte) 174,
        (byte) 15,
        (byte) 18,
        (byte) 220,
        (byte) 43,
        (byte) 111,
        (byte) 70,
        (byte) 171,
        (byte) 84,
        (byte) 140,
        (byte) 254,
        (byte) 4,
        (byte) 93,
        (byte) 96 /*0x60*/,
        (byte) 74,
        (byte) 152,
        (byte) 210,
        (byte) 154,
        (byte) 2,
        (byte) 22
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 33,
        (byte) 227,
        (byte) 123,
        (byte) 104,
        (byte) 79,
        (byte) 58,
        (byte) 207,
        (byte) 71,
        (byte) 212,
        (byte) 80 /*0x50*/,
        (byte) 252,
        (byte) 209,
        (byte) 189,
        (byte) 26,
        (byte) 242,
        (byte) 137,
        (byte) 77,
        (byte) 101,
        (byte) 68,
        (byte) 194,
        (byte) 123,
        (byte) 135,
        (byte) 110,
        (byte) 241,
        (byte) 149,
        (byte) 172,
        (byte) 132,
        (byte) 189,
        (byte) 83,
        (byte) 27,
        (byte) 52,
        (byte) 20,
        (byte) 69,
        (byte) 56,
        (byte) 55,
        (byte) 109,
        (byte) 179,
        (byte) 154,
        (byte) 214,
        (byte) 105,
        (byte) 209,
        (byte) 28,
        (byte) 169,
        (byte) 243,
        (byte) 89,
        (byte) 40,
        (byte) 233,
        (byte) 91,
        (byte) 138,
        (byte) 53,
        (byte) 201,
        (byte) 63 /*0x3F*/,
        (byte) 160 /*0xA0*/,
        (byte) 44,
        (byte) 220
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 120,
        (byte) 130,
        (byte) 235,
        (byte) 103,
        (byte) 65,
        (byte) 34,
        (byte) 199,
        (byte) 16 /*0x10*/,
        (byte) 160 /*0xA0*/,
        (byte) 79,
        (byte) 158,
        (byte) 200,
        (byte) 240 /*0xF0*/,
        (byte) 150,
        (byte) 143,
        (byte) 163,
        (byte) 224 /*0xE0*/,
        (byte) 219,
        (byte) 148,
        (byte) 89,
        (byte) 240 /*0xF0*/,
        (byte) 27,
        (byte) 99,
        (byte) 96 /*0x60*/,
        (byte) 239,
        (byte) 208 /*0xD0*/,
        (byte) 111,
        (byte) 111,
        (byte) 107,
        (byte) 69,
        (byte) 210,
        (byte) 194,
        (byte) 185,
        (byte) 229,
        (byte) 67,
        (byte) 72,
        (byte) 189,
        (byte) 96 /*0x60*/,
        (byte) 21,
        (byte) 203,
        (byte) 172,
        (byte) 140,
        (byte) 227,
        (byte) 253,
        (byte) 220,
        (byte) 188,
        (byte) 135,
        (byte) 164,
        (byte) 47,
        (byte) 95,
        (byte) 53,
        (byte) 246,
        (byte) 151,
        (byte) 115,
        (byte) 42
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[25]
      {
        (byte) 137,
        (byte) 210,
        (byte) 163,
        (byte) 181,
        (byte) 17,
        (byte) 243,
        (byte) 184,
        (byte) 27,
        (byte) 232,
        (byte) 139,
        (byte) 133,
        (byte) 227,
        (byte) 132,
        (byte) 214,
        (byte) 179,
        (byte) 143,
        (byte) 218,
        (byte) 241,
        (byte) 94,
        (byte) 222,
        (byte) 123,
        (byte) 152,
        (byte) 109,
        (byte) 195,
        (byte) 41
      };
      byte[] numArray7 = new byte[25];
      numArray7[3] = (byte) 120;
      numArray7[19] = (byte) 46;
      numArray7[2] = (byte) 159;
      numArray7[15] = (byte) 239;
      numArray7[5] = (byte) 205;
      numArray7[23] = (byte) 178;
      numArray7[6] = (byte) 11;
      numArray7[10] = (byte) 248;
      numArray7[8] = (byte) 26;
      numArray7[9] = (byte) 75;
      numArray7[7] = (byte) 40;
      numArray7[11] = (byte) 207;
      numArray7[18] = (byte) 170;
      numArray7[13] = (byte) 18;
      numArray7[14] = (byte) 177;
      numArray7[20] = (byte) 102;
      numArray7[16 /*0x10*/] = (byte) 171;
      numArray7[0] = (byte) 222;
      numArray7[17] = (byte) 84;
      numArray7[22] = (byte) 119;
      numArray7[4] = (byte) 249;
      numArray7[1] = (byte) 100;
      numArray7[21] = (byte) 77;
      numArray7[12] = (byte) 230;
      numArray7[24] = (byte) 164;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[135];
    byte[] numArray9 = new byte[55];
    numArray9[36] = (byte) 56;
    numArray9[13] = (byte) 70;
    numArray9[53] = (byte) 10;
    numArray9[50] = (byte) 42;
    numArray9[7] = (byte) 213;
    numArray9[5] = (byte) 140;
    numArray9[45] = (byte) 41;
    numArray9[1] = (byte) 248;
    numArray9[4] = (byte) 224 /*0xE0*/;
    numArray9[9] = (byte) 37;
    numArray9[54] = (byte) 72;
    numArray9[19] = (byte) 48 /*0x30*/;
    numArray9[51] = (byte) 82;
    numArray9[47] = (byte) 49;
    numArray9[12] = (byte) 41;
    numArray9[15] = (byte) 172;
    numArray9[42] = (byte) 139;
    numArray9[17] = (byte) 92;
    numArray9[18] = (byte) 38;
    numArray9[3] = (byte) 101;
    numArray9[20] = (byte) 53;
    numArray9[6] = (byte) 222;
    numArray9[39] = (byte) 39;
    numArray9[23] = (byte) 38;
    numArray9[24] = (byte) 208 /*0xD0*/;
    numArray9[30] = (byte) 69;
    numArray9[32 /*0x20*/] = (byte) 222;
    numArray9[27] = (byte) 45;
    numArray9[28] = (byte) 190;
    numArray9[29] = (byte) 60;
    numArray9[26] = (byte) 177;
    numArray9[22] = (byte) 163;
    numArray9[21] = (byte) 237;
    numArray9[16 /*0x10*/] = (byte) 235;
    numArray9[34] = (byte) 224 /*0xE0*/;
    numArray9[35] = (byte) 0;
    numArray9[0] = (byte) 188;
    numArray9[25] = (byte) 95;
    numArray9[38] = (byte) 82;
    numArray9[8] = (byte) 101;
    numArray9[40] = (byte) 225;
    numArray9[41] = (byte) 82;
    numArray9[46] = (byte) 58;
    numArray9[43] = (byte) 177;
    numArray9[44] = (byte) 253;
    numArray9[49] = (byte) 179;
    numArray9[2] = (byte) 241;
    numArray9[37] = (byte) 97;
    numArray9[31 /*0x1F*/] = (byte) 174;
    numArray9[10] = (byte) 115;
    numArray9[11] = (byte) 114;
    numArray9[14] = (byte) 139;
    numArray9[52] = (byte) 160 /*0xA0*/;
    numArray9[33] = (byte) 221;
    numArray9[48 /*0x30*/] = (byte) 122;
    byte[] numArray10 = new byte[55];
    numArray10[1] = (byte) 66;
    numArray10[51] = (byte) 170;
    numArray10[39] = (byte) 240 /*0xF0*/;
    numArray10[3] = (byte) 153;
    numArray10[4] = (byte) 170;
    numArray10[45] = (byte) 121;
    numArray10[30] = (byte) 199;
    numArray10[7] = (byte) 228;
    numArray10[54] = (byte) 251;
    numArray10[13] = (byte) 244;
    numArray10[6] = (byte) 251;
    numArray10[22] = (byte) 223;
    numArray10[44] = (byte) 243;
    numArray10[46] = (byte) 195;
    numArray10[28] = (byte) 85;
    numArray10[40] = (byte) 65;
    numArray10[20] = (byte) 112 /*0x70*/;
    numArray10[17] = (byte) 216;
    numArray10[16 /*0x10*/] = (byte) 20;
    numArray10[19] = (byte) 51;
    numArray10[33] = (byte) 113;
    numArray10[21] = (byte) 248;
    numArray10[14] = (byte) 144 /*0x90*/;
    numArray10[26] = (byte) 65;
    numArray10[42] = (byte) 241;
    numArray10[15] = (byte) 72;
    numArray10[31 /*0x1F*/] = (byte) 142;
    numArray10[12] = (byte) 188;
    numArray10[35] = (byte) 110;
    numArray10[29] = (byte) 24;
    numArray10[0] = (byte) 4;
    numArray10[5] = (byte) 210;
    numArray10[25] = (byte) 41;
    numArray10[34] = (byte) 24;
    numArray10[32 /*0x20*/] = (byte) 219;
    numArray10[49] = (byte) 114;
    numArray10[36] = (byte) 101;
    numArray10[37] = (byte) 13;
    numArray10[38] = (byte) 211;
    numArray10[18] = (byte) 236;
    numArray10[27] = (byte) 33;
    numArray10[41] = (byte) 98;
    numArray10[24] = (byte) 93;
    numArray10[43] = (byte) 150;
    numArray10[11] = (byte) 26;
    numArray10[48 /*0x30*/] = (byte) 55;
    numArray10[9] = (byte) 45;
    numArray10[47] = (byte) 138;
    numArray10[8] = (byte) 4;
    numArray10[53] = (byte) 155;
    numArray10[50] = (byte) 189;
    numArray10[2] = (byte) 100;
    numArray10[52] = (byte) 84;
    numArray10[10] = (byte) 193;
    numArray10[23] = (byte) 55;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 178,
      (byte) 219,
      (byte) 37,
      (byte) 165,
      (byte) 60,
      (byte) 63 /*0x3F*/,
      (byte) 165,
      (byte) 66,
      (byte) 75,
      (byte) 168,
      (byte) 136,
      (byte) 175,
      (byte) 149,
      (byte) 60,
      (byte) 222,
      (byte) 195,
      (byte) 191,
      (byte) 236,
      (byte) 120,
      (byte) 180,
      (byte) 60,
      (byte) 112 /*0x70*/,
      (byte) 61,
      (byte) 0,
      (byte) 63 /*0x3F*/,
      (byte) 176 /*0xB0*/,
      (byte) 148,
      (byte) 142,
      (byte) 154,
      (byte) 123,
      (byte) 124,
      (byte) 113,
      (byte) 55,
      (byte) 6,
      (byte) 23,
      (byte) 3,
      (byte) 79,
      (byte) 76,
      (byte) 245,
      (byte) 239,
      (byte) 165,
      (byte) 161,
      (byte) 52,
      (byte) 43,
      (byte) 174,
      (byte) 198,
      (byte) 124,
      (byte) 251,
      (byte) 4,
      (byte) 76,
      (byte) 48 /*0x30*/,
      (byte) 175,
      (byte) 69,
      (byte) 185,
      (byte) 6
    };
    byte[] numArray12 = new byte[55];
    numArray12[46] = (byte) 244;
    numArray12[9] = (byte) 49;
    numArray12[17] = (byte) 234;
    numArray12[30] = (byte) 17;
    numArray12[34] = (byte) 231;
    numArray12[51] = (byte) 2;
    numArray12[6] = (byte) 211;
    numArray12[7] = (byte) 92;
    numArray12[8] = (byte) 153;
    numArray12[15] = (byte) 126;
    numArray12[10] = (byte) 21;
    numArray12[2] = (byte) 168;
    numArray12[37] = (byte) 154;
    numArray12[13] = (byte) 48 /*0x30*/;
    numArray12[20] = (byte) 74;
    numArray12[28] = (byte) 77;
    numArray12[1] = (byte) 247;
    numArray12[45] = (byte) 103;
    numArray12[18] = (byte) 1;
    numArray12[33] = (byte) 214;
    numArray12[42] = (byte) 57;
    numArray12[21] = (byte) 95;
    numArray12[50] = (byte) 243;
    numArray12[53] = (byte) 24;
    numArray12[24] = (byte) 28;
    numArray12[25] = (byte) 160 /*0xA0*/;
    numArray12[38] = (byte) 103;
    numArray12[48 /*0x30*/] = (byte) 13;
    numArray12[5] = (byte) 71;
    numArray12[29] = (byte) 160 /*0xA0*/;
    numArray12[23] = (byte) 54;
    numArray12[16 /*0x10*/] = (byte) 235;
    numArray12[32 /*0x20*/] = (byte) 31 /*0x1F*/;
    numArray12[26] = (byte) 226;
    numArray12[31 /*0x1F*/] = (byte) 102;
    numArray12[35] = (byte) 165;
    numArray12[22] = (byte) 86;
    numArray12[36] = (byte) 98;
    numArray12[49] = (byte) 64 /*0x40*/;
    numArray12[39] = (byte) 190;
    numArray12[40] = (byte) 147;
    numArray12[11] = (byte) 33;
    numArray12[27] = (byte) 186;
    numArray12[47] = (byte) 251;
    numArray12[44] = (byte) 41;
    numArray12[43] = (byte) 25;
    numArray12[19] = (byte) 60;
    numArray12[0] = (byte) 117;
    numArray12[4] = (byte) 78;
    numArray12[12] = (byte) 111;
    numArray12[14] = (byte) 96 /*0x60*/;
    numArray12[3] = (byte) 8;
    numArray12[52] = (byte) 239;
    numArray12[41] = (byte) 233;
    numArray12[54] = (byte) 53;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[25];
    numArray13[22] = (byte) 43;
    numArray13[7] = (byte) 96 /*0x60*/;
    numArray13[14] = (byte) 139;
    numArray13[15] = (byte) 140;
    numArray13[4] = (byte) 142;
    numArray13[5] = (byte) 89;
    numArray13[10] = (byte) 141;
    numArray13[8] = (byte) 234;
    numArray13[20] = (byte) 18;
    numArray13[21] = (byte) 186;
    numArray13[2] = (byte) 87;
    numArray13[11] = (byte) 78;
    numArray13[12] = (byte) 118;
    numArray13[6] = (byte) 132;
    numArray13[16 /*0x10*/] = (byte) 68;
    numArray13[13] = (byte) 73;
    numArray13[9] = (byte) 250;
    numArray13[17] = (byte) 129;
    numArray13[18] = (byte) 160 /*0xA0*/;
    numArray13[1] = (byte) 236;
    numArray13[0] = (byte) 210;
    numArray13[19] = (byte) 216;
    numArray13[3] = (byte) 18;
    numArray13[23] = (byte) 69;
    numArray13[24] = (byte) 193;
    byte[] numArray14 = new byte[25]
    {
      (byte) 114,
      (byte) 101,
      (byte) 170,
      (byte) 140,
      (byte) 135,
      (byte) 118,
      (byte) 17,
      (byte) 150,
      (byte) 117,
      (byte) 8,
      (byte) 232,
      (byte) 83,
      (byte) 67,
      (byte) 43,
      (byte) 229,
      (byte) 217,
      (byte) 113,
      (byte) 31 /*0x1F*/,
      (byte) 160 /*0xA0*/,
      (byte) 103,
      (byte) 42,
      (byte) 35,
      (byte) 12,
      (byte) 168,
      (byte) 98
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 25);
    for (int index = 0; index < 25; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12725()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[6] = (byte) 226;
      numArray2[3] = (byte) 18;
      numArray2[4] = (byte) 81;
      numArray2[0] = (byte) 120;
      numArray2[1] = (byte) 23;
      numArray2[5] = (byte) 45;
      numArray2[2] = (byte) 193;
      numArray2[7] = (byte) 247;
      numArray2[8] = (byte) 76;
      numArray2[9] = (byte) 122;
      byte[] numArray3 = new byte[10]
      {
        (byte) 119,
        (byte) 167,
        (byte) 69,
        (byte) 248,
        (byte) 54,
        (byte) 160 /*0xA0*/,
        (byte) 59,
        (byte) 82,
        (byte) 225,
        (byte) 189
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
      (byte) 216,
      (byte) 229,
      (byte) 56,
      (byte) 174,
      (byte) 232,
      (byte) 91,
      (byte) 230,
      (byte) 246,
      (byte) 170,
      (byte) 201
    };
    byte[] numArray6 = new byte[10];
    numArray6[0] = (byte) 48 /*0x30*/;
    numArray6[8] = (byte) 197;
    numArray6[2] = (byte) 153;
    numArray6[3] = (byte) 31 /*0x1F*/;
    numArray6[1] = (byte) 119;
    numArray6[7] = (byte) 13;
    numArray6[6] = (byte) 32 /*0x20*/;
    numArray6[5] = (byte) 47;
    numArray6[4] = (byte) 154;
    numArray6[9] = (byte) 211;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12726(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[5] = (byte) 201;
    sourceArray1[8] = (byte) 9;
    sourceArray1[15] = (byte) 102;
    sourceArray1[36] = (byte) 53;
    sourceArray1[39] = byte.MaxValue;
    sourceArray1[22] = (byte) 88;
    sourceArray1[6] = (byte) 52;
    sourceArray1[4] = (byte) 167;
    sourceArray1[45] = (byte) 199;
    sourceArray1[13] = (byte) 250;
    sourceArray1[10] = (byte) 44;
    sourceArray1[29] = (byte) 218;
    sourceArray1[11] = (byte) 24;
    sourceArray1[34] = (byte) 75;
    sourceArray1[19] = (byte) 221;
    sourceArray1[37] = (byte) 144 /*0x90*/;
    sourceArray1[16 /*0x10*/] = (byte) 132;
    sourceArray1[12] = (byte) 126;
    sourceArray1[14] = (byte) 155;
    sourceArray1[17] = (byte) 124;
    sourceArray1[20] = (byte) 59;
    sourceArray1[21] = (byte) 176 /*0xB0*/;
    sourceArray1[30] = (byte) 171;
    sourceArray1[23] = (byte) 122;
    sourceArray1[33] = (byte) 152;
    sourceArray1[25] = (byte) 175;
    sourceArray1[26] = (byte) 68;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[28] = (byte) 32 /*0x20*/;
    sourceArray1[9] = (byte) 233;
    sourceArray1[3] = (byte) 102;
    sourceArray1[31 /*0x1F*/] = (byte) 127 /*0x7F*/;
    sourceArray1[32 /*0x20*/] = (byte) 47;
    sourceArray1[7] = (byte) 5;
    sourceArray1[42] = (byte) 55;
    sourceArray1[35] = (byte) 187;
    sourceArray1[44] = (byte) 211;
    sourceArray1[18] = (byte) 47;
    sourceArray1[38] = (byte) 2;
    sourceArray1[1] = (byte) 160 /*0xA0*/;
    sourceArray1[40] = (byte) 109;
    sourceArray1[41] = (byte) 91;
    sourceArray1[47] = (byte) 194;
    sourceArray1[43] = (byte) 79;
    sourceArray1[0] = (byte) 157;
    sourceArray1[2] = (byte) 177;
    sourceArray1[46] = (byte) 83;
    sourceArray1[24] = (byte) 50;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 64 /*0x40*/,
      (byte) 171,
      (byte) 142,
      (byte) 41,
      (byte) 159,
      (byte) 157,
      (byte) 246,
      (byte) 208 /*0xD0*/,
      (byte) 247,
      (byte) 12,
      (byte) 174,
      (byte) 11,
      (byte) 209,
      (byte) 129,
      (byte) 166,
      (byte) 106,
      (byte) 112 /*0x70*/,
      (byte) 122,
      (byte) 106,
      (byte) 53,
      (byte) 111,
      (byte) 143,
      (byte) 12,
      (byte) 88,
      (byte) 175,
      (byte) 93,
      (byte) 61,
      (byte) 204,
      (byte) 103,
      (byte) 44,
      (byte) 123,
      (byte) 146,
      (byte) 76,
      (byte) 130,
      (byte) 7,
      (byte) 135,
      (byte) 98,
      (byte) 61,
      (byte) 100,
      (byte) 231,
      (byte) 42,
      (byte) 177,
      (byte) 171,
      (byte) 90,
      (byte) 213,
      (byte) 22,
      (byte) 39,
      (byte) 30
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12727(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 109,
      (byte) 19,
      (byte) 109,
      (byte) 188,
      (byte) 186,
      (byte) 53,
      (byte) 6,
      (byte) 188,
      (byte) 250,
      (byte) 116,
      (byte) 62,
      (byte) 143,
      (byte) 200,
      (byte) 157,
      (byte) 3,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 2,
      (byte) 66,
      (byte) 100,
      (byte) 229,
      (byte) 40,
      (byte) 80 /*0x50*/,
      (byte) 107,
      (byte) 48 /*0x30*/,
      (byte) 152,
      (byte) 112 /*0x70*/,
      (byte) 182,
      (byte) 38,
      (byte) 106,
      (byte) 112 /*0x70*/,
      (byte) 247,
      (byte) 128 /*0x80*/,
      (byte) 141,
      (byte) 237,
      (byte) 110,
      (byte) 86,
      (byte) 119,
      (byte) 114,
      (byte) 197,
      (byte) 65,
      (byte) 71,
      (byte) 195,
      (byte) 234,
      (byte) 54,
      (byte) 111,
      (byte) 148,
      (byte) 107
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 31 /*0x1F*/,
      (byte) 132,
      (byte) 92,
      (byte) 19,
      (byte) 80 /*0x50*/,
      (byte) 115,
      (byte) 84,
      (byte) 122,
      (byte) 107,
      (byte) 16 /*0x10*/,
      (byte) 99,
      (byte) 129,
      (byte) 5,
      (byte) 149,
      (byte) 137,
      (byte) 72,
      (byte) 179,
      (byte) 233,
      (byte) 230,
      (byte) 8,
      (byte) 52,
      (byte) 202,
      (byte) 135,
      (byte) 46,
      (byte) 187,
      (byte) 183,
      (byte) 31 /*0x1F*/,
      (byte) 138,
      (byte) 106,
      (byte) 84,
      (byte) 211,
      (byte) 176 /*0xB0*/,
      (byte) 229,
      (byte) 123,
      (byte) 49,
      (byte) 231,
      (byte) 161,
      (byte) 20,
      (byte) 163,
      (byte) 12,
      (byte) 155,
      (byte) 234,
      (byte) 154,
      (byte) 115,
      (byte) 179,
      (byte) 15,
      (byte) 118,
      (byte) 216
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[35];
    byte[] response2 = new byte[35];
    Array.Copy((Array) sc_12714.sspq, 83, (Array) numArray2, 0, 35);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12714.sspr, 83, (Array) numArray2, 0, 35);
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
}
