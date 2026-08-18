// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_361
// Assembly: Intermech.AltiumDesigner.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 4CE9F573-7E4B-4FE9-9600-ADBDE2EC9D6B
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Integrator.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_361
{
  private static byte[] sspq = new byte[80 /*0x50*/]
  {
    (byte) 67,
    (byte) 124,
    (byte) 101,
    (byte) 128 /*0x80*/,
    (byte) 52,
    (byte) 182,
    (byte) 55,
    (byte) 30,
    (byte) 154,
    (byte) 212,
    (byte) 124,
    (byte) 233,
    (byte) 28,
    (byte) 245,
    (byte) 56,
    (byte) 233,
    (byte) 245,
    (byte) 54,
    (byte) 42,
    byte.MaxValue,
    (byte) 73,
    (byte) 87,
    (byte) 6,
    (byte) 211,
    (byte) 146,
    (byte) 74,
    (byte) 163,
    (byte) 17,
    (byte) 201,
    (byte) 22,
    (byte) 79,
    (byte) 42,
    (byte) 8,
    (byte) 101,
    (byte) 114,
    (byte) 203,
    (byte) 159,
    (byte) 171,
    (byte) 117,
    (byte) 158,
    (byte) 68,
    (byte) 38,
    (byte) 152,
    (byte) 148,
    (byte) 169,
    (byte) 139,
    (byte) 5,
    (byte) 248,
    (byte) 173,
    (byte) 21,
    (byte) 136,
    (byte) 27,
    (byte) 79,
    (byte) 203,
    (byte) 34,
    (byte) 48 /*0x30*/,
    (byte) 142,
    (byte) 219,
    (byte) 50,
    (byte) 75,
    (byte) 190,
    (byte) 75,
    (byte) 246,
    (byte) 195,
    (byte) 239,
    (byte) 117,
    (byte) 207,
    (byte) 96 /*0x60*/,
    (byte) 228,
    (byte) 125,
    (byte) 176 /*0xB0*/,
    (byte) 215,
    (byte) 93,
    (byte) 120,
    (byte) 126,
    (byte) 185,
    (byte) 245,
    (byte) 101,
    (byte) 198,
    (byte) 99
  };
  private static byte[] sspr = new byte[80 /*0x50*/]
  {
    (byte) 211,
    (byte) 176 /*0xB0*/,
    (byte) 225,
    (byte) 231,
    (byte) 149,
    (byte) 32 /*0x20*/,
    (byte) 179,
    (byte) 44,
    (byte) 18,
    (byte) 193,
    (byte) 45,
    (byte) 131,
    (byte) 216,
    (byte) 167,
    (byte) 222,
    (byte) 109,
    (byte) 169,
    (byte) 30,
    (byte) 199,
    (byte) 142,
    (byte) 53,
    (byte) 142,
    (byte) 3,
    (byte) 188,
    (byte) 107,
    (byte) 246,
    (byte) 226,
    (byte) 110,
    (byte) 177,
    (byte) 118,
    (byte) 122,
    (byte) 76,
    (byte) 188,
    (byte) 72,
    (byte) 187,
    (byte) 91,
    (byte) 56,
    (byte) 109,
    (byte) 92,
    (byte) 243,
    (byte) 181,
    (byte) 187,
    (byte) 18,
    (byte) 103,
    (byte) 210,
    (byte) 7,
    (byte) 157,
    (byte) 113,
    (byte) 65,
    (byte) 180,
    (byte) 104,
    (byte) 67,
    (byte) 170,
    (byte) 166,
    (byte) 214,
    (byte) 118,
    (byte) 217,
    (byte) 60,
    (byte) 173,
    (byte) 87,
    (byte) 21,
    (byte) 9,
    (byte) 101,
    (byte) 137,
    (byte) 246,
    (byte) 126,
    (byte) 166,
    (byte) 103,
    (byte) 73,
    (byte) 233,
    (byte) 94,
    (byte) 125,
    (byte) 144 /*0x90*/,
    (byte) 108,
    (byte) 226,
    (byte) 130,
    (byte) 254,
    (byte) 75,
    (byte) 221,
    (byte) 111
  };

  internal static string ssp_altium_362()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[91];
      byte[] numArray2 = new byte[55]
      {
        (byte) 164,
        (byte) 168,
        (byte) 104,
        (byte) 67,
        (byte) 47,
        (byte) 197,
        (byte) 24,
        (byte) 97,
        (byte) 209,
        (byte) 138,
        (byte) 30,
        (byte) 210,
        (byte) 164,
        (byte) 169,
        (byte) 32 /*0x20*/,
        (byte) 61,
        (byte) 4,
        (byte) 160 /*0xA0*/,
        (byte) 195,
        (byte) 202,
        (byte) 115,
        (byte) 164,
        (byte) 61,
        (byte) 189,
        (byte) 39,
        (byte) 241,
        (byte) 67,
        (byte) 85,
        (byte) 79,
        (byte) 192 /*0xC0*/,
        (byte) 247,
        (byte) 79,
        (byte) 49,
        (byte) 155,
        (byte) 172,
        (byte) 112 /*0x70*/,
        (byte) 241,
        (byte) 129,
        (byte) 55,
        (byte) 18,
        (byte) 173,
        (byte) 175,
        (byte) 28,
        (byte) 2,
        (byte) 144 /*0x90*/,
        (byte) 143,
        (byte) 178,
        (byte) 4,
        (byte) 203,
        (byte) 235,
        (byte) 179,
        (byte) 173,
        (byte) 135,
        (byte) 186,
        (byte) 208 /*0xD0*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 209,
        (byte) 184,
        (byte) 49,
        (byte) 195,
        (byte) 161,
        (byte) 192 /*0xC0*/,
        (byte) 183,
        (byte) 119,
        (byte) 141,
        (byte) 145,
        (byte) 183,
        (byte) 199,
        (byte) 224 /*0xE0*/,
        (byte) 17,
        (byte) 15,
        (byte) 5,
        (byte) 66,
        (byte) 104,
        (byte) 69,
        (byte) 226,
        (byte) 24,
        (byte) 37,
        (byte) 96 /*0x60*/,
        (byte) 18,
        (byte) 69,
        (byte) 198,
        (byte) 63 /*0x3F*/,
        (byte) 137,
        (byte) 206,
        (byte) 1,
        (byte) 196,
        (byte) 188,
        (byte) 128 /*0x80*/,
        (byte) 18,
        (byte) 198,
        (byte) 190,
        (byte) 230,
        (byte) 204,
        (byte) 206,
        (byte) 193,
        (byte) 16 /*0x10*/,
        (byte) 167,
        (byte) 35,
        (byte) 218,
        (byte) 139,
        (byte) 191,
        (byte) 128 /*0x80*/,
        (byte) 80 /*0x50*/,
        (byte) 62,
        (byte) 33,
        (byte) 198,
        (byte) 171,
        (byte) 222,
        (byte) 91,
        (byte) 58
      };
      key.Query(true, 334, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[36]
      {
        (byte) 171,
        (byte) 27,
        (byte) 62,
        (byte) 129,
        (byte) 194,
        (byte) 18,
        (byte) 17,
        (byte) 147,
        (byte) 206,
        (byte) 161,
        (byte) 8,
        (byte) 253,
        (byte) 176 /*0xB0*/,
        (byte) 76,
        (byte) 131,
        (byte) 104,
        (byte) 177,
        (byte) 28,
        (byte) 195,
        (byte) 135,
        (byte) 58,
        (byte) 45,
        (byte) 151,
        (byte) 62,
        (byte) 63 /*0x3F*/,
        (byte) 138,
        (byte) 100,
        (byte) 12,
        (byte) 73,
        (byte) 200,
        (byte) 118,
        (byte) 203,
        (byte) 172,
        (byte) 5,
        (byte) 70,
        (byte) 102
      };
      byte[] numArray5 = new byte[36];
      numArray5[23] = (byte) 108;
      numArray5[3] = (byte) 46;
      numArray5[2] = (byte) 56;
      numArray5[30] = (byte) 190;
      numArray5[4] = (byte) 7;
      numArray5[8] = (byte) 8;
      numArray5[19] = (byte) 100;
      numArray5[7] = (byte) 189;
      numArray5[5] = (byte) 234;
      numArray5[28] = (byte) 5;
      numArray5[20] = (byte) 230;
      numArray5[24] = (byte) 154;
      numArray5[12] = (byte) 13;
      numArray5[13] = (byte) 107;
      numArray5[25] = (byte) 202;
      numArray5[6] = (byte) 18;
      numArray5[22] = (byte) 41;
      numArray5[17] = (byte) 235;
      numArray5[32 /*0x20*/] = (byte) 159;
      numArray5[14] = (byte) 43;
      numArray5[33] = (byte) 98;
      numArray5[21] = (byte) 96 /*0x60*/;
      numArray5[10] = (byte) 143;
      numArray5[15] = (byte) 144 /*0x90*/;
      numArray5[18] = byte.MaxValue;
      numArray5[34] = (byte) 51;
      numArray5[26] = (byte) 215;
      numArray5[27] = (byte) 52;
      numArray5[9] = (byte) 30;
      numArray5[16 /*0x10*/] = (byte) 200;
      numArray5[11] = (byte) 118;
      numArray5[31 /*0x1F*/] = (byte) 97;
      numArray5[0] = (byte) 216;
      numArray5[1] = (byte) 99;
      numArray5[29] = (byte) 254;
      numArray5[35] = (byte) 56;
      key.Query(true, 334, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[91];
    byte[] numArray7 = new byte[55]
    {
      (byte) 203,
      (byte) 107,
      (byte) 169,
      (byte) 148,
      (byte) 55,
      (byte) 110,
      (byte) 197,
      (byte) 219,
      (byte) 5,
      (byte) 242,
      (byte) 216,
      (byte) 189,
      (byte) 51,
      (byte) 213,
      (byte) 199,
      (byte) 226,
      (byte) 249,
      (byte) 78,
      (byte) 178,
      (byte) 238,
      (byte) 6,
      (byte) 37,
      (byte) 187,
      (byte) 195,
      (byte) 108,
      (byte) 182,
      (byte) 113,
      (byte) 152,
      (byte) 19,
      (byte) 213,
      (byte) 181,
      (byte) 108,
      (byte) 230,
      (byte) 251,
      (byte) 101,
      (byte) 19,
      (byte) 218,
      (byte) 106,
      (byte) 10,
      (byte) 202,
      (byte) 233,
      (byte) 40,
      (byte) 68,
      (byte) 20,
      (byte) 81,
      (byte) 127 /*0x7F*/,
      (byte) 29,
      (byte) 213,
      (byte) 62,
      (byte) 58,
      (byte) 117,
      (byte) 210,
      (byte) 127 /*0x7F*/,
      (byte) 55,
      (byte) 118
    };
    byte[] numArray8 = new byte[55];
    numArray8[14] = (byte) 67;
    numArray8[40] = (byte) 47;
    numArray8[32 /*0x20*/] = (byte) 254;
    numArray8[3] = (byte) 143;
    numArray8[4] = (byte) 30;
    numArray8[1] = (byte) 242;
    numArray8[19] = (byte) 65;
    numArray8[15] = (byte) 128 /*0x80*/;
    numArray8[8] = (byte) 108;
    numArray8[53] = (byte) 22;
    numArray8[10] = (byte) 118;
    numArray8[11] = (byte) 236;
    numArray8[51] = (byte) 163;
    numArray8[45] = (byte) 46;
    numArray8[49] = (byte) 122;
    numArray8[13] = (byte) 246;
    numArray8[16 /*0x10*/] = (byte) 22;
    numArray8[17] = (byte) 161;
    numArray8[12] = (byte) 138;
    numArray8[28] = (byte) 22;
    numArray8[22] = (byte) 44;
    numArray8[21] = (byte) 252;
    numArray8[46] = (byte) 148;
    numArray8[2] = (byte) 143;
    numArray8[24] = (byte) 3;
    numArray8[25] = (byte) 229;
    numArray8[26] = (byte) 6;
    numArray8[27] = (byte) 243;
    numArray8[18] = (byte) 0;
    numArray8[6] = (byte) 163;
    numArray8[33] = (byte) 162;
    numArray8[30] = (byte) 215;
    numArray8[43] = (byte) 251;
    numArray8[23] = (byte) 185;
    numArray8[7] = (byte) 173;
    numArray8[35] = (byte) 213;
    numArray8[36] = (byte) 220;
    numArray8[52] = (byte) 101;
    numArray8[38] = (byte) 84;
    numArray8[34] = (byte) 139;
    numArray8[5] = (byte) 118;
    numArray8[41] = (byte) 133;
    numArray8[42] = (byte) 67;
    numArray8[48 /*0x30*/] = (byte) 187;
    numArray8[44] = (byte) 212;
    numArray8[9] = (byte) 218;
    numArray8[31 /*0x1F*/] = (byte) 109;
    numArray8[47] = (byte) 156;
    numArray8[39] = (byte) 247;
    numArray8[0] = (byte) 144 /*0x90*/;
    numArray8[50] = (byte) 172;
    numArray8[29] = (byte) 131;
    numArray8[37] = (byte) 173;
    numArray8[20] = (byte) 15;
    numArray8[54] = (byte) 144 /*0x90*/;
    key.Query(true, 334, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[36]
    {
      (byte) 117,
      (byte) 58,
      (byte) 232,
      (byte) 96 /*0x60*/,
      (byte) 209,
      (byte) 239,
      (byte) 208 /*0xD0*/,
      (byte) 90,
      (byte) 127 /*0x7F*/,
      (byte) 39,
      (byte) 177,
      (byte) 175,
      (byte) 81,
      (byte) 20,
      (byte) 76,
      (byte) 186,
      (byte) 26,
      (byte) 72,
      (byte) 124,
      (byte) 205,
      (byte) 208 /*0xD0*/,
      (byte) 140,
      (byte) 69,
      (byte) 178,
      (byte) 54,
      (byte) 209,
      (byte) 166,
      (byte) 121,
      (byte) 83,
      (byte) 85,
      (byte) 125,
      (byte) 236,
      (byte) 252,
      (byte) 90,
      (byte) 126,
      (byte) 8
    };
    byte[] numArray10 = new byte[36]
    {
      (byte) 113,
      (byte) 23,
      (byte) 223,
      (byte) 163,
      (byte) 145,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 189,
      (byte) 139,
      (byte) 48 /*0x30*/,
      (byte) 154,
      (byte) 131,
      (byte) 194,
      (byte) 227,
      (byte) 99,
      (byte) 49,
      (byte) 18,
      (byte) 163,
      (byte) 183,
      (byte) 252,
      (byte) 66,
      (byte) 242,
      (byte) 68,
      (byte) 195,
      (byte) 94,
      (byte) 179,
      (byte) 234,
      (byte) 167,
      (byte) 113,
      (byte) 5,
      (byte) 155,
      (byte) 29,
      (byte) 187,
      (byte) 166,
      (byte) 159,
      (byte) 51
    };
    key.Query(true, 334, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 36);
    for (int index = 0; index < 36; ++index)
      numArray6[index + 55] ^= numArray10[index];
    byte[] numArray11 = new byte[23];
    byte[] response = new byte[23];
    Array.Copy((Array) sc_361.sspq, 0, (Array) numArray11, 0, 23);
    key.Query(true, 334, numArray11, response);
    Array.Copy((Array) sc_361.sspr, 0, (Array) numArray11, 0, 23);
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

  internal static string ssp_altium_363()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[182];
      byte[] numArray2 = new byte[55]
      {
        (byte) 169,
        (byte) 197,
        (byte) 178,
        (byte) 247,
        (byte) 149,
        (byte) 67,
        (byte) 26,
        (byte) 174,
        (byte) 92,
        (byte) 11,
        (byte) 138,
        (byte) 84,
        (byte) 38,
        (byte) 196,
        (byte) 126,
        (byte) 128 /*0x80*/,
        (byte) 154,
        (byte) 135,
        (byte) 223,
        (byte) 127 /*0x7F*/,
        (byte) 217,
        (byte) 147,
        (byte) 208 /*0xD0*/,
        (byte) 6,
        (byte) 33,
        (byte) 149,
        (byte) 19,
        (byte) 106,
        (byte) 66,
        (byte) 236,
        (byte) 40,
        (byte) 236,
        (byte) 117,
        (byte) 60,
        (byte) 215,
        (byte) 110,
        (byte) 3,
        (byte) 64 /*0x40*/,
        (byte) 136,
        (byte) 86,
        (byte) 186,
        (byte) 206,
        (byte) 141,
        (byte) 16 /*0x10*/,
        (byte) 106,
        (byte) 228,
        (byte) 194,
        (byte) 48 /*0x30*/,
        (byte) 184,
        (byte) 169,
        (byte) 101,
        (byte) 193,
        (byte) 119,
        (byte) 135,
        (byte) 80 /*0x50*/
      };
      byte[] numArray3 = new byte[55];
      numArray3[3] = (byte) 26;
      numArray3[49] = (byte) 177;
      numArray3[11] = (byte) 221;
      numArray3[43] = (byte) 207;
      numArray3[4] = (byte) 73;
      numArray3[28] = (byte) 224 /*0xE0*/;
      numArray3[5] = (byte) 109;
      numArray3[39] = (byte) 0;
      numArray3[20] = (byte) 209;
      numArray3[1] = (byte) 195;
      numArray3[47] = (byte) 1;
      numArray3[50] = (byte) 70;
      numArray3[13] = (byte) 65;
      numArray3[29] = (byte) 107;
      numArray3[14] = (byte) 83;
      numArray3[15] = (byte) 211;
      numArray3[0] = (byte) 157;
      numArray3[8] = (byte) 31 /*0x1F*/;
      numArray3[2] = (byte) 52;
      numArray3[19] = (byte) 173;
      numArray3[54] = (byte) 250;
      numArray3[21] = (byte) 159;
      numArray3[24] = (byte) 249;
      numArray3[41] = (byte) 175;
      numArray3[22] = (byte) 254;
      numArray3[25] = (byte) 119;
      numArray3[26] = (byte) 226;
      numArray3[27] = (byte) 176 /*0xB0*/;
      numArray3[34] = (byte) 44;
      numArray3[10] = (byte) 196;
      numArray3[30] = (byte) 162;
      numArray3[52] = (byte) 36;
      numArray3[32 /*0x20*/] = (byte) 31 /*0x1F*/;
      numArray3[44] = (byte) 182;
      numArray3[18] = (byte) 49;
      numArray3[35] = (byte) 54;
      numArray3[36] = (byte) 240 /*0xF0*/;
      numArray3[23] = (byte) 3;
      numArray3[37] = (byte) 37;
      numArray3[16 /*0x10*/] = (byte) 245;
      numArray3[40] = (byte) 61;
      numArray3[6] = (byte) 106;
      numArray3[42] = (byte) 189;
      numArray3[17] = (byte) 83;
      numArray3[46] = (byte) 42;
      numArray3[45] = (byte) 110;
      numArray3[38] = (byte) 126;
      numArray3[9] = (byte) 200;
      numArray3[48 /*0x30*/] = (byte) 192 /*0xC0*/;
      numArray3[12] = (byte) 244;
      numArray3[7] = (byte) 59;
      numArray3[51] = (byte) 166;
      numArray3[33] = (byte) 72;
      numArray3[53] = (byte) 164;
      numArray3[31 /*0x1F*/] = (byte) 86;
      key.Query(true, 334, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[7] = (byte) 125;
      numArray4[1] = (byte) 153;
      numArray4[40] = (byte) 51;
      numArray4[8] = (byte) 42;
      numArray4[4] = (byte) 253;
      numArray4[48 /*0x30*/] = (byte) 27;
      numArray4[22] = (byte) 54;
      numArray4[50] = (byte) 139;
      numArray4[17] = (byte) 187;
      numArray4[9] = (byte) 240 /*0xF0*/;
      numArray4[47] = (byte) 5;
      numArray4[11] = (byte) 44;
      numArray4[10] = (byte) 253;
      numArray4[6] = (byte) 233;
      numArray4[33] = (byte) 69;
      numArray4[0] = (byte) 16 /*0x10*/;
      numArray4[34] = (byte) 70;
      numArray4[25] = (byte) 9;
      numArray4[49] = (byte) 75;
      numArray4[44] = (byte) 219;
      numArray4[20] = (byte) 146;
      numArray4[15] = (byte) 119;
      numArray4[2] = (byte) 45;
      numArray4[23] = (byte) 249;
      numArray4[24] = (byte) 158;
      numArray4[39] = (byte) 144 /*0x90*/;
      numArray4[26] = (byte) 109;
      numArray4[3] = (byte) 77;
      numArray4[28] = (byte) 164;
      numArray4[13] = (byte) 38;
      numArray4[27] = (byte) 123;
      numArray4[18] = (byte) 168;
      numArray4[32 /*0x20*/] = (byte) 177;
      numArray4[5] = (byte) 210;
      numArray4[46] = (byte) 19;
      numArray4[35] = (byte) 209;
      numArray4[36] = (byte) 90;
      numArray4[30] = (byte) 44;
      numArray4[38] = (byte) 12;
      numArray4[53] = (byte) 38;
      numArray4[37] = (byte) 216;
      numArray4[41] = (byte) 176 /*0xB0*/;
      numArray4[19] = (byte) 233;
      numArray4[16 /*0x10*/] = (byte) 98;
      numArray4[21] = (byte) 207;
      numArray4[43] = (byte) 21;
      numArray4[42] = (byte) 162;
      numArray4[45] = (byte) 23;
      numArray4[29] = (byte) 133;
      numArray4[31 /*0x1F*/] = (byte) 167;
      numArray4[14] = (byte) 239;
      numArray4[51] = (byte) 6;
      numArray4[52] = (byte) 156;
      numArray4[12] = (byte) 149;
      numArray4[54] = (byte) 162;
      byte[] numArray5 = new byte[55]
      {
        (byte) 107,
        (byte) 13,
        (byte) 221,
        (byte) 54,
        (byte) 14,
        (byte) 203,
        (byte) 101,
        (byte) 97,
        (byte) 155,
        (byte) 121,
        (byte) 91,
        (byte) 34,
        (byte) 203,
        (byte) 142,
        (byte) 28,
        (byte) 6,
        (byte) 247,
        (byte) 83,
        (byte) 130,
        (byte) 157,
        (byte) 177,
        (byte) 95,
        (byte) 110,
        (byte) 9,
        (byte) 42,
        (byte) 79,
        (byte) 77,
        (byte) 7,
        (byte) 72,
        (byte) 239,
        (byte) 40,
        (byte) 188,
        (byte) 121,
        (byte) 240 /*0xF0*/,
        (byte) 0,
        (byte) 204,
        (byte) 154,
        (byte) 202,
        (byte) 237,
        (byte) 106,
        (byte) 118,
        (byte) 193,
        (byte) 160 /*0xA0*/,
        (byte) 81,
        (byte) 105,
        (byte) 204,
        (byte) 115,
        (byte) 31 /*0x1F*/,
        (byte) 74,
        (byte) 149,
        (byte) 214,
        (byte) 175,
        (byte) 10,
        (byte) 91,
        (byte) 225
      };
      key.Query(true, 334, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 22,
        (byte) 39,
        (byte) 254,
        (byte) 253,
        (byte) 14,
        (byte) 128 /*0x80*/,
        (byte) 170,
        (byte) 157,
        (byte) 76,
        (byte) 240 /*0xF0*/,
        (byte) 173,
        (byte) 218,
        (byte) 175,
        (byte) 247,
        (byte) 96 /*0x60*/,
        (byte) 150,
        (byte) 237,
        (byte) 239,
        (byte) 10,
        (byte) 47,
        (byte) 207,
        (byte) 152,
        (byte) 235,
        (byte) 50,
        (byte) 65,
        (byte) 98,
        (byte) 42,
        (byte) 247,
        (byte) 206,
        (byte) 133,
        (byte) 25,
        (byte) 225,
        (byte) 242,
        (byte) 45,
        (byte) 15,
        (byte) 2,
        (byte) 25,
        (byte) 44,
        (byte) 183,
        (byte) 87,
        (byte) 83,
        (byte) 180,
        (byte) 56,
        (byte) 171,
        (byte) 21,
        (byte) 75,
        (byte) 77,
        (byte) 55,
        (byte) 161,
        (byte) 192 /*0xC0*/,
        (byte) 151,
        (byte) 129,
        (byte) 181,
        (byte) 227,
        (byte) 113
      };
      byte[] numArray7 = new byte[55];
      numArray7[24] = (byte) 230;
      numArray7[1] = (byte) 114;
      numArray7[14] = (byte) 14;
      numArray7[3] = (byte) 94;
      numArray7[7] = (byte) 217;
      numArray7[5] = (byte) 191;
      numArray7[50] = (byte) 231;
      numArray7[27] = (byte) 26;
      numArray7[29] = (byte) 129;
      numArray7[9] = (byte) 105;
      numArray7[41] = (byte) 141;
      numArray7[11] = (byte) 126;
      numArray7[12] = (byte) 108;
      numArray7[43] = (byte) 171;
      numArray7[48 /*0x30*/] = (byte) 77;
      numArray7[15] = (byte) 103;
      numArray7[16 /*0x10*/] = (byte) 218;
      numArray7[17] = (byte) 236;
      numArray7[4] = (byte) 87;
      numArray7[13] = (byte) 64 /*0x40*/;
      numArray7[20] = (byte) 142;
      numArray7[21] = (byte) 196;
      numArray7[49] = (byte) 183;
      numArray7[23] = (byte) 151;
      numArray7[25] = (byte) 103;
      numArray7[39] = (byte) 173;
      numArray7[52] = (byte) 35;
      numArray7[18] = (byte) 0;
      numArray7[28] = (byte) 254;
      numArray7[6] = (byte) 2;
      numArray7[30] = byte.MaxValue;
      numArray7[22] = (byte) 227;
      numArray7[32 /*0x20*/] = (byte) 70;
      numArray7[33] = (byte) 128 /*0x80*/;
      numArray7[54] = (byte) 102;
      numArray7[38] = (byte) 28;
      numArray7[45] = (byte) 22;
      numArray7[46] = (byte) 252;
      numArray7[42] = (byte) 140;
      numArray7[2] = (byte) 251;
      numArray7[40] = (byte) 126;
      numArray7[34] = (byte) 247;
      numArray7[36] = (byte) 173;
      numArray7[8] = (byte) 247;
      numArray7[44] = (byte) 223;
      numArray7[37] = (byte) 191;
      numArray7[19] = (byte) 220;
      numArray7[47] = (byte) 63 /*0x3F*/;
      numArray7[35] = (byte) 76;
      numArray7[26] = (byte) 244;
      numArray7[10] = (byte) 100;
      numArray7[51] = (byte) 182;
      numArray7[53] = (byte) 36;
      numArray7[31 /*0x1F*/] = (byte) 11;
      numArray7[0] = (byte) 157;
      key.Query(true, 334, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[17]
      {
        (byte) 174,
        (byte) 197,
        (byte) 149,
        (byte) 98,
        (byte) 92,
        (byte) 192 /*0xC0*/,
        (byte) 217,
        (byte) 63 /*0x3F*/,
        (byte) 159,
        (byte) 216,
        (byte) 197,
        (byte) 86,
        (byte) 150,
        (byte) 46,
        (byte) 79,
        (byte) 45,
        (byte) 178
      };
      byte[] numArray9 = new byte[17]
      {
        (byte) 130,
        (byte) 70,
        (byte) 44,
        (byte) 90,
        (byte) 246,
        (byte) 88,
        (byte) 129,
        (byte) 10,
        (byte) 0,
        (byte) 15,
        (byte) 138,
        (byte) 233,
        (byte) 186,
        (byte) 217,
        (byte) 214,
        (byte) 228,
        (byte) 131
      };
      key.Query(true, 334, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 17);
      for (int index = 0; index < 17; ++index)
        numArray1[index + 165] ^= numArray9[index];
      byte[] numArray10 = new byte[46];
      byte[] response = new byte[46];
      Array.Copy((Array) sc_361.sspq, 23, (Array) numArray10, 0, 46);
      key.Query(true, 334, numArray10, response);
      Array.Copy((Array) sc_361.sspr, 23, (Array) numArray10, 0, 46);
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
    byte[] numArray11 = new byte[182];
    byte[] numArray12 = new byte[55];
    numArray12[40] = (byte) 19;
    numArray12[17] = (byte) 218;
    numArray12[44] = (byte) 43;
    numArray12[3] = (byte) 183;
    numArray12[39] = (byte) 212;
    numArray12[5] = (byte) 246;
    numArray12[31 /*0x1F*/] = (byte) 187;
    numArray12[7] = (byte) 85;
    numArray12[8] = (byte) 216;
    numArray12[45] = (byte) 5;
    numArray12[33] = (byte) 20;
    numArray12[11] = (byte) 69;
    numArray12[2] = (byte) 163;
    numArray12[29] = (byte) 164;
    numArray12[23] = (byte) 121;
    numArray12[54] = (byte) 162;
    numArray12[16 /*0x10*/] = (byte) 52;
    numArray12[26] = (byte) 249;
    numArray12[18] = (byte) 60;
    numArray12[19] = (byte) 67;
    numArray12[20] = (byte) 66;
    numArray12[6] = (byte) 14;
    numArray12[43] = (byte) 178;
    numArray12[41] = (byte) 218;
    numArray12[10] = (byte) 7;
    numArray12[4] = (byte) 20;
    numArray12[36] = (byte) 229;
    numArray12[52] = (byte) 147;
    numArray12[22] = (byte) 195;
    numArray12[50] = (byte) 187;
    numArray12[30] = (byte) 52;
    numArray12[1] = (byte) 143;
    numArray12[32 /*0x20*/] = (byte) 164;
    numArray12[28] = (byte) 212;
    numArray12[34] = (byte) 226;
    numArray12[35] = (byte) 133;
    numArray12[51] = (byte) 154;
    numArray12[27] = (byte) 47;
    numArray12[38] = (byte) 59;
    numArray12[15] = (byte) 141;
    numArray12[37] = (byte) 99;
    numArray12[13] = (byte) 99;
    numArray12[42] = (byte) 181;
    numArray12[9] = (byte) 41;
    numArray12[24] = (byte) 42;
    numArray12[25] = (byte) 223;
    numArray12[46] = (byte) 19;
    numArray12[47] = (byte) 231;
    numArray12[48 /*0x30*/] = (byte) 65;
    numArray12[49] = (byte) 53;
    numArray12[21] = (byte) 61;
    numArray12[0] = (byte) 202;
    numArray12[12] = (byte) 171;
    numArray12[53] = (byte) 28;
    numArray12[14] = (byte) 180;
    byte[] numArray13 = new byte[55];
    numArray13[13] = (byte) 187;
    numArray13[19] = (byte) 133;
    numArray13[2] = (byte) 133;
    numArray13[54] = (byte) 77;
    numArray13[16 /*0x10*/] = byte.MaxValue;
    numArray13[27] = (byte) 173;
    numArray13[32 /*0x20*/] = (byte) 187;
    numArray13[3] = (byte) 234;
    numArray13[8] = (byte) 34;
    numArray13[34] = (byte) 177;
    numArray13[43] = (byte) 229;
    numArray13[11] = (byte) 151;
    numArray13[10] = (byte) 110;
    numArray13[9] = (byte) 5;
    numArray13[50] = (byte) 234;
    numArray13[15] = (byte) 246;
    numArray13[48 /*0x30*/] = (byte) 84;
    numArray13[17] = (byte) 232;
    numArray13[18] = (byte) 222;
    numArray13[42] = (byte) 241;
    numArray13[20] = (byte) 243;
    numArray13[49] = (byte) 12;
    numArray13[21] = (byte) 105;
    numArray13[0] = (byte) 7;
    numArray13[24] = (byte) 104;
    numArray13[7] = (byte) 227;
    numArray13[39] = (byte) 152;
    numArray13[44] = (byte) 136;
    numArray13[33] = (byte) 113;
    numArray13[29] = (byte) 75;
    numArray13[38] = (byte) 194;
    numArray13[31 /*0x1F*/] = (byte) 169;
    numArray13[1] = (byte) 97;
    numArray13[45] = (byte) 180;
    numArray13[12] = (byte) 234;
    numArray13[35] = (byte) 65;
    numArray13[36] = (byte) 178;
    numArray13[6] = (byte) 65;
    numArray13[14] = (byte) 155;
    numArray13[23] = (byte) 220;
    numArray13[40] = (byte) 60;
    numArray13[41] = (byte) 157;
    numArray13[4] = (byte) 190;
    numArray13[52] = (byte) 223;
    numArray13[22] = (byte) 140;
    numArray13[26] = (byte) 167;
    numArray13[46] = (byte) 143;
    numArray13[47] = (byte) 197;
    numArray13[37] = (byte) 92;
    numArray13[5] = (byte) 20;
    numArray13[25] = (byte) 147;
    numArray13[51] = (byte) 131;
    numArray13[30] = (byte) 123;
    numArray13[53] = (byte) 58;
    numArray13[28] = (byte) 235;
    key.Query(true, 334, numArray12, numArray12);
    Array.Copy((Array) numArray12, 0, (Array) numArray11, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index] ^= numArray13[index];
    byte[] numArray14 = new byte[55];
    numArray14[15] = (byte) 250;
    numArray14[3] = (byte) 65;
    numArray14[31 /*0x1F*/] = (byte) 10;
    numArray14[19] = (byte) 206;
    numArray14[4] = (byte) 20;
    numArray14[5] = (byte) 208 /*0xD0*/;
    numArray14[41] = (byte) 162;
    numArray14[7] = (byte) 190;
    numArray14[21] = (byte) 177;
    numArray14[49] = (byte) 104;
    numArray14[10] = (byte) 238;
    numArray14[1] = (byte) 167;
    numArray14[12] = (byte) 119;
    numArray14[13] = (byte) 13;
    numArray14[32 /*0x20*/] = (byte) 76;
    numArray14[48 /*0x30*/] = (byte) 104;
    numArray14[30] = (byte) 166;
    numArray14[39] = (byte) 167;
    numArray14[16 /*0x10*/] = (byte) 166;
    numArray14[0] = (byte) 109;
    numArray14[8] = (byte) 36;
    numArray14[18] = (byte) 151;
    numArray14[36] = (byte) 135;
    numArray14[23] = (byte) 250;
    numArray14[14] = (byte) 138;
    numArray14[25] = (byte) 63 /*0x3F*/;
    numArray14[26] = (byte) 114;
    numArray14[27] = (byte) 10;
    numArray14[28] = (byte) 59;
    numArray14[29] = (byte) 99;
    numArray14[20] = (byte) 243;
    numArray14[40] = (byte) 232;
    numArray14[51] = (byte) 62;
    numArray14[33] = (byte) 152;
    numArray14[34] = (byte) 114;
    numArray14[35] = (byte) 199;
    numArray14[38] = (byte) 48 /*0x30*/;
    numArray14[37] = (byte) 169;
    numArray14[11] = (byte) 221;
    numArray14[46] = (byte) 36;
    numArray14[17] = (byte) 180;
    numArray14[6] = (byte) 77;
    numArray14[42] = (byte) 152;
    numArray14[24] = (byte) 110;
    numArray14[44] = (byte) 103;
    numArray14[45] = (byte) 94;
    numArray14[43] = (byte) 206;
    numArray14[47] = (byte) 145;
    numArray14[2] = (byte) 23;
    numArray14[22] = (byte) 115;
    numArray14[50] = (byte) 243;
    numArray14[54] = (byte) 114;
    numArray14[52] = (byte) 149;
    numArray14[53] = (byte) 50;
    numArray14[9] = (byte) 126;
    byte[] numArray15 = new byte[55];
    numArray15[24] = (byte) 133;
    numArray15[1] = (byte) 58;
    numArray15[31 /*0x1F*/] = (byte) 166;
    numArray15[46] = (byte) 69;
    numArray15[4] = (byte) 203;
    numArray15[50] = (byte) 194;
    numArray15[6] = (byte) 118;
    numArray15[48 /*0x30*/] = (byte) 245;
    numArray15[8] = (byte) 243;
    numArray15[27] = (byte) 102;
    numArray15[10] = (byte) 113;
    numArray15[37] = (byte) 121;
    numArray15[49] = (byte) 211;
    numArray15[13] = (byte) 105;
    numArray15[47] = (byte) 126;
    numArray15[22] = (byte) 52;
    numArray15[16 /*0x10*/] = (byte) 95;
    numArray15[26] = (byte) 182;
    numArray15[33] = (byte) 98;
    numArray15[19] = (byte) 194;
    numArray15[21] = (byte) 183;
    numArray15[28] = (byte) 8;
    numArray15[43] = (byte) 58;
    numArray15[23] = (byte) 80 /*0x50*/;
    numArray15[9] = (byte) 191;
    numArray15[25] = (byte) 41;
    numArray15[51] = (byte) 248;
    numArray15[14] = (byte) 182;
    numArray15[5] = (byte) 38;
    numArray15[29] = (byte) 49;
    numArray15[3] = (byte) 227;
    numArray15[2] = (byte) 148;
    numArray15[17] = (byte) 93;
    numArray15[45] = (byte) 69;
    numArray15[7] = (byte) 248;
    numArray15[35] = (byte) 63 /*0x3F*/;
    numArray15[36] = (byte) 54;
    numArray15[54] = (byte) 215;
    numArray15[38] = (byte) 130;
    numArray15[39] = (byte) 242;
    numArray15[40] = (byte) 8;
    numArray15[0] = (byte) 79;
    numArray15[42] = (byte) 237;
    numArray15[41] = (byte) 89;
    numArray15[44] = (byte) 214;
    numArray15[11] = (byte) 210;
    numArray15[15] = (byte) 100;
    numArray15[18] = (byte) 80 /*0x50*/;
    numArray15[20] = (byte) 159;
    numArray15[32 /*0x20*/] = (byte) 228;
    numArray15[30] = (byte) 67;
    numArray15[12] = (byte) 32 /*0x20*/;
    numArray15[52] = (byte) 179;
    numArray15[34] = (byte) 143;
    numArray15[53] = (byte) 252;
    key.Query(true, 334, numArray14, numArray14);
    Array.Copy((Array) numArray14, 0, (Array) numArray11, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 55] ^= numArray15[index];
    byte[] numArray16 = new byte[55]
    {
      (byte) 121,
      (byte) 127 /*0x7F*/,
      (byte) 230,
      (byte) 145,
      (byte) 163,
      (byte) 209,
      (byte) 214,
      (byte) 194,
      (byte) 159,
      (byte) 169,
      (byte) 179,
      (byte) 207,
      (byte) 44,
      (byte) 39,
      (byte) 18,
      (byte) 134,
      (byte) 216,
      (byte) 58,
      (byte) 194,
      (byte) 186,
      (byte) 38,
      (byte) 251,
      (byte) 234,
      (byte) 72,
      (byte) 215,
      (byte) 135,
      (byte) 241,
      (byte) 169,
      (byte) 25,
      (byte) 208 /*0xD0*/,
      (byte) 191,
      (byte) 22,
      (byte) 179,
      (byte) 35,
      (byte) 103,
      (byte) 148,
      (byte) 186,
      (byte) 60,
      (byte) 97,
      (byte) 157,
      (byte) 176 /*0xB0*/,
      (byte) 197,
      (byte) 117,
      (byte) 174,
      (byte) 216,
      (byte) 66,
      (byte) 72,
      (byte) 197,
      (byte) 189,
      (byte) 12,
      (byte) 38,
      (byte) 210,
      (byte) 43,
      (byte) 166,
      (byte) 185
    };
    byte[] numArray17 = new byte[55]
    {
      (byte) 191,
      (byte) 72,
      (byte) 194,
      (byte) 131,
      (byte) 103,
      (byte) 83,
      (byte) 183,
      (byte) 198,
      (byte) 59,
      (byte) 33,
      (byte) 133,
      (byte) 51,
      (byte) 104,
      (byte) 70,
      (byte) 236,
      (byte) 95,
      (byte) 61,
      (byte) 240 /*0xF0*/,
      (byte) 155,
      (byte) 165,
      (byte) 252,
      (byte) 37,
      (byte) 101,
      (byte) 113,
      (byte) 95,
      (byte) 205,
      (byte) 34,
      (byte) 217,
      (byte) 18,
      (byte) 28,
      (byte) 134,
      (byte) 96 /*0x60*/,
      (byte) 200,
      (byte) 135,
      (byte) 56,
      (byte) 153,
      (byte) 95,
      (byte) 53,
      (byte) 71,
      (byte) 53,
      (byte) 147,
      (byte) 199,
      (byte) 164,
      (byte) 253,
      (byte) 193,
      (byte) 42,
      (byte) 96 /*0x60*/,
      (byte) 89,
      (byte) 247,
      (byte) 29,
      (byte) 200,
      (byte) 230,
      (byte) 166,
      (byte) 126,
      (byte) 233
    };
    key.Query(true, 334, numArray16, numArray16);
    Array.Copy((Array) numArray16, 0, (Array) numArray11, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray11[index + 110] ^= numArray17[index];
    byte[] numArray18 = new byte[17]
    {
      (byte) 158,
      (byte) 186,
      (byte) 27,
      (byte) 224 /*0xE0*/,
      (byte) 44,
      (byte) 169,
      (byte) 206,
      (byte) 234,
      (byte) 156,
      (byte) 28,
      (byte) 9,
      (byte) 191,
      (byte) 125,
      (byte) 204,
      (byte) 156,
      (byte) 217,
      (byte) 37
    };
    byte[] numArray19 = new byte[17]
    {
      (byte) 3,
      (byte) 245,
      (byte) 232,
      (byte) 220,
      (byte) 104,
      (byte) 14,
      (byte) 200,
      (byte) 20,
      (byte) 170,
      (byte) 108,
      (byte) 129,
      (byte) 89,
      (byte) 141,
      (byte) 135,
      (byte) 86,
      (byte) 11,
      (byte) 243
    };
    key.Query(true, 334, numArray18, numArray18);
    Array.Copy((Array) numArray18, 0, (Array) numArray11, 165, 17);
    for (int index = 0; index < 17; ++index)
      numArray11[index + 165] ^= numArray19[index];
    return Encoding.UTF8.GetString(numArray11);
  }

  internal static string ssp_altium_364()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[54];
      byte[] numArray2 = new byte[54]
      {
        (byte) 111,
        (byte) 148,
        (byte) 63 /*0x3F*/,
        (byte) 44,
        (byte) 236,
        (byte) 12,
        (byte) 170,
        (byte) 236,
        (byte) 235,
        (byte) 12,
        (byte) 105,
        (byte) 233,
        byte.MaxValue,
        (byte) 28,
        (byte) 130,
        (byte) 69,
        (byte) 188,
        (byte) 131,
        (byte) 27,
        (byte) 167,
        (byte) 195,
        (byte) 59,
        (byte) 38,
        (byte) 63 /*0x3F*/,
        (byte) 5,
        (byte) 63 /*0x3F*/,
        (byte) 64 /*0x40*/,
        (byte) 138,
        (byte) 248,
        (byte) 6,
        (byte) 155,
        (byte) 88,
        (byte) 248,
        (byte) 88,
        (byte) 46,
        (byte) 37,
        (byte) 220,
        (byte) 180,
        (byte) 64 /*0x40*/,
        (byte) 156,
        (byte) 36,
        (byte) 254,
        (byte) 177,
        (byte) 15,
        (byte) 159,
        (byte) 214,
        (byte) 126,
        (byte) 175,
        (byte) 26,
        (byte) 148,
        (byte) 104,
        (byte) 135,
        (byte) 70,
        (byte) 222
      };
      byte[] numArray3 = new byte[54]
      {
        (byte) 21,
        (byte) 64 /*0x40*/,
        (byte) 151,
        (byte) 171,
        (byte) 24,
        (byte) 39,
        (byte) 178,
        (byte) 106,
        (byte) 172,
        (byte) 73,
        (byte) 109,
        (byte) 26,
        (byte) 224 /*0xE0*/,
        (byte) 198,
        (byte) 53,
        (byte) 169,
        (byte) 74,
        (byte) 217,
        (byte) 79,
        (byte) 40,
        (byte) 129,
        (byte) 48 /*0x30*/,
        (byte) 225,
        (byte) 178,
        (byte) 75,
        (byte) 65,
        (byte) 115,
        (byte) 195,
        (byte) 9,
        (byte) 46,
        (byte) 135,
        (byte) 42,
        (byte) 64 /*0x40*/,
        (byte) 179,
        (byte) 71,
        (byte) 136,
        (byte) 32 /*0x20*/,
        (byte) 252,
        (byte) 182,
        (byte) 138,
        (byte) 163,
        (byte) 60,
        (byte) 56,
        (byte) 54,
        (byte) 135,
        (byte) 99,
        (byte) 152,
        (byte) 47,
        (byte) 199,
        (byte) 0,
        (byte) 15,
        (byte) 2,
        (byte) 90,
        (byte) 14
      };
      key.Query(true, 334, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[54];
    byte[] numArray5 = new byte[54]
    {
      byte.MaxValue,
      (byte) 38,
      (byte) 172,
      (byte) 204,
      (byte) 31 /*0x1F*/,
      (byte) 243,
      (byte) 229,
      (byte) 113,
      (byte) 16 /*0x10*/,
      (byte) 241,
      (byte) 166,
      (byte) 238,
      (byte) 92,
      (byte) 104,
      (byte) 8,
      (byte) 66,
      (byte) 126,
      (byte) 131,
      (byte) 197,
      (byte) 137,
      (byte) 22,
      (byte) 159,
      (byte) 47,
      (byte) 193,
      (byte) 144 /*0x90*/,
      (byte) 112 /*0x70*/,
      (byte) 100,
      (byte) 157,
      (byte) 20,
      (byte) 18,
      (byte) 25,
      (byte) 224 /*0xE0*/,
      (byte) 250,
      (byte) 183,
      (byte) 221,
      (byte) 141,
      (byte) 148,
      (byte) 208 /*0xD0*/,
      (byte) 4,
      (byte) 10,
      (byte) 204,
      (byte) 158,
      (byte) 143,
      (byte) 229,
      (byte) 216,
      (byte) 193,
      (byte) 189,
      (byte) 44,
      (byte) 96 /*0x60*/,
      (byte) 68,
      (byte) 199,
      (byte) 91,
      (byte) 178,
      (byte) 170
    };
    byte[] numArray6 = new byte[54]
    {
      (byte) 151,
      (byte) 12,
      (byte) 137,
      (byte) 182,
      (byte) 148,
      (byte) 250,
      (byte) 147,
      (byte) 233,
      (byte) 174,
      (byte) 84,
      (byte) 24,
      (byte) 25,
      (byte) 90,
      (byte) 80 /*0x50*/,
      (byte) 48 /*0x30*/,
      (byte) 112 /*0x70*/,
      (byte) 154,
      (byte) 162,
      (byte) 130,
      (byte) 127 /*0x7F*/,
      (byte) 222,
      (byte) 17,
      (byte) 254,
      (byte) 114,
      (byte) 229,
      (byte) 197,
      (byte) 250,
      (byte) 219,
      (byte) 154,
      (byte) 227,
      (byte) 109,
      (byte) 141,
      (byte) 250,
      (byte) 111,
      (byte) 115,
      (byte) 182,
      (byte) 236,
      (byte) 0,
      (byte) 129,
      (byte) 184,
      (byte) 71,
      (byte) 134,
      (byte) 156,
      (byte) 239,
      (byte) 123,
      (byte) 245,
      (byte) 183,
      (byte) 171,
      (byte) 62,
      (byte) 81,
      (byte) 89,
      (byte) 198,
      (byte) 202,
      (byte) 7
    };
    key.Query(true, 334, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 54);
    for (int index = 0; index < 54; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_altium_365()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[77];
      byte[] numArray2 = new byte[55];
      numArray2[54] = (byte) 1;
      numArray2[14] = (byte) 126;
      numArray2[2] = (byte) 143;
      numArray2[44] = (byte) 160 /*0xA0*/;
      numArray2[4] = (byte) 92;
      numArray2[5] = (byte) 254;
      numArray2[6] = (byte) 215;
      numArray2[42] = (byte) 205;
      numArray2[45] = (byte) 229;
      numArray2[9] = (byte) 165;
      numArray2[36] = (byte) 123;
      numArray2[3] = (byte) 71;
      numArray2[10] = (byte) 139;
      numArray2[23] = (byte) 56;
      numArray2[0] = (byte) 60;
      numArray2[11] = (byte) 122;
      numArray2[41] = (byte) 18;
      numArray2[17] = (byte) 120;
      numArray2[15] = (byte) 187;
      numArray2[19] = (byte) 74;
      numArray2[7] = (byte) 197;
      numArray2[21] = (byte) 206;
      numArray2[31 /*0x1F*/] = (byte) 125;
      numArray2[37] = (byte) 132;
      numArray2[12] = (byte) 174;
      numArray2[25] = (byte) 208 /*0xD0*/;
      numArray2[26] = (byte) 74;
      numArray2[27] = (byte) 190;
      numArray2[24] = (byte) 61;
      numArray2[29] = (byte) 80 /*0x50*/;
      numArray2[8] = (byte) 243;
      numArray2[33] = (byte) 169;
      numArray2[32 /*0x20*/] = (byte) 150;
      numArray2[48 /*0x30*/] = (byte) 221;
      numArray2[34] = (byte) 147;
      numArray2[38] = (byte) 65;
      numArray2[43] = (byte) 23;
      numArray2[18] = (byte) 16 /*0x10*/;
      numArray2[40] = (byte) 14;
      numArray2[39] = (byte) 144 /*0x90*/;
      numArray2[35] = (byte) 84;
      numArray2[16 /*0x10*/] = (byte) 189;
      numArray2[1] = (byte) 175;
      numArray2[20] = (byte) 233;
      numArray2[28] = (byte) 45;
      numArray2[30] = (byte) 95;
      numArray2[46] = (byte) 149;
      numArray2[22] = (byte) 178;
      numArray2[47] = (byte) 24;
      numArray2[49] = (byte) 247;
      numArray2[50] = (byte) 236;
      numArray2[51] = (byte) 223;
      numArray2[52] = (byte) 79;
      numArray2[53] = (byte) 173;
      numArray2[13] = (byte) 174;
      byte[] numArray3 = new byte[55]
      {
        (byte) 26,
        byte.MaxValue,
        (byte) 175,
        (byte) 201,
        (byte) 79,
        (byte) 133,
        (byte) 134,
        (byte) 26,
        (byte) 30,
        (byte) 208 /*0xD0*/,
        (byte) 26,
        (byte) 200,
        (byte) 32 /*0x20*/,
        (byte) 252,
        (byte) 192 /*0xC0*/,
        (byte) 189,
        (byte) 225,
        (byte) 156,
        (byte) 209,
        (byte) 106,
        (byte) 103,
        (byte) 18,
        (byte) 142,
        (byte) 129,
        (byte) 83,
        (byte) 55,
        (byte) 202,
        (byte) 21,
        (byte) 221,
        (byte) 149,
        (byte) 5,
        (byte) 60,
        (byte) 93,
        (byte) 166,
        (byte) 74,
        (byte) 121,
        (byte) 29,
        (byte) 12,
        (byte) 243,
        (byte) 207,
        (byte) 246,
        (byte) 49,
        (byte) 20,
        (byte) 137,
        (byte) 243,
        (byte) 221,
        (byte) 124,
        (byte) 9,
        (byte) 243,
        (byte) 186,
        (byte) 109,
        (byte) 126,
        (byte) 109,
        (byte) 130,
        (byte) 124
      };
      key.Query(true, 334, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22]
      {
        (byte) 191,
        (byte) 179,
        (byte) 81,
        (byte) 129,
        (byte) 55,
        (byte) 98,
        (byte) 64 /*0x40*/,
        (byte) 197,
        (byte) 224 /*0xE0*/,
        (byte) 249,
        (byte) 93,
        (byte) 244,
        (byte) 210,
        (byte) 27,
        (byte) 192 /*0xC0*/,
        (byte) 129,
        (byte) 162,
        (byte) 70,
        (byte) 93,
        (byte) 214,
        (byte) 200,
        (byte) 4
      };
      byte[] numArray5 = new byte[22]
      {
        (byte) 174,
        (byte) 142,
        (byte) 137,
        (byte) 92,
        (byte) 180,
        (byte) 209,
        (byte) 246,
        (byte) 5,
        (byte) 2,
        (byte) 25,
        (byte) 77,
        (byte) 180,
        (byte) 76,
        (byte) 254,
        (byte) 165,
        (byte) 123,
        (byte) 171,
        (byte) 136,
        (byte) 66,
        (byte) 249,
        (byte) 110,
        (byte) 160 /*0xA0*/
      };
      key.Query(true, 334, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 22);
      for (int index = 0; index < 22; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[11];
      byte[] response = new byte[11];
      Array.Copy((Array) sc_361.sspq, 69, (Array) numArray6, 0, 11);
      key.Query(true, 334, numArray6, response);
      Array.Copy((Array) sc_361.sspr, 69, (Array) numArray6, 0, 11);
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
    byte[] numArray7 = new byte[77];
    byte[] numArray8 = new byte[55]
    {
      (byte) 91,
      (byte) 189,
      (byte) 23,
      (byte) 234,
      (byte) 11,
      (byte) 177,
      (byte) 8,
      (byte) 51,
      (byte) 9,
      (byte) 225,
      (byte) 156,
      (byte) 242,
      (byte) 39,
      (byte) 45,
      (byte) 42,
      (byte) 204,
      (byte) 150,
      (byte) 153,
      (byte) 186,
      (byte) 94,
      (byte) 166,
      (byte) 77,
      (byte) 9,
      (byte) 228,
      (byte) 143,
      (byte) 238,
      (byte) 131,
      (byte) 129,
      (byte) 222,
      (byte) 59,
      (byte) 220,
      (byte) 181,
      (byte) 250,
      (byte) 29,
      (byte) 71,
      (byte) 137,
      (byte) 70,
      (byte) 236,
      (byte) 188,
      (byte) 82,
      (byte) 76,
      (byte) 119,
      (byte) 42,
      (byte) 76,
      (byte) 224 /*0xE0*/,
      (byte) 190,
      (byte) 176 /*0xB0*/,
      (byte) 48 /*0x30*/,
      (byte) 78,
      (byte) 177,
      (byte) 34,
      (byte) 91,
      (byte) 171,
      (byte) 103,
      (byte) 188
    };
    byte[] numArray9 = new byte[55]
    {
      (byte) 131,
      (byte) 124,
      (byte) 245,
      (byte) 14,
      (byte) 42,
      (byte) 194,
      (byte) 242,
      (byte) 120,
      (byte) 82,
      (byte) 217,
      (byte) 119,
      (byte) 251,
      (byte) 152,
      (byte) 162,
      (byte) 58,
      (byte) 120,
      (byte) 154,
      (byte) 117,
      (byte) 84,
      (byte) 44,
      (byte) 195,
      (byte) 79,
      (byte) 175,
      (byte) 56,
      (byte) 148,
      (byte) 211,
      (byte) 159,
      (byte) 216,
      (byte) 75,
      (byte) 150,
      (byte) 150,
      (byte) 253,
      (byte) 77,
      (byte) 118,
      (byte) 49,
      (byte) 75,
      (byte) 196,
      (byte) 26,
      (byte) 242,
      (byte) 161,
      (byte) 125,
      (byte) 236,
      (byte) 253,
      (byte) 190,
      (byte) 201,
      (byte) 150,
      (byte) 166,
      (byte) 151,
      (byte) 253,
      (byte) 19,
      (byte) 99,
      (byte) 27,
      (byte) 39,
      (byte) 140,
      (byte) 246
    };
    key.Query(true, 334, numArray8, numArray8);
    Array.Copy((Array) numArray8, 0, (Array) numArray7, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray7[index] ^= numArray9[index];
    byte[] numArray10 = new byte[22]
    {
      (byte) 50,
      (byte) 90,
      (byte) 29,
      (byte) 46,
      (byte) 135,
      (byte) 84,
      (byte) 206,
      (byte) 4,
      (byte) 231,
      (byte) 248,
      (byte) 9,
      (byte) 229,
      (byte) 161,
      (byte) 134,
      (byte) 170,
      (byte) 220,
      (byte) 103,
      (byte) 17,
      (byte) 47,
      (byte) 35,
      (byte) 188,
      (byte) 3
    };
    byte[] numArray11 = new byte[22]
    {
      (byte) 226,
      (byte) 139,
      (byte) 157,
      (byte) 221,
      (byte) 169,
      (byte) 62,
      (byte) 76,
      (byte) 20,
      (byte) 140,
      (byte) 29,
      (byte) 216,
      (byte) 97,
      (byte) 89,
      (byte) 59,
      (byte) 145,
      (byte) 15,
      (byte) 16 /*0x10*/,
      (byte) 98,
      (byte) 38,
      (byte) 145,
      (byte) 152,
      (byte) 46
    };
    key.Query(true, 334, numArray10, numArray10);
    Array.Copy((Array) numArray10, 0, (Array) numArray7, 55, 22);
    for (int index = 0; index < 22; ++index)
      numArray7[index + 55] ^= numArray11[index];
    return Encoding.UTF8.GetString(numArray7);
  }
}
