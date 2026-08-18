// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21824
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_21824
{
  internal static string ssp_workflow_21825()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[18];
      byte[] numArray2 = new byte[18]
      {
        (byte) 175,
        (byte) 112 /*0x70*/,
        (byte) 165,
        (byte) 58,
        (byte) 96 /*0x60*/,
        (byte) 117,
        (byte) 86,
        (byte) 0,
        (byte) 159,
        (byte) 194,
        (byte) 233,
        (byte) 20,
        (byte) 82,
        (byte) 187,
        (byte) 155,
        (byte) 141,
        (byte) 22,
        (byte) 98
      };
      byte[] numArray3 = new byte[18]
      {
        (byte) 154,
        (byte) 118,
        (byte) 144 /*0x90*/,
        (byte) 157,
        (byte) 80 /*0x50*/,
        (byte) 96 /*0x60*/,
        (byte) 173,
        (byte) 190,
        (byte) 163,
        (byte) 140,
        (byte) 46,
        (byte) 212,
        (byte) 212,
        (byte) 202,
        (byte) 147,
        (byte) 19,
        (byte) 119,
        (byte) 201
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 18);
      for (int index = 0; index < 18; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[18];
    byte[] numArray5 = new byte[18]
    {
      (byte) 204,
      (byte) 72,
      (byte) 76,
      (byte) 158,
      (byte) 215,
      (byte) 26,
      (byte) 157,
      (byte) 30,
      (byte) 96 /*0x60*/,
      (byte) 194,
      (byte) 57,
      (byte) 121,
      (byte) 212,
      (byte) 199,
      (byte) 218,
      (byte) 87,
      (byte) 246,
      (byte) 26
    };
    byte[] numArray6 = new byte[18];
    numArray6[2] = (byte) 219;
    numArray6[1] = (byte) 127 /*0x7F*/;
    numArray6[5] = (byte) 90;
    numArray6[12] = (byte) 146;
    numArray6[4] = (byte) 195;
    numArray6[13] = (byte) 66;
    numArray6[6] = (byte) 173;
    numArray6[7] = (byte) 205;
    numArray6[8] = (byte) 113;
    numArray6[0] = (byte) 177;
    numArray6[17] = (byte) 5;
    numArray6[11] = (byte) 23;
    numArray6[10] = (byte) 29;
    numArray6[15] = (byte) 7;
    numArray6[9] = (byte) 20;
    numArray6[14] = (byte) 167;
    numArray6[16 /*0x10*/] = (byte) 219;
    numArray6[3] = (byte) 21;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 18);
    for (int index = 0; index < 18; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
