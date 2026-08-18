// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12552
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12552
{
  internal static int ssp_appserver_12553(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 147,
      (byte) 2,
      (byte) 203,
      (byte) 232,
      (byte) 135,
      (byte) 44,
      (byte) 132,
      (byte) 206,
      (byte) 248,
      (byte) 166,
      (byte) 39,
      (byte) 124,
      (byte) 145,
      (byte) 149,
      (byte) 247,
      (byte) 151,
      (byte) 34,
      (byte) 86,
      (byte) 112 /*0x70*/,
      (byte) 204,
      (byte) 213,
      (byte) 97,
      (byte) 9,
      (byte) 193,
      (byte) 99,
      (byte) 226,
      (byte) 189,
      (byte) 253,
      (byte) 115,
      (byte) 38,
      (byte) 89,
      (byte) 140,
      (byte) 91,
      (byte) 37,
      (byte) 54,
      (byte) 57,
      (byte) 212,
      (byte) 38,
      (byte) 233,
      (byte) 89,
      (byte) 253,
      (byte) 60,
      (byte) 106,
      (byte) 225,
      (byte) 137,
      (byte) 89,
      (byte) 217,
      (byte) 70
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 186,
      (byte) 107,
      (byte) 204,
      (byte) 159,
      (byte) 211,
      (byte) 120,
      (byte) 95,
      (byte) 204,
      (byte) 129,
      (byte) 22,
      (byte) 232,
      (byte) 150,
      (byte) 44,
      (byte) 45,
      (byte) 76,
      (byte) 44,
      (byte) 225,
      (byte) 128 /*0x80*/,
      (byte) 79,
      (byte) 135,
      (byte) 186,
      (byte) 49,
      (byte) 216,
      (byte) 87,
      (byte) 84,
      (byte) 31 /*0x1F*/,
      (byte) 146,
      (byte) 193,
      (byte) 48 /*0x30*/,
      (byte) 164,
      (byte) 115,
      (byte) 123,
      (byte) 235,
      (byte) 173,
      (byte) 43,
      (byte) 32 /*0x20*/,
      (byte) 230,
      (byte) 175,
      (byte) 173,
      (byte) 222,
      (byte) 181,
      (byte) 163,
      (byte) 14,
      (byte) 160 /*0xA0*/,
      (byte) 170,
      (byte) 205,
      (byte) 156,
      (byte) 202
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_appserver_12554(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 43,
      (byte) 1,
      (byte) 50,
      (byte) 249,
      (byte) 9,
      (byte) 133,
      (byte) 165,
      (byte) 111,
      (byte) 134,
      (byte) 90,
      (byte) 209,
      (byte) 77,
      (byte) 154,
      (byte) 33,
      (byte) 107,
      (byte) 95,
      (byte) 189,
      (byte) 195,
      (byte) 251,
      (byte) 108,
      (byte) 22,
      (byte) 207,
      (byte) 152,
      (byte) 247,
      (byte) 39,
      (byte) 56,
      (byte) 9,
      (byte) 8,
      (byte) 118,
      (byte) 171,
      (byte) 57,
      (byte) 110,
      (byte) 99,
      (byte) 82,
      (byte) 47,
      (byte) 36,
      (byte) 4,
      (byte) 63 /*0x3F*/,
      (byte) 202,
      (byte) 161,
      (byte) 67,
      (byte) 234,
      (byte) 118,
      (byte) 226,
      (byte) 152,
      (byte) 148,
      (byte) 239,
      (byte) 56
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 129,
      (byte) 192 /*0xC0*/,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 17,
      (byte) 145,
      (byte) 123,
      (byte) 152,
      (byte) 130,
      (byte) 206,
      (byte) 161,
      (byte) 219,
      (byte) 29,
      (byte) 52,
      (byte) 108,
      (byte) 248,
      (byte) 88,
      (byte) 43,
      (byte) 42,
      (byte) 247,
      (byte) 213,
      (byte) 88,
      (byte) 236,
      (byte) 222,
      (byte) 172,
      (byte) 33,
      (byte) 170,
      (byte) 203,
      (byte) 18,
      (byte) 61,
      (byte) 175,
      (byte) 43,
      (byte) 86,
      (byte) 145,
      (byte) 197,
      (byte) 234,
      (byte) 122,
      (byte) 57,
      (byte) 189,
      (byte) 130,
      (byte) 65,
      (byte) 26,
      (byte) 247,
      (byte) 140,
      (byte) 171,
      (byte) 37,
      (byte) 90,
      (byte) 252
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
