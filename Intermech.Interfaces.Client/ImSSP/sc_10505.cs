// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10505
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_10505
{
  internal static int ssp_appserver_10506(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 114,
      (byte) 28,
      (byte) 125,
      (byte) 23,
      (byte) 179,
      (byte) 93,
      (byte) 74,
      (byte) 181,
      (byte) 59,
      (byte) 179,
      (byte) 219,
      (byte) 213,
      (byte) 40,
      (byte) 65,
      (byte) 55,
      (byte) 132,
      (byte) 237,
      (byte) 72,
      (byte) 237,
      (byte) 43,
      (byte) 172,
      (byte) 189,
      (byte) 193,
      (byte) 34,
      (byte) 219,
      (byte) 49,
      (byte) 240 /*0xF0*/,
      (byte) 108,
      (byte) 59,
      (byte) 157,
      (byte) 170,
      (byte) 144 /*0x90*/,
      (byte) 47,
      (byte) 57,
      (byte) 83,
      (byte) 96 /*0x60*/,
      (byte) 175,
      (byte) 130,
      (byte) 15,
      (byte) 41,
      (byte) 55,
      (byte) 31 /*0x1F*/,
      (byte) 219,
      (byte) 3,
      (byte) 26,
      (byte) 31 /*0x1F*/,
      (byte) 240 /*0xF0*/,
      (byte) 174
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 210,
      (byte) 17,
      (byte) 224 /*0xE0*/,
      (byte) 160 /*0xA0*/,
      (byte) 200,
      (byte) 89,
      (byte) 227,
      (byte) 147,
      (byte) 137,
      (byte) 67,
      (byte) 172,
      (byte) 38,
      (byte) 233,
      (byte) 76,
      (byte) 97,
      (byte) 222,
      (byte) 219,
      (byte) 34,
      (byte) 108,
      (byte) 26,
      (byte) 76,
      (byte) 157,
      (byte) 20,
      (byte) 5,
      (byte) 84,
      (byte) 79,
      (byte) 24,
      (byte) 40,
      (byte) 190,
      (byte) 138,
      (byte) 91,
      (byte) 225,
      (byte) 157,
      (byte) 35,
      (byte) 57,
      (byte) 70,
      (byte) 145,
      (byte) 25,
      (byte) 19,
      (byte) 104,
      (byte) 248,
      (byte) 58,
      (byte) 17,
      (byte) 227,
      (byte) 158,
      (byte) 58,
      (byte) 228,
      (byte) 88
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
