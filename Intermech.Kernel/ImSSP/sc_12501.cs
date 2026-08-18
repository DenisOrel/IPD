// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12501
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12501
{
  internal static string ssp_appserver_12502()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[5] = (byte) 177;
      numArray2[1] = (byte) 134;
      numArray2[2] = (byte) 177;
      numArray2[7] = (byte) 242;
      numArray2[4] = (byte) 54;
      numArray2[3] = (byte) 203;
      numArray2[8] = (byte) 160 /*0xA0*/;
      numArray2[0] = (byte) 245;
      numArray2[6] = (byte) 78;
      numArray2[9] = (byte) 61;
      byte[] numArray3 = new byte[10]
      {
        (byte) 198,
        (byte) 76,
        (byte) 102,
        (byte) 229,
        (byte) 240 /*0xF0*/,
        (byte) 20,
        (byte) 123,
        (byte) 158,
        (byte) 27,
        (byte) 25
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10];
    numArray5[5] = (byte) 136;
    numArray5[0] = (byte) 83;
    numArray5[4] = (byte) 15;
    numArray5[3] = (byte) 90;
    numArray5[1] = (byte) 88;
    numArray5[8] = (byte) 89;
    numArray5[6] = (byte) 54;
    numArray5[7] = (byte) 101;
    numArray5[2] = (byte) 108;
    numArray5[9] = (byte) 167;
    byte[] numArray6 = new byte[10]
    {
      (byte) 119,
      (byte) 64 /*0x40*/,
      (byte) 215,
      (byte) 99,
      (byte) 209,
      (byte) 71,
      (byte) 64 /*0x40*/,
      (byte) 204,
      (byte) 52,
      (byte) 118
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
