// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13274
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13274
{
  private static byte[] sspq = new byte[77]
  {
    (byte) 199,
    (byte) 229,
    (byte) 191,
    (byte) 5,
    (byte) 175,
    (byte) 216,
    (byte) 101,
    (byte) 115,
    (byte) 34,
    (byte) 60,
    (byte) 196,
    (byte) 208 /*0xD0*/,
    (byte) 64 /*0x40*/,
    (byte) 141,
    (byte) 6,
    (byte) 154,
    (byte) 13,
    (byte) 18,
    (byte) 89,
    (byte) 95,
    (byte) 130,
    (byte) 169,
    (byte) 72,
    (byte) 75,
    (byte) 209,
    (byte) 169,
    (byte) 188,
    (byte) 203,
    (byte) 173,
    (byte) 83,
    (byte) 0,
    (byte) 251,
    (byte) 113,
    (byte) 144 /*0x90*/,
    (byte) 68,
    (byte) 192 /*0xC0*/,
    (byte) 38,
    (byte) 149,
    (byte) 102,
    (byte) 134,
    (byte) 187,
    (byte) 197,
    (byte) 21,
    (byte) 63 /*0x3F*/,
    (byte) 46,
    (byte) 71,
    (byte) 68,
    (byte) 136,
    (byte) 96 /*0x60*/,
    (byte) 63 /*0x3F*/,
    (byte) 49,
    (byte) 46,
    (byte) 59,
    (byte) 129,
    (byte) 234,
    (byte) 223,
    (byte) 221,
    (byte) 197,
    (byte) 202,
    (byte) 250,
    (byte) 149,
    (byte) 40,
    (byte) 158,
    (byte) 223,
    (byte) 129,
    (byte) 104,
    (byte) 169,
    (byte) 18,
    (byte) 107,
    (byte) 169,
    (byte) 152,
    (byte) 144 /*0x90*/,
    (byte) 109,
    (byte) 179,
    (byte) 69,
    (byte) 46,
    (byte) 151
  };
  private static byte[] sspr = new byte[77]
  {
    (byte) 197,
    (byte) 244,
    (byte) 151,
    (byte) 153,
    (byte) 211,
    (byte) 41,
    (byte) 214,
    (byte) 175,
    (byte) 5,
    (byte) 104,
    (byte) 100,
    (byte) 41,
    (byte) 218,
    (byte) 164,
    (byte) 223,
    (byte) 141,
    (byte) 138,
    (byte) 78,
    (byte) 149,
    (byte) 91,
    (byte) 95,
    (byte) 121,
    (byte) 0,
    (byte) 230,
    (byte) 28,
    (byte) 82,
    (byte) 216,
    (byte) 25,
    (byte) 44,
    (byte) 89,
    (byte) 25,
    (byte) 222,
    (byte) 211,
    (byte) 182,
    (byte) 216,
    (byte) 66,
    (byte) 121,
    (byte) 171,
    (byte) 89,
    (byte) 60,
    (byte) 80 /*0x50*/,
    (byte) 151,
    (byte) 86,
    (byte) 219,
    (byte) 136,
    (byte) 4,
    (byte) 10,
    (byte) 196,
    (byte) 186,
    (byte) 191,
    (byte) 129,
    (byte) 2,
    (byte) 96 /*0x60*/,
    (byte) 186,
    (byte) 101,
    (byte) 175,
    (byte) 193,
    (byte) 8,
    (byte) 183,
    (byte) 111,
    (byte) 82,
    (byte) 227,
    (byte) 211,
    (byte) 55,
    (byte) 136,
    (byte) 63 /*0x3F*/,
    (byte) 106,
    (byte) 104,
    (byte) 236,
    (byte) 2,
    (byte) 144 /*0x90*/,
    (byte) 107,
    (byte) 170,
    (byte) 124,
    (byte) 229,
    (byte) 23,
    (byte) 78
  };

  internal static string ssp_appserver_13275()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[219];
      byte[] numArray2 = new byte[55];
      numArray2[8] = (byte) 2;
      numArray2[1] = (byte) 151;
      numArray2[14] = (byte) 73;
      numArray2[37] = (byte) 152;
      numArray2[4] = (byte) 1;
      numArray2[5] = (byte) 27;
      numArray2[6] = (byte) 214;
      numArray2[7] = (byte) 178;
      numArray2[41] = (byte) 238;
      numArray2[9] = (byte) 36;
      numArray2[10] = (byte) 76;
      numArray2[11] = (byte) 15;
      numArray2[17] = (byte) 65;
      numArray2[13] = (byte) 130;
      numArray2[43] = (byte) 16 /*0x10*/;
      numArray2[3] = (byte) 11;
      numArray2[23] = (byte) 33;
      numArray2[0] = (byte) 85;
      numArray2[18] = (byte) 154;
      numArray2[19] = (byte) 119;
      numArray2[20] = (byte) 193;
      numArray2[16 /*0x10*/] = (byte) 247;
      numArray2[25] = (byte) 230;
      numArray2[47] = (byte) 37;
      numArray2[28] = (byte) 16 /*0x10*/;
      numArray2[26] = (byte) 3;
      numArray2[46] = (byte) 171;
      numArray2[52] = (byte) 87;
      numArray2[24] = (byte) 162;
      numArray2[54] = (byte) 85;
      numArray2[30] = (byte) 90;
      numArray2[31 /*0x1F*/] = (byte) 32 /*0x20*/;
      numArray2[50] = (byte) 90;
      numArray2[33] = (byte) 206;
      numArray2[21] = (byte) 188;
      numArray2[35] = (byte) 66;
      numArray2[2] = (byte) 173;
      numArray2[29] = (byte) 6;
      numArray2[38] = (byte) 52;
      numArray2[15] = (byte) 3;
      numArray2[40] = (byte) 151;
      numArray2[36] = (byte) 213;
      numArray2[42] = (byte) 29;
      numArray2[27] = (byte) 194;
      numArray2[22] = (byte) 39;
      numArray2[53] = (byte) 39;
      numArray2[32 /*0x20*/] = (byte) 72;
      numArray2[39] = (byte) 24;
      numArray2[48 /*0x30*/] = (byte) 222;
      numArray2[44] = (byte) 12;
      numArray2[49] = (byte) 91;
      numArray2[51] = (byte) 49;
      numArray2[45] = (byte) 72;
      numArray2[12] = (byte) 251;
      numArray2[34] = (byte) 141;
      byte[] numArray3 = new byte[55]
      {
        (byte) 7,
        (byte) 203,
        (byte) 178,
        (byte) 42,
        (byte) 181,
        (byte) 60,
        (byte) 102,
        (byte) 171,
        (byte) 39,
        (byte) 125,
        (byte) 112 /*0x70*/,
        (byte) 132,
        (byte) 84,
        (byte) 172,
        (byte) 112 /*0x70*/,
        (byte) 170,
        (byte) 235,
        (byte) 161,
        (byte) 126,
        (byte) 193,
        (byte) 224 /*0xE0*/,
        (byte) 203,
        (byte) 198,
        (byte) 240 /*0xF0*/,
        (byte) 214,
        (byte) 196,
        (byte) 159,
        (byte) 62,
        (byte) 244,
        (byte) 233,
        (byte) 40,
        (byte) 232,
        (byte) 140,
        (byte) 40,
        (byte) 173,
        (byte) 34,
        (byte) 152,
        (byte) 24,
        (byte) 97,
        (byte) 124,
        (byte) 87,
        (byte) 51,
        (byte) 13,
        (byte) 0,
        (byte) 159,
        (byte) 138,
        (byte) 126,
        (byte) 52,
        (byte) 104,
        (byte) 96 /*0x60*/,
        (byte) 136,
        (byte) 83,
        (byte) 149,
        (byte) 181,
        (byte) 83
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 118,
        (byte) 14,
        (byte) 222,
        (byte) 58,
        (byte) 46,
        (byte) 237,
        (byte) 80 /*0x50*/,
        (byte) 159,
        (byte) 100,
        (byte) 165,
        (byte) 180,
        (byte) 0,
        (byte) 52,
        (byte) 228,
        (byte) 46,
        (byte) 242,
        (byte) 130,
        (byte) 91,
        (byte) 133,
        (byte) 125,
        (byte) 206,
        (byte) 53,
        (byte) 215,
        (byte) 189,
        (byte) 174,
        (byte) 13,
        (byte) 135,
        (byte) 176 /*0xB0*/,
        (byte) 5,
        (byte) 242,
        (byte) 217,
        (byte) 246,
        (byte) 250,
        (byte) 120,
        (byte) 211,
        (byte) 33,
        (byte) 59,
        (byte) 197,
        (byte) 85,
        (byte) 94,
        (byte) 208 /*0xD0*/,
        (byte) 167,
        (byte) 123,
        (byte) 220,
        (byte) 201,
        (byte) 47,
        (byte) 163,
        (byte) 72,
        (byte) 205,
        (byte) 49,
        (byte) 84,
        (byte) 235,
        (byte) 252,
        (byte) 125,
        (byte) 139
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 175,
        (byte) 246,
        (byte) 127 /*0x7F*/,
        (byte) 160 /*0xA0*/,
        (byte) 33,
        (byte) 2,
        (byte) 178,
        (byte) 34,
        (byte) 54,
        (byte) 202,
        (byte) 178,
        (byte) 254,
        (byte) 152,
        (byte) 87,
        (byte) 149,
        (byte) 103,
        (byte) 16 /*0x10*/,
        (byte) 223,
        (byte) 231,
        (byte) 228,
        (byte) 6,
        (byte) 230,
        (byte) 21,
        (byte) 38,
        (byte) 121,
        (byte) 8,
        (byte) 6,
        (byte) 26,
        (byte) 79,
        (byte) 171,
        (byte) 69,
        (byte) 90,
        (byte) 232,
        (byte) 245,
        (byte) 157,
        (byte) 13,
        (byte) 81,
        (byte) 244,
        (byte) 71,
        (byte) 168,
        (byte) 132,
        (byte) 6,
        (byte) 124,
        (byte) 124,
        (byte) 195,
        (byte) 167,
        (byte) 125,
        (byte) 47,
        (byte) 93,
        (byte) 91,
        (byte) 117,
        (byte) 250,
        (byte) 136,
        (byte) 119,
        (byte) 97
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55];
      numArray6[37] = (byte) 248;
      numArray6[29] = (byte) 249;
      numArray6[7] = (byte) 204;
      numArray6[3] = (byte) 102;
      numArray6[32 /*0x20*/] = (byte) 147;
      numArray6[5] = (byte) 154;
      numArray6[53] = (byte) 134;
      numArray6[20] = (byte) 251;
      numArray6[1] = (byte) 140;
      numArray6[30] = (byte) 125;
      numArray6[10] = (byte) 187;
      numArray6[11] = (byte) 17;
      numArray6[4] = (byte) 160 /*0xA0*/;
      numArray6[31 /*0x1F*/] = (byte) 237;
      numArray6[49] = (byte) 228;
      numArray6[18] = (byte) 39;
      numArray6[28] = (byte) 237;
      numArray6[17] = (byte) 172;
      numArray6[26] = (byte) 211;
      numArray6[9] = (byte) 6;
      numArray6[6] = (byte) 113;
      numArray6[27] = (byte) 158;
      numArray6[46] = (byte) 192 /*0xC0*/;
      numArray6[23] = (byte) 105;
      numArray6[16 /*0x10*/] = (byte) 39;
      numArray6[25] = (byte) 226;
      numArray6[22] = (byte) 127 /*0x7F*/;
      numArray6[33] = (byte) 234;
      numArray6[2] = (byte) 33;
      numArray6[0] = (byte) 33;
      numArray6[15] = (byte) 6;
      numArray6[14] = (byte) 188;
      numArray6[51] = (byte) 208 /*0xD0*/;
      numArray6[44] = (byte) 231;
      numArray6[34] = (byte) 26;
      numArray6[35] = (byte) 56;
      numArray6[13] = (byte) 183;
      numArray6[48 /*0x30*/] = (byte) 151;
      numArray6[38] = (byte) 48 /*0x30*/;
      numArray6[39] = (byte) 12;
      numArray6[40] = (byte) 9;
      numArray6[41] = (byte) 15;
      numArray6[42] = (byte) 11;
      numArray6[43] = (byte) 40;
      numArray6[12] = (byte) 12;
      numArray6[45] = (byte) 55;
      numArray6[19] = (byte) 186;
      numArray6[47] = (byte) 121;
      numArray6[36] = (byte) 51;
      numArray6[24] = (byte) 62;
      numArray6[50] = (byte) 213;
      numArray6[8] = (byte) 189;
      numArray6[52] = (byte) 99;
      numArray6[21] = (byte) 244;
      numArray6[54] = (byte) 60;
      byte[] numArray7 = new byte[55]
      {
        (byte) 141,
        (byte) 22,
        (byte) 81,
        (byte) 213,
        (byte) 132,
        (byte) 138,
        (byte) 194,
        (byte) 124,
        (byte) 8,
        (byte) 29,
        (byte) 107,
        (byte) 181,
        (byte) 18,
        (byte) 204,
        (byte) 93,
        (byte) 12,
        (byte) 101,
        (byte) 96 /*0x60*/,
        (byte) 213,
        (byte) 60,
        (byte) 239,
        (byte) 124,
        (byte) 64 /*0x40*/,
        (byte) 195,
        (byte) 229,
        (byte) 66,
        (byte) 197,
        (byte) 161,
        (byte) 150,
        (byte) 186,
        (byte) 224 /*0xE0*/,
        (byte) 41,
        (byte) 89,
        (byte) 46,
        (byte) 196,
        (byte) 46,
        (byte) 81,
        (byte) 136,
        (byte) 15,
        (byte) 20,
        (byte) 232,
        (byte) 155,
        (byte) 94,
        (byte) 149,
        (byte) 81,
        (byte) 44,
        (byte) 112 /*0x70*/,
        (byte) 118,
        (byte) 46,
        (byte) 137,
        (byte) 42,
        (byte) 62,
        (byte) 55,
        byte.MaxValue,
        (byte) 156
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[54];
      numArray8[47] = (byte) 184;
      numArray8[17] = (byte) 121;
      numArray8[1] = (byte) 32 /*0x20*/;
      numArray8[35] = (byte) 175;
      numArray8[42] = (byte) 212;
      numArray8[5] = (byte) 214;
      numArray8[0] = (byte) 53;
      numArray8[34] = (byte) 69;
      numArray8[8] = (byte) 68;
      numArray8[3] = (byte) 108;
      numArray8[10] = (byte) 116;
      numArray8[11] = (byte) 176 /*0xB0*/;
      numArray8[29] = (byte) 64 /*0x40*/;
      numArray8[27] = (byte) 125;
      numArray8[14] = (byte) 95;
      numArray8[15] = (byte) 9;
      numArray8[4] = (byte) 204;
      numArray8[13] = (byte) 88;
      numArray8[26] = (byte) 247;
      numArray8[48 /*0x30*/] = (byte) 71;
      numArray8[20] = (byte) 183;
      numArray8[21] = (byte) 104;
      numArray8[41] = (byte) 194;
      numArray8[23] = (byte) 131;
      numArray8[24] = (byte) 127 /*0x7F*/;
      numArray8[25] = (byte) 243;
      numArray8[49] = (byte) 75;
      numArray8[38] = (byte) 105;
      numArray8[28] = (byte) 87;
      numArray8[40] = (byte) 166;
      numArray8[30] = (byte) 55;
      numArray8[31 /*0x1F*/] = (byte) 93;
      numArray8[32 /*0x20*/] = (byte) 168;
      numArray8[22] = (byte) 78;
      numArray8[18] = (byte) 187;
      numArray8[43] = (byte) 71;
      numArray8[36] = (byte) 213;
      numArray8[53] = (byte) 164;
      numArray8[37] = (byte) 217;
      numArray8[39] = (byte) 89;
      numArray8[2] = (byte) 156;
      numArray8[6] = (byte) 160 /*0xA0*/;
      numArray8[50] = (byte) 116;
      numArray8[51] = (byte) 91;
      numArray8[44] = (byte) 42;
      numArray8[45] = (byte) 42;
      numArray8[46] = (byte) 186;
      numArray8[7] = (byte) 221;
      numArray8[52] = (byte) 148;
      numArray8[33] = (byte) 116;
      numArray8[16 /*0x10*/] = (byte) 212;
      numArray8[19] = (byte) 160 /*0xA0*/;
      numArray8[9] = (byte) 113;
      numArray8[12] = (byte) 89;
      byte[] numArray9 = new byte[54]
      {
        (byte) 69,
        (byte) 81,
        (byte) 102,
        (byte) 228,
        (byte) 39,
        (byte) 138,
        (byte) 252,
        (byte) 202,
        (byte) 143,
        (byte) 225,
        (byte) 223,
        (byte) 223,
        (byte) 190,
        (byte) 152,
        (byte) 175,
        (byte) 71,
        (byte) 182,
        (byte) 107,
        (byte) 95,
        (byte) 171,
        (byte) 106,
        (byte) 160 /*0xA0*/,
        (byte) 181,
        (byte) 64 /*0x40*/,
        (byte) 160 /*0xA0*/,
        (byte) 166,
        (byte) 114,
        (byte) 61,
        (byte) 72,
        (byte) 217,
        (byte) 143,
        (byte) 173,
        (byte) 192 /*0xC0*/,
        (byte) 184,
        (byte) 3,
        (byte) 141,
        (byte) 120,
        (byte) 228,
        (byte) 248,
        byte.MaxValue,
        (byte) 185,
        (byte) 160 /*0xA0*/,
        (byte) 92,
        (byte) 44,
        (byte) 91,
        (byte) 135,
        (byte) 195,
        (byte) 128 /*0x80*/,
        (byte) 202,
        (byte) 162,
        (byte) 23,
        (byte) 116,
        (byte) 121,
        (byte) 233
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[219];
    byte[] numArray11 = new byte[55]
    {
      (byte) 202,
      (byte) 192 /*0xC0*/,
      (byte) 152,
      (byte) 158,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 121,
      (byte) 56,
      (byte) 94,
      (byte) 77,
      (byte) 4,
      (byte) 112 /*0x70*/,
      (byte) 153,
      (byte) 209,
      (byte) 121,
      (byte) 14,
      (byte) 127 /*0x7F*/,
      (byte) 231,
      (byte) 117,
      (byte) 184,
      (byte) 195,
      (byte) 249,
      (byte) 69,
      (byte) 50,
      (byte) 100,
      (byte) 152,
      (byte) 158,
      (byte) 245,
      (byte) 82,
      (byte) 143,
      (byte) 149,
      (byte) 88,
      (byte) 106,
      (byte) 64 /*0x40*/,
      (byte) 19,
      (byte) 250,
      (byte) 83,
      (byte) 241,
      (byte) 218,
      (byte) 151,
      (byte) 194,
      (byte) 152,
      (byte) 175,
      (byte) 198,
      (byte) 29,
      (byte) 100,
      (byte) 12,
      (byte) 198,
      (byte) 157,
      (byte) 107,
      (byte) 204,
      (byte) 76,
      (byte) 53,
      (byte) 81,
      (byte) 116
    };
    byte[] numArray12 = new byte[55];
    numArray12[18] = (byte) 30;
    numArray12[1] = (byte) 51;
    numArray12[21] = (byte) 245;
    numArray12[3] = (byte) 125;
    numArray12[26] = (byte) 138;
    numArray12[5] = (byte) 179;
    numArray12[14] = (byte) 72;
    numArray12[40] = (byte) 29;
    numArray12[8] = (byte) 1;
    numArray12[9] = (byte) 220;
    numArray12[49] = (byte) 219;
    numArray12[20] = (byte) 105;
    numArray12[12] = (byte) 126;
    numArray12[15] = (byte) 86;
    numArray12[44] = (byte) 131;
    numArray12[4] = (byte) 108;
    numArray12[16 /*0x10*/] = (byte) 219;
    numArray12[0] = (byte) 23;
    numArray12[50] = (byte) 248;
    numArray12[7] = (byte) 237;
    numArray12[37] = (byte) 213;
    numArray12[22] = (byte) 58;
    numArray12[54] = (byte) 251;
    numArray12[38] = (byte) 238;
    numArray12[36] = (byte) 25;
    numArray12[39] = (byte) 165;
    numArray12[27] = (byte) 238;
    numArray12[33] = (byte) 86;
    numArray12[41] = (byte) 253;
    numArray12[29] = (byte) 189;
    numArray12[30] = (byte) 97;
    numArray12[32 /*0x20*/] = (byte) 161;
    numArray12[24] = (byte) 15;
    numArray12[28] = (byte) 63 /*0x3F*/;
    numArray12[19] = (byte) 145;
    numArray12[35] = (byte) 147;
    numArray12[23] = (byte) 212;
    numArray12[11] = (byte) 77;
    numArray12[25] = (byte) 39;
    numArray12[2] = (byte) 107;
    numArray12[43] = (byte) 236;
    numArray12[34] = (byte) 202;
    numArray12[42] = (byte) 177;
    numArray12[47] = (byte) 203;
    numArray12[31 /*0x1F*/] = (byte) 42;
    numArray12[45] = (byte) 238;
    numArray12[46] = (byte) 205;
    numArray12[10] = (byte) 226;
    numArray12[48 /*0x30*/] = (byte) 156;
    numArray12[6] = (byte) 138;
    numArray12[13] = (byte) 94;
    numArray12[51] = (byte) 133;
    numArray12[52] = (byte) 129;
    numArray12[53] = (byte) 245;
    numArray12[17] = (byte) 108;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 81,
      (byte) 58,
      (byte) 124,
      (byte) 119,
      (byte) 43,
      (byte) 254,
      (byte) 11,
      (byte) 110,
      (byte) 136,
      (byte) 237,
      (byte) 141,
      (byte) 125,
      (byte) 254,
      (byte) 204,
      (byte) 214,
      (byte) 58,
      (byte) 150,
      (byte) 183,
      (byte) 119,
      (byte) 106,
      (byte) 111,
      (byte) 163,
      (byte) 161,
      (byte) 215,
      (byte) 207,
      (byte) 57,
      (byte) 54,
      (byte) 133,
      (byte) 10,
      (byte) 204,
      (byte) 189,
      (byte) 129,
      (byte) 186,
      (byte) 130,
      (byte) 156,
      (byte) 125,
      (byte) 86,
      (byte) 209,
      (byte) 19,
      (byte) 180,
      (byte) 212,
      (byte) 160 /*0xA0*/,
      (byte) 233,
      (byte) 112 /*0x70*/,
      (byte) 39,
      (byte) 139,
      (byte) 32 /*0x20*/,
      (byte) 228,
      (byte) 104,
      (byte) 5,
      (byte) 192 /*0xC0*/,
      (byte) 164,
      (byte) 210,
      (byte) 29,
      (byte) 141
    };
    byte[] numArray14 = new byte[55];
    numArray14[23] = (byte) 67;
    numArray14[45] = (byte) 218;
    numArray14[28] = (byte) 19;
    numArray14[3] = (byte) 81;
    numArray14[4] = (byte) 14;
    numArray14[5] = (byte) 216;
    numArray14[37] = (byte) 246;
    numArray14[36] = (byte) 1;
    numArray14[43] = (byte) 108;
    numArray14[50] = (byte) 61;
    numArray14[26] = (byte) 70;
    numArray14[0] = (byte) 37;
    numArray14[10] = (byte) 100;
    numArray14[2] = (byte) 226;
    numArray14[14] = (byte) 244;
    numArray14[47] = (byte) 12;
    numArray14[30] = (byte) 93;
    numArray14[41] = (byte) 67;
    numArray14[18] = (byte) 125;
    numArray14[12] = (byte) 227;
    numArray14[20] = (byte) 103;
    numArray14[21] = (byte) 83;
    numArray14[6] = (byte) 214;
    numArray14[15] = (byte) 160 /*0xA0*/;
    numArray14[24] = (byte) 171;
    numArray14[13] = (byte) 22;
    numArray14[31 /*0x1F*/] = (byte) 214;
    numArray14[27] = (byte) 93;
    numArray14[40] = (byte) 25;
    numArray14[29] = (byte) 249;
    numArray14[7] = (byte) 10;
    numArray14[16 /*0x10*/] = (byte) 239;
    numArray14[42] = (byte) 230;
    numArray14[1] = (byte) 231;
    numArray14[34] = (byte) 97;
    numArray14[35] = (byte) 8;
    numArray14[48 /*0x30*/] = (byte) 177;
    numArray14[33] = (byte) 74;
    numArray14[38] = (byte) 171;
    numArray14[39] = (byte) 102;
    numArray14[11] = (byte) 251;
    numArray14[51] = (byte) 207;
    numArray14[19] = (byte) 118;
    numArray14[17] = (byte) 35;
    numArray14[44] = (byte) 69;
    numArray14[22] = (byte) 181;
    numArray14[46] = (byte) 21;
    numArray14[8] = (byte) 133;
    numArray14[25] = (byte) 63 /*0x3F*/;
    numArray14[32 /*0x20*/] = (byte) 217;
    numArray14[53] = (byte) 119;
    numArray14[9] = (byte) 177;
    numArray14[52] = (byte) 127 /*0x7F*/;
    numArray14[49] = (byte) 0;
    numArray14[54] = (byte) 245;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55];
    numArray15[53] = (byte) 101;
    numArray15[1] = (byte) 99;
    numArray15[0] = (byte) 245;
    numArray15[42] = (byte) 191;
    numArray15[6] = (byte) 175;
    numArray15[5] = (byte) 94;
    numArray15[34] = (byte) 187;
    numArray15[7] = (byte) 23;
    numArray15[47] = (byte) 237;
    numArray15[33] = (byte) 124;
    numArray15[36] = (byte) 71;
    numArray15[21] = (byte) 148;
    numArray15[12] = (byte) 116;
    numArray15[13] = (byte) 51;
    numArray15[17] = (byte) 70;
    numArray15[35] = (byte) 197;
    numArray15[14] = (byte) 155;
    numArray15[43] = (byte) 161;
    numArray15[18] = (byte) 146;
    numArray15[11] = (byte) 65;
    numArray15[2] = (byte) 217;
    numArray15[19] = (byte) 141;
    numArray15[8] = (byte) 92;
    numArray15[23] = (byte) 117;
    numArray15[24] = (byte) 157;
    numArray15[25] = (byte) 13;
    numArray15[22] = (byte) 124;
    numArray15[27] = (byte) 16 /*0x10*/;
    numArray15[28] = (byte) 24;
    numArray15[29] = (byte) 160 /*0xA0*/;
    numArray15[30] = (byte) 20;
    numArray15[31 /*0x1F*/] = (byte) 55;
    numArray15[15] = (byte) 130;
    numArray15[49] = (byte) 126;
    numArray15[51] = (byte) 66;
    numArray15[20] = (byte) 241;
    numArray15[16 /*0x10*/] = (byte) 216;
    numArray15[44] = (byte) 27;
    numArray15[38] = (byte) 230;
    numArray15[9] = (byte) 38;
    numArray15[40] = (byte) 126;
    numArray15[41] = (byte) 40;
    numArray15[4] = (byte) 104;
    numArray15[46] = (byte) 154;
    numArray15[26] = (byte) 228;
    numArray15[3] = (byte) 118;
    numArray15[10] = (byte) 135;
    numArray15[39] = (byte) 221;
    numArray15[48 /*0x30*/] = (byte) 95;
    numArray15[54] = (byte) 118;
    numArray15[50] = (byte) 54;
    numArray15[32 /*0x20*/] = (byte) 252;
    numArray15[52] = (byte) 42;
    numArray15[37] = (byte) 116;
    numArray15[45] = (byte) 114;
    byte[] numArray16 = new byte[55];
    numArray16[42] = (byte) 216;
    numArray16[16 /*0x10*/] = (byte) 218;
    numArray16[2] = (byte) 68;
    numArray16[3] = (byte) 50;
    numArray16[4] = (byte) 201;
    numArray16[18] = (byte) 87;
    numArray16[6] = (byte) 43;
    numArray16[7] = (byte) 176 /*0xB0*/;
    numArray16[8] = (byte) 156;
    numArray16[5] = (byte) 115;
    numArray16[25] = (byte) 221;
    numArray16[46] = (byte) 55;
    numArray16[12] = (byte) 171;
    numArray16[13] = (byte) 148;
    numArray16[50] = (byte) 79;
    numArray16[15] = (byte) 30;
    numArray16[49] = (byte) 138;
    numArray16[24] = (byte) 119;
    numArray16[41] = (byte) 52;
    numArray16[47] = (byte) 126;
    numArray16[11] = (byte) 115;
    numArray16[10] = (byte) 0;
    numArray16[22] = (byte) 205;
    numArray16[23] = (byte) 239;
    numArray16[43] = (byte) 175;
    numArray16[45] = (byte) 51;
    numArray16[26] = (byte) 85;
    numArray16[20] = (byte) 168;
    numArray16[28] = (byte) 106;
    numArray16[29] = (byte) 204;
    numArray16[53] = (byte) 111;
    numArray16[52] = (byte) 223;
    numArray16[14] = (byte) 234;
    numArray16[33] = (byte) 169;
    numArray16[34] = (byte) 16 /*0x10*/;
    numArray16[35] = (byte) 246;
    numArray16[9] = (byte) 16 /*0x10*/;
    numArray16[37] = (byte) 214;
    numArray16[38] = (byte) 102;
    numArray16[39] = (byte) 159;
    numArray16[40] = (byte) 251;
    numArray16[31 /*0x1F*/] = (byte) 97;
    numArray16[30] = (byte) 198;
    numArray16[21] = (byte) 101;
    numArray16[19] = (byte) 108;
    numArray16[0] = (byte) 142;
    numArray16[27] = (byte) 28;
    numArray16[17] = (byte) 69;
    numArray16[48 /*0x30*/] = (byte) 42;
    numArray16[1] = (byte) 243;
    numArray16[44] = (byte) 159;
    numArray16[51] = (byte) 213;
    numArray16[36] = (byte) 40;
    numArray16[32 /*0x20*/] = (byte) 118;
    numArray16[54] = (byte) 252;
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[54]
    {
      (byte) 125,
      (byte) 175,
      (byte) 21,
      (byte) 3,
      (byte) 196,
      (byte) 14,
      (byte) 185,
      (byte) 78,
      (byte) 171,
      (byte) 108,
      (byte) 154,
      (byte) 77,
      (byte) 36,
      (byte) 125,
      (byte) 216,
      (byte) 48 /*0x30*/,
      (byte) 115,
      (byte) 60,
      (byte) 217,
      (byte) 29,
      (byte) 248,
      (byte) 4,
      (byte) 125,
      (byte) 34,
      (byte) 39,
      (byte) 150,
      (byte) 103,
      (byte) 138,
      (byte) 226,
      (byte) 115,
      (byte) 26,
      (byte) 73,
      (byte) 19,
      (byte) 15,
      (byte) 91,
      (byte) 62,
      (byte) 142,
      (byte) 218,
      (byte) 46,
      (byte) 182,
      (byte) 249,
      (byte) 2,
      (byte) 220,
      (byte) 19,
      (byte) 21,
      (byte) 11,
      (byte) 109,
      (byte) 112 /*0x70*/,
      (byte) 51,
      (byte) 220,
      (byte) 175,
      (byte) 234,
      (byte) 53,
      (byte) 157
    };
    byte[] numArray18 = new byte[54]
    {
      (byte) 40,
      (byte) 110,
      (byte) 163,
      (byte) 176 /*0xB0*/,
      (byte) 41,
      (byte) 131,
      (byte) 229,
      (byte) 136,
      (byte) 197,
      (byte) 46,
      (byte) 39,
      (byte) 83,
      (byte) 74,
      (byte) 102,
      (byte) 138,
      (byte) 50,
      (byte) 24,
      (byte) 203,
      (byte) 15,
      (byte) 71,
      (byte) 176 /*0xB0*/,
      (byte) 38,
      (byte) 174,
      (byte) 167,
      (byte) 97,
      (byte) 33,
      (byte) 214,
      (byte) 138,
      (byte) 128 /*0x80*/,
      (byte) 98,
      (byte) 215,
      (byte) 221,
      (byte) 202,
      (byte) 167,
      (byte) 161,
      (byte) 149,
      (byte) 104,
      (byte) 218,
      (byte) 65,
      (byte) 224 /*0xE0*/,
      (byte) 142,
      (byte) 30,
      (byte) 181,
      (byte) 174,
      (byte) 127 /*0x7F*/,
      (byte) 171,
      (byte) 198,
      (byte) 131,
      (byte) 98,
      (byte) 38,
      (byte) 43,
      (byte) 225,
      (byte) 118,
      (byte) 209
    };
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 54);
    for (int index = 0; index < 54; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static int ssp_appserver_13276(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 46,
      (byte) 166,
      (byte) 47,
      (byte) 108,
      (byte) 56,
      (byte) 82,
      (byte) 80 /*0x50*/,
      (byte) 58,
      (byte) 21,
      (byte) 34,
      (byte) 153,
      (byte) 162,
      (byte) 153,
      (byte) 13,
      (byte) 34,
      (byte) 35,
      (byte) 246,
      (byte) 39,
      (byte) 106,
      (byte) 13,
      (byte) 28,
      (byte) 40,
      (byte) 247,
      (byte) 86,
      (byte) 6,
      (byte) 196,
      (byte) 47,
      (byte) 19,
      (byte) 117,
      (byte) 69,
      (byte) 225,
      (byte) 12,
      (byte) 155,
      (byte) 254,
      (byte) 0,
      (byte) 15,
      (byte) 202,
      (byte) 150,
      (byte) 164,
      (byte) 192 /*0xC0*/,
      (byte) 196,
      (byte) 208 /*0xD0*/,
      (byte) 87,
      (byte) 78,
      (byte) 153,
      (byte) 58,
      (byte) 22,
      (byte) 220
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 96 /*0x60*/,
      (byte) 174,
      (byte) 139,
      (byte) 39,
      (byte) 137,
      (byte) 89,
      (byte) 140,
      (byte) 32 /*0x20*/,
      (byte) 180,
      (byte) 37,
      (byte) 162,
      (byte) 22,
      (byte) 70,
      (byte) 76,
      (byte) 230,
      (byte) 125,
      (byte) 86,
      (byte) 200,
      (byte) 209,
      (byte) 127 /*0x7F*/,
      (byte) 200,
      (byte) 92,
      (byte) 90,
      (byte) 170,
      (byte) 245,
      (byte) 243,
      (byte) 57,
      (byte) 160 /*0xA0*/,
      (byte) 8,
      (byte) 141,
      (byte) 7,
      (byte) 18,
      (byte) 99,
      (byte) 209,
      (byte) 205,
      (byte) 108,
      (byte) 77,
      (byte) 16 /*0x10*/,
      (byte) 87,
      (byte) 126,
      (byte) 140,
      (byte) 32 /*0x20*/,
      (byte) 225,
      (byte) 152,
      (byte) 244,
      (byte) 169,
      (byte) 115,
      (byte) 46
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[35];
    byte[] response2 = new byte[35];
    Array.Copy((Array) sc_13274.sspq, 0, (Array) numArray2, 0, 35);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13274.sspr, 0, (Array) numArray2, 0, 35);
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

  internal static int ssp_appserver_13277(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 250,
      (byte) 96 /*0x60*/,
      (byte) 241,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 158,
      (byte) 136,
      (byte) 11,
      (byte) 135,
      (byte) 13,
      (byte) 12,
      (byte) 51,
      (byte) 123,
      (byte) 206,
      (byte) 0,
      (byte) 32 /*0x20*/,
      (byte) 116,
      (byte) 206,
      (byte) 29,
      (byte) 156,
      (byte) 116,
      (byte) 52,
      (byte) 6,
      (byte) 27,
      (byte) 154,
      (byte) 197,
      (byte) 207,
      (byte) 59,
      (byte) 201,
      (byte) 94,
      (byte) 176 /*0xB0*/,
      (byte) 73,
      (byte) 79,
      (byte) 92,
      (byte) 19,
      (byte) 178,
      (byte) 103,
      (byte) 47,
      (byte) 82,
      (byte) 11,
      (byte) 192 /*0xC0*/,
      (byte) 63 /*0x3F*/,
      (byte) 73,
      (byte) 155,
      (byte) 176 /*0xB0*/,
      (byte) 152,
      (byte) 50,
      (byte) 132
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 6;
    sourceArray2[28] = (byte) 134;
    sourceArray2[16 /*0x10*/] = (byte) 192 /*0xC0*/;
    sourceArray2[31 /*0x1F*/] = (byte) 86;
    sourceArray2[4] = (byte) 196;
    sourceArray2[18] = (byte) 198;
    sourceArray2[20] = (byte) 102;
    sourceArray2[1] = (byte) 37;
    sourceArray2[8] = (byte) 205;
    sourceArray2[7] = (byte) 158;
    sourceArray2[47] = (byte) 119;
    sourceArray2[11] = (byte) 92;
    sourceArray2[12] = (byte) 10;
    sourceArray2[44] = (byte) 91;
    sourceArray2[14] = (byte) 24;
    sourceArray2[10] = (byte) 137;
    sourceArray2[39] = (byte) 138;
    sourceArray2[17] = (byte) 154;
    sourceArray2[23] = (byte) 13;
    sourceArray2[42] = (byte) 54;
    sourceArray2[25] = (byte) 208 /*0xD0*/;
    sourceArray2[21] = (byte) 144 /*0x90*/;
    sourceArray2[0] = (byte) 170;
    sourceArray2[6] = (byte) 183;
    sourceArray2[33] = (byte) 31 /*0x1F*/;
    sourceArray2[3] = (byte) 154;
    sourceArray2[45] = (byte) 50;
    sourceArray2[27] = (byte) 227;
    sourceArray2[13] = (byte) 126;
    sourceArray2[15] = (byte) 184;
    sourceArray2[30] = (byte) 168;
    sourceArray2[36] = (byte) 42;
    sourceArray2[32 /*0x20*/] = (byte) 30;
    sourceArray2[9] = (byte) 62;
    sourceArray2[34] = (byte) 185;
    sourceArray2[35] = (byte) 23;
    sourceArray2[22] = (byte) 169;
    sourceArray2[37] = (byte) 232;
    sourceArray2[38] = (byte) 97;
    sourceArray2[26] = (byte) 136;
    sourceArray2[40] = (byte) 223;
    sourceArray2[41] = (byte) 9;
    sourceArray2[29] = (byte) 99;
    sourceArray2[43] = (byte) 39;
    sourceArray2[5] = (byte) 211;
    sourceArray2[2] = (byte) 156;
    sourceArray2[46] = (byte) 2;
    sourceArray2[24] = (byte) 51;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13278()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[45];
      byte[] numArray2 = new byte[45]
      {
        (byte) 180,
        (byte) 18,
        (byte) 200,
        (byte) 177,
        (byte) 33,
        (byte) 184,
        (byte) 69,
        (byte) 20,
        (byte) 62,
        (byte) 149,
        (byte) 218,
        (byte) 239,
        (byte) 108,
        (byte) 148,
        (byte) 215,
        (byte) 132,
        (byte) 16 /*0x10*/,
        (byte) 83,
        (byte) 199,
        (byte) 111,
        (byte) 160 /*0xA0*/,
        (byte) 174,
        (byte) 141,
        (byte) 110,
        (byte) 246,
        (byte) 39,
        (byte) 171,
        (byte) 149,
        (byte) 213,
        (byte) 102,
        (byte) 55,
        (byte) 202,
        (byte) 44,
        (byte) 124,
        (byte) 77,
        (byte) 129,
        (byte) 130,
        (byte) 137,
        (byte) 204,
        (byte) 168,
        (byte) 169,
        (byte) 13,
        (byte) 246,
        (byte) 252,
        (byte) 114
      };
      byte[] numArray3 = new byte[45]
      {
        (byte) 13,
        (byte) 139,
        (byte) 161,
        (byte) 47,
        (byte) 228,
        (byte) 234,
        (byte) 5,
        (byte) 195,
        (byte) 90,
        (byte) 223,
        (byte) 177,
        (byte) 67,
        (byte) 110,
        (byte) 206,
        (byte) 139,
        (byte) 113,
        (byte) 166,
        (byte) 172,
        (byte) 235,
        (byte) 152,
        (byte) 18,
        (byte) 44,
        (byte) 250,
        (byte) 60,
        (byte) 195,
        (byte) 141,
        (byte) 12,
        (byte) 57,
        (byte) 66,
        (byte) 95,
        (byte) 37,
        (byte) 105,
        (byte) 34,
        (byte) 164,
        (byte) 120,
        (byte) 183,
        (byte) 129,
        (byte) 153,
        (byte) 53,
        (byte) 29,
        (byte) 160 /*0xA0*/,
        (byte) 79,
        (byte) 1,
        (byte) 45,
        (byte) 2
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 45);
      for (int index = 0; index < 45; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[45];
    byte[] numArray5 = new byte[45];
    numArray5[31 /*0x1F*/] = (byte) 141;
    numArray5[1] = (byte) 184;
    numArray5[15] = (byte) 215;
    numArray5[21] = (byte) 86;
    numArray5[4] = (byte) 88;
    numArray5[5] = (byte) 206;
    numArray5[28] = (byte) 184;
    numArray5[8] = (byte) 132;
    numArray5[6] = (byte) 77;
    numArray5[9] = (byte) 89;
    numArray5[10] = (byte) 187;
    numArray5[11] = (byte) 249;
    numArray5[43] = (byte) 40;
    numArray5[0] = (byte) 15;
    numArray5[20] = (byte) 70;
    numArray5[2] = (byte) 99;
    numArray5[16 /*0x10*/] = (byte) 94;
    numArray5[17] = (byte) 16 /*0x10*/;
    numArray5[18] = (byte) 194;
    numArray5[22] = (byte) 2;
    numArray5[14] = (byte) 63 /*0x3F*/;
    numArray5[13] = (byte) 175;
    numArray5[29] = (byte) 145;
    numArray5[23] = (byte) 135;
    numArray5[7] = (byte) 5;
    numArray5[12] = (byte) 13;
    numArray5[26] = (byte) 198;
    numArray5[27] = (byte) 146;
    numArray5[3] = (byte) 169;
    numArray5[32 /*0x20*/] = (byte) 2;
    numArray5[30] = (byte) 252;
    numArray5[36] = (byte) 3;
    numArray5[25] = (byte) 31 /*0x1F*/;
    numArray5[33] = (byte) 166;
    numArray5[34] = (byte) 134;
    numArray5[35] = (byte) 28;
    numArray5[19] = (byte) 239;
    numArray5[37] = (byte) 141;
    numArray5[24] = (byte) 135;
    numArray5[39] = (byte) 93;
    numArray5[40] = (byte) 46;
    numArray5[41] = (byte) 72;
    numArray5[42] = (byte) 252;
    numArray5[38] = (byte) 114;
    numArray5[44] = (byte) 206;
    byte[] numArray6 = new byte[45];
    numArray6[8] = (byte) 203;
    numArray6[39] = (byte) 40;
    numArray6[2] = (byte) 238;
    numArray6[3] = (byte) 90;
    numArray6[33] = (byte) 175;
    numArray6[24] = (byte) 159;
    numArray6[12] = (byte) 254;
    numArray6[32 /*0x20*/] = (byte) 69;
    numArray6[21] = (byte) 196;
    numArray6[9] = (byte) 83;
    numArray6[31 /*0x1F*/] = (byte) 60;
    numArray6[1] = (byte) 165;
    numArray6[5] = (byte) 97;
    numArray6[42] = (byte) 149;
    numArray6[25] = (byte) 150;
    numArray6[15] = (byte) 160 /*0xA0*/;
    numArray6[4] = (byte) 135;
    numArray6[18] = (byte) 71;
    numArray6[13] = (byte) 18;
    numArray6[19] = (byte) 74;
    numArray6[0] = (byte) 191;
    numArray6[23] = (byte) 226;
    numArray6[22] = (byte) 15;
    numArray6[35] = (byte) 239;
    numArray6[29] = (byte) 238;
    numArray6[30] = (byte) 83;
    numArray6[26] = (byte) 91;
    numArray6[27] = (byte) 91;
    numArray6[28] = (byte) 93;
    numArray6[16 /*0x10*/] = (byte) 105;
    numArray6[17] = (byte) 106;
    numArray6[6] = (byte) 33;
    numArray6[43] = (byte) 200;
    numArray6[20] = (byte) 15;
    numArray6[34] = (byte) 42;
    numArray6[14] = (byte) 36;
    numArray6[7] = (byte) 122;
    numArray6[37] = (byte) 163;
    numArray6[36] = (byte) 81;
    numArray6[10] = (byte) 186;
    numArray6[40] = (byte) 40;
    numArray6[41] = (byte) 46;
    numArray6[11] = (byte) 233;
    numArray6[38] = (byte) 89;
    numArray6[44] = (byte) 109;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 45);
    for (int index = 0; index < 45; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13279(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 214,
      (byte) 167,
      (byte) 88,
      (byte) 40,
      (byte) 109,
      (byte) 137,
      (byte) 3,
      (byte) 252,
      (byte) 131,
      (byte) 24,
      (byte) 122,
      (byte) 71,
      (byte) 7,
      (byte) 8,
      (byte) 91,
      (byte) 178,
      (byte) 183,
      (byte) 115,
      (byte) 238,
      (byte) 156,
      (byte) 103,
      (byte) 94,
      (byte) 48 /*0x30*/,
      (byte) 38,
      (byte) 189,
      (byte) 13,
      (byte) 206,
      (byte) 10,
      (byte) 149,
      (byte) 101,
      (byte) 64 /*0x40*/,
      (byte) 223,
      (byte) 99,
      (byte) 218,
      (byte) 236,
      (byte) 157,
      (byte) 208 /*0xD0*/,
      (byte) 84,
      (byte) 238,
      (byte) 52,
      (byte) 169,
      (byte) 104,
      (byte) 225,
      (byte) 247,
      (byte) 211,
      (byte) 186,
      (byte) 73,
      (byte) 5
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 185,
      (byte) 106,
      (byte) 23,
      (byte) 111,
      (byte) 44,
      (byte) 250,
      (byte) 143,
      (byte) 28,
      (byte) 2,
      (byte) 247,
      (byte) 92,
      (byte) 168,
      (byte) 196,
      (byte) 24,
      (byte) 39,
      (byte) 174,
      (byte) 241,
      (byte) 163,
      (byte) 185,
      (byte) 184,
      (byte) 162,
      (byte) 167,
      (byte) 29,
      (byte) 134,
      (byte) 248,
      (byte) 126,
      (byte) 176 /*0xB0*/,
      (byte) 123,
      (byte) 41,
      (byte) 152,
      (byte) 170,
      (byte) 15,
      (byte) 212,
      (byte) 205,
      (byte) 16 /*0x10*/,
      (byte) 49,
      (byte) 204,
      (byte) 20,
      (byte) 66,
      (byte) 41,
      (byte) 180,
      (byte) 158,
      (byte) 65,
      (byte) 137,
      (byte) 214,
      (byte) 231,
      (byte) 97,
      (byte) 86
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13280()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 41,
        (byte) 180,
        (byte) 190,
        (byte) 119,
        (byte) 118,
        (byte) 50,
        (byte) 107,
        (byte) 95,
        (byte) 186,
        (byte) 249
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 96 /*0x60*/,
        (byte) 109,
        (byte) 176 /*0xB0*/,
        (byte) 248,
        (byte) 168,
        (byte) 81,
        (byte) 159,
        (byte) 122,
        (byte) 193,
        (byte) 99
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[0] = (byte) 155;
    numArray5[4] = (byte) 189;
    numArray5[2] = (byte) 45;
    numArray5[3] = (byte) 28;
    numArray5[8] = (byte) 186;
    numArray5[5] = (byte) 225;
    numArray5[6] = (byte) 98;
    numArray5[7] = (byte) 133;
    numArray5[1] = (byte) 228;
    numArray5[9] = (byte) 242;
    byte[] numArray6 = new byte[10];
    numArray6[3] = (byte) 138;
    numArray6[1] = (byte) 55;
    numArray6[0] = (byte) 172;
    numArray6[9] = (byte) 158;
    numArray6[4] = (byte) 35;
    numArray6[6] = (byte) 194;
    numArray6[8] = (byte) 223;
    numArray6[7] = (byte) 99;
    numArray6[5] = (byte) 177;
    numArray6[2] = (byte) 223;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13281(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[6] = (byte) 3;
    sourceArray1[37] = (byte) 77;
    sourceArray1[2] = (byte) 232;
    sourceArray1[3] = byte.MaxValue;
    sourceArray1[27] = (byte) 246;
    sourceArray1[5] = (byte) 53;
    sourceArray1[43] = (byte) 92;
    sourceArray1[14] = (byte) 156;
    sourceArray1[8] = (byte) 193;
    sourceArray1[9] = (byte) 191;
    sourceArray1[22] = (byte) 17;
    sourceArray1[11] = (byte) 61;
    sourceArray1[34] = (byte) 44;
    sourceArray1[26] = (byte) 121;
    sourceArray1[39] = (byte) 152;
    sourceArray1[15] = (byte) 241;
    sourceArray1[16 /*0x10*/] = (byte) 173;
    sourceArray1[44] = (byte) 122;
    sourceArray1[31 /*0x1F*/] = (byte) 186;
    sourceArray1[12] = (byte) 222;
    sourceArray1[20] = (byte) 229;
    sourceArray1[10] = (byte) 250;
    sourceArray1[1] = (byte) 28;
    sourceArray1[19] = (byte) 33;
    sourceArray1[24] = (byte) 151;
    sourceArray1[25] = (byte) 66;
    sourceArray1[32 /*0x20*/] = (byte) 27;
    sourceArray1[46] = (byte) 191;
    sourceArray1[28] = (byte) 64 /*0x40*/;
    sourceArray1[18] = (byte) 80 /*0x50*/;
    sourceArray1[7] = (byte) 111;
    sourceArray1[17] = (byte) 248;
    sourceArray1[47] = (byte) 129;
    sourceArray1[38] = (byte) 41;
    sourceArray1[23] = (byte) 168;
    sourceArray1[35] = (byte) 167;
    sourceArray1[36] = (byte) 224 /*0xE0*/;
    sourceArray1[0] = (byte) 214;
    sourceArray1[42] = (byte) 237;
    sourceArray1[4] = (byte) 69;
    sourceArray1[40] = (byte) 180;
    sourceArray1[41] = (byte) 69;
    sourceArray1[33] = (byte) 27;
    sourceArray1[13] = (byte) 104;
    sourceArray1[30] = (byte) 253;
    sourceArray1[45] = (byte) 233;
    sourceArray1[21] = (byte) 53;
    sourceArray1[29] = (byte) 66;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 75,
      (byte) 216,
      (byte) 179,
      (byte) 180,
      (byte) 218,
      (byte) 66,
      (byte) 188,
      (byte) 201,
      (byte) 224 /*0xE0*/,
      (byte) 51,
      (byte) 250,
      (byte) 125,
      (byte) 174,
      (byte) 119,
      (byte) 113,
      (byte) 74,
      (byte) 42,
      (byte) 18,
      (byte) 40,
      (byte) 109,
      (byte) 79,
      (byte) 244,
      (byte) 8,
      (byte) 177,
      (byte) 196,
      (byte) 176 /*0xB0*/,
      (byte) 171,
      (byte) 140,
      (byte) 36,
      (byte) 199,
      (byte) 129,
      (byte) 211,
      (byte) 148,
      (byte) 12,
      (byte) 194,
      (byte) 235,
      (byte) 231,
      (byte) 45,
      (byte) 230,
      (byte) 227,
      (byte) 138,
      (byte) 59,
      (byte) 31 /*0x1F*/,
      (byte) 141,
      (byte) 87,
      (byte) 49,
      (byte) 72,
      (byte) 19
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13282()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[112 /*0x70*/];
      byte[] numArray2 = new byte[55];
      numArray2[52] = (byte) 46;
      numArray2[34] = (byte) 229;
      numArray2[44] = (byte) 51;
      numArray2[3] = (byte) 254;
      numArray2[27] = (byte) 167;
      numArray2[5] = (byte) 163;
      numArray2[53] = (byte) 203;
      numArray2[7] = (byte) 185;
      numArray2[8] = (byte) 144 /*0x90*/;
      numArray2[54] = (byte) 66;
      numArray2[46] = (byte) 141;
      numArray2[11] = (byte) 86;
      numArray2[35] = (byte) 134;
      numArray2[0] = (byte) 199;
      numArray2[45] = (byte) 69;
      numArray2[2] = (byte) 69;
      numArray2[15] = (byte) 196;
      numArray2[41] = (byte) 141;
      numArray2[18] = (byte) 63 /*0x3F*/;
      numArray2[36] = (byte) 178;
      numArray2[1] = (byte) 252;
      numArray2[6] = (byte) 58;
      numArray2[22] = (byte) 249;
      numArray2[9] = (byte) 178;
      numArray2[24] = (byte) 89;
      numArray2[25] = (byte) 112 /*0x70*/;
      numArray2[26] = (byte) 122;
      numArray2[16 /*0x10*/] = (byte) 9;
      numArray2[38] = (byte) 64 /*0x40*/;
      numArray2[23] = (byte) 175;
      numArray2[30] = (byte) 137;
      numArray2[47] = (byte) 43;
      numArray2[4] = (byte) 119;
      numArray2[33] = (byte) 243;
      numArray2[19] = (byte) 206;
      numArray2[20] = (byte) 248;
      numArray2[32 /*0x20*/] = (byte) 27;
      numArray2[37] = (byte) 5;
      numArray2[17] = (byte) 118;
      numArray2[39] = (byte) 156;
      numArray2[40] = (byte) 185;
      numArray2[31 /*0x1F*/] = (byte) 217;
      numArray2[21] = (byte) 3;
      numArray2[43] = (byte) 23;
      numArray2[28] = (byte) 220;
      numArray2[12] = (byte) 18;
      numArray2[13] = (byte) 186;
      numArray2[42] = (byte) 141;
      numArray2[48 /*0x30*/] = (byte) 166;
      numArray2[49] = (byte) 23;
      numArray2[50] = (byte) 157;
      numArray2[51] = (byte) 159;
      numArray2[29] = (byte) 197;
      numArray2[10] = (byte) 88;
      numArray2[14] = (byte) 128 /*0x80*/;
      byte[] numArray3 = new byte[55];
      numArray3[48 /*0x30*/] = (byte) 3;
      numArray3[1] = (byte) 191;
      numArray3[0] = (byte) 181;
      numArray3[21] = (byte) 39;
      numArray3[47] = (byte) 35;
      numArray3[5] = (byte) 69;
      numArray3[7] = (byte) 161;
      numArray3[46] = (byte) 188;
      numArray3[8] = (byte) 150;
      numArray3[52] = (byte) 137;
      numArray3[40] = (byte) 194;
      numArray3[11] = (byte) 176 /*0xB0*/;
      numArray3[12] = (byte) 109;
      numArray3[13] = (byte) 100;
      numArray3[14] = (byte) 172;
      numArray3[15] = (byte) 30;
      numArray3[16 /*0x10*/] = (byte) 176 /*0xB0*/;
      numArray3[50] = (byte) 62;
      numArray3[18] = (byte) 38;
      numArray3[23] = (byte) 159;
      numArray3[6] = (byte) 103;
      numArray3[2] = (byte) 234;
      numArray3[22] = (byte) 193;
      numArray3[27] = (byte) 156;
      numArray3[17] = (byte) 201;
      numArray3[10] = (byte) 223;
      numArray3[35] = (byte) 233;
      numArray3[54] = (byte) 69;
      numArray3[31 /*0x1F*/] = (byte) 105;
      numArray3[19] = (byte) 195;
      numArray3[26] = (byte) 8;
      numArray3[29] = (byte) 191;
      numArray3[32 /*0x20*/] = (byte) 66;
      numArray3[33] = (byte) 68;
      numArray3[34] = (byte) 225;
      numArray3[37] = (byte) 91;
      numArray3[49] = (byte) 81;
      numArray3[38] = (byte) 46;
      numArray3[4] = (byte) 163;
      numArray3[39] = (byte) 84;
      numArray3[24] = (byte) 153;
      numArray3[41] = (byte) 11;
      numArray3[42] = (byte) 247;
      numArray3[36] = (byte) 1;
      numArray3[44] = (byte) 100;
      numArray3[45] = (byte) 53;
      numArray3[20] = (byte) 150;
      numArray3[3] = (byte) 207;
      numArray3[25] = (byte) 135;
      numArray3[43] = (byte) 80 /*0x50*/;
      numArray3[28] = (byte) 58;
      numArray3[51] = (byte) 47;
      numArray3[30] = (byte) 162;
      numArray3[53] = (byte) 158;
      numArray3[9] = (byte) 196;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[52] = (byte) 251;
      numArray4[23] = (byte) 116;
      numArray4[2] = (byte) 176 /*0xB0*/;
      numArray4[53] = (byte) 10;
      numArray4[54] = (byte) 229;
      numArray4[5] = (byte) 87;
      numArray4[6] = (byte) 19;
      numArray4[7] = (byte) 192 /*0xC0*/;
      numArray4[14] = (byte) 213;
      numArray4[0] = (byte) 81;
      numArray4[3] = (byte) 224 /*0xE0*/;
      numArray4[1] = (byte) 134;
      numArray4[12] = (byte) 20;
      numArray4[50] = (byte) 195;
      numArray4[48 /*0x30*/] = (byte) 31 /*0x1F*/;
      numArray4[19] = (byte) 146;
      numArray4[16 /*0x10*/] = (byte) 150;
      numArray4[17] = (byte) 90;
      numArray4[18] = (byte) 33;
      numArray4[44] = (byte) 107;
      numArray4[20] = (byte) 70;
      numArray4[21] = (byte) 230;
      numArray4[4] = (byte) 62;
      numArray4[37] = (byte) 184;
      numArray4[26] = (byte) 174;
      numArray4[15] = (byte) 232;
      numArray4[42] = (byte) 48 /*0x30*/;
      numArray4[27] = (byte) 155;
      numArray4[10] = (byte) 148;
      numArray4[36] = (byte) 100;
      numArray4[30] = (byte) 208 /*0xD0*/;
      numArray4[31 /*0x1F*/] = (byte) 189;
      numArray4[8] = (byte) 177;
      numArray4[32 /*0x20*/] = (byte) 52;
      numArray4[34] = (byte) 179;
      numArray4[35] = (byte) 72;
      numArray4[45] = (byte) 194;
      numArray4[13] = (byte) 240 /*0xF0*/;
      numArray4[38] = (byte) 84;
      numArray4[39] = (byte) 65;
      numArray4[40] = (byte) 11;
      numArray4[41] = (byte) 237;
      numArray4[51] = (byte) 109;
      numArray4[43] = (byte) 203;
      numArray4[24] = (byte) 84;
      numArray4[9] = (byte) 82;
      numArray4[46] = (byte) 33;
      numArray4[47] = (byte) 33;
      numArray4[28] = (byte) 19;
      numArray4[49] = (byte) 132;
      numArray4[11] = (byte) 225;
      numArray4[25] = (byte) 29;
      numArray4[29] = (byte) 199;
      numArray4[33] = (byte) 53;
      numArray4[22] = (byte) 236;
      byte[] numArray5 = new byte[55]
      {
        (byte) 74,
        (byte) 103,
        (byte) 195,
        (byte) 49,
        (byte) 177,
        (byte) 131,
        (byte) 141,
        (byte) 160 /*0xA0*/,
        (byte) 211,
        (byte) 40,
        (byte) 120,
        (byte) 186,
        (byte) 188,
        (byte) 44,
        (byte) 19,
        (byte) 202,
        (byte) 226,
        (byte) 48 /*0x30*/,
        (byte) 243,
        (byte) 238,
        (byte) 190,
        (byte) 86,
        (byte) 25,
        (byte) 76,
        (byte) 83,
        (byte) 131,
        (byte) 68,
        (byte) 60,
        (byte) 139,
        (byte) 108,
        (byte) 195,
        (byte) 225,
        (byte) 186,
        (byte) 76,
        (byte) 235,
        (byte) 86,
        (byte) 251,
        (byte) 98,
        (byte) 100,
        (byte) 27,
        (byte) 241,
        (byte) 245,
        (byte) 165,
        (byte) 133,
        (byte) 209,
        (byte) 247,
        (byte) 147,
        (byte) 155,
        (byte) 108,
        (byte) 11,
        (byte) 17,
        (byte) 175,
        (byte) 219,
        (byte) 119,
        (byte) 245
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[2]{ (byte) 0, (byte) 250 };
      numArray6[0] = (byte) 50;
      byte[] numArray7 = new byte[2]
      {
        (byte) 235,
        (byte) 51
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[112 /*0x70*/];
    byte[] numArray9 = new byte[55];
    numArray9[30] = (byte) 123;
    numArray9[0] = (byte) 29;
    numArray9[24] = (byte) 196;
    numArray9[3] = (byte) 142;
    numArray9[23] = (byte) 167;
    numArray9[5] = (byte) 221;
    numArray9[6] = (byte) 90;
    numArray9[7] = (byte) 176 /*0xB0*/;
    numArray9[38] = (byte) 238;
    numArray9[19] = (byte) 142;
    numArray9[11] = (byte) 24;
    numArray9[49] = (byte) 126;
    numArray9[12] = (byte) 111;
    numArray9[25] = (byte) 147;
    numArray9[14] = (byte) 117;
    numArray9[20] = (byte) 29;
    numArray9[34] = (byte) 44;
    numArray9[17] = (byte) 169;
    numArray9[46] = (byte) 68;
    numArray9[28] = (byte) 50;
    numArray9[52] = (byte) 19;
    numArray9[4] = (byte) 244;
    numArray9[31 /*0x1F*/] = (byte) 150;
    numArray9[2] = (byte) 129;
    numArray9[16 /*0x10*/] = (byte) 185;
    numArray9[37] = (byte) 179;
    numArray9[18] = (byte) 233;
    numArray9[51] = (byte) 115;
    numArray9[42] = (byte) 148;
    numArray9[41] = (byte) 53;
    numArray9[15] = (byte) 123;
    numArray9[40] = (byte) 166;
    numArray9[32 /*0x20*/] = (byte) 35;
    numArray9[22] = (byte) 167;
    numArray9[33] = (byte) 96 /*0x60*/;
    numArray9[35] = (byte) 127 /*0x7F*/;
    numArray9[36] = (byte) 149;
    numArray9[44] = (byte) 143;
    numArray9[10] = (byte) 243;
    numArray9[26] = (byte) 53;
    numArray9[8] = (byte) 210;
    numArray9[13] = (byte) 214;
    numArray9[29] = (byte) 13;
    numArray9[43] = (byte) 210;
    numArray9[9] = (byte) 72;
    numArray9[45] = (byte) 116;
    numArray9[39] = (byte) 181;
    numArray9[47] = (byte) 163;
    numArray9[48 /*0x30*/] = (byte) 189;
    numArray9[21] = (byte) 251;
    numArray9[50] = (byte) 108;
    numArray9[1] = (byte) 20;
    numArray9[27] = (byte) 246;
    numArray9[53] = (byte) 143;
    numArray9[54] = (byte) 200;
    byte[] numArray10 = new byte[55];
    numArray10[24] = (byte) 4;
    numArray10[27] = (byte) 10;
    numArray10[33] = (byte) 236;
    numArray10[37] = (byte) 113;
    numArray10[4] = (byte) 74;
    numArray10[51] = (byte) 254;
    numArray10[40] = (byte) 217;
    numArray10[7] = (byte) 122;
    numArray10[0] = (byte) 59;
    numArray10[5] = (byte) 85;
    numArray10[49] = (byte) 237;
    numArray10[11] = (byte) 181;
    numArray10[31 /*0x1F*/] = (byte) 138;
    numArray10[9] = (byte) 189;
    numArray10[14] = (byte) 119;
    numArray10[15] = (byte) 48 /*0x30*/;
    numArray10[16 /*0x10*/] = (byte) 99;
    numArray10[18] = (byte) 149;
    numArray10[12] = (byte) 38;
    numArray10[36] = (byte) 156;
    numArray10[20] = (byte) 177;
    numArray10[21] = (byte) 150;
    numArray10[22] = (byte) 89;
    numArray10[23] = (byte) 55;
    numArray10[47] = (byte) 118;
    numArray10[25] = (byte) 48 /*0x30*/;
    numArray10[26] = (byte) 93;
    numArray10[44] = (byte) 85;
    numArray10[43] = (byte) 190;
    numArray10[35] = (byte) 186;
    numArray10[28] = (byte) 85;
    numArray10[48 /*0x30*/] = (byte) 152;
    numArray10[6] = (byte) 213;
    numArray10[3] = (byte) 17;
    numArray10[34] = (byte) 254;
    numArray10[13] = (byte) 234;
    numArray10[38] = (byte) 207;
    numArray10[19] = (byte) 248;
    numArray10[17] = (byte) 98;
    numArray10[30] = (byte) 18;
    numArray10[29] = (byte) 66;
    numArray10[41] = (byte) 117;
    numArray10[42] = (byte) 199;
    numArray10[10] = (byte) 21;
    numArray10[52] = (byte) 178;
    numArray10[45] = (byte) 50;
    numArray10[46] = (byte) 130;
    numArray10[32 /*0x20*/] = (byte) 213;
    numArray10[39] = (byte) 182;
    numArray10[1] = (byte) 28;
    numArray10[2] = (byte) 59;
    numArray10[50] = (byte) 236;
    numArray10[8] = (byte) 205;
    numArray10[53] = (byte) 212;
    numArray10[54] = (byte) 8;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 221,
      (byte) 169,
      (byte) 248,
      (byte) 124,
      (byte) 184,
      (byte) 176 /*0xB0*/,
      (byte) 233,
      (byte) 119,
      (byte) 53,
      (byte) 113,
      (byte) 193,
      (byte) 80 /*0x50*/,
      (byte) 104,
      (byte) 49,
      (byte) 172,
      (byte) 177,
      (byte) 45,
      (byte) 143,
      (byte) 69,
      (byte) 236,
      (byte) 145,
      (byte) 137,
      (byte) 177,
      (byte) 92,
      (byte) 64 /*0x40*/,
      (byte) 141,
      (byte) 173,
      (byte) 239,
      (byte) 192 /*0xC0*/,
      (byte) 233,
      (byte) 237,
      (byte) 7,
      (byte) 44,
      (byte) 179,
      (byte) 27,
      (byte) 197,
      (byte) 129,
      (byte) 123,
      (byte) 94,
      (byte) 59,
      (byte) 194,
      (byte) 199,
      (byte) 37,
      (byte) 173,
      (byte) 251,
      (byte) 9,
      (byte) 123,
      (byte) 59,
      (byte) 158,
      (byte) 219,
      (byte) 140,
      (byte) 112 /*0x70*/,
      (byte) 105,
      (byte) 54,
      (byte) 54
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 245,
      (byte) 85,
      (byte) 229,
      (byte) 220,
      (byte) 194,
      (byte) 66,
      (byte) 38,
      (byte) 37,
      (byte) 51,
      (byte) 227,
      (byte) 192 /*0xC0*/,
      (byte) 220,
      (byte) 125,
      (byte) 160 /*0xA0*/,
      (byte) 175,
      (byte) 139,
      (byte) 119,
      (byte) 58,
      (byte) 118,
      (byte) 76,
      (byte) 192 /*0xC0*/,
      (byte) 232,
      (byte) 15,
      (byte) 99,
      (byte) 27,
      (byte) 60,
      (byte) 249,
      (byte) 216,
      (byte) 161,
      (byte) 50,
      (byte) 156,
      (byte) 127 /*0x7F*/,
      (byte) 90,
      (byte) 218,
      (byte) 48 /*0x30*/,
      (byte) 160 /*0xA0*/,
      (byte) 26,
      (byte) 42,
      (byte) 104,
      (byte) 254,
      (byte) 36,
      (byte) 238,
      (byte) 184,
      (byte) 74,
      (byte) 1,
      (byte) 22,
      (byte) 247,
      (byte) 7,
      (byte) 125,
      (byte) 32 /*0x20*/,
      (byte) 13,
      (byte) 244,
      (byte) 12,
      (byte) 48 /*0x30*/,
      (byte) 74
    };
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[2]{ (byte) 156, (byte) 60 };
    byte[] numArray14 = new byte[2]{ (byte) 81, (byte) 148 };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 2);
    for (int index = 0; index < 2; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_appserver_13283()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[114];
      byte[] numArray2 = new byte[55];
      numArray2[53] = (byte) 168;
      numArray2[1] = (byte) 132;
      numArray2[2] = (byte) 185;
      numArray2[28] = (byte) 229;
      numArray2[4] = (byte) 181;
      numArray2[17] = (byte) 17;
      numArray2[54] = (byte) 209;
      numArray2[9] = (byte) 48 /*0x30*/;
      numArray2[40] = (byte) 183;
      numArray2[25] = (byte) 124;
      numArray2[10] = (byte) 9;
      numArray2[11] = (byte) 41;
      numArray2[38] = (byte) 198;
      numArray2[13] = (byte) 100;
      numArray2[14] = (byte) 3;
      numArray2[0] = (byte) 75;
      numArray2[48 /*0x30*/] = (byte) 244;
      numArray2[3] = (byte) 35;
      numArray2[18] = (byte) 113;
      numArray2[19] = (byte) 81;
      numArray2[20] = (byte) 229;
      numArray2[21] = (byte) 93;
      numArray2[5] = (byte) 22;
      numArray2[6] = (byte) 230;
      numArray2[24] = (byte) 245;
      numArray2[47] = (byte) 239;
      numArray2[26] = (byte) 199;
      numArray2[27] = (byte) 205;
      numArray2[32 /*0x20*/] = (byte) 200;
      numArray2[51] = (byte) 24;
      numArray2[29] = (byte) 63 /*0x3F*/;
      numArray2[31 /*0x1F*/] = (byte) 56;
      numArray2[23] = (byte) 157;
      numArray2[46] = (byte) 108;
      numArray2[34] = (byte) 218;
      numArray2[35] = (byte) 41;
      numArray2[7] = (byte) 47;
      numArray2[37] = (byte) 229;
      numArray2[39] = (byte) 37;
      numArray2[22] = (byte) 124;
      numArray2[12] = (byte) 202;
      numArray2[41] = (byte) 172;
      numArray2[42] = (byte) 218;
      numArray2[30] = (byte) 52;
      numArray2[43] = (byte) 64 /*0x40*/;
      numArray2[45] = (byte) 12;
      numArray2[16 /*0x10*/] = (byte) 54;
      numArray2[33] = (byte) 15;
      numArray2[15] = (byte) 97;
      numArray2[49] = (byte) 58;
      numArray2[50] = (byte) 5;
      numArray2[8] = (byte) 177;
      numArray2[36] = (byte) 65;
      numArray2[44] = (byte) 96 /*0x60*/;
      numArray2[52] = (byte) 145;
      byte[] numArray3 = new byte[55];
      numArray3[52] = (byte) 91;
      numArray3[4] = (byte) 74;
      numArray3[2] = (byte) 196;
      numArray3[48 /*0x30*/] = (byte) 4;
      numArray3[30] = (byte) 252;
      numArray3[17] = (byte) 218;
      numArray3[3] = (byte) 33;
      numArray3[10] = (byte) 232;
      numArray3[8] = (byte) 121;
      numArray3[38] = (byte) 164;
      numArray3[9] = (byte) 100;
      numArray3[11] = (byte) 145;
      numArray3[14] = (byte) 49;
      numArray3[13] = (byte) 231;
      numArray3[41] = (byte) 141;
      numArray3[15] = (byte) 80 /*0x50*/;
      numArray3[12] = (byte) 126;
      numArray3[49] = (byte) 220;
      numArray3[18] = (byte) 221;
      numArray3[19] = (byte) 104;
      numArray3[5] = (byte) 181;
      numArray3[21] = (byte) 248;
      numArray3[22] = (byte) 173;
      numArray3[23] = (byte) 195;
      numArray3[7] = (byte) 241;
      numArray3[25] = (byte) 202;
      numArray3[26] = (byte) 248;
      numArray3[27] = (byte) 36;
      numArray3[42] = (byte) 98;
      numArray3[29] = (byte) 44;
      numArray3[0] = (byte) 193;
      numArray3[20] = (byte) 67;
      numArray3[28] = (byte) 193;
      numArray3[31 /*0x1F*/] = (byte) 5;
      numArray3[34] = (byte) 200;
      numArray3[35] = (byte) 252;
      numArray3[36] = (byte) 92;
      numArray3[37] = (byte) 12;
      numArray3[6] = (byte) 137;
      numArray3[32 /*0x20*/] = (byte) 51;
      numArray3[1] = (byte) 62;
      numArray3[53] = (byte) 110;
      numArray3[46] = (byte) 206;
      numArray3[43] = (byte) 65;
      numArray3[24] = (byte) 192 /*0xC0*/;
      numArray3[45] = (byte) 208 /*0xD0*/;
      numArray3[16 /*0x10*/] = (byte) 83;
      numArray3[47] = (byte) 27;
      numArray3[39] = (byte) 127 /*0x7F*/;
      numArray3[44] = (byte) 104;
      numArray3[50] = (byte) 176 /*0xB0*/;
      numArray3[51] = (byte) 0;
      numArray3[33] = (byte) 87;
      numArray3[40] = (byte) 50;
      numArray3[54] = (byte) 166;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[12] = (byte) 96 /*0x60*/;
      numArray4[1] = (byte) 8;
      numArray4[22] = (byte) 210;
      numArray4[39] = (byte) 183;
      numArray4[25] = (byte) 90;
      numArray4[5] = (byte) 12;
      numArray4[19] = (byte) 232;
      numArray4[7] = (byte) 146;
      numArray4[2] = (byte) 201;
      numArray4[9] = (byte) 46;
      numArray4[29] = (byte) 147;
      numArray4[44] = (byte) 94;
      numArray4[47] = (byte) 110;
      numArray4[53] = (byte) 105;
      numArray4[14] = (byte) 38;
      numArray4[40] = (byte) 128 /*0x80*/;
      numArray4[16 /*0x10*/] = (byte) 24;
      numArray4[37] = (byte) 87;
      numArray4[31 /*0x1F*/] = (byte) 227;
      numArray4[43] = (byte) 60;
      numArray4[20] = (byte) 148;
      numArray4[21] = (byte) 73;
      numArray4[52] = (byte) 176 /*0xB0*/;
      numArray4[26] = (byte) 112 /*0x70*/;
      numArray4[24] = (byte) 170;
      numArray4[4] = (byte) 131;
      numArray4[11] = (byte) 28;
      numArray4[18] = (byte) 18;
      numArray4[28] = (byte) 61;
      numArray4[10] = (byte) 215;
      numArray4[13] = (byte) 178;
      numArray4[0] = (byte) 47;
      numArray4[32 /*0x20*/] = (byte) 47;
      numArray4[41] = (byte) 209;
      numArray4[34] = (byte) 24;
      numArray4[35] = (byte) 106;
      numArray4[36] = (byte) 157;
      numArray4[30] = byte.MaxValue;
      numArray4[38] = (byte) 217;
      numArray4[50] = (byte) 181;
      numArray4[17] = (byte) 159;
      numArray4[33] = (byte) 132;
      numArray4[42] = (byte) 156;
      numArray4[15] = (byte) 86;
      numArray4[3] = (byte) 5;
      numArray4[6] = (byte) 177;
      numArray4[46] = (byte) 2;
      numArray4[23] = (byte) 41;
      numArray4[48 /*0x30*/] = (byte) 245;
      numArray4[27] = (byte) 106;
      numArray4[8] = (byte) 200;
      numArray4[51] = (byte) 114;
      numArray4[49] = (byte) 186;
      numArray4[45] = (byte) 171;
      numArray4[54] = (byte) 62;
      byte[] numArray5 = new byte[55]
      {
        (byte) 21,
        (byte) 119,
        (byte) 23,
        (byte) 167,
        (byte) 0,
        (byte) 113,
        (byte) 160 /*0xA0*/,
        (byte) 223,
        (byte) 132,
        (byte) 218,
        (byte) 45,
        (byte) 84,
        (byte) 73,
        (byte) 79,
        (byte) 204,
        (byte) 100,
        (byte) 236,
        (byte) 67,
        (byte) 16 /*0x10*/,
        (byte) 104,
        (byte) 230,
        (byte) 60,
        (byte) 85,
        (byte) 19,
        (byte) 230,
        (byte) 27,
        (byte) 210,
        (byte) 35,
        (byte) 72,
        (byte) 16 /*0x10*/,
        (byte) 212,
        (byte) 41,
        (byte) 172,
        (byte) 72,
        (byte) 63 /*0x3F*/,
        (byte) 202,
        (byte) 119,
        (byte) 111,
        (byte) 105,
        (byte) 36,
        (byte) 14,
        (byte) 143,
        (byte) 176 /*0xB0*/,
        (byte) 21,
        (byte) 110,
        (byte) 252,
        (byte) 47,
        (byte) 246,
        (byte) 110,
        (byte) 86,
        (byte) 105,
        (byte) 94,
        (byte) 84,
        (byte) 126,
        (byte) 62
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[4]
      {
        (byte) 213,
        (byte) 93,
        (byte) 208 /*0xD0*/,
        (byte) 153
      };
      byte[] numArray7 = new byte[4]
      {
        (byte) 72,
        (byte) 180,
        (byte) 59,
        (byte) 77
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[42];
      byte[] response = new byte[42];
      Array.Copy((Array) sc_13274.sspq, 35, (Array) numArray8, 0, 42);
      key.Query(true, 335, numArray8, response);
      Array.Copy((Array) sc_13274.sspr, 35, (Array) numArray8, 0, 42);
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
    byte[] numArray9 = new byte[114];
    byte[] numArray10 = new byte[55];
    numArray10[29] = (byte) 192 /*0xC0*/;
    numArray10[37] = (byte) 5;
    numArray10[6] = (byte) 29;
    numArray10[2] = (byte) 51;
    numArray10[45] = (byte) 167;
    numArray10[5] = (byte) 116;
    numArray10[17] = (byte) 241;
    numArray10[7] = (byte) 158;
    numArray10[8] = (byte) 107;
    numArray10[11] = (byte) 107;
    numArray10[10] = (byte) 80 /*0x50*/;
    numArray10[46] = (byte) 169;
    numArray10[12] = (byte) 193;
    numArray10[22] = (byte) 198;
    numArray10[14] = (byte) 97;
    numArray10[49] = (byte) 197;
    numArray10[16 /*0x10*/] = (byte) 108;
    numArray10[26] = (byte) 182;
    numArray10[18] = (byte) 147;
    numArray10[19] = (byte) 38;
    numArray10[53] = (byte) 8;
    numArray10[9] = (byte) 149;
    numArray10[36] = (byte) 151;
    numArray10[28] = (byte) 161;
    numArray10[24] = (byte) 49;
    numArray10[25] = (byte) 29;
    numArray10[4] = (byte) 22;
    numArray10[27] = (byte) 167;
    numArray10[13] = (byte) 227;
    numArray10[3] = (byte) 244;
    numArray10[30] = (byte) 136;
    numArray10[31 /*0x1F*/] = (byte) 59;
    numArray10[32 /*0x20*/] = (byte) 187;
    numArray10[33] = (byte) 72;
    numArray10[15] = (byte) 203;
    numArray10[35] = (byte) 242;
    numArray10[51] = (byte) 194;
    numArray10[21] = (byte) 189;
    numArray10[23] = (byte) 157;
    numArray10[39] = (byte) 179;
    numArray10[40] = (byte) 129;
    numArray10[41] = (byte) 64 /*0x40*/;
    numArray10[42] = (byte) 22;
    numArray10[43] = (byte) 210;
    numArray10[44] = (byte) 88;
    numArray10[0] = (byte) 22;
    numArray10[20] = (byte) 54;
    numArray10[47] = (byte) 198;
    numArray10[48 /*0x30*/] = (byte) 55;
    numArray10[1] = (byte) 211;
    numArray10[50] = (byte) 0;
    numArray10[34] = (byte) 199;
    numArray10[52] = (byte) 178;
    numArray10[38] = (byte) 126;
    numArray10[54] = (byte) 97;
    byte[] numArray11 = new byte[55]
    {
      (byte) 228,
      (byte) 84,
      (byte) 236,
      (byte) 33,
      (byte) 12,
      (byte) 96 /*0x60*/,
      (byte) 201,
      (byte) 210,
      (byte) 188,
      (byte) 12,
      (byte) 125,
      (byte) 225,
      (byte) 59,
      (byte) 251,
      (byte) 217,
      (byte) 215,
      (byte) 152,
      (byte) 46,
      (byte) 44,
      (byte) 15,
      (byte) 109,
      (byte) 197,
      (byte) 172,
      (byte) 233,
      (byte) 235,
      (byte) 233,
      (byte) 60,
      (byte) 144 /*0x90*/,
      (byte) 92,
      (byte) 71,
      (byte) 164,
      (byte) 67,
      (byte) 104,
      (byte) 52,
      (byte) 48 /*0x30*/,
      (byte) 159,
      (byte) 217,
      (byte) 168,
      (byte) 185,
      (byte) 184,
      (byte) 212,
      (byte) 146,
      (byte) 120,
      (byte) 83,
      (byte) 188,
      (byte) 90,
      (byte) 92,
      (byte) 32 /*0x20*/,
      (byte) 160 /*0xA0*/,
      (byte) 120,
      (byte) 139,
      (byte) 39,
      (byte) 203,
      (byte) 185,
      (byte) 189
    };
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray9, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index] ^= numArray11[index];
    byte[] numArray12 = new byte[55]
    {
      (byte) 80 /*0x50*/,
      (byte) 56,
      (byte) 133,
      (byte) 206,
      (byte) 5,
      (byte) 33,
      (byte) 234,
      (byte) 71,
      (byte) 139,
      (byte) 252,
      (byte) 146,
      (byte) 173,
      (byte) 137,
      (byte) 51,
      (byte) 36,
      (byte) 237,
      (byte) 235,
      (byte) 154,
      (byte) 178,
      (byte) 59,
      (byte) 46,
      (byte) 1,
      (byte) 146,
      (byte) 67,
      (byte) 69,
      (byte) 168,
      (byte) 74,
      (byte) 39,
      (byte) 118,
      (byte) 183,
      (byte) 80 /*0x50*/,
      (byte) 123,
      (byte) 188,
      (byte) 204,
      (byte) 176 /*0xB0*/,
      (byte) 9,
      byte.MaxValue,
      (byte) 38,
      (byte) 5,
      (byte) 33,
      (byte) 129,
      (byte) 125,
      (byte) 183,
      (byte) 250,
      (byte) 158,
      (byte) 252,
      (byte) 62,
      (byte) 94,
      (byte) 116,
      (byte) 98,
      (byte) 231,
      (byte) 200,
      (byte) 185,
      (byte) 117,
      (byte) 234
    };
    byte[] numArray13 = new byte[55];
    numArray13[26] = (byte) 207;
    numArray13[15] = (byte) 81;
    numArray13[2] = (byte) 108;
    numArray13[3] = (byte) 233;
    numArray13[4] = (byte) 129;
    numArray13[16 /*0x10*/] = (byte) 92;
    numArray13[43] = (byte) 174;
    numArray13[46] = (byte) 114;
    numArray13[8] = (byte) 111;
    numArray13[23] = (byte) 22;
    numArray13[7] = (byte) 151;
    numArray13[11] = (byte) 94;
    numArray13[38] = (byte) 119;
    numArray13[32 /*0x20*/] = (byte) 224 /*0xE0*/;
    numArray13[51] = (byte) 39;
    numArray13[50] = (byte) 184;
    numArray13[22] = (byte) 141;
    numArray13[17] = (byte) 168;
    numArray13[18] = (byte) 163;
    numArray13[19] = (byte) 198;
    numArray13[27] = (byte) 252;
    numArray13[21] = (byte) 64 /*0x40*/;
    numArray13[20] = (byte) 110;
    numArray13[5] = (byte) 108;
    numArray13[54] = (byte) 24;
    numArray13[10] = (byte) 212;
    numArray13[12] = (byte) 211;
    numArray13[1] = (byte) 164;
    numArray13[33] = (byte) 248;
    numArray13[6] = (byte) 79;
    numArray13[28] = (byte) 183;
    numArray13[31 /*0x1F*/] = (byte) 52;
    numArray13[24] = (byte) 92;
    numArray13[40] = (byte) 196;
    numArray13[34] = (byte) 214;
    numArray13[35] = (byte) 246;
    numArray13[14] = (byte) 61;
    numArray13[9] = (byte) 117;
    numArray13[37] = (byte) 104;
    numArray13[44] = (byte) 158;
    numArray13[0] = (byte) 171;
    numArray13[41] = (byte) 90;
    numArray13[42] = (byte) 222;
    numArray13[25] = (byte) 34;
    numArray13[39] = (byte) 99;
    numArray13[47] = (byte) 215;
    numArray13[36] = (byte) 194;
    numArray13[13] = (byte) 13;
    numArray13[48 /*0x30*/] = (byte) 92;
    numArray13[49] = (byte) 170;
    numArray13[45] = (byte) 68;
    numArray13[30] = (byte) 91;
    numArray13[52] = (byte) 73;
    numArray13[53] = (byte) 55;
    numArray13[29] = (byte) 203;
    key.Query(true, 335, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray9, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray9[index + 55] ^= numArray13[index];
    byte[] numArray14 = new byte[4]
    {
      (byte) 157,
      (byte) 63 /*0x3F*/,
      (byte) 243,
      (byte) 234
    };
    byte[] numArray15 = new byte[4]
    {
      (byte) 0,
      (byte) 0,
      (byte) 169,
      (byte) 0
    };
    numArray15[0] = (byte) 59;
    numArray15[1] = (byte) 99;
    numArray15[3] = (byte) 144 /*0x90*/;
    key.Query(true, 335, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray9, 110, 4);
    for (int index = 0; index < 4; ++index)
      numArray9[index + 110] ^= numArray15[index];
    return Encoding.UTF8.GetString(numArray9);
  }

  internal static string ssp_appserver_13284()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[68];
      byte[] numArray2 = new byte[55];
      numArray2[33] = (byte) 185;
      numArray2[34] = (byte) 163;
      numArray2[17] = (byte) 151;
      numArray2[5] = (byte) 252;
      numArray2[9] = (byte) 30;
      numArray2[2] = (byte) 38;
      numArray2[6] = (byte) 4;
      numArray2[18] = (byte) 140;
      numArray2[31 /*0x1F*/] = (byte) 97;
      numArray2[38] = (byte) 228;
      numArray2[11] = (byte) 233;
      numArray2[28] = (byte) 222;
      numArray2[42] = (byte) 195;
      numArray2[37] = (byte) 70;
      numArray2[14] = (byte) 158;
      numArray2[47] = (byte) 183;
      numArray2[16 /*0x10*/] = (byte) 84;
      numArray2[13] = (byte) 89;
      numArray2[15] = (byte) 173;
      numArray2[8] = (byte) 73;
      numArray2[20] = (byte) 91;
      numArray2[21] = (byte) 86;
      numArray2[19] = (byte) 167;
      numArray2[24] = (byte) 180;
      numArray2[1] = (byte) 48 /*0x30*/;
      numArray2[25] = (byte) 97;
      numArray2[26] = (byte) 173;
      numArray2[4] = (byte) 68;
      numArray2[51] = (byte) 189;
      numArray2[29] = (byte) 43;
      numArray2[30] = (byte) 49;
      numArray2[7] = (byte) 102;
      numArray2[32 /*0x20*/] = (byte) 163;
      numArray2[23] = (byte) 94;
      numArray2[52] = (byte) 29;
      numArray2[35] = (byte) 68;
      numArray2[22] = (byte) 31 /*0x1F*/;
      numArray2[45] = (byte) 9;
      numArray2[3] = (byte) 236;
      numArray2[39] = (byte) 189;
      numArray2[27] = (byte) 238;
      numArray2[41] = (byte) 158;
      numArray2[40] = (byte) 53;
      numArray2[43] = (byte) 127 /*0x7F*/;
      numArray2[44] = (byte) 0;
      numArray2[36] = (byte) 251;
      numArray2[10] = (byte) 62;
      numArray2[0] = (byte) 182;
      numArray2[48 /*0x30*/] = (byte) 31 /*0x1F*/;
      numArray2[49] = (byte) 86;
      numArray2[50] = (byte) 206;
      numArray2[46] = (byte) 217;
      numArray2[12] = (byte) 16 /*0x10*/;
      numArray2[53] = (byte) 52;
      numArray2[54] = (byte) 198;
      byte[] numArray3 = new byte[55]
      {
        (byte) 34,
        (byte) 55,
        (byte) 75,
        (byte) 185,
        (byte) 149,
        (byte) 184,
        (byte) 136,
        (byte) 244,
        (byte) 180,
        (byte) 83,
        (byte) 72,
        (byte) 41,
        (byte) 156,
        (byte) 182,
        (byte) 64 /*0x40*/,
        (byte) 58,
        (byte) 171,
        (byte) 103,
        (byte) 164,
        (byte) 86,
        (byte) 105,
        (byte) 186,
        (byte) 45,
        (byte) 243,
        (byte) 240 /*0xF0*/,
        (byte) 60,
        (byte) 192 /*0xC0*/,
        (byte) 74,
        (byte) 251,
        (byte) 11,
        (byte) 58,
        (byte) 17,
        (byte) 163,
        (byte) 0,
        (byte) 177,
        (byte) 6,
        (byte) 86,
        (byte) 122,
        (byte) 64 /*0x40*/,
        (byte) 168,
        (byte) 9,
        (byte) 22,
        (byte) 72,
        (byte) 215,
        (byte) 194,
        (byte) 186,
        (byte) 40,
        (byte) 76,
        (byte) 115,
        (byte) 142,
        (byte) 14,
        (byte) 162,
        (byte) 38,
        (byte) 234,
        (byte) 79
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13]
      {
        (byte) 106,
        (byte) 141,
        (byte) 90,
        (byte) 88,
        (byte) 70,
        byte.MaxValue,
        (byte) 194,
        (byte) 122,
        (byte) 152,
        (byte) 214,
        (byte) 100,
        (byte) 113,
        (byte) 39
      };
      byte[] numArray5 = new byte[13];
      numArray5[7] = (byte) 252;
      numArray5[0] = (byte) 90;
      numArray5[11] = (byte) 223;
      numArray5[9] = (byte) 168;
      numArray5[8] = (byte) 8;
      numArray5[5] = (byte) 33;
      numArray5[6] = (byte) 182;
      numArray5[2] = (byte) 31 /*0x1F*/;
      numArray5[3] = (byte) 231;
      numArray5[4] = (byte) 3;
      numArray5[1] = (byte) 52;
      numArray5[10] = (byte) 254;
      numArray5[12] = (byte) 118;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[68];
    byte[] numArray7 = new byte[55]
    {
      (byte) 245,
      (byte) 173,
      (byte) 234,
      (byte) 118,
      (byte) 192 /*0xC0*/,
      (byte) 200,
      (byte) 129,
      (byte) 166,
      (byte) 22,
      (byte) 216,
      (byte) 23,
      (byte) 227,
      (byte) 24,
      (byte) 138,
      (byte) 236,
      (byte) 29,
      (byte) 143,
      (byte) 159,
      (byte) 36,
      (byte) 192 /*0xC0*/,
      (byte) 89,
      (byte) 238,
      (byte) 111,
      (byte) 98,
      (byte) 222,
      (byte) 101,
      (byte) 48 /*0x30*/,
      (byte) 165,
      (byte) 83,
      (byte) 98,
      (byte) 62,
      (byte) 27,
      (byte) 38,
      (byte) 17,
      (byte) 163,
      (byte) 41,
      (byte) 122,
      (byte) 250,
      (byte) 103,
      (byte) 182,
      (byte) 126,
      (byte) 10,
      (byte) 72,
      (byte) 121,
      (byte) 143,
      (byte) 10,
      (byte) 244,
      (byte) 175,
      (byte) 250,
      (byte) 233,
      (byte) 101,
      (byte) 105,
      (byte) 191,
      (byte) 219,
      (byte) 15
    };
    byte[] numArray8 = new byte[55];
    numArray8[0] = (byte) 43;
    numArray8[1] = (byte) 242;
    numArray8[11] = (byte) 212;
    numArray8[3] = (byte) 74;
    numArray8[4] = (byte) 155;
    numArray8[5] = (byte) 160 /*0xA0*/;
    numArray8[20] = (byte) 150;
    numArray8[7] = (byte) 216;
    numArray8[8] = (byte) 239;
    numArray8[9] = (byte) 47;
    numArray8[41] = (byte) 189;
    numArray8[44] = (byte) 212;
    numArray8[18] = (byte) 79;
    numArray8[13] = (byte) 182;
    numArray8[46] = (byte) 47;
    numArray8[19] = (byte) 26;
    numArray8[27] = (byte) 231;
    numArray8[25] = (byte) 206;
    numArray8[53] = (byte) 86;
    numArray8[2] = (byte) 117;
    numArray8[35] = (byte) 200;
    numArray8[21] = (byte) 84;
    numArray8[22] = (byte) 2;
    numArray8[16 /*0x10*/] = (byte) 243;
    numArray8[49] = (byte) 100;
    numArray8[32 /*0x20*/] = (byte) 74;
    numArray8[38] = (byte) 69;
    numArray8[15] = (byte) 55;
    numArray8[28] = (byte) 239;
    numArray8[17] = (byte) 3;
    numArray8[26] = (byte) 251;
    numArray8[31 /*0x1F*/] = (byte) 77;
    numArray8[29] = (byte) 11;
    numArray8[37] = (byte) 128 /*0x80*/;
    numArray8[24] = (byte) 58;
    numArray8[33] = (byte) 154;
    numArray8[36] = (byte) 77;
    numArray8[30] = (byte) 125;
    numArray8[12] = (byte) 174;
    numArray8[10] = (byte) 23;
    numArray8[23] = (byte) 21;
    numArray8[51] = (byte) 231;
    numArray8[43] = (byte) 204;
    numArray8[42] = (byte) 135;
    numArray8[40] = (byte) 166;
    numArray8[45] = (byte) 40;
    numArray8[6] = (byte) 217;
    numArray8[47] = (byte) 102;
    numArray8[48 /*0x30*/] = (byte) 85;
    numArray8[14] = (byte) 95;
    numArray8[50] = (byte) 26;
    numArray8[39] = (byte) 133;
    numArray8[52] = (byte) 252;
    numArray8[34] = (byte) 57;
    numArray8[54] = (byte) 146;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[13];
    numArray9[1] = (byte) 195;
    numArray9[3] = (byte) 24;
    numArray9[12] = (byte) 215;
    numArray9[9] = (byte) 113;
    numArray9[11] = (byte) 75;
    numArray9[5] = (byte) 232;
    numArray9[0] = (byte) 179;
    numArray9[7] = (byte) 197;
    numArray9[6] = (byte) 182;
    numArray9[2] = (byte) 234;
    numArray9[10] = (byte) 4;
    numArray9[8] = (byte) 159;
    numArray9[4] = (byte) 49;
    byte[] numArray10 = new byte[13]
    {
      (byte) 58,
      (byte) 250,
      (byte) 55,
      (byte) 116,
      (byte) 244,
      (byte) 116,
      (byte) 87,
      (byte) 30,
      (byte) 101,
      (byte) 160 /*0xA0*/,
      (byte) 192 /*0xC0*/,
      (byte) 242,
      (byte) 91
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 13);
    for (int index = 0; index < 13; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_13285()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/];
      numArray2[28] = (byte) 95;
      numArray2[1] = (byte) 65;
      numArray2[21] = (byte) 249;
      numArray2[3] = (byte) 163;
      numArray2[11] = (byte) 25;
      numArray2[25] = (byte) 115;
      numArray2[24] = (byte) 50;
      numArray2[13] = (byte) 220;
      numArray2[10] = (byte) 221;
      numArray2[5] = (byte) 159;
      numArray2[2] = (byte) 58;
      numArray2[4] = (byte) 62;
      numArray2[12] = (byte) 243;
      numArray2[30] = (byte) 196;
      numArray2[14] = (byte) 233;
      numArray2[18] = (byte) 159;
      numArray2[16 /*0x10*/] = (byte) 95;
      numArray2[9] = (byte) 206;
      numArray2[23] = (byte) 80 /*0x50*/;
      numArray2[19] = (byte) 6;
      numArray2[20] = (byte) 2;
      numArray2[6] = (byte) 34;
      numArray2[7] = (byte) 6;
      numArray2[22] = (byte) 41;
      numArray2[17] = (byte) 149;
      numArray2[0] = (byte) 178;
      numArray2[8] = (byte) 113;
      numArray2[27] = (byte) 103;
      numArray2[15] = (byte) 124;
      numArray2[29] = (byte) 190;
      numArray2[26] = (byte) 61;
      byte[] numArray3 = new byte[31 /*0x1F*/]
      {
        (byte) 123,
        (byte) 226,
        (byte) 229,
        (byte) 184,
        (byte) 166,
        (byte) 6,
        (byte) 168,
        (byte) 221,
        (byte) 252,
        (byte) 154,
        (byte) 54,
        (byte) 139,
        (byte) 64 /*0x40*/,
        (byte) 240 /*0xF0*/,
        (byte) 188,
        (byte) 88,
        (byte) 189,
        (byte) 186,
        (byte) 14,
        (byte) 169,
        (byte) 116,
        (byte) 189,
        (byte) 214,
        (byte) 232,
        (byte) 228,
        (byte) 44,
        (byte) 6,
        (byte) 35,
        (byte) 20,
        (byte) 59,
        (byte) 214
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 83,
      (byte) 246,
      (byte) 94,
      (byte) 30,
      (byte) 38,
      (byte) 107,
      (byte) 42,
      (byte) 35,
      (byte) 13,
      (byte) 252,
      (byte) 184,
      (byte) 202,
      (byte) 96 /*0x60*/,
      (byte) 30,
      (byte) 53,
      (byte) 39,
      (byte) 148,
      (byte) 205,
      (byte) 35,
      (byte) 171,
      (byte) 54,
      (byte) 254,
      (byte) 133,
      (byte) 141,
      (byte) 5,
      (byte) 171,
      (byte) 40,
      (byte) 196,
      (byte) 167,
      (byte) 48 /*0x30*/,
      (byte) 78
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 237,
      (byte) 217,
      (byte) 44,
      (byte) 33,
      (byte) 241,
      (byte) 209,
      byte.MaxValue,
      (byte) 175,
      (byte) 49,
      (byte) 168,
      (byte) 25,
      (byte) 214,
      (byte) 64 /*0x40*/,
      (byte) 179,
      (byte) 43,
      (byte) 160 /*0xA0*/,
      (byte) 16 /*0x10*/,
      (byte) 147,
      (byte) 199,
      (byte) 79,
      (byte) 200,
      (byte) 145,
      (byte) 160 /*0xA0*/,
      (byte) 12,
      (byte) 20,
      (byte) 93,
      (byte) 214,
      (byte) 52,
      (byte) 180,
      (byte) 195,
      (byte) 16 /*0x10*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13286(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[47] = (byte) 4;
    sourceArray1[1] = (byte) 139;
    sourceArray1[13] = (byte) 162;
    sourceArray1[18] = (byte) 211;
    sourceArray1[6] = (byte) 127 /*0x7F*/;
    sourceArray1[33] = (byte) 47;
    sourceArray1[15] = (byte) 237;
    sourceArray1[7] = (byte) 160 /*0xA0*/;
    sourceArray1[8] = (byte) 79;
    sourceArray1[11] = (byte) 203;
    sourceArray1[23] = (byte) 122;
    sourceArray1[32 /*0x20*/] = (byte) 62;
    sourceArray1[28] = (byte) 174;
    sourceArray1[9] = (byte) 238;
    sourceArray1[20] = (byte) 112 /*0x70*/;
    sourceArray1[24] = (byte) 67;
    sourceArray1[44] = (byte) 244;
    sourceArray1[45] = (byte) 93;
    sourceArray1[4] = (byte) 232;
    sourceArray1[19] = (byte) 84;
    sourceArray1[5] = (byte) 107;
    sourceArray1[21] = (byte) 208 /*0xD0*/;
    sourceArray1[22] = (byte) 24;
    sourceArray1[3] = (byte) 208 /*0xD0*/;
    sourceArray1[39] = (byte) 202;
    sourceArray1[36] = (byte) 90;
    sourceArray1[12] = (byte) 59;
    sourceArray1[0] = (byte) 237;
    sourceArray1[26] = (byte) 124;
    sourceArray1[29] = (byte) 21;
    sourceArray1[30] = (byte) 79;
    sourceArray1[31 /*0x1F*/] = (byte) 161;
    sourceArray1[27] = (byte) 220;
    sourceArray1[2] = (byte) 115;
    sourceArray1[34] = (byte) 57;
    sourceArray1[35] = (byte) 9;
    sourceArray1[17] = (byte) 7;
    sourceArray1[37] = (byte) 83;
    sourceArray1[38] = (byte) 194;
    sourceArray1[46] = (byte) 136;
    sourceArray1[14] = (byte) 205;
    sourceArray1[40] = (byte) 127 /*0x7F*/;
    sourceArray1[42] = (byte) 62;
    sourceArray1[43] = (byte) 22;
    sourceArray1[16 /*0x10*/] = (byte) 130;
    sourceArray1[41] = (byte) 229;
    sourceArray1[10] = (byte) 182;
    sourceArray1[25] = (byte) 102;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 120,
      (byte) 186,
      (byte) 157,
      (byte) 188,
      (byte) 174,
      (byte) 174,
      (byte) 104,
      (byte) 246,
      (byte) 61,
      (byte) 121,
      (byte) 68,
      (byte) 228,
      (byte) 244,
      (byte) 155,
      (byte) 240 /*0xF0*/,
      (byte) 14,
      (byte) 188,
      (byte) 11,
      (byte) 163,
      (byte) 251,
      (byte) 199,
      (byte) 55,
      (byte) 55,
      (byte) 176 /*0xB0*/,
      (byte) 139,
      (byte) 36,
      (byte) 125,
      (byte) 226,
      (byte) 56,
      (byte) 222,
      (byte) 1,
      (byte) 141,
      (byte) 192 /*0xC0*/,
      (byte) 128 /*0x80*/,
      (byte) 166,
      (byte) 102,
      (byte) 231,
      (byte) 53,
      (byte) 33,
      (byte) 245,
      (byte) 99,
      (byte) 61,
      (byte) 21,
      (byte) 14,
      (byte) 137,
      (byte) 23,
      (byte) 170,
      (byte) 155
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
