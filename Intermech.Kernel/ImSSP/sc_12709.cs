// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12709
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12709
{
  private static byte[] sspq = new byte[10]
  {
    (byte) 80 /*0x50*/,
    (byte) 29,
    (byte) 140,
    (byte) 30,
    (byte) 98,
    (byte) 72,
    (byte) 164,
    (byte) 103,
    (byte) 172,
    (byte) 155
  };
  private static byte[] sspr = new byte[10]
  {
    (byte) 11,
    (byte) 78,
    (byte) 128 /*0x80*/,
    (byte) 85,
    (byte) 235,
    (byte) 150,
    (byte) 10,
    (byte) 211,
    (byte) 145,
    (byte) 107
  };

  internal static string ssp_appserver_12710()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[1] = (byte) 223;
      numArray2[4] = (byte) 230;
      numArray2[2] = (byte) 125;
      numArray2[6] = (byte) 52;
      numArray2[0] = (byte) 28;
      numArray2[5] = (byte) 221;
      numArray2[3] = (byte) 18;
      numArray2[7] = (byte) 232;
      numArray2[8] = (byte) 23;
      numArray2[9] = (byte) 24;
      byte[] numArray3 = new byte[10];
      numArray3[3] = (byte) 234;
      numArray3[1] = (byte) 196;
      numArray3[2] = (byte) 114;
      numArray3[0] = (byte) 154;
      numArray3[8] = (byte) 172;
      numArray3[7] = (byte) 45;
      numArray3[5] = (byte) 115;
      numArray3[6] = (byte) 246;
      numArray3[4] = (byte) 208 /*0xD0*/;
      numArray3[9] = (byte) 193;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 184,
      (byte) 101,
      (byte) 217,
      (byte) 50,
      (byte) 226,
      (byte) 157,
      (byte) 116,
      (byte) 71,
      (byte) 170,
      (byte) 108
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 172,
      (byte) 139,
      (byte) 34,
      byte.MaxValue,
      (byte) 81,
      (byte) 196,
      (byte) 83,
      (byte) 41,
      (byte) 133,
      (byte) 47
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[10];
    byte[] response = new byte[10];
    Array.Copy((Array) sc_12709.sspq, 0, (Array) numArray7, 0, 10);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12709.sspr, 0, (Array) numArray7, 0, 10);
    for (int index = 0; index < numArray7.Length; ++index)
    {
      if ((int) numArray7[index] != (int) response[index])
      {
        key.TagValue = (int) response[index];
        break;
      }
    }
    return Encoding.UTF8.GetString(numArray4);
  }
}
