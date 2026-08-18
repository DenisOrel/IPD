// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21864
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21864
{
  internal static string ssp_workflow_21865()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 11)
    {
      byte[] numArray1 = new byte[12];
      byte[] numArray2 = new byte[12]
      {
        (byte) 26,
        (byte) 83,
        (byte) 170,
        (byte) 53,
        (byte) 80 /*0x50*/,
        (byte) 165,
        (byte) 110,
        (byte) 105,
        (byte) 225,
        (byte) 27,
        (byte) 158,
        (byte) 16 /*0x10*/
      };
      byte[] numArray3 = new byte[12]
      {
        (byte) 29,
        (byte) 194,
        (byte) 127 /*0x7F*/,
        (byte) 233,
        (byte) 150,
        (byte) 88,
        (byte) 95,
        (byte) 30,
        (byte) 243,
        (byte) 101,
        (byte) 130,
        (byte) 105
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 12);
      for (int index = 0; index < 12; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[12];
    byte[] numArray5 = new byte[12]
    {
      (byte) 182,
      (byte) 33,
      (byte) 72,
      (byte) 234,
      (byte) 116,
      (byte) 54,
      (byte) 225,
      (byte) 141,
      (byte) 100,
      (byte) 38,
      (byte) 243,
      (byte) 143
    };
    byte[] numArray6 = new byte[12]
    {
      (byte) 145,
      (byte) 167,
      (byte) 86,
      (byte) 239,
      (byte) 125,
      (byte) 24,
      (byte) 93,
      (byte) 73,
      (byte) 19,
      (byte) 99,
      (byte) 203,
      (byte) 180
    };
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 12);
    for (int index = 0; index < 12; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
