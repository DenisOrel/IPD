
// Type: ImSSP.sc_5187
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_5187
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 111,
    (byte) 91,
    (byte) 45,
    (byte) 58,
    (byte) 32 /*0x20*/,
    (byte) 242,
    (byte) 182,
    (byte) 161,
    (byte) 34,
    (byte) 206,
    (byte) 84,
    (byte) 197,
    (byte) 157,
    (byte) 251,
    (byte) 118,
    (byte) 98,
    (byte) 59,
    (byte) 51,
    (byte) 240 /*0xF0*/,
    (byte) 55,
    (byte) 72,
    (byte) 103,
    (byte) 194,
    (byte) 164,
    (byte) 42,
    (byte) 193,
    (byte) 43,
    (byte) 119,
    (byte) 190,
    (byte) 161,
    (byte) 100,
    (byte) 162,
    (byte) 124,
    (byte) 0,
    (byte) 207
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 152,
    (byte) 185,
    (byte) 63 /*0x3F*/,
    (byte) 130,
    (byte) 97,
    (byte) 185,
    (byte) 108,
    (byte) 209,
    (byte) 89,
    (byte) 209,
    (byte) 75,
    (byte) 45,
    (byte) 183,
    (byte) 145,
    (byte) 216,
    (byte) 52,
    (byte) 191,
    (byte) 202,
    (byte) 166,
    (byte) 21,
    (byte) 184,
    (byte) 138,
    (byte) 161,
    (byte) 96 /*0x60*/,
    (byte) 3,
    (byte) 176 /*0xB0*/,
    (byte) 108,
    (byte) 15,
    (byte) 3,
    (byte) 50,
    (byte) 223,
    (byte) 253,
    (byte) 108,
    (byte) 58,
    (byte) 34
  };

  internal static string ssp_imclient_5188()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[4] = (byte) 117;
      numArray2[2] = (byte) 214;
      numArray2[9] = (byte) 89;
      numArray2[0] = (byte) 46;
      numArray2[5] = (byte) 209;
      numArray2[3] = (byte) 222;
      numArray2[1] = (byte) 33;
      numArray2[7] = (byte) 48 /*0x30*/;
      numArray2[8] = (byte) 34;
      numArray2[10] = (byte) 90;
      numArray2[6] = (byte) 68;
      numArray2[11] = (byte) 93;
      numArray2[12] = (byte) 43;
      numArray2[13] = (byte) 63 /*0x3F*/;
      numArray2[14] = (byte) 254;
      byte[] numArray3 = new byte[15]
      {
        (byte) 38,
        (byte) 90,
        (byte) 219,
        (byte) 203,
        (byte) 70,
        (byte) 112 /*0x70*/,
        (byte) 145,
        (byte) 179,
        (byte) 59,
        (byte) 57,
        (byte) 51,
        (byte) 238,
        (byte) 107,
        (byte) 173,
        (byte) 87
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[35];
      byte[] response = new byte[35];
      Array.Copy((Array) sc_5187.sspq, 0, (Array) numArray4, 0, 35);
      key.Query(true, 348, numArray4, response);
      Array.Copy((Array) sc_5187.sspr, 0, (Array) numArray4, 0, 35);
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
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15]
    {
      (byte) 163,
      (byte) 55,
      (byte) 240 /*0xF0*/,
      (byte) 42,
      (byte) 194,
      (byte) 152,
      (byte) 27,
      (byte) 163,
      (byte) 145,
      (byte) 221,
      (byte) 238,
      (byte) 22,
      (byte) 171,
      (byte) 87,
      (byte) 15
    };
    byte[] numArray7 = new byte[15]
    {
      (byte) 46,
      (byte) 249,
      (byte) 213,
      (byte) 202,
      (byte) 221,
      (byte) 220,
      (byte) 192 /*0xC0*/,
      (byte) 120,
      (byte) 201,
      (byte) 21,
      (byte) 94,
      (byte) 250,
      (byte) 62,
      (byte) 68,
      (byte) 22
    };
    key.Query(true, 348, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
