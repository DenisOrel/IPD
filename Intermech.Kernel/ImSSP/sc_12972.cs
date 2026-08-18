// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12972
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12972
{
  private static byte[] sspq = new byte[132]
  {
    (byte) 25,
    (byte) 89,
    (byte) 236,
    (byte) 105,
    (byte) 10,
    (byte) 137,
    (byte) 193,
    (byte) 87,
    (byte) 54,
    (byte) 2,
    (byte) 147,
    (byte) 77,
    (byte) 139,
    (byte) 203,
    (byte) 181,
    (byte) 36,
    (byte) 48 /*0x30*/,
    (byte) 18,
    (byte) 73,
    (byte) 8,
    (byte) 54,
    (byte) 237,
    (byte) 124,
    (byte) 73,
    (byte) 124,
    (byte) 51,
    (byte) 171,
    (byte) 23,
    (byte) 224 /*0xE0*/,
    (byte) 5,
    (byte) 31 /*0x1F*/,
    (byte) 140,
    (byte) 236,
    (byte) 16 /*0x10*/,
    (byte) 46,
    (byte) 148,
    (byte) 155,
    (byte) 162,
    (byte) 50,
    (byte) 214,
    (byte) 240 /*0xF0*/,
    (byte) 55,
    (byte) 105,
    (byte) 131,
    (byte) 178,
    (byte) 221,
    (byte) 235,
    (byte) 34,
    (byte) 229,
    (byte) 9,
    (byte) 172,
    (byte) 220,
    (byte) 12,
    (byte) 97,
    (byte) 160 /*0xA0*/,
    (byte) 56,
    (byte) 219,
    (byte) 205,
    (byte) 224 /*0xE0*/,
    (byte) 211,
    (byte) 233,
    (byte) 26,
    (byte) 243,
    (byte) 33,
    (byte) 237,
    (byte) 222,
    (byte) 127 /*0x7F*/,
    (byte) 176 /*0xB0*/,
    (byte) 51,
    (byte) 89,
    (byte) 81,
    (byte) 241,
    (byte) 171,
    (byte) 109,
    (byte) 153,
    (byte) 54,
    (byte) 116,
    (byte) 87,
    (byte) 63 /*0x3F*/,
    (byte) 234,
    (byte) 132,
    (byte) 239,
    (byte) 3,
    (byte) 195,
    (byte) 237,
    (byte) 246,
    (byte) 124,
    (byte) 144 /*0x90*/,
    (byte) 228,
    (byte) 253,
    (byte) 16 /*0x10*/,
    (byte) 110,
    (byte) 42,
    (byte) 190,
    (byte) 253,
    (byte) 99,
    (byte) 83,
    (byte) 42,
    (byte) 63 /*0x3F*/,
    (byte) 246,
    (byte) 12,
    (byte) 188,
    (byte) 171,
    (byte) 139,
    (byte) 90,
    (byte) 240 /*0xF0*/,
    (byte) 183,
    (byte) 204,
    (byte) 39,
    (byte) 207,
    (byte) 143,
    (byte) 155,
    (byte) 12,
    (byte) 55,
    (byte) 80 /*0x50*/,
    (byte) 4,
    (byte) 23,
    (byte) 97,
    (byte) 96 /*0x60*/,
    (byte) 61,
    (byte) 50,
    (byte) 214,
    (byte) 253,
    byte.MaxValue,
    (byte) 177,
    (byte) 54,
    (byte) 252,
    (byte) 90,
    (byte) 91,
    (byte) 134,
    (byte) 135,
    (byte) 21
  };
  private static byte[] sspr = new byte[132]
  {
    (byte) 164,
    (byte) 158,
    (byte) 171,
    (byte) 11,
    (byte) 14,
    (byte) 142,
    (byte) 20,
    (byte) 48 /*0x30*/,
    (byte) 44,
    (byte) 89,
    (byte) 227,
    (byte) 17,
    (byte) 118,
    (byte) 196,
    (byte) 127 /*0x7F*/,
    (byte) 5,
    (byte) 110,
    (byte) 195,
    (byte) 209,
    (byte) 203,
    (byte) 197,
    (byte) 227,
    (byte) 101,
    (byte) 44,
    (byte) 162,
    (byte) 242,
    (byte) 38,
    (byte) 81,
    (byte) 149,
    (byte) 234,
    (byte) 15,
    (byte) 163,
    (byte) 39,
    (byte) 177,
    (byte) 173,
    (byte) 53,
    (byte) 149,
    (byte) 191,
    (byte) 25,
    (byte) 128 /*0x80*/,
    (byte) 33,
    (byte) 11,
    (byte) 73,
    (byte) 210,
    (byte) 42,
    (byte) 138,
    (byte) 105,
    (byte) 159,
    (byte) 127 /*0x7F*/,
    (byte) 106,
    (byte) 133,
    (byte) 143,
    (byte) 109,
    (byte) 207,
    (byte) 44,
    (byte) 15,
    (byte) 206,
    (byte) 193,
    (byte) 133,
    (byte) 185,
    (byte) 18,
    (byte) 220,
    (byte) 88,
    (byte) 178,
    (byte) 55,
    (byte) 222,
    (byte) 230,
    (byte) 97,
    (byte) 132,
    (byte) 16 /*0x10*/,
    (byte) 156,
    (byte) 208 /*0xD0*/,
    (byte) 77,
    (byte) 123,
    (byte) 84,
    (byte) 2,
    (byte) 252,
    (byte) 46,
    (byte) 98,
    (byte) 172,
    (byte) 83,
    (byte) 131,
    (byte) 182,
    (byte) 49,
    (byte) 244,
    (byte) 144 /*0x90*/,
    (byte) 85,
    (byte) 168,
    (byte) 214,
    (byte) 77,
    (byte) 157,
    (byte) 54,
    (byte) 77,
    (byte) 13,
    (byte) 181,
    (byte) 114,
    (byte) 96 /*0x60*/,
    (byte) 66,
    (byte) 70,
    (byte) 131,
    (byte) 240 /*0xF0*/,
    (byte) 192 /*0xC0*/,
    (byte) 254,
    (byte) 46,
    (byte) 89,
    (byte) 163,
    (byte) 76,
    (byte) 144 /*0x90*/,
    (byte) 205,
    (byte) 49,
    byte.MaxValue,
    (byte) 96 /*0x60*/,
    (byte) 109,
    (byte) 27,
    (byte) 150,
    (byte) 109,
    (byte) 229,
    (byte) 246,
    (byte) 218,
    (byte) 43,
    (byte) 19,
    (byte) 81,
    (byte) 24,
    (byte) 79,
    (byte) 20,
    (byte) 182,
    (byte) 253,
    (byte) 244,
    (byte) 84,
    (byte) 131,
    (byte) 237,
    (byte) 197
  };

  internal static string ssp_appserver_12973()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[53];
      byte[] numArray2 = new byte[53];
      numArray2[25] = (byte) 62;
      numArray2[1] = (byte) 165;
      numArray2[2] = (byte) 35;
      numArray2[14] = (byte) 219;
      numArray2[8] = (byte) 176 /*0xB0*/;
      numArray2[33] = (byte) 127 /*0x7F*/;
      numArray2[6] = (byte) 77;
      numArray2[7] = (byte) 72;
      numArray2[13] = (byte) 175;
      numArray2[9] = (byte) 71;
      numArray2[46] = (byte) 248;
      numArray2[18] = (byte) 135;
      numArray2[12] = (byte) 74;
      numArray2[10] = (byte) 50;
      numArray2[32 /*0x20*/] = (byte) 93;
      numArray2[39] = (byte) 126;
      numArray2[41] = (byte) 2;
      numArray2[17] = (byte) 60;
      numArray2[49] = (byte) 170;
      numArray2[19] = (byte) 171;
      numArray2[20] = (byte) 65;
      numArray2[21] = (byte) 100;
      numArray2[29] = (byte) 143;
      numArray2[0] = (byte) 227;
      numArray2[3] = (byte) 77;
      numArray2[15] = (byte) 246;
      numArray2[47] = (byte) 117;
      numArray2[27] = (byte) 197;
      numArray2[28] = (byte) 4;
      numArray2[37] = (byte) 77;
      numArray2[30] = (byte) 61;
      numArray2[31 /*0x1F*/] = (byte) 91;
      numArray2[22] = (byte) 69;
      numArray2[11] = (byte) 54;
      numArray2[23] = (byte) 46;
      numArray2[44] = (byte) 158;
      numArray2[35] = (byte) 153;
      numArray2[26] = (byte) 154;
      numArray2[24] = (byte) 5;
      numArray2[43] = (byte) 46;
      numArray2[40] = (byte) 155;
      numArray2[16 /*0x10*/] = (byte) 49;
      numArray2[4] = (byte) 250;
      numArray2[42] = (byte) 168;
      numArray2[38] = (byte) 82;
      numArray2[45] = (byte) 152;
      numArray2[36] = (byte) 150;
      numArray2[34] = (byte) 245;
      numArray2[48 /*0x30*/] = (byte) 87;
      numArray2[5] = (byte) 79;
      numArray2[50] = (byte) 109;
      numArray2[51] = (byte) 238;
      numArray2[52] = (byte) 49;
      byte[] numArray3 = new byte[53];
      numArray3[29] = (byte) 94;
      numArray3[40] = (byte) 1;
      numArray3[41] = (byte) 5;
      numArray3[33] = (byte) 3;
      numArray3[4] = (byte) 102;
      numArray3[5] = (byte) 32 /*0x20*/;
      numArray3[3] = (byte) 73;
      numArray3[15] = (byte) 32 /*0x20*/;
      numArray3[8] = (byte) 116;
      numArray3[0] = (byte) 184;
      numArray3[10] = (byte) 52;
      numArray3[11] = (byte) 210;
      numArray3[43] = (byte) 212;
      numArray3[9] = (byte) 236;
      numArray3[30] = (byte) 207;
      numArray3[22] = (byte) 158;
      numArray3[48 /*0x30*/] = (byte) 226;
      numArray3[14] = (byte) 81;
      numArray3[18] = (byte) 212;
      numArray3[35] = (byte) 136;
      numArray3[19] = (byte) 185;
      numArray3[21] = (byte) 82;
      numArray3[2] = (byte) 109;
      numArray3[7] = (byte) 208 /*0xD0*/;
      numArray3[16 /*0x10*/] = (byte) 134;
      numArray3[25] = (byte) 22;
      numArray3[52] = (byte) 199;
      numArray3[13] = (byte) 229;
      numArray3[20] = (byte) 59;
      numArray3[42] = (byte) 170;
      numArray3[6] = (byte) 204;
      numArray3[31 /*0x1F*/] = (byte) 203;
      numArray3[32 /*0x20*/] = (byte) 242;
      numArray3[17] = (byte) 141;
      numArray3[28] = (byte) 49;
      numArray3[38] = (byte) 119;
      numArray3[1] = (byte) 111;
      numArray3[37] = (byte) 103;
      numArray3[24] = (byte) 206;
      numArray3[39] = (byte) 161;
      numArray3[47] = (byte) 142;
      numArray3[12] = (byte) 34;
      numArray3[36] = (byte) 58;
      numArray3[23] = (byte) 156;
      numArray3[44] = (byte) 144 /*0x90*/;
      numArray3[45] = (byte) 85;
      numArray3[27] = (byte) 94;
      numArray3[34] = (byte) 217;
      numArray3[46] = (byte) 113;
      numArray3[49] = (byte) 238;
      numArray3[50] = (byte) 81;
      numArray3[51] = (byte) 170;
      numArray3[26] = (byte) 93;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 53);
      for (int index = 0; index < 53; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[53];
    byte[] numArray5 = new byte[53];
    numArray5[31 /*0x1F*/] = (byte) 132;
    numArray5[21] = (byte) 4;
    numArray5[13] = (byte) 170;
    numArray5[44] = (byte) 253;
    numArray5[7] = (byte) 93;
    numArray5[4] = (byte) 84;
    numArray5[6] = (byte) 46;
    numArray5[22] = (byte) 4;
    numArray5[34] = (byte) 23;
    numArray5[42] = (byte) 21;
    numArray5[14] = (byte) 44;
    numArray5[10] = (byte) 216;
    numArray5[1] = (byte) 82;
    numArray5[29] = (byte) 243;
    numArray5[43] = (byte) 52;
    numArray5[15] = (byte) 237;
    numArray5[41] = (byte) 194;
    numArray5[17] = (byte) 22;
    numArray5[11] = (byte) 101;
    numArray5[18] = (byte) 129;
    numArray5[8] = (byte) 36;
    numArray5[40] = (byte) 251;
    numArray5[38] = (byte) 157;
    numArray5[26] = (byte) 172;
    numArray5[49] = (byte) 211;
    numArray5[25] = (byte) 138;
    numArray5[2] = (byte) 97;
    numArray5[27] = (byte) 198;
    numArray5[39] = (byte) 160 /*0xA0*/;
    numArray5[24] = (byte) 169;
    numArray5[30] = (byte) 74;
    numArray5[9] = (byte) 195;
    numArray5[32 /*0x20*/] = (byte) 237;
    numArray5[16 /*0x10*/] = (byte) 144 /*0x90*/;
    numArray5[33] = (byte) 152;
    numArray5[35] = (byte) 92;
    numArray5[36] = (byte) 29;
    numArray5[37] = (byte) 37;
    numArray5[20] = (byte) 50;
    numArray5[5] = (byte) 156;
    numArray5[3] = (byte) 142;
    numArray5[23] = (byte) 169;
    numArray5[48 /*0x30*/] = (byte) 185;
    numArray5[12] = (byte) 113;
    numArray5[28] = (byte) 87;
    numArray5[45] = (byte) 37;
    numArray5[46] = (byte) 247;
    numArray5[47] = (byte) 112 /*0x70*/;
    numArray5[0] = (byte) 233;
    numArray5[19] = (byte) 179;
    numArray5[50] = (byte) 230;
    numArray5[51] = (byte) 246;
    numArray5[52] = (byte) 10;
    byte[] numArray6 = new byte[53];
    numArray6[29] = (byte) 137;
    numArray6[22] = (byte) 222;
    numArray6[15] = (byte) 186;
    numArray6[26] = (byte) 236;
    numArray6[4] = (byte) 122;
    numArray6[7] = (byte) 155;
    numArray6[9] = (byte) 66;
    numArray6[47] = (byte) 207;
    numArray6[8] = (byte) 173;
    numArray6[52] = (byte) 49;
    numArray6[42] = (byte) 186;
    numArray6[20] = (byte) 143;
    numArray6[12] = (byte) 241;
    numArray6[13] = (byte) 191;
    numArray6[14] = (byte) 175;
    numArray6[46] = (byte) 128 /*0x80*/;
    numArray6[3] = (byte) 215;
    numArray6[17] = (byte) 105;
    numArray6[36] = (byte) 180;
    numArray6[19] = (byte) 164;
    numArray6[2] = (byte) 233;
    numArray6[21] = (byte) 6;
    numArray6[41] = (byte) 232;
    numArray6[1] = (byte) 185;
    numArray6[24] = (byte) 160 /*0xA0*/;
    numArray6[25] = (byte) 171;
    numArray6[23] = (byte) 153;
    numArray6[27] = (byte) 195;
    numArray6[28] = (byte) 68;
    numArray6[16 /*0x10*/] = (byte) 94;
    numArray6[30] = (byte) 21;
    numArray6[6] = (byte) 97;
    numArray6[32 /*0x20*/] = (byte) 120;
    numArray6[10] = (byte) 219;
    numArray6[11] = (byte) 239;
    numArray6[33] = (byte) 56;
    numArray6[39] = (byte) 190;
    numArray6[37] = (byte) 148;
    numArray6[38] = (byte) 137;
    numArray6[31 /*0x1F*/] = (byte) 217;
    numArray6[5] = (byte) 204;
    numArray6[40] = (byte) 168;
    numArray6[34] = (byte) 230;
    numArray6[43] = (byte) 253;
    numArray6[44] = (byte) 189;
    numArray6[45] = (byte) 77;
    numArray6[0] = (byte) 151;
    numArray6[18] = (byte) 245;
    numArray6[48 /*0x30*/] = (byte) 244;
    numArray6[49] = (byte) 162;
    numArray6[50] = (byte) 218;
    numArray6[51] = (byte) 107;
    numArray6[35] = (byte) 133;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 53);
    for (int index = 0; index < 53; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[29];
    byte[] response = new byte[29];
    Array.Copy((Array) sc_12972.sspq, 0, (Array) numArray7, 0, 29);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12972.sspr, 0, (Array) numArray7, 0, 29);
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

  internal static string ssp_appserver_12974()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14]
      {
        (byte) 233,
        (byte) 62,
        byte.MaxValue,
        (byte) 87,
        (byte) 157,
        (byte) 177,
        (byte) 80 /*0x50*/,
        (byte) 40,
        (byte) 78,
        (byte) 86,
        (byte) 220,
        (byte) 163,
        (byte) 1,
        (byte) 53
      };
      byte[] numArray3 = new byte[14]
      {
        (byte) 55,
        (byte) 141,
        (byte) 182,
        (byte) 150,
        (byte) 15,
        (byte) 4,
        (byte) 167,
        (byte) 70,
        (byte) 157,
        (byte) 216,
        (byte) 8,
        (byte) 71,
        (byte) 232,
        (byte) 196
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 95,
      (byte) 168,
      (byte) 89,
      (byte) 247,
      (byte) 132,
      (byte) 9,
      (byte) 207,
      (byte) 193,
      (byte) 43,
      (byte) 220,
      (byte) 162,
      (byte) 236,
      (byte) 214,
      (byte) 222
    };
    byte[] numArray6 = new byte[14]
    {
      (byte) 192 /*0xC0*/,
      (byte) 69,
      (byte) 42,
      (byte) 169,
      (byte) 237,
      (byte) 61,
      (byte) 113,
      (byte) 2,
      (byte) 123,
      (byte) 214,
      (byte) 205,
      (byte) 13,
      (byte) 121,
      (byte) 219
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12975()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[187];
      byte[] numArray2 = new byte[55]
      {
        (byte) 85,
        (byte) 78,
        (byte) 52,
        (byte) 46,
        (byte) 3,
        (byte) 212,
        (byte) 7,
        (byte) 195,
        (byte) 243,
        (byte) 43,
        (byte) 147,
        (byte) 235,
        (byte) 71,
        (byte) 28,
        (byte) 50,
        (byte) 240 /*0xF0*/,
        (byte) 76,
        (byte) 111,
        (byte) 135,
        (byte) 4,
        (byte) 100,
        (byte) 53,
        (byte) 47,
        (byte) 149,
        (byte) 113,
        (byte) 213,
        (byte) 173,
        (byte) 59,
        (byte) 120,
        (byte) 120,
        (byte) 50,
        (byte) 43,
        (byte) 199,
        (byte) 27,
        (byte) 216,
        (byte) 251,
        (byte) 14,
        (byte) 49,
        (byte) 164,
        (byte) 133,
        (byte) 240 /*0xF0*/,
        (byte) 192 /*0xC0*/,
        (byte) 21,
        (byte) 163,
        (byte) 46,
        (byte) 64 /*0x40*/,
        (byte) 148,
        (byte) 134,
        (byte) 185,
        (byte) 229,
        (byte) 145,
        (byte) 58,
        (byte) 240 /*0xF0*/,
        (byte) 53,
        (byte) 180
      };
      byte[] numArray3 = new byte[55];
      numArray3[19] = (byte) 49;
      numArray3[49] = (byte) 231;
      numArray3[2] = (byte) 66;
      numArray3[3] = (byte) 111;
      numArray3[30] = (byte) 107;
      numArray3[5] = (byte) 187;
      numArray3[39] = (byte) 212;
      numArray3[4] = (byte) 28;
      numArray3[41] = (byte) 104;
      numArray3[9] = (byte) 98;
      numArray3[10] = (byte) 218;
      numArray3[29] = (byte) 247;
      numArray3[12] = (byte) 4;
      numArray3[13] = (byte) 90;
      numArray3[8] = (byte) 121;
      numArray3[26] = (byte) 134;
      numArray3[38] = (byte) 229;
      numArray3[6] = (byte) 186;
      numArray3[17] = (byte) 236;
      numArray3[1] = (byte) 248;
      numArray3[15] = (byte) 210;
      numArray3[21] = (byte) 248;
      numArray3[22] = (byte) 235;
      numArray3[20] = (byte) 41;
      numArray3[27] = (byte) 79;
      numArray3[11] = (byte) 74;
      numArray3[7] = (byte) 33;
      numArray3[16 /*0x10*/] = (byte) 15;
      numArray3[28] = (byte) 122;
      numArray3[37] = (byte) 101;
      numArray3[23] = (byte) 252;
      numArray3[31 /*0x1F*/] = (byte) 121;
      numArray3[32 /*0x20*/] = (byte) 32 /*0x20*/;
      numArray3[33] = (byte) 49;
      numArray3[34] = (byte) 161;
      numArray3[14] = (byte) 44;
      numArray3[36] = (byte) 67;
      numArray3[47] = (byte) 90;
      numArray3[40] = (byte) 231;
      numArray3[42] = (byte) 235;
      numArray3[24] = (byte) 251;
      numArray3[51] = (byte) 106;
      numArray3[25] = (byte) 228;
      numArray3[43] = (byte) 91;
      numArray3[44] = (byte) 150;
      numArray3[45] = (byte) 147;
      numArray3[46] = (byte) 203;
      numArray3[35] = (byte) 143;
      numArray3[48 /*0x30*/] = (byte) 6;
      numArray3[50] = (byte) 133;
      numArray3[54] = (byte) 44;
      numArray3[0] = (byte) 185;
      numArray3[52] = (byte) 217;
      numArray3[53] = (byte) 136;
      numArray3[18] = (byte) 55;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 56,
        (byte) 139,
        (byte) 237,
        byte.MaxValue,
        (byte) 73,
        (byte) 185,
        (byte) 148,
        (byte) 1,
        (byte) 43,
        (byte) 147,
        (byte) 196,
        (byte) 140,
        (byte) 89,
        (byte) 113,
        (byte) 166,
        (byte) 83,
        (byte) 163,
        (byte) 240 /*0xF0*/,
        (byte) 47,
        (byte) 132,
        (byte) 225,
        (byte) 159,
        (byte) 22,
        (byte) 215,
        (byte) 229,
        (byte) 254,
        (byte) 142,
        (byte) 111,
        (byte) 135,
        (byte) 77,
        (byte) 111,
        (byte) 87,
        (byte) 182,
        (byte) 73,
        (byte) 55,
        (byte) 233,
        (byte) 136,
        (byte) 95,
        (byte) 218,
        (byte) 187,
        (byte) 5,
        (byte) 169,
        (byte) 155,
        (byte) 135,
        (byte) 164,
        (byte) 181,
        (byte) 41,
        (byte) 146,
        (byte) 207,
        (byte) 28,
        (byte) 75,
        (byte) 49,
        (byte) 224 /*0xE0*/,
        (byte) 124,
        (byte) 178
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 216,
        (byte) 205,
        (byte) 135,
        (byte) 174,
        (byte) 162,
        (byte) 148,
        (byte) 75,
        (byte) 90,
        (byte) 56,
        (byte) 159,
        (byte) 13,
        (byte) 142,
        (byte) 65,
        (byte) 189,
        (byte) 22,
        (byte) 148,
        (byte) 157,
        (byte) 209,
        (byte) 209,
        (byte) 80 /*0x50*/,
        (byte) 216,
        (byte) 136,
        (byte) 134,
        (byte) 62,
        (byte) 211,
        (byte) 70,
        (byte) 173,
        (byte) 87,
        (byte) 38,
        (byte) 229,
        (byte) 212,
        (byte) 107,
        (byte) 167,
        (byte) 12,
        (byte) 176 /*0xB0*/,
        (byte) 213,
        (byte) 205,
        (byte) 234,
        (byte) 23,
        (byte) 145,
        (byte) 52,
        (byte) 169,
        (byte) 80 /*0x50*/,
        (byte) 78,
        (byte) 53,
        (byte) 192 /*0xC0*/,
        (byte) 143,
        (byte) 97,
        (byte) 93,
        (byte) 210,
        (byte) 16 /*0x10*/,
        (byte) 87,
        (byte) 251,
        (byte) 16 /*0x10*/,
        (byte) 118
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 22,
        (byte) 26,
        (byte) 74,
        (byte) 43,
        (byte) 37,
        (byte) 121,
        (byte) 222,
        (byte) 35,
        (byte) 109,
        (byte) 213,
        (byte) 16 /*0x10*/,
        (byte) 58,
        (byte) 189,
        (byte) 202,
        (byte) 67,
        (byte) 21,
        (byte) 116,
        (byte) 126,
        (byte) 55,
        (byte) 3,
        (byte) 142,
        (byte) 50,
        (byte) 47,
        (byte) 213,
        (byte) 251,
        (byte) 160 /*0xA0*/,
        (byte) 107,
        (byte) 2,
        (byte) 62,
        (byte) 118,
        (byte) 58,
        (byte) 151,
        (byte) 158,
        (byte) 116,
        (byte) 114,
        (byte) 69,
        (byte) 143,
        (byte) 144 /*0x90*/,
        (byte) 224 /*0xE0*/,
        (byte) 47,
        (byte) 189,
        (byte) 99,
        (byte) 55,
        (byte) 224 /*0xE0*/,
        (byte) 24,
        (byte) 87,
        (byte) 18,
        (byte) 82,
        (byte) 7,
        (byte) 27,
        (byte) 108,
        (byte) 254,
        (byte) 122,
        (byte) 150,
        (byte) 231
      };
      byte[] numArray7 = new byte[55];
      numArray7[25] = (byte) 56;
      numArray7[11] = (byte) 39;
      numArray7[31 /*0x1F*/] = (byte) 114;
      numArray7[9] = (byte) 147;
      numArray7[4] = (byte) 73;
      numArray7[5] = (byte) 40;
      numArray7[17] = (byte) 13;
      numArray7[30] = (byte) 162;
      numArray7[3] = (byte) 204;
      numArray7[52] = (byte) 178;
      numArray7[10] = (byte) 37;
      numArray7[13] = (byte) 4;
      numArray7[12] = (byte) 179;
      numArray7[7] = (byte) 7;
      numArray7[34] = (byte) 221;
      numArray7[15] = (byte) 154;
      numArray7[33] = (byte) 23;
      numArray7[14] = (byte) 97;
      numArray7[47] = (byte) 124;
      numArray7[19] = (byte) 7;
      numArray7[45] = (byte) 233;
      numArray7[21] = (byte) 229;
      numArray7[51] = (byte) 242;
      numArray7[24] = (byte) 36;
      numArray7[48 /*0x30*/] = (byte) 118;
      numArray7[0] = (byte) 254;
      numArray7[26] = (byte) 107;
      numArray7[27] = (byte) 58;
      numArray7[28] = (byte) 132;
      numArray7[29] = (byte) 209;
      numArray7[42] = (byte) 20;
      numArray7[16 /*0x10*/] = (byte) 212;
      numArray7[32 /*0x20*/] = (byte) 167;
      numArray7[49] = (byte) 124;
      numArray7[2] = (byte) 163;
      numArray7[35] = (byte) 41;
      numArray7[46] = (byte) 229;
      numArray7[37] = (byte) 22;
      numArray7[38] = (byte) 45;
      numArray7[18] = (byte) 136;
      numArray7[40] = (byte) 49;
      numArray7[41] = (byte) 4;
      numArray7[23] = (byte) 37;
      numArray7[43] = (byte) 230;
      numArray7[44] = (byte) 237;
      numArray7[1] = (byte) 131;
      numArray7[22] = (byte) 253;
      numArray7[39] = (byte) 205;
      numArray7[50] = (byte) 51;
      numArray7[8] = (byte) 224 /*0xE0*/;
      numArray7[6] = (byte) 140;
      numArray7[20] = (byte) 5;
      numArray7[36] = (byte) 152;
      numArray7[53] = (byte) 215;
      numArray7[54] = (byte) 172;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[22]
      {
        (byte) 184,
        (byte) 138,
        (byte) 247,
        (byte) 245,
        (byte) 225,
        (byte) 195,
        (byte) 235,
        (byte) 94,
        (byte) 92,
        (byte) 148,
        (byte) 188,
        (byte) 200,
        (byte) 163,
        (byte) 107,
        (byte) 130,
        (byte) 65,
        (byte) 229,
        (byte) 100,
        (byte) 171,
        (byte) 186,
        (byte) 8,
        (byte) 28
      };
      byte[] numArray9 = new byte[22];
      numArray9[20] = (byte) 121;
      numArray9[1] = (byte) 66;
      numArray9[17] = (byte) 73;
      numArray9[0] = (byte) 194;
      numArray9[4] = (byte) 55;
      numArray9[5] = (byte) 168;
      numArray9[13] = (byte) 146;
      numArray9[14] = (byte) 149;
      numArray9[8] = (byte) 139;
      numArray9[21] = (byte) 234;
      numArray9[3] = (byte) 128 /*0x80*/;
      numArray9[11] = (byte) 212;
      numArray9[12] = (byte) 210;
      numArray9[2] = (byte) 200;
      numArray9[7] = (byte) 37;
      numArray9[9] = (byte) 208 /*0xD0*/;
      numArray9[16 /*0x10*/] = (byte) 229;
      numArray9[6] = (byte) 171;
      numArray9[18] = (byte) 93;
      numArray9[19] = (byte) 46;
      numArray9[15] = (byte) 108;
      numArray9[10] = (byte) 202;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_12972.sspq, 29, (Array) numArray10, 0, 23);
      key.Query(true, 335, numArray10, response);
      Array.Copy((Array) sc_12972.sspr, 29, (Array) numArray10, 0, 23);
      for (int index = 0; index < numArray10.Length; ++index)
      {
        if ((int) numArray10[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray11 = new byte[187];
    byte[] numArray12 = new byte[55]
    {
      (byte) 185,
      (byte) 100,
      (byte) 241,
      (byte) 100,
      (byte) 33,
      (byte) 217,
      (byte) 193,
      (byte) 184,
      (byte) 236,
      (byte) 139,
      (byte) 98,
      (byte) 124,
      (byte) 187,
      (byte) 28,
      (byte) 202,
      (byte) 191,
      (byte) 120,
      (byte) 42,
      (byte) 197,
      (byte) 51,
      (byte) 50,
      (byte) 85,
      (byte) 14,
      (byte) 131,
      (byte) 235,
      (byte) 99,
      (byte) 144 /*0x90*/,
      (byte) 73,
      (byte) 32 /*0x20*/,
      (byte) 15,
      (byte) 128 /*0x80*/,
      (byte) 32 /*0x20*/,
      (byte) 89,
      (byte) 66,
      (byte) 211,
      (byte) 135,
      (byte) 31 /*0x1F*/,
      (byte) 201,
      (byte) 114,
      (byte) 194,
      (byte) 225,
      (byte) 121,
      (byte) 233,
      (byte) 101,
      (byte) 212,
      (byte) 236,
      (byte) 199,
      (byte) 149,
      (byte) 18,
      (byte) 44,
      (byte) 169,
      (byte) 187,
      (byte) 67,
      (byte) 120,
      (byte) 169
    };
    byte[] numArray13 = new byte[55]
    {
      (byte) 35,
      (byte) 196,
      (byte) 176 /*0xB0*/,
      (byte) 121,
      (byte) 205,
      (byte) 13,
      (byte) 253,
      (byte) 158,
      (byte) 82,
      (byte) 156,
      (byte) 53,
      (byte) 19,
      (byte) 172,
      (byte) 190,
      (byte) 78,
      byte.MaxValue,
      (byte) 154,
      (byte) 14,
      (byte) 173,
      (byte) 113,
      (byte) 159,
      (byte) 118,
      (byte) 111,
      (byte) 232,
      (byte) 230,
      (byte) 40,
      (byte) 90,
      (byte) 71,
      (byte) 164,
      (byte) 231,
      (byte) 192 /*0xC0*/,
      (byte) 241,
      (byte) 132,
      (byte) 42,
      (byte) 41,
      (byte) 17,
      (byte) 134,
      (byte) 104,
      (byte) 219,
      (byte) 13,
      (byte) 26,
      (byte) 79,
      (byte) 154,
      (byte) 143,
      (byte) 188,
      (byte) 181,
      (byte) 94,
      (byte) 70,
      (byte) 252,
      (byte) 14,
      (byte) 167,
      (byte) 186,
      (byte) 249,
      (byte) 192 /*0xC0*/,
      (byte) 200
    };
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray11, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index] ^= numArray13[index];
    byte[] numArray14 = new byte[55]
    {
      (byte) 109,
      (byte) 121,
      (byte) 189,
      (byte) 72,
      (byte) 119,
      (byte) 151,
      (byte) 221,
      (byte) 10,
      (byte) 65,
      (byte) 35,
      (byte) 120,
      (byte) 73,
      (byte) 67,
      (byte) 159,
      (byte) 175,
      (byte) 183,
      (byte) 3,
      (byte) 82,
      (byte) 184,
      (byte) 108,
      (byte) 168,
      (byte) 174,
      (byte) 254,
      (byte) 155,
      (byte) 251,
      (byte) 55,
      (byte) 244,
      (byte) 102,
      (byte) 197,
      (byte) 220,
      (byte) 224 /*0xE0*/,
      (byte) 153,
      (byte) 112 /*0x70*/,
      (byte) 49,
      (byte) 177,
      (byte) 80 /*0x50*/,
      (byte) 181,
      (byte) 122,
      (byte) 105,
      (byte) 226,
      (byte) 12,
      (byte) 91,
      (byte) 147,
      (byte) 201,
      (byte) 79,
      (byte) 76,
      (byte) 207,
      (byte) 140,
      (byte) 250,
      (byte) 3,
      (byte) 64 /*0x40*/,
      (byte) 38,
      (byte) 240 /*0xF0*/,
      (byte) 247,
      (byte) 173
    };
    byte[] numArray15 = new byte[55]
    {
      (byte) 219,
      (byte) 44,
      (byte) 215,
      (byte) 228,
      (byte) 153,
      (byte) 15,
      (byte) 190,
      (byte) 17,
      (byte) 82,
      (byte) 135,
      (byte) 11,
      (byte) 198,
      (byte) 166,
      (byte) 173,
      (byte) 62,
      (byte) 7,
      (byte) 50,
      (byte) 91,
      (byte) 97,
      (byte) 173,
      (byte) 144 /*0x90*/,
      (byte) 105,
      (byte) 212,
      (byte) 158,
      (byte) 243,
      (byte) 179,
      (byte) 135,
      (byte) 118,
      (byte) 16 /*0x10*/,
      (byte) 105,
      (byte) 137,
      (byte) 146,
      (byte) 169,
      (byte) 146,
      (byte) 204,
      (byte) 57,
      (byte) 40,
      (byte) 114,
      (byte) 37,
      (byte) 197,
      (byte) 162,
      (byte) 173,
      (byte) 141,
      (byte) 123,
      (byte) 122,
      (byte) 117,
      (byte) 124,
      (byte) 81,
      (byte) 168,
      (byte) 91,
      (byte) 26,
      (byte) 226,
      (byte) 158,
      (byte) 158,
      (byte) 151
    };
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray11, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 55] ^= numArray15[index];
    byte[] numArray16 = new byte[55]
    {
      (byte) 33,
      (byte) 77,
      (byte) 64 /*0x40*/,
      (byte) 162,
      (byte) 119,
      (byte) 68,
      (byte) 100,
      (byte) 67,
      (byte) 223,
      (byte) 216,
      (byte) 18,
      (byte) 238,
      (byte) 94,
      (byte) 2,
      (byte) 137,
      (byte) 232,
      (byte) 214,
      (byte) 95,
      (byte) 77,
      (byte) 27,
      (byte) 53,
      (byte) 76,
      (byte) 174,
      (byte) 103,
      (byte) 104,
      (byte) 107,
      (byte) 193,
      (byte) 156,
      (byte) 186,
      (byte) 168,
      (byte) 121,
      (byte) 131,
      (byte) 242,
      (byte) 214,
      (byte) 147,
      (byte) 44,
      (byte) 41,
      (byte) 63 /*0x3F*/,
      (byte) 149,
      (byte) 22,
      (byte) 46,
      (byte) 134,
      (byte) 111,
      (byte) 115,
      (byte) 189,
      (byte) 158,
      (byte) 155,
      (byte) 1,
      (byte) 183,
      (byte) 61,
      (byte) 26,
      (byte) 151,
      (byte) 96 /*0x60*/,
      (byte) 164,
      (byte) 68
    };
    byte[] numArray17 = new byte[55]
    {
      (byte) 104,
      (byte) 34,
      (byte) 216,
      (byte) 127 /*0x7F*/,
      (byte) 186,
      (byte) 43,
      (byte) 111,
      (byte) 231,
      (byte) 98,
      (byte) 199,
      (byte) 3,
      (byte) 113,
      (byte) 125,
      (byte) 199,
      (byte) 133,
      (byte) 93,
      (byte) 101,
      (byte) 154,
      (byte) 50,
      (byte) 181,
      (byte) 172,
      (byte) 17,
      (byte) 79,
      (byte) 153,
      (byte) 80 /*0x50*/,
      (byte) 38,
      (byte) 26,
      (byte) 142,
      (byte) 214,
      (byte) 171,
      (byte) 78,
      (byte) 191,
      (byte) 71,
      (byte) 230,
      (byte) 190,
      (byte) 199,
      (byte) 220,
      (byte) 120,
      (byte) 248,
      (byte) 181,
      (byte) 6,
      (byte) 21,
      (byte) 189,
      (byte) 130,
      (byte) 247,
      (byte) 26,
      (byte) 77,
      (byte) 16 /*0x10*/,
      (byte) 191,
      (byte) 11,
      (byte) 165,
      (byte) 155,
      (byte) 121,
      (byte) 163,
      (byte) 102
    };
    key.Query(true, 335, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray11, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 110] ^= numArray17[index];
    byte[] numArray18 = new byte[22];
    numArray18[4] = (byte) 200;
    numArray18[13] = (byte) 191;
    numArray18[18] = (byte) 175;
    numArray18[3] = (byte) 131;
    numArray18[20] = (byte) 59;
    numArray18[14] = (byte) 150;
    numArray18[6] = (byte) 55;
    numArray18[17] = (byte) 236;
    numArray18[8] = (byte) 143;
    numArray18[9] = (byte) 251;
    numArray18[10] = (byte) 136;
    numArray18[11] = (byte) 111;
    numArray18[7] = (byte) 95;
    numArray18[5] = (byte) 137;
    numArray18[1] = (byte) 58;
    numArray18[15] = (byte) 201;
    numArray18[16 /*0x10*/] = (byte) 54;
    numArray18[12] = (byte) 92;
    numArray18[0] = (byte) 216;
    numArray18[19] = (byte) 221;
    numArray18[2] = (byte) 11;
    numArray18[21] = (byte) 170;
    byte[] numArray19 = new byte[22];
    numArray19[14] = (byte) 161;
    numArray19[5] = (byte) 251;
    numArray19[4] = (byte) 115;
    numArray19[3] = (byte) 88;
    numArray19[7] = (byte) 121;
    numArray19[6] = (byte) 128 /*0x80*/;
    numArray19[15] = (byte) 155;
    numArray19[19] = (byte) 174;
    numArray19[16 /*0x10*/] = (byte) 213;
    numArray19[9] = (byte) 110;
    numArray19[10] = (byte) 241;
    numArray19[0] = (byte) 39;
    numArray19[11] = (byte) 124;
    numArray19[13] = (byte) 194;
    numArray19[8] = (byte) 57;
    numArray19[2] = (byte) 131;
    numArray19[12] = (byte) 111;
    numArray19[17] = (byte) 11;
    numArray19[18] = (byte) 88;
    numArray19[1] = (byte) 1;
    numArray19[20] = (byte) 101;
    numArray19[21] = (byte) 209;
    key.Query(true, 335, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray11, 165, 22);
    for (int index = 0; index < 22; ++index)
      numArray11[index + 165] ^= numArray19[index];
    return Encoding.UTF8.GetString(numArray11);
  }

  internal static string ssp_appserver_12976()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[88];
      byte[] numArray2 = new byte[55];
      numArray2[15] = (byte) 151;
      numArray2[1] = (byte) 163;
      numArray2[2] = (byte) 135;
      numArray2[3] = (byte) 178;
      numArray2[4] = (byte) 164;
      numArray2[38] = (byte) 83;
      numArray2[6] = (byte) 124;
      numArray2[7] = (byte) 39;
      numArray2[46] = (byte) 158;
      numArray2[9] = (byte) 45;
      numArray2[10] = (byte) 138;
      numArray2[11] = (byte) 254;
      numArray2[12] = (byte) 10;
      numArray2[20] = (byte) 147;
      numArray2[14] = (byte) 247;
      numArray2[43] = (byte) 150;
      numArray2[16 /*0x10*/] = (byte) 253;
      numArray2[22] = (byte) 207;
      numArray2[13] = (byte) 19;
      numArray2[23] = (byte) 129;
      numArray2[37] = (byte) 225;
      numArray2[19] = (byte) 35;
      numArray2[24] = (byte) 240 /*0xF0*/;
      numArray2[17] = (byte) 197;
      numArray2[21] = (byte) 78;
      numArray2[25] = (byte) 143;
      numArray2[26] = (byte) 149;
      numArray2[0] = (byte) 63 /*0x3F*/;
      numArray2[8] = (byte) 189;
      numArray2[36] = (byte) 48 /*0x30*/;
      numArray2[30] = (byte) 225;
      numArray2[31 /*0x1F*/] = (byte) 140;
      numArray2[32 /*0x20*/] = (byte) 15;
      numArray2[45] = (byte) 13;
      numArray2[34] = (byte) 141;
      numArray2[35] = (byte) 135;
      numArray2[41] = (byte) 73;
      numArray2[54] = (byte) 254;
      numArray2[18] = (byte) 43;
      numArray2[27] = (byte) 193;
      numArray2[40] = (byte) 177;
      numArray2[5] = (byte) 39;
      numArray2[29] = (byte) 28;
      numArray2[39] = (byte) 59;
      numArray2[44] = (byte) 89;
      numArray2[42] = (byte) 126;
      numArray2[33] = (byte) 241;
      numArray2[47] = (byte) 106;
      numArray2[48 /*0x30*/] = (byte) 117;
      numArray2[49] = (byte) 22;
      numArray2[50] = (byte) 249;
      numArray2[51] = (byte) 235;
      numArray2[52] = (byte) 163;
      numArray2[53] = (byte) 74;
      numArray2[28] = (byte) 244;
      byte[] numArray3 = new byte[55]
      {
        (byte) 134,
        (byte) 77,
        (byte) 186,
        (byte) 75,
        (byte) 136,
        (byte) 155,
        (byte) 5,
        (byte) 241,
        (byte) 253,
        (byte) 135,
        (byte) 6,
        (byte) 165,
        (byte) 101,
        (byte) 225,
        (byte) 216,
        (byte) 22,
        (byte) 152,
        (byte) 58,
        (byte) 192 /*0xC0*/,
        (byte) 154,
        (byte) 36,
        (byte) 48 /*0x30*/,
        (byte) 158,
        (byte) 57,
        (byte) 94,
        (byte) 159,
        (byte) 195,
        (byte) 214,
        (byte) 128 /*0x80*/,
        (byte) 27,
        (byte) 83,
        (byte) 254,
        (byte) 218,
        (byte) 100,
        (byte) 27,
        (byte) 240 /*0xF0*/,
        (byte) 27,
        (byte) 202,
        (byte) 22,
        (byte) 144 /*0x90*/,
        (byte) 198,
        (byte) 55,
        (byte) 111,
        (byte) 103,
        (byte) 43,
        (byte) 210,
        (byte) 190,
        (byte) 93,
        (byte) 118,
        (byte) 77,
        (byte) 179,
        (byte) 126,
        (byte) 153,
        (byte) 34,
        (byte) 245
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[33]
      {
        (byte) 220,
        (byte) 224 /*0xE0*/,
        (byte) 35,
        (byte) 102,
        (byte) 111,
        (byte) 84,
        (byte) 126,
        (byte) 85,
        (byte) 20,
        (byte) 41,
        (byte) 37,
        (byte) 232,
        (byte) 188,
        (byte) 60,
        (byte) 251,
        (byte) 50,
        (byte) 69,
        (byte) 124,
        (byte) 52,
        (byte) 243,
        (byte) 239,
        (byte) 205,
        (byte) 195,
        (byte) 108,
        (byte) 71,
        (byte) 71,
        (byte) 46,
        (byte) 68,
        (byte) 123,
        (byte) 53,
        (byte) 237,
        (byte) 63 /*0x3F*/,
        (byte) 124
      };
      byte[] numArray5 = new byte[33]
      {
        (byte) 144 /*0x90*/,
        (byte) 82,
        (byte) 153,
        (byte) 85,
        (byte) 225,
        (byte) 198,
        (byte) 156,
        (byte) 119,
        (byte) 91,
        (byte) 72,
        (byte) 243,
        (byte) 90,
        (byte) 197,
        (byte) 1,
        (byte) 234,
        (byte) 159,
        (byte) 22,
        (byte) 3,
        (byte) 122,
        (byte) 139,
        (byte) 196,
        (byte) 81,
        (byte) 171,
        (byte) 254,
        (byte) 134,
        (byte) 136,
        (byte) 33,
        (byte) 229,
        (byte) 185,
        (byte) 151,
        (byte) 209,
        (byte) 142,
        (byte) 186
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[88];
    byte[] numArray7 = new byte[55]
    {
      (byte) 27,
      (byte) 67,
      (byte) 134,
      (byte) 193,
      (byte) 95,
      (byte) 244,
      (byte) 71,
      (byte) 246,
      (byte) 95,
      (byte) 251,
      (byte) 71,
      (byte) 115,
      (byte) 226,
      (byte) 71,
      (byte) 110,
      (byte) 91,
      (byte) 126,
      (byte) 45,
      (byte) 170,
      (byte) 15,
      (byte) 29,
      (byte) 183,
      (byte) 31 /*0x1F*/,
      (byte) 39,
      (byte) 38,
      (byte) 57,
      (byte) 218,
      (byte) 222,
      (byte) 60,
      (byte) 229,
      (byte) 108,
      (byte) 162,
      (byte) 238,
      (byte) 188,
      (byte) 23,
      (byte) 1,
      (byte) 246,
      (byte) 115,
      (byte) 147,
      (byte) 251,
      (byte) 134,
      (byte) 205,
      (byte) 12,
      (byte) 75,
      (byte) 0,
      (byte) 187,
      (byte) 97,
      (byte) 52,
      (byte) 199,
      (byte) 145,
      (byte) 192 /*0xC0*/,
      (byte) 79,
      (byte) 112 /*0x70*/,
      (byte) 62,
      (byte) 34
    };
    byte[] numArray8 = new byte[55];
    numArray8[30] = (byte) 135;
    numArray8[33] = (byte) 2;
    numArray8[47] = (byte) 92;
    numArray8[25] = (byte) 253;
    numArray8[18] = (byte) 197;
    numArray8[41] = (byte) 141;
    numArray8[42] = (byte) 176 /*0xB0*/;
    numArray8[7] = (byte) 43;
    numArray8[2] = (byte) 6;
    numArray8[43] = (byte) 155;
    numArray8[8] = (byte) 160 /*0xA0*/;
    numArray8[4] = (byte) 188;
    numArray8[12] = (byte) 223;
    numArray8[14] = (byte) 199;
    numArray8[34] = (byte) 13;
    numArray8[3] = (byte) 19;
    numArray8[9] = (byte) 130;
    numArray8[19] = (byte) 108;
    numArray8[0] = (byte) 251;
    numArray8[17] = (byte) 35;
    numArray8[20] = (byte) 173;
    numArray8[21] = (byte) 109;
    numArray8[22] = (byte) 209;
    numArray8[23] = (byte) 107;
    numArray8[24] = (byte) 238;
    numArray8[53] = (byte) 248;
    numArray8[50] = (byte) 15;
    numArray8[15] = (byte) 243;
    numArray8[28] = (byte) 178;
    numArray8[27] = (byte) 254;
    numArray8[5] = (byte) 79;
    numArray8[31 /*0x1F*/] = (byte) 211;
    numArray8[32 /*0x20*/] = (byte) 198;
    numArray8[10] = (byte) 196;
    numArray8[6] = (byte) 7;
    numArray8[39] = (byte) 254;
    numArray8[36] = (byte) 52;
    numArray8[40] = (byte) 119;
    numArray8[38] = (byte) 131;
    numArray8[16 /*0x10*/] = (byte) 21;
    numArray8[35] = (byte) 23;
    numArray8[13] = (byte) 123;
    numArray8[11] = (byte) 121;
    numArray8[1] = (byte) 16 /*0x10*/;
    numArray8[44] = (byte) 42;
    numArray8[37] = (byte) 17;
    numArray8[46] = (byte) 191;
    numArray8[45] = (byte) 138;
    numArray8[48 /*0x30*/] = (byte) 216;
    numArray8[49] = (byte) 55;
    numArray8[29] = (byte) 174;
    numArray8[51] = (byte) 203;
    numArray8[52] = (byte) 77;
    numArray8[26] = (byte) 164;
    numArray8[54] = (byte) 154;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[33]
    {
      (byte) 56,
      (byte) 48 /*0x30*/,
      (byte) 78,
      (byte) 116,
      (byte) 81,
      (byte) 15,
      (byte) 85,
      (byte) 114,
      (byte) 113,
      (byte) 213,
      (byte) 138,
      (byte) 125,
      (byte) 144 /*0x90*/,
      (byte) 206,
      (byte) 51,
      (byte) 44,
      (byte) 206,
      (byte) 108,
      (byte) 60,
      (byte) 24,
      (byte) 23,
      (byte) 202,
      (byte) 52,
      (byte) 155,
      (byte) 161,
      (byte) 188,
      (byte) 127 /*0x7F*/,
      (byte) 111,
      (byte) 103,
      (byte) 113,
      (byte) 249,
      (byte) 200,
      (byte) 249
    };
    byte[] numArray10 = new byte[33];
    numArray10[18] = (byte) 157;
    numArray10[14] = (byte) 38;
    numArray10[32 /*0x20*/] = (byte) 235;
    numArray10[17] = (byte) 95;
    numArray10[30] = (byte) 141;
    numArray10[13] = (byte) 42;
    numArray10[6] = (byte) 100;
    numArray10[1] = (byte) 94;
    numArray10[8] = (byte) 246;
    numArray10[15] = (byte) 102;
    numArray10[10] = (byte) 242;
    numArray10[22] = (byte) 121;
    numArray10[4] = (byte) 248;
    numArray10[26] = (byte) 136;
    numArray10[3] = (byte) 231;
    numArray10[5] = (byte) 212;
    numArray10[16 /*0x10*/] = (byte) 212;
    numArray10[11] = (byte) 93;
    numArray10[21] = (byte) 177;
    numArray10[19] = (byte) 29;
    numArray10[20] = (byte) 180;
    numArray10[2] = (byte) 22;
    numArray10[28] = (byte) 247;
    numArray10[23] = (byte) 169;
    numArray10[7] = (byte) 220;
    numArray10[25] = (byte) 25;
    numArray10[9] = (byte) 150;
    numArray10[27] = (byte) 235;
    numArray10[0] = (byte) 89;
    numArray10[12] = (byte) 244;
    numArray10[29] = (byte) 63 /*0x3F*/;
    numArray10[24] = (byte) 81;
    numArray10[31 /*0x1F*/] = (byte) 140;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 33);
    for (int index = 0; index < 33; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[36];
    byte[] response = new byte[36];
    Array.Copy((Array) sc_12972.sspq, 52, (Array) numArray11, 0, 36);
    key.Query(true, 335, numArray11, response);
    Array.Copy((Array) sc_12972.sspr, 52, (Array) numArray11, 0, 36);
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

  internal static string ssp_appserver_12977()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 40;
      numArray2[1] = (byte) 196;
      numArray2[2] = (byte) 108;
      numArray2[6] = (byte) 185;
      numArray2[9] = (byte) 247;
      numArray2[7] = (byte) 161;
      numArray2[3] = (byte) 140;
      numArray2[5] = (byte) 200;
      numArray2[8] = (byte) 110;
      numArray2[0] = (byte) 92;
      byte[] numArray3 = new byte[10];
      numArray3[8] = (byte) 46;
      numArray3[0] = (byte) 144 /*0x90*/;
      numArray3[7] = (byte) 46;
      numArray3[3] = (byte) 189;
      numArray3[2] = (byte) 172;
      numArray3[5] = (byte) 28;
      numArray3[4] = (byte) 211;
      numArray3[1] = (byte) 118;
      numArray3[6] = (byte) 17;
      numArray3[9] = (byte) 42;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 7,
      (byte) 215,
      (byte) 149,
      (byte) 228,
      (byte) 100,
      (byte) 130,
      (byte) 164,
      (byte) 224 /*0xE0*/,
      (byte) 148,
      (byte) 6
    };
    byte[] numArray6 = new byte[10];
    numArray6[9] = (byte) 93;
    numArray6[6] = (byte) 151;
    numArray6[8] = (byte) 117;
    numArray6[1] = (byte) 108;
    numArray6[4] = (byte) 246;
    numArray6[5] = (byte) 145;
    numArray6[0] = (byte) 97;
    numArray6[7] = (byte) 27;
    numArray6[2] = (byte) 63 /*0x3F*/;
    numArray6[3] = (byte) 11;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12978()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[154];
      byte[] numArray2 = new byte[55]
      {
        (byte) 223,
        (byte) 150,
        (byte) 218,
        (byte) 205,
        (byte) 162,
        (byte) 114,
        (byte) 57,
        (byte) 0,
        (byte) 201,
        (byte) 146,
        (byte) 36,
        (byte) 182,
        (byte) 148,
        (byte) 207,
        (byte) 26,
        (byte) 93,
        (byte) 251,
        (byte) 232,
        (byte) 212,
        (byte) 190,
        (byte) 37,
        (byte) 121,
        (byte) 140,
        (byte) 245,
        (byte) 153,
        (byte) 59,
        (byte) 31 /*0x1F*/,
        (byte) 198,
        (byte) 225,
        (byte) 132,
        (byte) 38,
        (byte) 200,
        (byte) 64 /*0x40*/,
        (byte) 85,
        (byte) 5,
        (byte) 205,
        (byte) 168,
        (byte) 214,
        (byte) 61,
        (byte) 34,
        (byte) 204,
        (byte) 95,
        (byte) 73,
        (byte) 215,
        (byte) 53,
        (byte) 169,
        (byte) 69,
        (byte) 63 /*0x3F*/,
        (byte) 84,
        (byte) 184,
        (byte) 226,
        (byte) 223,
        (byte) 105,
        (byte) 216,
        (byte) 26
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 93,
        (byte) 82,
        (byte) 251,
        (byte) 83,
        (byte) 144 /*0x90*/,
        (byte) 88,
        (byte) 195,
        (byte) 133,
        (byte) 53,
        (byte) 238,
        (byte) 111,
        (byte) 168,
        (byte) 52,
        (byte) 78,
        (byte) 2,
        (byte) 28,
        (byte) 205,
        (byte) 125,
        (byte) 55,
        (byte) 106,
        (byte) 35,
        (byte) 12,
        (byte) 13,
        (byte) 249,
        (byte) 148,
        (byte) 233,
        (byte) 7,
        (byte) 26,
        (byte) 97,
        (byte) 105,
        (byte) 174,
        (byte) 94,
        (byte) 203,
        (byte) 200,
        (byte) 252,
        (byte) 88,
        (byte) 87,
        (byte) 146,
        (byte) 39,
        (byte) 132,
        (byte) 212,
        (byte) 12,
        (byte) 229,
        (byte) 73,
        (byte) 98,
        (byte) 68,
        (byte) 48 /*0x30*/,
        (byte) 49,
        (byte) 5,
        (byte) 217,
        (byte) 207,
        (byte) 70,
        (byte) 96 /*0x60*/,
        (byte) 123,
        (byte) 149
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 229,
        (byte) 52,
        (byte) 102,
        (byte) 82,
        (byte) 115,
        (byte) 30,
        (byte) 69,
        (byte) 107,
        (byte) 91,
        (byte) 118,
        (byte) 62,
        (byte) 202,
        (byte) 76,
        (byte) 181,
        (byte) 36,
        (byte) 171,
        (byte) 178,
        (byte) 242,
        (byte) 198,
        (byte) 47,
        (byte) 181,
        (byte) 191,
        (byte) 239,
        (byte) 245,
        (byte) 224 /*0xE0*/,
        (byte) 113,
        (byte) 40,
        (byte) 162,
        (byte) 70,
        (byte) 94,
        (byte) 103,
        (byte) 59,
        (byte) 159,
        (byte) 195,
        (byte) 194,
        (byte) 162,
        (byte) 214,
        (byte) 208 /*0xD0*/,
        (byte) 89,
        (byte) 64 /*0x40*/,
        (byte) 4,
        (byte) 107,
        (byte) 217,
        (byte) 51,
        (byte) 7,
        (byte) 117,
        (byte) 7,
        (byte) 3,
        (byte) 189,
        (byte) 52,
        (byte) 183,
        (byte) 230,
        (byte) 84,
        (byte) 53,
        (byte) 85
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 19,
        (byte) 226,
        (byte) 83,
        (byte) 139,
        (byte) 143,
        (byte) 219,
        (byte) 114,
        (byte) 48 /*0x30*/,
        (byte) 96 /*0x60*/,
        (byte) 35,
        (byte) 0,
        (byte) 180,
        (byte) 189,
        (byte) 112 /*0x70*/,
        (byte) 143,
        (byte) 191,
        (byte) 249,
        (byte) 228,
        (byte) 73,
        (byte) 143,
        (byte) 7,
        (byte) 173,
        (byte) 85,
        (byte) 128 /*0x80*/,
        (byte) 31 /*0x1F*/,
        (byte) 162,
        (byte) 187,
        (byte) 64 /*0x40*/,
        (byte) 253,
        (byte) 176 /*0xB0*/,
        (byte) 97,
        (byte) 245,
        (byte) 145,
        (byte) 86,
        (byte) 55,
        (byte) 250,
        (byte) 247,
        (byte) 19,
        (byte) 215,
        (byte) 136,
        (byte) 104,
        (byte) 131,
        (byte) 85,
        (byte) 28,
        (byte) 204,
        (byte) 37,
        (byte) 105,
        (byte) 63 /*0x3F*/,
        (byte) 233,
        (byte) 151,
        (byte) 84,
        (byte) 83,
        (byte) 218,
        (byte) 34,
        (byte) 249
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[44]
      {
        (byte) 112 /*0x70*/,
        (byte) 114,
        (byte) 1,
        (byte) 246,
        (byte) 163,
        (byte) 37,
        (byte) 253,
        (byte) 15,
        (byte) 3,
        (byte) 42,
        (byte) 137,
        (byte) 242,
        (byte) 130,
        (byte) 212,
        (byte) 225,
        (byte) 152,
        (byte) 134,
        (byte) 43,
        (byte) 73,
        (byte) 230,
        (byte) 188,
        (byte) 238,
        (byte) 107,
        (byte) 106,
        (byte) 37,
        (byte) 228,
        (byte) 18,
        (byte) 166,
        (byte) 128 /*0x80*/,
        (byte) 230,
        (byte) 105,
        (byte) 109,
        (byte) 157,
        (byte) 94,
        (byte) 101,
        (byte) 94,
        (byte) 88,
        (byte) 73,
        (byte) 96 /*0x60*/,
        (byte) 70,
        (byte) 220,
        (byte) 35,
        (byte) 3,
        (byte) 74
      };
      byte[] numArray7 = new byte[44]
      {
        (byte) 143,
        (byte) 131,
        (byte) 117,
        (byte) 35,
        (byte) 210,
        (byte) 158,
        (byte) 55,
        (byte) 57,
        (byte) 161,
        (byte) 97,
        (byte) 56,
        (byte) 37,
        (byte) 238,
        (byte) 20,
        (byte) 62,
        (byte) 221,
        (byte) 63 /*0x3F*/,
        (byte) 71,
        (byte) 88,
        (byte) 190,
        (byte) 87,
        (byte) 108,
        (byte) 219,
        (byte) 94,
        (byte) 56,
        (byte) 119,
        (byte) 195,
        (byte) 118,
        (byte) 252,
        (byte) 168,
        (byte) 37,
        (byte) 61,
        (byte) 242,
        (byte) 106,
        (byte) 90,
        (byte) 24,
        (byte) 173,
        (byte) 163,
        (byte) 218,
        (byte) 160 /*0xA0*/,
        (byte) 142,
        (byte) 164,
        (byte) 120,
        (byte) 81
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
      (byte) 95,
      (byte) 213,
      (byte) 78,
      (byte) 206,
      (byte) 10,
      (byte) 11,
      (byte) 62,
      (byte) 135,
      (byte) 130,
      (byte) 7,
      (byte) 151,
      (byte) 209,
      (byte) 191,
      (byte) 18,
      (byte) 86,
      (byte) 143,
      (byte) 29,
      (byte) 193,
      (byte) 27,
      (byte) 85,
      (byte) 35,
      (byte) 111,
      (byte) 131,
      (byte) 64 /*0x40*/,
      (byte) 60,
      (byte) 202,
      (byte) 3,
      (byte) 9,
      (byte) 153,
      (byte) 235,
      (byte) 32 /*0x20*/,
      (byte) 236,
      (byte) 19,
      (byte) 168,
      (byte) 179,
      (byte) 208 /*0xD0*/,
      (byte) 50,
      (byte) 24,
      (byte) 95,
      (byte) 98,
      (byte) 94,
      (byte) 215,
      (byte) 227,
      (byte) 87,
      (byte) 193,
      (byte) 120,
      (byte) 161,
      (byte) 52,
      (byte) 134,
      (byte) 148,
      (byte) 230,
      (byte) 137,
      (byte) 98,
      (byte) 188,
      (byte) 23
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 213,
      (byte) 32 /*0x20*/,
      (byte) 52,
      (byte) 62,
      (byte) 137,
      (byte) 113,
      (byte) 169,
      (byte) 95,
      (byte) 54,
      (byte) 144 /*0x90*/,
      (byte) 154,
      (byte) 140,
      (byte) 224 /*0xE0*/,
      (byte) 214,
      (byte) 205,
      (byte) 218,
      (byte) 248,
      (byte) 112 /*0x70*/,
      (byte) 189,
      (byte) 178,
      (byte) 5,
      (byte) 47,
      (byte) 203,
      (byte) 209,
      (byte) 138,
      (byte) 154,
      (byte) 49,
      (byte) 151,
      (byte) 143,
      (byte) 1,
      (byte) 26,
      (byte) 116,
      (byte) 160 /*0xA0*/,
      (byte) 92,
      (byte) 179,
      (byte) 176 /*0xB0*/,
      (byte) 164,
      (byte) 0,
      (byte) 85,
      (byte) 30,
      (byte) 167,
      (byte) 32 /*0x20*/,
      (byte) 201,
      (byte) 156,
      (byte) 97,
      (byte) 143,
      (byte) 73,
      (byte) 201,
      (byte) 134,
      (byte) 219,
      (byte) 162,
      (byte) 82,
      (byte) 1,
      (byte) 8,
      (byte) 81
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 13,
      (byte) 145,
      (byte) 64 /*0x40*/,
      (byte) 8,
      (byte) 52,
      (byte) 237,
      (byte) 150,
      (byte) 230,
      (byte) 170,
      (byte) 189,
      (byte) 22,
      (byte) 95,
      (byte) 133,
      (byte) 92,
      (byte) 4,
      (byte) 97,
      (byte) 185,
      (byte) 246,
      (byte) 76,
      (byte) 139,
      (byte) 44,
      (byte) 127 /*0x7F*/,
      (byte) 195,
      (byte) 40,
      (byte) 207,
      (byte) 131,
      (byte) 4,
      (byte) 5,
      (byte) 151,
      (byte) 137,
      (byte) 87,
      (byte) 144 /*0x90*/,
      (byte) 195,
      (byte) 126,
      (byte) 36,
      (byte) 134,
      (byte) 31 /*0x1F*/,
      (byte) 89,
      (byte) 41,
      (byte) 233,
      (byte) 77,
      (byte) 112 /*0x70*/,
      (byte) 22,
      (byte) 187,
      (byte) 139,
      (byte) 212,
      (byte) 0,
      (byte) 55,
      (byte) 226,
      (byte) 39,
      (byte) 169,
      (byte) 197,
      (byte) 203,
      (byte) 100,
      (byte) 37
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 131,
      (byte) 9,
      (byte) 246,
      (byte) 198,
      (byte) 172,
      (byte) 221,
      (byte) 147,
      (byte) 216,
      (byte) 145,
      (byte) 99,
      (byte) 4,
      (byte) 1,
      (byte) 17,
      (byte) 252,
      (byte) 143,
      (byte) 184,
      (byte) 168,
      (byte) 147,
      (byte) 142,
      (byte) 36,
      (byte) 87,
      (byte) 182,
      (byte) 77,
      (byte) 107,
      (byte) 1,
      (byte) 119,
      (byte) 183,
      (byte) 85,
      (byte) 114,
      (byte) 112 /*0x70*/,
      (byte) 159,
      (byte) 71,
      (byte) 56,
      (byte) 252,
      (byte) 81,
      (byte) 171,
      (byte) 156,
      (byte) 11,
      (byte) 72,
      (byte) 142,
      (byte) 129,
      (byte) 247,
      (byte) 111,
      (byte) 125,
      (byte) 196,
      (byte) 224 /*0xE0*/,
      (byte) 72,
      (byte) 152,
      (byte) 27,
      (byte) 21,
      (byte) 218,
      (byte) 61,
      (byte) 179,
      (byte) 76,
      (byte) 0
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[44]
    {
      (byte) 23,
      (byte) 57,
      (byte) 177,
      (byte) 233,
      (byte) 145,
      (byte) 78,
      (byte) 33,
      (byte) 101,
      (byte) 133,
      (byte) 156,
      (byte) 7,
      (byte) 55,
      (byte) 125,
      (byte) 66,
      (byte) 95,
      (byte) 204,
      (byte) 99,
      (byte) 62,
      (byte) 187,
      (byte) 105,
      (byte) 8,
      (byte) 156,
      (byte) 250,
      (byte) 131,
      (byte) 204,
      (byte) 35,
      (byte) 112 /*0x70*/,
      (byte) 224 /*0xE0*/,
      (byte) 146,
      (byte) 197,
      (byte) 94,
      (byte) 88,
      (byte) 150,
      (byte) 184,
      (byte) 7,
      (byte) 32 /*0x20*/,
      (byte) 120,
      (byte) 0,
      (byte) 187,
      (byte) 34,
      (byte) 244,
      (byte) 85,
      (byte) 6,
      (byte) 56
    };
    byte[] numArray14 = new byte[44]
    {
      (byte) 3,
      (byte) 140,
      (byte) 218,
      (byte) 19,
      (byte) 68,
      (byte) 202,
      (byte) 154,
      (byte) 63 /*0x3F*/,
      (byte) 137,
      (byte) 99,
      (byte) 91,
      (byte) 41,
      (byte) 241,
      (byte) 36,
      (byte) 247,
      (byte) 234,
      (byte) 11,
      (byte) 3,
      (byte) 217,
      (byte) 158,
      (byte) 251,
      (byte) 187,
      (byte) 203,
      (byte) 108,
      (byte) 214,
      (byte) 30,
      (byte) 8,
      (byte) 249,
      (byte) 224 /*0xE0*/,
      (byte) 205,
      (byte) 187,
      (byte) 48 /*0x30*/,
      (byte) 112 /*0x70*/,
      (byte) 172,
      (byte) 176 /*0xB0*/,
      (byte) 196,
      (byte) 123,
      (byte) 87,
      (byte) 135,
      (byte) 165,
      (byte) 93,
      (byte) 241,
      (byte) 219,
      (byte) 146
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 44);
    for (int index = 0; index < 44; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12979()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[149];
      byte[] numArray2 = new byte[55]
      {
        (byte) 73,
        (byte) 248,
        (byte) 103,
        (byte) 38,
        (byte) 106,
        (byte) 36,
        (byte) 197,
        (byte) 122,
        (byte) 63 /*0x3F*/,
        (byte) 45,
        (byte) 28,
        (byte) 110,
        (byte) 221,
        (byte) 65,
        (byte) 211,
        (byte) 191,
        (byte) 61,
        (byte) 174,
        (byte) 160 /*0xA0*/,
        (byte) 196,
        (byte) 59,
        (byte) 63 /*0x3F*/,
        (byte) 84,
        (byte) 99,
        (byte) 130,
        (byte) 152,
        (byte) 116,
        (byte) 178,
        (byte) 150,
        (byte) 193,
        (byte) 138,
        (byte) 204,
        (byte) 190,
        (byte) 140,
        (byte) 150,
        (byte) 153,
        (byte) 94,
        (byte) 55,
        (byte) 238,
        (byte) 114,
        (byte) 242,
        (byte) 140,
        (byte) 225,
        (byte) 75,
        (byte) 227,
        (byte) 77,
        (byte) 69,
        (byte) 178,
        (byte) 130,
        (byte) 70,
        (byte) 250,
        (byte) 74,
        (byte) 95,
        (byte) 159,
        (byte) 169
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 243,
        (byte) 180,
        (byte) 157,
        (byte) 92,
        (byte) 72,
        (byte) 38,
        (byte) 43,
        (byte) 233,
        (byte) 145,
        (byte) 133,
        (byte) 139,
        (byte) 134,
        (byte) 178,
        (byte) 118,
        (byte) 72,
        (byte) 233,
        (byte) 77,
        (byte) 254,
        (byte) 7,
        (byte) 52,
        (byte) 112 /*0x70*/,
        (byte) 162,
        (byte) 193,
        (byte) 128 /*0x80*/,
        (byte) 84,
        (byte) 54,
        (byte) 6,
        (byte) 64 /*0x40*/,
        (byte) 186,
        (byte) 76,
        (byte) 161,
        (byte) 61,
        (byte) 71,
        (byte) 201,
        (byte) 138,
        (byte) 225,
        (byte) 200,
        (byte) 70,
        (byte) 148,
        (byte) 183,
        (byte) 140,
        (byte) 151,
        (byte) 221,
        (byte) 239,
        (byte) 110,
        (byte) 61,
        (byte) 54,
        (byte) 55,
        (byte) 111,
        (byte) 9,
        (byte) 40,
        (byte) 53,
        (byte) 232,
        (byte) 14,
        (byte) 179
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 3,
        (byte) 163,
        (byte) 209,
        (byte) 175,
        (byte) 19,
        (byte) 228,
        (byte) 125,
        (byte) 163,
        (byte) 79,
        (byte) 175,
        (byte) 81,
        (byte) 171,
        (byte) 57,
        (byte) 97,
        (byte) 5,
        (byte) 192 /*0xC0*/,
        (byte) 46,
        (byte) 210,
        (byte) 175,
        (byte) 93,
        (byte) 9,
        (byte) 127 /*0x7F*/,
        (byte) 53,
        (byte) 83,
        (byte) 194,
        (byte) 47,
        (byte) 80 /*0x50*/,
        (byte) 198,
        (byte) 63 /*0x3F*/,
        (byte) 109,
        (byte) 30,
        (byte) 227,
        (byte) 139,
        (byte) 245,
        (byte) 90,
        (byte) 148,
        (byte) 29,
        (byte) 194,
        (byte) 223,
        (byte) 168,
        (byte) 99,
        (byte) 167,
        (byte) 148,
        (byte) 157,
        (byte) 243,
        (byte) 7,
        (byte) 73,
        (byte) 229,
        (byte) 245,
        (byte) 138,
        (byte) 48 /*0x30*/,
        (byte) 205,
        (byte) 249,
        (byte) 172,
        (byte) 154
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 253,
        (byte) 221,
        (byte) 210,
        (byte) 229,
        (byte) 87,
        (byte) 126,
        (byte) 89,
        (byte) 161,
        (byte) 253,
        (byte) 240 /*0xF0*/,
        (byte) 78,
        (byte) 142,
        (byte) 199,
        (byte) 229,
        (byte) 38,
        (byte) 204,
        (byte) 176 /*0xB0*/,
        (byte) 163,
        (byte) 160 /*0xA0*/,
        (byte) 57,
        (byte) 251,
        (byte) 109,
        (byte) 252,
        (byte) 194,
        (byte) 145,
        (byte) 224 /*0xE0*/,
        (byte) 21,
        (byte) 229,
        (byte) 105,
        (byte) 28,
        (byte) 220,
        (byte) 86,
        (byte) 67,
        (byte) 130,
        (byte) 188,
        (byte) 211,
        (byte) 176 /*0xB0*/,
        (byte) 232,
        (byte) 73,
        (byte) 60,
        (byte) 154,
        (byte) 155,
        (byte) 148,
        (byte) 123,
        (byte) 156,
        (byte) 67,
        (byte) 170,
        (byte) 124,
        (byte) 160 /*0xA0*/,
        (byte) 20,
        (byte) 16 /*0x10*/,
        (byte) 116,
        (byte) 87,
        (byte) 83,
        (byte) 187
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[39];
      numArray6[12] = (byte) 113;
      numArray6[30] = (byte) 222;
      numArray6[2] = (byte) 227;
      numArray6[6] = (byte) 226;
      numArray6[4] = (byte) 183;
      numArray6[5] = (byte) 99;
      numArray6[38] = (byte) 172;
      numArray6[18] = (byte) 97;
      numArray6[8] = (byte) 110;
      numArray6[9] = (byte) 31 /*0x1F*/;
      numArray6[10] = (byte) 68;
      numArray6[11] = (byte) 6;
      numArray6[17] = (byte) 250;
      numArray6[13] = (byte) 163;
      numArray6[14] = (byte) 107;
      numArray6[0] = (byte) 125;
      numArray6[16 /*0x10*/] = (byte) 7;
      numArray6[23] = (byte) 230;
      numArray6[3] = (byte) 148;
      numArray6[35] = (byte) 213;
      numArray6[33] = (byte) 38;
      numArray6[21] = (byte) 95;
      numArray6[22] = (byte) 186;
      numArray6[15] = (byte) 242;
      numArray6[7] = (byte) 144 /*0x90*/;
      numArray6[1] = (byte) 237;
      numArray6[24] = (byte) 106;
      numArray6[27] = (byte) 60;
      numArray6[26] = (byte) 181;
      numArray6[31 /*0x1F*/] = (byte) 90;
      numArray6[29] = (byte) 233;
      numArray6[28] = (byte) 81;
      numArray6[32 /*0x20*/] = (byte) 214;
      numArray6[34] = (byte) 159;
      numArray6[19] = (byte) 235;
      numArray6[20] = (byte) 151;
      numArray6[36] = (byte) 251;
      numArray6[37] = (byte) 19;
      numArray6[25] = (byte) 209;
      byte[] numArray7 = new byte[39]
      {
        (byte) 26,
        (byte) 66,
        (byte) 254,
        (byte) 6,
        (byte) 125,
        (byte) 9,
        (byte) 158,
        (byte) 40,
        (byte) 189,
        (byte) 21,
        (byte) 103,
        (byte) 193,
        (byte) 121,
        (byte) 225,
        (byte) 56,
        (byte) 44,
        (byte) 165,
        (byte) 138,
        (byte) 163,
        (byte) 121,
        (byte) 1,
        (byte) 95,
        (byte) 218,
        (byte) 171,
        (byte) 241,
        (byte) 194,
        (byte) 19,
        (byte) 54,
        (byte) 242,
        (byte) 137,
        (byte) 211,
        (byte) 185,
        (byte) 163,
        (byte) 14,
        (byte) 102,
        (byte) 137,
        (byte) 170,
        (byte) 15,
        (byte) 226
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[149];
    byte[] numArray9 = new byte[55]
    {
      (byte) 205,
      (byte) 119,
      (byte) 118,
      (byte) 246,
      (byte) 44,
      (byte) 33,
      (byte) 61,
      (byte) 79,
      (byte) 129,
      (byte) 161,
      (byte) 254,
      (byte) 234,
      (byte) 198,
      (byte) 149,
      (byte) 11,
      (byte) 204,
      (byte) 1,
      (byte) 167,
      (byte) 27,
      (byte) 174,
      (byte) 55,
      (byte) 227,
      (byte) 46,
      (byte) 227,
      byte.MaxValue,
      (byte) 100,
      (byte) 27,
      (byte) 95,
      (byte) 75,
      (byte) 68,
      (byte) 96 /*0x60*/,
      (byte) 27,
      (byte) 157,
      (byte) 151,
      (byte) 225,
      (byte) 46,
      (byte) 201,
      (byte) 141,
      (byte) 20,
      (byte) 200,
      (byte) 243,
      (byte) 252,
      (byte) 130,
      (byte) 244,
      (byte) 249,
      (byte) 8,
      (byte) 134,
      (byte) 136,
      (byte) 45,
      (byte) 190,
      (byte) 216,
      (byte) 43,
      (byte) 198,
      (byte) 220,
      (byte) 22
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 81,
      (byte) 74,
      (byte) 201,
      (byte) 192 /*0xC0*/,
      (byte) 50,
      (byte) 249,
      (byte) 92,
      (byte) 184,
      (byte) 57,
      (byte) 189,
      (byte) 179,
      (byte) 20,
      (byte) 166,
      (byte) 12,
      (byte) 17,
      (byte) 240 /*0xF0*/,
      (byte) 128 /*0x80*/,
      (byte) 164,
      (byte) 21,
      (byte) 188,
      (byte) 118,
      (byte) 176 /*0xB0*/,
      (byte) 252,
      (byte) 49,
      (byte) 133,
      (byte) 49,
      (byte) 166,
      (byte) 204,
      (byte) 147,
      (byte) 108,
      (byte) 28,
      (byte) 134,
      (byte) 72,
      (byte) 163,
      (byte) 176 /*0xB0*/,
      (byte) 228,
      (byte) 217,
      (byte) 162,
      (byte) 252,
      (byte) 52,
      (byte) 84,
      (byte) 51,
      (byte) 135,
      (byte) 187,
      (byte) 3,
      (byte) 175,
      (byte) 54,
      (byte) 246,
      (byte) 181,
      (byte) 243,
      (byte) 237,
      (byte) 14,
      (byte) 2,
      (byte) 49,
      (byte) 238
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[39] = (byte) 248;
    numArray11[1] = (byte) 247;
    numArray11[5] = (byte) 4;
    numArray11[7] = (byte) 65;
    numArray11[2] = (byte) 199;
    numArray11[18] = (byte) 59;
    numArray11[10] = (byte) 47;
    numArray11[30] = (byte) 192 /*0xC0*/;
    numArray11[8] = (byte) 227;
    numArray11[9] = (byte) 76;
    numArray11[32 /*0x20*/] = (byte) 246;
    numArray11[20] = (byte) 57;
    numArray11[12] = (byte) 221;
    numArray11[13] = (byte) 3;
    numArray11[14] = (byte) 115;
    numArray11[35] = (byte) 150;
    numArray11[44] = (byte) 87;
    numArray11[38] = (byte) 21;
    numArray11[25] = (byte) 49;
    numArray11[19] = (byte) 97;
    numArray11[40] = (byte) 69;
    numArray11[48 /*0x30*/] = (byte) 101;
    numArray11[28] = (byte) 53;
    numArray11[23] = (byte) 222;
    numArray11[24] = (byte) 147;
    numArray11[45] = (byte) 212;
    numArray11[50] = (byte) 116;
    numArray11[47] = (byte) 186;
    numArray11[4] = (byte) 222;
    numArray11[11] = (byte) 180;
    numArray11[29] = (byte) 84;
    numArray11[49] = (byte) 152;
    numArray11[3] = (byte) 5;
    numArray11[21] = (byte) 233;
    numArray11[34] = (byte) 54;
    numArray11[15] = (byte) 146;
    numArray11[36] = (byte) 76;
    numArray11[37] = (byte) 195;
    numArray11[26] = (byte) 141;
    numArray11[17] = (byte) 187;
    numArray11[54] = (byte) 16 /*0x10*/;
    numArray11[41] = (byte) 179;
    numArray11[42] = (byte) 22;
    numArray11[43] = (byte) 188;
    numArray11[33] = (byte) 229;
    numArray11[27] = (byte) 107;
    numArray11[46] = (byte) 80 /*0x50*/;
    numArray11[6] = (byte) 61;
    numArray11[0] = (byte) 147;
    numArray11[22] = (byte) 246;
    numArray11[31 /*0x1F*/] = (byte) 129;
    numArray11[51] = (byte) 33;
    numArray11[52] = (byte) 218;
    numArray11[53] = (byte) 136;
    numArray11[16 /*0x10*/] = (byte) 82;
    byte[] numArray12 = new byte[55]
    {
      (byte) 67,
      (byte) 204,
      (byte) 28,
      (byte) 130,
      (byte) 72,
      (byte) 149,
      (byte) 131,
      (byte) 46,
      (byte) 134,
      (byte) 75,
      (byte) 77,
      (byte) 158,
      (byte) 17,
      (byte) 21,
      (byte) 136,
      (byte) 62,
      (byte) 238,
      (byte) 232,
      (byte) 23,
      (byte) 32 /*0x20*/,
      (byte) 31 /*0x1F*/,
      (byte) 135,
      (byte) 185,
      (byte) 119,
      (byte) 90,
      (byte) 87,
      (byte) 130,
      (byte) 163,
      (byte) 194,
      (byte) 223,
      (byte) 168,
      (byte) 41,
      (byte) 195,
      (byte) 85,
      (byte) 153,
      (byte) 30,
      (byte) 32 /*0x20*/,
      (byte) 170,
      (byte) 113,
      (byte) 82,
      (byte) 22,
      (byte) 19,
      (byte) 2,
      (byte) 67,
      (byte) 210,
      (byte) 20,
      (byte) 41,
      (byte) 212,
      (byte) 191,
      (byte) 191,
      (byte) 77,
      (byte) 113,
      (byte) 80 /*0x50*/,
      (byte) 181,
      (byte) 135
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[39]
    {
      (byte) 98,
      (byte) 179,
      (byte) 204,
      (byte) 163,
      (byte) 204,
      (byte) 111,
      (byte) 174,
      (byte) 236,
      (byte) 7,
      (byte) 78,
      (byte) 35,
      (byte) 65,
      (byte) 70,
      (byte) 227,
      (byte) 62,
      (byte) 62,
      (byte) 194,
      (byte) 169,
      (byte) 222,
      (byte) 75,
      (byte) 10,
      (byte) 221,
      (byte) 167,
      (byte) 135,
      (byte) 229,
      (byte) 123,
      (byte) 74,
      (byte) 251,
      (byte) 36,
      (byte) 15,
      (byte) 1,
      (byte) 74,
      (byte) 181,
      (byte) 207,
      (byte) 0,
      (byte) 157,
      (byte) 156,
      (byte) 221,
      (byte) 166
    };
    byte[] numArray14 = new byte[39]
    {
      (byte) 71,
      (byte) 183,
      (byte) 173,
      (byte) 29,
      (byte) 10,
      (byte) 47,
      (byte) 137,
      (byte) 93,
      (byte) 32 /*0x20*/,
      (byte) 82,
      (byte) 216,
      (byte) 205,
      (byte) 18,
      (byte) 159,
      (byte) 179,
      (byte) 47,
      (byte) 82,
      (byte) 179,
      (byte) 0,
      (byte) 25,
      (byte) 86,
      (byte) 55,
      (byte) 147,
      (byte) 24,
      (byte) 246,
      (byte) 37,
      (byte) 99,
      (byte) 33,
      (byte) 47,
      (byte) 219,
      (byte) 45,
      (byte) 147,
      (byte) 176 /*0xB0*/,
      (byte) 2,
      (byte) 94,
      (byte) 65,
      (byte) 73,
      (byte) 182,
      (byte) 103
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 39);
    for (int index = 0; index < 39; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12980()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[52];
      byte[] numArray2 = new byte[52];
      numArray2[11] = (byte) 187;
      numArray2[18] = (byte) 178;
      numArray2[2] = (byte) 171;
      numArray2[10] = (byte) 139;
      numArray2[13] = (byte) 11;
      numArray2[5] = (byte) 165;
      numArray2[6] = (byte) 61;
      numArray2[7] = (byte) 212;
      numArray2[28] = (byte) 228;
      numArray2[49] = (byte) 32 /*0x20*/;
      numArray2[16 /*0x10*/] = (byte) 81;
      numArray2[0] = (byte) 200;
      numArray2[12] = (byte) 133;
      numArray2[9] = (byte) 172;
      numArray2[14] = (byte) 68;
      numArray2[27] = (byte) 161;
      numArray2[26] = (byte) 161;
      numArray2[38] = (byte) 219;
      numArray2[19] = (byte) 199;
      numArray2[3] = (byte) 67;
      numArray2[22] = (byte) 217;
      numArray2[21] = (byte) 77;
      numArray2[45] = (byte) 20;
      numArray2[23] = (byte) 139;
      numArray2[46] = (byte) 122;
      numArray2[25] = (byte) 227;
      numArray2[41] = (byte) 59;
      numArray2[20] = (byte) 247;
      numArray2[17] = (byte) 37;
      numArray2[15] = (byte) 189;
      numArray2[30] = (byte) 166;
      numArray2[31 /*0x1F*/] = (byte) 113;
      numArray2[32 /*0x20*/] = (byte) 197;
      numArray2[33] = (byte) 59;
      numArray2[34] = (byte) 223;
      numArray2[35] = (byte) 82;
      numArray2[36] = (byte) 57;
      numArray2[29] = (byte) 3;
      numArray2[24] = (byte) 118;
      numArray2[39] = (byte) 85;
      numArray2[37] = (byte) 56;
      numArray2[48 /*0x30*/] = (byte) 88;
      numArray2[42] = (byte) 62;
      numArray2[43] = (byte) 157;
      numArray2[44] = (byte) 42;
      numArray2[40] = (byte) 231;
      numArray2[8] = (byte) 194;
      numArray2[47] = (byte) 145;
      numArray2[1] = (byte) 53;
      numArray2[4] = (byte) 111;
      numArray2[50] = (byte) 84;
      numArray2[51] = (byte) 18;
      byte[] numArray3 = new byte[52]
      {
        (byte) 154,
        (byte) 195,
        (byte) 6,
        (byte) 166,
        (byte) 31 /*0x1F*/,
        (byte) 17,
        (byte) 207,
        (byte) 60,
        (byte) 61,
        (byte) 93,
        (byte) 162,
        (byte) 165,
        (byte) 234,
        (byte) 8,
        (byte) 21,
        (byte) 105,
        (byte) 138,
        (byte) 26,
        (byte) 203,
        (byte) 201,
        (byte) 9,
        (byte) 128 /*0x80*/,
        (byte) 124,
        (byte) 104,
        (byte) 160 /*0xA0*/,
        (byte) 252,
        (byte) 211,
        (byte) 229,
        (byte) 211,
        (byte) 166,
        (byte) 172,
        (byte) 46,
        (byte) 238,
        (byte) 86,
        (byte) 186,
        (byte) 65,
        (byte) 67,
        (byte) 38,
        (byte) 23,
        (byte) 49,
        (byte) 82,
        (byte) 132,
        (byte) 146,
        (byte) 182,
        (byte) 67,
        (byte) 238,
        (byte) 49,
        (byte) 3,
        (byte) 31 /*0x1F*/,
        (byte) 0,
        (byte) 223,
        (byte) 116
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 52);
      for (int index = 0; index < 52; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[52];
    byte[] numArray5 = new byte[52]
    {
      (byte) 202,
      (byte) 156,
      (byte) 238,
      (byte) 109,
      (byte) 32 /*0x20*/,
      (byte) 191,
      (byte) 106,
      (byte) 35,
      (byte) 244,
      (byte) 81,
      (byte) 43,
      (byte) 16 /*0x10*/,
      (byte) 115,
      (byte) 206,
      (byte) 172,
      (byte) 185,
      (byte) 199,
      (byte) 251,
      (byte) 106,
      (byte) 172,
      (byte) 251,
      (byte) 11,
      (byte) 148,
      (byte) 113,
      (byte) 142,
      (byte) 163,
      (byte) 79,
      (byte) 68,
      (byte) 56,
      (byte) 169,
      (byte) 227,
      (byte) 40,
      (byte) 114,
      (byte) 202,
      (byte) 128 /*0x80*/,
      (byte) 173,
      (byte) 162,
      (byte) 247,
      (byte) 213,
      (byte) 89,
      (byte) 1,
      (byte) 43,
      (byte) 92,
      (byte) 250,
      (byte) 31 /*0x1F*/,
      (byte) 110,
      (byte) 36,
      (byte) 66,
      (byte) 113,
      (byte) 119,
      (byte) 158,
      (byte) 107
    };
    byte[] numArray6 = new byte[52];
    numArray6[41] = (byte) 88;
    numArray6[1] = (byte) 113;
    numArray6[2] = (byte) 0;
    numArray6[3] = (byte) 54;
    numArray6[50] = byte.MaxValue;
    numArray6[30] = (byte) 82;
    numArray6[51] = (byte) 82;
    numArray6[34] = (byte) 220;
    numArray6[40] = (byte) 50;
    numArray6[49] = (byte) 250;
    numArray6[24] = (byte) 147;
    numArray6[21] = (byte) 108;
    numArray6[4] = (byte) 103;
    numArray6[10] = (byte) 239;
    numArray6[25] = (byte) 70;
    numArray6[12] = (byte) 125;
    numArray6[16 /*0x10*/] = (byte) 216;
    numArray6[11] = (byte) 112 /*0x70*/;
    numArray6[18] = (byte) 149;
    numArray6[19] = (byte) 213;
    numArray6[20] = (byte) 19;
    numArray6[31 /*0x1F*/] = (byte) 209;
    numArray6[22] = (byte) 214;
    numArray6[29] = (byte) 109;
    numArray6[15] = (byte) 23;
    numArray6[38] = (byte) 119;
    numArray6[0] = (byte) 145;
    numArray6[27] = (byte) 79;
    numArray6[7] = (byte) 10;
    numArray6[23] = (byte) 124;
    numArray6[36] = (byte) 232;
    numArray6[26] = (byte) 122;
    numArray6[5] = (byte) 83;
    numArray6[33] = (byte) 9;
    numArray6[14] = (byte) 241;
    numArray6[17] = (byte) 44;
    numArray6[35] = (byte) 10;
    numArray6[37] = (byte) 123;
    numArray6[45] = (byte) 111;
    numArray6[39] = (byte) 48 /*0x30*/;
    numArray6[6] = (byte) 94;
    numArray6[8] = (byte) 199;
    numArray6[42] = (byte) 188;
    numArray6[43] = (byte) 202;
    numArray6[44] = (byte) 170;
    numArray6[28] = (byte) 186;
    numArray6[32 /*0x20*/] = (byte) 61;
    numArray6[47] = (byte) 183;
    numArray6[13] = (byte) 164;
    numArray6[46] = (byte) 81;
    numArray6[48 /*0x30*/] = (byte) 235;
    numArray6[9] = (byte) 249;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 52);
    for (int index = 0; index < 52; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[44];
    byte[] response = new byte[44];
    Array.Copy((Array) sc_12972.sspq, 88, (Array) numArray7, 0, 44);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12972.sspr, 88, (Array) numArray7, 0, 44);
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
}
