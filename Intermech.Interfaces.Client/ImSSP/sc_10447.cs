// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_10447
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_10447
{
  private static byte[] sspq = new byte[43]
  {
    (byte) 117,
    (byte) 79,
    (byte) 100,
    (byte) 94,
    (byte) 3,
    (byte) 231,
    (byte) 146,
    (byte) 174,
    (byte) 34,
    (byte) 227,
    (byte) 31 /*0x1F*/,
    (byte) 92,
    (byte) 208 /*0xD0*/,
    (byte) 102,
    (byte) 114,
    (byte) 156,
    (byte) 225,
    (byte) 233,
    (byte) 70,
    (byte) 232,
    (byte) 83,
    (byte) 217,
    (byte) 137,
    (byte) 202,
    (byte) 33,
    (byte) 203,
    (byte) 90,
    (byte) 155,
    (byte) 223,
    (byte) 82,
    (byte) 139,
    (byte) 75,
    (byte) 254,
    (byte) 135,
    (byte) 24,
    (byte) 214,
    (byte) 221,
    (byte) 105,
    (byte) 4,
    (byte) 202,
    (byte) 247,
    (byte) 46,
    (byte) 232
  };
  private static byte[] sspr = new byte[43]
  {
    (byte) 40,
    (byte) 64 /*0x40*/,
    (byte) 43,
    (byte) 130,
    (byte) 203,
    (byte) 161,
    (byte) 51,
    byte.MaxValue,
    (byte) 118,
    (byte) 55,
    (byte) 195,
    (byte) 154,
    (byte) 157,
    (byte) 89,
    (byte) 126,
    (byte) 167,
    (byte) 55,
    (byte) 42,
    (byte) 106,
    (byte) 89,
    (byte) 84,
    (byte) 53,
    (byte) 148,
    (byte) 253,
    (byte) 26,
    (byte) 30,
    (byte) 1,
    (byte) 146,
    (byte) 105,
    (byte) 35,
    (byte) 43,
    (byte) 242,
    (byte) 117,
    (byte) 71,
    (byte) 164,
    (byte) 44,
    (byte) 200,
    (byte) 154,
    (byte) 58,
    (byte) 102,
    (byte) 240 /*0xF0*/,
    (byte) 215,
    (byte) 182
  };

  internal static int ssp_appserver_10448(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[19] = (byte) 109;
    sourceArray1[1] = (byte) 58;
    sourceArray1[44] = (byte) 111;
    sourceArray1[43] = (byte) 60;
    sourceArray1[11] = (byte) 61;
    sourceArray1[33] = (byte) 141;
    sourceArray1[8] = (byte) 245;
    sourceArray1[0] = (byte) 212;
    sourceArray1[35] = (byte) 94;
    sourceArray1[26] = (byte) 4;
    sourceArray1[10] = (byte) 244;
    sourceArray1[14] = (byte) 117;
    sourceArray1[40] = (byte) 80 /*0x50*/;
    sourceArray1[4] = (byte) 214;
    sourceArray1[2] = (byte) 89;
    sourceArray1[41] = (byte) 175;
    sourceArray1[16 /*0x10*/] = (byte) 128 /*0x80*/;
    sourceArray1[17] = (byte) 34;
    sourceArray1[18] = (byte) 161;
    sourceArray1[9] = (byte) 201;
    sourceArray1[20] = (byte) 105;
    sourceArray1[3] = (byte) 11;
    sourceArray1[27] = (byte) 106;
    sourceArray1[23] = (byte) 43;
    sourceArray1[24] = (byte) 229;
    sourceArray1[22] = (byte) 191;
    sourceArray1[30] = (byte) 93;
    sourceArray1[7] = (byte) 198;
    sourceArray1[28] = (byte) 9;
    sourceArray1[29] = (byte) 58;
    sourceArray1[37] = (byte) 124;
    sourceArray1[31 /*0x1F*/] = (byte) 182;
    sourceArray1[32 /*0x20*/] = (byte) 103;
    sourceArray1[34] = (byte) 190;
    sourceArray1[47] = (byte) 73;
    sourceArray1[21] = (byte) 210;
    sourceArray1[15] = (byte) 36;
    sourceArray1[5] = (byte) 202;
    sourceArray1[38] = (byte) 60;
    sourceArray1[39] = (byte) 33;
    sourceArray1[12] = (byte) 138;
    sourceArray1[46] = (byte) 242;
    sourceArray1[25] = (byte) 242;
    sourceArray1[42] = (byte) 194;
    sourceArray1[6] = (byte) 223;
    sourceArray1[45] = (byte) 239;
    sourceArray1[36] = (byte) 252;
    sourceArray1[13] = (byte) 207;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[24] = (byte) 24;
    sourceArray2[43] = (byte) 46;
    sourceArray2[1] = (byte) 113;
    sourceArray2[25] = (byte) 197;
    sourceArray2[4] = (byte) 85;
    sourceArray2[5] = (byte) 68;
    sourceArray2[7] = (byte) 49;
    sourceArray2[36] = (byte) 1;
    sourceArray2[8] = (byte) 219;
    sourceArray2[17] = (byte) 238;
    sourceArray2[28] = (byte) 43;
    sourceArray2[33] = (byte) 216;
    sourceArray2[2] = (byte) 44;
    sourceArray2[13] = (byte) 236;
    sourceArray2[9] = (byte) 174;
    sourceArray2[15] = (byte) 92;
    sourceArray2[42] = (byte) 118;
    sourceArray2[12] = (byte) 16 /*0x10*/;
    sourceArray2[32 /*0x20*/] = (byte) 149;
    sourceArray2[19] = (byte) 155;
    sourceArray2[37] = (byte) 135;
    sourceArray2[3] = (byte) 187;
    sourceArray2[22] = (byte) 90;
    sourceArray2[23] = (byte) 14;
    sourceArray2[47] = (byte) 244;
    sourceArray2[21] = (byte) 185;
    sourceArray2[26] = (byte) 117;
    sourceArray2[29] = (byte) 27;
    sourceArray2[11] = (byte) 235;
    sourceArray2[10] = (byte) 247;
    sourceArray2[30] = (byte) 65;
    sourceArray2[31 /*0x1F*/] = (byte) 119;
    sourceArray2[0] = (byte) 210;
    sourceArray2[34] = (byte) 242;
    sourceArray2[6] = (byte) 32 /*0x20*/;
    sourceArray2[16 /*0x10*/] = (byte) 54;
    sourceArray2[35] = (byte) 199;
    sourceArray2[14] = (byte) 141;
    sourceArray2[20] = (byte) 211;
    sourceArray2[39] = (byte) 84;
    sourceArray2[40] = (byte) 185;
    sourceArray2[41] = (byte) 228;
    sourceArray2[27] = (byte) 81;
    sourceArray2[18] = (byte) 1;
    sourceArray2[44] = (byte) 34;
    sourceArray2[45] = (byte) 58;
    sourceArray2[46] = (byte) 81;
    sourceArray2[38] = (byte) 115;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[43];
    byte[] response2 = new byte[43];
    Array.Copy((Array) sc_10447.sspq, 0, (Array) numArray2, 0, 43);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_10447.sspr, 0, (Array) numArray2, 0, 43);
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
