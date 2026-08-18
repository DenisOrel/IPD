
// Type: ImSSP.sc_4248
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4248
{
  internal static string ssp_imclient_4249()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[4];
      byte[] numArray2 = new byte[4]
      {
        (byte) 246,
        (byte) 190,
        (byte) 101,
        (byte) 81
      };
      byte[] numArray3 = new byte[4]
      {
        (byte) 0,
        (byte) 0,
        (byte) 29,
        (byte) 0
      };
      numArray3[1] = (byte) 250;
      numArray3[0] = (byte) 179;
      numArray3[3] = (byte) 60;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 4);
      for (int index = 0; index < 4; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[4];
    byte[] numArray5 = new byte[4]
    {
      (byte) 178,
      (byte) 171,
      (byte) 105,
      (byte) 82
    };
    byte[] numArray6 = new byte[4]
    {
      (byte) 24,
      (byte) 0,
      (byte) 0,
      (byte) 214
    };
    numArray6[2] = (byte) 82;
    numArray6[1] = (byte) 205;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 4);
    for (int index = 0; index < 4; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
