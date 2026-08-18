// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12305
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12305
{
  private static byte[] sspq = new byte[144 /*0x90*/]
  {
    (byte) 206,
    (byte) 33,
    (byte) 158,
    (byte) 13,
    (byte) 28,
    (byte) 159,
    (byte) 57,
    (byte) 134,
    (byte) 201,
    (byte) 53,
    (byte) 82,
    (byte) 120,
    (byte) 70,
    (byte) 167,
    (byte) 118,
    (byte) 247,
    (byte) 25,
    (byte) 13,
    (byte) 248,
    (byte) 253,
    (byte) 205,
    (byte) 55,
    (byte) 234,
    (byte) 199,
    (byte) 149,
    (byte) 104,
    (byte) 77,
    (byte) 214,
    (byte) 75,
    (byte) 233,
    (byte) 160 /*0xA0*/,
    (byte) 184,
    (byte) 64 /*0x40*/,
    (byte) 76,
    (byte) 250,
    (byte) 196,
    (byte) 150,
    (byte) 52,
    (byte) 224 /*0xE0*/,
    (byte) 182,
    (byte) 163,
    (byte) 85,
    (byte) 77,
    (byte) 175,
    (byte) 128 /*0x80*/,
    (byte) 192 /*0xC0*/,
    (byte) 63 /*0x3F*/,
    (byte) 246,
    (byte) 40,
    (byte) 87,
    (byte) 199,
    (byte) 190,
    (byte) 102,
    (byte) 179,
    (byte) 82,
    (byte) 99,
    (byte) 58,
    (byte) 223,
    (byte) 149,
    (byte) 153,
    (byte) 56,
    (byte) 250,
    (byte) 172,
    (byte) 178,
    (byte) 96 /*0x60*/,
    (byte) 229,
    (byte) 44,
    (byte) 66,
    (byte) 90,
    (byte) 119,
    (byte) 234,
    (byte) 59,
    (byte) 70,
    (byte) 212,
    (byte) 41,
    (byte) 87,
    (byte) 173,
    (byte) 244,
    (byte) 34,
    (byte) 225,
    (byte) 81,
    (byte) 199,
    (byte) 134,
    (byte) 99,
    (byte) 141,
    (byte) 69,
    (byte) 3,
    (byte) 242,
    (byte) 47,
    (byte) 217,
    (byte) 48 /*0x30*/,
    (byte) 87,
    (byte) 165,
    (byte) 237,
    (byte) 127 /*0x7F*/,
    (byte) 72,
    (byte) 124,
    (byte) 99,
    (byte) 26,
    (byte) 200,
    (byte) 59,
    (byte) 145,
    (byte) 146,
    (byte) 113,
    (byte) 17,
    (byte) 92,
    (byte) 118,
    (byte) 145,
    (byte) 134,
    (byte) 203,
    (byte) 187,
    (byte) 192 /*0xC0*/,
    (byte) 52,
    (byte) 18,
    (byte) 12,
    (byte) 228,
    (byte) 166,
    (byte) 135,
    (byte) 190,
    (byte) 231,
    (byte) 165,
    (byte) 70,
    (byte) 135,
    (byte) 184,
    byte.MaxValue,
    (byte) 192 /*0xC0*/,
    (byte) 253,
    (byte) 122,
    (byte) 9,
    (byte) 140,
    (byte) 51,
    (byte) 2,
    (byte) 131,
    (byte) 79,
    (byte) 225,
    (byte) 155,
    (byte) 162,
    (byte) 245,
    (byte) 165,
    (byte) 51,
    (byte) 34,
    (byte) 245,
    (byte) 10,
    (byte) 218
  };
  private static byte[] sspr = new byte[144 /*0x90*/]
  {
    (byte) 189,
    (byte) 0,
    (byte) 88,
    (byte) 101,
    (byte) 61,
    (byte) 96 /*0x60*/,
    (byte) 10,
    (byte) 175,
    (byte) 170,
    (byte) 5,
    (byte) 78,
    (byte) 138,
    (byte) 197,
    (byte) 17,
    (byte) 163,
    (byte) 61,
    (byte) 90,
    (byte) 246,
    (byte) 162,
    (byte) 176 /*0xB0*/,
    (byte) 202,
    (byte) 226,
    (byte) 111,
    (byte) 19,
    (byte) 251,
    (byte) 115,
    (byte) 68,
    (byte) 5,
    (byte) 83,
    (byte) 167,
    (byte) 114,
    (byte) 2,
    (byte) 200,
    (byte) 52,
    (byte) 200,
    (byte) 44,
    (byte) 221,
    (byte) 172,
    (byte) 36,
    (byte) 116,
    (byte) 173,
    (byte) 221,
    (byte) 246,
    (byte) 137,
    (byte) 54,
    (byte) 73,
    (byte) 84,
    (byte) 99,
    (byte) 130,
    (byte) 247,
    (byte) 130,
    (byte) 181,
    (byte) 96 /*0x60*/,
    (byte) 113,
    (byte) 171,
    (byte) 132,
    (byte) 193,
    (byte) 216,
    (byte) 187,
    (byte) 57,
    (byte) 39,
    (byte) 88,
    (byte) 9,
    (byte) 164,
    (byte) 13,
    (byte) 221,
    (byte) 195,
    (byte) 180,
    (byte) 64 /*0x40*/,
    (byte) 195,
    (byte) 251,
    (byte) 78,
    (byte) 46,
    (byte) 242,
    (byte) 157,
    (byte) 163,
    (byte) 131,
    (byte) 69,
    (byte) 73,
    (byte) 184,
    (byte) 147,
    (byte) 37,
    (byte) 14,
    (byte) 123,
    (byte) 40,
    (byte) 245,
    (byte) 26,
    (byte) 213,
    (byte) 132,
    (byte) 196,
    (byte) 210,
    (byte) 13,
    (byte) 91,
    byte.MaxValue,
    (byte) 149,
    (byte) 208 /*0xD0*/,
    (byte) 6,
    (byte) 87,
    (byte) 182,
    (byte) 117,
    (byte) 184,
    (byte) 93,
    (byte) 113,
    (byte) 101,
    (byte) 154,
    (byte) 20,
    (byte) 201,
    (byte) 132,
    (byte) 56,
    (byte) 75,
    (byte) 24,
    (byte) 31 /*0x1F*/,
    (byte) 16 /*0x10*/,
    (byte) 254,
    (byte) 228,
    (byte) 112 /*0x70*/,
    (byte) 52,
    (byte) 23,
    (byte) 37,
    (byte) 53,
    (byte) 159,
    (byte) 56,
    (byte) 54,
    (byte) 50,
    (byte) 205,
    (byte) 75,
    (byte) 222,
    (byte) 161,
    (byte) 191,
    (byte) 43,
    (byte) 112 /*0x70*/,
    (byte) 210,
    (byte) 204,
    (byte) 84,
    (byte) 41,
    (byte) 69,
    (byte) 58,
    (byte) 174,
    (byte) 126,
    (byte) 3,
    (byte) 121,
    (byte) 80 /*0x50*/,
    (byte) 150,
    (byte) 18
  };

  internal static string ssp_appserver_12306()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[39];
      byte[] numArray2 = new byte[39]
      {
        (byte) 17,
        (byte) 125,
        (byte) 223,
        (byte) 253,
        (byte) 209,
        (byte) 72,
        (byte) 207,
        (byte) 6,
        (byte) 233,
        (byte) 225,
        (byte) 226,
        (byte) 65,
        (byte) 113,
        (byte) 241,
        (byte) 73,
        (byte) 92,
        (byte) 194,
        (byte) 244,
        (byte) 85,
        (byte) 210,
        (byte) 11,
        (byte) 15,
        (byte) 96 /*0x60*/,
        (byte) 29,
        (byte) 107,
        (byte) 153,
        (byte) 72,
        (byte) 53,
        (byte) 220,
        (byte) 1,
        (byte) 31 /*0x1F*/,
        (byte) 110,
        (byte) 149,
        (byte) 226,
        (byte) 224 /*0xE0*/,
        (byte) 9,
        (byte) 77,
        (byte) 115,
        (byte) 190
      };
      byte[] numArray3 = new byte[39];
      numArray3[7] = (byte) 117;
      numArray3[6] = (byte) 201;
      numArray3[20] = (byte) 88;
      numArray3[0] = (byte) 86;
      numArray3[4] = (byte) 104;
      numArray3[5] = (byte) 77;
      numArray3[3] = (byte) 62;
      numArray3[8] = (byte) 28;
      numArray3[2] = (byte) 55;
      numArray3[9] = (byte) 122;
      numArray3[26] = (byte) 221;
      numArray3[11] = (byte) 131;
      numArray3[34] = (byte) 50;
      numArray3[13] = (byte) 183;
      numArray3[16 /*0x10*/] = (byte) 121;
      numArray3[15] = (byte) 228;
      numArray3[21] = (byte) 171;
      numArray3[17] = (byte) 225;
      numArray3[23] = (byte) 242;
      numArray3[14] = (byte) 113;
      numArray3[29] = (byte) 211;
      numArray3[33] = (byte) 121;
      numArray3[22] = (byte) 152;
      numArray3[35] = (byte) 195;
      numArray3[24] = (byte) 110;
      numArray3[31 /*0x1F*/] = (byte) 57;
      numArray3[25] = (byte) 73;
      numArray3[27] = (byte) 86;
      numArray3[28] = (byte) 34;
      numArray3[1] = (byte) 83;
      numArray3[19] = (byte) 42;
      numArray3[12] = (byte) 112 /*0x70*/;
      numArray3[32 /*0x20*/] = (byte) 48 /*0x30*/;
      numArray3[30] = (byte) 14;
      numArray3[10] = (byte) 129;
      numArray3[18] = (byte) 80 /*0x50*/;
      numArray3[36] = (byte) 213;
      numArray3[37] = (byte) 246;
      numArray3[38] = (byte) 38;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[39];
    byte[] numArray5 = new byte[39];
    numArray5[18] = (byte) 208 /*0xD0*/;
    numArray5[17] = (byte) 225;
    numArray5[2] = (byte) 142;
    numArray5[38] = (byte) 236;
    numArray5[31 /*0x1F*/] = (byte) 230;
    numArray5[22] = (byte) 242;
    numArray5[6] = (byte) 179;
    numArray5[5] = (byte) 109;
    numArray5[21] = (byte) 181;
    numArray5[9] = (byte) 237;
    numArray5[37] = (byte) 32 /*0x20*/;
    numArray5[11] = (byte) 225;
    numArray5[35] = (byte) 151;
    numArray5[13] = (byte) 113;
    numArray5[15] = (byte) 121;
    numArray5[33] = (byte) 208 /*0xD0*/;
    numArray5[16 /*0x10*/] = (byte) 24;
    numArray5[20] = (byte) 163;
    numArray5[8] = (byte) 221;
    numArray5[19] = (byte) 55;
    numArray5[10] = (byte) 140;
    numArray5[7] = (byte) 185;
    numArray5[28] = (byte) 192 /*0xC0*/;
    numArray5[12] = (byte) 134;
    numArray5[24] = (byte) 234;
    numArray5[36] = (byte) 154;
    numArray5[26] = (byte) 29;
    numArray5[14] = (byte) 64 /*0x40*/;
    numArray5[3] = (byte) 200;
    numArray5[4] = (byte) 213;
    numArray5[30] = (byte) 94;
    numArray5[25] = (byte) 19;
    numArray5[32 /*0x20*/] = (byte) 9;
    numArray5[0] = (byte) 130;
    numArray5[34] = (byte) 125;
    numArray5[29] = (byte) 244;
    numArray5[23] = (byte) 63 /*0x3F*/;
    numArray5[1] = (byte) 3;
    numArray5[27] = (byte) 236;
    byte[] numArray6 = new byte[39]
    {
      (byte) 8,
      (byte) 235,
      (byte) 76,
      (byte) 86,
      (byte) 180,
      (byte) 99,
      (byte) 20,
      (byte) 22,
      (byte) 133,
      (byte) 1,
      (byte) 136,
      (byte) 87,
      (byte) 204,
      (byte) 204,
      (byte) 235,
      (byte) 201,
      (byte) 177,
      (byte) 129,
      (byte) 60,
      (byte) 192 /*0xC0*/,
      (byte) 168,
      (byte) 19,
      (byte) 152,
      (byte) 128 /*0x80*/,
      (byte) 6,
      (byte) 101,
      (byte) 44,
      (byte) 205,
      (byte) 223,
      (byte) 55,
      (byte) 156,
      (byte) 239,
      (byte) 187,
      (byte) 160 /*0xA0*/,
      (byte) 154,
      (byte) 175,
      (byte) 129,
      (byte) 254,
      (byte) 150
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 39);
    for (int index = 0; index < 39; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12307()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[109];
      byte[] numArray2 = new byte[55];
      numArray2[12] = (byte) 181;
      numArray2[20] = (byte) 125;
      numArray2[2] = byte.MaxValue;
      numArray2[52] = (byte) 229;
      numArray2[3] = (byte) 130;
      numArray2[49] = (byte) 56;
      numArray2[23] = (byte) 252;
      numArray2[7] = (byte) 176 /*0xB0*/;
      numArray2[28] = (byte) 75;
      numArray2[33] = (byte) 165;
      numArray2[18] = (byte) 150;
      numArray2[11] = (byte) 41;
      numArray2[39] = (byte) 31 /*0x1F*/;
      numArray2[13] = (byte) 49;
      numArray2[41] = (byte) 68;
      numArray2[29] = (byte) 112 /*0x70*/;
      numArray2[5] = (byte) 184;
      numArray2[17] = (byte) 220;
      numArray2[8] = (byte) 134;
      numArray2[10] = (byte) 66;
      numArray2[45] = (byte) 203;
      numArray2[9] = (byte) 75;
      numArray2[22] = (byte) 16 /*0x10*/;
      numArray2[4] = (byte) 220;
      numArray2[24] = (byte) 230;
      numArray2[54] = (byte) 45;
      numArray2[6] = (byte) 12;
      numArray2[27] = (byte) 245;
      numArray2[31 /*0x1F*/] = (byte) 97;
      numArray2[46] = (byte) 21;
      numArray2[30] = (byte) 146;
      numArray2[19] = (byte) 249;
      numArray2[32 /*0x20*/] = (byte) 94;
      numArray2[51] = (byte) 225;
      numArray2[34] = (byte) 144 /*0x90*/;
      numArray2[35] = (byte) 40;
      numArray2[36] = (byte) 233;
      numArray2[37] = (byte) 12;
      numArray2[38] = (byte) 201;
      numArray2[16 /*0x10*/] = (byte) 194;
      numArray2[40] = (byte) 170;
      numArray2[21] = (byte) 11;
      numArray2[26] = (byte) 18;
      numArray2[43] = (byte) 147;
      numArray2[15] = (byte) 73;
      numArray2[1] = (byte) 209;
      numArray2[42] = (byte) 35;
      numArray2[47] = (byte) 12;
      numArray2[48 /*0x30*/] = (byte) 241;
      numArray2[25] = (byte) 64 /*0x40*/;
      numArray2[50] = (byte) 222;
      numArray2[14] = (byte) 220;
      numArray2[0] = (byte) 54;
      numArray2[53] = (byte) 106;
      numArray2[44] = (byte) 182;
      byte[] numArray3 = new byte[55]
      {
        (byte) 187,
        (byte) 82,
        (byte) 56,
        (byte) 36,
        (byte) 95,
        (byte) 210,
        (byte) 61,
        (byte) 168,
        (byte) 89,
        (byte) 42,
        (byte) 67,
        (byte) 3,
        (byte) 121,
        (byte) 14,
        (byte) 150,
        (byte) 42,
        (byte) 172,
        (byte) 24,
        (byte) 5,
        (byte) 1,
        (byte) 17,
        (byte) 89,
        (byte) 193,
        (byte) 78,
        (byte) 201,
        (byte) 16 /*0x10*/,
        (byte) 94,
        (byte) 244,
        (byte) 43,
        (byte) 52,
        (byte) 154,
        (byte) 222,
        (byte) 189,
        (byte) 19,
        (byte) 123,
        (byte) 211,
        (byte) 45,
        (byte) 191,
        (byte) 216,
        (byte) 204,
        (byte) 120,
        (byte) 10,
        (byte) 131,
        (byte) 243,
        (byte) 31 /*0x1F*/,
        (byte) 127 /*0x7F*/,
        (byte) 18,
        (byte) 130,
        (byte) 37,
        (byte) 213,
        (byte) 44,
        (byte) 193,
        (byte) 254,
        (byte) 89,
        (byte) 248
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      numArray4[12] = (byte) 76;
      numArray4[1] = (byte) 128 /*0x80*/;
      numArray4[46] = (byte) 240 /*0xF0*/;
      numArray4[3] = (byte) 159;
      numArray4[4] = (byte) 18;
      numArray4[5] = (byte) 80 /*0x50*/;
      numArray4[6] = (byte) 132;
      numArray4[15] = (byte) 230;
      numArray4[7] = (byte) 7;
      numArray4[0] = (byte) 188;
      numArray4[22] = (byte) 26;
      numArray4[28] = (byte) 226;
      numArray4[9] = (byte) 215;
      numArray4[26] = (byte) 59;
      numArray4[32 /*0x20*/] = (byte) 35;
      numArray4[53] = (byte) 103;
      numArray4[16 /*0x10*/] = (byte) 254;
      numArray4[21] = (byte) 112 /*0x70*/;
      numArray4[18] = (byte) 89;
      numArray4[19] = (byte) 218;
      numArray4[51] = (byte) 95;
      numArray4[42] = (byte) 112 /*0x70*/;
      numArray4[49] = (byte) 231;
      numArray4[23] = (byte) 37;
      numArray4[44] = (byte) 109;
      numArray4[25] = (byte) 198;
      numArray4[31 /*0x1F*/] = (byte) 130;
      numArray4[27] = (byte) 104;
      numArray4[34] = (byte) 42;
      numArray4[24] = (byte) 155;
      numArray4[30] = (byte) 216;
      numArray4[20] = (byte) 163;
      numArray4[14] = (byte) 69;
      numArray4[17] = (byte) 187;
      numArray4[50] = (byte) 112 /*0x70*/;
      numArray4[33] = (byte) 37;
      numArray4[36] = (byte) 186;
      numArray4[8] = (byte) 100;
      numArray4[2] = (byte) 113;
      numArray4[39] = (byte) 142;
      numArray4[40] = (byte) 136;
      numArray4[41] = (byte) 210;
      numArray4[37] = (byte) 217;
      numArray4[45] = (byte) 47;
      numArray4[11] = (byte) 59;
      numArray4[38] = (byte) 128 /*0x80*/;
      numArray4[10] = (byte) 152;
      numArray4[43] = (byte) 41;
      numArray4[48 /*0x30*/] = (byte) 227;
      numArray4[35] = (byte) 239;
      numArray4[13] = (byte) 121;
      numArray4[29] = (byte) 13;
      numArray4[52] = (byte) 154;
      numArray4[47] = (byte) 94;
      byte[] numArray5 = new byte[54]
      {
        (byte) 42,
        (byte) 15,
        (byte) 97,
        (byte) 236,
        (byte) 70,
        (byte) 13,
        (byte) 203,
        (byte) 19,
        (byte) 127 /*0x7F*/,
        (byte) 143,
        (byte) 95,
        (byte) 208 /*0xD0*/,
        (byte) 140,
        (byte) 158,
        (byte) 183,
        (byte) 223,
        (byte) 172,
        (byte) 187,
        (byte) 61,
        (byte) 173,
        (byte) 234,
        (byte) 52,
        (byte) 223,
        (byte) 240 /*0xF0*/,
        (byte) 34,
        (byte) 176 /*0xB0*/,
        (byte) 190,
        (byte) 60,
        (byte) 74,
        (byte) 128 /*0x80*/,
        (byte) 22,
        (byte) 149,
        (byte) 55,
        (byte) 164,
        (byte) 161,
        (byte) 242,
        (byte) 159,
        (byte) 32 /*0x20*/,
        (byte) 241,
        (byte) 122,
        (byte) 53,
        (byte) 216,
        (byte) 76,
        (byte) 115,
        (byte) 82,
        (byte) 18,
        (byte) 153,
        (byte) 20,
        (byte) 218,
        (byte) 116,
        (byte) 214,
        (byte) 190,
        (byte) 224 /*0xE0*/,
        (byte) 194
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[109];
    byte[] numArray7 = new byte[55];
    numArray7[20] = (byte) 229;
    numArray7[1] = (byte) 148;
    numArray7[2] = (byte) 152;
    numArray7[3] = (byte) 117;
    numArray7[37] = (byte) 191;
    numArray7[47] = (byte) 192 /*0xC0*/;
    numArray7[33] = (byte) 147;
    numArray7[42] = (byte) 164;
    numArray7[53] = (byte) 168;
    numArray7[38] = (byte) 65;
    numArray7[10] = (byte) 91;
    numArray7[11] = (byte) 160 /*0xA0*/;
    numArray7[8] = (byte) 133;
    numArray7[13] = (byte) 204;
    numArray7[15] = (byte) 80 /*0x50*/;
    numArray7[24] = (byte) 12;
    numArray7[27] = (byte) 63 /*0x3F*/;
    numArray7[17] = (byte) 81;
    numArray7[51] = (byte) 126;
    numArray7[22] = (byte) 96 /*0x60*/;
    numArray7[18] = (byte) 230;
    numArray7[52] = (byte) 46;
    numArray7[34] = (byte) 20;
    numArray7[23] = (byte) 84;
    numArray7[49] = (byte) 250;
    numArray7[5] = (byte) 151;
    numArray7[26] = (byte) 247;
    numArray7[36] = (byte) 59;
    numArray7[28] = (byte) 82;
    numArray7[14] = (byte) 189;
    numArray7[54] = (byte) 101;
    numArray7[31 /*0x1F*/] = (byte) 167;
    numArray7[25] = (byte) 205;
    numArray7[6] = (byte) 42;
    numArray7[32 /*0x20*/] = (byte) 185;
    numArray7[35] = (byte) 218;
    numArray7[12] = (byte) 111;
    numArray7[29] = (byte) 83;
    numArray7[9] = (byte) 45;
    numArray7[39] = (byte) 212;
    numArray7[40] = (byte) 130;
    numArray7[41] = (byte) 96 /*0x60*/;
    numArray7[50] = (byte) 30;
    numArray7[43] = (byte) 42;
    numArray7[16 /*0x10*/] = (byte) 6;
    numArray7[45] = (byte) 41;
    numArray7[46] = (byte) 159;
    numArray7[4] = (byte) 56;
    numArray7[48 /*0x30*/] = (byte) 254;
    numArray7[30] = (byte) 24;
    numArray7[44] = (byte) 186;
    numArray7[19] = (byte) 36;
    numArray7[21] = (byte) 198;
    numArray7[0] = (byte) 116;
    numArray7[7] = (byte) 185;
    byte[] numArray8 = new byte[55]
    {
      (byte) 228,
      (byte) 190,
      (byte) 148,
      (byte) 80 /*0x50*/,
      (byte) 166,
      (byte) 176 /*0xB0*/,
      (byte) 45,
      (byte) 141,
      (byte) 116,
      (byte) 69,
      (byte) 224 /*0xE0*/,
      (byte) 9,
      (byte) 137,
      (byte) 33,
      (byte) 154,
      (byte) 238,
      (byte) 54,
      (byte) 139,
      (byte) 231,
      (byte) 162,
      (byte) 108,
      (byte) 186,
      (byte) 114,
      (byte) 55,
      (byte) 143,
      (byte) 43,
      (byte) 233,
      (byte) 29,
      (byte) 188,
      (byte) 15,
      (byte) 112 /*0x70*/,
      (byte) 245,
      (byte) 24,
      (byte) 242,
      (byte) 63 /*0x3F*/,
      (byte) 13,
      (byte) 180,
      (byte) 21,
      (byte) 86,
      (byte) 10,
      (byte) 204,
      (byte) 167,
      (byte) 135,
      (byte) 198,
      (byte) 197,
      (byte) 219,
      (byte) 213,
      (byte) 187,
      (byte) 226,
      (byte) 214,
      (byte) 13,
      (byte) 89,
      (byte) 112 /*0x70*/,
      (byte) 219,
      (byte) 78
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[54]
    {
      (byte) 169,
      (byte) 134,
      (byte) 60,
      (byte) 103,
      (byte) 170,
      (byte) 15,
      (byte) 253,
      (byte) 122,
      (byte) 59,
      (byte) 214,
      (byte) 173,
      (byte) 81,
      (byte) 60,
      (byte) 28,
      (byte) 241,
      (byte) 57,
      (byte) 219,
      (byte) 9,
      (byte) 138,
      (byte) 175,
      (byte) 32 /*0x20*/,
      (byte) 147,
      (byte) 161,
      (byte) 105,
      (byte) 47,
      (byte) 37,
      (byte) 113,
      (byte) 93,
      (byte) 44,
      (byte) 91,
      (byte) 10,
      (byte) 78,
      (byte) 218,
      (byte) 169,
      (byte) 77,
      (byte) 207,
      (byte) 154,
      (byte) 205,
      (byte) 237,
      (byte) 235,
      (byte) 117,
      (byte) 154,
      (byte) 202,
      (byte) 107,
      (byte) 23,
      (byte) 182,
      (byte) 223,
      (byte) 201,
      (byte) 230,
      (byte) 53,
      (byte) 65,
      (byte) 252,
      (byte) 85,
      (byte) 220
    };
    byte[] numArray10 = new byte[54]
    {
      (byte) 14,
      (byte) 238,
      (byte) 232,
      (byte) 37,
      (byte) 95,
      (byte) 123,
      (byte) 25,
      (byte) 132,
      (byte) 212,
      (byte) 98,
      (byte) 124,
      (byte) 247,
      (byte) 48 /*0x30*/,
      (byte) 203,
      (byte) 153,
      (byte) 44,
      (byte) 142,
      (byte) 16 /*0x10*/,
      (byte) 160 /*0xA0*/,
      (byte) 65,
      (byte) 82,
      (byte) 135,
      (byte) 71,
      (byte) 251,
      (byte) 237,
      (byte) 56,
      (byte) 250,
      (byte) 156,
      (byte) 65,
      (byte) 182,
      (byte) 22,
      (byte) 1,
      (byte) 80 /*0x50*/,
      (byte) 150,
      (byte) 73,
      (byte) 38,
      (byte) 206,
      (byte) 213,
      (byte) 67,
      (byte) 225,
      (byte) 16 /*0x10*/,
      (byte) 50,
      (byte) 60,
      (byte) 240 /*0xF0*/,
      (byte) 197,
      (byte) 173,
      (byte) 244,
      (byte) 44,
      (byte) 193,
      (byte) 99,
      (byte) 117,
      (byte) 232,
      (byte) 85,
      (byte) 119
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 54);
    for (int index = 0; index < 54; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12308()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[2];
      byte[] numArray2 = new byte[2]
      {
        (byte) 56,
        (byte) 133
      };
      byte[] numArray3 = new byte[2]
      {
        (byte) 49,
        (byte) 138
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_12305.sspq, 0, (Array) numArray4, 0, 53);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12305.sspr, 0, (Array) numArray4, 0, 53);
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
    byte[] numArray5 = new byte[2];
    byte[] numArray6 = new byte[2]{ (byte) 120, (byte) 197 };
    byte[] numArray7 = new byte[2]{ (byte) 87, (byte) 157 };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 2);
    for (int index = 0; index < 2; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[32 /*0x20*/];
    byte[] response1 = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_12305.sspq, 53, (Array) numArray8, 0, 32 /*0x20*/);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_12305.sspr, 53, (Array) numArray8, 0, 32 /*0x20*/);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12309()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 187,
        (byte) 45,
        (byte) 99,
        (byte) 63 /*0x3F*/
      };
      byte[] numArray3 = new byte[4]
      {
        (byte) 0,
        (byte) 63 /*0x3F*/,
        (byte) 0,
        (byte) 0
      };
      numArray3[0] = (byte) 191;
      numArray3[2] = (byte) 9;
      numArray3[3] = (byte) 140;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[4];
    byte[] numArray5 = new byte[4]
    {
      (byte) 118,
      (byte) 247,
      (byte) 226,
      (byte) 145
    };
    byte[] numArray6 = new byte[4]
    {
      (byte) 60,
      (byte) 234,
      (byte) 16 /*0x10*/,
      (byte) 103
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12310()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 251,
        (byte) 144 /*0x90*/,
        (byte) 163,
        (byte) 138,
        (byte) 251,
        (byte) 182,
        (byte) 69,
        (byte) 188,
        (byte) 179,
        (byte) 72
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 16 /*0x10*/,
        (byte) 181,
        (byte) 232,
        (byte) 160 /*0xA0*/,
        (byte) 250,
        (byte) 231,
        (byte) 153,
        (byte) 225,
        (byte) 179,
        (byte) 123
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
      (byte) 36,
      (byte) 160 /*0xA0*/,
      (byte) 41,
      byte.MaxValue,
      (byte) 222,
      (byte) 27,
      (byte) 91,
      (byte) 124,
      (byte) 33,
      (byte) 95
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 246,
      (byte) 222,
      (byte) 61,
      (byte) 130,
      (byte) 150,
      (byte) 8,
      (byte) 85,
      (byte) 175,
      (byte) 241,
      (byte) 62
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12311()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[0] = (byte) 253;
      numArray2[1] = (byte) 64 /*0x40*/;
      numArray2[6] = (byte) 183;
      numArray2[5] = (byte) 56;
      numArray2[3] = (byte) 45;
      numArray2[2] = (byte) 80 /*0x50*/;
      numArray2[4] = (byte) 168;
      numArray2[7] = (byte) 146;
      numArray2[8] = (byte) 47;
      numArray2[9] = (byte) 23;
      byte[] numArray3 = new byte[10]
      {
        (byte) 212,
        (byte) 39,
        (byte) 250,
        (byte) 36,
        (byte) 34,
        (byte) 7,
        (byte) 151,
        (byte) 88,
        (byte) 47,
        (byte) 69
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[49];
      byte[] response = new byte[49];
      Array.Copy((Array) sc_12305.sspq, 85, (Array) numArray4, 0, 49);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12305.sspr, 85, (Array) numArray4, 0, 49);
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
      (byte) 150,
      (byte) 111,
      (byte) 191,
      (byte) 229,
      (byte) 167,
      (byte) 53,
      (byte) 206,
      (byte) 113,
      (byte) 107,
      (byte) 100
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 80 /*0x50*/,
      (byte) 78,
      (byte) 160 /*0xA0*/,
      (byte) 181,
      (byte) 172,
      (byte) 130,
      (byte) 21,
      (byte) 138,
      (byte) 24,
      (byte) 33
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[10];
    byte[] response1 = new byte[10];
    Array.Copy((Array) sc_12305.sspq, 134, (Array) numArray8, 0, 10);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_12305.sspr, 134, (Array) numArray8, 0, 10);
    for (int index = 0; index < numArray8.Length; ++index)
    {
      if ((int) numArray8[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static int ssp_appserver_12312(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 95,
      (byte) 252,
      (byte) 136,
      (byte) 249,
      (byte) 151,
      (byte) 184,
      (byte) 61,
      (byte) 229,
      (byte) 124,
      (byte) 157,
      (byte) 36,
      (byte) 193,
      (byte) 223,
      (byte) 137,
      (byte) 24,
      (byte) 52,
      (byte) 218,
      (byte) 35,
      (byte) 153,
      (byte) 143,
      (byte) 157,
      (byte) 106,
      (byte) 153,
      (byte) 109,
      (byte) 234,
      (byte) 62,
      (byte) 93,
      (byte) 80 /*0x50*/,
      (byte) 44,
      (byte) 160 /*0xA0*/,
      (byte) 100,
      (byte) 229,
      (byte) 114,
      (byte) 86,
      (byte) 213,
      (byte) 210,
      (byte) 232,
      (byte) 114,
      (byte) 204,
      (byte) 101,
      (byte) 50,
      (byte) 61,
      (byte) 22,
      (byte) 67,
      (byte) 119,
      (byte) 224 /*0xE0*/,
      (byte) 22,
      (byte) 27
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[33] = (byte) 95;
    sourceArray2[44] = (byte) 142;
    sourceArray2[15] = (byte) 182;
    sourceArray2[19] = (byte) 235;
    sourceArray2[11] = (byte) 61;
    sourceArray2[5] = (byte) 87;
    sourceArray2[6] = (byte) 36;
    sourceArray2[12] = (byte) 223;
    sourceArray2[27] = (byte) 143;
    sourceArray2[9] = (byte) 123;
    sourceArray2[10] = (byte) 106;
    sourceArray2[32 /*0x20*/] = (byte) 226;
    sourceArray2[4] = (byte) 250;
    sourceArray2[13] = (byte) 124;
    sourceArray2[14] = (byte) 211;
    sourceArray2[34] = (byte) 253;
    sourceArray2[40] = (byte) 30;
    sourceArray2[17] = (byte) 161;
    sourceArray2[18] = (byte) 157;
    sourceArray2[38] = (byte) 211;
    sourceArray2[30] = (byte) 183;
    sourceArray2[21] = (byte) 154;
    sourceArray2[22] = (byte) 131;
    sourceArray2[8] = (byte) 68;
    sourceArray2[20] = (byte) 247;
    sourceArray2[25] = (byte) 96 /*0x60*/;
    sourceArray2[3] = (byte) 49;
    sourceArray2[29] = (byte) 74;
    sourceArray2[16 /*0x10*/] = (byte) 181;
    sourceArray2[1] = (byte) 102;
    sourceArray2[24] = (byte) 68;
    sourceArray2[39] = (byte) 234;
    sourceArray2[23] = (byte) 45;
    sourceArray2[28] = (byte) 117;
    sourceArray2[47] = (byte) 135;
    sourceArray2[7] = (byte) 243;
    sourceArray2[42] = (byte) 241;
    sourceArray2[37] = (byte) 78;
    sourceArray2[26] = (byte) 224 /*0xE0*/;
    sourceArray2[36] = byte.MaxValue;
    sourceArray2[45] = (byte) 192 /*0xC0*/;
    sourceArray2[41] = (byte) 152;
    sourceArray2[46] = (byte) 98;
    sourceArray2[43] = (byte) 132;
    sourceArray2[0] = (byte) 163;
    sourceArray2[2] = (byte) 225;
    sourceArray2[35] = (byte) 72;
    sourceArray2[31 /*0x1F*/] = (byte) 146;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
