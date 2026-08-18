// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_746
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_746
{
  private static byte[] sspq = new byte[53]
  {
    (byte) 91,
    (byte) 56,
    (byte) 176 /*0xB0*/,
    (byte) 128 /*0x80*/,
    (byte) 208 /*0xD0*/,
    (byte) 192 /*0xC0*/,
    (byte) 245,
    (byte) 236,
    (byte) 13,
    (byte) 52,
    (byte) 172,
    (byte) 51,
    (byte) 55,
    (byte) 77,
    (byte) 1,
    (byte) 253,
    (byte) 231,
    (byte) 226,
    (byte) 47,
    (byte) 47,
    (byte) 111,
    (byte) 147,
    (byte) 20,
    (byte) 120,
    (byte) 19,
    (byte) 61,
    (byte) 19,
    (byte) 153,
    (byte) 156,
    (byte) 54,
    (byte) 21,
    (byte) 93,
    (byte) 214,
    (byte) 169,
    (byte) 115,
    (byte) 101,
    (byte) 164,
    (byte) 25,
    (byte) 201,
    (byte) 10,
    (byte) 66,
    (byte) 125,
    (byte) 18,
    (byte) 203,
    (byte) 41,
    (byte) 105,
    (byte) 155,
    (byte) 169,
    (byte) 68,
    (byte) 28,
    (byte) 251,
    (byte) 228,
    (byte) 124
  };
  private static byte[] sspr = new byte[53]
  {
    (byte) 8,
    (byte) 46,
    (byte) 173,
    (byte) 64 /*0x40*/,
    (byte) 58,
    (byte) 7,
    (byte) 75,
    (byte) 113,
    (byte) 250,
    (byte) 208 /*0xD0*/,
    (byte) 15,
    (byte) 58,
    (byte) 159,
    (byte) 88,
    (byte) 57,
    (byte) 97,
    (byte) 17,
    (byte) 194,
    (byte) 109,
    (byte) 152,
    (byte) 24,
    (byte) 102,
    (byte) 177,
    (byte) 40,
    (byte) 182,
    (byte) 98,
    (byte) 52,
    (byte) 187,
    (byte) 186,
    (byte) 66,
    (byte) 41,
    (byte) 2,
    (byte) 134,
    (byte) 240 /*0xF0*/,
    (byte) 47,
    (byte) 215,
    (byte) 4,
    (byte) 45,
    (byte) 225,
    (byte) 191,
    (byte) 154,
    (byte) 53,
    (byte) 47,
    (byte) 98,
    (byte) 65,
    (byte) 219,
    (byte) 65,
    (byte) 11,
    (byte) 120,
    (byte) 203,
    (byte) 202,
    (byte) 68,
    (byte) 177
  };

  internal static string ssp_automatch_747()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[23];
      byte[] numArray2 = new byte[23];
      numArray2[13] = (byte) 250;
      numArray2[16 /*0x10*/] = (byte) 179;
      numArray2[3] = (byte) 38;
      numArray2[1] = (byte) 213;
      numArray2[4] = (byte) 16 /*0x10*/;
      numArray2[5] = (byte) 181;
      numArray2[15] = (byte) 217;
      numArray2[9] = (byte) 159;
      numArray2[8] = (byte) 29;
      numArray2[10] = (byte) 82;
      numArray2[21] = (byte) 10;
      numArray2[11] = (byte) 129;
      numArray2[17] = (byte) 20;
      numArray2[2] = (byte) 163;
      numArray2[14] = (byte) 103;
      numArray2[12] = (byte) 124;
      numArray2[7] = (byte) 187;
      numArray2[0] = (byte) 203;
      numArray2[18] = (byte) 78;
      numArray2[19] = (byte) 176 /*0xB0*/;
      numArray2[20] = (byte) 16 /*0x10*/;
      numArray2[6] = (byte) 127 /*0x7F*/;
      numArray2[22] = (byte) 224 /*0xE0*/;
      byte[] numArray3 = new byte[23]
      {
        (byte) 47,
        (byte) 247,
        (byte) 151,
        (byte) 72,
        (byte) 69,
        (byte) 114,
        (byte) 99,
        byte.MaxValue,
        (byte) 15,
        (byte) 242,
        (byte) 152,
        (byte) 73,
        (byte) 133,
        (byte) 99,
        (byte) 106,
        (byte) 240 /*0xF0*/,
        (byte) 147,
        (byte) 137,
        (byte) 69,
        (byte) 92,
        (byte) 208 /*0xD0*/,
        (byte) 154,
        (byte) 26
      };
      key.Query(true, 338, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 23);
      for (int index = 0; index < 23; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[53];
      byte[] response = new byte[53];
      Array.Copy((Array) sc_746.sspq, 0, (Array) numArray4, 0, 53);
      key.Query(true, 338, numArray4, response);
      Array.Copy((Array) sc_746.sspr, 0, (Array) numArray4, 0, 53);
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
      (byte) 69,
      (byte) 107,
      (byte) 200,
      (byte) 35,
      (byte) 55,
      (byte) 109,
      (byte) 146,
      (byte) 173,
      (byte) 91,
      (byte) 80 /*0x50*/,
      (byte) 215,
      (byte) 31 /*0x1F*/,
      (byte) 78,
      (byte) 93,
      (byte) 141,
      (byte) 218,
      (byte) 156,
      (byte) 89,
      (byte) 205,
      (byte) 25,
      (byte) 36,
      (byte) 237,
      (byte) 241
    };
    byte[] numArray7 = new byte[23];
    numArray7[22] = (byte) 136;
    numArray7[1] = (byte) 31 /*0x1F*/;
    numArray7[16 /*0x10*/] = (byte) 13;
    numArray7[14] = (byte) 38;
    numArray7[3] = (byte) 246;
    numArray7[19] = (byte) 196;
    numArray7[2] = (byte) 22;
    numArray7[7] = (byte) 140;
    numArray7[12] = (byte) 62;
    numArray7[0] = (byte) 103;
    numArray7[10] = (byte) 225;
    numArray7[8] = (byte) 77;
    numArray7[17] = (byte) 177;
    numArray7[13] = (byte) 198;
    numArray7[11] = (byte) 176 /*0xB0*/;
    numArray7[15] = (byte) 241;
    numArray7[9] = (byte) 191;
    numArray7[18] = (byte) 105;
    numArray7[6] = (byte) 60;
    numArray7[4] = (byte) 189;
    numArray7[20] = (byte) 88;
    numArray7[21] = (byte) 41;
    numArray7[5] = (byte) 19;
    key.Query(true, 338, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 23);
    for (int index = 0; index < 23; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
