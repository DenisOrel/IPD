// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19451
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19451
{
  private static byte[] sspq = new byte[45]
  {
    (byte) 13,
    (byte) 8,
    (byte) 161,
    (byte) 88,
    (byte) 77,
    (byte) 151,
    (byte) 29,
    (byte) 103,
    (byte) 212,
    (byte) 60,
    (byte) 125,
    (byte) 100,
    (byte) 150,
    (byte) 248,
    (byte) 202,
    (byte) 150,
    (byte) 195,
    (byte) 203,
    (byte) 32 /*0x20*/,
    (byte) 221,
    (byte) 63 /*0x3F*/,
    (byte) 57,
    (byte) 246,
    (byte) 227,
    (byte) 195,
    (byte) 246,
    (byte) 210,
    (byte) 227,
    (byte) 23,
    (byte) 195,
    (byte) 3,
    byte.MaxValue,
    (byte) 240 /*0xF0*/,
    (byte) 234,
    (byte) 226,
    (byte) 174,
    (byte) 231,
    (byte) 212,
    (byte) 152,
    (byte) 158,
    (byte) 66,
    (byte) 18,
    (byte) 242,
    (byte) 213,
    (byte) 192 /*0xC0*/
  };
  private static byte[] sspr = new byte[45]
  {
    (byte) 139,
    (byte) 216,
    (byte) 144 /*0x90*/,
    (byte) 236,
    (byte) 153,
    (byte) 195,
    (byte) 213,
    (byte) 13,
    (byte) 69,
    (byte) 230,
    (byte) 130,
    (byte) 8,
    (byte) 131,
    (byte) 187,
    (byte) 48 /*0x30*/,
    (byte) 195,
    (byte) 117,
    (byte) 113,
    (byte) 224 /*0xE0*/,
    (byte) 47,
    (byte) 168,
    (byte) 245,
    (byte) 14,
    (byte) 102,
    (byte) 236,
    (byte) 234,
    (byte) 19,
    (byte) 141,
    (byte) 50,
    (byte) 84,
    (byte) 28,
    (byte) 25,
    (byte) 110,
    (byte) 14,
    (byte) 3,
    (byte) 122,
    (byte) 244,
    (byte) 87,
    (byte) 210,
    (byte) 154,
    (byte) 163,
    (byte) 19,
    (byte) 90,
    (byte) 223,
    (byte) 107
  };

  internal static string ssp_techcard_19452()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 113,
        (byte) 105,
        (byte) 240 /*0xF0*/,
        (byte) 183,
        (byte) 136,
        (byte) 45,
        (byte) 229,
        (byte) 53,
        (byte) 95,
        (byte) 122,
        (byte) 199,
        (byte) 18,
        (byte) 62,
        (byte) 195,
        (byte) 80 /*0x50*/,
        (byte) 242,
        (byte) 108,
        (byte) 35,
        (byte) 159
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 164,
        (byte) 76,
        (byte) 170,
        (byte) 215,
        (byte) 163,
        (byte) 98,
        (byte) 136,
        (byte) 16 /*0x10*/,
        (byte) 155,
        (byte) 101,
        (byte) 27,
        (byte) 46,
        (byte) 68,
        (byte) 116,
        (byte) 136,
        (byte) 62,
        (byte) 245,
        (byte) 53,
        (byte) 206
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[45];
      byte[] response = new byte[45];
      Array.Copy((Array) sc_19451.sspq, 0, (Array) numArray4, 0, 45);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19451.sspr, 0, (Array) numArray4, 0, 45);
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
    byte[] numArray5 = new byte[19];
    byte[] numArray6 = new byte[19]
    {
      (byte) 95,
      (byte) 186,
      (byte) 250,
      (byte) 161,
      (byte) 141,
      (byte) 220,
      (byte) 163,
      (byte) 82,
      (byte) 163,
      (byte) 64 /*0x40*/,
      (byte) 212,
      (byte) 18,
      (byte) 189,
      (byte) 149,
      (byte) 87,
      (byte) 196,
      (byte) 57,
      (byte) 80 /*0x50*/,
      (byte) 17
    };
    byte[] numArray7 = new byte[19];
    numArray7[2] = (byte) 103;
    numArray7[11] = (byte) 80 /*0x50*/;
    numArray7[17] = (byte) 238;
    numArray7[3] = (byte) 15;
    numArray7[14] = (byte) 125;
    numArray7[1] = (byte) 13;
    numArray7[6] = (byte) 238;
    numArray7[5] = (byte) 32 /*0x20*/;
    numArray7[0] = (byte) 189;
    numArray7[9] = (byte) 222;
    numArray7[4] = (byte) 223;
    numArray7[18] = (byte) 225;
    numArray7[12] = (byte) 236;
    numArray7[13] = (byte) 206;
    numArray7[15] = (byte) 149;
    numArray7[10] = (byte) 154;
    numArray7[8] = (byte) 239;
    numArray7[7] = (byte) 122;
    numArray7[16 /*0x10*/] = (byte) 101;
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
