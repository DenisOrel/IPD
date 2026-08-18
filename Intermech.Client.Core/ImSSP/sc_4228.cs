
// Type: ImSSP.sc_4228
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4228
{
  internal static string ssp_imclient_4229()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 0,
        (byte) 0,
        (byte) 132,
        (byte) 0,
        (byte) 0
      };
      numArray2[1] = (byte) 249;
      numArray2[4] = (byte) 147;
      numArray2[0] = (byte) 138;
      numArray2[3] = (byte) 0;
      byte[] numArray3 = new byte[5]
      {
        (byte) 30,
        (byte) 154,
        (byte) 110,
        (byte) 29,
        (byte) 70
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
      (byte) 0,
      (byte) 120,
      (byte) 0,
      (byte) 242,
      (byte) 0
    };
    numArray5[2] = (byte) 102;
    numArray5[4] = (byte) 215;
    numArray5[0] = (byte) 147;
    byte[] numArray6 = new byte[5]
    {
      (byte) 80 /*0x50*/,
      (byte) 207,
      (byte) 82,
      (byte) 100,
      (byte) 172
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4230()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 140,
        (byte) 207,
        (byte) 229,
        (byte) 197,
        (byte) 187
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 168,
        (byte) 92,
        (byte) 53,
        (byte) 217,
        (byte) 23
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
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 0,
      (byte) 17
    };
    numArray5[1] = (byte) 50;
    numArray5[0] = (byte) 68;
    numArray5[3] = (byte) 233;
    numArray5[2] = (byte) 7;
    byte[] numArray6 = new byte[5]
    {
      (byte) 220,
      (byte) 141,
      (byte) 148,
      (byte) 155,
      (byte) 68
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4231()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        byte.MaxValue,
        (byte) 169,
        (byte) 221,
        (byte) 31 /*0x1F*/,
        (byte) 153
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 0,
        (byte) 69,
        (byte) 0,
        (byte) 0,
        (byte) 44
      };
      numArray3[0] = (byte) 157;
      numArray3[3] = (byte) 146;
      numArray3[2] = (byte) 213;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[5];
    byte[] numArray5 = new byte[5]
    {
      (byte) 35,
      (byte) 53,
      (byte) 32 /*0x20*/,
      (byte) 13,
      (byte) 250
    };
    byte[] numArray6 = new byte[5]
    {
      (byte) 236,
      (byte) 74,
      (byte) 33,
      (byte) 102,
      (byte) 22
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4232()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 234,
        (byte) 237,
        (byte) 228,
        (byte) 22,
        (byte) 9
      };
      byte[] numArray3 = new byte[5]
      {
        (byte) 239,
        (byte) 107,
        (byte) 111,
        (byte) 69,
        (byte) 5
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
      (byte) 81,
      (byte) 104,
      (byte) 75,
      (byte) 6,
      (byte) 208 /*0xD0*/
    };
    byte[] numArray6 = new byte[5]
    {
      (byte) 50,
      (byte) 95,
      (byte) 125,
      (byte) 40,
      (byte) 129
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
