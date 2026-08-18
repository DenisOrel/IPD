// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12395
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12395
{
  private static byte[] sspq = new byte[178]
  {
    (byte) 17,
    (byte) 41,
    (byte) 249,
    (byte) 81,
    (byte) 115,
    (byte) 62,
    (byte) 117,
    (byte) 55,
    (byte) 82,
    (byte) 89,
    (byte) 37,
    (byte) 7,
    (byte) 135,
    (byte) 242,
    (byte) 95,
    (byte) 93,
    (byte) 79,
    (byte) 183,
    (byte) 49,
    (byte) 157,
    (byte) 113,
    (byte) 167,
    (byte) 130,
    (byte) 17,
    (byte) 243,
    (byte) 179,
    (byte) 54,
    (byte) 93,
    (byte) 61,
    (byte) 9,
    (byte) 97,
    (byte) 227,
    (byte) 31 /*0x1F*/,
    (byte) 239,
    (byte) 215,
    (byte) 121,
    (byte) 168,
    (byte) 204,
    (byte) 156,
    (byte) 163,
    (byte) 250,
    (byte) 152,
    (byte) 91,
    (byte) 57,
    (byte) 99,
    (byte) 51,
    (byte) 31 /*0x1F*/,
    (byte) 35,
    (byte) 64 /*0x40*/,
    (byte) 132,
    (byte) 78,
    (byte) 88,
    (byte) 50,
    (byte) 220,
    (byte) 218,
    (byte) 44,
    (byte) 139,
    (byte) 201,
    (byte) 0,
    (byte) 55,
    (byte) 145,
    (byte) 147,
    (byte) 0,
    (byte) 164,
    (byte) 188,
    (byte) 56,
    (byte) 223,
    (byte) 182,
    (byte) 231,
    (byte) 120,
    (byte) 130,
    (byte) 136,
    (byte) 144 /*0x90*/,
    (byte) 48 /*0x30*/,
    (byte) 232,
    (byte) 194,
    (byte) 16 /*0x10*/,
    (byte) 62,
    (byte) 88,
    (byte) 218,
    (byte) 111,
    (byte) 159,
    (byte) 112 /*0x70*/,
    (byte) 158,
    (byte) 194,
    (byte) 142,
    (byte) 208 /*0xD0*/,
    (byte) 186,
    (byte) 179,
    (byte) 251,
    (byte) 30,
    (byte) 127 /*0x7F*/,
    (byte) 192 /*0xC0*/,
    (byte) 127 /*0x7F*/,
    (byte) 76,
    (byte) 37,
    (byte) 0,
    (byte) 45,
    (byte) 99,
    (byte) 59,
    (byte) 32 /*0x20*/,
    (byte) 128 /*0x80*/,
    (byte) 171,
    (byte) 82,
    (byte) 232,
    (byte) 76,
    (byte) 77,
    (byte) 113,
    (byte) 187,
    (byte) 128 /*0x80*/,
    (byte) 48 /*0x30*/,
    (byte) 90,
    (byte) 109,
    (byte) 90,
    (byte) 45,
    (byte) 220,
    (byte) 5,
    (byte) 18,
    (byte) 139,
    (byte) 222,
    (byte) 30,
    (byte) 169,
    (byte) 112 /*0x70*/,
    (byte) 146,
    (byte) 248,
    (byte) 129,
    (byte) 237,
    (byte) 202,
    (byte) 180,
    (byte) 22,
    (byte) 79,
    (byte) 163,
    (byte) 108,
    (byte) 84,
    (byte) 112 /*0x70*/,
    (byte) 7,
    (byte) 204,
    (byte) 160 /*0xA0*/,
    (byte) 194,
    (byte) 33,
    (byte) 135,
    (byte) 141,
    (byte) 126,
    (byte) 34,
    (byte) 54,
    (byte) 49,
    (byte) 236,
    (byte) 17,
    (byte) 69,
    (byte) 188,
    (byte) 192 /*0xC0*/,
    (byte) 2,
    (byte) 149,
    (byte) 142,
    (byte) 237,
    (byte) 220,
    (byte) 44,
    (byte) 138,
    (byte) 138,
    (byte) 252,
    (byte) 127 /*0x7F*/,
    (byte) 228,
    (byte) 75,
    (byte) 199,
    (byte) 6,
    (byte) 247,
    (byte) 41,
    (byte) 160 /*0xA0*/,
    (byte) 97,
    (byte) 38,
    (byte) 92,
    (byte) 185,
    (byte) 131,
    (byte) 29,
    (byte) 100,
    (byte) 122,
    (byte) 44,
    (byte) 228
  };
  private static byte[] sspr = new byte[178]
  {
    (byte) 151,
    (byte) 215,
    (byte) 25,
    (byte) 220,
    (byte) 32 /*0x20*/,
    (byte) 129,
    (byte) 31 /*0x1F*/,
    (byte) 185,
    (byte) 164,
    (byte) 252,
    (byte) 71,
    (byte) 218,
    (byte) 96 /*0x60*/,
    (byte) 167,
    (byte) 80 /*0x50*/,
    (byte) 98,
    (byte) 6,
    (byte) 87,
    (byte) 205,
    (byte) 166,
    (byte) 13,
    (byte) 60,
    (byte) 250,
    (byte) 183,
    (byte) 222,
    (byte) 118,
    (byte) 47,
    (byte) 235,
    (byte) 217,
    (byte) 203,
    (byte) 236,
    (byte) 247,
    (byte) 119,
    (byte) 155,
    (byte) 58,
    (byte) 104,
    (byte) 40,
    (byte) 218,
    (byte) 186,
    (byte) 11,
    (byte) 161,
    (byte) 252,
    (byte) 73,
    (byte) 107,
    (byte) 200,
    (byte) 38,
    (byte) 74,
    (byte) 27,
    (byte) 247,
    (byte) 177,
    (byte) 224 /*0xE0*/,
    (byte) 147,
    (byte) 65,
    (byte) 151,
    (byte) 184,
    (byte) 241,
    (byte) 242,
    (byte) 40,
    (byte) 191,
    (byte) 240 /*0xF0*/,
    (byte) 158,
    (byte) 228,
    (byte) 233,
    (byte) 62,
    (byte) 115,
    (byte) 86,
    (byte) 56,
    (byte) 178,
    byte.MaxValue,
    (byte) 125,
    (byte) 191,
    (byte) 116,
    (byte) 35,
    (byte) 81,
    (byte) 173,
    (byte) 150,
    (byte) 200,
    (byte) 133,
    (byte) 132,
    (byte) 74,
    (byte) 42,
    (byte) 23,
    (byte) 123,
    (byte) 48 /*0x30*/,
    (byte) 197,
    (byte) 232,
    (byte) 53,
    (byte) 0,
    (byte) 194,
    (byte) 115,
    (byte) 50,
    (byte) 12,
    (byte) 140,
    (byte) 5,
    (byte) 188,
    (byte) 65,
    (byte) 149,
    (byte) 78,
    (byte) 60,
    (byte) 105,
    (byte) 93,
    (byte) 22,
    (byte) 86,
    (byte) 48 /*0x30*/,
    (byte) 7,
    (byte) 96 /*0x60*/,
    byte.MaxValue,
    (byte) 166,
    (byte) 69,
    (byte) 192 /*0xC0*/,
    (byte) 153,
    (byte) 244,
    (byte) 14,
    (byte) 192 /*0xC0*/,
    (byte) 235,
    (byte) 59,
    (byte) 142,
    (byte) 226,
    (byte) 86,
    (byte) 119,
    (byte) 240 /*0xF0*/,
    (byte) 222,
    (byte) 100,
    (byte) 86,
    (byte) 92,
    (byte) 151,
    (byte) 29,
    (byte) 246,
    (byte) 67,
    (byte) 237,
    (byte) 11,
    (byte) 64 /*0x40*/,
    (byte) 115,
    (byte) 17,
    (byte) 230,
    (byte) 190,
    (byte) 208 /*0xD0*/,
    (byte) 221,
    (byte) 14,
    (byte) 109,
    (byte) 193,
    (byte) 118,
    (byte) 73,
    (byte) 84,
    (byte) 107,
    (byte) 202,
    (byte) 249,
    (byte) 47,
    (byte) 11,
    (byte) 244,
    (byte) 101,
    (byte) 114,
    (byte) 96 /*0x60*/,
    (byte) 40,
    (byte) 174,
    (byte) 225,
    (byte) 28,
    (byte) 182,
    (byte) 168,
    (byte) 168,
    (byte) 167,
    (byte) 223,
    (byte) 242,
    (byte) 81,
    (byte) 207,
    (byte) 116,
    (byte) 248,
    (byte) 85,
    (byte) 238,
    (byte) 55,
    (byte) 57,
    (byte) 194,
    (byte) 250,
    (byte) 204,
    (byte) 239,
    (byte) 242,
    (byte) 124,
    (byte) 80 /*0x50*/
  };

  internal static int ssp_appserver_12396(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 204,
      (byte) 216,
      byte.MaxValue,
      (byte) 1,
      (byte) 53,
      (byte) 204,
      (byte) 90,
      (byte) 40,
      (byte) 134,
      (byte) 32 /*0x20*/,
      (byte) 88,
      (byte) 23,
      (byte) 156,
      (byte) 219,
      (byte) 21,
      (byte) 117,
      (byte) 249,
      (byte) 159,
      (byte) 27,
      (byte) 101,
      (byte) 31 /*0x1F*/,
      (byte) 24,
      (byte) 23,
      (byte) 221,
      (byte) 220,
      (byte) 138,
      (byte) 141,
      (byte) 10,
      (byte) 232,
      (byte) 101,
      (byte) 196,
      (byte) 244,
      (byte) 174,
      (byte) 47,
      (byte) 196,
      (byte) 39,
      (byte) 82,
      (byte) 95,
      (byte) 246,
      (byte) 133,
      (byte) 254,
      (byte) 151,
      (byte) 206,
      (byte) 129,
      (byte) 204,
      (byte) 225,
      (byte) 216,
      (byte) 159
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 42;
    sourceArray2[35] = (byte) 236;
    sourceArray2[46] = (byte) 37;
    sourceArray2[24] = (byte) 34;
    sourceArray2[6] = (byte) 81;
    sourceArray2[5] = (byte) 36;
    sourceArray2[32 /*0x20*/] = (byte) 2;
    sourceArray2[16 /*0x10*/] = (byte) 29;
    sourceArray2[7] = (byte) 211;
    sourceArray2[15] = (byte) 215;
    sourceArray2[44] = (byte) 101;
    sourceArray2[2] = (byte) 181;
    sourceArray2[0] = (byte) 142;
    sourceArray2[41] = (byte) 166;
    sourceArray2[31 /*0x1F*/] = (byte) 208 /*0xD0*/;
    sourceArray2[27] = (byte) 206;
    sourceArray2[21] = (byte) 184;
    sourceArray2[38] = (byte) 43;
    sourceArray2[20] = (byte) 169;
    sourceArray2[39] = (byte) 168;
    sourceArray2[1] = (byte) 59;
    sourceArray2[30] = (byte) 187;
    sourceArray2[14] = (byte) 141;
    sourceArray2[23] = (byte) 88;
    sourceArray2[36] = (byte) 184;
    sourceArray2[25] = (byte) 1;
    sourceArray2[34] = (byte) 165;
    sourceArray2[42] = (byte) 61;
    sourceArray2[17] = (byte) 206;
    sourceArray2[13] = (byte) 98;
    sourceArray2[9] = (byte) 111;
    sourceArray2[29] = (byte) 32 /*0x20*/;
    sourceArray2[10] = (byte) 26;
    sourceArray2[33] = (byte) 113;
    sourceArray2[12] = (byte) 139;
    sourceArray2[28] = (byte) 102;
    sourceArray2[43] = (byte) 226;
    sourceArray2[37] = (byte) 107;
    sourceArray2[26] = (byte) 233;
    sourceArray2[45] = (byte) 38;
    sourceArray2[40] = (byte) 244;
    sourceArray2[8] = (byte) 104;
    sourceArray2[18] = (byte) 213;
    sourceArray2[22] = (byte) 215;
    sourceArray2[3] = (byte) 120;
    sourceArray2[4] = (byte) 103;
    sourceArray2[11] = (byte) 198;
    sourceArray2[47] = (byte) 165;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12397()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 120,
        (byte) 107,
        (byte) 135,
        (byte) 64 /*0x40*/,
        (byte) 195,
        (byte) 32 /*0x20*/,
        (byte) 247,
        (byte) 76
      };
      byte[] numArray3 = new byte[8]
      {
        (byte) 253,
        (byte) 19,
        (byte) 87,
        (byte) 225,
        (byte) 62,
        (byte) 28,
        (byte) 136,
        (byte) 152
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[23];
      byte[] response = new byte[23];
      Array.Copy((Array) sc_12395.sspq, 0, (Array) numArray4, 0, 23);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12395.sspr, 0, (Array) numArray4, 0, 23);
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
    byte[] numArray5 = new byte[8];
    byte[] numArray6 = new byte[8];
    numArray6[4] = (byte) 139;
    numArray6[6] = (byte) 196;
    numArray6[2] = (byte) 155;
    numArray6[0] = (byte) 70;
    numArray6[3] = (byte) 62;
    numArray6[5] = (byte) 44;
    numArray6[7] = (byte) 213;
    numArray6[1] = (byte) 102;
    byte[] numArray7 = new byte[8]
    {
      (byte) 34,
      (byte) 65,
      (byte) 27,
      (byte) 15,
      (byte) 13,
      (byte) 13,
      (byte) 20,
      (byte) 70
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12398()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[67];
      byte[] numArray2 = new byte[55]
      {
        (byte) 30,
        (byte) 70,
        (byte) 127 /*0x7F*/,
        (byte) 184,
        (byte) 209,
        (byte) 193,
        (byte) 154,
        (byte) 253,
        (byte) 129,
        (byte) 46,
        (byte) 203,
        (byte) 185,
        (byte) 106,
        (byte) 166,
        (byte) 179,
        (byte) 234,
        (byte) 215,
        (byte) 0,
        (byte) 234,
        (byte) 11,
        (byte) 141,
        (byte) 131,
        (byte) 60,
        (byte) 96 /*0x60*/,
        (byte) 75,
        (byte) 211,
        (byte) 78,
        (byte) 12,
        (byte) 90,
        (byte) 124,
        (byte) 25,
        (byte) 101,
        (byte) 131,
        (byte) 145,
        (byte) 100,
        (byte) 0,
        (byte) 230,
        (byte) 227,
        (byte) 109,
        (byte) 201,
        (byte) 146,
        (byte) 199,
        (byte) 84,
        (byte) 186,
        (byte) 219,
        (byte) 130,
        (byte) 180,
        (byte) 106,
        (byte) 158,
        (byte) 10,
        (byte) 99,
        (byte) 23,
        (byte) 77,
        (byte) 226,
        (byte) 136
      };
      byte[] numArray3 = new byte[55];
      numArray3[47] = (byte) 142;
      numArray3[12] = (byte) 90;
      numArray3[35] = (byte) 119;
      numArray3[41] = (byte) 159;
      numArray3[16 /*0x10*/] = (byte) 99;
      numArray3[3] = (byte) 223;
      numArray3[37] = (byte) 238;
      numArray3[7] = (byte) 254;
      numArray3[20] = (byte) 144 /*0x90*/;
      numArray3[50] = (byte) 21;
      numArray3[1] = (byte) 13;
      numArray3[2] = (byte) 254;
      numArray3[40] = (byte) 97;
      numArray3[13] = (byte) 32 /*0x20*/;
      numArray3[25] = (byte) 18;
      numArray3[6] = (byte) 56;
      numArray3[0] = (byte) 184;
      numArray3[17] = (byte) 41;
      numArray3[11] = (byte) 121;
      numArray3[19] = (byte) 216;
      numArray3[26] = (byte) 117;
      numArray3[21] = (byte) 225;
      numArray3[22] = (byte) 252;
      numArray3[15] = (byte) 95;
      numArray3[24] = (byte) 145;
      numArray3[9] = (byte) 226;
      numArray3[36] = (byte) 31 /*0x1F*/;
      numArray3[27] = (byte) 120;
      numArray3[28] = (byte) 219;
      numArray3[29] = (byte) 141;
      numArray3[14] = (byte) 238;
      numArray3[31 /*0x1F*/] = (byte) 124;
      numArray3[32 /*0x20*/] = (byte) 161;
      numArray3[33] = (byte) 115;
      numArray3[34] = (byte) 162;
      numArray3[45] = (byte) 159;
      numArray3[52] = (byte) 148;
      numArray3[23] = (byte) 54;
      numArray3[18] = (byte) 9;
      numArray3[38] = (byte) 185;
      numArray3[30] = (byte) 59;
      numArray3[4] = (byte) 76;
      numArray3[8] = (byte) 184;
      numArray3[43] = (byte) 113;
      numArray3[44] = (byte) 63 /*0x3F*/;
      numArray3[5] = (byte) 71;
      numArray3[46] = (byte) 203;
      numArray3[39] = (byte) 222;
      numArray3[48 /*0x30*/] = (byte) 41;
      numArray3[49] = (byte) 140;
      numArray3[42] = (byte) 184;
      numArray3[51] = (byte) 15;
      numArray3[10] = (byte) 207;
      numArray3[53] = (byte) 83;
      numArray3[54] = (byte) 8;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 55);
      for (int index = 0; index < 55; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12]
      {
        (byte) 155,
        (byte) 163,
        (byte) 222,
        (byte) 142,
        (byte) 32 /*0x20*/,
        (byte) 82,
        (byte) 236,
        (byte) 18,
        (byte) 79,
        (byte) 148,
        (byte) 208 /*0xD0*/,
        (byte) 120
      };
      byte[] numArray5 = new byte[12]
      {
        (byte) 195,
        (byte) 144 /*0x90*/,
        (byte) 237,
        (byte) 194,
        (byte) 104,
        (byte) 246,
        (byte) 112 /*0x70*/,
        (byte) 200,
        (byte) 206,
        (byte) 37,
        (byte) 226,
        (byte) 240 /*0xF0*/
      };
      key.Query(true, 335, numArray4, numArray4);
      Array.Copy((Array) numArray4, 0, (Array) numArray1, 55, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index + 55] ^= numArray5[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray6 = new byte[67];
    byte[] numArray7 = new byte[55]
    {
      (byte) 35,
      (byte) 201,
      (byte) 117,
      (byte) 162,
      (byte) 224 /*0xE0*/,
      (byte) 219,
      (byte) 15,
      (byte) 124,
      (byte) 68,
      (byte) 188,
      (byte) 131,
      (byte) 74,
      (byte) 169,
      (byte) 126,
      (byte) 234,
      (byte) 56,
      (byte) 69,
      (byte) 95,
      (byte) 110,
      (byte) 43,
      (byte) 211,
      (byte) 145,
      (byte) 200,
      (byte) 227,
      (byte) 209,
      (byte) 98,
      (byte) 147,
      (byte) 38,
      (byte) 184,
      (byte) 10,
      (byte) 175,
      (byte) 46,
      (byte) 200,
      (byte) 241,
      (byte) 95,
      (byte) 100,
      (byte) 243,
      (byte) 32 /*0x20*/,
      (byte) 137,
      (byte) 172,
      (byte) 140,
      (byte) 39,
      (byte) 154,
      (byte) 210,
      (byte) 157,
      (byte) 8,
      (byte) 66,
      (byte) 147,
      (byte) 100,
      (byte) 92,
      (byte) 139,
      (byte) 25,
      (byte) 77,
      (byte) 18,
      (byte) 170
    };
    byte[] numArray8 = new byte[55]
    {
      (byte) 119,
      (byte) 21,
      (byte) 119,
      (byte) 254,
      (byte) 114,
      (byte) 151,
      (byte) 223,
      (byte) 242,
      (byte) 247,
      (byte) 170,
      (byte) 133,
      (byte) 215,
      (byte) 249,
      (byte) 65,
      (byte) 38,
      (byte) 86,
      (byte) 219,
      (byte) 105,
      (byte) 178,
      (byte) 82,
      (byte) 62,
      (byte) 79,
      (byte) 224 /*0xE0*/,
      (byte) 134,
      (byte) 119,
      (byte) 188,
      (byte) 215,
      (byte) 32 /*0x20*/,
      (byte) 223,
      (byte) 85,
      (byte) 232,
      (byte) 245,
      (byte) 254,
      (byte) 54,
      (byte) 107,
      (byte) 107,
      (byte) 69,
      (byte) 181,
      (byte) 165,
      (byte) 12,
      (byte) 88,
      (byte) 124,
      (byte) 47,
      (byte) 24,
      (byte) 56,
      (byte) 173,
      (byte) 209,
      (byte) 247,
      (byte) 214,
      (byte) 128 /*0x80*/,
      (byte) 131,
      (byte) 229,
      (byte) 207,
      (byte) 121,
      (byte) 83
    };
    key.Query(true, 335, numArray7, numArray7);
    Array.Copy((Array) numArray7, 0, (Array) numArray6, 0, 55);
    for (int index = 0; index < 55; ++index)
      numArray6[index] ^= numArray8[index];
    byte[] numArray9 = new byte[12];
    numArray9[10] = (byte) 184;
    numArray9[1] = (byte) 209;
    numArray9[2] = (byte) 126;
    numArray9[3] = (byte) 72;
    numArray9[4] = (byte) 232;
    numArray9[0] = (byte) 145;
    numArray9[5] = (byte) 195;
    numArray9[7] = (byte) 13;
    numArray9[11] = (byte) 31 /*0x1F*/;
    numArray9[6] = (byte) 57;
    numArray9[8] = (byte) 84;
    numArray9[9] = (byte) 130;
    byte[] numArray10 = new byte[12]
    {
      (byte) 133,
      (byte) 7,
      (byte) 107,
      (byte) 90,
      (byte) 249,
      (byte) 249,
      (byte) 155,
      (byte) 164,
      (byte) 96 /*0x60*/,
      (byte) 215,
      (byte) 171,
      (byte) 180
    };
    key.Query(true, 335, numArray9, numArray9);
    Array.Copy((Array) numArray9, 0, (Array) numArray6, 55, 12);
    for (int index = 0; index < 12; ++index)
      numArray6[index + 55] ^= numArray10[index];
    return Encoding.UTF8.GetString(numArray6);
  }

  internal static string ssp_appserver_12399()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 150,
        (byte) 224 /*0xE0*/,
        (byte) 182,
        (byte) 70,
        (byte) 205,
        (byte) 100,
        (byte) 98,
        (byte) 79,
        (byte) 11,
        (byte) 99,
        (byte) 163,
        (byte) 100,
        (byte) 96 /*0x60*/,
        (byte) 196,
        (byte) 72,
        (byte) 134,
        (byte) 138,
        (byte) 116,
        (byte) 223,
        (byte) 23,
        (byte) 101,
        (byte) 67,
        (byte) 139,
        (byte) 115,
        (byte) 254,
        (byte) 154,
        (byte) 9
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 187,
        (byte) 120,
        (byte) 212,
        (byte) 210,
        (byte) 183,
        (byte) 32 /*0x20*/,
        (byte) 73,
        (byte) 39,
        (byte) 48 /*0x30*/,
        (byte) 55,
        (byte) 73,
        (byte) 127 /*0x7F*/,
        (byte) 40,
        (byte) 64 /*0x40*/,
        (byte) 141,
        (byte) 198,
        (byte) 52,
        (byte) 44,
        (byte) 113,
        (byte) 218,
        (byte) 110,
        (byte) 229,
        (byte) 15,
        (byte) 46,
        (byte) 188,
        (byte) 93,
        (byte) 137
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27];
    numArray5[5] = (byte) 115;
    numArray5[1] = (byte) 39;
    numArray5[2] = (byte) 103;
    numArray5[3] = (byte) 186;
    numArray5[15] = (byte) 71;
    numArray5[14] = (byte) 251;
    numArray5[6] = (byte) 204;
    numArray5[23] = (byte) 68;
    numArray5[26] = (byte) 85;
    numArray5[22] = (byte) 225;
    numArray5[10] = (byte) 225;
    numArray5[4] = (byte) 118;
    numArray5[12] = (byte) 202;
    numArray5[24] = (byte) 25;
    numArray5[11] = (byte) 214;
    numArray5[8] = (byte) 81;
    numArray5[7] = (byte) 113;
    numArray5[17] = (byte) 49;
    numArray5[13] = (byte) 94;
    numArray5[19] = (byte) 66;
    numArray5[20] = (byte) 149;
    numArray5[21] = (byte) 138;
    numArray5[0] = (byte) 72;
    numArray5[9] = (byte) 33;
    numArray5[16 /*0x10*/] = (byte) 151;
    numArray5[25] = (byte) 82;
    numArray5[18] = (byte) 254;
    byte[] numArray6 = new byte[27];
    numArray6[14] = (byte) 226;
    numArray6[1] = (byte) 177;
    numArray6[4] = (byte) 123;
    numArray6[15] = (byte) 214;
    numArray6[0] = (byte) 236;
    numArray6[10] = (byte) 126;
    numArray6[22] = (byte) 121;
    numArray6[9] = (byte) 29;
    numArray6[8] = (byte) 55;
    numArray6[3] = (byte) 23;
    numArray6[17] = (byte) 135;
    numArray6[11] = (byte) 58;
    numArray6[12] = (byte) 173;
    numArray6[13] = (byte) 73;
    numArray6[26] = (byte) 11;
    numArray6[7] = (byte) 76;
    numArray6[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    numArray6[5] = (byte) 124;
    numArray6[18] = (byte) 176 /*0xB0*/;
    numArray6[19] = (byte) 84;
    numArray6[20] = (byte) 103;
    numArray6[21] = (byte) 102;
    numArray6[2] = (byte) 246;
    numArray6[23] = (byte) 12;
    numArray6[24] = (byte) 231;
    numArray6[25] = (byte) 198;
    numArray6[6] = (byte) 101;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12400(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[0] = (byte) 204;
    sourceArray1[1] = (byte) 129;
    sourceArray1[2] = (byte) 227;
    sourceArray1[35] = (byte) 14;
    sourceArray1[4] = (byte) 53;
    sourceArray1[15] = (byte) 91;
    sourceArray1[21] = (byte) 33;
    sourceArray1[7] = (byte) 41;
    sourceArray1[8] = (byte) 181;
    sourceArray1[10] = (byte) 238;
    sourceArray1[27] = (byte) 24;
    sourceArray1[3] = (byte) 93;
    sourceArray1[46] = (byte) 45;
    sourceArray1[13] = (byte) 109;
    sourceArray1[37] = (byte) 229;
    sourceArray1[43] = (byte) 87;
    sourceArray1[17] = (byte) 248;
    sourceArray1[42] = (byte) 116;
    sourceArray1[41] = (byte) 186;
    sourceArray1[19] = (byte) 17;
    sourceArray1[20] = (byte) 243;
    sourceArray1[18] = (byte) 108;
    sourceArray1[33] = (byte) 67;
    sourceArray1[23] = (byte) 75;
    sourceArray1[24] = (byte) 85;
    sourceArray1[25] = (byte) 33;
    sourceArray1[26] = (byte) 20;
    sourceArray1[22] = (byte) 61;
    sourceArray1[16 /*0x10*/] = (byte) 232;
    sourceArray1[12] = (byte) 246;
    sourceArray1[14] = (byte) 0;
    sourceArray1[29] = (byte) 245;
    sourceArray1[47] = (byte) 161;
    sourceArray1[31 /*0x1F*/] = (byte) 153;
    sourceArray1[34] = (byte) 252;
    sourceArray1[36] = (byte) 57;
    sourceArray1[32 /*0x20*/] = (byte) 240 /*0xF0*/;
    sourceArray1[40] = (byte) 237;
    sourceArray1[38] = (byte) 74;
    sourceArray1[28] = (byte) 75;
    sourceArray1[5] = (byte) 218;
    sourceArray1[9] = (byte) 48 /*0x30*/;
    sourceArray1[39] = (byte) 112 /*0x70*/;
    sourceArray1[11] = (byte) 3;
    sourceArray1[44] = (byte) 143;
    sourceArray1[45] = (byte) 100;
    sourceArray1[30] = (byte) 190;
    sourceArray1[6] = (byte) 3;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[34] = (byte) 146;
    sourceArray2[46] = (byte) 222;
    sourceArray2[6] = (byte) 221;
    sourceArray2[43] = (byte) 121;
    sourceArray2[13] = (byte) 190;
    sourceArray2[5] = (byte) 36;
    sourceArray2[30] = (byte) 131;
    sourceArray2[3] = (byte) 19;
    sourceArray2[8] = (byte) 23;
    sourceArray2[44] = (byte) 207;
    sourceArray2[38] = (byte) 225;
    sourceArray2[35] = (byte) 233;
    sourceArray2[12] = (byte) 158;
    sourceArray2[42] = (byte) 21;
    sourceArray2[15] = (byte) 146;
    sourceArray2[45] = (byte) 150;
    sourceArray2[16 /*0x10*/] = (byte) 92;
    sourceArray2[17] = (byte) 213;
    sourceArray2[18] = (byte) 118;
    sourceArray2[19] = (byte) 80 /*0x50*/;
    sourceArray2[20] = (byte) 227;
    sourceArray2[21] = (byte) 248;
    sourceArray2[22] = (byte) 243;
    sourceArray2[9] = (byte) 239;
    sourceArray2[28] = (byte) 18;
    sourceArray2[25] = (byte) 134;
    sourceArray2[10] = (byte) 142;
    sourceArray2[2] = (byte) 23;
    sourceArray2[23] = (byte) 74;
    sourceArray2[29] = (byte) 219;
    sourceArray2[26] = (byte) 196;
    sourceArray2[31 /*0x1F*/] = (byte) 12;
    sourceArray2[32 /*0x20*/] = (byte) 93;
    sourceArray2[27] = (byte) 145;
    sourceArray2[11] = (byte) 131;
    sourceArray2[14] = (byte) 26;
    sourceArray2[36] = (byte) 228;
    sourceArray2[37] = (byte) 36;
    sourceArray2[33] = (byte) 141;
    sourceArray2[39] = (byte) 207;
    sourceArray2[40] = (byte) 71;
    sourceArray2[41] = (byte) 134;
    sourceArray2[0] = (byte) 222;
    sourceArray2[1] = (byte) 83;
    sourceArray2[4] = (byte) 235;
    sourceArray2[7] = (byte) 42;
    sourceArray2[24] = (byte) 63 /*0x3F*/;
    sourceArray2[47] = (byte) 24;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12401()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 21,
        (byte) 87,
        (byte) 166,
        (byte) 205,
        (byte) 167,
        (byte) 93,
        (byte) 89,
        (byte) 17,
        (byte) 124,
        (byte) 16 /*0x10*/,
        (byte) 110,
        (byte) 218,
        (byte) 23,
        (byte) 173,
        (byte) 213,
        (byte) 216,
        (byte) 249,
        (byte) 5,
        (byte) 91,
        (byte) 90,
        (byte) 120,
        (byte) 167,
        (byte) 16 /*0x10*/,
        (byte) 98,
        (byte) 155,
        (byte) 147,
        (byte) 193
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 137,
        (byte) 92,
        (byte) 184,
        (byte) 135,
        (byte) 98,
        (byte) 55,
        (byte) 189,
        (byte) 123,
        (byte) 95,
        (byte) 230,
        (byte) 253,
        (byte) 176 /*0xB0*/,
        (byte) 93,
        (byte) 148,
        (byte) 23,
        (byte) 84,
        (byte) 112 /*0x70*/,
        (byte) 158,
        (byte) 162,
        (byte) 137,
        (byte) 40,
        (byte) 5,
        (byte) 174,
        (byte) 247,
        (byte) 123,
        (byte) 83,
        (byte) 169
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 206,
      (byte) 186,
      (byte) 187,
      (byte) 95,
      (byte) 97,
      (byte) 250,
      (byte) 122,
      (byte) 143,
      (byte) 81,
      (byte) 164,
      (byte) 40,
      (byte) 141,
      (byte) 251,
      (byte) 50,
      (byte) 139,
      (byte) 153,
      (byte) 96 /*0x60*/,
      (byte) 24,
      (byte) 119,
      (byte) 0,
      (byte) 100,
      (byte) 76,
      (byte) 18,
      (byte) 107,
      (byte) 78,
      (byte) 110,
      (byte) 71
    };
    byte[] numArray6 = new byte[27];
    numArray6[16 /*0x10*/] = (byte) 76;
    numArray6[1] = (byte) 237;
    numArray6[24] = (byte) 226;
    numArray6[11] = (byte) 235;
    numArray6[4] = (byte) 154;
    numArray6[21] = (byte) 180;
    numArray6[0] = (byte) 204;
    numArray6[23] = (byte) 27;
    numArray6[8] = (byte) 175;
    numArray6[9] = (byte) 254;
    numArray6[20] = (byte) 233;
    numArray6[19] = (byte) 179;
    numArray6[12] = (byte) 187;
    numArray6[13] = (byte) 122;
    numArray6[5] = (byte) 192 /*0xC0*/;
    numArray6[14] = (byte) 62;
    numArray6[22] = (byte) 196;
    numArray6[17] = (byte) 72;
    numArray6[18] = (byte) 127 /*0x7F*/;
    numArray6[6] = (byte) 159;
    numArray6[15] = (byte) 102;
    numArray6[2] = (byte) 195;
    numArray6[3] = (byte) 12;
    numArray6[10] = (byte) 186;
    numArray6[7] = (byte) 24;
    numArray6[25] = (byte) 76;
    numArray6[26] = (byte) 59;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[22];
    byte[] response = new byte[22];
    Array.Copy((Array) sc_12395.sspq, 23, (Array) numArray7, 0, 22);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12395.sspr, 23, (Array) numArray7, 0, 22);
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

  internal static string ssp_appserver_12402()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[37];
      byte[] numArray2 = new byte[37]
      {
        (byte) 104,
        (byte) 109,
        (byte) 47,
        (byte) 224 /*0xE0*/,
        (byte) 242,
        (byte) 172,
        (byte) 67,
        (byte) 161,
        (byte) 192 /*0xC0*/,
        (byte) 11,
        (byte) 206,
        (byte) 166,
        (byte) 88,
        (byte) 39,
        (byte) 38,
        (byte) 12,
        (byte) 118,
        (byte) 173,
        (byte) 208 /*0xD0*/,
        (byte) 6,
        (byte) 140,
        (byte) 238,
        (byte) 254,
        (byte) 136,
        (byte) 138,
        (byte) 220,
        (byte) 126,
        (byte) 195,
        (byte) 65,
        (byte) 232,
        (byte) 76,
        (byte) 22,
        (byte) 173,
        (byte) 48 /*0x30*/,
        (byte) 74,
        (byte) 201,
        (byte) 46
      };
      byte[] numArray3 = new byte[37];
      numArray3[30] = (byte) 184;
      numArray3[10] = (byte) 32 /*0x20*/;
      numArray3[2] = (byte) 146;
      numArray3[16 /*0x10*/] = (byte) 126;
      numArray3[3] = (byte) 226;
      numArray3[32 /*0x20*/] = (byte) 86;
      numArray3[6] = (byte) 240 /*0xF0*/;
      numArray3[4] = (byte) 130;
      numArray3[8] = (byte) 207;
      numArray3[9] = (byte) 204;
      numArray3[26] = (byte) 177;
      numArray3[11] = (byte) 170;
      numArray3[19] = (byte) 25;
      numArray3[22] = (byte) 141;
      numArray3[14] = (byte) 134;
      numArray3[15] = (byte) 95;
      numArray3[12] = (byte) 59;
      numArray3[21] = (byte) 90;
      numArray3[18] = (byte) 141;
      numArray3[34] = (byte) 148;
      numArray3[20] = (byte) 66;
      numArray3[17] = (byte) 240 /*0xF0*/;
      numArray3[29] = (byte) 190;
      numArray3[23] = (byte) 99;
      numArray3[24] = (byte) 217;
      numArray3[25] = (byte) 158;
      numArray3[0] = (byte) 222;
      numArray3[27] = (byte) 108;
      numArray3[28] = (byte) 175;
      numArray3[35] = (byte) 29;
      numArray3[13] = (byte) 230;
      numArray3[5] = (byte) 193;
      numArray3[7] = (byte) 217;
      numArray3[33] = (byte) 151;
      numArray3[31 /*0x1F*/] = (byte) 88;
      numArray3[1] = (byte) 53;
      numArray3[36] = (byte) 230;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 37);
      for (int index = 0; index < 37; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[37];
    byte[] numArray5 = new byte[37];
    numArray5[13] = (byte) 89;
    numArray5[8] = (byte) 158;
    numArray5[2] = (byte) 136;
    numArray5[32 /*0x20*/] = (byte) 39;
    numArray5[16 /*0x10*/] = (byte) 1;
    numArray5[29] = (byte) 23;
    numArray5[3] = (byte) 105;
    numArray5[15] = (byte) 7;
    numArray5[7] = (byte) 198;
    numArray5[9] = (byte) 173;
    numArray5[10] = (byte) 80 /*0x50*/;
    numArray5[11] = (byte) 250;
    numArray5[18] = (byte) 31 /*0x1F*/;
    numArray5[22] = (byte) 242;
    numArray5[14] = (byte) 135;
    numArray5[6] = (byte) 84;
    numArray5[0] = (byte) 50;
    numArray5[17] = (byte) 134;
    numArray5[12] = (byte) 207;
    numArray5[19] = (byte) 30;
    numArray5[20] = (byte) 87;
    numArray5[21] = (byte) 12;
    numArray5[28] = (byte) 9;
    numArray5[23] = (byte) 8;
    numArray5[24] = (byte) 74;
    numArray5[4] = (byte) 9;
    numArray5[5] = (byte) 71;
    numArray5[27] = (byte) 14;
    numArray5[1] = (byte) 221;
    numArray5[25] = (byte) 151;
    numArray5[30] = (byte) 77;
    numArray5[31 /*0x1F*/] = (byte) 246;
    numArray5[26] = (byte) 30;
    numArray5[33] = (byte) 187;
    numArray5[34] = (byte) 60;
    numArray5[35] = (byte) 200;
    numArray5[36] = (byte) 204;
    byte[] numArray6 = new byte[37]
    {
      (byte) 138,
      (byte) 136,
      (byte) 69,
      (byte) 96 /*0x60*/,
      (byte) 143,
      (byte) 3,
      (byte) 49,
      (byte) 74,
      (byte) 194,
      (byte) 94,
      (byte) 6,
      (byte) 129,
      (byte) 121,
      (byte) 5,
      (byte) 129,
      (byte) 216,
      (byte) 222,
      (byte) 207,
      (byte) 214,
      (byte) 176 /*0xB0*/,
      (byte) 82,
      (byte) 107,
      (byte) 118,
      (byte) 93,
      (byte) 219,
      (byte) 180,
      (byte) 83,
      (byte) 6,
      (byte) 184,
      (byte) 243,
      (byte) 90,
      (byte) 95,
      (byte) 62,
      (byte) 155,
      (byte) 34,
      (byte) 196,
      (byte) 154
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 37);
    for (int index = 0; index < 37; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12403()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27];
      numArray2[13] = (byte) 76;
      numArray2[0] = (byte) 95;
      numArray2[2] = (byte) 137;
      numArray2[26] = (byte) 213;
      numArray2[6] = (byte) 129;
      numArray2[5] = (byte) 249;
      numArray2[12] = (byte) 221;
      numArray2[7] = (byte) 183;
      numArray2[8] = (byte) 175;
      numArray2[25] = (byte) 151;
      numArray2[10] = (byte) 83;
      numArray2[4] = (byte) 183;
      numArray2[22] = (byte) 90;
      numArray2[9] = (byte) 54;
      numArray2[14] = (byte) 141;
      numArray2[19] = (byte) 153;
      numArray2[16 /*0x10*/] = (byte) 15;
      numArray2[17] = (byte) 197;
      numArray2[11] = (byte) 22;
      numArray2[18] = (byte) 84;
      numArray2[20] = (byte) 169;
      numArray2[21] = (byte) 79;
      numArray2[1] = (byte) 29;
      numArray2[23] = (byte) 249;
      numArray2[24] = (byte) 230;
      numArray2[3] = (byte) 26;
      numArray2[15] = (byte) 177;
      byte[] numArray3 = new byte[27]
      {
        (byte) 196,
        (byte) 216,
        (byte) 132,
        (byte) 222,
        (byte) 56,
        (byte) 148,
        (byte) 233,
        (byte) 173,
        (byte) 138,
        (byte) 72,
        (byte) 6,
        (byte) 76,
        (byte) 172,
        (byte) 195,
        (byte) 21,
        (byte) 236,
        (byte) 119,
        (byte) 217,
        (byte) 47,
        byte.MaxValue,
        (byte) 90,
        (byte) 69,
        (byte) 56,
        (byte) 85,
        (byte) 131,
        (byte) 229,
        (byte) 66
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[37];
      byte[] response = new byte[37];
      Array.Copy((Array) sc_12395.sspq, 45, (Array) numArray4, 0, 37);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12395.sspr, 45, (Array) numArray4, 0, 37);
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
    byte[] numArray5 = new byte[27];
    byte[] numArray6 = new byte[27];
    numArray6[15] = (byte) 107;
    numArray6[19] = (byte) 105;
    numArray6[0] = (byte) 197;
    numArray6[3] = (byte) 68;
    numArray6[12] = (byte) 95;
    numArray6[5] = (byte) 42;
    numArray6[11] = (byte) 21;
    numArray6[7] = (byte) 193;
    numArray6[8] = (byte) 60;
    numArray6[13] = (byte) 189;
    numArray6[10] = (byte) 214;
    numArray6[16 /*0x10*/] = (byte) 5;
    numArray6[2] = (byte) 91;
    numArray6[24] = (byte) 199;
    numArray6[20] = (byte) 60;
    numArray6[26] = (byte) 243;
    numArray6[4] = (byte) 141;
    numArray6[17] = (byte) 87;
    numArray6[1] = (byte) 36;
    numArray6[14] = (byte) 63 /*0x3F*/;
    numArray6[6] = (byte) 6;
    numArray6[21] = (byte) 199;
    numArray6[22] = (byte) 2;
    numArray6[23] = (byte) 198;
    numArray6[18] = (byte) 84;
    numArray6[25] = (byte) 2;
    numArray6[9] = (byte) 231;
    byte[] numArray7 = new byte[27];
    numArray7[15] = (byte) 196;
    numArray7[1] = (byte) 249;
    numArray7[11] = (byte) 49;
    numArray7[3] = (byte) 11;
    numArray7[10] = (byte) 85;
    numArray7[7] = (byte) 5;
    numArray7[6] = (byte) 151;
    numArray7[4] = (byte) 179;
    numArray7[21] = (byte) 164;
    numArray7[9] = byte.MaxValue;
    numArray7[22] = (byte) 99;
    numArray7[24] = (byte) 111;
    numArray7[13] = (byte) 205;
    numArray7[19] = (byte) 67;
    numArray7[14] = (byte) 215;
    numArray7[12] = (byte) 43;
    numArray7[5] = (byte) 21;
    numArray7[17] = (byte) 46;
    numArray7[18] = (byte) 222;
    numArray7[16 /*0x10*/] = (byte) 233;
    numArray7[20] = (byte) 3;
    numArray7[26] = (byte) 46;
    numArray7[23] = (byte) 146;
    numArray7[8] = (byte) 117;
    numArray7[0] = (byte) 89;
    numArray7[25] = (byte) 121;
    numArray7[2] = (byte) 83;
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[42];
    byte[] response1 = new byte[42];
    Array.Copy((Array) sc_12395.sspq, 82, (Array) numArray8, 0, 42);
    key.Query(true, 335, numArray8, response1);
    Array.Copy((Array) sc_12395.sspr, 82, (Array) numArray8, 0, 42);
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

  internal static string ssp_appserver_12404()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[27];
      byte[] numArray2 = new byte[27]
      {
        (byte) 31 /*0x1F*/,
        (byte) 249,
        (byte) 150,
        (byte) 63 /*0x3F*/,
        (byte) 65,
        (byte) 20,
        (byte) 30,
        (byte) 206,
        (byte) 140,
        (byte) 116,
        (byte) 247,
        (byte) 104,
        (byte) 29,
        (byte) 182,
        (byte) 117,
        (byte) 65,
        (byte) 95,
        (byte) 254,
        (byte) 50,
        (byte) 202,
        (byte) 196,
        (byte) 17,
        (byte) 102,
        (byte) 82,
        (byte) 111,
        (byte) 31 /*0x1F*/,
        (byte) 238
      };
      byte[] numArray3 = new byte[27]
      {
        (byte) 24,
        (byte) 186,
        (byte) 223,
        (byte) 107,
        (byte) 149,
        (byte) 104,
        (byte) 242,
        (byte) 250,
        (byte) 221,
        (byte) 208 /*0xD0*/,
        (byte) 80 /*0x50*/,
        (byte) 38,
        (byte) 49,
        (byte) 225,
        (byte) 101,
        (byte) 10,
        (byte) 248,
        (byte) 134,
        (byte) 170,
        (byte) 93,
        (byte) 192 /*0xC0*/,
        (byte) 27,
        (byte) 185,
        (byte) 134,
        (byte) 29,
        (byte) 240 /*0xF0*/,
        (byte) 126
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 27);
      for (int index = 0; index < 27; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[27];
    byte[] numArray5 = new byte[27]
    {
      (byte) 118,
      (byte) 16 /*0x10*/,
      (byte) 25,
      (byte) 196,
      (byte) 200,
      (byte) 83,
      (byte) 182,
      (byte) 50,
      (byte) 121,
      (byte) 108,
      (byte) 18,
      (byte) 140,
      (byte) 224 /*0xE0*/,
      (byte) 230,
      (byte) 54,
      (byte) 115,
      (byte) 231,
      (byte) 91,
      (byte) 21,
      (byte) 157,
      (byte) 191,
      (byte) 170,
      (byte) 61,
      (byte) 199,
      (byte) 201,
      (byte) 13,
      (byte) 147
    };
    byte[] numArray6 = new byte[27];
    numArray6[5] = (byte) 218;
    numArray6[3] = (byte) 22;
    numArray6[2] = (byte) 14;
    numArray6[13] = (byte) 27;
    numArray6[4] = (byte) 127 /*0x7F*/;
    numArray6[15] = (byte) 165;
    numArray6[6] = (byte) 72;
    numArray6[0] = (byte) 202;
    numArray6[10] = (byte) 222;
    numArray6[9] = byte.MaxValue;
    numArray6[16 /*0x10*/] = (byte) 186;
    numArray6[18] = (byte) 82;
    numArray6[12] = (byte) 139;
    numArray6[23] = (byte) 47;
    numArray6[22] = (byte) 90;
    numArray6[14] = (byte) 106;
    numArray6[25] = (byte) 178;
    numArray6[17] = (byte) 104;
    numArray6[26] = (byte) 117;
    numArray6[19] = (byte) 168;
    numArray6[20] = (byte) 240 /*0xF0*/;
    numArray6[21] = (byte) 127 /*0x7F*/;
    numArray6[1] = (byte) 222;
    numArray6[24] = (byte) 31 /*0x1F*/;
    numArray6[11] = (byte) 194;
    numArray6[8] = (byte) 147;
    numArray6[7] = (byte) 247;
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 27);
    for (int index = 0; index < 27; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_appserver_12405()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 251,
        (byte) 187,
        (byte) 192 /*0xC0*/,
        (byte) 113,
        (byte) 98,
        (byte) 87,
        (byte) 69,
        (byte) 149,
        (byte) 66,
        (byte) 113,
        (byte) 232,
        (byte) 79,
        (byte) 36,
        (byte) 142,
        (byte) 109,
        (byte) 242,
        (byte) 18,
        (byte) 146,
        (byte) 37,
        (byte) 119
      };
      byte[] numArray3 = new byte[20];
      numArray3[9] = (byte) 65;
      numArray3[18] = (byte) 199;
      numArray3[11] = (byte) 217;
      numArray3[3] = (byte) 142;
      numArray3[15] = (byte) 207;
      numArray3[5] = (byte) 207;
      numArray3[0] = (byte) 214;
      numArray3[7] = (byte) 161;
      numArray3[16 /*0x10*/] = (byte) 174;
      numArray3[6] = (byte) 7;
      numArray3[10] = (byte) 226;
      numArray3[2] = (byte) 139;
      numArray3[12] = (byte) 11;
      numArray3[13] = (byte) 197;
      numArray3[14] = (byte) 229;
      numArray3[8] = (byte) 83;
      numArray3[1] = (byte) 186;
      numArray3[17] = (byte) 236;
      numArray3[4] = (byte) 26;
      numArray3[19] = byte.MaxValue;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[13] = (byte) 222;
    numArray5[11] = (byte) 65;
    numArray5[2] = (byte) 204;
    numArray5[3] = (byte) 215;
    numArray5[4] = (byte) 170;
    numArray5[5] = (byte) 37;
    numArray5[6] = (byte) 154;
    numArray5[0] = (byte) 192 /*0xC0*/;
    numArray5[14] = (byte) 172;
    numArray5[16 /*0x10*/] = (byte) 142;
    numArray5[18] = (byte) 177;
    numArray5[12] = (byte) 106;
    numArray5[8] = (byte) 203;
    numArray5[1] = (byte) 151;
    numArray5[7] = (byte) 212;
    numArray5[15] = (byte) 73;
    numArray5[10] = (byte) 90;
    numArray5[17] = (byte) 110;
    numArray5[9] = (byte) 167;
    numArray5[19] = (byte) 166;
    byte[] numArray6 = new byte[20]
    {
      (byte) 186,
      (byte) 244,
      (byte) 7,
      (byte) 151,
      (byte) 142,
      (byte) 90,
      (byte) 159,
      (byte) 130,
      (byte) 22,
      (byte) 245,
      (byte) 184,
      (byte) 95,
      (byte) 243,
      (byte) 63 /*0x3F*/,
      (byte) 146,
      (byte) 235,
      (byte) 159,
      (byte) 227,
      (byte) 4,
      (byte) 15
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_appserver_12406(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[10] = (byte) 96 /*0x60*/;
    sourceArray1[1] = (byte) 243;
    sourceArray1[22] = (byte) 207;
    sourceArray1[3] = (byte) 181;
    sourceArray1[20] = (byte) 100;
    sourceArray1[27] = (byte) 48 /*0x30*/;
    sourceArray1[0] = (byte) 188;
    sourceArray1[6] = (byte) 190;
    sourceArray1[11] = (byte) 225;
    sourceArray1[30] = (byte) 81;
    sourceArray1[26] = (byte) 145;
    sourceArray1[16 /*0x10*/] = (byte) 10;
    sourceArray1[41] = (byte) 39;
    sourceArray1[13] = (byte) 176 /*0xB0*/;
    sourceArray1[39] = (byte) 139;
    sourceArray1[35] = (byte) 60;
    sourceArray1[4] = (byte) 207;
    sourceArray1[17] = (byte) 218;
    sourceArray1[32 /*0x20*/] = (byte) 43;
    sourceArray1[19] = (byte) 175;
    sourceArray1[29] = (byte) 81;
    sourceArray1[21] = (byte) 84;
    sourceArray1[9] = (byte) 22;
    sourceArray1[43] = (byte) 132;
    sourceArray1[24] = (byte) 49;
    sourceArray1[2] = (byte) 83;
    sourceArray1[8] = (byte) 124;
    sourceArray1[45] = (byte) 154;
    sourceArray1[7] = (byte) 207;
    sourceArray1[25] = (byte) 236;
    sourceArray1[28] = (byte) 66;
    sourceArray1[31 /*0x1F*/] = (byte) 26;
    sourceArray1[15] = (byte) 234;
    sourceArray1[33] = (byte) 129;
    sourceArray1[47] = (byte) 75;
    sourceArray1[23] = (byte) 194;
    sourceArray1[36] = (byte) 230;
    sourceArray1[37] = (byte) 217;
    sourceArray1[14] = (byte) 121;
    sourceArray1[5] = (byte) 60;
    sourceArray1[40] = (byte) 56;
    sourceArray1[34] = (byte) 105;
    sourceArray1[42] = (byte) 113;
    sourceArray1[12] = (byte) 145;
    sourceArray1[44] = (byte) 187;
    sourceArray1[18] = (byte) 3;
    sourceArray1[46] = (byte) 203;
    sourceArray1[38] = (byte) 41;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 129,
      (byte) 4,
      (byte) 7,
      (byte) 102,
      (byte) 118,
      (byte) 156,
      (byte) 88,
      (byte) 29,
      (byte) 4,
      (byte) 60,
      (byte) 143,
      (byte) 32 /*0x20*/,
      (byte) 125,
      (byte) 145,
      (byte) 234,
      (byte) 113,
      (byte) 221,
      (byte) 13,
      (byte) 89,
      (byte) 193,
      (byte) 163,
      (byte) 68,
      (byte) 176 /*0xB0*/,
      (byte) 251,
      (byte) 33,
      (byte) 224 /*0xE0*/,
      (byte) 238,
      (byte) 17,
      (byte) 104,
      (byte) 146,
      (byte) 119,
      (byte) 213,
      (byte) 250,
      (byte) 195,
      (byte) 191,
      (byte) 207,
      (byte) 240 /*0xF0*/,
      (byte) 135,
      (byte) 248,
      (byte) 66,
      (byte) 117,
      (byte) 122,
      (byte) 168,
      (byte) 194,
      (byte) 82,
      (byte) 24,
      (byte) 148,
      (byte) 204
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_appserver_12407()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[36];
      byte[] numArray2 = new byte[36];
      numArray2[21] = (byte) 101;
      numArray2[1] = (byte) 187;
      numArray2[22] = (byte) 228;
      numArray2[3] = (byte) 136;
      numArray2[8] = (byte) 100;
      numArray2[5] = (byte) 25;
      numArray2[6] = (byte) 3;
      numArray2[24] = (byte) 35;
      numArray2[12] = (byte) 138;
      numArray2[9] = (byte) 24;
      numArray2[10] = (byte) 132;
      numArray2[11] = (byte) 216;
      numArray2[18] = (byte) 35;
      numArray2[32 /*0x20*/] = (byte) 140;
      numArray2[14] = (byte) 193;
      numArray2[15] = (byte) 100;
      numArray2[16 /*0x10*/] = (byte) 35;
      numArray2[4] = (byte) 128 /*0x80*/;
      numArray2[28] = (byte) 151;
      numArray2[27] = (byte) 105;
      numArray2[19] = (byte) 40;
      numArray2[0] = (byte) 228;
      numArray2[30] = (byte) 161;
      numArray2[17] = (byte) 205;
      numArray2[35] = (byte) 101;
      numArray2[25] = (byte) 174;
      numArray2[26] = (byte) 219;
      numArray2[20] = (byte) 109;
      numArray2[34] = (byte) 178;
      numArray2[29] = (byte) 205;
      numArray2[2] = (byte) 4;
      numArray2[31 /*0x1F*/] = (byte) 157;
      numArray2[7] = (byte) 201;
      numArray2[33] = (byte) 145;
      numArray2[13] = (byte) 237;
      numArray2[23] = (byte) 69;
      byte[] numArray3 = new byte[36];
      numArray3[0] = (byte) 179;
      numArray3[1] = (byte) 161;
      numArray3[20] = (byte) 222;
      numArray3[3] = (byte) 51;
      numArray3[6] = (byte) 10;
      numArray3[5] = (byte) 122;
      numArray3[8] = (byte) 216;
      numArray3[7] = (byte) 42;
      numArray3[17] = (byte) 73;
      numArray3[34] = (byte) 182;
      numArray3[10] = (byte) 215;
      numArray3[23] = (byte) 140;
      numArray3[22] = (byte) 73;
      numArray3[33] = (byte) 233;
      numArray3[16 /*0x10*/] = (byte) 232;
      numArray3[15] = (byte) 11;
      numArray3[2] = (byte) 247;
      numArray3[11] = (byte) 225;
      numArray3[18] = (byte) 206;
      numArray3[19] = (byte) 218;
      numArray3[13] = (byte) 87;
      numArray3[21] = (byte) 127 /*0x7F*/;
      numArray3[27] = (byte) 116;
      numArray3[4] = (byte) 239;
      numArray3[24] = (byte) 237;
      numArray3[14] = (byte) 135;
      numArray3[26] = (byte) 86;
      numArray3[28] = (byte) 150;
      numArray3[9] = (byte) 246;
      numArray3[29] = (byte) 91;
      numArray3[30] = (byte) 231;
      numArray3[31 /*0x1F*/] = (byte) 49;
      numArray3[32 /*0x20*/] = (byte) 20;
      numArray3[25] = (byte) 149;
      numArray3[12] = (byte) 94;
      numArray3[35] = (byte) 68;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 36);
      for (int index = 0; index < 36; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[54];
      byte[] response = new byte[54];
      Array.Copy((Array) sc_12395.sspq, 124, (Array) numArray4, 0, 54);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12395.sspr, 124, (Array) numArray4, 0, 54);
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
    byte[] numArray5 = new byte[36];
    byte[] numArray6 = new byte[36]
    {
      (byte) 206,
      (byte) 199,
      (byte) 148,
      (byte) 220,
      (byte) 133,
      (byte) 248,
      (byte) 85,
      (byte) 77,
      (byte) 14,
      (byte) 221,
      (byte) 102,
      (byte) 147,
      (byte) 218,
      (byte) 66,
      (byte) 22,
      (byte) 197,
      (byte) 161,
      (byte) 163,
      (byte) 65,
      (byte) 183,
      (byte) 28,
      (byte) 63 /*0x3F*/,
      (byte) 7,
      (byte) 75,
      (byte) 175,
      (byte) 30,
      (byte) 95,
      (byte) 7,
      (byte) 127 /*0x7F*/,
      (byte) 119,
      (byte) 42,
      (byte) 115,
      (byte) 176 /*0xB0*/,
      (byte) 72,
      (byte) 187,
      (byte) 194
    };
    byte[] numArray7 = new byte[36]
    {
      (byte) 145,
      (byte) 4,
      (byte) 222,
      (byte) 8,
      (byte) 60,
      (byte) 1,
      (byte) 185,
      (byte) 129,
      (byte) 100,
      (byte) 167,
      (byte) 122,
      (byte) 4,
      (byte) 221,
      (byte) 87,
      (byte) 118,
      (byte) 188,
      (byte) 56,
      (byte) 243,
      (byte) 92,
      (byte) 232,
      (byte) 45,
      (byte) 141,
      (byte) 126,
      (byte) 21,
      (byte) 251,
      (byte) 219,
      (byte) 95,
      (byte) 65,
      (byte) 120,
      (byte) 250,
      (byte) 15,
      (byte) 42,
      (byte) 241,
      (byte) 96 /*0x60*/,
      (byte) 66,
      (byte) 116
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 36);
    for (int index = 0; index < 36; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }

  internal static string ssp_appserver_12408()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 113,
        (byte) 185,
        (byte) 232,
        (byte) 75,
        (byte) 57,
        (byte) 206,
        (byte) 75,
        (byte) 25,
        (byte) 162,
        (byte) 189,
        (byte) 123,
        (byte) 193,
        (byte) 56,
        (byte) 117,
        (byte) 199,
        (byte) 142,
        (byte) 191,
        (byte) 72,
        (byte) 44,
        (byte) 102
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 108,
        (byte) 205,
        (byte) 144 /*0x90*/,
        (byte) 30,
        (byte) 158,
        (byte) 82,
        (byte) 79,
        (byte) 35,
        (byte) 187,
        (byte) 192 /*0xC0*/,
        (byte) 25,
        (byte) 30,
        (byte) 32 /*0x20*/,
        (byte) 129,
        (byte) 142,
        (byte) 232,
        (byte) 172,
        (byte) 17,
        (byte) 8,
        (byte) 198
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 150,
      (byte) 179,
      (byte) 243,
      (byte) 227,
      (byte) 146,
      (byte) 134,
      (byte) 83,
      (byte) 93,
      (byte) 254,
      (byte) 68,
      (byte) 238,
      (byte) 69,
      (byte) 98,
      (byte) 87,
      (byte) 187,
      (byte) 17,
      (byte) 225,
      (byte) 211,
      (byte) 70,
      (byte) 100
    };
    byte[] numArray6 = new byte[20]
    {
      (byte) 209,
      (byte) 117,
      (byte) 67,
      (byte) 124,
      (byte) 116,
      (byte) 246,
      (byte) 195,
      (byte) 3,
      (byte) 209,
      (byte) 98,
      (byte) 200,
      (byte) 12,
      (byte) 18,
      (byte) 34,
      (byte) 176 /*0xB0*/,
      (byte) 168,
      (byte) 231,
      (byte) 77,
      (byte) 182,
      (byte) 69
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
