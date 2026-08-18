// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_17065
// Assembly: Intermech.Pdm.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EC8EF964-D01E-4AAA-8100-7A99DC670202
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Pdm.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_17065
{
  internal static int ssp_pdm_server_17066(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 95;
    sourceArray1[3] = (byte) 207;
    sourceArray1[37] = (byte) 50;
    sourceArray1[22] = (byte) 209;
    sourceArray1[4] = (byte) 151;
    sourceArray1[24] = (byte) 162;
    sourceArray1[16 /*0x10*/] = (byte) 125;
    sourceArray1[7] = (byte) 225;
    sourceArray1[8] = (byte) 91;
    sourceArray1[31 /*0x1F*/] = (byte) 193;
    sourceArray1[10] = (byte) 79;
    sourceArray1[11] = (byte) 201;
    sourceArray1[13] = (byte) 217;
    sourceArray1[29] = (byte) 242;
    sourceArray1[14] = (byte) 71;
    sourceArray1[15] = (byte) 147;
    sourceArray1[41] = (byte) 91;
    sourceArray1[42] = (byte) 58;
    sourceArray1[18] = (byte) 60;
    sourceArray1[19] = (byte) 115;
    sourceArray1[20] = (byte) 226;
    sourceArray1[5] = (byte) 10;
    sourceArray1[47] = (byte) 96 /*0x60*/;
    sourceArray1[23] = (byte) 21;
    sourceArray1[45] = (byte) 158;
    sourceArray1[39] = (byte) 251;
    sourceArray1[26] = (byte) 64 /*0x40*/;
    sourceArray1[34] = (byte) 187;
    sourceArray1[28] = (byte) 107;
    sourceArray1[1] = (byte) 174;
    sourceArray1[40] = (byte) 18;
    sourceArray1[0] = (byte) 10;
    sourceArray1[32 /*0x20*/] = (byte) 71;
    sourceArray1[17] = (byte) 66;
    sourceArray1[9] = (byte) 57;
    sourceArray1[30] = (byte) 36;
    sourceArray1[35] = (byte) 90;
    sourceArray1[27] = (byte) 222;
    sourceArray1[38] = (byte) 233;
    sourceArray1[2] = (byte) 150;
    sourceArray1[33] = (byte) 178;
    sourceArray1[36] = (byte) 107;
    sourceArray1[12] = (byte) 118;
    sourceArray1[43] = (byte) 120;
    sourceArray1[44] = (byte) 57;
    sourceArray1[6] = (byte) 102;
    sourceArray1[46] = (byte) 77;
    sourceArray1[25] = (byte) 161;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[39] = (byte) 71;
    sourceArray2[1] = (byte) 176 /*0xB0*/;
    sourceArray2[17] = (byte) 100;
    sourceArray2[3] = (byte) 48 /*0x30*/;
    sourceArray2[29] = (byte) 165;
    sourceArray2[46] = (byte) 250;
    sourceArray2[36] = (byte) 181;
    sourceArray2[7] = (byte) 36;
    sourceArray2[5] = (byte) 105;
    sourceArray2[9] = (byte) 43;
    sourceArray2[14] = (byte) 23;
    sourceArray2[21] = (byte) 217;
    sourceArray2[30] = (byte) 107;
    sourceArray2[32 /*0x20*/] = (byte) 204;
    sourceArray2[8] = (byte) 149;
    sourceArray2[13] = (byte) 4;
    sourceArray2[16 /*0x10*/] = (byte) 142;
    sourceArray2[10] = (byte) 58;
    sourceArray2[38] = (byte) 164;
    sourceArray2[6] = (byte) 98;
    sourceArray2[18] = (byte) 0;
    sourceArray2[33] = (byte) 158;
    sourceArray2[34] = (byte) 52;
    sourceArray2[0] = byte.MaxValue;
    sourceArray2[22] = (byte) 198;
    sourceArray2[25] = (byte) 67;
    sourceArray2[4] = (byte) 183;
    sourceArray2[2] = (byte) 34;
    sourceArray2[35] = (byte) 140;
    sourceArray2[11] = (byte) 209;
    sourceArray2[20] = (byte) 119;
    sourceArray2[31 /*0x1F*/] = (byte) 99;
    sourceArray2[19] = (byte) 74;
    sourceArray2[27] = (byte) 54;
    sourceArray2[44] = (byte) 31 /*0x1F*/;
    sourceArray2[24] = (byte) 39;
    sourceArray2[45] = (byte) 169;
    sourceArray2[37] = (byte) 78;
    sourceArray2[28] = (byte) 248;
    sourceArray2[15] = (byte) 35;
    sourceArray2[40] = (byte) 191;
    sourceArray2[41] = (byte) 72;
    sourceArray2[42] = (byte) 132;
    sourceArray2[43] = (byte) 73;
    sourceArray2[47] = (byte) 159;
    sourceArray2[12] = (byte) 66;
    sourceArray2[23] = (byte) 189;
    sourceArray2[26] = (byte) 38;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 350, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
