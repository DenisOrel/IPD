// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22135
// Assembly: Intermech.Workflow.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8228C0CD-1234-4581-9863-2FEE480D176A
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Workflow.Server.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_22135
{
  internal static int ssp_workflow_server_22136(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 37,
      (byte) 213,
      (byte) 12,
      (byte) 253,
      (byte) 97,
      (byte) 31 /*0x1F*/,
      (byte) 123,
      (byte) 175,
      (byte) 100,
      (byte) 123,
      (byte) 124,
      (byte) 91,
      (byte) 252,
      (byte) 110,
      (byte) 126,
      (byte) 151,
      (byte) 236,
      (byte) 54,
      (byte) 173,
      (byte) 162,
      (byte) 224 /*0xE0*/,
      (byte) 98,
      (byte) 123,
      (byte) 11,
      (byte) 159,
      (byte) 216,
      (byte) 100,
      (byte) 69,
      (byte) 190,
      (byte) 43,
      (byte) 120,
      (byte) 177,
      (byte) 175,
      (byte) 60,
      (byte) 94,
      (byte) 119,
      (byte) 199,
      (byte) 253,
      (byte) 122,
      (byte) 196,
      (byte) 72,
      (byte) 71,
      (byte) 99,
      (byte) 192 /*0xC0*/,
      (byte) 135,
      (byte) 30,
      (byte) 25,
      (byte) 0
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 242;
    sourceArray2[46] = (byte) 19;
    sourceArray2[2] = (byte) 181;
    sourceArray2[24] = (byte) 76;
    sourceArray2[21] = (byte) 184;
    sourceArray2[5] = (byte) 235;
    sourceArray2[41] = (byte) 156;
    sourceArray2[32 /*0x20*/] = (byte) 76;
    sourceArray2[12] = (byte) 237;
    sourceArray2[20] = (byte) 216;
    sourceArray2[10] = (byte) 219;
    sourceArray2[38] = (byte) 20;
    sourceArray2[31 /*0x1F*/] = (byte) 252;
    sourceArray2[13] = (byte) 192 /*0xC0*/;
    sourceArray2[14] = (byte) 139;
    sourceArray2[15] = (byte) 111;
    sourceArray2[18] = (byte) 126;
    sourceArray2[17] = (byte) 134;
    sourceArray2[6] = (byte) 41;
    sourceArray2[19] = (byte) 193;
    sourceArray2[30] = (byte) 234;
    sourceArray2[33] = (byte) 241;
    sourceArray2[47] = (byte) 38;
    sourceArray2[23] = (byte) 28;
    sourceArray2[7] = (byte) 132;
    sourceArray2[1] = (byte) 39;
    sourceArray2[26] = (byte) 201;
    sourceArray2[4] = (byte) 234;
    sourceArray2[25] = (byte) 190;
    sourceArray2[29] = (byte) 125;
    sourceArray2[28] = (byte) 117;
    sourceArray2[22] = (byte) 155;
    sourceArray2[9] = (byte) 231;
    sourceArray2[35] = (byte) 150;
    sourceArray2[34] = (byte) 64 /*0x40*/;
    sourceArray2[42] = (byte) 131;
    sourceArray2[36] = (byte) 206;
    sourceArray2[37] = (byte) 151;
    sourceArray2[0] = (byte) 243;
    sourceArray2[39] = (byte) 18;
    sourceArray2[40] = (byte) 145;
    sourceArray2[3] = (byte) 235;
    sourceArray2[27] = (byte) 244;
    sourceArray2[43] = (byte) 35;
    sourceArray2[44] = (byte) 10;
    sourceArray2[45] = (byte) 168;
    sourceArray2[8] = (byte) 172;
    sourceArray2[11] = (byte) 68;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_server_22137(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[1] = (byte) 240 /*0xF0*/;
    sourceArray1[35] = (byte) 115;
    sourceArray1[2] = (byte) 92;
    sourceArray1[3] = (byte) 25;
    sourceArray1[4] = (byte) 12;
    sourceArray1[38] = (byte) 36;
    sourceArray1[6] = (byte) 22;
    sourceArray1[7] = (byte) 69;
    sourceArray1[47] = (byte) 40;
    sourceArray1[42] = (byte) 198;
    sourceArray1[36] = (byte) 132;
    sourceArray1[10] = (byte) 159;
    sourceArray1[30] = (byte) 212;
    sourceArray1[13] = (byte) 201;
    sourceArray1[27] = (byte) 222;
    sourceArray1[31 /*0x1F*/] = (byte) 253;
    sourceArray1[16 /*0x10*/] = (byte) 27;
    sourceArray1[40] = (byte) 155;
    sourceArray1[18] = (byte) 19;
    sourceArray1[19] = (byte) 25;
    sourceArray1[20] = (byte) 156;
    sourceArray1[21] = (byte) 11;
    sourceArray1[22] = (byte) 115;
    sourceArray1[23] = (byte) 234;
    sourceArray1[5] = (byte) 203;
    sourceArray1[25] = (byte) 80 /*0x50*/;
    sourceArray1[14] = (byte) 86;
    sourceArray1[29] = (byte) 66;
    sourceArray1[28] = (byte) 72;
    sourceArray1[34] = (byte) 101;
    sourceArray1[39] = (byte) 37;
    sourceArray1[0] = (byte) 89;
    sourceArray1[32 /*0x20*/] = (byte) 199;
    sourceArray1[33] = (byte) 13;
    sourceArray1[43] = (byte) 216;
    sourceArray1[45] = (byte) 71;
    sourceArray1[24] = (byte) 10;
    sourceArray1[37] = (byte) 187;
    sourceArray1[11] = (byte) 126;
    sourceArray1[9] = (byte) 195;
    sourceArray1[17] = (byte) 9;
    sourceArray1[41] = (byte) 201;
    sourceArray1[12] = (byte) 222;
    sourceArray1[15] = (byte) 85;
    sourceArray1[8] = (byte) 238;
    sourceArray1[44] = (byte) 125;
    sourceArray1[26] = (byte) 202;
    sourceArray1[46] = (byte) 192 /*0xC0*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[47] = (byte) 245;
    sourceArray2[0] = (byte) 28;
    sourceArray2[28] = (byte) 170;
    sourceArray2[11] = (byte) 227;
    sourceArray2[30] = (byte) 61;
    sourceArray2[18] = (byte) 20;
    sourceArray2[6] = (byte) 174;
    sourceArray2[14] = (byte) 49;
    sourceArray2[8] = (byte) 194;
    sourceArray2[9] = (byte) 236;
    sourceArray2[44] = (byte) 121;
    sourceArray2[31 /*0x1F*/] = (byte) 166;
    sourceArray2[13] = (byte) 136;
    sourceArray2[3] = (byte) 183;
    sourceArray2[22] = byte.MaxValue;
    sourceArray2[42] = (byte) 8;
    sourceArray2[16 /*0x10*/] = (byte) 75;
    sourceArray2[17] = (byte) 147;
    sourceArray2[36] = (byte) 13;
    sourceArray2[19] = (byte) 48 /*0x30*/;
    sourceArray2[20] = (byte) 108;
    sourceArray2[10] = (byte) 83;
    sourceArray2[45] = (byte) 79;
    sourceArray2[5] = (byte) 200;
    sourceArray2[24] = (byte) 103;
    sourceArray2[33] = (byte) 181;
    sourceArray2[25] = (byte) 105;
    sourceArray2[1] = (byte) 193;
    sourceArray2[37] = (byte) 246;
    sourceArray2[12] = (byte) 33;
    sourceArray2[15] = (byte) 13;
    sourceArray2[4] = (byte) 178;
    sourceArray2[32 /*0x20*/] = (byte) 61;
    sourceArray2[7] = (byte) 12;
    sourceArray2[34] = (byte) 99;
    sourceArray2[35] = (byte) 172;
    sourceArray2[2] = (byte) 179;
    sourceArray2[29] = (byte) 133;
    sourceArray2[23] = (byte) 88;
    sourceArray2[39] = byte.MaxValue;
    sourceArray2[40] = (byte) 233;
    sourceArray2[41] = (byte) 152;
    sourceArray2[26] = (byte) 97;
    sourceArray2[43] = (byte) 216;
    sourceArray2[27] = (byte) 96 /*0x60*/;
    sourceArray2[21] = (byte) 247;
    sourceArray2[46] = (byte) 247;
    sourceArray2[38] = (byte) 147;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_server_22138(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[25] = (byte) 196;
    sourceArray1[21] = (byte) 130;
    sourceArray1[35] = (byte) 194;
    sourceArray1[41] = (byte) 74;
    sourceArray1[43] = (byte) 118;
    sourceArray1[5] = (byte) 139;
    sourceArray1[42] = (byte) 100;
    sourceArray1[7] = (byte) 85;
    sourceArray1[0] = (byte) 197;
    sourceArray1[18] = (byte) 116;
    sourceArray1[16 /*0x10*/] = (byte) 223;
    sourceArray1[11] = (byte) 37;
    sourceArray1[39] = (byte) 6;
    sourceArray1[13] = (byte) 243;
    sourceArray1[34] = (byte) 4;
    sourceArray1[15] = (byte) 254;
    sourceArray1[45] = (byte) 7;
    sourceArray1[26] = (byte) 185;
    sourceArray1[10] = (byte) 42;
    sourceArray1[19] = (byte) 25;
    sourceArray1[20] = (byte) 140;
    sourceArray1[6] = byte.MaxValue;
    sourceArray1[17] = (byte) 49;
    sourceArray1[23] = (byte) 205;
    sourceArray1[24] = (byte) 119;
    sourceArray1[4] = (byte) 173;
    sourceArray1[32 /*0x20*/] = (byte) 85;
    sourceArray1[27] = (byte) 143;
    sourceArray1[8] = (byte) 120;
    sourceArray1[29] = (byte) 61;
    sourceArray1[30] = (byte) 217;
    sourceArray1[12] = (byte) 190;
    sourceArray1[22] = (byte) 178;
    sourceArray1[33] = (byte) 65;
    sourceArray1[37] = (byte) 179;
    sourceArray1[28] = (byte) 104;
    sourceArray1[36] = (byte) 168;
    sourceArray1[14] = (byte) 9;
    sourceArray1[38] = (byte) 3;
    sourceArray1[40] = (byte) 50;
    sourceArray1[3] = (byte) 118;
    sourceArray1[9] = (byte) 228;
    sourceArray1[46] = (byte) 184;
    sourceArray1[31 /*0x1F*/] = (byte) 181;
    sourceArray1[44] = (byte) 120;
    sourceArray1[1] = (byte) 87;
    sourceArray1[2] = (byte) 93;
    sourceArray1[47] = (byte) 163;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 113,
      (byte) 214,
      (byte) 225,
      (byte) 162,
      (byte) 58,
      (byte) 94,
      (byte) 230,
      (byte) 26,
      (byte) 126,
      (byte) 30,
      (byte) 7,
      (byte) 43,
      (byte) 252,
      (byte) 28,
      (byte) 119,
      (byte) 185,
      (byte) 238,
      (byte) 163,
      (byte) 197,
      (byte) 54,
      (byte) 29,
      (byte) 125,
      (byte) 144 /*0x90*/,
      (byte) 228,
      (byte) 230,
      (byte) 222,
      (byte) 248,
      (byte) 150,
      (byte) 142,
      (byte) 235,
      (byte) 228,
      (byte) 115,
      (byte) 49,
      (byte) 129,
      (byte) 205,
      (byte) 151,
      (byte) 94,
      (byte) 137,
      (byte) 209,
      (byte) 98,
      (byte) 123,
      (byte) 214,
      (byte) 60,
      (byte) 192 /*0xC0*/,
      (byte) 170,
      (byte) 111,
      (byte) 180,
      (byte) 123
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 365, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
