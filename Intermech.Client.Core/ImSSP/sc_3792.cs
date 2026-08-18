
// Type: ImSSP.sc_3792
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3792
{
  private static byte[] sspq = new byte[12]
  {
    (byte) 20,
    (byte) 145,
    (byte) 144 /*0x90*/,
    (byte) 75,
    (byte) 229,
    (byte) 87,
    (byte) 249,
    (byte) 227,
    (byte) 42,
    (byte) 95,
    (byte) 226,
    (byte) 228
  };
  private static byte[] sspr = new byte[12]
  {
    (byte) 238,
    (byte) 193,
    (byte) 151,
    (byte) 236,
    (byte) 110,
    (byte) 239,
    (byte) 59,
    (byte) 125,
    (byte) 23,
    (byte) 153,
    (byte) 127 /*0x7F*/,
    (byte) 120
  };

  internal static string ssp_imclient_3793()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12];
      numArray2[11] = (byte) 194;
      numArray2[0] = (byte) 81;
      numArray2[2] = (byte) 184;
      numArray2[8] = (byte) 206;
      numArray2[5] = (byte) 239;
      numArray2[1] = (byte) 208 /*0xD0*/;
      numArray2[6] = (byte) 86;
      numArray2[4] = (byte) 21;
      numArray2[3] = byte.MaxValue;
      numArray2[9] = (byte) 76;
      numArray2[10] = (byte) 224 /*0xE0*/;
      numArray2[7] = (byte) 153;
      byte[] numArray3 = new byte[12]
      {
        (byte) 9,
        (byte) 21,
        (byte) 50,
        (byte) 37,
        (byte) 254,
        (byte) 181,
        (byte) 133,
        (byte) 101,
        (byte) 63 /*0x3F*/,
        (byte) 58,
        (byte) 159,
        (byte) 229
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[12];
      byte[] response = new byte[12];
      Array.Copy((Array) sc_3792.sspq, 0, (Array) numArray4, 0, 12);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_3792.sspr, 0, (Array) numArray4, 0, 12);
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
    byte[] numArray5 = new byte[12];
    byte[] numArray6 = new byte[12]
    {
      (byte) 151,
      (byte) 176 /*0xB0*/,
      (byte) 35,
      (byte) 221,
      (byte) 162,
      (byte) 185,
      (byte) 123,
      (byte) 106,
      (byte) 130,
      (byte) 188,
      (byte) 59,
      (byte) 97
    };
    byte[] numArray7 = new byte[12]
    {
      (byte) 116,
      (byte) 218,
      (byte) 117,
      (byte) 242,
      (byte) 47,
      (byte) 233,
      (byte) 35,
      (byte) 14,
      (byte) 146,
      (byte) 118,
      (byte) 147,
      (byte) 244
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
