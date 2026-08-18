// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14226
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14226
{
  private static byte[] sspq = new byte[17]
  {
    (byte) 167,
    (byte) 232,
    (byte) 40,
    (byte) 103,
    (byte) 201,
    (byte) 244,
    (byte) 132,
    (byte) 30,
    (byte) 90,
    (byte) 84,
    (byte) 32 /*0x20*/,
    (byte) 108,
    (byte) 187,
    (byte) 49,
    (byte) 141,
    (byte) 159,
    (byte) 189
  };
  private static byte[] sspr = new byte[17]
  {
    (byte) 5,
    (byte) 41,
    (byte) 118,
    (byte) 154,
    (byte) 227,
    (byte) 135,
    (byte) 26,
    (byte) 9,
    (byte) 117,
    (byte) 83,
    (byte) 171,
    (byte) 89,
    (byte) 191,
    (byte) 178,
    (byte) 235,
    (byte) 112 /*0x70*/,
    (byte) 54
  };

  internal static string ssp_appserver_14227()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[15];
      byte[] numArray2 = new byte[15]
      {
        (byte) 145,
        (byte) 110,
        (byte) 90,
        (byte) 33,
        (byte) 250,
        (byte) 234,
        (byte) 213,
        (byte) 157,
        (byte) 131,
        (byte) 155,
        (byte) 187,
        (byte) 239,
        (byte) 32 /*0x20*/,
        (byte) 150,
        (byte) 5
      };
      byte[] numArray3 = new byte[15];
      numArray3[12] = (byte) 169;
      numArray3[0] = (byte) 162;
      numArray3[2] = (byte) 12;
      numArray3[8] = (byte) 102;
      numArray3[13] = (byte) 4;
      numArray3[5] = (byte) 191;
      numArray3[4] = (byte) 38;
      numArray3[7] = (byte) 107;
      numArray3[1] = (byte) 99;
      numArray3[9] = (byte) 183;
      numArray3[6] = (byte) 192 /*0xC0*/;
      numArray3[11] = (byte) 56;
      numArray3[10] = (byte) 27;
      numArray3[3] = (byte) 205;
      numArray3[14] = (byte) 26;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 15);
      for (int index = 0; index < 15; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[17];
      byte[] response = new byte[17];
      Array.Copy((Array) sc_14226.sspq, 0, (Array) numArray4, 0, 17);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_14226.sspr, 0, (Array) numArray4, 0, 17);
      for (int index = 0; index < numArray4.Length; ++index)
      {
        if ((int) numArray4[index] != (int) response[index])
        {
          key.TagValue = (int) response[index];
          break;
        }
      }
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray5 = new byte[15];
    byte[] numArray6 = new byte[15]
    {
      (byte) 12,
      (byte) 163,
      (byte) 53,
      (byte) 71,
      (byte) 44,
      (byte) 171,
      (byte) 14,
      (byte) 39,
      (byte) 67,
      (byte) 191,
      (byte) 134,
      (byte) 74,
      (byte) 103,
      (byte) 251,
      (byte) 19
    };
    byte[] numArray7 = new byte[15]
    {
      (byte) 95,
      (byte) 75,
      (byte) 163,
      (byte) 22,
      (byte) 236,
      (byte) 187,
      (byte) 194,
      (byte) 219,
      (byte) 24,
      (byte) 7,
      (byte) 235,
      (byte) 223,
      (byte) 200,
      (byte) 62,
      (byte) 155
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 15);
    for (int index = 0; index < 15; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
