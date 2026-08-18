
// Type: ImSSP.sc_4310
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4310
{
  private static byte[] sspq = new byte[28]
  {
    (byte) 2,
    (byte) 75,
    (byte) 105,
    (byte) 6,
    (byte) 211,
    (byte) 36,
    (byte) 32 /*0x20*/,
    (byte) 228,
    (byte) 164,
    (byte) 191,
    (byte) 32 /*0x20*/,
    (byte) 87,
    (byte) 90,
    (byte) 196,
    (byte) 177,
    (byte) 20,
    (byte) 230,
    (byte) 156,
    (byte) 48 /*0x30*/,
    (byte) 125,
    (byte) 86,
    (byte) 92,
    (byte) 118,
    (byte) 229,
    (byte) 110,
    (byte) 207,
    (byte) 128 /*0x80*/,
    (byte) 59
  };
  private static byte[] sspr = new byte[28]
  {
    (byte) 117,
    (byte) 180,
    (byte) 227,
    (byte) 219,
    (byte) 125,
    (byte) 52,
    (byte) 78,
    (byte) 68,
    (byte) 51,
    (byte) 36,
    (byte) 68,
    (byte) 50,
    (byte) 127 /*0x7F*/,
    (byte) 118,
    (byte) 6,
    (byte) 117,
    (byte) 70,
    (byte) 15,
    (byte) 58,
    (byte) 246,
    (byte) 196,
    (byte) 52,
    (byte) 45,
    (byte) 52,
    (byte) 222,
    (byte) 25,
    (byte) 111,
    (byte) 250
  };

  internal static string ssp_imclient_4311()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[6];
      byte[] numArray2 = new byte[6];
      numArray2[5] = (byte) 127 /*0x7F*/;
      numArray2[4] = (byte) 37;
      numArray2[2] = (byte) 59;
      numArray2[3] = (byte) 214;
      numArray2[1] = (byte) 238;
      numArray2[0] = (byte) 46;
      byte[] numArray3 = new byte[6];
      numArray3[2] = (byte) 228;
      numArray3[1] = (byte) 164;
      numArray3[5] = (byte) 240 /*0xF0*/;
      numArray3[3] = (byte) 151;
      numArray3[4] = (byte) 25;
      numArray3[0] = (byte) 157;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 6);
      for (int index = 0; index < 6; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[6];
    byte[] numArray5 = new byte[6]
    {
      (byte) 74,
      (byte) 74,
      (byte) 123,
      (byte) 182,
      (byte) 205,
      (byte) 36
    };
    byte[] numArray6 = new byte[6]
    {
      (byte) 3,
      (byte) 180,
      (byte) 58,
      (byte) 147,
      (byte) 94,
      (byte) 152
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 6);
    for (int index = 0; index < 6; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4312()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 149,
        (byte) 189,
        (byte) 217,
        (byte) 103,
        (byte) 157
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 49,
        (byte) 109,
        (byte) 190,
        (byte) 125,
        (byte) 96 /*0x60*/
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[5];
    byte[] numArray5 = new byte[5]
    {
      (byte) 105,
      (byte) 83,
      (byte) 106,
      (byte) 100,
      (byte) 239
    };
    byte[] numArray6 = new byte[5]
    {
      (byte) 104,
      (byte) 137,
      (byte) 217,
      (byte) 100,
      (byte) 44
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[28];
    byte[] response = new byte[28];
    Array.Copy((Array) sc_4310.sspq, 0, (Array) numArray7, 0, 28);
    key.Query(true, 348, numArray7, response);
    Array.Copy((Array) sc_4310.sspr, 0, (Array) numArray7, 0, 28);
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
