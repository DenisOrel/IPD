// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_900
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_900
{
  internal static string ssp_avs_901()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 7)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20];
      numArray2[19] = (byte) 159;
      numArray2[1] = (byte) 80 /*0x50*/;
      numArray2[2] = (byte) 96 /*0x60*/;
      numArray2[17] = (byte) 10;
      numArray2[4] = (byte) 55;
      numArray2[3] = (byte) 220;
      numArray2[9] = (byte) 243;
      numArray2[7] = (byte) 33;
      numArray2[8] = (byte) 116;
      numArray2[12] = (byte) 138;
      numArray2[11] = (byte) 198;
      numArray2[6] = (byte) 53;
      numArray2[13] = (byte) 95;
      numArray2[0] = (byte) 207;
      numArray2[10] = (byte) 164;
      numArray2[15] = (byte) 242;
      numArray2[5] = (byte) 28;
      numArray2[16 /*0x10*/] = (byte) 104;
      numArray2[18] = (byte) 80 /*0x50*/;
      numArray2[14] = (byte) 199;
      byte[] numArray3 = new byte[20];
      numArray3[3] = (byte) 112 /*0x70*/;
      numArray3[18] = (byte) 205;
      numArray3[2] = (byte) 160 /*0xA0*/;
      numArray3[12] = (byte) 60;
      numArray3[4] = (byte) 101;
      numArray3[15] = (byte) 16 /*0x10*/;
      numArray3[13] = (byte) 111;
      numArray3[17] = (byte) 154;
      numArray3[5] = (byte) 16 /*0x10*/;
      numArray3[9] = (byte) 102;
      numArray3[10] = byte.MaxValue;
      numArray3[11] = (byte) 122;
      numArray3[8] = (byte) 17;
      numArray3[7] = (byte) 32 /*0x20*/;
      numArray3[6] = (byte) 125;
      numArray3[14] = (byte) 209;
      numArray3[1] = (byte) 15;
      numArray3[16 /*0x10*/] = (byte) 44;
      numArray3[0] = (byte) 126;
      numArray3[19] = (byte) 174;
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[4] = (byte) 165;
    numArray5[7] = (byte) 58;
    numArray5[14] = (byte) 71;
    numArray5[6] = (byte) 185;
    numArray5[9] = (byte) 122;
    numArray5[5] = (byte) 168;
    numArray5[19] = (byte) 77;
    numArray5[2] = (byte) 212;
    numArray5[13] = (byte) 250;
    numArray5[1] = (byte) 51;
    numArray5[10] = (byte) 233;
    numArray5[11] = (byte) 60;
    numArray5[12] = (byte) 41;
    numArray5[8] = (byte) 43;
    numArray5[17] = (byte) 10;
    numArray5[15] = (byte) 181;
    numArray5[16 /*0x10*/] = (byte) 39;
    numArray5[0] = (byte) 107;
    numArray5[18] = (byte) 172;
    numArray5[3] = (byte) 220;
    byte[] numArray6 = new byte[20]
    {
      (byte) 18,
      (byte) 76,
      (byte) 210,
      (byte) 109,
      (byte) 205,
      (byte) 210,
      (byte) 232,
      (byte) 73,
      (byte) 8,
      (byte) 190,
      (byte) 92,
      (byte) 36,
      (byte) 73,
      (byte) 183,
      byte.MaxValue,
      (byte) 205,
      (byte) 216,
      (byte) 63 /*0x3F*/,
      (byte) 239,
      (byte) 236
    };
    key.Query(true, 339, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
