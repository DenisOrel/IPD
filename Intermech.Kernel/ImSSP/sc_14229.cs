// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_14229
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_14229
{
  private static byte[] sspq = new byte[36]
  {
    (byte) 118,
    (byte) 82,
    (byte) 45,
    (byte) 184,
    (byte) 207,
    (byte) 230,
    (byte) 196,
    (byte) 29,
    (byte) 221,
    (byte) 227,
    (byte) 80 /*0x50*/,
    (byte) 101,
    (byte) 60,
    (byte) 213,
    (byte) 113,
    (byte) 44,
    (byte) 35,
    (byte) 32 /*0x20*/,
    (byte) 143,
    (byte) 64 /*0x40*/,
    (byte) 236,
    (byte) 188,
    (byte) 242,
    (byte) 78,
    (byte) 111,
    (byte) 39,
    (byte) 14,
    (byte) 18,
    (byte) 76,
    (byte) 218,
    (byte) 112 /*0x70*/,
    (byte) 148,
    (byte) 26,
    (byte) 98,
    (byte) 193,
    (byte) 232
  };
  private static byte[] sspr = new byte[36]
  {
    (byte) 140,
    (byte) 149,
    (byte) 203,
    (byte) 239,
    (byte) 189,
    (byte) 206,
    (byte) 98,
    (byte) 118,
    (byte) 141,
    (byte) 188,
    (byte) 194,
    (byte) 232,
    (byte) 90,
    (byte) 71,
    (byte) 99,
    (byte) 100,
    (byte) 251,
    (byte) 167,
    (byte) 84,
    (byte) 147,
    (byte) 6,
    (byte) 71,
    (byte) 57,
    (byte) 177,
    (byte) 125,
    (byte) 214,
    (byte) 209,
    (byte) 170,
    (byte) 154,
    (byte) 117,
    (byte) 189,
    (byte) 219,
    (byte) 224 /*0xE0*/,
    (byte) 56,
    (byte) 166,
    (byte) 113
  };

  internal static int ssp_appserver_14230(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray1 = new byte[4];
    byte[] response1 = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/];
    sourceArray1[24] = (byte) 253;
    sourceArray1[27] = (byte) 89;
    sourceArray1[31 /*0x1F*/] = (byte) 65;
    sourceArray1[3] = (byte) 11;
    sourceArray1[39] = (byte) 34;
    sourceArray1[5] = (byte) 10;
    sourceArray1[6] = (byte) 110;
    sourceArray1[40] = (byte) 60;
    sourceArray1[8] = (byte) 141;
    sourceArray1[9] = (byte) 251;
    sourceArray1[22] = (byte) 133;
    sourceArray1[19] = (byte) 61;
    sourceArray1[4] = (byte) 127 /*0x7F*/;
    sourceArray1[38] = (byte) 199;
    sourceArray1[14] = (byte) 147;
    sourceArray1[15] = (byte) 3;
    sourceArray1[12] = (byte) 178;
    sourceArray1[32 /*0x20*/] = (byte) 113;
    sourceArray1[18] = (byte) 122;
    sourceArray1[46] = (byte) 152;
    sourceArray1[0] = (byte) 56;
    sourceArray1[7] = (byte) 43;
    sourceArray1[2] = (byte) 223;
    sourceArray1[23] = (byte) 52;
    sourceArray1[16 /*0x10*/] = (byte) 84;
    sourceArray1[25] = (byte) 211;
    sourceArray1[26] = (byte) 243;
    sourceArray1[20] = (byte) 25;
    sourceArray1[29] = (byte) 7;
    sourceArray1[17] = (byte) 243;
    sourceArray1[30] = (byte) 213;
    sourceArray1[11] = (byte) 192 /*0xC0*/;
    sourceArray1[10] = (byte) 250;
    sourceArray1[33] = (byte) 108;
    sourceArray1[34] = (byte) 132;
    sourceArray1[35] = (byte) 227;
    sourceArray1[36] = (byte) 196;
    sourceArray1[1] = (byte) 41;
    sourceArray1[37] = (byte) 41;
    sourceArray1[13] = (byte) 94;
    sourceArray1[21] = (byte) 64 /*0x40*/;
    sourceArray1[41] = (byte) 227;
    sourceArray1[42] = (byte) 153;
    sourceArray1[45] = (byte) 198;
    sourceArray1[44] = (byte) 197;
    sourceArray1[28] = (byte) 13;
    sourceArray1[43] = (byte) 72;
    sourceArray1[47] = (byte) 8;
    byte[] sourceArray2 = new byte[48 /*0x30*/]
    {
      (byte) 226,
      (byte) 134,
      (byte) 181,
      (byte) 80 /*0x50*/,
      (byte) 241,
      (byte) 173,
      (byte) 215,
      (byte) 207,
      (byte) 48 /*0x30*/,
      (byte) 48 /*0x30*/,
      (byte) 21,
      (byte) 121,
      (byte) 231,
      (byte) 129,
      (byte) 83,
      (byte) 23,
      (byte) 106,
      (byte) 179,
      (byte) 212,
      (byte) 18,
      (byte) 31 /*0x1F*/,
      (byte) 140,
      (byte) 240 /*0xF0*/,
      (byte) 205,
      (byte) 108,
      (byte) 27,
      (byte) 97,
      (byte) 131,
      (byte) 135,
      (byte) 98,
      (byte) 133,
      (byte) 5,
      (byte) 26,
      (byte) 35,
      (byte) 127 /*0x7F*/,
      (byte) 204,
      (byte) 20,
      (byte) 177,
      (byte) 78,
      (byte) 230,
      (byte) 58,
      (byte) 226,
      (byte) 141,
      (byte) 16 /*0x10*/,
      (byte) 149,
      (byte) 136,
      (byte) 1,
      (byte) 9
    };
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    key.Query(true, 335, numArray1, response1);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray1, 0, 4);
    byte[] numArray2 = new byte[36];
    byte[] response2 = new byte[36];
    Array.Copy((Array) sc_14229.sspq, 0, (Array) numArray2, 0, 36);
    key.Query(true, 335, numArray2, response2);
    Array.Copy((Array) sc_14229.sspr, 0, (Array) numArray2, 0, 36);
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
