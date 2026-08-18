// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7788
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7788
{
  private static byte[] sspq = new byte[71]
  {
    (byte) 228,
    (byte) 10,
    (byte) 157,
    (byte) 240 /*0xF0*/,
    (byte) 199,
    (byte) 135,
    (byte) 206,
    (byte) 9,
    (byte) 125,
    (byte) 202,
    (byte) 75,
    (byte) 103,
    (byte) 146,
    (byte) 63 /*0x3F*/,
    (byte) 236,
    (byte) 206,
    (byte) 126,
    (byte) 176 /*0xB0*/,
    (byte) 66,
    (byte) 128 /*0x80*/,
    (byte) 241,
    (byte) 138,
    (byte) 121,
    (byte) 251,
    (byte) 235,
    (byte) 100,
    (byte) 250,
    (byte) 24,
    (byte) 31 /*0x1F*/,
    (byte) 111,
    (byte) 184,
    (byte) 21,
    (byte) 58,
    (byte) 66,
    (byte) 215,
    (byte) 211,
    (byte) 70,
    (byte) 15,
    (byte) 217,
    (byte) 31 /*0x1F*/,
    (byte) 168,
    (byte) 130,
    (byte) 95,
    (byte) 143,
    (byte) 214,
    (byte) 183,
    (byte) 221,
    (byte) 172,
    (byte) 237,
    (byte) 102,
    (byte) 184,
    (byte) 120,
    (byte) 108,
    (byte) 77,
    (byte) 221,
    (byte) 139,
    (byte) 95,
    (byte) 163,
    (byte) 232,
    (byte) 32 /*0x20*/,
    (byte) 249,
    (byte) 96 /*0x60*/,
    (byte) 47,
    (byte) 14,
    (byte) 182,
    (byte) 29,
    (byte) 49,
    (byte) 18,
    (byte) 17,
    (byte) 120,
    (byte) 224 /*0xE0*/
  };
  private static byte[] sspr = new byte[71]
  {
    (byte) 164,
    (byte) 242,
    (byte) 222,
    (byte) 30,
    (byte) 219,
    (byte) 34,
    (byte) 7,
    (byte) 204,
    (byte) 62,
    (byte) 70,
    (byte) 172,
    (byte) 192 /*0xC0*/,
    (byte) 90,
    (byte) 132,
    (byte) 107,
    (byte) 110,
    (byte) 27,
    (byte) 236,
    (byte) 28,
    (byte) 30,
    (byte) 216,
    (byte) 47,
    (byte) 4,
    (byte) 246,
    (byte) 88,
    (byte) 52,
    (byte) 17,
    (byte) 89,
    (byte) 151,
    (byte) 54,
    (byte) 141,
    (byte) 122,
    (byte) 11,
    (byte) 225,
    (byte) 90,
    (byte) 47,
    (byte) 237,
    (byte) 98,
    (byte) 157,
    (byte) 7,
    (byte) 29,
    (byte) 14,
    (byte) 225,
    (byte) 103,
    (byte) 161,
    (byte) 2,
    (byte) 213,
    (byte) 148,
    (byte) 182,
    (byte) 182,
    (byte) 65,
    (byte) 253,
    (byte) 231,
    (byte) 61,
    (byte) 239,
    (byte) 110,
    (byte) 200,
    (byte) 193,
    (byte) 42,
    (byte) 161,
    (byte) 76,
    (byte) 87,
    (byte) 204,
    (byte) 40,
    (byte) 188,
    (byte) 98,
    (byte) 93,
    (byte) 46,
    (byte) 157,
    (byte) 87,
    (byte) 62
  };

  internal static string ssp_imbase_7789()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 7,
        (byte) 50,
        (byte) 143,
        (byte) 209,
        (byte) 231,
        (byte) 165,
        (byte) 230,
        (byte) 182,
        (byte) 123,
        (byte) 152,
        (byte) 198,
        (byte) 11
      };
      byte[] numArray3 = new byte[12];
      numArray3[1] = (byte) 62;
      numArray3[4] = (byte) 160 /*0xA0*/;
      numArray3[2] = (byte) 233;
      numArray3[3] = (byte) 46;
      numArray3[0] = (byte) 180;
      numArray3[5] = (byte) 242;
      numArray3[6] = (byte) 52;
      numArray3[7] = (byte) 249;
      numArray3[11] = (byte) 128 /*0x80*/;
      numArray3[9] = (byte) 184;
      numArray3[10] = (byte) 119;
      numArray3[8] = (byte) 254;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12];
    numArray5[8] = (byte) 29;
    numArray5[1] = (byte) 126;
    numArray5[5] = (byte) 125;
    numArray5[3] = (byte) 233;
    numArray5[4] = (byte) 248;
    numArray5[7] = (byte) 211;
    numArray5[6] = (byte) 102;
    numArray5[11] = (byte) 230;
    numArray5[2] = (byte) 81;
    numArray5[9] = (byte) 10;
    numArray5[10] = (byte) 211;
    numArray5[0] = (byte) 177;
    byte[] numArray6 = new byte[12];
    numArray6[3] = (byte) 184;
    numArray6[0] = (byte) 110;
    numArray6[2] = (byte) 100;
    numArray6[1] = (byte) 6;
    numArray6[4] = (byte) 84;
    numArray6[5] = (byte) 56;
    numArray6[6] = (byte) 8;
    numArray6[7] = (byte) 195;
    numArray6[8] = (byte) 141;
    numArray6[9] = (byte) 93;
    numArray6[10] = (byte) 20;
    numArray6[11] = (byte) 102;
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[54];
    byte[] response = new byte[54];
    Array.Copy((Array) sc_7788.sspq, 0, (Array) numArray7, 0, 54);
    key.Query(true, 343, numArray7, response);
    Array.Copy((Array) sc_7788.sspr, 0, (Array) numArray7, 0, 54);
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

  internal static string ssp_imbase_7790()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 89,
        (byte) 145,
        (byte) 161,
        (byte) 5,
        (byte) 99,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 17
      };
      numArray2[8] = (byte) 13;
      numArray2[6] = (byte) 182;
      numArray2[7] = (byte) 233;
      numArray2[5] = (byte) 141;
      byte[] numArray3 = new byte[10]
      {
        (byte) 66,
        (byte) 3,
        (byte) 121,
        (byte) 111,
        (byte) 143,
        (byte) 53,
        (byte) 150,
        (byte) 191,
        (byte) 92,
        (byte) 24
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 36,
      (byte) 51,
      (byte) 135,
      (byte) 78,
      (byte) 42,
      (byte) 184,
      (byte) 182,
      (byte) 63 /*0x3F*/,
      (byte) 168,
      (byte) 237
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 75,
      (byte) 49,
      (byte) 134,
      (byte) 23,
      (byte) 20,
      (byte) 20,
      (byte) 59,
      (byte) 203,
      byte.MaxValue,
      (byte) 222
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static int ssp_imbase_7791(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 139;
    sourceArray1[28] = (byte) 63 /*0x3F*/;
    sourceArray1[13] = (byte) 161;
    sourceArray1[22] = (byte) 63 /*0x3F*/;
    sourceArray1[4] = (byte) 81;
    sourceArray1[40] = (byte) 133;
    sourceArray1[25] = (byte) 87;
    sourceArray1[5] = (byte) 125;
    sourceArray1[0] = (byte) 117;
    sourceArray1[9] = (byte) 68;
    sourceArray1[10] = (byte) 212;
    sourceArray1[32 /*0x20*/] = (byte) 226;
    sourceArray1[12] = (byte) 72;
    sourceArray1[26] = (byte) 210;
    sourceArray1[14] = (byte) 74;
    sourceArray1[17] = (byte) 46;
    sourceArray1[16 /*0x10*/] = (byte) 45;
    sourceArray1[15] = (byte) 117;
    sourceArray1[11] = (byte) 223;
    sourceArray1[19] = (byte) 125;
    sourceArray1[20] = (byte) 209;
    sourceArray1[21] = (byte) 220;
    sourceArray1[34] = (byte) 97;
    sourceArray1[23] = (byte) 139;
    sourceArray1[41] = (byte) 168;
    sourceArray1[43] = (byte) 9;
    sourceArray1[35] = (byte) 144 /*0x90*/;
    sourceArray1[24] = (byte) 48 /*0x30*/;
    sourceArray1[1] = (byte) 146;
    sourceArray1[29] = (byte) 169;
    sourceArray1[33] = (byte) 102;
    sourceArray1[31 /*0x1F*/] = (byte) 214;
    sourceArray1[3] = (byte) 153;
    sourceArray1[8] = (byte) 141;
    sourceArray1[18] = (byte) 167;
    sourceArray1[39] = (byte) 108;
    sourceArray1[36] = (byte) 229;
    sourceArray1[6] = (byte) 159;
    sourceArray1[38] = (byte) 217;
    sourceArray1[44] = (byte) 86;
    sourceArray1[30] = (byte) 234;
    sourceArray1[2] = (byte) 242;
    sourceArray1[42] = (byte) 249;
    sourceArray1[27] = (byte) 142;
    sourceArray1[45] = (byte) 217;
    sourceArray1[37] = (byte) 213;
    sourceArray1[46] = (byte) 110;
    sourceArray1[47] = (byte) 158;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 208 /*0xD0*/,
      (byte) 165,
      (byte) 156,
      (byte) 35,
      (byte) 14,
      (byte) 250,
      (byte) 239,
      (byte) 115,
      (byte) 216,
      (byte) 45,
      (byte) 186,
      (byte) 115,
      (byte) 180,
      (byte) 167,
      (byte) 197,
      (byte) 243,
      (byte) 248,
      (byte) 68,
      (byte) 115,
      (byte) 178,
      (byte) 91,
      (byte) 126,
      (byte) 155,
      (byte) 185,
      (byte) 204,
      (byte) 183,
      (byte) 201,
      (byte) 160 /*0xA0*/,
      (byte) 1,
      (byte) 188,
      (byte) 203,
      (byte) 136,
      (byte) 159,
      (byte) 155,
      (byte) 102,
      (byte) 182,
      (byte) 13,
      (byte) 74,
      (byte) 41,
      (byte) 164,
      (byte) 121,
      (byte) 22,
      (byte) 79,
      (byte) 220,
      (byte) 230,
      (byte) 249,
      (byte) 130,
      (byte) 144 /*0x90*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 343, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[17];
    byte[] response2 = new byte[17];
    Array.Copy((Array) sc_7788.sspq, 54, (Array) numArray2, 0, 17);
    key.Query(true, 343, numArray2, response2);
    Array.Copy((Array) sc_7788.sspr, 54, (Array) numArray2, 0, 17);
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
}
