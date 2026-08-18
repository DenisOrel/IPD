// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13512
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13512
{
  internal static string ssp_appserver_13513()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[119];
      byte[] numArray2 = new byte[55]
      {
        (byte) 2,
        (byte) 46,
        (byte) 38,
        (byte) 204,
        (byte) 159,
        (byte) 97,
        (byte) 70,
        (byte) 241,
        (byte) 183,
        (byte) 43,
        (byte) 41,
        (byte) 7,
        (byte) 95,
        (byte) 166,
        (byte) 243,
        (byte) 83,
        (byte) 206,
        (byte) 79,
        (byte) 228,
        (byte) 23,
        (byte) 180,
        (byte) 119,
        (byte) 183,
        (byte) 146,
        (byte) 243,
        (byte) 131,
        (byte) 142,
        (byte) 202,
        (byte) 82,
        (byte) 249,
        (byte) 136,
        (byte) 98,
        (byte) 133,
        (byte) 44,
        (byte) 230,
        (byte) 34,
        (byte) 68,
        (byte) 245,
        (byte) 44,
        (byte) 4,
        (byte) 37,
        (byte) 98,
        (byte) 223,
        (byte) 167,
        (byte) 9,
        (byte) 107,
        (byte) 70,
        (byte) 101,
        (byte) 3,
        (byte) 122,
        (byte) 30,
        (byte) 191,
        (byte) 155,
        (byte) 242,
        (byte) 215
      };
      byte[] numArray3 = new byte[55];
      numArray3[35] = (byte) 54;
      numArray3[1] = (byte) 238;
      numArray3[2] = (byte) 25;
      numArray3[17] = (byte) 38;
      numArray3[16 /*0x10*/] = (byte) 53;
      numArray3[5] = (byte) 220;
      numArray3[7] = (byte) 196;
      numArray3[24] = (byte) 181;
      numArray3[8] = (byte) 193;
      numArray3[28] = (byte) 252;
      numArray3[10] = (byte) 2;
      numArray3[11] = (byte) 174;
      numArray3[3] = (byte) 103;
      numArray3[13] = (byte) 184;
      numArray3[14] = (byte) 207;
      numArray3[15] = (byte) 37;
      numArray3[26] = (byte) 168;
      numArray3[12] = (byte) 148;
      numArray3[43] = (byte) 244;
      numArray3[6] = (byte) 191;
      numArray3[20] = (byte) 146;
      numArray3[21] = (byte) 31 /*0x1F*/;
      numArray3[52] = (byte) 241;
      numArray3[51] = (byte) 190;
      numArray3[4] = (byte) 252;
      numArray3[53] = (byte) 238;
      numArray3[48 /*0x30*/] = (byte) 108;
      numArray3[41] = (byte) 9;
      numArray3[38] = (byte) 139;
      numArray3[29] = (byte) 115;
      numArray3[30] = (byte) 131;
      numArray3[31 /*0x1F*/] = (byte) 35;
      numArray3[32 /*0x20*/] = (byte) 66;
      numArray3[33] = (byte) 107;
      numArray3[34] = (byte) 145;
      numArray3[23] = (byte) 193;
      numArray3[19] = (byte) 143;
      numArray3[37] = (byte) 181;
      numArray3[39] = (byte) 199;
      numArray3[50] = (byte) 154;
      numArray3[36] = (byte) 22;
      numArray3[25] = (byte) 45;
      numArray3[42] = (byte) 8;
      numArray3[40] = (byte) 20;
      numArray3[44] = (byte) 109;
      numArray3[45] = (byte) 54;
      numArray3[46] = (byte) 131;
      numArray3[47] = (byte) 190;
      numArray3[27] = (byte) 237;
      numArray3[9] = (byte) 220;
      numArray3[0] = (byte) 67;
      numArray3[22] = (byte) 216;
      numArray3[18] = (byte) 220;
      numArray3[49] = (byte) 242;
      numArray3[54] = (byte) 171;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 10,
        (byte) 80 /*0x50*/,
        (byte) 221,
        (byte) 169,
        (byte) 181,
        (byte) 198,
        (byte) 226,
        (byte) 217,
        (byte) 254,
        (byte) 239,
        (byte) 221,
        (byte) 100,
        (byte) 37,
        (byte) 94,
        (byte) 187,
        (byte) 17,
        (byte) 174,
        (byte) 73,
        (byte) 142,
        (byte) 118,
        (byte) 11,
        (byte) 70,
        (byte) 158,
        (byte) 41,
        (byte) 131,
        (byte) 149,
        (byte) 108,
        (byte) 149,
        (byte) 43,
        (byte) 12,
        (byte) 146,
        (byte) 206,
        (byte) 103,
        (byte) 69,
        (byte) 165,
        (byte) 204,
        (byte) 50,
        (byte) 135,
        (byte) 243,
        (byte) 253,
        (byte) 62,
        (byte) 43,
        (byte) 67,
        (byte) 250,
        (byte) 54,
        (byte) 65,
        (byte) 12,
        (byte) 12,
        (byte) 138,
        (byte) 140,
        (byte) 12,
        (byte) 182,
        (byte) 248,
        (byte) 125,
        (byte) 90
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 37,
        (byte) 21,
        (byte) 25,
        (byte) 107,
        (byte) 114,
        (byte) 214,
        (byte) 121,
        (byte) 17,
        (byte) 121,
        (byte) 156,
        (byte) 216,
        (byte) 67,
        (byte) 147,
        (byte) 110,
        (byte) 100,
        (byte) 0,
        (byte) 143,
        byte.MaxValue,
        (byte) 239,
        (byte) 234,
        (byte) 68,
        (byte) 246,
        (byte) 195,
        (byte) 67,
        (byte) 213,
        (byte) 98,
        (byte) 168,
        (byte) 101,
        (byte) 214,
        (byte) 194,
        (byte) 192 /*0xC0*/,
        (byte) 219,
        (byte) 96 /*0x60*/,
        (byte) 8,
        (byte) 127 /*0x7F*/,
        (byte) 172,
        (byte) 214,
        (byte) 116,
        (byte) 95,
        (byte) 68,
        (byte) 165,
        (byte) 83,
        (byte) 25,
        (byte) 165,
        (byte) 100,
        (byte) 252,
        (byte) 79,
        (byte) 167,
        (byte) 216,
        (byte) 110,
        (byte) 135,
        (byte) 210,
        (byte) 39,
        (byte) 38,
        (byte) 74
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[9];
      numArray6[7] = (byte) 93;
      numArray6[5] = (byte) 173;
      numArray6[2] = (byte) 32 /*0x20*/;
      numArray6[1] = (byte) 38;
      numArray6[0] = (byte) 180;
      numArray6[4] = (byte) 77;
      numArray6[6] = (byte) 242;
      numArray6[3] = (byte) 89;
      numArray6[8] = (byte) 14;
      byte[] numArray7 = new byte[9]
      {
        (byte) 179,
        (byte) 16 /*0x10*/,
        (byte) 27,
        (byte) 137,
        (byte) 100,
        (byte) 240 /*0xF0*/,
        (byte) 226,
        (byte) 245,
        (byte) 73
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[119];
    byte[] numArray9 = new byte[55];
    numArray9[46] = (byte) 211;
    numArray9[5] = (byte) 159;
    numArray9[15] = (byte) 210;
    numArray9[3] = (byte) 95;
    numArray9[4] = (byte) 11;
    numArray9[36] = (byte) 151;
    numArray9[6] = byte.MaxValue;
    numArray9[27] = (byte) 230;
    numArray9[8] = (byte) 214;
    numArray9[51] = (byte) 69;
    numArray9[50] = (byte) 236;
    numArray9[41] = (byte) 130;
    numArray9[22] = (byte) 69;
    numArray9[13] = (byte) 126;
    numArray9[1] = (byte) 65;
    numArray9[10] = (byte) 234;
    numArray9[35] = (byte) 229;
    numArray9[17] = (byte) 241;
    numArray9[44] = (byte) 245;
    numArray9[19] = (byte) 81;
    numArray9[37] = (byte) 249;
    numArray9[21] = (byte) 40;
    numArray9[31 /*0x1F*/] = (byte) 122;
    numArray9[0] = (byte) 243;
    numArray9[24] = (byte) 83;
    numArray9[14] = (byte) 96 /*0x60*/;
    numArray9[26] = (byte) 173;
    numArray9[48 /*0x30*/] = (byte) 197;
    numArray9[28] = (byte) 95;
    numArray9[29] = (byte) 103;
    numArray9[11] = (byte) 225;
    numArray9[49] = (byte) 131;
    numArray9[32 /*0x20*/] = (byte) 193;
    numArray9[33] = (byte) 14;
    numArray9[34] = (byte) 192 /*0xC0*/;
    numArray9[12] = (byte) 26;
    numArray9[30] = (byte) 70;
    numArray9[43] = (byte) 19;
    numArray9[38] = (byte) 65;
    numArray9[39] = (byte) 13;
    numArray9[40] = (byte) 242;
    numArray9[23] = (byte) 43;
    numArray9[42] = (byte) 55;
    numArray9[45] = (byte) 215;
    numArray9[16 /*0x10*/] = (byte) 163;
    numArray9[25] = (byte) 156;
    numArray9[18] = (byte) 126;
    numArray9[47] = (byte) 95;
    numArray9[9] = (byte) 233;
    numArray9[53] = (byte) 176 /*0xB0*/;
    numArray9[2] = (byte) 137;
    numArray9[7] = (byte) 219;
    numArray9[52] = (byte) 98;
    numArray9[20] = (byte) 156;
    numArray9[54] = (byte) 7;
    byte[] numArray10 = new byte[55];
    numArray10[8] = (byte) 53;
    numArray10[51] = (byte) 213;
    numArray10[2] = (byte) 193;
    numArray10[39] = (byte) 140;
    numArray10[41] = (byte) 212;
    numArray10[20] = (byte) 247;
    numArray10[6] = (byte) 111;
    numArray10[7] = (byte) 200;
    numArray10[25] = (byte) 59;
    numArray10[9] = (byte) 184;
    numArray10[16 /*0x10*/] = (byte) 184;
    numArray10[11] = (byte) 233;
    numArray10[12] = (byte) 182;
    numArray10[49] = (byte) 119;
    numArray10[21] = (byte) 186;
    numArray10[15] = (byte) 188;
    numArray10[22] = (byte) 211;
    numArray10[44] = (byte) 221;
    numArray10[18] = (byte) 81;
    numArray10[26] = (byte) 36;
    numArray10[14] = (byte) 228;
    numArray10[19] = (byte) 254;
    numArray10[4] = (byte) 218;
    numArray10[29] = (byte) 86;
    numArray10[54] = (byte) 201;
    numArray10[36] = (byte) 85;
    numArray10[13] = (byte) 87;
    numArray10[27] = (byte) 209;
    numArray10[28] = (byte) 212;
    numArray10[47] = (byte) 250;
    numArray10[30] = (byte) 23;
    numArray10[45] = (byte) 246;
    numArray10[32 /*0x20*/] = (byte) 17;
    numArray10[1] = (byte) 56;
    numArray10[34] = (byte) 89;
    numArray10[35] = (byte) 175;
    numArray10[5] = (byte) 164;
    numArray10[37] = (byte) 35;
    numArray10[33] = (byte) 251;
    numArray10[24] = (byte) 80 /*0x50*/;
    numArray10[40] = (byte) 103;
    numArray10[53] = (byte) 150;
    numArray10[42] = (byte) 52;
    numArray10[3] = (byte) 62;
    numArray10[46] = (byte) 110;
    numArray10[38] = (byte) 196;
    numArray10[17] = (byte) 57;
    numArray10[10] = (byte) 153;
    numArray10[48 /*0x30*/] = (byte) 47;
    numArray10[0] = (byte) 168;
    numArray10[43] = (byte) 118;
    numArray10[31 /*0x1F*/] = (byte) 130;
    numArray10[52] = (byte) 142;
    numArray10[50] = (byte) 226;
    numArray10[23] = (byte) 117;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 120,
      (byte) 202,
      (byte) 164,
      (byte) 246,
      (byte) 33,
      (byte) 12,
      (byte) 187,
      (byte) 55,
      (byte) 48 /*0x30*/,
      (byte) 79,
      (byte) 237,
      (byte) 172,
      (byte) 113,
      (byte) 239,
      (byte) 92,
      (byte) 174,
      (byte) 220,
      (byte) 163,
      (byte) 173,
      (byte) 84,
      (byte) 244,
      (byte) 129,
      (byte) 26,
      (byte) 64 /*0x40*/,
      (byte) 211,
      (byte) 29,
      (byte) 76,
      (byte) 177,
      (byte) 177,
      (byte) 137,
      (byte) 241,
      (byte) 63 /*0x3F*/,
      (byte) 107,
      (byte) 24,
      (byte) 153,
      (byte) 148,
      (byte) 137,
      (byte) 129,
      (byte) 191,
      (byte) 90,
      (byte) 24,
      (byte) 150,
      (byte) 253,
      (byte) 90,
      (byte) 211,
      (byte) 44,
      (byte) 71,
      (byte) 86,
      (byte) 41,
      (byte) 10,
      (byte) 193,
      (byte) 41,
      (byte) 44,
      (byte) 92,
      (byte) 19
    };
    byte[] numArray12 = new byte[55];
    numArray12[52] = (byte) 107;
    numArray12[1] = (byte) 222;
    numArray12[16 /*0x10*/] = (byte) 204;
    numArray12[13] = (byte) 219;
    numArray12[4] = (byte) 137;
    numArray12[5] = (byte) 74;
    numArray12[11] = (byte) 179;
    numArray12[0] = (byte) 82;
    numArray12[2] = (byte) 41;
    numArray12[9] = (byte) 174;
    numArray12[10] = (byte) 31 /*0x1F*/;
    numArray12[7] = (byte) 74;
    numArray12[12] = (byte) 224 /*0xE0*/;
    numArray12[25] = (byte) 123;
    numArray12[36] = (byte) 142;
    numArray12[37] = (byte) 141;
    numArray12[31 /*0x1F*/] = (byte) 216;
    numArray12[3] = (byte) 182;
    numArray12[18] = (byte) 8;
    numArray12[53] = (byte) 110;
    numArray12[8] = (byte) 162;
    numArray12[28] = (byte) 52;
    numArray12[22] = (byte) 196;
    numArray12[50] = (byte) 113;
    numArray12[24] = (byte) 43;
    numArray12[15] = (byte) 157;
    numArray12[38] = (byte) 143;
    numArray12[27] = (byte) 47;
    numArray12[20] = (byte) 250;
    numArray12[29] = (byte) 193;
    numArray12[30] = (byte) 56;
    numArray12[17] = (byte) 51;
    numArray12[32 /*0x20*/] = (byte) 102;
    numArray12[21] = (byte) 22;
    numArray12[34] = (byte) 54;
    numArray12[35] = (byte) 0;
    numArray12[23] = (byte) 68;
    numArray12[14] = (byte) 188;
    numArray12[26] = (byte) 252;
    numArray12[39] = (byte) 134;
    numArray12[40] = byte.MaxValue;
    numArray12[41] = (byte) 85;
    numArray12[42] = (byte) 135;
    numArray12[43] = (byte) 182;
    numArray12[45] = (byte) 78;
    numArray12[44] = (byte) 54;
    numArray12[46] = (byte) 244;
    numArray12[47] = (byte) 65;
    numArray12[48 /*0x30*/] = (byte) 137;
    numArray12[49] = (byte) 242;
    numArray12[19] = (byte) 124;
    numArray12[51] = (byte) 166;
    numArray12[33] = (byte) 222;
    numArray12[6] = (byte) 30;
    numArray12[54] = (byte) 114;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[9];
    numArray13[5] = (byte) 223;
    numArray13[1] = (byte) 123;
    numArray13[2] = (byte) 45;
    numArray13[8] = (byte) 153;
    numArray13[4] = (byte) 129;
    numArray13[7] = (byte) 149;
    numArray13[6] = (byte) 223;
    numArray13[3] = (byte) 201;
    numArray13[0] = (byte) 245;
    byte[] numArray14 = new byte[9]
    {
      (byte) 111,
      (byte) 222,
      (byte) 247,
      (byte) 112 /*0x70*/,
      (byte) 248,
      (byte) 104,
      (byte) 215,
      (byte) 89,
      (byte) 38
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 9);
    for (int index = 0; index < 9; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static int ssp_appserver_13514(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 231,
      (byte) 245,
      (byte) 38,
      (byte) 193,
      (byte) 98,
      (byte) 129,
      (byte) 58,
      (byte) 237,
      (byte) 78,
      (byte) 46,
      (byte) 245,
      (byte) 48 /*0x30*/,
      (byte) 226,
      (byte) 141,
      (byte) 209,
      (byte) 142,
      (byte) 48 /*0x30*/,
      (byte) 118,
      (byte) 102,
      (byte) 140,
      (byte) 185,
      (byte) 154,
      (byte) 76,
      (byte) 125,
      (byte) 229,
      (byte) 104,
      (byte) 29,
      (byte) 33,
      (byte) 244,
      (byte) 105,
      (byte) 70,
      (byte) 43,
      (byte) 174,
      (byte) 192 /*0xC0*/,
      (byte) 69,
      (byte) 211,
      (byte) 90,
      (byte) 30,
      (byte) 125,
      (byte) 19,
      (byte) 95,
      (byte) 68,
      (byte) 5,
      (byte) 133,
      (byte) 238,
      (byte) 207,
      (byte) 26,
      (byte) 154
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 150,
      (byte) 131,
      (byte) 207,
      (byte) 90,
      (byte) 5,
      (byte) 25,
      (byte) 166,
      (byte) 240 /*0xF0*/,
      (byte) 84,
      (byte) 86,
      (byte) 200,
      (byte) 54,
      (byte) 115,
      (byte) 46,
      (byte) 116,
      (byte) 244,
      (byte) 153,
      (byte) 240 /*0xF0*/,
      (byte) 172,
      (byte) 48 /*0x30*/,
      (byte) 182,
      (byte) 230,
      (byte) 100,
      (byte) 182,
      (byte) 4,
      (byte) 115,
      (byte) 82,
      (byte) 103,
      (byte) 123,
      (byte) 252,
      (byte) 219,
      (byte) 41,
      (byte) 165,
      (byte) 19,
      (byte) 131,
      (byte) 111,
      (byte) 147,
      (byte) 127 /*0x7F*/,
      (byte) 214,
      (byte) 119,
      (byte) 172,
      (byte) 69,
      (byte) 151,
      (byte) 4,
      (byte) 121,
      (byte) 200,
      (byte) 194,
      (byte) 111
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
