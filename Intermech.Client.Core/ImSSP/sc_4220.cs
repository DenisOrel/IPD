
// Type: ImSSP.sc_4220
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4220
{
  internal static string ssp_imclient_4221()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[7] = (byte) 246;
      numArray2[1] = (byte) 202;
      numArray2[2] = (byte) 208 /*0xD0*/;
      numArray2[8] = (byte) 76;
      numArray2[14] = (byte) 146;
      numArray2[9] = (byte) 71;
      numArray2[13] = (byte) 74;
      numArray2[4] = (byte) 210;
      numArray2[5] = (byte) 147;
      numArray2[6] = (byte) 67;
      numArray2[10] = (byte) 110;
      numArray2[11] = (byte) 194;
      numArray2[0] = (byte) 194;
      numArray2[3] = (byte) 131;
      numArray2[12] = (byte) 35;
      byte[] numArray3 = new byte[15]
      {
        (byte) 165,
        (byte) 125,
        (byte) 165,
        (byte) 131,
        (byte) 24,
        (byte) 140,
        (byte) 115,
        (byte) 1,
        (byte) 229,
        (byte) 249,
        (byte) 33,
        (byte) 203,
        (byte) 221,
        (byte) 101,
        (byte) 83
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[9] = (byte) 237;
    numArray5[13] = (byte) 10;
    numArray5[2] = (byte) 221;
    numArray5[8] = (byte) 246;
    numArray5[3] = (byte) 103;
    numArray5[5] = (byte) 184;
    numArray5[11] = (byte) 106;
    numArray5[0] = (byte) 227;
    numArray5[4] = (byte) 96 /*0x60*/;
    numArray5[6] = (byte) 212;
    numArray5[10] = (byte) 109;
    numArray5[14] = (byte) 219;
    numArray5[12] = (byte) 40;
    numArray5[7] = (byte) 132;
    numArray5[1] = (byte) 236;
    byte[] numArray6 = new byte[15];
    numArray6[14] = (byte) 181;
    numArray6[7] = (byte) 17;
    numArray6[2] = (byte) 44;
    numArray6[9] = (byte) 238;
    numArray6[10] = (byte) 29;
    numArray6[5] = (byte) 184;
    numArray6[6] = (byte) 201;
    numArray6[1] = (byte) 186;
    numArray6[4] = (byte) 104;
    numArray6[3] = (byte) 150;
    numArray6[12] = (byte) 106;
    numArray6[11] = (byte) 143;
    numArray6[8] = (byte) 8;
    numArray6[13] = (byte) 110;
    numArray6[0] = (byte) 121;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4222()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15];
      numArray2[10] = (byte) 168;
      numArray2[1] = (byte) 38;
      numArray2[14] = (byte) 83;
      numArray2[3] = (byte) 183;
      numArray2[4] = (byte) 41;
      numArray2[0] = (byte) 130;
      numArray2[12] = (byte) 29;
      numArray2[7] = (byte) 153;
      numArray2[8] = (byte) 183;
      numArray2[5] = (byte) 155;
      numArray2[2] = (byte) 9;
      numArray2[9] = (byte) 23;
      numArray2[6] = (byte) 96 /*0x60*/;
      numArray2[13] = (byte) 250;
      numArray2[11] = (byte) 182;
      byte[] numArray3 = new byte[15]
      {
        (byte) 204,
        (byte) 153,
        (byte) 254,
        (byte) 253,
        (byte) 238,
        (byte) 172,
        (byte) 223,
        (byte) 202,
        (byte) 173,
        (byte) 173,
        (byte) 234,
        (byte) 129,
        (byte) 40,
        (byte) 57,
        (byte) 235
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15]
    {
      (byte) 182,
      (byte) 8,
      (byte) 124,
      (byte) 118,
      (byte) 153,
      (byte) 99,
      (byte) 117,
      (byte) 223,
      (byte) 197,
      (byte) 253,
      (byte) 132,
      (byte) 117,
      (byte) 254,
      (byte) 101,
      (byte) 155
    };
    byte[] numArray6 = new byte[15]
    {
      (byte) 141,
      (byte) 104,
      (byte) 167,
      (byte) 95,
      (byte) 148,
      (byte) 155,
      (byte) 50,
      (byte) 25,
      (byte) 120,
      (byte) 6,
      (byte) 81,
      (byte) 180,
      (byte) 97,
      (byte) 4,
      (byte) 71
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
