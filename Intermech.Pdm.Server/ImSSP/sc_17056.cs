// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_17056
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_17056
{
  internal static int ssp_appserver_17057(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 45,
      (byte) 70,
      (byte) 63 /*0x3F*/,
      (byte) 54,
      (byte) 19,
      (byte) 58,
      (byte) 136,
      (byte) 7,
      (byte) 196,
      (byte) 166,
      (byte) 57,
      (byte) 186,
      (byte) 38,
      (byte) 99,
      (byte) 233,
      (byte) 42,
      (byte) 132,
      (byte) 217,
      (byte) 233,
      (byte) 224 /*0xE0*/,
      (byte) 152,
      (byte) 163,
      (byte) 64 /*0x40*/,
      (byte) 85,
      (byte) 213,
      (byte) 209,
      (byte) 253,
      (byte) 85,
      (byte) 173,
      (byte) 127 /*0x7F*/,
      byte.MaxValue,
      (byte) 158,
      (byte) 148,
      (byte) 216,
      (byte) 42,
      (byte) 110,
      (byte) 4,
      (byte) 137,
      (byte) 182,
      (byte) 187,
      (byte) 104,
      (byte) 17,
      (byte) 251,
      (byte) 155,
      (byte) 142,
      (byte) 84,
      (byte) 238
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 79,
      (byte) 14,
      (byte) 161,
      (byte) 234,
      (byte) 179,
      (byte) 201,
      (byte) 55,
      (byte) 135,
      (byte) 217,
      (byte) 65,
      (byte) 164,
      (byte) 237,
      (byte) 227,
      (byte) 80 /*0x50*/,
      (byte) 186,
      (byte) 243,
      (byte) 153,
      (byte) 213,
      (byte) 207,
      (byte) 95,
      (byte) 13,
      (byte) 83,
      (byte) 62,
      (byte) 236,
      (byte) 218,
      (byte) 8,
      (byte) 245,
      (byte) 146,
      (byte) 15,
      (byte) 40,
      (byte) 130,
      (byte) 135,
      (byte) 97,
      (byte) 120,
      (byte) 67,
      (byte) 66,
      (byte) 119,
      (byte) 216,
      (byte) 134,
      (byte) 48 /*0x30*/,
      (byte) 19,
      (byte) 104,
      (byte) 16 /*0x10*/,
      (byte) 171,
      (byte) 218,
      (byte) 197,
      (byte) 37,
      (byte) 232
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
