// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_727
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_727
{
  private static byte[] sspq = new byte[20]
  {
    (byte) 54,
    (byte) 145,
    (byte) 182,
    (byte) 127 /*0x7F*/,
    (byte) 41,
    (byte) 188,
    (byte) 5,
    (byte) 73,
    (byte) 140,
    (byte) 240 /*0xF0*/,
    (byte) 25,
    (byte) 43,
    (byte) 203,
    (byte) 142,
    (byte) 201,
    (byte) 25,
    (byte) 110,
    (byte) 73,
    (byte) 226,
    (byte) 79
  };
  private static byte[] sspr = new byte[20]
  {
    (byte) 1,
    (byte) 15,
    (byte) 31 /*0x1F*/,
    (byte) 214,
    (byte) 65,
    (byte) 23,
    (byte) 37,
    (byte) 185,
    (byte) 18,
    (byte) 81,
    (byte) 142,
    (byte) 181,
    (byte) 82,
    (byte) 27,
    (byte) 143,
    (byte) 66,
    (byte) 3,
    (byte) 22,
    (byte) 60,
    (byte) 84
  };

  internal static string ssp_automatch_728()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23]
      {
        (byte) 215,
        (byte) 205,
        (byte) 79,
        (byte) 130,
        (byte) 118,
        (byte) 140,
        (byte) 76,
        (byte) 129,
        (byte) 120,
        (byte) 149,
        (byte) 115,
        (byte) 80 /*0x50*/,
        (byte) 4,
        (byte) 230,
        (byte) 29,
        (byte) 100,
        (byte) 171,
        (byte) 28,
        (byte) 63 /*0x3F*/,
        (byte) 23,
        (byte) 7,
        (byte) 210,
        (byte) 193
      };
      byte[] numArray3 = new byte[23]
      {
        (byte) 98,
        (byte) 51,
        (byte) 145,
        (byte) 41,
        (byte) 117,
        (byte) 184,
        (byte) 41,
        (byte) 48 /*0x30*/,
        (byte) 121,
        (byte) 54,
        (byte) 5,
        (byte) 184,
        (byte) 127 /*0x7F*/,
        (byte) 133,
        (byte) 106,
        (byte) 85,
        (byte) 107,
        (byte) 19,
        (byte) 168,
        (byte) 55,
        (byte) 119,
        (byte) 29,
        (byte) 185
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[23];
    byte[] numArray5 = new byte[23];
    numArray5[9] = (byte) 213;
    numArray5[19] = (byte) 203;
    numArray5[21] = (byte) 129;
    numArray5[3] = (byte) 201;
    numArray5[7] = (byte) 56;
    numArray5[5] = (byte) 186;
    numArray5[6] = (byte) 228;
    numArray5[12] = (byte) 61;
    numArray5[8] = (byte) 147;
    numArray5[0] = (byte) 62;
    numArray5[10] = (byte) 26;
    numArray5[1] = (byte) 16 /*0x10*/;
    numArray5[4] = (byte) 122;
    numArray5[11] = (byte) 166;
    numArray5[17] = (byte) 48 /*0x30*/;
    numArray5[15] = (byte) 195;
    numArray5[16 /*0x10*/] = (byte) 155;
    numArray5[13] = (byte) 48 /*0x30*/;
    numArray5[18] = (byte) 151;
    numArray5[14] = (byte) 140;
    numArray5[20] = (byte) 89;
    numArray5[2] = (byte) 86;
    numArray5[22] = (byte) 11;
    byte[] numArray6 = new byte[23]
    {
      (byte) 168,
      (byte) 22,
      (byte) 196,
      (byte) 2,
      (byte) 48 /*0x30*/,
      (byte) 112 /*0x70*/,
      (byte) 213,
      (byte) 16 /*0x10*/,
      (byte) 75,
      (byte) 36,
      (byte) 227,
      (byte) 83,
      (byte) 219,
      (byte) 48 /*0x30*/,
      (byte) 230,
      (byte) 69,
      (byte) 198,
      (byte) 149,
      (byte) 92,
      (byte) 90,
      (byte) 104,
      (byte) 30,
      (byte) 3
    };
    key.Query(true, 338, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_727.sspq, 0, (Array) numArray7, 0, 20);
    key.Query(true, 338, numArray7, response);
    Array.Copy((Array) sc_727.sspr, 0, (Array) numArray7, 0, 20);
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
