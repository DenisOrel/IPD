// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21746
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21746
{
  internal static int ssp_workflow_21747(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[14] = (byte) 109;
    sourceArray1[19] = (byte) 68;
    sourceArray1[2] = (byte) 131;
    sourceArray1[10] = (byte) 46;
    sourceArray1[22] = (byte) 6;
    sourceArray1[35] = (byte) 155;
    sourceArray1[6] = (byte) 1;
    sourceArray1[28] = (byte) 152;
    sourceArray1[8] = (byte) 140;
    sourceArray1[9] = (byte) 36;
    sourceArray1[1] = (byte) 220;
    sourceArray1[11] = (byte) 197;
    sourceArray1[16 /*0x10*/] = (byte) 35;
    sourceArray1[13] = (byte) 222;
    sourceArray1[20] = (byte) 40;
    sourceArray1[15] = (byte) 2;
    sourceArray1[37] = (byte) 201;
    sourceArray1[46] = (byte) 44;
    sourceArray1[7] = (byte) 126;
    sourceArray1[32 /*0x20*/] = (byte) 209;
    sourceArray1[40] = (byte) 115;
    sourceArray1[38] = (byte) 241;
    sourceArray1[44] = (byte) 72;
    sourceArray1[0] = (byte) 175;
    sourceArray1[21] = (byte) 231;
    sourceArray1[25] = (byte) 20;
    sourceArray1[42] = (byte) 198;
    sourceArray1[45] = (byte) 226;
    sourceArray1[4] = (byte) 153;
    sourceArray1[29] = (byte) 124;
    sourceArray1[30] = (byte) 98;
    sourceArray1[43] = (byte) 253;
    sourceArray1[18] = (byte) 188;
    sourceArray1[33] = (byte) 173;
    sourceArray1[34] = (byte) 9;
    sourceArray1[41] = (byte) 57;
    sourceArray1[36] = (byte) 210;
    sourceArray1[26] = (byte) 156;
    sourceArray1[31 /*0x1F*/] = (byte) 238;
    sourceArray1[39] = (byte) 108;
    sourceArray1[17] = (byte) 160 /*0xA0*/;
    sourceArray1[3] = (byte) 152;
    sourceArray1[23] = (byte) 119;
    sourceArray1[27] = (byte) 140;
    sourceArray1[5] = (byte) 36;
    sourceArray1[24] = (byte) 185;
    sourceArray1[12] = (byte) 224 /*0xE0*/;
    sourceArray1[47] = (byte) 138;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[22] = (byte) 79;
    sourceArray2[17] = (byte) 252;
    sourceArray2[2] = (byte) 212;
    sourceArray2[23] = (byte) 42;
    sourceArray2[4] = (byte) 11;
    sourceArray2[0] = (byte) 88;
    sourceArray2[1] = (byte) 94;
    sourceArray2[7] = (byte) 61;
    sourceArray2[8] = (byte) 241;
    sourceArray2[5] = (byte) 47;
    sourceArray2[10] = (byte) 82;
    sourceArray2[11] = (byte) 242;
    sourceArray2[12] = (byte) 89;
    sourceArray2[20] = (byte) 148;
    sourceArray2[28] = (byte) 129;
    sourceArray2[15] = (byte) 1;
    sourceArray2[43] = (byte) 177;
    sourceArray2[41] = (byte) 139;
    sourceArray2[21] = (byte) 3;
    sourceArray2[19] = (byte) 212;
    sourceArray2[16 /*0x10*/] = (byte) 143;
    sourceArray2[13] = (byte) 211;
    sourceArray2[45] = (byte) 24;
    sourceArray2[32 /*0x20*/] = (byte) 119;
    sourceArray2[24] = (byte) 200;
    sourceArray2[6] = (byte) 34;
    sourceArray2[40] = (byte) 41;
    sourceArray2[27] = (byte) 91;
    sourceArray2[25] = (byte) 14;
    sourceArray2[9] = (byte) 241;
    sourceArray2[30] = (byte) 130;
    sourceArray2[31 /*0x1F*/] = (byte) 238;
    sourceArray2[37] = (byte) 12;
    sourceArray2[44] = (byte) 252;
    sourceArray2[34] = (byte) 61;
    sourceArray2[35] = (byte) 232;
    sourceArray2[36] = (byte) 8;
    sourceArray2[18] = (byte) 180;
    sourceArray2[26] = (byte) 205;
    sourceArray2[39] = (byte) 78;
    sourceArray2[29] = (byte) 12;
    sourceArray2[3] = (byte) 211;
    sourceArray2[42] = (byte) 27;
    sourceArray2[38] = (byte) 166;
    sourceArray2[33] = (byte) 25;
    sourceArray2[14] = (byte) 64 /*0x40*/;
    sourceArray2[46] = (byte) 118;
    sourceArray2[47] = (byte) 155;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_21748(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 110,
      (byte) 230,
      (byte) 169,
      (byte) 14,
      (byte) 99,
      (byte) 35,
      (byte) 151,
      (byte) 79,
      (byte) 32 /*0x20*/,
      (byte) 77,
      (byte) 193,
      (byte) 72,
      (byte) 133,
      (byte) 217,
      (byte) 143,
      (byte) 153,
      (byte) 58,
      byte.MaxValue,
      (byte) 182,
      (byte) 33,
      (byte) 172,
      (byte) 24,
      (byte) 20,
      (byte) 153,
      (byte) 246,
      (byte) 180,
      (byte) 208 /*0xD0*/,
      (byte) 29,
      (byte) 85,
      (byte) 249,
      (byte) 23,
      (byte) 128 /*0x80*/,
      (byte) 142,
      (byte) 114,
      (byte) 252,
      (byte) 237,
      (byte) 241,
      (byte) 93,
      (byte) 105,
      (byte) 2,
      (byte) 79,
      (byte) 160 /*0xA0*/,
      (byte) 61,
      (byte) 180,
      (byte) 79,
      (byte) 103,
      (byte) 156,
      (byte) 60
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 134;
    sourceArray2[1] = (byte) 186;
    sourceArray2[12] = (byte) 58;
    sourceArray2[3] = (byte) 122;
    sourceArray2[19] = (byte) 28;
    sourceArray2[8] = (byte) 145;
    sourceArray2[40] = (byte) 198;
    sourceArray2[2] = (byte) 103;
    sourceArray2[29] = (byte) 156;
    sourceArray2[27] = (byte) 81;
    sourceArray2[10] = (byte) 187;
    sourceArray2[20] = (byte) 50;
    sourceArray2[21] = (byte) 170;
    sourceArray2[7] = (byte) 175;
    sourceArray2[14] = (byte) 163;
    sourceArray2[15] = (byte) 86;
    sourceArray2[16 /*0x10*/] = (byte) 32 /*0x20*/;
    sourceArray2[22] = (byte) 212;
    sourceArray2[43] = (byte) 48 /*0x30*/;
    sourceArray2[33] = (byte) 4;
    sourceArray2[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    sourceArray2[37] = (byte) 239;
    sourceArray2[39] = (byte) 157;
    sourceArray2[23] = (byte) 214;
    sourceArray2[24] = (byte) 214;
    sourceArray2[28] = (byte) 235;
    sourceArray2[4] = (byte) 239;
    sourceArray2[17] = (byte) 223;
    sourceArray2[0] = (byte) 84;
    sourceArray2[32 /*0x20*/] = (byte) 91;
    sourceArray2[30] = (byte) 54;
    sourceArray2[5] = (byte) 38;
    sourceArray2[38] = (byte) 0;
    sourceArray2[9] = (byte) 143;
    sourceArray2[35] = (byte) 187;
    sourceArray2[25] = (byte) 214;
    sourceArray2[36] = (byte) 113;
    sourceArray2[18] = (byte) 73;
    sourceArray2[34] = (byte) 160 /*0xA0*/;
    sourceArray2[11] = (byte) 234;
    sourceArray2[26] = (byte) 104;
    sourceArray2[6] = (byte) 147;
    sourceArray2[42] = (byte) 92;
    sourceArray2[13] = (byte) 211;
    sourceArray2[44] = (byte) 218;
    sourceArray2[45] = (byte) 244;
    sourceArray2[46] = (byte) 112 /*0x70*/;
    sourceArray2[47] = (byte) 148;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_21749(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[34] = (byte) 143;
    sourceArray1[25] = (byte) 152;
    sourceArray1[33] = (byte) 225;
    sourceArray1[3] = (byte) 18;
    sourceArray1[20] = (byte) 235;
    sourceArray1[41] = (byte) 223;
    sourceArray1[6] = (byte) 127 /*0x7F*/;
    sourceArray1[29] = (byte) 102;
    sourceArray1[8] = (byte) 22;
    sourceArray1[16 /*0x10*/] = (byte) 95;
    sourceArray1[5] = (byte) 96 /*0x60*/;
    sourceArray1[18] = (byte) 21;
    sourceArray1[12] = (byte) 136;
    sourceArray1[10] = (byte) 114;
    sourceArray1[14] = (byte) 191;
    sourceArray1[9] = (byte) 123;
    sourceArray1[39] = (byte) 192 /*0xC0*/;
    sourceArray1[21] = (byte) 196;
    sourceArray1[37] = (byte) 135;
    sourceArray1[7] = (byte) 238;
    sourceArray1[24] = (byte) 199;
    sourceArray1[23] = (byte) 135;
    sourceArray1[22] = (byte) 54;
    sourceArray1[19] = (byte) 243;
    sourceArray1[32 /*0x20*/] = (byte) 60;
    sourceArray1[1] = (byte) 96 /*0x60*/;
    sourceArray1[26] = (byte) 219;
    sourceArray1[27] = (byte) 5;
    sourceArray1[28] = (byte) 251;
    sourceArray1[13] = (byte) 150;
    sourceArray1[30] = (byte) 195;
    sourceArray1[31 /*0x1F*/] = (byte) 225;
    sourceArray1[2] = (byte) 6;
    sourceArray1[4] = (byte) 130;
    sourceArray1[0] = (byte) 136;
    sourceArray1[35] = (byte) 51;
    sourceArray1[36] = (byte) 109;
    sourceArray1[11] = (byte) 197;
    sourceArray1[43] = (byte) 42;
    sourceArray1[38] = (byte) 24;
    sourceArray1[40] = (byte) 206;
    sourceArray1[47] = (byte) 35;
    sourceArray1[42] = (byte) 56;
    sourceArray1[15] = (byte) 5;
    sourceArray1[44] = (byte) 169;
    sourceArray1[45] = (byte) 27;
    sourceArray1[46] = (byte) 188;
    sourceArray1[17] = (byte) 140;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 74,
      (byte) 146,
      (byte) 46,
      (byte) 193,
      (byte) 76,
      (byte) 97,
      (byte) 185,
      (byte) 9,
      (byte) 107,
      (byte) 237,
      (byte) 152,
      (byte) 44,
      (byte) 190,
      (byte) 145,
      (byte) 103,
      (byte) 40,
      (byte) 235,
      (byte) 219,
      (byte) 162,
      (byte) 108,
      (byte) 55,
      (byte) 131,
      (byte) 92,
      (byte) 245,
      (byte) 136,
      (byte) 101,
      (byte) 254,
      (byte) 32 /*0x20*/,
      (byte) 125,
      (byte) 55,
      (byte) 226,
      (byte) 238,
      (byte) 119,
      (byte) 182,
      (byte) 84,
      (byte) 117,
      byte.MaxValue,
      (byte) 205,
      (byte) 35,
      (byte) 185,
      (byte) 14,
      (byte) 106,
      (byte) 1,
      (byte) 240 /*0xF0*/,
      (byte) 209,
      (byte) 86,
      (byte) 33,
      (byte) 73
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
