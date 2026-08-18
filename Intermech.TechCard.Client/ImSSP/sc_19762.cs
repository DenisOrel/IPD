// Decompiled with JetBrains decompiler
// Type: ImSSP.sc_19762
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Protection;
using System;
using System.Text;

#nullable disable
namespace ImSSP;

internal static class sc_19762
{
  internal static string ssp_techcard_19763()
  {
    IProtectionKey key = ProtectionService.Key;
    if (DateTime.Now.Month == 9)
    {
      byte[] numArray1 = new byte[19];
      byte[] numArray2 = new byte[19]
      {
        (byte) 228,
        (byte) 167,
        (byte) 0,
        (byte) 3,
        (byte) 54,
        (byte) 165,
        (byte) 19,
        (byte) 90,
        (byte) 116,
        (byte) 124,
        (byte) 179,
        (byte) 107,
        (byte) 182,
        (byte) 55,
        (byte) 232,
        (byte) 3,
        (byte) 104,
        (byte) 101,
        (byte) 100
      };
      byte[] numArray3 = new byte[19];
      numArray3[5] = (byte) 83;
      numArray3[1] = (byte) 65;
      numArray3[0] = (byte) 217;
      numArray3[12] = (byte) 79;
      numArray3[2] = (byte) 106;
      numArray3[11] = (byte) 199;
      numArray3[4] = (byte) 53;
      numArray3[7] = (byte) 230;
      numArray3[3] = (byte) 183;
      numArray3[9] = (byte) 34;
      numArray3[15] = (byte) 196;
      numArray3[17] = (byte) 224 /*0xE0*/;
      numArray3[18] = (byte) 44;
      numArray3[13] = (byte) 81;
      numArray3[14] = (byte) 206;
      numArray3[16 /*0x10*/] = (byte) 9;
      numArray3[8] = (byte) 232;
      numArray3[6] = (byte) 48 /*0x30*/;
      numArray3[10] = (byte) 154;
      key.Query(true, 359, numArray2, numArray2);
      Array.Copy((Array) numArray2, 0, (Array) numArray1, 0, 19);
      for (int index = 0; index < 19; ++index)
        numArray1[index] ^= numArray3[index];
      return Encoding.UTF8.GetString(numArray1);
    }
    byte[] numArray4 = new byte[19];
    byte[] numArray5 = new byte[19]
    {
      (byte) 71,
      (byte) 57,
      (byte) 140,
      (byte) 128 /*0x80*/,
      (byte) 153,
      (byte) 150,
      (byte) 70,
      (byte) 178,
      (byte) 85,
      (byte) 220,
      (byte) 204,
      (byte) 8,
      (byte) 236,
      (byte) 147,
      (byte) 71,
      (byte) 54,
      (byte) 105,
      (byte) 47,
      (byte) 44
    };
    byte[] numArray6 = new byte[19];
    numArray6[17] = (byte) 164;
    numArray6[15] = (byte) 129;
    numArray6[9] = (byte) 234;
    numArray6[13] = (byte) 155;
    numArray6[4] = (byte) 98;
    numArray6[18] = (byte) 117;
    numArray6[6] = (byte) 156;
    numArray6[0] = (byte) 74;
    numArray6[8] = (byte) 61;
    numArray6[7] = (byte) 251;
    numArray6[10] = (byte) 229;
    numArray6[1] = (byte) 186;
    numArray6[3] = (byte) 197;
    numArray6[11] = (byte) 22;
    numArray6[14] = (byte) 226;
    numArray6[2] = (byte) 19;
    numArray6[16 /*0x10*/] = (byte) 67;
    numArray6[12] = (byte) 109;
    numArray6[5] = (byte) 2;
    key.Query(true, 359, numArray5, numArray5);
    Array.Copy((Array) numArray5, 0, (Array) numArray4, 0, 19);
    for (int index = 0; index < 19; ++index)
      numArray4[index] ^= numArray6[index];
    return Encoding.UTF8.GetString(numArray4);
  }
}
