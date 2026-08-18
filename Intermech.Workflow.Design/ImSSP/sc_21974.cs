// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21974
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21974
{
  internal static int ssp_workflow_21975(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[37] = (byte) 100;
    sourceArray1[1] = (byte) 254;
    sourceArray1[2] = (byte) 174;
    sourceArray1[19] = (byte) 241;
    sourceArray1[4] = (byte) 160 /*0xA0*/;
    sourceArray1[43] = (byte) 198;
    sourceArray1[21] = (byte) 31 /*0x1F*/;
    sourceArray1[23] = (byte) 90;
    sourceArray1[8] = (byte) 72;
    sourceArray1[9] = (byte) 151;
    sourceArray1[10] = (byte) 141;
    sourceArray1[11] = (byte) 55;
    sourceArray1[42] = (byte) 15;
    sourceArray1[27] = (byte) 228;
    sourceArray1[44] = (byte) 52;
    sourceArray1[15] = (byte) 239;
    sourceArray1[7] = (byte) 22;
    sourceArray1[13] = (byte) 197;
    sourceArray1[18] = (byte) 144 /*0x90*/;
    sourceArray1[6] = (byte) 213;
    sourceArray1[20] = (byte) 3;
    sourceArray1[22] = (byte) 159;
    sourceArray1[41] = (byte) 137;
    sourceArray1[28] = (byte) 183;
    sourceArray1[24] = (byte) 88;
    sourceArray1[3] = (byte) 51;
    sourceArray1[26] = (byte) 200;
    sourceArray1[17] = (byte) 39;
    sourceArray1[14] = (byte) 191;
    sourceArray1[47] = (byte) 63 /*0x3F*/;
    sourceArray1[30] = (byte) 116;
    sourceArray1[31 /*0x1F*/] = (byte) 101;
    sourceArray1[40] = (byte) 106;
    sourceArray1[36] = (byte) 158;
    sourceArray1[34] = (byte) 49;
    sourceArray1[35] = (byte) 14;
    sourceArray1[32 /*0x20*/] = (byte) 76;
    sourceArray1[16 /*0x10*/] = (byte) 96 /*0x60*/;
    sourceArray1[38] = (byte) 3;
    sourceArray1[39] = (byte) 123;
    sourceArray1[12] = (byte) 197;
    sourceArray1[45] = (byte) 193;
    sourceArray1[25] = (byte) 51;
    sourceArray1[0] = (byte) 96 /*0x60*/;
    sourceArray1[5] = (byte) 52;
    sourceArray1[33] = (byte) 134;
    sourceArray1[46] = (byte) 111;
    sourceArray1[29] = (byte) 15;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 250,
      (byte) 212,
      (byte) 112 /*0x70*/,
      (byte) 190,
      (byte) 211,
      (byte) 95,
      (byte) 33,
      (byte) 216,
      (byte) 135,
      (byte) 145,
      (byte) 103,
      (byte) 205,
      (byte) 86,
      (byte) 11,
      (byte) 82,
      (byte) 90,
      (byte) 133,
      (byte) 63 /*0x3F*/,
      (byte) 169,
      (byte) 109,
      (byte) 171,
      (byte) 96 /*0x60*/,
      (byte) 200,
      (byte) 95,
      (byte) 132,
      (byte) 228,
      (byte) 60,
      (byte) 71,
      (byte) 191,
      (byte) 253,
      (byte) 106,
      (byte) 103,
      (byte) 96 /*0x60*/,
      (byte) 218,
      (byte) 207,
      (byte) 120,
      (byte) 41,
      (byte) 169,
      (byte) 121,
      (byte) 0,
      (byte) 110,
      (byte) 89,
      (byte) 15,
      (byte) 5,
      (byte) 60,
      (byte) 106,
      (byte) 183,
      (byte) 149
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
