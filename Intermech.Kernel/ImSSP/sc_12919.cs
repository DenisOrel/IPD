// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_12919
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Protection;
using System;
using System.Text;


namespace ImSSP;

internal static class sc_12919
{
  internal static string ssp_appserver_12920()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[33];
      byte[] numArray2 = new byte[33];
      numArray2[16 /*0x10*/] = (byte) 103;
      numArray2[9] = (byte) 252;
      numArray2[2] = (byte) 23;
      numArray2[3] = (byte) 227;
      numArray2[19] = (byte) 33;
      numArray2[25] = (byte) 222;
      numArray2[12] = (byte) 203;
      numArray2[0] = (byte) 102;
      numArray2[11] = (byte) 167;
      numArray2[18] = (byte) 169;
      numArray2[21] = (byte) 210;
      numArray2[24] = (byte) 227;
      numArray2[27] = (byte) 111;
      numArray2[7] = (byte) 106;
      numArray2[14] = (byte) 187;
      numArray2[15] = (byte) 10;
      numArray2[26] = (byte) 161;
      numArray2[17] = (byte) 186;
      numArray2[13] = (byte) 139;
      numArray2[10] = (byte) 8;
      numArray2[20] = (byte) 144 /*0x90*/;
      numArray2[1] = (byte) 44;
      numArray2[22] = (byte) 212;
      numArray2[23] = (byte) 122;
      numArray2[4] = (byte) 169;
      numArray2[30] = (byte) 148;
      numArray2[6] = (byte) 115;
      numArray2[8] = (byte) 180;
      numArray2[28] = (byte) 181;
      numArray2[29] = (byte) 93;
      numArray2[5] = (byte) 228;
      numArray2[31 /*0x1F*/] = (byte) 109;
      numArray2[32 /*0x20*/] = (byte) 235;
      byte[] numArray3 = new byte[33]
      {
        (byte) 164,
        (byte) 248,
        (byte) 108,
        (byte) 140,
        (byte) 6,
        (byte) 181,
        (byte) 87,
        (byte) 172,
        (byte) 188,
        (byte) 235,
        (byte) 15,
        (byte) 66,
        (byte) 85,
        (byte) 158,
        (byte) 244,
        (byte) 147,
        (byte) 223,
        (byte) 102,
        (byte) 148,
        (byte) 189,
        (byte) 203,
        (byte) 6,
        (byte) 107,
        (byte) 119,
        (byte) 184,
        (byte) 16 /*0x10*/,
        (byte) 195,
        (byte) 202,
        (byte) 22,
        (byte) 32 /*0x20*/,
        (byte) 192 /*0xC0*/,
        (byte) 239,
        (byte) 24
      };
      key.Query(true, 335, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 33);
      for (int index = 0; index < 33; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[33];
    byte[] numArray5 = new byte[33]
    {
      (byte) 163,
      (byte) 253,
      (byte) 176 /*0xB0*/,
      (byte) 137,
      (byte) 66,
      (byte) 153,
      (byte) 194,
      (byte) 140,
      (byte) 222,
      (byte) 245,
      (byte) 7,
      (byte) 99,
      (byte) 228,
      (byte) 150,
      (byte) 161,
      (byte) 250,
      (byte) 66,
      (byte) 80 /*0x50*/,
      (byte) 23,
      (byte) 176 /*0xB0*/,
      (byte) 95,
      (byte) 192 /*0xC0*/,
      (byte) 111,
      (byte) 182,
      (byte) 102,
      (byte) 35,
      (byte) 132,
      (byte) 25,
      (byte) 238,
      (byte) 142,
      (byte) 233,
      (byte) 32 /*0x20*/,
      (byte) 92
    };
    byte[] numArray6 = new byte[33]
    {
      (byte) 0,
      (byte) 77,
      (byte) 95,
      (byte) 116,
      (byte) 190,
      (byte) 9,
      (byte) 236,
      (byte) 72,
      (byte) 2,
      (byte) 7,
      (byte) 155,
      (byte) 10,
      (byte) 19,
      (byte) 58,
      (byte) 127 /*0x7F*/,
      (byte) 18,
      (byte) 209,
      (byte) 229,
      (byte) 193,
      (byte) 151,
      (byte) 89,
      (byte) 77,
      (byte) 134,
      (byte) 122,
      (byte) 161,
      (byte) 221,
      (byte) 12,
      (byte) 218,
      (byte) 203,
      (byte) 73,
      (byte) 249,
      (byte) 163,
      (byte) 133
    };
    key.Query(true, 335, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 33);
    for (int index = 0; index < 33; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
