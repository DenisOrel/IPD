// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_931
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_931
{
  private static byte[] sspq = new byte[77]
  {
    (byte) 164,
    (byte) 77,
    (byte) 155,
    (byte) 118,
    (byte) 162,
    (byte) 150,
    (byte) 43,
    (byte) 10,
    (byte) 200,
    (byte) 128 /*0x80*/,
    (byte) 103,
    (byte) 47,
    (byte) 36,
    (byte) 131,
    (byte) 76,
    (byte) 70,
    (byte) 45,
    (byte) 220,
    (byte) 93,
    (byte) 78,
    (byte) 76,
    (byte) 43,
    (byte) 239,
    (byte) 128 /*0x80*/,
    (byte) 58,
    (byte) 41,
    (byte) 166,
    (byte) 149,
    (byte) 53,
    (byte) 103,
    (byte) 38,
    (byte) 251,
    (byte) 234,
    (byte) 50,
    (byte) 121,
    (byte) 212,
    (byte) 94,
    (byte) 154,
    (byte) 226,
    (byte) 240 /*0xF0*/,
    (byte) 19,
    (byte) 238,
    (byte) 119,
    (byte) 236,
    (byte) 73,
    (byte) 0,
    (byte) 118,
    (byte) 67,
    (byte) 94,
    (byte) 105,
    (byte) 0,
    (byte) 124,
    (byte) 25,
    (byte) 21,
    (byte) 145,
    (byte) 221,
    (byte) 95,
    (byte) 248,
    (byte) 126,
    (byte) 125,
    (byte) 179,
    (byte) 40,
    (byte) 189,
    (byte) 40,
    (byte) 210,
    (byte) 220,
    (byte) 196,
    (byte) 90,
    (byte) 228,
    (byte) 165,
    (byte) 57,
    (byte) 209,
    (byte) 247,
    (byte) 59,
    (byte) 182,
    (byte) 235,
    (byte) 6
  };
  private static byte[] sspr = new byte[77]
  {
    (byte) 102,
    (byte) 80 /*0x50*/,
    (byte) 46,
    (byte) 208 /*0xD0*/,
    (byte) 172,
    (byte) 69,
    (byte) 159,
    (byte) 39,
    (byte) 115,
    (byte) 247,
    (byte) 142,
    (byte) 153,
    (byte) 87,
    (byte) 76,
    (byte) 119,
    (byte) 70,
    (byte) 24,
    (byte) 72,
    (byte) 152,
    (byte) 166,
    (byte) 66,
    (byte) 21,
    (byte) 228,
    (byte) 111,
    (byte) 233,
    (byte) 134,
    (byte) 18,
    (byte) 51,
    (byte) 190,
    (byte) 18,
    (byte) 222,
    (byte) 149,
    (byte) 42,
    (byte) 246,
    (byte) 223,
    (byte) 116,
    (byte) 71,
    (byte) 127 /*0x7F*/,
    (byte) 235,
    (byte) 205,
    (byte) 209,
    (byte) 39,
    (byte) 252,
    (byte) 134,
    (byte) 202,
    (byte) 102,
    (byte) 219,
    (byte) 205,
    (byte) 216,
    (byte) 171,
    (byte) 207,
    (byte) 41,
    (byte) 8,
    (byte) 152,
    (byte) 105,
    (byte) 40,
    (byte) 241,
    (byte) 132,
    (byte) 95,
    (byte) 166,
    (byte) 119,
    (byte) 140,
    (byte) 131,
    (byte) 175,
    (byte) 222,
    (byte) 43,
    (byte) 130,
    (byte) 162,
    (byte) 91,
    (byte) 86,
    (byte) 28,
    (byte) 252,
    (byte) 213,
    (byte) 210,
    (byte) 183,
    (byte) 210,
    (byte) 49
  };

  internal static string ssp_avs_932()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 119,
        (byte) 222,
        (byte) 161,
        (byte) 110,
        (byte) 64 /*0x40*/,
        (byte) 61,
        (byte) 97,
        (byte) 227,
        (byte) 141,
        (byte) 69,
        (byte) 107,
        (byte) 92,
        (byte) 49,
        (byte) 14,
        (byte) 55,
        (byte) 177,
        (byte) 216,
        (byte) 23,
        (byte) 80 /*0x50*/,
        (byte) 186
      };
      byte[] numArray3 = new byte[20];
      numArray3[15] = (byte) 38;
      numArray3[13] = (byte) 235;
      numArray3[1] = (byte) 91;
      numArray3[3] = (byte) 62;
      numArray3[4] = (byte) 91;
      numArray3[0] = (byte) 24;
      numArray3[2] = (byte) 218;
      numArray3[7] = (byte) 22;
      numArray3[8] = (byte) 119;
      numArray3[9] = (byte) 228;
      numArray3[10] = (byte) 251;
      numArray3[11] = (byte) 205;
      numArray3[18] = (byte) 2;
      numArray3[6] = (byte) 162;
      numArray3[19] = (byte) 234;
      numArray3[14] = (byte) 7;
      numArray3[16 /*0x10*/] = (byte) 108;
      numArray3[17] = (byte) 61;
      numArray3[12] = (byte) 253;
      numArray3[5] = (byte) 50;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_931.sspq, 0, (Array) numArray4, 0, 45);
      key.Query(true, 339, numArray4, response);
      Array.Copy((Array) sc_931.sspr, 0, (Array) numArray4, 0, 45);
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
    byte[] numArray5 = new byte[20];
    byte[] numArray6 = new byte[20]
    {
      (byte) 164,
      (byte) 233,
      (byte) 136,
      (byte) 246,
      (byte) 155,
      (byte) 187,
      (byte) 183,
      (byte) 149,
      (byte) 14,
      (byte) 59,
      (byte) 121,
      (byte) 93,
      (byte) 26,
      (byte) 59,
      (byte) 121,
      (byte) 239,
      (byte) 150,
      (byte) 213,
      (byte) 97,
      (byte) 90
    };
    byte[] numArray7 = new byte[20];
    numArray7[14] = (byte) 75;
    numArray7[1] = (byte) 134;
    numArray7[15] = (byte) 53;
    numArray7[18] = (byte) 46;
    numArray7[4] = (byte) 44;
    numArray7[2] = (byte) 70;
    numArray7[13] = (byte) 221;
    numArray7[7] = (byte) 31 /*0x1F*/;
    numArray7[8] = (byte) 34;
    numArray7[9] = (byte) 252;
    numArray7[10] = (byte) 209;
    numArray7[12] = (byte) 51;
    numArray7[17] = (byte) 237;
    numArray7[0] = (byte) 233;
    numArray7[5] = (byte) 123;
    numArray7[3] = (byte) 199;
    numArray7[6] = (byte) 20;
    numArray7[16 /*0x10*/] = (byte) 169;
    numArray7[11] = (byte) 45;
    numArray7[19] = (byte) 10;
    key.Query(true, 339, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[32 /*0x20*/];
    byte[] response1 = new byte[32 /*0x20*/];
    Array.Copy((Array) sc_931.sspq, 45, (Array) numArray8, 0, 32 /*0x20*/);
    key.Query(true, 339, numArray8, response1);
    Array.Copy((Array) sc_931.sspr, 45, (Array) numArray8, 0, 32 /*0x20*/);
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
}
