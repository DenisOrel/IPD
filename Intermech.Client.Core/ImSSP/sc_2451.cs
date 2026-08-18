
// Type: ImSSP.sc_2451
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_2451
{
  internal static string ssp_imclient_2452()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 150,
        (byte) 13,
        (byte) 40,
        (byte) 238,
        (byte) 141,
        (byte) 145,
        (byte) 26,
        (byte) 237,
        (byte) 99,
        (byte) 169,
        (byte) 194,
        (byte) 154,
        (byte) 10,
        (byte) 195,
        (byte) 159
      };
      byte[] numArray3 = new byte[15];
      numArray3[8] = (byte) 21;
      numArray3[1] = (byte) 94;
      numArray3[5] = (byte) 228;
      numArray3[3] = (byte) 70;
      numArray3[4] = (byte) 132;
      numArray3[10] = (byte) 82;
      numArray3[0] = (byte) 114;
      numArray3[7] = (byte) 206;
      numArray3[14] = (byte) 180;
      numArray3[9] = (byte) 202;
      numArray3[2] = (byte) 249;
      numArray3[11] = (byte) 158;
      numArray3[12] = (byte) 244;
      numArray3[13] = (byte) 45;
      numArray3[6] = (byte) 108;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[0] = (byte) 203;
    numArray5[10] = (byte) 185;
    numArray5[12] = (byte) 154;
    numArray5[3] = (byte) 183;
    numArray5[4] = (byte) 96 /*0x60*/;
    numArray5[5] = (byte) 23;
    numArray5[6] = (byte) 236;
    numArray5[8] = (byte) 62;
    numArray5[13] = (byte) 146;
    numArray5[1] = (byte) 133;
    numArray5[9] = (byte) 23;
    numArray5[11] = (byte) 126;
    numArray5[2] = (byte) 27;
    numArray5[7] = (byte) 131;
    numArray5[14] = (byte) 145;
    byte[] numArray6 = new byte[15];
    numArray6[9] = (byte) 17;
    numArray6[12] = (byte) 197;
    numArray6[0] = (byte) 212;
    numArray6[3] = (byte) 114;
    numArray6[4] = (byte) 31 /*0x1F*/;
    numArray6[5] = (byte) 187;
    numArray6[6] = (byte) 102;
    numArray6[14] = (byte) 81;
    numArray6[7] = (byte) 62;
    numArray6[10] = (byte) 38;
    numArray6[8] = (byte) 190;
    numArray6[13] = (byte) 74;
    numArray6[11] = (byte) 105;
    numArray6[1] = (byte) 49;
    numArray6[2] = (byte) 124;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
