// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_874
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_874
{
  private static byte[] sspq = new byte[41]
  {
    (byte) 200,
    (byte) 109,
    (byte) 245,
    (byte) 9,
    (byte) 63 /*0x3F*/,
    (byte) 232,
    (byte) 202,
    (byte) 64 /*0x40*/,
    (byte) 240 /*0xF0*/,
    (byte) 27,
    (byte) 46,
    (byte) 67,
    (byte) 158,
    (byte) 210,
    (byte) 30,
    (byte) 86,
    (byte) 106,
    (byte) 56,
    (byte) 250,
    (byte) 149,
    (byte) 214,
    (byte) 188,
    (byte) 119,
    (byte) 91,
    (byte) 134,
    (byte) 208 /*0xD0*/,
    (byte) 98,
    (byte) 188,
    (byte) 22,
    (byte) 158,
    (byte) 154,
    (byte) 36,
    (byte) 180,
    (byte) 117,
    (byte) 174,
    (byte) 106,
    (byte) 183,
    (byte) 201,
    byte.MaxValue,
    (byte) 62,
    (byte) 240 /*0xF0*/
  };
  private static byte[] sspr = new byte[41]
  {
    (byte) 211,
    (byte) 44,
    (byte) 91,
    (byte) 117,
    (byte) 20,
    (byte) 164,
    (byte) 140,
    (byte) 62,
    (byte) 229,
    (byte) 4,
    (byte) 136,
    (byte) 183,
    (byte) 46,
    (byte) 112 /*0x70*/,
    (byte) 92,
    (byte) 89,
    (byte) 191,
    (byte) 192 /*0xC0*/,
    (byte) 217,
    (byte) 171,
    (byte) 244,
    (byte) 227,
    (byte) 207,
    (byte) 205,
    (byte) 89,
    (byte) 46,
    (byte) 2,
    (byte) 11,
    (byte) 38,
    (byte) 125,
    (byte) 253,
    (byte) 183,
    (byte) 56,
    (byte) 100,
    (byte) 177,
    (byte) 185,
    (byte) 218,
    (byte) 20,
    (byte) 186,
    (byte) 32 /*0x20*/,
    (byte) 187
  };

  internal static string ssp_avs_875()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[164];
      byte[] numArray2 = new byte[55]
      {
        (byte) 167,
        (byte) 194,
        (byte) 46,
        (byte) 202,
        (byte) 87,
        (byte) 88,
        (byte) 121,
        (byte) 72,
        (byte) 218,
        (byte) 232,
        (byte) 195,
        (byte) 239,
        (byte) 67,
        (byte) 6,
        (byte) 208 /*0xD0*/,
        (byte) 155,
        (byte) 238,
        (byte) 108,
        (byte) 204,
        (byte) 79,
        (byte) 47,
        (byte) 91,
        (byte) 237,
        (byte) 32 /*0x20*/,
        (byte) 226,
        (byte) 198,
        (byte) 6,
        (byte) 196,
        (byte) 68,
        (byte) 157,
        (byte) 116,
        (byte) 158,
        (byte) 149,
        (byte) 154,
        (byte) 174,
        (byte) 188,
        (byte) 12,
        (byte) 102,
        (byte) 137,
        (byte) 56,
        (byte) 196,
        (byte) 98,
        (byte) 103,
        (byte) 132,
        (byte) 233,
        (byte) 70,
        (byte) 25,
        (byte) 224 /*0xE0*/,
        (byte) 92,
        (byte) 159,
        (byte) 189,
        (byte) 109,
        (byte) 63 /*0x3F*/,
        (byte) 189,
        (byte) 160 /*0xA0*/
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 247,
        (byte) 101,
        (byte) 218,
        (byte) 61,
        (byte) 90,
        (byte) 181,
        (byte) 17,
        (byte) 133,
        (byte) 71,
        (byte) 226,
        (byte) 224 /*0xE0*/,
        (byte) 163,
        (byte) 133,
        (byte) 211,
        (byte) 20,
        (byte) 202,
        (byte) 78,
        (byte) 2,
        (byte) 84,
        (byte) 11,
        (byte) 52,
        (byte) 216,
        (byte) 235,
        (byte) 172,
        (byte) 118,
        (byte) 175,
        (byte) 6,
        (byte) 57,
        (byte) 2,
        (byte) 212,
        (byte) 149,
        (byte) 101,
        (byte) 212,
        (byte) 63 /*0x3F*/,
        (byte) 49,
        (byte) 196,
        (byte) 165,
        (byte) 91,
        (byte) 179,
        (byte) 188,
        (byte) 43,
        (byte) 71,
        (byte) 199,
        (byte) 129,
        (byte) 165,
        (byte) 21,
        (byte) 150,
        (byte) 156,
        (byte) 144 /*0x90*/,
        (byte) 115,
        (byte) 6,
        (byte) 94,
        (byte) 55,
        (byte) 129,
        (byte) 136
      };
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55];
      numArray4[44] = (byte) 111;
      numArray4[28] = (byte) 6;
      numArray4[23] = (byte) 172;
      numArray4[17] = (byte) 138;
      numArray4[26] = (byte) 164;
      numArray4[45] = (byte) 136;
      numArray4[6] = (byte) 60;
      numArray4[7] = (byte) 115;
      numArray4[8] = (byte) 36;
      numArray4[9] = (byte) 90;
      numArray4[13] = (byte) 143;
      numArray4[1] = (byte) 231;
      numArray4[5] = (byte) 4;
      numArray4[36] = (byte) 248;
      numArray4[38] = (byte) 15;
      numArray4[15] = (byte) 227;
      numArray4[16 /*0x10*/] = (byte) 147;
      numArray4[4] = (byte) 180;
      numArray4[18] = (byte) 205;
      numArray4[19] = (byte) 244;
      numArray4[20] = (byte) 105;
      numArray4[21] = (byte) 54;
      numArray4[22] = (byte) 155;
      numArray4[24] = (byte) 38;
      numArray4[40] = (byte) 42;
      numArray4[37] = (byte) 231;
      numArray4[48 /*0x30*/] = (byte) 54;
      numArray4[27] = (byte) 224 /*0xE0*/;
      numArray4[25] = (byte) 103;
      numArray4[41] = (byte) 16 /*0x10*/;
      numArray4[2] = (byte) 145;
      numArray4[31 /*0x1F*/] = (byte) 250;
      numArray4[32 /*0x20*/] = (byte) 97;
      numArray4[33] = (byte) 231;
      numArray4[49] = (byte) 138;
      numArray4[46] = (byte) 73;
      numArray4[10] = (byte) 169;
      numArray4[42] = (byte) 196;
      numArray4[53] = (byte) 87;
      numArray4[14] = (byte) 135;
      numArray4[34] = (byte) 254;
      numArray4[35] = (byte) 127 /*0x7F*/;
      numArray4[47] = (byte) 16 /*0x10*/;
      numArray4[43] = (byte) 236;
      numArray4[30] = (byte) 215;
      numArray4[12] = (byte) 19;
      numArray4[11] = (byte) 88;
      numArray4[29] = (byte) 94;
      numArray4[39] = (byte) 94;
      numArray4[50] = (byte) 172;
      numArray4[0] = (byte) 13;
      numArray4[51] = (byte) 50;
      numArray4[52] = (byte) 211;
      numArray4[3] = (byte) 81;
      numArray4[54] = (byte) 108;
      byte[] numArray5 = new byte[55];
      numArray5[19] = (byte) 41;
      numArray5[26] = (byte) 74;
      numArray5[52] = (byte) 214;
      numArray5[6] = (byte) 203;
      numArray5[4] = (byte) 140;
      numArray5[5] = (byte) 185;
      numArray5[42] = (byte) 176 /*0xB0*/;
      numArray5[7] = (byte) 225;
      numArray5[34] = (byte) 132;
      numArray5[9] = (byte) 75;
      numArray5[44] = (byte) 134;
      numArray5[39] = (byte) 4;
      numArray5[12] = (byte) 63 /*0x3F*/;
      numArray5[0] = (byte) 44;
      numArray5[13] = (byte) 183;
      numArray5[14] = (byte) 203;
      numArray5[15] = (byte) 102;
      numArray5[33] = (byte) 176 /*0xB0*/;
      numArray5[21] = (byte) 247;
      numArray5[41] = (byte) 180;
      numArray5[20] = (byte) 233;
      numArray5[54] = (byte) 125;
      numArray5[25] = (byte) 131;
      numArray5[40] = (byte) 108;
      numArray5[24] = (byte) 170;
      numArray5[27] = (byte) 231;
      numArray5[16 /*0x10*/] = (byte) 153;
      numArray5[29] = (byte) 109;
      numArray5[35] = (byte) 254;
      numArray5[3] = (byte) 151;
      numArray5[30] = (byte) 195;
      numArray5[31 /*0x1F*/] = (byte) 194;
      numArray5[10] = (byte) 23;
      numArray5[28] = (byte) 106;
      numArray5[8] = (byte) 156;
      numArray5[11] = (byte) 115;
      numArray5[36] = (byte) 171;
      numArray5[37] = (byte) 30;
      numArray5[22] = (byte) 203;
      numArray5[2] = (byte) 154;
      numArray5[17] = (byte) 217;
      numArray5[18] = (byte) 245;
      numArray5[23] = (byte) 167;
      numArray5[38] = (byte) 254;
      numArray5[43] = (byte) 253;
      numArray5[45] = (byte) 66;
      numArray5[46] = (byte) 244;
      numArray5[47] = (byte) 125;
      numArray5[48 /*0x30*/] = (byte) 85;
      numArray5[49] = (byte) 87;
      numArray5[50] = (byte) 145;
      numArray5[51] = (byte) 21;
      numArray5[32 /*0x20*/] = (byte) 125;
      numArray5[53] = (byte) 26;
      numArray5[1] = (byte) 127 /*0x7F*/;
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[54]
      {
        (byte) 42,
        (byte) 128 /*0x80*/,
        (byte) 46,
        (byte) 183,
        (byte) 55,
        (byte) 20,
        (byte) 99,
        (byte) 240 /*0xF0*/,
        (byte) 198,
        (byte) 112 /*0x70*/,
        (byte) 74,
        (byte) 165,
        (byte) 59,
        (byte) 105,
        (byte) 190,
        (byte) 37,
        (byte) 60,
        (byte) 222,
        (byte) 1,
        (byte) 137,
        (byte) 185,
        (byte) 69,
        (byte) 102,
        (byte) 149,
        (byte) 251,
        (byte) 31 /*0x1F*/,
        (byte) 19,
        (byte) 158,
        (byte) 237,
        (byte) 206,
        (byte) 232,
        (byte) 7,
        (byte) 209,
        (byte) 65,
        (byte) 66,
        (byte) 175,
        (byte) 100,
        (byte) 123,
        (byte) 228,
        (byte) 158,
        (byte) 134,
        (byte) 155,
        (byte) 113,
        (byte) 53,
        (byte) 32 /*0x20*/,
        (byte) 115,
        (byte) 31 /*0x1F*/,
        (byte) 95,
        (byte) 84,
        (byte) 218,
        (byte) 183,
        (byte) 180,
        (byte) 93,
        (byte) 104
      };
      byte[] numArray7 = new byte[54]
      {
        (byte) 124,
        (byte) 80 /*0x50*/,
        (byte) 117,
        (byte) 184,
        (byte) 175,
        (byte) 46,
        (byte) 172,
        (byte) 204,
        (byte) 117,
        (byte) 45,
        (byte) 194,
        (byte) 226,
        (byte) 23,
        (byte) 44,
        (byte) 87,
        (byte) 221,
        (byte) 121,
        (byte) 108,
        (byte) 46,
        (byte) 118,
        (byte) 38,
        (byte) 251,
        (byte) 194,
        (byte) 251,
        (byte) 122,
        (byte) 171,
        (byte) 161,
        (byte) 155,
        (byte) 97,
        (byte) 108,
        (byte) 137,
        (byte) 93,
        (byte) 89,
        (byte) 190,
        (byte) 38,
        (byte) 43,
        (byte) 127 /*0x7F*/,
        (byte) 48 /*0x30*/,
        (byte) 11,
        (byte) 134,
        (byte) 132,
        (byte) 98,
        (byte) 115,
        (byte) 124,
        (byte) 225,
        (byte) 66,
        (byte) 116,
        (byte) 130,
        (byte) 205,
        (byte) 192 /*0xC0*/,
        (byte) 175,
        (byte) 119,
        (byte) 58,
        (byte) 114
      };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 54);
      for (int index = 0; index < 54; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[164];
    byte[] numArray9 = new byte[55]
    {
      (byte) 152,
      (byte) 188,
      (byte) 24,
      (byte) 1,
      (byte) 197,
      (byte) 2,
      (byte) 247,
      (byte) 136,
      (byte) 67,
      (byte) 211,
      (byte) 249,
      (byte) 39,
      (byte) 14,
      (byte) 192 /*0xC0*/,
      (byte) 193,
      (byte) 250,
      (byte) 98,
      (byte) 2,
      (byte) 30,
      (byte) 212,
      (byte) 49,
      (byte) 116,
      (byte) 136,
      byte.MaxValue,
      (byte) 179,
      (byte) 74,
      (byte) 19,
      (byte) 22,
      (byte) 52,
      (byte) 169,
      (byte) 104,
      (byte) 165,
      (byte) 93,
      (byte) 149,
      (byte) 204,
      (byte) 190,
      (byte) 204,
      (byte) 157,
      (byte) 193,
      (byte) 152,
      (byte) 121,
      (byte) 173,
      (byte) 221,
      (byte) 159,
      (byte) 37,
      (byte) 56,
      (byte) 70,
      (byte) 26,
      (byte) 246,
      (byte) 185,
      (byte) 251,
      (byte) 102,
      (byte) 126,
      (byte) 17,
      (byte) 232
    };
    byte[] numArray10 = new byte[55];
    numArray10[36] = (byte) 16 /*0x10*/;
    numArray10[37] = (byte) 215;
    numArray10[2] = (byte) 184;
    numArray10[3] = (byte) 230;
    numArray10[40] = (byte) 161;
    numArray10[39] = (byte) 242;
    numArray10[6] = (byte) 104;
    numArray10[34] = (byte) 185;
    numArray10[10] = (byte) 124;
    numArray10[51] = (byte) 123;
    numArray10[12] = (byte) 163;
    numArray10[9] = (byte) 171;
    numArray10[26] = (byte) 208 /*0xD0*/;
    numArray10[35] = (byte) 37;
    numArray10[24] = (byte) 144 /*0x90*/;
    numArray10[15] = (byte) 151;
    numArray10[18] = (byte) 0;
    numArray10[0] = (byte) 87;
    numArray10[25] = (byte) 64 /*0x40*/;
    numArray10[19] = (byte) 58;
    numArray10[20] = (byte) 111;
    numArray10[21] = (byte) 243;
    numArray10[22] = (byte) 69;
    numArray10[23] = (byte) 236;
    numArray10[46] = (byte) 8;
    numArray10[32 /*0x20*/] = (byte) 113;
    numArray10[14] = (byte) 164;
    numArray10[42] = (byte) 59;
    numArray10[28] = (byte) 162;
    numArray10[29] = (byte) 9;
    numArray10[30] = (byte) 148;
    numArray10[53] = (byte) 32 /*0x20*/;
    numArray10[13] = (byte) 83;
    numArray10[16 /*0x10*/] = (byte) 159;
    numArray10[11] = (byte) 189;
    numArray10[45] = (byte) 217;
    numArray10[33] = (byte) 7;
    numArray10[1] = (byte) 162;
    numArray10[27] = (byte) 49;
    numArray10[38] = (byte) 136;
    numArray10[7] = (byte) 243;
    numArray10[41] = (byte) 5;
    numArray10[31 /*0x1F*/] = (byte) 250;
    numArray10[43] = (byte) 154;
    numArray10[5] = (byte) 25;
    numArray10[50] = (byte) 229;
    numArray10[4] = (byte) 112 /*0x70*/;
    numArray10[47] = (byte) 15;
    numArray10[48 /*0x30*/] = (byte) 208 /*0xD0*/;
    numArray10[49] = (byte) 106;
    numArray10[17] = (byte) 70;
    numArray10[8] = (byte) 178;
    numArray10[52] = (byte) 40;
    numArray10[44] = (byte) 190;
    numArray10[54] = (byte) 89;
    key.Query(true, 339, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 62,
      (byte) 211,
      (byte) 119,
      (byte) 246,
      (byte) 193,
      (byte) 217,
      (byte) 235,
      (byte) 104,
      (byte) 44,
      (byte) 125,
      (byte) 203,
      (byte) 75,
      (byte) 120,
      (byte) 122,
      (byte) 116,
      (byte) 250,
      (byte) 220,
      (byte) 194,
      (byte) 43,
      (byte) 83,
      (byte) 180,
      (byte) 8,
      (byte) 11,
      (byte) 54,
      (byte) 210,
      (byte) 44,
      (byte) 238,
      (byte) 119,
      (byte) 220,
      (byte) 47,
      (byte) 149,
      (byte) 72,
      (byte) 27,
      (byte) 62,
      (byte) 117,
      (byte) 125,
      (byte) 207,
      (byte) 178,
      (byte) 214,
      (byte) 127 /*0x7F*/,
      (byte) 88,
      (byte) 203,
      (byte) 165,
      (byte) 241,
      (byte) 2,
      (byte) 218,
      (byte) 123,
      (byte) 17,
      (byte) 194,
      (byte) 43,
      (byte) 27,
      (byte) 243,
      (byte) 57,
      (byte) 57,
      (byte) 150
    };
    byte[] numArray12 = new byte[55]
    {
      (byte) 105,
      (byte) 164,
      (byte) 150,
      (byte) 186,
      (byte) 24,
      (byte) 64 /*0x40*/,
      (byte) 29,
      (byte) 100,
      (byte) 253,
      (byte) 197,
      (byte) 206,
      (byte) 31 /*0x1F*/,
      (byte) 109,
      (byte) 189,
      (byte) 244,
      (byte) 87,
      (byte) 242,
      (byte) 194,
      (byte) 10,
      (byte) 179,
      (byte) 218,
      (byte) 2,
      (byte) 245,
      (byte) 20,
      (byte) 252,
      (byte) 10,
      (byte) 191,
      (byte) 128 /*0x80*/,
      (byte) 184,
      (byte) 200,
      (byte) 46,
      (byte) 35,
      (byte) 163,
      (byte) 118,
      (byte) 134,
      (byte) 58,
      (byte) 198,
      (byte) 31 /*0x1F*/,
      (byte) 15,
      (byte) 203,
      (byte) 14,
      (byte) 38,
      (byte) 190,
      (byte) 174,
      (byte) 141,
      (byte) 200,
      (byte) 154,
      (byte) 2,
      (byte) 46,
      (byte) 181,
      (byte) 39,
      (byte) 122,
      (byte) 186,
      (byte) 89,
      (byte) 158
    };
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[54]
    {
      (byte) 64 /*0x40*/,
      (byte) 35,
      (byte) 149,
      (byte) 235,
      (byte) 115,
      (byte) 140,
      (byte) 253,
      (byte) 231,
      (byte) 130,
      (byte) 48 /*0x30*/,
      (byte) 58,
      (byte) 4,
      (byte) 246,
      (byte) 43,
      (byte) 42,
      (byte) 236,
      (byte) 171,
      (byte) 251,
      (byte) 233,
      (byte) 99,
      (byte) 25,
      (byte) 51,
      (byte) 248,
      (byte) 177,
      (byte) 220,
      (byte) 181,
      (byte) 177,
      (byte) 19,
      (byte) 162,
      (byte) 15,
      (byte) 226,
      (byte) 167,
      (byte) 62,
      (byte) 181,
      (byte) 142,
      (byte) 142,
      (byte) 65,
      (byte) 148,
      (byte) 229,
      (byte) 63 /*0x3F*/,
      (byte) 167,
      (byte) 9,
      (byte) 128 /*0x80*/,
      (byte) 169,
      (byte) 64 /*0x40*/,
      (byte) 12,
      (byte) 203,
      (byte) 0,
      (byte) 241,
      (byte) 7,
      (byte) 141,
      (byte) 61,
      (byte) 173,
      (byte) 211
    };
    byte[] numArray14 = new byte[54]
    {
      (byte) 161,
      (byte) 206,
      (byte) 41,
      (byte) 206,
      (byte) 109,
      (byte) 211,
      (byte) 196,
      (byte) 52,
      (byte) 195,
      (byte) 128 /*0x80*/,
      (byte) 63 /*0x3F*/,
      (byte) 80 /*0x50*/,
      (byte) 102,
      (byte) 25,
      (byte) 132,
      (byte) 19,
      (byte) 71,
      (byte) 64 /*0x40*/,
      (byte) 160 /*0xA0*/,
      (byte) 19,
      (byte) 205,
      (byte) 176 /*0xB0*/,
      (byte) 82,
      (byte) 79,
      (byte) 124,
      (byte) 149,
      (byte) 38,
      (byte) 152,
      (byte) 171,
      (byte) 138,
      (byte) 116,
      (byte) 0,
      (byte) 181,
      (byte) 157,
      (byte) 13,
      (byte) 219,
      (byte) 202,
      (byte) 117,
      (byte) 246,
      (byte) 110,
      (byte) 214,
      (byte) 120,
      byte.MaxValue,
      (byte) 106,
      (byte) 161,
      (byte) 3,
      (byte) 105,
      (byte) 174,
      (byte) 100,
      (byte) 103,
      (byte) 192 /*0xC0*/,
      (byte) 104,
      (byte) 23,
      (byte) 170
    };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 54);
    for (int index = 0; index < 54; ++index)
      numArray8[index + 110] ^= numArray14[index];
    return Encoding.UTF8.GetString(numArray8);
  }

  internal static string ssp_avs_876()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[114];
      byte[] numArray2 = new byte[55]
      {
        (byte) 14,
        (byte) 189,
        (byte) 219,
        (byte) 234,
        (byte) 69,
        (byte) 115,
        (byte) 78,
        (byte) 147,
        (byte) 198,
        (byte) 65,
        (byte) 157,
        (byte) 155,
        (byte) 171,
        (byte) 23,
        (byte) 22,
        (byte) 170,
        (byte) 187,
        (byte) 15,
        (byte) 179,
        (byte) 78,
        (byte) 112 /*0x70*/,
        (byte) 207,
        (byte) 234,
        (byte) 177,
        (byte) 33,
        (byte) 155,
        (byte) 129,
        (byte) 164,
        (byte) 249,
        (byte) 107,
        (byte) 8,
        (byte) 128 /*0x80*/,
        (byte) 99,
        (byte) 92,
        (byte) 133,
        (byte) 39,
        (byte) 100,
        (byte) 188,
        (byte) 196,
        (byte) 13,
        (byte) 91,
        (byte) 173,
        (byte) 70,
        (byte) 138,
        (byte) 98,
        (byte) 86,
        (byte) 91,
        (byte) 118,
        (byte) 39,
        (byte) 3,
        (byte) 23,
        (byte) 157,
        (byte) 89,
        (byte) 220,
        (byte) 115
      };
      byte[] numArray3 = new byte[55];
      numArray3[51] = (byte) 39;
      numArray3[42] = (byte) 166;
      numArray3[4] = (byte) 113;
      numArray3[0] = (byte) 252;
      numArray3[16 /*0x10*/] = (byte) 233;
      numArray3[38] = (byte) 20;
      numArray3[6] = (byte) 125;
      numArray3[7] = (byte) 214;
      numArray3[35] = (byte) 248;
      numArray3[9] = (byte) 66;
      numArray3[10] = (byte) 174;
      numArray3[1] = (byte) 84;
      numArray3[34] = (byte) 248;
      numArray3[13] = (byte) 20;
      numArray3[39] = (byte) 138;
      numArray3[15] = (byte) 24;
      numArray3[44] = (byte) 32 /*0x20*/;
      numArray3[17] = (byte) 186;
      numArray3[45] = (byte) 102;
      numArray3[5] = (byte) 227;
      numArray3[2] = (byte) 44;
      numArray3[21] = (byte) 83;
      numArray3[3] = (byte) 78;
      numArray3[23] = (byte) 250;
      numArray3[24] = (byte) 68;
      numArray3[12] = (byte) 217;
      numArray3[26] = (byte) 26;
      numArray3[27] = (byte) 212;
      numArray3[28] = (byte) 3;
      numArray3[29] = (byte) 55;
      numArray3[40] = (byte) 79;
      numArray3[31 /*0x1F*/] = (byte) 176 /*0xB0*/;
      numArray3[32 /*0x20*/] = (byte) 190;
      numArray3[33] = (byte) 114;
      numArray3[50] = (byte) 245;
      numArray3[30] = (byte) 88;
      numArray3[19] = (byte) 95;
      numArray3[37] = (byte) 2;
      numArray3[49] = (byte) 54;
      numArray3[8] = (byte) 35;
      numArray3[14] = (byte) 46;
      numArray3[41] = (byte) 165;
      numArray3[11] = (byte) 182;
      numArray3[43] = (byte) 42;
      numArray3[18] = (byte) 241;
      numArray3[36] = (byte) 23;
      numArray3[46] = (byte) 152;
      numArray3[47] = (byte) 159;
      numArray3[48 /*0x30*/] = (byte) 90;
      numArray3[54] = (byte) 82;
      numArray3[25] = (byte) 77;
      numArray3[22] = (byte) 52;
      numArray3[52] = (byte) 189;
      numArray3[53] = (byte) 150;
      numArray3[20] = (byte) 80 /*0x50*/;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 219,
        (byte) 254,
        (byte) 106,
        (byte) 231,
        (byte) 45,
        (byte) 162,
        (byte) 7,
        (byte) 205,
        (byte) 245,
        (byte) 177,
        (byte) 100,
        (byte) 75,
        (byte) 155,
        (byte) 12,
        (byte) 11,
        (byte) 49,
        (byte) 138,
        (byte) 64 /*0x40*/,
        (byte) 175,
        (byte) 157,
        (byte) 14,
        (byte) 7,
        (byte) 172,
        (byte) 50,
        (byte) 37,
        (byte) 67,
        (byte) 57,
        (byte) 58,
        (byte) 24,
        (byte) 115,
        (byte) 156,
        (byte) 57,
        (byte) 144 /*0x90*/,
        (byte) 223,
        (byte) 93,
        (byte) 245,
        (byte) 175,
        (byte) 52,
        (byte) 133,
        (byte) 113,
        (byte) 228,
        (byte) 131,
        (byte) 81,
        (byte) 108,
        (byte) 168,
        (byte) 153,
        (byte) 157,
        (byte) 176 /*0xB0*/,
        (byte) 13,
        (byte) 33,
        (byte) 40,
        (byte) 169,
        (byte) 0,
        (byte) 24,
        (byte) 101
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 148,
        (byte) 137,
        (byte) 32 /*0x20*/,
        (byte) 33,
        (byte) 172,
        (byte) 154,
        (byte) 75,
        (byte) 240 /*0xF0*/,
        (byte) 140,
        (byte) 201,
        (byte) 171,
        (byte) 106,
        (byte) 129,
        (byte) 0,
        (byte) 217,
        (byte) 187,
        (byte) 172,
        (byte) 134,
        (byte) 126,
        (byte) 236,
        (byte) 176 /*0xB0*/,
        (byte) 35,
        (byte) 229,
        (byte) 34,
        (byte) 111,
        (byte) 48 /*0x30*/,
        (byte) 59,
        (byte) 233,
        (byte) 62,
        (byte) 252,
        (byte) 240 /*0xF0*/,
        (byte) 94,
        (byte) 229,
        (byte) 10,
        (byte) 201,
        (byte) 117,
        (byte) 27,
        (byte) 174,
        (byte) 30,
        (byte) 227,
        (byte) 103,
        (byte) 140,
        (byte) 159,
        (byte) 249,
        (byte) 20,
        (byte) 92,
        (byte) 133,
        (byte) 160 /*0xA0*/,
        (byte) 151,
        (byte) 15,
        (byte) 223,
        (byte) 38,
        (byte) 160 /*0xA0*/,
        (byte) 176 /*0xB0*/,
        (byte) 21
      };
      key.Query(true, 339, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[4]
      {
        (byte) 56,
        (byte) 193,
        (byte) 233,
        (byte) 104
      };
      byte[] numArray7 = new byte[4]
      {
        (byte) 111,
        (byte) 124,
        (byte) 149,
        (byte) 24
      };
      key.Query(true, 339, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[114];
    byte[] numArray9 = new byte[55]
    {
      (byte) 21,
      (byte) 43,
      (byte) 187,
      (byte) 187,
      (byte) 174,
      (byte) 21,
      (byte) 129,
      (byte) 86,
      (byte) 187,
      (byte) 62,
      (byte) 184,
      (byte) 138,
      (byte) 182,
      (byte) 64 /*0x40*/,
      (byte) 137,
      (byte) 139,
      (byte) 165,
      (byte) 137,
      (byte) 45,
      (byte) 247,
      (byte) 75,
      (byte) 242,
      (byte) 56,
      (byte) 119,
      (byte) 122,
      (byte) 137,
      (byte) 164,
      (byte) 118,
      (byte) 114,
      (byte) 155,
      (byte) 8,
      (byte) 24,
      (byte) 206,
      (byte) 207,
      (byte) 87,
      (byte) 125,
      (byte) 222,
      (byte) 226,
      (byte) 2,
      (byte) 248,
      (byte) 159,
      (byte) 209,
      (byte) 238,
      (byte) 193,
      (byte) 96 /*0x60*/,
      (byte) 218,
      (byte) 30,
      (byte) 17,
      (byte) 175,
      (byte) 53,
      (byte) 67,
      (byte) 223,
      (byte) 159,
      (byte) 189,
      (byte) 54
    };
    byte[] numArray10 = new byte[55];
    numArray10[13] = (byte) 70;
    numArray10[27] = (byte) 206;
    numArray10[2] = (byte) 244;
    numArray10[3] = (byte) 210;
    numArray10[1] = (byte) 94;
    numArray10[5] = (byte) 148;
    numArray10[6] = (byte) 133;
    numArray10[7] = (byte) 203;
    numArray10[28] = (byte) 112 /*0x70*/;
    numArray10[36] = (byte) 57;
    numArray10[54] = (byte) 105;
    numArray10[11] = (byte) 161;
    numArray10[12] = (byte) 245;
    numArray10[41] = (byte) 105;
    numArray10[14] = (byte) 118;
    numArray10[25] = (byte) 96 /*0x60*/;
    numArray10[42] = (byte) 18;
    numArray10[26] = (byte) 215;
    numArray10[44] = (byte) 130;
    numArray10[19] = (byte) 183;
    numArray10[9] = (byte) 233;
    numArray10[43] = (byte) 41;
    numArray10[22] = (byte) 251;
    numArray10[46] = (byte) 57;
    numArray10[33] = (byte) 58;
    numArray10[0] = (byte) 226;
    numArray10[17] = (byte) 159;
    numArray10[10] = (byte) 154;
    numArray10[16 /*0x10*/] = (byte) 10;
    numArray10[23] = (byte) 254;
    numArray10[40] = (byte) 195;
    numArray10[31 /*0x1F*/] = (byte) 86;
    numArray10[32 /*0x20*/] = (byte) 216;
    numArray10[51] = (byte) 235;
    numArray10[34] = (byte) 149;
    numArray10[35] = (byte) 182;
    numArray10[47] = (byte) 142;
    numArray10[18] = (byte) 186;
    numArray10[38] = (byte) 34;
    numArray10[39] = (byte) 172;
    numArray10[8] = (byte) 75;
    numArray10[30] = (byte) 81;
    numArray10[24] = (byte) 131;
    numArray10[15] = (byte) 55;
    numArray10[29] = (byte) 159;
    numArray10[50] = (byte) 228;
    numArray10[37] = (byte) 240 /*0xF0*/;
    numArray10[45] = (byte) 110;
    numArray10[48 /*0x30*/] = (byte) 221;
    numArray10[49] = (byte) 217;
    numArray10[20] = (byte) 180;
    numArray10[21] = (byte) 133;
    numArray10[52] = (byte) 32 /*0x20*/;
    numArray10[53] = (byte) 136;
    numArray10[4] = (byte) 93;
    key.Query(true, 339, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 44,
      (byte) 161,
      (byte) 185,
      (byte) 221,
      (byte) 67,
      (byte) 167,
      (byte) 200,
      (byte) 14,
      (byte) 40,
      (byte) 172,
      (byte) 77,
      (byte) 179,
      (byte) 130,
      (byte) 18,
      (byte) 33,
      (byte) 170,
      (byte) 173,
      (byte) 195,
      (byte) 139,
      (byte) 54,
      (byte) 235,
      (byte) 87,
      (byte) 129,
      (byte) 51,
      (byte) 107,
      (byte) 36,
      (byte) 180,
      (byte) 214,
      (byte) 208 /*0xD0*/,
      (byte) 14,
      (byte) 170,
      (byte) 219,
      (byte) 23,
      (byte) 14,
      (byte) 110,
      (byte) 0,
      (byte) 35,
      (byte) 170,
      (byte) 80 /*0x50*/,
      (byte) 70,
      (byte) 6,
      (byte) 125,
      (byte) 151,
      (byte) 114,
      (byte) 233,
      (byte) 116,
      (byte) 32 /*0x20*/,
      (byte) 118,
      (byte) 95,
      (byte) 173,
      (byte) 189,
      (byte) 162,
      (byte) 49,
      (byte) 231,
      (byte) 144 /*0x90*/
    };
    byte[] numArray12 = new byte[55];
    numArray12[0] = (byte) 77;
    numArray12[1] = (byte) 25;
    numArray12[2] = (byte) 89;
    numArray12[30] = (byte) 141;
    numArray12[15] = (byte) 125;
    numArray12[22] = (byte) 25;
    numArray12[42] = (byte) 251;
    numArray12[9] = (byte) 229;
    numArray12[7] = (byte) 42;
    numArray12[41] = (byte) 190;
    numArray12[10] = (byte) 104;
    numArray12[20] = (byte) 166;
    numArray12[12] = (byte) 192 /*0xC0*/;
    numArray12[32 /*0x20*/] = (byte) 164;
    numArray12[14] = (byte) 247;
    numArray12[36] = (byte) 117;
    numArray12[13] = (byte) 191;
    numArray12[50] = (byte) 228;
    numArray12[5] = (byte) 120;
    numArray12[19] = (byte) 12;
    numArray12[26] = (byte) 176 /*0xB0*/;
    numArray12[46] = (byte) 248;
    numArray12[29] = (byte) 78;
    numArray12[23] = (byte) 239;
    numArray12[47] = (byte) 180;
    numArray12[24] = (byte) 98;
    numArray12[16 /*0x10*/] = (byte) 46;
    numArray12[27] = (byte) 170;
    numArray12[28] = (byte) 222;
    numArray12[54] = (byte) 214;
    numArray12[18] = (byte) 253;
    numArray12[31 /*0x1F*/] = (byte) 7;
    numArray12[17] = (byte) 21;
    numArray12[33] = (byte) 189;
    numArray12[25] = (byte) 60;
    numArray12[35] = (byte) 215;
    numArray12[11] = (byte) 97;
    numArray12[37] = (byte) 4;
    numArray12[38] = (byte) 64 /*0x40*/;
    numArray12[48 /*0x30*/] = (byte) 100;
    numArray12[21] = (byte) 93;
    numArray12[3] = (byte) 51;
    numArray12[4] = (byte) 72;
    numArray12[43] = (byte) 243;
    numArray12[44] = (byte) 70;
    numArray12[34] = (byte) 39;
    numArray12[6] = (byte) 155;
    numArray12[8] = (byte) 185;
    numArray12[40] = (byte) 91;
    numArray12[49] = (byte) 96 /*0x60*/;
    numArray12[51] = (byte) 150;
    numArray12[39] = (byte) 31 /*0x1F*/;
    numArray12[52] = (byte) 156;
    numArray12[45] = (byte) 46;
    numArray12[53] = (byte) 8;
    key.Query(true, 339, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[4]
    {
      (byte) 59,
      (byte) 123,
      (byte) 27,
      (byte) 181
    };
    byte[] numArray14 = new byte[4]
    {
      (byte) 244,
      (byte) 22,
      (byte) 29,
      (byte) 244
    };
    key.Query(true, 339, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 4);
    for (int index = 0; index < 4; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_874.sspq, 0, (Array) numArray15, 0, 41);
    key.Query(true, 339, numArray15, response);
    Array.Copy((Array) sc_874.sspr, 0, (Array) numArray15, 0, 41);
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
}
