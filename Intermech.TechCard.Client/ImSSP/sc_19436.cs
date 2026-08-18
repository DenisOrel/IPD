// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19436
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;

#nullable disable
namespace ImSSP;

internal static class sc_19436
{
  internal static int ssp_techcard_19437(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 40,
      (byte) 0,
      (byte) 90,
      (byte) 159,
      (byte) 243,
      (byte) 194,
      (byte) 58,
      (byte) 172,
      (byte) 141,
      (byte) 73,
      (byte) 179,
      (byte) 199,
      (byte) 17,
      (byte) 14,
      (byte) 219,
      (byte) 20,
      (byte) 0,
      (byte) 42,
      (byte) 253,
      (byte) 171,
      (byte) 31 /*0x1F*/,
      (byte) 41,
      (byte) 63 /*0x3F*/,
      (byte) 44,
      (byte) 5,
      (byte) 163,
      (byte) 231,
      (byte) 75,
      (byte) 106,
      (byte) 253,
      (byte) 42,
      (byte) 233,
      (byte) 9,
      (byte) 215,
      byte.MaxValue,
      (byte) 201,
      (byte) 80 /*0x50*/,
      (byte) 32 /*0x20*/,
      (byte) 151,
      (byte) 160 /*0xA0*/,
      (byte) 62,
      (byte) 125,
      (byte) 242,
      (byte) 145,
      (byte) 59,
      (byte) 47,
      (byte) 163,
      (byte) 254
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[0] = (byte) 86;
    sourceArray2[2] = (byte) 223;
    sourceArray2[25] = (byte) 231;
    sourceArray2[3] = (byte) 111;
    sourceArray2[4] = (byte) 169;
    sourceArray2[5] = (byte) 151;
    sourceArray2[21] = (byte) 252;
    sourceArray2[33] = (byte) 132;
    sourceArray2[8] = (byte) 252;
    sourceArray2[32 /*0x20*/] = (byte) 66;
    sourceArray2[22] = (byte) 28;
    sourceArray2[11] = (byte) 123;
    sourceArray2[12] = (byte) 54;
    sourceArray2[45] = (byte) 204;
    sourceArray2[14] = (byte) 131;
    sourceArray2[15] = (byte) 20;
    sourceArray2[16 /*0x10*/] = (byte) 49;
    sourceArray2[17] = (byte) 126;
    sourceArray2[18] = (byte) 180;
    sourceArray2[1] = (byte) 191;
    sourceArray2[20] = (byte) 151;
    sourceArray2[27] = (byte) 214;
    sourceArray2[13] = (byte) 104;
    sourceArray2[47] = (byte) 23;
    sourceArray2[19] = (byte) 139;
    sourceArray2[9] = (byte) 144 /*0x90*/;
    sourceArray2[24] = (byte) 200;
    sourceArray2[6] = (byte) 168;
    sourceArray2[28] = (byte) 191;
    sourceArray2[29] = (byte) 122;
    sourceArray2[30] = (byte) 66;
    sourceArray2[44] = (byte) 96 /*0x60*/;
    sourceArray2[31 /*0x1F*/] = (byte) 100;
    sourceArray2[42] = (byte) 241;
    sourceArray2[10] = (byte) 216;
    sourceArray2[35] = (byte) 248;
    sourceArray2[23] = (byte) 184;
    sourceArray2[37] = (byte) 81;
    sourceArray2[38] = (byte) 144 /*0x90*/;
    sourceArray2[39] = (byte) 138;
    sourceArray2[40] = (byte) 97;
    sourceArray2[34] = (byte) 110;
    sourceArray2[41] = (byte) 96 /*0x60*/;
    sourceArray2[43] = (byte) 150;
    sourceArray2[7] = (byte) 130;
    sourceArray2[26] = (byte) 189;
    sourceArray2[46] = (byte) 63 /*0x3F*/;
    sourceArray2[36] = (byte) 30;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 359, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
