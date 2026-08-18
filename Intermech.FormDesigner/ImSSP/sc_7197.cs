// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7197
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7197
{
  internal static string ssp_imclient_7198()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 6)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 237,
        (byte) 219,
        (byte) 68,
        (byte) 92,
        (byte) 1,
        (byte) 99,
        (byte) 95,
        (byte) 62,
        (byte) 144 /*0x90*/,
        (byte) 56,
        (byte) 68,
        (byte) 155,
        (byte) 13,
        (byte) 127 /*0x7F*/,
        (byte) 236,
        (byte) 201
      };
      byte[] numArray3 = new byte[16 /*0x10*/]
      {
        (byte) 7,
        (byte) 39,
        (byte) 128 /*0x80*/,
        (byte) 11,
        (byte) 29,
        (byte) 191,
        (byte) 181,
        (byte) 41,
        (byte) 229,
        (byte) 18,
        (byte) 183,
        (byte) 176 /*0xB0*/,
        (byte) 210,
        (byte) 111,
        (byte) 166,
        (byte) 245
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
      (byte) 27,
      (byte) 95,
      (byte) 145,
      (byte) 79,
      (byte) 24,
      (byte) 143,
      (byte) 136,
      (byte) 233,
      (byte) 58,
      (byte) 197,
      (byte) 230,
      (byte) 171,
      (byte) 229,
      (byte) 148,
      (byte) 148,
      (byte) 22
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[6] = (byte) 136;
    numArray6[5] = (byte) 219;
    numArray6[2] = (byte) 191;
    numArray6[3] = (byte) 195;
    numArray6[4] = (byte) 159;
    numArray6[15] = (byte) 84;
    numArray6[0] = (byte) 13;
    numArray6[7] = (byte) 164;
    numArray6[8] = (byte) 107;
    numArray6[9] = (byte) 111;
    numArray6[14] = (byte) 65;
    numArray6[11] = (byte) 234;
    numArray6[12] = (byte) 254;
    numArray6[13] = (byte) 36;
    numArray6[10] = (byte) 9;
    numArray6[1] = (byte) 88;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
