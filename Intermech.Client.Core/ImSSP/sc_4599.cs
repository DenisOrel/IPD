
// Type: ImSSP.sc_4599
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4599
{
  private static byte[] sspq = new byte[40]
  {
    (byte) 92,
    (byte) 47,
    (byte) 148,
    (byte) 237,
    (byte) 114,
    (byte) 119,
    (byte) 192 /*0xC0*/,
    (byte) 17,
    (byte) 213,
    (byte) 41,
    (byte) 51,
    (byte) 128 /*0x80*/,
    (byte) 205,
    (byte) 248,
    (byte) 201,
    (byte) 185,
    (byte) 151,
    (byte) 46,
    (byte) 107,
    (byte) 212,
    (byte) 77,
    (byte) 229,
    (byte) 126,
    (byte) 118,
    (byte) 203,
    (byte) 141,
    (byte) 231,
    (byte) 101,
    (byte) 7,
    (byte) 4,
    (byte) 215,
    (byte) 101,
    (byte) 135,
    (byte) 117,
    (byte) 252,
    (byte) 182,
    (byte) 246,
    (byte) 203,
    (byte) 119,
    (byte) 132
  };
  private static byte[] sspr = new byte[40]
  {
    (byte) 125,
    (byte) 205,
    (byte) 115,
    (byte) 246,
    (byte) 203,
    (byte) 137,
    (byte) 6,
    (byte) 204,
    (byte) 200,
    (byte) 93,
    (byte) 106,
    (byte) 31 /*0x1F*/,
    (byte) 120,
    (byte) 185,
    (byte) 122,
    (byte) 209,
    (byte) 147,
    (byte) 41,
    (byte) 150,
    (byte) 236,
    (byte) 210,
    (byte) 81,
    (byte) 76,
    (byte) 84,
    (byte) 112 /*0x70*/,
    (byte) 209,
    (byte) 253,
    (byte) 33,
    (byte) 101,
    (byte) 154,
    (byte) 83,
    (byte) 239,
    (byte) 32 /*0x20*/,
    byte.MaxValue,
    (byte) 38,
    (byte) 236,
    (byte) 119,
    (byte) 79,
    (byte) 7,
    (byte) 166
  };

  internal static string ssp_imclient_4600()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[1];
      byte[] numArray2 = new byte[1]{ (byte) 5 };
      byte[] numArray3 = new byte[1]{ (byte) 23 };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 1);
      for (int index = 0; index < 1; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[40];
      byte[] response = new byte[40];
      Array.Copy((Array) sc_4599.sspq, 0, (Array) numArray4, 0, 40);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_4599.sspr, 0, (Array) numArray4, 0, 40);
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
    byte[] numArray5 = new byte[1];
    byte[] numArray6 = new byte[1]{ (byte) 242 };
    byte[] numArray7 = new byte[1]{ (byte) 216 };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 1);
    for (int index = 0; index < 1; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
