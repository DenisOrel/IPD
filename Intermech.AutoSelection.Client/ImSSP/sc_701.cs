// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_701
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_701
{
  private static byte[] sspq = new byte[38]
  {
    (byte) 32 /*0x20*/,
    (byte) 229,
    (byte) 241,
    (byte) 214,
    (byte) 248,
    (byte) 91,
    (byte) 10,
    (byte) 147,
    (byte) 2,
    (byte) 52,
    (byte) 67,
    (byte) 115,
    (byte) 197,
    (byte) 133,
    (byte) 148,
    (byte) 42,
    (byte) 62,
    (byte) 239,
    (byte) 102,
    byte.MaxValue,
    (byte) 250,
    (byte) 50,
    (byte) 36,
    (byte) 117,
    (byte) 119,
    (byte) 176 /*0xB0*/,
    (byte) 124,
    (byte) 198,
    (byte) 114,
    (byte) 20,
    (byte) 187,
    (byte) 97,
    (byte) 227,
    (byte) 237,
    (byte) 30,
    (byte) 159,
    (byte) 75,
    (byte) 129
  };
  private static byte[] sspr = new byte[38]
  {
    (byte) 201,
    (byte) 144 /*0x90*/,
    (byte) 15,
    (byte) 126,
    (byte) 83,
    (byte) 11,
    (byte) 91,
    (byte) 166,
    (byte) 140,
    (byte) 148,
    (byte) 33,
    (byte) 62,
    (byte) 41,
    (byte) 23,
    (byte) 254,
    (byte) 10,
    (byte) 43,
    (byte) 220,
    (byte) 131,
    (byte) 249,
    (byte) 54,
    (byte) 107,
    (byte) 69,
    (byte) 163,
    (byte) 144 /*0x90*/,
    (byte) 135,
    (byte) 127 /*0x7F*/,
    (byte) 75,
    (byte) 221,
    (byte) 122,
    (byte) 48 /*0x30*/,
    (byte) 188,
    (byte) 172,
    (byte) 6,
    (byte) 60,
    (byte) 163,
    (byte) 15,
    (byte) 190
  };

  internal static string ssp_automatch_702()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[15] = (byte) 101;
      numArray2[17] = (byte) 180;
      numArray2[9] = (byte) 25;
      numArray2[3] = (byte) 220;
      numArray2[4] = (byte) 108;
      numArray2[13] = (byte) 139;
      numArray2[6] = (byte) 177;
      numArray2[1] = (byte) 214;
      numArray2[8] = (byte) 196;
      numArray2[5] = (byte) 75;
      numArray2[16 /*0x10*/] = (byte) 176 /*0xB0*/;
      numArray2[18] = (byte) 12;
      numArray2[22] = (byte) 203;
      numArray2[7] = (byte) 146;
      numArray2[14] = (byte) 58;
      numArray2[10] = (byte) 103;
      numArray2[0] = (byte) 189;
      numArray2[11] = (byte) 150;
      numArray2[19] = (byte) 154;
      numArray2[2] = (byte) 151;
      numArray2[12] = (byte) 0;
      numArray2[21] = (byte) 98;
      numArray2[20] = (byte) 238;
      byte[] numArray3 = new byte[23]
      {
        (byte) 187,
        (byte) 55,
        (byte) 18,
        (byte) 243,
        (byte) 182,
        (byte) 237,
        (byte) 135,
        (byte) 41,
        (byte) 223,
        (byte) 147,
        (byte) 62,
        (byte) 35,
        (byte) 205,
        (byte) 232,
        (byte) 53,
        (byte) 247,
        (byte) 172,
        (byte) 55,
        (byte) 245,
        (byte) 121,
        (byte) 33,
        (byte) 132,
        (byte) 88
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[38];
      byte[] response = new byte[38];
      Array.Copy((Array) sc_701.sspq, 0, (Array) numArray4, 0, 38);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_701.sspr, 0, (Array) numArray4, 0, 38);
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
      (byte) 23,
      (byte) 201,
      (byte) 47,
      (byte) 215,
      (byte) 123,
      (byte) 91,
      (byte) 83,
      (byte) 175,
      (byte) 120,
      (byte) 95,
      (byte) 112 /*0x70*/,
      (byte) 103,
      (byte) 135,
      (byte) 140,
      (byte) 64 /*0x40*/,
      (byte) 91,
      (byte) 229,
      (byte) 237,
      (byte) 193,
      (byte) 124,
      (byte) 94,
      (byte) 72,
      (byte) 222
    };
    byte[] numArray7 = new byte[23]
    {
      (byte) 252,
      (byte) 136,
      (byte) 170,
      (byte) 2,
      (byte) 30,
      (byte) 73,
      (byte) 91,
      (byte) 42,
      (byte) 7,
      (byte) 136,
      (byte) 93,
      (byte) 131,
      (byte) 58,
      (byte) 237,
      (byte) 103,
      (byte) 215,
      (byte) 112 /*0x70*/,
      (byte) 222,
      (byte) 135,
      (byte) 146,
      (byte) 246,
      (byte) 83,
      (byte) 132
    };
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
