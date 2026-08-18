// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7149
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7149
{
  internal static string ssp_imclient_7150()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 4)
    {
      byte[] numArray1 = new byte[16 /*0x10*/];
      byte[] numArray2 = new byte[16 /*0x10*/]
      {
        (byte) 82,
        (byte) 54,
        (byte) 122,
        (byte) 175,
        (byte) 250,
        (byte) 40,
        (byte) 118,
        (byte) 197,
        (byte) 33,
        (byte) 85,
        (byte) 87,
        (byte) 178,
        (byte) 171,
        (byte) 155,
        (byte) 177,
        (byte) 181
      };
      byte[] numArray3 = new byte[16 /*0x10*/];
      numArray3[11] = (byte) 242;
      numArray3[10] = (byte) 17;
      numArray3[9] = (byte) 99;
      numArray3[3] = (byte) 26;
      numArray3[4] = (byte) 151;
      numArray3[2] = (byte) 250;
      numArray3[6] = (byte) 124;
      numArray3[0] = (byte) 0;
      numArray3[13] = (byte) 235;
      numArray3[5] = (byte) 230;
      numArray3[1] = (byte) 89;
      numArray3[12] = (byte) 21;
      numArray3[8] = (byte) 8;
      numArray3[7] = (byte) 192 /*0xC0*/;
      numArray3[14] = (byte) 164;
      numArray3[15] = (byte) 207;
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 16 /*0x10*/);
      for (int index = 0; index < 16 /*0x10*/; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[16 /*0x10*/];
    byte[] numArray5 = new byte[16 /*0x10*/]
    {
      (byte) 190,
      (byte) 119,
      (byte) 32 /*0x20*/,
      (byte) 62,
      (byte) 237,
      (byte) 102,
      (byte) 69,
      (byte) 218,
      (byte) 35,
      (byte) 85,
      (byte) 97,
      (byte) 199,
      (byte) 118,
      byte.MaxValue,
      byte.MaxValue,
      (byte) 21
    };
    byte[] numArray6 = new byte[16 /*0x10*/];
    numArray6[15] = (byte) 233;
    numArray6[13] = (byte) 23;
    numArray6[0] = (byte) 122;
    numArray6[2] = (byte) 61;
    numArray6[14] = (byte) 195;
    numArray6[5] = (byte) 136;
    numArray6[6] = (byte) 31 /*0x1F*/;
    numArray6[4] = (byte) 173;
    numArray6[8] = (byte) 175;
    numArray6[9] = (byte) 20;
    numArray6[10] = (byte) 172;
    numArray6[7] = (byte) 246;
    numArray6[12] = (byte) 7;
    numArray6[3] = (byte) 205;
    numArray6[1] = (byte) 145;
    numArray6[11] = (byte) 29;
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 16 /*0x10*/);
    for (int index = 0; index < 16 /*0x10*/; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
