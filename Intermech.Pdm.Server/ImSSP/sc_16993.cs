// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_16993
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_16993
{
  internal static string ssp_pdm_server_16994()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[13];
      byte[] numArray2 = new byte[13]
      {
        (byte) 193,
        (byte) 164,
        (byte) 51,
        (byte) 246,
        (byte) 107,
        (byte) 147,
        (byte) 138,
        (byte) 122,
        (byte) 40,
        (byte) 166,
        (byte) 168,
        (byte) 124,
        (byte) 65
      };
      byte[] numArray3 = new byte[13];
      numArray3[2] = (byte) 101;
      numArray3[11] = (byte) 57;
      numArray3[7] = (byte) 186;
      numArray3[3] = (byte) 247;
      numArray3[4] = (byte) 67;
      numArray3[5] = (byte) 10;
      numArray3[6] = (byte) 253;
      numArray3[0] = (byte) 142;
      numArray3[8] = (byte) 180;
      numArray3[9] = (byte) 92;
      numArray3[10] = (byte) 151;
      numArray3[1] = (byte) 122;
      numArray3[12] = (byte) 237;
      key.Query(true, 350, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 13);
      for (int index = 0; index < 13; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[13];
    byte[] numArray5 = new byte[13]
    {
      (byte) 192 /*0xC0*/,
      (byte) 188,
      (byte) 43,
      (byte) 220,
      (byte) 103,
      (byte) 100,
      (byte) 122,
      (byte) 88,
      (byte) 47,
      (byte) 76,
      (byte) 45,
      (byte) 157,
      (byte) 211
    };
    byte[] numArray6 = new byte[13];
    numArray6[2] = (byte) 180;
    numArray6[4] = (byte) 68;
    numArray6[8] = (byte) 47;
    numArray6[3] = (byte) 77;
    numArray6[12] = (byte) 240 /*0xF0*/;
    numArray6[5] = (byte) 252;
    numArray6[6] = (byte) 19;
    numArray6[7] = (byte) 44;
    numArray6[10] = (byte) 19;
    numArray6[9] = (byte) 249;
    numArray6[11] = (byte) 217;
    numArray6[1] = (byte) 59;
    numArray6[0] = (byte) 214;
    key.Query(true, 350, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 13);
    for (int index = 0; index < 13; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
