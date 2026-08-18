// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13165
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13165
{
  private static byte[] sspq = new byte[78]
  {
    (byte) 12,
    (byte) 134,
    (byte) 75,
    (byte) 43,
    (byte) 133,
    (byte) 180,
    (byte) 3,
    (byte) 196,
    (byte) 240 /*0xF0*/,
    (byte) 160 /*0xA0*/,
    (byte) 19,
    (byte) 97,
    (byte) 12,
    (byte) 168,
    (byte) 67,
    (byte) 169,
    (byte) 184,
    (byte) 180,
    (byte) 193,
    (byte) 12,
    (byte) 212,
    (byte) 217,
    (byte) 114,
    (byte) 26,
    (byte) 154,
    (byte) 149,
    (byte) 189,
    (byte) 7,
    (byte) 208 /*0xD0*/,
    (byte) 68,
    (byte) 157,
    (byte) 18,
    (byte) 24,
    (byte) 109,
    (byte) 57,
    (byte) 75,
    (byte) 21,
    (byte) 161,
    (byte) 84,
    (byte) 133,
    (byte) 232,
    (byte) 177,
    (byte) 63 /*0x3F*/,
    (byte) 78,
    (byte) 119,
    (byte) 215,
    (byte) 10,
    (byte) 26,
    (byte) 236,
    (byte) 20,
    (byte) 14,
    (byte) 65,
    (byte) 209,
    (byte) 10,
    (byte) 85,
    (byte) 219,
    (byte) 113,
    (byte) 142,
    (byte) 54,
    (byte) 82,
    (byte) 7,
    (byte) 194,
    (byte) 232,
    (byte) 181,
    (byte) 177,
    (byte) 202,
    (byte) 147,
    (byte) 157,
    byte.MaxValue,
    (byte) 178,
    (byte) 238,
    (byte) 178,
    (byte) 156,
    (byte) 159,
    (byte) 4,
    (byte) 237,
    (byte) 167,
    (byte) 31 /*0x1F*/
  };
  private static byte[] sspr = new byte[78]
  {
    (byte) 139,
    (byte) 54,
    (byte) 232,
    (byte) 206,
    (byte) 80 /*0x50*/,
    (byte) 66,
    (byte) 160 /*0xA0*/,
    (byte) 27,
    (byte) 142,
    (byte) 58,
    (byte) 207,
    (byte) 56,
    (byte) 134,
    (byte) 252,
    (byte) 113,
    (byte) 203,
    (byte) 49,
    (byte) 239,
    (byte) 52,
    (byte) 2,
    (byte) 99,
    (byte) 19,
    (byte) 168,
    (byte) 210,
    (byte) 201,
    (byte) 223,
    (byte) 122,
    (byte) 140,
    (byte) 149,
    (byte) 208 /*0xD0*/,
    (byte) 205,
    (byte) 98,
    (byte) 21,
    (byte) 224 /*0xE0*/,
    (byte) 141,
    (byte) 58,
    (byte) 254,
    (byte) 83,
    (byte) 69,
    (byte) 46,
    (byte) 187,
    (byte) 129,
    (byte) 212,
    (byte) 207,
    (byte) 64 /*0x40*/,
    (byte) 232,
    (byte) 103,
    (byte) 69,
    (byte) 113,
    (byte) 135,
    (byte) 37,
    (byte) 61,
    (byte) 193,
    (byte) 110,
    (byte) 242,
    (byte) 32 /*0x20*/,
    (byte) 152,
    (byte) 61,
    (byte) 82,
    (byte) 2,
    (byte) 226,
    (byte) 42,
    (byte) 49,
    (byte) 139,
    (byte) 139,
    (byte) 86,
    (byte) 106,
    (byte) 141,
    (byte) 144 /*0x90*/,
    (byte) 243,
    (byte) 65,
    (byte) 54,
    (byte) 148,
    (byte) 79,
    (byte) 25,
    (byte) 235,
    (byte) 154,
    (byte) 215
  };

  internal static string ssp_appserver_13166()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18];
      numArray2[7] = (byte) 105;
      numArray2[1] = (byte) 224 /*0xE0*/;
      numArray2[2] = (byte) 79;
      numArray2[3] = (byte) 191;
      numArray2[4] = (byte) 197;
      numArray2[14] = (byte) 215;
      numArray2[15] = (byte) 85;
      numArray2[8] = (byte) 79;
      numArray2[0] = (byte) 74;
      numArray2[5] = (byte) 44;
      numArray2[12] = (byte) 133;
      numArray2[11] = (byte) 191;
      numArray2[9] = (byte) 35;
      numArray2[17] = (byte) 80 /*0x50*/;
      numArray2[6] = (byte) 40;
      numArray2[13] = (byte) 184;
      numArray2[16 /*0x10*/] = (byte) 42;
      numArray2[10] = (byte) 182;
      byte[] numArray3 = new byte[18]
      {
        (byte) 65,
        (byte) 215,
        (byte) 17,
        (byte) 50,
        (byte) 79,
        (byte) 5,
        (byte) 228,
        (byte) 22,
        (byte) 63 /*0x3F*/,
        (byte) 46,
        (byte) 136,
        (byte) 115,
        (byte) 58,
        (byte) 132,
        (byte) 232,
        (byte) 25,
        (byte) 68,
        (byte) 141
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_13165.sspq, 0, (Array) numArray4, 0, 14);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_13165.sspr, 0, (Array) numArray4, 0, 14);
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
    byte[] numArray5 = new byte[18];
    byte[] numArray6 = new byte[18];
    numArray6[1] = (byte) 207;
    numArray6[8] = (byte) 106;
    numArray6[2] = (byte) 31 /*0x1F*/;
    numArray6[17] = (byte) 16 /*0x10*/;
    numArray6[3] = (byte) 6;
    numArray6[5] = (byte) 61;
    numArray6[14] = (byte) 63 /*0x3F*/;
    numArray6[7] = (byte) 124;
    numArray6[0] = (byte) 225;
    numArray6[9] = (byte) 136;
    numArray6[12] = (byte) 152;
    numArray6[11] = (byte) 14;
    numArray6[10] = (byte) 206;
    numArray6[13] = (byte) 31 /*0x1F*/;
    numArray6[15] = (byte) 187;
    numArray6[16 /*0x10*/] = (byte) 43;
    numArray6[4] = (byte) 218;
    numArray6[6] = (byte) 142;
    byte[] numArray7 = new byte[18];
    numArray7[8] = (byte) 22;
    numArray7[0] = (byte) 174;
    numArray7[2] = (byte) 0;
    numArray7[1] = (byte) 96 /*0x60*/;
    numArray7[4] = (byte) 221;
    numArray7[11] = (byte) 22;
    numArray7[6] = (byte) 127 /*0x7F*/;
    numArray7[9] = (byte) 67;
    numArray7[16 /*0x10*/] = (byte) 205;
    numArray7[7] = (byte) 65;
    numArray7[10] = (byte) 226;
    numArray7[17] = (byte) 240 /*0xF0*/;
    numArray7[5] = (byte) 236;
    numArray7[13] = (byte) 149;
    numArray7[14] = (byte) 158;
    numArray7[15] = (byte) 130;
    numArray7[3] = (byte) 76;
    numArray7[12] = (byte) 237;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[33];
    byte[] response1 = new byte[33];
    Array.Copy((Array) sc_13165.sspq, 14, (Array) numArray8, 0, 33);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_13165.sspr, 14, (Array) numArray8, 0, 33);
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

  internal static string ssp_appserver_13167()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[49];
      byte[] numArray2 = new byte[49]
      {
        (byte) 224 /*0xE0*/,
        (byte) 150,
        (byte) 174,
        (byte) 108,
        (byte) 205,
        (byte) 15,
        (byte) 238,
        (byte) 131,
        (byte) 5,
        (byte) 0,
        (byte) 72,
        (byte) 238,
        (byte) 119,
        (byte) 109,
        (byte) 50,
        (byte) 112 /*0x70*/,
        (byte) 176 /*0xB0*/,
        (byte) 243,
        (byte) 149,
        (byte) 49,
        (byte) 79,
        (byte) 247,
        (byte) 148,
        (byte) 23,
        (byte) 95,
        (byte) 59,
        (byte) 36,
        (byte) 34,
        (byte) 154,
        (byte) 35,
        (byte) 162,
        (byte) 195,
        (byte) 139,
        (byte) 72,
        (byte) 39,
        (byte) 58,
        (byte) 150,
        (byte) 48 /*0x30*/,
        (byte) 68,
        (byte) 8,
        (byte) 126,
        (byte) 182,
        (byte) 70,
        (byte) 20,
        (byte) 88,
        (byte) 102,
        (byte) 14,
        (byte) 123,
        (byte) 126
      };
      byte[] numArray3 = new byte[49];
      numArray3[26] = (byte) 93;
      numArray3[1] = (byte) 173;
      numArray3[2] = (byte) 130;
      numArray3[3] = (byte) 183;
      numArray3[18] = (byte) 138;
      numArray3[45] = (byte) 61;
      numArray3[11] = (byte) 118;
      numArray3[40] = (byte) 166;
      numArray3[5] = (byte) 229;
      numArray3[9] = (byte) 244;
      numArray3[27] = (byte) 78;
      numArray3[38] = (byte) 187;
      numArray3[8] = (byte) 28;
      numArray3[13] = (byte) 195;
      numArray3[36] = (byte) 157;
      numArray3[15] = (byte) 253;
      numArray3[16 /*0x10*/] = (byte) 251;
      numArray3[17] = (byte) 6;
      numArray3[14] = (byte) 37;
      numArray3[47] = (byte) 206;
      numArray3[29] = (byte) 27;
      numArray3[21] = (byte) 233;
      numArray3[22] = (byte) 76;
      numArray3[44] = (byte) 73;
      numArray3[41] = (byte) 191;
      numArray3[34] = (byte) 31 /*0x1F*/;
      numArray3[32 /*0x20*/] = (byte) 124;
      numArray3[6] = (byte) 125;
      numArray3[28] = (byte) 162;
      numArray3[4] = (byte) 244;
      numArray3[30] = (byte) 241;
      numArray3[31 /*0x1F*/] = (byte) 64 /*0x40*/;
      numArray3[12] = (byte) 21;
      numArray3[33] = (byte) 127 /*0x7F*/;
      numArray3[25] = (byte) 202;
      numArray3[35] = (byte) 219;
      numArray3[0] = (byte) 143;
      numArray3[39] = (byte) 104;
      numArray3[19] = (byte) 141;
      numArray3[10] = (byte) 24;
      numArray3[24] = (byte) 166;
      numArray3[23] = (byte) 78;
      numArray3[42] = (byte) 235;
      numArray3[43] = byte.MaxValue;
      numArray3[7] = (byte) 92;
      numArray3[48 /*0x30*/] = (byte) 210;
      numArray3[46] = (byte) 166;
      numArray3[37] = (byte) 55;
      numArray3[20] = (byte) 209;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 49);
      for (int index = 0; index < 49; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[49];
    byte[] numArray5 = new byte[49];
    numArray5[20] = (byte) 177;
    numArray5[11] = (byte) 214;
    numArray5[36] = (byte) 29;
    numArray5[26] = (byte) 252;
    numArray5[4] = (byte) 29;
    numArray5[33] = (byte) 247;
    numArray5[6] = (byte) 181;
    numArray5[12] = (byte) 19;
    numArray5[27] = (byte) 110;
    numArray5[9] = (byte) 54;
    numArray5[48 /*0x30*/] = (byte) 220;
    numArray5[42] = (byte) 149;
    numArray5[1] = (byte) 52;
    numArray5[13] = (byte) 100;
    numArray5[37] = (byte) 216;
    numArray5[19] = (byte) 4;
    numArray5[15] = (byte) 40;
    numArray5[16 /*0x10*/] = (byte) 27;
    numArray5[41] = (byte) 140;
    numArray5[2] = (byte) 134;
    numArray5[7] = (byte) 37;
    numArray5[21] = (byte) 11;
    numArray5[38] = (byte) 104;
    numArray5[23] = (byte) 192 /*0xC0*/;
    numArray5[39] = (byte) 73;
    numArray5[25] = (byte) 92;
    numArray5[32 /*0x20*/] = (byte) 236;
    numArray5[5] = (byte) 212;
    numArray5[28] = (byte) 1;
    numArray5[29] = (byte) 202;
    numArray5[30] = (byte) 77;
    numArray5[31 /*0x1F*/] = (byte) 201;
    numArray5[22] = (byte) 7;
    numArray5[0] = (byte) 223;
    numArray5[34] = (byte) 77;
    numArray5[35] = (byte) 116;
    numArray5[8] = (byte) 141;
    numArray5[18] = (byte) 63 /*0x3F*/;
    numArray5[40] = (byte) 203;
    numArray5[14] = (byte) 65;
    numArray5[24] = (byte) 218;
    numArray5[17] = (byte) 91;
    numArray5[3] = (byte) 244;
    numArray5[43] = (byte) 8;
    numArray5[44] = (byte) 182;
    numArray5[45] = (byte) 41;
    numArray5[46] = (byte) 16 /*0x10*/;
    numArray5[47] = (byte) 77;
    numArray5[10] = (byte) 129;
    byte[] numArray6 = new byte[49]
    {
      (byte) 139,
      (byte) 131,
      (byte) 122,
      (byte) 52,
      (byte) 93,
      (byte) 51,
      (byte) 77,
      (byte) 189,
      (byte) 243,
      (byte) 167,
      (byte) 239,
      (byte) 4,
      (byte) 236,
      (byte) 163,
      (byte) 142,
      (byte) 213,
      (byte) 60,
      (byte) 85,
      (byte) 119,
      (byte) 96 /*0x60*/,
      (byte) 153,
      (byte) 240 /*0xF0*/,
      (byte) 197,
      (byte) 33,
      (byte) 120,
      (byte) 253,
      (byte) 48 /*0x30*/,
      (byte) 204,
      (byte) 11,
      (byte) 132,
      (byte) 194,
      (byte) 165,
      (byte) 113,
      (byte) 130,
      (byte) 241,
      (byte) 218,
      (byte) 127 /*0x7F*/,
      (byte) 147,
      (byte) 169,
      (byte) 115,
      (byte) 79,
      (byte) 50,
      (byte) 82,
      (byte) 52,
      (byte) 223,
      (byte) 33,
      (byte) 237,
      (byte) 201,
      (byte) 191
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 49);
    for (int index = 0; index < 49; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13168(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 76;
    sourceArray1[41] = (byte) 26;
    sourceArray1[36] = (byte) 169;
    sourceArray1[3] = (byte) 20;
    sourceArray1[6] = (byte) 44;
    sourceArray1[44] = (byte) 84;
    sourceArray1[42] = (byte) 24;
    sourceArray1[15] = (byte) 170;
    sourceArray1[30] = (byte) 12;
    sourceArray1[9] = (byte) 188;
    sourceArray1[8] = (byte) 46;
    sourceArray1[11] = (byte) 117;
    sourceArray1[12] = (byte) 40;
    sourceArray1[13] = (byte) 180;
    sourceArray1[18] = (byte) 203;
    sourceArray1[32 /*0x20*/] = (byte) 87;
    sourceArray1[16 /*0x10*/] = (byte) 163;
    sourceArray1[47] = (byte) 208 /*0xD0*/;
    sourceArray1[27] = (byte) 186;
    sourceArray1[23] = (byte) 134;
    sourceArray1[20] = (byte) 158;
    sourceArray1[29] = (byte) 223;
    sourceArray1[22] = (byte) 179;
    sourceArray1[17] = (byte) 103;
    sourceArray1[1] = (byte) 127 /*0x7F*/;
    sourceArray1[4] = (byte) 130;
    sourceArray1[39] = (byte) 137;
    sourceArray1[25] = (byte) 131;
    sourceArray1[21] = (byte) 134;
    sourceArray1[14] = (byte) 171;
    sourceArray1[0] = (byte) 14;
    sourceArray1[31 /*0x1F*/] = (byte) 114;
    sourceArray1[2] = (byte) 110;
    sourceArray1[19] = (byte) 117;
    sourceArray1[34] = (byte) 131;
    sourceArray1[35] = (byte) 249;
    sourceArray1[10] = (byte) 105;
    sourceArray1[37] = (byte) 48 /*0x30*/;
    sourceArray1[26] = (byte) 179;
    sourceArray1[28] = (byte) 162;
    sourceArray1[40] = (byte) 121;
    sourceArray1[33] = (byte) 121;
    sourceArray1[5] = (byte) 149;
    sourceArray1[43] = (byte) 154;
    sourceArray1[7] = (byte) 94;
    sourceArray1[45] = (byte) 50;
    sourceArray1[38] = (byte) 206;
    sourceArray1[46] = (byte) 72;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[13] = (byte) 175;
    sourceArray2[24] = (byte) 75;
    sourceArray2[4] = (byte) 236;
    sourceArray2[33] = (byte) 60;
    sourceArray2[5] = (byte) 198;
    sourceArray2[37] = (byte) 183;
    sourceArray2[6] = (byte) 162;
    sourceArray2[7] = (byte) 174;
    sourceArray2[23] = (byte) 87;
    sourceArray2[9] = (byte) 172;
    sourceArray2[10] = (byte) 36;
    sourceArray2[8] = (byte) 99;
    sourceArray2[12] = (byte) 9;
    sourceArray2[28] = (byte) 234;
    sourceArray2[14] = (byte) 157;
    sourceArray2[30] = (byte) 8;
    sourceArray2[16 /*0x10*/] = (byte) 66;
    sourceArray2[17] = (byte) 246;
    sourceArray2[18] = (byte) 186;
    sourceArray2[19] = (byte) 51;
    sourceArray2[20] = (byte) 16 /*0x10*/;
    sourceArray2[41] = (byte) 194;
    sourceArray2[39] = (byte) 168;
    sourceArray2[11] = (byte) 44;
    sourceArray2[1] = (byte) 106;
    sourceArray2[25] = (byte) 176 /*0xB0*/;
    sourceArray2[22] = (byte) 174;
    sourceArray2[46] = (byte) 222;
    sourceArray2[0] = (byte) 60;
    sourceArray2[29] = (byte) 32 /*0x20*/;
    sourceArray2[27] = (byte) 71;
    sourceArray2[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
    sourceArray2[32 /*0x20*/] = (byte) 185;
    sourceArray2[44] = (byte) 206;
    sourceArray2[34] = (byte) 170;
    sourceArray2[21] = (byte) 66;
    sourceArray2[36] = (byte) 42;
    sourceArray2[38] = (byte) 25;
    sourceArray2[42] = (byte) 140;
    sourceArray2[40] = (byte) 21;
    sourceArray2[15] = (byte) 224 /*0xE0*/;
    sourceArray2[35] = (byte) 32 /*0x20*/;
    sourceArray2[26] = (byte) 30;
    sourceArray2[43] = (byte) 107;
    sourceArray2[3] = (byte) 186;
    sourceArray2[45] = (byte) 254;
    sourceArray2[2] = (byte) 219;
    sourceArray2[47] = (byte) 188;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13169()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 12,
        (byte) 166,
        (byte) 146,
        (byte) 28,
        (byte) 224 /*0xE0*/,
        (byte) 176 /*0xB0*/,
        (byte) 100,
        (byte) 208 /*0xD0*/,
        (byte) 203,
        (byte) 106
      };
      byte[] numArray3 = new byte[10];
      numArray3[9] = (byte) 179;
      numArray3[1] = (byte) 214;
      numArray3[2] = (byte) 33;
      numArray3[3] = (byte) 239;
      numArray3[8] = (byte) 44;
      numArray3[5] = (byte) 178;
      numArray3[7] = (byte) 25;
      numArray3[0] = (byte) 29;
      numArray3[6] = (byte) 23;
      numArray3[4] = (byte) 155;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[9] = (byte) 52;
    numArray5[3] = (byte) 193;
    numArray5[0] = (byte) 134;
    numArray5[8] = (byte) 9;
    numArray5[4] = (byte) 44;
    numArray5[2] = (byte) 143;
    numArray5[6] = (byte) 253;
    numArray5[7] = (byte) 84;
    numArray5[5] = (byte) 237;
    numArray5[1] = (byte) 239;
    byte[] numArray6 = new byte[10]
    {
      byte.MaxValue,
      (byte) 192 /*0xC0*/,
      (byte) 241,
      (byte) 149,
      (byte) 120,
      (byte) 15,
      (byte) 75,
      (byte) 114,
      (byte) 40,
      (byte) 107
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_13170()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 67,
        (byte) 53,
        (byte) 201,
        (byte) 227,
        (byte) 104,
        (byte) 245,
        (byte) 38,
        (byte) 16 /*0x10*/,
        (byte) 234,
        (byte) 233
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 192 /*0xC0*/,
        (byte) 101,
        (byte) 172,
        (byte) 88,
        (byte) 141,
        (byte) 187,
        (byte) 124,
        (byte) 96 /*0x60*/,
        (byte) 253,
        (byte) 33
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
      (byte) 239,
      (byte) 56,
      (byte) 226,
      (byte) 206,
      (byte) 21,
      (byte) 126,
      (byte) 180,
      (byte) 171,
      (byte) 120,
      (byte) 162
    };
    byte[] numArray6 = new byte[10];
    numArray6[0] = (byte) 176 /*0xB0*/;
    numArray6[5] = (byte) 95;
    numArray6[7] = (byte) 96 /*0x60*/;
    numArray6[9] = (byte) 138;
    numArray6[4] = (byte) 240 /*0xF0*/;
    numArray6[2] = (byte) 103;
    numArray6[6] = (byte) 108;
    numArray6[1] = (byte) 220;
    numArray6[8] = (byte) 186;
    numArray6[3] = (byte) 32 /*0x20*/;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[31 /*0x1F*/];
    byte[] response = new byte[31 /*0x1F*/];
    Array.Copy((Array) sc_13165.sspq, 47, (Array) numArray7, 0, 31 /*0x1F*/);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_13165.sspr, 47, (Array) numArray7, 0, 31 /*0x1F*/);
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
