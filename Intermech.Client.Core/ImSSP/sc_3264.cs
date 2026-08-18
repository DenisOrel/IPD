
// Type: ImSSP.sc_3264
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_3264
{
  internal static string ssp_imclient_3265()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[2] = (byte) 143;
      numArray2[1] = (byte) 44;
      numArray2[9] = (byte) 102;
      numArray2[3] = (byte) 199;
      numArray2[4] = (byte) 104;
      numArray2[8] = (byte) 120;
      numArray2[6] = (byte) 147;
      numArray2[7] = (byte) 193;
      numArray2[5] = (byte) 162;
      numArray2[11] = (byte) 137;
      numArray2[10] = (byte) 107;
      numArray2[12] = (byte) 100;
      numArray2[14] = (byte) 108;
      numArray2[13] = (byte) 16 /*0x10*/;
      numArray2[0] = (byte) 197;
      byte[] numArray3 = new byte[15]
      {
        (byte) 69,
        (byte) 87,
        (byte) 207,
        (byte) 126,
        (byte) 88,
        (byte) 51,
        (byte) 190,
        (byte) 115,
        (byte) 227,
        (byte) 115,
        (byte) 23,
        (byte) 11,
        (byte) 183,
        (byte) 129,
        (byte) 188
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[4] = (byte) 231;
    numArray5[2] = (byte) 167;
    numArray5[7] = (byte) 237;
    numArray5[3] = (byte) 216;
    numArray5[11] = (byte) 195;
    numArray5[5] = (byte) 71;
    numArray5[6] = (byte) 143;
    numArray5[8] = (byte) 28;
    numArray5[14] = (byte) 236;
    numArray5[10] = (byte) 189;
    numArray5[0] = (byte) 202;
    numArray5[1] = (byte) 75;
    numArray5[12] = (byte) 75;
    numArray5[9] = (byte) 238;
    numArray5[13] = (byte) 95;
    byte[] numArray6 = new byte[15]
    {
      (byte) 96 /*0x60*/,
      (byte) 238,
      (byte) 193,
      (byte) 144 /*0x90*/,
      (byte) 128 /*0x80*/,
      (byte) 107,
      (byte) 197,
      (byte) 92,
      (byte) 205,
      (byte) 187,
      (byte) 219,
      (byte) 43,
      (byte) 31 /*0x1F*/,
      (byte) 129,
      (byte) 30
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
