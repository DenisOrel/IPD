// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7216
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7216
{
  internal static string ssp_imclient_7217()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 247,
        (byte) 161,
        (byte) 26,
        (byte) 146,
        (byte) 42,
        (byte) 155,
        (byte) 5,
        (byte) 8,
        (byte) 250,
        (byte) 29,
        (byte) 85,
        (byte) 109,
        (byte) 173,
        (byte) 156,
        (byte) 69,
        (byte) 78
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 134,
        (byte) 202,
        (byte) 229,
        (byte) 37,
        (byte) 28,
        (byte) 159,
        (byte) 209,
        (byte) 159,
        (byte) 6,
        (byte) 203,
        (byte) 153,
        (byte) 18,
        (byte) 65,
        (byte) 116,
        (byte) 5,
        (byte) 20
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[14] = (byte) 180;
    numArray5[1] = (byte) 49;
    numArray5[2] = (byte) 101;
    numArray5[12] = (byte) 63 /*0x3F*/;
    numArray5[8] = (byte) 224 /*0xE0*/;
    numArray5[4] = byte.MaxValue;
    numArray5[6] = (byte) 126;
    numArray5[7] = (byte) 13;
    numArray5[5] = (byte) 51;
    numArray5[11] = (byte) 211;
    numArray5[10] = (byte) 122;
    numArray5[3] = (byte) 242;
    numArray5[0] = (byte) 223;
    numArray5[13] = (byte) 114;
    numArray5[15] = (byte) 90;
    numArray5[9] = (byte) 171;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 37,
      (byte) 8,
      (byte) 4,
      (byte) 3,
      (byte) 129,
      (byte) 43,
      (byte) 99,
      (byte) 174,
      (byte) 99,
      (byte) 250,
      (byte) 124,
      (byte) 247,
      (byte) 38,
      (byte) 60,
      (byte) 159,
      (byte) 121
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
