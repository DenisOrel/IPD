// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19383
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19383
{
  internal static int ssp_techcard_19384(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[14] = (byte) 165;
    sourceArray1[1] = (byte) 71;
    sourceArray1[34] = (byte) 54;
    sourceArray1[3] = (byte) 191;
    sourceArray1[33] = (byte) 122;
    sourceArray1[40] = (byte) 166;
    sourceArray1[46] = (byte) 196;
    sourceArray1[47] = (byte) 52;
    sourceArray1[8] = (byte) 220;
    sourceArray1[9] = (byte) 244;
    sourceArray1[10] = (byte) 55;
    sourceArray1[31 /*0x1F*/] = (byte) 16 /*0x10*/;
    sourceArray1[12] = (byte) 27;
    sourceArray1[7] = (byte) 139;
    sourceArray1[39] = (byte) 69;
    sourceArray1[15] = (byte) 78;
    sourceArray1[17] = (byte) 125;
    sourceArray1[4] = (byte) 109;
    sourceArray1[11] = (byte) 47;
    sourceArray1[28] = (byte) 146;
    sourceArray1[16 /*0x10*/] = (byte) 244;
    sourceArray1[21] = (byte) 248;
    sourceArray1[22] = (byte) 154;
    sourceArray1[35] = (byte) 122;
    sourceArray1[24] = (byte) 0;
    sourceArray1[25] = (byte) 27;
    sourceArray1[26] = (byte) 106;
    sourceArray1[38] = (byte) 187;
    sourceArray1[45] = (byte) 226;
    sourceArray1[29] = (byte) 180;
    sourceArray1[6] = (byte) 112 /*0x70*/;
    sourceArray1[23] = (byte) 66;
    sourceArray1[5] = (byte) 219;
    sourceArray1[32 /*0x20*/] = (byte) 93;
    sourceArray1[43] = (byte) 235;
    sourceArray1[20] = (byte) 18;
    sourceArray1[36] = (byte) 11;
    sourceArray1[37] = (byte) 168;
    sourceArray1[42] = (byte) 118;
    sourceArray1[0] = (byte) 158;
    sourceArray1[18] = (byte) 105;
    sourceArray1[41] = (byte) 145;
    sourceArray1[27] = (byte) 135;
    sourceArray1[2] = (byte) 110;
    sourceArray1[44] = (byte) 131;
    sourceArray1[13] = (byte) 102;
    sourceArray1[19] = (byte) 53;
    sourceArray1[30] = (byte) 249;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[5] = (byte) 38;
    sourceArray2[47] = (byte) 110;
    sourceArray2[22] = (byte) 34;
    sourceArray2[3] = (byte) 228;
    sourceArray2[28] = (byte) 127 /*0x7F*/;
    sourceArray2[10] = (byte) 5;
    sourceArray2[21] = (byte) 105;
    sourceArray2[16 /*0x10*/] = (byte) 104;
    sourceArray2[8] = (byte) 117;
    sourceArray2[39] = (byte) 157;
    sourceArray2[20] = (byte) 224 /*0xE0*/;
    sourceArray2[34] = (byte) 235;
    sourceArray2[12] = (byte) 108;
    sourceArray2[13] = (byte) 177;
    sourceArray2[14] = (byte) 233;
    sourceArray2[41] = (byte) 62;
    sourceArray2[9] = (byte) 245;
    sourceArray2[2] = (byte) 149;
    sourceArray2[37] = (byte) 54;
    sourceArray2[19] = (byte) 162;
    sourceArray2[0] = (byte) 113;
    sourceArray2[45] = (byte) 24;
    sourceArray2[24] = (byte) 105;
    sourceArray2[1] = (byte) 109;
    sourceArray2[17] = (byte) 88;
    sourceArray2[26] = (byte) 69;
    sourceArray2[11] = (byte) 231;
    sourceArray2[27] = (byte) 2;
    sourceArray2[15] = (byte) 150;
    sourceArray2[29] = (byte) 87;
    sourceArray2[7] = (byte) 141;
    sourceArray2[25] = (byte) 194;
    sourceArray2[32 /*0x20*/] = (byte) 64 /*0x40*/;
    sourceArray2[33] = (byte) 207;
    sourceArray2[18] = (byte) 250;
    sourceArray2[35] = (byte) 31 /*0x1F*/;
    sourceArray2[36] = (byte) 61;
    sourceArray2[6] = (byte) 14;
    sourceArray2[30] = (byte) 13;
    sourceArray2[4] = (byte) 233;
    sourceArray2[40] = (byte) 82;
    sourceArray2[46] = (byte) 100;
    sourceArray2[42] = (byte) 226;
    sourceArray2[43] = (byte) 161;
    sourceArray2[44] = (byte) 166;
    sourceArray2[31 /*0x1F*/] = (byte) 165;
    sourceArray2[38] = (byte) 77;
    sourceArray2[23] = (byte) 40;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19385(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[7] = (byte) 54;
    sourceArray1[1] = (byte) 248;
    sourceArray1[2] = (byte) 27;
    sourceArray1[36] = (byte) 206;
    sourceArray1[0] = (byte) 152;
    sourceArray1[38] = (byte) 60;
    sourceArray1[6] = (byte) 148;
    sourceArray1[28] = (byte) 122;
    sourceArray1[8] = (byte) 100;
    sourceArray1[9] = (byte) 220;
    sourceArray1[10] = (byte) 113;
    sourceArray1[31 /*0x1F*/] = (byte) 239;
    sourceArray1[41] = (byte) 253;
    sourceArray1[42] = (byte) 205;
    sourceArray1[35] = (byte) 217;
    sourceArray1[14] = (byte) 165;
    sourceArray1[17] = (byte) 63 /*0x3F*/;
    sourceArray1[3] = (byte) 39;
    sourceArray1[5] = (byte) 6;
    sourceArray1[19] = (byte) 147;
    sourceArray1[45] = (byte) 152;
    sourceArray1[27] = (byte) 158;
    sourceArray1[22] = (byte) 219;
    sourceArray1[23] = (byte) 211;
    sourceArray1[44] = (byte) 55;
    sourceArray1[25] = (byte) 138;
    sourceArray1[26] = (byte) 160 /*0xA0*/;
    sourceArray1[21] = (byte) 10;
    sourceArray1[39] = (byte) 91;
    sourceArray1[29] = (byte) 52;
    sourceArray1[30] = (byte) 142;
    sourceArray1[11] = (byte) 3;
    sourceArray1[20] = (byte) 198;
    sourceArray1[13] = (byte) 203;
    sourceArray1[34] = (byte) 183;
    sourceArray1[47] = (byte) 234;
    sourceArray1[32 /*0x20*/] = (byte) 25;
    sourceArray1[43] = (byte) 13;
    sourceArray1[16 /*0x10*/] = (byte) 239;
    sourceArray1[40] = (byte) 120;
    sourceArray1[37] = (byte) 13;
    sourceArray1[15] = (byte) 90;
    sourceArray1[33] = (byte) 235;
    sourceArray1[4] = (byte) 130;
    sourceArray1[18] = (byte) 11;
    sourceArray1[24] = (byte) 139;
    sourceArray1[46] = (byte) 146;
    sourceArray1[12] = (byte) 80 /*0x50*/;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 24,
      (byte) 65,
      (byte) 162,
      (byte) 143,
      (byte) 41,
      (byte) 84,
      (byte) 65,
      (byte) 117,
      (byte) 34,
      (byte) 146,
      (byte) 184,
      (byte) 104,
      (byte) 106,
      (byte) 30,
      (byte) 214,
      (byte) 241,
      (byte) 186,
      (byte) 167,
      (byte) 105,
      (byte) 195,
      (byte) 126,
      (byte) 208 /*0xD0*/,
      (byte) 240 /*0xF0*/,
      (byte) 128 /*0x80*/,
      (byte) 98,
      (byte) 86,
      (byte) 234,
      (byte) 177,
      (byte) 128 /*0x80*/,
      (byte) 249,
      (byte) 232,
      (byte) 135,
      (byte) 113,
      (byte) 76,
      (byte) 236,
      (byte) 244,
      (byte) 89,
      (byte) 8,
      (byte) 58,
      (byte) 37,
      (byte) 27,
      (byte) 247,
      (byte) 57,
      (byte) 55,
      (byte) 19,
      (byte) 222,
      (byte) 214,
      (byte) 118
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_techcard_19386(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[46] = (byte) 200;
    sourceArray1[6] = (byte) 183;
    sourceArray1[2] = (byte) 107;
    sourceArray1[1] = (byte) 214;
    sourceArray1[4] = (byte) 154;
    sourceArray1[10] = (byte) 78;
    sourceArray1[13] = byte.MaxValue;
    sourceArray1[34] = (byte) 202;
    sourceArray1[8] = (byte) 223;
    sourceArray1[45] = (byte) 218;
    sourceArray1[43] = (byte) 216;
    sourceArray1[11] = (byte) 171;
    sourceArray1[12] = (byte) 168;
    sourceArray1[39] = (byte) 115;
    sourceArray1[14] = (byte) 174;
    sourceArray1[15] = (byte) 143;
    sourceArray1[19] = (byte) 249;
    sourceArray1[22] = (byte) 233;
    sourceArray1[18] = (byte) 78;
    sourceArray1[25] = (byte) 112 /*0x70*/;
    sourceArray1[21] = (byte) 34;
    sourceArray1[47] = (byte) 69;
    sourceArray1[26] = (byte) 190;
    sourceArray1[16 /*0x10*/] = (byte) 89;
    sourceArray1[24] = (byte) 5;
    sourceArray1[5] = (byte) 139;
    sourceArray1[41] = (byte) 201;
    sourceArray1[9] = (byte) 81;
    sourceArray1[7] = (byte) 154;
    sourceArray1[29] = (byte) 135;
    sourceArray1[30] = (byte) 159;
    sourceArray1[36] = (byte) 251;
    sourceArray1[32 /*0x20*/] = (byte) 229;
    sourceArray1[33] = (byte) 67;
    sourceArray1[31 /*0x1F*/] = (byte) 15;
    sourceArray1[35] = (byte) 223;
    sourceArray1[37] = (byte) 216;
    sourceArray1[20] = (byte) 133;
    sourceArray1[38] = (byte) 28;
    sourceArray1[42] = (byte) 99;
    sourceArray1[40] = (byte) 208 /*0xD0*/;
    sourceArray1[28] = (byte) 153;
    sourceArray1[3] = (byte) 139;
    sourceArray1[17] = (byte) 120;
    sourceArray1[44] = (byte) 110;
    sourceArray1[0] = (byte) 45;
    sourceArray1[27] = (byte) 186;
    sourceArray1[23] = (byte) 15;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 187,
      (byte) 71,
      (byte) 111,
      (byte) 2,
      (byte) 166,
      (byte) 27,
      (byte) 236,
      (byte) 34,
      (byte) 61,
      (byte) 76,
      (byte) 6,
      (byte) 73,
      (byte) 174,
      (byte) 77,
      (byte) 119,
      (byte) 161,
      (byte) 102,
      (byte) 216,
      (byte) 17,
      (byte) 118,
      (byte) 210,
      (byte) 104,
      (byte) 205,
      (byte) 196,
      (byte) 232,
      (byte) 207,
      (byte) 175,
      (byte) 240 /*0xF0*/,
      (byte) 6,
      (byte) 250,
      (byte) 188,
      (byte) 32 /*0x20*/,
      (byte) 63 /*0x3F*/,
      (byte) 150,
      (byte) 182,
      (byte) 34,
      (byte) 2,
      (byte) 141,
      (byte) 36,
      (byte) 143,
      (byte) 27,
      (byte) 93,
      (byte) 254,
      (byte) 151,
      (byte) 92,
      (byte) 32 /*0x20*/,
      (byte) 184,
      (byte) 11
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
