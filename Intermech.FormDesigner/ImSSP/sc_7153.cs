// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7153
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7153
{
  internal static string ssp_imclient_7154()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 44,
        (byte) 156,
        (byte) 211,
        (byte) 139,
        (byte) 172,
        (byte) 171,
        (byte) 24,
        (byte) 240 /*0xF0*/,
        (byte) 178,
        (byte) 219,
        (byte) 216,
        (byte) 87,
        (byte) 76,
        (byte) 232,
        (byte) 152,
        (byte) 123
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 197,
        (byte) 98,
        (byte) 183,
        (byte) 77,
        (byte) 197,
        (byte) 252,
        (byte) 119,
        (byte) 116,
        (byte) 3,
        (byte) 247,
        (byte) 227,
        (byte) 103,
        (byte) 31 /*0x1F*/,
        (byte) 45,
        (byte) 205,
        (byte) 132
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
      (byte) 224 /*0xE0*/,
      (byte) 193,
      (byte) 211,
      (byte) 211,
      (byte) 11,
      (byte) 237,
      (byte) 53,
      (byte) 79,
      (byte) 215,
      (byte) 164,
      (byte) 165,
      (byte) 117,
      (byte) 215,
      (byte) 83,
      (byte) 98,
      (byte) 217
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[7] = (byte) 136;
    numArray6[9] = (byte) 76;
    numArray6[2] = (byte) 121;
    numArray6[11] = (byte) 96 /*0x60*/;
    numArray6[4] = (byte) 110;
    numArray6[5] = (byte) 86;
    numArray6[15] = (byte) 129;
    numArray6[1] = (byte) 201;
    numArray6[8] = (byte) 251;
    numArray6[13] = (byte) 22;
    numArray6[10] = (byte) 200;
    numArray6[0] = (byte) 209;
    numArray6[12] = (byte) 28;
    numArray6[14] = (byte) 6;
    numArray6[6] = (byte) 220;
    numArray6[3] = (byte) 135;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
