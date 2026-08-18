// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12296
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12296
{
  private static byte[] sspq = new byte[155]
  {
    (byte) 211,
    (byte) 12,
    (byte) 82,
    (byte) 243,
    (byte) 8,
    (byte) 88,
    (byte) 170,
    (byte) 81,
    (byte) 51,
    (byte) 139,
    (byte) 235,
    (byte) 86,
    (byte) 231,
    (byte) 142,
    (byte) 80 /*0x50*/,
    (byte) 163,
    (byte) 243,
    (byte) 53,
    (byte) 108,
    (byte) 192 /*0xC0*/,
    (byte) 237,
    (byte) 203,
    (byte) 149,
    (byte) 122,
    (byte) 160 /*0xA0*/,
    (byte) 125,
    (byte) 75,
    (byte) 53,
    (byte) 52,
    (byte) 69,
    (byte) 209,
    (byte) 61,
    (byte) 28,
    (byte) 60,
    (byte) 117,
    (byte) 215,
    (byte) 111,
    (byte) 131,
    (byte) 173,
    (byte) 139,
    (byte) 120,
    (byte) 234,
    (byte) 134,
    (byte) 163,
    (byte) 128 /*0x80*/,
    (byte) 38,
    (byte) 197,
    (byte) 148,
    (byte) 122,
    (byte) 195,
    (byte) 166,
    (byte) 248,
    (byte) 109,
    (byte) 68,
    (byte) 53,
    (byte) 48 /*0x30*/,
    (byte) 245,
    (byte) 253,
    (byte) 65,
    (byte) 148,
    (byte) 33,
    (byte) 184,
    (byte) 48 /*0x30*/,
    (byte) 173,
    (byte) 218,
    (byte) 183,
    (byte) 63 /*0x3F*/,
    (byte) 46,
    (byte) 59,
    (byte) 140,
    (byte) 137,
    (byte) 141,
    (byte) 224 /*0xE0*/,
    (byte) 98,
    (byte) 18,
    (byte) 127 /*0x7F*/,
    (byte) 197,
    (byte) 148,
    (byte) 223,
    (byte) 209,
    (byte) 108,
    (byte) 233,
    (byte) 50,
    (byte) 86,
    (byte) 201,
    (byte) 18,
    (byte) 37,
    (byte) 72,
    (byte) 180,
    (byte) 48 /*0x30*/,
    (byte) 237,
    (byte) 35,
    (byte) 21,
    (byte) 128 /*0x80*/,
    (byte) 9,
    (byte) 91,
    (byte) 207,
    (byte) 103,
    (byte) 149,
    (byte) 167,
    (byte) 219,
    (byte) 30,
    (byte) 229,
    (byte) 121,
    (byte) 218,
    (byte) 210,
    (byte) 215,
    (byte) 12,
    (byte) 51,
    (byte) 206,
    (byte) 119,
    (byte) 225,
    (byte) 121,
    (byte) 69,
    (byte) 18,
    (byte) 95,
    (byte) 211,
    (byte) 8,
    (byte) 134,
    (byte) 225,
    (byte) 186,
    (byte) 217,
    (byte) 50,
    (byte) 34,
    (byte) 18,
    (byte) 168,
    (byte) 234,
    (byte) 203,
    (byte) 169,
    (byte) 16 /*0x10*/,
    (byte) 64 /*0x40*/,
    (byte) 104,
    (byte) 153,
    (byte) 250,
    (byte) 184,
    (byte) 43,
    (byte) 32 /*0x20*/,
    (byte) 43,
    (byte) 124,
    byte.MaxValue,
    (byte) 254,
    (byte) 233,
    (byte) 80 /*0x50*/,
    (byte) 254,
    (byte) 158,
    (byte) 2,
    (byte) 43,
    (byte) 53,
    (byte) 209,
    (byte) 195,
    (byte) 204,
    (byte) 176 /*0xB0*/,
    (byte) 43,
    (byte) 154,
    (byte) 74
  };
  private static byte[] sspr = new byte[155]
  {
    (byte) 174,
    (byte) 197,
    (byte) 168,
    (byte) 160 /*0xA0*/,
    (byte) 69,
    (byte) 190,
    (byte) 227,
    (byte) 124,
    (byte) 158,
    (byte) 229,
    (byte) 249,
    (byte) 150,
    (byte) 8,
    (byte) 167,
    (byte) 233,
    (byte) 242,
    (byte) 161,
    (byte) 76,
    (byte) 102,
    (byte) 106,
    (byte) 242,
    (byte) 157,
    (byte) 178,
    (byte) 9,
    (byte) 140,
    (byte) 115,
    (byte) 252,
    (byte) 8,
    (byte) 68,
    (byte) 30,
    (byte) 206,
    (byte) 61,
    (byte) 46,
    (byte) 38,
    (byte) 243,
    (byte) 150,
    (byte) 107,
    (byte) 150,
    (byte) 57,
    (byte) 112 /*0x70*/,
    (byte) 146,
    (byte) 92,
    (byte) 126,
    (byte) 135,
    (byte) 159,
    (byte) 14,
    (byte) 145,
    (byte) 154,
    (byte) 237,
    (byte) 82,
    (byte) 71,
    (byte) 138,
    (byte) 119,
    (byte) 208 /*0xD0*/,
    (byte) 21,
    (byte) 157,
    (byte) 39,
    (byte) 217,
    (byte) 139,
    (byte) 218,
    (byte) 12,
    (byte) 107,
    (byte) 122,
    (byte) 120,
    (byte) 40,
    (byte) 136,
    (byte) 230,
    (byte) 138,
    (byte) 118,
    (byte) 60,
    (byte) 220,
    (byte) 226,
    (byte) 66,
    (byte) 124,
    (byte) 195,
    (byte) 40,
    (byte) 240 /*0xF0*/,
    (byte) 51,
    (byte) 107,
    (byte) 166,
    (byte) 97,
    (byte) 16 /*0x10*/,
    (byte) 183,
    (byte) 97,
    (byte) 109,
    (byte) 187,
    (byte) 205,
    (byte) 235,
    (byte) 67,
    (byte) 239,
    (byte) 120,
    (byte) 156,
    (byte) 245,
    (byte) 176 /*0xB0*/,
    (byte) 27,
    (byte) 49,
    (byte) 26,
    (byte) 108,
    (byte) 228,
    (byte) 30,
    (byte) 141,
    (byte) 20,
    (byte) 162,
    (byte) 177,
    (byte) 10,
    (byte) 72,
    (byte) 232,
    (byte) 191,
    (byte) 167,
    (byte) 73,
    (byte) 85,
    (byte) 77,
    (byte) 216,
    (byte) 162,
    (byte) 192 /*0xC0*/,
    (byte) 163,
    (byte) 174,
    (byte) 60,
    (byte) 158,
    (byte) 157,
    (byte) 52,
    (byte) 253,
    (byte) 95,
    (byte) 71,
    (byte) 14,
    (byte) 196,
    (byte) 120,
    (byte) 2,
    (byte) 87,
    (byte) 61,
    (byte) 82,
    (byte) 67,
    (byte) 94,
    (byte) 210,
    byte.MaxValue,
    (byte) 164,
    (byte) 206,
    (byte) 243,
    (byte) 71,
    (byte) 3,
    (byte) 98,
    (byte) 169,
    (byte) 1,
    (byte) 40,
    (byte) 190,
    (byte) 58,
    (byte) 122,
    (byte) 221,
    (byte) 56,
    (byte) 112 /*0x70*/,
    (byte) 112 /*0x70*/,
    (byte) 134,
    (byte) 253,
    (byte) 80 /*0x50*/,
    (byte) 242
  };

  internal static string ssp_appserver_12297()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[39];
      byte[] numArray2 = new byte[39];
      numArray2[25] = (byte) 82;
      numArray2[16 /*0x10*/] = (byte) 70;
      numArray2[37] = (byte) 153;
      numArray2[3] = (byte) 38;
      numArray2[28] = (byte) 254;
      numArray2[27] = (byte) 43;
      numArray2[1] = (byte) 236;
      numArray2[7] = (byte) 187;
      numArray2[2] = (byte) 200;
      numArray2[9] = (byte) 30;
      numArray2[10] = (byte) 51;
      numArray2[11] = (byte) 212;
      numArray2[12] = (byte) 27;
      numArray2[13] = (byte) 11;
      numArray2[4] = (byte) 78;
      numArray2[29] = (byte) 127 /*0x7F*/;
      numArray2[5] = (byte) 187;
      numArray2[14] = (byte) 176 /*0xB0*/;
      numArray2[20] = (byte) 27;
      numArray2[8] = (byte) 143;
      numArray2[32 /*0x20*/] = (byte) 250;
      numArray2[21] = (byte) 121;
      numArray2[6] = (byte) 217;
      numArray2[23] = (byte) 106;
      numArray2[24] = (byte) 106;
      numArray2[22] = (byte) 175;
      numArray2[30] = (byte) 116;
      numArray2[34] = (byte) 29;
      numArray2[17] = (byte) 63 /*0x3F*/;
      numArray2[31 /*0x1F*/] = (byte) 10;
      numArray2[15] = (byte) 68;
      numArray2[0] = (byte) 178;
      numArray2[18] = (byte) 120;
      numArray2[33] = (byte) 180;
      numArray2[19] = (byte) 131;
      numArray2[35] = (byte) 13;
      numArray2[36] = (byte) 137;
      numArray2[26] = (byte) 249;
      numArray2[38] = (byte) 8;
      byte[] numArray3 = new byte[39]
      {
        (byte) 97,
        (byte) 182,
        (byte) 20,
        (byte) 130,
        (byte) 2,
        (byte) 175,
        (byte) 180,
        (byte) 214,
        (byte) 162,
        (byte) 61,
        (byte) 131,
        (byte) 161,
        (byte) 167,
        (byte) 235,
        (byte) 150,
        (byte) 22,
        (byte) 158,
        (byte) 183,
        (byte) 119,
        (byte) 74,
        (byte) 160 /*0xA0*/,
        (byte) 223,
        (byte) 211,
        (byte) 88,
        (byte) 189,
        (byte) 53,
        (byte) 132,
        (byte) 233,
        (byte) 89,
        (byte) 226,
        (byte) 54,
        (byte) 166,
        (byte) 170,
        (byte) 242,
        (byte) 111,
        (byte) 45,
        (byte) 78,
        (byte) 25,
        (byte) 149
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 39);
      for (int index = 0; index < 39; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[39];
    byte[] numArray5 = new byte[39]
    {
      (byte) 210,
      (byte) 32 /*0x20*/,
      (byte) 33,
      (byte) 196,
      (byte) 65,
      (byte) 207,
      (byte) 113,
      (byte) 207,
      (byte) 227,
      (byte) 208 /*0xD0*/,
      (byte) 43,
      (byte) 221,
      (byte) 12,
      (byte) 113,
      (byte) 207,
      (byte) 155,
      (byte) 107,
      (byte) 183,
      (byte) 82,
      (byte) 219,
      (byte) 128 /*0x80*/,
      (byte) 57,
      (byte) 104,
      (byte) 35,
      (byte) 14,
      (byte) 251,
      (byte) 246,
      (byte) 130,
      (byte) 33,
      (byte) 60,
      (byte) 239,
      (byte) 111,
      (byte) 6,
      (byte) 147,
      (byte) 240 /*0xF0*/,
      (byte) 141,
      (byte) 155,
      (byte) 75,
      (byte) 105
    };
    byte[] numArray6 = new byte[39]
    {
      (byte) 148,
      (byte) 154,
      (byte) 150,
      (byte) 239,
      (byte) 24,
      (byte) 30,
      (byte) 133,
      (byte) 98,
      (byte) 62,
      (byte) 202,
      (byte) 17,
      (byte) 206,
      (byte) 141,
      (byte) 251,
      (byte) 115,
      (byte) 23,
      (byte) 168,
      (byte) 172,
      (byte) 240 /*0xF0*/,
      (byte) 137,
      (byte) 65,
      (byte) 6,
      (byte) 163,
      (byte) 33,
      (byte) 114,
      (byte) 185,
      (byte) 156,
      (byte) 37,
      (byte) 197,
      (byte) 4,
      (byte) 176 /*0xB0*/,
      (byte) 107,
      (byte) 31 /*0x1F*/,
      (byte) 199,
      (byte) 172,
      (byte) 41,
      (byte) 22,
      (byte) 112 /*0x70*/,
      (byte) 206
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 39);
    for (int index = 0; index < 39; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12298(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[44] = (byte) 170;
    sourceArray1[37] = (byte) 35;
    sourceArray1[40] = (byte) 171;
    sourceArray1[3] = (byte) 211;
    sourceArray1[23] = (byte) 120;
    sourceArray1[5] = (byte) 39;
    sourceArray1[6] = (byte) 145;
    sourceArray1[30] = (byte) 114;
    sourceArray1[2] = (byte) 42;
    sourceArray1[9] = (byte) 217;
    sourceArray1[10] = (byte) 232;
    sourceArray1[22] = (byte) 214;
    sourceArray1[43] = (byte) 44;
    sourceArray1[7] = (byte) 150;
    sourceArray1[34] = (byte) 148;
    sourceArray1[31 /*0x1F*/] = (byte) 232;
    sourceArray1[16 /*0x10*/] = (byte) 216;
    sourceArray1[33] = (byte) 149;
    sourceArray1[18] = (byte) 174;
    sourceArray1[29] = (byte) 249;
    sourceArray1[21] = (byte) 187;
    sourceArray1[13] = (byte) 254;
    sourceArray1[24] = (byte) 129;
    sourceArray1[19] = (byte) 167;
    sourceArray1[35] = (byte) 62;
    sourceArray1[25] = (byte) 66;
    sourceArray1[11] = (byte) 152;
    sourceArray1[27] = (byte) 32 /*0x20*/;
    sourceArray1[28] = (byte) 77;
    sourceArray1[20] = (byte) 224 /*0xE0*/;
    sourceArray1[8] = (byte) 155;
    sourceArray1[17] = (byte) 239;
    sourceArray1[41] = (byte) 89;
    sourceArray1[32 /*0x20*/] = (byte) 47;
    sourceArray1[0] = (byte) 142;
    sourceArray1[15] = (byte) 70;
    sourceArray1[36] = (byte) 208 /*0xD0*/;
    sourceArray1[12] = (byte) 184;
    sourceArray1[38] = (byte) 76;
    sourceArray1[39] = (byte) 249;
    sourceArray1[26] = (byte) 0;
    sourceArray1[4] = (byte) 53;
    sourceArray1[42] = (byte) 212;
    sourceArray1[14] = (byte) 182;
    sourceArray1[1] = (byte) 207;
    sourceArray1[45] = (byte) 87;
    sourceArray1[46] = (byte) 213;
    sourceArray1[47] = (byte) 157;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[44] = (byte) 41;
    sourceArray2[1] = (byte) 185;
    sourceArray2[2] = (byte) 149;
    sourceArray2[40] = (byte) 141;
    sourceArray2[46] = (byte) 40;
    sourceArray2[5] = (byte) 254;
    sourceArray2[6] = (byte) 203;
    sourceArray2[7] = (byte) 68;
    sourceArray2[8] = (byte) 193;
    sourceArray2[9] = (byte) 47;
    sourceArray2[3] = (byte) 6;
    sourceArray2[26] = (byte) 64 /*0x40*/;
    sourceArray2[21] = (byte) 200;
    sourceArray2[38] = (byte) 213;
    sourceArray2[43] = (byte) 250;
    sourceArray2[14] = (byte) 121;
    sourceArray2[16 /*0x10*/] = (byte) 164;
    sourceArray2[15] = (byte) 14;
    sourceArray2[18] = (byte) 5;
    sourceArray2[22] = (byte) 120;
    sourceArray2[45] = (byte) 212;
    sourceArray2[4] = (byte) 190;
    sourceArray2[30] = (byte) 104;
    sourceArray2[23] = (byte) 23;
    sourceArray2[24] = (byte) 195;
    sourceArray2[25] = (byte) 22;
    sourceArray2[42] = (byte) 169;
    sourceArray2[35] = (byte) 193;
    sourceArray2[28] = (byte) 143;
    sourceArray2[39] = (byte) 28;
    sourceArray2[19] = (byte) 87;
    sourceArray2[31 /*0x1F*/] = (byte) 234;
    sourceArray2[32 /*0x20*/] = (byte) 64 /*0x40*/;
    sourceArray2[33] = (byte) 104;
    sourceArray2[41] = (byte) 142;
    sourceArray2[36] = (byte) 52;
    sourceArray2[20] = (byte) 148;
    sourceArray2[29] = (byte) 86;
    sourceArray2[34] = (byte) 170;
    sourceArray2[0] = (byte) 186;
    sourceArray2[37] = (byte) 250;
    sourceArray2[11] = (byte) 152;
    sourceArray2[13] = (byte) 176 /*0xB0*/;
    sourceArray2[10] = (byte) 74;
    sourceArray2[17] = (byte) 222;
    sourceArray2[47] = (byte) 188;
    sourceArray2[27] = (byte) 222;
    sourceArray2[12] = (byte) 64 /*0x40*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[50];
    byte[] response2 = new byte[50];
    Array.Copy((Array) sc_12296.sspq, 0, (Array) numArray2, 0, 50);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_12296.sspr, 0, (Array) numArray2, 0, 50);
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

  internal static string ssp_appserver_12299()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[130];
      byte[] numArray2 = new byte[55]
      {
        (byte) 160 /*0xA0*/,
        (byte) 136,
        (byte) 121,
        (byte) 186,
        (byte) 8,
        (byte) 118,
        (byte) 216,
        (byte) 82,
        (byte) 138,
        (byte) 13,
        (byte) 117,
        (byte) 28,
        (byte) 238,
        (byte) 186,
        (byte) 173,
        (byte) 58,
        (byte) 170,
        (byte) 25,
        (byte) 120,
        (byte) 51,
        (byte) 111,
        (byte) 135,
        (byte) 224 /*0xE0*/,
        (byte) 72,
        (byte) 2,
        (byte) 68,
        (byte) 242,
        (byte) 250,
        (byte) 121,
        (byte) 183,
        (byte) 182,
        (byte) 186,
        (byte) 113,
        byte.MaxValue,
        (byte) 48 /*0x30*/,
        (byte) 181,
        (byte) 162,
        (byte) 129,
        (byte) 109,
        (byte) 96 /*0x60*/,
        (byte) 151,
        (byte) 49,
        (byte) 34,
        (byte) 126,
        (byte) 3,
        (byte) 205,
        (byte) 152,
        (byte) 233,
        (byte) 211,
        (byte) 236,
        (byte) 26,
        (byte) 123,
        (byte) 2,
        (byte) 144 /*0x90*/,
        (byte) 214
      };
      byte[] numArray3 = new byte[55];
      numArray3[32 /*0x20*/] = (byte) 59;
      numArray3[1] = (byte) 87;
      numArray3[5] = (byte) 151;
      numArray3[42] = (byte) 39;
      numArray3[4] = (byte) 241;
      numArray3[7] = (byte) 86;
      numArray3[6] = (byte) 173;
      numArray3[3] = (byte) 26;
      numArray3[8] = (byte) 84;
      numArray3[9] = (byte) 71;
      numArray3[13] = (byte) 246;
      numArray3[11] = (byte) 167;
      numArray3[23] = (byte) 9;
      numArray3[47] = (byte) 82;
      numArray3[14] = (byte) 22;
      numArray3[15] = (byte) 177;
      numArray3[16 /*0x10*/] = (byte) 82;
      numArray3[17] = (byte) 65;
      numArray3[2] = (byte) 71;
      numArray3[19] = (byte) 188;
      numArray3[36] = (byte) 151;
      numArray3[26] = (byte) 45;
      numArray3[18] = (byte) 116;
      numArray3[44] = (byte) 165;
      numArray3[24] = (byte) 212;
      numArray3[25] = (byte) 124;
      numArray3[41] = (byte) 231;
      numArray3[27] = (byte) 225;
      numArray3[20] = (byte) 250;
      numArray3[29] = (byte) 163;
      numArray3[0] = (byte) 225;
      numArray3[51] = (byte) 244;
      numArray3[28] = (byte) 80 /*0x50*/;
      numArray3[53] = (byte) 167;
      numArray3[34] = (byte) 222;
      numArray3[35] = (byte) 150;
      numArray3[54] = (byte) 130;
      numArray3[37] = (byte) 35;
      numArray3[38] = (byte) 118;
      numArray3[39] = (byte) 10;
      numArray3[21] = (byte) 14;
      numArray3[33] = (byte) 60;
      numArray3[30] = (byte) 208 /*0xD0*/;
      numArray3[31 /*0x1F*/] = (byte) 162;
      numArray3[12] = (byte) 174;
      numArray3[45] = (byte) 226;
      numArray3[46] = (byte) 42;
      numArray3[40] = (byte) 254;
      numArray3[48 /*0x30*/] = (byte) 46;
      numArray3[10] = (byte) 70;
      numArray3[50] = (byte) 98;
      numArray3[22] = (byte) 175;
      numArray3[52] = (byte) 107;
      numArray3[43] = (byte) 236;
      numArray3[49] = (byte) 46;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 235,
        (byte) 130,
        (byte) 22,
        (byte) 122,
        (byte) 183,
        (byte) 76,
        (byte) 53,
        (byte) 26,
        (byte) 1,
        (byte) 202,
        (byte) 197,
        (byte) 114,
        (byte) 234,
        (byte) 39,
        (byte) 95,
        (byte) 2,
        (byte) 59,
        (byte) 226,
        (byte) 230,
        (byte) 20,
        (byte) 59,
        (byte) 252,
        (byte) 19,
        (byte) 3,
        (byte) 170,
        (byte) 75,
        (byte) 56,
        (byte) 172,
        (byte) 108,
        (byte) 206,
        (byte) 236,
        (byte) 144 /*0x90*/,
        (byte) 98,
        (byte) 200,
        (byte) 133,
        (byte) 70,
        (byte) 146,
        (byte) 97,
        (byte) 2,
        (byte) 10,
        (byte) 26,
        (byte) 12,
        (byte) 71,
        (byte) 86,
        (byte) 227,
        (byte) 236,
        (byte) 239,
        (byte) 44,
        (byte) 145,
        (byte) 204,
        (byte) 175,
        (byte) 17,
        (byte) 177,
        (byte) 160 /*0xA0*/,
        (byte) 9
      };
      byte[] numArray5 = new byte[55]
      {
        (byte) 155,
        (byte) 246,
        (byte) 238,
        (byte) 42,
        (byte) 71,
        (byte) 44,
        (byte) 52,
        (byte) 195,
        (byte) 118,
        (byte) 151,
        (byte) 126,
        (byte) 215,
        (byte) 187,
        (byte) 5,
        (byte) 91,
        (byte) 41,
        (byte) 51,
        (byte) 51,
        (byte) 104,
        (byte) 138,
        (byte) 92,
        (byte) 116,
        (byte) 157,
        (byte) 119,
        (byte) 150,
        (byte) 43,
        (byte) 166,
        (byte) 184,
        (byte) 114,
        (byte) 229,
        (byte) 120,
        (byte) 58,
        (byte) 159,
        (byte) 167,
        (byte) 232,
        (byte) 174,
        (byte) 228,
        (byte) 56,
        (byte) 204,
        (byte) 235,
        (byte) 90,
        (byte) 186,
        (byte) 177,
        (byte) 188,
        (byte) 36,
        (byte) 177,
        (byte) 56,
        (byte) 214,
        (byte) 38,
        (byte) 146,
        (byte) 85,
        (byte) 108,
        (byte) 240 /*0xF0*/,
        (byte) 60,
        (byte) 65
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[20];
      numArray6[11] = (byte) 161;
      numArray6[1] = (byte) 208 /*0xD0*/;
      numArray6[13] = (byte) 111;
      numArray6[3] = (byte) 19;
      numArray6[8] = (byte) 53;
      numArray6[16 /*0x10*/] = (byte) 219;
      numArray6[6] = (byte) 51;
      numArray6[7] = (byte) 222;
      numArray6[9] = (byte) 16 /*0x10*/;
      numArray6[0] = (byte) 19;
      numArray6[2] = (byte) 107;
      numArray6[14] = (byte) 217;
      numArray6[5] = (byte) 174;
      numArray6[17] = (byte) 250;
      numArray6[12] = (byte) 24;
      numArray6[15] = (byte) 224 /*0xE0*/;
      numArray6[10] = (byte) 34;
      numArray6[4] = (byte) 137;
      numArray6[18] = (byte) 46;
      numArray6[19] = (byte) 181;
      byte[] numArray7 = new byte[20]
      {
        (byte) 4,
        (byte) 91,
        (byte) 243,
        (byte) 181,
        (byte) 220,
        (byte) 6,
        (byte) 7,
        (byte) 52,
        (byte) 120,
        (byte) 104,
        (byte) 129,
        (byte) 218,
        (byte) 123,
        (byte) 127 /*0x7F*/,
        (byte) 33,
        (byte) 213,
        (byte) 220,
        (byte) 132,
        (byte) 22,
        (byte) 149
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index + 110] ^= numArray7[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray8 = new byte[130];
    byte[] numArray9 = new byte[55]
    {
      (byte) 219,
      (byte) 205,
      (byte) 138,
      (byte) 120,
      (byte) 93,
      (byte) 213,
      (byte) 49,
      (byte) 213,
      (byte) 166,
      (byte) 219,
      (byte) 57,
      (byte) 178,
      (byte) 244,
      (byte) 19,
      (byte) 15,
      (byte) 7,
      (byte) 110,
      (byte) 14,
      (byte) 20,
      (byte) 3,
      (byte) 85,
      (byte) 78,
      (byte) 99,
      (byte) 10,
      (byte) 64 /*0x40*/,
      (byte) 240 /*0xF0*/,
      (byte) 72,
      (byte) 73,
      (byte) 228,
      (byte) 104,
      (byte) 207,
      (byte) 160 /*0xA0*/,
      (byte) 68,
      (byte) 112 /*0x70*/,
      (byte) 225,
      (byte) 142,
      (byte) 129,
      (byte) 162,
      (byte) 66,
      (byte) 73,
      (byte) 168,
      (byte) 9,
      (byte) 27,
      (byte) 227,
      (byte) 93,
      (byte) 37,
      (byte) 138,
      (byte) 125,
      (byte) 135,
      (byte) 239,
      (byte) 35,
      (byte) 80 /*0x50*/,
      (byte) 0,
      (byte) 116,
      (byte) 88
    };
    byte[] numArray10 = new byte[55]
    {
      (byte) 68,
      (byte) 41,
      (byte) 134,
      (byte) 184,
      (byte) 83,
      (byte) 9,
      (byte) 30,
      (byte) 156,
      (byte) 200,
      (byte) 22,
      (byte) 0,
      (byte) 21,
      (byte) 195,
      (byte) 226,
      (byte) 50,
      (byte) 66,
      (byte) 246,
      (byte) 7,
      (byte) 199,
      (byte) 60,
      (byte) 6,
      (byte) 55,
      (byte) 42,
      (byte) 74,
      (byte) 250,
      (byte) 117,
      (byte) 224 /*0xE0*/,
      (byte) 99,
      (byte) 130,
      (byte) 237,
      (byte) 158,
      (byte) 145,
      (byte) 155,
      (byte) 66,
      (byte) 189,
      byte.MaxValue,
      (byte) 121,
      (byte) 167,
      (byte) 103,
      (byte) 177,
      (byte) 112 /*0x70*/,
      (byte) 139,
      (byte) 173,
      (byte) 147,
      (byte) 44,
      (byte) 65,
      (byte) 75,
      (byte) 91,
      (byte) 70,
      (byte) 217,
      (byte) 10,
      (byte) 208 /*0xD0*/,
      (byte) 247,
      (byte) 183,
      (byte) 4
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray8, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index] ^= numArray10[index];
    byte[] numArray11 = new byte[55]
    {
      (byte) 128 /*0x80*/,
      (byte) 56,
      (byte) 109,
      (byte) 140,
      (byte) 232,
      (byte) 240 /*0xF0*/,
      (byte) 61,
      (byte) 11,
      (byte) 152,
      (byte) 110,
      (byte) 131,
      (byte) 49,
      (byte) 129,
      (byte) 134,
      (byte) 204,
      (byte) 204,
      (byte) 203,
      (byte) 90,
      (byte) 75,
      (byte) 51,
      (byte) 128 /*0x80*/,
      (byte) 172,
      (byte) 27,
      (byte) 101,
      (byte) 203,
      (byte) 194,
      (byte) 245,
      (byte) 69,
      (byte) 126,
      (byte) 160 /*0xA0*/,
      (byte) 207,
      (byte) 23,
      (byte) 84,
      (byte) 36,
      (byte) 1,
      (byte) 105,
      (byte) 212,
      (byte) 188,
      (byte) 192 /*0xC0*/,
      (byte) 25,
      (byte) 221,
      (byte) 215,
      (byte) 197,
      (byte) 183,
      (byte) 7,
      (byte) 76,
      (byte) 5,
      (byte) 105,
      (byte) 87,
      (byte) 132,
      (byte) 53,
      (byte) 26,
      (byte) 13,
      (byte) 90,
      (byte) 232
    };
    byte[] numArray12 = new byte[55];
    numArray12[47] = (byte) 102;
    numArray12[1] = (byte) 31 /*0x1F*/;
    numArray12[2] = (byte) 250;
    numArray12[3] = (byte) 179;
    numArray12[4] = (byte) 96 /*0x60*/;
    numArray12[14] = (byte) 238;
    numArray12[6] = (byte) 240 /*0xF0*/;
    numArray12[7] = (byte) 116;
    numArray12[53] = (byte) 203;
    numArray12[20] = (byte) 71;
    numArray12[8] = (byte) 196;
    numArray12[12] = (byte) 120;
    numArray12[15] = (byte) 135;
    numArray12[24] = (byte) 243;
    numArray12[0] = (byte) 178;
    numArray12[22] = (byte) 94;
    numArray12[16 /*0x10*/] = (byte) 93;
    numArray12[52] = (byte) 158;
    numArray12[18] = (byte) 155;
    numArray12[10] = (byte) 131;
    numArray12[31 /*0x1F*/] = (byte) 213;
    numArray12[21] = (byte) 174;
    numArray12[42] = (byte) 74;
    numArray12[23] = (byte) 88;
    numArray12[11] = (byte) 166;
    numArray12[25] = (byte) 52;
    numArray12[26] = (byte) 228;
    numArray12[27] = (byte) 136;
    numArray12[28] = (byte) 232;
    numArray12[29] = (byte) 110;
    numArray12[30] = (byte) 219;
    numArray12[48 /*0x30*/] = (byte) 0;
    numArray12[5] = (byte) 96 /*0x60*/;
    numArray12[33] = (byte) 173;
    numArray12[43] = (byte) 194;
    numArray12[35] = (byte) 221;
    numArray12[36] = (byte) 227;
    numArray12[17] = (byte) 210;
    numArray12[38] = (byte) 208 /*0xD0*/;
    numArray12[39] = (byte) 126;
    numArray12[40] = (byte) 54;
    numArray12[41] = (byte) 106;
    numArray12[50] = (byte) 90;
    numArray12[32 /*0x20*/] = (byte) 138;
    numArray12[34] = (byte) 9;
    numArray12[45] = (byte) 183;
    numArray12[46] = (byte) 35;
    numArray12[9] = (byte) 173;
    numArray12[13] = (byte) 124;
    numArray12[49] = (byte) 20;
    numArray12[19] = (byte) 233;
    numArray12[51] = (byte) 183;
    numArray12[44] = (byte) 116;
    numArray12[37] = (byte) 206;
    numArray12[54] = (byte) 174;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray8, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray8[index + 55] ^= numArray12[index];
    byte[] numArray13 = new byte[20]
    {
      (byte) 170,
      (byte) 66,
      (byte) 105,
      (byte) 112 /*0x70*/,
      (byte) 10,
      (byte) 146,
      (byte) 141,
      (byte) 233,
      (byte) 96 /*0x60*/,
      (byte) 226,
      (byte) 32 /*0x20*/,
      (byte) 86,
      (byte) 21,
      (byte) 42,
      (byte) 20,
      byte.MaxValue,
      (byte) 21,
      (byte) 111,
      (byte) 95,
      (byte) 26
    };
    byte[] numArray14 = new byte[20]
    {
      (byte) 123,
      (byte) 10,
      (byte) 195,
      (byte) 232,
      (byte) 132,
      (byte) 69,
      (byte) 166,
      (byte) 156,
      (byte) 7,
      (byte) 239,
      (byte) 57,
      (byte) 225,
      (byte) 77,
      (byte) 237,
      (byte) 230,
      (byte) 216,
      (byte) 229,
      (byte) 94,
      (byte) 46,
      (byte) 174
    };
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray8, 110, 20);
    for (int index = 0; index < 20; ++index)
      numArray8[index + 110] ^= numArray14[index];
    byte[] numArray15 = new byte[52];
    byte[] response = new byte[52];
    Array.Copy((Array) sc_12296.sspq, 50, (Array) numArray15, 0, 52);
    key.Query(true, 335, numArray15, response);
    Array.Copy((Array) sc_12296.sspr, 50, (Array) numArray15, 0, 52);
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

  internal static string ssp_appserver_12300()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[2];
      byte[] numArray2 = new byte[2]
      {
        (byte) 131,
        (byte) 137
      };
      byte[] numArray3 = new byte[2]
      {
        (byte) 0,
        (byte) 64 /*0x40*/
      };
      numArray3[0] = (byte) 210;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 2);
      for (int index = 0; index < 2; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[2];
    byte[] numArray5 = new byte[2]{ (byte) 29, (byte) 111 };
    byte[] numArray6 = new byte[2]{ (byte) 84, (byte) 173 };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 2);
    for (int index = 0; index < 2; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12301()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 146,
        (byte) 247,
        (byte) 121,
        (byte) 11
      };
      byte[] numArray3 = new byte[4]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 110
      };
      numArray3[1] = (byte) 111;
      numArray3[0] = (byte) 229;
      numArray3[2] = (byte) 122;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[4];
    byte[] numArray5 = new byte[4]
    {
      (byte) 0,
      (byte) 0,
      (byte) 56,
      (byte) 0
    };
    numArray5[0] = (byte) 93;
    numArray5[1] = byte.MaxValue;
    numArray5[3] = (byte) 246;
    byte[] numArray6 = new byte[4]
    {
      (byte) 201,
      (byte) 112 /*0x70*/,
      (byte) 40,
      (byte) 77
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12302()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        byte.MaxValue,
        (byte) 197,
        (byte) 140,
        (byte) 5,
        (byte) 165,
        (byte) 87,
        (byte) 210,
        (byte) 213,
        (byte) 246,
        (byte) 131
      };
      byte[] numArray3 = new byte[10];
      numArray3[0] = (byte) 159;
      numArray3[3] = (byte) 25;
      numArray3[2] = (byte) 79;
      numArray3[9] = (byte) 124;
      numArray3[4] = (byte) 225;
      numArray3[5] = (byte) 177;
      numArray3[6] = (byte) 205;
      numArray3[1] = (byte) 173;
      numArray3[8] = (byte) 179;
      numArray3[7] = (byte) 63 /*0x3F*/;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_12296.sspq, 102, (Array) numArray4, 0, 53);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12296.sspr, 102, (Array) numArray4, 0, 53);
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
    byte[] numArray6 = new byte[10];
    numArray6[2] = (byte) 194;
    numArray6[1] = (byte) 46;
    numArray6[4] = (byte) 50;
    numArray6[3] = (byte) 102;
    numArray6[5] = (byte) 232;
    numArray6[7] = (byte) 98;
    numArray6[6] = (byte) 11;
    numArray6[8] = (byte) 34;
    numArray6[0] = (byte) 51;
    numArray6[9] = (byte) 141;
    byte[] numArray7 = new byte[10]
    {
      (byte) 158,
      (byte) 157,
      (byte) 199,
      (byte) 7,
      (byte) 183,
      (byte) 11,
      (byte) 65,
      (byte) 180,
      (byte) 162,
      (byte) 91
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12303()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[4] = (byte) 229;
      numArray2[1] = (byte) 125;
      numArray2[3] = (byte) 249;
      numArray2[0] = (byte) 16 /*0x10*/;
      numArray2[8] = byte.MaxValue;
      numArray2[5] = (byte) 67;
      numArray2[6] = (byte) 144 /*0x90*/;
      numArray2[7] = (byte) 61;
      numArray2[2] = (byte) 180;
      numArray2[9] = (byte) 175;
      byte[] numArray3 = new byte[10]
      {
        (byte) 141,
        (byte) 207,
        (byte) 136,
        (byte) 172,
        (byte) 138,
        (byte) 169,
        (byte) 7,
        (byte) 187,
        (byte) 68,
        (byte) 58
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
      (byte) 169,
      (byte) 234,
      (byte) 132,
      (byte) 37,
      (byte) 36,
      (byte) 163,
      (byte) 182,
      (byte) 122,
      (byte) 169,
      (byte) 251
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 30,
      (byte) 49,
      (byte) 107,
      (byte) 126,
      (byte) 236,
      (byte) 141,
      (byte) 9,
      (byte) 177,
      (byte) 225,
      (byte) 59
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
