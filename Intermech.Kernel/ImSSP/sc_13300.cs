// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_13300
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_13300
{
  private static byte[] sspq = new byte[49]
  {
    (byte) 109,
    (byte) 170,
    (byte) 16 /*0x10*/,
    (byte) 130,
    (byte) 191,
    (byte) 87,
    (byte) 9,
    (byte) 209,
    (byte) 191,
    (byte) 179,
    (byte) 224 /*0xE0*/,
    (byte) 59,
    (byte) 49,
    (byte) 39,
    (byte) 47,
    (byte) 124,
    (byte) 137,
    (byte) 166,
    (byte) 225,
    (byte) 94,
    (byte) 148,
    (byte) 127 /*0x7F*/,
    (byte) 153,
    (byte) 59,
    (byte) 94,
    (byte) 48 /*0x30*/,
    (byte) 206,
    (byte) 241,
    (byte) 44,
    (byte) 108,
    (byte) 14,
    (byte) 98,
    (byte) 211,
    (byte) 121,
    (byte) 183,
    (byte) 11,
    (byte) 2,
    (byte) 26,
    (byte) 236,
    (byte) 252,
    (byte) 107,
    (byte) 110,
    (byte) 58,
    (byte) 158,
    (byte) 85,
    (byte) 239,
    (byte) 219,
    (byte) 254,
    (byte) 171
  };
  private static byte[] sspr = new byte[49]
  {
    (byte) 175,
    (byte) 172,
    (byte) 88,
    (byte) 229,
    (byte) 132,
    (byte) 201,
    (byte) 108,
    (byte) 190,
    (byte) 96 /*0x60*/,
    (byte) 151,
    (byte) 178,
    (byte) 84,
    (byte) 89,
    (byte) 253,
    (byte) 122,
    (byte) 37,
    (byte) 101,
    (byte) 218,
    (byte) 64 /*0x40*/,
    (byte) 120,
    (byte) 80 /*0x50*/,
    (byte) 138,
    (byte) 176 /*0xB0*/,
    (byte) 99,
    (byte) 232,
    (byte) 164,
    (byte) 60,
    (byte) 47,
    (byte) 64 /*0x40*/,
    (byte) 214,
    (byte) 123,
    (byte) 19,
    (byte) 158,
    (byte) 145,
    (byte) 31 /*0x1F*/,
    (byte) 214,
    (byte) 109,
    (byte) 197,
    (byte) 102,
    (byte) 138,
    (byte) 45,
    (byte) 1,
    (byte) 34,
    (byte) 124,
    (byte) 56,
    (byte) 151,
    (byte) 83,
    (byte) 163,
    (byte) 99
  };

  internal static int ssp_appserver_13301(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[40] = (byte) 1;
    sourceArray1[32 /*0x20*/] = (byte) 88;
    sourceArray1[10] = (byte) 131;
    sourceArray1[3] = (byte) 59;
    sourceArray1[18] = (byte) 41;
    sourceArray1[30] = (byte) 204;
    sourceArray1[31 /*0x1F*/] = (byte) 245;
    sourceArray1[7] = (byte) 136;
    sourceArray1[14] = (byte) 104;
    sourceArray1[1] = (byte) 43;
    sourceArray1[12] = (byte) 235;
    sourceArray1[11] = (byte) 92;
    sourceArray1[28] = (byte) 101;
    sourceArray1[13] = (byte) 130;
    sourceArray1[26] = (byte) 76;
    sourceArray1[21] = (byte) 164;
    sourceArray1[0] = (byte) 161;
    sourceArray1[17] = (byte) 89;
    sourceArray1[6] = (byte) 46;
    sourceArray1[19] = (byte) 150;
    sourceArray1[45] = (byte) 165;
    sourceArray1[46] = (byte) 31 /*0x1F*/;
    sourceArray1[44] = (byte) 99;
    sourceArray1[23] = (byte) 173;
    sourceArray1[24] = (byte) 134;
    sourceArray1[25] = (byte) 14;
    sourceArray1[4] = (byte) 118;
    sourceArray1[27] = (byte) 174;
    sourceArray1[9] = (byte) 53;
    sourceArray1[5] = (byte) 146;
    sourceArray1[38] = (byte) 77;
    sourceArray1[43] = (byte) 181;
    sourceArray1[22] = (byte) 85;
    sourceArray1[16 /*0x10*/] = (byte) 230;
    sourceArray1[34] = (byte) 150;
    sourceArray1[37] = (byte) 141;
    sourceArray1[20] = (byte) 217;
    sourceArray1[41] = (byte) 47;
    sourceArray1[35] = (byte) 162;
    sourceArray1[39] = (byte) 219;
    sourceArray1[36] = (byte) 76;
    sourceArray1[15] = (byte) 173;
    sourceArray1[42] = (byte) 249;
    sourceArray1[8] = (byte) 196;
    sourceArray1[33] = (byte) 194;
    sourceArray1[2] = (byte) 201;
    sourceArray1[29] = (byte) 200;
    sourceArray1[47] = (byte) 243;
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[41] = (byte) 193;
    sourceArray2[24] = (byte) 153;
    sourceArray2[2] = (byte) 139;
    sourceArray2[3] = (byte) 229;
    sourceArray2[12] = (byte) 18;
    sourceArray2[30] = (byte) 190;
    sourceArray2[6] = (byte) 190;
    sourceArray2[23] = (byte) 37;
    sourceArray2[8] = (byte) 216;
    sourceArray2[29] = (byte) 246;
    sourceArray2[10] = (byte) 174;
    sourceArray2[9] = (byte) 150;
    sourceArray2[36] = (byte) 181;
    sourceArray2[13] = (byte) 140;
    sourceArray2[35] = (byte) 105;
    sourceArray2[15] = (byte) 48 /*0x30*/;
    sourceArray2[16 /*0x10*/] = (byte) 183;
    sourceArray2[39] = (byte) 209;
    sourceArray2[18] = (byte) 75;
    sourceArray2[44] = (byte) 222;
    sourceArray2[31 /*0x1F*/] = (byte) 28;
    sourceArray2[21] = (byte) 196;
    sourceArray2[25] = (byte) 217;
    sourceArray2[14] = (byte) 197;
    sourceArray2[42] = (byte) 186;
    sourceArray2[26] = (byte) 246;
    sourceArray2[0] = (byte) 166;
    sourceArray2[27] = (byte) 216;
    sourceArray2[5] = (byte) 20;
    sourceArray2[19] = (byte) 167;
    sourceArray2[4] = (byte) 38;
    sourceArray2[7] = (byte) 163;
    sourceArray2[32 /*0x20*/] = (byte) 61;
    sourceArray2[33] = (byte) 239;
    sourceArray2[34] = (byte) 40;
    sourceArray2[22] = (byte) 252;
    sourceArray2[37] = (byte) 51;
    sourceArray2[28] = (byte) 238;
    sourceArray2[38] = (byte) 118;
    sourceArray2[1] = (byte) 48 /*0x30*/;
    sourceArray2[20] = (byte) 89;
    sourceArray2[17] = (byte) 53;
    sourceArray2[40] = (byte) 152;
    sourceArray2[43] = (byte) 235;
    sourceArray2[11] = (byte) 113;
    sourceArray2[45] = (byte) 120;
    sourceArray2[46] = (byte) 52;
    sourceArray2[47] = (byte) 169;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[49];
    byte[] response2 = new byte[49];
    Array.Copy((Array) sc_13300.sspq, 0, (Array) numArray2, 0, 49);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_13300.sspr, 0, (Array) numArray2, 0, 49);
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
