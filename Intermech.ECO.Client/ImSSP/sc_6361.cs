// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_6361
// Assembly: Intermech.ECO.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BF6FF14F-986B-44C3-A04A-31D571D76B17
// Assembly location: D:\IPS\Client\Intermech.ECO.Client.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_6361
{
  private static byte[] sspq = new byte[48 /*0x30*/]
  {
    (byte) 42,
    (byte) 254,
    (byte) 67,
    (byte) 95,
    (byte) 243,
    (byte) 165,
    (byte) 197,
    (byte) 157,
    (byte) 77,
    (byte) 177,
    (byte) 205,
    (byte) 55,
    (byte) 254,
    (byte) 16 /*0x10*/,
    (byte) 43,
    (byte) 39,
    (byte) 213,
    (byte) 178,
    (byte) 144 /*0x90*/,
    (byte) 81,
    (byte) 122,
    (byte) 192 /*0xC0*/,
    (byte) 25,
    (byte) 196,
    (byte) 16 /*0x10*/,
    (byte) 252,
    (byte) 2,
    (byte) 58,
    (byte) 217,
    (byte) 20,
    (byte) 67,
    (byte) 241,
    (byte) 244,
    (byte) 75,
    (byte) 103,
    (byte) 168,
    (byte) 17,
    (byte) 40,
    (byte) 94,
    (byte) 8,
    (byte) 239,
    (byte) 27,
    (byte) 220,
    (byte) 10,
    (byte) 84,
    (byte) 120,
    (byte) 235,
    (byte) 175
  };
  private static byte[] sspr = new byte[48 /*0x30*/]
  {
    (byte) 66,
    (byte) 41,
    (byte) 55,
    (byte) 44,
    (byte) 143,
    (byte) 8,
    (byte) 119,
    (byte) 22,
    (byte) 204,
    (byte) 225,
    (byte) 26,
    (byte) 90,
    (byte) 9,
    (byte) 167,
    (byte) 254,
    (byte) 16 /*0x10*/,
    (byte) 155,
    (byte) 69,
    (byte) 121,
    (byte) 44,
    (byte) 215,
    (byte) 243,
    (byte) 31 /*0x1F*/,
    (byte) 224 /*0xE0*/,
    (byte) 28,
    (byte) 206,
    (byte) 232,
    (byte) 152,
    (byte) 120,
    (byte) 134,
    (byte) 66,
    (byte) 66,
    (byte) 172,
    (byte) 176 /*0xB0*/,
    (byte) 101,
    (byte) 235,
    (byte) 137,
    (byte) 85,
    (byte) 174,
    (byte) 100,
    (byte) 57,
    (byte) 73,
    (byte) 197,
    (byte) 66,
    (byte) 93,
    (byte) 161,
    (byte) 196,
    (byte) 215
  };

  internal static int ssp_workflow_6362(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[26] = (byte) 127 /*0x7F*/;
    sourceArray1[18] = (byte) 0;
    sourceArray1[44] = (byte) 224 /*0xE0*/;
    sourceArray1[33] = (byte) 23;
    sourceArray1[4] = (byte) 75;
    sourceArray1[38] = (byte) 128 /*0x80*/;
    sourceArray1[32 /*0x20*/] = (byte) 56;
    sourceArray1[25] = (byte) 121;
    sourceArray1[8] = (byte) 85;
    sourceArray1[0] = (byte) 82;
    sourceArray1[10] = (byte) 56;
    sourceArray1[11] = (byte) 35;
    sourceArray1[42] = (byte) 181;
    sourceArray1[13] = (byte) 60;
    sourceArray1[29] = (byte) 22;
    sourceArray1[15] = (byte) 197;
    sourceArray1[16 /*0x10*/] = (byte) 37;
    sourceArray1[23] = (byte) 217;
    sourceArray1[30] = (byte) 98;
    sourceArray1[19] = (byte) 201;
    sourceArray1[20] = (byte) 21;
    sourceArray1[21] = (byte) 29;
    sourceArray1[22] = (byte) 148;
    sourceArray1[27] = (byte) 166;
    sourceArray1[7] = (byte) 47;
    sourceArray1[17] = (byte) 105;
    sourceArray1[31 /*0x1F*/] = (byte) 5;
    sourceArray1[5] = (byte) 253;
    sourceArray1[2] = (byte) 207;
    sourceArray1[6] = (byte) 4;
    sourceArray1[14] = (byte) 103;
    sourceArray1[37] = (byte) 99;
    sourceArray1[40] = (byte) 9;
    sourceArray1[9] = (byte) 104;
    sourceArray1[34] = (byte) 86;
    sourceArray1[35] = (byte) 167;
    sourceArray1[24] = (byte) 168;
    sourceArray1[1] = (byte) 141;
    sourceArray1[47] = (byte) 42;
    sourceArray1[39] = (byte) 82;
    sourceArray1[3] = (byte) 75;
    sourceArray1[41] = (byte) 107;
    sourceArray1[28] = (byte) 42;
    sourceArray1[43] = (byte) 211;
    sourceArray1[36] = (byte) 224 /*0xE0*/;
    sourceArray1[45] = (byte) 21;
    sourceArray1[46] = (byte) 211;
    sourceArray1[12] = (byte) 237;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 92,
      (byte) 43,
      (byte) 45,
      (byte) 116,
      (byte) 168,
      (byte) 123,
      (byte) 45,
      (byte) 79,
      (byte) 207,
      (byte) 20,
      (byte) 65,
      (byte) 92,
      (byte) 178,
      (byte) 151,
      (byte) 94,
      (byte) 14,
      (byte) 241,
      (byte) 142,
      (byte) 117,
      (byte) 238,
      (byte) 178,
      (byte) 129,
      (byte) 155,
      (byte) 204,
      (byte) 6,
      (byte) 110,
      (byte) 132,
      (byte) 155,
      (byte) 47,
      (byte) 250,
      (byte) 52,
      (byte) 33,
      (byte) 56,
      (byte) 115,
      (byte) 81,
      (byte) 196,
      (byte) 62,
      (byte) 155,
      (byte) 177,
      (byte) 188,
      (byte) 172,
      (byte) 126,
      (byte) 212,
      (byte) 192 /*0xC0*/,
      (byte) 217,
      (byte) 151,
      (byte) 70,
      (byte) 96 /*0x60*/
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 366, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[48 /*0x30*/];
    byte[] response2 = new byte[48 /*0x30*/];
    Array.Copy((Array) sc_6361.sspq, 0, (Array) numArray2, 0, 48 /*0x30*/);
    key.Query(true, 366, numArray2, response2);
    Array.Copy((Array) sc_6361.sspr, 0, (Array) numArray2, 0, 48 /*0x30*/);
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
}
