// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_693
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_693
{
  private static byte[] sspq = new byte[22]
  {
    (byte) 39,
    (byte) 18,
    (byte) 149,
    (byte) 234,
    (byte) 151,
    (byte) 227,
    (byte) 220,
    (byte) 254,
    (byte) 191,
    (byte) 197,
    (byte) 155,
    (byte) 3,
    (byte) 71,
    (byte) 83,
    (byte) 183,
    (byte) 119,
    (byte) 10,
    (byte) 225,
    (byte) 120,
    (byte) 69,
    (byte) 21,
    (byte) 174
  };
  private static byte[] sspr = new byte[22]
  {
    (byte) 221,
    (byte) 61,
    (byte) 90,
    (byte) 180,
    (byte) 102,
    (byte) 115,
    (byte) 127 /*0x7F*/,
    (byte) 35,
    (byte) 27,
    (byte) 165,
    (byte) 172,
    (byte) 34,
    (byte) 240 /*0xF0*/,
    (byte) 35,
    (byte) 24,
    (byte) 202,
    (byte) 159,
    (byte) 113,
    (byte) 210,
    (byte) 33,
    (byte) 66,
    byte.MaxValue
  };

  internal static string ssp_automatch_694()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[3] = (byte) 81;
      numArray2[1] = (byte) 59;
      numArray2[2] = (byte) 61;
      numArray2[10] = (byte) 102;
      numArray2[18] = (byte) 16 /*0x10*/;
      numArray2[5] = (byte) 100;
      numArray2[6] = (byte) 79;
      numArray2[7] = (byte) 195;
      numArray2[8] = (byte) 247;
      numArray2[0] = (byte) 117;
      numArray2[15] = (byte) 212;
      numArray2[11] = (byte) 233;
      numArray2[12] = (byte) 244;
      numArray2[21] = (byte) 229;
      numArray2[16 /*0x10*/] = (byte) 175;
      numArray2[13] = (byte) 162;
      numArray2[14] = (byte) 186;
      numArray2[17] = (byte) 244;
      numArray2[4] = (byte) 127 /*0x7F*/;
      numArray2[19] = (byte) 176 /*0xB0*/;
      numArray2[20] = (byte) 95;
      numArray2[9] = (byte) 238;
      numArray2[22] = (byte) 3;
      byte[] numArray3 = new byte[23];
      numArray3[4] = (byte) 247;
      numArray3[1] = (byte) 12;
      numArray3[2] = (byte) 38;
      numArray3[3] = (byte) 146;
      numArray3[0] = (byte) 171;
      numArray3[15] = (byte) 135;
      numArray3[5] = (byte) 128 /*0x80*/;
      numArray3[7] = (byte) 233;
      numArray3[9] = (byte) 206;
      numArray3[21] = (byte) 73;
      numArray3[10] = (byte) 184;
      numArray3[11] = (byte) 49;
      numArray3[8] = (byte) 244;
      numArray3[13] = (byte) 245;
      numArray3[16 /*0x10*/] = (byte) 41;
      numArray3[14] = (byte) 76;
      numArray3[18] = (byte) 183;
      numArray3[6] = (byte) 108;
      numArray3[20] = (byte) 140;
      numArray3[19] = (byte) 4;
      numArray3[12] = (byte) 111;
      numArray3[17] = (byte) 235;
      numArray3[22] = (byte) 135;
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 173,
      (byte) 113,
      (byte) 35,
      (byte) 169,
      (byte) 252,
      (byte) 134,
      (byte) 225,
      (byte) 177,
      (byte) 70,
      (byte) 125,
      (byte) 62,
      (byte) 20,
      (byte) 189,
      (byte) 208 /*0xD0*/,
      (byte) 103,
      (byte) 50,
      (byte) 203,
      (byte) 248,
      (byte) 174,
      (byte) 160 /*0xA0*/,
      (byte) 34,
      (byte) 65,
      (byte) 254
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 86,
      (byte) 160 /*0xA0*/,
      (byte) 39,
      (byte) 136,
      (byte) 216,
      (byte) 30,
      (byte) 30,
      (byte) 146,
      (byte) 81,
      (byte) 130,
      (byte) 15,
      (byte) 63 /*0x3F*/,
      (byte) 84,
      (byte) 152,
      (byte) 249,
      (byte) 207,
      (byte) 246,
      (byte) 183,
      (byte) 141,
      (byte) 134,
      (byte) 181,
      (byte) 64 /*0x40*/,
      (byte) 182
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_695()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 162,
        (byte) 250,
        (byte) 142,
        (byte) 44,
        (byte) 136,
        (byte) 101,
        (byte) 14,
        (byte) 25,
        (byte) 252,
        (byte) 129,
        (byte) 218,
        (byte) 147,
        (byte) 176 /*0xB0*/,
        (byte) 70,
        (byte) 224 /*0xE0*/,
        (byte) 191,
        (byte) 35,
        (byte) 116,
        (byte) 39,
        (byte) 115,
        (byte) 133,
        (byte) 42,
        (byte) 223
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 141,
        (byte) 242,
        (byte) 16 /*0x10*/,
        (byte) 232,
        (byte) 68,
        (byte) 55,
        (byte) 161,
        (byte) 96 /*0x60*/,
        (byte) 155,
        (byte) 49,
        (byte) 175,
        (byte) 243,
        (byte) 230,
        (byte) 190,
        (byte) 130,
        (byte) 31 /*0x1F*/,
        (byte) 233,
        (byte) 241,
        (byte) 68,
        (byte) 214,
        (byte) 232,
        (byte) 247,
        (byte) 125
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23]
    {
      (byte) 108,
      (byte) 215,
      (byte) 108,
      (byte) 115,
      (byte) 73,
      (byte) 47,
      (byte) 67,
      (byte) 96 /*0x60*/,
      (byte) 62,
      (byte) 182,
      (byte) 184,
      (byte) 81,
      (byte) 182,
      (byte) 175,
      (byte) 174,
      (byte) 183,
      (byte) 106,
      (byte) 176 /*0xB0*/,
      (byte) 241,
      (byte) 101,
      (byte) 254,
      (byte) 90,
      (byte) 65
    };
    byte[] numArray6 = new byte[23]
    {
      (byte) 250,
      (byte) 236,
      (byte) 110,
      (byte) 163,
      (byte) 184,
      (byte) 137,
      (byte) 81,
      (byte) 228,
      (byte) 163,
      (byte) 208 /*0xD0*/,
      (byte) 227,
      (byte) 251,
      (byte) 177,
      (byte) 235,
      (byte) 184,
      (byte) 240 /*0xF0*/,
      (byte) 11,
      (byte) 174,
      (byte) 134,
      (byte) 176 /*0xB0*/,
      (byte) 226,
      (byte) 80 /*0x50*/,
      (byte) 236
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_automatch_696()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 69,
        (byte) 160 /*0xA0*/,
        (byte) 120,
        (byte) 27,
        (byte) 181,
        (byte) 9,
        (byte) 98,
        (byte) 248,
        (byte) 55,
        (byte) 173,
        (byte) 136,
        (byte) 56,
        (byte) 171,
        (byte) 79,
        (byte) 79,
        (byte) 7,
        (byte) 42,
        (byte) 7,
        (byte) 209,
        (byte) 190,
        (byte) 119,
        (byte) 196,
        (byte) 206
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 232,
        (byte) 170,
        (byte) 139,
        (byte) 225,
        (byte) 136,
        (byte) 178,
        (byte) 188,
        (byte) 231,
        (byte) 246,
        (byte) 97,
        (byte) 232,
        (byte) 135,
        (byte) 54,
        (byte) 190,
        (byte) 206,
        (byte) 29,
        (byte) 162,
        (byte) 101,
        (byte) 55,
        (byte) 15,
        (byte) 32 /*0x20*/,
        byte.MaxValue,
        (byte) 109
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[22];
      byte[] response = new byte[22];
      Array.Copy((Array) sc_693.sspq, 0, (Array) numArray4, 0, 22);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_693.sspr, 0, (Array) numArray4, 0, 22);
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
    byte[] numArray5 = new byte[23];
    byte[] numArray6 = new byte[23]
    {
      (byte) 82,
      (byte) 23,
      (byte) 122,
      (byte) 9,
      (byte) 166,
      (byte) 218,
      (byte) 128 /*0x80*/,
      (byte) 210,
      (byte) 185,
      (byte) 237,
      (byte) 205,
      (byte) 76,
      (byte) 59,
      (byte) 229,
      (byte) 161,
      (byte) 194,
      (byte) 241,
      (byte) 84,
      (byte) 206,
      (byte) 80 /*0x50*/,
      (byte) 143,
      (byte) 65,
      (byte) 142
    };
    byte[] numArray7 = new byte[23]
    {
      (byte) 71,
      (byte) 105,
      (byte) 132,
      (byte) 190,
      (byte) 75,
      (byte) 76,
      (byte) 54,
      (byte) 168,
      (byte) 122,
      (byte) 50,
      (byte) 248,
      (byte) 70,
      (byte) 65,
      (byte) 125,
      (byte) 116,
      (byte) 111,
      (byte) 214,
      (byte) 207,
      (byte) 237,
      (byte) 245,
      (byte) 150,
      (byte) 46,
      (byte) 216
    };
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
