// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12679
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12679
{
  internal static string ssp_appserver_12680()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[72];
      byte[] numArray2 = new byte[55]
      {
        (byte) 46,
        (byte) 80 /*0x50*/,
        (byte) 222,
        (byte) 85,
        (byte) 247,
        (byte) 151,
        (byte) 8,
        (byte) 122,
        (byte) 134,
        (byte) 189,
        (byte) 58,
        (byte) 9,
        (byte) 234,
        (byte) 133,
        (byte) 133,
        (byte) 184,
        (byte) 230,
        (byte) 121,
        (byte) 137,
        (byte) 84,
        (byte) 38,
        (byte) 134,
        (byte) 168,
        (byte) 75,
        (byte) 144 /*0x90*/,
        (byte) 64 /*0x40*/,
        (byte) 241,
        (byte) 180,
        (byte) 196,
        (byte) 48 /*0x30*/,
        (byte) 229,
        (byte) 167,
        (byte) 136,
        (byte) 193,
        (byte) 127 /*0x7F*/,
        (byte) 164,
        (byte) 87,
        (byte) 152,
        (byte) 187,
        (byte) 206,
        (byte) 183,
        (byte) 150,
        (byte) 224 /*0xE0*/,
        (byte) 230,
        (byte) 28,
        (byte) 188,
        (byte) 174,
        (byte) 46,
        (byte) 50,
        (byte) 22,
        (byte) 67,
        (byte) 211,
        (byte) 54,
        (byte) 27,
        (byte) 155
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 206,
        (byte) 47,
        (byte) 209,
        (byte) 25,
        (byte) 225,
        (byte) 151,
        (byte) 92,
        (byte) 190,
        (byte) 57,
        (byte) 227,
        (byte) 107,
        (byte) 112 /*0x70*/,
        (byte) 93,
        (byte) 224 /*0xE0*/,
        (byte) 14,
        (byte) 215,
        (byte) 239,
        (byte) 31 /*0x1F*/,
        (byte) 66,
        (byte) 30,
        (byte) 174,
        (byte) 225,
        (byte) 205,
        (byte) 19,
        (byte) 11,
        (byte) 163,
        (byte) 134,
        (byte) 213,
        (byte) 28,
        (byte) 199,
        (byte) 176 /*0xB0*/,
        (byte) 182,
        (byte) 198,
        (byte) 43,
        (byte) 65,
        (byte) 19,
        (byte) 223,
        (byte) 224 /*0xE0*/,
        (byte) 124,
        (byte) 199,
        (byte) 101,
        (byte) 30,
        (byte) 108,
        (byte) 97,
        (byte) 126,
        (byte) 198,
        (byte) 65,
        (byte) 229,
        (byte) 158,
        (byte) 37,
        (byte) 128 /*0x80*/,
        (byte) 107,
        (byte) 141,
        (byte) 217,
        (byte) 154
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17]
      {
        (byte) 147,
        (byte) 196,
        (byte) 183,
        (byte) 165,
        (byte) 83,
        (byte) 182,
        (byte) 85,
        (byte) 215,
        (byte) 146,
        (byte) 129,
        (byte) 41,
        (byte) 5,
        (byte) 225,
        (byte) 96 /*0x60*/,
        (byte) 78,
        (byte) 190,
        (byte) 187
      };
      byte[] numArray5 = new byte[17]
      {
        (byte) 146,
        (byte) 151,
        (byte) 129,
        (byte) 117,
        (byte) 237,
        (byte) 231,
        (byte) 140,
        (byte) 126,
        (byte) 163,
        (byte) 182,
        (byte) 30,
        (byte) 141,
        (byte) 183,
        (byte) 135,
        (byte) 55,
        (byte) 118,
        (byte) 71
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[72];
    byte[] numArray7 = new byte[55];
    numArray7[23] = (byte) 59;
    numArray7[0] = (byte) 194;
    numArray7[34] = (byte) 228;
    numArray7[54] = (byte) 26;
    numArray7[24] = (byte) 133;
    numArray7[5] = (byte) 130;
    numArray7[6] = (byte) 173;
    numArray7[27] = (byte) 181;
    numArray7[8] = (byte) 18;
    numArray7[35] = (byte) 100;
    numArray7[26] = (byte) 81;
    numArray7[11] = (byte) 37;
    numArray7[12] = (byte) 215;
    numArray7[13] = (byte) 252;
    numArray7[38] = (byte) 226;
    numArray7[15] = (byte) 212;
    numArray7[16 /*0x10*/] = (byte) 3;
    numArray7[3] = (byte) 145;
    numArray7[29] = (byte) 20;
    numArray7[46] = (byte) 46;
    numArray7[20] = (byte) 139;
    numArray7[21] = (byte) 14;
    numArray7[22] = (byte) 137;
    numArray7[25] = (byte) 40;
    numArray7[7] = (byte) 237;
    numArray7[49] = (byte) 121;
    numArray7[33] = (byte) 2;
    numArray7[53] = (byte) 175;
    numArray7[28] = (byte) 245;
    numArray7[9] = (byte) 112 /*0x70*/;
    numArray7[1] = (byte) 46;
    numArray7[31 /*0x1F*/] = (byte) 126;
    numArray7[32 /*0x20*/] = (byte) 11;
    numArray7[14] = (byte) 196;
    numArray7[2] = (byte) 211;
    numArray7[47] = (byte) 228;
    numArray7[44] = (byte) 19;
    numArray7[37] = (byte) 37;
    numArray7[18] = (byte) 99;
    numArray7[39] = (byte) 209;
    numArray7[40] = (byte) 8;
    numArray7[30] = (byte) 123;
    numArray7[42] = (byte) 197;
    numArray7[10] = (byte) 109;
    numArray7[43] = (byte) 33;
    numArray7[41] = (byte) 103;
    numArray7[19] = (byte) 224 /*0xE0*/;
    numArray7[4] = (byte) 195;
    numArray7[48 /*0x30*/] = (byte) 156;
    numArray7[52] = (byte) 56;
    numArray7[50] = (byte) 186;
    numArray7[51] = (byte) 147;
    numArray7[45] = (byte) 3;
    numArray7[36] = (byte) 240 /*0xF0*/;
    numArray7[17] = (byte) 40;
    byte[] numArray8 = new byte[55]
    {
      (byte) 146,
      (byte) 125,
      (byte) 223,
      (byte) 175,
      (byte) 72,
      (byte) 32 /*0x20*/,
      (byte) 170,
      (byte) 208 /*0xD0*/,
      (byte) 192 /*0xC0*/,
      (byte) 169,
      (byte) 102,
      (byte) 143,
      (byte) 204,
      (byte) 50,
      (byte) 72,
      (byte) 49,
      (byte) 244,
      (byte) 101,
      (byte) 1,
      (byte) 40,
      (byte) 120,
      (byte) 5,
      (byte) 127 /*0x7F*/,
      (byte) 81,
      (byte) 91,
      (byte) 217,
      (byte) 134,
      (byte) 177,
      (byte) 170,
      (byte) 10,
      (byte) 38,
      (byte) 233,
      (byte) 52,
      (byte) 16 /*0x10*/,
      (byte) 127 /*0x7F*/,
      (byte) 181,
      (byte) 101,
      (byte) 32 /*0x20*/,
      (byte) 90,
      (byte) 123,
      (byte) 8,
      (byte) 124,
      (byte) 127 /*0x7F*/,
      (byte) 30,
      (byte) 189,
      (byte) 46,
      (byte) 226,
      (byte) 224 /*0xE0*/,
      (byte) 116,
      (byte) 51,
      (byte) 78,
      (byte) 111,
      (byte) 76,
      (byte) 67,
      (byte) 26
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[17]
    {
      (byte) 112 /*0x70*/,
      (byte) 138,
      (byte) 161,
      (byte) 195,
      (byte) 222,
      (byte) 125,
      (byte) 162,
      (byte) 13,
      (byte) 168,
      (byte) 190,
      (byte) 236,
      (byte) 231,
      (byte) 160 /*0xA0*/,
      (byte) 138,
      (byte) 198,
      (byte) 253,
      (byte) 241
    };
    byte[] numArray10 = new byte[17];
    numArray10[15] = (byte) 1;
    numArray10[7] = (byte) 103;
    numArray10[0] = (byte) 183;
    numArray10[3] = (byte) 16 /*0x10*/;
    numArray10[5] = (byte) 191;
    numArray10[2] = (byte) 28;
    numArray10[1] = (byte) 107;
    numArray10[6] = (byte) 249;
    numArray10[4] = (byte) 15;
    numArray10[9] = (byte) 50;
    numArray10[10] = (byte) 122;
    numArray10[8] = (byte) 180;
    numArray10[12] = (byte) 184;
    numArray10[13] = (byte) 148;
    numArray10[14] = (byte) 200;
    numArray10[11] = (byte) 75;
    numArray10[16 /*0x10*/] = (byte) 219;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 17);
    for (int index = 0; index < 17; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static int ssp_appserver_12681(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[28] = (byte) 29;
    sourceArray1[39] = (byte) 49;
    sourceArray1[43] = (byte) 106;
    sourceArray1[18] = (byte) 1;
    sourceArray1[40] = (byte) 188;
    sourceArray1[20] = (byte) 3;
    sourceArray1[9] = (byte) 21;
    sourceArray1[17] = (byte) 241;
    sourceArray1[8] = (byte) 11;
    sourceArray1[5] = (byte) 109;
    sourceArray1[11] = (byte) 180;
    sourceArray1[26] = (byte) 251;
    sourceArray1[12] = (byte) 17;
    sourceArray1[6] = (byte) 228;
    sourceArray1[30] = (byte) 56;
    sourceArray1[0] = (byte) 241;
    sourceArray1[47] = (byte) 175;
    sourceArray1[4] = (byte) 42;
    sourceArray1[45] = (byte) 234;
    sourceArray1[19] = (byte) 34;
    sourceArray1[14] = (byte) 231;
    sourceArray1[21] = (byte) 152;
    sourceArray1[22] = (byte) 35;
    sourceArray1[10] = (byte) 80 /*0x50*/;
    sourceArray1[24] = (byte) 8;
    sourceArray1[31 /*0x1F*/] = (byte) 86;
    sourceArray1[33] = (byte) 148;
    sourceArray1[27] = (byte) 31 /*0x1F*/;
    sourceArray1[46] = (byte) 113;
    sourceArray1[29] = (byte) 161;
    sourceArray1[16 /*0x10*/] = (byte) 109;
    sourceArray1[3] = (byte) 124;
    sourceArray1[32 /*0x20*/] = (byte) 20;
    sourceArray1[42] = (byte) 216;
    sourceArray1[7] = (byte) 157;
    sourceArray1[35] = (byte) 93;
    sourceArray1[36] = (byte) 27;
    sourceArray1[1] = (byte) 37;
    sourceArray1[38] = (byte) 110;
    sourceArray1[23] = (byte) 114;
    sourceArray1[34] = (byte) 195;
    sourceArray1[41] = (byte) 62;
    sourceArray1[13] = (byte) 65;
    sourceArray1[37] = (byte) 133;
    sourceArray1[44] = (byte) 73;
    sourceArray1[25] = (byte) 59;
    sourceArray1[2] = (byte) 31 /*0x1F*/;
    sourceArray1[15] = (byte) 1;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[18] = (byte) 68;
    sourceArray2[1] = (byte) 242;
    sourceArray2[2] = (byte) 31 /*0x1F*/;
    sourceArray2[3] = (byte) 168;
    sourceArray2[14] = (byte) 69;
    sourceArray2[45] = (byte) 171;
    sourceArray2[23] = (byte) 177;
    sourceArray2[20] = (byte) 10;
    sourceArray2[8] = (byte) 55;
    sourceArray2[32 /*0x20*/] = (byte) 72;
    sourceArray2[7] = (byte) 160 /*0xA0*/;
    sourceArray2[11] = (byte) 250;
    sourceArray2[28] = (byte) 22;
    sourceArray2[13] = (byte) 154;
    sourceArray2[41] = (byte) 230;
    sourceArray2[15] = (byte) 99;
    sourceArray2[16 /*0x10*/] = (byte) 241;
    sourceArray2[17] = (byte) 72;
    sourceArray2[6] = (byte) 105;
    sourceArray2[12] = (byte) 172;
    sourceArray2[46] = (byte) 149;
    sourceArray2[21] = (byte) 158;
    sourceArray2[22] = (byte) 101;
    sourceArray2[31 /*0x1F*/] = (byte) 17;
    sourceArray2[24] = (byte) 44;
    sourceArray2[10] = (byte) 79;
    sourceArray2[26] = (byte) 163;
    sourceArray2[27] = (byte) 223;
    sourceArray2[36] = (byte) 239;
    sourceArray2[9] = (byte) 129;
    sourceArray2[30] = (byte) 21;
    sourceArray2[29] = (byte) 123;
    sourceArray2[19] = (byte) 195;
    sourceArray2[33] = (byte) 121;
    sourceArray2[34] = (byte) 211;
    sourceArray2[4] = (byte) 84;
    sourceArray2[40] = (byte) 102;
    sourceArray2[37] = (byte) 195;
    sourceArray2[38] = (byte) 23;
    sourceArray2[39] = (byte) 108;
    sourceArray2[44] = (byte) 165;
    sourceArray2[0] = (byte) 79;
    sourceArray2[42] = (byte) 172;
    sourceArray2[43] = (byte) 132;
    sourceArray2[25] = (byte) 116;
    sourceArray2[5] = (byte) 71;
    sourceArray2[35] = (byte) 231;
    sourceArray2[47] = (byte) 144 /*0x90*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12682()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[83];
      byte[] numArray2 = new byte[55]
      {
        (byte) 92,
        (byte) 153,
        (byte) 113,
        (byte) 220,
        (byte) 43,
        (byte) 75,
        (byte) 7,
        (byte) 5,
        (byte) 218,
        (byte) 28,
        (byte) 37,
        (byte) 35,
        (byte) 187,
        (byte) 21,
        (byte) 196,
        (byte) 227,
        (byte) 98,
        (byte) 73,
        (byte) 28,
        (byte) 170,
        (byte) 129,
        (byte) 76,
        (byte) 11,
        (byte) 153,
        (byte) 222,
        (byte) 174,
        (byte) 190,
        (byte) 107,
        (byte) 61,
        (byte) 28,
        (byte) 68,
        (byte) 29,
        (byte) 139,
        (byte) 27,
        (byte) 166,
        (byte) 119,
        (byte) 253,
        (byte) 42,
        (byte) 36,
        (byte) 224 /*0xE0*/,
        (byte) 210,
        (byte) 206,
        (byte) 252,
        byte.MaxValue,
        (byte) 197,
        (byte) 224 /*0xE0*/,
        (byte) 216,
        (byte) 109,
        (byte) 24,
        (byte) 36,
        (byte) 152,
        (byte) 134,
        (byte) 245,
        (byte) 65,
        (byte) 57
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 86,
        (byte) 249,
        (byte) 74,
        (byte) 74,
        (byte) 254,
        (byte) 73,
        (byte) 240 /*0xF0*/,
        (byte) 134,
        (byte) 93,
        (byte) 32 /*0x20*/,
        (byte) 92,
        (byte) 192 /*0xC0*/,
        (byte) 242,
        (byte) 137,
        (byte) 14,
        (byte) 186,
        (byte) 222,
        (byte) 6,
        (byte) 189,
        (byte) 43,
        (byte) 68,
        (byte) 175,
        (byte) 121,
        (byte) 108,
        (byte) 71,
        (byte) 37,
        (byte) 229,
        (byte) 48 /*0x30*/,
        (byte) 228,
        (byte) 49,
        (byte) 109,
        (byte) 164,
        (byte) 97,
        (byte) 222,
        (byte) 132,
        (byte) 230,
        (byte) 64 /*0x40*/,
        (byte) 198,
        (byte) 146,
        (byte) 117,
        (byte) 104,
        (byte) 219,
        (byte) 83,
        (byte) 16 /*0x10*/,
        (byte) 31 /*0x1F*/,
        (byte) 245,
        (byte) 31 /*0x1F*/,
        (byte) 157,
        (byte) 238,
        (byte) 25,
        (byte) 36,
        (byte) 228,
        (byte) 64 /*0x40*/,
        (byte) 22,
        (byte) 16 /*0x10*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[28];
      numArray4[10] = (byte) 160 /*0xA0*/;
      numArray4[12] = (byte) 77;
      numArray4[2] = (byte) 124;
      numArray4[3] = (byte) 141;
      numArray4[1] = (byte) 254;
      numArray4[17] = (byte) 216;
      numArray4[13] = (byte) 229;
      numArray4[7] = (byte) 215;
      numArray4[5] = (byte) 200;
      numArray4[9] = (byte) 151;
      numArray4[8] = (byte) 8;
      numArray4[14] = (byte) 56;
      numArray4[4] = (byte) 156;
      numArray4[26] = (byte) 24;
      numArray4[18] = (byte) 179;
      numArray4[15] = (byte) 36;
      numArray4[24] = (byte) 81;
      numArray4[0] = (byte) 215;
      numArray4[27] = (byte) 132;
      numArray4[6] = (byte) 189;
      numArray4[20] = (byte) 12;
      numArray4[21] = (byte) 212;
      numArray4[22] = (byte) 254;
      numArray4[23] = (byte) 213;
      numArray4[16 /*0x10*/] = (byte) 59;
      numArray4[11] = (byte) 110;
      numArray4[25] = (byte) 82;
      numArray4[19] = (byte) 80 /*0x50*/;
      byte[] numArray5 = new byte[28]
      {
        (byte) 11,
        (byte) 204,
        (byte) 115,
        (byte) 106,
        (byte) 107,
        (byte) 5,
        (byte) 154,
        (byte) 210,
        (byte) 38,
        (byte) 155,
        (byte) 85,
        (byte) 105,
        (byte) 61,
        (byte) 136,
        (byte) 150,
        (byte) 222,
        (byte) 227,
        (byte) 6,
        (byte) 10,
        (byte) 130,
        (byte) 224 /*0xE0*/,
        (byte) 87,
        (byte) 132,
        (byte) 136,
        (byte) 193,
        (byte) 15,
        (byte) 95,
        (byte) 115
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 28);
      for (int index = 0; index < 28; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[83];
    byte[] numArray7 = new byte[55]
    {
      (byte) 73,
      (byte) 175,
      (byte) 57,
      (byte) 25,
      (byte) 225,
      (byte) 77,
      (byte) 190,
      (byte) 215,
      (byte) 100,
      (byte) 191,
      (byte) 12,
      (byte) 124,
      (byte) 232,
      (byte) 162,
      (byte) 174,
      (byte) 17,
      (byte) 228,
      (byte) 23,
      (byte) 182,
      (byte) 157,
      (byte) 82,
      (byte) 134,
      (byte) 218,
      (byte) 36,
      (byte) 91,
      (byte) 109,
      (byte) 225,
      (byte) 91,
      (byte) 215,
      (byte) 102,
      (byte) 34,
      (byte) 44,
      (byte) 0,
      (byte) 22,
      (byte) 236,
      (byte) 86,
      (byte) 4,
      (byte) 13,
      (byte) 220,
      (byte) 250,
      (byte) 121,
      (byte) 38,
      (byte) 103,
      (byte) 126,
      (byte) 83,
      (byte) 230,
      (byte) 209,
      (byte) 198,
      (byte) 215,
      (byte) 173,
      (byte) 157,
      (byte) 236,
      (byte) 72,
      (byte) 9,
      (byte) 233
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 211,
      (byte) 109,
      (byte) 248,
      (byte) 77,
      (byte) 5,
      (byte) 47,
      (byte) 207,
      (byte) 212,
      (byte) 226,
      (byte) 172,
      (byte) 238,
      (byte) 156,
      (byte) 31 /*0x1F*/,
      (byte) 246,
      (byte) 65,
      (byte) 227,
      (byte) 15,
      (byte) 219,
      (byte) 2,
      (byte) 84,
      (byte) 213,
      (byte) 13,
      (byte) 124,
      (byte) 114,
      (byte) 250,
      (byte) 35,
      (byte) 35,
      (byte) 57,
      (byte) 114,
      (byte) 219,
      (byte) 138,
      (byte) 248,
      (byte) 222,
      (byte) 177,
      (byte) 75,
      (byte) 183,
      (byte) 118,
      (byte) 180,
      (byte) 176 /*0xB0*/,
      (byte) 252,
      (byte) 197,
      (byte) 131,
      (byte) 105,
      (byte) 242,
      (byte) 135,
      (byte) 120,
      (byte) 232,
      (byte) 63 /*0x3F*/,
      byte.MaxValue,
      (byte) 133,
      (byte) 70,
      (byte) 185,
      (byte) 89,
      (byte) 185,
      (byte) 111
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[28]
    {
      (byte) 21,
      (byte) 190,
      (byte) 115,
      (byte) 108,
      (byte) 99,
      byte.MaxValue,
      (byte) 253,
      (byte) 157,
      (byte) 224 /*0xE0*/,
      (byte) 123,
      (byte) 209,
      (byte) 182,
      (byte) 88,
      (byte) 12,
      (byte) 215,
      (byte) 59,
      (byte) 187,
      (byte) 35,
      (byte) 44,
      (byte) 31 /*0x1F*/,
      (byte) 91,
      (byte) 209,
      (byte) 116,
      (byte) 156,
      (byte) 26,
      (byte) 131,
      (byte) 42,
      (byte) 201
    };
    byte[] numArray10 = new byte[28];
    numArray10[1] = (byte) 26;
    numArray10[0] = (byte) 204;
    numArray10[2] = (byte) 3;
    numArray10[19] = (byte) 33;
    numArray10[13] = (byte) 176 /*0xB0*/;
    numArray10[5] = (byte) 120;
    numArray10[3] = (byte) 155;
    numArray10[26] = (byte) 238;
    numArray10[8] = (byte) 156;
    numArray10[9] = (byte) 21;
    numArray10[17] = (byte) 165;
    numArray10[11] = (byte) 71;
    numArray10[12] = (byte) 173;
    numArray10[4] = (byte) 157;
    numArray10[14] = (byte) 175;
    numArray10[7] = (byte) 164;
    numArray10[27] = (byte) 174;
    numArray10[23] = (byte) 88;
    numArray10[18] = (byte) 71;
    numArray10[15] = (byte) 101;
    numArray10[20] = (byte) 201;
    numArray10[21] = (byte) 58;
    numArray10[22] = (byte) 41;
    numArray10[6] = (byte) 252;
    numArray10[24] = (byte) 171;
    numArray10[25] = (byte) 197;
    numArray10[10] = (byte) 192 /*0xC0*/;
    numArray10[16 /*0x10*/] = (byte) 62;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 28);
    for (int index = 0; index < 28; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
