// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7214
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7214
{
  internal static string ssp_imclient_7215()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 123,
        (byte) 13,
        (byte) 120,
        (byte) 120,
        (byte) 123,
        (byte) 240 /*0xF0*/,
        byte.MaxValue,
        (byte) 119,
        (byte) 20,
        (byte) 132,
        (byte) 105,
        (byte) 251,
        (byte) 14,
        (byte) 131,
        (byte) 37,
        (byte) 234
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[6] = (byte) 93;
      numArray3[1] = (byte) 222;
      numArray3[12] = (byte) 48 /*0x30*/;
      numArray3[3] = (byte) 195;
      numArray3[4] = (byte) 205;
      numArray3[5] = (byte) 191;
      numArray3[14] = (byte) 157;
      numArray3[7] = (byte) 179;
      numArray3[13] = (byte) 32 /*0x20*/;
      numArray3[9] = (byte) 55;
      numArray3[8] = (byte) 173;
      numArray3[2] = (byte) 137;
      numArray3[0] = (byte) 150;
      numArray3[11] = (byte) 74;
      numArray3[15] = (byte) 231;
      numArray3[10] = (byte) 136;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 8,
      (byte) 68,
      (byte) 126,
      (byte) 100,
      (byte) 170,
      (byte) 54,
      (byte) 238,
      (byte) 51,
      (byte) 223,
      (byte) 91,
      (byte) 101,
      (byte) 235,
      (byte) 239,
      (byte) 143,
      (byte) 90,
      (byte) 82
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[12] = (byte) 103;
    numArray6[2] = (byte) 71;
    numArray6[15] = (byte) 36;
    numArray6[7] = (byte) 190;
    numArray6[4] = (byte) 46;
    numArray6[5] = (byte) 18;
    numArray6[13] = (byte) 179;
    numArray6[6] = (byte) 113;
    numArray6[1] = (byte) 46;
    numArray6[9] = (byte) 180;
    numArray6[3] = (byte) 72;
    numArray6[11] = (byte) 90;
    numArray6[8] = (byte) 176 /*0xB0*/;
    numArray6[0] = (byte) 226;
    numArray6[14] = (byte) 195;
    numArray6[10] = (byte) 190;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
