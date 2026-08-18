// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_7896
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_7896
{
  private static byte[] sspq = new byte[35]
  {
    (byte) 47,
    (byte) 49,
    (byte) 120,
    (byte) 69,
    (byte) 191,
    (byte) 37,
    (byte) 56,
    (byte) 234,
    (byte) 95,
    (byte) 150,
    (byte) 32 /*0x20*/,
    (byte) 202,
    (byte) 190,
    (byte) 17,
    (byte) 253,
    (byte) 172,
    (byte) 239,
    (byte) 204,
    (byte) 130,
    (byte) 13,
    (byte) 37,
    (byte) 132,
    (byte) 15,
    (byte) 160 /*0xA0*/,
    (byte) 193,
    (byte) 154,
    (byte) 221,
    (byte) 24,
    (byte) 230,
    (byte) 130,
    (byte) 201,
    (byte) 60,
    (byte) 50,
    (byte) 73,
    (byte) 28
  };
  private static byte[] sspr = new byte[35]
  {
    (byte) 3,
    (byte) 35,
    (byte) 86,
    (byte) 13,
    (byte) 218,
    (byte) 155,
    (byte) 28,
    (byte) 100,
    (byte) 151,
    (byte) 28,
    (byte) 53,
    (byte) 118,
    (byte) 135,
    (byte) 101,
    (byte) 9,
    (byte) 229,
    (byte) 146,
    (byte) 140,
    (byte) 38,
    (byte) 205,
    (byte) 181,
    (byte) 133,
    (byte) 118,
    (byte) 196,
    (byte) 222,
    (byte) 197,
    (byte) 187,
    (byte) 58,
    (byte) 45,
    (byte) 216,
    (byte) 218,
    (byte) 18,
    (byte) 136,
    (byte) 206,
    (byte) 224 /*0xE0*/
  };

  internal static int ssp_techcard_7897(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 201,
      (byte) 190,
      (byte) 150,
      (byte) 198,
      (byte) 200,
      (byte) 123,
      (byte) 9,
      (byte) 190,
      (byte) 101,
      (byte) 184,
      (byte) 93,
      (byte) 65,
      (byte) 4,
      (byte) 32 /*0x20*/,
      (byte) 89,
      (byte) 193,
      (byte) 199,
      (byte) 66,
      (byte) 108,
      (byte) 62,
      (byte) 95,
      (byte) 132,
      (byte) 254,
      (byte) 112 /*0x70*/,
      (byte) 141,
      (byte) 222,
      (byte) 28,
      (byte) 115,
      (byte) 157,
      (byte) 155,
      (byte) 250,
      (byte) 76,
      (byte) 68,
      (byte) 87,
      (byte) 49,
      (byte) 140,
      (byte) 63 /*0x3F*/,
      (byte) 186,
      (byte) 29,
      (byte) 232,
      (byte) 46,
      (byte) 190,
      (byte) 195,
      (byte) 100,
      (byte) 146,
      (byte) 116,
      (byte) 248,
      (byte) 153
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 150,
      (byte) 74,
      (byte) 129,
      (byte) 3,
      (byte) 48 /*0x30*/,
      (byte) 246,
      (byte) 196,
      (byte) 71,
      (byte) 223,
      (byte) 120,
      (byte) 76,
      (byte) 150,
      (byte) 76,
      (byte) 203,
      (byte) 45,
      (byte) 231,
      (byte) 189,
      (byte) 94,
      (byte) 22,
      (byte) 10,
      (byte) 29,
      (byte) 155,
      (byte) 181,
      (byte) 3,
      (byte) 35,
      (byte) 136,
      (byte) 239,
      (byte) 236,
      (byte) 215,
      (byte) 89,
      (byte) 207,
      (byte) 210,
      (byte) 34,
      (byte) 213,
      (byte) 149,
      (byte) 21,
      (byte) 83,
      (byte) 25,
      (byte) 144 /*0x90*/,
      (byte) 236,
      (byte) 17,
      (byte) 5,
      (byte) 166,
      (byte) 229,
      (byte) 233,
      (byte) 112 /*0x70*/,
      (byte) 139,
      (byte) 71
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 359, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[35];
    byte[] response2 = new byte[35];
    Array.Copy((Array) sc_7896.sspq, 0, (Array) numArray2, 0, 35);
    key.Query(true, 359, numArray2, response2);
    Array.Copy((Array) sc_7896.sspr, 0, (Array) numArray2, 0, 35);
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

  internal static int ssp_techcard_7898(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[21] = (byte) 132;
    sourceArray1[1] = (byte) 204;
    sourceArray1[29] = (byte) 151;
    sourceArray1[24] = (byte) 31 /*0x1F*/;
    sourceArray1[13] = (byte) 169;
    sourceArray1[5] = (byte) 137;
    sourceArray1[28] = (byte) 228;
    sourceArray1[7] = (byte) 157;
    sourceArray1[35] = byte.MaxValue;
    sourceArray1[0] = (byte) 235;
    sourceArray1[2] = (byte) 25;
    sourceArray1[41] = (byte) 2;
    sourceArray1[25] = (byte) 3;
    sourceArray1[46] = (byte) 154;
    sourceArray1[14] = (byte) 220;
    sourceArray1[10] = (byte) 129;
    sourceArray1[16 /*0x10*/] = (byte) 92;
    sourceArray1[4] = (byte) 51;
    sourceArray1[3] = (byte) 129;
    sourceArray1[43] = (byte) 244;
    sourceArray1[20] = (byte) 215;
    sourceArray1[18] = (byte) 170;
    sourceArray1[22] = (byte) 37;
    sourceArray1[26] = (byte) 220;
    sourceArray1[30] = (byte) 116;
    sourceArray1[11] = (byte) 119;
    sourceArray1[19] = (byte) 33;
    sourceArray1[15] = (byte) 222;
    sourceArray1[9] = (byte) 165;
    sourceArray1[31 /*0x1F*/] = (byte) 244;
    sourceArray1[8] = (byte) 249;
    sourceArray1[17] = (byte) 139;
    sourceArray1[32 /*0x20*/] = (byte) 233;
    sourceArray1[33] = (byte) 204;
    sourceArray1[34] = (byte) 4;
    sourceArray1[12] = (byte) 75;
    sourceArray1[36] = (byte) 104;
    sourceArray1[37] = (byte) 140;
    sourceArray1[6] = (byte) 152;
    sourceArray1[39] = (byte) 251;
    sourceArray1[40] = (byte) 119;
    sourceArray1[45] = (byte) 161;
    sourceArray1[42] = (byte) 199;
    sourceArray1[38] = (byte) 242;
    sourceArray1[44] = (byte) 92;
    sourceArray1[47] = (byte) 217;
    sourceArray1[27] = (byte) 204;
    sourceArray1[23] = (byte) 72;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 126,
      (byte) 2,
      (byte) 193,
      (byte) 16 /*0x10*/,
      (byte) 209,
      (byte) 27,
      (byte) 63 /*0x3F*/,
      (byte) 152,
      (byte) 183,
      (byte) 77,
      (byte) 184,
      (byte) 182,
      (byte) 69,
      (byte) 8,
      (byte) 40,
      (byte) 185,
      (byte) 159,
      (byte) 8,
      (byte) 240 /*0xF0*/,
      (byte) 200,
      (byte) 67,
      (byte) 215,
      (byte) 205,
      (byte) 142,
      (byte) 135,
      (byte) 186,
      (byte) 185,
      (byte) 49,
      (byte) 170,
      (byte) 179,
      (byte) 95,
      (byte) 113,
      (byte) 91,
      (byte) 147,
      (byte) 90,
      (byte) 108,
      (byte) 2,
      (byte) 180,
      (byte) 171,
      (byte) 8,
      (byte) 60,
      (byte) 124,
      (byte) 74,
      (byte) 63 /*0x3F*/,
      (byte) 67,
      (byte) 204,
      (byte) 230,
      (byte) 223
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
