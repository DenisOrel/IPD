
// Type: ImSSP.sc_4368
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4368
{
  internal static string ssp_imclient_4369()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 91,
        (byte) 29,
        (byte) 153,
        (byte) 37,
        (byte) 120,
        (byte) 244,
        (byte) 24,
        (byte) 7,
        (byte) 123,
        (byte) 193,
        (byte) 152,
        (byte) 185,
        (byte) 131,
        (byte) 249,
        (byte) 30
      };
      byte[] numArray3 = new byte[15];
      numArray3[1] = (byte) 64 /*0x40*/;
      numArray3[10] = (byte) 16 /*0x10*/;
      numArray3[2] = (byte) 81;
      numArray3[4] = (byte) 196;
      numArray3[6] = (byte) 58;
      numArray3[5] = (byte) 205;
      numArray3[11] = (byte) 44;
      numArray3[3] = (byte) 32 /*0x20*/;
      numArray3[0] = (byte) 64 /*0x40*/;
      numArray3[9] = (byte) 103;
      numArray3[7] = (byte) 29;
      numArray3[8] = (byte) 58;
      numArray3[14] = (byte) 40;
      numArray3[13] = (byte) 169;
      numArray3[12] = (byte) 200;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[4] = (byte) 49;
    numArray5[6] = (byte) 207;
    numArray5[7] = (byte) 152;
    numArray5[2] = (byte) 162;
    numArray5[14] = (byte) 99;
    numArray5[5] = (byte) 226;
    numArray5[3] = (byte) 134;
    numArray5[1] = (byte) 72;
    numArray5[8] = (byte) 12;
    numArray5[9] = (byte) 107;
    numArray5[10] = (byte) 219;
    numArray5[0] = (byte) 254;
    numArray5[12] = (byte) 195;
    numArray5[13] = (byte) 211;
    numArray5[11] = (byte) 151;
    byte[] numArray6 = new byte[15];
    numArray6[11] = (byte) 24;
    numArray6[14] = (byte) 9;
    numArray6[2] = (byte) 218;
    numArray6[10] = (byte) 162;
    numArray6[4] = (byte) 153;
    numArray6[5] = (byte) 93;
    numArray6[6] = (byte) 209;
    numArray6[1] = (byte) 125;
    numArray6[7] = (byte) 214;
    numArray6[8] = (byte) 218;
    numArray6[9] = (byte) 112 /*0x70*/;
    numArray6[0] = (byte) 114;
    numArray6[12] = (byte) 192 /*0xC0*/;
    numArray6[13] = (byte) 23;
    numArray6[3] = (byte) 93;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
