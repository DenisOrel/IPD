// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12738
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12738
{
  private static byte[] sspq = new byte[108]
  {
    (byte) 155,
    (byte) 32 /*0x20*/,
    (byte) 216,
    (byte) 112 /*0x70*/,
    (byte) 189,
    (byte) 14,
    (byte) 46,
    (byte) 74,
    (byte) 220,
    (byte) 27,
    (byte) 193,
    (byte) 124,
    (byte) 147,
    (byte) 144 /*0x90*/,
    (byte) 195,
    (byte) 202,
    (byte) 31 /*0x1F*/,
    (byte) 213,
    (byte) 77,
    (byte) 88,
    (byte) 39,
    (byte) 147,
    (byte) 232,
    (byte) 226,
    (byte) 125,
    (byte) 29,
    (byte) 109,
    (byte) 173,
    (byte) 127 /*0x7F*/,
    (byte) 86,
    (byte) 26,
    (byte) 1,
    (byte) 220,
    (byte) 41,
    (byte) 234,
    (byte) 117,
    (byte) 103,
    (byte) 76,
    (byte) 212,
    (byte) 123,
    (byte) 87,
    (byte) 205,
    (byte) 111,
    (byte) 144 /*0x90*/,
    (byte) 71,
    (byte) 7,
    (byte) 92,
    (byte) 119,
    (byte) 153,
    (byte) 118,
    (byte) 21,
    (byte) 171,
    (byte) 124,
    (byte) 133,
    (byte) 122,
    (byte) 252,
    (byte) 115,
    (byte) 238,
    (byte) 249,
    (byte) 94,
    (byte) 232,
    (byte) 186,
    (byte) 130,
    (byte) 56,
    (byte) 80 /*0x50*/,
    (byte) 217,
    (byte) 185,
    (byte) 180,
    (byte) 242,
    (byte) 187,
    (byte) 215,
    (byte) 246,
    (byte) 233,
    (byte) 160 /*0xA0*/,
    (byte) 37,
    (byte) 172,
    (byte) 253,
    (byte) 180,
    (byte) 213,
    (byte) 185,
    (byte) 92,
    (byte) 186,
    (byte) 21,
    (byte) 1,
    (byte) 221,
    (byte) 77,
    (byte) 111,
    (byte) 65,
    (byte) 24,
    (byte) 119,
    (byte) 79,
    (byte) 231,
    (byte) 244,
    (byte) 95,
    (byte) 221,
    (byte) 213,
    (byte) 84,
    (byte) 118,
    (byte) 194,
    (byte) 202,
    (byte) 151,
    (byte) 237,
    (byte) 100,
    (byte) 16 /*0x10*/,
    (byte) 30,
    (byte) 156,
    (byte) 119,
    (byte) 48 /*0x30*/
  };
  private static byte[] sspr = new byte[108]
  {
    (byte) 234,
    (byte) 218,
    (byte) 193,
    (byte) 85,
    (byte) 228,
    (byte) 55,
    (byte) 2,
    (byte) 35,
    (byte) 86,
    (byte) 101,
    (byte) 90,
    (byte) 26,
    (byte) 236,
    (byte) 234,
    (byte) 183,
    (byte) 84,
    (byte) 79,
    (byte) 73,
    (byte) 242,
    (byte) 216,
    (byte) 249,
    (byte) 72,
    (byte) 182,
    (byte) 129,
    (byte) 71,
    (byte) 241,
    (byte) 76,
    (byte) 204,
    (byte) 29,
    (byte) 40,
    (byte) 131,
    (byte) 112 /*0x70*/,
    (byte) 184,
    (byte) 244,
    (byte) 39,
    (byte) 249,
    (byte) 209,
    (byte) 19,
    (byte) 19,
    (byte) 129,
    (byte) 114,
    (byte) 118,
    (byte) 77,
    (byte) 80 /*0x50*/,
    (byte) 195,
    (byte) 81,
    (byte) 177,
    (byte) 11,
    (byte) 217,
    (byte) 133,
    (byte) 244,
    (byte) 11,
    (byte) 9,
    (byte) 13,
    (byte) 179,
    (byte) 125,
    (byte) 89,
    (byte) 244,
    (byte) 208 /*0xD0*/,
    (byte) 247,
    (byte) 18,
    (byte) 131,
    (byte) 6,
    (byte) 179,
    (byte) 57,
    (byte) 245,
    (byte) 30,
    (byte) 114,
    (byte) 45,
    (byte) 11,
    (byte) 62,
    (byte) 120,
    (byte) 150,
    (byte) 129,
    (byte) 188,
    (byte) 33,
    (byte) 68,
    (byte) 196,
    (byte) 121,
    (byte) 181,
    (byte) 203,
    (byte) 118,
    (byte) 6,
    (byte) 138,
    (byte) 200,
    (byte) 54,
    (byte) 105,
    (byte) 210,
    (byte) 52,
    (byte) 41,
    (byte) 38,
    (byte) 108,
    (byte) 163,
    (byte) 219,
    (byte) 44,
    (byte) 81,
    (byte) 110,
    (byte) 33,
    (byte) 83,
    (byte) 155,
    (byte) 224 /*0xE0*/,
    (byte) 113,
    (byte) 63 /*0x3F*/,
    (byte) 24,
    (byte) 123,
    (byte) 193,
    (byte) 126,
    (byte) 78
  };

  internal static string ssp_appserver_12739()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[120];
      byte[] numArray2 = new byte[55];
      numArray2[32 /*0x20*/] = (byte) 35;
      numArray2[54] = (byte) 193;
      numArray2[35] = (byte) 95;
      numArray2[3] = (byte) 74;
      numArray2[24] = (byte) 1;
      numArray2[34] = (byte) 60;
      numArray2[1] = (byte) 254;
      numArray2[49] = (byte) 252;
      numArray2[8] = (byte) 103;
      numArray2[9] = (byte) 67;
      numArray2[10] = (byte) 231;
      numArray2[0] = (byte) 187;
      numArray2[17] = (byte) 119;
      numArray2[13] = (byte) 209;
      numArray2[14] = (byte) 105;
      numArray2[44] = (byte) 226;
      numArray2[50] = (byte) 197;
      numArray2[33] = (byte) 12;
      numArray2[7] = (byte) 17;
      numArray2[19] = (byte) 68;
      numArray2[39] = (byte) 124;
      numArray2[2] = (byte) 109;
      numArray2[28] = (byte) 104;
      numArray2[23] = (byte) 154;
      numArray2[4] = (byte) 169;
      numArray2[20] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 29;
      numArray2[11] = (byte) 21;
      numArray2[22] = (byte) 254;
      numArray2[29] = (byte) 72;
      numArray2[27] = (byte) 175;
      numArray2[31 /*0x1F*/] = (byte) 134;
      numArray2[26] = (byte) 97;
      numArray2[40] = (byte) 208 /*0xD0*/;
      numArray2[45] = (byte) 243;
      numArray2[6] = (byte) 168;
      numArray2[36] = (byte) 252;
      numArray2[41] = (byte) 244;
      numArray2[38] = (byte) 23;
      numArray2[21] = (byte) 113;
      numArray2[37] = (byte) 81;
      numArray2[18] = (byte) 68;
      numArray2[53] = (byte) 32 /*0x20*/;
      numArray2[16 /*0x10*/] = (byte) 107;
      numArray2[43] = (byte) 198;
      numArray2[30] = (byte) 27;
      numArray2[46] = (byte) 234;
      numArray2[47] = (byte) 88;
      numArray2[48 /*0x30*/] = (byte) 105;
      numArray2[15] = (byte) 195;
      numArray2[42] = (byte) 189;
      numArray2[51] = (byte) 186;
      numArray2[52] = (byte) 60;
      numArray2[12] = (byte) 118;
      numArray2[25] = (byte) 119;
      byte[] numArray3 = new byte[55];
      numArray3[14] = (byte) 75;
      numArray3[24] = (byte) 97;
      numArray3[42] = (byte) 204;
      numArray3[3] = (byte) 18;
      numArray3[17] = (byte) 195;
      numArray3[52] = (byte) 132;
      numArray3[1] = (byte) 254;
      numArray3[43] = (byte) 7;
      numArray3[8] = (byte) 158;
      numArray3[44] = (byte) 224 /*0xE0*/;
      numArray3[25] = (byte) 124;
      numArray3[11] = (byte) 205;
      numArray3[12] = (byte) 183;
      numArray3[13] = (byte) 143;
      numArray3[49] = (byte) 69;
      numArray3[15] = (byte) 187;
      numArray3[16 /*0x10*/] = (byte) 234;
      numArray3[33] = (byte) 79;
      numArray3[27] = (byte) 120;
      numArray3[19] = (byte) 144 /*0x90*/;
      numArray3[20] = (byte) 19;
      numArray3[36] = (byte) 114;
      numArray3[30] = (byte) 224 /*0xE0*/;
      numArray3[23] = (byte) 71;
      numArray3[54] = (byte) 26;
      numArray3[21] = (byte) 94;
      numArray3[48 /*0x30*/] = (byte) 58;
      numArray3[32 /*0x20*/] = (byte) 238;
      numArray3[34] = byte.MaxValue;
      numArray3[51] = (byte) 1;
      numArray3[0] = (byte) 240 /*0xF0*/;
      numArray3[31 /*0x1F*/] = (byte) 77;
      numArray3[38] = (byte) 20;
      numArray3[4] = (byte) 144 /*0x90*/;
      numArray3[5] = (byte) 76;
      numArray3[35] = (byte) 199;
      numArray3[46] = (byte) 184;
      numArray3[10] = (byte) 189;
      numArray3[29] = (byte) 170;
      numArray3[39] = (byte) 163;
      numArray3[40] = (byte) 22;
      numArray3[41] = (byte) 117;
      numArray3[28] = (byte) 37;
      numArray3[7] = (byte) 102;
      numArray3[6] = (byte) 212;
      numArray3[45] = (byte) 210;
      numArray3[9] = (byte) 120;
      numArray3[47] = (byte) 144 /*0x90*/;
      numArray3[37] = (byte) 177;
      numArray3[53] = (byte) 202;
      numArray3[50] = (byte) 153;
      numArray3[2] = (byte) 46;
      numArray3[22] = (byte) 147;
      numArray3[26] = (byte) 183;
      numArray3[18] = (byte) 201;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 226,
        (byte) 45,
        (byte) 152,
        (byte) 107,
        (byte) 203,
        (byte) 7,
        (byte) 197,
        (byte) 193,
        (byte) 65,
        (byte) 153,
        (byte) 9,
        (byte) 70,
        (byte) 17,
        (byte) 140,
        (byte) 65,
        (byte) 57,
        (byte) 247,
        (byte) 118,
        (byte) 51,
        (byte) 21,
        (byte) 196,
        (byte) 205,
        (byte) 251,
        (byte) 170,
        (byte) 38,
        (byte) 48 /*0x30*/,
        (byte) 157,
        (byte) 60,
        (byte) 38,
        (byte) 103,
        (byte) 69,
        (byte) 30,
        (byte) 221,
        (byte) 106,
        (byte) 46,
        (byte) 161,
        (byte) 167,
        (byte) 163,
        (byte) 89,
        (byte) 4,
        (byte) 59,
        (byte) 20,
        (byte) 40,
        (byte) 120,
        (byte) 88,
        (byte) 217,
        (byte) 165,
        (byte) 12,
        (byte) 89,
        (byte) 165,
        (byte) 117,
        (byte) 153,
        (byte) 193,
        (byte) 105,
        (byte) 104
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 188,
        (byte) 233,
        (byte) 32 /*0x20*/,
        (byte) 212,
        (byte) 165,
        (byte) 160 /*0xA0*/,
        (byte) 85,
        (byte) 143,
        (byte) 91,
        (byte) 170,
        (byte) 254,
        (byte) 117,
        (byte) 236,
        (byte) 49,
        (byte) 46,
        (byte) 137,
        (byte) 79,
        (byte) 178,
        (byte) 35,
        (byte) 17,
        (byte) 32 /*0x20*/,
        (byte) 10,
        (byte) 11,
        (byte) 3,
        (byte) 153,
        (byte) 67,
        (byte) 76,
        (byte) 209,
        (byte) 129,
        (byte) 163,
        (byte) 66,
        (byte) 108,
        (byte) 92,
        (byte) 106,
        (byte) 29,
        (byte) 214,
        (byte) 247,
        (byte) 97,
        (byte) 30,
        (byte) 245,
        (byte) 40,
        (byte) 100,
        (byte) 39,
        (byte) 73,
        (byte) 201,
        (byte) 235,
        (byte) 30,
        (byte) 194,
        (byte) 226,
        (byte) 204,
        (byte) 231,
        (byte) 131,
        (byte) 213,
        (byte) 32 /*0x20*/,
        (byte) 220
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[10];
      numArray6[5] = (byte) 155;
      numArray6[1] = (byte) 69;
      numArray6[2] = (byte) 155;
      numArray6[3] = (byte) 106;
      numArray6[4] = (byte) 137;
      numArray6[8] = (byte) 235;
      numArray6[0] = (byte) 238;
      numArray6[7] = (byte) 83;
      numArray6[9] = (byte) 104;
      numArray6[6] = (byte) 217;
      byte[] numArray7 = new byte[10]
      {
        (byte) 22,
        (byte) 237,
        (byte) 183,
        (byte) 102,
        (byte) 229,
        (byte) 66,
        (byte) 28,
        (byte) 249,
        (byte) 116,
        (byte) 189
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[120];
    byte[] numArray9 = new byte[55]
    {
      (byte) 138,
      (byte) 144 /*0x90*/,
      (byte) 226,
      (byte) 128 /*0x80*/,
      (byte) 242,
      (byte) 171,
      (byte) 92,
      (byte) 116,
      (byte) 2,
      (byte) 25,
      (byte) 110,
      (byte) 185,
      (byte) 93,
      (byte) 189,
      (byte) 154,
      (byte) 178,
      (byte) 120,
      (byte) 79,
      (byte) 36,
      (byte) 108,
      (byte) 112 /*0x70*/,
      (byte) 254,
      (byte) 174,
      (byte) 185,
      (byte) 19,
      (byte) 32 /*0x20*/,
      (byte) 124,
      (byte) 147,
      (byte) 143,
      (byte) 183,
      (byte) 112 /*0x70*/,
      (byte) 176 /*0xB0*/,
      (byte) 156,
      (byte) 248,
      (byte) 16 /*0x10*/,
      (byte) 218,
      (byte) 166,
      (byte) 26,
      (byte) 231,
      (byte) 168,
      (byte) 142,
      (byte) 244,
      (byte) 164,
      (byte) 68,
      (byte) 49,
      (byte) 216,
      (byte) 247,
      (byte) 96 /*0x60*/,
      (byte) 148,
      (byte) 39,
      (byte) 56,
      (byte) 85,
      (byte) 93,
      (byte) 229,
      (byte) 219
    };
    byte[] numArray10 = new byte[55];
    numArray10[0] = (byte) 101;
    numArray10[1] = (byte) 231;
    numArray10[53] = (byte) 252;
    numArray10[3] = (byte) 122;
    numArray10[24] = (byte) 139;
    numArray10[46] = (byte) 12;
    numArray10[6] = (byte) 59;
    numArray10[42] = (byte) 145;
    numArray10[54] = (byte) 226;
    numArray10[9] = (byte) 139;
    numArray10[10] = (byte) 174;
    numArray10[11] = (byte) 123;
    numArray10[23] = (byte) 77;
    numArray10[28] = (byte) 204;
    numArray10[17] = (byte) 171;
    numArray10[8] = (byte) 32 /*0x20*/;
    numArray10[16 /*0x10*/] = (byte) 206;
    numArray10[43] = (byte) 37;
    numArray10[33] = (byte) 147;
    numArray10[19] = (byte) 137;
    numArray10[20] = (byte) 220;
    numArray10[21] = (byte) 187;
    numArray10[22] = (byte) 212;
    numArray10[4] = (byte) 151;
    numArray10[15] = (byte) 247;
    numArray10[25] = (byte) 190;
    numArray10[34] = (byte) 229;
    numArray10[44] = (byte) 72;
    numArray10[27] = (byte) 111;
    numArray10[14] = (byte) 217;
    numArray10[2] = (byte) 245;
    numArray10[30] = (byte) 158;
    numArray10[32 /*0x20*/] = (byte) 228;
    numArray10[7] = (byte) 219;
    numArray10[49] = (byte) 75;
    numArray10[35] = (byte) 37;
    numArray10[36] = (byte) 99;
    numArray10[41] = (byte) 39;
    numArray10[13] = (byte) 79;
    numArray10[39] = (byte) 250;
    numArray10[40] = (byte) 50;
    numArray10[37] = (byte) 42;
    numArray10[5] = (byte) 155;
    numArray10[12] = (byte) 246;
    numArray10[48 /*0x30*/] = (byte) 16 /*0x10*/;
    numArray10[45] = (byte) 100;
    numArray10[26] = (byte) 110;
    numArray10[47] = (byte) 137;
    numArray10[18] = (byte) 35;
    numArray10[31 /*0x1F*/] = (byte) 130;
    numArray10[50] = (byte) 236;
    numArray10[51] = (byte) 53;
    numArray10[52] = (byte) 232;
    numArray10[38] = (byte) 253;
    numArray10[29] = (byte) 79;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 161,
      (byte) 48 /*0x30*/,
      (byte) 168,
      (byte) 19,
      (byte) 203,
      (byte) 124,
      (byte) 202,
      (byte) 140,
      (byte) 108,
      (byte) 102,
      (byte) 71,
      (byte) 40,
      (byte) 199,
      (byte) 143,
      (byte) 176 /*0xB0*/,
      (byte) 82,
      (byte) 2,
      (byte) 25,
      (byte) 108,
      byte.MaxValue,
      (byte) 205,
      (byte) 20,
      (byte) 129,
      (byte) 150,
      (byte) 104,
      (byte) 41,
      (byte) 77,
      (byte) 165,
      (byte) 46,
      (byte) 112 /*0x70*/,
      (byte) 176 /*0xB0*/,
      (byte) 189,
      (byte) 81,
      (byte) 128 /*0x80*/,
      (byte) 218,
      (byte) 125,
      (byte) 109,
      (byte) 102,
      (byte) 249,
      (byte) 147,
      (byte) 143,
      (byte) 16 /*0x10*/,
      (byte) 6,
      (byte) 83,
      (byte) 75,
      (byte) 139,
      (byte) 13,
      (byte) 139,
      (byte) 41,
      (byte) 154,
      (byte) 223,
      (byte) 236,
      (byte) 243,
      (byte) 50,
      (byte) 170
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 133,
      (byte) 148,
      (byte) 120,
      (byte) 231,
      (byte) 13,
      (byte) 160 /*0xA0*/,
      (byte) 130,
      (byte) 21,
      (byte) 45,
      (byte) 33,
      (byte) 97,
      (byte) 73,
      (byte) 111,
      (byte) 130,
      (byte) 68,
      (byte) 102,
      (byte) 70,
      (byte) 175,
      (byte) 248,
      (byte) 71,
      (byte) 242,
      (byte) 224 /*0xE0*/,
      (byte) 106,
      (byte) 174,
      (byte) 166,
      (byte) 201,
      (byte) 138,
      (byte) 222,
      (byte) 108,
      (byte) 10,
      (byte) 87,
      (byte) 46,
      (byte) 120,
      (byte) 67,
      (byte) 115,
      (byte) 183,
      (byte) 79,
      (byte) 243,
      (byte) 150,
      (byte) 164,
      (byte) 180,
      (byte) 135,
      (byte) 224 /*0xE0*/,
      (byte) 202,
      (byte) 174,
      (byte) 242,
      (byte) 26,
      (byte) 49,
      (byte) 186,
      (byte) 190,
      (byte) 230,
      (byte) 18,
      (byte) 134,
      (byte) 111,
      (byte) 178
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[10]
    {
      (byte) 17,
      (byte) 170,
      (byte) 162,
      (byte) 47,
      (byte) 37,
      (byte) 93,
      (byte) 252,
      (byte) 182,
      (byte) 137,
      (byte) 214
    };
    byte[] numArray14 = new byte[10]
    {
      (byte) 254,
      (byte) 166,
      (byte) 177,
      (byte) 147,
      (byte) 119,
      (byte) 21,
      (byte) 161,
      (byte) 68,
      (byte) 83,
      (byte) 151
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 10);
    for (int index = 0; index < 10; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[19];
    byte[] response = new byte[19];
    Array.Copy((Array) sc_12738.sspq, 0, (Array) numArray15, 0, 19);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12738.sspr, 0, (Array) numArray15, 0, 19);
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

  internal static string ssp_appserver_12740()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[148];
      byte[] numArray2 = new byte[55]
      {
        (byte) 91,
        (byte) 187,
        (byte) 86,
        (byte) 100,
        (byte) 105,
        (byte) 178,
        (byte) 9,
        (byte) 156,
        (byte) 193,
        (byte) 181,
        (byte) 197,
        (byte) 215,
        (byte) 246,
        (byte) 181,
        (byte) 67,
        (byte) 203,
        (byte) 26,
        (byte) 193,
        (byte) 237,
        (byte) 126,
        (byte) 129,
        (byte) 198,
        (byte) 206,
        (byte) 134,
        (byte) 110,
        (byte) 168,
        (byte) 201,
        (byte) 108,
        (byte) 243,
        (byte) 191,
        (byte) 182,
        (byte) 113,
        (byte) 102,
        (byte) 101,
        (byte) 137,
        (byte) 224 /*0xE0*/,
        (byte) 202,
        (byte) 116,
        (byte) 6,
        (byte) 10,
        (byte) 4,
        (byte) 145,
        (byte) 137,
        (byte) 38,
        (byte) 86,
        (byte) 233,
        (byte) 133,
        (byte) 222,
        (byte) 76,
        (byte) 247,
        (byte) 176 /*0xB0*/,
        (byte) 93,
        (byte) 232,
        (byte) 206,
        (byte) 76
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 174,
        (byte) 101,
        (byte) 153,
        (byte) 65,
        (byte) 32 /*0x20*/,
        (byte) 153,
        (byte) 130,
        (byte) 220,
        (byte) 236,
        (byte) 250,
        (byte) 40,
        (byte) 194,
        (byte) 28,
        (byte) 107,
        (byte) 93,
        (byte) 187,
        (byte) 74,
        (byte) 200,
        (byte) 1,
        (byte) 177,
        (byte) 245,
        (byte) 189,
        (byte) 218,
        (byte) 34,
        (byte) 109,
        (byte) 77,
        (byte) 187,
        (byte) 124,
        (byte) 174,
        (byte) 51,
        (byte) 96 /*0x60*/,
        (byte) 99,
        (byte) 22,
        (byte) 26,
        (byte) 100,
        (byte) 59,
        (byte) 3,
        (byte) 147,
        (byte) 160 /*0xA0*/,
        (byte) 253,
        (byte) 137,
        (byte) 235,
        (byte) 163,
        (byte) 64 /*0x40*/,
        (byte) 77,
        (byte) 233,
        (byte) 105,
        (byte) 205,
        (byte) 224 /*0xE0*/,
        (byte) 63 /*0x3F*/,
        (byte) 61,
        (byte) 124,
        (byte) 32 /*0x20*/,
        (byte) 14,
        (byte) 180
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[8] = (byte) 83;
      numArray4[1] = (byte) 245;
      numArray4[49] = (byte) 162;
      numArray4[3] = (byte) 232;
      numArray4[20] = (byte) 167;
      numArray4[4] = (byte) 157;
      numArray4[27] = (byte) 15;
      numArray4[7] = (byte) 254;
      numArray4[48 /*0x30*/] = (byte) 10;
      numArray4[9] = (byte) 112 /*0x70*/;
      numArray4[45] = (byte) 178;
      numArray4[39] = (byte) 231;
      numArray4[33] = (byte) 242;
      numArray4[52] = (byte) 92;
      numArray4[14] = (byte) 165;
      numArray4[42] = (byte) 2;
      numArray4[47] = (byte) 245;
      numArray4[40] = (byte) 135;
      numArray4[18] = (byte) 240 /*0xF0*/;
      numArray4[19] = (byte) 61;
      numArray4[44] = (byte) 244;
      numArray4[5] = (byte) 169;
      numArray4[22] = (byte) 132;
      numArray4[41] = (byte) 39;
      numArray4[24] = (byte) 235;
      numArray4[25] = (byte) 31 /*0x1F*/;
      numArray4[11] = (byte) 43;
      numArray4[2] = (byte) 193;
      numArray4[28] = (byte) 88;
      numArray4[29] = (byte) 137;
      numArray4[30] = (byte) 151;
      numArray4[31 /*0x1F*/] = (byte) 24;
      numArray4[13] = (byte) 1;
      numArray4[15] = (byte) 186;
      numArray4[34] = (byte) 249;
      numArray4[21] = (byte) 48 /*0x30*/;
      numArray4[36] = (byte) 69;
      numArray4[37] = (byte) 34;
      numArray4[38] = (byte) 103;
      numArray4[50] = (byte) 5;
      numArray4[0] = (byte) 208 /*0xD0*/;
      numArray4[17] = (byte) 121;
      numArray4[51] = (byte) 85;
      numArray4[54] = (byte) 69;
      numArray4[16 /*0x10*/] = (byte) 226;
      numArray4[43] = (byte) 120;
      numArray4[46] = (byte) 232;
      numArray4[10] = (byte) 145;
      numArray4[6] = (byte) 1;
      numArray4[32 /*0x20*/] = (byte) 245;
      numArray4[12] = (byte) 99;
      numArray4[26] = (byte) 251;
      numArray4[23] = (byte) 35;
      numArray4[53] = (byte) 86;
      numArray4[35] = (byte) 126;
      byte[] numArray5 = new byte[55]
      {
        (byte) 122,
        (byte) 225,
        (byte) 115,
        (byte) 6,
        (byte) 36,
        (byte) 241,
        (byte) 151,
        (byte) 203,
        (byte) 79,
        (byte) 147,
        (byte) 146,
        (byte) 5,
        (byte) 222,
        (byte) 196,
        (byte) 101,
        (byte) 140,
        (byte) 191,
        (byte) 31 /*0x1F*/,
        (byte) 46,
        (byte) 145,
        (byte) 92,
        (byte) 129,
        (byte) 13,
        (byte) 32 /*0x20*/,
        (byte) 169,
        (byte) 178,
        (byte) 30,
        (byte) 134,
        (byte) 177,
        (byte) 100,
        (byte) 218,
        (byte) 49,
        (byte) 240 /*0xF0*/,
        (byte) 192 /*0xC0*/,
        (byte) 123,
        (byte) 31 /*0x1F*/,
        (byte) 148,
        (byte) 129,
        (byte) 122,
        (byte) 248,
        (byte) 149,
        (byte) 142,
        (byte) 50,
        (byte) 232,
        (byte) 28,
        (byte) 91,
        (byte) 218,
        (byte) 134,
        (byte) 174,
        (byte) 140,
        (byte) 233,
        (byte) 0,
        (byte) 246,
        (byte) 32 /*0x20*/,
        (byte) 206
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[38];
      numArray6[29] = (byte) 103;
      numArray6[28] = (byte) 0;
      numArray6[2] = (byte) 71;
      numArray6[31 /*0x1F*/] = (byte) 230;
      numArray6[12] = (byte) 25;
      numArray6[30] = (byte) 253;
      numArray6[37] = (byte) 145;
      numArray6[7] = (byte) 149;
      numArray6[5] = (byte) 240 /*0xF0*/;
      numArray6[21] = (byte) 180;
      numArray6[22] = (byte) 187;
      numArray6[11] = (byte) 173;
      numArray6[3] = (byte) 168;
      numArray6[13] = (byte) 204;
      numArray6[8] = (byte) 189;
      numArray6[15] = (byte) 41;
      numArray6[16 /*0x10*/] = (byte) 206;
      numArray6[33] = (byte) 167;
      numArray6[18] = (byte) 253;
      numArray6[19] = (byte) 246;
      numArray6[20] = (byte) 107;
      numArray6[10] = (byte) 196;
      numArray6[4] = (byte) 237;
      numArray6[23] = (byte) 191;
      numArray6[24] = (byte) 24;
      numArray6[25] = (byte) 34;
      numArray6[26] = (byte) 136;
      numArray6[1] = (byte) 196;
      numArray6[6] = (byte) 70;
      numArray6[14] = (byte) 155;
      numArray6[17] = (byte) 32 /*0x20*/;
      numArray6[0] = (byte) 110;
      numArray6[32 /*0x20*/] = (byte) 232;
      numArray6[27] = (byte) 62;
      numArray6[34] = (byte) 57;
      numArray6[9] = (byte) 151;
      numArray6[36] = (byte) 161;
      numArray6[35] = (byte) 96 /*0x60*/;
      byte[] numArray7 = new byte[38]
      {
        (byte) 152,
        (byte) 58,
        (byte) 158,
        (byte) 180,
        (byte) 75,
        (byte) 173,
        (byte) 198,
        (byte) 126,
        (byte) 38,
        (byte) 181,
        (byte) 141,
        (byte) 41,
        (byte) 35,
        (byte) 219,
        (byte) 40,
        (byte) 192 /*0xC0*/,
        (byte) 203,
        (byte) 17,
        (byte) 30,
        (byte) 194,
        (byte) 198,
        (byte) 23,
        (byte) 35,
        (byte) 230,
        (byte) 2,
        (byte) 147,
        (byte) 246,
        (byte) 37,
        (byte) 94,
        (byte) 91,
        (byte) 1,
        (byte) 108,
        (byte) 33,
        (byte) 144 /*0x90*/,
        (byte) 139,
        (byte) 218,
        (byte) 80 /*0x50*/,
        (byte) 33
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 38);
      for (int index = 0; index < 38; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[30];
      byte[] response = new byte[30];
      Array.Copy((Array) sc_12738.sspq, 19, (Array) numArray8, 0, 30);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_12738.sspr, 19, (Array) numArray8, 0, 30);
      for (int index = 0; index < numArray8.Length; ++index)
      {
        if ((int) numArray8[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray9 = new byte[148];
    byte[] numArray10 = new byte[55]
    {
      (byte) 195,
      (byte) 51,
      (byte) 122,
      (byte) 183,
      (byte) 114,
      (byte) 64 /*0x40*/,
      (byte) 116,
      (byte) 173,
      (byte) 115,
      (byte) 27,
      (byte) 192 /*0xC0*/,
      (byte) 86,
      (byte) 196,
      (byte) 20,
      (byte) 228,
      (byte) 109,
      (byte) 60,
      (byte) 156,
      (byte) 66,
      (byte) 25,
      (byte) 235,
      (byte) 143,
      (byte) 137,
      (byte) 56,
      (byte) 93,
      (byte) 1,
      (byte) 89,
      (byte) 36,
      (byte) 87,
      (byte) 154,
      (byte) 118,
      (byte) 193,
      (byte) 246,
      (byte) 20,
      (byte) 188,
      (byte) 35,
      (byte) 160 /*0xA0*/,
      (byte) 74,
      (byte) 106,
      (byte) 232,
      (byte) 117,
      (byte) 200,
      (byte) 243,
      (byte) 48 /*0x30*/,
      (byte) 20,
      (byte) 36,
      (byte) 146,
      (byte) 184,
      (byte) 139,
      (byte) 35,
      (byte) 11,
      (byte) 107,
      (byte) 26,
      (byte) 93,
      (byte) 174
    };
    byte[] numArray11 = new byte[55]
    {
      (byte) 118,
      (byte) 210,
      (byte) 224 /*0xE0*/,
      (byte) 207,
      (byte) 122,
      (byte) 198,
      (byte) 82,
      (byte) 206,
      (byte) 65,
      (byte) 197,
      (byte) 254,
      (byte) 59,
      (byte) 96 /*0x60*/,
      (byte) 232,
      (byte) 201,
      (byte) 227,
      (byte) 92,
      (byte) 84,
      (byte) 186,
      (byte) 244,
      (byte) 149,
      (byte) 31 /*0x1F*/,
      (byte) 135,
      (byte) 230,
      (byte) 36,
      (byte) 156,
      (byte) 200,
      (byte) 98,
      (byte) 136,
      (byte) 251,
      (byte) 109,
      (byte) 139,
      (byte) 47,
      (byte) 130,
      (byte) 61,
      (byte) 156,
      (byte) 154,
      (byte) 157,
      (byte) 56,
      (byte) 118,
      (byte) 63 /*0x3F*/,
      (byte) 7,
      (byte) 58,
      (byte) 172,
      (byte) 36,
      (byte) 31 /*0x1F*/,
      (byte) 81,
      (byte) 155,
      (byte) 35,
      (byte) 74,
      (byte) 213,
      (byte) 160 /*0xA0*/,
      (byte) 36,
      (byte) 71,
      (byte) 14
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 66,
      (byte) 90,
      (byte) 113,
      (byte) 231,
      (byte) 80 /*0x50*/,
      (byte) 85,
      (byte) 217,
      (byte) 164,
      byte.MaxValue,
      (byte) 96 /*0x60*/,
      (byte) 176 /*0xB0*/,
      (byte) 88,
      (byte) 192 /*0xC0*/,
      (byte) 204,
      (byte) 241,
      (byte) 49,
      (byte) 89,
      (byte) 164,
      (byte) 34,
      (byte) 156,
      (byte) 89,
      (byte) 35,
      (byte) 57,
      (byte) 110,
      (byte) 160 /*0xA0*/,
      (byte) 153,
      (byte) 51,
      (byte) 142,
      (byte) 11,
      (byte) 166,
      (byte) 153,
      (byte) 102,
      (byte) 136,
      (byte) 122,
      (byte) 94,
      (byte) 240 /*0xF0*/,
      (byte) 248,
      (byte) 1,
      (byte) 37,
      (byte) 114,
      (byte) 26,
      (byte) 140,
      (byte) 21,
      (byte) 196,
      (byte) 226,
      (byte) 96 /*0x60*/,
      (byte) 202,
      (byte) 86,
      (byte) 25,
      (byte) 57,
      (byte) 193,
      (byte) 184,
      (byte) 139,
      (byte) 150,
      (byte) 80 /*0x50*/
    };
    byte[] numArray13 = new byte[55];
    numArray13[9] = (byte) 17;
    numArray13[50] = (byte) 90;
    numArray13[6] = (byte) 189;
    numArray13[43] = (byte) 222;
    numArray13[4] = (byte) 57;
    numArray13[28] = (byte) 250;
    numArray13[20] = (byte) 145;
    numArray13[14] = (byte) 173;
    numArray13[5] = (byte) 141;
    numArray13[17] = (byte) 48 /*0x30*/;
    numArray13[10] = (byte) 99;
    numArray13[11] = (byte) 235;
    numArray13[0] = byte.MaxValue;
    numArray13[51] = (byte) 22;
    numArray13[35] = (byte) 2;
    numArray13[24] = (byte) 81;
    numArray13[16 /*0x10*/] = (byte) 197;
    numArray13[33] = (byte) 22;
    numArray13[29] = (byte) 85;
    numArray13[3] = (byte) 116;
    numArray13[2] = (byte) 185;
    numArray13[21] = (byte) 213;
    numArray13[22] = (byte) 125;
    numArray13[23] = (byte) 87;
    numArray13[48 /*0x30*/] = (byte) 26;
    numArray13[25] = (byte) 215;
    numArray13[18] = (byte) 16 /*0x10*/;
    numArray13[27] = (byte) 62;
    numArray13[44] = (byte) 46;
    numArray13[40] = (byte) 186;
    numArray13[30] = (byte) 123;
    numArray13[31 /*0x1F*/] = (byte) 151;
    numArray13[32 /*0x20*/] = (byte) 252;
    numArray13[8] = (byte) 111;
    numArray13[34] = (byte) 225;
    numArray13[41] = (byte) 151;
    numArray13[36] = (byte) 46;
    numArray13[39] = (byte) 253;
    numArray13[38] = (byte) 141;
    numArray13[47] = (byte) 63 /*0x3F*/;
    numArray13[13] = (byte) 173;
    numArray13[37] = (byte) 26;
    numArray13[42] = (byte) 218;
    numArray13[26] = (byte) 83;
    numArray13[19] = (byte) 25;
    numArray13[45] = (byte) 198;
    numArray13[49] = (byte) 251;
    numArray13[12] = (byte) 12;
    numArray13[52] = (byte) 138;
    numArray13[7] = (byte) 142;
    numArray13[46] = (byte) 55;
    numArray13[1] = (byte) 166;
    numArray13[15] = (byte) 40;
    numArray13[53] = (byte) 10;
    numArray13[54] = (byte) 194;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[38]
    {
      (byte) 119,
      (byte) 5,
      (byte) 232,
      (byte) 69,
      (byte) 213,
      (byte) 198,
      (byte) 115,
      (byte) 229,
      (byte) 111,
      (byte) 221,
      (byte) 28,
      (byte) 46,
      (byte) 50,
      (byte) 67,
      (byte) 155,
      (byte) 16 /*0x10*/,
      (byte) 96 /*0x60*/,
      (byte) 177,
      (byte) 217,
      (byte) 81,
      (byte) 119,
      (byte) 243,
      (byte) 174,
      (byte) 74,
      (byte) 244,
      (byte) 92,
      (byte) 210,
      (byte) 122,
      (byte) 166,
      (byte) 0,
      (byte) 221,
      (byte) 166,
      (byte) 48 /*0x30*/,
      (byte) 35,
      (byte) 125,
      (byte) 181,
      (byte) 134,
      (byte) 10
    };
    byte[] numArray15 = new byte[38];
    numArray15[1] = (byte) 114;
    numArray15[8] = (byte) 231;
    numArray15[2] = (byte) 237;
    numArray15[3] = (byte) 235;
    numArray15[4] = (byte) 179;
    numArray15[34] = (byte) 223;
    numArray15[20] = (byte) 4;
    numArray15[7] = (byte) 126;
    numArray15[0] = (byte) 174;
    numArray15[27] = (byte) 30;
    numArray15[10] = (byte) 111;
    numArray15[30] = (byte) 119;
    numArray15[37] = (byte) 57;
    numArray15[31 /*0x1F*/] = (byte) 163;
    numArray15[14] = (byte) 99;
    numArray15[26] = (byte) 2;
    numArray15[16 /*0x10*/] = (byte) 237;
    numArray15[36] = (byte) 81;
    numArray15[18] = (byte) 15;
    numArray15[35] = (byte) 100;
    numArray15[13] = (byte) 220;
    numArray15[21] = (byte) 11;
    numArray15[22] = (byte) 242;
    numArray15[12] = (byte) 147;
    numArray15[24] = (byte) 243;
    numArray15[25] = (byte) 236;
    numArray15[29] = (byte) 158;
    numArray15[5] = (byte) 167;
    numArray15[28] = (byte) 227;
    numArray15[6] = (byte) 136;
    numArray15[9] = (byte) 204;
    numArray15[19] = (byte) 139;
    numArray15[32 /*0x20*/] = (byte) 120;
    numArray15[17] = (byte) 164;
    numArray15[23] = (byte) 86;
    numArray15[15] = (byte) 150;
    numArray15[11] = (byte) 130;
    numArray15[33] = (byte) 6;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 38);
    for (int index = 0; index < 38; ++index)
      numArray9[index + 110] ^= numArray15[index];
    byte[] numArray16 = new byte[37];
    byte[] response1 = new byte[37];
    Array.Copy((Array) sc_12738.sspq, 49, (Array) numArray16, 0, 37);
    key.Query(true, 335, numArray16, response1);
    Array.Copy((Array) sc_12738.sspr, 49, (Array) numArray16, 0, 37);
    for (int index = 0; index < numArray16.Length; ++index)
    {
      if ((int) numArray16[index] != (int) response1[index])
      {
        key.TagValue = (int) response1[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_12741()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[12] = (byte) 134;
      numArray2[10] = (byte) 115;
      numArray2[4] = (byte) 31 /*0x1F*/;
      numArray2[3] = (byte) 138;
      numArray2[0] = (byte) 234;
      numArray2[5] = (byte) 203;
      numArray2[6] = (byte) 134;
      numArray2[11] = (byte) 184;
      numArray2[8] = (byte) 155;
      numArray2[7] = (byte) 161;
      numArray2[1] = (byte) 133;
      numArray2[9] = (byte) 51;
      numArray2[14] = (byte) 70;
      numArray2[2] = (byte) 120;
      numArray2[13] = (byte) 16 /*0x10*/;
      byte[] numArray3 = new byte[15];
      numArray3[13] = (byte) 36;
      numArray3[4] = (byte) 62;
      numArray3[14] = (byte) 93;
      numArray3[5] = (byte) 57;
      numArray3[8] = (byte) 190;
      numArray3[9] = (byte) 121;
      numArray3[6] = (byte) 174;
      numArray3[12] = (byte) 127 /*0x7F*/;
      numArray3[7] = (byte) 4;
      numArray3[2] = (byte) 209;
      numArray3[10] = (byte) 205;
      numArray3[11] = (byte) 248;
      numArray3[1] = (byte) 58;
      numArray3[3] = (byte) 203;
      numArray3[0] = (byte) 101;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 21,
      (byte) 201,
      (byte) 148,
      (byte) 98,
      (byte) 190,
      (byte) 139,
      (byte) 230,
      (byte) 138,
      (byte) 184,
      (byte) 121,
      (byte) 203,
      (byte) 6,
      (byte) 139,
      (byte) 131,
      (byte) 115
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 111,
      (byte) 49,
      (byte) 58,
      (byte) 236,
      (byte) 201,
      (byte) 84,
      (byte) 160 /*0xA0*/,
      (byte) 169,
      (byte) 11,
      (byte) 184,
      (byte) 224 /*0xE0*/,
      (byte) 3,
      (byte) 152,
      (byte) 58,
      (byte) 74
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[22];
    byte[] response = new byte[22];
    Array.Copy((Array) sc_12738.sspq, 86, (Array) numArray7, 0, 22);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12738.sspr, 86, (Array) numArray7, 0, 22);
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

  internal static string ssp_appserver_12742()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[174];
      byte[] numArray2 = new byte[55]
      {
        (byte) 117,
        byte.MaxValue,
        (byte) 101,
        (byte) 177,
        (byte) 158,
        (byte) 223,
        (byte) 139,
        (byte) 147,
        (byte) 197,
        (byte) 112 /*0x70*/,
        (byte) 70,
        (byte) 223,
        (byte) 103,
        (byte) 253,
        (byte) 81,
        (byte) 69,
        (byte) 123,
        (byte) 202,
        (byte) 44,
        (byte) 9,
        (byte) 180,
        (byte) 99,
        (byte) 3,
        (byte) 9,
        (byte) 59,
        (byte) 244,
        (byte) 150,
        (byte) 15,
        (byte) 230,
        (byte) 149,
        (byte) 124,
        (byte) 61,
        (byte) 63 /*0x3F*/,
        (byte) 252,
        (byte) 162,
        (byte) 120,
        (byte) 234,
        (byte) 54,
        (byte) 85,
        (byte) 127 /*0x7F*/,
        (byte) 81,
        (byte) 65,
        (byte) 143,
        (byte) 5,
        (byte) 123,
        (byte) 235,
        (byte) 48 /*0x30*/,
        (byte) 239,
        (byte) 18,
        (byte) 202,
        (byte) 162,
        (byte) 209,
        (byte) 75,
        (byte) 38,
        (byte) 144 /*0x90*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 93;
      numArray3[14] = (byte) 96 /*0x60*/;
      numArray3[2] = (byte) 45;
      numArray3[3] = (byte) 208 /*0xD0*/;
      numArray3[4] = (byte) 253;
      numArray3[52] = (byte) 12;
      numArray3[6] = (byte) 216;
      numArray3[48 /*0x30*/] = (byte) 62;
      numArray3[8] = (byte) 233;
      numArray3[9] = (byte) 45;
      numArray3[12] = (byte) 198;
      numArray3[11] = (byte) 62;
      numArray3[49] = (byte) 87;
      numArray3[13] = (byte) 165;
      numArray3[20] = (byte) 228;
      numArray3[7] = (byte) 162;
      numArray3[16 /*0x10*/] = (byte) 134;
      numArray3[17] = (byte) 206;
      numArray3[41] = (byte) 119;
      numArray3[19] = (byte) 26;
      numArray3[25] = (byte) 94;
      numArray3[24] = (byte) 70;
      numArray3[40] = (byte) 214;
      numArray3[18] = (byte) 132;
      numArray3[39] = (byte) 193;
      numArray3[23] = (byte) 180;
      numArray3[15] = (byte) 125;
      numArray3[27] = (byte) 234;
      numArray3[28] = (byte) 207;
      numArray3[29] = (byte) 152;
      numArray3[47] = (byte) 123;
      numArray3[31 /*0x1F*/] = (byte) 15;
      numArray3[46] = (byte) 102;
      numArray3[33] = (byte) 153;
      numArray3[34] = (byte) 106;
      numArray3[10] = (byte) 19;
      numArray3[21] = (byte) 34;
      numArray3[37] = (byte) 109;
      numArray3[44] = (byte) 15;
      numArray3[1] = (byte) 43;
      numArray3[50] = (byte) 61;
      numArray3[36] = (byte) 174;
      numArray3[42] = (byte) 252;
      numArray3[43] = (byte) 72;
      numArray3[26] = (byte) 0;
      numArray3[30] = (byte) 35;
      numArray3[38] = (byte) 197;
      numArray3[5] = (byte) 103;
      numArray3[22] = (byte) 175;
      numArray3[45] = (byte) 19;
      numArray3[0] = (byte) 151;
      numArray3[35] = (byte) 241;
      numArray3[32 /*0x20*/] = (byte) 155;
      numArray3[53] = (byte) 251;
      numArray3[54] = (byte) 229;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 206,
        (byte) 38,
        (byte) 230,
        (byte) 161,
        (byte) 127 /*0x7F*/,
        (byte) 146,
        (byte) 62,
        (byte) 72,
        (byte) 23,
        (byte) 134,
        byte.MaxValue,
        (byte) 203,
        (byte) 94,
        (byte) 204,
        (byte) 132,
        (byte) 202,
        (byte) 116,
        (byte) 226,
        (byte) 222,
        (byte) 117,
        (byte) 235,
        (byte) 27,
        (byte) 3,
        byte.MaxValue,
        (byte) 18,
        (byte) 195,
        (byte) 171,
        (byte) 148,
        (byte) 78,
        (byte) 82,
        (byte) 169,
        (byte) 77,
        (byte) 81,
        (byte) 123,
        (byte) 192 /*0xC0*/,
        (byte) 154,
        (byte) 196,
        (byte) 149,
        (byte) 85,
        (byte) 1,
        (byte) 32 /*0x20*/,
        (byte) 201,
        (byte) 90,
        (byte) 197,
        (byte) 196,
        (byte) 201,
        (byte) 218,
        (byte) 163,
        (byte) 217,
        (byte) 145,
        (byte) 42,
        (byte) 141,
        (byte) 62,
        (byte) 201,
        (byte) 20
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 158,
        (byte) 194,
        (byte) 173,
        (byte) 185,
        (byte) 219,
        (byte) 59,
        (byte) 129,
        (byte) 12,
        (byte) 4,
        (byte) 132,
        (byte) 104,
        (byte) 34,
        (byte) 129,
        (byte) 68,
        (byte) 7,
        (byte) 88,
        (byte) 224 /*0xE0*/,
        (byte) 213,
        (byte) 92,
        (byte) 184,
        (byte) 144 /*0x90*/,
        (byte) 219,
        (byte) 160 /*0xA0*/,
        (byte) 107,
        (byte) 43,
        (byte) 149,
        (byte) 114,
        (byte) 252,
        (byte) 246,
        (byte) 245,
        (byte) 56,
        (byte) 243,
        (byte) 140,
        (byte) 29,
        (byte) 169,
        (byte) 20,
        (byte) 90,
        (byte) 14,
        (byte) 99,
        (byte) 28,
        (byte) 79,
        (byte) 84,
        (byte) 36,
        (byte) 32 /*0x20*/,
        (byte) 111,
        (byte) 244,
        (byte) 245,
        (byte) 100,
        (byte) 174,
        (byte) 61,
        (byte) 249,
        (byte) 244,
        (byte) 163,
        (byte) 40,
        (byte) 95
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[12] = (byte) 234;
      numArray6[18] = (byte) 34;
      numArray6[2] = (byte) 187;
      numArray6[5] = (byte) 249;
      numArray6[50] = (byte) 97;
      numArray6[6] = (byte) 197;
      numArray6[51] = (byte) 116;
      numArray6[7] = (byte) 221;
      numArray6[49] = (byte) 0;
      numArray6[9] = (byte) 121;
      numArray6[24] = (byte) 8;
      numArray6[23] = (byte) 233;
      numArray6[22] = (byte) 72;
      numArray6[13] = (byte) 5;
      numArray6[26] = (byte) 189;
      numArray6[10] = (byte) 226;
      numArray6[16 /*0x10*/] = (byte) 221;
      numArray6[15] = (byte) 140;
      numArray6[42] = (byte) 18;
      numArray6[52] = (byte) 241;
      numArray6[40] = (byte) 148;
      numArray6[21] = (byte) 58;
      numArray6[34] = (byte) 222;
      numArray6[14] = (byte) 222;
      numArray6[17] = (byte) 84;
      numArray6[33] = (byte) 120;
      numArray6[27] = (byte) 39;
      numArray6[1] = (byte) 143;
      numArray6[4] = (byte) 169;
      numArray6[29] = (byte) 14;
      numArray6[30] = (byte) 136;
      numArray6[11] = (byte) 12;
      numArray6[32 /*0x20*/] = (byte) 13;
      numArray6[35] = (byte) 6;
      numArray6[43] = (byte) 79;
      numArray6[53] = (byte) 240 /*0xF0*/;
      numArray6[36] = (byte) 153;
      numArray6[19] = (byte) 193;
      numArray6[38] = (byte) 118;
      numArray6[8] = (byte) 178;
      numArray6[20] = (byte) 62;
      numArray6[41] = (byte) 231;
      numArray6[3] = (byte) 230;
      numArray6[28] = (byte) 28;
      numArray6[44] = (byte) 248;
      numArray6[45] = (byte) 150;
      numArray6[46] = (byte) 239;
      numArray6[37] = (byte) 26;
      numArray6[48 /*0x30*/] = (byte) 75;
      numArray6[39] = (byte) 207;
      numArray6[25] = (byte) 186;
      numArray6[47] = (byte) 139;
      numArray6[31 /*0x1F*/] = (byte) 103;
      numArray6[0] = (byte) 230;
      numArray6[54] = (byte) 227;
      byte[] numArray7 = new byte[55]
      {
        (byte) 55,
        (byte) 177,
        (byte) 10,
        (byte) 131,
        (byte) 198,
        (byte) 225,
        (byte) 94,
        (byte) 166,
        (byte) 30,
        (byte) 164,
        (byte) 136,
        (byte) 144 /*0x90*/,
        (byte) 254,
        (byte) 236,
        (byte) 163,
        (byte) 117,
        (byte) 2,
        (byte) 175,
        (byte) 212,
        (byte) 243,
        (byte) 121,
        (byte) 93,
        (byte) 41,
        (byte) 72,
        (byte) 43,
        (byte) 204,
        (byte) 27,
        (byte) 42,
        (byte) 168,
        (byte) 207,
        (byte) 67,
        (byte) 167,
        (byte) 175,
        (byte) 111,
        (byte) 241,
        (byte) 101,
        (byte) 158,
        (byte) 70,
        (byte) 127 /*0x7F*/,
        (byte) 148,
        (byte) 252,
        (byte) 61,
        (byte) 111,
        (byte) 96 /*0x60*/,
        (byte) 161,
        (byte) 82,
        (byte) 239,
        (byte) 56,
        (byte) 254,
        (byte) 241,
        (byte) 41,
        (byte) 204,
        (byte) 31 /*0x1F*/,
        (byte) 68,
        (byte) 135
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[9]
      {
        (byte) 225,
        (byte) 109,
        (byte) 35,
        (byte) 10,
        (byte) 193,
        (byte) 166,
        (byte) 246,
        (byte) 221,
        (byte) 4
      };
      byte[] numArray9 = new byte[9];
      numArray9[6] = (byte) 219;
      numArray9[3] = (byte) 86;
      numArray9[2] = (byte) 221;
      numArray9[5] = (byte) 84;
      numArray9[4] = (byte) 177;
      numArray9[8] = (byte) 237;
      numArray9[1] = (byte) 27;
      numArray9[7] = (byte) 175;
      numArray9[0] = (byte) 202;
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 9);
      for (int index = 0; index < 9; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[174];
    byte[] numArray11 = new byte[55]
    {
      (byte) 115,
      (byte) 217,
      (byte) 78,
      (byte) 205,
      (byte) 104,
      (byte) 13,
      (byte) 12,
      (byte) 125,
      (byte) 214,
      (byte) 89,
      (byte) 7,
      (byte) 173,
      (byte) 115,
      (byte) 229,
      (byte) 207,
      (byte) 100,
      (byte) 136,
      (byte) 52,
      (byte) 128 /*0x80*/,
      (byte) 120,
      (byte) 129,
      (byte) 120,
      (byte) 181,
      (byte) 159,
      (byte) 109,
      (byte) 91,
      (byte) 225,
      (byte) 26,
      (byte) 64 /*0x40*/,
      (byte) 223,
      (byte) 140,
      (byte) 92,
      (byte) 203,
      (byte) 179,
      (byte) 110,
      (byte) 147,
      (byte) 212,
      (byte) 60,
      (byte) 89,
      (byte) 152,
      (byte) 233,
      (byte) 95,
      (byte) 45,
      (byte) 136,
      (byte) 214,
      (byte) 72,
      (byte) 164,
      (byte) 70,
      (byte) 182,
      (byte) 241,
      (byte) 54,
      (byte) 2,
      (byte) 176 /*0xB0*/,
      (byte) 63 /*0x3F*/,
      (byte) 24
    };
    byte[] numArray12 = new byte[55];
    numArray12[23] = (byte) 2;
    numArray12[1] = (byte) 7;
    numArray12[14] = (byte) 101;
    numArray12[7] = (byte) 27;
    numArray12[4] = (byte) 192 /*0xC0*/;
    numArray12[38] = (byte) 142;
    numArray12[6] = (byte) 91;
    numArray12[20] = (byte) 112 /*0x70*/;
    numArray12[8] = (byte) 68;
    numArray12[9] = (byte) 116;
    numArray12[19] = (byte) 201;
    numArray12[46] = (byte) 123;
    numArray12[12] = (byte) 115;
    numArray12[13] = (byte) 61;
    numArray12[35] = (byte) 139;
    numArray12[5] = (byte) 116;
    numArray12[16 /*0x10*/] = (byte) 14;
    numArray12[32 /*0x20*/] = (byte) 86;
    numArray12[48 /*0x30*/] = (byte) 204;
    numArray12[18] = (byte) 30;
    numArray12[34] = (byte) 83;
    numArray12[22] = (byte) 197;
    numArray12[3] = (byte) 76;
    numArray12[37] = (byte) 240 /*0xF0*/;
    numArray12[24] = (byte) 172;
    numArray12[25] = (byte) 208 /*0xD0*/;
    numArray12[26] = (byte) 72;
    numArray12[53] = (byte) 47;
    numArray12[28] = (byte) 1;
    numArray12[43] = (byte) 45;
    numArray12[30] = (byte) 230;
    numArray12[31 /*0x1F*/] = (byte) 0;
    numArray12[47] = (byte) 234;
    numArray12[21] = (byte) 17;
    numArray12[33] = (byte) 236;
    numArray12[42] = (byte) 42;
    numArray12[17] = (byte) 127 /*0x7F*/;
    numArray12[10] = (byte) 254;
    numArray12[45] = (byte) 94;
    numArray12[39] = (byte) 249;
    numArray12[40] = (byte) 162;
    numArray12[41] = (byte) 200;
    numArray12[15] = (byte) 197;
    numArray12[49] = (byte) 24;
    numArray12[44] = (byte) 0;
    numArray12[51] = (byte) 30;
    numArray12[36] = (byte) 89;
    numArray12[27] = (byte) 207;
    numArray12[11] = (byte) 135;
    numArray12[2] = (byte) 92;
    numArray12[50] = (byte) 102;
    numArray12[0] = (byte) 13;
    numArray12[52] = (byte) 141;
    numArray12[29] = (byte) 0;
    numArray12[54] = (byte) 250;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55];
    numArray13[17] = (byte) 151;
    numArray13[36] = (byte) 246;
    numArray13[2] = (byte) 85;
    numArray13[3] = (byte) 146;
    numArray13[4] = (byte) 142;
    numArray13[29] = (byte) 229;
    numArray13[16 /*0x10*/] = (byte) 124;
    numArray13[7] = (byte) 67;
    numArray13[11] = (byte) 3;
    numArray13[0] = (byte) 230;
    numArray13[45] = (byte) 204;
    numArray13[49] = (byte) 152;
    numArray13[8] = (byte) 253;
    numArray13[13] = (byte) 106;
    numArray13[6] = (byte) 77;
    numArray13[15] = (byte) 142;
    numArray13[52] = (byte) 247;
    numArray13[39] = (byte) 51;
    numArray13[40] = (byte) 233;
    numArray13[31 /*0x1F*/] = (byte) 163;
    numArray13[20] = (byte) 113;
    numArray13[47] = (byte) 246;
    numArray13[12] = (byte) 204;
    numArray13[14] = (byte) 110;
    numArray13[24] = (byte) 202;
    numArray13[25] = (byte) 233;
    numArray13[1] = (byte) 58;
    numArray13[43] = (byte) 16 /*0x10*/;
    numArray13[28] = (byte) 97;
    numArray13[27] = (byte) 189;
    numArray13[41] = (byte) 120;
    numArray13[33] = (byte) 120;
    numArray13[9] = (byte) 85;
    numArray13[32 /*0x20*/] = (byte) 196;
    numArray13[34] = (byte) 63 /*0x3F*/;
    numArray13[35] = (byte) 162;
    numArray13[22] = (byte) 191;
    numArray13[21] = (byte) 156;
    numArray13[38] = (byte) 104;
    numArray13[26] = (byte) 237;
    numArray13[19] = (byte) 19;
    numArray13[5] = (byte) 238;
    numArray13[42] = (byte) 47;
    numArray13[37] = (byte) 217;
    numArray13[44] = (byte) 191;
    numArray13[18] = byte.MaxValue;
    numArray13[23] = (byte) 210;
    numArray13[10] = (byte) 37;
    numArray13[48 /*0x30*/] = (byte) 124;
    numArray13[30] = (byte) 219;
    numArray13[50] = (byte) 75;
    numArray13[51] = (byte) 24;
    numArray13[46] = (byte) 174;
    numArray13[53] = (byte) 155;
    numArray13[54] = (byte) 133;
    byte[] numArray14 = new byte[55];
    numArray14[13] = (byte) 134;
    numArray14[28] = (byte) 211;
    numArray14[2] = (byte) 41;
    numArray14[48 /*0x30*/] = (byte) 188;
    numArray14[4] = (byte) 217;
    numArray14[5] = (byte) 85;
    numArray14[44] = (byte) 117;
    numArray14[15] = (byte) 254;
    numArray14[26] = (byte) 123;
    numArray14[9] = (byte) 170;
    numArray14[10] = (byte) 190;
    numArray14[30] = (byte) 205;
    numArray14[12] = (byte) 20;
    numArray14[18] = (byte) 161;
    numArray14[14] = (byte) 122;
    numArray14[43] = (byte) 81;
    numArray14[16 /*0x10*/] = (byte) 243;
    numArray14[17] = (byte) 208 /*0xD0*/;
    numArray14[49] = (byte) 233;
    numArray14[24] = (byte) 58;
    numArray14[0] = (byte) 180;
    numArray14[27] = (byte) 78;
    numArray14[36] = (byte) 174;
    numArray14[23] = (byte) 242;
    numArray14[19] = (byte) 205;
    numArray14[20] = (byte) 105;
    numArray14[41] = (byte) 1;
    numArray14[46] = (byte) 237;
    numArray14[40] = (byte) 91;
    numArray14[29] = (byte) 54;
    numArray14[8] = (byte) 158;
    numArray14[31 /*0x1F*/] = (byte) 226;
    numArray14[32 /*0x20*/] = (byte) 211;
    numArray14[33] = (byte) 17;
    numArray14[34] = (byte) 161;
    numArray14[35] = (byte) 133;
    numArray14[52] = (byte) 7;
    numArray14[37] = (byte) 22;
    numArray14[39] = (byte) 237;
    numArray14[6] = (byte) 142;
    numArray14[22] = (byte) 6;
    numArray14[42] = (byte) 156;
    numArray14[38] = (byte) 232;
    numArray14[25] = (byte) 252;
    numArray14[21] = (byte) 203;
    numArray14[45] = (byte) 56;
    numArray14[11] = (byte) 231;
    numArray14[47] = (byte) 185;
    numArray14[7] = (byte) 237;
    numArray14[1] = (byte) 28;
    numArray14[50] = (byte) 254;
    numArray14[51] = (byte) 220;
    numArray14[3] = (byte) 236;
    numArray14[53] = (byte) 49;
    numArray14[54] = (byte) 188;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[48 /*0x30*/] = (byte) 203;
    numArray15[25] = (byte) 135;
    numArray15[4] = (byte) 189;
    numArray15[50] = (byte) 31 /*0x1F*/;
    numArray15[33] = (byte) 165;
    numArray15[5] = (byte) 81;
    numArray15[6] = (byte) 139;
    numArray15[7] = (byte) 221;
    numArray15[51] = (byte) 215;
    numArray15[38] = (byte) 76;
    numArray15[10] = (byte) 248;
    numArray15[11] = (byte) 29;
    numArray15[12] = (byte) 75;
    numArray15[23] = (byte) 229;
    numArray15[36] = (byte) 205;
    numArray15[8] = (byte) 86;
    numArray15[3] = (byte) 224 /*0xE0*/;
    numArray15[17] = (byte) 200;
    numArray15[47] = (byte) 188;
    numArray15[14] = (byte) 73;
    numArray15[49] = (byte) 104;
    numArray15[9] = (byte) 95;
    numArray15[22] = (byte) 252;
    numArray15[21] = (byte) 110;
    numArray15[24] = (byte) 150;
    numArray15[27] = (byte) 221;
    numArray15[26] = (byte) 234;
    numArray15[45] = (byte) 119;
    numArray15[34] = (byte) 105;
    numArray15[13] = (byte) 32 /*0x20*/;
    numArray15[1] = (byte) 86;
    numArray15[31 /*0x1F*/] = (byte) 61;
    numArray15[32 /*0x20*/] = (byte) 147;
    numArray15[0] = (byte) 154;
    numArray15[28] = (byte) 92;
    numArray15[35] = (byte) 242;
    numArray15[15] = (byte) 20;
    numArray15[37] = (byte) 70;
    numArray15[40] = (byte) 12;
    numArray15[39] = (byte) 44;
    numArray15[46] = (byte) 190;
    numArray15[41] = (byte) 3;
    numArray15[52] = (byte) 67;
    numArray15[43] = (byte) 22;
    numArray15[44] = (byte) 2;
    numArray15[29] = (byte) 237;
    numArray15[30] = (byte) 24;
    numArray15[16 /*0x10*/] = (byte) 114;
    numArray15[42] = (byte) 17;
    numArray15[19] = (byte) 190;
    numArray15[54] = (byte) 126;
    numArray15[2] = (byte) 138;
    numArray15[20] = (byte) 54;
    numArray15[53] = (byte) 199;
    numArray15[18] = (byte) 229;
    byte[] numArray16 = new byte[55]
    {
      (byte) 169,
      (byte) 163,
      (byte) 106,
      (byte) 234,
      (byte) 152,
      (byte) 193,
      (byte) 152,
      (byte) 163,
      (byte) 239,
      (byte) 69,
      (byte) 75,
      (byte) 197,
      (byte) 253,
      (byte) 181,
      (byte) 211,
      (byte) 75,
      (byte) 226,
      (byte) 234,
      (byte) 84,
      (byte) 215,
      (byte) 87,
      (byte) 254,
      (byte) 155,
      (byte) 94,
      (byte) 126,
      (byte) 238,
      (byte) 36,
      (byte) 105,
      (byte) 10,
      (byte) 37,
      (byte) 225,
      (byte) 113,
      (byte) 231,
      (byte) 242,
      (byte) 174,
      (byte) 112 /*0x70*/,
      (byte) 128 /*0x80*/,
      (byte) 123,
      (byte) 228,
      (byte) 202,
      (byte) 202,
      (byte) 96 /*0x60*/,
      (byte) 250,
      (byte) 162,
      (byte) 236,
      (byte) 32 /*0x20*/,
      (byte) 145,
      (byte) 108,
      (byte) 218,
      (byte) 198,
      (byte) 226,
      (byte) 13,
      (byte) 167,
      (byte) 206,
      (byte) 144 /*0x90*/
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[9];
    numArray17[7] = (byte) 5;
    numArray17[8] = (byte) 193;
    numArray17[4] = (byte) 156;
    numArray17[3] = (byte) 55;
    numArray17[1] = (byte) 43;
    numArray17[5] = (byte) 139;
    numArray17[6] = (byte) 154;
    numArray17[2] = (byte) 107;
    numArray17[0] = (byte) 194;
    byte[] numArray18 = new byte[9]
    {
      (byte) 132,
      (byte) 181,
      (byte) 202,
      (byte) 184,
      (byte) 84,
      (byte) 95,
      (byte) 161,
      (byte) 251,
      (byte) 57
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 9);
    for (int index = 0; index < 9; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }
}
