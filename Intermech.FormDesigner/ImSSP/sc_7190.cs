// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7190
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_7190
{
  internal static string ssp_imclient_7191()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[5];
      byte[] numArray2 = new byte[5]
      {
        (byte) 0,
        (byte) 0,
        (byte) 0,
        (byte) 184,
        (byte) 0
      };
      numArray2[0] = (byte) 148;
      numArray2[1] = (byte) 140;
      numArray2[2] = (byte) 70;
      numArray2[4] = (byte) 229;
      byte[] numArray3 = new byte[5]
      {
        (byte) 153,
        (byte) 203,
        (byte) 214,
        (byte) 16 /*0x10*/,
        (byte) 127 /*0x7F*/
      };
      key.Query(true, 348, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 5);
      for (int index = 0; index < 5; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[5];
    byte[] numArray5 = new byte[5]
    {
      (byte) 156,
      (byte) 0,
      (byte) 0,
      (byte) 216,
      (byte) 0
    };
    numArray5[2] = (byte) 15;
    numArray5[1] = (byte) 78;
    numArray5[4] = (byte) 59;
    byte[] numArray6 = new byte[5]
    {
      (byte) 64 /*0x40*/,
      (byte) 250,
      (byte) 107,
      (byte) 231,
      (byte) 116
    };
    key.Query(true, 348, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 5);
    for (int index = 0; index < 5; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
