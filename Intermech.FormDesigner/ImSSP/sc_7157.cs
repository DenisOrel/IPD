// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7157
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7157
{
  internal static string ssp_imclient_7158()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 1)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[4] = (byte) 205;
      numArray2[10] = (byte) 67;
      numArray2[13] = (byte) 5;
      numArray2[3] = (byte) 129;
      numArray2[14] = (byte) 131;
      numArray2[2] = (byte) 51;
      numArray2[7] = (byte) 106;
      numArray2[11] = (byte) 205;
      numArray2[8] = (byte) 252;
      numArray2[9] = (byte) 79;
      numArray2[1] = (byte) 79;
      numArray2[5] = (byte) 63 /*0x3F*/;
      numArray2[12] = (byte) 88;
      numArray2[6] = (byte) 181;
      numArray2[0] = (byte) 98;
      numArray2[15] = (byte) 88;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 62,
        (byte) 96 /*0x60*/,
        (byte) 32 /*0x20*/,
        (byte) 77,
        (byte) 132,
        (byte) 66,
        (byte) 20,
        (byte) 54,
        (byte) 128 /*0x80*/,
        (byte) 236,
        (byte) 31 /*0x1F*/,
        (byte) 183,
        (byte) 129,
        (byte) 127 /*0x7F*/,
        (byte) 206,
        (byte) 143
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[2] = (byte) 217;
    numArray5[0] = (byte) 53;
    numArray5[9] = (byte) 102;
    numArray5[3] = (byte) 17;
    numArray5[1] = (byte) 55;
    numArray5[15] = (byte) 242;
    numArray5[6] = (byte) 220;
    numArray5[7] = (byte) 69;
    numArray5[4] = (byte) 130;
    numArray5[13] = (byte) 108;
    numArray5[10] = (byte) 149;
    numArray5[8] = (byte) 172;
    numArray5[12] = (byte) 182;
    numArray5[11] = (byte) 0;
    numArray5[14] = (byte) 18;
    numArray5[5] = (byte) 212;
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[11] = (byte) 24;
    numArray6[9] = (byte) 39;
    numArray6[2] = (byte) 142;
    numArray6[1] = (byte) 33;
    numArray6[4] = (byte) 242;
    numArray6[5] = (byte) 215;
    numArray6[6] = (byte) 3;
    numArray6[7] = (byte) 113;
    numArray6[8] = (byte) 201;
    numArray6[15] = (byte) 253;
    numArray6[10] = (byte) 156;
    numArray6[0] = (byte) 0;
    numArray6[13] = (byte) 61;
    numArray6[14] = (byte) 100;
    numArray6[12] = (byte) 17;
    numArray6[3] = (byte) 114;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
