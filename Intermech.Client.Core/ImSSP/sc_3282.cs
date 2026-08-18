
// Type: ImSSP.sc_3282
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3282
{
  internal static string ssp_imclient_3283()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[6] = (byte) 145;
      numArray2[10] = (byte) 213;
      numArray2[2] = (byte) 197;
      numArray2[12] = (byte) 77;
      numArray2[4] = (byte) 24;
      numArray2[13] = (byte) 249;
      numArray2[5] = (byte) 253;
      numArray2[0] = (byte) 154;
      numArray2[8] = (byte) 182;
      numArray2[15] = (byte) 213;
      numArray2[3] = (byte) 50;
      numArray2[11] = (byte) 43;
      numArray2[9] = (byte) 25;
      numArray2[7] = (byte) 63 /*0x3F*/;
      numArray2[14] = (byte) 188;
      numArray2[1] = (byte) 197;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 55,
        (byte) 144 /*0x90*/,
        (byte) 240 /*0xF0*/,
        (byte) 180,
        (byte) 108,
        (byte) 128 /*0x80*/,
        (byte) 157,
        (byte) 54,
        (byte) 27,
        (byte) 136,
        (byte) 0,
        (byte) 93,
        (byte) 207,
        (byte) 31 /*0x1F*/,
        (byte) 31 /*0x1F*/,
        (byte) 61
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 39,
      (byte) 190,
      (byte) 67,
      (byte) 31 /*0x1F*/,
      (byte) 1,
      (byte) 134,
      (byte) 246,
      (byte) 74,
      (byte) 164,
      (byte) 196,
      (byte) 116,
      (byte) 225,
      (byte) 21,
      (byte) 60,
      (byte) 99,
      (byte) 104
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 249,
      (byte) 52,
      (byte) 214,
      (byte) 111,
      (byte) 200,
      (byte) 217,
      (byte) 237,
      (byte) 166,
      (byte) 49,
      (byte) 88,
      (byte) 23,
      (byte) 76,
      (byte) 57,
      (byte) 213,
      (byte) 191,
      (byte) 85
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
