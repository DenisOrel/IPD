// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13009
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_13009
{
  private static byte[] sspq = new byte[59]
  {
    (byte) 180,
    (byte) 82,
    (byte) 23,
    (byte) 131,
    (byte) 4,
    (byte) 86,
    (byte) 85,
    (byte) 92,
    (byte) 175,
    (byte) 191,
    (byte) 97,
    (byte) 181,
    (byte) 177,
    (byte) 170,
    (byte) 7,
    (byte) 186,
    (byte) 11,
    (byte) 91,
    (byte) 119,
    (byte) 48 /*0x30*/,
    (byte) 102,
    (byte) 240 /*0xF0*/,
    (byte) 23,
    (byte) 107,
    (byte) 38,
    (byte) 159,
    (byte) 138,
    (byte) 231,
    (byte) 112 /*0x70*/,
    (byte) 232,
    (byte) 250,
    (byte) 153,
    (byte) 119,
    (byte) 45,
    (byte) 251,
    (byte) 175,
    (byte) 214,
    (byte) 227,
    (byte) 211,
    (byte) 45,
    (byte) 65,
    (byte) 208 /*0xD0*/,
    (byte) 218,
    (byte) 152,
    (byte) 55,
    (byte) 113,
    (byte) 28,
    (byte) 153,
    (byte) 200,
    (byte) 242,
    (byte) 230,
    (byte) 65,
    (byte) 156,
    (byte) 172,
    (byte) 8,
    (byte) 203,
    (byte) 193,
    (byte) 90,
    (byte) 242
  };
  private static byte[] sspr = new byte[59]
  {
    (byte) 102,
    (byte) 248,
    (byte) 119,
    (byte) 99,
    (byte) 138,
    (byte) 49,
    (byte) 243,
    (byte) 144 /*0x90*/,
    (byte) 25,
    (byte) 201,
    (byte) 153,
    (byte) 170,
    (byte) 174,
    (byte) 218,
    (byte) 179,
    (byte) 223,
    (byte) 131,
    (byte) 170,
    (byte) 162,
    (byte) 62,
    (byte) 29,
    (byte) 223,
    (byte) 141,
    (byte) 184,
    (byte) 130,
    (byte) 40,
    (byte) 163,
    (byte) 199,
    (byte) 219,
    (byte) 203,
    (byte) 87,
    (byte) 22,
    (byte) 165,
    (byte) 107,
    (byte) 17,
    (byte) 52,
    (byte) 46,
    (byte) 183,
    (byte) 136,
    (byte) 126,
    (byte) 114,
    (byte) 190,
    (byte) 40,
    (byte) 191,
    (byte) 183,
    (byte) 78,
    (byte) 48 /*0x30*/,
    (byte) 234,
    (byte) 136,
    (byte) 76,
    (byte) 241,
    (byte) 111,
    (byte) 157,
    (byte) 233,
    (byte) 236,
    (byte) 181,
    (byte) 222,
    (byte) 74,
    (byte) 114
  };

  internal static int ssp_appserver_13010(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 182,
      (byte) 88,
      (byte) 141,
      (byte) 189,
      (byte) 86,
      (byte) 156,
      (byte) 208 /*0xD0*/,
      (byte) 106,
      (byte) 244,
      (byte) 188,
      (byte) 89,
      (byte) 17,
      (byte) 84,
      (byte) 83,
      (byte) 208 /*0xD0*/,
      (byte) 185,
      (byte) 230,
      (byte) 157,
      (byte) 93,
      (byte) 23,
      (byte) 236,
      (byte) 83,
      (byte) 131,
      (byte) 60,
      (byte) 9,
      (byte) 236,
      (byte) 205,
      (byte) 245,
      (byte) 212,
      (byte) 166,
      (byte) 89,
      (byte) 53,
      (byte) 192 /*0xC0*/,
      (byte) 95,
      (byte) 235,
      (byte) 46,
      (byte) 33,
      (byte) 87,
      (byte) 39,
      (byte) 90,
      (byte) 195,
      (byte) 41,
      (byte) 159,
      (byte) 66,
      (byte) 69,
      (byte) 171,
      (byte) 45,
      (byte) 30
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 94;
    sourceArray2[1] = (byte) 234;
    sourceArray2[2] = (byte) 78;
    sourceArray2[8] = (byte) 59;
    sourceArray2[45] = (byte) 18;
    sourceArray2[5] = (byte) 109;
    sourceArray2[37] = (byte) 119;
    sourceArray2[7] = (byte) 225;
    sourceArray2[47] = (byte) 204;
    sourceArray2[32 /*0x20*/] = (byte) 43;
    sourceArray2[10] = (byte) 65;
    sourceArray2[36] = (byte) 28;
    sourceArray2[12] = (byte) 254;
    sourceArray2[0] = (byte) 197;
    sourceArray2[17] = (byte) 186;
    sourceArray2[3] = (byte) 67;
    sourceArray2[4] = (byte) 141;
    sourceArray2[41] = (byte) 208 /*0xD0*/;
    sourceArray2[39] = (byte) 15;
    sourceArray2[35] = (byte) 131;
    sourceArray2[20] = (byte) 159;
    sourceArray2[42] = (byte) 48 /*0x30*/;
    sourceArray2[21] = (byte) 203;
    sourceArray2[22] = (byte) 241;
    sourceArray2[11] = (byte) 174;
    sourceArray2[25] = (byte) 7;
    sourceArray2[16 /*0x10*/] = (byte) 48 /*0x30*/;
    sourceArray2[27] = (byte) 224 /*0xE0*/;
    sourceArray2[28] = (byte) 134;
    sourceArray2[33] = (byte) 132;
    sourceArray2[29] = (byte) 133;
    sourceArray2[31 /*0x1F*/] = (byte) 26;
    sourceArray2[30] = (byte) 200;
    sourceArray2[23] = (byte) 170;
    sourceArray2[24] = (byte) 171;
    sourceArray2[9] = (byte) 169;
    sourceArray2[15] = (byte) 105;
    sourceArray2[18] = (byte) 229;
    sourceArray2[38] = (byte) 145;
    sourceArray2[6] = (byte) 113;
    sourceArray2[40] = (byte) 186;
    sourceArray2[26] = (byte) 95;
    sourceArray2[34] = (byte) 25;
    sourceArray2[43] = (byte) 253;
    sourceArray2[44] = (byte) 201;
    sourceArray2[13] = (byte) 119;
    sourceArray2[46] = (byte) 45;
    sourceArray2[14] = (byte) 66;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_13011()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[110];
      byte[] numArray2 = new byte[55]
      {
        (byte) 90,
        (byte) 55,
        (byte) 182,
        (byte) 125,
        (byte) 73,
        (byte) 235,
        (byte) 147,
        (byte) 31 /*0x1F*/,
        (byte) 8,
        (byte) 108,
        (byte) 174,
        (byte) 148,
        (byte) 90,
        (byte) 85,
        (byte) 169,
        (byte) 31 /*0x1F*/,
        (byte) 5,
        (byte) 163,
        (byte) 33,
        (byte) 19,
        (byte) 15,
        (byte) 236,
        (byte) 197,
        (byte) 223,
        (byte) 72,
        (byte) 199,
        (byte) 234,
        (byte) 184,
        (byte) 119,
        (byte) 107,
        (byte) 47,
        (byte) 98,
        (byte) 96 /*0x60*/,
        (byte) 211,
        (byte) 3,
        (byte) 135,
        (byte) 249,
        (byte) 88,
        (byte) 168,
        (byte) 64 /*0x40*/,
        (byte) 187,
        (byte) 243,
        (byte) 92,
        (byte) 194,
        (byte) 217,
        (byte) 197,
        (byte) 117,
        (byte) 78,
        (byte) 19,
        (byte) 108,
        (byte) 226,
        (byte) 239,
        (byte) 48 /*0x30*/,
        (byte) 204,
        (byte) 18
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 118,
        (byte) 215,
        (byte) 98,
        (byte) 176 /*0xB0*/,
        (byte) 167,
        (byte) 135,
        (byte) 83,
        (byte) 17,
        (byte) 195,
        (byte) 35,
        (byte) 207,
        (byte) 227,
        (byte) 162,
        (byte) 78,
        (byte) 129,
        (byte) 2,
        (byte) 248,
        (byte) 58,
        (byte) 10,
        (byte) 235,
        (byte) 188,
        (byte) 102,
        (byte) 14,
        (byte) 142,
        (byte) 37,
        (byte) 203,
        (byte) 50,
        (byte) 65,
        (byte) 222,
        (byte) 109,
        (byte) 181,
        (byte) 68,
        (byte) 9,
        (byte) 96 /*0x60*/,
        (byte) 158,
        (byte) 241,
        (byte) 105,
        (byte) 236,
        (byte) 72,
        (byte) 147,
        (byte) 171,
        (byte) 190,
        (byte) 52,
        (byte) 173,
        (byte) 69,
        (byte) 98,
        (byte) 164,
        (byte) 122,
        (byte) 185,
        (byte) 163,
        (byte) 161,
        (byte) 190,
        (byte) 90,
        (byte) 22,
        (byte) 48 /*0x30*/
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[8] = (byte) 76;
      numArray4[1] = (byte) 52;
      numArray4[50] = (byte) 79;
      numArray4[3] = (byte) 1;
      numArray4[34] = (byte) 33;
      numArray4[5] = (byte) 167;
      numArray4[53] = (byte) 222;
      numArray4[4] = (byte) 219;
      numArray4[36] = (byte) 38;
      numArray4[9] = (byte) 51;
      numArray4[54] = (byte) 110;
      numArray4[7] = (byte) 61;
      numArray4[10] = (byte) 134;
      numArray4[30] = (byte) 148;
      numArray4[29] = (byte) 49;
      numArray4[15] = (byte) 251;
      numArray4[16 /*0x10*/] = (byte) 224 /*0xE0*/;
      numArray4[32 /*0x20*/] = (byte) 149;
      numArray4[49] = (byte) 243;
      numArray4[18] = (byte) 28;
      numArray4[20] = (byte) 3;
      numArray4[21] = (byte) 5;
      numArray4[22] = (byte) 131;
      numArray4[11] = (byte) 82;
      numArray4[24] = (byte) 155;
      numArray4[51] = (byte) 115;
      numArray4[26] = (byte) 11;
      numArray4[17] = (byte) 88;
      numArray4[25] = (byte) 164;
      numArray4[6] = (byte) 156;
      numArray4[14] = (byte) 172;
      numArray4[2] = (byte) 130;
      numArray4[23] = (byte) 81;
      numArray4[33] = (byte) 21;
      numArray4[13] = (byte) 38;
      numArray4[27] = (byte) 137;
      numArray4[37] = (byte) 63 /*0x3F*/;
      numArray4[41] = (byte) 132;
      numArray4[38] = (byte) 223;
      numArray4[39] = (byte) 76;
      numArray4[40] = (byte) 191;
      numArray4[52] = (byte) 246;
      numArray4[42] = (byte) 141;
      numArray4[43] = (byte) 146;
      numArray4[44] = (byte) 238;
      numArray4[45] = (byte) 252;
      numArray4[46] = (byte) 241;
      numArray4[47] = (byte) 70;
      numArray4[48 /*0x30*/] = (byte) 53;
      numArray4[19] = (byte) 144 /*0x90*/;
      numArray4[28] = (byte) 64 /*0x40*/;
      numArray4[35] = (byte) 36;
      numArray4[12] = (byte) 34;
      numArray4[31 /*0x1F*/] = (byte) 193;
      numArray4[0] = (byte) 215;
      byte[] numArray5 = new byte[55];
      numArray5[21] = (byte) 51;
      numArray5[14] = (byte) 50;
      numArray5[36] = (byte) 131;
      numArray5[6] = (byte) 174;
      numArray5[35] = (byte) 248;
      numArray5[48 /*0x30*/] = (byte) 223;
      numArray5[25] = (byte) 230;
      numArray5[7] = (byte) 248;
      numArray5[1] = (byte) 164;
      numArray5[10] = (byte) 175;
      numArray5[40] = (byte) 156;
      numArray5[11] = (byte) 63 /*0x3F*/;
      numArray5[24] = (byte) 77;
      numArray5[39] = (byte) 96 /*0x60*/;
      numArray5[47] = (byte) 81;
      numArray5[45] = (byte) 47;
      numArray5[16 /*0x10*/] = (byte) 147;
      numArray5[17] = (byte) 0;
      numArray5[19] = (byte) 154;
      numArray5[3] = (byte) 38;
      numArray5[20] = (byte) 165;
      numArray5[4] = (byte) 37;
      numArray5[13] = (byte) 141;
      numArray5[5] = (byte) 246;
      numArray5[0] = (byte) 123;
      numArray5[12] = (byte) 124;
      numArray5[8] = (byte) 189;
      numArray5[27] = (byte) 9;
      numArray5[28] = (byte) 246;
      numArray5[9] = (byte) 247;
      numArray5[2] = (byte) 121;
      numArray5[31 /*0x1F*/] = (byte) 136;
      numArray5[32 /*0x20*/] = (byte) 21;
      numArray5[33] = (byte) 154;
      numArray5[34] = (byte) 190;
      numArray5[44] = (byte) 200;
      numArray5[46] = (byte) 132;
      numArray5[37] = (byte) 85;
      numArray5[38] = (byte) 5;
      numArray5[15] = (byte) 63 /*0x3F*/;
      numArray5[29] = (byte) 93;
      numArray5[41] = (byte) 178;
      numArray5[22] = (byte) 154;
      numArray5[18] = (byte) 192 /*0xC0*/;
      numArray5[42] = (byte) 234;
      numArray5[50] = (byte) 99;
      numArray5[30] = (byte) 116;
      numArray5[23] = (byte) 223;
      numArray5[26] = (byte) 147;
      numArray5[49] = (byte) 32 /*0x20*/;
      numArray5[43] = (byte) 141;
      numArray5[51] = (byte) 174;
      numArray5[52] = (byte) 53;
      numArray5[53] = (byte) 23;
      numArray5[54] = (byte) 108;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[14];
      byte[] response = new byte[14];
      Array.Copy((Array) sc_13009.sspq, 0, (Array) numArray6, 0, 14);
      key.Query(true, 335, numArray6, response);
      Array.Copy((Array) sc_13009.sspr, 0, (Array) numArray6, 0, 14);
      for (int index = 0; index < numArray6.Length; ++index)
      {
        if ((int) numArray6[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray7 = new byte[110];
    byte[] numArray8 = new byte[55]
    {
      (byte) 70,
      (byte) 55,
      (byte) 32 /*0x20*/,
      (byte) 202,
      (byte) 135,
      (byte) 11,
      (byte) 39,
      (byte) 80 /*0x50*/,
      (byte) 246,
      (byte) 15,
      (byte) 236,
      (byte) 76,
      (byte) 252,
      (byte) 158,
      (byte) 192 /*0xC0*/,
      (byte) 189,
      (byte) 160 /*0xA0*/,
      (byte) 241,
      (byte) 150,
      (byte) 175,
      (byte) 113,
      (byte) 135,
      (byte) 213,
      (byte) 82,
      (byte) 54,
      (byte) 31 /*0x1F*/,
      (byte) 216,
      (byte) 220,
      (byte) 206,
      (byte) 205,
      (byte) 54,
      (byte) 139,
      (byte) 54,
      (byte) 128 /*0x80*/,
      (byte) 191,
      (byte) 177,
      (byte) 217,
      (byte) 138,
      (byte) 59,
      (byte) 135,
      (byte) 61,
      (byte) 99,
      (byte) 91,
      (byte) 8,
      (byte) 151,
      (byte) 184,
      (byte) 89,
      (byte) 183,
      (byte) 156,
      (byte) 242,
      (byte) 30,
      (byte) 111,
      (byte) 31 /*0x1F*/,
      (byte) 1,
      (byte) 56
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 176 /*0xB0*/,
      (byte) 150,
      (byte) 182,
      (byte) 126,
      (byte) 186,
      (byte) 195,
      (byte) 130,
      (byte) 41,
      (byte) 70,
      (byte) 139,
      (byte) 40,
      (byte) 152,
      (byte) 74,
      (byte) 94,
      (byte) 246,
      (byte) 90,
      (byte) 104,
      (byte) 17,
      (byte) 195,
      (byte) 93,
      (byte) 11,
      (byte) 250,
      (byte) 77,
      (byte) 161,
      (byte) 199,
      (byte) 233,
      (byte) 11,
      (byte) 77,
      (byte) 216,
      (byte) 148,
      (byte) 52,
      (byte) 142,
      (byte) 72,
      (byte) 128 /*0x80*/,
      (byte) 192 /*0xC0*/,
      (byte) 146,
      (byte) 96 /*0x60*/,
      (byte) 209,
      (byte) 229,
      (byte) 251,
      (byte) 173,
      (byte) 71,
      (byte) 118,
      (byte) 239,
      (byte) 219,
      (byte) 191,
      (byte) 244,
      (byte) 134,
      (byte) 167,
      (byte) 44,
      (byte) 90,
      (byte) 96 /*0x60*/,
      (byte) 243,
      (byte) 251,
      (byte) 156
    };
    key.Query(true, 335, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[55]
    {
      (byte) 205,
      (byte) 148,
      (byte) 103,
      (byte) 51,
      (byte) 74,
      (byte) 129,
      (byte) 41,
      (byte) 216,
      (byte) 97,
      (byte) 21,
      (byte) 124,
      (byte) 223,
      (byte) 14,
      (byte) 227,
      (byte) 22,
      (byte) 182,
      (byte) 15,
      (byte) 114,
      (byte) 13,
      (byte) 43,
      (byte) 204,
      (byte) 187,
      (byte) 126,
      (byte) 198,
      (byte) 33,
      (byte) 63 /*0x3F*/,
      (byte) 219,
      (byte) 175,
      (byte) 199,
      (byte) 107,
      (byte) 52,
      (byte) 71,
      (byte) 0,
      (byte) 79,
      (byte) 11,
      (byte) 246,
      (byte) 211,
      (byte) 83,
      (byte) 242,
      (byte) 226,
      (byte) 50,
      (byte) 48 /*0x30*/,
      (byte) 53,
      (byte) 162,
      (byte) 221,
      (byte) 215,
      (byte) 142,
      (byte) 14,
      (byte) 7,
      (byte) 185,
      (byte) 15,
      (byte) 143,
      (byte) 43,
      (byte) 148,
      (byte) 149
    };
    byte[] numArray11 = new byte[55];
    numArray11[23] = (byte) 212;
    numArray11[40] = (byte) 185;
    numArray11[50] = (byte) 56;
    numArray11[10] = (byte) 244;
    numArray11[4] = (byte) 119;
    numArray11[18] = (byte) 204;
    numArray11[34] = (byte) 215;
    numArray11[25] = (byte) 82;
    numArray11[49] = (byte) 5;
    numArray11[2] = (byte) 152;
    numArray11[6] = (byte) 141;
    numArray11[1] = (byte) 141;
    numArray11[12] = (byte) 189;
    numArray11[13] = (byte) 218;
    numArray11[14] = (byte) 22;
    numArray11[3] = (byte) 214;
    numArray11[19] = (byte) 35;
    numArray11[11] = (byte) 203;
    numArray11[17] = (byte) 90;
    numArray11[8] = (byte) 193;
    numArray11[20] = (byte) 51;
    numArray11[21] = (byte) 59;
    numArray11[16 /*0x10*/] = (byte) 242;
    numArray11[5] = (byte) 247;
    numArray11[15] = (byte) 207;
    numArray11[37] = (byte) 198;
    numArray11[26] = (byte) 5;
    numArray11[27] = (byte) 11;
    numArray11[42] = (byte) 23;
    numArray11[29] = (byte) 103;
    numArray11[35] = (byte) 44;
    numArray11[47] = (byte) 243;
    numArray11[32 /*0x20*/] = (byte) 48 /*0x30*/;
    numArray11[33] = (byte) 2;
    numArray11[24] = (byte) 136;
    numArray11[45] = (byte) 182;
    numArray11[53] = (byte) 45;
    numArray11[43] = (byte) 209;
    numArray11[38] = (byte) 11;
    numArray11[39] = (byte) 63 /*0x3F*/;
    numArray11[31 /*0x1F*/] = (byte) 162;
    numArray11[41] = (byte) 9;
    numArray11[9] = (byte) 238;
    numArray11[0] = (byte) 210;
    numArray11[22] = (byte) 88;
    numArray11[36] = (byte) 55;
    numArray11[30] = (byte) 235;
    numArray11[7] = (byte) 224 /*0xE0*/;
    numArray11[48 /*0x30*/] = (byte) 55;
    numArray11[46] = (byte) 126;
    numArray11[28] = (byte) 140;
    numArray11[51] = (byte) 210;
    numArray11[52] = (byte) 156;
    numArray11[44] = (byte) 196;
    numArray11[54] = (byte) 4;
    key.Query(true, 335, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }

  internal static string ssp_appserver_13012()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 89,
        (byte) 216,
        (byte) 209,
        (byte) 2,
        (byte) 153,
        (byte) 59,
        (byte) 25,
        (byte) 114,
        (byte) 4,
        (byte) 222
      };
      byte[] numArray3 = new byte[10];
      numArray3[3] = (byte) 245;
      numArray3[1] = (byte) 115;
      numArray3[0] = (byte) 217;
      numArray3[2] = (byte) 49;
      numArray3[4] = (byte) 97;
      numArray3[7] = (byte) 141;
      numArray3[5] = (byte) 107;
      numArray3[6] = (byte) 42;
      numArray3[8] = (byte) 75;
      numArray3[9] = (byte) 254;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 191,
      (byte) 55,
      (byte) 7,
      (byte) 149,
      (byte) 27,
      (byte) 208 /*0xD0*/,
      (byte) 192 /*0xC0*/,
      (byte) 109,
      (byte) 166,
      (byte) 123
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 26,
      (byte) 242,
      (byte) 150,
      (byte) 114,
      (byte) 193,
      (byte) 4,
      (byte) 253,
      (byte) 2,
      (byte) 145,
      (byte) 96 /*0x60*/
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_13013(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 133;
    sourceArray1[5] = (byte) 88;
    sourceArray1[7] = (byte) 38;
    sourceArray1[3] = (byte) 134;
    sourceArray1[4] = (byte) 203;
    sourceArray1[30] = (byte) 9;
    sourceArray1[25] = (byte) 67;
    sourceArray1[14] = (byte) 32 /*0x20*/;
    sourceArray1[8] = (byte) 218;
    sourceArray1[9] = (byte) 120;
    sourceArray1[10] = (byte) 17;
    sourceArray1[33] = (byte) 95;
    sourceArray1[31 /*0x1F*/] = (byte) 138;
    sourceArray1[24] = (byte) 161;
    sourceArray1[26] = (byte) 113;
    sourceArray1[43] = (byte) 28;
    sourceArray1[41] = (byte) 16 /*0x10*/;
    sourceArray1[17] = (byte) 119;
    sourceArray1[18] = (byte) 66;
    sourceArray1[38] = (byte) 23;
    sourceArray1[28] = (byte) 52;
    sourceArray1[39] = (byte) 62;
    sourceArray1[19] = (byte) 17;
    sourceArray1[23] = (byte) 253;
    sourceArray1[6] = (byte) 168;
    sourceArray1[22] = (byte) 166;
    sourceArray1[15] = (byte) 118;
    sourceArray1[27] = (byte) 153;
    sourceArray1[32 /*0x20*/] = (byte) 88;
    sourceArray1[29] = (byte) 0;
    sourceArray1[20] = (byte) 203;
    sourceArray1[11] = (byte) 91;
    sourceArray1[37] = (byte) 202;
    sourceArray1[2] = (byte) 142;
    sourceArray1[34] = (byte) 160 /*0xA0*/;
    sourceArray1[35] = (byte) 148;
    sourceArray1[36] = (byte) 178;
    sourceArray1[12] = (byte) 124;
    sourceArray1[13] = (byte) 69;
    sourceArray1[16 /*0x10*/] = (byte) 120;
    sourceArray1[40] = (byte) 23;
    sourceArray1[42] = (byte) 44;
    sourceArray1[0] = (byte) 165;
    sourceArray1[21] = (byte) 146;
    sourceArray1[44] = (byte) 10;
    sourceArray1[45] = (byte) 117;
    sourceArray1[46] = (byte) 68;
    sourceArray1[47] = (byte) 39;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 175;
    sourceArray2[32 /*0x20*/] = (byte) 119;
    sourceArray2[0] = (byte) 229;
    sourceArray2[17] = (byte) 226;
    sourceArray2[14] = (byte) 243;
    sourceArray2[12] = (byte) 27;
    sourceArray2[6] = (byte) 129;
    sourceArray2[7] = (byte) 89;
    sourceArray2[8] = (byte) 172;
    sourceArray2[9] = (byte) 220;
    sourceArray2[10] = (byte) 193;
    sourceArray2[11] = (byte) 8;
    sourceArray2[13] = (byte) 123;
    sourceArray2[15] = (byte) 0;
    sourceArray2[2] = (byte) 188;
    sourceArray2[31 /*0x1F*/] = (byte) 185;
    sourceArray2[37] = (byte) 150;
    sourceArray2[41] = (byte) 177;
    sourceArray2[18] = (byte) 163;
    sourceArray2[19] = (byte) 129;
    sourceArray2[20] = (byte) 211;
    sourceArray2[34] = (byte) 110;
    sourceArray2[25] = (byte) 119;
    sourceArray2[23] = (byte) 24;
    sourceArray2[24] = (byte) 252;
    sourceArray2[26] = (byte) 36;
    sourceArray2[35] = (byte) 220;
    sourceArray2[3] = (byte) 66;
    sourceArray2[28] = (byte) 9;
    sourceArray2[4] = (byte) 143;
    sourceArray2[30] = (byte) 227;
    sourceArray2[43] = (byte) 160 /*0xA0*/;
    sourceArray2[46] = (byte) 108;
    sourceArray2[33] = (byte) 207;
    sourceArray2[47] = (byte) 29;
    sourceArray2[29] = (byte) 213;
    sourceArray2[36] = (byte) 129;
    sourceArray2[16 /*0x10*/] = (byte) 57;
    sourceArray2[38] = (byte) 116;
    sourceArray2[39] = (byte) 95;
    sourceArray2[45] = (byte) 249;
    sourceArray2[42] = (byte) 28;
    sourceArray2[21] = (byte) 86;
    sourceArray2[1] = (byte) 253;
    sourceArray2[44] = (byte) 246;
    sourceArray2[22] = (byte) 5;
    sourceArray2[5] = (byte) 98;
    sourceArray2[40] = (byte) 194;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[45];
    byte[] response2 = new byte[45];
    Array.Copy((Array) sc_13009.sspq, 14, (Array) numArray2, 0, 45);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13009.sspr, 14, (Array) numArray2, 0, 45);
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

  internal static string ssp_appserver_13014()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[62];
      byte[] numArray2 = new byte[55]
      {
        (byte) 34,
        (byte) 83,
        (byte) 220,
        (byte) 190,
        (byte) 124,
        (byte) 70,
        (byte) 235,
        (byte) 87,
        (byte) 50,
        (byte) 87,
        (byte) 227,
        (byte) 40,
        (byte) 135,
        (byte) 73,
        (byte) 238,
        (byte) 150,
        (byte) 21,
        (byte) 126,
        (byte) 52,
        (byte) 180,
        (byte) 130,
        (byte) 241,
        (byte) 30,
        (byte) 125,
        (byte) 1,
        (byte) 185,
        (byte) 252,
        (byte) 202,
        (byte) 36,
        (byte) 48 /*0x30*/,
        (byte) 238,
        (byte) 123,
        (byte) 241,
        (byte) 132,
        (byte) 11,
        (byte) 238,
        (byte) 243,
        (byte) 27,
        (byte) 28,
        (byte) 240 /*0xF0*/,
        (byte) 134,
        (byte) 197,
        (byte) 102,
        (byte) 113,
        (byte) 15,
        (byte) 190,
        (byte) 185,
        (byte) 83,
        (byte) 189,
        (byte) 114,
        (byte) 181,
        (byte) 66,
        (byte) 38,
        (byte) 62,
        (byte) 204
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 241,
        (byte) 188,
        (byte) 197,
        (byte) 24,
        (byte) 179,
        (byte) 101,
        (byte) 137,
        (byte) 213,
        (byte) 205,
        (byte) 122,
        (byte) 29,
        (byte) 123,
        (byte) 231,
        (byte) 132,
        (byte) 57,
        (byte) 31 /*0x1F*/,
        (byte) 245,
        (byte) 193,
        (byte) 234,
        (byte) 100,
        (byte) 196,
        (byte) 193,
        (byte) 112 /*0x70*/,
        (byte) 209,
        (byte) 112 /*0x70*/,
        (byte) 2,
        (byte) 94,
        (byte) 202,
        (byte) 109,
        (byte) 231,
        (byte) 139,
        (byte) 252,
        (byte) 174,
        (byte) 114,
        (byte) 58,
        (byte) 113,
        (byte) 219,
        (byte) 219,
        (byte) 207,
        (byte) 245,
        (byte) 154,
        (byte) 73,
        (byte) 32 /*0x20*/,
        (byte) 251,
        (byte) 103,
        (byte) 131,
        (byte) 149,
        (byte) 42,
        (byte) 31 /*0x1F*/,
        (byte) 223,
        (byte) 254,
        (byte) 167,
        (byte) 53,
        (byte) 113,
        (byte) 46
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[7];
      numArray4[5] = (byte) 50;
      numArray4[1] = (byte) 188;
      numArray4[6] = (byte) 2;
      numArray4[3] = (byte) 73;
      numArray4[0] = (byte) 95;
      numArray4[2] = (byte) 195;
      numArray4[4] = (byte) 32 /*0x20*/;
      byte[] numArray5 = new byte[7]
      {
        (byte) 101,
        (byte) 132,
        (byte) 0,
        (byte) 85,
        (byte) 177,
        (byte) 243,
        (byte) 172
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[62];
    byte[] numArray7 = new byte[55]
    {
      (byte) 45,
      (byte) 189,
      (byte) 139,
      (byte) 173,
      (byte) 109,
      (byte) 66,
      byte.MaxValue,
      (byte) 169,
      (byte) 13,
      (byte) 178,
      (byte) 122,
      (byte) 172,
      (byte) 85,
      (byte) 179,
      (byte) 184,
      (byte) 218,
      (byte) 185,
      (byte) 148,
      (byte) 95,
      (byte) 89,
      (byte) 167,
      (byte) 102,
      (byte) 146,
      (byte) 99,
      (byte) 8,
      (byte) 167,
      (byte) 135,
      (byte) 182,
      (byte) 21,
      (byte) 189,
      (byte) 207,
      (byte) 187,
      (byte) 224 /*0xE0*/,
      (byte) 227,
      (byte) 16 /*0x10*/,
      (byte) 14,
      (byte) 166,
      (byte) 135,
      (byte) 241,
      (byte) 121,
      (byte) 195,
      (byte) 160 /*0xA0*/,
      (byte) 19,
      (byte) 128 /*0x80*/,
      (byte) 135,
      (byte) 188,
      (byte) 18,
      (byte) 131,
      (byte) 74,
      (byte) 99,
      (byte) 71,
      (byte) 14,
      (byte) 154,
      (byte) 8,
      (byte) 156
    };
    byte[] numArray8 = new byte[55];
    numArray8[40] = (byte) 178;
    numArray8[52] = (byte) 187;
    numArray8[38] = (byte) 117;
    numArray8[3] = (byte) 107;
    numArray8[4] = (byte) 208 /*0xD0*/;
    numArray8[36] = (byte) 128 /*0x80*/;
    numArray8[44] = (byte) 163;
    numArray8[7] = (byte) 168;
    numArray8[6] = (byte) 19;
    numArray8[33] = (byte) 229;
    numArray8[10] = (byte) 108;
    numArray8[5] = (byte) 116;
    numArray8[12] = (byte) 105;
    numArray8[13] = (byte) 27;
    numArray8[14] = (byte) 125;
    numArray8[24] = (byte) 4;
    numArray8[43] = (byte) 149;
    numArray8[11] = (byte) 241;
    numArray8[18] = (byte) 244;
    numArray8[19] = (byte) 42;
    numArray8[8] = (byte) 203;
    numArray8[21] = (byte) 21;
    numArray8[22] = (byte) 94;
    numArray8[23] = (byte) 224 /*0xE0*/;
    numArray8[1] = (byte) 81;
    numArray8[25] = (byte) 225;
    numArray8[30] = (byte) 192 /*0xC0*/;
    numArray8[20] = (byte) 235;
    numArray8[28] = (byte) 51;
    numArray8[29] = (byte) 80 /*0x50*/;
    numArray8[49] = (byte) 8;
    numArray8[31 /*0x1F*/] = (byte) 72;
    numArray8[32 /*0x20*/] = (byte) 253;
    numArray8[27] = (byte) 174;
    numArray8[34] = (byte) 189;
    numArray8[35] = (byte) 65;
    numArray8[16 /*0x10*/] = (byte) 197;
    numArray8[42] = (byte) 88;
    numArray8[47] = (byte) 172;
    numArray8[46] = (byte) 11;
    numArray8[9] = (byte) 49;
    numArray8[17] = (byte) 45;
    numArray8[26] = (byte) 172;
    numArray8[39] = (byte) 67;
    numArray8[2] = (byte) 213;
    numArray8[45] = (byte) 217;
    numArray8[41] = (byte) 144 /*0x90*/;
    numArray8[37] = (byte) 189;
    numArray8[48 /*0x30*/] = (byte) 14;
    numArray8[0] = (byte) 189;
    numArray8[50] = (byte) 115;
    numArray8[51] = (byte) 36;
    numArray8[15] = (byte) 242;
    numArray8[53] = (byte) 106;
    numArray8[54] = (byte) 141;
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[7]
    {
      (byte) 75,
      (byte) 89,
      (byte) 48 /*0x30*/,
      (byte) 182,
      (byte) 220,
      (byte) 225,
      (byte) 217
    };
    byte[] numArray10 = new byte[7];
    numArray10[5] = (byte) 100;
    numArray10[1] = (byte) 28;
    numArray10[3] = (byte) 15;
    numArray10[2] = (byte) 131;
    numArray10[4] = (byte) 106;
    numArray10[6] = (byte) 204;
    numArray10[0] = (byte) 14;
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 7);
    for (int index = 0; index < 7; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }
}
