// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22034
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22034
{
  internal static string ssp_workflow_22035()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 2)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 19,
        (byte) 142,
        (byte) 111,
        (byte) 244,
        (byte) 8,
        (byte) 70,
        (byte) 66,
        (byte) 106,
        (byte) 72,
        (byte) 153,
        (byte) 111,
        (byte) 171,
        (byte) 150,
        (byte) 193,
        (byte) 222,
        (byte) 237,
        (byte) 23,
        (byte) 223
      };
      byte[] numArray3 = new byte[18];
      numArray3[1] = (byte) 151;
      numArray3[14] = (byte) 182;
      numArray3[7] = (byte) 73;
      numArray3[3] = (byte) 191;
      numArray3[4] = (byte) 18;
      numArray3[9] = (byte) 118;
      numArray3[0] = (byte) 98;
      numArray3[8] = (byte) 92;
      numArray3[6] = (byte) 67;
      numArray3[2] = (byte) 113;
      numArray3[10] = (byte) 108;
      numArray3[11] = (byte) 92;
      numArray3[12] = (byte) 151;
      numArray3[13] = (byte) 150;
      numArray3[5] = (byte) 107;
      numArray3[15] = (byte) 47;
      numArray3[16 /*0x10*/] = (byte) 186;
      numArray3[17] = (byte) 127 /*0x7F*/;
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 126,
      (byte) 90,
      (byte) 200,
      (byte) 212,
      (byte) 105,
      (byte) 105,
      (byte) 208 /*0xD0*/,
      (byte) 160 /*0xA0*/,
      (byte) 101,
      (byte) 94,
      (byte) 40,
      (byte) 244,
      (byte) 123,
      (byte) 73,
      (byte) 99,
      (byte) 112 /*0x70*/,
      (byte) 192 /*0xC0*/,
      (byte) 135
    };
    byte[] numArray6 = new byte[18]
    {
      (byte) 146,
      (byte) 209,
      (byte) 192 /*0xC0*/,
      (byte) 147,
      (byte) 102,
      (byte) 158,
      (byte) 88,
      (byte) 72,
      (byte) 208 /*0xD0*/,
      (byte) 201,
      (byte) 83,
      (byte) 162,
      (byte) 117,
      (byte) 251,
      (byte) 160 /*0xA0*/,
      (byte) 38,
      (byte) 38,
      (byte) 30
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
