// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19680
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19680
{
  internal static int ssp_techcard_19681(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[20] = (byte) 38;
    sourceArray1[1] = (byte) 184;
    sourceArray1[35] = (byte) 228;
    sourceArray1[3] = (byte) 37;
    sourceArray1[4] = (byte) 8;
    sourceArray1[17] = (byte) 254;
    sourceArray1[11] = (byte) 25;
    sourceArray1[7] = (byte) 115;
    sourceArray1[8] = (byte) 158;
    sourceArray1[37] = (byte) 119;
    sourceArray1[43] = (byte) 180;
    sourceArray1[5] = (byte) 129;
    sourceArray1[27] = (byte) 250;
    sourceArray1[13] = (byte) 248;
    sourceArray1[45] = (byte) 226;
    sourceArray1[34] = (byte) 135;
    sourceArray1[16 /*0x10*/] = (byte) 63 /*0x3F*/;
    sourceArray1[40] = (byte) 5;
    sourceArray1[0] = (byte) 182;
    sourceArray1[25] = (byte) 87;
    sourceArray1[32 /*0x20*/] = (byte) 91;
    sourceArray1[21] = (byte) 218;
    sourceArray1[38] = (byte) 169;
    sourceArray1[23] = (byte) 103;
    sourceArray1[24] = (byte) 59;
    sourceArray1[14] = (byte) 28;
    sourceArray1[26] = (byte) 134;
    sourceArray1[19] = (byte) 150;
    sourceArray1[9] = (byte) 108;
    sourceArray1[29] = (byte) 137;
    sourceArray1[30] = (byte) 149;
    sourceArray1[31 /*0x1F*/] = (byte) 160 /*0xA0*/;
    sourceArray1[47] = (byte) 102;
    sourceArray1[33] = (byte) 192 /*0xC0*/;
    sourceArray1[12] = (byte) 24;
    sourceArray1[15] = (byte) 142;
    sourceArray1[44] = (byte) 29;
    sourceArray1[36] = (byte) 189;
    sourceArray1[2] = (byte) 17;
    sourceArray1[39] = (byte) 228;
    sourceArray1[6] = (byte) 53;
    sourceArray1[41] = (byte) 142;
    sourceArray1[42] = (byte) 34;
    sourceArray1[22] = (byte) 15;
    sourceArray1[28] = (byte) 122;
    sourceArray1[10] = (byte) 87;
    sourceArray1[46] = (byte) 139;
    sourceArray1[18] = (byte) 129;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[27] = (byte) 148;
    sourceArray2[4] = (byte) 233;
    sourceArray2[1] = (byte) 101;
    sourceArray2[15] = (byte) 210;
    sourceArray2[5] = (byte) 155;
    sourceArray2[26] = (byte) 113;
    sourceArray2[6] = (byte) 89;
    sourceArray2[7] = (byte) 93;
    sourceArray2[39] = (byte) 11;
    sourceArray2[43] = (byte) 65;
    sourceArray2[44] = (byte) 145;
    sourceArray2[46] = (byte) 178;
    sourceArray2[12] = (byte) 30;
    sourceArray2[13] = (byte) 194;
    sourceArray2[8] = (byte) 110;
    sourceArray2[2] = (byte) 131;
    sourceArray2[16 /*0x10*/] = (byte) 195;
    sourceArray2[17] = (byte) 123;
    sourceArray2[20] = (byte) 132;
    sourceArray2[19] = (byte) 165;
    sourceArray2[41] = (byte) 59;
    sourceArray2[35] = (byte) 251;
    sourceArray2[22] = (byte) 224 /*0xE0*/;
    sourceArray2[29] = (byte) 161;
    sourceArray2[37] = (byte) 224 /*0xE0*/;
    sourceArray2[25] = (byte) 35;
    sourceArray2[9] = (byte) 223;
    sourceArray2[10] = (byte) 1;
    sourceArray2[28] = (byte) 119;
    sourceArray2[32 /*0x20*/] = (byte) 70;
    sourceArray2[30] = (byte) 245;
    sourceArray2[31 /*0x1F*/] = (byte) 92;
    sourceArray2[21] = (byte) 145;
    sourceArray2[33] = (byte) 112 /*0x70*/;
    sourceArray2[34] = (byte) 214;
    sourceArray2[42] = (byte) 141;
    sourceArray2[14] = (byte) 74;
    sourceArray2[11] = (byte) 142;
    sourceArray2[38] = (byte) 89;
    sourceArray2[0] = (byte) 218;
    sourceArray2[40] = (byte) 233;
    sourceArray2[23] = (byte) 122;
    sourceArray2[36] = (byte) 27;
    sourceArray2[3] = (byte) 149;
    sourceArray2[24] = (byte) 207;
    sourceArray2[45] = (byte) 45;
    sourceArray2[18] = (byte) 169;
    sourceArray2[47] = (byte) 88;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19682(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 14,
      (byte) 134,
      (byte) 105,
      (byte) 53,
      (byte) 141,
      (byte) 164,
      (byte) 41,
      (byte) 35,
      (byte) 47,
      (byte) 210,
      (byte) 31 /*0x1F*/,
      (byte) 74,
      (byte) 91,
      (byte) 88,
      (byte) 65,
      (byte) 222,
      (byte) 123,
      (byte) 206,
      (byte) 125,
      (byte) 92,
      (byte) 77,
      (byte) 176 /*0xB0*/,
      (byte) 28,
      (byte) 59,
      (byte) 31 /*0x1F*/,
      (byte) 128 /*0x80*/,
      (byte) 216,
      (byte) 62,
      (byte) 169,
      (byte) 64 /*0x40*/,
      (byte) 11,
      (byte) 11,
      (byte) 62,
      (byte) 74,
      (byte) 165,
      (byte) 63 /*0x3F*/,
      (byte) 153,
      (byte) 116,
      (byte) 240 /*0xF0*/,
      (byte) 63 /*0x3F*/,
      (byte) 11,
      (byte) 116,
      (byte) 250,
      (byte) 85,
      (byte) 209,
      (byte) 109,
      (byte) 115,
      (byte) 144 /*0x90*/
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 178;
    sourceArray2[19] = (byte) 22;
    sourceArray2[38] = (byte) 84;
    sourceArray2[3] = (byte) 172;
    sourceArray2[4] = (byte) 31 /*0x1F*/;
    sourceArray2[23] = (byte) 26;
    sourceArray2[40] = (byte) 18;
    sourceArray2[45] = (byte) 168;
    sourceArray2[28] = (byte) 17;
    sourceArray2[24] = (byte) 117;
    sourceArray2[10] = (byte) 65;
    sourceArray2[11] = (byte) 134;
    sourceArray2[12] = (byte) 30;
    sourceArray2[13] = (byte) 169;
    sourceArray2[22] = (byte) 203;
    sourceArray2[15] = (byte) 95;
    sourceArray2[16 /*0x10*/] = (byte) 22;
    sourceArray2[36] = (byte) 28;
    sourceArray2[31 /*0x1F*/] = (byte) 28;
    sourceArray2[17] = (byte) 247;
    sourceArray2[20] = (byte) 188;
    sourceArray2[21] = (byte) 219;
    sourceArray2[34] = (byte) 244;
    sourceArray2[25] = (byte) 83;
    sourceArray2[30] = (byte) 205;
    sourceArray2[32 /*0x20*/] = (byte) 183;
    sourceArray2[39] = (byte) 220;
    sourceArray2[6] = (byte) 145;
    sourceArray2[14] = (byte) 203;
    sourceArray2[2] = (byte) 44;
    sourceArray2[9] = (byte) 224 /*0xE0*/;
    sourceArray2[7] = (byte) 200;
    sourceArray2[1] = (byte) 144 /*0x90*/;
    sourceArray2[33] = (byte) 180;
    sourceArray2[0] = (byte) 113;
    sourceArray2[35] = (byte) 128 /*0x80*/;
    sourceArray2[27] = (byte) 152;
    sourceArray2[18] = (byte) 12;
    sourceArray2[47] = (byte) 203;
    sourceArray2[8] = (byte) 113;
    sourceArray2[42] = (byte) 9;
    sourceArray2[37] = (byte) 148;
    sourceArray2[29] = (byte) 196;
    sourceArray2[43] = (byte) 148;
    sourceArray2[44] = (byte) 250;
    sourceArray2[26] = (byte) 140;
    sourceArray2[46] = (byte) 95;
    sourceArray2[5] = (byte) 68;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
