// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12570
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12570
{
  private static byte[] sspq = new byte[10]
  {
    (byte) 153,
    (byte) 224 /*0xE0*/,
    (byte) 224 /*0xE0*/,
    (byte) 130,
    (byte) 85,
    (byte) 117,
    (byte) 107,
    (byte) 33,
    (byte) 65,
    (byte) 185
  };
  private static byte[] sspr = new byte[10]
  {
    (byte) 134,
    (byte) 58,
    (byte) 161,
    (byte) 48 /*0x30*/,
    (byte) 24,
    (byte) 225,
    (byte) 62,
    (byte) 196,
    (byte) 94,
    (byte) 32 /*0x20*/
  };

  internal static string ssp_appserver_12571()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10]
      {
        (byte) 142,
        (byte) 176 /*0xB0*/,
        (byte) 61,
        (byte) 176 /*0xB0*/,
        (byte) 219,
        (byte) 36,
        (byte) 133,
        (byte) 127 /*0x7F*/,
        (byte) 147,
        (byte) 240 /*0xF0*/
      };
      byte[] numArray3 = new byte[10]
      {
        (byte) 168,
        (byte) 154,
        (byte) 62,
        (byte) 73,
        (byte) 236,
        (byte) 7,
        (byte) 154,
        (byte) 215,
        (byte) 172,
        byte.MaxValue
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      byte[] numArray4 = new byte[10];
      byte[] response = new byte[10];
      Array.Copy((Array) sc_12570.sspq, 0, (Array) numArray4, 0, 10);
      key.Query(true, 335, numArray4, response);
      Array.Copy((Array) sc_12570.sspr, 0, (Array) numArray4, 0, 10);
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
    byte[] numArray5 = new byte[10];
    byte[] numArray6 = new byte[10]
    {
      (byte) 97,
      (byte) 37,
      (byte) 157,
      (byte) 251,
      (byte) 144 /*0x90*/,
      (byte) 216,
      (byte) 155,
      (byte) 72,
      (byte) 131,
      (byte) 56
    };
    byte[] numArray7 = new byte[10]
    {
      (byte) 177,
      (byte) 7,
      (byte) 61,
      (byte) 76,
      (byte) 87,
      (byte) 245,
      (byte) 43,
      (byte) 105,
      (byte) 117,
      (byte) 179
    };
    key.Query(true, 335, numArray6, numArray6);
    Array.Copy((Array) numArray6, 0, (Array) numArray5, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray5[index] ^= numArray7[index];
    return Encoding.UTF8.GetString(numArray5);
  }
}
