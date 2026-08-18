// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12572
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12572
{
  internal static int ssp_appserver_12573(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 68,
      (byte) 209,
      (byte) 48 /*0x30*/,
      (byte) 51,
      (byte) 112 /*0x70*/,
      (byte) 138,
      (byte) 81,
      (byte) 232,
      (byte) 28,
      (byte) 139,
      (byte) 12,
      (byte) 109,
      (byte) 83,
      (byte) 2,
      (byte) 32 /*0x20*/,
      (byte) 238,
      (byte) 109,
      (byte) 111,
      (byte) 131,
      (byte) 78,
      (byte) 182,
      (byte) 134,
      (byte) 137,
      (byte) 238,
      (byte) 186,
      (byte) 84,
      (byte) 60,
      (byte) 206,
      (byte) 137,
      (byte) 2,
      (byte) 217,
      (byte) 174,
      (byte) 210,
      (byte) 204,
      (byte) 16 /*0x10*/,
      (byte) 164,
      (byte) 58,
      (byte) 237,
      (byte) 209,
      (byte) 47,
      (byte) 15,
      (byte) 60,
      (byte) 129,
      (byte) 112 /*0x70*/,
      (byte) 131,
      (byte) 107,
      (byte) 72,
      (byte) 69
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 178,
      (byte) 140,
      (byte) 34,
      (byte) 147,
      (byte) 118,
      (byte) 251,
      (byte) 110,
      (byte) 155,
      (byte) 252,
      (byte) 120,
      (byte) 179,
      (byte) 86,
      (byte) 228,
      (byte) 102,
      (byte) 5,
      (byte) 59,
      (byte) 238,
      (byte) 246,
      (byte) 52,
      (byte) 122,
      (byte) 219,
      (byte) 152,
      (byte) 172,
      (byte) 161,
      (byte) 101,
      (byte) 89,
      (byte) 94,
      (byte) 221,
      (byte) 252,
      (byte) 120,
      (byte) 224 /*0xE0*/,
      (byte) 127 /*0x7F*/,
      (byte) 32 /*0x20*/,
      (byte) 128 /*0x80*/,
      (byte) 98,
      (byte) 53,
      (byte) 67,
      (byte) 206,
      (byte) 189,
      (byte) 202,
      (byte) 47,
      (byte) 62,
      (byte) 186,
      (byte) 9,
      (byte) 98,
      (byte) 122,
      (byte) 218,
      (byte) 20
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12574()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[122];
      byte[] numArray2 = new byte[55];
      numArray2[0] = (byte) 174;
      numArray2[2] = byte.MaxValue;
      numArray2[43] = (byte) 141;
      numArray2[3] = (byte) 189;
      numArray2[4] = (byte) 71;
      numArray2[52] = (byte) 150;
      numArray2[46] = (byte) 189;
      numArray2[7] = (byte) 27;
      numArray2[47] = (byte) 37;
      numArray2[17] = (byte) 123;
      numArray2[10] = (byte) 95;
      numArray2[31 /*0x1F*/] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 178;
      numArray2[13] = (byte) 94;
      numArray2[14] = (byte) 154;
      numArray2[15] = (byte) 135;
      numArray2[16 /*0x10*/] = (byte) 241;
      numArray2[1] = (byte) 191;
      numArray2[53] = (byte) 215;
      numArray2[19] = (byte) 127 /*0x7F*/;
      numArray2[42] = (byte) 226;
      numArray2[6] = (byte) 120;
      numArray2[22] = (byte) 62;
      numArray2[23] = (byte) 121;
      numArray2[27] = (byte) 78;
      numArray2[25] = (byte) 25;
      numArray2[21] = (byte) 84;
      numArray2[44] = (byte) 221;
      numArray2[28] = (byte) 12;
      numArray2[54] = (byte) 178;
      numArray2[40] = (byte) 116;
      numArray2[36] = (byte) 204;
      numArray2[9] = (byte) 130;
      numArray2[33] = (byte) 136;
      numArray2[34] = byte.MaxValue;
      numArray2[29] = (byte) 246;
      numArray2[35] = (byte) 18;
      numArray2[20] = (byte) 158;
      numArray2[38] = (byte) 93;
      numArray2[50] = (byte) 84;
      numArray2[37] = (byte) 83;
      numArray2[49] = (byte) 192 /*0xC0*/;
      numArray2[24] = (byte) 5;
      numArray2[11] = (byte) 25;
      numArray2[30] = (byte) 107;
      numArray2[45] = (byte) 103;
      numArray2[8] = (byte) 77;
      numArray2[39] = (byte) 248;
      numArray2[48 /*0x30*/] = (byte) 208 /*0xD0*/;
      numArray2[5] = (byte) 65;
      numArray2[18] = (byte) 179;
      numArray2[51] = (byte) 231;
      numArray2[32 /*0x20*/] = (byte) 171;
      numArray2[41] = (byte) 181;
      numArray2[26] = (byte) 107;
      byte[] numArray3 = new byte[55]
      {
        (byte) 183,
        (byte) 146,
        (byte) 140,
        (byte) 206,
        (byte) 31 /*0x1F*/,
        (byte) 221,
        (byte) 93,
        (byte) 191,
        (byte) 58,
        (byte) 228,
        (byte) 206,
        (byte) 139,
        (byte) 119,
        (byte) 109,
        (byte) 130,
        (byte) 192 /*0xC0*/,
        (byte) 119,
        (byte) 113,
        (byte) 149,
        (byte) 157,
        (byte) 138,
        (byte) 8,
        (byte) 204,
        (byte) 113,
        (byte) 98,
        (byte) 42,
        (byte) 106,
        (byte) 46,
        (byte) 46,
        (byte) 135,
        (byte) 168,
        (byte) 110,
        (byte) 20,
        (byte) 131,
        (byte) 25,
        (byte) 244,
        (byte) 191,
        (byte) 94,
        (byte) 249,
        (byte) 54,
        (byte) 142,
        (byte) 47,
        (byte) 146,
        (byte) 149,
        (byte) 22,
        (byte) 5,
        (byte) 142,
        (byte) 134,
        (byte) 235,
        (byte) 195,
        (byte) 77,
        (byte) 172,
        (byte) 156,
        (byte) 158,
        (byte) 152
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 232,
        (byte) 25,
        (byte) 210,
        (byte) 145,
        (byte) 171,
        (byte) 195,
        (byte) 181,
        (byte) 147,
        (byte) 119,
        (byte) 154,
        (byte) 85,
        (byte) 185,
        (byte) 247,
        (byte) 115,
        (byte) 2,
        (byte) 239,
        (byte) 153,
        (byte) 191,
        (byte) 66,
        (byte) 231,
        (byte) 60,
        (byte) 223,
        (byte) 165,
        (byte) 200,
        (byte) 126,
        (byte) 54,
        (byte) 69,
        (byte) 234,
        (byte) 186,
        (byte) 206,
        (byte) 6,
        (byte) 11,
        (byte) 188,
        (byte) 193,
        (byte) 94,
        (byte) 129,
        (byte) 93,
        (byte) 144 /*0x90*/,
        (byte) 159,
        (byte) 226,
        (byte) 213,
        (byte) 225,
        (byte) 69,
        (byte) 93,
        (byte) 62,
        (byte) 13,
        (byte) 80 /*0x50*/,
        (byte) 146,
        (byte) 180,
        (byte) 218,
        (byte) 64 /*0x40*/,
        (byte) 13,
        (byte) 135,
        (byte) 1,
        (byte) 147
      };
      byte[] numArray5 = new byte[55];
      numArray5[40] = (byte) 131;
      numArray5[8] = (byte) 159;
      numArray5[46] = (byte) 206;
      numArray5[27] = (byte) 146;
      numArray5[4] = (byte) 161;
      numArray5[16 /*0x10*/] = (byte) 208 /*0xD0*/;
      numArray5[6] = (byte) 241;
      numArray5[10] = (byte) 167;
      numArray5[14] = (byte) 65;
      numArray5[19] = (byte) 174;
      numArray5[15] = (byte) 77;
      numArray5[35] = (byte) 201;
      numArray5[12] = (byte) 21;
      numArray5[13] = (byte) 3;
      numArray5[38] = (byte) 83;
      numArray5[2] = (byte) 14;
      numArray5[0] = (byte) 82;
      numArray5[33] = (byte) 202;
      numArray5[37] = (byte) 39;
      numArray5[22] = (byte) 158;
      numArray5[9] = (byte) 34;
      numArray5[32 /*0x20*/] = (byte) 162;
      numArray5[17] = (byte) 34;
      numArray5[23] = (byte) 101;
      numArray5[54] = (byte) 152;
      numArray5[25] = (byte) 183;
      numArray5[26] = (byte) 213;
      numArray5[7] = (byte) 141;
      numArray5[44] = (byte) 248;
      numArray5[1] = (byte) 172;
      numArray5[42] = (byte) 79;
      numArray5[3] = (byte) 201;
      numArray5[30] = (byte) 87;
      numArray5[21] = (byte) 130;
      numArray5[34] = (byte) 147;
      numArray5[5] = (byte) 156;
      numArray5[18] = (byte) 46;
      numArray5[20] = (byte) 5;
      numArray5[47] = (byte) 46;
      numArray5[39] = (byte) 131;
      numArray5[11] = (byte) 37;
      numArray5[41] = (byte) 143;
      numArray5[29] = (byte) 239;
      numArray5[43] = (byte) 217;
      numArray5[45] = (byte) 205;
      numArray5[28] = (byte) 225;
      numArray5[31 /*0x1F*/] = (byte) 18;
      numArray5[36] = (byte) 47;
      numArray5[48 /*0x30*/] = (byte) 129;
      numArray5[49] = (byte) 207;
      numArray5[50] = (byte) 68;
      numArray5[51] = (byte) 125;
      numArray5[52] = (byte) 124;
      numArray5[53] = (byte) 33;
      numArray5[24] = (byte) 244;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[12];
      numArray6[8] = (byte) 197;
      numArray6[0] = (byte) 126;
      numArray6[2] = (byte) 144 /*0x90*/;
      numArray6[6] = (byte) 28;
      numArray6[1] = (byte) 160 /*0xA0*/;
      numArray6[4] = (byte) 148;
      numArray6[3] = (byte) 203;
      numArray6[7] = (byte) 156;
      numArray6[10] = (byte) 7;
      numArray6[9] = (byte) 67;
      numArray6[5] = (byte) 250;
      numArray6[11] = (byte) 191;
      byte[] numArray7 = new byte[12]
      {
        (byte) 120,
        (byte) 154,
        (byte) 88,
        (byte) 162,
        (byte) 242,
        (byte) 78,
        (byte) 94,
        (byte) 228,
        (byte) 174,
        (byte) 62,
        (byte) 233,
        (byte) 120
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[122];
    byte[] numArray9 = new byte[55]
    {
      (byte) 85,
      (byte) 11,
      (byte) 93,
      (byte) 201,
      (byte) 245,
      (byte) 231,
      (byte) 223,
      (byte) 213,
      (byte) 222,
      (byte) 153,
      (byte) 153,
      (byte) 118,
      (byte) 14,
      (byte) 127 /*0x7F*/,
      (byte) 44,
      (byte) 47,
      (byte) 204,
      (byte) 19,
      (byte) 190,
      (byte) 192 /*0xC0*/,
      (byte) 9,
      (byte) 173,
      (byte) 245,
      (byte) 42,
      (byte) 234,
      (byte) 231,
      (byte) 110,
      (byte) 175,
      (byte) 232,
      (byte) 61,
      (byte) 197,
      (byte) 150,
      (byte) 146,
      (byte) 98,
      (byte) 198,
      (byte) 144 /*0x90*/,
      (byte) 103,
      (byte) 121,
      (byte) 18,
      (byte) 215,
      (byte) 147,
      (byte) 105,
      (byte) 172,
      (byte) 94,
      (byte) 99,
      (byte) 143,
      (byte) 67,
      (byte) 128 /*0x80*/,
      (byte) 81,
      (byte) 102,
      (byte) 150,
      (byte) 183,
      (byte) 117,
      (byte) 65,
      (byte) 141
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 200,
      (byte) 118,
      (byte) 106,
      (byte) 189,
      (byte) 106,
      (byte) 216,
      (byte) 91,
      (byte) 51,
      (byte) 104,
      (byte) 52,
      (byte) 212,
      (byte) 36,
      (byte) 221,
      (byte) 157,
      (byte) 42,
      (byte) 132,
      (byte) 64 /*0x40*/,
      (byte) 114,
      (byte) 248,
      (byte) 84,
      (byte) 127 /*0x7F*/,
      (byte) 42,
      (byte) 80 /*0x50*/,
      (byte) 218,
      (byte) 47,
      (byte) 209,
      (byte) 150,
      (byte) 61,
      (byte) 64 /*0x40*/,
      (byte) 53,
      (byte) 203,
      (byte) 251,
      (byte) 197,
      (byte) 125,
      (byte) 122,
      (byte) 216,
      (byte) 155,
      (byte) 179,
      (byte) 174,
      (byte) 3,
      (byte) 206,
      (byte) 236,
      (byte) 119,
      (byte) 188,
      (byte) 19,
      (byte) 143,
      (byte) 209,
      (byte) 69,
      (byte) 105,
      (byte) 92,
      (byte) 190,
      (byte) 203,
      (byte) 148,
      (byte) 243,
      (byte) 112 /*0x70*/
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 50,
      (byte) 223,
      (byte) 14,
      (byte) 120,
      (byte) 48 /*0x30*/,
      (byte) 236,
      (byte) 160 /*0xA0*/,
      (byte) 212,
      (byte) 2,
      (byte) 228,
      (byte) 171,
      (byte) 184,
      (byte) 156,
      (byte) 199,
      (byte) 82,
      (byte) 0,
      (byte) 230,
      (byte) 44,
      (byte) 160 /*0xA0*/,
      (byte) 1,
      (byte) 151,
      (byte) 199,
      (byte) 90,
      (byte) 163,
      (byte) 238,
      (byte) 93,
      (byte) 129,
      (byte) 167,
      (byte) 13,
      (byte) 32 /*0x20*/,
      (byte) 80 /*0x50*/,
      (byte) 213,
      (byte) 30,
      (byte) 122,
      (byte) 52,
      (byte) 153,
      (byte) 4,
      (byte) 95,
      (byte) 99,
      (byte) 124,
      (byte) 11,
      (byte) 137,
      (byte) 96 /*0x60*/,
      (byte) 171,
      (byte) 152,
      (byte) 199,
      (byte) 180,
      (byte) 159,
      (byte) 67,
      (byte) 182,
      (byte) 142,
      (byte) 21,
      (byte) 236,
      (byte) 167,
      (byte) 126
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 64 /*0x40*/,
      (byte) 158,
      (byte) 239,
      (byte) 126,
      (byte) 203,
      (byte) 13,
      (byte) 233,
      (byte) 151,
      (byte) 197,
      (byte) 13,
      (byte) 121,
      (byte) 254,
      (byte) 141,
      (byte) 44,
      (byte) 56,
      (byte) 229,
      (byte) 61,
      (byte) 169,
      (byte) 54,
      (byte) 199,
      (byte) 250,
      (byte) 22,
      (byte) 153,
      (byte) 110,
      (byte) 254,
      (byte) 180,
      (byte) 51,
      (byte) 46,
      (byte) 123,
      (byte) 182,
      (byte) 180,
      (byte) 53,
      (byte) 75,
      (byte) 185,
      (byte) 66,
      (byte) 173,
      (byte) 103,
      (byte) 36,
      (byte) 203,
      (byte) 61,
      (byte) 139,
      (byte) 67,
      (byte) 72,
      (byte) 111,
      (byte) 100,
      (byte) 132,
      (byte) 244,
      (byte) 98,
      (byte) 20,
      (byte) 249,
      (byte) 79,
      (byte) 164,
      (byte) 105,
      (byte) 8,
      (byte) 200
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[12]
    {
      (byte) 106,
      (byte) 137,
      (byte) 211,
      (byte) 106,
      (byte) 249,
      (byte) 198,
      (byte) 225,
      (byte) 147,
      (byte) 214,
      (byte) 154,
      (byte) 34,
      (byte) 146
    };
    byte[] numArray14 = new byte[12];
    numArray14[8] = (byte) 97;
    numArray14[5] = (byte) 61;
    numArray14[1] = (byte) 87;
    numArray14[7] = (byte) 88;
    numArray14[4] = (byte) 226;
    numArray14[2] = (byte) 198;
    numArray14[9] = (byte) 90;
    numArray14[11] = (byte) 52;
    numArray14[10] = (byte) 56;
    numArray14[3] = (byte) 55;
    numArray14[0] = (byte) 69;
    numArray14[6] = (byte) 88;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 12);
    for (int index = 0; index < 12; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12575()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[93];
      byte[] numArray2 = new byte[55]
      {
        (byte) 59,
        (byte) 232,
        (byte) 119,
        (byte) 251,
        (byte) 201,
        (byte) 21,
        (byte) 153,
        (byte) 213,
        (byte) 144 /*0x90*/,
        (byte) 17,
        (byte) 18,
        (byte) 195,
        (byte) 126,
        (byte) 23,
        (byte) 194,
        (byte) 45,
        (byte) 146,
        (byte) 12,
        (byte) 138,
        (byte) 186,
        (byte) 7,
        (byte) 76,
        (byte) 200,
        (byte) 177,
        (byte) 113,
        (byte) 3,
        (byte) 212,
        (byte) 96 /*0x60*/,
        (byte) 98,
        (byte) 42,
        (byte) 135,
        (byte) 149,
        (byte) 111,
        (byte) 80 /*0x50*/,
        (byte) 87,
        (byte) 238,
        (byte) 34,
        (byte) 38,
        (byte) 201,
        (byte) 163,
        (byte) 99,
        (byte) 177,
        (byte) 56,
        (byte) 18,
        (byte) 227,
        (byte) 74,
        (byte) 232,
        (byte) 152,
        (byte) 72,
        (byte) 249,
        (byte) 21,
        (byte) 214,
        (byte) 217,
        (byte) 156,
        (byte) 138
      };
      byte[] numArray3 = new byte[55];
      numArray3[2] = (byte) 175;
      numArray3[29] = (byte) 122;
      numArray3[41] = (byte) 50;
      numArray3[4] = (byte) 3;
      numArray3[20] = (byte) 44;
      numArray3[5] = (byte) 240 /*0xF0*/;
      numArray3[49] = (byte) 222;
      numArray3[21] = (byte) 155;
      numArray3[8] = (byte) 89;
      numArray3[9] = (byte) 40;
      numArray3[10] = (byte) 245;
      numArray3[36] = (byte) 211;
      numArray3[27] = (byte) 28;
      numArray3[13] = (byte) 234;
      numArray3[33] = (byte) 111;
      numArray3[44] = (byte) 126;
      numArray3[17] = (byte) 22;
      numArray3[14] = (byte) 13;
      numArray3[18] = (byte) 37;
      numArray3[19] = (byte) 89;
      numArray3[0] = (byte) 243;
      numArray3[53] = (byte) 167;
      numArray3[46] = (byte) 151;
      numArray3[23] = (byte) 93;
      numArray3[38] = (byte) 5;
      numArray3[26] = (byte) 84;
      numArray3[42] = (byte) 174;
      numArray3[48 /*0x30*/] = (byte) 169;
      numArray3[28] = (byte) 137;
      numArray3[7] = (byte) 77;
      numArray3[30] = (byte) 37;
      numArray3[31 /*0x1F*/] = (byte) 229;
      numArray3[32 /*0x20*/] = (byte) 127 /*0x7F*/;
      numArray3[54] = (byte) 4;
      numArray3[34] = (byte) 214;
      numArray3[15] = (byte) 100;
      numArray3[12] = (byte) 36;
      numArray3[37] = (byte) 145;
      numArray3[6] = (byte) 15;
      numArray3[3] = (byte) 124;
      numArray3[47] = (byte) 59;
      numArray3[40] = (byte) 12;
      numArray3[25] = (byte) 157;
      numArray3[43] = (byte) 4;
      numArray3[35] = (byte) 123;
      numArray3[45] = (byte) 38;
      numArray3[1] = (byte) 59;
      numArray3[39] = (byte) 241;
      numArray3[24] = (byte) 253;
      numArray3[11] = (byte) 2;
      numArray3[50] = (byte) 8;
      numArray3[51] = (byte) 109;
      numArray3[52] = (byte) 60;
      numArray3[16 /*0x10*/] = (byte) 209;
      numArray3[22] = (byte) 6;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38]
      {
        (byte) 246,
        (byte) 99,
        (byte) 170,
        (byte) 3,
        (byte) 32 /*0x20*/,
        (byte) 8,
        (byte) 80 /*0x50*/,
        (byte) 216,
        (byte) 192 /*0xC0*/,
        (byte) 127 /*0x7F*/,
        (byte) 104,
        (byte) 87,
        (byte) 5,
        (byte) 173,
        (byte) 206,
        (byte) 63 /*0x3F*/,
        (byte) 131,
        (byte) 44,
        (byte) 22,
        (byte) 63 /*0x3F*/,
        (byte) 45,
        (byte) 104,
        (byte) 47,
        (byte) 96 /*0x60*/,
        (byte) 100,
        (byte) 163,
        (byte) 123,
        (byte) 106,
        (byte) 29,
        (byte) 201,
        (byte) 141,
        (byte) 144 /*0x90*/,
        (byte) 140,
        (byte) 100,
        (byte) 102,
        (byte) 57,
        (byte) 88,
        (byte) 95
      };
      byte[] numArray5 = new byte[38]
      {
        (byte) 146,
        (byte) 58,
        (byte) 173,
        (byte) 98,
        (byte) 207,
        (byte) 71,
        (byte) 212,
        (byte) 236,
        (byte) 227,
        (byte) 118,
        (byte) 73,
        (byte) 59,
        (byte) 231,
        (byte) 248,
        (byte) 109,
        (byte) 166,
        (byte) 247,
        (byte) 19,
        (byte) 248,
        (byte) 191,
        (byte) 161,
        (byte) 241,
        (byte) 75,
        (byte) 25,
        (byte) 57,
        (byte) 224 /*0xE0*/,
        (byte) 83,
        (byte) 201,
        (byte) 34,
        (byte) 11,
        (byte) 220,
        (byte) 61,
        (byte) 92,
        (byte) 134,
        (byte) 213,
        (byte) 228,
        (byte) 212,
        (byte) 52
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[93];
    byte[] numArray7 = new byte[55]
    {
      (byte) 176 /*0xB0*/,
      (byte) 165,
      (byte) 119,
      (byte) 82,
      (byte) 128 /*0x80*/,
      (byte) 110,
      (byte) 64 /*0x40*/,
      (byte) 219,
      (byte) 83,
      (byte) 111,
      (byte) 25,
      (byte) 30,
      (byte) 156,
      (byte) 60,
      (byte) 142,
      (byte) 51,
      (byte) 74,
      (byte) 158,
      (byte) 124,
      (byte) 141,
      (byte) 230,
      (byte) 186,
      (byte) 246,
      (byte) 241,
      (byte) 215,
      (byte) 203,
      (byte) 161,
      (byte) 70,
      (byte) 149,
      (byte) 231,
      (byte) 35,
      (byte) 35,
      (byte) 223,
      (byte) 189,
      (byte) 125,
      (byte) 194,
      (byte) 184,
      (byte) 14,
      (byte) 228,
      (byte) 53,
      (byte) 58,
      (byte) 143,
      (byte) 201,
      (byte) 90,
      (byte) 116,
      (byte) 114,
      (byte) 240 /*0xF0*/,
      (byte) 40,
      (byte) 216,
      (byte) 6,
      (byte) 14,
      (byte) 192 /*0xC0*/,
      (byte) 140,
      (byte) 168,
      (byte) 155
    };
    byte[] numArray8 = new byte[55];
    numArray8[4] = (byte) 29;
    numArray8[1] = (byte) 144 /*0x90*/;
    numArray8[3] = (byte) 21;
    numArray8[43] = (byte) 136;
    numArray8[28] = (byte) 147;
    numArray8[26] = (byte) 36;
    numArray8[39] = (byte) 38;
    numArray8[7] = (byte) 86;
    numArray8[48 /*0x30*/] = (byte) 209;
    numArray8[13] = (byte) 119;
    numArray8[18] = (byte) 222;
    numArray8[54] = (byte) 241;
    numArray8[45] = (byte) 208 /*0xD0*/;
    numArray8[52] = (byte) 79;
    numArray8[6] = (byte) 49;
    numArray8[15] = (byte) 154;
    numArray8[10] = (byte) 164;
    numArray8[17] = (byte) 197;
    numArray8[20] = (byte) 135;
    numArray8[24] = (byte) 199;
    numArray8[2] = (byte) 158;
    numArray8[21] = (byte) 59;
    numArray8[19] = (byte) 46;
    numArray8[23] = (byte) 194;
    numArray8[0] = (byte) 202;
    numArray8[25] = (byte) 217;
    numArray8[14] = (byte) 211;
    numArray8[27] = (byte) 19;
    numArray8[8] = (byte) 105;
    numArray8[29] = (byte) 122;
    numArray8[16 /*0x10*/] = (byte) 81;
    numArray8[30] = (byte) 201;
    numArray8[32 /*0x20*/] = (byte) 85;
    numArray8[33] = (byte) 252;
    numArray8[34] = (byte) 189;
    numArray8[35] = (byte) 161;
    numArray8[36] = (byte) 136;
    numArray8[37] = (byte) 52;
    numArray8[38] = (byte) 253;
    numArray8[44] = (byte) 133;
    numArray8[40] = (byte) 244;
    numArray8[31 /*0x1F*/] = (byte) 44;
    numArray8[5] = (byte) 12;
    numArray8[51] = (byte) 176 /*0xB0*/;
    numArray8[11] = (byte) 152;
    numArray8[53] = (byte) 148;
    numArray8[46] = (byte) 78;
    numArray8[47] = (byte) 187;
    numArray8[41] = (byte) 41;
    numArray8[49] = (byte) 209;
    numArray8[42] = (byte) 132;
    numArray8[22] = (byte) 20;
    numArray8[50] = (byte) 103;
    numArray8[12] = (byte) 81;
    numArray8[9] = (byte) 219;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[38]
    {
      (byte) 8,
      (byte) 20,
      (byte) 218,
      (byte) 83,
      (byte) 140,
      (byte) 182,
      (byte) 147,
      (byte) 233,
      (byte) 95,
      (byte) 8,
      (byte) 188,
      (byte) 54,
      (byte) 183,
      (byte) 72,
      (byte) 167,
      (byte) 139,
      (byte) 222,
      (byte) 19,
      (byte) 14,
      (byte) 148,
      (byte) 141,
      (byte) 254,
      (byte) 50,
      (byte) 201,
      (byte) 15,
      (byte) 96 /*0x60*/,
      (byte) 4,
      (byte) 6,
      (byte) 92,
      (byte) 81,
      (byte) 225,
      (byte) 221,
      (byte) 132,
      (byte) 0,
      (byte) 42,
      (byte) 127 /*0x7F*/,
      (byte) 84,
      (byte) 21
    };
    byte[] numArray10 = new byte[38]
    {
      (byte) 54,
      (byte) 184,
      (byte) 250,
      (byte) 235,
      (byte) 53,
      (byte) 194,
      (byte) 173,
      (byte) 176 /*0xB0*/,
      (byte) 90,
      (byte) 36,
      (byte) 209,
      (byte) 211,
      (byte) 111,
      (byte) 177,
      (byte) 90,
      (byte) 180,
      (byte) 209,
      (byte) 46,
      (byte) 216,
      (byte) 31 /*0x1F*/,
      (byte) 104,
      (byte) 222,
      (byte) 181,
      (byte) 19,
      (byte) 132,
      (byte) 128 /*0x80*/,
      (byte) 144 /*0x90*/,
      (byte) 194,
      (byte) 219,
      (byte) 79,
      (byte) 46,
      (byte) 247,
      (byte) 101,
      (byte) 54,
      (byte) 247,
      (byte) 90,
      (byte) 249,
      (byte) 133
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 38);
    for (int index = 0; index < 38; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12576()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[139];
      byte[] numArray2 = new byte[55]
      {
        (byte) 31 /*0x1F*/,
        (byte) 43,
        (byte) 84,
        (byte) 50,
        (byte) 86,
        (byte) 66,
        (byte) 42,
        (byte) 122,
        (byte) 103,
        (byte) 66,
        (byte) 54,
        (byte) 112 /*0x70*/,
        (byte) 133,
        (byte) 9,
        (byte) 226,
        (byte) 17,
        (byte) 67,
        (byte) 43,
        (byte) 178,
        (byte) 140,
        (byte) 140,
        (byte) 13,
        (byte) 38,
        (byte) 223,
        (byte) 178,
        (byte) 182,
        (byte) 29,
        (byte) 40,
        (byte) 55,
        (byte) 200,
        (byte) 95,
        (byte) 123,
        (byte) 202,
        (byte) 103,
        (byte) 222,
        (byte) 7,
        (byte) 116,
        (byte) 221,
        (byte) 10,
        (byte) 193,
        (byte) 220,
        (byte) 226,
        (byte) 234,
        (byte) 159,
        (byte) 166,
        (byte) 20,
        (byte) 123,
        (byte) 32 /*0x20*/,
        (byte) 156,
        (byte) 67,
        (byte) 209,
        (byte) 135,
        (byte) 223,
        (byte) 61,
        (byte) 152
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 171,
        (byte) 8,
        (byte) 232,
        (byte) 111,
        (byte) 11,
        (byte) 180,
        (byte) 42,
        (byte) 100,
        (byte) 82,
        (byte) 206,
        (byte) 189,
        (byte) 115,
        (byte) 25,
        (byte) 252,
        (byte) 173,
        (byte) 36,
        (byte) 159,
        (byte) 122,
        (byte) 13,
        (byte) 88,
        (byte) 34,
        (byte) 231,
        (byte) 120,
        (byte) 28,
        (byte) 178,
        (byte) 202,
        (byte) 72,
        (byte) 151,
        (byte) 201,
        (byte) 78,
        (byte) 141,
        (byte) 196,
        (byte) 97,
        (byte) 132,
        (byte) 139,
        (byte) 69,
        (byte) 157,
        (byte) 23,
        (byte) 119,
        (byte) 65,
        (byte) 217,
        (byte) 109,
        (byte) 68,
        (byte) 230,
        (byte) 87,
        (byte) 224 /*0xE0*/,
        (byte) 114,
        (byte) 156,
        (byte) 254,
        (byte) 48 /*0x30*/,
        (byte) 212,
        byte.MaxValue,
        (byte) 176 /*0xB0*/,
        (byte) 181,
        (byte) 91
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 117,
        (byte) 127 /*0x7F*/,
        (byte) 159,
        (byte) 36,
        (byte) 2,
        (byte) 66,
        (byte) 158,
        (byte) 226,
        (byte) 186,
        (byte) 165,
        (byte) 30,
        (byte) 43,
        (byte) 219,
        (byte) 156,
        (byte) 102,
        (byte) 33,
        (byte) 241,
        (byte) 176 /*0xB0*/,
        (byte) 170,
        (byte) 35,
        (byte) 135,
        (byte) 56,
        (byte) 158,
        (byte) 59,
        (byte) 252,
        (byte) 139,
        (byte) 244,
        (byte) 247,
        (byte) 216,
        (byte) 156,
        (byte) 141,
        (byte) 206,
        (byte) 204,
        (byte) 2,
        byte.MaxValue,
        (byte) 94,
        (byte) 106,
        (byte) 156,
        (byte) 217,
        (byte) 159,
        (byte) 75,
        (byte) 188,
        (byte) 236,
        (byte) 110,
        (byte) 92,
        (byte) 245,
        (byte) 193,
        (byte) 166,
        (byte) 107,
        (byte) 102,
        (byte) 238,
        (byte) 140,
        (byte) 238,
        (byte) 238,
        (byte) 125
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 235,
        (byte) 129,
        (byte) 38,
        (byte) 229,
        (byte) 126,
        (byte) 117,
        (byte) 72,
        (byte) 213,
        (byte) 182,
        (byte) 180,
        (byte) 142,
        (byte) 38,
        (byte) 101,
        (byte) 245,
        (byte) 20,
        (byte) 37,
        (byte) 104,
        (byte) 111,
        (byte) 159,
        (byte) 129,
        (byte) 217,
        (byte) 60,
        (byte) 220,
        (byte) 208 /*0xD0*/,
        (byte) 208 /*0xD0*/,
        (byte) 114,
        (byte) 99,
        (byte) 200,
        (byte) 120,
        (byte) 133,
        (byte) 76,
        (byte) 73,
        (byte) 12,
        (byte) 96 /*0x60*/,
        (byte) 66,
        (byte) 16 /*0x10*/,
        (byte) 254,
        (byte) 124,
        (byte) 124,
        (byte) 224 /*0xE0*/,
        (byte) 89,
        (byte) 58,
        (byte) 68,
        (byte) 116,
        (byte) 47,
        (byte) 100,
        (byte) 173,
        (byte) 126,
        (byte) 172,
        (byte) 158,
        (byte) 82,
        (byte) 85,
        (byte) 250,
        (byte) 115,
        (byte) 153
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[29]
      {
        (byte) 100,
        (byte) 40,
        (byte) 118,
        (byte) 78,
        (byte) 167,
        (byte) 10,
        (byte) 30,
        (byte) 24,
        (byte) 80 /*0x50*/,
        (byte) 94,
        (byte) 217,
        (byte) 157,
        (byte) 8,
        (byte) 182,
        (byte) 84,
        (byte) 214,
        (byte) 11,
        (byte) 215,
        (byte) 202,
        (byte) 76,
        (byte) 48 /*0x30*/,
        (byte) 222,
        (byte) 7,
        (byte) 202,
        (byte) 77,
        (byte) 139,
        (byte) 113,
        (byte) 174,
        (byte) 0
      };
      byte[] numArray7 = new byte[29]
      {
        (byte) 238,
        (byte) 107,
        (byte) 16 /*0x10*/,
        (byte) 124,
        (byte) 233,
        (byte) 149,
        (byte) 244,
        (byte) 22,
        (byte) 9,
        (byte) 236,
        (byte) 231,
        (byte) 74,
        (byte) 143,
        (byte) 223,
        (byte) 243,
        (byte) 171,
        (byte) 87,
        (byte) 96 /*0x60*/,
        (byte) 106,
        (byte) 123,
        (byte) 124,
        (byte) 249,
        (byte) 233,
        (byte) 5,
        (byte) 114,
        (byte) 106,
        (byte) 233,
        (byte) 116,
        (byte) 211
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 29);
      for (int index = 0; index < 29; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[139];
    byte[] numArray9 = new byte[55]
    {
      (byte) 194,
      (byte) 125,
      (byte) 6,
      (byte) 116,
      (byte) 99,
      (byte) 81,
      (byte) 164,
      (byte) 178,
      (byte) 205,
      (byte) 85,
      (byte) 12,
      (byte) 151,
      (byte) 232,
      (byte) 9,
      (byte) 99,
      (byte) 176 /*0xB0*/,
      (byte) 153,
      (byte) 182,
      (byte) 226,
      (byte) 22,
      (byte) 178,
      (byte) 239,
      (byte) 175,
      (byte) 133,
      (byte) 32 /*0x20*/,
      (byte) 172,
      (byte) 0,
      (byte) 27,
      (byte) 152,
      (byte) 112 /*0x70*/,
      (byte) 24,
      (byte) 111,
      (byte) 128 /*0x80*/,
      (byte) 83,
      (byte) 218,
      (byte) 138,
      (byte) 175,
      (byte) 223,
      (byte) 115,
      (byte) 186,
      (byte) 51,
      (byte) 24,
      (byte) 126,
      (byte) 218,
      (byte) 210,
      (byte) 112 /*0x70*/,
      (byte) 226,
      (byte) 67,
      (byte) 40,
      (byte) 193,
      (byte) 20,
      (byte) 74,
      (byte) 253,
      (byte) 199,
      (byte) 210
    };
    byte[] numArray10 = new byte[55];
    numArray10[30] = (byte) 70;
    numArray10[13] = (byte) 8;
    numArray10[2] = (byte) 241;
    numArray10[41] = (byte) 55;
    numArray10[51] = (byte) 36;
    numArray10[26] = (byte) 117;
    numArray10[20] = (byte) 59;
    numArray10[3] = (byte) 208 /*0xD0*/;
    numArray10[8] = (byte) 54;
    numArray10[52] = (byte) 22;
    numArray10[5] = (byte) 14;
    numArray10[11] = (byte) 202;
    numArray10[12] = (byte) 106;
    numArray10[40] = (byte) 161;
    numArray10[50] = (byte) 143;
    numArray10[15] = (byte) 49;
    numArray10[16 /*0x10*/] = (byte) 89;
    numArray10[17] = (byte) 9;
    numArray10[18] = (byte) 121;
    numArray10[46] = (byte) 116;
    numArray10[4] = (byte) 219;
    numArray10[10] = (byte) 144 /*0x90*/;
    numArray10[22] = (byte) 189;
    numArray10[23] = (byte) 14;
    numArray10[24] = (byte) 4;
    numArray10[25] = (byte) 99;
    numArray10[42] = (byte) 211;
    numArray10[19] = (byte) 183;
    numArray10[31 /*0x1F*/] = (byte) 11;
    numArray10[29] = (byte) 195;
    numArray10[6] = (byte) 19;
    numArray10[34] = (byte) 128 /*0x80*/;
    numArray10[32 /*0x20*/] = (byte) 36;
    numArray10[33] = (byte) 251;
    numArray10[7] = (byte) 119;
    numArray10[35] = (byte) 44;
    numArray10[36] = (byte) 239;
    numArray10[1] = (byte) 24;
    numArray10[21] = (byte) 92;
    numArray10[39] = (byte) 81;
    numArray10[0] = (byte) 57;
    numArray10[38] = (byte) 197;
    numArray10[28] = (byte) 16 /*0x10*/;
    numArray10[44] = (byte) 153;
    numArray10[9] = (byte) 88;
    numArray10[45] = (byte) 128 /*0x80*/;
    numArray10[37] = (byte) 163;
    numArray10[47] = (byte) 226;
    numArray10[48 /*0x30*/] = (byte) 240 /*0xF0*/;
    numArray10[14] = (byte) 145;
    numArray10[49] = (byte) 79;
    numArray10[43] = (byte) 217;
    numArray10[27] = (byte) 162;
    numArray10[53] = (byte) 127 /*0x7F*/;
    numArray10[54] = (byte) 63 /*0x3F*/;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55];
    numArray11[18] = (byte) 155;
    numArray11[1] = (byte) 162;
    numArray11[2] = (byte) 237;
    numArray11[3] = (byte) 147;
    numArray11[4] = (byte) 149;
    numArray11[22] = (byte) 87;
    numArray11[21] = (byte) 124;
    numArray11[11] = (byte) 40;
    numArray11[54] = (byte) 193;
    numArray11[51] = (byte) 172;
    numArray11[25] = (byte) 162;
    numArray11[35] = (byte) 233;
    numArray11[40] = (byte) 30;
    numArray11[13] = (byte) 210;
    numArray11[14] = (byte) 45;
    numArray11[15] = (byte) 134;
    numArray11[16 /*0x10*/] = (byte) 191;
    numArray11[43] = (byte) 251;
    numArray11[17] = (byte) 55;
    numArray11[19] = (byte) 79;
    numArray11[36] = (byte) 206;
    numArray11[12] = (byte) 28;
    numArray11[34] = (byte) 95;
    numArray11[23] = (byte) 123;
    numArray11[7] = (byte) 111;
    numArray11[5] = (byte) 66;
    numArray11[39] = (byte) 187;
    numArray11[41] = (byte) 45;
    numArray11[28] = (byte) 198;
    numArray11[44] = (byte) 58;
    numArray11[30] = (byte) 212;
    numArray11[31 /*0x1F*/] = (byte) 72;
    numArray11[37] = (byte) 213;
    numArray11[33] = (byte) 195;
    numArray11[24] = (byte) 168;
    numArray11[32 /*0x20*/] = (byte) 163;
    numArray11[38] = (byte) 21;
    numArray11[10] = (byte) 19;
    numArray11[27] = (byte) 0;
    numArray11[29] = (byte) 156;
    numArray11[8] = (byte) 42;
    numArray11[20] = (byte) 55;
    numArray11[42] = (byte) 11;
    numArray11[9] = (byte) 94;
    numArray11[49] = (byte) 128 /*0x80*/;
    numArray11[45] = (byte) 235;
    numArray11[46] = (byte) 51;
    numArray11[47] = (byte) 239;
    numArray11[48 /*0x30*/] = (byte) 45;
    numArray11[26] = (byte) 21;
    numArray11[50] = (byte) 21;
    numArray11[6] = (byte) 2;
    numArray11[52] = (byte) 157;
    numArray11[53] = (byte) 19;
    numArray11[0] = (byte) 206;
    byte[] numArray12 = new byte[55]
    {
      (byte) 186,
      (byte) 239,
      (byte) 20,
      (byte) 110,
      (byte) 66,
      (byte) 242,
      (byte) 90,
      (byte) 110,
      (byte) 155,
      (byte) 94,
      (byte) 57,
      (byte) 77,
      (byte) 67,
      (byte) 183,
      (byte) 77,
      (byte) 207,
      (byte) 190,
      (byte) 28,
      (byte) 71,
      (byte) 156,
      (byte) 252,
      (byte) 139,
      (byte) 120,
      (byte) 250,
      (byte) 159,
      (byte) 89,
      (byte) 2,
      (byte) 197,
      (byte) 192 /*0xC0*/,
      (byte) 120,
      (byte) 226,
      (byte) 60,
      (byte) 128 /*0x80*/,
      (byte) 24,
      (byte) 38,
      (byte) 235,
      (byte) 97,
      (byte) 156,
      (byte) 163,
      (byte) 133,
      (byte) 204,
      (byte) 75,
      (byte) 98,
      (byte) 98,
      (byte) 220,
      (byte) 192 /*0xC0*/,
      (byte) 243,
      (byte) 153,
      (byte) 195,
      (byte) 109,
      (byte) 194,
      (byte) 40,
      (byte) 224 /*0xE0*/,
      (byte) 182,
      (byte) 221
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[29]
    {
      (byte) 50,
      (byte) 46,
      (byte) 131,
      (byte) 5,
      (byte) 46,
      (byte) 99,
      (byte) 6,
      (byte) 97,
      (byte) 32 /*0x20*/,
      (byte) 244,
      (byte) 85,
      (byte) 100,
      (byte) 165,
      (byte) 92,
      byte.MaxValue,
      (byte) 0,
      (byte) 80 /*0x50*/,
      (byte) 18,
      (byte) 133,
      (byte) 244,
      (byte) 235,
      (byte) 208 /*0xD0*/,
      (byte) 86,
      (byte) 148,
      (byte) 170,
      (byte) 234,
      (byte) 125,
      (byte) 91,
      (byte) 158
    };
    byte[] numArray14 = new byte[29];
    numArray14[9] = (byte) 125;
    numArray14[1] = (byte) 105;
    numArray14[22] = (byte) 184;
    numArray14[19] = (byte) 28;
    numArray14[4] = (byte) 217;
    numArray14[21] = (byte) 106;
    numArray14[11] = (byte) 197;
    numArray14[7] = (byte) 125;
    numArray14[8] = (byte) 159;
    numArray14[2] = (byte) 13;
    numArray14[3] = (byte) 18;
    numArray14[10] = (byte) 106;
    numArray14[0] = (byte) 138;
    numArray14[13] = (byte) 159;
    numArray14[14] = (byte) 12;
    numArray14[15] = (byte) 123;
    numArray14[16 /*0x10*/] = (byte) 142;
    numArray14[17] = (byte) 31 /*0x1F*/;
    numArray14[24] = (byte) 135;
    numArray14[28] = (byte) 42;
    numArray14[6] = (byte) 233;
    numArray14[18] = (byte) 3;
    numArray14[12] = (byte) 128 /*0x80*/;
    numArray14[23] = (byte) 13;
    numArray14[5] = (byte) 115;
    numArray14[20] = (byte) 152;
    numArray14[25] = (byte) 99;
    numArray14[27] = (byte) 131;
    numArray14[26] = (byte) 253;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 29);
    for (int index = 0; index < 29; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_12577()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[145];
      byte[] numArray2 = new byte[55]
      {
        (byte) 194,
        (byte) 50,
        (byte) 70,
        (byte) 10,
        (byte) 55,
        (byte) 246,
        (byte) 50,
        (byte) 68,
        (byte) 97,
        (byte) 199,
        (byte) 35,
        (byte) 100,
        (byte) 125,
        (byte) 218,
        (byte) 129,
        (byte) 227,
        (byte) 14,
        (byte) 145,
        (byte) 166,
        (byte) 17,
        (byte) 47,
        (byte) 20,
        (byte) 128 /*0x80*/,
        (byte) 221,
        (byte) 25,
        (byte) 65,
        (byte) 172,
        (byte) 200,
        (byte) 227,
        (byte) 175,
        (byte) 45,
        (byte) 171,
        (byte) 58,
        (byte) 204,
        (byte) 77,
        (byte) 28,
        (byte) 44,
        (byte) 122,
        (byte) 224 /*0xE0*/,
        (byte) 77,
        (byte) 57,
        (byte) 231,
        (byte) 40,
        (byte) 165,
        (byte) 172,
        (byte) 205,
        (byte) 110,
        (byte) 40,
        (byte) 224 /*0xE0*/,
        (byte) 244,
        (byte) 224 /*0xE0*/,
        (byte) 201,
        (byte) 157,
        (byte) 213,
        (byte) 122
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 250,
        (byte) 60,
        (byte) 60,
        (byte) 188,
        (byte) 212,
        (byte) 231,
        (byte) 225,
        (byte) 141,
        (byte) 35,
        (byte) 247,
        (byte) 242,
        (byte) 148,
        (byte) 22,
        (byte) 141,
        (byte) 53,
        (byte) 98,
        (byte) 4,
        (byte) 209,
        (byte) 88,
        (byte) 98,
        (byte) 246,
        (byte) 220,
        (byte) 113,
        (byte) 90,
        (byte) 146,
        (byte) 225,
        (byte) 164,
        (byte) 28,
        (byte) 100,
        (byte) 196,
        (byte) 248,
        (byte) 8,
        (byte) 18,
        (byte) 211,
        (byte) 110,
        (byte) 51,
        (byte) 217,
        (byte) 146,
        (byte) 72,
        (byte) 16 /*0x10*/,
        (byte) 57,
        (byte) 172,
        (byte) 137,
        (byte) 61,
        (byte) 169,
        (byte) 26,
        (byte) 11,
        (byte) 96 /*0x60*/,
        (byte) 5,
        (byte) 100,
        (byte) 22,
        (byte) 12,
        (byte) 0,
        (byte) 82,
        (byte) 20
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[14] = (byte) 148;
      numArray4[1] = (byte) 241;
      numArray4[2] = (byte) 245;
      numArray4[32 /*0x20*/] = (byte) 158;
      numArray4[40] = (byte) 117;
      numArray4[15] = (byte) 177;
      numArray4[27] = (byte) 206;
      numArray4[7] = (byte) 153;
      numArray4[8] = (byte) 120;
      numArray4[9] = (byte) 41;
      numArray4[50] = (byte) 67;
      numArray4[10] = (byte) 176 /*0xB0*/;
      numArray4[35] = (byte) 137;
      numArray4[13] = (byte) 183;
      numArray4[30] = (byte) 101;
      numArray4[49] = (byte) 24;
      numArray4[54] = (byte) 215;
      numArray4[12] = (byte) 100;
      numArray4[18] = (byte) 71;
      numArray4[31 /*0x1F*/] = (byte) 81;
      numArray4[5] = (byte) 96 /*0x60*/;
      numArray4[21] = (byte) 49;
      numArray4[22] = (byte) 75;
      numArray4[19] = (byte) 236;
      numArray4[47] = (byte) 60;
      numArray4[53] = (byte) 193;
      numArray4[26] = (byte) 238;
      numArray4[33] = (byte) 106;
      numArray4[42] = (byte) 52;
      numArray4[6] = (byte) 178;
      numArray4[3] = (byte) 73;
      numArray4[23] = (byte) 210;
      numArray4[24] = (byte) 131;
      numArray4[17] = (byte) 215;
      numArray4[28] = (byte) 99;
      numArray4[44] = (byte) 190;
      numArray4[0] = (byte) 104;
      numArray4[20] = (byte) 218;
      numArray4[38] = (byte) 207;
      numArray4[39] = (byte) 135;
      numArray4[29] = (byte) 14;
      numArray4[41] = (byte) 12;
      numArray4[16 /*0x10*/] = (byte) 89;
      numArray4[43] = (byte) 164;
      numArray4[37] = (byte) 3;
      numArray4[4] = (byte) 106;
      numArray4[46] = (byte) 123;
      numArray4[11] = (byte) 141;
      numArray4[48 /*0x30*/] = (byte) 76;
      numArray4[25] = (byte) 232;
      numArray4[34] = (byte) 195;
      numArray4[51] = (byte) 253;
      numArray4[52] = (byte) 51;
      numArray4[45] = (byte) 154;
      numArray4[36] = (byte) 234;
      byte[] numArray5 = new byte[55]
      {
        (byte) 2,
        (byte) 10,
        (byte) 25,
        (byte) 208 /*0xD0*/,
        (byte) 103,
        (byte) 28,
        (byte) 249,
        (byte) 31 /*0x1F*/,
        (byte) 79,
        (byte) 8,
        (byte) 68,
        (byte) 222,
        (byte) 253,
        (byte) 79,
        (byte) 18,
        (byte) 184,
        (byte) 142,
        (byte) 232,
        (byte) 53,
        (byte) 225,
        (byte) 16 /*0x10*/,
        (byte) 76,
        (byte) 249,
        (byte) 70,
        (byte) 23,
        (byte) 94,
        (byte) 160 /*0xA0*/,
        (byte) 107,
        (byte) 40,
        (byte) 20,
        (byte) 150,
        (byte) 81,
        (byte) 19,
        (byte) 83,
        (byte) 64 /*0x40*/,
        (byte) 0,
        (byte) 239,
        (byte) 249,
        (byte) 151,
        (byte) 212,
        (byte) 89,
        (byte) 86,
        (byte) 86,
        (byte) 174,
        (byte) 76,
        (byte) 53,
        (byte) 210,
        (byte) 185,
        (byte) 103,
        (byte) 74,
        (byte) 135,
        (byte) 19,
        (byte) 91,
        (byte) 54,
        (byte) 167
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[35];
      numArray6[30] = (byte) 145;
      numArray6[24] = (byte) 105;
      numArray6[1] = (byte) 220;
      numArray6[32 /*0x20*/] = (byte) 83;
      numArray6[16 /*0x10*/] = (byte) 171;
      numArray6[5] = (byte) 235;
      numArray6[10] = (byte) 103;
      numArray6[7] = (byte) 91;
      numArray6[23] = (byte) 234;
      numArray6[27] = (byte) 221;
      numArray6[25] = (byte) 64 /*0x40*/;
      numArray6[4] = (byte) 98;
      numArray6[18] = (byte) 248;
      numArray6[15] = (byte) 187;
      numArray6[11] = (byte) 102;
      numArray6[8] = (byte) 65;
      numArray6[3] = (byte) 191;
      numArray6[17] = (byte) 228;
      numArray6[19] = (byte) 50;
      numArray6[14] = (byte) 24;
      numArray6[20] = (byte) 198;
      numArray6[21] = (byte) 181;
      numArray6[22] = (byte) 60;
      numArray6[31 /*0x1F*/] = (byte) 136;
      numArray6[6] = (byte) 158;
      numArray6[34] = (byte) 184;
      numArray6[26] = (byte) 247;
      numArray6[0] = (byte) 15;
      numArray6[28] = (byte) 116;
      numArray6[29] = (byte) 171;
      numArray6[2] = (byte) 21;
      numArray6[12] = (byte) 40;
      numArray6[9] = (byte) 17;
      numArray6[33] = (byte) 227;
      numArray6[13] = (byte) 254;
      byte[] numArray7 = new byte[35];
      numArray7[25] = (byte) 204;
      numArray7[1] = (byte) 243;
      numArray7[2] = (byte) 227;
      numArray7[3] = (byte) 192 /*0xC0*/;
      numArray7[4] = (byte) 234;
      numArray7[24] = (byte) 10;
      numArray7[23] = (byte) 52;
      numArray7[8] = (byte) 91;
      numArray7[26] = (byte) 83;
      numArray7[9] = (byte) 213;
      numArray7[0] = (byte) 98;
      numArray7[12] = (byte) 108;
      numArray7[6] = (byte) 208 /*0xD0*/;
      numArray7[13] = (byte) 179;
      numArray7[7] = (byte) 17;
      numArray7[15] = (byte) 68;
      numArray7[11] = (byte) 246;
      numArray7[34] = (byte) 16 /*0x10*/;
      numArray7[18] = (byte) 93;
      numArray7[5] = (byte) 126;
      numArray7[10] = (byte) 244;
      numArray7[21] = (byte) 254;
      numArray7[22] = (byte) 93;
      numArray7[20] = (byte) 19;
      numArray7[16 /*0x10*/] = (byte) 70;
      numArray7[14] = (byte) 108;
      numArray7[32 /*0x20*/] = (byte) 66;
      numArray7[30] = (byte) 38;
      numArray7[29] = (byte) 22;
      numArray7[19] = (byte) 121;
      numArray7[28] = (byte) 41;
      numArray7[31 /*0x1F*/] = (byte) 35;
      numArray7[27] = (byte) 216;
      numArray7[33] = (byte) 231;
      numArray7[17] = (byte) 185;
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 35);
      for (int index = 0; index < 35; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[145];
    byte[] numArray9 = new byte[55]
    {
      byte.MaxValue,
      (byte) 245,
      (byte) 192 /*0xC0*/,
      (byte) 254,
      (byte) 89,
      (byte) 207,
      (byte) 251,
      (byte) 80 /*0x50*/,
      (byte) 183,
      (byte) 171,
      (byte) 176 /*0xB0*/,
      (byte) 85,
      (byte) 102,
      (byte) 197,
      (byte) 152,
      (byte) 231,
      (byte) 247,
      (byte) 118,
      (byte) 86,
      (byte) 169,
      (byte) 24,
      (byte) 155,
      (byte) 164,
      (byte) 16 /*0x10*/,
      (byte) 135,
      (byte) 149,
      (byte) 183,
      (byte) 82,
      (byte) 149,
      (byte) 236,
      (byte) 135,
      (byte) 115,
      (byte) 121,
      (byte) 117,
      (byte) 77,
      (byte) 181,
      (byte) 158,
      (byte) 193,
      (byte) 179,
      (byte) 47,
      (byte) 17,
      (byte) 166,
      (byte) 180,
      (byte) 241,
      (byte) 215,
      (byte) 197,
      (byte) 212,
      (byte) 9,
      (byte) 59,
      (byte) 38,
      (byte) 36,
      (byte) 62,
      (byte) 26,
      (byte) 7,
      (byte) 234
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 189,
      (byte) 47,
      (byte) 248,
      (byte) 32 /*0x20*/,
      (byte) 234,
      (byte) 151,
      (byte) 240 /*0xF0*/,
      (byte) 192 /*0xC0*/,
      (byte) 99,
      (byte) 99,
      (byte) 5,
      (byte) 88,
      (byte) 84,
      (byte) 212,
      (byte) 174,
      (byte) 4,
      (byte) 126,
      (byte) 111,
      (byte) 80 /*0x50*/,
      (byte) 57,
      (byte) 95,
      (byte) 209,
      (byte) 199,
      (byte) 218,
      (byte) 98,
      (byte) 217,
      (byte) 135,
      (byte) 253,
      (byte) 103,
      (byte) 167,
      (byte) 224 /*0xE0*/,
      (byte) 151,
      (byte) 38,
      (byte) 5,
      (byte) 39,
      (byte) 80 /*0x50*/,
      (byte) 130,
      (byte) 49,
      (byte) 72,
      (byte) 74,
      (byte) 228,
      (byte) 8,
      (byte) 252,
      (byte) 178,
      (byte) 56,
      (byte) 61,
      (byte) 15,
      (byte) 57,
      (byte) 65,
      (byte) 31 /*0x1F*/,
      (byte) 157,
      (byte) 63 /*0x3F*/,
      (byte) 175,
      (byte) 91,
      (byte) 18
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 176 /*0xB0*/,
      (byte) 119,
      (byte) 196,
      (byte) 24,
      (byte) 168,
      (byte) 186,
      (byte) 202,
      (byte) 36,
      (byte) 60,
      (byte) 219,
      (byte) 241,
      (byte) 119,
      (byte) 226,
      (byte) 73,
      (byte) 54,
      (byte) 195,
      (byte) 38,
      (byte) 151,
      (byte) 114,
      (byte) 231,
      (byte) 178,
      (byte) 56,
      (byte) 194,
      (byte) 192 /*0xC0*/,
      (byte) 173,
      (byte) 23,
      (byte) 111,
      (byte) 199,
      (byte) 73,
      (byte) 88,
      (byte) 113,
      (byte) 139,
      (byte) 233,
      (byte) 100,
      (byte) 237,
      (byte) 74,
      (byte) 239,
      (byte) 23,
      (byte) 105,
      (byte) 235,
      (byte) 233,
      (byte) 205,
      (byte) 154,
      (byte) 233,
      (byte) 227,
      (byte) 145,
      (byte) 89,
      (byte) 220,
      (byte) 238,
      (byte) 122,
      (byte) 243,
      (byte) 112 /*0x70*/,
      (byte) 120,
      (byte) 125,
      (byte) 158
    };
    byte[] numArray12 = new byte[55];
    numArray12[41] = (byte) 201;
    numArray12[53] = (byte) 190;
    numArray12[37] = (byte) 198;
    numArray12[50] = (byte) 228;
    numArray12[31 /*0x1F*/] = (byte) 200;
    numArray12[38] = (byte) 71;
    numArray12[18] = (byte) 10;
    numArray12[7] = (byte) 78;
    numArray12[8] = (byte) 148;
    numArray12[9] = (byte) 76;
    numArray12[10] = (byte) 184;
    numArray12[36] = (byte) 136;
    numArray12[45] = (byte) 70;
    numArray12[13] = (byte) 137;
    numArray12[14] = (byte) 198;
    numArray12[46] = (byte) 38;
    numArray12[1] = (byte) 116;
    numArray12[19] = (byte) 70;
    numArray12[24] = (byte) 37;
    numArray12[33] = (byte) 181;
    numArray12[20] = (byte) 131;
    numArray12[21] = (byte) 181;
    numArray12[47] = (byte) 58;
    numArray12[44] = (byte) 139;
    numArray12[6] = (byte) 0;
    numArray12[3] = (byte) 121;
    numArray12[26] = (byte) 210;
    numArray12[2] = (byte) 77;
    numArray12[42] = (byte) 2;
    numArray12[48 /*0x30*/] = (byte) 168;
    numArray12[30] = (byte) 114;
    numArray12[16 /*0x10*/] = (byte) 123;
    numArray12[17] = (byte) 139;
    numArray12[4] = (byte) 95;
    numArray12[34] = (byte) 133;
    numArray12[35] = (byte) 81;
    numArray12[43] = (byte) 46;
    numArray12[25] = (byte) 8;
    numArray12[28] = (byte) 225;
    numArray12[39] = (byte) 49;
    numArray12[40] = (byte) 129;
    numArray12[11] = (byte) 194;
    numArray12[32 /*0x20*/] = (byte) 226;
    numArray12[54] = (byte) 112 /*0x70*/;
    numArray12[0] = (byte) 231;
    numArray12[15] = (byte) 202;
    numArray12[5] = (byte) 218;
    numArray12[22] = (byte) 240 /*0xF0*/;
    numArray12[27] = (byte) 141;
    numArray12[49] = (byte) 164;
    numArray12[29] = (byte) 179;
    numArray12[51] = (byte) 145;
    numArray12[52] = (byte) 240 /*0xF0*/;
    numArray12[12] = (byte) 116;
    numArray12[23] = (byte) 193;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[35];
    numArray13[4] = (byte) 41;
    numArray13[20] = (byte) 89;
    numArray13[26] = (byte) 39;
    numArray13[3] = (byte) 183;
    numArray13[33] = (byte) 185;
    numArray13[5] = (byte) 13;
    numArray13[1] = (byte) 78;
    numArray13[7] = (byte) 104;
    numArray13[25] = (byte) 213;
    numArray13[9] = (byte) 34;
    numArray13[10] = (byte) 184;
    numArray13[30] = (byte) 236;
    numArray13[23] = (byte) 227;
    numArray13[11] = (byte) 228;
    numArray13[29] = (byte) 33;
    numArray13[12] = (byte) 86;
    numArray13[16 /*0x10*/] = (byte) 229;
    numArray13[17] = (byte) 23;
    numArray13[18] = (byte) 243;
    numArray13[19] = (byte) 231;
    numArray13[8] = (byte) 83;
    numArray13[34] = (byte) 197;
    numArray13[22] = (byte) 24;
    numArray13[21] = (byte) 96 /*0x60*/;
    numArray13[15] = (byte) 85;
    numArray13[6] = (byte) 127 /*0x7F*/;
    numArray13[32 /*0x20*/] = (byte) 157;
    numArray13[27] = (byte) 128 /*0x80*/;
    numArray13[24] = (byte) 17;
    numArray13[13] = (byte) 232;
    numArray13[28] = (byte) 190;
    numArray13[31 /*0x1F*/] = (byte) 12;
    numArray13[2] = (byte) 211;
    numArray13[14] = (byte) 98;
    numArray13[0] = (byte) 37;
    byte[] numArray14 = new byte[35]
    {
      (byte) 154,
      (byte) 246,
      (byte) 111,
      (byte) 165,
      (byte) 18,
      (byte) 179,
      (byte) 151,
      (byte) 172,
      (byte) 92,
      (byte) 179,
      (byte) 196,
      (byte) 114,
      (byte) 16 /*0x10*/,
      (byte) 200,
      (byte) 1,
      (byte) 169,
      (byte) 118,
      (byte) 251,
      (byte) 63 /*0x3F*/,
      (byte) 253,
      (byte) 215,
      (byte) 47,
      (byte) 202,
      (byte) 178,
      (byte) 9,
      (byte) 11,
      (byte) 116,
      (byte) 165,
      (byte) 89,
      (byte) 122,
      (byte) 165,
      (byte) 95,
      (byte) 127 /*0x7F*/,
      (byte) 99,
      (byte) 131
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 35);
    for (int index = 0; index < 35; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }
}
