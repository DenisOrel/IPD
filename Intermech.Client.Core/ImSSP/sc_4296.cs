
// Type: ImSSP.sc_4296
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4296
{
  internal static string ssp_imclient_4297()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 104,
        (byte) 210,
        (byte) 25,
        (byte) 37,
        (byte) 149,
        (byte) 87,
        (byte) 229,
        (byte) 201,
        (byte) 64 /*0x40*/,
        (byte) 216,
        (byte) 7,
        (byte) 228,
        (byte) 117,
        (byte) 13,
        (byte) 39
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 147,
        (byte) 6,
        (byte) 135,
        (byte) 175,
        (byte) 154,
        (byte) 10,
        (byte) 171,
        (byte) 118,
        (byte) 2,
        (byte) 133,
        (byte) 206,
        (byte) 86,
        (byte) 12,
        (byte) 28,
        (byte) 161
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[5] = (byte) 183;
    numArray5[13] = (byte) 163;
    numArray5[2] = (byte) 169;
    numArray5[8] = (byte) 95;
    numArray5[12] = (byte) 87;
    numArray5[1] = (byte) 124;
    numArray5[6] = (byte) 10;
    numArray5[7] = (byte) 179;
    numArray5[10] = (byte) 47;
    numArray5[9] = (byte) 39;
    numArray5[0] = (byte) 70;
    numArray5[3] = (byte) 112 /*0x70*/;
    numArray5[11] = (byte) 101;
    numArray5[4] = (byte) 181;
    numArray5[14] = (byte) 53;
    byte[] numArray6 = new byte[15]
    {
      (byte) 82,
      (byte) 3,
      (byte) 160 /*0xA0*/,
      (byte) 242,
      (byte) 149,
      (byte) 251,
      (byte) 187,
      (byte) 66,
      (byte) 32 /*0x20*/,
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 234,
      (byte) 28,
      (byte) 48 /*0x30*/,
      (byte) 164
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
