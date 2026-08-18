// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12353
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12353
{
  private static byte[] sspq = new byte[41]
  {
    (byte) 158,
    (byte) 135,
    (byte) 206,
    (byte) 207,
    (byte) 116,
    (byte) 156,
    (byte) 21,
    (byte) 110,
    (byte) 50,
    (byte) 148,
    (byte) 175,
    (byte) 150,
    (byte) 172,
    (byte) 243,
    (byte) 17,
    (byte) 206,
    (byte) 32 /*0x20*/,
    (byte) 174,
    (byte) 92,
    (byte) 151,
    (byte) 62,
    (byte) 124,
    (byte) 119,
    (byte) 142,
    (byte) 234,
    (byte) 229,
    (byte) 205,
    (byte) 25,
    (byte) 1,
    (byte) 173,
    (byte) 146,
    (byte) 7,
    (byte) 131,
    (byte) 113,
    (byte) 117,
    (byte) 74,
    (byte) 198,
    (byte) 250,
    (byte) 76,
    (byte) 203,
    (byte) 207
  };
  private static byte[] sspr = new byte[41]
  {
    (byte) 134,
    (byte) 75,
    (byte) 91,
    (byte) 229,
    (byte) 201,
    (byte) 11,
    (byte) 7,
    (byte) 166,
    (byte) 187,
    (byte) 30,
    (byte) 53,
    (byte) 185,
    (byte) 159,
    (byte) 154,
    (byte) 13,
    (byte) 174,
    (byte) 217,
    (byte) 202,
    (byte) 81,
    (byte) 185,
    (byte) 253,
    (byte) 124,
    (byte) 117,
    (byte) 10,
    (byte) 30,
    (byte) 142,
    (byte) 12,
    (byte) 94,
    (byte) 163,
    (byte) 146,
    (byte) 243,
    (byte) 181,
    (byte) 133,
    (byte) 154,
    (byte) 178,
    (byte) 124,
    (byte) 54,
    (byte) 35,
    (byte) 16 /*0x10*/,
    (byte) 17,
    (byte) 134
  };

  internal static string ssp_appserver_12354()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[10];
      byte[] numArray2 = new byte[10];
      numArray2[9] = (byte) 43;
      numArray2[1] = (byte) 124;
      numArray2[0] = (byte) 196;
      numArray2[3] = (byte) 109;
      numArray2[4] = (byte) 55;
      numArray2[8] = (byte) 234;
      numArray2[6] = (byte) 112 /*0x70*/;
      numArray2[2] = (byte) 141;
      numArray2[5] = (byte) 38;
      numArray2[7] = (byte) 194;
      byte[] numArray3 = new byte[10]
      {
        (byte) 250,
        (byte) 34,
        (byte) 65,
        (byte) 36,
        (byte) 142,
        (byte) 156,
        (byte) 190,
        (byte) 164,
        (byte) 78,
        (byte) 101
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 10);
      for (int index = 0; index < 10; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[10];
    byte[] numArray5 = new byte[10]
    {
      (byte) 78,
      (byte) 240 /*0xF0*/,
      (byte) 192 /*0xC0*/,
      (byte) 151,
      (byte) 134,
      (byte) 221,
      (byte) 28,
      (byte) 18,
      (byte) 250,
      (byte) 73
    };
    byte[] numArray6 = new byte[10]
    {
      (byte) 235,
      (byte) 102,
      (byte) 124,
      (byte) 196,
      (byte) 167,
      (byte) 180,
      (byte) 69,
      (byte) 70,
      (byte) 51,
      (byte) 117
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 10);
    for (int index = 0; index < 10; ++index)
      numArray4[index] ^= numArray6[index];
    byte[] numArray7 = new byte[41];
    byte[] response = new byte[41];
    Array.Copy((Array) sc_12353.sspq, 0, (Array) numArray7, 0, 41);
    key.Query(true, 335, numArray7, response);
    Array.Copy((Array) sc_12353.sspr, 0, (Array) numArray7, 0, 41);
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
