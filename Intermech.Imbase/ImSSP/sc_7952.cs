// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7952
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7952
{
  private static byte[] sspq = new byte[48 /*0x30*/]
  {
    (byte) 9,
    (byte) 145,
    (byte) 228,
    (byte) 207,
    (byte) 141,
    (byte) 96 /*0x60*/,
    (byte) 31 /*0x1F*/,
    (byte) 163,
    (byte) 88,
    (byte) 11,
    (byte) 147,
    (byte) 77,
    (byte) 228,
    (byte) 119,
    (byte) 95,
    (byte) 226,
    (byte) 32 /*0x20*/,
    (byte) 198,
    (byte) 21,
    (byte) 107,
    (byte) 9,
    (byte) 212,
    (byte) 68,
    (byte) 249,
    (byte) 20,
    (byte) 13,
    (byte) 133,
    (byte) 100,
    (byte) 211,
    (byte) 254,
    (byte) 62,
    (byte) 204,
    (byte) 222,
    (byte) 137,
    (byte) 63 /*0x3F*/,
    (byte) 11,
    (byte) 139,
    (byte) 200,
    (byte) 245,
    (byte) 37,
    (byte) 66,
    (byte) 230,
    (byte) 235,
    (byte) 135,
    (byte) 234,
    (byte) 186,
    (byte) 57,
    (byte) 187
  };
  private static byte[] sspr = new byte[48 /*0x30*/]
  {
    (byte) 145,
    (byte) 26,
    (byte) 125,
    (byte) 203,
    (byte) 49,
    (byte) 48 /*0x30*/,
    (byte) 24,
    (byte) 170,
    (byte) 97,
    (byte) 218,
    (byte) 166,
    (byte) 26,
    (byte) 254,
    (byte) 44,
    (byte) 127 /*0x7F*/,
    (byte) 137,
    (byte) 173,
    (byte) 103,
    (byte) 104,
    (byte) 245,
    (byte) 196,
    (byte) 250,
    (byte) 95,
    (byte) 171,
    (byte) 53,
    (byte) 89,
    (byte) 248,
    (byte) 89,
    (byte) 0,
    (byte) 215,
    (byte) 98,
    (byte) 106,
    (byte) 118,
    (byte) 144 /*0x90*/,
    (byte) 56,
    (byte) 115,
    (byte) 70,
    (byte) 140,
    (byte) 245,
    (byte) 120,
    (byte) 86,
    (byte) 65,
    (byte) 110,
    (byte) 105,
    (byte) 236,
    (byte) 125,
    (byte) 168,
    (byte) 246
  };

  internal static string ssp_imbase_7953()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24]
      {
        (byte) 104,
        (byte) 208 /*0xD0*/,
        (byte) 85,
        (byte) 236,
        (byte) 45,
        (byte) 251,
        (byte) 211,
        (byte) 73,
        (byte) 156,
        (byte) 112 /*0x70*/,
        (byte) 152,
        (byte) 130,
        (byte) 134,
        (byte) 97,
        (byte) 137,
        (byte) 67,
        (byte) 146,
        (byte) 125,
        (byte) 7,
        (byte) 29,
        (byte) 50,
        (byte) 4,
        (byte) 253,
        (byte) 87
      };
      byte[] numArray3 = new byte[24]
      {
        (byte) 189,
        (byte) 76,
        (byte) 192 /*0xC0*/,
        (byte) 159,
        (byte) 106,
        (byte) 143,
        (byte) 156,
        (byte) 47,
        (byte) 187,
        (byte) 114,
        (byte) 96 /*0x60*/,
        (byte) 167,
        (byte) 174,
        (byte) 201,
        (byte) 155,
        (byte) 214,
        (byte) 237,
        (byte) 13,
        (byte) 17,
        (byte) 133,
        (byte) 157,
        (byte) 57,
        (byte) 201,
        (byte) 214
      };
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[48 /*0x30*/];
      byte[] response = new byte[48 /*0x30*/];
      Array.Copy((Array) sc_7952.sspq, 0, (Array) numArray4, 0, 48 /*0x30*/);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7952.sspr, 0, (Array) numArray4, 0, 48 /*0x30*/);
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
    byte[] numArray6 = new byte[24];
    numArray6[23] = (byte) 88;
    numArray6[1] = (byte) 26;
    numArray6[16 /*0x10*/] = (byte) 204;
    numArray6[3] = (byte) 230;
    numArray6[10] = (byte) 186;
    numArray6[5] = (byte) 177;
    numArray6[6] = (byte) 64 /*0x40*/;
    numArray6[7] = (byte) 53;
    numArray6[8] = (byte) 77;
    numArray6[9] = (byte) 235;
    numArray6[13] = (byte) 186;
    numArray6[11] = (byte) 128 /*0x80*/;
    numArray6[20] = (byte) 55;
    numArray6[14] = (byte) 208 /*0xD0*/;
    numArray6[15] = (byte) 90;
    numArray6[12] = (byte) 219;
    numArray6[17] = (byte) 198;
    numArray6[2] = (byte) 54;
    numArray6[18] = (byte) 110;
    numArray6[19] = (byte) 90;
    numArray6[4] = (byte) 75;
    numArray6[21] = (byte) 84;
    numArray6[22] = (byte) 86;
    numArray6[0] = (byte) 165;
    byte[] numArray7 = new byte[24]
    {
      (byte) 93,
      (byte) 98,
      (byte) 182,
      (byte) 142,
      (byte) 155,
      (byte) 189,
      (byte) 56,
      (byte) 126,
      (byte) 10,
      (byte) 62,
      (byte) 18,
      (byte) 251,
      (byte) 109,
      (byte) 77,
      (byte) 167,
      (byte) 249,
      (byte) 21,
      (byte) 89,
      (byte) 191,
      (byte) 137,
      (byte) 195,
      (byte) 136,
      (byte) 58,
      (byte) 113
    };
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
