// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7937
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7937
{
  private static byte[] sspq = new byte[91]
  {
    (byte) 53,
    (byte) 140,
    (byte) 121,
    (byte) 162,
    (byte) 1,
    (byte) 2,
    (byte) 101,
    (byte) 220,
    (byte) 101,
    (byte) 146,
    (byte) 43,
    (byte) 33,
    (byte) 18,
    (byte) 95,
    (byte) 117,
    (byte) 132,
    (byte) 232,
    (byte) 170,
    (byte) 71,
    (byte) 185,
    (byte) 153,
    (byte) 194,
    (byte) 125,
    (byte) 134,
    (byte) 88,
    (byte) 170,
    (byte) 6,
    (byte) 40,
    (byte) 66,
    (byte) 61,
    (byte) 181,
    (byte) 164,
    (byte) 154,
    (byte) 173,
    (byte) 37,
    (byte) 224 /*0xE0*/,
    (byte) 83,
    (byte) 26,
    (byte) 116,
    (byte) 229,
    (byte) 44,
    (byte) 209,
    (byte) 227,
    (byte) 166,
    (byte) 36,
    byte.MaxValue,
    (byte) 147,
    (byte) 1,
    (byte) 121,
    (byte) 4,
    (byte) 61,
    (byte) 228,
    (byte) 210,
    (byte) 110,
    (byte) 102,
    (byte) 242,
    (byte) 213,
    (byte) 48 /*0x30*/,
    (byte) 147,
    (byte) 57,
    (byte) 144 /*0x90*/,
    (byte) 241,
    (byte) 64 /*0x40*/,
    (byte) 246,
    (byte) 141,
    (byte) 92,
    (byte) 39,
    (byte) 197,
    (byte) 55,
    (byte) 78,
    (byte) 177,
    (byte) 136,
    (byte) 196,
    (byte) 209,
    (byte) 254,
    (byte) 25,
    (byte) 119,
    (byte) 156,
    (byte) 144 /*0x90*/,
    (byte) 36,
    (byte) 13,
    (byte) 198,
    (byte) 223,
    (byte) 232,
    (byte) 62,
    (byte) 202,
    (byte) 107,
    (byte) 145,
    (byte) 178,
    (byte) 93,
    (byte) 99
  };
  private static byte[] sspr = new byte[91]
  {
    (byte) 72,
    (byte) 254,
    (byte) 43,
    (byte) 234,
    (byte) 219,
    (byte) 89,
    (byte) 55,
    (byte) 176 /*0xB0*/,
    (byte) 137,
    (byte) 150,
    (byte) 28,
    (byte) 163,
    (byte) 46,
    (byte) 124,
    (byte) 177,
    (byte) 202,
    (byte) 134,
    (byte) 64 /*0x40*/,
    (byte) 131,
    (byte) 36,
    (byte) 44,
    (byte) 188,
    (byte) 104,
    (byte) 29,
    (byte) 83,
    (byte) 115,
    (byte) 204,
    (byte) 130,
    (byte) 175,
    (byte) 254,
    (byte) 118,
    (byte) 108,
    (byte) 213,
    (byte) 87,
    (byte) 246,
    (byte) 210,
    (byte) 62,
    (byte) 82,
    (byte) 242,
    (byte) 36,
    (byte) 94,
    (byte) 55,
    (byte) 95,
    (byte) 4,
    (byte) 58,
    (byte) 80 /*0x50*/,
    (byte) 132,
    (byte) 162,
    (byte) 138,
    (byte) 31 /*0x1F*/,
    (byte) 246,
    (byte) 37,
    (byte) 230,
    (byte) 246,
    (byte) 121,
    (byte) 22,
    (byte) 165,
    (byte) 130,
    (byte) 1,
    (byte) 17,
    (byte) 26,
    (byte) 153,
    (byte) 118,
    (byte) 60,
    (byte) 89,
    (byte) 124,
    (byte) 84,
    (byte) 158,
    (byte) 18,
    (byte) 83,
    (byte) 159,
    (byte) 143,
    (byte) 222,
    (byte) 86,
    (byte) 70,
    (byte) 197,
    (byte) 52,
    (byte) 40,
    (byte) 5,
    (byte) 93,
    (byte) 45,
    (byte) 76,
    (byte) 146,
    (byte) 5,
    (byte) 77,
    (byte) 40,
    (byte) 81,
    (byte) 92,
    (byte) 8,
    (byte) 113,
    (byte) 27
  };

  internal static int ssp_imbase_7938(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[39] = (byte) 0;
    sourceArray1[1] = (byte) 245;
    sourceArray1[14] = (byte) 141;
    sourceArray1[15] = (byte) 82;
    sourceArray1[6] = (byte) 153;
    sourceArray1[3] = (byte) 89;
    sourceArray1[20] = (byte) 111;
    sourceArray1[5] = (byte) 242;
    sourceArray1[8] = (byte) 52;
    sourceArray1[32 /*0x20*/] = (byte) 181;
    sourceArray1[10] = (byte) 64 /*0x40*/;
    sourceArray1[11] = (byte) 194;
    sourceArray1[40] = (byte) 117;
    sourceArray1[34] = (byte) 155;
    sourceArray1[29] = (byte) 92;
    sourceArray1[47] = (byte) 128 /*0x80*/;
    sourceArray1[16 /*0x10*/] = (byte) 182;
    sourceArray1[17] = (byte) 198;
    sourceArray1[18] = (byte) 153;
    sourceArray1[2] = (byte) 103;
    sourceArray1[24] = (byte) 204;
    sourceArray1[21] = (byte) 47;
    sourceArray1[22] = (byte) 239;
    sourceArray1[23] = (byte) 83;
    sourceArray1[0] = (byte) 45;
    sourceArray1[38] = (byte) 250;
    sourceArray1[26] = (byte) 94;
    sourceArray1[27] = (byte) 16 /*0x10*/;
    sourceArray1[28] = (byte) 192 /*0xC0*/;
    sourceArray1[36] = (byte) 14;
    sourceArray1[30] = (byte) 188;
    sourceArray1[31 /*0x1F*/] = (byte) 50;
    sourceArray1[25] = (byte) 20;
    sourceArray1[33] = (byte) 56;
    sourceArray1[9] = (byte) 45;
    sourceArray1[35] = (byte) 116;
    sourceArray1[4] = (byte) 108;
    sourceArray1[37] = (byte) 207;
    sourceArray1[13] = (byte) 168;
    sourceArray1[12] = (byte) 221;
    sourceArray1[43] = (byte) 175;
    sourceArray1[19] = (byte) 65;
    sourceArray1[42] = (byte) 209;
    sourceArray1[46] = (byte) 121;
    sourceArray1[44] = (byte) 83;
    sourceArray1[7] = (byte) 174;
    sourceArray1[45] = (byte) 65;
    sourceArray1[41] = (byte) 244;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[22] = (byte) 122;
    sourceArray2[29] = (byte) 58;
    sourceArray2[2] = (byte) 90;
    sourceArray2[34] = (byte) 227;
    sourceArray2[37] = (byte) 133;
    sourceArray2[30] = (byte) 234;
    sourceArray2[25] = (byte) 70;
    sourceArray2[7] = (byte) 214;
    sourceArray2[5] = (byte) 254;
    sourceArray2[18] = (byte) 43;
    sourceArray2[10] = (byte) 1;
    sourceArray2[19] = (byte) 124;
    sourceArray2[4] = (byte) 100;
    sourceArray2[0] = (byte) 115;
    sourceArray2[14] = (byte) 103;
    sourceArray2[47] = (byte) 193;
    sourceArray2[16 /*0x10*/] = (byte) 207;
    sourceArray2[45] = (byte) 7;
    sourceArray2[15] = (byte) 103;
    sourceArray2[46] = (byte) 136;
    sourceArray2[13] = (byte) 166;
    sourceArray2[21] = (byte) 61;
    sourceArray2[6] = (byte) 112 /*0x70*/;
    sourceArray2[20] = (byte) 134;
    sourceArray2[24] = (byte) 139;
    sourceArray2[17] = (byte) 69;
    sourceArray2[26] = (byte) 56;
    sourceArray2[11] = (byte) 151;
    sourceArray2[28] = (byte) 85;
    sourceArray2[27] = (byte) 93;
    sourceArray2[23] = (byte) 77;
    sourceArray2[8] = (byte) 228;
    sourceArray2[32 /*0x20*/] = (byte) 249;
    sourceArray2[39] = (byte) 186;
    sourceArray2[44] = (byte) 135;
    sourceArray2[35] = (byte) 97;
    sourceArray2[36] = (byte) 35;
    sourceArray2[40] = (byte) 97;
    sourceArray2[38] = (byte) 84;
    sourceArray2[3] = (byte) 105;
    sourceArray2[1] = (byte) 236;
    sourceArray2[41] = (byte) 140;
    sourceArray2[42] = (byte) 136;
    sourceArray2[43] = (byte) 186;
    sourceArray2[31 /*0x1F*/] = (byte) 142;
    sourceArray2[33] = (byte) 66;
    sourceArray2[12] = (byte) 239;
    sourceArray2[9] = (byte) 233;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_imbase_7939()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[25];
      byte[] numArray2 = new byte[25]
      {
        (byte) 230,
        (byte) 181,
        (byte) 61,
        (byte) 107,
        (byte) 169,
        (byte) 207,
        (byte) 208 /*0xD0*/,
        (byte) 246,
        (byte) 82,
        (byte) 4,
        (byte) 114,
        (byte) 224 /*0xE0*/,
        (byte) 73,
        (byte) 197,
        (byte) 212,
        (byte) 61,
        (byte) 39,
        (byte) 59,
        (byte) 196,
        (byte) 90,
        (byte) 169,
        (byte) 247,
        (byte) 77,
        (byte) 240 /*0xF0*/,
        (byte) 116
      };
      byte[] numArray3 = new byte[25];
      numArray3[5] = (byte) 195;
      numArray3[13] = (byte) 219;
      numArray3[2] = (byte) 175;
      numArray3[1] = (byte) 6;
      numArray3[6] = (byte) 96 /*0x60*/;
      numArray3[21] = (byte) 154;
      numArray3[22] = (byte) 157;
      numArray3[7] = (byte) 66;
      numArray3[8] = (byte) 131;
      numArray3[9] = (byte) 82;
      numArray3[10] = (byte) 216;
      numArray3[18] = (byte) 234;
      numArray3[0] = (byte) 208 /*0xD0*/;
      numArray3[16 /*0x10*/] = (byte) 146;
      numArray3[12] = (byte) 212;
      numArray3[15] = (byte) 61;
      numArray3[11] = (byte) 179;
      numArray3[14] = (byte) 157;
      numArray3[3] = (byte) 40;
      numArray3[4] = (byte) 252;
      numArray3[20] = (byte) 107;
      numArray3[17] = (byte) 153;
      numArray3[19] = (byte) 36;
      numArray3[23] = (byte) 26;
      numArray3[24] = (byte) 233;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 25);
      for (int index = 0; index < 25; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[25];
    byte[] numArray5 = new byte[25];
    numArray5[21] = (byte) 112 /*0x70*/;
    numArray5[1] = (byte) 153;
    numArray5[2] = (byte) 238;
    numArray5[12] = (byte) 112 /*0x70*/;
    numArray5[19] = (byte) 126;
    numArray5[5] = (byte) 246;
    numArray5[6] = (byte) 243;
    numArray5[20] = (byte) 146;
    numArray5[8] = (byte) 241;
    numArray5[17] = (byte) 155;
    numArray5[7] = (byte) 118;
    numArray5[4] = (byte) 76;
    numArray5[9] = (byte) 105;
    numArray5[0] = (byte) 140;
    numArray5[3] = (byte) 103;
    numArray5[13] = (byte) 124;
    numArray5[16 /*0x10*/] = (byte) 92;
    numArray5[11] = (byte) 146;
    numArray5[18] = (byte) 30;
    numArray5[10] = (byte) 86;
    numArray5[22] = (byte) 71;
    numArray5[14] = (byte) 232;
    numArray5[15] = (byte) 131;
    numArray5[23] = (byte) 132;
    numArray5[24] = (byte) 233;
    byte[] numArray6 = new byte[25];
    numArray6[4] = (byte) 151;
    numArray6[1] = (byte) 123;
    numArray6[19] = (byte) 151;
    numArray6[3] = (byte) 197;
    numArray6[9] = (byte) 88;
    numArray6[17] = (byte) 164;
    numArray6[6] = (byte) 140;
    numArray6[7] = (byte) 204;
    numArray6[21] = (byte) 172;
    numArray6[8] = (byte) 121;
    numArray6[2] = (byte) 242;
    numArray6[20] = (byte) 174;
    numArray6[12] = (byte) 165;
    numArray6[13] = (byte) 154;
    numArray6[14] = (byte) 88;
    numArray6[0] = (byte) 216;
    numArray6[10] = (byte) 149;
    numArray6[16 /*0x10*/] = (byte) 100;
    numArray6[18] = (byte) 78;
    numArray6[5] = (byte) 152;
    numArray6[24] = (byte) 105;
    numArray6[15] = (byte) 46;
    numArray6[22] = (byte) 127 /*0x7F*/;
    numArray6[23] = (byte) 246;
    numArray6[11] = (byte) 156;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 25);
    for (int index = 0; index < 25; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7940()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24]
      {
        (byte) 23,
        (byte) 202,
        (byte) 99,
        (byte) 252,
        (byte) 63 /*0x3F*/,
        (byte) 181,
        (byte) 62,
        (byte) 90,
        (byte) 108,
        (byte) 205,
        (byte) 188,
        (byte) 118,
        (byte) 232,
        (byte) 146,
        (byte) 173,
        (byte) 73,
        (byte) 57,
        (byte) 80 /*0x50*/,
        (byte) 33,
        (byte) 166,
        (byte) 97,
        (byte) 239,
        (byte) 136,
        (byte) 37
      };
      byte[] numArray3 = new byte[24]
      {
        (byte) 173,
        (byte) 50,
        (byte) 184,
        (byte) 66,
        (byte) 230,
        (byte) 53,
        (byte) 155,
        (byte) 56,
        (byte) 155,
        (byte) 180,
        (byte) 8,
        (byte) 128 /*0x80*/,
        (byte) 248,
        (byte) 86,
        (byte) 157,
        (byte) 16 /*0x10*/,
        (byte) 147,
        (byte) 38,
        (byte) 113,
        (byte) 254,
        (byte) 22,
        (byte) 126,
        (byte) 107,
        (byte) 67
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[51];
      byte[] response = new byte[51];
      Array.Copy((Array) sc_7937.sspq, 0, (Array) numArray4, 0, 51);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7937.sspr, 0, (Array) numArray4, 0, 51);
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
    byte[] numArray5 = new byte[24];
    byte[] numArray6 = new byte[24]
    {
      (byte) 156,
      (byte) 83,
      (byte) 97,
      (byte) 248,
      (byte) 114,
      (byte) 15,
      (byte) 243,
      (byte) 74,
      (byte) 87,
      (byte) 252,
      (byte) 61,
      (byte) 154,
      (byte) 40,
      (byte) 221,
      (byte) 135,
      (byte) 135,
      (byte) 101,
      (byte) 58,
      (byte) 149,
      (byte) 49,
      (byte) 206,
      (byte) 54,
      (byte) 97,
      (byte) 140
    };
    byte[] numArray7 = new byte[24];
    numArray7[12] = (byte) 185;
    numArray7[11] = (byte) 133;
    numArray7[6] = (byte) 233;
    numArray7[15] = (byte) 112 /*0x70*/;
    numArray7[4] = (byte) 86;
    numArray7[16 /*0x10*/] = (byte) 49;
    numArray7[14] = (byte) 96 /*0x60*/;
    numArray7[7] = (byte) 202;
    numArray7[13] = (byte) 39;
    numArray7[1] = (byte) 9;
    numArray7[10] = (byte) 238;
    numArray7[17] = (byte) 239;
    numArray7[2] = (byte) 228;
    numArray7[9] = (byte) 177;
    numArray7[8] = (byte) 236;
    numArray7[22] = (byte) 7;
    numArray7[0] = (byte) 137;
    numArray7[21] = (byte) 240 /*0xF0*/;
    numArray7[18] = (byte) 126;
    numArray7[19] = (byte) 202;
    numArray7[20] = (byte) 2;
    numArray7[5] = (byte) 194;
    numArray7[3] = (byte) 48 /*0x30*/;
    numArray7[23] = (byte) 245;
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[40];
    byte[] response1 = new byte[40];
    Array.Copy((Array) sc_7937.sspq, 51, (Array) numArray8, 0, 40);
    key.Query(true, 343, numArray8, response1);
    Array.Copy((Array) sc_7937.sspr, 51, (Array) numArray8, 0, 40);
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

  internal static int ssp_imbase_7941(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 151,
      (byte) 207,
      (byte) 222,
      (byte) 220,
      (byte) 243,
      (byte) 97,
      (byte) 218,
      (byte) 147,
      (byte) 3,
      (byte) 100,
      (byte) 41,
      (byte) 124,
      (byte) 62,
      (byte) 31 /*0x1F*/,
      (byte) 1,
      (byte) 112 /*0x70*/,
      (byte) 77,
      (byte) 122,
      (byte) 56,
      (byte) 159,
      (byte) 110,
      (byte) 43,
      (byte) 202,
      (byte) 17,
      (byte) 88,
      (byte) 184,
      (byte) 195,
      (byte) 241,
      (byte) 149,
      (byte) 18,
      (byte) 218,
      (byte) 235,
      (byte) 44,
      (byte) 104,
      (byte) 44,
      (byte) 1,
      (byte) 31 /*0x1F*/,
      (byte) 60,
      (byte) 132,
      (byte) 58,
      (byte) 182,
      (byte) 145,
      (byte) 9,
      (byte) 97,
      (byte) 181,
      (byte) 108,
      (byte) 0,
      (byte) 170
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 68,
      (byte) 81,
      (byte) 85,
      (byte) 99,
      (byte) 117,
      (byte) 121,
      (byte) 105,
      (byte) 221,
      (byte) 250,
      (byte) 151,
      (byte) 238,
      (byte) 181,
      (byte) 77,
      (byte) 44,
      (byte) 116,
      (byte) 109,
      (byte) 41,
      (byte) 104,
      (byte) 81,
      (byte) 5,
      (byte) 164,
      (byte) 81,
      (byte) 156,
      (byte) 212,
      (byte) 219,
      (byte) 202,
      (byte) 132,
      (byte) 63 /*0x3F*/,
      (byte) 246,
      (byte) 173,
      (byte) 28,
      (byte) 214,
      (byte) 247,
      (byte) 136,
      (byte) 140,
      (byte) 113,
      (byte) 226,
      (byte) 99,
      (byte) 49,
      (byte) 245,
      (byte) 2,
      (byte) 245,
      (byte) 109,
      (byte) 247,
      (byte) 58,
      (byte) 171,
      (byte) 14,
      (byte) 68
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 343, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
