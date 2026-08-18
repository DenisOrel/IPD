// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_22030
// Assembly: Intermech.Workflow.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 48E18BC1-AABA-4AA1-97DA-4BBD788BE326
// Assembly location: D:\IPS\Client\Intermech.Workflow.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Editor.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_22030
{
  internal static int ssp_workflow_22031(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[36] = (byte) 59;
    sourceArray1[47] = (byte) 52;
    sourceArray1[6] = (byte) 234;
    sourceArray1[3] = (byte) 200;
    sourceArray1[12] = (byte) 181;
    sourceArray1[26] = (byte) 112 /*0x70*/;
    sourceArray1[16 /*0x10*/] = (byte) 35;
    sourceArray1[38] = (byte) 3;
    sourceArray1[7] = (byte) 194;
    sourceArray1[4] = (byte) 231;
    sourceArray1[13] = (byte) 185;
    sourceArray1[11] = (byte) 96 /*0x60*/;
    sourceArray1[35] = (byte) 56;
    sourceArray1[2] = (byte) 185;
    sourceArray1[14] = (byte) 23;
    sourceArray1[19] = (byte) 202;
    sourceArray1[39] = (byte) 105;
    sourceArray1[17] = (byte) 99;
    sourceArray1[18] = (byte) 207;
    sourceArray1[44] = (byte) 196;
    sourceArray1[20] = (byte) 151;
    sourceArray1[0] = (byte) 175;
    sourceArray1[22] = (byte) 2;
    sourceArray1[10] = (byte) 12;
    sourceArray1[24] = (byte) 183;
    sourceArray1[25] = (byte) 14;
    sourceArray1[32 /*0x20*/] = (byte) 186;
    sourceArray1[8] = (byte) 245;
    sourceArray1[28] = (byte) 1;
    sourceArray1[29] = (byte) 104;
    sourceArray1[30] = (byte) 92;
    sourceArray1[31 /*0x1F*/] = (byte) 12;
    sourceArray1[33] = (byte) 22;
    sourceArray1[43] = (byte) 15;
    sourceArray1[34] = (byte) 77;
    sourceArray1[5] = (byte) 85;
    sourceArray1[15] = (byte) 2;
    sourceArray1[37] = (byte) 125;
    sourceArray1[23] = (byte) 172;
    sourceArray1[40] = (byte) 230;
    sourceArray1[21] = (byte) 225;
    sourceArray1[41] = (byte) 199;
    sourceArray1[42] = (byte) 60;
    sourceArray1[9] = (byte) 71;
    sourceArray1[27] = (byte) 50;
    sourceArray1[45] = (byte) 73;
    sourceArray1[46] = (byte) 31 /*0x1F*/;
    sourceArray1[1] = (byte) 223;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 153,
      (byte) 39,
      (byte) 108,
      (byte) 83,
      (byte) 224 /*0xE0*/,
      (byte) 251,
      (byte) 202,
      (byte) 240 /*0xF0*/,
      (byte) 178,
      (byte) 233,
      (byte) 252,
      (byte) 188,
      (byte) 9,
      (byte) 98,
      (byte) 221,
      (byte) 159,
      (byte) 209,
      (byte) 137,
      (byte) 143,
      (byte) 142,
      (byte) 109,
      (byte) 100,
      (byte) 11,
      (byte) 201,
      (byte) 234,
      (byte) 192 /*0xC0*/,
      (byte) 251,
      (byte) 18,
      (byte) 207,
      (byte) 24,
      (byte) 220,
      (byte) 127 /*0x7F*/,
      (byte) 60,
      (byte) 215,
      (byte) 109,
      (byte) 156,
      (byte) 26,
      (byte) 48 /*0x30*/,
      (byte) 116,
      (byte) 14,
      (byte) 40,
      (byte) 154,
      (byte) 80 /*0x50*/,
      (byte) 52,
      (byte) 0,
      (byte) 49,
      (byte) 102,
      (byte) 21
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 366, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }

  internal static string ssp_workflow_22032()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 3)
    {
      byte[] numArray1 = new byte[24];
      byte[] numArray2 = new byte[24]
      {
        (byte) 163,
        (byte) 128 /*0x80*/,
        (byte) 200,
        (byte) 92,
        (byte) 252,
        (byte) 89,
        (byte) 179,
        (byte) 226,
        (byte) 122,
        (byte) 101,
        (byte) 40,
        (byte) 234,
        (byte) 37,
        (byte) 162,
        (byte) 234,
        (byte) 169,
        (byte) 192 /*0xC0*/,
        (byte) 128 /*0x80*/,
        (byte) 36,
        (byte) 160 /*0xA0*/,
        (byte) 59,
        (byte) 73,
        (byte) 190,
        (byte) 235
      };
      byte[] numArray3 = new byte[24]
      {
        (byte) 99,
        (byte) 214,
        (byte) 133,
        (byte) 54,
        (byte) 153,
        (byte) 182,
        (byte) 223,
        (byte) 96 /*0x60*/,
        (byte) 84,
        (byte) 244,
        (byte) 142,
        (byte) 122,
        (byte) 52,
        (byte) 35,
        (byte) 46,
        (byte) 200,
        (byte) 247,
        (byte) 201,
        (byte) 43,
        (byte) 119,
        (byte) 183,
        (byte) 196,
        (byte) 28,
        (byte) 123
      };
      key.Query(true, 366, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 24);
      for (int index = 0; index < 24; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[24];
    byte[] numArray5 = new byte[24]
    {
      (byte) 52,
      (byte) 226,
      (byte) 180,
      (byte) 200,
      (byte) 245,
      (byte) 205,
      (byte) 34,
      (byte) 202,
      (byte) 109,
      (byte) 115,
      (byte) 154,
      (byte) 67,
      (byte) 14,
      (byte) 149,
      (byte) 224 /*0xE0*/,
      (byte) 226,
      (byte) 123,
      (byte) 87,
      (byte) 112 /*0x70*/,
      (byte) 9,
      (byte) 201,
      (byte) 64 /*0x40*/,
      (byte) 217,
      (byte) 110
    };
    byte[] numArray6 = new byte[24];
    numArray6[13] = (byte) 109;
    numArray6[1] = (byte) 217;
    numArray6[2] = (byte) 14;
    numArray6[7] = (byte) 151;
    numArray6[21] = (byte) 240 /*0xF0*/;
    numArray6[18] = (byte) 151;
    numArray6[6] = (byte) 2;
    numArray6[8] = (byte) 249;
    numArray6[0] = (byte) 118;
    numArray6[9] = (byte) 124;
    numArray6[10] = (byte) 81;
    numArray6[20] = (byte) 101;
    numArray6[12] = (byte) 90;
    numArray6[17] = (byte) 176 /*0xB0*/;
    numArray6[16 /*0x10*/] = (byte) 55;
    numArray6[3] = (byte) 184;
    numArray6[11] = (byte) 198;
    numArray6[22] = (byte) 250;
    numArray6[4] = (byte) 65;
    numArray6[19] = (byte) 94;
    numArray6[15] = (byte) 128 /*0x80*/;
    numArray6[14] = (byte) 230;
    numArray6[23] = (byte) 55;
    numArray6[5] = (byte) 120;
    key.Query(true, 366, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 24);
    for (int index = 0; index < 24; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
