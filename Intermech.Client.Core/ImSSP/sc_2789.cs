
// Type: ImSSP.sc_2789
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2789
{
  private static byte[] sspq = new byte[20]
  {
    (byte) 133,
    (byte) 12,
    (byte) 109,
    (byte) 166,
    (byte) 120,
    (byte) 177,
    (byte) 8,
    (byte) 237,
    (byte) 226,
    (byte) 197,
    (byte) 33,
    (byte) 220,
    (byte) 21,
    (byte) 10,
    (byte) 148,
    (byte) 157,
    (byte) 184,
    (byte) 134,
    (byte) 13,
    (byte) 199
  };
  private static byte[] sspr = new byte[20]
  {
    (byte) 200,
    (byte) 242,
    (byte) 131,
    (byte) 18,
    (byte) 78,
    (byte) 96 /*0x60*/,
    (byte) 204,
    (byte) 12,
    (byte) 100,
    (byte) 54,
    (byte) 117,
    (byte) 170,
    (byte) 151,
    (byte) 114,
    (byte) 43,
    (byte) 190,
    byte.MaxValue,
    (byte) 176 /*0xB0*/,
    (byte) 207,
    (byte) 204
  };

  internal static string ssp_imclient_2790()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6]
      {
        (byte) 116,
        (byte) 159,
        (byte) 241,
        (byte) 54,
        (byte) 224 /*0xE0*/,
        (byte) 105
      };
      byte[] numArray3 = new byte[6]
      {
        (byte) 0,
        (byte) 247,
        (byte) 0,
        (byte) 0,
        (byte) 211,
        (byte) 0
      };
      numArray3[3] = (byte) 1;
      numArray3[2] = (byte) 42;
      numArray3[5] = (byte) 38;
      numArray3[0] = (byte) 167;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6]
    {
      (byte) 136,
      (byte) 11,
      (byte) 236,
      (byte) 126,
      (byte) 240 /*0xF0*/,
      (byte) 96 /*0x60*/
    };
    byte[] numArray6 = new byte[6]
    {
      (byte) 23,
      (byte) 212,
      (byte) 85,
      (byte) 116,
      (byte) 146,
      (byte) 64 /*0x40*/
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[20];
    byte[] response = new byte[20];
    Array.Copy((Array) sc_2789.sspq, 0, (Array) numArray7, 0, 20);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_2789.sspr, 0, (Array) numArray7, 0, 20);
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
