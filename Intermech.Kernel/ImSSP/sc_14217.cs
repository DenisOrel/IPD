// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14217
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14217
{
  private static byte[] sspq = new byte[43]
  {
    (byte) 103,
    (byte) 200,
    (byte) 230,
    (byte) 129,
    (byte) 143,
    (byte) 168,
    (byte) 128 /*0x80*/,
    (byte) 45,
    (byte) 70,
    (byte) 59,
    (byte) 19,
    (byte) 47,
    (byte) 82,
    (byte) 55,
    (byte) 5,
    (byte) 35,
    (byte) 22,
    (byte) 251,
    (byte) 172,
    (byte) 199,
    (byte) 223,
    (byte) 233,
    (byte) 151,
    (byte) 185,
    (byte) 218,
    (byte) 191,
    (byte) 217,
    (byte) 219,
    (byte) 189,
    (byte) 117,
    (byte) 196,
    (byte) 187,
    (byte) 180,
    (byte) 192 /*0xC0*/,
    (byte) 154,
    (byte) 171,
    (byte) 63 /*0x3F*/,
    (byte) 242,
    byte.MaxValue,
    (byte) 95,
    (byte) 106,
    (byte) 61,
    (byte) 105
  };
  private static byte[] sspr = new byte[43]
  {
    (byte) 217,
    (byte) 159,
    (byte) 159,
    (byte) 199,
    (byte) 122,
    (byte) 191,
    (byte) 184,
    (byte) 164,
    (byte) 3,
    (byte) 110,
    (byte) 98,
    (byte) 238,
    (byte) 157,
    (byte) 117,
    (byte) 186,
    (byte) 226,
    (byte) 26,
    (byte) 68,
    (byte) 235,
    (byte) 70,
    (byte) 197,
    (byte) 34,
    (byte) 185,
    (byte) 169,
    (byte) 52,
    (byte) 64 /*0x40*/,
    (byte) 91,
    (byte) 73,
    (byte) 211,
    (byte) 11,
    (byte) 134,
    (byte) 213,
    (byte) 24,
    (byte) 158,
    (byte) 72,
    (byte) 20,
    (byte) 60,
    (byte) 5,
    (byte) 8,
    (byte) 42,
    (byte) 94,
    (byte) 182,
    (byte) 211
  };

  internal static int ssp_appserver_14218(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 15,
      (byte) 235,
      (byte) 19,
      (byte) 20,
      (byte) 116,
      (byte) 35,
      (byte) 186,
      (byte) 153,
      (byte) 119,
      (byte) 241,
      (byte) 71,
      (byte) 37,
      (byte) 174,
      (byte) 124,
      (byte) 212,
      (byte) 142,
      (byte) 243,
      (byte) 94,
      (byte) 34,
      (byte) 51,
      (byte) 124,
      (byte) 7,
      (byte) 128 /*0x80*/,
      (byte) 158,
      (byte) 176 /*0xB0*/,
      (byte) 95,
      (byte) 191,
      (byte) 30,
      (byte) 210,
      (byte) 53,
      (byte) 157,
      (byte) 87,
      (byte) 118,
      (byte) 253,
      (byte) 193,
      (byte) 103,
      (byte) 89,
      (byte) 240 /*0xF0*/,
      (byte) 165,
      (byte) 113,
      (byte) 110,
      (byte) 236,
      (byte) 197,
      (byte) 88,
      (byte) 8,
      (byte) 64 /*0x40*/,
      (byte) 151,
      (byte) 5
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[40] = (byte) 205;
    sourceArray2[47] = (byte) 50;
    sourceArray2[2] = (byte) 104;
    sourceArray2[3] = (byte) 18;
    sourceArray2[23] = (byte) 215;
    sourceArray2[46] = (byte) 27;
    sourceArray2[7] = (byte) 116;
    sourceArray2[30] = (byte) 194;
    sourceArray2[8] = (byte) 47;
    sourceArray2[4] = (byte) 152;
    sourceArray2[10] = (byte) 45;
    sourceArray2[32 /*0x20*/] = (byte) 251;
    sourceArray2[0] = (byte) 197;
    sourceArray2[13] = (byte) 65;
    sourceArray2[37] = (byte) 81;
    sourceArray2[15] = (byte) 242;
    sourceArray2[16 /*0x10*/] = (byte) 168;
    sourceArray2[9] = (byte) 96 /*0x60*/;
    sourceArray2[45] = (byte) 100;
    sourceArray2[42] = (byte) 145;
    sourceArray2[22] = (byte) 72;
    sourceArray2[21] = (byte) 241;
    sourceArray2[28] = (byte) 41;
    sourceArray2[12] = (byte) 1;
    sourceArray2[24] = (byte) 139;
    sourceArray2[25] = (byte) 106;
    sourceArray2[17] = (byte) 76;
    sourceArray2[27] = (byte) 63 /*0x3F*/;
    sourceArray2[18] = (byte) 20;
    sourceArray2[29] = (byte) 168;
    sourceArray2[1] = (byte) 24;
    sourceArray2[31 /*0x1F*/] = (byte) 175;
    sourceArray2[19] = (byte) 30;
    sourceArray2[41] = (byte) 161;
    sourceArray2[34] = (byte) 244;
    sourceArray2[20] = (byte) 191;
    sourceArray2[36] = (byte) 120;
    sourceArray2[35] = (byte) 211;
    sourceArray2[33] = (byte) 153;
    sourceArray2[26] = (byte) 59;
    sourceArray2[38] = (byte) 157;
    sourceArray2[14] = (byte) 194;
    sourceArray2[39] = (byte) 174;
    sourceArray2[43] = (byte) 80 /*0x50*/;
    sourceArray2[44] = (byte) 41;
    sourceArray2[11] = (byte) 183;
    sourceArray2[5] = (byte) 157;
    sourceArray2[6] = (byte) 154;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_14219()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[73];
      byte[] numArray2 = new byte[55];
      numArray2[23] = (byte) 1;
      numArray2[44] = (byte) 244;
      numArray2[2] = (byte) 74;
      numArray2[25] = (byte) 79;
      numArray2[15] = (byte) 169;
      numArray2[5] = (byte) 19;
      numArray2[7] = (byte) 93;
      numArray2[18] = (byte) 34;
      numArray2[19] = (byte) 191;
      numArray2[9] = (byte) 151;
      numArray2[10] = (byte) 197;
      numArray2[3] = (byte) 120;
      numArray2[0] = (byte) 145;
      numArray2[33] = (byte) 126;
      numArray2[14] = (byte) 75;
      numArray2[30] = (byte) 105;
      numArray2[1] = (byte) 229;
      numArray2[21] = (byte) 97;
      numArray2[43] = (byte) 62;
      numArray2[29] = (byte) 241;
      numArray2[20] = (byte) 166;
      numArray2[28] = (byte) 162;
      numArray2[12] = (byte) 221;
      numArray2[49] = (byte) 100;
      numArray2[42] = (byte) 231;
      numArray2[16 /*0x10*/] = (byte) 98;
      numArray2[26] = (byte) 118;
      numArray2[17] = (byte) 212;
      numArray2[48 /*0x30*/] = (byte) 68;
      numArray2[27] = (byte) 40;
      numArray2[6] = (byte) 79;
      numArray2[31 /*0x1F*/] = (byte) 189;
      numArray2[32 /*0x20*/] = (byte) 35;
      numArray2[13] = (byte) 242;
      numArray2[34] = (byte) 222;
      numArray2[35] = (byte) 227;
      numArray2[36] = (byte) 111;
      numArray2[40] = (byte) 68;
      numArray2[38] = (byte) 70;
      numArray2[39] = (byte) 11;
      numArray2[11] = (byte) 66;
      numArray2[22] = (byte) 219;
      numArray2[52] = (byte) 97;
      numArray2[8] = (byte) 101;
      numArray2[4] = (byte) 172;
      numArray2[45] = (byte) 186;
      numArray2[46] = (byte) 7;
      numArray2[47] = (byte) 154;
      numArray2[50] = (byte) 112 /*0x70*/;
      numArray2[41] = (byte) 235;
      numArray2[37] = (byte) 192 /*0xC0*/;
      numArray2[51] = (byte) 81;
      numArray2[24] = (byte) 111;
      numArray2[53] = (byte) 68;
      numArray2[54] = (byte) 113;
      byte[] numArray3 = new byte[55]
      {
        (byte) 147,
        (byte) 10,
        (byte) 135,
        (byte) 188,
        (byte) 28,
        (byte) 78,
        (byte) 206,
        (byte) 233,
        (byte) 77,
        (byte) 76,
        (byte) 5,
        (byte) 78,
        (byte) 169,
        (byte) 74,
        (byte) 116,
        (byte) 61,
        (byte) 64 /*0x40*/,
        (byte) 86,
        (byte) 55,
        (byte) 242,
        (byte) 206,
        (byte) 74,
        (byte) 133,
        (byte) 63 /*0x3F*/,
        (byte) 108,
        (byte) 198,
        (byte) 26,
        (byte) 20,
        (byte) 189,
        (byte) 113,
        (byte) 39,
        (byte) 118,
        (byte) 99,
        (byte) 204,
        (byte) 5,
        (byte) 32 /*0x20*/,
        (byte) 174,
        (byte) 49,
        (byte) 244,
        byte.MaxValue,
        (byte) 189,
        (byte) 46,
        (byte) 78,
        (byte) 224 /*0xE0*/,
        (byte) 222,
        (byte) 51,
        (byte) 96 /*0x60*/,
        (byte) 104,
        (byte) 126,
        (byte) 152,
        (byte) 165,
        (byte) 172,
        (byte) 94,
        (byte) 99,
        (byte) 174
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[18];
      numArray4[13] = (byte) 21;
      numArray4[2] = (byte) 20;
      numArray4[11] = (byte) 101;
      numArray4[3] = (byte) 193;
      numArray4[4] = (byte) 67;
      numArray4[5] = (byte) 124;
      numArray4[6] = (byte) 243;
      numArray4[0] = (byte) 60;
      numArray4[8] = (byte) 171;
      numArray4[9] = (byte) 64 /*0x40*/;
      numArray4[16 /*0x10*/] = (byte) 168;
      numArray4[17] = (byte) 94;
      numArray4[12] = (byte) 143;
      numArray4[10] = (byte) 82;
      numArray4[14] = (byte) 233;
      numArray4[15] = (byte) 164;
      numArray4[1] = (byte) 82;
      numArray4[7] = (byte) 22;
      byte[] numArray5 = new byte[18];
      numArray5[13] = (byte) 92;
      numArray5[1] = (byte) 30;
      numArray5[2] = (byte) 251;
      numArray5[16 /*0x10*/] = (byte) 152;
      numArray5[17] = (byte) 61;
      numArray5[5] = (byte) 198;
      numArray5[11] = (byte) 159;
      numArray5[4] = (byte) 199;
      numArray5[8] = (byte) 102;
      numArray5[9] = (byte) 254;
      numArray5[10] = (byte) 209;
      numArray5[3] = (byte) 250;
      numArray5[12] = (byte) 74;
      numArray5[7] = (byte) 88;
      numArray5[14] = (byte) 154;
      numArray5[15] = (byte) 218;
      numArray5[0] = (byte) 200;
      numArray5[6] = (byte) 134;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[73];
    byte[] numArray7 = new byte[55];
    numArray7[38] = (byte) 63 /*0x3F*/;
    numArray7[1] = (byte) 108;
    numArray7[48 /*0x30*/] = (byte) 165;
    numArray7[3] = (byte) 104;
    numArray7[4] = (byte) 64 /*0x40*/;
    numArray7[28] = (byte) 170;
    numArray7[16 /*0x10*/] = (byte) 213;
    numArray7[17] = (byte) 135;
    numArray7[8] = (byte) 253;
    numArray7[9] = (byte) 110;
    numArray7[10] = (byte) 36;
    numArray7[26] = (byte) 53;
    numArray7[50] = (byte) 87;
    numArray7[13] = (byte) 208 /*0xD0*/;
    numArray7[23] = (byte) 186;
    numArray7[25] = (byte) 205;
    numArray7[5] = (byte) 194;
    numArray7[49] = (byte) 230;
    numArray7[18] = (byte) 144 /*0x90*/;
    numArray7[19] = (byte) 223;
    numArray7[20] = (byte) 190;
    numArray7[15] = (byte) 132;
    numArray7[22] = (byte) 52;
    numArray7[39] = (byte) 55;
    numArray7[24] = (byte) 0;
    numArray7[53] = (byte) 61;
    numArray7[40] = (byte) 167;
    numArray7[21] = (byte) 74;
    numArray7[14] = (byte) 251;
    numArray7[46] = (byte) 98;
    numArray7[44] = (byte) 40;
    numArray7[52] = (byte) 82;
    numArray7[32 /*0x20*/] = (byte) 203;
    numArray7[12] = (byte) 131;
    numArray7[34] = (byte) 95;
    numArray7[35] = (byte) 64 /*0x40*/;
    numArray7[36] = (byte) 156;
    numArray7[37] = (byte) 75;
    numArray7[31 /*0x1F*/] = (byte) 135;
    numArray7[0] = (byte) 117;
    numArray7[42] = (byte) 87;
    numArray7[47] = (byte) 78;
    numArray7[11] = (byte) 44;
    numArray7[33] = (byte) 45;
    numArray7[29] = (byte) 39;
    numArray7[45] = (byte) 231;
    numArray7[2] = (byte) 60;
    numArray7[41] = (byte) 48 /*0x30*/;
    numArray7[6] = (byte) 153;
    numArray7[30] = (byte) 70;
    numArray7[43] = (byte) 178;
    numArray7[51] = (byte) 41;
    numArray7[7] = (byte) 42;
    numArray7[27] = (byte) 112 /*0x70*/;
    numArray7[54] = (byte) 122;
    byte[] numArray8 = new byte[55]
    {
      (byte) 38,
      (byte) 155,
      (byte) 237,
      (byte) 130,
      (byte) 189,
      (byte) 142,
      (byte) 83,
      (byte) 175,
      (byte) 135,
      (byte) 53,
      (byte) 119,
      (byte) 245,
      (byte) 68,
      (byte) 109,
      (byte) 173,
      (byte) 227,
      (byte) 181,
      (byte) 99,
      (byte) 154,
      (byte) 135,
      (byte) 205,
      (byte) 24,
      (byte) 90,
      (byte) 78,
      (byte) 150,
      (byte) 105,
      (byte) 146,
      (byte) 135,
      (byte) 202,
      (byte) 57,
      (byte) 99,
      (byte) 154,
      (byte) 90,
      (byte) 102,
      (byte) 173,
      (byte) 216,
      (byte) 224 /*0xE0*/,
      (byte) 191,
      (byte) 155,
      (byte) 252,
      (byte) 150,
      (byte) 184,
      (byte) 112 /*0x70*/,
      (byte) 64 /*0x40*/,
      (byte) 9,
      (byte) 199,
      (byte) 89,
      (byte) 31 /*0x1F*/,
      (byte) 5,
      (byte) 71,
      (byte) 14,
      (byte) 54,
      (byte) 205,
      (byte) 135,
      (byte) 9
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[18]
    {
      (byte) 240 /*0xF0*/,
      (byte) 79,
      (byte) 8,
      (byte) 166,
      (byte) 94,
      (byte) 188,
      (byte) 6,
      (byte) 10,
      (byte) 102,
      (byte) 210,
      (byte) 162,
      (byte) 59,
      (byte) 178,
      (byte) 205,
      (byte) 165,
      (byte) 124,
      (byte) 174,
      (byte) 185
    };
    byte[] numArray10 = new byte[18]
    {
      (byte) 87,
      (byte) 67,
      (byte) 183,
      (byte) 155,
      (byte) 64 /*0x40*/,
      (byte) 104,
      (byte) 0,
      (byte) 38,
      (byte) 2,
      (byte) 245,
      (byte) 88,
      (byte) 229,
      (byte) 105,
      (byte) 238,
      (byte) 71,
      (byte) 71,
      (byte) 123,
      (byte) 135
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 18);
    for (int index = 0; index < 18; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_14220(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[40] = (byte) 41;
    sourceArray1[31 /*0x1F*/] = (byte) 190;
    sourceArray1[13] = (byte) 195;
    sourceArray1[3] = (byte) 197;
    sourceArray1[25] = (byte) 39;
    sourceArray1[10] = (byte) 82;
    sourceArray1[36] = (byte) 126;
    sourceArray1[7] = (byte) 136;
    sourceArray1[37] = (byte) 247;
    sourceArray1[24] = (byte) 48 /*0x30*/;
    sourceArray1[41] = (byte) 74;
    sourceArray1[30] = (byte) 199;
    sourceArray1[9] = (byte) 252;
    sourceArray1[14] = (byte) 4;
    sourceArray1[46] = (byte) 17;
    sourceArray1[32 /*0x20*/] = (byte) 234;
    sourceArray1[18] = (byte) 128 /*0x80*/;
    sourceArray1[27] = (byte) 74;
    sourceArray1[2] = (byte) 224 /*0xE0*/;
    sourceArray1[19] = (byte) 36;
    sourceArray1[17] = (byte) 122;
    sourceArray1[0] = (byte) 94;
    sourceArray1[22] = (byte) 162;
    sourceArray1[23] = (byte) 55;
    sourceArray1[47] = (byte) 184;
    sourceArray1[26] = (byte) 100;
    sourceArray1[21] = (byte) 105;
    sourceArray1[12] = (byte) 148;
    sourceArray1[8] = (byte) 199;
    sourceArray1[29] = (byte) 14;
    sourceArray1[43] = (byte) 63 /*0x3F*/;
    sourceArray1[6] = (byte) 60;
    sourceArray1[42] = (byte) 219;
    sourceArray1[33] = (byte) 190;
    sourceArray1[34] = (byte) 170;
    sourceArray1[35] = (byte) 157;
    sourceArray1[1] = (byte) 8;
    sourceArray1[28] = (byte) 75;
    sourceArray1[38] = (byte) 5;
    sourceArray1[39] = (byte) 205;
    sourceArray1[15] = (byte) 31 /*0x1F*/;
    sourceArray1[4] = (byte) 1;
    sourceArray1[20] = (byte) 180;
    sourceArray1[5] = (byte) 60;
    sourceArray1[44] = (byte) 96 /*0x60*/;
    sourceArray1[45] = (byte) 164;
    sourceArray1[16 /*0x10*/] = (byte) 244;
    sourceArray1[11] = (byte) 41;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[21] = (byte) 74;
    sourceArray2[1] = (byte) 128 /*0x80*/;
    sourceArray2[35] = (byte) 180;
    sourceArray2[3] = (byte) 21;
    sourceArray2[8] = (byte) 41;
    sourceArray2[20] = (byte) 37;
    sourceArray2[10] = (byte) 101;
    sourceArray2[34] = (byte) 166;
    sourceArray2[27] = (byte) 233;
    sourceArray2[47] = (byte) 159;
    sourceArray2[2] = (byte) 145;
    sourceArray2[5] = (byte) 107;
    sourceArray2[46] = (byte) 126;
    sourceArray2[30] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 230;
    sourceArray2[15] = (byte) 24;
    sourceArray2[7] = (byte) 82;
    sourceArray2[32 /*0x20*/] = (byte) 203;
    sourceArray2[0] = (byte) 74;
    sourceArray2[38] = (byte) 231;
    sourceArray2[33] = (byte) 200;
    sourceArray2[12] = (byte) 76;
    sourceArray2[22] = (byte) 77;
    sourceArray2[13] = (byte) 35;
    sourceArray2[24] = (byte) 5;
    sourceArray2[4] = (byte) 235;
    sourceArray2[23] = (byte) 144 /*0x90*/;
    sourceArray2[19] = (byte) 99;
    sourceArray2[28] = (byte) 67;
    sourceArray2[29] = (byte) 94;
    sourceArray2[11] = (byte) 238;
    sourceArray2[9] = (byte) 167;
    sourceArray2[36] = (byte) 182;
    sourceArray2[37] = (byte) 42;
    sourceArray2[25] = (byte) 172;
    sourceArray2[16 /*0x10*/] = (byte) 151;
    sourceArray2[42] = (byte) 15;
    sourceArray2[26] = (byte) 134;
    sourceArray2[6] = (byte) 58;
    sourceArray2[39] = (byte) 156;
    sourceArray2[40] = (byte) 4;
    sourceArray2[41] = (byte) 28;
    sourceArray2[31 /*0x1F*/] = (byte) 201;
    sourceArray2[43] = (byte) 5;
    sourceArray2[44] = (byte) 244;
    sourceArray2[45] = (byte) 189;
    sourceArray2[18] = (byte) 82;
    sourceArray2[17] = (byte) 220;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[22];
    byte[] response2 = new byte[22];
    Array.Copy((Array) sc_14217.sspq, 0, (Array) numArray2, 0, 22);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14217.sspr, 0, (Array) numArray2, 0, 22);
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

  internal static string ssp_appserver_14221()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[103];
      byte[] numArray2 = new byte[55]
      {
        (byte) 243,
        (byte) 212,
        (byte) 91,
        (byte) 19,
        (byte) 200,
        (byte) 21,
        (byte) 135,
        (byte) 20,
        (byte) 252,
        (byte) 197,
        (byte) 62,
        (byte) 226,
        (byte) 246,
        (byte) 220,
        (byte) 219,
        (byte) 219,
        (byte) 175,
        (byte) 137,
        (byte) 250,
        (byte) 148,
        (byte) 207,
        (byte) 74,
        (byte) 136,
        (byte) 229,
        (byte) 24,
        (byte) 125,
        (byte) 253,
        (byte) 87,
        (byte) 83,
        (byte) 135,
        (byte) 61,
        (byte) 123,
        (byte) 126,
        (byte) 167,
        (byte) 242,
        (byte) 173,
        (byte) 51,
        (byte) 148,
        (byte) 183,
        (byte) 23,
        (byte) 191,
        (byte) 39,
        (byte) 23,
        (byte) 105,
        (byte) 20,
        (byte) 84,
        (byte) 15,
        (byte) 70,
        (byte) 59,
        (byte) 31 /*0x1F*/,
        (byte) 253,
        (byte) 62,
        (byte) 228,
        (byte) 84,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[43] = (byte) 223;
      numArray3[1] = (byte) 110;
      numArray3[2] = (byte) 214;
      numArray3[3] = (byte) 252;
      numArray3[35] = (byte) 119;
      numArray3[52] = (byte) 145;
      numArray3[6] = (byte) 241;
      numArray3[0] = (byte) 201;
      numArray3[19] = (byte) 236;
      numArray3[21] = (byte) 153;
      numArray3[10] = (byte) 200;
      numArray3[39] = (byte) 45;
      numArray3[12] = (byte) 121;
      numArray3[5] = (byte) 25;
      numArray3[14] = (byte) 43;
      numArray3[13] = (byte) 173;
      numArray3[11] = (byte) 204;
      numArray3[17] = (byte) 110;
      numArray3[18] = (byte) 128 /*0x80*/;
      numArray3[33] = (byte) 203;
      numArray3[15] = (byte) 58;
      numArray3[24] = (byte) 157;
      numArray3[27] = (byte) 62;
      numArray3[38] = (byte) 213;
      numArray3[32 /*0x20*/] = (byte) 98;
      numArray3[20] = (byte) 145;
      numArray3[26] = (byte) 221;
      numArray3[31 /*0x1F*/] = (byte) 12;
      numArray3[44] = (byte) 62;
      numArray3[29] = (byte) 254;
      numArray3[25] = (byte) 38;
      numArray3[37] = (byte) 192 /*0xC0*/;
      numArray3[49] = (byte) 98;
      numArray3[8] = (byte) 56;
      numArray3[34] = (byte) 108;
      numArray3[22] = (byte) 233;
      numArray3[36] = (byte) 52;
      numArray3[9] = (byte) 97;
      numArray3[51] = (byte) 245;
      numArray3[7] = (byte) 173;
      numArray3[40] = (byte) 131;
      numArray3[41] = (byte) 114;
      numArray3[42] = (byte) 149;
      numArray3[16 /*0x10*/] = (byte) 231;
      numArray3[30] = (byte) 174;
      numArray3[28] = (byte) 230;
      numArray3[46] = (byte) 29;
      numArray3[47] = (byte) 230;
      numArray3[48 /*0x30*/] = (byte) 159;
      numArray3[54] = (byte) 218;
      numArray3[50] = (byte) 168;
      numArray3[4] = (byte) 2;
      numArray3[23] = (byte) 234;
      numArray3[53] = (byte) 156;
      numArray3[45] = (byte) 66;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      numArray4[43] = (byte) 181;
      numArray4[4] = (byte) 20;
      numArray4[42] = (byte) 37;
      numArray4[33] = (byte) 51;
      numArray4[39] = (byte) 217;
      numArray4[19] = (byte) 145;
      numArray4[23] = (byte) 172;
      numArray4[7] = (byte) 136;
      numArray4[20] = (byte) 60;
      numArray4[9] = (byte) 59;
      numArray4[35] = (byte) 97;
      numArray4[11] = (byte) 161;
      numArray4[12] = (byte) 203;
      numArray4[13] = (byte) 234;
      numArray4[14] = (byte) 129;
      numArray4[3] = (byte) 97;
      numArray4[10] = (byte) 248;
      numArray4[24] = (byte) 90;
      numArray4[27] = (byte) 167;
      numArray4[16 /*0x10*/] = (byte) 227;
      numArray4[6] = (byte) 235;
      numArray4[21] = (byte) 128 /*0x80*/;
      numArray4[47] = (byte) 139;
      numArray4[2] = (byte) 222;
      numArray4[8] = (byte) 43;
      numArray4[18] = (byte) 66;
      numArray4[15] = (byte) 101;
      numArray4[44] = (byte) 201;
      numArray4[1] = (byte) 55;
      numArray4[29] = (byte) 149;
      numArray4[5] = (byte) 140;
      numArray4[26] = (byte) 189;
      numArray4[32 /*0x20*/] = (byte) 211;
      numArray4[28] = (byte) 147;
      numArray4[34] = (byte) 24;
      numArray4[31 /*0x1F*/] = (byte) 174;
      numArray4[36] = (byte) 142;
      numArray4[37] = (byte) 157;
      numArray4[38] = (byte) 122;
      numArray4[25] = (byte) 14;
      numArray4[40] = (byte) 182;
      numArray4[41] = (byte) 89;
      numArray4[0] = (byte) 164;
      numArray4[22] = (byte) 211;
      numArray4[17] = (byte) 70;
      numArray4[45] = (byte) 186;
      numArray4[46] = (byte) 66;
      numArray4[30] = (byte) 169;
      byte[] numArray5 = new byte[48 /*0x30*/];
      numArray5[3] = (byte) 221;
      numArray5[1] = (byte) 62;
      numArray5[4] = (byte) 211;
      numArray5[22] = (byte) 199;
      numArray5[10] = (byte) 78;
      numArray5[16 /*0x10*/] = (byte) 210;
      numArray5[6] = (byte) 219;
      numArray5[34] = (byte) 113;
      numArray5[2] = (byte) 43;
      numArray5[9] = (byte) 222;
      numArray5[0] = (byte) 149;
      numArray5[7] = (byte) 89;
      numArray5[12] = (byte) 45;
      numArray5[32 /*0x20*/] = (byte) 243;
      numArray5[14] = (byte) 168;
      numArray5[15] = (byte) 41;
      numArray5[44] = (byte) 141;
      numArray5[17] = (byte) 45;
      numArray5[24] = (byte) 207;
      numArray5[19] = (byte) 176 /*0xB0*/;
      numArray5[13] = (byte) 60;
      numArray5[21] = (byte) 228;
      numArray5[41] = (byte) 71;
      numArray5[11] = (byte) 246;
      numArray5[5] = (byte) 122;
      numArray5[25] = (byte) 205;
      numArray5[18] = (byte) 77;
      numArray5[45] = (byte) 126;
      numArray5[35] = (byte) 74;
      numArray5[23] = (byte) 193;
      numArray5[30] = (byte) 192 /*0xC0*/;
      numArray5[20] = (byte) 152;
      numArray5[33] = (byte) 32 /*0x20*/;
      numArray5[42] = (byte) 189;
      numArray5[28] = (byte) 142;
      numArray5[40] = (byte) 50;
      numArray5[36] = (byte) 137;
      numArray5[37] = (byte) 145;
      numArray5[38] = (byte) 65;
      numArray5[39] = (byte) 145;
      numArray5[27] = (byte) 115;
      numArray5[26] = (byte) 16 /*0x10*/;
      numArray5[29] = (byte) 224 /*0xE0*/;
      numArray5[43] = (byte) 166;
      numArray5[31 /*0x1F*/] = (byte) 2;
      numArray5[47] = (byte) 208 /*0xD0*/;
      numArray5[46] = (byte) 243;
      numArray5[8] = (byte) 115;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 48 /*0x30*/);
      for (int index = 0; index < 48 /*0x30*/; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[103];
    byte[] numArray7 = new byte[55]
    {
      (byte) 46,
      (byte) 59,
      (byte) 109,
      (byte) 116,
      (byte) 250,
      (byte) 114,
      (byte) 2,
      (byte) 125,
      (byte) 11,
      (byte) 84,
      (byte) 219,
      (byte) 28,
      (byte) 180,
      (byte) 38,
      (byte) 23,
      (byte) 159,
      (byte) 162,
      (byte) 144 /*0x90*/,
      byte.MaxValue,
      (byte) 76,
      (byte) 234,
      (byte) 180,
      (byte) 23,
      (byte) 37,
      (byte) 248,
      (byte) 12,
      (byte) 123,
      (byte) 31 /*0x1F*/,
      (byte) 182,
      (byte) 252,
      (byte) 34,
      (byte) 179,
      (byte) 109,
      (byte) 109,
      (byte) 130,
      (byte) 27,
      (byte) 240 /*0xF0*/,
      (byte) 242,
      (byte) 131,
      (byte) 87,
      (byte) 247,
      (byte) 230,
      (byte) 167,
      (byte) 205,
      (byte) 156,
      (byte) 232,
      (byte) 168,
      (byte) 123,
      (byte) 234,
      (byte) 171,
      (byte) 64 /*0x40*/,
      (byte) 163,
      (byte) 43,
      (byte) 105,
      (byte) 34
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 220,
      (byte) 104,
      (byte) 142,
      (byte) 196,
      (byte) 226,
      (byte) 80 /*0x50*/,
      (byte) 12,
      (byte) 171,
      (byte) 221,
      (byte) 234,
      (byte) 91,
      (byte) 0,
      (byte) 131,
      (byte) 153,
      (byte) 79,
      (byte) 154,
      (byte) 47,
      (byte) 92,
      (byte) 86,
      (byte) 251,
      (byte) 235,
      (byte) 107,
      (byte) 47,
      (byte) 167,
      (byte) 58,
      (byte) 25,
      (byte) 116,
      (byte) 184,
      (byte) 19,
      (byte) 237,
      (byte) 136,
      (byte) 202,
      (byte) 55,
      (byte) 177,
      (byte) 143,
      (byte) 60,
      (byte) 220,
      (byte) 182,
      (byte) 17,
      (byte) 116,
      (byte) 139,
      (byte) 105,
      (byte) 105,
      (byte) 12,
      (byte) 162,
      (byte) 12,
      (byte) 72,
      (byte) 63 /*0x3F*/,
      (byte) 42,
      (byte) 36,
      (byte) 69,
      (byte) 209,
      (byte) 95,
      (byte) 62,
      (byte) 87
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[48 /*0x30*/]
    {
      (byte) 72,
      (byte) 124,
      (byte) 237,
      (byte) 247,
      (byte) 227,
      (byte) 199,
      (byte) 15,
      (byte) 50,
      (byte) 40,
      (byte) 175,
      (byte) 70,
      (byte) 164,
      (byte) 252,
      (byte) 175,
      (byte) 176 /*0xB0*/,
      (byte) 12,
      (byte) 167,
      (byte) 85,
      (byte) 4,
      (byte) 13,
      (byte) 74,
      (byte) 137,
      (byte) 15,
      (byte) 100,
      (byte) 0,
      (byte) 53,
      (byte) 10,
      (byte) 188,
      (byte) 126,
      (byte) 135,
      (byte) 4,
      (byte) 75,
      (byte) 9,
      (byte) 248,
      (byte) 167,
      (byte) 118,
      (byte) 150,
      (byte) 96 /*0x60*/,
      (byte) 20,
      (byte) 31 /*0x1F*/,
      (byte) 152,
      (byte) 154,
      (byte) 115,
      (byte) 57,
      (byte) 97,
      (byte) 215,
      (byte) 237,
      (byte) 251
    };
    byte[] numArray10 = new byte[48 /*0x30*/]
    {
      (byte) 171,
      (byte) 42,
      (byte) 191,
      (byte) 224 /*0xE0*/,
      (byte) 229,
      (byte) 193,
      (byte) 109,
      (byte) 123,
      (byte) 186,
      (byte) 205,
      (byte) 6,
      (byte) 100,
      (byte) 150,
      (byte) 193,
      (byte) 232,
      (byte) 171,
      (byte) 240 /*0xF0*/,
      (byte) 92,
      (byte) 170,
      (byte) 33,
      (byte) 18,
      (byte) 192 /*0xC0*/,
      (byte) 172,
      (byte) 174,
      (byte) 139,
      (byte) 147,
      (byte) 178,
      (byte) 32 /*0x20*/,
      (byte) 240 /*0xF0*/,
      (byte) 20,
      (byte) 39,
      (byte) 77,
      (byte) 7,
      (byte) 64 /*0x40*/,
      (byte) 47,
      (byte) 87,
      (byte) 122,
      (byte) 167,
      (byte) 104,
      (byte) 204,
      (byte) 108,
      (byte) 249,
      (byte) 7,
      (byte) 60,
      (byte) 239,
      (byte) 29,
      (byte) 194,
      (byte) 252
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 48 /*0x30*/);
    for (int index = 0; index < 48 /*0x30*/; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_14222()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[60];
      byte[] numArray2 = new byte[55]
      {
        (byte) 30,
        (byte) 107,
        (byte) 216,
        (byte) 191,
        (byte) 108,
        (byte) 136,
        (byte) 246,
        (byte) 174,
        (byte) 188,
        (byte) 82,
        (byte) 179,
        (byte) 160 /*0xA0*/,
        (byte) 171,
        (byte) 251,
        (byte) 155,
        (byte) 4,
        (byte) 7,
        (byte) 57,
        (byte) 118,
        (byte) 70,
        (byte) 64 /*0x40*/,
        (byte) 229,
        (byte) 117,
        (byte) 175,
        (byte) 21,
        (byte) 214,
        (byte) 3,
        (byte) 41,
        (byte) 71,
        (byte) 14,
        (byte) 43,
        (byte) 231,
        (byte) 3,
        (byte) 239,
        (byte) 44,
        (byte) 157,
        (byte) 31 /*0x1F*/,
        (byte) 232,
        (byte) 9,
        (byte) 13,
        (byte) 105,
        (byte) 101,
        (byte) 98,
        (byte) 69,
        (byte) 231,
        (byte) 210,
        (byte) 141,
        (byte) 102,
        (byte) 60,
        (byte) 233,
        (byte) 244,
        (byte) 86,
        (byte) 32 /*0x20*/,
        (byte) 61,
        (byte) 211
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 252,
        (byte) 61,
        (byte) 60,
        (byte) 118,
        (byte) 208 /*0xD0*/,
        (byte) 190,
        (byte) 117,
        (byte) 219,
        (byte) 24,
        (byte) 200,
        (byte) 118,
        (byte) 198,
        (byte) 40,
        (byte) 215,
        (byte) 233,
        (byte) 181,
        (byte) 117,
        (byte) 151,
        (byte) 203,
        (byte) 222,
        (byte) 37,
        (byte) 121,
        (byte) 40,
        (byte) 95,
        (byte) 19,
        (byte) 89,
        (byte) 67,
        (byte) 119,
        (byte) 196,
        (byte) 63 /*0x3F*/,
        (byte) 10,
        (byte) 128 /*0x80*/,
        (byte) 48 /*0x30*/,
        (byte) 206,
        (byte) 31 /*0x1F*/,
        (byte) 170,
        (byte) 73,
        (byte) 100,
        (byte) 95,
        (byte) 188,
        (byte) 108,
        (byte) 65,
        (byte) 83,
        (byte) 9,
        (byte) 111,
        (byte) 169,
        (byte) 160 /*0xA0*/,
        (byte) 29,
        (byte) 174,
        (byte) 37,
        (byte) 93,
        (byte) 147,
        (byte) 111,
        (byte) 135,
        (byte) 69
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[5]
      {
        (byte) 0,
        (byte) 0,
        (byte) 150,
        (byte) 0,
        (byte) 0
      };
      numArray4[1] = (byte) 169;
      numArray4[4] = (byte) 205;
      numArray4[3] = (byte) 133;
      numArray4[0] = (byte) 41;
      byte[] numArray5 = new byte[5]
      {
        (byte) 253,
        (byte) 60,
        (byte) 66,
        (byte) 196,
        (byte) 99
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[60];
    byte[] numArray7 = new byte[55]
    {
      (byte) 228,
      (byte) 31 /*0x1F*/,
      (byte) 48 /*0x30*/,
      (byte) 105,
      (byte) 221,
      (byte) 219,
      (byte) 130,
      (byte) 47,
      (byte) 200,
      (byte) 67,
      (byte) 72,
      (byte) 156,
      (byte) 236,
      (byte) 93,
      (byte) 26,
      (byte) 212,
      (byte) 198,
      (byte) 197,
      (byte) 27,
      (byte) 243,
      (byte) 166,
      (byte) 246,
      (byte) 63 /*0x3F*/,
      (byte) 115,
      (byte) 135,
      (byte) 155,
      byte.MaxValue,
      (byte) 181,
      (byte) 30,
      (byte) 27,
      (byte) 53,
      (byte) 210,
      (byte) 151,
      (byte) 62,
      (byte) 5,
      (byte) 212,
      (byte) 38,
      (byte) 183,
      (byte) 96 /*0x60*/,
      (byte) 248,
      (byte) 183,
      (byte) 198,
      (byte) 96 /*0x60*/,
      (byte) 102,
      (byte) 77,
      (byte) 233,
      (byte) 224 /*0xE0*/,
      (byte) 9,
      (byte) 146,
      (byte) 82,
      (byte) 4,
      (byte) 215,
      (byte) 159,
      (byte) 219,
      (byte) 86
    };
    byte[] numArray8 = new byte[55];
    numArray8[14] = (byte) 17;
    numArray8[47] = (byte) 1;
    numArray8[42] = (byte) 108;
    numArray8[13] = (byte) 13;
    numArray8[4] = (byte) 189;
    numArray8[28] = (byte) 141;
    numArray8[6] = (byte) 112 /*0x70*/;
    numArray8[7] = (byte) 19;
    numArray8[19] = (byte) 189;
    numArray8[9] = (byte) 121;
    numArray8[26] = (byte) 180;
    numArray8[11] = (byte) 33;
    numArray8[12] = (byte) 13;
    numArray8[16 /*0x10*/] = (byte) 155;
    numArray8[43] = (byte) 182;
    numArray8[45] = (byte) 8;
    numArray8[0] = (byte) 141;
    numArray8[17] = (byte) 209;
    numArray8[15] = (byte) 106;
    numArray8[23] = (byte) 240 /*0xF0*/;
    numArray8[49] = (byte) 248;
    numArray8[21] = (byte) 38;
    numArray8[39] = (byte) 71;
    numArray8[48 /*0x30*/] = (byte) 252;
    numArray8[24] = (byte) 231;
    numArray8[25] = (byte) 105;
    numArray8[18] = (byte) 11;
    numArray8[27] = (byte) 120;
    numArray8[2] = (byte) 27;
    numArray8[41] = (byte) 3;
    numArray8[30] = (byte) 48 /*0x30*/;
    numArray8[31 /*0x1F*/] = (byte) 240 /*0xF0*/;
    numArray8[1] = (byte) 240 /*0xF0*/;
    numArray8[33] = (byte) 246;
    numArray8[37] = (byte) 254;
    numArray8[35] = (byte) 26;
    numArray8[32 /*0x20*/] = (byte) 201;
    numArray8[10] = (byte) 126;
    numArray8[38] = (byte) 29;
    numArray8[5] = (byte) 144 /*0x90*/;
    numArray8[40] = (byte) 163;
    numArray8[3] = (byte) 57;
    numArray8[8] = (byte) 92;
    numArray8[22] = (byte) 21;
    numArray8[44] = (byte) 68;
    numArray8[51] = (byte) 29;
    numArray8[46] = (byte) 150;
    numArray8[20] = (byte) 247;
    numArray8[34] = (byte) 35;
    numArray8[50] = (byte) 72;
    numArray8[29] = (byte) 98;
    numArray8[36] = (byte) 64 /*0x40*/;
    numArray8[52] = (byte) 179;
    numArray8[53] = (byte) 61;
    numArray8[54] = (byte) 39;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[5]
    {
      (byte) 151,
      (byte) 67,
      (byte) 134,
      (byte) 229,
      (byte) 170
    };
    byte[] numArray10 = new byte[5]
    {
      (byte) 60,
      (byte) 37,
      (byte) 12,
      (byte) 92,
      (byte) 162
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 5);
    for (int index = 0; index < 5; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_14223(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 199,
      (byte) 65,
      (byte) 80 /*0x50*/,
      (byte) 68,
      (byte) 57,
      (byte) 71,
      (byte) 6,
      (byte) 82,
      (byte) 43,
      (byte) 225,
      (byte) 128 /*0x80*/,
      (byte) 77,
      (byte) 186,
      (byte) 136,
      (byte) 69,
      (byte) 213,
      (byte) 40,
      (byte) 32 /*0x20*/,
      (byte) 236,
      (byte) 181,
      (byte) 29,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 238,
      (byte) 209,
      (byte) 146,
      (byte) 151,
      (byte) 54,
      (byte) 62,
      (byte) 111,
      (byte) 29,
      (byte) 168,
      (byte) 147,
      (byte) 156,
      (byte) 107,
      (byte) 110,
      (byte) 252,
      (byte) 94,
      (byte) 188,
      (byte) 42,
      (byte) 117,
      (byte) 76,
      (byte) 75,
      (byte) 119,
      (byte) 134,
      (byte) 73,
      (byte) 137,
      (byte) 254
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 34,
      (byte) 80 /*0x50*/,
      (byte) 164,
      (byte) 172,
      (byte) 60,
      (byte) 135,
      (byte) 30,
      (byte) 46,
      (byte) 131,
      (byte) 132,
      (byte) 147,
      (byte) 251,
      (byte) 89,
      (byte) 47,
      (byte) 209,
      (byte) 43,
      (byte) 19,
      (byte) 91,
      (byte) 126,
      (byte) 123,
      (byte) 61,
      (byte) 217,
      (byte) 160 /*0xA0*/,
      (byte) 26,
      (byte) 232,
      (byte) 153,
      (byte) 77,
      (byte) 144 /*0x90*/,
      (byte) 32 /*0x20*/,
      (byte) 178,
      (byte) 133,
      (byte) 194,
      (byte) 92,
      (byte) 186,
      (byte) 100,
      (byte) 91,
      (byte) 144 /*0x90*/,
      (byte) 219,
      (byte) 51,
      (byte) 177,
      (byte) 251,
      (byte) 197,
      (byte) 250,
      (byte) 149,
      (byte) 47,
      (byte) 188,
      (byte) 89,
      (byte) 13
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_14224(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 142,
      (byte) 42,
      (byte) 86,
      (byte) 132,
      (byte) 98,
      (byte) 115,
      (byte) 166,
      (byte) 175,
      (byte) 207,
      (byte) 19,
      (byte) 187,
      (byte) 195,
      (byte) 216,
      (byte) 186,
      (byte) 30,
      (byte) 28,
      (byte) 215,
      (byte) 217,
      (byte) 201,
      (byte) 99,
      (byte) 193,
      (byte) 63 /*0x3F*/,
      (byte) 209,
      (byte) 37,
      (byte) 70,
      (byte) 104,
      (byte) 135,
      (byte) 9,
      (byte) 152,
      (byte) 150,
      (byte) 105,
      (byte) 82,
      (byte) 167,
      (byte) 228,
      (byte) 5,
      (byte) 113,
      (byte) 20,
      (byte) 234,
      (byte) 153,
      (byte) 235,
      (byte) 22,
      (byte) 122,
      (byte) 166,
      (byte) 57,
      (byte) 143,
      (byte) 179,
      (byte) 173,
      (byte) 143
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[32 /*0x20*/] = (byte) 30;
    sourceArray2[36] = (byte) 100;
    sourceArray2[17] = (byte) 139;
    sourceArray2[3] = (byte) 148;
    sourceArray2[7] = (byte) 109;
    sourceArray2[5] = (byte) 196;
    sourceArray2[1] = (byte) 160 /*0xA0*/;
    sourceArray2[25] = (byte) 243;
    sourceArray2[8] = (byte) 166;
    sourceArray2[24] = (byte) 196;
    sourceArray2[45] = (byte) 223;
    sourceArray2[21] = (byte) 10;
    sourceArray2[27] = (byte) 242;
    sourceArray2[13] = (byte) 89;
    sourceArray2[23] = (byte) 85;
    sourceArray2[20] = (byte) 65;
    sourceArray2[16 /*0x10*/] = (byte) 52;
    sourceArray2[2] = (byte) 196;
    sourceArray2[18] = (byte) 214;
    sourceArray2[42] = (byte) 170;
    sourceArray2[9] = (byte) 22;
    sourceArray2[41] = (byte) 6;
    sourceArray2[22] = (byte) 181;
    sourceArray2[44] = (byte) 27;
    sourceArray2[6] = (byte) 78;
    sourceArray2[11] = (byte) 82;
    sourceArray2[10] = (byte) 9;
    sourceArray2[26] = (byte) 174;
    sourceArray2[28] = (byte) 67;
    sourceArray2[29] = (byte) 2;
    sourceArray2[12] = (byte) 162;
    sourceArray2[31 /*0x1F*/] = (byte) 86;
    sourceArray2[15] = (byte) 190;
    sourceArray2[33] = (byte) 84;
    sourceArray2[30] = (byte) 207;
    sourceArray2[35] = (byte) 149;
    sourceArray2[0] = (byte) 132;
    sourceArray2[37] = (byte) 41;
    sourceArray2[39] = (byte) 93;
    sourceArray2[34] = (byte) 246;
    sourceArray2[40] = (byte) 12;
    sourceArray2[4] = (byte) 47;
    sourceArray2[14] = (byte) 215;
    sourceArray2[43] = (byte) 10;
    sourceArray2[19] = (byte) 144 /*0x90*/;
    sourceArray2[38] = (byte) 132;
    sourceArray2[46] = (byte) 227;
    sourceArray2[47] = (byte) 160 /*0xA0*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[21];
    byte[] response2 = new byte[21];
    Array.Copy((Array) sc_14217.sspq, 22, (Array) numArray2, 0, 21);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14217.sspr, 22, (Array) numArray2, 0, 21);
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

  internal static string ssp_appserver_14225()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24];
      numArray2[10] = (byte) 9;
      numArray2[20] = (byte) 208 /*0xD0*/;
      numArray2[2] = (byte) 147;
      numArray2[3] = (byte) 35;
      numArray2[18] = (byte) 175;
      numArray2[5] = (byte) 160 /*0xA0*/;
      numArray2[23] = (byte) 87;
      numArray2[8] = (byte) 226;
      numArray2[4] = (byte) 64 /*0x40*/;
      numArray2[14] = (byte) 30;
      numArray2[19] = (byte) 138;
      numArray2[21] = (byte) 38;
      numArray2[1] = (byte) 254;
      numArray2[13] = (byte) 1;
      numArray2[6] = (byte) 221;
      numArray2[15] = (byte) 222;
      numArray2[22] = (byte) 1;
      numArray2[11] = (byte) 112 /*0x70*/;
      numArray2[16 /*0x10*/] = (byte) 221;
      numArray2[7] = (byte) 213;
      numArray2[9] = (byte) 44;
      numArray2[12] = (byte) 250;
      numArray2[17] = (byte) 242;
      numArray2[0] = (byte) 18;
      byte[] numArray3 = new byte[24]
      {
        (byte) 75,
        (byte) 165,
        (byte) 208 /*0xD0*/,
        (byte) 107,
        (byte) 150,
        (byte) 98,
        (byte) 90,
        (byte) 91,
        (byte) 126,
        (byte) 164,
        (byte) 189,
        (byte) 111,
        (byte) 29,
        (byte) 253,
        (byte) 11,
        (byte) 47,
        (byte) 98,
        (byte) 149,
        (byte) 36,
        (byte) 204,
        (byte) 243,
        (byte) 85,
        (byte) 128 /*0x80*/,
        (byte) 131
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 120,
      (byte) 141,
      (byte) 222,
      (byte) 182,
      (byte) 235,
      (byte) 253,
      (byte) 59,
      (byte) 218,
      (byte) 63 /*0x3F*/,
      (byte) 64 /*0x40*/,
      (byte) 207,
      (byte) 92,
      (byte) 221,
      (byte) 42,
      (byte) 183,
      (byte) 178,
      (byte) 234,
      (byte) 89,
      (byte) 67,
      (byte) 178,
      (byte) 39,
      (byte) 86,
      (byte) 146,
      (byte) 243
    };
    byte[] numArray6 = new byte[24];
    numArray6[14] = (byte) 38;
    numArray6[6] = (byte) 158;
    numArray6[2] = (byte) 215;
    numArray6[3] = (byte) 246;
    numArray6[9] = (byte) 158;
    numArray6[4] = (byte) 253;
    numArray6[21] = (byte) 144 /*0x90*/;
    numArray6[7] = (byte) 208 /*0xD0*/;
    numArray6[0] = (byte) 197;
    numArray6[17] = (byte) 196;
    numArray6[10] = (byte) 92;
    numArray6[11] = (byte) 68;
    numArray6[12] = (byte) 3;
    numArray6[13] = (byte) 119;
    numArray6[5] = (byte) 46;
    numArray6[18] = (byte) 246;
    numArray6[15] = (byte) 117;
    numArray6[16 /*0x10*/] = (byte) 90;
    numArray6[1] = (byte) 240 /*0xF0*/;
    numArray6[8] = (byte) 214;
    numArray6[20] = (byte) 172;
    numArray6[22] = (byte) 164;
    numArray6[23] = (byte) 20;
    numArray6[19] = (byte) 73;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
