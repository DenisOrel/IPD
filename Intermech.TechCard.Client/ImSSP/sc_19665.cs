// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19665
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19665
{
  private static byte[] sspq = new byte[12]
  {
    (byte) 15,
    (byte) 174,
    (byte) 24,
    (byte) 35,
    (byte) 125,
    (byte) 14,
    (byte) 244,
    (byte) 11,
    (byte) 84,
    (byte) 215,
    (byte) 73,
    (byte) 247
  };
  private static byte[] sspr = new byte[12]
  {
    (byte) 215,
    (byte) 166,
    (byte) 31 /*0x1F*/,
    (byte) 193,
    (byte) 140,
    (byte) 60,
    (byte) 250,
    (byte) 152,
    (byte) 194,
    (byte) 82,
    (byte) 138,
    (byte) 116
  };

  internal static string ssp_techcard_19666()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 222,
        (byte) 133,
        (byte) 237,
        (byte) 231,
        (byte) 81,
        (byte) 163,
        (byte) 119,
        (byte) 67,
        (byte) 64 /*0x40*/,
        (byte) 227,
        (byte) 45,
        (byte) 249,
        (byte) 59,
        (byte) 75,
        (byte) 213,
        (byte) 7,
        (byte) 177,
        (byte) 143,
        (byte) 143
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 154,
        (byte) 7,
        (byte) 98,
        (byte) 144 /*0x90*/,
        (byte) 101,
        (byte) 214,
        (byte) 57,
        (byte) 198,
        (byte) 103,
        (byte) 247,
        (byte) 202,
        (byte) 15,
        (byte) 97,
        (byte) 234,
        (byte) 29,
        (byte) 75,
        (byte) 68,
        (byte) 216,
        (byte) 19
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 241,
      (byte) 188,
      (byte) 242,
      (byte) 222,
      (byte) 254,
      (byte) 188,
      (byte) 95,
      (byte) 56,
      (byte) 178,
      (byte) 76,
      (byte) 144 /*0x90*/,
      (byte) 114,
      (byte) 59,
      (byte) 38,
      (byte) 239,
      (byte) 142,
      (byte) 18,
      (byte) 129,
      (byte) 145
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 245,
      (byte) 193,
      (byte) 73,
      (byte) 178,
      (byte) 41,
      (byte) 40,
      (byte) 118,
      (byte) 37,
      (byte) 143,
      (byte) 226,
      (byte) 5,
      (byte) 237,
      (byte) 183,
      (byte) 169,
      (byte) 172,
      (byte) 88,
      (byte) 99,
      (byte) 246,
      (byte) 57
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_techcard_19667()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 250,
        (byte) 81,
        (byte) 253,
        (byte) 37,
        (byte) 154,
        (byte) 3,
        (byte) 48 /*0x30*/,
        (byte) 134,
        (byte) 63 /*0x3F*/,
        (byte) 103,
        (byte) 37,
        (byte) 148,
        (byte) 48 /*0x30*/,
        (byte) 245,
        (byte) 115,
        (byte) 164,
        (byte) 186,
        (byte) 227,
        (byte) 8
      };
      byte[] numArray3 = new byte[19]
      {
        (byte) 128 /*0x80*/,
        (byte) 93,
        (byte) 28,
        (byte) 187,
        (byte) 29,
        (byte) 243,
        (byte) 252,
        (byte) 145,
        (byte) 56,
        (byte) 5,
        (byte) 87,
        (byte) 76,
        (byte) 127 /*0x7F*/,
        (byte) 87,
        (byte) 158,
        (byte) 112 /*0x70*/,
        (byte) 85,
        (byte) 44,
        (byte) 242
      };
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_19665.sspq, 0, (Array) numArray4, 0, 12);
      key.Query(true, 359, numArray4, response);
      Array.Copy((Array) sc_19665.sspr, 0, (Array) numArray4, 0, 12);
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
      (byte) 140,
      (byte) 1,
      (byte) 151,
      (byte) 197,
      (byte) 53,
      (byte) 129,
      (byte) 197,
      (byte) 81,
      (byte) 11,
      (byte) 22,
      (byte) 145,
      (byte) 190,
      (byte) 153,
      (byte) 159,
      (byte) 220,
      (byte) 214,
      (byte) 25,
      (byte) 92,
      (byte) 232
    };
    byte[] numArray7 = new byte[19]
    {
      (byte) 139,
      (byte) 52,
      (byte) 88,
      (byte) 67,
      (byte) 181,
      (byte) 89,
      (byte) 71,
      (byte) 206,
      (byte) 11,
      (byte) 27,
      (byte) 81,
      (byte) 148,
      (byte) 201,
      (byte) 70,
      (byte) 191,
      (byte) 130,
      (byte) 62,
      (byte) 83,
      (byte) 82
    };
    key.Query(true, 359, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
