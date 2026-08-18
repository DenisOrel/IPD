// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_967
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_967
{
  internal static string ssp_avs_968()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[20];
      byte[] numArray2 = new byte[20]
      {
        (byte) 250,
        (byte) 9,
        (byte) 2,
        (byte) 164,
        (byte) 52,
        (byte) 144 /*0x90*/,
        (byte) 108,
        (byte) 190,
        (byte) 69,
        (byte) 158,
        (byte) 113,
        (byte) 88,
        (byte) 54,
        (byte) 71,
        (byte) 160 /*0xA0*/,
        (byte) 33,
        (byte) 229,
        (byte) 130,
        (byte) 177,
        (byte) 60
      };
      byte[] numArray3 = new byte[20]
      {
        (byte) 115,
        (byte) 82,
        (byte) 35,
        (byte) 135,
        (byte) 112 /*0x70*/,
        (byte) 118,
        (byte) 145,
        (byte) 249,
        (byte) 198,
        (byte) 249,
        (byte) 18,
        (byte) 23,
        (byte) 53,
        (byte) 74,
        (byte) 7,
        (byte) 23,
        (byte) 179,
        (byte) 200,
        (byte) 94,
        (byte) 81
      };
      key.Query(true, 339, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 20);
      for (int index = 0; index < 20; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[20];
    byte[] numArray5 = new byte[20];
    numArray5[19] = (byte) 150;
    numArray5[1] = (byte) 95;
    numArray5[2] = (byte) 185;
    numArray5[17] = (byte) 188;
    numArray5[18] = (byte) 19;
    numArray5[7] = (byte) 94;
    numArray5[5] = (byte) 133;
    numArray5[0] = (byte) 28;
    numArray5[8] = (byte) 166;
    numArray5[9] = (byte) 215;
    numArray5[13] = (byte) 123;
    numArray5[11] = (byte) 253;
    numArray5[12] = (byte) 116;
    numArray5[10] = (byte) 247;
    numArray5[14] = (byte) 15;
    numArray5[15] = (byte) 244;
    numArray5[16 /*0x10*/] = (byte) 45;
    numArray5[6] = (byte) 221;
    numArray5[4] = (byte) 46;
    numArray5[3] = (byte) 47;
    byte[] numArray6 = new byte[20]
    {
      (byte) 208 /*0xD0*/,
      (byte) 131,
      (byte) 26,
      (byte) 15,
      (byte) 41,
      (byte) 253,
      (byte) 47,
      (byte) 93,
      (byte) 88,
      (byte) 244,
      (byte) 204,
      (byte) 213,
      (byte) 186,
      (byte) 87,
      (byte) 193,
      (byte) 106,
      (byte) 210,
      (byte) 187,
      (byte) 219,
      (byte) 21
    };
    key.Query(true, 339, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 20);
    for (int index = 0; index < 20; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
