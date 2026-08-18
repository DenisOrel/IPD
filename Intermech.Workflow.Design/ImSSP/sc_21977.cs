// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21977
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21977
{
  internal static int ssp_workflow_21978(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[29] = (byte) 78;
    sourceArray1[1] = (byte) 170;
    sourceArray1[39] = (byte) 36;
    sourceArray1[33] = (byte) 192 /*0xC0*/;
    sourceArray1[21] = (byte) 222;
    sourceArray1[36] = (byte) 102;
    sourceArray1[27] = (byte) 3;
    sourceArray1[7] = (byte) 140;
    sourceArray1[8] = (byte) 172;
    sourceArray1[32 /*0x20*/] = (byte) 25;
    sourceArray1[3] = (byte) 84;
    sourceArray1[11] = (byte) 46;
    sourceArray1[4] = (byte) 33;
    sourceArray1[45] = (byte) 28;
    sourceArray1[34] = (byte) 148;
    sourceArray1[15] = (byte) 164;
    sourceArray1[16 /*0x10*/] = (byte) 76;
    sourceArray1[17] = (byte) 69;
    sourceArray1[19] = (byte) 36;
    sourceArray1[40] = (byte) 183;
    sourceArray1[20] = (byte) 176 /*0xB0*/;
    sourceArray1[6] = (byte) 76;
    sourceArray1[22] = (byte) 164;
    sourceArray1[24] = (byte) 6;
    sourceArray1[14] = (byte) 225;
    sourceArray1[25] = (byte) 113;
    sourceArray1[26] = (byte) 142;
    sourceArray1[12] = (byte) 67;
    sourceArray1[43] = (byte) 190;
    sourceArray1[46] = (byte) 69;
    sourceArray1[13] = (byte) 169;
    sourceArray1[31 /*0x1F*/] = (byte) 52;
    sourceArray1[10] = (byte) 134;
    sourceArray1[23] = (byte) 2;
    sourceArray1[0] = (byte) 47;
    sourceArray1[35] = (byte) 222;
    sourceArray1[41] = (byte) 173;
    sourceArray1[28] = (byte) 13;
    sourceArray1[38] = (byte) 209;
    sourceArray1[5] = (byte) 150;
    sourceArray1[2] = (byte) 230;
    sourceArray1[9] = (byte) 49;
    sourceArray1[42] = (byte) 179;
    sourceArray1[37] = (byte) 190;
    sourceArray1[44] = (byte) 40;
    sourceArray1[18] = (byte) 105;
    sourceArray1[30] = (byte) 57;
    sourceArray1[47] = (byte) 236;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 209;
    sourceArray2[38] = (byte) 95;
    sourceArray2[9] = (byte) 150;
    sourceArray2[43] = (byte) 87;
    sourceArray2[33] = (byte) 183;
    sourceArray2[19] = (byte) 232;
    sourceArray2[25] = (byte) 178;
    sourceArray2[7] = (byte) 171;
    sourceArray2[8] = (byte) 41;
    sourceArray2[14] = (byte) 85;
    sourceArray2[10] = (byte) 249;
    sourceArray2[0] = (byte) 115;
    sourceArray2[12] = (byte) 212;
    sourceArray2[26] = (byte) 7;
    sourceArray2[42] = (byte) 164;
    sourceArray2[47] = (byte) 139;
    sourceArray2[16 /*0x10*/] = (byte) 102;
    sourceArray2[17] = (byte) 196;
    sourceArray2[36] = (byte) 7;
    sourceArray2[2] = (byte) 132;
    sourceArray2[20] = (byte) 140;
    sourceArray2[13] = (byte) 112 /*0x70*/;
    sourceArray2[46] = (byte) 130;
    sourceArray2[23] = (byte) 226;
    sourceArray2[24] = (byte) 61;
    sourceArray2[15] = (byte) 175;
    sourceArray2[34] = (byte) 196;
    sourceArray2[27] = (byte) 75;
    sourceArray2[4] = (byte) 148;
    sourceArray2[6] = (byte) 236;
    sourceArray2[30] = (byte) 140;
    sourceArray2[31 /*0x1F*/] = (byte) 47;
    sourceArray2[29] = (byte) 177;
    sourceArray2[40] = (byte) 205;
    sourceArray2[28] = (byte) 21;
    sourceArray2[35] = (byte) 73;
    sourceArray2[41] = (byte) 215;
    sourceArray2[37] = (byte) 85;
    sourceArray2[1] = (byte) 203;
    sourceArray2[39] = (byte) 164;
    sourceArray2[18] = (byte) 105;
    sourceArray2[3] = (byte) 161;
    sourceArray2[11] = (byte) 112 /*0x70*/;
    sourceArray2[22] = (byte) 228;
    sourceArray2[44] = (byte) 45;
    sourceArray2[45] = (byte) 174;
    sourceArray2[21] = (byte) 110;
    sourceArray2[32 /*0x20*/] = (byte) 144 /*0x90*/;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
