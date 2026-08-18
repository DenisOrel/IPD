// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7210
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7210
{
  internal static string ssp_imclient_7211()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 109,
        (byte) 229,
        (byte) 95,
        (byte) 254,
        (byte) 127 /*0x7F*/,
        (byte) 22,
        (byte) 73,
        (byte) 41,
        (byte) 193,
        (byte) 3,
        (byte) 184,
        (byte) 177,
        (byte) 76,
        (byte) 184,
        (byte) 203,
        (byte) 230
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 8,
        (byte) 171,
        (byte) 205,
        (byte) 138,
        (byte) 88,
        (byte) 27,
        (byte) 40,
        (byte) 129,
        (byte) 183,
        (byte) 4,
        (byte) 63 /*0x3F*/,
        (byte) 39,
        (byte) 114,
        (byte) 6,
        (byte) 154,
        (byte) 78
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 21,
      (byte) 188,
      (byte) 247,
      (byte) 223,
      (byte) 154,
      (byte) 54,
      (byte) 102,
      (byte) 71,
      (byte) 100,
      (byte) 197,
      (byte) 97,
      (byte) 250,
      (byte) 115,
      (byte) 180,
      (byte) 68,
      (byte) 200
    };
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 198,
      (byte) 250,
      (byte) 191,
      (byte) 247,
      (byte) 171,
      (byte) 225,
      (byte) 28,
      (byte) 247,
      (byte) 65,
      (byte) 213,
      (byte) 121,
      (byte) 119,
      (byte) 38,
      (byte) 42,
      (byte) 101,
      (byte) 44
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
