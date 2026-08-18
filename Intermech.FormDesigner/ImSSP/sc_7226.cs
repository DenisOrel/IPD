// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7226
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7226
{
  internal static string ssp_imclient_7227()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 54,
        (byte) 219,
        (byte) 124,
        (byte) 184,
        (byte) 176 /*0xB0*/,
        (byte) 23,
        (byte) 168,
        (byte) 35,
        (byte) 127 /*0x7F*/,
        (byte) 77,
        (byte) 206,
        (byte) 10,
        (byte) 104,
        (byte) 178,
        (byte) 117,
        (byte) 30
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 197,
        (byte) 103,
        (byte) 215,
        (byte) 117,
        (byte) 215,
        (byte) 210,
        (byte) 238,
        (byte) 246,
        (byte) 75,
        (byte) 52,
        (byte) 161,
        (byte) 23,
        (byte) 38,
        (byte) 53,
        (byte) 105,
        (byte) 185
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[4] = (byte) 175;
    numArray5[1] = (byte) 163;
    numArray5[11] = (byte) 62;
    numArray5[14] = (byte) 92;
    numArray5[12] = (byte) 189;
    numArray5[3] = (byte) 195;
    numArray5[6] = (byte) 166;
    numArray5[7] = (byte) 252;
    numArray5[8] = (byte) 43;
    numArray5[5] = (byte) 103;
    numArray5[10] = (byte) 15;
    numArray5[0] = (byte) 150;
    numArray5[2] = (byte) 51;
    numArray5[13] = (byte) 49;
    numArray5[9] = (byte) 230;
    numArray5[15] = (byte) 219;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 149,
      (byte) 59,
      (byte) 61,
      (byte) 68,
      (byte) 102,
      (byte) 67,
      (byte) 238,
      (byte) 120,
      (byte) 199,
      (byte) 163,
      (byte) 24,
      (byte) 209,
      (byte) 77,
      (byte) 188,
      (byte) 244,
      (byte) 239
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
