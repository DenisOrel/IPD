// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12961
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12961
{
  private static byte[] sspq = new byte[92]
  {
    (byte) 250,
    (byte) 141,
    (byte) 194,
    (byte) 206,
    (byte) 63 /*0x3F*/,
    (byte) 215,
    (byte) 163,
    (byte) 254,
    (byte) 131,
    (byte) 91,
    (byte) 137,
    (byte) 12,
    (byte) 66,
    (byte) 93,
    (byte) 187,
    (byte) 135,
    (byte) 183,
    (byte) 7,
    (byte) 151,
    (byte) 112 /*0x70*/,
    (byte) 141,
    (byte) 0,
    (byte) 126,
    (byte) 40,
    (byte) 205,
    (byte) 252,
    (byte) 179,
    (byte) 31 /*0x1F*/,
    (byte) 71,
    (byte) 200,
    (byte) 88,
    (byte) 20,
    (byte) 38,
    (byte) 225,
    (byte) 230,
    (byte) 247,
    (byte) 174,
    (byte) 62,
    (byte) 79,
    (byte) 184,
    (byte) 19,
    (byte) 200,
    (byte) 121,
    (byte) 16 /*0x10*/,
    (byte) 70,
    (byte) 237,
    (byte) 223,
    (byte) 174,
    (byte) 70,
    (byte) 159,
    (byte) 42,
    (byte) 136,
    (byte) 25,
    (byte) 175,
    (byte) 205,
    (byte) 137,
    (byte) 86,
    (byte) 148,
    (byte) 139,
    (byte) 189,
    (byte) 186,
    byte.MaxValue,
    (byte) 212,
    (byte) 130,
    (byte) 192 /*0xC0*/,
    (byte) 248,
    (byte) 165,
    (byte) 47,
    (byte) 103,
    (byte) 20,
    (byte) 105,
    (byte) 224 /*0xE0*/,
    (byte) 216,
    (byte) 62,
    (byte) 185,
    (byte) 155,
    (byte) 77,
    (byte) 206,
    (byte) 233,
    (byte) 74,
    (byte) 88,
    (byte) 117,
    (byte) 208 /*0xD0*/,
    (byte) 207,
    (byte) 67,
    (byte) 37,
    (byte) 51,
    (byte) 160 /*0xA0*/,
    (byte) 159,
    (byte) 42,
    (byte) 185,
    (byte) 11
  };
  private static byte[] sspr = new byte[92]
  {
    (byte) 113,
    (byte) 124,
    (byte) 52,
    (byte) 37,
    (byte) 142,
    (byte) 175,
    (byte) 43,
    (byte) 217,
    (byte) 53,
    (byte) 232,
    (byte) 107,
    (byte) 90,
    (byte) 239,
    (byte) 250,
    (byte) 99,
    (byte) 166,
    (byte) 139,
    (byte) 90,
    (byte) 189,
    (byte) 58,
    (byte) 241,
    (byte) 225,
    (byte) 140,
    (byte) 71,
    (byte) 224 /*0xE0*/,
    (byte) 74,
    (byte) 53,
    (byte) 114,
    (byte) 249,
    (byte) 110,
    (byte) 4,
    (byte) 207,
    (byte) 254,
    (byte) 246,
    (byte) 71,
    (byte) 99,
    (byte) 227,
    (byte) 150,
    (byte) 126,
    (byte) 130,
    (byte) 126,
    (byte) 42,
    (byte) 208 /*0xD0*/,
    (byte) 160 /*0xA0*/,
    (byte) 171,
    (byte) 0,
    (byte) 239,
    (byte) 149,
    (byte) 91,
    (byte) 111,
    (byte) 42,
    byte.MaxValue,
    (byte) 63 /*0x3F*/,
    (byte) 193,
    (byte) 31 /*0x1F*/,
    (byte) 194,
    (byte) 251,
    (byte) 215,
    (byte) 108,
    (byte) 240 /*0xF0*/,
    (byte) 163,
    (byte) 62,
    (byte) 254,
    (byte) 26,
    (byte) 82,
    (byte) 130,
    (byte) 43,
    (byte) 31 /*0x1F*/,
    (byte) 154,
    (byte) 119,
    (byte) 221,
    (byte) 227,
    (byte) 84,
    (byte) 64 /*0x40*/,
    (byte) 2,
    (byte) 103,
    (byte) 136,
    (byte) 79,
    (byte) 55,
    (byte) 71,
    (byte) 182,
    (byte) 159,
    (byte) 242,
    (byte) 101,
    (byte) 221,
    (byte) 121,
    (byte) 150,
    (byte) 233,
    (byte) 90,
    (byte) 120,
    (byte) 122,
    (byte) 140
  };

  internal static string ssp_appserver_12962()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 234,
        (byte) 90,
        (byte) 55,
        (byte) 186,
        (byte) 139,
        (byte) 215
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 193,
        (byte) 42,
        (byte) 20,
        (byte) 201,
        (byte) 181,
        (byte) 17
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[43];
      byte[] response = new byte[43];
      Array.Copy((Array) sc_12961.sspq, 0, (Array) numArray4, 0, 43);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12961.sspr, 0, (Array) numArray4, 0, 43);
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
    byte[] numArray5 = new byte[6];
    byte[] numArray6 = new byte[6]
    {
      (byte) 5,
      (byte) 117,
      (byte) 221,
      (byte) 23,
      (byte) 217,
      (byte) 254
    };
    byte[] numArray7 = new byte[6]
    {
      (byte) 160 /*0xA0*/,
      (byte) 85,
      (byte) 0,
      (byte) 0,
      (byte) 136,
      (byte) 0
    };
    numArray7[3] = (byte) 66;
    numArray7[2] = (byte) 250;
    numArray7[5] = (byte) 86;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12963()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[179];
      byte[] numArray2 = new byte[55]
      {
        (byte) 66,
        (byte) 36,
        (byte) 132,
        (byte) 182,
        (byte) 186,
        (byte) 3,
        (byte) 117,
        (byte) 229,
        (byte) 97,
        (byte) 119,
        (byte) 196,
        (byte) 216,
        (byte) 120,
        (byte) 184,
        (byte) 222,
        (byte) 92,
        (byte) 197,
        (byte) 134,
        (byte) 121,
        (byte) 178,
        (byte) 239,
        (byte) 228,
        (byte) 217,
        (byte) 32 /*0x20*/,
        (byte) 67,
        (byte) 168,
        (byte) 135,
        (byte) 178,
        (byte) 214,
        (byte) 189,
        (byte) 236,
        (byte) 232,
        (byte) 20,
        (byte) 61,
        (byte) 12,
        (byte) 148,
        (byte) 143,
        (byte) 17,
        (byte) 191,
        (byte) 1,
        (byte) 0,
        (byte) 248,
        (byte) 182,
        (byte) 98,
        (byte) 58,
        (byte) 43,
        (byte) 168,
        (byte) 24,
        (byte) 131,
        (byte) 243,
        (byte) 94,
        (byte) 116,
        (byte) 149,
        (byte) 231,
        (byte) 18
      };
      byte[] numArray3 = new byte[55]
      {
        (byte) 49,
        (byte) 90,
        (byte) 200,
        (byte) 179,
        (byte) 162,
        (byte) 172,
        (byte) 71,
        (byte) 161,
        (byte) 135,
        (byte) 176 /*0xB0*/,
        (byte) 142,
        (byte) 7,
        (byte) 217,
        (byte) 81,
        (byte) 135,
        (byte) 103,
        (byte) 29,
        (byte) 204,
        (byte) 182,
        (byte) 36,
        (byte) 199,
        (byte) 191,
        (byte) 81,
        (byte) 26,
        (byte) 160 /*0xA0*/,
        (byte) 69,
        (byte) 81,
        (byte) 242,
        (byte) 102,
        (byte) 101,
        (byte) 35,
        (byte) 100,
        (byte) 138,
        (byte) 197,
        (byte) 254,
        (byte) 191,
        (byte) 125,
        (byte) 10,
        (byte) 72,
        (byte) 18,
        (byte) 150,
        (byte) 105,
        (byte) 217,
        (byte) 181,
        (byte) 251,
        (byte) 190,
        (byte) 37,
        (byte) 63 /*0x3F*/,
        (byte) 251,
        (byte) 86,
        (byte) 51,
        (byte) 176 /*0xB0*/,
        (byte) 50,
        (byte) 84,
        (byte) 34
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[55]
      {
        (byte) 99,
        (byte) 115,
        (byte) 18,
        (byte) 124,
        (byte) 194,
        (byte) 15,
        (byte) 163,
        (byte) 139,
        (byte) 220,
        (byte) 196,
        (byte) 59,
        (byte) 172,
        (byte) 74,
        (byte) 205,
        (byte) 180,
        (byte) 198,
        (byte) 119,
        (byte) 177,
        (byte) 247,
        (byte) 45,
        (byte) 119,
        (byte) 230,
        (byte) 23,
        byte.MaxValue,
        (byte) 110,
        (byte) 47,
        (byte) 226,
        (byte) 94,
        (byte) 119,
        (byte) 82,
        (byte) 44,
        (byte) 42,
        (byte) 174,
        (byte) 73,
        (byte) 43,
        (byte) 157,
        (byte) 172,
        (byte) 133,
        (byte) 62,
        (byte) 232,
        (byte) 18,
        (byte) 214,
        (byte) 93,
        (byte) 103,
        (byte) 108,
        (byte) 107,
        (byte) 56,
        (byte) 38,
        (byte) 170,
        (byte) 173,
        (byte) 29,
        (byte) 53,
        (byte) 27,
        (byte) 48 /*0x30*/,
        (byte) 211
      };
      byte[] numArray5 = new byte[55];
      numArray5[23] = (byte) 136;
      numArray5[42] = (byte) 177;
      numArray5[2] = (byte) 145;
      numArray5[3] = (byte) 180;
      numArray5[10] = (byte) 166;
      numArray5[5] = (byte) 14;
      numArray5[6] = (byte) 135;
      numArray5[30] = (byte) 253;
      numArray5[26] = (byte) 104;
      numArray5[9] = (byte) 54;
      numArray5[0] = (byte) 235;
      numArray5[11] = (byte) 58;
      numArray5[24] = (byte) 184;
      numArray5[47] = (byte) 119;
      numArray5[41] = (byte) 183;
      numArray5[16 /*0x10*/] = (byte) 13;
      numArray5[38] = (byte) 101;
      numArray5[15] = (byte) 211;
      numArray5[18] = (byte) 55;
      numArray5[19] = (byte) 253;
      numArray5[12] = (byte) 70;
      numArray5[21] = (byte) 247;
      numArray5[22] = (byte) 37;
      numArray5[34] = (byte) 219;
      numArray5[43] = (byte) 128 /*0x80*/;
      numArray5[28] = (byte) 43;
      numArray5[33] = (byte) 59;
      numArray5[7] = (byte) 132;
      numArray5[29] = (byte) 32 /*0x20*/;
      numArray5[37] = (byte) 225;
      numArray5[8] = (byte) 1;
      numArray5[31 /*0x1F*/] = (byte) 135;
      numArray5[40] = (byte) 97;
      numArray5[1] = (byte) 191;
      numArray5[4] = (byte) 80 /*0x50*/;
      numArray5[35] = (byte) 83;
      numArray5[36] = (byte) 149;
      numArray5[44] = (byte) 19;
      numArray5[14] = (byte) 35;
      numArray5[39] = (byte) 21;
      numArray5[45] = (byte) 103;
      numArray5[17] = (byte) 140;
      numArray5[48 /*0x30*/] = (byte) 157;
      numArray5[50] = (byte) 108;
      numArray5[25] = (byte) 112 /*0x70*/;
      numArray5[20] = (byte) 201;
      numArray5[46] = (byte) 149;
      numArray5[27] = (byte) 188;
      numArray5[13] = (byte) 39;
      numArray5[32 /*0x20*/] = (byte) 233;
      numArray5[49] = (byte) 201;
      numArray5[51] = (byte) 192 /*0xC0*/;
      numArray5[52] = (byte) 22;
      numArray5[53] = (byte) 212;
      numArray5[54] = (byte) 201;
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 55] ^= numArray5[index];
      byte[] numArray6 = new byte[55]
      {
        (byte) 127 /*0x7F*/,
        (byte) 10,
        (byte) 155,
        (byte) 113,
        (byte) 144 /*0x90*/,
        (byte) 72,
        (byte) 84,
        (byte) 160 /*0xA0*/,
        (byte) 0,
        (byte) 128 /*0x80*/,
        (byte) 140,
        (byte) 242,
        (byte) 212,
        (byte) 127 /*0x7F*/,
        (byte) 122,
        (byte) 199,
        (byte) 82,
        (byte) 77,
        (byte) 221,
        (byte) 180,
        (byte) 224 /*0xE0*/,
        (byte) 55,
        (byte) 149,
        (byte) 191,
        (byte) 54,
        (byte) 32 /*0x20*/,
        (byte) 55,
        (byte) 9,
        (byte) 52,
        (byte) 3,
        (byte) 104,
        (byte) 65,
        (byte) 185,
        (byte) 150,
        (byte) 27,
        (byte) 109,
        (byte) 16 /*0x10*/,
        (byte) 74,
        (byte) 61,
        (byte) 122,
        (byte) 127 /*0x7F*/,
        (byte) 47,
        (byte) 90,
        (byte) 237,
        (byte) 171,
        (byte) 146,
        (byte) 191,
        (byte) 19,
        (byte) 177,
        (byte) 250,
        (byte) 186,
        (byte) 199,
        (byte) 85,
        (byte) 64 /*0x40*/,
        (byte) 203
      };
      byte[] numArray7 = new byte[55]
      {
        (byte) 54,
        (byte) 234,
        (byte) 166,
        (byte) 164,
        (byte) 36,
        (byte) 208 /*0xD0*/,
        (byte) 33,
        (byte) 249,
        (byte) 5,
        (byte) 162,
        (byte) 130,
        (byte) 146,
        (byte) 114,
        (byte) 246,
        (byte) 148,
        (byte) 126,
        (byte) 232,
        (byte) 240 /*0xF0*/,
        (byte) 152,
        (byte) 201,
        (byte) 104,
        (byte) 182,
        (byte) 244,
        (byte) 113,
        (byte) 126,
        (byte) 221,
        (byte) 210,
        (byte) 105,
        (byte) 84,
        (byte) 244,
        (byte) 63 /*0x3F*/,
        (byte) 165,
        (byte) 2,
        (byte) 55,
        (byte) 13,
        (byte) 124,
        (byte) 189,
        (byte) 178,
        (byte) 123,
        (byte) 15,
        (byte) 169,
        (byte) 62,
        (byte) 211,
        (byte) 251,
        (byte) 233,
        (byte) 218,
        (byte) 170,
        (byte) 234,
        (byte) 248,
        (byte) 197,
        (byte) 234,
        (byte) 29,
        (byte) 202,
        (byte) 144 /*0x90*/,
        (byte) 41
      };
      key.Query(true, 335, numArray6, numArray6);
      Array.Copy((Array) numArray6, 0, (Array) numArray1, 110, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index + 110] ^= numArray7[index];
      byte[] numArray8 = new byte[14]
      {
        (byte) 220,
        (byte) 58,
        (byte) 111,
        (byte) 17,
        (byte) 75,
        (byte) 107,
        (byte) 253,
        (byte) 24,
        (byte) 74,
        (byte) 57,
        (byte) 61,
        (byte) 100,
        (byte) 13,
        (byte) 182
      };
      byte[] numArray9 = new byte[14]
      {
        (byte) 186,
        (byte) 138,
        (byte) 160 /*0xA0*/,
        (byte) 72,
        (byte) 153,
        (byte) 120,
        (byte) 137,
        (byte) 37,
        (byte) 37,
        (byte) 91,
        (byte) 204,
        (byte) 218,
        (byte) 172,
        (byte) 237
      };
      key.Query(true, 335, numArray8, numArray8);
      Array.Copy((Array) numArray8, 0, (Array) numArray1, 165, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index + 165] ^= numArray9[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray10 = new byte[179];
    byte[] numArray11 = new byte[55]
    {
      (byte) 52,
      (byte) 176 /*0xB0*/,
      (byte) 220,
      (byte) 168,
      (byte) 19,
      (byte) 163,
      (byte) 97,
      (byte) 35,
      (byte) 87,
      (byte) 57,
      (byte) 72,
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 121,
      (byte) 203,
      (byte) 211,
      (byte) 247,
      (byte) 52,
      (byte) 15,
      (byte) 205,
      (byte) 104,
      (byte) 52,
      (byte) 195,
      (byte) 93,
      (byte) 212,
      (byte) 97,
      (byte) 89,
      (byte) 129,
      (byte) 174,
      (byte) 164,
      (byte) 68,
      (byte) 179,
      (byte) 183,
      (byte) 242,
      (byte) 12,
      (byte) 26,
      (byte) 106,
      (byte) 205,
      (byte) 200,
      (byte) 151,
      (byte) 14,
      (byte) 237,
      (byte) 243,
      (byte) 49,
      (byte) 181,
      (byte) 119,
      (byte) 94,
      (byte) 192 /*0xC0*/,
      (byte) 114,
      (byte) 144 /*0x90*/,
      (byte) 21,
      (byte) 130,
      (byte) 247,
      (byte) 67,
      (byte) 250
    };
    byte[] numArray12 = new byte[55];
    numArray12[42] = (byte) 185;
    numArray12[1] = (byte) 141;
    numArray12[47] = (byte) 226;
    numArray12[50] = (byte) 18;
    numArray12[18] = (byte) 147;
    numArray12[5] = (byte) 60;
    numArray12[49] = (byte) 176 /*0xB0*/;
    numArray12[7] = (byte) 68;
    numArray12[35] = (byte) 150;
    numArray12[26] = (byte) 115;
    numArray12[41] = (byte) 249;
    numArray12[11] = (byte) 145;
    numArray12[12] = (byte) 34;
    numArray12[30] = (byte) 38;
    numArray12[48 /*0x30*/] = (byte) 78;
    numArray12[38] = (byte) 102;
    numArray12[0] = (byte) 228;
    numArray12[17] = (byte) 26;
    numArray12[19] = (byte) 89;
    numArray12[3] = (byte) 37;
    numArray12[54] = (byte) 146;
    numArray12[21] = (byte) 131;
    numArray12[39] = (byte) 140;
    numArray12[9] = (byte) 64 /*0x40*/;
    numArray12[24] = (byte) 125;
    numArray12[10] = (byte) 196;
    numArray12[34] = (byte) 247;
    numArray12[27] = (byte) 147;
    numArray12[28] = (byte) 211;
    numArray12[52] = (byte) 210;
    numArray12[29] = (byte) 109;
    numArray12[4] = (byte) 78;
    numArray12[32 /*0x20*/] = (byte) 6;
    numArray12[53] = (byte) 219;
    numArray12[43] = (byte) 239;
    numArray12[22] = (byte) 251;
    numArray12[36] = (byte) 187;
    numArray12[37] = (byte) 95;
    numArray12[13] = (byte) 132;
    numArray12[16 /*0x10*/] = (byte) 39;
    numArray12[40] = (byte) 149;
    numArray12[8] = (byte) 235;
    numArray12[14] = (byte) 230;
    numArray12[23] = (byte) 211;
    numArray12[44] = (byte) 169;
    numArray12[45] = (byte) 37;
    numArray12[46] = (byte) 18;
    numArray12[31 /*0x1F*/] = (byte) 182;
    numArray12[6] = (byte) 155;
    numArray12[20] = (byte) 31 /*0x1F*/;
    numArray12[2] = (byte) 154;
    numArray12[51] = (byte) 123;
    numArray12[15] = (byte) 109;
    numArray12[33] = (byte) 93;
    numArray12[25] = (byte) 173;
    key.Query(true, 335, numArray11, numArray11);
    Array.Copy((Array) numArray11, 0, (Array) numArray10, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index] ^= numArray12[index];
    byte[] numArray13 = new byte[55]
    {
      (byte) 215,
      (byte) 117,
      (byte) 175,
      (byte) 176 /*0xB0*/,
      (byte) 208 /*0xD0*/,
      (byte) 86,
      (byte) 36,
      (byte) 52,
      (byte) 217,
      (byte) 117,
      (byte) 2,
      (byte) 95,
      (byte) 130,
      (byte) 126,
      (byte) 48 /*0x30*/,
      (byte) 85,
      (byte) 43,
      (byte) 143,
      (byte) 2,
      (byte) 1,
      (byte) 86,
      (byte) 135,
      (byte) 207,
      (byte) 158,
      (byte) 70,
      (byte) 77,
      (byte) 246,
      (byte) 164,
      (byte) 170,
      (byte) 61,
      (byte) 239,
      (byte) 119,
      (byte) 78,
      (byte) 211,
      (byte) 19,
      (byte) 132,
      (byte) 102,
      (byte) 156,
      (byte) 94,
      (byte) 33,
      (byte) 27,
      (byte) 7,
      (byte) 71,
      (byte) 24,
      (byte) 97,
      (byte) 244,
      (byte) 63 /*0x3F*/,
      (byte) 169,
      (byte) 185,
      (byte) 193,
      (byte) 83,
      (byte) 244,
      (byte) 125,
      (byte) 188,
      (byte) 3
    };
    byte[] numArray14 = new byte[55];
    numArray14[30] = (byte) 225;
    numArray14[1] = (byte) 87;
    numArray14[28] = (byte) 87;
    numArray14[2] = (byte) 161;
    numArray14[43] = (byte) 152;
    numArray14[5] = (byte) 232;
    numArray14[7] = (byte) 9;
    numArray14[3] = (byte) 196;
    numArray14[8] = (byte) 251;
    numArray14[40] = (byte) 172;
    numArray14[34] = (byte) 171;
    numArray14[9] = (byte) 59;
    numArray14[11] = (byte) 201;
    numArray14[6] = (byte) 76;
    numArray14[14] = (byte) 157;
    numArray14[15] = (byte) 113;
    numArray14[52] = (byte) 242;
    numArray14[17] = (byte) 145;
    numArray14[18] = (byte) 170;
    numArray14[19] = (byte) 107;
    numArray14[10] = (byte) 197;
    numArray14[21] = (byte) 157;
    numArray14[31 /*0x1F*/] = (byte) 165;
    numArray14[23] = (byte) 33;
    numArray14[24] = (byte) 36;
    numArray14[25] = (byte) 39;
    numArray14[26] = (byte) 145;
    numArray14[27] = (byte) 225;
    numArray14[51] = (byte) 34;
    numArray14[29] = (byte) 51;
    numArray14[12] = (byte) 238;
    numArray14[20] = (byte) 32 /*0x20*/;
    numArray14[32 /*0x20*/] = (byte) 251;
    numArray14[53] = (byte) 187;
    numArray14[16 /*0x10*/] = (byte) 138;
    numArray14[45] = (byte) 79;
    numArray14[50] = (byte) 97;
    numArray14[49] = (byte) 92;
    numArray14[38] = (byte) 192 /*0xC0*/;
    numArray14[39] = (byte) 120;
    numArray14[0] = (byte) 119;
    numArray14[42] = (byte) 181;
    numArray14[13] = (byte) 62;
    numArray14[54] = (byte) 16 /*0x10*/;
    numArray14[33] = (byte) 92;
    numArray14[47] = (byte) 131;
    numArray14[36] = (byte) 222;
    numArray14[41] = (byte) 43;
    numArray14[48 /*0x30*/] = (byte) 148;
    numArray14[22] = (byte) 27;
    numArray14[44] = (byte) 242;
    numArray14[35] = (byte) 21;
    numArray14[46] = (byte) 70;
    numArray14[4] = (byte) 95;
    numArray14[37] = (byte) 9;
    key.Query(true, 335, numArray13, numArray13);
    Array.Copy((Array) numArray13, 0, (Array) numArray10, 55, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 55] ^= numArray14[index];
    byte[] numArray15 = new byte[55]
    {
      (byte) 211,
      (byte) 24,
      (byte) 117,
      (byte) 17,
      (byte) 149,
      (byte) 161,
      (byte) 178,
      (byte) 139,
      (byte) 145,
      (byte) 126,
      (byte) 223,
      (byte) 168,
      (byte) 206,
      (byte) 153,
      (byte) 93,
      (byte) 86,
      (byte) 30,
      (byte) 75,
      (byte) 209,
      (byte) 149,
      (byte) 35,
      (byte) 104,
      (byte) 110,
      (byte) 125,
      (byte) 15,
      (byte) 221,
      (byte) 21,
      (byte) 37,
      (byte) 55,
      (byte) 93,
      (byte) 186,
      (byte) 27,
      (byte) 178,
      (byte) 239,
      (byte) 130,
      (byte) 25,
      (byte) 101,
      (byte) 176 /*0xB0*/,
      (byte) 5,
      (byte) 220,
      (byte) 249,
      (byte) 7,
      (byte) 67,
      (byte) 100,
      (byte) 176 /*0xB0*/,
      (byte) 150,
      (byte) 63 /*0x3F*/,
      (byte) 57,
      (byte) 102,
      (byte) 109,
      (byte) 126,
      (byte) 139,
      (byte) 227,
      (byte) 123,
      (byte) 203
    };
    byte[] numArray16 = new byte[55]
    {
      (byte) 249,
      (byte) 158,
      (byte) 82,
      (byte) 68,
      (byte) 200,
      (byte) 159,
      (byte) 136,
      (byte) 42,
      (byte) 183,
      (byte) 16 /*0x10*/,
      (byte) 163,
      (byte) 97,
      (byte) 212,
      (byte) 220,
      (byte) 57,
      (byte) 98,
      (byte) 82,
      (byte) 177,
      (byte) 51,
      (byte) 69,
      (byte) 128 /*0x80*/,
      (byte) 83,
      (byte) 133,
      (byte) 28,
      (byte) 38,
      (byte) 215,
      (byte) 77,
      (byte) 18,
      (byte) 150,
      (byte) 164,
      (byte) 202,
      (byte) 232,
      (byte) 43,
      (byte) 228,
      (byte) 99,
      (byte) 23,
      (byte) 99,
      (byte) 118,
      (byte) 84,
      (byte) 24,
      (byte) 193,
      (byte) 226,
      (byte) 17,
      (byte) 134,
      (byte) 231,
      (byte) 98,
      (byte) 119,
      (byte) 75,
      (byte) 61,
      (byte) 52,
      (byte) 178,
      (byte) 216,
      (byte) 34,
      (byte) 203,
      (byte) 126
    };
    key.Query(true, 335, numArray15, numArray15);
    Array.Copy((Array) numArray15, 0, (Array) numArray10, 110, 55);
    for (int index = 0; index < 55; ++index)
      numArray10[index + 110] ^= numArray16[index];
    byte[] numArray17 = new byte[14]
    {
      (byte) 247,
      (byte) 2,
      (byte) 184,
      (byte) 139,
      (byte) 140,
      (byte) 83,
      (byte) 52,
      (byte) 195,
      (byte) 196,
      (byte) 245,
      (byte) 222,
      (byte) 22,
      (byte) 68,
      (byte) 240 /*0xF0*/
    };
    byte[] numArray18 = new byte[14];
    numArray18[3] = (byte) 199;
    numArray18[6] = (byte) 207;
    numArray18[10] = (byte) 2;
    numArray18[12] = (byte) 188;
    numArray18[4] = (byte) 214;
    numArray18[8] = (byte) 215;
    numArray18[0] = (byte) 248;
    numArray18[7] = (byte) 177;
    numArray18[5] = (byte) 96 /*0x60*/;
    numArray18[9] = (byte) 199;
    numArray18[2] = (byte) 111;
    numArray18[11] = (byte) 218;
    numArray18[1] = (byte) 1;
    numArray18[13] = (byte) 190;
    key.Query(true, 335, numArray17, numArray17);
    Array.Copy((Array) numArray17, 0, (Array) numArray10, 165, 14);
    for (int index = 0; index < 14; ++index)
      numArray10[index + 165] ^= numArray18[index];
    return Encoding.UTF8.GetString(numArray10);
  }

  internal static string ssp_appserver_12964()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[3];
      byte[] numArray2 = new byte[3]
      {
        (byte) 0,
        (byte) 0,
        (byte) 110
      };
      numArray2[1] = (byte) 111;
      numArray2[0] = (byte) 23;
      byte[] numArray3 = new byte[3]
      {
        (byte) 25,
        (byte) 221,
        (byte) 227
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 3);
      for (int index = 0; index < 3; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[3];
    byte[] numArray5 = new byte[3]
    {
      (byte) 160 /*0xA0*/,
      (byte) 236,
      (byte) 160 /*0xA0*/
    };
    byte[] numArray6 = new byte[3]
    {
      (byte) 248,
      (byte) 215,
      (byte) 197
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 3);
    for (int index = 0; index < 3; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[49];
    byte[] response = new byte[49];
    Array.Copy((Array) sc_12961.sspq, 43, (Array) numArray7, 0, 49);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12961.sspr, 43, (Array) numArray7, 0, 49);
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
