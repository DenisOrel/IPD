// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7230
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7230
{
  internal static string ssp_imclient_7231()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 2,
        (byte) 23,
        (byte) 204,
        (byte) 81,
        (byte) 10,
        (byte) 26,
        (byte) 125,
        (byte) 61,
        (byte) 111,
        (byte) 15,
        (byte) 17,
        (byte) 217,
        (byte) 11,
        (byte) 215,
        (byte) 229,
        (byte) 8
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[1] = (byte) 152;
      numArray3[9] = (byte) 75;
      numArray3[3] = (byte) 252;
      numArray3[13] = (byte) 5;
      numArray3[8] = (byte) 6;
      numArray3[0] = (byte) 147;
      numArray3[6] = (byte) 244;
      numArray3[12] = (byte) 159;
      numArray3[4] = (byte) 27;
      numArray3[7] = (byte) 11;
      numArray3[11] = (byte) 101;
      numArray3[2] = (byte) 145;
      numArray3[15] = (byte) 72;
      numArray3[10] = (byte) 45;
      numArray3[14] = (byte) 197;
      numArray3[5] = (byte) 159;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 120,
      (byte) 73,
      (byte) 236,
      (byte) 12,
      (byte) 148,
      (byte) 35,
      (byte) 111,
      (byte) 148,
      (byte) 47,
      (byte) 88,
      (byte) 96 /*0x60*/,
      (byte) 79,
      (byte) 112 /*0x70*/,
      (byte) 151,
      (byte) 203,
      (byte) 8
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[9] = (byte) 253;
    numArray6[13] = (byte) 132;
    numArray6[1] = (byte) 101;
    numArray6[3] = (byte) 35;
    numArray6[4] = (byte) 164;
    numArray6[0] = (byte) 217;
    numArray6[6] = (byte) 79;
    numArray6[5] = (byte) 161;
    numArray6[15] = (byte) 107;
    numArray6[2] = (byte) 190;
    numArray6[10] = (byte) 116;
    numArray6[11] = (byte) 90;
    numArray6[12] = (byte) 216;
    numArray6[7] = (byte) 62;
    numArray6[14] = (byte) 124;
    numArray6[8] = (byte) 4;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
