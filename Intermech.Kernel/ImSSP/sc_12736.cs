// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12736
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;


namespace ImSSP;

internal static class sc_12736
{
  internal static int ssp_appserver_12737(int k)
  {
    IProtectionKey key = ProtectionService.Key;
    byte[] numArray = new byte[4];
    byte[] response = new byte[4];
    byte[] sourceArray1 = new byte[48 /*0x30*/]
    {
      (byte) 87,
      (byte) 74,
      (byte) 217,
      (byte) 18,
      (byte) 45,
      (byte) 104,
      (byte) 248,
      (byte) 121,
      (byte) 33,
      (byte) 98,
      (byte) 196,
      (byte) 155,
      (byte) 164,
      (byte) 30,
      (byte) 179,
      (byte) 252,
      (byte) 127 /*0x7F*/,
      (byte) 27,
      (byte) 20,
      (byte) 177,
      (byte) 182,
      (byte) 103,
      (byte) 172,
      (byte) 20,
      (byte) 73,
      (byte) 101,
      (byte) 59,
      (byte) 241,
      (byte) 65,
      (byte) 66,
      (byte) 164,
      (byte) 63 /*0x3F*/,
      (byte) 5,
      (byte) 155,
      (byte) 185,
      (byte) 168,
      (byte) 37,
      (byte) 243,
      (byte) 153,
      (byte) 236,
      (byte) 56,
      (byte) 105,
      (byte) 95,
      (byte) 35,
      (byte) 198,
      (byte) 81,
      (byte) 7,
      (byte) 210
    };
    byte[] sourceArray2 = new byte[48 /*0x30*/];
    sourceArray2[42] = (byte) 205;
    sourceArray2[36] = (byte) 178;
    sourceArray2[8] = (byte) 78;
    sourceArray2[3] = (byte) 160 /*0xA0*/;
    sourceArray2[0] = (byte) 177;
    sourceArray2[21] = (byte) 169;
    sourceArray2[6] = (byte) 66;
    sourceArray2[7] = (byte) 100;
    sourceArray2[17] = (byte) 81;
    sourceArray2[5] = (byte) 147;
    sourceArray2[10] = (byte) 159;
    sourceArray2[16 /*0x10*/] = (byte) 203;
    sourceArray2[12] = (byte) 240 /*0xF0*/;
    sourceArray2[27] = (byte) 80 /*0x50*/;
    sourceArray2[46] = (byte) 104;
    sourceArray2[15] = (byte) 120;
    sourceArray2[26] = (byte) 226;
    sourceArray2[1] = (byte) 222;
    sourceArray2[18] = (byte) 10;
    sourceArray2[20] = (byte) 147;
    sourceArray2[25] = (byte) 199;
    sourceArray2[32 /*0x20*/] = (byte) 170;
    sourceArray2[22] = (byte) 115;
    sourceArray2[4] = (byte) 8;
    sourceArray2[31 /*0x1F*/] = (byte) 197;
    sourceArray2[23] = (byte) 201;
    sourceArray2[29] = (byte) 199;
    sourceArray2[34] = (byte) 22;
    sourceArray2[13] = (byte) 151;
    sourceArray2[33] = (byte) 3;
    sourceArray2[30] = (byte) 66;
    sourceArray2[41] = (byte) 104;
    sourceArray2[19] = (byte) 10;
    sourceArray2[44] = (byte) 153;
    sourceArray2[2] = (byte) 74;
    sourceArray2[35] = (byte) 115;
    sourceArray2[38] = (byte) 144 /*0x90*/;
    sourceArray2[39] = (byte) 161;
    sourceArray2[43] = (byte) 81;
    sourceArray2[28] = (byte) 89;
    sourceArray2[40] = (byte) 151;
    sourceArray2[14] = (byte) 160 /*0xA0*/;
    sourceArray2[24] = (byte) 166;
    sourceArray2[9] = (byte) 70;
    sourceArray2[37] = (byte) 138;
    sourceArray2[45] = byte.MaxValue;
    sourceArray2[11] = (byte) 17;
    sourceArray2[47] = (byte) 23;
    Array.Copy((Array) sourceArray1, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    key.Query(true, 335, numArray, response);
    Array.Copy((Array) sourceArray2, (DateTime.Now.Month - 1) * 4, (Array) numArray, 0, 4);
    return BitConverter.ToInt32(response, 0) ^ BitConverter.ToInt32(numArray, 0) ^ k;
  }
}
