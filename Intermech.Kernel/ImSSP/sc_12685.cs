// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12685
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12685
{
  internal static string ssp_appserver_12686()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[31 /*0x1F*/];
      byte[] numArray2 = new byte[31 /*0x1F*/]
      {
        (byte) 213,
        (byte) 52,
        (byte) 103,
        (byte) 252,
        (byte) 132,
        (byte) 164,
        (byte) 141,
        (byte) 215,
        (byte) 29,
        (byte) 136,
        (byte) 210,
        (byte) 45,
        (byte) 12,
        (byte) 5,
        (byte) 181,
        (byte) 121,
        (byte) 94,
        (byte) 9,
        (byte) 156,
        (byte) 9,
        (byte) 3,
        (byte) 137,
        (byte) 3,
        (byte) 132,
        (byte) 86,
        (byte) 16 /*0x10*/,
        (byte) 111,
        (byte) 132,
        (byte) 6,
        (byte) 170,
        (byte) 4
      };
      byte[] numArray3 = new byte[31 /*0x1F*/];
      numArray3[10] = (byte) 230;
      numArray3[14] = (byte) 129;
      numArray3[2] = (byte) 31 /*0x1F*/;
      numArray3[3] = (byte) 48 /*0x30*/;
      numArray3[12] = (byte) 177;
      numArray3[20] = (byte) 143;
      numArray3[30] = (byte) 133;
      numArray3[7] = (byte) 101;
      numArray3[8] = (byte) 214;
      numArray3[5] = (byte) 103;
      numArray3[28] = (byte) 1;
      numArray3[4] = (byte) 116;
      numArray3[25] = (byte) 43;
      numArray3[1] = (byte) 40;
      numArray3[11] = (byte) 1;
      numArray3[16 /*0x10*/] = (byte) 65;
      numArray3[27] = (byte) 86;
      numArray3[15] = (byte) 10;
      numArray3[18] = (byte) 51;
      numArray3[19] = (byte) 231;
      numArray3[29] = (byte) 153;
      numArray3[21] = (byte) 189;
      numArray3[22] = (byte) 25;
      numArray3[23] = (byte) 215;
      numArray3[24] = (byte) 80 /*0x50*/;
      numArray3[9] = (byte) 119;
      numArray3[13] = (byte) 46;
      numArray3[26] = (byte) 35;
      numArray3[0] = (byte) 34;
      numArray3[6] = (byte) 20;
      numArray3[17] = (byte) 249;
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 31 /*0x1F*/);
      for (int index = 0; index < 31 /*0x1F*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[31 /*0x1F*/];
    byte[] numArray5 = new byte[31 /*0x1F*/]
    {
      (byte) 66,
      (byte) 240 /*0xF0*/,
      (byte) 213,
      (byte) 179,
      (byte) 127 /*0x7F*/,
      byte.MaxValue,
      (byte) 231,
      (byte) 35,
      (byte) 66,
      (byte) 134,
      (byte) 91,
      (byte) 6,
      (byte) 197,
      (byte) 166,
      (byte) 246,
      (byte) 54,
      (byte) 183,
      (byte) 149,
      (byte) 6,
      (byte) 196,
      (byte) 196,
      (byte) 85,
      (byte) 244,
      (byte) 91,
      (byte) 5,
      (byte) 4,
      (byte) 151,
      (byte) 208 /*0xD0*/,
      (byte) 114,
      (byte) 37,
      (byte) 123
    };
    byte[] numArray6 = new byte[31 /*0x1F*/]
    {
      (byte) 29,
      (byte) 4,
      (byte) 235,
      (byte) 15,
      (byte) 90,
      (byte) 41,
      (byte) 84,
      (byte) 251,
      (byte) 27,
      (byte) 191,
      (byte) 154,
      (byte) 95,
      (byte) 52,
      (byte) 34,
      (byte) 50,
      (byte) 209,
      (byte) 230,
      (byte) 103,
      (byte) 148,
      (byte) 41,
      (byte) 24,
      (byte) 69,
      (byte) 5,
      (byte) 212,
      (byte) 44,
      (byte) 197,
      (byte) 66,
      (byte) 121,
      (byte) 155,
      (byte) 40,
      (byte) 8
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 31 /*0x1F*/);
    for (int index = 0; index < 31 /*0x1F*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
