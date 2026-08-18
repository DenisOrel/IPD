// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7201
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7201
{
  internal static string ssp_imclient_7202()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/];
      numArray2[15] = (byte) 206;
      numArray2[1] = (byte) 88;
      numArray2[9] = (byte) 53;
      numArray2[3] = (byte) 119;
      numArray2[4] = (byte) 85;
      numArray2[5] = (byte) 4;
      numArray2[7] = (byte) 89;
      numArray2[10] = (byte) 230;
      numArray2[8] = (byte) 124;
      numArray2[0] = (byte) 55;
      numArray2[11] = (byte) 193;
      numArray2[13] = (byte) 234;
      numArray2[2] = (byte) 212;
      numArray2[12] = (byte) 141;
      numArray2[6] = (byte) 251;
      numArray2[14] = (byte) 219;
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 77,
        (byte) 90,
        (byte) 101,
        (byte) 15,
        byte.MaxValue,
        (byte) 94,
        (byte) 102,
        (byte) 114,
        (byte) 47,
        (byte) 242,
        (byte) 176 /*0xB0*/,
        (byte) 59,
        (byte) 176 /*0xB0*/,
        (byte) 114,
        (byte) 196,
        (byte) 209
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/];
    numArray5[0] = (byte) 43;
    numArray5[6] = (byte) 57;
    numArray5[2] = (byte) 65;
    numArray5[15] = (byte) 82;
    numArray5[13] = (byte) 172;
    numArray5[5] = (byte) 20;
    numArray5[1] = (byte) 99;
    numArray5[7] = (byte) 195;
    numArray5[8] = (byte) 63 /*0x3F*/;
    numArray5[9] = (byte) 137;
    numArray5[3] = (byte) 101;
    numArray5[10] = (byte) 193;
    numArray5[4] = (byte) 86;
    numArray5[11] = (byte) 82;
    numArray5[12] = (byte) 80 /*0x50*/;
    numArray5[14] = (byte) 69;
    byte[] numArray6 = new byte[16 /*0x10*/]
    {
      (byte) 237,
      (byte) 125,
      (byte) 155,
      (byte) 59,
      (byte) 230,
      (byte) 65,
      (byte) 141,
      (byte) 24,
      (byte) 202,
      (byte) 14,
      (byte) 152,
      (byte) 0,
      (byte) 193,
      (byte) 108,
      (byte) 204,
      (byte) 126
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
