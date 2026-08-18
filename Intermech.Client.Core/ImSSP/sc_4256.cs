
// Type: ImSSP.sc_4256
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_4256
{
  internal static string ssp_imclient_4257()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 190,
        (byte) 162,
        (byte) 70,
        (byte) 192 /*0xC0*/,
        (byte) 52,
        (byte) 15,
        (byte) 220,
        (byte) 37,
        (byte) 100,
        (byte) 199,
        (byte) 194,
        (byte) 178,
        (byte) 15,
        (byte) 179,
        (byte) 244
      };
      byte[] numArray3 = new byte[15]
      {
        (byte) 59,
        (byte) 205,
        (byte) 121,
        (byte) 228,
        (byte) 64 /*0x40*/,
        (byte) 250,
        (byte) 57,
        (byte) 36,
        (byte) 101,
        (byte) 211,
        (byte) 90,
        (byte) 25,
        (byte) 105,
        (byte) 90,
        (byte) 99
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[15];
    byte[] numArray5 = new byte[15];
    numArray5[14] = (byte) 100;
    numArray5[1] = (byte) 181;
    numArray5[8] = (byte) 75;
    numArray5[3] = (byte) 119;
    numArray5[0] = (byte) 149;
    numArray5[5] = (byte) 191;
    numArray5[11] = (byte) 239;
    numArray5[6] = (byte) 180;
    numArray5[2] = (byte) 38;
    numArray5[9] = (byte) 192 /*0xC0*/;
    numArray5[10] = (byte) 50;
    numArray5[7] = (byte) 10;
    numArray5[12] = (byte) 189;
    numArray5[13] = (byte) 236;
    numArray5[4] = (byte) 9;
    byte[] numArray6 = new byte[15]
    {
      (byte) 139,
      (byte) 217,
      (byte) 167,
      (byte) 158,
      (byte) 166,
      (byte) 176 /*0xB0*/,
      (byte) 110,
      (byte) 251,
      (byte) 66,
      (byte) 8,
      (byte) 155,
      (byte) 40,
      (byte) 130,
      (byte) 175,
      (byte) 157
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }

  internal static string ssp_imclient_4258()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 8)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[1] = (byte) 221;
      numArray2[4] = (byte) 152;
      numArray2[7] = (byte) 42;
      numArray2[9] = (byte) 27;
      numArray2[0] = (byte) 52;
      numArray2[5] = (byte) 135;
      numArray2[6] = (byte) 11;
      numArray2[2] = (byte) 132;
      numArray2[8] = (byte) 195;
      numArray2[3] = (byte) 70;
      byte[] numArray3 = new byte[10]
      {
        (byte) 182,
        (byte) 71,
        (byte) 55,
        (byte) 146,
        (byte) 184,
        (byte) 40,
        (byte) 145,
        (byte) 98,
        (byte) 15,
        (byte) 106
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[8] = (byte) 60;
    numArray5[1] = (byte) 204;
    numArray5[2] = (byte) 30;
    numArray5[3] = (byte) 213;
    numArray5[4] = (byte) 253;
    numArray5[5] = (byte) 200;
    numArray5[0] = (byte) 145;
    numArray5[6] = (byte) 228;
    numArray5[9] = (byte) 33;
    numArray5[7] = (byte) 117;
    byte[] numArray6 = new byte[10];
    numArray6[4] = (byte) 45;
    numArray6[0] = (byte) 146;
    numArray6[2] = (byte) 67;
    numArray6[8] = (byte) 16 /*0x10*/;
    numArray6[3] = (byte) 145;
    numArray6[5] = (byte) 164;
    numArray6[9] = (byte) 124;
    numArray6[7] = (byte) 23;
    numArray6[6] = (byte) 6;
    numArray6[1] = (byte) 201;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
