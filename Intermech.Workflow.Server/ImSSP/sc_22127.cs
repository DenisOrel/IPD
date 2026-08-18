// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22127
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_22127
{
  internal static int ssp_workflow_server_22128(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[23] = (byte) 103;
    sourceArray1[1] = (byte) 71;
    sourceArray1[8] = (byte) 224 /*0xE0*/;
    sourceArray1[3] = (byte) 94;
    sourceArray1[2] = (byte) 238;
    sourceArray1[20] = (byte) 152;
    sourceArray1[10] = (byte) 47;
    sourceArray1[31 /*0x1F*/] = (byte) 173;
    sourceArray1[33] = (byte) 161;
    sourceArray1[9] = (byte) 139;
    sourceArray1[39] = (byte) 163;
    sourceArray1[11] = (byte) 86;
    sourceArray1[17] = (byte) 11;
    sourceArray1[41] = (byte) 74;
    sourceArray1[14] = (byte) 206;
    sourceArray1[7] = (byte) 6;
    sourceArray1[16 /*0x10*/] = (byte) 89;
    sourceArray1[15] = (byte) 248;
    sourceArray1[18] = (byte) 102;
    sourceArray1[46] = (byte) 19;
    sourceArray1[12] = (byte) 65;
    sourceArray1[19] = (byte) 83;
    sourceArray1[26] = (byte) 117;
    sourceArray1[5] = (byte) 18;
    sourceArray1[24] = (byte) 217;
    sourceArray1[21] = (byte) 201;
    sourceArray1[27] = (byte) 92;
    sourceArray1[6] = (byte) 216;
    sourceArray1[28] = (byte) 254;
    sourceArray1[45] = (byte) 52;
    sourceArray1[34] = (byte) 149;
    sourceArray1[25] = (byte) 162;
    sourceArray1[30] = (byte) 245;
    sourceArray1[22] = (byte) 76;
    sourceArray1[36] = (byte) 237;
    sourceArray1[32 /*0x20*/] = (byte) 75;
    sourceArray1[0] = (byte) 92;
    sourceArray1[37] = (byte) 51;
    sourceArray1[38] = (byte) 80 /*0x50*/;
    sourceArray1[35] = (byte) 92;
    sourceArray1[4] = (byte) 52;
    sourceArray1[29] = (byte) 51;
    sourceArray1[42] = (byte) 62;
    sourceArray1[40] = (byte) 205;
    sourceArray1[44] = (byte) 224 /*0xE0*/;
    sourceArray1[13] = (byte) 95;
    sourceArray1[43] = (byte) 118;
    sourceArray1[47] = (byte) 114;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 126,
      (byte) 78,
      (byte) 152,
      (byte) 9,
      (byte) 92,
      (byte) 191,
      (byte) 39,
      (byte) 139,
      (byte) 219,
      (byte) 17,
      (byte) 67,
      (byte) 228,
      (byte) 43,
      (byte) 253,
      (byte) 7,
      (byte) 104,
      (byte) 243,
      (byte) 146,
      (byte) 26,
      (byte) 222,
      (byte) 130,
      (byte) 134,
      (byte) 134,
      (byte) 253,
      (byte) 164,
      (byte) 16 /*0x10*/,
      (byte) 3,
      (byte) 93,
      (byte) 247,
      (byte) 83,
      (byte) 5,
      byte.MaxValue,
      (byte) 223,
      (byte) 64 /*0x40*/,
      (byte) 165,
      (byte) 95,
      (byte) 185,
      (byte) 12,
      (byte) 101,
      (byte) 74,
      (byte) 39,
      (byte) 192 /*0xC0*/,
      (byte) 231,
      (byte) 247,
      (byte) 89,
      (byte) 211,
      (byte) 3,
      (byte) 207
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
