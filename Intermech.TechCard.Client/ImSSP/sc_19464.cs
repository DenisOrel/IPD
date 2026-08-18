// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19464
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19464
{
  internal static int ssp_techcard_19465(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[13] = (byte) 34;
    sourceArray1[32 /*0x20*/] = (byte) 146;
    sourceArray1[2] = (byte) 83;
    sourceArray1[3] = (byte) 26;
    sourceArray1[4] = (byte) 15;
    sourceArray1[5] = (byte) 101;
    sourceArray1[6] = (byte) 62;
    sourceArray1[29] = (byte) 204;
    sourceArray1[8] = (byte) 33;
    sourceArray1[26] = (byte) 189;
    sourceArray1[15] = (byte) 71;
    sourceArray1[25] = (byte) 92;
    sourceArray1[12] = (byte) 84;
    sourceArray1[42] = (byte) 95;
    sourceArray1[9] = (byte) 30;
    sourceArray1[16 /*0x10*/] = (byte) 173;
    sourceArray1[40] = (byte) 183;
    sourceArray1[24] = (byte) 213;
    sourceArray1[10] = (byte) 150;
    sourceArray1[19] = (byte) 81;
    sourceArray1[20] = (byte) 57;
    sourceArray1[21] = (byte) 77;
    sourceArray1[22] = (byte) 23;
    sourceArray1[23] = (byte) 248;
    sourceArray1[7] = (byte) 210;
    sourceArray1[39] = (byte) 122;
    sourceArray1[17] = (byte) 84;
    sourceArray1[27] = (byte) 238;
    sourceArray1[11] = (byte) 65;
    sourceArray1[43] = (byte) 168;
    sourceArray1[30] = (byte) 90;
    sourceArray1[35] = (byte) 174;
    sourceArray1[28] = (byte) 54;
    sourceArray1[33] = (byte) 252;
    sourceArray1[34] = (byte) 41;
    sourceArray1[31 /*0x1F*/] = (byte) 11;
    sourceArray1[36] = (byte) 88;
    sourceArray1[37] = (byte) 114;
    sourceArray1[38] = (byte) 23;
    sourceArray1[18] = (byte) 207;
    sourceArray1[0] = (byte) 105;
    sourceArray1[41] = (byte) 251;
    sourceArray1[14] = (byte) 163;
    sourceArray1[47] = (byte) 152;
    sourceArray1[44] = (byte) 243;
    sourceArray1[45] = (byte) 175;
    sourceArray1[46] = (byte) 198;
    sourceArray1[1] = (byte) 23;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[19] = (byte) 175;
    sourceArray2[0] = (byte) 192 /*0xC0*/;
    sourceArray2[33] = (byte) 92;
    sourceArray2[3] = (byte) 202;
    sourceArray2[35] = (byte) 52;
    sourceArray2[25] = (byte) 47;
    sourceArray2[10] = (byte) 237;
    sourceArray2[2] = (byte) 228;
    sourceArray2[32 /*0x20*/] = (byte) 68;
    sourceArray2[9] = (byte) 197;
    sourceArray2[41] = (byte) 100;
    sourceArray2[11] = (byte) 57;
    sourceArray2[12] = (byte) 170;
    sourceArray2[13] = (byte) 205;
    sourceArray2[14] = (byte) 158;
    sourceArray2[26] = (byte) 27;
    sourceArray2[16 /*0x10*/] = (byte) 165;
    sourceArray2[17] = (byte) 107;
    sourceArray2[28] = (byte) 31 /*0x1F*/;
    sourceArray2[39] = (byte) 231;
    sourceArray2[20] = (byte) 67;
    sourceArray2[21] = (byte) 169;
    sourceArray2[31 /*0x1F*/] = (byte) 154;
    sourceArray2[8] = (byte) 60;
    sourceArray2[1] = (byte) 74;
    sourceArray2[15] = (byte) 92;
    sourceArray2[6] = (byte) 193;
    sourceArray2[27] = (byte) 3;
    sourceArray2[5] = (byte) 254;
    sourceArray2[29] = (byte) 225;
    sourceArray2[30] = (byte) 230;
    sourceArray2[7] = (byte) 68;
    sourceArray2[46] = (byte) 40;
    sourceArray2[24] = (byte) 50;
    sourceArray2[44] = (byte) 84;
    sourceArray2[23] = (byte) 82;
    sourceArray2[36] = (byte) 16 /*0x10*/;
    sourceArray2[43] = (byte) 171;
    sourceArray2[38] = (byte) 79;
    sourceArray2[37] = (byte) 104;
    sourceArray2[40] = (byte) 152;
    sourceArray2[47] = (byte) 49;
    sourceArray2[4] = (byte) 239;
    sourceArray2[42] = (byte) 140;
    sourceArray2[18] = (byte) 193;
    sourceArray2[45] = (byte) 101;
    sourceArray2[22] = (byte) 207;
    sourceArray2[34] = (byte) 65;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
