// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21930
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21930
{
  internal static int ssp_workflow_21931(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[42] = (byte) 150;
    sourceArray1[19] = (byte) 39;
    sourceArray1[0] = (byte) 179;
    sourceArray1[31 /*0x1F*/] = (byte) 49;
    sourceArray1[27] = (byte) 49;
    sourceArray1[1] = (byte) 94;
    sourceArray1[43] = (byte) 95;
    sourceArray1[7] = (byte) 74;
    sourceArray1[8] = (byte) 110;
    sourceArray1[45] = (byte) 80 /*0x50*/;
    sourceArray1[3] = (byte) 103;
    sourceArray1[5] = (byte) 86;
    sourceArray1[12] = (byte) 64 /*0x40*/;
    sourceArray1[28] = (byte) 107;
    sourceArray1[13] = (byte) 201;
    sourceArray1[24] = (byte) 17;
    sourceArray1[16 /*0x10*/] = (byte) 65;
    sourceArray1[17] = (byte) 33;
    sourceArray1[18] = (byte) 116;
    sourceArray1[23] = (byte) 129;
    sourceArray1[20] = (byte) 61;
    sourceArray1[21] = (byte) 217;
    sourceArray1[9] = (byte) 98;
    sourceArray1[47] = (byte) 43;
    sourceArray1[14] = (byte) 211;
    sourceArray1[25] = (byte) 203;
    sourceArray1[26] = (byte) 35;
    sourceArray1[4] = (byte) 200;
    sourceArray1[2] = (byte) 10;
    sourceArray1[29] = (byte) 10;
    sourceArray1[39] = (byte) 72;
    sourceArray1[38] = (byte) 113;
    sourceArray1[32 /*0x20*/] = (byte) 45;
    sourceArray1[36] = (byte) 36;
    sourceArray1[34] = (byte) 119;
    sourceArray1[35] = (byte) 122;
    sourceArray1[22] = (byte) 220;
    sourceArray1[37] = (byte) 174;
    sourceArray1[33] = (byte) 69;
    sourceArray1[10] = (byte) 109;
    sourceArray1[40] = (byte) 137;
    sourceArray1[41] = (byte) 27;
    sourceArray1[30] = (byte) 6;
    sourceArray1[44] = (byte) 102;
    sourceArray1[11] = (byte) 78;
    sourceArray1[6] = (byte) 212;
    sourceArray1[46] = (byte) 175;
    sourceArray1[15] = (byte) 62;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[46] = (byte) 8;
    sourceArray2[28] = (byte) 33;
    sourceArray2[6] = (byte) 119;
    sourceArray2[8] = (byte) 67;
    sourceArray2[17] = (byte) 8;
    sourceArray2[16 /*0x10*/] = (byte) 8;
    sourceArray2[47] = (byte) 145;
    sourceArray2[29] = (byte) 167;
    sourceArray2[2] = (byte) 173;
    sourceArray2[9] = (byte) 237;
    sourceArray2[10] = (byte) 74;
    sourceArray2[1] = (byte) 63 /*0x3F*/;
    sourceArray2[12] = (byte) 5;
    sourceArray2[39] = (byte) 213;
    sourceArray2[14] = (byte) 182;
    sourceArray2[15] = (byte) 234;
    sourceArray2[5] = (byte) 93;
    sourceArray2[22] = (byte) 235;
    sourceArray2[18] = (byte) 61;
    sourceArray2[19] = (byte) 87;
    sourceArray2[20] = (byte) 180;
    sourceArray2[43] = (byte) 17;
    sourceArray2[0] = (byte) 153;
    sourceArray2[23] = (byte) 131;
    sourceArray2[7] = (byte) 114;
    sourceArray2[25] = (byte) 78;
    sourceArray2[24] = (byte) 72;
    sourceArray2[31 /*0x1F*/] = (byte) 239;
    sourceArray2[3] = (byte) 105;
    sourceArray2[38] = (byte) 104;
    sourceArray2[30] = (byte) 86;
    sourceArray2[11] = (byte) 169;
    sourceArray2[27] = (byte) 20;
    sourceArray2[33] = (byte) 55;
    sourceArray2[34] = (byte) 121;
    sourceArray2[35] = (byte) 53;
    sourceArray2[36] = (byte) 105;
    sourceArray2[37] = (byte) 119;
    sourceArray2[26] = (byte) 133;
    sourceArray2[4] = (byte) 220;
    sourceArray2[13] = (byte) 80 /*0x50*/;
    sourceArray2[32 /*0x20*/] = (byte) 86;
    sourceArray2[42] = (byte) 33;
    sourceArray2[40] = (byte) 65;
    sourceArray2[21] = (byte) 8;
    sourceArray2[45] = (byte) 93;
    sourceArray2[44] = (byte) 136;
    sourceArray2[41] = (byte) 126;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
