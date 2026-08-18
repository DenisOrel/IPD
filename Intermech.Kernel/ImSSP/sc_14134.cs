// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14134
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_14134
{
  internal static string ssp_webportal_14135()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[14];
      byte[] numArray2 = new byte[14];
      numArray2[3] = (byte) 236;
      numArray2[12] = (byte) 233;
      numArray2[2] = (byte) 59;
      numArray2[9] = (byte) 6;
      numArray2[7] = (byte) 205;
      numArray2[5] = (byte) 188;
      numArray2[6] = (byte) 65;
      numArray2[0] = (byte) 128 /*0x80*/;
      numArray2[8] = (byte) 134;
      numArray2[10] = (byte) 53;
      numArray2[1] = (byte) 166;
      numArray2[11] = (byte) 202;
      numArray2[4] = (byte) 248;
      numArray2[13] = (byte) 196;
      byte[] numArray3 = new byte[14]
      {
        (byte) 181,
        (byte) 77,
        (byte) 245,
        (byte) 19,
        (byte) 71,
        (byte) 38,
        (byte) 173,
        (byte) 246,
        (byte) 64 /*0x40*/,
        (byte) 147,
        (byte) 189,
        (byte) 73,
        (byte) 23,
        (byte) 26
      };
      key.Query(true, 363, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 14);
      for (int index = 0; index < 14; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[14];
    byte[] numArray5 = new byte[14]
    {
      (byte) 207,
      (byte) 11,
      (byte) 75,
      (byte) 51,
      (byte) 173,
      (byte) 13,
      (byte) 165,
      (byte) 65,
      (byte) 174,
      (byte) 50,
      (byte) 35,
      (byte) 39,
      (byte) 21,
      (byte) 212
    };
    byte[] numArray6 = new byte[14];
    numArray6[10] = (byte) 3;
    numArray6[2] = (byte) 49;
    numArray6[13] = (byte) 43;
    numArray6[1] = (byte) 101;
    numArray6[4] = (byte) 210;
    numArray6[3] = (byte) 197;
    numArray6[6] = (byte) 0;
    numArray6[7] = (byte) 62;
    numArray6[8] = (byte) 251;
    numArray6[9] = (byte) 178;
    numArray6[5] = (byte) 228;
    numArray6[11] = (byte) 148;
    numArray6[12] = (byte) 169;
    numArray6[0] = (byte) 240 /*0xF0*/;
    key.Query(true, 363, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 14);
    for (int index = 0; index < 14; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
