
// Type: ImSSP.sc_4307
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4307
{
  internal static string ssp_imclient_4308()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[7];
      byte[] numArray2 = new byte[7]
      {
        (byte) 207,
        (byte) 145,
        (byte) 138,
        (byte) 248,
        (byte) 29,
        (byte) 97,
        (byte) 26
      };
      byte[] numArray3 = new byte[7];
      numArray3[2] = (byte) 116;
      numArray3[0] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 25;
      numArray3[1] = (byte) 58;
      numArray3[4] = (byte) 107;
      numArray3[5] = (byte) 186;
      numArray3[3] = (byte) 81;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 7);
      for (int index = 0; index < 7; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[7];
    byte[] numArray5 = new byte[7]
    {
      (byte) 131,
      (byte) 16 /*0x10*/,
      (byte) 10,
      (byte) 107,
      (byte) 45,
      (byte) 219,
      (byte) 234
    };
    byte[] numArray6 = new byte[7]
    {
      (byte) 172,
      (byte) 119,
      (byte) 36,
      (byte) 131,
      (byte) 51,
      (byte) 38,
      (byte) 132
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 7);
    for (int index = 0; index < 7; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4309()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11]
      {
        (byte) 241,
        (byte) 15,
        (byte) 175,
        (byte) 26,
        (byte) 240 /*0xF0*/,
        (byte) 41,
        (byte) 211,
        (byte) 164,
        (byte) 246,
        (byte) 85,
        (byte) 134
      };
      byte[] numArray3 = new byte[11]
      {
        (byte) 234,
        (byte) 43,
        (byte) 173,
        (byte) 241,
        (byte) 181,
        (byte) 20,
        (byte) 37,
        (byte) 139,
        (byte) 234,
        (byte) 50,
        (byte) 135
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11];
    numArray5[8] = (byte) 143;
    numArray5[2] = (byte) 37;
    numArray5[1] = (byte) 226;
    numArray5[3] = (byte) 51;
    numArray5[6] = (byte) 194;
    numArray5[0] = (byte) 33;
    numArray5[4] = (byte) 162;
    numArray5[7] = (byte) 204;
    numArray5[5] = (byte) 12;
    numArray5[9] = (byte) 244;
    numArray5[10] = (byte) 5;
    byte[] numArray6 = new byte[11];
    numArray6[10] = (byte) 209;
    numArray6[1] = (byte) 218;
    numArray6[8] = (byte) 41;
    numArray6[3] = (byte) 2;
    numArray6[9] = (byte) 237;
    numArray6[4] = (byte) 142;
    numArray6[6] = (byte) 156;
    numArray6[7] = (byte) 89;
    numArray6[5] = (byte) 73;
    numArray6[2] = (byte) 221;
    numArray6[0] = (byte) 145;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
