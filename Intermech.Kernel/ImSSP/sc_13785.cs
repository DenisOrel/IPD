// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13785
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13785
{
  private static byte[] sspq = new byte[25]
  {
    (byte) 200,
    (byte) 122,
    (byte) 224 /*0xE0*/,
    (byte) 202,
    (byte) 214,
    (byte) 207,
    (byte) 95,
    (byte) 132,
    (byte) 12,
    (byte) 252,
    (byte) 126,
    (byte) 234,
    (byte) 26,
    (byte) 211,
    (byte) 84,
    (byte) 97,
    (byte) 217,
    (byte) 144 /*0x90*/,
    (byte) 58,
    (byte) 62,
    (byte) 145,
    (byte) 112 /*0x70*/,
    (byte) 196,
    (byte) 26,
    (byte) 220
  };
  private static byte[] sspr = new byte[25]
  {
    (byte) 222,
    (byte) 43,
    (byte) 206,
    (byte) 168,
    (byte) 228,
    (byte) 205,
    (byte) 171,
    (byte) 237,
    (byte) 250,
    (byte) 210,
    (byte) 128 /*0x80*/,
    (byte) 112 /*0x70*/,
    (byte) 100,
    (byte) 139,
    (byte) 236,
    (byte) 24,
    (byte) 86,
    (byte) 91,
    (byte) 122,
    (byte) 46,
    (byte) 234,
    (byte) 249,
    (byte) 187,
    (byte) 166,
    (byte) 243
  };

  internal static int ssp_appserver_13786(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 190,
      (byte) 2,
      (byte) 155,
      (byte) 211,
      (byte) 218,
      (byte) 36,
      (byte) 215,
      (byte) 124,
      (byte) 69,
      (byte) 206,
      (byte) 233,
      (byte) 57,
      (byte) 156,
      (byte) 196,
      (byte) 101,
      (byte) 55,
      (byte) 196,
      (byte) 176 /*0xB0*/,
      (byte) 134,
      (byte) 40,
      (byte) 153,
      (byte) 244,
      (byte) 43,
      (byte) 204,
      (byte) 116,
      (byte) 85,
      (byte) 24,
      (byte) 218,
      (byte) 254,
      (byte) 90,
      (byte) 180,
      (byte) 21,
      (byte) 229,
      (byte) 58,
      (byte) 235,
      (byte) 212,
      (byte) 253,
      (byte) 133,
      (byte) 30,
      (byte) 22,
      (byte) 100,
      (byte) 126,
      (byte) 23,
      byte.MaxValue,
      (byte) 126,
      (byte) 161,
      (byte) 120,
      (byte) 171
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 43,
      (byte) 217,
      (byte) 29,
      (byte) 243,
      (byte) 91,
      (byte) 22,
      (byte) 90,
      (byte) 110,
      (byte) 163,
      (byte) 20,
      (byte) 251,
      (byte) 32 /*0x20*/,
      (byte) 220,
      (byte) 207,
      (byte) 114,
      (byte) 66,
      (byte) 239,
      (byte) 44,
      (byte) 5,
      (byte) 171,
      (byte) 232,
      (byte) 136,
      (byte) 243,
      (byte) 178,
      (byte) 243,
      (byte) 165,
      (byte) 190,
      (byte) 229,
      (byte) 4,
      (byte) 44,
      (byte) 174,
      (byte) 168,
      (byte) 76,
      (byte) 64 /*0x40*/,
      (byte) 252,
      (byte) 223,
      (byte) 128 /*0x80*/,
      (byte) 183,
      (byte) 58,
      (byte) 84,
      (byte) 177,
      (byte) 3,
      (byte) 49,
      (byte) 133,
      (byte) 137,
      (byte) 24,
      (byte) 11,
      (byte) 238
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[25];
    byte[] response2 = new byte[25];
    Array.Copy((Array) sc_13785.sspq, 0, (Array) numArray2, 0, 25);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13785.sspr, 0, (Array) numArray2, 0, 25);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }
}
