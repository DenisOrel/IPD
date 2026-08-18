// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_660
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_660
{
  private static byte[] sspq = new byte[61]
  {
    (byte) 47,
    (byte) 39,
    (byte) 78,
    (byte) 72,
    (byte) 225,
    (byte) 179,
    (byte) 23,
    (byte) 228,
    (byte) 82,
    (byte) 142,
    (byte) 72,
    (byte) 203,
    (byte) 190,
    (byte) 38,
    (byte) 128 /*0x80*/,
    (byte) 87,
    (byte) 107,
    (byte) 18,
    (byte) 57,
    (byte) 152,
    (byte) 21,
    (byte) 68,
    (byte) 188,
    (byte) 115,
    (byte) 133,
    (byte) 62,
    (byte) 69,
    (byte) 250,
    (byte) 241,
    (byte) 246,
    (byte) 40,
    (byte) 167,
    (byte) 174,
    (byte) 185,
    (byte) 114,
    (byte) 113,
    (byte) 10,
    (byte) 154,
    (byte) 159,
    (byte) 242,
    (byte) 60,
    (byte) 0,
    (byte) 18,
    (byte) 65,
    (byte) 139,
    (byte) 214,
    (byte) 11,
    (byte) 55,
    (byte) 73,
    (byte) 1,
    (byte) 30,
    (byte) 78,
    (byte) 73,
    (byte) 242,
    (byte) 220,
    (byte) 116,
    (byte) 225,
    (byte) 115,
    (byte) 64 /*0x40*/,
    (byte) 207,
    (byte) 69
  };
  private static byte[] sspr = new byte[61]
  {
    (byte) 94,
    (byte) 34,
    (byte) 117,
    (byte) 48 /*0x30*/,
    (byte) 51,
    (byte) 158,
    (byte) 48 /*0x30*/,
    (byte) 70,
    (byte) 50,
    (byte) 239,
    (byte) 49,
    (byte) 204,
    (byte) 228,
    (byte) 31 /*0x1F*/,
    (byte) 204,
    (byte) 230,
    (byte) 39,
    (byte) 109,
    (byte) 16 /*0x10*/,
    (byte) 29,
    (byte) 247,
    (byte) 201,
    (byte) 65,
    (byte) 39,
    (byte) 58,
    (byte) 35,
    (byte) 129,
    (byte) 27,
    (byte) 224 /*0xE0*/,
    (byte) 25,
    (byte) 213,
    (byte) 165,
    (byte) 67,
    (byte) 161,
    (byte) 155,
    (byte) 237,
    (byte) 210,
    (byte) 162,
    (byte) 189,
    (byte) 225,
    (byte) 174,
    (byte) 85,
    (byte) 73,
    (byte) 249,
    (byte) 64 /*0x40*/,
    (byte) 251,
    (byte) 49,
    (byte) 46,
    (byte) 198,
    (byte) 237,
    (byte) 232,
    (byte) 112 /*0x70*/,
    (byte) 29,
    (byte) 50,
    (byte) 161,
    (byte) 17,
    (byte) 41,
    (byte) 239,
    (byte) 78,
    (byte) 240 /*0xF0*/,
    (byte) 55
  };

  internal static string ssp_automatch_661()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 183,
        (byte) 3,
        (byte) 1,
        (byte) 95,
        (byte) 95,
        (byte) 60,
        (byte) 173,
        (byte) 199,
        (byte) 83,
        (byte) 34,
        (byte) 123,
        (byte) 199,
        (byte) 202,
        (byte) 40,
        (byte) 177,
        (byte) 88,
        (byte) 161,
        (byte) 86,
        (byte) 31 /*0x1F*/,
        (byte) 59,
        (byte) 196,
        (byte) 12,
        (byte) 215
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 166,
        (byte) 123,
        (byte) 137,
        (byte) 96 /*0x60*/,
        (byte) 152,
        (byte) 168,
        (byte) 147,
        (byte) 172,
        (byte) 78,
        (byte) 88,
        (byte) 240 /*0xF0*/,
        (byte) 103,
        (byte) 182,
        (byte) 65,
        (byte) 252,
        (byte) 79,
        (byte) 138,
        (byte) 11,
        (byte) 146,
        (byte) 165,
        (byte) 178,
        (byte) 50,
        (byte) 226
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[13];
      byte[] response = new byte[13];
      Array.Copy((Array) sc_660.sspq, 0, (Array) numArray4, 0, 13);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_660.sspr, 0, (Array) numArray4, 0, 13);
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
    byte[] numArray6 = new byte[23];
    numArray6[22] = (byte) 160 /*0xA0*/;
    numArray6[20] = (byte) 172;
    numArray6[6] = (byte) 56;
    numArray6[3] = (byte) 30;
    numArray6[21] = (byte) 126;
    numArray6[16 /*0x10*/] = (byte) 43;
    numArray6[17] = (byte) 14;
    numArray6[7] = (byte) 130;
    numArray6[12] = (byte) 137;
    numArray6[19] = (byte) 96 /*0x60*/;
    numArray6[8] = (byte) 164;
    numArray6[11] = (byte) 128 /*0x80*/;
    numArray6[1] = (byte) 56;
    numArray6[13] = (byte) 42;
    numArray6[14] = (byte) 58;
    numArray6[9] = (byte) 195;
    numArray6[5] = (byte) 135;
    numArray6[10] = (byte) 74;
    numArray6[2] = (byte) 26;
    numArray6[18] = (byte) 241;
    numArray6[15] = (byte) 171;
    numArray6[0] = (byte) 183;
    numArray6[4] = (byte) 86;
    byte[] numArray7 = new byte[23]
    {
      (byte) 185,
      (byte) 105,
      (byte) 202,
      (byte) 242,
      (byte) 166,
      (byte) 23,
      (byte) 146,
      (byte) 100,
      (byte) 40,
      (byte) 98,
      (byte) 149,
      (byte) 168,
      (byte) 150,
      (byte) 90,
      (byte) 87,
      (byte) 14,
      (byte) 249,
      (byte) 101,
      (byte) 61,
      (byte) 235,
      (byte) 134,
      (byte) 144 /*0x90*/,
      (byte) 137
    };
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    byte[] numArray8 = new byte[48 /*0x30*/];
    byte[] response1 = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_660.sspq, 13, (Array) numArray8, 0, 48 /*0x30*/);
    key.Query(true, 338, numArray8, response1);
    Array.Copy((Array) sc_660.sspr, 13, (Array) numArray8, 0, 48 /*0x30*/);
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
