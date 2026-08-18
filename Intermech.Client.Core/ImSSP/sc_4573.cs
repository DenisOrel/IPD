
// Type: ImSSP.sc_4573
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4573
{
  private static byte[] sspq = new byte[42]
  {
    (byte) 28,
    (byte) 178,
    (byte) 238,
    (byte) 74,
    (byte) 161,
    (byte) 173,
    (byte) 7,
    (byte) 60,
    (byte) 165,
    (byte) 101,
    (byte) 130,
    (byte) 197,
    (byte) 128 /*0x80*/,
    (byte) 235,
    (byte) 156,
    (byte) 98,
    (byte) 193,
    (byte) 5,
    (byte) 222,
    (byte) 66,
    (byte) 189,
    (byte) 189,
    byte.MaxValue,
    (byte) 225,
    (byte) 58,
    (byte) 105,
    (byte) 115,
    (byte) 169,
    (byte) 143,
    (byte) 84,
    (byte) 83,
    (byte) 151,
    (byte) 26,
    (byte) 35,
    (byte) 172,
    (byte) 117,
    (byte) 228,
    (byte) 48 /*0x30*/,
    (byte) 194,
    (byte) 173,
    (byte) 22,
    (byte) 149
  };
  private static byte[] sspr = new byte[42]
  {
    (byte) 90,
    (byte) 187,
    byte.MaxValue,
    (byte) 186,
    (byte) 24,
    (byte) 226,
    (byte) 186,
    (byte) 124,
    (byte) 46,
    (byte) 252,
    (byte) 156,
    (byte) 79,
    (byte) 19,
    (byte) 167,
    (byte) 118,
    (byte) 157,
    (byte) 107,
    (byte) 179,
    (byte) 237,
    (byte) 187,
    (byte) 75,
    (byte) 111,
    (byte) 145,
    (byte) 49,
    (byte) 226,
    (byte) 31 /*0x1F*/,
    (byte) 216,
    byte.MaxValue,
    (byte) 39,
    (byte) 15,
    (byte) 149,
    (byte) 58,
    (byte) 57,
    (byte) 121,
    (byte) 159,
    (byte) 210,
    (byte) 1,
    (byte) 236,
    (byte) 159,
    (byte) 125,
    (byte) 27,
    (byte) 138
  };

  internal static string ssp_imclient_4574()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[8];
      byte[] numArray2 = new byte[8]
      {
        (byte) 198,
        (byte) 9,
        (byte) 156,
        (byte) 159,
        (byte) 161,
        (byte) 64 /*0x40*/,
        (byte) 129,
        (byte) 146
      };
      byte[] numArray3 = new byte[8]
      {
        (byte) 142,
        (byte) 208 /*0xD0*/,
        (byte) 118,
        (byte) 72,
        (byte) 184,
        (byte) 195,
        (byte) 210,
        (byte) 199
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 8);
      for (int index = 0; index < 8; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[8];
    byte[] numArray5 = new byte[8]
    {
      (byte) 53,
      (byte) 61,
      (byte) 105,
      (byte) 57,
      (byte) 97,
      (byte) 143,
      (byte) 219,
      (byte) 104
    };
    byte[] numArray6 = new byte[8]
    {
      (byte) 73,
      (byte) 188,
      (byte) 111,
      (byte) 65,
      (byte) 204,
      (byte) 8,
      (byte) 169,
      (byte) 50
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 8);
    for (int index = 0; index < 8; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4575()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 149,
        (byte) 99,
        (byte) 252,
        (byte) 218,
        byte.MaxValue,
        (byte) 204,
        (byte) 162
      };
      byte[] numArray3 = new byte[7]
      {
        (byte) 58,
        (byte) 244,
        (byte) 200,
        (byte) 220,
        (byte) 37,
        (byte) 182,
        (byte) 132
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7];
    numArray5[5] = (byte) 30;
    numArray5[2] = (byte) 90;
    numArray5[0] = (byte) 123;
    numArray5[3] = (byte) 30;
    numArray5[6] = (byte) 104;
    numArray5[4] = (byte) 210;
    numArray5[1] = (byte) 100;
    byte[] numArray6 = new byte[7]
    {
      (byte) 207,
      (byte) 193,
      (byte) 58,
      (byte) 83,
      (byte) 175,
      (byte) 201,
      (byte) 1
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[42];
    byte[] response = new byte[42];
    Array.Copy((Array) sc_4573.sspq, 0, (Array) numArray7, 0, 42);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4573.sspr, 0, (Array) numArray7, 0, 42);
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
