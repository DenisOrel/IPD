// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7892
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7892
{
  private static byte[] sspq = new byte[31 /*0x1F*/]
  {
    (byte) 9,
    (byte) 164,
    (byte) 81,
    (byte) 127 /*0x7F*/,
    (byte) 125,
    (byte) 51,
    (byte) 166,
    (byte) 70,
    (byte) 246,
    (byte) 51,
    (byte) 27,
    (byte) 164,
    (byte) 150,
    (byte) 72,
    (byte) 40,
    (byte) 208 /*0xD0*/,
    (byte) 119,
    (byte) 219,
    (byte) 173,
    (byte) 26,
    (byte) 96 /*0x60*/,
    (byte) 67,
    (byte) 219,
    (byte) 214,
    (byte) 117,
    (byte) 191,
    (byte) 205,
    (byte) 237,
    (byte) 232,
    (byte) 180,
    (byte) 47
  };
  private static byte[] sspr = new byte[31 /*0x1F*/]
  {
    (byte) 90,
    (byte) 68,
    (byte) 191,
    (byte) 118,
    (byte) 52,
    (byte) 141,
    (byte) 171,
    (byte) 80 /*0x50*/,
    (byte) 7,
    (byte) 99,
    (byte) 2,
    (byte) 161,
    (byte) 37,
    (byte) 156,
    (byte) 125,
    (byte) 65,
    (byte) 0,
    (byte) 112 /*0x70*/,
    (byte) 235,
    (byte) 205,
    (byte) 132,
    (byte) 162,
    (byte) 179,
    (byte) 3,
    (byte) 112 /*0x70*/,
    (byte) 92,
    (byte) 110,
    (byte) 48 /*0x30*/,
    (byte) 53,
    (byte) 51,
    (byte) 225
  };

  internal static string ssp_imbase_7893()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[8] = (byte) 80 /*0x50*/;
      numArray2[1] = (byte) 187;
      numArray2[6] = (byte) 199;
      numArray2[11] = (byte) 79;
      numArray2[10] = (byte) 45;
      numArray2[3] = (byte) 236;
      numArray2[13] = (byte) 141;
      numArray2[7] = (byte) 225;
      numArray2[2] = (byte) 85;
      numArray2[9] = (byte) 105;
      numArray2[0] = (byte) 163;
      numArray2[4] = (byte) 58;
      numArray2[12] = (byte) 15;
      numArray2[5] = (byte) 132;
      byte[] numArray3 = new byte[14];
      numArray3[2] = (byte) 215;
      numArray3[1] = (byte) 141;
      numArray3[11] = (byte) 189;
      numArray3[3] = (byte) 209;
      numArray3[4] = (byte) 159;
      numArray3[6] = (byte) 198;
      numArray3[0] = (byte) 7;
      numArray3[7] = (byte) 48 /*0x30*/;
      numArray3[12] = (byte) 149;
      numArray3[9] = (byte) 93;
      numArray3[10] = (byte) 1;
      numArray3[13] = (byte) 48 /*0x30*/;
      numArray3[8] = (byte) 68;
      numArray3[5] = (byte) 96 /*0x60*/;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 175,
      (byte) 254,
      (byte) 157,
      (byte) 247,
      (byte) 122,
      (byte) 175,
      (byte) 156,
      (byte) 145,
      (byte) 21,
      (byte) 141,
      (byte) 200,
      (byte) 98,
      (byte) 209,
      (byte) 179
    };
    byte[] numArray6 = new byte[14]
    {
      (byte) 101,
      (byte) 181,
      (byte) 63 /*0x3F*/,
      (byte) 237,
      (byte) 144 /*0x90*/,
      (byte) 64 /*0x40*/,
      (byte) 203,
      (byte) 109,
      (byte) 246,
      (byte) 207,
      (byte) 230,
      (byte) 126,
      (byte) 108,
      (byte) 97
    };
    key.Query(true, 343, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imbase_7894()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 221,
        (byte) 150,
        (byte) 239,
        (byte) 10,
        (byte) 172,
        (byte) 29,
        (byte) 121,
        (byte) 71,
        (byte) 9,
        (byte) 85,
        (byte) 154,
        (byte) 225,
        (byte) 113,
        (byte) 35,
        (byte) 203,
        (byte) 137
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[15] = (byte) 136;
      numArray3[1] = (byte) 3;
      numArray3[2] = (byte) 179;
      numArray3[12] = (byte) 144 /*0x90*/;
      numArray3[4] = (byte) 201;
      numArray3[5] = (byte) 35;
      numArray3[13] = (byte) 147;
      numArray3[7] = (byte) 52;
      numArray3[9] = (byte) 167;
      numArray3[6] = (byte) 243;
      numArray3[0] = (byte) 74;
      numArray3[11] = (byte) 138;
      numArray3[8] = (byte) 191;
      numArray3[10] = (byte) 195;
      numArray3[3] = (byte) 95;
      numArray3[14] = (byte) 82;
      key.Query(true, 343, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[31 /*0x1F*/];
      byte[] response = new byte[31 /*0x1F*/];
      Array.Copy((Array) sc_7892.sspq, 0, (Array) numArray4, 0, 31 /*0x1F*/);
      key.Query(true, 343, numArray4, response);
      Array.Copy((Array) sc_7892.sspr, 0, (Array) numArray4, 0, 31 /*0x1F*/);
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
    byte[] numArray5 = new byte[16 /*0x10*/];
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 250,
      (byte) 74,
      (byte) 213,
      (byte) 128 /*0x80*/,
      (byte) 230,
      (byte) 99,
      (byte) 56,
      (byte) 124,
      (byte) 41,
      (byte) 28,
      (byte) 240 /*0xF0*/,
      (byte) 182,
      (byte) 252,
      (byte) 186,
      (byte) 2,
      (byte) 34
    };
    byte[] numArray7 = new byte[16 /*0x10*/]
    {
      (byte) 224 /*0xE0*/,
      (byte) 212,
      (byte) 60,
      (byte) 208 /*0xD0*/,
      (byte) 239,
      (byte) 177,
      (byte) 110,
      (byte) 161,
      (byte) 39,
      (byte) 244,
      (byte) 77,
      (byte) 145,
      (byte) 66,
      (byte) 153,
      (byte) 140,
      (byte) 212
    };
    key.Query(true, 343, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
