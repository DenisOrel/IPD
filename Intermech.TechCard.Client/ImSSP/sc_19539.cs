// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19539
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19539
{
  internal static int ssp_techcard_19540(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 101,
      (byte) 248,
      (byte) 224 /*0xE0*/,
      (byte) 48 /*0x30*/,
      (byte) 104,
      (byte) 130,
      (byte) 119,
      (byte) 185,
      (byte) 54,
      (byte) 86,
      (byte) 86,
      (byte) 163,
      (byte) 185,
      (byte) 222,
      (byte) 34,
      (byte) 11,
      (byte) 25,
      (byte) 192 /*0xC0*/,
      (byte) 97,
      (byte) 216,
      (byte) 148,
      (byte) 226,
      (byte) 151,
      (byte) 51,
      (byte) 142,
      (byte) 13,
      (byte) 54,
      (byte) 168,
      (byte) 146,
      (byte) 182,
      (byte) 185,
      (byte) 244,
      (byte) 183,
      (byte) 178,
      (byte) 146,
      (byte) 76,
      (byte) 23,
      (byte) 186,
      (byte) 243,
      (byte) 94,
      (byte) 88,
      (byte) 106,
      (byte) 17,
      (byte) 117,
      (byte) 180,
      (byte) 231,
      (byte) 69,
      (byte) 250
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 95,
      (byte) 246,
      (byte) 174,
      (byte) 177,
      (byte) 244,
      (byte) 35,
      (byte) 58,
      (byte) 233,
      (byte) 30,
      (byte) 197,
      (byte) 147,
      (byte) 100,
      (byte) 189,
      (byte) 0,
      (byte) 205,
      (byte) 198,
      (byte) 119,
      (byte) 216,
      (byte) 49,
      (byte) 76,
      (byte) 16 /*0x10*/,
      (byte) 157,
      (byte) 153,
      (byte) 185,
      (byte) 175,
      (byte) 166,
      (byte) 194,
      (byte) 209,
      (byte) 140,
      (byte) 182,
      (byte) 227,
      (byte) 3,
      (byte) 3,
      (byte) 242,
      (byte) 70,
      (byte) 100,
      (byte) 181,
      (byte) 136,
      (byte) 225,
      (byte) 243,
      (byte) 254,
      (byte) 140,
      (byte) 205,
      (byte) 143,
      (byte) 119,
      (byte) 90,
      (byte) 228,
      (byte) 195
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
