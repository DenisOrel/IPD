// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21836
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21836
{
  internal static string ssp_workflow_21837()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 5)
    {
      byte[] numArray1 = new byte[11];
      byte[] numArray2 = new byte[11];
      numArray2[1] = (byte) 225;
      numArray2[2] = (byte) 62;
      numArray2[6] = (byte) 126;
      numArray2[5] = (byte) 154;
      numArray2[4] = (byte) 176 /*0xB0*/;
      numArray2[3] = (byte) 0;
      numArray2[9] = (byte) 54;
      numArray2[7] = (byte) 244;
      numArray2[8] = (byte) 192 /*0xC0*/;
      numArray2[0] = (byte) 184;
      numArray2[10] = (byte) 32 /*0x20*/;
      byte[] numArray3 = new byte[11]
      {
        (byte) 178,
        (byte) 119,
        (byte) 104,
        (byte) 51,
        (byte) 32 /*0x20*/,
        (byte) 152,
        (byte) 74,
        (byte) 134,
        (byte) 151,
        (byte) 151,
        (byte) 214
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 11);
      for (int index = 0; index < 11; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[11];
    byte[] numArray5 = new byte[11]
    {
      (byte) 206,
      (byte) 127 /*0x7F*/,
      (byte) 224 /*0xE0*/,
      (byte) 227,
      (byte) 178,
      (byte) 254,
      (byte) 90,
      (byte) 145,
      (byte) 102,
      (byte) 213,
      (byte) 130
    };
    byte[] numArray6 = new byte[11];
    numArray6[4] = (byte) 125;
    numArray6[1] = (byte) 132;
    numArray6[2] = (byte) 212;
    numArray6[10] = (byte) 103;
    numArray6[5] = (byte) 41;
    numArray6[6] = (byte) 80 /*0x50*/;
    numArray6[3] = (byte) 230;
    numArray6[7] = (byte) 182;
    numArray6[0] = (byte) 217;
    numArray6[9] = (byte) 176 /*0xB0*/;
    numArray6[8] = (byte) 228;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 11);
    for (int index = 0; index < 11; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
