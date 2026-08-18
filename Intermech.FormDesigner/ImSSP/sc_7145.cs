// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7145
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7145
{
  internal static string ssp_imclient_7146()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 167,
        (byte) 197,
        (byte) 224 /*0xE0*/,
        (byte) 167,
        (byte) 79,
        (byte) 156,
        (byte) 209,
        (byte) 157,
        (byte) 41,
        (byte) 247,
        (byte) 120,
        (byte) 230,
        (byte) 104,
        (byte) 125,
        (byte) 154,
        (byte) 250
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 254,
        (byte) 110,
        (byte) 41,
        (byte) 76,
        (byte) 209,
        (byte) 24,
        (byte) 209,
        (byte) 150,
        (byte) 248,
        (byte) 114,
        (byte) 91,
        (byte) 146,
        (byte) 36,
        (byte) 223,
        (byte) 4,
        (byte) 222
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[14] = (byte) 113;
    numArray5[1] = (byte) 227;
    numArray5[2] = (byte) 57;
    numArray5[12] = (byte) 221;
    numArray5[11] = (byte) 137;
    numArray5[13] = (byte) 82;
    numArray5[6] = (byte) 28;
    numArray5[10] = (byte) 206;
    numArray5[8] = (byte) 34;
    numArray5[0] = (byte) 137;
    numArray5[4] = (byte) 106;
    numArray5[7] = (byte) 181;
    numArray5[3] = (byte) 142;
    numArray5[9] = (byte) 46;
    numArray5[5] = (byte) 87;
    numArray5[15] = (byte) 99;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 28,
      (byte) 101,
      (byte) 51,
      (byte) 163,
      (byte) 143,
      (byte) 72,
      (byte) 53,
      (byte) 56,
      (byte) 26,
      (byte) 93,
      (byte) 196,
      (byte) 101,
      (byte) 180,
      (byte) 177,
      (byte) 165,
      (byte) 51
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
