// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19756
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19756
{
  internal static string ssp_techcard_19757()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 10)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19];
      numArray2[6] = (byte) 169;
      numArray2[15] = (byte) 172;
      numArray2[2] = (byte) 208 /*0xD0*/;
      numArray2[3] = (byte) 2;
      numArray2[1] = (byte) 205;
      numArray2[5] = (byte) 180;
      numArray2[14] = (byte) 122;
      numArray2[4] = (byte) 77;
      numArray2[17] = (byte) 123;
      numArray2[9] = (byte) 190;
      numArray2[10] = (byte) 118;
      numArray2[18] = (byte) 127 /*0x7F*/;
      numArray2[12] = (byte) 132;
      numArray2[7] = (byte) 165;
      numArray2[13] = (byte) 11;
      numArray2[8] = (byte) 235;
      numArray2[16 /*0x10*/] = (byte) 111;
      numArray2[11] = (byte) 14;
      numArray2[0] = (byte) 94;
      byte[] numArray3 = new byte[19];
      numArray3[9] = (byte) 221;
      numArray3[14] = (byte) 138;
      numArray3[2] = (byte) 96 /*0x60*/;
      numArray3[0] = (byte) 231;
      numArray3[4] = (byte) 228;
      numArray3[5] = (byte) 145;
      numArray3[10] = (byte) 74;
      numArray3[1] = (byte) 96 /*0x60*/;
      numArray3[8] = (byte) 118;
      numArray3[11] = (byte) 221;
      numArray3[18] = (byte) 106;
      numArray3[7] = (byte) 1;
      numArray3[6] = (byte) 51;
      numArray3[3] = (byte) 137;
      numArray3[13] = (byte) 84;
      numArray3[15] = (byte) 110;
      numArray3[16 /*0x10*/] = (byte) 106;
      numArray3[17] = (byte) 64 /*0x40*/;
      numArray3[12] = (byte) 165;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 15,
      (byte) 24,
      (byte) 117,
      (byte) 202,
      (byte) 242,
      (byte) 152,
      (byte) 128 /*0x80*/,
      (byte) 231,
      (byte) 230,
      (byte) 162,
      (byte) 173,
      (byte) 8,
      (byte) 51,
      (byte) 4,
      (byte) 77,
      (byte) 126,
      (byte) 154,
      (byte) 88,
      (byte) 69
    };
    byte[] numArray6 = new byte[19]
    {
      (byte) 43,
      (byte) 69,
      (byte) 245,
      (byte) 232,
      (byte) 19,
      (byte) 163,
      (byte) 200,
      (byte) 109,
      (byte) 166,
      (byte) 231,
      (byte) 175,
      (byte) 193,
      (byte) 4,
      (byte) 36,
      (byte) 122,
      (byte) 186,
      (byte) 179,
      (byte) 35,
      (byte) 229
    };
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
