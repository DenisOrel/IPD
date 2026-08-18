// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19740
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19740
{
  internal static string ssp_techcard_19741()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 12)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[13] = (byte) 117;
      numArray2[18] = (byte) 100;
      numArray2[12] = (byte) 224 /*0xE0*/;
      numArray2[3] = (byte) 253;
      numArray2[6] = (byte) 68;
      numArray2[5] = (byte) 106;
      numArray2[0] = (byte) 34;
      numArray2[1] = (byte) 215;
      numArray2[8] = (byte) 91;
      numArray2[9] = (byte) 95;
      numArray2[14] = (byte) 2;
      numArray2[11] = (byte) 228;
      numArray2[10] = (byte) 182;
      numArray2[4] = (byte) 199;
      numArray2[15] = (byte) 164;
      numArray2[7] = (byte) 21;
      numArray2[16 /*0x10*/] = (byte) 1;
      numArray2[2] = (byte) 76;
      numArray2[17] = (byte) 49;
      byte[] numArray3 = new byte[19];
      numArray3[10] = (byte) 101;
      numArray3[11] = (byte) 59;
      numArray3[2] = (byte) 75;
      numArray3[13] = (byte) 130;
      numArray3[7] = (byte) 120;
      numArray3[9] = (byte) 252;
      numArray3[6] = (byte) 235;
      numArray3[8] = (byte) 69;
      numArray3[1] = (byte) 69;
      numArray3[3] = (byte) 226;
      numArray3[0] = (byte) 143;
      numArray3[5] = (byte) 108;
      numArray3[12] = (byte) 240 /*0xF0*/;
      numArray3[4] = (byte) 250;
      numArray3[16 /*0x10*/] = (byte) 129;
      numArray3[15] = (byte) 171;
      numArray3[14] = (byte) 141;
      numArray3[17] = (byte) 53;
      numArray3[18] = (byte) 174;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 63 /*0x3F*/,
      (byte) 242,
      (byte) 51,
      (byte) 91,
      (byte) 171,
      (byte) 76,
      (byte) 233,
      (byte) 223,
      (byte) 59,
      (byte) 87,
      (byte) 166,
      (byte) 158,
      (byte) 149,
      (byte) 205,
      (byte) 209,
      (byte) 154,
      (byte) 231,
      (byte) 127 /*0x7F*/,
      (byte) 206
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 106,
      (byte) 122,
      (byte) 55,
      (byte) 94,
      (byte) 121,
      (byte) 78,
      (byte) 133,
      (byte) 24,
      (byte) 178,
      (byte) 156,
      (byte) 163,
      (byte) 239,
      (byte) 70,
      (byte) 4,
      (byte) 63 /*0x3F*/,
      (byte) 187,
      (byte) 20,
      (byte) 176 /*0xB0*/,
      (byte) 247
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
