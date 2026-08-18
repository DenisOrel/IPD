// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21678
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21678
{
  internal static int ssp_workflow_21679(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[16 /*0x10*/] = (byte) 7;
    sourceArray1[15] = (byte) 28;
    sourceArray1[2] = (byte) 143;
    sourceArray1[9] = (byte) 9;
    sourceArray1[4] = (byte) 58;
    sourceArray1[5] = (byte) 97;
    sourceArray1[41] = (byte) 187;
    sourceArray1[34] = (byte) 191;
    sourceArray1[10] = (byte) 87;
    sourceArray1[47] = (byte) 4;
    sourceArray1[42] = (byte) 66;
    sourceArray1[46] = (byte) 234;
    sourceArray1[40] = (byte) 184;
    sourceArray1[13] = (byte) 230;
    sourceArray1[44] = (byte) 51;
    sourceArray1[24] = (byte) 36;
    sourceArray1[20] = (byte) 109;
    sourceArray1[7] = (byte) 226;
    sourceArray1[18] = (byte) 168;
    sourceArray1[11] = (byte) 14;
    sourceArray1[17] = (byte) 6;
    sourceArray1[21] = (byte) 188;
    sourceArray1[22] = (byte) 224 /*0xE0*/;
    sourceArray1[23] = (byte) 126;
    sourceArray1[25] = (byte) 162;
    sourceArray1[29] = (byte) 69;
    sourceArray1[26] = (byte) 124;
    sourceArray1[27] = (byte) 200;
    sourceArray1[28] = (byte) 93;
    sourceArray1[1] = (byte) 79;
    sourceArray1[30] = (byte) 187;
    sourceArray1[31 /*0x1F*/] = (byte) 158;
    sourceArray1[35] = (byte) 230;
    sourceArray1[33] = (byte) 145;
    sourceArray1[3] = (byte) 254;
    sourceArray1[6] = (byte) 33;
    sourceArray1[36] = (byte) 185;
    sourceArray1[12] = (byte) 204;
    sourceArray1[38] = (byte) 0;
    sourceArray1[39] = (byte) 128 /*0x80*/;
    sourceArray1[14] = (byte) 213;
    sourceArray1[37] = (byte) 230;
    sourceArray1[32 /*0x20*/] = (byte) 46;
    sourceArray1[43] = (byte) 159;
    sourceArray1[45] = (byte) 44;
    sourceArray1[0] = (byte) 90;
    sourceArray1[8] = (byte) 112 /*0x70*/;
    sourceArray1[19] = (byte) 176 /*0xB0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 213,
      (byte) 161,
      (byte) 132,
      (byte) 204,
      (byte) 250,
      (byte) 103,
      (byte) 207,
      (byte) 161,
      (byte) 92,
      (byte) 143,
      (byte) 167,
      (byte) 83,
      (byte) 211,
      (byte) 226,
      (byte) 126,
      (byte) 241,
      (byte) 162,
      (byte) 37,
      (byte) 136,
      (byte) 253,
      (byte) 32 /*0x20*/,
      (byte) 211,
      (byte) 12,
      (byte) 178,
      (byte) 201,
      (byte) 61,
      (byte) 157,
      (byte) 202,
      (byte) 240 /*0xF0*/,
      (byte) 149,
      (byte) 167,
      (byte) 254,
      (byte) 158,
      (byte) 22,
      (byte) 169,
      (byte) 242,
      (byte) 80 /*0x50*/,
      (byte) 193,
      (byte) 248,
      (byte) 53,
      (byte) 219,
      (byte) 30,
      (byte) 173,
      (byte) 197,
      (byte) 6,
      (byte) 246,
      (byte) 2,
      (byte) 127 /*0x7F*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
