// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10728
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_10728
{
  internal static string ssp_imclient_10729()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 228,
        (byte) 149,
        (byte) 248,
        (byte) 181,
        (byte) 91,
        (byte) 90,
        (byte) 60,
        (byte) 129,
        (byte) 108,
        (byte) 222,
        (byte) 121,
        (byte) 158,
        (byte) 8,
        (byte) 75,
        (byte) 165,
        (byte) 239,
        (byte) 237,
        (byte) 214,
        (byte) 212,
        (byte) 106
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 213,
        (byte) 62,
        (byte) 159,
        (byte) 217,
        (byte) 58,
        (byte) 189,
        (byte) 82,
        (byte) 142,
        (byte) 94,
        (byte) 105,
        (byte) 100,
        (byte) 58,
        (byte) 59,
        (byte) 65,
        (byte) 74,
        (byte) 54,
        (byte) 153,
        (byte) 241,
        (byte) 30,
        (byte) 77
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20]
    {
      (byte) 140,
      (byte) 226,
      (byte) 189,
      (byte) 8,
      (byte) 187,
      (byte) 65,
      (byte) 65,
      (byte) 207,
      (byte) 116,
      (byte) 26,
      (byte) 113,
      (byte) 171,
      (byte) 153,
      (byte) 64 /*0x40*/,
      (byte) 158,
      (byte) 5,
      (byte) 84,
      (byte) 69,
      (byte) 141,
      (byte) 188
    };
    byte[] numArray6 = new byte[20];
    numArray6[0] = (byte) 78;
    numArray6[1] = (byte) 65;
    numArray6[3] = (byte) 60;
    numArray6[14] = (byte) 78;
    numArray6[11] = (byte) 199;
    numArray6[5] = (byte) 130;
    numArray6[6] = (byte) 52;
    numArray6[18] = (byte) 174;
    numArray6[17] = (byte) 5;
    numArray6[13] = (byte) 242;
    numArray6[10] = (byte) 127 /*0x7F*/;
    numArray6[2] = (byte) 84;
    numArray6[7] = (byte) 200;
    numArray6[8] = (byte) 237;
    numArray6[9] = (byte) 191;
    numArray6[15] = (byte) 148;
    numArray6[16 /*0x10*/] = (byte) 69;
    numArray6[12] = (byte) 115;
    numArray6[4] = (byte) 5;
    numArray6[19] = (byte) 201;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
