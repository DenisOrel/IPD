
// Type: ImSSP.sc_4592
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4592
{
  private static byte[] sspq = new byte[43]
  {
    (byte) 184,
    (byte) 232,
    (byte) 39,
    (byte) 241,
    (byte) 59,
    (byte) 38,
    (byte) 222,
    (byte) 188,
    (byte) 41,
    (byte) 243,
    (byte) 84,
    (byte) 63 /*0x3F*/,
    (byte) 88,
    (byte) 67,
    (byte) 154,
    (byte) 28,
    (byte) 182,
    (byte) 81,
    (byte) 165,
    (byte) 10,
    (byte) 195,
    (byte) 4,
    (byte) 143,
    (byte) 227,
    (byte) 64 /*0x40*/,
    (byte) 219,
    (byte) 254,
    (byte) 250,
    (byte) 243,
    (byte) 99,
    (byte) 167,
    (byte) 112 /*0x70*/,
    (byte) 184,
    (byte) 188,
    (byte) 115,
    (byte) 63 /*0x3F*/,
    (byte) 27,
    (byte) 10,
    (byte) 128 /*0x80*/,
    (byte) 68,
    (byte) 79,
    (byte) 185,
    (byte) 48 /*0x30*/
  };
  private static byte[] sspr = new byte[43]
  {
    (byte) 83,
    (byte) 228,
    (byte) 61,
    (byte) 3,
    (byte) 146,
    (byte) 107,
    (byte) 108,
    (byte) 105,
    (byte) 237,
    (byte) 86,
    (byte) 145,
    (byte) 206,
    (byte) 197,
    (byte) 65,
    (byte) 110,
    (byte) 236,
    (byte) 209,
    (byte) 180,
    (byte) 177,
    (byte) 180,
    (byte) 71,
    (byte) 254,
    (byte) 222,
    (byte) 130,
    (byte) 88,
    (byte) 47,
    (byte) 170,
    (byte) 28,
    (byte) 172,
    (byte) 47,
    (byte) 87,
    (byte) 65,
    (byte) 208 /*0xD0*/,
    (byte) 80 /*0x50*/,
    (byte) 155,
    (byte) 240 /*0xF0*/,
    (byte) 131,
    (byte) 182,
    (byte) 223,
    (byte) 67,
    (byte) 39,
    (byte) 97,
    (byte) 145
  };

  internal static string ssp_imclient_4593()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 3,
        (byte) 38,
        (byte) 208 /*0xD0*/,
        (byte) 59,
        (byte) 252,
        (byte) 79,
        (byte) 113,
        (byte) 230,
        (byte) 130,
        (byte) 137,
        (byte) 111,
        (byte) 193,
        (byte) 95,
        (byte) 178,
        (byte) 12
      };
      byte[] numArray3 = new byte[15];
      numArray3[0] = (byte) 97;
      numArray3[1] = (byte) 43;
      numArray3[11] = (byte) 126;
      numArray3[7] = (byte) 190;
      numArray3[2] = (byte) 85;
      numArray3[4] = (byte) 7;
      numArray3[14] = (byte) 139;
      numArray3[6] = (byte) 103;
      numArray3[8] = (byte) 61;
      numArray3[10] = (byte) 136;
      numArray3[13] = (byte) 127 /*0x7F*/;
      numArray3[3] = (byte) 53;
      numArray3[12] = (byte) 237;
      numArray3[9] = (byte) 233;
      numArray3[5] = (byte) 165;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 67,
      (byte) 252,
      (byte) 187,
      (byte) 106,
      (byte) 45,
      (byte) 118,
      (byte) 89,
      (byte) 29,
      (byte) 110,
      (byte) 208 /*0xD0*/,
      (byte) 14,
      (byte) 171,
      (byte) 98,
      (byte) 76,
      (byte) 194
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 79,
      (byte) 110,
      (byte) 148,
      (byte) 227,
      (byte) 123,
      (byte) 187,
      (byte) 2,
      (byte) 57,
      (byte) 145,
      (byte) 146,
      (byte) 142,
      (byte) 36,
      (byte) 38,
      (byte) 124,
      (byte) 181
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[43];
    byte[] response = new byte[43];
    Array.Copy((Array) sc_4592.sspq, 0, (Array) numArray7, 0, 43);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4592.sspr, 0, (Array) numArray7, 0, 43);
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
