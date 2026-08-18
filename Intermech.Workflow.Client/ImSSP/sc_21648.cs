// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_21648
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_21648
{
  private static byte[] sspq = new byte[44]
  {
    (byte) 99,
    (byte) 220,
    (byte) 135,
    (byte) 83,
    (byte) 104,
    (byte) 60,
    (byte) 219,
    (byte) 101,
    (byte) 26,
    (byte) 4,
    (byte) 230,
    (byte) 156,
    (byte) 190,
    (byte) 83,
    (byte) 128 /*0x80*/,
    (byte) 231,
    (byte) 66,
    (byte) 211,
    (byte) 156,
    (byte) 24,
    (byte) 170,
    (byte) 68,
    (byte) 0,
    (byte) 215,
    (byte) 20,
    (byte) 28,
    (byte) 142,
    (byte) 29,
    (byte) 51,
    (byte) 229,
    (byte) 93,
    (byte) 23,
    (byte) 110,
    (byte) 181,
    (byte) 200,
    (byte) 51,
    (byte) 74,
    (byte) 164,
    (byte) 132,
    (byte) 90,
    (byte) 5,
    (byte) 28,
    (byte) 26,
    (byte) 202
  };
  private static byte[] sspr = new byte[44]
  {
    (byte) 184,
    (byte) 95,
    (byte) 246,
    (byte) 133,
    (byte) 77,
    (byte) 24,
    (byte) 96 /*0x60*/,
    (byte) 188,
    (byte) 199,
    (byte) 42,
    (byte) 195,
    (byte) 91,
    (byte) 115,
    (byte) 196,
    (byte) 48 /*0x30*/,
    (byte) 172,
    (byte) 189,
    (byte) 51,
    (byte) 135,
    (byte) 77,
    (byte) 25,
    (byte) 182,
    (byte) 115,
    (byte) 39,
    (byte) 128 /*0x80*/,
    (byte) 94,
    (byte) 37,
    (byte) 16 /*0x10*/,
    (byte) 183,
    (byte) 86,
    (byte) 34,
    (byte) 40,
    (byte) 7,
    (byte) 224 /*0xE0*/,
    (byte) 243,
    (byte) 10,
    (byte) 108,
    (byte) 97,
    (byte) 140,
    (byte) 116,
    (byte) 227,
    (byte) 162,
    (byte) 242,
    (byte) 153
  };

  internal static int ssp_workflow_21649(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[25] = (byte) 188;
    sourceArray1[1] = (byte) 83;
    sourceArray1[2] = (byte) 40;
    sourceArray1[14] = (byte) 16 /*0x10*/;
    sourceArray1[4] = (byte) 77;
    sourceArray1[30] = (byte) 32 /*0x20*/;
    sourceArray1[6] = (byte) 107;
    sourceArray1[7] = (byte) 157;
    sourceArray1[35] = (byte) 41;
    sourceArray1[22] = (byte) 253;
    sourceArray1[0] = (byte) 7;
    sourceArray1[43] = (byte) 68;
    sourceArray1[12] = (byte) 60;
    sourceArray1[13] = (byte) 184;
    sourceArray1[17] = (byte) 60;
    sourceArray1[27] = (byte) 50;
    sourceArray1[15] = (byte) 0;
    sourceArray1[3] = (byte) 136;
    sourceArray1[18] = (byte) 129;
    sourceArray1[19] = (byte) 37;
    sourceArray1[20] = (byte) 150;
    sourceArray1[10] = (byte) 143;
    sourceArray1[44] = (byte) 199;
    sourceArray1[32 /*0x20*/] = (byte) 56;
    sourceArray1[39] = (byte) 3;
    sourceArray1[42] = (byte) 115;
    sourceArray1[45] = (byte) 221;
    sourceArray1[47] = (byte) 176 /*0xB0*/;
    sourceArray1[28] = (byte) 220;
    sourceArray1[29] = (byte) 100;
    sourceArray1[31 /*0x1F*/] = (byte) 100;
    sourceArray1[11] = (byte) 178;
    sourceArray1[24] = (byte) 9;
    sourceArray1[33] = (byte) 109;
    sourceArray1[9] = (byte) 26;
    sourceArray1[5] = (byte) 59;
    sourceArray1[36] = (byte) 121;
    sourceArray1[37] = (byte) 129;
    sourceArray1[23] = (byte) 24;
    sourceArray1[16 /*0x10*/] = (byte) 18;
    sourceArray1[40] = (byte) 60;
    sourceArray1[41] = (byte) 253;
    sourceArray1[21] = (byte) 202;
    sourceArray1[38] = (byte) 215;
    sourceArray1[8] = (byte) 152;
    sourceArray1[34] = (byte) 143;
    sourceArray1[46] = (byte) 172;
    sourceArray1[26] = (byte) 130;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 104,
      (byte) 44,
      (byte) 236,
      (byte) 34,
      (byte) 236,
      (byte) 47,
      (byte) 131,
      (byte) 80 /*0x50*/,
      (byte) 171,
      (byte) 40,
      (byte) 45,
      (byte) 48 /*0x30*/,
      (byte) 72,
      (byte) 221,
      (byte) 0,
      (byte) 212,
      (byte) 205,
      (byte) 72,
      (byte) 151,
      (byte) 144 /*0x90*/,
      (byte) 209,
      (byte) 116,
      (byte) 4,
      (byte) 224 /*0xE0*/,
      (byte) 133,
      (byte) 16 /*0x10*/,
      (byte) 174,
      (byte) 188,
      (byte) 108,
      (byte) 198,
      (byte) 231,
      (byte) 196,
      (byte) 254,
      (byte) 28,
      (byte) 192 /*0xC0*/,
      (byte) 251,
      (byte) 105,
      (byte) 140,
      (byte) 20,
      (byte) 109,
      (byte) 128 /*0x80*/,
      (byte) 45,
      (byte) 81,
      (byte) 60,
      (byte) 91,
      (byte) 226,
      (byte) 193,
      (byte) 132
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 366, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[44];
    byte[] response2 = new byte[44];
    Array.Copy((Array) sc_21648.sspq, 0, (Array) numArray2, 0, 44);
    key.Query(true, 366, numArray2, response2);
    Array.Copy((Array) sc_21648.sspr, 0, (Array) numArray2, 0, 44);
    for (int index = 0; index < numArray2.Length; ++index)
    {
      if ((int) numArray2[index] != (int) response2[index])
      {
        key.TagValue = (int) response2[index];
        break;
      }
    }
    return BitConverter.ToInt32(response1, 0) ^ BitConverter.ToInt32(numArray1, 0) ^ k;
  }

  internal static int ssp_workflow_21650(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[3] = (byte) 99;
    sourceArray1[1] = (byte) 175;
    sourceArray1[27] = (byte) 66;
    sourceArray1[20] = (byte) 46;
    sourceArray1[4] = (byte) 193;
    sourceArray1[32 /*0x20*/] = (byte) 106;
    sourceArray1[22] = (byte) 138;
    sourceArray1[7] = (byte) 50;
    sourceArray1[37] = (byte) 208 /*0xD0*/;
    sourceArray1[28] = (byte) 19;
    sourceArray1[24] = (byte) 157;
    sourceArray1[18] = (byte) 167;
    sourceArray1[12] = (byte) 62;
    sourceArray1[8] = (byte) 241;
    sourceArray1[30] = (byte) 42;
    sourceArray1[26] = (byte) 78;
    sourceArray1[47] = (byte) 55;
    sourceArray1[9] = (byte) 30;
    sourceArray1[14] = (byte) 174;
    sourceArray1[19] = (byte) 206;
    sourceArray1[23] = (byte) 77;
    sourceArray1[11] = (byte) 176 /*0xB0*/;
    sourceArray1[15] = (byte) 190;
    sourceArray1[21] = (byte) 9;
    sourceArray1[10] = (byte) 219;
    sourceArray1[25] = (byte) 157;
    sourceArray1[0] = (byte) 171;
    sourceArray1[6] = (byte) 163;
    sourceArray1[44] = (byte) 87;
    sourceArray1[29] = (byte) 142;
    sourceArray1[16 /*0x10*/] = (byte) 49;
    sourceArray1[31 /*0x1F*/] = (byte) 54;
    sourceArray1[39] = (byte) 243;
    sourceArray1[33] = (byte) 222;
    sourceArray1[34] = (byte) 112 /*0x70*/;
    sourceArray1[35] = (byte) 223;
    sourceArray1[36] = (byte) 87;
    sourceArray1[17] = (byte) 52;
    sourceArray1[38] = (byte) 36;
    sourceArray1[40] = (byte) 116;
    sourceArray1[46] = (byte) 101;
    sourceArray1[41] = (byte) 60;
    sourceArray1[42] = (byte) 196;
    sourceArray1[43] = (byte) 170;
    sourceArray1[5] = (byte) 105;
    sourceArray1[45] = (byte) 153;
    sourceArray1[13] = (byte) 76;
    sourceArray1[2] = (byte) 198;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[16 /*0x10*/] = (byte) 156;
    sourceArray2[42] = (byte) 89;
    sourceArray2[4] = (byte) 69;
    sourceArray2[3] = (byte) 130;
    sourceArray2[8] = (byte) 179;
    sourceArray2[35] = (byte) 72;
    sourceArray2[6] = (byte) 56;
    sourceArray2[5] = (byte) 253;
    sourceArray2[38] = (byte) 155;
    sourceArray2[9] = (byte) 212;
    sourceArray2[10] = (byte) 238;
    sourceArray2[14] = (byte) 76;
    sourceArray2[12] = (byte) 230;
    sourceArray2[43] = (byte) 3;
    sourceArray2[47] = (byte) 232;
    sourceArray2[15] = (byte) 150;
    sourceArray2[2] = (byte) 120;
    sourceArray2[17] = (byte) 63 /*0x3F*/;
    sourceArray2[29] = (byte) 23;
    sourceArray2[41] = (byte) 148;
    sourceArray2[21] = (byte) 117;
    sourceArray2[1] = (byte) 62;
    sourceArray2[22] = (byte) 96 /*0x60*/;
    sourceArray2[23] = (byte) 31 /*0x1F*/;
    sourceArray2[7] = (byte) 200;
    sourceArray2[20] = (byte) 250;
    sourceArray2[26] = (byte) 53;
    sourceArray2[30] = (byte) 71;
    sourceArray2[28] = (byte) 137;
    sourceArray2[27] = (byte) 121;
    sourceArray2[0] = (byte) 11;
    sourceArray2[24] = (byte) 186;
    sourceArray2[19] = (byte) 62;
    sourceArray2[33] = (byte) 29;
    sourceArray2[32 /*0x20*/] = (byte) 237;
    sourceArray2[18] = (byte) 114;
    sourceArray2[36] = (byte) 201;
    sourceArray2[37] = (byte) 191;
    sourceArray2[25] = (byte) 40;
    sourceArray2[39] = (byte) 6;
    sourceArray2[13] = (byte) 151;
    sourceArray2[45] = (byte) 37;
    sourceArray2[31 /*0x1F*/] = (byte) 150;
    sourceArray2[34] = (byte) 92;
    sourceArray2[40] = (byte) 152;
    sourceArray2[11] = (byte) 41;
    sourceArray2[46] = byte.MaxValue;
    sourceArray2[44] = (byte) 219;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static int ssp_workflow_21651(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 211,
      (byte) 72,
      (byte) 203,
      (byte) 163,
      (byte) 6,
      (byte) 32 /*0x20*/,
      (byte) 220,
      (byte) 123,
      (byte) 76,
      (byte) 198,
      (byte) 86,
      (byte) 49,
      (byte) 197,
      (byte) 148,
      (byte) 251,
      (byte) 65,
      (byte) 123,
      (byte) 26,
      (byte) 62,
      (byte) 18,
      (byte) 184,
      (byte) 73,
      (byte) 16 /*0x10*/,
      (byte) 101,
      (byte) 162,
      (byte) 63 /*0x3F*/,
      (byte) 238,
      (byte) 236,
      (byte) 201,
      (byte) 159,
      (byte) 159,
      (byte) 45,
      (byte) 14,
      (byte) 249,
      (byte) 7,
      (byte) 254,
      (byte) 8,
      (byte) 62,
      (byte) 35,
      (byte) 35,
      (byte) 211,
      (byte) 182,
      (byte) 110,
      (byte) 243,
      (byte) 149,
      (byte) 233,
      (byte) 169,
      (byte) 15
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[9] = (byte) 193;
    sourceArray2[1] = (byte) 31 /*0x1F*/;
    sourceArray2[16 /*0x10*/] = (byte) 105;
    sourceArray2[32 /*0x20*/] = (byte) 241;
    sourceArray2[4] = (byte) 229;
    sourceArray2[5] = (byte) 23;
    sourceArray2[30] = (byte) 20;
    sourceArray2[6] = (byte) 131;
    sourceArray2[8] = (byte) 3;
    sourceArray2[46] = (byte) 207;
    sourceArray2[10] = (byte) 140;
    sourceArray2[31 /*0x1F*/] = (byte) 177;
    sourceArray2[12] = (byte) 124;
    sourceArray2[13] = (byte) 239;
    sourceArray2[14] = (byte) 143;
    sourceArray2[23] = (byte) 11;
    sourceArray2[45] = (byte) 46;
    sourceArray2[41] = (byte) 166;
    sourceArray2[35] = (byte) 205;
    sourceArray2[0] = (byte) 253;
    sourceArray2[20] = (byte) 139;
    sourceArray2[21] = (byte) 182;
    sourceArray2[7] = (byte) 145;
    sourceArray2[3] = (byte) 100;
    sourceArray2[17] = (byte) 66;
    sourceArray2[25] = (byte) 7;
    sourceArray2[36] = (byte) 215;
    sourceArray2[27] = (byte) 73;
    sourceArray2[28] = (byte) 54;
    sourceArray2[24] = (byte) 253;
    sourceArray2[18] = (byte) 243;
    sourceArray2[38] = (byte) 176 /*0xB0*/;
    sourceArray2[15] = (byte) 16 /*0x10*/;
    sourceArray2[33] = (byte) 62;
    sourceArray2[19] = (byte) 44;
    sourceArray2[2] = (byte) 66;
    sourceArray2[43] = (byte) 21;
    sourceArray2[37] = (byte) 203;
    sourceArray2[44] = (byte) 151;
    sourceArray2[11] = (byte) 22;
    sourceArray2[29] = (byte) 188;
    sourceArray2[34] = (byte) 12;
    sourceArray2[42] = (byte) 174;
    sourceArray2[26] = (byte) 209;
    sourceArray2[22] = (byte) 227;
    sourceArray2[39] = (byte) 99;
    sourceArray2[40] = (byte) 165;
    sourceArray2[47] = (byte) 93;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
